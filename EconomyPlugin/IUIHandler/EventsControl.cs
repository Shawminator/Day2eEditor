using Day2eEditor;
using System.ComponentModel;
using System.Windows.Forms.Design;

namespace EconomyPlugin
{
    public partial class EventsControl : UserControl, IUIHandler
    {
        private eventsEvent _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;
        private eventsEvent _originalData;
        private Category Cat;
        private bool isCat = false;

        private eventsEventChild CurrentChild;
        public BindingList<string> AllEvents;

        public Control GetControl() => this;
        public EventsControl()
        {
            InitializeComponent();
            _suppressEvents = true;
            positionComboBox.DataSource = Enum.GetValues(typeof(position));
            limitComboBox.DataSource = Enum.GetValues(typeof(limit));
            AllEvents = new BindingList<string>();
            foreach (EventsFile eventfile in AppServices.GetRequired<EconomyManager>().eventsConfig.AllData)
            {
                foreach (eventsEvent cevents in eventfile.Data.@event)
                {
                    if (!AllEvents.Contains(cevents.name))
                        AllEvents.Add(cevents.name);
                }
            }
            var sortedListInstance = new BindingList<string>(AllEvents.OrderBy(x => x).ToList());
            sortedListInstance.Insert(0, "None");
            SecondaryCB.DisplayMember = "DisplayName";
            SecondaryCB.ValueMember = "Value";
            SecondaryCB.DataSource = sortedListInstance;
            _suppressEvents = false;
        }
        public void LoadFromData(object data, List<TreeNode> selectedNodes)
        {
            _data = data as eventsEvent ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data);
            _suppressEvents = true;

            ClearChildren();
            positionComboBox.SelectedItem = (position)_data.position;
            limitComboBox.SelectedItem = (limit)_data.limit;

            nameTB.Text = _data.name;
            nominalNUD.Value = (int)_data.nominal;
            minNUD.Value = (int)_data.min;
            maxNUD.Value = (int)_data.max;
            lifetimeNUD.Value = (int)_data.lifetime;
            restockNUD.Value = (int)_data.restock;
            saferadiusNUD.Value = (int)_data.saferadius;
            distanceradiusNUD.Value = (int)_data.distanceradius;
            cleanupradiusNUD.Value = (int)_data.cleanupradius;
            deletableCB.Checked = _data.flags.deletable == 1 ? true : false;
            init_randomCB.Checked = _data.flags.init_random == 1 ? true : false;
            remove_damagedCB.Checked = _data.flags.remove_damaged == 1 ? true : false;
            activeCB.Checked = _data.active == 1 ? true : false;
            if (_data.secondary != null)
                SecondaryCB.SelectedIndex = SecondaryCB.FindStringExact(_data.secondary);
            else
                SecondaryCB.SelectedIndex = SecondaryCB.FindStringExact("None");

            ChildrenLB.DisplayMember = "DisplayName";
            ChildrenLB.ValueMember = "Value";
            ChildrenLB.DataSource = _data.children;

