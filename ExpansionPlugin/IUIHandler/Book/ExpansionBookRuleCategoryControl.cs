using Day2eEditor;
using ExpansionPlugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionBookRuleCategoryControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionBookRuleCategory _data;
        private ExpansionBookRuleCategory _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionBookRuleCategoryControl()
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
            _data = data as ExpansionBookRuleCategory ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            textBox5.Text = _data.CategoryName;

            _suppressEvents = false;
        }

        /// <summary>
        /// Applies changes to the data and updates the original snapshot
        /// </summary>
        public void ApplyChanges()
        {
            _originalData = CloneData(_data);
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
        /// Clones the data for reset purposes
        /// </summary>
        private ExpansionBookRuleCategory CloneData(ExpansionBookRuleCategory data)
        {
            if (data == null)
                return null;

            return new ExpansionBookRuleCategory
            {
                CategoryName = data.CategoryName,
                Rules = new BindingList<ExpansionBookRule>(
                    data.Rules?.Select(r => new ExpansionBookRule
                    {
                        RuleParagraph = r.RuleParagraph,
                        RuleText = r.RuleText
                    }).ToList() ?? new List<ExpansionBookRule>())
            };
        }

        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = $"Category Name: {_data.CategoryName}";
            }
        }

        #endregion

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CategoryName = textBox5.Text;
            UpdateTreeNodeText();
            HasChanges();
        }
    }
}