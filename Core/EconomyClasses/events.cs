using System.ComponentModel;
using System.Reflection;
using System.Xml.Serialization;

namespace Day2eEditor
{
    public class EventsConfig : ParameterizedMultiFileConfigLoaderBase<EventsFile>
    {
        public EventsConfig(string path) : base(path)
        {
        }

        protected override void LoadCore()
        {
            ResetState();

            if (string.IsNullOrWhiteSpace(VanillaPath))
            {
                HasErrors = true;
                _errors.Add("Vanilla path is missing.");
                return;
            }

            try
            {
                var vanilla = LoadItem(VanillaPath);
                vanilla.IsModded = false;
                vanilla.FileType = "events";

                OnAfterItemLoad(vanilla, VanillaPath);
                _clonedItems[GetID(vanilla)] = vanilla.Clone();

                var vanillaIssues = ValidateItem(vanilla);
                if (vanillaIssues?.Any() == true)
                {
                    Console.WriteLine("Validation issues in " + vanilla.FileName + ":");
                    foreach (var msg in vanillaIssues)
                        Console.WriteLine("- " + msg);
                }

                MutableItems.Add(vanilla);

                if (vanilla.HasErrors)
                {
                    HasErrors = true;
                    var fileName = Path.GetFileName(vanilla.FilePath);
                    _errors.AddRange(vanilla.Errors.Select(e => $"[Vanilla] [{fileName}] {e}"));
                }
            }
            catch (Exception ex)
            {
                HasErrors = true;
                HandleItemError(VanillaPath, ex);
            }

            foreach (var file in ModPaths)
            {
                try
                {
                    var item = LoadItem(file);
                    item.IsModded = true;
                    item.FileType = "events";
                    item.ModFolder = Path.GetRelativePath(BasePath, Path.GetDirectoryName(file) ?? BasePath);

                    OnAfterItemLoad(item, file);
                    _clonedItems[GetID(item)] = item.Clone();

                    var issues = ValidateItem(item);
                    if (issues?.Any() == true)
                    {
                        Console.WriteLine("Validation issues in " + item.FileName + ":");
                        foreach (var msg in issues)
                            Console.WriteLine("- " + msg);
                    }

                    MutableItems.Add(item);

                    if (item.HasErrors)
                    {
                        HasErrors = true;
                        var modName = Path.GetFileName(item.ModFolder);
                        var fileName = Path.GetFileName(item.FilePath);
                        _errors.AddRange(item.Errors.Select(e => $"[{modName}] [{fileName}] {e}"));
                    }
                }
                catch (Exception ex)
                {
                    HasErrors = true;
                    HandleItemError(file, ex);
                }
            }

            OnAfterLoadAll();
        }

        protected override EventsFile LoadItem(string filePath)
        {
            var item = new EventsFile(filePath);
            var loadFailed = false;

            item.Data = AppServices.GetRequired<FileService>().LoadOrCreateXml(
                filePath,
                createNew: () => new events
                {
                    @event = new BindingList<eventsEvent>()
                },
                onError: ex =>
                {
                    loadFailed = true;
                    item.HasErrors = true;

                    var message =
                        $"Error in {Path.GetFileName(filePath)}\n{ex.Message}\n{ex.InnerException?.Message}";

                    Console.WriteLine(message + "\n");
                    item.Errors.Add(message);
                },
                configName: "Events"
            );

            item.Data.@event ??= new BindingList<eventsEvent>();
            item.SetPath(filePath);
            item.SetGuid(Guid.NewGuid());

            if (!loadFailed)
            {
                ValidateLoadedEvents(item, item.Data);
            }

            return item;
        }

        protected override IEnumerable<string> ValidateItem(EventsFile item)
        {
            return item.Errors;
        }

        public override IEnumerable<string> Save()
        {
            var saved = new List<string>();

            for (int i = MutableItems.Count - 1; i >= 0; i--)
            {
                var item = MutableItems[i];
                var id = GetID(item);
                var fileName = GetItemFileName(item);
                var fullfielName = item.FilePath;

                if (ShouldDelete(item))
                {
                    DeleteItemFile(item);
                    MutableItems.RemoveAt(i);
                    _clonedItems.Remove(id);
                    saved.Add("File Remove " + fullfielName);
                    continue;
                }

                if (!_clonedItems.TryGetValue(id, out var baseline))
                {
                    SaveItem(item);
                    _clonedItems[id] = item.Clone();
                    saved.Add(fullfielName);
                    continue;
                }

                if (!item.Equals(baseline))
                {
                    var oldPath = baseline.FilePath;

                    SaveItem(item);

                    if (!string.Equals(oldPath, item.FilePath, StringComparison.OrdinalIgnoreCase) &&
                        !string.IsNullOrWhiteSpace(oldPath) &&
                        File.Exists(oldPath))
                    {
                        File.Delete(oldPath);
                    }

                    _clonedItems[id] = item.Clone();
                    saved.Add(fullfielName);
                }
            }
            return saved;
        }

