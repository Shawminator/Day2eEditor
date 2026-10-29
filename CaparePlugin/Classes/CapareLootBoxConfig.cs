using Day2eEditor;
using System.ComponentModel;

namespace CaparePlugin
{
    /// <summary>
    /// $profile:Capare_Shawminator/Capare_LootBox/Capare_LootBox.json
    /// Capare_Scripts_Shawminator - CapareLootBoxConfig.c / CapareLootBoxCore.c
    /// </summary>
    public class CapareLootBoxConfig : CapareConfigBase<CapareLootBoxSettingsData>
    {
        public CapareLootBoxConfig(string path) : base(path)
        {
        }
        protected override CapareLootBoxSettingsData CreateDefaultData()
        {
            return CapareLootBoxSettingsData.CreateDefault();
        }
        protected override IEnumerable<string> ValidateData()
        {
            return Data.FixMissingOrInvalidFields();
        }
    }

    public class CapareLootBoxSettingsData : IEquatable<CapareLootBoxSettingsData>, IDeepCloneable<CapareLootBoxSettingsData>
    {
        public int LootBoxLifetime { get; set; }
        public BindingList<CapareLootBoxSettings> CapareLootBoxConfigs { get; set; }
        public bool StaticLootBoxEnabled { get; set; }
        public BindingList<CapareLootBoxStaticBox> CapareLootBoxStaticBoxes { get; set; }

        public CapareLootBoxSettingsData()
        {
        }

        // Same values CapareLootBoxDefaultConfig() writes in the mod
        public static CapareLootBoxSettingsData CreateDefault()
        {
            var data = new CapareLootBoxSettingsData()
            {
                LootBoxLifetime = 3600,
                CapareLootBoxConfigs = new BindingList<CapareLootBoxSettings>(),
                StaticLootBoxEnabled = true,
                CapareLootBoxStaticBoxes = new BindingList<CapareLootBoxStaticBox>()
            };
            data.CapareLootBoxConfigs.Add(new CapareLootBoxSettings("CapareLootBox_Ammo", "Reward_Table_1", "Reward_Table_2"));
            data.CapareLootBoxConfigs.Add(new CapareLootBoxSettings("CapareLootBox_BlackCamo", "Reward_Table_MiliCrate01"));
            data.CapareLootBoxConfigs.Add(new CapareLootBoxSettings("CapareLootBox_BlackDigital", "Reward_Table_MiliCrate01"));
            data.CapareLootBoxConfigs.Add(new CapareLootBoxSettings("CapareLootBox_Blue", "Reward_Table_PoliceCrate01"));
            foreach (string box in new[] { "CapareLootBox_Clothes", "CapareLootBox_Green", "CapareLootBox_Medical", "CapareLootBox_Misc", "CapareLootBox_Orange",
                "CapareLootBox_Red", "CapareLootBox_RedCamo", "CapareLootBox_Turquoise", "CapareLootBox_Yellow", "CapareLootBox_WeaponCase" })
            {
                data.CapareLootBoxConfigs.Add(new CapareLootBoxSettings(box, "Reward_Table_1", "Reward_Table_2"));
            }
            data.CapareLootBoxStaticBoxes.Add(new CapareLootBoxStaticBox()
            {
                LootBoxSetName = "Test",
                PossibleLootBoxNames = new BindingList<string>() { "Capare_LootBox_Ammo" },
                NumberOfPositions = 2,
                StaticBoxPositions = new BindingList<CapareStaticBoxPosRot>()
                {
                    new CapareStaticBoxPosRot(new Vec3(0f, 0f, 0f), new Vec3(0f, 0f, 0f)),
                    new CapareStaticBoxPosRot(new Vec3(0f, 0f, 0f), new Vec3(0f, 0f, 0f))
                },
                UseCustomLootList = false,
                CustomLootList = new BindingList<string>() { "Reward_Table_1", "Reward_Table_2" }
            });
            return data;
        }

        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();
            if (CapareLootBoxConfigs == null)
            {
                CapareLootBoxConfigs = new BindingList<CapareLootBoxSettings>();
                fixes.Add("Initialized CapareLootBoxConfigs");
            }
            if (CapareLootBoxStaticBoxes == null)
            {
                CapareLootBoxStaticBoxes = new BindingList<CapareLootBoxStaticBox>();
                fixes.Add("Initialized CapareLootBoxStaticBoxes");
            }
            foreach (CapareLootBoxSettings box in CapareLootBoxConfigs)
            {
                if (box.PossibleLootList == null)
                {
                    box.PossibleLootList = new BindingList<string>();
                    fixes.Add($"Initialized PossibleLootList for {box.LootBoxName}");
                }
            }
            foreach (CapareLootBoxStaticBox staticBox in CapareLootBoxStaticBoxes)
            {
                if (staticBox.PossibleLootBoxNames == null)
                {
                    staticBox.PossibleLootBoxNames = new BindingList<string>();
                    fixes.Add($"Initialized PossibleLootBoxNames for {staticBox.LootBoxSetName}");
                }
                if (staticBox.StaticBoxPositions == null)
                {
                    staticBox.StaticBoxPositions = new BindingList<CapareStaticBoxPosRot>();
                    fixes.Add($"Initialized StaticBoxPositions for {staticBox.LootBoxSetName}");
                }
                if (staticBox.CustomLootList == null)
                {
                    staticBox.CustomLootList = new BindingList<string>();
                    fixes.Add($"Initialized CustomLootList for {staticBox.LootBoxSetName}");
                }
                foreach (CapareStaticBoxPosRot pos in staticBox.StaticBoxPositions)
                {
                    pos.Position ??= new Vec3(0f, 0f, 0f);
                    pos.Rotation ??= new Vec3(0f, 0f, 0f);
                }
            }
            return fixes;
        }

        public bool Equals(CapareLootBoxSettingsData other) => CapareJson.AreEqual(this, other);
        public override bool Equals(object? obj) => Equals(obj as CapareLootBoxSettingsData);
        public override int GetHashCode() => base.GetHashCode();
        public CapareLootBoxSettingsData Clone() => CapareJson.Clone(this);
    }
    public class CapareLootBoxSettings
    {
        public string LootBoxName { get; set; }
        public BindingList<string> PossibleLootList { get; set; }

        public CapareLootBoxSettings() { }
        public CapareLootBoxSettings(string lootboxname, params string[] possiblelootlist)
        {
            LootBoxName = lootboxname;
            PossibleLootList = new BindingList<string>(possiblelootlist.ToList());
        }
        public override string ToString() => LootBoxName;
    }
    public class CapareLootBoxStaticBox
    {
        public string LootBoxSetName { get; set; }
        public BindingList<string> PossibleLootBoxNames { get; set; }
        public int NumberOfPositions { get; set; }
        public BindingList<CapareStaticBoxPosRot> StaticBoxPositions { get; set; }
        public bool UseCustomLootList { get; set; }
        public BindingList<string> CustomLootList { get; set; }

        public CapareLootBoxStaticBox() { }
        public override string ToString() => LootBoxSetName;
    }
    public class CapareStaticBoxPosRot
    {
        public Vec3 Position { get; set; }
        public Vec3 Rotation { get; set; }

        public CapareStaticBoxPosRot() { }
        public CapareStaticBoxPosRot(Vec3 position, Vec3 rotation)
        {
            Position = position;
            Rotation = rotation;
        }
        public override string ToString() => Position.GetString();
    }
}
