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
    public partial class ExpansionVehicleSettingsLocksControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionVehicleSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionVehicleSettingsLocksControl()
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

            CanPickLockCB.Checked = _data.CanPickLock == 1 ? true : false;
            PickLockChancePercentNUD.Value = (decimal)_data.PickLockChancePercent;
            PickLockTimeSecondsNUD.Value = (int)_data.PickLockTimeSeconds;
            PickLockToolDamagePercentNUD.Value = (decimal)_data.PickLockToolDamagePercent;
            CanChangeLockCB.Checked = _data.CanChangeLock == 1 ? true : false;
            ChangeLockTimeSecondsNUD.Value = (int)_data.ChangeLockTimeSeconds;
            ChangeLockToolDamagePercentNUD.Value = (decimal)_data.ChangeLockToolDamagePercent;

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

        private void CanPickLockCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.CanPickLock = CanPickLockCB.Checked == true ? 1 : 0;
            if (_data.CanPickLock == 1 ? true : false)
            {
                TreeNode PickLockNodes = new TreeNode("Pick Lock Tools")
                {
                    Tag = "VehiclePickLockTools"
                };
                foreach (string tool in _data.PickLockTools)
                {
                    PickLockNodes.Nodes.Add(new TreeNode(tool)
                    {
                        Tag = "VehiclePickLockTool"
                    });
                }
                if(_nodes.Last().Nodes.Count > 0)
                    _nodes.Last().Nodes.Insert(0,PickLockNodes);
                else
                    _nodes.Last().Nodes.Add(PickLockNodes);
            }
            else
            {
                var parent = _nodes.Last();
                TreeNode nodeToRemove = parent.Nodes
                    .Cast<TreeNode>()
                    .FirstOrDefault(n => (string)n.Tag == "VehiclePickLockTools");

                if (nodeToRemove != null)
                {
                    parent.Nodes.Remove(nodeToRemove);
                }
            }
        }
        private void PickLockChancePercentNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.PickLockChancePercent = (decimal)PickLockChancePercentNUD.Value;
        }
        private void PickLockTimeSecondsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.PickLockTimeSeconds = (int)PickLockTimeSecondsNUD.Value;
        }
        private void PickLockToolDamagePercentNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.PickLockToolDamagePercent = (decimal)PickLockToolDamagePercentNUD.Value;
        }
        private void CanChangeLockCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.CanChangeLock = CanChangeLockCB.Checked == true ? 1 : 0;
            if (_data.CanChangeLock == 1 ? true : false)
            {
                TreeNode ChangeLockNodes = new TreeNode("Change Lock Tools")
                {
                    Tag = "VehicleChangeLockTools"
                };
                foreach (string tool in _data.ChangeLockTools)
                {
                    ChangeLockNodes.Nodes.Add(new TreeNode(tool)
                    {
                        Tag = "VehiclePickLockTool"
                    });
                }
                _nodes.Last().Nodes.Add(ChangeLockNodes);
            }
            else
            {
                var parent = _nodes.Last();
                TreeNode nodeToRemove = parent.Nodes
                    .Cast<TreeNode>()
                    .FirstOrDefault(n => (string)n.Tag == "VehicleChangeLockTools");

                if (nodeToRemove != null)
                {
                    parent.Nodes.Remove(nodeToRemove);
                }
            }
        }
        private void ChangeLockTimeSecondsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.ChangeLockTimeSeconds = (int)ChangeLockTimeSecondsNUD.Value;
        }
        private void ChangeLockToolDamagePercentNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.ChangeLockToolDamagePercent = (decimal)ChangeLockToolDamagePercentNUD.Value;
        }
    }
}