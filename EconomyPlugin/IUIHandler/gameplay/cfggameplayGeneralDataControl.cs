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
    public partial class cfggameplayGeneralDataControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private Generaldata _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfggameplayGeneralDataControl()
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
            _data = data as Generaldata ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            disableBaseDamageCB.Checked = _data.disableBaseDamage;
            disableContainerDamageCB.Checked = _data.disableContainerDamage;
            disableRespawnDialogCB.Checked = _data.disableRespawnDialog;
            disableRespawnInUnconsciousnessCB.Checked = _data.disableRespawnInUnconsciousness;

            _suppressEvents = false;
        }
        private void disableBaseDamageCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.disableBaseDamage = disableBaseDamageCB.Checked;
        }
        private void disableContainerDamageCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.disableContainerDamage = disableContainerDamageCB.Checked;
        }
        private void disableRespawnDialogCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.disableRespawnDialog = disableRespawnDialogCB.Checked;
        }
        private void disableRespawnInUnconsciousnessCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.disableRespawnInUnconsciousness = disableRespawnInUnconsciousnessCB.Checked;
        }
    }
}