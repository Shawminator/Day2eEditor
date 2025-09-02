using Day2eEditor;

namespace EconomyPlugin
{
    public partial class SpawnabletypesDamageControl : UserControl, IUIHandler
    {
        private Type _parentType;
        public Control GetControl() => this;
        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as spawnableTypeDamage ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset
            _suppressEvents = true;

            DamageMinNUD.Value = _data.min;
            DamageMaxNUD.Value = _data.max;


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

        private spawnableTypeDamage _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;
        private spawnableTypeDamage _originalData;

        public SpawnabletypesDamageControl()
        {
            InitializeComponent();
        }
        private spawnableTypeDamage CloneData(spawnableTypeDamage data)
        {
            // Clone the original data to preserve it for reset purposes
            return new spawnableTypeDamage
            {
               min = data.min,
               max = data.max,
            };
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
            HasChanges();
            UpdateTreeNodeText();
        }

        private void DamageMaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.max = DamageMaxNUD.Value;
            HasChanges();
            UpdateTreeNodeText();
        }
    }
}
