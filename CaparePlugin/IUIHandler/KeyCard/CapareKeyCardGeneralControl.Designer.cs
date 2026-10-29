namespace CaparePlugin
{
    partial class CapareKeyCardGeneralControl
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
            LockedDoorsEnabledL = new Label();
            LockedDoorsEnabledCB = new CheckBox();
            DebugModeEnabledL = new Label();
            DebugModeEnabledCB = new CheckBox();
            AdminLoggingEnabledL = new Label();
            AdminLoggingEnabledCB = new CheckBox();
            groupBox1.SuspendLayout();

            SuspendLayout();
            //
            // groupBox1
            //
            groupBox1.Controls.Add(LockedDoorsEnabledL);
            groupBox1.Controls.Add(LockedDoorsEnabledCB);
            groupBox1.Controls.Add(DebugModeEnabledL);
            groupBox1.Controls.Add(DebugModeEnabledCB);
            groupBox1.Controls.Add(AdminLoggingEnabledL);
            groupBox1.Controls.Add(AdminLoggingEnabledCB);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(435, 115);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "General";
            //
            // LockedDoorsEnabledL
            //
            LockedDoorsEnabledL.AutoSize = true;
            LockedDoorsEnabledL.ForeColor = Color.FromArgb(220, 220, 220);
            LockedDoorsEnabledL.Location = new Point(11, 25);
            LockedDoorsEnabledL.Margin = new Padding(4, 0, 4, 0);
            LockedDoorsEnabledL.Name = "LockedDoorsEnabledL";
            LockedDoorsEnabledL.Size = new Size(140, 15);
            LockedDoorsEnabledL.TabIndex = 0;
            LockedDoorsEnabledL.Text = "Locked Doors Enabled";
            //
            // LockedDoorsEnabledCB
            //
            LockedDoorsEnabledCB.AutoSize = true;
            LockedDoorsEnabledCB.Location = new Point(190, 25);
            LockedDoorsEnabledCB.Margin = new Padding(4, 3, 4, 3);
            LockedDoorsEnabledCB.Name = "LockedDoorsEnabledCB";
            LockedDoorsEnabledCB.Size = new Size(15, 14);
            LockedDoorsEnabledCB.TabIndex = 1;
            LockedDoorsEnabledCB.UseVisualStyleBackColor = true;
            LockedDoorsEnabledCB.CheckedChanged += LockedDoorsEnabledCB_CheckedChanged;
            //
            // DebugModeEnabledL
            //
            DebugModeEnabledL.AutoSize = true;
            DebugModeEnabledL.ForeColor = Color.FromArgb(220, 220, 220);
            DebugModeEnabledL.Location = new Point(11, 54);
            DebugModeEnabledL.Margin = new Padding(4, 0, 4, 0);
            DebugModeEnabledL.Name = "DebugModeEnabledL";
            DebugModeEnabledL.Size = new Size(126, 15);
            DebugModeEnabledL.TabIndex = 2;
            DebugModeEnabledL.Text = "Debug Mode Enabled";
            //
            // DebugModeEnabledCB
            //
            DebugModeEnabledCB.AutoSize = true;
            DebugModeEnabledCB.Location = new Point(190, 54);
            DebugModeEnabledCB.Margin = new Padding(4, 3, 4, 3);
            DebugModeEnabledCB.Name = "DebugModeEnabledCB";
            DebugModeEnabledCB.Size = new Size(15, 14);
            DebugModeEnabledCB.TabIndex = 3;
            DebugModeEnabledCB.UseVisualStyleBackColor = true;
            DebugModeEnabledCB.CheckedChanged += DebugModeEnabledCB_CheckedChanged;
            //
            // AdminLoggingEnabledL
            //
            AdminLoggingEnabledL.AutoSize = true;
            AdminLoggingEnabledL.ForeColor = Color.FromArgb(220, 220, 220);
            AdminLoggingEnabledL.Location = new Point(11, 83);
            AdminLoggingEnabledL.Margin = new Padding(4, 0, 4, 0);
            AdminLoggingEnabledL.Name = "AdminLoggingEnabledL";
            AdminLoggingEnabledL.Size = new Size(147, 15);
            AdminLoggingEnabledL.TabIndex = 4;
            AdminLoggingEnabledL.Text = "Admin Logging Enabled";
            //
            // AdminLoggingEnabledCB
            //
            AdminLoggingEnabledCB.AutoSize = true;
            AdminLoggingEnabledCB.Location = new Point(190, 83);
            AdminLoggingEnabledCB.Margin = new Padding(4, 3, 4, 3);
            AdminLoggingEnabledCB.Name = "AdminLoggingEnabledCB";
            AdminLoggingEnabledCB.Size = new Size(15, 14);
            AdminLoggingEnabledCB.TabIndex = 5;
            AdminLoggingEnabledCB.UseVisualStyleBackColor = true;
            AdminLoggingEnabledCB.CheckedChanged += AdminLoggingEnabledCB_CheckedChanged;
            //
            // CapareKeyCardGeneralControl
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "CapareKeyCardGeneralControl";
            Size = new Size(435, 121);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();

            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label LockedDoorsEnabledL;
        private CheckBox LockedDoorsEnabledCB;
        private Label DebugModeEnabledL;
        private CheckBox DebugModeEnabledCB;
        private Label AdminLoggingEnabledL;
        private CheckBox AdminLoggingEnabledCB;
    }
}
