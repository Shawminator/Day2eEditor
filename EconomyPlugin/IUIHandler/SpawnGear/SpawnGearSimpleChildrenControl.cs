using Day2eEditor;
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
    public partial class SpawnGearSimpleChildrenControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private IHasSimpleChildren _data;
        private IHasSimpleChildren _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public SpawnGearSimpleChildrenControl()
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
            _data = data as IHasSimpleChildren ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            simpleChildrenTypesLB.DataSource = _data.SimpleChildrenTypes;

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
                parent.isDirty = !_data.SimpleChildrenTypes.SequenceEqual(_originalData.SimpleChildrenTypes);
            }
        }

        #region Helper Methods

        /// <summary>
        /// Clones the data for reset purposes
        /// </summary>
        private IHasSimpleChildren CloneData(IHasSimpleChildren data)
        {
            // TODO: Implement actual cloning logic
            return new SimpleSimpleChildrenSnapshot
            {
                SimpleChildrenTypes = new BindingList<string>(new List<string>(data.SimpleChildrenTypes))
            };
        }

        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() != true)
            {
                // TODO: Update _nodes.Last().Text based on _data
            }
        }

        #endregion

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

        private void darkButton73_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes{};
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    if (!_data.SimpleChildrenTypes.Contains(l))
                    {
                        _data.SimpleChildrenTypes.Add(l);
                        simpleChildrenTypesLB.Refresh();
                        HasChanges();
                    }
                }
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }
        private void darkButton74_Click(object sender, EventArgs e)
        {
            _data.SimpleChildrenTypes.Remove(simpleChildrenTypesLB.GetItemText(simpleChildrenTypesLB.SelectedItem));
            HasChanges();
            simpleChildrenTypesLB.Refresh();
        }
    }
    internal class SimpleSimpleChildrenSnapshot : IHasSimpleChildren
    {
        public BindingList<string> SimpleChildrenTypes { get; set; }
    }
}