using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpansionPlugin
{
    public class ExpansionLoadoutConfig : MultiFileConfigLoaderBase<ExpansionPrefabObject>
    {
        public ExpansionLoadoutConfig(string path) : base(path)
        {
        }
        protected override ExpansionPrefabObject LoadItem(string filePath)
        {
            var item = AppServices.GetRequired<FileService>().LoadOrCreateJson(
                    filePath,
                    createNew: () => new ExpansionPrefabObject(),
                    onError: ex => HandleItemError(filePath, ex),
                    configName: "Loadout",
                    useBoolConvertor: true
                );

            item.SetPath(filePath);
            item.SetGuid(Guid.NewGuid());
            return item;
        }
        protected override void SaveItem(ExpansionPrefabObject item)
        {
            AppServices.GetRequired<FileService>().SaveJson(item._path, item);
        }
        protected override string GetItemFileName(ExpansionPrefabObject AILoadouts)
            => AILoadouts.FileName;
        protected override string GetItemFilePath(ExpansionPrefabObject AILoadouts)
           => AILoadouts.FilePath;
        protected override bool ShouldDelete(ExpansionPrefabObject item)
            => item.ToDelete;
        protected override Guid GetID(ExpansionPrefabObject item)
            => item.Id;
        protected override void DeleteItemFile(ExpansionPrefabObject item)
        {
            if (!string.IsNullOrWhiteSpace(item._path) && File.Exists(item._path))
                File.Delete(item._path);
        }
        internal bool AddNewLoadoutFile(ExpansionPrefabObject newAILoadouts)
        {
            bool exists = Items.Any(ld => ld.FileName.ToLower() == newAILoadouts.FileName.ToLower());

            if (exists)
                return false; // File with same name already exists

            MutableItems.Add(newAILoadouts);
            return true;

        }
        internal void RemoveFile(ExpansionPrefabObject AILoadouts)
        {
            AILoadouts.ToDelete = true;
        }
        public bool needToSave()
        {
            return false;
        }
    }
    public class ExpansionPrefabObject : IDeepCloneable<ExpansionPrefabObject>, IEquatable<ExpansionPrefabObject>
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
        public ExpansionMinMax Quantity { get; set; }
        public BindingList<ExpansionHealth> Health { get; set; }
        public BindingList<ExpansionPrefabSlot> InventoryAttachments { get; set; }
        public BindingList<ExpansionPrefabObject> InventoryCargo { get; set; }
        public BindingList<string> ConstructionPartsBuilt { get; set; }
        public BindingList<ExpansionPrefabObject> Sets { get; set; }

        public ExpansionPrefabObject()
        {
            ClassName = "";
            Include = "";
            Chance = (decimal)1;
            Quantity = new ExpansionMinMax();
            Health = new BindingList<ExpansionHealth>();
            InventoryAttachments = new BindingList<ExpansionPrefabSlot>();
            InventoryCargo = new BindingList<ExpansionPrefabObject>();
            ConstructionPartsBuilt = new BindingList<string>();
            Sets = new BindingList<ExpansionPrefabObject>();
        }

        public bool Equals(ExpansionPrefabObject other)
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
        public override bool Equals(object? obj) => Equals(obj as ExpansionPrefabObject);
        public ExpansionPrefabObject Clone()
        {
            var clone = new  ExpansionPrefabObject()
            {
                ClassName = this.ClassName,
                Include = this.Include,
                Chance = this.Chance,
                Quantity = this.Quantity.Clone(),
                Health = new BindingList<ExpansionHealth>(this.Health.Select(x => x.Clone()).ToList()),
                InventoryAttachments = new BindingList<ExpansionPrefabSlot>(this.InventoryAttachments.Select(x => x.Clone()).ToList()),
                InventoryCargo = new BindingList<ExpansionPrefabObject>(this.InventoryCargo.Select(x => x.Clone()).ToList()),
                ConstructionPartsBuilt = new BindingList<string>(this.ConstructionPartsBuilt.ToList()),
                Sets = new BindingList<ExpansionPrefabObject>(this.Sets.Select(x => x.Clone()).ToList())
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

    public class ExpansionMinMax : IDeepCloneable<ExpansionMinMax>, IEquatable<ExpansionMinMax>
    {
        public decimal Min { get; set; }
        public decimal Max { get; set; }

        public override bool Equals(object? obj) => Equals(obj as ExpansionMinMax);
        public bool Equals(ExpansionMinMax other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            
            return Min == other.Min && Max == other.Max;
        }
        public ExpansionMinMax Clone()
        {
            return new ExpansionMinMax()
            {
                Min = Min,
                Max = Max
            };
        }
    }

    public class ExpansionHealth : IDeepCloneable<ExpansionHealth>, IEquatable<ExpansionHealth>
    {
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public string Zone { get; set; }

        public ExpansionHealth()
        {
            Min = 0.7m;
            Max = 1.0m;
            Zone = "";
        }
        public override string ToString()
        {
            if (Zone == "")
                return "No Zone";
            return Zone;
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionHealth);
        public bool Equals(ExpansionHealth other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Min == other.Min && Max == other.Max && Zone == other.Zone;
        }
        public ExpansionHealth Clone()
        {
            return new ExpansionHealth()
            {
                Min = this.Min,
                Max = this.Max,
                Zone = this.Zone
            };
        }
    }

    public class ExpansionPrefabSlot : IDeepCloneable<ExpansionPrefabSlot>, IEquatable<ExpansionPrefabSlot>
    {
        public string SlotName { get; set; }
        public BindingList<ExpansionPrefabObject> Items { get; set; }

        public override string ToString()
        {
            return SlotName; ;
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionPrefabSlot);
        public bool Equals(ExpansionPrefabSlot other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return SlotName == other.SlotName &&
                   Helper.ListEquals(Items, other.Items);
        }
        public ExpansionPrefabSlot Clone()
        {
            return new ExpansionPrefabSlot()
            {
                SlotName = this.SlotName,
                Items = new BindingList<ExpansionPrefabObject>(this.Items.Select(x => x.Clone()).ToList())
            };
        }

    }
}
