namespace DayZFileManagerPlugin
{
    partial class DayZFileManagerForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pendingListView = new ListView();
            menuStrip1 = new MenuStrip();
            connectionToolStripMenuItem = new ToolStripMenuItem();
            testConnectionToolStripMenuItem = new ToolStripMenuItem();
            setUpRemoteRootToolStripMenuItem = new ToolStripMenuItem();
            donwloadToolStripMenuItem = new ToolStripMenuItem();
            downloadAllToolStripMenuItem = new ToolStripMenuItem();
            downloadMapOutputToolStripMenuItem = new ToolStripMenuItem();
            downloadAttachmentDumpToolStripMenuItem = new ToolStripMenuItem();
            uploadToolStripMenuItem = new ToolStripMenuItem();
            uploadAllToolStripMenuItem = new ToolStripMenuItem();
            uploadCheckedToolStripMenuItem = new ToolStripMenuItem();
            syncToolStripMenuItem = new ToolStripMenuItem();
            checkForChangesOnServerToolStripMenuItem = new ToolStripMenuItem();
            filesToolStripMenuItem = new ToolStripMenuItem();
            clearFilesFromTrackerToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pendingListView
            // 
            pendingListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pendingListView.BackColor = Color.FromArgb(60, 63, 65);
            pendingListView.ForeColor = SystemColors.Control;
            pendingListView.Location = new Point(0, 27);
            pendingListView.Name = "pendingListView";
            pendingListView.Size = new Size(1038, 606);
            pendingListView.TabIndex = 2;
            pendingListView.UseCompatibleStateImageBehavior = false;
            pendingListView.ItemChecked += pendingListView_ItemChecked;
            pendingListView.SelectedIndexChanged += pendingListView_SelectedIndexChanged;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.FromArgb(40, 40, 40);
            menuStrip1.Items.AddRange(new ToolStripItem[] { connectionToolStripMenuItem, donwloadToolStripMenuItem, uploadToolStripMenuItem, syncToolStripMenuItem, filesToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1038, 24);
            menuStrip1.TabIndex = 8;
            menuStrip1.Text = "menuStrip1";
            // 
            // connectionToolStripMenuItem
            // 
            connectionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { testConnectionToolStripMenuItem, setUpRemoteRootToolStripMenuItem });
            connectionToolStripMenuItem.ForeColor = SystemColors.Control;
            connectionToolStripMenuItem.Name = "connectionToolStripMenuItem";
            connectionToolStripMenuItem.Size = new Size(81, 20);
            connectionToolStripMenuItem.Text = "Connection";
            connectionToolStripMenuItem.Click += connectionToolStripMenuItem_Click;
            // 
            // testConnectionToolStripMenuItem
            // 
            testConnectionToolStripMenuItem.BackColor = Color.FromArgb(40, 40, 40);
            testConnectionToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            testConnectionToolStripMenuItem.ForeColor = SystemColors.Control;
            testConnectionToolStripMenuItem.Name = "testConnectionToolStripMenuItem";
            testConnectionToolStripMenuItem.Size = new Size(180, 22);
            testConnectionToolStripMenuItem.Text = "Test Connection";
            testConnectionToolStripMenuItem.Click += TestConnectionButton_Click;
            // 
            // setUpRemoteRootToolStripMenuItem
            // 
            setUpRemoteRootToolStripMenuItem.BackColor = Color.FromArgb(40, 40, 40);
            setUpRemoteRootToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            setUpRemoteRootToolStripMenuItem.ForeColor = SystemColors.Control;
            setUpRemoteRootToolStripMenuItem.Name = "setUpRemoteRootToolStripMenuItem";
            setUpRemoteRootToolStripMenuItem.Size = new Size(180, 22);
            setUpRemoteRootToolStripMenuItem.Text = "Set up Remote Root";
            setUpRemoteRootToolStripMenuItem.Click += SelectRootDieButton_Click;
            // 
            // donwloadToolStripMenuItem
            // 
            donwloadToolStripMenuItem.BackColor = Color.FromArgb(40, 40, 40);
            donwloadToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { downloadAllToolStripMenuItem, downloadMapOutputToolStripMenuItem, downloadAttachmentDumpToolStripMenuItem });
            donwloadToolStripMenuItem.ForeColor = SystemColors.Control;
            donwloadToolStripMenuItem.Name = "donwloadToolStripMenuItem";
            donwloadToolStripMenuItem.Size = new Size(73, 20);
            donwloadToolStripMenuItem.Text = "Download";
            // 
            // downloadAllToolStripMenuItem
            // 
            downloadAllToolStripMenuItem.BackColor = Color.FromArgb(40, 40, 40);
            downloadAllToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            downloadAllToolStripMenuItem.ForeColor = SystemColors.Control;
            downloadAllToolStripMenuItem.Name = "downloadAllToolStripMenuItem";
            downloadAllToolStripMenuItem.Size = new Size(230, 22);
            downloadAllToolStripMenuItem.Text = "Download All";
            downloadAllToolStripMenuItem.Click += DownloadAllButton_Click;
            // 
            // downloadMapOutputToolStripMenuItem
            // 
            downloadMapOutputToolStripMenuItem.BackColor = Color.FromArgb(40, 40, 40);
            downloadMapOutputToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            downloadMapOutputToolStripMenuItem.ForeColor = SystemColors.Control;
            downloadMapOutputToolStripMenuItem.Name = "downloadMapOutputToolStripMenuItem";
            downloadMapOutputToolStripMenuItem.Size = new Size(230, 22);
            downloadMapOutputToolStripMenuItem.Text = "Download Map Output";
            downloadMapOutputToolStripMenuItem.Click += button1_Click;
            // 
            // downloadAttachmentDumpToolStripMenuItem
            // 
            downloadAttachmentDumpToolStripMenuItem.BackColor = Color.FromArgb(40, 40, 40);
            downloadAttachmentDumpToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            downloadAttachmentDumpToolStripMenuItem.ForeColor = SystemColors.Control;
            downloadAttachmentDumpToolStripMenuItem.Name = "downloadAttachmentDumpToolStripMenuItem";
            downloadAttachmentDumpToolStripMenuItem.Size = new Size(230, 22);
            downloadAttachmentDumpToolStripMenuItem.Text = "Download Attachment Dump";
            downloadAttachmentDumpToolStripMenuItem.Click += button2_Click;
            // 
            // uploadToolStripMenuItem
            // 
            uploadToolStripMenuItem.BackColor = Color.FromArgb(40, 40, 40);
            uploadToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { uploadAllToolStripMenuItem, uploadCheckedToolStripMenuItem });
            uploadToolStripMenuItem.ForeColor = SystemColors.Control;
            uploadToolStripMenuItem.Name = "uploadToolStripMenuItem";
            uploadToolStripMenuItem.Size = new Size(95, 20);
            uploadToolStripMenuItem.Text = "Sync To Server";
            uploadToolStripMenuItem.Click += uploadToolStripMenuItem_Click;
            // 
            // uploadAllToolStripMenuItem
            // 
            uploadAllToolStripMenuItem.BackColor = Color.FromArgb(40, 40, 40);
            uploadAllToolStripMenuItem.ForeColor = SystemColors.Control;
            uploadAllToolStripMenuItem.Name = "uploadAllToolStripMenuItem";
            uploadAllToolStripMenuItem.Size = new Size(180, 22);
            uploadAllToolStripMenuItem.Text = "Upload All";
            uploadAllToolStripMenuItem.Click += uploadAllToolStripMenuItem_Click;
            // 
            // uploadCheckedToolStripMenuItem
            // 
            uploadCheckedToolStripMenuItem.BackColor = Color.FromArgb(40, 40, 40);
            uploadCheckedToolStripMenuItem.ForeColor = SystemColors.Control;
            uploadCheckedToolStripMenuItem.Name = "uploadCheckedToolStripMenuItem";
            uploadCheckedToolStripMenuItem.Size = new Size(180, 22);
            uploadCheckedToolStripMenuItem.Text = "Upload Checked";
            uploadCheckedToolStripMenuItem.Click += uploadCheckedToolStripMenuItem_Click;
            // 
            // syncToolStripMenuItem
            // 
            syncToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { checkForChangesOnServerToolStripMenuItem });
            syncToolStripMenuItem.ForeColor = SystemColors.Control;
            syncToolStripMenuItem.Name = "syncToolStripMenuItem";
            syncToolStripMenuItem.Size = new Size(108, 20);
            syncToolStripMenuItem.Text = "Sync from Server";
            // 
            // checkForChangesOnServerToolStripMenuItem
            // 
            checkForChangesOnServerToolStripMenuItem.BackColor = Color.FromArgb(40, 40, 40);
            checkForChangesOnServerToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            checkForChangesOnServerToolStripMenuItem.ForeColor = SystemColors.Control;
            checkForChangesOnServerToolStripMenuItem.Name = "checkForChangesOnServerToolStripMenuItem";
            checkForChangesOnServerToolStripMenuItem.Size = new Size(226, 22);
            checkForChangesOnServerToolStripMenuItem.Text = "Check for Changes on Server";
            checkForChangesOnServerToolStripMenuItem.Click += checkForChangesOnServerToolStripMenuItem_Click;
            // 
            // filesToolStripMenuItem
            // 
            filesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { clearFilesFromTrackerToolStripMenuItem });
            filesToolStripMenuItem.ForeColor = SystemColors.Control;
            filesToolStripMenuItem.Name = "filesToolStripMenuItem";
            filesToolStripMenuItem.Size = new Size(83, 20);
            filesToolStripMenuItem.Text = "Tracker Files";
            // 
            // clearFilesFromTrackerToolStripMenuItem
            // 
            clearFilesFromTrackerToolStripMenuItem.BackColor = Color.FromArgb(40, 40, 40);
            clearFilesFromTrackerToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            clearFilesFromTrackerToolStripMenuItem.ForeColor = SystemColors.Control;
            clearFilesFromTrackerToolStripMenuItem.Name = "clearFilesFromTrackerToolStripMenuItem";
            clearFilesFromTrackerToolStripMenuItem.Size = new Size(199, 22);
            clearFilesFromTrackerToolStripMenuItem.Text = "Clear Files From Tracker";
            clearFilesFromTrackerToolStripMenuItem.Click += button3_Click;
            // 
            // DayZFileManagerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(1038, 633);
            Controls.Add(pendingListView);
            Controls.Add(menuStrip1);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            MainMenuStrip = menuStrip1;
            Name = "DayZFileManagerForm";
            Text = "Form1";
            Load += DayZFileManagerForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView pendingListView;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem connectionToolStripMenuItem;
        private ToolStripMenuItem testConnectionToolStripMenuItem;
        private ToolStripMenuItem setUpRemoteRootToolStripMenuItem;
        private ToolStripMenuItem donwloadToolStripMenuItem;
        private ToolStripMenuItem downloadAllToolStripMenuItem;
        private ToolStripMenuItem downloadMapOutputToolStripMenuItem;
        private ToolStripMenuItem downloadAttachmentDumpToolStripMenuItem;
        private ToolStripMenuItem uploadToolStripMenuItem;
        private ToolStripMenuItem uploadAllToolStripMenuItem;
        private ToolStripMenuItem uploadCheckedToolStripMenuItem;
        private ToolStripMenuItem syncToolStripMenuItem;
        private ToolStripMenuItem checkForChangesOnServerToolStripMenuItem;
        private ToolStripMenuItem filesToolStripMenuItem;
        private ToolStripMenuItem clearFilesFromTrackerToolStripMenuItem;
    }
}
