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
    public partial class cfgweatherStormControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private weatherStorm _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgweatherStormControl()
        {
            InitializeComponent();
        }

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as weatherStorm ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            SdensityNUD.Value = _data.density;
            SthresholdNUD.Value = _data.threshold;
            StimeoutNUD.Value = _data.timeout;

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                // TODO: Update _nodes.Last().Text based on _data
            }
        }
        private void SdensityNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.density = SdensityNUD.Value;
        }
        private void SthresholdNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.threshold = SthresholdNUD.Value;
         }
        private void StimeoutNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.timeout = (int)StimeoutNUD.Value;
        }
    }
}