        public override bool NeedToSave()
        {
            foreach (var item in Items)
            {
                var id = GetID(item);

                if (ShouldDelete(item))
                    return true;

                if (!_clonedItems.TryGetValue(id, out var baseline))
                    return true;

                if (!item.Equals(baseline))
                    return true;
            }

            return false;
        }

        protected override void SaveItem(EventsFile item)
        {
            AppServices.GetRequired<FileService>().SaveXml(item.FilePath, item.Data);
            item.IsDirty = false;
        }

        protected override string GetItemFileName(EventsFile item)
            => item.FileName;

        protected override Guid GetID(EventsFile item)
            => item.Id;

        protected override bool ShouldDelete(EventsFile item)
            => item.ToDelete;

        protected override void DeleteItemFile(EventsFile item)
        {
            if (!string.IsNullOrWhiteSpace(item.FilePath) && File.Exists(item.FilePath))
            {
                File.Delete(item.FilePath);
            }
        }

        protected override void HandleItemError(string path, Exception ex)
        {
            var msg = $"Error in {Path.GetFileName(path)}: {ex.Message}";
            _errors.Add(msg);
            Console.WriteLine(msg);
        }

        private void ValidateLoadedEvents(EventsFile owner, events cfg)
        {
            cfg.@event ??= new BindingList<eventsEvent>();

            foreach (var ev in cfg.@event)
            {
                CheckPropertiesRecursively(owner, ev, ev?.name ?? "UnknownEntry");
            }
        }

        private void CheckPropertiesRecursively(EventsFile owner, object obj, string topTypeName)
        {
            if (obj == null)
                return;

            var objType = obj.GetType();

            foreach (var property in objType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var propertyValue = property.GetValue(obj);

                if (propertyValue == null)
                    continue;

                if (typeof(System.Collections.IEnumerable).IsAssignableFrom(property.PropertyType)
                    && property.PropertyType != typeof(string))
                {
                    foreach (var item in (System.Collections.IEnumerable)propertyValue)
                    {
                        if (item != null)
                            CheckPropertiesRecursively(owner, item, topTypeName);
                    }
                    continue;
                }

                if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                {
                    CheckPropertiesRecursively(owner, propertyValue, topTypeName);
                    continue;
                }

                var specifiedProperty = objType.GetProperty(property.Name + "Specified");
                if (specifiedProperty != null && specifiedProperty.PropertyType == typeof(bool))
                {
                    var isSpecified = (bool)specifiedProperty.GetValue(obj)!;

                    if (isSpecified)
                    {
                        if (propertyValue == null)
                        {
                            owner.HasErrors = true;
                            owner.Errors.Add(
                                $"[{topTypeName}] → '{objType.Name}.{property.Name}' is null but marked as specified.");
                        }
                        else if (propertyValue is string str && string.IsNullOrWhiteSpace(str))
                        {
                            owner.HasErrors = true;
                            owner.Errors.Add(
                                $"[{topTypeName}] → '{objType.Name}.{property.Name}' is an empty string but marked as specified.");
                        }
                    }
                }
            }
        }

        private List<string> DeleteEmptyDirectoriesFromPath(string rootPath)
        {
            var removedFolders = new List<string>();

            if (!Directory.Exists(rootPath))
                return removedFolders;

            var directories = Directory
                .GetDirectories(rootPath, "*", SearchOption.AllDirectories)
                .OrderByDescending(d => d.Count(c =>
                    c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar))
                .ToList();

            foreach (var dir in directories)
            {
                if (!Directory.EnumerateFileSystemEntries(dir).Any())
                {
                    Directory.Delete(dir);
                    var relativePath = Path.GetRelativePath(rootPath, dir);
                    removedFolders.Add("Empty Folder Removed " + relativePath);
                }
            }

            return removedFolders;
        }
    }
    public class EventsFile : IDeepCloneable<EventsFile>, IEquatable<EventsFile>
    {
        private string _path;

        public string FilePath => _path;
        public string FileName => Path.GetFileName(_path);

