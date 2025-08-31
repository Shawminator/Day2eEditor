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
    public partial class SpawnGearsimpleChildrenUseDefaultAttributesControl : UserControl, IUIHandler
    {
        private IHassimpleChildrenUseDefaultAttributes _data;
        private IHassimpleChildrenUseDefaultAttributes _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public SpawnGearsimpleChildrenUseDefaultAttributesControl()
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
        public void LoadFromData(object data, List<TreeNode> selectedNodes)
        {
            // TODO: Replace ClassType with your actual type
            _data = data as IHassimpleChildrenUseDefaultAttributes ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            simpleChildrenUseDefaultAttributesCB.Checked = _data.SimpleChildrenUseDefaultAttributes;

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
            if (_nodes?.Any() != true) return;

            // TODO: Replace Parentfile with your actual parent type if different
            var ef = _nodes.Last().FindParentOfType<SpawnGearPresetFiles>();
            if (ef != null)
                ef.isDirty = _data.SimpleChildrenUseDefaultAttributes != _originalData.SimpleChildrenUseDefaultAttributes;
        }

        #region Helper Methods

        /// <summary>
        /// Clones the data for reset purposes
        /// </summary>
        private IHassimpleChildrenUseDefaultAttributes CloneData(IHassimpleChildrenUseDefaultAttributes data)
        {
            // TODO: Implement actual cloning logic
            return new SimpleIHassimpleChildrenUseDefaultAttributesSnapshot
            {
                SimpleChildrenUseDefaultAttributes = data.SimpleChildrenUseDefaultAttributes
            };
        }

        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() != true)
            {
                // TODO: Update _nodes.Last().Text based on _data
            }
        }

        #endregion

        private void simpleChildrenUseDefaultAttributesCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SimpleChildrenUseDefaultAttributes = simpleChildrenUseDefaultAttributesCB.Checked;    
            HasChanges();
        }
    }
    internal class SimpleIHassimpleChildrenUseDefaultAttributesSnapshot : IHassimpleChildrenUseDefaultAttributes
    {
        public bool SimpleChildrenUseDefaultAttributes { get; set; }
    }
}