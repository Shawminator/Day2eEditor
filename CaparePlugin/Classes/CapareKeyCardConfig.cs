using Day2eEditor;
using System.ComponentModel;

namespace CaparePlugin
{
    /// <summary>
    /// $profile:Capare_Shawminator/Capare_KeyCard_System/Capare_KeyCard_System.json
    /// Capare_Scripts_Shawminator - KeyCardConfig.c / KeyCardlocations.c / KeyCardLootLocation.c
    /// </summary>
    public class CapareKeyCardConfig : CapareConfigBase<KeycardsSettings>
    {
        public CapareKeyCardConfig(string path) : base(path)
        {
        }
        protected override KeycardsSettings CreateDefaultData()
        {
            return KeycardsSettings.CreateDefault();
        }
        protected override IEnumerable<string> ValidateData()
        {
            return Data.FixMissingOrInvalidFields();
        }
    }

    public class KeycardsSettings : IEquatable<KeycardsSettings>, IDeepCloneable<KeycardsSettings>
    {
        public bool LockedDoorsEnabled { get; set; }
        public bool DebugModeEnabled { get; set; }
        public bool AdminLoggingEnabled { get; set; }
        public BindingList<string> AdminSteamIDs { get; set; }
        public BindingList<string> KeycardClassNames { get; set; }
        public BindingList<KeyCardAreas> KeyCard_Areas { get; set; }
        public BindingList<CapareBuildingScanTarget> ScanTargets { get; set; }

        public KeycardsSettings()
        {
        }

