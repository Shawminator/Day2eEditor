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
    public partial class ExpansionSocialMediaSettingsTextControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionNewsFeedTextSetting _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionSocialMediaSettingsTextControl()
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
            _data = data as ExpansionNewsFeedTextSetting ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            ExpansionNewsFeedTextSettingTitleTB.Text = _data.m_Title;
            ExpansionNewsFeedTextSettingTextTB.Text = _data.m_Text;

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

        private void ExpansionNewsFeedTextSettingTitleTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.m_Title = ExpansionNewsFeedTextSettingTitleTB.Text;
        }

        private void ExpansionNewsFeedTextSettingTextTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.m_Text = ExpansionNewsFeedTextSettingTextTB.Text;
        }
    }
}