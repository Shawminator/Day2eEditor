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
    public partial class ExpansionRaidSettingsSafesControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionRaidSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionRaidSettingsSafesControl()
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
            _data = data as ExpansionRaidSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            CanRaidSafesCB.Checked = _data.CanRaidSafes == 1 ? true : false;
            SafeRaidUseScheduleCB.Checked = _data.SafeRaidUseSchedule == 1 ? true : false;
            SafeExplosionDamageMultiplierNUD.Value = (decimal)_data.SafeExplosionDamageMultiplier;
            SafeProjectileDamageMultiplierNUD.Value = (decimal)_data.SafeProjectileDamageMultiplier;
            SafeRaidToolTimeSecondsNUD.Value = (decimal)_data.SafeRaidToolTimeSeconds;
            SafeRaidToolCyclesNUD.Value = (decimal)_data.SafeRaidToolCycles;
            SafeRaidToolDamagePercentNUD.Value = (decimal)_data.SafeRaidToolDamagePercent;

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

        private void CanRaidSafesCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.CanRaidSafes = CanRaidSafesCB.Checked == true ? 1 : 0;
            
        }

        private void SafeRaidUseScheduleCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.SafeRaidUseSchedule = SafeRaidUseScheduleCB.Checked == true ? 1 : 0;
            
        }

        private void SafeExplosionDamageMultiplierNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.SafeExplosionDamageMultiplier = (int)SafeExplosionDamageMultiplierNUD.Value;
            
        }

        private void SafeProjectileDamageMultiplierNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.SafeProjectileDamageMultiplier = (int)SafeProjectileDamageMultiplierNUD.Value;
            
        }

        private void SafeRaidToolTimeSecondsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.SafeRaidToolTimeSeconds = (int)SafeRaidToolTimeSecondsNUD.Value;
            
        }

        private void SafeRaidToolCyclesNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.SafeRaidToolCycles = (int)SafeRaidToolCyclesNUD.Value;
            
        }

        private void SafeRaidToolDamagePercentNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.SafeRaidToolDamagePercent = (int)SafeRaidToolDamagePercentNUD.Value;
            
        }
    }
}