        // Same values KeycardsConfig.DefaultConfig() writes in the mod
        public static KeycardsSettings CreateDefault()
        {
            var settings = new KeycardsSettings()
            {
                LockedDoorsEnabled = true,
                DebugModeEnabled = true,
                AdminLoggingEnabled = false,
                AdminSteamIDs = new BindingList<string>() { "76561198019833908" },
                KeycardClassNames = new BindingList<string>()
                {
                    "Capare_KeyCard_Laggan_Dam",
                    "Capare_KeyCard_Blackrock_research",
                    "Capare_KeyCard_NewBilibadPrison_research",
                    "Capare_KeyCard_MountainFacility",
                    "Capare_KeyCard_Kingswood_Bunker_Research",
                    "Capare_Keycard_BlackMarket",
                    "Capare_Keycard_Industrial",
                    "Capare_Keycard_Medical",
                    "Capare_Keycard_Military",
                    "Capare_Keycard_Police"
                },
                KeyCard_Areas = new BindingList<KeyCardAreas>(),
                ScanTargets = new BindingList<CapareBuildingScanTarget>()
            };

            settings.KeyCard_Areas.Add(KeyCardAreas.Create("BlackMarket", 0,
                new KeyCardLocations(5684.734375f, 31.882811f, 20247.591797f, 0, 1200, "Land_T2Door", "Capare_Keycard_BlackMarket", false, "Capare_Military_Alarm", true, true, true)));
            settings.KeyCard_Areas.Add(KeyCardAreas.Create("NewBilibadPrison", 0,
                new KeyCardLocations(10847.809570f, 36.173111f, 19317.371094f, 0, 1200, "Land_T1Door", "Capare_KeyCard_NewBilibadPrison_research", true, "Capare_Military_Alarm", true, false, true)));
            settings.KeyCard_Areas.Add(KeyCardAreas.Create("BlackRock", 0,
                new KeyCardLocations(2012.419556f, 48.463005f, 10555.734375f, 8, 900, "Land_Darkvalley_x18lab_Factory", "Capare_KeyCard_Blackrock_research", true, "Capare_Military_Alarm", true, false, true)));
            settings.KeyCard_Areas.Add(KeyCardAreas.Create("MountainFacility", 0,
                new KeyCardLocations(10060.619141f, 201.251160f, 15020.927734f, 0, 900, "Land_T2DoorDam", "Capare_KeyCard_MountainFacility", true, "Capare_Military_Alarm", true, false, true)));
            settings.KeyCard_Areas.Add(KeyCardAreas.Create("EscapeTunnel1", 1,
                new KeyCardLocations(6280.034668f, 67.025108f, 16639.330078f, 0, 60, "Land_Escape_tunnel_2_LA", "Capare_KeyCard_Kingswood_Bunker_Research", true, "Capare_Military_Alarm", true, false, false)));

            var lagganDam = new KeyCardLocations(14088.511719f, 106.298920f, 6251.01660f, 0, 300, "Land_T2DoorDam", "Capare_KeyCard_Laggan_Dam", true, "Capare_Military_Alarm", true, false, true);
            foreach (Vec3 v in new[]
            {
                new Vec3(14083.185546875f, 106.40548706054688f, 6259.08203125f),
                new Vec3(14104.3505859375f, 106.4054946899414f, 6258.12060546875f),
                new Vec3(14109.556640625f, 106.40550231933594f, 6266.37255859375f),
                new Vec3(14102.1025390625f, 109.85588073730469f, 6258.23779296875f),
                new Vec3(14084.8125f, 109.85588073730469f, 6259.01513671875f),
                new Vec3(14101.6748046875f, 109.85588073730469f, 6264.04638671875f),
                new Vec3(14115.3232421875f, 106.63394165039063f, 6261.74951171875f),
                new Vec3(14092.080078125f, 106.4054946899414f, 6270.693359375f)
            })
            {
                lagganDam.InfectedPositions.Add(new InfectedPosition(v));
            }
            lagganDam.InfectedClassnames.Add("ZmbM_usSoldier_normal_Woodland");
            lagganDam.InfectedClassnames.Add("ZmbM_usSoldier_Heavy_Woodland");
            lagganDam.InfectedClassnames.Add("ZmbM_usSoldier_normal_Desert");
            settings.KeyCard_Areas.Add(KeyCardAreas.Create("LagganDam", 0, lagganDam));

            settings.ScanTargets.Add(new CapareBuildingScanTarget("Land_Village_PoliceStation", 7, "Police", "Capare_Keycard_Police", "Capare_Police_Alarm", false, true, 0));
            settings.ScanTargets.Add(new CapareBuildingScanTarget("Land_Village_PoliceStation_Enoch", 7, "Police", "Capare_Keycard_Police", "Capare_Police_Alarm", false, true, 0));
            settings.ScanTargets.Add(new CapareBuildingScanTarget("Land_Village_PoliceStation_East", 7, "Police", "Capare_Keycard_Police", "Capare_Police_Alarm", false, true, 0));
            settings.ScanTargets.Add(new CapareBuildingScanTarget("Land_City_PoliceStation", 9, "Police", "Capare_Keycard_Police", "Capare_Police_Alarm", false, true, 0));
            settings.ScanTargets.Add(new CapareBuildingScanTarget("Land_City_PoliceStation_Enoch", 9, "Police", "Capare_Keycard_Police", "Capare_Police_Alarm", false, true, 0));

            return settings;
        }

        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();
            if (AdminSteamIDs == null)
            {
                AdminSteamIDs = new BindingList<string>();
                fixes.Add("Initialized AdminSteamIDs");
            }
            if (KeycardClassNames == null)
            {
                KeycardClassNames = new BindingList<string>();
                fixes.Add("Initialized KeycardClassNames");
            }
            if (KeyCard_Areas == null)
            {
                KeyCard_Areas = new BindingList<KeyCardAreas>();
                fixes.Add("Initialized KeyCard_Areas");
            }
            if (ScanTargets == null)
            {
                ScanTargets = new BindingList<CapareBuildingScanTarget>();
                fixes.Add("Initialized ScanTargets");
            }
            foreach (KeyCardAreas area in KeyCard_Areas)
            {
                if (area.KeyCard_Locations == null)
                {
                    area.KeyCard_Locations = new BindingList<KeyCardLocations>();
                    fixes.Add($"Initialized KeyCard_Locations for {area.AreaName}");
                }
                if (area.RewardTableNames == null)
                {
                    area.RewardTableNames = new BindingList<string>();
                    fixes.Add($"Initialized RewardTableNames for {area.AreaName}");
                }
                foreach (KeyCardLocations location in area.KeyCard_Locations)
                {
                    if (location.RewardTableNames == null)
                    {
                        location.RewardTableNames = new BindingList<string>();
                        fixes.Add($"Initialized RewardTableNames for {area.AreaName} location {location.BuildingClassName}");
                    }
                    if (location.LootSpawnPoints == null)
                    {
                        location.LootSpawnPoints = new BindingList<KeyCardLootLocation>();
                        fixes.Add($"Initialized LootSpawnPoints for {area.AreaName} location {location.BuildingClassName}");
                    }
                    if (location.InfectedPositions == null)
                    {
                        location.InfectedPositions = new BindingList<InfectedPosition>();
                        fixes.Add($"Initialized InfectedPositions for {area.AreaName} location {location.BuildingClassName}");
                    }
                    if (location.InfectedClassnames == null)
                    {
                        location.InfectedClassnames = new BindingList<string>();
                        fixes.Add($"Initialized InfectedClassnames for {area.AreaName} location {location.BuildingClassName}");
                    }
                    foreach (KeyCardLootLocation loot in location.LootSpawnPoints)
                    {
                        loot.Position ??= new Vec3(0f, 0f, 0f);
                        loot.Orientation ??= new Vec3(0f, 0f, 0f);
                    }
                    foreach (InfectedPosition infected in location.InfectedPositions)
                    {
                        infected.Position ??= new Vec3(0f, 0f, 0f);
                    }
                }
            }
            return fixes;
        }

