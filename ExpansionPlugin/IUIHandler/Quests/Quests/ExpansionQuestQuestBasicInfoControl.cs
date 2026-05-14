using Day2eEditor;
using System;
using System.Collections;
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
    public partial class ExpansionQuestQuestBasicInfoControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionQuestQuest _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionQuestQuestBasicInfoControl()
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

            QuestTypeCB.DataSource = Enum.GetValues(typeof(ExpansionQuestType));

            QuestFilenameTB.Text = Path.GetFileNameWithoutExtension(_data.FileName);
            QuestConfigVersionNUD.Value = _data.ConfigVersion;
            QuestsIDNUD.Value = (int)_data.ID;
            QuestTypeCB.SelectedItem = (ExpansionQuestType)_data.Type;
            QuestActiveCB.Checked = _data.Active == 1 ? true : false;
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

                parent.Text = $"ID {_data.ID} : {_data.Title}";

                grandparent.Nodes.Remove(parent);

                int parentId = _data.ID ?? int.MaxValue;

                int index = 0;

                foreach (TreeNode sibling in grandparent.Nodes)
                {
                    var siblingQuest = sibling.Tag as ExpansionQuestQuest;
                    int siblingId = siblingQuest?.ID ?? int.MaxValue;

                    if (parentId < siblingId)
                        break;

                    index++;
                }

                grandparent.Nodes.Insert(index, parent);

                // reselect
                child.TreeView.SelectedNode = child;
                child.EnsureVisible();
            }
        }

        #endregion

        private void QuestFilenameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            string dirName = Path.GetDirectoryName(_data._path);
            string newFilename = QuestFilenameTB.Text + ".json";
            _data.SetPath(Path.Combine(dirName, newFilename));
           
        }

        private void QuestConfigVersionNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            // we dont change this.....
        }

        private void QuestsIDNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            List<int> AllQuestIDs = AppServices.GetRequired<ExpansionManager>().ExpansionQuestQuestConfig.GetAllQuestIDS();
            int currentid = (int)_data.ID;
            int newid = (int)QuestsIDNUD.Value;
            if (AllQuestIDs.Contains(newid))
            {
                MessageBox.Show("That quest ID is allready in use, Please select a different ID");
                _suppressEvents = true;
                QuestsIDNUD.Value = (int)_data.ID;
                _suppressEvents = false;
            }
            else
            {
                _data.ID = newid;
                UpdateTreeNodeText();
                int count = 0;
                var QuestList = AppServices.GetRequired<ExpansionManager>().ExpansionQuestQuestConfig.MutableItems;
                foreach (ExpansionQuestQuest quest in QuestList)
                {
                    if (quest.PreQuestIDs.Contains(currentid) || quest.FollowUpQuest == currentid)
                    {
                        count++;
                        if (quest.PreQuestIDs.Contains(currentid))
                        {
                            int index = quest.PreQuestIDs.IndexOf(currentid);
                            if (index != -1)
                            {
                                quest.PreQuestIDs[index] = newid;
                            }
                        }
                        if (quest.FollowUpQuest == currentid)
                        {
                            quest.FollowUpQuest = newid;
                        }
                    }
                }
                Console.WriteLine($"Total number of additional quests edited :{count}");
            }
        }

        private void QuestTypeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Type = (ExpansionQuestType)QuestTypeCB.SelectedItem;
        }

        private void QuestObjectivesActiveCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Active = QuestActiveCB.Checked == true ? 1 : 0;

        }
    }
}