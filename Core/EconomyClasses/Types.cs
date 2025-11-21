using System.ComponentModel;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Day2eEditor
{
    public class TypesConfig : IAdvancedConfigLoader
    {
        public string FileName => Path.GetFileName(_basepath); // e.g., "types.xml"
        public string FilePath => _basepath;
        public string _basepath { get; set; }
        public List<TypesFile> AllData { get; private set; } = new List<TypesFile>();
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();

        public void Load() => throw new InvalidOperationException("Use LoadWithParameters for this config.");
        public void LoadWithParameters(string basePath, string vanillaPath, List<string> modPaths)
        {
            _basepath = basePath;
            HasErrors = false;
            Errors.Clear();
            // Load vanilla file
            var vanilla = new TypesFile(vanillaPath)
            {
                IsModded = false,
                FileType = "types"
            };

            vanilla.Load();
            AllData.Add(vanilla);

            if (vanilla.HasErrors)
            {
                HasErrors = true;
                var fileName = Path.GetFileName(vanilla.FilePath);
                Errors.AddRange(vanilla.Errors.Select(e => $"[Vanilla] [{fileName}] {e}"));
            }

            // Load mod files
            foreach (var modPath in modPaths)
            {
                var modFile = new TypesFile(modPath)
                {
                    IsModded = true,
                    FileType = "types",
                    ModFolder = Path.GetRelativePath(basePath, Path.GetDirectoryName(modPath))
                };

                modFile.Load();
                AllData.Add(modFile);

                if (modFile.HasErrors)
                {
                    HasErrors = true;
                    var modName = Path.GetFileName(modFile.ModFolder);
                    var fileName = Path.GetFileName(modFile.FilePath);
                    Errors.AddRange(modFile.Errors.Select(e => $"[{modName}] [{fileName}] {e}"));
                }
            }
        }
        public IEnumerable<string> Save()
        {
            var savedFiles = new List<string>();

            foreach (var data in AllData.ToList())
            {
                var result = data.Save();
                savedFiles.AddRange(result);

                if (data.ToDelete)
                {
                    AllData.Remove(data); // cleanup after deleting
                }
            }

            return savedFiles;
        }
        public bool needToSave()
        {
            foreach(var Data in AllData)
            {
                if (Data.needToSave())
                    return true;
            }
            return false;
        }

        public TypeEntry Gettypebyname(string name)
        {
            return AllData
                    .SelectMany(tf => tf.Data.TypeList)
                    .Where(te => te.Name == name)
                    .LastOrDefault();

        }
        public List<TypeEntry> SerachTypes(string Searchterm, bool exact = false)
        {
            if (exact)
                return AllData
                    .SelectMany(tf => tf.Data.TypeList)
                    .Where(te => te.Name == Searchterm)
                    .ToList();
            else
                return AllData
                    .SelectMany(tf => tf.Data.TypeList)
                    .Where(te => te.Name.ToLower().Contains(Searchterm.ToLower()))
                    .ToList();
        }
    }
    public class TypesFile : IConfigLoader
    {
        private readonly string _path;

        public Types Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }
        public bool ToDelete { get; set; }

        // Metadata for file type and source
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;                  // Full file path
        public string FileType { get; set; }               // "types"
        public bool IsModded { get; set; }                 // true if modded, false if vanilla
        public string ModFolder { get; set; }              // Only set for modded types

        public TypesFile(string path)
        {
            _path = path;
        }
        public void CreateNew()
        {
            Data = new Types()
            {
                TypeList = new BindingList<TypeEntry>()
            };
        }
        public void Load()
        {
            Data = AppServices.GetRequired<FileService>().LoadOrCreateXml<Types>(
               _path,
               createNew: () => new Types(),
               onAfterLoad: cfg => 
               {
                   CheckValuesAfterLoad(cfg);
                   if (HasErrors)
                   {
                       throw new Exception("Validation failed.");
                   }
               },
               onError: ex =>
               {
                   HasErrors = true;

                   if (ex.Message == "Validation failed.")
                   {
                       Console.WriteLine("Validation errors found:\n" + string.Join("\n", Errors));
                   }
                   else
                   {
                       var message = "Error in " + Path.GetFileName(_path) + "\n" +
                                     ex.Message + "\n" +
                                     ex.InnerException?.Message;

                       Console.WriteLine(message + "\n");
                       Errors.Add(message);
                   }
               },
               configName: "Types"
            );
        }
        public IEnumerable<string> Save()
        {
            if (ToDelete)
            {
                if (File.Exists(_path))
                {
                    File.Delete(_path);
                    // Delete empty directories if needed
                    ShellHelper.DeleteEmptyFoldersUpToBase(Path.GetDirectoryName(_path), AppServices.GetRequired<EconomyManager>().basePath);
                    return new[] { FileName + " (deleted)" };
                }
                return Array.Empty<string>();
            }

            else if (isDirty)
            {
                AppServices.GetRequired<FileService>().SaveXml(_path, Data);
                isDirty = false;
                return new[] { FileName };
            }

            return Array.Empty<string>();
        }
        public bool needToSave()
        {
            return isDirty;
        }
        private void CheckValuesAfterLoad(Types cfg)
        {
            foreach (var type in cfg.TypeList)
            {
                // Recursively check each property in the type
                CheckPropertiesRecursively(type, (type as TypeEntry)?.Name ?? "UnknownEntry");
            }
        }
        private void CheckPropertiesRecursively(object obj, string topTypeName)
        {
            if (obj == null)
                return;

            Type objType = obj.GetType();

            foreach (var property in objType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                object propertyValue = property.GetValue(obj);

                // Skip nulls
                if (propertyValue == null)
                    continue;

                // Handle collections like BindingList<T>, List<T>, etc.
                if (typeof(System.Collections.IEnumerable).IsAssignableFrom(property.PropertyType)
                    && property.PropertyType != typeof(string))
                {
                    foreach (var item in (System.Collections.IEnumerable)propertyValue)
                    {
                        CheckPropertiesRecursively(item, topTypeName); // Recursively check each item
                    }
                    continue; // skip rest of logic for collections
                }

                // Handle nested complex types (but not strings or value types)
                if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                {
                    CheckPropertiesRecursively(propertyValue, topTypeName); // Recursive for objects
                    continue;
                }

                // Handle Specified logic
                var specifiedProperty = objType.GetProperty(property.Name + "Specified");
                if (specifiedProperty != null && specifiedProperty.PropertyType == typeof(bool))
                {
                    bool isSpecified = (bool)specifiedProperty.GetValue(obj);

                    if (isSpecified)
                    {
                        if (propertyValue == null)
                        {
                            HasErrors = true;
                            Errors.Add($"[{topTypeName}] → '{objType.Name}.{property.Name}' is null but marked as specified.");
                        }
                        else if (propertyValue is string str && string.IsNullOrWhiteSpace(str))
                        {
                            HasErrors = true;
                            Errors.Add($"[{topTypeName}] → '{objType.Name}.{property.Name}' is an empty string but marked as specified.");
                        }
                    }
                }
            }
        }

    }


    [XmlRoot("types")]
    public class Types
    {
        public Types() { }
        private BindingList<TypeEntry> _typeList = new();

        [XmlElement("type")]
        public BindingList<TypeEntry> TypeList
        {
            get => _typeList;
            set => _typeList = value;
        }
    }

    public class TypeEntry
    {
        private string _name;
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

        private Flags _flags;
        private Category _category;

        private BindingList<Usage> _usages = new();
        private BindingList<Tag> _tags = new();
        private BindingList<Value> _values = new();

        [XmlAttribute("name")]
        public string Name
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
        public Flags Flags
        {
            get => _flags;
            set => _flags = value;
        }

        [XmlElement("category")]
        public Category Category
        {
            get => _category;
            set => _category = value;
        }

        [XmlElement("usage")]
        public BindingList<Usage> Usages
        {
            get => _usages;
            set => _usages = value;
        }

        [XmlElement("tag")]
        public BindingList<Tag> Tags
        {
            get => _tags;
            set => _tags = value;
        }

        [XmlElement("value")]
        public BindingList<Value> Values
        {
            get => _values;
            set => _values = value;
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (obj is not TypeEntry other)
                return false;

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
        private static bool ListsEqual<T>(IList<T> a, IList<T> b)
        {
            if (a == b) return true;
            if (a == null || b == null) return false;
            if (a.Count != b.Count) return false;

            for (int i = 0; i < a.Count; i++)
            {
                if (!Equals(a[i], b[i]))
                    return false;
            }
            return true;
        }
        public void AddTier(string tier)
        {
            if (Values == null)
                Values = new BindingList<Value>();
            Value newtier = (new Value() { Name = tier, NameSpecified = true });
            if (!Values.Any(x => x.Name == newtier.Name))
                Values.Add(newtier);
            for (int i = 0; i < Values.Count; i++)
            {
                if (Values[i].Name == null)
                {
                    Values.RemoveAt(i);
                    i--;
                }
            }
        }
        public void removetier(string tier)
        {
            if (Values == null) return;
            if (Values.Any(x => x.Name == tier))
                Values.Remove(Values.First(X => X.Name == tier));
            if (Values.Count == 0)
                Values = null;
        }
        public void AdduserTier(string tier)
        {
            if (Values == null)
                Values = new BindingList<Value>();
            Value newusertier = new Value() { User = tier, UserSpecified = true};
            if (!Values.Any(x => x.User == newusertier.User))
                Values.Add(newusertier);
            for (int i = 0; i < Values.Count; i++)
            {
                if (Values[i].User == null)
                {
                    Values.RemoveAt(i);
                    i--;
                }
            }
        }
        public void removeusertier(string tier)
        {
            if (Values == null) return;
            if (Values.Any(x => x.User == tier))
                Values.Remove(Values.First(X => X.User == tier));
            if (Values.Count == 0)
            {
                Values = null;
            }
        }
        public void removetiers()
        {
            if (Values != null)
                Values = null;
        }
        public void AddnewUsage(listsUsage u)
        {
            if (Usages == null)
                Usages = new BindingList<Usage>();
            if (!Usages.Any(x => x.Name == u.name))
            {
                Usages.Add(new Usage() { Name = u.name, NameSpecified = true });
            }
        }
        public void AddnewUserUsage(user_listsUser uu)
        {
            if (Usages == null)
                Usages = new BindingList<Usage>();
            if (!Usages.Any(x => x.User == uu.name))
            {
                Usages.Add(new Usage() { User = uu.name, UserSpecified = true });
            }
        }
        public void removeusage(Usage u)
        {
            if (Usages == null) return;
            Usage usagetoremove = Usages.FirstOrDefault(x => x.Name == u.Name);
            if (usagetoremove != null)
                Usages.Remove(usagetoremove);
        }
        public void Addnewtag(listsTag t)
        {
            if (Tags == null)
                Tags = new BindingList<Tag>();
            if (!Tags.Any(x => x.Name == t.name))
            {
                Tags.Add(new Tag() { Name = t.name, NameSpecified = true});
            }
        }
        public void removetag(Tag t)
        {
            if (Tags == null) return;
            Tag tagtoremove = Tags.FirstOrDefault(x => x.Name == t.Name);
            if (tagtoremove != null)
                Tags.Remove(tagtoremove);
        }
        public void changecategory(listsCategory c)
        {
            Category cat = new Category()
            {
                Name = c.name
            };
            if (cat.Name == "other")
                Category = null;
            else
            {
                Category = cat;
                Category.NameSpecified = true;
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

    public class Flags
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

        public override bool Equals(object obj)
        {
            if (obj is not Flags other)
                return false;

            return
                count_in_cargo == other.count_in_cargo &&
                count_in_hoarder == other.count_in_hoarder &&
                count_in_map == other.count_in_map &&
                count_in_player == other.count_in_player &&
                crafted == other.crafted &&
                deloot == other.deloot;
        }

        public override string ToString() => DisplayString;

        [XmlIgnore]
        public string DisplayString
        {
            get
            {
                List<string> flags = new();
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

    public class Category
    {
        private string _name;
        private bool _nameSpecified;

        [XmlAttribute("name")]
        public string Name
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

        public override bool Equals(object obj)
        {
            if (obj is not Category other)
                return false;

            return
                Name == other.Name &&
                NameSpecified == other.NameSpecified;
        }
        public override string ToString()
        {
            return Name;
        }
        public Category Clone() => new Category
        {
            Name = Name,
            NameSpecified = NameSpecified
        };
    }

    public class Usage
    {
        private string _name;
        private string _user;
        private bool _nameSpecified;
        private bool _userSpecified;

        [XmlAttribute("name")]
        public string Name
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
        public string User
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
            string r = "";
            if (Name != null && User == null)
                r = Name;
            else if (Name == null && User != null)
                r = User;
            return r;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Usage other)
                return false;

            return
                Name == other.Name &&
                NameSpecified == other.NameSpecified &&
                User == other.User &&
                UserSpecified == other.UserSpecified;
        }
        public Usage Clone() => new Usage
        {
            Name = Name,
            NameSpecified = NameSpecified,
            User = User,
            UserSpecified = UserSpecified
        };
    }

    public class Tag
    {
        private string _name;
        private bool _nameSpecified;

        [XmlAttribute("name")]
        public string Name
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
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Tag other)
                return false;

            return
                Name == other.Name &&
                NameSpecified == other.NameSpecified;
        }
        public Tag Clone() => new Tag
        {
            Name = Name,
            NameSpecified = NameSpecified
        };
    }

    public class Value
    {
        private string _name;
        private bool _nameSpecified;
        
        private string _user;
        private bool _userSpecified;

        [XmlAttribute("name")]
        public string Name
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
        public string User
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
            string r = "";
            if (Name != null && User == null)
                r = Name;
            else if (Name == null && User != null)
                r = User;
            return r;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Value other)
                return false;

            return
                Name == other.Name &&
                NameSpecified == other.NameSpecified &&
                User == other.User &&
                UserSpecified == other.UserSpecified;
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