        public bool Equals(KeycardsSettings other) => CapareJson.AreEqual(this, other);
        public override bool Equals(object? obj) => Equals(obj as KeycardsSettings);
        public override int GetHashCode() => base.GetHashCode();
        public KeycardsSettings Clone() => CapareJson.Clone(this);
    }
    public class CapareBuildingScanTarget
    {
        public string BuildingClassName { get; set; }
        public int DoorIndex { get; set; }
        public string AreaName { get; set; }
        public string KeycardClassName { get; set; }
        public string AlarmSound { get; set; }
        public bool AlarmMapwide { get; set; }
        public bool SingleUse { get; set; }
        public int AutomaticCloseTimer { get; set; }

        public CapareBuildingScanTarget() { }
        public CapareBuildingScanTarget(string buildingClassName, int doorIndex, string areaName, string keycardClassName, string alarmSound, bool alarmMapwide, bool singleUse, int automaticCloseTimer)
        {
            BuildingClassName = buildingClassName;
            DoorIndex = doorIndex;
            AreaName = areaName;
            KeycardClassName = keycardClassName;
            AlarmSound = alarmSound;
            AlarmMapwide = alarmMapwide;
            SingleUse = singleUse;
            AutomaticCloseTimer = automaticCloseTimer;
        }
        public override string ToString() => $"{BuildingClassName} (Door {DoorIndex})";
    }
    public class KeyCardAreas
    {
        public string AreaName { get; set; }
        public int MaxInitialLootSpawns { get; set; }
        public BindingList<KeyCardLocations> KeyCard_Locations { get; set; }
        public BindingList<string> RewardTableNames { get; set; }

        public KeyCardAreas()
        {
        }
        public static KeyCardAreas Create(string areaName, int maxInitialLootSpawns, params KeyCardLocations[] locations)
        {
            return new KeyCardAreas()
            {
                AreaName = areaName,
                MaxInitialLootSpawns = maxInitialLootSpawns,
                KeyCard_Locations = new BindingList<KeyCardLocations>(locations.ToList()),
                RewardTableNames = new BindingList<string>()
            };
        }
        public override string ToString() => AreaName;
    }
    public class KeyCardLocations
    {
        public float building_x { get; set; }
        public float building_y { get; set; }
        public float building_z { get; set; }
        public int door_index { get; set; }
        public int AutomaticCloseTimer { get; set; }
        public string BuildingClassName { get; set; }
        public string KeycardClassName { get; set; }
        public string AlarmSound { get; set; }
        public bool UseAlarm { get; set; }
        public bool AlarmMapwide { get; set; }
        public bool SingleUse { get; set; }
        public bool CloseWarning { get; set; }
        public bool SpawnLootOnInit { get; set; }
        public int NumberOfLootPoints { get; set; }
        public bool SpawnSpecificLoot { get; set; }
        public BindingList<string> RewardTableNames { get; set; }
        public BindingList<KeyCardLootLocation> LootSpawnPoints { get; set; }
        public bool SpawnInfected { get; set; }
        public BindingList<InfectedPosition> InfectedPositions { get; set; }
        public BindingList<string> InfectedClassnames { get; set; }

        public KeyCardLocations() { }
        public KeyCardLocations(float x, float y, float z, int doorIndex, int automaticCloseTimer, string buildingClassName, string keycardClassName,
            bool singleUse, string alarmSound, bool useAlarm, bool alarmMapwide, bool closeWarning)
        {
            building_x = x;
            building_y = y;
            building_z = z;
            door_index = doorIndex;
            AutomaticCloseTimer = automaticCloseTimer;
            BuildingClassName = buildingClassName;
            KeycardClassName = keycardClassName;
            SingleUse = singleUse;
            AlarmSound = alarmSound;
            UseAlarm = useAlarm;
            AlarmMapwide = alarmMapwide;
            CloseWarning = closeWarning;
            SpawnLootOnInit = false;
            NumberOfLootPoints = 0;
            SpawnSpecificLoot = false;
            RewardTableNames = new BindingList<string>();
            LootSpawnPoints = new BindingList<KeyCardLootLocation>();
            SpawnInfected = false;
            InfectedPositions = new BindingList<InfectedPosition>();
            InfectedClassnames = new BindingList<string>();
        }
        public override string ToString() => $"{BuildingClassName} (Door {door_index})";
    }
    public class KeyCardLootLocation
    {
        public Vec3 Position { get; set; }
        public Vec3 Orientation { get; set; }

        public KeyCardLootLocation() { }
        public KeyCardLootLocation(Vec3 position, Vec3 orientation)
        {
            Position = position;
            Orientation = orientation;
        }
        public override string ToString() => Position.GetString();
    }
    public class InfectedPosition
    {
        public Vec3 Position { get; set; }

        public InfectedPosition() { }
        public InfectedPosition(Vec3 position)
        {
            Position = position;
        }
        public override string ToString() => Position.GetString();
    }
}
