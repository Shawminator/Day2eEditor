using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CaparePlugin
{
    /// <summary>
    /// LootBox type and the reward tables it can roll
    /// </summary>
    public partial class CapareLootBoxSettingsControl : UserControl, IUIHandler
    {
        public event Action DataChanged;
        private Type _parentType;
        private CapareLootBoxSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public CapareLootBoxSettingsControl()
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
            _data = data as CapareLootBoxSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            LootBoxNameTB.Text = _data.LootBoxName ?? "";

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

        private void LootBoxNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.LootBoxName = LootBoxNameTB.Text;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
    }
}
