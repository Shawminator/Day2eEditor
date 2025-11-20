namespace ExpansionPlugin
{
    partial class ExpansionGeneralMappingControl
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
            groupBox37 = new GroupBox();
            darkLabel233 = new Label();
            BuildingIvysComboBox = new ComboBox();
            darkLabel94 = new Label();
            BuildingInteriorsCB = new CheckBox();
            UseCustomMappingModuleCB = new CheckBox();
            groupBox37.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox37
            // 
            groupBox37.Controls.Add(darkLabel233);
            groupBox37.Controls.Add(BuildingIvysComboBox);
            groupBox37.Controls.Add(darkLabel94);
            groupBox37.Controls.Add(BuildingInteriorsCB);
            groupBox37.Controls.Add(UseCustomMappingModuleCB);
            groupBox37.ForeColor = SystemColors.Control;
            groupBox37.Location = new Point(0, 0);
            groupBox37.Margin = new Padding(4, 3, 4, 3);
            groupBox37.Name = "groupBox37";
            groupBox37.Padding = new Padding(4, 3, 4, 3);
            groupBox37.Size = new Size(306, 187);
            groupBox37.TabIndex = 19;
            groupBox37.TabStop = false;
            groupBox37.Text = "Mapping";
            // 
            // darkLabel233
            // 
            darkLabel233.AutoSize = true;
            darkLabel233.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel233.Location = new Point(9, 123);
            darkLabel233.Margin = new Padding(4, 0, 4, 0);
            darkLabel233.Name = "darkLabel233";
            darkLabel233.Size = new Size(69, 15);
            darkLabel233.TabIndex = 3;
            darkLabel233.Text = "Building Ivy";
            // 
            // BuildingIvysComboBox
            // 
            BuildingIvysComboBox.BackColor = Color.FromArgb(60, 63, 65);
            BuildingIvysComboBox.ForeColor = SystemColors.Control;
            BuildingIvysComboBox.FormattingEnabled = true;
            BuildingIvysComboBox.Location = new Point(12, 145);
            BuildingIvysComboBox.Margin = new Padding(4, 3, 4, 3);
            BuildingIvysComboBox.Name = "BuildingIvysComboBox";
            BuildingIvysComboBox.Size = new Size(284, 23);
            BuildingIvysComboBox.TabIndex = 4;
            BuildingIvysComboBox.SelectedIndexChanged += BuildingIvysComboBox_SelectedIndexChanged;
            // 
            // darkLabel94
            // 
            darkLabel94.AutoSize = true;
            darkLabel94.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel94.Location = new Point(8, 18);
            darkLabel94.Margin = new Padding(4, 0, 4, 0);
            darkLabel94.Name = "darkLabel94";
            darkLabel94.Size = new Size(288, 45);
            darkLabel94.TabIndex = 0;
            darkLabel94.Text = "NOTE:- Only for Chernarus Map\r\nDo not enable any if using any other map.\r\nEdit list of item manually if you wish to change them.";
            // 
            // BuildingInteriorsCB
            // 
            BuildingInteriorsCB.AutoSize = true;
            BuildingInteriorsCB.Location = new Point(13, 100);
            BuildingInteriorsCB.Margin = new Padding(4, 3, 4, 3);
            BuildingInteriorsCB.Name = "BuildingInteriorsCB";
            BuildingInteriorsCB.Size = new Size(116, 19);
            BuildingInteriorsCB.TabIndex = 2;
            BuildingInteriorsCB.Text = "Building Interiors";
            BuildingInteriorsCB.UseVisualStyleBackColor = true;
            BuildingInteriorsCB.CheckedChanged += BuildingInteriorsCB_CheckedChanged;
            // 
            // UseCustomMappingModuleCB
            // 
            UseCustomMappingModuleCB.AutoSize = true;
            UseCustomMappingModuleCB.Location = new Point(13, 74);
            UseCustomMappingModuleCB.Margin = new Padding(4, 3, 4, 3);
            UseCustomMappingModuleCB.Name = "UseCustomMappingModuleCB";
            UseCustomMappingModuleCB.Size = new Size(185, 19);
            UseCustomMappingModuleCB.TabIndex = 1;
            UseCustomMappingModuleCB.Text = "Use Custom Mapping Module";
            UseCustomMappingModuleCB.UseVisualStyleBackColor = true;
            UseCustomMappingModuleCB.CheckedChanged += UseCustomMappingModuleCB_CheckedChanged;
            // 
            // ExpansionGeneralMappingControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox37);
            ForeColor = SystemColors.Control;
            Name = "ExpansionGeneralMappingControl";
            Size = new Size(306, 187);
            groupBox37.ResumeLayout(false);
            groupBox37.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox37;
        private Label darkLabel233;
        private ComboBox BuildingIvysComboBox;
        private Label darkLabel94;
        private CheckBox BuildingInteriorsCB;
        private CheckBox UseCustomMappingModuleCB;
    }
}
