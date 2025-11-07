using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionAILoadoutsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private AILoadouts _data;
        private AILoadouts _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public Health? Currenthealth { get; private set; }

        public ExpansionAILoadoutsControl()
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
            _data = data as AILoadouts ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            textBox1.Text = _data.ClassName;
            numericUpDown1.Value = _data.Chance;
            numericUpDown2.Value = _data.Quantity?.Min ?? numericUpDown2.Minimum;
            numericUpDown3.Value = _data.Quantity?.Max ?? numericUpDown3.Minimum;

            listBox1.DisplayMember = "DisplayName";
            listBox1.ValueMember = "Value";
            listBox1.DataSource = _data.Health;
            groupBox1.Visible = (_data.Health != null && _data.Health.Count > 0);

            _suppressEvents = false;
        }
        private void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            ListBox lb = sender as ListBox;
            e.DrawBackground();
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
            var parentObj = _nodes.Last().FindLastParentOfType(_parentType);
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

        private AILoadouts CloneData(AILoadouts data)
        {
            var clone = new AILoadouts
            {
                ClassName = data.ClassName,
                Include = data.Include,
                Chance = data.Chance,
                Quantity = new Quantity
                {
                    Min = data.Quantity.Min,
                    Max = data.Quantity.Max
                },
                Health = new BindingList<Health>(
                    data.Health.Select(h => new Health
                    {
                        Min = h.Min,
                        Max = h.Max,
                        Zone = h.Zone
                    }).ToList()
                ),
                InventoryAttachments = new BindingList<Inventoryattachment>(
                    data.InventoryAttachments.Select(att => new Inventoryattachment
                    {
                        SlotName = att.SlotName,
                        Items = new BindingList<AILoadouts>(
                            att.Items.Select(CloneData).ToList()
                        )
                    }).ToList()
                ),
                InventoryCargo = new BindingList<AILoadouts>(
                    data.InventoryCargo.Select(CloneData).ToList()
                ),
                ConstructionPartsBuilt = new BindingList<object>(
                    data.ConstructionPartsBuilt.ToList() // Assumes shallow copy is sufficient
                ),
                Sets = new BindingList<AILoadouts>(
                    data.Sets.Select(CloneData).ToList()
                ),
                isDirty = data.isDirty,
                ToDelete = data.ToDelete
            };

            clone.setpath(data.FilePath); // Preserve the original path

            return clone;
        }


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

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Chance = numericUpDown1.Value;
            HasChanges();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (_data.Quantity == null) _data.Quantity = new Quantity();
            _data.Quantity.Min = numericUpDown2.Value;
            HasChanges();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (_data.Quantity == null) _data.Quantity = new Quantity();
            _data.Quantity.Max = numericUpDown3.Value;
            HasChanges();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count < 1) return;
            Currenthealth = listBox1.SelectedItem as Health;
            _suppressEvents = true;
            groupBox1.Visible = true;
            numericUpDown4.Value = Currenthealth.Min;
            numericUpDown5.Value = Currenthealth.Max;
            textBox2.Text = Currenthealth.Zone;
            _suppressEvents = false;
        }

        private void darkButton8_Click(object sender, EventArgs e)
        {
            Health newhealth = new Health()
            {
                Min = (decimal)0.7,
                Max = (decimal)1.0,
                Zone = ""
            };
            _data.Health.Add(newhealth);

            HasChanges();

            groupBox1.Visible = true;
            listBox1.SelectedIndex = _data.Health.Count - 1;
        }


        private void darkButton9_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            _data.Health.Remove(Currenthealth);
            HasChanges();
            if (_data.Health.Count > 0)
                listBox1.SelectedIndex = Math.Max(0, index - 1);
            else
                groupBox1.Visible = false;
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            Currenthealth.Min = numericUpDown4.Value;
            HasChanges();
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            Currenthealth.Max = numericUpDown5.Value;
            HasChanges();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            Currenthealth.Zone = textBox2.Text;
            HasChanges();
        }

        private void darkButton11_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes
            {
                UseOnlySingleItem = true
            };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    textBox1.Text = l; // keep original behaviour
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ClassName = textBox1.Text;
            HasChanges();
            UpdateTreeNodeText();
        }
    }
}