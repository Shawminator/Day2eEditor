namespace EconomyPlugin
{
    partial class cfgweatherFogControl
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
            groupBox43 = new GroupBox();
            groupBox44 = new GroupBox();
            label4 = new Label();
            FCLmaxNUD = new NumericUpDown();
            FCLminNUD = new NumericUpDown();
            label72 = new Label();
            label73 = new Label();
            groupBox45 = new GroupBox();
            label3 = new Label();
            FTLmaxNUD = new NumericUpDown();
            FTLminNUD = new NumericUpDown();
            label74 = new Label();
            label75 = new Label();
            groupBox46 = new GroupBox();
            label2 = new Label();
            FLmaxNUD = new NumericUpDown();
            FLminNUD = new NumericUpDown();
            label76 = new Label();
            label77 = new Label();
            groupBox47 = new GroupBox();
            label1 = new Label();
            FCdurationNUD = new NumericUpDown();
            FCtimeNUD = new NumericUpDown();
            FCactualNUD = new NumericUpDown();
            label78 = new Label();
            label79 = new Label();
            label80 = new Label();
            groupBox43.SuspendLayout();
            groupBox44.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)FCLmaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FCLminNUD).BeginInit();
            groupBox45.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)FTLmaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FTLminNUD).BeginInit();
            groupBox46.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)FLmaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FLminNUD).BeginInit();
            groupBox47.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)FCdurationNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FCtimeNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FCactualNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox43
            // 
            groupBox43.Controls.Add(groupBox44);
            groupBox43.Controls.Add(groupBox45);
            groupBox43.Controls.Add(groupBox46);
            groupBox43.Controls.Add(groupBox47);
            groupBox43.ForeColor = SystemColors.Control;
            groupBox43.Location = new Point(0, 0);
            groupBox43.Margin = new Padding(4, 3, 4, 3);
            groupBox43.Name = "groupBox43";
            groupBox43.Padding = new Padding(4, 3, 4, 3);
            groupBox43.Size = new Size(517, 440);
            groupBox43.TabIndex = 83;
            groupBox43.TabStop = false;
            groupBox43.Text = "Fog";
            // 
            // groupBox44
            // 
            groupBox44.Controls.Add(label4);
            groupBox44.Controls.Add(FCLmaxNUD);
            groupBox44.Controls.Add(FCLminNUD);
            groupBox44.Controls.Add(label72);
            groupBox44.Controls.Add(label73);
            groupBox44.ForeColor = SystemColors.Control;
            groupBox44.Location = new Point(7, 331);
            groupBox44.Margin = new Padding(4, 3, 4, 3);
            groupBox44.Name = "groupBox44";
            groupBox44.Padding = new Padding(4, 3, 4, 3);
            groupBox44.Size = new Size(499, 97);
            groupBox44.TabIndex = 75;
            groupBox44.TabStop = false;
            groupBox44.Text = "Change Limits";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(7, 19);
            label4.Name = "label4";
            label4.Size = new Size(217, 15);
            label4.TabIndex = 73;
            label4.Text = "How much should the fog change (0..1)";
            // 
            // FCLmaxNUD
            // 
            FCLmaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            FCLmaxNUD.DecimalPlaces = 2;
            FCLmaxNUD.ForeColor = SystemColors.Control;
            FCLmaxNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            FCLmaxNUD.Location = new Point(112, 59);
            FCLmaxNUD.Margin = new Padding(4, 3, 4, 3);
            FCLmaxNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            FCLmaxNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            FCLmaxNUD.Name = "FCLmaxNUD";
            FCLmaxNUD.Size = new Size(91, 23);
            FCLmaxNUD.TabIndex = 72;
            FCLmaxNUD.TextAlign = HorizontalAlignment.Center;
            FCLmaxNUD.ValueChanged += FCLmaxNUD_ValueChanged;
            // 
            // FCLminNUD
            // 
            FCLminNUD.BackColor = Color.FromArgb(60, 63, 65);
            FCLminNUD.DecimalPlaces = 2;
            FCLminNUD.ForeColor = SystemColors.Control;
            FCLminNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            FCLminNUD.Location = new Point(13, 59);
            FCLminNUD.Margin = new Padding(4, 3, 4, 3);
            FCLminNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            FCLminNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            FCLminNUD.Name = "FCLminNUD";
            FCLminNUD.Size = new Size(91, 23);
            FCLminNUD.TabIndex = 71;
            FCLminNUD.TextAlign = HorizontalAlignment.Center;
            FCLminNUD.ValueChanged += FCLminNUD_ValueChanged;
            // 
            // label72
            // 
            label72.AutoSize = true;
            label72.Location = new Point(135, 40);
            label72.Margin = new Padding(4, 0, 4, 0);
            label72.Name = "label72";
            label72.Size = new Size(29, 15);
            label72.TabIndex = 1;
            label72.Text = "max";
            // 
            // label73
            // 
            label73.AutoSize = true;
            label73.Location = new Point(40, 40);
            label73.Margin = new Padding(4, 0, 4, 0);
            label73.Name = "label73";
            label73.Size = new Size(28, 15);
            label73.TabIndex = 0;
            label73.Text = "min";
            // 
            // groupBox45
            // 
            groupBox45.Controls.Add(label3);
            groupBox45.Controls.Add(FTLmaxNUD);
            groupBox45.Controls.Add(FTLminNUD);
            groupBox45.Controls.Add(label74);
            groupBox45.Controls.Add(label75);
            groupBox45.ForeColor = SystemColors.Control;
            groupBox45.Location = new Point(7, 228);
            groupBox45.Margin = new Padding(4, 3, 4, 3);
            groupBox45.Name = "groupBox45";
            groupBox45.Padding = new Padding(4, 3, 4, 3);
            groupBox45.Size = new Size(499, 97);
            groupBox45.TabIndex = 75;
            groupBox45.TabStop = false;
            groupBox45.Text = "Time Limits";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(7, 19);
            label3.Name = "label3";
            label3.Size = new Size(455, 15);
            label3.TabIndex = 73;
            label3.Text = "How long does it take to the fog to change from one value to other (time in seconds)";
            // 
            // FTLmaxNUD
            // 
            FTLmaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            FTLmaxNUD.ForeColor = SystemColors.Control;
            FTLmaxNUD.Location = new Point(112, 59);
            FTLmaxNUD.Margin = new Padding(4, 3, 4, 3);
            FTLmaxNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            FTLmaxNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            FTLmaxNUD.Name = "FTLmaxNUD";
            FTLmaxNUD.Size = new Size(91, 23);
            FTLmaxNUD.TabIndex = 72;
            FTLmaxNUD.TextAlign = HorizontalAlignment.Center;
            FTLmaxNUD.ValueChanged += FTLmaxNUD_ValueChanged;
            // 
            // FTLminNUD
            // 
            FTLminNUD.BackColor = Color.FromArgb(60, 63, 65);
            FTLminNUD.ForeColor = SystemColors.Control;
            FTLminNUD.Location = new Point(13, 59);
            FTLminNUD.Margin = new Padding(4, 3, 4, 3);
            FTLminNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            FTLminNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            FTLminNUD.Name = "FTLminNUD";
            FTLminNUD.Size = new Size(91, 23);
            FTLminNUD.TabIndex = 71;
            FTLminNUD.TextAlign = HorizontalAlignment.Center;
            FTLminNUD.ValueChanged += FTLminNUD_ValueChanged;
            // 
            // label74
            // 
            label74.AutoSize = true;
            label74.Location = new Point(135, 40);
            label74.Margin = new Padding(4, 0, 4, 0);
            label74.Name = "label74";
            label74.Size = new Size(29, 15);
            label74.TabIndex = 1;
            label74.Text = "max";
            // 
            // label75
            // 
            label75.AutoSize = true;
            label75.Location = new Point(40, 40);
            label75.Margin = new Padding(4, 0, 4, 0);
            label75.Name = "label75";
            label75.Size = new Size(28, 15);
            label75.TabIndex = 0;
            label75.Text = "min";
            // 
            // groupBox46
            // 
            groupBox46.Controls.Add(label2);
            groupBox46.Controls.Add(FLmaxNUD);
            groupBox46.Controls.Add(FLminNUD);
            groupBox46.Controls.Add(label76);
            groupBox46.Controls.Add(label77);
            groupBox46.ForeColor = SystemColors.Control;
            groupBox46.Location = new Point(7, 125);
            groupBox46.Margin = new Padding(4, 3, 4, 3);
            groupBox46.Name = "groupBox46";
            groupBox46.Padding = new Padding(4, 3, 4, 3);
            groupBox46.Size = new Size(498, 97);
            groupBox46.TabIndex = 74;
            groupBox46.TabStop = false;
            groupBox46.Text = "Limits";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 19);
            label2.Name = "label2";
            label2.Size = new Size(214, 15);
            label2.TabIndex = 74;
            label2.Text = "What is the range of the fog value (0..1)\r\n";
            // 
            // FLmaxNUD
            // 
            FLmaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            FLmaxNUD.DecimalPlaces = 2;
            FLmaxNUD.ForeColor = SystemColors.Control;
            FLmaxNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            FLmaxNUD.Location = new Point(112, 59);
            FLmaxNUD.Margin = new Padding(4, 3, 4, 3);
            FLmaxNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            FLmaxNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            FLmaxNUD.Name = "FLmaxNUD";
            FLmaxNUD.Size = new Size(91, 23);
            FLmaxNUD.TabIndex = 72;
            FLmaxNUD.TextAlign = HorizontalAlignment.Center;
            FLmaxNUD.ValueChanged += FLmaxNUD_ValueChanged;
            // 
            // FLminNUD
            // 
            FLminNUD.BackColor = Color.FromArgb(60, 63, 65);
            FLminNUD.DecimalPlaces = 2;
            FLminNUD.ForeColor = SystemColors.Control;
            FLminNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            FLminNUD.Location = new Point(13, 59);
            FLminNUD.Margin = new Padding(4, 3, 4, 3);
            FLminNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            FLminNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            FLminNUD.Name = "FLminNUD";
            FLminNUD.Size = new Size(91, 23);
            FLminNUD.TabIndex = 71;
            FLminNUD.TextAlign = HorizontalAlignment.Center;
            FLminNUD.ValueChanged += FLminNUD_ValueChanged;
            // 
            // label76
            // 
            label76.AutoSize = true;
            label76.Location = new Point(135, 40);
            label76.Margin = new Padding(4, 0, 4, 0);
            label76.Name = "label76";
            label76.Size = new Size(29, 15);
            label76.TabIndex = 1;
            label76.Text = "max";
            // 
            // label77
            // 
            label77.AutoSize = true;
            label77.Location = new Point(40, 40);
            label77.Margin = new Padding(4, 0, 4, 0);
            label77.Name = "label77";
            label77.Size = new Size(28, 15);
            label77.TabIndex = 0;
            label77.Text = "min";
            // 
            // groupBox47
            // 
            groupBox47.Controls.Add(label1);
            groupBox47.Controls.Add(FCdurationNUD);
            groupBox47.Controls.Add(FCtimeNUD);
            groupBox47.Controls.Add(FCactualNUD);
            groupBox47.Controls.Add(label78);
            groupBox47.Controls.Add(label79);
            groupBox47.Controls.Add(label80);
            groupBox47.ForeColor = SystemColors.Control;
            groupBox47.Location = new Point(7, 22);
            groupBox47.Margin = new Padding(4, 3, 4, 3);
            groupBox47.Name = "groupBox47";
            groupBox47.Padding = new Padding(4, 3, 4, 3);
            groupBox47.Size = new Size(499, 97);
            groupBox47.TabIndex = 4;
            groupBox47.TabStop = false;
            groupBox47.Text = "Current";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 19);
            label1.Name = "label1";
            label1.Size = new Size(420, 15);
            label1.TabIndex = 74;
            label1.Text = "Initial conditions of the fog (target value, time to change, how long will it stay)";
            // 
            // FCdurationNUD
            // 
            FCdurationNUD.BackColor = Color.FromArgb(60, 63, 65);
            FCdurationNUD.ForeColor = SystemColors.Control;
            FCdurationNUD.Location = new Point(211, 59);
            FCdurationNUD.Margin = new Padding(4, 3, 4, 3);
            FCdurationNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            FCdurationNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            FCdurationNUD.Name = "FCdurationNUD";
            FCdurationNUD.Size = new Size(91, 23);
            FCdurationNUD.TabIndex = 73;
            FCdurationNUD.TextAlign = HorizontalAlignment.Center;
            FCdurationNUD.ValueChanged += FCdurationNUD_ValueChanged;
            // 
            // FCtimeNUD
            // 
            FCtimeNUD.BackColor = Color.FromArgb(60, 63, 65);
            FCtimeNUD.ForeColor = SystemColors.Control;
            FCtimeNUD.Location = new Point(112, 59);
            FCtimeNUD.Margin = new Padding(4, 3, 4, 3);
            FCtimeNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            FCtimeNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            FCtimeNUD.Name = "FCtimeNUD";
            FCtimeNUD.Size = new Size(91, 23);
            FCtimeNUD.TabIndex = 72;
            FCtimeNUD.TextAlign = HorizontalAlignment.Center;
            FCtimeNUD.ValueChanged += FCtimeNUD_ValueChanged;
            // 
            // FCactualNUD
            // 
            FCactualNUD.BackColor = Color.FromArgb(60, 63, 65);
            FCactualNUD.DecimalPlaces = 2;
            FCactualNUD.ForeColor = SystemColors.Control;
            FCactualNUD.Location = new Point(13, 59);
            FCactualNUD.Margin = new Padding(4, 3, 4, 3);
            FCactualNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            FCactualNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            FCactualNUD.Name = "FCactualNUD";
            FCactualNUD.Size = new Size(91, 23);
            FCactualNUD.TabIndex = 71;
            FCactualNUD.TextAlign = HorizontalAlignment.Center;
            FCactualNUD.ValueChanged += FCactualNUD_ValueChanged;
            // 
            // label78
            // 
            label78.AutoSize = true;
            label78.Location = new Point(227, 40);
            label78.Margin = new Padding(4, 0, 4, 0);
            label78.Name = "label78";
            label78.Size = new Size(52, 15);
            label78.TabIndex = 2;
            label78.Text = "duration";
            // 
            // label79
            // 
            label79.AutoSize = true;
            label79.Location = new Point(133, 40);
            label79.Margin = new Padding(4, 0, 4, 0);
            label79.Name = "label79";
            label79.Size = new Size(31, 15);
            label79.TabIndex = 1;
            label79.Text = "time";
            // 
            // label80
            // 
            label80.AutoSize = true;
            label80.Location = new Point(35, 40);
            label80.Margin = new Padding(4, 0, 4, 0);
            label80.Name = "label80";
            label80.Size = new Size(39, 15);
            label80.TabIndex = 0;
            label80.Text = "actual";
            // 
            // cfgweatherFogControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox43);
            ForeColor = SystemColors.Control;
            Name = "cfgweatherFogControl";
            Size = new Size(526, 448);
            groupBox43.ResumeLayout(false);
            groupBox44.ResumeLayout(false);
            groupBox44.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)FCLmaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)FCLminNUD).EndInit();
            groupBox45.ResumeLayout(false);
            groupBox45.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)FTLmaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)FTLminNUD).EndInit();
            groupBox46.ResumeLayout(false);
            groupBox46.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)FLmaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)FLminNUD).EndInit();
            groupBox47.ResumeLayout(false);
            groupBox47.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)FCdurationNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)FCtimeNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)FCactualNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox43;
        private GroupBox groupBox44;
        private NumericUpDown FCLmaxNUD;
        private NumericUpDown FCLminNUD;
        private Label label72;
        private Label label73;
        private GroupBox groupBox45;
        private NumericUpDown FTLmaxNUD;
        private NumericUpDown FTLminNUD;
        private Label label74;
        private Label label75;
        private GroupBox groupBox46;
        private NumericUpDown FLmaxNUD;
        private NumericUpDown FLminNUD;
        private Label label76;
        private Label label77;
        private GroupBox groupBox47;
        private NumericUpDown FCdurationNUD;
        private NumericUpDown FCtimeNUD;
        private NumericUpDown FCactualNUD;
        private Label label78;
        private Label label79;
        private Label label80;
        private Label label1;
        private Label label4;
        private Label label3;
        private Label label2;
    }
}
