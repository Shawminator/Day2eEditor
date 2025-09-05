using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2eEditor
{
    public class cfgeventspawnsConfig : IConfigLoader
    {
        private readonly string _path;
        public eventposdef Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }

        public cfgeventspawnsConfig(string path)
        {
            _path = path;
        }
        public void Load()
        {
            Data = AppServices.GetRequired<FileService>().LoadOrCreateXml<eventposdef>(
               _path,
               createNew: () => new eventposdef(),
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
               configName: "cfgeventspawns"
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
        public eventposdefEvent Findevent(string eventpname)
        {
            foreach (eventposdefEvent _eventspawn in Data.@event)
            {
                if (_eventspawn.name == eventpname)
                    return _eventspawn;
            }
            return null;
        }
        public eventposdefEventPos findeventgroup(string eventgroupname)
        {
            foreach (eventposdefEvent eventposdefEvent in Data.@event)
            {
                foreach (eventposdefEventPos eventposdefEventPos in eventposdefEvent.pos)
                {
                    if (eventposdefEventPos.group != null && eventposdefEventPos.group == eventgroupname)
                    {
                        return eventposdefEventPos;
                    }
                }
            }
            return null;
        }
        public void AddNewEventSpawn(eventposdefEvent newvenspawn)
        {
            Data.@event.Add(newvenspawn);
            isDirty = true;
        }
    }
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class eventposdef
    {
        private BindingList<eventposdefEvent> eventField;

        [System.Xml.Serialization.XmlElementAttribute("event")]
        public BindingList<eventposdefEvent> @event
        {
            get
            {
                return this.eventField;
            }
            set
            {
                this.eventField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class eventposdefEvent
    {

        private eventposdefEventZone zoneField;

        private BindingList<eventposdefEventPos> posField;

        private string nameField;

        /// <remarks/>
        public eventposdefEventZone zone
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
        [System.Xml.Serialization.XmlElementAttribute("pos")]
        public BindingList<eventposdefEventPos> pos
        {
            get
            {
                return this.posField;
            }
            set
            {
                this.posField = value;
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
    public partial class eventposdefEventZone
    {

        private int sminField;

        private int smaxField;

        private int dminField;

        private int dmaxField;

        private int rField;


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
        public int r
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

        public override bool Equals(object? obj)
        {
            if (obj is not eventposdefEventZone other)
                return false;

            return smin == other.smin && smax == other.smax && dmin == other.dmin && dmax == other.dmax && r == other.r;
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class eventposdefEventPos
    {

        private decimal xField;

        private decimal yField;

        private bool yFieldSpecified;

        private decimal zField;

        private decimal aField;

        private bool aFieldSpecified;

        private string groupField;

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
        public bool aSpecified
        {
            get
            {
                return this.aFieldSpecified;
            }
            set
            {
                this.aFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string group
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

        public override string ToString()
        {
            return x.ToString() + "," + z.ToString();
        }
        public string ToExpansionMapString(float y, float a)
        {
            return x.ToString() + " " + y + " " + z.ToString() + "|" + a.ToString() + " 0.0 0.0";
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is not eventposdefEventPos other)
                return false;

            return this.x == other.x
                   && this.y == other.y
                   && this.yFieldSpecified == other.yFieldSpecified
                   && this.z == other.z
                   && this.a == other.a
                   && this.aFieldSpecified == other.aFieldSpecified
                   && this.group == other.group;
        }
    }
}
