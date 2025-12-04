namespace DayZeEditor
{
    partial class Form4
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            groupBox20 = new GroupBox();
            EnableServerMarkersCB = new CheckBox();
            ShowDistanceOnServerMarkersCB = new CheckBox();
            ShowNameOnServerMarkersCB = new CheckBox();
            groupBox18 = new GroupBox();
            pictureBox7 = new PictureBox();
            darkLabel224 = new Label();
            CompassColor = new PictureBox();
            darkLabel55 = new Label();
            NeedGPSItemForHUDCompassCB = new CheckBox();
            EnableHUDCompassCB = new CheckBox();
            NeedCompassItemForHUDCompassCB = new CheckBox();
            groupBox17 = new GroupBox();
            m_PersistCB = new CheckBox();
            m_LockedCB = new CheckBox();
            m_Color = new PictureBox();
            darkLabel54 = new Label();
            m_Is3DCB = new CheckBox();
            darkLabel53 = new Label();
            comboBox3 = new ComboBox();
            textBox11 = new TextBox();
            darkLabel52 = new Label();
            darkLabel51 = new Label();
            comboBox1 = new ComboBox();
            textBox9 = new TextBox();
            darkLabel50 = new Label();
            darkLabel47 = new Label();
            darkLabel48 = new Label();
            darkLabel49 = new Label();
            numericUpDown24 = new NumericUpDown();
            numericUpDown25 = new NumericUpDown();
            numericUpDown26 = new NumericUpDown();
            groupBox20.SuspendLayout();
            groupBox18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CompassColor).BeginInit();
            groupBox17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)m_Color).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown24).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown25).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown26).BeginInit();
            SuspendLayout();
            // 
            // groupBox20
            // 
            groupBox20.Controls.Add(EnableServerMarkersCB);
            groupBox20.Controls.Add(ShowDistanceOnServerMarkersCB);
            groupBox20.Controls.Add(ShowNameOnServerMarkersCB);
            groupBox20.ForeColor = SystemColors.Control;
            groupBox20.Location = new Point(569, 16);
            groupBox20.Margin = new Padding(4, 3, 4, 3);
            groupBox20.Name = "groupBox20";
            groupBox20.Padding = new Padding(4, 3, 4, 3);
            groupBox20.Size = new Size(260, 90);
            groupBox20.TabIndex = 6;
            groupBox20.TabStop = false;
            groupBox20.Text = "Server Markers";
            // 
            // EnableServerMarkersCB
            // 
            EnableServerMarkersCB.AutoSize = true;
            EnableServerMarkersCB.ForeColor = SystemColors.Control;
            EnableServerMarkersCB.Location = new Point(12, 18);
            EnableServerMarkersCB.Margin = new Padding(4, 3, 4, 3);
            EnableServerMarkersCB.Name = "EnableServerMarkersCB";
            EnableServerMarkersCB.Size = new Size(141, 19);
            EnableServerMarkersCB.TabIndex = 0;
            EnableServerMarkersCB.Text = "Enable Server Markers";
            EnableServerMarkersCB.TextAlign = ContentAlignment.MiddleCenter;
            EnableServerMarkersCB.UseVisualStyleBackColor = true;
            // 
            // ShowDistanceOnServerMarkersCB
            // 
            ShowDistanceOnServerMarkersCB.AutoSize = true;
            ShowDistanceOnServerMarkersCB.ForeColor = SystemColors.Control;
            ShowDistanceOnServerMarkersCB.Location = new Point(12, 65);
            ShowDistanceOnServerMarkersCB.Margin = new Padding(4, 3, 4, 3);
            ShowDistanceOnServerMarkersCB.Name = "ShowDistanceOnServerMarkersCB";
            ShowDistanceOnServerMarkersCB.Size = new Size(202, 19);
            ShowDistanceOnServerMarkersCB.TabIndex = 2;
            ShowDistanceOnServerMarkersCB.Text = "Show Distance On Server Markers";
            ShowDistanceOnServerMarkersCB.TextAlign = ContentAlignment.MiddleRight;
            ShowDistanceOnServerMarkersCB.UseVisualStyleBackColor = true;
            // 
            // ShowNameOnServerMarkersCB
            // 
            ShowNameOnServerMarkersCB.AutoSize = true;
            ShowNameOnServerMarkersCB.ForeColor = SystemColors.Control;
            ShowNameOnServerMarkersCB.Location = new Point(12, 42);
            ShowNameOnServerMarkersCB.Margin = new Padding(4, 3, 4, 3);
            ShowNameOnServerMarkersCB.Name = "ShowNameOnServerMarkersCB";
            ShowNameOnServerMarkersCB.Size = new Size(189, 19);
            ShowNameOnServerMarkersCB.TabIndex = 1;
            ShowNameOnServerMarkersCB.Text = "Show Name On Server Markers";
            ShowNameOnServerMarkersCB.TextAlign = ContentAlignment.MiddleRight;
            ShowNameOnServerMarkersCB.UseVisualStyleBackColor = true;
            // 
            // groupBox18
            // 
            groupBox18.Controls.Add(pictureBox7);
            groupBox18.Controls.Add(darkLabel224);
            groupBox18.Controls.Add(CompassColor);
            groupBox18.Controls.Add(darkLabel55);
            groupBox18.Controls.Add(NeedGPSItemForHUDCompassCB);
            groupBox18.Controls.Add(EnableHUDCompassCB);
            groupBox18.Controls.Add(NeedCompassItemForHUDCompassCB);
            groupBox18.ForeColor = SystemColors.Control;
            groupBox18.Location = new Point(535, 337);
            groupBox18.Margin = new Padding(4, 3, 4, 3);
            groupBox18.Name = "groupBox18";
            groupBox18.Padding = new Padding(4, 3, 4, 3);
            groupBox18.Size = new Size(260, 156);
            groupBox18.TabIndex = 10;
            groupBox18.TabStop = false;
            groupBox18.Text = "Compass";
            // 
            // pictureBox7
            // 
            pictureBox7.BackgroundImage = (Image)resources.GetObject("pictureBox7.BackgroundImage");
            pictureBox7.Location = new Point(112, 118);
            pictureBox7.Margin = new Padding(4, 3, 4, 3);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(141, 15);
            pictureBox7.TabIndex = 120;
            pictureBox7.TabStop = false;
            // 
            // darkLabel224
            // 
            darkLabel224.AutoSize = true;
            darkLabel224.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel224.Location = new Point(8, 118);
            darkLabel224.Margin = new Padding(4, 0, 4, 0);
            darkLabel224.Name = "darkLabel224";
            darkLabel224.Size = new Size(79, 15);
            darkLabel224.TabIndex = 4;
            darkLabel224.Text = "Badge Colour";
            // 
            // CompassColor
            // 
            CompassColor.BackgroundImage = (Image)resources.GetObject("CompassColor.BackgroundImage");
            CompassColor.Location = new Point(112, 96);
            CompassColor.Margin = new Padding(4, 3, 4, 3);
            CompassColor.Name = "CompassColor";
            CompassColor.Size = new Size(141, 15);
            CompassColor.TabIndex = 118;
            CompassColor.TabStop = false;
            // 
            // darkLabel55
            // 
            darkLabel55.AutoSize = true;
            darkLabel55.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel55.Location = new Point(8, 96);
            darkLabel55.Margin = new Padding(4, 0, 4, 0);
            darkLabel55.Name = "darkLabel55";
            darkLabel55.Size = new Size(95, 15);
            darkLabel55.TabIndex = 3;
            darkLabel55.Text = "Compass Colour";
            // 
            // NeedGPSItemForHUDCompassCB
            // 
            NeedGPSItemForHUDCompassCB.AutoSize = true;
            NeedGPSItemForHUDCompassCB.ForeColor = SystemColors.Control;
            NeedGPSItemForHUDCompassCB.Location = new Point(12, 69);
            NeedGPSItemForHUDCompassCB.Margin = new Padding(4, 3, 4, 3);
            NeedGPSItemForHUDCompassCB.Name = "NeedGPSItemForHUDCompassCB";
            NeedGPSItemForHUDCompassCB.Size = new Size(205, 19);
            NeedGPSItemForHUDCompassCB.TabIndex = 2;
            NeedGPSItemForHUDCompassCB.Text = "Need GPS Item For HUD Compass";
            NeedGPSItemForHUDCompassCB.TextAlign = ContentAlignment.MiddleCenter;
            NeedGPSItemForHUDCompassCB.UseVisualStyleBackColor = true;
            // 
            // EnableHUDCompassCB
            // 
            EnableHUDCompassCB.AutoSize = true;
            EnableHUDCompassCB.ForeColor = SystemColors.Control;
            EnableHUDCompassCB.Location = new Point(12, 16);
            EnableHUDCompassCB.Margin = new Padding(4, 3, 4, 3);
            EnableHUDCompassCB.Name = "EnableHUDCompassCB";
            EnableHUDCompassCB.Size = new Size(141, 19);
            EnableHUDCompassCB.TabIndex = 0;
            EnableHUDCompassCB.Text = "Enable HUD Compass";
            EnableHUDCompassCB.TextAlign = ContentAlignment.MiddleRight;
            EnableHUDCompassCB.UseVisualStyleBackColor = true;
            // 
            // NeedCompassItemForHUDCompassCB
            // 
            NeedCompassItemForHUDCompassCB.AutoSize = true;
            NeedCompassItemForHUDCompassCB.ForeColor = SystemColors.Control;
            NeedCompassItemForHUDCompassCB.Location = new Point(12, 43);
            NeedCompassItemForHUDCompassCB.Margin = new Padding(4, 3, 4, 3);
            NeedCompassItemForHUDCompassCB.Name = "NeedCompassItemForHUDCompassCB";
            NeedCompassItemForHUDCompassCB.Size = new Size(233, 19);
            NeedCompassItemForHUDCompassCB.TabIndex = 1;
            NeedCompassItemForHUDCompassCB.Text = "Need Compass Item For HUD Compass";
            NeedCompassItemForHUDCompassCB.TextAlign = ContentAlignment.MiddleRight;
            NeedCompassItemForHUDCompassCB.UseVisualStyleBackColor = true;
            // 
            // groupBox17
            // 
            groupBox17.Controls.Add(m_PersistCB);
            groupBox17.Controls.Add(m_LockedCB);
            groupBox17.Controls.Add(m_Color);
            groupBox17.Controls.Add(darkLabel54);
            groupBox17.Controls.Add(m_Is3DCB);
            groupBox17.Controls.Add(darkLabel53);
            groupBox17.Controls.Add(comboBox3);
            groupBox17.Controls.Add(textBox11);
            groupBox17.Controls.Add(darkLabel52);
            groupBox17.Controls.Add(darkLabel51);
            groupBox17.Controls.Add(comboBox1);
            groupBox17.Controls.Add(textBox9);
            groupBox17.Controls.Add(darkLabel50);
            groupBox17.Controls.Add(darkLabel47);
            groupBox17.Controls.Add(darkLabel48);
            groupBox17.Controls.Add(darkLabel49);
            groupBox17.Controls.Add(numericUpDown24);
            groupBox17.Controls.Add(numericUpDown25);
            groupBox17.Controls.Add(numericUpDown26);
            groupBox17.ForeColor = SystemColors.Control;
            groupBox17.Location = new Point(281, 16);
            groupBox17.Margin = new Padding(4, 3, 4, 3);
            groupBox17.Name = "groupBox17";
            groupBox17.Padding = new Padding(4, 3, 4, 3);
            groupBox17.Size = new Size(280, 256);
            groupBox17.TabIndex = 9;
            groupBox17.TabStop = false;
            groupBox17.Text = "Marker Info";
            // 
            // m_PersistCB
            // 
            m_PersistCB.AutoSize = true;
            m_PersistCB.ForeColor = SystemColors.Control;
            m_PersistCB.Location = new Point(61, 20);
            m_PersistCB.Margin = new Padding(4, 3, 4, 3);
            m_PersistCB.Name = "m_PersistCB";
            m_PersistCB.Size = new Size(60, 19);
            m_PersistCB.TabIndex = 0;
            m_PersistCB.Text = "Persist";
            m_PersistCB.TextAlign = ContentAlignment.MiddleRight;
            m_PersistCB.UseVisualStyleBackColor = true;
            // 
            // m_LockedCB
            // 
            m_LockedCB.AutoSize = true;
            m_LockedCB.ForeColor = SystemColors.Control;
            m_LockedCB.Location = new Point(134, 20);
            m_LockedCB.Margin = new Padding(4, 3, 4, 3);
            m_LockedCB.Name = "m_LockedCB";
            m_LockedCB.Size = new Size(64, 19);
            m_LockedCB.TabIndex = 1;
            m_LockedCB.Text = "Locked";
            m_LockedCB.TextAlign = ContentAlignment.MiddleRight;
            m_LockedCB.UseVisualStyleBackColor = true;
            // 
            // m_Color
            // 
            m_Color.BackgroundImage = (Image)resources.GetObject("m_Color.BackgroundImage");
            m_Color.Location = new Point(82, 188);
            m_Color.Margin = new Padding(4, 3, 4, 3);
            m_Color.Name = "m_Color";
            m_Color.Size = new Size(188, 15);
            m_Color.TabIndex = 116;
            m_Color.TabStop = false;
            // 
            // darkLabel54
            // 
            darkLabel54.AutoSize = true;
            darkLabel54.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel54.Location = new Point(13, 188);
            darkLabel54.Margin = new Padding(4, 0, 4, 0);
            darkLabel54.Name = "darkLabel54";
            darkLabel54.Size = new Size(52, 15);
            darkLabel54.TabIndex = 11;
            darkLabel54.Text = "m_Color";
            // 
            // m_Is3DCB
            // 
            m_Is3DCB.AutoSize = true;
            m_Is3DCB.ForeColor = SystemColors.Control;
            m_Is3DCB.Location = new Point(214, 20);
            m_Is3DCB.Margin = new Padding(4, 3, 4, 3);
            m_Is3DCB.Name = "m_Is3DCB";
            m_Is3DCB.Size = new Size(51, 19);
            m_Is3DCB.TabIndex = 2;
            m_Is3DCB.Text = "Is 3D";
            m_Is3DCB.TextAlign = ContentAlignment.MiddleRight;
            m_Is3DCB.UseVisualStyleBackColor = true;
            // 
            // darkLabel53
            // 
            darkLabel53.AutoSize = true;
            darkLabel53.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel53.Location = new Point(13, 155);
            darkLabel53.Margin = new Padding(4, 0, 4, 0);
            darkLabel53.Name = "darkLabel53";
            darkLabel53.Size = new Size(30, 15);
            darkLabel53.TabIndex = 9;
            darkLabel53.Text = "Icon";
            // 
            // comboBox3
            // 
            comboBox3.BackColor = Color.FromArgb(60, 63, 65);
            comboBox3.ForeColor = SystemColors.Control;
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(82, 151);
            comboBox3.Margin = new Padding(4, 3, 4, 3);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(187, 23);
            comboBox3.TabIndex = 10;
            // 
            // textBox11
            // 
            textBox11.BackColor = Color.FromArgb(60, 63, 65);
            textBox11.ForeColor = SystemColors.Control;
            textBox11.Location = new Point(7, 90);
            textBox11.Margin = new Padding(4, 3, 4, 3);
            textBox11.Name = "textBox11";
            textBox11.Size = new Size(262, 23);
            textBox11.TabIndex = 6;
            // 
            // darkLabel52
            // 
            darkLabel52.AutoSize = true;
            darkLabel52.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel52.Location = new Point(7, 72);
            darkLabel52.Margin = new Padding(4, 0, 4, 0);
            darkLabel52.Name = "darkLabel52";
            darkLabel52.Size = new Size(45, 15);
            darkLabel52.TabIndex = 5;
            darkLabel52.Text = "Display";
            // 
            // darkLabel51
            // 
            darkLabel51.AutoSize = true;
            darkLabel51.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel51.Location = new Point(13, 123);
            darkLabel51.Margin = new Padding(4, 0, 4, 0);
            darkLabel51.Name = "darkLabel51";
            darkLabel51.Size = new Size(51, 15);
            darkLabel51.TabIndex = 7;
            darkLabel51.Text = "Visibility";
            // 
            // comboBox1
            // 
            comboBox1.BackColor = Color.FromArgb(60, 63, 65);
            comboBox1.ForeColor = SystemColors.Control;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(82, 120);
            comboBox1.Margin = new Padding(4, 3, 4, 3);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(187, 23);
            comboBox1.TabIndex = 8;
            // 
            // textBox9
            // 
            textBox9.BackColor = Color.FromArgb(60, 63, 65);
            textBox9.ForeColor = SystemColors.Control;
            textBox9.Location = new Point(7, 47);
            textBox9.Margin = new Padding(4, 3, 4, 3);
            textBox9.Name = "textBox9";
            textBox9.Size = new Size(262, 23);
            textBox9.TabIndex = 4;
            // 
            // darkLabel50
            // 
            darkLabel50.AutoSize = true;
            darkLabel50.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel50.Location = new Point(7, 29);
            darkLabel50.Margin = new Padding(4, 0, 4, 0);
            darkLabel50.Name = "darkLabel50";
            darkLabel50.Size = new Size(39, 15);
            darkLabel50.TabIndex = 3;
            darkLabel50.Text = "Name";
            // 
            // darkLabel47
            // 
            darkLabel47.AutoSize = true;
            darkLabel47.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel47.Location = new Point(194, 209);
            darkLabel47.Margin = new Padding(4, 0, 4, 0);
            darkLabel47.Name = "darkLabel47";
            darkLabel47.Size = new Size(57, 15);
            darkLabel47.TabIndex = 14;
            darkLabel47.Text = "Center Z-";
            // 
            // darkLabel48
            // 
            darkLabel48.AutoSize = true;
            darkLabel48.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel48.Location = new Point(108, 209);
            darkLabel48.Margin = new Padding(4, 0, 4, 0);
            darkLabel48.Name = "darkLabel48";
            darkLabel48.Size = new Size(60, 15);
            darkLabel48.TabIndex = 13;
            darkLabel48.Text = "Center Y -";
            // 
            // darkLabel49
            // 
            darkLabel49.AutoSize = true;
            darkLabel49.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel49.Location = new Point(13, 209);
            darkLabel49.Margin = new Padding(4, 0, 4, 0);
            darkLabel49.Name = "darkLabel49";
            darkLabel49.Size = new Size(60, 15);
            darkLabel49.TabIndex = 12;
            darkLabel49.Text = "Center X -";
            // 
            // numericUpDown24
            // 
            numericUpDown24.BackColor = Color.FromArgb(60, 63, 65);
            numericUpDown24.DecimalPlaces = 3;
            numericUpDown24.ForeColor = SystemColors.Control;
            numericUpDown24.Location = new Point(7, 227);
            numericUpDown24.Margin = new Padding(4, 3, 4, 3);
            numericUpDown24.Maximum = new decimal(new int[] { 50000, 0, 0, 0 });
            numericUpDown24.Minimum = new decimal(new int[] { 50000, 0, 0, int.MinValue });
            numericUpDown24.Name = "numericUpDown24";
            numericUpDown24.Size = new Size(83, 23);
            numericUpDown24.TabIndex = 15;
            numericUpDown24.TextAlign = HorizontalAlignment.Center;
            // 
            // numericUpDown25
            // 
            numericUpDown25.BackColor = Color.FromArgb(60, 63, 65);
            numericUpDown25.DecimalPlaces = 3;
            numericUpDown25.ForeColor = SystemColors.Control;
            numericUpDown25.Location = new Point(97, 227);
            numericUpDown25.Margin = new Padding(4, 3, 4, 3);
            numericUpDown25.Maximum = new decimal(new int[] { 50000, 0, 0, 0 });
            numericUpDown25.Minimum = new decimal(new int[] { 500000, 0, 0, int.MinValue });
            numericUpDown25.Name = "numericUpDown25";
            numericUpDown25.Size = new Size(83, 23);
            numericUpDown25.TabIndex = 16;
            numericUpDown25.TextAlign = HorizontalAlignment.Center;
            // 
            // numericUpDown26
            // 
            numericUpDown26.BackColor = Color.FromArgb(60, 63, 65);
            numericUpDown26.DecimalPlaces = 3;
            numericUpDown26.ForeColor = SystemColors.Control;
            numericUpDown26.Location = new Point(187, 227);
            numericUpDown26.Margin = new Padding(4, 3, 4, 3);
            numericUpDown26.Maximum = new decimal(new int[] { 50000, 0, 0, 0 });
            numericUpDown26.Minimum = new decimal(new int[] { 50000, 0, 0, int.MinValue });
            numericUpDown26.Name = "numericUpDown26";
            numericUpDown26.Size = new Size(83, 23);
            numericUpDown26.TabIndex = 17;
            numericUpDown26.TextAlign = HorizontalAlignment.Center;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(1195, 770);
            Controls.Add(groupBox20);
            Controls.Add(groupBox18);
            Controls.Add(groupBox17);
            ForeColor = SystemColors.Control;
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form4";
            Text = "Form4";
            groupBox20.ResumeLayout(false);
            groupBox20.PerformLayout();
            groupBox18.ResumeLayout(false);
            groupBox18.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)CompassColor).EndInit();
            groupBox17.ResumeLayout(false);
            groupBox17.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)m_Color).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown24).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown25).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown26).EndInit();
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox20;
        private System.Windows.Forms.CheckBox EnableServerMarkersCB;
        private System.Windows.Forms.CheckBox ShowDistanceOnServerMarkersCB;
        private System.Windows.Forms.CheckBox ShowNameOnServerMarkersCB;
        private System.Windows.Forms.GroupBox groupBox18;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Label darkLabel224;
        private System.Windows.Forms.PictureBox CompassColor;
        private System.Windows.Forms.Label darkLabel55;
        private System.Windows.Forms.CheckBox NeedGPSItemForHUDCompassCB;
        private System.Windows.Forms.CheckBox EnableHUDCompassCB;
        private System.Windows.Forms.CheckBox NeedCompassItemForHUDCompassCB;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.CheckBox m_PersistCB;
        private System.Windows.Forms.CheckBox m_LockedCB;
        private System.Windows.Forms.PictureBox m_Color;
        private System.Windows.Forms.Label darkLabel54;
        private System.Windows.Forms.CheckBox m_Is3DCB;
        private System.Windows.Forms.Label darkLabel53;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.Label darkLabel52;
        private System.Windows.Forms.Label darkLabel51;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label darkLabel50;
        private System.Windows.Forms.Label darkLabel47;
        private System.Windows.Forms.Label darkLabel48;
        private System.Windows.Forms.Label darkLabel49;
        private System.Windows.Forms.NumericUpDown numericUpDown24;
        private System.Windows.Forms.NumericUpDown numericUpDown25;
        private System.Windows.Forms.NumericUpDown numericUpDown26;
    }
}