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
    public class ExpansionDamageSystemConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path);
        public string FilePath => _path;
        public ExpansionDamageSystemSettings Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }

        public const int CurrentVersion = 1;

        public ExpansionDamageSystemConfig(string path)
        {
            _path = path;
        }

        public void Load()
        {
            Data = AppServices.GetRequired<FileService>().LoadOrCreateJson<ExpansionDamageSystemSettings>(
                _path,
                createNew: () => new ExpansionDamageSystemSettings(CurrentVersion),
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
                configName: "ExpansionDamageSystem"
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
    public class ExpansionDamageSystemSettings
    {
        public int m_Version { get; set; }
        public int? Enabled { get; set; }
        public int? CheckForBlockingObjects { get; set; }
        public BindingList<string> ExplosionTargets { get; set; }
        public Dictionary<string, string> ExplosiveProjectiles { get; set; }

        [JsonIgnore]
        public BindingList<ExplosiveProjectiles> explosinvesList;

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
        public override bool Equals(object obj)
        {
            if (obj is not ExpansionDamageSystemSettings other)
                return false;

            return m_Version == other.m_Version &&
                   Enabled == other.Enabled &&
                   CheckForBlockingObjects == other.CheckForBlockingObjects &&
                   ExplosionTargets.SequenceEqual(other.ExplosionTargets ?? new BindingList<string>()) &&
                   (ExplosiveProjectiles?.OrderBy(kvp => kvp.Key) ?? Enumerable.Empty<KeyValuePair<string, string>>())
                       .SequenceEqual(other.ExplosiveProjectiles?.OrderBy(kvp => kvp.Key) ?? Enumerable.Empty<KeyValuePair<string, string>>());
        }


        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (m_Version < ExpansionDamageSystemConfig.CurrentVersion)
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

    }
    public class ExplosiveProjectiles
    {
        public string explosion { get; set; }
        public string ammo { get; set; }

        public override string ToString()
        {
            return explosion;
        }
    }
}
