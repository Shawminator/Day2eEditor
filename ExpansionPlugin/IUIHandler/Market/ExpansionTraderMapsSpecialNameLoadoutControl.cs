using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionTraderMapsSpecialNameLoadoutControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private TraderNPCSpecialProperties _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionTraderMapsSpecialNameLoadoutControl()
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
            _data = data as TraderNPCSpecialProperties ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            BindingList<string> LoadoutNameList = new BindingList<string>
                {
                    ""
                };
            foreach (AILoadouts lo in AppServices.GetRequired<ExpansionManager>().ExpansionLoadoutConfig.Items)
            {
                LoadoutNameList.Add(Path.GetFileNameWithoutExtension(lo.FileName));
            }
            StaticPatrolLoadoutsCB.DataSource = new BindingList<string>(LoadoutNameList);
            if (string.IsNullOrEmpty(_data.Loadout))
            {
                StaticPatrolLoadoutsCB.SelectedIndex = 0;
            }
            else
            {
                int index = StaticPatrolLoadoutsCB.FindStringExact(_data.Loadout);
                StaticPatrolLoadoutsCB.SelectedIndex = index >= 0 ? index : 0;
            }
            _suppressEvents = false;
        }

        #region Helper Methods
        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            string name = _data.Loadout;
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = string.IsNullOrEmpty(_data.Loadout) ? "Loadout:" : $"Loadout: {name}";
            }
        }

        #endregion

        private void StaticPatrolLoadoutsCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Loadout = StaticPatrolLoadoutsCB.GetItemText(StaticPatrolLoadoutsCB.SelectedItem);
            if (_data.Loadout == "")
                _data.Loadout = null;
            UpdateTreeNodeText();
        }
    }
}