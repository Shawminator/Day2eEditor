using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Day2eEditor
{
    public class TerritoriesConfig: IConfigLoader
    {
        public string _basepath { get; set; }
        public BindingList<territorytype> AllData { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();

        public TerritoriesConfig(string basepath)
        {
            _basepath = basepath;
        }
        public void Load()
        {
            HasErrors = false;
            Errors.Clear();
            AllData = new BindingList<territorytype>();
            foreach (envTerritoriesFile envtf in AppServices.GetRequired<EconomyManager>().cfgenvironmentConfig.Data.territories.file)
            {
                string fullPath = Path.Combine(_basepath, envtf.path);
                var tf = AppServices.GetRequired<FileService>().LoadOrCreateXml<territorytype>(
                    fullPath,
                    createNew: () => new territorytype(),
                    onAfterLoad: cfg => { /* optional: do something after load */ },
                    onError: ex =>
                    {
                        HasErrors = true;
                        Console.WriteLine(
                            "Error in " + Path.GetFileName(fullPath) + "\n" +
                            ex.Message + "\n" +
                            ex.InnerException?.Message + "\n"
                        );
                        Errors.Add("Error in " + Path.GetFileName(fullPath) + "\n" +
                            ex.Message + "\n" +
                            ex.InnerException?.Message);
                    },
                    configName: "territorytype"
                );
                tf.setpath(fullPath);
                AllData.Add(tf);
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
            return false;
        }
    }

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute("territory-type", Namespace = "", IsNullable = false)]
    public partial class territorytype
    {
        [XmlIgnore]
        private string _path;
        [XmlIgnore]
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        [XmlIgnore]
        public string FilePath => _path;
        [XmlIgnore]
        public bool isDirty { get; set; }
        [XmlIgnore]
        public bool ToDelete { get; set; }


        public territorytype()
        {
            territory = new BindingList<territorytypeTerritory>();
        }

        private BindingList<territorytypeTerritory> territoryField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("territory")]
        public BindingList<territorytypeTerritory> territory
        {
            get
            {
                return this.territoryField;
            }
            set
            {
                this.territoryField = value;
            }
        }

        public void setpath(string path)
        {
            _path = path;
        }
        internal IEnumerable<string> Save()
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
                AppServices.GetRequired<FileService>().SaveXml(_path, this);
                isDirty = false;
                return new[] { FileName };
            }

            return Array.Empty<string>();
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class territorytypeTerritory
    {

        private BindingList<territorytypeTerritoryZone> zoneField;

        private long colorField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("zone")]
        public BindingList<territorytypeTerritoryZone> zone
        {
            get
            {
                return this.zoneField;
            }
            set
            {
                this.zoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public long color
        {
            get
            {
                return this.colorField;
            }
            set
            {
                this.colorField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class territorytypeTerritoryZone
    {

        private string nameField;

        private int sminField;

        private int smaxField;

        private int dminField;

        private int dmaxField;

        private decimal xField;

        private decimal yField;

        private bool yFieldSpecified;

        private decimal zField;

        private decimal rField;

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
        public int smin
        {
            get
            {
                return this.sminField;
            }
            set
            {
                this.sminField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int smax
        {
            get
            {
                return this.smaxField;
            }
            set
            {
                this.smaxField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int dmin
        {
            get
            {
                return this.dminField;
            }
            set
            {
                this.dminField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int dmax
        {
            get
            {
                return this.dmaxField;
            }
            set
            {
                this.dmaxField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal x
        {
            get
            {
                return this.xField;
            }
            set
            {
                this.xField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal y
        {
            get
            {
                return this.yField;
            }
            set
            {
                this.yField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ySpecified
        {
            get
            {
                return this.yFieldSpecified;
            }
            set
            {
                this.yFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal z
        {
            get
            {
                return this.zField;
            }
            set
            {
                this.zField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal r
        {
            get
            {
                return this.rField;
            }
            set
            {
                this.rField = value;
            }
        }
        public override string ToString()
        {
            return $"Name:{name}, X:{x.ToString()}, Z:{z.ToString()}";
        }
        public bool Equals(territorytypeTerritoryZone p)
        {
            if ((object)p == null)
                return false;

            return (name == p.name) && (smin == p.smin) && (smax == p.smax) && (dmin == p.dmin) && (dmax == p.dmax) && (x == p.x) && (z == p.z) && (r == p.r);
        }
    }
}
