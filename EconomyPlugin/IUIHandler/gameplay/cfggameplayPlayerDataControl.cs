using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class cfggameplayPlayerDataControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private Playerdata _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfggameplayPlayerDataControl()
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
            _data = data as Playerdata ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            disablePersonalLightCB.Checked = _data.disablePersonalLight;
            sprintStaminaModifierErcNUD.Value = _data.StaminaData.sprintStaminaModifierErc;
            sprintStaminaModifierCroNUD.Value = _data.StaminaData.sprintStaminaModifierCro;
            staminaWeightLimitThresholdNUD.Value = _data.StaminaData.staminaWeightLimitThreshold;
            staminaMaxNUD.Value = _data.StaminaData.staminaMax;
            staminaKgToStaminaPercentPenaltyNUD.Value = _data.StaminaData.staminaKgToStaminaPercentPenalty;
            staminaMinCapNUD.Value = _data.StaminaData.staminaMinCap;
            sprintSwimmingStaminaModifierNUD.Value = _data.StaminaData.sprintSwimmingStaminaModifier;
            sprintLadderStaminaModifierNUD.Value = _data.StaminaData.sprintLadderStaminaModifier;
            meleeStaminaModifierNUD.Value = _data.StaminaData.meleeStaminaModifier;
            obstacleTraversalStaminaModifierNUD.Value = _data.StaminaData.obstacleTraversalStaminaModifier;
            holdBreathStaminaModifierNUD.Value = _data.StaminaData.holdBreathStaminaModifier;

            shockRefillSpeedConsciousNUD.Value = _data.ShockHandlingData.shockRefillSpeedConscious;
            shockRefillSpeedUnconsciousNUD.Value = _data.ShockHandlingData.shockRefillSpeedUnconscious;
            allowRefillSpeedModifierCB.Checked = _data.ShockHandlingData.allowRefillSpeedModifier;

            timeToStrafeJogNUD.Value = _data.MovementData.timeToStrafeJog;
            rotationSpeedJogNUD.Value = _data.MovementData.rotationSpeedJog;
            timeToSprintNUD.Value = _data.MovementData.timeToSprint;
            timeToStrafeSprintNUD.Value = _data.MovementData.timeToStrafeSprint;
            rotationSpeedSprintNUD.Value = _data.MovementData.rotationSpeedSprint;
            allowStaminaAffectInertiaCB.Checked = _data.MovementData.allowStaminaAffectInertia;

            staminaDepletionSpeedNUD.Value = _data.DrowningData.staminaDepletionSpeed;
            healthDepletionSpeedNUD.Value = _data.DrowningData.healthDepletionSpeed;
            shockDepletionSpeedNUD.Value = _data.DrowningData.shockDepletionSpeed;

            staticModeCB.SelectedIndex = _data.WeaponObstructionData.staticMode;
            dynamicModeCB.SelectedIndex = _data.WeaponObstructionData.dynamicMode;

            _suppressEvents = false;
        }
        private void disablePersonalLightCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.disablePersonalLight = disablePersonalLightCB.Checked;
        }
        private void sprintStaminaModifierErcNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.StaminaData.sprintStaminaModifierErc = Math.Round(sprintStaminaModifierErcNUD.Value, 2);
        }
        private void sprintStaminaModifierCroNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.StaminaData.sprintStaminaModifierCro = Math.Round(sprintStaminaModifierCroNUD.Value, 2);
        }
        private void staminaWeightLimitThresholdNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.StaminaData.staminaWeightLimitThreshold = Math.Round(staminaWeightLimitThresholdNUD.Value, 2);
        }
        private void staminaMaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.StaminaData.staminaMax = Math.Round(staminaMaxNUD.Value, 2);
        }
        private void staminaKgToStaminaPercentPenaltyNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.StaminaData.staminaKgToStaminaPercentPenalty = Math.Round(staminaKgToStaminaPercentPenaltyNUD.Value, 2);
        }
        private void staminaMinCapNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.StaminaData.staminaMinCap = Math.Round(staminaMinCapNUD.Value, 2);
        }
        private void sprintSwimmingStaminaModifierNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.StaminaData.sprintSwimmingStaminaModifier = Math.Round(sprintSwimmingStaminaModifierNUD.Value, 2);
        }
        private void sprintLadderStaminaModifierNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.StaminaData.sprintLadderStaminaModifier = Math.Round(sprintLadderStaminaModifierNUD.Value, 2);
        }
        private void meleeStaminaModifierNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.StaminaData.meleeStaminaModifier = Math.Round(meleeStaminaModifierNUD.Value, 2);
        }
        private void obstacleTraversalStaminaModifierNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.StaminaData.obstacleTraversalStaminaModifier = Math.Round(obstacleTraversalStaminaModifierNUD.Value, 2);
        }
        private void holdBreathStaminaModifierNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.StaminaData.holdBreathStaminaModifier = Math.Round(holdBreathStaminaModifierNUD.Value, 2);
        }
        private void shockRefillSpeedConsciousNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ShockHandlingData.shockRefillSpeedConscious = Math.Round(shockRefillSpeedConsciousNUD.Value, 2);
        }
        private void shockRefillSpeedUnconsciousNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ShockHandlingData.shockRefillSpeedUnconscious = Math.Round(shockRefillSpeedUnconsciousNUD.Value, 2);
        }
        private void allowRefillSpeedModifierCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ShockHandlingData.allowRefillSpeedModifier = allowRefillSpeedModifierCB.Checked;
        }
        private void staminaDepletionSpeedNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DrowningData.staminaDepletionSpeed = Math.Round(staminaDepletionSpeedNUD.Value, 2);
        }
        private void healthDepletionSpeedNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DrowningData.healthDepletionSpeed = Math.Round(healthDepletionSpeedNUD.Value, 2);
        }
        private void shockDepletionSpeedNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DrowningData.shockDepletionSpeed = Math.Round(shockDepletionSpeedNUD.Value, 2);
        }
        private void timeToStrafeJogNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MovementData.timeToStrafeJog = Math.Round(timeToStrafeJogNUD.Value, 2);
        }
        private void rotationSpeedJogNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MovementData.rotationSpeedJog = Math.Round(rotationSpeedJogNUD.Value, 2);
        }
        private void timeToSprintNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MovementData.timeToSprint = Math.Round(timeToSprintNUD.Value, 2);
        }
        private void timeToStrafeSprintNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MovementData.timeToStrafeSprint = Math.Round(timeToStrafeSprintNUD.Value, 2);
        }
        private void rotationSpeedSprintNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MovementData.rotationSpeedSprint = Math.Round(rotationSpeedSprintNUD.Value, 2);
        }
        private void allowStaminaAffectInertiaCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MovementData.allowStaminaAffectInertia = allowStaminaAffectInertiaCB.Checked;
        }
        private void staticModeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.WeaponObstructionData.staticMode = staticModeCB.SelectedIndex;
        }
        private void dynamicModeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.WeaponObstructionData.dynamicMode = dynamicModeCB.SelectedIndex;
        }
    }
}