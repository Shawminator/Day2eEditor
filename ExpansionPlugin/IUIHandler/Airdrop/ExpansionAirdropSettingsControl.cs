using Day2eEditor;
using ExpansionPlugin;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionAirdropSettingsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionAirdropSettings _data;
        private ExpansionAirdropSettings _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionAirdropSettingsControl()
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
            _data = data as ExpansionAirdropSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            checkBox1.Checked = _data.ServerMarkerOnDropLocation == 1 ? true : false;
            checkBox2.Checked = _data.Server3DMarkerOnDropLocation == 1 ? true : false;
            checkBox3.Checked = _data.ShowAirdropTypeOnMarker == 1 ? true : false;
            checkBox4.Checked = _data.HeightIsRelativeToGroundLevel == 1 ? true : false;
            checkBox8.Checked = _data.HideCargoWhileParachuteIsDeployed == 1 ? true : false;
            checkBox11.Checked = _data.ExplodeAirVehiclesOnCollision == 1 ? true : false;
            numericUpDown1.Value = (decimal)_data.Height;
            numericUpDown2.Value = (decimal)_data.FollowTerrainFraction;
            numericUpDown3.Value = (decimal)_data.Speed;
            numericUpDown4.Value = (decimal)_data.Radius;
            numericUpDown5.Value = (decimal)_data.InfectedSpawnRadius;
            numericUpDown6.Value = (decimal)_data.InfectedSpawnInterval;
            numericUpDown7.Value = (decimal)_data.ItemCount;
            numericUpDown12.Value = (decimal)_data.DropZoneHeight;
            numericUpDown31.Value = (decimal)_data.DropZoneSpeed;
            numericUpDown33.Value = (decimal)_data.DropZoneProximityDistance;
            textBox1.Text = _data.AirdropPlaneClassName;

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

        private ExpansionAirdropSettings CloneData(ExpansionAirdropSettings data)
        {
            return new ExpansionAirdropSettings
            {
                m_Version = data.m_Version,
                ServerMarkerOnDropLocation = data.ServerMarkerOnDropLocation,
                Server3DMarkerOnDropLocation = data.Server3DMarkerOnDropLocation,
                ShowAirdropTypeOnMarker = data.ShowAirdropTypeOnMarker,
                HideCargoWhileParachuteIsDeployed = data.HideCargoWhileParachuteIsDeployed,
                HeightIsRelativeToGroundLevel = data.HeightIsRelativeToGroundLevel,
                Height = data.Height,
                DropZoneHeight = data.DropZoneHeight,
                FollowTerrainFraction = data.FollowTerrainFraction,
                Speed = data.Speed,
                DropZoneSpeed = data.DropZoneSpeed,
                Radius = data.Radius,
                InfectedSpawnRadius = data.InfectedSpawnRadius,
                InfectedSpawnInterval = data.InfectedSpawnInterval,
                ItemCount = data.ItemCount,
                AirdropPlaneClassName = data.AirdropPlaneClassName,
                DropZoneProximityDistance = data.DropZoneProximityDistance,
                ExplodeAirVehiclesOnCollision = data.ExplodeAirVehiclesOnCollision
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
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ServerMarkerOnDropLocation = checkBox1.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Server3DMarkerOnDropLocation = checkBox1.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ShowAirdropTypeOnMarker = checkBox3.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HeightIsRelativeToGroundLevel = checkBox1.Checked == true ? 1 : 0;
             HasChanges();
        }
        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HideCargoWhileParachuteIsDeployed = checkBox8.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ExplodeAirVehiclesOnCollision = checkBox11.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Height = (decimal)numericUpDown1.Value;
            HasChanges();
        }
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.FollowTerrainFraction = (decimal)numericUpDown2.Value;
            HasChanges();
        }
        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Speed = (decimal)numericUpDown3.Value;
            HasChanges();
        }
        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Radius = (decimal)numericUpDown4.Value;
            HasChanges();
        }
        private void numericUpDown12_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DropZoneHeight = (decimal)numericUpDown12.Value;
            HasChanges();
        }
        private void numericUpDown31_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DropZoneSpeed = (decimal)numericUpDown31.Value;
            HasChanges();
        }
        private void numericUpDown33_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DropZoneProximityDistance = (decimal)numericUpDown33.Value;
            HasChanges();
        }
        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.InfectedSpawnRadius = (decimal)numericUpDown5.Value;
            HasChanges();
        }
        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.InfectedSpawnInterval = (int)numericUpDown6.Value;
            HasChanges();
        }
        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ItemCount = (int)numericUpDown7.Value;
            HasChanges();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AirdropPlaneClassName = textBox1.Text;
            HasChanges();
        }
    }
}