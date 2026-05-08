using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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

            List<ExpansionMarketTrader> intraderlists = AppServices.GetRequired<ExpansionManager>().ExpansionMarketTraderConfig.GetTradersForItem(_data);
            List<ExpansionTraderMaps> associatedNPC = AppServices.GetRequired<ExpansionManager>().ExpansionMarketTraderMapsConfig.GetNPCSFromTraders(intraderlists);
            List<ExpansionMarketTraderZone> itemzones = AppServices.GetRequired<ExpansionManager>().ExpansionMarketTraderZoneConfig.GetzonesfromNPClist(associatedNPC);
            ZoneCB.DataSource = itemzones;

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
            label7.Text = $"Stock Value:{trackBar1.Value.ToString()}";
            trackBar1.Maximum = (int)_data.MaxStockThreshold;
            trackBar1.Minimum = (int)_data.MinStockThreshold;
            textBox10.Text = _data.ClassName;
            numericUpDown6.Value = (int)_data.MaxPriceThreshold;
            numericUpDown7.Value = (int)_data.MinPriceThreshold;
            numericUpDown23.Value = (decimal)_data.SellPricePercent;
            numericUpDown8.Value = (int)_data.MaxStockThreshold;
            numericUpDown9.Value = (int)_data.MinStockThreshold;
            numericUpDown24.Value = (int)_data.QuantityPercent;

            GetBuyPrice();
            GetSellPrince();
            _suppressEvents = false;
        }
        private void GetBuyPrice()
        {
            ExpansionMarketTraderZone currentzone = ZoneCB.SelectedItem as ExpansionMarketTraderZone;
            decimal initialbuyPriceModifier = (decimal)currentzone.BuyPricePercent / 100;
            numericUpDown1.Value = _data.CalculatePrice(trackBar1.Value, (float)initialbuyPriceModifier, true);
        }
        private void GetSellPrince()
        {
            decimal SellpricePercent = (decimal)_data.SellPricePercent;
            if (SellpricePercent == -1)
            {
                ExpansionMarketTraderZone currentzone = ZoneCB.SelectedItem as ExpansionMarketTraderZone;
                SellpricePercent = (decimal)currentzone.SellPricePercent;
                if (SellpricePercent == -1)
                {
                    SellpricePercent = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionMarketSettingsConfig.Data.SellPricePercent;
                }
            }
            decimal initialSellPriceModifier = (SellpricePercent / 100) * (decimal)ConditionCB.SelectedValue;
            numericUpDown2.Value = _data.CalculatePrice(trackBar1.Value, (float)initialSellPriceModifier, true);
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
            GetSellPrince();
            GetBuyPrice();
        }
        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MinPriceThreshold = (int)numericUpDown7.Value;
            GetBuyPrice();
            GetSellPrince();
        }
        private void numericUpDown23_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SellPricePercent = (int)numericUpDown23.Value;
            GetBuyPrice();
            GetSellPrince();
        }
        private void numericUpDown8_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxStockThreshold = (int)numericUpDown8.Value;
            trackBar1.Maximum = (int)_data.MaxStockThreshold;
            trackBar1.Minimum = (int)_data.MinStockThreshold;
            GetBuyPrice();
            GetSellPrince();
        }
        private void numericUpDown9_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MinStockThreshold = (int)numericUpDown9.Value;
            trackBar1.Maximum = (int)_data.MaxStockThreshold;
            trackBar1.Minimum = (int)_data.MinStockThreshold;
            GetBuyPrice();
            GetSellPrince();
        }
        private void numericUpDown24_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.QuantityPercent = (int)numericUpDown24.Value;
            GetBuyPrice();
            GetSellPrince();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            label7.Text = $"Stock Value:{trackBar1.Value.ToString()}";
            GetBuyPrice();
            GetSellPrince();
        }
        private void ZoneCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            GetBuyPrice();
            GetSellPrince();
        }
        private void ConditionCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            GetBuyPrice();
            GetSellPrince();
        }

 
    }
    public class ComboConditionItem
    {
        public decimal multiplier { get; set; }        // value
        public string Name { get; set; }   // display
    }
}