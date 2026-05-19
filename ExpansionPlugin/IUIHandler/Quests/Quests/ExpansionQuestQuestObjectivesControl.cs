using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionQuestQuestObjectivesControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private Objectives _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionQuestQuestObjectivesControl()
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
            _data = data as Objectives ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            List<ExpansionQuestObjectiveConfig> objectiveslist = AppServices.GetRequired<ExpansionManager>().ExpansionQuestObjectiveConfigConfig.MutableItems;
            ObjTypeCB.DataSource = Enum.GetValues<ExpansionQuestObjectiveType>()
                .Where(t => t != ExpansionQuestObjectiveType.NONE)
                .ToList();
            ObjIdCB.DataSource = objectiveslist
            .Where(o => o.ObjectiveType == _data.ObjectiveType)
            .ToList();
            ObjIdCB.DisplayMember = nameof(ExpansionQuestObjectiveConfig.DisplayText);

            ObjTypeCB.SelectedItem = (ExpansionQuestObjectiveType)_data.ObjectiveType;

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
        private void QuestsCB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}