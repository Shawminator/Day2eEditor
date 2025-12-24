using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionMonitoringConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;
        public MonitoringSettings Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }
        public const int CurrentVersion = 1;
        public ExpansionMonitoringConfig(string path)
        {
            _path = path;
        }

        public void Load()
        {
            Data = null;
            Data = AppServices.GetRequired<FileService>().LoadOrCreateJson<MonitoringSettings>(
                _path,
                createNew: () => new MonitoringSettings(CurrentVersion),
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
                configName: "ExpansionMonitoringSettings"
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
    public class MonitoringSettings
    {
        public int m_Version { get; set; }
        public int? Enabled { get; set; }

        public MonitoringSettings() { }
        public MonitoringSettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
            Enabled = 1;
        }
        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();
            if (m_Version != ExpansionMonitoringConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionMonitoringConfig.CurrentVersion}");
                m_Version = ExpansionMonitoringConfig.CurrentVersion;
            }
            if (Enabled == null || (Enabled != 0 && Enabled != 1))
            {
                Enabled = 1;
                fixes.Add("Corrected Enabled");
            }
            return fixes;
        }
        public override bool Equals(object obj)
        {
            if (obj is not MonitoringSettings other)
                return false;


            return m_Version == other.m_Version &&
                Enabled == other.Enabled; 
        }
    }
}
