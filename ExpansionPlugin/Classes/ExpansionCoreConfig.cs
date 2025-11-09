using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionCoreConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path);
        public string FilePath => _path;
        public ExpansionCoreSettings Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }

        public const int CurrentVersion = 9;

        public ExpansionCoreConfig(string path)
        {
            _path = path;
        }

        public void Load()
        {
            Data = AppServices.GetRequired<FileService>().LoadOrCreateJson<ExpansionCoreSettings>(
                _path,
                createNew: () => new ExpansionCoreSettings(CurrentVersion),
                onAfterLoad: cfg => { /* optional: do something after load */ },
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
                configName: "ExpansionCore"
            );
            var missingFields = Data.FixMissingOrInvalidFields();
            if (missingFields.Any())
            {
                HasErrors = true;
                Errors.AddRange(missingFields);
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
    public class ExpansionCoreSettings
    {
        public int m_Version { get; set; }
        public int? ServerUpdateRateLimit { get; set; }
        public int? ForceExactCEItemLifetime { get; set; }
        public int? EnableInventoryCargoTidy { get; set; }

        public ExpansionCoreSettings()
        {
        }
        public ExpansionCoreSettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
            ServerUpdateRateLimit = 0;
            ForceExactCEItemLifetime = 0;
            EnableInventoryCargoTidy = 0;
        }
        public override bool Equals(object obj)
        {
            if (obj is not ExpansionCoreSettings other)
                return false;

            return m_Version == other.m_Version &&
                   ServerUpdateRateLimit == other.ServerUpdateRateLimit &&
                   ForceExactCEItemLifetime == other.ForceExactCEItemLifetime &&
                   EnableInventoryCargoTidy == other.EnableInventoryCargoTidy;
        }

        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (m_Version < ExpansionCoreConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionCoreConfig.CurrentVersion}");
                m_Version = ExpansionCoreConfig.CurrentVersion;
            }

            if (ServerUpdateRateLimit == null || (ServerUpdateRateLimit != 0 && ServerUpdateRateLimit != 1))
            {
                ServerUpdateRateLimit = 0;
                fixes.Add("Corrected ServerUpdateRateLimit to 0");
            }

            if (ForceExactCEItemLifetime == null || (ForceExactCEItemLifetime != 0 && ForceExactCEItemLifetime != 1))
            {
                ForceExactCEItemLifetime = 0;
                fixes.Add("Corrected ForceExactCEItemLifetime to 0");
            }

            if (EnableInventoryCargoTidy == null || (EnableInventoryCargoTidy != 0 && EnableInventoryCargoTidy != 1))
            {
                EnableInventoryCargoTidy = 0;
                fixes.Add("Corrected EnableInventoryCargoTidy to 0");
            }

            return fixes;
        }

    }
}
