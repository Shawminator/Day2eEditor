using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionAINoGoAreaControl : UserControl, IUIHandler
    {
        public event Action<ExpansionAINoGoArea> PositionChanged;
        public event Action<ExpansionAINoGoArea> RadiusChanged;
        private Type _parentType;
        private ExpansionAINoGoArea _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionAINoGoAreaControl()
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
            _data = data as ExpansionAINoGoArea ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            NameTB.Text = _data.Name;
            POSXNUD.Value = (decimal)_data.Position.X;
            POSYNUD.Value = (decimal)_data.Position.Y;
            POSZNUD.Value = (decimal)_data.Position.Z;
            RadiusNUD.Value = (decimal)_data.Radius;
            HieghtNUD.Value = (decimal)_data.Height;

            _suppressEvents = false;
        }

        #region Helper Methods
        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = $"{_data.Name}";
            }
        }

        #endregion

        private void NameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Name = NameTB.Text;
            UpdateTreeNodeText();
            
        }
        private void POSXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Position.X = (float)POSXNUD.Value;
            
            PositionChanged?.Invoke(_data);
        }
        private void POSYNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Position.Y = (float)POSYNUD.Value;
            
        }
        private void POSZNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Position.Z = (float)POSZNUD.Value;
            
            PositionChanged?.Invoke(_data);
        }
        private void RadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Radius= (float)RadiusNUD.Value;
            
            RadiusChanged?.Invoke(_data);
        }
        private void HieghtNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Height = (float)HieghtNUD.Value;
            
        }
    }
}