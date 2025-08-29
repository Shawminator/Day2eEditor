using Day2eEditor;
using System.Security.Cryptography;

namespace EconomyPlugin
{
    public partial class RandompresetsAttchmentsControl : UserControl, IUIHandler
    {
        public Control GetControl() => this;
        public void LoadFromData(object data, List<TreeNode> selectedNodes)
        {
            _data = data as randompresetsAttachments ?? throw new InvalidCastException();
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
            cfgrandompresetsFile ef = _nodes.Last().FindParentOfType<cfgrandompresetsFile>();
            ef.isDirty = !_data.Equals(_originalData);
        }

        private randompresetsAttachments _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;
        private randompresetsAttachments _originalData;

        public RandompresetsAttchmentsControl()
        {
            InitializeComponent();
        }
        private randompresetsAttachments CloneData(randompresetsAttachments data)
        {
            return new randompresetsAttachments
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
