using Day2eEditor;

namespace EconomyPlugin
{
    public partial class economyControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private EconomySection _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public economyControl()
        {
            InitializeComponent();
        }
        public Control GetControl() => this;
        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as EconomySection ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _suppressEvents = true;

            economyGB.Text = _nodes.Last().Text.Split(' ')[0];

            economyinitCB.Checked = _data.init == 1 ? true : false;
            economyloadCB.Checked = _data.load == 1 ? true : false;
            economyrespawnCB.Checked = _data.respawn == 1 ? true : false;
            economysaveCB.Checked = _data.save == 1 ? true : false;

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes.Last() != null)
                _nodes.Last().Text = $"{economyGB.Text} init:{_data.init} load:{_data.load} respawn:{_data.respawn} save:{_data.save}";
        }
        private void economyinitCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.init = economyinitCB.Checked == true ? 1 : 0;
            UpdateTreeNodeText();
        }
        private void economyloadCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.load = economyloadCB.Checked == true ? 1 : 0;
            UpdateTreeNodeText();
        }
        private void economyrespawnCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.respawn = economyrespawnCB.Checked == true ? 1 : 0;
            UpdateTreeNodeText();
        }
        private void economysaveCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.save = economysaveCB.Checked == true ? 1 : 0;
            UpdateTreeNodeText();
        }
    }
}
