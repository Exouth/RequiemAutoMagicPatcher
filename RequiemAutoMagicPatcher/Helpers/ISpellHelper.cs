using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;
using RequiemAutoMagicPatcher.Models;

namespace RequiemAutoMagicPatcher.Helpers;

/// <summary>
///     Interface that provides utility functions for analyzing and processing magic spells.
///     Defines methods for retrieving spell-related attributes such as spell level, magic school,
///     flag translations, and percentage increases for cost adjustments.
/// </summary>
public interface ISpellHelper
{
    /// <summary>
    ///     Determines the spell level based on its effects.
    /// </summary>
    /// <param name="state">The current patcher state, providing access to the load order and patch mod.</param>
    /// <param name="spell">The spell whose level is to be determined.</param>
    /// <returns>
    ///     A string representing the spell level ("Novice", "Apprentice", "Adept", "Expert", "Master"), or null if the level
    ///     cannot be determined.
    /// </returns>
    /// <remarks>
    ///     This method iterates through the effects of the given spell, resolves the base effects, and determines the spell
    ///     level
    ///     based on the highest minimum skill level required by the effects. The spell level is categorized into one of five
    ///     levels:
    ///     "Novice", "Apprentice", "Adept", "Expert", or "Master".
    /// </remarks>
    string? GetSpellLevelFromEffects(IPatcherState<ISkyrimMod, ISkyrimModGetter> state, ISpellGetter spell);

    /// <summary>
    ///     Retrieves the magic school associated with a spell based on its effects.
    /// </summary>
    /// <param name="state">The current patcher state, providing access to the load order and patch mod.</param>
    /// <param name="spell">The spell whose magic school is to be determined.</param>
    /// <returns>
    ///     A string representing the magic school of the spell, or null if the magic school cannot be determined.
    /// </returns>
    /// <remarks>
    ///     This method iterates through the effects of the given spell, resolves the base effects, and retrieves the
    ///     magic school associated with the first effect that has a valid base effect.
    /// </remarks>
    string? GetMagicSchoolFromEffects(IPatcherState<ISkyrimMod, ISkyrimModGetter> state, ISpellGetter spell);

    /// <summary>
    ///     Translates a given spell flag value into a human-readable string.
    /// </summary>
    /// <param name="flags">The spell flag value to be translated.</param>
    /// <returns>
    ///     A string containing the human-readable descriptions of the flags, separated by commas.
    ///     If a flag is not recognized, it is labeled as "Unknown" followed by its position.
    /// </returns>
    /// <remarks>
    ///     This method converts the given flag value to a binary string, iterates through each bit,
    ///     and translates the set bits into their corresponding descriptions using a predefined dictionary.
    /// </remarks>
    string TranslateFlags(uint flags);

    /// <summary>
    ///     Retrieves the percentage increase associated with a spell level.
    /// </summary>
    /// <param name="spellLevel">The level of the spell (e.g., "Novice", "Apprentice", "Adept", "Expert", "Master").</param>
    /// <returns>
    ///     A double representing the percentage increase for the given spell level, or 0 if the level is not found.
    /// </returns>
    /// <remarks>
    ///     This method looks up the percentage increase for the specified spell level from a predefined dictionary.
    /// </remarks>
    double GetPercentageIncrease(string spellLevel);

    /// <summary>
    ///     Checks if a given spell level has a defined percentage increase.
    /// </summary>
    /// <param name="spellLevel">The level of the spell (e.g., "Novice", "Apprentice", "Adept", "Expert", "Master").</param>
    /// <returns>
    ///     True if the spell level has a defined percentage increase; otherwise, false.
    /// </returns>
    /// <remarks>
    ///     This method checks if the specified spell level exists in the predefined dictionary of percentage increases.
    /// </remarks>
    bool HasPercentageIncrease(string spellLevel);

    /// <summary>
    ///     Finds a matching spell flag rule based on magic school, skill level, and target type.
    /// </summary>
    /// <param name="magicSchool">The magic school of the spell (e.g., "Conjuration", "Alteration").</param>
    /// <param name="skillLevel">The skill level required for the spell.</param>
    /// <param name="targetType">The target type of the spell (e.g., "Self", "Aimed").</param>
    /// <returns>
    ///     A SpellFlagRule object that matches the specified criteria, or null if no match is found.
    /// </returns>
    /// <remarks>
    ///     This method searches through a predefined list of spell flag rules to find a rule that matches
    ///     the specified magic school, skill level, and target type.
    /// </remarks>
    SpellFlagRule? GetMatchingSpellFlagRule(string magicSchool, int skillLevel, TargetType targetType);

    /// <summary>
    ///     Tries to retrieve the numerical skill level from a skill level string.
    /// </summary>
    /// <param name="skillLevelString">The skill level string (e.g., "Novice", "Apprentice", "Adept", "Expert", "Master").</param>
    /// <param name="skillLevel">
    ///     When this method returns, contains the numerical skill level if the conversion succeeded, or 0
    ///     if it failed.
    /// </param>
    /// <returns>
    ///     True if the skill level string was successfully converted to a numerical skill level; otherwise, false.
    /// </returns>
    /// <remarks>
    ///     This method attempts to convert the given skill level string to its corresponding numerical value using a
    ///     predefined dictionary.
    /// </remarks>
    bool TryGetSkillLevel(string skillLevelString, out int skillLevel);
}