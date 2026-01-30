namespace ExpansionPlugin
{
    partial class ExpansionRaidSettingsSafesControl
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
            groupBox23 = new GroupBox();
            SafeRaidUseScheduleCB = new CheckBox();
            darkLabel66 = new Label();
            SafeRaidToolDamagePercentNUD = new NumericUpDown();
            darkLabel65 = new Label();
            SafeRaidToolCyclesNUD = new NumericUpDown();
            darkLabel62 = new Label();
            SafeRaidToolTimeSecondsNUD = new NumericUpDown();
            darkLabel63 = new Label();
            SafeProjectileDamageMultiplierNUD = new NumericUpDown();
            darkLabel64 = new Label();
            SafeExplosionDamageMultiplierNUD = new NumericUpDown();
            CanRaidSafesCB = new CheckBox();
            label1 = new Label();
            label2 = new Label();
            groupBox23.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SafeRaidToolDamagePercentNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SafeRaidToolCyclesNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SafeRaidToolTimeSecondsNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SafeProjectileDamageMultiplierNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SafeExplosionDamageMultiplierNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox23
            // 
            groupBox23.Controls.Add(label2);
            groupBox23.Controls.Add(label1);
            groupBox23.Controls.Add(SafeRaidUseScheduleCB);
            groupBox23.Controls.Add(darkLabel66);
            groupBox23.Controls.Add(SafeRaidToolDamagePercentNUD);
            groupBox23.Controls.Add(darkLabel65);
            groupBox23.Controls.Add(SafeRaidToolCyclesNUD);
            groupBox23.Controls.Add(darkLabel62);
            groupBox23.Controls.Add(SafeRaidToolTimeSecondsNUD);
            groupBox23.Controls.Add(darkLabel63);
            groupBox23.Controls.Add(SafeProjectileDamageMultiplierNUD);
            groupBox23.Controls.Add(darkLabel64);
            groupBox23.Controls.Add(SafeExplosionDamageMultiplierNUD);
            groupBox23.Controls.Add(CanRaidSafesCB);
            groupBox23.ForeColor = SystemColors.Control;
            groupBox23.Location = new Point(0, 0);
            groupBox23.Margin = new Padding(4, 3, 4, 3);
            groupBox23.Name = "groupBox23";
            groupBox23.Padding = new Padding(4, 3, 4, 3);
            groupBox23.Size = new Size(353, 229);
            groupBox23.TabIndex = 15;
            groupBox23.TabStop = false;
            groupBox23.Text = "Safes";
            // 
            // SafeRaidUseScheduleCB
            // 
            SafeRaidUseScheduleCB.AutoSize = true;
            SafeRaidUseScheduleCB.ForeColor = SystemColors.Control;
            SafeRaidUseScheduleCB.Location = new Point(211, 47);
            SafeRaidUseScheduleCB.Margin = new Padding(4, 3, 4, 3);
            SafeRaidUseScheduleCB.Name = "SafeRaidUseScheduleCB";
            SafeRaidUseScheduleCB.Size = new Size(15, 14);
            SafeRaidUseScheduleCB.TabIndex = 1;
            SafeRaidUseScheduleCB.TextAlign = ContentAlignment.MiddleRight;
            SafeRaidUseScheduleCB.UseVisualStyleBackColor = true;
            SafeRaidUseScheduleCB.CheckedChanged += SafeRaidUseScheduleCB_CheckedChanged;
            // 
            // darkLabel66
            // 
            darkLabel66.AutoSize = true;
            darkLabel66.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel66.Location = new Point(11, 194);
            darkLabel66.Margin = new Padding(4, 0, 4, 0);
            darkLabel66.Name = "darkLabel66";
            darkLabel66.Size = new Size(170, 15);
            darkLabel66.TabIndex = 14;
            darkLabel66.Text = "Safe Raid Tool Damage Percent";
            // 
            // SafeRaidToolDamagePercentNUD
            // 
            SafeRaidToolDamagePercentNUD.BackColor = Color.FromArgb(60, 63, 65);
            SafeRaidToolDamagePercentNUD.DecimalPlaces = 1;
            SafeRaidToolDamagePercentNUD.ForeColor = SystemColors.Control;
            SafeRaidToolDamagePercentNUD.Location = new Point(211, 192);
            SafeRaidToolDamagePercentNUD.Margin = new Padding(4, 3, 4, 3);
            SafeRaidToolDamagePercentNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            SafeRaidToolDamagePercentNUD.Name = "SafeRaidToolDamagePercentNUD";
            SafeRaidToolDamagePercentNUD.Size = new Size(126, 23);
            SafeRaidToolDamagePercentNUD.TabIndex = 15;
            SafeRaidToolDamagePercentNUD.TextAlign = HorizontalAlignment.Center;
            SafeRaidToolDamagePercentNUD.ValueChanged += SafeRaidToolDamagePercentNUD_ValueChanged;
            // 
            // darkLabel65
            // 
            darkLabel65.AutoSize = true;
            darkLabel65.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel65.Location = new Point(11, 164);
            darkLabel65.Margin = new Padding(4, 0, 4, 0);
            darkLabel65.Name = "darkLabel65";
            darkLabel65.Size = new Size(117, 15);
            darkLabel65.TabIndex = 12;
            darkLabel65.Text = "Safe Raid Tool Cycles";
            // 
            // SafeRaidToolCyclesNUD
            // 
            SafeRaidToolCyclesNUD.BackColor = Color.FromArgb(60, 63, 65);
            SafeRaidToolCyclesNUD.ForeColor = SystemColors.Control;
            SafeRaidToolCyclesNUD.Location = new Point(211, 162);
            SafeRaidToolCyclesNUD.Margin = new Padding(4, 3, 4, 3);
            SafeRaidToolCyclesNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            SafeRaidToolCyclesNUD.Name = "SafeRaidToolCyclesNUD";
            SafeRaidToolCyclesNUD.Size = new Size(126, 23);
            SafeRaidToolCyclesNUD.TabIndex = 13;
            SafeRaidToolCyclesNUD.TextAlign = HorizontalAlignment.Center;
            SafeRaidToolCyclesNUD.ValueChanged += SafeRaidToolCyclesNUD_ValueChanged;
            // 
            // darkLabel62
            // 
            darkLabel62.AutoSize = true;
            darkLabel62.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel62.Location = new Point(11, 134);
            darkLabel62.Margin = new Padding(4, 0, 4, 0);
            darkLabel62.Name = "darkLabel62";
            darkLabel62.Size = new Size(156, 15);
            darkLabel62.TabIndex = 10;
            darkLabel62.Text = "Safe Raid Tool Time Seconds";
            // 
            // SafeRaidToolTimeSecondsNUD
            // 
            SafeRaidToolTimeSecondsNUD.BackColor = Color.FromArgb(60, 63, 65);
            SafeRaidToolTimeSecondsNUD.ForeColor = SystemColors.Control;
            SafeRaidToolTimeSecondsNUD.Location = new Point(211, 132);
            SafeRaidToolTimeSecondsNUD.Margin = new Padding(4, 3, 4, 3);
            SafeRaidToolTimeSecondsNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            SafeRaidToolTimeSecondsNUD.Name = "SafeRaidToolTimeSecondsNUD";
            SafeRaidToolTimeSecondsNUD.Size = new Size(126, 23);
            SafeRaidToolTimeSecondsNUD.TabIndex = 11;
            SafeRaidToolTimeSecondsNUD.TextAlign = HorizontalAlignment.Center;
            SafeRaidToolTimeSecondsNUD.ValueChanged += SafeRaidToolTimeSecondsNUD_ValueChanged;
            // 
            // darkLabel63
            // 
            darkLabel63.AutoSize = true;
            darkLabel63.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel63.Location = new Point(11, 48);
            darkLabel63.Margin = new Padding(4, 0, 4, 0);
            darkLabel63.Name = "darkLabel63";
            darkLabel63.Size = new Size(128, 15);
            darkLabel63.TabIndex = 8;
            darkLabel63.Text = "Safe Raid Use Schedule";
            // 
            // SafeProjectileDamageMultiplierNUD
            // 
            SafeProjectileDamageMultiplierNUD.BackColor = Color.FromArgb(60, 63, 65);
            SafeProjectileDamageMultiplierNUD.DecimalPlaces = 1;
            SafeProjectileDamageMultiplierNUD.ForeColor = SystemColors.Control;
            SafeProjectileDamageMultiplierNUD.Location = new Point(211, 102);
            SafeProjectileDamageMultiplierNUD.Margin = new Padding(4, 3, 4, 3);
            SafeProjectileDamageMultiplierNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            SafeProjectileDamageMultiplierNUD.Name = "SafeProjectileDamageMultiplierNUD";
            SafeProjectileDamageMultiplierNUD.Size = new Size(126, 23);
            SafeProjectileDamageMultiplierNUD.TabIndex = 9;
            SafeProjectileDamageMultiplierNUD.TextAlign = HorizontalAlignment.Center;
            SafeProjectileDamageMultiplierNUD.ValueChanged += SafeProjectileDamageMultiplierNUD_ValueChanged;
            // 
            // darkLabel64
            // 
            darkLabel64.AutoSize = true;
            darkLabel64.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel64.Location = new Point(11, 74);
            darkLabel64.Margin = new Padding(4, 0, 4, 0);
            darkLabel64.Name = "darkLabel64";
            darkLabel64.Size = new Size(184, 15);
            darkLabel64.TabIndex = 6;
            darkLabel64.Text = "Safe Explosion Damage Multiplier";
            // 
            // SafeExplosionDamageMultiplierNUD
            // 
            SafeExplosionDamageMultiplierNUD.BackColor = Color.FromArgb(60, 63, 65);
            SafeExplosionDamageMultiplierNUD.DecimalPlaces = 1;
            SafeExplosionDamageMultiplierNUD.ForeColor = SystemColors.Control;
            SafeExplosionDamageMultiplierNUD.Location = new Point(211, 72);
            SafeExplosionDamageMultiplierNUD.Margin = new Padding(4, 3, 4, 3);
            SafeExplosionDamageMultiplierNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            SafeExplosionDamageMultiplierNUD.Name = "SafeExplosionDamageMultiplierNUD";
            SafeExplosionDamageMultiplierNUD.Size = new Size(126, 23);
            SafeExplosionDamageMultiplierNUD.TabIndex = 7;
            SafeExplosionDamageMultiplierNUD.TextAlign = HorizontalAlignment.Center;
            SafeExplosionDamageMultiplierNUD.ValueChanged += SafeExplosionDamageMultiplierNUD_ValueChanged;
            // 
            // CanRaidSafesCB
            // 
            CanRaidSafesCB.AutoSize = true;
            CanRaidSafesCB.ForeColor = SystemColors.Control;
            CanRaidSafesCB.Location = new Point(211, 22);
            CanRaidSafesCB.Margin = new Padding(4, 3, 4, 3);
            CanRaidSafesCB.Name = "CanRaidSafesCB";
            CanRaidSafesCB.Size = new Size(15, 14);
            CanRaidSafesCB.TabIndex = 0;
            CanRaidSafesCB.TextAlign = ContentAlignment.MiddleRight;
            CanRaidSafesCB.UseVisualStyleBackColor = true;
            CanRaidSafesCB.CheckedChanged += CanRaidSafesCB_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(220, 220, 220);
            label1.Location = new Point(11, 104);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(184, 15);
            label1.TabIndex = 16;
            label1.Text = "Safe Explosion Damage Multiplier";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.FromArgb(220, 220, 220);
            label2.Location = new Point(11, 23);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(84, 15);
            label2.TabIndex = 17;
            label2.Text = "Can Raid Safes";
            // 
            // ExpansionRaidSettingsSafesControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox23);
            ForeColor = SystemColors.Control;
            Name = "ExpansionRaidSettingsSafesControl";
            Size = new Size(353, 229);
            groupBox23.ResumeLayout(false);
            groupBox23.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SafeRaidToolDamagePercentNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)SafeRaidToolCyclesNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)SafeRaidToolTimeSecondsNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)SafeProjectileDamageMultiplierNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)SafeExplosionDamageMultiplierNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox23;
        private Label label2;
        private Label label1;
        private CheckBox SafeRaidUseScheduleCB;
        private Label darkLabel66;
        private NumericUpDown SafeRaidToolDamagePercentNUD;
        private Label darkLabel65;
        private NumericUpDown SafeRaidToolCyclesNUD;
        private Label darkLabel62;
        private NumericUpDown SafeRaidToolTimeSecondsNUD;
        private Label darkLabel63;
        private NumericUpDown SafeProjectileDamageMultiplierNUD;
        private Label darkLabel64;
        private NumericUpDown SafeExplosionDamageMultiplierNUD;
        private CheckBox CanRaidSafesCB;
    }
}
