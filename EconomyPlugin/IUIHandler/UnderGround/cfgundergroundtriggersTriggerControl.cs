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
    public partial class cfgundergroundtriggersTriggerControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private Trigger _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgundergroundtriggersTriggerControl()
        {
            InitializeComponent();
        }

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as Trigger ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            CFGUTriggerPositionXNUD.Value = (decimal)_data.Position[0];
            CFGUTriggerPositionYNUD.Value = (decimal)_data.Position[1];
            CFGUTriggerPositionZNUD.Value = (decimal)_data.Position[2];
            CFGUTriggerOrientationXNUD.Value = (decimal)_data.Orientation[0];
            CFGUTriggerOrientationYNUD.Value = (decimal)_data.Orientation[1];
            CFGUTriggerOrientationZNUD.Value = (decimal)_data.Orientation[2];
            CFGUTriggerSizeXNUD.Value = (decimal)_data.Size[0];
            CFGUTriggerSizeYNUD.Value = (decimal)_data.Size[1];
            CFGUTriggerSizeZNUD.Value = (decimal)_data.Size[2];
            CFGUTriggerEyeAccommodationNUD.Value = (decimal)_data.EyeAccommodation;
            if (_data.InterpolationSpeed != null)
            {
                UseInterpolationSpeedCB.Checked =  CFGUTriggerInterpolationSpeedNUD.Visible = true;
                CFGUTriggerInterpolationSpeedNUD.Value = (decimal)_data.InterpolationSpeed;
            }
            else
            {
                CFGUTriggerInterpolationSpeedNUD.Visible = false;
            }
            if (_data.UseLinePointFade != null)
            {
                UseUseLinePointFadeCB.Checked =  UseLInePointFadeCB.Visible = true;
                UseLInePointFadeCB.Checked = _data.UseLinePointFade == 1 ? true : false;
            }
            else
            {
                UseLInePointFadeCB.Visible = false;
            }
            if (_data.AmbientSoundType != null)
            {
                UseAmbientSoundTypeCB.Checked =  AmbientSoundTypeTB.Visible = true;
                AmbientSoundTypeTB.Text = _data.AmbientSoundType;
            }
            else
            {
                AmbientSoundTypeTB.Visible = false;
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
        private void UseInterpolationSpeedCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (UseInterpolationSpeedCB.Checked)
            {
                CFGUTriggerInterpolationSpeedNUD.Visible = true;
                _data.InterpolationSpeed = 1;
                CFGUTriggerInterpolationSpeedNUD.Value = (int)_data.InterpolationSpeed;
            }
            else
            {
                CFGUTriggerInterpolationSpeedNUD.Visible = false;
                _data.InterpolationSpeed = null;
            }
        }
        private void UseUseLinePointFadeCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (UseUseLinePointFadeCB.Checked)
            {
                UseLInePointFadeCB.Visible = true;
                _data.UseLinePointFade = 1;
                UseLInePointFadeCB.Checked = _data.UseLinePointFade == 1 ? true : false;
            }
            else
            {
                UseLInePointFadeCB.Visible = false;
                _data.UseLinePointFade = null;
            }
        }
        private void UseAmbientSoundTypeCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (UseAmbientSoundTypeCB.Checked)
            {
                AmbientSoundTypeTB.Visible = true;
                _data.AmbientSoundType = "Change Me";
                AmbientSoundTypeTB.Text = _data.AmbientSoundType;
            }
            else
            {
                AmbientSoundTypeTB.Visible = false;
                _data.AmbientSoundType = null;
            }
        }
    }
}