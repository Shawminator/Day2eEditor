using CoreUI.Forms;
using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionMapServerMarkerInfoControl : UserControl, IUIHandler
    {
        public event Action<ExpansionServerMarkerData> MarkerChanged;
        private Type _parentType;
        private ExpansionServerMarkerData _data;
        private ExpansionServerMarkerData _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionMapServerMarkerInfoControl()
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
            _data = data as ExpansionServerMarkerData ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;
            BindingList<string> Icons = new BindingList<string>(File.ReadAllLines("Data\\ExpansionIconnames.txt").ToList());
            comboBox3.DataSource = Icons;
            comboBox1.DisplayMember = "Description";
            comboBox1.ValueMember = "Value";
            comboBox1.DataSource = Enum.GetValues(typeof(MapMarkerVisibility))
                .Cast<Enum>()
                .Select(value => new
                {
                    (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                    value
                })
                .OrderBy(item => item.value)
                .ToList();

            comboBox1.SelectedValue = (MapMarkerVisibility)_data.m_Visibility;
            comboBox3.SelectedIndex = comboBox3.FindStringExact(_data.m_IconName);
            textBox9.Text = _data.m_UID;
            textBox11.Text = _data.m_Text;
            numericUpDown24.Value = (decimal)_data.m_Position[0];
            numericUpDown25.Value = (decimal)_data.m_Position[1];
            numericUpDown26.Value = (decimal)_data.m_Position[2];
            m_Is3DCB.Checked = _data.m_Is3D == 1 ? true : false;
            m_LockedCB.Checked = _data.m_Locked == 1 ? true : false;
            m_PersistCB.Checked = _data.m_Persist == 1 ? true : false;
            Color CompassColor = Color.FromArgb((int)_data.m_Color);
            m_ColorPB.BackColor = CompassColor;

            _suppressEvents = false;
        }

        /// <summary>
        /// Applies changes to the data and updates the original snapshot
        /// </summary>
        public void ApplyChanges()
        {
            _originalData = CloneData(_data);
        }

        /// <summary>
        /// Resets control fields to the original data
        /// </summary>
        public void Reset()
        {
            // TODO: Reset control fields to _originalData
        }

        /// <summary>
        /// Checks if there are changes and updates the parent file's dirty state
        /// </summary>
        public void HasChanges()
        {
            var parentObj = _nodes.Last().FindParentOfType(_parentType);
            if (parentObj != null)
            {
                dynamic parent = parentObj;
                parent.isDirty = !_data.Equals(_originalData);
            }
        }

        #region Helper Methods

        /// <summary>
        /// Clones the data for reset purposes
        /// </summary>
        private ExpansionServerMarkerData CloneData(ExpansionServerMarkerData m)
        {
            // TODO: Implement actual cloning logic
            return new ExpansionServerMarkerData
            {
                m_UID = m.m_UID,
                m_Visibility = m.m_Visibility,
                m_Is3D = m.m_Is3D,
                m_Text = m.m_Text,
                m_IconName = m.m_IconName,
                m_Color = m.m_Color,
                m_Position = m.m_Position != null ? (float[])m.m_Position.Clone() : null, // deep clone array
                m_Locked = m.m_Locked,
                m_Persist = m.m_Persist
            };
        }

        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = _data.m_UID;
            }
        }

        #endregion

        private void m_PersistCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_Persist = m_PersistCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void m_LockedCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_Locked = m_LockedCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void m_Is3DCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_Is3D = m_Is3DCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_UID = textBox9.Text;
            HasChanges();
            UpdateTreeNodeText();
        }
        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_Text = textBox11.Text;
            HasChanges();

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_Visibility = (int)comboBox1.SelectedValue;
            HasChanges();
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_IconName = comboBox3.SelectedItem.ToString();
            HasChanges();
            MarkerChanged?.Invoke(_data);
        }
        private void m_ColorPB_Click(object sender, EventArgs e)
        {
            Color initialColor = Color.FromArgb((int)_data.m_Color);
            using (AdvancedColorPickerForm picker = new AdvancedColorPickerForm(initialColor))
            {
                picker.StartPosition = FormStartPosition.CenterParent;
                if (picker.ShowDialog() == DialogResult.OK)
                {
                    Color color = picker.SelectedColor;
                    _data.m_Color = color.ToArgb();
                    m_ColorPB.BackColor = color;
                    HasChanges();
                    MarkerChanged?.Invoke(_data);
                }
            }
            
        }
        private void numericUpDown24_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_Position[0] = (float)numericUpDown24.Value;
            HasChanges();
            MarkerChanged?.Invoke(_data);
        }
        private void numericUpDown25_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_Position[1] = (float)numericUpDown25.Value;
            HasChanges();
        }
        private void numericUpDown26_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_Position[2] = (float)numericUpDown26.Value;
            HasChanges();
            MarkerChanged?.Invoke(_data);
        }
    }
}