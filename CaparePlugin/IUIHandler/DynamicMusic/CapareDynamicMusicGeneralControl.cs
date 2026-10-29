using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CaparePlugin
{
    /// <summary>
    /// Wait times for each music category
    /// </summary>
    public partial class CapareDynamicMusicGeneralControl : UserControl, IUIHandler
    {
        public event Action DataChanged;
        private Type _parentType;
        private CapareDynamicMusicSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public CapareDynamicMusicGeneralControl()
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
            _data = data as CapareDynamicMusicSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            SetNUD(GlobalMinWaitTimeSecondsNUD, (decimal)_data.GlobalMinWaitTimeSeconds);
            SetNUD(GlobalMaxWaitTimeSecondsNUD, (decimal)_data.GlobalMaxWaitTimeSeconds);
            SetNUD(MenuMinWaitTimeSecondsNUD, (decimal)_data.MenuMinWaitTimeSeconds);
            SetNUD(MenuMaxWaitTimeSecondsNUD, (decimal)_data.MenuMaxWaitTimeSeconds);
            SetNUD(TimeMinWaitTimeSecondsNUD, (decimal)_data.TimeMinWaitTimeSeconds);
            SetNUD(TimeMaxWaitTimeSecondsNUD, (decimal)_data.TimeMaxWaitTimeSeconds);
            UsedeafultTODTracksCB.Checked = _data.UsedeafultTODTracks;
            SetNUD(StaticMinWaitTimeSecondsNUD, (decimal)_data.StaticMinWaitTimeSeconds);
            SetNUD(StaticMaxWaitTimeSecondsNUD, (decimal)_data.StaticMaxWaitTimeSeconds);
            SetNUD(StaticPreviousTrackFadeoutSecondsNUD, (decimal)_data.StaticPreviousTrackFadeoutSeconds);

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

        private void GlobalMinWaitTimeSecondsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.GlobalMinWaitTimeSeconds = (float)GlobalMinWaitTimeSecondsNUD.Value;
            DataChanged?.Invoke();
        }
        private void GlobalMaxWaitTimeSecondsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.GlobalMaxWaitTimeSeconds = (float)GlobalMaxWaitTimeSecondsNUD.Value;
            DataChanged?.Invoke();
        }
        private void MenuMinWaitTimeSecondsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MenuMinWaitTimeSeconds = (float)MenuMinWaitTimeSecondsNUD.Value;
            DataChanged?.Invoke();
        }
        private void MenuMaxWaitTimeSecondsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MenuMaxWaitTimeSeconds = (float)MenuMaxWaitTimeSecondsNUD.Value;
            DataChanged?.Invoke();
        }
        private void TimeMinWaitTimeSecondsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.TimeMinWaitTimeSeconds = (float)TimeMinWaitTimeSecondsNUD.Value;
            DataChanged?.Invoke();
        }
        private void TimeMaxWaitTimeSecondsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.TimeMaxWaitTimeSeconds = (float)TimeMaxWaitTimeSecondsNUD.Value;
            DataChanged?.Invoke();
        }
        private void UsedeafultTODTracksCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UsedeafultTODTracks = UsedeafultTODTracksCB.Checked;
            DataChanged?.Invoke();
        }
        private void StaticMinWaitTimeSecondsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.StaticMinWaitTimeSeconds = (float)StaticMinWaitTimeSecondsNUD.Value;
            DataChanged?.Invoke();
        }
        private void StaticMaxWaitTimeSecondsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.StaticMaxWaitTimeSeconds = (float)StaticMaxWaitTimeSecondsNUD.Value;
            DataChanged?.Invoke();
        }
        private void StaticPreviousTrackFadeoutSecondsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.StaticPreviousTrackFadeoutSeconds = (float)StaticPreviousTrackFadeoutSecondsNUD.Value;
            DataChanged?.Invoke();
        }
    }
}
