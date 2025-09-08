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
    public partial class cfgweatherFogControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private weatherFog _data;
        private weatherFog _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgweatherFogControl()
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
            _data = data as weatherFog ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            FCactualNUD.Value = _data.current.actual;
            FCtimeNUD.Value = _data.current.time;
            FCdurationNUD.Value = _data.current.duration;
            FLminNUD.Value = _data.limits.min;
            FLmaxNUD.Value = _data.limits.max;
            FTLminNUD.Value = _data.timelimits.min;
            FTLmaxNUD.Value = _data.timelimits.max;
            FCLminNUD.Value = _data.changelimits.min;
            FCLmaxNUD.Value = _data.changelimits.max;

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
        private weatherFog CloneData(weatherFog data)
        {
            if (data == null) return null;

            return new weatherFog
            {
                current = data.current == null ? null : new weatherFogCurrent
                {
                    actual = data.current.actual,
                    time = data.current.time,
                    duration = data.current.duration
                },
                limits = data.limits == null ? null : new weatherFogLimits
                {
                    min = data.limits.min,
                    max = data.limits.max
                },
                timelimits = data.timelimits == null ? null : new weatherFogTimelimits
                {
                    min = data.timelimits.min,
                    max = data.timelimits.max
                },
                changelimits = data.changelimits == null ? null : new weatherFogChangelimits
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

        private void FCactualNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
           _data.current.actual = FCactualNUD.Value;
            HasChanges();
        }
        private void FCtimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
           _data.current.time = (int)FCtimeNUD.Value;
            HasChanges();
        }
        private void FCdurationNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
           _data.current.duration = (int)FCdurationNUD.Value;
            HasChanges();
        }
        private void FLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
           _data.limits.min = FLminNUD.Value;
            HasChanges();
        }
        private void FLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
           _data.limits.max = FLmaxNUD.Value;
            HasChanges();
        }
        private void FTLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
           _data.timelimits.min = (int)FTLminNUD.Value;
            HasChanges();
        }
        private void FTLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
           _data.timelimits.max = (int)FTLmaxNUD.Value;
            HasChanges();
        }
        private void FCLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
           _data.changelimits.min = FCLminNUD.Value;
            HasChanges();
        }
        private void FCLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
           _data.changelimits.max = FCLmaxNUD.Value;
            HasChanges();
        }
    }
}