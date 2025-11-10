namespace ExpansionPlugin
{
    partial class ExpansionAINoGoAreaControl
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
            label3 = new Label();
            label2 = new Label();
            HieghtNUD = new NumericUpDown();
            RadiusNUD = new NumericUpDown();
            POSXNUD = new NumericUpDown();
            label1 = new Label();
            darkLabel44 = new Label();
            POSZNUD = new NumericUpDown();
            darkLabel45 = new Label();
            POSYNUD = new NumericUpDown();
            NameTB = new TextBox();
            darkLabel30 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)HieghtNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RadiusNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)POSXNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)POSZNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)POSYNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(HieghtNUD);
            groupBox1.Controls.Add(RadiusNUD);
            groupBox1.Controls.Add(POSXNUD);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(darkLabel44);
            groupBox1.Controls.Add(POSZNUD);
            groupBox1.Controls.Add(darkLabel45);
            groupBox1.Controls.Add(POSYNUD);
            groupBox1.Controls.Add(NameTB);
            groupBox1.Controls.Add(darkLabel30);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(403, 208);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Roaming Locations";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.FromArgb(220, 220, 220);
            label3.Location = new Point(14, 174);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(43, 15);
            label3.TabIndex = 213;
            label3.Text = "Height";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.FromArgb(220, 220, 220);
            label2.Location = new Point(14, 142);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(42, 15);
            label2.TabIndex = 212;
            label2.Text = "Radius";
            // 
            // HieghtNUD
            // 
            HieghtNUD.BackColor = Color.FromArgb(60, 63, 65);
            HieghtNUD.DecimalPlaces = 1;
            HieghtNUD.ForeColor = SystemColors.Control;
            HieghtNUD.Location = new Point(75, 172);
            HieghtNUD.Margin = new Padding(4, 3, 4, 3);
            HieghtNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            HieghtNUD.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            HieghtNUD.Name = "HieghtNUD";
            HieghtNUD.Size = new Size(318, 23);
            HieghtNUD.TabIndex = 211;
            HieghtNUD.TextAlign = HorizontalAlignment.Center;
            HieghtNUD.ValueChanged += HieghtNUD_ValueChanged;
            // 
            // RadiusNUD
            // 
            RadiusNUD.BackColor = Color.FromArgb(60, 63, 65);
            RadiusNUD.DecimalPlaces = 1;
            RadiusNUD.ForeColor = SystemColors.Control;
            RadiusNUD.Location = new Point(76, 140);
            RadiusNUD.Margin = new Padding(4, 3, 4, 3);
            RadiusNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            RadiusNUD.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            RadiusNUD.Name = "RadiusNUD";
            RadiusNUD.Size = new Size(318, 23);
            RadiusNUD.TabIndex = 210;
            RadiusNUD.TextAlign = HorizontalAlignment.Center;
            RadiusNUD.ValueChanged += RadiusNUD_ValueChanged;
            // 
            // POSXNUD
            // 
            POSXNUD.BackColor = Color.FromArgb(60, 63, 65);
            POSXNUD.DecimalPlaces = 6;
            POSXNUD.ForeColor = SystemColors.Control;
            POSXNUD.Location = new Point(76, 51);
            POSXNUD.Margin = new Padding(4, 3, 4, 3);
            POSXNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            POSXNUD.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            POSXNUD.Name = "POSXNUD";
            POSXNUD.Size = new Size(318, 23);
            POSXNUD.TabIndex = 206;
            POSXNUD.TextAlign = HorizontalAlignment.Center;
            POSXNUD.ValueChanged += POSXNUD_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(220, 220, 220);
            label1.Location = new Point(13, 110);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(17, 15);
            label1.TabIndex = 208;
            label1.Text = "Z:";
            // 
            // darkLabel44
            // 
            darkLabel44.AutoSize = true;
            darkLabel44.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel44.Location = new Point(13, 83);
            darkLabel44.Margin = new Padding(4, 0, 4, 0);
            darkLabel44.Name = "darkLabel44";
            darkLabel44.Size = new Size(17, 15);
            darkLabel44.TabIndex = 207;
            darkLabel44.Text = "Y:";
            // 
            // POSZNUD
            // 
            POSZNUD.BackColor = Color.FromArgb(60, 63, 65);
            POSZNUD.DecimalPlaces = 6;
            POSZNUD.ForeColor = SystemColors.Control;
            POSZNUD.Location = new Point(75, 111);
            POSZNUD.Margin = new Padding(4, 3, 4, 3);
            POSZNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            POSZNUD.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            POSZNUD.Name = "POSZNUD";
            POSZNUD.Size = new Size(318, 23);
            POSZNUD.TabIndex = 209;
            POSZNUD.TextAlign = HorizontalAlignment.Center;
            POSZNUD.ValueChanged += POSZNUD_ValueChanged;
            // 
            // darkLabel45
            // 
            darkLabel45.AutoSize = true;
            darkLabel45.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel45.Location = new Point(14, 53);
            darkLabel45.Margin = new Padding(4, 0, 4, 0);
            darkLabel45.Name = "darkLabel45";
            darkLabel45.Size = new Size(17, 15);
            darkLabel45.TabIndex = 205;
            darkLabel45.Text = "X:";
            // 
            // POSYNUD
            // 
            POSYNUD.BackColor = Color.FromArgb(60, 63, 65);
            POSYNUD.DecimalPlaces = 6;
            POSYNUD.ForeColor = SystemColors.Control;
            POSYNUD.Location = new Point(75, 81);
            POSYNUD.Margin = new Padding(4, 3, 4, 3);
            POSYNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            POSYNUD.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            POSYNUD.Name = "POSYNUD";
            POSYNUD.Size = new Size(318, 23);
            POSYNUD.TabIndex = 204;
            POSYNUD.TextAlign = HorizontalAlignment.Center;
            POSYNUD.ValueChanged += POSYNUD_ValueChanged;
            // 
            // NameTB
            // 
            NameTB.BackColor = Color.FromArgb(60, 63, 65);
            NameTB.ForeColor = SystemColors.Control;
            NameTB.Location = new Point(76, 22);
            NameTB.Margin = new Padding(4, 3, 4, 3);
            NameTB.Name = "NameTB";
            NameTB.Size = new Size(317, 23);
            NameTB.TabIndex = 114;
            NameTB.TextChanged += NameTB_TextChanged;
            // 
            // darkLabel30
            // 
            darkLabel30.AutoSize = true;
            darkLabel30.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel30.Location = new Point(14, 25);
            darkLabel30.Margin = new Padding(4, 0, 4, 0);
            darkLabel30.Name = "darkLabel30";
            darkLabel30.Size = new Size(39, 15);
            darkLabel30.TabIndex = 125;
            darkLabel30.Text = "Name";
            // 
            // ExpansionAINoGoAreaControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionAINoGoAreaControl";
            Size = new Size(403, 208);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)HieghtNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)RadiusNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)POSXNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)POSZNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)POSYNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox NameTB;
        private Label darkLabel30;
        private Label label3;
        private Label label2;
        private NumericUpDown HieghtNUD;
        private NumericUpDown RadiusNUD;
        private NumericUpDown POSXNUD;
        private Label label1;
        private Label darkLabel44;
        private NumericUpDown POSZNUD;
        private Label darkLabel45;
        private NumericUpDown POSYNUD;
    }
}
