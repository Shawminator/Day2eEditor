using Day2eEditor;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ExpansionPlugin
{
    public class ExpansionAIConfig : ExpansionBaseIConfigLoader<ExpansionAISettings>
    {
        public const int CurrentVersion = 20;
 
        public ExpansionAIConfig(string path) : base(path)
        {
        }
        public override IEnumerable<string> Save()
        {
            if (Data is null)
                return Array.Empty<string>();

            if (!AreEqual(Data, ClonedData) || isDirty == true)
            {
                isDirty = false;
                Data.CreateDictionary();
                AppServices.GetRequired<FileService>().SaveJson(_path, Data);
                ClonedData = CloneData(Data);
                return new[] { Path.GetFileName(_path) };
            }

            return Array.Empty<string>();
        }
        protected override ExpansionAISettings CreateDefaultData()
        {
            return new ExpansionAISettings(CurrentVersion);
        }
        protected override IEnumerable<string> ValidateData()
        {
            return Data.FixMissingOrInvalidFields();
        }
        protected override void OnAfterLoad(ExpansionAISettings data)
        {
            Data.createlistfromdict();
        }
    }

    public class ExpansionAISettings : IEquatable<ExpansionAISettings>, IDeepCloneable<ExpansionAISettings>
    {
        public int m_Version { get; set; }

        public decimal? AccuracyMin { get; set; }
        public decimal? AccuracyMax { get; set; }
        public decimal? ThreatDistanceLimit { get; set; }
        public decimal? NoiseInvestigationDistanceLimit { get; set; }
        public decimal? MaxFlankingDistance { get; set; }
        public int? EnableFlankingOutsideCombat { get; set; }
        public decimal? DamageMultiplier { get; set; }
        public decimal? DamageReceivedMultiplier { get; set; }
        public decimal? ShoryukenChance { get; set; }
        public decimal? ShoryukenDamageMultiplier { get; set; }
        public BindingList<string> Admins { get; set; }
        public int? Vaulting { get; set; }
        public decimal? SniperProneDistanceThreshold { get; set; }
        public decimal? AggressionTimeout { get; set; }
        public decimal? GuardAggressionTimeout { get; set; }
        public int? Manners { get; set; }
        public int? MemeLevel { get; set; }
        public int? CanRecruitFriendly { get; set; }
        public int? CanRecruitGuards { get; set; }
        public int? MaxRecruitableAI { get; set; }
        public BindingList<string>? PreventClimb { get; set; }
        public decimal? FormationScale { get; set; } //added in version 13
        public BindingList<string>? PlayerFactions { get; set; }
        public int? LogAIHitBy { get; set; }
        public int? LogAIKilled { get; set; }
        public int? EnableZombieVehicleAttackHandler { get; set; }
        public int? EnableZombieVehicleAttackPhysics { get; set; }
        public int? OverrideClientWeaponFiring { get; set; }
        public int? RecreateWeaponNetworkRepresentation { get; set; }
        public Dictionary<int, decimal>? LightingConfigMinNightVisibilityMeters { get; set; }

        [JsonIgnore]
        public BindingList<AILightEntries> AILightEntries { get; set; }

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
            MaxFlankingDistance = (decimal)200.0;
            EnableFlankingOutsideCombat = 0;
            DamageMultiplier = (decimal)1.0;
            ShoryukenChance = 0.009999999776482582m;
            ShoryukenDamageMultiplier = 3.0m;
            Admins = new BindingList<string>();
            Vaulting = 1;
            SniperProneDistanceThreshold = (decimal)0.0;
            AggressionTimeout = (decimal)120.0;
            GuardAggressionTimeout = (decimal)150.0;
            Manners = 0;
            MemeLevel = 1;
            CanRecruitFriendly = 1;
            CanRecruitGuards = 0;
            MaxRecruitableAI = 1;
            PreventClimb = new BindingList<string>();
            FormationScale = (decimal)1.0;
            PlayerFactions = new BindingList<string>();
            LogAIHitBy = 1;
            LogAIKilled = 1;
            EnableZombieVehicleAttackHandler = 0;
            EnableZombieVehicleAttackPhysics = 0;
            OverrideClientWeaponFiring = 1;
            RecreateWeaponNetworkRepresentation = 1;
            LightingConfigMinNightVisibilityMeters = new Dictionary<int, decimal>
            {
                {0, 100.0m },
                {1, 10.0m }
            };
        }
        public void createlistfromdict()
        {
            AILightEntries = new BindingList<AILightEntries>(LightingConfigMinNightVisibilityMeters.Select(kvp => new AILightEntries { Key = kvp.Key, Value = kvp.Value }).ToList());
        }
        public void CreateDictionary()
        {
            LightingConfigMinNightVisibilityMeters = AILightEntries.ToDictionary(e => e.Key, e => e.Value);
        }
        public bool Equals(ExpansionAISettings other)
        {
            if(other is null) return false;
            if (ReferenceEquals(this, other)) return true;


            return m_Version == other.m_Version &&
                   AccuracyMin == other.AccuracyMin &&
                   AccuracyMax == other.AccuracyMax &&
                   ThreatDistanceLimit == other.ThreatDistanceLimit &&
                   NoiseInvestigationDistanceLimit == other.NoiseInvestigationDistanceLimit &&
                   MaxFlankingDistance == other.MaxFlankingDistance &&
                   EnableFlankingOutsideCombat == other.EnableFlankingOutsideCombat &&
                   DamageMultiplier == other.DamageMultiplier &&
                   DamageReceivedMultiplier == other.DamageReceivedMultiplier &&
                   ShoryukenChance == other.ShoryukenChance &&
                   ShoryukenDamageMultiplier == other.ShoryukenDamageMultiplier &&
                   Vaulting == other.Vaulting &&
                   SniperProneDistanceThreshold == other.SniperProneDistanceThreshold &&
                   AggressionTimeout == other.AggressionTimeout &&
                   GuardAggressionTimeout == other.GuardAggressionTimeout &&
                   Manners == other.Manners &&
                   MemeLevel == other.MemeLevel &&
                   CanRecruitFriendly == other.CanRecruitFriendly &&
                   CanRecruitGuards == other.CanRecruitGuards &&
                   MaxRecruitableAI == other.MaxRecruitableAI &&
                   FormationScale == other.FormationScale &&
                   LogAIHitBy == other.LogAIHitBy &&
                   LogAIKilled == other.LogAIKilled &&
                   EnableZombieVehicleAttackHandler == other.EnableZombieVehicleAttackHandler &&
                   EnableZombieVehicleAttackPhysics == other.EnableZombieVehicleAttackPhysics &&
                   OverrideClientWeaponFiring == other.OverrideClientWeaponFiring &&
                   RecreateWeaponNetworkRepresentation == other.RecreateWeaponNetworkRepresentation &&
                   SequenceEqual(Admins, other.Admins) &&
                   SequenceEqual(PreventClimb, other.PreventClimb) &&
                   SequenceEqual(PlayerFactions, other.PlayerFactions) &&
                   SequenceEqual(AILightEntries, other.AILightEntries);
        }
        public override bool Equals(object obj) => Equals(obj as ExpansionAISettings);
        public ExpansionAISettings Clone()
        {
            return new ExpansionAISettings()
            {
                m_Version = this.m_Version,
                AccuracyMin = this.AccuracyMin,
                AccuracyMax = this.AccuracyMax,
                ThreatDistanceLimit = this.ThreatDistanceLimit,
                NoiseInvestigationDistanceLimit = this.NoiseInvestigationDistanceLimit,
                MaxFlankingDistance = this.MaxFlankingDistance,
                EnableFlankingOutsideCombat = this.EnableFlankingOutsideCombat,
                DamageMultiplier = this.DamageMultiplier,
                DamageReceivedMultiplier = this.DamageReceivedMultiplier,
                ShoryukenChance = this.ShoryukenChance,
                ShoryukenDamageMultiplier = this.ShoryukenDamageMultiplier,
                Vaulting = this.Vaulting,
                SniperProneDistanceThreshold = this.SniperProneDistanceThreshold,
                AggressionTimeout = this.AggressionTimeout,
                GuardAggressionTimeout = this.GuardAggressionTimeout,
                Manners = this.Manners,
                MemeLevel = this.MemeLevel,
                CanRecruitFriendly = this.CanRecruitFriendly,
                CanRecruitGuards = this.CanRecruitGuards,
                MaxRecruitableAI = this.MaxRecruitableAI,
                FormationScale = this.FormationScale,
                LogAIHitBy = this.LogAIHitBy,
                LogAIKilled = this.LogAIKilled,
                EnableZombieVehicleAttackHandler = this.EnableZombieVehicleAttackHandler,
                EnableZombieVehicleAttackPhysics = this.EnableZombieVehicleAttackPhysics,
                OverrideClientWeaponFiring = this.OverrideClientWeaponFiring,
                RecreateWeaponNetworkRepresentation = this.RecreateWeaponNetworkRepresentation,
                Admins = new BindingList<string>(this.Admins.ToList()),
                PreventClimb = new BindingList<string>(this.PreventClimb.ToList()),
                PlayerFactions = new BindingList<string>(this.PlayerFactions.ToList()),
                AILightEntries = new BindingList<AILightEntries>(this.AILightEntries.Select(e => e.Clone()).ToList())
            };
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

            if (m_Version != ExpansionAIConfig.CurrentVersion)
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
            if (MaxFlankingDistance == null)
            {
                MaxFlankingDistance = 200.0m;
                fixes.Add("Set default MaxFlankingDistance");
            }
            if (EnableFlankingOutsideCombat != 0 && EnableFlankingOutsideCombat != 1)
            {
                EnableFlankingOutsideCombat = 0;
                fixes.Add("Corrected EnableFlankingOutsideCombat");
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

            if (ShoryukenChance == null)
            {
                ShoryukenChance = 0.009999999776482582m;
                fixes.Add("Set default ShoryukenChance");
            }

            if (ShoryukenDamageMultiplier == null)
            {
                ShoryukenDamageMultiplier = 3.0m;
                fixes.Add("Set default ShoryukenDamageMultiplier");
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

            if(AggressionTimeout == null)
            {
                AggressionTimeout = (decimal)120.0;
                fixes.Add("Set default AggressionTimeout");
            }

            if (GuardAggressionTimeout == null)
            {
                GuardAggressionTimeout = (decimal)150.0;
                fixes.Add("Set default GuardAggressionTimeout");
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

            if (MaxRecruitableAI == null)
            {
                MaxRecruitableAI = 1;
                fixes.Add("Corrected MaxRecruitableAI");
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

            if (OverrideClientWeaponFiring != 0 && OverrideClientWeaponFiring != 1)
            {
                OverrideClientWeaponFiring = 1;
                fixes.Add("Corrected OverrideClientWeaponFiring");
            }

            if (RecreateWeaponNetworkRepresentation != 0 && RecreateWeaponNetworkRepresentation != 1)
            {
                RecreateWeaponNetworkRepresentation = 1;
                fixes.Add("Corrected RecreateWeaponNetworkRepresentation");
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
        public AILightEntries Clone()
        {
            return new AILightEntries()
            {
                Key = this.Key,
                Value = this.Value
            };
        }

    }
}
