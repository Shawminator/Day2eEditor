using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    public partial class ExpansionQuestObjectiveDeliveryControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionQuestObjectiveDelivery _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionQuestObjectiveDeliveryControl()
        {
            InitializeComponent();
        }
        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as ExpansionQuestObjectiveDelivery ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            ObjectivesDeliveryClassnameTB.Text = _data.ClassName;
            ObjectivesDeliveryAmountNUD.Value = (int)_data.Amount;
            ObjectivesDeliveryQuantityPercentNUD.Value = (int)_data.QuantityPercent;
            ObjectivesDeliveryMinQuantityPerentNUD.Value = (int)_data.MinQuantityPercent;

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = _data.ClassName;
            }
        }

        private void ObjectivesDeliveryAmountNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Amount = (int)ObjectivesDeliveryAmountNUD.Value;
        }
        private void ObjectivesDeliveryClassnameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ClassName = ObjectivesDeliveryClassnameTB.Text;
            UpdateTreeNodeText();
        }
        private void darkButton56_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes
            {
                UseOnlySingleItem = true
            };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                TreeNode FocusNode = new TreeNode();
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    ObjectivesDeliveryClassnameTB.Text = l;
                }
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }
        private void ObjectivesDeliveryQuantityPercentNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.QuantityPercent = (int)ObjectivesDeliveryQuantityPercentNUD.Value;
        }

        private void ObjectivesDeliveryMinQuantityPerentNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MinQuantityPercent = (int)ObjectivesDeliveryMinQuantityPerentNUD.Value;
        }


    }
}