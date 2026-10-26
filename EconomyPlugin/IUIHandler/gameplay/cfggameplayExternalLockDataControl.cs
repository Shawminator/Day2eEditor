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
    public partial class cfggameplayExternalLockDataControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ITEM_ExternalLockData _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfggameplayExternalLockDataControl()
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
            _data = data as ITEM_ExternalLockData ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            protectionCountStageOneNUD.Value = (int)_data.protectionCountStageOne;
            protectionCountStageTwoNUD.Value = (int)_data.protectionCountStageTwo;
            protectionTimeNUD.Value = _data.protectionTime;
            protectionResetTimeNUD.Value = _data.protectionResetTime;

            _suppressEvents = false;
        }
        private void protectionCountStageOneNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.protectionCountStageOne = _data.protectionCountStageOne;
        }

        private void protectionCountStageTwoNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.protectionCountStageTwo = _data.protectionCountStageTwo;
        }

        private void protectionTimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.protectionTime = _data.protectionTime;
        }

        private void protectionResetTimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.protectionResetTime = _data.protectionResetTime;
        }
    }
}