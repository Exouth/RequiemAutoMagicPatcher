using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;

namespace RequiemAutoMagicPatcher.Processors;

public class BookProcessor : IBookProcessor
{
    public List<(string FormID, string EditorID, string? Name, uint OriginalValue, uint PatchedValue)>
        ProcessBooksValue(
            IPatcherState<ISkyrimMod, ISkyrimModGetter> state, ISkyrimModGetter mod)
    {
        var changes = new List<(string FormID, string EditorID, string? Name, uint OriginalValue, uint PatchedValue)>();
        var count = 0;

        foreach (var book in mod.Books.EnumerateMajorRecords<IBookGetter>())
        {
            if (book.Teaches is not IBookSpellGetter spellTeach || spellTeach.Spell == null) continue;

            var spell = state.LinkCache.Resolve<ISpellGetter>(spellTeach.Spell.FormKey);
            var spellLevel = PatcherService.SpellHelper.GetSpellLevelFromEffects(state, spell);

            if (spellLevel == null || !PatcherService.SpellHelper.HasPercentageIncrease(spellLevel)) continue;

            var modifiedBook = state.PatchMod.Books.GetOrAddAsOverride(book);
            var originalValue = book.Value;
            var percentage = PatcherService.SpellHelper.GetPercentageIncrease(spellLevel);
            var newValue = (uint)(originalValue * (1 + percentage / 100));
            count++;

            changes.Add((book.FormKey.ToString(), book.EditorID ?? "Unknown EditorID",
                book.Name?.ToString() ?? "Unknown Book", originalValue, newValue));
            modifiedBook.Value = newValue;

            Console.WriteLine($"Patched Book: {book.Name}, New Value: {newValue}");
        }

        Console.WriteLine($"Total Books patched: {count}");
        return changes;
    }
}