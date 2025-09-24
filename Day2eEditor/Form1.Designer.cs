namespace Day2eEditor
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            TitlePanel = new Panel();
            MinimiseButton = new Button();
            CloseButton = new Button();
            TitleLabel = new Label();
            label1 = new Label();
            ResizePanel = new Panel();
            ShowConsoleCB = new CheckBox();
            slidePanel = new Panel();
            listView1 = new ListView();
            HidePBox = new Label();
            Slidelabel = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            statusStrip1.SuspendLayout();
            TitlePanel.SuspendLayout();
            slidePanel.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.AutoSize = false;
            statusStrip1.BackColor = Color.FromArgb(60, 63, 65);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(1, 666);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1146, 28);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.ForeColor = SystemColors.Control;
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(118, 23);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // TitlePanel
            // 
            TitlePanel.BackColor = Color.Black;
            TitlePanel.Controls.Add(MinimiseButton);
            TitlePanel.Controls.Add(CloseButton);
            TitlePanel.Controls.Add(TitleLabel);
            TitlePanel.Controls.Add(label1);
            TitlePanel.Dock = DockStyle.Top;
            TitlePanel.Location = new Point(1, 1);
            TitlePanel.Name = "TitlePanel";
            TitlePanel.Size = new Size(1146, 28);
            TitlePanel.TabIndex = 4;
            // 
            // MinimiseButton
            // 
            MinimiseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            MinimiseButton.BackColor = Color.Black;
            MinimiseButton.FlatStyle = FlatStyle.Popup;
            MinimiseButton.Font = new Font("Segoe UI", 12F);
            MinimiseButton.ForeColor = Color.DarkRed;
            MinimiseButton.Location = new Point(1065, -3);
            MinimiseButton.Name = "MinimiseButton";
            MinimiseButton.Size = new Size(41, 28);
            MinimiseButton.TabIndex = 7;
            MinimiseButton.Text = "_";
            MinimiseButton.UseVisualStyleBackColor = false;
            // 
            // CloseButton
            // 
            CloseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CloseButton.BackColor = Color.Black;
            CloseButton.FlatStyle = FlatStyle.Popup;
            CloseButton.Font = new Font("Segoe UI", 12F);
            CloseButton.ForeColor = Color.DarkRed;
            CloseButton.Location = new Point(1105, 0);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(41, 28);
            CloseButton.TabIndex = 6;
            CloseButton.Text = "X";
            CloseButton.UseVisualStyleBackColor = false;
            // 
            // TitleLabel
            // 
            TitleLabel.AutoSize = true;
            TitleLabel.ForeColor = Color.FromArgb(75, 110, 175);
            TitleLabel.Location = new Point(5, 6);
            TitleLabel.Name = "TitleLabel";
            TitleLabel.Size = new Size(159, 15);
            TitleLabel.TabIndex = 0;
            TitleLabel.Text = "Day2eEditor by Shawminator";
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.ForeColor = Color.FromArgb(75, 110, 175);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(1146, 28);
            label1.TabIndex = 8;
            label1.Text = "label1";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ResizePanel
            // 
            ResizePanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ResizePanel.BackgroundImage = (Image)resources.GetObject("ResizePanel.BackgroundImage");
            ResizePanel.BackgroundImageLayout = ImageLayout.Stretch;
            ResizePanel.Location = new Point(1121, 668);
            ResizePanel.Name = "ResizePanel";
            ResizePanel.Size = new Size(25, 25);
            ResizePanel.TabIndex = 6;
            // 
            // ShowConsoleCB
            // 
            ShowConsoleCB.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ShowConsoleCB.AutoSize = true;
            ShowConsoleCB.Checked = true;
            ShowConsoleCB.CheckState = CheckState.Checked;
            ShowConsoleCB.ForeColor = SystemColors.Control;
            ShowConsoleCB.Location = new Point(1015, 672);
            ShowConsoleCB.Name = "ShowConsoleCB";
            ShowConsoleCB.RightToLeft = RightToLeft.Yes;
            ShowConsoleCB.Size = new Size(101, 19);
            ShowConsoleCB.TabIndex = 8;
            ShowConsoleCB.Text = "Show Console";
            ShowConsoleCB.UseVisualStyleBackColor = true;
            ShowConsoleCB.CheckedChanged += ShowConsoleCB_CheckedChanged;
            // 
            // slidePanel
            // 
            slidePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            slidePanel.BackColor = SystemColors.ControlDarkDark;
            slidePanel.Controls.Add(listView1);
            slidePanel.Controls.Add(HidePBox);
            slidePanel.Controls.Add(Slidelabel);
            slidePanel.Location = new Point(0, 28);
            slidePanel.Name = "slidePanel";
            slidePanel.Size = new Size(235, 635);
            slidePanel.TabIndex = 12;
            slidePanel.Click += label2_Click;
            // 
            // listView1
            // 
            listView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView1.BackColor = SystemColors.ControlDarkDark;
            listView1.BorderStyle = BorderStyle.None;
            listView1.ForeColor = SystemColors.Control;
            listView1.Location = new Point(31, 29);
            listView1.Name = "listView1";
            listView1.Size = new Size(185, 587);
            listView1.TabIndex = 13;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.MouseClick += listView1_MouseClick;
            // 
            // HidePBox
            // 
            HidePBox.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            HidePBox.ForeColor = SystemColors.Control;
            HidePBox.Location = new Point(31, 6);
            HidePBox.Name = "HidePBox";
            HidePBox.Size = new Size(166, 20);
            HidePBox.TabIndex = 12;
            HidePBox.Text = "<<Hide";
            HidePBox.TextAlign = ContentAlignment.MiddleCenter;
            HidePBox.Click += label2_Click;
            // 
            // Slidelabel
            // 
            Slidelabel.Image = (Image)resources.GetObject("Slidelabel.Image");
            Slidelabel.Location = new Point(5, 11);
            Slidelabel.Name = "Slidelabel";
            Slidelabel.Size = new Size(20, 100);
            Slidelabel.TabIndex = 11;
            Slidelabel.Click += label2_Click;
            // 
            // timer1
            // 
            timer1.Interval = 1;
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(1148, 695);
            Controls.Add(slidePanel);
            Controls.Add(ShowConsoleCB);
            Controls.Add(ResizePanel);
            Controls.Add(TitlePanel);
            Controls.Add(statusStrip1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            IsMdiContainer = true;
            Name = "Form1";
            Padding = new Padding(1);
            Text = "Day2eEditor";
            FormClosed += Form1_FormClosed;
            Load += Form1_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            TitlePanel.ResumeLayout(false);
            TitlePanel.PerformLayout();
            slidePanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private StatusStrip statusStrip1;
        private Panel TitlePanel;
        private Label TitleLabel;
        private Button CloseButton;
        private Button MinimiseButton;
        private Label label1;
        private Panel ResizePanel;
        public ToolStripStatusLabel toolStripStatusLabel1;
        private CheckBox ShowConsoleCB;
        private Panel slidePanel;
        private Label Slidelabel;
        private Label HidePBox;
        private System.Windows.Forms.Timer timer1;
        private ListView listView1;
    }
}
