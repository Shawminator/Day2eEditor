using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Day2eEditor
{
    public class SpawnGearPresetsConfig : MultiFileConfigLoaderBase<SpawnGearPresetFile>
    {
        public SpawnGearPresetsConfig(string basePath) : base(basePath)
        {
        }

        public void Load(string basePath, BindingList<string> presetPaths)
        {
            BasePath = basePath;
            ResetState();

            foreach (var relativePath in presetPaths ?? new BindingList<string>())
            {
                var fullPath = Path.Combine(BasePath, relativePath.Replace('/', Path.DirectorySeparatorChar));

                try
                {
                    var item = LoadItem(fullPath);

                    OnAfterItemLoad(item, fullPath);
                    _clonedItems[GetID(item)] = item.Clone();

                    var issues = ValidateItem(item);
                    if (issues?.Any() == true)
                    {
                        Console.WriteLine("Validation issues in " + item.FileName + ":");
                        foreach (var msg in issues)
                            Console.WriteLine("- " + msg);
                    }

                    MutableItems.Add(item);
                }
                catch (Exception ex)
                {
                    HasErrors = true;
                    HandleItemError(fullPath, ex);
                }
            }

            OnAfterLoadAll();
        }

        protected override SpawnGearPresetFile LoadItem(string filePath)
        {
            var data = AppServices.GetRequired<FileService>().LoadOrCreateJson<SpawnGearPresetData>(
                filePath,
                createNew: () => new SpawnGearPresetData(),
                configName: "SpawnGearPreset",
                useBoolConvertor: false
            );

            return new SpawnGearPresetFile(filePath)
            {
                Data = data,
                ModFolder = Path.GetRelativePath(BasePath, Path.GetDirectoryName(filePath) ?? BasePath)
            };
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
        protected override void SaveItem(SpawnGearPresetFile item)
        {
            AppServices.GetRequired<FileService>().SaveJson(item.FilePath, item.Data);
            item.IsDirty = false;
        }

        protected override string GetItemFileName(SpawnGearPresetFile item) => item.FileName;

        protected override Guid GetID(SpawnGearPresetFile item) => item.Id;

        protected override bool ShouldDelete(SpawnGearPresetFile item) => item.ToDelete;

        protected override void DeleteItemFile(SpawnGearPresetFile item)
        {
            if (!string.IsNullOrWhiteSpace(item.FilePath) && File.Exists(item.FilePath))
            {
                File.Delete(item.FilePath);
                Helper.DeleteEmptyFoldersUpToBase(
                    Path.GetDirectoryName(item.FilePath),
                    AppServices.GetRequired<EconomyManager>().basePath);
            }
        }
    }

    public class SpawnGearPresetFile : IDeepCloneable<SpawnGearPresetFile>, IEquatable<SpawnGearPresetFile>
    {
        private readonly string _path;

        public string FileName => Path.GetFileName(_path);
        public string FilePath => _path;

        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsDirty { get; set; }
        public bool ToDelete { get; set; }
        public string ModFolder { get; set; } = string.Empty;

        public SpawnGearPresetData Data { get; set; } = new();

        public SpawnGearPresetFile(string path)
        {
            _path = path;
        }

        public SpawnGearPresetFile Clone()
        {
            return new SpawnGearPresetFile(_path)
            {
                Id = Id,
                IsDirty = IsDirty,
                ToDelete = ToDelete,
                ModFolder = ModFolder,
                Data = Data.Clone()
            };
        }

        public bool Equals(SpawnGearPresetFile? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                Id == other.Id &&
                string.Equals(_path, other._path, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(ModFolder, other.ModFolder, StringComparison.OrdinalIgnoreCase) &&
                ToDelete == other.ToDelete &&
                Equals(Data, other.Data);
        }

        public override bool Equals(object? obj) => Equals(obj as SpawnGearPresetFile);
    }

    public class SpawnGearPresetData : IHasSpawnWeight, IHasSpawnName, IDeepCloneable<SpawnGearPresetData>, IEquatable<SpawnGearPresetData>
    {
        public string name { get; set; } = string.Empty;
        public int spawnWeight { get; set; } = 1;
        public BindingList<string> characterTypes { get; set; } = new();
        public BindingList<Attachmentslotitemset> attachmentSlotItemSets { get; set; } = new();
        public BindingList<Discreteunsorteditemset> discreteUnsortedItemSets { get; set; } = new();

        [JsonIgnore]
        public int SpawnWeight { get => spawnWeight; set => spawnWeight = value; }

        [JsonIgnore]
        public string Name { get => name; set => name = value; }

        public override string ToString() => name;

        public SpawnGearPresetData Clone()
        {
            return new SpawnGearPresetData
            {
                name = name,
                spawnWeight = spawnWeight,
                characterTypes = new BindingList<string>(characterTypes?.ToList() ?? new List<string>()),
                attachmentSlotItemSets = new BindingList<Attachmentslotitemset>(
                    attachmentSlotItemSets?.Select(x => x.Clone()).ToList() ?? new List<Attachmentslotitemset>()),
                discreteUnsortedItemSets = new BindingList<Discreteunsorteditemset>(
                    discreteUnsortedItemSets?.Select(x => x.Clone()).ToList() ?? new List<Discreteunsorteditemset>())
            };
        }

        public bool Equals(SpawnGearPresetData? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                name == other.name &&
                spawnWeight == other.spawnWeight &&
                ListEqual(characterTypes, other.characterTypes) &&
                ListEqual(attachmentSlotItemSets, other.attachmentSlotItemSets) &&
                ListEqual(discreteUnsortedItemSets, other.discreteUnsortedItemSets);
        }

        public override bool Equals(object? obj) => Equals(obj as SpawnGearPresetData);

        private static bool ListEqual<T>(IList<T>? a, IList<T>? b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            if (a.Count != b.Count) return false;

            for (int i = 0; i < a.Count; i++)
            {
                if (!Equals(a[i], b[i]))
                    return false;
            }

            return true;
        }
    }

    public class Attachmentslotitemset : IDeepCloneable<Attachmentslotitemset>, IEquatable<Attachmentslotitemset>
    {
        public string slotName { get; set; } = string.Empty;
        public BindingList<Discreteitemset> discreteItemSets { get; set; } = new();

        public Attachmentslotitemset Clone()
        {
            return new Attachmentslotitemset
            {
                slotName = slotName,
                discreteItemSets = new BindingList<Discreteitemset>(
                    discreteItemSets?.Select(x => x.Clone()).ToList() ?? new List<Discreteitemset>())
            };
        }

        public bool Equals(Attachmentslotitemset? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return slotName == other.slotName &&
                   ListEqual(discreteItemSets, other.discreteItemSets);
        }

        public override bool Equals(object? obj) => Equals(obj as Attachmentslotitemset);

        private static bool ListEqual<T>(IList<T>? a, IList<T>? b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            if (a.Count != b.Count) return false;

            for (int i = 0; i < a.Count; i++)
            {
                if (!Equals(a[i], b[i]))
                    return false;
            }

            return true;
        }
    }

    public class Discreteitemset : IHasSpawnWeight, IHasSpawnItemType, IHasSimpleChildren, IHasQuikBarSlot, IHassimpleChildrenUseDefaultAttributes,
        IDeepCloneable<Discreteitemset>, IEquatable<Discreteitemset>
    {
        public string itemType { get; set; } = string.Empty;
        public int spawnWeight { get; set; }
        public Attributes attributes { get; set; } = new();
        public int quickBarSlot { get; set; }
        public BindingList<Complexchildrentype> complexChildrenTypes { get; set; } = new();
        public bool simpleChildrenUseDefaultAttributes { get; set; }
        public BindingList<string> simpleChildrenTypes { get; set; } = new();

        [JsonIgnore]
        public string ItemType { get => itemType; set => itemType = value; }

        [JsonIgnore]
        public int SpawnWeight { get => spawnWeight; set => spawnWeight = value; }

        [JsonIgnore]
        public int QuickBarSlot { get => quickBarSlot; set => quickBarSlot = value; }

        [JsonIgnore]
        public bool SimpleChildrenUseDefaultAttributes
        {
            get => simpleChildrenUseDefaultAttributes;
            set => simpleChildrenUseDefaultAttributes = value;
        }

        [JsonIgnore]
        public BindingList<string> SimpleChildrenTypes
        {
            get => simpleChildrenTypes;
            set => simpleChildrenTypes = value;
        }

        public Discreteitemset Clone()
        {
            return new Discreteitemset
            {
                itemType = itemType,
                spawnWeight = spawnWeight,
                attributes = attributes?.Clone() ?? new Attributes(),
                quickBarSlot = quickBarSlot,
                complexChildrenTypes = new BindingList<Complexchildrentype>(
                    complexChildrenTypes?.Select(x => x.Clone()).ToList() ?? new List<Complexchildrentype>()),
                simpleChildrenUseDefaultAttributes = simpleChildrenUseDefaultAttributes,
                simpleChildrenTypes = new BindingList<string>(simpleChildrenTypes?.ToList() ?? new List<string>())
            };
        }

        public bool Equals(Discreteitemset? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return itemType == other.itemType &&
                   spawnWeight == other.spawnWeight &&
                   Equals(attributes, other.attributes) &&
                   quickBarSlot == other.quickBarSlot &&
                   simpleChildrenUseDefaultAttributes == other.simpleChildrenUseDefaultAttributes &&
                   ListEqual(complexChildrenTypes, other.complexChildrenTypes) &&
                   ListEqual(simpleChildrenTypes, other.simpleChildrenTypes);
        }

        public override bool Equals(object? obj) => Equals(obj as Discreteitemset);

        private static bool ListEqual<T>(IList<T>? a, IList<T>? b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            if (a.Count != b.Count) return false;

            for (int i = 0; i < a.Count; i++)
            {
                if (!Equals(a[i], b[i]))
                    return false;
            }

            return true;
        }
    }

    public class Attributes : IDeepCloneable<Attributes>, IEquatable<Attributes>
    {
        public decimal healthMin { get; set; }
        public decimal healthMax { get; set; }
        public decimal quantityMin { get; set; }
        public decimal quantityMax { get; set; }

        public Attributes Clone()
        {
            return new Attributes
            {
                healthMin = healthMin,
                healthMax = healthMax,
                quantityMin = quantityMin,
                quantityMax = quantityMax
            };
        }

        public bool Equals(Attributes? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return healthMin == other.healthMin &&
                   healthMax == other.healthMax &&
                   quantityMin == other.quantityMin &&
                   quantityMax == other.quantityMax;
        }

        public override bool Equals(object? obj) => Equals(obj as Attributes);
    }

    public class Complexchildrentype : IHasSpawnItemType, IHasSimpleChildren, IHasQuikBarSlot, IHassimpleChildrenUseDefaultAttributes,
        IDeepCloneable<Complexchildrentype>, IEquatable<Complexchildrentype>
    {
        public string itemType { get; set; } = string.Empty;
        public Attributes attributes { get; set; } = new();
        public int quickBarSlot { get; set; }
        public bool simpleChildrenUseDefaultAttributes { get; set; }
        public BindingList<string> simpleChildrenTypes { get; set; } = new();

        [JsonIgnore]
        public string ItemType { get => itemType; set => itemType = value; }

        [JsonIgnore]
        public int QuickBarSlot { get => quickBarSlot; set => quickBarSlot = value; }

        [JsonIgnore]
        public bool SimpleChildrenUseDefaultAttributes
        {
            get => simpleChildrenUseDefaultAttributes;
            set => simpleChildrenUseDefaultAttributes = value;
        }

        [JsonIgnore]
        public BindingList<string> SimpleChildrenTypes
        {
            get => simpleChildrenTypes;
            set => simpleChildrenTypes = value;
        }

        public Complexchildrentype Clone()
        {
            return new Complexchildrentype
            {
                itemType = itemType,
                attributes = attributes?.Clone() ?? new Attributes(),
                quickBarSlot = quickBarSlot,
                simpleChildrenUseDefaultAttributes = simpleChildrenUseDefaultAttributes,
                simpleChildrenTypes = new BindingList<string>(simpleChildrenTypes?.ToList() ?? new List<string>())
            };
        }

        public bool Equals(Complexchildrentype? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return itemType == other.itemType &&
                   Equals(attributes, other.attributes) &&
                   quickBarSlot == other.quickBarSlot &&
                   simpleChildrenUseDefaultAttributes == other.simpleChildrenUseDefaultAttributes &&
                   ListEqual(simpleChildrenTypes, other.simpleChildrenTypes);
        }

        public override bool Equals(object? obj) => Equals(obj as Complexchildrentype);

        private static bool ListEqual<T>(IList<T>? a, IList<T>? b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            if (a.Count != b.Count) return false;

            for (int i = 0; i < a.Count; i++)
            {
                if (!Equals(a[i], b[i]))
                    return false;
            }

            return true;
        }
    }

    public class Discreteunsorteditemset : IHasSpawnWeight, IHasSpawnName, IHasSimpleChildren, IHassimpleChildrenUseDefaultAttributes,
        IDeepCloneable<Discreteunsorteditemset>, IEquatable<Discreteunsorteditemset>
    {
        public string name { get; set; } = string.Empty;
        public int spawnWeight { get; set; }
        public Attributes attributes { get; set; } = new();
        public BindingList<Complexchildrentype> complexChildrenTypes { get; set; } = new();
        public bool simpleChildrenUseDefaultAttributes { get; set; }
        public BindingList<string> simpleChildrenTypes { get; set; } = new();

        [JsonIgnore]
        public int SpawnWeight { get => spawnWeight; set => spawnWeight = value; }

        [JsonIgnore]
        public string Name { get => name; set => name = value; }

        [JsonIgnore]
        public bool SimpleChildrenUseDefaultAttributes
        {
            get => simpleChildrenUseDefaultAttributes;
            set => simpleChildrenUseDefaultAttributes = value;
        }

        [JsonIgnore]
        public BindingList<string> SimpleChildrenTypes
        {
            get => simpleChildrenTypes;
            set => simpleChildrenTypes = value;
        }

        public Discreteunsorteditemset Clone()
        {
            return new Discreteunsorteditemset
            {
                name = name,
                spawnWeight = spawnWeight,
                attributes = attributes?.Clone() ?? new Attributes(),
                complexChildrenTypes = new BindingList<Complexchildrentype>(
                    complexChildrenTypes?.Select(x => x.Clone()).ToList() ?? new List<Complexchildrentype>()),
                simpleChildrenUseDefaultAttributes = simpleChildrenUseDefaultAttributes,
                simpleChildrenTypes = new BindingList<string>(simpleChildrenTypes?.ToList() ?? new List<string>())
            };
        }

        public bool Equals(Discreteunsorteditemset? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return name == other.name &&
                   spawnWeight == other.spawnWeight &&
                   Equals(attributes, other.attributes) &&
                   simpleChildrenUseDefaultAttributes == other.simpleChildrenUseDefaultAttributes &&
                   ListEqual(complexChildrenTypes, other.complexChildrenTypes) &&
                   ListEqual(simpleChildrenTypes, other.simpleChildrenTypes);
        }

        public override bool Equals(object? obj) => Equals(obj as Discreteunsorteditemset);

        private static bool ListEqual<T>(IList<T>? a, IList<T>? b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            if (a.Count != b.Count) return false;

            for (int i = 0; i < a.Count; i++)
            {
                if (!Equals(a[i], b[i]))
                    return false;
            }

            return true;
        }
    }
}