using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CaparePlugin
{
    /// <summary>
    /// Static loot box set
    /// </summary>
    public partial class CapareLootBoxStaticBoxControl : UserControl, IUIHandler
    {
        public event Action DataChanged;
        private Type _parentType;
        private CapareLootBoxStaticBox _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public CapareLootBoxStaticBoxControl()
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
            _data = data as CapareLootBoxStaticBox ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            LootBoxSetNameTB.Text = _data.LootBoxSetName ?? "";
            SetNUD(NumberOfPositionsNUD, (decimal)_data.NumberOfPositions);
            UseCustomLootListCB.Checked = _data.UseCustomLootList;

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

        private void LootBoxSetNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.LootBoxSetName = LootBoxSetNameTB.Text;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void NumberOfPositionsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NumberOfPositions = (int)NumberOfPositionsNUD.Value;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void UseCustomLootListCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseCustomLootList = UseCustomLootListCB.Checked;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
    }
}
