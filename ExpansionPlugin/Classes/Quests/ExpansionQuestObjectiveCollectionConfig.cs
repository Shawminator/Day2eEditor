using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionQuestObjectiveCollectionConfig : ExpansionQuestObjectiveConfig
    {
        [JsonPropertyOrder(10)]
        public BindingList<ExpansionQuestObjectiveDelivery>? Collections { get; set; }
        [JsonPropertyOrder(11)]
        public int? ShowDistance { get; set; }
        [JsonPropertyOrder(12)]
        public int? AddItemsToNearbyMarketZone { get; set; }
        [JsonPropertyOrder(13)]
        public int? NeedAnyCollection { get; set; }

        public override ExpansionQuestObjectiveConfig Clone()
        {
            return new ExpansionQuestObjectiveCollectionConfig
            {
                ConfigVersion = ConfigVersion,
                ID = ID,
                ObjectiveType = ObjectiveType,
                ObjectiveText = ObjectiveText,
                TimeLimit = TimeLimit,
                Active = Active,

                Collections = Collections != null
                    ? new BindingList<ExpansionQuestObjectiveDelivery>(Collections.Select(x => x.Clone()).ToList())
                    : null,

                ShowDistance = ShowDistance,
                AddItemsToNearbyMarketZone = AddItemsToNearbyMarketZone,
                NeedAnyCollection = NeedAnyCollection
            };
        }
        protected override bool EqualsCore(ExpansionQuestObjectiveConfig other)
        {
            var o = (ExpansionQuestObjectiveCollectionConfig)other;

            if (!ListEquals(Collections, o.Collections))
                return false;

            if (ShowDistance != o.ShowDistance ||
                AddItemsToNearbyMarketZone != o.AddItemsToNearbyMarketZone ||
                NeedAnyCollection != o.NeedAnyCollection)
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

            if (Collections == null)
            {
                Collections = new BindingList<ExpansionQuestObjectiveDelivery>();
                fixes.Add("Initialised Collections");
            }
            for (int i = 0; i < Collections.Count; i++)
            {
                var CollectionsFixes = Collections[i].FixMissingOrInvalidFields(i);
                fixes.AddRange(CollectionsFixes.Select(f => $"Collections '{Collections[i].ClassName}': {f}"));
            }
            if (ShowDistance is null || (ShowDistance != 0 && ShowDistance != 1))
            {
                ShowDistance = 0;
                fixes.Add("Normalised ShowDistance to 0 (valid values: 0 or 1)");
            }
            if (AddItemsToNearbyMarketZone is null || (AddItemsToNearbyMarketZone != 0 && AddItemsToNearbyMarketZone != 1))
            {
                AddItemsToNearbyMarketZone = 0;
                fixes.Add("Normalised AddItemsToNearbyMarketZone to 0 (valid values: 0 or 1)");
            }
            if (NeedAnyCollection is null || (NeedAnyCollection != 0 && NeedAnyCollection != 1))
            {
                NeedAnyCollection = 0;
                fixes.Add("Normalised NeedAnyCollection to 0 (valid values: 0 or 1)");
            }
            return fixes;
        }
    }
}
