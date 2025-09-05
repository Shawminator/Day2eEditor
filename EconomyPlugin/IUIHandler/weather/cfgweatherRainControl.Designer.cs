namespace EconomyPlugin
{
    partial class cfgweatherRainControl
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
            groupBox48 = new GroupBox();
            groupBox53 = new GroupBox();
            label5 = new Label();
            RTendNUD = new NumericUpDown();
            RTmaxNUD = new NumericUpDown();
            RTminNUD = new NumericUpDown();
            label90 = new Label();
            label91 = new Label();
            label92 = new Label();
            groupBox49 = new GroupBox();
            label4 = new Label();
            RCLmaxNUD = new NumericUpDown();
            RCLminNUD = new NumericUpDown();
            label81 = new Label();
            label82 = new Label();
            groupBox50 = new GroupBox();
            label3 = new Label();
            RTLmaxNUD = new NumericUpDown();
            RTLminNUD = new NumericUpDown();
            label83 = new Label();
            label84 = new Label();
            groupBox51 = new GroupBox();
            label2 = new Label();
            RLmaxNUD = new NumericUpDown();
            RLminNUD = new NumericUpDown();
            label85 = new Label();
            label86 = new Label();
            groupBox52 = new GroupBox();
            label1 = new Label();
            RCdurationNUD = new NumericUpDown();
            RCtimeNUD = new NumericUpDown();
            RCactualNUD = new NumericUpDown();
            label87 = new Label();
            label88 = new Label();
            label89 = new Label();
            groupBox48.SuspendLayout();
            groupBox53.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)RTendNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RTmaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RTminNUD).BeginInit();
            groupBox49.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)RCLmaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RCLminNUD).BeginInit();
            groupBox50.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)RTLmaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RTLminNUD).BeginInit();
            groupBox51.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)RLmaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RLminNUD).BeginInit();
            groupBox52.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)RCdurationNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RCtimeNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RCactualNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox48
            // 
            groupBox48.Controls.Add(groupBox53);
            groupBox48.Controls.Add(groupBox49);
            groupBox48.Controls.Add(groupBox50);
            groupBox48.Controls.Add(groupBox51);
            groupBox48.Controls.Add(groupBox52);
            groupBox48.ForeColor = SystemColors.Control;
            groupBox48.Location = new Point(0, 0);
            groupBox48.Margin = new Padding(4, 3, 4, 3);
            groupBox48.Name = "groupBox48";
            groupBox48.Padding = new Padding(4, 3, 4, 3);
            groupBox48.Size = new Size(956, 540);
            groupBox48.TabIndex = 82;
            groupBox48.TabStop = false;
            groupBox48.Text = "Rain";
            // 
            // groupBox53
            // 
            groupBox53.Controls.Add(label5);
            groupBox53.Controls.Add(RTendNUD);
            groupBox53.Controls.Add(RTmaxNUD);
            groupBox53.Controls.Add(RTminNUD);
            groupBox53.Controls.Add(label90);
            groupBox53.Controls.Add(label91);
            groupBox53.Controls.Add(label92);
            groupBox53.ForeColor = SystemColors.Control;
            groupBox53.Location = new Point(7, 434);
            groupBox53.Margin = new Padding(4, 3, 4, 3);
            groupBox53.Name = "groupBox53";
            groupBox53.Padding = new Padding(4, 3, 4, 3);
            groupBox53.Size = new Size(940, 97);
            groupBox53.TabIndex = 74;
            groupBox53.TabStop = false;
            groupBox53.Text = "Thresholds";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(7, 19);
            label5.Name = "label5";
            label5.Size = new Size(919, 15);
            label5.TabIndex = 74;
            label5.Text = "What range of the overcast value allows the rain to be preset (min, max overcast value, time in seconds it takes for rain to stop if the overcast is outside of the specified range)";
            // 
            // RTendNUD
            // 
            RTendNUD.BackColor = Color.FromArgb(60, 63, 65);
            RTendNUD.ForeColor = SystemColors.Control;
            RTendNUD.Location = new Point(210, 59);
            RTendNUD.Margin = new Padding(4, 3, 4, 3);
            RTendNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            RTendNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            RTendNUD.Name = "RTendNUD";
            RTendNUD.Size = new Size(91, 23);
            RTendNUD.TabIndex = 73;
            RTendNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // RTmaxNUD
            // 
            RTmaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            RTmaxNUD.DecimalPlaces = 2;
            RTmaxNUD.ForeColor = SystemColors.Control;
            RTmaxNUD.Location = new Point(111, 59);
            RTmaxNUD.Margin = new Padding(4, 3, 4, 3);
            RTmaxNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            RTmaxNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            RTmaxNUD.Name = "RTmaxNUD";
            RTmaxNUD.Size = new Size(91, 23);
            RTmaxNUD.TabIndex = 72;
            RTmaxNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // RTminNUD
            // 
            RTminNUD.BackColor = Color.FromArgb(60, 63, 65);
            RTminNUD.DecimalPlaces = 2;
            RTminNUD.ForeColor = SystemColors.Control;
            RTminNUD.Location = new Point(13, 59);
            RTminNUD.Margin = new Padding(4, 3, 4, 3);
            RTminNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            RTminNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            RTminNUD.Name = "RTminNUD";
            RTminNUD.Size = new Size(91, 23);
            RTminNUD.TabIndex = 71;
            RTminNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // label90
            // 
            label90.AutoSize = true;
            label90.Location = new Point(234, 40);
            label90.Margin = new Padding(4, 0, 4, 0);
            label90.Name = "label90";
            label90.Size = new Size(27, 15);
            label90.TabIndex = 2;
            label90.Text = "End";
            // 
            // label91
            // 
            label91.AutoSize = true;
            label91.Location = new Point(135, 40);
            label91.Margin = new Padding(4, 0, 4, 0);
            label91.Name = "label91";
            label91.Size = new Size(29, 15);
            label91.TabIndex = 1;
            label91.Text = "max";
            // 
            // label92
            // 
            label92.AutoSize = true;
            label92.Location = new Point(40, 40);
            label92.Margin = new Padding(4, 0, 4, 0);
            label92.Name = "label92";
            label92.Size = new Size(28, 15);
            label92.TabIndex = 0;
            label92.Text = "min";
            // 
            // groupBox49
            // 
            groupBox49.Controls.Add(label4);
            groupBox49.Controls.Add(RCLmaxNUD);
            groupBox49.Controls.Add(RCLminNUD);
            groupBox49.Controls.Add(label81);
            groupBox49.Controls.Add(label82);
            groupBox49.ForeColor = SystemColors.Control;
            groupBox49.Location = new Point(7, 331);
            groupBox49.Margin = new Padding(4, 3, 4, 3);
            groupBox49.Name = "groupBox49";
            groupBox49.Padding = new Padding(4, 3, 4, 3);
            groupBox49.Size = new Size(940, 97);
            groupBox49.TabIndex = 75;
            groupBox49.TabStop = false;
            groupBox49.Text = "Change Limits";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(7, 19);
            label4.Name = "label4";
            label4.Size = new Size(219, 15);
            label4.TabIndex = 73;
            label4.Text = "How much should the rain change (0..1)";
            // 
            // RCLmaxNUD
            // 
            RCLmaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            RCLmaxNUD.DecimalPlaces = 2;
            RCLmaxNUD.ForeColor = SystemColors.Control;
            RCLmaxNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            RCLmaxNUD.Location = new Point(112, 59);
            RCLmaxNUD.Margin = new Padding(4, 3, 4, 3);
            RCLmaxNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            RCLmaxNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            RCLmaxNUD.Name = "RCLmaxNUD";
            RCLmaxNUD.Size = new Size(91, 23);
            RCLmaxNUD.TabIndex = 72;
            RCLmaxNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // RCLminNUD
            // 
            RCLminNUD.BackColor = Color.FromArgb(60, 63, 65);
            RCLminNUD.DecimalPlaces = 2;
            RCLminNUD.ForeColor = SystemColors.Control;
            RCLminNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            RCLminNUD.Location = new Point(13, 59);
            RCLminNUD.Margin = new Padding(4, 3, 4, 3);
            RCLminNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            RCLminNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            RCLminNUD.Name = "RCLminNUD";
            RCLminNUD.Size = new Size(91, 23);
            RCLminNUD.TabIndex = 71;
            RCLminNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // label81
            // 
            label81.AutoSize = true;
            label81.Location = new Point(135, 40);
            label81.Margin = new Padding(4, 0, 4, 0);
            label81.Name = "label81";
            label81.Size = new Size(29, 15);
            label81.TabIndex = 1;
            label81.Text = "max";
            // 
            // label82
            // 
            label82.AutoSize = true;
            label82.Location = new Point(40, 40);
            label82.Margin = new Padding(4, 0, 4, 0);
            label82.Name = "label82";
            label82.Size = new Size(28, 15);
            label82.TabIndex = 0;
            label82.Text = "min";
            // 
            // groupBox50
            // 
            groupBox50.Controls.Add(label3);
            groupBox50.Controls.Add(RTLmaxNUD);
            groupBox50.Controls.Add(RTLminNUD);
            groupBox50.Controls.Add(label83);
            groupBox50.Controls.Add(label84);
            groupBox50.ForeColor = SystemColors.Control;
            groupBox50.Location = new Point(7, 228);
            groupBox50.Margin = new Padding(4, 3, 4, 3);
            groupBox50.Name = "groupBox50";
            groupBox50.Padding = new Padding(4, 3, 4, 3);
            groupBox50.Size = new Size(940, 97);
            groupBox50.TabIndex = 75;
            groupBox50.TabStop = false;
            groupBox50.Text = "Time Limits";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(7, 19);
            label3.Name = "label3";
            label3.Size = new Size(457, 15);
            label3.TabIndex = 73;
            label3.Text = "How long does it take to the rain to change from one value to other (time in seconds)";
            // 
            // RTLmaxNUD
            // 
            RTLmaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            RTLmaxNUD.ForeColor = SystemColors.Control;
            RTLmaxNUD.Location = new Point(112, 59);
            RTLmaxNUD.Margin = new Padding(4, 3, 4, 3);
            RTLmaxNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            RTLmaxNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            RTLmaxNUD.Name = "RTLmaxNUD";
            RTLmaxNUD.Size = new Size(91, 23);
            RTLmaxNUD.TabIndex = 72;
            RTLmaxNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // RTLminNUD
            // 
            RTLminNUD.BackColor = Color.FromArgb(60, 63, 65);
            RTLminNUD.ForeColor = SystemColors.Control;
            RTLminNUD.Location = new Point(13, 59);
            RTLminNUD.Margin = new Padding(4, 3, 4, 3);
            RTLminNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            RTLminNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            RTLminNUD.Name = "RTLminNUD";
            RTLminNUD.Size = new Size(91, 23);
            RTLminNUD.TabIndex = 71;
            RTLminNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // label83
            // 
            label83.AutoSize = true;
            label83.Location = new Point(135, 40);
            label83.Margin = new Padding(4, 0, 4, 0);
            label83.Name = "label83";
            label83.Size = new Size(29, 15);
            label83.TabIndex = 1;
            label83.Text = "max";
            // 
            // label84
            // 
            label84.AutoSize = true;
            label84.Location = new Point(40, 40);
            label84.Margin = new Padding(4, 0, 4, 0);
            label84.Name = "label84";
            label84.Size = new Size(28, 15);
            label84.TabIndex = 0;
            label84.Text = "min";
            // 
            // groupBox51
            // 
            groupBox51.Controls.Add(label2);
            groupBox51.Controls.Add(RLmaxNUD);
            groupBox51.Controls.Add(RLminNUD);
            groupBox51.Controls.Add(label85);
            groupBox51.Controls.Add(label86);
            groupBox51.ForeColor = SystemColors.Control;
            groupBox51.Location = new Point(7, 125);
            groupBox51.Margin = new Padding(4, 3, 4, 3);
            groupBox51.Name = "groupBox51";
            groupBox51.Padding = new Padding(4, 3, 4, 3);
            groupBox51.Size = new Size(940, 97);
            groupBox51.TabIndex = 74;
            groupBox51.TabStop = false;
            groupBox51.Text = "Limits";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 19);
            label2.Name = "label2";
            label2.Size = new Size(216, 15);
            label2.TabIndex = 73;
            label2.Text = "What is the range of the rain value (0..1)";
            // 
            // RLmaxNUD
            // 
            RLmaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            RLmaxNUD.DecimalPlaces = 2;
            RLmaxNUD.ForeColor = SystemColors.Control;
            RLmaxNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            RLmaxNUD.Location = new Point(112, 59);
            RLmaxNUD.Margin = new Padding(4, 3, 4, 3);
            RLmaxNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            RLmaxNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            RLmaxNUD.Name = "RLmaxNUD";
            RLmaxNUD.Size = new Size(91, 23);
            RLmaxNUD.TabIndex = 72;
            RLmaxNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // RLminNUD
            // 
            RLminNUD.BackColor = Color.FromArgb(60, 63, 65);
            RLminNUD.DecimalPlaces = 2;
            RLminNUD.ForeColor = SystemColors.Control;
            RLminNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            RLminNUD.Location = new Point(13, 59);
            RLminNUD.Margin = new Padding(4, 3, 4, 3);
            RLminNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            RLminNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            RLminNUD.Name = "RLminNUD";
            RLminNUD.Size = new Size(91, 23);
            RLminNUD.TabIndex = 71;
            RLminNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // label85
            // 
            label85.AutoSize = true;
            label85.Location = new Point(135, 40);
            label85.Margin = new Padding(4, 0, 4, 0);
            label85.Name = "label85";
            label85.Size = new Size(29, 15);
            label85.TabIndex = 1;
            label85.Text = "max";
            // 
            // label86
            // 
            label86.AutoSize = true;
            label86.Location = new Point(40, 40);
            label86.Margin = new Padding(4, 0, 4, 0);
            label86.Name = "label86";
            label86.Size = new Size(28, 15);
            label86.TabIndex = 0;
            label86.Text = "min";
            // 
            // groupBox52
            // 
            groupBox52.Controls.Add(label1);
            groupBox52.Controls.Add(RCdurationNUD);
            groupBox52.Controls.Add(RCtimeNUD);
            groupBox52.Controls.Add(RCactualNUD);
            groupBox52.Controls.Add(label87);
            groupBox52.Controls.Add(label88);
            groupBox52.Controls.Add(label89);
            groupBox52.ForeColor = SystemColors.Control;
            groupBox52.Location = new Point(7, 22);
            groupBox52.Margin = new Padding(4, 3, 4, 3);
            groupBox52.Name = "groupBox52";
            groupBox52.Padding = new Padding(4, 3, 4, 3);
            groupBox52.Size = new Size(940, 97);
            groupBox52.TabIndex = 4;
            groupBox52.TabStop = false;
            groupBox52.Text = "Current";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 19);
            label1.Name = "label1";
            label1.Size = new Size(614, 15);
            label1.TabIndex = 74;
            label1.Text = "Initial conditions of the rain (target value, time to change, how long will it stay), restricted by thresholds (see below)";
            // 
            // RCdurationNUD
            // 
            RCdurationNUD.BackColor = Color.FromArgb(60, 63, 65);
            RCdurationNUD.ForeColor = SystemColors.Control;
            RCdurationNUD.Location = new Point(211, 59);
            RCdurationNUD.Margin = new Padding(4, 3, 4, 3);
            RCdurationNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            RCdurationNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            RCdurationNUD.Name = "RCdurationNUD";
            RCdurationNUD.Size = new Size(91, 23);
            RCdurationNUD.TabIndex = 73;
            RCdurationNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // RCtimeNUD
            // 
            RCtimeNUD.BackColor = Color.FromArgb(60, 63, 65);
            RCtimeNUD.ForeColor = SystemColors.Control;
            RCtimeNUD.Location = new Point(112, 59);
            RCtimeNUD.Margin = new Padding(4, 3, 4, 3);
            RCtimeNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            RCtimeNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            RCtimeNUD.Name = "RCtimeNUD";
            RCtimeNUD.Size = new Size(91, 23);
            RCtimeNUD.TabIndex = 72;
            RCtimeNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // RCactualNUD
            // 
            RCactualNUD.BackColor = Color.FromArgb(60, 63, 65);
            RCactualNUD.DecimalPlaces = 2;
            RCactualNUD.ForeColor = SystemColors.Control;
            RCactualNUD.Location = new Point(13, 59);
            RCactualNUD.Margin = new Padding(4, 3, 4, 3);
            RCactualNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            RCactualNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            RCactualNUD.Name = "RCactualNUD";
            RCactualNUD.Size = new Size(91, 23);
            RCactualNUD.TabIndex = 71;
            RCactualNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // label87
            // 
            label87.AutoSize = true;
            label87.Location = new Point(227, 40);
            label87.Margin = new Padding(4, 0, 4, 0);
            label87.Name = "label87";
            label87.Size = new Size(52, 15);
            label87.TabIndex = 2;
            label87.Text = "duration";
            // 
            // label88
            // 
            label88.AutoSize = true;
            label88.Location = new Point(133, 40);
            label88.Margin = new Padding(4, 0, 4, 0);
            label88.Name = "label88";
            label88.Size = new Size(31, 15);
            label88.TabIndex = 1;
            label88.Text = "time";
            // 
            // label89
            // 
            label89.AutoSize = true;
            label89.Location = new Point(35, 40);
            label89.Margin = new Padding(4, 0, 4, 0);
            label89.Name = "label89";
            label89.Size = new Size(39, 15);
            label89.TabIndex = 0;
            label89.Text = "actual";
            // 
            // cfgweatherRainControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox48);
            ForeColor = SystemColors.Control;
            Name = "cfgweatherRainControl";
            Size = new Size(968, 554);
            groupBox48.ResumeLayout(false);
            groupBox53.ResumeLayout(false);
            groupBox53.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)RTendNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)RTmaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)RTminNUD).EndInit();
            groupBox49.ResumeLayout(false);
            groupBox49.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)RCLmaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)RCLminNUD).EndInit();
            groupBox50.ResumeLayout(false);
            groupBox50.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)RTLmaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)RTLminNUD).EndInit();
            groupBox51.ResumeLayout(false);
            groupBox51.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)RLmaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)RLminNUD).EndInit();
            groupBox52.ResumeLayout(false);
            groupBox52.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)RCdurationNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)RCtimeNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)RCactualNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox48;
        private GroupBox groupBox53;
        private NumericUpDown RTendNUD;
        private NumericUpDown RTmaxNUD;
        private NumericUpDown RTminNUD;
        private Label label90;
        private Label label91;
        private Label label92;
        private GroupBox groupBox49;
        private NumericUpDown RCLmaxNUD;
        private NumericUpDown RCLminNUD;
        private Label label81;
        private Label label82;
        private GroupBox groupBox50;
        private NumericUpDown RTLmaxNUD;
        private NumericUpDown RTLminNUD;
        private Label label83;
        private Label label84;
        private GroupBox groupBox51;
        private NumericUpDown RLmaxNUD;
        private NumericUpDown RLminNUD;
        private Label label85;
        private Label label86;
        private GroupBox groupBox52;
        private NumericUpDown RCdurationNUD;
        private NumericUpDown RCtimeNUD;
        private NumericUpDown RCactualNUD;
        private Label label87;
        private Label label88;
        private Label label89;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label5;
        private Label label4;
    }
}
