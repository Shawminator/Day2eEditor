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
    public partial class cfgweatherRainControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private weatherRain _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgweatherRainControl()
        {
            InitializeComponent();
        }

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as weatherRain ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            RCactualNUD.Value = _data.current.actual;
            RCtimeNUD.Value = _data.current.time;
            RCdurationNUD.Value = _data.current.duration;
            RLminNUD.Value = _data.limits.min;
            RLmaxNUD.Value = _data.limits.max;
            RTLminNUD.Value = _data.timelimits.min;
            RTLmaxNUD.Value = _data.timelimits.max;
            RCLminNUD.Value = _data.changelimits.min;
            RCLmaxNUD.Value = _data.changelimits.max;
            RTmaxNUD.Value = _data.thresholds.max;
            RTminNUD.Value = _data.thresholds.min;
            RTendNUD.Value = _data.thresholds.end;

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                // TODO: Update _nodes.Last().Text based on _data
            }
        }
        private void RCactualNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.actual = RCactualNUD.Value;
        }
        private void RCtimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.time = (int)RCtimeNUD.Value;
        }
        private void RCdurationNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.duration = (int)RCdurationNUD.Value;
        }
        private void RLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.limits.min = RLminNUD.Value;
        }
        private void RLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.limits.min = RLmaxNUD.Value;
        }
        private void RTLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.timelimits.min = (int)RTLminNUD.Value;
        }
        private void RTLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.timelimits.max = (int)RTLmaxNUD.Value;
        }
        private void RCLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.changelimits.min = RCLminNUD.Value;
        }
        private void RCLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.changelimits.max = RCLmaxNUD.Value;
        }
        private void RTminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.thresholds.min = RTminNUD.Value;
        }
        private void RTmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.thresholds.max = RTmaxNUD.Value;
        }
        private void RTendNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.thresholds.end = (int)RTendNUD.Value;
        }
    }
}