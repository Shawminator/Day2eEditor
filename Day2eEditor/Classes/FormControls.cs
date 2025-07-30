namespace Day2eEditor
{
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public static class SettingsManager
    {
        private static string SettingsPath =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Day2eEditor", "settings.json");

        public static AppSettings Load()
        {
            if (!File.Exists(SettingsPath))
                return null;

            var json = File.ReadAllText(SettingsPath);
            return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
        }

        public static void Save(AppSettings settings)
        {
            var dir = Path.GetDirectoryName(SettingsPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SettingsPath, json);
        }
    }
    public class AppSettings
    {
        public FormWindowState FormState { get; set; }
        public Point FormLocation { get; set; }
        public Size FormSize { get; set; }
        public bool ShowConsole { get; set; }
    }
    public static class Form_Controls
    {
#pragma warning disable CS8618
#pragma warning disable CS8622
        public static Size Formsize;
        private static Button B;
        private static Button B1;
        private static Panel P;
        private static Panel P2;
        private static Label L;
        private static Label L2;
        private static Form F;
        private static bool mouseDown;
        private static Point lastLocation;
        private static Size LastSize;
        private static Point LastMousePositon;

        public static int Height { get; private set; }

        public static void InitializeForm_Controls(Form _F, Panel _P, Panel _P2, Label _L, Label _L2, Button _B, Button _B1)
        {
            F = _F;
            P = _P;
            P2 = _P2;
            B = _B;
            B1 = _B1;
            L = _L;
            L2 = _L2;

            P.MouseDoubleClick += new MouseEventHandler(FormMax_MouseDoubleClick);
            P.MouseDown += new MouseEventHandler(FormMove_MouseDown);
            P.MouseMove += new MouseEventHandler(FormMove_MouseMove);
            P.MouseUp += new MouseEventHandler(FormMove_MouseUp);
            P2.MouseDown += new MouseEventHandler(FormResize_MouseDown);
            P2.MouseMove += new MouseEventHandler(FormResize_MouseMove);
            P2.MouseUp += new MouseEventHandler(FormResize_MouseUp);
            L.MouseDoubleClick += new MouseEventHandler(FormMax_MouseDoubleClick);
            L.MouseDown += new MouseEventHandler(FormMove_MouseDown);
            L.MouseMove += new MouseEventHandler(FormMove_MouseMove);
            L.MouseUp += new MouseEventHandler(FormMove_MouseUp);
            L2.MouseDoubleClick += new MouseEventHandler(FormMax_MouseDoubleClick);
            L2.MouseDown += new MouseEventHandler(FormMove_MouseDown);
            L2.MouseMove += new MouseEventHandler(FormMove_MouseMove);
            L2.MouseUp += new MouseEventHandler(FormMove_MouseUp);
            if (_B != null)
                B.Click += new System.EventHandler(FormClose_Click);
            if (_B1 != null)
                B1.Click += new System.EventHandler(MinimiseForm_Click);
            Formsize = F.Size;
        }

        public static void Setfromsize()
        {
            Formsize = F.Size;
        }

        private static void FormResize_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            Formsize = F.Size;
        }

        private static void FormResize_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown == true && F.WindowState == System.Windows.Forms.FormWindowState.Normal)
            {
                F.Size = new Size(LastSize.Width + e.X, LastSize.Height + e.Y);
                LastSize = F.Size;
            }
            F.Update();
        }

        private static void FormResize_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            LastSize = F.Size;
            LastMousePositon = e.Location;
            Console.WriteLine(F.Size.ToString());
            Console.WriteLine(e.X.ToString() + " , " + e.Y.ToString());
        }

        private static void FormMove_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private static void FormMove_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                if (F.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                    F.WindowState = System.Windows.Forms.FormWindowState.Normal;
                F.Location = new Point(
                    (F.Location.X - lastLocation.X) + e.X, (F.Location.Y - lastLocation.Y) + e.Y);

                F.Update();
            }
        }

        private static void FormMove_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private static void FormMax_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (F.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                {
                    F.WindowState = System.Windows.Forms.FormWindowState.Normal;
                    Formsize = F.Size;
                }
                else if (F.WindowState == System.Windows.Forms.FormWindowState.Normal)
                {
                    Rectangle rect = Screen.FromHandle(F.Handle).WorkingArea;
                    rect.Location = new Point(0, 0);
                    F.MaximumSize = rect.Size;
                    F.WindowState = FormWindowState.Maximized;
                    Formsize = F.Size;
                }
                F.Refresh();
            }
        }

        // Fixed code: sender is now non-nullable, which is the expected behavior for event handlers.
        private static void FormClose_Click(object sender, EventArgs e)
        {
            F.Close();
        }

        // Fixed code: sender is now non-nullable, which is the expected behavior for event handlers.
        private static void MinimiseForm_Click(object sender, EventArgs e)
        {
            F.WindowState = FormWindowState.Minimized;
        }
    }
}