using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionPlayerListConfig: ExpansionBaseIConfigLoader<ExpansionPlayerListSettings>
    {
        public const int CurrentVersion = 0;
        public ExpansionPlayerListConfig(string path) : base(path)
        {
        }
        protected override ExpansionPlayerListSettings CreateDefaultData()
        {
            return new ExpansionPlayerListSettings(CurrentVersion);
        }
        protected override IEnumerable<string> ValidateData()
        {
            return Data.FixMissingOrInvalidFields();
        }
    }
    public class ExpansionPlayerListSettings : IEquatable<ExpansionPlayerListSettings>, IDeepCloneable<ExpansionPlayerListSettings>
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
        public bool Equals(ExpansionPlayerListSettings other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return m_Version == other.m_Version &&
                EnablePlayerList == other.EnablePlayerList &&
                EnableTooltip == other.EnableTooltip;
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionPlayerListSettings);
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