using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionLootDropConfig : IConfigLoader
    {
        public string FileName => Path.GetFileName(_basepath); // e.g., "types.xml"
        public string FilePath => _basepath;
        private readonly string _basepath;
        public List<AILootDrops> AllData { get; private set; } = new List<AILootDrops>();
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();

        public ExpansionLootDropConfig(string path)
        {
            _basepath = path;
        }
        public void Load()
        {
            HasErrors = false;
            Errors.Clear();
            if(!Directory.Exists(_basepath))
                Directory.CreateDirectory(_basepath);

            string[] PresetPaths = Directory.GetFiles(_basepath, "*.json");
            foreach (string filename in PresetPaths)
            {
                var preset = AppServices.GetRequired<FileService>().LoadOrCreateJson<BindingList<AILoadouts>>(
                     filename,
                     createNew: () => new BindingList<AILoadouts>(),
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
                     configName: "LootDrops",
                     useBoolConvertor: false
                 );
                AILootDrops drops = new AILootDrops
                {
                    LootdropList = preset
                };
                drops.setpath(filename);
                AllData.Add(drops);
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
                    AllData.Remove(data);
                }
            }

            return savedFiles;
        }
        public bool needToSave()
        {
            return false;
        }

        internal bool AddNewLootDropFile(AILootDrops newAILootDrops)
        {
            bool exists = AllData.Any(ld => ld.FileName.ToLower() == newAILootDrops.FileName.ToLower());

            if (exists)
                return false; // File with same name already exists

            AllData.Add(newAILootDrops);
            return true;
        }
    }
    public class AILootDrops
    {
        [JsonIgnore]
        private string _path;
        [JsonIgnore]
        public string FileName => Path.GetFileName(_path);
        [JsonIgnore]
        public string FilePath => _path;
        [JsonIgnore]
        public bool isDirty { get; set; }
        [JsonIgnore]
        public bool ToDelete { get; set; }

        public AILootDrops()
        {
            LootdropList = new BindingList<AILoadouts>();
        }
        public AILootDrops(string name)
        {
            name = name;
            LootdropList = new BindingList<AILoadouts>();
        }
        public BindingList<AILoadouts> LootdropList { get; set; }
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
                AppServices.GetRequired<FileService>().SaveJson(_path, LootdropList);
                isDirty = false;
                return new[] { FileName };
            }

            return Array.Empty<string>();
        }
        public void setpath(string path)
        {
            _path = path;
        }
        public override string ToString()
        {
            return FileName;
        }
    }
}
