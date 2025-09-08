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
    public partial class cfgweatherOvercastControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private weatherOvercast _data;
        private weatherOvercast _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgweatherOvercastControl()
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
            _data = data as weatherOvercast ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            OCactualNUD.Value = _data.current.actual;
            OCtimeNUD.Value = _data.current.time;
            OCdurationNUD.Value = _data.current.duration;
            OLminNUD.Value = _data.limits.min;
            OLmaxNUD.Value = _data.limits.max;
            OTLminNUD.Value = _data.timelimits.min;
            OTLmaxNUD.Value = _data.timelimits.max;
            OCLminNUD.Value = _data.changelimits.min;
            OCLmaxNUD.Value = _data.changelimits.max;

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
        private weatherOvercast CloneData(weatherOvercast data)
        {
            if (data == null) return null;

            return new weatherOvercast
            {
                current = data.current == null ? null : new weatherOvercastCurrent
                {
                    actual = data.current.actual,
                    time = data.current.time,
                    duration = data.current.duration
                },
                limits = data.limits == null ? null : new weatherOvercastLimits
                {
                    min = data.limits.min,
                    max = data.limits.max
                },
                timelimits = data.timelimits == null ? null : new weatherOvercastTimelimits
                {
                    min = data.timelimits.min,
                    max = data.timelimits.max
                },
                changelimits = data.changelimits == null ? null : new weatherOvercastChangelimits
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

        private void OCactualNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.actual = OCactualNUD.Value;
            HasChanges();
        }
        private void OCtimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.time = (int)OCtimeNUD.Value;
            HasChanges();
        }
        private void OCdurationNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.duration = (int)OCdurationNUD.Value;
            HasChanges();
        }
        private void OLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.limits.min = OLminNUD.Value;
            HasChanges();
        }
        private void OLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.limits.max = OLmaxNUD.Value;
            HasChanges();
        }
        private void OTLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.timelimits.min = (int)OTLminNUD.Value;
            HasChanges();
        }
        private void OTLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.timelimits.max = (int)OTLmaxNUD.Value;
            HasChanges();
        }
        private void OCLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.changelimits.min = OCLminNUD.Value;
            HasChanges();
        }
        private void OCLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.changelimits.max = OCLmaxNUD.Value;
            HasChanges();
        }
    }
}