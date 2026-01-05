using Day2eEditor;
using System.Reflection;


namespace ExpansionPlugin
{
    public class ExpansionManager
    {
        private readonly Dictionary<string, string> _paths = new();
        public string basePath { get; set; }
        public string profilePath { get; set; }
        public bool HasErrors { get; set; }
        public List<string> Errors = new List<string>();

        public ExpansionLoadoutConfig ExpansionLoadoutConfig { get; set; }
        public ExpansionLootDropConfig ExpansionLootDropConfig { get; set; }
        public ExpansionAirdropConfig ExpansionAirdropConfig { get; set; }
        public ExpansionAIConfig ExpansionAIConfig { get; set; }
        public ExpansionAILocationConfig ExpansionAILocationConfig { get; set; }
        public ExpansionAIPatrolConfig ExpansionAIPatrolConfig { get; set; }
        public ExpansionBaseBuildingConfig ExpansionBaseBuildingConfig { get; set; }
        public ExpansionBookConfig ExpansionBookConfig { get; set; }
        public ExpansionChatConfig ExpansionChatConfig { get; set; }
        public ExpansionCoreConfig ExpansionCoreConfig { get; set; }
        public ExpansionDamageSystemConfig ExpansionDamageSystemConfig { get; set; }
        public ExpansionGarageConfig ExpansionGarageConfig { get; set; }
        public ExpansionGeneralConfig ExpansionGeneralConfig { get; set; }
        public ExpansionHardlineConfig ExpansionHardlineConfig { get; set; }
        public ExpansionLogsConfig ExpansionLogsConfig { get; set; }
        public ExpansionMapConfig ExpansionMapConfig { get; set; }
        public ExpansionMarketSettingsConfig ExpansionMarketSettingsConfig { get; set; }
        public ExpansionMissionConfig ExpansionMissionConfig { get; set; }
        public ExpansionMonitoringConfig ExpansionMonitoringConfig { get; set; }
        public ExpansionNameTagsConfig ExpansionNameTagsConfig { get; set; }
        public ExpansionNotificationSchedulerConfig ExpansionNotificationSchedulerConfig { get; set; }
        public ExpansionNotificationConfig ExpansionNotificationConfig { get; set; }
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

            //Multiple files. not settings files,
            _paths["ExpansionLoadouts"] = Path.Combine(profilePath, "ExpansionMod", "Loadouts");
            _paths["ExpansionLootDrops"] = Path.Combine(profilePath, "ExpansionMod", "AI", "LootDrops");

            //Settings files in profiles
            _paths["AirdropSettings"] = Path.Combine(profilePath, "ExpansionMod", "settings", "AirdropSettings.json");
            _paths["AISettings"] = Path.Combine(profilePath, "ExpansionMod", "settings", "AISettings.json");
            _paths["BookSettings"] = Path.Combine(profilePath, "Expansionmod", "settings", "BookSettings.json");
            _paths["ChatSettings"] = Path.Combine(profilePath, "Expansionmod", "settings", "ChatSettings.json");
            _paths["CoreSettings"] = Path.Combine(profilePath, "Expansionmod", "settings", "CoreSettings.json");
            _paths["DamageSystemSettings"] = Path.Combine(profilePath, "Expansionmod", "settings", "DamageSystemSettings.json");
            _paths["GarageSettings"] = Path.Combine(profilePath, "Expansionmod", "settings", "GarageSettings.json");
            _paths["GeneralSettings"] = Path.Combine(profilePath, "Expansionmod", "settings", "GeneralSettings.json");
            _paths["LogsSettings"] = Path.Combine(profilePath, "Expansionmod", "settings", "LogsSettings.json");
            _paths["MissionSettings"] = Path.Combine(profilePath, "Expansionmod", "settings", "MissionSettings.json");
            _paths["MonitoringSettings"] = Path.Combine(profilePath, "Expansionmod", "settings", "MonitoringSettings.json");
            _paths["NameTagsSettings"] = Path.Combine(profilePath, "Expansionmod", "settings", "NameTagsSettings.json");
            _paths["NotificationSchedulerSettings"] = Path.Combine(profilePath, "Expansionmod", "settings", "NotificationSchedulerSettings.json");
            _paths["NotificationSettings"] = Path.Combine(profilePath, "Expansionmod", "settings", "NotificationSettings.json");

