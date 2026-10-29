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
    public partial class cfglimitsdefinitionuserUsageControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private cfglimitsdefinitionuserConfig _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfglimitsdefinitionuserUsageControl()
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

            listBox7.DataSource = AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionConfig.Data.usageflags;
            listBox9.DataSource = _data.Data.usageflags;

            _suppressEvents = false;
        }

        private void listBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox9.SelectedItems.Count <= 0) return;
            user_listsUser uu = listBox9.SelectedItem as user_listsUser;
            _suppressEvents = true;
            textBox3.Text = uu.name;
            listBox11.DataSource = uu.usage;
            _suppressEvents = false;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            darkButton83.Visible = true;
        }

        private void darkButton27_Click(object sender, EventArgs e)
        {
            user_listsUser newusage = new user_listsUser();
            newusage.name = AppServices.GetRequired<EconomyManager>().GetUniqueDefinitionFlagName(DefinitionFlagKind.UserUsage, "NewUserUsageDef");
            newusage.usage = new BindingList<user_listsUserUsage>();
            _data.Data.usageflags.Add(newusage);
        }

        private void darkButton76_Click(object sender, EventArgs e)
        {
            if (listBox9.SelectedItems.Count <= 0) return;
            Cursor.Current = Cursors.WaitCursor;
            user_listsUser uu = listBox9.SelectedItem as user_listsUser;
            string uuname = uu.name;
            _data.Data.usageflags.Remove(uu);
            // Remove it from every type, map group prototype and user definition that uses it
            AppServices.GetRequired<EconomyManager>().RemoveDefinitionFlag(DefinitionFlagKind.UserUsage, uuname);
            Cursor.Current = Cursors.Default;
        }

        private void darkButton83_Click(object sender, EventArgs e)
        {
            if (listBox9.SelectedItems.Count <= 0) return;
            Cursor.Current = Cursors.WaitCursor;
            user_listsUser uu = listBox9.SelectedItem as user_listsUser;
            string uuname = uu.name;
            string newName = textBox3.Text.Trim();
            var economyManager = AppServices.GetRequired<EconomyManager>();
            if (string.IsNullOrEmpty(newName) || newName == uuname)
            {
                darkButton83.Visible = false;
                Cursor.Current = Cursors.Default;
                return;
            }
            if (economyManager.DefinitionFlagExists(DefinitionFlagKind.UserUsage, newName))
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show($"'{newName}' already exists. Choose a different name.", "Rename", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            uu.name = newName;
            // Rename it in every type, map group prototype and user definition that uses it
            economyManager.RenameDefinitionFlag(DefinitionFlagKind.UserUsage, uuname, newName);
            darkButton83.Visible = false;
            Cursor.Current = Cursors.Default;
        }

        private void darkButton77_Click(object sender, EventArgs e)
        {
            if (listBox11.SelectedItems.Count <= 0) return;
            if (listBox9.SelectedItems.Count <= 0) return;
            user_listsUser uu = listBox9.SelectedItem as user_listsUser;
            user_listsUserUsage luu = listBox11.SelectedItem as user_listsUserUsage;
            uu.usage.Remove(luu);

            // An empty user flag does nothing, so advise deleting it
            if (uu.usage.Count == 0)
            {
                MessageBox.Show(
                    $"'{uu.name}' no longer contains any usage flags.\n\n" +
                    "An empty user flag does nothing, but any types that use it still reference it. " +
                    "Delete it with the delete button for the user flag, which also removes it from those types.",
                    "Empty User Flag",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void darkButton78_Click(object sender, EventArgs e)
        {
            if (listBox7.SelectedItems.Count <= 0) return;
            listsUsage lu = listBox7.SelectedItem as listsUsage;
            if (lu != null)
            {
                user_listsUserUsage newluu = new user_listsUserUsage();
                newluu.name = lu.name;
                user_listsUser uu = listBox9.SelectedItem as user_listsUser;
                uu.usage.Add(newluu);
            }
        }
        
    }
}