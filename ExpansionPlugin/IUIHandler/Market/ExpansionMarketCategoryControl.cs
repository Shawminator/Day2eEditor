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
    public partial class ExpansionMarketCategoryControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionMarketCategory _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionMarketCategoryControl()
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
            _data = data as ExpansionMarketCategory ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;
            BindingList<string> Icons = new BindingList<string>(File.ReadAllLines("Data\\ExpansionIconnames.txt").ToList());
            IconCB.DataSource = Icons;
            textBox11.Text = Path.GetFileNameWithoutExtension(_data.FileName);
            textBox9.Text = _data.DisplayName;
            IconCB.SelectedIndex = IconCB.FindStringExact(_data.Icon);
            InitStockPercentNUD.Value = (decimal)_data.InitStockPercent;
            IsExchangeCB.Checked = _data.IsExchange == 1 ? true : false;
            SetColor(_data.Color, ColorPB);
            _suppressEvents = false;
        }
        private void SetColor(string hexColor, PictureBox targetPB)
        {
            string formattedColor = "#" + hexColor.Substring(6) + hexColor.Remove(6, 2);
            Color selectedColor = ColorTranslator.FromHtml(formattedColor);
            targetPB.BackColor = selectedColor;
        }
        #region Helper Methods
        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = _data.FileName;
            }
        }

        #endregion

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            string dirName = Path.GetDirectoryName(_data._path);
            string newFilename = textBox11.Text + ".JSON";
            _data.SetPath(Path.Combine(dirName, newFilename));
            UpdateTreeNodeText();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DisplayName = textBox9.Text;
        }

        private void CategorycolourPB_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox pb)
            {
                HandleColorChange(pb);
            }

        }
        private void HandleColorChange(PictureBox pb)
        {
            Color startColor = pb.BackColor;
            using (AdvancedColorPickerForm picker = new AdvancedColorPickerForm(startColor))
            {
                picker.StartPosition = FormStartPosition.CenterParent;
                if (picker.ShowDialog() == DialogResult.OK)
                {
                    string colorHex = picker.SelectedColorHex;
                    Color selectedColor = ColorTranslator.FromHtml(colorHex);
                    pb.BackColor = selectedColor;

                    string propertyName = pb.Name.Replace("PB", "");
                    var prop = typeof(ExpansionMarketCategory).GetProperty(propertyName);
                    if (prop != null)
                    {
                        prop.SetValue(_data, colorHex.Substring(4, 6) + colorHex.Substring(2, 2));
                    }
                }
            }
        }
        private void IsExchangeCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.IsExchange = IsExchangeCB.Checked == true ? 1 : 0;
        }

        private void InitStockPercentNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.InitStockPercent = (decimal)InitStockPercentNUD.Value;
        }

        private void IconCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Icon = IconCB.SelectedItem.ToString();
        }
    }
}