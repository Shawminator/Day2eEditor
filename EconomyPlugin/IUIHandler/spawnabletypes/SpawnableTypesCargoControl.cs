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
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public BindingList<randompresetsCargo> cargoItems = new BindingList<randompresetsCargo>();

        public SpawnableTypesCargoControl()
        {
            InitializeComponent();
            foreach (CfgrandompresetsFile rpf in AppServices.GetRequired<EconomyManager>().cfgrandompresetsConfig.MutableItems)
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

            _suppressEvents = true;

            CarcgoChanceNUD.Visible = UseCargoChanceCB.Checked = _data.chanceSpecified;
            CarcgoChanceNUD.Value = _data.chance;
            if (CargoPresetGB.Visible = CargoPresetTB.Visible = IsCargoPresetCB.Checked = _data.preset != null)
            {
                CargoPresetTB.Text = _data.preset;
            }

            _suppressEvents = false;
        }
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
        private void UseCargoChanceCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            CarcgoChanceNUD.Visible = _data.chanceSpecified = UseCargoChanceCB.Checked;
            UpdateTreeNodeText();
        }

        private void CarcgoChanceNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.chance = CarcgoChanceNUD.Value;
            UpdateTreeNodeText();
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
        }
        private void darkButton36_Click(object sender, EventArgs e)
        {
            randompresetsCargo newcargopreset = CargoPresetComboBox.SelectedItem as randompresetsCargo;
            CargoPresetTB.Text = _data.preset = newcargopreset.name;
            UpdateTreeNodeText();
        }
    }
}