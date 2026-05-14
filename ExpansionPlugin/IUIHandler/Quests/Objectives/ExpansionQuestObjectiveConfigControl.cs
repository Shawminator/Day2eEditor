using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ExpansionPlugin
{
    public partial class ExpansionQuestObjectiveConfigControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionQuestObjectiveConfig _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionQuestObjectiveConfigControl()
        {
            InitializeComponent();
        }

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as ExpansionQuestObjectiveConfig ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;
            QuestObjectivesObjectiveTypeCB.DataSource = Enum.GetValues(typeof(ExpansionQuestObjectiveType));
            QuestObjectivesFilenameTB.Text = Path.GetFileNameWithoutExtension(_data.FileName);
            QuestObjectivesConfigVersionNUD.Value = _data.ConfigVersion;
            QuestsObjectivesIDNUD.Value = (int)_data.ID;
            QuestObjectivesObjectiveTypeCB.SelectedItem = (ExpansionQuestObjectiveType)_data.ObjectiveType;
            QuestObjectivesObjectiveTextTB.Text = _data.ObjectiveText;
            QuestObjectivesTimeLimitNUD.Value = (int)_data.TimeLimit;
            QuestObjectivesActiveCB.Checked = _data.Active == 1 ? true : false;
            QuestObjectivesObjectiveTypeCB.SelectedItem = (ExpansionQuestObjectiveType)_data.ObjectiveType;


            _suppressEvents = false;
        }

        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {

                TreeNode child = _nodes.Last();
                TreeNode parent = _nodes.Last().Parent;
                TreeNode grandparent = _nodes.Last().Parent.Parent;

                parent.Text = $"ID:{_data.ID} {_data.ObjectiveText}";

                grandparent.Nodes.Remove(parent);

                int parentId = _data.ID ?? int.MaxValue;

                int index = 0;

                foreach (TreeNode sibling in grandparent.Nodes)
                {
                    var siblingQuest = sibling.Tag as ExpansionQuestObjectiveConfig;
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
        private void QuestObjectivesFilenameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            string dirName = Path.GetDirectoryName(_data._path);
            string newFilename = QuestObjectivesFilenameTB.Text + ".json";
            _data.SetPath(Path.Combine(dirName, newFilename));
        }
        private void QuestObjectivesObjectiveTextTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ObjectiveText = QuestObjectivesObjectiveTextTB.Text;
        }
        private void QuestObjectivesTimeLimitNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.TimeLimit = (int)QuestObjectivesTimeLimitNUD.Value;
        }
        private void QuestObjectivesActiveCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Active = QuestObjectivesActiveCB.Checked == true ? 1 : 0;
        }

        private void QuestsObjectivesIDNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            List<int> AllObjectiveIDs = AppServices.GetRequired<ExpansionManager>().ExpansionQuestObjectiveConfigConfig.GetAllObjectivesIDS((ExpansionQuestObjectiveType)_data.ObjectiveType);
            int currentid = (int)_data.ID;
            int newid = (int)QuestsObjectivesIDNUD.Value;
            if (AllObjectiveIDs.Contains(newid))
            {
                MessageBox.Show($"ID:{newid} is allready in use, Please select a different ID");
                _suppressEvents = true;
                QuestsObjectivesIDNUD.Value = (int)_data.ID;
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
                    foreach(Objectives obj in quest.Objectives)
                    {
                        if (obj.ObjectiveType == (ExpansionQuestObjectiveType)_data.ObjectiveType &&
                            obj.ID == currentid)
                        {
                            count++;
                            obj.ID = (int)_data.ID;
                        }
                    }
                }
                Console.WriteLine($"Total number of additional quests edited :{count}");
            }
        }
    }
}