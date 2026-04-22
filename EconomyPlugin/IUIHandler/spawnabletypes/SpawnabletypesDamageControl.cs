using Day2eEditor;

namespace EconomyPlugin
{
    public partial class SpawnabletypesDamageControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private spawnableTypeDamage _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public SpawnabletypesDamageControl()
        {
            InitializeComponent();
        }
        public Control GetControl() => this;
        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as spawnableTypeDamage ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _suppressEvents = true;

            DamageMinNUD.Value = _data.min;
            DamageMaxNUD.Value = _data.max;


            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes.Last() != null)
                _nodes.Last().Text = $"damage : quantmin={_data.min} quamtmax={_data.max}";
        }

        private void DamageMinNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.min = DamageMinNUD.Value;
            UpdateTreeNodeText();
        }
        private void DamageMaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.max = DamageMaxNUD.Value;
            UpdateTreeNodeText();
        }
    }
}
