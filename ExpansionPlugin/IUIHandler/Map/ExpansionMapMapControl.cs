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
    public partial class ExpansionMapMapControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionMapSettings _data;
        private ExpansionMapSettings _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionMapMapControl()
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
            _data = data as ExpansionMapSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            EnableMapCB.Checked = _data.EnableMap == 1 ? true : false;
            UseMapOnMapItemCB.Checked = _data.UseMapOnMapItem == 1 ? true : false;
            ShowPlayerPositionCB.SelectedIndex = (int)_data.ShowPlayerPosition;
            ShowMapStatsCB.Checked = _data.ShowMapStats == 1 ? true : false;
            CanOpenMapWithKeyBindingCB.Checked = _data.CanOpenMapWithKeyBinding == 1 ? true : false;
            EnableHUDGPSCB.Checked = _data.EnableHUDGPS == 1 ? true : false;
            NeedGPSItemForKeyBindingCB.Checked = _data.NeedGPSItemForKeyBinding == 1 ? true : false;
            NeedMapItemForKeyBindingCB.Checked = _data.NeedMapItemForKeyBinding == 1 ? true : false;
            CreateDeathMarkerCB.Checked = _data.CreateDeathMarker == 1 ? true : false;
            PlayerLocationNotifierCB.Checked = _data.PlayerLocationNotifier == 1 ? true : false;

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
        private ExpansionMapSettings CloneData(ExpansionMapSettings data)
        {
            // TODO: Implement actual cloning logic
            return new ExpansionMapSettings
            {
                // Copy properties here
            };
        }

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

        private void EnableMapCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableMap = EnableMapCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void EnableHUDGPSCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableHUDGPS = EnableHUDGPSCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void ShowPlayerPositionCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ShowPlayerPosition = ShowPlayerPositionCB.SelectedIndex;
            HasChanges();
        }

        private void UseMapOnMapItemCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseMapOnMapItem = UseMapOnMapItemCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void CanOpenMapWithKeyBindingCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CanOpenMapWithKeyBinding = CanOpenMapWithKeyBindingCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void NeedMapItemForKeyBindingCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NeedMapItemForKeyBinding = NeedMapItemForKeyBindingCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void NeedGPSItemForKeyBindingCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NeedGPSItemForKeyBinding = NeedGPSItemForKeyBindingCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void CreateDeathMarkerCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CreateDeathMarker = CreateDeathMarkerCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void ShowMapStatsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ShowMapStats = ShowMapStatsCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void PlayerLocationNotifierCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.PlayerLocationNotifier = PlayerLocationNotifierCB.Checked == true ? 1 : 0;
            HasChanges();
        }
    }
}