using CoreUI.Forms;
using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionMapCompassControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionMapSettings _data;
        private ExpansionMapSettings _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionMapCompassControl()
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
            _data = data as ExpansionMapSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = _data.Clone();

            _suppressEvents = true;

            EnableHUDCompassCB.Checked = _data.EnableHUDCompass == 1 ? true : false;
            NeedGPSItemForHUDCompassCB.Checked = _data.NeedGPSItemForHUDCompass == 1 ? true : false;
            NeedCompassItemForHUDCompassCB.Checked = _data.NeedCompassItemForHUDCompass == 1 ? true : false;

            Color CompassColor = Color.FromArgb((int)_data.CompassColor);
            CompassColorPB.BackColor = CompassColor;

            Color CompassBadgesColor = Color.FromArgb((int)_data.CompassBadgesColor);
            CompassBadgesColorPB.BackColor = CompassBadgesColor;

            _suppressEvents = false;
        }
        /// <summary>
        /// Applies changes to the data and updates the original snapshot
        /// </summary>
        public void ApplyChanges()
        {
            _originalData = _data.Clone();
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

        private void EnableHUDCompassCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableHUDCompass = EnableHUDCompassCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void NeedGPSItemForHUDCompassCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NeedGPSItemForHUDCompass = NeedGPSItemForHUDCompassCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void NeedCompassItemForHUDCompassCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NeedCompassItemForHUDCompass = NeedCompassItemForHUDCompassCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void CompassColor_Click(object sender, EventArgs e)
        {
            Color initialColor = Color.FromArgb((int)_data.CompassColor);
            using (AdvancedColorPickerForm picker = new AdvancedColorPickerForm(initialColor))
            {
                picker.StartPosition = FormStartPosition.CenterParent;
                if (picker.ShowDialog() == DialogResult.OK)
                {
                    Color color = picker.SelectedColor;
                    _data.CompassColor = color.ToArgb();
                    CompassColorPB.BackColor = color;
                    HasChanges();
                }
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Color initialColor = Color.FromArgb((int)_data.CompassBadgesColor);
            using (AdvancedColorPickerForm picker = new AdvancedColorPickerForm(initialColor))
            {
                picker.StartPosition = FormStartPosition.CenterParent;
                if (picker.ShowDialog() == DialogResult.OK)
                {
                    Color color = picker.SelectedColor;
                    _data.CompassBadgesColor = color.ToArgb();
                    CompassBadgesColorPB.BackColor = color;
                    HasChanges();
                }
            }
        }
    }
}