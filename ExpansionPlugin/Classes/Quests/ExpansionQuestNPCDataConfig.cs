using Day2eEditor;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ExpansionPlugin
{
    public enum ExpansionQuestNPCType
    {
        NORMAL,
        OBJECT,
        AI
    };
    public class ExpansionQuestNPCDataConfig : MultiFileConfigLoaderBase<ExpansionQuestNPCData>
    {
        public const int CurrentVersion = 6;
        public ExpansionQuestNPCDataConfig(string path) : base(path)
        {
        }
        protected override ExpansionQuestNPCData LoadItem(string filePath)
        {
            var TraderZone = AppServices.GetRequired<FileService>().LoadOrCreateJson(
                    filePath,
                    createNew: () => new ExpansionQuestNPCData(),
                    onError: ex => HandleItemError(filePath, ex),
                    configName: "ExpansionQuestNPCData",
                    useVecConvertor: true
                );

            TraderZone.SetPath(filePath);
            TraderZone.SetGuid(Guid.NewGuid());
            return TraderZone;
        }
        protected override void SaveItem(ExpansionQuestNPCData ExpansionQuestNPCData)
        {
            AppServices.GetRequired<FileService>().SaveJson(ExpansionQuestNPCData._path, ExpansionQuestNPCData, false, true);
        }
        protected override string GetItemFileName(ExpansionQuestNPCData ExpansionQuestNPCData)
            => ExpansionQuestNPCData.FileName;
        protected override string GetItemFilePath(ExpansionQuestNPCData ExpansionQuestNPCData)
            => ExpansionQuestNPCData.FilePath;
        protected override bool ShouldDelete(ExpansionQuestNPCData ExpansionQuestNPCData)
            => ExpansionQuestNPCData.ToDelete;
        protected override Guid GetID(ExpansionQuestNPCData ExpansionQuestNPCData)
            => ExpansionQuestNPCData.Id;
        protected override void DeleteItemFile(ExpansionQuestNPCData ExpansionQuestNPCData)
        {
            if (!string.IsNullOrWhiteSpace(ExpansionQuestNPCData._path) && File.Exists(ExpansionQuestNPCData._path))
            {
                File.Delete(ExpansionQuestNPCData._path);
            }
        }
        public bool needToSave()
        {
            return false;
        }
        protected override IEnumerable<string> ValidateItem(ExpansionQuestNPCData ExpansionQuestNPCData)
        {
            return ExpansionQuestNPCData.FixMissingOrInvalidFields();
        }
    }
    public class ExpansionQuestNPCData : IDeepCloneable<ExpansionQuestNPCData>, IEquatable<ExpansionQuestNPCData>
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

        public void SetPath(string path) => _path = path;
        internal void SetGuid(Guid guid) => Id = guid;



        public int ConfigVersion { get;set; }
        public int? ID { get; set; }
        public string? ClassName { get; set; }
        public Vec3? Position { get; set; }
        public Vec3? Orientation { get; set; }
        public string? NPCName { get; set; }
        public string? DefaultNPCText { get; set; }
        public BindingList<Vec3> Waypoints { get; set; }
        public int? NPCEmoteID { get; set; }
        public int? NPCEmoteIsStatic { get; set; }
        public string? NPCLoadoutFile { get; set; }
        public int? NPCInteractionEmoteID { get; set; }
        public int? NPCQuestCancelEmoteID { get; set; }
        public int? NPCQuestStartEmoteID { get; set; }
        public int? NPCQuestCompleteEmoteID { get; set; }
        public string? NPCFaction { get; set; }
        public ExpansionQuestNPCType? NPCType { get; set; }

        public ExpansionQuestNPCData Clone()
        {
            throw new NotImplementedException();
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionQuestNPCData);
        public bool Equals(ExpansionQuestNPCData? other)
        {
            throw new NotImplementedException();
        }

        internal IEnumerable<string> FixMissingOrInvalidFields()
        {
            throw new NotImplementedException();
        }
    }
}
