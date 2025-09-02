using Day2eEditor;
using System.ComponentModel;
using System.Net.Mail;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class SpawnableTypesAttachmentsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private spawnableTypeAttachment _data;
        private spawnableTypeAttachment _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public BindingList<randompresetsAttachments> cargoAttachments = new BindingList<randompresetsAttachments>();
        

        public SpawnableTypesAttachmentsControl()
        {
            InitializeComponent();
            foreach (cfgrandompresetsFile rpf in AppServices.GetRequired<EconomyManager>().cfgrandompresetsConfig.AllData)
            {
                foreach (var item in rpf.Data.Items)
                {
                    if (item is randompresetsAttachments rpa)
                    {
                        cargoAttachments.Add(rpa);
                    }
                }
            }
            AttachmentPresetComboBox.DataSource = cargoAttachments;
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
            _data = data as spawnableTypeAttachment ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            AttachmentchanceNUD.Visible = UseAttachmentchanceCB.Checked = _data.chanceSpecified;
            AttachmentchanceNUD.Value = _data.chance;
            if (AttachmentPresetGB.Visible = AttachemntTB.Visible = isAttchmentIsPresetCB.Checked = _data.preset != null)
            {
                AttachemntTB.Text = _data.preset;
            }

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
            var parentObj = _nodes.Last().FindParentOfType(_parentType);
            if (parentObj != null)
            {
                dynamic parent = parentObj;
                parent.isDirty = !_data.Equals(_originalData);
            }
        }

        #region Helper Methods

        /// <summary>
        /// Clones the data for reset purposes
        /// </summary>
        private spawnableTypeAttachment CloneData(spawnableTypeAttachment data)
        {
            // TODO: Implement actual cloning logic
            return new spawnableTypeAttachment
            {
                chance = data.chance,
                chanceSpecified = data.chanceSpecified,
                preset = data.preset,
                damage = null,
                item = null
            };
        }

        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                string attachmentstring = "Attachments :";
                if (_data.preset != null)
                {
                    attachmentstring += " Preset = " + _data.preset;
                }
                if (_data.chanceSpecified)
                {
                    attachmentstring += " Chance = " + _data.chance;
                }
                _nodes.Last().Text = attachmentstring;
            }
        }

        #endregion

        private void UseAttachmentchanceCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            AttachmentchanceNUD.Visible = _data.chanceSpecified = UseAttachmentchanceCB.Checked;
            UpdateTreeNodeText();
            HasChanges();
        }
        private void AttachmentchanceNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.chance = AttachmentchanceNUD.Value;
            UpdateTreeNodeText();
            HasChanges();
        }
        private void isAttchmentIsPresetCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            if (AttachemntTB.Visible = AttachmentPresetGB.Visible = isAttchmentIsPresetCB.Checked)
            {
                AttachemntTB.Text = _data.preset;
            }
            else
            {
                _data.preset = null;
            }
            UpdateTreeNodeText();
            HasChanges();
        }

        private void darkButton37_Click(object sender, EventArgs e)
        {
            randompresetsAttachments newattachmentpreset = AttachmentPresetComboBox.SelectedItem as randompresetsAttachments;
            AttachemntTB.Text = _data.preset = newattachmentpreset.name;
            UpdateTreeNodeText();
            HasChanges();
        }
    }
}