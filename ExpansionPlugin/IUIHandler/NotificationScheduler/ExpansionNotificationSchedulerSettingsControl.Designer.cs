namespace ExpansionPlugin
{
    partial class ExpansionNotificationSchedulerSettingsControl
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
            groupBox63 = new GroupBox();
            UseMissionTimeCB = new CheckBox();
            UTCTimeCB = new CheckBox();
            SchedulerEnabledCB = new CheckBox();
            groupBox63.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox63
            // 
            groupBox63.Controls.Add(UseMissionTimeCB);
            groupBox63.Controls.Add(UTCTimeCB);
            groupBox63.Controls.Add(SchedulerEnabledCB);
            groupBox63.ForeColor = SystemColors.Control;
            groupBox63.Location = new Point(0, 0);
            groupBox63.Margin = new Padding(4, 3, 4, 3);
            groupBox63.Name = "groupBox63";
            groupBox63.Padding = new Padding(4, 3, 4, 3);
            groupBox63.Size = new Size(179, 106);
            groupBox63.TabIndex = 12;
            groupBox63.TabStop = false;
            groupBox63.Text = "Notification Settings";
            // 
            // UseMissionTimeCB
            // 
            UseMissionTimeCB.AutoSize = true;
            UseMissionTimeCB.Location = new Point(25, 72);
            UseMissionTimeCB.Margin = new Padding(4, 3, 4, 3);
            UseMissionTimeCB.Name = "UseMissionTimeCB";
            UseMissionTimeCB.Size = new Size(118, 19);
            UseMissionTimeCB.TabIndex = 2;
            UseMissionTimeCB.Tag = "";
            UseMissionTimeCB.Text = "Use Mission Time";
            UseMissionTimeCB.UseVisualStyleBackColor = true;
            UseMissionTimeCB.CheckedChanged += UseMissionTimeCB_CheckedChanged;
            // 
            // UTCTimeCB
            // 
            UTCTimeCB.AutoSize = true;
            UTCTimeCB.Location = new Point(25, 47);
            UTCTimeCB.Margin = new Padding(4, 3, 4, 3);
            UTCTimeCB.Name = "UTCTimeCB";
            UTCTimeCB.Size = new Size(76, 19);
            UTCTimeCB.TabIndex = 1;
            UTCTimeCB.Tag = "";
            UTCTimeCB.Text = "UTC Time";
            UTCTimeCB.UseVisualStyleBackColor = true;
            UTCTimeCB.CheckedChanged += UTCTimeCB_CheckedChanged;
            // 
            // SchedulerEnabledCB
            // 
            SchedulerEnabledCB.AutoSize = true;
            SchedulerEnabledCB.Location = new Point(25, 22);
            SchedulerEnabledCB.Margin = new Padding(4, 3, 4, 3);
            SchedulerEnabledCB.Name = "SchedulerEnabledCB";
            SchedulerEnabledCB.Size = new Size(123, 19);
            SchedulerEnabledCB.TabIndex = 0;
            SchedulerEnabledCB.Tag = "";
            SchedulerEnabledCB.Text = "Scheduler Enabled";
            SchedulerEnabledCB.UseVisualStyleBackColor = true;
            SchedulerEnabledCB.CheckedChanged += SchedulerEnabledCB_CheckedChanged;
            // 
            // ExpansionNotificationSchedulerSettingsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox63);
            ForeColor = SystemColors.Control;
            Name = "ExpansionNotificationSchedulerSettingsControl";
            Size = new Size(179, 106);
            groupBox63.ResumeLayout(false);
            groupBox63.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox63;
        private CheckBox UseMissionTimeCB;
        private CheckBox UTCTimeCB;
        private CheckBox SchedulerEnabledCB;
    }
}
