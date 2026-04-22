using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class Vector3Control : UserControl, IUIHandler
    {
        private Type _parentType;
        private Vec3 _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public Vector3Control()
        {
            InitializeComponent();
        }

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data is Vec3 v ? v : throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            POSXNUD.Value = (decimal)_data.X;
            POSYNUD.Value = (decimal)_data.Y;
            POSZNUD.Value = (decimal)_data.Z;


            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                string split = _nodes.Last().Text.Split(':')[0];
                _nodes.Last().Text = split + ": [" + string.Join(", ", _data) + "]";
            }
        }
        private void POSXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.X = (float)POSXNUD.Value;
            UpdateTreeNodeText();
        }
        private void POSYNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Y= (float)POSYNUD.Value;
            UpdateTreeNodeText();
        }
        private void POSZNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Z = (float)POSZNUD.Value;
            UpdateTreeNodeText();
        }
    }
}