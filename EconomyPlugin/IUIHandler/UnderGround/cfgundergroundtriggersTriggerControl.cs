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

            CFGUTriggerPositionXNUD.Value = (decimal)_data.Position.X;
            CFGUTriggerPositionYNUD.Value = (decimal)_data.Position.Y;
            CFGUTriggerPositionZNUD.Value = (decimal)_data.Position.Z;
            CFGUTriggerOrientationXNUD.Value = (decimal)_data.Orientation.X;
            CFGUTriggerOrientationYNUD.Value = (decimal)_data.Orientation.Y;
            CFGUTriggerOrientationZNUD.Value = (decimal)_data.Orientation.Z;
            CFGUTriggerSizeXNUD.Value = (decimal)_data.Size.X;
            CFGUTriggerSizeYNUD.Value = (decimal)_data.Size.Y;
            CFGUTriggerSizeZNUD.Value = (decimal)_data.Size.Z;
            CFGUTriggerEyeAccommodationNUD.Value = (decimal)_data.EyeAccommodation;
            if (_data.InterpolationSpeed != null)
            {
                UseInterpolationSpeedCB.Checked = CFGUTriggerInterpolationSpeedNUD.Visible = true;
                CFGUTriggerInterpolationSpeedNUD.Value = (decimal)_data.InterpolationSpeed;
            }
            else
            {
                UseInterpolationSpeedCB.Checked = CFGUTriggerInterpolationSpeedNUD.Visible = false;
            }
            if (_data.UseLinePointFade != null)
            {
                UseUseLinePointFadeCB.Checked = UseLInePointFadeCB.Visible = true;
                UseLInePointFadeCB.Checked = _data.UseLinePointFade == 1 ? true : false;
            }
            else
            {
                UseUseLinePointFadeCB.Checked = UseLInePointFadeCB.Visible = false;
            }
            if (_data.AmbientSoundType != null)
            {
                UseAmbientSoundTypeCB.Checked = AmbientSoundTypeTB.Visible = true;
                AmbientSoundTypeTB.Text = _data.AmbientSoundType;
            }
            else
            {
                AmbientSoundTypeTB.Visible = false;
            }
            if (_data.AmbientSoundSet != null)
            {
                checkBox1.Checked = AmbientSoundSetTB.Visible = true;
                AmbientSoundSetTB.Text = _data.AmbientSoundSet;
            }
            else
            {
                checkBox1.Checked = AmbientSoundSetTB.Visible = false;
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

        private void CFGUTriggerPositionXNUD_ValueChanged(object sender, EventArgs e)
        {

        }
        private void CFGUTriggerPositionYNUD_ValueChanged(object sender, EventArgs e)
        {

        }
        private void CFGUTriggerPositionZNUD_ValueChanged(object sender, EventArgs e)
        {

        }
        private void CFGUTriggerOrientationXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Orientation.X = (float)CFGUTriggerOrientationXNUD.Value;

        }
        private void CFGUTriggerOrientationYNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Orientation.Y = (float)CFGUTriggerOrientationYNUD.Value;
        }
        private void CFGUTriggerOrientationZNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Orientation.Z = (float)CFGUTriggerOrientationZNUD.Value;
        }
        private void CFGUTriggerSizeXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Size.X = (float)CFGUTriggerSizeXNUD.Value;
        }
        private void CFGUTriggerSizeYNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Size.Y = (float)CFGUTriggerSizeYNUD.Value;
        }
        private void CFGUTriggerSizeZNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Size.Z = (float)CFGUTriggerSizeZNUD.Value;
        }
        private void CFGUTriggerEyeAccommodationNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EyeAccommodation = CFGUTriggerEyeAccommodationNUD.Value;
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
        private void CFGUTriggerInterpolationSpeedNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.InterpolationSpeed = CFGUTriggerInterpolationSpeedNUD.Value;
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
        private void UseLInePointFadeCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseLinePointFade = UseLInePointFadeCB.Checked == true ? 1 : 0;
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
        private void AmbientSoundTypeTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AmbientSoundType = AmbientSoundTypeTB.Text;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (checkBox1.Checked)
            {
                AmbientSoundSetTB.Visible = true;
                _data.AmbientSoundSet = "Change Me";
                AmbientSoundSetTB.Text = _data.AmbientSoundType;
            }
            else
            {
                AmbientSoundSetTB.Visible = false;
                _data.AmbientSoundType = null;
            }
        }
        private void AmbientSoundSetTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AmbientSoundSet = AmbientSoundSetTB.Text;
        }

        private void UseCustopmSpawnCB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CustomSpawnCB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void UseCommentCB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CommentTB_TextChanged(object sender, EventArgs e)
        {

        }
    }
}