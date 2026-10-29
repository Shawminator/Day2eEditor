using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CaparePlugin
{
    /// <summary>
    /// Static location music track
    /// </summary>
    public partial class CapareDynamicMusicLocationControl : UserControl, IUIHandler
    {
        public event Action DataChanged;
        private Type _parentType;
        private CapareDynamicMusicLocationStatic _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public CapareDynamicMusicLocationControl()
        {
            InitializeComponent();
            m_TimeOfDayComboBox.Items.AddRange(Enum.GetValues(typeof(CapareDynamicMusicTimeOfDayType)).Cast<object>().ToArray());
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
            _data = data as CapareDynamicMusicLocationStatic ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            m_SoundSetTB.Text = _data.m_SoundSet ?? "";
            m_TimeOfDayComboBox.SelectedItem = (CapareDynamicMusicTimeOfDayType)_data.m_TimeOfDay;
            m_runImmediatelyCB.Checked = _data.m_runImmediately;

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
        private static void SetNUD(NumericUpDown nud, decimal value)
        {
            nud.Value = Math.Clamp(value, nud.Minimum, nud.Maximum);
        }

        #endregion

        private void m_SoundSetTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_SoundSet = m_SoundSetTB.Text;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void m_TimeOfDayComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (m_TimeOfDayComboBox.SelectedItem is CapareDynamicMusicTimeOfDayType tod)
                _data.m_TimeOfDay = (int)tod;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
        private void m_runImmediatelyCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_runImmediately = m_runImmediatelyCB.Checked;
            UpdateTreeNodeText();
            DataChanged?.Invoke();
        }
    }
}
