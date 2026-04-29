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
    public class ExpansionQuestObjectiveDeliveryConfig : ExpansionQuestObjectiveConfig
    {
        [JsonPropertyOrder(10)]
        public BindingList<ExpansionQuestObjectiveDelivery>? Collections { get; set; }
        [JsonPropertyOrder(11)]
        public int? ShowDistance { get; set; }
        [JsonPropertyOrder(12)]
        public int? AddItemsToNearbyMarketZone { get; set; }
        [JsonPropertyOrder(13)]
        public decimal? MaxDistance { get; set; }
        [JsonPropertyOrder(14)]
        public string? MarkerName { get; set; }

        public override ExpansionQuestObjectiveConfig Clone()
        {
            ExpansionQuestObjectiveDeliveryConfig clone = new ExpansionQuestObjectiveDeliveryConfig
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
                MaxDistance = MaxDistance,
                MarkerName = MarkerName
            };
            clone.SetPath(_path);
            clone.SetGuid(Id);
            return clone;
        }
        protected override bool EqualsCore(ExpansionQuestObjectiveConfig other)
        {
            var o = (ExpansionQuestObjectiveDeliveryConfig)other;

            if (!ListEquals(Collections, o.Collections))
                return false;

            if (ShowDistance != o.ShowDistance ||
                AddItemsToNearbyMarketZone != o.AddItemsToNearbyMarketZone ||
                MaxDistance != o.MaxDistance ||
                MarkerName != o.MarkerName)
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
            if (MaxDistance == null)
            {
                MaxDistance = 20;
                fixes.Add("Clamped MaxDistance to 20");
            }
            if (MarkerName == null)
            {
                MarkerName = "Marker Name";
                fixes.Add("Clamped MarkerName to MarkerName");
            }
            return fixes;
        }

        internal override void AddSpecificCategoryNodes(TreeNode categoryNode)
        {
            categoryNode.Nodes.Add(new TreeNode("General")
            {
                Tag = new ObjectiveNodeTag(this, ObjectiveNodeKind.SpecificConfig)
            });
            TreeNode collectionNodes = new TreeNode("Collections")
            {
                Tag = "QuestObjectivesDeliveryCollections"
            };
            foreach (ExpansionQuestObjectiveDelivery delivery in Collections)
            {
                collectionNodes.Nodes.Add(new TreeNode(delivery.ClassName)
                {
                    Tag = delivery
                });
            }
            categoryNode.Nodes.Add(collectionNodes);
        }
    }
}
