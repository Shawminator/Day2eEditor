using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class Vector3Control : UserControl, IUIHandler
    {
        private Type _parentType;
        private Vec3 _data;
        private Vec3 _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public Vector3Control()
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
            _data = data is Vec3 v ? v : throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            POSXNUD.Value = (decimal)_data.X;
            POSYNUD.Value = (decimal)_data.Y;
            POSZNUD.Value = (decimal)_data.Z;


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
        private Vec3 CloneData(Vec3 data)
        {
            // TODO: Implement actual cloning logic
            return new Vec3
            {
                X = data.X,
                Y = data.Y,
                Z = data.Z
            };
        }

        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                string split = _nodes.Last().Text.Split(':')[0];
                _nodes.Last().Text = split + ": [" + string.Join(", ", _data) + "]";
            }
        }

        #endregion

        private void POSXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.X = (float)POSXNUD.Value;
            HasChanges();
            UpdateTreeNodeText();
        }

        private void POSYNUD_ValueChanged(object sender, EventArgs e)
        {

        }

        private void POSZNUD_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}