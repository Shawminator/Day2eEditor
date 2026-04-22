using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class prototypeGroupContainerControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private prototypeGroupContainer _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

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

        public prototypeGroupContainerControl()
        {
            InitializeComponent();

            SetupCats();
            SetupTags();
        }

        private void SetupTags()
        {
            MapGroupProtoGroupContainerTagCB.DataSource = AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionConfig.Data.tags;
        }

        private void SetupCats()
        {
            MapGroupProtoGroupContainerCategroyCB.DataSource = AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionConfig.Data.categories;
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
            _data = data as prototypeGroupContainer ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            MapgroupProtoGroupcontainerNameTB.Text = _data.name;
            MapgroupProtoGroupcontainerUseLootMaxNUD.Visible = MapgroupProtoGroupcontainerUseLootMaxCB.Checked = _data.lootmaxSpecified;
            MapgroupProtoGroupcontainerUseLootMaxNUD.Value = _data.lootmax;
            label131.Text = "Number of Loot Points :- " + _data.point.Count();

            MapgroupProtoGroupPopulatecategory();
            MapgroupProtoGroupPopulateTags();

            _suppressEvents = false;
        }
        private void MapgroupProtoGroupPopulatecategory()
        {
            MapGroupProtoGroupContainerCategroyLB.DisplayMember = "DisplayName";
            MapGroupProtoGroupContainerCategroyLB.ValueMember = "Value";
            MapGroupProtoGroupContainerCategroyLB.DataSource = _data.category;
        }
        private void MapgroupProtoGroupPopulateTags()
        {
            MapGroupprotoGroupContainerTagLB.DisplayMember = "DisplayName";
            MapGroupprotoGroupContainerTagLB.ValueMember = "Value";
            MapGroupprotoGroupContainerTagLB.DataSource = _data.tag;
        }
         private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = _data.name;
            }
        }
        private void MapgroupProtoGroupcontainerNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.name = MapgroupProtoGroupcontainerNameTB.Text;
        }
        private void MapgroupProtoGroupcontainerUseLootMaxCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            MapgroupProtoGroupcontainerUseLootMaxNUD.Visible = _data.lootmaxSpecified = MapgroupProtoGroupcontainerUseLootMaxCB.Checked;
        }
        private void MapgroupProtoGroupcontainerUseLootMaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.lootmax = (int)MapgroupProtoGroupcontainerUseLootMaxNUD.Value;
        }
        private void darkButton61_Click(object sender, EventArgs e)
        {
            listsCategory u = MapGroupProtoGroupContainerCategroyCB.SelectedItem as listsCategory;
            _data.AddnewCategory(u);
        }
        private void darkButton62_Click(object sender, EventArgs e)
        {
            prototypeGroupContainerCategory c = MapGroupProtoGroupContainerCategroyLB.SelectedItem as prototypeGroupContainerCategory;
            _data.removecategory(c);
        }
        private void darkButton57_Click(object sender, EventArgs e)
        {
            listsTag t = MapGroupProtoGroupContainerTagCB.SelectedItem as listsTag;
            _data.Addnewtag(t);
        }
        private void darkButton59_Click(object sender, EventArgs e)
        {
            prototypeGroupContainerTag t = MapGroupprotoGroupContainerTagLB.SelectedItem as prototypeGroupContainerTag;
            _data.removetag(t);
        }
    }
}