using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EconomyPlugin
{
    public partial class TypesCollectionControl : UserControl, IUIHandler
    {
        private Type _parentType;
        public Control GetControl() => this;
        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as TypesFile ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            if (selectedNodes[0].Tag is Category)
            {
                Cat = selectedNodes[0].Tag as Category;
                isCat = true;
                CollectionNameTB.Text = Cat.Name;
                label1.Text = "Category:-";
                UpdateTypesFileButton.Visible = false;
            }
            else
            {
                CollectionNameTB.Text = _data.FileName;
                label1.Text = "Filename:-";
                UpdateTypesFileButton.Visible = true;
            }

        }
        public void ApplyChanges()
        {

        }
        public void Reset()
        {

        }
        public void HasChanges()
        {
        }

        private TypesFile _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;
        private TypeEntry _originalData;
        private Category Cat;
        private bool isCat = false;

        public TypesCollectionControl()
        {
            InitializeComponent();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string title = "";
            if (isCat)
                title = Cat.Name;
            else
                title = _data.FileName;
            foreach (TypeEntry te in _data.Data.TypeList)
            {
                if (isCat)
                {
                    if ((te.Category != null && te.Category.Name == Cat.Name) ||
                        (te.Category == null && Cat.Name == "other"))
                    {
                        te.Nominal = 0;
                        te.Min = 0;
                    }
                }
                else
                {
                    te.Nominal = 0;
                    te.Min = 0;
                }
            }
            Console.WriteLine($"[INFO] Zeroing Complete for all entires in {title}");
            _data.isDirty = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                TypesFile newfile = new TypesFile(openFileDialog.FileName);
                newfile.Load();

                foreach (var newEntry in newfile.Data.TypeList)
                {
                    if (!_data.Data.TypeList.Any(e => e.Name == newEntry.Name))
                    {
                        Console.WriteLine($"[INFO] {newEntry.Name} added to {_data.FileName}");
                        _data.Data.TypeList.Add(newEntry);
                    }
                }

            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string title = "";
            if (isCat)
                title = Cat.Name;
            else
                title = _data.FileName;

            foreach (TypeEntry te in _data.Data.TypeList)
            {
                if (isCat)
                {
                    if ((te.Category != null && te.Category.Name == Cat.Name) ||
                        (te.Category == null && Cat.Name == "other"))
                    {
                        te.Min = te.Nominal;
                    }
                }
                else
                {
                    te.Min = te.Nominal;
                }
            }
            Console.WriteLine($"[INFO] Syncing Minimum to Nominal for all Entries in {title}");
            _data.isDirty = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string title = "";
            if (isCat)
                title = Cat.Name;
            else
                title = _data.FileName;

            foreach (TypeEntry te in _data.Data.TypeList)
            {
                if (isCat)
                {
                    if ((te.Category != null && te.Category.Name == Cat.Name) ||
                        (te.Category == null && Cat.Name == "other"))
                    {
                        te.Nominal = te.Min;
                    }
                }
                else
                {
                    te.Nominal = te.Min;
                }
            }
            Console.WriteLine($"[INFO] Syncing Nominal to Minimum for all Entries in {title}");
            _data.isDirty = true;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            string title = "";
            if (isCat)
                title = Cat.Name;
            else
                title = _data.FileName;
            foreach (TypeEntry te in _data.Data.TypeList)
            {
                if (isCat)
                {
                    if ((te.Category != null && te.Category.Name == Cat.Name) ||
                        (te.Category == null && Cat.Name == "other"))
                    {
                        te.Nominal = (int)CollectionCustomNUD.Value;
                        if (ChangeMinCB.Checked)
                            te.Min = (int)CollectionCustomNUD.Value;
                    }
                }
                else
                {
                    te.Nominal = (int)CollectionCustomNUD.Value; ;
                    if (ChangeMinCB.Checked)
                        te.Min = (int)CollectionCustomNUD.Value;
                }
            }
            Console.WriteLine($"[INFO] All Entries Nominal Value Set to {CollectionCustomNUD.Value} in {title}");
            if (ChangeMinCB.Checked)
                Console.WriteLine($"[INFO] All Entries Minimum Value Set to {CollectionCustomNUD.Value} in {title}");

            _data.isDirty = true;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            string title = "";
            if (isCat)
                title = Cat.Name;
            else
                title = _data.FileName;
            foreach (TypeEntry te in _data.Data.TypeList)
            {
                if (isCat)
                {
                    if ((te.Category != null && te.Category.Name == Cat.Name) ||
                        (te.Category == null && Cat.Name == "other"))
                    {
                        Domultiplier(te);
                    }
                }
                else
                {
                    Domultiplier(te);
                }
            }
            Console.WriteLine($"[INFO] All Entries Nominal Value Set to {MultiplierCB.GetItemText(MultiplierCB.SelectedItem)} in {title}");
            if (ChangeMinCheckBox.Checked)
                Console.WriteLine($"[INFO] All Entries Minimum Value Set to {MultiplierCB.GetItemText(MultiplierCB.SelectedItem)} in {title}");

            _data.isDirty = true;
        }
        private void Domultiplier(TypeEntry item)
        {
            if (!item.NominalSpecified) { return; }
            if (item.Nominal != 0)
            {
                item.Nominal = getmultiplier((int)item.Nominal);
                if (item.Nominal == 0 && if0setto1CB.Checked)
                    item.Nominal = 1;
            }
            if (ChangeMinCheckBox.Checked)
            {
                if (item.Min != 0)
                {
                    item.Min = getmultiplier((int)item.Min);
                    if (item.Min == 0 && if0setto1CB.Checked)
                        item.Min = 1;
                }
            }
        }
        private int getmultiplier(int nominal)
        {
            switch (MultiplierCB.SelectedIndex)
            {
                case 0:
                    return nominal * 10;
                case 1:
                    return nominal * 9;
                case 2:
                    return nominal * 8;
                case 3:
                    return nominal * 7;
                case 4:
                    return nominal * 6;
                case 5:
                    return nominal * 5;
                case 6:
                    return nominal * 4;
                case 7:
                    return nominal * 3;
                case 8:
                    return nominal * 2;
                case 9:
                    return (int)((float)(nominal * 1.5));
                case 10:
                    return (int)((float)(nominal / 1.5));
                case 11:
                    return nominal / 2;
                case 12:
                    return nominal / 3;
                case 13:
                    return nominal / 4;
                case 14:
                    return nominal / 5;
                case 15:
                    return nominal / 6;
                case 16:
                    return nominal / 7;
                case 17:
                    return nominal / 8;
                case 18:
                    return nominal / 9;
                case 19:
                    return nominal / 10;
                default:
                    return 0;
            }
        }
    }
}
