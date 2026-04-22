using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class SpawnabletypesTagsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private spawnableTypeTag _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public SpawnabletypesTagsControl()
        {
            InitializeComponent();
            comboBox5.DataSource = AppServices.GetRequired<EconomyManager>().cfglimitsdefinitionConfig.Data.tags;
        }

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as spawnableTypeTag ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            textBox2.Text = _data.name;

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = $"tag : {_data.name}";
            }
        }
        private void darkButton29_Click(object sender, EventArgs e)
        {
            listsTag t = comboBox5.SelectedItem as listsTag;
            textBox2.Text = _data.name = t.name;
            UpdateTreeNodeText();
            
        }
    }
}