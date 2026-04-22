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
    public partial class eventspawngroupchildinfoControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private eventgroupdefGroupChild _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public eventspawngroupchildinfoControl()
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
            _data = data as eventgroupdefGroupChild ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            eventgroupnameTB.Text = _data.type;
            eventgroupXNUD.Value = _data.x;
            checkBox112.Checked = eventgroupYNUD.Visible = _data.ySpecified;
            eventgroupYNUD.Value = _data.y;
            eventgroupZNUD.Value = _data.z;
            eventgroupANUD.Value = _data.a;

            eventgroupSecondarySpawnRB.Checked = false;
            eventgroupLootoptionRB.Checked = false;
            eventgroupSecondarySpawnRB.Checked = _data.spawnsecondarySpecified;
            eventgroupLootoptionRB.Checked = !_data.spawnsecondarySpecified;

            

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                // TODO: Update _nodes.Last().Text based on _data
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton but = sender as RadioButton;
            switch (but.Name)
            {
                case "eventgroupSecondarySpawnRB":
                    if (but.Checked == true)
                    {
                        eventgroupSecondarySpawnCB.Visible = true;
                        if (_suppressEvents) return;

                        _data.spawnsecondarySpecified = true;
                    }
                    else
                    {
                        eventgroupSecondarySpawnCB.Visible = false;
                        if (_suppressEvents) return;

                        _data.spawnsecondarySpecified = false;
                    }
                    break;
                case "eventgroupLootoptionRB":
                    if (but.Checked == true)
                    {
                        eventgroupdelootCB.Visible = true;
                        eventgroupLootminNUD.Visible = true;
                        eventgrouplootmaxNUD.Visible = true;
                        label129.Visible = true;
                        label130.Visible = true;
                        if (!_suppressEvents)
                        {

                            _data.delootSpecified = true;
                            _data.lootminSpecified = true;
                            _data.lootmaxSpecified = true;
                        }
                    }
                    else
                    {
                        eventgroupdelootCB.Visible = false;
                        eventgroupLootminNUD.Visible = false;
                        eventgrouplootmaxNUD.Visible = false;
                        label129.Visible = false;
                        label130.Visible = false;
                        if (!_suppressEvents)
                        {
                            _data.delootSpecified = false;
                            _data.lootminSpecified = false;
                            _data.lootmaxSpecified = false;
                        }
                    }
                    break;
            }
            eventgroupSecondarySpawnCB.Checked = _data.spawnsecondary;
            eventgroupdelootCB.Checked = _data.deloot == 1 ? true : false;
            eventgroupLootminNUD.Value = _data.lootmin;
            eventgrouplootmaxNUD.Value = _data.lootmax;
        }

        private void eventgroupnameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.type = eventgroupnameTB.Text;
        }
        private void eventgroupXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.x = eventgroupXNUD.Value;
        }
        private void eventgroupYNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.y = eventgroupYNUD.Value;
         }
        private void eventgroupZNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.z = eventgroupZNUD.Value;
        }
        private void eventgroupANUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.a = eventgroupANUD.Value;
        }
        private void eventgroupdelootCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.deloot = eventgroupdelootCB.Checked == true ? 1 : 0;
        }
        private void eventgroupLootminNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.lootmin = (int)eventgroupLootminNUD.Value;
        }
        private void eventgrouplootmaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.lootmax = (int)eventgrouplootmaxNUD.Value;
        }
        private void eventgroupSecondarySpawnCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.spawnsecondary = eventgroupSecondarySpawnCB.Checked;
        }
        private void checkBox112_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            eventgroupYNUD.Visible = _data.ySpecified = checkBox112.Checked;
        }
    }
}