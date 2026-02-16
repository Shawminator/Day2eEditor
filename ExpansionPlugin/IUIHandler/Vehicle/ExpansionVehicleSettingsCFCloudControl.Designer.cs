namespace ExpansionPlugin
{
    partial class ExpansionVehicleSettingsCFCloudControl
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
            groupBox80 = new GroupBox();
            darkLabel250 = new Label();
            VehicleCarCoverIconNameTB = new TextBox();
            darkLabel249 = new Label();
            VehicleBoatCoverIconNameTB = new TextBox();
            darkLabel248 = new Label();
            VehicleHeliCoverIconNameTB = new TextBox();
            groupBox80.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox80
            // 
            groupBox80.Controls.Add(darkLabel250);
            groupBox80.Controls.Add(VehicleCarCoverIconNameTB);
            groupBox80.Controls.Add(darkLabel249);
            groupBox80.Controls.Add(VehicleBoatCoverIconNameTB);
            groupBox80.Controls.Add(darkLabel248);
            groupBox80.Controls.Add(VehicleHeliCoverIconNameTB);
            groupBox80.ForeColor = SystemColors.Control;
            groupBox80.Location = new Point(0, 0);
            groupBox80.Margin = new Padding(4, 3, 4, 3);
            groupBox80.Name = "groupBox80";
            groupBox80.Padding = new Padding(4, 3, 4, 3);
            groupBox80.Size = new Size(687, 119);
            groupBox80.TabIndex = 12;
            groupBox80.TabStop = false;
            groupBox80.Text = "CF Cloud";
            // 
            // darkLabel250
            // 
            darkLabel250.AutoSize = true;
            darkLabel250.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel250.Location = new Point(14, 85);
            darkLabel250.Margin = new Padding(4, 0, 4, 0);
            darkLabel250.Name = "darkLabel250";
            darkLabel250.Size = new Size(120, 15);
            darkLabel250.TabIndex = 4;
            darkLabel250.Text = "Car Cover Icon Name";
            // 
            // VehicleCarCoverIconNameTB
            // 
            VehicleCarCoverIconNameTB.BackColor = Color.FromArgb(60, 63, 65);
            VehicleCarCoverIconNameTB.ForeColor = SystemColors.Control;
            VehicleCarCoverIconNameTB.Location = new Point(175, 82);
            VehicleCarCoverIconNameTB.Margin = new Padding(4, 3, 4, 3);
            VehicleCarCoverIconNameTB.Name = "VehicleCarCoverIconNameTB";
            VehicleCarCoverIconNameTB.Size = new Size(493, 23);
            VehicleCarCoverIconNameTB.TabIndex = 5;
            VehicleCarCoverIconNameTB.Tag = "Container";
            VehicleCarCoverIconNameTB.TextChanged += VehicleCarCoverIconNameTB_TextChanged;
            // 
            // darkLabel249
            // 
            darkLabel249.AutoSize = true;
            darkLabel249.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel249.Location = new Point(14, 55);
            darkLabel249.Margin = new Padding(4, 0, 4, 0);
            darkLabel249.Name = "darkLabel249";
            darkLabel249.Size = new Size(126, 15);
            darkLabel249.TabIndex = 2;
            darkLabel249.Text = "Boat Cover Icon Name";
            // 
            // VehicleBoatCoverIconNameTB
            // 
            VehicleBoatCoverIconNameTB.BackColor = Color.FromArgb(60, 63, 65);
            VehicleBoatCoverIconNameTB.ForeColor = SystemColors.Control;
            VehicleBoatCoverIconNameTB.Location = new Point(175, 52);
            VehicleBoatCoverIconNameTB.Margin = new Padding(4, 3, 4, 3);
            VehicleBoatCoverIconNameTB.Name = "VehicleBoatCoverIconNameTB";
            VehicleBoatCoverIconNameTB.Size = new Size(493, 23);
            VehicleBoatCoverIconNameTB.TabIndex = 3;
            VehicleBoatCoverIconNameTB.Tag = "Container";
            VehicleBoatCoverIconNameTB.TextChanged += VehicleBoatCoverIconNameTB_TextChanged;
            // 
            // darkLabel248
            // 
            darkLabel248.AutoSize = true;
            darkLabel248.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel248.Location = new Point(14, 25);
            darkLabel248.Margin = new Padding(4, 0, 4, 0);
            darkLabel248.Name = "darkLabel248";
            darkLabel248.Size = new Size(123, 15);
            darkLabel248.TabIndex = 0;
            darkLabel248.Text = "Heli Cover Icon Name";
            // 
            // VehicleHeliCoverIconNameTB
            // 
            VehicleHeliCoverIconNameTB.BackColor = Color.FromArgb(60, 63, 65);
            VehicleHeliCoverIconNameTB.ForeColor = SystemColors.Control;
            VehicleHeliCoverIconNameTB.Location = new Point(175, 22);
            VehicleHeliCoverIconNameTB.Margin = new Padding(4, 3, 4, 3);
            VehicleHeliCoverIconNameTB.Name = "VehicleHeliCoverIconNameTB";
            VehicleHeliCoverIconNameTB.Size = new Size(493, 23);
            VehicleHeliCoverIconNameTB.TabIndex = 1;
            VehicleHeliCoverIconNameTB.Tag = "Container";
            VehicleHeliCoverIconNameTB.TextChanged += VehicleHeliCoverIconNameTB_TextChanged;
            // 
            // ExpansionVehicleSettingsCFCloudControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox80);
            ForeColor = SystemColors.Control;
            Name = "ExpansionVehicleSettingsCFCloudControl";
            Size = new Size(687, 119);
            groupBox80.ResumeLayout(false);
            groupBox80.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox80;
        private Label darkLabel250;
        private TextBox VehicleCarCoverIconNameTB;
        private Label darkLabel249;
        private TextBox VehicleBoatCoverIconNameTB;
        private Label darkLabel248;
        private TextBox VehicleHeliCoverIconNameTB;
    }
}
