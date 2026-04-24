using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionQuestRewardConfig : IDeepCloneable<ExpansionQuestRewardConfig>, IEquatable<ExpansionQuestRewardConfig>
    {
        public string? ClassName { get; set; }
        public int? Amount { get; set; }
        public BindingList<string> Attachments { get; set; }
        public int? DamagePercent { get; set; }
        public int? HealthPercent { get; set; }
        public int? QuestID { get; set; }
        public decimal? Chance { get; set; }

        public ExpansionQuestRewardConfig Clone()
        {
            return new ExpansionQuestRewardConfig()
            {
                ClassName = ClassName,
                Amount = Amount,
                Attachments = Attachments != null
                        ? new BindingList<string>(Attachments.ToList())
                        : null,
                DamagePercent = DamagePercent,
                HealthPercent = HealthPercent,
                QuestID = QuestID,
                Chance = Chance
            };
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionQuestRewardConfig);
        public bool Equals(ExpansionQuestRewardConfig? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (!Equals(ClassName, other.ClassName))
                return false;

            if (Amount != other.Amount)
                return false;

            if (!ListEquals(Attachments, other.Attachments))
                return false;

            if (DamagePercent != other.DamagePercent)
                return false;

            if (HealthPercent != other.HealthPercent)
                return false;

            if (QuestID != other.QuestID)
                return false;

            if (Chance != other.Chance)
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
    }
}
