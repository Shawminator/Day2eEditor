using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CaparePlugin
{
    /// <summary>
    /// Salvage yield rate, scrap per gram for a basket
    /// </summary>
    public partial class CapareSalvageYieldRateControl : UserControl, IUIHandler
    {
        public event Action DataChanged;
        private Type _parentType;
        private CapareSalvageYieldRate _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public CapareSalvageYieldRateControl()
        {
            InitializeComponent();
            BasketComboBox.Items.AddRange(CapareSalvageData.Baskets);
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
            _data = data as CapareSalvageYieldRate ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            BasketComboBox.Text = _data.Basket ?? "";
            SetNUD(RateNUD, (decimal)_data.Rate);

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

        private void BasketComboBox_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Basket = BasketComboBox.Text;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void RateNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Rate = (float)RateNUD.Value;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
    }
}
