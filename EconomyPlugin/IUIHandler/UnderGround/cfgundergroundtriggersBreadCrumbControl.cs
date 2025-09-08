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
    public partial class cfgundergroundtriggersBreadCrumbControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private Breadcrumb _data;
        private Breadcrumb _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgundergroundtriggersBreadCrumbControl()
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
            _data = data as Breadcrumb ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            CFGUBreadCrumbPositionXNUD.Value = (decimal)_data.Position[0];
            CFGUBreadCrumbPositionYNUD.Value = (decimal)_data.Position[1];
            CFGUBreadCrumbPositionZNUD.Value = (decimal)_data.Position[2];
            CFGUBreadCrumbEyeAccommodationNUD.Value = (decimal)_data.EyeAccommodation;
            if (_data.UseRaycast != null)
            {
                UseRayCastCB.Checked = CFGUBreadCrumbUseRayCastCB.Visible = true;
                CFGUBreadCrumbUseRayCastCB.Checked = _data.UseRaycast == 1 ? true : false;
            }
            else
            {
                UseRayCastCB.Checked = CFGUBreadCrumbUseRayCastCB.Visible = false;
            }
            if (_data.Radius != null)
            {
                UseRadiusCB.Checked = CFGUBreadCrumbRadiusNUD.Visible = true;
                CFGUBreadCrumbRadiusNUD.Value = (decimal)_data.Radius;
            }
            else
            {
                UseLightLerpCB.Checked = CFGUBreadCrumbRadiusNUD.Visible = false;
            }
            if (_data.LightLerp != null)
            {
                UseLightLerpCB.Checked = LightLerpCB.Visible = true;
                LightLerpCB.Checked = _data.LightLerp == 1 ? true : false;
            }
            else
            {
                UseLightLerpCB.Checked = LightLerpCB.Visible = false;
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
        private Breadcrumb CloneData(Breadcrumb data)
        {
            if (data == null) return null;

            return new Breadcrumb
            {
                // Clone the Position array to avoid reference sharing
                Position = data.Position != null ? (decimal[])data.Position.Clone() : null,
                EyeAccommodation = data.EyeAccommodation,
                UseRaycast = data.UseRaycast,
                Radius = data.Radius,
                LightLerp = data.LightLerp
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

        private void UseRayCastCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (UseRayCastCB.Checked)
            {
                CFGUBreadCrumbUseRayCastCB.Visible = true;
                _data.UseRaycast = 1;
                CFGUBreadCrumbUseRayCastCB.Checked = _data.UseRaycast == 1 ? true : false;
            }
            else
            {
                CFGUBreadCrumbUseRayCastCB.Visible = false;
                _data.UseRaycast = null;
            }
            HasChanges();
        }

        private void UseRadiusCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (UseRadiusCB.Checked)
            {
                CFGUBreadCrumbRadiusNUD.Visible = true;
                _data.Radius = -1;
                CFGUBreadCrumbRadiusNUD.Value = (int)_data.Radius;
            }
            else
            {
                CFGUBreadCrumbRadiusNUD.Visible = false;
                _data.Radius = null;
            }
            HasChanges();
        }

        private void UseLightLerpCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (UseLightLerpCB.Checked)
            {
                LightLerpCB.Visible = true;
                _data.LightLerp = 1;
                LightLerpCB.Checked = _data.LightLerp == 1 ? true : false;
            }
            else
            {
                LightLerpCB.Visible = false;
                _data.LightLerp = null;
            }
            HasChanges();
        }

        private void CFGUBreadCrumbPositionXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Position[0] = (decimal)CFGUBreadCrumbPositionXNUD.Value;
            HasChanges();
        }
        private void CFGUBreadCrumbPositionYNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Position[1] = (decimal)CFGUBreadCrumbPositionYNUD.Value;
            HasChanges();
        }
        private void CFGUBreadCrumbPositionZNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Position[2] = (decimal)CFGUBreadCrumbPositionZNUD.Value;
            HasChanges();
        }
        private void CFGUBreadCrumbEyeAccommodationNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EyeAccommodation = (decimal)CFGUBreadCrumbEyeAccommodationNUD.Value;
            HasChanges();
        }
        private void CFGUBreadCrumbUseRayCastCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseRaycast = CFGUBreadCrumbUseRayCastCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void CFGUBreadCrumbRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Radius = (decimal)CFGUBreadCrumbRadiusNUD.Value;
            HasChanges();
        }
        private void LightLerpCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.LightLerp = LightLerpCB.Checked == true ? 1 : 0;
            HasChanges();
        }
    }
}