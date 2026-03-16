using Day2eEditor;
using System.ComponentModel;

using System.Text.Json.Serialization;

namespace ExpansionPlugin
{
    public class ExpansionMarketCategoryConfig : MultiFileConfigLoader<ExpansionMarketCategory>
    {
        public const int CurrentVersion = 12;
        public ExpansionMarketCategoryConfig(string path) : base(path)
        {
        }
        public override void Load()
        {
            HasErrors = false;
            Errors.Clear();
            Items.Clear();
            ClonedItems.Clear();

            var filePaths = Directory.GetFiles(BasePath, "*.json", SearchOption.AllDirectories);

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
        protected override ExpansionMarketCategory LoadItem(string filePath)
        {
            var TraderZone = AppServices.GetRequired<FileService>().LoadOrCreateJson(
                    filePath,
                    createNew: () => new ExpansionMarketCategory(),
                    onError: ex => HandleItemError(filePath, ex),
                    configName: "MarketCategory",
                    useVecConvertor: true
                );

            TraderZone.SetPath(filePath);
            TraderZone.SetFoldeParts(BasePath);
            TraderZone.SetGuid(Guid.NewGuid());
            return TraderZone;
        }
        protected override void SaveItem(ExpansionMarketCategory ExpansionMarketCategory)
        {
            AppServices.GetRequired<FileService>().SaveJson(ExpansionMarketCategory._path, ExpansionMarketCategory, false, true);
        }
        protected override string GetItemFileName(ExpansionMarketCategory ExpansionMarketCategory)
            => ExpansionMarketCategory.FileName;
        protected override bool ShouldDelete(ExpansionMarketCategory ExpansionMarketCategory)
            => ExpansionMarketCategory.ToDelete;
        protected override Guid GetID(ExpansionMarketCategory ExpansionMarketCategory)
            => ExpansionMarketCategory.Id;
        protected override void DeleteItemFile(ExpansionMarketCategory ExpansionMarketCategory)
        {
            if (!string.IsNullOrWhiteSpace(ExpansionMarketCategory._path) && File.Exists(ExpansionMarketCategory._path))
            {
                File.Delete(ExpansionMarketCategory._path);
            }
        }
        internal ExpansionMarketCategory AddNewMarketCategory(string ExpansionMarketCategoryName)
        {
            string filepath = Path.Combine(AppServices.GetRequired<ExpansionManager>().basePath, "expansion", "p2pmarket");
            string filename = ExpansionMarketCategoryName + ".json";
            ExpansionMarketCategory ExpansionMarketCategory = new ExpansionMarketCategory()
            {
                m_Version = CurrentVersion,
                DisplayName = ExpansionMarketCategoryName,
                Icon = "Deliver",
                Color = "FBFCFEFF",
                InitStockPercent = (decimal)75.0,
                Items = new BindingList<ExpansionMarketItem>()
            };
            ExpansionMarketCategory.SetPath(Path.Combine(filepath, filename));
            ExpansionMarketCategory.SetGuid(Guid.NewGuid());
            Items.Add(ExpansionMarketCategory);
            return ExpansionMarketCategory;

        }
        internal void RemoveFile(ExpansionMarketCategory ExpansionMarketCategory)
        {
            ExpansionMarketCategory.ToDelete = true;
        }
        public bool needToSave()
        {
            return false;
        }
        protected override IEnumerable<string> ValidateData(ExpansionMarketCategory ExpansionMarketCategory)
        {
            return ExpansionMarketCategory.FixMissingOrInvalidFields();
        }
    }
    public class ExpansionMarketCategory : IDeepCloneable<ExpansionMarketCategory>, IEquatable<ExpansionMarketCategory>
    {
        [JsonIgnore]
        public string _path { get; private set; }
        [JsonIgnore]
        public List<string> FolderParts { get; private set; }
        [JsonIgnore]
        public string FileName => Path.GetFileName(_path);
        [JsonIgnore]
        public bool ToDelete { get; set; }
        [JsonIgnore]
        public Guid Id { get; set; }

