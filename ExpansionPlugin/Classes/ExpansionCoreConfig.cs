using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionCoreConfig : ExpansionBaseIConfigLoader<ExpansionCoreSettings>
    {
        public const int CurrentVersion = 9;
 
        public ExpansionCoreConfig(string path) : base(path)
        {
        }
        protected override ExpansionCoreSettings CreateDefaultData()
        {
            return new ExpansionCoreSettings(CurrentVersion);
        }
        protected override IEnumerable<string> ValidateData()
        {
            return Data.FixMissingOrInvalidFields();
        }
    }
    public class ExpansionCoreSettings : IEquatable<ExpansionCoreSettings>, IDeepCloneable<ExpansionCoreSettings>
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
        public bool Equals(ExpansionCoreSettings? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return m_Version == other.m_Version
                && ServerUpdateRateLimit == other.ServerUpdateRateLimit
                && ForceExactCEItemLifetime == other.ForceExactCEItemLifetime
                && EnableInventoryCargoTidy == other.EnableInventoryCargoTidy;
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionCoreSettings);

        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (m_Version != ExpansionCoreConfig.CurrentVersion)
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
        public ExpansionCoreSettings Clone()
        {
            return new ExpansionCoreSettings
            {
                m_Version = this.m_Version,
                ServerUpdateRateLimit = this.ServerUpdateRateLimit,
                ForceExactCEItemLifetime = this.ForceExactCEItemLifetime,
                EnableInventoryCargoTidy = this.EnableInventoryCargoTidy
            };
        }
    }
}
