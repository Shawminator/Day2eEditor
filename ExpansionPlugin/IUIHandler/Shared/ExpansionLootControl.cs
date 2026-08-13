using Day2eEditor;
using System.ComponentModel;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionLootControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private BindingList<ExpansionLoot> _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;
        private TreeNode? currentTreeNode;
        public ExpansionLoot currentExpanionLootItem;
        public ExpansionLootVariant CurrentLootVariant;

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
                AttachmentTN.Nodes.Add(getLootVariants(elv));
            }
            TreeNode VariantsTN = new TreeNode("Variants")
            {
                Tag = "Variants"
            };
            foreach (ExpansionLootVariant elv in eL.Variants)
            {
                VariantsTN.Nodes.Add(getLootVariants(elv));
            }
            ExpansionLootTN.Nodes.Add(AttachmentTN);
            ExpansionLootTN.Nodes.Add(VariantsTN);
            return ExpansionLootTN;
        }
        private TreeNode getLootVariants(ExpansionLootVariant elv)
        {
            TreeNode ExpansionLootVariantTN = new TreeNode(elv.Name)
            {
                Tag = elv
            };
            TreeNode AttachmentTN = new TreeNode("Attachments")
            {
                Tag = "Attachments"
            };
            foreach (ExpansionLootVariant elv2 in elv.Attachments)
            {
                AttachmentTN.Nodes.Add(getLootVariants(elv2));
            }
            ExpansionLootVariantTN.Nodes.Add(AttachmentTN);
            return ExpansionLootVariantTN;
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

        private bool AreLootListsEqual(BindingList<ExpansionLoot> list1, BindingList<ExpansionLoot> list2)
        {
            if (list1 == null || list2 == null)
                return list1 == list2;

            if (list1.Count != list2.Count)
                return false;

            for (int i = 0; i < list1.Count; i++)
            {
                if (!list1[i].Equals(list2[i]))
                    return false;
            }

            return true;
        }

        #region Helper Methods

        /// <summary>
        /// Clones the data for reset purposes
        /// </summary>

        private BindingList<ExpansionLoot> CloneData(BindingList<ExpansionLoot> data)
        {
            return new BindingList<ExpansionLoot>(data.Select(e => e.Clone()).ToList());
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
            expansionLootVariantGB.Visible = false;
            ExpansionLootitemSetAllChanceButton.Visible = false;
            ExpansionLootitemSetAllRandomChanceButton.Visible = false;
            currentExpanionLootItem = null;
            CurrentLootVariant = null;
            if (e.Node.Tag is string)
            {
                if (e.Node.Tag.ToString() == "LootParent")
                {
                    expansionLootVariantGB.Visible = true;
                    expansionLootVariantGB.Text = "Set All Chance";
                    ExpansionLootitemSetAllChanceButton.Visible = true;
                    ExpansionLootitemSetAllRandomChanceButton.Visible = true;
                }
                if (e.Node.Tag.ToString() == "Attachments")
                {
                    if (e.Node.Parent.Tag is ExpansionLoot)
                        currentExpanionLootItem = e.Node.Parent.Tag as ExpansionLoot;
                    else if (e.Node.Parent.Tag is ExpansionLootVariant)
                        CurrentLootVariant = e.Node.Parent.Tag as ExpansionLootVariant;
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
                expansionLootVariantGB.Visible = true;
                CurrentLootVariant = e.Node.Tag as ExpansionLootVariant;
                setvariant();
                if (e.Node.Parent.Tag.ToString() == "Attachments")
                {
                    expansionLootVariantGB.Text = "Expansion Loot Attachment";
                }
                else if (e.Node.Parent.Tag.ToString() == "Variants")
                {
                    expansionLootVariantGB.Text = "Expansion Loot Variant";
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
        private void setvariant()
        {
            _suppressEvents = true;
            if (CurrentLootVariant.Chance > 1)
                CurrentLootVariant.Chance = 1;
            trackBar2.Value = (int)(CurrentLootVariant.Chance * 100);


            _suppressEvents = false;
        }
        private void ExpansionLootTV_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ExpansionLootTV.SelectedNode = e.Node;
            currentTreeNode = e.Node;

            if (e.Button != MouseButtons.Right) return;

            if (e.Node.Tag.ToString() == "LootParent")
            {
                ExpansionLootCM.Items.Clear();
                ExpansionLootCM.Items.Add(addLootItemsToolStripMenuItem);
                ExpansionLootCM.Show(Cursor.Position);
            }
            else if (e.Node.Tag.ToString() == "Attachments")
            {
                if (e.Button == MouseButtons.Right)
                {
                    ExpansionLootCM.Items.Clear();
                    ExpansionLootCM.Items.Add(addAttachmentToolStripMenuItem);
                    ExpansionLootCM.Show(Cursor.Position);
                }
            }
            else  if (e.Node.Tag.ToString() == "Variants")
            {
                if (e.Button == MouseButtons.Right)
                {
                    ExpansionLootCM.Items.Clear();
                    ExpansionLootCM.Items.Add(addLootVariantsToolStripMenuItem);
                    ExpansionLootCM.Show(Cursor.Position);
                }
            }
            else if (e.Node.Tag is ExpansionLoot)
            {
                ExpansionLootCM.Items.Clear();
                ExpansionLootCM.Items.Add(removeToolStripMenuItem);
                ExpansionLootCM.Show(Cursor.Position);
            }
            else if (e.Node.Tag is ExpansionLootVariant)
            {
                ExpansionLootCM.Items.Clear();
                ExpansionLootCM.Items.Add(removeToolStripMenuItem);
                ExpansionLootCM.Show(Cursor.Position);
            }
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
            if (CurrentLootVariant == null) return;
            CurrentLootVariant.Chance = ((decimal)trackBar2.Value) / 100;
            
        }

        private void ExpansionLootitemSetAllChanceButton_Click(object sender, EventArgs e)
        {
            foreach (ExpansionLoot el in _data)
            {
                el.Chance = ((decimal)trackBar2.Value) / 100;
            }
            
        }

        private void ExpansionLootitemSetAllRandomChanceButton_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            foreach (ExpansionLoot el in _data)
            {
                TypeEntry type = AppServices.GetRequired<EconomyManager>().TypesConfig.GetTypeByName(el.Name);
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
            
        }

        private void numericUpDown31_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            currentExpanionLootItem.QuantityPercent = (int)numericUpDown31.Value;
            
        }

        private void numericUpDown12_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            currentExpanionLootItem.Max = (int)numericUpDown12.Value;
            
        }

        private void numericUpDown33_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            currentExpanionLootItem.Min = (int)numericUpDown33.Value;
            
        }

        private void addLootItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes
            {
                UseMultipleOfSameItem = true
            };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                TreeNode FocusNode = new TreeNode();
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    ExpansionLoot Newloot = new ExpansionLoot()
                    {
                        Name = l,
                        Attachments = new BindingList<ExpansionLootVariant>(),
                        Chance = (decimal)0.5,
                        Max = -1,
                        Min = 0,
                        Variants = new BindingList<ExpansionLootVariant>()
                    };
                    _data.Add(Newloot);
                    TreeNode tn = CreateLootNode(Newloot);
                    ExpansionLootTV.SelectedNode.Nodes.Add(tn);
                    FocusNode = tn;
                    
                }
                ExpansionLootTV.SelectedNode = FocusNode;
                ExpansionLootTV.Focus();
                currentExpanionLootItem = ExpansionLootTV.SelectedNode.Tag as ExpansionLoot;
                SetLootitem();
            }
        }

        private void addLootVariantsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes
            {
            };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                TreeNode FocusNode = new TreeNode();
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    ExpansionLootVariant Newloot = new ExpansionLootVariant(l);
                    ExpansionLoot loot = currentTreeNode.Parent.Tag as ExpansionLoot;
                    loot.Variants.Add(Newloot);
                    TreeNode tn = getLootVariants(Newloot);
                    ExpansionLootTV.SelectedNode.Nodes.Add(tn);
                    FocusNode = tn;
                    
                }
                ExpansionLootTV.SelectedNode = FocusNode;
                ExpansionLootTV.Focus();
                expansionLootVariantGB.Visible = true;
                CurrentLootVariant = currentTreeNode.Tag as ExpansionLootVariant;
                setvariant();
                if (currentTreeNode.Parent.Tag.ToString() == "Attachments")
                {
                    expansionLootVariantGB.Text = "Expansion Loot Attachment";
                }
                else if (currentTreeNode.Parent.Tag.ToString() == "Variants")
                {
                    expansionLootVariantGB.Text = "Expansion Loot Variant";
                }
            }
        }

        private void addAttachmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes
            {
            };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                TreeNode FocusNode = new TreeNode();
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    if (currentTreeNode.Parent.Tag is ExpansionLoot)
                    {
                        ExpansionLootVariant Newloot = new ExpansionLootVariant(l);
                        ExpansionLoot loot = currentTreeNode.Parent.Tag as ExpansionLoot;
                        loot.Attachments.Add(Newloot);
                        TreeNode tn = getLootVariants(Newloot);
                        ExpansionLootTV.SelectedNode.Nodes.Add(tn);
                        FocusNode = tn;
                    }
                    else if (currentTreeNode.Parent.Tag is ExpansionLootVariant)
                    {
                        ExpansionLootVariant Newloot = new ExpansionLootVariant(l);
                        ExpansionLootVariant loot = currentTreeNode.Parent.Tag as ExpansionLootVariant;
                        loot.Attachments.Add(Newloot);
                        TreeNode tn = getLootVariants(Newloot);
                        ExpansionLootTV.SelectedNode.Nodes.Add(tn);
                        FocusNode = tn;
                    }
                    
                }
                ExpansionLootTV.SelectedNode = FocusNode;
                ExpansionLootTV.Focus();
                expansionLootVariantGB.Visible = true;
                CurrentLootVariant = ExpansionLootTV.SelectedNode.Tag as ExpansionLootVariant;
                setvariant();
                if (currentTreeNode.Parent.Tag.ToString() == "Attachments")
                {
                    expansionLootVariantGB.Text = "Expansion Loot Attachment";
                }
                else if (currentTreeNode.Parent.Tag.ToString() == "Variants")
                {
                    expansionLootVariantGB.Text = "Expansion Loot Variant";
                }
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTreeNode.Parent.Tag.ToString() == "LootParent")
            {
                _data.Remove(currentExpanionLootItem);
                ExpansionLootTV.SelectedNode.Remove();
            }
            else if (currentTreeNode.Parent.Tag.ToString() == "Variants")
            {
                ExpansionLoot loot = currentTreeNode.Parent.Parent.Tag as ExpansionLoot;
                loot.Variants.Remove(CurrentLootVariant);
                ExpansionLootTV.SelectedNode.Remove();
            }
            else if (currentTreeNode.Parent.Tag.ToString() == "Attachments")
            {
                if (currentTreeNode.Parent.Parent.Tag is ExpansionLoot)
                {
                    ExpansionLoot loot = currentTreeNode.Parent.Parent.Tag as ExpansionLoot;
                    loot.Attachments.Remove(CurrentLootVariant);
                    ExpansionLootTV.SelectedNode.Remove();
                }
                else if (currentTreeNode.Parent.Parent.Tag is ExpansionLootVariant)
                {
                    ExpansionLootVariant loot = currentTreeNode.Parent.Parent.Tag as ExpansionLootVariant;
                    loot.Attachments.Remove(CurrentLootVariant);
                    ExpansionLootTV.SelectedNode.Remove();
                }
            }

            
        }
    }
}