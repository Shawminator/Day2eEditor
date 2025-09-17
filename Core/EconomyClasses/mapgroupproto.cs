using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2eEditor
{
    public class mapgroupprotoConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;
        public prototype Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }

        public mapgroupprotoConfig(string path)
        {
            _path = path;
        }

        public void Load()
        {
            Data = AppServices.GetRequired<FileService>().LoadOrCreateXml<prototype>(
                _path,
                createNew: () => new prototype(),
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
                configName: "prototype"
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

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class prototype
    {

        private BindingList<prototypeDefault> defaultsField;

        private BindingList<prototypeGroup> groupField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("default", IsNullable = false)]
        public BindingList<prototypeDefault> defaults
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
        [System.Xml.Serialization.XmlElementAttribute("group")]
        public BindingList<prototypeGroup> group
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

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class prototypeDefault
    {

        private string nameField;
        private int lootmaxField;
        private bool lootmaxFieldSpecified;
        private string enabledField;
        private string deField;
        private int widthField;
        private bool widthFieldSpecified;
        private int heightField;
        private bool heightFieldSpecified;

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
        public string enabled
        {
            get
            {
                return this.enabledField;
            }
            set
            {
                this.enabledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string de
        {
            get
            {
                return this.deField;
            }
            set
            {
                this.deField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int width
        {
            get
            {
                return this.widthField;
            }
            set
            {
                this.widthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool widthSpecified
        {
            get
            {
                return this.widthFieldSpecified;
            }
            set
            {
                this.widthFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool heightSpecified
        {
            get
            {
                return this.heightFieldSpecified;
            }
            set
            {
                this.heightFieldSpecified = value;
            }
        }
        public override string ToString()
        {
            string toname = "";
            toname += $"name : {name}";
            if (de != null)
                toname += $", de : {de}";
            if(lootmaxSpecified)
                toname += $", lootmax : {lootmax}";
            if (enabled != null)
                toname += $", enabled : {enabled}";
            if (widthSpecified)
                toname += $", width : {width}";
            if (heightSpecified)
                toname += $", height : {height}";

            return toname;
        }
        public override bool Equals(object obj)
        {
            if (obj is not prototypeDefault other)
                return false;

            return string.Equals(this.name, other.name, StringComparison.Ordinal) &&
                   this.lootmax == other.lootmax &&
                   this.lootmaxSpecified == other.lootmaxSpecified &&
                   string.Equals(this.enabled, other.enabled, StringComparison.Ordinal) &&
                   string.Equals(this.de, other.de, StringComparison.Ordinal) &&
                   this.width == other.width &&
                   this.widthSpecified == other.widthSpecified &&
                   this.height == other.height &&
                   this.heightSpecified == other.heightSpecified;
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class prototypeGroup
    {

        private BindingList<prototypeGroupValue> valueField;

        private BindingList<prototypeGroupUsage> usageField;

        private BindingList<prototypeGroupContainer> containerField;

        private BindingList<prototypeGroupProxy> dispatchField;

        private string nameField;

        private int lootmaxField;

        private bool lootmaxFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("value")]
        public BindingList<prototypeGroupValue> value
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("usage")]
        public BindingList<prototypeGroupUsage> usage
        {
            get
            {
                return this.usageField;
            }
            set
            {
                this.usageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("container")]
        public BindingList<prototypeGroupContainer> container
        {
            get
            {
                return this.containerField;
            }
            set
            {
                this.containerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("proxy", IsNullable = true)]
        public BindingList<prototypeGroupProxy> dispatch
        {
            get
            {
                return this.dispatchField;
            }
            set
            {
                this.dispatchField = value;
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

        public override string ToString()
        {
            return name;
        }

        public void AddTier(string tier)
        {
            if (value == null)
                value = new BindingList<prototypeGroupValue>();
            prototypeGroupValue newtier = (new prototypeGroupValue() { name = tier });
            if (!value.Any(x => x.name == newtier.name))
                value.Add(newtier);
            for (int i = 0; i < value.Count; i++)
            {
                if (value[i].name == null)
                {
                    value.RemoveAt(i);
                    i--;
                }
            }
        }
        public void removetier(string tier)
        {
            if (value == null) return;
            if (value.Any(x => x.name == tier))
                value.Remove(value.First(X => X.name == tier));
            //if (value.Count == 0)
            //    value = null;
        }
        public void AdduserTier(string tier)
        {
            if (value == null)
                value = new BindingList<prototypeGroupValue>();
            prototypeGroupValue newusertier = new prototypeGroupValue() { user = tier };
            if (!value.Any(x => x.user == newusertier.user))
                value.Add(newusertier);
            for (int i = 0; i < value.Count; i++)
            {
                if (value[i].user == null)
                {
                    value.RemoveAt(i);
                    i--;
                }
            }
        }
        public void removeusertier(string tier)
        {
            if (value == null) return;
            if (value.Any(x => x.user == tier))
                value.Remove(value.First(X => X.user == tier));
            //if (value.Count == 0)
            //{
            //    value = null;
            //}
        }
        public void removetiers()
        {
            if (value != null)
                value = null;
        }
        public void AddnewUsage(listsUsage u)
        {
            if (usage == null)
                usage = new BindingList<prototypeGroupUsage>();
            if (!usage.Any(x => x.name == u.name))
            {
                usage.Add(new prototypeGroupUsage() { name = u.name });
            }
        }
        public void removeusage(prototypeGroupUsage u)
        {
            if (usage == null) return;
            prototypeGroupUsage usagetoremove = usage.FirstOrDefault(x => x.name == u.name);
            if (usagetoremove != null)
                usage.Remove(usagetoremove);
        }

        public override bool Equals(object obj)
        {
            if (obj is not prototypeGroup other)
                return false;

            return string.Equals(name, other.name, StringComparison.Ordinal) &&
                   lootmax == other.lootmax &&
                   lootmaxSpecified == other.lootmaxSpecified &&
                   SequenceEqual(value, other.value) &&
                   SequenceEqual(usage, other.usage) &&
                   SequenceEqual(container, other.container) &&
                   SequenceEqual(dispatch, other.dispatch);
        }
        private static bool SequenceEqual<T>(IEnumerable<T> a, IEnumerable<T> b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.SequenceEqual(b);
        }
        public bool ShouldSerializedispatch()
        {
            return dispatch != null && dispatch.Count > 0;
        }

    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class prototypeGroupValue
    {

        private string userField;

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string user
        {
            get
            {
                return this.userField;
            }
            set
            {
                this.userField = value;
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

        public override bool Equals(object obj)
        {
            if (obj is not prototypeGroupValue other)
                return false;

            return string.Equals(user, other.user, StringComparison.Ordinal) &&
                   string.Equals(name, other.name, StringComparison.Ordinal);
        }
    }


    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class prototypeGroupUsage
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

        public override bool Equals(object obj)
        {
            if (obj is not prototypeGroupUsage other)
                return false;

            return string.Equals(name, other.name, StringComparison.Ordinal);
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class prototypeGroupContainer
    {

        private BindingList<prototypeGroupContainerCategory> categoryField;

        private BindingList<prototypeGroupContainerTag> tagField;

        private BindingList<prototypeGroupContainerPoint> pointField;

        private string nameField;

        private int lootmaxField;

        private bool lootmaxFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("category")]
        public BindingList<prototypeGroupContainerCategory> category
        {
            get
            {
                return this.categoryField;
            }
            set
            {
                this.categoryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("tag")]
        public BindingList<prototypeGroupContainerTag> tag
        {
            get
            {
                return this.tagField;
            }
            set
            {
                this.tagField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("point")]
        public BindingList<prototypeGroupContainerPoint> point
        {
            get
            {
                return this.pointField;
            }
            set
            {
                this.pointField = value;
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
        public override string ToString()
        {
            return name;
        }

        public void AddnewCategory(listsCategory c)
        {
            if (category == null)
                category = new BindingList<prototypeGroupContainerCategory>();
            if (!category.Any(x => x.name == c.name))
            {
                category.Add(new prototypeGroupContainerCategory() { name = c.name });
            }
        }
        public void removecategory(prototypeGroupContainerCategory c)
        {
            if (category == null) return;
            prototypeGroupContainerCategory cattoremove = category.FirstOrDefault(x => x.name == c.name);
            if (cattoremove != null)
                category.Remove(cattoremove);
        }
        public void Addnewtag(listsTag t)
        {
            if (tag == null)
                tag = new BindingList<prototypeGroupContainerTag>();
            if (!tag.Any(x => x.name == t.name))
            {
                tag.Add(new prototypeGroupContainerTag() { name = t.name });
            }
        }
        public void removetag(prototypeGroupContainerTag t)
        {
            if (tag == null) return;
            prototypeGroupContainerTag tagtoremove = tag.FirstOrDefault(x => x.name == t.name);
            if (tagtoremove != null)
                tag.Remove(tagtoremove);
        }

        public override bool Equals(object obj)
        {
            if (obj is not prototypeGroupContainer other)
                return false;

            return string.Equals(name, other.name, StringComparison.Ordinal) &&
                   lootmax == other.lootmax &&
                   lootmaxSpecified == other.lootmaxSpecified &&
                   SequenceEqual(category, other.category) &&
                   SequenceEqual(tag, other.tag) &&
                   SequenceEqual(point, other.point);
        }
        private static bool SequenceEqual<T>(IEnumerable<T> a, IEnumerable<T> b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.SequenceEqual(b);
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class prototypeGroupContainerCategory
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

        public override bool Equals(object obj)
        {
            if (obj is not prototypeGroupContainerCategory other)
                return false;

            return string.Equals(name, other.name, StringComparison.Ordinal);
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class prototypeGroupContainerTag
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

        public override bool Equals(object obj)
        {
            if (obj is not prototypeGroupContainerTag other)
                return false;

            return string.Equals(name, other.name, StringComparison.Ordinal);
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class prototypeGroupContainerPoint
    {

        private string posField;

        private decimal rangeField;

        private decimal heightField;

        private int flagsField;

        private bool flagsFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string pos
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
        public decimal range
        {
            get
            {
                return this.rangeField;
            }
            set
            {
                this.rangeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int flags
        {
            get
            {
                return this.flagsField;
            }
            set
            {
                this.flagsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool flagsSpecified
        {
            get
            {
                return this.flagsFieldSpecified;
            }
            set
            {
                this.flagsFieldSpecified = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is not prototypeGroupContainerPoint other)
                return false;

            return string.Equals(pos, other.pos, StringComparison.Ordinal) &&
                   range == other.range &&
                   height == other.height &&
                   flags == other.flags &&
                   flagsSpecified == other.flagsSpecified;
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class prototypeGroupProxy
    {

        private string typeField;

        private string posField;

        private string rpyField;

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
        public string pos
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
        public string rpy
        {
            get
            {
                return this.rpyField;
            }
            set
            {
                this.rpyField = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is not prototypeGroupProxy other)
                return false;

            return string.Equals(type, other.type, StringComparison.Ordinal) &&
                   string.Equals(pos, other.pos, StringComparison.Ordinal) &&
                   string.Equals(rpy, other.rpy, StringComparison.Ordinal);
        }
    }
}
