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
        private prototypeGroupContainer _originalData;
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
            _originalData = CloneData(_data); // Store original data for reset

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
        private prototypeGroupContainer CloneData(prototypeGroupContainer data)
        {
            if (data == null) return null;

            return new prototypeGroupContainer
            {
                name = data.name,
                lootmax = data.lootmax,
                lootmaxSpecified = data.lootmaxSpecified,

                category = data.category == null
                    ? null
                    : new BindingList<prototypeGroupContainerCategory>(
                        data.category.Select(c => new prototypeGroupContainerCategory
                        {
                            name = c.name
                        }).ToList()),

                tag = data.tag == null
                    ? null
                    : new BindingList<prototypeGroupContainerTag>(
                        data.tag.Select(t => new prototypeGroupContainerTag
                        {
                            name = t.name
                        }).ToList()),

                point = data.point == null
                    ? null
                    : new BindingList<prototypeGroupContainerPoint>(
                        data.point.Select(p => new prototypeGroupContainerPoint
                        {
                            pos = p.pos,
                            range = p.range,
                            height = p.height,
                            flags = p.flags,
                            flagsSpecified = p.flagsSpecified
                        }).ToList())
            };
        }

        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = _data.name;
            }
        }

        #endregion

        private void MapgroupProtoGroupcontainerNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.name = MapgroupProtoGroupcontainerNameTB.Text;
            HasChanges();
        }
        private void MapgroupProtoGroupcontainerUseLootMaxCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            MapgroupProtoGroupcontainerUseLootMaxNUD.Visible = _data.lootmaxSpecified = MapgroupProtoGroupcontainerUseLootMaxCB.Checked;
            HasChanges();
        }
        private void MapgroupProtoGroupcontainerUseLootMaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.lootmax = (int)MapgroupProtoGroupcontainerUseLootMaxNUD.Value;
            HasChanges();
        }
        private void darkButton61_Click(object sender, EventArgs e)
        {
            listsCategory u = MapGroupProtoGroupContainerCategroyCB.SelectedItem as listsCategory;
            _data.AddnewCategory(u);
            HasChanges();
        }
        private void darkButton62_Click(object sender, EventArgs e)
        {
            prototypeGroupContainerCategory c = MapGroupProtoGroupContainerCategroyLB.SelectedItem as prototypeGroupContainerCategory;
            _data.removecategory(c);
            HasChanges();
        }
        private void darkButton57_Click(object sender, EventArgs e)
        {
            listsTag t = MapGroupProtoGroupContainerTagCB.SelectedItem as listsTag;
            _data.Addnewtag(t);
            HasChanges();
        }
        private void darkButton59_Click(object sender, EventArgs e)
        {
            prototypeGroupContainerTag t = MapGroupprotoGroupContainerTagLB.SelectedItem as prototypeGroupContainerTag;
            _data.removetag(t);
            HasChanges();
        }
    }
}