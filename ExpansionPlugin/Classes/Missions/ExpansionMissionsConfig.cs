using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ExpansionPlugin
{
    public class ExpansionMissionsConfig : MultiFileConfigLoaderBase<ExpansionMissionEventBase>
    {
        public ExpansionMissionsConfig(string path) : base(path)
        {
        }
        protected override ExpansionMissionEventBase LoadItem(string filePath)
        {
            string fileName = Path.GetFileName(filePath).ToLower();

            Type actualType =
                fileName.StartsWith("airdrop_") ? typeof(ExpansionMissionEventAirdrop) :
                fileName.StartsWith("contaminatedarea_") ? typeof(ExpansionMissionEventContaminatedArea) :
                fileName.StartsWith("helicrash_") ? typeof(ExpansionMissionEventHeliCrash) :
                fileName.StartsWith("horde_") ? typeof(ExpansionMissionEventHorde) :
                typeof(ExpansionMissionEventBase);

            var fileService = AppServices.GetRequired<FileService>();

            var item = (ExpansionMissionEventBase)fileService.LoadOrCreateJson(
                path: filePath,
                createNew: () => Activator.CreateInstance(actualType)!,
                onError: ex => HandleItemError(filePath, ex),
                configName: "Mission",
                useBoolConvertor: true,
                useVecConvertor: actualType == typeof(ExpansionMissionEventHorde),
                targetType: actualType
            );

            item.SetPath(filePath);
            item.SetGuid(Guid.NewGuid());
            return item;
        }
        protected override void SaveItem(ExpansionMissionEventBase item)
        {
            var fs = AppServices.GetRequired<FileService>();

            if (item is ExpansionMissionEventAirdrop airdrop)
                fs.SaveJson(airdrop._path, airdrop);
            else if (item is ExpansionMissionEventContaminatedArea contaminated)
                fs.SaveJson(contaminated._path, contaminated, true);
            else if (item is ExpansionMissionEventHeliCrash helicrash)
                fs.SaveJson(helicrash._path, helicrash);
            else if (item is ExpansionMissionEventHorde horde)
                fs.SaveJson(horde._path, horde, true, true);
            else
                fs.SaveJson(item._path, item);
        }
        protected override string GetItemFileName(ExpansionMissionEventBase item)
            => item.FileName;
        protected override string GetItemFilePath(ExpansionMissionEventBase ExpansionMissionEventBase)
            => ExpansionMissionEventBase.FilePath;
        protected override bool ShouldDelete(ExpansionMissionEventBase item)
            => item.ToDelete;
        protected override Guid GetID(ExpansionMissionEventBase item)
            => item.Id;
        protected override void DeleteItemFile(ExpansionMissionEventBase item)
        {
            if (!string.IsNullOrWhiteSpace(item._path) && File.Exists(item._path))
                File.Delete(item._path);
        }
        internal bool AddNewMissionFile(ExpansionMissionEventBase newExpansionMissionEventBase)
        {
            MutableItems.Add(newExpansionMissionEventBase);
            newExpansionMissionEventBase.Id = Guid.NewGuid();
            return true;
        }
        internal void RemoveFile(ExpansionMissionEventBase ExpansionMissionEventBase)
        {
            ExpansionMissionEventBase.ToDelete = true;
        }
        public bool needToSave()
        {
            return false;
        }
        protected override IEnumerable<string> ValidateItem(ExpansionMissionEventBase ExpansionMissionEventBase)
        {
            return ExpansionMissionEventBase.FixMissingOrInvalidFields();
        }
    }
    public class ExpansionMissionEventBase : IDeepCloneable<ExpansionMissionEventBase>, IEquatable<ExpansionMissionEventBase>
    {
        [JsonIgnore]
        public string _path { get; private set; }
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

        [JsonPropertyOrder(0)]
        public int m_Version { get; set; }
        [JsonPropertyOrder(1)]
        public int? Enabled { get; set; }
        [JsonPropertyOrder(2)]
        public decimal? Weight { get; set; }
        [JsonPropertyOrder(3)]
        public int? MissionMaxTime { get; set; }
        [JsonPropertyOrder(4)]
        public string? MissionName { get; set; }
        [JsonPropertyOrder(5)]
        public int? Difficulty { get; set; } // GUI ONLY. 0 - Easy, 1 - Medium, 2 - Hard
        [JsonPropertyOrder(6)]
        public int? Objective { get; set; } // GUI ONLY. 0 - Loot, 1 - Capture, 2 - ?
        [JsonPropertyOrder(7)]
        public string? Reward { get; set; } // GUI ONLY

        public ExpansionMissionEventBase()
        {

        }
        public bool Equals(ExpansionMissionEventBase other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != other.GetType()) return false;

            return EqualsCore(other);

        }
        protected virtual bool EqualsCore(ExpansionMissionEventBase other)
        {
            return m_Version == other.m_Version &&
                   Enabled == other.Enabled &&
                   Weight == other.Weight &&
                   MissionMaxTime == other.MissionMaxTime &&
                   MissionName == other.MissionName &&
                   Difficulty == other.Difficulty &&
                   Objective == other.Objective &&
                   Reward == other.Reward &&
                   Id == other.Id;
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionMissionEventBase);
        public virtual ExpansionMissionEventBase Clone()
        {
            ExpansionMissionEventBase clone = new ExpansionMissionEventBase
            {
                m_Version = m_Version,
                Enabled = Enabled,
                Weight = Weight,
                MissionMaxTime = MissionMaxTime,
                MissionName = MissionName,
                Difficulty = Difficulty,
                Objective = Objective,
                Reward = Reward
            };

            clone.SetPath(_path);
            clone.SetGuid(Id);
            return clone;
        }

        public virtual List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            return fixes;
        }
    }
    public class ExpansionMissionEventAirdrop : ExpansionMissionEventBase
    {
        public const int VERSION = 3;

        [JsonPropertyOrder(100)]
        public int? ShowNotification { get; set; }
        [JsonPropertyOrder(101)]
        public decimal? Height { get; set; }
        [JsonPropertyOrder(102)]
        public decimal? DropZoneHeight { get; set; }
        [JsonPropertyOrder(103)]
        public decimal? Speed { get; set; }
        [JsonPropertyOrder(104)]
        public decimal? DropZoneSpeed { get; set; }
        [JsonPropertyOrder(105)]
        public string? Container { get; set; }
        [JsonPropertyOrder(106)]
        public decimal? FallSpeed { get; set; }
        [JsonPropertyOrder(107)]
        public ExpansionAirdropLocation DropLocation { get; set; }
        [JsonPropertyOrder(108)]
        public BindingList<string> Infected { get; set; }
        [JsonPropertyOrder(109)]
        public int? ItemCount { get; set; }
        [JsonPropertyOrder(110)]
        public int? InfectedCount { get; set; }
        [JsonPropertyOrder(111)]
        public string? AirdropPlaneClassName { get; set; }
        [JsonPropertyOrder(112)]
        public BindingList<ExpansionLoot> Loot { get; set; }

        public ExpansionMissionEventAirdrop()
        {
            m_Version = VERSION;
        }

        protected override bool EqualsCore(ExpansionMissionEventBase otherBase)
        {
            if (!base.EqualsCore(otherBase)) return false;

            var other = (ExpansionMissionEventAirdrop)otherBase;

            return ShowNotification == other.ShowNotification &&
                   Height == other.Height &&
                   DropZoneHeight == other.DropZoneHeight &&
                   Speed == other.Speed &&
                   DropZoneSpeed == other.DropZoneSpeed &&
                   string.Equals(Container, other.Container, StringComparison.Ordinal) &&
                   FallSpeed == other.FallSpeed &&
                   Equals(DropLocation, other.DropLocation) &&
                   ItemCount == other.ItemCount &&
                   InfectedCount == other.InfectedCount &&
                   string.Equals(AirdropPlaneClassName, other.AirdropPlaneClassName, StringComparison.Ordinal) &&
                   Infected.SequenceEqual(other.Infected) &&
                   Loot.SequenceEqual(other.Loot); ;
        }
        public override ExpansionMissionEventBase Clone()
        {
            var clone = new ExpansionMissionEventAirdrop
            {
                m_Version = m_Version,
                Enabled = Enabled,
                Weight = Weight,
                MissionMaxTime = MissionMaxTime,
                MissionName = MissionName,
                Difficulty = Difficulty,
                Objective = Objective,
                Reward = Reward,

                ShowNotification = ShowNotification,
                Height = Height,
                DropZoneHeight = DropZoneHeight,
                Speed = Speed,
                DropZoneSpeed = DropZoneSpeed,
                Container = Container,
                FallSpeed = FallSpeed,
                DropLocation = DropLocation?.Clone(),
                ItemCount = ItemCount,
                InfectedCount = InfectedCount,
                AirdropPlaneClassName = AirdropPlaneClassName,

                Infected = Infected != null
                    ? new BindingList<string>(Infected.ToList())
                    : new BindingList<string>(),

                Loot = Loot != null
                    ? new BindingList<ExpansionLoot>(Loot.Select(li => li.Clone()).ToList())
                    : new BindingList<ExpansionLoot>(),

            };

            clone.SetPath(_path);
            clone.SetGuid(Id);
            return clone;
        }
        public override List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (m_Version != VERSION)
            {
                fixes.Add($"Updated version from {m_Version} to {VERSION}");
                m_Version = VERSION;
            }
            if (Enabled == null || (Enabled != 0 && Enabled != 1))
            {
                Enabled = 0;
                fixes.Add("Corrected Enabled to 0");
            }
            if (Weight == null)
            {
                Weight = 5m;
                fixes.Add("Set default Weight to 5.0");
            }
            if (MissionMaxTime == null)
            {
                MissionMaxTime = 1200;
                fixes.Add("Set default MissionMaxTime to 1200");
            }
            if(MissionName == null)
            {
                MissionName = "Random";
                fixes.Add("Set default MissionName to Random");
            }
            if (Difficulty == null)
            {
                Difficulty = 0;
                fixes.Add("Set default Difficulty to 0");
            }
            if (Objective == null)
            {
                Objective = 0;
                fixes.Add("Set default Objective to 0");
            }
            if (Reward != "")
            {
                Reward = "";
                fixes.Add("Set default Reward to null ");
            }
            if (ShowNotification == null || (ShowNotification != 0 && ShowNotification != 1))
            {
                ShowNotification = 1;
                fixes.Add("Corrected ShowNotification to 1");
            }
            if (Height == null)
            {
                Height = 450.0m;
                fixes.Add("Set default Height to 450");
            }
            if (DropZoneHeight == null)
            {
                DropZoneHeight = 450.0m;
                fixes.Add("Set default DropZoneHeight to 450");
            }
            if (Speed == null)
            {
                Speed = 25.0m;
                fixes.Add("Set default Speed to 25");
            }
            if (DropZoneSpeed == null)
            {
                DropZoneSpeed = 25.0m;
                fixes.Add("Set default DropZoneSpeed to 25");
            }
            if(Container == null)
            {
                Container = "Random";
                fixes.Add("Set default Container to Random");
            }
            if (FallSpeed == null)
            {
                FallSpeed = 4.5m;
                fixes.Add("Set default FallSpeed to 4.5");
            }
            if (ItemCount == null)
            {
                ItemCount = 0;
                fixes.Add("Set default ItemCount to 0");
            }
            if (InfectedCount == null)
            {
                InfectedCount = 0;
                fixes.Add("Set default InfectedCount to 0");
            }
            return fixes;
        }
    }
    public class ExpansionAirdropLocation
    {
        public decimal? x { get; set; }
        public decimal? z { get; set; }
        public string? Name { get; set; }
        public decimal? Radius { get; set; }

        public ExpansionAirdropLocation() { }
        public override bool Equals(object obj)
        {
            if (obj is not ExpansionAirdropLocation other)
                return false;

            return x == other.x &&
                   z == other.z &&
                   string.Equals(Name, other.Name, StringComparison.Ordinal) &&
                   Radius == other.Radius;
        }
        public ExpansionAirdropLocation Clone()
        {
            return new ExpansionAirdropLocation
            {
                x = this.x,
                z = this.z,
                Name = this.Name,
                Radius = this.Radius
            };
        }
    };
    public class ExpansionMissionEventContaminatedArea : ExpansionMissionEventBase
    {
        public const int VERSION = 0;

        [JsonPropertyOrder(100)]
        public Data Data { get; set; }
        [JsonPropertyOrder(102)]
        public PlayerData PlayerData { get; set; }
        [JsonPropertyOrder(103)]
        public decimal? StartDecayLifetime { get; set; }
        [JsonPropertyOrder(104)]
        public decimal? FinishDecayLifetime { get;set; }

        protected override bool EqualsCore(ExpansionMissionEventBase otherBase)
        {
            if (!base.EqualsCore(otherBase)) return false;

            var other = (ExpansionMissionEventContaminatedArea)otherBase;

            return Data.Equals(other.Data) &&
                PlayerData.Equals(other.PlayerData) &&
                StartDecayLifetime == other.StartDecayLifetime &&
                FinishDecayLifetime == other.FinishDecayLifetime;
        }
        public override ExpansionMissionEventBase Clone()
        {
            var clone = new ExpansionMissionEventContaminatedArea
            {
                m_Version = m_Version,
                Enabled = Enabled,
                Weight = Weight,
                MissionMaxTime = MissionMaxTime,
                MissionName = MissionName,
                Difficulty = Difficulty,
                Objective = Objective,
                Reward = Reward,

                Data = Data?.Clone(),
                PlayerData = PlayerData?.Clone(),
                StartDecayLifetime = StartDecayLifetime,
                FinishDecayLifetime = FinishDecayLifetime,

            };

            clone.SetPath(_path);
            clone.SetGuid(Id);
            return clone;
        }
        public override List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (m_Version != VERSION)
            {
                fixes.Add($"Updated version from {m_Version} to {VERSION}");
                m_Version = VERSION;
            }
            if (Enabled == null || (Enabled != 0 && Enabled != 1))
            {
                Enabled = 0;
                fixes.Add("Corrected Enabled to 0");
            }
            if (Weight == null)
            {
                Weight = 5m;
                fixes.Add("Set default Weight to 5.0");
            }
            if (MissionMaxTime == null)
            {
                MissionMaxTime = 600;
                fixes.Add("Set default MissionMaxTime to 600");
            }
            if (Difficulty == null)
            {
                Difficulty = 0;
                fixes.Add("Set default Difficulty to 0");
            }
            if (Objective == null)
            {
                Objective = 0;
                fixes.Add("Set default Objective to 0");
            }
            if (Reward != "")
            {
                Reward = "";
                fixes.Add("Set default Reward to null ");
            }




            return fixes;
        }
    }
    public class ExpansionMissionEventHeliCrash : ExpansionMissionEventBase
    {
        [JsonPropertyOrder(100)]
        public int? MakeCrashAreaPVP { get; set; }
        [JsonPropertyOrder(101)]
        public int? ShowCrashMapMarker { get; set;  }
        [JsonPropertyOrder(102)]
        public int? UseLootFramework { get; set; }
        [JsonPropertyOrder(103)]
        public int? ShowNotifications { get; set; }
        [JsonPropertyOrder(104)]
        public decimal? Height { get; set;  }
        [JsonPropertyOrder(105)]
        public decimal? Speed { get; set; }
        [JsonPropertyOrder(106)]
        public ExpansionAirdropLocation CrashLocation { get; set; }
        [JsonPropertyOrder(107)]
        public BindingList<string> RewardTables { get; set;  }
        [JsonPropertyOrder(108)]
        public BindingList<string> Infected { get; set; }
        [JsonPropertyOrder(109)]
        public int? ItemCount { get; set; }
        [JsonPropertyOrder(110)]
        public int? InfectedCount { get; set; }
        [JsonPropertyOrder(111)]
        public BindingList<ExpansionLoot> Loot { get; set; }

        protected override bool EqualsCore(ExpansionMissionEventBase otherBase)
        {
            if (!base.EqualsCore(otherBase)) return false;

            var other = (ExpansionMissionEventHeliCrash)otherBase;

            return MakeCrashAreaPVP == other.MakeCrashAreaPVP &&
                   ShowCrashMapMarker == other.ShowCrashMapMarker &&
                   UseLootFramework == other.UseLootFramework &&
                   ShowNotifications == other.ShowNotifications &&
                   Height == other.Height &&
                   Speed == other.Speed &&
                   Equals(CrashLocation, other.CrashLocation) &&
                   RewardTables.SequenceEqual(other.RewardTables) &&
                   ItemCount == other.ItemCount &&
                   InfectedCount == other.InfectedCount &&
                   Infected.SequenceEqual(other.Infected) &&
                   Loot.SequenceEqual(other.Loot); ;
        }
        public override ExpansionMissionEventBase Clone()
        {
            var clone = new ExpansionMissionEventHeliCrash
            {
                m_Version = m_Version,
                Enabled = Enabled,
                Weight = Weight,
                MissionMaxTime = MissionMaxTime,
                MissionName = MissionName,
                Difficulty = Difficulty,
                Objective = Objective,
                Reward = Reward,

                MakeCrashAreaPVP = MakeCrashAreaPVP,
                ShowCrashMapMarker = ShowCrashMapMarker,
                UseLootFramework = UseLootFramework,
                ShowNotifications = ShowNotifications,
                Height = Height,
                Speed = Speed,
                CrashLocation = CrashLocation?.Clone(),
                RewardTables = RewardTables != null
                    ? new BindingList<string>(RewardTables.ToList())
                    : new BindingList<string>(),
                ItemCount = ItemCount,
                InfectedCount = InfectedCount,

                Infected = Infected != null
                    ? new BindingList<string>(Infected.ToList())
                    : new BindingList<string>(),

                Loot = Loot != null
                    ? new BindingList<ExpansionLoot>(Loot.Select(li => li.Clone()).ToList())
                    : new BindingList<ExpansionLoot>(),

            };

            clone.SetPath(_path);
            clone.SetGuid(Id);
            return clone;
        }
    }
    public class ExpansionMissionEventHorde : ExpansionMissionEventBase
    {
        public const int VERSION = 0;

        [JsonPropertyOrder(100)]
        public int? MinInfectedAmount { get; set; }
        [JsonPropertyOrder(101)]
        public int? MaxInfectedAmount { get; set; }
        [JsonPropertyOrder(102)]
        public Vec3 Position { get; set; }
        [JsonPropertyOrder(103)]
        public decimal? MinimumSpawnRadius { get; set; }
        [JsonPropertyOrder(104)]
        public decimal? MaximumSpawnRadius { get; set; }
        [JsonPropertyOrder(105)]
        public BindingList<BehaviourGroupInfectedPackWaypointParams> WayPoints { get; set; }

        public ExpansionMissionEventHorde()
        {
        }
        public ExpansionMissionEventHorde(string missionName, Vec3 position)
        {
            m_Version = VERSION;
            Enabled = 1;
            Weight = 5m;
            MissionMaxTime = 120;
            MissionName = missionName;
            Difficulty = 0;
            Objective = 0;
            Reward = "";
            MinInfectedAmount = 10;
            MaxInfectedAmount = 20;
            Position = position;
            MinimumSpawnRadius = 10m;
            MaximumSpawnRadius = 20m;
            WayPoints = new BindingList<BehaviourGroupInfectedPackWaypointParams>
            {
                new BehaviourGroupInfectedPackWaypointParams { m_CenterPosition = position.Clone(), m_Radius = 40m }
            };
        }
        protected override bool EqualsCore(ExpansionMissionEventBase otherBase)
        {
            if (!base.EqualsCore(otherBase)) return false;

            var other = (ExpansionMissionEventHorde)otherBase;

            return MinInfectedAmount == other.MinInfectedAmount &&
                   MaxInfectedAmount == other.MaxInfectedAmount &&
                   Equals(Position, other.Position) &&
                   MinimumSpawnRadius == other.MinimumSpawnRadius &&
                   MaximumSpawnRadius == other.MaximumSpawnRadius &&
                   WayPoints.SequenceEqual(other.WayPoints);
        }
        public override ExpansionMissionEventBase Clone()
        {
            var clone = new ExpansionMissionEventHorde
            {
                m_Version = m_Version,
                Enabled = Enabled,
                Weight = Weight,
                MissionMaxTime = MissionMaxTime,
                MissionName = MissionName,
                Difficulty = Difficulty,
                Objective = Objective,
                Reward = Reward,

                MinInfectedAmount = MinInfectedAmount,
                MaxInfectedAmount = MaxInfectedAmount,
                Position = Position?.Clone(),
                MinimumSpawnRadius = MinimumSpawnRadius,
                MaximumSpawnRadius = MaximumSpawnRadius,
                WayPoints = WayPoints != null
                    ? new BindingList<BehaviourGroupInfectedPackWaypointParams>(WayPoints.Select(w => w.Clone()).ToList())
                    : new BindingList<BehaviourGroupInfectedPackWaypointParams>(),
            };

            clone.SetPath(_path);
            clone.SetGuid(Id);
            return clone;
        }
        public override List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (m_Version != VERSION)
            {
                fixes.Add($"Updated version from {m_Version} to {VERSION}");
                m_Version = VERSION;
            }
            if (Enabled == null || (Enabled != 0 && Enabled != 1))
            {
                Enabled = 1;
                fixes.Add("Corrected Enabled to 1");
            }
            if (Weight == null)
            {
                Weight = 5m;
                fixes.Add("Set default Weight to 5.0");
            }
            if (MissionMaxTime == null)
            {
                MissionMaxTime = 120;
                fixes.Add("Set default MissionMaxTime to 120");
            }
            if (MissionName == null)
            {
                MissionName = "Horde";
                fixes.Add("Set default MissionName to Horde");
            }
            if (Difficulty == null)
            {
                Difficulty = 0;
                fixes.Add("Set default Difficulty to 0");
            }
            if (Objective == null)
            {
                Objective = 0;
                fixes.Add("Set default Objective to 0");
            }
            if (Reward == null)
            {
                Reward = "";
                fixes.Add("Set default Reward to empty");
            }
            if (MinInfectedAmount == null || MinInfectedAmount < 0)
            {
                MinInfectedAmount = 10;
                fixes.Add("Set default MinInfectedAmount to 10");
            }
            if (MaxInfectedAmount == null || MaxInfectedAmount < MinInfectedAmount)
            {
                MaxInfectedAmount = Math.Max(20, (int)MinInfectedAmount);
                fixes.Add($"Set MaxInfectedAmount to {MaxInfectedAmount}");
            }
            if (Position == null)
            {
                Position = new Vec3(0f, 0f, 0f);
                fixes.Add("Set default Position to 0 0 0");
            }
            if (MinimumSpawnRadius == null || MinimumSpawnRadius < 0)
            {
                MinimumSpawnRadius = 10m;
                fixes.Add("Set default MinimumSpawnRadius to 10");
            }
            if (MaximumSpawnRadius == null || MaximumSpawnRadius < MinimumSpawnRadius)
            {
                MaximumSpawnRadius = Math.Max(20m, (decimal)MinimumSpawnRadius);
                fixes.Add($"Set MaximumSpawnRadius to {MaximumSpawnRadius}");
            }
            if (WayPoints == null)
            {
                WayPoints = new BindingList<BehaviourGroupInfectedPackWaypointParams>();
                fixes.Add("Initialised WayPoints");
            }
            foreach (var waypoint in WayPoints)
            {
                if (waypoint.m_CenterPosition == null)
                {
                    waypoint.m_CenterPosition = Position.Clone();
                    fixes.Add("Set missing waypoint position to mission Position");
                }
                if (waypoint.m_Radius == null || waypoint.m_Radius < 0)
                {
                    waypoint.m_Radius = 40m;
                    fixes.Add("Set waypoint radius to 40");
                }
            }

            return fixes;
        }
    }
    public class BehaviourGroupInfectedPackWaypointParams
    {
        public Vec3 m_CenterPosition { get; set; }
        public decimal? m_Radius { get; set; }

        public override string ToString()
        {
            return m_CenterPosition == null ? "Waypoint" : $"{m_CenterPosition.X:0.##}, {m_CenterPosition.Z:0.##}";
        }
        public override bool Equals(object? obj)
        {
            if (obj is not BehaviourGroupInfectedPackWaypointParams other)
                return false;

            return Equals(m_CenterPosition, other.m_CenterPosition) &&
                   m_Radius == other.m_Radius;
        }
        public BehaviourGroupInfectedPackWaypointParams Clone()
        {
            return new BehaviourGroupInfectedPackWaypointParams
            {
                m_CenterPosition = m_CenterPosition?.Clone(),
                m_Radius = m_Radius
            };
        }
    }
}