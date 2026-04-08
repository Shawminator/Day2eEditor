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
    public partial class ExpansionMarketTraderZoneStockControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionMarketTraderZone _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionMarketTraderZoneStockControl()
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

            dataGridView1.RowHeadersVisible = false;

            dataGridView1.BackgroundColor = Color.FromArgb(60, 63, 65);

            dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(60, 63, 65);
            dataGridView1.DefaultCellStyle.ForeColor = SystemColors.Control;

            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(75, 110, 175); 
            dataGridView1.DefaultCellStyle.SelectionForeColor = SystemColors.Control;

            dataGridView1.EnableHeadersVisualStyles = false;

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 45, 48);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.Control;

            dataGridView1.GridColor = SystemColors.Control;

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(65, 68, 70);

            dataGridView1.EnableHeadersVisualStyles = false;

            _suppressEvents = true;

            dataGridView1.DataSource = _data.stockList;
            dataGridView1.Columns["Name"].ReadOnly = true;

            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            dataGridView1.Columns["Name"].Width = dataGridView1.Width - SystemInformation.VerticalScrollBarWidth - 105;
            dataGridView1.Columns["Quantity"].Width = 100;

            dataGridView1.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["Quantity"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

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
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Quantity")
            {
                if (!int.TryParse(e.FormattedValue.ToString(), out int value) || value < 0)
                {
                    e.Cancel = true;
                    MessageBox.Show("Please enter a valid non-negative number.");
                }
            }
        }
    }
}