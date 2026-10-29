using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CaparePlugin
{
    /// <summary>
    /// Static loot box position
    /// </summary>
    public partial class CapareStaticBoxPosRotControl : UserControl, IUIHandler
    {
        public event Action DataChanged;
        private Type _parentType;
        private CapareStaticBoxPosRot _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public CapareStaticBoxPosRotControl()
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
            _data = data as CapareStaticBoxPosRot ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            SetNUD(PositionXNUD, (decimal)_data.Position.X);
            SetNUD(PositionYNUD, (decimal)_data.Position.Y);
            SetNUD(PositionZNUD, (decimal)_data.Position.Z);
            SetNUD(RotationXNUD, (decimal)_data.Rotation.X);
            SetNUD(RotationYNUD, (decimal)_data.Rotation.Y);
            SetNUD(RotationZNUD, (decimal)_data.Rotation.Z);

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
        private static void SetNUD(NumericUpDown nud, decimal value)
        {
            nud.Value = Math.Clamp(value, nud.Minimum, nud.Maximum);
        }

        #endregion

        private void PositionXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Position.X = (float)PositionXNUD.Value;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void PositionYNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Position.Y = (float)PositionYNUD.Value;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void PositionZNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Position.Z = (float)PositionZNUD.Value;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void RotationXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Rotation.X = (float)RotationXNUD.Value;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void RotationYNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Rotation.Y = (float)RotationYNUD.Value;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void RotationZNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Rotation.Z = (float)RotationZNUD.Value;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
    }
}
