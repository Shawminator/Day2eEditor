using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExpansionPlugin
{
    public class ExpansionLoadoutConfig : IConfigLoader
    {
        private readonly string _basepath;
        public List<AILoadouts> AllData { get; private set; } = new List<AILoadouts>();
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();

        public ExpansionLoadoutConfig(string path)
        {
            _basepath = path;
        }
        public void Load()
        {
            HasErrors = false;
            Errors.Clear();
            string[] PresetPaths = Directory.GetFiles(_basepath, "*.json");
            foreach (string filename in PresetPaths)
            {
                var preset = AppServices.GetRequired<FileService>().LoadOrCreateJson<AILoadouts>(
                     filename,
                     createNew: () => new AILoadouts(),
                     onAfterLoad: cfg => { },
                     onError: ex =>
                     {
                         HasErrors = true;
                         Console.WriteLine(
                             "Error in " + Path.GetFileName(filename) + "\n" +
                             ex.Message + "\n" +
                             ex.InnerException?.Message + "\n"
                         );
                         Errors.Add("Error in " + Path.GetFileName(filename) + "\n" +
                             ex.Message + "\n" +
                             ex.InnerException?.Message);
                     },
                     configName: "Loadouts",
                     useBoolConvertor: false
                 );
                preset.setpath(filename);
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
        public bool needToSave()
        {
            return false;
        }
    }
    public class AILoadouts
    {
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

        public string ClassName { get; set; }
        public decimal Chance { get; set; }
        public Quantity Quantity { get; set; }
        public BindingList<Health> Health { get; set; }
        public BindingList<Inventoryattachment> InventoryAttachments { get; set; }
        public BindingList<AILoadouts> InventoryCargo { get; set; }
        public BindingList<object> ConstructionPartsBuilt { get; set; }
        public BindingList<AILoadouts> Sets { get; set; }

        public void setpath(string path)
        {
            _path = path;
        }
        public AILoadouts()
        {
            ClassName = "";
            Chance = (decimal)1;
            Quantity = new Quantity();
            Health = new BindingList<Health>();
            InventoryAttachments = new BindingList<Inventoryattachment>();
            InventoryCargo = new BindingList<AILoadouts>();
            ConstructionPartsBuilt = new BindingList<object>();
            Sets = new BindingList<AILoadouts>();
        }
        public AILoadouts(string name)
        {
            name = name;
            ClassName = "";
            Chance = (decimal)1;
            Quantity = new Quantity();
            Health = new BindingList<Health>();
            InventoryAttachments = new BindingList<Inventoryattachment>();
            InventoryCargo = new BindingList<AILoadouts>();
            ConstructionPartsBuilt = new BindingList<object>();
            Sets = new BindingList<AILoadouts>();
        }

        internal IEnumerable<string> Save()
        {
            if (ToDelete)
            {
                if (File.Exists(_path))
                {
                    File.Delete(_path);
                    // Delete empty directories if needed
                    //ShellHelper.DeleteEmptyFoldersUpToBase(Path.GetDirectoryName(_path), AppServices.GetRequired<EconomyManager>().basePath);
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

        public override string ToString()
        {
            if (ClassName == "")
                return FileName;

            return ClassName;
        }
    }

    public class Quantity
    {
        public decimal Min { get; set; }
        public decimal Max { get; set; }
    }

    public class Health
    {
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public string Zone { get; set; }

        public override string ToString()
        {
            if (Zone == "")
                return "No Zone";
            return Zone;
        }

    }

    public class Inventoryattachment
    {
        public string SlotName { get; set; }
        public BindingList<AILoadouts> Items { get; set; }

        public override string ToString()
        {
            return SlotName; ;
        }
    }
}
