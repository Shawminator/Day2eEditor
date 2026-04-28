using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
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
            QuestsObjectivesIDNUD.Value = _data.ID;
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
                _nodes.Last().Parent.Text = _data.FileName;
            }
        }
        private void QuestObjectivesFilenameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            string dirName = Path.GetDirectoryName(_data._path);
            string newFilename = QuestObjectivesFilenameTB.Text + ".json";
            _data.SetPath(Path.Combine(dirName, newFilename));
            UpdateTreeNodeText();
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
    }
}