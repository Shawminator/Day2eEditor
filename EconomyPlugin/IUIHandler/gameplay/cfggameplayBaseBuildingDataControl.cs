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
        private Basebuildingdata _data;
        private Basebuildingdata _originalData;
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
        public void LoadFromData(object data, List<TreeNode> selectedNodes)
        {
            // TODO: Replace ClassType with your actual type
            _data = data as Basebuildingdata ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

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
            if (_nodes?.Any() != true) return;

            // TODO: Replace Parentfile with your actual parent type if different
            var ef = _nodes.Last().FindParentOfType<CFGGameplayConfig>();
            if (ef != null)
                ef.isDirty = !_data.Equals(_originalData);
        }

        #region Helper Methods

        /// <summary>
        /// Clones the data for reset purposes
        /// </summary>
        private Basebuildingdata CloneData(Basebuildingdata data)
        {

            return new Basebuildingdata
            {
                HologramData = new Hologramdata
                {
                    disableIsCollidingBBoxCheck = data.HologramData.disableIsCollidingBBoxCheck,
                    disableIsCollidingPlayerCheck = data.HologramData.disableIsCollidingPlayerCheck,
                    disableIsClippingRoofCheck = data.HologramData.disableIsClippingRoofCheck,
                    disableIsBaseViableCheck = data.HologramData.disableIsBaseViableCheck,
                    disableIsCollidingGPlotCheck = data.HologramData.disableIsCollidingGPlotCheck,
                    disableIsCollidingAngleCheck = data.HologramData.disableIsCollidingAngleCheck,
                    disableIsPlacementPermittedCheck = data.HologramData.disableIsPlacementPermittedCheck,
                    disableHeightPlacementCheck = data.HologramData.disableHeightPlacementCheck,
                    disableIsUnderwaterCheck = data.HologramData.disableIsUnderwaterCheck,
                    disableIsInTerrainCheck = data.HologramData.disableIsInTerrainCheck,
                    disableColdAreaBuildingCheck = data.HologramData.disableColdAreaBuildingCheck,
                    disallowedTypesInUnderground = new BindingList<string>(data.HologramData.disallowedTypesInUnderground.ToList())
                },
                ConstructionData = new Constructiondata
                {
                    disablePerformRoofCheck = data.ConstructionData.disablePerformRoofCheck,
                    disableIsCollidingCheck = data.ConstructionData.disableIsCollidingCheck,
                    disableDistanceCheck = data.ConstructionData.disableDistanceCheck


                }
            };
        }

        #endregion
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
            HasChanges();
        }
        private void disableIsCollidingPlayerCheckCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HologramData.disableIsCollidingPlayerCheck = disableIsCollidingPlayerCheckCB.Checked;
            HasChanges();
        }
        private void disableIsClippingRoofCheckCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HologramData.disableIsClippingRoofCheck = disableIsClippingRoofCheckCB.Checked;
            HasChanges();
        }
        private void disableIsBaseViableCheckCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HologramData.disableIsBaseViableCheck = disableIsBaseViableCheckCB.Checked;
            HasChanges();
        }
        private void disableIsCollidingGPlotCheckCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HologramData.disableIsCollidingGPlotCheck = disableIsCollidingGPlotCheckCB.Checked;
            HasChanges();
        }
        private void disableIsCollidingAngleCheckCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HologramData.disableIsCollidingAngleCheck = disableIsCollidingAngleCheckCB.Checked;
            HasChanges();
        }
        private void disableIsPlacementPermittedCheckCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HologramData.disableIsPlacementPermittedCheck = disableIsPlacementPermittedCheckCB.Checked;
            HasChanges();
        }
        private void disableHeightPlacementCheckCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HologramData.disableHeightPlacementCheck = disableHeightPlacementCheckCB.Checked;
            HasChanges();
        }
        private void disableIsUnderwaterCheckCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HologramData.disableIsUnderwaterCheck = disableIsUnderwaterCheckCB.Checked;
            HasChanges();
        }
        private void disableIsInTerrainCheckCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HologramData.disableIsInTerrainCheck = disableIsInTerrainCheckCB.Checked;
            HasChanges();
        }
        private void disableColdAreaBuildingCheckCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HologramData.disableColdAreaBuildingCheck = disableColdAreaBuildingCheckCB.Checked;
            HasChanges();
        }
        private void disablePerformRoofCheckCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ConstructionData.disablePerformRoofCheck = disablePerformRoofCheckCB.Checked;
            HasChanges();
        }
        private void disableIsCollidingCheckCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ConstructionData.disableIsCollidingCheck = disableIsCollidingCheckCB.Checked;
            HasChanges();
        }
        private void disableDistanceCheckCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ConstructionData.disableDistanceCheck = disableDistanceCheckCB.Checked;
            HasChanges();
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
                HasChanges();

            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }
        private void darkButton67_Click(object sender, EventArgs e)
        {
            _data.HologramData.disallowedTypesInUnderground.Remove(CFGGameplayDisallowedtypesLB.GetItemText(CFGGameplayDisallowedtypesLB.SelectedItem));
            HasChanges();
        }
    }
}