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
        private List<ExpansionQuestObjectiveConfig> objectiveslist { get; set; }
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

            objectiveslist = AppServices.GetRequired<ExpansionManager>().ExpansionQuestObjectiveConfigConfig.MutableItems;
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
                _nodes.Last().Text = _data.DisplayText;
            }
        }

        #endregion
        private void ObjTypeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _suppressEvents = true;
            var selectedValue = (ExpansionQuestObjectiveType)ObjTypeCB.SelectedItem;

            ObjIdCB.DataSource = objectiveslist
           .Where(o => o.ObjectiveType == selectedValue)
           .ToList();
            ObjIdCB.DisplayMember = nameof(ExpansionQuestObjectiveConfig.DisplayText);
            ObjIdCB.SelectedIndex = -1;
            _suppressEvents = false;
        }
        private void ObjIdCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            ExpansionQuestObjectiveConfig obj = ObjIdCB.SelectedItem as ExpansionQuestObjectiveConfig;
            _data.ObjectiveType = obj.ObjectiveType;
            _data.ID = (int)obj.ID;
            UpdateTreeNodeText();
        }


    }
}