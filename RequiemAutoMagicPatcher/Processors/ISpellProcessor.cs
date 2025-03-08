using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;
using RequiemAutoMagicPatcher.Settings;

namespace RequiemAutoMagicPatcher.Processors;

/// <summary>
///     Interface for processing magic spells within the Requiem patcher.
/// </summary>
public interface ISpellProcessor
{
    /// <summary>
    ///     Processes and adjusts the base cost of spells.
    /// </summary>
    /// <param name="state">The current patcher state, providing access to the load order and patch mod.</param>
    /// <param name="mod">The mod containing the spells to be processed.</param>
    /// <param name="spellSettings">The settings that define how the spell base costs should be adjusted.</param>
    /// <returns>
    ///     A list of tuples containing the FormID, EditorID, SpellName, OriginalValue, and PatchedValue of each modified
    ///     spell.
    /// </returns>
    /// <remarks>
    ///     This method iterates through all spells in the provided mod, calculates new base costs based on the spell level
    ///     and the provided settings, and applies these changes to the patch mod. Spells with a base cost of 0 or above
    ///     the skip threshold are ignored.
    /// </remarks>
    List<(string FormID, string EditorID, string? SpellName, uint OriginalValue, uint PatchedValue)>
        ProcessSpellsBaseCost(
            IPatcherState<ISkyrimMod, ISkyrimModGetter> state, ISkyrimModGetter mod,
            SpellPatchSettings.SpellSettings spellSettings);

    /// <summary>
    ///     Analyzes and modifies spell flags based.
    /// </summary>
    /// <param name="state">The current patcher state, providing access to the load order and patch mod.</param>
    /// <param name="mod">The mod containing the spells to be processed.</param>
    /// <returns>
    ///     A list of tuples containing the FormID, EditorID, Name, OriginalFlags, and PatchedFlags of each modified
    ///     spell.
    /// </returns>
    /// <remarks>
    ///     This method iterates through all spells in the provided mod, identifies the magic school, skill level, and target
    ///     type
    ///     of each spell, and applies matching flag rules to the spell. Spells that do not match the criteria are skipped.
    /// </remarks>
    List<(string FormID, string EditorID, string? Name, string OriginalFlags, string PatchedFlags)> ProcessSpellFlags(
        IPatcherState<ISkyrimMod, ISkyrimModGetter> state, ISkyrimModGetter mod);
}