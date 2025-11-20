namespace ExpansionPlugin
{
    partial class ExpansionGeneralGeneralControl
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
            groupBox32 = new GroupBox();
            EnableEarPlugsCB = new CheckBox();
            DisableShootToUnlockCB = new CheckBox();
            DisableMagicCrosshairCB = new CheckBox();
            EnableAutoRunCB = new CheckBox();
            EnableHUDNightvisionOverlayCB = new CheckBox();
            groupBox32.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox32
            // 
            groupBox32.Controls.Add(EnableEarPlugsCB);
            groupBox32.Controls.Add(DisableShootToUnlockCB);
            groupBox32.Controls.Add(DisableMagicCrosshairCB);
            groupBox32.Controls.Add(EnableAutoRunCB);
            groupBox32.Controls.Add(EnableHUDNightvisionOverlayCB);
            groupBox32.ForeColor = SystemColors.Control;
            groupBox32.Location = new Point(0, 0);
            groupBox32.Margin = new Padding(4, 3, 4, 3);
            groupBox32.Name = "groupBox32";
            groupBox32.Padding = new Padding(4, 3, 4, 3);
            groupBox32.Size = new Size(256, 155);
            groupBox32.TabIndex = 13;
            groupBox32.TabStop = false;
            groupBox32.Text = "General";
            // 
            // EnableEarPlugsCB
            // 
            EnableEarPlugsCB.AutoSize = true;
            EnableEarPlugsCB.Location = new Point(23, 122);
            EnableEarPlugsCB.Margin = new Padding(4, 3, 4, 3);
            EnableEarPlugsCB.Name = "EnableEarPlugsCB";
            EnableEarPlugsCB.Size = new Size(112, 19);
            EnableEarPlugsCB.TabIndex = 4;
            EnableEarPlugsCB.Text = "Enable Ear Plugs";
            EnableEarPlugsCB.UseVisualStyleBackColor = true;
            EnableEarPlugsCB.CheckedChanged += EnableEarPlugsCB_CheckedChanged;
            // 
            // DisableShootToUnlockCB
            // 
            DisableShootToUnlockCB.AutoSize = true;
            DisableShootToUnlockCB.Location = new Point(24, 22);
            DisableShootToUnlockCB.Margin = new Padding(4, 3, 4, 3);
            DisableShootToUnlockCB.Name = "DisableShootToUnlockCB";
            DisableShootToUnlockCB.Size = new Size(153, 19);
            DisableShootToUnlockCB.TabIndex = 0;
            DisableShootToUnlockCB.Text = "Disable Shoot To Unlock";
            DisableShootToUnlockCB.UseVisualStyleBackColor = true;
            DisableShootToUnlockCB.CheckedChanged += DisableShootToUnlockCB_CheckedChanged;
            // 
            // DisableMagicCrosshairCB
            // 
            DisableMagicCrosshairCB.AutoSize = true;
            DisableMagicCrosshairCB.Location = new Point(24, 72);
            DisableMagicCrosshairCB.Margin = new Padding(4, 3, 4, 3);
            DisableMagicCrosshairCB.Name = "DisableMagicCrosshairCB";
            DisableMagicCrosshairCB.Size = new Size(152, 19);
            DisableMagicCrosshairCB.TabIndex = 2;
            DisableMagicCrosshairCB.Text = "Disable Magic Crosshair";
            DisableMagicCrosshairCB.UseVisualStyleBackColor = true;
            DisableMagicCrosshairCB.CheckedChanged += DisableMagicCrosshairCB_CheckedChanged;
            // 
            // EnableAutoRunCB
            // 
            EnableAutoRunCB.AutoSize = true;
            EnableAutoRunCB.Location = new Point(23, 97);
            EnableAutoRunCB.Margin = new Padding(4, 3, 4, 3);
            EnableAutoRunCB.Name = "EnableAutoRunCB";
            EnableAutoRunCB.Size = new Size(114, 19);
            EnableAutoRunCB.TabIndex = 3;
            EnableAutoRunCB.Text = "Enable Auto Run";
            EnableAutoRunCB.UseVisualStyleBackColor = true;
            EnableAutoRunCB.CheckedChanged += EnableAutoRunCB_CheckedChanged;
            // 
            // EnableHUDNightvisionOverlayCB
            // 
            EnableHUDNightvisionOverlayCB.AutoSize = true;
            EnableHUDNightvisionOverlayCB.Location = new Point(24, 47);
            EnableHUDNightvisionOverlayCB.Margin = new Padding(4, 3, 4, 3);
            EnableHUDNightvisionOverlayCB.Name = "EnableHUDNightvisionOverlayCB";
            EnableHUDNightvisionOverlayCB.Size = new Size(199, 19);
            EnableHUDNightvisionOverlayCB.TabIndex = 1;
            EnableHUDNightvisionOverlayCB.Text = "Enable HUD Night vision Overlay";
            EnableHUDNightvisionOverlayCB.UseVisualStyleBackColor = true;
            EnableHUDNightvisionOverlayCB.CheckedChanged += EnableHUDNightvisionOverlayCB_CheckedChanged;
            // 
            // ExpansionGeneralGeneralControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox32);
            ForeColor = SystemColors.Control;
            Name = "ExpansionGeneralGeneralControl";
            Size = new Size(256, 155);
            groupBox32.ResumeLayout(false);
            groupBox32.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox32;
        private CheckBox EnableEarPlugsCB;
        private CheckBox DisableShootToUnlockCB;
        private CheckBox DisableMagicCrosshairCB;
        private CheckBox EnableAutoRunCB;
        private CheckBox EnableHUDNightvisionOverlayCB;
    }
}
