using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public enum ExpansionQuestObjectiveType
    {
        NONE = 1,
        TARGET,
        TRAVEL,
        COLLECT,
        DELIVERY,
        TREASUREHUNT,
        AIPATROL,
        AICAMP,
        AIESCORT,
        ACTION,
        CRAFTING
    };
    public class ExpansionQuestObjectiveConfigConfig : MultiFileConfigLoaderBase<ExpansionQuestObjectiveConfig>
    {
        public const int CurrentVersion = 28;
        public ExpansionQuestObjectiveConfigConfig(string path) : base(path)
        {
        }
        protected override ExpansionQuestObjectiveConfig LoadItem(string filePath)
        {
            var json = File.ReadAllText(filePath);
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            if (!root.TryGetProperty("ObjectiveType", out var typeProp))
                throw new InvalidDataException("ObjectiveType missing");

            var objectiveType = (ExpansionQuestObjectiveType)typeProp.GetInt32();

            Type targetType = objectiveType switch
            {
                ExpansionQuestObjectiveType.TARGET => typeof(ExpansionQuestObjectiveTargetConfig),
                //ExpansionQuestObjectiveType.TRAVEL => typeof(ExpansionQuestObjectiveTravelConfig),
                //ExpansionQuestObjectiveType.COLLECT => typeof(ExpansionQuestObjectiveCollectionConfig),
                //ExpansionQuestObjectiveType.DELIVERY => typeof(ExpansionQuestObjectiveDeliveryConfig),
                //ExpansionQuestObjectiveType.TREASUREHUNT => typeof(ExpansionQuestObjectiveTreasureHuntConfig),
                //ExpansionQuestObjectiveType.AIPATROL => typeof(ExpansionQuestObjectiveAIPatrolConfig),
                //ExpansionQuestObjectiveType.AICAMP => typeof(ExpansionQuestObjectiveAICampConfig),
                //ExpansionQuestObjectiveType.AIESCORT => typeof(ExpansionQuestObjectiveAIEscortConfig),
                //ExpansionQuestObjectiveType.ACTION => typeof(ExpansionQuestObjectiveActionConfig),
                //ExpansionQuestObjectiveType.CRAFTING => typeof(ExpansionQuestObjectiveCraftingConfig),
                _ => throw new ArgumentOutOfRangeException(nameof(objectiveType))
            };

            var objective = (ExpansionQuestObjectiveConfig)AppServices.GetRequired<FileService>().LoadOrCreateJson(
                        filePath,
                        createNew: () => throw new InvalidOperationException("Objective configs must already exist."),
                        onError: ex => HandleItemError(filePath, ex),
                        configName: "ExpansionQuestObjectiveConfig",
                        useVecConvertor: true,
                        targetType: targetType
                    );

            objective.SetPath(filePath);
            objective.SetGuid(Guid.NewGuid());

            return objective;

        }
        protected override void SaveItem(ExpansionQuestObjectiveConfig item)
        {
            var fs = AppServices.GetRequired<FileService>();

            if (item is ExpansionQuestObjectiveTargetConfig ExpansionQuestObjectiveTargetConfig)
                fs.SaveJson(ExpansionQuestObjectiveTargetConfig._path, ExpansionQuestObjectiveTargetConfig, false, true);
            //else if (item is ExpansionMissionEventContaminatedArea contaminated)
            //    fs.SaveJson(contaminated._path, contaminated, true);
            //else if (item is ExpansionMissionEventHeliCrash helicrash)
            //    fs.SaveJson(helicrash._path, helicrash);
            else
                fs.SaveJson(item._path, item);
        }
        protected override string GetItemFileName(ExpansionQuestObjectiveConfig ExpansionQuestObjectiveConfigBase)
            => ExpansionQuestObjectiveConfigBase.FileName;
        protected override string GetItemFilePath(ExpansionQuestObjectiveConfig ExpansionQuestObjectiveConfigBase)
            => ExpansionQuestObjectiveConfigBase.FilePath;
        protected override bool ShouldDelete(ExpansionQuestObjectiveConfig ExpansionQuestObjectiveConfigBase)
            => ExpansionQuestObjectiveConfigBase.ToDelete;
        protected override Guid GetID(ExpansionQuestObjectiveConfig ExpansionQuestObjectiveConfigBase)
            => ExpansionQuestObjectiveConfigBase.Id;
        protected override void DeleteItemFile(ExpansionQuestObjectiveConfig ExpansionQuestObjectiveConfigBase)
        {
            if (!string.IsNullOrWhiteSpace(ExpansionQuestObjectiveConfigBase._path) && File.Exists(ExpansionQuestObjectiveConfigBase._path))
            {
                File.Delete(ExpansionQuestObjectiveConfigBase._path);
            }
        }
        public bool needToSave()
        {
            return false;
        }
        protected override IEnumerable<string> ValidateItem(ExpansionQuestObjectiveConfig ExpansionQuestObjectiveConfigBase)
        {
            return ExpansionQuestObjectiveConfigBase.FixMissingOrInvalidFields();
        }
    }
    public abstract class ExpansionQuestObjectiveConfig : IDeepCloneable<ExpansionQuestObjectiveConfig>, IEquatable<ExpansionQuestObjectiveConfig>
    {
        [JsonIgnore]
        public string _path { get; private set; }
        [JsonIgnore]
        public List<string> FolderParts { get; private set; }
        [JsonIgnore]
        public string FileName => Path.GetFileName(_path);
        [JsonIgnore]
        public string FilePath => _path;
        [JsonIgnore]
        public bool ToDelete { get; set; }
        [JsonIgnore]
        public Guid Id { get; set; }

        internal void SetGuid(Guid guid) => Id = guid;
        public void SetPath(string path) => _path = path;

        [JsonPropertyOrder(1)]
        public virtual int ConfigVersion { get; set; }
        [JsonPropertyOrder(2)]
        public virtual int ID { get; set; }
        [JsonPropertyOrder(3)]
        public virtual ExpansionQuestObjectiveType ObjectiveType { get; set; }
        [JsonPropertyOrder(4)]
        public virtual string? ObjectiveText { get; set; }
        [JsonPropertyOrder(5)]
        public virtual int? TimeLimit { get; set; }
        [JsonPropertyOrder(6)]
        public virtual int? Active { get; set; }

        public abstract ExpansionQuestObjectiveConfig Clone();

        public bool Equals(ExpansionQuestObjectiveConfig? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (ConfigVersion != other.ConfigVersion)
                return false;

            if (ID != other.ID)
                return false;

            if (ObjectiveType != other.ObjectiveType)
                return false;

            return true;
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionQuestObjectiveConfig);

        internal virtual IEnumerable<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (ConfigVersion != ExpansionQuestObjectiveConfigConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {ConfigVersion} to {ExpansionQuestObjectiveConfigConfig.CurrentVersion}");
                ConfigVersion = ExpansionQuestObjectiveConfigConfig.CurrentVersion;
            }


            return fixes;
        }
    }
}
