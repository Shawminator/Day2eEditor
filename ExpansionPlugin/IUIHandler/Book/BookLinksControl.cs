using CoreUI.Forms;
using Day2eEditor;
using ExpansionPlugin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class BookLinksControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionBookLink _data;
        private ExpansionBookLink _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public BookLinksControl()
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
            _data = data as ExpansionBookLink ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            textBox12.Text = _data.Name;
            textBox13.Text = _data.URL;
            comboBox5.SelectedIndex = comboBox5.FindStringExact(_data.IconName);
            Color selectedColor = Color.FromArgb(_data.IconColor);
            LinkIconColour.BackColor = selectedColor;

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
        private ExpansionBookLink CloneData(ExpansionBookLink data)
        {
            if (data == null)
                return null;

            return new ExpansionBookLink
            {
                Name = data.Name,
                URL = data.URL,
                IconName = data.IconName,
                IconColor = data.IconColor
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

        private void LinkIconColour_Click(object sender, EventArgs e)
        {
            Color initialColor = Color.FromArgb(_data.IconColor);
            using (AdvancedColorPickerForm picker = new AdvancedColorPickerForm(initialColor))
            {
                picker.StartPosition = FormStartPosition.CenterParent;
                if (picker.ShowDialog() == DialogResult.OK)
                {
                    Color color = picker.SelectedColor;
                    _data.IconColor = color.ToArgb();
                    LinkIconColour.BackColor = color;
                    HasChanges();
                }
            }
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Name = textBox12.Text;
            HasChanges();
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.URL = textBox13.Text;
            HasChanges();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.IconName = comboBox5.SelectedItem.ToString();
            HasChanges();
        }
    }
}