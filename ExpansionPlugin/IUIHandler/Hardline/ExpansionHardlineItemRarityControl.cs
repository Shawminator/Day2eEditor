using Day2eEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionHardlineItemRarityControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionHardlineSettings _data;
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
        public ExpansionHardlineItemRarityControl()
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
            _data = data as ExpansionHardlineSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            ItemRarityCB.DataSource = Enum.GetValues(typeof(ExpansionHardlineItemRarity));
            ItemRarityMoveToCB.DataSource = Enum.GetValues(typeof(ExpansionHardlineItemRarity));
            _suppressEvents = true;



            _suppressEvents = false;
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

        private void ItemRarityCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;

            string Type = ItemRarityCB.GetItemText(ItemRarityCB.SelectedItem);

            _suppressEvents = true;

            if (Type == "NONE" || Type == "Quest" || Type == "Collectable" || Type == "Ingredient")
            {
                ItemRequirementNUD.Visible = false;
                ItemRequirementNUD.Value = -1;
                //ItemRequirementNUD.Controls[0].Visible = false;
                darkLabel237.Visible = false;
            }
            else
            {
                darkLabel237.Visible = true;
                ItemRequirementNUD.Visible = true;
                ItemRequirementNUD.Value = (int)getRequirment(Type);
            }
            ItemRarityLB.DisplayMember = "DisplayName";
            ItemRarityLB.ValueMember = "Value";
            ItemRarityLB.DataSource = getlist(Type);
            _suppressEvents = false;
        }
        public BindingList<string> getlist(string type)
        {
            switch (type)
            {
                case "NONE":
                    return _data.NoneItems;
                case "Poor":
                    return _data.PoorItems;
                case "Common":
                    return _data.CommonItems;
                case "Uncommon":
                    return _data.UncommonItems;
                case "Rare":
                    return _data.RareItems;
                case "Epic":
                    return _data.EpicItems;
                case "Legendary":
                    return _data.LegendaryItems;
                case "Mythic":
                    return _data.MythicItems;
                case "Exotic":
                    return _data.ExoticItems;
                case "Quest":
                    return _data.QuestItems;
                case "Collectable":
                    return _data.CollectableItems;
                case "Ingredient":
                    return _data.IngredientItems;
            }
            return new BindingList<string>();
        }
        public string GetListfromitem(string item)
        {
            if (_data.NoneItems.Contains(item))
                return "NONE";
            if (_data.PoorItems.Contains(item))
                return "Poor";
            if (_data.CommonItems.Contains(item))
                return "Common";
            if (_data.UncommonItems.Contains(item))
                return "Uncommon";
            if (_data.RareItems.Contains(item))
                return "Rare";
            if (_data.EpicItems.Contains(item))
                return "Epic";
            if (_data.LegendaryItems.Contains(item))
                return "Legendary";
            if (_data.MythicItems.Contains(item))
                return "Mythic";
            if (_data.ExoticItems.Contains(item))
                return "Exotic";
            if (_data.QuestItems.Contains(item))
                return "Quest";
            if (_data.CollectableItems.Contains(item))
                return "Collectable";
            if (_data.IngredientItems.Contains(item))
                return "Ingredient";
            return "NOLIST";

        }
        public int? getRequirment(string type)
        {
            switch (type)
            {
                case "Poor":
                    return _data.PoorItemRequirement;
                case "Common":
                    return _data.CommonItemRequirement;
                case "Uncommon":
                    return _data.UncommonItemRequirement;
                case "Rare":
                    return _data.RareItemRequirement;
                case "Epic":
                    return _data.EpicItemRequirement;
                case "Legendary":
                    return _data.LegendaryItemRequirement;
                case "Mythic":
                    return _data.MythicItemRequirement;
                case "Exotic":
                    return _data.ExoticItemRequirement;
            }
            return 0;
        }
        public void SetRequirment(string type, int value)
        {
            switch (type)
            {
                case "Poor":
                    _data.PoorItemRequirement = value;
                    break;
                case "Common":
                    _data.CommonItemRequirement = value;
                    break;
                case "Uncommon":
                    _data.UncommonItemRequirement = value;
                    break;
                case "Rare":
                    _data.RareItemRequirement = value;
                    break;
                case "Epic":
                    _data.EpicItemRequirement = value;
                    break;
                case "Legendary":
                    _data.LegendaryItemRequirement = value;
                    break;
                case "Mythic":
                    _data.MythicItemRequirement = value;
                    break;
                case "Exotic":
                    _data.ExoticItemRequirement = value;
                    break;
            }
        }

        private void darkButton71_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes
            {
                LowerCase = true
            };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                string Type = ItemRarityCB.GetItemText(ItemRarityCB.SelectedItem);
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    string Typelist = GetListfromitem(l);
                    if (Typelist == "NOLIST")
                    {
                        getlist(Type).Add(l);
                    }
                    else
                    {
                        MessageBox.Show($"{l} is allready in {Typelist}");
                    }
                }
                
            }
        }

        private void darkButton70_Click(object sender, EventArgs e)
        {
            if (ItemRarityLB.SelectedItems.Count == 0)
                return;

            MoveSelected(ItemRarityLB, ItemRarityCB.GetItemText(ItemRarityCB.SelectedItem), "NONE");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ItemRarityLB.SelectedItems.Count == 0)
                return;

            MoveSelected(ItemRarityLB, ItemRarityCB.GetItemText(ItemRarityCB.SelectedItem), ItemRarityMoveToCB.GetItemText(ItemRarityMoveToCB.SelectedItem));
        }
        private void MoveSelected(ListBox listBox, string fromType, string toType)
        {
            var src = getlist(fromType);
            var dest = getlist(toType);

            var selected = listBox.SelectedItems.Cast<string>().ToList();

            foreach (var item in selected)
            {
                if (src.Remove(item) && !dest.Contains(item))
                    dest.Add(item);
            }

            var sorted = dest.OrderBy(s => s, StringComparer.CurrentCultureIgnoreCase).ToList();

            listBox.BeginUpdate();
            try
            {
                dest.RaiseListChangedEvents = false;
                try
                {
                    dest.Clear();
                    foreach (var s in sorted)
                        dest.Add(s);
                }
                finally
                {
                    dest.RaiseListChangedEvents = true;
                    dest.ResetBindings(); // notify all bound controls
                }
            }
            finally
            {
                listBox.EndUpdate();
            }

            
            listBox.ClearSelected();

        }

        private void ItemRequirementNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            string Type = ItemRarityCB.GetItemText(ItemRarityCB.SelectedItem);
            SetRequirment(Type, (int)ItemRequirementNUD.Value);
            
        }
    }
}