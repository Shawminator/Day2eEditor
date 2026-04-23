using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Day2eEditor
{
    public class ObjectSpawnerArrConfig : MultiFileConfigLoaderBase<ObjectSpawnerArrFile>
    {
        public ObjectSpawnerArrConfig(string basePath) : base(basePath)
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

        protected override ObjectSpawnerArrFile LoadItem(string filePath)
        {
            var data = AppServices.GetRequired<FileService>().LoadOrCreateJson<ObjectSpawnerArrData>(
                filePath,
                createNew: () => new ObjectSpawnerArrData(),
                configName: "ObjectSpawnerArr",
                useBoolConvertor: false
            );

            return new ObjectSpawnerArrFile(filePath)
            {
                Data = data,
                ModFolder = Path.GetRelativePath(BasePath, Path.GetDirectoryName(filePath) ?? BasePath)
            };
        }
        protected override void SaveItem(ObjectSpawnerArrFile item)
        {
            AppServices.GetRequired<FileService>().SaveJson(item.FilePath, item.Data);
            item.IsDirty = false;
        }

        protected override string GetItemFileName(ObjectSpawnerArrFile item) => item.FileName;
        protected override string GetItemFilePath(ObjectSpawnerArrFile ObjectSpawnerArrFile)
            => ObjectSpawnerArrFile.FilePath;
        protected override Guid GetID(ObjectSpawnerArrFile item) => item.Id;

        protected override bool ShouldDelete(ObjectSpawnerArrFile item) => item.ToDelete;

        protected override void DeleteItemFile(ObjectSpawnerArrFile item)
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
    public class ObjectSpawnerArrFile : IDeepCloneable<ObjectSpawnerArrFile>, IEquatable<ObjectSpawnerArrFile>
    {
        private string _path;

        public string FileName => Path.GetFileName(_path);
        public string FilePath => _path;

        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsDirty { get; set; }
        public bool ToDelete { get; set; }
        public string ModFolder { get; set; } = string.Empty;

        public ObjectSpawnerArrData Data { get; set; } = new();

        public ObjectSpawnerArrFile(string path)
        {
            _path = path;
        }

        public void SetPath(string path) => _path = path;

        public ObjectSpawnerArrFile Clone()
        {
            return new ObjectSpawnerArrFile(_path)
            {
                Id = Id,
                IsDirty = IsDirty,
                ToDelete = ToDelete,
                ModFolder = ModFolder,
                Data = Data.Clone()
            };
        }

        public bool Equals(ObjectSpawnerArrFile? other)
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

        public override bool Equals(object? obj) => Equals(obj as ObjectSpawnerArrFile);
    }
    public class ObjectSpawnerArrData : IDeepCloneable<ObjectSpawnerArrData>, IEquatable<ObjectSpawnerArrData>
    {
        public BindingList<SpawnObjects> Objects { get; set; } = new();

        public ObjectSpawnerArrData Clone()
        {
            return new ObjectSpawnerArrData
            {
                Objects = new BindingList<SpawnObjects>(Objects.Select(x => x.Clone()).ToList())
            };
        }

        public bool Equals(ObjectSpawnerArrData? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Objects.SequenceEqual(other.Objects);
        }

        public override bool Equals(object? obj) => Equals(obj as ObjectSpawnerArrData);
    }

    public class SpawnObjects : IDeepCloneable<SpawnObjects>, IEquatable<SpawnObjects>
    {
        public string name { get; set; } = string.Empty;
        public float[] pos { get; set; } = Array.Empty<float>();
        public float[] ypr { get; set; } = Array.Empty<float>();
        public float scale { get; set; }
        public bool enableCEPersistency { get; set; }

        public SpawnObjects()
        {
        }

        public SpawnObjects(SpawnObjects other)
        {
            name = other.name;
            pos = (float[])other.pos.Clone();
            ypr = (float[])other.ypr.Clone();
            scale = other.scale;
            enableCEPersistency = other.enableCEPersistency;
        }

        public SpawnObjects Clone()
        {
            return new SpawnObjects(this);
        }

        public bool Equals(SpawnObjects? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                name == other.name &&
                pos.SequenceEqual(other.pos) &&
                ypr.SequenceEqual(other.ypr) &&
                scale == other.scale &&
                enableCEPersistency == other.enableCEPersistency;
        }

        public override bool Equals(object? obj) => Equals(obj as SpawnObjects);
    }
}
