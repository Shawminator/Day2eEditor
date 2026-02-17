using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionMissionSettingsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionMissionSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionMissionSettingsControl()
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
            _data = data as ExpansionMissionSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            MissionsEnabledCB.Checked = _data.Enabled == 1 ? true : false;
            InitialMissionStartDelayNUD.Value = (int)Helper.ConvertMillisecondsToMinutes((int)_data.InitialMissionStartDelay);
            TimeBetweenMissionsNUD.Value = (int)Helper.ConvertMillisecondsToMinutes((int)_data.TimeBetweenMissions);
            MinMissionsNUD.Value = (int)_data.MinMissions;
            MaxMissionsNUD.Value = (int)_data.MaxMissions;
            MinPlayersToStartMissionsNUD.Value = (int)_data.MinPlayersToStartMissions;

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

        private void MissionsEnabledCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Enabled = MissionsEnabledCB.Checked == true ? 1 : 0;
        }
        private void InitialMissionStartDelayNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.InitialMissionStartDelay = (int)Helper.ConvertMinutesToMilliseconds((int)InitialMissionStartDelayNUD.Value);
        }
        private void TimeBetweenMissionsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.TimeBetweenMissions = (int)Helper.ConvertMinutesToMilliseconds((int)TimeBetweenMissionsNUD.Value);
        }
        private void MinMissionsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MinMissions = (int)MinMissionsNUD.Value;
        }
        private void MaxMissionsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxMissions = (int)MaxMissionsNUD.Value;
        }
        private void MinPlayersToStartMissionsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MinPlayersToStartMissions = (int)MinPlayersToStartMissionsNUD.Value;
        }
    }
}