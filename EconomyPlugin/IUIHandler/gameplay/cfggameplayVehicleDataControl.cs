using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class cfggameplayVehicleDataControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private VehicleData _data;
        private VehicleData _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfggameplayVehicleDataControl()
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
            _data = data as VehicleData ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            boatDecayMultiplierNUD.Value = _data.boatDecayMultiplier;

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
        private VehicleData CloneData(VehicleData data)
        {
            // TODO: Implement actual cloning logic
            return new VehicleData
            {
                boatDecayMultiplier = data.boatDecayMultiplier
            };
        }
        #endregion

        private void boatDecayMultiplierNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.boatDecayMultiplier = boatDecayMultiplierNUD.Value;
            HasChanges();
        }
    }
}