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
    public partial class cfgweatherWindDirectionControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private weatherWindDirection _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgweatherWindDirectionControl()
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
            _data = data as weatherWindDirection ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            WDCactualNUD.Value = _data.current.actual;
            WDCtimeNUD.Value = _data.current.time;
            WDCdurationNUD.Value = _data.current.duration;
            WDLminNUD.Value = _data.limits.min;
            WDLmaxNUD.Value = _data.limits.max;
            WDTLminNUD.Value = _data.timelimits.min;
            WDTLmaxNUD.Value = _data.timelimits.max;
            WDCLminNUD.Value = _data.changelimits.min;
            WDCLmaxNUD.Value = _data.changelimits.max;

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                // TODO: Update _nodes.Last().Text based on _data
            }
        }
        private void WDCactualNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.actual = WDCactualNUD.Value;
        }
        private void WDCtimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.time = (int)WDCtimeNUD.Value;
        }
        private void WDCdurationNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.duration = (int)WDCdurationNUD.Value;
        }
        private void WDLminNUD_ValueChanged(object sender, EventArgs e)
        {

            if (_suppressEvents) return;
            _data.limits.min = WDLminNUD.Value;
        }
        private void WDLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.limits.max = WDLmaxNUD.Value;
        }
        private void WDTLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.timelimits.min = (int)WDTLminNUD.Value;
        }
        private void WDTLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.timelimits.max = (int)WDTLmaxNUD.Value;
        }
        private void WDCLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.changelimits.min = WDCLminNUD.Value;
        }
        private void WDCLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.changelimits.max = WDCLmaxNUD.Value;

        }
    }
}