namespace EconomyPlugin
{
    partial class cfgweatherStormControl
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
            groupBox55 = new GroupBox();
            label94 = new Label();
            StimeoutNUD = new NumericUpDown();
            SdensityNUD = new NumericUpDown();
            SthresholdNUD = new NumericUpDown();
            label96 = new Label();
            label95 = new Label();
            groupBox55.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)StimeoutNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SdensityNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SthresholdNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox55
            // 
            groupBox55.Controls.Add(label94);
            groupBox55.Controls.Add(StimeoutNUD);
            groupBox55.Controls.Add(SdensityNUD);
            groupBox55.Controls.Add(SthresholdNUD);
            groupBox55.Controls.Add(label96);
            groupBox55.Controls.Add(label95);
            groupBox55.ForeColor = SystemColors.Control;
            groupBox55.Location = new Point(0, 0);
            groupBox55.Margin = new Padding(4, 3, 4, 3);
            groupBox55.Name = "groupBox55";
            groupBox55.Padding = new Padding(4, 3, 4, 3);
            groupBox55.Size = new Size(215, 138);
            groupBox55.TabIndex = 85;
            groupBox55.TabStop = false;
            groupBox55.Text = "Storm";
            // 
            // label94
            // 
            label94.AutoSize = true;
            label94.Location = new Point(17, 99);
            label94.Margin = new Padding(4, 0, 4, 0);
            label94.Name = "label94";
            label94.Size = new Size(49, 15);
            label94.TabIndex = 74;
            label94.Text = "timeout";
            // 
            // StimeoutNUD
            // 
            StimeoutNUD.BackColor = Color.FromArgb(60, 63, 65);
            StimeoutNUD.ForeColor = SystemColors.Control;
            StimeoutNUD.Location = new Point(106, 97);
            StimeoutNUD.Margin = new Padding(4, 3, 4, 3);
            StimeoutNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            StimeoutNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            StimeoutNUD.Name = "StimeoutNUD";
            StimeoutNUD.Size = new Size(91, 23);
            StimeoutNUD.TabIndex = 73;
            StimeoutNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // SdensityNUD
            // 
            SdensityNUD.BackColor = Color.FromArgb(60, 63, 65);
            SdensityNUD.DecimalPlaces = 2;
            SdensityNUD.ForeColor = SystemColors.Control;
            SdensityNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            SdensityNUD.Location = new Point(106, 28);
            SdensityNUD.Margin = new Padding(4, 3, 4, 3);
            SdensityNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            SdensityNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            SdensityNUD.Name = "SdensityNUD";
            SdensityNUD.Size = new Size(91, 23);
            SdensityNUD.TabIndex = 71;
            SdensityNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // SthresholdNUD
            // 
            SthresholdNUD.BackColor = Color.FromArgb(60, 63, 65);
            SthresholdNUD.DecimalPlaces = 2;
            SthresholdNUD.ForeColor = SystemColors.Control;
            SthresholdNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            SthresholdNUD.Location = new Point(106, 62);
            SthresholdNUD.Margin = new Padding(4, 3, 4, 3);
            SthresholdNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            SthresholdNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            SthresholdNUD.Name = "SthresholdNUD";
            SthresholdNUD.Size = new Size(91, 23);
            SthresholdNUD.TabIndex = 72;
            SthresholdNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // label96
            // 
            label96.AutoSize = true;
            label96.Location = new Point(17, 30);
            label96.Margin = new Padding(4, 0, 4, 0);
            label96.Name = "label96";
            label96.Size = new Size(45, 15);
            label96.TabIndex = 0;
            label96.Text = "density";
            // 
            // label95
            // 
            label95.AutoSize = true;
            label95.Location = new Point(17, 65);
            label95.Margin = new Padding(4, 0, 4, 0);
            label95.Name = "label95";
            label95.Size = new Size(57, 15);
            label95.TabIndex = 1;
            label95.Text = "threshold";
            // 
            // cfgweatherStormControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox55);
            ForeColor = SystemColors.Control;
            Name = "cfgweatherStormControl";
            Size = new Size(224, 143);
            groupBox55.ResumeLayout(false);
            groupBox55.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)StimeoutNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)SdensityNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)SthresholdNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox55;
        private Label label94;
        private NumericUpDown StimeoutNUD;
        private NumericUpDown SdensityNUD;
        private NumericUpDown SthresholdNUD;
        private Label label96;
        private Label label95;
    }
}
