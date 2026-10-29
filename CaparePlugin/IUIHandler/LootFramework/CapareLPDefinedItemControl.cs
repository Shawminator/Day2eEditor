using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CaparePlugin
{
    /// <summary>
    /// Defined item with magazines and attachments
    /// </summary>
    public partial class CapareLPDefinedItemControl : UserControl, IUIHandler
    {
        public event Action DataChanged;
        private Type _parentType;
        private CapareLPdefinedItem _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public CapareLPDefinedItemControl()
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
            _data = data as CapareLPdefinedItem ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            DefineNameTB.Text = _data.DefineName ?? "";
            ItemTB.Text = _data.Item ?? "";
            SpawnExactCB.Checked = _data.SpawnExact;

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
                _nodes.Last().Text = _data.ToString();
            }
        }
        private static void SetNUD(NumericUpDown nud, decimal value)
        {
            nud.Value = Math.Clamp(value, nud.Minimum, nud.Maximum);
        }

        #endregion

        private void DefineNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DefineName = DefineNameTB.Text;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void ItemTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Item = ItemTB.Text;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void SpawnExactCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SpawnExact = SpawnExactCB.Checked;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
    }
}
