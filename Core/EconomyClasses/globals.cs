using System.ComponentModel;
using System.Xml.Serialization;

namespace Day2eEditor
{
    public class globalsConfig : IAdvancedConfigLoader
    {
        public string FileName => Path.GetFileName(_basepath); // e.g., "types.xml"
        public string FilePath => _basepath;
        public string _basepath { get; set; }
        public List<globalsFile> AllData { get; private set; } = new List<globalsFile>();
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();

        public void Load() => throw new InvalidOperationException("Use LoadWithParameters for this config.");
        public void LoadWithParameters(string basePath, string vanillaPath, List<string> modPaths)
        {
            _basepath = basePath;
            HasErrors = false;
            Errors.Clear();
            // Load vanilla file
            var vanilla = new globalsFile(vanillaPath)
            {
                IsModded = false,
                FileType = "globals"
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
                var modFile = new globalsFile(modPath)
                {
                    IsModded = true,
                    FileType = "globals",
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

                if (data.toDelete)
                {
                    AllData.Remove(data); // cleanup after deleting
                }
            }

            return savedFiles;
        }
        public bool needToSave()
        {
            foreach (var Data in AllData)
            {
                if (Data.needToSave())
                    return true;
            }
            return false;
        }
    }
    public class globalsFile : IConfigLoader
    {
        private readonly string _path;

        public variables Data { get; set; } = new variables();
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }
        public bool toDelete { get; set; }

        // Metadata for file type and source
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;                  // Full file path
        public string FileType { get; set; }               // "types"
        public bool IsModded { get; set; }                 // true if modded, false if vanilla
        public string ModFolder { get; set; }

        public globalsFile(string path)
        {
            _path = path ?? throw new ArgumentNullException(nameof(path));
        }
        public void Load()
        {
            Data = AppServices.GetRequired<FileService>().LoadOrCreateXml<variables>(
                _path,
                createNew: () => new variables(),
                onAfterLoad: cfg => { /* optional: do something after load */ },
                onError: ex =>
                {
                    HasErrors = true;
                    Console.WriteLine(
                        "Error in " + Path.GetFileName(_path) + "\n" +
                        ex.Message + "\n" +
                        ex.InnerException?.Message + "\n"
                        );
                    Errors.Add("Error in " + Path.GetFileName(_path) + "\n" +
                        ex.Message + "\n" +
                        ex.InnerException?.Message);
                },
                configName: "globals"
            );
        }
        public IEnumerable<string> Save()
        {
            if(toDelete)
            {
                if (File.Exists(_path))
                {
                    File.Delete(_path);
                }
                // Delete empty directories if needed
                Helper.DeleteEmptyFoldersUpToBase(Path.GetDirectoryName(_path), AppServices.GetRequired<EconomyManager>().basePath);
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
    }

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class variables
    {
        private BindingList<variablesVar> varField;

        [System.Xml.Serialization.XmlElementAttribute("var")]
        public BindingList<variablesVar> var
        {
            get
            {
                return this.varField;
            }
            set
            {
                this.varField = value;
            }
        }

        public variables() { }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class variablesVar
    {
        private string nameField;
        private int typeField;
        private string valueField;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        [XmlIgnore]
        public object TypedValue
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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is null || obj.GetType() != typeof(variablesVar)) return false;

            var other = (variablesVar)obj;

            return this.name == other.name &&
                   this.type == other.type &&
                   this.value == other.value;
        }

        public override int GetHashCode()
        {
            unchecked // allow overflow
            {
                int hash = 17;
                hash = hash * 23 + (name?.GetHashCode() ?? 0);
                hash = hash * 23 + type.GetHashCode();
                hash = hash * 23 + (value?.GetHashCode() ?? 0);
                return hash;
            }
        }
    }


}
