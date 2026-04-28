using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionQuestObjectiveAICampConfig : ExpansionQuestObjectiveConfig
    {
        [JsonPropertyOrder(10)]
        public decimal? InfectedDeletionRadius { get; set; }
        [JsonPropertyOrder(11)]
        public decimal? MaxDistance { get; set; }
        [JsonPropertyOrder(12)]
        public decimal? MinDistance { get; set; }
        [JsonPropertyOrder(13)]
        public BindingList<string>? AllowedWeapons { get; set; }
        [JsonPropertyOrder(14)]
        public BindingList<string>? AllowedDamageZones { get; set; }
        [JsonPropertyOrder(15)]
        public BindingList<ExpansionAIPatrol>? AISpawns { get; set; }

        public override ExpansionQuestObjectiveConfig Clone()
        {
            ExpansionQuestObjectiveAICampConfig clone = new ExpansionQuestObjectiveAICampConfig
            {
                ConfigVersion = ConfigVersion,
                ID = ID,
                ObjectiveType = ObjectiveType,
                ObjectiveText = ObjectiveText,
                TimeLimit = TimeLimit,
                Active = Active,

                InfectedDeletionRadius = InfectedDeletionRadius,
                MaxDistance = MaxDistance,
                MinDistance = MinDistance,
                AllowedWeapons = AllowedWeapons != null
                    ? new BindingList<string>(AllowedWeapons.ToList())
                    : null,

                AllowedDamageZones = AllowedDamageZones != null
                    ? new BindingList<string>(AllowedDamageZones.ToList())
                    : null,

                AISpawns = AISpawns != null
                    ? new BindingList<ExpansionAIPatrol>(AISpawns.Select(x => x.Clone()).ToList())
                    : null,

                
            };
            clone.SetPath(_path);
            clone.SetGuid(Id);
            return clone;
        }
        protected override bool EqualsCore(ExpansionQuestObjectiveConfig other)
        {
            var o = (ExpansionQuestObjectiveAICampConfig)other;

            if (InfectedDeletionRadius != o.InfectedDeletionRadius)
                return false;
            if (MaxDistance != o.MaxDistance)
                return false;
            if (MinDistance != o.MinDistance)
                return false;

            if (!ListEquals(AllowedWeapons, o.AllowedWeapons))
                return false;

            if (!ListEquals(AllowedDamageZones, o.AllowedDamageZones))
                return false;

            if (!ListEquals(AISpawns, o.AISpawns))
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

            if (InfectedDeletionRadius == null || (InfectedDeletionRadius.HasValue && InfectedDeletionRadius < 0))
            {
                InfectedDeletionRadius = 0;
                fixes.Add("Clamped InfectedDeletionRadius to 0");
            }
            if (MaxDistance == null || (MaxDistance.HasValue && MaxDistance < -1))
            {
                MaxDistance = -1;
                fixes.Add("Clamped ExecutionAmount to -1");
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
            if (AISpawns == null)
            {
                AISpawns = new BindingList<ExpansionAIPatrol>();
                fixes.Add("Initialised AISpawns");
            }
            else
            {
                for (int i = 0; i < AISpawns.Count; i++)
                {
                    var patrolFixes = AISpawns[i].FixMissingOrInvalidFields(i);
                    fixes.AddRange(patrolFixes.Select(f => $"Patrol '{AISpawns[i].Name}[{i.ToString()}]': {f}"));
                }
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
            foreach (ExpansionAIPatrol pat in AISpawns)
            {
                TreeNode PatrolRoot = new TreeNode(pat.Name)
                {
                    Tag = pat
                };
                CreatePatrolNodes(pat, PatrolRoot);
                AIAPatrolsNode.Nodes.Add(PatrolRoot);
            }
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
            Root.Nodes.Add(UnitsNode);
        }
    }
}
