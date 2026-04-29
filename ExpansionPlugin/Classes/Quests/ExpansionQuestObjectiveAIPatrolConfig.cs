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
    public class ExpansionQuestObjectiveAIPatrolConfig : ExpansionQuestObjectiveConfig
    {
        [JsonPropertyOrder(10)]
        public decimal? MaxDistance { get; set; }
        [JsonPropertyOrder(11)]
        public decimal? MinDistance { get; set; }
        [JsonPropertyOrder(12)]
        public BindingList<string>? AllowedWeapons { get; set; }
        [JsonPropertyOrder(13)]
        public BindingList<string>? AllowedDamageZones { get; set; }
        [JsonPropertyOrder(14)]
        public ExpansionAIPatrol? AISpawn { get; set; }

        public override ExpansionQuestObjectiveConfig Clone()
        {
            ExpansionQuestObjectiveAIPatrolConfig clone = new ExpansionQuestObjectiveAIPatrolConfig
            {
                ConfigVersion = ConfigVersion,
                ID = ID,
                ObjectiveType = ObjectiveType,
                ObjectiveText = ObjectiveText,
                TimeLimit = TimeLimit,
                Active = Active,


                MinDistance = MinDistance,
                MaxDistance = MaxDistance,

                AllowedWeapons = AllowedWeapons != null
                    ? new BindingList<string>(AllowedWeapons.ToList())
                    : null,

                AllowedDamageZones = AllowedDamageZones != null
                    ? new BindingList<string>(AllowedDamageZones.ToList())
                    : null,

                AISpawn = AISpawn.Clone()
            };
            clone.SetPath(_path);
            clone.SetGuid(Id);
            return clone;
        }
        protected override bool EqualsCore(ExpansionQuestObjectiveConfig other)
        {
            var o = (ExpansionQuestObjectiveAIPatrolConfig)other;

            if (MinDistance != o.MinDistance)
                return false;
            if (MaxDistance != o.MaxDistance)
                return false;

            if (!ListEquals(AllowedWeapons, o.AllowedWeapons))
                return false;

            if (!ListEquals(AllowedDamageZones, o.AllowedDamageZones))
                return false;

            if (!AISpawn.Equals(o.AISpawn))
                return false;

            return true;
        }
        private static bool ListEquals<T>(IList<T>? a, IList<T>? b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (a is null || b is null)
                return false;

            if (a.Count != b.Count)
                return false;

            for (int i = 0; i < a.Count; i++)
            {
                if (!Equals(a[i], b[i]))
                    return false;
            }

            return true;
        }
        internal override IEnumerable<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            fixes.AddRange(base.FixMissingOrInvalidFields());

            if (MaxDistance == null || (MaxDistance.HasValue && MaxDistance < -1))
            {
                MaxDistance = 0;
                fixes.Add("Clamped MaxDistance to -1");
            }
            if (MinDistance == null || (MinDistance.HasValue && MinDistance < -1))
            {
                MinDistance = 0;
                fixes.Add("Clamped MinDistance to -1");
            }

            if (AllowedWeapons == null)
            {
                AllowedWeapons = new BindingList<string>();
                fixes.Add("Initialised AllowedWeapons");
            }
            if (AllowedDamageZones == null)
            {
                AllowedDamageZones = new BindingList<string>();
                fixes.Add("Initialised AllowedDamageZones");
            }
            if (AISpawn == null)
            {
                AISpawn = new ExpansionAIPatrol();
                fixes.Add("Initialised AISpawn");
            }
            else
            {
                var patrolFixes = AISpawn.FixMissingOrInvalidFields(0);
                fixes.AddRange(patrolFixes.Select(f => $"Patrol '{AISpawn.Name}[0]': {f}"));
            }

            return fixes;
        }

        internal override void AddSpecificCategoryNodes(TreeNode categoryNode)
        {
            categoryNode.Nodes.Add(new TreeNode("General")
            {
                Tag = new ObjectiveNodeTag(this, ObjectiveNodeKind.SpecificConfig)
            });
            TreeNode AIAPatrolsNode = new TreeNode("AI Spawns")
            {
                Tag = "AIPatrols"
            };
            TreeNode PatrolRoot = new TreeNode(AISpawn.Name)
            {
                Tag = AISpawn
            };
            CreatePatrolNodes(AISpawn, PatrolRoot);
            AIAPatrolsNode.Nodes.Add(PatrolRoot);
            categoryNode.Nodes.Add(AIAPatrolsNode);
            TreeNode AllowedWeaponsNode = new TreeNode("Allowed Weapons")
            {
                Tag = "ObjectivesAICAllowedWeapons",
            };
            foreach (string AllowedWeapon in AllowedWeapons)
            {
                AllowedWeaponsNode.Nodes.Add(new TreeNode(AllowedWeapon)
                {
                    Tag = "bjectivesAICAllowedWeapon"
                });
            }
            categoryNode.Nodes.Add(AllowedWeaponsNode);
            TreeNode AllowedDamageZonesNode = new TreeNode("Allowed Damage Zones")
            {
                Tag = "ObjectivesAICAllowedDamageZones",
            };
            foreach (string AllowedDamageZone in AllowedDamageZones)
            {
                AllowedDamageZonesNode.Nodes.Add(new TreeNode(AllowedDamageZone)
                {
                    Tag = "bjectivesAICAllowedDamageZone"
                });
            }
            categoryNode.Nodes.Add(AllowedDamageZonesNode);
        }
        private void CreatePatrolNodes(ExpansionAIPatrol pat, TreeNode Root)
        {
            Root.Nodes.Add(new TreeNode("General")
            {
                Tag = "AIPatrolGeneral"
            });
            TreeNode WaypointsNode = new TreeNode("WayPoints")
            {
                Tag = "AIPatrolWayPoints"
            };
            foreach (Vec3 v3 in pat.Waypoints)
            {
                WaypointsNode.Nodes.Add(new TreeNode(v3.GetString())
                {
                    Tag = v3
                });
            }
            Root.Nodes.Add(WaypointsNode);
            TreeNode UnitsNode = new TreeNode("Units")
            {
                Tag = "AIPatrolUnits"
            };
            foreach (string s in pat.Units)
            {
                UnitsNode.Nodes.Add(new TreeNode(s)
                {
                    Tag = "AIPatrolsUnit"
                });

            }
        }
    }
}
