using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Day2eEditor
{
    public class EconomyConfig : ParameterizedMultiFileConfigLoaderBase<EconomyFile>
    {
        public EconomyConfig(string path) : base(path)
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
                vanilla.FileType = "economy";

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
                    item.FileType = "economy";
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

        protected override EconomyFile LoadItem(string filePath)
        {
            var item = new EconomyFile(filePath);

            item.Data = AppServices.GetRequired<FileService>().LoadOrCreateXml(
                filePath,
                createNew: () => new economy(),
                onError: ex =>
                {
                    item.HasErrors = true;

                    var message =
                        $"Error in {Path.GetFileName(filePath)}\n{ex.Message}\n{ex.InnerException?.Message}";

                    Console.WriteLine(message + "\n");
                    item.Errors.Add(message);
                },
                configName: "economy"
            );

            item.SetPath(filePath);
            item.SetGuid(Guid.NewGuid());

            return item;
        }

        protected override IEnumerable<string> ValidateItem(EconomyFile item)
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

        protected override void SaveItem(EconomyFile item)
        {
            AppServices.GetRequired<FileService>().SaveXml(item.FilePath, item.Data);
            item.IsDirty = false;
        }

        protected override string GetItemFileName(EconomyFile item)
            => item.FileName;

        protected override Guid GetID(EconomyFile item)
            => item.Id;

        protected override bool ShouldDelete(EconomyFile item)
            => item.ToDelete;

        protected override void DeleteItemFile(EconomyFile item)
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
    public class EconomyFile : IDeepCloneable<EconomyFile>, IEquatable<EconomyFile>
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

        public economy Data { get; set; } = new();

        public EconomyFile(string path)
        {
            _path = path;
        }
        public void SetPath(string path) => _path = path;
        internal void SetGuid(Guid guid) => Id = guid;
        public EconomyFile Clone()
        {
            var clone = new EconomyFile(_path)
            {
                ToDelete = ToDelete,
                IsDirty = IsDirty,
                HasErrors = HasErrors,
                Id = Id,
                FileType = FileType,
                IsModded = IsModded,
                ModFolder = ModFolder,
                Data = Data?.Clone() ?? new economy()
            };

            clone.Errors.AddRange(Errors);
            return clone;
        }
        public bool Equals(EconomyFile? other)
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
            return Equals(obj as EconomyFile);
        }
    }


    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class economy : IDeepCloneable<economy>, IEquatable<economy>
    {
        private EconomySection? dynamicField;
        private EconomySection? animalsField;
        private EconomySection? zombiesField;
        private EconomySection? vehiclesField;
        private EconomySection? randomsField;
        private EconomySection? customField;
        private EconomySection? buildingField;
        private EconomySection? playerField;

        public EconomySection? dynamic
        {
            get => dynamicField;
            set => dynamicField = value;
        }
        public EconomySection? animals
        {
            get => animalsField;
            set => animalsField = value;
        }
        public EconomySection? zombies
        {
            get => zombiesField;
            set => zombiesField = value;
        }
        public EconomySection? vehicles
        {
            get => vehiclesField;
            set => vehiclesField = value;
        }
        public EconomySection? randoms
        {
            get => randomsField;
            set => randomsField = value;
        }
        public EconomySection? custom
        {
            get => customField;
            set => customField = value;
        }
        public EconomySection? building
        {
            get => buildingField;
            set => buildingField = value;
        }
        public EconomySection? player
        {
            get => playerField;
            set => playerField = value;
        }

        public economy()
        {
        }

        public bool Equals(economy? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
                Equals(dynamic, other.dynamic) &&
                Equals(animals, other.animals) &&
                Equals(zombies, other.zombies) &&
                Equals(vehicles, other.vehicles) &&
                Equals(randoms, other.randoms) &&
                Equals(custom, other.custom) &&
                Equals(building, other.building) &&
                Equals(player, other.player);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as economy);
        }

        public economy Clone()
        {
            return new economy
            {
                dynamic = dynamic?.Clone(),
                animals = animals?.Clone(),
                zombies = zombies?.Clone(),
                vehicles = vehicles?.Clone(),
                randoms = randoms?.Clone(),
                custom = custom?.Clone(),
                building = building?.Clone(),
                player = player?.Clone()
            };
        }
    }

    [Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class EconomySection : IDeepCloneable<EconomySection>, IEquatable<EconomySection>
    {
        private int initField;
        private int loadField;
        private int respawnField;
        private int saveField;

        [XmlAttribute]
        public int init
        {
            get => initField;
            set => initField = value;
        }

        [XmlAttribute]
        public int load
        {
            get => loadField;
            set => loadField = value;
        }

        [XmlAttribute]
        public int respawn
        {
            get => respawnField;
            set => respawnField = value;
        }

        [XmlAttribute]
        public int save
        {
            get => saveField;
            set => saveField = value;
        }

        public bool Equals(EconomySection? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
                init == other.init &&
                load == other.load &&
                respawn == other.respawn &&
                save == other.save;
        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as EconomySection);
        }
        public EconomySection Clone()
        {
            return new EconomySection
            {
                init = init,
                load = load,
                respawn = respawn,
                save = save
            };
        }
    }
}
