using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class eventgroupsspawnpositionControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private eventposdefEventPos _data;
        private eventposdefEventPos _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public eventgroupsspawnpositionControl()
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
            _data = data as eventposdefEventPos ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            EventSpawnPosXNUD.Value = _data.x;
            if (EventSpawnPosYNUD.Visible = checkBox50.Checked = _data.ySpecified)
            {
                EventSpawnPosYNUD.Value = _data.y;
            }
            EventSpawnPosZNUD.Value = _data.z;
            if (EventSpawnPosANUD.Visible = checkBox51.Checked = _data.aSpecified)
                EventSpawnPosANUD.Value = _data.a;

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
        private eventposdefEventPos CloneData(eventposdefEventPos data)
        {
            // TODO: Implement actual cloning logic
            return new eventposdefEventPos
            {
               x = data.x,
               z = data.z,
               ySpecified = data.ySpecified,
               y = data.y,
               aSpecified = data.aSpecified,
               a = data.a
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
    }
}