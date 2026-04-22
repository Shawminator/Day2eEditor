using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class cfgweatherSnowfallControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private weatherSnowfall _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgweatherSnowfallControl()
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
            _data = data as weatherSnowfall ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            SCactualNUD.Value = _data.current.actual;
            SCtimeNUD.Value = _data.current.time;
            SCdurationNUD.Value = _data.current.duration;
            SLminNUD.Value = _data.limits.min;
            SLmaxNUD.Value = _data.limits.max;
            STLminNUD.Value = _data.timelimits.min;
            STLmaxNUD.Value = _data.timelimits.max;
            SCLminNUD.Value = _data.changelimits.min;
            SCLmaxNUD.Value = _data.changelimits.max;
            STmaxNUD.Value = _data.thresholds.max;
            STminNUD.Value = _data.thresholds.min;
            STendNUD.Value = _data.thresholds.end;

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                // TODO: Update _nodes.Last().Text based on _data
            }
        }
        private void SCactualNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.actual = SCactualNUD.Value;
        }
        private void SCtimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.time = (int)SCtimeNUD.Value;
        }
        private void SCdurationNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.current.duration = (int)SCdurationNUD.Value;
        }
        private void SLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.limits.min = SLminNUD.Value;
        }
        private void SLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.limits.max = SLmaxNUD.Value;
        }
        private void STLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.timelimits.min = (int)STLminNUD.Value;
        }
        private void STLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.timelimits.max = (int)STLmaxNUD.Value;
        }
        private void SCLminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.changelimits.min = SCLminNUD.Value;
        }
        private void SCLmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.changelimits.max = SCLmaxNUD.Value;
        }
        private void STminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.thresholds.min = STminNUD.Value;
        }
        private void STmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.thresholds.max = STmaxNUD.Value;
        }
        private void STendNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.thresholds.end = (int)STendNUD.Value;
        }
    }
}