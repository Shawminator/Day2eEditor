using System.ComponentModel;

namespace Day2eEditor
{
    public class economyCoreConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;
        public economycore Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }

        public economyCoreConfig(string path)
        {
            _path = path;
        }

        public void Load()
        {
            Data = AppServices.GetRequired<FileService>().LoadOrCreateXml<economycore>(
                _path,
                createNew: () => new economycore(),
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
                configName: "cfgeconomycore"
            );
        }
        public IEnumerable<string> Save()
        {
            if (isDirty)
            {
                AppServices.GetRequired<FileService>().SaveXml(_path, Data);
                isDirty = false;
                return new[] { Path.GetFileName(_path) };
            }

            return Array.Empty<string>();
        }

        public bool needToSave()
        {
            return isDirty;
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
                Data.ce.FirstOrDefault(x => x.folder == _path).file.Add(newfile);
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
            isDirty = true;
        }
        public economycoreCE getfolder(string path)
        {
            return Data.ce.FirstOrDefault(x => x.folder == path);
        }
        public void RemoveCe(string modname, out string Folderpath, out string filename, out bool deletedirectory)
        {
            economycoreCE ce = Data.findFile(modname);
            Folderpath = ce.folder;
            economycoreCEFile file = ce.getfile(modname);
            filename = file.name;
            ce.removefile(file);
            deletedirectory = false;
            if (ce.file.Count == 0)
            {
                Data.ce.Remove(ce);
                deletedirectory = true;
            }
            isDirty = true;
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
    }
    
    
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class economycore
    {

        private BindingList<economycoreRootclass> classesField;

        private BindingList<economycoreDefault> defaultsField;

        private BindingList<economycoreCE> ceField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("rootclass", IsNullable = false)]
        public BindingList<economycoreRootclass> classes
        {
            get
            {
                return this.classesField;
            }
            set
            {
                this.classesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("default", IsNullable = false)]
        public BindingList<economycoreDefault> defaults
        {
            get
            {
                return this.defaultsField;
            }
            set
            {
                this.defaultsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ce")]
        public BindingList<economycoreCE> ce
        {
            get
            {
                return this.ceField;
            }
            set
            {
                this.ceField = value;
            }
        }
        public economycoreCE findFile(string modname)
        {
            foreach (economycoreCE ce in ce)
            {
                if (ce.file.Any(x => x.name == modname))
                {
                    return ce;
                }
            }
            return null;
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class economycoreRootclass
    {

        private string nameField;

        private string reportMemoryLODField;

        private string actField;

        /// <remarks/>
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string reportMemoryLOD
        {
            get
            {
                return this.reportMemoryLODField;
            }
            set
            {
                this.reportMemoryLODField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string act
        {
            get
            {
                return this.actField;
            }
            set
            {
                this.actField = value;
            }
        }

        public override string ToString()
        {
            return name;
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class economycoreDefault
    {

        private string nameField;

        private string valueField;

        /// <remarks/>
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

        /// <remarks/>
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

        public override string ToString()
        {
            return name;
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class economycoreCE
    {

        private BindingList<economycoreCEFile> fileField;

        private string folderField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("file")]
        public BindingList<economycoreCEFile> file
        {
            get
            {
                return this.fileField;
            }
            set
            {
                this.fileField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string folder
        {
            get
            {
                return this.folderField;
            }
            set
            {
                this.folderField = value;
            }
        }

        public override string ToString()
        {
            return folder;
        }
        public economycoreCEFile getfile(string modname)
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
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class economycoreCEFile
    {
        private string nameField;
        private string typeField;  //available types = types, spawnabletypes, globals, economy, events, messages, randompresets

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
        public string type
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

        public override string ToString()
        {
            return name;
        }
    }




}
