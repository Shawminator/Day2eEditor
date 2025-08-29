using Day2eEditor;
using System.Security.Cryptography;

namespace EconomyPlugin
{
    public partial class RandomPresetItemControl : UserControl, IUIHandler
    {
        public Control GetControl() => this;
        public void LoadFromData(object data, List<TreeNode> selectedNodes)
        {
            _data = data as randompresetsItem ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset
            _suppressEvents = true;

            RandomPresetItemNameTB.Text = _data.name;
            RandomPresetItemChanceNUD.Value = _data.chance;

            _suppressEvents = false;
        }
        public void ApplyChanges()
        {
            _originalData = CloneData(_data);
        }
        public void Reset()
        {

        }
        public void HasChanges()
        {
            cfgrandompresetsFile ef = _nodes.Last().FindParentOfType<cfgrandompresetsFile>();
            ef.isDirty = !_data.Equals(_originalData);
        }

        private randompresetsItem _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;
        private randompresetsItem _originalData;

        public RandomPresetItemControl()
        {
            InitializeComponent();
        }
        private randompresetsItem CloneData(randompresetsItem data)
        {
            return new randompresetsItem
            {
                name = data.name,
                chance = data.chance,
            };
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes.Last() != null)
            {
                _nodes.Last().Text = $"Name = {_data.name}, Chance = {_data.chance}";
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
            HasChanges();
        }

        private void RandomPresetItemChanceNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.chance = RandomPresetItemChanceNUD.Value;
            HasChanges();
            UpdateTreeNodeText();
        }
    }
}
