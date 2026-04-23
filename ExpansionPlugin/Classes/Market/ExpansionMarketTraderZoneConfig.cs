using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionMarketTraderZoneConfig : MultiFileConfigLoaderBase<ExpansionMarketTraderZone>
    {
        public const int CurrentVersion = 6;
        public ExpansionMarketTraderZoneConfig(string path) : base(path)
        {
        }
        public override void Load()
        {
            ResetState();

            var filePaths = Directory.GetFiles(BasePath, "*.json");

            foreach (var file in filePaths)
            {
                try
                {
                    var item = LoadItem(file);

                    OnAfterItemLoad(item, file);
                    _clonedItems.Add(GetID(item), item.Clone());
                    var issues = ValidateItem(item);
                    if (issues?.Any() == true)
                    {
                        Console.WriteLine("Validation issues in " + FileName + ":");
                        foreach (var msg in issues)
                            Console.WriteLine("- " + msg);
                    }
                    _clonedItems[item.Id].stockList = item.stockList != null
                        ? new BindingList<ExpansionMarketTraderStockItem>(item.stockList.Select(cat => cat.Clone()).ToList())
                        : new BindingList<ExpansionMarketTraderStockItem>();
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
        protected override ExpansionMarketTraderZone LoadItem(string filePath)
        {
            var TraderZone = AppServices.GetRequired<FileService>().LoadOrCreateJson(
                    filePath,
                    createNew: () => new ExpansionMarketTraderZone(),
                    onError: ex => HandleItemError(filePath, ex),
                    configName: "MarketTraderZone",
                    useVecConvertor: true
                );

            TraderZone.SetPath(filePath);
            TraderZone.SetGuid(Guid.NewGuid());
            return TraderZone;
        }
        public override IEnumerable<string> Save()
        {
            var saved = new List<string>();

            for (int i = Items.Count - 1; i >= 0; i--)
            {
                var item = Items[i];
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
                //new file, needs to be written to disk and cloned
                if (!_clonedItems.TryGetValue(id, out var baseline))
                {
                    item.CreatestockDictionary();
                    SaveItem(item);
                    _clonedItems[id] = item.Clone();
                    saved.Add(fullfielName);
                    continue;
                }
                //edit to existing file, needs to be recloned
                if (!item.Equals(baseline))
                {
                    item.CreatestockDictionary();
                    SaveItem(item);
                    if (_clonedItems[id]._path != item._path)
                    {
                        if (File.Exists(_clonedItems[id]._path))
                            File.Delete(_clonedItems[id]._path);
                    }
                    _clonedItems[id] = item.Clone();
                    saved.Add(fullfielName);
                }
            }
            return saved;
        }
        protected override void SaveItem(ExpansionMarketTraderZone TraderZone)
        {
            AppServices.GetRequired<FileService>().SaveJson(TraderZone._path, TraderZone, false, true);
        }
        protected override string GetItemFileName(ExpansionMarketTraderZone TraderZone)
            => TraderZone.FileName;
        protected override string GetItemFilePath(ExpansionMarketTraderZone ExpansionMarketTraderZone)
          => ExpansionMarketTraderZone.FilePath;
        protected override bool ShouldDelete(ExpansionMarketTraderZone TraderZone)
            => TraderZone.ToDelete;
        protected override Guid GetID(ExpansionMarketTraderZone TraderZone)
            => TraderZone.Id;
        protected override void DeleteItemFile(ExpansionMarketTraderZone TraderZone)
        {
            if (!string.IsNullOrWhiteSpace(TraderZone._path) && File.Exists(TraderZone._path))
            {
                File.Delete(TraderZone._path);
            }
        }
        internal ExpansionMarketTraderZone AddNewTraderZone(string Zonename)
        {
            string filename = Helpers.SanitizePath(Zonename) + ".json";
            int mapsize = AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize;
            ExpansionMarketTraderZone TraderZone = new ExpansionMarketTraderZone()
            {
                m_Version = CurrentVersion,
                m_DisplayName = $"{Zonename} Trader Zone",
                Position = new Vec3 (new float[] { mapsize/2, 0, mapsize/2 }),
                Radius = mapsize,
                BuyPricePercent = 100,
                SellPricePercent = -1, //! -1 = Use global sell price percentage
                Stock = new Dictionary<string, int>(),
                stockList = new BindingList<ExpansionMarketTraderStockItem>()
            };
            TraderZone.SetPath(Path.Combine(FilePath, filename));
            TraderZone.SetGuid(Guid.NewGuid());
            MutableItems.Add(TraderZone);
            return TraderZone;

        }
        internal void RemoveFile(ExpansionMarketTraderZone TraderZone)
        {
            TraderZone.ToDelete = true;
        }
        public bool needToSave()
        {
            return false;
        }
        protected override IEnumerable<string> ValidateItem(ExpansionMarketTraderZone TraderZone)
        {
            return TraderZone.FixMissingOrInvalidFields();
        }
    }
    public class ExpansionMarketTraderZone : IDeepCloneable<ExpansionMarketTraderZone>, IEquatable<ExpansionMarketTraderZone>
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

        public int m_Version { get; set; }  //current version 5
        public string? m_DisplayName { get; set; }
        public Vec3? Position { get; set; }
        public decimal? Radius { get; set; }
        public decimal? BuyPricePercent { get; set; }
        public decimal? SellPricePercent { get; set; }
        public Dictionary<string, int> Stock { get; set; }
        public BindingList<ExpansionMarketTraderStockItem> stockList { get; set; }



        public bool Equals(ExpansionMarketTraderZone other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (Id != other.Id) return false;
            
            if (_path != other._path ||
                m_Version != other.m_Version ||
                m_DisplayName != other.m_DisplayName ||
                !Position.Equals(other.Position) ||
                Radius != other.Radius ||
                BuyPricePercent != other.BuyPricePercent ||
                SellPricePercent != other.SellPricePercent) 
                return false;
            
            if (!ListEquals(stockList, other.stockList))
                return false;
            
            return true;
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
        public override bool Equals(object? obj) => Equals(obj as ExpansionMarketTraderZone);
        public ExpansionMarketTraderZone Clone()
        {
            ExpansionMarketTraderZone clone = new ExpansionMarketTraderZone
            {
                m_Version = this.m_Version,
                m_DisplayName = this.m_DisplayName,
                Position = this.Position.Clone(),
                Radius = this.Radius,
                BuyPricePercent = this.BuyPricePercent,
                SellPricePercent = this.SellPricePercent,
                stockList = this.stockList != null
                    ? new BindingList<ExpansionMarketTraderStockItem>(this.stockList.Select(StockItem => StockItem.Clone()).ToList())
                    : new BindingList<ExpansionMarketTraderStockItem>(),
            };
            clone.SetPath(_path);
            clone.SetGuid(Id);
            return clone;
        }
        public void BuildStocklistruntime()
        {
            stockList = new BindingList<ExpansionMarketTraderStockItem>(
                Stock.Select(kvp => new ExpansionMarketTraderStockItem
                {
                    Name = kvp.Key,
                    Quantity = kvp.Value
                })
                .OrderBy(x => x.Name)
                .ToList()
            );
        }
        public void CreatestockDictionary()
        {
            Stock = stockList.ToDictionary(x => x.Name, x => x.Quantity);
        }
        internal IEnumerable<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();
            int mapsize = AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize;
            if (m_Version != ExpansionMarketTraderZoneConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionMarketTraderZoneConfig.CurrentVersion}");
                m_Version = ExpansionMarketTraderZoneConfig.CurrentVersion;
            }
            if (m_DisplayName == null)
            {
                fixes.Add($"Updated m_DisplayName to null");
                m_DisplayName = "";
            }
            if (Position == null)
            {
                Position = new Vec3(new float[] { mapsize / 2, 0, mapsize / 2 });
                fixes.Add($"Set default Position to {mapsize / 2},0,{mapsize / 2}");
            }
            if (Radius == null || Radius < 0)
            {
                fixes.Add($"Updated Radius to {mapsize}");
                Radius = mapsize;
            }
            if (BuyPricePercent == null || BuyPricePercent < 0)
            {
                fixes.Add($"Updated BuyPricePercent  to 100");
                Radius = 100;
            }
            if (SellPricePercent == null || SellPricePercent < -1)
            {
                fixes.Add($"Updated SellPricePercent to -1");
                Radius = -1;
            }
            BuildStocklistruntime();
            return fixes;
        }
    }
    public class ExpansionMarketTraderStockItem : IDeepCloneable<ExpansionMarketTraderStockItem>, IEquatable<ExpansionMarketTraderStockItem>
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public ExpansionMarketTraderStockItem() { }
        public override bool Equals(object? obj) => Equals(obj as ExpansionMarketTraderStockItem);
        public bool Equals(ExpansionMarketTraderStockItem other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (!Name.Equals(other.Name) ||
                 Quantity != other.Quantity)
                return false;

            return true;
        }
        public ExpansionMarketTraderStockItem Clone()
        {
            ExpansionMarketTraderStockItem clone = new ExpansionMarketTraderStockItem()
            {
                Name = this.Name,
                Quantity = this.Quantity
            };
            return clone;
        }
    }
}
