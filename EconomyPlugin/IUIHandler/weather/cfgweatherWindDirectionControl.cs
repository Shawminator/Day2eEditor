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
    public partial class cfgweatherWindDirectionControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private weatherWindDirection _data;
        private weatherWindDirection _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgweatherWindDirectionControl()
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
            _data = data as weatherWindDirection ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            WDCactualNUD.Value = _data.current.actual;
            WDCtimeNUD.Value = _data.current.time;
            WDCdurationNUD.Value = _data.current.duration;
            WDLminNUD.Value = _data.limits.min;
            WDLmaxNUD.Value = _data.limits.max;
            WDTLminNUD.Value = _data.timelimits.min;
            WDTLmaxNUD.Value = _data.timelimits.max;
            WDCLminNUD.Value = _data.changelimits.min;
            WDCLmaxNUD.Value = _data.changelimits.max;

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
        private weatherWindDirection CloneData(weatherWindDirection data)
        {
            // TODO: Implement actual cloning logic
            return new weatherWindDirection
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