using Day2eEditor;
using ExpansionPlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class AIRoamingLocationControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionAIRoamingLocation _data;
        private ExpansionAIRoamingLocation _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public AIRoamingLocationControl()
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
            _data = data as ExpansionAIRoamingLocation ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            NameTB.Text = _data.Name;
            XNUD.Value = (decimal)_data._Position.X;
            YNUD.Value = (decimal)_data._Position.Y;
            ZNUD.Value = (decimal)_data._Position.Z;
            RadiusNUD.Value = (decimal)_data.Radius;
            TypeTB.Text = _data.Type;
            EnabledCB.Checked = _data.Enabled == 1 ? true : false;

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

        private ExpansionAIRoamingLocation CloneData(ExpansionAIRoamingLocation data)
        {
            return new ExpansionAIRoamingLocation
            {
                Name = data.Name,
                Position = data.Position != null ? (float[])data.Position.Clone() : null,
                Radius = data.Radius,
                Type = data.Type,
                Enabled = data.Enabled,
                _Position = data._Position != null ? new Vec3(data._Position.X, data._Position.Y, data._Position.Z) : null
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

        private void EnabledCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Enabled = EnabledCB.Checked == true ? 1 : 0;
            HasChanges();
        }
    }
}