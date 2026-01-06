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
    public partial class ExpansionBookRuleControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionBookRule _data;
        private ExpansionBookRule _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionBookRuleControl()
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
            _data = data as ExpansionBookRule ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = _data.CLone();

            _suppressEvents = true;

            textBox6.Text = _data.RuleParagraph;
            textBox7.Text = _data.RuleText;

            _suppressEvents = false;
        }

        /// <summary>
        /// Applies changes to the data and updates the original snapshot
        /// </summary>
        public void ApplyChanges()
        {
            _originalData = _data.CLone();
        }

        /// <summary>
        /// Resets control fields to the original data
        /// </summary>
        public void Reset()
        {
            // TODO: Reset control fields to _originalData
        }

        /// <summary>
        /// Checks if there are changes and updates the parent file's dirty state
        /// </summary>
        public void HasChanges()
        {
            var parentObj = _nodes.Last().FindParentOfType(_parentType);
            if (parentObj != null)
            {
                dynamic parent = parentObj;
                parent.isDirty = !_data.Equals(_originalData);
            }
        }

        #region Helper Methods
        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = $"Rule Paragraph: {_data.RuleParagraph}";
            }
        }

        #endregion

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.RuleParagraph = textBox6.Text;
            UpdateTreeNodeText();
            HasChanges();

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.RuleText = textBox7.Text;
            HasChanges();
        }
    }
}