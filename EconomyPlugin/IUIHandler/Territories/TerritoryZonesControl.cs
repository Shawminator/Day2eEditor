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
    public partial class TerritoryZonesControl : UserControl, IUIHandler
    {
        public event Action<territorytypeTerritoryZone> PositionChanged;
        private Type _parentType;
        private territorytypeTerritoryZone _data;
        private territorytypeTerritoryZone _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
        public TerritoryZonesControl()
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
            _data = data as territorytypeTerritoryZone ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            TerritoriesZonesRadiusNUD.Value = _data.r;
            TerritoriesZonesStaticMInNUD.Value = _data.smin;
            TerritoriesZonesStaticMaxNUD.Value = _data.smax;
            TerritoriesZonesDynamicMinNUD.Value = _data.dmin;
            TerritoriesZonesDynamicMaxNUD.Value = _data.dmax;
            TerritoriesZonesPOSXNUD.Value = _data.x;
            TerritoriesZonesPOSZNUD.Value = _data.z;
            TerritoriesZonesUseYCB.Checked = TerritoriesZonesPOSYNUD.Visible = _data.ySpecified;
            TerritoriesZonesPOSYNUD.Value = _data.y;
            RadioButton rb = groupBox74.Controls
                              .OfType<RadioButton>()
                              .FirstOrDefault(x => x.Text == _data.name);
            if (rb != null)
                rb.Checked = true;
            else
            {
                TerritoriesZonesDynamicRB.Checked = true;
                TerritoriesZonesDynamicTB.Text = _data.name;
            }

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
        private territorytypeTerritoryZone CloneData(territorytypeTerritoryZone data)
        {
            // TODO: Implement actual cloning logic
            return new territorytypeTerritoryZone
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

        private void TerritoriesZonesPOSXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.x = TerritoriesZonesPOSXNUD.Value;
            HasChanges();
            PositionChanged?.Invoke(_data);
        }

        private void TerritoriesZonesPOSZNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.z = TerritoriesZonesPOSZNUD.Value;
            HasChanges();
            PositionChanged?.Invoke(_data);
        }

        private void TerritoriesZonesRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.r = TerritoriesZonesRadiusNUD.Value;
            HasChanges();
            PositionChanged?.Invoke(_data);
        }
    }
}