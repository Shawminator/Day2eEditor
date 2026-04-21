using System.ComponentModel;

namespace Day2eEditor
{
    public class economyCoreConfig : SingleFileConfigLoaderBase<economycore>
    {
        public economyCoreConfig(string path) : base(path)
        {
        }

        protected override economycore CreateDefaultData()
        {
            return new economycore();
        }

        public override void Load()
        {
            HasErrors = false;
            _errors.Clear();

            try
            {
                Data = AppServices.GetRequired<FileService>()
                    .LoadOrCreateXml<economycore>(
                        _path,
                        createNew: () => new economycore(),
                        onError: ex =>
                        {
                            HandleLoadError(ex);
                        },
                        configName: "cfgeconomycore"
                    );

                var issues = ValidateData();
                if (issues?.Any() == true)
                {
                    Console.WriteLine("Validation issues in " + FileName + ":");
                    foreach (var msg in issues)
                        Console.WriteLine("- " + msg);

                    MarkDirty();
                }

                OnAfterLoad(Data);
                ClonedData = CloneData(Data);
            }
            catch (Exception ex)
            {
                HandleLoadError(ex);
            }
        }

        public override IEnumerable<string> Save()
        {
            if (Data is null)
                return Array.Empty<string>();

            if (!AreEqual(Data, ClonedData) || IsDirty == true)
            {
                ClearDirty();
                AppServices.GetRequired<FileService>().SaveXml(_path, Data);
                ClonedData = CloneData(Data);
                return new[] { _path };
            }

            return Array.Empty<string>();
        }

        public void AddCe(string path, string filename, string type)
        {
            economycoreCEFile newfile = new economycoreCEFile();
            string _path = path.Replace("\\", "/").Replace("//", "/");
            switch (type)
            {
                case "types":
                    newfile.name = filename;
                    newfile.type = "types";
                    break;
                case "events":
                    newfile.name = filename;
                    newfile.type = "events";
                    break;
                case "spawnabletypes":
                    newfile.name = filename;
                    newfile.type = "spawnabletypes";
                    break;
                case "randompresets":
                    newfile.name = filename;
                    newfile.type = "randompresets";
                    break;
                case "globals":
                    newfile.name = filename;
                    newfile.type = "globals";
                    break;
                case "economy":
                    newfile.name = filename;
                    newfile.type = "economy";
                    break;
                case "messages":
                    newfile.name = filename;
                    newfile.type = "messages";
                    break;
                default:
                    break;
            }

            if (Data.ce.Any(x => x.folder == _path))
            {
                Data.ce.FirstOrDefault(x => x.folder == _path)?.file.Add(newfile);
            }
            else
            {
                economycoreCE newce = new economycoreCE
                {
                    folder = _path,
                    file = new BindingList<economycoreCEFile>()
                };
                newce.file.Add(newfile);

                if (Data.ce == null)
                {
                    Data.ce = new BindingList<economycoreCE>();
                }

                Data.ce.Add(newce);
            }
        }

        public economycoreCE? getfolder(string path)
        {
            return Data.ce.FirstOrDefault(x => x.folder == path);
        }

        public void RemoveCe(string modname, out string Folderpath, out string filename, out bool deletedirectory)
        {
            economycoreCE ce = Data.findFile(modname)!;
            Folderpath = ce.folder;
            economycoreCEFile file = ce.getfile(modname)!;
            filename = file.name;
            ce.removefile(file);
            deletedirectory = false;

            if (ce.file.Count == 0)
            {
                Data.ce.Remove(ce);
                deletedirectory = true;
            }
        }

        public List<string> GetModdedPaths(string type)
        {
            var result = new List<string>();

            var project = AppServices.GetRequired<ProjectManager>()?.CurrentProject;
            if (project == null)
                return result;

            var basePath = Path.Combine(project.ProjectRoot, "mpmissions", project.MpMissionPath);

            foreach (economycoreCE folder in Data.ce)
            {
                string modFolderPath = Path.Combine(basePath, folder.folder.Replace("/", "\\"));
                foreach (economycoreCEFile file in folder.file)
                {
                    if (file.type?.Equals(type, StringComparison.OrdinalIgnoreCase) == true)
                    {
                        string modFilePath = Path.Combine(modFolderPath, file.name);
                        result.Add(modFilePath);
                    }
                }
            }

            return result;
        }

