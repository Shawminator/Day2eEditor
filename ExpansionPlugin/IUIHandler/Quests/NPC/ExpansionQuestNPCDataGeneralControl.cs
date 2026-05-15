using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionQuestNPCDataGeneralControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionQuestNPCData _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionQuestNPCDataGeneralControl()
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
            _data = data as ExpansionQuestNPCData ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;
            ClassNameCB.DataSource = File.ReadAllLines("Data\\ExpansionQuestNPCnames.txt").ToList();
            BindingList<string> LoadoutNameList = new BindingList<string>
                {
                    ""
                };
            foreach (AILoadouts lo in AppServices.GetRequired<ExpansionManager>().ExpansionLoadoutConfig.Items)
            {
                LoadoutNameList.Add(Path.GetFileNameWithoutExtension(lo.FileName));
            }
            NPCLoadoutFileCB.DataSource = new BindingList<string>(LoadoutNameList);
            ClassNameCB.DataSource = File.ReadAllLines("Data\\ExpansionQuestNPCnames.txt").ToList(); ;
            NPCTypeCB.DataSource = Enum.GetValues(typeof(ExpansionQuestNPCType));
            BindingList<string> Factions = new BindingList<string>(File.ReadAllLines("Data\\ExpansionFactions.txt").ToList());
            Factions.Insert(0, "");
            NPCFactionCB.DataSource = Factions;


            FilenameTB.Text = Path.GetFileNameWithoutExtension(_data.FileName);
            ConfigVersionNUD.Value = _data.ConfigVersion;
            IDNUD.Value = (int)_data.ID;
            NPCTypeCB.SelectedItem = (ExpansionQuestNPCType)_data.NPCType;
            ClassNameCB.SelectedIndex = ClassNameCB.FindStringExact(_data.ClassName);
            NPCNameTB.Text = _data.NPCName;
            DefaultNPCTextTB.Text = _data.DefaultNPCText;
            NPCLoadoutFileCB.SelectedIndex = NPCLoadoutFileCB.FindStringExact(_data.NPCLoadoutFile);
            NPCFactionCB.SelectedIndex = NPCFactionCB.FindStringExact(_data.NPCFaction);
            ActiveCB.Checked = _data.Active == 1 ? true : false;
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
                TreeNode child = _nodes.Last();
                TreeNode parent = _nodes.Last().Parent;
                TreeNode grandparent = _nodes.Last().Parent.Parent;
                TreeNode greatgrandparent = _nodes.Last().Parent.Parent.Parent;

                //update QuestNPC Nodes
                parent.Text = $"ID:{_data.ID} {_data.NPCName} ({_data.ClassName}) {_data.GetNPCType()}";

                child.TreeView.SelectedNode = child;
                child.EnsureVisible();

                foreach (TreeNode tn in greatgrandparent.Nodes[3].Nodes)
                {
                    foreach (TreeNode tnn in tn.Nodes[4].Nodes)
                    {
                        foreach (TreeNode tnnn in tnn.Nodes)
                        {
                            if ((int)tnnn.Tag == _data.ID)
                            {
                                tnnn.Text = $"{Helpers.GetNPCReferenceText((int)_data.ID)}";
                            }
                        }
                    }
                }
            }
        }
        private void UpdateTreeNodeTextIDChange()
        {
            if (_nodes?.Any() == true)
            {
                TreeNode child = _nodes.Last();
                TreeNode parent = _nodes.Last().Parent;
                TreeNode grandparent = _nodes.Last().Parent.Parent;
                TreeNode greatgrandparent = _nodes.Last().Parent.Parent.Parent;

                //update QuestNPC Nodes
                parent.Text = $"ID:{_data.ID} {_data.NPCName} ({_data.ClassName}) {_data.GetNPCType()}";

                grandparent.Nodes.Remove(parent);

                int parentId = _data.ID ?? int.MaxValue;

                int index = 0;

                foreach (TreeNode sibling in grandparent.Nodes)
                {
                    var siblingQuest = sibling.Tag as ExpansionQuestNPCData;
                    int siblingId = siblingQuest?.ID ?? int.MaxValue;

                    if (parentId < siblingId)
                        break;

                    index++;
                }

                grandparent.Nodes.Insert(index, parent);

                child.TreeView.SelectedNode = child;
                child.EnsureVisible();
            }
        }
        #endregion
        private void FilenameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            string dirName = Path.GetDirectoryName(_data._path);
            string newFilename = FilenameTB.Text + ".json";
            _data.SetPath(Path.Combine(dirName, newFilename));
        }
        private void ConfigVersionNUD_ValueChanged(object sender, EventArgs e)
        {

        }
        private void IDNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            List<int> AllQuestIDs = AppServices.GetRequired<ExpansionManager>().ExpansionQuestNPCDataConfig.GetAllNPCIDS();
            int currentid = (int)_data.ID;
            int newid = (int)IDNUD.Value;
            if (AllQuestIDs.Contains(newid))
            {
                MessageBox.Show("That quest ID is allready in use, Please select a different ID");
                _suppressEvents = true;
                IDNUD.Value = (int)_data.ID;
                _suppressEvents = false;
            }
            else
            {
                _data.ID = newid;
                UpdateTreeNodeTextIDChange();
                int count = 0;
                var QuestList = AppServices.GetRequired<ExpansionManager>().ExpansionQuestQuestConfig.MutableItems;
                foreach (ExpansionQuestQuest quest in QuestList)
                {
                    bool updated = false;
                    if (quest.QuestGiverIDs.Contains(currentid))
                    {
                        int index = quest.QuestGiverIDs.IndexOf(currentid);
                        quest.QuestGiverIDs[index] = newid;
                        updated = true;
                    }
                    if (quest.QuestTurnInIDs.Contains(currentid))
                    {
                        int index = quest.QuestTurnInIDs.IndexOf(currentid);
                        quest.QuestTurnInIDs[index] = newid;
                        updated = true;
                    }
                    if (updated)
                    {
                        count++;
                    }
                    TreeNode greatgrandparent = _nodes.Last().Parent.Parent.Parent;
                    foreach (TreeNode tn in greatgrandparent.Nodes[3].Nodes)
                    {
                        foreach (TreeNode tnn in tn.Nodes[4].Nodes)
                        {
                            foreach (TreeNode tnnn in tnn.Nodes)
                            {
                                if ((int)tnnn.Tag == currentid)
                                {
                                    tnnn.Text = $"{Helpers.GetNPCReferenceText((int)_data.ID)}";
                                    tnnn.Tag = _data.ID;
                                }
                            }
                        }
                    }
                }
                Console.WriteLine($"Total number of additional quests edited :{count}");
            }
        }
        private void NPCTypeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NPCType = (ExpansionQuestNPCType)NPCTypeCB.SelectedItem;
            UpdateTreeNodeText();
        }
        private void ClassNameCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ClassName = ClassNameCB.SelectedItem?.ToString();
            UpdateTreeNodeText();
        }
        private void NPCNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (NPCNameTB.Text != _data.NPCName)
                darkButton36.Enabled = true;
        }
        private void darkButton36_Click(object sender, EventArgs e)
        {
            _data.NPCName = NPCNameTB.Text;
            UpdateTreeNodeText();
        }
        private void DefaultNPCTextTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DefaultNPCText = DefaultNPCTextTB.Text;
        }
        private void NPCLoadoutFileCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NPCLoadoutFile = NPCLoadoutFileCB.GetItemText(NPCLoadoutFileCB.SelectedItem);
        }
        private void ActiveCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Active = ActiveCB.Checked == true ? 1 : 0;
        }

        private void NPCFactionCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NPCFaction = NPCFactionCB.GetItemText(NPCFactionCB.SelectedItem);
        }
    }
}