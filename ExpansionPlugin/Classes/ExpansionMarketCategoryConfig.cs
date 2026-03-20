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
                    SaveItem(item);
                    ClonedItems[id] = item.Clone();
                    saved.Add(fileName);
                    continue;
                }
                //edit to existing file, needs to be recloned
                if (!item.Equals(baseline))
                {
                    SaveItem(item);
                    if (ClonedItems[id]._path != item._path)
                    {
                        if(File.Exists(ClonedItems[id]._path))
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
        public bool needToSave()
        {
            return false;
        }
        protected override IEnumerable<string> ValidateData(ExpansionMarketCategory ExpansionMarketCategory)
        {
            return ExpansionMarketCategory.FixMissingOrInvalidFields();
        }
        protected override void OnAfterLoadAll()
        {
            var duplicateGroups = Items
                .Where(category => category.Items != null)
                .SelectMany(category => category.Items.Select(item => new
                {
                    Category = category,
                    Item = item,
                    ClassName = item.ClassName?.Trim()
                }))
                .Where(x => !string.IsNullOrWhiteSpace(x.ClassName))
                .GroupBy(x => x.ClassName, StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .ToList();

            foreach (var group in duplicateGroups)
            {
                string locations = string.Join(", ",group.Select(x => x.Category.FileName));
                Console.WriteLine($"[ERROR] : Duplicate market item classname '{group.Key}' found in: {locations}");
            }
        }
        internal ExpansionMarketCategory AddNewMarketCategory(string fileName, List<string> folderParts)
        {
            string filepath = Path.Combine(FilePath, Path.Combine(folderParts.ToArray()));
            string filename = fileName + ".json";
            ExpansionMarketCategory ExpansionMarketCategory = new ExpansionMarketCategory()
            {
                m_Version = CurrentVersion,
                DisplayName = filename,
                Icon = "Deliver",
                Color = "FBFCFEFF",
                InitStockPercent = (decimal)75.0,
                IsExchange = 0,
                Items = new BindingList<ExpansionMarketItem>()
            };
            ExpansionMarketCategory.SetPath(Path.Combine(filepath, filename));
            ExpansionMarketCategory.SetGuid(Guid.NewGuid());
            ExpansionMarketCategory.SetFolderParts(folderParts);
            Items.Add(ExpansionMarketCategory);
            return ExpansionMarketCategory;

        }
        internal void RemoveFile(ExpansionMarketCategory ExpansionMarketCategory)
        {
            ExpansionMarketCategory.ToDelete = true;
        }
        internal bool MarketItemClassNameExists(string className)
        {
            if (string.IsNullOrWhiteSpace(className))
                return false;

            return Items.Any(category =>
                category.Items != null &&
                category.Items.Any(item =>
                    !string.IsNullOrWhiteSpace(item.ClassName) &&
                    string.Equals(item.ClassName, className, StringComparison.OrdinalIgnoreCase)));
        }
        internal ExpansionMarketItem FindMarketItemByClassName(string className)
        {
            if (string.IsNullOrWhiteSpace(className))
                return null;

            return Items
                .Where(category => category.Items != null)
                .SelectMany(category => category.Items)
                .FirstOrDefault(item =>
                    !string.IsNullOrWhiteSpace(item.ClassName) &&
                    string.Equals(item.ClassName, className, StringComparison.OrdinalIgnoreCase));
        }
        internal (ExpansionMarketItem item, ExpansionMarketCategory category) FindMarketItemWithCategory(string className)
        {
            if (string.IsNullOrWhiteSpace(className))
                return (null, null);

            foreach (var category in Items)
            {
                if (category.Items == null)
                    continue;

                var item = category.Items.FirstOrDefault(x =>
                    !string.IsNullOrWhiteSpace(x.ClassName) &&
                    string.Equals(x.ClassName, className, StringComparison.OrdinalIgnoreCase));

                if (item != null)
                    return (item, category);
            }

            return (null, null);
        }
        internal List<(ExpansionMarketItem item, ExpansionMarketCategory category)> FindAllDuplicates(string className)
        {
            var results = new List<(ExpansionMarketItem, ExpansionMarketCategory)>();

            if (string.IsNullOrWhiteSpace(className))
                return results;

            foreach (var category in Items)
            {
                if (category.Items == null)
                    continue;

                foreach (var item in category.Items)
                {
                    if (string.IsNullOrWhiteSpace(item.ClassName))
                        continue;

                    if (string.Equals(item.ClassName, className, StringComparison.OrdinalIgnoreCase))
                    {
                        results.Add((item, category));
                    }
                }
            }

            return results;
        }
        internal ExpansionMarketCategory FindCategoryContainingItem(ExpansionMarketItem item)
        {
            if (item == null)
                return null;

            return Items.FirstOrDefault(category =>
                category.Items != null &&
                category.Items.Any(x => ReferenceEquals(x, item)));
        }
        internal List<(ExpansionMarketItem ownerItem, ExpansionMarketCategory category, string variantClassName)> FindVariantReferences(string className)
        {
            var results = new List<(ExpansionMarketItem, ExpansionMarketCategory, string)>();

            if (string.IsNullOrWhiteSpace(className))
                return results;

            foreach (var category in Items)
            {
                if (category.Items == null)
                    continue;

                foreach (var marketItem in category.Items)
                {
                    if (marketItem.Variants == null)
                        continue;

                    foreach (string variant in marketItem.Variants)
                    {
                        if (string.IsNullOrWhiteSpace(variant))
                            continue;

                        if (string.Equals(variant, className, StringComparison.OrdinalIgnoreCase))
                        {
                            results.Add((marketItem, category, variant));
                        }
                    }
                }
            }

            return results;
        }
        internal bool CanAddMarketItem(ExpansionMarketCategory category, ExpansionMarketItem item, out string error, out bool requiresVariantPromotionConfirmation)
        {
            error = null;
            requiresVariantPromotionConfirmation = false;

            if (category == null)
            {
                error = "Category was null.";
                return false;
            }

            if (item == null)
            {
                error = "Item was null.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(item.ClassName))
            {
                error = "Item classname is required.";
                return false;
            }

            var matches = FindAllDuplicates(item.ClassName);

            if (matches.Count > 1 || (matches.Count == 1 && !ReferenceEquals(matches[0].item, item)))
            {
                var locations = string.Join(Environment.NewLine,
                    matches.Select(x => $"- {x.category.FileName}"));

                error = $"An item with classname '{item.ClassName}' already exists in:{Environment.NewLine}{locations}";
                return false;
            }

            var variantRefs = FindVariantReferences(item.ClassName);

            if (variantRefs.Count > 0)
            {
                var refsInOtherCategories = variantRefs
                    .Where(x => !ReferenceEquals(x.category, category))
                    .ToList();

                if (refsInOtherCategories.Count > 0)
                {
                    var locations = string.Join(Environment.NewLine,
                        refsInOtherCategories.Select(x =>
                            $"- {x.category.FileName} (variant of '{x.ownerItem.ClassName}')"));

                    error =
                        $"Cannot add '{item.ClassName}' as a full market item because it already exists as a variant in other category files:{Environment.NewLine}" +
                        locations;

                    return false;
                }

                var refsInSameCategory = variantRefs
                    .Where(x => ReferenceEquals(x.category, category))
                    .ToList();

                if (refsInSameCategory.Count > 0)
                {
                    requiresVariantPromotionConfirmation = true;
                }
            }

            return true;
        }
        internal bool AddMarketItem(ExpansionMarketCategory category, ExpansionMarketItem item, out string error)
        {
            error = null;

            if (category == null)
            {
                error = "Category was null.";
                return false;
            }

            if (item == null)
            {
                error = "Item was null.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(item.ClassName))
            {
                error = "Item classname is required.";
                return false;
            }

            if (category.Items == null)
                category.Items = new BindingList<ExpansionMarketItem>();

            if (!category.Items.Any(x => ReferenceEquals(x, item)))
                category.Items.Add(item);

            return true;
        }
        internal bool RemoveMarketItem(ExpansionMarketCategory category, ExpansionMarketItem item, out string error)
        {
            error = null;

            if (category == null)
            {
                error = "Category was null.";
                return false;
            }

            if (item == null)
            {
                error = "Item was null.";
                return false;
            }

            if (category.Items == null)
            {
                error = "Category has no items.";
                return false;
            }

            ExpansionMarketItem existing = category.Items.FirstOrDefault(x => ReferenceEquals(x, item));

            if (existing == null)
            {
                error = "Item was not found in the selected category.";
                return false;
            }

            category.Items.Remove(existing);
            return true;
        }
        internal bool MoveMarketItem(ExpansionMarketItem item, ExpansionMarketCategory destinationCategory, out string error)
        {
            error = null;

            if (item == null)
            {
                error = "Item was null.";
                return false;
            }

            if (destinationCategory == null)
            {
                error = "Destination category was null.";
                return false;
            }

            ExpansionMarketCategory sourceCategory = FindCategoryContainingItem(item);
            if (sourceCategory == null)
            {
                error = "Could not find the source category.";
                return false;
            }

            if (ReferenceEquals(sourceCategory, destinationCategory))
            {
                error = "The item is already in that category.";
                return false;
            }

            if (!RemoveMarketItem(sourceCategory, item, out error))
                return false;

            if (!AddMarketItem(destinationCategory, item, out error))
            {
                AddMarketItem(sourceCategory, item, out _); // rollback
                return false;
            }

            return true;
        }
        internal bool CanUseMarketItemClassName(ExpansionMarketItem item, string className)
        {
            if (string.IsNullOrWhiteSpace(className))
                return false;

            ExpansionMarketItem existing = FindMarketItemByClassName(className);
            return existing == null || ReferenceEquals(existing, item);
        }
        internal ExpansionMarketCategory GetCategoryByPath(string categoryPath)
        {
            if (string.IsNullOrWhiteSpace(categoryPath))
                return null;

            categoryPath = categoryPath.Trim()
                                       .Replace('/', '\\')
                                       .Trim('\\');

            if (!categoryPath.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                categoryPath += ".json";

            string fullPath = Path.GetFullPath(Path.Combine(FilePath, categoryPath));

            return Items.FirstOrDefault(category =>
                !string.IsNullOrWhiteSpace(category._path) &&
                string.Equals(
                    Path.GetFullPath(category._path),
                    fullPath,
                    StringComparison.OrdinalIgnoreCase));
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
        public void SetFolderParts(List<string> parts)
        {
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
            if (Id != other.Id) return false;

            if (_path != other._path ||
                m_Version != other.m_Version ||
                DisplayName != other.DisplayName ||
                Icon != other.Icon ||
                Color != other.Color ||
                InitStockPercent != other.InitStockPercent ||
                IsExchange != other.IsExchange )
                return false;

            if (!ListEquals(Items, other.Items))
                return false;

            if (!ListEquals(FolderParts, other.FolderParts))
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
                FolderParts = this.FolderParts != null
                    ? new List<string>(this.FolderParts.Select(x => x).ToList())
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
                MinStockThreshold != other.MinStockThreshold ||
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
