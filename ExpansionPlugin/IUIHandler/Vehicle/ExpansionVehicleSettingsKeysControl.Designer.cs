namespace ExpansionPlugin
{
    partial class ExpansionVehicleSettingsKeysControl
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
            groupBox42 = new GroupBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            VehicleRequireKeyToStartComboBox = new ComboBox();
            darkLabel131 = new Label();
            VehicleRequireAllDoorsCB = new CheckBox();
            VehicleLockedAllowInventoryAccessCB = new CheckBox();
            VehicleLockedAllowInventoryAccessWithoutDoorsCB = new CheckBox();
            MasterKeyPairingModeComboBox = new ComboBox();
            darkLabel116 = new Label();
            MasterKeyUsesNUD = new NumericUpDown();
            darkLabel117 = new Label();
            groupBox42.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MasterKeyUsesNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox42
            // 
            groupBox42.Controls.Add(label3);
            groupBox42.Controls.Add(label2);
            groupBox42.Controls.Add(label1);
            groupBox42.Controls.Add(VehicleRequireKeyToStartComboBox);
            groupBox42.Controls.Add(darkLabel131);
            groupBox42.Controls.Add(VehicleRequireAllDoorsCB);
            groupBox42.Controls.Add(VehicleLockedAllowInventoryAccessCB);
            groupBox42.Controls.Add(VehicleLockedAllowInventoryAccessWithoutDoorsCB);
            groupBox42.Controls.Add(MasterKeyPairingModeComboBox);
            groupBox42.Controls.Add(darkLabel116);
            groupBox42.Controls.Add(MasterKeyUsesNUD);
            groupBox42.Controls.Add(darkLabel117);
            groupBox42.ForeColor = SystemColors.Control;
            groupBox42.Location = new Point(0, 0);
            groupBox42.Margin = new Padding(4, 3, 4, 3);
            groupBox42.Name = "groupBox42";
            groupBox42.Padding = new Padding(4, 3, 4, 3);
            groupBox42.Size = new Size(677, 214);
            groupBox42.TabIndex = 10;
            groupBox42.TabStop = false;
            groupBox42.Text = "Keys";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.FromArgb(220, 220, 220);
            label3.Location = new Point(14, 115);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(290, 15);
            label3.TabIndex = 11;
            label3.Text = "Vehicle Locked Allow Inventory Access Without Doors";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.FromArgb(220, 220, 220);
            label2.Location = new Point(14, 85);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(210, 15);
            label2.TabIndex = 10;
            label2.Text = "Vehicle Locked Allow Inventory Access";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(220, 220, 220);
            label1.Location = new Point(14, 55);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(138, 15);
            label1.TabIndex = 9;
            label1.Text = "Vehicle Require All Doors";
            // 
            // VehicleRequireKeyToStartComboBox
            // 
            VehicleRequireKeyToStartComboBox.FormattingEnabled = true;
            VehicleRequireKeyToStartComboBox.Location = new Point(328, 22);
            VehicleRequireKeyToStartComboBox.Margin = new Padding(4, 3, 4, 3);
            VehicleRequireKeyToStartComboBox.Name = "VehicleRequireKeyToStartComboBox";
            VehicleRequireKeyToStartComboBox.Size = new Size(328, 23);
            VehicleRequireKeyToStartComboBox.TabIndex = 1;
            VehicleRequireKeyToStartComboBox.SelectedIndexChanged += VehicleRequireKeyToStartComboBox_SelectedIndexChanged;
            // 
            // darkLabel131
            // 
            darkLabel131.AutoSize = true;
            darkLabel131.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel131.Location = new Point(14, 25);
            darkLabel131.Margin = new Padding(4, 0, 4, 0);
            darkLabel131.Name = "darkLabel131";
            darkLabel131.Size = new Size(151, 15);
            darkLabel131.TabIndex = 0;
            darkLabel131.Text = "Vehicle Require Key To Start";
            // 
            // VehicleRequireAllDoorsCB
            // 
            VehicleRequireAllDoorsCB.AutoSize = true;
            VehicleRequireAllDoorsCB.ForeColor = SystemColors.Control;
            VehicleRequireAllDoorsCB.Location = new Point(328, 54);
            VehicleRequireAllDoorsCB.Margin = new Padding(4, 3, 4, 3);
            VehicleRequireAllDoorsCB.Name = "VehicleRequireAllDoorsCB";
            VehicleRequireAllDoorsCB.Size = new Size(15, 14);
            VehicleRequireAllDoorsCB.TabIndex = 2;
            VehicleRequireAllDoorsCB.Tag = "VehicleRequireAllDoors";
            VehicleRequireAllDoorsCB.UseVisualStyleBackColor = true;
            VehicleRequireAllDoorsCB.CheckedChanged += VehicleRequireAllDoorsCB_CheckedChanged;
            // 
            // VehicleLockedAllowInventoryAccessCB
            // 
            VehicleLockedAllowInventoryAccessCB.AutoSize = true;
            VehicleLockedAllowInventoryAccessCB.ForeColor = SystemColors.Control;
            VehicleLockedAllowInventoryAccessCB.Location = new Point(328, 84);
            VehicleLockedAllowInventoryAccessCB.Margin = new Padding(4, 3, 4, 3);
            VehicleLockedAllowInventoryAccessCB.Name = "VehicleLockedAllowInventoryAccessCB";
            VehicleLockedAllowInventoryAccessCB.Size = new Size(15, 14);
            VehicleLockedAllowInventoryAccessCB.TabIndex = 3;
            VehicleLockedAllowInventoryAccessCB.Tag = "VehicleLockedAllowInventoryAccess";
            VehicleLockedAllowInventoryAccessCB.UseVisualStyleBackColor = true;
            VehicleLockedAllowInventoryAccessCB.CheckedChanged += VehicleLockedAllowInventoryAccessCB_CheckedChanged;
            // 
            // VehicleLockedAllowInventoryAccessWithoutDoorsCB
            // 
            VehicleLockedAllowInventoryAccessWithoutDoorsCB.AutoSize = true;
            VehicleLockedAllowInventoryAccessWithoutDoorsCB.ForeColor = SystemColors.Control;
            VehicleLockedAllowInventoryAccessWithoutDoorsCB.Location = new Point(328, 114);
            VehicleLockedAllowInventoryAccessWithoutDoorsCB.Margin = new Padding(4, 3, 4, 3);
            VehicleLockedAllowInventoryAccessWithoutDoorsCB.Name = "VehicleLockedAllowInventoryAccessWithoutDoorsCB";
            VehicleLockedAllowInventoryAccessWithoutDoorsCB.Size = new Size(15, 14);
            VehicleLockedAllowInventoryAccessWithoutDoorsCB.TabIndex = 4;
            VehicleLockedAllowInventoryAccessWithoutDoorsCB.Tag = "VehicleLockedAllowInventoryAccessWithoutDoors";
            VehicleLockedAllowInventoryAccessWithoutDoorsCB.UseVisualStyleBackColor = true;
            VehicleLockedAllowInventoryAccessWithoutDoorsCB.CheckedChanged += VehicleLockedAllowInventoryAccessWithoutDoorsCB_CheckedChanged;
            // 
            // MasterKeyPairingModeComboBox
            // 
            MasterKeyPairingModeComboBox.FormattingEnabled = true;
            MasterKeyPairingModeComboBox.Location = new Point(330, 142);
            MasterKeyPairingModeComboBox.Margin = new Padding(4, 3, 4, 3);
            MasterKeyPairingModeComboBox.Name = "MasterKeyPairingModeComboBox";
            MasterKeyPairingModeComboBox.Size = new Size(326, 23);
            MasterKeyPairingModeComboBox.TabIndex = 6;
            MasterKeyPairingModeComboBox.SelectedIndexChanged += MasterKeyPairingModeComboBox_SelectedIndexChanged;
            // 
            // darkLabel116
            // 
            darkLabel116.AutoSize = true;
            darkLabel116.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel116.Location = new Point(14, 145);
            darkLabel116.Margin = new Padding(4, 0, 4, 0);
            darkLabel116.Name = "darkLabel116";
            darkLabel116.Size = new Size(139, 15);
            darkLabel116.TabIndex = 5;
            darkLabel116.Text = "Master Key Pairing Mode";
            // 
            // MasterKeyUsesNUD
            // 
            MasterKeyUsesNUD.BackColor = Color.FromArgb(60, 63, 65);
            MasterKeyUsesNUD.ForeColor = SystemColors.Control;
            MasterKeyUsesNUD.Location = new Point(328, 173);
            MasterKeyUsesNUD.Margin = new Padding(4, 3, 4, 3);
            MasterKeyUsesNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            MasterKeyUsesNUD.Name = "MasterKeyUsesNUD";
            MasterKeyUsesNUD.Size = new Size(158, 23);
            MasterKeyUsesNUD.TabIndex = 8;
            MasterKeyUsesNUD.Tag = "MissionMaxTime";
            MasterKeyUsesNUD.TextAlign = HorizontalAlignment.Center;
            MasterKeyUsesNUD.ValueChanged += MasterKeyUsesNUD_ValueChanged;
            // 
            // darkLabel117
            // 
            darkLabel117.AutoSize = true;
            darkLabel117.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel117.Location = new Point(14, 175);
            darkLabel117.Margin = new Padding(4, 0, 4, 0);
            darkLabel117.Name = "darkLabel117";
            darkLabel117.Size = new Size(92, 15);
            darkLabel117.TabIndex = 7;
            darkLabel117.Text = "Master Key Uses";
            // 
            // ExpansionVehicleSettingsKeysControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox42);
            ForeColor = SystemColors.Control;
            Name = "ExpansionVehicleSettingsKeysControl";
            Size = new Size(677, 214);
            groupBox42.ResumeLayout(false);
            groupBox42.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)MasterKeyUsesNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox42;
        private Label label3;
        private Label label2;
        private Label label1;
        private ComboBox VehicleRequireKeyToStartComboBox;
        private Label darkLabel131;
        private CheckBox VehicleRequireAllDoorsCB;
        private CheckBox VehicleLockedAllowInventoryAccessCB;
        private CheckBox VehicleLockedAllowInventoryAccessWithoutDoorsCB;
        private ComboBox MasterKeyPairingModeComboBox;
        private Label darkLabel116;
        private NumericUpDown MasterKeyUsesNUD;
        private Label darkLabel117;
    }
}
