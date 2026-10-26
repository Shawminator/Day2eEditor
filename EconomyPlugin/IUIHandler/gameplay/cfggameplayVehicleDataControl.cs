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
        private ITEM_VehicleData _data;
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
            _data = data as ITEM_VehicleData ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            boatDecayMultiplierNUD.Value = _data.boatDecayMultiplier;
            canDetachAttachedCarWheelsCB.Checked = _data.canDetachAttachedCarWheels;
            canDamageAttachedCarWheelsCB.Checked = _data.canDamageAttachedCarWheels;
            canDetachAttachedBikeWheelsCB.Checked = _data.canDetachAttachedBikeWheels;
            canDamageAttachedBikeWheelsCB.Checked = _data.canDamageAttachedBikeWheels;

            _suppressEvents = false;
        }
        private void boatDecayMultiplierNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.boatDecayMultiplier = boatDecayMultiplierNUD.Value;
        }

        private void canDetachAttachedCarWheelsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.canDetachAttachedCarWheels = canDetachAttachedCarWheelsCB.Checked;
        }
        private void canDamageAttachedCarWheelsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.canDamageAttachedCarWheels = canDamageAttachedCarWheelsCB.Checked;
        }

        private void canDetachAttachedBikeWheelsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.canDetachAttachedBikeWheels = canDetachAttachedBikeWheelsCB.Checked;
        }

        private void canDamageAttachedBikeWheelsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.canDamageAttachedBikeWheels = canDamageAttachedBikeWheelsCB.Checked;
        }
    }
}