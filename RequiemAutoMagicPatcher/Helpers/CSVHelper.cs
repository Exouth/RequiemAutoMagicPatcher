using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace RequiemAutoMagicPatcher.Helpers;

public static class CsvHelper
{
    public static void ExportChangesToCsv<T>(
        string filePath,
        List<(string FormID, string EditorID, string? Name, T OriginalValue, T PatchedValue)> bookChanges,
        List<(string FormID, string EditorID, string? SpellName, T OriginalValue, T PatchedValue)> spellBaseCostChanges,
        List<(string FormID, string EditorID, string? SpellName, string OriginalFlags, string PatchedFlags)>
            spellFlagChanges)
    {
        using var writer = new StreamWriter(filePath);
        var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";", Encoding = Encoding.UTF8 };
        using var csv = new CsvWriter(writer, config);

        var hasChanges = false;

        if (bookChanges.Any())
        {
            AddSheetToCsv(csv, "Book - Value", bookChanges);
            hasChanges = true;
        }

        if (spellBaseCostChanges.Any())
        {
            AddSheetToCsv(csv, "Spell - BaseCost", spellBaseCostChanges);
            hasChanges = true;
        }

        if (spellFlagChanges.Any())
        {
            AddSheetToCsv(csv, "Spell - Flags", spellFlagChanges);
            hasChanges = true;
        }

        if (hasChanges) return;
        csv.WriteField("No Changes");
        csv.NextRecord();
    }

    private static void AddSheetToCsv<T>(CsvWriter csv, string sheetName,
        List<(string FormID, string EditorID, string? Name, T OriginalValue, T PatchedValue)> changes)
    {
        if (!changes.Any()) return;

        csv.WriteField(sheetName);
        csv.NextRecord();

        csv.WriteField("FormID");
        csv.WriteField("EditorID");
        csv.WriteField("Name");
        csv.WriteField("Original Value");
        csv.WriteField("Patched Value");
        csv.NextRecord();

        foreach (var change in changes)
        {
            csv.WriteField(change.FormID);
            csv.WriteField(change.EditorID);
            csv.WriteField(change.Name ?? "NULL");
            csv.WriteField(change.OriginalValue?.ToString() ?? "NULL");
            csv.WriteField(change.PatchedValue?.ToString() ?? "NULL");
            csv.NextRecord();
        }

        csv.Flush();
    }
}