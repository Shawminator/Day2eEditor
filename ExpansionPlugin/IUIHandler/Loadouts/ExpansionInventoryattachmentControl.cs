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
    public partial class ExpansionInventoryattachmentControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private Inventoryattachment _data;
        private Inventoryattachment _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionInventoryattachmentControl()
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
            _data = data as Inventoryattachment ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            string slotname = string.IsNullOrWhiteSpace(_data.SlotName)
                    ? "Default Slot"
                    : _data.SlotName;
            ItemAttachmentSlotNameCB.SelectedIndex = ItemAttachmentSlotNameCB.FindStringExact(slotname);

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
                parent.isDirty = !_data.Equals(_originalData);
            }
        }

        #region Helper Methods

        /// <summary>
        /// Clones the data for reset purposes
        /// </summary>
        private Inventoryattachment CloneData(Inventoryattachment data)
        {
            return new Inventoryattachment
            {
                SlotName = data.SlotName,
                Items = new BindingList<AILoadouts>(
                        data.Items.Select(item => CloneLoadout(item)).ToList()
                    )
            };
        }
        private AILoadouts CloneLoadout(AILoadouts original)
        {
            return new AILoadouts
            {
                ClassName = original.ClassName,
                Include = original.Include,
                Chance = original.Chance,
                Quantity = new Quantity
                {
                    Min = original.Quantity.Min,
                    Max = original.Quantity.Max
                },
                Health = new BindingList<Health>(
                    original.Health.Select(h => new Health
                    {
                        Min = h.Min,
                        Max = h.Max,
                        Zone = h.Zone
                    }).ToList()
                ),
                InventoryAttachments = new BindingList<Inventoryattachment>(
                    original.InventoryAttachments.Select(CloneData).ToList()
                ),
                InventoryCargo = new BindingList<AILoadouts>(
                    original.InventoryCargo.Select(CloneLoadout).ToList()
                ),
                ConstructionPartsBuilt = new BindingList<object>(
                    original.ConstructionPartsBuilt.ToList()
                ),
                Sets = new BindingList<AILoadouts>(
                    original.Sets.Select(CloneLoadout).ToList()
                ),
                isDirty = original.isDirty,
                ToDelete = original.ToDelete
            };
        }

        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = ItemAttachmentSlotNameCB.GetItemText(ItemAttachmentSlotNameCB.SelectedItem);
            }
        }

        #endregion

        private void ItemAttachmentSlotNameCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            string Slot = ItemAttachmentSlotNameCB.GetItemText(ItemAttachmentSlotNameCB.SelectedItem);
            if (Slot == "Default Slot") 
                Slot = "";
            _data.SlotName = Slot;
            UpdateTreeNodeText();
            HasChanges();
        }
    }
}