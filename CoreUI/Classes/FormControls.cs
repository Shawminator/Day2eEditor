using System.Text.Json;

namespace Day2eEditor
{
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
    public class FormController : IDisposable
    {
        private bool disposed;

        private Form F;
        private Button B;
        private Button B1;
        private Panel P;
        private Panel P2;
        private Label L;
        private Label L2;

        private bool mouseDown;
        private Point lastLocation;
        private Size LastSize;
        private Point LastMousePositon;

        public Size Formsize { get; private set; }

        public FormController(Form _F, Panel _P, Panel _P2, Label _L, Label _L2, Button _B, Button _B1)
        {

            F = _F;
            P = _P;
            P2 = _P2;
            B = _B;
            B1 = _B1;
            L = _L;
            L2 = _L2;

            P.MouseDoubleClick += FormMax_MouseDoubleClick;
            P.MouseDown += FormMove_MouseDown;
            P.MouseMove += FormMove_MouseMove;
            P.MouseUp += FormMove_MouseUp;

            if (P2 != null)
            {
                P2.MouseDown += FormResize_MouseDown;
                P2.MouseMove += FormResize_MouseMove;
                P2.MouseUp += FormResize_MouseUp;
            }

            L.MouseDoubleClick += FormMax_MouseDoubleClick;
            L.MouseDown += FormMove_MouseDown;
            L.MouseMove += FormMove_MouseMove;
            L.MouseUp += FormMove_MouseUp;

            L2.MouseDoubleClick += FormMax_MouseDoubleClick;
            L2.MouseDown += FormMove_MouseDown;
            L2.MouseMove += FormMove_MouseMove;
            L2.MouseUp += FormMove_MouseUp;

            if (_B != null)
                B.Click += FormClose_Click;
            if (_B1 != null)
                B1.Click += MinimiseForm_Click;

            Formsize = F.Size;
        }

        public void SetFromSize()
        {
            Formsize = F.Size;
        }

        private void FormResize_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            Formsize = F.Size;
        }

        private void FormResize_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown && F.WindowState == FormWindowState.Normal)
            {
                F.Size = new Size(LastSize.Width + e.X, LastSize.Height + e.Y);
                LastSize = F.Size;
            }
            F.Update();
        }

        private void FormResize_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            LastSize = F.Size;
            LastMousePositon = e.Location;
        }

        private void FormMove_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void FormMove_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                if (F.WindowState == FormWindowState.Maximized)
                    F.WindowState = FormWindowState.Normal;

                F.Location = new Point(
                    (F.Location.X - lastLocation.X) + e.X,
                    (F.Location.Y - lastLocation.Y) + e.Y
                );

                F.Update();
            }
        }

        private void FormMove_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void FormMax_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (F.WindowState == FormWindowState.Maximized)
                {
                    F.WindowState = FormWindowState.Normal;
                    Formsize = F.Size;
                }
                else if (F.WindowState == FormWindowState.Normal)
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

        private void FormClose_Click(object sender, EventArgs e)
        {
            F.Close();
        }

        private void MinimiseForm_Click(object sender, EventArgs e)
        {
            F.WindowState = FormWindowState.Minimized;
        }
        public void Dispose()
        {
            if (disposed) return;

            // Unhook events
            if (P != null)
            {
                P.MouseDoubleClick -= FormMax_MouseDoubleClick;
                P.MouseDown -= FormMove_MouseDown;
                P.MouseMove -= FormMove_MouseMove;
                P.MouseUp -= FormMove_MouseUp;
            }

            if (P2 != null)
            {
                P2.MouseDown -= FormResize_MouseDown;
                P2.MouseMove -= FormResize_MouseMove;
                P2.MouseUp -= FormResize_MouseUp;
            }

            if (L != null)
            {
                L.MouseDoubleClick -= FormMax_MouseDoubleClick;
                L.MouseDown -= FormMove_MouseDown;
                L.MouseMove -= FormMove_MouseMove;
                L.MouseUp -= FormMove_MouseUp;
            }

            if (L2 != null)
            {
                L2.MouseDoubleClick -= FormMax_MouseDoubleClick;
                L2.MouseDown -= FormMove_MouseDown;
                L2.MouseMove -= FormMove_MouseMove;
                L2.MouseUp -= FormMove_MouseUp;
            }

            if (B != null)
                B.Click -= FormClose_Click;

            if (B1 != null)
                B1.Click -= MinimiseForm_Click;

            disposed = true;
        }
    }

}