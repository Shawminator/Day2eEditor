using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    public partial class ExpansionMissionEventHordeControl : UserControl, IUIHandler
    {
        public event Action PositionChanged;
        private Type _parentType;
        private ExpansionMissionEventHorde _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionMissionEventHordeControl()
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
            _data = data as ExpansionMissionEventHorde ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            MinInfectedAmountNUD.Value = (decimal)_data.MinInfectedAmount;
            MaxInfectedAmountNUD.Value = (decimal)_data.MaxInfectedAmount;
            PositionXNUD.Value = (decimal)_data.Position.X;
            PositionYNUD.Value = (decimal)_data.Position.Y;
            PositionZNUD.Value = (decimal)_data.Position.Z;
            MinimumSpawnRadiusNUD.Value = (decimal)_data.MinimumSpawnRadius;
            MaximumSpawnRadiusNUD.Value = (decimal)_data.MaximumSpawnRadius;

            _suppressEvents = false;
        }

        private void MinInfectedAmountNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MinInfectedAmount = (int)MinInfectedAmountNUD.Value;
        }

        private void MaxInfectedAmountNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxInfectedAmount = (int)MaxInfectedAmountNUD.Value;
        }

        private void PositionNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Position.X = (float)PositionXNUD.Value;
            _data.Position.Y = (float)PositionYNUD.Value;
            _data.Position.Z = (float)PositionZNUD.Value;
            PositionChanged?.Invoke();
        }

        private void MinimumSpawnRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MinimumSpawnRadius = MinimumSpawnRadiusNUD.Value;
            PositionChanged?.Invoke();
        }

        private void MaximumSpawnRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaximumSpawnRadius = MaximumSpawnRadiusNUD.Value;
            PositionChanged?.Invoke();
        }
    }
}
