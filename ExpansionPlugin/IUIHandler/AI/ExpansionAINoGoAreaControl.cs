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
        private ExpansionAINoGoArea _originalData;
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
            _originalData = _data.Clone();

            _suppressEvents = true;

            NameTB.Text = _data.Name;
            POSXNUD.Value = (decimal)_data._Position.X;
            POSYNUD.Value = (decimal)_data._Position.Y;
            POSZNUD.Value = (decimal)_data._Position.Z;
            RadiusNUD.Value = (decimal)_data.Radius;
            HieghtNUD.Value = (decimal)_data.Height;

            _suppressEvents = false;
        }

        /// <summary>
        /// Applies changes to the data and updates the original snapshot
        /// </summary>
        public void ApplyChanges()
        {
            _originalData = _data.Clone();
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
                parent.isDirty = !_data.Equals(_originalData);
            }
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
            HasChanges();
        }
        private void POSXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data._Position.X = (float)POSXNUD.Value;
            HasChanges();
            PositionChanged?.Invoke(_data);
        }
        private void POSYNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data._Position.Y = (float)POSYNUD.Value;
            HasChanges();
        }
        private void POSZNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data._Position.Z = (float)POSZNUD.Value;
            HasChanges();
            PositionChanged?.Invoke(_data);
        }
        private void RadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Radius= (float)RadiusNUD.Value;
            HasChanges();
            RadiusChanged?.Invoke(_data);
        }
        private void HieghtNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Height = (float)HieghtNUD.Value;
            HasChanges();
        }
    }
}