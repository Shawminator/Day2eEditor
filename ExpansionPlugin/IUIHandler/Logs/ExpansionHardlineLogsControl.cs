using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionHardlineLogsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionLogsSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionHardlineLogsControl()
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
            _data = data as ExpansionLogsSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            SafezoneCB.Checked = _data.Safezone == 1 ? true : false;
            VehicleCarKeyCB.Checked = _data.VehicleCarKey == 1 ? true : false;
            VehicleDestroyedCB.Checked = _data.VehicleDestroyed == 1 ? true : false;
            VehicleTowingCB.Checked = _data.VehicleTowing == 1 ? true : false;
            VehicleLockPickingCB.Checked = _data.VehicleLockPicking == 1 ? true : false;
            VehicleDestroyedCB.Checked = _data.VehicleDestroyed == 1 ? true : false;
            VehicleAttachmentsCB.Checked = _data.VehicleAttachments == 1 ? true : false;
            VehicleEnterCB.Checked = _data  .VehicleEnter == 1 ? true : false;
            VehicleLeaveCB.Checked = _data.VehicleLeave == 1 ? true : false;
            VehicleDeletedCB.Checked = _data.VehicleDeleted == 1 ? true : false;
            VehicleEngineCB.Checked = _data.VehicleEngine == 1 ? true : false;
            BaseBuildingRaidingCB.Checked = _data.BaseBuildingRaiding == 1 ? true : false;
            CodeLockRaidingCB.Checked = _data.CodeLockRaiding == 1 ? true : false;
            TerritoryCB.Checked = _data.Territory == 1 ? true : false;
            KillfeedCB.Checked = _data.Killfeed == 1 ? true : false;
            PartyCB.Checked = _data.Party == 1 ? true : false;
            ChatCB.Checked = _data.Chat == 1 ? true : false;
            AdminToolsCB.Checked = _data.AdminTools == 1 ? true : false;
            SpawnSelectionCB.Checked = _data.SpawnSelection == 1 ? true : false;
            MissionAirdropCB.Checked = _data.MissionAirdrop == 1 ? true : false;
            MarketCB.Checked = _data.Market == 1 ? true : false;
            ATMCB.Checked = _data.ATM == 1 ? true : false;
            LogToScriptsCB.Checked = _data.LogToScripts == 1 ? true : false;
            LogToADMCB.Checked = _data.LogToADM == 1 ? true : false;
            AIObjectPatrolCB.Checked = _data.AIObjectPatrol == 1 ? true : false;
            AIGeneralCB.Checked = _data.AIGeneral == 1 ? true : false;
            AIObjectPatrolCB.Checked = _data.AIObjectPatrol == 1 ? true : false;
            AIPatrolCB.Checked = _data.AIPatrol == 1 ? true : false;
            HardlineCB.Checked = _data.Hardline == 1 ? true : false;
            ExplosionDamageSystemCB.Checked = _data.ExplosionDamageSystem == 1 ? true : false;
            EntityStorageCB.Checked = _data.EntityStorage == 1 ? true : false;
            GarageCB.Checked = _data.Garage == 1 ? true : false;
            VehicleCoverCB.Checked = _data.VehicleCover == 1 ? true : false;
            QuestsCB.Checked = _data.Quests == 1 ? true : false;

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

        private void LogSettingsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            CheckBox cb = sender as CheckBox;
            _data.SetIntValue(cb.Name.Substring(0, cb.Name.Length - 2), cb.Checked == true ? 1 : 0);
        }
    }
}