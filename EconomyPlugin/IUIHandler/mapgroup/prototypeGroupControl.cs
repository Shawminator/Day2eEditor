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
    public partial class prototypeGroupControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private prototypeGroup _data;
        private prototypeGroup _originalData;
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

        public prototypeGroupControl()
        {
            InitializeComponent();
            SetTiers();
            SetUsageCB();
        }

        private void SetUsageCB()
        {
            List<object> usagelist = new List<object>();
            usagelist.AddRange(AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionConfig.Data.usageflags);
            MapGroupProtoUsageCB.DataSource = usagelist;
        }

        private void SetTiers()
        {
            _suppressEvents = true;

            foreach (listsValue value in AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionConfig.Data.valueflags)
            {
                CheckBox cb = new CheckBox();
                cb.Tag = value.name;
                cb.Checked = false;
                cb.Visible = true;
                cb.Text = value.name;
                cb.CheckedChanged += mapgroupprotoTierCheckBoxchanged;
                flowLayoutPanel1.Controls.Add(cb);
            }

            foreach (user_listsUser1 user in AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionuserConfig.Data.valueflags)
            {
                CheckBox cb = new CheckBox();
                cb.Tag = user.name;
                cb.Visible = true;
                cb.Checked = false;
                cb.Text = user.name;
                cb.CheckedChanged += MapgroupProtoUserdefiniedTiersChanged;
                flowLayoutPanel2.Controls.Add(cb);
            }
            _suppressEvents = false;
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
            _data = data as prototypeGroup ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            MapgroupprotoGroupNameTB.Text = _data.name;
            MapGroupProtoGroupUseLootMaxNUD.Visible = MapGroupprotoGroupUseLootmaxCB.Checked = _data.lootmaxSpecified;
            MapGroupProtoGroupUseLootMaxNUD.Value = _data.lootmax;

            MapgroupProtoPopulateTiers();
            MapgroupProtopopulateUsage();
            _suppressEvents = false;
        }
        private void MapgroupProtopopulateUsage()
        {
            MapGroupProtoGroupUsageLB.DisplayMember = "DisplayName";
            MapGroupProtoGroupUsageLB.ValueMember = "Value";
            MapGroupProtoGroupUsageLB.DataSource = _data.usage;
        }
        private void MapgroupProtoPopulateTiers()
        {
            foreach (CheckBox cb in flowLayoutPanel1.Controls)
            {
                cb.Checked = false;
            }
            foreach (CheckBox cb in flowLayoutPanel2.Controls)
            {
                cb.Checked = false;
            }
            if (_data.value != null)
            {
                for (int i = 0; i < _data.value.Count; i++)
                {
                    if (_data.value[i].user != null && _data.value[i].user.Count() > 0 && _data.value[i].name == null)
                    {
                        tabControl24.SelectedIndex = 1;
                        try
                        {
                            flowLayoutPanel2.Controls.OfType<CheckBox>().First(x => x.Tag.ToString() == _data.value[i].user).Checked = true;
                        }
                        catch
                        {
                            _data.value.RemoveAt(i);
                            i--;
                        }
                    }
                    else
                    {
                        tabControl24.SelectedIndex = 0;

                        try
                        {
                            flowLayoutPanel1.Controls.OfType<CheckBox>().First(x => x.Tag.ToString() == _data.value[i].name).Checked = true;
                        }
                        catch
                        {
                            _data.value.RemoveAt(i);
                            i--;
                        }
                    }
                }
            }
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
        private prototypeGroup CloneData(prototypeGroup data)
        {
            if (data == null) return null;

            return new prototypeGroup
            {
                name = data.name,
                lootmax = data.lootmax,
                lootmaxSpecified = data.lootmaxSpecified,

                value = data.value == null
                    ? null
                    : new BindingList<prototypeGroupValue>(
                        data.value.Select(v => new prototypeGroupValue
                        {
                            name = v.name,
                            user = v.user
                        }).ToList()),

                usage = data.usage == null
                    ? null
                    : new BindingList<prototypeGroupUsage>(
                        data.usage.Select(u => new prototypeGroupUsage
                        {
                            name = u.name
                        }).ToList()),

                container = data.container == null
                    ? null
                    : new BindingList<prototypeGroupContainer>(
                        data.container.Select(c => new prototypeGroupContainer
                        {
                            name = c.name,
                            lootmax = c.lootmax,
                            lootmaxSpecified = c.lootmaxSpecified,
                            category = c.category == null
                                ? null
                                : new BindingList<prototypeGroupContainerCategory>(
                                    c.category.Select(cat => new prototypeGroupContainerCategory
                                    {
                                        name = cat.name
                                    }).ToList()),
                            tag = c.tag == null
                                ? null
                                : new BindingList<prototypeGroupContainerTag>(
                                    c.tag.Select(t => new prototypeGroupContainerTag
                                    {
                                        name = t.name
                                    }).ToList()),
                            point = c.point == null
                                ? null
                                : new BindingList<prototypeGroupContainerPoint>(
                                    c.point.Select(p => new prototypeGroupContainerPoint
                                    {
                                        pos = p.pos,
                                        range = p.range,
                                        height = p.height,
                                        flags = p.flags,
                                        flagsSpecified = p.flagsSpecified
                                    }).ToList())
                        }).ToList()),

                dispatch = data.dispatch == null
                    ? null
                    : new BindingList<prototypeGroupProxy>(
                        data.dispatch.Select(d => new prototypeGroupProxy
                        {
                            type = d.type,
                            pos = d.pos,
                            rpy = d.rpy
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

        private void mapgroupprotoTierCheckBoxchanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            CheckBox cb = sender as CheckBox;
            string tier = cb.Tag.ToString();
            if (cb.Checked)
                _data.AddTier(tier);
            else
                _data.removetier(tier);
            HasChanges();
        }
        private void MapgroupProtoUserdefiniedTiersChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            CheckBox cb = sender as CheckBox;
            string tier = cb.Tag.ToString();
            if (cb.Checked)
            {
                if (_data.value != null)
                {
                    _data.removetiers();
                }
                _data.AdduserTier(tier);
            }
            else
                _data.removeusertier(tier);
            HasChanges();
        }
        private void MapgroupprotoGroupNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.name = MapgroupprotoGroupNameTB.Text;
            HasChanges();
            UpdateTreeNodeText();
        }
        private void MapGroupprotoGroupUseLootmaxCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            MapGroupProtoGroupUseLootMaxNUD.Visible = _data.lootmaxSpecified = MapGroupprotoGroupUseLootmaxCB.Checked;
            HasChanges();
        }
        private void MapGroupProtoGroupUseLootMaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.lootmax = (int)MapGroupProtoGroupUseLootMaxNUD.Value;
            HasChanges();
        }
        private void darkButton56_Click(object sender, EventArgs e)
        {
            _data.AddnewUsage(MapGroupProtoUsageCB.SelectedItem as listsUsage);
            HasChanges();
        }
        private void darkButton58_Click(object sender, EventArgs e)
        {
            prototypeGroupUsage u = MapGroupProtoGroupUsageLB.SelectedItem as prototypeGroupUsage;
            _data.removeusage(u);
            HasChanges();
        }
    }
}