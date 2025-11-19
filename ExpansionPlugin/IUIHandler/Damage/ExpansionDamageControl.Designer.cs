namespace ExpansionPlugin
{
    partial class ExpansionDamageControl
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
            groupBox68 = new GroupBox();
            CheckForBlockingObjectsCB = new CheckBox();
            DSEnabledCB = new CheckBox();
            groupBox68.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox68
            // 
            groupBox68.Controls.Add(CheckForBlockingObjectsCB);
            groupBox68.Controls.Add(DSEnabledCB);
            groupBox68.ForeColor = SystemColors.Control;
            groupBox68.Location = new Point(0, 0);
            groupBox68.Margin = new Padding(4, 3, 4, 3);
            groupBox68.Name = "groupBox68";
            groupBox68.Padding = new Padding(4, 3, 4, 3);
            groupBox68.Size = new Size(308, 80);
            groupBox68.TabIndex = 11;
            groupBox68.TabStop = false;
            groupBox68.Text = "Damage System Settings";
            // 
            // CheckForBlockingObjectsCB
            // 
            CheckForBlockingObjectsCB.AutoSize = true;
            CheckForBlockingObjectsCB.ForeColor = SystemColors.Control;
            CheckForBlockingObjectsCB.Location = new Point(115, 35);
            CheckForBlockingObjectsCB.Margin = new Padding(4, 3, 4, 3);
            CheckForBlockingObjectsCB.Name = "CheckForBlockingObjectsCB";
            CheckForBlockingObjectsCB.Size = new Size(171, 19);
            CheckForBlockingObjectsCB.TabIndex = 21;
            CheckForBlockingObjectsCB.Text = "Check For Blocking Objects";
            CheckForBlockingObjectsCB.TextAlign = ContentAlignment.MiddleRight;
            CheckForBlockingObjectsCB.UseVisualStyleBackColor = true;
            CheckForBlockingObjectsCB.CheckedChanged += CheckForBlockingObjectsCB_CheckedChanged;
            // 
            // DSEnabledCB
            // 
            DSEnabledCB.AutoSize = true;
            DSEnabledCB.ForeColor = SystemColors.Control;
            DSEnabledCB.Location = new Point(25, 35);
            DSEnabledCB.Margin = new Padding(4, 3, 4, 3);
            DSEnabledCB.Name = "DSEnabledCB";
            DSEnabledCB.Size = new Size(68, 19);
            DSEnabledCB.TabIndex = 0;
            DSEnabledCB.Text = "Enabled";
            DSEnabledCB.TextAlign = ContentAlignment.MiddleRight;
            DSEnabledCB.UseVisualStyleBackColor = true;
            DSEnabledCB.CheckedChanged += DSEnabledCB_CheckedChanged;
            // 
            // ExpansionDamageControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox68);
            ForeColor = SystemColors.Control;
            Name = "ExpansionDamageControl";
            Size = new Size(308, 80);
            groupBox68.ResumeLayout(false);
            groupBox68.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox68;
        private CheckBox CheckForBlockingObjectsCB;
        private CheckBox DSEnabledCB;
    }
}
