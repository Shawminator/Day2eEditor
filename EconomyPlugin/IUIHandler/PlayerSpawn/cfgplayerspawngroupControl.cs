using Day2eEditor;
using DayZeLib;
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
    public partial class cfgplayerspawngroupControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private playerspawnpointsGroup _data;
        private playerspawnpointsGroup _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgplayerspawngroupControl()
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
            _data = data as playerspawnpointsGroup ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            generatorposbubblesGroupnameTB.Text = _data.name;
            generatorposbubblesGroupnameTB.Text = _data.name;
            if (generatorposbubblesUseLifetimeCB.Checked = _data.lifetimeSpecified == true)
            {
                generatorposbubbleslifetiemNUD.Value = _data.lifetime;
            }
            if (generatorposbubblesusecounterCB.Checked = _data.counterSpecified == true)
            {
                generatorposbubblescounterNUD.Value = _data.counter;
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
        private playerspawnpointsGroup CloneData(playerspawnpointsGroup data)
        {
            // TODO: Implement actual cloning logic
            return new playerspawnpointsGroup
            {
                name = data.name,
                lifetimeSpecified = data.lifetimeSpecified,
                lifetime = data.lifetime,
                counterSpecified = data.counterSpecified,
                counter = data.counter
            };
        }

        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = $"Group Name: {_data.name}";
            }
        }

        #endregion
        private void generatorposbubblesGroupnameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.name = generatorposbubblesGroupnameTB.Text;
            UpdateTreeNodeText();
            HasChanges();
        }
        private void generatorposbubblesUseLifetimeCB_CheckedChanged(object sender, EventArgs e)
        {
            if (generatorposbubblesUseLifetimeCB.Checked == true)
            {
                generatorposbubbleslifetiemNUD.Visible = true;
                label42.Visible = true;
                if (_suppressEvents) return;

                _data.lifetimeSpecified = true;
                HasChanges();
            }
            else
            {
                generatorposbubbleslifetiemNUD.Visible = false;
                label42.Visible = false;
                if (_suppressEvents) return;

                _data.lifetimeSpecified = false;
                HasChanges();
            }
        }

        private void generatorposbubbleslifetiemNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.lifetime = (int)generatorposbubbleslifetiemNUD.Value;
            HasChanges();
        }

        private void generatorposbubblesusecounterCB_CheckedChanged(object sender, EventArgs e)
        {
            if (generatorposbubblesusecounterCB.Checked == true)
            {
                generatorposbubblescounterNUD.Visible = true;
                label43.Visible = true;
                if (_suppressEvents) return;

                _data.counterSpecified = true;
                HasChanges();
            }
            else
            {
                generatorposbubblescounterNUD.Visible = false;
                label43.Visible = false;
                if (_suppressEvents) return;

                _data.counterSpecified = false;
                HasChanges();
            }
        }

        private void generatorposbubblescounterNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.counter = (int)generatorposbubblescounterNUD.Value;
            HasChanges();
        }


    }
}