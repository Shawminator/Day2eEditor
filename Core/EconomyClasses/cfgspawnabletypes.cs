using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Day2eEditor
{
    public class CfgSpawnableTypesConfig : ParameterizedMultiFileConfigLoaderBase<CfgSpawnableTypesFile>
    {
        public CfgSpawnableTypesConfig(string path) : base(path)
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
                vanilla.FileType = "cfgspawnabletypes";

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
                    item.FileType = "cfgspawnabletypes";
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
        protected override CfgSpawnableTypesFile LoadItem(string filePath)
        {
            var item = new CfgSpawnableTypesFile(filePath);

            item.Data = AppServices.GetRequired<FileService>().LoadOrCreateXml(
                filePath,
                createNew: () => new SpawnableTypes
                {
                    type = new BindingList<SpawnableType>()
                },
                onError: ex =>
                {
                    item.HasErrors = true;

                    var message =
                        $"Error in {Path.GetFileName(filePath)}\n{ex.Message}\n{ex.InnerException?.Message}";

                    Console.WriteLine(message + "\n");
                    item.Errors.Add(message);
                },
                configName: "cfgspawnabletypes"
            );

            item.Data.type ??= new BindingList<SpawnableType>();
            item.SetPath(filePath);
            item.SetGuid(Guid.NewGuid());

            return item;
        }
        protected override IEnumerable<string> ValidateItem(CfgSpawnableTypesFile item)
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
        protected override void SaveItem(CfgSpawnableTypesFile item)
        {
            AppServices.GetRequired<FileService>().SaveXml(item.FilePath, item.Data);
            item.IsDirty = false;
        }
        protected override string GetItemFileName(CfgSpawnableTypesFile item)
            => item.FileName;
        protected override Guid GetID(CfgSpawnableTypesFile item)
            => item.Id;
        protected override bool ShouldDelete(CfgSpawnableTypesFile item)
            => item.ToDelete;
        protected override void DeleteItemFile(CfgSpawnableTypesFile item)
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
    public class CfgSpawnableTypesFile : IDeepCloneable<CfgSpawnableTypesFile>, IEquatable<CfgSpawnableTypesFile>
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

        public SpawnableTypes Data { get; set; } = new()
        {
            type = new BindingList<SpawnableType>()
        };

        public CfgSpawnableTypesFile(string path)
        {
            _path = path;
        }

        public void SetPath(string path) => _path = path;

        internal void SetGuid(Guid guid) => Id = guid;

        public CfgSpawnableTypesFile Clone()
        {
            var clone = new CfgSpawnableTypesFile(_path)
            {
                ToDelete = ToDelete,
                IsDirty = IsDirty,
                HasErrors = HasErrors,
                Id = Id,
                FileType = FileType,
                IsModded = IsModded,
                ModFolder = ModFolder,
                Data = Data?.Clone() ?? new SpawnableTypes
                {
                    type = new BindingList<SpawnableType>()
                }
            };

            clone.Errors.AddRange(Errors);
            return clone;
        }

        public bool Equals(CfgSpawnableTypesFile? other)
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
            return Equals(obj as CfgSpawnableTypesFile);
        }
    }

    [XmlRoot("spawnabletypes")]
    public partial class SpawnableTypes : IDeepCloneable<SpawnableTypes>, IEquatable<SpawnableTypes>
    {
        private spawnableTypeDamage? damageField;
        private BindingList<SpawnableType>? typeField = new();

        [XmlElement("damage")]
        public spawnableTypeDamage? damage
        {
            get => damageField;
            set => damageField = value;
        }

        [XmlElement("type")]
        public BindingList<SpawnableType>? type
        {
            get => typeField;
            set => typeField = value;
        }
        public bool Equals(SpawnableTypes? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
                Equals(damage, other.damage) &&
                ListsEqual(type, other.type);
        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as SpawnableTypes);
        }
        public SpawnableTypes Clone()
        {
            return new SpawnableTypes
            {
                damage = damage?.Clone(),
                type = new BindingList<SpawnableType>(
                    type?.Select(x => x.Clone()).ToList() ?? new List<SpawnableType>())
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
    public partial class SpawnableType : IDeepCloneable<SpawnableType>, IEquatable<SpawnableType>
    {
        private BindingList<object>? itemsField = new();
        private string? nameField;

        [XmlElement("attachments", typeof(spawnableTypeAttachment))]
        [XmlElement("cargo", typeof(spawnableTypeCargo))]
        [XmlElement("damage", typeof(spawnableTypeDamage))]
        [XmlElement("hoarder", typeof(spawnableTypesHoarder))]
        [XmlElement("tag", typeof(spawnableTypeTag))]
        public BindingList<object>? Items
        {
            get => itemsField;
            set => itemsField = value;
        }

        [XmlAttribute]
        public string? name
        {
            get => nameField;
            set => nameField = value;
        }
        public override string ToString()
        {
            return name ?? string.Empty;
        }
        public bool ContainsAttchorcargo()
        {
            if (Items == null)
                return false;

            foreach (var item in Items)
            {
                if (item is spawnableTypeAttachment att)
                {
                    if ((att.item?.Count ?? 0) > 0 && att.preset == null)
                        return true;
                }

                if (item is spawnableTypeCargo cargo)
                {
                    if ((cargo.item?.Count ?? 0) > 0 && cargo.preset == null)
                        return true;
                }
            }

            return false;
        }
        public bool Equals(SpawnableType? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
                string.Equals(name, other.name, StringComparison.Ordinal) &&
                ListsEqual(Items, other.Items);
        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as SpawnableType);
        }
        public SpawnableType Clone()
        {
            var clonedItems = new BindingList<object>();

            if (Items != null)
            {
                foreach (var item in Items)
                {
                    switch (item)
                    {
                        case spawnableTypeAttachment attachment:
                            clonedItems.Add(attachment.Clone());
                            break;
                        case spawnableTypeCargo cargo:
                            clonedItems.Add(cargo.Clone());
                            break;
                        case spawnableTypeDamage damage:
                            clonedItems.Add(damage.Clone());
                            break;
                        case spawnableTypesHoarder hoarder:
                            clonedItems.Add(hoarder.Clone());
                            break;
                        case spawnableTypeTag tag:
                            clonedItems.Add(tag.Clone());
                            break;
                    }
                }
            }

            return new SpawnableType
            {
                name = name,
                Items = clonedItems
            };
        }
        private static bool ListsEqual(IList<object>? a, IList<object>? b)
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
    public partial class spawnableTypeTag : IDeepCloneable<spawnableTypeTag>, IEquatable<spawnableTypeTag>
    {
        private string? nameField;

        [XmlAttribute]
        public string? name
        {
            get => nameField;
            set => nameField = value;
        }

        public override string ToString()
        {
            return "Tag";
        }
        public bool Equals(spawnableTypeTag? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return string.Equals(name, other.name, StringComparison.Ordinal);
        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as spawnableTypeTag);
        }
        public spawnableTypeTag Clone()
        {
            return new spawnableTypeTag
            {
                name = name
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class spawnableTypeDamage : IDeepCloneable<spawnableTypeDamage>, IEquatable<spawnableTypeDamage>
    {
        private decimal minField;
        private decimal maxField;

        [XmlAttribute]
        public decimal min
        {
            get => minField;
            set => minField = value;
        }

        [XmlAttribute]
        public decimal max
        {
            get => maxField;
            set => maxField = value;
        }

        public override string ToString()
        {
            return "Damage";
        }
        public bool Equals(spawnableTypeDamage? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return min == other.min && max == other.max;
        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as spawnableTypeDamage);
        }
        public spawnableTypeDamage Clone()
        {
            return new spawnableTypeDamage
            {
                min = min,
                max = max
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class spawnableTypesHoarder : IDeepCloneable<spawnableTypesHoarder>, IEquatable<spawnableTypesHoarder>
    {
        public override string ToString()
        {
            return "Hoarder";
        }
        public bool Equals(spawnableTypesHoarder? other)
        {
            return other is not null;
        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as spawnableTypesHoarder);
        }
        public spawnableTypesHoarder Clone()
        {
            return new spawnableTypesHoarder();
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class spawnableTypeAttachment : IDeepCloneable<spawnableTypeAttachment>, IEquatable<spawnableTypeAttachment>
    {
        private spawnableTypeDamage? damageField;
        private BindingList<spawnableTypeItem>? itemField = new();
        private decimal chanceField;
        private bool chanceFieldSpecified;
        private string? presetField;

        [XmlElement("damage")]
        public spawnableTypeDamage? damage
        {
            get => damageField;
            set => damageField = value;
        }

        [XmlElement("item")]
        public BindingList<spawnableTypeItem>? item
        {
            get => itemField;
            set => itemField = value;
        }

        [XmlAttribute]
        public decimal chance
        {
            get => chanceField;
            set => chanceField = value;
        }

        [XmlIgnore]
        public bool chanceSpecified
        {
            get => chanceFieldSpecified;
            set => chanceFieldSpecified = value;
        }

        [XmlAttribute]
        public string? preset
        {
            get => presetField;
            set => presetField = value;
        }

        public override string ToString()
        {
            return "Attachments";
        }

        public bool Equals(spawnableTypeAttachment? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
                Equals(damage, other.damage) &&
                ListsEqual(item, other.item) &&
                chance == other.chance &&
                chanceSpecified == other.chanceSpecified &&
                string.Equals(preset, other.preset, StringComparison.Ordinal);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as spawnableTypeAttachment);
        }

        public spawnableTypeAttachment Clone()
        {
            return new spawnableTypeAttachment
            {
                damage = damage?.Clone(),
                item = new BindingList<spawnableTypeItem>(
                    item?.Select(x => x.Clone()).ToList() ?? new List<spawnableTypeItem>()),
                chance = chance,
                chanceSpecified = chanceSpecified,
                preset = preset
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
    public partial class spawnableTypeCargo : IDeepCloneable<spawnableTypeCargo>, IEquatable<spawnableTypeCargo>
    {
        private spawnableTypeDamage? damageField;
        private BindingList<spawnableTypeItem>? itemField = new();
        private string? presetField;
        private decimal chanceField;
        private bool chanceFieldSpecified;

        [XmlElement("damage")]
        public spawnableTypeDamage? damage
        {
            get => damageField;
            set => damageField = value;
        }

        [XmlElement("item")]
        public BindingList<spawnableTypeItem>? item
        {
            get => itemField;
            set => itemField = value;
        }

        [XmlAttribute]
        public string? preset
        {
            get => presetField;
            set => presetField = value;
        }

        [XmlAttribute]
        public decimal chance
        {
            get => chanceField;
            set => chanceField = value;
        }

        [XmlIgnore]
        public bool chanceSpecified
        {
            get => chanceFieldSpecified;
            set => chanceFieldSpecified = value;
        }

        public override string ToString()
        {
            return "Cargo";
        }
        public bool Equals(spawnableTypeCargo? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
                Equals(damage, other.damage) &&
                ListsEqual(item, other.item) &&
                string.Equals(preset, other.preset, StringComparison.Ordinal) &&
                chance == other.chance &&
                chanceSpecified == other.chanceSpecified;
        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as spawnableTypeCargo);
        }
        public spawnableTypeCargo Clone()
        {
            return new spawnableTypeCargo
            {
                damage = damage?.Clone(),
                item = new BindingList<spawnableTypeItem>(
                    item?.Select(x => x.Clone()).ToList() ?? new List<spawnableTypeItem>()),
                preset = preset,
                chance = chance,
                chanceSpecified = chanceSpecified
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

    public partial class spawnableTypeItem : IDeepCloneable<spawnableTypeItem>, IEquatable<spawnableTypeItem>
    {
        private spawnableTypeDamage? damageField;
        private BindingList<spawnableTypeAttachment>? attachmentsField = new();
        private BindingList<spawnableTypeCargo>? cargoField = new();
        private string? nameField;
        private bool equipField;
        private bool equipFieldSpecified;
        private decimal chanceField;
        private bool chanceFieldSpecified;
        private int quantminField;
        private bool quantminFieldSpecified;
        private int quantmaxField;
        private bool quantmaxFieldSpecified;

        [XmlElement("damage")]
        public spawnableTypeDamage? damage
        {
            get => damageField;
            set => damageField = value;
        }

        [XmlElement("attachments")]
        public BindingList<spawnableTypeAttachment>? attachments
        {
            get => attachmentsField;
            set => attachmentsField = value;
        }

        [XmlElement("cargo")]
        public BindingList<spawnableTypeCargo>? cargo
        {
            get => cargoField;
            set => cargoField = value;
        }

        [XmlAttribute]
        public string? name
        {
            get => nameField;
            set => nameField = value;
        }

        [XmlAttribute]
        public bool equip
        {
            get => equipField;
            set => equipField = value;
        }

        [XmlIgnore]
        public bool equipSpecified
        {
            get => equipFieldSpecified;
            set => equipFieldSpecified = value;
        }

        [XmlAttribute]
        public decimal chance
        {
            get => chanceField;
            set => chanceField = value;
        }

        [XmlIgnore]
        public bool chanceSpecified
        {
            get => chanceFieldSpecified;
            set => chanceFieldSpecified = value;
        }

        [XmlAttribute]
        public int quantmin
        {
            get => quantminField;
            set => quantminField = value;
        }

        [XmlIgnore]
        public bool quantminSpecified
        {
            get => quantminFieldSpecified;
            set => quantminFieldSpecified = value;
        }

        [XmlAttribute]
        public int quantmax
        {
            get => quantmaxField;
            set => quantmaxField = value;
        }

        [XmlIgnore]
        public bool quantmaxSpecified
        {
            get => quantmaxFieldSpecified;
            set => quantmaxFieldSpecified = value;
        }

        public bool Equals(spawnableTypeItem? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
                Equals(damage, other.damage) &&
                ListsEqual(attachments, other.attachments) &&
                ListsEqual(cargo, other.cargo) &&
                string.Equals(name, other.name, StringComparison.Ordinal) &&
                equip == other.equip &&
                equipSpecified == other.equipSpecified &&
                chance == other.chance &&
                chanceSpecified == other.chanceSpecified &&
                quantmin == other.quantmin &&
                quantminSpecified == other.quantminSpecified &&
                quantmax == other.quantmax &&
                quantmaxSpecified == other.quantmaxSpecified;
        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as spawnableTypeItem);
        }
        public spawnableTypeItem Clone()
        {
            return new spawnableTypeItem
            {
                damage = damage?.Clone(),
                attachments = new BindingList<spawnableTypeAttachment>(
                    attachments?.Select(x => x.Clone()).ToList() ?? new List<spawnableTypeAttachment>()),
                cargo = new BindingList<spawnableTypeCargo>(
                    cargo?.Select(x => x.Clone()).ToList() ?? new List<spawnableTypeCargo>()),
                name = name,
                equip = equip,
                equipSpecified = equipSpecified,
                chance = chance,
                chanceSpecified = chanceSpecified,
                quantmin = quantmin,
                quantminSpecified = quantminSpecified,
                quantmax = quantmax,
                quantmaxSpecified = quantmaxSpecified
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
}
