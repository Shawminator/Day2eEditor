using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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

        public prototypeGroupControl()
        {
            InitializeComponent();
            SetTiers();
            SetUsageCB();
        }

        private void SetUsageCB()
        {
            
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
                flowLayoutPanel1.Controls.Add(cb);
            }

            foreach (user_listsUser1 user in AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionuserConfig.Data.valueflags)
            {
                CheckBox cb = new CheckBox();
                cb.Tag = user.name;
                cb.Visible = true;
                cb.Checked = false;
                cb.Text = user.name;
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

            _suppressEvents = false;
        }

        private void MapgroupProtoPopulateTiers()
        {
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
            // TODO: Implement actual cloning logic
            return new prototypeGroup
            {
                name = data.name,
                lootmax = data.lootmax,
                lootmaxSpecified = data.lootmaxSpecified
            };
        }

        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                // TODO: Update _nodes.Last().Text based on _data
            }
        }

        #endregion

        private void mapgroupprotoTierCheckBoxchanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            CheckBox cb = sender as CheckBox;
            string tier = cb.Tag.ToString();
            if (cb.Checked)
                currentmapgroupprotoGroup.AddTier(tier);
            else
                currentmapgroupprotoGroup.removetier(tier);
            currentproject.mapgroupproto.isDirty = true;
            isUserInteraction = false;
            MapgroupProtoPopulateTiers();
            isUserInteraction = true;
        }

        private void MapgroupProtoUserdefiniedTiersChanged(object sender, EventArgs e)
        {
            if (isUserInteraction)
            {
                CheckBox cb = sender as CheckBox;
                string tier = cb.Tag.ToString();
                if (cb.Checked)
                {
                    if (currentmapgroupprotoGroup.value != null)
                    {
                        currentmapgroupprotoGroup.removetiers();
                    }
                    currentmapgroupprotoGroup.AdduserTier(tier);
                }
                else
                    currentmapgroupprotoGroup.removeusertier(tier);
                currentproject.mapgroupproto.isDirty = true;
                isUserInteraction = false;
                MapgroupProtoPopulateTiers();
                isUserInteraction = true;
            }
        }
    }
}