using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionQuestObjectiveCraftingConfig : ExpansionQuestObjectiveConfig
    {
        [JsonPropertyOrder(10)]
        public BindingList<string>? ItemNames { get; set; }
        [JsonPropertyOrder(11)]
        public int? ExecutionAmount { get; set; }

        public override ExpansionQuestObjectiveConfig Clone()
        {
            ExpansionQuestObjectiveCraftingConfig clone = new ExpansionQuestObjectiveCraftingConfig
            {
                ConfigVersion = ConfigVersion,
                ID = ID,
                ObjectiveType = ObjectiveType,
                ObjectiveText = ObjectiveText,
                TimeLimit = TimeLimit,
                Active = Active,

                ItemNames = ItemNames != null
                    ? new BindingList<string>(ItemNames.ToList())
                    : null,

                ExecutionAmount = ExecutionAmount
            };
            clone.SetPath(_path);
            clone.SetGuid(Id);
            return clone;
        }
        protected override bool EqualsCore(ExpansionQuestObjectiveConfig other)
        {
            var o = (ExpansionQuestObjectiveCraftingConfig)other;

            if (!ListEquals(ItemNames, o.ItemNames))
                return false;

            if (ExecutionAmount != o.ExecutionAmount )
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

            if (ItemNames == null)
            {
                ItemNames = new BindingList<string>();
                fixes.Add("Initialised Collections");
            }
            if (ExecutionAmount == null || (ExecutionAmount.HasValue && ExecutionAmount < 1))
            {
                ExecutionAmount = 1;
                fixes.Add("Clamped MarkerName to 1");
            }
            return fixes;
        }

        internal override void AddSpecificCategoryNodes(TreeNode categoryNode)
        {
            
        }
    }
}
