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
    public partial class TerritoryZonesControl : UserControl, IUIHandler
    {
        public event Action<territorytypeTerritoryZone> PositionChanged;
        private Type _parentType;
        private territorytypeTerritoryZone _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public TerritoryZonesControl()
        {
            InitializeComponent();

        }

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as territorytypeTerritoryZone ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            TerritoriesZonesRadiusNUD.Value = _data.r;
            TerritoriesZonesStaticMInNUD.Value = _data.smin;
            TerritoriesZonesStaticMaxNUD.Value = _data.smax;
            TerritoriesZonesDynamicMinNUD.Value = _data.dmin;
            TerritoriesZonesDynamicMaxNUD.Value = _data.dmax;
            TerritoriesZonesPOSXNUD.Value = _data.x;
            TerritoriesZonesPOSZNUD.Value = _data.z;
            TerritoriesZonesUseYCB.Checked = TerritoriesZonesPOSYNUD.Visible = _data.ySpecified;
            TerritoriesZonesPOSYNUD.Value = _data.y;
            RadioButton rb = groupBox74.Controls
                              .OfType<RadioButton>()
                              .FirstOrDefault(x => x.Text == _data.name);
            if (rb != null)
                rb.Checked = true;
            else
            {
                TerritoriesZonesDynamicRB.Checked = true;
                TerritoriesZonesDynamicTB.Text = _data.name;
            }

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = _data.ToString();
            }
        }
        private void TerritoriesZonesPOSXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.x = TerritoriesZonesPOSXNUD.Value;
            PositionChanged?.Invoke(_data);
        }
        private void TerritoriesZonesPOSZNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.z = TerritoriesZonesPOSZNUD.Value;
            PositionChanged?.Invoke(_data);
        }
        private void TerritoriesZonesRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.r = TerritoriesZonesRadiusNUD.Value;
            PositionChanged?.Invoke(_data);
        }
        private void TerritoriesZonesStaticMInNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.smin = (int)TerritoriesZonesStaticMInNUD.Value;
        }
        private void TerritoriesZonesStaticMaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.smax = (int)TerritoriesZonesStaticMaxNUD.Value;
        }
        private void TerritoriesZonesDynamicMinNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.dmin = (int)TerritoriesZonesDynamicMinNUD.Value;
        }
        private void TerritoriesZonesDynamicMaxNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.dmax = (int)TerritoriesZonesDynamicMaxNUD.Value;
          }
        private void TerritoriesZonesUseYCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ySpecified = TerritoriesZonesUseYCB.Checked;
        }
        private void TerritoriesZonesPOSYNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.y = TerritoriesZonesPOSYNUD.Value;
        }
        private void TerritoriesZonesDynamicTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.name = TerritoriesZonesDynamicTB.Text;
            UpdateTreeNodeText();
        }
        private void TerritoriesZonesAIUSage_CheckedChanged(object sender, EventArgs e)
        {
            if (TerritoriesZonesDynamicRB.Checked)
            {
                TerritoriesZonesDynamicTB.Visible = true;
            }
            else
            {
                TerritoriesZonesDynamicTB.Visible = false;
            }
            if (_suppressEvents) return;
            RadioButton rb = groupBox74.Controls
                              .OfType<RadioButton>()
                              .FirstOrDefault(x => x.Checked == true);
            if (rb.Text == "Dynamic")
                _data.name = TerritoriesZonesDynamicTB.Text;
            else
                _data.name = rb.Text;
            UpdateTreeNodeText();
        }
    }
}