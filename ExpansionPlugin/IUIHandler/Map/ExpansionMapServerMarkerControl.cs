using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionMapServerMarkerControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionMapSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionMapServerMarkerControl()
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
            _data = data as ExpansionMapSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            EnableServerMarkersCB.Checked = _data.EnableServerMarkers == 1 ? true : false;
            ShowNameOnServerMarkersCB.Checked = _data.ShowNameOnServerMarkers == 1 ? true : false;
            ShowDistanceOnServerMarkersCB.Checked = _data.ShowDistanceOnServerMarkers == 1 ? true : false;

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

        private void EnableServerMarkersCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableServerMarkers = EnableServerMarkersCB.Checked == true ? 1 : 0;
            
        }

        private void ShowNameOnServerMarkersCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ShowNameOnServerMarkers = ShowNameOnServerMarkersCB.Checked == true ? 1 : 0;
            
        }

        private void ShowDistanceOnServerMarkersCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ShowDistanceOnServerMarkers = ShowDistanceOnServerMarkersCB.Checked == true ? 1 : 0;
            
        }
    }
}