namespace EconomyPlugin
{
    partial class cfgweatherSnowfallControl
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
            groupBox89 = new GroupBox();
            groupBox90 = new GroupBox();
            label5 = new Label();
            STendNUD = new NumericUpDown();
            STmaxNUD = new NumericUpDown();
            STminNUD = new NumericUpDown();
            label179 = new Label();
            label180 = new Label();
            label181 = new Label();
            groupBox91 = new GroupBox();
            label4 = new Label();
            SCLmaxNUD = new NumericUpDown();
            SCLminNUD = new NumericUpDown();
            label182 = new Label();
            label183 = new Label();
            groupBox92 = new GroupBox();
            label3 = new Label();
            STLmaxNUD = new NumericUpDown();
            STLminNUD = new NumericUpDown();
            label184 = new Label();
            label185 = new Label();
            groupBox93 = new GroupBox();
            label2 = new Label();
            SLmaxNUD = new NumericUpDown();
            SLminNUD = new NumericUpDown();
            label186 = new Label();
            label187 = new Label();
            groupBox94 = new GroupBox();
            label1 = new Label();
            SCdurationNUD = new NumericUpDown();
            SCtimeNUD = new NumericUpDown();
            SCactualNUD = new NumericUpDown();
            label188 = new Label();
            label189 = new Label();
            label190 = new Label();
            groupBox89.SuspendLayout();
            groupBox90.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)STendNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)STmaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)STminNUD).BeginInit();
            groupBox91.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SCLmaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SCLminNUD).BeginInit();
            groupBox92.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)STLmaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)STLminNUD).BeginInit();
            groupBox93.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SLmaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SLminNUD).BeginInit();
            groupBox94.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SCdurationNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SCtimeNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SCactualNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox89
            // 
            groupBox89.Controls.Add(groupBox90);
            groupBox89.Controls.Add(groupBox91);
            groupBox89.Controls.Add(groupBox92);
            groupBox89.Controls.Add(groupBox93);
            groupBox89.Controls.Add(groupBox94);
            groupBox89.ForeColor = SystemColors.Control;
            groupBox89.Location = new Point(0, 0);
            groupBox89.Margin = new Padding(4, 3, 4, 3);
            groupBox89.Name = "groupBox89";
            groupBox89.Padding = new Padding(4, 3, 4, 3);
            groupBox89.Size = new Size(992, 540);
            groupBox89.TabIndex = 87;
            groupBox89.TabStop = false;
            groupBox89.Text = "Snowfall";
            // 
            // groupBox90
            // 
            groupBox90.Controls.Add(label5);
            groupBox90.Controls.Add(STendNUD);
            groupBox90.Controls.Add(STmaxNUD);
            groupBox90.Controls.Add(STminNUD);
            groupBox90.Controls.Add(label179);
            groupBox90.Controls.Add(label180);
            groupBox90.Controls.Add(label181);
            groupBox90.ForeColor = SystemColors.Control;
            groupBox90.Location = new Point(7, 434);
            groupBox90.Margin = new Padding(4, 3, 4, 3);
            groupBox90.Name = "groupBox90";
            groupBox90.Padding = new Padding(4, 3, 4, 3);
            groupBox90.Size = new Size(977, 97);
            groupBox90.TabIndex = 74;
            groupBox90.TabStop = false;
            groupBox90.Text = "Thresholds";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(7, 19);
            label5.Name = "label5";
            label5.Size = new Size(967, 15);
            label5.TabIndex = 74;
            label5.Text = "What range of the overcast value allows the snowfall to be preset (min, max overcast value, time in seconds it takes for snowfall to stop if the overcast is outside of the specified range)";
            // 
            // STendNUD
            // 
            STendNUD.BackColor = Color.FromArgb(60, 63, 65);
            STendNUD.ForeColor = SystemColors.Control;
            STendNUD.Location = new Point(211, 59);
            STendNUD.Margin = new Padding(4, 3, 4, 3);
            STendNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            STendNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            STendNUD.Name = "STendNUD";
            STendNUD.Size = new Size(91, 23);
            STendNUD.TabIndex = 73;
            STendNUD.TextAlign = HorizontalAlignment.Center;
            STendNUD.ValueChanged += STendNUD_ValueChanged;
            // 
            // STmaxNUD
            // 
            STmaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            STmaxNUD.DecimalPlaces = 2;
            STmaxNUD.ForeColor = SystemColors.Control;
            STmaxNUD.Location = new Point(112, 59);
            STmaxNUD.Margin = new Padding(4, 3, 4, 3);
            STmaxNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            STmaxNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            STmaxNUD.Name = "STmaxNUD";
            STmaxNUD.Size = new Size(91, 23);
            STmaxNUD.TabIndex = 72;
            STmaxNUD.TextAlign = HorizontalAlignment.Center;
            STmaxNUD.ValueChanged += STmaxNUD_ValueChanged;
            // 
            // STminNUD
            // 
            STminNUD.BackColor = Color.FromArgb(60, 63, 65);
            STminNUD.DecimalPlaces = 2;
            STminNUD.ForeColor = SystemColors.Control;
            STminNUD.Location = new Point(13, 59);
            STminNUD.Margin = new Padding(4, 3, 4, 3);
            STminNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            STminNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            STminNUD.Name = "STminNUD";
            STminNUD.Size = new Size(91, 23);
            STminNUD.TabIndex = 71;
            STminNUD.TextAlign = HorizontalAlignment.Center;
            STminNUD.ValueChanged += STminNUD_ValueChanged;
            // 
            // label179
            // 
            label179.AutoSize = true;
            label179.Location = new Point(234, 40);
            label179.Margin = new Padding(4, 0, 4, 0);
            label179.Name = "label179";
            label179.Size = new Size(27, 15);
            label179.TabIndex = 2;
            label179.Text = "End";
            // 
            // label180
            // 
            label180.AutoSize = true;
            label180.Location = new Point(135, 40);
            label180.Margin = new Padding(4, 0, 4, 0);
            label180.Name = "label180";
            label180.Size = new Size(29, 15);
            label180.TabIndex = 1;
            label180.Text = "max";
            // 
            // label181
            // 
            label181.AutoSize = true;
            label181.Location = new Point(40, 40);
            label181.Margin = new Padding(4, 0, 4, 0);
            label181.Name = "label181";
            label181.Size = new Size(28, 15);
            label181.TabIndex = 0;
            label181.Text = "min";
            // 
            // groupBox91
            // 
            groupBox91.Controls.Add(label4);
            groupBox91.Controls.Add(SCLmaxNUD);
            groupBox91.Controls.Add(SCLminNUD);
            groupBox91.Controls.Add(label182);
            groupBox91.Controls.Add(label183);
            groupBox91.ForeColor = SystemColors.Control;
            groupBox91.Location = new Point(7, 331);
            groupBox91.Margin = new Padding(4, 3, 4, 3);
            groupBox91.Name = "groupBox91";
            groupBox91.Padding = new Padding(4, 3, 4, 3);
            groupBox91.Size = new Size(977, 97);
            groupBox91.TabIndex = 75;
            groupBox91.TabStop = false;
            groupBox91.Text = "Change Limits";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(7, 19);
            label4.Name = "label4";
            label4.Size = new Size(243, 15);
            label4.TabIndex = 73;
            label4.Text = "How much should the snowfall change (0..1)";
            // 
            // SCLmaxNUD
            // 
            SCLmaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            SCLmaxNUD.DecimalPlaces = 2;
            SCLmaxNUD.ForeColor = SystemColors.Control;
            SCLmaxNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            SCLmaxNUD.Location = new Point(112, 59);
            SCLmaxNUD.Margin = new Padding(4, 3, 4, 3);
            SCLmaxNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            SCLmaxNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            SCLmaxNUD.Name = "SCLmaxNUD";
            SCLmaxNUD.Size = new Size(91, 23);
            SCLmaxNUD.TabIndex = 72;
            SCLmaxNUD.TextAlign = HorizontalAlignment.Center;
            SCLmaxNUD.ValueChanged += SCLmaxNUD_ValueChanged;
            // 
            // SCLminNUD
            // 
            SCLminNUD.BackColor = Color.FromArgb(60, 63, 65);
            SCLminNUD.DecimalPlaces = 2;
            SCLminNUD.ForeColor = SystemColors.Control;
            SCLminNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            SCLminNUD.Location = new Point(13, 59);
            SCLminNUD.Margin = new Padding(4, 3, 4, 3);
            SCLminNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            SCLminNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            SCLminNUD.Name = "SCLminNUD";
            SCLminNUD.Size = new Size(91, 23);
            SCLminNUD.TabIndex = 71;
            SCLminNUD.TextAlign = HorizontalAlignment.Center;
            SCLminNUD.ValueChanged += SCLminNUD_ValueChanged;
            // 
            // label182
            // 
            label182.AutoSize = true;
            label182.Location = new Point(135, 40);
            label182.Margin = new Padding(4, 0, 4, 0);
            label182.Name = "label182";
            label182.Size = new Size(29, 15);
            label182.TabIndex = 1;
            label182.Text = "max";
            // 
            // label183
            // 
            label183.AutoSize = true;
            label183.Location = new Point(40, 40);
            label183.Margin = new Padding(4, 0, 4, 0);
            label183.Name = "label183";
            label183.Size = new Size(28, 15);
            label183.TabIndex = 0;
            label183.Text = "min";
            // 
            // groupBox92
            // 
            groupBox92.Controls.Add(label3);
            groupBox92.Controls.Add(STLmaxNUD);
            groupBox92.Controls.Add(STLminNUD);
            groupBox92.Controls.Add(label184);
            groupBox92.Controls.Add(label185);
            groupBox92.ForeColor = SystemColors.Control;
            groupBox92.Location = new Point(7, 228);
            groupBox92.Margin = new Padding(4, 3, 4, 3);
            groupBox92.Name = "groupBox92";
            groupBox92.Padding = new Padding(4, 3, 4, 3);
            groupBox92.Size = new Size(977, 97);
            groupBox92.TabIndex = 75;
            groupBox92.TabStop = false;
            groupBox92.Text = "Time Limits";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(7, 19);
            label3.Name = "label3";
            label3.Size = new Size(481, 15);
            label3.TabIndex = 73;
            label3.Text = "How long does it take to the snowfall to change from one value to other (time in seconds)";
            // 
            // STLmaxNUD
            // 
            STLmaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            STLmaxNUD.ForeColor = SystemColors.Control;
            STLmaxNUD.Location = new Point(112, 59);
            STLmaxNUD.Margin = new Padding(4, 3, 4, 3);
            STLmaxNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            STLmaxNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            STLmaxNUD.Name = "STLmaxNUD";
            STLmaxNUD.Size = new Size(91, 23);
            STLmaxNUD.TabIndex = 72;
            STLmaxNUD.TextAlign = HorizontalAlignment.Center;
            STLmaxNUD.ValueChanged += STLmaxNUD_ValueChanged;
            // 
            // STLminNUD
            // 
            STLminNUD.BackColor = Color.FromArgb(60, 63, 65);
            STLminNUD.ForeColor = SystemColors.Control;
            STLminNUD.Location = new Point(13, 59);
            STLminNUD.Margin = new Padding(4, 3, 4, 3);
            STLminNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            STLminNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            STLminNUD.Name = "STLminNUD";
            STLminNUD.Size = new Size(91, 23);
            STLminNUD.TabIndex = 71;
            STLminNUD.TextAlign = HorizontalAlignment.Center;
            STLminNUD.ValueChanged += STLminNUD_ValueChanged;
            // 
            // label184
            // 
            label184.AutoSize = true;
            label184.Location = new Point(135, 40);
            label184.Margin = new Padding(4, 0, 4, 0);
            label184.Name = "label184";
            label184.Size = new Size(29, 15);
            label184.TabIndex = 1;
            label184.Text = "max";
            // 
            // label185
            // 
            label185.AutoSize = true;
            label185.Location = new Point(40, 40);
            label185.Margin = new Padding(4, 0, 4, 0);
            label185.Name = "label185";
            label185.Size = new Size(28, 15);
            label185.TabIndex = 0;
            label185.Text = "min";
            // 
            // groupBox93
            // 
            groupBox93.Controls.Add(label2);
            groupBox93.Controls.Add(SLmaxNUD);
            groupBox93.Controls.Add(SLminNUD);
            groupBox93.Controls.Add(label186);
            groupBox93.Controls.Add(label187);
            groupBox93.ForeColor = SystemColors.Control;
            groupBox93.Location = new Point(7, 125);
            groupBox93.Margin = new Padding(4, 3, 4, 3);
            groupBox93.Name = "groupBox93";
            groupBox93.Padding = new Padding(4, 3, 4, 3);
            groupBox93.Size = new Size(977, 97);
            groupBox93.TabIndex = 74;
            groupBox93.TabStop = false;
            groupBox93.Text = "Limits";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 19);
            label2.Name = "label2";
            label2.Size = new Size(240, 15);
            label2.TabIndex = 73;
            label2.Text = "What is the range of the snowfall value (0..1)";
            // 
            // SLmaxNUD
            // 
            SLmaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            SLmaxNUD.DecimalPlaces = 2;
            SLmaxNUD.ForeColor = SystemColors.Control;
            SLmaxNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            SLmaxNUD.Location = new Point(112, 59);
            SLmaxNUD.Margin = new Padding(4, 3, 4, 3);
            SLmaxNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            SLmaxNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            SLmaxNUD.Name = "SLmaxNUD";
            SLmaxNUD.Size = new Size(91, 23);
            SLmaxNUD.TabIndex = 72;
            SLmaxNUD.TextAlign = HorizontalAlignment.Center;
            SLmaxNUD.ValueChanged += SLmaxNUD_ValueChanged;
            // 
            // SLminNUD
            // 
            SLminNUD.BackColor = Color.FromArgb(60, 63, 65);
            SLminNUD.DecimalPlaces = 2;
            SLminNUD.ForeColor = SystemColors.Control;
            SLminNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            SLminNUD.Location = new Point(13, 59);
            SLminNUD.Margin = new Padding(4, 3, 4, 3);
            SLminNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            SLminNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            SLminNUD.Name = "SLminNUD";
            SLminNUD.Size = new Size(91, 23);
            SLminNUD.TabIndex = 71;
            SLminNUD.TextAlign = HorizontalAlignment.Center;
            SLminNUD.ValueChanged += SLminNUD_ValueChanged;
            // 
            // label186
            // 
            label186.AutoSize = true;
            label186.Location = new Point(135, 40);
            label186.Margin = new Padding(4, 0, 4, 0);
            label186.Name = "label186";
            label186.Size = new Size(29, 15);
            label186.TabIndex = 1;
            label186.Text = "max";
            // 
            // label187
            // 
            label187.AutoSize = true;
            label187.Location = new Point(40, 40);
            label187.Margin = new Padding(4, 0, 4, 0);
            label187.Name = "label187";
            label187.Size = new Size(28, 15);
            label187.TabIndex = 0;
            label187.Text = "min";
            // 
            // groupBox94
            // 
            groupBox94.Controls.Add(label1);
            groupBox94.Controls.Add(SCdurationNUD);
            groupBox94.Controls.Add(SCtimeNUD);
            groupBox94.Controls.Add(SCactualNUD);
            groupBox94.Controls.Add(label188);
            groupBox94.Controls.Add(label189);
            groupBox94.Controls.Add(label190);
            groupBox94.ForeColor = SystemColors.Control;
            groupBox94.Location = new Point(7, 22);
            groupBox94.Margin = new Padding(4, 3, 4, 3);
            groupBox94.Name = "groupBox94";
            groupBox94.Padding = new Padding(4, 3, 4, 3);
            groupBox94.Size = new Size(977, 97);
            groupBox94.TabIndex = 4;
            groupBox94.TabStop = false;
            groupBox94.Text = "Current";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 19);
            label1.Name = "label1";
            label1.Size = new Size(638, 15);
            label1.TabIndex = 74;
            label1.Text = "Initial conditions of the snowfall (target value, time to change, how long will it stay), restricted by thresholds (see below)";
            // 
            // SCdurationNUD
            // 
            SCdurationNUD.BackColor = Color.FromArgb(60, 63, 65);
            SCdurationNUD.ForeColor = SystemColors.Control;
            SCdurationNUD.Location = new Point(211, 59);
            SCdurationNUD.Margin = new Padding(4, 3, 4, 3);
            SCdurationNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            SCdurationNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            SCdurationNUD.Name = "SCdurationNUD";
            SCdurationNUD.Size = new Size(91, 23);
            SCdurationNUD.TabIndex = 73;
            SCdurationNUD.TextAlign = HorizontalAlignment.Center;
            SCdurationNUD.ValueChanged += SCdurationNUD_ValueChanged;
            // 
            // SCtimeNUD
            // 
            SCtimeNUD.BackColor = Color.FromArgb(60, 63, 65);
            SCtimeNUD.ForeColor = SystemColors.Control;
            SCtimeNUD.Location = new Point(112, 59);
            SCtimeNUD.Margin = new Padding(4, 3, 4, 3);
            SCtimeNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            SCtimeNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            SCtimeNUD.Name = "SCtimeNUD";
            SCtimeNUD.Size = new Size(91, 23);
            SCtimeNUD.TabIndex = 72;
            SCtimeNUD.TextAlign = HorizontalAlignment.Center;
            SCtimeNUD.ValueChanged += SCtimeNUD_ValueChanged;
            // 
            // SCactualNUD
            // 
            SCactualNUD.BackColor = Color.FromArgb(60, 63, 65);
            SCactualNUD.DecimalPlaces = 2;
            SCactualNUD.ForeColor = SystemColors.Control;
            SCactualNUD.Location = new Point(13, 59);
            SCactualNUD.Margin = new Padding(4, 3, 4, 3);
            SCactualNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            SCactualNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            SCactualNUD.Name = "SCactualNUD";
            SCactualNUD.Size = new Size(91, 23);
            SCactualNUD.TabIndex = 71;
            SCactualNUD.TextAlign = HorizontalAlignment.Center;
            SCactualNUD.ValueChanged += SCactualNUD_ValueChanged;
            // 
            // label188
            // 
            label188.AutoSize = true;
            label188.Location = new Point(227, 40);
            label188.Margin = new Padding(4, 0, 4, 0);
            label188.Name = "label188";
            label188.Size = new Size(52, 15);
            label188.TabIndex = 2;
            label188.Text = "duration";
            // 
            // label189
            // 
            label189.AutoSize = true;
            label189.Location = new Point(133, 40);
            label189.Margin = new Padding(4, 0, 4, 0);
            label189.Name = "label189";
            label189.Size = new Size(31, 15);
            label189.TabIndex = 1;
            label189.Text = "time";
            // 
            // label190
            // 
            label190.AutoSize = true;
            label190.Location = new Point(35, 40);
            label190.Margin = new Padding(4, 0, 4, 0);
            label190.Name = "label190";
            label190.Size = new Size(39, 15);
            label190.TabIndex = 0;
            label190.Text = "actual";
            // 
            // cfgweatherSnowfallControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox89);
            ForeColor = SystemColors.Control;
            Name = "cfgweatherSnowfallControl";
            Size = new Size(1009, 551);
            groupBox89.ResumeLayout(false);
            groupBox90.ResumeLayout(false);
            groupBox90.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)STendNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)STmaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)STminNUD).EndInit();
            groupBox91.ResumeLayout(false);
            groupBox91.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SCLmaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)SCLminNUD).EndInit();
            groupBox92.ResumeLayout(false);
            groupBox92.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)STLmaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)STLminNUD).EndInit();
            groupBox93.ResumeLayout(false);
            groupBox93.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SLmaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)SLminNUD).EndInit();
            groupBox94.ResumeLayout(false);
            groupBox94.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SCdurationNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)SCtimeNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)SCactualNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox89;
        private GroupBox groupBox90;
        private NumericUpDown STendNUD;
        private NumericUpDown STmaxNUD;
        private NumericUpDown STminNUD;
        private Label label179;
        private Label label180;
        private Label label181;
        private GroupBox groupBox91;
        private NumericUpDown SCLmaxNUD;
        private NumericUpDown SCLminNUD;
        private Label label182;
        private Label label183;
        private GroupBox groupBox92;
        private NumericUpDown STLmaxNUD;
        private NumericUpDown STLminNUD;
        private Label label184;
        private Label label185;
        private GroupBox groupBox93;
        private NumericUpDown SLmaxNUD;
        private NumericUpDown SLminNUD;
        private Label label186;
        private Label label187;
        private GroupBox groupBox94;
        private NumericUpDown SCdurationNUD;
        private NumericUpDown SCtimeNUD;
        private NumericUpDown SCactualNUD;
        private Label label188;
        private Label label189;
        private Label label190;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}