            // settings files in missions files
            _paths["AILocationSettings"] = Path.Combine(basePath, "expansion", "settings", "AILocationSettings.json");
            _paths["AIPatrolSettings"] = Path.Combine(basePath, "expansion", "settings", "AIPatrolSettings.json");
            _paths["BaseBuildingSettings"] = Path.Combine(basePath, "expansion", "settings", "BaseBuildingSettings.json");
            _paths["HardlineSettings"] = Path.Combine(basePath, "expansion", "settings", "HardlineSettings.json");
            _paths["MapSettings"] = Path.Combine(basePath, "expansion", "settings", "MapSettings.json");
            _paths["MarketSettings"] = Path.Combine(basePath, "expansion", "settings", "MarketSettings.json");

            LoadFiles(basePath);
        }
        private void LoadFiles(string basePath)
        {
            Console.WriteLine($"\n[Expansion Manager] Loading all files associated with the Expansion Mod.");

            ExpansionLoadoutConfig = new ExpansionLoadoutConfig(_paths["ExpansionLoadouts"]);
            LoadConfigWithErrorReport("ExpansionLoadouts", ExpansionLoadoutConfig);

            ExpansionLootDropConfig = new ExpansionLootDropConfig(_paths["ExpansionLootDrops"]);
            LoadConfigWithErrorReport("ExpansionLootDrops", ExpansionLootDropConfig);

            ExpansionAirdropConfig = new ExpansionAirdropConfig(_paths["AirdropSettings"]);
            LoadConfigWithErrorReport("AirdropSettings", ExpansionAirdropConfig);

            ExpansionAILocationConfig = new ExpansionAILocationConfig(_paths["AILocationSettings"]);
            LoadConfigWithErrorReport("AILocationSettings", ExpansionAILocationConfig);

            ExpansionAIConfig = new ExpansionAIConfig(_paths["AISettings"]);
            LoadConfigWithErrorReport("AISettings", ExpansionAIConfig);

            ExpansionAIPatrolConfig = new ExpansionAIPatrolConfig(_paths["AIPatrolSettings"]);
            LoadConfigWithErrorReport("AIPatrolSettings", ExpansionAIPatrolConfig);

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

            ExpansionGeneralConfig = new ExpansionGeneralConfig(_paths["GeneralSettings"]);
            LoadConfigWithErrorReport("GeneralSettings", ExpansionGeneralConfig);

            ExpansionHardlineConfig = new ExpansionHardlineConfig(_paths["HardlineSettings"]);
            LoadConfigWithErrorReport("HardlineSettings", ExpansionHardlineConfig);

            ExpansionLogsConfig = new ExpansionLogsConfig(_paths["LogsSettings"]);
            LoadConfigWithErrorReport("LogsSettings", ExpansionLogsConfig);

            ExpansionMapConfig = new ExpansionMapConfig(_paths["MapSettings"]);
            LoadConfigWithErrorReport("MapSettings", ExpansionMapConfig);

            ExpansionMarketSettingsConfig = new ExpansionMarketSettingsConfig(_paths["MarketSettings"]);
            LoadConfigWithErrorReport("MarketSettings", ExpansionMarketSettingsConfig);

            ExpansionMissionConfig = new ExpansionMissionConfig(_paths["MissionSettings"]);
            LoadConfigWithErrorReport("MissionSettings", ExpansionMissionConfig);

            ExpansionMonitoringConfig = new ExpansionMonitoringConfig(_paths["MonitoringSettings"]);
            LoadConfigWithErrorReport("MonitoringSettings", ExpansionMonitoringConfig);

            ExpansionNameTagsConfig = new ExpansionNameTagsConfig(_paths["NameTagsSettings"]);
            LoadConfigWithErrorReport("NameTagsSettings", ExpansionNameTagsConfig);

            ExpansionNotificationSchedulerConfig = new ExpansionNotificationSchedulerConfig(_paths["NotificationSchedulerSettings"]);
            LoadConfigWithErrorReport("NotificationSchedulerSettings", ExpansionNotificationSchedulerConfig);

            ExpansionNotificationConfig = new ExpansionNotificationConfig(_paths["NotificationSettings"]);
            LoadConfigWithErrorReport("NotificationSettings", ExpansionNotificationConfig);

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
                ExpansionLoadoutConfig,
                ExpansionLootDropConfig,
                ExpansionAirdropConfig,
                ExpansionAILocationConfig,
                ExpansionAIConfig,
                ExpansionAIPatrolConfig,
                ExpansionBookConfig,
                ExpansionBaseBuildingConfig,
                ExpansionChatConfig,
                ExpansionCoreConfig,
                ExpansionDamageSystemConfig,
                ExpansionGarageConfig,
                ExpansionGeneralConfig,
                ExpansionHardlineConfig,
                ExpansionLogsConfig,
                ExpansionMapConfig,
                ExpansionMarketSettingsConfig,
                ExpansionMissionConfig,
                ExpansionMonitoringConfig,
                ExpansionNameTagsConfig,
                ExpansionNotificationSchedulerConfig,
                ExpansionNotificationConfig

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
                ExpansionLoadoutConfig,
                ExpansionLootDropConfig,
                ExpansionAirdropConfig,
                ExpansionAILocationConfig,
                ExpansionAIConfig,
                ExpansionAIPatrolConfig,
                ExpansionBookConfig,
                ExpansionBaseBuildingConfig,
                ExpansionChatConfig,
                ExpansionCoreConfig,
                ExpansionDamageSystemConfig,
                ExpansionGarageConfig,
                ExpansionGeneralConfig,
                ExpansionHardlineConfig,
                ExpansionLogsConfig,
                ExpansionMapConfig,
                ExpansionMarketSettingsConfig,
                ExpansionMissionConfig,
                ExpansionMonitoringConfig,
                ExpansionNameTagsConfig,
                ExpansionNotificationSchedulerConfig,
                ExpansionNotificationConfig
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

        internal void SetExternalFiles()
        {
            checkExpansionSlotNames();
            CheckExpansionFactions();
            checkExpansionIcons();
        }
        private void CheckExpansionFactions()
        {
            string filePath = "Data\\ExpansionFactions.txt";

            // Ensure the directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            List<string> fileExpansionFactions = new List<string>();

            if (File.Exists(filePath))
            {
                fileExpansionFactions = File.ReadAllLines(filePath).ToList();
            }

            // Add any missing entries from the static list
            bool updated = false;
            foreach (string slot in ExpansionFactions)
            {
                if (!fileExpansionFactions.Contains(slot))
                {
                    fileExpansionFactions.Add(slot);
                    updated = true;
                }
            }

            // If there were updates, write back to the file
            if (updated || !File.Exists(filePath))
            {
                File.WriteAllLines(filePath, fileExpansionFactions);
            }
        }
        private void checkExpansionSlotNames()
        {
            string filePath = "Data\\ExpansionSlotnames.txt";

            // Ensure the directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            List<string> fileSlotNames = new List<string>();

            if (File.Exists(filePath))
            {
                fileSlotNames = File.ReadAllLines(filePath).ToList();
            }

            // Add any missing entries from the static list
            bool updated = false;
            foreach (string slot in ExpansionSlotnames)
            {
                if (!fileSlotNames.Contains(slot))
                {
                    fileSlotNames.Add(slot);
                    updated = true;
                }
            }

            // If there were updates, write back to the file
            if (updated || !File.Exists(filePath))
            {
                File.WriteAllLines(filePath, fileSlotNames);
            }

        }
        private void checkExpansionIcons()
        {
            string filePath = "Data\\ExpansionIconnames.txt";

            // Ensure the directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            List<string> fileIconNames = new List<string>();

            if (File.Exists(filePath))
            {
                fileIconNames = File.ReadAllLines(filePath).ToList();
            }

            // Add any missing entries from the static list
            bool updated = false;
            foreach (string slot in ExpansionIcons)
            {
                if (!fileIconNames.Contains(slot))
                {
                    fileIconNames.Add(slot);
                    updated = true;
                }
            }

            // If there were updates, write back to the file
            if (updated || !File.Exists(filePath))
            {
                File.WriteAllLines(filePath, fileIconNames);
            }
        }
        static List<string> ExpansionFactions = new List<string>()
        {
            "Brawlers",
            "Civilian",
            "East",
            "Guards",
            "InvincibleGuards",
            "InvincibleObservers",
            "InvincibleYeetBrigade",
            "Mercenaries",
            "Observers",
            "Passive",
            "Raiders",
            "RANDOM",
            "Shamans",
            "West",
            "YeetBrigade"
        };
        static List<string> ExpansionSlotnames = new List<string>()
        {
            "Default Slot",
            "Back",
            "Body",
            "Dogtag",
            "Eyes",
            "Feet",
            "Gloves",
            "Hands",
            "Headgear",
            "Hips",
            "Legs",
            "Mask",
            "Melee",
            "Shoulder",
            "Vest"
        };
        public static List<string> ExpansionIcons = new List<string>()
        {
            "Ambush",
            "Animal Skull",
            "Apple",
            "Apple Core",
            "Arrows",
            "Axe",
            "Arrow",
            "Airdrop",
            "Anomaly",
            "Backpack",
            "Bandage",
            "Bandit",
            "Batteries",
            "Berries",
            "Binoculars",
            "Bolt",
            "Bonfire",
            "Bottle",
            "Bow",
            "Base",
            "Boat",
            "Book 1",
            "Book 2",
            "Broken Lighter",
            "Bear",
            "Car",
            "Car 2",
            "Craft",
            "Can Of Beans Big",
            "Can Of Beans Small",
            "Car Keys",
            "Carrot",
            "Chain Saw",
            "Chicken",
            "Chocolate",
            "Cigarets",
            "Circuit Board",
            "Cloth",
            "Compass",
            "Corn",
            "Crowbar",
            "Cow",
            "Claw",
            "Coins",
            "Coins 2",
            "Codelock",
            "Deliver",
            "Dinosaur Skull",
            "Dry Wood",
            "Drip",
            "Ear",
            "Eye",
            "Error",
            "Exclamationmark",
            "Ellipse",
            "Eatable Flowers",
            "Electrical Tape",
            "Empty Can",
            "Fishing",
            "Fireplace",
            "Fish",
            "Flare",
            "Flare Gun",
            "Flare Gun Ammo",
            "Flashlight",
            "Fox",
            "Frying Pan",
            "Grab",
            "Group",
            "Gas",
            "Gas Mask",
            "Golf Club",
            "Goose",
            "Grenade",
            "Guitar",
            "Gun",
            "Gun Bullets",
            "Heart",
            "Hook",
            "Helicopter",
            "Hammer",
            "Herbal Medicine",
            "Home Made Grenade",
            "Human Skull",
            "Info",
            "Infected 1",
            "Infected 2",
            "Insect",
            "Knife",
            "Kitchen Knife",
            "Kitchen Knife Big",
            "Ladder",
            "Lantern",
            "Lighter",
            "Menu",
            "Marker",
            "Map",
            "Map Marker",
            "Moon",
            "Machette",
            "Matches",
            "Medic Box",
            "Mushrooms",
            "Note",
            "Nails",
            "Open Hand",
            "Orientation",
            "Options",
            "Pen",
            "Persona",
            "Pill",
            "Paper Map",
            "Paper",
            "Pills",
            "Pipe Wrench",
            "Powder",
            "Pumpkin",
            "Questionmark",
            "Questionmark 2",
            "Radiation",
            "Radio",
            "Rabbit",
            "Racoon",
            "Rat",
            "Rock 1",
            "Rock 2",
            "Rope",
            "Skull 1",
            "Skull 2",
            "Skull 3",
            "Star",
            "Sun",
            "Snow",
            "Square",
            "Shield",
            "Saw",
            "Scrap Metal",
            "Screwdriver",
            "Shotgun",
            "Shotgun Bullets",
            "Shovel",
            "Soda",
            "Soldier",
            "Status",
            "Territory",
            "Trader",
            "Tent",
            "Thermometer",
            "Vehicle Crash",
            "Water 1",
            "Water 2",
            "Walkie Talkie",
            "Water Jug",
            "Wild Pork",
            "Worms",
            "Three Stick",
            "Sleeping Bag",
            "Slingshot",
            "Molotov",
            "Lizard",
            "Cooked Meat 1",
            "Cooked Meat 2",
            "Cooked Meat 3",
            "Collection Of Bolts",
            "Collection Of Sticks 1",
            "Collection Of Sticks 2",
            "Collection Of Sticks 3",
            "Collection Of Sticks 4",
            "Discord",
            "Reddit",
            "Steam",
            "GitHub",
            "Homepage",
            "Forums",
            "Twitter",
            "YouTube",
            "Patreon",
            "Guilded",
            "PayPal",
            "Cross",
            "Battery Full",
            "Battery Med",
            "Battery Low",
            "Battery Empty",
            "Clock Blank",
            "Expansion",
            "Expansion B/W"
        };

    }
    
