using Day2eEditor;
using System.ComponentModel;

namespace DayZeLib
{
    public class cfgignorelistConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;
        public ignore Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }

        public cfgignorelistConfig(string path)
        {
            _path = path;
        }
        public void Load()
        {
            Data = AppServices.GetRequired<FileService>().LoadOrCreateXml<ignore>(
               _path,
               createNew: () => new ignore(),
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
               configName: "cfgignorelist"
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
    public partial class ignore
    {

        private BindingList<ignoreType> typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("type")]
        public BindingList<ignoreType> type
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

        public override bool Equals(object obj)
        {
            if (obj is not ignore other) return false;

            if (type == null && other.type == null) return true;
            if (type == null || other.type == null) return false;

            if (type.Count != other.type.Count) return false;

            // Compare each ignoreType in order
            for (int i = 0; i < type.Count; i++)
            {
                if (!type[i].Equals(other.type[i]))
                    return false;
            }

            return true;
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ignoreType
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

        public override bool Equals(object obj)
        {
            if (obj is not ignoreType other) return false;
            return string.Equals(name, other.name, StringComparison.Ordinal);
        }

    }


}
