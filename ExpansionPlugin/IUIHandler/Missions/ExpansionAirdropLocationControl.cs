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
    public partial class ExpansionAirdropLocationControl : UserControl, IUIHandler
    {
        public event Action<decimal> PositionChanged;
        public event Action<decimal> RadiusChanged;
        private Type _parentType;
        private ExpansionAirdropLocation _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionAirdropLocationControl()
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
            _data = data as ExpansionAirdropLocation ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            MissionDropXNUD.Value = (decimal)_data.x;
            MissionDropYNUD.Value = (decimal)_data.z;
            MissionDropRadiusNUD.Value = (decimal)_data.Radius;
            MissionDropNameTB.Text = _data.Name;
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
                _nodes.Last().Text = $"Drop Location - {_data.Name}";
            }
        }

        #endregion

        private void MissionDropNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Name = MissionDropNameTB.Text;
            UpdateTreeNodeText();
        }

        private void MissionDropXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.x = (decimal)MissionDropXNUD.Value;
            PositionChanged?.Invoke((decimal)_data.x);
        }

        private void MissionDropYNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.z = (decimal)MissionDropYNUD.Value;
            PositionChanged?.Invoke((decimal)_data.z);
        }

        private void MissionDropRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Radius = (decimal)MissionDropRadiusNUD.Value;
            RadiusChanged?.Invoke((decimal)_data.Radius);
        }
    }
}