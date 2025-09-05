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
    public partial class cfgplayerspawnSpawnParamControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private playerspawnpointsSpawn_params _data;
        private playerspawnpointsSpawn_params _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgplayerspawnSpawnParamControl()
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
            _data = data as playerspawnpointsSpawn_params ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            SpawnParamsmin_dist_infectedNUD.Value = _data.min_dist_infected;
            SpawnParamsmax_dist_infectedNUD.Value = _data.max_dist_infected;
            SpawnParamsmin_dist_playerNUD.Value = _data.min_dist_player;
            SpawnParamsmax_dist_playerNUD.Value = _data.max_dist_player;
            SpawnParamsmin_dist_staticNUD.Value = _data.min_dist_static;
            SpawnParamsmax_dist_staticNUD.Value = _data.max_dist_static;
            numericUpDown2.Value = _data.min_dist_trigger;
            numericUpDown1.Value = _data.max_dist_trigger;

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
        private playerspawnpointsSpawn_params CloneData(playerspawnpointsSpawn_params data)
        {
            // TODO: Implement actual cloning logic
            return new playerspawnpointsSpawn_params
            {
                min_dist_infected = data.min_dist_infected,
                max_dist_infected = data.max_dist_infected,
                min_dist_player = data.min_dist_player,
                max_dist_player= data.max_dist_player,
                min_dist_static = data.min_dist_static,
                max_dist_static= data.max_dist_static,
                min_dist_triggerSpecified = data.min_dist_triggerSpecified,
                min_dist_trigger = data.min_dist_trigger,
                max_dist_triggerSpecified = data.max_dist_triggerSpecified,
                max_dist_trigger= data.max_dist_trigger
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

        private void SpawnParamsmin_dist_infectedNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.min_dist_infected = (int)SpawnParamsmin_dist_infectedNUD.Value;
            HasChanges();
        }
        private void SpawnParamsmax_dist_infectedNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.max_dist_infected = (int)SpawnParamsmax_dist_infectedNUD.Value;
            HasChanges();
        }

        private void SpawnParamsmin_dist_playerNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.min_dist_player = (int)SpawnParamsmin_dist_playerNUD.Value;
            HasChanges();
        }

        private void SpawnParamsmax_dist_playerNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.max_dist_player = (int)SpawnParamsmax_dist_playerNUD.Value;
            HasChanges();
        }

        private void SpawnParamsmin_dist_staticNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.min_dist_static = (int)SpawnParamsmin_dist_staticNUD.Value;
            HasChanges();
        }

        private void SpawnParamsmax_dist_staticNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.max_dist_static = (int)SpawnParamsmax_dist_staticNUD.Value;
            HasChanges();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.max_dist_trigger = (int)numericUpDown2.Value;
            HasChanges();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.min_dist_trigger = (int)numericUpDown1.Value;
            HasChanges();
        }
    }
}