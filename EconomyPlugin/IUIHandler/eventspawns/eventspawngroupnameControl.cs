using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class eventspawngroupnameControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private eventgroupdefGroup _data;
        private eventgroupdefGroup _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public eventspawngroupnameControl()
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
            _data = data as eventgroupdefGroup ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            textBox1.Text = _data.name;

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
            cfgeventgroupsConfig parentObj = AppServices.GetRequired<EconomyManager>().cfgeventgroupsConfig;
            if (parentObj != null)
            {
                dynamic parent = parentObj;
                AppServices.GetRequired<EconomyManager>().cfgeventspawnsConfig.isDirty = parent.isDirty = _data.name != _originalData.name;
            }
        }

        #region Helper Methods

        /// <summary>
        /// Clones the data for reset purposes
        /// </summary>
        private eventgroupdefGroup CloneData(eventgroupdefGroup data)
        {
            // TODO: Implement actual cloning logic
            return new eventgroupdefGroup
            {
                name = data.name
            };
        }

        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = $"Group Name : {_data.name}";
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            _data.name = textBox1.Text;

            eventposdefEventPos spawnpos = _nodes.Last().Parent.Tag as eventposdefEventPos;
            spawnpos.group = _data.name;
            UpdateTreeNodeText();
            HasChanges();
        }
    }
}