using System.ComponentModel;
using System.Xml.Serialization;

namespace Day2eEditor
{
    public class CfgrandompresetsConfig : ParameterizedMultiFileConfigLoaderBase<CfgrandompresetsFile>
    {
        public CfgrandompresetsConfig(string path) : base(path)
        {
        }
        protected override void LoadCore()
        {
            ResetState();

            if (string.IsNullOrWhiteSpace(VanillaPath))
            {
                HasErrors = true;
                _errors.Add("Vanilla path is missing.");
                return;
            }

            try
            {
                var vanilla = LoadItem(VanillaPath);
                vanilla.IsModded = false;
                vanilla.FileType = "cfgrandompresets";

                OnAfterItemLoad(vanilla, VanillaPath);
                _clonedItems[GetID(vanilla)] = vanilla.Clone();

                var vanillaIssues = ValidateItem(vanilla);
                if (vanillaIssues?.Any() == true)
                {
                    Console.WriteLine("Validation issues in " + vanilla.FileName + ":");
                    foreach (var msg in vanillaIssues)
                        Console.WriteLine("- " + msg);
                }

                MutableItems.Add(vanilla);

                if (vanilla.HasErrors)
                {
                    HasErrors = true;
                    var fileName = Path.GetFileName(vanilla.FilePath);
                    _errors.AddRange(vanilla.Errors.Select(e => $"[Vanilla] [{fileName}] {e}"));
                }
            }
            catch (Exception ex)
            {
                HasErrors = true;
                HandleItemError(VanillaPath, ex);
            }

            foreach (var file in ModPaths)
            {
                try
                {
                    var item = LoadItem(file);
                    item.IsModded = true;
                    item.FileType = "cfgrandompresets";
                    item.ModFolder = Path.GetRelativePath(BasePath, Path.GetDirectoryName(file) ?? BasePath);

                    OnAfterItemLoad(item, file);
                    _clonedItems[GetID(item)] = item.Clone();

                    var issues = ValidateItem(item);
                    if (issues?.Any() == true)
                    {
                        Console.WriteLine("Validation issues in " + item.FileName + ":");
                        foreach (var msg in issues)
                            Console.WriteLine("- " + msg);
                    }

                    MutableItems.Add(item);

                    if (item.HasErrors)
                    {
                        HasErrors = true;
                        var modName = Path.GetFileName(item.ModFolder);
                        var fileName = Path.GetFileName(item.FilePath);
                        _errors.AddRange(item.Errors.Select(e => $"[{modName}] [{fileName}] {e}"));
                    }
                }
                catch (Exception ex)
                {
                    HasErrors = true;
                    HandleItemError(file, ex);
                }
            }

            OnAfterLoadAll();
        }
        protected override CfgrandompresetsFile LoadItem(string filePath)
        {
            var item = new CfgrandompresetsFile(filePath);

            item.Data = AppServices.GetRequired<FileService>().LoadOrCreateXml(
                filePath,
                createNew: () => new randompresets
                {
                    Items = new BindingList<object>()
                },
                onError: ex =>
                {
                    item.HasErrors = true;

                    var message =
                        $"Error in {Path.GetFileName(filePath)}\n{ex.Message}\n{ex.InnerException?.Message}";

                    Console.WriteLine(message + "\n");
                    item.Errors.Add(message);
                },
                configName: "cfgrandompresets"
            );

            item.Data.Items ??= new BindingList<object>();
            item.SetPath(filePath);
            item.SetGuid(Guid.NewGuid());

            return item;
        }
        protected override IEnumerable<string> ValidateItem(CfgrandompresetsFile item)
        {
            return item.Errors;
        }
        public override bool NeedToSave()
        {
            foreach (var item in Items)
            {
                var id = GetID(item);

                if (ShouldDelete(item))
                    return true;

                if (!_clonedItems.TryGetValue(id, out var baseline))
                    return true;

                if (!item.Equals(baseline))
                    return true;
            }

            return false;
        }
        protected override void SaveItem(CfgrandompresetsFile item)
        {
            AppServices.GetRequired<FileService>().SaveXml(item.FilePath, item.Data);
            item.IsDirty = false;
        }
        protected override string GetItemFileName(CfgrandompresetsFile item)
            => item.FileName;
        protected override string GetItemFilePath(CfgrandompresetsFile CfgrandompresetsFile)
            => CfgrandompresetsFile.FilePath;
        protected override Guid GetID(CfgrandompresetsFile item)
            => item.Id;
        protected override bool ShouldDelete(CfgrandompresetsFile item)
            => item.ToDelete;
        protected override void DeleteItemFile(CfgrandompresetsFile item)
        {
            if (!string.IsNullOrWhiteSpace(item.FilePath) && File.Exists(item.FilePath))
            {
                File.Delete(item.FilePath);
            }
        }
        protected override void HandleItemError(string path, Exception ex)
        {
            var msg = $"Error in {Path.GetFileName(path)}: {ex.Message}";
            _errors.Add(msg);
            Console.WriteLine(msg);
        }
        private List<string> DeleteEmptyDirectoriesFromPath(string rootPath)
        {
            var removedFolders = new List<string>();

            if (!Directory.Exists(rootPath))
                return removedFolders;

            var directories = Directory
                .GetDirectories(rootPath, "*", SearchOption.AllDirectories)
                .OrderByDescending(d => d.Count(c =>
                    c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar))
                .ToList();

            foreach (var dir in directories)
            {
                if (!Directory.EnumerateFileSystemEntries(dir).Any())
                {
                    Directory.Delete(dir);
                    var relativePath = Path.GetRelativePath(rootPath, dir);
                    removedFolders.Add("Empty Folder Removed " + relativePath);
                }
            }

            return removedFolders;
        }
    }
    public class CfgrandompresetsFile : IDeepCloneable<CfgrandompresetsFile>, IEquatable<CfgrandompresetsFile>
    {
        private string _path;

