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
    public partial class SpawnGearAttributesControl : UserControl, IUIHandler
    {
        private Attributes _data;
        private Attributes _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public SpawnGearAttributesControl()
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
            _data = data as Attributes ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            SpawnGearhealthMinNUD.Value = _data.healthMin;
            SpawnGearhealthMaxNUD.Value = _data.healthMax;
            SpawnGearQuanityMinNUD.Value = _data.quantityMin;
            SpawnGearQuanityMaxNUD.Value = _data.quantityMax;

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
                ef.isDirty = !_data.Equals(_originalData);
        }

        #region Helper Methods

        /// <summary>
        /// Clones the data for reset purposes
        /// </summary>
        private Attributes CloneData(Attributes data)
        {
            // TODO: Implement actual cloning logic
            return new Attributes
            {
                healthMin = data.healthMin,
                healthMax = data.healthMax,
                quantityMin = data.quantityMin,
                quantityMax = data.quantityMax
            };
        }


        #endregion

        private void SpawnGearhealthMinNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.healthMin = SpawnGearhealthMinNUD.Value;
            HasChanges();

        }

        private void SpawnGearhealthMaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.healthMax = SpawnGearhealthMaxNUD.Value;
            HasChanges();
        }

        private void SpawnGearQuanityMinNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.quantityMin = SpawnGearQuanityMinNUD.Value;
            HasChanges();
        }

        private void SpawnGearQuanityMaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.quantityMax = SpawnGearQuanityMaxNUD.Value;
            HasChanges();
        }
    }
}