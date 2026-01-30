using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpansionPlugin
{
    public class ExpansionLoadoutConfig : MultiFileConfigLoader<AILoadouts>
    {
        public ExpansionLoadoutConfig(string path) : base(path)
        {
        }
        protected override AILoadouts LoadItem(string filePath)
        {
            var item = AppServices.GetRequired<FileService>().LoadOrCreateJson(
                    filePath,
                    createNew: () => new AILoadouts(),
                    onError: ex => HandleItemError(filePath, ex),
                    configName: "Loadout",
                    useBoolConvertor: true
                );

            item.SetPath(filePath);
            return item;
        }

        protected override void SaveItem(AILoadouts item)
        {
            AppServices.GetRequired<FileService>().SaveJson(item._path, item);
        }
        protected override string GetItemFileName(AILoadouts item)
            => item.FileName;

        protected override bool ShouldDelete(AILoadouts item)
            => item.ToDelete;

        protected override void DeleteItemFile(AILoadouts item)
        {
            if (!string.IsNullOrWhiteSpace(item._path) && File.Exists(item._path))
                File.Delete(item._path);
        }


        internal bool AddNewLoadoutFile(AILoadouts newAILoadouts)
        {
            bool exists = AllData.Any(ld => ld.FileName.ToLower() == newAILoadouts.FileName.ToLower());

            if (exists)
                return false; // File with same name already exists

            AllData.Add(newAILoadouts);
            return true;

        }
        internal void RemoveFile(AILoadouts aILoadouts)
        {
            AllData.Remove(aILoadouts);

        }

        public bool needToSave()
        {
            return false;
        }
    }
    public class AILoadouts : IDeepCloneable<AILoadouts>, IEquatable<AILoadouts>
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

        public string ClassName { get; set; }
        public string Include { get; set; }
        public decimal Chance { get; set; }
        public Quantity Quantity { get; set; }
        public BindingList<Health> Health { get; set; }
        public BindingList<Inventoryattachment> InventoryAttachments { get; set; }
        public BindingList<AILoadouts> InventoryCargo { get; set; }
        public BindingList<object> ConstructionPartsBuilt { get; set; }
        public BindingList<AILoadouts> Sets { get; set; }

        public AILoadouts()
        {
            ClassName = "";
            Include = "";
            Chance = (decimal)1;
            Quantity = new Quantity();
            Health = new BindingList<Health>();
            InventoryAttachments = new BindingList<Inventoryattachment>();
            InventoryCargo = new BindingList<AILoadouts>();
            ConstructionPartsBuilt = new BindingList<object>();
            Sets = new BindingList<AILoadouts>();
        }
        public AILoadouts(string _name)
        {
            name = _name;
            ClassName = "";
            Include = "";
            Chance = (decimal)1;
            Quantity = new Quantity();
            Health = new BindingList<Health>();
            InventoryAttachments = new BindingList<Inventoryattachment>();
            InventoryCargo = new BindingList<AILoadouts>();
            ConstructionPartsBuilt = new BindingList<object>();
            Sets = new BindingList<AILoadouts>();
        }

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
                AppServices.GetRequired<FileService>().SaveJson(_path, this);
                isDirty = false;
                return new[] { FileName };
            }

            return Array.Empty<string>();
        }



        public bool Equals(AILoadouts other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return ClassName == other.ClassName &&
                   Include == other.Include &&
                   Chance == other.Chance &&
                   Equals(Quantity, other.Quantity) &&
                   Health.SequenceEqual(other.Health) &&
                   InventoryAttachments.SequenceEqual(other.InventoryAttachments) &&
                   InventoryCargo.SequenceEqual(other.InventoryCargo) &&
                   ConstructionPartsBuilt.SequenceEqual(other.ConstructionPartsBuilt) &&
                   Sets.SequenceEqual(other.Sets);
        }
        public override bool Equals(object? obj) => Equals(obj as AILoadouts);
        public AILoadouts Clone()
        {
            return new AILoadouts()
            {
                ClassName = this.ClassName,
                Include = this.Include,
                Chance = this.Chance,
                InventoryAttachments = new BindingList<Inventoryattachment>(this.InventoryAttachments.Select(x => x.Clone()).ToList()),
                InventoryCargo = new BindingList<AILoadouts>(this.InventoryCargo.Select(x => x.Clone()).ToList()),
                ConstructionPartsBuilt = new BindingList<object>(this.ConstructionPartsBuilt.ToList()),
                Sets = new BindingList<AILoadouts>(this.Sets.Select(x => x.Clone()).ToList()),
                _path = this.FilePath
            };
        }


        public override string ToString()
        {
            if (ClassName == "")
                return FileName;

            return ClassName;
        }
    }

    public class Quantity
    {
        public decimal Min { get; set; }
        public decimal Max { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is not Quantity other) return false;
            return Min == other.Min && Max == other.Max;
        }
        public Quantity Clone()
        {
            return new Quantity()
            {
                Min = Min,
                Max = Max
            };
        }
    }

    public class Health
    {
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public string Zone { get; set; }

        public override string ToString()
        {
            if (Zone == "")
                return "No Zone";
            return Zone;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Health other) return false;
            return Min == other.Min && Max == other.Max && Zone == other.Zone;
        }
        public Health Clone()
        {
            return new Health()
            {
                Min = this.Min,
                Max = this.Max,
                Zone = this.Zone
            };
        }
    }

    public class Inventoryattachment
    {
        public string SlotName { get; set; }
        public BindingList<AILoadouts> Items { get; set; }

        public override string ToString()
        {
            return SlotName; ;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Inventoryattachment other) return false;
            return SlotName == other.SlotName &&
                   Items.SequenceEqual(other.Items);
        }
        public Inventoryattachment Clone()
        {
            return new Inventoryattachment()
            {
                SlotName = this.SlotName,
                Items = new BindingList<AILoadouts>(this.Items.Select(x => x.Clone()).ToList())
            };
        }

    }
}
