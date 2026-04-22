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
    public partial class cfgplayerspawngroupControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private playerspawnpointsGroup _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgplayerspawngroupControl()
        {
            InitializeComponent();
        }

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as playerspawnpointsGroup ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            generatorposbubblesGroupnameTB.Text = _data.name;
            generatorposbubblesGroupnameTB.Text = _data.name;
            if (generatorposbubblesUseLifetimeCB.Checked = _data.lifetimeSpecified == true)
            {
                generatorposbubbleslifetiemNUD.Value = _data.lifetime;
            }
            if (generatorposbubblesusecounterCB.Checked = _data.counterSpecified == true)
            {
                generatorposbubblescounterNUD.Value = _data.counter;
            }

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = $"Group Name: {_data.name}";
            }
        }
        private void generatorposbubblesGroupnameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.name = generatorposbubblesGroupnameTB.Text;
            UpdateTreeNodeText();
        }
        private void generatorposbubblesUseLifetimeCB_CheckedChanged(object sender, EventArgs e)
        {
            if (generatorposbubblesUseLifetimeCB.Checked == true)
            {
                generatorposbubbleslifetiemNUD.Visible = true;
                label42.Visible = true;
                if (_suppressEvents) return;

                _data.lifetimeSpecified = true;
            }
            else
            {
                generatorposbubbleslifetiemNUD.Visible = false;
                label42.Visible = false;
                if (_suppressEvents) return;

                _data.lifetimeSpecified = false;
            }
        }
        private void generatorposbubbleslifetiemNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.lifetime = (int)generatorposbubbleslifetiemNUD.Value;
        }
        private void generatorposbubblesusecounterCB_CheckedChanged(object sender, EventArgs e)
        {
            if (generatorposbubblesusecounterCB.Checked == true)
            {
                generatorposbubblescounterNUD.Visible = true;
                label43.Visible = true;
                if (_suppressEvents) return;

                _data.counterSpecified = true;
            }
            else
            {
                generatorposbubblescounterNUD.Visible = false;
                label43.Visible = false;
                if (_suppressEvents) return;

                _data.counterSpecified = false;
            }
        }
        private void generatorposbubblescounterNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.counter = (int)generatorposbubblescounterNUD.Value;
        }
    }
}