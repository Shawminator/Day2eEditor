using Core;
using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class EnfusionScriptfileXYZMapper : UserControl, IUIHandler
    {
        private Type _parentType;
        private EnfusionScriptfile _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public EnfusionScriptfileXYZMapper()
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
            _data = data as EnfusionScriptfile ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;


            MAPXNUD.Value = (int)ExtractValue(_data.Data, "MAP_X");
            MAPZNUD.Value = (int)ExtractValue(_data.Data, "MAP_Z");
            RESOLUTIONNUD.Value = (decimal)ExtractValue(_data.Data, "RESOLUTION");


            _suppressEvents = false;
        }

        #region Helper Methods
        static float ExtractValue(string text, string variableName)
        {
            var match = Regex.Match(
                text,
                $@"{variableName}\s*=\s*([0-9]+(?:\.[0-9]+)?)",
                RegexOptions.IgnoreCase
            );

            if (match.Success && float.TryParse(match.Groups[1].Value, out float value))
                return value;

            throw new Exception($"{variableName} not found");
        }
        static string SetValue(string text, string variableName, float newValue)
        {
            return Regex.Replace(
                text,
                $@"({variableName}\s*=\s*)([0-9]+(?:\.[0-9]+)?)",
                match => match.Groups[1].Value + newValue,
                RegexOptions.IgnoreCase
            );
        }


        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                // TODO: Update _nodes.Last().Text based on _data
            }
        }

        #endregion

        private void MAPXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Data = SetValue(_data.Data, "MAP_X", (int)MAPXNUD.Value);
        }

        private void MAPZNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Data = SetValue(_data.Data, "MAP_Z", (int)MAPZNUD.Value);
        }

        private void RESOLUTIONNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Data = SetValue(_data.Data, "RESOLUTION", (float)RESOLUTIONNUD.Value);
        }
    }
}