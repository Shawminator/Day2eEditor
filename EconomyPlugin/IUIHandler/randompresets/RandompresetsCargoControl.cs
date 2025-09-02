using Day2eEditor;
using System.Security.Cryptography;

namespace EconomyPlugin
{
    public partial class RandompresetsCargoControl : UserControl, IUIHandler
    {
        private Type _parentType;
        public Control GetControl() => this;
        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as randompresetsCargo ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset
            _suppressEvents = true;

            RandomPresetAttchemntNameTB.Text = _data.name;
            RandomPresetAttachmentChanceNUD.Value = _data.chance;

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
            var parentObj = _nodes.Last().FindParentOfType(_parentType);
            if (parentObj != null)
            {
                dynamic parent = parentObj;
                parent.isDirty = !_data.Equals(_originalData);
            }
        }

        private randompresetsCargo _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;
        private randompresetsCargo _originalData;

        public RandompresetsCargoControl()
        {
            InitializeComponent();
        }
        private randompresetsCargo CloneData(randompresetsCargo data)
        {
            return new randompresetsCargo
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
        private void RandomPresetItemNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.name = RandomPresetAttchemntNameTB.Text;
            HasChanges();
            UpdateTreeNodeText();
        }
        private void RandomPresetItemChanceNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.chance = RandomPresetAttachmentChanceNUD.Value;
            HasChanges();
            UpdateTreeNodeText();
        }
    }
}
