using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionGeneralMappingControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionMapping _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionGeneralMappingControl()
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
            _data = data as ExpansionMapping ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            BuildingIvysComboBox.DataSource = Enum.GetValues(typeof(buildingIvy));
            UseCustomMappingModuleCB.Checked = _data.UseCustomMappingModule == 1 ? true : false;
            BuildingInteriorsCB.Checked = _data.BuildingInteriors == 1 ? true : false;
            BuildingIvysComboBox.SelectedItem = (buildingIvy)_data.BuildingIvys;

            _suppressEvents = false;
        }

        #region Helper Methods

        /// <summary>
        /// Clones the data for reset purposes
        /// </summary>
        private ExpansionMapping CloneData(ExpansionMapping data)
        {
            // TODO: Implement actual cloning logic
            return new ExpansionMapping
            {
                UseCustomMappingModule = data.UseCustomMappingModule,
                BuildingInteriors = data.BuildingInteriors,
                BuildingIvys = data.BuildingIvys,
                Mapping = new BindingList<string>(data.Mapping.ToList()),
                Interiors = new BindingList<string>(data.Interiors.ToList())
            };
        }

        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateMappingNode()
        {
            if (_nodes?.Any() == true)
            {
                if(_data.UseCustomMappingModule == 1)
                {
                    TreeNode Newcustomnode = new TreeNode("Custom Mappings")
                    {
                        Tag = "CustomMappings"
                    };
                    foreach (string s in _data.Mapping)
                    {
                        Newcustomnode.Nodes.Add(new TreeNode(s)
                        {
                            Tag = "CustomMapping"
                        });
                    }
                    if (_nodes.Last().Nodes.Count == 0)
                        _nodes.Last().Nodes.Add(Newcustomnode);
                    else 
                        _nodes.Last().Nodes.Insert(0, Newcustomnode);
                }
                else
                {
                    var nodesToRemove = _nodes.Last().Nodes
                        .Cast<TreeNode>()
                        .Where(n => n.Tag?.ToString() == "CustomMappings")
                        .ToList();

                    foreach (var node in nodesToRemove)
                    {
                        _nodes.Last().Nodes.Remove(node);
                    }
                }
            }
        }
        private void UpdateInteriorsNode()
        {
            if (_nodes?.Any() == true)
            {
                if (_data.BuildingInteriors == 1)
                {
                    TreeNode Newinteriornode = new TreeNode("Interiors")
                    {
                        Tag = "Interiors"
                    };
                    foreach (string s in _data.Interiors)
                    {
                        Newinteriornode.Nodes.Add(new TreeNode(s)
                        {
                            Tag = "Interior"
                        });
                    }
                    _nodes.Last().Nodes.Add(Newinteriornode);
                }
                else
                {
                    var nodesToRemove = _nodes.Last().Nodes
                        .Cast<TreeNode>()
                        .Where(n => n.Tag?.ToString() == "Interiors")
                        .ToList();

                    foreach (var node in nodesToRemove)
                    {
                        _nodes.Last().Nodes.Remove(node);
                    }
                }



            }
        }
        #endregion

        private void UseCustomMappingModuleCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseCustomMappingModule = UseCustomMappingModuleCB.Checked == true ? 1 : 0;
            
            UpdateMappingNode();
        }

        private void BuildingInteriorsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.BuildingInteriors = BuildingInteriorsCB.Checked == true ? 1 : 0;
            
            UpdateInteriorsNode();
        }

        private void BuildingIvysComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.BuildingIvys = (int)(buildingIvy)BuildingIvysComboBox.SelectedItem;
            
        }
    }
}