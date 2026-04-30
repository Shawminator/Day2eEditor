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
    public partial class ExpansionQuestQuestTextDialogueControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionQuestQuest _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionQuestQuestTextDialogueControl()
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
            QuestTitleTB.Text = _data.Title;
            QuestTextTB.Text = _data.ObjectiveText;
            if (_data.Descriptions.Count == 0)
            {
                _data.Descriptions = new BindingList<string>(new string[] { "", "", "" });
            }
            if (_data.Descriptions.Count != 3)
            {
                switch (_data.Descriptions.Count)
                {
                    case 0:
                        QuestDescription1TB.Text = "";
                        QuestDescription2TB.Text = "";
                        QuestDescription3TB.Text = "";
                        break;
                    case 1:
                        QuestDescription1TB.Text = _data.Descriptions[0];
                        QuestDescription2TB.Text = "";
                        QuestDescription3TB.Text = "";
                        break;
                    case 2:
                        QuestDescription1TB.Text = _data.Descriptions[0];
                        QuestDescription2TB.Text = _data.Descriptions[1];
                        QuestDescription3TB.Text = "";
                        break;
                }
                MessageBox.Show("Incorrect number of lines for description. Please save to fix the file.");
                Console.WriteLine("Quest " + _data.ID.ToString() + "has incorrect number of lines for description. Please save to fix the file.\n");
            }
            else
            {
                QuestDescription1TB.Text = _data.Descriptions[0];
                QuestDescription2TB.Text = _data.Descriptions[1];
                QuestDescription3TB.Text = _data.Descriptions[2];
            }

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



        private void QuestTitleTB_TextChanged(object sender, EventArgs e)
        {

        }

        private void QuestTextTB_TextChanged(object sender, EventArgs e)
        {

        }

        private void QuestDescription1TB_TextChanged(object sender, EventArgs e)
        {

        }
        private void QuestDescription2TB_TextChanged(object sender, EventArgs e)
        {

        }
        private void QuestDescription3TB_TextChanged(object sender, EventArgs e)
        {

        }

    }
}