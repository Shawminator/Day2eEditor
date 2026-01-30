using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionMissionConfig : ExpansionBaseIConfigLoader<MissionSettings>
    {
        public const int CurrentVersion = 2;
        public ExpansionMissionConfig(string path) : base(path)
        {
        }
        protected override MissionSettings CreateDefaultData()
        {
            return new MissionSettings(CurrentVersion);
        }
        protected override IEnumerable<string> ValidateData()
        {
            return Data.FixMissingOrInvalidFields();
        }
    }

    public class MissionSettings : IEquatable<MissionSettings>, IDeepCloneable<MissionSettings>
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
        public bool Equals(MissionSettings other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;


            return m_Version == other.m_Version &&
                Enabled == other.Enabled &&
                InitialMissionStartDelay == other.InitialMissionStartDelay &&
                TimeBetweenMissions == other.TimeBetweenMissions &&
                MinMissions == other.MinMissions &&
                MaxMissions == other.MaxMissions &&
                MinPlayersToStartMissions == other.MinPlayersToStartMissions;

        }
        public override bool Equals(object? obj) => Equals(obj as MissionSettings);
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
