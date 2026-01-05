using CoreUI.Forms;
using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionMarketSettingsHUDControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private MarketSettings _data;
        private MarketSettings _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionMarketSettingsHUDControl()
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
            _data = data as MarketSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = _data.Clone();

            _suppressEvents = true;

            CurrencyIconTB.Text = _data.CurrencyIcon;

            var colours = _data.MarketMenuColors; // Assuming this exists in MarketSettings
            foreach (PropertyInfo property in colours.GetType().GetProperties())
            {
                // Get the hex color string from the property
                var hex = property.GetValue(colours) as string;
                if (string.IsNullOrWhiteSpace(hex))
                    continue;

                // Find the PictureBox by naming convention: "<PropertyName>Colour"
                var pb = this.Controls.Find(property.Name + "Colour", true)
                                      .OfType<PictureBox>()
                                      .FirstOrDefault();

                if (pb != null)
                {
                    SetChatColor(hex, pb);
                }
                else
                {
                    // Optional: log/diagnose missing PictureBox
                    MessageBox.Show($"PictureBox '{property.Name}Colour' not found.");
                }
            }
            _suppressEvents = false;
        }
        private void SetChatColor(string hexColor, PictureBox targetPB)
        {
            string formattedColor = "#" + hexColor.Substring(6) + hexColor.Remove(6, 2);
            Color selectedColor = ColorTranslator.FromHtml(formattedColor);
            targetPB.BackColor = selectedColor;
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

        private void CurrencyIconTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CurrencyIcon = CurrencyIconTB.Text;
            HasChanges();
        }
        private void HUDColour_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox pb)
            {
                HandleColorChange(pb);
            }
        }
        private void HandleColorChange(PictureBox pb)
        {
            Color startColor = pb.BackColor;
            using (AdvancedColorPickerForm picker = new AdvancedColorPickerForm(startColor))
            {
                picker.StartPosition = FormStartPosition.CenterParent;
                if (picker.ShowDialog() == DialogResult.OK)
                {
                    string colorHex = picker.SelectedColorHex;
                    Color selectedColor = ColorTranslator.FromHtml(colorHex);
                    pb.BackColor = selectedColor;
                    string propertyName = pb.Name.Replace("Colour", "");
                    var prop = typeof(MarketMenuColours).GetProperty(propertyName);
                    if (prop != null)
                    {
                        prop.SetValue(_data.MarketMenuColors, colorHex.Substring(4, 6) + colorHex.Substring(2, 2));
                    }
                    HasChanges();
                }
            }
        }
    }
}