namespace EconomyPlugin
{
    partial class cfggameplayGeneralDataControl
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
            disableRespawnInUnconsciousnessCB = new CheckBox();
            disableRespawnDialogCB = new CheckBox();
            disableContainerDamageCB = new CheckBox();
            disableBaseDamageCB = new CheckBox();
            groupBox23.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox23
            // 
            groupBox23.Controls.Add(disableRespawnInUnconsciousnessCB);
            groupBox23.Controls.Add(disableRespawnDialogCB);
            groupBox23.Controls.Add(disableContainerDamageCB);
            groupBox23.Controls.Add(disableBaseDamageCB);
            groupBox23.ForeColor = SystemColors.Control;
            groupBox23.Location = new Point(0, 0);
            groupBox23.Margin = new Padding(4, 3, 4, 3);
            groupBox23.Name = "groupBox23";
            groupBox23.Padding = new Padding(4, 3, 4, 3);
            groupBox23.Size = new Size(282, 125);
            groupBox23.TabIndex = 82;
            groupBox23.TabStop = false;
            groupBox23.Text = "General Data";
            // 
            // disableRespawnInUnconsciousnessCB
            // 
            disableRespawnInUnconsciousnessCB.AutoSize = true;
            disableRespawnInUnconsciousnessCB.Location = new Point(14, 97);
            disableRespawnInUnconsciousnessCB.Margin = new Padding(4, 3, 4, 3);
            disableRespawnInUnconsciousnessCB.Name = "disableRespawnInUnconsciousnessCB";
            disableRespawnInUnconsciousnessCB.Size = new Size(217, 19);
            disableRespawnInUnconsciousnessCB.TabIndex = 3;
            disableRespawnInUnconsciousnessCB.Text = "disable RespawnIn Unconsciousness";
            disableRespawnInUnconsciousnessCB.UseVisualStyleBackColor = true;
            disableRespawnInUnconsciousnessCB.CheckedChanged += disableRespawnInUnconsciousnessCB_CheckedChanged;
            // 
            // disableRespawnDialogCB
            // 
            disableRespawnDialogCB.AutoSize = true;
            disableRespawnDialogCB.Location = new Point(14, 70);
            disableRespawnDialogCB.Margin = new Padding(4, 3, 4, 3);
            disableRespawnDialogCB.Name = "disableRespawnDialogCB";
            disableRespawnDialogCB.Size = new Size(151, 19);
            disableRespawnDialogCB.TabIndex = 2;
            disableRespawnDialogCB.Text = "Disable Respawn Dialog";
            disableRespawnDialogCB.UseVisualStyleBackColor = true;
            disableRespawnDialogCB.CheckedChanged += disableRespawnDialogCB_CheckedChanged;
            // 
            // disableContainerDamageCB
            // 
            disableContainerDamageCB.AutoSize = true;
            disableContainerDamageCB.Location = new Point(14, 44);
            disableContainerDamageCB.Margin = new Padding(4, 3, 4, 3);
            disableContainerDamageCB.Name = "disableContainerDamageCB";
            disableContainerDamageCB.Size = new Size(166, 19);
            disableContainerDamageCB.TabIndex = 1;
            disableContainerDamageCB.Text = "Disable Container Damage";
            disableContainerDamageCB.UseVisualStyleBackColor = true;
            disableContainerDamageCB.CheckedChanged += disableContainerDamageCB_CheckedChanged;
            // 
            // disableBaseDamageCB
            // 
            disableBaseDamageCB.AutoSize = true;
            disableBaseDamageCB.Location = new Point(14, 17);
            disableBaseDamageCB.Margin = new Padding(4, 3, 4, 3);
            disableBaseDamageCB.Name = "disableBaseDamageCB";
            disableBaseDamageCB.Size = new Size(138, 19);
            disableBaseDamageCB.TabIndex = 0;
            disableBaseDamageCB.Text = "Disable Base Damage";
            disableBaseDamageCB.UseVisualStyleBackColor = true;
            disableBaseDamageCB.CheckedChanged += disableBaseDamageCB_CheckedChanged;
            // 
            // cfggameplayGeneralDataControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox23);
            ForeColor = SystemColors.Control;
            Name = "cfggameplayGeneralDataControl";
            Size = new Size(295, 137);
            groupBox23.ResumeLayout(false);
            groupBox23.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox23;
        private CheckBox disableRespawnInUnconsciousnessCB;
        private CheckBox disableRespawnDialogCB;
        private CheckBox disableContainerDamageCB;
        private CheckBox disableBaseDamageCB;
    }
}
