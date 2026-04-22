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
    public partial class eventspawnZoneControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private eventposdefEventZone _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public eventspawnZoneControl()
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
            _data = data as eventposdefEventZone ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            eventzonesminNUD.Value = _data.smin;
            eventzonesmaxNUD.Value = _data.smax;
            eventzonedminNUD.Value = _data.dmin;
            eventzonedmaxNUD.Value = _data.dmax;
            eventzonedNUD.Value = _data.r;

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                // TODO: Update _nodes.Last().Text based on _data
            }
        }
        private void eventzonesminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.smin = (int)eventzonesminNUD.Value;
        }
        private void eventzonesmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.smax = (int)eventzonesmaxNUD.Value;
        }
        private void eventzonedminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.dmin = (int)eventzonedminNUD.Value;
        }
        private void eventzonedmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.dmax = (int)eventzonedmaxNUD.Value;
        }
        private void eventzonedNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.r = (int)eventzonedNUD.Value;
        }
    }
}