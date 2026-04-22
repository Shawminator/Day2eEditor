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
    public partial class SpawnGearCharacterTypesControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private SpawnGearPresetFile _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public SpawnGearCharacterTypesControl()
        {
            InitializeComponent();

            characterTypesCB.DataSource = File.ReadAllLines("Data\\VanillaCharacterClassnames.txt").ToList();
        }

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as SpawnGearPresetFile ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            characterTypesLB.DisplayMember = "DisplayName";
            characterTypesLB.ValueMember = "Value";
            characterTypesLB.DataSource = _data.Data.characterTypes;

            _suppressEvents = false;
        }

        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = $"Spawn Weight: {_data.Data.spawnWeight}";
            }
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
        private void darkButton71_Click(object sender, EventArgs e)
        {
            string NPCClassname = characterTypesCB.GetItemText(characterTypesCB.SelectedItem);
            if (!_data.Data.characterTypes.Contains(NPCClassname))
            {
                _data.Data.characterTypes.Add(NPCClassname);
            }
        }
        private void darkButton75_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < characterTypesCB.Items.Count; i++)
            {
                string NPCClassname = characterTypesCB.GetItemText(characterTypesCB.Items[i]);
                if (!_data.Data.characterTypes.Contains(NPCClassname))
                {
                    _data.Data.characterTypes.Add(NPCClassname);
                }
            }
        }
        private void darkButton72_Click(object sender, EventArgs e)
        {
            if (characterTypesLB.SelectedItems.Count < 1) return;
            List<String> lstitems = new List<String>();
            foreach (int i in characterTypesLB.SelectedIndices)
            {
                lstitems.Add(characterTypesLB.GetItemText(characterTypesLB.Items[i]));
            }
            foreach (var item in lstitems)
            {
                _data.Data.characterTypes.Remove(item.ToString());
            }
        }
    }
}