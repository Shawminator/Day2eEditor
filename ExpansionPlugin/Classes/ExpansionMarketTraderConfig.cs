
using Day2eEditor;
using System;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ExpansionPlugin
{
    public enum ExpansionMarketTraderBuySell
    {
        CanOnlyBuy = 0,
        CanBuyAndSell,
        CanOnlySell,
        CanBuyAndSellAsAttachmentOnly  //! Item should not be shown in menu, but can be sold/purchased as attachment on another item. For internal use only
    }
    public class ExpansionMarketTraderItem
    {
        public ExpansionMarketItem MarketItem { get; set; }
        public ExpansionMarketTraderBuySell BuySell;

        public ExpansionMarketTraderItem(ExpansionMarketItem marketItem, ExpansionMarketTraderBuySell buySell)
        {
            MarketItem = marketItem;
            BuySell = buySell;
        }
    }
    public class ExpansionMarketTraderConfig : MultiFileConfigLoader<ExpansionMarketTrader>
    {
        public const int CurrentVersion = 13;
        public ExpansionMarketTraderConfig(string path) : base(path)
        {
        }
        protected override ExpansionMarketTrader LoadItem(string filePath)
        {
            var TraderZone = AppServices.GetRequired<FileService>().LoadOrCreateJson(
                    filePath,
                    createNew: () => new ExpansionMarketTrader(),
                    onError: ex => HandleItemError(filePath, ex),
                    configName: "MarketTrader",
                    useVecConvertor: true
                );

            TraderZone.SetPath(filePath);
            TraderZone.SetGuid(Guid.NewGuid());
            TraderZone.getCategories();
            TraderZone.getSingleItems();
            return TraderZone;
        }
        protected override void SaveItem(ExpansionMarketTrader ExpansionMarketTrader)
        {
            AppServices.GetRequired<FileService>().SaveJson(ExpansionMarketTrader._path, ExpansionMarketTrader, false, true);
        }
        protected override string GetItemFileName(ExpansionMarketTrader ExpansionMarketTrader)
            => ExpansionMarketTrader.FileName;
        protected override bool ShouldDelete(ExpansionMarketTrader ExpansionMarketTrader)
            => ExpansionMarketTrader.ToDelete;
        protected override Guid GetID(ExpansionMarketTrader ExpansionMarketTrader)
            => ExpansionMarketTrader.Id;
        protected override void DeleteItemFile(ExpansionMarketTrader ExpansionMarketTrader)
        {
            if (!string.IsNullOrWhiteSpace(ExpansionMarketTrader._path) && File.Exists(ExpansionMarketTrader._path))
            {
                File.Delete(ExpansionMarketTrader._path);
            }
        }
        internal ExpansionMarketTrader AddNewMarketCategory(string ExpansionMarketTraderName)
        {
            string filepath = Path.Combine(AppServices.GetRequired<ExpansionManager>().basePath, "expansion", "p2pmarket");
            string filename = ExpansionMarketTraderName + ".json";
            ExpansionMarketTrader ExpansionMarketTrader = new ExpansionMarketTrader()
            {
                m_Version = CurrentVersion,
                DisplayName = filename,
                MinRequiredReputation = 0,
                MaxRequiredReputation = 2147483647,
                TraderIcon = "Trader",
                Currencies = new BindingList<string>(),
                Items = new Dictionary<string, ExpansionMarketTraderBuySell>(),
                Categories = new BindingList<string>(),
                RequiredFaction = "",
                RequiredCompletedQuestID = -1,
                DisplayCurrencyName = "",
                DisplayCurrencyValue = 1
            };
            ExpansionMarketTrader.SetPath(Path.Combine(filepath, filename));
            ExpansionMarketTrader.SetGuid(Guid.NewGuid());
            Items.Add(ExpansionMarketTrader);
            return ExpansionMarketTrader;

        }
        internal void RemoveFile(ExpansionMarketTrader ExpansionMarketTrader)
        {
            ExpansionMarketTrader.ToDelete = true;
        }
        public bool needToSave()
        {
            return false;
        }
        protected override IEnumerable<string> ValidateData(ExpansionMarketTrader ExpansionMarketTrader)
        {
            return ExpansionMarketTrader.FixMissingOrInvalidFields();
        }
    }
    public class ExpansionMarketTrader : IDeepCloneable<ExpansionMarketTrader>, IEquatable<ExpansionMarketTrader>
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


        public int m_Version { get; set; }
        public string DisplayName { get; set; }
        public int? MinRequiredReputation { get; set; }
        public int? MaxRequiredReputation { get; set; }
        public string? RequiredFaction { get; set; }
        public int? RequiredCompletedQuestID { get; set; }
        public string? TraderIcon { get; set; }
        public BindingList<string> Currencies { get; set; }
        public int? DisplayCurrencyValue { get; set; }
        public string? DisplayCurrencyName { get; set; }
        public int? UseCategoryOrder { get; set; }
        public BindingList<string> Categories { get; set; }
        public Dictionary<string, ExpansionMarketTraderBuySell> Items { get; set; }


        [JsonIgnore]
        public BindingList<ExpansionMarketCategory> ExpansionMarketCategories { get; set; }
        [JsonIgnore]
        public BindingList<ExpansionMarketTraderItem> ExpansionMarketItems { get; set; }  

        public bool Equals(ExpansionMarketTrader other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (m_Version != other.m_Version ||
                 DisplayName != other.DisplayName ||
                 MinRequiredReputation != other.MinRequiredReputation ||
                 MaxRequiredReputation != other.MinRequiredReputation ||
                 RequiredFaction != other.RequiredFaction ||
                 RequiredCompletedQuestID != other.RequiredCompletedQuestID ||
                 TraderIcon != TraderIcon ||
                 DisplayCurrencyName != other.DisplayCurrencyName ||
                 DisplayCurrencyValue != other.DisplayCurrencyValue ||
                 UseCategoryOrder != other.UseCategoryOrder)
                return false;

            if (!ListEquals(Categories, other.Categories))
                return false;

            if (!AreEqual(Items, other.Items))
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
        public bool AreEqual(Dictionary<string, ExpansionMarketTraderBuySell> a, Dictionary<string, ExpansionMarketTraderBuySell> b)
        {
            if (a.Count != b.Count)
                return false;

            foreach (var kvp in a)
            {
                if (!b.TryGetValue(kvp.Key, out ExpansionMarketTraderBuySell value))
                    return false;

                if (kvp.Value != value)
                    return false;
            }

            return true;
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionMarketTrader);
        public ExpansionMarketTrader Clone()
        {
            ExpansionMarketTrader clone = new ExpansionMarketTrader
            {
                m_Version = this.m_Version,
                DisplayName = this.DisplayName,
                MinRequiredReputation = this.MinRequiredReputation,
                MaxRequiredReputation = this.MaxRequiredReputation,
                RequiredFaction = this.RequiredFaction,
                RequiredCompletedQuestID = this.RequiredCompletedQuestID,
                TraderIcon = this.TraderIcon,
                Currencies = this.Currencies != null
                    ? new BindingList<string>(this.Currencies.ToList())
                    : null,
                DisplayCurrencyValue = this.DisplayCurrencyValue,
                DisplayCurrencyName = this.DisplayCurrencyName,
                UseCategoryOrder = this.UseCategoryOrder,
                Categories = this.Categories != null
                    ? new BindingList<string>(this.Categories.ToList())
                    : null,
                Items = new Dictionary<string, ExpansionMarketTraderBuySell>(this.Items)
            };
            clone.SetPath(_path);
            clone.SetGuid(Id);
            return clone;
        }
        internal IEnumerable<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();
            if (m_Version != ExpansionMarketTraderConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionMarketTraderConfig.CurrentVersion}");
                m_Version = ExpansionMarketTraderConfig.CurrentVersion;
            }
            if (DisplayName == null)
            {
                fixes.Add($"Updated m_DisplayName to null");
                DisplayName = "";
            }
            if (MinRequiredReputation == null || MinRequiredReputation < 0 || MaxRequiredReputation > int.MaxValue)
            {
                fixes.Add($"Updated MinRequiredReputation to 0");
                MinRequiredReputation = 0;
            }
            if (MaxRequiredReputation == null || MinRequiredReputation < 0 || MaxRequiredReputation > int.MaxValue)
            {
                fixes.Add($"Updated MaxRequiredReputation to {int.MaxValue}");
                MaxRequiredReputation = int.MaxValue;
            }
            if (RequiredFaction == null)
            {
                fixes.Add($"Updated RequiredFaction to null");
                RequiredFaction = "";
            }
            if (RequiredCompletedQuestID == null)
            {
                fixes.Add($"Updated RequiredCompletedQuestID to -1");
                RequiredCompletedQuestID = -1;
            }
            if (TraderIcon == null)
            {
                fixes.Add($"Updated TraderIcon to null");
                TraderIcon = "";
            }
            if (Currencies.Count > 0)
            {
                for (int i = 0; i < Currencies.Count; i++)
                {
                    string original = Currencies[i];

                    if (original.Any(char.IsUpper))
                    {
                        string lower = original.ToLowerInvariant();
                        Currencies[i] = lower;

                        fixes.Add($"\tMARKET CONFIGURATION ERROR: {original} changed to {lower}");
                    }
                }
            }

            if (DisplayCurrencyValue == null || DisplayCurrencyValue < 0 || DisplayCurrencyValue > 1)
            {
                fixes.Add($"Updated SellPricePercent to 0");
                DisplayCurrencyValue = 0;
            }
            if (DisplayCurrencyName == null)
            {
                fixes.Add($"Updated DisplayCurrencyName to null");
                DisplayCurrencyName = "";
            }
            if (UseCategoryOrder == null || UseCategoryOrder < 0 || UseCategoryOrder > 1)
            {
                fixes.Add($"Updated UseCategoryOrder to 0");
                UseCategoryOrder = 0;
            }
            if (Categories == null)
            {
                Categories = new BindingList<string>();
                fixes.Add("Items Categories Currencies");
            }
            if (Items == null)
            {
                Items = new Dictionary<string, ExpansionMarketTraderBuySell>();
                fixes.Add("Items Items Currencies");
            }
            if (Items.Count > 0)
            {
                var keysWithUpper = Items.Keys.Where(k => k.Any(char.IsUpper)).ToList();
                foreach (var key in keysWithUpper)
                {
                    string lowerKey = key.ToLower();
                    fixes.Add($"\tMARKET CONFIGURATION ERROR: {key} changed to {lowerKey}");
                    var value = Items[key];
                    Items.Remove(key);
                    Items[lowerKey] = value;
                }

            }
            return fixes;
        }

        internal void getCategories()
        {
            
        }

        internal void getSingleItems()
        {
            foreach (KeyValuePair<string,ExpansionMarketTraderBuySell> item in Items)
            {
                ExpansionMarketItem Titem = AppServices.GetRequired<ExpansionMarketCategoryConfig>().getitem(item.Key);
            }
        }
    }
}
