using System.ComponentModel;
using System.Xml.Serialization;

namespace Day2eEditor
{
    public class GlobalsConfig : ParameterizedMultiFileConfigLoaderBase<GlobalsFile>
    {
        public GlobalsConfig(string path) : base(path)
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
                vanilla.FileType = "globals";

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
                    item.FileType = "globals";
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

        protected override GlobalsFile LoadItem(string filePath)
        {
            var item = new GlobalsFile(filePath);

            item.Data = AppServices.GetRequired<FileService>().LoadOrCreateXml(
                filePath,
                createNew: () => new variables
                {
                    var = new BindingList<variablesVar>()
                },
                onError: ex =>
                {
                    item.HasErrors = true;

                    var message =
                        $"Error in {Path.GetFileName(filePath)}\n{ex.Message}\n{ex.InnerException?.Message}";

                    Console.WriteLine(message + "\n");
                    item.Errors.Add(message);
                },
                configName: "globals"
            );

            item.Data.var ??= new BindingList<variablesVar>();
            item.SetPath(filePath);
            item.SetGuid(Guid.NewGuid());

            return item;
        }

        protected override IEnumerable<string> ValidateItem(GlobalsFile item)
        {
            return item.Errors;
        }

        public override IEnumerable<string> Save()
        {
            var saved = new List<string>();

            for (int i = MutableItems.Count - 1; i >= 0; i--)
            {
                var item = MutableItems[i];
                var id = GetID(item);
                var fileName = GetItemFileName(item);

                if (ShouldDelete(item))
                {
                    DeleteItemFile(item);
                    MutableItems.RemoveAt(i);
                    _clonedItems.Remove(id);
                    saved.Add("File Remove " + fileName);
                    continue;
                }

                if (!_clonedItems.TryGetValue(id, out var baseline))
                {
                    SaveItem(item);
                    _clonedItems[id] = item.Clone();
                    saved.Add(fileName);
                    continue;
                }

                if (!item.Equals(baseline))
                {
                    var oldPath = baseline.FilePath;

                    SaveItem(item);

                    if (!string.Equals(oldPath, item.FilePath, StringComparison.OrdinalIgnoreCase) &&
                        !string.IsNullOrWhiteSpace(oldPath) &&
                        File.Exists(oldPath))
                    {
                        File.Delete(oldPath);
                    }

                    _clonedItems[id] = item.Clone();
                    saved.Add(fileName);
                }
            }

            return saved;
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

        protected override void SaveItem(GlobalsFile item)
        {
            AppServices.GetRequired<FileService>().SaveXml(item.FilePath, item.Data);
            item.IsDirty = false;
        }

        protected override string GetItemFileName(GlobalsFile item)
            => item.FileName;

        protected override Guid GetID(GlobalsFile item)
            => item.Id;

        protected override bool ShouldDelete(GlobalsFile item)
            => item.ToDelete;

        protected override void DeleteItemFile(GlobalsFile item)
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
    public class GlobalsFile : IDeepCloneable<GlobalsFile>, IEquatable<GlobalsFile>
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

        public variables Data { get; set; } = new()
        {
            var = new BindingList<variablesVar>()
        };

        public GlobalsFile(string path)
        {
            _path = path;
        }

        public void SetPath(string path) => _path = path;

        internal void SetGuid(Guid guid) => Id = guid;

        public GlobalsFile Clone()
        {
            var clone = new GlobalsFile(_path)
            {
                ToDelete = ToDelete,
                IsDirty = IsDirty,
                HasErrors = HasErrors,
                Id = Id,
                FileType = FileType,
                IsModded = IsModded,
                ModFolder = ModFolder,
                Data = Data?.Clone() ?? new variables
                {
                    var = new BindingList<variablesVar>()
                }
            };

            clone.Errors.AddRange(Errors);
            return clone;
        }

        public bool Equals(GlobalsFile? other)
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
            return Equals(obj as GlobalsFile);
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class variables : IDeepCloneable<variables>, IEquatable<variables>
    {
        private BindingList<variablesVar>? varField = new();

        [XmlElement("var")]
        public BindingList<variablesVar>? var
        {
            get => varField;
            set => varField = value;
        }

        public variables()
        {
        }
        public bool Equals(variables? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return ListsEqual(var, other.var);
        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as variables);
        }
        public variables Clone()
        {
            return new variables
            {
                var = new BindingList<variablesVar>(
                    var?.Select(x => x.Clone()).ToList() ?? new List<variablesVar>())
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
    public partial class variablesVar : IDeepCloneable<variablesVar>, IEquatable<variablesVar>
    {
        private string? nameField;
        private int typeField;
        private string? valueField;

        [XmlAttribute]
        public string? name
        {
            get => nameField;
            set => nameField = value;
        }
        [XmlAttribute]
        public int type
        {
            get => typeField;
            set => typeField = value;
        }
        [XmlAttribute]
        public string? value
        {
            get => valueField;
            set => valueField = value;
        }
        [XmlIgnore]
        public object? TypedValue
        {
            get
            {
                return type switch
                {
                    0 => int.TryParse(value, out var intVal) ? intVal : 0,
                    1 => decimal.TryParse(value, out var decVal) ? decVal : 0m,
                    _ => value
                };
            }
            set
            {
                switch (type)
                {
                    case 0:
                        this.value = Convert.ToInt32(value).ToString();
                        break;
                    case 1:
                        this.value = Convert.ToDecimal(value).ToString();
                        break;
                    default:
                        this.value = value?.ToString();
                        break;
                }
            }
        }

        public bool Equals(variablesVar? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
                string.Equals(name, other.name, StringComparison.Ordinal) &&
                type == other.type &&
                string.Equals(value, other.value, StringComparison.Ordinal);
        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as variablesVar);
        }
        public variablesVar Clone()
        {
            return new variablesVar
            {
                name = name,
                type = type,
                value = value
            };
        }
    }
}
