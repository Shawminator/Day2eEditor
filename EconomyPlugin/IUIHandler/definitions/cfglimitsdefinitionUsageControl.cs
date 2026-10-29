using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class cfglimitsdefinitionUagesControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private cfglimitsdefinitionConfig _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfglimitsdefinitionUagesControl()
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
            _data = data as cfglimitsdefinitionConfig ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            listBox9.DataSource = _data.Data.usageflags;

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                // TODO: Update _nodes.Last().Text based on _data
            }
        }


        private void darkButton27_Click(object sender, EventArgs e)
        {
            listsUsage newusage = new listsUsage();
            newusage.name = AppServices.GetRequired<EconomyManager>().GetUniqueDefinitionFlagName(DefinitionFlagKind.Usage, "Change Me");
            _data.Data.usageflags.Add(newusage);
        }

        private void darkButton76_Click(object sender, EventArgs e)
        {
            if (listBox9.SelectedItems.Count <= 0) return;
            listsUsage uu = listBox9.SelectedItem as listsUsage;
            string uuname = uu.name;
            _data.Data.usageflags.Remove(uu);
            // Remove it from every type, map group prototype and user definition that uses it
            AppServices.GetRequired<EconomyManager>().RemoveDefinitionFlag(DefinitionFlagKind.Usage, uuname, out var emptiedUserFlags);

            // A user flag that only contained this usage flag is now empty. It is still defined, so types
            // keep using it, but it no longer stands for anything, so advise deleting it.
            if (emptiedUserFlags.Count > 0)
            {
                MessageBox.Show(
                    $"Removing '{uuname}' left these user usage flags with no usage flags in them:\n\n" +
                    string.Join("\n", emptiedUserFlags) +
                    "\n\nAn empty user flag does nothing, but any types that use it still reference it. " +
                    "Delete it under User Usage Flags (cfglimitsdefinitionuser.xml), which also removes it from those types.",
                    "Empty User Flags",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void darkButton83_Click(object sender, EventArgs e)
        {
            if (listBox9.SelectedItems.Count <= 0) return;
            listsUsage uu = listBox9.SelectedItem as listsUsage;
            string uuname = uu.name;
            string newName = textBox3.Text.Trim();
            var economyManager = AppServices.GetRequired<EconomyManager>();
            if (string.IsNullOrEmpty(newName) || newName == uuname)
            {
                darkButton83.Visible = false;
                Cursor.Current = Cursors.Default;
                return;
            }
            if (economyManager.DefinitionFlagExists(DefinitionFlagKind.Usage, newName))
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show($"'{newName}' already exists. Choose a different name.", "Rename", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            uu.name = newName;
            // Rename it in every type, map group prototype and user definition that uses it
            economyManager.RenameDefinitionFlag(DefinitionFlagKind.Usage, uuname, newName);
            darkButton83.Visible = false;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            darkButton83.Visible = true;
        }

        private void listBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox9.SelectedItems.Count <= 0) return;
            listsUsage uu = listBox9.SelectedItem as listsUsage;
            _suppressEvents = true;
            textBox3.Text = uu.name;
            _suppressEvents = false;
        }
    }
}