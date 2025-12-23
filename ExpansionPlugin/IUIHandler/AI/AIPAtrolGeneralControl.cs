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
            if (_data.FormationScale == -1)
            {
                AIGeneralFormationScaleNUD.Visible = false;
                AIGeneralFormationScaleDefaultCB.Checked = true;
            }
            else
            {
                AIGeneralFormationScaleNUD.Visible = true;
                AIGeneralFormationScaleNUD.Value = (decimal)_data.FormationScale;
            }
            if (_data.DespawnTime == -1)
            {
                AIGeneralDespawnTimeNUD.Visible = false;
                AIGeneralDespawnTimeDefaultCB.Checked = true;
            }
            else
            {
                AIGeneralDespawnTimeNUD.Visible = true;
                AIGeneralDespawnTimeNUD.Value = (decimal)_data.DespawnTime;
            }
            if (_data.RespawnTime == -1)
            {
                AIGeneralRespawnTimeNUD.Visible = false;
                AIGeneralRespawnTimeDefaultCB.Checked = true;
            }
            else
            {
                AIGeneralRespawnTimeNUD.Visible = true;
                AIGeneralRespawnTimeNUD.Value = (decimal)_data.RespawnTime;
            }
            if (_data.MinDistRadius == -1)
            {
                AIGeneralMinDistRadiusNUD.Visible = false;
                AIGeneralMinDistRadiusDefaultCB.Checked = true;
            }
            else
            {
                AIGeneralMinDistRadiusNUD.Visible = true;
                AIGeneralMinDistRadiusNUD.Value = (decimal)_data.MinDistRadius;
            }
            if (_data.MaxDistRadius == -1)
            {
                AIGeneralMaxDistRadiusNUD.Visible = false;
                AIGeneralMaxDistRadiusDefaultCB.Checked = true;
            }
            else
            {
                AIGeneralMaxDistRadiusNUD.Visible = true;
                AIGeneralMaxDistRadiusNUD.Value = (decimal)_data.MaxDistRadius;
            }
            AIGeneralDespawnRadiusNUD.Value = (decimal)_data.DespawnRadius;
            if (_data.AccuracyMin == -1)
            {
                AIGeneralAccuracyMinNUD.Visible = false;
                AIGeneralAccuracyMinDefaultCB.Checked = true;
            }
            else
            {
                AIGeneralAccuracyMinNUD.Visible = true;
                AIGeneralAccuracyMinNUD.Value = (decimal)_data.AccuracyMin;
            }
            if (_data.AccuracyMax == -1)
            {
                AIGeneralAccuracyMaxNUD.Visible = false;
                AIGeneralAccuracyMaxDefaultCB.Checked = true;
            }
            else
            {
                AIGeneralAccuracyMaxNUD.Visible = true;
                AIGeneralAccuracyMaxNUD.Value = (decimal)_data.AccuracyMax;
            }
            if (_data.ThreatDistanceLimit == -1)
            {
                AIGeneralThreatDistanceLimitNUD.Visible = false;
                AIGeneralThreatDistanceLimitDefaultCB.Checked = true;
            }
            else
            {
                AIGeneralThreatDistanceLimitNUD.Visible = true;
                AIGeneralThreatDistanceLimitNUD.Value = (decimal)_data.ThreatDistanceLimit;
            }
            if (_data.NoiseInvestigationDistanceLimit == -1)
            {
                AIGeneralNoiseInvestigationDistanceLimitNUD.Visible = false;
                AIGeneralNoiseInvestigationDistanceLimitDewfaultCB.Checked = true;
            }
            else
            {
                AIGeneralNoiseInvestigationDistanceLimitNUD.Visible = true;
                AIGeneralNoiseInvestigationDistanceLimitNUD.Value = (decimal)_data.NoiseInvestigationDistanceLimit;
            }
            if (_data.MaxFlankingDistance == -1)
            {
                AIGeneralMaxFlankingDistanceNUD.Visible = false;
                AIGeneralMaxFlankingDistanceDefaultCB.Checked = true;
            }
            else
            {
                AIGeneralMaxFlankingDistanceNUD.Visible = true;
                AIGeneralMaxFlankingDistanceNUD.Value = (decimal)_data.MaxFlankingDistance;
            }
            if (_data.EnableFlankingOutsideCombat == -1)
            {
                AIGeneralEnableFlankingOutsideCombatCB.Visible = false;
                AIGeneralEnableFlankingOutsideCombatDefailtCB.Checked = true;
            }
            else
            {
                AIGeneralEnableFlankingOutsideCombatCB.Visible = true;
                AIGeneralEnableFlankingOutsideCombatCB.Checked = _data.EnableFlankingOutsideCombat == 1 ? true : false;
            }
            if (_data.DamageMultiplier == -1)
            {
                AIGeneralDanageMultiplierNUD.Visible = false;
                AIGeneralDanageMultiplierDefaultCB.Checked = true;
            }
            else
            {
                AIGeneralDanageMultiplierNUD.Visible = true;
                AIGeneralDanageMultiplierNUD.Value = (decimal)_data.DamageMultiplier;
            }
            if (_data.DamageReceivedMultiplier == -1)
            {
                AIGeneralDamageReceivedMultiplierNUD.Visible = false;
                AIGeneralDamageReceivedMultiplierDefaultCB.Checked = true;
            }
            else
            {
                AIGeneralDamageReceivedMultiplierNUD.Visible = true;
                AIGeneralDamageReceivedMultiplierNUD.Value = (decimal)_data.DamageReceivedMultiplier;
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
                MaxFlankingDistance = data.MaxFlankingDistance,
                EnableFlankingOutsideCombat = data.EnableFlankingOutsideCombat,
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
                            DefaultStance = p.DefaultStance,
                            DefaultLookAngle = p.DefaultLookAngle,
                            CanBeLooted = p.CanBeLooted,
                            LootDropOnDeath = p.LootDropOnDeath,
                            UnlimitedReload = p.UnlimitedReload,
                            SniperProneDistanceThreshold = p.SniperProneDistanceThreshold,
                            AccuracyMin = p.AccuracyMin,
                            AccuracyMax = p.AccuracyMax,
                            ThreatDistanceLimit = p.ThreatDistanceLimit,
                            NoiseInvestigationDistanceLimit = p.NoiseInvestigationDistanceLimit,
                            MaxFlankingDistance = p.MaxFlankingDistance,
                            EnableFlankingOutsideCombat = p.EnableFlankingOutsideCombat,
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

        private void AIGeneralEnabledCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Enabled = AIGeneralEnabledCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void AIGeneralFormationScaleNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.FormationScale = AIGeneralFormationScaleNUD.Value;
            HasChanges();
        }
        private void AIGeneralFormationScaleDefaultCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (AIGeneralFormationScaleDefaultCB.Checked)
            {
                AIGeneralFormationScaleNUD.Visible = false;
                _data.FormationScale = -1;
            }
            else
            {
                AIGeneralFormationScaleNUD.Visible = true;
                _data.FormationScale = AIGeneralFormationScaleNUD.Value;
            }
            HasChanges();
        }
        private void AIGeneralDespawnTimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DespawnTime = AIGeneralDespawnTimeNUD.Value;
            HasChanges();
        }
        private void AIGeneralDespawnTimeDefaultCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (AIGeneralDespawnTimeDefaultCB.Checked)
            {
                AIGeneralDespawnTimeNUD.Visible = false;
                _data.DespawnTime = -1;
            }
            else
            {
                AIGeneralDespawnTimeNUD.Visible = true;
                _data.DespawnTime = AIGeneralDespawnTimeNUD.Value;
            }
            HasChanges();
        }
        private void AIGeneralRespawnTimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.RespawnTime = AIGeneralRespawnTimeNUD.Value;
            HasChanges();
        }
        private void AIGeneralRespawnTimeDefaultCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (AIGeneralRespawnTimeDefaultCB.Checked)
            {
                AIGeneralRespawnTimeNUD.Visible = false;
                _data.RespawnTime = -1;
            }
            else
            {
                AIGeneralRespawnTimeNUD.Visible = true;
                _data.RespawnTime = AIGeneralRespawnTimeNUD.Value;
            }
            HasChanges();
        }
        private void AIGeneralMinDistRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MinDistRadius = AIGeneralMinDistRadiusNUD.Value;
            HasChanges();
        }
        private void AIGeneralMinDistRadiusDefaultCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (AIGeneralMinDistRadiusDefaultCB.Checked)
            {
                AIGeneralMinDistRadiusNUD.Visible = false;
                _data.MinDistRadius = -1;
            }
            else
            {
                AIGeneralMinDistRadiusNUD.Visible = true;
                _data.MinDistRadius = AIGeneralMinDistRadiusNUD.Value;
            }
            HasChanges();
        }
        private void AIGeneralMaxDistRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxDistRadius = AIGeneralMaxDistRadiusNUD.Value;
            HasChanges();
        }
        private void AIGeneralMaxDistRadiusDefaultCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (AIGeneralMaxDistRadiusDefaultCB.Checked)
            {
                AIGeneralMaxDistRadiusNUD.Visible = false;
                _data.MaxDistRadius = -1;
            }
            else
            {
                AIGeneralMaxDistRadiusNUD.Visible = true;
                _data.MaxDistRadius = AIGeneralMaxDistRadiusNUD.Value;
            }
            HasChanges();
        }
        private void AIGeneralDespawnRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DespawnRadius = AIGeneralDespawnRadiusNUD.Value;
            HasChanges();
        }
        private void AIGeneralAccuracyMinNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AccuracyMin = AIGeneralAccuracyMinNUD.Value;
            HasChanges();
        }
        private void AIGeneralAccuracyMinDefaultCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (AIGeneralAccuracyMinDefaultCB.Checked)
            {
                AIGeneralAccuracyMinNUD.Visible = false;
                _data.AccuracyMin = -1;
            }
            else
            {
                AIGeneralAccuracyMinNUD.Visible = true;
                _data.AccuracyMin = AIGeneralAccuracyMinNUD.Value;
            }
            HasChanges();
        }
        private void AIGenralAccuracyMaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AccuracyMax = AIGeneralAccuracyMaxNUD.Value;
            HasChanges();
        }
        private void AIGeneralAccuracyMaxDefaultCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (AIGeneralAccuracyMaxDefaultCB.Checked)
            {
                AIGeneralAccuracyMaxNUD.Visible = false;
                _data.AccuracyMax = -1;
            }
            else
            {
                AIGeneralAccuracyMaxNUD.Visible = true;
                _data.AccuracyMax = AIGeneralAccuracyMaxNUD.Value;
            }
            HasChanges();
        }
        private void AIGeneralThreatDistanceLimitNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ThreatDistanceLimit = AIGeneralThreatDistanceLimitNUD.Value;
            HasChanges();
        }
        private void AIGeneralThreatDistanceLimitDefaultCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (AIGeneralThreatDistanceLimitDefaultCB.Checked)
            {
                AIGeneralThreatDistanceLimitNUD.Visible = false;
                _data.ThreatDistanceLimit = -1;
            }
            else
            {
                AIGeneralThreatDistanceLimitNUD.Visible = true;
                _data.ThreatDistanceLimit = AIGeneralThreatDistanceLimitNUD.Value;
            }
            HasChanges();
        }
        private void NoiseInvestigationDistanceLimitNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NoiseInvestigationDistanceLimit = AIGeneralNoiseInvestigationDistanceLimitNUD.Value;
            HasChanges();
        }
        private void AIGeneralNoiseInvestigationDistanceLimitDewfaultCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (AIGeneralNoiseInvestigationDistanceLimitDewfaultCB.Checked)
            {
                AIGeneralNoiseInvestigationDistanceLimitNUD.Visible = false;
                _data.NoiseInvestigationDistanceLimit = -1;
            }
            else
            {
                AIGeneralNoiseInvestigationDistanceLimitNUD.Visible = true;
                _data.NoiseInvestigationDistanceLimit = AIGeneralNoiseInvestigationDistanceLimitNUD.Value;
            }
            HasChanges();
        }
        private void AIGeneralMaxFlankingDistanceNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxFlankingDistance = AIGeneralMaxFlankingDistanceNUD.Value;
            HasChanges();
        }
        private void AIGeneralMaxFlankingDistanceDefaultCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (AIGeneralMaxFlankingDistanceDefaultCB.Checked)
            {
                AIGeneralMaxFlankingDistanceNUD.Visible = false;
                _data.MaxFlankingDistance = -1;
            }
            else
            {
                AIGeneralMaxFlankingDistanceNUD.Visible = true;
                _data.MaxFlankingDistance = AIGeneralMaxFlankingDistanceNUD.Value;
            }
            HasChanges();
        }
        private void AIGeneralEnableFlankingOutsideCombatCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableFlankingOutsideCombat = AIGeneralEnableFlankingOutsideCombatCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void AIGeneralEnableFlankingOutsideCombatDefailtCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (AIGeneralEnableFlankingOutsideCombatDefailtCB.Checked)
            {
                AIGeneralEnableFlankingOutsideCombatCB.Visible = false;
                _data.EnableFlankingOutsideCombat = -1;
            }
            else
            {
                AIGeneralEnableFlankingOutsideCombatCB.Visible = true;
                _data.EnableFlankingOutsideCombat = AIGeneralEnableFlankingOutsideCombatCB.Checked == true ? 1 : 0;
            }
            HasChanges();
        }
        private void AIGeneralDanageMultiplierNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DamageMultiplier = AIGeneralDanageMultiplierNUD.Value;
            HasChanges();
        }
        private void AIGeneralDanageMultiplierDefaultCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (AIGeneralDanageMultiplierDefaultCB.Checked)
            {
                AIGeneralDanageMultiplierNUD.Visible = false;
                _data.DamageMultiplier = -1;
            }
            else
            {
                AIGeneralDanageMultiplierNUD.Visible = true;
                _data.DamageMultiplier = AIGeneralDanageMultiplierNUD.Value;
            }
            HasChanges();
        }
        private void AIGeneralDamageReceivedMultiplierNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DamageReceivedMultiplier = AIGeneralDamageReceivedMultiplierNUD.Value;
            HasChanges();
        }
        private void AIGeneralDamageReceivedMultiplierDefaultCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (AIGeneralDamageReceivedMultiplierDefaultCB.Checked)
            {
                AIGeneralDamageReceivedMultiplierNUD.Visible = false;
                _data.DamageReceivedMultiplier = -1;
            }
            else
            {
                AIGeneralDamageReceivedMultiplierNUD.Visible = true;
                _data.DamageReceivedMultiplier = AIGeneralDamageReceivedMultiplierNUD.Value;
            }
            HasChanges();
        }
    }
}