using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CaparePlugin
{
    /// <summary>
    /// WorkBench recipe
    /// </summary>
    public partial class CapareWorkBenchRecipeControl : UserControl, IUIHandler
    {
        public event Action DataChanged;
        private Type _parentType;
        private CapareWorkBenchRecipe _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public CapareWorkBenchRecipeControl()
        {
            InitializeComponent();
            CraftTypeComboBox.Items.AddRange(CapareWorkBenchData.CraftTypes);
            RecipeCategoryComboBox.Items.AddRange(CapareWorkBenchData.RecipeCategories);
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
            _data = data as CapareWorkBenchRecipe ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            Recipe_NameTB.Text = _data.Recipe_Name ?? "";
            ResultNameTB.Text = _data.ResultName ?? "";
            ResultDescriptionTB.Text = _data.ResultDescription ?? "";
            PreviewNameTB.Text = _data.PreviewName ?? "";
            IsQuantityCB.Checked = _data.IsQuantity;
            SetNUD(ResultCountNUD, (decimal)_data.ResultCount);
            CraftTypeComboBox.Text = _data.CraftType ?? "";
            RecipeCategoryComboBox.Text = _data.RecipeCategory ?? "";
            SetNUD(CraftTimeSecondsNUD, (decimal)_data.CraftTimeSeconds);
            SetNUD(PowerConsumptionNUD, (decimal)_data.PowerConsumption);
            SetNUD(GasConsumptionNUD, (decimal)_data.GasConsumption);
            IsAmmoCB.Checked = _data.IsAmmo;

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

        private void Recipe_NameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Recipe_Name = Recipe_NameTB.Text;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void ResultNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ResultName = ResultNameTB.Text;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void ResultDescriptionTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ResultDescription = ResultDescriptionTB.Text;
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
        private void ResultCountNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ResultCount = (int)ResultCountNUD.Value;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void CraftTypeComboBox_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CraftType = CraftTypeComboBox.Text;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void RecipeCategoryComboBox_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.RecipeCategory = RecipeCategoryComboBox.Text;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void CraftTimeSecondsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CraftTimeSeconds = (int)CraftTimeSecondsNUD.Value;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void PowerConsumptionNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.PowerConsumption = (float)PowerConsumptionNUD.Value;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void GasConsumptionNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.GasConsumption = (float)GasConsumptionNUD.Value;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void IsAmmoCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.IsAmmo = IsAmmoCB.Checked;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
    }
}
