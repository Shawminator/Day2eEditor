using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionQuestObjectiveTravelConfigControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionQuestObjectiveTravelConfig _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionQuestObjectiveTravelConfigControl()
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
            _data = data as ExpansionQuestObjectiveTravelConfig ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            ObjectivesTravelMaxDistanceNUD.Value = (decimal)_data.MaxDistance;
            ObjectivesTravelMarkerNameTB.Text = _data.MarkerName;
            ObjectivesTravelShowDistanceCB.Checked = _data.ShowDistance == 1 ? true : false;
            checkBox8.Checked = _data.TriggerOnEnter == 1 ? true : false;
            checkBox9.Checked = _data.TriggerOnExit == 1 ? true : false;

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

        private void ObjectivesTravelMaxDistanceNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxDistance = ObjectivesTravelMaxDistanceNUD.Value;
        }

        private void ObjectivesTravelMarkerNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MarkerName = ObjectivesTravelMarkerNameTB.Text;
        }

        private void ObjectivesTravelShowDistanceCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ShowDistance = ObjectivesTravelShowDistanceCB.Checked == true ? 1 : 0;
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.TriggerOnEnter = checkBox8.Checked == true ? 1 : 0;
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.TriggerOnExit = checkBox9.Checked == true ? 1 : 0;
        }
    }
}