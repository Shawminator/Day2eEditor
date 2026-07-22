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
        public BindingList<eventsEvent> _eentries = new BindingList<eventsEvent>();
        public BindingList<SpawnableType> _stentries = new BindingList<SpawnableType>();
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
        public string SetLaable2
        {
            set { label2.Text = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Button4visable
        {
            set { button4.Visible = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Button5text
        {
            set { button5.Text = value; }
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
        public void HideCEStuff()
        {
            label3.Visible = false;
            textBox2.Visible = false;
            SelectProjectFolderbutton.Visible = false;
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
                if (label1.Text == "Add New Event File")
                {
                    EventsFile newevents = new EventsFile(openfile.FileName);
                    _eentries = newevents.Data.@event;
                    textBox1.Text = Path.GetFileNameWithoutExtension(openfile.FileName);
                    MessageBox.Show($"{newevents.Data.@event.Count} evenmts loaded,\nplease import once you have set filename and directory");
                }
                else if (label1.Text == "Add new Spawnable Types")
                {
                    var item = new CfgSpawnableTypesFile(openfile.FileName);

                    item.Data = AppServices.GetRequired<FileService>().LoadOrCreateXml(
                        openfile.FileName,
                        createNew: () => new SpawnableTypes
                        {
                            type = new BindingList<SpawnableType>()
                        },
                        onError: ex =>
                        {
                            item.HasErrors = true;

                            var message =
                                $"Error in {Path.GetFileName(openfile.FileName)}\n{ex.Message}\n{ex.InnerException?.Message}";

                            Console.WriteLine(message + "\n");
                            item.Errors.Add(message);
                        },
                        configName: "cfgspawnabletypes"
                    );

                    item.Data.type ??= new BindingList<SpawnableType>();
                    _stentries = item.Data.type;


                    MessageBox.Show($"{_stentries.Count} spawnabletypes loaded,\nplease import once you have set filename and directory");
                }
            }
        }
    }

}


