using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class AIRoamingLocationControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionAIRoamingLocation _data;
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

            _suppressEvents = true;

            NameTB.Text = _data.Name;
            XNUD.Text = _data._Position.X.ToString();
            YNUD.Text = _data._Position.Y.ToString();
            ZNUD.Text = _data._Position.Z.ToString();
            RadiusNUD.Text = _data.Radius.ToString();
            TypeTB.Text = _data.Type;
            EnabledCB.Checked = _data.Enabled == 1 ? true : false;

            _suppressEvents = false;
        }

        #region Helper Methods
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
        }
    }
}