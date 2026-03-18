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
    public partial class ExpasnionPersonalStorageContainerSpawnInfoControl : UserControl, IUIHandler
    {
        public event Action<ExpansionPersonalStorageConfig> PositionChanged;
        public event Action<ExpansionPersonalStorageConfig> OrientationChanged;
        private Type _parentType;
        private ExpansionPersonalStorageConfig _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpasnionPersonalStorageContainerSpawnInfoControl()
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
            _data = data as ExpansionPersonalStorageConfig ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            POSXNUD.Value = (decimal)_data.Position.X;
            POSYNUD.Value = (decimal)_data.Position.Y;
            POSZNUD.Value = (decimal)_data.Position.Z;
            ORIXNUD.Value = (decimal)_data.Orientation.X;
            ORIYNUD.Value = (decimal)_data.Orientation.Y;
            ORIZNUD.Value = (decimal)_data.Orientation.Z;
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

        private void POSXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Position.X = (float)POSXNUD.Value;
            
            PositionChanged?.Invoke(_data);
            UpdateTreeNodeText();
        }
        private void POSYNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Position.Y = (float)POSYNUD.Value;
            
            UpdateTreeNodeText();
        }
        private void POSZNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Position.Z = (float)POSZNUD.Value;
            
            PositionChanged?.Invoke(_data);
            UpdateTreeNodeText();
        }
        private void ORIXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Orientation.X = (float)ORIXNUD.Value;
            
            OrientationChanged?.Invoke(_data);
        }
        private void ORIYNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Orientation.Y = (float)ORIYNUD.Value;
            
        }
        private void ORIZNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Orientation.Z = (float)ORIZNUD.Value;
            
        }
    }
}