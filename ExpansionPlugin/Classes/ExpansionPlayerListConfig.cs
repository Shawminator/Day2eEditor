using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionPlayerListConfig: IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;
        public ExpansionPlayerListSettings Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }
        public const int CurrentVersion = 0;

        public ExpansionPlayerListConfig(string path)
        {
            _path = path;
        }
        public void Load()
        {
            Data = null;
            Data = AppServices.GetRequired<FileService>().LoadOrCreateJson<ExpansionPlayerListSettings>(
                _path,
                createNew: () => new ExpansionPlayerListSettings(CurrentVersion),
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
                configName: "ExpansionPlayerList"
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
    public class ExpansionPlayerListSettings
    {
        public int m_Version { get; set; }
        public int? EnablePlayerList { get; set; }
        public int? EnableTooltip { get; set; }

        public ExpansionPlayerListSettings()
        {

        }
        public ExpansionPlayerListSettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
            EnablePlayerList = 1;
            EnableTooltip = 1;
        }
        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (m_Version != ExpansionPlayerListConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionPlayerListConfig.CurrentVersion}");
                m_Version = ExpansionPlayerListConfig.CurrentVersion;
            }
            if (EnablePlayerList is null or < 0 or > 1)
            {
                EnablePlayerList = 1;
                fixes.Add("Corrected EnablePlayerList");
            }
            if (EnableTooltip is null or < 0 or > 1)
            {
                EnableTooltip = 0;
                fixes.Add("Corrected EnableTooltip");
            }
            return fixes;
        }
        public override bool Equals(object obj)
        {
            if (obj is not ExpansionPlayerListSettings other)
                return false;

            return m_Version == other.m_Version &&
                EnablePlayerList == other.EnablePlayerList &&
                EnableTooltip == other.EnableTooltip;
        }
        public ExpansionPlayerListSettings Clone()
        {
            return new ExpansionPlayerListSettings()
            {
                m_Version = this.m_Version,
                EnablePlayerList = this.EnablePlayerList,
                EnableTooltip = this.EnableTooltip
            };
        }
    }
}