using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionTraderMapsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionTraderMaps _data;
        private List<TreeNode> _nodes;

        public List<string> NPCNames { get; private set; }
        public List<ExpansionMarketTrader> TraderNames { get; private set; }

        private bool _suppressEvents;

        public ExpansionTraderMapsControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Returns the UserControl instance
        /// </summary>
        public Control GetControl() => this;

        /// <summary>
        /// Loads data into the control and stores the selected tree nodes
        /// </summary>
        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as ExpansionTraderMaps ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            string filePath = "Data\\ExpansionTradernames.txt";
            NPCNames = File.ReadAllLines(filePath).ToList();
            TraderNames = AppServices.GetRequired<ExpansionManager>().ExpansionMarketTraderConfig.MutableItems.ToList();

            NpcClassNameCB.DataSource = NPCNames;
            TraderNameCB.DataSource = TraderNames;
            TraderNameCB.DisplayMember = "FileName";

            // Set NPC combobox
            if (!string.IsNullOrEmpty(_data.NpcClassName))
            {
                NpcClassNameCB.SelectedItem = NPCNames
                    .FirstOrDefault(x => x == _data.NpcClassName);
            }

            // Set Trader combobox
            if (!string.IsNullOrEmpty(_data.TraderName))
            {
                var matchingTrader = TraderNames.FirstOrDefault(x =>
                    Path.GetFileNameWithoutExtension(x.FileName)
                        .Equals(_data.TraderName, StringComparison.OrdinalIgnoreCase));

                TraderNameCB.SelectedItem = matchingTrader;
            }

            _suppressEvents = false;
        }

        #region Helper Methods
        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                string typeLabel = _data.IsAI ? "[Roaming AI]" : "[Static]";
                _nodes.Last().Text = $"{_data.NpcClassName} ({_data.TraderName}) {typeLabel}";
            }
        }

        #endregion

        private void NpcClassNameCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NpcClassName = NpcClassNameCB.SelectedItem?.ToString();
            UpdateTreeNodeText();
        }

        private void TraderNameCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (TraderNameCB.SelectedItem is ExpansionMarketTrader trader)
            {
                _data.TraderName = Path.GetFileNameWithoutExtension(trader.FileName);
            }
            else
            {
                _data.TraderName = null;
            }
            UpdateTreeNodeText();
        }
    }
}