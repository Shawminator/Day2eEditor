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
    public partial class AIPatrolControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionAIPatrol _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public AIPatrolControl()
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
            _data = data as ExpansionAIPatrol ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;
            var parentObj = _nodes.Last().FindParentOfType(_parentType);
            if (parentObj != null)
            {
                dynamic parent = parentObj;
                BindingList<string> LoadoutNameList = new BindingList<string>
                {
                    ""
                };
                foreach (AILoadouts lo in AppServices.GetRequired<ExpansionManager>().ExpansionLoadoutConfig.Items)
                {
                    LoadoutNameList.Add(Path.GetFileNameWithoutExtension(lo.FileName));
                }
                BindingList<string> LootDropOnDeathNameList = new BindingList<string>
                {
                    ""
                };
                foreach (AILootDrops AILootDrops in AppServices.GetRequired<ExpansionManager>().ExpansionLootDropConfig.Items)
                {
                    LootDropOnDeathNameList.Add(Path.GetFileNameWithoutExtension(AILootDrops.FileName));
                }
                BindingList<string> Factions = new BindingList<string>(File.ReadAllLines("Data\\ExpansionFactions.txt").ToList());
                Factions.Insert(0, "");
                StaticPatrolFactionCB.DataSource = Factions;


                StaticPatrolLoadoutsCB.DataSource = new BindingList<string>(LoadoutNameList);
                StaticPatrolLootDropOnDeathCB.DataSource = new BindingList<string>(LootDropOnDeathNameList);
                StaticPatrolLoadBalancingCategoryCB.DataSource = new BindingSource(parent.Data._LoadBalancingCategories, null);
            }
            StaticPatrolNameTB.Text = _data.Name;
            StaticPatrolPersistCB.Checked = _data.Persist == 1 ? true : false;
            StaticPatrolFactionCB.SelectedIndex = StaticPatrolFactionCB.FindStringExact(_data.Faction);
            StaticPatrolFormationCB.SelectedIndex = StaticPatrolFormationCB.FindStringExact(_data.Formation);
            StaticPatrolFormationLoosenessNUD.Value = (decimal)_data.FormationLooseness;
            StaticPatrolLoadoutsCB.SelectedIndex = StaticPatrolLoadoutsCB.FindStringExact(_data.Loadout);
            StaticPatrolNumberOfAINUD.Value = (int)_data.NumberOfAI;
            StaticPatrolNumberOfAIMaxNUD.Value = (int)_data.NumberOfAIMax;
            StaticPatrolBehaviorCB.SelectedIndex = StaticPatrolBehaviorCB.FindStringExact(_data.Behaviour);
            StaticPatrolSpeedCB.SelectedIndex = StaticPatrolSpeedCB.FindStringExact(_data.Speed);
            StaticPatrolUnderThreatSpeedCB.SelectedIndex = StaticPatrolUnderThreatSpeedCB.FindStringExact(_data.UnderThreatSpeed);
            StaticPatrolDefaultStanceCB.SelectedIndex = StaticPatrolDefaultStanceCB.FindStringExact(_data.DefaultStance);
            StaticPatrolDefaultLookAngleNUD.Value = (decimal)_data.DefaultLookAngle;
            StaticPatrolCanBeLotedCB.Checked = _data.CanBeLooted == 1 ? true : false;
            StaticPatrolLootDropOnDeathCB.SelectedIndex = StaticPatrolLootDropOnDeathCB.FindStringExact(_data.LootDropOnDeath);
            StaticPatrolSniperProneDistanceThresholdNUD.Value = (decimal)_data.SniperProneDistanceThreshold;
            StaticPatrolHeadshotResistanceNUD.Value = (decimal)_data.HeadshotResistance;
            StaticPatrolCanBeTriggeredByAICB.Checked = _data.CanBeTriggeredByAI == 1 ? true : false;
            StaticPatrolMinSpreadRadiusNUD.Value = (decimal)_data.MinSpreadRadius;
            StaticPatrolMaxSpreadRadiusNUD.Value = (decimal)_data.MaxSpreadRadius;
            StaticPatrolChanceCB.Value = (decimal)_data.Chance;
            StaticPatrolLoadBalancingCategoryCB.SelectedIndex = StaticPatrolLoadBalancingCategoryCB.FindStringExact(_data.LoadBalancingCategory);
            textBox6.Text = _data.ObjectClassName;
            StaticPatrolWaypointInterpolationCB.SelectedIndex = StaticPatrolWaypointInterpolationCB.FindStringExact(_data.WaypointInterpolation);
            StaticPatrolUseRandomWaypointAsStartPointCB.Checked = _data.UseRandomWaypointAsStartPoint == 1 ? true : false;
            StaticPatrolCanSpawnInContaminatedAreaCB.Checked = _data.CanSpawnInContaminatedArea == 1 ? true : false;
            if (_data.FormationScale == -1)
            {
                StaticPatrolFormationScaleNUD.Enabled = false;
                StaticPatrolFormationScaleGeneralCB.Checked = true;
                if ((decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.FormationScale == -1)
                {
                    StaticPatrolFormationScaleNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.FormationScale;
                }
                else
                {
                    StaticPatrolFormationScaleNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.FormationScale;
                }
            }
            else
            {
                StaticPatrolFormationScaleGeneralCB.Checked = false;
                StaticPatrolFormationScaleNUD.Enabled = true;
                StaticPatrolFormationScaleNUD.Value = (decimal)_data.FormationScale;
            }

            if (_data.AccuracyMin == -1)
            {
                StaticPatrolAccuracyMinNUD.Enabled = false;
                StaticPatrolAccuracyMinGeneralCB.Checked = true;
                if ((decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.AccuracyMin == -1)
                {
                    StaticPatrolAccuracyMinNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.AccuracyMin;
                }
                else
                {
                    StaticPatrolAccuracyMinNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.AccuracyMin;
                }
            }
            else
            {
                StaticPatrolAccuracyMinGeneralCB.Checked = false;
                StaticPatrolAccuracyMinNUD.Enabled = true;
                StaticPatrolAccuracyMinNUD.Value = (decimal)_data.AccuracyMin;

            }
            if (_data.AccuracyMax == -1)
            {
                StaticPatrolAccuracyMaxNUD.Enabled = false;
                StaticPatrolAccuracyMaxGenerralCB.Checked = true;
                if ((decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.AccuracyMax == -1)
                {
                    StaticPatrolAccuracyMaxNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.AccuracyMax;
                }
                else
                {
                    StaticPatrolAccuracyMaxNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.AccuracyMax;
                }
            }
            else
            {
                StaticPatrolAccuracyMaxGenerralCB.Checked = false;
                StaticPatrolAccuracyMaxNUD.Enabled = true;
                StaticPatrolAccuracyMaxNUD.Value = (decimal)_data.AccuracyMax;
            }
            if (_data.ThreatDistanceLimit == -1)
            {
                StaticPatrolThreatDistanceLimitNUD.Enabled = false;
                StaticPatrolThreatDistanceLimitGeneralCB.Checked = true;
                if ((decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.ThreatDistanceLimit == -1)
                {
                    StaticPatrolThreatDistanceLimitNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.ThreatDistanceLimit;
                }
                else
                {
                    StaticPatrolThreatDistanceLimitNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.ThreatDistanceLimit;
                }
            }
            else
            {
                StaticPatrolThreatDistanceLimitGeneralCB.Checked = false;
                StaticPatrolThreatDistanceLimitNUD.Enabled = true;
                StaticPatrolThreatDistanceLimitNUD.Value = (decimal)_data.ThreatDistanceLimit;
            }
            if (_data.NoiseInvestigationDistanceLimit == -1)
            {
                StaticPatrolNoiseInvestigationDistanceLimitNUD.Enabled = false;
                StaticPatrolNoiseInvestigationDistanceLimitGeneralCB.Checked = true;
                if ((decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.NoiseInvestigationDistanceLimit == -1)
                {
                    StaticPatrolNoiseInvestigationDistanceLimitNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.NoiseInvestigationDistanceLimit;
                }
                else
                {
                    StaticPatrolNoiseInvestigationDistanceLimitNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.NoiseInvestigationDistanceLimit;
                }
            }
            else
            {
                StaticPatrolNoiseInvestigationDistanceLimitGeneralCB.Checked = false;
                StaticPatrolNoiseInvestigationDistanceLimitNUD.Enabled = true;
                StaticPatrolNoiseInvestigationDistanceLimitNUD.Value = (decimal)_data.NoiseInvestigationDistanceLimit;
            }
            if (_data.MaxFlankingDistance == -1)
            {
                StsticPatrolMaxFlankingDistanceNUD.Enabled = false;
                StsticPatrolMaxFlankingDistanceGeneralCB.Checked = true;
                if ((decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.MaxFlankingDistance == -1)
                {
                    StsticPatrolMaxFlankingDistanceNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.MaxFlankingDistance;
                }
                else
                {
                    StsticPatrolMaxFlankingDistanceNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.MaxFlankingDistance;
                }
            }
            else
            {
                StsticPatrolMaxFlankingDistanceGeneralCB.Checked = false;
                StsticPatrolMaxFlankingDistanceNUD.Enabled = true;
                StsticPatrolMaxFlankingDistanceNUD.Value = (decimal)_data.MaxFlankingDistance;
            }
            if (_data.EnableFlankingOutsideCombat == -1)
            {
                StaticPatrolEnableFlankingOutsideCombatCB.Enabled = false;
                StaticPatrolEnableFlankingOutsideCombatGeneralCB.Checked = true;
                if ((decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.EnableFlankingOutsideCombat == -1)
                {
                    StaticPatrolEnableFlankingOutsideCombatCB.Checked = AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.EnableFlankingOutsideCombat == 1 ? true : false;
                }
                else
                {
                    StaticPatrolEnableFlankingOutsideCombatCB.Checked = AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.EnableFlankingOutsideCombat == 1 ? true : false;
                }
            }
            else
            {
                StaticPatrolEnableFlankingOutsideCombatGeneralCB.Checked = false;
                StaticPatrolEnableFlankingOutsideCombatCB.Enabled = true;
                StaticPatrolEnableFlankingOutsideCombatCB.Checked = _data.EnableFlankingOutsideCombat == 1 ? true : false;
            }
            if (_data.DamageMultiplier == -1)
            {
                StaticPatrolDamageMultiplierNUD.Enabled = false;
                StaticPatrolDamageMultiplierGeneralCB.Checked = true;
                if ((decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.DamageMultiplier == -1)
                {
                    StaticPatrolDamageMultiplierNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.DamageMultiplier;
                }
                else
                {
                    StaticPatrolDamageMultiplierNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.DamageMultiplier;
                }
            }
            else
            {
                StaticPatrolDamageMultiplierGeneralCB.Checked = false;
                StaticPatrolDamageMultiplierNUD.Enabled = true;
                StaticPatrolDamageMultiplierNUD.Value = (decimal)_data.DamageMultiplier;
            }
            if (_data.DamageReceivedMultiplier == -1)
            {
                StaticPatrolDamageReceivedMultiplierNUD.Enabled = false;
                StaticPatrolDamageReceivedMultiplierGeneralCB.Checked = true;
                if ((decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.DamageReceivedMultiplier == -1)
                {
                    StaticPatrolDamageReceivedMultiplierNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.DamageReceivedMultiplier;
                }
                else
                {
                    StaticPatrolDamageReceivedMultiplierNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.DamageReceivedMultiplier;
                }
            }
            else
            {
                StaticPatrolDamageReceivedMultiplierGeneralCB.Checked = false;
                StaticPatrolDamageReceivedMultiplierNUD.Enabled = true;
                StaticPatrolDamageReceivedMultiplierNUD.Value = (decimal)_data.DamageReceivedMultiplier;
            }
            if (_data.ShoryukenChance == -1)
            {
                StaticPatrolShoryukenChanceNUD.Enabled = false;
                staticPatrolShoryukenChanceGeneralCB.Checked = true;
                if ((decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.ShoryukenChance == -1)
                {
                    StaticPatrolShoryukenChanceNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.ShoryukenChance;
                }
                else
                {
                    StaticPatrolShoryukenChanceNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.ShoryukenChance;
                }
            }
            else
            {
                staticPatrolShoryukenChanceGeneralCB.Checked = false;
                StaticPatrolShoryukenChanceNUD.Enabled = true;
                StaticPatrolShoryukenChanceNUD.Value = (decimal)_data.ShoryukenChance;
            }
            if (_data.ShoryukenDamageMultiplier == -1)
            {
                StaticPatrolShoryukenDamageMultiplierNID.Enabled = false;
                StaticPatrolShoryukenDamageMultiplierGeneralCB.Checked = true;
                if ((decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.ShoryukenDamageMultiplier == -1)
                {
                    StaticPatrolShoryukenDamageMultiplierNID.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.ShoryukenDamageMultiplier;
                }
                else
                {
                    StaticPatrolShoryukenDamageMultiplierNID.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.ShoryukenDamageMultiplier;
                }
            }
            else
            {
                StaticPatrolShoryukenDamageMultiplierGeneralCB.Checked = false;
                StaticPatrolShoryukenDamageMultiplierNID.Enabled = true;
                StaticPatrolShoryukenDamageMultiplierNID.Value = (decimal)_data.ShoryukenDamageMultiplier;
            }
            if (_data.MinDistRadius == -1)
            {
                StaticPatrolMinDistRadiusNUD.Enabled = false;
                StaticPatrolMinDistRadiusGeneralCB.Checked = true;
                StaticPatrolMinDistRadiusNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.MinDistRadius;
            }
            else
            {
                StaticPatrolMinDistRadiusGeneralCB.Checked = false;
                StaticPatrolMinDistRadiusNUD.Enabled = true;
                StaticPatrolMinDistRadiusNUD.Value = (decimal)_data.MinDistRadius;
            }
            if (_data.MaxDistRadius == -1)
            {
                StaticPatrolMaxDistRadiusNUD.Enabled = false;
                StaticPatrolMaxDistRadiusGeneralCB.Checked = true;
                StaticPatrolMaxDistRadiusNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.MaxDistRadius;
            }
            else
            {
                StaticPatrolMaxDistRadiusGeneralCB.Checked = false;
                StaticPatrolMaxDistRadiusNUD.Enabled = true;
                StaticPatrolMaxDistRadiusNUD.Value = (decimal)_data.MaxDistRadius;
            }
            if (_data.DespawnTime == -1)
            {
                StaticPatrolDespawnTimeNUD.Enabled = false;
                StaticPatrolDespawnTimeGeneralCB.Checked = true;
                StaticPatrolDespawnTimeNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.DespawnTime;
            }
            else
            {
                StaticPatrolDespawnTimeGeneralCB.Checked = false;
                StaticPatrolDespawnTimeNUD.Enabled = true;
                StaticPatrolDespawnTimeNUD.Value = (decimal)_data.DespawnTime;
            }
            if (_data.DespawnRadius == -1)
            {
                StaticPatrolDespawnRadiusNUD.Enabled = false;
                StaticPatrolDespawnRadiusGeneralCB.Checked = true;
                StaticPatrolDespawnRadiusNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.DespawnRadius;
            }
            else
            {
                StaticPatrolDespawnRadiusGeneralCB.Checked = false;
                StaticPatrolDespawnRadiusNUD.Enabled = true;
                StaticPatrolDespawnRadiusNUD.Value = (decimal)_data.DespawnRadius;
            }
            if (_data.RespawnTime == -2)
            {
                StaticPatrolRespawnTimeGeneralCB.Checked = true;

                if (AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.RespawnTime == -1)
                {
                    StaticPatrolRespawnTimeNWonttRespawnCB.Enabled = false;
                    StaticPatrolRespawnTimeNUD.Visible = false;
                    StaticPatrolRespawnTimeNWonttRespawnCB.Checked = true;
                    StaticPatrolRespawnTimeNWonttRespawnCB.Visible = true;
                }
                else
                {
                    StaticPatrolRespawnTimeNUD.Visible = true;
                    StaticPatrolRespawnTimeNWonttRespawnCB.Visible = false;
                    StaticPatrolRespawnTimeNUD.Enabled = false;
                    StaticPatrolRespawnTimeNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.RespawnTime;
                    StaticPatrolRespawnTimeNWonttRespawnCB.Visible = false;
                }
            }
            else if (_data.RespawnTime == -1)
            {
                StaticPatrolRespawnTimeGeneralCB.Checked = false;
                StaticPatrolRespawnTimeNWonttRespawnCB.Checked = true;
                StaticPatrolRespawnTimeNWonttRespawnCB.Visible = true;
                StaticPatrolRespawnTimeNUD.Visible = false;
            }
            else
            {
                StaticPatrolRespawnTimeNWonttRespawnCB.Visible = true;
                StaticPatrolRespawnTimeNWonttRespawnCB.Checked = false;
                StaticPatrolRespawnTimeGeneralCB.Checked = false;
                StaticPatrolRespawnTimeNUD.Visible = true;
                StaticPatrolRespawnTimeNUD.Enabled = true;
                StaticPatrolRespawnTimeNUD.Value = (decimal)_data.RespawnTime;
            }



            int StaticPatrolUnlimitedReloadBitmask = (int)_data.UnlimitedReload;
            if (StaticPatrolUnlimitedReloadBitmask == 1)
                StaticPatrolUnlimitedReloadBitmask = 30;
            StaticPatrolURAnimalsCB.Checked = ((StaticPatrolUnlimitedReloadBitmask & 2) != 0) ? true : false;
            StaticPatrolURInfectedCB.Checked = ((StaticPatrolUnlimitedReloadBitmask & 4) != 0) ? true : false;
            StaticPatrolURPlayersCB.Checked = ((StaticPatrolUnlimitedReloadBitmask & 8) != 0) ? true : false;
            StaticPatrolURVehiclesCB.Checked = ((StaticPatrolUnlimitedReloadBitmask & 16) != 0) ? true : false;

            for (int i = 0; i < StaticPatrolLootingBehaviousCLB.Items.Count; i++)
            {
                StaticPatrolLootingBehaviousCLB.SetItemChecked(i, false);
            }
            foreach (string s in _data.LootingBehaviour.Split('|'))
            {
                if (s == "") continue;
                StaticPatrolLootingBehaviousCLB.SetItemChecked(StaticPatrolLootingBehaviousCLB.Items.IndexOf(s.Trim()), true);
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
                _nodes.Last().Text = _data.Name;
            }
        }

        #endregion

        private void StaticPatrolNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Name = StaticPatrolNameTB.Text;
            UpdateTreeNodeText();

        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ObjectClassName = textBox6.Text;

        }
        private void StaticPatrolFactionCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Faction = StaticPatrolFactionCB.GetItemText(StaticPatrolFactionCB.SelectedItem);

        }
        private void StaticPatrolFormationCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Formation = StaticPatrolFormationCB.GetItemText(StaticPatrolFormationCB.SelectedItem);

        }
        private void StaticPatrolFormationLoosenessNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.FormationLooseness = (int)StaticPatrolFormationLoosenessNUD.Value;

        }
        private void StaticPatrolFormationScaleNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.FormationScale = StaticPatrolFormationScaleNUD.Value;

        }
        private void StaticPatrolLoadoutsCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Loadout = StaticPatrolLoadoutsCB.GetItemText(StaticPatrolLoadoutsCB.SelectedItem);

        }
        private void StaticPatrolNumberOfAINUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NumberOfAI = (int)StaticPatrolNumberOfAINUD.Value;

        }
        private void StaticPatrolNumberOfAIMaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NumberOfAIMax = (int)StaticPatrolNumberOfAIMaxNUD.Value;

        }
        private void StaticPatrolPersistCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Persist = StaticPatrolPersistCB.Checked == true ? 1 : 0;

        }
        private void StaticPatrolCanBeTriggeredByAICB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CanBeTriggeredByAI = StaticPatrolCanBeTriggeredByAICB.Checked == true ? 1 : 0;

        }
        private void StaticPatrolBehaviorCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Behaviour = StaticPatrolBehaviorCB.GetItemText(StaticPatrolBehaviorCB.SelectedItem);

        }
        private void StaticPatrolSpeedCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Speed = StaticPatrolSpeedCB.GetItemText(StaticPatrolSpeedCB.SelectedItem);

        }
        private void StaticPatrolUnderThreatSpeedCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UnderThreatSpeed = StaticPatrolUnderThreatSpeedCB.GetItemText(StaticPatrolUnderThreatSpeedCB.SelectedItem);

        }
        private void StaticPatrolWaypointInterpolationCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.WaypointInterpolation = StaticPatrolWaypointInterpolationCB.GetItemText(StaticPatrolWaypointInterpolationCB.SelectedItem);

        }
        private void StaticPatrolLoadBalancingCategoryCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.LoadBalancingCategory = StaticPatrolLoadBalancingCategoryCB.GetItemText(StaticPatrolLoadBalancingCategoryCB.SelectedItem);

        }
        private void StaticPatrolLootDropOnDeathCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.LootDropOnDeath = StaticPatrolLootDropOnDeathCB.GetItemText(StaticPatrolLootDropOnDeathCB.SelectedItem);

        }
        private void StaticPatrolAccuracyMinNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AccuracyMin = StaticPatrolAccuracyMinNUD.Value;

        }
        private void StaticPatrolAccuracyMaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AccuracyMax = StaticPatrolAccuracyMaxNUD.Value;

        }
        private void StaticPatrolThreatDistanceLimitNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ThreatDistanceLimit = StaticPatrolThreatDistanceLimitNUD.Value;

        }
        private void StaticPatrolSniperProneDistanceThresholdNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SniperProneDistanceThreshold = StaticPatrolSniperProneDistanceThresholdNUD.Value;

        }
        private void StaticPatrolDamageReceivedMultiplierNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DamageReceivedMultiplier = StaticPatrolDamageReceivedMultiplierNUD.Value;
        }
        private void StaticPatrolShoryukenDamageMultiplierNID_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ShoryukenDamageMultiplier = StaticPatrolShoryukenDamageMultiplierNID.Value;
        }
        private void StaticPatrolShoryukenChanceNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ShoryukenChance = StaticPatrolShoryukenChanceNUD.Value;
        }
        private void StaticPatrolDamageMultiplierNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DamageMultiplier = StaticPatrolDamageMultiplierNUD.Value;

        }
        private void StaticPatrolNoiseInvestigationDistanceLimitNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NoiseInvestigationDistanceLimit = StaticPatrolNoiseInvestigationDistanceLimitNUD.Value;

        }
        private void StaticPatrolMinSpreadRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MinSpreadRadius = (int)StaticPatrolMinSpreadRadiusNUD.Value;

        }
        private void StaticPatrolMaxSpreadRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxSpreadRadius = (int)StaticPatrolMaxSpreadRadiusNUD.Value;

        }
        private void StaticPatrolRespawnTimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.RespawnTime = StaticPatrolRespawnTimeNUD.Value;

        }
        private void StaticPatrolDespawnTimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DespawnTime = StaticPatrolDespawnTimeNUD.Value;

        }
        private void StaticPatrolDespawnRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DespawnRadius = StaticPatrolDespawnRadiusNUD.Value;

        }
        private void StaticPatrolMaxDistRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxDistRadius = StaticPatrolMaxDistRadiusNUD.Value;

        }
        private void StaticPatrolMinDistRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MinDistRadius = StaticPatrolMinDistRadiusNUD.Value;

        }
        private void StaticPatrolChanceCB_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Chance = StaticPatrolChanceCB.Value;

        }
        private void StaticPatrolCanBeLotedCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CanBeLooted = StaticPatrolCanBeLotedCB.Checked == true ? 1 : 0;

        }
        private void StaticPatrolURBitmaskCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            int StaticPatrolUnlimitedReloadBitmask = 0;
            StaticPatrolUnlimitedReloadBitmask |= StaticPatrolURAnimalsCB.Checked ? 2 : 0;
            StaticPatrolUnlimitedReloadBitmask |= StaticPatrolURInfectedCB.Checked ? 4 : 0;
            StaticPatrolUnlimitedReloadBitmask |= StaticPatrolURPlayersCB.Checked ? 8 : 0;
            StaticPatrolUnlimitedReloadBitmask |= StaticPatrolURVehiclesCB.Checked ? 16 : 0;
            if (StaticPatrolUnlimitedReloadBitmask == 30)
                StaticPatrolUnlimitedReloadBitmask = 1;
            _data.UnlimitedReload = StaticPatrolUnlimitedReloadBitmask;

        }
        private void StaticPatrolLootingBehaviousCLB_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_suppressEvents) return;
            var list = (CheckedListBox)sender;
            string changedItem = list.Items[e.Index].ToString();
            bool willBeChecked = e.NewValue == CheckState.Checked;

            // Temporarily remove the event handler to avoid recursion
            list.ItemCheck -= StaticPatrolLootingBehaviousCLB_ItemCheck;

            if (changedItem == "ALL" && willBeChecked)
            {
                // Uncheck everything else
                for (int i = 0; i < list.Items.Count; i++)
                {
                    if (i != e.Index)
                        list.SetItemChecked(i, false);
                }
            }
            else
            {
                // Uncheck "ALL" if anything else is checked
                int allIndex = list.Items.IndexOf("ALL");
                if (allIndex >= 0)
                    list.SetItemChecked(allIndex, false);


                // WEAPONS logic
                if (changedItem == "WEAPONS" && willBeChecked)
                {
                    for (int i = 0; i < list.Items.Count; i++)
                    {
                        if (list.Items[i].ToString().StartsWith("WEAPONS_"))
                            list.SetItemChecked(i, false);
                    }
                }
                else if (changedItem.StartsWith("WEAPONS_") && willBeChecked)
                {
                    int weaponsIndex = list.Items.IndexOf("WEAPONS");
                    if (weaponsIndex >= 0)
                        list.SetItemChecked(weaponsIndex, false);
                }

                // CLOTHING logic
                if (changedItem == "CLOTHING" && willBeChecked)
                {
                    for (int i = 0; i < list.Items.Count; i++)
                    {
                        if (list.Items[i].ToString().StartsWith("CLOTHING_"))
                            list.SetItemChecked(i, false);
                    }
                }
                else if (changedItem.StartsWith("CLOTHING_") && willBeChecked)
                {
                    int clothingIndex = list.Items.IndexOf("CLOTHING");
                    if (clothingIndex >= 0)
                        list.SetItemChecked(clothingIndex, false);
                }

                // CLOTHING_BACK hierarchy logic
                if (changedItem == "CLOTHING_BACK" && willBeChecked)
                {
                    // Uncheck all sub-options
                    for (int i = 0; i < list.Items.Count; i++)
                    {
                        string item = list.Items[i].ToString();
                        if (item == "CLOTHING_BACK_SMALL" ||
                            item == "CLOTHING_BACK_MEDIUM" ||
                            item == "CLOTHING_BACK_LARGE")
                        {
                            list.SetItemChecked(i, false);
                        }
                    }
                }
                else if ((changedItem == "CLOTHING_BACK_SMALL" ||
                          changedItem == "CLOTHING_BACK_MEDIUM" ||
                          changedItem == "CLOTHING_BACK_LARGE") && willBeChecked)
                {
                    int backIndex = list.Items.IndexOf("CLOTHING_BACK");
                    if (backIndex >= 0)
                        list.SetItemChecked(backIndex, false);
                }
            }

            // Reattach the event handler
            list.ItemCheck += StaticPatrolLootingBehaviousCLB_ItemCheck;

            // Finally, update the checked items string (now safely updated)

            _data.LootingBehaviour = UpdateCheckedItemsString(list, e.Index, e.NewValue);

        }
        private string UpdateCheckedItemsString(CheckedListBox list, int changingIndex, CheckState newState)
        {
            List<string> selected = new List<string>();
            for (int i = 0; i < list.Items.Count; i++)
            {
                bool isChecked = (i == changingIndex) ? (newState == CheckState.Checked) : list.GetItemChecked(i);
                if (isChecked)
                {
                    selected.Add(list.Items[i].ToString());
                }
            }
            return string.Join(" | ", selected);
        }
        private void StaticPatrolLootingBehaviousCLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((CheckedListBox)sender).ClearSelected();
        }
        private void StaticPatrolUseRandomWaypointAsStartPointCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseRandomWaypointAsStartPoint = StaticPatrolUseRandomWaypointAsStartPointCB.Checked == true ? 1 : 0;

        }
        private void StaticPatrolCanSpawnInContaminatedAreaCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CanSpawnInContaminatedArea = StaticPatrolCanSpawnInContaminatedAreaCB.Checked == true ? 1 : 0;

        }
        private void StaticPatrolHeadshotResistanceNUD_Valuechanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HeadshotResistance = StaticPatrolHeadshotResistanceNUD.Value;

        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void StaticPatrolFormationScaleGeneralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolFormationScaleGeneralCB.Checked)
            {
                StaticPatrolFormationScaleNUD.Enabled = false;
                _data.FormationScale = -1;
                _suppressEvents = true;
                if ((decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.FormationScale == -1)
                {
                    StaticPatrolFormationScaleNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.FormationScale;
                }
                else
                {
                    StaticPatrolFormationScaleNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.FormationScale;
                }
                _suppressEvents = false;
            }
            else
            {
                StaticPatrolFormationScaleNUD.Enabled = true;
                _data.FormationScale = StaticPatrolFormationScaleNUD.Value;
            }

        }
        private void StaticPatrolAccuracyMinGeneralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolAccuracyMinGeneralCB.Checked)
            {
                StaticPatrolAccuracyMinNUD.Enabled = false;
                _data.AccuracyMin = -1;
                _suppressEvents = true;
                if ((decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.AccuracyMin == -1)
                {
                    StaticPatrolAccuracyMinNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.AccuracyMin;
                }
                else
                {
                    StaticPatrolAccuracyMinNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.AccuracyMin;
                }
                _suppressEvents = false;
            }
            else
            {
                StaticPatrolAccuracyMinNUD.Enabled = true;
                _data.AccuracyMin = StaticPatrolAccuracyMinNUD.Value;
            }

        }
        private void StaticPatrolAccuracyMaxGenerralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolAccuracyMaxGenerralCB.Checked)
            {
                StaticPatrolAccuracyMaxNUD.Enabled = false;
                _data.AccuracyMax = -1;
                _suppressEvents = true;
                if ((decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.AccuracyMax == -1)
                {
                    StaticPatrolAccuracyMaxNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.AccuracyMax;
                }
                else
                {
                    StaticPatrolAccuracyMaxNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.AccuracyMax;
                }
                _suppressEvents = false;
            }
            else
            {
                StaticPatrolAccuracyMaxNUD.Enabled = true;
                _data.AccuracyMax = StaticPatrolAccuracyMaxNUD.Value;
            }

        }
        private void StaticPatrolThreatDistanceLimitGeneralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolThreatDistanceLimitGeneralCB.Checked)
            {
                StaticPatrolThreatDistanceLimitNUD.Enabled = false;
                _data.ThreatDistanceLimit = -1;
                if ((decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.ThreatDistanceLimit == -1)
                {
                    StaticPatrolThreatDistanceLimitNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.ThreatDistanceLimit;
                }
                else
                {
                    StaticPatrolThreatDistanceLimitNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.ThreatDistanceLimit;
                }
                _suppressEvents = false;
            }
            else
            {
                StaticPatrolThreatDistanceLimitNUD.Enabled = true;
                _data.ThreatDistanceLimit = StaticPatrolThreatDistanceLimitNUD.Value;
            }

        }
        private void StaticPatrolNoiseInvestigationDistanceLimitGeneralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolNoiseInvestigationDistanceLimitGeneralCB.Checked)
            {
                StaticPatrolNoiseInvestigationDistanceLimitNUD.Enabled = false;
                _data.NoiseInvestigationDistanceLimit = -1;
                if ((decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.NoiseInvestigationDistanceLimit == -1)
                {
                    StaticPatrolNoiseInvestigationDistanceLimitNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.NoiseInvestigationDistanceLimit;
                }
                else
                {
                    StaticPatrolNoiseInvestigationDistanceLimitNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.NoiseInvestigationDistanceLimit;
                }
                _suppressEvents = false;
            }
            else
            {
                StaticPatrolNoiseInvestigationDistanceLimitNUD.Enabled = true;
                _data.NoiseInvestigationDistanceLimit = StaticPatrolNoiseInvestigationDistanceLimitNUD.Value;
            }

        }
        private void StsticPatrolMaxFlankingDistanceGeneralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StsticPatrolMaxFlankingDistanceGeneralCB.Checked)
            {
                StsticPatrolMaxFlankingDistanceNUD.Enabled = false;
                _data.MaxFlankingDistance = -1;
                if ((decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.MaxFlankingDistance == -1)
                {
                    StsticPatrolMaxFlankingDistanceNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.MaxFlankingDistance;
                }
                else
                {
                    StsticPatrolMaxFlankingDistanceNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.MaxFlankingDistance;
                }
                _suppressEvents = false;
            }
            else
            {
                StsticPatrolMaxFlankingDistanceNUD.Enabled = true;
                _data.MaxFlankingDistance = StsticPatrolMaxFlankingDistanceNUD.Value;
            }

        }
        private void StaticPatrolEnableFlankingOutsideCombatGeneralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolEnableFlankingOutsideCombatGeneralCB.Checked)
            {
                StaticPatrolEnableFlankingOutsideCombatCB.Enabled = false;
                _data.EnableFlankingOutsideCombat = -1;
                if ((decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.EnableFlankingOutsideCombat == -1)
                {
                    StaticPatrolEnableFlankingOutsideCombatCB.Checked = AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.EnableFlankingOutsideCombat == 1 ? true : false;
                }
                else
                {
                    StaticPatrolEnableFlankingOutsideCombatCB.Checked = AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.EnableFlankingOutsideCombat == 1 ? true : false;
                }
                _suppressEvents = false;
            }
            else
            {
                StaticPatrolEnableFlankingOutsideCombatCB.Enabled = true;
                _data.EnableFlankingOutsideCombat = StaticPatrolEnableFlankingOutsideCombatCB.Checked == true ? 1 : 0;
            }

        }
        private void StaticPatrolDamageMultiplierGeneralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolDamageMultiplierGeneralCB.Checked)
            {
                StaticPatrolDamageMultiplierNUD.Enabled = false;
                _data.DamageMultiplier = -1;
                if ((decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.DamageMultiplier == -1)
                {
                    StaticPatrolDamageMultiplierNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.DamageMultiplier;
                }
                else
                {
                    StaticPatrolDamageMultiplierNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.DamageMultiplier;
                }
                _suppressEvents = false;
            }
            else
            {
                StaticPatrolDamageMultiplierNUD.Enabled = true;
                _data.DamageMultiplier = StaticPatrolDamageMultiplierNUD.Value;
            }

        }
        private void StaticPatrolDamageReceivedMultiplierGeneralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolDamageReceivedMultiplierGeneralCB.Checked)
            {
                StaticPatrolDamageReceivedMultiplierNUD.Enabled = false;
                _data.DamageReceivedMultiplier = -1;
                if ((decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.DamageReceivedMultiplier == -1)
                {
                    StaticPatrolDamageReceivedMultiplierNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.DamageReceivedMultiplier;
                }
                else
                {
                    StaticPatrolDamageReceivedMultiplierNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.DamageReceivedMultiplier;
                }
                _suppressEvents = false;
            }
            else
            {
                StaticPatrolDamageReceivedMultiplierNUD.Enabled = true;
                _data.DamageReceivedMultiplier = StaticPatrolDamageReceivedMultiplierNUD.Value;
            }

        }
        private void StaticPatrolMinDistRadiusGeneralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolMinDistRadiusGeneralCB.Checked)
            {
                StaticPatrolMinDistRadiusNUD.Enabled = false;
                _data.MinDistRadius = -1;
                StaticPatrolMinDistRadiusNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.MinDistRadius;
                _suppressEvents = false;
            }
            else
            {
                StaticPatrolMinDistRadiusNUD.Enabled = true;
                _data.MinDistRadius = StaticPatrolMinDistRadiusNUD.Value;
            }

        }
        private void StaticPatrolMaxDistRadiusGeneralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolMaxDistRadiusGeneralCB.Checked)
            {
                StaticPatrolMaxDistRadiusNUD.Enabled = false;
                _data.MaxDistRadius = -1;
                StaticPatrolMaxDistRadiusNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.MaxDistRadius;
                _suppressEvents = false;
            }
            else
            {
                StaticPatrolMaxDistRadiusNUD.Enabled = true;
                _data.MaxDistRadius = StaticPatrolMaxDistRadiusNUD.Value;
            }

        }
        private void StaticPatrolDespawnTimeGeneralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolDespawnTimeGeneralCB.Checked)
            {
                StaticPatrolDespawnTimeNUD.Enabled = false;
                _data.DespawnTime = -1;
                StaticPatrolDespawnTimeNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.DespawnTime;
                _suppressEvents = false;
            }
            else
            {
                StaticPatrolDespawnTimeNUD.Enabled = true;
                _data.DespawnTime = StaticPatrolDespawnTimeNUD.Value;
            }

        }
        private void StaticPatrolDespawnRadiusGeneralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolDespawnRadiusGeneralCB.Checked)
            {
                StaticPatrolDespawnRadiusNUD.Enabled = false;
                _data.DespawnRadius = -1;
                StaticPatrolDespawnRadiusNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.DespawnRadius;
                _suppressEvents = false;
            }
            else
            {
                StaticPatrolDespawnRadiusNUD.Enabled = true;
                _data.DespawnRadius = StaticPatrolDespawnRadiusNUD.Value;
            }
        }
        private void StaticPatrolRespawnTimeGeneralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolRespawnTimeGeneralCB.Checked)
            {
                StaticPatrolRespawnTimeNUD.Enabled = false;
                _data.RespawnTime = -2;
                _suppressEvents = true;
                if (AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.RespawnTime == -1)
                {
                    StaticPatrolRespawnTimeNWonttRespawnCB.Checked = true;
                    StaticPatrolRespawnTimeNWonttRespawnCB.Enabled = false;
                    StaticPatrolRespawnTimeNUD.Visible = false;
                }
                else
                {
                    StaticPatrolRespawnTimeNWonttRespawnCB.Visible = false;
                    StaticPatrolRespawnTimeNUD.Visible = true;
                    StaticPatrolRespawnTimeNUD.Enabled = false;
                    StaticPatrolRespawnTimeNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.RespawnTime;
                }
                _suppressEvents = false;
            }
            else
            {
                StaticPatrolRespawnTimeNUD.Visible = true;
                StaticPatrolRespawnTimeNUD.Enabled = true;
                StaticPatrolRespawnTimeNWonttRespawnCB.Checked = false;
                StaticPatrolRespawnTimeNWonttRespawnCB.Enabled = true;
                StaticPatrolRespawnTimeNWonttRespawnCB.Visible = true;
                _data.RespawnTime = StaticPatrolRespawnTimeNUD.Value;
            }
        }
        private void StaticPatrolRespawnTimeNWonttRespawnCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolRespawnTimeNWonttRespawnCB.Checked)
            {
                StaticPatrolRespawnTimeNUD.Visible = false;
                _data.RespawnTime = -1;
            }
            else
            {
                if (StaticPatrolRespawnTimeGeneralCB.Checked)
                {
                    _suppressEvents = true;
                    StaticPatrolRespawnTimeGeneralCB.Checked = false;
                    _suppressEvents = false;
                }
                StaticPatrolRespawnTimeNUD.Visible = true;
                _data.RespawnTime = StaticPatrolRespawnTimeNUD.Value;
            }
        }
        private void staticPatrolShoryukenChanceGeneralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (staticPatrolShoryukenChanceGeneralCB.Checked)
            {
                StaticPatrolShoryukenChanceNUD.Enabled = false;
                _data.ShoryukenChance = -1;
                if ((decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.ShoryukenChance == -1)
                {
                    StaticPatrolShoryukenChanceNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.ShoryukenChance;
                }
                else
                {
                    StaticPatrolShoryukenChanceNUD.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.ShoryukenChance;
                }
                _suppressEvents = false;
            }
            else
            {
                StaticPatrolShoryukenChanceNUD.Enabled = true;
                _data.ShoryukenChance = StaticPatrolShoryukenChanceNUD.Value;
            }
        }
        private void StaticPatrolShoryukenDamageMultiplierGeneralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolShoryukenDamageMultiplierGeneralCB.Checked)
            {
                StaticPatrolShoryukenDamageMultiplierNID.Enabled = false;
                _data.ShoryukenDamageMultiplier = -1;
                if ((decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.ShoryukenDamageMultiplier == -1)
                {
                    StaticPatrolShoryukenDamageMultiplierNID.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIConfig.Data.ShoryukenDamageMultiplier;
                }
                else
                {
                    StaticPatrolShoryukenDamageMultiplierNID.Value = (decimal)AppServices.GetRequired<ExpansionManager>().ExpansionAIPatrolConfig.Data.ShoryukenDamageMultiplier;
                }
                _suppressEvents = false;
            }
            else
            {
                StaticPatrolShoryukenDamageMultiplierNID.Enabled = true;
                _data.ShoryukenDamageMultiplier = StaticPatrolShoryukenDamageMultiplierNID.Value;
            }
        }




    }
}