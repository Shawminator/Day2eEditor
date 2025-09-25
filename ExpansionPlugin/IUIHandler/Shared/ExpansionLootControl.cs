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
                parent.isDirty = !AreLootListsEqual(_data, _originalData);
            }
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
            var clonedList = new BindingList<ExpansionLoot>();

            foreach (var loot in data)
            {
                var clonedLoot = new ExpansionLoot
                {
                    Name = loot.Name,
                    Chance = loot.Chance,
                    QuantityPercent = loot.QuantityPercent,
                    Max = loot.Max,
                    Min = loot.Min,
                    Attachments = CloneVariants(loot.Attachments),
                    Variants = CloneVariants(loot.Variants)
                };

                clonedList.Add(clonedLoot);
            }

            return clonedList;
        }

        private BindingList<ExpansionLootVariant> CloneVariants(BindingList<ExpansionLootVariant> variants)
        {
            var clonedVariants = new BindingList<ExpansionLootVariant>();

            foreach (var variant in variants)
            {
                var clonedVariant = new ExpansionLootVariant
                {
                    Name = variant.Name,
                    Chance = variant.Chance,
                    Attachments = CloneVariants(variant.Attachments)
                };

                clonedVariants.Add(clonedVariant);
            }

            return clonedVariants;
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
                    ExpansionLootCM.Items.Add(addLootVarientsToolStripMenuItem);
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
            if (_suppressEvents) return;
            currentExpanionLootItem.QuantityPercent = (int)numericUpDown31.Value;
            HasChanges();
        }

        private void numericUpDown12_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            currentExpanionLootItem.Max = (int)numericUpDown12.Value;
            HasChanges();
        }

        private void numericUpDown33_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            currentExpanionLootItem.Min = (int)numericUpDown33.Value;
            HasChanges();
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
                    HasChanges();
                }
                ExpansionLootTV.SelectedNode = FocusNode;
                ExpansionLootTV.Focus();
                currentExpanionLootItem = ExpansionLootTV.SelectedNode.Tag as ExpansionLoot;
                SetLootitem();
            }
        }

        private void addLootVarientsToolStripMenuItem_Click(object sender, EventArgs e)
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
                    TreeNode tn = getLootVarients(Newloot);
                    ExpansionLootTV.SelectedNode.Nodes.Add(tn);
                    FocusNode = tn;
                    HasChanges();
                }
                ExpansionLootTV.SelectedNode = FocusNode;
                ExpansionLootTV.Focus();
                expansionLootVarientGB.Visible = true;
                CurrentLootVArient = currentTreeNode.Tag as ExpansionLootVariant;
                setvarient();
                if (currentTreeNode.Parent.Tag.ToString() == "Attachments")
                {
                    expansionLootVarientGB.Text = "Expansion Loot Attachment";
                }
                else if (currentTreeNode.Parent.Tag.ToString() == "Variants")
                {
                    expansionLootVarientGB.Text = "Expansion Loot Variant";
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
                        TreeNode tn = getLootVarients(Newloot);
                        ExpansionLootTV.SelectedNode.Nodes.Add(tn);
                        FocusNode = tn;
                    }
                    else if (currentTreeNode.Parent.Tag is ExpansionLootVariant)
                    {
                        ExpansionLootVariant Newloot = new ExpansionLootVariant(l);
                        ExpansionLootVariant loot = currentTreeNode.Parent.Tag as ExpansionLootVariant;
                        loot.Attachments.Add(Newloot);
                        TreeNode tn = getLootVarients(Newloot);
                        ExpansionLootTV.SelectedNode.Nodes.Add(tn);
                        FocusNode = tn;
                    }
                    HasChanges();
                }
                ExpansionLootTV.SelectedNode = FocusNode;
                ExpansionLootTV.Focus();
                expansionLootVarientGB.Visible = true;
                CurrentLootVArient = ExpansionLootTV.SelectedNode.Tag as ExpansionLootVariant;
                setvarient();
                if (currentTreeNode.Parent.Tag.ToString() == "Attachments")
                {
                    expansionLootVarientGB.Text = "Expansion Loot Attachment";
                }
                else if (currentTreeNode.Parent.Tag.ToString() == "Variants")
                {
                    expansionLootVarientGB.Text = "Expansion Loot Variant";
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
                loot.Variants.Remove(CurrentLootVArient);
                ExpansionLootTV.SelectedNode.Remove();
            }
            else if (currentTreeNode.Parent.Tag.ToString() == "Attachments")
            {
                if (currentTreeNode.Parent.Parent.Tag is ExpansionLoot)
                {
                    ExpansionLoot loot = currentTreeNode.Parent.Parent.Tag as ExpansionLoot;
                    loot.Attachments.Remove(CurrentLootVArient);
                    ExpansionLootTV.SelectedNode.Remove();
                }
                else if (currentTreeNode.Parent.Parent.Tag is ExpansionLootVariant)
                {
                    ExpansionLootVariant loot = currentTreeNode.Parent.Parent.Tag as ExpansionLootVariant;
                    loot.Attachments.Remove(CurrentLootVArient);
                    ExpansionLootTV.SelectedNode.Remove();
                }
            }

            HasChanges();
        }
    }
}