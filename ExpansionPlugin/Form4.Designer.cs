namespace DayZeEditor
{
    partial class Form4
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            numericUpDown27 = new NumericUpDown();
            darkLabel179 = new Label();
            numericUpDown28 = new NumericUpDown();
            darkLabel180 = new Label();
            groupBox1 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)numericUpDown27).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown28).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // numericUpDown27
            // 
            numericUpDown27.BackColor = Color.FromArgb(60, 63, 65);
            numericUpDown27.ForeColor = SystemColors.Control;
            numericUpDown27.Location = new Point(168, 23);
            numericUpDown27.Margin = new Padding(4, 3, 4, 3);
            numericUpDown27.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            numericUpDown27.Name = "numericUpDown27";
            numericUpDown27.Size = new Size(140, 23);
            numericUpDown27.TabIndex = 12;
            numericUpDown27.TextAlign = HorizontalAlignment.Center;
            // 
            // darkLabel179
            // 
            darkLabel179.AutoSize = true;
            darkLabel179.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel179.Location = new Point(14, 25);
            darkLabel179.Margin = new Padding(4, 0, 4, 0);
            darkLabel179.Name = "darkLabel179";
            darkLabel179.Size = new Size(112, 15);
            darkLabel179.TabIndex = 11;
            darkLabel179.Text = "Start Decay Lifetime";
            // 
            // numericUpDown28
            // 
            numericUpDown28.BackColor = Color.FromArgb(60, 63, 65);
            numericUpDown28.ForeColor = SystemColors.Control;
            numericUpDown28.Location = new Point(168, 53);
            numericUpDown28.Margin = new Padding(4, 3, 4, 3);
            numericUpDown28.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            numericUpDown28.Name = "numericUpDown28";
            numericUpDown28.Size = new Size(140, 23);
            numericUpDown28.TabIndex = 14;
            numericUpDown28.TextAlign = HorizontalAlignment.Center;
            // 
            // darkLabel180
            // 
            darkLabel180.AutoSize = true;
            darkLabel180.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel180.Location = new Point(14, 55);
            darkLabel180.Margin = new Padding(4, 0, 4, 0);
            darkLabel180.Name = "darkLabel180";
            darkLabel180.Size = new Size(119, 15);
            darkLabel180.TabIndex = 13;
            darkLabel180.Text = "Finish Decay Lifetime";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(darkLabel179);
            groupBox1.Controls.Add(numericUpDown27);
            groupBox1.Controls.Add(numericUpDown28);
            groupBox1.Controls.Add(darkLabel180);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(12, 501);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(322, 94);
            groupBox1.TabIndex = 19;
            groupBox1.TabStop = false;
            groupBox1.Text = "Contaminated Area";
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(1353, 950);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form4";
            Text = "Form4";
            ((System.ComponentModel.ISupportInitialize)numericUpDown27).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown28).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NumericUpDown numericUpDown27;
        private System.Windows.Forms.Label darkLabel179;
        private System.Windows.Forms.NumericUpDown numericUpDown28;
        private System.Windows.Forms.Label darkLabel180;
        private GroupBox groupBox1;
    }
}