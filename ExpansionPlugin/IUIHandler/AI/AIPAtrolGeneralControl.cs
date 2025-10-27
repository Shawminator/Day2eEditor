using Day2eEditor;
using ExpansionPlugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class AIPAtrolGeneralControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionAIPatrolSettings _data;
        private ExpansionAIPatrolSettings _originalData;
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
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            AIGeneralEnabledCB.Checked = _data.Enabled == 1 ? true : false;
            AIGeneralDespawnTimeNUD.Value = (decimal)_data.DespawnTime;
            AIGeneralRespawnTimeNUD.Value = (decimal)_data.RespawnTime;
            AIGeneralMinDistRadiusNUD.Value = (decimal)_data.MinDistRadius;
            AIGeneralMaxDistRadiusNUD.Value = (decimal)_data.MaxDistRadius;
            AIGeneralDespawnRadiusNUD.Value = (decimal)_data.DespawnRadius;
            AIGeneralAccuracyMinNUD.Value = (decimal)_data.AccuracyMin;
            AIGeneralAccuracyMaxNUD.Value = (decimal)_data.AccuracyMax;
            AIGeneralThreatDistanceLimitNUD.Value = (decimal)_data.ThreatDistanceLimit;
            AIGeneralNoiseInvestigationDistanceLimitNUD.Value = (decimal)_data.NoiseInvestigationDistanceLimit;
            AIGeneralDanageMultiplierNUD.Value = (decimal)_data.DamageMultiplier;
            AIGeneralDamageReceivedMultiplierNUD.Value = (decimal)_data.DamageReceivedMultiplier;
            AIGeneralFormationScaleNUD.Value = (decimal)_data.FormationScale;

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

        private ExpansionAIPatrolSettings CloneData(ExpansionAIPatrolSettings data)
        {
            return new ExpansionAIPatrolSettings
            {
                m_Version = data.m_Version,
                Enabled = data.Enabled,
                FormationScale = data.FormationScale,
                DespawnTime = data.DespawnTime,
                RespawnTime = data.RespawnTime,
                MinDistRadius = data.MinDistRadius,
                MaxDistRadius = data.MaxDistRadius,
                DespawnRadius = data.DespawnRadius,
                AccuracyMin = data.AccuracyMin,
                AccuracyMax = data.AccuracyMax,
                ThreatDistanceLimit = data.ThreatDistanceLimit,
                NoiseInvestigationDistanceLimit = data.NoiseInvestigationDistanceLimit,
                DamageMultiplier = data.DamageMultiplier,
                DamageReceivedMultiplier = data.DamageReceivedMultiplier,

                LoadBalancingCategories = data.LoadBalancingCategories?.ToDictionary(
                    kvp => kvp.Key,
                    kvp => new BindingList<Loadbalancingcategories>(
                        kvp.Value.Select(c => new Loadbalancingcategories
                        {
                            MinPlayers = c.MinPlayers,
                            MaxPlayers = c.MaxPlayers,
                            MaxPatrols = c.MaxPatrols
                        }).ToList()
                    )
                ),

                _LoadBalancingCategories = data._LoadBalancingCategories != null
                    ? new BindingList<Loadbalancingcategorie>(
                        data._LoadBalancingCategories.Select(cat => new Loadbalancingcategorie
                        {
                            name = cat.name,
                            Categorieslist = new BindingList<Loadbalancingcategories>(
                                cat.Categorieslist.Select(c => new Loadbalancingcategories
                                {
                                    MinPlayers = c.MinPlayers,
                                    MaxPlayers = c.MaxPlayers,
                                    MaxPatrols = c.MaxPatrols
                                }).ToList()
                            )
                        }).ToList()
                    )
                    : null,

                Patrols = data.Patrols != null
                    ? new BindingList<ExpansionAIPatrol>(
                        data.Patrols.Select(p => new ExpansionAIPatrol
                        {
                            Name = p.Name,
                            Persist = p.Persist,
                            Faction = p.Faction,
                            Formation = p.Formation,
                            FormationScale = p.FormationScale,
                            FormationLooseness = p.FormationLooseness,
                            Loadout = p.Loadout,
                            Units = new BindingList<string>(p.Units.ToList()),
                            NumberOfAI = p.NumberOfAI,
                            Behaviour = p.Behaviour,
                            LootingBehaviour = p.LootingBehaviour,
                            Speed = p.Speed,
                            UnderThreatSpeed = p.UnderThreatSpeed,
                            CanBeLooted = p.CanBeLooted,
                            LootDropOnDeath = p.LootDropOnDeath,
                            UnlimitedReload = p.UnlimitedReload,
                            SniperProneDistanceThreshold = p.SniperProneDistanceThreshold,
                            AccuracyMin = p.AccuracyMin,
                            AccuracyMax = p.AccuracyMax,
                            ThreatDistanceLimit = p.ThreatDistanceLimit,
                            NoiseInvestigationDistanceLimit = p.NoiseInvestigationDistanceLimit,
                            DamageMultiplier = p.DamageMultiplier,
                            DamageReceivedMultiplier = p.DamageReceivedMultiplier,
                            CanBeTriggeredByAI = p.CanBeTriggeredByAI,
                            MinDistRadius = p.MinDistRadius,
                            MaxDistRadius = p.MaxDistRadius,
                            DespawnRadius = p.DespawnRadius,
                            MinSpreadRadius = p.MinSpreadRadius,
                            MaxSpreadRadius = p.MaxSpreadRadius,
                            Chance = p.Chance,
                            DespawnTime = p.DespawnTime,
                            RespawnTime = p.RespawnTime,
                            LoadBalancingCategory = p.LoadBalancingCategory,
                            ObjectClassName = p.ObjectClassName,
                            WaypointInterpolation = p.WaypointInterpolation,
                            UseRandomWaypointAsStartPoint = p.UseRandomWaypointAsStartPoint,
                            Waypoints = new BindingList<float[]>(p.Waypoints.Select(wp => wp.ToArray()).ToList()),
                            _waypoints = new BindingList<Vec3>(p._waypoints.Select(wp => new Vec3(wp.X, wp.Y, wp.Z)).ToList())
                        }).ToList()
                    )
                    : null
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

        private void AIGeneralDamageReceivedMultiplierNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DamageReceivedMultiplier = AIGeneralDamageReceivedMultiplierNUD.Value;
            HasChanges();
        }
        private void AIGeneralEnabledCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Enabled = AIGeneralEnabledCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void AIGeneralRespawnTimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.RespawnTime = AIGeneralRespawnTimeNUD.Value;
            HasChanges();
        }
        private void AIGeneralMinDistRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MinDistRadius = AIGeneralMinDistRadiusNUD.Value;
            HasChanges();
        }
        private void AIGeneralMaxDistRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxDistRadius = AIGeneralMaxDistRadiusNUD.Value;
            HasChanges();
        }
        private void AIGeneralDespawnTimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DespawnTime = AIGeneralDespawnTimeNUD.Value;
            HasChanges();
        }
        private void AIGeneralAccuracyMinNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AccuracyMin = AIGeneralAccuracyMinNUD.Value;
            HasChanges();
        }
        private void AIGenralAccuracyMaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AccuracyMax = AIGeneralAccuracyMaxNUD.Value;
            HasChanges();
        }
        private void AIGeneralDespawnRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DespawnRadius = AIGeneralDespawnRadiusNUD.Value;
            HasChanges();
        }
        private void AIGeneralThreatDistanceLimitNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ThreatDistanceLimit = AIGeneralThreatDistanceLimitNUD.Value;
            HasChanges();
        }
        private void AIGeneralDanageMultiplierNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DamageMultiplier = AIGeneralDanageMultiplierNUD.Value;
            HasChanges();
        }
        private void AIGeneralFormationScaleNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.FormationScale = AIGeneralFormationScaleNUD.Value;
            HasChanges();
        }
        private void NoiseInvestigationDistanceLimitNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NoiseInvestigationDistanceLimit = AIGeneralNoiseInvestigationDistanceLimitNUD.Value;
            HasChanges();
        }
    }
}