using System.ComponentModel;
using System.Xml.Serialization;

namespace Day2eEditor
{
    public class cfglimitsdefinitionConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;
        public cfglimitsdefinition Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }

        public cfglimitsdefinitionConfig(string path)
        {
            _path = path;
        }
        public void Load()
        {
            Data = AppServices.GetRequired<FileService>().LoadOrCreateXml<cfglimitsdefinition>(
               _path,
               createNew: () => new cfglimitsdefinition(),
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
               configName: "cfglimitsdefinition"
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
    }


    [XmlRoot("lists")]
    public partial class cfglimitsdefinition
    {
        private BindingList<listsCategory> categoriesField;
        private BindingList<listsTag> tagsField;
        private BindingList<listsUsage> usageflagsField;
        private BindingList<listsValue> valueflagsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("category", IsNullable = false)]
        public BindingList<listsCategory> categories
        {
            get
            {
                return this.categoriesField;
            }
            set
            {
                this.categoriesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("tag", IsNullable = false)]
        public BindingList<listsTag> tags
        {
            get
            {
                return this.tagsField;
            }
            set
            {
                this.tagsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("usage", IsNullable = false)]
        public BindingList<listsUsage> usageflags
        {
            get
            {
                return this.usageflagsField;
            }
            set
            {
                this.usageflagsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("value", IsNullable = false)]
        public BindingList<listsValue> valueflags
        {
            get
            {
                return this.valueflagsField;
            }
            set
            {
                this.valueflagsField = value;
            }
        }

        public bool Equals(cfglimitsdefinition other)
        {
            if (other is null) return false;

            return Enumerable.SequenceEqual(categories ?? new BindingList<listsCategory>(), other.categories ?? new BindingList<listsCategory>()) &&
                   Enumerable.SequenceEqual(tags ?? new BindingList<listsTag>(), other.tags ?? new BindingList<listsTag>()) &&
                   Enumerable.SequenceEqual(usageflags ?? new BindingList<listsUsage>(), other.usageflags ?? new BindingList<listsUsage>()) &&
                   Enumerable.SequenceEqual(valueflags ?? new BindingList<listsValue>(), other.valueflags ?? new BindingList<listsValue>());
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class listsCategory
    {
        private string nameField;

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
        public override bool Equals(object obj) =>
            obj is listsCategory other && string.Equals(name, other.name, StringComparison.Ordinal);
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class listsTag
    {
        private string nameField;

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
        public override bool Equals(object obj) =>
            obj is listsTag other && string.Equals(name, other.name, StringComparison.Ordinal);
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class listsUsage
    {
        private string nameField;

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
        public override bool Equals(object obj) =>
            obj is listsUsage other && string.Equals(name, other.name, StringComparison.Ordinal);
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class listsValue
    {
        private string nameField;

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
        public override bool Equals(object obj) =>
            obj is listsValue other && string.Equals(name, other.name, StringComparison.Ordinal);
    }



}
