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
        private SpawnGearPresetFiles _data;
        private SpawnGearPresetFiles _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public SpawnGearCharacterTypesControl()
        {
            InitializeComponent();

            characterTypesCB.DataSource = File.ReadAllLines("Data\\CharacterClassnames.txt").ToList();
        }

        /// <summary>
        /// Returns the UserControl instance
        /// </summary>
        public Control GetControl() => this;

        /// <summary>
        /// Loads data into the control and stores the selected tree nodes
        /// </summary>
        public void LoadFromData(object data, List<TreeNode> selectedNodes)
        {
            // TODO: Replace ClassType with your actual type
            _data = data as SpawnGearPresetFiles ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            characterTypesLB.DisplayMember = "DisplayName";
            characterTypesLB.ValueMember = "Value";
            characterTypesLB.DataSource = _data.characterTypes;

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
            if (_nodes?.Any() != true) return;

            // TODO: Replace Parentfile with your actual parent type if different
            var ef = _nodes.Last().FindParentOfType<SpawnGearPresetFiles>();
            if (ef != null)
                ef.isDirty = !_data.Equals(_originalData);
        }

        #region Helper Methods

        /// <summary>
        /// Clones the data for reset purposes
        /// </summary>
        private SpawnGearPresetFiles CloneData(SpawnGearPresetFiles data)
        {
            // TODO: Implement actual cloning logic
            return new SpawnGearPresetFiles()
            {
                name = data.name,
                spawnWeight = data.spawnWeight,
                characterTypes = data.characterTypes
            };
        }

        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = $"Spawn Weight: {_data.spawnWeight}";
            }
        }

        #endregion
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
            if (!_data.characterTypes.Contains(NPCClassname))
            {
                _data.characterTypes.Add(NPCClassname);
            }
            HasChanges();
        }

        private void darkButton75_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < characterTypesCB.Items.Count; i++)
            {
                string NPCClassname = characterTypesCB.GetItemText(characterTypesCB.Items[i]);
                if (!_data.characterTypes.Contains(NPCClassname))
                {
                    _data.characterTypes.Add(NPCClassname);
                }
            }
            HasChanges();
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
                _data.characterTypes.Remove(item.ToString());
            }
            HasChanges();
        }
    }
}