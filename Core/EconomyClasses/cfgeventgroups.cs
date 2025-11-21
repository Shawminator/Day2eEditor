using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2eEditor
{
    public class cfgeventgroupsConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;
        public eventgroupdef Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }

        public cfgeventgroupsConfig(string path)
        {
            _path = path;
        }
        public void Load()
        {
            Data = AppServices.GetRequired<FileService>().LoadOrCreateXml<eventgroupdef>(
               _path,
               createNew: () => new eventgroupdef(),
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
               configName: "cfgeventgroups"
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
        public eventgroupdefGroup getassociatedgroup(string name)
        {
            foreach (eventgroupdefGroup eventgroupdefGroup in Data.group)
            {
                if (eventgroupdefGroup.name == name)
                    return eventgroupdefGroup;
            }
            return null;
        }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class eventgroupdef
    {
        private BindingList<eventgroupdefGroup> groupField;

        [System.Xml.Serialization.XmlElementAttribute("group")]
        public BindingList<eventgroupdefGroup> group
        {
            get
            {
                return this.groupField;
            }
            set
            {
                this.groupField = value;
            }
        }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class eventgroupdefGroup
    {

        private BindingList<eventgroupdefGroupChild> childField;

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("child")]
        public BindingList<eventgroupdefGroupChild> child
        {
            get
            {
                return this.childField;
            }
            set
            {
                this.childField = value;
            }
        }

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


        public override string ToString()
        {
            return name;
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class eventgroupdefGroupChild
    {

        private string typeField;

        private bool spawnsecondaryField;

        private bool spawnsecondaryFieldSpecified;

        private int delootField;

        private bool delootFieldSpecified;

        private int lootmaxField;

        private bool lootmaxFieldSpecified;

        private int lootminField;

        private bool lootminFieldSpecified;

        private decimal xField;

        private decimal zField;

        private decimal aField;

        private bool yFieldSpecified;

        private decimal yField;

        /// <remarks/>
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool spawnsecondary
        {
            get
            {
                return this.spawnsecondaryField;
            }
            set
            {
                this.spawnsecondaryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool spawnsecondarySpecified
        {
            get
            {
                return this.spawnsecondaryFieldSpecified;
            }
            set
            {
                this.spawnsecondaryFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int deloot
        {
            get
            {
                return this.delootField;
            }
            set
            {
                this.delootField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool delootSpecified
        {
            get
            {
                return this.delootFieldSpecified;
            }
            set
            {
                this.delootFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int lootmax
        {
            get
            {
                return this.lootmaxField;
            }
            set
            {
                this.lootmaxField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool lootmaxSpecified
        {
            get
            {
                return this.lootmaxFieldSpecified;
            }
            set
            {
                this.lootmaxFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int lootmin
        {
            get
            {
                return this.lootminField;
            }
            set
            {
                this.lootminField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool lootminSpecified
        {
            get
            {
                return this.lootminFieldSpecified;
            }
            set
            {
                this.lootminFieldSpecified = value;
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
        public decimal a
        {
            get
            {
                return this.aField;
            }
            set
            {
                this.aField = value;
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

        public override string ToString()
        {
            return type;
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is not eventgroupdefGroupChild other)
                return false;

            return this.type == other.type
                   && this.spawnsecondary == other.spawnsecondary
                   && this.spawnsecondarySpecified == other.spawnsecondarySpecified
                   && this.deloot == other.deloot
                   && this.delootSpecified == other.delootSpecified
                   && this.lootmax == other.lootmax
                   && this.lootmaxSpecified == other.lootmaxSpecified
                   && this.lootmin == other.lootmin
                   && this.lootminSpecified == other.lootminSpecified
                   && this.x == other.x
                   && this.y == other.y
                   && this.yFieldSpecified == other.yFieldSpecified
                   && this.z == other.z
                   && this.a == other.a;
        }
    }
}
