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
    public partial class cfgplayerspawnGroupParamsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private playerspawnpointsGroup_params _data;
        private playerspawnpointsGroup_params _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgplayerspawnGroupParamsControl()
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
            _data = data as playerspawnpointsGroup_params ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            // TODO: Populate control with data fields here

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
        private playerspawnpointsGroup_params CloneData(playerspawnpointsGroup_params data)
        {
            // TODO: Implement actual cloning logic
            return new playerspawnpointsGroup_params
            {
                enablegroups = data.enablegroups,
                groups_as_regular = data.groups_as_regular,
                lifetime = data.lifetime,
                counter = data.counter
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

        private void GroupParamsenablegroupsCB_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}