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
    public partial class ExpansionVehiclesLockConfigControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionVehiclesLockConfig _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionVehiclesLockConfigControl()
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
            _data = data as ExpansionVehiclesLockConfig ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            ClassNameTB.Text = _data.ClassName;
            LockComplexityNUD.Value = (decimal)_data.LockComplexity;

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
            if (_suppressEvents) { return; }
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
                        ClassNameTB.Text = _data.ClassName;
                    }
                }
            }
            UpdateTreeNodeText();
        }

        private void LockComplexityNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.LockComplexity = LockComplexityNUD.Value;
        }
    }
}