using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionRaidSettingsRaidScheduleControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionRaidSchedule _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionRaidSettingsRaidScheduleControl()
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
            _data = data as ExpansionRaidSchedule ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            List<string> _Weekdays = new List<string>() { ""};
            _Weekdays.AddRange(ExpansionRaidSchedule.WEEKDAYS);
            WeekdayCB.DataSource = _Weekdays;
            WeekdayCB.SelectedIndex = WeekdayCB.FindStringExact(_data.Weekday); ;
            StartHourNUD.Value = (int)_data.StartHour;
            StartMinuteNUD.Value = (int)_data.StartMinute;
            DurationMinutesNUD.Value = (int)_data.DurationMinutes;

            _suppressEvents = false;
        }

        #region Helper Methods

        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = _data.ToString();
            }
        }

        #endregion
        private void WeekdayCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Weekday = WeekdayCB.SelectedItem.ToString();
            UpdateTreeNodeText();
        }

        private void StartHourNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.StartHour = (int)StartHourNUD.Value;
            UpdateTreeNodeText();
        }

        private void StartMinuteNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.StartMinute = (int)StartMinuteNUD.Value;
            UpdateTreeNodeText();
        }

        private void DurationMinutesNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DurationMinutes = (int)DurationMinutesNUD.Value;
            UpdateTreeNodeText();
        }


    }
}