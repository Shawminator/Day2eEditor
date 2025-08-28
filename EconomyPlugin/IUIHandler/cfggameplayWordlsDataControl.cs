using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class cfggameplayWordlsDataControl : UserControl, IUIHandler
    {
        private Worldsdata _data;
        private Worldsdata _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfggameplayWordlsDataControl()
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
        public void LoadFromData(object data, List<TreeNode> selectedNodes)
        {
            // TODO: Replace ClassType with your actual type
            _data = data as Worldsdata ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            lightingConfigNUD.Value = _data.lightingConfig;
            JanMinNUD.Value = _data.environmentMinTemps[0];
            FebMinNUD.Value = _data.environmentMinTemps[1];
            MarMinNUD.Value = _data.environmentMinTemps[2];
            AprMinNUD.Value = _data.environmentMinTemps[3];
            MayMinNUD.Value = _data.environmentMinTemps[4];
            JunMinNUD.Value = _data.environmentMinTemps[5];
            JulMinNUD.Value = _data.environmentMinTemps[6];
            AugMinNUD.Value = _data.environmentMinTemps[7];
            SepMinNUD.Value = _data.environmentMinTemps[8];
            OctMinNUD.Value = _data.environmentMinTemps[9];
            NovMinNUD.Value = _data.environmentMinTemps[10];
            DecMinNUD.Value = _data.environmentMinTemps[11];

            JanMaxNUD.Value = _data.environmentMaxTemps[0];
            FebMaxNUD.Value = _data.environmentMaxTemps[1];
            MarMaxNUD.Value = _data.environmentMaxTemps[2];
            AprMaxNUD.Value = _data.environmentMaxTemps[3];
            MayMaxNUD.Value = _data.environmentMaxTemps[4];
            JunMaxNUD.Value = _data.environmentMaxTemps[5];
            JulMaxNUD.Value = _data.environmentMaxTemps[6];
            AugMaxNUD.Value = _data.environmentMaxTemps[7];
            SepMaxNUD.Value = _data.environmentMaxTemps[8];
            OctMaxNUD.Value = _data.environmentMaxTemps[9];
            NovMaxNUD.Value = _data.environmentMaxTemps[10];
            DecMaxNUD.Value = _data.environmentMaxTemps[11];

            wetnessWeightModifiers1NUD.Value = _data.wetnessWeightModifiers[0];
            wetnessWeightModifiers2NUD.Value = _data.wetnessWeightModifiers[1];
            wetnessWeightModifiers3NUD.Value = _data.wetnessWeightModifiers[2];
            wetnessWeightModifiers4NUD.Value = _data.wetnessWeightModifiers[3];
            wetnessWeightModifiers5NUD.Value = _data.wetnessWeightModifiers[4];

            _suppressEvents = false;
        }

        /// <summary>
        /// Applies changes to the data and updates the original snapshot
        /// </summary>
        public void ApplyChanges()
        {
            _originalData = CloneData(_data);
        }

        /// <summary>
        /// Resets control fields to the original data
        /// </summary>
        public void Reset()
        {
            // TODO: Reset control fields to _originalData
        }

        /// <summary>
        /// Checks if there are changes and updates the parent file's dirty state
        /// </summary>
        public void HasChanges()
        {
            if (_nodes?.Any() != true) return;

            // TODO: Replace Parentfile with your actual parent type if different
            var ef = _nodes.Last().FindParentOfType<CFGGameplayConfig>();
            if (ef != null)
                ef.isDirty = !_data.Equals(_originalData);
        }

        #region Helper Methods

        /// <summary>
        /// Clones the data for reset purposes
        /// </summary>
        private Worldsdata CloneData(Worldsdata data)
        {

            return new Worldsdata
            {
                lightingConfig = data.lightingConfig,
                environmentMinTemps = new BindingList<decimal>(data.environmentMinTemps.ToList()),
                environmentMaxTemps = new BindingList<decimal>(data.environmentMaxTemps.ToList()),
                wetnessWeightModifiers = new BindingList<decimal>(data.wetnessWeightModifiers.ToList())
                // objectSpawnersArr and playerRestrictedAreaFiles are intentionally excluded
            };

        }
        #endregion

        private void lightingConfigNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.lightingConfig = (int)lightingConfigNUD.Value;
            HasChanges();
        }

        private void MinTemp_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.environmentMinTemps[0] = JanMinNUD.Value;
            _data.environmentMinTemps[1] = FebMinNUD.Value;
            _data.environmentMinTemps[2] = MarMinNUD.Value;
            _data.environmentMinTemps[3] = AprMinNUD.Value;
            _data.environmentMinTemps[4] = MayMinNUD.Value;
            _data.environmentMinTemps[5] = JunMinNUD.Value;
            _data.environmentMinTemps[6] = JulMinNUD.Value;
            _data.environmentMinTemps[7] = AugMinNUD.Value;
            _data.environmentMinTemps[8] = SepMinNUD.Value;
            _data.environmentMinTemps[9] = OctMinNUD.Value;
            _data.environmentMinTemps[10] = NovMinNUD.Value;
            _data.environmentMinTemps[11] = DecMinNUD.Value;
            HasChanges();
        }
        private void MaxTemp_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.environmentMaxTemps[0] = JanMaxNUD.Value;
            _data.environmentMaxTemps[1] = FebMaxNUD.Value;
            _data.environmentMaxTemps[2] = MarMaxNUD.Value;
            _data.environmentMaxTemps[3] = AprMaxNUD.Value;
            _data.environmentMaxTemps[4] = MayMaxNUD.Value;
            _data.environmentMaxTemps[5] = JunMaxNUD.Value;
            _data.environmentMaxTemps[6] = JulMaxNUD.Value;
            _data.environmentMaxTemps[7] = AugMaxNUD.Value;
            _data.environmentMaxTemps[8] = SepMaxNUD.Value;
            _data.environmentMaxTemps[9] = OctMaxNUD.Value;
            _data.environmentMaxTemps[10] = NovMaxNUD.Value;
            _data.environmentMaxTemps[11] = DecMaxNUD.Value;
            HasChanges();
        }
        private void wetnessWeightModifiers_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.wetnessWeightModifiers[0] = wetnessWeightModifiers1NUD.Value;
            _data.wetnessWeightModifiers[1] = wetnessWeightModifiers2NUD.Value;
            _data.wetnessWeightModifiers[2] = wetnessWeightModifiers3NUD.Value;
            _data.wetnessWeightModifiers[3] = wetnessWeightModifiers4NUD.Value;
            _data.wetnessWeightModifiers[4] = wetnessWeightModifiers5NUD.Value;
            HasChanges();
        }
    }
}