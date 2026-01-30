namespace ExpansionPlugin
{
    partial class ExpansionBuildNoBuildZonesControl
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
            darkLabel28 = new Label();
            ZonesAreNoBuildZonesCB = new CheckBox();
            textBox2 = new TextBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(darkLabel28);
            groupBox1.Controls.Add(ZonesAreNoBuildZonesCB);
            groupBox1.Controls.Add(textBox2);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(498, 101);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Build Zones General";
            // 
            // darkLabel28
            // 
            darkLabel28.AutoSize = true;
            darkLabel28.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel28.Location = new Point(17, 18);
            darkLabel28.Margin = new Padding(4, 0, 4, 0);
            darkLabel28.Name = "darkLabel28";
            darkLabel28.Size = new Size(205, 15);
            darkLabel28.TabIndex = 112;
            darkLabel28.Text = "BuildZone Required Custom Message";
            // 
            // ZonesAreNoBuildZonesCB
            // 
            ZonesAreNoBuildZonesCB.AutoSize = true;
            ZonesAreNoBuildZonesCB.ForeColor = SystemColors.Control;
            ZonesAreNoBuildZonesCB.Location = new Point(17, 65);
            ZonesAreNoBuildZonesCB.Margin = new Padding(4, 3, 4, 3);
            ZonesAreNoBuildZonesCB.Name = "ZonesAreNoBuildZonesCB";
            ZonesAreNoBuildZonesCB.Size = new Size(163, 19);
            ZonesAreNoBuildZonesCB.TabIndex = 111;
            ZonesAreNoBuildZonesCB.Text = "Zones Are No Build Zones";
            ZonesAreNoBuildZonesCB.TextAlign = ContentAlignment.MiddleRight;
            ZonesAreNoBuildZonesCB.UseVisualStyleBackColor = true;
            ZonesAreNoBuildZonesCB.CheckedChanged += ZonesAreNoBuildZonesCB_CheckedChanged;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(60, 63, 65);
            textBox2.ForeColor = SystemColors.Control;
            textBox2.Location = new Point(16, 36);
            textBox2.Margin = new Padding(4, 3, 4, 3);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(475, 23);
            textBox2.TabIndex = 110;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // ExpansionBuildNoBuildZonesControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionBuildNoBuildZonesControl";
            Size = new Size(498, 101);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label darkLabel28;
        private CheckBox ZonesAreNoBuildZonesCB;
        private TextBox textBox2;
    }
}
