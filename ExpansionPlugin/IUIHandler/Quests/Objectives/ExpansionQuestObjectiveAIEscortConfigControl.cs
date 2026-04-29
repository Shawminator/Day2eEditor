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
            ObjectivesAIVIPMaxDistanceNUD.Value = (decimal)_data.MaxDistance;
            ObjectivesAIVIPNPCLoadoutFileCB.SelectedIndex = ObjectivesAIVIPNPCLoadoutFileCB.FindStringExact(_data.NPCLoadoutFile);
            ObjectivesAIVIPMarkerNameTB.Text = _data.MarkerName;
            QuestObjectivesAIVIPShowDistanceCB.Checked = _data.ShowDistance == 1 ? true : false;
            QuestObjectivesAIVIPCanLootAICB.Checked = _data.CanLootAI == 1 ? true : false;
            ObjectivesAIVIPNPCNPCClassnameTB.Text = _data.NPCClassName;
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

        private void ObjectivesAIVIPNPCNPCClassnameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NPCClassName = ObjectivesAIVIPNPCNPCClassnameTB.Text;
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
    }
}