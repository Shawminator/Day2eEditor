namespace EconomyPlugin
{
    partial class ExpansionMonitoringSettingsControl
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
            groupBox62 = new GroupBox();
            MonitoringSettingsEnabledCB = new CheckBox();
            groupBox62.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox62
            // 
            groupBox62.Controls.Add(MonitoringSettingsEnabledCB);
            groupBox62.ForeColor = SystemColors.Control;
            groupBox62.Location = new Point(0, 0);
            groupBox62.Margin = new Padding(4, 3, 4, 3);
            groupBox62.Name = "groupBox62";
            groupBox62.Padding = new Padding(4, 3, 4, 3);
            groupBox62.Size = new Size(144, 53);
            groupBox62.TabIndex = 9;
            groupBox62.TabStop = false;
            groupBox62.Text = "Monitoring Settings";
            // 
            // MonitoringSettingsEnabledCB
            // 
            MonitoringSettingsEnabledCB.AutoSize = true;
            MonitoringSettingsEnabledCB.ForeColor = SystemColors.Control;
            MonitoringSettingsEnabledCB.Location = new Point(26, 22);
            MonitoringSettingsEnabledCB.Margin = new Padding(4, 3, 4, 3);
            MonitoringSettingsEnabledCB.Name = "MonitoringSettingsEnabledCB";
            MonitoringSettingsEnabledCB.Size = new Size(68, 19);
            MonitoringSettingsEnabledCB.TabIndex = 0;
            MonitoringSettingsEnabledCB.Text = "Enabled";
            MonitoringSettingsEnabledCB.TextAlign = ContentAlignment.MiddleRight;
            MonitoringSettingsEnabledCB.UseVisualStyleBackColor = true;
            MonitoringSettingsEnabledCB.CheckedChanged += MonitoringSettingsEnabledCB_CheckedChanged;
            // 
            // ExpansionMonitoringSettingsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox62);
            ForeColor = SystemColors.Control;
            Name = "ExpansionMonitoringSettingsControl";
            Size = new Size(144, 53);
            groupBox62.ResumeLayout(false);
            groupBox62.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox62;
        private CheckBox MonitoringSettingsEnabledCB;
    }
}
