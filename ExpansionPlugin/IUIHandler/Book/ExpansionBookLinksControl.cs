using CoreUI.Forms;
using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionBookLinksControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionBookLink _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionBookLinksControl()
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
            _data = data as ExpansionBookLink ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;
            BindingList<string> Icons = new BindingList<string>(File.ReadAllLines("Data\\ExpansionIconnames.txt").ToList());
            comboBox5.DataSource = Icons;
            textBox12.Text = _data.Name;
            textBox13.Text = _data.URL;
            comboBox5.SelectedIndex = comboBox5.FindStringExact(_data.IconName);
            Color selectedColor = Color.FromArgb(_data.IconColor);
            LinkIconColour.BackColor = selectedColor;
            GetIcon();
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
                // TODO: Update _nodes.Last().Text based on _data
            }
        }

        #endregion

        private void LinkIconColour_Click(object sender, EventArgs e)
        {
            Color initialColor = Color.FromArgb(_data.IconColor);
            using (AdvancedColorPickerForm picker = new AdvancedColorPickerForm(initialColor))
            {
                picker.StartPosition = FormStartPosition.CenterParent;
                if (picker.ShowDialog() == DialogResult.OK)
                {
                    Color color = picker.SelectedColor;
                    _data.IconColor = color.ToArgb();
                    LinkIconColour.BackColor = color;
                    GetIcon();
                    
                }
            }
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Name = textBox12.Text;
            
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.URL = textBox13.Text;
            
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.IconName = comboBox5.SelectedItem.ToString();
            GetIcon();
            
        }

        private void GetIcon()
        {
            string iconname = _data.IconName.Replace("/","");
            var resourceName = $"ExpansionPlugin.Icons.{iconname}.png";
            var stream = ResourceHelper.OpenEmbeddedStream(resourceName);
            if (stream != null)
            {
                Bitmap image = new Bitmap(Image.FromStream(stream));
                Image image2 = ResourceHelper.MultiplyColorToBitmap(image, Color.FromArgb(_data.IconColor), 200, true);
                Image image3 = resizeImage(image2, new Size(128,128));
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
    }
}