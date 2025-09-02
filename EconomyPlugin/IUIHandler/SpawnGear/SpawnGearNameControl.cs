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
    public partial class SpawnGearNameControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private IHasSpawnName _data;
        private IHasSpawnName _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public SpawnGearNameControl()
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
            _data = data as IHasSpawnName ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            SpawnGearNameTB.Text = _data.Name;

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
                parent.isDirty = _data.Name != _originalData.Name;
            }
        }

        #region Helper Methods

        /// <summary>
        /// Clones the data for reset purposes
        /// </summary>
        private IHasSpawnName CloneData(IHasSpawnName data)
        {
            // TODO: Implement actual cloning logic
            return new SimpleSpawnNameSnapshot
            {
                Name = data.Name
            };
        }

        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = $"Name: {_data.Name}";
            }
        }

        #endregion

        private void SpawnGearNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Name = SpawnGearNameTB.Text;
            HasChanges();
            UpdateTreeNodeText();
        }
    }
    internal class SimpleSpawnNameSnapshot : IHasSpawnName
    {
        public string Name { get; set; }
    }
}