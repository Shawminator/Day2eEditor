using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Day2eEditor
{
    public class SpawnGearPresetsConfig
    {
        public string _basepath { get; set; }
        public List<SpawnGearPresetFiles> AllData { get; private set; } = new List<SpawnGearPresetFiles>();
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();

        public void Load(string basePath, BindingList<string> PresetPaths)
        {
            _basepath = basePath;
            HasErrors = false;
            Errors.Clear();

            foreach (string filename in PresetPaths)
            {
                var fullPath = Path.Combine(_basepath, filename.Replace('/', Path.DirectorySeparatorChar));
                var relativepath = Path.GetRelativePath(_basepath, fullPath).Replace(Path.DirectorySeparatorChar, '/');

                var preset = AppServices.GetRequired<FileService>().LoadOrCreateJson<SpawnGearPresetFiles>(
                     fullPath,
                     createNew: () => new SpawnGearPresetFiles(),
                     configName: "SpawnGearPreset",
                     useBoolConvertor: false
                 );
                preset.setpath(fullPath);
                preset.ModFolder = Path.GetRelativePath(_basepath, Path.GetDirectoryName(fullPath));
                AllData.Add(preset);    
            }
        }
        public IEnumerable<string> Save()
        {
            var savedFiles = new List<string>();

            foreach (var data in AllData.ToList())
            {
                var result = data.Save();
                savedFiles.AddRange(result);

                if (data.ToDelete)
                {
                    AllData.Remove(data); // cleanup after deleting
                }
            }

            return savedFiles;
        }
    }
    public class SpawnGearPresetFiles : IHasSpawnWeight, IHasSpawnName
    {
        public string name { get; set; }
        public int spawnWeight { get; set; }
        public BindingList<string> characterTypes { get; set; }
        public BindingList<Attachmentslotitemset> attachmentSlotItemSets { get; set; }
        public BindingList<Discreteunsorteditemset> discreteUnsortedItemSets { get; set; }

        [JsonIgnore]
        private string _path;
        [JsonIgnore]
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        [JsonIgnore]
        public string FilePath => _path;
        [JsonIgnore]
        public bool isDirty { get; set; }
        [JsonIgnore]
        public bool ToDelete { get; set; }
        [JsonIgnore]
        public string ModFolder { get; set; }
        [JsonIgnore]
        public int SpawnWeight { get => spawnWeight; set => spawnWeight = value; }
        [JsonIgnore]
        public string Name { get => name; set => name = value; }

        public void setpath(string path)
        {
            _path = path;
        }
        public SpawnGearPresetFiles()
        {
            spawnWeight = 1;
            name = "";
            characterTypes = new BindingList<string>();
            attachmentSlotItemSets = new BindingList<Attachmentslotitemset>();
            discreteUnsortedItemSets = new BindingList<Discreteunsorteditemset>();
        }
        public override string ToString()
        {
            return name;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not SpawnGearPresetFiles other)
                return false;

            return spawnWeight == other.spawnWeight &&
                   name == other.name &&
                   characterTypes.SequenceEqual(other.characterTypes);
        }

        internal IEnumerable<string> Save()
        {
            if (ToDelete)
            {
                if (File.Exists(_path))
                {
                    File.Delete(_path);
                    // Delete empty directories if needed
                    ShellHelper.DeleteEmptyFoldersUpToBase(Path.GetDirectoryName(_path), AppServices.GetRequired<EconomyManager>().basePath);
                    return new[] { FileName + " (deleted)" };
                }
                return Array.Empty<string>();
            }

            else if (isDirty)
            {
                AppServices.GetRequired<FileService>().SaveJson(_path, this);
                isDirty = false;
                return new[] { FileName };
            }

            return Array.Empty<string>();
        }

    }

    public class Attachmentslotitemset
    {
        public string slotName { get; set; }
        public BindingList<Discreteitemset> discreteItemSets { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is not Attachmentslotitemset other)
                return false;

            return slotName == other.slotName;
        }


    }

    public class Discreteitemset : IHasSpawnWeight, IHasSpawnItemType, IHasSimpleChildren, IHasQuikBarSlot, IHassimpleChildrenUseDefaultAttributes
    {
        public string itemType { get; set; }
        public int spawnWeight { get; set; }
        public Attributes attributes { get; set; }
        public int quickBarSlot { get; set; }
        public BindingList<Complexchildrentype> complexChildrenTypes { get; set; }
        public bool simpleChildrenUseDefaultAttributes { get; set; }
        public BindingList<string> simpleChildrenTypes { get; set; }

        [JsonIgnore]
        public string ItemType { get => itemType; set => itemType = value; }
        [JsonIgnore]
        public int SpawnWeight { get => spawnWeight; set => spawnWeight = value; }
        [JsonIgnore]
        public int QuickBarSlot { get => quickBarSlot; set => quickBarSlot = value; }
        [JsonIgnore]
        public bool SimpleChildrenUseDefaultAttributes { get => simpleChildrenUseDefaultAttributes; set => simpleChildrenUseDefaultAttributes = value; }
        [JsonIgnore]
        public BindingList<string> SimpleChildrenTypes { get => simpleChildrenTypes; set => simpleChildrenTypes = value; }

        public Discreteitemset()
        {
            attributes = new Attributes();
            complexChildrenTypes = new BindingList<Complexchildrentype>();
            simpleChildrenTypes = new BindingList<string>();
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Discreteitemset other)
                return false;

            return itemType == other.itemType &&
                   spawnWeight == other.spawnWeight &&
                   quickBarSlot == other.quickBarSlot &&
                   simpleChildrenUseDefaultAttributes == other.simpleChildrenUseDefaultAttributes;
        }

    }

    public class Attributes
    {
        public decimal healthMin { get; set; }
        public decimal healthMax { get; set; }
        public decimal quantityMin { get; set; }
        public decimal quantityMax { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is not Attributes other)
                return false;

            return healthMin == other.healthMin &&
                   healthMax == other.healthMax &&
                   quantityMin == other.quantityMin &&
                   quantityMax == other.quantityMax;
        }

    }

    public class Complexchildrentype : IHasSpawnItemType, IHasSimpleChildren, IHasQuikBarSlot, IHassimpleChildrenUseDefaultAttributes
    {
        public string itemType { get; set; }
        public Attributes attributes { get; set; }
        public int quickBarSlot { get; set; }
        public bool simpleChildrenUseDefaultAttributes { get; set; }
        public BindingList<string> simpleChildrenTypes { get; set; }

        [JsonIgnore]
        public string ItemType { get => itemType; set => itemType = value; }
        [JsonIgnore]
        public int QuickBarSlot { get => quickBarSlot; set => quickBarSlot = value; }
        [JsonIgnore]
        public bool SimpleChildrenUseDefaultAttributes { get => simpleChildrenUseDefaultAttributes; set => simpleChildrenUseDefaultAttributes = value; }
        [JsonIgnore]
        public BindingList<string> SimpleChildrenTypes { get => simpleChildrenTypes; set => simpleChildrenTypes = value; }

        public Complexchildrentype()
        {
            attributes = new Attributes();
            simpleChildrenTypes = new BindingList<string>();
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Complexchildrentype other)
                return false;

            return itemType == other.itemType &&
                   quickBarSlot == other.quickBarSlot &&
                   simpleChildrenUseDefaultAttributes == other.simpleChildrenUseDefaultAttributes;
        }


    }

    public class Discreteunsorteditemset : IHasSpawnWeight, IHasSpawnName, IHasSimpleChildren, IHassimpleChildrenUseDefaultAttributes
    {
        public string name { get; set; }
        public int spawnWeight { get; set; }
        public Attributes attributes { get; set; }
        public BindingList<Complexchildrentype> complexChildrenTypes { get; set; }
        public bool simpleChildrenUseDefaultAttributes { get; set; }
        public BindingList<string> simpleChildrenTypes { get; set; }

        [JsonIgnore]
        public int SpawnWeight { get => spawnWeight; set => spawnWeight = value; }
        [JsonIgnore]
        public string Name { get => name; set => name = value; }
        [JsonIgnore]
        public bool SimpleChildrenUseDefaultAttributes { get => simpleChildrenUseDefaultAttributes; set => simpleChildrenUseDefaultAttributes = value; }
        [JsonIgnore]
        public BindingList<string> SimpleChildrenTypes { get => simpleChildrenTypes; set => simpleChildrenTypes = value; }

        public Discreteunsorteditemset()
        {
            attributes = new Attributes();
            complexChildrenTypes = new BindingList<Complexchildrentype>();
            simpleChildrenTypes = new BindingList<string>();
        }
        public override bool Equals(object? obj)
        {
            if (obj is not Discreteunsorteditemset other)
                return false;

            return name == other.name &&
                   spawnWeight == other.spawnWeight &&
                   simpleChildrenUseDefaultAttributes == other.simpleChildrenUseDefaultAttributes;
        }
    }

    public interface IHasSpawnWeight
    {
        int SpawnWeight { get; set; }
    }
    public interface IHasSpawnItemType
    {
        string ItemType { get; set; }
    }
    public interface IHasSpawnName
    {
        string Name { get; set; }
    }
    public interface IHasQuikBarSlot
    {
        int QuickBarSlot { get; set; }
    }
    public interface IHasSimpleChildren
    {
        BindingList<string> SimpleChildrenTypes { get; set; }
    }
    public interface IHassimpleChildrenUseDefaultAttributes
    {
        bool SimpleChildrenUseDefaultAttributes { get; set; }
    }
}
