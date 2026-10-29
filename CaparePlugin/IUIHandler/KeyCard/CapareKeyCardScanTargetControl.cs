using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CaparePlugin
{
    /// <summary>
    /// Building scan target, every matching building door on the map is locked
    /// </summary>
    public partial class CapareKeyCardScanTargetControl : UserControl, IUIHandler
    {
        public event Action DataChanged;
        private Type _parentType;
        private CapareBuildingScanTarget _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public CapareKeyCardScanTargetControl()
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
            _data = data as CapareBuildingScanTarget ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            BuildingClassNameTB.Text = _data.BuildingClassName ?? "";
            SetNUD(DoorIndexNUD, (decimal)_data.DoorIndex);
            AreaNameTB.Text = _data.AreaName ?? "";
            KeycardClassNameComboBox.Text = _data.KeycardClassName ?? "";
            AlarmSoundTB.Text = _data.AlarmSound ?? "";
            AlarmMapwideCB.Checked = _data.AlarmMapwide;
            SingleUseCB.Checked = _data.SingleUse;
            SetNUD(AutomaticCloseTimerNUD, (decimal)_data.AutomaticCloseTimer);

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

        private void BuildingClassNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.BuildingClassName = BuildingClassNameTB.Text;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void DoorIndexNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DoorIndex = (int)DoorIndexNUD.Value;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void AreaNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AreaName = AreaNameTB.Text;
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
    }
}
