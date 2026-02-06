namespace ExpansionPlugin
{
    partial class ExpansionSafeZoneCylinderControl
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
            CircleZNUD = new NumericUpDown();
            CircleYNUD = new NumericUpDown();
            CircleXNUD = new NumericUpDown();
            darkLabel41 = new Label();
            CircleRadiusNUD = new NumericUpDown();
            darkLabel40 = new Label();
            darkLabel42 = new Label();
            darkLabel43 = new Label();
            CircleHeightNUD = new NumericUpDown();
            darkLabel294 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)CircleZNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CircleYNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CircleXNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CircleRadiusNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CircleHeightNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(CircleHeightNUD);
            groupBox1.Controls.Add(darkLabel294);
            groupBox1.Controls.Add(CircleZNUD);
            groupBox1.Controls.Add(CircleYNUD);
            groupBox1.Controls.Add(CircleXNUD);
            groupBox1.Controls.Add(darkLabel41);
            groupBox1.Controls.Add(CircleRadiusNUD);
            groupBox1.Controls.Add(darkLabel40);
            groupBox1.Controls.Add(darkLabel42);
            groupBox1.Controls.Add(darkLabel43);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(270, 177);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Polygon Zone";
            // 
            // CircleZNUD
            // 
            CircleZNUD.BackColor = Color.FromArgb(60, 63, 65);
            CircleZNUD.DecimalPlaces = 3;
            CircleZNUD.ForeColor = SystemColors.Control;
            CircleZNUD.Location = new Point(101, 82);
            CircleZNUD.Margin = new Padding(4, 3, 4, 3);
            CircleZNUD.Maximum = new decimal(new int[] { 50000, 0, 0, 0 });
            CircleZNUD.Minimum = new decimal(new int[] { 50000, 0, 0, int.MinValue });
            CircleZNUD.Name = "CircleZNUD";
            CircleZNUD.Size = new Size(148, 23);
            CircleZNUD.TabIndex = 16;
            CircleZNUD.TextAlign = HorizontalAlignment.Center;
            CircleZNUD.ValueChanged += CircleZNUD_ValueChanged;
            // 
            // CircleYNUD
            // 
            CircleYNUD.BackColor = Color.FromArgb(60, 63, 65);
            CircleYNUD.DecimalPlaces = 3;
            CircleYNUD.ForeColor = SystemColors.Control;
            CircleYNUD.Location = new Point(101, 52);
            CircleYNUD.Margin = new Padding(4, 3, 4, 3);
            CircleYNUD.Maximum = new decimal(new int[] { 50000, 0, 0, 0 });
            CircleYNUD.Minimum = new decimal(new int[] { 50000, 0, 0, int.MinValue });
            CircleYNUD.Name = "CircleYNUD";
            CircleYNUD.Size = new Size(148, 23);
            CircleYNUD.TabIndex = 14;
            CircleYNUD.TextAlign = HorizontalAlignment.Center;
            CircleYNUD.ValueChanged += CircleYNUD_ValueChanged;
            // 
            // CircleXNUD
            // 
            CircleXNUD.BackColor = Color.FromArgb(60, 63, 65);
            CircleXNUD.DecimalPlaces = 3;
            CircleXNUD.ForeColor = SystemColors.Control;
            CircleXNUD.Location = new Point(101, 23);
            CircleXNUD.Margin = new Padding(4, 3, 4, 3);
            CircleXNUD.Maximum = new decimal(new int[] { 50000, 0, 0, 0 });
            CircleXNUD.Minimum = new decimal(new int[] { 50000, 0, 0, int.MinValue });
            CircleXNUD.Name = "CircleXNUD";
            CircleXNUD.Size = new Size(148, 23);
            CircleXNUD.TabIndex = 12;
            CircleXNUD.TextAlign = HorizontalAlignment.Center;
            CircleXNUD.ValueChanged += CircleXNUD_ValueChanged;
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
            // CircleRadiusNUD
            // 
            CircleRadiusNUD.BackColor = Color.FromArgb(60, 63, 65);
            CircleRadiusNUD.DecimalPlaces = 3;
            CircleRadiusNUD.ForeColor = SystemColors.Control;
            CircleRadiusNUD.Location = new Point(101, 112);
            CircleRadiusNUD.Margin = new Padding(4, 3, 4, 3);
            CircleRadiusNUD.Maximum = new decimal(new int[] { 50000, 0, 0, 0 });
            CircleRadiusNUD.Name = "CircleRadiusNUD";
            CircleRadiusNUD.Size = new Size(148, 23);
            CircleRadiusNUD.TabIndex = 18;
            CircleRadiusNUD.TextAlign = HorizontalAlignment.Center;
            CircleRadiusNUD.ValueChanged += CircleRadiusNUD_ValueChanged;
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
            // CircleHeightNUD
            // 
            CircleHeightNUD.BackColor = Color.FromArgb(60, 63, 65);
            CircleHeightNUD.DecimalPlaces = 3;
            CircleHeightNUD.ForeColor = SystemColors.Control;
            CircleHeightNUD.Location = new Point(101, 143);
            CircleHeightNUD.Margin = new Padding(4, 3, 4, 3);
            CircleHeightNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            CircleHeightNUD.Minimum = new decimal(new int[] { 20000, 0, 0, int.MinValue });
            CircleHeightNUD.Name = "CircleHeightNUD";
            CircleHeightNUD.Size = new Size(148, 23);
            CircleHeightNUD.TabIndex = 20;
            CircleHeightNUD.TextAlign = HorizontalAlignment.Center;
            CircleHeightNUD.ValueChanged += CircleHeightNUD_ValueChanged;
            // 
            // darkLabel294
            // 
            darkLabel294.AutoSize = true;
            darkLabel294.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel294.Location = new Point(14, 145);
            darkLabel294.Margin = new Padding(4, 0, 4, 0);
            darkLabel294.Name = "darkLabel294";
            darkLabel294.Size = new Size(43, 15);
            darkLabel294.TabIndex = 19;
            darkLabel294.Text = "Height";
            // 
            // ExpansionSafeZoneCylinderControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionSafeZoneCylinderControl";
            Size = new Size(270, 177);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)CircleZNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)CircleYNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)CircleXNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)CircleRadiusNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)CircleHeightNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private NumericUpDown CircleZNUD;
        private NumericUpDown CircleYNUD;
        private NumericUpDown CircleXNUD;
        private Label darkLabel41;
        private NumericUpDown CircleRadiusNUD;
        private Label darkLabel40;
        private Label darkLabel42;
        private Label darkLabel43;
        private NumericUpDown CircleHeightNUD;
        private Label darkLabel294;
    }
}
