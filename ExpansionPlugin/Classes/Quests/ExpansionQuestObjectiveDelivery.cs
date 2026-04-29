using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{ 
    public class ExpansionQuestObjectiveDelivery : IDeepCloneable<ExpansionQuestObjectiveDelivery>, IEquatable<ExpansionQuestObjectiveDelivery>
    {
        public int? Amount { get; set; }
        public string? ClassName { get; set; }
        public int? QuantityPercent { get; set; }
        public int? MinQuantityPercent { get; set; }

        public ExpansionQuestObjectiveDelivery Clone()
        {
            return new ExpansionQuestObjectiveDelivery
            {
                Amount = Amount,
                ClassName = ClassName,
                QuantityPercent = QuantityPercent,
                MinQuantityPercent = MinQuantityPercent
            };
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionQuestObjectiveDelivery);
        public bool Equals(ExpansionQuestObjectiveDelivery? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (Amount != other.Amount)
                return false;
            if (ClassName != other.ClassName)
                return false;
            if (QuantityPercent != other.QuantityPercent)
                return false;
            if (MinQuantityPercent != other.MinQuantityPercent)
                return false;

            return true;
        }

        internal IEnumerable<object> FixMissingOrInvalidFields(int i)
        {
            var fixes = new List<string>();

            if (Amount == null || (Amount.HasValue && Amount < 1))
            {
                Amount = 0;
                fixes.Add("Clamped Amount to 1");
            }
            if (ClassName == null)
            {
                ClassName = "ChangeMe";
                fixes.Add("Set ClassName to ChangeMe");
            }
            if (QuantityPercent == null || (QuantityPercent.HasValue && QuantityPercent < -1))
            {
                QuantityPercent = -1;
                fixes.Add("Clamped QuantityPercent to -1");
            }

            if (MinQuantityPercent == null || (MinQuantityPercent.HasValue && MinQuantityPercent < -1))
            {
                MinQuantityPercent = -1;
                fixes.Add("Clamped MinQuantityPercent to -1");
            }

            return fixes;
        }
    }
}
