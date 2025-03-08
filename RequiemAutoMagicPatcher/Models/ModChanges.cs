namespace RequiemAutoMagicPatcher.Models;

/// <summary>
///     Represents the changes made to a mod during the patching process.
/// </summary>
public class ModChanges
{
    /// <summary>
    ///     Gets or sets the list of changes made to books.
    /// </summary>
    /// <remarks>
    ///     Each tuple contains the FormID, EditorID, Name, OriginalValue, and PatchedValue of the book.
    /// </remarks>
    public List<(string FormID, string EditorID, string? Name, uint OriginalValue, uint PatchedValue)> BookChanges
    {
        get;
        set;
    } = new();

    /// <summary>
    ///     Gets or sets the list of changes made to spell base costs.
    /// </summary>
    /// <remarks>
    ///     Each tuple contains the FormID, EditorID, SpellName, OriginalValue, and PatchedValue of the spell.
    /// </remarks>
    public List<(string FormID, string EditorID, string? SpellName, uint OriginalValue, uint PatchedValue)>
        SpellBaseCostChanges { get; set; } = new();

    /// <summary>
    ///     Gets or sets the list of changes made to spell flags.
    /// </summary>
    /// <remarks>
    ///     Each tuple contains the FormID, EditorID, SpellName, OriginalFlags, and PatchedFlags of the spell.
    /// </remarks>
    public List<(string FormID, string EditorID, string? SpellName, string OriginalFlags, string PatchedFlags)>
        SpellFlagChanges { get; set; } = new();
}