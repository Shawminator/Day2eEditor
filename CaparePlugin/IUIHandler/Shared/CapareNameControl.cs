using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CaparePlugin
{
    /// <summary>
    /// Edits the name of a named list entry (reward tables, loot tables, item sets...)
    /// </summary>
    public partial class CapareNameControl : UserControl, IUIHandler
    {
        public event Action DataChanged;
        private Type _parentType;
        private CapareNamedItem _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public CapareNameControl()
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
            _data = data as CapareNamedItem ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            NameTB.Text = _data.Name ?? "";

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
                _nodes.Last().Text = _data.Name;
            }
        }
        private static void SetNUD(NumericUpDown nud, decimal value)
        {
            nud.Value = Math.Clamp(value, nud.Minimum, nud.Maximum);
        }

        #endregion

        private void NameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Name = NameTB.Text;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
    }
}
