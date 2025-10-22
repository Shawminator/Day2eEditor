using Day2eEditor;
using DayZeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionManager
    {
        private readonly Dictionary<string, string> _paths = new();

        public string basePath { get; set; }
        public string profilePath { get; set; }

        public bool HasErrors { get; set; }
        public List<string> Errors = new List<string>();

        public ExpansionAirdropConfig ExpansionAirdropConfig { get; set; }
        public ExpansionAIConfig ExpansionAIConfig { get; set; }
        public ExpansionBaseBuildingConfig ExpansionBaseBuildingConfig { get; set; }
        public ExpansionBookConfig ExpansionBookConfig { get; set; }
        public ExpansionChatConfig ExpansionChatConfig { get; set; }
        public ExpansionCoreConfig ExpansionCoreConfig { get; set; }
        public ExpansionDamageSystemConfig ExpansionDamageSystemConfig { get; set; }
        public ExpansionGarageConfig ExpansionGarageConfig { get; set; }
        //public ExpansionGeneralSettings GeneralSettings { get; set; }
        //public ExpansionHardlineSettings HardLineSettings { get; set; }
        //public ExpansionHardlinePlayerDataList ExpansionHardlinePlayerDataList { get; set; }
        //public ExpansionLogSettings LogSettings { get; set; }
        //public ExpansionMapSettings MapSettings { get; set; }
        //public MarketSettings marketsettings { get; set; }
        //public ExpansionMissionSettings MissionSettings { get; set; }
        //public ExpansionMonitoringSettings MonitoringSettings { get; set; }
        //public ExpansionNameTagsSettings NameTagSettings { get; set; }
        //public ExpansionNotificationSchedulerSettings NotificationSchedulerSettings { get; set; }
        //public ExpansionNotificationSettings NotificationSettings { get; set; }
        //public ExpansionPartySettings PartySettings { get; set; }
        //public ExpansionPersonalStorageList PersonalStorageList { get; set; }
        //public ExpansionPersonalStorageNewSettings PersonalStorageSettingsNew { get; set; }
        //public ExpansionPersonalStorageSettings PersonalStorageSettings { get; set; }
        //public ExpansionPlayerListSettings PlayerListSettings { get; set; }
        //public ExpansionRaidSettings RaidSettings { get; set; }
        //public ExpansionSafeZoneSettings SafeZoneSettings { get; set; }
        //public ExpansionSocialMediaSettings SocialMediaSettings { get; set; }
        //public ExpansionSpawnSettings SpawnSettings { get; set; }
        //public ExpansionTerritorySettings TerritorySettings { get; set; }
        //public ExpansionVehicleSettings VehicleSettings { get; set; }

        public ExpansionManager() { }
        public void SetExpansionStuff()
        {
            basePath = Path.Combine(AppServices.GetRequired<ProjectManager>().CurrentProject.ProjectRoot, "mpmissions", AppServices.GetRequired<ProjectManager>().CurrentProject.MpMissionPath);
            profilePath = Path.Combine(AppServices.GetRequired<ProjectManager>().CurrentProject.ProjectRoot, AppServices.GetRequired<ProjectManager>().CurrentProject.ProfileName);

            //profile files
            _paths["AirdropSettings"] = Path.Combine(profilePath, "ExpansionMod", "settings", "AirdropSettings.json");
            _paths["AISettings"] = Path.Combine(profilePath, "ExpansionMod", "settings", "AISettings.json");
            _paths["BookSettings"] = Path.Combine(profilePath, "Expansionmod", "settings", "BookSettings.json");
            _paths["ChatSettings"] = Path.Combine(profilePath, "Expansionmod", "settings", "ChatSettings.json");
            _paths["CoreSettings"] = Path.Combine(profilePath, "Expansionmod", "settings", "CoreSettings.json");
            _paths["DamageSystemSettings"] = Path.Combine(profilePath, "Expansionmod", "settings", "DamageSystemSettings.json");
            _paths["GarageSettings"] = Path.Combine(profilePath, "Expansionmod", "settings", "GarageSettings.json");

            // missions files
            _paths["BaseBuildingSettings"] = Path.Combine(basePath, "expansion", "settings", "BaseBuildingSettings.json");

            LoadFiles(basePath);
        }
        private void LoadFiles(string basePath)
        {
            Console.WriteLine($"\n[Load Expansion] Loading all files associated with the Expansion Mod.");

            ExpansionAirdropConfig = new ExpansionAirdropConfig(_paths["AirdropSettings"]);
            LoadConfigWithErrorReport("AirdropSettings", ExpansionAirdropConfig);

            ExpansionAIConfig = new ExpansionAIConfig(_paths["AISettings"]);
            LoadConfigWithErrorReport("AISettings", ExpansionAIConfig);

            ExpansionBaseBuildingConfig = new ExpansionBaseBuildingConfig(_paths["BaseBuildingSettings"]);
            LoadConfigWithErrorReport("BaseBuildingSettings", ExpansionBaseBuildingConfig);

            ExpansionBookConfig = new ExpansionBookConfig(_paths["BookSettings"]);
            LoadConfigWithErrorReport("BookSettings", ExpansionBookConfig);

            ExpansionChatConfig = new ExpansionChatConfig(_paths["ChatSettings"]);
            LoadConfigWithErrorReport("ChatSettings", ExpansionChatConfig);

            ExpansionCoreConfig = new ExpansionCoreConfig(_paths["CoreSettings"]);
            LoadConfigWithErrorReport("CoreSettings", ExpansionCoreConfig);

            ExpansionDamageSystemConfig = new ExpansionDamageSystemConfig(_paths["DamageSystemSettings"]);
            LoadConfigWithErrorReport("DamageSystemSettings", ExpansionDamageSystemConfig);

            ExpansionGarageConfig = new ExpansionGarageConfig(_paths["GarageSettings"]);
            LoadConfigWithErrorReport("GarageSettings", ExpansionGarageConfig);

            Save();
        }
        private void LoadConfigWithErrorReport(string name, IConfigLoader config)
        {
            if (config is IConfigLoader loader)
            {
                config.Load();
            }

            if (config.HasErrors)
            {
                HasErrors = true;
                Errors.AddRange(config.Errors.Select(e => $"[{name}] {e}"));
            }
        }

        public IEnumerable<string> Save()
        {
            var configs = new object[]
            {
                ExpansionAirdropConfig,
                ExpansionAIConfig,
                ExpansionBookConfig,
                ExpansionBaseBuildingConfig,
                ExpansionChatConfig,
                ExpansionCoreConfig,
                ExpansionDamageSystemConfig,
                ExpansionGarageConfig

            };

            var savedFiles = new List<string>();

            foreach (var obj in configs)
            {
                if (obj is IConfigLoader config)
                {
                    savedFiles.AddRange(config.Save());
                }
            }

            return savedFiles;
        }
        public bool needToSave()
        {
            bool needtosave = false;
            var configs = new object[]
            {
                ExpansionAirdropConfig,
                ExpansionAIConfig,
                ExpansionBookConfig,
                ExpansionBaseBuildingConfig,
                ExpansionChatConfig,
                ExpansionCoreConfig,
                ExpansionDamageSystemConfig,
                ExpansionGarageConfig
            };
            foreach (var obj in configs)
            {
                if (obj is not IConfigLoader config)
                    continue;
                if (config.needToSave())
                    needtosave = true;
            }
            return needtosave;
        }
    }
}