            _suppressEvents = false;
        }
        private eventsEvent CloneData(eventsEvent source)
        {
            if (source == null) return null;

            var clone = new eventsEvent
            {
                nominal = source.nominal,
                min = source.min,
                max = source.max,
                lifetime = source.lifetime,
                restock = source.restock,
                saferadius = source.saferadius,
                distanceradius = source.distanceradius,
                cleanupradius = source.cleanupradius,
                secondary = source.secondary,
                active = source.active,
                name = source.name,

                // enums can be assigned directly
                position = source.position,
                limit = source.limit
            };

            // Clone Flags
            if (source.flags != null)
            {
                clone.flags = new eventsEventFlags
                {
                    deletable = source.flags.deletable,
                    init_random = source.flags.init_random,
                    remove_damaged = source.flags.remove_damaged
                };
            }

            // Clone Children
            if (source.children != null)
            {
                clone.children = new BindingList<eventsEventChild>();
                foreach (var child in source.children)
                {
                    var childClone = new eventsEventChild
                    {
                        lootmax = child.lootmax,
                        lootmin = child.lootmin,
                        max = child.max,
                        min = child.min,
                        type = child.type
                    };
                    clone.children.Add(childClone);
                }
            }

            return clone;
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
        private void ClearChildren()
        {
            ChildrenLB.DataSource = null;
            ChildGB.Visible = false;
        }
        public void ApplyChanges()
        {

        }
        public void Reset()
        {

        }
        public void HasChanges()
        {
            EventsFile ef = _nodes.Last().Parent.Tag as EventsFile;
            if (!_data.Equals(_originalData))
            {
                ef.isDirty = true;
            }
        }
        private void ChildrenLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ChildrenLB.SelectedItem as eventsEventChild == CurrentChild) { return; }
            if (ChildrenLB.SelectedIndex == -1) { return; }
            CurrentChild = ChildrenLB.SelectedItem as eventsEventChild;
            _suppressEvents = true;
            ChildGB.Visible = true;
            ClootmaxNUD.Value = (int)CurrentChild.lootmax;
            ClootminNUD.Value = (int)CurrentChild.lootmin;
            CmaxNUD.Value = (int)CurrentChild.max;
            CminNUD.Value = (int)CurrentChild.min;
            CtypeTB.Text = CurrentChild.type;
            _suppressEvents = false;
        }
        private void CreateeventSpawnButton_Click(object sender, EventArgs e)
        {
            eventposdefEvent newvenspawn = new eventposdefEvent()
            {
                name = _data.name,
                zone = null,
                pos = null
            };
            _nodes[0].Nodes.Add(new TreeNode($"Event Spawn: {newvenspawn.name}")
            {
                Tag = newvenspawn
            });
            AppServices.GetRequired<EconomyManager>().cfgeventspawnsConfig.AddNewEventSpawn(newvenspawn);
        }
        private void nameTB_Validated(object sender, EventArgs e)
        {
            ApplyNameChange();
        }
        private void ApplyNameChange()
        {
            if (_suppressEvents) return;
            eventposdefEvent points = AppServices.GetRequired<EconomyManager>().cfgeventspawnsConfig.Findevent(_data.name);
            if(points != null)
            {
                DialogResult dialogResult = MessageBox.Show("Exisitng Event spawn found, Do you want me to rename that as well?", "Rename Event Spawn", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    points.name = nameTB.Text;
                    _nodes[0].Nodes[0].Text = $"Event Spawn: {nameTB.Text}";
                    AppServices.GetRequired<EconomyManager>().cfgeventspawnsConfig.isDirty = true;
                }
            }
            _data.name = nameTB.Text;
            _nodes[0].Text = _data.name;
            
            HasChanges();
        }
        private void EventsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            NumericUpDown nud = sender as NumericUpDown;
            if (nud == null) return;
            _data.SetIntValue(nud.Name.Substring(0, nud.Name.Length - 3), (int)nud.Value);
            HasChanges();
        }
        private void EventsChildNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            NumericUpDown nud = sender as NumericUpDown;
            CurrentChild.SetIntValue(nud.Name.Substring(1, nud.Name.Length - 4), (int)nud.Value);
            HasChanges();
        }
        private void activeCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.active = activeCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void deletableCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.flags.deletable = deletableCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void init_randomCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.flags.init_random = init_randomCB.Checked == true ? 1 : 0;
            HasChanges();

        }
        private void remove_damagedCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.flags.remove_damaged = remove_damagedCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void positionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            position pos = (position)positionComboBox.SelectedItem;
            _data.position = pos;
            HasChanges();
        }
        private void limitComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            limit lim = (limit)limitComboBox.SelectedItem;
            _data.limit = lim;
            HasChanges();
        }
        private void SecondaryCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (SecondaryCB.GetItemText(SecondaryCB.SelectedItem) == "None")
                _data.secondary = null;
            else
                _data.secondary = SecondaryCB.GetItemText(SecondaryCB.SelectedItem);
            HasChanges();
        }
        private void darkButton60_Click(object sender, EventArgs e)
        {
            AllEvents = new BindingList<string>();
            foreach (EventsFile eventsconfig in AppServices.GetRequired<EconomyManager>().eventsConfig.AllData)
            {
                foreach (eventsEvent cevents in eventsconfig.Data.@event)
                {
                    AllEvents.Add(cevents.name);
                }
            }
            var sortedListInstance = new BindingList<string>(AllEvents.OrderBy(x => x).ToList());
            sortedListInstance.Insert(0, "None");
            SecondaryCB.DisplayMember = "DisplayName";
            SecondaryCB.ValueMember = "Value";
            SecondaryCB.DataSource = sortedListInstance;
        }
        private void darkButton19_Click(object sender, EventArgs e)
        {
            eventsEventChild newventeventschild = new eventsEventChild()
            {
                type = "newChild",
                lootmax = 0,
                lootmin = 0,
                max = 0,
                min = 0
            };
            _data.Addnechild(newventeventschild);
            HasChanges();
        }
        private void darkButton18_Click(object sender, EventArgs e)
        {
            _data.Removechild(CurrentChild);
            HasChanges();
        }
        private void CtypeTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            CurrentChild.type = CtypeTB.Text;
            ChildrenLB.Invalidate();
            HasChanges();
        }
    }
}
