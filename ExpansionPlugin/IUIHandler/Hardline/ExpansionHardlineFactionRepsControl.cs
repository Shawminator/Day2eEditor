using Day2eEditor;
using Org.BouncyCastle.Asn1.Cmp;
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
    public partial class ExpansionHardlineFactionRepsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private FactionReps _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        private ExpansionHardlinePlayerData  ExpansionHardlinePlayerData{ get; set; }

        public ExpansionHardlineFactionRepsControl()
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
            _data = data as FactionReps ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            ExpansionHardlinePlayerData = _nodes.Last().Parent.Tag as ExpansionHardlinePlayerData;
            _suppressEvents = true;

            HardlineFactionIDNUD.Value = _data.FactionID;
            HardlineReputationNUD.Value = _data.FactionRep;

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
                _nodes.Last().Text = $"Faction Id : {_data.FactionID}";
            }
        }

        #endregion

        private void HardlineReputationNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.FactionRep = (int)HardlineReputationNUD.Value;
        }

        private void HardlineFactionIDNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            int newid = (int)HardlineReputationNUD.Value;


            if (!ExpansionHardlinePlayerData.FactionReputation.Any(x => x.FactionID == newid))
            {
                _data.FactionID = newid;
                UpdateTreeNodeText();
            }
            else
            {
                _suppressEvents = true;
                HardlineReputationNUD.Value = _data.FactionID;
                MessageBox.Show($"a Faction Rep is allready in the list for ID:{newid}");
                _suppressEvents = false;
            }
        }
    }
}