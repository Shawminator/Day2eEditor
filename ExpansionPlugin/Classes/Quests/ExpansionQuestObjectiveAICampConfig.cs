using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            return new ExpansionQuestObjectiveAICampConfig
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
    }
}
