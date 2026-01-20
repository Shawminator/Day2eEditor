using CoreUI.Forms;
using Day2eEditor;
using ExpansionPlugin;
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
    public partial class ExpansionNameTagSettingsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private NameTagsSettings _data;
        private NameTagsSettings _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionNameTagSettingsControl()
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
            _data = data as NameTagsSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = _data.Clone(); // Store original data for reset

            _suppressEvents = true;

            EnablePlayerTagsCB.Checked = _data.EnablePlayerTags == 1 ? true : false;
            PlayerTagViewRangeNUD.Value = (int)_data.PlayerTagViewRange;
            PlayerTagsIconTB.Text = _data.PlayerTagsIcon;
            PlayerTagsColorPB.BackColor = Color.FromArgb((int)_data.PlayerTagsColor);
            PlayerNameColorPB.BackColor = Color.FromArgb((int)_data.PlayerNameColor);
            OnlyInSafeZonesCB.Checked = _data.OnlyInSafeZones == 1 ? true : false;
            OnlyInTerritoriesCB.Checked = _data.OnlyInTerritories == 1 ? true : false;
            ShowPlayerItemInHandsCB.Checked = _data.ShowPlayerItemInHands == 1 ? true : false;
            ShowNPCTagsCB.Checked = _data.ShowNPCTags == 1 ? true : false;
            ShowPlayerFactionCB.Checked = _data.ShowNPCTags == 1 ? true : false;
            UseRarityColorForItemInHandsCB.Checked = _data.UseRarityColorForItemInHands == 1 ? true : false;

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

        private void EnablePlayerTagsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnablePlayerTags = EnablePlayerTagsCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void PlayerTagViewRangeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.PlayerTagViewRange = (int)PlayerTagViewRangeNUD.Value;
            HasChanges();
        }
        private void PlayerTagsIconTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.PlayerTagsIcon = PlayerTagsIconTB.Text;
            HasChanges();
        }
        private void PlayerTagsColorPB_Click(object sender, EventArgs e)
        {
            HandleColorChange(PlayerTagsColorPB);
        }

        private void PlayerNameColorPB_Click(object sender, EventArgs e)
        {
            HandleColorChange(PlayerNameColorPB);
        }
        private void HandleColorChange(PictureBox pb)
        {
            Color startColor = pb.BackColor;
            using (AdvancedColorPickerForm picker = new AdvancedColorPickerForm(startColor))
            {
                picker.StartPosition = FormStartPosition.CenterParent;
                if (picker.ShowDialog() == DialogResult.OK)
                {
                    pb.BackColor = picker.SelectedColor;

                    // Map PictureBox name to _data property dynamically
                    string propertyName = pb.Name.Replace("PB", ""); // e.g., SystemChatColorPB → SystemChatColor
                    var prop = typeof(NameTagsSettings).GetProperty(propertyName);
                    if (prop != null)
                    {
                        prop.SetValue(_data, (int?)pb.BackColor.ToArgb());
                    }

                    HasChanges();
                }
            }
        }
        private void OnlyInSafeZonesCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.OnlyInSafeZones = OnlyInSafeZonesCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void OnlyInTerritoriesCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.OnlyInTerritories = OnlyInTerritoriesCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void ShowPlayerItemInHandsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ShowPlayerItemInHands = ShowPlayerItemInHandsCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void ShowNPCTagsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ShowNPCTags = ShowNPCTagsCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void ShowPlayerFactionCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ShowPlayerFaction = ShowPlayerFactionCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void UseRarityColorForItemInHandsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseRarityColorForItemInHands = UseRarityColorForItemInHandsCB.Checked == true ? 1 : 0;
            HasChanges();
        }
    }
}