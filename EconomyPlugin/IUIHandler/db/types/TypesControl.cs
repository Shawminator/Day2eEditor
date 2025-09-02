using Day2eEditor;
using System.ComponentModel;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EconomyPlugin
{
    public partial class TypesControl : UserControl, IUIHandler
    {
        private Type _parentType;
        public Control GetControl() => this;
        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _currentdata = data as TypeEntry ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            typesFile = _nodes.Last().FindParentOfType<TypesFile>();
            LoadNodesTotypeslist(_nodes);
            _originalData = CloneData(_entries); // Store original data for reset
            _suppressEvents = true;

            textBox1.Text = _currentdata.Name;
            if (_currentdata.Category == null)
                comboBox1.SelectedIndex = 0;
            else
                comboBox1.SelectedIndex = comboBox1.FindStringExact(_currentdata.Category.Name);
            populateUsage();
            PopulateCounts();
            PopulateFlags();
            PopulateTiers();
            PopulateTags();
            _suppressEvents = false;
        }
        public void ApplyChanges()
        {
            _originalData = CloneData(_entries);
        }
        public void Reset()
        {
        }
        public void HasChanges()
        {
            typesFile.isDirty = IsDirty();
        }
        public bool IsDirty()
        {
            if (_entries.Count != _originalData.Count)
                return true;

            return !_entries.SequenceEqual(_originalData);
        }
        private TypesFile typesFile;
        private TypeEntry _currentdata;
        private BindingList<TypeEntry> _entries;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;
        private BindingList<TypeEntry> _originalData;

        public TypesControl()
        {
            InitializeComponent();
            PopulateDefs();
            setNumberofTiers();
        }
        private void LoadNodesTotypeslist(List<TreeNode> selectedNodes)
        {
            _entries = new BindingList<TypeEntry>();
            foreach (TreeNode _node in selectedNodes)
            {
                _entries.Add(_node.Tag as TypeEntry);
            }
        }
        private BindingList<TypeEntry> CloneData(BindingList<TypeEntry> data)
        {
            return new BindingList<TypeEntry>(data.Select(d => d.Clone()).ToList());
        }
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
            if (_currentdata != null && _currentdata.Values != null)
            {
                for (int i = 0; i < _currentdata.Values.Count; i++)
                {
                    if (_currentdata.Values[i].User != null && _currentdata.Values[i].User.Count() > 0 && _currentdata.Values[i].Name == null)
                    {
                        tabControl3.SelectedIndex = 1;
                        try
                        {
                            flowLayoutPanel2.Controls.OfType<CheckBox>().First(x => x.Tag.ToString() == _currentdata.Values[i].User).Checked = true;
                        }
                        catch
                        {
                            _currentdata.Values.RemoveAt(i);
                            i--;
                        }
                    }
                    else
                    {
                        tabControl3.SelectedIndex = 0;

                        try
                        {
                            flowLayoutPanel1.Controls.OfType<CheckBox>().First(x => x.Tag.ToString() == _currentdata.Values[i].Name).Checked = true;
                        }
                        catch
                        {
                            _currentdata.Values.RemoveAt(i);
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
            if (typeNomCountNUD.Visible = NomCountCB.Checked = _currentdata.NominalSpecified)
                typeNomCountNUD.Value = (decimal)_currentdata.Nominal;
            if (typeMinCountNUD.Visible = MinCountCB.Checked = _currentdata.MinSpecified)
                typeMinCountNUD.Value = (decimal)_currentdata.Min;
            typeLifetimeNUD.Visible = true;
            typeLifetimeNUD.Value = (decimal)_currentdata.Lifetime;
            if (typeRestockNUD.Visible = RestockCB.Checked = _currentdata.RestockSpecified)
                typeRestockNUD.Value = (decimal)_currentdata.Restock;
            if (typeQuantMINNUD.Visible = QuanMinCB.Checked = _currentdata.QuantMinSpecified)
                typeQuantMINNUD.Value = (decimal)_currentdata.QuantMin;
            if (typeQuantMAXNUD.Visible = QuanMaxCB.Checked = _currentdata.QuantMaxSpecified)
                if (typeCostNUD.Visible = costCB.Checked = _currentdata.CostSpecified)
                    typeCostNUD.Value = (decimal)_currentdata.Cost;
        }
        private void populateUsage()
        {
            listBox1.DisplayMember = "Name";
            listBox1.ValueMember = "Value";
            if (_currentdata.Usages != null)
            {



                listBox1.DataSource = _currentdata.Usages;
            }
        }
        private void PopulateFlags()
        {
            if (_currentdata.Flags != null)
            {
                checkBox1.Checked = _currentdata.Flags.count_in_cargo == 1 ? true : false;
                checkBox2.Checked = _currentdata.Flags.count_in_hoarder == 1 ? true : false;
                checkBox3.Checked = _currentdata.Flags.count_in_map == 1 ? true : false;
                checkBox4.Checked = _currentdata.Flags.count_in_player == 1 ? true : false;
                checkBox5.Checked = _currentdata.Flags.crafted == 1 ? true : false;
                checkBox6.Checked = _currentdata.Flags.deloot == 1 ? true : false;
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
            listBox2.DataSource = _currentdata.Tags;
        }
        private void NomCountCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (TypeEntry typeentry in _entries)
            {
                typeNomCountNUD.Visible = typeentry.NominalSpecified = NomCountCB.Checked;
                typeNomCountNUD.Value = 0;
                typeentry.Nominal = (int)typeNomCountNUD.Value;
            }
            HasChanges();
        }
        private void typeNomCountNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (TypeEntry typeentry in _entries)
            {
                if (typeentry.NominalSpecified)
                {
                    typeentry.Nominal = (int)typeNomCountNUD.Value;
                }
            }
            HasChanges();
        }
        private void MinCountCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (TypeEntry typeentry in _entries)
            {

                typeMinCountNUD.Visible = typeentry.MinSpecified = MinCountCB.Checked;
                typeMinCountNUD.Value = 0;
                typeentry.Min = (int)typeMinCountNUD.Value;
            }
            HasChanges();
        }
        private void typeMinCountNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (TypeEntry typeentry in _entries)
            {
                if (typeentry.MinSpecified)
                {
                    typeentry.Min = (int)typeMinCountNUD.Value;
                }
            }
            HasChanges();
        }
        private void typeLifetimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (TypeEntry typeentry in _entries)
            {

                typeentry.Lifetime = (int)typeLifetimeNUD.Value;
            }
            HasChanges();
        }
        private void RestockCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (TypeEntry typeentry in _entries)
            {

                typeRestockNUD.Visible = typeentry.RestockSpecified = RestockCB.Checked;
                typeRestockNUD.Value = 0;
                typeentry.Min = (int)typeRestockNUD.Value;
            }
            HasChanges();
        }
        private void typeRestockNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (TypeEntry typeentry in _entries)
            {

                if (typeentry.RestockSpecified)
                {
                    typeentry.Restock = (int)typeRestockNUD.Value;
                }
            }
            HasChanges();
        }
        private void QuanMinCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (TypeEntry typeentry in _entries)
            {

                typeQuantMINNUD.Visible = typeentry.QuantMinSpecified = QuanMinCB.Checked;
                typeQuantMINNUD.Value = 0;
                typeentry.QuantMin = (int)typeQuantMINNUD.Value;
            }
            HasChanges();
        }
        private void typeQuantMINNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (TypeEntry typeentry in _entries)
            {

                if (typeentry.QuantMinSpecified)
                {
                    typeentry.QuantMin = (int)typeQuantMINNUD.Value;
                }
            }
            HasChanges();
        }
        private void QuanMaxCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (TypeEntry typeentry in _entries)
            {

                typeQuantMAXNUD.Visible = typeentry.QuantMaxSpecified = QuanMaxCB.Checked;
                typeQuantMAXNUD.Value = 0;
                typeentry.QuantMax = (int)typeQuantMAXNUD.Value;
            }
            HasChanges();
        }
        private void typeQuantMAXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (TypeEntry typeentry in _entries)
            {

                if (typeentry.QuantMaxSpecified)
                {
                    typeentry.QuantMax = (int)typeQuantMAXNUD.Value;
                }
            }
            HasChanges();
        }
        private void costCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (TypeEntry typeentry in _entries)
            {

                typeCostNUD.Visible = typeentry.CostSpecified = costCB.Checked;
                typeCostNUD.Value = 0;
                typeentry.Cost = (int)typeCostNUD.Value;
            }
            HasChanges();
        }
        private void typeCostNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (TypeEntry typeentry in _entries)
            {

                if (typeentry.CostSpecified)
                {
                    typeentry.Cost = (int)typeCostNUD.Value;
                }
            }
            HasChanges();
        }
        private void TierCheckBoxchanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            CheckBox cb = sender as CheckBox;
            string tier = cb.Tag.ToString();
            foreach (TypeEntry typeentry in _entries)
            {

                if (cb.Checked)
                {
                    typeentry.AddTier(tier);
                }
                else
                    typeentry.removetier(tier);
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
            foreach (TypeEntry typeentry in _entries)
            {

                if (cb.Checked)
                {
                    if (typeentry.Values != null)
                    {
                        typeentry.removetiers();
                    }
                    typeentry.AdduserTier(tier);
                }
                else
                    typeentry.removeusertier(tier);
            }
            _suppressEvents = true;
            PopulateTiers();
            _suppressEvents = false;
            HasChanges();
        }
        private void Button28_Click(object sender, EventArgs e)
        {
            foreach (TypeEntry typeentry in _entries)
            {

                typeentry.removetiers();
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
                foreach (TypeEntry typeentry in _entries)
                {
    
                    typeentry.AddnewUsage(u);
                }
            }
            if (comboBox2.SelectedItem is user_listsUser uu)
            {
                foreach (TypeEntry typeentry in _entries)
                {
    
                    typeentry.AddnewUserUsage(uu);

                }
            }
            HasChanges();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Usage u = listBox1.SelectedItem as Usage;
            foreach (TypeEntry typeentry in _entries)
            {

                typeentry.removeusage(u);
            }
            HasChanges();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            listsTag t = comboBox4.SelectedItem as listsTag;
            foreach (TypeEntry typeentry in _entries)
            {

                typeentry.Addnewtag(t);
            }
            HasChanges();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Tag t = listBox2.SelectedItem as Tag;
            foreach (TypeEntry typeentry in _entries)
            {

                typeentry.removetag(t);
            }
            HasChanges();
        }
        private void flags_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (TypeEntry typeentry in _entries)
            {

                CheckBox cb = sender as CheckBox;
                switch (cb.Name)
                {
                    case "checkBox1":
                        typeentry.Flags.count_in_cargo = checkBox1.Checked == true ? 1 : 0;
                        break;
                    case "checkBox2":
                        typeentry.Flags.count_in_hoarder = checkBox2.Checked == true ? 1 : 0;
                        break;
                    case "checkBox3":
                        typeentry.Flags.count_in_map = checkBox3.Checked == true ? 1 : 0;
                        break;
                    case "checkBox4":
                        typeentry.Flags.count_in_player = checkBox4.Checked == true ? 1 : 0;
                        break;
                    case "checkBox5":
                        typeentry.Flags.crafted = checkBox5.Checked == true ? 1 : 0;
                        break;
                    case "checkBox6":
                        typeentry.Flags.deloot = checkBox6.Checked == true ? 1 : 0;
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
                TypeEntry typeentry = tn.Tag as TypeEntry;
                typeentry.changecategory(c);

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
