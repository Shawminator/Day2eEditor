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
    public class ExpansionAIConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;
        public ExpansionAISettings Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }
        public const int CurrentVersion = 15;
        public ExpansionAIConfig(string path)
        {
            _path = path;
        }

        public void Load()
        {
            Data = null;
            Data = AppServices.GetRequired<FileService>().LoadOrCreateJson<ExpansionAISettings>(
                _path,
                createNew: () => new ExpansionAISettings(CurrentVersion),
                onAfterLoad: cfg => {  },
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
                configName: "ExpansionAISettings"
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


            Data.createlistfromdict();
        }
        public IEnumerable<string> Save()
        {
            if (isDirty)
            {
                Data.CreateDictionary();
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

        public bool checkver()
        {
            if (Data.m_Version != CurrentVersion)
            {
                Data.m_Version = CurrentVersion;
                isDirty = true;
                return true;
            }
            return false;
        }
    }
    public class AILightEntries
    {
        public int Key { get; set; }
        public decimal Value { get; set; }

        public override string ToString() => Key.ToString();

        public override bool Equals(object obj)
        {
            if (obj is not AILightEntries other)
                return false;

            return Key == other.Key && Value == other.Value;
        }

    }
    public class ExpansionAISettings
    {
        public int m_Version { get; set; }

        public decimal? AccuracyMin { get; set; }
        public decimal? AccuracyMax { get; set; }

        public decimal? ThreatDistanceLimit { get; set; }
        public decimal? NoiseInvestigationDistanceLimit { get; set; }
        public decimal? DamageMultiplier { get; set; }
        public decimal? DamageReceivedMultiplier { get; set; }

        public BindingList<string> Admins { get; set; }

        public int? Vaulting { get; set; }
        public decimal? SniperProneDistanceThreshold { get; set; }
        public int? Manners { get; set; }
        public int? MemeLevel { get; set; }
        public int? CanRecruitFriendly { get; set; }
        public int? CanRecruitGuards { get; set; }
        public BindingList<string>? PreventClimb { get; set; }
        public decimal? FormationScale { get; set; } //added in version 13
        public BindingList<string>? PlayerFactions { get; set; }
        public int? LogAIHitBy { get; set; }
        public int? LogAIKilled { get; set; }
        public int? EnableZombieVehicleAttackHandler { get; set; }
        public int? EnableZombieVehicleAttackPhysics { get; set; }

        public Dictionary<int, decimal>? LightingConfigMinNightVisibilityMeters { get; set; }

        [JsonIgnore]
        public BindingList<AILightEntries> AILightEntries { get; set; }

        public void createlistfromdict()
        {
            AILightEntries = new BindingList<AILightEntries>(LightingConfigMinNightVisibilityMeters.Select(kvp => new AILightEntries { Key = kvp.Key, Value = kvp.Value }).ToList());
        }
        public void CreateDictionary()
        {
            LightingConfigMinNightVisibilityMeters = AILightEntries.ToDictionary(e => e.Key, e => e.Value);
        }

        public ExpansionAISettings()
        {

        }
        public ExpansionAISettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
            AccuracyMin = (decimal)0.35;
            AccuracyMax = (decimal)0.95;
            ThreatDistanceLimit = (decimal)1000.0;
            NoiseInvestigationDistanceLimit = (decimal)500.0;
            DamageMultiplier = (decimal)1.0;
            Admins = new BindingList<string>();
            Vaulting = 1;
            SniperProneDistanceThreshold = (decimal)0.0;
            Manners = 0;
            MemeLevel = 1;
            CanRecruitFriendly = 1;
            CanRecruitGuards = 0;
            PreventClimb = new BindingList<string>();
            FormationScale = (decimal)1.0;
            PlayerFactions = new BindingList<string>();
            LogAIHitBy = 1;
            LogAIKilled = 1;
            EnableZombieVehicleAttackHandler = 0;
            EnableZombieVehicleAttackPhysics = 0;
            LightingConfigMinNightVisibilityMeters = new Dictionary<int, decimal>
            {
                {0, 100.0m },
                {1, 10.0m }
            };
        }


        public override bool Equals(object obj)
        {
            if (obj is not ExpansionAISettings other)
                return false;

            return m_Version == other.m_Version &&
                   AccuracyMin == other.AccuracyMin &&
                   AccuracyMax == other.AccuracyMax &&
                   ThreatDistanceLimit == other.ThreatDistanceLimit &&
                   NoiseInvestigationDistanceLimit == other.NoiseInvestigationDistanceLimit &&
                   DamageMultiplier == other.DamageMultiplier &&
                   DamageReceivedMultiplier == other.DamageReceivedMultiplier &&
                   Vaulting == other.Vaulting &&
                   SniperProneDistanceThreshold == other.SniperProneDistanceThreshold &&
                   Manners == other.Manners &&
                   MemeLevel == other.MemeLevel &&
                   CanRecruitFriendly == other.CanRecruitFriendly &&
                   CanRecruitGuards == other.CanRecruitGuards &&
                   FormationScale == other.FormationScale &&
                   LogAIHitBy == other.LogAIHitBy &&
                   LogAIKilled == other.LogAIKilled &&
                   EnableZombieVehicleAttackHandler == other.EnableZombieVehicleAttackHandler &&
                   EnableZombieVehicleAttackPhysics == other.EnableZombieVehicleAttackPhysics &&
                   SequenceEqual(Admins, other.Admins) &&
                   SequenceEqual(PreventClimb, other.PreventClimb) &&
                   SequenceEqual(PlayerFactions, other.PlayerFactions) &&
                   SequenceEqual(AILightEntries, other.AILightEntries);
        }

        private bool SequenceEqual(BindingList<string> a, BindingList<string> b)
        {
            return a != null && b != null && a.SequenceEqual(b);
        }
        private bool SequenceEqual(BindingList<AILightEntries> a, BindingList<AILightEntries> b)
        {
            return a != null && b != null && a.SequenceEqual(b);
        }
        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (m_Version < ExpansionAIConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionAIConfig.CurrentVersion}");
                m_Version = ExpansionAIConfig.CurrentVersion;
            }

            if (AccuracyMin == null)
            {
                AccuracyMin = 0.35m;
                fixes.Add("Set default AccuracyMin");
            }

            if (AccuracyMax == null)
            {
                AccuracyMax = 0.95m;
                fixes.Add("Set default AccuracyMax");
            }

            if (AccuracyMin > AccuracyMax)
            {
                AccuracyMin = 0.35m;
                AccuracyMax = 0.95m;
                fixes.Add("Corrected AccuracyMin > AccuracyMax");
            }

            if (ThreatDistanceLimit == null)
            {
                ThreatDistanceLimit = 1000.0m;
                fixes.Add("Set default ThreatDistanceLimit");
            }

            if (NoiseInvestigationDistanceLimit == null)
            {
                NoiseInvestigationDistanceLimit = 500.0m;
                fixes.Add("Set default NoiseInvestigationDistanceLimit");
            }

            if (DamageMultiplier == null)
            {
                DamageMultiplier = 1.0m;
                fixes.Add("Set default DamageMultiplier");
            }

            if (DamageReceivedMultiplier == null)
            {
                DamageReceivedMultiplier = 1.0m;
                fixes.Add("Set default DamageReceivedMultiplier");
            }

            if (Admins == null)
            {
                Admins = new BindingList<string>();
                fixes.Add("Initialized Admins");
            }
            
            if (Vaulting != 0 && Vaulting != 1)
            {
                Vaulting = 1;
                fixes.Add("Corrected Vaulting");
            }
            
            if (SniperProneDistanceThreshold == null)
            {
                SniperProneDistanceThreshold = 0.0m;
                fixes.Add("Set default SniperProneDistanceThreshold");
            }

            if (Manners != 0 && Manners != 1)
            {
                Manners = 0;
                fixes.Add("Corrected Manners");
            }

            if (MemeLevel == null)
            {
                MemeLevel = 1;
                fixes.Add("Corrected MemeLevel");
            }

            if (CanRecruitFriendly != 0 && CanRecruitFriendly != 1)
            {
                CanRecruitFriendly = 1;
                fixes.Add("Corrected CanRecruitFriendly");
            }

            if (CanRecruitGuards != 0 && CanRecruitGuards != 1)
            {
                CanRecruitGuards = 0;
                fixes.Add("Corrected CanRecruitGuards");
            }
 
            if (PreventClimb == null)
            {
                PreventClimb = new BindingList<string>();
                fixes.Add("Initialized PreventClimb");
            }

            if (FormationScale == null)
            {
                FormationScale = 1.0m;
                fixes.Add("Set default FormationScale");
            }

            if (PlayerFactions == null)
            {
                PlayerFactions = new BindingList<string>();
                fixes.Add("Initialized PlayerFactions");
            }

            if (LogAIHitBy != 0 && LogAIHitBy != 1)
            {
                LogAIHitBy = 1;
                fixes.Add("Corrected LogAIHitBy");
            }

            if (LogAIKilled != 0 && LogAIKilled != 1)
            {
                LogAIKilled = 1;
                fixes.Add("Corrected LogAIKilled");
            }

            if (EnableZombieVehicleAttackHandler != 0 && EnableZombieVehicleAttackHandler != 1)
            {
                EnableZombieVehicleAttackHandler = 0;
                fixes.Add("Corrected EnableZombieVehicleAttackHandler");
            }

            if (EnableZombieVehicleAttackPhysics != 0 && EnableZombieVehicleAttackPhysics != 1)
            {
                EnableZombieVehicleAttackPhysics = 0;
                fixes.Add("Corrected EnableZombieVehicleAttackPhysics");
            }

            if (LightingConfigMinNightVisibilityMeters == null)
            {
                LightingConfigMinNightVisibilityMeters = new Dictionary<int, decimal>
                {
                    { 0, 100.0m },
                    { 1, 10.0m }
                };
                fixes.Add("Initialized LightingConfigMinNightVisibilityMeters");
            }


            return fixes;
        }


    }
}
