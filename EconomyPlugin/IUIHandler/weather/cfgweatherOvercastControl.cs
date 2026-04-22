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
    public partial class cfgweatherOvercastControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private weatherOvercast _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgweatherOvercastControl()
        {
            InitializeComponent();
        }

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as weatherOvercast ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            OCactualNUD.Value = _data.current.actual;
            OCtimeNUD.Value = _data.current.time;
            OCdurationNUD.Value = _data.current.duration;
            OLminNUD.Value = _data.limits.min;
            OLmaxNUD.Value = _data.limits.max;
            OTLminNUD.Value = _data.timelimits.min;
            OTLmaxNUD.Value = _data.timelimits.max;
            OCLminNUD.Value = _data.changelimits.min;
            OCLmaxNUD.Value = _data.changelimits.max;

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                // TODO: Update _nodes.Last().Text based on _data
            }
        }
        private void OCactualNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.actual = OCactualNUD.Value;
        }
        private void OCtimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.time = (int)OCtimeNUD.Value;
        }
        private void OCdurationNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.duration = (int)OCdurationNUD.Value;
        }
        private void OLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.limits.min = OLminNUD.Value;
        }
        private void OLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.limits.max = OLmaxNUD.Value;
        }
        private void OTLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.timelimits.min = (int)OTLminNUD.Value;
        }
        private void OTLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.timelimits.max = (int)OTLmaxNUD.Value;
        }
        private void OCLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.changelimits.min = OCLminNUD.Value;
        }
        private void OCLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.changelimits.max = OCLmaxNUD.Value;
        }
    }
}