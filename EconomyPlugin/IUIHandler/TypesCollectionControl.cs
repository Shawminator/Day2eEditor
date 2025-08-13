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

namespace EconomyPlugin
{
    public partial class TypesCollectionControl : UserControl, IUIHandler
    {
        private TypesFile _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;
        private TypeEntry _originalData;
        private Category Cat;
        private bool isCat = false;

        public Control GetControl() => this;
        public TypesCollectionControl()
        {
            InitializeComponent();
        }
        public void LoadFromData(object data, List<TreeNode> selectedNodes)
        {
            _data = data as TypesFile ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            if (selectedNodes[0].Tag is Category)
            {
                Cat = selectedNodes[0].Tag as Category;
                isCat = true;
                textBox1.Text = Cat.Name;
                label1.Text = "Category:-";
                button1.Visible = false;
            }
            else
            {
                textBox1.Text = _data.FileName;
                label1.Text = "Filename:-";
                button1.Visible = true;
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
                        if(ChangeMinCB.Checked)
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
            if(ChangeMinCB.Checked)
                Console.WriteLine($"[INFO] All Entries Minimum Value Set to {CollectionCustomNUD.Value} in {title}");

            _data.isDirty = true;
        }
    }
}
