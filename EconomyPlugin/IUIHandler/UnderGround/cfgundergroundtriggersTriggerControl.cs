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
        private Trigger _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgundergroundtriggersTriggerControl()
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
            _data = data as Trigger ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

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
        private Trigger CloneData(Trigger data)
        {
            if (data == null) return null;

            return new Trigger
            {
                // Clone Position array
                Position = data.Position != null ? (decimal[])data.Position.Clone() : null,

                // Clone Size array
                Size = data.Size != null ? (decimal[])data.Size.Clone() : null,

                // Copy other properties
                EyeAccommodation = data.EyeAccommodation,
                UseLinePointFade = data.UseLinePointFade,
                AmbientSoundType = data.AmbientSoundType,
                InterpolationSpeed = data.InterpolationSpeed

                // Breadcrumbs list intentionally ignored
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
            HasChanges();
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
            HasChanges();
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
            HasChanges();
        }
    }
}