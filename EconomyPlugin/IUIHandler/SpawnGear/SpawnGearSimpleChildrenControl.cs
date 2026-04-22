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
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public SpawnGearSimpleChildrenControl()
        {
            InitializeComponent();
        }

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as IHasSimpleChildren ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            simpleChildrenTypesLB.DataSource = _data.SimpleChildrenTypes;

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() != true)
            {
                // TODO: Update _nodes.Last().Text based on _data
            }
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
            simpleChildrenTypesLB.Refresh();
        }
    }
}