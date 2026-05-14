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
    public partial class TraderNPCSpecialPropertiesFactionControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private TraderNPCSpecialProperties _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public TraderNPCSpecialPropertiesFactionControl()
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

            BindingList<string> Factions = new BindingList<string>(File.ReadAllLines("Data\\ExpansionFactions.txt").ToList());
            Factions.Insert(0, "");
            FactionCB.DataSource = Factions;
            if (string.IsNullOrEmpty(_data.Faction))
            {
                FactionCB.SelectedIndex = 0;
            }
            else
            {
                int index = FactionCB.FindStringExact(_data.Faction);
                FactionCB.SelectedIndex = index >= 0 ? index : 0;
            }
            _suppressEvents = false;
        }

        #region Helper Methods
        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            string name = _data.Faction;
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = string.IsNullOrEmpty(_data.Faction) ? "Faction:" : $"Faction: {name}";
            }
        }

        #endregion
        private void FactionCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Faction = FactionCB.GetItemText(FactionCB.SelectedItem);
            if (_data.Faction == "")
                _data.Faction = null;
            UpdateTreeNodeText();
        }
    }
}