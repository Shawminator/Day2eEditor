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
    public partial class cfgweatherWindMagnitudeControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private weatherWindMagnitude _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgweatherWindMagnitudeControl()
        {
            InitializeComponent();
        }

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as weatherWindMagnitude ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            WMCactualNUD.Value = _data.current.actual;
            WMCtimeNUD.Value = _data.current.time;
            WMCdurationNUD.Value = _data.current.duration;
            WMLminNUD.Value = _data.limits.min;
            WMLmaxNUD.Value = _data.limits.max;
            WMTLminNUD.Value = _data.timelimits.min;
            WMTLmaxNUD.Value = _data.timelimits.max;
            WMCLminNUD.Value = _data.changelimits.min;
            WMCLmaxNUD.Value = _data.changelimits.max;

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                // TODO: Update _nodes.Last().Text based on _data
            }
        }
        private void WMCactualNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.actual = WMCactualNUD.Value;
        }
        private void WMCtimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.time = (int)WMCtimeNUD.Value;
        }
        private void WMCdurationNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.duration = (int)WMCdurationNUD.Value;
        }
        private void WMLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.limits.min = WMLminNUD.Value;
        }
        private void WMLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.limits.max = WMLmaxNUD.Value;
        }
        private void WMTLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.timelimits.min = (int)WMTLminNUD.Value;
         }
        private void WMTLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.timelimits.max = (int)WMTLmaxNUD.Value;
        }
        private void WMCLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.changelimits.min = WMCLminNUD.Value;
        }
        private void WMCLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.changelimits.max = WMCLmaxNUD.Value;
        }
    }
}