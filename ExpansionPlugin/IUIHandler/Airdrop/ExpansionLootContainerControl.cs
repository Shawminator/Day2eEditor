using Day2eEditor;
using System.ComponentModel;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionLootContainerControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionLootContainer _data;
        private ExpansionLootContainer _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionLootContainerControl()
        {
            InitializeComponent();
            _suppressEvents = true;
            comboBox2.DataSource = Enum.GetValues(typeof(ContainerTypes));
            _suppressEvents = false;
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
            _data = data as ExpansionLootContainer ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = _data.Clone();

            _suppressEvents = true;

            comboBox2.SelectedItem = getContainertype(_data.Container);
            numericUpDown8.Value = _data.Usage;
            numericUpDown32.Value = _data.FallSpeed;
            numericUpDown9.Value = (decimal)_data.Weight;
            numericUpDown10.Value = _data.ItemCount;
            numericUpDown11.Value = _data.InfectedCount;
            checkBox5.Checked = _data.SpawnInfectedForPlayerCalledDrops.Equals(1) ? true : false;
            darkLabel19.Text = "Number of Loot Items = " + _data.Loot.Count.ToString();
            darkLabel20.Text = "Number of Infected = " + _data.Infected.Count.ToString();
            switch (_data.ExplodeAirVehiclesOnCollision)
            {
                case -1:
                    radioButton6.Checked = true;
                    break;
                case 0:
                    radioButton5.Checked = true;
                    break;
                case 1:
                    radioButton4.Checked = true;
                    break;
            }

            _suppressEvents = false;
        }
        public ContainerTypes getContainertype(string container)
        {
            switch (container)
            {
                case "ExpansionAirdropContainer":
                    return ContainerTypes.ExpansionAirdropContainer;
                case "ExpansionAirdropContainer_Basebuilding":
                    return ContainerTypes.ExpansionAirdropContainer_Basebuilding;
                case "ExpansionAirdropContainer_Blue":
                    return ContainerTypes.ExpansionAirdropContainer_Blue;
                case "ExpansionAirdropContainer_Grey":
                    return ContainerTypes.ExpansionAirdropContainer_Grey;
                case "ExpansionAirdropContainer_Medical":
                    return ContainerTypes.ExpansionAirdropContainer_Medical;
                case "ExpansionAirdropContainer_Military":
                    return ContainerTypes.ExpansionAirdropContainer_Military;
                case "ExpansionAirdropContainer_Military_GreenCamo":
                    return ContainerTypes.ExpansionAirdropContainer_Military_GreenCamo;
                case "ExpansionAirdropContainer_Military_MarineCamo":
                    return ContainerTypes.ExpansionAirdropContainer_Military_MarineCamo;
                case "ExpansionAirdropContainer_Military_OliveCamo":
                    return ContainerTypes.ExpansionAirdropContainer_Military_OliveCamo;
                case "ExpansionAirdropContainer_Military_OliveCamo2":
                    return ContainerTypes.ExpansionAirdropContainer_Military_OliveCamo2;
                case "ExpansionAirdropContainer_Military_WinterCamo":
                    return ContainerTypes.ExpansionAirdropContainer_Military_WinterCamo;
                case "ExpansionAirdropContainer_Olive":
                    return ContainerTypes.ExpansionAirdropContainer_Olive;
                default:
                    return ContainerTypes.ExpansionAirdropContainer;
            }
        }
        public String getContainerString(ContainerTypes containertype)
        {
            switch (containertype)
            {
                case ContainerTypes.ExpansionAirdropContainer:
                    return "ExpansionAirdropContainer";
                case ContainerTypes.ExpansionAirdropContainer_Medical:
                    return "ExpansionAirdropContainer_Medical";
                case ContainerTypes.ExpansionAirdropContainer_Military:
                    return "ExpansionAirdropContainer_Military";
                case ContainerTypes.ExpansionAirdropContainer_Basebuilding:
                    return "ExpansionAirdropContainer_Basebuilding";
                case ContainerTypes.ExpansionAirdropContainer_Grey:
                    return "ExpansionAirdropContainer_Grey";
                case ContainerTypes.ExpansionAirdropContainer_Blue:
                    return "ExpansionAirdropContainer_Blue";
                case ContainerTypes.ExpansionAirdropContainer_Olive:
                    return "ExpansionAirdropContainer_Olive";
                case ContainerTypes.ExpansionAirdropContainer_Military_GreenCamo:
                    return "ExpansionAirdropContainer_Military_GreenCamo";
                case ContainerTypes.ExpansionAirdropContainer_Military_MarineCamo:
                    return "ExpansionAirdropContainer_Military_MarineCamo";
                case ContainerTypes.ExpansionAirdropContainer_Military_OliveCamo:
                    return "ExpansionAirdropContainer_Military_OliveCamo";
                case ContainerTypes.ExpansionAirdropContainer_Military_OliveCamo2:
                    return "ExpansionAirdropContainer_Military_OliveCamo2";
                case ContainerTypes.ExpansionAirdropContainer_Military_WinterCamo:
                    return "ExpansionAirdropContainer_Military_WinterCamo";
                default:
                    return "ExpansionAirdropContainer";
            }
        }
        /// <summary>
        /// Applies changes to the data and updates the original snapshot
        /// </summary>
        public void ApplyChanges()
        {
            _originalData = _data.Clone();
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
            var parentObj = _nodes.Last().FindParentOfType(_parentType);
            if (parentObj != null)
            {
                dynamic parent = parentObj;
                parent.isDirty = !_data.Equals(_originalData);
            }
        }

        #region Helper Methods
        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = _data.Container;
            }
        }

        #endregion

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Container = getContainerString((ContainerTypes)comboBox2.SelectedItem);
            HasChanges();
            UpdateTreeNodeText();
        }
        private void numericUpDown8_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Usage = (int)numericUpDown8.Value;
            HasChanges();

        }
        private void numericUpDown32_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.FallSpeed = numericUpDown32.Value;
            HasChanges();
        }
        private void numericUpDown9_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Weight = (decimal)numericUpDown9.Value;
            HasChanges();
        }
        private void numericUpDown10_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ItemCount = (int)numericUpDown10.Value;
            HasChanges();
        }
        private void numericUpDown11_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.InfectedCount = (int)numericUpDown11.Value;
            HasChanges();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SpawnInfectedForPlayerCalledDrops = checkBox5.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void ExplodeAirVehiclesOnCollision_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            RadioButton rb = sender as RadioButton;
            if(rb.Checked)
            {
                if (rb.Name == "radioButton6")
                    _data.ExplodeAirVehiclesOnCollision = -1;
                else if (rb.Name == "radioButton4")
                    _data.ExplodeAirVehiclesOnCollision = 1;
                else if (rb.Name == "radioButton5")
                    _data.ExplodeAirVehiclesOnCollision = 0;
            }
             HasChanges();
        }
    }
}