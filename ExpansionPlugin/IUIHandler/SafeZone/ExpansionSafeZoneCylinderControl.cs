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
    public partial class ExpansionSafeZoneCylinderControl : UserControl, IUIHandler
    {
        private Type _parentType;
        public event Action<Vec3> PositionChanged;
        public event Action<decimal> RadiusChanged;
        private ExpansionSafeZoneCylinder _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionSafeZoneCylinderControl()
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
            _data = data as ExpansionSafeZoneCylinder ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            CircleXNUD.Value = (decimal)_data.Center.X;
            CircleYNUD.Value = (decimal)_data.Center.Y;
            CircleZNUD.Value = (decimal)_data.Center.Z;
            CircleRadiusNUD.Value = (decimal)_data.Radius;
            CircleHeightNUD.Value = (decimal)_data.Height;
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
            _data.Center.X = (float)CircleXNUD.Value;
            PositionChanged?.Invoke(_data.Center);
        }
        private void CircleYNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.Center.Y = (float)CircleYNUD.Value;
            PositionChanged?.Invoke(_data.Center);
        }
        private void CircleZNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.Center.Z = (float)CircleZNUD.Value;
            PositionChanged?.Invoke(_data.Center);
        }
        private void CircleRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.Radius = CircleRadiusNUD.Value;
            RadiusChanged?.Invoke((decimal)_data.Radius);
        }

        private void CircleHeightNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.Height = CircleHeightNUD.Value;
        }
    }
}