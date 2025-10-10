using Day2eEditor;
using ExpansionPlugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionBookCraftingCategoryControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionBookCraftingCategory _data;
        private ExpansionBookCraftingCategory _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

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
        public ExpansionBookCraftingCategoryControl()
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
            _data = data as ExpansionBookCraftingCategory ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            textBox14.Text = _data.CategoryName;


            listBox20.DisplayMember = "DisplayName";
            listBox20.ValueMember = "Value";
            listBox20.DataSource = _data.Results;
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
        private ExpansionBookCraftingCategory CloneData(ExpansionBookCraftingCategory data)
        {
            return new ExpansionBookCraftingCategory
            {
                CategoryName = data.CategoryName,
                Results = new BindingList<string>(data.Results.ToList())
            };
        }


        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = $"Category: {_data.CategoryName}";
            }
        }

        #endregion
        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CategoryName = textBox14.Text;
            HasChanges();
            UpdateTreeNodeText();
        }
        private void darkButton36_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes();
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    if (!_data.Results.Contains(l))
                    {
                        _data.Results.Add(l);
                        HasChanges();
                    }
                }
            }
        }
        private void darkButton35_Click(object sender, EventArgs e)
        {
            _data.Results.Remove(listBox20.GetItemText(listBox20.SelectedItem));
            HasChanges();
        }


    }
}