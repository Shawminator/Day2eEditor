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
    public partial class ExpansionVehicleSettingsCFCloudControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionVehicleSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionVehicleSettingsCFCloudControl()
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
            _data = data as ExpansionVehicleSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            VehicleHeliCoverIconNameTB.Text = _data.CFToolsHeliCoverIconName;
            VehicleBoatCoverIconNameTB.Text = _data.CFToolsBoatCoverIconName;
            VehicleCarCoverIconNameTB.Text = _data.CFToolsCarCoverIconName;

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

        private void VehicleHeliCoverIconNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.CFToolsHeliCoverIconName = VehicleHeliCoverIconNameTB.Text;
        }

        private void VehicleBoatCoverIconNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.CFToolsBoatCoverIconName = VehicleBoatCoverIconNameTB.Text;
        }

        private void VehicleCarCoverIconNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.CFToolsCarCoverIconName = VehicleCarCoverIconNameTB.Text;
        }
    }
}