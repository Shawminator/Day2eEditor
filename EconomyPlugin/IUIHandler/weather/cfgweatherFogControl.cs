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
    public partial class cfgweatherFogControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private weatherFog _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgweatherFogControl()
        {
            InitializeComponent();
        }

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as weatherFog ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            FCactualNUD.Value = _data.current.actual;
            FCtimeNUD.Value = _data.current.time;
            FCdurationNUD.Value = _data.current.duration;
            FLminNUD.Value = _data.limits.min;
            FLmaxNUD.Value = _data.limits.max;
            FTLminNUD.Value = _data.timelimits.min;
            FTLmaxNUD.Value = _data.timelimits.max;
            FCLminNUD.Value = _data.changelimits.min;
            FCLmaxNUD.Value = _data.changelimits.max;

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                // TODO: Update _nodes.Last().Text based on _data
            }
        }
        private void FCactualNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
           _data.current.actual = FCactualNUD.Value;
        }
        private void FCtimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
           _data.current.time = (int)FCtimeNUD.Value;
        }
        private void FCdurationNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
           _data.current.duration = (int)FCdurationNUD.Value;
        }
        private void FLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
           _data.limits.min = FLminNUD.Value;
        }
        private void FLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
           _data.limits.max = FLmaxNUD.Value;
        }
        private void FTLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
           _data.timelimits.min = (int)FTLminNUD.Value;
        }
        private void FTLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
           _data.timelimits.max = (int)FTLmaxNUD.Value;
        }
        private void FCLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
           _data.changelimits.min = FCLminNUD.Value;
        }
        private void FCLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
           _data.changelimits.max = FCLmaxNUD.Value;
        }
    }
}