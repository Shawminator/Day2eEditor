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
    public class ExpansionQuestObjectiveActionConfig : ExpansionQuestObjectiveConfig
    {
        [JsonPropertyOrder(10)]
        public BindingList<string> ActionNames { get; set; }
        [JsonPropertyOrder(11)]
        public BindingList<string> AllowedClassNames { get; set; }
        [JsonPropertyOrder(12)]
        public BindingList<string> ExcludedClassNames { get; set; }
        [JsonPropertyOrder(13)]
        public int? ExecutionAmount { get; set; }
        public override ExpansionQuestObjectiveConfig Clone()
        {
            return new ExpansionQuestObjectiveActionConfig
            {
                ConfigVersion = ConfigVersion,
                ID = ID,
                ObjectiveType = ObjectiveType,
                ObjectiveText = ObjectiveText,
                TimeLimit = TimeLimit,
                Active = Active,

                ActionNames = ActionNames != null
                    ? new BindingList<string>(ActionNames.ToList())
                    : null,

                AllowedClassNames = AllowedClassNames != null
                    ? new BindingList<string>(AllowedClassNames.ToList())
                    : null,

                ExcludedClassNames = ExcludedClassNames != null
                    ? new BindingList<string>(ExcludedClassNames.ToList())
                    : null,

                ExecutionAmount = ExecutionAmount,
            };
        }
        protected override bool EqualsCore(ExpansionQuestObjectiveConfig other)
        {
            var o = (ExpansionQuestObjectiveActionConfig)other;

            if (!ListEquals(ActionNames, o.ActionNames))
                return false;

            if (!ListEquals(AllowedClassNames, o.AllowedClassNames))
                return false;

            if (!ListEquals(ExcludedClassNames, o.ExcludedClassNames))
                return false;

            if (ExecutionAmount != o.ExecutionAmount)
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

            if (ActionNames == null)
            {
                ActionNames = new BindingList<string>();
                fixes.Add("Initialised ActionNames");
            }
            if (AllowedClassNames == null)
            {
                AllowedClassNames = new BindingList<string>();
                fixes.Add("Initialised AllowedClassNames");
            }
            if (ExcludedClassNames == null)
            {
                ExcludedClassNames = new BindingList<string>();
                fixes.Add("Initialised ExcludedClassNames");
            }

            if (ExecutionAmount == null || (ExecutionAmount.HasValue && ExecutionAmount < 0))
            {
                ExecutionAmount = 0;
                fixes.Add("Clamped ExecutionAmount to 0");
            }
            return fixes;
        }
    }
}
