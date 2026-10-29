using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CaparePlugin
{
    /// <summary>
    /// WorkBench general, repair and tool wear settings
    /// </summary>
    public partial class CapareWorkBenchGeneralControl : UserControl, IUIHandler
    {
        public event Action DataChanged;
        private Type _parentType;
        private CapareWorkBenchData _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public CapareWorkBenchGeneralControl()
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
            _data = data as CapareWorkBenchData ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            DebugLoggingCB.Checked = _data.DebugLogging;
            RepairingAllowRepairFromRuinedCB.Checked = _data.Repairing.AllowRepairFromRuined;
            SetNUD(RepairingRuinedPunishmentMultiplierNUD, (decimal)_data.Repairing.RuinedPunishmentMultiplier);
            SetNUD(RepairingSewingKitUsageNUD, (decimal)_data.Repairing.SewingKitUsage);
            SetNUD(RepairingLeatherSewingKitUsageNUD, (decimal)_data.Repairing.LeatherSewingKitUsage);
            SetNUD(RepairingWeaponCleaningKitUsageNUD, (decimal)_data.Repairing.WeaponCleaningKitUsage);
            SetNUD(RepairingWhetstoneUsageNUD, (decimal)_data.Repairing.WhetstoneUsage);
            SetNUD(RepairingTireRepairKitUsageNUD, (decimal)_data.Repairing.TireRepairKitUsage);
            SetNUD(RepairingElectronicRepairKitUsageNUD, (decimal)_data.Repairing.ElectronicRepairKitUsage);
            SetNUD(RepairingEpoxyPuttyUsageNUD, (decimal)_data.Repairing.EpoxyPuttyUsage);
            SetNUD(ToolWearToolDamagePerUseNUD, (decimal)_data.ToolWear.ToolDamagePerUse);
            SetNUD(ToolWearWearPartDamagePerUseNUD, (decimal)_data.ToolWear.WearPartDamagePerUse);

            _suppressEvents = false;
        }

        #region Helper Methods
        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            // Node text does not depend on this data
        }
        private static void SetNUD(NumericUpDown nud, decimal value)
        {
            nud.Value = Math.Clamp(value, nud.Minimum, nud.Maximum);
        }

        #endregion

        private void DebugLoggingCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DebugLogging = DebugLoggingCB.Checked;
            DataChanged?.Invoke();
        }
        private void RepairingAllowRepairFromRuinedCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Repairing.AllowRepairFromRuined = RepairingAllowRepairFromRuinedCB.Checked;
            DataChanged?.Invoke();
        }
        private void RepairingRuinedPunishmentMultiplierNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Repairing.RuinedPunishmentMultiplier = (int)RepairingRuinedPunishmentMultiplierNUD.Value;
            DataChanged?.Invoke();
        }
        private void RepairingSewingKitUsageNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Repairing.SewingKitUsage = (int)RepairingSewingKitUsageNUD.Value;
            DataChanged?.Invoke();
        }
        private void RepairingLeatherSewingKitUsageNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Repairing.LeatherSewingKitUsage = (int)RepairingLeatherSewingKitUsageNUD.Value;
            DataChanged?.Invoke();
        }
        private void RepairingWeaponCleaningKitUsageNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Repairing.WeaponCleaningKitUsage = (int)RepairingWeaponCleaningKitUsageNUD.Value;
            DataChanged?.Invoke();
        }
        private void RepairingWhetstoneUsageNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Repairing.WhetstoneUsage = (int)RepairingWhetstoneUsageNUD.Value;
            DataChanged?.Invoke();
        }
        private void RepairingTireRepairKitUsageNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Repairing.TireRepairKitUsage = (int)RepairingTireRepairKitUsageNUD.Value;
            DataChanged?.Invoke();
        }
        private void RepairingElectronicRepairKitUsageNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Repairing.ElectronicRepairKitUsage = (int)RepairingElectronicRepairKitUsageNUD.Value;
            DataChanged?.Invoke();
        }
        private void RepairingEpoxyPuttyUsageNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Repairing.EpoxyPuttyUsage = (int)RepairingEpoxyPuttyUsageNUD.Value;
            DataChanged?.Invoke();
        }
        private void ToolWearToolDamagePerUseNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ToolWear.ToolDamagePerUse = (float)ToolWearToolDamagePerUseNUD.Value;
            DataChanged?.Invoke();
        }
        private void ToolWearWearPartDamagePerUseNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ToolWear.WearPartDamagePerUse = (float)ToolWearWearPartDamagePerUseNUD.Value;
            DataChanged?.Invoke();
        }
    }
}
