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
    public partial class cfgweatherRainControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private weatherRain _data;
        private weatherRain _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgweatherRainControl()
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
            _data = data as weatherRain ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            RCactualNUD.Value = _data.current.actual;
            RCtimeNUD.Value = _data.current.time;
            RCdurationNUD.Value = _data.current.duration;
            RLminNUD.Value = _data.limits.min;
            RLmaxNUD.Value = _data.limits.max;
            RTLminNUD.Value = _data.timelimits.min;
            RTLmaxNUD.Value = _data.timelimits.max;
            RCLminNUD.Value = _data.changelimits.min;
            RCLmaxNUD.Value = _data.changelimits.max;
            RTmaxNUD.Value = _data.thresholds.max;
            RTminNUD.Value = _data.thresholds.min;
            RTendNUD.Value = _data.thresholds.end;

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
        private weatherRain CloneData(weatherRain data)
        {
            if (data == null) return null;

            return new weatherRain
            {
                current = data.current == null ? null : new weatherRainCurrent
                {
                    actual = data.current.actual,
                    time = data.current.time,
                    duration = data.current.duration
                },
                limits = data.limits == null ? null : new weatherRainLimits
                {
                    min = data.limits.min,
                    max = data.limits.max
                },
                timelimits = data.timelimits == null ? null : new weatherRainTimelimits
                {
                    min = data.timelimits.min,
                    max = data.timelimits.max
                },
                changelimits = data.changelimits == null ? null : new weatherRainChangelimits
                {
                    min = data.changelimits.min,
                    max = data.changelimits.max
                },
                thresholds = data.thresholds == null ? null : new weatherRainThresholds
                {
                    min = data.thresholds.min,
                    max = data.thresholds.max,
                    end = data.thresholds.end
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

        private void RCactualNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.actual = RCactualNUD.Value;
            HasChanges();
        }
        private void RCtimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.time = (int)RCtimeNUD.Value;
            HasChanges();
        }
        private void RCdurationNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.duration = (int)RCdurationNUD.Value;
            HasChanges();
        }
        private void RLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.limits.min = RLminNUD.Value;
            HasChanges();
        }
        private void RLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.limits.min = RLmaxNUD.Value;
            HasChanges();
        }
        private void RTLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.timelimits.min = (int)RTLminNUD.Value;
            HasChanges();
        }
        private void RTLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.timelimits.max = (int)RTLmaxNUD.Value;
            HasChanges();
        }
        private void RCLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.changelimits.min = RCLminNUD.Value;
            HasChanges();
        }
        private void RCLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.changelimits.max = RCLmaxNUD.Value;
            HasChanges();
        }
        private void RTminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.thresholds.min = RTminNUD.Value;
            HasChanges();
        }
        private void RTmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.thresholds.max = RTmaxNUD.Value;
            HasChanges();
        }
        private void RTendNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.thresholds.end = (int)RTendNUD.Value;
            HasChanges();
        }
    }
}