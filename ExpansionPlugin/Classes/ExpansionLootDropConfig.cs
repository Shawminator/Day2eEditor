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
    public class ExpansionLootDropConfig : MultiFileConfigLoader<AILootDrops>
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
            return drops;
        }

        protected override void SaveItem(AILootDrops item)
        {
            AppServices.GetRequired<FileService>().SaveJson(item._path, item);
        }
        protected override string GetItemFileName(AILootDrops item)
            => item.FileName;

        protected override bool ShouldDelete(AILootDrops item)
            => item.ToDelete;

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

            Items.Add(newAILootDrops);
            return true;
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
        public bool isDirty { get; set; }
        [JsonIgnore]
        public bool ToDelete { get; set; }

        public void SetPath(string path) => _path = path;

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
        internal IEnumerable<string> Save()
        {
            if (ToDelete)
            {
                if (File.Exists(_path))
                {
                    File.Delete(_path);
                    // Delete empty directories if needed
                    //ShellHelper.DeleteEmptyFoldersUpToBase(Path.GetDirectoryName(_path), AppServices.GetRequired<EconomyManager>().basePath);
                    return new[] { FileName + " (deleted)" };
                }
                return Array.Empty<string>();
            }

            else if (isDirty)
            {
                AppServices.GetRequired<FileService>().SaveJson(_path, LootdropList);
                isDirty = false;
                return new[] { FileName };
            }

            return Array.Empty<string>();
        }

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
