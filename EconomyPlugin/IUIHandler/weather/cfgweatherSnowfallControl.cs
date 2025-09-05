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
    public partial class cfgweatherSnowfallControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private weatherSnowfall _data;
        private weatherSnowfall _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgweatherSnowfallControl()
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
            _data = data as weatherSnowfall ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            SCactualNUD.Value = _data.current.actual;
            SCtimeNUD.Value = _data.current.time;
            SCdurationNUD.Value = _data.current.duration;
            SLminNUD.Value = _data.limits.min;
            SLmaxNUD.Value = _data.limits.max;
            STLminNUD.Value = _data.timelimits.min;
            STLmaxNUD.Value = _data.timelimits.max;
            SCLminNUD.Value = _data.changelimits.min;
            SCLmaxNUD.Value = _data.changelimits.max;
            STmaxNUD.Value = _data.thresholds.max;
            STminNUD.Value = _data.thresholds.min;
            STendNUD.Value = _data.thresholds.end;

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
        private weatherSnowfall CloneData(weatherSnowfall data)
        {
            // TODO: Implement actual cloning logic
            return new weatherSnowfall
            {
                // Copy properties here
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