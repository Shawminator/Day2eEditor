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
        private variablesVar _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;
        private variablesVar _originalData; // For storing the original state

        public VariablesVarControl()
        {
            InitializeComponent();
        }


        public void LoadFromData(object data, List<TreeNode> selectedNodes)
        {
            _data = data as variablesVar ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset
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
        public void ApplyChanges()
        {
            // Apply the changes to the underlying data object (no additional logic here if done in LoadFromData)
        }
        public void Reset()
        {
            // Reset the data and controls to the original state
            _data = CloneData(_originalData);
            UpdateTypedValueDisplay();
            UpdateTreeNodeText();
        }
        public void HasChanges()
        {
            // Compare current data with original data to check if changes were made
            if (!_data.Equals(_originalData))
            {
                globalsFile gf = _nodes[0].Parent.Tag as globalsFile;
                gf.isDirty = true;
            }
        }
        public Control GetControl() => this;
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
            // Clone the original data to preserve it for reset purposes
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
            HasChanges();
        }
    }
}
