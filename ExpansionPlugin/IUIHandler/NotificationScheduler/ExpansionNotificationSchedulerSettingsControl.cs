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
    public partial class ExpansionNotificationSchedulerSettingsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionNotificationSchedulerSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionNotificationSchedulerSettingsControl()
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
            _data = data as ExpansionNotificationSchedulerSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            SchedulerEnabledCB.Checked = _data.Enabled == 1 ? true : false;
            UTCTimeCB.Checked = _data.UTC == 1 ? true : false;
            UseMissionTimeCB.Checked = _data.UseMissionTime == 1 ? true : false;

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

        private void SchedulerEnabledCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Enabled = SchedulerEnabledCB.Checked == true ? 1 : 0;
            
        }

        private void UTCTimeCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UTC = UTCTimeCB.Checked == true ? 1 : 0;
            
        }

        private void UseMissionTimeCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseMissionTime = UseMissionTimeCB.Checked == true ? 1 : 0;
            
        }
    }
}