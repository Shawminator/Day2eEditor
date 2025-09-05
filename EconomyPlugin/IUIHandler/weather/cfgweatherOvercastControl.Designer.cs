namespace EconomyPlugin
{
    partial class cfgweatherOvercastControl
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
            groupBox38 = new GroupBox();
            groupBox42 = new GroupBox();
            label4 = new Label();
            OCLmaxNUD = new NumericUpDown();
            OCLminNUD = new NumericUpDown();
            label70 = new Label();
            label71 = new Label();
            groupBox41 = new GroupBox();
            label3 = new Label();
            OTLmaxNUD = new NumericUpDown();
            OTLminNUD = new NumericUpDown();
            label66 = new Label();
            label69 = new Label();
            groupBox40 = new GroupBox();
            label2 = new Label();
            OLmaxNUD = new NumericUpDown();
            OLminNUD = new NumericUpDown();
            label67 = new Label();
            label68 = new Label();
            groupBox39 = new GroupBox();
            label1 = new Label();
            OCdurationNUD = new NumericUpDown();
            OCtimeNUD = new NumericUpDown();
            OCactualNUD = new NumericUpDown();
            label65 = new Label();
            label62 = new Label();
            label61 = new Label();
            groupBox38.SuspendLayout();
            groupBox42.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)OCLmaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)OCLminNUD).BeginInit();
            groupBox41.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)OTLmaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)OTLminNUD).BeginInit();
            groupBox40.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)OLmaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)OLminNUD).BeginInit();
            groupBox39.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)OCdurationNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)OCtimeNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)OCactualNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox38
            // 
            groupBox38.Controls.Add(groupBox42);
            groupBox38.Controls.Add(groupBox41);
            groupBox38.Controls.Add(groupBox40);
            groupBox38.Controls.Add(groupBox39);
            groupBox38.ForeColor = SystemColors.Control;
            groupBox38.Location = new Point(0, 0);
            groupBox38.Margin = new Padding(4, 3, 4, 3);
            groupBox38.Name = "groupBox38";
            groupBox38.Padding = new Padding(4, 3, 4, 3);
            groupBox38.Size = new Size(517, 440);
            groupBox38.TabIndex = 81;
            groupBox38.TabStop = false;
            groupBox38.Text = "Overcast";
            // 
            // groupBox42
            // 
            groupBox42.Controls.Add(label4);
            groupBox42.Controls.Add(OCLmaxNUD);
            groupBox42.Controls.Add(OCLminNUD);
            groupBox42.Controls.Add(label70);
            groupBox42.Controls.Add(label71);
            groupBox42.ForeColor = SystemColors.Control;
            groupBox42.Location = new Point(7, 331);
            groupBox42.Margin = new Padding(4, 3, 4, 3);
            groupBox42.Name = "groupBox42";
            groupBox42.Padding = new Padding(4, 3, 4, 3);
            groupBox42.Size = new Size(499, 97);
            groupBox42.TabIndex = 75;
            groupBox42.TabStop = false;
            groupBox42.Text = "Change Limits";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(7, 19);
            label4.Name = "label4";
            label4.Size = new Size(243, 15);
            label4.TabIndex = 73;
            label4.Text = "How much should the overcast change (0..1)";
            // 
            // OCLmaxNUD
            // 
            OCLmaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            OCLmaxNUD.DecimalPlaces = 2;
            OCLmaxNUD.ForeColor = SystemColors.Control;
            OCLmaxNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            OCLmaxNUD.Location = new Point(112, 59);
            OCLmaxNUD.Margin = new Padding(4, 3, 4, 3);
            OCLmaxNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            OCLmaxNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            OCLmaxNUD.Name = "OCLmaxNUD";
            OCLmaxNUD.Size = new Size(91, 23);
            OCLmaxNUD.TabIndex = 72;
            OCLmaxNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // OCLminNUD
            // 
            OCLminNUD.BackColor = Color.FromArgb(60, 63, 65);
            OCLminNUD.DecimalPlaces = 2;
            OCLminNUD.ForeColor = SystemColors.Control;
            OCLminNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            OCLminNUD.Location = new Point(13, 59);
            OCLminNUD.Margin = new Padding(4, 3, 4, 3);
            OCLminNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            OCLminNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            OCLminNUD.Name = "OCLminNUD";
            OCLminNUD.Size = new Size(91, 23);
            OCLminNUD.TabIndex = 71;
            OCLminNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // label70
            // 
            label70.AutoSize = true;
            label70.Location = new Point(135, 40);
            label70.Margin = new Padding(4, 0, 4, 0);
            label70.Name = "label70";
            label70.Size = new Size(29, 15);
            label70.TabIndex = 1;
            label70.Text = "max";
            // 
            // label71
            // 
            label71.AutoSize = true;
            label71.Location = new Point(40, 40);
            label71.Margin = new Padding(4, 0, 4, 0);
            label71.Name = "label71";
            label71.Size = new Size(28, 15);
            label71.TabIndex = 0;
            label71.Text = "min";
            // 
            // groupBox41
            // 
            groupBox41.Controls.Add(label3);
            groupBox41.Controls.Add(OTLmaxNUD);
            groupBox41.Controls.Add(OTLminNUD);
            groupBox41.Controls.Add(label66);
            groupBox41.Controls.Add(label69);
            groupBox41.ForeColor = SystemColors.Control;
            groupBox41.Location = new Point(7, 228);
            groupBox41.Margin = new Padding(4, 3, 4, 3);
            groupBox41.Name = "groupBox41";
            groupBox41.Padding = new Padding(4, 3, 4, 3);
            groupBox41.Size = new Size(499, 97);
            groupBox41.TabIndex = 75;
            groupBox41.TabStop = false;
            groupBox41.Text = "Time Limits";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(7, 19);
            label3.Name = "label3";
            label3.Size = new Size(481, 15);
            label3.TabIndex = 73;
            label3.Text = "How long does it take to the overcast to change from one value to other (time in seconds)";
            // 
            // OTLmaxNUD
            // 
            OTLmaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            OTLmaxNUD.ForeColor = SystemColors.Control;
            OTLmaxNUD.Location = new Point(112, 59);
            OTLmaxNUD.Margin = new Padding(4, 3, 4, 3);
            OTLmaxNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            OTLmaxNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            OTLmaxNUD.Name = "OTLmaxNUD";
            OTLmaxNUD.Size = new Size(91, 23);
            OTLmaxNUD.TabIndex = 72;
            OTLmaxNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // OTLminNUD
            // 
            OTLminNUD.BackColor = Color.FromArgb(60, 63, 65);
            OTLminNUD.ForeColor = SystemColors.Control;
            OTLminNUD.Location = new Point(13, 59);
            OTLminNUD.Margin = new Padding(4, 3, 4, 3);
            OTLminNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            OTLminNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            OTLminNUD.Name = "OTLminNUD";
            OTLminNUD.Size = new Size(91, 23);
            OTLminNUD.TabIndex = 71;
            OTLminNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // label66
            // 
            label66.AutoSize = true;
            label66.Location = new Point(135, 40);
            label66.Margin = new Padding(4, 0, 4, 0);
            label66.Name = "label66";
            label66.Size = new Size(29, 15);
            label66.TabIndex = 1;
            label66.Text = "max";
            // 
            // label69
            // 
            label69.AutoSize = true;
            label69.Location = new Point(40, 40);
            label69.Margin = new Padding(4, 0, 4, 0);
            label69.Name = "label69";
            label69.Size = new Size(28, 15);
            label69.TabIndex = 0;
            label69.Text = "min";
            // 
            // groupBox40
            // 
            groupBox40.Controls.Add(label2);
            groupBox40.Controls.Add(OLmaxNUD);
            groupBox40.Controls.Add(OLminNUD);
            groupBox40.Controls.Add(label67);
            groupBox40.Controls.Add(label68);
            groupBox40.ForeColor = SystemColors.Control;
            groupBox40.Location = new Point(7, 125);
            groupBox40.Margin = new Padding(4, 3, 4, 3);
            groupBox40.Name = "groupBox40";
            groupBox40.Padding = new Padding(4, 3, 4, 3);
            groupBox40.Size = new Size(498, 97);
            groupBox40.TabIndex = 74;
            groupBox40.TabStop = false;
            groupBox40.Text = "Limits";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 19);
            label2.Name = "label2";
            label2.Size = new Size(240, 15);
            label2.TabIndex = 73;
            label2.Text = "What is the range of the overcast value (0..1)";
            // 
            // OLmaxNUD
            // 
            OLmaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            OLmaxNUD.DecimalPlaces = 2;
            OLmaxNUD.ForeColor = SystemColors.Control;
            OLmaxNUD.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            OLmaxNUD.Location = new Point(112, 59);
            OLmaxNUD.Margin = new Padding(4, 3, 4, 3);
            OLmaxNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            OLmaxNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            OLmaxNUD.Name = "OLmaxNUD";
            OLmaxNUD.Size = new Size(91, 23);
            OLmaxNUD.TabIndex = 72;
            OLmaxNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // OLminNUD
            // 
            OLminNUD.BackColor = Color.FromArgb(60, 63, 65);
            OLminNUD.DecimalPlaces = 2;
            OLminNUD.ForeColor = SystemColors.Control;
            OLminNUD.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            OLminNUD.Location = new Point(13, 59);
            OLminNUD.Margin = new Padding(4, 3, 4, 3);
            OLminNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            OLminNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            OLminNUD.Name = "OLminNUD";
            OLminNUD.Size = new Size(91, 23);
            OLminNUD.TabIndex = 71;
            OLminNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // label67
            // 
            label67.AutoSize = true;
            label67.Location = new Point(135, 40);
            label67.Margin = new Padding(4, 0, 4, 0);
            label67.Name = "label67";
            label67.Size = new Size(29, 15);
            label67.TabIndex = 1;
            label67.Text = "max";
            // 
            // label68
            // 
            label68.AutoSize = true;
            label68.Location = new Point(40, 40);
            label68.Margin = new Padding(4, 0, 4, 0);
            label68.Name = "label68";
            label68.Size = new Size(28, 15);
            label68.TabIndex = 0;
            label68.Text = "min";
            // 
            // groupBox39
            // 
            groupBox39.Controls.Add(label1);
            groupBox39.Controls.Add(OCdurationNUD);
            groupBox39.Controls.Add(OCtimeNUD);
            groupBox39.Controls.Add(OCactualNUD);
            groupBox39.Controls.Add(label65);
            groupBox39.Controls.Add(label62);
            groupBox39.Controls.Add(label61);
            groupBox39.ForeColor = SystemColors.Control;
            groupBox39.Location = new Point(7, 22);
            groupBox39.Margin = new Padding(4, 3, 4, 3);
            groupBox39.Name = "groupBox39";
            groupBox39.Padding = new Padding(4, 3, 4, 3);
            groupBox39.Size = new Size(499, 97);
            groupBox39.TabIndex = 4;
            groupBox39.TabStop = false;
            groupBox39.Text = "Current";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 19);
            label1.Name = "label1";
            label1.Size = new Size(446, 15);
            label1.TabIndex = 74;
            label1.Text = "Initial conditions of the overcast (target value, time to change, how long will it stay)";
            // 
            // OCdurationNUD
            // 
            OCdurationNUD.BackColor = Color.FromArgb(60, 63, 65);
            OCdurationNUD.ForeColor = SystemColors.Control;
            OCdurationNUD.Location = new Point(211, 59);
            OCdurationNUD.Margin = new Padding(4, 3, 4, 3);
            OCdurationNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            OCdurationNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            OCdurationNUD.Name = "OCdurationNUD";
            OCdurationNUD.Size = new Size(91, 23);
            OCdurationNUD.TabIndex = 73;
            OCdurationNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // OCtimeNUD
            // 
            OCtimeNUD.BackColor = Color.FromArgb(60, 63, 65);
            OCtimeNUD.ForeColor = SystemColors.Control;
            OCtimeNUD.Location = new Point(112, 59);
            OCtimeNUD.Margin = new Padding(4, 3, 4, 3);
            OCtimeNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            OCtimeNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            OCtimeNUD.Name = "OCtimeNUD";
            OCtimeNUD.Size = new Size(91, 23);
            OCtimeNUD.TabIndex = 72;
            OCtimeNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // OCactualNUD
            // 
            OCactualNUD.BackColor = Color.FromArgb(60, 63, 65);
            OCactualNUD.DecimalPlaces = 2;
            OCactualNUD.ForeColor = SystemColors.Control;
            OCactualNUD.Location = new Point(13, 59);
            OCactualNUD.Margin = new Padding(4, 3, 4, 3);
            OCactualNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            OCactualNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            OCactualNUD.Name = "OCactualNUD";
            OCactualNUD.Size = new Size(91, 23);
            OCactualNUD.TabIndex = 71;
            OCactualNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // label65
            // 
            label65.AutoSize = true;
            label65.Location = new Point(227, 40);
            label65.Margin = new Padding(4, 0, 4, 0);
            label65.Name = "label65";
            label65.Size = new Size(52, 15);
            label65.TabIndex = 2;
            label65.Text = "duration";
            // 
            // label62
            // 
            label62.AutoSize = true;
            label62.Location = new Point(133, 40);
            label62.Margin = new Padding(4, 0, 4, 0);
            label62.Name = "label62";
            label62.Size = new Size(31, 15);
            label62.TabIndex = 1;
            label62.Text = "time";
            // 
            // label61
            // 
            label61.AutoSize = true;
            label61.Location = new Point(35, 40);
            label61.Margin = new Padding(4, 0, 4, 0);
            label61.Name = "label61";
            label61.Size = new Size(39, 15);
            label61.TabIndex = 0;
            label61.Text = "actual";
            // 
            // cfgweatherOvercastControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox38);
            ForeColor = SystemColors.Control;
            Name = "cfgweatherOvercastControl";
            Size = new Size(526, 448);
            groupBox38.ResumeLayout(false);
            groupBox42.ResumeLayout(false);
            groupBox42.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)OCLmaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)OCLminNUD).EndInit();
            groupBox41.ResumeLayout(false);
            groupBox41.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)OTLmaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)OTLminNUD).EndInit();
            groupBox40.ResumeLayout(false);
            groupBox40.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)OLmaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)OLminNUD).EndInit();
            groupBox39.ResumeLayout(false);
            groupBox39.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)OCdurationNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)OCtimeNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)OCactualNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox38;
        private GroupBox groupBox42;
        private NumericUpDown OCLmaxNUD;
        private NumericUpDown OCLminNUD;
        private Label label70;
        private Label label71;
        private GroupBox groupBox41;
        private NumericUpDown OTLmaxNUD;
        private NumericUpDown OTLminNUD;
        private Label label66;
        private Label label69;
        private GroupBox groupBox40;
        private NumericUpDown OLmaxNUD;
        private NumericUpDown OLminNUD;
        private Label label67;
        private Label label68;
        private GroupBox groupBox39;
        private NumericUpDown OCdurationNUD;
        private NumericUpDown OCtimeNUD;
        private NumericUpDown OCactualNUD;
        private Label label65;
        private Label label62;
        private Label label61;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}
