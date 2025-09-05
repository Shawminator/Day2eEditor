namespace EconomyPlugin
{
    partial class cfgweatherWindMagnitudeControl
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
            groupBox54 = new GroupBox();
            groupBox57 = new GroupBox();
            label4 = new Label();
            WMCLmaxNUD = new NumericUpDown();
            WMCLminNUD = new NumericUpDown();
            label93 = new Label();
            label97 = new Label();
            groupBox81 = new GroupBox();
            label3 = new Label();
            WMTLmaxNUD = new NumericUpDown();
            WMTLminNUD = new NumericUpDown();
            label98 = new Label();
            label101 = new Label();
            groupBox82 = new GroupBox();
            label2 = new Label();
            WMLmaxNUD = new NumericUpDown();
            WMLminNUD = new NumericUpDown();
            label162 = new Label();
            label166 = new Label();
            groupBox83 = new GroupBox();
            label1 = new Label();
            WMCdurationNUD = new NumericUpDown();
            WMCtimeNUD = new NumericUpDown();
            WMCactualNUD = new NumericUpDown();
            label167 = new Label();
            label168 = new Label();
            label169 = new Label();
            groupBox54.SuspendLayout();
            groupBox57.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WMCLmaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)WMCLminNUD).BeginInit();
            groupBox81.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WMTLmaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)WMTLminNUD).BeginInit();
            groupBox82.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WMLmaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)WMLminNUD).BeginInit();
            groupBox83.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WMCdurationNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)WMCtimeNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)WMCactualNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox54
            // 
            groupBox54.Controls.Add(groupBox57);
            groupBox54.Controls.Add(groupBox81);
            groupBox54.Controls.Add(groupBox82);
            groupBox54.Controls.Add(groupBox83);
            groupBox54.ForeColor = SystemColors.Control;
            groupBox54.Location = new Point(0, 0);
            groupBox54.Margin = new Padding(4, 3, 4, 3);
            groupBox54.Name = "groupBox54";
            groupBox54.Padding = new Padding(4, 3, 4, 3);
            groupBox54.Size = new Size(713, 440);
            groupBox54.TabIndex = 86;
            groupBox54.TabStop = false;
            groupBox54.Text = "Wind Magnitude";
            // 
            // groupBox57
            // 
            groupBox57.Controls.Add(label4);
            groupBox57.Controls.Add(WMCLmaxNUD);
            groupBox57.Controls.Add(WMCLminNUD);
            groupBox57.Controls.Add(label93);
            groupBox57.Controls.Add(label97);
            groupBox57.ForeColor = SystemColors.Control;
            groupBox57.Location = new Point(7, 331);
            groupBox57.Margin = new Padding(4, 3, 4, 3);
            groupBox57.Name = "groupBox57";
            groupBox57.Padding = new Padding(4, 3, 4, 3);
            groupBox57.Size = new Size(698, 97);
            groupBox57.TabIndex = 75;
            groupBox57.TabStop = false;
            groupBox57.Text = "Change Limits";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(7, 19);
            label4.Name = "label4";
            label4.Size = new Size(513, 15);
            label4.TabIndex = 78;
            label4.Text = "How long does it take to the wind direction to change from one value to other (time in seconds)\r\n";
            // 
            // WMCLmaxNUD
            // 
            WMCLmaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            WMCLmaxNUD.DecimalPlaces = 2;
            WMCLmaxNUD.ForeColor = SystemColors.Control;
            WMCLmaxNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            WMCLmaxNUD.Location = new Point(112, 59);
            WMCLmaxNUD.Margin = new Padding(4, 3, 4, 3);
            WMCLmaxNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            WMCLmaxNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            WMCLmaxNUD.Name = "WMCLmaxNUD";
            WMCLmaxNUD.Size = new Size(91, 23);
            WMCLmaxNUD.TabIndex = 72;
            WMCLmaxNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // WMCLminNUD
            // 
            WMCLminNUD.BackColor = Color.FromArgb(60, 63, 65);
            WMCLminNUD.DecimalPlaces = 2;
            WMCLminNUD.ForeColor = SystemColors.Control;
            WMCLminNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            WMCLminNUD.Location = new Point(13, 59);
            WMCLminNUD.Margin = new Padding(4, 3, 4, 3);
            WMCLminNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            WMCLminNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            WMCLminNUD.Name = "WMCLminNUD";
            WMCLminNUD.Size = new Size(91, 23);
            WMCLminNUD.TabIndex = 71;
            WMCLminNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // label93
            // 
            label93.AutoSize = true;
            label93.Location = new Point(135, 40);
            label93.Margin = new Padding(4, 0, 4, 0);
            label93.Name = "label93";
            label93.Size = new Size(29, 15);
            label93.TabIndex = 1;
            label93.Text = "max";
            // 
            // label97
            // 
            label97.AutoSize = true;
            label97.Location = new Point(40, 40);
            label97.Margin = new Padding(4, 0, 4, 0);
            label97.Name = "label97";
            label97.Size = new Size(28, 15);
            label97.TabIndex = 0;
            label97.Text = "min";
            // 
            // groupBox81
            // 
            groupBox81.Controls.Add(label3);
            groupBox81.Controls.Add(WMTLmaxNUD);
            groupBox81.Controls.Add(WMTLminNUD);
            groupBox81.Controls.Add(label98);
            groupBox81.Controls.Add(label101);
            groupBox81.ForeColor = SystemColors.Control;
            groupBox81.Location = new Point(7, 228);
            groupBox81.Margin = new Padding(4, 3, 4, 3);
            groupBox81.Name = "groupBox81";
            groupBox81.Padding = new Padding(4, 3, 4, 3);
            groupBox81.Size = new Size(698, 97);
            groupBox81.TabIndex = 75;
            groupBox81.TabStop = false;
            groupBox81.Text = "Time Limits";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(7, 19);
            label3.Name = "label3";
            label3.Size = new Size(513, 15);
            label3.TabIndex = 77;
            label3.Text = "How long does it take to the wind direction to change from one value to other (time in seconds)\r\n";
            // 
            // WMTLmaxNUD
            // 
            WMTLmaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            WMTLmaxNUD.ForeColor = SystemColors.Control;
            WMTLmaxNUD.Location = new Point(112, 59);
            WMTLmaxNUD.Margin = new Padding(4, 3, 4, 3);
            WMTLmaxNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            WMTLmaxNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            WMTLmaxNUD.Name = "WMTLmaxNUD";
            WMTLmaxNUD.Size = new Size(91, 23);
            WMTLmaxNUD.TabIndex = 72;
            WMTLmaxNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // WMTLminNUD
            // 
            WMTLminNUD.BackColor = Color.FromArgb(60, 63, 65);
            WMTLminNUD.ForeColor = SystemColors.Control;
            WMTLminNUD.Location = new Point(13, 59);
            WMTLminNUD.Margin = new Padding(4, 3, 4, 3);
            WMTLminNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            WMTLminNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            WMTLminNUD.Name = "WMTLminNUD";
            WMTLminNUD.Size = new Size(91, 23);
            WMTLminNUD.TabIndex = 71;
            WMTLminNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // label98
            // 
            label98.AutoSize = true;
            label98.Location = new Point(135, 40);
            label98.Margin = new Padding(4, 0, 4, 0);
            label98.Name = "label98";
            label98.Size = new Size(29, 15);
            label98.TabIndex = 1;
            label98.Text = "max";
            // 
            // label101
            // 
            label101.AutoSize = true;
            label101.Location = new Point(40, 40);
            label101.Margin = new Padding(4, 0, 4, 0);
            label101.Name = "label101";
            label101.Size = new Size(28, 15);
            label101.TabIndex = 0;
            label101.Text = "min";
            // 
            // groupBox82
            // 
            groupBox82.Controls.Add(label2);
            groupBox82.Controls.Add(WMLmaxNUD);
            groupBox82.Controls.Add(WMLminNUD);
            groupBox82.Controls.Add(label162);
            groupBox82.Controls.Add(label166);
            groupBox82.ForeColor = SystemColors.Control;
            groupBox82.Location = new Point(7, 125);
            groupBox82.Margin = new Padding(4, 3, 4, 3);
            groupBox82.Name = "groupBox82";
            groupBox82.Padding = new Padding(4, 3, 4, 3);
            groupBox82.Size = new Size(698, 97);
            groupBox82.TabIndex = 74;
            groupBox82.TabStop = false;
            groupBox82.Text = "Limits";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 19);
            label2.Name = "label2";
            label2.Size = new Size(291, 15);
            label2.TabIndex = 76;
            label2.Text = "What is the range of the wind magnitude value in m/s";
            // 
            // WMLmaxNUD
            // 
            WMLmaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            WMLmaxNUD.DecimalPlaces = 2;
            WMLmaxNUD.ForeColor = SystemColors.Control;
            WMLmaxNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            WMLmaxNUD.Location = new Point(112, 59);
            WMLmaxNUD.Margin = new Padding(4, 3, 4, 3);
            WMLmaxNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            WMLmaxNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            WMLmaxNUD.Name = "WMLmaxNUD";
            WMLmaxNUD.Size = new Size(91, 23);
            WMLmaxNUD.TabIndex = 72;
            WMLmaxNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // WMLminNUD
            // 
            WMLminNUD.BackColor = Color.FromArgb(60, 63, 65);
            WMLminNUD.DecimalPlaces = 2;
            WMLminNUD.ForeColor = SystemColors.Control;
            WMLminNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            WMLminNUD.Location = new Point(13, 59);
            WMLminNUD.Margin = new Padding(4, 3, 4, 3);
            WMLminNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            WMLminNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            WMLminNUD.Name = "WMLminNUD";
            WMLminNUD.Size = new Size(91, 23);
            WMLminNUD.TabIndex = 71;
            WMLminNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // label162
            // 
            label162.AutoSize = true;
            label162.Location = new Point(135, 40);
            label162.Margin = new Padding(4, 0, 4, 0);
            label162.Name = "label162";
            label162.Size = new Size(29, 15);
            label162.TabIndex = 1;
            label162.Text = "max";
            // 
            // label166
            // 
            label166.AutoSize = true;
            label166.Location = new Point(40, 40);
            label166.Margin = new Padding(4, 0, 4, 0);
            label166.Name = "label166";
            label166.Size = new Size(28, 15);
            label166.TabIndex = 0;
            label166.Text = "min";
            // 
            // groupBox83
            // 
            groupBox83.Controls.Add(label1);
            groupBox83.Controls.Add(WMCdurationNUD);
            groupBox83.Controls.Add(WMCtimeNUD);
            groupBox83.Controls.Add(WMCactualNUD);
            groupBox83.Controls.Add(label167);
            groupBox83.Controls.Add(label168);
            groupBox83.Controls.Add(label169);
            groupBox83.ForeColor = SystemColors.Control;
            groupBox83.Location = new Point(7, 22);
            groupBox83.Margin = new Padding(4, 3, 4, 3);
            groupBox83.Name = "groupBox83";
            groupBox83.Padding = new Padding(4, 3, 4, 3);
            groupBox83.Size = new Size(698, 97);
            groupBox83.TabIndex = 4;
            groupBox83.TabStop = false;
            groupBox83.Text = "Current";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 19);
            label1.Name = "label1";
            label1.Size = new Size(681, 15);
            label1.TabIndex = 75;
            label1.Text = "Initial conditions of the wind magnitude (target value, time to change, how long will it stay), restricted by thresholds (see below)";
            // 
            // WMCdurationNUD
            // 
            WMCdurationNUD.BackColor = Color.FromArgb(60, 63, 65);
            WMCdurationNUD.ForeColor = SystemColors.Control;
            WMCdurationNUD.Location = new Point(211, 59);
            WMCdurationNUD.Margin = new Padding(4, 3, 4, 3);
            WMCdurationNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            WMCdurationNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            WMCdurationNUD.Name = "WMCdurationNUD";
            WMCdurationNUD.Size = new Size(91, 23);
            WMCdurationNUD.TabIndex = 73;
            WMCdurationNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // WMCtimeNUD
            // 
            WMCtimeNUD.BackColor = Color.FromArgb(60, 63, 65);
            WMCtimeNUD.ForeColor = SystemColors.Control;
            WMCtimeNUD.Location = new Point(112, 59);
            WMCtimeNUD.Margin = new Padding(4, 3, 4, 3);
            WMCtimeNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            WMCtimeNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            WMCtimeNUD.Name = "WMCtimeNUD";
            WMCtimeNUD.Size = new Size(91, 23);
            WMCtimeNUD.TabIndex = 72;
            WMCtimeNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // WMCactualNUD
            // 
            WMCactualNUD.BackColor = Color.FromArgb(60, 63, 65);
            WMCactualNUD.DecimalPlaces = 2;
            WMCactualNUD.ForeColor = SystemColors.Control;
            WMCactualNUD.Location = new Point(13, 59);
            WMCactualNUD.Margin = new Padding(4, 3, 4, 3);
            WMCactualNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            WMCactualNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            WMCactualNUD.Name = "WMCactualNUD";
            WMCactualNUD.Size = new Size(91, 23);
            WMCactualNUD.TabIndex = 71;
            WMCactualNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // label167
            // 
            label167.AutoSize = true;
            label167.Location = new Point(227, 40);
            label167.Margin = new Padding(4, 0, 4, 0);
            label167.Name = "label167";
            label167.Size = new Size(52, 15);
            label167.TabIndex = 2;
            label167.Text = "duration";
            // 
            // label168
            // 
            label168.AutoSize = true;
            label168.Location = new Point(133, 40);
            label168.Margin = new Padding(4, 0, 4, 0);
            label168.Name = "label168";
            label168.Size = new Size(31, 15);
            label168.TabIndex = 1;
            label168.Text = "time";
            // 
            // label169
            // 
            label169.AutoSize = true;
            label169.Location = new Point(35, 40);
            label169.Margin = new Padding(4, 0, 4, 0);
            label169.Name = "label169";
            label169.Size = new Size(39, 15);
            label169.TabIndex = 0;
            label169.Text = "actual";
            // 
            // cfgweatherWindMagnitudeControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox54);
            ForeColor = SystemColors.Control;
            Name = "cfgweatherWindMagnitudeControl";
            Size = new Size(723, 449);
            groupBox54.ResumeLayout(false);
            groupBox57.ResumeLayout(false);
            groupBox57.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)WMCLmaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)WMCLminNUD).EndInit();
            groupBox81.ResumeLayout(false);
            groupBox81.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)WMTLmaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)WMTLminNUD).EndInit();
            groupBox82.ResumeLayout(false);
            groupBox82.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)WMLmaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)WMLminNUD).EndInit();
            groupBox83.ResumeLayout(false);
            groupBox83.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)WMCdurationNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)WMCtimeNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)WMCactualNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox54;
        private GroupBox groupBox57;
        private NumericUpDown WMCLmaxNUD;
        private NumericUpDown WMCLminNUD;
        private Label label93;
        private Label label97;
        private GroupBox groupBox81;
        private NumericUpDown WMTLmaxNUD;
        private NumericUpDown WMTLminNUD;
        private Label label98;
        private Label label101;
        private GroupBox groupBox82;
        private NumericUpDown WMLmaxNUD;
        private NumericUpDown WMLminNUD;
        private Label label162;
        private Label label166;
        private GroupBox groupBox83;
        private NumericUpDown WMCdurationNUD;
        private NumericUpDown WMCtimeNUD;
        private NumericUpDown WMCactualNUD;
        private Label label167;
        private Label label168;
        private Label label169;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}
