using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Day2eEditor
{
    public partial class AddEventFile : Form
    {
        private FormController controller;
        private readonly BindingSource _binding = new();
        public BindingList<eventsEvent> _entries = new BindingList<eventsEvent>();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string moddir
        {
            get { return textBox2.Text; }
            set { textBox2.Text = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string typesname
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SetTitle
        {
            set { label1.Text = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Button4visable
        {
            set { button4.Visible = value; }
        }
        public AddEventFile()
        {
            InitializeComponent();
            controller = new FormController(
                this,
                TitlePanel,
                null,
                TitleLabel,
                label1,
                CloseButton,
                null
            );
            this.Disposed += (s, e) => controller.Dispose();
        }

        private void AddEventFile_Load(object sender, EventArgs e)
        {
            var economymanager = AppServices.GetRequired<EconomyManager>();
        }
        private void SelectProjectFolderbutton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.InitialDirectory = AppServices.GetRequired<EconomyManager>().basePath;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = dialog.SelectedPath.Replace(AppServices.GetRequired<EconomyManager>().basePath + "\\", "").Replace("\\", "/");
                moddir = textBox2.Text;
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            typesname = textBox1.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                EventsFile newevents = new EventsFile(openfile.FileName);
                newevents.Load();
                _entries = newevents.Data.@event;
                textBox1.Text = Path.GetFileNameWithoutExtension(openfile.FileName);
                MessageBox.Show($"{newevents.Data.@event.Count} evenmts loaded,\nplease import once you have set filename and directory");
            }
        }
    }

}


