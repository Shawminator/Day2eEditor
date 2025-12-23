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
        private ExpansionAIPatrol _originalData;
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
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;
            var parentObj = _nodes.Last().FindParentOfType(_parentType);
            if (parentObj != null)
            {
                dynamic parent = parentObj;
                BindingList<string> LoadoutNameList = new BindingList<string>
                {
                    ""
                };
                foreach (AILoadouts lo in AppServices.GetRequired<ExpansionManager>().ExpansionLoadoutConfig.AllData)
                {
                    LoadoutNameList.Add(Path.GetFileNameWithoutExtension(lo.FileName));
                }
                BindingList<string> LootDropOnDeathNameList = new BindingList<string>
                {
                    ""
                };
                foreach (AILootDrops AILootDrops in AppServices.GetRequired<ExpansionManager>().ExpansionLootDropConfig.AllData)
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
            StaticPatrolNumberOfAINUD.Value = (decimal)_data.NumberOfAI;
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

            if (_data.AccuracyMin == -1)
            {
                StaticPatrolAccuracyMinNUD.Visible = false;
                StaticPatrolAccuracyMinGeneralCB.Checked = true;
            }
            else
            {
                StaticPatrolAccuracyMinNUD.Visible = true;
                StaticPatrolAccuracyMinNUD.Value = (decimal)_data.AccuracyMin;
            }
            if (_data.AccuracyMax == -1)
            {
                StaticPatrolAccuracyMaxNUD.Visible = false;
                StaticPatrolAccuracyMaxGenerralCB.Checked = true;
            }
            else
            {
                StaticPatrolAccuracyMaxNUD.Visible = true;
                StaticPatrolAccuracyMaxNUD.Value = (decimal)_data.AccuracyMax;
            }
            if (_data.ThreatDistanceLimit == -1)
            {
                StaticPatrolThreatDistanceLimitNUD.Visible = false;
                StaticPatrolThreatDistanceLimitGeneralCB.Checked = true;
            }
            else
            {
                StaticPatrolThreatDistanceLimitNUD.Visible = true;
                StaticPatrolThreatDistanceLimitNUD.Value = (decimal)_data.ThreatDistanceLimit;
            }
            if (_data.NoiseInvestigationDistanceLimit == -1)
            {
                StaticPatrolNoiseInvestigationDistanceLimitNUD.Visible = false;
                StaticPatrolNoiseInvestigationDistanceLimitGeneralCB.Checked = true;
            }
            else
            {
                StaticPatrolNoiseInvestigationDistanceLimitNUD.Visible = true;
                StaticPatrolNoiseInvestigationDistanceLimitNUD.Value = (decimal)_data.NoiseInvestigationDistanceLimit;
            }
            if (_data.MaxFlankingDistance == -1)
            {
                StsticPatrolMaxFlankingDistanceNUD.Visible = false;
                StsticPatrolMaxFlankingDistanceGeneralCB.Checked = true;
            }
            else
            {
                StsticPatrolMaxFlankingDistanceNUD.Visible = true;
                StsticPatrolMaxFlankingDistanceNUD.Value = (decimal)_data.MaxFlankingDistance;
            }
            if (_data.EnableFlankingOutsideCombat == -1)
            {
                StaticPatrolEnableFlankingOutsideCombatCB.Visible = false;
                StaticPatrolEnableFlankingOutsideCombatGeneralCB.Checked = true;
            }
            else
            {
                StaticPatrolEnableFlankingOutsideCombatCB.Visible = true;
                StaticPatrolEnableFlankingOutsideCombatCB.Checked = _data.EnableFlankingOutsideCombat == 1 ? true : false;
            }
            if (_data.DamageMultiplier == -1)
            {
                StaticPatrolDamageMultiplierNUD.Visible = false;
                StaticPatrolDamageMultiplierGeneralCB.Checked = true;
            }
            else
            {
                StaticPatrolDamageMultiplierNUD.Visible = true;
                StaticPatrolDamageMultiplierNUD.Value = (decimal)_data.DamageMultiplier;
            }
            if (_data.DamageReceivedMultiplier == -1)
            {
                StaticPatrolDamageReceivedMultiplierNUD.Visible = false;
                StaticPatrolDamageReceivedMultiplierGeneralCB.Checked = true;
            }
            else
            {
                StaticPatrolDamageReceivedMultiplierNUD.Visible = true;
                StaticPatrolDamageReceivedMultiplierNUD.Value = (decimal)_data.DamageReceivedMultiplier;
            }
            if (_data.MinDistRadius == -1)
            {
                StaticPatrolMinDistRadiusNUD.Visible = false;
                StaticPatrolMinDistRadiusGeneralCB.Checked = true;
            }
            else
            {
                StaticPatrolMinDistRadiusNUD.Visible = true;
                StaticPatrolMinDistRadiusNUD.Value = (decimal)_data.MinDistRadius;
            }
            if (_data.MaxDistRadius == -1)
            {
                StaticPatrolMaxDistRadiusNUD.Visible = false;
                StaticPatrolMaxDistRadiusGeneralCB.Checked = true;
            }
            else
            {
                StaticPatrolMaxDistRadiusNUD.Visible = true;
                StaticPatrolMaxDistRadiusNUD.Value = (decimal)_data.MaxDistRadius;
            }
            if (_data.DespawnTime == -1)
            {
                StaticPatrolDespawnTimeNUD.Visible = false;
                StaticPatrolDespawnTimeGeneralCB.Checked = true;
            }
            else
            {
                StaticPatrolDespawnTimeNUD.Visible = true;
                StaticPatrolDespawnTimeNUD.Value = (decimal)_data.DespawnTime;
            }
            if (_data.DespawnRadius == -1)
            {
                StaticPatrolDespawnRadiusNUD.Visible = false;
                StaticPatrolDespawnRadiusGeneralCB.Checked = true;
            }
            else
            {
                StaticPatrolDespawnRadiusNUD.Visible = true;
                StaticPatrolDespawnRadiusNUD.Value = (decimal)_data.DespawnRadius;
            }
            if (_data.RespawnTime == -1)
            {
                StaticPatrolRespawnTimeNUD.Visible = false;
                StaticPatrolDespawnRadiusGeneralCB.Checked = true;
            }
            else if (_data.RespawnTime == -2)
            {
                StaticPatrolRespawnTimeNWonttRespawnCB.Checked = true;
                StaticPatrolRespawnTimeGeneralCB.Visible = false;
                StaticPatrolRespawnTimeNUD.Visible = false;
            }
            else
            {
                StaticPatrolRespawnTimeNUD.Visible = true;
                StaticPatrolRespawnTimeNUD.Value = (decimal)_data.RespawnTime;
            }
            if (_data.FormationScale == -1)
            {
                StaticPatrolFormationScaleNUD.Visible = false;
                StaticPatrolFormationScaleGeneralCB.Checked = true;
            }
            else
            {
                StaticPatrolFormationScaleNUD.Visible = true;
                StaticPatrolFormationScaleNUD.Value = (decimal)_data.FormationScale;
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

        private ExpansionAIPatrol CloneData(ExpansionAIPatrol data)
        {
            return new ExpansionAIPatrol
            {
                _waypoints = new BindingList<Vec3>(data._waypoints.Select(wp => new Vec3
                {
                    X = wp.X,
                    Y = wp.Y,
                    Z = wp.Z
                }).ToList()),

                Name = data.Name,
                Persist = data.Persist,
                Faction = data.Faction,
                Formation = data.Formation,
                FormationScale = data.FormationScale,
                FormationLooseness = data.FormationLooseness,
                Loadout = data.Loadout,
                Units = new BindingList<string>(data.Units.ToList()),
                NumberOfAI = data.NumberOfAI,
                Behaviour = data.Behaviour,
                LootingBehaviour = data.LootingBehaviour,
                Speed = data.Speed,
                UnderThreatSpeed = data.UnderThreatSpeed,
                DefaultStance = data.DefaultStance,
                DefaultLookAngle = data.DefaultLookAngle,
                CanBeLooted = data.CanBeLooted,
                LootDropOnDeath = data.LootDropOnDeath,
                UnlimitedReload = data.UnlimitedReload,
                SniperProneDistanceThreshold = data.SniperProneDistanceThreshold,
                AccuracyMin = data.AccuracyMin,
                AccuracyMax = data.AccuracyMax,
                ThreatDistanceLimit = data.ThreatDistanceLimit,
                NoiseInvestigationDistanceLimit = data.NoiseInvestigationDistanceLimit,
                MaxFlankingDistance = data.MaxFlankingDistance,
                EnableFlankingOutsideCombat = data.EnableFlankingOutsideCombat,
                DamageMultiplier = data.DamageMultiplier,
                DamageReceivedMultiplier = data.DamageReceivedMultiplier,
                HeadshotResistance = data.HeadshotResistance,
                CanBeTriggeredByAI = data.CanBeTriggeredByAI,
                MinDistRadius = data.MinDistRadius,
                MaxDistRadius = data.MaxDistRadius,
                DespawnRadius = data.DespawnRadius,
                MinSpreadRadius = data.MinSpreadRadius,
                MaxSpreadRadius = data.MaxSpreadRadius,
                Chance = data.Chance,
                DespawnTime = data.DespawnTime,
                RespawnTime = data.RespawnTime,
                LoadBalancingCategory = data.LoadBalancingCategory,
                ObjectClassName = data.ObjectClassName,
                WaypointInterpolation = data.WaypointInterpolation,
                UseRandomWaypointAsStartPoint = data.UseRandomWaypointAsStartPoint,
                Waypoints = new BindingList<float[]>(data.Waypoints.Select(wp => wp.ToArray()).ToList())
            };
        }


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
            HasChanges();
        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ObjectClassName = textBox6.Text;
            HasChanges();
        }
        private void StaticPatrolFactionCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Faction = StaticPatrolFactionCB.GetItemText(StaticPatrolFactionCB.SelectedItem);
            HasChanges();
        }
        private void StaticPatrolFormationCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Formation = StaticPatrolFormationCB.GetItemText(StaticPatrolFormationCB.SelectedItem);
            HasChanges();
        }
        private void StaticPatrolFormationLoosenessNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.FormationLooseness = (int)StaticPatrolFormationLoosenessNUD.Value;
            HasChanges();
        }
        private void StaticPatrolFormationScaleNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.FormationScale = StaticPatrolFormationScaleNUD.Value;
            HasChanges();
        }
        private void StaticPatrolLoadoutsCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Loadout = StaticPatrolLoadoutsCB.GetItemText(StaticPatrolLoadoutsCB.SelectedItem);
            HasChanges();
        }
        private void StaticPatrolNumberOfAINUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NumberOfAI = (int)StaticPatrolNumberOfAINUD.Value;
            HasChanges();
        }
        private void StaticPatrolPersistCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Persist = StaticPatrolPersistCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void StaticPatrolCanBeTriggeredByAICB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CanBeTriggeredByAI = StaticPatrolCanBeTriggeredByAICB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void StaticPatrolBehaviorCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Behaviour = StaticPatrolBehaviorCB.GetItemText(StaticPatrolBehaviorCB.SelectedItem);
            HasChanges();
        }
        private void StaticPatrolSpeedCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Speed = StaticPatrolSpeedCB.GetItemText(StaticPatrolSpeedCB.SelectedItem);
            HasChanges();
        }
        private void StaticPatrolUnderThreatSpeedCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UnderThreatSpeed = StaticPatrolUnderThreatSpeedCB.GetItemText(StaticPatrolUnderThreatSpeedCB.SelectedItem);
            HasChanges();
        }
        private void StaticPatrolWaypointInterpolationCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.WaypointInterpolation = StaticPatrolWaypointInterpolationCB.GetItemText(StaticPatrolWaypointInterpolationCB.SelectedItem);
            HasChanges();
        }
        private void StaticPatrolLoadBalancingCategoryCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.LoadBalancingCategory = StaticPatrolLoadBalancingCategoryCB.GetItemText(StaticPatrolLoadBalancingCategoryCB.SelectedItem);
            HasChanges();
        }
        private void StaticPatrolLootDropOnDeathCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.LootDropOnDeath = StaticPatrolLootDropOnDeathCB.GetItemText(StaticPatrolLootDropOnDeathCB.SelectedItem);
            HasChanges();
        }
        private void StaticPatrolAccuracyMinNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AccuracyMin = StaticPatrolAccuracyMinNUD.Value;
            HasChanges();
        }
        private void StaticPatrolAccuracyMaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AccuracyMax = StaticPatrolAccuracyMaxNUD.Value;
            HasChanges();
        }
        private void StaticPatrolThreatDistanceLimitNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ThreatDistanceLimit = StaticPatrolThreatDistanceLimitNUD.Value;
            HasChanges();
        }
        private void StaticPatrolSniperProneDistanceThresholdNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SniperProneDistanceThreshold = StaticPatrolSniperProneDistanceThresholdNUD.Value;
            HasChanges();
        }
        private void StaticPatrolDamageReceivedMultiplierNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DamageReceivedMultiplier = StaticPatrolDamageReceivedMultiplierNUD.Value;
            HasChanges();

        }
        private void StaticPatrolDamageMultiplierNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DamageMultiplier = StaticPatrolDamageMultiplierNUD.Value;
            HasChanges();
        }
        private void StaticPatrolNoiseInvestigationDistanceLimitNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NoiseInvestigationDistanceLimit = StaticPatrolNoiseInvestigationDistanceLimitNUD.Value;
            HasChanges();
        }
        private void StaticPatrolMinSpreadRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MinSpreadRadius = (int)StaticPatrolMinSpreadRadiusNUD.Value;
            HasChanges();
        }
        private void StaticPatrolMaxSpreadRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxSpreadRadius = (int)StaticPatrolMaxSpreadRadiusNUD.Value;
            HasChanges();
        }
        private void StaticPatrolRespawnTimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.RespawnTime = StaticPatrolRespawnTimeNUD.Value;
            HasChanges();
        }
        private void StaticPatrolDespawnTimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DespawnTime = StaticPatrolDespawnTimeNUD.Value;
            HasChanges();
        }
        private void StaticPatrolDespawnRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DespawnRadius = StaticPatrolDespawnRadiusNUD.Value;
            HasChanges();
        }
        private void StaticPatrolMaxDistRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxDistRadius = StaticPatrolMaxDistRadiusNUD.Value;
            HasChanges();
        }
        private void StaticPatrolMinDistRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MinDistRadius = StaticPatrolMinDistRadiusNUD.Value;
            HasChanges();
        }
        private void StaticPatrolChanceCB_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Chance = StaticPatrolChanceCB.Value;
            HasChanges();
        }
        private void StaticPatrolCanBeLotedCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CanBeLooted = StaticPatrolCanBeLotedCB.Checked == true ? 1 : 0;
            HasChanges();
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
            HasChanges();
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
            HasChanges();
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
            HasChanges();
        }

        private void StaticPatrolHeadshotResistanceNUD_Valuechanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HeadshotResistance = StaticPatrolHeadshotResistanceNUD.Value;
            HasChanges();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void StaticPatrolAccuracyMinGeneralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolAccuracyMinGeneralCB.Checked)
            {
                StaticPatrolAccuracyMinNUD.Visible = false;
                _data.AccuracyMin = -1;
            }
            else
            {
                StaticPatrolAccuracyMinNUD.Visible = true;
                _data.AccuracyMin = StaticPatrolAccuracyMinNUD.Value;
            }
            HasChanges();
        }

        private void StaticPatrolAccuracyMaxGenerralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolAccuracyMaxGenerralCB.Checked)
            {
                StaticPatrolAccuracyMaxNUD.Visible = false;
                _data.AccuracyMax = -1;
            }
            else
            {
                StaticPatrolAccuracyMaxNUD.Visible = true;
                _data.AccuracyMax = StaticPatrolAccuracyMaxNUD.Value;
            }
            HasChanges();
        }

        private void StaticPatrolThreatDistanceLimitGeneralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolThreatDistanceLimitGeneralCB.Checked)
            {
                StaticPatrolThreatDistanceLimitNUD.Visible = false;
                _data.ThreatDistanceLimit = -1;
            }
            else
            {
                StaticPatrolThreatDistanceLimitNUD.Visible = true;
                _data.ThreatDistanceLimit = StaticPatrolThreatDistanceLimitNUD.Value;
            }
            HasChanges();
        }

        private void StaticPatrolNoiseInvestigationDistanceLimitGeneralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolNoiseInvestigationDistanceLimitGeneralCB.Checked)
            {
                StaticPatrolNoiseInvestigationDistanceLimitNUD.Visible = false;
                _data.NoiseInvestigationDistanceLimit = -1;
            }
            else
            {
                StaticPatrolNoiseInvestigationDistanceLimitNUD.Visible = true;
                _data.NoiseInvestigationDistanceLimit = StaticPatrolNoiseInvestigationDistanceLimitNUD.Value;
            }
            HasChanges();
        }

        private void StsticPatrolMaxFlankingDistanceGeneralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StsticPatrolMaxFlankingDistanceGeneralCB.Checked)
            {
                StsticPatrolMaxFlankingDistanceNUD.Visible = false;
                _data.MaxFlankingDistance = -1;
            }
            else
            {
                StsticPatrolMaxFlankingDistanceNUD.Visible = true;
                _data.MaxFlankingDistance = StsticPatrolMaxFlankingDistanceNUD.Value;
            }
            HasChanges();
        }

        private void StaticPatrolEnableFlankingOutsideCombatGeneralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolEnableFlankingOutsideCombatGeneralCB.Checked)
            {
                StaticPatrolEnableFlankingOutsideCombatCB.Visible = false;
                _data.EnableFlankingOutsideCombat = -1;
            }
            else
            {
                StaticPatrolEnableFlankingOutsideCombatCB.Visible = true;
                _data.EnableFlankingOutsideCombat = StaticPatrolEnableFlankingOutsideCombatCB.Checked == true ? 1: 0;
            }
            HasChanges();
        }

        private void StaticPatrolDamageMultiplierGeneralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolDamageMultiplierGeneralCB.Checked)
            {
                StaticPatrolDamageMultiplierNUD.Visible = false;
                _data.DamageMultiplier = -1;
            }
            else
            {
                StaticPatrolDamageMultiplierNUD.Visible = true;
                _data.DamageMultiplier = StaticPatrolDamageMultiplierNUD.Value;
            }
            HasChanges();
        }

        private void StaticPatrolDamageReceivedMultiplierGeneralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolDamageReceivedMultiplierGeneralCB.Checked)
            {
                StaticPatrolDamageReceivedMultiplierNUD.Visible = false;
                _data.DamageReceivedMultiplier = -1;
            }
            else
            {
                StaticPatrolDamageReceivedMultiplierNUD.Visible = true;
                _data.DamageReceivedMultiplier = StaticPatrolDamageReceivedMultiplierNUD.Value;
            }
            HasChanges();
        }

        private void StaticPatrolMinDistRadiusGeneralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolMinDistRadiusGeneralCB.Checked)
            {
                StaticPatrolMinDistRadiusNUD.Visible = false;
                _data.MinDistRadius = -1;
            }
            else
            {
                StaticPatrolMinDistRadiusNUD.Visible = true;
                _data.MinDistRadius = StaticPatrolMinDistRadiusNUD.Value;
            }
            HasChanges();
        }

        private void StaticPatrolMaxDistRadiusGeneralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolMaxDistRadiusGeneralCB.Checked)
            {
                StaticPatrolMaxDistRadiusNUD.Visible = false;
                _data.MaxDistRadius = -1;
            }
            else
            {
                StaticPatrolMaxDistRadiusNUD.Visible = true;
                _data.MaxDistRadius = StaticPatrolMaxDistRadiusNUD.Value;
            }
            HasChanges();
        }

        private void StaticPatrolDespawnTimeGeneralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolDespawnTimeGeneralCB.Checked)
            {
                StaticPatrolDespawnTimeNUD.Visible = false;
                _data.DespawnTime = -1;
            }
            else
            {
                StaticPatrolDespawnTimeNUD.Visible = true;
                _data.DespawnTime = StaticPatrolDespawnTimeNUD.Value;
            }
            HasChanges();
        }

        private void StaticPatrolDespawnRadiusGeneralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolDespawnRadiusGeneralCB.Checked)
            {
                StaticPatrolDespawnRadiusNUD.Visible = false;
                _data.DespawnRadius = -1;
            }
            else
            {
                StaticPatrolDespawnRadiusNUD.Visible = true;
                _data.DespawnRadius = StaticPatrolDespawnRadiusNUD.Value;
            }
            HasChanges();
        }

        private void StaticPatrolRespawnTimeGeneralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolRespawnTimeGeneralCB.Checked)
            {
                StaticPatrolRespawnTimeNUD.Visible = false;
                _data.RespawnTime = -1;
            }
            else
            {
                StaticPatrolRespawnTimeNUD.Visible = true;
                _data.RespawnTime = StaticPatrolRespawnTimeNUD.Value;
            }
            HasChanges();
        }

        private void StaticPatrolRespawnTimeNWonttRespawnCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolRespawnTimeNWonttRespawnCB.Checked)
            {
                StaticPatrolRespawnTimeGeneralCB.Visible = false;
                StaticPatrolRespawnTimeNUD.Visible = false;
                _data.RespawnTime = -2;
            }
            else
            {
                StaticPatrolRespawnTimeGeneralCB.Visible = true;
                if(StaticPatrolRespawnTimeGeneralCB.Checked)
                {
                    _suppressEvents = true;
                    StaticPatrolRespawnTimeGeneralCB.Checked = false;
                    _suppressEvents = false;
                }
                StaticPatrolRespawnTimeNUD.Visible = true;
                _data.RespawnTime = StaticPatrolRespawnTimeNUD.Value;
            }
            HasChanges();
        }

        private void StaticPatrolFormationScaleGeneralCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (StaticPatrolFormationScaleGeneralCB.Checked)
            {
                StaticPatrolFormationScaleNUD.Visible = false;
                _data.FormationScale = -1;
            }
            else
            {
                StaticPatrolFormationScaleNUD.Visible = true;
                _data.FormationScale = StaticPatrolFormationScaleNUD.Value;
            }
            HasChanges();
        }
    }
}