using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Windows.Forms;

namespace Day2eEditor
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ChangeToolstrip
        {
            get => toolStripStatusLabel1.Text;
            set { toolStripStatusLabel1.Text = value; }
        }

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        private List<IPluginForm> plugins = new List<IPluginForm>();
        private bool hidden;
        
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
        public Form1()
        {

            InitializeComponent();
            Form_Controls.InitializeForm_Controls
            (
                this,
                TitlePanel,
                ResizePanel,
                TitleLabel,
                label1,
                CloseButton,
                MinimiseButton

            );
            LoadPlugins();
            slidePanel.Width = 30;
            hidden = true;
        }
        void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            ListBox lb = sender as ListBox;

            // Optional: Adjust horizontal scroll
            var CurrentItemWidth = (int)this.CreateGraphics().MeasureString(
                lb.Items[lb.Items.Count - 1].ToString(),
                lb.Font,
                TextRenderer.MeasureText(lb.Items[lb.Items.Count - 1].ToString(), new Font("Arial", 20.0F)).Width
            ).Width;
            lb.HorizontalExtent = CurrentItemWidth + 5;

            // Always draw background the same — no highlight
            e.Graphics.FillRectangle(new SolidBrush(SystemColors.ControlDarkDark), e.Bounds);

            // Always use same text color
            Brush textBrush = Brushes.White;

            // Draw centered text
            string itemText = lb.Items[e.Index].ToString();
            SizeF textSize = e.Graphics.MeasureString(itemText, e.Font);
            float x = e.Bounds.Left + (e.Bounds.Width - textSize.Width) / 2;
            float y = e.Bounds.Top + (e.Bounds.Height - textSize.Height) / 2;
            e.Graphics.DrawString(itemText, e.Font, textBrush, x, y);

            // Skip focus rectangle to avoid drawing dotted outline
            // e.DrawFocusRectangle(); // Commented out to remove any visual indication
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            var settings = SettingsManager.Load();

            if (settings != null)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.WindowState = settings.FormState;
                this.Location = settings.FormLocation;
                this.Size = settings.FormSize;
                ShowConsoleCB.Checked = settings.ShowConsole;
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            var settings = new AppSettings
            {
                FormState = this.WindowState,
                FormLocation = this.WindowState == FormWindowState.Normal ? this.Location : this.RestoreBounds.Location,
                FormSize = this.WindowState == FormWindowState.Normal ? this.Size : this.RestoreBounds.Size,
                ShowConsole = ShowConsoleCB.Checked
            };

            SettingsManager.Save(settings);
        }
        private void ShowConsoleCB_CheckedChanged(object sender, EventArgs e)
        {
            HideConsole(ShowConsoleCB.Checked);
        }
        private void HideConsole(bool Hide)
        {
            IntPtr handle = GetConsoleWindow();
            if (Hide)
                ShowWindow(handle, SW_SHOW);
            else
            {
                ShowWindow(handle, SW_HIDE);
            }
        }
        private void LoadPlugins()
        {
            pluginListbox.Items.Clear();
            pluginListbox.Items.Add("Donate");
            pluginListbox.Items.Add("Discord");
            string pluginPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");
            if (!Directory.Exists(pluginPath))
            {
                Directory.CreateDirectory(pluginPath);
                return;
            }
            foreach (var dll in Directory.GetFiles(pluginPath, "*.dll"))
            {
                try
                {
                    var asm = Assembly.LoadFrom(dll);
                    var types = asm.GetTypes().Where(t => typeof(IPluginForm).IsAssignableFrom(t) && !t.IsInterface);
                    foreach (var type in types)
                    {
                        var plugin = (IPluginForm)Activator.CreateInstance(type);
                        plugins.Add(plugin);
                        pluginListbox.Items.Add(plugin.pluginName);
                    }
                }
                catch { }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (hidden)
            {
                slidePanel.Width = slidePanel.Width + 10;
                if (slidePanel.Width == 200)
                {
                    timer1.Stop();
                    hidden = false;
                    Refresh();
                }
            }
            else
            {
                slidePanel.Width = slidePanel.Width - 10;
                if (slidePanel.Width == 30)
                {
                    timer1.Stop();
                    hidden = true;
                    Refresh();
                }
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {
            if (sender is Label)
            {
                Label pb = sender as Label;
                if (pb.Name == "HidePBox")
                {
                    //ToolStrip1.Visible = false;
                    if (!hidden)
                        timer1.Start();
                }
                else if (pb.Name == "Slidelabel")
                {
                    //ShowButtons();
                    timer1.Start();
                }
            }
            else if (sender is Panel)
            {
                Panel p = sender as Panel;
                if (p.Name == "SlidePanel")
                {
                    //ShowButtons();
                    timer1.Start();
                }
            }
        }
        void OpenUrl(string url)
        {
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
            }
            catch (Exception ex)
            {
                // Optional: Log or show error
                Console.WriteLine("Could not open URL: " + ex.Message);
            }
        }
        private void pluginListbox_Click(object sender, EventArgs e)
        {
            var selectedItem = pluginListbox.SelectedItem?.ToString();
            if(selectedItem == "Donate")
            {
                OpenUrl("https://www.paypal.me/ADecadeOfdecay");
            }
            else if (selectedItem == "Discord")
            {
                OpenUrl("https://discord.gg/5EHE49Kjsv");
            }
            else
            {
                var plugin = plugins.FirstOrDefault(p => p.pluginName == selectedItem);
                if (plugin != null)
                {
                    var _TM = Application.OpenForms[plugin.pluginIdentifier] as Form;
                    if (_TM != null)
                    {
                        _TM.WindowState = FormWindowState.Normal;
                        _TM.BringToFront();
                        _TM.Activate();
                    }
                    else
                    {
                        closeMdiChildren();
                        var form = plugin.GetForm();
                        form.MdiParent = this;
                        form.Location = new Point(30,0);
                        form.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                        form.Size = this.Size - new Size(40, 65);
                        form.Show();
                        Console.WriteLine("loading Project manager....");
                        label1.Text = "Project Manager";
                    }
                }
            }
            timer1.Start();
        }
        private void closeMdiChildren()
        {
            if (MdiChildren.Length > 0)
            {
                MdiChildren[0].Close();
            }
        }
    }
}