        protected override IEnumerable<string> ValidateData()
        {
            return CheckCE();
        }

        public List<string> CheckCE()
        {
            var fixes = new List<string>();
            bool needToSave = false;
            List<economycoreCE> ceToRemove = new List<economycoreCE>();

            foreach (var ce in Data.ce)
            {
                string fullFolderPath = Path.Combine(
                    AppServices.GetRequired<EconomyManager>().basePath,
                    ce.folder
                );

                List<economycoreCEFile> filesToRemove = new List<economycoreCEFile>();
                foreach (var ceFile in ce.file)
                {
                    string filePath = Path.Combine(fullFolderPath, ceFile.name);

                    if (!File.Exists(filePath))
                    {
                        filesToRemove.Add(ceFile);
                        Console.WriteLine($"\tNon-existing file found, removing {ceFile.name} from {ce.folder}");
                        fixes.Add($"\tNon-existing file found, removing {ceFile.name} from {ce.folder}");
                        needToSave = true;
                    }
                }

                foreach (economycoreCEFile f in filesToRemove)
                {
                    ce.removefile(f);
                }

                if (ce.file == null || ce.file.Count == 0)
                {
                    Console.WriteLine($"\tEmpty CE entry found in economy core, removing {ce.folder}");
                    fixes.Add($"\tEmpty CE entry found in economy core, removing {ce.folder}");
                    ceToRemove.Add(ce);
                    needToSave = true;
                }
            }

            foreach (economycoreCE ce in ceToRemove)
            {
                Data.ce.Remove(ce);
            }

            if (needToSave)
            {
                MarkDirty();
                Save();
            }

            return fixes;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
    public partial class economycore : IEquatable<economycore>, IDeepCloneable<economycore>
    {
        private BindingList<economycoreRootclass>? classesField;
        private BindingList<economycoreDefault>? defaultsField;
        private BindingList<economycoreCE>? ceField;

        [System.Xml.Serialization.XmlArrayItem("rootclass", IsNullable = false)]
        public BindingList<economycoreRootclass> classes
        {
            get => classesField ??= new BindingList<economycoreRootclass>();
            set => classesField = value;
        }

        [System.Xml.Serialization.XmlArrayItem("default", IsNullable = false)]
        public BindingList<economycoreDefault> defaults
        {
            get => defaultsField ??= new BindingList<economycoreDefault>();
            set => defaultsField = value;
        }

        [System.Xml.Serialization.XmlElement("ce")]
        public BindingList<economycoreCE> ce
        {
            get => ceField ??= new BindingList<economycoreCE>();
            set => ceField = value;
        }

        public economycoreCE? findFile(string modname)
        {
            foreach (economycoreCE ceEntry in ce)
            {
                if (ceEntry.file.Any(x => x.name == modname))
                {
                    return ceEntry;
                }
            }
            return null;
        }

        public bool Equals(economycore? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return classes.SequenceEqual(other.classes) &&
                   defaults.SequenceEqual(other.defaults) &&
                   ce.SequenceEqual(other.ce);
        }

        public override bool Equals(object? obj) => Equals(obj as economycore);

        public economycore Clone()
        {
            return new economycore
            {
                classes = new BindingList<economycoreRootclass>(classes.Select(x => x.Clone()).ToList()),
                defaults = new BindingList<economycoreDefault>(defaults.Select(x => x.Clone()).ToList()),
                ce = new BindingList<economycoreCE>(ce.Select(x => x.Clone()).ToList())
            };
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class economycoreRootclass : IEquatable<economycoreRootclass>, IDeepCloneable<economycoreRootclass>
    {
        private string? nameField;
        private string? reportMemoryLODField;
        private string? actField;

        [System.Xml.Serialization.XmlAttribute]
        public string? name
        {
            get => nameField;
            set => nameField = value;
        }

        [System.Xml.Serialization.XmlAttribute]
        public string? reportMemoryLOD
        {
            get => reportMemoryLODField;
            set => reportMemoryLODField = value;
        }

        [System.Xml.Serialization.XmlAttribute]
        public string? act
        {
            get => actField;
            set => actField = value;
        }

        public override string ToString()
        {
            return name ?? string.Empty;
        }

        public bool Equals(economycoreRootclass? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(name, other.name, StringComparison.Ordinal) &&
                   string.Equals(reportMemoryLOD, other.reportMemoryLOD, StringComparison.Ordinal) &&
                   string.Equals(act, other.act, StringComparison.Ordinal);
        }

        public override bool Equals(object? obj) => Equals(obj as economycoreRootclass);

        public economycoreRootclass Clone()
        {
            return new economycoreRootclass
            {
                name = name,
                reportMemoryLOD = reportMemoryLOD,
                act = act
            };
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class economycoreDefault : IEquatable<economycoreDefault>, IDeepCloneable<economycoreDefault>
    {
        private string? nameField;
        private string? valueField;

        [System.Xml.Serialization.XmlAttribute]
        public string? name
        {
            get => nameField;
            set => nameField = value;
        }

        [System.Xml.Serialization.XmlAttribute]
        public string? value
        {
            get => valueField;
            set => valueField = value;
        }

        public override string ToString()
        {
            return name ?? string.Empty;
        }

        public bool Equals(economycoreDefault? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(name, other.name, StringComparison.Ordinal) &&
                   string.Equals(value, other.value, StringComparison.Ordinal);
        }

        public override bool Equals(object? obj) => Equals(obj as economycoreDefault);

        public economycoreDefault Clone()
        {
            return new economycoreDefault
            {
                name = name,
                value = value
            };
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class economycoreCE : IEquatable<economycoreCE>, IDeepCloneable<economycoreCE>
    {
        private BindingList<economycoreCEFile>? fileField;
        private string? folderField;

        [System.Xml.Serialization.XmlElement("file")]
        public BindingList<economycoreCEFile> file
        {
            get => fileField ??= new BindingList<economycoreCEFile>();
            set => fileField = value;
        }

        [System.Xml.Serialization.XmlAttribute]
        public string folder
        {
            get => folderField ?? string.Empty;
            set => folderField = value;
        }

        public override string ToString()
        {
            return folder;
        }

        public economycoreCEFile? getfile(string modname)
        {
            foreach (economycoreCEFile _file in file)
            {
                if (_file.name == modname)
                {
                    return _file;
                }
            }
            return null;
        }

        public void removefile(economycoreCEFile _file)
        {
            file.Remove(_file);
        }

        public bool Equals(economycoreCE? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(folder, other.folder, StringComparison.Ordinal) &&
                   file.SequenceEqual(other.file);
        }

        public override bool Equals(object? obj) => Equals(obj as economycoreCE);

        public economycoreCE Clone()
        {
            return new economycoreCE
            {
                folder = folder,
                file = new BindingList<economycoreCEFile>(file.Select(x => x.Clone()).ToList())
            };
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class economycoreCEFile : IEquatable<economycoreCEFile>, IDeepCloneable<economycoreCEFile>
    {
        private string? nameField;
        private string? typeField;

        [System.Xml.Serialization.XmlAttribute]
        public string name
        {
            get => nameField ?? string.Empty;
            set => nameField = value;
        }

        [System.Xml.Serialization.XmlAttribute]
        public string type
        {
            get => typeField ?? string.Empty;
            set => typeField = value;
        }

        public override string ToString()
        {
            return name;
        }

        public bool Equals(economycoreCEFile? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(name, other.name, StringComparison.Ordinal) &&
                   string.Equals(type, other.type, StringComparison.Ordinal);
        }

        public override bool Equals(object? obj) => Equals(obj as economycoreCEFile);

        public economycoreCEFile Clone()
        {
            return new economycoreCEFile
            {
                name = name,
                type = type
            };
        }
    }
}