        public void SetPath(string path) => _path = path;
        internal void SetGuid(Guid guid) => Id = guid;
        public void SetFoldeParts(string basePath)
        {
            string dir = Path.GetDirectoryName(_path);
            string normBase = Path.GetFullPath(basePath).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            string normDir = Path.GetFullPath(dir).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            if (string.Equals(normBase, normDir, StringComparison.OrdinalIgnoreCase))
            {
                FolderParts = new List<string>();
                return;
            }
            var relative = Path.GetRelativePath(normBase, normDir);
            var parts = relative.Split(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
            FolderParts = parts.ToList();

        }

        public int m_Version { get; set; } //current version 9
        public string? DisplayName { get; set; }
        public string? Icon { get; set; }
        public string? Color { get; set; }
        public decimal? InitStockPercent { get; set; }
        public int? IsExchange { get; set; }
        public BindingList<ExpansionMarketItem> Items { get; set; }

        public bool Equals(ExpansionMarketCategory other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (m_Version != other.m_Version ||
                DisplayName != other.DisplayName ||
                Icon != other.Icon ||
                Color != other.Color ||
                InitStockPercent != other.InitStockPercent ||
                IsExchange != other.IsExchange )
                return false;

            if (!ListEquals(Items, other.Items))
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
        public override bool Equals(object? obj) => Equals(obj as ExpansionMarketCategory);
        public ExpansionMarketCategory Clone()
        {
            ExpansionMarketCategory clone = new ExpansionMarketCategory
            {
                m_Version = this.m_Version,
                DisplayName = this.DisplayName,
                Icon = this.Icon,
                Color = this.Color,
                InitStockPercent = this.InitStockPercent,
                IsExchange  = this.IsExchange,
                Items = this.Items != null
                    ? new BindingList<ExpansionMarketItem>(this.Items.Select(cat => cat.Clone()).ToList())
                    : null,

            };
            clone.SetPath(_path);
            clone.SetGuid(Id);
            return clone;
        }

        internal IEnumerable<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();
            if (m_Version != ExpansionMarketCategoryConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionMarketCategoryConfig.CurrentVersion}");
                m_Version = ExpansionMarketCategoryConfig.CurrentVersion;
            }
            if (DisplayName == null)
            {
                fixes.Add($"Updated DisplayName to null");
                DisplayName = "";
            }
            if (Icon == null)
            {
                fixes.Add($"Updated Icon to Deliver");
                Icon = "Deliver";
            }
            if (Color == null)
            {
                fixes.Add($"Updated Color to FBFCFEFF");
                Color = "FBFCFEFF";
            }
            if (InitStockPercent == null)
            {
                fixes.Add($"Updated InitStockPercent to 75");
                InitStockPercent = 75m;
            }
            if (IsExchange == null)
            {
                fixes.Add($"Updated IsExchange to false");
                IsExchange = 0;
            }
            if (Items == null)
            {
                Items = new BindingList<ExpansionMarketItem>();
                fixes.Add("Items List Initialized");
            }
            if (Items.Count > 0)
            {
                foreach (var item in Items)
                {
                    fixes.AddRange(item.SanityCheckAndRepair());
                }
            }
            return fixes;
        }

   }
    public class ExpansionMarketItem : IDeepCloneable<ExpansionMarketItem>, IEquatable<ExpansionMarketItem>
    {
        public string? ClassName { get; set; }
        public int? MaxPriceThreshold { get; set; }
        public int? MinPriceThreshold { get; set; }
        public decimal? SellPricePercent { get; set; }
        public int? MaxStockThreshold { get; set; }
        public int? MinStockThreshold { get; set; }
        public int? QuantityPercent { get; set; }
        public BindingList<string> SpawnAttachments { get; set; }
        public BindingList<string> Variants { get; set; }

