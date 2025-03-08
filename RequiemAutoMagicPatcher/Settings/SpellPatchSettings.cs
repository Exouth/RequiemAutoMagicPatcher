using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.WPF.Reflection.Attributes;

namespace RequiemAutoMagicPatcher.Settings
{
    public class SpellPatchSettings
    {
        [SettingName("Select Mods to Patch")]
        [Tooltip("Select which ESP mods to include in the patch.")]
        public List<ModKey> SelectedMods = new List<ModKey>();

        public BookSettings Books = new();
        public SpellSettings Spells = new();

        [SettingName("Enable Csv Report Changes")]
        [Tooltip("Enable or disable the creation of an Csv report of all Changes.")]
        public bool EnableCsvReportChanges { get; set; } = true;

        public class BookSettings
        {
            [SettingName("Patch Books")]
            [Tooltip("Enable or disable patching of spell tomes.")]
            public bool PatchBooks { get; set; } = true;

            public ValueSettings Value = new();

            public class ValueSettings
            {
                [SettingName("Novice Spell Percentage")]
                [Tooltip("Percentage increase for Novice level spell tomes.")]
                public double NovicePercentage { get; set; } = 236.17;

                [SettingName("Apprentice Spell Percentage")]
                [Tooltip("Percentage increase for Apprentice level spell tomes.")]
                public double ApprenticePercentage { get; set; } = 91.95;

                [SettingName("Adept Spell Percentage")]
                [Tooltip("Percentage increase for Adept level spell tomes.")]
                public double AdeptPercentage { get; set; } = 82.50;

                [SettingName("Expert Spell Percentage")]
                [Tooltip("Percentage increase for Expert level spell tomes.")]
                public double ExpertPercentage { get; set; } = 167.18;

                [SettingName("Master Spell Percentage")]
                [Tooltip("Percentage increase for Master level spell tomes.")]
                public double MasterPercentage { get; set; } = 180;
            }
        }

        public class SpellSettings
        {
            [SettingName("Patch Spells")]
            [Tooltip("Enable or disable patching of spell BaseCost values.")]
            public bool PatchSpells { get; set; } = true;

            public BaseCostSettings BaseCost = new();

            public class BaseCostSettings
            {
                [SettingName("BaseCost Skip Threshold")]
                [Tooltip("BaseCost threshold for skipping a spell. Spells with a BaseCost above this value will be skipped.")]
                public uint BaseCostSkipThreshold { get; set; } = 3500;

                [SettingName("Max BaseCost After Calculation")]
                [Tooltip("Maximum BaseCost allowed after calculation. If a spell exceeds this value, it will be capped.")]
                public uint MaxBaseCostAfterCalculation { get; set; } = 3500;

                [SettingName("Novice Spell Percentage Increase")]
                public double NovicePercentageIncrease { get; set; } = 236.17;

                [SettingName("Novice Spell Percentage Decrease")]
                public double NovicePercentageDecrease { get; set; } = 10;

                [SettingName("Novice BaseCost Threshold")]
                public double NoviceBaseCostThreshold { get; set; } = 100;

                [SettingName("Apprentice Spell Percentage Increase")]
                public double ApprenticePercentageIncrease { get; set; } = 91.95;

                [SettingName("Apprentice Spell Percentage Decrease")]
                public double ApprenticePercentageDecrease { get; set; } = 15;

                [SettingName("Apprentice BaseCost Threshold")]
                public double ApprenticeBaseCostThreshold { get; set; } = 150;

                [SettingName("Adept Spell Percentage Increase")]
                public double AdeptPercentageIncrease { get; set; } = 82.50;

                [SettingName("Adept Spell Percentage Decrease")]
                public double AdeptPercentageDecrease { get; set; } = 20;

                [SettingName("Adept BaseCost Threshold")]
                public double AdeptBaseCostThreshold { get; set; } = 200;

                [SettingName("Expert Spell Percentage Increase")]
                public double ExpertPercentageIncrease { get; set; } = 167.18;

                [SettingName("Expert Spell Percentage Decrease")]
                public double ExpertPercentageDecrease { get; set; } = 25;

                [SettingName("Expert BaseCost Threshold")]
                public double ExpertBaseCostThreshold { get; set; } = 300;

                [SettingName("Master Spell Percentage Increase")]
                public double MasterPercentageIncrease { get; set; } = 180;

                [SettingName("Master Spell Percentage Decrease")]
                public double MasterPercentageDecrease { get; set; } = 30;

                [SettingName("Master BaseCost Threshold")]
                public double MasterBaseCostThreshold { get; set; } = 400;
            }
        }
    }
}
