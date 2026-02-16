namespace ExpansionPlugin
{
    partial class ExpansionVehicleSettingsCoversControl
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
            groupBox78 = new GroupBox();
            EnableAutoCoveringDEVehiclesCB = new CheckBox();
            CanCoverWithCargoCB = new CheckBox();
            darkLabel246 = new Label();
            VehicleAutoCoverTimeSecondsNUD = new NumericUpDown();
            VehicleAutoCoverRequireCamonetCB = new CheckBox();
            UseVirtualStorageForCoverCargoCB = new CheckBox();
            AllowCoveringDEVehiclesCB = new CheckBox();
            EnableVehicleCoversCB = new CheckBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            groupBox78.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)VehicleAutoCoverTimeSecondsNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox78
            // 
            groupBox78.Controls.Add(label6);
            groupBox78.Controls.Add(label5);
            groupBox78.Controls.Add(label4);
            groupBox78.Controls.Add(label3);
            groupBox78.Controls.Add(label2);
            groupBox78.Controls.Add(label1);
            groupBox78.Controls.Add(EnableAutoCoveringDEVehiclesCB);
            groupBox78.Controls.Add(CanCoverWithCargoCB);
            groupBox78.Controls.Add(darkLabel246);
            groupBox78.Controls.Add(VehicleAutoCoverTimeSecondsNUD);
            groupBox78.Controls.Add(VehicleAutoCoverRequireCamonetCB);
            groupBox78.Controls.Add(UseVirtualStorageForCoverCargoCB);
            groupBox78.Controls.Add(AllowCoveringDEVehiclesCB);
            groupBox78.Controls.Add(EnableVehicleCoversCB);
            groupBox78.ForeColor = SystemColors.Control;
            groupBox78.Location = new Point(0, 0);
            groupBox78.Margin = new Padding(4, 3, 4, 3);
            groupBox78.Name = "groupBox78";
            groupBox78.Padding = new Padding(4, 3, 4, 3);
            groupBox78.Size = new Size(393, 242);
            groupBox78.TabIndex = 11;
            groupBox78.TabStop = false;
            groupBox78.Text = "Covers";
            // 
            // EnableAutoCoveringDEVehiclesCB
            // 
            EnableAutoCoveringDEVehiclesCB.AutoSize = true;
            EnableAutoCoveringDEVehiclesCB.ForeColor = SystemColors.Control;
            EnableAutoCoveringDEVehiclesCB.Location = new Point(247, 204);
            EnableAutoCoveringDEVehiclesCB.Margin = new Padding(4, 3, 4, 3);
            EnableAutoCoveringDEVehiclesCB.Name = "EnableAutoCoveringDEVehiclesCB";
            EnableAutoCoveringDEVehiclesCB.Size = new Size(15, 14);
            EnableAutoCoveringDEVehiclesCB.TabIndex = 7;
            EnableAutoCoveringDEVehiclesCB.Tag = "";
            EnableAutoCoveringDEVehiclesCB.UseVisualStyleBackColor = true;
            EnableAutoCoveringDEVehiclesCB.CheckedChanged += EnableAutoCoveringDEVehiclesCB_CheckedChanged;
            // 
            // CanCoverWithCargoCB
            // 
            CanCoverWithCargoCB.AutoSize = true;
            CanCoverWithCargoCB.ForeColor = SystemColors.Control;
            CanCoverWithCargoCB.Location = new Point(247, 84);
            CanCoverWithCargoCB.Margin = new Padding(4, 3, 4, 3);
            CanCoverWithCargoCB.Name = "CanCoverWithCargoCB";
            CanCoverWithCargoCB.Size = new Size(15, 14);
            CanCoverWithCargoCB.TabIndex = 2;
            CanCoverWithCargoCB.Tag = "";
            CanCoverWithCargoCB.UseVisualStyleBackColor = true;
            CanCoverWithCargoCB.CheckedChanged += CanCoverWithCargoCB_CheckedChanged;
            // 
            // darkLabel246
            // 
            darkLabel246.AutoSize = true;
            darkLabel246.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel246.Location = new Point(14, 145);
            darkLabel246.Margin = new Padding(4, 0, 4, 0);
            darkLabel246.Name = "darkLabel246";
            darkLabel246.Size = new Size(183, 15);
            darkLabel246.TabIndex = 4;
            darkLabel246.Text = "Vehicle Auto Cover Time Seconds";
            // 
            // VehicleAutoCoverTimeSecondsNUD
            // 
            VehicleAutoCoverTimeSecondsNUD.BackColor = Color.FromArgb(60, 63, 65);
            VehicleAutoCoverTimeSecondsNUD.ForeColor = SystemColors.Control;
            VehicleAutoCoverTimeSecondsNUD.Location = new Point(247, 143);
            VehicleAutoCoverTimeSecondsNUD.Margin = new Padding(4, 3, 4, 3);
            VehicleAutoCoverTimeSecondsNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            VehicleAutoCoverTimeSecondsNUD.Name = "VehicleAutoCoverTimeSecondsNUD";
            VehicleAutoCoverTimeSecondsNUD.Size = new Size(128, 23);
            VehicleAutoCoverTimeSecondsNUD.TabIndex = 5;
            VehicleAutoCoverTimeSecondsNUD.Tag = "ChangeLockTimeSeconds";
            VehicleAutoCoverTimeSecondsNUD.TextAlign = HorizontalAlignment.Center;
            VehicleAutoCoverTimeSecondsNUD.ValueChanged += VehicleAutoCoverTimeSecondsNUD_ValueChanged;
            // 
            // VehicleAutoCoverRequireCamonetCB
            // 
            VehicleAutoCoverRequireCamonetCB.AutoSize = true;
            VehicleAutoCoverRequireCamonetCB.ForeColor = SystemColors.Control;
            VehicleAutoCoverRequireCamonetCB.Location = new Point(247, 174);
            VehicleAutoCoverRequireCamonetCB.Margin = new Padding(4, 3, 4, 3);
            VehicleAutoCoverRequireCamonetCB.Name = "VehicleAutoCoverRequireCamonetCB";
            VehicleAutoCoverRequireCamonetCB.Size = new Size(15, 14);
            VehicleAutoCoverRequireCamonetCB.TabIndex = 6;
            VehicleAutoCoverRequireCamonetCB.Tag = "";
            VehicleAutoCoverRequireCamonetCB.UseVisualStyleBackColor = true;
            VehicleAutoCoverRequireCamonetCB.CheckedChanged += VehicleAutoCoverRequireCamonetCB_CheckedChanged;
            // 
            // UseVirtualStorageForCoverCargoCB
            // 
            UseVirtualStorageForCoverCargoCB.AutoSize = true;
            UseVirtualStorageForCoverCargoCB.ForeColor = SystemColors.Control;
            UseVirtualStorageForCoverCargoCB.Location = new Point(247, 114);
            UseVirtualStorageForCoverCargoCB.Margin = new Padding(4, 3, 4, 3);
            UseVirtualStorageForCoverCargoCB.Name = "UseVirtualStorageForCoverCargoCB";
            UseVirtualStorageForCoverCargoCB.Size = new Size(15, 14);
            UseVirtualStorageForCoverCargoCB.TabIndex = 3;
            UseVirtualStorageForCoverCargoCB.Tag = "";
            UseVirtualStorageForCoverCargoCB.UseVisualStyleBackColor = true;
            UseVirtualStorageForCoverCargoCB.CheckedChanged += UseVirtualStorageForCoverCargoCB_CheckedChanged;
            // 
            // AllowCoveringDEVehiclesCB
            // 
            AllowCoveringDEVehiclesCB.AutoSize = true;
            AllowCoveringDEVehiclesCB.ForeColor = SystemColors.Control;
            AllowCoveringDEVehiclesCB.Location = new Point(247, 54);
            AllowCoveringDEVehiclesCB.Margin = new Padding(4, 3, 4, 3);
            AllowCoveringDEVehiclesCB.Name = "AllowCoveringDEVehiclesCB";
            AllowCoveringDEVehiclesCB.Size = new Size(15, 14);
            AllowCoveringDEVehiclesCB.TabIndex = 1;
            AllowCoveringDEVehiclesCB.Tag = "";
            AllowCoveringDEVehiclesCB.UseVisualStyleBackColor = true;
            AllowCoveringDEVehiclesCB.CheckedChanged += AllowCoveringDEVehiclesCB_CheckedChanged;
            // 
            // EnableVehicleCoversCB
            // 
            EnableVehicleCoversCB.AutoSize = true;
            EnableVehicleCoversCB.ForeColor = SystemColors.Control;
            EnableVehicleCoversCB.Location = new Point(247, 24);
            EnableVehicleCoversCB.Margin = new Padding(4, 3, 4, 3);
            EnableVehicleCoversCB.Name = "EnableVehicleCoversCB";
            EnableVehicleCoversCB.Size = new Size(15, 14);
            EnableVehicleCoversCB.TabIndex = 0;
            EnableVehicleCoversCB.Tag = "";
            EnableVehicleCoversCB.UseVisualStyleBackColor = true;
            EnableVehicleCoversCB.CheckedChanged += EnableVehicleCoversCB_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(220, 220, 220);
            label1.Location = new Point(14, 25);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(121, 15);
            label1.TabIndex = 8;
            label1.Text = "Enable Vehicle Covers";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.FromArgb(220, 220, 220);
            label2.Location = new Point(14, 115);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(195, 15);
            label2.TabIndex = 9;
            label2.Text = "Use Virtual Storage For Cover Cargo";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.FromArgb(220, 220, 220);
            label3.Location = new Point(14, 85);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(125, 15);
            label3.TabIndex = 10;
            label3.Text = "Can Cover With Cargo";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.FromArgb(220, 220, 220);
            label4.Location = new Point(14, 55);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(150, 15);
            label4.TabIndex = 11;
            label4.Text = "Allow Covering DE Vehicles";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.FromArgb(220, 220, 220);
            label5.Location = new Point(14, 175);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(202, 15);
            label5.TabIndex = 12;
            label5.Text = "Vehicle Auto Cover Require Camonet";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.FromArgb(220, 220, 220);
            label6.Location = new Point(14, 205);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(184, 15);
            label6.TabIndex = 13;
            label6.Text = "Enable Auto Covering DE Vehicles";
            // 
            // ExpansionVehicleSettingsCoversControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox78);
            ForeColor = SystemColors.Control;
            Name = "ExpansionVehicleSettingsCoversControl";
            Size = new Size(393, 242);
            groupBox78.ResumeLayout(false);
            groupBox78.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)VehicleAutoCoverTimeSecondsNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox78;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private CheckBox EnableAutoCoveringDEVehiclesCB;
        private CheckBox CanCoverWithCargoCB;
        private Label darkLabel246;
        private NumericUpDown VehicleAutoCoverTimeSecondsNUD;
        private CheckBox VehicleAutoCoverRequireCamonetCB;
        private CheckBox UseVirtualStorageForCoverCargoCB;
        private CheckBox AllowCoveringDEVehiclesCB;
        private CheckBox EnableVehicleCoversCB;
        private Label label6;
        private Label label5;
    }
}
