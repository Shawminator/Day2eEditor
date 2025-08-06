namespace Day2eEditor
{
    public class economyConfig : IAdvancedConfigLoader
    {
        public List<economyFile> AllData { get; private set; } = new List<economyFile>();
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();

        public void Load() => throw new InvalidOperationException("Use LoadWithParameters for this config.");
        public void LoadWithParameters(string vanillaPath, List<string> modPaths)
        {
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
                    ModFolder = Path.GetDirectoryName(modPath)
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
        public void Save()
        {
            foreach (var Data in AllData)
            {
                // Save only if the file is dirty
                if (Data.isDirty)
                {
                    Data.Save();
                }

            }
        }
    }
    public class economyFile : IConfigLoader
    {
        private readonly string _path;

        public economy Data { get; private set; } = new economy();
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }

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
        public void Save()
        {
            if (isDirty)
            {
                AppServices.GetRequired<FileService>().SaveXml(_path, Data);
                isDirty = false;
            }
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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class EconomySection
    {
        private byte initField;
        private byte loadField;
        private byte respawnField;
        private byte saveField;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte init
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
        public byte load
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
        public byte respawn
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
        public byte save
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
    }
}
