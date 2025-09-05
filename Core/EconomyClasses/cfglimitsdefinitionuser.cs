using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Day2eEditor
{
    public class cfglimitsdefinitionuserConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;
        public cfglimitsdefinitionuser Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }

        public cfglimitsdefinitionuserConfig() { }
        public cfglimitsdefinitionuserConfig(string path)
        {
            _path = path;
        }

        public void Load()
        {
            Data = AppServices.GetRequired<FileService>().LoadOrCreateXml<cfglimitsdefinitionuser>(
               _path,
               createNew: () => new cfglimitsdefinitionuser(),
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
               configName: "cfglimitsdefinitionuser"
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


    [XmlRoot("user_lists")]
    public partial class cfglimitsdefinitionuser
    {

        private BindingList<user_listsUser> usageflagsField;

        private BindingList<user_listsUser1> valueflagsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("user", IsNullable = false)]
        public BindingList<user_listsUser> usageflags
        {
            get => usageflagsField;
            set => usageflagsField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("user", IsNullable = false)]
        public BindingList<user_listsUser1> valueflags
        {
            get => valueflagsField;
            set => valueflagsField = value;
        }

        public override bool Equals(object obj) => Equals(obj as cfglimitsdefinitionuser);

        public bool Equals(cfglimitsdefinitionuser other)
        {
            if (other is null) return false;
            return Enumerable.SequenceEqual(usageflags ?? new BindingList<user_listsUser>(), other.usageflags ?? new BindingList<user_listsUser>())
                && Enumerable.SequenceEqual(valueflags ?? new BindingList<user_listsUser1>(), other.valueflags ?? new BindingList<user_listsUser1>());
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                if (usageflags != null)
                    foreach (var u in usageflags) hash = hash * 23 + (u?.GetHashCode() ?? 0);
                if (valueflags != null)
                    foreach (var v in valueflags) hash = hash * 23 + (v?.GetHashCode() ?? 0);
                return hash;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class user_listsUser
    {

        private BindingList<user_listsUserUsage> usageField;
        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("usage")]
        public BindingList<user_listsUserUsage> usage
        {
            get => usageField;
            set => usageField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get => nameField;
            set => nameField = value;
        }
        public override string ToString() => name;
        public override bool Equals(object obj) => Equals(obj as user_listsUser);

        public bool Equals(user_listsUser other)
        {
            if (other is null) return false;
            return string.Equals(name, other.name)
                && Enumerable.SequenceEqual(usage ?? new BindingList<user_listsUserUsage>(), other.usage ?? new BindingList<user_listsUserUsage>());
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = name?.GetHashCode() ?? 0;
                if (usage != null)
                    foreach (var u in usage) hash = hash * 23 + (u?.GetHashCode() ?? 0);
                return hash;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class user_listsUserUsage
    {

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get => nameField;
            set => nameField = value;
        }

        public override string ToString() => name;

        public override bool Equals(object obj) => Equals(obj as user_listsUserUsage);

        public bool Equals(user_listsUserUsage other)
        {
            if (other is null) return false;
            return string.Equals(name, other.name);
        }

        public override int GetHashCode() => name?.GetHashCode() ?? 0;
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class user_listsUser1
    {

        private BindingList<user_listsUserValue> valueField;

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("value")]
        public BindingList<user_listsUserValue> value
        {
            get => valueField;
            set => valueField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get => nameField;
            set => nameField = value;
        }
        public override string ToString() => name;

        public override bool Equals(object obj) => Equals(obj as user_listsUser1);

        public bool Equals(user_listsUser1 other)
        {
            if (other is null) return false;
            return string.Equals(name, other.name)
                && Enumerable.SequenceEqual(value ?? new BindingList<user_listsUserValue>(), other.value ?? new BindingList<user_listsUserValue>());
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = name?.GetHashCode() ?? 0;
                if (value != null)
                    foreach (var v in value) hash = hash * 23 + (v?.GetHashCode() ?? 0);
                return hash;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class user_listsUserValue
    {

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get => nameField;
            set => nameField = value;
        }
        public override string ToString() => name;

        public override bool Equals(object obj) => Equals(obj as user_listsUserValue);

        public bool Equals(user_listsUserValue other)
        {
            if (other is null) return false;
            return string.Equals(name, other.name);
        }

        public override int GetHashCode() => name?.GetHashCode() ?? 0;
    }
}
