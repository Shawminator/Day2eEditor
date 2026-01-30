using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpansionPlugin
{
    public class ExpansionAIPatrolConfig : ExpansionBaseIConfigLoader<ExpansionAIPatrolSettings>
    {
        public const int CurrentVersion = 31;

        public ExpansionAIPatrolConfig(string path) :base(path)
        {
        }
        public override IEnumerable<string> Save()
        {
            if (isDirty)
            {
                SetLoadBalancingCategoriestoDictionary();
                AppServices.GetRequired<FileService>().SaveJson(_path, Data, false, true);
                isDirty = false;
                return new[] { Path.GetFileName(_path) };
            }

            return Array.Empty<string>();
        }
        protected override ExpansionAIPatrolSettings CreateDefaultData()
        {
            return new ExpansionAIPatrolSettings(CurrentVersion);
        }
        protected override IEnumerable<string> ValidateData()
        {
            return Data.FixMissingOrInvalidFields();
        }
        protected override void OnAfterLoad(ExpansionAIPatrolSettings data)
        {
            GetLoadBalancingCategoriestoList();
        }
        //additional functions
        public bool GetLoadBalancingCategoriestoList()
        {
            bool needtosave = false;
            Data._LoadBalancingCategories = new BindingList<Loadbalancingcategorie>();
            foreach (KeyValuePair<string, BindingList<Loadbalancingcategories>> keyValuePair in Data.LoadBalancingCategories)
            {
                Data._LoadBalancingCategories.Add(new Loadbalancingcategorie()
                {
                    name = keyValuePair.Key,
                    Categorieslist = keyValuePair.Value
                });
            }
            return needtosave;
        }
        public void SetLoadBalancingCategoriestoDictionary()
        {
            Data.LoadBalancingCategories = new Dictionary<string, BindingList<Loadbalancingcategories>>();
            foreach (Loadbalancingcategorie keyValuePair in Data._LoadBalancingCategories)
            {
                Data.LoadBalancingCategories.Add(keyValuePair.name, keyValuePair.Categorieslist);
            }
        }
    }
    public class ExpansionAIPatrolSettings : IEquatable<ExpansionAIPatrolSettings>, IDeepCloneable<ExpansionAIPatrolSettings>
    {
        public int m_Version { get; set; }
        public int? Enabled { get; set; }
        public decimal? FormationScale { get; set; }
        public decimal? DespawnTime { get; set; }
        public decimal? RespawnTime { get; set; }
        public decimal? MinDistRadius { get; set; }
        public decimal? MaxDistRadius { get; set; }
        public decimal? DespawnRadius { get; set; }
        public decimal? AccuracyMin { get; set; }
        public decimal? AccuracyMax { get; set; }
        public decimal? ThreatDistanceLimit { get; set; }
        public decimal? NoiseInvestigationDistanceLimit { get; set; }
        public decimal? MaxFlankingDistance { get; set; }
        public int? EnableFlankingOutsideCombat { get; set; }
        public decimal? DamageMultiplier { get; set; }
        public decimal? DamageReceivedMultiplier { get; set; }

        public Dictionary<string, BindingList<Loadbalancingcategories>> LoadBalancingCategories { get; set; } //added version 24
        public BindingList<ExpansionAIPatrol> Patrols { get; set; }

        [JsonIgnore]
        public BindingList<Loadbalancingcategorie> _LoadBalancingCategories { get; set; }

        public ExpansionAIPatrolSettings()
        {
        }
        public ExpansionAIPatrolSettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
            Enabled = 1;
            FormationScale = (decimal)-1.0;
            DespawnTime = (decimal)600.0;
            RespawnTime = (decimal)600.0;
            MinDistRadius = (decimal)400.0;
            MaxDistRadius = (decimal)1000.0;
            DespawnRadius = (decimal)1100.0;
            AccuracyMin = (decimal)-1.0;
            AccuracyMax = (decimal)-1.0;
            ThreatDistanceLimit = (decimal)-1.0;
            DamageMultiplier = (decimal)-1.0;
            NoiseInvestigationDistanceLimit = (decimal)-1.0;
            MaxFlankingDistance = (decimal)-1;
            EnableFlankingOutsideCombat = -1;
            DamageReceivedMultiplier = (decimal)-1.0;
            DefaultLoadBalancing();
            DefaultObjectPatrols();
        }
        private void DefaultLoadBalancing()
        {
            LoadBalancingCategories = new Dictionary<string, BindingList<Loadbalancingcategories>>();
            LoadBalancingCategories.Add(
                "Global",
                new BindingList<Loadbalancingcategories>()
                {
                    new Loadbalancingcategories(){MinPlayers = 0, MaxPlayers = 10, MaxPatrols = 20},
                    new Loadbalancingcategories(){MinPlayers = 11, MaxPlayers = 20, MaxPatrols = 16},
                    new Loadbalancingcategories(){MinPlayers = 21, MaxPlayers = 30, MaxPatrols = 12},
                    new Loadbalancingcategories(){MinPlayers = 31, MaxPlayers = 40, MaxPatrols = 8},
                    new Loadbalancingcategories(){MinPlayers = 41, MaxPlayers = 50, MaxPatrols = 4},
                    new Loadbalancingcategories(){MinPlayers = 51, MaxPlayers = 255, MaxPatrols = 0},
                }
            );
            LoadBalancingCategories.Add(
                "ObjectPatrol",
                new BindingList<Loadbalancingcategories>()
                {
                    new Loadbalancingcategories(){MinPlayers = 0, MaxPlayers = 255, MaxPatrols = 5}
                }
            );
            LoadBalancingCategories.Add(
                "Patrol",
                new BindingList<Loadbalancingcategories>()
                {
                    new Loadbalancingcategories(){MinPlayers = 0, MaxPlayers = 255, MaxPatrols = 5}
                }
            );
            LoadBalancingCategories.Add(
                "Quest",
                new BindingList<Loadbalancingcategories>()
                {
                    new Loadbalancingcategories(){MinPlayers = 0, MaxPlayers = 255, MaxPatrols = -1}
                }
            );
            LoadBalancingCategories.Add(
                "HelicopterWreck",
                new BindingList<Loadbalancingcategories>()
                {
                    new Loadbalancingcategories(){MinPlayers = 0, MaxPlayers = 255, MaxPatrols = 3}
                }
            );
            LoadBalancingCategories.Add(
                "ContaminatedArea",
                new BindingList<Loadbalancingcategories>()
                {
                    new Loadbalancingcategories(){MinPlayers = 0, MaxPlayers = 255, MaxPatrols = 3}
                }
            );
            LoadBalancingCategories.Add(
               "MilitaryRoaming",
               new BindingList<Loadbalancingcategories>()
               {
                    new Loadbalancingcategories(){MinPlayers = 0, MaxPlayers = 255, MaxPatrols = 5}
               }
           );
            LoadBalancingCategories.Add(
               "MilitaryStatic",
               new BindingList<Loadbalancingcategories>()
               {
                    new Loadbalancingcategories(){MinPlayers = 0, MaxPlayers = 255, MaxPatrols = 5}
               }
           );
            LoadBalancingCategories.Add(
               "Survivor",
               new BindingList<Loadbalancingcategories>()
               {
                    new Loadbalancingcategories(){MinPlayers = 0, MaxPlayers = 255, MaxPatrols = 15}
               }
           );
            LoadBalancingCategories.Add(
                "Example",
                new BindingList<Loadbalancingcategories>()
                {
                    new Loadbalancingcategories(){MinPlayers = 0, MaxPlayers = 255, MaxPatrols = -1}
                }
            );
        }
        private void DefaultObjectPatrols()
        {
            int unlimitedReload =6;
            Patrols = new BindingList<ExpansionAIPatrol>();
            //! -------------------------------------------------------------------
            //! Heli crash patrols
            //! -------------------------------------------------------------------
            ExpansionAIPatrol patrol = new ExpansionAIPatrol(-2, "JOG", "SPRINT", "LOOP_OR_ALTERNATE", "West", "", true, unlimitedReload, (decimal)0.5, -1, -1, "Wreck_UH1Y");
            patrol.DefaultStance = "CROUCHED";
            patrol.LoadBalancingCategory = "HelicopterWreck";
            patrol.LootingBehaviour = "DEFAULT | CLOTHING_BODY | CLOTHING_LEGS | CLOTHING_GLOVES | CLOTHING_FEET | CLOTHING_SIMILAR | UPGRADE";
            Patrols.Add(patrol);

            patrol = new ExpansionAIPatrol(-2, "JOG", "SPRINT", "LOOP_OR_ALTERNATE", "East", "", true, unlimitedReload, (decimal)0.5, -1, -1, "Wreck_Mi8_Crashed");
            patrol.DefaultStance = "CROUCHED";
            patrol.LoadBalancingCategory = "HelicopterWreck";
            patrol.LootingBehaviour = "DEFAULT | CLOTHING_BODY | CLOTHING_LEGS | CLOTHING_GLOVES | CLOTHING_FEET | CLOTHING_SIMILAR | UPGRADE";
            Patrols.Add(patrol);

            //! -------------------------------------------------------------------
            //! Police patrols
            //! -------------------------------------------------------------------
            patrol = new ExpansionAIPatrol(-2, "WALK", "SPRINT", "LOOP_OR_ALTERNATE", "Guards", "PoliceLoadout", true, unlimitedReload, (decimal)0.1, -1, -1, "Land_City_PoliceStation");
            patrol.LootingBehaviour = "DEFAULT | CLOTHING_BODY | CLOTHING_LEGS | CLOTHING_FEET | CLOTHING_SIMILAR";
            patrol.WaypointInterpolation = "UniformCubic";
            Patrols.Add(patrol);

            patrol = new ExpansionAIPatrol(-2, "WALK", "SPRINT", "LOOP_OR_ALTERNATE", "Guards", "PoliceLoadout", true, unlimitedReload, (decimal)0.1, -1, -1, "Land_Village_PoliceStation");
            patrol.LootingBehaviour = "DEFAULT | CLOTHING_BODY | CLOTHING_LEGS | CLOTHING_FEET | CLOTHING_SIMILAR";
            patrol.WaypointInterpolation = "UniformCubic";
            Patrols.Add(patrol);

            //! -------------------------------------------------------------------
            //! Contaminated area patrols
            //! -------------------------------------------------------------------

            patrol = new ExpansionAIPatrol(-2, "JOG", "SPRINT", "LOOP_OR_ALTERNATE", "West", "NBCLoadout", true, unlimitedReload, (decimal)0.5, -1, -1, "ContaminatedArea_Static");
            patrol.CanSpawnInContaminatedArea = 1;
            patrol.LoadBalancingCategory = "ContaminatedArea";
            patrol.LootingBehaviour = "DEFAULT | CLOTHING_BODY | CLOTHING_LEGS | CLOTHING_HEADGEAR | CLOTHING_MASK | CLOTHING_GLOVES | CLOTHING_FEET | CLOTHING_SIMILAR";
            patrol.WaypointInterpolation = "UniformCubic";
            Patrols.Add(patrol);

            patrol = new ExpansionAIPatrol(-2, "JOG", "SPRINT", "LOOP_OR_ALTERNATE", "West", "NBCLoadout", true, unlimitedReload, (decimal)0.5, -1, -1, "ContaminatedArea_Dynamic");
            patrol.CanSpawnInContaminatedArea = 1;
            patrol.LoadBalancingCategory = "ContaminatedArea";
            patrol.LootingBehaviour = "DEFAULT | CLOTHING_BODY | CLOTHING_LEGS | CLOTHING_HEADGEAR | CLOTHING_MASK | CLOTHING_GLOVES | CLOTHING_FEET | CLOTHING_SIMILAR";
            patrol.WaypointInterpolation = "UniformCubic";
            Patrols.Add(patrol);

            //! -------------------------------------------------------------------
            //! Military patrols - static
            //! -------------------------------------------------------------------

            patrol = new ExpansionAIPatrol(1, "WALK", "SPRINT", "HALT", "West", "", true, unlimitedReload, (decimal)0.1, -1, -1, "Land_Mil_Tower_Small", new List<Vec3>() { new Vec3("-0.0151265 1.32325 1.19653") });
            patrol.LoadBalancingCategory = "MilitaryStatic";
            patrol.LootingBehaviour = "DEFAULT | CLOTHING_BODY | CLOTHING_LEGS | CLOTHING_FEET | CLOTHING_SIMILAR";
            Patrols.Add(patrol);

            patrol = new ExpansionAIPatrol(1, "WALK", "SPRINT", "HALT", "West", "", true, unlimitedReload, (decimal)0.2, -1, -1, "Land_Mil_GuardTower", new List<Vec3>() { new Vec3("0.0688021 3.62166 -3.98126") });
            patrol.DefaultLookAngle = 180;
            patrol.LoadBalancingCategory = "MilitaryStatic";
            patrol.LootingBehaviour = "DEFAULT | CLOTHING_BODY | CLOTHING_LEGS | CLOTHING_FEET | CLOTHING_SIMILAR";
            Patrols.Add(patrol);

            patrol = new ExpansionAIPatrol(1, "WALK", "SPRINT", "HALT", "West", "", true, unlimitedReload, (decimal)0.1, -1, -1, "Land_Mil_GuardBox_Brown", new List<Vec3>() { new Vec3("0.161377 -0.789551 -0.782226")});
            patrol.DefaultLookAngle = 180;
            patrol.LoadBalancingCategory = "MilitaryStatic";
            patrol.LootingBehaviour = "DEFAULT | CLOTHING_BODY | CLOTHING_LEGS | CLOTHING_FEET | CLOTHING_SIMILAR";
            Patrols.Add(patrol);

            patrol = new ExpansionAIPatrol(1, "WALK", "SPRINT", "HALT", "West", "", true, unlimitedReload, (decimal)0.1, -1, -1, "Land_Mil_GuardBox_Green", new List<Vec3>() { new Vec3("0.161377 -0.789551 -0.782226" ) });
            patrol.DefaultLookAngle = 180;
            patrol.LoadBalancingCategory = "MilitaryStatic";
            patrol.LootingBehaviour = "DEFAULT | CLOTHING_BODY | CLOTHING_LEGS | CLOTHING_FEET | CLOTHING_SIMILAR";
            Patrols.Add(patrol);

            patrol = new ExpansionAIPatrol(1, "WALK", "SPRINT", "HALT", "West", "", true, unlimitedReload, (decimal)0.1, -1, -1, "Land_Mil_GuardBox_Smooth", new List<Vec3>() { new Vec3("0.161377 -0.789551 -0.782226") });
            patrol.DefaultLookAngle = 180;
            patrol.LoadBalancingCategory = "MilitaryStatic";
            patrol.LootingBehaviour = "DEFAULT | CLOTHING_BODY | CLOTHING_LEGS | CLOTHING_FEET | CLOTHING_SIMILAR";
            Patrols.Add(patrol);

            patrol = new ExpansionAIPatrol(1, "WALK", "SPRINT", "HALT", "West", "", true, unlimitedReload, (decimal)0.1, -1, -1, "Land_Mil_Fortified_Nest_Small", new List<Vec3>() { new Vec3("0 -0.965636 0") });
            patrol.DefaultLookAngle = 180;
            patrol.LoadBalancingCategory = "MilitaryStatic";
            patrol.LootingBehaviour = "DEFAULT | CLOTHING_BODY | CLOTHING_LEGS | CLOTHING_FEET | CLOTHING_SIMILAR";
            Patrols.Add(patrol);

            patrol = new ExpansionAIPatrol(1, "WALK", "SPRINT", "HALT", "West", "", true, unlimitedReload, (decimal)0.1, -1, -1, "Land_Mil_GuardShed", new List<Vec3>() { new Vec3("0 -0.685577 0") });
            patrol.DefaultLookAngle = 180;
            patrol.LoadBalancingCategory = "MilitaryStatic";
            patrol.LootingBehaviour = "DEFAULT | CLOTHING_BODY | CLOTHING_LEGS | CLOTHING_FEET | CLOTHING_SIMILAR";
            Patrols.Add(patrol);

            //! -------------------------------------------------------------------
            //! Military patrols - roaming (local)
            //! -------------------------------------------------------------------

            patrol = new ExpansionAIPatrol(-3, "WALK", "SPRINT", "ROAMING_LOCAL", "West", "", true, unlimitedReload, (decimal)0.2, -1, -1, "Land_Mil_Barracks1", new List<Vec3>() { new Vec3("0 -1.94586 0")});
            patrol.LoadBalancingCategory = "MilitaryRoaming";
            patrol.LootingBehaviour = "DEFAULT | CLOTHING_BODY | CLOTHING_LEGS | CLOTHING_FEET | CLOTHING_SIMILAR";
            Patrols.Add(patrol);

            patrol = new ExpansionAIPatrol(1, "WALK", "SPRINT", "ROAMING_LOCAL", "West", "", true, unlimitedReload, (decimal)0.2, -1, -1, "Land_Mil_Barracks_Round", new List<Vec3>() { new Vec3("-1.0607 -0.691666 2.30131")});
            patrol.LoadBalancingCategory = "MilitaryRoaming";
            patrol.LootingBehaviour = "DEFAULT | CLOTHING_BODY | CLOTHING_LEGS | CLOTHING_FEET | CLOTHING_SIMILAR";
            Patrols.Add(patrol);

            patrol = new ExpansionAIPatrol(1, "WALK", "SPRINT", "ROAMING_LOCAL", "West", "", true, unlimitedReload, (decimal)0.2, -1, -1, "Land_Mil_Tent_Big1_2", new List<Vec3>() { new Vec3("0 0 0")});
            patrol.LoadBalancingCategory = "MilitaryRoaming";
            patrol.LootingBehaviour = "DEFAULT | CLOTHING_BODY | CLOTHING_LEGS | CLOTHING_FEET | CLOTHING_SIMILAR";
            Patrols.Add(patrol);

            patrol = new ExpansionAIPatrol(-2, "WALK", "SPRINT", "ROAMING_LOCAL", "West", "", true, unlimitedReload, (decimal)0.2, -1, -1, "Land_Mil_Tent_Big1_4", new List<Vec3>() { new Vec3("0 0 0")});
            patrol.LoadBalancingCategory = "MilitaryRoaming";
            patrol.LootingBehaviour = "DEFAULT | CLOTHING_BODY | CLOTHING_LEGS | CLOTHING_FEET | CLOTHING_SIMILAR";
            Patrols.Add(patrol);
        }
        public bool Equals(ExpansionAIPatrolSettings other)
        {
            if(other is null) return false;
            if (ReferenceEquals(this, other)) return true;


            return m_Version == other.m_Version &&
                   Enabled == other.Enabled &&
                   FormationScale == other.FormationScale &&
                   DespawnTime == other.DespawnTime &&
                   RespawnTime == other.RespawnTime &&
                   MinDistRadius == other.MinDistRadius &&
                   MaxDistRadius == other.MaxDistRadius &&
                   DespawnRadius == other.DespawnRadius &&
                   AccuracyMin == other.AccuracyMin &&
                   AccuracyMax == other.AccuracyMax &&
                   ThreatDistanceLimit == other.ThreatDistanceLimit &&
                   NoiseInvestigationDistanceLimit == other.NoiseInvestigationDistanceLimit &&
                   MaxFlankingDistance == other.MaxFlankingDistance &&
                   EnableFlankingOutsideCombat == other.EnableFlankingOutsideCombat &&
                   DamageMultiplier == other.DamageMultiplier &&
                   DamageReceivedMultiplier == other.DamageReceivedMultiplier &&
                   Patrols.SequenceEqual(other.Patrols) &&
                   _LoadBalancingCategories.SequenceEqual(other._LoadBalancingCategories);
        }
        public override bool Equals(object obj) => Equals(obj as ExpansionAIPatrolSettings);
        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();
            if (m_Version != ExpansionAIPatrolConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionAIPatrolConfig.CurrentVersion}");
                m_Version = ExpansionAIPatrolConfig.CurrentVersion;
            }
            if (Enabled == null || (Enabled != 0 && Enabled != 1))
            {
                Enabled = 0;
                fixes.Add("Corrected PoorItemRequirement to 0");
            }
            if (FormationScale == null )
            {
                FormationScale = -1;
                fixes.Add("Corrected FormationScale to -1");
            }
            if (DespawnTime == null || DespawnTime < 0)
            {
                DespawnTime = 600;
                fixes.Add("Corrected DespawnTime to 600");
            }
            if (RespawnTime == null)
            {
                RespawnTime = -1;
                fixes.Add("Corrected RespawnTime to -1");
            }
            if (MinDistRadius == null || MinDistRadius < 0)
            {
                MinDistRadius = 400;
                fixes.Add("Corrected MinDistRadius to 400");
            }
            if (MaxDistRadius == null || MaxDistRadius < 0)
            {
                MaxDistRadius = 1000;
                fixes.Add("Corrected MaxDistRadius to 1000");
            }
            if (DespawnRadius == null || DespawnRadius < 0)
            {
                DespawnRadius = 1100;
                fixes.Add("Corrected DespawnRadius to 1100");
            }
            if (AccuracyMin == null)
            {
                AccuracyMin = -1;
                fixes.Add("Corrected AccuracyMin to -1");
            }
            if (AccuracyMax == null)
            {
                AccuracyMax = -1;
                fixes.Add("Corrected AccuracyMax to -1");
            }
            if (ThreatDistanceLimit == null)
            {
                ThreatDistanceLimit = -1;
                fixes.Add("Corrected ThreatDistanceLimit to -1");
            }
            if (NoiseInvestigationDistanceLimit == null)
            {
                NoiseInvestigationDistanceLimit = -1;
                fixes.Add("Corrected MaxFlankingDistance to -1");
            }
            if (MaxFlankingDistance == null)
            {
                MaxFlankingDistance = -1;
                fixes.Add("Corrected NoiseInvestigationDistanceLimit to -1");
            }
            if (EnableFlankingOutsideCombat == null || (EnableFlankingOutsideCombat != -1 && EnableFlankingOutsideCombat != 0 && EnableFlankingOutsideCombat != 1))
            {
                EnableFlankingOutsideCombat = -1;
                fixes.Add("Corrected EnableFlankingOutsideCombat to -1");
            }
            if (DamageMultiplier == null)
            {
                DamageMultiplier = -1;
                fixes.Add("Corrected DamageMultiplier to -1");
            }
            if (DamageReceivedMultiplier == null)
            {
                DamageReceivedMultiplier = -1;
                fixes.Add("Corrected DamageReceivedMultiplier to -1");
            }
            if(LoadBalancingCategories == null)
            {
                DefaultLoadBalancing();
                fixes.Add("Initialized default LoadBalancingCategories");
            }
            else
            {
                foreach(KeyValuePair<string, BindingList<Loadbalancingcategories>> lbcat in LoadBalancingCategories)
                {
                    //var categoryFixes = category.FixMissingOrInvalidFields();
                    //fixes.AddRange(categoryFixes.Select(f => $"LoadBalancingCategory: {f}"));
                }
            }

            if (Patrols == null)
            {
                DefaultObjectPatrols();
                fixes.Add("Initialized default Patrols");
            }
            else
            {
                for (int i = 0; i < Patrols.Count; i++)
                {
                    var patrolFixes = Patrols[i].FixMissingOrInvalidFields(i);
                    fixes.AddRange(patrolFixes.Select(f => $"Patrol '{Patrols[i].Name}': {f}"));
                }
            }

            return fixes;
        }
        public ExpansionAIPatrolSettings Clone()
        {
            return new ExpansionAIPatrolSettings()
            {
                m_Version = this.m_Version,
                Enabled = this.Enabled,
                FormationScale = this.FormationScale,
                DespawnTime = this.DespawnTime,
                RespawnTime = this.RespawnTime,
                MinDistRadius = this.MinDistRadius,
                MaxDistRadius = this.MaxDistRadius,
                DespawnRadius = this.DespawnRadius,
                AccuracyMin = this.AccuracyMin,
                AccuracyMax = this.AccuracyMax,
                ThreatDistanceLimit = this.ThreatDistanceLimit,
                NoiseInvestigationDistanceLimit = this.NoiseInvestigationDistanceLimit,
                MaxFlankingDistance = this.MaxFlankingDistance,
                EnableFlankingOutsideCombat = this.EnableFlankingOutsideCombat,
                DamageMultiplier = this.DamageMultiplier,
                DamageReceivedMultiplier = this.DamageReceivedMultiplier,

                _LoadBalancingCategories = this._LoadBalancingCategories != null
                    ? new BindingList<Loadbalancingcategorie>(this._LoadBalancingCategories.Select(cat => cat.Clone()).ToList())
                    : null,

                Patrols = this.Patrols != null
                    ? new BindingList<ExpansionAIPatrol>(
                        this.Patrols.Select(p => p.Clone()).ToList()
                    )
                    : null
            };
        }
    }
    public class Loadbalancingcategories
    {
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int MaxPatrols { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is not Loadbalancingcategories other) return false;

            return MinPlayers == other.MinPlayers &&
                   MaxPlayers == other.MaxPlayers &&
                   MaxPatrols == other.MaxPatrols;
        }
        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            return fixes;
        }
        public Loadbalancingcategories Clone()
        {
            return new Loadbalancingcategories()
            {
                MinPlayers = this.MinPlayers,
                MaxPlayers = this.MaxPlayers,
                MaxPatrols = this.MaxPatrols
            };
        }

    }
    public class Loadbalancingcategorie
    {
        public string name { get; set; }
        public BindingList<Loadbalancingcategories> Categorieslist { get; set; }

        public override string ToString()
        {
            return name;
        }
        public override bool Equals(object obj)
        {
            if (obj is not Loadbalancingcategorie other) return false;

            return name == other.name &&
                   Categorieslist.SequenceEqual(other.Categorieslist);
        }
        public Loadbalancingcategorie Clone()
        {
            return new Loadbalancingcategorie()
            {
                name = this.name,
                Categorieslist = new BindingList<Loadbalancingcategories>(this.Categorieslist.Select(c => c.Clone()).ToList())
            };
        }

    }
    public class ExpansionAIPatrol
    {
        public string? Name { get; set; }
        public int? Persist { get; set; }
        public string? Faction { get; set; }
        public string? Formation { get; set; }
        public decimal? FormationScale { get; set; }
        public decimal? FormationLooseness { get; set; }
        public string? Loadout { get; set; }
        public BindingList<string> Units { get; set; }
        public int? NumberOfAI { get; set; }
        public int? NumberOfAIMax { get; set; }
        public string? Behaviour { get; set; }
        public string? LootingBehaviour { get; set; }
        public string? Speed { get; set; }
        public string? UnderThreatSpeed { get; set; }
        public string? DefaultStance { get; set; }
        public decimal? DefaultLookAngle { get; set; }
        public int? CanBeLooted { get; set; }
        public string? LootDropOnDeath { get; set; }
        public int? UnlimitedReload { get; set; }
        public decimal? SniperProneDistanceThreshold { get; set; }
        public decimal? AccuracyMin { get; set; }
        public decimal? AccuracyMax { get; set; }
        public decimal? ThreatDistanceLimit { get; set; }
        public decimal? NoiseInvestigationDistanceLimit { get; set; }
        public decimal? MaxFlankingDistance { get; set; }
        public int? EnableFlankingOutsideCombat { get; set; }
        public decimal? DamageMultiplier { get; set; }
        public decimal? DamageReceivedMultiplier { get; set; }
        public decimal? HeadshotResistance { get; set; }
        public decimal? CanSpawnInContaminatedArea { get; set; }
        public int? CanBeTriggeredByAI { get; set; }
        public decimal? MinDistRadius { get; set; }
        public decimal? MaxDistRadius { get; set; }
        public decimal? DespawnRadius { get; set; }
        public decimal? MinSpreadRadius { get; set; }
        public decimal? MaxSpreadRadius { get; set; }
        public decimal? Chance { get; set; }
        public decimal? DespawnTime { get; set; }
        public decimal? RespawnTime { get; set; }
        public string? LoadBalancingCategory { get; set; } //added versiobn 24
        public string? ObjectClassName { get; set; }
        public string? WaypointInterpolation { get; set; }
        public int? UseRandomWaypointAsStartPoint { get; set; }
        public BindingList<Vec3> Waypoints { get; set; }

        public ExpansionAIPatrol()
        {
        }
        public ExpansionAIPatrol(int bod = 1, string spd = "JOG", string threatspd = "SPRINT", string beh = "ALTERNATE", string fac = "WEST", string loa = "HumanLoadout", bool canbelooted = true, int unlimitedreload = 0, decimal chance = (decimal)1.0, float mindistradius = -2, float maxdistradius = -2, decimal respawntime = -2, decimal wprnd = 0, List<Vec3> way = null)
        {
            Name = "";
            Persist = 0;
            Faction = fac;
            Formation = "";
            FormationScale = (decimal)-1.0;
            FormationLooseness = (decimal)0.0;
            Loadout = loa;
            Units = new BindingList<string>();
            NumberOfAI = bod;
            NumberOfAIMax = 0;
            Behaviour = beh;
            LootingBehaviour = "DEFAULT";
            Speed = spd;
            UnderThreatSpeed = threatspd;
            DefaultStance = "STANDING";
            DefaultLookAngle = (decimal)0.0;
            CanBeLooted = canbelooted == true ? 1 : 0;
            LootDropOnDeath = "";
            UnlimitedReload = unlimitedreload;
            SniperProneDistanceThreshold = (decimal)0.0;
            AccuracyMin = -1;
            AccuracyMax = -1;
            ThreatDistanceLimit = -1;
            NoiseInvestigationDistanceLimit = -1;
            MaxFlankingDistance = -1;
            EnableFlankingOutsideCombat = -1;
            DamageMultiplier = -1;
            DamageReceivedMultiplier = (decimal)-1.0;
            HeadshotResistance = (decimal)0.0;
            CanBeTriggeredByAI = 0;
            CanSpawnInContaminatedArea = 0;
            MinDistRadius = -1;
            MaxDistRadius = -1;
            DespawnRadius = -1;
            MinSpreadRadius = 1;
            MaxSpreadRadius = wprnd;
            Chance = 1;
            WaypointInterpolation = "";
            DespawnTime = -1;
            RespawnTime = respawntime;
            LoadBalancingCategory = "";
            ObjectClassName = "";
            UseRandomWaypointAsStartPoint = 1;
            Waypoints = new BindingList<Vec3>(way);
        }
        public ExpansionAIPatrol(int numberOfAI = 1, string speed = "JOG", string threatSpeed = "SPRINT", string bhv = "ALTERNATE", string faction = "West", string loadout = "", bool canBeLooted = true, int unlimitedReload = 0, decimal chance = (decimal)1.0, decimal minDistRadius = -1, decimal maxDistRadius = -1, string objClassName = "Wreck_UH1Y", List<Vec3> way = null)
        {
            Name = "";
            Persist = 0;
            Faction = faction;
            Formation = "";
            FormationScale = (decimal)-1.0;
            FormationLooseness = (decimal)0.0;
            Loadout = loadout;
            Units = new BindingList<string>();
            NumberOfAI = numberOfAI;
            NumberOfAIMax = 0;
            Behaviour = bhv;
            LootingBehaviour = "DEFAULT";
            Speed = speed;
            UnderThreatSpeed = threatSpeed;
            DefaultStance = "STANDING";
            DefaultLookAngle = (decimal)0.0;
            CanBeLooted = canBeLooted == true ? 1 : 0;
            LootDropOnDeath = "";
            UnlimitedReload = unlimitedReload;
            SniperProneDistanceThreshold = (decimal)0.0;
            AccuracyMin = -1;
            AccuracyMax = -1;
            ThreatDistanceLimit = -1;
            NoiseInvestigationDistanceLimit = -1;
            MaxFlankingDistance = -1;
            EnableFlankingOutsideCombat = -1;
            DamageMultiplier = -1;
            DamageReceivedMultiplier = (decimal)-1.0;
            HeadshotResistance = (decimal)0.0;
            CanBeTriggeredByAI = 0;
            CanSpawnInContaminatedArea = 0;
            MinDistRadius = minDistRadius;
            MaxDistRadius = maxDistRadius;
            DespawnRadius = -1;
            MinSpreadRadius = 5;
            MaxSpreadRadius = 20;
            Chance = chance;
            WaypointInterpolation = "";
            DespawnTime = -1;
            RespawnTime = -2;
            LoadBalancingCategory = "";
            ObjectClassName = objClassName;
            UseRandomWaypointAsStartPoint = 1;
            Waypoints = new BindingList<Vec3>(way.ToList());
        }
        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(object obj)
        {
            if (obj is not ExpansionAIPatrol other) return false;

            bool result = Waypoints.SequenceEqual(other.Waypoints) &&
                   Name == other.Name &&
                   Persist == other.Persist &&
                   Faction == other.Faction &&
                   Formation == other.Formation &&
                   FormationScale == other.FormationScale &&
                   FormationLooseness == other.FormationLooseness &&
                   Loadout == other.Loadout &&
                   Units.SequenceEqual(other.Units) &&
                   NumberOfAI == other.NumberOfAI &&
                   NumberOfAIMax == other.NumberOfAIMax &&  
                   Behaviour == other.Behaviour &&
                   LootingBehaviour == other.LootingBehaviour &&
                   Speed == other.Speed &&
                   UnderThreatSpeed == other.UnderThreatSpeed &&
                   DefaultStance == other.DefaultStance &&
                   DefaultLookAngle == other.DefaultLookAngle &&
                   CanBeLooted == other.CanBeLooted &&
                   LootDropOnDeath == other.LootDropOnDeath &&
                   UnlimitedReload == other.UnlimitedReload &&
                   SniperProneDistanceThreshold == other.SniperProneDistanceThreshold &&
                   AccuracyMin == other.AccuracyMin &&
                   AccuracyMax == other.AccuracyMax &&
                   ThreatDistanceLimit == other.ThreatDistanceLimit &&
                   NoiseInvestigationDistanceLimit == other.NoiseInvestigationDistanceLimit &&
                   MaxFlankingDistance == other.MaxFlankingDistance &&
                   EnableFlankingOutsideCombat == other.EnableFlankingOutsideCombat &&
                   DamageMultiplier == other.DamageMultiplier &&
                   DamageReceivedMultiplier == other.DamageReceivedMultiplier &&
                   HeadshotResistance == other.HeadshotResistance &&
                   CanBeTriggeredByAI == other.CanBeTriggeredByAI &&
                   MinDistRadius == other.MinDistRadius &&
                   MaxDistRadius == other.MaxDistRadius &&
                   DespawnRadius == other.DespawnRadius &&
                   MinSpreadRadius == other.MinSpreadRadius &&
                   MaxSpreadRadius == other.MaxSpreadRadius &&
                   Chance == other.Chance &&
                   DespawnTime == other.DespawnTime &&
                   RespawnTime == other.RespawnTime &&
                   LoadBalancingCategory == other.LoadBalancingCategory &&
                   ObjectClassName == other.ObjectClassName &&
                   WaypointInterpolation == other.WaypointInterpolation &&
                   UseRandomWaypointAsStartPoint == other.UseRandomWaypointAsStartPoint;

            return result;
        }
        public ExpansionAIPatrol Clone()
        {
            return new ExpansionAIPatrol()
            {
                Name = this.Name,
                Persist = this.Persist,
                Faction = this.Faction,
                Formation = this.Formation,
                FormationScale = this.FormationScale,
                FormationLooseness = this.FormationLooseness,
                Loadout = this.Loadout,
                Units = new BindingList<string>(this.Units.Select(x => x).ToList()),
                NumberOfAI = this.NumberOfAI,
                NumberOfAIMax = this.NumberOfAIMax,
                Behaviour = this.Behaviour,
                LootingBehaviour = this.LootingBehaviour,
                Speed = this.Speed,
                UnderThreatSpeed = this.UnderThreatSpeed,
                DefaultStance = this.DefaultStance,
                DefaultLookAngle = this.DefaultLookAngle,
                CanBeLooted = this.CanBeLooted,
                LootDropOnDeath = this.LootDropOnDeath,
                UnlimitedReload = this.UnlimitedReload,
                SniperProneDistanceThreshold = this.SniperProneDistanceThreshold,
                AccuracyMin = this.AccuracyMin,
                AccuracyMax = this.AccuracyMax,
                ThreatDistanceLimit = this.ThreatDistanceLimit,
                NoiseInvestigationDistanceLimit = this.NoiseInvestigationDistanceLimit,
                MaxFlankingDistance = this.MaxFlankingDistance,
                EnableFlankingOutsideCombat = this.EnableFlankingOutsideCombat,
                DamageMultiplier = this.DamageMultiplier,
                DamageReceivedMultiplier = this.DamageReceivedMultiplier,
                HeadshotResistance = this.HeadshotResistance,
                CanBeTriggeredByAI = this.CanBeTriggeredByAI,
                MinDistRadius = this.MinDistRadius,
                MaxDistRadius = this.MaxDistRadius,
                DespawnRadius = this.DespawnRadius,
                MinSpreadRadius = this.MinSpreadRadius,
                MaxSpreadRadius = this.MaxSpreadRadius,
                Chance = this.Chance,
                DespawnTime = this.DespawnTime,
                RespawnTime = this.RespawnTime,
                LoadBalancingCategory = this.LoadBalancingCategory,
                ObjectClassName = this.ObjectClassName,
                WaypointInterpolation = this.WaypointInterpolation,
                UseRandomWaypointAsStartPoint = this.UseRandomWaypointAsStartPoint,
                Waypoints = new BindingList<Vec3>(this.Waypoints.Select(wp => wp.Clone()).ToList())
            };
        }
        public List<string> FixMissingOrInvalidFields(int i)
        {
            var fixes = new List<string>();
            if (Name == null || string.IsNullOrWhiteSpace(Name)) { Name = "Patrol " + i.ToString(); fixes.Add($"Set Name to Patrol {i.ToString()}"); }
            if (Persist == null || (Persist != 0 && Persist != 1)) { Persist = 0; fixes.Add("Corrected Persist to 0");}
            if (Faction == null ) { Faction = "West"; fixes.Add($"Set Faction to West"); }
            if (Formation == null || string.IsNullOrWhiteSpace(Formation)) { Formation = "RANDOM"; fixes.Add($"Set Formation to RANDOM"); }
            if (FormationScale == null || FormationScale < -1) { FormationScale = -1; fixes.Add("Set FormationScale to -1"); }
            if (FormationLooseness == null || FormationLooseness < -1) { FormationLooseness = -1; fixes.Add("Set FormationLooseness to -1"); }
            if (Loadout == null) { Loadout = ""; fixes.Add($"Loadout initialised"); }
            if (Units == null) { Units = new BindingList<string>(); fixes.Add("Initialized empty Units list"); }
            if (NumberOfAI == null) { NumberOfAI = 3; fixes.Add("Set NumberOfAI to 3"); }
            if (NumberOfAIMax == null) { NumberOfAIMax = 0; fixes.Add("Set NumberOfAIMax to 0"); }
            if (Behaviour == null || string.IsNullOrWhiteSpace(Behaviour)) { Behaviour = "ALTERNATE"; fixes.Add($"Set Behaviour to ALTERNATE"); }
            if (LootingBehaviour == null || string.IsNullOrWhiteSpace(LootingBehaviour)) { LootingBehaviour = "DEFAULT"; fixes.Add($"Set LootingBehaviour to DEFAULT"); }
            if (DefaultStance == null || string.IsNullOrWhiteSpace(DefaultStance) || (DefaultStance != "STANDING" && DefaultStance != "CROUCHED" && DefaultStance != "PRONE")) { DefaultStance = "STANDING"; fixes.Add($"Set DefaultStance to STANDING"); }
            if (DefaultLookAngle == null) { DefaultLookAngle = 0; fixes.Add("Set DefaultLookAngle to 0"); }
            if (Speed == null || string.IsNullOrWhiteSpace(Speed)) { Speed = "JOG"; fixes.Add($"Set Speed to JOG"); }
            if (UnderThreatSpeed == null || string.IsNullOrWhiteSpace(UnderThreatSpeed)) { UnderThreatSpeed = "SPRINT"; fixes.Add($"Set UnderThreatSpeed to SPRINT"); }
            if (CanBeLooted == null || (CanBeLooted != 0 && CanBeLooted != 1)) { CanBeLooted = 1; fixes.Add("Corrected CanBeLooted to 1"); }
            if (LootDropOnDeath == null) { LootDropOnDeath = ""; fixes.Add($"initilised LootDropOnDeath"); }
            if (UnlimitedReload == null) { UnlimitedReload = 0; fixes.Add($"Set UnlimitedReload to 0"); }
            if (SniperProneDistanceThreshold == null) { SniperProneDistanceThreshold = 500; fixes.Add($"Set SniperProneDistanceThreshold to 500"); }
            if (AccuracyMin == null || AccuracyMin < -1) { AccuracyMin = -1; fixes.Add("Set AccuracyMin to -1"); }
            if (AccuracyMax == null || AccuracyMax < -1) { AccuracyMax = -1; fixes.Add("Set AccuracyMax to -1"); }
            if (ThreatDistanceLimit == null || ThreatDistanceLimit < -1) { ThreatDistanceLimit = -1; fixes.Add("Set ThreatDistanceLimit to -1"); }
            if (NoiseInvestigationDistanceLimit == null) { NoiseInvestigationDistanceLimit = -1; fixes.Add("Set NoiseInvestigationDistanceLimit to -1"); }
            if (MaxFlankingDistance == null || MaxFlankingDistance < -1) { MaxFlankingDistance = -1; fixes.Add("Set MaxFlankingDistance to -1"); }
            if (EnableFlankingOutsideCombat == null || (EnableFlankingOutsideCombat != -1 && EnableFlankingOutsideCombat != 0 && EnableFlankingOutsideCombat != 1)) { EnableFlankingOutsideCombat = -1; fixes.Add("Set EnableFlankingOutsideCombat to -1"); }
            if (DamageMultiplier == null || DamageMultiplier < -1) { DamageMultiplier = -1; fixes.Add("Set DamageMultiplier to -1"); }
            if (DamageReceivedMultiplier == null || DamageReceivedMultiplier < -1) { DamageReceivedMultiplier = -1; fixes.Add("Set DamageReceivedMultiplier to -1"); }
            if (HeadshotResistance == null) { HeadshotResistance = 0; fixes.Add("Set HeadshotResistance to 0"); }
            if (CanBeTriggeredByAI == null || (CanBeTriggeredByAI != 0 && CanBeTriggeredByAI != 1)) { CanBeTriggeredByAI = 0; fixes.Add("Corrected CanBeTriggeredByAI to 0"); }
            if (CanSpawnInContaminatedArea == null || (CanSpawnInContaminatedArea != 0 && CanSpawnInContaminatedArea != 1)) { CanSpawnInContaminatedArea = 0; fixes.Add("Corrected CanSpawnInContaminatedArea to 0"); }
            if (MinDistRadius == null || MinDistRadius < -1) { MinDistRadius = -1; fixes.Add("Set MinDistRadius to -1"); }
            if (MaxDistRadius == null || MaxDistRadius < -1) { MaxDistRadius = -1; fixes.Add("Set MaxDistRadius to -1"); }
            if (DespawnRadius == null || DespawnRadius < -1) { DespawnRadius = -1; fixes.Add("Set DespawnRadius to -1"); }
            if (MinSpreadRadius == null || MinSpreadRadius < -1) { MinSpreadRadius = -1; fixes.Add("Set MinSpreadRadius to -1"); }
            if (MaxSpreadRadius == null || MaxSpreadRadius < -1) { MaxSpreadRadius = -1; fixes.Add("Set MaxSpreadRadius to -1"); }
            if (Chance == null) { Chance = 1; fixes.Add("Set Chance to 1"); }
            if (DespawnTime == null || DespawnTime < -1) { DespawnTime = -1; fixes.Add("Set DespawnTime to -1"); }
            if (RespawnTime == null || RespawnTime < -2) { RespawnTime = -2; fixes.Add("Set RespawnTime to -2"); }
            if (LoadBalancingCategory == null) { LoadBalancingCategory = ""; fixes.Add($"initilised LoadBalancingCategory"); }
            if (ObjectClassName == null) { ObjectClassName = ""; fixes.Add($"initilised ObjectClassName"); }
            if (WaypointInterpolation == null) { WaypointInterpolation = ""; fixes.Add($"initilised WaypointInterpolation"); }
            if (UseRandomWaypointAsStartPoint == null || (UseRandomWaypointAsStartPoint != 0 && UseRandomWaypointAsStartPoint != 1)) { UseRandomWaypointAsStartPoint = 0; fixes.Add("Corrected UseRandomWaypointAsStartPoint to 0"); }
            if (Waypoints == null ) { Waypoints = new BindingList<Vec3>(); fixes.Add("Initialized empty waypoints list"); }
            return fixes;
        }
    }
}
