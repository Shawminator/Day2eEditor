using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace Day2eEditor
{
    public partial class NewProjectFTP : Form
    {
        private FormController controller;
        public bool isSFTP = true;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SetTitle
        {
            set
            {
                TitleLabel.Text = value;
            }
        }
        public string CurrentRemoteDirectory { get; private set; }
        public string RemoteRoot { get; private set; }
        public string ProfileDirecrtory { get; private set; }
        public string MpMissionDirectory { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Isconsole { get; set; }

        public NewProjectFTP()
        {
            InitializeComponent();
            controller = new FormController(
                this,
                panel1,
                null,
                TitleLabel,
                null,
                CloseButton,
                null
            );
            this.Disposed += (s, e) => controller.Dispose();
        }

        private void NewProjectFTP_Load(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            var ftp = AppServices.GetRequired<FileTransferManager>();

            var items = ftp.ListDirectory(AppServices.GetRequired<ProjectManager>().CurrentProject.ServerSettings,"/");

            foreach (var item in items.OrderByDescending(x => x.IsDirectory))
            {
                ListViewItem lv = new ListViewItem(
                    item.Name,
                    item.IsDirectory ? 0 : 1);

                lv.Tag = item;

                listView1.Items.Add(lv);
            }

            listView1.AutoResizeColumns(
                ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = listView1.HitTest(e.X, e.Y);
            ListViewItem item = info.Item;

            if (item == null)
                return;

            if (item.Tag is not RemoteItem remoteItem)
                return;

            // Ignore files
            if (!remoteItem.IsDirectory)
                return;

            try
            {
                CurrentRemoteDirectory = remoteItem.FullPath;

                var ftp = AppServices.GetRequired<FileTransferManager>();

                listView1.Items.Clear();

                var items = ftp.ListDirectory(
                    AppServices.GetRequired<ProjectManager>().CurrentProject.ServerSettings,
                    CurrentRemoteDirectory);

                // Add "go up" entry if not at root
                if (!IsRoot(CurrentRemoteDirectory))
                {
                    items.Insert(0, new RemoteItem
                    {
                        Name = "..",
                        FullPath = GetParentPath(CurrentRemoteDirectory),
                        IsDirectory = true
                    });
                }

                // Directories first
                foreach (var file in items.Where(x => x.IsDirectory))
                {
                    var lv = new ListViewItem(file.Name, 0);

                    lv.Tag = file;

                    listView1.Items.Add(lv);
                }

                // Files second
                foreach (var file in items.Where(x => !x.IsDirectory))
                {
                    var lv = new ListViewItem(file.Name, 1);

                    lv.SubItems.Add(file.Size.ToString());
                    lv.SubItems.Add(file.Modified.ToString());

                    lv.Tag = file;

                    listView1.Items.Add(lv);
                }

                listView1.AutoResizeColumns(
                    ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Remote Browser",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private string GetParentPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return "/";

            path = path.TrimEnd('/');

            int lastSlash = path.LastIndexOf('/');

            if (lastSlash <= 0)
                return "/";

            return path.Substring(0, lastSlash);
        }
        private bool IsRoot(string path)
        {
            return string.IsNullOrWhiteSpace(path)
                || path == "/";

        }
        private void darkButton2_Click(object sender, EventArgs e)
        {
            darkTextBox2.Text = CurrentRemoteDirectory;
        }
        private void darkTextBox2_TextChanged(object sender, EventArgs e)
        {
            MpMissionDirectory = darkTextBox2.Text;
        }
    }
}
