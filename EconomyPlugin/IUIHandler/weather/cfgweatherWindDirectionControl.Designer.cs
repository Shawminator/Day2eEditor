namespace EconomyPlugin
{
    partial class cfgweatherWindDirectionControl
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
            groupBox84 = new GroupBox();
            groupBox85 = new GroupBox();
            label4 = new Label();
            WDCLmaxNUD = new NumericUpDown();
            WDCLminNUD = new NumericUpDown();
            label170 = new Label();
            label171 = new Label();
            groupBox86 = new GroupBox();
            label3 = new Label();
            WDTLmaxNUD = new NumericUpDown();
            WDTLminNUD = new NumericUpDown();
            label172 = new Label();
            label173 = new Label();
            groupBox87 = new GroupBox();
            label2 = new Label();
            WDLmaxNUD = new NumericUpDown();
            WDLminNUD = new NumericUpDown();
            label174 = new Label();
            label175 = new Label();
            groupBox88 = new GroupBox();
            label1 = new Label();
            WDCdurationNUD = new NumericUpDown();
            WDCtimeNUD = new NumericUpDown();
            WDCactualNUD = new NumericUpDown();
            label176 = new Label();
            label177 = new Label();
            label178 = new Label();
            groupBox84.SuspendLayout();
            groupBox85.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WDCLmaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)WDCLminNUD).BeginInit();
            groupBox86.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WDTLmaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)WDTLminNUD).BeginInit();
            groupBox87.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WDLmaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)WDLminNUD).BeginInit();
            groupBox88.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WDCdurationNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)WDCtimeNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)WDCactualNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox84
            // 
            groupBox84.Controls.Add(groupBox85);
            groupBox84.Controls.Add(groupBox86);
            groupBox84.Controls.Add(groupBox87);
            groupBox84.Controls.Add(groupBox88);
            groupBox84.ForeColor = SystemColors.Control;
            groupBox84.Location = new Point(0, 0);
            groupBox84.Margin = new Padding(4, 3, 4, 3);
            groupBox84.Name = "groupBox84";
            groupBox84.Padding = new Padding(4, 3, 4, 3);
            groupBox84.Size = new Size(713, 440);
            groupBox84.TabIndex = 84;
            groupBox84.TabStop = false;
            groupBox84.Text = "Wind Direction";
            // 
            // groupBox85
            // 
            groupBox85.Controls.Add(label4);
            groupBox85.Controls.Add(WDCLmaxNUD);
            groupBox85.Controls.Add(WDCLminNUD);
            groupBox85.Controls.Add(label170);
            groupBox85.Controls.Add(label171);
            groupBox85.ForeColor = SystemColors.Control;
            groupBox85.Location = new Point(7, 331);
            groupBox85.Margin = new Padding(4, 3, 4, 3);
            groupBox85.Name = "groupBox85";
            groupBox85.Padding = new Padding(4, 3, 4, 3);
            groupBox85.Size = new Size(698, 97);
            groupBox85.TabIndex = 75;
            groupBox85.TabStop = false;
            groupBox85.Text = "Change Limits";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(7, 19);
            label4.Name = "label4";
            label4.Size = new Size(246, 15);
            label4.TabIndex = 73;
            label4.Text = "How much should the wind change direction";
            // 
            // WDCLmaxNUD
            // 
            WDCLmaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            WDCLmaxNUD.DecimalPlaces = 2;
            WDCLmaxNUD.ForeColor = SystemColors.Control;
            WDCLmaxNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            WDCLmaxNUD.Location = new Point(112, 59);
            WDCLmaxNUD.Margin = new Padding(4, 3, 4, 3);
            WDCLmaxNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            WDCLmaxNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            WDCLmaxNUD.Name = "WDCLmaxNUD";
            WDCLmaxNUD.Size = new Size(91, 23);
            WDCLmaxNUD.TabIndex = 72;
            WDCLmaxNUD.TextAlign = HorizontalAlignment.Center;
            WDCLmaxNUD.ValueChanged += WDCLmaxNUD_ValueChanged;
            // 
            // WDCLminNUD
            // 
            WDCLminNUD.BackColor = Color.FromArgb(60, 63, 65);
            WDCLminNUD.DecimalPlaces = 2;
            WDCLminNUD.ForeColor = SystemColors.Control;
            WDCLminNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            WDCLminNUD.Location = new Point(13, 59);
            WDCLminNUD.Margin = new Padding(4, 3, 4, 3);
            WDCLminNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            WDCLminNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            WDCLminNUD.Name = "WDCLminNUD";
            WDCLminNUD.Size = new Size(91, 23);
            WDCLminNUD.TabIndex = 71;
            WDCLminNUD.TextAlign = HorizontalAlignment.Center;
            WDCLminNUD.ValueChanged += WDCLminNUD_ValueChanged;
            // 
            // label170
            // 
            label170.AutoSize = true;
            label170.Location = new Point(135, 40);
            label170.Margin = new Padding(4, 0, 4, 0);
            label170.Name = "label170";
            label170.Size = new Size(29, 15);
            label170.TabIndex = 1;
            label170.Text = "max";
            // 
            // label171
            // 
            label171.AutoSize = true;
            label171.Location = new Point(40, 40);
            label171.Margin = new Padding(4, 0, 4, 0);
            label171.Name = "label171";
            label171.Size = new Size(28, 15);
            label171.TabIndex = 0;
            label171.Text = "min";
            // 
            // groupBox86
            // 
            groupBox86.Controls.Add(label3);
            groupBox86.Controls.Add(WDTLmaxNUD);
            groupBox86.Controls.Add(WDTLminNUD);
            groupBox86.Controls.Add(label172);
            groupBox86.Controls.Add(label173);
            groupBox86.ForeColor = SystemColors.Control;
            groupBox86.Location = new Point(7, 228);
            groupBox86.Margin = new Padding(4, 3, 4, 3);
            groupBox86.Name = "groupBox86";
            groupBox86.Padding = new Padding(4, 3, 4, 3);
            groupBox86.Size = new Size(698, 97);
            groupBox86.TabIndex = 75;
            groupBox86.TabStop = false;
            groupBox86.Text = "Time Limits";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(7, 19);
            label3.Name = "label3";
            label3.Size = new Size(513, 15);
            label3.TabIndex = 73;
            label3.Text = "How long does it take to the wind direction to change from one value to other (time in seconds)";
            // 
            // WDTLmaxNUD
            // 
            WDTLmaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            WDTLmaxNUD.ForeColor = SystemColors.Control;
            WDTLmaxNUD.Location = new Point(112, 59);
            WDTLmaxNUD.Margin = new Padding(4, 3, 4, 3);
            WDTLmaxNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            WDTLmaxNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            WDTLmaxNUD.Name = "WDTLmaxNUD";
            WDTLmaxNUD.Size = new Size(91, 23);
            WDTLmaxNUD.TabIndex = 72;
            WDTLmaxNUD.TextAlign = HorizontalAlignment.Center;
            WDTLmaxNUD.ValueChanged += WDTLmaxNUD_ValueChanged;
            // 
            // WDTLminNUD
            // 
            WDTLminNUD.BackColor = Color.FromArgb(60, 63, 65);
            WDTLminNUD.ForeColor = SystemColors.Control;
            WDTLminNUD.Location = new Point(13, 59);
            WDTLminNUD.Margin = new Padding(4, 3, 4, 3);
            WDTLminNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            WDTLminNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            WDTLminNUD.Name = "WDTLminNUD";
            WDTLminNUD.Size = new Size(91, 23);
            WDTLminNUD.TabIndex = 71;
            WDTLminNUD.TextAlign = HorizontalAlignment.Center;
            WDTLminNUD.ValueChanged += WDTLminNUD_ValueChanged;
            // 
            // label172
            // 
            label172.AutoSize = true;
            label172.Location = new Point(135, 40);
            label172.Margin = new Padding(4, 0, 4, 0);
            label172.Name = "label172";
            label172.Size = new Size(29, 15);
            label172.TabIndex = 1;
            label172.Text = "max";
            // 
            // label173
            // 
            label173.AutoSize = true;
            label173.Location = new Point(40, 40);
            label173.Margin = new Padding(4, 0, 4, 0);
            label173.Name = "label173";
            label173.Size = new Size(28, 15);
            label173.TabIndex = 0;
            label173.Text = "min";
            // 
            // groupBox87
            // 
            groupBox87.Controls.Add(label2);
            groupBox87.Controls.Add(WDLmaxNUD);
            groupBox87.Controls.Add(WDLminNUD);
            groupBox87.Controls.Add(label174);
            groupBox87.Controls.Add(label175);
            groupBox87.ForeColor = SystemColors.Control;
            groupBox87.Location = new Point(7, 125);
            groupBox87.Margin = new Padding(4, 3, 4, 3);
            groupBox87.Name = "groupBox87";
            groupBox87.Padding = new Padding(4, 3, 4, 3);
            groupBox87.Size = new Size(698, 97);
            groupBox87.TabIndex = 74;
            groupBox87.TabStop = false;
            groupBox87.Text = "Limits";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 19);
            label2.Name = "label2";
            label2.Size = new Size(302, 15);
            label2.TabIndex = 73;
            label2.Text = "What is the range of the wind direction (angle in radians";
            // 
            // WDLmaxNUD
            // 
            WDLmaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            WDLmaxNUD.DecimalPlaces = 2;
            WDLmaxNUD.ForeColor = SystemColors.Control;
            WDLmaxNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            WDLmaxNUD.Location = new Point(112, 59);
            WDLmaxNUD.Margin = new Padding(4, 3, 4, 3);
            WDLmaxNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            WDLmaxNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            WDLmaxNUD.Name = "WDLmaxNUD";
            WDLmaxNUD.Size = new Size(91, 23);
            WDLmaxNUD.TabIndex = 72;
            WDLmaxNUD.TextAlign = HorizontalAlignment.Center;
            WDLmaxNUD.ValueChanged += WDLmaxNUD_ValueChanged;
            // 
            // WDLminNUD
            // 
            WDLminNUD.BackColor = Color.FromArgb(60, 63, 65);
            WDLminNUD.DecimalPlaces = 2;
            WDLminNUD.ForeColor = SystemColors.Control;
            WDLminNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            WDLminNUD.Location = new Point(13, 59);
            WDLminNUD.Margin = new Padding(4, 3, 4, 3);
            WDLminNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            WDLminNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            WDLminNUD.Name = "WDLminNUD";
            WDLminNUD.Size = new Size(91, 23);
            WDLminNUD.TabIndex = 71;
            WDLminNUD.TextAlign = HorizontalAlignment.Center;
            WDLminNUD.ValueChanged += WDLminNUD_ValueChanged;
            // 
            // label174
            // 
            label174.AutoSize = true;
            label174.Location = new Point(135, 40);
            label174.Margin = new Padding(4, 0, 4, 0);
            label174.Name = "label174";
            label174.Size = new Size(29, 15);
            label174.TabIndex = 1;
            label174.Text = "max";
            // 
            // label175
            // 
            label175.AutoSize = true;
            label175.Location = new Point(40, 40);
            label175.Margin = new Padding(4, 0, 4, 0);
            label175.Name = "label175";
            label175.Size = new Size(28, 15);
            label175.TabIndex = 0;
            label175.Text = "min";
            // 
            // groupBox88
            // 
            groupBox88.Controls.Add(label1);
            groupBox88.Controls.Add(WDCdurationNUD);
            groupBox88.Controls.Add(WDCtimeNUD);
            groupBox88.Controls.Add(WDCactualNUD);
            groupBox88.Controls.Add(label176);
            groupBox88.Controls.Add(label177);
            groupBox88.Controls.Add(label178);
            groupBox88.ForeColor = SystemColors.Control;
            groupBox88.Location = new Point(7, 22);
            groupBox88.Margin = new Padding(4, 3, 4, 3);
            groupBox88.Name = "groupBox88";
            groupBox88.Padding = new Padding(4, 3, 4, 3);
            groupBox88.Size = new Size(698, 97);
            groupBox88.TabIndex = 4;
            groupBox88.TabStop = false;
            groupBox88.Text = "Current";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 19);
            label1.Name = "label1";
            label1.Size = new Size(673, 15);
            label1.TabIndex = 74;
            label1.Text = "Initial conditions of the wind direction(target value, time to change, how long will it stay), restricted by thresholds (see below)1";
            // 
            // WDCdurationNUD
            // 
            WDCdurationNUD.BackColor = Color.FromArgb(60, 63, 65);
            WDCdurationNUD.ForeColor = SystemColors.Control;
            WDCdurationNUD.Location = new Point(211, 59);
            WDCdurationNUD.Margin = new Padding(4, 3, 4, 3);
            WDCdurationNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            WDCdurationNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            WDCdurationNUD.Name = "WDCdurationNUD";
            WDCdurationNUD.Size = new Size(91, 23);
            WDCdurationNUD.TabIndex = 73;
            WDCdurationNUD.TextAlign = HorizontalAlignment.Center;
            WDCdurationNUD.ValueChanged += WDCdurationNUD_ValueChanged;
            // 
            // WDCtimeNUD
            // 
            WDCtimeNUD.BackColor = Color.FromArgb(60, 63, 65);
            WDCtimeNUD.ForeColor = SystemColors.Control;
            WDCtimeNUD.Location = new Point(112, 59);
            WDCtimeNUD.Margin = new Padding(4, 3, 4, 3);
            WDCtimeNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            WDCtimeNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            WDCtimeNUD.Name = "WDCtimeNUD";
            WDCtimeNUD.Size = new Size(91, 23);
            WDCtimeNUD.TabIndex = 72;
            WDCtimeNUD.TextAlign = HorizontalAlignment.Center;
            WDCtimeNUD.ValueChanged += WDCtimeNUD_ValueChanged;
            // 
            // WDCactualNUD
            // 
            WDCactualNUD.BackColor = Color.FromArgb(60, 63, 65);
            WDCactualNUD.DecimalPlaces = 2;
            WDCactualNUD.ForeColor = SystemColors.Control;
            WDCactualNUD.Location = new Point(13, 59);
            WDCactualNUD.Margin = new Padding(4, 3, 4, 3);
            WDCactualNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            WDCactualNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            WDCactualNUD.Name = "WDCactualNUD";
            WDCactualNUD.Size = new Size(91, 23);
            WDCactualNUD.TabIndex = 71;
            WDCactualNUD.TextAlign = HorizontalAlignment.Center;
            WDCactualNUD.ValueChanged += WDCactualNUD_ValueChanged;
            // 
            // label176
            // 
            label176.AutoSize = true;
            label176.Location = new Point(227, 40);
            label176.Margin = new Padding(4, 0, 4, 0);
            label176.Name = "label176";
            label176.Size = new Size(52, 15);
            label176.TabIndex = 2;
            label176.Text = "duration";
            // 
            // label177
            // 
            label177.AutoSize = true;
            label177.Location = new Point(133, 40);
            label177.Margin = new Padding(4, 0, 4, 0);
            label177.Name = "label177";
            label177.Size = new Size(31, 15);
            label177.TabIndex = 1;
            label177.Text = "time";
            // 
            // label178
            // 
            label178.AutoSize = true;
            label178.Location = new Point(35, 40);
            label178.Margin = new Padding(4, 0, 4, 0);
            label178.Name = "label178";
            label178.Size = new Size(39, 15);
            label178.TabIndex = 0;
            label178.Text = "actual";
            // 
            // cfgweatherWindDirectionControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox84);
            ForeColor = SystemColors.Control;
            Name = "cfgweatherWindDirectionControl";
            Size = new Size(723, 449);
            groupBox84.ResumeLayout(false);
            groupBox85.ResumeLayout(false);
            groupBox85.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)WDCLmaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)WDCLminNUD).EndInit();
            groupBox86.ResumeLayout(false);
            groupBox86.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)WDTLmaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)WDTLminNUD).EndInit();
            groupBox87.ResumeLayout(false);
            groupBox87.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)WDLmaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)WDLminNUD).EndInit();
            groupBox88.ResumeLayout(false);
            groupBox88.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)WDCdurationNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)WDCtimeNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)WDCactualNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox84;
        private GroupBox groupBox85;
        private NumericUpDown WDCLmaxNUD;
        private NumericUpDown WDCLminNUD;
        private Label label170;
        private Label label171;
        private GroupBox groupBox86;
        private NumericUpDown WDTLmaxNUD;
        private NumericUpDown WDTLminNUD;
        private Label label172;
        private Label label173;
        private GroupBox groupBox87;
        private NumericUpDown WDLmaxNUD;
        private NumericUpDown WDLminNUD;
        private Label label174;
        private Label label175;
        private GroupBox groupBox88;
        private NumericUpDown WDCdurationNUD;
        private NumericUpDown WDCtimeNUD;
        private NumericUpDown WDCactualNUD;
        private Label label176;
        private Label label177;
        private Label label178;
        private Label label1;
        private Label label4;
        private Label label3;
        private Label label2;
    }
}
