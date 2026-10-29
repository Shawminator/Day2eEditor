using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CaparePlugin
{
    /// <summary>
    /// KeyCard system general settings
    /// </summary>
    public partial class CapareKeyCardGeneralControl : UserControl, IUIHandler
    {
        public event Action DataChanged;
        private Type _parentType;
        private KeycardsSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public CapareKeyCardGeneralControl()
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
            _data = data as KeycardsSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            LockedDoorsEnabledCB.Checked = _data.LockedDoorsEnabled;
            DebugModeEnabledCB.Checked = _data.DebugModeEnabled;
            AdminLoggingEnabledCB.Checked = _data.AdminLoggingEnabled;

            _suppressEvents = false;
        }

        #region Helper Methods
        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            // Node text does not depend on this data
        }
        private static void SetNUD(NumericUpDown nud, decimal value)
        {
            nud.Value = Math.Clamp(value, nud.Minimum, nud.Maximum);
        }

        #endregion

        private void LockedDoorsEnabledCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.LockedDoorsEnabled = LockedDoorsEnabledCB.Checked;
            DataChanged?.Invoke();
        }
        private void DebugModeEnabledCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DebugModeEnabled = DebugModeEnabledCB.Checked;
            DataChanged?.Invoke();
        }
        private void AdminLoggingEnabledCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AdminLoggingEnabled = AdminLoggingEnabledCB.Checked;
            DataChanged?.Invoke();
        }
    }
}
