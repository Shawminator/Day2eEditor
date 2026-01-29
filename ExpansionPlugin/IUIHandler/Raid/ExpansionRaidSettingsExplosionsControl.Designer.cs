namespace ExpansionPlugin
{
    partial class ExpansionRaidSettingsExplosionsControl
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
            groupBox22 = new GroupBox();
            darkLabel59 = new Label();
            ProjectileDamageMultiplierNUD = new NumericUpDown();
            darkLabel58 = new Label();
            ExplosionDamageMultiplierNUD = new NumericUpDown();
            darkLabel57 = new Label();
            ExplosionTimeNUD = new NumericUpDown();
            EnableExplosiveWhitelistCB = new CheckBox();
            label1 = new Label();
            groupBox22.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ProjectileDamageMultiplierNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ExplosionDamageMultiplierNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ExplosionTimeNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox22
            // 
            groupBox22.Controls.Add(label1);
            groupBox22.Controls.Add(darkLabel59);
            groupBox22.Controls.Add(ProjectileDamageMultiplierNUD);
            groupBox22.Controls.Add(darkLabel58);
            groupBox22.Controls.Add(ExplosionDamageMultiplierNUD);
            groupBox22.Controls.Add(darkLabel57);
            groupBox22.Controls.Add(ExplosionTimeNUD);
            groupBox22.Controls.Add(EnableExplosiveWhitelistCB);
            groupBox22.ForeColor = SystemColors.Control;
            groupBox22.Location = new Point(0, 0);
            groupBox22.Margin = new Padding(4, 3, 4, 3);
            groupBox22.Name = "groupBox22";
            groupBox22.Padding = new Padding(4, 3, 4, 3);
            groupBox22.Size = new Size(335, 150);
            groupBox22.TabIndex = 12;
            groupBox22.TabStop = false;
            groupBox22.Text = "Explosives";
            // 
            // darkLabel59
            // 
            darkLabel59.AutoSize = true;
            darkLabel59.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel59.Location = new Point(12, 115);
            darkLabel59.Margin = new Padding(4, 0, 4, 0);
            darkLabel59.Name = "darkLabel59";
            darkLabel59.Size = new Size(157, 15);
            darkLabel59.TabIndex = 9;
            darkLabel59.Text = "Projectile Damage Multiplier";
            // 
            // ProjectileDamageMultiplierNUD
            // 
            ProjectileDamageMultiplierNUD.BackColor = Color.FromArgb(60, 63, 65);
            ProjectileDamageMultiplierNUD.DecimalPlaces = 1;
            ProjectileDamageMultiplierNUD.ForeColor = SystemColors.Control;
            ProjectileDamageMultiplierNUD.Location = new Point(178, 113);
            ProjectileDamageMultiplierNUD.Margin = new Padding(4, 3, 4, 3);
            ProjectileDamageMultiplierNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            ProjectileDamageMultiplierNUD.Name = "ProjectileDamageMultiplierNUD";
            ProjectileDamageMultiplierNUD.Size = new Size(135, 23);
            ProjectileDamageMultiplierNUD.TabIndex = 10;
            ProjectileDamageMultiplierNUD.TextAlign = HorizontalAlignment.Center;
            ProjectileDamageMultiplierNUD.ValueChanged += ProjectileDamageMultiplierNUD_ValueChanged;
            // 
            // darkLabel58
            // 
            darkLabel58.AutoSize = true;
            darkLabel58.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel58.Location = new Point(12, 85);
            darkLabel58.Margin = new Padding(4, 0, 4, 0);
            darkLabel58.Name = "darkLabel58";
            darkLabel58.Size = new Size(159, 15);
            darkLabel58.TabIndex = 7;
            darkLabel58.Text = "Explosion Damage Multiplier";
            // 
            // ExplosionDamageMultiplierNUD
            // 
            ExplosionDamageMultiplierNUD.BackColor = Color.FromArgb(60, 63, 65);
            ExplosionDamageMultiplierNUD.DecimalPlaces = 1;
            ExplosionDamageMultiplierNUD.ForeColor = SystemColors.Control;
            ExplosionDamageMultiplierNUD.Location = new Point(178, 83);
            ExplosionDamageMultiplierNUD.Margin = new Padding(4, 3, 4, 3);
            ExplosionDamageMultiplierNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            ExplosionDamageMultiplierNUD.Name = "ExplosionDamageMultiplierNUD";
            ExplosionDamageMultiplierNUD.Size = new Size(135, 23);
            ExplosionDamageMultiplierNUD.TabIndex = 8;
            ExplosionDamageMultiplierNUD.TextAlign = HorizontalAlignment.Center;
            ExplosionDamageMultiplierNUD.ValueChanged += ExplosionDamageMultiplierNUD_ValueChanged;
            // 
            // darkLabel57
            // 
            darkLabel57.AutoSize = true;
            darkLabel57.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel57.Location = new Point(12, 55);
            darkLabel57.Margin = new Padding(4, 0, 4, 0);
            darkLabel57.Name = "darkLabel57";
            darkLabel57.Size = new Size(87, 15);
            darkLabel57.TabIndex = 5;
            darkLabel57.Text = "Explosion Time";
            // 
            // ExplosionTimeNUD
            // 
            ExplosionTimeNUD.BackColor = Color.FromArgb(60, 63, 65);
            ExplosionTimeNUD.DecimalPlaces = 1;
            ExplosionTimeNUD.ForeColor = SystemColors.Control;
            ExplosionTimeNUD.Location = new Point(178, 53);
            ExplosionTimeNUD.Margin = new Padding(4, 3, 4, 3);
            ExplosionTimeNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            ExplosionTimeNUD.Name = "ExplosionTimeNUD";
            ExplosionTimeNUD.Size = new Size(135, 23);
            ExplosionTimeNUD.TabIndex = 6;
            ExplosionTimeNUD.TextAlign = HorizontalAlignment.Center;
            ExplosionTimeNUD.ValueChanged += ExplosionTimeNUD_ValueChanged;
            // 
            // EnableExplosiveWhitelistCB
            // 
            EnableExplosiveWhitelistCB.AutoSize = true;
            EnableExplosiveWhitelistCB.ForeColor = SystemColors.Control;
            EnableExplosiveWhitelistCB.Location = new Point(178, 28);
            EnableExplosiveWhitelistCB.Margin = new Padding(4, 3, 4, 3);
            EnableExplosiveWhitelistCB.Name = "EnableExplosiveWhitelistCB";
            EnableExplosiveWhitelistCB.Size = new Size(15, 14);
            EnableExplosiveWhitelistCB.TabIndex = 0;
            EnableExplosiveWhitelistCB.TextAlign = ContentAlignment.MiddleRight;
            EnableExplosiveWhitelistCB.UseVisualStyleBackColor = true;
            EnableExplosiveWhitelistCB.CheckedChanged += EnableExplosiveWhitelistCB_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(220, 220, 220);
            label1.Location = new Point(12, 28);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(146, 15);
            label1.TabIndex = 11;
            label1.Text = "Enable Explosive WhiteList";
            // 
            // ExpansionRaidSettingsExplosionsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox22);
            ForeColor = SystemColors.Control;
            Name = "ExpansionRaidSettingsExplosionsControl";
            Size = new Size(335, 150);
            groupBox22.ResumeLayout(false);
            groupBox22.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ProjectileDamageMultiplierNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)ExplosionDamageMultiplierNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)ExplosionTimeNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox22;
        private Label label1;
        private Label darkLabel59;
        private NumericUpDown ProjectileDamageMultiplierNUD;
        private Label darkLabel58;
        private NumericUpDown ExplosionDamageMultiplierNUD;
        private Label darkLabel57;
        private NumericUpDown ExplosionTimeNUD;
        private CheckBox EnableExplosiveWhitelistCB;
    }
}
