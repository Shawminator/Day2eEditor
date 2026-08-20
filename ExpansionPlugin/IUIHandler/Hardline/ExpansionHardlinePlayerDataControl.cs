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
    public partial class ExpansionHardlinePlayerDataControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionHardlinePlayerData _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionHardlinePlayerDataControl()
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
            _data = data as ExpansionHardlinePlayerData ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            HardlineReputationNUD.Value = _data.Reputation;
            HardlineFactionIDNUD.Value = _data.FactionID;
            hardLinePersonalStorageLevelNUD.Value = _data.PersonalStorageLevel;

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
        private void HardlineReputationNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Reputation = (int)HardlineReputationNUD.Value;
        }
        private void HardlineFactionIDNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.FactionID = (int)HardlineFactionIDNUD.Value;
        }
        private void hardLinePersonalStorageLevelNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.PersonalStorageLevel = (int)hardLinePersonalStorageLevelNUD.Value;
        }
    }
}