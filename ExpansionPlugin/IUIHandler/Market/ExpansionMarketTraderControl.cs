using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionMarketTraderControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionMarketTrader _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionMarketTraderControl()
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
            _data = data as ExpansionMarketTrader ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            BindingList<string> Factions = new BindingList<string>(File.ReadAllLines("Data\\ExpansionFactions.txt").ToList());
            BindingList<string> Icons = new BindingList<string>(File.ReadAllLines("Data\\ExpansionIconnames.txt").ToList());
            Factions.Insert(0, "");
            RequiredFactionLB.DataSource = Factions;
            TraderIconCB.DataSource = Icons;
            textBox2.Text = _data.FileName;
            textBox5.Text = _data.DisplayName;
            MinRequiredHumanityNUD.Value = (int)_data.MinRequiredReputation;
            MaxRequiredHumanityNUD.Value = (int)_data.MaxRequiredReputation;
            RequiredFactionLB.SelectedIndex = RequiredFactionLB.FindStringExact(_data.RequiredFaction);
            RequiredCompletedQuestIDNUD.Value = (int)_data.RequiredCompletedQuestID;
            TraderIconCB.SelectedIndex = TraderIconCB.FindStringExact(_data.TraderIcon);
            UseCategoryOrderCB.Checked = _data.UseCategoryOrder == 1 ? true : false;

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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DisplayName = textBox5.Text;

        }

        private void MinRequiredHumanityNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MinRequiredReputation = (int)MinRequiredHumanityNUD.Value;
        }

        private void MaxRequiredHumanityNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxRequiredReputation = (int)MaxRequiredHumanityNUD.Value;
        }

        private void RequiredFactionLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.RequiredFaction = RequiredFactionLB.GetItemText(RequiredFactionLB.SelectedItem);
        }

        private void RequiredCompletedQuestIDNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.RequiredCompletedQuestID = (int)RequiredCompletedQuestIDNUD.Value;
        }

        private void UseCategoryOrderCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseCategoryOrder = UseCategoryOrderCB.Checked == true ? 1 : 0;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void TraderIconCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.TraderIcon = TraderIconCB.Text;
        }
    }
}