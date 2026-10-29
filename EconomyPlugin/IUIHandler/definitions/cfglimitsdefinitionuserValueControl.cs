using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class cfglimitsdefinitionuserValueControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private cfglimitsdefinitionuserConfig _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfglimitsdefinitionuserValueControl()
        {
            InitializeComponent();
        }
        private void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox lb = sender as ListBox;
            e.DrawBackground();
            if (lb.Items.Count == 0) return;
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
            _data = data as cfglimitsdefinitionuserConfig ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            listBox8.DataSource = AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionConfig.Data.valueflags;
            listBox10.DataSource = _data.Data.valueflags;

            _suppressEvents = false;
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            darkButton84.Visible = true;
        }

        private void listBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox10.SelectedItems.Count <= 0) return;
            user_listsUser1 uu = listBox10.SelectedItem as user_listsUser1;
            _suppressEvents = true;
            textBox4.Text = uu.name;
            listBox12.DataSource = uu.value;
            _suppressEvents = false;
        }

        private void darkButton82_Click(object sender, EventArgs e)
        {
            user_listsUser1 newvalue = new user_listsUser1();
            newvalue.name = AppServices.GetRequired<EconomyManager>().GetUniqueDefinitionFlagName(DefinitionFlagKind.UserValue, "NewUserValueDef");
            newvalue.value = new BindingList<user_listsUserValue>();
            _data.Data.valueflags.Add(newvalue);
        }

        private void darkButton81_Click(object sender, EventArgs e)
        {
            if (listBox10.SelectedItems.Count <= 0) return;
            Cursor.Current = Cursors.WaitCursor;
            user_listsUser1 uu = listBox10.SelectedItem as user_listsUser1;
            string uuname = uu.name;
            _data.Data.valueflags.Remove(uu);
            // Remove it from every type, map group prototype and user definition that uses it
            AppServices.GetRequired<EconomyManager>().RemoveDefinitionFlag(DefinitionFlagKind.UserValue, uuname);
            Cursor.Current = Cursors.Default;
        }

        private void darkButton84_Click(object sender, EventArgs e)
        {
            if (listBox10.SelectedItems.Count <= 0) return;
            Cursor.Current = Cursors.WaitCursor;
            user_listsUser1 uu = listBox10.SelectedItem as user_listsUser1;
            string uuname = uu.name;
            string newName = textBox4.Text.Trim();
            var economyManager = AppServices.GetRequired<EconomyManager>();
            if (string.IsNullOrEmpty(newName) || newName == uuname)
            {
                darkButton84.Visible = false;
                Cursor.Current = Cursors.Default;
                return;
            }
            if (economyManager.DefinitionFlagExists(DefinitionFlagKind.UserValue, newName))
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show($"'{newName}' already exists. Choose a different name.", "Rename", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            uu.name = newName;
            // Rename it in every type, map group prototype and user definition that uses it
            economyManager.RenameDefinitionFlag(DefinitionFlagKind.UserValue, uuname, newName);
            darkButton84.Visible = false;
            Cursor.Current = Cursors.Default;
        }

        private void darkButton79_Click(object sender, EventArgs e)
        {
            if (listBox10.SelectedItems.Count <= 0) return;
            if (listBox12.SelectedItems.Count <= 0) return;
            user_listsUser1 uu = listBox10.SelectedItem as user_listsUser1;
            user_listsUserValue luu = listBox12.SelectedItem as user_listsUserValue;
            uu.value.Remove(luu);

            // An empty user flag does nothing, so advise deleting it
            if (uu.value.Count == 0)
            {
                MessageBox.Show(
                    $"'{uu.name}' no longer contains any value flags.\n\n" +
                    "An empty user flag does nothing, but any types and map group prototypes that use it still reference it. " +
                    "Delete it with the delete button for the user flag, which also removes it from those types and prototypes.",
                    "Empty User Flag",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void darkButton80_Click(object sender, EventArgs e)
        {
            if (listBox8.SelectedItems.Count <= 0) return;
            listsValue lu = listBox8.SelectedItem as listsValue;
            if (lu != null)
            {
                user_listsUserValue newluu = new user_listsUserValue();
                newluu.name = lu.name;
                user_listsUser1 uu = listBox10.SelectedItem as user_listsUser1;
                uu.value.Add(newluu);
            }
        }
    }
}