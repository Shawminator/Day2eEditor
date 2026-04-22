using CoreUI.Forms;
using Day2eEditor;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class territorytypeTerritoryColourControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private territorytypeTerritory _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public territorytypeTerritoryColourControl()
        {
            InitializeComponent();
        }

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as territorytypeTerritory ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            string col = string.Format("{0:X}", _data.color);
            Color initialColor = ColorTranslator.FromHtml("#" + col.Substring(2));
            m_Color.BackColor = initialColor;

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                // TODO: Update _nodes.Last().Text based on _data
            }
        }
        private void m_Color_Click(object sender, EventArgs e)
        {
            string col = string.Format("{0:X}", _data.color);
            Color initialColor = ColorTranslator.FromHtml("#" + col.Substring(2));
            using (AdvancedColorPickerForm picker = new AdvancedColorPickerForm(initialColor))
            {
                picker.StartPosition = FormStartPosition.CenterParent;
                if (picker.ShowDialog() == DialogResult.OK)
                {
                    string colorHex = picker.SelectedColorHex;
                    long answer = Convert.ToInt64(colorHex.ToLower(), 16);
                    _data.color = answer;
                    Color selectedColor = ColorTranslator.FromHtml(colorHex);
                    m_Color.BackColor = selectedColor;
                }
            }
        }
    }
}