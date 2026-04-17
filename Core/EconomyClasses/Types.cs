using System.ComponentModel;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Day2eEditor
{
    public class TypesConfig : ParameterizedMultiFileConfigLoaderBase<TypesFile>
    {
        public TypesConfig(string path) : base(path)
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
                vanilla.FileType = "types";

                OnAfterItemLoad(vanilla, VanillaPath);
                _clonedItems.Add(GetID(vanilla), vanilla.Clone());

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
                    item.FileType = "types";
                    item.ModFolder = Path.GetRelativePath(BasePath, Path.GetDirectoryName(file) ?? BasePath);

                    OnAfterItemLoad(item, file);
                    _clonedItems.Add(GetID(item), item.Clone());

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
        protected override TypesFile LoadItem(string filePath)
        {
            var item = new TypesFile(filePath);
            var loadFailed = false;

            item.Data = AppServices.GetRequired<FileService>().LoadOrCreateXml(
                filePath,
                createNew: () => new Types
                {
                    TypeList = new BindingList<TypeEntry>()
                },
                onError: ex =>
                {
                    loadFailed = true;
                    item.HasErrors = true;

                    var message =
                        $"Error in {Path.GetFileName(filePath)}\n{ex.Message}\n{ex.InnerException?.Message}";

                    Console.WriteLine(message + "\n");
                    item.Errors.Add(message);
                },
                configName: "Types"
            );

            item.Data.TypeList ??= new BindingList<TypeEntry>();
            item.SetPath(filePath);
            item.SetGuid(Guid.NewGuid());

            if (!loadFailed)
            {
                ValidateLoadedTypes(item, item.Data);
            }

            return item;
        }
        protected override IEnumerable<string> ValidateItem(TypesFile item)
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

        protected override void SaveItem(TypesFile item)
        {
            AppServices.GetRequired<FileService>().SaveXml(item.FilePath, item.Data);
            item.IsDirty = false;
        }
        protected override string GetItemFileName(TypesFile item)
                    => item.FileName;
        protected override Guid GetID(TypesFile item)
            => item.Id;
        protected override bool ShouldDelete(TypesFile item)
            => item.ToDelete;
        protected override void DeleteItemFile(TypesFile item)
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
        public TypeEntry? GetTypeByName(string name)
        {
            return Items
                .SelectMany(tf => tf.Data.TypeList)
                .LastOrDefault(te => te.Name == name);
        }
        public List<TypeEntry> SearchTypes(string searchTerm, bool exact = false)
        {
            var query = Items.SelectMany(tf => tf.Data.TypeList);

            if (exact)
            {
                return query
                    .Where(te => string.Equals(te.Name, searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return query
                .Where(te => !string.IsNullOrWhiteSpace(te.Name) &&
                             te.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        private void ValidateLoadedTypes(TypesFile item, Types cfg)
        {
            foreach (var type in cfg.TypeList)
            {
                CheckPropertiesRecursively(item, type, type?.Name ?? "UnknownEntry");
            }
        }
        private void CheckPropertiesRecursively(TypesFile owner, object obj, string topTypeName)
        {
            if (obj == null)
                return;

            var objType = obj.GetType();

            foreach (var property in objType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var propertyValue = property.GetValue(obj);

                if (propertyValue == null)
                    continue;

                if (typeof(System.Collections.IEnumerable).IsAssignableFrom(property.PropertyType)
                    && property.PropertyType != typeof(string))
                {
                    foreach (var item in (System.Collections.IEnumerable)propertyValue)
                    {
                        if (item != null)
                            CheckPropertiesRecursively(owner, item, topTypeName);
                    }

                    continue;
                }

                if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                {
                    CheckPropertiesRecursively(owner, propertyValue, topTypeName);
                    continue;
                }

                var specifiedProperty = objType.GetProperty(property.Name + "Specified");
                if (specifiedProperty != null && specifiedProperty.PropertyType == typeof(bool))
                {
                    var isSpecified = (bool)specifiedProperty.GetValue(obj)!;

                    if (isSpecified)
                    {
                        if (propertyValue == null)
                        {
                            owner.HasErrors = true;
                            owner.Errors.Add(
                                $"[{topTypeName}] → '{objType.Name}.{property.Name}' is null but marked as specified.");
                        }
                        else if (propertyValue is string str && string.IsNullOrWhiteSpace(str))
                        {
                            owner.HasErrors = true;
                            owner.Errors.Add(
                                $"[{topTypeName}] → '{objType.Name}.{property.Name}' is an empty string but marked as specified.");
                        }
                    }
                }
            }
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
    public class TypesFile : IDeepCloneable<TypesFile>, IEquatable<TypesFile>
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

        public Types Data { get; set; } = new()
        {
            TypeList = new BindingList<TypeEntry>()
        };

        public TypesFile(string path)
        {
            _path = path;
        }
        public void SetPath(string path) => _path = path;

        internal void SetGuid(Guid guid) => Id = guid;

        public TypesFile Clone()
        {
            var clone = new TypesFile(_path)
            {
                ToDelete = ToDelete,
                IsDirty = IsDirty,
                HasErrors = HasErrors,
                Id = Id,
                FileType = FileType,
                IsModded = IsModded,
                ModFolder = ModFolder,
                Data = Data?.Clone() ?? new Types
                {
                    TypeList = new BindingList<TypeEntry>()
                }
            };

            clone.Errors.AddRange(Errors);
            return clone;
        }

        public bool Equals(TypesFile? other)
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
            return Equals(obj as TypesFile);
        }
    }


    [XmlRoot("types")]
    public class Types : IDeepCloneable<Types>, IEquatable<Types>
    {
        public Types() { }

        private BindingList<TypeEntry>? _typeList = new();

        [XmlElement("type")]
        public BindingList<TypeEntry>? TypeList
        {
            get => _typeList;
            set => _typeList = value;
        }

        public bool Equals(Types? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return ListsEqual(TypeList, other.TypeList);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Types);
        }

        public Types Clone()
        {
            return new Types
            {
                TypeList = new BindingList<TypeEntry>(
                    TypeList?.Select(x => x.Clone()).ToList() ?? new List<TypeEntry>())
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

    public class TypeEntry : IDeepCloneable<TypeEntry>, IEquatable<TypeEntry>
    {
        private string? _name;
        private bool _nameSpecified;

        private int _nominal;
        private bool _nominalSpecified;

        private int _lifetime;
        private bool _lifetimeSpecified;

        private int _restock;
        private bool _restockSpecified;

        private int _min;
        private bool _minSpecified;

        private int _quantMin;
        private bool _quantMinSpecified;

        private int _quantMax;
        private bool _quantMaxSpecified;

        private int _cost;
        private bool _costSpecified;

        private Flags? _flags;
        private Category? _category;

        private BindingList<Usage>? _usages = new();
        private BindingList<Tag>? _tags = new();
        private BindingList<Value>? _values = new();

        [XmlAttribute("name")]
        public string? Name
        {
            get => _name;
            set => _name = value;
        }

        [XmlIgnore]
        public bool NameSpecified
        {
            get => _nameSpecified;
            set => _nameSpecified = value;
        }

        [XmlElement("nominal")]
        public int Nominal
        {
            get => _nominal;
            set => _nominal = value;
        }

        [XmlIgnore]
        public bool NominalSpecified
        {
            get => _nominalSpecified;
            set => _nominalSpecified = value;
        }

        [XmlElement("lifetime")]
        public int Lifetime
        {
            get => _lifetime;
            set => _lifetime = value;
        }

        [XmlIgnore]
        public bool LifetimeSpecified
        {
            get => _lifetimeSpecified;
            set => _lifetimeSpecified = value;
        }

        [XmlElement("restock")]
        public int Restock
        {
            get => _restock;
            set => _restock = value;
        }

        [XmlIgnore]
        public bool RestockSpecified
        {
            get => _restockSpecified;
            set => _restockSpecified = value;
        }

        [XmlElement("min")]
        public int Min
        {
            get => _min;
            set => _min = value;
        }

        [XmlIgnore]
        public bool MinSpecified
        {
            get => _minSpecified;
            set => _minSpecified = value;
        }

        [XmlElement("quantmin")]
        public int QuantMin
        {
            get => _quantMin;
            set => _quantMin = value;
        }

        [XmlIgnore]
        public bool QuantMinSpecified
        {
            get => _quantMinSpecified;
            set => _quantMinSpecified = value;
        }

        [XmlElement("quantmax")]
        public int QuantMax
        {
            get => _quantMax;
            set => _quantMax = value;
        }

        [XmlIgnore]
        public bool QuantMaxSpecified
        {
            get => _quantMaxSpecified;
            set => _quantMaxSpecified = value;
        }

        [XmlElement("cost")]
        public int Cost
        {
            get => _cost;
            set => _cost = value;
        }

        [XmlIgnore]
        public bool CostSpecified
        {
            get => _costSpecified;
            set => _costSpecified = value;
        }

        [XmlElement("flags")]
        public Flags? Flags
        {
            get => _flags;
            set => _flags = value;
        }

        [XmlElement("category")]
        public Category? Category
        {
            get => _category;
            set => _category = value;
        }

        [XmlElement("usage")]
        public BindingList<Usage>? Usages
        {
            get => _usages;
            set => _usages = value;
        }

        [XmlElement("tag")]
        public BindingList<Tag>? Tags
        {
            get => _tags;
            set => _tags = value;
        }

        [XmlElement("value")]
        public BindingList<Value>? Values
        {
            get => _values;
            set => _values = value;
        }

        public override string ToString()
        {
            return Name ?? string.Empty;
        }

        public bool Equals(TypeEntry? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
                Name == other.Name &&
                NameSpecified == other.NameSpecified &&

                Nominal == other.Nominal &&
                NominalSpecified == other.NominalSpecified &&

                Lifetime == other.Lifetime &&
                LifetimeSpecified == other.LifetimeSpecified &&

                Restock == other.Restock &&
                RestockSpecified == other.RestockSpecified &&

                Min == other.Min &&
                MinSpecified == other.MinSpecified &&

                QuantMin == other.QuantMin &&
                QuantMinSpecified == other.QuantMinSpecified &&

                QuantMax == other.QuantMax &&
                QuantMaxSpecified == other.QuantMaxSpecified &&

                Cost == other.Cost &&
                CostSpecified == other.CostSpecified &&

                Equals(Flags, other.Flags) &&
                Equals(Category, other.Category) &&
                ListsEqual(Usages, other.Usages) &&
                ListsEqual(Tags, other.Tags) &&
                ListsEqual(Values, other.Values);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as TypeEntry);
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

        public void AddTier(string tier)
        {
            Values ??= new BindingList<Value>();

            var newTier = new Value { Name = tier, NameSpecified = true };

            if (!Values.Any(x => x.Name == newTier.Name))
                Values.Add(newTier);

            for (int i = 0; i < Values.Count; i++)
            {
                if (Values[i].Name == null)
                {
                    Values.RemoveAt(i);
                    i--;
                }
            }
        }

        public void RemoveTier(string tier)
        {
            if (Values == null)
                return;

            var existing = Values.FirstOrDefault(x => x.Name == tier);
            if (existing != null)
                Values.Remove(existing);

            if (Values.Count == 0)
                Values = null;
        }

        public void AddUserTier(string tier)
        {
            Values ??= new BindingList<Value>();

            var newUserTier = new Value { User = tier, UserSpecified = true };

            if (!Values.Any(x => x.User == newUserTier.User))
                Values.Add(newUserTier);

            for (int i = 0; i < Values.Count; i++)
            {
                if (Values[i].User == null)
                {
                    Values.RemoveAt(i);
                    i--;
                }
            }
        }

        public void RemoveUserTier(string tier)
        {
            if (Values == null)
                return;

            var existing = Values.FirstOrDefault(x => x.User == tier);
            if (existing != null)
                Values.Remove(existing);

            if (Values.Count == 0)
                Values = null;
        }

        public void RemoveTiers()
        {
            if (Values != null)
                Values = null;
        }

        public void AddNewUsage(listsUsage u)
        {
            Usages ??= new BindingList<Usage>();

            if (!Usages.Any(x => x.Name == u.name))
            {
                Usages.Add(new Usage
                {
                    Name = u.name,
                    NameSpecified = true
                });
            }
        }

        public void AddNewUserUsage(user_listsUser uu)
        {
            Usages ??= new BindingList<Usage>();

            if (!Usages.Any(x => x.User == uu.name))
            {
                Usages.Add(new Usage
                {
                    User = uu.name,
                    UserSpecified = true
                });
            }
        }

        public void RemoveUsage(Usage u)
        {
            if (Usages == null)
                return;

            var usageToRemove = Usages.FirstOrDefault(x => x.Name == u.Name);
            if (usageToRemove != null)
                Usages.Remove(usageToRemove);
        }

        public void AddNewTag(listsTag t)
        {
            Tags ??= new BindingList<Tag>();

            if (!Tags.Any(x => x.Name == t.name))
            {
                Tags.Add(new Tag
                {
                    Name = t.name,
                    NameSpecified = true
                });
            }
        }

        public void RemoveTag(Tag t)
        {
            if (Tags == null)
                return;

            var tagToRemove = Tags.FirstOrDefault(x => x.Name == t.Name);
            if (tagToRemove != null)
                Tags.Remove(tagToRemove);
        }

        public void ChangeCategory(listsCategory c)
        {
            var cat = new Category
            {
                Name = c.name
            };

            if (cat.Name == "other")
            {
                Category = null;
            }
            else
            {
                cat.NameSpecified = true;
                Category = cat;
            }
        }

        public TypeEntry Clone()
        {
            return new TypeEntry
            {
                Name = Name,
                NameSpecified = NameSpecified,
                Nominal = Nominal,
                NominalSpecified = NominalSpecified,
                Lifetime = Lifetime,
                LifetimeSpecified = LifetimeSpecified,
                Restock = Restock,
                RestockSpecified = RestockSpecified,
                Min = Min,
                MinSpecified = MinSpecified,
                QuantMin = QuantMin,
                QuantMinSpecified = QuantMinSpecified,
                QuantMax = QuantMax,
                QuantMaxSpecified = QuantMaxSpecified,
                Cost = Cost,
                CostSpecified = CostSpecified,

                Flags = Flags?.Clone(),
                Category = Category?.Clone(),

                Usages = new BindingList<Usage>(Usages?.Select(u => u.Clone()).ToList() ?? new List<Usage>()),
                Tags = new BindingList<Tag>(Tags?.Select(t => t.Clone()).ToList() ?? new List<Tag>()),
                Values = new BindingList<Value>(Values?.Select(v => v.Clone()).ToList() ?? new List<Value>())
            };
        }
    }

    public class Flags : IDeepCloneable<Flags>, IEquatable<Flags>
    {
        private int _countInCargo;
        private int _countInHoarder;
        private int _countInMap;
        private int _countInPlayer;
        private int _crafted;
        private int _deloot;

        [XmlAttribute("count_in_cargo")]
        public int count_in_cargo
        {
            get => _countInCargo;
            set => _countInCargo = value;
        }

        [XmlAttribute("count_in_hoarder")]
        public int count_in_hoarder
        {
            get => _countInHoarder;
            set => _countInHoarder = value;
        }

        [XmlAttribute("count_in_map")]
        public int count_in_map
        {
            get => _countInMap;
            set => _countInMap = value;
        }

        [XmlAttribute("count_in_player")]
        public int count_in_player
        {
            get => _countInPlayer;
            set => _countInPlayer = value;
        }

        [XmlAttribute("crafted")]
        public int crafted
        {
            get => _crafted;
            set => _crafted = value;
        }

        [XmlAttribute("deloot")]
        public int deloot
        {
            get => _deloot;
            set => _deloot = value;
        }

        public bool Equals(Flags? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
                count_in_cargo == other.count_in_cargo &&
                count_in_hoarder == other.count_in_hoarder &&
                count_in_map == other.count_in_map &&
                count_in_player == other.count_in_player &&
                crafted == other.crafted &&
                deloot == other.deloot;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Flags);
        }

        public override string ToString() => DisplayString;

        [XmlIgnore]
        public string DisplayString
        {
            get
            {
                var flags = new List<string>();

                if (count_in_cargo == 1) flags.Add("count_in_cargo");
                if (count_in_hoarder == 1) flags.Add("count_in_hoarder");
                if (count_in_map == 1) flags.Add("count_in_map");
                if (count_in_player == 1) flags.Add("count_in_player");
                if (crafted == 1) flags.Add("crafted");
                if (deloot == 1) flags.Add("deloot");

                return string.Join(", ", flags);
            }
        }

        public Flags Clone() => new Flags
        {
            count_in_cargo = count_in_cargo,
            count_in_hoarder = count_in_hoarder,
            count_in_map = count_in_map,
            count_in_player = count_in_player,
            crafted = crafted,
            deloot = deloot
        };
    }

    public class Category : IDeepCloneable<Category>, IEquatable<Category>
    {
        private string? _name;
        private bool _nameSpecified;

        [XmlAttribute("name")]
        public string? Name
        {
            get => _name;
            set => _name = value;
        }

        [XmlIgnore]
        public bool NameSpecified
        {
            get => _nameSpecified;
            set => _nameSpecified = value;
        }

        public bool Equals(Category? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
                Name == other.Name &&
                NameSpecified == other.NameSpecified;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Category);
        }

        public override string ToString()
        {
            return Name ?? string.Empty;
        }

        public Category Clone() => new Category
        {
            Name = Name,
            NameSpecified = NameSpecified
        };
    }

    public class Usage : IDeepCloneable<Usage>, IEquatable<Usage>
    {
        private string? _name;
        private string? _user;
        private bool _nameSpecified;
        private bool _userSpecified;

        [XmlAttribute("name")]
        public string? Name
        {
            get => _name;
            set => _name = value;
        }

        [XmlIgnore]
        public bool NameSpecified
        {
            get => _nameSpecified;
            set => _nameSpecified = value;
        }

        [XmlAttribute("user")]
        public string? User
        {
            get => _user;
            set => _user = value;
        }

        [XmlIgnore]
        public bool UserSpecified
        {
            get => _userSpecified;
            set => _userSpecified = value;
        }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(Name) && string.IsNullOrWhiteSpace(User))
                return Name;

            if (string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(User))
                return User;

            return string.Empty;
        }

        public bool Equals(Usage? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
                Name == other.Name &&
                NameSpecified == other.NameSpecified &&
                User == other.User &&
                UserSpecified == other.UserSpecified;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Usage);
        }

        public Usage Clone() => new Usage
        {
            Name = Name,
            NameSpecified = NameSpecified,
            User = User,
            UserSpecified = UserSpecified
        };
    }

    public class Tag : IDeepCloneable<Tag>, IEquatable<Tag>
    {
        private string? _name;
        private bool _nameSpecified;

        [XmlAttribute("name")]
        public string? Name
        {
            get => _name;
            set => _name = value;
        }

        [XmlIgnore]
        public bool NameSpecified
        {
            get => _nameSpecified;
            set => _nameSpecified = value;
        }

        public override string ToString()
        {
            return Name ?? string.Empty;
        }

        public bool Equals(Tag? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
                Name == other.Name &&
                NameSpecified == other.NameSpecified;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Tag);
        }

        public Tag Clone() => new Tag
        {
            Name = Name,
            NameSpecified = NameSpecified
        };
    }

    public class Value : IDeepCloneable<Value>, IEquatable<Value>
    {
        private string? _name;
        private bool _nameSpecified;

        private string? _user;
        private bool _userSpecified;

        [XmlAttribute("name")]
        public string? Name
        {
            get => _name;
            set => _name = value;
        }

        [XmlIgnore]
        public bool NameSpecified
        {
            get => _nameSpecified;
            set => _nameSpecified = value;
        }

        [XmlAttribute("user")]
        public string? User
        {
            get => _user;
            set => _user = value;
        }

        [XmlIgnore]
        public bool UserSpecified
        {
            get => _userSpecified;
            set => _userSpecified = value;
        }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(Name) && string.IsNullOrWhiteSpace(User))
                return Name;

            if (string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(User))
                return User;

            return string.Empty;
        }

        public bool Equals(Value? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
                Name == other.Name &&
                NameSpecified == other.NameSpecified &&
                User == other.User &&
                UserSpecified == other.UserSpecified;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Value);
        }

        public Value Clone() => new Value
        {
            Name = Name,
            NameSpecified = NameSpecified,
            User = User,
            UserSpecified = UserSpecified
        };
    }
}
