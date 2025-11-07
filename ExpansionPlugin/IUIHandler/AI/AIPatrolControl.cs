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
                StaticPatrolLoadoutsCB.DataSource = new BindingList<string>(LoadoutNameList);
                StaticPatrolLootDropOnDeathCB.DataSource = new BindingList<string>(LootDropOnDeathNameList);
                StaticPatrolLoadBalancingCategoryCB.DataSource = new BindingSource(parent.Data._LoadBalancingCategories, null);
            }
            StaticPatrolNameTB.Text = _data.Name;
            textBox6.Text = _data.ObjectClassName;
            StaticPatrolPersistCB.Checked = _data.Persist == 1 ? true : false;
            StaticPatrolFactionCB.SelectedIndex = StaticPatrolFactionCB.FindStringExact(_data.Faction);
            StaticPatrolNumberOfAINUD.Value = (decimal)_data.NumberOfAI;
            StaticPatrolBehaviorCB.SelectedIndex = StaticPatrolBehaviorCB.FindStringExact(_data.Behaviour);
            StaticPatrolSpeedCB.SelectedIndex = StaticPatrolSpeedCB.FindStringExact(_data.Speed);
            StaticPatrolUnderThreatSpeedCB.SelectedIndex = StaticPatrolUnderThreatSpeedCB.FindStringExact(_data.UnderThreatSpeed);
            StaticPatrolRespawnTimeNUD.Value = (decimal)_data.RespawnTime;
            StaticPatrolDespawnTimeNUD.Value = (decimal)_data.DespawnTime;
            StaticPatrolMinDistRadiusNUD.Value = (decimal)_data.MinDistRadius;
            StaticPatrolMaxDistRadiusNUD.Value = (decimal)_data.MaxDistRadius;
            StaticPatrolDespawnRadiusNUD.Value = (decimal)_data.DespawnRadius;
            StaticPatrolAccuracyMinNUD.Value = (decimal)_data.AccuracyMin;
            StaticPatrolAccuracyMaxNUD.Value = (decimal)_data.AccuracyMax;
            StaticPatrolDamageReceivedMultiplierNUD.Value = (decimal)_data.DamageReceivedMultiplier;
            StaticPatrolThreatDistanceLimitNUD.Value = (decimal)_data.ThreatDistanceLimit;
            StaticPatrolSniperProneDistanceThresholdNUD.Value = (decimal)_data.SniperProneDistanceThreshold;
            StaticPatrolDamageMultiplierNUD.Value = (decimal)_data.DamageMultiplier;
            StaticPatrolChanceCB.Value = (decimal)_data.Chance;
            StaticPatrolCanBeLotedCB.Checked = _data.CanBeLooted == 1 ? true : false;
            StaticPatrolLoadoutsCB.SelectedIndex = StaticPatrolLoadoutsCB.FindStringExact(_data.Loadout);
            StaticPatrolMinSpreadRadiusNUD.Value = (decimal)_data.MinSpreadRadius;
            StaticPatrolMaxSpreadRadiusNUD.Value = (decimal)_data.MaxSpreadRadius;
            StaticPatrolFormationCB.SelectedIndex = StaticPatrolFormationCB.FindStringExact(_data.Formation);
            StaticPatrolFormationLoosenessNUD.Value = (decimal)_data.FormationLooseness;
            StaticPatrolLoadBalancingCategoryCB.SelectedIndex = StaticPatrolLoadBalancingCategoryCB.FindStringExact(_data.LoadBalancingCategory);
            StaticPatrolWaypointInterpolationCB.SelectedIndex = StaticPatrolWaypointInterpolationCB.FindStringExact(_data.WaypointInterpolation);
            StaticPatrolLootDropOnDeathCB.SelectedIndex = StaticPatrolLootDropOnDeathCB.FindStringExact(_data.LootDropOnDeath);
            StaticPatrolUseRandomWaypointAsStartPointCB.Checked = _data.UseRandomWaypointAsStartPoint == 1 ? true : false;
            StaticPatrolCanBeTriggeredByAICB.Checked = _data.CanBeTriggeredByAI == 1 ? true : false;
            StaticPatrolNoiseInvestigationDistanceLimitNUD.Value = (decimal)_data.NoiseInvestigationDistanceLimit;
            StaticPatrolFormationScaleNUD.Value = (decimal)_data.FormationScale;
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
                CanBeLooted = data.CanBeLooted,
                LootDropOnDeath = data.LootDropOnDeath,
                UnlimitedReload = data.UnlimitedReload,
                SniperProneDistanceThreshold = data.SniperProneDistanceThreshold,
                AccuracyMin = data.AccuracyMin,
                AccuracyMax = data.AccuracyMax,
                ThreatDistanceLimit = data.ThreatDistanceLimit,
                NoiseInvestigationDistanceLimit = data.NoiseInvestigationDistanceLimit,
                DamageMultiplier = data.DamageMultiplier,
                DamageReceivedMultiplier = data.DamageReceivedMultiplier,
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
    }
}