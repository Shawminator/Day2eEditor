using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class cfggameplaySandstorm : UserControl, IUIHandler
    {
        private Type _parentType;
        private ITEM_SandstormData _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfggameplaySandstorm()
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
            _data = data as ITEM_SandstormData ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            sandstormFrequencyNUD.Value = _data.sandstormFrequency;

            _suppressEvents = false;
        }

        private void sandstormFrequencyNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.sandstormFrequency = sandstormFrequencyNUD.Value;
        }
    }
}