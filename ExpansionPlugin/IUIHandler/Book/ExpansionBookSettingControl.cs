using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionBookSettingControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionBookSetting _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionBookSettingControl()
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
            _data = data as ExpansionBookSetting ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            textBox6.Text = _data.SettingTitle;
            textBox7.Text = _data.SettingText;
            textBox8.Text = _data.SettingValue;

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
                _nodes.Last().Text = $"Setting Title: {_data.SettingTitle}";
            }
        }

        #endregion

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SettingTitle = textBox6.Text;
            UpdateTreeNodeText();
            

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SettingText = textBox7.Text;
            
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SettingValue = textBox8.Text;
        }
    }
}