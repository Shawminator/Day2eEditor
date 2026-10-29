using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CaparePlugin
{
    /// <summary>
    /// Edits a single Vec3
    /// </summary>
    public partial class CapareVector3Control : UserControl, IUIHandler
    {
        public event Action DataChanged;
        // Text put in front of the position in the tree node, eg "Orientation: "
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string NodeTextPrefix { get; set; } = "";
        private Type _parentType;
        private Vec3 _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public CapareVector3Control()
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
            _data = data as Vec3 ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            SetNUD(XNUD, (decimal)_data.X);
            SetNUD(YNUD, (decimal)_data.Y);
            SetNUD(ZNUD, (decimal)_data.Z);

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
                _nodes.Last().Text = NodeTextPrefix + _data.GetString();
            }
        }
        private static void SetNUD(NumericUpDown nud, decimal value)
        {
            nud.Value = Math.Clamp(value, nud.Minimum, nud.Maximum);
        }

        #endregion

        private void XNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.X = (float)XNUD.Value;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void YNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Y = (float)YNUD.Value;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void ZNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Z = (float)ZNUD.Value;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
    }
}
