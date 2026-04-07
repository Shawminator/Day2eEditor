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
    public class ExpansionMarketTraderZoneConfig : MultiFileConfigLoader<ExpansionMarketTraderZone>
    {
        public const int CurrentVersion = 6;
        public ExpansionMarketTraderZoneConfig(string path) : base(path)
        {
        }
        public override void Load()
        {
            HasErrors = false;
            Errors.Clear();
            Items.Clear();
            ClonedItems.Clear();

            var filePaths = Directory.GetFiles(BasePath, "*.json");

            foreach (var file in filePaths)
            {
                try
                {
                    var item = LoadItem(file);

                    OnAfterItemLoad(item, file);
                    ClonedItems.Add(GetID(item), item.Clone());
                    var issues = ValidateData(item);
                    if (issues?.Any() == true)
                    {
                        Console.WriteLine("Validation issues in " + FileName + ":");
                        foreach (var msg in issues)
                            Console.WriteLine("- " + msg);
                    }
                    ClonedItems[item.Id].m_Categories = item.m_Categories != null
                        ? new BindingList<ExpansionMarketTraderCategory>(item.m_Categories.Select(cat => cat.Clone()).ToList())
                        : new BindingList<ExpansionMarketTraderCategory>();
                    Items.Add(item);

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
                //delete file from disk
                if (ShouldDelete(item))
                {
                    DeleteItemFile(item);
                    Items.RemoveAt(i);
                    ClonedItems.Remove(id);
                    saved.Add("File Remove " + fileName);
                    continue;
                }
                //new file, needs to be written to disk and cloned
                if (!ClonedItems.TryGetValue(id, out var baseline))
                {
                    item.SaveStock();
                    SaveItem(item);
                    ClonedItems[id] = item.Clone();
                    saved.Add(fileName);
                    continue;
                }
                //edit to existing file, needs to be recloned
                if (!item.Equals(baseline))
                {
                    item.SaveStock();
                    SaveItem(item);
                    if (ClonedItems[id]._path != item._path)
                    {
                        if (File.Exists(ClonedItems[id]._path))
                            File.Delete(ClonedItems[id]._path);
                    }
                    ClonedItems[id] = item.Clone();
                    saved.Add(fileName);
                }
            }
            saved.AddRange(DeleteEmptyDirectoriesFromPath(FilePath));
            return saved;
        }
        private List<string> DeleteEmptyDirectoriesFromPath(string rootPath)
        {
            List<string> removedFolders = new List<string>();
            if (!Directory.Exists(rootPath))
                return removedFolders;

            var directories = Directory
                .GetDirectories(rootPath, "*", SearchOption.AllDirectories)
                .OrderByDescending(d => d.Count(c => c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar))
                .ToList();

            foreach (var dir in directories)
            {
                if (!Directory.EnumerateFileSystemEntries(dir).Any())
                {
                    Directory.Delete(dir);
                    string relativePath = Path.GetRelativePath(rootPath, dir);
                    removedFolders.Add("Empty Folder Removed " + relativePath);
                }
            }

            return removedFolders;
        }
        protected override void SaveItem(ExpansionMarketTraderZone TraderZone)
        {
            AppServices.GetRequired<FileService>().SaveJson(TraderZone._path, TraderZone, false, true);
        }
        protected override string GetItemFileName(ExpansionMarketTraderZone TraderZone)
            => TraderZone.FileName;
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
                Stock = new Dictionary<string, int>()
            };
            TraderZone.SetPath(Path.Combine(FilePath, filename));
            TraderZone.LoadStock();
            TraderZone.SetGuid(Guid.NewGuid());
            Items.Add(TraderZone);
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
        protected override IEnumerable<string> ValidateData(ExpansionMarketTraderZone TraderZone)
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
        public void LoadStock()
        {
            stockList = new BindingList<ExpansionMarketTraderStockItem>(
                Stock.Select(kvp => new ExpansionMarketTraderStockItem
                {
                    Name = kvp.Key,
                    Quantity = kvp.Value
                }).ToList()
            );
        }
        public void SaveStock()
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
