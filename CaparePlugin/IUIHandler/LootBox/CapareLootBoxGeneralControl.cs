using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CaparePlugin
{
    /// <summary>
    /// LootBox general settings
    /// </summary>
    public partial class CapareLootBoxGeneralControl : UserControl, IUIHandler
    {
        public event Action DataChanged;
        private Type _parentType;
        private CapareLootBoxSettingsData _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public CapareLootBoxGeneralControl()
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
            _data = data as CapareLootBoxSettingsData ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            SetNUD(LootBoxLifetimeNUD, (decimal)_data.LootBoxLifetime);
            StaticLootBoxEnabledCB.Checked = _data.StaticLootBoxEnabled;

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

        private void LootBoxLifetimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.LootBoxLifetime = (int)LootBoxLifetimeNUD.Value;
            DataChanged?.Invoke();
        }
        private void StaticLootBoxEnabledCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.StaticLootBoxEnabled = StaticLootBoxEnabledCB.Checked;
            DataChanged?.Invoke();
        }
    }
}
