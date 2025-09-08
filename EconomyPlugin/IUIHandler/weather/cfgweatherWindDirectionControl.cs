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
            if (data == null) return null;

            return new weatherWindDirection
            {
                current = data.current == null ? null : new weatherWindDirectionCurrent
                {
                    actual = data.current.actual,
                    time = data.current.time,
                    duration = data.current.duration
                },
                limits = data.limits == null ? null : new weatherWindDirectionLimits
                {
                    min = data.limits.min,
                    max = data.limits.max
                },
                timelimits = data.timelimits == null ? null : new weatherWindDirectionTimelimits
                {
                    min = data.timelimits.min,
                    max = data.timelimits.max
                },
                changelimits = data.changelimits == null ? null : new weatherWindDirectionChangelimits
                {
                    min = data.changelimits.min,
                    max = data.changelimits.max
                }
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

        private void WDCactualNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.actual = WDCactualNUD.Value;
            HasChanges();
        }
        private void WDCtimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.time = (int)WDCtimeNUD.Value;
            HasChanges();
        }
        private void WDCdurationNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.duration = (int)WDCdurationNUD.Value;
            HasChanges();
        }
        private void WDLminNUD_ValueChanged(object sender, EventArgs e)
        {

            if (_suppressEvents) return;
            _data.limits.min = WDLminNUD.Value;
            HasChanges();
        }
        private void WDLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.limits.max = WDLmaxNUD.Value;
            HasChanges();
        }
        private void WDTLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.timelimits.min = (int)WDTLminNUD.Value;
            HasChanges();
        }
        private void WDTLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.timelimits.max = (int)WDTLmaxNUD.Value;
            HasChanges();
        }
        private void WDCLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.changelimits.min = WDCLminNUD.Value;
            HasChanges();
        }
        private void WDCLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.changelimits.max = WDCLmaxNUD.Value;
            HasChanges();
        }
    }
}