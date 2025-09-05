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
    public partial class eventspawnZoneControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private eventposdefEventZone _data;
        private eventposdefEventZone _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public eventspawnZoneControl()
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
            _data = data as eventposdefEventZone ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            eventzonesminNUD.Value = _data.smin;
            eventzonesmaxNUD.Value = _data.smax;
            eventzonedminNUD.Value = _data.dmin;
            eventzonedmaxNUD.Value = _data.dmax;
            eventzonedNUD.Value = _data.r;

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
        private eventposdefEventZone CloneData(eventposdefEventZone data)
        {
            return new eventposdefEventZone
            {
                smin = data.smin,
                smax = data.smax,
                dmin = data.dmin,
                dmax = data.dmax,
                r = data.r
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

        private void eventzonesminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.smin = (int)eventzonesminNUD.Value;
            HasChanges();
        }
        private void eventzonesmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.smax = (int)eventzonesmaxNUD.Value;
            HasChanges();
        }
        private void eventzonedminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.dmin = (int)eventzonedminNUD.Value;
            HasChanges();
        }
        private void eventzonedmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.dmax = (int)eventzonedmaxNUD.Value;
            HasChanges();
        }
        private void eventzonedNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.r = (int)eventzonedNUD.Value;
            HasChanges();
        }
    }
}