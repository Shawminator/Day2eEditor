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
            if (data == null) return null;

            return new weatherSnowfall
            {
                current = data.current == null ? null : new weatherSnowfallCurrent
                {
                    actual = data.current.actual,
                    time = data.current.time,
                    duration = data.current.duration
                },
                limits = data.limits == null ? null : new weatherSnowfallLimits
                {
                    min = data.limits.min,
                    max = data.limits.max
                },
                timelimits = data.timelimits == null ? null : new weatherSnowfallTimelimits
                {
                    min = data.timelimits.min,
                    max = data.timelimits.max
                },
                changelimits = data.changelimits == null ? null : new weatherSnowfallChangelimits
                {
                    min = data.changelimits.min,
                    max = data.changelimits.max
                },
                thresholds = data.thresholds == null ? null : new weatherSnowfallThresholds
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

        private void SCactualNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.actual = SCactualNUD.Value;
            HasChanges();
        }
        private void SCtimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.time = (int)SCtimeNUD.Value;
            HasChanges();
        }
        private void SCdurationNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.duration = (int)SCdurationNUD.Value;
            HasChanges();
        }
        private void SLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.limits.min = SLminNUD.Value;
            HasChanges();
        }
        private void SLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.limits.max = SLmaxNUD.Value;
            HasChanges();
        }
        private void STLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.timelimits.min = (int)STLminNUD.Value;
            HasChanges();
        }
        private void STLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.timelimits.max = (int)STLmaxNUD.Value;
            HasChanges();
        }
        private void SCLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.changelimits.min = SCLminNUD.Value;
            HasChanges();
        }
        private void SCLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.changelimits.max = SCLmaxNUD.Value;
            HasChanges();
        }
        private void STminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.thresholds.min = STminNUD.Value;
            HasChanges();
        }
        private void STmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.thresholds.max = STmaxNUD.Value;
            HasChanges();
        }
        private void STendNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.thresholds.end = (int)STendNUD.Value;
            HasChanges();
        }
    }
}