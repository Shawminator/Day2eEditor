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

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as prototypeGroup ?? throw new InvalidCastException();
            _nodes = selectedNodes;

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
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = _data.name;
            }
        }
        private void mapgroupprotoTierCheckBoxchanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            CheckBox cb = sender as CheckBox;
            string tier = cb.Tag.ToString();
            if (cb.Checked)
                _data.AddTier(tier);
            else
                _data.removetier(tier);
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
        }
        private void MapgroupprotoGroupNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.name = MapgroupprotoGroupNameTB.Text;
            UpdateTreeNodeText();
        }
        private void MapGroupprotoGroupUseLootmaxCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            MapGroupProtoGroupUseLootMaxNUD.Visible = _data.lootmaxSpecified = MapGroupprotoGroupUseLootmaxCB.Checked;
        }
        private void MapGroupProtoGroupUseLootMaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.lootmax = (int)MapGroupProtoGroupUseLootMaxNUD.Value;
        }
        private void darkButton56_Click(object sender, EventArgs e)
        {
            _data.AddnewUsage(MapGroupProtoUsageCB.SelectedItem as listsUsage);
        }
        private void darkButton58_Click(object sender, EventArgs e)
        {
            prototypeGroupUsage u = MapGroupProtoGroupUsageLB.SelectedItem as prototypeGroupUsage;
            _data.removeusage(u);
        }
    }
}