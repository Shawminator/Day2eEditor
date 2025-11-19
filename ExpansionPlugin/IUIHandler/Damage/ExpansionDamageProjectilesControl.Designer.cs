namespace ExpansionPlugin
{
    partial class ExpansionDamageProjectilesControl
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
            darkLabel245 = new Label();
            ExplosionTB = new TextBox();
            label1 = new Label();
            AmmoTB = new TextBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(AmmoTB);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(darkLabel245);
            groupBox1.Controls.Add(ExplosionTB);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(465, 114);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Explosive Projectiles";
            // 
            // darkLabel245
            // 
            darkLabel245.AutoSize = true;
            darkLabel245.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel245.Location = new Point(22, 37);
            darkLabel245.Margin = new Padding(4, 0, 4, 0);
            darkLabel245.Name = "darkLabel245";
            darkLabel245.Size = new Size(58, 15);
            darkLabel245.TabIndex = 2;
            darkLabel245.Text = "Explosion";
            // 
            // ExplosionTB
            // 
            ExplosionTB.BackColor = Color.FromArgb(60, 63, 65);
            ExplosionTB.ForeColor = SystemColors.Control;
            ExplosionTB.Location = new Point(88, 34);
            ExplosionTB.Margin = new Padding(4, 3, 4, 3);
            ExplosionTB.Name = "ExplosionTB";
            ExplosionTB.Size = new Size(361, 23);
            ExplosionTB.TabIndex = 3;
            ExplosionTB.TextChanged += ExplosionTB_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(220, 220, 220);
            label1.Location = new Point(22, 67);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(44, 15);
            label1.TabIndex = 4;
            label1.Text = "Ammo";
            // 
            // AmmoTB
            // 
            AmmoTB.BackColor = Color.FromArgb(60, 63, 65);
            AmmoTB.ForeColor = SystemColors.Control;
            AmmoTB.Location = new Point(88, 67);
            AmmoTB.Margin = new Padding(4, 3, 4, 3);
            AmmoTB.Name = "AmmoTB";
            AmmoTB.Size = new Size(361, 23);
            AmmoTB.TabIndex = 5;
            AmmoTB.TextChanged += AmmoTB_TextChanged;
            // 
            // ExpansionDamageProjectilesControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionDamageProjectilesControl";
            Size = new Size(465, 114);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label darkLabel245;
        private TextBox ExplosionTB;
        private TextBox AmmoTB;
        private Label label1;
    }
}
