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
    public partial class ExpansionP2PMarketSettingsGeneralControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpasnionP2PMarketSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionP2PMarketSettingsGeneralControl()
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
            _data = data as ExpasnionP2PMarketSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            checkBox1.Checked = _data.Enabled == 1 ? true : false;
            numericUpDown1.Value = (int)_data.MaxListingTime;
            numericUpDown2.Value = (int)_data.MaxListings;
            numericUpDown3.Value = (int)_data.ListingOwnerDiscountPercent;
            numericUpDown4.Value = (int)_data.ListingPricePercent;
            numericUpDown5.Value = (int)_data.SalesDepositTime;
            checkBox2.Checked = _data.DisallowUnpersisted == 1 ? true : false;

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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Enabled = checkBox1.Checked == true ? 1 : 0;
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxListingTime = (int)numericUpDown1.Value;
        }
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxListings = (int)numericUpDown2.Value;
        }
        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ListingOwnerDiscountPercent = (int)numericUpDown3.Value;
        }
        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ListingPricePercent = (int)numericUpDown4.Value;
        }
        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SalesDepositTime = (int)numericUpDown5.Value;
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DisallowUnpersisted = checkBox2.Checked == true ? 1 : 0;
        }
    }
}