using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CaparePlugin
{
    /// <summary>
    /// KeyCard door location
    /// </summary>
    public partial class CapareKeyCardLocationControl : UserControl, IUIHandler
    {
        public event Action DataChanged;
        private Type _parentType;
        private KeyCardLocations _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public CapareKeyCardLocationControl()
        {
            InitializeComponent();
            KeycardClassNameComboBox.Items.AddRange(AppServices.GetRequired<CapareManager>().CapareKeyCardConfig.Data.KeycardClassNames.ToArray());
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
            _data = data as KeyCardLocations ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            SetNUD(building_xNUD, (decimal)_data.building_x);
            SetNUD(building_yNUD, (decimal)_data.building_y);
            SetNUD(building_zNUD, (decimal)_data.building_z);
            BuildingClassNameTB.Text = _data.BuildingClassName ?? "";
            SetNUD(door_indexNUD, (decimal)_data.door_index);
            KeycardClassNameComboBox.Text = _data.KeycardClassName ?? "";
            SingleUseCB.Checked = _data.SingleUse;
            SetNUD(AutomaticCloseTimerNUD, (decimal)_data.AutomaticCloseTimer);
            CloseWarningCB.Checked = _data.CloseWarning;
            UseAlarmCB.Checked = _data.UseAlarm;
            AlarmSoundTB.Text = _data.AlarmSound ?? "";
            AlarmMapwideCB.Checked = _data.AlarmMapwide;
            SpawnLootOnInitCB.Checked = _data.SpawnLootOnInit;
            SetNUD(NumberOfLootPointsNUD, (decimal)_data.NumberOfLootPoints);
            SpawnSpecificLootCB.Checked = _data.SpawnSpecificLoot;
            SpawnInfectedCB.Checked = _data.SpawnInfected;

            _suppressEvents = false;
        }

        #region Helper Methods
        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = _data.ToString();
            }
        }
        private static void SetNUD(NumericUpDown nud, decimal value)
        {
            nud.Value = Math.Clamp(value, nud.Minimum, nud.Maximum);
        }

        #endregion

        private void building_xNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.building_x = (float)building_xNUD.Value;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void building_yNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.building_y = (float)building_yNUD.Value;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void building_zNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.building_z = (float)building_zNUD.Value;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void BuildingClassNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.BuildingClassName = BuildingClassNameTB.Text;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void door_indexNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.door_index = (int)door_indexNUD.Value;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void KeycardClassNameComboBox_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.KeycardClassName = KeycardClassNameComboBox.Text;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void SingleUseCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SingleUse = SingleUseCB.Checked;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void AutomaticCloseTimerNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AutomaticCloseTimer = (int)AutomaticCloseTimerNUD.Value;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void CloseWarningCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CloseWarning = CloseWarningCB.Checked;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void UseAlarmCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseAlarm = UseAlarmCB.Checked;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void AlarmSoundTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AlarmSound = AlarmSoundTB.Text;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void AlarmMapwideCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AlarmMapwide = AlarmMapwideCB.Checked;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void SpawnLootOnInitCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SpawnLootOnInit = SpawnLootOnInitCB.Checked;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void NumberOfLootPointsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NumberOfLootPoints = (int)NumberOfLootPointsNUD.Value;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void SpawnSpecificLootCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SpawnSpecificLoot = SpawnSpecificLootCB.Checked;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void SpawnInfectedCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SpawnInfected = SpawnInfectedCB.Checked;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
    }
}
