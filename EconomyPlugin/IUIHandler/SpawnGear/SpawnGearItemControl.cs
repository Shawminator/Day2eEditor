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
    public partial class SpawnGearItemControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private IHasSpawnItemType _data;
        private IHasSpawnItemType _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public SpawnGearItemControl()
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
            _data = data as IHasSpawnItemType ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            SpawnGearItemTypeTB.Text = _data.ItemType;

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
                parent.isDirty = _data.ItemType != _originalData.ItemType;
            }
        }

        #region Helper Methods

        /// <summary>
        /// Clones the data for reset purposes
        /// </summary>
        private IHasSpawnItemType CloneData(IHasSpawnItemType data)
        {
            return new SimpleSpawnItemTypeSnapshot
            {
                ItemType = data.ItemType
            };
        }

        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = _data.ItemType;
            }
        }

        #endregion

        private void darkButton70_Click(object sender, EventArgs e)
        {
            string Classname = "";
            AddItemfromTypes form = new AddItemfromTypes
            {
                UseOnlySingleItem = true
            };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    SpawnGearItemTypeTB.Text = _data.ItemType = l;
                    HasChanges();
                    UpdateTreeNodeText();
                }
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }
    }
    /// <summary>
    /// Helper class just to store a lightweight snapshot for comparison/reset
    /// </summary>
    internal class SimpleSpawnItemTypeSnapshot : IHasSpawnItemType
    {
        public string ItemType { get; set; }
    }
}