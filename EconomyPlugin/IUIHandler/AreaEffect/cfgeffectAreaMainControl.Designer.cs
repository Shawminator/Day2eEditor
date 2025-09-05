namespace EconomyPlugin
{
    partial class cfgeffectAreaMainControl
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
            groupBox33 = new GroupBox();
            darkLabel22 = new Label();
            TriggerTypeTB = new TextBox();
            darkLabel23 = new Label();
            TypeTB = new TextBox();
            darkLabel33 = new Label();
            AreaNameTB = new TextBox();
            groupBox33.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox33
            // 
            groupBox33.BackColor = Color.FromArgb(60, 63, 65);
            groupBox33.Controls.Add(darkLabel22);
            groupBox33.Controls.Add(TriggerTypeTB);
            groupBox33.Controls.Add(darkLabel23);
            groupBox33.Controls.Add(TypeTB);
            groupBox33.Controls.Add(darkLabel33);
            groupBox33.Controls.Add(AreaNameTB);
            groupBox33.ForeColor = SystemColors.Control;
            groupBox33.Location = new Point(0, 0);
            groupBox33.Margin = new Padding(4, 3, 4, 3);
            groupBox33.Name = "groupBox33";
            groupBox33.Padding = new Padding(4, 3, 4, 3);
            groupBox33.Size = new Size(770, 114);
            groupBox33.TabIndex = 130;
            groupBox33.TabStop = false;
            groupBox33.Text = "Area Info";
            // 
            // darkLabel22
            // 
            darkLabel22.AutoSize = true;
            darkLabel22.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel22.Location = new Point(17, 85);
            darkLabel22.Margin = new Padding(4, 0, 4, 0);
            darkLabel22.Name = "darkLabel22";
            darkLabel22.Size = new Size(72, 15);
            darkLabel22.TabIndex = 125;
            darkLabel22.Text = "Trigger Type";
            // 
            // TriggerTypeTB
            // 
            TriggerTypeTB.BackColor = Color.FromArgb(60, 63, 65);
            TriggerTypeTB.ForeColor = SystemColors.Control;
            TriggerTypeTB.Location = new Point(114, 82);
            TriggerTypeTB.Margin = new Padding(4, 3, 4, 3);
            TriggerTypeTB.Name = "TriggerTypeTB";
            TriggerTypeTB.Size = new Size(648, 23);
            TriggerTypeTB.TabIndex = 124;
            TriggerTypeTB.TextChanged += TriggerTypeTB_TextChanged;
            // 
            // darkLabel23
            // 
            darkLabel23.AutoSize = true;
            darkLabel23.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel23.Location = new Point(17, 55);
            darkLabel23.Margin = new Padding(4, 0, 4, 0);
            darkLabel23.Name = "darkLabel23";
            darkLabel23.Size = new Size(59, 15);
            darkLabel23.TabIndex = 123;
            darkLabel23.Text = "Area Type";
            // 
            // TypeTB
            // 
            TypeTB.BackColor = Color.FromArgb(60, 63, 65);
            TypeTB.ForeColor = SystemColors.Control;
            TypeTB.Location = new Point(114, 52);
            TypeTB.Margin = new Padding(4, 3, 4, 3);
            TypeTB.Name = "TypeTB";
            TypeTB.Size = new Size(648, 23);
            TypeTB.TabIndex = 122;
            TypeTB.TextChanged += TypeTB_TextChanged;
            // 
            // darkLabel33
            // 
            darkLabel33.AutoSize = true;
            darkLabel33.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel33.Location = new Point(17, 25);
            darkLabel33.Margin = new Padding(4, 0, 4, 0);
            darkLabel33.Name = "darkLabel33";
            darkLabel33.Size = new Size(66, 15);
            darkLabel33.TabIndex = 119;
            darkLabel33.Text = "Area Name";
            // 
            // AreaNameTB
            // 
            AreaNameTB.BackColor = Color.FromArgb(60, 63, 65);
            AreaNameTB.ForeColor = SystemColors.Control;
            AreaNameTB.Location = new Point(114, 22);
            AreaNameTB.Margin = new Padding(4, 3, 4, 3);
            AreaNameTB.Name = "AreaNameTB";
            AreaNameTB.Size = new Size(648, 23);
            AreaNameTB.TabIndex = 118;
            AreaNameTB.TextChanged += AreaNameTB_TextChanged;
            // 
            // cfgeffectAreaMainControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox33);
            ForeColor = SystemColors.Control;
            Name = "cfgeffectAreaMainControl";
            Size = new Size(770, 114);
            groupBox33.ResumeLayout(false);
            groupBox33.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox33;
        private Label darkLabel22;
        private TextBox TriggerTypeTB;
        private Label darkLabel23;
        private TextBox TypeTB;
        private Label darkLabel33;
        private TextBox AreaNameTB;
    }
}