        public bool Equals(ExpansionMarketItem other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (ClassName != other.ClassName ||
                MaxPriceThreshold != other.MaxPriceThreshold ||
                MinPriceThreshold != other.MinPriceThreshold ||
                SellPricePercent != other.SellPricePercent ||
                MaxStockThreshold != other.MaxStockThreshold ||
                MinStockThreshold != other.MinPriceThreshold ||
                QuantityPercent != other.QuantityPercent)
                return false;

            if (!ListEquals(SpawnAttachments, other.SpawnAttachments))
                return false;

            if (!ListEquals(Variants, other.Variants))
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
        public override bool Equals(object? obj) => Equals(obj as ExpansionMarketItem);
        public ExpansionMarketItem Clone()
        {
            ExpansionMarketItem clone = new ExpansionMarketItem
            {
                ClassName = this.ClassName,
                MaxPriceThreshold = this.MaxPriceThreshold,
                MinPriceThreshold = this.MinPriceThreshold,
                SellPricePercent = this.SellPricePercent,
                MaxStockThreshold = this.MaxStockThreshold,
                MinStockThreshold = this.MinStockThreshold,
                QuantityPercent = this.QuantityPercent,
                SpawnAttachments = new BindingList<string>(this.SpawnAttachments?.ToList() ?? new List<string>()),
                Variants = new BindingList<string>( this.Variants?.ToList() ?? new List<string>())
            };
            return clone;
        }

        internal IEnumerable<string> SanityCheckAndRepair()
        {
            List<string> fixes = new List<string>();
            if (ClassName.Any(Char.IsUpper))
            {
                string uppercase = ClassName;
                ClassName = uppercase.ToLower();
                fixes.Add($"\tMARKET CONFIGURATION ERROR:{uppercase} changed to {ClassName}");
            }
            if (MinPriceThreshold < 0)
            {
                fixes.Add($"\tMARKET CONFIGURATION ERROR: The minimum price must be 0 or higher for '{ClassName}'");
                MinPriceThreshold = 0;
            }
            if (MinStockThreshold < 0)
            {
                fixes.Add($"\tMARKET CONFIGURATION ERROR: The minimum stock must be 0 or higher for '{ClassName}'");
                MinStockThreshold = 0;
            }
            if (MinPriceThreshold > MaxPriceThreshold)
            {
                fixes.Add($"\tMARKET CONFIGURATION ERROR: The minimum price must be lower than or equal to the maximum price for '{ClassName}'");
                MaxPriceThreshold = MinPriceThreshold;
            }
            if (MinStockThreshold > MaxStockThreshold)
            {
                fixes.Add($"\tMARKET CONFIGURATION ERROR: The minimum stock must be lower than or equal to the maximum stock for '{ClassName}'");
                MaxStockThreshold = MinStockThreshold;
            }
            if (MinStockThreshold > 655535)
            {
                fixes.Add($"\tMarket configuration warning: The minimum stock must be lower than or equal to 655535 for '{ClassName}'");
                MinStockThreshold = 655535;
            }
            if (MaxStockThreshold > 655535)
            {
                fixes.Add($"\tMarket configuration warning: The maximum stock must be lower than or equal to 655535 for '{ClassName}'");
                MaxStockThreshold = 655535;
            }
            fixes.AddRange(SetAttachments(SpawnAttachments));
            fixes.AddRange(SetVariants(Variants));
            return fixes;
        }
        private IEnumerable<string> SetAttachments(BindingList<string> attachments)
        {
            List<string> fixes = new List<string>();
            SpawnAttachments = new BindingList<string>();
            if (attachments != null)
            {
                for (int i = 0; i < attachments.Count; i++)
                {
                    string attClsName = attachments[i];

                    if (i == 255)
                    {
                        fixes.Add($"\tMARKET CONFIGURATION ERROR: The max allowed number of attachments per item is 255. Item: '{ClassName}'-'{attClsName}'");
                        break;
                    }
                    if (attClsName.Any(Char.IsUpper))
                    {
                        string uppercase = attClsName;
                        attClsName = uppercase.ToLower();
                        fixes.Add($"\t\tMARKET CONFIGURATION ERROR:{uppercase} changed to {attClsName}");
                    }

                    // Prevent attachment recursion
                    if (attClsName == ClassName)
                    {
                        fixes.Add($"\t\tMARKET CONFIGURATION ERROR: Trying to add {attClsName} as attachment to itself, will be removed...." );
                    }
                    else
                    {
                        SpawnAttachments.Add(attClsName);
                    }
                }
            }
            return fixes;
        }
        private IEnumerable<string> SetVariants(BindingList<string> variants)
        {
            List<string> fixes = new List<string>();
            Variants = new BindingList<string>();
            if (variants != null)
            {
                for(int i = 0; i < variants.Count; i++)
                {
                    string varClsName = variants[i];
                    if (i == 255)
                    {
                        fixes.Add($"\tMARKET CONFIGURATION ERROR: The max allowed number of attachments per item is 255. Item: '{ClassName}'-'{varClsName}'");
                        break;
                    }
                    if (varClsName.Any(Char.IsUpper))
                    {
                        string uppercase = varClsName;
                        varClsName = uppercase.ToLower();
                        fixes.Add($"\t\tMARKET CONFIGURATION ERROR:{uppercase} changed to {varClsName}");
                    }
                    if (varClsName == ClassName)
                    {
                        fixes.Add($"\t\tMARKET CONFIGURATION ERROR: Trying to add {varClsName} as variant to itself, will be removed....");
                    }
                    else
                    {
                        Variants.Add(varClsName);
                    }
                }

            }
            return fixes;
        }
    }
}
