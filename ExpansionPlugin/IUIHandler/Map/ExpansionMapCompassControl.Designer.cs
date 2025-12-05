namespace ExpansionPlugin
{
    partial class ExpansionMapCompassControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox18 = new GroupBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            EnableHUDCompassCB = new CheckBox();
            NeedGPSItemForHUDCompassCB = new CheckBox();
            NeedCompassItemForHUDCompassCB = new CheckBox();
            panel1 = new Panel();
            CompassColorPB = new PictureBox();
            darkLabel55 = new Label();
            panel2 = new Panel();
            CompassBadgesColorPB = new PictureBox();
            darkLabel224 = new Label();
            groupBox18.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)CompassColorPB).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)CompassBadgesColorPB).BeginInit();
            SuspendLayout();
            // 
            // groupBox18
            // 
            groupBox18.Controls.Add(flowLayoutPanel1);
            groupBox18.Dock = DockStyle.Fill;
            groupBox18.ForeColor = SystemColors.Control;
            groupBox18.Location = new Point(0, 0);
            groupBox18.Margin = new Padding(4, 3, 4, 3);
            groupBox18.Name = "groupBox18";
            groupBox18.Padding = new Padding(4, 3, 4, 3);
            groupBox18.Size = new Size(273, 145);
            groupBox18.TabIndex = 11;
            groupBox18.TabStop = false;
            groupBox18.Text = "Compass";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(EnableHUDCompassCB);
            flowLayoutPanel1.Controls.Add(NeedGPSItemForHUDCompassCB);
            flowLayoutPanel1.Controls.Add(NeedCompassItemForHUDCompassCB);
            flowLayoutPanel1.Controls.Add(panel1);
            flowLayoutPanel1.Controls.Add(panel2);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(4, 19);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(265, 123);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // EnableHUDCompassCB
            // 
            EnableHUDCompassCB.AutoSize = true;
            EnableHUDCompassCB.ForeColor = SystemColors.Control;
            EnableHUDCompassCB.Location = new Point(4, 3);
            EnableHUDCompassCB.Margin = new Padding(4, 3, 4, 3);
            EnableHUDCompassCB.Name = "EnableHUDCompassCB";
            EnableHUDCompassCB.Size = new Size(141, 19);
            EnableHUDCompassCB.TabIndex = 0;
            EnableHUDCompassCB.Text = "Enable HUD Compass";
            EnableHUDCompassCB.TextAlign = ContentAlignment.MiddleRight;
            EnableHUDCompassCB.UseVisualStyleBackColor = true;
            EnableHUDCompassCB.CheckedChanged += EnableHUDCompassCB_CheckedChanged;
            // 
            // NeedGPSItemForHUDCompassCB
            // 
            NeedGPSItemForHUDCompassCB.AutoSize = true;
            NeedGPSItemForHUDCompassCB.ForeColor = SystemColors.Control;
            NeedGPSItemForHUDCompassCB.Location = new Point(4, 28);
            NeedGPSItemForHUDCompassCB.Margin = new Padding(4, 3, 4, 3);
            NeedGPSItemForHUDCompassCB.Name = "NeedGPSItemForHUDCompassCB";
            NeedGPSItemForHUDCompassCB.Size = new Size(205, 19);
            NeedGPSItemForHUDCompassCB.TabIndex = 2;
            NeedGPSItemForHUDCompassCB.Text = "Need GPS Item For HUD Compass";
            NeedGPSItemForHUDCompassCB.TextAlign = ContentAlignment.MiddleCenter;
            NeedGPSItemForHUDCompassCB.UseVisualStyleBackColor = true;
            NeedGPSItemForHUDCompassCB.CheckedChanged += NeedGPSItemForHUDCompassCB_CheckedChanged;
            // 
            // NeedCompassItemForHUDCompassCB
            // 
            NeedCompassItemForHUDCompassCB.AutoSize = true;
            NeedCompassItemForHUDCompassCB.ForeColor = SystemColors.Control;
            NeedCompassItemForHUDCompassCB.Location = new Point(4, 53);
            NeedCompassItemForHUDCompassCB.Margin = new Padding(4, 3, 4, 3);
            NeedCompassItemForHUDCompassCB.Name = "NeedCompassItemForHUDCompassCB";
            NeedCompassItemForHUDCompassCB.Size = new Size(233, 19);
            NeedCompassItemForHUDCompassCB.TabIndex = 1;
            NeedCompassItemForHUDCompassCB.Text = "Need Compass Item For HUD Compass";
            NeedCompassItemForHUDCompassCB.TextAlign = ContentAlignment.MiddleRight;
            NeedCompassItemForHUDCompassCB.UseVisualStyleBackColor = true;
            NeedCompassItemForHUDCompassCB.CheckedChanged += NeedCompassItemForHUDCompassCB_CheckedChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(CompassColorPB);
            panel1.Controls.Add(darkLabel55);
            panel1.Location = new Point(3, 78);
            panel1.Name = "panel1";
            panel1.Size = new Size(253, 15);
            panel1.TabIndex = 121;
            // 
            // CompassColorPB
            // 
            CompassColorPB.Location = new Point(112, 0);
            CompassColorPB.Margin = new Padding(4, 3, 4, 3);
            CompassColorPB.Name = "CompassColorPB";
            CompassColorPB.Size = new Size(141, 15);
            CompassColorPB.TabIndex = 118;
            CompassColorPB.TabStop = false;
            CompassColorPB.Click += CompassColor_Click;
            // 
            // darkLabel55
            // 
            darkLabel55.AutoSize = true;
            darkLabel55.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel55.Location = new Point(0, 0);
            darkLabel55.Margin = new Padding(4, 0, 4, 0);
            darkLabel55.Name = "darkLabel55";
            darkLabel55.Size = new Size(95, 15);
            darkLabel55.TabIndex = 3;
            darkLabel55.Text = "Compass Colour";
            // 
            // panel2
            // 
            panel2.Controls.Add(CompassBadgesColorPB);
            panel2.Controls.Add(darkLabel224);
            panel2.Location = new Point(3, 99);
            panel2.Name = "panel2";
            panel2.Size = new Size(253, 15);
            panel2.TabIndex = 122;
            // 
            // CompassBadgesColorPB
            // 
            CompassBadgesColorPB.Location = new Point(112, 0);
            CompassBadgesColorPB.Margin = new Padding(4, 3, 4, 3);
            CompassBadgesColorPB.Name = "CompassBadgesColorPB";
            CompassBadgesColorPB.Size = new Size(141, 15);
            CompassBadgesColorPB.TabIndex = 120;
            CompassBadgesColorPB.TabStop = false;
            CompassBadgesColorPB.Click += pictureBox7_Click;
            // 
            // darkLabel224
            // 
            darkLabel224.AutoSize = true;
            darkLabel224.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel224.Location = new Point(0, 0);
            darkLabel224.Margin = new Padding(4, 0, 4, 0);
            darkLabel224.Name = "darkLabel224";
            darkLabel224.Size = new Size(79, 15);
            darkLabel224.TabIndex = 4;
            darkLabel224.Text = "Badge Colour";
            // 
            // ExpansionMapCompassControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox18);
            ForeColor = SystemColors.Control;
            Name = "ExpansionMapCompassControl";
            Size = new Size(273, 145);
            groupBox18.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)CompassColorPB).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)CompassBadgesColorPB).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox18;
        private FlowLayoutPanel flowLayoutPanel1;
        private CheckBox EnableHUDCompassCB;
        private CheckBox NeedGPSItemForHUDCompassCB;
        private CheckBox NeedCompassItemForHUDCompassCB;
        private Panel panel1;
        private PictureBox CompassColorPB;
        private Label darkLabel55;
        private Panel panel2;
        private PictureBox CompassBadgesColorPB;
        private Label darkLabel224;
    }
}
