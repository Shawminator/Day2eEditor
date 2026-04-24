using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionQuestItemConfig : IDeepCloneable<ExpansionQuestItemConfig>, IEquatable<ExpansionQuestItemConfig>
    {
        public string? ClassName { get; set; }
        public int? Amount { get; set; }

        public ExpansionQuestItemConfig Clone()
        {
            return new ExpansionQuestItemConfig()
            {
                ClassName = this.ClassName,
                Amount = this.Amount
            };
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionQuestItemConfig);
        public bool Equals(ExpansionQuestItemConfig? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (ClassName != other.ClassName) return false;
            if(Amount != other.Amount) return false;

            return true;
        }
    }
}
