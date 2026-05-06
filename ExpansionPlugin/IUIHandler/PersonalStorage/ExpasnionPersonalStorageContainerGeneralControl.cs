using Day2eEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ExpansionPlugin
{
    public partial class ExpasnionPersonalStorageContainerGeneralControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionPersonalStorageConfig _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpasnionPersonalStorageContainerGeneralControl()
        {
            InitializeComponent();
        }

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as ExpansionPersonalStorageConfig ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;
            List<ComboItem> comboItems = new List<ComboItem>();
            foreach (ExpansionQuestQuest quest in AppServices.GetRequired<ExpansionManager>().ExpansionQuestQuestConfig.MutableItems)
            {
                comboItems.Add(new ComboItem()
                {
                    Id = (int)quest.ID,
                    Name = $"Quest {(int)quest.ID}: {quest.Title}"
                });
            }
            comboItems.Insert(0, new ComboItem()
            {
                Id = -1,
                Name = $"Quest {-1}: No Quest Selected"
            });
            QuestIDCB.DataSource = comboItems.OrderBy(x => x.Id).ToList();
            QuestIDCB.DisplayMember = "Name";
            QuestIDCB.ValueMember = "Id";

            BindingList<string> Icons = new BindingList<string>(File.ReadAllLines("Data\\ExpansionIconnames.txt").ToList());
            IconCB.DataSource = Icons;
            BindingList<string> Factions = new BindingList<string>(File.ReadAllLines("Data\\ExpansionFactions.txt").ToList());
            Factions.Insert(0, "");
            FactionCB.DataSource = Factions;

            FileNameTB.Text = Path.GetFileNameWithoutExtension(_data.FileName);
            StorageIDNUD.Value = (int)_data.StorageID;
            PSClassNameTB.Text = _data.ClassName;
            PSDisplayNameTB.Text = _data.DisplayName;
            IconCB.SelectedIndex = IconCB.FindStringExact(_data.DisplayIcon);
            QuestIDCB.SelectedValue = (int)_data.QuestID;
            ReputationNUD.Value = (int)_data.Reputation;
            FactionCB.SelectedIndex = FactionCB.FindStringExact(_data.Faction);
            IsGlobalStorageCB.Checked = _data.IsGlobalStorage == 1 ? true : false;
            GetIcon();

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Parent.Text = _data.FileName;
            }
        }
        private void GetIcon()
        {
            string iconname = _data.DisplayIcon.Replace("/", "");
            var resourceName = $"ExpansionPlugin.Icons.{iconname}.png";
            var stream = ResourceHelper.OpenEmbeddedStream(resourceName);
            if (stream != null)
            {
                Bitmap image = new Bitmap(Image.FromStream(stream));
                Image image3 = resizeImage(image, new Size(128, 128));
                pictureBox1.Image = image3;
            }
            else
            {
                pictureBox1.Image = null;
            }
        }
        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        private void FileNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            string dirName = Path.GetDirectoryName(_data._path);
            string newFilename = FileNameTB.Text + ".json";
            _data.SetPath(Path.Combine(dirName, newFilename));
            UpdateTreeNodeText();
        }

        private void StorageIDNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            List<int> AllPSIDs = AppServices.GetRequired<ExpansionManager>().ExpansionPersonalStorageContainersConfig.UsedIDS;
            int currentid = (int)_data.StorageID;
            int newid = (int)StorageIDNUD.Value;
            if (AllPSIDs.Contains(newid))
            {
                MessageBox.Show($"Personal Storage ID {newid} is allready in use, Please select a different ID");
                _suppressEvents = true;
                StorageIDNUD.Value = (int)_data.StorageID;
                _suppressEvents = false;
            }
            else
            {
                _data.StorageID = (int)StorageIDNUD.Value;
                AppServices.GetRequired<ExpansionManager>().ExpansionPersonalStorageContainersConfig.UsedIDS.Remove(currentid);
                AppServices.GetRequired<ExpansionManager>().ExpansionPersonalStorageContainersConfig.UsedIDS.Add(newid);
            }
        }

        private void PSClassNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ClassName = PSClassNameTB.Text;
        }

        private void PSDisplayNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DisplayName = PSDisplayNameTB.Text;
        }
        private void IconCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DisplayIcon = IconCB.SelectedItem.ToString();
            GetIcon();
        }

        private void QuestIDCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.QuestID = (int)QuestIDCB.SelectedValue;
        }

        private void ReputationNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Reputation = (int)ReputationNUD.Value;

        }

        private void FactionCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Faction = FactionCB.GetItemText(FactionCB.SelectedItem);
        }

        private void IsGlobalStorageCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.IsGlobalStorage = IsGlobalStorageCB.Checked == true ? 1 : 0;
        }


    }
    public class ComboItem
    {
        public int Id { get; set; }        // value
        public string Name { get; set; }   // display
    }

}