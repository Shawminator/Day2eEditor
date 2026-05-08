using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
            BindingList<string> Icons = new BindingList<string>(File.ReadAllLines("Data\\ExpansionIconnames.txt").ToList());
            m_DisplayIconCB.DataSource = Icons;



            FilenameTB.Text = Path.GetFileNameWithoutExtension(_data.FileName);
            m_TraderIDNUD.Value = (int)_data.m_TraderID;
            m_ClassNameCB.SelectedIndex = m_ClassNameCB.FindStringExact(_data.m_ClassName);
            m_LoadoutFileCB.SelectedIndex = m_LoadoutFileCB.FindStringExact(_data.m_LoadoutFile);
            m_DisplayNameTB.Text = _data.m_DisplayName;
            m_DisplayIconCB.SelectedIndex = m_DisplayIconCB.FindStringExact(_data.m_DisplayIcon);
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
            GetIcon();
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
                _nodes.Last().Parent.Text = _data.FileName;
            }
        }

        #endregion

        private void FilenameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            string dirName = Path.GetDirectoryName(_data._path);
            string newFilename = FilenameTB.Text + ".json";
            _data.SetPath(Path.Combine(dirName, newFilename));
            UpdateTreeNodeText();
        }

        private void m_TraderIDNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            List<int> AllPSIDs = AppServices.GetRequired<ExpansionManager>().ExpansionP2pMarketTradersConfig.UsedIDS;
            int currentid = (int)_data.m_TraderID;
            int newid = (int)m_TraderIDNUD.Value;
            if (AllPSIDs.Contains(newid))
            {
                MessageBox.Show($"P2P Trader ID {newid} is allready in use, Please select a different ID");
                _suppressEvents = true;
                m_TraderIDNUD.Value = (int)_data.m_TraderID;
                _suppressEvents = false;
            }
            else
            {
                _data.m_TraderID = (int)m_TraderIDNUD.Value;
                AppServices.GetRequired<ExpansionManager>().ExpansionP2pMarketTradersConfig.UsedIDS.Remove(currentid);
                AppServices.GetRequired<ExpansionManager>().ExpansionP2pMarketTradersConfig.UsedIDS.Add(newid);
            }
        }
        private void GetIcon()
        {
            string iconname = _data.m_DisplayIcon.Replace("/", "");
            var resourceName = $"ExpansionPlugin.Icons.{iconname}.png";
            var stream = ResourceHelper.OpenEmbeddedStream(resourceName);
            if (stream != null)
            {
                Bitmap image = new Bitmap(Image.FromStream(stream));
                Image image3 = resizeImage(image, new Size(128, 128));
                pictureBox1.Image = image3;
            }
            else
            {
                pictureBox1.Image = null;
            }
        }
        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }
        private void m_ClassNameCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_ClassName = m_ClassNameCB.GetItemText(m_ClassNameCB.SelectedIndex);
        }

        private void m_LoadoutFileCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_LoadoutFile = m_LoadoutFileCB.GetItemText(m_LoadoutFileCB.SelectedIndex);
        }

        private void m_DisplayNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_DisplayName = m_DisplayNameTB.Text;
        }

        private void m_DisplayIconCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_DisplayIcon = m_DisplayIconCB.SelectedItem.ToString();
            GetIcon();
        }

        private void m_FactionCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_Faction = m_FactionCB.GetItemText(m_FactionCB.SelectedIndex);
        }

        private void m_EmoteIDNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_EmoteID = (int)m_EmoteIDNUD.Value;
        }

        private void m_EmoteIsStaticCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_EmoteIsStatic = m_EmoteIsStaticCB.Checked == true ? 1 : 0;
        }

        private void m_IsGlobalTraderCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_IsGlobalTrader = m_IsGlobalTraderCB.Checked == true ? 1 : 0;
        }

        private void m_UseReputationCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_UseReputation = m_UseReputationCB.Checked == true ? 1 : 0;
        }

        private void m_MinRequiredReputationNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_MinRequiredReputation = (int)m_MinRequiredReputationNUD.Value;
        }

        private void m_MaxRequiredReputationNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_MaxRequiredReputation = (int)m_MaxRequiredReputationNUD.Value;
        }

        private void m_RequiredFactionCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_RequiredFaction = m_RequiredFactionCB.GetItemText(m_RequiredFactionCB.SelectedItem);
        }

        private void QuestCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_RequiredCompletedQuestID = (QuestCB.SelectedItem as ComboItem).Id ;
        }

        private void m_DisplayCurrencyValueNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_DisplayCurrencyValue = (int)m_DisplayCurrencyValueNUD.Value;
        }

        private void m_DisplayCurrencyNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_DisplayCurrencyName = m_DisplayCurrencyNameTB.Text;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


    }
}