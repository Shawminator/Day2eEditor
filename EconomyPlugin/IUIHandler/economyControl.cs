using Day2eEditor;

namespace EconomyPlugin
{
    public partial class economyControl : UserControl, IUIHandler
    {
        private EconomySection _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;
        private EconomySection _originalData;

        public Control GetControl() => this;
        public economyControl()
        {
            InitializeComponent();
        }
        public void LoadFromData(object data, List<TreeNode> selectedNodes)
        {
            _data = data as EconomySection ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset
            _suppressEvents = true;

            economyGB.Text = _nodes.Last().Text.Split(' ')[0];

            economyinitCB.Checked = _data.init == 1 ? true : false;
            economyloadCB.Checked = _data.load == 1 ? true : false;
            economyrespawnCB.Checked = _data.respawn == 1 ? true : false;
            economysaveCB.Checked = _data.save == 1 ? true : false;

            _suppressEvents = false;
        }
        public void ApplyChanges()
        {

        }
        public void Reset()
        {

        }
        private void UpdateTreeNodeText()
        {
            if (_nodes.Last() != null)
                _nodes.Last().Text = $"{economyGB.Text} init:{_data.init} load:{_data.load} respawn:{_data.respawn} save:{_data.save}";
        }
        public void HasChanges()
        {
            economyFile ef = _nodes.Last().Parent.Tag as economyFile;
            // Compare current data with original data to check if changes were made
            if (!_data.Equals(_originalData))
            {
                ef.isDirty = true;
            }
        }
        private EconomySection CloneData(EconomySection data)
        {
            // Clone the original data to preserve it for reset purposes
            return new EconomySection
            {
                init = data.init,
                load = data.load,
                respawn = data.respawn,
                save = data.save,
            };
        }
        private void economyinitCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.init = economyinitCB.Checked == true ? 1 : 0;
            UpdateTreeNodeText();
            HasChanges();
        }
        private void economyloadCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.load = economyloadCB.Checked == true ? 1 : 0;
            UpdateTreeNodeText();
            HasChanges();
        }
        private void economyrespawnCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.respawn = economyrespawnCB.Checked == true ? 1 : 0;
            UpdateTreeNodeText();
            HasChanges();
        }
        private void economysaveCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.save = economysaveCB.Checked == true ? 1 : 0;
            UpdateTreeNodeText();
            HasChanges();
        }
    }
}
