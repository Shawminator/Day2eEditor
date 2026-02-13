using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionStartingGearGeneralControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionStartingGear _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionStartingGearGeneralControl()
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
            _data = data as ExpansionStartingGear ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            EnableStartingGearCB.Checked = _data.EnableStartingGear == 1 ? true : false;
            SetRandomHealthSGCB.Checked = _data.SetRandomHealth == 1 ? true : false;
            ApplyEnergySourcesCB.Checked = _data.ApplyEnergySources == 1 ? true : false;

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
                if (_data.EnableStartingGear == 1)
                {
                    var listProps = typeof(ExpansionStartingGear)
                        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(p => p.PropertyType == typeof(BindingList<ExpansionStartingGearItem>));

                    foreach (var prop in listProps)
                    {
                        var list = (BindingList<ExpansionStartingGearItem>)prop.GetValue(_data);
                        var categoryNode = new TreeNode(prop.Name) { Tag = prop.Name };
                        if (list == null || list.Count == 0)
                        {
                            _nodes.Last().Nodes.Add(categoryNode);
                            continue;
                        }
                        foreach (ExpansionStartingGearItem item in list)
                        {
                            TreeNode Itemnode = new TreeNode(item.ClassName)
                            {
                                Tag = item // store the actual object
                            };
                            TreeNode itemAttchmentsNode = new TreeNode("Attachments")
                            {
                                Tag = "ExpansionStartingGearItemAttachments"
                            };
                            foreach (string itemclassanme in item.Attachments)
                            {
                                itemAttchmentsNode.Nodes.Add(new TreeNode(itemclassanme)
                                {
                                    Tag = "ExpansionStartingGearItemAttachment"
                                });
                            }
                            Itemnode.Nodes.Add(itemAttchmentsNode);
                            categoryNode.Nodes.Add(Itemnode);
                        }
                        _nodes.Last().Nodes.Add(categoryNode);
                    }
                    AddSingleItemNodeIfNotEmpty(_nodes.Last(), _data.PrimaryWeapon, nameof(_data.PrimaryWeapon));
                    AddSingleItemNodeIfNotEmpty(_nodes.Last(), _data.SecondaryWeapon, nameof(_data.SecondaryWeapon));
                }
                else if (_data.EnableStartingGear == 0)
                {
                    _nodes.Last().Nodes.Clear();
                }
            }
        }
        private void AddSingleItemNodeIfNotEmpty(TreeNode root, ExpansionStartingGearItem item, string name)
        {
            TreeNode categoryNode = new TreeNode(name) { Tag = name };
            if (item != null && item.ClassName != null && item.Quantity != null && item.Attachments != null)
            {
                TreeNode Itemnode = new TreeNode((item.ClassName))
                {
                    Tag = item
                };
                TreeNode itemAttchmentsNode = new TreeNode("Attachments")
                {
                    Tag = "ExpansionStartingGearItemAttachments"
                };
                foreach (string itemclassanme in item.Attachments)
                {
                    itemAttchmentsNode.Nodes.Add(new TreeNode(itemclassanme)
                    {
                        Tag = "ExpansionStartingGearItemAttachment"
                    });
                }
                Itemnode.Nodes.Add(itemAttchmentsNode);
                categoryNode.Nodes.Add(Itemnode);
            }
            root.Nodes.Add(categoryNode);
        }
        #endregion
        private void EnableStartingGearCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.EnableStartingGear = EnableStartingGearCB.Checked == true ? 1 : 0;
            UpdateTreeNodeNodes();
        }
        private void SetRandomHealthSGCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.SetRandomHealth = SetRandomHealthSGCB.Checked == true ? 1 : 0;
        }
        private void ApplyEnergySourcesCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.ApplyEnergySources = ApplyEnergySourcesCB.Checked == true ? 1 : 0;
        }
    }
}