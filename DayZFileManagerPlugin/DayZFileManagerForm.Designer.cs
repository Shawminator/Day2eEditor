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
            panel1 = new Panel();
            button2 = new Button();
            button1 = new Button();
            DownloadAllButton = new Button();
            SelectRootDieButton = new Button();
            TestConnectionButton = new Button();
            SyncCheckedButton = new Button();
            SyncAllButton = new Button();
            button3 = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pendingListView
            // 
            pendingListView.BackColor = Color.FromArgb(60, 63, 65);
            pendingListView.Dock = DockStyle.Fill;
            pendingListView.ForeColor = SystemColors.Control;
            pendingListView.Location = new Point(0, 31);
            pendingListView.Name = "pendingListView";
            pendingListView.Size = new Size(1038, 602);
            pendingListView.TabIndex = 2;
            pendingListView.UseCompatibleStateImageBehavior = false;
            pendingListView.ItemChecked += pendingListView_ItemChecked;
            pendingListView.SelectedIndexChanged += pendingListView_SelectedIndexChanged;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(60, 63, 65);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(DownloadAllButton);
            panel1.Controls.Add(SelectRootDieButton);
            panel1.Controls.Add(TestConnectionButton);
            panel1.Controls.Add(SyncCheckedButton);
            panel1.Controls.Add(SyncAllButton);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1038, 31);
            panel1.TabIndex = 6;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(60, 63, 65);
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(542, 5);
            button2.Name = "button2";
            button2.Size = new Size(149, 23);
            button2.TabIndex = 23;
            button2.Text = "Download Dump Attach";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(60, 63, 65);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(387, 5);
            button1.Name = "button1";
            button1.Size = new Size(149, 23);
            button1.TabIndex = 22;
            button1.Text = "Download Map_Output";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // DownloadAllButton
            // 
            DownloadAllButton.BackColor = Color.FromArgb(60, 63, 65);
            DownloadAllButton.FlatStyle = FlatStyle.Flat;
            DownloadAllButton.Location = new Point(259, 5);
            DownloadAllButton.Name = "DownloadAllButton";
            DownloadAllButton.Size = new Size(122, 23);
            DownloadAllButton.TabIndex = 21;
            DownloadAllButton.Text = "Download All";
            DownloadAllButton.UseVisualStyleBackColor = false;
            DownloadAllButton.Click += DownloadAllButton_Click;
            // 
            // SelectRootDieButton
            // 
            SelectRootDieButton.BackColor = Color.FromArgb(60, 63, 65);
            SelectRootDieButton.FlatStyle = FlatStyle.Flat;
            SelectRootDieButton.Location = new Point(131, 5);
            SelectRootDieButton.Name = "SelectRootDieButton";
            SelectRootDieButton.Size = new Size(122, 23);
            SelectRootDieButton.TabIndex = 20;
            SelectRootDieButton.Text = "Set Root Directory";
            SelectRootDieButton.UseVisualStyleBackColor = false;
            SelectRootDieButton.Click += SelectRootDieButton_Click;
            // 
            // TestConnectionButton
            // 
            TestConnectionButton.BackColor = Color.FromArgb(60, 63, 65);
            TestConnectionButton.FlatStyle = FlatStyle.Flat;
            TestConnectionButton.Location = new Point(3, 5);
            TestConnectionButton.Name = "TestConnectionButton";
            TestConnectionButton.Size = new Size(122, 23);
            TestConnectionButton.TabIndex = 19;
            TestConnectionButton.Text = "Test Connection";
            TestConnectionButton.UseVisualStyleBackColor = false;
            TestConnectionButton.Click += TestConnectionButton_Click;
            // 
            // SyncCheckedButton
            // 
            SyncCheckedButton.BackColor = Color.FromArgb(60, 63, 65);
            SyncCheckedButton.FlatStyle = FlatStyle.Flat;
            SyncCheckedButton.Location = new Point(776, 5);
            SyncCheckedButton.Name = "SyncCheckedButton";
            SyncCheckedButton.Size = new Size(112, 23);
            SyncCheckedButton.TabIndex = 17;
            SyncCheckedButton.Text = "Sync Checked";
            SyncCheckedButton.UseVisualStyleBackColor = false;
            SyncCheckedButton.Click += SyncCheckedButton_Click;
            // 
            // SyncAllButton
            // 
            SyncAllButton.BackColor = Color.FromArgb(60, 63, 65);
            SyncAllButton.FlatStyle = FlatStyle.Flat;
            SyncAllButton.Location = new Point(697, 5);
            SyncAllButton.Name = "SyncAllButton";
            SyncAllButton.Size = new Size(73, 23);
            SyncAllButton.TabIndex = 16;
            SyncAllButton.Text = "Sync All";
            SyncAllButton.UseVisualStyleBackColor = false;
            SyncAllButton.Click += SyncAllButton_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(60, 63, 65);
            button3.FlatStyle = FlatStyle.Flat;
            button3.Location = new Point(894, 5);
            button3.Name = "button3";
            button3.Size = new Size(112, 23);
            button3.TabIndex = 24;
            button3.Text = "Clear All";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // DayZFileManagerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(1038, 633);
            Controls.Add(pendingListView);
            Controls.Add(panel1);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            Name = "DayZFileManagerForm";
            Text = "Form1";
            Load += DayZFileManagerForm_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListView pendingListView;
        private Panel panel1;
        private Button SyncCheckedButton;
        private Button SyncAllButton;
        private Button SelectRootDieButton;
        private Button TestConnectionButton;
        private Button DownloadAllButton;
        private Button button2;
        private Button button1;
        private Button button3;
    }
}
