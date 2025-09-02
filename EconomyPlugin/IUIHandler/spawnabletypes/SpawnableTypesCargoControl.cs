using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class SpawnableTypesCargoControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private spawnableTypeCargo _data;
        private spawnableTypeCargo _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public BindingList<randompresetsCargo> cargoItems = new BindingList<randompresetsCargo>();

        public SpawnableTypesCargoControl()
        {
            InitializeComponent();
            foreach (cfgrandompresetsFile rpf in AppServices.GetRequired<EconomyManager>().cfgrandompresetsConfig.AllData)
            {
                foreach (var item in rpf.Data.Items)
                {
                    if (item is randompresetsCargo rpc)
                    {
                        cargoItems.Add(rpc);
                    }
                }
            }
            CargoPresetComboBox.DataSource = cargoItems;
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
            _data = data as spawnableTypeCargo ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            CarcgoChanceNUD.Visible = UseCargoChanceCB.Checked = _data.chanceSpecified;
            CarcgoChanceNUD.Value = _data.chance;
            if (CargoPresetGB.Visible = CargoPresetTB.Visible = IsCargoPresetCB.Checked = _data.preset != null)
            {
                CargoPresetTB.Text = _data.preset;
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
        private spawnableTypeCargo CloneData(spawnableTypeCargo data)
        {
            // TODO: Implement actual cloning logic
            return new spawnableTypeCargo
            {
                chance = data.chance,
                chanceSpecified = data.chanceSpecified,
                preset = data.preset,
                damage = null,
                item = null
            };
        }

        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                string cargostring = "Cargo :";
                if (_data.preset != null)
                {
                    cargostring += " Preset = " + _data.preset;
                }
                if (_data.chanceSpecified)
                {
                    cargostring += " Chance = " + _data.chance;
                }
                _nodes.Last().Text = cargostring;
            }
        }

        #endregion

        private void UseCargoChanceCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            CarcgoChanceNUD.Visible = _data.chanceSpecified = UseCargoChanceCB.Checked;
            UpdateTreeNodeText();
            HasChanges();
        }

        private void CarcgoChanceNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.chance = CarcgoChanceNUD.Value;
            UpdateTreeNodeText();
            HasChanges();
        }

        private void IsCargoPresetCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (CargoPresetTB.Visible = CargoPresetGB.Visible = IsCargoPresetCB.Checked)
            {
                CargoPresetTB.Text = _data.preset;
            }
            else
            {
                _data.preset = null;
            }
            UpdateTreeNodeText();
            HasChanges();
        }

        private void darkButton36_Click(object sender, EventArgs e)
        {
            randompresetsCargo newcargopreset = CargoPresetComboBox.SelectedItem as randompresetsCargo;
            CargoPresetTB.Text = _data.preset = newcargopreset.name;
            UpdateTreeNodeText();
            HasChanges();
        }
    }
}