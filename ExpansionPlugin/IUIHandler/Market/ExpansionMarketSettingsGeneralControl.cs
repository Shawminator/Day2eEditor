using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionMarketSettingsGeneralControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private MarketSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionMarketSettingsGeneralControl()
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
            _data = data as MarketSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            MarketSytemeEnabedcheckBox.Checked = _data.MarketSystemEnabled == 1 ? true : false;
            ATMSytemeEnabledcheckBox.Checked = _data.ATMSystemEnabled == 1 ? true : false;
            MaxdepositUpDown.Value = (int)_data.MaxDepositMoney;
            DefaultDepositUpDown.Value = (int)_data.DefaultDepositMoney;
            ATMPlayerTransferEnabledcheckbox.Checked = _data.ATMPlayerTransferEnabled == 1 ? true : false;
            ATMPartyLockerEnabledCheckBox.Checked = _data.ATMPartyLockerEnabled == 1 ? true : false;
            MaxPartyDepositMoneyUpDown.Value = (int)_data.MaxPartyDepositMoney;
            SellPricePercentNUD.Value = (int)_data.SellPricePercent;
            UseWholeMapForATMPlayerListCheckBox.Checked = _data.UseWholeMapForATMPlayerList == 1 ? true : false;
            NetworkBatchSizeNUD.Value = (int)_data.NetworkBatchSize;
            MaxVehicleDistanceToTraderNUD.Value = (decimal)_data.MaxVehicleDistanceToTrader;
            MaxLargeVehicleDistanceToTraderNUD.Value = (decimal)_data.MaxLargeVehicleDistanceToTrader;
            MaxSZVehicleParkingTimeNUD.Value = (decimal)_data.MaxSZVehicleParkingTime;
            SZVehicleParkingTicketFineNUD.Value = (int)_data.SZVehicleParkingTicketFine;
            SZVehicleParkingFineUseKeyCB.Checked = _data.SZVehicleParkingFineUseKey == 1 ? true : false;
            DisallowUnpersistedCB.Checked = _data.DisallowUnpersisted == 1 ? true : false;
            DisableClientSellTransactionDetailsCB.Checked = _data.DisableClientSellTransactionDetails == 1 ? true : false;

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
                // TODO: Update _nodes.Last().Text based on _data
            }
        }

        #endregion

        private void MarketSytemeEnabedcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MarketSystemEnabled = MarketSytemeEnabedcheckBox.Checked == true ? 1 : 0;
            
        }
        private void ATMSytemeEnabledcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ATMSystemEnabled = ATMSytemeEnabledcheckBox.Checked == true ? 1 : 0;
            
        }
        private void MaxdepositUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxDepositMoney = (int)MaxdepositUpDown.Value;
            
        }
        private void DefaultDepositUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DefaultDepositMoney = (int)DefaultDepositUpDown.Value;
            
        }
        private void ATMPlayerTransferEnabledcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ATMPlayerTransferEnabled = ATMPlayerTransferEnabledcheckbox.Checked == true ? 1 : 0;
            
        }
        private void ATMPartyLockerEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ATMPartyLockerEnabled = ATMPartyLockerEnabledCheckBox.Checked == true ? 1 : 0;
            
        }
        private void MaxPartyDepositMoneyUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxPartyDepositMoney = (int)MaxPartyDepositMoneyUpDown.Value;
            
        }
        private void UseWholeMapForATMPlayerListCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseWholeMapForATMPlayerList = UseWholeMapForATMPlayerListCheckBox.Checked == true ? 1 : 0;
            
        }
        private void SellPricePercentNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SellPricePercent = SellPricePercentNUD.Value;
            
        }
        private void NetworkBatchSizeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NetworkBatchSize = (int)NetworkBatchSizeNUD.Value;
            
        }
        private void MaxVehicleDistanceToTraderNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxVehicleDistanceToTrader = (int)MaxVehicleDistanceToTraderNUD.Value;
            
        }
        private void MaxLargeVehicleDistanceToTraderNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxLargeVehicleDistanceToTrader = (int)MaxVehicleDistanceToTraderNUD.Value;
            
        }
        private void MaxSZVehicleParkingTimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxSZVehicleParkingTime = (int)MaxSZVehicleParkingTimeNUD.Value;
            
        }
        private void SZVehicleParkingTicketFineNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SZVehicleParkingTicketFine = (int)SZVehicleParkingTicketFineNUD.Value;
            
        }
        private void SZVehicleParkingFineUseKeyCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SZVehicleParkingFineUseKey = SZVehicleParkingFineUseKeyCB.Checked == true ? 1 : 0;
            
        }
        private void DisallowUnpersistedCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DisallowUnpersisted = DisallowUnpersistedCB.Checked == true ? 1 : 0;
            
        }
        private void DisableClientSellTransactionDetailsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DisableClientSellTransactionDetails = DisableClientSellTransactionDetailsCB.Checked == true ? 1 : 0;
            
        }
    }
}