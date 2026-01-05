using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionMissionConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;
        public MissionSettings Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }
        public const int CurrentVersion = 2;
        public ExpansionMissionConfig(string path)
        {
            _path = path;
        }

        public void Load()
        {
            Data = null;
            Data = AppServices.GetRequired<FileService>().LoadOrCreateJson<MissionSettings>(
                _path,
                createNew: () => new MissionSettings(CurrentVersion),
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
                configName: "ExpansionMissionSettings"
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

    public class MissionSettings
    {
        public int m_Version { get; set; }
        public int? Enabled { get; set; }
        public int? InitialMissionStartDelay { get; set; }
        public int? TimeBetweenMissions { get; set; }
        public int? MinMissions { get; set; }
        public int? MaxMissions { get; set; }
        public int? MinPlayersToStartMissions { get; set; }

        public MissionSettings() { }
        public MissionSettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
            Enabled = 1;

            InitialMissionStartDelay = 300000; // 5 minutes
            TimeBetweenMissions = 3600000; // 1 hour

            MinMissions = 0;
            MaxMissions = 1;

            MinPlayersToStartMissions = 0;
        }
        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();
            if (m_Version != ExpansionMissionConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionMissionConfig.CurrentVersion}");
                m_Version = ExpansionMissionConfig.CurrentVersion;
            }
            if (Enabled == null || (Enabled != 0 && Enabled != 1))
            {
                Enabled = 1;
                fixes.Add("Corrected Enabled");
            }
            if (InitialMissionStartDelay == null || InitialMissionStartDelay < 0)
            {
                InitialMissionStartDelay = 300000;
                fixes.Add("Set default InitialMissionStartDelay");
            }
            if (TimeBetweenMissions == null || TimeBetweenMissions < 0)
            {
                TimeBetweenMissions = 3600000;
                fixes.Add("Set default TimeBetweenMissions");
            }
            if (MinMissions == null || MinMissions < 0)
            {
                MinMissions = 0;
                fixes.Add("Set default MinMissions");
            }
            if (MaxMissions == null || MaxMissions < 0)
            {
                MaxMissions = 1;
                fixes.Add("Set default MaxMissions");
            }
            if (MinPlayersToStartMissions == null || MinPlayersToStartMissions < 0)
            {
                MinPlayersToStartMissions = 1;
                fixes.Add("Set default MinPlayersToStartMissions");
            }
            return fixes;
        }
        public override bool Equals(object obj)
        {
            if (obj is not MissionSettings other)
                return false;


            return m_Version == other.m_Version &&
                Enabled == other.Enabled &&
                InitialMissionStartDelay == other.InitialMissionStartDelay &&
                TimeBetweenMissions == other.TimeBetweenMissions &&
                MinMissions == other.MinMissions &&
                MaxMissions == other.MaxMissions &&
                MinPlayersToStartMissions == other.MinPlayersToStartMissions;

        }
        public MissionSettings Clone()
        {
            return new MissionSettings()
            { 
                m_Version = this.m_Version,
                Enabled = this.Enabled,
                InitialMissionStartDelay = this.InitialMissionStartDelay,
                TimeBetweenMissions = this.TimeBetweenMissions,
                MinMissions = this.MinMissions,
                MaxMissions = this.MaxMissions,
                MinPlayersToStartMissions= this.MinPlayersToStartMissions
            };
        }
    }
}
