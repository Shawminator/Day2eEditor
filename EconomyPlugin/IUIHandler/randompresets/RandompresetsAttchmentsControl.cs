using Day2eEditor;
using System.Security.Cryptography;

namespace EconomyPlugin
{
    public partial class RandompresetsAttchmentsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private randompresetsAttachments _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public RandompresetsAttchmentsControl()
        {
            InitializeComponent();
        }
        public Control GetControl() => this;
        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as randompresetsAttachments ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _suppressEvents = true;

            RandomPresetAttchemntNameTB.Text = _data.name;
            RandomPresetAttachmentChanceNUD.Value = _data.chance;

            _suppressEvents = false;
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
            UpdateTreeNodeText();
        }
        private void RandomPresetItemChanceNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.chance = RandomPresetAttachmentChanceNUD.Value;
            UpdateTreeNodeText();
        }
    }
}
