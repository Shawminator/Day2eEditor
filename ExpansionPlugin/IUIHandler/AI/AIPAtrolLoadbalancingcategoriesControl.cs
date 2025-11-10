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
    public partial class AIPAtrolLoadbalancingcategoriesControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private Loadbalancingcategories _data;
        private Loadbalancingcategories _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public AIPAtrolLoadbalancingcategoriesControl()
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
            _data = data as Loadbalancingcategories ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            MinPlayersLBCNUD.Value = _data.MinPlayers;
            MaxPlayersLBCNUD.Value = _data.MaxPlayers;
            MaxPatrolsLBCNUD.Value = _data.MaxPatrols;

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
        private Loadbalancingcategories CloneData(Loadbalancingcategories data)
        {
            // TODO: Implement actual cloning logic
            return new Loadbalancingcategories
            {
                MinPlayers = data.MinPlayers,
                MaxPlayers = data.MaxPlayers,
                MaxPatrols = data.MaxPatrols,
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

        private void MinPlayersLBCNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MinPlayers = (int)MinPlayersLBCNUD.Value;
            HasChanges();
        }
        private void MaxPlayersLBCNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxPlayers = (int)MaxPlayersLBCNUD.Value;
            HasChanges();
        }

        private void MaxPatrolsLBCNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxPatrols = (int)MaxPatrolsLBCNUD.Value;
            HasChanges();
        }
    }
}