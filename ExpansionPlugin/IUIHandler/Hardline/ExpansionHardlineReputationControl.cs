using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionHardlineReputationControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionHardlineSettings _data;
        private ExpansionHardlineSettings _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public EntityReputationlevels? CurrentEntityrep { get; private set; }

        public ExpansionHardlineReputationControl()
        {
            InitializeComponent();
        }
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
            _data = data as ExpansionHardlineSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = _data.Clone();

            _suppressEvents = true;

            ReputationMaxReputationNUD.Value = (int)_data.MaxReputation;
            ReputationLossOnDeathNUD.Value = (int)_data.ReputationLossOnDeath;

            EntityReputationLB.DisplayMember = "DisplayName";
            EntityReputationLB.ValueMember = "Value";
            EntityReputationLB.DataSource = _data.entityreps;

            _suppressEvents = false;
        }

        /// <summary>
        /// Applies changes to the data and updates the original snapshot
        /// </summary>
        public void ApplyChanges()
        {
            _originalData = _data.Clone();
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

        private void EntityReputationLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentEntityrep = EntityReputationLB.SelectedItem as EntityReputationlevels;
            if (CurrentEntityrep == null) { return; }
            _suppressEvents = true;
            EntityReputationNUD.Value = CurrentEntityrep.Level;
            _suppressEvents = false;
        }
        private void ReputationLossOnDeathNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ReputationLossOnDeath = (int)ReputationLossOnDeathNUD.Value;
            HasChanges();
        }
        private void ReputationMaxReputationNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxReputation = (int)ReputationMaxReputationNUD.Value;
            HasChanges();
        }
        private void EntityReputationNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            CurrentEntityrep.Level = (int)EntityReputationNUD.Value;
            HasChanges();
        }
        private void darkButton111_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes();
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    if (!_data.entityreps.Any(x => x.Classname == l))
                    {
                        _data.entityreps.Add(new EntityReputationlevels(l, 0));
                    }
                    HasChanges();
                }
            }
        }
        private void darkButton110_Click(object sender, EventArgs e)
        {
            EntityReputationlevels erl = _data.entityreps.First(x => x.Classname == EntityReputationLB.GetItemText(EntityReputationLB.SelectedItem));
            if (erl != null)
            {
                _data.entityreps.Remove(erl);
                HasChanges();
            }
        }
    }
}