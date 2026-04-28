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
        [JsonPropertyOrder(12)]
        public decimal? MinDistance { get; set; }
        [JsonPropertyOrder(13)]
        public int? Amount { get; set; }
        [JsonPropertyOrder(14)]
        public BindingList<string> ClassNames { get; set; }
        [JsonPropertyOrder(15)]
        public int? CountSelfKill { get; set; }
        [JsonPropertyOrder(16)]
        public BindingList<string> AllowedWeapons { get; set; }
        [JsonPropertyOrder(17)]
        public BindingList<string> ExcludedClassNames { get; set; }
        [JsonPropertyOrder(18)]
        public int? CountAIPlayers { get; set; }
        [JsonPropertyOrder(19)]
        public BindingList<string> AllowedTargetFactions { get; set; }
        [JsonPropertyOrder(20)]
        public BindingList<string> AllowedDamageZones { get; set; }


        public override ExpansionQuestObjectiveConfig Clone()
        {
            ExpansionQuestObjectiveTargetConfig clone = new ExpansionQuestObjectiveTargetConfig
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
            clone.SetPath(_path);
            clone.SetGuid(Id);
            return clone;
        }
        protected override bool EqualsCore(ExpansionQuestObjectiveConfig other)
        {
            var o = (ExpansionQuestObjectiveTargetConfig)other;

            if (!Equals(Position, o.Position))
                return false;

            if (MaxDistance != o.MaxDistance)
                return false;

            if (MinDistance != o.MinDistance)
                return false;

            if (Amount != o.Amount)
                return false;

            if (!ListEquals(ClassNames, o.ClassNames))
                return false;

            if (CountSelfKill != o.CountSelfKill)
                return false;

            if (!ListEquals(AllowedWeapons, o.AllowedWeapons))
                return false;

            if (!ListEquals(ExcludedClassNames, o.ExcludedClassNames))
                return false;

            if (CountAIPlayers != o.CountAIPlayers)
                return false;

            if (!ListEquals(AllowedTargetFactions, o.AllowedTargetFactions))
                return false;

            if (!ListEquals(AllowedDamageZones, o.AllowedDamageZones))
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

            if (MaxDistance == null || (MaxDistance.HasValue && MaxDistance < -1))
            {
                MaxDistance = -1;
                fixes.Add("Clamped MaxDistance to -1");
            }

            if (MinDistance == null || (MinDistance.HasValue && MinDistance < -1))
            {
                MinDistance = -1;
                fixes.Add("Clamped MinDistance to -1");
            }

            if (MinDistance > MaxDistance)
            {
                (MinDistance, MaxDistance) = (MaxDistance, MinDistance);
                fixes.Add("Swapped MinDistance and MaxDistance");
            }

            if (Amount == null || (Amount.HasValue && Amount < 0))
            {
                Amount = 0;
                fixes.Add("Clamped Amount to 0");
            }
            if (CountSelfKill is null || (CountSelfKill != 0 && CountSelfKill != 1))
            {
                CountSelfKill = 0;
                fixes.Add("Normalised CountSelfKill to 0 (valid values: 0 or 1)");
            }

            if (CountAIPlayers is null || (CountAIPlayers != 0 && CountAIPlayers != 1))
            {
                CountAIPlayers = 0;
                fixes.Add("Normalised CountAIPlayers to 0 (valid values: 0 or 1)");
            }


            if (ClassNames == null)
            {
                ClassNames = new BindingList<string>();
                fixes.Add("Initialised ClassNames");
            }

            if (AllowedWeapons == null)
            {
                AllowedWeapons = new BindingList<string>();
                fixes.Add("Initialised AllowedWeapons");
            }

            if (ExcludedClassNames == null)
            {
                ExcludedClassNames = new BindingList<string>();
                fixes.Add("Initialised ExcludedClassNames");
            }

            if (AllowedTargetFactions == null)
            {
                AllowedTargetFactions = new BindingList<string>();
                fixes.Add("Initialised AllowedTargetFactions");
            }

            if (AllowedDamageZones == null)
            {
                AllowedDamageZones = new BindingList<string>();
                fixes.Add("Initialised AllowedDamageZones");
            }

            return fixes;
        }

        internal override void AddSpecificCategoryNodes(TreeNode categoryNode)
        {
            
        }
    }
}
