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
    public partial class cfgplayerspawnSpawnParamControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private playerspawnpointsSpawn_params _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgplayerspawnSpawnParamControl()
        {
            InitializeComponent();
        }

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as playerspawnpointsSpawn_params ?? throw new InvalidCastException();
            _nodes = selectedNodes;

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
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                // TODO: Update _nodes.Last().Text based on _data
            }
        }
        private void SpawnParamsmin_dist_infectedNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.min_dist_infected = (int)SpawnParamsmin_dist_infectedNUD.Value;
        }
        private void SpawnParamsmax_dist_infectedNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.max_dist_infected = (int)SpawnParamsmax_dist_infectedNUD.Value;
        }
        private void SpawnParamsmin_dist_playerNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.min_dist_player = (int)SpawnParamsmin_dist_playerNUD.Value;
        }
        private void SpawnParamsmax_dist_playerNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.max_dist_player = (int)SpawnParamsmax_dist_playerNUD.Value;
        }
        private void SpawnParamsmin_dist_staticNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.min_dist_static = (int)SpawnParamsmin_dist_staticNUD.Value;
        }
        private void SpawnParamsmax_dist_staticNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.max_dist_static = (int)SpawnParamsmax_dist_staticNUD.Value;
        }
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.max_dist_trigger = (int)numericUpDown2.Value;
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.min_dist_trigger = (int)numericUpDown1.Value;
        }
    }
}