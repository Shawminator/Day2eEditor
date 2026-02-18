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
    public partial class AIPAtrolGeneralControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionAIPatrolSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public AIPAtrolGeneralControl()
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
            _data = data as ExpansionAIPatrolSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            AIGeneralEnabledCB.Checked = _data.Enabled == 1 ? true : false;
            if (_data.FormationScale == -1)
            {
                AIGeneralFormationScaleDefaultCB.Checked = true;
                AIGeneralFormationScaleNUD.Enabled = false;
                AIGeneralFormationScaleNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.FormationScale;
            }
            else
            {
                AIGeneralFormationScaleNUD.Value = (decimal)_data.FormationScale;
            }
            AIGeneralDespawnTimeNUD.Value = (decimal)_data.DespawnTime;
            if (_data.RespawnTime == -1)
            {
                AIGeneralRespawnTimeNUD.Visible = false;
                AIGeneralRespawnTimeNWonttRespawnCB.Checked = true;
            }
            else
            {
                AIGeneralRespawnTimeNUD.Value = (decimal)_data.RespawnTime;
            }
            AIGeneralMinDistRadiusNUD.Value = (decimal)_data.MinDistRadius;
            AIGeneralMaxDistRadiusNUD.Value = (decimal)_data.MaxDistRadius;
            AIGeneralDespawnRadiusNUD.Value = (decimal)_data.DespawnRadius;
            if (_data.AccuracyMin == -1)
            {
                AIGeneralAccuracyMinNUD.Enabled = false;
                AIGeneralAccuracyMinDefaultCB.Checked = true;
                AIGeneralAccuracyMinNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.AccuracyMin;
            }
            else
            {
                AIGeneralAccuracyMinNUD.Value = (decimal)_data.AccuracyMin;
            }
            if (_data.AccuracyMax == -1)
            {
                AIGeneralAccuracyMaxNUD.Enabled = false;
                AIGeneralAccuracyMaxDefaultCB.Checked = true;
                AIGeneralAccuracyMaxNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.AccuracyMax;
            }
            else
            {
                AIGeneralAccuracyMaxNUD.Value = (decimal)_data.AccuracyMax;
            }
            if (_data.ThreatDistanceLimit == -1)
            {
                AIGeneralThreatDistanceLimitNUD.Enabled = false;
                AIGeneralThreatDistanceLimitDefaultCB.Checked = true;
                AIGeneralThreatDistanceLimitNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.ThreatDistanceLimit;
            }
            else
            {
                AIGeneralThreatDistanceLimitNUD.Value = (decimal)_data.ThreatDistanceLimit;
            }
            if (_data.NoiseInvestigationDistanceLimit == -1)
            {
                AIGeneralNoiseInvestigationDistanceLimitNUD.Enabled = false;
                AIGeneralNoiseInvestigationDistanceLimitDewfaultCB.Checked = true;
                AIGeneralNoiseInvestigationDistanceLimitNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.NoiseInvestigationDistanceLimit;
            }
            else
            {
                AIGeneralNoiseInvestigationDistanceLimitNUD.Value = (decimal)_data.NoiseInvestigationDistanceLimit;
            }
            if (_data.MaxFlankingDistance == -1)
            {
                AIGeneralMaxFlankingDistanceNUD.Enabled = false;
                AIGeneralMaxFlankingDistanceDefaultCB.Checked = true;
                AIGeneralMaxFlankingDistanceNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.MaxFlankingDistance;
            }
            else
            {
                AIGeneralMaxFlankingDistanceNUD.Value = (decimal)_data.MaxFlankingDistance;
            }
            if (_data.EnableFlankingOutsideCombat == -1)
            {
                AIGeneralEnableFlankingOutsideCombatCB.Enabled = false;
                AIGeneralEnableFlankingOutsideCombatDefailtCB.Checked = true;
                AIGeneralEnableFlankingOutsideCombatCB.Checked = AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.EnableFlankingOutsideCombat == 1 ? true : false;
            }
            else
            {
                AIGeneralEnableFlankingOutsideCombatCB.Checked = _data.EnableFlankingOutsideCombat == 1 ? true : false;
            }
            if (_data.DamageMultiplier == -1)
            {
                AIGeneralDanageMultiplierNUD.Enabled = false;
                AIGeneralDanageMultiplierDefaultCB.Checked = true;
                AIGeneralDanageMultiplierNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.DamageMultiplier;
            }
            else
            {
                AIGeneralDanageMultiplierNUD.Value = (decimal)_data.DamageMultiplier;
            }
            if (_data.DamageReceivedMultiplier == -1)
            {
                AIGeneralDamageReceivedMultiplierNUD.Enabled = false;
                AIGeneralDamageReceivedMultiplierDefaultCB.Checked = true;
                AIGeneralDamageReceivedMultiplierNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.DamageReceivedMultiplier;
            }
            else
            {
                AIGeneralDamageReceivedMultiplierNUD.Value = (decimal)_data.DamageReceivedMultiplier;
            }
            if (_data.ShoryukenChance == -1)
            {
                AIGeneralShoryukenChanceNUD.Enabled = false;
                AIGeneralShoryukenChanceDefaultCB.Checked = true;
                AIGeneralShoryukenChanceNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.ShoryukenChance;
            }
            else
            {
                AIGeneralShoryukenChanceNUD.Value = (decimal)_data.ShoryukenChance;
            }
            if (_data.ShoryukenDamageMultiplier == -1)
            {
                AIGeneralShoryukenDamageMultiplierNUD.Enabled = false;
                AIGeneralShoryukenDamageMultiplierdefaultCB.Checked = true;
                AIGeneralShoryukenDamageMultiplierNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.ShoryukenDamageMultiplier;
            }
            else
            {
                AIGeneralShoryukenDamageMultiplierNUD.Value = (decimal)_data.ShoryukenDamageMultiplier;
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
                // TODO: Update _nodes.Last().Text based on _data
            }
        }

        #endregion

        private void AIGeneralEnabledCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Enabled = AIGeneralEnabledCB.Checked == true ? 1 : 0;

        }
        private void AIGeneralFormationScaleNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.FormationScale = AIGeneralFormationScaleNUD.Value;

        }
        private void AIGeneralFormationScaleDefaultCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (AIGeneralFormationScaleDefaultCB.Checked)
            {
                AIGeneralFormationScaleNUD.Enabled = false;
                _suppressEvents = true;
                AIGeneralFormationScaleNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.FormationScale;
                _suppressEvents = false;
                _data.FormationScale = -1;
            }
            else
            {
                AIGeneralFormationScaleNUD.Enabled = true;
                _data.FormationScale = AIGeneralFormationScaleNUD.Value;
            }

        }
        private void AIGeneralDespawnTimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DespawnTime = AIGeneralDespawnTimeNUD.Value;

        }
        private void AIGeneralRespawnTimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.RespawnTime = AIGeneralRespawnTimeNUD.Value;

        }
        private void AIGeneralRespawnTimeNWonttRespawnCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (AIGeneralRespawnTimeNWonttRespawnCB.Checked)
            {
                AIGeneralRespawnTimeNUD.Visible = false;
                _data.RespawnTime = -1;
            }
            else
            {
                AIGeneralRespawnTimeNUD.Visible = true;
                _data.RespawnTime = AIGeneralRespawnTimeNUD.Value;
            }

        }
        private void AIGeneralMinDistRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MinDistRadius = AIGeneralMinDistRadiusNUD.Value;

        }
        private void AIGeneralMaxDistRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxDistRadius = AIGeneralMaxDistRadiusNUD.Value;

        }
        private void AIGeneralDespawnRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DespawnRadius = AIGeneralDespawnRadiusNUD.Value;

        }
        private void AIGeneralAccuracyMinNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AccuracyMin = AIGeneralAccuracyMinNUD.Value;

        }
        private void AIGeneralAccuracyMinDefaultCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (AIGeneralAccuracyMinDefaultCB.Checked)
            {
                AIGeneralAccuracyMinNUD.Enabled = false;
                _suppressEvents = true;
                AIGeneralAccuracyMinNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.AccuracyMin;
                _suppressEvents = false;
                _data.AccuracyMin = -1;
            }
            else
            {
                AIGeneralAccuracyMinNUD.Enabled = true;
                _data.AccuracyMin = AIGeneralAccuracyMinNUD.Value;
            }

        }
        private void AIGenralAccuracyMaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AccuracyMax = AIGeneralAccuracyMaxNUD.Value;

        }
        private void AIGeneralAccuracyMaxDefaultCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (AIGeneralAccuracyMaxDefaultCB.Checked)
            {
                AIGeneralAccuracyMaxNUD.Enabled = false;
                _suppressEvents = true;
                AIGeneralAccuracyMaxNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.AccuracyMax;
                _suppressEvents = false;
                _data.AccuracyMax = -1;
            }
            else
            {
                AIGeneralAccuracyMaxNUD.Enabled = true;
                _data.AccuracyMax = AIGeneralAccuracyMaxNUD.Value;
            }

        }
        private void AIGeneralThreatDistanceLimitNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ThreatDistanceLimit = AIGeneralThreatDistanceLimitNUD.Value;

        }
        private void AIGeneralThreatDistanceLimitDefaultCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (AIGeneralThreatDistanceLimitDefaultCB.Checked)
            {
                AIGeneralThreatDistanceLimitNUD.Enabled = false;
                _suppressEvents = true;
                AIGeneralThreatDistanceLimitNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.ThreatDistanceLimit;
                _suppressEvents = false;
                _data.ThreatDistanceLimit = -1;
            }
            else
            {
                AIGeneralThreatDistanceLimitNUD.Enabled = true;
                _data.ThreatDistanceLimit = AIGeneralThreatDistanceLimitNUD.Value;
            }

        }
        private void NoiseInvestigationDistanceLimitNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NoiseInvestigationDistanceLimit = AIGeneralNoiseInvestigationDistanceLimitNUD.Value;

        }
        private void AIGeneralNoiseInvestigationDistanceLimitDewfaultCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (AIGeneralNoiseInvestigationDistanceLimitDewfaultCB.Checked)
            {
                AIGeneralNoiseInvestigationDistanceLimitNUD.Enabled = false;
                _suppressEvents = true;
                AIGeneralNoiseInvestigationDistanceLimitNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.NoiseInvestigationDistanceLimit;
                _suppressEvents = false;
                _data.NoiseInvestigationDistanceLimit = -1;
            }
            else
            {
                AIGeneralNoiseInvestigationDistanceLimitNUD.Enabled = true;
                _data.NoiseInvestigationDistanceLimit = AIGeneralNoiseInvestigationDistanceLimitNUD.Value;
            }

        }
        private void AIGeneralMaxFlankingDistanceNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxFlankingDistance = AIGeneralMaxFlankingDistanceNUD.Value;

        }
        private void AIGeneralMaxFlankingDistanceDefaultCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (AIGeneralMaxFlankingDistanceDefaultCB.Checked)
            {
                AIGeneralMaxFlankingDistanceNUD.Enabled = false;
                _suppressEvents = true;
                AIGeneralMaxFlankingDistanceNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.MaxFlankingDistance;
                _suppressEvents = false;
                _data.MaxFlankingDistance = -1;
            }
            else
            {
                AIGeneralMaxFlankingDistanceNUD.Enabled = true;
                _data.MaxFlankingDistance = AIGeneralMaxFlankingDistanceNUD.Value;
            }

        }
        private void AIGeneralEnableFlankingOutsideCombatCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableFlankingOutsideCombat = AIGeneralEnableFlankingOutsideCombatCB.Checked == true ? 1 : 0;

        }
        private void AIGeneralEnableFlankingOutsideCombatDefailtCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (AIGeneralEnableFlankingOutsideCombatDefailtCB.Checked)
            {
                AIGeneralEnableFlankingOutsideCombatCB.Enabled = false;
                _suppressEvents = true;
                AIGeneralEnableFlankingOutsideCombatCB.Checked = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.EnableFlankingOutsideCombat == 1 ? true : false;
                _suppressEvents = false;
                _data.EnableFlankingOutsideCombat = -1;
            }
            else
            {
                AIGeneralEnableFlankingOutsideCombatCB.Enabled = true;
                _data.EnableFlankingOutsideCombat = AIGeneralEnableFlankingOutsideCombatCB.Checked == true ? 1 : 0;
            }

        }
        private void AIGeneralDanageMultiplierNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DamageMultiplier = AIGeneralDanageMultiplierNUD.Value;

        }
        private void AIGeneralDanageMultiplierDefaultCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (AIGeneralDanageMultiplierDefaultCB.Checked)
            {
                AIGeneralDanageMultiplierNUD.Enabled = false;
                _suppressEvents = true;
                AIGeneralDanageMultiplierNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.DamageMultiplier;
                _suppressEvents = false;
                _data.DamageMultiplier = -1;
            }
            else
            {
                AIGeneralDanageMultiplierNUD.Enabled = true;
                _data.DamageMultiplier = AIGeneralDanageMultiplierNUD.Value;
            }

        }
        private void AIGeneralDamageReceivedMultiplierNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DamageReceivedMultiplier = AIGeneralDamageReceivedMultiplierNUD.Value;

        }
        private void AIGeneralDamageReceivedMultiplierDefaultCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (AIGeneralDamageReceivedMultiplierDefaultCB.Checked)
            {
                AIGeneralDamageReceivedMultiplierNUD.Enabled = false;
                _suppressEvents = true;
                AIGeneralDamageReceivedMultiplierNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.DamageReceivedMultiplier;
                _suppressEvents = false;
                _data.DamageReceivedMultiplier = -1;
            }
            else
            {
                AIGeneralDamageReceivedMultiplierNUD.Enabled = true;
                _data.DamageReceivedMultiplier = AIGeneralDamageReceivedMultiplierNUD.Value;
            }

        }
        private void AIGeneralShoryukenChanceNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ShoryukenChance = AIGeneralShoryukenChanceNUD.Value;
        }
        private void AIGeneralShoryukenChanceDefaultCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (AIGeneralShoryukenChanceDefaultCB.Checked)
            {
                AIGeneralShoryukenChanceNUD.Enabled = false;
                _suppressEvents = true;
                AIGeneralShoryukenChanceNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.ShoryukenChance;
                _suppressEvents = false;
                _data.ShoryukenChance = -1;
            }
            else
            {
                AIGeneralShoryukenChanceNUD.Enabled = true;
                _data.ShoryukenChance = AIGeneralShoryukenChanceNUD.Value;
            }
        }
        private void AIGeneralShoryukenDamageMultiplierNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ShoryukenDamageMultiplier = AIGeneralShoryukenDamageMultiplierNUD.Value;
        }
        private void AIGeneralShoryukenDamageMultiplierdefaultCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (AIGeneralShoryukenDamageMultiplierdefaultCB.Checked)
            {
                AIGeneralShoryukenDamageMultiplierNUD.Enabled = false;
                _suppressEvents = true;
                AIGeneralShoryukenDamageMultiplierNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.ShoryukenDamageMultiplier;
                _suppressEvents = false;
                _data.ShoryukenDamageMultiplier = -1;
            }
            else
            {
                AIGeneralShoryukenDamageMultiplierNUD.Enabled = true;
                _data.ShoryukenDamageMultiplier = AIGeneralShoryukenDamageMultiplierNUD.Value;
            }
        }
    }
}