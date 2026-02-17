namespace ExpansionPlugin
{
    partial class ExpansionAirdropLocationControl
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
            groupBox81 = new GroupBox();
            darkLabel109 = new Label();
            MissionDropNameTB = new TextBox();
            MissionDropXNUD = new NumericUpDown();
            darkLabel106 = new Label();
            darkLabel110 = new Label();
            MissionDropRadiusNUD = new NumericUpDown();
            MissionDropYNUD = new NumericUpDown();
            darkLabel108 = new Label();
            groupBox81.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MissionDropXNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MissionDropRadiusNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MissionDropYNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox81
            // 
            groupBox81.Controls.Add(darkLabel109);
            groupBox81.Controls.Add(MissionDropNameTB);
            groupBox81.Controls.Add(MissionDropXNUD);
            groupBox81.Controls.Add(darkLabel106);
            groupBox81.Controls.Add(darkLabel110);
            groupBox81.Controls.Add(MissionDropRadiusNUD);
            groupBox81.Controls.Add(MissionDropYNUD);
            groupBox81.Controls.Add(darkLabel108);
            groupBox81.ForeColor = SystemColors.Control;
            groupBox81.Location = new Point(0, 0);
            groupBox81.Margin = new Padding(4, 3, 4, 3);
            groupBox81.Name = "groupBox81";
            groupBox81.Padding = new Padding(4, 3, 4, 3);
            groupBox81.Size = new Size(481, 145);
            groupBox81.TabIndex = 19;
            groupBox81.TabStop = false;
            groupBox81.Text = "Drop Location";
            // 
            // darkLabel109
            // 
            darkLabel109.AutoSize = true;
            darkLabel109.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel109.Location = new Point(15, 25);
            darkLabel109.Margin = new Padding(4, 0, 4, 0);
            darkLabel109.Name = "darkLabel109";
            darkLabel109.Size = new Size(39, 15);
            darkLabel109.TabIndex = 0;
            darkLabel109.Text = "Name";
            // 
            // MissionDropNameTB
            // 
            MissionDropNameTB.BackColor = Color.FromArgb(60, 63, 65);
            MissionDropNameTB.ForeColor = SystemColors.Control;
            MissionDropNameTB.Location = new Point(93, 22);
            MissionDropNameTB.Margin = new Padding(4, 3, 4, 3);
            MissionDropNameTB.Name = "MissionDropNameTB";
            MissionDropNameTB.Size = new Size(377, 23);
            MissionDropNameTB.TabIndex = 1;
            MissionDropNameTB.Tag = "Name";
            // 
            // MissionDropXNUD
            // 
            MissionDropXNUD.BackColor = Color.FromArgb(60, 63, 65);
            MissionDropXNUD.DecimalPlaces = 1;
            MissionDropXNUD.ForeColor = SystemColors.Control;
            MissionDropXNUD.Location = new Point(93, 53);
            MissionDropXNUD.Margin = new Padding(4, 3, 4, 3);
            MissionDropXNUD.Maximum = new decimal(new int[] { 50000, 0, 0, 0 });
            MissionDropXNUD.Minimum = new decimal(new int[] { 50000, 0, 0, int.MinValue });
            MissionDropXNUD.Name = "MissionDropXNUD";
            MissionDropXNUD.Size = new Size(172, 23);
            MissionDropXNUD.TabIndex = 3;
            MissionDropXNUD.Tag = "x";
            MissionDropXNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // darkLabel106
            // 
            darkLabel106.AutoSize = true;
            darkLabel106.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel106.Location = new Point(15, 55);
            darkLabel106.Margin = new Padding(4, 0, 4, 0);
            darkLabel106.Name = "darkLabel106";
            darkLabel106.Size = new Size(14, 15);
            darkLabel106.TabIndex = 2;
            darkLabel106.Text = "X";
            // 
            // darkLabel110
            // 
            darkLabel110.AutoSize = true;
            darkLabel110.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel110.Location = new Point(15, 115);
            darkLabel110.Margin = new Padding(4, 0, 4, 0);
            darkLabel110.Name = "darkLabel110";
            darkLabel110.Size = new Size(42, 15);
            darkLabel110.TabIndex = 6;
            darkLabel110.Text = "Radius";
            // 
            // MissionDropRadiusNUD
            // 
            MissionDropRadiusNUD.BackColor = Color.FromArgb(60, 63, 65);
            MissionDropRadiusNUD.DecimalPlaces = 1;
            MissionDropRadiusNUD.ForeColor = SystemColors.Control;
            MissionDropRadiusNUD.Location = new Point(93, 113);
            MissionDropRadiusNUD.Margin = new Padding(4, 3, 4, 3);
            MissionDropRadiusNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            MissionDropRadiusNUD.Name = "MissionDropRadiusNUD";
            MissionDropRadiusNUD.Size = new Size(172, 23);
            MissionDropRadiusNUD.TabIndex = 7;
            MissionDropRadiusNUD.Tag = "Radius";
            MissionDropRadiusNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // MissionDropYNUD
            // 
            MissionDropYNUD.BackColor = Color.FromArgb(60, 63, 65);
            MissionDropYNUD.DecimalPlaces = 1;
            MissionDropYNUD.ForeColor = SystemColors.Control;
            MissionDropYNUD.Location = new Point(93, 83);
            MissionDropYNUD.Margin = new Padding(4, 3, 4, 3);
            MissionDropYNUD.Maximum = new decimal(new int[] { 50000, 0, 0, 0 });
            MissionDropYNUD.Minimum = new decimal(new int[] { 50000, 0, 0, int.MinValue });
            MissionDropYNUD.Name = "MissionDropYNUD";
            MissionDropYNUD.Size = new Size(172, 23);
            MissionDropYNUD.TabIndex = 5;
            MissionDropYNUD.Tag = "z";
            MissionDropYNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // darkLabel108
            // 
            darkLabel108.AutoSize = true;
            darkLabel108.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel108.Location = new Point(15, 85);
            darkLabel108.Margin = new Padding(4, 0, 4, 0);
            darkLabel108.Name = "darkLabel108";
            darkLabel108.Size = new Size(14, 15);
            darkLabel108.TabIndex = 4;
            darkLabel108.Text = "Z";
            // 
            // ExpansionAirdropLocationControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox81);
            ForeColor = SystemColors.Control;
            Name = "ExpansionAirdropLocationControl";
            Size = new Size(481, 145);
            groupBox81.ResumeLayout(false);
            groupBox81.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)MissionDropXNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)MissionDropRadiusNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)MissionDropYNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox81;
        private Label darkLabel109;
        private TextBox MissionDropNameTB;
        private NumericUpDown MissionDropXNUD;
        private Label darkLabel106;
        private Label darkLabel110;
        private NumericUpDown MissionDropRadiusNUD;
        private NumericUpDown MissionDropYNUD;
        private Label darkLabel108;
    }
}
