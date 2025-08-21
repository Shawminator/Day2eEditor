using System.ComponentModel;
using System.Reflection;

namespace Day2eEditor
{
    public class eventsConfig : IAdvancedConfigLoader
    {
        public string _basepath { get; set; }
        public List<EventsFile> AllData { get; private set; } = new List<EventsFile>();
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();

        public void Load() => throw new InvalidOperationException("Use LoadWithParameters for this config.");
        public void LoadWithParameters(string basePath, string vanillaPath, List<string> modPaths)
        {
            _basepath = basePath;
            HasErrors = false;
            Errors.Clear();
            // Load vanilla file
            var vanilla = new EventsFile(vanillaPath)
            {
                IsModded = false,
                FileType = "events"
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
                var modFile = new EventsFile(modPath)
                {
                    IsModded = true,
                    FileType = "events",
                    ModFolder = Path.GetRelativePath(basePath, Path.GetDirectoryName(modPath))
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
        public IEnumerable<string> Save()
        {
            var savedFiles = new List<string>();

            foreach (var data in AllData)
            {
                if (data.isDirty)
                {
                    savedFiles.AddRange(data.Save());
                }
            }

            return savedFiles;
        }
        public bool needToSave()
        {
            foreach (var Data in AllData)
            {
                if (Data.needToSave())
                    return true;
            }
            return false;
        }
    }
    public class EventsFile : IConfigLoader
    {
        private readonly string _path;

        public events Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }

        // Metadata for file type and source
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;                  // Full file path
        public string FileType { get; set; }               // "types"
        public bool IsModded { get; set; }                 // true if modded, false if vanilla
        public string ModFolder { get; set; }

        public EventsFile(string path)
        {
            _path = path;
        }
        public void Load()
        {
            Data = AppServices.GetRequired<FileService>().LoadOrCreateXml<events>(
               _path,
               createNew: () => new events(),
               onAfterLoad: cfg =>
               {
                   CheckValuesAfterLoad(cfg);
                   if (HasErrors)
                   {
                       throw new Exception("Validation failed.");
                   }
               },
               onError: ex =>
               {
                   HasErrors = true;

                   if (ex.Message == "Validation failed.")
                   {
                       Console.WriteLine("Validation errors found:\n" + string.Join("\n", Errors));
                   }
                   else
                   {
                       var message = "Error in " + Path.GetFileName(_path) + "\n" +
                                     ex.Message + "\n" +
                                     ex.InnerException?.Message;

                       Console.WriteLine(message + "\n");
                       Errors.Add(message);
                   }
               },
               configName: "Events"
            );
        }
        public IEnumerable<string> Save()
        {
            if (isDirty)
            {
                AppServices.GetRequired<FileService>().SaveXml(_path, Data);
                isDirty = false;
                return new[] { FileName };
            }

            return Array.Empty<string>();
        }
        public bool needToSave()
        {
            return isDirty;
        }
        private void CheckValuesAfterLoad(events cfg)
        {
            foreach (var type in cfg.@event)
            {
                // Recursively check each property in the type
                CheckPropertiesRecursively(type, (type as eventsEvent)?.name ?? "UnknownEntry");
            }
        }
        private void CheckPropertiesRecursively(object obj, string topTypeName)
        {
            if (obj == null)
                return;

            Type objType = obj.GetType();

            foreach (var property in objType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                object propertyValue = property.GetValue(obj);

                // Skip nulls
                if (propertyValue == null)
                    continue;

                // Handle collections like BindingList<T>, List<T>, etc.
                if (typeof(System.Collections.IEnumerable).IsAssignableFrom(property.PropertyType)
                    && property.PropertyType != typeof(string))
                {
                    foreach (var item in (System.Collections.IEnumerable)propertyValue)
                    {
                        CheckPropertiesRecursively(item, topTypeName); // Recursively check each item
                    }
                    continue; // skip rest of logic for collections
                }

                // Handle nested complex types (but not strings or value types)
                if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                {
                    CheckPropertiesRecursively(propertyValue, topTypeName); // Recursive for objects
                    continue;
                }

                // Handle Specified logic
                var specifiedProperty = objType.GetProperty(property.Name + "Specified");
                if (specifiedProperty != null && specifiedProperty.PropertyType == typeof(bool))
                {
                    bool isSpecified = (bool)specifiedProperty.GetValue(obj);

                    if (isSpecified)
                    {
                        if (propertyValue == null)
                        {
                            HasErrors = true;
                            Errors.Add($"[{topTypeName}] → '{objType.Name}.{property.Name}' is null but marked as specified.");
                        }
                        else if (propertyValue is string str && string.IsNullOrWhiteSpace(str))
                        {
                            HasErrors = true;
                            Errors.Add($"[{topTypeName}] → '{objType.Name}.{property.Name}' is an empty string but marked as specified.");
                        }
                    }
                }
            }
        }
    }
    #region Events
    public enum position
    {
        [Description("Fixed")]
        @fixed,
        [Description("Player")]
        player,
        [Description("Uniform")]
        uniform
    };
    public enum limit
    {
        [Description("Mixed")]
        mixed,
        [Description("Custom")]
        custom,
        [Description("Child")]
        child,
        [Description("Parent")]
        parent
    };


    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class events
    {

        private BindingList<eventsEvent> eventField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("event")]
        public BindingList<eventsEvent> @event
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
        public void AddnewEvent(eventsEvent neweventEvent)
        {
            @event.Add(neweventEvent);
        }
        public void RemoveEvent(eventsEvent currentEvent)
        {
            @event.Remove(currentEvent);
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class eventsEvent : IEquatable<eventsEvent>
    {
        public override bool Equals(object obj) => Equals(obj as eventsEvent);

        private int nominalField;
        private int minField;
        private int maxField;
        private int lifetimeField;
        private int restockField;
        private int saferadiusField;
        private int distanceradiusField;
        private int cleanupradiusField;
        private string secondaryField;
        private eventsEventFlags flagsField;
        private position positionField;
        private limit limitField;
        private int activeField;
        private BindingList<eventsEventChild> childrenField;
        private string nameField;

        /// <remarks/>
        public int nominal
        {
            get
            {
                return this.nominalField;
            }
            set
            {
                this.nominalField = value;
            }
        }
        /// <remarks/>
        public int min
        {
            get
            {
                return this.minField;
            }
            set
            {
                this.minField = value;
            }
        }
        /// <remarks/>
        public int max
        {
            get
            {
                return this.maxField;
            }
            set
            {
                this.maxField = value;
            }
        }
        /// <remarks/>
        public int lifetime
        {
            get
            {
                return this.lifetimeField;
            }
            set
            {
                this.lifetimeField = value;
            }
        }
        /// <remarks/>
        public int restock
        {
            get
            {
                return this.restockField;
            }
            set
            {
                this.restockField = value;
            }
        }
        /// <remarks/>
        public int saferadius
        {
            get
            {
                return this.saferadiusField;
            }
            set
            {
                this.saferadiusField = value;
            }
        }
        /// <remarks/>
        public int distanceradius
        {
            get
            {
                return this.distanceradiusField;
            }
            set
            {
                this.distanceradiusField = value;
            }
        }
        /// <remarks/>
        public int cleanupradius
        {
            get
            {
                return this.cleanupradiusField;
            }
            set
            {
                this.cleanupradiusField = value;
            }
        }
        /// <remarks/>
        public string secondary
        {
            get
            {
                return this.secondaryField;
            }
            set
            {
                this.secondaryField = value;
            }
        }
        /// <remarks/>
        public eventsEventFlags flags
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
        public position position
        {
            get
            {
                return this.positionField;
            }
            set
            {
                this.positionField = value;
            }
        }
        /// <remarks/>
        public limit limit
        {
            get
            {
                return this.limitField;
            }
            set
            {
                this.limitField = value;
            }
        }
        /// <remarks/>
        public int active
        {
            get
            {
                return this.activeField;
            }
            set
            {
                this.activeField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("child", IsNullable = false)]
        public BindingList<eventsEventChild> children
        {
            get
            {
                return this.childrenField;
            }
            set
            {
                this.childrenField = value;
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
        public void SetIntValue(string mytype, int myvalue)
        {
            GetType().GetProperty(mytype).SetValue(this, myvalue, null);
        }
        public override string ToString()
        {
            return name;
        }
        public void Addnechild(eventsEventChild neweventeventschild)
        {
            children.Add(neweventeventschild);
        }
        public void Removechild(eventsEventChild currentChild)
        {
            children.Remove(currentChild);
        }
        public bool Equals(eventsEvent other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            // Compare simple fields
            bool basicEqual =
                nominal == other.nominal &&
                min == other.min &&
                max == other.max &&
                lifetime == other.lifetime &&
                restock == other.restock &&
                saferadius == other.saferadius &&
                distanceradius == other.distanceradius &&
                cleanupradius == other.cleanupradius &&
                secondary == other.secondary &&
                active == other.active &&
                name == other.name &&
                position == other.position &&
                limit == other.limit;

            if (!basicEqual) return false;

            // Compare flags
            if ((flags is null) != (other.flags is null)) return false;
            if (flags != null)
            {
                if (flags.deletable != other.flags.deletable ||
                    flags.init_random != other.flags.init_random ||
                    flags.remove_damaged != other.flags.remove_damaged)
                    return false;
            }

            // Compare children
            if ((children is null) != (other.children is null)) return false;
            if (children != null)
            {
                if (children.Count != other.children.Count) return false;
                for (int i = 0; i < children.Count; i++)
                {
                    if (!children[i].Equals(other.children[i]))
                        return false;
                }
            }

            return true;
        }
        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(nominal);
            hash.Add(min);
            hash.Add(max);
            hash.Add(lifetime);
            hash.Add(restock);
            hash.Add(saferadius);
            hash.Add(distanceradius);
            hash.Add(cleanupradius);
            hash.Add(secondary);
            hash.Add(active);
            hash.Add(name);
            hash.Add(position);
            hash.Add(limit);

            if (flags != null)
            {
                hash.Add(flags.deletable);
                hash.Add(flags.init_random);
                hash.Add(flags.remove_damaged);
            }

            if (children != null)
            {
                foreach (var child in children)
                    hash.Add(child);
            }

            return hash.ToHashCode();
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class eventsEventFlags : IEquatable<eventsEventFlags>
    {
        public override bool Equals(object obj) => Equals(obj as eventsEventFlags);

        private int deletableField;
        private int init_randomField;
        private int remove_damagedField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int deletable
        {
            get
            {
                return this.deletableField;
            }
            set
            {
                this.deletableField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int init_random
        {
            get
            {
                return this.init_randomField;
            }
            set
            {
                this.init_randomField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int remove_damaged
        {
            get
            {
                return this.remove_damagedField;
            }
            set
            {
                this.remove_damagedField = value;
            }
        }

        public bool Equals(eventsEventFlags other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return deletable == other.deletable &&
                   init_random == other.init_random &&
                   remove_damaged == other.remove_damaged;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(deletable);
            hash.Add(init_random);
            hash.Add(remove_damaged);
            return hash.ToHashCode();
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class eventsEventChild : IEquatable<eventsEventChild>
    {
        public override bool Equals(object obj) => Equals(obj as eventsEventChild);

        private int lootmaxField;
        private int lootminField;
        private int maxField;
        private int minField;
        private string typeField;

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
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int max
        {
            get
            {
                return this.maxField;
            }
            set
            {
                this.maxField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int min
        {
            get
            {
                return this.minField;
            }
            set
            {
                this.minField = value;
            }
        }
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
        public void SetIntValue(string mytype, int myvalue)
        {
            GetType().GetProperty(mytype).SetValue(this, myvalue, null);
        }
        public override string ToString()
        {
            return type;
        }
        public bool Equals(eventsEventChild other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return lootmax == other.lootmax &&
                   lootmin == other.lootmin &&
                   max == other.max &&
                   min == other.min &&
                   type == other.type;
        }
        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(lootmax);
            hash.Add(lootmin);
            hash.Add(max);
            hash.Add(min);
            hash.Add(type);
            return hash.ToHashCode();
        }
    }
    #endregion events
}
