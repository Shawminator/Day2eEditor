using CoreUI.Forms;
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
    public partial class cfggameplayUIDataControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private Uidata _data;
        private Uidata _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfggameplayUIDataControl()
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
            _data = data as Uidata ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            use3DMapCB.Checked = _data.use3DMap;
            hitDirectionOverrideEnabledCB.Checked = _data.HitIndicationData.hitDirectionOverrideEnabled;
            hitDirectionBehaviourCB.SelectedIndex = _data.HitIndicationData.hitDirectionBehaviour;
            hitDirectionStyleCB.SelectedIndex = _data.HitIndicationData.hitDirectionStyle;
            hitDirectionMaxDurationNUD.Value = (decimal)_data.HitIndicationData.hitDirectionMaxDuration;
            hitDirectionBreakPointRelativeNUD.Value = (decimal)_data.HitIndicationData.hitDirectionBreakPointRelative;
            hitDirectionScatterNUD.Value = (decimal)_data.HitIndicationData.hitDirectionScatter;
            hitIndicationPostProcessEnabledCB.Checked = _data.HitIndicationData.hitIndicationPostProcessEnabled;

            Color selectedColor = ColorTranslator.FromHtml(_data.HitIndicationData.hitDirectionIndicatorColorStr);
            m_Color.BackColor = selectedColor;

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
        private Uidata CloneData(Uidata data)
        {
            return new Uidata
            {
                use3DMap = data.use3DMap,
                HitIndicationData = new Hitindicationdata
                {
                    hitDirectionOverrideEnabled = data.HitIndicationData.hitDirectionOverrideEnabled,
                    hitDirectionBehaviour = data.HitIndicationData.hitDirectionBehaviour,
                    hitDirectionStyle = data.HitIndicationData.hitDirectionStyle,
                    hitDirectionIndicatorColorStr = data.HitIndicationData.hitDirectionIndicatorColorStr,
                    hitDirectionMaxDuration = data.HitIndicationData.hitDirectionMaxDuration,
                    hitDirectionBreakPointRelative = data.HitIndicationData.hitDirectionBreakPointRelative,
                    hitDirectionScatter = data.HitIndicationData.hitDirectionScatter,
                    hitIndicationPostProcessEnabled = data.HitIndicationData.hitIndicationPostProcessEnabled
                }
            };

        }

        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() != true)
            {
                // TODO: Update _nodes.Last().Text based on _data
            }
        }

        #endregion

        private void use3DMapCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.use3DMap = use3DMapCB.Checked;
            HasChanges();
        }
        private void hitDirectionOverrideEnabledCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HitIndicationData.hitDirectionOverrideEnabled = hitDirectionOverrideEnabledCB.Checked;
            HasChanges();
        }
        private void hitDirectionBehaviourCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HitIndicationData.hitDirectionBehaviour = hitDirectionBehaviourCB.SelectedIndex;
            HasChanges();
        }
        private void hitDirectionStyleCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HitIndicationData.hitDirectionStyle = hitDirectionStyleCB.SelectedIndex;
            HasChanges();
        }
        private void hitDirectionMaxDurationNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HitIndicationData.hitDirectionMaxDuration = Math.Round(hitDirectionMaxDurationNUD.Value, 2);
            HasChanges();
        }
        private void hitDirectionBreakPointRelativeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HitIndicationData.hitDirectionBreakPointRelative = Math.Round(hitDirectionBreakPointRelativeNUD.Value, 2);
            HasChanges();
        }
        private void hitDirectionScatterNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HitIndicationData.hitDirectionScatter = Math.Round(hitDirectionScatterNUD.Value, 2);
            HasChanges();
        }
        private void hitIndicationPostProcessEnabledCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HitIndicationData.hitIndicationPostProcessEnabled = hitIndicationPostProcessEnabledCB.Checked;
            HasChanges();
        }
        private void m_Color_Click(object sender, EventArgs e)
        {

            Color initialColor = ColorTranslator.FromHtml(_data.HitIndicationData.hitDirectionIndicatorColorStr);
            using (AdvancedColorPickerForm picker = new AdvancedColorPickerForm(initialColor))
            {
                picker.StartPosition = FormStartPosition.CenterParent;
                if (picker.ShowDialog() == DialogResult.OK)
                {
                    string colorHex = picker.SelectedColorHex;
                    _data.HitIndicationData.hitDirectionIndicatorColorStr = colorHex;
                    Color selectedColor = ColorTranslator.FromHtml(_data.HitIndicationData.hitDirectionIndicatorColorStr);
                    m_Color.BackColor = selectedColor;
                    HasChanges();
                }
            }
        }
    }
}