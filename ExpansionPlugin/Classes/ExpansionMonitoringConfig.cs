using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionMonitoringConfig : ExpansionBaseIConfigLoader<MonitoringSettings>
    {
        public const int CurrentVersion = 1;
        public ExpansionMonitoringConfig(string path) : base(path)
        {
        }
        protected override MonitoringSettings CreateDefaultData()
        {
            return new MonitoringSettings(CurrentVersion);
        }
        protected override IEnumerable<string> ValidateData()
        {
            return Data.FixMissingOrInvalidFields();
        }
    }
    public class MonitoringSettings : IEquatable<MonitoringSettings>, IDeepCloneable<MonitoringSettings>
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
        public bool Equals(MonitoringSettings other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return m_Version == other.m_Version &&
                Enabled == other.Enabled; 
        }
        public override bool Equals(object? obj) => Equals(obj as MonitoringSettings);
        public MonitoringSettings Clone()
        {
            return new MonitoringSettings()
            {
                m_Version = this.m_Version,
                Enabled = this.Enabled
            };
        }
    }
}
