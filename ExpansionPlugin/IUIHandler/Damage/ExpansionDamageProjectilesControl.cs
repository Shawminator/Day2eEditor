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
    public partial class ExpansionDamageProjectilesControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExplosiveProjectiles _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionDamageProjectilesControl()
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
            _data = data as ExplosiveProjectiles ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            ExplosionTB.Text = _data.explosion;
            AmmoTB.Text = _data.ammo;

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
                _nodes.Last().Text = $"{_data.explosion}:{_data.ammo}";
            }
        }

        #endregion

        private void ExplosionTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.explosion = ExplosionTB.Text;
            UpdateTreeNodeText();
        }

        private void AmmoTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ammo = AmmoTB.Text;
            UpdateTreeNodeText();
        }
    }
}