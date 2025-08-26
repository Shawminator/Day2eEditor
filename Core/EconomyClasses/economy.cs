using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Day2eEditor
{
    public class economyConfig : IAdvancedConfigLoader
    {
        public string _basepath { get; set; }
        public List<economyFile> AllData { get; private set; } = new List<economyFile>();
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();

        public void Load() => throw new InvalidOperationException("Use LoadWithParameters for this config.");
        public void LoadWithParameters(string basePath, string vanillaPath, List<string> modPaths)
        {
            _basepath = basePath;
            HasErrors = false;
            Errors.Clear();
            // Load vanilla file
            var vanilla = new economyFile(vanillaPath)
            {
                IsModded = false,
                FileType = "economy"
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
                var modFile = new economyFile(modPath)
                {
                    IsModded = true,
                    FileType = "economy",
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
    public class economyFile : IConfigLoader
    {
        private readonly string _path;

        public economy Data { get; set; } = new economy();
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

        public economyFile(string path)
        {
            _path = path ?? throw new ArgumentNullException(nameof(path));
        }
        public void Load()
        {
            Data = AppServices.GetRequired<FileService>().LoadOrCreateXml<economy>(
                _path,
                createNew: () => new economy(),
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
                configName: "economy"
            );
        }
        public IEnumerable<string> Save()
        {
            if (toDelete)
            {
                if (File.Exists(_path))
                {
                    File.Delete(_path);
                }

                // Delete empty directories if needed
                ShellHelper.DeleteEmptyFoldersUpToBase(Path.GetDirectoryName(_path), AppServices.GetRequired<EconomyManager>().basePath);
                
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
    public partial class economy
    {
        private EconomySection dynamicField;
        private EconomySection animalsField;
        private EconomySection zombiesField;
        private EconomySection vehiclesField;
        private EconomySection randomsField;
        private EconomySection customField;
        private EconomySection buildingField;
        private EconomySection playerField;

        public EconomySection dynamic
        {
            get
            {
                return this.dynamicField;
            }
            set
            {
                this.dynamicField = value;
            }
        }
        public EconomySection animals
        {
            get
            {
                return this.animalsField;
            }
            set
            {
                this.animalsField = value;
            }
        }
        public EconomySection zombies
        {
            get
            {
                return this.zombiesField;
            }
            set
            {
                this.zombiesField = value;
            }
        }
        public EconomySection vehicles
        {
            get
            {
                return this.vehiclesField;
            }
            set
            {
                this.vehiclesField = value;
            }
        }
        public EconomySection randoms
        {
            get
            {
                return this.randomsField;
            }
            set
            {
                this.randomsField = value;
            }
        }
        public EconomySection custom
        {
            get
            {
                return this.customField;
            }
            set
            {
                this.customField = value;
            }
        }
        public EconomySection building
        {
            get
            {
                return this.buildingField;
            }
            set
            {
                this.buildingField = value;
            }
        }
        public EconomySection player
        {
            get
            {
                return this.playerField;
            }
            set
            {
                this.playerField = value;
            }
        }

        public economy()
        {

        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class EconomySection
    {
        private int initField;
        private int loadField;
        private int respawnField;
        private int saveField;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int init
        {
            get
            {
                return this.initField;
            }
            set
            {
                this.initField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int load
        {
            get
            {
                return this.loadField;
            }
            set
            {
                this.loadField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int respawn
        {
            get
            {
                return this.respawnField;
            }
            set
            {
                this.respawnField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int save
        {
            get
            {
                return this.saveField;
            }
            set
            {
                this.saveField = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is EconomySection other)
            {
                return init == other.init &&
                       load == other.load &&
                       respawn == other.respawn &&
                       save == other.save;
            }
            return false;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + init.GetHashCode();
                hash = hash * 23 + load.GetHashCode();
                hash = hash * 23 + respawn.GetHashCode();
                hash = hash * 23 + save.GetHashCode();
                return hash;
            }
        }
    }
}
