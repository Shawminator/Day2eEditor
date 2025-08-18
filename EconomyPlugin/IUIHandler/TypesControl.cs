using Day2eEditor;
using System.ComponentModel;
using System.Data;

namespace EconomyPlugin
{
    public partial class TypesControl : UserControl, IUIHandler
    {
        private TypeEntry _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;
        private TypeEntry _originalData;

        public Control GetControl() => this;
        private void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox lb = sender as ListBox;
            e.DrawBackground();
            if (lb.Items.Count == 0) return;
            Brush myBrush = Brushes.Black;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            }
            else
            {
                myBrush = Brushes.White;
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(60, 63, 65)), e.Bounds);
            }
            e.Graphics.DrawString(lb.Items[e.Index].ToString(), e.Font, myBrush, e.Bounds);
            e.DrawFocusRectangle();
        }
        public TypesControl()
        {
            InitializeComponent();
            PopulateDefs();
            setNumberofTiers();
        }
        public void LoadFromData(object data, List<TreeNode> selectedNodes)
        {
            _data = data as TypeEntry ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset
            _suppressEvents = true;

            textBox1.Text = _data.Name;
            if (_data.Category == null)
                comboBox1.SelectedIndex = 0;
            else
                comboBox1.SelectedIndex = comboBox1.FindStringExact(_data.Category.Name);
            populateUsage();
            PopulateCounts();
            PopulateFlags();
            PopulateTiers();
            PopulateTags();
            _suppressEvents = false;
        }
        private void PopulateTiers()
        {
            List<CheckBox> checkboxes = flowLayoutPanel1.Controls.OfType<CheckBox>().ToList();
            foreach (CheckBox cb in checkboxes)
            {
                cb.Checked = false; ;
            }
            checkboxes = flowLayoutPanel2.Controls.OfType<CheckBox>().ToList();
            foreach (CheckBox cb in checkboxes)
            {
                cb.Checked = false;
            }
            if (_data != null && _data.Values != null)
            {
                for (int i = 0; i < _data.Values.Count; i++)
                {
                    if (_data.Values[i].User != null && _data.Values[i].User.Count() > 0 && _data.Values[i].Name == null)
                    {
                        tabControl3.SelectedIndex = 1;
                        try
                        {
                            flowLayoutPanel2.Controls.OfType<CheckBox>().First(x => x.Tag.ToString() == _data.Values[i].User).Checked = true;
                        }
                        catch
                        {
                            _data.Values.RemoveAt(i);
                            i--;
                        }
                    }
                    else
                    {
                        tabControl3.SelectedIndex = 0;

                        try
                        {
                            flowLayoutPanel1.Controls.OfType<CheckBox>().First(x => x.Tag.ToString() == _data.Values[i].Name).Checked = true;
                        }
                        catch
                        {
                            _data.Values.RemoveAt(i);
                            i--;
                        }
                    }
                }
            }
        }
        public void setNumberofTiers()
        {
            _suppressEvents = true;
            List<CheckBox> checkboxes = flowLayoutPanel1.Controls.OfType<CheckBox>().ToList();

            foreach (CheckBox cb in checkboxes)
            {
                cb.Visible = false;

            }

            int index = AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionConfig.Data.valueflags.Count;
            for (int i = 0; i < AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionConfig.Data.valueflags.Count; i++)
            {
                listsValue value = AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionConfig.Data.valueflags[i];
                CheckBox cb = checkboxes[i];
                cb.Tag = value.name;
                cb.Checked = false;
                cb.Visible = true;
                cb.Text = value.name;
                index--;
            }

            checkboxes = flowLayoutPanel2.Controls.OfType<CheckBox>().ToList();
            checkboxes = checkboxes.OrderBy(x => x.Name).ToList();
            foreach (CheckBox cb in checkboxes)
            {
                cb.Visible = false;
            }


            index = 0;
            foreach (user_listsUser1 user in AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionuserConfig.Data.valueflags)
            {
                CheckBox cb = checkboxes[index];
                cb.Tag = user.name;
                cb.Visible = true;
                cb.Checked = false;
                cb.Text = user.name;
                index++;
            }
            _suppressEvents = false;
        }
        private void PopulateDefs()
        {
            _suppressEvents = true;
            BindingList<listsCategory> newlist = new BindingList<listsCategory>
            {
                new listsCategory()
                {
                    name = "other"
                }
            };
            foreach (listsCategory cat in AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionConfig.Data.categories)
            {
                newlist.Add(cat);
            }
            comboBox1.DataSource = newlist;
            comboBox4.DataSource = AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionConfig.Data.tags;
            List<object> usagelist = new List<object>();
            usagelist.AddRange(AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionConfig.Data.usageflags);
            usagelist.AddRange(AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionuserConfig.Data.usageflags);

            comboBox2.DataSource = usagelist;
            if (comboBox2.Items.Count == 0)
                groupBox2.Visible = false;
            else
                groupBox2.Visible = true;
            _suppressEvents = false;
        }
        private void PopulateCounts()
        {
            if (typeNomCountNUD.Visible = NomCountCB.Checked = _data.NominalSpecified)
                typeNomCountNUD.Value = (decimal)_data.Nominal;
            if (typeMinCountNUD.Visible = MinCountCB.Checked = _data.MinSpecified)
                typeMinCountNUD.Value = (decimal)_data.Min;
            typeLifetimeNUD.Visible = true;
            typeLifetimeNUD.Value = (decimal)_data.Lifetime;
            if (typeRestockNUD.Visible = RestockCB.Checked = _data.RestockSpecified)
                typeRestockNUD.Value = (decimal)_data.Restock;
            if (typeQuantMINNUD.Visible = QuanMinCB.Checked = _data.QuantMinSpecified)
                typeQuantMINNUD.Value = (decimal)_data.QuantMin;
            if (typeQuantMAXNUD.Visible = QuanMaxCB.Checked = _data.QuantMaxSpecified)
                if (typeCostNUD.Visible = costCB.Checked = _data.CostSpecified)
                    typeCostNUD.Value = (decimal)_data.Cost;
        }
        private void populateUsage()
        {
            listBox1.DisplayMember = "Name";
            listBox1.ValueMember = "Value";
            if (_data.Usages != null)
            {



                listBox1.DataSource = _data.Usages;
            }
        }
        private void PopulateFlags()
        {
            if (_data.Flags != null)
            {
                checkBox1.Checked = _data.Flags.count_in_cargo == 1 ? true : false;
                checkBox2.Checked = _data.Flags.count_in_hoarder == 1 ? true : false;
                checkBox3.Checked = _data.Flags.count_in_map == 1 ? true : false;
                checkBox4.Checked = _data.Flags.count_in_player == 1 ? true : false;
                checkBox5.Checked = _data.Flags.crafted == 1 ? true : false;
                checkBox6.Checked = _data.Flags.deloot == 1 ? true : false;
            }
            else
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
            }
        }
        private void PopulateTags()
        {
            listBox2.DisplayMember = "Name";
            listBox2.ValueMember = "Value";
            listBox2.DataSource = _data.Tags;
        }
        public void ApplyChanges()
        {

        }
        public void Reset()
        {

        }
        public void HasChanges()
        {
            TypesFile tf = _nodes.Last().Parent.Parent.Tag as TypesFile;
            // Compare current data with original data to check if changes were made
            if (!_data.Equals(_originalData))
            {
                tf.isDirty = true;
            }
        }
        private TypeEntry CloneData(TypeEntry data)
        {
            return new TypeEntry
            {
                Name = data.Name,
                NameSpecified = data.NameSpecified,

                Nominal = data.Nominal,
                NominalSpecified = data.NominalSpecified,

                Lifetime = data.Lifetime,
                LifetimeSpecified = data.LifetimeSpecified,

                Restock = data.Restock,
                RestockSpecified = data.RestockSpecified,

                Min = data.Min,
                MinSpecified = data.MinSpecified,

                QuantMin = data.QuantMin,
                QuantMinSpecified = data.QuantMinSpecified,

                QuantMax = data.QuantMax,
                QuantMaxSpecified = data.QuantMaxSpecified,

                Cost = data.Cost,
                CostSpecified = data.CostSpecified,

                Flags = data.Flags != null
                    ? new Flags
                    {
                        count_in_cargo = data.Flags.count_in_cargo,
                        count_in_hoarder = data.Flags.count_in_hoarder,
                        count_in_map = data.Flags.count_in_map,
                        count_in_player = data.Flags.count_in_player,
                        crafted = data.Flags.crafted,
                        deloot = data.Flags.deloot
                    }
                    : null,

                Category = data.Category != null
                    ? new Category
                    {
                        Name = data.Category.Name,
                        NameSpecified = data.Category.NameSpecified
                    }
                    : null,

                Usages = new BindingList<Usage>(
                    data.Usages.Select(u => new Usage
                    {
                        Name = u.Name,
                        NameSpecified = u.NameSpecified,
                        User = u.User,
                        UserSpecified = u.UserSpecified
                    }).ToList()
                ),

                Tags = new BindingList<Tag>(
                    data.Tags.Select(t => new Tag
                    {
                        Name = t.Name,
                        NameSpecified = t.NameSpecified
                    }).ToList()
                ),

                Values = new BindingList<Value>(
                    data.Values.Select(v => new Value
                    {
                        Name = v.Name,
                        NameSpecified = v.NameSpecified,
                        User = v.User,
                        UserSpecified = v.UserSpecified
                    }).ToList()
                )
            };
        }

        private void NomCountCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (TreeNode tn in _nodes)
            {
                TypeEntry typeentry = tn.Tag as TypeEntry;
                typeNomCountNUD.Visible = typeentry.NominalSpecified = NomCountCB.Checked;
                typeNomCountNUD.Value = 0;
                typeentry.Nominal = (int)typeNomCountNUD.Value;
            }
            HasChanges();
        }
        private void typeNomCountNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (TreeNode tn in _nodes)
            {
                TypeEntry looptype = tn.Tag as TypeEntry;
                if (looptype.NominalSpecified)
                {
                    looptype.Nominal = (int)typeNomCountNUD.Value;
                }
            }
            HasChanges();
        }
        private void MinCountCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (TreeNode tn in _nodes)
            {
                TypeEntry typeentry = tn.Tag as TypeEntry;
                typeMinCountNUD.Visible = typeentry.MinSpecified = MinCountCB.Checked;
                typeMinCountNUD.Value = 0;
                typeentry.Min = (int)typeMinCountNUD.Value;
            }
            HasChanges();
        }
        private void typeMinCountNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (TreeNode tn in _nodes)
            {
                TypeEntry looptype = tn.Tag as TypeEntry;
                if (looptype.MinSpecified)
                {
                    looptype.Min = (int)typeMinCountNUD.Value;
                }
            }
            HasChanges();
        }
        private void typeLifetimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (TreeNode tn in _nodes)
            {
                TypeEntry looptype = tn.Tag as TypeEntry;
                looptype.Lifetime = (int)typeLifetimeNUD.Value;
            }
            HasChanges();
        }
        private void RestockCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (TreeNode tn in _nodes)
            {
                TypeEntry typeentry = tn.Tag as TypeEntry;
                typeRestockNUD.Visible = typeentry.RestockSpecified = RestockCB.Checked;
                typeRestockNUD.Value = 0;
                typeentry.Min = (int)typeRestockNUD.Value;
            }
            HasChanges();
        }
        private void typeRestockNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (TreeNode tn in _nodes)
            {
                TypeEntry looptype = tn.Tag as TypeEntry;
                if (looptype.RestockSpecified)
                {
                    looptype.Restock = (int)typeRestockNUD.Value;
                }
            }
            HasChanges();
        }
        private void QuanMinCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (TreeNode tn in _nodes)
            {
                TypeEntry typeentry = tn.Tag as TypeEntry;
                typeQuantMINNUD.Visible = typeentry.QuantMinSpecified = QuanMinCB.Checked;
                typeQuantMINNUD.Value = 0;
                typeentry.QuantMin = (int)typeQuantMINNUD.Value;
            }
            HasChanges();
        }
        private void typeQuantMINNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (TreeNode tn in _nodes)
            {
                TypeEntry looptype = tn.Tag as TypeEntry;
                if (looptype.QuantMinSpecified)
                {
                    looptype.QuantMin = (int)typeQuantMINNUD.Value;
                }
            }
            HasChanges();
        }
        private void QuanMaxCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (TreeNode tn in _nodes)
            {
                TypeEntry typeentry = tn.Tag as TypeEntry;
                typeQuantMAXNUD.Visible = typeentry.QuantMaxSpecified = QuanMaxCB.Checked;
                typeQuantMAXNUD.Value = 0;
                typeentry.QuantMax = (int)typeQuantMAXNUD.Value;
            }
            HasChanges();
        }
        private void typeQuantMAXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (TreeNode tn in _nodes)
            {
                TypeEntry looptype = tn.Tag as TypeEntry;
                if (looptype.QuantMaxSpecified)
                {
                    looptype.QuantMax = (int)typeQuantMAXNUD.Value;
                }
            }
            HasChanges();
        }
        private void costCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (TreeNode tn in _nodes)
            {
                TypeEntry typeentry = tn.Tag as TypeEntry;
                typeCostNUD.Visible = typeentry.CostSpecified = costCB.Checked;
                typeCostNUD.Value = 0;
                typeentry.Cost = (int)typeCostNUD.Value;
            }
            HasChanges();
        }
        private void typeCostNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (TreeNode tn in _nodes)
            {
                TypeEntry looptype = tn.Tag as TypeEntry;
                if (looptype.CostSpecified)
                {
                    looptype.Cost = (int)typeCostNUD.Value;
                }
            }
            HasChanges();
        }

        private void TierCheckBoxchanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            CheckBox cb = sender as CheckBox;
            string tier = cb.Tag.ToString();
            foreach (TreeNode tn in _nodes)
            {
                TypeEntry looptype = tn.Tag as TypeEntry;
                if (cb.Checked)
                {
                    looptype.AddTier(tier);
                }
                else
                    looptype.removetier(tier);
            }
            _suppressEvents = true;
            PopulateTiers();
            _suppressEvents = false;
            HasChanges();
        }
        private void UserdefiniedTiersChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            CheckBox cb = sender as CheckBox;
            string tier = cb.Tag.ToString();
            foreach (TreeNode tn in _nodes)
            {
                TypeEntry looptype = tn.Tag as TypeEntry;
                if (cb.Checked)
                {
                    if (looptype.Values != null)
                    {
                        looptype.removetiers();
                    }
                    looptype.AdduserTier(tier);
                }
                else
                    looptype.removeusertier(tier);
            }
            _suppressEvents = true;
            PopulateTiers();
            _suppressEvents = false;
            HasChanges();
        }
        private void Button28_Click(object sender, EventArgs e)
        {
            foreach (TreeNode tn in _nodes)
            {
                TypeEntry looptype = tn.Tag as TypeEntry;
                looptype.removetiers();
            }
            _suppressEvents = true;
            PopulateTiers();
            _suppressEvents = false;
            HasChanges();
        }

        private void checkBox117_CheckedChanged(object sender, EventArgs e)
        {
            switch (checkBox117.Checked)
            {
                case true:
                    comboBox2.DataSource = AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionuserConfig.Data.usageflags;
                    if (comboBox2.Items.Count == 0)
                        groupBox2.Visible = false;
                    else
                        groupBox2.Visible = true;
                    break;
                case false:
                    List<object> usagelist = new List<object>();
                    usagelist.AddRange(AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionConfig.Data.usageflags);
                    usagelist.AddRange(AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionuserConfig.Data.usageflags);

                    comboBox2.DataSource = usagelist;
                    if (comboBox2.Items.Count == 0)
                        groupBox2.Visible = false;
                    else
                        groupBox2.Visible = true;
                    break;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem is listsUsage u)
            {
                foreach (TreeNode tn in _nodes)
                {
                    TypeEntry looptype = tn.Tag as TypeEntry;
                    looptype.AddnewUsage(u);
                }
            }
            if (comboBox2.SelectedItem is user_listsUser uu)
            {
                foreach (TreeNode tn in _nodes)
                {
                    TypeEntry looptype = tn.Tag as TypeEntry;
                    looptype.AddnewUserUsage(uu);

                }
            }
            HasChanges();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Usage u = listBox1.SelectedItem as Usage;
            foreach (TreeNode tn in _nodes)
            {
                TypeEntry looptype = tn.Tag as TypeEntry;
                looptype.removeusage(u);
            }
            HasChanges();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            listsTag t = comboBox4.SelectedItem as listsTag;
            foreach (TreeNode tn in _nodes)
            {
                TypeEntry looptype = tn.Tag as TypeEntry;
                looptype.Addnewtag(t);
            }
            HasChanges();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Tag t = listBox2.SelectedItem as Tag;
            foreach (TreeNode tn in _nodes)
            {
                TypeEntry looptype = tn.Tag as TypeEntry;
                looptype.removetag(t);
            }
            HasChanges();
        }

        private void flags_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (TreeNode tn in _nodes)
            {
                TypeEntry looptype = tn.Tag as TypeEntry;
                CheckBox cb = sender as CheckBox;
                switch (cb.Name)
                {
                    case "checkBox1":
                        looptype.Flags.count_in_cargo = checkBox1.Checked == true ? 1 : 0;
                        break;
                    case "checkBox2":
                        looptype.Flags.count_in_hoarder = checkBox2.Checked == true ? 1 : 0;
                        break;
                    case "checkBox3":
                        looptype.Flags.count_in_map = checkBox3.Checked == true ? 1 : 0;
                        break;
                    case "checkBox4":
                        looptype.Flags.count_in_player = checkBox4.Checked == true ? 1 : 0;
                        break;
                    case "checkBox5":
                        looptype.Flags.crafted = checkBox5.Checked == true ? 1 : 0;
                        break;
                    case "checkBox6":
                        looptype.Flags.deloot = checkBox6.Checked == true ? 1 : 0;
                        break;
                }
            }
            HasChanges();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;

            listsCategory c = comboBox1.SelectedItem as listsCategory;

            TreeNode _lastNode = null; // ensure default value

            foreach (TreeNode tn in _nodes.ToList()) // Avoid modifying collection during iteration
            {
                _lastNode = tn;
                TypeEntry looptype = tn.Tag as TypeEntry;
                looptype.changecategory(c);

                // Save expanded state for the moving node
                bool wasExpanded = tn.IsExpanded;

                // Find the top-level category container
                TreeNode root = tn.Parent?.Parent;
                if (root == null) continue; // safety

                // Save expanded state for the root
                bool rootExpanded = root.IsExpanded;

                // Find or create the target category node
                TreeNode categoryNode = root.Nodes
                    .Cast<TreeNode>()
                    .FirstOrDefault(n => n.Text == c.name);

                if (categoryNode == null)
                {
                    categoryNode = new TreeNode(c.name);
                    root.Nodes.Add(categoryNode);
                }

                // Save expanded state of category node
                bool categoryExpanded = categoryNode.IsExpanded;

                // Remove and re-add to new category in alphabetical order
                TreeNode tnparent = tn.Parent;
                tn.Remove();
                if (tnparent.Nodes.Count == 0)
                {
                    tnparent.Remove();
                }
                int insertIndex = 0;
                while (insertIndex < categoryNode.Nodes.Count &&
                       string.Compare(categoryNode.Nodes[insertIndex].Text, tn.Text, StringComparison.OrdinalIgnoreCase) < 0)
                {
                    insertIndex++;
                }
                categoryNode.Nodes.Insert(insertIndex, tn);
            }
            HasChanges();
            if (_lastNode != null && _lastNode.TreeView != null)
            {
                _lastNode.TreeView.SelectedNode = _lastNode;
                _lastNode.EnsureVisible();
                _lastNode.TreeView.Focus();
            }
        }
    }
}
