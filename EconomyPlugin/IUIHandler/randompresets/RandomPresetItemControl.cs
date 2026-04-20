using Day2eEditor;
using System.ComponentModel;
using System.Security.Cryptography;

namespace EconomyPlugin
{
    public partial class RandomPresetItemControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private randompresetsItem _data;
        private BindingList<randompresetsItem> _entries;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public RandomPresetItemControl()
        {
            InitializeComponent();
        }
        private void LoadNodesTotypeslist(List<TreeNode> selectedNodes)
        {
            _entries = new BindingList<randompresetsItem>();
            foreach (TreeNode _node in selectedNodes)
            {
                _entries.Add(_node.Tag as randompresetsItem);
            }
        }
        public Control GetControl() => this;
        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as randompresetsItem ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            LoadNodesTotypeslist(_nodes);
            _suppressEvents = true;

            RandomPresetItemNameTB.Text = _data.name;
            RandomPresetItemChanceNUD.Value = _data.chance;

            _suppressEvents = false;
        }



        private void UpdateTreeNodeText()
        {
            if (_nodes.Last() != null)
            {
                _nodes.Last().Text = $"Name = {_data.name}, Chance = {_data.chance}";
            }

        }
        private void UpdateTreeNodeTextAll()
        {
            for (int i = 0; i < _nodes.Count; i++)
            {

                _nodes[i].Text = $"Name = {_entries[i].name}, Chance = {_entries[i].chance}";
               
            }

        }

        private void darkButton60_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes
            {
                UseMultipleOfSameItem = false,
                UseOnlySingleItem = true

            };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    RandomPresetItemNameTB.Text = l;
                    UpdateTreeNodeText();
                }

            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }

        private void RandomPresetItemNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.name = RandomPresetItemNameTB.Text;
        }

        private void RandomPresetItemChanceNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            foreach (randompresetsItem randompresetsItem in _entries)
            {
                randompresetsItem.chance = RandomPresetItemChanceNUD.Value;
            }
            UpdateTreeNodeTextAll();
        }
    }
}
