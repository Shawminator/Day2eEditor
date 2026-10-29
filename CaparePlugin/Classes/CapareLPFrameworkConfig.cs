using Day2eEditor;
using System.ComponentModel;

namespace CaparePlugin
{
    /// <summary>
    /// $profile:Capare_Shawminator/Capare_LP_Framework/Capare_LP_Framework.json
    /// Capare_Scripts_Shawminator - CapareLPLootConfig.c / CapareLPCore.c
    /// </summary>
    public class CapareLPFrameworkConfig : CapareConfigBase<CapareLPLootData>
    {
        public CapareLPFrameworkConfig(string path) : base(path)
        {
        }
        protected override CapareLPLootData CreateDefaultData()
        {
            return CapareLPLootData.CreateDefault();
        }
        protected override IEnumerable<string> ValidateData()
        {
            return Data.FixMissingOrInvalidFields();
        }
    }

    public class CapareLPLootData : IEquatable<CapareLPLootData>, IDeepCloneable<CapareLPLootData>
    {
        public BindingList<CapareLPRewardTable> CapareLPRewardTables { get; set; }
        public BindingList<CapareLPLootTable> CapareLPLootTables { get; set; }
        public BindingList<CapareLPLootItemSet> CapareLPItemSets { get; set; }
        public int NumberOfExtraMagsForDefinedWeapons { get; set; }
        public BindingList<CapareLPdefinedItem> CapareLPdefinedItems { get; set; }

        public CapareLPLootData()
        {
        }

        // Same values CapareLPDefaultConfig() writes in the mod
        public static CapareLPLootData CreateDefault()
        {
            var data = new CapareLPLootData()
            {
                CapareLPRewardTables = new BindingList<CapareLPRewardTable>()
                {
                    new CapareLPRewardTable("Reward_Table_1", "Loot_Table_Weapons_1", "Loot_Table_Weapons_1", "Loot_Table_Magazines_1", "Loot_Table_Magazines_2"),
                    new CapareLPRewardTable("Reward_Table_2", "Defined_FirstAidKit", "Defined_NVGHeadstrap", "ItemSet_NBC_Yellow", "GasMask_Filter")
                },
                CapareLPLootTables = new BindingList<CapareLPLootTable>()
                {
                    new CapareLPLootTable("Loot_Table_Weapons_1", "Glock19", "Colt1911", "MakarovIJ70"),
                    new CapareLPLootTable("Loot_Table_Magazines_1", "Mag_MP5_30Rnd", "Mag_AK74_30Rnd", "Mag_CZ527_5rnd"),
                    new CapareLPLootTable("Loot_Table_Magazines_2", "Mag_STANAG_30Rnd", "Mag_AKM_30Rnd", "Mag_FAL_20Rnd")
                },
                CapareLPItemSets = new BindingList<CapareLPLootItemSet>()
                {
                    new CapareLPLootItemSet("ItemSet_NBC_Yellow", "NBCBootsYellow", "NBCGlovesYellow", "NBCHoodYellow", "NBCJacketYellow", "NBCPantsYellow", "Defined_GasMask")
                },
                NumberOfExtraMagsForDefinedWeapons = 1,
                CapareLPdefinedItems = new BindingList<CapareLPdefinedItem>()
            };

            var mp5k = new CapareLPdefinedItem("Defined_MP5K", "MP5K", false, "Mag_MP5_15Rnd", "Mag_MP5_30Rnd");
            mp5k.AddAttachmentSlot("MP5_RailHndgrd");
            mp5k.AddAttachmentSlot("MP5k_StockBttstck");
            mp5k.AddAttachmentSlot("BUISOptic", "M68Optic", "M4_T3NRDSOptic", "ReflexOptic", "ACOGOptic");
            mp5k.AddAttachmentSlot("MP5_Compensator");
            mp5k.AddAttachmentSlot("PistolSuppressor");
            data.CapareLPdefinedItems.Add(mp5k);

            var firstAidKit = new CapareLPdefinedItem("Defined_FirstAidKit", "FirstAidKit", false);
            firstAidKit.AddAttachmentSlot("BandageDressing");
            firstAidKit.AddAttachmentSlot("Thermometer");
            firstAidKit.AddAttachmentSlot("StartKitIV");
            firstAidKit.AddAttachmentSlot("BloodBagEmpty");
            firstAidKit.AddAttachmentSlot("BloodTestKit");
            data.CapareLPdefinedItems.Add(firstAidKit);

            // The mod builds Defined_NVGHeadstrap but never inserts it, so it is not in its default file either
            var gasMask = new CapareLPdefinedItem("Defined_GasMask", "GasMask", false);
            gasMask.AddAttachmentSlot("GasMask_Filter");
            data.CapareLPdefinedItems.Add(gasMask);

            return data;
        }

        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();
            if (CapareLPRewardTables == null)
            {
                CapareLPRewardTables = new BindingList<CapareLPRewardTable>();
                fixes.Add("Initialized CapareLPRewardTables");
            }
            if (CapareLPLootTables == null)
            {
                CapareLPLootTables = new BindingList<CapareLPLootTable>();
                fixes.Add("Initialized CapareLPLootTables");
            }
            if (CapareLPItemSets == null)
            {
                CapareLPItemSets = new BindingList<CapareLPLootItemSet>();
                fixes.Add("Initialized CapareLPItemSets");
            }
            if (CapareLPdefinedItems == null)
            {
                CapareLPdefinedItems = new BindingList<CapareLPdefinedItem>();
                fixes.Add("Initialized CapareLPdefinedItems");
            }
            foreach (CapareLPRewardTable table in CapareLPRewardTables)
            {
                if (table.Rewards == null)
                {
                    table.Rewards = new BindingList<string>();
                    fixes.Add($"Initialized Rewards for {table.RewardName}");
                }
            }
            foreach (CapareLPLootTable table in CapareLPLootTables)
            {
                if (table.LootItems == null)
                {
                    table.LootItems = new BindingList<string>();
                    fixes.Add($"Initialized LootItems for {table.TableName}");
                }
            }
            foreach (CapareLPLootItemSet set in CapareLPItemSets)
            {
                if (set.SetItems == null)
                {
                    set.SetItems = new BindingList<string>();
                    fixes.Add($"Initialized SetItems for {set.SetName}");
                }
            }
            foreach (CapareLPdefinedItem item in CapareLPdefinedItems)
            {
                if (item.magazines == null)
                {
                    item.magazines = new BindingList<string>();
                    fixes.Add($"Initialized magazines for {item.DefineName}");
                }
                if (item.attachments == null)
                {
                    item.attachments = new BindingList<CapareLPdefinewItemAttachments>();
                    fixes.Add($"Initialized attachments for {item.DefineName}");
                }
                foreach (CapareLPdefinewItemAttachments slot in item.attachments)
                {
                    slot.attachments ??= new BindingList<string>();
                }
            }
            return fixes;
        }

