using Day2eEditor;
using System;
using System.ComponentModel;
using System.IO;
using System.Text.Json.Serialization;

namespace ExpansionPlugin
{
    public class ExpansionHardlinePlayerDataConfig : MultiFileConfigLoaderBase<ExpansionHardlinePlayerData>
    {
        public const int CurrentVersion = 8;
        public ExpansionHardlinePlayerDataConfig(string path) : base(path)
        {
        }
        public override void Load()
        {
            ResetState();

            var filePaths = Directory.GetFiles(BasePath, "*.bin");

            foreach (var file in filePaths)
            {
                try
                {
                    var item = LoadItem(file);
                    OnAfterItemLoad(item, file);
                    _clonedItems.Add(GetID(item), item.Clone());
                    MutableItems.Add(item);

                }
                catch (Exception ex)
                {
                    HasErrors = true;
                    HandleItemError(file, ex);
                }
            }

            OnAfterLoadAll();
        }
        protected override ExpansionHardlinePlayerData LoadItem(string filePath)
        {
            var item = new ExpansionHardlinePlayerData();
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (BinaryReader br = new BinaryReader(fs))
            {
                if (br.ReadInt32() != CurrentVersion) return null;
                item.Reputation = br.ReadInt32();
                item.factionRepCount = br.ReadInt32();
                item.FactionReputation = new BindingList<FactionReps>();
                for (int i = 0; i < item.factionRepCount; i++)
                {
                    item.FactionReputation.Add(new FactionReps(br));
                }
                item.FactionID = br.ReadInt32();
                item.PersonalStorageLevel = br.ReadInt32();
            }
            return item;
        }
        public override IEnumerable<string> Save()
        {
            var saved = new List<string>();

            for (int i = MutableItems.Count - 1; i >= 0; i--)
            {
                var item = MutableItems[i];
                var id = GetID(item);
                var fileName = GetItemFileName(item);
                var fullfielName = GetItemFilePath(item);
                if (ShouldDelete(item))
                {
                    DeleteItemFile(item);
                    MutableItems.RemoveAt(i);
                    _clonedItems.Remove(id);
                    saved.Add("File Remove " + fullfielName);
                    continue;
                }

                if (!_clonedItems.TryGetValue(id, out var baseline))
                {
                    SaveItem(item);
                    _clonedItems[id] = CloneItem(item);
                    saved.Add(fullfielName);
                    continue;
                }

                if (!AreEqual(item, baseline))
                {
                    SaveItem(item);
                    if (GetItemFilePath(_clonedItems[id]) != GetItemFilePath(item))
                    {
                        if (File.Exists(GetItemFilePath(_clonedItems[id])))
                            File.Delete(GetItemFilePath(_clonedItems[id]));
                    }
                    _clonedItems[id] = CloneItem(item);
                    saved.Add(fullfielName);
                }
            }
            return saved;
        }
        protected override void SaveItem(ExpansionHardlinePlayerData item)
        {
            using (FileStream fs = new FileStream(item.FilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                bw.Write(CurrentVersion);
                bw.Write(item.Reputation);
                bw.Write(item.FactionReputation.Count());
                foreach (FactionReps fr in item.FactionReputation)
                {
                    bw.Write(fr.FactionID);
                    bw.Write(fr.FactionRep);
                }
                bw.Write(item.FactionID);
                bw.Write(item.PersonalStorageLevel);
            }
        }
        protected override void DeleteItemFile(ExpansionHardlinePlayerData item)
        {
            if (!string.IsNullOrWhiteSpace(item._path) && File.Exists(item._path))
            {
                File.Delete(item._path);
            }
        }
        protected override Guid GetID(ExpansionHardlinePlayerData item) => item.Id;
        protected override string GetItemFileName(ExpansionHardlinePlayerData item) => item.FileName;
        protected override string GetItemFilePath(ExpansionHardlinePlayerData item) => item.FilePath;
        protected override bool ShouldDelete(ExpansionHardlinePlayerData item)
            => item.ToDelete;
    }
    public class ExpansionHardlinePlayerData : IDeepCloneable<ExpansionHardlinePlayerData>, IEquatable<ExpansionHardlinePlayerData>
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

        public ExpansionHardlinePlayerData Clone()
        {
            ExpansionHardlinePlayerData clone = new ExpansionHardlinePlayerData
            {
                CONFIGVERSION = this.CONFIGVERSION,
                Reputation = this.Reputation,
                FactionReputation = this.FactionReputation != null
                    ? new BindingList<FactionReps>(this.FactionReputation.Select(StockItem => StockItem.Clone()).ToList())
                    : new BindingList<FactionReps>(),
                FactionID = this.FactionID,
                PersonalStorageLevel = this.PersonalStorageLevel
            };
            return clone;
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionHardlinePlayerData);
        public bool Equals(ExpansionHardlinePlayerData other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (Id != other.Id) return false;

            if (_path != other._path) return false;
            if (CONFIGVERSION != other.CONFIGVERSION) return false;
            if (Reputation != other.Reputation) return false;
            if (!Helper.ListEquals(FactionReputation, other.FactionReputation)) return false;
            if (FactionID != other.FactionID) return false;
            if (PersonalStorageLevel != other.PersonalStorageLevel) return false;

            return true;
        }

        public void SetPath(string path) => _path = path;
        internal void SetGuid(Guid guid) => Id = guid;

        public int CONFIGVERSION { get; set; }
        public int Reputation { get; set; }
        public int factionRepCount { get; set; }
        public BindingList<FactionReps> FactionReputation { get; set; }
        public int FactionID { get; set; }
        public int PersonalStorageLevel { get; set; }
    }
    public class FactionReps : IDeepCloneable<FactionReps>, IEquatable<FactionReps>
    {
        public int FactionID;
        public int FactionRep;

        public FactionReps() { }
        public FactionReps(BinaryReader br)
        {
            FactionID = br.ReadInt32();
            FactionRep = br.ReadInt32();
        }

        public void SetFactionReputation(int value)
        {
            FactionRep = value;
        }

        public override string ToString()
        {
            return FactionID.ToString();
        }
        public override bool Equals(object? obj) => Equals(obj as FactionReps);

        public FactionReps Clone()
        {
            FactionReps clone = new FactionReps
            {
                FactionID = this.FactionID,
                FactionRep = this.FactionRep
            };
            return clone;
        }

        public bool Equals(FactionReps other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (FactionID != other.FactionID) return false;
            if (FactionRep != other.FactionRep) return false;
            

            return true;
        }
    }
}
