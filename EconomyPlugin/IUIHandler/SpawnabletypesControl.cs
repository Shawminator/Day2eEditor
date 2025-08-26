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
    public partial class SpawnabletypesControl : UserControl, IUIHandler
    {
        private SpawnableType _data;
        private SpawnableType _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public SpawnabletypesControl()
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
            _data = data as SpawnableType ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            SpawnableTypeTB.Text = _data.name;

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
            var ef = _nodes.Last().FindParentOfType<cfgspawnabletypesFile>();
            if (ef != null)
                ef.isDirty = _data.name != _originalData.name;
        }

        #region Helper Methods

        /// <summary>
        /// Clones the data for reset purposes
        /// </summary>
        private SpawnableType CloneData(SpawnableType data)
        {
            // TODO: Implement actual cloning logic
            return new SpawnableType
            {
                name = data.name
            };
        }

        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = _data.name;
            }
        }

        #endregion

        private void darkButton30_Click(object sender, EventArgs e)
        {
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
                    SpawnableTypeTB.Text = _data.name = l;
                    UpdateTreeNodeText();
                    HasChanges();
                }
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }
    }
}