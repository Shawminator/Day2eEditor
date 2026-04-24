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
    public class ExpansionQuestObjectiveTargetConfig : ExpansionQuestObjectiveConfig
    {
        [JsonPropertyOrder(10)]
        public Vec3 Position { get; set; }
        [JsonPropertyOrder(11)]
        public decimal? MaxDistance { get; set; }
        [JsonPropertyOrder(10)]
        public decimal? MinDistance { get; set; }
        [JsonPropertyOrder(10)]
        public int? Amount { get; set; }
        [JsonPropertyOrder(10)]
        public BindingList<string> ClassNames { get; set; }
        [JsonPropertyOrder(10)]
        public int? CountSelfKill { get; set; }
        [JsonPropertyOrder(10)]
        public BindingList<string> AllowedWeapons { get; set; }
        [JsonPropertyOrder(10)]
        public BindingList<string> ExcludedClassNames { get; set; }
        [JsonPropertyOrder(10)]
        public int? CountAIPlayers { get; set; }
        [JsonPropertyOrder(10)]
        public BindingList<string> AllowedTargetFactions { get; set; }
        [JsonPropertyOrder(10)]
        public BindingList<string> AllowedDamageZones { get; set; }


        public override ExpansionQuestObjectiveConfig Clone()
        {
            return new ExpansionQuestObjectiveTargetConfig
            {
                ConfigVersion = ConfigVersion,
                ID = ID,
                ObjectiveType = ObjectiveType,
                ObjectiveText = ObjectiveText,
                TimeLimit = TimeLimit,
                Active = Active,

                Position = Position != null ? Position.Clone() : null,
                MaxDistance = MaxDistance,
                MinDistance = MinDistance,
                Amount = Amount,

                ClassNames = ClassNames != null
                    ? new BindingList<string>(ClassNames.ToList())
                    : null,

                CountSelfKill = CountSelfKill,

                AllowedWeapons = AllowedWeapons != null
                    ? new BindingList<string>(AllowedWeapons.ToList())
                    : null,

                ExcludedClassNames = ExcludedClassNames != null
                    ? new BindingList<string>(ExcludedClassNames.ToList())
                    : null,

                CountAIPlayers = CountAIPlayers,

                AllowedTargetFactions = AllowedTargetFactions != null
                    ? new BindingList<string>(AllowedTargetFactions.ToList())
                    : null,

                AllowedDamageZones = AllowedDamageZones != null
                    ? new BindingList<string>(AllowedDamageZones.ToList())
                    : null
            };
        }
        public override bool Equals(object? obj)
        {
            if (!base.Equals(obj))
                return false;

            var other = obj as ExpansionQuestObjectiveTargetConfig;
            if (other is null)
                return false;

            if (!Equals(Position, other.Position))
                return false;

            if (MaxDistance != other.MaxDistance)
                return false;

            if (MinDistance != other.MinDistance)
                return false;

            if (Amount != other.Amount)
                return false;

            if (!ListEquals(ClassNames, other.ClassNames))
                return false;

            if (CountSelfKill != other.CountSelfKill)
                return false;

            if (!ListEquals(AllowedWeapons, other.AllowedWeapons))
                return false;

            if (!ListEquals(ExcludedClassNames, other.ExcludedClassNames))
                return false;

            if (CountAIPlayers != other.CountAIPlayers)
                return false;

            if (!ListEquals(AllowedTargetFactions, other.AllowedTargetFactions))
                return false;

            if (!ListEquals(AllowedDamageZones, other.AllowedDamageZones))
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

            if (Position == null)
            {
                Position = new Vec3();
                fixes.Add("Initialised Position");
            }

            return fixes;
        }

    }
}
