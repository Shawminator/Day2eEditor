using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    public partial class AddObjectives : Form
    {
        private FormController controller;


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<ExpansionQuestObjectiveConfig> selectedObjectives { get; set; }
        public AddObjectives()
        {
            InitializeComponent();
            controller = new FormController(
                this,
                panel1,
                null,
                TitleLabel,
                null,
                CloseButton,
                null
            );
            this.Disposed += (s, e) => controller.Dispose();
        }

        private void AddObjectives_Load(object sender, EventArgs e)
        {
            LoadObjectiveText();
        }
        private void LoadObjectiveText()
        {
            treeViewMS1.Nodes.Clear();
            var categoryNodes = new Dictionary<Type, TreeNode>
            {
                { typeof(ExpansionQuestObjectiveActionConfig), new TreeNode("Action") { Tag = "Action" } },
                { typeof(ExpansionQuestObjectiveAICampConfig), new TreeNode("AICamp") { Tag = "AICamp" } },
                { typeof(ExpansionQuestObjectiveAIPatrolConfig), new TreeNode("AIPatrol") { Tag = "AIPatrol" } },
                { typeof(ExpansionQuestObjectiveAIEscortConfig), new TreeNode("AIVIP") { Tag = "AIVIP" } },
                { typeof(ExpansionQuestObjectiveCollectionConfig), new TreeNode("Collection") { Tag = "Collection" } },
                { typeof(ExpansionQuestObjectiveCraftingConfig), new TreeNode("Crafting") { Tag = "Crafting" } },
                { typeof(ExpansionQuestObjectiveDeliveryConfig), new TreeNode("Delivery") { Tag = "Delivery" } },
                { typeof(ExpansionQuestObjectiveTargetConfig), new TreeNode("Target") { Tag = "Target" } },
                { typeof(ExpansionQuestObjectiveTravelConfig), new TreeNode("Travel") { Tag = "Travel" } },
                { typeof(ExpansionQuestObjectiveTreasureHuntConfig), new TreeNode("TreasureHunt") { Tag = "TreasureHunt" } }
            };

            foreach (var objective in AppServices.GetRequired<ExpansionManager>().ExpansionQuestObjectiveConfigConfig.MutableItems)
            {
                var objectiveType = objective.GetType();

                if (!categoryNodes.TryGetValue(objectiveType, out var categoryNode))
                    continue;

                var node = new TreeNode($"ID:{objective.ID} {objective.ObjectiveText}") { Tag = objective };

                categoryNode.Nodes.Add(node);

            }

            foreach (var node in categoryNodes.Values)
            {
                treeViewMS1.Nodes.Add(node);
            }
        }

        private void darkButton1_Click(object sender, EventArgs e)
        {
            selectedObjectives = new List<ExpansionQuestObjectiveConfig>();
            foreach (TreeNode tn in treeViewMS1.SelectedNodes)
            {
                ExpansionQuestObjectiveConfig quest = tn.Tag as ExpansionQuestObjectiveConfig;
                selectedObjectives.Add(quest);
            }
        }
    }
}
