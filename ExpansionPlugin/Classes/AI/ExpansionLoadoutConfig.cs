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
    public class ExpansionLoadoutConfig : MultiFileConfigLoaderBase<AILoadouts>
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
            item.SetGuid(Guid.NewGuid());
            return item;
        }
        protected override void SaveItem(AILoadouts item)
        {
            AppServices.GetRequired<FileService>().SaveJson(item._path, item);
        }
        protected override string GetItemFileName(AILoadouts AILoadouts)
            => AILoadouts.FileName;
        protected override string GetItemFilePath(AILoadouts AILoadouts)
           => AILoadouts.FilePath;
        protected override bool ShouldDelete(AILoadouts item)
            => item.ToDelete;
        protected override Guid GetID(AILoadouts item)
            => item.Id;
        protected override void DeleteItemFile(AILoadouts item)
        {
            if (!string.IsNullOrWhiteSpace(item._path) && File.Exists(item._path))
                File.Delete(item._path);
        }
        internal bool AddNewLoadoutFile(AILoadouts newAILoadouts)
        {
            bool exists = Items.Any(ld => ld.FileName.ToLower() == newAILoadouts.FileName.ToLower());

            if (exists)
                return false; // File with same name already exists

            MutableItems.Add(newAILoadouts);
            return true;

        }
        internal void RemoveFile(AILoadouts AILoadouts)
        {
            AILoadouts.ToDelete = true;
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
        public string FilePath => _path;
        [JsonIgnore]
        public bool ToDelete { get; set; }
        [JsonIgnore]
        public Guid Id { get; set; }

        public void SetPath(string path) => _path = path;
        internal void SetGuid(Guid guid) => Id = guid;
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

        public bool Equals(AILoadouts other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return _path == other._path &&
                   ClassName == other.ClassName &&
                   Include == other.Include &&
                   Chance == other.Chance &&
                   Quantity.Equals(other.Quantity) &&
                   Helper.ListEquals(Health, other.Health) &&
                   Helper.ListEquals(InventoryAttachments, other.InventoryAttachments) &&
                   Helper.ListEquals(InventoryCargo, other.InventoryCargo) &&
                   Helper.ListEquals(ConstructionPartsBuilt, other.ConstructionPartsBuilt) &&
                   Helper.ListEquals(Sets, other.Sets) &&
                   Id == other.Id;
        }
        public override bool Equals(object? obj) => Equals(obj as AILoadouts);
        public AILoadouts Clone()
        {
            var clone = new  AILoadouts()
            {
                ClassName = this.ClassName,
                Include = this.Include,
                Chance = this.Chance,
                Quantity = this.Quantity.Clone(),
                Health = new BindingList<Health>(this.Health.Select(x => x.Clone()).ToList()),
                InventoryAttachments = new BindingList<Inventoryattachment>(this.InventoryAttachments.Select(x => x.Clone()).ToList()),
                InventoryCargo = new BindingList<AILoadouts>(this.InventoryCargo.Select(x => x.Clone()).ToList()),
                ConstructionPartsBuilt = new BindingList<object>(this.ConstructionPartsBuilt.ToList()),
                Sets = new BindingList<AILoadouts>(this.Sets.Select(x => x.Clone()).ToList())
            };
            clone.SetPath(_path);
            clone.SetGuid(Id);
            return clone;
        }


        public override string ToString()
        {
            if (ClassName == "")
                return FileName;

            return ClassName;
        }
    }

    public class Quantity : IDeepCloneable<Quantity>, IEquatable<Quantity>
    {
        public decimal Min { get; set; }
        public decimal Max { get; set; }

        public override bool Equals(object? obj) => Equals(obj as Quantity);
        public bool Equals(Quantity other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            
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

    public class Health : IDeepCloneable<Health>, IEquatable<Health>
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
        public override bool Equals(object? obj) => Equals(obj as Health);
        public bool Equals(Health other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

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

    public class Inventoryattachment : IDeepCloneable<Inventoryattachment>, IEquatable<Inventoryattachment>
    {
        public string SlotName { get; set; }
        public BindingList<AILoadouts> Items { get; set; }

        public override string ToString()
        {
            return SlotName; ;
        }
        public override bool Equals(object? obj) => Equals(obj as Inventoryattachment);
        public bool Equals(Inventoryattachment other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return SlotName == other.SlotName &&
                   Helper.ListEquals(Items, other.Items);
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
