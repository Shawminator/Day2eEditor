using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionPersonalStorageNewConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;
        public ExpansionPersonalStorageNewSettings Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }
        public const int CurrentVersion = 3;

        public ExpansionPersonalStorageNewConfig(string path)
        {
            _path = path;
        }
        public void Load()
        {
            Data = null;
            Data = AppServices.GetRequired<FileService>().LoadOrCreateJson<ExpansionPersonalStorageNewSettings>(
                _path,
                createNew: () => new ExpansionPersonalStorageNewSettings(CurrentVersion),
                onAfterLoad: cfg => { },
                onError: ex =>
                {
                    HasErrors = true;
                    Console.WriteLine(
                        "Error in " + Path.GetFileName(_path) + "\n" +
                        ex.Message + "\n" +
                        ex.InnerException?.Message + "\n"
                    );
                    Errors.Add("Error in " + Path.GetFileName(_path) + "\n" +
                        ex.Message + "\n" +
                        ex.InnerException?.Message);
                },
                configName: "ExpansionPersonalStorageNew"
            );
            var missingFields = Data.FixMissingOrInvalidFields();
            if (missingFields.Any())
            {
                Console.WriteLine("Validation issues in " + FileName + ":");
                foreach (var issue in missingFields)
                {
                    Console.WriteLine("- " + issue);
                }
                isDirty = true;
            }
        }
        public IEnumerable<string> Save()
        {
            if (isDirty)
            {
                AppServices.GetRequired<FileService>().SaveJson(_path, Data);
                isDirty = false;
                return new[] { Path.GetFileName(_path) };
            }

            return Array.Empty<string>();
        }
        public bool needToSave()
        {
            return isDirty;
        }
    }
    public class ExpansionPersonalStorageNewSettings
    {
        public int m_Version { get; set; }
        public int UseCategoryMenu { get; set; }
        public BindingList<string> ExcludedItems { get; set; }
        public Dictionary<int,ExpansionPersonalStorageLevel> StorageLevels { get; set; }

        public ExpansionPersonalStorageNewSettings()
        {

        }
        public ExpansionPersonalStorageNewSettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
            UseCategoryMenu = 0;
            ExcludedItems = new BindingList<string>();
            StorageLevels = new Dictionary<int, ExpansionPersonalStorageLevel>();

            DefaultStirageLevels();
        }

        private void DefaultStirageLevels()
        {
            List<string> defaultExcludedSlots = new List<string>() { "Vest", "Body", "Hips", "Legs", "Back" };

            for (int lvl = 1; lvl <= 10; lvl++)
            {
                switch (lvl)
                {
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                        StorageLevels.Add(lvl, new ExpansionPersonalStorageLevel(lvl, -1, 0));
                        break;
                    case 10:
                        StorageLevels.Add(lvl, new ExpansionPersonalStorageLevel(lvl, -1, 0, null, 1));
                        break;
                    default:
                        StorageLevels.Add(lvl, new ExpansionPersonalStorageLevel(lvl, -1, 0, new BindingList<string>(defaultExcludedSlots), 0));
                        break;
                }
            }
        }

        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (m_Version != ExpansionPersonalStorageNewConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionPersonalStorageNewConfig.CurrentVersion}");
                m_Version = ExpansionPersonalStorageNewConfig.CurrentVersion;
            }
            if (UseCategoryMenu == null || (UseCategoryMenu != 0 && UseCategoryMenu != 1))
            {
                UseCategoryMenu = 0;
                fixes.Add("Corrected UseCategoryMenu to 0");
            }
            if(ExcludedItems == null)
            {
                ExcludedItems = new BindingList<string>();
                fixes.Add("Initialized ExcludedItems");
            }
            if(StorageLevels == null)
            {
                DefaultStirageLevels();
                fixes.Add("Initialized StorageLevels to default Levels");
            }

            return fixes;
        }

    public class ExpansionPersonalStorageLevel
    {
        public int ReputationRequirement { get; set; }
        public int QuestID { get; set; }
        public BindingList<string> ExcludedSlots { get; set; }
	    public int AllowAttachmentCargo { get; set; }

        [JsonIgnore]
        public int m_Level { get; set; }

        public ExpansionPersonalStorageLevel(int lvl, int repReq, int questID, BindingList<string> excludedSlots = null, int allowAttCargo = 0)
        {
            m_Level = lvl;
            ReputationRequirement = repReq;
            QuestID = questID;
            if (excludedSlots != null)
                ExcludedSlots = excludedSlots;
            AllowAttachmentCargo = allowAttCargo;
        }
    }
}
