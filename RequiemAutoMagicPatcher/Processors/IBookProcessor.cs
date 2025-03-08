using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;

namespace RequiemAutoMagicPatcher.Processors;

/// <summary>
///     Interface for processing spell tomes (books) within the Requiem patcher.
/// </summary>
public interface IBookProcessor
{
    /// <summary>
    ///     Adjusts the value of spell tomes based on the spell level they teach.
    /// </summary>
    /// <param name="state">The current patcher state, providing access to the load order and patch mod.</param>
    /// <param name="mod">The mod containing the books to be processed.</param>
    /// <returns>A list of tuples containing the FormID, EditorID, Name, OriginalValue, and PatchedValue of each modified book.</returns>
    /// <remarks>
    ///     This method iterates through all books in the provided mod, identifies those that teach spells, and adjusts their
    ///     value
    ///     based on the level of the spell they teach. The new value is calculated using a percentage increase defined in the
    ///     settings.
    ///     Books that do not teach spells or whose spell level does not have a defined percentage increase are skipped.
    /// </remarks>
    List<(string FormID, string EditorID, string? Name, uint OriginalValue, uint PatchedValue)> ProcessBooksValue(
        IPatcherState<ISkyrimMod, ISkyrimModGetter> state, ISkyrimModGetter mod);
}