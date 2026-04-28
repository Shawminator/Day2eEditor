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
    public class ExpansionQuestObjectiveTravelConfig : ExpansionQuestObjectiveConfig
    {
        [JsonPropertyOrder(10)]
        public Vec3? Position { get; set; }
        [JsonPropertyOrder(11)]
        public decimal? MaxDistance { get; set; }
        [JsonPropertyOrder(12)]
        public string? MarkerName { get; set; }
        [JsonPropertyOrder(13)]
        public int? ShowDistance { get; set; }
        [JsonPropertyOrder(14)]
        public int? TriggerOnEnter { get; set; }
        [JsonPropertyOrder(15)]
        public int? TriggerOnExit { get; set; }


        public override ExpansionQuestObjectiveConfig Clone()
        {
            ExpansionQuestObjectiveTravelConfig clone = new ExpansionQuestObjectiveTravelConfig
            {
                ConfigVersion = ConfigVersion,
                ID = ID,
                ObjectiveType = ObjectiveType,
                ObjectiveText = ObjectiveText,
                TimeLimit = TimeLimit,
                Active = Active,

                Position = Position.Clone(),
                MaxDistance = MaxDistance,
                MarkerName = MarkerName,
                ShowDistance = ShowDistance,
                TriggerOnEnter = TriggerOnEnter,
                TriggerOnExit = TriggerOnExit
            };
            clone.SetPath(_path);
            clone.SetGuid(Id);
            return clone;
        }
        protected override bool EqualsCore(ExpansionQuestObjectiveConfig other)
        {
            var o = (ExpansionQuestObjectiveTravelConfig)other;

            if (!Position.Equals(o.Position))
                return false;

            if (MaxDistance != o.MaxDistance ||
                MarkerName != o.MarkerName ||
                ShowDistance != o.ShowDistance ||
                TriggerOnEnter != o.TriggerOnEnter ||
                TriggerOnExit != o.TriggerOnExit)
                return false;

            return true;
        }

        internal override void AddSpecificCategoryNodes(TreeNode categoryNode)
        {
           
        }

        internal override IEnumerable<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            fixes.AddRange(base.FixMissingOrInvalidFields());

            if (Position == null)
            {
                Position = new Vec3(0m,0m,0m);
                fixes.Add("Position Collections");
            }
            if (MaxDistance == null || (MaxDistance.HasValue && MaxDistance < 0))
            {
                MaxDistance = 10;
                fixes.Add("Clamped MaxDistance to 10");
            }
            if (string.IsNullOrWhiteSpace(MarkerName))
            {
                MarkerName = string.Empty;
                fixes.Add("Defaulted MarkerName to empty string");
            }
            if (ShowDistance is null || (ShowDistance != 0 && ShowDistance != 1))
            {
                ShowDistance = 1;
                fixes.Add("Normalised ShowDistance to 1 (valid values: 0 or 1)");
            }
            if (TriggerOnEnter is null || (TriggerOnEnter != 0 && TriggerOnEnter != 1))
            {
                TriggerOnEnter = 1;
                fixes.Add("Normalised TriggerOnEnter to 1 (valid values: 0 or 1)");
            }
            if (TriggerOnExit is null || (TriggerOnExit != 0 && TriggerOnExit != 1))
            {
                TriggerOnExit = 0;
                fixes.Add("Normalised TriggerOnExit to 0 (valid values: 0 or 1)");
            }
            return fixes;
        }
    }
}
