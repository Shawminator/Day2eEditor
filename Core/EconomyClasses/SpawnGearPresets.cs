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
    public class SpawnGearPresetFiles
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

    public class Discreteitemset
    {
        public string itemType { get; set; }
        public int spawnWeight { get; set; }
        public Attributes attributes { get; set; }
        public int quickBarSlot { get; set; }
        public BindingList<Complexchildrentype> complexChildrenTypes { get; set; }
        public bool simpleChildrenUseDefaultAttributes { get; set; }
        public BindingList<string> simpleChildrenTypes { get; set; }

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

    public class Complexchildrentype
    {
        public string itemType { get; set; }
        public Attributes attributes { get; set; }
        public int quickBarSlot { get; set; }
        public bool simpleChildrenUseDefaultAttributes { get; set; }
        public BindingList<string> simpleChildrenTypes { get; set; }

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

    public class Discreteunsorteditemset
    {
        public string name { get; set; }
        public int spawnWeight { get; set; }
        public Attributes attributes { get; set; }
        public BindingList<Complexchildrentype> complexChildrenTypes { get; set; }
        public bool simpleChildrenUseDefaultAttributes { get; set; }
        public BindingList<string> simpleChildrenTypes { get; set; }

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
}