        public string FilePath => _path;
        public string FileName => Path.GetFileName(_path);

        public bool ToDelete { get; set; }
        public bool IsDirty { get; set; }
        public bool HasErrors { get; set; }

        public List<string> Errors { get; } = new();

        public Guid Id { get; set; } = Guid.NewGuid();
        public string FileType { get; set; } = string.Empty;
        public bool IsModded { get; set; }
        public string ModFolder { get; set; } = string.Empty;

        public randompresets Data { get; set; } = new()
        {
            Items = new BindingList<object>()
        };
        public CfgrandompresetsFile(string path)
        {
            _path = path;
        }
        public void SetPath(string path) => _path = path;
        internal void SetGuid(Guid guid) => Id = guid;
        public CfgrandompresetsFile Clone()
        {
            var clone = new CfgrandompresetsFile(_path)
            {
                ToDelete = ToDelete,
                IsDirty = IsDirty,
                HasErrors = HasErrors,
                Id = Id,
                FileType = FileType,
                IsModded = IsModded,
                ModFolder = ModFolder,
                Data = Data?.Clone() ?? new randompresets
                {
                    Items = new BindingList<object>()
                }
            };

            clone.Errors.AddRange(Errors);
            return clone;
        }
        public bool Equals(CfgrandompresetsFile? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
                Id == other.Id &&
                string.Equals(_path, other._path, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(FileType, other.FileType, StringComparison.Ordinal) &&
                IsModded == other.IsModded &&
                string.Equals(ModFolder, other.ModFolder, StringComparison.OrdinalIgnoreCase) &&
                ToDelete == other.ToDelete &&
                Equals(Data, other.Data);
        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as CfgrandompresetsFile);
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class randompresets : IDeepCloneable<randompresets>, IEquatable<randompresets>
    {
        private BindingList<object>? itemsField = new();

        [XmlElement("attachments", typeof(randompresetsAttachments))]
        [XmlElement("cargo", typeof(randompresetsCargo))]
        public BindingList<object>? Items
        {
            get => itemsField;
            set => itemsField = value;
        }

        public bool Equals(randompresets? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return ListsEqual(Items, other.Items);
        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as randompresets);
        }
        public randompresets Clone()
        {
            var clonedItems = new BindingList<object>();

            if (Items != null)
            {
                foreach (var item in Items)
                {
                    if (item is randompresetsAttachments attachments)
                        clonedItems.Add(attachments.Clone());
                    else if (item is randompresetsCargo cargo)
                        clonedItems.Add(cargo.Clone());
                }
            }

            return new randompresets
            {
                Items = clonedItems
            };
        }
        private static bool ListsEqual(IList<object>? a, IList<object>? b)
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
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class randompresetsAttachments : IDeepCloneable<randompresetsAttachments>, IEquatable<randompresetsAttachments>
    {
        private BindingList<randompresetsItem>? itemField = new();
        private decimal chanceField;
        private string? nameField;

        [XmlElement("item")]
        public BindingList<randompresetsItem>? item
        {
            get => itemField;
            set => itemField = value;
        }
        [XmlAttribute]
        public decimal chance
        {
            get => chanceField;
            set => chanceField = value;
        }
        [XmlAttribute]
        public string? name
        {
            get => nameField;
            set => nameField = value;
        }

        public override string ToString()
        {
            return name ?? string.Empty;
        }
        public bool Equals(randompresetsAttachments? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return chance == other.chance
                && string.Equals(name, other.name, StringComparison.Ordinal)
                && ListsEqual(item, other.item);
        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as randompresetsAttachments);
        }
        public randompresetsAttachments Clone()
        {
            return new randompresetsAttachments
            {
                chance = chance,
                name = name,
                item = new BindingList<randompresetsItem>(
                    item?.Select(x => x.Clone()).ToList() ?? new List<randompresetsItem>())
            };
        }
        private static bool ListsEqual<T>(IList<T>? a, IList<T>? b)
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
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class randompresetsCargo : IDeepCloneable<randompresetsCargo>, IEquatable<randompresetsCargo>
    {
        private BindingList<randompresetsItem>? itemField = new();
        private decimal chanceField;
        private string? nameField;

        [XmlElement("item")]
        public BindingList<randompresetsItem>? item
        {
            get => itemField;
            set => itemField = value;
        }
        [XmlAttribute]
        public decimal chance
        {
            get => chanceField;
            set => chanceField = value;
        }
        [XmlAttribute]
        public string? name
        {
            get => nameField;
            set => nameField = value;
        }

        public override string ToString()
        {
            return name ?? string.Empty;
        }
        public bool Equals(randompresetsCargo? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return chance == other.chance
                && string.Equals(name, other.name, StringComparison.Ordinal)
                && ListsEqual(item, other.item);
        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as randompresetsCargo);
        }
        public randompresetsCargo Clone()
        {
            return new randompresetsCargo
            {
                chance = chance,
                name = name,
                item = new BindingList<randompresetsItem>(
                    item?.Select(x => x.Clone()).ToList() ?? new List<randompresetsItem>())
            };
        }
        private static bool ListsEqual<T>(IList<T>? a, IList<T>? b)
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
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class randompresetsItem : IDeepCloneable<randompresetsItem>, IEquatable<randompresetsItem>
    {
        private string? nameField;
        private decimal chanceField;

        [XmlAttribute]
        public string? name
        {
            get => nameField;
            set => nameField = value;
        }
        [XmlAttribute]

        public decimal chance
        {
            get => chanceField;
            set => chanceField = value;
        }
        public override string ToString()
        {
            return name ?? string.Empty;
        }
        public bool Equals(randompresetsItem? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
                string.Equals(name, other.name, StringComparison.Ordinal) &&
                chance == other.chance;
        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as randompresetsItem);
        }
        public randompresetsItem Clone()
        {
            return new randompresetsItem
            {
                name = name,
                chance = chance
            };
        }
    }
}