        // Every name a reward table entry can point at
        public List<string> GetRewardEntryNames()
        {
            return CapareLPLootTables.Select(x => x.TableName)
                .Concat(CapareLPItemSets.Select(x => x.SetName))
                .Concat(CapareLPdefinedItems.Select(x => x.DefineName))
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct()
                .ToList();
        }

        public bool Equals(CapareLPLootData other) => CapareJson.AreEqual(this, other);
        public override bool Equals(object? obj) => Equals(obj as CapareLPLootData);
        public override int GetHashCode() => base.GetHashCode();
        public CapareLPLootData Clone() => CapareJson.Clone(this);
    }
    public class CapareLPRewardTable
    {
        public string RewardName { get; set; }
        public BindingList<string> Rewards { get; set; }

        public CapareLPRewardTable() { }
        public CapareLPRewardTable(string rewardname, params string[] rewards)
        {
            RewardName = rewardname;
            Rewards = new BindingList<string>(rewards.ToList());
        }
        public override string ToString() => RewardName;
    }
    public class CapareLPLootTable
    {
        public string TableName { get; set; }
        public BindingList<string> LootItems { get; set; }

        public CapareLPLootTable() { }
        public CapareLPLootTable(string tablename, params string[] lootitems)
        {
            TableName = tablename;
            LootItems = new BindingList<string>(lootitems.ToList());
        }
        public override string ToString() => TableName;
    }
    public class CapareLPLootItemSet
    {
        public string SetName { get; set; }
        public BindingList<string> SetItems { get; set; }

        public CapareLPLootItemSet() { }
        public CapareLPLootItemSet(string setname, params string[] setitems)
        {
            SetName = setname;
            SetItems = new BindingList<string>(setitems.ToList());
        }
        public override string ToString() => SetName;
    }
    public class CapareLPdefinedItem
    {
        public string DefineName { get; set; }
        public string Item { get; set; }
        public bool SpawnExact { get; set; }
        public BindingList<string> magazines { get; set; }
        public BindingList<CapareLPdefinewItemAttachments> attachments { get; set; }

        public CapareLPdefinedItem() { }
        public CapareLPdefinedItem(string defineName, string item, bool spawnExact, params string[] mags)
        {
            DefineName = defineName;
            Item = item;
            SpawnExact = spawnExact;
            magazines = new BindingList<string>(mags.ToList());
            attachments = new BindingList<CapareLPdefinewItemAttachments>();
        }
        public void AddAttachmentSlot(params string[] items)
        {
            attachments.Add(new CapareLPdefinewItemAttachments() { attachments = new BindingList<string>(items.ToList()) });
        }
        public override string ToString() => DefineName;
    }
    // One entry per attachment slot - one random item from the list is attached
    public class CapareLPdefinewItemAttachments
    {
        public BindingList<string> attachments { get; set; }

        public CapareLPdefinewItemAttachments() { }
    }
}
