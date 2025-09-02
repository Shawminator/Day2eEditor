namespace EconomyPlugin
{
    partial class Vector3Control
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
            darkLabel30 = new Label();
            POSZNUD = new NumericUpDown();
            POSYNUD = new NumericUpDown();
            darkLabel45 = new Label();
            POSXNUD = new NumericUpDown();
            darkLabel44 = new Label();
            groupBox1 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)POSZNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)POSYNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)POSXNUD).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // darkLabel30
            // 
            darkLabel30.AutoSize = true;
            darkLabel30.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel30.Location = new Point(15, 81);
            darkLabel30.Margin = new Padding(4, 0, 4, 0);
            darkLabel30.Name = "darkLabel30";
            darkLabel30.Size = new Size(17, 15);
            darkLabel30.TabIndex = 202;
            darkLabel30.Text = "Z:";
            // 
            // POSZNUD
            // 
            POSZNUD.BackColor = Color.FromArgb(60, 63, 65);
            POSZNUD.DecimalPlaces = 6;
            POSZNUD.ForeColor = SystemColors.Control;
            POSZNUD.Location = new Point(50, 82);
            POSZNUD.Margin = new Padding(4, 3, 4, 3);
            POSZNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            POSZNUD.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            POSZNUD.Name = "POSZNUD";
            POSZNUD.Size = new Size(227, 23);
            POSZNUD.TabIndex = 203;
            POSZNUD.TextAlign = HorizontalAlignment.Center;
            POSZNUD.ValueChanged += POSZNUD_ValueChanged;
            // 
            // POSYNUD
            // 
            POSYNUD.BackColor = Color.FromArgb(60, 63, 65);
            POSYNUD.DecimalPlaces = 6;
            POSYNUD.ForeColor = SystemColors.Control;
            POSYNUD.Location = new Point(50, 52);
            POSYNUD.Margin = new Padding(4, 3, 4, 3);
            POSYNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            POSYNUD.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            POSYNUD.Name = "POSYNUD";
            POSYNUD.Size = new Size(227, 23);
            POSYNUD.TabIndex = 198;
            POSYNUD.TextAlign = HorizontalAlignment.Center;
            POSYNUD.ValueChanged += POSYNUD_ValueChanged;
            // 
            // darkLabel45
            // 
            darkLabel45.AutoSize = true;
            darkLabel45.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel45.Location = new Point(16, 24);
            darkLabel45.Margin = new Padding(4, 0, 4, 0);
            darkLabel45.Name = "darkLabel45";
            darkLabel45.Size = new Size(17, 15);
            darkLabel45.TabIndex = 199;
            darkLabel45.Text = "X:";
            // 
            // POSXNUD
            // 
            POSXNUD.BackColor = Color.FromArgb(60, 63, 65);
            POSXNUD.DecimalPlaces = 6;
            POSXNUD.ForeColor = SystemColors.Control;
            POSXNUD.Location = new Point(51, 22);
            POSXNUD.Margin = new Padding(4, 3, 4, 3);
            POSXNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            POSXNUD.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            POSXNUD.Name = "POSXNUD";
            POSXNUD.Size = new Size(227, 23);
            POSXNUD.TabIndex = 200;
            POSXNUD.TextAlign = HorizontalAlignment.Center;
            POSXNUD.ValueChanged += POSXNUD_ValueChanged;
            // 
            // darkLabel44
            // 
            darkLabel44.AutoSize = true;
            darkLabel44.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel44.Location = new Point(15, 54);
            darkLabel44.Margin = new Padding(4, 0, 4, 0);
            darkLabel44.Name = "darkLabel44";
            darkLabel44.Size = new Size(17, 15);
            darkLabel44.TabIndex = 201;
            darkLabel44.Text = "Y:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(POSXNUD);
            groupBox1.Controls.Add(darkLabel30);
            groupBox1.Controls.Add(darkLabel44);
            groupBox1.Controls.Add(POSZNUD);
            groupBox1.Controls.Add(darkLabel45);
            groupBox1.Controls.Add(POSYNUD);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(293, 124);
            groupBox1.TabIndex = 204;
            groupBox1.TabStop = false;
            groupBox1.Text = "Vector 3";
            // 
            // Vector3Control
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "Vector3Control";
            Size = new Size(306, 140);
            ((System.ComponentModel.ISupportInitialize)POSZNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)POSYNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)POSXNUD).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label darkLabel30;
        private NumericUpDown POSZNUD;
        private NumericUpDown POSYNUD;
        private Label darkLabel45;
        private NumericUpDown POSXNUD;
        private Label darkLabel44;
        private GroupBox groupBox1;
    }
}
