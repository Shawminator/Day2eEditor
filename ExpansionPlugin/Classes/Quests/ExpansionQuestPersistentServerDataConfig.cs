using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionQuestPersistentServerDataConfig : SingleFileConfigLoaderBase<ExpansionQuestPersistentServerData>
    {
        public const int CurrentVersion = 1;
        public ExpansionQuestPersistentServerDataConfig(string path) : base(path)
        {
        }
        public override void Load()
        {
            try
            {
                Data = AppServices.GetRequired<FileService>()
                    .LoadOrCreateJson(
                        _path,
                        createNew: () => CreateDefaultData(),
                        onError: ex => HandleLoadError(ex),
                        configName: FileName,
                        useVecConvertor: true
                    );
                var issues = ValidateData();
                if (issues?.Any() == true)
                {
                    Console.WriteLine("Validation issues in " + FileName + ":");
                    foreach (var msg in issues)
                        Console.WriteLine("- " + msg);

                    MarkDirty();
                }
                OnAfterLoad(Data);
                ClonedData = CloneData(Data);
            }
            catch (Exception ex)
            {
                HandleLoadError(ex);
            }
        }
        public override IEnumerable<string> Save()
        {
            if (Data is null)
                return Array.Empty<string>();

            if (!AreEqual(Data, ClonedData) || IsDirty == true)
            {
                ClearDirty();
                AppServices.GetRequired<FileService>().SaveJson(_path, Data, false, true);
                ClonedData = CloneData(Data);
                return new[] { _path };
            }

            return Array.Empty<string>();
        }
        protected override ExpansionQuestPersistentServerData CreateDefaultData()
        {
            return new ExpansionQuestPersistentServerData(CurrentVersion);
        }
        protected override IEnumerable<string> ValidateData()
        {
            return Data.FixMissingOrInvalidFields();
        }

    }
    public class ExpansionQuestPersistentServerData : IEquatable<ExpansionQuestPersistentServerData>, IDeepCloneable<ExpansionQuestPersistentServerData>
    {
        public int ConfigVersion { get; set; }
        public BindingList<ExpansionQuestItemForMarket> m_QuestMarketItems { get; set; }

        public ExpansionQuestPersistentServerData()
        { }
        public ExpansionQuestPersistentServerData(int CurrentVersion)
        {
            ConfigVersion = CurrentVersion;
            m_QuestMarketItems = new BindingList<ExpansionQuestItemForMarket>();
        }

        public ExpansionQuestPersistentServerData Clone()
        {
            return new ExpansionQuestPersistentServerData()
            {
                ConfigVersion = this.ConfigVersion,
                m_QuestMarketItems = new BindingList<ExpansionQuestItemForMarket>(this.m_QuestMarketItems.Select(x => x.Clone()).ToList()),
            };
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionQuestPersistentServerData);
        public bool Equals(ExpansionQuestPersistentServerData? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (ConfigVersion != other.ConfigVersion)
                return false;
            if (!ListEquals(m_QuestMarketItems, other.m_QuestMarketItems))
                    return false;

            return true;
        }
        private static bool ListEquals<T>(IList<T> a, IList<T> b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (a is null || b is null)
                return false;

            if (a.Count != b.Count)
                return false;

            for (int i = 0; i < a.Count; i++)
            {
                if (!Equals(a[i], b[i]))
                    return false;
            }

            return true;
        }

        internal IEnumerable<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (ConfigVersion != ExpansionQuestPersistentServerDataConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {ConfigVersion} to {ExpansionQuestPersistentServerDataConfig.CurrentVersion}");
                ConfigVersion = ExpansionQuestPersistentServerDataConfig.CurrentVersion;
            }
            if (m_QuestMarketItems == null)
            {
                m_QuestMarketItems = new BindingList<ExpansionQuestItemForMarket>();
                fixes.Add("Initialised m_QuestMarketItems");
            }

            return fixes;
        }
    }
    public class ExpansionQuestItemForMarket : IEquatable<ExpansionQuestItemForMarket>, IDeepCloneable<ExpansionQuestItemForMarket>
    {
        public Vec3? ZonePosition { get; set; }
        public string? ClassName { get; set; }
        public int? Amount { get; set; }

        public ExpansionQuestItemForMarket() 
        { }
        public ExpansionQuestItemForMarket(Vec3 pos, string className, int amount)
        {
            ZonePosition = pos;
            ClassName = className;
            Amount = amount;
        }

        public ExpansionQuestItemForMarket Clone()
        {
            return new ExpansionQuestItemForMarket
            {
                ZonePosition = this.ZonePosition.Clone(),
                ClassName = this.ClassName,
                Amount = this.Amount
            };
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionQuestItemForMarket);
        public bool Equals(ExpansionQuestItemForMarket? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                ZonePosition.Equals(other.ZonePosition) &&
                ClassName == other.ClassName &&
                Amount == other.Amount;
        }
    };
}
