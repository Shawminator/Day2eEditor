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
    public partial class ExpansionP2PMarketTraderConfigGeneralControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionP2PMarketTraderConfig _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionP2PMarketTraderConfigGeneralControl()
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
            _data = data as ExpansionP2PMarketTraderConfig ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;
            List<ComboItem> comboItems = new List<ComboItem>();
            foreach (ExpansionQuestQuest quest in AppServices.GetRequired<ExpansionManager>().ExpansionQuestQuestConfig.MutableItems)
            {
                comboItems.Add(new ComboItem()
                {
                    Id = (int)quest.ID,
                    Name = $"Quest {(int)quest.ID}: {quest.Title}"
                });
            }
            comboItems.Insert(0, new ComboItem()
            {
                Id = -1,
                Name = $"Quest {-1}: No Quest Selected"
            });
            QuestCB.DataSource = comboItems.OrderBy(x => x.Id).ToList();
            QuestCB.DisplayMember = "Name";
            QuestCB.ValueMember = "Id";

            BindingList<string> Factions = new BindingList<string>(File.ReadAllLines("Data\\ExpansionFactions.txt").ToList());
            Factions.Insert(0, "");
            m_FactionCB.DataSource = new BindingList<string>(Factions?.ToList() ?? new List<string>());
            m_RequiredFactionCB.DataSource = new BindingList<string>(Factions?.ToList() ?? new List<string>());

            BindingList<string> LoadoutNameList = new BindingList<string> { "" };
            foreach (AILoadouts lo in AppServices.GetRequired<ExpansionManager>().ExpansionLoadoutConfig.Items)
            {
                LoadoutNameList.Add(Path.GetFileNameWithoutExtension(lo.FileName));
            }
            m_LoadoutFileCB.DataSource = new BindingList<string>(LoadoutNameList);
            FilenameTB.Text = Path.GetFileNameWithoutExtension(_data.FileName);
            m_TraderIDNUD.Value = (int)_data.m_TraderID;
            m_ClassNameCB.SelectedIndex = m_ClassNameCB.FindStringExact(_data.m_ClassName);
            m_LoadoutFileCB.SelectedIndex = m_LoadoutFileCB.FindStringExact(_data.m_LoadoutFile);
            m_DisplayNameTB.Text = _data.m_DisplayName;
            m_DisplayIconTB.Text = _data.m_DisplayIcon;
            m_FactionCB.SelectedIndex = m_FactionCB.FindStringExact(_data.m_Faction);
            m_EmoteIDNUD.Value = (int)_data.m_EmoteID;
            m_EmoteIsStaticCB.Checked = _data.m_EmoteIsStatic == 1 ? true : false;
            m_IsGlobalTraderCB.Checked = _data.m_IsGlobalTrader == 1 ? true : false;
            m_UseReputationCB.Checked = _data.m_UseReputation == 1 ? true : false;
            m_MinRequiredReputationNUD.Value = (int)_data.m_MinRequiredReputation;
            m_MaxRequiredReputationNUD.Value = (int)_data.m_MaxRequiredReputation;
            m_RequiredFactionCB.SelectedIndex = m_RequiredFactionCB.FindStringExact(_data.m_RequiredFaction);
            QuestCB.SelectedValue = (int)_data.m_RequiredCompletedQuestID;
            m_DisplayCurrencyValueNUD.Value = (int)_data.m_DisplayCurrencyValue;
            m_DisplayCurrencyNameTB.Text = _data.m_DisplayCurrencyName;

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
    }
}