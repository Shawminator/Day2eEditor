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
        public event Action<eventposdefEventPos> PositionChanged;
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
                _nodes.Last().Text = _data.ToString();
            }
        }

        #endregion

        private void EventSpawnPosXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.x = EventSpawnPosXNUD.Value;
            HasChanges();
            UpdateTreeNodeText();
            PositionChanged?.Invoke(_data);
        }

        private void EventSpawnPosZNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.z = EventSpawnPosZNUD.Value;
            HasChanges();
            UpdateTreeNodeText();
            PositionChanged?.Invoke(_data);
        }

        private void checkBox50_CheckedChanged(object sender, EventArgs e)
        {
            EventSpawnPosYNUD.Visible = checkBox50.Checked;
            if (_suppressEvents) return;

            if (checkBox50.Checked)
            {
                _data.y = 0;
                _data.ySpecified = true;
                EventSpawnPosYNUD.Value = _data.y;
            }
            else
            {
                _data.ySpecified = false;
            }
            HasChanges();
        }

        private void EventSpawnPosYNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.y = EventSpawnPosYNUD.Value;
            HasChanges();
            UpdateTreeNodeText();
        }

        private void checkBox51_CheckedChanged(object sender, EventArgs e)
        {
            EventSpawnPosANUD.Visible = checkBox51.Checked;
            if (_suppressEvents) return;
            if (checkBox51.Checked)
            {
                _data.a = 0;
                _data.aSpecified = true;
                EventSpawnPosANUD.Value = _data.a;
            }
            else
            {
                _data.aSpecified = false;
            }
            HasChanges();
        }

        private void EventSpawnPosANUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;

            _suppressEvents = true;
            if (EventSpawnPosANUD.Value < 0)
            {
                while (EventSpawnPosANUD.Value < 0)
                {
                    EventSpawnPosANUD.Value += 360;
                }
            }
            else if (EventSpawnPosANUD.Value >= 360)
            {
                while (EventSpawnPosANUD.Value >= 360)
                {
                    EventSpawnPosANUD.Value -= 360;
                }
            }
            _suppressEvents = false;
            _data.a = EventSpawnPosANUD.Value;
            HasChanges();
        }
    }
}