using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class Vector3Control : UserControl, IUIHandler
    {
        public event Action<Vec3> PositionChanged;
        private Type _parentType;
        private Vec3 _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public Vector3Control()
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
            _data = data is Vec3 v ? v : throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            POSXNUD.Value = (decimal)_data.X;
            POSYNUD.Value = (decimal)_data.Y;
            POSZNUD.Value = (decimal)_data.Z;


            _suppressEvents = false;
        }

        #region Helper Methods
        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {

            if (_parentType == typeof(ExpansionP2pMarketTradersConfig))
                return;

            if (_nodes?.Any() == true )
            {
                string split = _nodes.Last().Text.Split(':')[0];
                _nodes.Last().Text = _data.GetString();
            }
        }

        #endregion

        private void POSXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.X = (float)POSXNUD.Value;
            
            UpdateTreeNodeText();
            PositionChanged?.Invoke(_data);
        }

        private void POSYNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Y= (float)POSYNUD.Value;
            
            UpdateTreeNodeText();
            PositionChanged?.Invoke(_data);
        }

        private void POSZNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Z = (float)POSZNUD.Value;
            
            UpdateTreeNodeText();
            PositionChanged?.Invoke(_data);
        }
    }
}