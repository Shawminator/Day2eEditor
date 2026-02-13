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
    public partial class ExpansionStartingGearItemControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionStartingGearItem _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionStartingGearItemControl()
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
            _data = data as ExpansionStartingGearItem ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            GearItemNameTB.Text = _data.ClassName;
            GearItemQuantityNUD.Value = (int)_data.Quantity;

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
                _nodes.Last().Text = _data.ClassName;
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            if (_suppressEvents) {return;}
            AddItemfromTypes form = new AddItemfromTypes 
            {
                UseOnlySingleItem = true,
            };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    if (_data.ClassName != l)
                    {
                        _data.ClassName = l;
                        GearItemNameTB.Text = _data.ClassName;
                    }
                }
            }
            UpdateTreeNodeText();
        }

        private void GearItemQuantityNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.Quantity = (int)GearItemQuantityNUD.Value;
        }
    }
}