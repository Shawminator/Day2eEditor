namespace ExpansionPlugin
{
    partial class ExpansionRaidSettingsControl
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
            groupBox25 = new GroupBox();
            BaseBuildingRaidModeComboBox = new ComboBox();
            darkLabel71 = new Label();
            groupBox25.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox25
            // 
            groupBox25.Controls.Add(BaseBuildingRaidModeComboBox);
            groupBox25.Controls.Add(darkLabel71);
            groupBox25.ForeColor = SystemColors.Control;
            groupBox25.Location = new Point(0, 0);
            groupBox25.Margin = new Padding(4, 3, 4, 3);
            groupBox25.Name = "groupBox25";
            groupBox25.Padding = new Padding(4, 3, 4, 3);
            groupBox25.Size = new Size(650, 57);
            groupBox25.TabIndex = 11;
            groupBox25.TabStop = false;
            groupBox25.Text = "General";
            // 
            // BaseBuildingRaidModeComboBox
            // 
            BaseBuildingRaidModeComboBox.FormattingEnabled = true;
            BaseBuildingRaidModeComboBox.Location = new Point(82, 22);
            BaseBuildingRaidModeComboBox.Margin = new Padding(4, 3, 4, 3);
            BaseBuildingRaidModeComboBox.Name = "BaseBuildingRaidModeComboBox";
            BaseBuildingRaidModeComboBox.Size = new Size(560, 23);
            BaseBuildingRaidModeComboBox.TabIndex = 1;
            BaseBuildingRaidModeComboBox.SelectedIndexChanged += BaseBuildingRaidModeComboBox_SelectedIndexChanged;
            // 
            // darkLabel71
            // 
            darkLabel71.AutoSize = true;
            darkLabel71.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel71.Location = new Point(10, 27);
            darkLabel71.Margin = new Padding(4, 0, 4, 0);
            darkLabel71.Name = "darkLabel71";
            darkLabel71.Size = new Size(64, 15);
            darkLabel71.TabIndex = 0;
            darkLabel71.Text = "Raid Mode";
            // 
            // ExpansionRaidSettingsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox25);
            ForeColor = SystemColors.Control;
            Name = "ExpansionRaidSettingsControl";
            Size = new Size(650, 57);
            groupBox25.ResumeLayout(false);
            groupBox25.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox25;
        private ComboBox BaseBuildingRaidModeComboBox;
        private Label darkLabel71;
    }
}
