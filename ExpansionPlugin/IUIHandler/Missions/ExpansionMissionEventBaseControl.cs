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
    public partial class ExpansionMissionEventBaseControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionMissionEventBase _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionMissionEventBaseControl()
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
            _data = data as ExpansionMissionEventBase ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            EnabledCB.Checked = _data.Enabled == 1 ? true : false;
            WeightNUD.Value = (decimal)_data.Weight;
            MissionMaxTimeNUD.Value = (decimal)_data.MissionMaxTime;
            MissionNameTB.Text = _data.MissionName;
            DifficultyNUD.Value = (decimal)_data.Difficulty;
            ObjectiveNUD.Value = (decimal)_data.Objective;
            RewardTB.Text = _data.Reward;

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
    }
}