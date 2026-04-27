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
    public class ExpansionQuestObjectiveTreasureHuntConfig : ExpansionQuestObjectiveConfig
    {
        [JsonPropertyOrder(10)]
        public int? ShowDistance { get; set; }
        [JsonPropertyOrder(11)]
        public string? ContainerName { get; set; }
        [JsonPropertyOrder(12)]
        public int? DigInStash { get; set; }
        [JsonPropertyOrder(13)]
        public string? MarkerName { get; set; }
        [JsonPropertyOrder(14)]
        public MapMarkerVisibility? MarkerVisibility { get; set; }
        [JsonPropertyOrder(15)]
        public BindingList<Vec3> Positions { get; set; }
        [JsonPropertyOrder(16)]
        public int? LootItemsAmount { get; set; }
        [JsonPropertyOrder(17)]
        public decimal? MaxDistance { get; set; }
        [JsonPropertyOrder(18)]
        public BindingList<ExpansionLoot> Loot { get; set; }
        public override ExpansionQuestObjectiveConfig Clone()
        {
            return new ExpansionQuestObjectiveTreasureHuntConfig
            {
                ConfigVersion = ConfigVersion,
                ID = ID,
                ObjectiveType = ObjectiveType,
                ObjectiveText = ObjectiveText,
                TimeLimit = TimeLimit,
                Active = Active,

                ShowDistance = ShowDistance,
                ContainerName = ContainerName,
                DigInStash = DigInStash,
                MarkerName = MarkerName,
                MarkerVisibility = MarkerVisibility,
                Positions = Positions != null
                    ? new BindingList<Vec3>(Positions.Select(x => x.Clone()).ToList())
                    : null,
                Loot = Loot != null
                    ? new BindingList<ExpansionLoot>(Loot.Select(x => x.Clone()).ToList())
                    : null,
                LootItemsAmount = LootItemsAmount,
                MaxDistance = MaxDistance
            };
        }
        protected override bool EqualsCore(ExpansionQuestObjectiveConfig other)
        {
            var o = (ExpansionQuestObjectiveTreasureHuntConfig)other;

            if (ShowDistance != o.ShowDistance ||
                ContainerName != o.ContainerName ||
                DigInStash != o.DigInStash ||
                MarkerName != o.MarkerName ||
                MarkerVisibility != o.MarkerVisibility ||
                LootItemsAmount != o.LootItemsAmount ||
                MaxDistance != o.MaxDistance)
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
            if (ShowDistance is null || (ShowDistance != 0 && ShowDistance != 1))
            {
                ShowDistance = 1;
                fixes.Add("Normalised TriggerOnEnter to 1 (valid values: 0 or 1)");
            }
            if (string.IsNullOrWhiteSpace(ContainerName))
            {
                ContainerName = "ExpansionQuestSeaChest";
                fixes.Add("Defaulted ContainerName to ExpansionQuestSeaChest");
            }
            if (DigInStash is null || (DigInStash != 0 && DigInStash != 1))
            {
                DigInStash = 1;
                fixes.Add("Normalised DigInStash to 1 (valid values: 0 or 1)");
            }
            if (string.IsNullOrWhiteSpace(MarkerName))
            {
                MarkerName = "???";
                fixes.Add("Defaulted MarkerName to ???");
            }

            if (!MarkerVisibility.HasValue ||
                !Enum.IsDefined(typeof(MapMarkerVisibility), MarkerVisibility.Value))
            {
                MarkerVisibility = MapMarkerVisibility.Visible_on_the_Map_only;
                fixes.Add("Defaulted MarkerVisibility to Visible_on_the_Map_only (4)");
            }

            if (Positions == null || Positions.Count == 0)
            {
                Positions = new BindingList<Vec3>{new Vec3(0m, 0m, 0m)};
                fixes.Add("Defaulted Positions to single Vec3.Zero entry");
            }
            if (Loot == null)
            {
                Loot = new BindingList<ExpansionLoot>();
                fixes.Add("Initialised empty Loot list");
            }
            if (LootItemsAmount is null || LootItemsAmount < 0)
            {
                LootItemsAmount = Loot.Count;
                fixes.Add("Normalised LootItemsAmount to match Loot count");
            }
            if (!MaxDistance.HasValue || MaxDistance.Value < 0)
            {
                MaxDistance = 10m;
                fixes.Add("Defaulted MaxDistance to 10");
            }

            return fixes;
        }
    }
}
