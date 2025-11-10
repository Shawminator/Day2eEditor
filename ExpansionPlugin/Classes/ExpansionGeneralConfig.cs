using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionGeneralConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path);
        public string FilePath => _path;
        public ExpansionGeneralSettings Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }

        public const int CurrentVersion = 16;

        public ExpansionGeneralConfig(string path)
        {
            _path = path;
        }

        public void Load()
        {
            Data = AppServices.GetRequired<FileService>().LoadOrCreateJson<ExpansionGeneralSettings>(
                _path,
                createNew: () => new ExpansionGeneralSettings(CurrentVersion),
                onAfterLoad: cfg => { /* optional: do something after load */ },
                onError: ex =>
                {
                    HasErrors = true;
                    Console.WriteLine(
                        "Error in " + Path.GetFileName(_path) + "\n" +
                        ex.Message + "\n" +
                        ex.InnerException?.Message + "\n"
                    );
                    Errors.Add("Error in " + Path.GetFileName(_path) + "\n" +
                        ex.Message + "\n" +
                        ex.InnerException?.Message);
                },
                configName: "ExpansionGeneral"
            );
            var missingFields = Data.FixMissingOrInvalidFields();
            if (missingFields.Any())
            {
                Console.WriteLine("Validation issues in " + FileName + ":");
                foreach (var issue in missingFields)
                {
                    Console.WriteLine("- " + issue);
                }
                isDirty = true;
            }
        }
        public IEnumerable<string> Save()
        {
            if (isDirty)
            {
                AppServices.GetRequired<FileService>().SaveJson(_path, Data);
                isDirty = false;
                return new[] { Path.GetFileName(_path) };
            }

            return Array.Empty<string>();
        }

        public bool needToSave()
        {
            return isDirty;
        }
    }
    public class ExpansionGeneralSettings
    {
        public int m_Version { get; set; }
        public int? DisableShootToUnlock { get; set; }
        public int? EnableGravecross { get; set; }
        public int? EnableAIGravecross { get; set; }
        public int? GravecrossDeleteBody { get; set; }
        public decimal? GravecrossTimeThreshold { get; set; }
        public decimal? GravecrossSpawnTimeDelay { get; set; }
        public ExpansionMapping Mapping { get; set; }
        public int? EnableLamps { get; set; }
        public int? LampAmount_OneInX { get; set; }
        public string? LampSelectionMode { get; set; }
        public int? EnableGenerators { get; set; }
        public int? EnableLighthouses { get; set; }
        public int? EnableHUDNightvisionOverlay { get; set; }
        public int? DisableMagicCrosshair { get; set; }
        public int? EnableAutoRun { get; set; }
        public int? UseDeathScreen { get; set; }
        public int? UseDeathScreenStatistics { get; set; }
        public int? UseExpansionMainMenuLogo { get; set; }
        public int? UseExpansionMainMenuIcons { get; set; }
        public int? UseExpansionMainMenuIntroScene { get; set; }
        public int? UseNewsFeedInGameMenu { get; set; }
        public int? UseHUDColors { get; set; }
        public ExpansionHudIndicatorColors HUDColors { get; set; }
        public int? EnableEarPlugs { get; set; }
        public string? InGameMenuLogoPath { get; set; }

        public ExpansionGeneralSettings() { }
        public ExpansionGeneralSettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
            DisableShootToUnlock = 0;
            EnableGravecross = 0;
            EnableAIGravecross = 0;
            GravecrossDeleteBody = 1;
            GravecrossTimeThreshold = 1200;
            GravecrossSpawnTimeDelay = 180;

            Mapping = new ExpansionMapping();

            EnableLamps = (int)LampModeEnum.AlwaysOnEverywhere;
            LampAmount_OneInX = 3;
            LampSelectionMode = "FARTHEST_RANDOM";

            EnableGenerators = 0;
            EnableLighthouses = 1;

            EnableHUDNightvisionOverlay = 1;

            DisableMagicCrosshair = 1;

            EnableAutoRun = 1;

            UseDeathScreen = 1;
            UseDeathScreenStatistics = 1;

            UseExpansionMainMenuLogo = 1;
            UseExpansionMainMenuIcons = 1;
            UseExpansionMainMenuIntroScene = 1;
            UseNewsFeedInGameMenu = 0;

            HUDColors = new ExpansionHudIndicatorColors();

            EnableEarPlugs = 1;
            InGameMenuLogoPath = "set:expansion_iconset image:logo_expansion_white";
        }

        public override bool Equals(object obj)
        {
            if (obj is not ExpansionGeneralSettings other)
                return false;

            return m_Version == other.m_Version &&
                   DisableShootToUnlock == other.DisableShootToUnlock &&
                   EnableGravecross == other.EnableGravecross &&
                   EnableAIGravecross == other.EnableAIGravecross &&
                   GravecrossDeleteBody == other.GravecrossDeleteBody &&
                   GravecrossTimeThreshold == other.GravecrossTimeThreshold &&
                   GravecrossSpawnTimeDelay == other.GravecrossSpawnTimeDelay &&
                   EnableLamps == other.EnableLamps &&
                   LampAmount_OneInX == other.LampAmount_OneInX &&
                   LampSelectionMode == other.LampSelectionMode &&
                   EnableGenerators == other.EnableGenerators &&
                   EnableLighthouses == other.EnableLighthouses &&
                   EnableHUDNightvisionOverlay == other.EnableHUDNightvisionOverlay &&
                   DisableMagicCrosshair == other.DisableMagicCrosshair &&
                   EnableAutoRun == other.EnableAutoRun &&
                   UseDeathScreen == other.UseDeathScreen &&
                   UseDeathScreenStatistics == other.UseDeathScreenStatistics &&
                   UseExpansionMainMenuLogo == other.UseExpansionMainMenuLogo &&
                   UseExpansionMainMenuIcons == other.UseExpansionMainMenuIcons &&
                   UseExpansionMainMenuIntroScene == other.UseExpansionMainMenuIntroScene &&
                   UseNewsFeedInGameMenu == other.UseNewsFeedInGameMenu &&
                   UseHUDColors == other.UseHUDColors &&
                   EnableEarPlugs == other.EnableEarPlugs &&
                   InGameMenuLogoPath == other.InGameMenuLogoPath &&
                   Equals(Mapping, other.Mapping) &&
                   Equals(HUDColors, other.HUDColors);
        }

        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (m_Version < ExpansionGeneralConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionGeneralConfig.CurrentVersion}");
                m_Version = ExpansionGeneralConfig.CurrentVersion;
            }

            if (DisableShootToUnlock == null || (DisableShootToUnlock != 0 && DisableShootToUnlock != 1))
            {
                DisableShootToUnlock = 0;
                fixes.Add("Corrected DisableShootToUnlock to 0");
            }

            if (EnableGravecross == null || (EnableGravecross != 0 && EnableGravecross != 1))
            {
                EnableGravecross = 1;
                fixes.Add("Corrected EnableGravecross to 1");
            }

            if (EnableAIGravecross == null || (EnableAIGravecross != 0 && EnableAIGravecross != 1))
            {
                EnableAIGravecross = 1;
                fixes.Add("Corrected EnableAIGravecross to 1");
            }

            if (GravecrossDeleteBody == null || (GravecrossDeleteBody != 0 && GravecrossDeleteBody != 1))
            {
                GravecrossDeleteBody = 1;
                fixes.Add("Corrected GravecrossDeleteBody to 1");
            }

            if (GravecrossTimeThreshold == null)
            {
                GravecrossTimeThreshold = 300.0m;
                fixes.Add("Set default GravecrossTimeThreshold to 300.0");
            }

            if (GravecrossSpawnTimeDelay == null)
            {
                GravecrossSpawnTimeDelay = 10.0m;
                fixes.Add("Set default GravecrossSpawnTimeDelay to 10.0");
            }


            if (Mapping == null)
            {
                Mapping = new ExpansionMapping();
                fixes.Add("Initialized Mapping object");
            }

            // Helper function to initialize BindingList<string> if null
            BindingList<string> EnsureListInitialized(BindingList<string> list, string name)
            {
                if (list == null)
                {
                    fixes.Add($"Initialized {name} list");
                    return new BindingList<string>();
                }
                return list;
            }

            Mapping.Mapping = EnsureListInitialized(Mapping.Mapping, "Mapping.Mapping");
            Mapping.Interiors = EnsureListInitialized(Mapping.Interiors, "Mapping.Interiors");

            // Validate integer properties
            if (Mapping.UseCustomMappingModule == null || (Mapping.UseCustomMappingModule != 0 && Mapping.UseCustomMappingModule != 1))
            {
                Mapping.UseCustomMappingModule = 0;
                fixes.Add("Corrected UseCustomMappingModule to 0");
            }

            if (Mapping.BuildingInteriors == null || (Mapping.BuildingInteriors != 0 && Mapping.BuildingInteriors != 1))
            {
                Mapping.BuildingInteriors = 0;
                fixes.Add("Corrected BuildingInteriors to 0");
            }

            if (Mapping.BuildingIvys == null || (Mapping.BuildingIvys < 0 && Mapping.BuildingIvys >2))
            {
                Mapping.BuildingIvys = (int)buildingIvy.Disabled;
                fixes.Add("Corrected BuildingIvys to Disabled");
            }

            if (EnableLamps == null || (EnableLamps < 0 || EnableLamps > 3))
            {
                EnableLamps = (int)LampModeEnum.AlwaysOnEverywhere;
                fixes.Add("Corrected EnableLamps to AlwaysOnEverywhere");
            }

            if (LampAmount_OneInX == null)
            {
                LampAmount_OneInX = 3;
                fixes.Add("Set default LampAmount_OneInX to 3");
            }

            if (string.IsNullOrWhiteSpace(LampSelectionMode))
            {
                LampSelectionMode = "FARTHEST_RANDOM";
                fixes.Add("Set default LampSelectionMode to 'Random'");
            }

            if (EnableGenerators == null || (EnableGenerators != 0 && EnableGenerators != 1))
            {
                EnableGenerators = 1;
                fixes.Add("Corrected EnableGenerators to 1");
            }

            if (EnableLighthouses == null || (EnableLighthouses != 0 && EnableLighthouses != 1))
            {
                EnableLighthouses = 1;
                fixes.Add("Corrected EnableLighthouses to 1");
            }

            if (EnableHUDNightvisionOverlay == null || (EnableHUDNightvisionOverlay != 0 && EnableHUDNightvisionOverlay != 1))
            {
                EnableHUDNightvisionOverlay = 1;
                fixes.Add("Corrected EnableHUDNightvisionOverlay to 1");
            }

            if (DisableMagicCrosshair == null || (DisableMagicCrosshair != 0 && DisableMagicCrosshair != 1))
            {
                DisableMagicCrosshair = 0;
                fixes.Add("Corrected DisableMagicCrosshair to 0");
            }

            if (EnableAutoRun == null || (EnableAutoRun != 0 && EnableAutoRun != 1))
            {
                EnableAutoRun = 1;
                fixes.Add("Corrected EnableAutoRun to 1");
            }

            if (UseDeathScreen == null || (UseDeathScreen != 0 && UseDeathScreen != 1))
            {
                UseDeathScreen = 1;
                fixes.Add("Corrected UseDeathScreen to 1");
            }

            if (UseDeathScreenStatistics == null || (UseDeathScreenStatistics != 0 && UseDeathScreenStatistics != 1))
            {
                UseDeathScreenStatistics = 1;
                fixes.Add("Corrected UseDeathScreenStatistics to 1");
            }

            if (UseExpansionMainMenuLogo == null || (UseExpansionMainMenuLogo != 0 && UseExpansionMainMenuLogo != 1))
            {
                UseExpansionMainMenuLogo = 1;
                fixes.Add("Corrected UseExpansionMainMenuLogo to 1");
            }

            if (UseExpansionMainMenuIcons == null || (UseExpansionMainMenuIcons != 0 && UseExpansionMainMenuIcons != 1))
            {
                UseExpansionMainMenuIcons = 1;
                fixes.Add("Corrected UseExpansionMainMenuIcons to 1");
            }

            if (UseExpansionMainMenuIntroScene == null || (UseExpansionMainMenuIntroScene != 0 && UseExpansionMainMenuIntroScene != 1))
            {
                UseExpansionMainMenuIntroScene = 1;
                fixes.Add("Corrected UseExpansionMainMenuIntroScene to 1");
            }

            if (UseNewsFeedInGameMenu == null || (UseNewsFeedInGameMenu != 0 && UseNewsFeedInGameMenu != 1))
            {
                UseNewsFeedInGameMenu = 1;
                fixes.Add("Corrected UseNewsFeedInGameMenu to 1");
            }

            if (UseHUDColors == null || (UseHUDColors != 0 && UseHUDColors != 1))
            {
                UseHUDColors = 1;
                fixes.Add("Corrected UseHUDColors to 1");
            }


            if (HUDColors == null)
            {
                HUDColors = new ExpansionHudIndicatorColors();
                fixes.Add("Initialized HUDColors");
            }

            // Helper function to set default color if null or whitespace
            string SetDefaultColor(string? currentValue, string defaultValue, string name)
            {
                if (string.IsNullOrWhiteSpace(currentValue))
                {
                    fixes.Add($"Set default {name}");
                    return defaultValue;
                }
                return currentValue;
            }

            HUDColors.StaminaBarColor = SetDefaultColor(HUDColors.StaminaBarColor, "#00FF00", "StaminaBarColor");
            HUDColors.StaminaBarColorHalf = SetDefaultColor(HUDColors.StaminaBarColorHalf, "#FFFF00", "StaminaBarColorHalf");
            HUDColors.StaminaBarColorLow = SetDefaultColor(HUDColors.StaminaBarColorLow, "#FF0000", "StaminaBarColorLow");
            HUDColors.NotifierDividerColor = SetDefaultColor(HUDColors.NotifierDividerColor, "#FFFFFF", "NotifierDividerColor");
            HUDColors.TemperatureBurningColor = SetDefaultColor(HUDColors.TemperatureBurningColor, "#FF4500", "TemperatureBurningColor");
            HUDColors.TemperatureHotColor = SetDefaultColor(HUDColors.TemperatureHotColor, "#FFA500", "TemperatureHotColor");
            HUDColors.TemperatureIdealColor = SetDefaultColor(HUDColors.TemperatureIdealColor, "#00FF00", "TemperatureIdealColor");
            HUDColors.TemperatureColdColor = SetDefaultColor(HUDColors.TemperatureColdColor, "#00BFFF", "TemperatureColdColor");
            HUDColors.TemperatureFreezingColor = SetDefaultColor(HUDColors.TemperatureFreezingColor, "#1E90FF", "TemperatureFreezingColor");
            HUDColors.NotifiersIdealColor = SetDefaultColor(HUDColors.NotifiersIdealColor, "#00FF00", "NotifiersIdealColor");
            HUDColors.NotifiersHalfColor = SetDefaultColor(HUDColors.NotifiersHalfColor, "#FFFF00", "NotifiersHalfColor");
            HUDColors.NotifiersLowColor = SetDefaultColor(HUDColors.NotifiersLowColor, "#FF0000", "NotifiersLowColor");
            HUDColors.ReputationBaseColor = SetDefaultColor(HUDColors.ReputationBaseColor, "#808080", "ReputationBaseColor");
            HUDColors.ReputationMedColor = SetDefaultColor(HUDColors.ReputationMedColor, "#00CED1", "ReputationMedColor");
            HUDColors.ReputationHighColor = SetDefaultColor(HUDColors.ReputationHighColor, "#FFD700", "ReputationHighColor");


            if (EnableEarPlugs == null || (EnableEarPlugs != 0 && EnableEarPlugs != 1))
            {
                EnableEarPlugs = 1;
                fixes.Add("Corrected EnableEarPlugs to 1");
            }

            if (string.IsNullOrWhiteSpace(InGameMenuLogoPath))
            {
                InGameMenuLogoPath = "default_logo.png";
                fixes.Add("Set default InGameMenuLogoPath to 'default_logo.png'");
            }

            return fixes;
        }



    }
    public class ExpansionMapping
    {
        public int UseCustomMappingModule { get; set; }
        public BindingList<string> Mapping { get; set; }
        public int BuildingInteriors { get; set; }
        public BindingList<string> Interiors { get; set; }
        public int BuildingIvys { get; set; }
        public ExpansionMapping() { }

        public override bool Equals(object obj)
        {
            if (obj is not ExpansionMapping other)
                return false;

            return UseCustomMappingModule == other.UseCustomMappingModule &&
                   BuildingInteriors == other.BuildingInteriors &&
                   BuildingIvys == other.BuildingIvys &&
                   Mapping.SequenceEqual(other.Mapping ?? new BindingList<string>()) &&
                   Interiors.SequenceEqual(other.Interiors ?? new BindingList<string>());
        }

    }
    public class ExpansionHudIndicatorColors
    {
        public string? StaminaBarColor { get; set; }
        public string? StaminaBarColorHalf { get; set; }
        public string? StaminaBarColorLow { get; set; }
        public string? NotifierDividerColor { get; set; }
        public string? TemperatureBurningColor { get; set; }
        public string? TemperatureHotColor { get; set; }
        public string? TemperatureIdealColor { get; set; }
        public string? TemperatureColdColor { get; set; }
        public string? TemperatureFreezingColor { get; set; }
        public string? NotifiersIdealColor { get; set; }
        public string? NotifiersHalfColor { get; set; }
        public string? NotifiersLowColor { get; set; }
        public string? ReputationBaseColor { get; set; }
        public string? ReputationMedColor { get; set; }
        public string? ReputationHighColor { get; set; }
        public ExpansionHudIndicatorColors() { }
        public override bool Equals(object obj)
        {
            if (obj is not ExpansionHudIndicatorColors other)
                return false;

            return StaminaBarColor == other.StaminaBarColor &&
                   StaminaBarColorHalf == other.StaminaBarColorHalf &&
                   StaminaBarColorLow == other.StaminaBarColorLow &&
                   NotifierDividerColor == other.NotifierDividerColor &&
                   TemperatureBurningColor == other.TemperatureBurningColor &&
                   TemperatureHotColor == other.TemperatureHotColor &&
                   TemperatureIdealColor == other.TemperatureIdealColor &&
                   TemperatureColdColor == other.TemperatureColdColor &&
                   TemperatureFreezingColor == other.TemperatureFreezingColor &&
                   NotifiersIdealColor == other.NotifiersIdealColor &&
                   NotifiersHalfColor == other.NotifiersHalfColor &&
                   NotifiersLowColor == other.NotifiersLowColor &&
                   ReputationBaseColor == other.ReputationBaseColor &&
                   ReputationMedColor == other.ReputationMedColor &&
                   ReputationHighColor == other.ReputationHighColor;
        }

    }
    public enum LampModeEnum
    {
        [Description("The streets lights are off")]
        Disabled = 0,
        [Description("Currently unused. Would require you to fix a generator to make street lights work. - CURRENTLY DOESNT WORK")]
        RequireGenerator = 1,
        [Description("Street lights are emitting lights but some of them will stay off intentionnaly")]
        AlwaysOn = 2,
        [Description("Force every lights to be turned on")]
        AlwaysOnEverywhere = 3
    };
    public enum buildingIvy
    {
        [Description("No custom ivies will be added to the map")]
        Disabled = 0,
        [Description("Custom Ivies in specific locations will be added to the map")]
        Specific_locations = 1,
        [Description("on all buildings on the map, not just predefined areas")]
        All_Buildings = 2,
    };
}
