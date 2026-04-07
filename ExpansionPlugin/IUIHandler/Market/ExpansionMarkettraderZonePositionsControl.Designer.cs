namespace ExpansionPlugin
{
    partial class ExpansionMarkettraderZonePositionsControl
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
            groupBox1 = new GroupBox();
            ZoneZNUD = new NumericUpDown();
            ZoneYNUD = new NumericUpDown();
            ZoneXNUD = new NumericUpDown();
            darkLabel41 = new Label();
            ZoneRadiusNUD = new NumericUpDown();
            darkLabel40 = new Label();
            darkLabel42 = new Label();
            darkLabel43 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ZoneZNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ZoneYNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ZoneXNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ZoneRadiusNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ZoneZNUD);
            groupBox1.Controls.Add(ZoneYNUD);
            groupBox1.Controls.Add(ZoneXNUD);
            groupBox1.Controls.Add(darkLabel41);
            groupBox1.Controls.Add(ZoneRadiusNUD);
            groupBox1.Controls.Add(darkLabel40);
            groupBox1.Controls.Add(darkLabel42);
            groupBox1.Controls.Add(darkLabel43);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(270, 149);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Trader Zone";
            // 
            // ZoneZNUD
            // 
            ZoneZNUD.BackColor = Color.FromArgb(60, 63, 65);
            ZoneZNUD.DecimalPlaces = 3;
            ZoneZNUD.ForeColor = SystemColors.Control;
            ZoneZNUD.Location = new Point(101, 82);
            ZoneZNUD.Margin = new Padding(4, 3, 4, 3);
            ZoneZNUD.Maximum = new decimal(new int[] { 50000, 0, 0, 0 });
            ZoneZNUD.Minimum = new decimal(new int[] { 50000, 0, 0, int.MinValue });
            ZoneZNUD.Name = "ZoneZNUD";
            ZoneZNUD.Size = new Size(148, 23);
            ZoneZNUD.TabIndex = 16;
            ZoneZNUD.TextAlign = HorizontalAlignment.Center;
            ZoneZNUD.ValueChanged += ZoneZNUD_ValueChanged;
            // 
            // ZoneYNUD
            // 
            ZoneYNUD.BackColor = Color.FromArgb(60, 63, 65);
            ZoneYNUD.DecimalPlaces = 3;
            ZoneYNUD.ForeColor = SystemColors.Control;
            ZoneYNUD.Location = new Point(101, 52);
            ZoneYNUD.Margin = new Padding(4, 3, 4, 3);
            ZoneYNUD.Maximum = new decimal(new int[] { 50000, 0, 0, 0 });
            ZoneYNUD.Minimum = new decimal(new int[] { 50000, 0, 0, int.MinValue });
            ZoneYNUD.Name = "ZoneYNUD";
            ZoneYNUD.Size = new Size(148, 23);
            ZoneYNUD.TabIndex = 14;
            ZoneYNUD.TextAlign = HorizontalAlignment.Center;
            ZoneYNUD.ValueChanged += ZoneYNUD_ValueChanged;
            // 
            // ZoneXNUD
            // 
            ZoneXNUD.BackColor = Color.FromArgb(60, 63, 65);
            ZoneXNUD.DecimalPlaces = 3;
            ZoneXNUD.ForeColor = SystemColors.Control;
            ZoneXNUD.Location = new Point(101, 23);
            ZoneXNUD.Margin = new Padding(4, 3, 4, 3);
            ZoneXNUD.Maximum = new decimal(new int[] { 50000, 0, 0, 0 });
            ZoneXNUD.Minimum = new decimal(new int[] { 50000, 0, 0, int.MinValue });
            ZoneXNUD.Name = "ZoneXNUD";
            ZoneXNUD.Size = new Size(148, 23);
            ZoneXNUD.TabIndex = 12;
            ZoneXNUD.TextAlign = HorizontalAlignment.Center;
            ZoneXNUD.ValueChanged += ZoneXNUD_ValueChanged;
            // 
            // darkLabel41
            // 
            darkLabel41.AutoSize = true;
            darkLabel41.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel41.Location = new Point(14, 25);
            darkLabel41.Margin = new Padding(4, 0, 4, 0);
            darkLabel41.Name = "darkLabel41";
            darkLabel41.Size = new Size(60, 15);
            darkLabel41.TabIndex = 11;
            darkLabel41.Text = "Center X -";
            // 
            // ZoneRadiusNUD
            // 
            ZoneRadiusNUD.BackColor = Color.FromArgb(60, 63, 65);
            ZoneRadiusNUD.DecimalPlaces = 3;
            ZoneRadiusNUD.ForeColor = SystemColors.Control;
            ZoneRadiusNUD.Location = new Point(101, 112);
            ZoneRadiusNUD.Margin = new Padding(4, 3, 4, 3);
            ZoneRadiusNUD.Maximum = new decimal(new int[] { 50000, 0, 0, 0 });
            ZoneRadiusNUD.Name = "ZoneRadiusNUD";
            ZoneRadiusNUD.Size = new Size(148, 23);
            ZoneRadiusNUD.TabIndex = 18;
            ZoneRadiusNUD.TextAlign = HorizontalAlignment.Center;
            ZoneRadiusNUD.ValueChanged += ZoneRadiusNUD_ValueChanged;
            // 
            // darkLabel40
            // 
            darkLabel40.AutoSize = true;
            darkLabel40.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel40.Location = new Point(14, 115);
            darkLabel40.Margin = new Padding(4, 0, 4, 0);
            darkLabel40.Name = "darkLabel40";
            darkLabel40.Size = new Size(42, 15);
            darkLabel40.TabIndex = 17;
            darkLabel40.Text = "Radius";
            // 
            // darkLabel42
            // 
            darkLabel42.AutoSize = true;
            darkLabel42.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel42.Location = new Point(14, 55);
            darkLabel42.Margin = new Padding(4, 0, 4, 0);
            darkLabel42.Name = "darkLabel42";
            darkLabel42.Size = new Size(60, 15);
            darkLabel42.TabIndex = 13;
            darkLabel42.Text = "Center Y -";
            // 
            // darkLabel43
            // 
            darkLabel43.AutoSize = true;
            darkLabel43.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel43.Location = new Point(14, 85);
            darkLabel43.Margin = new Padding(4, 0, 4, 0);
            darkLabel43.Name = "darkLabel43";
            darkLabel43.Size = new Size(57, 15);
            darkLabel43.TabIndex = 15;
            darkLabel43.Text = "Center Z-";
            // 
            // ExpansionMarkettraderZonePositionsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionMarkettraderZonePositionsControl";
            Size = new Size(270, 149);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ZoneZNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)ZoneYNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)ZoneXNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)ZoneRadiusNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private NumericUpDown ZoneZNUD;
        private NumericUpDown ZoneYNUD;
        private NumericUpDown ZoneXNUD;
        private Label darkLabel41;
        private NumericUpDown ZoneRadiusNUD;
        private Label darkLabel40;
        private Label darkLabel42;
        private Label darkLabel43;
    }
}
