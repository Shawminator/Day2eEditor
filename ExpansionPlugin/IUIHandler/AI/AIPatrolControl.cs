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
                //foreach (AILootDrops AILootDrops in AILoadoutsConfig.AILootDropsData)
                //{
                //    LootDropOnDeathNameList.Add(Path.GetFileNameWithoutExtension(AILootDrops.Name));
                //}
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
            StaticPatrolThreatDistanceLimitNUD.Value =  (decimal)_data.ThreatDistanceLimit;
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
            // TODO: Implement actual cloning logic
            return new ExpansionAIPatrol
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