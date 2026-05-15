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

        internal List<int> GetAllNPCIDS()
        {
            return MutableItems.Select(npcdata => (int)npcdata.ID).OrderBy(id => id).ToList();
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
        public EmoteConstants? NPCEmoteID { get; set; }
        public int? NPCEmoteIsStatic { get; set; }
        public string? NPCLoadoutFile { get; set; }
        public EmoteConstants? NPCInteractionEmoteID { get; set; }
        public EmoteConstants? NPCQuestCancelEmoteID { get; set; }
        public EmoteConstants? NPCQuestStartEmoteID { get; set; }
        public EmoteConstants? NPCQuestCompleteEmoteID { get; set; }
        public string? NPCFaction { get; set; }
        public ExpansionQuestNPCType? NPCType { get; set; }
        public int? Active { get; set;  }

        public ExpansionQuestNPCData Clone()
        {
            var clone = new ExpansionQuestNPCData()
            {
                ConfigVersion = ConfigVersion,
                ID = ID,
                ClassName = ClassName,
                Position = Position?.Clone(),
                Orientation = Orientation?.Clone(),
                NPCName = NPCName,
                DefaultNPCText = DefaultNPCText,
                Waypoints = Waypoints != null
                    ? new BindingList<Vec3>(Waypoints.Select(x => x.Clone()).ToList())
                    : null,
                NPCEmoteID = NPCEmoteID,
                NPCEmoteIsStatic = NPCEmoteIsStatic,
                NPCLoadoutFile = NPCLoadoutFile,
                NPCInteractionEmoteID = NPCInteractionEmoteID,
                NPCQuestCancelEmoteID = NPCQuestCancelEmoteID,
                NPCQuestStartEmoteID = NPCQuestStartEmoteID,
                NPCQuestCompleteEmoteID = NPCQuestCompleteEmoteID,
                NPCFaction = NPCFaction,
                NPCType = NPCType,
                Active = Active
            };
            clone.SetPath(_path);
            clone.SetGuid(Id);
            return clone;

        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionQuestNPCData);
        public bool Equals(ExpansionQuestNPCData? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (Id != other.Id) return false;

            if (_path != other._path) return false;
            if (ConfigVersion != other.ConfigVersion) return false;
            if (ID != other.ID) return false;
            if (!Equals(ClassName, other.ClassName)) return false;
            if (!Equals(Position, other.Position)) return false;
            if (!Equals(Orientation, other.Orientation)) return false;
            if (!Equals(NPCName, other.NPCName)) return false;
            if (!Equals(DefaultNPCText, other.DefaultNPCText)) return false;
            if (!ListEquals(Waypoints, other.Waypoints)) return false;
            if (NPCEmoteID != other.NPCEmoteID) return false;
            if (NPCEmoteIsStatic != other.NPCEmoteIsStatic) return false;
            if (!Equals(NPCLoadoutFile, other.NPCLoadoutFile)) return false;
            if (NPCInteractionEmoteID != other.NPCInteractionEmoteID) return false;
            if (NPCQuestCancelEmoteID != other.NPCQuestCancelEmoteID) return false;
            if (NPCQuestStartEmoteID != other.NPCQuestStartEmoteID) return false;
            if (NPCQuestCompleteEmoteID != other.NPCQuestCompleteEmoteID) return false;
            if (!Equals(NPCFaction, other.NPCFaction)) return false;
            if (NPCType != other.NPCType) return false;
            if (Active != other.Active) return false;
            
            return true;

        }

        internal IEnumerable<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (ConfigVersion != ExpansionQuestNPCDataConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {ConfigVersion} to {ExpansionQuestNPCDataConfig.CurrentVersion}");
                ConfigVersion = ExpansionQuestNPCDataConfig.CurrentVersion;
            }

            if (ClassName == null)
            {
                ClassName = string.Empty;
                fixes.Add("Initialised ClassName");
            }

            if (Position == null)
            {
                Position = new Vec3();
                fixes.Add("Initialised Position");
            }

            if (Orientation == null)
            {
                Orientation = new Vec3();
                fixes.Add("Initialised Orientation");
            }

            if (NPCName == null)
            {
                NPCName = "Unknown";
                fixes.Add("Initialised NPCName");
            }

            if (DefaultNPCText == null)
            {
                DefaultNPCText = "What do you want? Leave me alone!";
                fixes.Add("Initialised DefaultNPCText");
            }

            if (Waypoints == null)
            {
                Waypoints = new BindingList<Vec3>();
                fixes.Add("Initialised Waypoints");
            }

            if (NPCEmoteID == null)
            {
                NPCEmoteID = EmoteConstants.Watching;
                fixes.Add("Initialised NPCEmoteID");
            }

            if (NPCEmoteIsStatic == null)
            {
                NPCEmoteIsStatic = default;
                fixes.Add("Initialised NPCEmoteIsStatic");
            }

            if (NPCLoadoutFile == null)
            {
                NPCLoadoutFile = string.Empty;
                fixes.Add("Initialised NPCLoadoutFile");
            }

            if (NPCInteractionEmoteID == null)
            {
                NPCInteractionEmoteID = EmoteConstants.Greeting;
                fixes.Add("Initialised NPCInteractionEmoteID");
            }

            if (NPCQuestCancelEmoteID == null)
            {
                NPCQuestCancelEmoteID = EmoteConstants.Shrug;
                fixes.Add("Initialised NPCQuestCancelEmoteID");
            }

            if (NPCQuestStartEmoteID == null)
            {
                NPCQuestStartEmoteID = EmoteConstants.Nod;
                fixes.Add("Initialised NPCQuestStartEmoteID");
            }

            if (NPCQuestCompleteEmoteID == null)
            {
                NPCQuestCompleteEmoteID = EmoteConstants.Clap;
                fixes.Add("Initialised NPCQuestCompleteEmoteID");
            }

            if (NPCFaction == null)
            {
                NPCFaction = "InvincibleObservers";
                fixes.Add("Initialised NPCFaction");
            }

            if (NPCType == null)
            {
                NPCType = default;
                fixes.Add("Initialised NPCType");
            }

            if (NPCType == ExpansionQuestNPCType.AI)
            {
                if (Waypoints == null)
                {
                    Waypoints = new BindingList<Vec3>();
                    fixes.Add("Initialised Waypoints for AI NPC");
                }

                if (Position != null)
                {
                    if (Waypoints.Count == 0)
                    {
                        Waypoints.Insert(0, Position.Clone());
                        fixes.Add("Inserted Position as first Waypoint for AI NPC");
                    }
                    else if (!Equals(Waypoints[0], Position))
                    {
                        Waypoints.Insert(0, Position.Clone());
                        fixes.Add("Corrected first Waypoint to match Position for AI NPC");
                    }
                }
            }

            return fixes;


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

        public string GetNPCType()
        {
            return NPCType switch
            {
                ExpansionQuestNPCType.AI => "[Roaming AI]",
                ExpansionQuestNPCType.OBJECT => "[Object]",
                ExpansionQuestNPCType.NORMAL => "[Static]",
                _ => "[Unknown]"
            };
        }
        public bool GetISAI()
        {
            return NPCType == ExpansionQuestNPCType.AI &&
                   Waypoints != null &&
                   Waypoints.Count > 0;
        }
    }
}
