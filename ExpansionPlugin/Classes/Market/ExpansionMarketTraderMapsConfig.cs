using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExpansionPlugin
{
    public class ExpansionMarketTraderMapsConfig : MultiFileConfigLoaderBase<ExpansionMarketTraderNpcs>
    {
        public ExpansionMarketTraderMapsConfig(string path) : base(path)
        {
        }
        public override void Load()
        {
            ResetState();

            var filePaths = Directory.GetFiles(BasePath, "*.map");

            foreach (var file in filePaths)
            {
                try
                {
                    var item = LoadItem(file);

                    OnAfterItemLoad(item, file);
                    _clonedItems.Add(GetID(item), item.Clone());
                    //var issues = ValidateData(item);
                    //if (issues?.Any() == true)
                    //{
                    //    Console.WriteLine("Validation issues in " + FileName + ":");
                    //    foreach (var msg in issues)
                    //        Console.WriteLine("- " + msg);
                    //}
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
        protected override ExpansionMarketTraderNpcs LoadItem(string filePath)
        {
            ExpansionMarketTraderNpcs ExpansionMarketTraderNpcs = new ExpansionMarketTraderNpcs();
            foreach (string line in File.ReadLines(filePath))
            {
                if (Helpers.GetTraderFromMissionFile(line,
                        out string npcClass,
                        out string traderName,
                        out BindingList<Vec3> positions,
                        out Vec3 rotation,
                        out List<TraderNPCItem> items,
                        out TraderNPCSpecialProperties special))
                {
                    var trader = new ExpansionTraderMaps
                    {
                        NpcClassName = npcClass,
                        TraderName = traderName,
                        Positions = positions,
                        Rotation = rotation,
                        Items = items,
                        Special = special
                    };

                    ExpansionMarketTraderNpcs.Tradersmaps.Add(trader);
                }
            }
            ExpansionMarketTraderNpcs.SetPath(filePath);
            ExpansionMarketTraderNpcs.SetGuid(Guid.NewGuid());  //Runtime Only
            return ExpansionMarketTraderNpcs;
        }
        protected override void SaveItem(ExpansionMarketTraderNpcs ExpansionMarketTrader)
        {
            AppServices.GetRequired<FileService>().SaveJson(ExpansionMarketTrader._path, ExpansionMarketTrader, false, true);
        }
        protected override bool ShouldDelete(ExpansionMarketTraderNpcs ExpansionMarketTraderNpcs)
            => ExpansionMarketTraderNpcs.ToDelete;
        protected override void DeleteItemFile(ExpansionMarketTraderNpcs ExpansionMarketTraderNpcs)
        {
            if (!string.IsNullOrWhiteSpace(ExpansionMarketTraderNpcs._path) && File.Exists(ExpansionMarketTraderNpcs._path))
            {
                File.Delete(ExpansionMarketTraderNpcs._path);
            }
        }
        protected override Guid GetID(ExpansionMarketTraderNpcs ExpansionMarketTraderNpcs)
            => ExpansionMarketTraderNpcs.Id;
        protected override string GetItemFileName(ExpansionMarketTraderNpcs ExpansionMarketTraderNpcs)
            => ExpansionMarketTraderNpcs.FileName;
        protected override string GetItemFilePath(ExpansionMarketTraderNpcs ExpansionMarketTraderNpcs)
            => ExpansionMarketTraderNpcs.FilePath;
    }
    public class ExpansionMarketTraderNpcs : IDeepCloneable<ExpansionMarketTraderNpcs>, IEquatable<ExpansionMarketTraderNpcs>
    {
        public string _path { get; private set; }
        public string FileName => Path.GetFileName(_path);
        public string FilePath => _path;
        public bool ToDelete { get; set; }
        public Guid Id { get; set; }
        public void SetPath(string path) => _path = path;
        internal void SetGuid(Guid guid) => Id = guid;
        public BindingList<ExpansionTraderMaps> Tradersmaps { get; set; }


        public ExpansionMarketTraderNpcs()
        {
            Tradersmaps = new BindingList<ExpansionTraderMaps>();
        }

        public override bool Equals(object? obj) => Equals(obj as ExpansionMarketTraderNpcs);
        public bool Equals(ExpansionMarketTraderNpcs? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;


            return true;
        }
        public ExpansionMarketTraderNpcs Clone()
        {
            ExpansionMarketTraderNpcs clone = new ExpansionMarketTraderNpcs
            {
            };
            return clone;
        }

    }
    public class ExpansionTraderMaps : IDeepCloneable<ExpansionTraderMaps>, IEquatable<ExpansionTraderMaps>
    {
        public string NpcClassName { get; set; } = string.Empty;
        public string TraderName { get; set; } = string.Empty;
        public bool IsAI => NpcClassName.StartsWith("ExpansionTraderAI");
        public BindingList<Vec3> Positions { get; set; } = new BindingList<Vec3>();
        public Vec3 Rotation { get; set; } = new Vec3();
        public List<TraderNPCItem> Items { get; set; } = new();
        public TraderNPCSpecialProperties Special { get; set; } = new();

        public override bool Equals(object? obj) => Equals(obj as ExpansionTraderMaps);
        public bool Equals(ExpansionTraderMaps? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (NpcClassName != other.NpcClassName) return false;
            if (TraderName != other.TraderName) return false;

            if (!Rotation.Equals(other.Rotation)) return false;

            // Positions
            if (Positions.Count != other.Positions.Count) return false;
            for (int i = 0; i < Positions.Count; i++)
            {
                if (!Positions[i].Equals(other.Positions[i]))
                    return false;
            }

            // Items (including attachments)
            if (Items.Count != other.Items.Count) return false;
            for (int i = 0; i < Items.Count; i++)
            {
                var a = Items[i];
                var b = other.Items[i];

                if (a.ClassName != b.ClassName)
                    return false;

                if (a.Attachments.Count != b.Attachments.Count)
                    return false;

                for (int j = 0; j < a.Attachments.Count; j++)
                {
                    if (a.Attachments[j] != b.Attachments[j])
                        return false;
                }
            }

            // Special properties
            if (Special.Name != other.Special.Name) return false;
            if (Special.Loadout != other.Special.Loadout) return false;
            if (Special.Faction != other.Special.Faction) return false;

            return true;
        }
        public ExpansionTraderMaps Clone()
        {
            var clone = new ExpansionTraderMaps
            {
                NpcClassName = this.NpcClassName,
                TraderName = this.TraderName,
                Rotation = new Vec3(this.Rotation.GetString()),
                Special = new TraderNPCSpecialProperties
                {
                    Name = this.Special.Name,
                    Loadout = this.Special.Loadout,
                    Faction = this.Special.Faction
                }
            };

            // Positions
            foreach (var pos in this.Positions)
            {
                clone.Positions.Add(new Vec3(pos.GetString()));
            }

            // Items + attachments
            foreach (var item in this.Items)
            {
                var newItem = new TraderNPCItem
                {
                    ClassName = item.ClassName
                };

                foreach (var att in item.Attachments)
                {
                    newItem.Attachments.Add(att);
                }

                clone.Items.Add(newItem);
            }

            return clone;
        }
    }
    public class TraderNPCItem
    {
        public string ClassName { get; set; } = string.Empty;
        public List<string> Attachments { get; set; } = new List<string>();
    }
    public class TraderNPCSpecialProperties
    {
        public string? Name { get; set; }
        public string? Loadout { get; set; }
        public string? Faction { get; set; }
    }
}
