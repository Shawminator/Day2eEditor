using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionMarketItemControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionMarketItem _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionMarketItemControl()
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
            _data = data as ExpansionMarketItem ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;


            List<ComboConditionItem> conditionList = new()
            {
                new ComboConditionItem { Name = "Pristine x1.0",        multiplier = 1.0m },
                new ComboConditionItem { Name = "Worn x0.75",           multiplier = 0.75m },
                new ComboConditionItem { Name = "Damaged x0.5",         multiplier = 0.5m },
                new ComboConditionItem { Name = "Badly Damaged x0.25",  multiplier = 0.25m },
                new ComboConditionItem { Name = "Ruined x0",            multiplier = 0m }
            };

            ConditionCB.DataSource = conditionList;
            ConditionCB.SelectedIndex = 0;
            ConditionCB.DisplayMember = "Name";
            ConditionCB.ValueMember = "multiplier";
            trackBar1.Maximum = (int)_data.MaxStockThreshold;
            trackBar1.Minimum = (int)_data.MinStockThreshold;
            textBox10.Text = _data.ClassName;
            numericUpDown6.Value = (int)_data.MaxPriceThreshold;
            numericUpDown7.Value = (int)_data.MinPriceThreshold;
            numericUpDown23.Value = (decimal)_data.SellPricePercent;
            numericUpDown8.Value = (int)_data.MaxStockThreshold;
            numericUpDown9.Value = (int)_data.MinStockThreshold;
            numericUpDown24.Value = (int)_data.QuantityPercent;

            decimal SellpricePercent = (decimal)_data.SellPricePercent;
            if(SellpricePercent == -1)
            {
                
            }

            
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
                _nodes.Last().Text = _data.ClassName;
            }
        }

        #endregion

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ClassName = textBox10.Text;
            UpdateTreeNodeText();
        }
        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxPriceThreshold = (int)numericUpDown6.Value;
        }
        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MinPriceThreshold = (int)numericUpDown7.Value;
        }
        private void numericUpDown23_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SellPricePercent = (int)numericUpDown23.Value;
        }
        private void numericUpDown8_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxStockThreshold = (int)numericUpDown8.Value;
        }
        private void numericUpDown9_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MinStockThreshold = (int)numericUpDown9.Value;
        }
        private void numericUpDown24_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.QuantityPercent = (int)numericUpDown24.Value;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }
    }
    public class ComboConditionItem
    {
        public decimal multiplier { get; set; }        // value
        public string Name { get; set; }   // display
    }
}