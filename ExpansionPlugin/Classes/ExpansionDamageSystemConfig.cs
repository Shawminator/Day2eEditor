using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpansionPlugin
{
    public class ExpansionDamageSystemConfig : ExpansionBaseIConfigLoader<ExpansionDamageSystemSettings>
    {
        public const int CurrentVersion = 1;

        public ExpansionDamageSystemConfig(string path):base(path)
        {
        }
        protected override ExpansionDamageSystemSettings CreateDefaultData()
        {
            return new ExpansionDamageSystemSettings(CurrentVersion);
        }
        protected override IEnumerable<string> ValidateData()
        {
            return Data.FixMissingOrInvalidFields();
        }
        protected override void OnAfterLoad(ExpansionDamageSystemSettings data)
        {
            GetExplosiveProjectilesList();
        }
        public void GetExplosiveProjectilesList()
        {
            Data._ExplosiveProjectiles = new BindingList<ExplosiveProjectiles>();
            foreach (KeyValuePair<string,string> ep in Data.ExplosiveProjectiles)
            {
                Data._ExplosiveProjectiles.Add(new ExplosiveProjectiles()
                {
                    explosion = ep.Key,
                    ammo = ep.Value,
                });
            }
        }
        public void SetExplosiveProjectilesDictionary()
        {
            Data.ExplosiveProjectiles = new Dictionary<string, string>();
            foreach (ExplosiveProjectiles ep in Data._ExplosiveProjectiles)
            {
                Data.ExplosiveProjectiles.Add(ep.explosion, ep.ammo);
            }
        }
    }
    public class ExpansionDamageSystemSettings : IEquatable<ExpansionDamageSystemSettings>, IDeepCloneable<ExpansionDamageSystemSettings>
    {
        public int m_Version { get; set; }
        public int? Enabled { get; set; }
        public int? CheckForBlockingObjects { get; set; }
        public BindingList<string> ExplosionTargets { get; set; }
        public Dictionary<string, string> ExplosiveProjectiles { get; set; }

        [JsonIgnore]
        public BindingList<ExplosiveProjectiles> _ExplosiveProjectiles { get; set; }

        public ExpansionDamageSystemSettings()
        {
        }
        public ExpansionDamageSystemSettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
            Enabled = 0;
            CheckForBlockingObjects = 1;
            DefaultTargets();
            DefaultProjectiles();
  
        }
        private void DefaultTargets()
        {
            ExplosionTargets = new BindingList<string>()
            {
                "ExpansionBaseBuilding",
                "ExpansionSafeBase"
            };

        }
        private void DefaultProjectiles()
        {
            ExplosiveProjectiles = new Dictionary<string, string>
            {
                { "Bullet_40mm_Explosive", "Explosion_40mm_Ammo" }
            };

        }
        public bool Equals(ExpansionDamageSystemSettings other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return m_Version == other.m_Version &&
                   Enabled == other.Enabled &&
                   CheckForBlockingObjects == other.CheckForBlockingObjects &&
                   ExplosionTargets.SequenceEqual(other.ExplosionTargets) &&
                   _ExplosiveProjectiles.SequenceEqual(other._ExplosiveProjectiles);
                  
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionCoreSettings);
        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (m_Version != ExpansionDamageSystemConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionDamageSystemConfig.CurrentVersion}");
                m_Version = ExpansionDamageSystemConfig.CurrentVersion;
            }

            if (Enabled == null || (Enabled != 0 && Enabled != 1))
            {
                Enabled = 1;
                fixes.Add("Corrected Enabled to 1");
            }

            if (CheckForBlockingObjects == null || (CheckForBlockingObjects != 0 && CheckForBlockingObjects != 1))
            {
                CheckForBlockingObjects = 1;
                fixes.Add("Corrected CheckForBlockingObjects to 1");
            }

            if (ExplosionTargets == null)
            {
                DefaultTargets();
                fixes.Add("Initialized ExplosionTargets");
            }

            if (ExplosiveProjectiles == null)
            {
                DefaultProjectiles();
                fixes.Add("Initialized ExplosiveProjectiles with default values");
            }

            return fixes;
        }
        public ExpansionDamageSystemSettings Clone()
        {
            return new ExpansionDamageSystemSettings()
            {
                m_Version = this.m_Version,
                Enabled = this.Enabled,
                CheckForBlockingObjects = this.CheckForBlockingObjects,
                ExplosionTargets = this.ExplosionTargets != null
                    ? new BindingList<string>(this.ExplosionTargets.ToList())
                    : new BindingList<string>(),
                _ExplosiveProjectiles = this._ExplosiveProjectiles != null
                    ? new BindingList<ExplosiveProjectiles>(
                        this._ExplosiveProjectiles.Select(ep => ep.Clone()).ToList())
                    : new BindingList<ExplosiveProjectiles>()
            };
        }
    }
    public class ExplosiveProjectiles
    {
        public string explosion { get; set; }
        public string ammo { get; set; }

        public override string ToString()
        {
            return explosion;
        }
        public override bool Equals(object? obj)
        {
            if (obj is not ExplosiveProjectiles other)
                return false;

            return explosion == other.explosion &&
                   ammo == other.ammo;
        }
        public ExplosiveProjectiles Clone()
        {
            // TODO: Implement actual cloning logic
            return new ExplosiveProjectiles
            {
                explosion = this.explosion,
                ammo = this.ammo
            };
        }
    }
}
