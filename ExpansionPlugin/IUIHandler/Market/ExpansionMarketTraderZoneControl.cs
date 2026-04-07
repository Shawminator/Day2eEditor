using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionMarketTraderZoneControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionMarketTraderZone _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionMarketTraderZoneControl()
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
            _data = data as ExpansionMarketTraderZone ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            FilenameTB.Text = Path.GetFileNameWithoutExtension(_data.FileName);
            m_DisplayNameTB.Text = _data.m_DisplayName;
            BuyPricePercentNUD.Value = (int)_data.BuyPricePercent;
            SellPricePercentNUD.Value = (int)_data.SellPricePercent;

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
                _nodes.Last().Text = _data.FileName;
            }
        }

        #endregion

        private void FilenameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            string dirName = Path.GetDirectoryName(_data._path);
            string newFilename = FilenameTB.Text + ".JSON";
            _data.SetPath(Path.Combine(dirName, newFilename));
            UpdateTreeNodeText();
        }

        private void m_DisplayNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.m_DisplayName = m_DisplayNameTB.Text;
        }

        private void BuyPricePercentNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.BuyPricePercent = (decimal)BuyPricePercentNUD.Value;
        }

        private void SellPricePercentNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SellPricePercent = (decimal)SellPricePercentNUD.Value;
        }
    }
}