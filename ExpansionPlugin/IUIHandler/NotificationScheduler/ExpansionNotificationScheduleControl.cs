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
    public partial class ExpansionNotificationScheduleControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionNotificationSchedule _data;
        private ExpansionNotificationSchedule _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionNotificationScheduleControl()
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
            _data = data as ExpansionNotificationSchedule ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = _data.Clone(); // Store original data for reset

            _suppressEvents = true;

            BindingList<string> Icons = new BindingList<string>(File.ReadAllLines("Data\\ExpansionIconnames.txt").ToList());
            NotfifcationIconComboBox.DataSource = Icons;

            NSTitleTB.Text = _data.Title;
            DateTime Dtime = DateTime.Now.Date;
            Dtime = Dtime.AddHours((int)_data.Hour);
            Dtime = Dtime.AddMinutes((int)_data.Minute);
            Dtime = Dtime.AddSeconds((int)_data.Second);
            NSTimeTP.Value = Dtime;
            NSTextTB.Text = _data.Text;
            NotfifcationIconComboBox.SelectedIndex = NotfifcationIconComboBox.FindStringExact(_data.Icon);
            ColorPB.BackColor = GetColor(_data.Color);
            GetIcon();
            dateTimePicker1.Value = DateTime.Now;
            _suppressEvents = false;
        }
        private Color GetColor(string hexColor)
        {
            string formattedColor = "#" + hexColor.Substring(6) + hexColor.Remove(6, 2);
            Color selectedColor = ColorTranslator.FromHtml(formattedColor);
            return selectedColor;
        }
        private void GetIcon()
        {
            string iconname = _data.Icon.Replace("/", "");
            var resourceName = $"ExpansionPlugin.Icons.{iconname}.png";
            var stream = ResourceHelper.OpenEmbeddedStream(resourceName);
            if (stream != null)
            {
                Bitmap image = new Bitmap(Image.FromStream(stream));
                Image image2 = ResourceHelper.MultiplyColorToBitmap(image, GetColor(_data.Color), 200, true);
                Image image3 = resizeImage(image2, new Size(128, 128));
                pictureBox1.Image = image3;
            }
            else
            {
                pictureBox1.Image = null;
            }
        }
        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }
        /// <summary>
        /// Applies changes to the data and updates the original snapshot
        /// </summary>
        public void ApplyChanges()
        {
            _originalData = _data.Clone();
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

        private void NSTitleTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Title = NSTitleTB.Text;
            UpdateTreeNodeText();
            HasChanges();
        }

        private void NSTimeTP_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            DateTime Dtime = NSTimeTP.Value;
            _data.Hour = Dtime.Hour;
            _data.Minute = Dtime.Minute;
            _data.Second = Dtime.Second;
            HasChanges();
        }

        private void NSTextTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Text = NSTextTB.Text;
            HasChanges();
        }

        private void NotfifcationIconComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Icon = NotfifcationIconComboBox.SelectedItem.ToString();
            GetIcon();
            HasChanges();
        }

        private void ColorPB_Click(object sender, EventArgs e)
        {
            Color startColor = ColorPB.BackColor;
            using (AdvancedColorPickerForm picker = new AdvancedColorPickerForm(startColor))
            {
                picker.StartPosition = FormStartPosition.CenterParent;
                if (picker.ShowDialog() == DialogResult.OK)
                {
                    string colorHex = picker.SelectedColorHex;
                    Color selectedColor = ColorTranslator.FromHtml(colorHex);
                    ColorPB.BackColor = selectedColor;
                    _data.Color = colorHex.Substring(4, 6) + colorHex.Substring(2, 2);
                    GetIcon();
                    HasChanges();
                }
            }
        }

        private void darkButton81_Click(object sender, EventArgs e)
        {
            DateTime localtime = dateTimePicker1.Value;
            darkLabel252.Text = "UTC Time : " + TimeZoneInfo.ConvertTimeToUtc(localtime, TimeZoneInfo.Local).ToString();
        }
    }
}