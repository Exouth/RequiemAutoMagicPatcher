using RequiemAutoMagicPatcher.Helpers;
using RequiemAutoMagicPatcher.Processors;
using RequiemAutoMagicPatcher.Settings;

namespace RequiemAutoMagicPatcher;

public static class PatcherService
{
    public static Lazy<SpellPatchSettings> _settings = new(() => new SpellPatchSettings());
    private static readonly Lazy<SpellHelper> _spellHelper = new(() => new SpellHelper());
    private static readonly Lazy<BookProcessor> _bookProcessor = new(() => new BookProcessor());
    private static readonly Lazy<SpellProcessor> _spellProcessor = new(() => new SpellProcessor());

    public static ISpellHelper SpellHelper => _spellHelper.Value;
    public static IBookProcessor BookProcessor => _bookProcessor.Value;
    public static ISpellProcessor SpellProcessor => _spellProcessor.Value;
    public static SpellPatchSettings Settings => _settings.Value;
}