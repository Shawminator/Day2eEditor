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
    public partial class ExpasnionMarksetSettingsVehicleSpawnInfoControl : UserControl, IUIHandler
    {
        public event Action<ExpansionMarketSpawnPosition> PositionChanged;
        public event Action<ExpansionMarketSpawnPosition> OrientationChanged;
        private Type _parentType;
        private ExpansionMarketSpawnPosition _data;
        private ExpansionMarketSpawnPosition _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpasnionMarksetSettingsVehicleSpawnInfoControl()
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
            _data = data as ExpansionMarketSpawnPosition ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            POSXNUD.Value = (decimal)_data.Position[0];
            POSYNUD.Value = (decimal)_data.Position[1];
            POSZNUD.Value = (decimal)_data.Position[2];
            ORIXNUD.Value = (decimal)_data.Orientation[0];
            ORIYNUD.Value = (decimal)_data.Orientation[1];
            ORIZNUD.Value = (decimal)_data.Orientation[2];
            _suppressEvents = false;
        }

        /// <summary>
        /// Applies changes to the data and updates the original snapshot
        /// </summary>
        public void ApplyChanges()
        {
            _originalData = CloneData(_data);
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
        /// Clones the data for reset purposes
        /// </summary>
        private ExpansionMarketSpawnPosition CloneData(ExpansionMarketSpawnPosition data)
        {
            // TODO: Implement actual cloning logic
            return new ExpansionMarketSpawnPosition
            {
                Position = data.Position != null ? (float[])data.Position.Clone() : null,
                Orientation = data.Orientation != null ? (float[])data.Orientation.Clone() : null
            };
        }

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
            _data.Position[0] = (float)POSXNUD.Value;
            HasChanges();
            PositionChanged?.Invoke(_data);
            UpdateTreeNodeText();
        }
        private void POSYNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Position[1] = (float)POSYNUD.Value;
            HasChanges();
            UpdateTreeNodeText();
        }
        private void POSZNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Position[2] = (float)POSZNUD.Value;
            HasChanges();
            PositionChanged?.Invoke(_data);
            UpdateTreeNodeText();
        }
        private void ORIXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Orientation[0] = (float)ORIXNUD.Value;
            HasChanges();
            OrientationChanged?.Invoke(_data);
        }
        private void ORIYNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Orientation[1] = (float)ORIYNUD.Value;
            HasChanges();
        }
        private void ORIZNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Orientation[2] = (float)ORIZNUD.Value;
            HasChanges();
        }
    }
}