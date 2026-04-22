using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class SpawnGearAttributesControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private Attributes _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public SpawnGearAttributesControl()
        {
            InitializeComponent();
        }

        public Control GetControl() => this;

         public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as Attributes ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            SpawnGearhealthMinNUD.Value = _data.healthMin;
            SpawnGearhealthMaxNUD.Value = _data.healthMax;
            SpawnGearQuanityMinNUD.Value = _data.quantityMin;
            SpawnGearQuanityMaxNUD.Value = _data.quantityMax;

            _suppressEvents = false;
        }
        private void SpawnGearhealthMinNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.healthMin = SpawnGearhealthMinNUD.Value;
        }

        private void SpawnGearhealthMaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.healthMax = SpawnGearhealthMaxNUD.Value;

        }

        private void SpawnGearQuanityMinNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.quantityMin = SpawnGearQuanityMinNUD.Value;
        }

        private void SpawnGearQuanityMaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.quantityMax = SpawnGearQuanityMaxNUD.Value;
        }
    }
}