using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CaparePlugin
{
    /// <summary>
    /// Salvage time = BaseSeconds + scrap * SecondsPerScrap + kg * SecondsPerKg, clamped to Min..Max
    /// </summary>
    public partial class CapareSalvageTimingControl : UserControl, IUIHandler
    {
        public event Action DataChanged;
        private Type _parentType;
        private CapareSalvageTimingSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public CapareSalvageTimingControl()
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
            _data = data as CapareSalvageTimingSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            SetNUD(BaseSecondsNUD, (decimal)_data.BaseSeconds);
            SetNUD(SecondsPerScrapNUD, (decimal)_data.SecondsPerScrap);
            SetNUD(SecondsPerKgNUD, (decimal)_data.SecondsPerKg);
            SetNUD(MinSecondsNUD, (decimal)_data.MinSeconds);
            SetNUD(MaxSecondsNUD, (decimal)_data.MaxSeconds);

            _suppressEvents = false;
        }

        #region Helper Methods
        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            // Node text does not depend on this data
        }
        private static void SetNUD(NumericUpDown nud, decimal value)
        {
            nud.Value = Math.Clamp(value, nud.Minimum, nud.Maximum);
        }

        #endregion

        private void BaseSecondsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.BaseSeconds = (float)BaseSecondsNUD.Value;
            DataChanged?.Invoke();
        }
        private void SecondsPerScrapNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SecondsPerScrap = (float)SecondsPerScrapNUD.Value;
            DataChanged?.Invoke();
        }
        private void SecondsPerKgNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SecondsPerKg = (float)SecondsPerKgNUD.Value;
            DataChanged?.Invoke();
        }
        private void MinSecondsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MinSeconds = (float)MinSecondsNUD.Value;
            DataChanged?.Invoke();
        }
        private void MaxSecondsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxSeconds = (float)MaxSecondsNUD.Value;
            DataChanged?.Invoke();
        }
    }
}