    public static class ResourceHelper
    {
        public static Stream? OpenEmbeddedStream(string resourceName)
        {
            // Typically: "{DefaultNamespace}.{Folder}.{FileName}"
            var asm = Assembly.GetExecutingAssembly();
            return asm.GetManifestResourceStream(resourceName);
        }

        public static string[] ListAllEmbeddedResourceNames()
        {
            var asm = Assembly.GetExecutingAssembly();
            return asm.GetManifestResourceNames();
        }
        public static Bitmap MultiplyColorToBitmap(Bitmap sourceBitmap, Color color, int divisor, bool preserveAlfa)
        {
            if (sourceBitmap == null) return (null);
            int nPixels = sourceBitmap.Width * sourceBitmap.Height;
            Rectangle rect = new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height);

            int[] m_RawData = new int[nPixels];


            System.Drawing.Imaging.BitmapData bmpData = sourceBitmap.LockBits(rect,
                System.Drawing.Imaging.ImageLockMode.ReadWrite,
                sourceBitmap.PixelFormat);
            IntPtr ptrData = bmpData.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(ptrData, m_RawData, 0, nPixels);
            sourceBitmap.UnlockBits(bmpData);

            //int maxr = 0;
            //int maxg = 0;
            //int maxb = 0;
            for (int k = 0; k < nPixels; k++)
            {

                Color mult = Color.FromArgb(m_RawData[k]);
                int a = mult.A;
                int r = (color.R * mult.R) / divisor;
                int g = (color.G * mult.G) / divisor;
                int b = (color.B * mult.B) / divisor;
                if (r > 255)
                    r = 255;
                if (g > 255)
                    g = 255;
                if (b > 255)
                    b = 255;
                // int a = (color.A * alfa) / 255;
                if (!preserveAlfa)
                {
                    a = 255;
                }
                m_RawData[k] = (int)((((((a << 8) | r) << 8) | g) << 8) | b);
            }

            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            rect = new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height);
            System.Drawing.Imaging.BitmapData bmpDest = resultBitmap.LockBits(rect,
                System.Drawing.Imaging.ImageLockMode.WriteOnly,
                resultBitmap.PixelFormat);
            // Get the address of the first line.
            IntPtr ptrDest = bmpDest.Scan0;

            // This code is specific to a bitmap with 32 bits per pixels.
            System.Runtime.InteropServices.Marshal.Copy(m_RawData, 0, ptrDest, nPixels);
            resultBitmap.UnlockBits(bmpDest);
            return (resultBitmap);
        }
    }

}
