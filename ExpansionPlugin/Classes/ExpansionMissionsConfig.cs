using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Channels;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpansionPlugin
{
    public class ExpansionMissionsConfig : MultiFileConfigLoader<ExpansionMissionEventBase>
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
                typeof(ExpansionMissionEventBase);

            var fileService = AppServices.GetRequired<FileService>();

            var item = (ExpansionMissionEventBase)fileService.LoadOrCreateJson(
                path: filePath,
                createNew: () => Activator.CreateInstance(actualType)!,
                onError: ex => HandleItemError(filePath, ex),
                configName: "Mission",
                useBoolConvertor: true,
                targetType: actualType
            );

            item.SetPath(filePath);

            return item;
        }
        protected override void SaveItem(ExpansionMissionEventBase item)
        {
            AppServices.GetRequired<FileService>().SaveJson(item._path, item);
        }
        protected override string GetItemFileName(ExpansionMissionEventBase item)
            => item.FileName;
        protected override bool ShouldDelete(ExpansionMissionEventBase item)
            => item.ToDelete;
        protected override void DeleteItemFile(ExpansionMissionEventBase item)
        {
            if (!string.IsNullOrWhiteSpace(item._path) && File.Exists(item._path))
                File.Delete(item._path);
        }
        internal bool AddNewMissionFile(ExpansionMissionEventBase newExpansionMissionEventBase)
        {
            bool exists = Items.Any(ld => ld.FileName.ToLower() == newExpansionMissionEventBase.FileName.ToLower());

            if (exists)
                return false;

            Items.Add(newExpansionMissionEventBase);
            return true;

        }
        internal void RemoveFile(ExpansionMissionEventBase ExpansionMissionEventBase)
        {
            Items.Remove(ExpansionMissionEventBase);
        }
        public bool needToSave()
        {
            return false;
        }
    }
    public class ExpansionMissionEventBase : IDeepCloneable<ExpansionMissionEventBase>, IEquatable<ExpansionMissionEventBase>
    {
        [JsonIgnore]
        public string _path { get; private set; }
        [JsonIgnore]
        public string FileName => Path.GetFileName(_path);
        [JsonIgnore]
        public bool isDirty { get; set; }
        [JsonIgnore]
        public bool ToDelete { get; set; }

        public void SetPath(string path) => _path = path;

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
        internal IEnumerable<string> Save()
        {
            if (ToDelete)
            {
                if (File.Exists(_path))
                {
                    File.Delete(_path);
                    return new[] { FileName + " (deleted)" };
                }
                return Array.Empty<string>();
            }

            else if (isDirty)
            {
                AppServices.GetRequired<FileService>().SaveJson(_path, this);
                isDirty = false;
                return new[] { FileName };
            }

            return Array.Empty<string>();
        }
        public bool Equals(ExpansionMissionEventBase other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return m_Version == other.m_Version &&
                   Enabled == other.Enabled &&
                   Weight == other.Weight &&
                   MissionMaxTime == other.MissionMaxTime &&
                   MissionName == other.MissionName &&
                   Difficulty == other.Difficulty &&
                   Objective == other.Objective &&
                   Reward == other.Reward;
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionMissionEventBase);
        public ExpansionMissionEventBase Clone()
        {
            return new ExpansionMissionEventBase()
            {
                m_Version = this.m_Version,
                Enabled = this.Enabled,
                Weight = this.Weight,
                MissionMaxTime = this.MissionMaxTime,
                MissionName = this.MissionName,
                Difficulty = this.Difficulty,
                Objective = this.Objective,
                Reward = this.Reward,
                _path = this._path
            };
        }
    }
    public class ExpansionMissionEventAirdrop : ExpansionMissionEventBase
    {
        const int VERSION = 3;

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

    }
    public class ExpansionAirdropLocation
    {
        public decimal? x { get; set; }
        public decimal? z { get; set; }
        public string? Name { get; set; }
        public decimal? Radius { get; set; }

        public ExpansionAirdropLocation() { }
    };
    public class ExpansionMissionEventContaminatedArea : ExpansionMissionEventBase
    {
        public Data Data { get; set; }
        public PlayerData PlayerData { get; set; }

        public decimal? StartDecayLifetime { get; set; }
        public decimal? FinishDecayLifetime { get;set; }
    }
}