namespace ExpansionPlugin
{
    partial class ExpansionAILoadoutsControl
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
            LoadOutGB = new GroupBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            groupBox7 = new GroupBox();
            textBox1 = new TextBox();
            darkButton11 = new Button();
            darkLabel40 = new Label();
            darkLabel43 = new Label();
            numericUpDown1 = new NumericUpDown();
            groupBox2 = new GroupBox();
            numericUpDown2 = new NumericUpDown();
            darkLabel41 = new Label();
            darkLabel42 = new Label();
            numericUpDown3 = new NumericUpDown();
            HealthGB = new GroupBox();
            groupBox1 = new GroupBox();
            darkLabel45 = new Label();
            textBox2 = new TextBox();
            darkLabel46 = new Label();
            darkLabel44 = new Label();
            numericUpDown5 = new NumericUpDown();
            numericUpDown4 = new NumericUpDown();
            darkButton8 = new Button();
            darkButton9 = new Button();
            listBox1 = new ListBox();
            LoadOutGB.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            HealthGB.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            SuspendLayout();
            // 
            // LoadOutGB
            // 
            LoadOutGB.Controls.Add(flowLayoutPanel1);
            LoadOutGB.ForeColor = SystemColors.Control;
            LoadOutGB.Location = new Point(0, 0);
            LoadOutGB.Margin = new Padding(4, 3, 4, 3);
            LoadOutGB.Name = "LoadOutGB";
            LoadOutGB.Padding = new Padding(4, 3, 4, 3);
            LoadOutGB.Size = new Size(324, 526);
            LoadOutGB.TabIndex = 216;
            LoadOutGB.TabStop = false;
            LoadOutGB.Text = "Loadout item";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(groupBox7);
            flowLayoutPanel1.Controls.Add(groupBox2);
            flowLayoutPanel1.Controls.Add(HealthGB);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(4, 19);
            flowLayoutPanel1.Margin = new Padding(4, 3, 4, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(316, 504);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(textBox1);
            groupBox7.Controls.Add(darkButton11);
            groupBox7.Controls.Add(darkLabel40);
            groupBox7.Controls.Add(darkLabel43);
            groupBox7.Controls.Add(numericUpDown1);
            groupBox7.ForeColor = SystemColors.Control;
            groupBox7.Location = new Point(4, 3);
            groupBox7.Margin = new Padding(4, 3, 4, 3);
            groupBox7.Name = "groupBox7";
            groupBox7.Padding = new Padding(4, 3, 4, 3);
            groupBox7.Size = new Size(310, 90);
            groupBox7.TabIndex = 208;
            groupBox7.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(60, 63, 65);
            textBox1.ForeColor = SystemColors.Control;
            textBox1.Location = new Point(102, 22);
            textBox1.Margin = new Padding(4, 3, 4, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(167, 23);
            textBox1.TabIndex = 180;
            // 
            // darkButton11
            // 
            darkButton11.FlatStyle = FlatStyle.Flat;
            darkButton11.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            darkButton11.Location = new Point(276, 21);
            darkButton11.Margin = new Padding(4, 3, 4, 3);
            darkButton11.Name = "darkButton11";
            darkButton11.Size = new Size(23, 23);
            darkButton11.TabIndex = 179;
            darkButton11.Text = "+";
            // 
            // darkLabel40
            // 
            darkLabel40.AutoSize = true;
            darkLabel40.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel40.Location = new Point(15, 55);
            darkLabel40.Margin = new Padding(4, 0, 4, 0);
            darkLabel40.Name = "darkLabel40";
            darkLabel40.Size = new Size(47, 15);
            darkLabel40.TabIndex = 187;
            darkLabel40.Text = "Chance";
            // 
            // darkLabel43
            // 
            darkLabel43.AutoSize = true;
            darkLabel43.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel43.Location = new Point(15, 27);
            darkLabel43.Margin = new Padding(4, 0, 4, 0);
            darkLabel43.Name = "darkLabel43";
            darkLabel43.Size = new Size(66, 15);
            darkLabel43.TabIndex = 181;
            darkLabel43.Text = "ClassName";
            // 
            // numericUpDown1
            // 
            numericUpDown1.BackColor = Color.FromArgb(60, 63, 65);
            numericUpDown1.DecimalPlaces = 3;
            numericUpDown1.ForeColor = SystemColors.Control;
            numericUpDown1.Location = new Point(102, 52);
            numericUpDown1.Margin = new Padding(4, 3, 4, 3);
            numericUpDown1.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(122, 23);
            numericUpDown1.TabIndex = 186;
            numericUpDown1.TextAlign = HorizontalAlignment.Center;
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(numericUpDown2);
            groupBox2.Controls.Add(darkLabel41);
            groupBox2.Controls.Add(darkLabel42);
            groupBox2.Controls.Add(numericUpDown3);
            groupBox2.ForeColor = SystemColors.Control;
            groupBox2.Location = new Point(4, 99);
            groupBox2.Margin = new Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 3, 4, 3);
            groupBox2.Size = new Size(310, 90);
            groupBox2.TabIndex = 198;
            groupBox2.TabStop = false;
            groupBox2.Text = "Quantity";
            // 
            // numericUpDown2
            // 
            numericUpDown2.BackColor = Color.FromArgb(60, 63, 65);
            numericUpDown2.DecimalPlaces = 3;
            numericUpDown2.ForeColor = SystemColors.Control;
            numericUpDown2.Location = new Point(102, 22);
            numericUpDown2.Margin = new Padding(4, 3, 4, 3);
            numericUpDown2.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericUpDown2.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(122, 23);
            numericUpDown2.TabIndex = 184;
            numericUpDown2.TextAlign = HorizontalAlignment.Center;
            numericUpDown2.ValueChanged += numericUpDown2_ValueChanged;
            // 
            // darkLabel41
            // 
            darkLabel41.AutoSize = true;
            darkLabel41.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel41.Location = new Point(15, 54);
            darkLabel41.Margin = new Padding(4, 0, 4, 0);
            darkLabel41.Name = "darkLabel41";
            darkLabel41.Size = new Size(79, 15);
            darkLabel41.TabIndex = 185;
            darkLabel41.Text = "Quantity Max";
            // 
            // darkLabel42
            // 
            darkLabel42.AutoSize = true;
            darkLabel42.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel42.Location = new Point(14, 24);
            darkLabel42.Margin = new Padding(4, 0, 4, 0);
            darkLabel42.Name = "darkLabel42";
            darkLabel42.Size = new Size(77, 15);
            darkLabel42.TabIndex = 183;
            darkLabel42.Text = "Quantity Min";
            // 
            // numericUpDown3
            // 
            numericUpDown3.BackColor = Color.FromArgb(60, 63, 65);
            numericUpDown3.DecimalPlaces = 3;
            numericUpDown3.ForeColor = SystemColors.Control;
            numericUpDown3.Location = new Point(102, 51);
            numericUpDown3.Margin = new Padding(4, 3, 4, 3);
            numericUpDown3.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numericUpDown3.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(122, 23);
            numericUpDown3.TabIndex = 182;
            numericUpDown3.TextAlign = HorizontalAlignment.Center;
            numericUpDown3.ValueChanged += numericUpDown3_ValueChanged;
            // 
            // HealthGB
            // 
            HealthGB.Controls.Add(groupBox1);
            HealthGB.Controls.Add(darkButton8);
            HealthGB.Controls.Add(darkButton9);
            HealthGB.Controls.Add(listBox1);
            HealthGB.ForeColor = SystemColors.Control;
            HealthGB.Location = new Point(4, 195);
            HealthGB.Margin = new Padding(4, 3, 4, 3);
            HealthGB.Name = "HealthGB";
            HealthGB.Padding = new Padding(4, 3, 4, 3);
            HealthGB.Size = new Size(312, 305);
            HealthGB.TabIndex = 199;
            HealthGB.TabStop = false;
            HealthGB.Text = "Health";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(darkLabel45);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(darkLabel46);
            groupBox1.Controls.Add(darkLabel44);
            groupBox1.Controls.Add(numericUpDown5);
            groupBox1.Controls.Add(numericUpDown4);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(8, 188);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(291, 109);
            groupBox1.TabIndex = 200;
            groupBox1.TabStop = false;
            groupBox1.Visible = false;
            // 
            // darkLabel45
            // 
            darkLabel45.AutoSize = true;
            darkLabel45.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel45.Location = new Point(13, 18);
            darkLabel45.Margin = new Padding(4, 0, 4, 0);
            darkLabel45.Name = "darkLabel45";
            darkLabel45.Size = new Size(66, 15);
            darkLabel45.TabIndex = 192;
            darkLabel45.Text = "Health Min";
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(60, 63, 65);
            textBox2.ForeColor = SystemColors.Control;
            textBox2.Location = new Point(74, 76);
            textBox2.Margin = new Padding(4, 3, 4, 3);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(196, 23);
            textBox2.TabIndex = 195;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // darkLabel46
            // 
            darkLabel46.AutoSize = true;
            darkLabel46.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel46.Location = new Point(15, 80);
            darkLabel46.Margin = new Padding(4, 0, 4, 0);
            darkLabel46.Name = "darkLabel46";
            darkLabel46.Size = new Size(34, 15);
            darkLabel46.TabIndex = 196;
            darkLabel46.Text = "Zone";
            // 
            // darkLabel44
            // 
            darkLabel44.AutoSize = true;
            darkLabel44.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel44.Location = new Point(13, 48);
            darkLabel44.Margin = new Padding(4, 0, 4, 0);
            darkLabel44.Name = "darkLabel44";
            darkLabel44.Size = new Size(68, 15);
            darkLabel44.TabIndex = 194;
            darkLabel44.Text = "Health Max";
            // 
            // numericUpDown5
            // 
            numericUpDown5.BackColor = Color.FromArgb(60, 63, 65);
            numericUpDown5.DecimalPlaces = 3;
            numericUpDown5.ForeColor = SystemColors.Control;
            numericUpDown5.Location = new Point(148, 46);
            numericUpDown5.Margin = new Padding(4, 3, 4, 3);
            numericUpDown5.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numericUpDown5.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            numericUpDown5.Name = "numericUpDown5";
            numericUpDown5.Size = new Size(122, 23);
            numericUpDown5.TabIndex = 191;
            numericUpDown5.TextAlign = HorizontalAlignment.Center;
            numericUpDown5.ValueChanged += numericUpDown5_ValueChanged;
            // 
            // numericUpDown4
            // 
            numericUpDown4.BackColor = Color.FromArgb(60, 63, 65);
            numericUpDown4.DecimalPlaces = 3;
            numericUpDown4.ForeColor = SystemColors.Control;
            numericUpDown4.Location = new Point(149, 16);
            numericUpDown4.Margin = new Padding(4, 3, 4, 3);
            numericUpDown4.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericUpDown4.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new Size(122, 23);
            numericUpDown4.TabIndex = 193;
            numericUpDown4.TextAlign = HorizontalAlignment.Center;
            numericUpDown4.ValueChanged += numericUpDown4_ValueChanged;
            // 
            // darkButton8
            // 
            darkButton8.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            darkButton8.FlatStyle = FlatStyle.Flat;
            darkButton8.Location = new Point(8, 155);
            darkButton8.Margin = new Padding(4, 3, 4, 3);
            darkButton8.Name = "darkButton8";
            darkButton8.Size = new Size(128, 27);
            darkButton8.TabIndex = 199;
            darkButton8.Text = "Add";
            darkButton8.Click += darkButton8_Click;
            // 
            // darkButton9
            // 
            darkButton9.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            darkButton9.FlatStyle = FlatStyle.Flat;
            darkButton9.Location = new Point(176, 155);
            darkButton9.Margin = new Padding(4, 3, 4, 3);
            darkButton9.Name = "darkButton9";
            darkButton9.Size = new Size(121, 27);
            darkButton9.TabIndex = 198;
            darkButton9.Text = "Remove";
            darkButton9.Click += darkButton9_Click;
            // 
            // listBox1
            // 
            listBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            listBox1.BackColor = Color.FromArgb(60, 63, 65);
            listBox1.DrawMode = DrawMode.OwnerDrawFixed;
            listBox1.ForeColor = SystemColors.Control;
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(8, 17);
            listBox1.Margin = new Padding(4, 3, 4, 3);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(289, 132);
            listBox1.TabIndex = 197;
            listBox1.DrawItem += listBox_DrawItem;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // ExpansionAILoadoutsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(LoadOutGB);
            ForeColor = SystemColors.Control;
            Name = "ExpansionAILoadoutsControl";
            Size = new Size(324, 526);
            LoadOutGB.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            HealthGB.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox LoadOutGB;
        private FlowLayoutPanel flowLayoutPanel1;
        private GroupBox groupBox7;
        private TextBox textBox1;
        private Button darkButton11;
        private Label darkLabel40;
        private Label darkLabel43;
        private NumericUpDown numericUpDown1;
        private GroupBox groupBox2;
        private NumericUpDown numericUpDown2;
        private Label darkLabel41;
        private Label darkLabel42;
        private NumericUpDown numericUpDown3;
        private GroupBox HealthGB;
        private GroupBox groupBox1;
        private Label darkLabel45;
        private TextBox textBox2;
        private Label darkLabel46;
        private Label darkLabel44;
        private NumericUpDown numericUpDown5;
        private NumericUpDown numericUpDown4;
        private Button darkButton8;
        private Button darkButton9;
        private ListBox listBox1;
    }
}
