namespace EconomyPlugin
{
    partial class AIRoamingLocationControl
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
            ZNUD = new TextBox();
            YNUD = new TextBox();
            XNUD = new TextBox();
            RadiusNUD = new TextBox();
            NameTB = new TextBox();
            EnabledCB = new CheckBox();
            darkLabel32 = new Label();
            TypeTB = new TextBox();
            darkLabel67 = new Label();
            darkLabel31 = new Label();
            darkLabel30 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ZNUD);
            groupBox1.Controls.Add(YNUD);
            groupBox1.Controls.Add(XNUD);
            groupBox1.Controls.Add(RadiusNUD);
            groupBox1.Controls.Add(NameTB);
            groupBox1.Controls.Add(EnabledCB);
            groupBox1.Controls.Add(darkLabel32);
            groupBox1.Controls.Add(TypeTB);
            groupBox1.Controls.Add(darkLabel67);
            groupBox1.Controls.Add(darkLabel31);
            groupBox1.Controls.Add(darkLabel30);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(400, 146);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "No Build Zone";
            // 
            // ZNUD
            // 
            ZNUD.BackColor = Color.FromArgb(60, 63, 65);
            ZNUD.ForeColor = SystemColors.Control;
            ZNUD.Location = new Point(294, 51);
            ZNUD.Margin = new Padding(4, 3, 4, 3);
            ZNUD.Name = "ZNUD";
            ZNUD.ReadOnly = true;
            ZNUD.Size = new Size(99, 23);
            ZNUD.TabIndex = 131;
            ZNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // YNUD
            // 
            YNUD.BackColor = Color.FromArgb(60, 63, 65);
            YNUD.ForeColor = SystemColors.Control;
            YNUD.Location = new Point(183, 51);
            YNUD.Margin = new Padding(4, 3, 4, 3);
            YNUD.Name = "YNUD";
            YNUD.ReadOnly = true;
            YNUD.Size = new Size(99, 23);
            YNUD.TabIndex = 130;
            YNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // XNUD
            // 
            XNUD.BackColor = Color.FromArgb(60, 63, 65);
            XNUD.ForeColor = SystemColors.Control;
            XNUD.Location = new Point(76, 51);
            XNUD.Margin = new Padding(4, 3, 4, 3);
            XNUD.Name = "XNUD";
            XNUD.ReadOnly = true;
            XNUD.Size = new Size(99, 23);
            XNUD.TabIndex = 129;
            XNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // RadiusNUD
            // 
            RadiusNUD.BackColor = Color.FromArgb(60, 63, 65);
            RadiusNUD.ForeColor = SystemColors.Control;
            RadiusNUD.Location = new Point(76, 81);
            RadiusNUD.Margin = new Padding(4, 3, 4, 3);
            RadiusNUD.Name = "RadiusNUD";
            RadiusNUD.ReadOnly = true;
            RadiusNUD.Size = new Size(99, 23);
            RadiusNUD.TabIndex = 128;
            RadiusNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // NameTB
            // 
            NameTB.BackColor = Color.FromArgb(60, 63, 65);
            NameTB.ForeColor = SystemColors.Control;
            NameTB.Location = new Point(76, 22);
            NameTB.Margin = new Padding(4, 3, 4, 3);
            NameTB.Name = "NameTB";
            NameTB.ReadOnly = true;
            NameTB.Size = new Size(317, 23);
            NameTB.TabIndex = 114;
            // 
            // EnabledCB
            // 
            EnabledCB.AutoSize = true;
            EnabledCB.ForeColor = SystemColors.Control;
            EnabledCB.Location = new Point(256, 83);
            EnabledCB.Margin = new Padding(4, 3, 4, 3);
            EnabledCB.Name = "EnabledCB";
            EnabledCB.Size = new Size(79, 19);
            EnabledCB.TabIndex = 119;
            EnabledCB.Text = "Is Enabled";
            EnabledCB.UseVisualStyleBackColor = true;
            EnabledCB.CheckedChanged += EnabledCB_CheckedChanged;
            // 
            // darkLabel32
            // 
            darkLabel32.AutoSize = true;
            darkLabel32.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel32.Location = new Point(14, 115);
            darkLabel32.Margin = new Padding(4, 0, 4, 0);
            darkLabel32.Name = "darkLabel32";
            darkLabel32.Size = new Size(31, 15);
            darkLabel32.TabIndex = 127;
            darkLabel32.Text = "Type";
            // 
            // TypeTB
            // 
            TypeTB.BackColor = Color.FromArgb(60, 63, 65);
            TypeTB.ForeColor = SystemColors.Control;
            TypeTB.Location = new Point(76, 112);
            TypeTB.Margin = new Padding(4, 3, 4, 3);
            TypeTB.Name = "TypeTB";
            TypeTB.ReadOnly = true;
            TypeTB.Size = new Size(317, 23);
            TypeTB.TabIndex = 120;
            // 
            // darkLabel67
            // 
            darkLabel67.AutoSize = true;
            darkLabel67.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel67.Location = new Point(14, 54);
            darkLabel67.Margin = new Padding(4, 0, 4, 0);
            darkLabel67.Name = "darkLabel67";
            darkLabel67.Size = new Size(42, 15);
            darkLabel67.TabIndex = 124;
            darkLabel67.Text = "Center";
            // 
            // darkLabel31
            // 
            darkLabel31.AutoSize = true;
            darkLabel31.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel31.Location = new Point(14, 84);
            darkLabel31.Margin = new Padding(4, 0, 4, 0);
            darkLabel31.Name = "darkLabel31";
            darkLabel31.Size = new Size(42, 15);
            darkLabel31.TabIndex = 126;
            darkLabel31.Text = "Radius";
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
            // AIRoamingLocationControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "AIRoamingLocationControl";
            Size = new Size(400, 146);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox NameTB;
        private CheckBox EnabledCB;
        private Label darkLabel32;
        private TextBox TypeTB;
        private Label darkLabel67;
        private Label darkLabel31;
        private Label darkLabel30;
        private TextBox ZNUD;
        private TextBox YNUD;
        private TextBox XNUD;
        private TextBox RadiusNUD;
    }
}
