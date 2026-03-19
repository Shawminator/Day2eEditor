
using Day2eEditor;
using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpansionPlugin
{
    public enum ExpansionMarketTraderBuySell
    {
        CanOnlyBuy = 0,
        CanBuyAndSell,
        CanOnlySell,
        CanBuyAndSellAsAttachmentOnly
    }
    public class ExpansionMarketTraderItem : IDeepCloneable<ExpansionMarketTraderItem>, IEquatable<ExpansionMarketTraderItem>
    {
        public ExpansionMarketItem MarketItem { get; set; }
        public ExpansionMarketTraderBuySell BuySell { get; set; }

        public ExpansionMarketTraderItem() { }  
        public ExpansionMarketTraderItem(ExpansionMarketItem marketItem, ExpansionMarketTraderBuySell buySell)
        {
            MarketItem = marketItem;
            BuySell = buySell;
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionMarketTraderItem);
        public bool Equals(ExpansionMarketTraderItem other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (MarketItem != other.MarketItem ||
                 BuySell != other.BuySell)
                return false;

            return true;
        }
        public ExpansionMarketTraderItem Clone()
        {
            ExpansionMarketTraderItem clone = new ExpansionMarketTraderItem()
            {
                MarketItem = this.MarketItem.Clone(),
                BuySell = this.BuySell
            };
            return clone;
        }
    }
    public class ExpansionMarketTraderConfig : MultiFileConfigLoader<ExpansionMarketTrader>
    {
        public const int CurrentVersion = 13;
        public ExpansionMarketTraderConfig(string path) : base(path)
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
                    ClonedItems[item.Id].m_Items = item.m_Items != null
                        ? new BindingList<ExpansionMarketTraderItem>(item.m_Items.Select(cat => cat.Clone()).ToList())
                        : null;
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
        protected override ExpansionMarketTrader LoadItem(string filePath)
        {
            var ExpansionTrader = AppServices.GetRequired<FileService>().LoadOrCreateJson(
                    filePath,
                    createNew: () => new ExpansionMarketTrader(),
                    onError: ex => HandleItemError(filePath, ex),
                    configName: "MarketTrader",
                    useVecConvertor: true
                );

            ExpansionTrader.SetPath(filePath);
            ExpansionTrader.SetGuid(Guid.NewGuid());  //Runtime Only
            return ExpansionTrader;
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
        internal ExpansionMarketTrader AddNewMarketTrader(string ExpansionMarketTraderName)
        {
            string filename = ExpansionMarketTraderName + ".json";
            ExpansionMarketTrader ExpansionMarketTrader = new ExpansionMarketTrader()
            {
                m_Version = CurrentVersion,
                DisplayName = ExpansionMarketTraderName,
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
            ExpansionMarketTrader.SetPath(Path.Combine(FilePath, filename));
            ExpansionMarketTrader.SetGuid(Guid.NewGuid());
            Items.Add(ExpansionMarketTrader);
            return ExpansionMarketTrader;

        }
        internal void RemoveFile(ExpansionMarketTrader ExpansionMarketTrader)
        {
            ExpansionMarketTrader.ToDelete = true;
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
        [JsonIgnore]
        public BindingList<ExpansionMarketTraderItem> m_Items { get; set; }    

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

        public bool Equals(ExpansionMarketTrader other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (m_Version != other.m_Version ||
                 DisplayName != other.DisplayName ||
                 MinRequiredReputation != other.MinRequiredReputation ||
                 MaxRequiredReputation != other.MaxRequiredReputation ||
                 RequiredFaction != other.RequiredFaction ||
                 RequiredCompletedQuestID != other.RequiredCompletedQuestID ||
                 TraderIcon != other.TraderIcon ||
                 DisplayCurrencyName != other.DisplayCurrencyName ||
                 DisplayCurrencyValue != other.DisplayCurrencyValue ||
                 UseCategoryOrder != other.UseCategoryOrder)
                return false;

            if (!ListEquals(Currencies, other.Currencies))
                return false;

            if (!ListEquals(Categories, other.Categories))
                return false;

            if (!ListEquals(m_Items, other.m_Items))
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
        private static bool AreEqual(Dictionary<string, ExpansionMarketTraderBuySell> a, Dictionary<string, ExpansionMarketTraderBuySell> b)
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
                    : new BindingList<string>(),
                DisplayCurrencyValue = this.DisplayCurrencyValue,
                DisplayCurrencyName = this.DisplayCurrencyName,
                UseCategoryOrder = this.UseCategoryOrder,
                Categories = this.Categories != null
                    ? new BindingList<string>(this.Categories.ToList())
                    : new BindingList<string>(),
                Items = this.Items != null
                    ? new Dictionary<string, ExpansionMarketTraderBuySell>(this.Items)
                    : new Dictionary<string, ExpansionMarketTraderBuySell>()
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
                fixes.Add($"Updated DisplayName to empty string");
                DisplayName = "";
            }
            if (MinRequiredReputation == null || MinRequiredReputation < 0)
            {
                fixes.Add("Updated MinRequiredReputation to 0");
                MinRequiredReputation = 0;
            }
            if (MaxRequiredReputation == null || MaxRequiredReputation < 0)
            {
                fixes.Add($"Updated MaxRequiredReputation to {int.MaxValue}");
                MaxRequiredReputation = int.MaxValue;
            }
            if (RequiredFaction == null)
            {
                fixes.Add($"Updated RequiredFaction to empty string");
                RequiredFaction = "";
            }
            if (RequiredCompletedQuestID == null)
            {
                fixes.Add($"Updated RequiredCompletedQuestID to -1");
                RequiredCompletedQuestID = -1;
            }
            if (TraderIcon == null)
            {
                fixes.Add($"Updated TraderIcon to empty string");
                TraderIcon = "";
            }
            if (Currencies == null)
            {
                Currencies = new BindingList<string>();
                fixes.Add("Initialized Currencies");
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
                fixes.Add($"Updated DisplayCurrencyValue to 1");
                DisplayCurrencyValue = 1;
            }
            if (DisplayCurrencyName == null)
            {
                fixes.Add($"Updated DisplayCurrencyName to empty string");
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
                fixes.Add("Initialized Categories");
            }
            else
            {
                for (int i = 0; i < Categories.Count; i++)
                {
                    string original = Categories[i];

                    if (!TryParseCategoryEntry(original, out var categoryPath, out var buySell, out var error))
                    {
                        fixes.Add($"MARKET CONFIGURATION ERROR: {error}");
                        continue;
                    }

                    string normalized = categoryPath;
                    if (original.Contains(':'))
                        normalized = $"{categoryPath}:{(int)buySell}";

                    if (!string.Equals(original, normalized, StringComparison.Ordinal))
                    {
                        Categories[i] = normalized;
                        fixes.Add($"Normalized category '{original}' to '{normalized}'");
                    }

                    if (!AppServices.GetRequired<ExpansionManager>().ExpansionMarketCategoryConfig.checkCategoryexists(categoryPath))
                    {
                        fixes.Add($"MARKET CONFIGURATION ERROR: Category '{categoryPath}' does not exist");
                        MessageBox.Show($"MARKET CONFIGURATION ERROR: Category '{categoryPath}' does not exist.\nPlease manaully check {FileName} for this");
                    }
                }
            }
            m_Items = new BindingList<ExpansionMarketTraderItem>();
            if (Items == null)
            {
                Items = new Dictionary<string, ExpansionMarketTraderBuySell>();
                
                fixes.Add("Initialized Items");
            }
            else if (Items.Count > 0)
            {
                var keysWithUpper = Items.Keys.Where(k => k.Any(char.IsUpper)).ToList();
                foreach (var key in keysWithUpper)
                {
                    string lowerKey = key.ToLowerInvariant();
                    fixes.Add($"\tMARKET CONFIGURATION ERROR: {key} changed to {lowerKey}");
                    var value = Items[key];
                    Items.Remove(key);
                    Items[lowerKey] = value;

                    ExpansionMarketItem marketItem = AppServices.GetRequired<ExpansionManager>().ExpansionMarketCategoryConfig.FindMarketItemByClassName(lowerKey);
                    ExpansionMarketTraderItem traderitem = new ExpansionMarketTraderItem(marketItem, value);
                    if (m_Items.Any(x => x.MarketItem.ClassName == traderitem.MarketItem.ClassName))
                    {
                        fixes.Add($"Item {traderitem.MarketItem.ClassName} has already been added to trader {_path}");
                        continue;
                    }
                    m_Items.Add(traderitem);
                }
            }
            return fixes;
        }
        private static bool TryParseCategoryEntry(
            string input,
            out string categoryPath,
            out ExpansionMarketTraderBuySell buySell,
            out string error)
        {
            categoryPath = string.Empty;
            buySell = ExpansionMarketTraderBuySell.CanBuyAndSell;
            error = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                error = "Category entry is empty.";
                return false;
            }

            string normalized = NormalizeCategoryReference(input);
            int colonIndex = normalized.IndexOf(':');

            if (colonIndex < 0)
            {
                categoryPath = normalized;
                return true;
            }

            categoryPath = normalized[..colonIndex];
            string suffix = normalized[(colonIndex + 1)..];

            if (!int.TryParse(suffix, out int rawValue) ||
                !Enum.IsDefined(typeof(ExpansionMarketTraderBuySell), rawValue))
            {
                error = $"Invalid buy/sell value '{suffix}' in category entry '{input}'.";
                return false;
            }

            buySell = (ExpansionMarketTraderBuySell)rawValue;
            return true;
        }
        private static string NormalizeCategoryReference(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            value = value.Trim();

            int colonIndex = value.IndexOf(':');
            string pathPart = colonIndex >= 0 ? value[..colonIndex] : value;
            string suffixPart = colonIndex >= 0 ? value[colonIndex..] : string.Empty;

            pathPart = pathPart.Replace('/', '\\').Trim('\\');

            if (pathPart.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                pathPart = pathPart[..^5];

            return pathPart + suffixPart;
        }
    }
}
