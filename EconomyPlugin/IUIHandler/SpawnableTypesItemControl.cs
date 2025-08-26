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
    public partial class SpawnableTypesItemControl : UserControl, IUIHandler
    {
        private spawnableTypeItem _data;
        private spawnableTypeItem _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public SpawnableTypesItemControl()
        {
            InitializeComponent();
            List<string> presets = new List<string>();
            foreach (cfgspawnabletypesFile ctc in AppServices.GetRequired<EconomyManager>().cfgspawnabletypesConfig.AllData)
            {
                foreach (SpawnableType SP in ctc.Data.type)
                {
                    if (!presets.Contains(SP.name))
                    {
                        presets.Add(SP.name);
                    }
                }
            }
            ItemPresetCB.DataSource = presets;
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
            _data = data as spawnableTypeItem ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            ItemNameTB.Text = _data.name;
            ItemChanceNUD.Visible = UseItemchanceCB.Checked = _data.chanceSpecified;
            ItemChanceNUD.Value = _data.chance;
            ItemPresetGB.Visible = isItemEquipCB.Checked = _data.equipSpecified;
            if (itemQuantGB.Visible = checkBox49.Checked = (_data.quantmaxSpecified && _data.quantminSpecified))
            {
                numericUpDown4.Value = _data.quantmin;
                numericUpDown3.Value = _data.quantmax;
            }

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
                ef.isDirty = !_data.Equals(_originalData);
        }

        #region Helper Methods

        /// <summary>
        /// Clones the data for reset purposes
        /// </summary>
        private spawnableTypeItem CloneData(spawnableTypeItem data)
        {
            return new spawnableTypeItem
            {
                name = data.name,
                equip = data.equip,
                equipSpecified = data.equipSpecified,
                chance = data.chance,
                chanceSpecified = data.chanceSpecified,
                quantmin = data.quantmin,
                quantminSpecified = data.quantminSpecified,
                quantmax = data.quantmax,
                quantmaxSpecified = data.quantmaxSpecified
            };
        }

        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                string itemstring = $"Item = {_data.name}";
                if (_data.equipSpecified)
                {
                    itemstring += " equip = " + _data.equip;
                }
                if (_data.chanceSpecified)
                {
                    itemstring += " Chance = " + _data.chance;
                }
                if (_data.quantminSpecified && _data.quantmaxSpecified)
                {
                    itemstring += " quantMin = " + _data.quantmin + " quantMax = " + _data.quantmax;
                }
                _nodes.Last().Text = itemstring;
            }
        }

        #endregion

        private void CargoChangeItemButton_Click(object sender, EventArgs e)
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
                    ItemNameTB.Text = _data.name = l;
                    UpdateTreeNodeText();
                    HasChanges();
                }
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }

        private void UseItemchanceCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            ItemChanceNUD.Visible = _data.chanceSpecified = UseItemchanceCB.Checked;
            UpdateTreeNodeText();
            HasChanges();
        }

        private void ItemChanceNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.chance = ItemChanceNUD.Value;
            UpdateTreeNodeText();
            HasChanges();
        }

        private void checkBox49_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            itemQuantGB.Visible = _data.quantminSpecified = _data.quantmaxSpecified = checkBox49.Checked;
            numericUpDown4.Value = _data.quantmin;
            numericUpDown3.Value = _data.quantmax;
            UpdateTreeNodeText();
            HasChanges();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.quantmin = (int)numericUpDown4.Value;
            UpdateTreeNodeText();
            HasChanges();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.quantmax = (int)numericUpDown3.Value;
            UpdateTreeNodeText();
            HasChanges();
        }

        private void isItemEquipCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (ItemPresetGB.Visible = _data.equipSpecified = _data.equip = isItemEquipCB.Checked)
            {

            }
            else
            {

            }
            UpdateTreeNodeText();
            HasChanges();
        }

        private void darkButton26_Click(object sender, EventArgs e)
        {
            string newitem = ItemPresetCB.GetItemText(ItemPresetCB.SelectedItem);
            _data.name = ItemNameTB.Text = newitem;
            UpdateTreeNodeText();
            HasChanges();
        }
    }
}