namespace EconomyPlugin
{
    partial class SpawnGearAttributesControl
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
            SpawnGearAttributesGB = new GroupBox();
            SpawnGearQuanityMaxNUD = new NumericUpDown();
            darkLabel30 = new Label();
            SpawnGearQuanityMinNUD = new NumericUpDown();
            darkLabel31 = new Label();
            SpawnGearhealthMaxNUD = new NumericUpDown();
            darkLabel45 = new Label();
            SpawnGearhealthMinNUD = new NumericUpDown();
            darkLabel44 = new Label();
            SpawnGearAttributesGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SpawnGearQuanityMaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SpawnGearQuanityMinNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SpawnGearhealthMaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SpawnGearhealthMinNUD).BeginInit();
            SuspendLayout();
            // 
            // SpawnGearAttributesGB
            // 
            SpawnGearAttributesGB.Controls.Add(SpawnGearQuanityMaxNUD);
            SpawnGearAttributesGB.Controls.Add(darkLabel30);
            SpawnGearAttributesGB.Controls.Add(SpawnGearQuanityMinNUD);
            SpawnGearAttributesGB.Controls.Add(darkLabel31);
            SpawnGearAttributesGB.Controls.Add(SpawnGearhealthMaxNUD);
            SpawnGearAttributesGB.Controls.Add(darkLabel45);
            SpawnGearAttributesGB.Controls.Add(SpawnGearhealthMinNUD);
            SpawnGearAttributesGB.Controls.Add(darkLabel44);
            SpawnGearAttributesGB.ForeColor = SystemColors.Control;
            SpawnGearAttributesGB.Location = new Point(0, 0);
            SpawnGearAttributesGB.Margin = new Padding(4, 3, 4, 3);
            SpawnGearAttributesGB.Name = "SpawnGearAttributesGB";
            SpawnGearAttributesGB.Padding = new Padding(4, 3, 4, 3);
            SpawnGearAttributesGB.Size = new Size(324, 148);
            SpawnGearAttributesGB.TabIndex = 230;
            SpawnGearAttributesGB.TabStop = false;
            SpawnGearAttributesGB.Text = "Attributes";
            // 
            // SpawnGearQuanityMaxNUD
            // 
            SpawnGearQuanityMaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            SpawnGearQuanityMaxNUD.DecimalPlaces = 1;
            SpawnGearQuanityMaxNUD.ForeColor = SystemColors.Control;
            SpawnGearQuanityMaxNUD.Location = new Point(181, 108);
            SpawnGearQuanityMaxNUD.Margin = new Padding(4, 3, 4, 3);
            SpawnGearQuanityMaxNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            SpawnGearQuanityMaxNUD.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            SpawnGearQuanityMaxNUD.Name = "SpawnGearQuanityMaxNUD";
            SpawnGearQuanityMaxNUD.Size = new Size(122, 23);
            SpawnGearQuanityMaxNUD.TabIndex = 195;
            SpawnGearQuanityMaxNUD.TextAlign = HorizontalAlignment.Center;
            SpawnGearQuanityMaxNUD.ValueChanged += SpawnGearQuanityMaxNUD_ValueChanged;
            // 
            // darkLabel30
            // 
            darkLabel30.AutoSize = true;
            darkLabel30.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel30.Location = new Point(16, 78);
            darkLabel30.Margin = new Padding(4, 0, 4, 0);
            darkLabel30.Name = "darkLabel30";
            darkLabel30.Size = new Size(77, 15);
            darkLabel30.TabIndex = 196;
            darkLabel30.Text = "Quantity Min";
            // 
            // SpawnGearQuanityMinNUD
            // 
            SpawnGearQuanityMinNUD.BackColor = Color.FromArgb(60, 63, 65);
            SpawnGearQuanityMinNUD.DecimalPlaces = 1;
            SpawnGearQuanityMinNUD.ForeColor = SystemColors.Control;
            SpawnGearQuanityMinNUD.Location = new Point(182, 78);
            SpawnGearQuanityMinNUD.Margin = new Padding(4, 3, 4, 3);
            SpawnGearQuanityMinNUD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            SpawnGearQuanityMinNUD.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            SpawnGearQuanityMinNUD.Name = "SpawnGearQuanityMinNUD";
            SpawnGearQuanityMinNUD.Size = new Size(122, 23);
            SpawnGearQuanityMinNUD.TabIndex = 197;
            SpawnGearQuanityMinNUD.TextAlign = HorizontalAlignment.Center;
            SpawnGearQuanityMinNUD.ValueChanged += SpawnGearQuanityMinNUD_ValueChanged;
            // 
            // darkLabel31
            // 
            darkLabel31.AutoSize = true;
            darkLabel31.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel31.Location = new Point(16, 111);
            darkLabel31.Margin = new Padding(4, 0, 4, 0);
            darkLabel31.Name = "darkLabel31";
            darkLabel31.Size = new Size(79, 15);
            darkLabel31.TabIndex = 198;
            darkLabel31.Text = "Quantity Max";
            // 
            // SpawnGearhealthMaxNUD
            // 
            SpawnGearhealthMaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            SpawnGearhealthMaxNUD.DecimalPlaces = 1;
            SpawnGearhealthMaxNUD.ForeColor = SystemColors.Control;
            SpawnGearhealthMaxNUD.Location = new Point(182, 48);
            SpawnGearhealthMaxNUD.Margin = new Padding(4, 3, 4, 3);
            SpawnGearhealthMaxNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            SpawnGearhealthMaxNUD.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            SpawnGearhealthMaxNUD.Name = "SpawnGearhealthMaxNUD";
            SpawnGearhealthMaxNUD.Size = new Size(122, 23);
            SpawnGearhealthMaxNUD.TabIndex = 191;
            SpawnGearhealthMaxNUD.TextAlign = HorizontalAlignment.Center;
            SpawnGearhealthMaxNUD.ValueChanged += SpawnGearhealthMaxNUD_ValueChanged;
            // 
            // darkLabel45
            // 
            darkLabel45.AutoSize = true;
            darkLabel45.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel45.Location = new Point(17, 21);
            darkLabel45.Margin = new Padding(4, 0, 4, 0);
            darkLabel45.Name = "darkLabel45";
            darkLabel45.Size = new Size(66, 15);
            darkLabel45.TabIndex = 192;
            darkLabel45.Text = "Health Min";
            // 
            // SpawnGearhealthMinNUD
            // 
            SpawnGearhealthMinNUD.BackColor = Color.FromArgb(60, 63, 65);
            SpawnGearhealthMinNUD.DecimalPlaces = 1;
            SpawnGearhealthMinNUD.ForeColor = SystemColors.Control;
            SpawnGearhealthMinNUD.Location = new Point(183, 18);
            SpawnGearhealthMinNUD.Margin = new Padding(4, 3, 4, 3);
            SpawnGearhealthMinNUD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            SpawnGearhealthMinNUD.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            SpawnGearhealthMinNUD.Name = "SpawnGearhealthMinNUD";
            SpawnGearhealthMinNUD.Size = new Size(122, 23);
            SpawnGearhealthMinNUD.TabIndex = 193;
            SpawnGearhealthMinNUD.TextAlign = HorizontalAlignment.Center;
            SpawnGearhealthMinNUD.ValueChanged += SpawnGearhealthMinNUD_ValueChanged;
            // 
            // darkLabel44
            // 
            darkLabel44.AutoSize = true;
            darkLabel44.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel44.Location = new Point(16, 51);
            darkLabel44.Margin = new Padding(4, 0, 4, 0);
            darkLabel44.Name = "darkLabel44";
            darkLabel44.Size = new Size(68, 15);
            darkLabel44.TabIndex = 194;
            darkLabel44.Text = "Health Max";
            // 
            // SpawnGearAttributesControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(SpawnGearAttributesGB);
            ForeColor = SystemColors.Control;
            Name = "SpawnGearAttributesControl";
            Size = new Size(337, 158);
            SpawnGearAttributesGB.ResumeLayout(false);
            SpawnGearAttributesGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SpawnGearQuanityMaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)SpawnGearQuanityMinNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)SpawnGearhealthMaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)SpawnGearhealthMinNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox SpawnGearAttributesGB;
        private NumericUpDown SpawnGearQuanityMaxNUD;
        private Label darkLabel30;
        private NumericUpDown SpawnGearQuanityMinNUD;
        private Label darkLabel31;
        private NumericUpDown SpawnGearhealthMaxNUD;
        private Label darkLabel45;
        private NumericUpDown SpawnGearhealthMinNUD;
        private Label darkLabel44;
    }
}
