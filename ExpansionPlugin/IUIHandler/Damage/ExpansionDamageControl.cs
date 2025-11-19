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
    public partial class ExpansionDamageControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionDamageSystemSettings _data;
        private ExpansionDamageSystemSettings _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionDamageControl()
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
            _data = data as ExpansionDamageSystemSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            DSEnabledCB.Checked = _data.Enabled == 1 ? true : false;
            CheckForBlockingObjectsCB.Checked = _data.CheckForBlockingObjects == 1 ? true : false;

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

        private ExpansionDamageSystemSettings CloneData(ExpansionDamageSystemSettings data)
        {
            if (data == null)
                return null;

            var clone = new ExpansionDamageSystemSettings
            {
                m_Version = data.m_Version,
                Enabled = data.Enabled,
                CheckForBlockingObjects = data.CheckForBlockingObjects,
                ExplosionTargets = data.ExplosionTargets != null
                    ? new BindingList<string>(data.ExplosionTargets.ToList())
                    : new BindingList<string>(),
                _ExplosiveProjectiles = data._ExplosiveProjectiles != null
                    ? new BindingList<ExplosiveProjectiles>(
                        data._ExplosiveProjectiles.Select(ep => new ExplosiveProjectiles
                        {
                            explosion = ep.explosion,
                            ammo = ep.ammo
                        }).ToList())
                    : new BindingList<ExplosiveProjectiles>()
            };
            return clone;
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

        private void DSEnabledCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Enabled = DSEnabledCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void CheckForBlockingObjectsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CheckForBlockingObjects = CheckForBlockingObjectsCB.Checked == true ? 1 : 0;
            HasChanges();
        }
    }
}