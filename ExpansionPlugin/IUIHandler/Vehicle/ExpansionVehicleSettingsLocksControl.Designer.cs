namespace ExpansionPlugin
{
    partial class ExpansionVehicleSettingsLocksControl
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
            groupBox40 = new GroupBox();
            label2 = new Label();
            label1 = new Label();
            CanPickLockCB = new CheckBox();
            PickLockChancePercentNUD = new NumericUpDown();
            darkLabel119 = new Label();
            PickLockTimeSecondsNUD = new NumericUpDown();
            darkLabel120 = new Label();
            darkLabel126 = new Label();
            PickLockToolDamagePercentNUD = new NumericUpDown();
            ChangeLockToolDamagePercentNUD = new NumericUpDown();
            darkLabel121 = new Label();
            darkLabel125 = new Label();
            CanChangeLockCB = new CheckBox();
            ChangeLockTimeSecondsNUD = new NumericUpDown();
            groupBox40.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PickLockChancePercentNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PickLockTimeSecondsNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PickLockToolDamagePercentNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ChangeLockToolDamagePercentNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ChangeLockTimeSecondsNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox40
            // 
            groupBox40.Controls.Add(label2);
            groupBox40.Controls.Add(label1);
            groupBox40.Controls.Add(CanPickLockCB);
            groupBox40.Controls.Add(PickLockChancePercentNUD);
            groupBox40.Controls.Add(darkLabel119);
            groupBox40.Controls.Add(PickLockTimeSecondsNUD);
            groupBox40.Controls.Add(darkLabel120);
            groupBox40.Controls.Add(darkLabel126);
            groupBox40.Controls.Add(PickLockToolDamagePercentNUD);
            groupBox40.Controls.Add(ChangeLockToolDamagePercentNUD);
            groupBox40.Controls.Add(darkLabel121);
            groupBox40.Controls.Add(darkLabel125);
            groupBox40.Controls.Add(CanChangeLockCB);
            groupBox40.Controls.Add(ChangeLockTimeSecondsNUD);
            groupBox40.ForeColor = SystemColors.Control;
            groupBox40.Location = new Point(0, 0);
            groupBox40.Margin = new Padding(4, 3, 4, 3);
            groupBox40.Name = "groupBox40";
            groupBox40.Padding = new Padding(4, 3, 4, 3);
            groupBox40.Size = new Size(394, 246);
            groupBox40.TabIndex = 14;
            groupBox40.TabStop = false;
            groupBox40.Text = "Locks";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.FromArgb(220, 220, 220);
            label2.Location = new Point(14, 145);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(100, 15);
            label2.TabIndex = 21;
            label2.Text = "Can Change Lock";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(220, 220, 220);
            label1.Location = new Point(14, 25);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(81, 15);
            label1.TabIndex = 20;
            label1.Text = "Can Pick Lock";
            // 
            // CanPickLockCB
            // 
            CanPickLockCB.AutoSize = true;
            CanPickLockCB.ForeColor = SystemColors.Control;
            CanPickLockCB.Location = new Point(227, 26);
            CanPickLockCB.Margin = new Padding(4, 3, 4, 3);
            CanPickLockCB.Name = "CanPickLockCB";
            CanPickLockCB.Size = new Size(15, 14);
            CanPickLockCB.TabIndex = 0;
            CanPickLockCB.Tag = "CanPickLock";
            CanPickLockCB.UseVisualStyleBackColor = true;
            CanPickLockCB.CheckedChanged += CanPickLockCB_CheckedChanged;
            // 
            // PickLockChancePercentNUD
            // 
            PickLockChancePercentNUD.BackColor = Color.FromArgb(60, 63, 65);
            PickLockChancePercentNUD.DecimalPlaces = 1;
            PickLockChancePercentNUD.ForeColor = SystemColors.Control;
            PickLockChancePercentNUD.Location = new Point(227, 53);
            PickLockChancePercentNUD.Margin = new Padding(4, 3, 4, 3);
            PickLockChancePercentNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            PickLockChancePercentNUD.Name = "PickLockChancePercentNUD";
            PickLockChancePercentNUD.Size = new Size(149, 23);
            PickLockChancePercentNUD.TabIndex = 6;
            PickLockChancePercentNUD.Tag = "PickLockChancePercent";
            PickLockChancePercentNUD.TextAlign = HorizontalAlignment.Center;
            PickLockChancePercentNUD.ValueChanged += PickLockChancePercentNUD_ValueChanged;
            // 
            // darkLabel119
            // 
            darkLabel119.AutoSize = true;
            darkLabel119.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel119.Location = new Point(14, 55);
            darkLabel119.Margin = new Padding(4, 0, 4, 0);
            darkLabel119.Name = "darkLabel119";
            darkLabel119.Size = new Size(143, 15);
            darkLabel119.TabIndex = 5;
            darkLabel119.Text = "Pick Lock Chance Percent";
            // 
            // PickLockTimeSecondsNUD
            // 
            PickLockTimeSecondsNUD.BackColor = Color.FromArgb(60, 63, 65);
            PickLockTimeSecondsNUD.ForeColor = SystemColors.Control;
            PickLockTimeSecondsNUD.Location = new Point(227, 83);
            PickLockTimeSecondsNUD.Margin = new Padding(4, 3, 4, 3);
            PickLockTimeSecondsNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            PickLockTimeSecondsNUD.Name = "PickLockTimeSecondsNUD";
            PickLockTimeSecondsNUD.Size = new Size(149, 23);
            PickLockTimeSecondsNUD.TabIndex = 8;
            PickLockTimeSecondsNUD.Tag = "PickLockTimeSeconds";
            PickLockTimeSecondsNUD.TextAlign = HorizontalAlignment.Center;
            PickLockTimeSecondsNUD.ValueChanged += PickLockTimeSecondsNUD_ValueChanged;
            // 
            // darkLabel120
            // 
            darkLabel120.AutoSize = true;
            darkLabel120.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel120.Location = new Point(14, 85);
            darkLabel120.Margin = new Padding(4, 0, 4, 0);
            darkLabel120.Name = "darkLabel120";
            darkLabel120.Size = new Size(133, 15);
            darkLabel120.TabIndex = 7;
            darkLabel120.Text = "Pick Lock Time Seconds";
            // 
            // darkLabel126
            // 
            darkLabel126.AutoSize = true;
            darkLabel126.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel126.Location = new Point(14, 205);
            darkLabel126.Margin = new Padding(4, 0, 4, 0);
            darkLabel126.Name = "darkLabel126";
            darkLabel126.Size = new Size(191, 15);
            darkLabel126.TabIndex = 18;
            darkLabel126.Text = "Change Lock Tool Damage Percent";
            // 
            // PickLockToolDamagePercentNUD
            // 
            PickLockToolDamagePercentNUD.BackColor = Color.FromArgb(60, 63, 65);
            PickLockToolDamagePercentNUD.DecimalPlaces = 1;
            PickLockToolDamagePercentNUD.ForeColor = SystemColors.Control;
            PickLockToolDamagePercentNUD.Location = new Point(227, 113);
            PickLockToolDamagePercentNUD.Margin = new Padding(4, 3, 4, 3);
            PickLockToolDamagePercentNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            PickLockToolDamagePercentNUD.Name = "PickLockToolDamagePercentNUD";
            PickLockToolDamagePercentNUD.Size = new Size(149, 23);
            PickLockToolDamagePercentNUD.TabIndex = 10;
            PickLockToolDamagePercentNUD.Tag = "PickLockToolDamagePercent";
            PickLockToolDamagePercentNUD.TextAlign = HorizontalAlignment.Center;
            PickLockToolDamagePercentNUD.ValueChanged += PickLockToolDamagePercentNUD_ValueChanged;
            // 
            // ChangeLockToolDamagePercentNUD
            // 
            ChangeLockToolDamagePercentNUD.BackColor = Color.FromArgb(60, 63, 65);
            ChangeLockToolDamagePercentNUD.DecimalPlaces = 1;
            ChangeLockToolDamagePercentNUD.ForeColor = SystemColors.Control;
            ChangeLockToolDamagePercentNUD.Location = new Point(227, 203);
            ChangeLockToolDamagePercentNUD.Margin = new Padding(4, 3, 4, 3);
            ChangeLockToolDamagePercentNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            ChangeLockToolDamagePercentNUD.Name = "ChangeLockToolDamagePercentNUD";
            ChangeLockToolDamagePercentNUD.Size = new Size(149, 23);
            ChangeLockToolDamagePercentNUD.TabIndex = 19;
            ChangeLockToolDamagePercentNUD.Tag = "ChangeLockToolDamagePercent";
            ChangeLockToolDamagePercentNUD.TextAlign = HorizontalAlignment.Center;
            ChangeLockToolDamagePercentNUD.ValueChanged += ChangeLockToolDamagePercentNUD_ValueChanged;
            // 
            // darkLabel121
            // 
            darkLabel121.AutoSize = true;
            darkLabel121.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel121.Location = new Point(14, 115);
            darkLabel121.Margin = new Padding(4, 0, 4, 0);
            darkLabel121.Name = "darkLabel121";
            darkLabel121.Size = new Size(172, 15);
            darkLabel121.TabIndex = 9;
            darkLabel121.Text = "Pick Lock Tool Damage Percent";
            // 
            // darkLabel125
            // 
            darkLabel125.AutoSize = true;
            darkLabel125.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel125.Location = new Point(14, 175);
            darkLabel125.Margin = new Padding(4, 0, 4, 0);
            darkLabel125.Name = "darkLabel125";
            darkLabel125.Size = new Size(152, 15);
            darkLabel125.TabIndex = 16;
            darkLabel125.Text = "Change Lock Time Seconds";
            // 
            // CanChangeLockCB
            // 
            CanChangeLockCB.AutoSize = true;
            CanChangeLockCB.ForeColor = SystemColors.Control;
            CanChangeLockCB.Location = new Point(227, 146);
            CanChangeLockCB.Margin = new Padding(4, 3, 4, 3);
            CanChangeLockCB.Name = "CanChangeLockCB";
            CanChangeLockCB.Size = new Size(15, 14);
            CanChangeLockCB.TabIndex = 11;
            CanChangeLockCB.Tag = "CanChangeLock";
            CanChangeLockCB.UseVisualStyleBackColor = true;
            CanChangeLockCB.CheckedChanged += CanChangeLockCB_CheckedChanged;
            // 
            // ChangeLockTimeSecondsNUD
            // 
            ChangeLockTimeSecondsNUD.BackColor = Color.FromArgb(60, 63, 65);
            ChangeLockTimeSecondsNUD.ForeColor = SystemColors.Control;
            ChangeLockTimeSecondsNUD.Location = new Point(227, 173);
            ChangeLockTimeSecondsNUD.Margin = new Padding(4, 3, 4, 3);
            ChangeLockTimeSecondsNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            ChangeLockTimeSecondsNUD.Name = "ChangeLockTimeSecondsNUD";
            ChangeLockTimeSecondsNUD.Size = new Size(149, 23);
            ChangeLockTimeSecondsNUD.TabIndex = 17;
            ChangeLockTimeSecondsNUD.Tag = "ChangeLockTimeSeconds";
            ChangeLockTimeSecondsNUD.TextAlign = HorizontalAlignment.Center;
            ChangeLockTimeSecondsNUD.ValueChanged += ChangeLockTimeSecondsNUD_ValueChanged;
            // 
            // ExpansionVehicleSettingsLocksControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox40);
            ForeColor = SystemColors.Control;
            Name = "ExpansionVehicleSettingsLocksControl";
            Size = new Size(394, 246);
            groupBox40.ResumeLayout(false);
            groupBox40.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PickLockChancePercentNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)PickLockTimeSecondsNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)PickLockToolDamagePercentNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)ChangeLockToolDamagePercentNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)ChangeLockTimeSecondsNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox40;
        private Label label2;
        private Label label1;
        private CheckBox CanPickLockCB;
        private NumericUpDown PickLockChancePercentNUD;
        private Label darkLabel119;
        private NumericUpDown PickLockTimeSecondsNUD;
        private Label darkLabel120;
        private Label darkLabel126;
        private NumericUpDown PickLockToolDamagePercentNUD;
        private NumericUpDown ChangeLockToolDamagePercentNUD;
        private Label darkLabel121;
        private Label darkLabel125;
        private CheckBox CanChangeLockCB;
        private NumericUpDown ChangeLockTimeSecondsNUD;
    }
}
