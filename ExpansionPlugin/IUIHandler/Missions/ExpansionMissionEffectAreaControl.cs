using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionMissionEffectAreaControl : UserControl, IUIHandler
    {
        private Type _parentType;
        public event Action<decimal[]> PositionChanged;
        public event Action<decimal> RadiusChanged;
        private ExpansionMissionEventContaminatedArea _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionMissionEffectAreaControl()
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
            _data = data as ExpansionMissionEventContaminatedArea ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            CircleXNUD.Value = (decimal)_data.Data.Pos[0];
            CircleYNUD.Value = (decimal)_data.Data.Pos[1];
            CircleZNUD.Value = (decimal)_data.Data.Pos[2];
            CircleRadiusNUD.Value = (decimal)_data.Data.Radius;

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
                // TODO: Update _nodes.Last().Text based on _data
            }
        }

        #endregion
        private void CircleXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.Data.Pos[0] = (decimal)CircleXNUD.Value;
            PositionChanged?.Invoke(_data.Data.Pos);
        }
        private void CircleYNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.Data.Pos[1] = (decimal)CircleYNUD.Value;
            PositionChanged?.Invoke(_data.Data.Pos);
        }
        private void CircleZNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.Data.Pos[2] = (decimal)CircleZNUD.Value;
            PositionChanged?.Invoke(_data.Data.Pos);
        }
        private void CircleRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.Data.Radius = CircleRadiusNUD.Value;
            RadiusChanged?.Invoke((decimal)_data.Data.Radius);
        }
    }
}