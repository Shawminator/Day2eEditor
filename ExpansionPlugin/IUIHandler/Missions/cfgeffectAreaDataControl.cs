using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class cfgeffectAreaDataControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private Data _data;
        private Data _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgeffectAreaDataControl()
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
            _data = data as Data ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            PosXNUD.Value = (decimal)_data.Pos[0];
            posYNUD.Value = (decimal)_data.Pos[1];
            posZNUD.Value = (decimal)_data.Pos[2];
            RadiusNUD.Value = (decimal)_data.Radius;
            PosHeightNUD.Value = (decimal)_data.PosHeight;
            NegHeightNUD.Value = (decimal)_data.NegHeight;


            if (UseInnerRingCountCB.Checked = _data.InnerRingCount != null)
                InnerRingCountNUD.Value = (decimal)_data.InnerRingCount;
            if (UseInnerPartDistCB.Checked = _data.InnerPartDist != null)
                InnerPartDistNUD.Value = (decimal)_data.InnerPartDist;
            if (UseOuterRingToggleCB.Checked = _data.OuterRingToggle != null)
                OuterRingToggleCB.Checked = (bool)_data.OuterRingToggle;
            if (UseOuterPartDistCB.Checked = _data.OuterPartDist != null)
                OuterPartDistNUD.Value = (decimal)_data.OuterPartDist;
            if (UseOuterOffsetCB.Checked = _data.OuterOffset != null)
                OuterOffsetNUD.Value = (decimal)_data.OuterOffset;
            if (UseVerticalLayersCB.Checked = _data.VerticalLayers != null)
                VerticalLayersNUD.Value = (decimal)_data.VerticalLayers;
            if (UseVerticalOffsetCB.Checked = _data.VerticalOffset != null)
                VerticalOffsetNUD.Value = (decimal)_data.VerticalOffset;
            if (UseParticleNameCB.Checked = _data.ParticleName != null)
                ParticleNameTB.Text = _data.ParticleName;
            if (UseEffectIntervalCB.Checked = _data.EffectInterval != null)
                EffectIntervalNUD.Value = (decimal)_data.EffectInterval;
            if (UseEffectDurationCB.Checked = _data.EffectDuration != null)
                EffectDurationNUD.Value = (decimal)_data.EffectDuration;
            if (UseEffectModifierCB.Checked = _data.EffectModifier != null)
                EffectModifierCB.Checked = (bool)_data.EffectModifier;

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
        private Data CloneData(Data data)
        {
            return new Data
            {
                Pos = data.Pos?.ToArray(),
                Radius = data.Radius,
                PosHeight = data.PosHeight,
                NegHeight = data.NegHeight,
                InnerRingCount = data.InnerRingCount,
                InnerPartDist = data.InnerPartDist,
                OuterRingToggle = data.OuterRingToggle,
                OuterPartDist = data.OuterPartDist,
                OuterOffset = data.OuterOffset,
                VerticalLayers = data.VerticalLayers,
                VerticalOffset = data.VerticalOffset,
                ParticleName = data.ParticleName,
                EffectInterval = data.EffectInterval,
                EffectDuration = data.EffectDuration,
                EffectModifier = data.EffectModifier
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

        #region usecases
        private void UseInnerRingCountCB_CheckedChanged(object sender, EventArgs e)
        {
            InnerRingCountNUD.Visible = UseInnerRingCountCB.Checked;
            if (_suppressEvents) return;
            if (UseInnerRingCountCB.Checked)
                _data.InnerRingCount = 0;
            else
                _data.InnerRingCount = null;
            HasChanges();
        }
        private void UseInnerPartDistCB_CheckedChanged(object sender, EventArgs e)
        {
            InnerPartDistNUD.Visible = UseInnerPartDistCB.Checked;
            if (_suppressEvents) return;
            if (UseInnerPartDistCB.Checked)
                _data.InnerPartDist = 0;
            else
                _data.InnerPartDist = null;
            HasChanges();
        }
        private void UseOuterRingToggleCB_CheckedChanged(object sender, EventArgs e)
        {
            OuterRingToggleCB.Visible = UseOuterRingToggleCB.Checked;
            if (_suppressEvents) return;
            if (UseOuterRingToggleCB.Checked)
                _data.OuterRingToggle = false;
            else
                _data.OuterRingToggle = null;
            HasChanges();
        }
        private void UseOuterPartDistCB_CheckedChanged(object sender, EventArgs e)
        {
            OuterPartDistNUD.Visible = UseOuterPartDistCB.Checked;
            if (_suppressEvents) return;
            if (UseOuterPartDistCB.Checked)
                _data.OuterPartDist = 0;
            else
                _data.OuterPartDist = null;
            HasChanges();
        }
        private void UseOuterOffsetCB_CheckedChanged(object sender, EventArgs e)
        {
            OuterOffsetNUD.Visible = UseOuterOffsetCB.Checked;
            if (_suppressEvents) return;
            if (UseOuterOffsetCB.Checked)
                _data.OuterOffset = 0;
            else
                _data.OuterOffset = null;
            HasChanges();
        }
        private void UseVerticalLayersCB_CheckedChanged(object sender, EventArgs e)
        {
            VerticalLayersNUD.Visible = UseVerticalLayersCB.Checked;
            if (_suppressEvents) return;
            if (UseVerticalLayersCB.Checked)
                _data.VerticalLayers = 0;
            else
                _data.VerticalLayers = null;
            HasChanges();
        }
        private void UseVerticalOffsetCB_CheckedChanged(object sender, EventArgs e)
        {
            VerticalOffsetNUD.Visible = UseVerticalOffsetCB.Checked;
            if (_suppressEvents) return;
            if (UseVerticalOffsetCB.Checked)
                _data.VerticalOffset = 0;
            else
                _data.VerticalOffset = null;
            HasChanges();
        }
        private void UseParticleNameCB_CheckedChanged(object sender, EventArgs e)
        {
            ParticleNameTB.Visible = UseParticleNameCB.Checked;
            if (_suppressEvents) return;
            if (UseParticleNameCB.Checked)
                _data.ParticleName = "";
            else
                _data.ParticleName = null;
            HasChanges();
        }
        private void UseEffectIntervalCB_CheckedChanged(object sender, EventArgs e)
        {
            EffectIntervalNUD.Visible = UseEffectIntervalCB.Checked;
            if (_suppressEvents) return;
            if (UseEffectIntervalCB.Checked)
                _data.EffectInterval = 0;
            else
                _data.EffectInterval = null;
            HasChanges();
        }
        private void UseEffectDurationCB_CheckedChanged(object sender, EventArgs e)
        {
            EffectDurationNUD.Visible = UseEffectDurationCB.Checked;
            if (_suppressEvents) return;
            if (UseEffectDurationCB.Checked)
                _data.EffectDuration = 0;
            else
                _data.EffectDuration = null;
            HasChanges();
        }
        private void UseEffectModifierCB_CheckedChanged(object sender, EventArgs e)
        {
            EffectModifierCB.Visible = UseEffectModifierCB.Checked;
            if (_suppressEvents) return;
            if (UseEffectModifierCB.Checked)
                _data.EffectModifier = false;
            else
                _data.EffectModifier = null;
            HasChanges();
        }
        #endregion usecases

        private void PosNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Pos = new decimal[] { (decimal)PosXNUD.Value, (decimal)posYNUD.Value, (decimal)posZNUD.Value };
            HasChanges();
        }
        private void AreaNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            NumericUpDown nud = sender as NumericUpDown;
            if (nud.DecimalPlaces > 0)
               _data.SetdecimalValue(nud.Name.Substring(0, nud.Name.Length - 3), (decimal)nud.Value);
            else
                _data.SetIntValue(nud.Name.Substring(0, nud.Name.Length - 3), (int)nud.Value);
            HasChanges();
        }
        private void OuterRingToggleCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.OuterRingToggle = OuterRingToggleCB.Checked;
            HasChanges();
        }
        private void EffectIntervalNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EffectInterval = (int)EffectIntervalNUD.Value;
            HasChanges();
        }
        private void EffectDurationNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EffectDuration = (int)EffectDurationNUD.Value;
            HasChanges();
        }
        private void ParticleNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ParticleName = ParticleNameTB.Text;
            HasChanges();
        }
        private void EffectModifierCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EffectModifier = EffectModifierCB.Checked;
            HasChanges();
        }
    }
}