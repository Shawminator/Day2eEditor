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
        private cfglimitsdefinitionuser _originalData;
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
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            listBox7.DataSource = AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionConfig.Data.usageflags;
            listBox9.DataSource = _data.Data.usageflags;

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
                parent.isDirty = !_data.Data.Equals(_originalData);
            }
        }

        #region Helper Methods

        /// <summary>
        /// Clones the data for reset purposes
        /// </summary>
        private cfglimitsdefinitionuser CloneData(cfglimitsdefinitionuserConfig data)
        {
            if (data == null) return null;

            // Deep clone Data (cfglimitsdefinitionuser)
            return  new cfglimitsdefinitionuser
            {
                usageflags = new BindingList<user_listsUser>(
                    data.Data?.usageflags?.Select(u => new user_listsUser
                    {
                        name = u.name,
                        usage = new BindingList<user_listsUserUsage>(
                            u.usage?.Select(x => new user_listsUserUsage
                            {
                                name = x.name
                            }).ToList() ?? new List<user_listsUserUsage>()
                        )
                    }).ToList() ?? new List<user_listsUser>()
                ),

                valueflags = new BindingList<user_listsUser1>(
                    data.Data?.valueflags?.Select(v => new user_listsUser1
                    {
                        name = v.name,
                        value = new BindingList<user_listsUserValue>(
                            v.value?.Select(x => new user_listsUserValue
                            {
                                name = x.name
                            }).ToList() ?? new List<user_listsUserValue>()
                        )
                    }).ToList() ?? new List<user_listsUser1>()
                )
            };
        }
        #endregion

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
            newusage.name = "NewUserUsageDef";
            newusage.usage = new BindingList<user_listsUserUsage>();
            _data.Data.usageflags.Add(newusage);
            HasChanges();
        }

        private void darkButton76_Click(object sender, EventArgs e)
        {
            if (listBox9.SelectedItems.Count <= 0) return;
            Cursor.Current = Cursors.WaitCursor;
            user_listsUser uu = listBox9.SelectedItem as user_listsUser;
            string uuname = uu.name;
            _data.Data.usageflags.Remove(uu);
            HasChanges();
            AppServices.GetRequired<EconomyManager>().CheckallTypes(uuname);
            Cursor.Current = Cursors.Default;
        }

        private void darkButton83_Click(object sender, EventArgs e)
        {
            if (listBox9.SelectedItems.Count <= 0) return;
            Cursor.Current = Cursors.WaitCursor;
            user_listsUser uu = listBox9.SelectedItem as user_listsUser;
            string uuname = uu.name;
            uu.name = textBox3.Text;
            AppServices.GetRequired<EconomyManager>().CheckallTypes(uuname, uu.name);
            HasChanges();
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
            HasChanges();
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
                HasChanges();

            }
        }
        
    }
}