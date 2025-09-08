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
    public partial class cfgweatherWindMagnitudeControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private weatherWindMagnitude _data;
        private weatherWindMagnitude _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgweatherWindMagnitudeControl()
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
            _data = data as weatherWindMagnitude ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            WMCactualNUD.Value = _data.current.actual;
            WMCtimeNUD.Value = _data.current.time;
            WMCdurationNUD.Value = _data.current.duration;
            WMLminNUD.Value = _data.limits.min;
            WMLmaxNUD.Value = _data.limits.max;
            WMTLminNUD.Value = _data.timelimits.min;
            WMTLmaxNUD.Value = _data.timelimits.max;
            WMCLminNUD.Value = _data.changelimits.min;
            WMCLmaxNUD.Value = _data.changelimits.max;

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
        private weatherWindMagnitude CloneData(weatherWindMagnitude data)
        {
            if (data == null) return null;

            return new weatherWindMagnitude
            {
                current = data.current == null ? null : new weatherWindMagnitudeCurrent
                {
                    actual = data.current.actual,
                    time = data.current.time,
                    duration = data.current.duration
                },
                limits = data.limits == null ? null : new weatherWindMagnitudeLimits
                {
                    min = data.limits.min,
                    max = data.limits.max
                },
                timelimits = data.timelimits == null ? null : new weatherWindMagnitudeTimelimits
                {
                    min = data.timelimits.min,
                    max = data.timelimits.max
                },
                changelimits = data.changelimits == null ? null : new weatherWindMagnitudeChangelimits
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

        private void WMCactualNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.actual = WMCactualNUD.Value;
            HasChanges();
        }
        private void WMCtimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.time = (int)WMCtimeNUD.Value;
            HasChanges();
        }
        private void WMCdurationNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.duration = (int)WMCdurationNUD.Value;
            HasChanges();
        }
        private void WMLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.limits.min = WMLminNUD.Value;
            HasChanges();
        }
        private void WMLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.limits.max = WMLmaxNUD.Value;
            HasChanges();
        }
        private void WMTLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.timelimits.min = (int)WMTLminNUD.Value;
            HasChanges();
        }
        private void WMTLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.timelimits.max = (int)WMTLmaxNUD.Value;
            HasChanges();
        }
        private void WMCLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.changelimits.min = WMCLminNUD.Value;
            HasChanges();
        }
        private void WMCLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.changelimits.max = WMCLmaxNUD.Value;
            HasChanges();
        }
    }
}