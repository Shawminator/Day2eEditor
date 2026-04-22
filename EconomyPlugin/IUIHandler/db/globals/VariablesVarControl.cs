using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EconomyPlugin
{
    public partial class VariablesVarControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private variablesVar _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public VariablesVarControl()
        {
            InitializeComponent();
        }
        public Control GetControl() => this;
        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as variablesVar ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _suppressEvents = true;

            globalsGB.Text = _data.name;

            switch (_data.type)
            {
                case 0:
                    variablesvarvalueNUD.DecimalPlaces = 0;
                    variablesvarvalueNUD.Increment = 1;
                    variablesvarvalueNUD.Maximum = 999999999;
                    variablesvarvalueNUD.Value = Convert.ToDecimal(_data.TypedValue);
                    break;
                case 1:
                    variablesvarvalueNUD.DecimalPlaces = 2;
                    variablesvarvalueNUD.Increment = 0.05m;
                    variablesvarvalueNUD.Maximum = 1;
                    variablesvarvalueNUD.Value = Convert.ToDecimal(_data.TypedValue);
                    break;
            }

            _suppressEvents = false;
        }

        private void UpdateTreeNodeText()
        {
            if (_nodes[0] != null)
                _nodes[0].Text = $"{_data.name} = {_data.value}";
        }
        private void UpdateTypedValueDisplay()
        {
            switch (_data.type)
            {
                case 0:
                    variablesvarvalueNUD.DecimalPlaces = 0;
                    variablesvarvalueNUD.Increment = 1;
                    variablesvarvalueNUD.Maximum = 999999999;
                    variablesvarvalueNUD.Value = Convert.ToDecimal(_data.TypedValue);
                    break;
                case 1:
                    variablesvarvalueNUD.DecimalPlaces = 2;
                    variablesvarvalueNUD.Increment = 0.05m;
                    variablesvarvalueNUD.Maximum = 1;
                    variablesvarvalueNUD.Value = Convert.ToDecimal(_data.TypedValue);
                    break;
            }
        }
        private variablesVar CloneData(variablesVar data)
        {
            return new variablesVar
            {
                name = data.name,
                type = data.type,
                value = data.value
            };
        }
        private void variablesvarvalueNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.TypedValue = variablesvarvalueNUD.Value;
            UpdateTreeNodeText();
        }
    }
}
