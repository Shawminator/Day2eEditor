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
    public partial class ExpansionMIssionAirdropSettingsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionMissionEventAirdrop _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionMIssionAirdropSettingsControl()
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
            _data = data as ExpansionMissionEventAirdrop ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            MissionShowNotificationCB.Checked = _data.ShowNotification == 1 ? true : false;
            MissionHeightNUD.Value = (decimal)_data.Height;
            MIssionDropZoneHeightNUD.Value = (decimal)_data.Height;
            MissionSpeedNUD.Value = (decimal)_data.Speed;
            MissionDropZoneSpeedNUD.Value = (decimal)_data.DropZoneSpeed;
            MIssionContainerCB.SelectedIndex = MIssionContainerCB.FindStringExact(_data.Container);
            MissionFallSpeedNUD.Value = (decimal)_data.FallSpeed;
            MissionItemCountNUD.Value = (decimal)_data.ItemCount;
            MissionInfectedCountNUD.Value = (decimal)_data.InfectedCount;
            MissionAirdropPlaneClassNameTB.Text = _data.AirdropPlaneClassName;
            
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