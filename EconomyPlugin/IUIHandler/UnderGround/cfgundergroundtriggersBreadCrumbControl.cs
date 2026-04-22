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
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgundergroundtriggersBreadCrumbControl()
        {
            InitializeComponent();
        }

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as Breadcrumb ?? throw new InvalidCastException();
            _nodes = selectedNodes;

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
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                // TODO: Update _nodes.Last().Text based on _data
            }
        }
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
        }
        private void CFGUBreadCrumbPositionXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Position[0] = (decimal)CFGUBreadCrumbPositionXNUD.Value;
        }
        private void CFGUBreadCrumbPositionYNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Position[1] = (decimal)CFGUBreadCrumbPositionYNUD.Value;
        }
        private void CFGUBreadCrumbPositionZNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Position[2] = (decimal)CFGUBreadCrumbPositionZNUD.Value;
        }
        private void CFGUBreadCrumbEyeAccommodationNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EyeAccommodation = (decimal)CFGUBreadCrumbEyeAccommodationNUD.Value;
        }
        private void CFGUBreadCrumbUseRayCastCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseRaycast = CFGUBreadCrumbUseRayCastCB.Checked == true ? 1 : 0;
        }
        private void CFGUBreadCrumbRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Radius = (decimal)CFGUBreadCrumbRadiusNUD.Value;
        }
        private void LightLerpCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.LightLerp = LightLerpCB.Checked == true ? 1 : 0;
        }
    }
}