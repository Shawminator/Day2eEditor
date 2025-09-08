using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class cfglimitsdefinitionCategoryControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private cfglimitsdefinitionConfig _data;
        private cfglimitsdefinition _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfglimitsdefinitionCategoryControl()
        {
            InitializeComponent();
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
            _data = data as cfglimitsdefinitionConfig ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            listBox9.DataSource = _data.Data.categories;

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
                parent.isDirty = !_data.Data.Equals(_originalData);
            }
        }

        #region Helper Methods

        /// <summary>
        /// Clones the data for reset purposes
        /// </summary>
        private cfglimitsdefinition CloneData(cfglimitsdefinitionConfig data)
        {
            if (data == null) return null;

            var clone = new cfglimitsdefinition
            {
                categories = data.Data.categories == null ? null : new BindingList<listsCategory>(
                    data.Data.categories.Select(c => c == null ? null : new listsCategory { name = c.name }).ToList()
                ),
                tags = data.Data.tags == null ? null : new BindingList<listsTag>(
                    data.Data.tags.Select(t => t == null ? null : new listsTag { name = t.name }).ToList()
                ),
                usageflags = data.Data.usageflags == null ? null : new BindingList<listsUsage>(
                    data.Data.usageflags.Select(u => u == null ? null : new listsUsage { name = u.name }).ToList()
                ),
                valueflags = data.Data.valueflags == null ? null : new BindingList<listsValue>(
                    data.Data.valueflags.Select(v => v == null ? null : new listsValue { name = v.name }).ToList()
                )
            };
            return clone;
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

        private void darkButton27_Click(object sender, EventArgs e)
        {
            listsCategory newusage = new listsCategory();
            newusage.name = "Change Me";
            _data.Data.categories.Add(newusage);
            HasChanges();
        }

        private void darkButton76_Click(object sender, EventArgs e)
        {
            if (listBox9.SelectedItems.Count <= 0) return;
            listsCategory uu = listBox9.SelectedItem as listsCategory;
            string uuname = uu.name;
            _data.Data.categories.Remove(uu);
            HasChanges();
        }

        private void darkButton83_Click(object sender, EventArgs e)
        {
            if (listBox9.SelectedItems.Count <= 0) return;
            listsCategory uu = listBox9.SelectedItem as listsCategory;
            string uuname = uu.name;
            uu.name = textBox3.Text;
            HasChanges();
            darkButton83.Visible = false;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            darkButton83.Visible = true;
        }

        private void listBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox9.SelectedItems.Count <= 0) return;
            listsCategory uu = listBox9.SelectedItem as listsCategory;
            _suppressEvents = true;
            textBox3.Text = uu.name;
            _suppressEvents = false;
        }
    }
}