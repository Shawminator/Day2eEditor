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
    public partial class ExpansionQuestQuestRequirementsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionQuestQuest _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionQuestQuestRequirementsControl()
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
            _data = data as ExpansionQuestQuest ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            BindingList<string> Factions = new BindingList<string>(File.ReadAllLines("Data\\ExpansionFactions.txt").ToList());
            Factions.Insert(0, "");
            RequiredFactionCB.DataSource = new BindingList<string>(Factions.ToList());
            FactionRewardCB.DataSource = new BindingList<string>(Factions.ToList());

            ReputationRewardNUD.Value = (int)_data.ReputationReward;
            ReputationRequirementNUD.Value = (int)_data.ReputationRequirement;
            RequiredFactionCB.SelectedIndex = RequiredFactionCB.FindStringExact(_data.RequiredFaction);
            FactionRewardCB.SelectedIndex = FactionRewardCB.FindStringExact(_data.FactionReward);

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

        private void ReputationRewardNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ReputationReward = (int)ReputationRewardNUD.Value;
        }

        private void ReputationRequirementNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ReputationRequirement = (int)ReputationRequirementNUD.Value;
        }

        private void RequiredFactionCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.RequiredFaction = RequiredFactionCB.GetItemText(RequiredFactionCB.SelectedItem);
        }

        private void FactionRewardCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (_suppressEvents) return;
            _data.FactionReward = FactionRewardCB.GetItemText(FactionRewardCB.SelectedItem);
        }
    }
}