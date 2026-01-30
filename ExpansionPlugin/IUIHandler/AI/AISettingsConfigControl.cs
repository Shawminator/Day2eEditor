using Day2eEditor;
using System.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class AISettingsConfigControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionAISettings _data;
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

            _suppressEvents = true;

            AccuracyMinNUD.Value = (decimal)_data.AccuracyMin;
            AccuracyMaxNUD.Value = (decimal)_data.AccuracyMax;
            ThreatDistanceLimitNUD.Value = (decimal)_data.ThreatDistanceLimit;
            NoiseInvestigationDistanceLimitNUD.Value = (decimal)_data.NoiseInvestigationDistanceLimit;
            MaxFlankingDistanceNUD.Value = (decimal)_data.MaxFlankingDistance;
            EnableFlankingOutsideCombatCB.Checked = _data.EnableFlankingOutsideCombat == 1 ? true : false;
            DamageMultiplierNUD.Value = (decimal)_data.DamageMultiplier;
            FormationScaleNUD.Value = (decimal)_data.FormationScale;
            SniperProneDistanceThresholdNUD.Value = (decimal)_data.SniperProneDistanceThreshold;
            DamageReceivedMultiplierNUD.Value = (decimal)_data.DamageReceivedMultiplier;
            AggressionTimeoutNUD.Value = (decimal)_data.AggressionTimeout;
            GuardAggressionTimeoutNUD.Value = (decimal)_data.GuardAggressionTimeout;
            MemeLevelNUD.Value = (int)_data.MemeLevel;
            VaultingCB.Checked = _data.Vaulting == 1 ? true : false;
            MannersCB.Checked = _data.Manners == 1 ? true : false;
            CanRecruitGuardsCB.Checked = _data.CanRecruitGuards == 1 ? true : false;
            CanRecruitFriendlyCB.Checked = _data.CanRecruitFriendly == 1 ? true : false;
            MaxRecruitableAINUD.Value = (decimal)_data.MaxRecruitableAI;
            LogAIHitByCB.Checked = _data.LogAIHitBy == 1 ? true : false;
            LogAIKilledCB.Checked = _data.LogAIKilled == 1 ? true : false;

            EnableZombieVehicleAttackHandlerCB.Checked = _data.EnableZombieVehicleAttackHandler == 1 ? true : false;
            EnableZombieVehicleAttackPhysicsCB.Checked = _data.EnableZombieVehicleAttackPhysics == 1 ? true : false;

            OverrideClientWeaponFiringCB.Checked = _data.OverrideClientWeaponFiring == 1 ? true : false;
            RecreateWeaponNetworkRepresentationCB.Checked = _data.RecreateWeaponNetworkRepresentation == 1 ? true : false;

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

        private void AccuracyMinNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AccuracyMin = AccuracyMinNUD.Value;
            
        }
        private void AccuracyMaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AccuracyMax = AccuracyMaxNUD.Value;
            
        }
        private void ThreatDistanceLimitNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ThreatDistanceLimit = ThreatDistanceLimitNUD.Value;
            
        }
        private void NoiseInvestigationDistanceLimitNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NoiseInvestigationDistanceLimit = NoiseInvestigationDistanceLimitNUD.Value;
            
        }
        private void DamageMultiplierNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DamageMultiplier = DamageMultiplierNUD.Value;
            
        }
        private void FormationScaleNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.FormationScale = FormationScaleNUD.Value;
            
        }
        private void SniperProneDistanceThresholdNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SniperProneDistanceThreshold = SniperProneDistanceThresholdNUD.Value;
            
        }
        private void DamageReceivedMultiplierNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DamageReceivedMultiplier = DamageReceivedMultiplierNUD.Value;
            
        }
        private void MemeLevelNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MemeLevel = (int)MemeLevelNUD.Value;
            
        }
        private void AggressionTimeoutNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AggressionTimeout = AggressionTimeoutNUD.Value;
            
        }
        private void GuardAggressionTimeoutNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.GuardAggressionTimeout = GuardAggressionTimeoutNUD.Value;
            
        }
        private void VaultingCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Vaulting = VaultingCB.Checked == true ? 1 : 0;
            
        }
        private void MannersCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Manners = MannersCB.Checked == true ? 1 : 0;
            
        }
        private void CanRecruitFriendlyCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CanRecruitFriendly = CanRecruitFriendlyCB.Checked == true ? 1 : 0;
            
        }
        private void CanRecruitGuardsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CanRecruitGuards = CanRecruitGuardsCB.Checked == true ? 1 : 0;
            

        }
        private void LogAIHitByCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.LogAIHitBy = LogAIHitByCB.Checked == true ? 1 : 0;
            
        }
        private void LogAIKilledCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.LogAIKilled = LogAIKilledCB.Checked == true ? 1 : 0;
            
        }
        private void EnableZombieVehicleAttackHandlerCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableZombieVehicleAttackHandler = EnableZombieVehicleAttackHandlerCB.Checked == true ? 1 : 0;
            
        }
        private void EnableZombieVehicleAttackPhysicsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableZombieVehicleAttackPhysics = EnableZombieVehicleAttackPhysicsCB.Checked == true ? 1 : 0;
            
        }

        private void MaxRecruitableAINUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxRecruitableAI = (int)MaxRecruitableAINUD.Value;
            
        }
        private void OverrideClientWeaponFiringCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.OverrideClientWeaponFiring = OverrideClientWeaponFiringCB.Checked == true ? 1 : 0;
            
        }
        private void RecreateWeaponNetworkRepresentationCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.RecreateWeaponNetworkRepresentation = RecreateWeaponNetworkRepresentationCB.Checked == true ? 1 : 0;
            
        }

        private void MaxFlankingDistanceNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxFlankingDistance = MaxFlankingDistanceNUD.Value;
            
        }

        private void EnableFlankingOutsideCombatCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableFlankingOutsideCombat = EnableFlankingOutsideCombatCB.Checked == true ? 1 : 0;
            
        }
    }
}