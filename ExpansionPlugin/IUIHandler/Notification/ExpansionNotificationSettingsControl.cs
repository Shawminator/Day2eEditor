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
    public partial class ExpansionNotificationSettingsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionNotificationSettings _data;
        private ExpansionNotificationSettings _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionNotificationSettingsControl()
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
            _data = data as ExpansionNotificationSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = _data.Clone();  // Store original data for reset

            _suppressEvents = true;

            JoinMessageTypeCB.DataSource = Enum.GetValues(typeof(ExpansionAnnouncementType));
            LeftMessageTypeCB.DataSource = Enum.GetValues(typeof(ExpansionAnnouncementType));
            KillFeedMessageTypeCB.DataSource = Enum.GetValues(typeof(ExpansionAnnouncementType));

            EnableNotificationCB.Checked = (bool)_data.EnableNotification;
            ShowPlayerJoinServerCB.Checked = (bool)_data.ShowPlayerJoinServer;
            JoinMessageTypeCB.SelectedItem = (ExpansionAnnouncementType)_data.JoinMessageType;
            ShowPlayerLeftServerCB.Checked = (bool)_data.ShowPlayerLeftServer;
            LeftMessageTypeCB.SelectedItem = (ExpansionAnnouncementType)_data.LeftMessageType;

            ShowAirdropStartedCB.Checked = (bool)_data.ShowAirdropStarted;
            ShowAirdropClosingOnCB.Checked = (bool)_data.ShowAirdropClosingOn;
            ShowAirdropDroppedCB.Checked = (bool)_data.ShowAirdropDropped;
            ShowAirdropEndedCB.Checked = (bool)_data.ShowAirdropEnded;
            ShowPlayerAirdropStartedCB.Checked = (bool)_data.ShowPlayerAirdropStarted;
            ShowPlayerAirdropClosingOnCB.Checked = (bool)_data.ShowPlayerAirdropClosingOn;
            ShowPlayerAirdropDroppedCB.Checked = (bool)_data.ShowPlayerAirdropDropped;

            ShowTerritoryNotificationsCB.Checked = (bool)_data.ShowTerritoryNotifications;

            EnableKillFeedCB.Checked = (bool)_data.EnableKillFeed;
            KillFeedMessageTypeCB.SelectedItem = (ExpansionAnnouncementType)_data.KillFeedMessageType;
            KillFeedFallCB.Checked = (bool)_data.KillFeedFall;
 
            KillFeedCarHitDriverCB.Checked = (bool)_data.KillFeedCarHitDriver;
            KillFeedCarHitNoDriverCB.Checked = (bool)_data.KillFeedCarHitNoDriver;
            KillFeedCarCrashCB.Checked = (bool)_data.KillFeedCarCrash;
            KillFeedCarCrashCrewCB.Checked = (bool)_data.KillFeedCarCrashCrew;

            KillFeedHeliHitDriverCB.Checked = (bool)_data.KillFeedHeliHitDriver;
            KillFeedHeliHitNoDriverCB.Checked = (bool)_data.KillFeedHeliHitNoDriver;
            KillFeedHeliCrashCB.Checked = (bool)_data.KillFeedHeliCrash;
            KillFeedHeliCrashCrewCB.Checked = (bool)_data.KillFeedHeliCrashCrew;
 
            KillFeedBoatHitDriverCB.Checked = (bool)_data.KillFeedBoatHitDriver;
            KillFeedBoatHitNoDriverCB.Checked = (bool)_data.KillFeedBoatHitNoDriver;
            KillFeedBoatCrashCB.Checked = (bool)_data.KillFeedBoatCrash;
            KillFeedBoatCrashCrewCB.Checked = (bool)_data.KillFeedBoatCrashCrew;

            KillFeedBarbedWireCB.Checked = (bool)_data.KillFeedBarbedWire;
            KillFeedFireCB.Checked = (bool)_data.KillFeedFire;
            KillFeedWeaponExplosionCB.Checked = (bool)_data.KillFeedWeaponExplosion;
            KillFeedDehydrationCB.Checked = (bool)_data.KillFeedDehydration;
            KillFeedStarvationCB.Checked = (bool)_data.KillFeedStarvation;
            KillFeedBleedingCB.Checked = (bool)_data.KillFeedBleeding;
            KillFeedStatusEffectsCB.Checked = (bool)_data.KillFeedStatusEffects;
            KillFeedSuicideCB.Checked = (bool)_data.KillFeedSuicide;
            KillFeedWeaponCB.Checked = (bool)_data.KillFeedWeapon;
            KillFeedMeleeWeaponCB.Checked = (bool)_data.KillFeedMeleeWeapon;
            KillFeedBarehandsCB.Checked = (bool)_data.KillFeedBarehands;
            KillFeedInfectedCB.Checked = (bool)_data.KillFeedInfected;
            KillFeedAnimalCB.Checked = (bool)_data.KillFeedAnimal;
            KillFeedAICB.Checked = (bool)_data.KillFeedAI;
            KillFeedKilledUnknownCB.Checked = (bool)_data.KillFeedKilledUnknown;
            KillFeedDiedUnknownCB.Checked = (bool)_data.KillFeedDiedUnknown;

            EnableKillFeedDiscordMsgCB.Checked = (bool)_data.EnableKillFeedDiscordMsg;

            _suppressEvents = false;
        }

        /// <summary>
        /// Applies changes to the data and updates the original snapshot
        /// </summary>
        public void ApplyChanges()
        {
            _originalData = _data.Clone();
        }

        /// <summary>
        /// Resets control fields to the original data
        /// </summary>
        public void Reset()
        {
            // TODO: Reset control fields to _originalData
        }

        /// <summary>
        /// Checks if there are changes and updates the parent file's dirty state
        /// </summary>
        public void HasChanges()
        {
            var parentObj = _nodes.Last().FindParentOfType(_parentType);
            if (parentObj != null)
            {
                dynamic parent = parentObj;
                parent.isDirty = !_data.Equals(_originalData);
            }
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

        private void NotificationCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            CheckBox cb = sender as CheckBox;
            _data.SetBoolValue(cb.Name.Substring(0, cb.Name.Length - 2), cb.Checked);
            HasChanges();
        }
        private void JoinMessageTypeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            ExpansionAnnouncementType cacl = (ExpansionAnnouncementType)JoinMessageTypeCB.SelectedItem;
            _data.JoinMessageType = cacl;
            HasChanges();
        }
        private void LeftMessageTypeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            ExpansionAnnouncementType cacl = (ExpansionAnnouncementType)LeftMessageTypeCB.SelectedItem;
            _data.LeftMessageType = cacl;
            HasChanges();
        }
        private void KillFeedMessageTypeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            ExpansionAnnouncementType cacl = (ExpansionAnnouncementType)KillFeedMessageTypeCB.SelectedItem;
            _data.KillFeedMessageType = cacl;
            HasChanges();
        }
    }
}