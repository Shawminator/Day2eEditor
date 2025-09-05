using Day2eEditor;
using DayZeLib;
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
    public partial class cfgplayerspawnGeneratorParamsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private playerspawnpointsGenerator_params _data;
        private playerspawnpointsGenerator_params _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgplayerspawnGeneratorParamsControl()
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
            _data = data as playerspawnpointsGenerator_params ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            generatorparamsgrid_densityNUD.Value = _data.grid_density;
            generatorparamsgrid_widthNUD.Value = _data.grid_width;
            generatorparamsgrid_heightNUD.Value = _data.grid_height;
            generatorparamsmin_dist_staticNUD.Value = _data.min_dist_static;
            generatorparamsmax_dist_staticNUD.Value = _data.max_dist_static;
            generatorparamsmin_steepnessNUD.Value = _data.min_steepness;
            generatorparamsmax_steepnessNUD.Value = _data.max_steepness;
            generatorparamsallow_in_waterCB.Checked = _data.allow_in_water;

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
        private playerspawnpointsGenerator_params CloneData(playerspawnpointsGenerator_params data)
        {
            // TODO: Implement actual cloning logic
            return new playerspawnpointsGenerator_params
            {
                grid_density = data.grid_density,
                grid_width = data.grid_width,
                grid_height = data.grid_height,
                min_dist_static = data.min_dist_static,
                max_dist_static = _data.max_dist_static,
                min_steepness = data.min_steepness,
                max_steepness = _data.max_steepness,
                allow_in_water = data.allow_in_water
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

        private void generatorparamsgrid_densityNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.grid_density = (int)generatorparamsgrid_densityNUD.Value;
            HasChanges();
        }

        private void generatorparamsgrid_widthNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.grid_width = (int)generatorparamsgrid_widthNUD.Value;
            HasChanges();
        }

        private void generatorparamsgrid_heightNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.grid_height = (int)generatorparamsgrid_heightNUD.Value;
            HasChanges();
        }

        private void generatorparamsmin_dist_staticNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.min_dist_static = (int)generatorparamsmin_dist_staticNUD.Value;
            HasChanges();
        }

        private void generatorparamsmax_dist_staticNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.max_dist_static = (int)generatorparamsmax_dist_staticNUD.Value;
            HasChanges();
        }

        private void generatorparamsmin_steepnessNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.min_steepness = (int)generatorparamsmin_steepnessNUD.Value;
            HasChanges();
        }

        private void generatorparamsmax_steepnessNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.max_steepness = (int)generatorparamsmax_steepnessNUD.Value;
            HasChanges();
        }

        private void generatorparamsallow_in_waterCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.allow_in_water = generatorparamsallow_in_waterCB.Checked;
            HasChanges();
        }
    }
}