using System.ComponentModel;
using System.Reflection;
using System.Xml.Serialization;

namespace Day2eEditor
{
    public class TypesConfig : IAdvancedConfigLoader
    {
        public List<TypesFile> AllTypes { get; private set; } = new List<TypesFile>();
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();

        public void Load() => throw new InvalidOperationException("Use LoadWithParameters for this config.");
        public void LoadWithParameters(string vanillaPath, List<string> modPaths)
        {
            HasErrors = false;
            Errors.Clear();
            // Load vanilla file
            var vanilla = new TypesFile(vanillaPath)
            {
                IsModded = false,
                FileType = "types"
            };

            vanilla.Load();
            AllTypes.Add(vanilla);

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
                    ModFolder = Path.GetDirectoryName(modPath)
                };

                modFile.Load();
                AllTypes.Add(modFile);

                if (modFile.HasErrors)
                {
                    HasErrors = true;
                    var modName = Path.GetFileName(modFile.ModFolder);
                    var fileName = Path.GetFileName(modFile.FilePath);
                    Errors.AddRange(modFile.Errors.Select(e => $"[{modName}] [{fileName}] {e}"));
                }
            }
        }
    }
    public class TypesFile : IConfigLoader
    {
        private readonly string _path;

        public Types Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }

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
        public int CountInCargo
        {
            get => _countInCargo;
            set => _countInCargo = value;
        }

        [XmlAttribute("count_in_hoarder")]
        public int CountInHoarder
        {
            get => _countInHoarder;
            set => _countInHoarder = value;
        }

        [XmlAttribute("count_in_map")]
        public int CountInMap
        {
            get => _countInMap;
            set => _countInMap = value;
        }

        [XmlAttribute("count_in_player")]
        public int CountInPlayer
        {
            get => _countInPlayer;
            set => _countInPlayer = value;
        }

        [XmlAttribute("crafted")]
        public int Crafted
        {
            get => _crafted;
            set => _crafted = value;
        }

        [XmlAttribute("deloot")]
        public int Deloot
        {
            get => _deloot;
            set => _deloot = value;
        }
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
            return Name;
        }
    }
}
