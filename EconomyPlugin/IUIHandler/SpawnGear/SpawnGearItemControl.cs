using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class SpawnGearItemControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private IHasSpawnItemType _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public SpawnGearItemControl()
        {
            InitializeComponent();
        }

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as IHasSpawnItemType ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            SpawnGearItemTypeTB.Text = _data.ItemType;

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = _data.ItemType;
            }
        }
        private void darkButton70_Click(object sender, EventArgs e)
        {
            string Classname = "";
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
                    SpawnGearItemTypeTB.Text = _data.ItemType = l;
                    UpdateTreeNodeText();
                }
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }
    }
}