        public bool ToDelete { get; set; }
        public bool IsDirty { get; set; }
        public bool HasErrors { get; set; }

        public List<string> Errors { get; } = new();

        public Guid Id { get; set; } = Guid.NewGuid();
        public string FileType { get; set; } = string.Empty;
        public bool IsModded { get; set; }
        public string ModFolder { get; set; } = string.Empty;

        public events Data { get; set; } = new()
        {
            @event = new BindingList<eventsEvent>()
        };

        public EventsFile(string path)
        {
            _path = path;
        }

        public void SetPath(string path) => _path = path;

        internal void SetGuid(Guid guid) => Id = guid;

        public EventsFile Clone()
        {
            var clone = new EventsFile(_path)
            {
                ToDelete = ToDelete,
                IsDirty = IsDirty,
                HasErrors = HasErrors,
                Id = Id,
                FileType = FileType,
                IsModded = IsModded,
                ModFolder = ModFolder,
                Data = Data?.Clone() ?? new events
                {
                    @event = new BindingList<eventsEvent>()
                }
            };

            clone.Errors.AddRange(Errors);
            return clone;
        }

        public bool Equals(EventsFile? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
                Id == other.Id &&
                string.Equals(_path, other._path, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(FileType, other.FileType, StringComparison.Ordinal) &&
                IsModded == other.IsModded &&
                string.Equals(ModFolder, other.ModFolder, StringComparison.OrdinalIgnoreCase) &&
                ToDelete == other.ToDelete &&
                Equals(Data, other.Data);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as EventsFile);
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


    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class events : IDeepCloneable<events>, IEquatable<events>
    {
        private BindingList<eventsEvent>? eventField = new();

        [XmlElement("event")]
        public BindingList<eventsEvent>? @event
        {
            get => eventField;
            set => eventField = value;
        }

        public void AddNewEvent(eventsEvent newEvent)
        {
            @event ??= new BindingList<eventsEvent>();
            @event.Add(newEvent);
        }

        public void RemoveEvent(eventsEvent currentEvent)
        {
            @event?.Remove(currentEvent);
        }

        public bool Equals(events? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return ListsEqual(@event, other.@event);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as events);
        }

        public events Clone()
        {
            return new events
            {
                @event = new BindingList<eventsEvent>(
                    @event?.Select(x => x.Clone()).ToList() ?? new List<eventsEvent>())
            };
        }

        private static bool ListsEqual<T>(IList<T>? a, IList<T>? b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (a is null || b is null)
                return false;

            if (a.Count != b.Count)
                return false;

            for (int i = 0; i < a.Count; i++)
            {
                if (!Equals(a[i], b[i]))
                    return false;
            }

            return true;
        }

    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class eventsEvent : IDeepCloneable<eventsEvent>, IEquatable<eventsEvent>
    {
        private int nominalField;
        private int minField;
        private int maxField;
        private int lifetimeField;
        private int restockField;
        private int saferadiusField;
        private int distanceradiusField;
        private int cleanupradiusField;
        private string? secondaryField;
        private eventsEventFlags? flagsField;
        private position positionField;
        private limit limitField;
        private int activeField;
        private BindingList<eventsEventChild>? childrenField = new();
        private string? nameField;

        public int nominal
        {
            get => nominalField;
            set => nominalField = value;
        }

        public int min
        {
            get => minField;
            set => minField = value;
        }

        public int max
        {
            get => maxField;
            set => maxField = value;
        }

        public int lifetime
        {
            get => lifetimeField;
            set => lifetimeField = value;
        }

        public int restock
        {
            get => restockField;
            set => restockField = value;
        }

        public int saferadius
        {
            get => saferadiusField;
            set => saferadiusField = value;
        }

        public int distanceradius
        {
            get => distanceradiusField;
            set => distanceradiusField = value;
        }

        public int cleanupradius
        {
            get => cleanupradiusField;
            set => cleanupradiusField = value;
        }

        public string? secondary
        {
            get => secondaryField;
            set => secondaryField = value;
        }

        public eventsEventFlags? flags
        {
            get => flagsField;
            set => flagsField = value;
        }

        public position position
        {
            get => positionField;
            set => positionField = value;
        }

        public limit limit
        {
            get => limitField;
            set => limitField = value;
        }

        public int active
        {
            get => activeField;
            set => activeField = value;
        }

        [XmlArrayItem("child", IsNullable = false)]
        public BindingList<eventsEventChild>? children
        {
            get => childrenField;
            set => childrenField = value;
        }

        [XmlAttribute]
        public string? name
        {
            get => nameField;
            set => nameField = value;
        }

        public void SetIntValue(string mytype, int myvalue)
        {
            GetType().GetProperty(mytype)?.SetValue(this, myvalue, null);
        }

        public override string ToString()
        {
            return name ?? string.Empty;
        }

        public void AddNewChild(eventsEventChild newChild)
        {
            children ??= new BindingList<eventsEventChild>();
            children.Add(newChild);
        }

        public void RemoveChild(eventsEventChild currentChild)
        {
            children?.Remove(currentChild);
        }

        public bool Equals(eventsEvent? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                nominal == other.nominal &&
                min == other.min &&
                max == other.max &&
                lifetime == other.lifetime &&
                restock == other.restock &&
                saferadius == other.saferadius &&
                distanceradius == other.distanceradius &&
                cleanupradius == other.cleanupradius &&
                string.Equals(secondary, other.secondary, StringComparison.Ordinal) &&
                Equals(flags, other.flags) &&
                position == other.position &&
                limit == other.limit &&
                active == other.active &&
                ListsEqual(children, other.children) &&
                string.Equals(name, other.name, StringComparison.Ordinal);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as eventsEvent);
        }

        public eventsEvent Clone()
        {
            return new eventsEvent
            {
                nominal = nominal,
                min = min,
                max = max,
                lifetime = lifetime,
                restock = restock,
                saferadius = saferadius,
                distanceradius = distanceradius,
                cleanupradius = cleanupradius,
                secondary = secondary,
                flags = flags?.Clone(),
                position = position,
                limit = limit,
                active = active,
                children = new BindingList<eventsEventChild>(
                    children?.Select(x => x.Clone()).ToList() ?? new List<eventsEventChild>()),
                name = name
            };
        }

        private static bool ListsEqual<T>(IList<T>? a, IList<T>? b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (a is null || b is null)
                return false;

            if (a.Count != b.Count)
                return false;

            for (int i = 0; i < a.Count; i++)
            {
                if (!Equals(a[i], b[i]))
                    return false;
            }

            return true;
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class eventsEventFlags : IDeepCloneable<eventsEventFlags>, IEquatable<eventsEventFlags>
    {
        private int deletableField;
        private int init_randomField;
        private int remove_damagedField;

        [XmlAttribute]
        public int deletable
        {
            get => deletableField;
            set => deletableField = value;
        }

        [XmlAttribute]
        public int init_random
        {
            get => init_randomField;
            set => init_randomField = value;
        }

        [XmlAttribute]
        public int remove_damaged
        {
            get => remove_damagedField;
            set => remove_damagedField = value;
        }

        public bool Equals(eventsEventFlags? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return deletable == other.deletable &&
                   init_random == other.init_random &&
                   remove_damaged == other.remove_damaged;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as eventsEventFlags);
        }

        public eventsEventFlags Clone()
        {
            return new eventsEventFlags
            {
                deletable = deletable,
                init_random = init_random,
                remove_damaged = remove_damaged
            };
        }
    }


    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class eventsEventChild : IDeepCloneable<eventsEventChild>, IEquatable<eventsEventChild>
    {
        private int lootmaxField;
        private int lootminField;
        private int maxField;
        private int minField;
        private string? typeField;

        [XmlAttribute]
        public int lootmax
        {
            get => lootmaxField;
            set => lootmaxField = value;
        }

        [XmlAttribute]
        public int lootmin
        {
            get => lootminField;
            set => lootminField = value;
        }

        [XmlAttribute]
        public int max
        {
            get => maxField;
            set => maxField = value;
        }

        [XmlAttribute]
        public int min
        {
            get => minField;
            set => minField = value;
        }

        [XmlAttribute]
        public string? type
        {
            get => typeField;
            set => typeField = value;
        }

        public void SetIntValue(string mytype, int myvalue)
        {
            GetType().GetProperty(mytype)?.SetValue(this, myvalue, null);
        }

        public override string ToString()
        {
            return type ?? string.Empty;
        }

        public bool Equals(eventsEventChild? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return lootmax == other.lootmax &&
                   lootmin == other.lootmin &&
                   max == other.max &&
                   min == other.min &&
                   string.Equals(type, other.type, StringComparison.Ordinal);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as eventsEventChild);
        }

        public eventsEventChild Clone()
        {
            return new eventsEventChild
            {
                lootmax = lootmax,
                lootmin = lootmin,
                max = max,
                min = min,
                type = type
            };
        }
    }
    #endregion events
}
