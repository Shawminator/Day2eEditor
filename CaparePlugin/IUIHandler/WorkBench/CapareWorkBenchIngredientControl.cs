using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CaparePlugin
{
    /// <summary>
    /// WorkBench recipe ingredient
    /// </summary>
    public partial class CapareWorkBenchIngredientControl : UserControl, IUIHandler
    {
        public event Action DataChanged;
        private Type _parentType;
        private CapareRecipeIngredient _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public CapareWorkBenchIngredientControl()
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
            _data = data as CapareRecipeIngredient ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            IngredientNameTB.Text = _data.IngredientName ?? "";
            PreviewImageTB.Text = _data.PreviewImage ?? "";
            PreviewNameTB.Text = _data.PreviewName ?? "";
            IsQuantityCB.Checked = _data.IsQuantity;
            SetNUD(IngredientAmountNUD, (decimal)_data.IngredientAmount);
            DestroyAfterUseCB.Checked = _data.DestroyAfterUse;
            SetNUD(HealthDecreaseNUD, (decimal)_data.HealthDecrease);

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

        private void IngredientNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.IngredientName = IngredientNameTB.Text;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void PreviewImageTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.PreviewImage = PreviewImageTB.Text;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void PreviewNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.PreviewName = PreviewNameTB.Text;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void IsQuantityCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.IsQuantity = IsQuantityCB.Checked;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void IngredientAmountNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.IngredientAmount = (int)IngredientAmountNUD.Value;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void DestroyAfterUseCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DestroyAfterUse = DestroyAfterUseCB.Checked;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void HealthDecreaseNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HealthDecrease = (float)HealthDecreaseNUD.Value;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
    }
}
