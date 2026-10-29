namespace CaparePlugin
{
    partial class CapareVector3Control
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
            XL = new Label();
            XNUD = new NumericUpDown();
            YL = new Label();
            YNUD = new NumericUpDown();
            ZL = new Label();
            ZNUD = new NumericUpDown();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)XNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)YNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ZNUD).BeginInit();
            SuspendLayout();
            //
            // groupBox1
            //
            groupBox1.Controls.Add(XL);
            groupBox1.Controls.Add(XNUD);
            groupBox1.Controls.Add(YL);
            groupBox1.Controls.Add(YNUD);
            groupBox1.Controls.Add(ZL);
            groupBox1.Controls.Add(ZNUD);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(435, 115);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Position";
            //
            // XL
            //
            XL.AutoSize = true;
            XL.ForeColor = Color.FromArgb(220, 220, 220);
            XL.Location = new Point(11, 25);
            XL.Margin = new Padding(4, 0, 4, 0);
            XL.Name = "XL";
            XL.Size = new Size(30, 15);
            XL.TabIndex = 0;
            XL.Text = "X:";
            //
            // XNUD
            //
            XNUD.BackColor = Color.FromArgb(60, 63, 65);
            XNUD.DecimalPlaces = 6;
            XNUD.ForeColor = SystemColors.Control;
            XNUD.Location = new Point(190, 22);
            XNUD.Margin = new Padding(4, 3, 4, 3);
            XNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            XNUD.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            XNUD.Name = "XNUD";
            XNUD.Size = new Size(230, 23);
            XNUD.TabIndex = 1;
            XNUD.TextAlign = HorizontalAlignment.Center;
            XNUD.ValueChanged += XNUD_ValueChanged;
            //
            // YL
            //
            YL.AutoSize = true;
            YL.ForeColor = Color.FromArgb(220, 220, 220);
            YL.Location = new Point(11, 54);
            YL.Margin = new Padding(4, 0, 4, 0);
            YL.Name = "YL";
            YL.Size = new Size(30, 15);
            YL.TabIndex = 2;
            YL.Text = "Y:";
            //
            // YNUD
            //
            YNUD.BackColor = Color.FromArgb(60, 63, 65);
            YNUD.DecimalPlaces = 6;
            YNUD.ForeColor = SystemColors.Control;
            YNUD.Location = new Point(190, 51);
            YNUD.Margin = new Padding(4, 3, 4, 3);
            YNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            YNUD.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            YNUD.Name = "YNUD";
            YNUD.Size = new Size(230, 23);
            YNUD.TabIndex = 3;
            YNUD.TextAlign = HorizontalAlignment.Center;
            YNUD.ValueChanged += YNUD_ValueChanged;
            //
            // ZL
            //
            ZL.AutoSize = true;
            ZL.ForeColor = Color.FromArgb(220, 220, 220);
            ZL.Location = new Point(11, 83);
            ZL.Margin = new Padding(4, 0, 4, 0);
            ZL.Name = "ZL";
            ZL.Size = new Size(30, 15);
            ZL.TabIndex = 4;
            ZL.Text = "Z:";
            //
            // ZNUD
            //
            ZNUD.BackColor = Color.FromArgb(60, 63, 65);
            ZNUD.DecimalPlaces = 6;
            ZNUD.ForeColor = SystemColors.Control;
            ZNUD.Location = new Point(190, 80);
            ZNUD.Margin = new Padding(4, 3, 4, 3);
            ZNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            ZNUD.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            ZNUD.Name = "ZNUD";
            ZNUD.Size = new Size(230, 23);
            ZNUD.TabIndex = 5;
            ZNUD.TextAlign = HorizontalAlignment.Center;
            ZNUD.ValueChanged += ZNUD_ValueChanged;
            //
            // CapareVector3Control
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "CapareVector3Control";
            Size = new Size(435, 121);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)XNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)YNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)ZNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label XL;
        private NumericUpDown XNUD;
        private Label YL;
        private NumericUpDown YNUD;
        private Label ZL;
        private NumericUpDown ZNUD;
    }
}
