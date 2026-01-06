using CoreUI.Forms;
using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionChatColorsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionChatColors _data;
        private ExpansionChatColors _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionChatColorsControl()
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
            _data = data as ExpansionChatColors ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = _data.Clone();

            _suppressEvents = true;


            SetChatColor(_data.SystemChatColor, SystemChatColorPB);
            SetChatColor(_data.AdminChatColor, AdminChatColorPB);
            SetChatColor(_data.GlobalChatColor, GlobalChatColorPB);
            SetChatColor(_data.DirectChatColor, DirectChatColorPB);
            SetChatColor(_data.PartyChatColor, PartyChatColorPB);
            SetChatColor(_data.TransportChatColor, TransportChatColorPB);
            SetChatColor(_data.TransmitterChatColor, TransmitterChatColorPB);
            SetChatColor(_data.StatusMessageColor, StatusMessageColorPB);
            SetChatColor(_data.ActionMessageColor, ActionMessageColorPB);
            SetChatColor(_data.FriendlyMessageColor, FriendlyMessageColorPB);
            SetChatColor(_data.ImportantMessageColor, ImportantMessageColorPB);
            SetChatColor(_data.DefaultMessageColor, DefaultMessageColorPB);



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

        private void SystemChatColorPB_Click(object sender, EventArgs e)
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

                    // Map PictureBox name to _data property dynamically
                    string propertyName = pb.Name.Replace("PB", ""); // e.g., SystemChatColorPB → SystemChatColor
                    var prop = typeof(ExpansionChatColors).GetProperty(propertyName);
                    if (prop != null)
                    {
                        prop.SetValue(_data, colorHex.Substring(4, 6) + colorHex.Substring(2, 2));
                    }

                    HasChanges();
                }
            }
        }

    }
}