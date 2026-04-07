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
    public partial class ExpansionMarkettraderZonePositionsControl : UserControl, IUIHandler
    {
        public event Action<Vec3> PositionChanged;
        public event Action<decimal> RadiusChanged;
        private Type _parentType;
        private ExpansionMarketTraderZone _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionMarkettraderZonePositionsControl()
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
            _data = data as ExpansionMarketTraderZone ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            ZoneXNUD.Value = (decimal)_data.Position.X;
            ZoneYNUD.Value = (decimal)_data.Position.Y;
            ZoneZNUD.Value = (decimal)_data.Position.Z;
            ZoneRadiusNUD.Value = (decimal)_data.Radius;

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

        private void ZoneXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.Position.X = (float)ZoneXNUD.Value;
            PositionChanged?.Invoke(_data.Position);
        }

        private void ZoneYNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.Position.Y = (float)ZoneYNUD.Value;
            PositionChanged?.Invoke(_data.Position);
        }

        private void ZoneZNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.Position.Z = (float)ZoneZNUD.Value;
            PositionChanged?.Invoke(_data.Position);
        }

        private void ZoneRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.Radius = ZoneRadiusNUD.Value;
            RadiusChanged?.Invoke((decimal)_data.Radius);
        }
    }
}