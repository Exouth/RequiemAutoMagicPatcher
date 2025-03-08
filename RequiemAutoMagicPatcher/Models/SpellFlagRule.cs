using Mutagen.Bethesda.Skyrim;

namespace RequiemAutoMagicPatcher.Models;

/// <summary>
///     Represents a rule for spell flags based on magic school, skill level, and target type.
/// </summary>
public record SpellFlagRule(
    string MagicSchool,
    int SkillLevel,
    TargetType TargetType,
    uint Flags)
{
    /// <summary>
    ///     Creates a new instance of the <see cref="SpellFlagRule" /> class.
    /// </summary>
    /// <param name="magicSchool">The magic school of the spell (e.g., "Conjuration", "Alteration").</param>
    /// <param name="skillLevel">The skill level required for the spell.</param>
    /// <param name="targetType">The target type of the spell (e.g., "Self", "Aimed").</param>
    /// <param name="flags">The flags associated with the spell.</param>
    /// <returns>A new instance of the <see cref="SpellFlagRule" /> class.</returns>
    public static SpellFlagRule Create(string magicSchool, int skillLevel, TargetType targetType, uint flags)
    {
        ArgumentNullException.ThrowIfNull(magicSchool);
        if (skillLevel < 0) throw new ArgumentException("Skill level must be positive", nameof(skillLevel));

        return new SpellFlagRule(magicSchool, skillLevel, targetType, flags);
    }
}