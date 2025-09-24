using Day2eEditor;
using ExpansionPlugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionLootControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private BindingList<ExpansionLoot> _data;
        private BindingList<ExpansionLoot> _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;
        private TreeNode? currentTreeNode;
        public ExpansionLoot currentExpanionLootItem;
        public ExpansionLootVariant CurrentLootVArient;

        public ExpansionLootControl()
        {
            InitializeComponent();

        }
        private TreeNode CreateLootNode(ExpansionLoot eL)
        {
            TreeNode ExpansionLootTN = new TreeNode(eL.Name)
            {
                Tag = eL
            };
            TreeNode AttachmentTN = new TreeNode("Attachments")
            {
                Tag = "Attachments"
            };
            foreach (ExpansionLootVariant elv in eL.Attachments)
            {
                AttachmentTN.Nodes.Add(getLootVarients(elv));
            }
            TreeNode VariantsTN = new TreeNode("Variants")
            {
                Tag = "Variants"
            };
            foreach (ExpansionLootVariant elv in eL.Variants)
            {
                VariantsTN.Nodes.Add(getLootVarients(elv));
            }
            ExpansionLootTN.Nodes.Add(AttachmentTN);
            ExpansionLootTN.Nodes.Add(VariantsTN);
            return ExpansionLootTN;
        }
        private TreeNode getLootVarients(ExpansionLootVariant elv)
        {
            TreeNode ExpansionLootVarientTN = new TreeNode(elv.Name)
            {
                Tag = elv
            };
            TreeNode AttachmentTN = new TreeNode("Attachments")
            {
                Tag = "Attachments"
            };
            foreach (ExpansionLootVariant elv2 in elv.Attachments)
            {
                AttachmentTN.Nodes.Add(getLootVarients(elv2));
            }
            ExpansionLootVarientTN.Nodes.Add(AttachmentTN);
            return ExpansionLootVarientTN;
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
            _data = data as BindingList<ExpansionLoot> ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            ExpansionLootTV.Nodes.Clear();
            TreeNode root = new TreeNode("Loot")
            {
                Tag = "LootParent"
            };
            foreach (ExpansionLoot EL in _data)
            {
                root.Nodes.Add(CreateLootNode(EL));
            }
            ExpansionLootTV.Nodes.Add(root);
            root.Expand();

            _suppressEvents = false;
        }

        /// <summary>
        /// Applies changes to the data and updates the original snapshot
        /// </summary>
        public void ApplyChanges()
        {
            _originalData = CloneData(_data);
        }

        /// <summary>
        /// Resets control fields to the original data
        /// </summary>
        public void Reset()
        {
            // TODO: Reset control fields to _originalData
        }

        /// <summary>
        /// Checks if there are changes and updates the parent file's dirty state
        /// </summary>
        public void HasChanges()
        {
            var parentObj = _nodes.Last().FindParentOfType(_parentType);
            if (parentObj != null)
            {
                dynamic parent = parentObj;
                parent.isDirty = !_data.Equals(_originalData);
            }
        }

        #region Helper Methods

        /// <summary>
        /// Clones the data for reset purposes
        /// </summary>
        private BindingList<ExpansionLoot> CloneData(BindingList<ExpansionLoot> data)
        {
            // TODO: Implement actual cloning logic
            return new BindingList<ExpansionLoot>
            {
                // Copy properties here
            };
        }

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

        private void ExpansionLootTV_AfterSelect(object sender, TreeViewEventArgs e)
        {
            currentTreeNode = e.Node;
            expansionLootItemGB.Visible = false;
            expansionLootVarientGB.Visible = false;
            ExpansionLootitemSetAllChanceButton.Visible = false;
            ExpansionLootitemSetAllRandomChanceButton.Visible = false;
            currentExpanionLootItem = null;
            CurrentLootVArient = null;
            if (e.Node.Tag is string)
            {
                if (e.Node.Tag.ToString() == "LootParent")
                {
                    expansionLootVarientGB.Visible = true;
                    expansionLootVarientGB.Text = "Set All Chance";
                    ExpansionLootitemSetAllChanceButton.Visible = true;
                    ExpansionLootitemSetAllRandomChanceButton.Visible = true;
                }
                if (e.Node.Tag.ToString() == "Attachments")
                {
                    if (e.Node.Parent.Tag is ExpansionLoot)
                        currentExpanionLootItem = e.Node.Parent.Tag as ExpansionLoot;
                    else if (e.Node.Parent.Tag is ExpansionLootVariant)
                        CurrentLootVArient = e.Node.Parent.Tag as ExpansionLootVariant;
                }
                if (e.Node.Tag.ToString() == "Variants")
                {
                    currentExpanionLootItem = e.Node.Parent.Tag as ExpansionLoot;
                }
            }
            else if (e.Node.Tag is ExpansionLoot)
            {
                expansionLootItemGB.Visible = true;
                currentExpanionLootItem = e.Node.Tag as ExpansionLoot;
                SetLootitem();
            }
            else if (e.Node.Tag is ExpansionLootVariant)
            {
                expansionLootVarientGB.Visible = true;
                CurrentLootVArient = e.Node.Tag as ExpansionLootVariant;
                setvarient();
                if (e.Node.Parent.Tag.ToString() == "Attachments")
                {
                    expansionLootVarientGB.Text = "Expansion Loot Attachment";
                }
                else if (e.Node.Parent.Tag.ToString() == "Variants")
                {
                    expansionLootVarientGB.Text = "Expansion Loot Variant";
                }
            }
        }
        private void SetLootitem()
        {
            _suppressEvents = true;

            if (currentExpanionLootItem.Chance > 1)
                currentExpanionLootItem.Chance = 1;
            trackBar1.Value = (int)(currentExpanionLootItem.Chance * 100);
            numericUpDown31.Value = currentExpanionLootItem.QuantityPercent;
            numericUpDown12.Value = currentExpanionLootItem.Max;
            numericUpDown33.Value = currentExpanionLootItem.Min;


            _suppressEvents = false;
        }
        private void setvarient()
        {
            _suppressEvents = true;
            if (CurrentLootVArient.Chance > 1)
                CurrentLootVArient.Chance = 1;
            trackBar2.Value = (int)(CurrentLootVArient.Chance * 100);


            _suppressEvents = false;
        }
        private void ExpansionLootTV_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            darkLabel23.Text = ((decimal)(trackBar1.Value)).ToString() + "%";
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            darkLabel23.Text = ((decimal)(trackBar1.Value)).ToString() + "%";
        }
        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            if (currentExpanionLootItem == null) return;
            currentExpanionLootItem.Chance = ((decimal)trackBar1.Value) / 100;
            HasChanges();
        }
        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            darkLabel1.Text = ((decimal)(trackBar2.Value)).ToString() + "%";
        }
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            darkLabel1.Text = ((decimal)(trackBar2.Value)).ToString() + "%";
        }
        private void trackBar2_MouseUp(object sender, MouseEventArgs e)
        {
            if (CurrentLootVArient == null) return;
            CurrentLootVArient.Chance = ((decimal)trackBar2.Value) / 100;
            HasChanges();
        }

        private void ExpansionLootitemSetAllChanceButton_Click(object sender, EventArgs e)
        {
            foreach (ExpansionLoot el in _data)
            {
                el.Chance = ((decimal)trackBar2.Value) / 100;
            }
            HasChanges();
        }

        private void ExpansionLootitemSetAllRandomChanceButton_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            foreach (ExpansionLoot el in _data)
            {
                TypeEntry type = AppServices.GetRequired<EconomyManager>().TypesConfig.Gettypebyname(el.Name);
                if (type == null) continue;
                int chancemax;
                int chancemin;
                if (type.Nominal <= 1)
                {
                    chancemin = 1;
                    chancemax = 11;
                }
                else if (type.Nominal <= 5)
                {
                    chancemin = 11;
                    chancemax = 26;
                }
                else if (type.Nominal <= 10)
                {
                    chancemin = 21;
                    chancemax = 51;
                }
                else if (type.Nominal <= 15)
                {
                    chancemin = 31;
                    chancemax = 76;
                }
                else
                {
                    chancemin = 41;
                    chancemax = 101;
                }

                el.Chance = (decimal)rnd.Next(chancemin, chancemax) / 100;
            }
            HasChanges();
        }

        private void numericUpDown31_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown12_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown33_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}