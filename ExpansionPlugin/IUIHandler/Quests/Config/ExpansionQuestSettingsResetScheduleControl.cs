using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionQuestSettingsResetScheduleControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionQuestSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionQuestSettingsResetScheduleControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Returns the UserControl instance
        /// </summary>
        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as ExpansionQuestSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            WeeklyResetDayCB.Text = _data.WeeklyResetDay;

            WeeklyResetHourNUD.Value = (int)_data.WeeklyResetHour;
            WeeklyResetMinuteNUD.Value = (int)_data.WeeklyResetMinute;

            DailyResetHourNUD.Value = (int)_data.DailyResetHour;
            DailyResetMinuteNUD.Value = (int)_data.DailyResetMinute;

            UseUTCTimeCB.Checked = _data.UseUTCTime == 1 ? true : false;

            _suppressEvents = false;
        }
        private void WeeklyResetDayCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.WeeklyResetDay = WeeklyResetDayCB.Text;
        }
        private void WeeklyResetHourNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.WeeklyResetHour = (int)WeeklyResetHourNUD.Value;
        }
        private void WeeklyResetMinuteNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.WeeklyResetMinute = (int)WeeklyResetMinuteNUD.Value;
        }
        private void DailyResetHourNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.DailyResetHour = (int)DailyResetHourNUD.Value;
        }
        private void DailyResetMinuteNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.DailyResetMinute = (int)DailyResetMinuteNUD.Value;
        }
        private void UseUTCTimeCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.UseUTCTime = UseUTCTimeCB.Checked ? 1 : 0;
        }
    }
}