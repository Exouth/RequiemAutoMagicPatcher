using System.Text;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;
using RequiemAutoMagicPatcher.Models;

namespace RequiemAutoMagicPatcher.Helpers;

public class SpellHelper : ISpellHelper
{
    private static readonly Dictionary<string, double> PercentageIncreases = new()
    {
        { "Novice", 236.17 },
        { "Apprentice", 91.95 },
        { "Adept", 82.50 },
        { "Expert", 167.18 },
        { "Master", 180 }
    };

    private static readonly List<SpellFlagRule> SpellFlagRules = new()
    {
        new SpellFlagRule("Conjuration", 50, TargetType.TargetLocation, 0x01000001),
        new SpellFlagRule("Conjuration", 25, TargetType.TargetLocation, 0x01000001),
        new SpellFlagRule("Conjuration", 75, TargetType.TargetLocation, 0x01000001),
        new SpellFlagRule("Conjuration", 100, TargetType.TargetLocation, 0x01000001),

        new SpellFlagRule("Alteration", 50, TargetType.Self, 0x01100001),
        new SpellFlagRule("Alteration", 75, TargetType.Aimed, 0x00000001),
        new SpellFlagRule("Alteration", 50, TargetType.TargetActor, 0x00000001),

        new SpellFlagRule("Destruction", 75, TargetType.Aimed, 0x00000001),
        new SpellFlagRule("Destruction", 50, TargetType.Aimed, 0x00000001),
        new SpellFlagRule("Destruction", 25, TargetType.Aimed, 0x00000001),

        new SpellFlagRule("Illusion", 25, TargetType.Aimed, 0x00000001),
        new SpellFlagRule("Illusion", 50, TargetType.Self, 0x01100001),

        new SpellFlagRule("Restoration", 75, TargetType.Aimed, 0x00000001),
        new SpellFlagRule("Restoration", 25, TargetType.Aimed, 0x00000001)
    };

    private static readonly Dictionary<string, int> SkillLevelMapping = new()
    {
        { "Novice", 0 },
        { "Apprentice", 25 },
        { "Adept", 50 },
        { "Expert", 75 },
        { "Master", 100 }
    };

    private static readonly Dictionary<string, string> FlagTranslations = new()
    {
        { "00000000000000000000000000000001", "Manual Cost Calc" },
        { "00000000000000000000000000000010", "Unknown 2" },
        { "00000000000000000000000000000100", "Unknown 3" },
        { "00000000000000000000000000001000", "Unknown 4" },
        { "00000000000000000000000000010000", "Unknown 5" },
        { "00000000000000000000000000100000", "Unknown 6" },
        { "00000000000000000000000001000000", "Unknown 7" },
        { "00000000000000000000000010000000", "Unknown 8" },
        { "00000000000000000000000100000000", "Unknown 9" },
        { "00000000000000000000001000000000", "Unknown 10" },
        { "00000000000000000000010000000000", "Unknown 11" },
        { "00000000000000000000100000000000", "Unknown 12" },
        { "00000000000000000001000000000000", "Unknown 13" },
        { "00000000000000000010000000000000", "Unknown 14" },
        { "00000000000000000100000000000000", "Unknown 15" },
        { "00000000000000001000000000000000", "Unknown 16" },
        { "00000000000000010000000000000000", "Unknown 17" },
        { "00000000000000100000000000000000", "PC Start Spell" },
        { "00000000000001000000000000000000", "Unknown 19" },
        { "00000000000010000000000000000000", "Area Effect Ignore LOS" },
        { "00000000000100000000000000000000", "Ignore Resistance" },
        { "00000000001000000000000000000000", "No Absorb/Reflect" },
        { "00000000010000000000000000000000", "Unknown 23" },
        { "00000000100000000000000000000000", "No Dual Cast Modification" },
        { "00000001000000000000000000000000", "Unknown 25" },
        { "00000010000000000000000000000000", "Unknown 26" },
        { "00000100000000000000000000000000", "Unknown 27" },
        { "00001000000000000000000000000000", "Unknown 28" },
        { "00010000000000000000000000000000", "Unknown 29" },
        { "00100000000000000000000000000000", "Unknown 30" },
        { "01000000000000000000000000000000", "Unknown 31" },
        { "10000000000000000000000000000000", "Unknown 32" }
    };

    public double GetPercentageIncrease(string spellLevel)
    {
        return PercentageIncreases.GetValueOrDefault(spellLevel, 0);
    }

    public bool HasPercentageIncrease(string spellLevel)
    {
        return PercentageIncreases.ContainsKey(spellLevel);
    }

    public SpellFlagRule? GetMatchingSpellFlagRule(string magicSchool, int skillLevel, TargetType targetType)
    {
        return SpellFlagRules.FirstOrDefault(rule =>
            rule.MagicSchool == magicSchool &&
            rule.SkillLevel == skillLevel &&
            rule.TargetType == targetType);
    }

    public bool TryGetSkillLevel(string skillLevelString, out int skillLevel)
    {
        return SkillLevelMapping.TryGetValue(skillLevelString, out skillLevel);
    }

    public string? GetSpellLevelFromEffects(IPatcherState<ISkyrimMod, ISkyrimModGetter> state, ISpellGetter spell)
    {
        var maxLevel = -1;
        IMagicEffectGetter? maxBaseEffect = null;

        foreach (var effect in spell.Effects)
        {
            if (effect.Data == null) continue;

            var baseEffect = effect.BaseEffect.TryResolve(state.LinkCache);

            if (baseEffect == null) continue;

            var skillLevel = (int)baseEffect.MinimumSkillLevel;
            var spellLevel = skillLevel / 25;
            spellLevel = Math.Max(0, Math.Min(4, spellLevel));

            if (spellLevel <= maxLevel) continue;

            maxLevel = spellLevel;
            maxBaseEffect = baseEffect;
        }

        if (maxBaseEffect == null) return null;

        return maxLevel switch
        {
            0 => "Novice",
            1 => "Apprentice",
            2 => "Adept",
            3 => "Expert",
            4 => "Master",
            _ => null
        };
    }

    public string? GetMagicSchoolFromEffects(IPatcherState<ISkyrimMod, ISkyrimModGetter> state, ISpellGetter spell)
    {
        foreach (var effect in spell.Effects)
        {
            if (effect.Data == null) continue;

            var baseEffect = effect.BaseEffect.TryResolve(state.LinkCache);
            if (baseEffect != null) return baseEffect.MagicSkill.ToString();
        }

        return null;
    }

    public string TranslateFlags(uint flags)
    {
        var flagDescriptions = new StringBuilder();
        var binaryString = Convert.ToString(flags, 2).PadLeft(32, '0');

        for (var i = 0; i < binaryString.Length; i++)
        {
            if (binaryString[31 - i] != '1') continue;

            var key = new string('0', 31 - i) + '1' + new string('0', i);
            flagDescriptions.Append(FlagTranslations.TryGetValue(key, out var flagName)
                ? flagName
                : $"Unknown {i + 1}");
            flagDescriptions.Append(", ");
        }

        return flagDescriptions.Length > 0
            ? flagDescriptions.ToString(0, flagDescriptions.Length - 2)
            : "No Flags";
    }
}