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
    public partial class ExpansionRaidSettingsExplosionsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionRaidSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionRaidSettingsExplosionsControl()
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

            ExplosionTimeNUD.Value = (decimal)_data.ExplosionTime;
            EnableExplosiveWhitelistCB.Checked = _data.EnableExplosiveWhitelist == 1 ? true : false;
            ExplosionDamageMultiplierNUD.Value = (decimal)_data.ExplosionDamageMultiplier;
            ProjectileDamageMultiplierNUD.Value = (decimal)_data.ProjectileDamageMultiplier;

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

        private void EnableExplosiveWhitelistCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.EnableExplosiveWhitelist = EnableExplosiveWhitelistCB.Checked == true ? 1 : 0;
            
        }

        private void ExplosionTimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.ExplosionTime = ExplosionTimeNUD.Value;
            
        }

        private void ExplosionDamageMultiplierNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.ExplosionDamageMultiplier = ExplosionDamageMultiplierNUD.Value;
            
        }

        private void ProjectileDamageMultiplierNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.ProjectileDamageMultiplier = ProjectileDamageMultiplierNUD.Value;
            
        }
    }
}