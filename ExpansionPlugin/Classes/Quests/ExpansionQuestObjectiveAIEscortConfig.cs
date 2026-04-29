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
    public class ExpansionQuestObjectiveAIEscortConfig : ExpansionQuestObjectiveConfig
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
        public int? CanLootAI { get; set; }
        [JsonPropertyOrder(15)]
        public string? NPCLoadoutFile { get; set; }
        [JsonPropertyOrder(16)]
        public string? NPCClassName { get; set; }
        [JsonPropertyOrder(17)]
        public string? NPCName { get; set; }

        public override ExpansionQuestObjectiveConfig Clone()
        {
            ExpansionQuestObjectiveAIEscortConfig clone =  new ExpansionQuestObjectiveAIEscortConfig
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
                CanLootAI = CanLootAI,
                NPCLoadoutFile = NPCLoadoutFile,
                NPCClassName = NPCClassName,
                NPCName = NPCName
            };
            clone.SetPath(_path);
            clone.SetGuid(Id);
            return clone;
        }
        protected override bool EqualsCore(ExpansionQuestObjectiveConfig other)
        {
            var o = (ExpansionQuestObjectiveAIEscortConfig)other;

            if (!Position.Equals(o.Position) ||
                MaxDistance != o.MaxDistance ||
                MarkerName != o.MarkerName ||
                ShowDistance != o.ShowDistance ||
                CanLootAI != o.CanLootAI ||
                NPCLoadoutFile != o.NPCLoadoutFile ||
                NPCClassName != o.NPCClassName ||
                NPCName != o.NPCName)
                return false;

            return true;
        }
        internal override IEnumerable<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            fixes.AddRange(base.FixMissingOrInvalidFields());

            if (Position == null)
            {
                Position = new Vec3(0m, 0m, 0m);
                fixes.Add("Initialised Position");
            }
            if (MaxDistance == null)
            {
                MaxDistance = 20;
                fixes.Add("Clamped ExecutionAmount to 20");
            }
            if (MarkerName == null)
            {
                MarkerName = "Escort Survivor";
                fixes.Add("Initialised MarkerName to Escort Survivor");
            }

            if (ShowDistance is null || (ShowDistance != 0 && ShowDistance != 1))
            {
                ShowDistance = 1;
                fixes.Add("Normalised ShowDistance to 1 (valid values: 0 or 1)");
            }
            if (CanLootAI is null || (CanLootAI != 0 && CanLootAI != 1))
            {
                CanLootAI = 0;
                fixes.Add("Normalised CanLootAI to 0 (valid values: 0 or 1)");
            }
            if (NPCLoadoutFile == null)
            {
                NPCLoadoutFile = "SurvivorLoadout";
                fixes.Add("Initialised NPCLoadoutFile to SurvivorLoadout");
            }
            if (NPCClassName == null)
            {
                NPCClassName = "eAI_SurvivorM_Rolf";
                fixes.Add("Initialised NPCClassName to eAI_SurvivorM_Rolf");
            }
            if (NPCName == null)
            {
                NPCName = "Survivior";
                fixes.Add("Initialised NPCName to Survivior");
            }
            return fixes;
        }
        internal override void AddSpecificCategoryNodes(TreeNode categoryNode)
        {
            categoryNode.Nodes.Add(new TreeNode("General")
            {
                Tag = new ObjectiveNodeTag(this, ObjectiveNodeKind.SpecificConfig)
            });
            TreeNode Positionnode = new TreeNode("Position")
            {
                Tag = "QuestObjectiveAIEscortPosition"
            };
            Positionnode.Nodes.Add(new TreeNode(Position.ToString())
            {
                Tag = Position
            });
            categoryNode.Nodes.Add(Positionnode);
        }
    }
}
