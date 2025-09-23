using Day2eEditor;
using ExpansionPlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Windows.Forms;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class AISettingsConfigControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionAISettings _data;
        private ExpansionAISettings _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public AISettingsConfigControl()
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
            _data = data as ExpansionAISettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            AccuracyMinNUD.Value = _data.AccuracyMin;
            AccuracyMaxNUD.Value = _data.AccuracyMax;
            ThreatDistanceLimitNUD.Value = _data.ThreatDistanceLimit;
            NoiseInvestigationDistanceLimitNUD.Value = _data.NoiseInvestigationDistanceLimit;
            DamageMultiplierNUD.Value = _data.DamageMultiplier;
            FormationScaleNUD.Value = _data.FormationScale;
            SniperProneDistanceThresholdNUD.Value = _data.SniperProneDistanceThreshold;
            DamageReceivedMultiplierNUD.Value = _data.DamageReceivedMultiplier;
            VaultingCB.Checked = _data.Vaulting == 1 ? true : false;
            MannersCB.Checked = _data.Manners == 1 ? true : false;
            CanRecruitGuardsCB.Checked = _data.CanRecruitGuards == 1 ? true : false;
            CanRecruitFriendlyCB.Checked = _data.CanRecruitFriendly == 1 ? true : false;
            LogAIHitByCB.Checked = _data.LogAIHitBy == 1 ? true : false;
            LogAIKilledCB.Checked = _data.LogAIKilled == 1 ? true : false;

            EnableZombieVehicleAttackHandlerCB.Checked = _data.EnableZombieVehicleAttackHandler == 1 ? true : false;
            EnableZombieVehicleAttackPhysicsCB.Checked = _data.EnableZombieVehicleAttackPhysics == 1 ? true : false;

            _suppressEvents = false;
        }

        /// <summary>
        /// Applies changes to the data and updates the original snapshot
        /// </summary>
        public void ApplyChanges()
        {
            _originalData = CloneData(_data);
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
        /// Clones the data for reset purposes
        /// </summary>
        private ExpansionAISettings CloneData(ExpansionAISettings data)
        {
            // TODO: Implement actual cloning logic
            return new ExpansionAISettings
            {
                // Copy properties here
            };
        }

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
    }
}