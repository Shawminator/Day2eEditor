using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class cfggameplayBaseBuildingDataControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private Basebuildingdata _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfggameplayBaseBuildingDataControl()
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
            _data = data as Basebuildingdata ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            disableIsCollidingBBoxCheckCB.Checked = _data.HologramData.disableIsCollidingBBoxCheck;
            disableIsCollidingPlayerCheckCB.Checked = _data.HologramData.disableIsCollidingPlayerCheck;
            disableIsClippingRoofCheckCB.Checked = _data.HologramData.disableIsClippingRoofCheck;
            disableIsBaseViableCheckCB.Checked = _data.HologramData.disableIsBaseViableCheck;
            disableIsCollidingGPlotCheckCB.Checked = _data.HologramData.disableIsCollidingGPlotCheck;
            disableIsCollidingAngleCheckCB.Checked = _data.HologramData.disableIsCollidingAngleCheck;
            disableIsPlacementPermittedCheckCB.Checked = _data.HologramData.disableIsPlacementPermittedCheck;
            disableHeightPlacementCheckCB.Checked = _data.HologramData.disableHeightPlacementCheck;
            disableIsUnderwaterCheckCB.Checked = _data.HologramData.disableIsUnderwaterCheck;
            disableIsInTerrainCheckCB.Checked = _data.HologramData.disableIsInTerrainCheck;
            disableColdAreaBuildingCheckCB.Checked = _data.HologramData.disableColdAreaBuildingCheck;

            disablePerformRoofCheckCB.Checked = _data.ConstructionData.disablePerformRoofCheck;
            disableIsCollidingCheckCB.Checked = _data.ConstructionData.disableIsCollidingCheck;
            disableDistanceCheckCB.Checked = _data.ConstructionData.disableDistanceCheck;

            CFGGameplayDisallowedtypesLB.DisplayMember = "DisplayName";
            CFGGameplayDisallowedtypesLB.ValueMember = "Value";
            CFGGameplayDisallowedtypesLB.DataSource = _data.HologramData.disallowedTypesInUnderground;

            _suppressEvents = false;
        }
        private void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox lb = sender as ListBox;
            e.DrawBackground();
            if (lb.Items.Count == 0) return;
            Brush myBrush = Brushes.Black;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            }
            else
            {
                myBrush = Brushes.White;
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(60, 63, 65)), e.Bounds);
            }
            e.Graphics.DrawString(lb.Items[e.Index].ToString(), e.Font, myBrush, e.Bounds);
            e.DrawFocusRectangle();
        }
        private void disableIsCollidingBBoxCheckCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HologramData.disableIsCollidingBBoxCheck = disableIsCollidingBBoxCheckCB.Checked;
        }
        private void disableIsCollidingPlayerCheckCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HologramData.disableIsCollidingPlayerCheck = disableIsCollidingPlayerCheckCB.Checked;
        }
        private void disableIsClippingRoofCheckCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HologramData.disableIsClippingRoofCheck = disableIsClippingRoofCheckCB.Checked;
        }
        private void disableIsBaseViableCheckCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HologramData.disableIsBaseViableCheck = disableIsBaseViableCheckCB.Checked;
        }
        private void disableIsCollidingGPlotCheckCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HologramData.disableIsCollidingGPlotCheck = disableIsCollidingGPlotCheckCB.Checked;
        }
        private void disableIsCollidingAngleCheckCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
        }
        private void disableIsPlacementPermittedCheckCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HologramData.disableIsPlacementPermittedCheck = disableIsPlacementPermittedCheckCB.Checked;
        }
        private void disableHeightPlacementCheckCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HologramData.disableHeightPlacementCheck = disableHeightPlacementCheckCB.Checked;
        }
        private void disableIsUnderwaterCheckCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HologramData.disableIsUnderwaterCheck = disableIsUnderwaterCheckCB.Checked;
        }
        private void disableIsInTerrainCheckCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HologramData.disableIsInTerrainCheck = disableIsInTerrainCheckCB.Checked;
        }
        private void disableColdAreaBuildingCheckCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HologramData.disableColdAreaBuildingCheck = disableColdAreaBuildingCheckCB.Checked;
        }
        private void disablePerformRoofCheckCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ConstructionData.disablePerformRoofCheck = disablePerformRoofCheckCB.Checked;
        }
        private void disableIsCollidingCheckCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ConstructionData.disableIsCollidingCheck = disableIsCollidingCheckCB.Checked;
        }
        private void disableDistanceCheckCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ConstructionData.disableDistanceCheck = disableDistanceCheckCB.Checked;
        }
        private void darkButton66_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes{};
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    if (!_data.HologramData.disallowedTypesInUnderground.Contains(l))
                        _data.HologramData.disallowedTypesInUnderground.Add(l);
                }
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }
        private void darkButton67_Click(object sender, EventArgs e)
        {
            _data.HologramData.disallowedTypesInUnderground.Remove(CFGGameplayDisallowedtypesLB.GetItemText(CFGGameplayDisallowedtypesLB.SelectedItem));
        }
    }
}