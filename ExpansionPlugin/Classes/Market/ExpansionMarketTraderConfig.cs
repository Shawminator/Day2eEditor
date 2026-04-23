
using Day2eEditor;
using System;
using System.ComponentModel;
using System.Security.Policy;
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
    public class ExpansionMarketTraderCategory : IDeepCloneable<ExpansionMarketTraderCategory>, IEquatable<ExpansionMarketTraderCategory>
    {
        public string CategoryPath { get; set; }
        public ExpansionMarketCategory MarketCategory { get; set; }
        public ExpansionMarketTraderBuySell BuySell { get; set; }

        public ExpansionMarketTraderCategory() { }
        public ExpansionMarketTraderCategory(string categoryPath, ExpansionMarketCategory marketCategory, ExpansionMarketTraderBuySell buySell)
        {
            CategoryPath = categoryPath;
            MarketCategory = marketCategory;
            BuySell = buySell;
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionMarketTraderCategory);
        public bool Equals(ExpansionMarketTraderCategory other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (CategoryPath != other.CategoryPath ||
                BuySell != other.BuySell)
                return false;

            if (MarketCategory == null && other.MarketCategory == null)
                return true;

            if (MarketCategory == null || other.MarketCategory == null)
                return false;

            return MarketCategory.Equals(other.MarketCategory);
        }
        public ExpansionMarketTraderCategory Clone()
        {
            return new ExpansionMarketTraderCategory()
            {
                CategoryPath = this.CategoryPath,
                MarketCategory = this.MarketCategory != null ? this.MarketCategory.Clone() : null,
                BuySell = this.BuySell
            };
        }
        public override string ToString()
        {
            if (MarketCategory == null)
                return CategoryPath;
            else
                return MarketCategory.FileName;
        }
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

            if (!MarketItem.Equals(other.MarketItem) ||
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
    public class ExpansionMarketTraderConfig : MultiFileConfigLoaderBase<ExpansionMarketTrader>
    {
        public const int CurrentVersion = 13;
        public ExpansionMarketTraderConfig(string path) : base(path)
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
                    _clonedItems[item.Id].m_Categories = item.m_Categories != null
                        ? new BindingList<ExpansionMarketTraderCategory>(item.m_Categories.Select(cat => cat.Clone()).ToList())
                        : new BindingList<ExpansionMarketTraderCategory>();
                    _clonedItems[item.Id].m_Items = item.m_Items != null
                        ? new BindingList<ExpansionMarketTraderItem>(item.m_Items.Select(cat => cat.Clone()).ToList())
                        : null;
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
                    item.CreateCategoryList();
                    item.CreateDictionary();
                    SaveItem(item);
                    _clonedItems[id] = item.Clone();
                    saved.Add(fullfielName);
                    continue;
                }
                //edit to existing file, needs to be recloned
                if (!item.Equals(baseline))
                {
                    item.CreateCategoryList();
                    item.CreateDictionary();
                    SaveItem(item);
                    _clonedItems[id] = item.Clone();
                    saved.Add(fullfielName);
                }
            }
            return saved;
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
            string filename = Helpers.SanitizePath(ExpansionMarketTraderName) + ".json";
            ExpansionMarketTrader ExpansionMarketTrader = new ExpansionMarketTrader()
            {
                m_Version = CurrentVersion,
                DisplayName = ExpansionMarketTraderName,
                MinRequiredReputation = 0,
                MaxRequiredReputation = 2147483647,
                TraderIcon = "Trader",
                Currencies = new BindingList<string>(),
                Items = new Dictionary<string, ExpansionMarketTraderBuySell>(),
                m_Items = new BindingList<ExpansionMarketTraderItem>(),
                Categories = new BindingList<string>(),
                m_Categories = new BindingList<ExpansionMarketTraderCategory>(),
                RequiredFaction = "",
                RequiredCompletedQuestID = -1,
                DisplayCurrencyName = "",
                DisplayCurrencyValue = 1
            };
            ExpansionMarketTrader.SetPath(Path.Combine(FilePath, filename));
            ExpansionMarketTrader.SetGuid(Guid.NewGuid());
            MutableItems.Add(ExpansionMarketTrader);
            return ExpansionMarketTrader;

        }
        internal void RemoveFile(ExpansionMarketTrader ExpansionMarketTrader)
        {
            ExpansionMarketTrader.ToDelete = true;
        }
        protected override IEnumerable<string> ValidateItem(ExpansionMarketTrader ExpansionMarketTrader)
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
        public string FilePath => _path;
        [JsonIgnore]
        public bool ToDelete { get; set; }
        [JsonIgnore]
        public Guid Id { get; set; }
        [JsonIgnore]
        public BindingList<ExpansionMarketTraderCategory> m_Categories { get; set; }
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

            if (!ListEquals(m_Categories, other.m_Categories))
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
                m_Categories = this.m_Categories != null
                    ? new BindingList<ExpansionMarketTraderCategory>(this.m_Categories.Select(cat => cat.Clone()).ToList())
                    : new BindingList<ExpansionMarketTraderCategory>(),
                m_Items = this.m_Items != null
                    ? new BindingList<ExpansionMarketTraderItem>(this.m_Items.Select(cat => cat.Clone()).ToList())
                    : null
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
            BuildCategoryRuntimeModel(fixes);
            BuildItemRuntimeModel(fixes);
            return fixes;
        }
        private void BuildCategoryRuntimeModel(List<string> fixes)
        {
            m_Categories = new BindingList<ExpansionMarketTraderCategory>();

            if (Categories == null)
            {
                Categories = new BindingList<string>();
                fixes.Add("Initialized Categories");
                return;
            }
            var seenPaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < Categories.Count; i++)
            {
                string original = Categories[i];

                if (!TryParseCategoryEntry(original, out var categoryPath, out var buySell, out var error))
                {
                    fixes.Add($"MARKET CONFIGURATION ERROR: {error}");
                    continue;
                }

                if (!seenPaths.Add(categoryPath))
                {
                    fixes.Add($"Category '{categoryPath}' has already been added to trader {_path}");
                    continue;
                }

                ExpansionMarketCategory marketCategory = AppServices
                    .GetRequired<ExpansionManager>()
                    .ExpansionMarketCategoryConfig
                    .GetCategoryByPath(categoryPath);

                if (marketCategory == null)
                {
                    fixes.Add($"MARKET CONFIGURATION ERROR: Category '{categoryPath}' does not exist");
                    MessageBox.Show($"MARKET CONFIGURATION ERROR: Category '{categoryPath}' does not exist.\nPlease manually check {FileName} for this");
                }

                var traderCategory = new ExpansionMarketTraderCategory(categoryPath, marketCategory, buySell);
                m_Categories.Add(traderCategory);
            }
        }
        private void BuildItemRuntimeModel(List<string> fixes)
        {
            m_Items = new BindingList<ExpansionMarketTraderItem>();

            if (Items == null)
            {
                Items = new Dictionary<string, ExpansionMarketTraderBuySell>();
                fixes.Add("Initialized Items");
                return;
            }

            if (Items.Count == 0)
                return;

            var keysWithUpper = Items.Keys.Where(k => k.Any(char.IsUpper)).ToList();

            foreach (var key in keysWithUpper)
            {
                string lowerKey = key.ToLowerInvariant();
                var value = Items[key];

                if (Items.ContainsKey(lowerKey) && key != lowerKey)
                {
                    fixes.Add($"Duplicate key conflict: {key} conflicts with existing key {lowerKey}");
                    continue;
                }

                fixes.Add($"\tMARKET CONFIGURATION ERROR: {key} changed to {lowerKey}");

                Items.Remove(key);
                Items[lowerKey] = value;
            }

            foreach (var kvp in Items)
            {
                string key = kvp.Key;
                var value = kvp.Value;

                ExpansionMarketItem marketItem = AppServices
                    .GetRequired<ExpansionManager>()
                    .ExpansionMarketCategoryConfig
                    .FindMarketItemByClassName(key);

                if (marketItem == null)
                {
                    fixes.Add($"Item {key} not found in market config");
                    continue;
                }

                var traderItem = new ExpansionMarketTraderItem(marketItem, value);

                if (m_Items.Any(x => x.MarketItem.ClassName == traderItem.MarketItem.ClassName))
                {
                    fixes.Add($"Item {traderItem.MarketItem.ClassName} has already been added to trader {_path}");
                    continue;
                }

                m_Items.Add(traderItem);

            }
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


        internal void CreateCategoryList()
        {
            Categories = new BindingList<string>();

            if (m_Categories == null)
                return;

            foreach (ExpansionMarketTraderCategory category in m_Categories)
            {
                if (category?.MarketCategory == null)
                    continue;

                string categoryPath = NormalizeCategoryReference(category.CategoryPath);

                if (string.IsNullOrWhiteSpace(categoryPath))
                    continue;

                Categories.Add($"{categoryPath}:{(int)category.BuySell}");
            }
        }
        internal void CreateDictionary()
        {
            Items = new Dictionary<string, ExpansionMarketTraderBuySell>();

            if (m_Items == null)
                return;

            foreach (ExpansionMarketTraderItem tItem in m_Items)
            {
                if (tItem?.MarketItem == null || string.IsNullOrWhiteSpace(tItem.MarketItem.ClassName))
                    continue;

                if (!Items.ContainsKey(tItem.MarketItem.ClassName))
                    Items.Add(tItem.MarketItem.ClassName, tItem.BuySell);
            }
        }

        public List<ExpansionMarketItem> GetMissedMarketItems()
        {
            var missedItems = new List<ExpansionMarketItem>();

            if (m_Categories == null || m_Categories.Count == 0)
                return missedItems;

            var categoryConfig = AppServices
                .GetRequired<ExpansionManager>()
                .ExpansionMarketCategoryConfig;

            // Build a fast lookup of all classnames that exist in trader categories
            var traderClassNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var traderCategory in m_Categories)
            {
                if (traderCategory?.MarketCategory?.Items == null)
                    continue;

                foreach (var item in traderCategory.MarketCategory.Items)
                {
                    if (!string.IsNullOrWhiteSpace(item.ClassName))
                        traderClassNames.Add(item.ClassName);
                }
            }

            // Now check attachments
            foreach (var traderCategory in m_Categories)
            {
                if (traderCategory?.MarketCategory?.Items == null)
                    continue;

                foreach (var item in traderCategory.MarketCategory.Items)
                {
                    if (item.SpawnAttachments == null)
                        continue;

                    foreach (var attachmentClass in item.SpawnAttachments)
                    {
                        if (attachmentClass == "headlighth7")
                        {
                            string stop = "";
                        }

                        if (string.IsNullOrWhiteSpace(attachmentClass))
                            continue;

                        // If attachment NOT in trader categories
                        if (!traderClassNames.Contains(attachmentClass))
                        {
                            var attachmentItem = categoryConfig.FindMarketItemByClassName(attachmentClass);

                            if (attachmentItem != null &&
                                !missedItems.Any(x =>
                                    string.Equals(x.ClassName, attachmentItem.ClassName, StringComparison.OrdinalIgnoreCase)))
                            {
                                missedItems.Add(attachmentItem);
                            }
                        }
                    }
                }
            }

            return missedItems;
        }
    }
}
