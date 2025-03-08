using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;
using RequiemAutoMagicPatcher.Settings;

namespace RequiemAutoMagicPatcher.Processors;

public class SpellProcessor : ISpellProcessor
{
    public List<(string FormID, string EditorID, string? SpellName, uint OriginalValue, uint PatchedValue)>
        ProcessSpellsBaseCost(
            IPatcherState<ISkyrimMod, ISkyrimModGetter> state, ISkyrimModGetter mod,
            SpellPatchSettings.SpellSettings spellSettings)
    {
        var changes =
            new List<(string FormID, string EditorID, string? SpellName, uint OriginalValue, uint PatchedValue)>();
        var count = 0;

        foreach (var spell in mod.Spells.EnumerateMajorRecords<ISpellGetter>())
        {
            var baseCost = spell.BaseCost;

            if (baseCost == 0 || baseCost > spellSettings.BaseCost.BaseCostSkipThreshold)
            {
                Console.WriteLine($"Skipped Spell: {spell.EditorID}");
                continue;
            }

            var spellLevel = PatcherService.SpellHelper.GetSpellLevelFromEffects(state, spell);

            if (spellLevel == null) continue;

            var (increasePercentage, decreasePercentage, baseCostThreshold) =
                GetSpellLevelSettings(spellLevel, spellSettings);

            var modifiedSpell = state.PatchMod.Spells.GetOrAddAsOverride(spell);

            var newBaseCost = CalculateNewBaseCost(baseCost, increasePercentage, decreasePercentage, baseCostThreshold,
                spellSettings);

            if (newBaseCost != baseCost)
            {
                modifiedSpell.BaseCost = newBaseCost;
                changes.Add((spell.FormKey.ToString(), spell.EditorID ?? "Unknown EditorID",
                    spell.Name?.ToString() ?? "Unknown Spell", baseCost, modifiedSpell.BaseCost));
            }

            count++;
            Console.WriteLine($"Patched Spell: {spell.EditorID}, New BaseCost: {modifiedSpell.BaseCost}");
        }

        Console.WriteLine($"Total Spells patched: {count}");
        return changes;
    }

    public List<(string FormID, string EditorID, string? Name, string OriginalFlags, string PatchedFlags)>
        ProcessSpellFlags(
            IPatcherState<ISkyrimMod, ISkyrimModGetter> state, ISkyrimModGetter mod)
    {
        var changes =
            new List<(string FormID, string EditorID, string? Name, string OriginalFlags, string PatchedFlags)>();
        var count = 0;

        foreach (var spell in mod.Spells.EnumerateMajorRecords<ISpellGetter>())
        {
            if (spell.Type != 0)
            {
                Console.WriteLine($"Skipped non-spell type: {spell.EditorID}");
                continue;
            }

            var magicSchool = PatcherService.SpellHelper.GetMagicSchoolFromEffects(state, spell);
            var skillLevelString = PatcherService.SpellHelper.GetSpellLevelFromEffects(state, spell);
            var targetType = spell.TargetType;

            if (skillLevelString == null ||
                !PatcherService.SpellHelper.TryGetSkillLevel(skillLevelString, out var skillLevel)) continue;

            if (magicSchool == null) continue;

            var matchingRule = PatcherService.SpellHelper.GetMatchingSpellFlagRule(magicSchool, skillLevel, targetType);

            if (matchingRule == null) continue;

            var modifiedSpell = state.PatchMod.Spells.GetOrAddAsOverride(spell);
            var previousFlags = modifiedSpell.Flags;
            modifiedSpell.Flags = (SpellDataFlag)((uint)previousFlags | matchingRule.Flags);

            count++;
            Console.WriteLine(
                $"Patched SPIT Flags for Spell: {spell.EditorID}, Previous Flags: {PatcherService.SpellHelper.TranslateFlags((uint)previousFlags)}, New Flags: {PatcherService.SpellHelper.TranslateFlags((uint)modifiedSpell.Flags)}");

            changes.Add((spell.FormKey.ToString(), spell.EditorID ?? "Unknown EditorID",
                spell.Name?.ToString() ?? "Unknown Spell",
                PatcherService.SpellHelper.TranslateFlags((uint)previousFlags),
                PatcherService.SpellHelper.TranslateFlags((uint)modifiedSpell.Flags)));
        }

        Console.WriteLine($"Total Spells patched for SPIT Flags: {count}");
        return changes;
    }

    private static (double increasePercentage, double decreasePercentage, double baseCostThreshold)
        GetSpellLevelSettings(
            string spellLevel, SpellPatchSettings.SpellSettings spellSettings)
    {
        return spellLevel switch
        {
            "Novice" => (spellSettings.BaseCost.NovicePercentageIncrease,
                spellSettings.BaseCost.NovicePercentageDecrease, spellSettings.BaseCost.NoviceBaseCostThreshold),
            "Apprentice" => (spellSettings.BaseCost.ApprenticePercentageIncrease,
                spellSettings.BaseCost.ApprenticePercentageDecrease,
                spellSettings.BaseCost.ApprenticeBaseCostThreshold),
            "Adept" => (spellSettings.BaseCost.AdeptPercentageIncrease, spellSettings.BaseCost.AdeptPercentageDecrease,
                spellSettings.BaseCost.AdeptBaseCostThreshold),
            "Expert" => (spellSettings.BaseCost.ExpertPercentageIncrease,
                spellSettings.BaseCost.ExpertPercentageDecrease, spellSettings.BaseCost.ExpertBaseCostThreshold),
            "Master" => (spellSettings.BaseCost.MasterPercentageIncrease,
                spellSettings.BaseCost.MasterPercentageDecrease, spellSettings.BaseCost.MasterBaseCostThreshold),
            _ => (0, 0, 0)
        };
    }

    private static uint CalculateNewBaseCost(uint baseCost, double increasePercentage, double decreasePercentage,
        double baseCostThreshold, SpellPatchSettings.SpellSettings spellSettings)
    {
        if (baseCost >= baseCostThreshold)
            return Math.Min((uint)Math.Round(baseCost * (1 + increasePercentage / 100)),
                Math.Min(baseCost * 2, spellSettings.BaseCost.MaxBaseCostAfterCalculation));

        return Math.Min((uint)Math.Round(baseCost * (1 - decreasePercentage / 100)),
            spellSettings.BaseCost.MaxBaseCostAfterCalculation);
    }
}