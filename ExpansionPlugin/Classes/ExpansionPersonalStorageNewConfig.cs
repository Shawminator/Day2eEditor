using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionPersonalStorageNewConfig : ExpansionBaseIConfigLoader<ExpansionPersonalStorageNewSettings>
    {
        public const int CurrentVersion = 4;
        public ExpansionPersonalStorageNewConfig(string path) : base(path)
        {
        }
        protected override ExpansionPersonalStorageNewSettings CreateDefaultData()
        {
            return new ExpansionPersonalStorageNewSettings(CurrentVersion);
        }
        protected override IEnumerable<string> ValidateData()
        {
            return Data.FixMissingOrInvalidFields();
        }
    }
    public class ExpansionPersonalStorageNewSettings : IEquatable<ExpansionPersonalStorageNewSettings>, IDeepCloneable<ExpansionPersonalStorageNewSettings>
    {
        public int m_Version { get; set; }
        public int? UseCategoryMenu { get; set; }
        public BindingList<string> ExcludedItems { get; set; }
        public Dictionary<int, ExpansionPersonalStorageLevel> StorageLevels { get; set; }

        public ExpansionPersonalStorageNewSettings()
        {

        }
        public ExpansionPersonalStorageNewSettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
            UseCategoryMenu = 0;
            ExcludedItems = new BindingList<string>();
            StorageLevels = new Dictionary<int, ExpansionPersonalStorageLevel>();

            DefaultStirageLevels();
        }
        private void DefaultStirageLevels()
        {
            List<string> defaultExcludedSlots = new List<string>() { "Vest", "Body", "Hips", "Legs", "Back" };

            for (int lvl = 1; lvl <= 10; lvl++)
            {
                switch (lvl)
                {
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                        StorageLevels.Add(lvl, new ExpansionPersonalStorageLevel(lvl, -1, 0));
                        break;
                    case 10:
                        StorageLevels.Add(lvl, new ExpansionPersonalStorageLevel(lvl, -1, 0, null, 1));
                        break;
                    default:
                        StorageLevels.Add(lvl, new ExpansionPersonalStorageLevel(lvl, -1, 0, new BindingList<string>(defaultExcludedSlots), 0));
                        break;
                }
            }
        }
        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (m_Version != ExpansionPersonalStorageNewConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionPersonalStorageNewConfig.CurrentVersion}");
                m_Version = ExpansionPersonalStorageNewConfig.CurrentVersion;
            }
            if (UseCategoryMenu == null || (UseCategoryMenu != 0 && UseCategoryMenu != 1))
            {
                UseCategoryMenu = 0;
                fixes.Add("Corrected UseCategoryMenu to 0");
            }
            if (ExcludedItems == null)
            {
                ExcludedItems = new BindingList<string>();
                fixes.Add("Initialized ExcludedItems");
            }
            if (StorageLevels == null)
            {
                DefaultStirageLevels();
                fixes.Add("Initialized StorageLevels to default Levels");
            }

            return fixes;
        }
        public bool Equals(ExpansionPersonalStorageNewSettings other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;


            if (m_Version != other.m_Version ||
                   UseCategoryMenu != other.UseCategoryMenu ||
                   !ExcludedItems.SequenceEqual(other.ExcludedItems))
                return false;

            if (!DictionaryEqual(StorageLevels, other.StorageLevels))
                return false;

            return true;
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionPersonalStorageNewSettings);
        public ExpansionPersonalStorageNewSettings Clone()
        {
            return new ExpansionPersonalStorageNewSettings()
            {
                m_Version = this.m_Version,
                UseCategoryMenu = this.UseCategoryMenu,
                ExcludedItems = new BindingList<string>(this.ExcludedItems.ToList()),
                StorageLevels = this.StorageLevels.ToDictionary(kv => kv.Key, kv => kv.Value?.Clone())
            };
        }
        private static bool DictionaryEqual<TKey, TValue>( Dictionary<TKey, TValue>? a, Dictionary<TKey, TValue>? b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            if (a.Count != b.Count) return false;

            foreach (var (key, value) in a)
            {
                if (!b.TryGetValue(key, out var otherValue))
                    return false;

                if (!Equals(value, otherValue))
                    return false;
            }
            return true;
        }
    }

    public class ExpansionPersonalStorageLevel
    {
        public int? ReputationRequirement { get; set; }
        public int? QuestID { get; set; }
        public BindingList<string> ExcludedSlots { get; set; }
	    public int? AllowAttachmentCargo { get; set; }

        [JsonIgnore]
        public int m_Level { get; set; }
        public ExpansionPersonalStorageLevel()
        {

        }
        public ExpansionPersonalStorageLevel(int lvl, int repReq, int questID, BindingList<string> excludedSlots = null, int allowAttCargo = 0)
        {
            m_Level = lvl;
            ReputationRequirement = repReq;
            QuestID = questID;
            if (excludedSlots != null)
                ExcludedSlots = excludedSlots;
            AllowAttachmentCargo = allowAttCargo;
        }

        public override bool Equals(object obj)
        {
            if (obj is not ExpansionPersonalStorageLevel other)
                return false;

            if (ReputationRequirement != other.ReputationRequirement ||
                   QuestID != other.QuestID ||
                   ExcludedSlots.SequenceEqual(other.ExcludedSlots) ||
                   AllowAttachmentCargo != other.AllowAttachmentCargo)
                return false;

            return true;
        }
        public ExpansionPersonalStorageLevel Clone()
        {
            return new ExpansionPersonalStorageLevel
            {
                ReputationRequirement = this.ReputationRequirement,
                QuestID = this.QuestID,
                AllowAttachmentCargo = this.AllowAttachmentCargo,
                ExcludedSlots = new BindingList<string>(this.ExcludedSlots.ToList())
            };
        }

    }
}
