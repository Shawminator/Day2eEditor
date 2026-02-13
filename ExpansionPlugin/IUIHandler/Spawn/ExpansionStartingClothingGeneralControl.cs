using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionStartingClothingGeneralControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionStartingClothing _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionStartingClothingGeneralControl()
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
            _data = data as ExpansionStartingClothing ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            EnableCustomClothingCB.Checked = _data.EnableCustomClothing == 1 ? true : false;
            SetRandomHealthCB.Checked = _data.SetRandomHealth == 1 ? true : false;

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
        private void UpdateTreeNodeNodes()
        {
            if (_nodes?.Any() == true)
            {
                if (_data.EnableCustomClothing == 1)
                {
                    // Loop through all list properties in the class
                    var listProperties = typeof(ExpansionStartingClothing)
                        .GetProperties()
                        .Where(p => p.PropertyType == typeof(BindingList<string>));

                    foreach (var prop in listProperties)
                    {
                        BindingList<string> list = (BindingList<string>)prop.GetValue(_data);

                        // Create category node
                        TreeNode categoryNode = new TreeNode(prop.Name)
                        {
                            Tag = prop.Name
                        };

                        if (list != null)
                        {
                            foreach (var item in list)
                            {
                                categoryNode.Nodes.Add(new TreeNode(item)
                                {
                                    Tag = prop.Name + "Item"
                                });
                            }
                        }

                        _nodes.Last().Nodes.Add(categoryNode);
                    }
                }
                else if (_data.EnableCustomClothing == 0)
                {
                    _nodes.Last().Nodes.Clear();
                }
            }
        }

        #endregion

        private void EnableCustomClothingCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.EnableCustomClothing = EnableCustomClothingCB.Checked == true ? 1 : 0;
            UpdateTreeNodeNodes();
        }

        private void SetRandomHealthCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.SetRandomHealth = SetRandomHealthCB.Checked == true ? 1 : 0;
        }
    }
}