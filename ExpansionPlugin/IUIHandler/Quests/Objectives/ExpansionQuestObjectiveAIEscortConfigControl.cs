using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionQuestObjectiveAIEscortConfigControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionQuestObjectiveAIEscortConfig _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionQuestObjectiveAIEscortConfigControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Returns the UserControl instance
        /// </summary>
        public Control GetControl() => this;

        /// <summary>
        /// Loads data into the control and stores the selected tree nodes
        /// </summary>
        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as ExpansionQuestObjectiveAIEscortConfig ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            BindingList<string> LoadoutNameList = new BindingList<string>
                {
                    ""
                };
            foreach (AILoadouts lo in AppServices.GetRequired<ExpansionManager>().ExpansionLoadoutConfig.Items)
            {
                LoadoutNameList.Add(Path.GetFileNameWithoutExtension(lo.FileName));
            }
            ObjectivesAIVIPNPCLoadoutFileCB.DataSource = new BindingList<string>(LoadoutNameList);
            ObjectivesAIVIPNPCNPCClassnameCB.DataSource = new BindingList<string>(NPCClassnames);
            ObjectivesAIVIPMaxDistanceNUD.Value = (decimal)_data.MaxDistance;
            ObjectivesAIVIPNPCLoadoutFileCB.SelectedIndex = ObjectivesAIVIPNPCLoadoutFileCB.FindStringExact(_data.NPCLoadoutFile);
            ObjectivesAIVIPMarkerNameTB.Text = _data.MarkerName;
            QuestObjectivesAIVIPShowDistanceCB.Checked = _data.ShowDistance == 1 ? true : false;
            QuestObjectivesAIVIPCanLootAICB.Checked = _data.CanLootAI == 1 ? true : false;
            ObjectivesAIVIPNPCNPCClassnameCB.SelectedIndex = ObjectivesAIVIPNPCNPCClassnameCB.FindStringExact(_data.NPCClassName);
            ObjectivesAIVIPNPCNameTB.Text = _data.NPCName;
            _suppressEvents = false;
        }

        #region Helper Methods
        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                // TODO: Update _nodes.Last().Text based on _data
            }
        }

        #endregion

        private void ObjectivesAIVIPMaxDistanceNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxDistance = ObjectivesAIVIPMaxDistanceNUD.Value;
        }

        private void ObjectivesAIVIPNPCNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NPCName = ObjectivesAIVIPNPCNameTB.Text;
        }

        private void ObjectivesAIVIPNPCLoadoutFileCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NPCLoadoutFile = ObjectivesAIVIPNPCLoadoutFileCB.GetItemText(ObjectivesAIVIPNPCLoadoutFileCB.SelectedItem);
        }

        private void ObjectivesAIVIPNPCNPCClassnameCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NPCClassName = ObjectivesAIVIPNPCNPCClassnameCB.GetItemText(ObjectivesAIVIPNPCNPCClassnameCB.SelectedItem);
        }

        private void ObjectivesAIVIPMarkerNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MarkerName = ObjectivesAIVIPMarkerNameTB.Text;
        }

        private void QuestObjectivesAIVIPShowDistanceCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ShowDistance = QuestObjectivesAIVIPShowDistanceCB.Checked == true ? 1 : 0;
        }

        private void QuestObjectivesAIVIPCanLootAICB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CanLootAI = QuestObjectivesAIVIPCanLootAICB.Checked == true ? 1 : 0;
        }
        List<string> NPCClassnames = new List<string>
            {
                "eAI_SurvivorM_Mirek",
                "eAI_SurvivorM_Denis",
                "eAI_SurvivorM_Boris",
                "eAI_SurvivorM_Cyril",
                "eAI_SurvivorM_Elias",
                "eAI_SurvivorM_Francis",
                "eAI_SurvivorM_Guo",
                "eAI_SurvivorM_Hassan",
                "eAI_SurvivorM_Indar",
                "eAI_SurvivorM_Jose",
                "eAI_SurvivorM_Kaito",
                "eAI_SurvivorM_Lewis",
                "eAI_SurvivorM_Manua",
                "eAI_SurvivorM_Niki",
                "eAI_SurvivorM_Oliver",
                "eAI_SurvivorM_Peter",
                "eAI_SurvivorM_Quinn",
                "eAI_SurvivorM_Rolf",
                "eAI_SurvivorM_Seth",
                "eAI_SurvivorM_Taiki",
                "eAI_SurvivorF_Linda",
                "eAI_SurvivorF_Maria",
                "eAI_SurvivorF_Frida",
                "eAI_SurvivorF_Gabi",
                "eAI_SurvivorF_Helga",
                "eAI_SurvivorF_Irena",
                "eAI_SurvivorF_Judy",
                "eAI_SurvivorF_Keiko",
                "eAI_SurvivorF_Eva",
                "eAI_SurvivorF_Naomi",
                "eAI_SurvivorF_Baty"
            };

    }
}