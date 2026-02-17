using System.ComponentModel;

namespace Day2eEditor
{
    public class cfgrandompresetsConfig : IAdvancedConfigLoader
    {
        public string FileName => Path.GetFileName(_basepath); // e.g., "types.xml"
        public string FilePath => _basepath;
        public string _basepath { get; set; }
        public List<cfgrandompresetsFile> AllData { get; private set; } = new List<cfgrandompresetsFile>();
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();

        public void Load() => throw new InvalidOperationException("Use LoadWithParameters for this config.");
        public void LoadWithParameters(string basePath, string vanillaPath, List<string> modPaths)
        {
            _basepath = basePath;
            HasErrors = false;
            Errors.Clear();
            // Load vanilla file
            var vanilla = new cfgrandompresetsFile(vanillaPath)
            {
                IsModded = false,
                FileType = "cfgspawnabletypes"
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
                var modFile = new cfgrandompresetsFile(modPath)
                {
                    IsModded = true,
                    FileType = "cfgspawnabletypes",
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
            foreach (var Data in AllData)
            {
                if (Data.needToSave())
                    return true;
            }
            return false;
        }
    }
    public class cfgrandompresetsFile : IConfigLoader
    {
        private readonly string _path;

        public randompresets Data { get; private set; } = new randompresets();
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }

        // Metadata for file type and source
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;                  // Full file path
        public string FileType { get; set; }               // "types"
        public bool IsModded { get; set; }                 // true if modded, false if vanilla
        public string ModFolder { get; set; }
        public bool ToDelete { get; set; }

        public cfgrandompresetsFile(string path)
        {
            _path = path ?? throw new ArgumentNullException(nameof(path));
        }
        public void Load()
        {
            Data = AppServices.GetRequired<FileService>().LoadOrCreateXml<randompresets>(
                _path,
                createNew: () => new randompresets(),
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
                configName: "cfgrandompresets"
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
                    Helper.DeleteEmptyFoldersUpToBase(Path.GetDirectoryName(_path), AppServices.GetRequired<EconomyManager>().basePath);
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

        public void CreateNew()
        {
            Data = new randompresets()
            {
                Items = new BindingList<object>()
            };
        }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class randompresets
    {
        private BindingList<object> itemsField;

        [System.Xml.Serialization.XmlElementAttribute("attachments", typeof(randompresetsAttachments))]
        [System.Xml.Serialization.XmlElementAttribute("cargo", typeof(randompresetsCargo))]
        public BindingList<object> Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class randompresetsAttachments
    {
        private BindingList<randompresetsItem> itemField;
        private decimal chanceField;
        private string nameField;

        [System.Xml.Serialization.XmlElementAttribute("item")]
        public BindingList<randompresetsItem> item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal chance
        {
            get
            {
                return this.chanceField;
            }
            set
            {
                this.chanceField = value;
            }
        }
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

        public override string ToString()
        {
            return name;
        }
        public override bool Equals(object obj)
        {
            if (obj is not randompresetsAttachments other)
                return false;

            return chance == other.chance
                && string.Equals(name, other.name, StringComparison.Ordinal)
                && ((item == null && other.item == null)
                    || (item != null && other.item != null && item.SequenceEqual(other.item)));
        }
    }
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class randompresetsCargo
    {
        private BindingList<randompresetsItem> itemField;
        private decimal chanceField;
        private string nameField;

        [System.Xml.Serialization.XmlElementAttribute("item")]
        public BindingList<randompresetsItem> item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal chance
        {
            get
            {
                return this.chanceField;
            }
            set
            {
                this.chanceField = value;
            }
        }
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

        public override string ToString()
        {
            return name;
        }
        public override bool Equals(object obj)
        {
            if (obj is not randompresetsCargo other)
                return false;

            return chance == other.chance
                && string.Equals(name, other.name, StringComparison.Ordinal)
                && ((item == null && other.item == null)
                    || (item != null && other.item != null && item.SequenceEqual(other.item)));
        }

    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class randompresetsItem
    {
        private string nameField;
        private decimal chanceField;

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
        public decimal chance
        {
            get
            {
                return this.chanceField;
            }
            set
            {
                this.chanceField = value;
            }
        }
        public override string ToString()
        {
            return name;
        }
        public override bool Equals(object obj)
        {
            if (obj is randompresetsItem other)
            {
                return this.name == other.name &&
                       this.chance == other.chance;
            }
            return false;
        }
    }
}
