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
    public partial class ExpansionSpawnGearLoadoutsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionSpawnSettings _data;
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

        public ExpansionSpawnGearLoadoutsControl()
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
            _data = data as ExpansionSpawnSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            SpawnLoadoutsLB.DataSource = AppServices.GetRequired<ExpansionManager>().ExpansionLoadoutConfig.Items;

            _suppressEvents = false;
        }

        #region Helper Methods
        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText(ExpansionSpawnGearLoadouts rs)
        {
            if (_nodes?.Any() == true)
            {
                TreeNode parentNode = _nodes.Last();
                TreeNode NewSpawnLoadoutNode = new TreeNode(rs.ToString())
                {
                    Tag = rs
                };
                parentNode.Nodes.Add(NewSpawnLoadoutNode);

                TreeView tv = parentNode.TreeView;
                if (tv != null)
                {
                    tv.SelectedNode = NewSpawnLoadoutNode;
                    NewSpawnLoadoutNode.EnsureVisible();
                }
            }
        }

        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            String LoadoutName = Path.GetFileNameWithoutExtension(SpawnLoadoutsLB.GetItemText(SpawnLoadoutsLB.SelectedItem));
            ExpansionSpawnGearLoadouts newExpansionSpawnGearLoadouts = new ExpansionSpawnGearLoadouts()
            {
                Loadout = LoadoutName,
                Chance = 1.0m
            };
            if (_nodes.Last().Tag.ToString() == "MaleLoadouts")
            {
                if (!_data.MaleLoadouts.Any(x => x.Loadout == LoadoutName))
                {
                    _data.MaleLoadouts.Add(newExpansionSpawnGearLoadouts);
                    UpdateTreeNodeText(newExpansionSpawnGearLoadouts);
                }
            }
            else if (_nodes.Last().Tag.ToString() == "FemaleLoadouts")
            {
                if (!_data.FemaleLoadouts.Any(x => x.Loadout == LoadoutName))
                {
                    _data.FemaleLoadouts.Add(newExpansionSpawnGearLoadouts);
                    UpdateTreeNodeText(newExpansionSpawnGearLoadouts);
                }
            }
        }
    }
}