using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    public partial class ExpansionHordeWaypointControl : UserControl, IUIHandler
    {
        public event Action PositionChanged;
        private Type _parentType;
        private BehaviourGroupInfectedPackWaypointParams _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionHordeWaypointControl()
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
            _data = data as BehaviourGroupInfectedPackWaypointParams ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            CenterXNUD.Value = (decimal)_data.m_CenterPosition.X;
            CenterYNUD.Value = (decimal)_data.m_CenterPosition.Y;
            CenterZNUD.Value = (decimal)_data.m_CenterPosition.Z;
            RadiusNUD.Value = (decimal)_data.m_Radius;

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
                _nodes.Last().Text = _data.ToString();
            }
        }

        #endregion

        private void CenterNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_CenterPosition.X = (float)CenterXNUD.Value;
            _data.m_CenterPosition.Y = (float)CenterYNUD.Value;
            _data.m_CenterPosition.Z = (float)CenterZNUD.Value;
            UpdateTreeNodeText();
            PositionChanged?.Invoke();
        }

        private void RadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_Radius = RadiusNUD.Value;
            PositionChanged?.Invoke();
        }
    }
}
