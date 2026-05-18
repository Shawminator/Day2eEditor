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
    public partial class ExpansionQuestQuestFactionRepsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private FactionQuestRep _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionQuestQuestFactionRepsControl()
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
            _data = data as FactionQuestRep ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            BindingList<string> Factions = new BindingList<string>(File.ReadAllLines("Data\\ExpansionFactions.txt").ToList());
            Factions.Insert(0, "");
            FactionCB.DataSource = Factions;

            FactionCB.SelectedIndex = FactionCB.FindStringExact(_data.Faction);
            ReputationNUD.Value = (int)_data.Reputation;

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
                _nodes.Last().Text = $"Faction:{_data.Faction} Rep:{_data.Reputation}";
            }
        }

        #endregion

        private void FactionCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Faction = FactionCB.GetItemText(FactionCB.SelectedItem);
            UpdateTreeNodeText();
        }

        private void ReputationNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Reputation = (int)ReputationNUD.Value;
            UpdateTreeNodeText();
        }
    }
}