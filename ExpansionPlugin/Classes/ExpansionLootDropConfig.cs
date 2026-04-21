using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionLootDropConfig : MultiFileConfigLoaderBase<AILootDrops>
    {
        public ExpansionLootDropConfig(string path) : base(path)
        {
        }
        protected override AILootDrops LoadItem(string filePath)
        {
            var item = AppServices.GetRequired<FileService>().LoadOrCreateJson<BindingList<AILoadouts>>(
                    filePath,
                    createNew: () => new BindingList<AILoadouts>(),
                    onError: ex => HandleItemError(filePath, ex),
                    configName: "LootDrop"
                );
            AILootDrops drops = new AILootDrops
            {
                LootdropList = item
            };
            drops.SetPath(filePath);
            drops.SetGuid(Guid.NewGuid());
            return drops;
        }
        public override IEnumerable<string> Save()
        {
            var saved = new List<string>();

            for (int i = MutableItems.Count - 1; i >= 0; i--)
            {
                var item = MutableItems[i];
                var id = GetID(item);
                var fileName = GetItemFileName(item);
                var fullfielName = item.FilePath;

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
                    _clonedItems[id] = item.Clone();
                    saved.Add(fullfielName);
                    continue;
                }

                if (!item.Equals(baseline))
                {
                    var oldPath = baseline.FilePath;

                    SaveItem(item);

                    if (!string.Equals(oldPath, item.FilePath, StringComparison.OrdinalIgnoreCase) &&
                        !string.IsNullOrWhiteSpace(oldPath) &&
                        File.Exists(oldPath))
                    {
                        File.Delete(oldPath);
                    }

                    _clonedItems[id] = item.Clone();
                    saved.Add(fullfielName);
                }
            }

            return saved;
        }
        protected override void SaveItem(AILootDrops item)
        {
            AppServices.GetRequired<FileService>().SaveJson(item._path, item.LootdropList);
        }
        protected override string GetItemFileName(AILootDrops item)
            => item.FileName;

        protected override bool ShouldDelete(AILootDrops item)
            => item.ToDelete;
        protected override Guid GetID(AILootDrops item)
            => item.Id;
        protected override void DeleteItemFile(AILootDrops item)
        {
            if (!string.IsNullOrWhiteSpace(item._path) && File.Exists(item._path))
                File.Delete(item._path);
        }

        internal bool AddNewLootDropFile(AILootDrops newAILootDrops)
        {
            bool exists = Items.Any(ld => ld.FileName.ToLower() == newAILootDrops.FileName.ToLower());

            if (exists)
                return false; // File with same name already exists

            MutableItems.Add(newAILootDrops);
            return true;
        }
        internal void RemoveFile(AILootDrops AILootDrops)
        {
            AILootDrops.ToDelete = true;
        }
        public bool needToSave()
        {
            return false;
        }
    }
    public class AILootDrops : IDeepCloneable<AILootDrops>, IEquatable<AILootDrops>
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

        public AILootDrops()
        {
            LootdropList = new BindingList<AILoadouts>();
        }
        public AILootDrops(string name)
        {
            name = name;
            LootdropList = new BindingList<AILoadouts>();
        }
        public BindingList<AILoadouts> LootdropList { get; set; }
        public override string ToString()
        {
            return FileName;
        }
        public bool Equals(AILootDrops other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return LootdropList.SequenceEqual(other.LootdropList);
        }
        public override bool Equals(object? obj) => Equals(obj as AILootDrops);
        public AILootDrops Clone()
        {
            return new AILootDrops()
            {
                LootdropList = new BindingList<AILoadouts>(this.LootdropList.Select(x => x.Clone()).ToList())
            };
        }
    }
}
