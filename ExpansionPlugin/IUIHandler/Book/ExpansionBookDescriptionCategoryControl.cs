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
    public partial class ExpansionBookDescriptionCategoryControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionBookDescriptionCategory _data;
        private ExpansionBookDescriptionCategory _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;
        private ExpansionBookDescription? currentdecriptiontext;

        private void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            ListBox lb = sender as ListBox;
            e.DrawBackground();
            Brush myBrush = Brushes.Black;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            }
            else
            {
                myBrush = Brushes.White;
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(60, 63, 65)), e.Bounds);
            }
            e.Graphics.DrawString(lb.Items[e.Index].ToString(), e.Font, myBrush, e.Bounds);
            e.DrawFocusRectangle();
        }
        public ExpansionBookDescriptionCategoryControl()
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
            _data = data as ExpansionBookDescriptionCategory ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = _data.Clone();

            _suppressEvents = true;

            textBox10.Text = _data.CategoryName;

            int i = 0;
            foreach (ExpansionBookDescription dt in _data.Descriptions)
            {
                dt.DTName = "Decription Text " + i.ToString();
                i++;
            }

            textBox8.Text = "";

            listBox12.DisplayMember = "DisplayName";
            listBox12.ValueMember = "Value";
            listBox12.DataSource = _data.Descriptions;

            _suppressEvents = false;
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
                _nodes.Last().Text = $"Category Name: {_data.CategoryName}";
            }
        }

        #endregion

        private void listBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox12.Items.Count == 0) { return; }
            if (listBox12.SelectedItem == null) { return; }

            currentdecriptiontext = listBox12.SelectedItem as ExpansionBookDescription;
            _suppressEvents = true;
            textBox8.Text = currentdecriptiontext.DescriptionText.Replace("<p>", "").Replace("</p>", "");
            _suppressEvents = false;
        }
        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CategoryName = textBox10.Text;
            HasChanges();
            UpdateTreeNodeText();
        }
        private void darkButton20_Click(object sender, EventArgs e)
        {
            _data.Descriptions.Add(new ExpansionBookDescription() { DescriptionText = "New DescriptionText", DTName = "New DescriptionText" });
            int i = 0;
            foreach (ExpansionBookDescription dt in _data.Descriptions)
            {
                dt.DTName = "Decription Text " + i.ToString();
                i++;
            }
            HasChanges();
            listBox12.SelectedIndex = -1;
            listBox12.SelectedIndex = listBox12.Items.Count - 1;
        }

        private void darkButton19_Click(object sender, EventArgs e)
        {
            textBox8.Text = "";
            _data.Descriptions.Remove(currentdecriptiontext);
            HasChanges();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            currentdecriptiontext.DescriptionText = "<p>" + textBox8.Text + "</p>";
            HasChanges();
        }


    }
}