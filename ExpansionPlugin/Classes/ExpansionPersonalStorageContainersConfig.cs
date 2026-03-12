using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    public class ExpansionPersonalStorageContainersConfig : MultiFileConfigLoader<ExpansionPersonalStorageConfig>
    {
        public List<int> UsedIDS = new List<int>();
        public ExpansionPersonalStorageContainersConfig(string path) : base(path)
        {
        }
        protected override ExpansionPersonalStorageConfig LoadItem(string filePath)
        {
            var PSContainer = AppServices.GetRequired<FileService>().LoadOrCreateJson(
                    filePath,
                    createNew: () => new ExpansionPersonalStorageConfig(),
                    onError: ex => HandleItemError(filePath, ex),
                    configName: "P2PTrader",
                    useVecConvertor: true
                );

            PSContainer.SetPath(filePath);
            PSContainer.SetGuid(Guid.NewGuid());
            UsedIDS.Add((int)PSContainer.StorageID);
            return PSContainer;
        }
        protected override void SaveItem(ExpansionPersonalStorageConfig ExpansionPersonalStorageConfig)
        {
            AppServices.GetRequired<FileService>().SaveJson(ExpansionPersonalStorageConfig._path, ExpansionPersonalStorageConfig, false, true);
        }
        protected override string GetItemFileName(ExpansionPersonalStorageConfig ExpansionPersonalStorageConfig)
            => ExpansionPersonalStorageConfig.FileName;
        protected override bool ShouldDelete(ExpansionPersonalStorageConfig ExpansionPersonalStorageConfig)
            => ExpansionPersonalStorageConfig.ToDelete;
        protected override Guid GetID(ExpansionPersonalStorageConfig ExpansionPersonalStorageConfig)
            => ExpansionPersonalStorageConfig.Id;
        protected override void DeleteItemFile(ExpansionPersonalStorageConfig ExpansionPersonalStorageConfig)
        {
            int id = (int)ExpansionPersonalStorageConfig.StorageID;
            if (!string.IsNullOrWhiteSpace(ExpansionPersonalStorageConfig._path) && File.Exists(ExpansionPersonalStorageConfig._path))
            {
                File.Delete(ExpansionPersonalStorageConfig._path);
                UsedIDS.Remove(id);
            }
        }
        public int GetNextID()
        {
            if (UsedIDS.Count == 0)
                return 1;
            List<int> result = Enumerable.Range(1, UsedIDS.Max() + 1).Except(UsedIDS).ToList();
            result.Sort();
            return result[0];
        }
        internal ExpansionPersonalStorageConfig AddNewPersonalStorageFile(int newid)
        {
            string filepath = Path.Combine(AppServices.GetRequired<ExpansionManager>().basePath, "expansion", "personalstorage");
            string filename = "PersonalStorage_" + newid + ".json";
            ExpansionPersonalStorageConfig PSContainer = new ExpansionPersonalStorageConfig()
            {
                ConfigVersion = ExpansionPersonalStorageConfig.VERSION,
                StorageID = newid,
                ClassName = "ExpansionPersonalStorageChest",
                DisplayName = "Personal Storage",
                DisplayIcon = "Backpack",
                Position = new Vec3( new decimal[] { 0, 0, 0 }),
                Orientation = new Vec3(new decimal[] { 0, 0, 0 }),
                QuestID = -1,
                Reputation = 0,
                Faction = "",
                IsGlobalStorage = 0,
            };
            PSContainer.SetPath(Path.Combine(filepath, filename));
            PSContainer.SetGuid(Guid.NewGuid());
            UsedIDS.Add((int)PSContainer.StorageID);
            Items.Add(PSContainer);
            return PSContainer;

        }
        internal void RemoveFile(ExpansionPersonalStorageConfig ExpansionPersonalStorageConfig)
        {
            ExpansionPersonalStorageConfig.ToDelete = true;
        }
        public bool needToSave()
        {
            return false;
        }
        protected override IEnumerable<string> ValidateData(ExpansionPersonalStorageConfig ExpansionPersonalStorageConfig)
        {
            return ExpansionPersonalStorageConfig.FixMissingOrInvalidFields();
        }
    }
    public class ExpansionPersonalStorageConfig : IDeepCloneable<ExpansionPersonalStorageConfig>, IEquatable<ExpansionPersonalStorageConfig>
    {
        [JsonIgnore]
        public static int VERSION = 2;

        [JsonIgnore]
        public string _path { get; private set; }
        [JsonIgnore]
        public string FileName => Path.GetFileName(_path);
        [JsonIgnore]
        public bool ToDelete { get; set; }
        [JsonIgnore]
        public Guid Id { get; set; }

        public void SetPath(string path) => _path = path;
        internal void SetGuid(Guid guid) => Id = guid;

        public int ConfigVersion { get; set; }
        public int? StorageID { get; set; }
        public string? ClassName { get; set; }
        public string? DisplayName { get; set; }
        public string? DisplayIcon { get; set; }
        public Vec3? Position { get; set; }
        public Vec3? Orientation { get; set; }
        public int? QuestID { get; set; }
        public int? Reputation { get; set; }
        public string? Faction { get; set; }
        public int? IsGlobalStorage { get; set; }

        public bool Equals(ExpansionPersonalStorageConfig other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (ConfigVersion != other.ConfigVersion ||
                StorageID != other.StorageID ||
                ClassName != other.ClassName ||
                DisplayName != other.DisplayName ||
                DisplayIcon != other.DisplayIcon ||
                !Position.Equals(other.Position) ||
                !Orientation.Equals(other.Orientation) ||
                QuestID != other.QuestID ||
                Reputation != other.Reputation ||
                Faction != other.Faction ||
                IsGlobalStorage != other.IsGlobalStorage)
                return false;

            return true;
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionPersonalStorageConfig);
        public ExpansionPersonalStorageConfig Clone()
        {
            ExpansionPersonalStorageConfig clone = new ExpansionPersonalStorageConfig
            {
                ConfigVersion = this.ConfigVersion,
                StorageID = this.StorageID,
                ClassName = this.ClassName,
                DisplayName = this.DisplayName,
                DisplayIcon = this.DisplayIcon,

                Position = this.Position?.Clone(),
                Orientation = this.Orientation?.Clone(),

                QuestID = this.QuestID,
                Reputation = this.Reputation,
                Faction = this.Faction,
                IsGlobalStorage = this.IsGlobalStorage
            };

            clone.SetPath(_path);
            clone.SetGuid(Id);
            return clone;
        }
        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (ConfigVersion != VERSION)
            {
                fixes.Add($"Updated version from {ConfigVersion} to {VERSION}");
                ConfigVersion = VERSION;
            }
            if (StorageID == null)
            {
                fixes.Add($"{FileName} has no Storage ID, Please assign one......");
            }
            if (ClassName == null)
            {
                ClassName = "ExpansionPersonalStorageChest";
                fixes.Add("Set default ClassName to ExpansionPersonalStorageChest");
            }
            if (DisplayName == null)
            {
                DisplayName = "Personal Storage";
                fixes.Add("Set default DisplayName to Personal Storage");
            }
            if (DisplayIcon == null)
            {
                DisplayIcon = "Backpack";
                fixes.Add("Set default DisplayIcon to Backpack");
            }
            if (Position == null)
            {
                Position = new Vec3("0, 0, 0");
                fixes.Add("Set default Position to 0,0,0");
            }
            if (Orientation == null)
            {
                Orientation = new Vec3("0, 0, 0");
                fixes.Add("Set default Orientation to 0,0,0");
            }
            if (QuestID == null)
            {
                fixes.Add($"Updated QuestID to -1");
                QuestID = -1;
            }
            if (Reputation == null)
            {
                fixes.Add($"Updated Reputation to 0");
                Reputation = 0;
            }
            if (Faction == null)
            {
                fixes.Add($"Updated Faction to null");
                Faction = "";
            }
            if (IsGlobalStorage == null || IsGlobalStorage < 0 || IsGlobalStorage > 1)
            {
                fixes.Add($"Updated IsGlobalStorage to 0");
                IsGlobalStorage = 0;
            }
            return fixes;
        }
    }
}
