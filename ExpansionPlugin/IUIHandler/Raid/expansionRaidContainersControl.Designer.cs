namespace ExpansionPlugin
{
    partial class expansionRaidContainersControl
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
            groupBox65 = new GroupBox();
            LockOnContainerRaidUseScheduleCB = new CheckBox();
            darkLabel162 = new Label();
            LockOnContainerRaidToolDamagePercentNUD = new NumericUpDown();
            darkLabel168 = new Label();
            LockOnContainerRaidToolCyclesNUD = new NumericUpDown();
            darkLabel170 = new Label();
            LockOnContainerRaidToolTimeSecondsNUD = new NumericUpDown();
            CanRaidLocksOnContainersCB = new CheckBox();
            label1 = new Label();
            label2 = new Label();
            groupBox65.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LockOnContainerRaidToolDamagePercentNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LockOnContainerRaidToolCyclesNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LockOnContainerRaidToolTimeSecondsNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox65
            // 
            groupBox65.Controls.Add(label2);
            groupBox65.Controls.Add(label1);
            groupBox65.Controls.Add(LockOnContainerRaidUseScheduleCB);
            groupBox65.Controls.Add(darkLabel162);
            groupBox65.Controls.Add(LockOnContainerRaidToolDamagePercentNUD);
            groupBox65.Controls.Add(darkLabel168);
            groupBox65.Controls.Add(LockOnContainerRaidToolCyclesNUD);
            groupBox65.Controls.Add(darkLabel170);
            groupBox65.Controls.Add(LockOnContainerRaidToolTimeSecondsNUD);
            groupBox65.Controls.Add(CanRaidLocksOnContainersCB);
            groupBox65.ForeColor = SystemColors.Control;
            groupBox65.Location = new Point(0, 0);
            groupBox65.Margin = new Padding(4, 3, 4, 3);
            groupBox65.Name = "groupBox65";
            groupBox65.Padding = new Padding(4, 3, 4, 3);
            groupBox65.Size = new Size(421, 174);
            groupBox65.TabIndex = 16;
            groupBox65.TabStop = false;
            groupBox65.Text = "Containers";
            // 
            // LockOnContainerRaidUseScheduleCB
            // 
            LockOnContainerRaidUseScheduleCB.AutoSize = true;
            LockOnContainerRaidUseScheduleCB.ForeColor = SystemColors.Control;
            LockOnContainerRaidUseScheduleCB.Location = new Point(288, 53);
            LockOnContainerRaidUseScheduleCB.Margin = new Padding(4, 3, 4, 3);
            LockOnContainerRaidUseScheduleCB.Name = "LockOnContainerRaidUseScheduleCB";
            LockOnContainerRaidUseScheduleCB.Size = new Size(15, 14);
            LockOnContainerRaidUseScheduleCB.TabIndex = 1;
            LockOnContainerRaidUseScheduleCB.TextAlign = ContentAlignment.MiddleRight;
            LockOnContainerRaidUseScheduleCB.UseVisualStyleBackColor = true;
            LockOnContainerRaidUseScheduleCB.CheckedChanged += LockOnContainerRaidUseScheduleCB_CheckedChanged;
            // 
            // darkLabel162
            // 
            darkLabel162.AutoSize = true;
            darkLabel162.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel162.Location = new Point(12, 138);
            darkLabel162.Margin = new Padding(4, 0, 4, 0);
            darkLabel162.Name = "darkLabel162";
            darkLabel162.Size = new Size(247, 15);
            darkLabel162.TabIndex = 10;
            darkLabel162.Text = "Lock On Container Raid Tool Damage Percent";
            // 
            // LockOnContainerRaidToolDamagePercentNUD
            // 
            LockOnContainerRaidToolDamagePercentNUD.BackColor = Color.FromArgb(60, 63, 65);
            LockOnContainerRaidToolDamagePercentNUD.DecimalPlaces = 1;
            LockOnContainerRaidToolDamagePercentNUD.ForeColor = SystemColors.Control;
            LockOnContainerRaidToolDamagePercentNUD.Location = new Point(288, 136);
            LockOnContainerRaidToolDamagePercentNUD.Margin = new Padding(4, 3, 4, 3);
            LockOnContainerRaidToolDamagePercentNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            LockOnContainerRaidToolDamagePercentNUD.Name = "LockOnContainerRaidToolDamagePercentNUD";
            LockOnContainerRaidToolDamagePercentNUD.Size = new Size(121, 23);
            LockOnContainerRaidToolDamagePercentNUD.TabIndex = 11;
            LockOnContainerRaidToolDamagePercentNUD.TextAlign = HorizontalAlignment.Center;
            LockOnContainerRaidToolDamagePercentNUD.ValueChanged += LockOnContainerRaidToolDamagePercentNUD_ValueChanged;
            // 
            // darkLabel168
            // 
            darkLabel168.AutoSize = true;
            darkLabel168.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel168.Location = new Point(12, 109);
            darkLabel168.Margin = new Padding(4, 0, 4, 0);
            darkLabel168.Name = "darkLabel168";
            darkLabel168.Size = new Size(194, 15);
            darkLabel168.TabIndex = 8;
            darkLabel168.Text = "Lock On Container Raid Tool Cycles";
            // 
            // LockOnContainerRaidToolCyclesNUD
            // 
            LockOnContainerRaidToolCyclesNUD.BackColor = Color.FromArgb(60, 63, 65);
            LockOnContainerRaidToolCyclesNUD.ForeColor = SystemColors.Control;
            LockOnContainerRaidToolCyclesNUD.Location = new Point(288, 107);
            LockOnContainerRaidToolCyclesNUD.Margin = new Padding(4, 3, 4, 3);
            LockOnContainerRaidToolCyclesNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            LockOnContainerRaidToolCyclesNUD.Name = "LockOnContainerRaidToolCyclesNUD";
            LockOnContainerRaidToolCyclesNUD.Size = new Size(121, 23);
            LockOnContainerRaidToolCyclesNUD.TabIndex = 9;
            LockOnContainerRaidToolCyclesNUD.TextAlign = HorizontalAlignment.Center;
            LockOnContainerRaidToolCyclesNUD.ValueChanged += LockOnContainerRaidToolCyclesNUD_ValueChanged;
            // 
            // darkLabel170
            // 
            darkLabel170.AutoSize = true;
            darkLabel170.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel170.Location = new Point(12, 78);
            darkLabel170.Margin = new Padding(4, 0, 4, 0);
            darkLabel170.Name = "darkLabel170";
            darkLabel170.Size = new Size(233, 15);
            darkLabel170.TabIndex = 6;
            darkLabel170.Text = "Lock On Container Raid Tool Time Seconds";
            // 
            // LockOnContainerRaidToolTimeSecondsNUD
            // 
            LockOnContainerRaidToolTimeSecondsNUD.BackColor = Color.FromArgb(60, 63, 65);
            LockOnContainerRaidToolTimeSecondsNUD.ForeColor = SystemColors.Control;
            LockOnContainerRaidToolTimeSecondsNUD.Location = new Point(288, 78);
            LockOnContainerRaidToolTimeSecondsNUD.Margin = new Padding(4, 3, 4, 3);
            LockOnContainerRaidToolTimeSecondsNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            LockOnContainerRaidToolTimeSecondsNUD.Name = "LockOnContainerRaidToolTimeSecondsNUD";
            LockOnContainerRaidToolTimeSecondsNUD.Size = new Size(121, 23);
            LockOnContainerRaidToolTimeSecondsNUD.TabIndex = 7;
            LockOnContainerRaidToolTimeSecondsNUD.TextAlign = HorizontalAlignment.Center;
            LockOnContainerRaidToolTimeSecondsNUD.ValueChanged += LockOnContainerRaidToolTimeSecondsNUD_ValueChanged;
            // 
            // CanRaidLocksOnContainersCB
            // 
            CanRaidLocksOnContainersCB.AutoSize = true;
            CanRaidLocksOnContainersCB.ForeColor = SystemColors.Control;
            CanRaidLocksOnContainersCB.Location = new Point(288, 28);
            CanRaidLocksOnContainersCB.Margin = new Padding(4, 3, 4, 3);
            CanRaidLocksOnContainersCB.Name = "CanRaidLocksOnContainersCB";
            CanRaidLocksOnContainersCB.Size = new Size(15, 14);
            CanRaidLocksOnContainersCB.TabIndex = 0;
            CanRaidLocksOnContainersCB.TextAlign = ContentAlignment.MiddleRight;
            CanRaidLocksOnContainersCB.UseVisualStyleBackColor = true;
            CanRaidLocksOnContainersCB.CheckedChanged += CanRaidLocksOnContainersCB_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(220, 220, 220);
            label1.Location = new Point(12, 54);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(205, 15);
            label1.TabIndex = 12;
            label1.Text = "Lock On Container Raid Use Schedule";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.FromArgb(220, 220, 220);
            label2.Location = new Point(12, 29);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(171, 15);
            label2.TabIndex = 13;
            label2.Text = "Can Raid Locks On Containerss";
            // 
            // expansionRaidContainersControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox65);
            ForeColor = SystemColors.Control;
            Name = "expansionRaidContainersControl";
            Size = new Size(421, 174);
            groupBox65.ResumeLayout(false);
            groupBox65.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)LockOnContainerRaidToolDamagePercentNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)LockOnContainerRaidToolCyclesNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)LockOnContainerRaidToolTimeSecondsNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox65;
        private Label label2;
        private Label label1;
        private CheckBox LockOnContainerRaidUseScheduleCB;
        private Label darkLabel162;
        private NumericUpDown LockOnContainerRaidToolDamagePercentNUD;
        private Label darkLabel168;
        private NumericUpDown LockOnContainerRaidToolCyclesNUD;
        private Label darkLabel170;
        private NumericUpDown LockOnContainerRaidToolTimeSecondsNUD;
        private CheckBox CanRaidLocksOnContainersCB;
    }
}
