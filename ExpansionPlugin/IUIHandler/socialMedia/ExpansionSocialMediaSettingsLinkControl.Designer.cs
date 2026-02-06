namespace ExpansionPlugin
{
    partial class ExpansionSocialMediaSettingsLinkControl
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
            groupBox29 = new GroupBox();
            label9 = new Label();
            ExpansionNewsFeedLinkSettingURLTB = new TextBox();
            label7 = new Label();
            ExpansionNewsFeedLinkSettingLabelTB = new TextBox();
            ExpansionNewsFeedLinkSettingIconTB = new TextBox();
            label8 = new Label();
            groupBox29.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox29
            // 
            groupBox29.Controls.Add(ExpansionNewsFeedLinkSettingLabelTB);
            groupBox29.Controls.Add(label8);
            groupBox29.Controls.Add(label9);
            groupBox29.Controls.Add(ExpansionNewsFeedLinkSettingIconTB);
            groupBox29.Controls.Add(ExpansionNewsFeedLinkSettingURLTB);
            groupBox29.Controls.Add(label7);
            groupBox29.ForeColor = SystemColors.Control;
            groupBox29.Location = new Point(0, 0);
            groupBox29.Margin = new Padding(4, 3, 4, 3);
            groupBox29.Name = "groupBox29";
            groupBox29.Padding = new Padding(4, 3, 4, 3);
            groupBox29.Size = new Size(393, 123);
            groupBox29.TabIndex = 18;
            groupBox29.TabStop = false;
            groupBox29.Text = "Social Media Settings";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(12, 87);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(28, 15);
            label9.TabIndex = 132;
            label9.Text = "URL";
            // 
            // ExpansionNewsFeedLinkSettingURLTB
            // 
            ExpansionNewsFeedLinkSettingURLTB.BackColor = Color.FromArgb(60, 63, 65);
            ExpansionNewsFeedLinkSettingURLTB.ForeColor = SystemColors.Control;
            ExpansionNewsFeedLinkSettingURLTB.Location = new Point(55, 84);
            ExpansionNewsFeedLinkSettingURLTB.Margin = new Padding(4, 3, 4, 3);
            ExpansionNewsFeedLinkSettingURLTB.Name = "ExpansionNewsFeedLinkSettingURLTB";
            ExpansionNewsFeedLinkSettingURLTB.Size = new Size(324, 23);
            ExpansionNewsFeedLinkSettingURLTB.TabIndex = 131;
            ExpansionNewsFeedLinkSettingURLTB.TextChanged += ExpansionNewsFeedLinkSettingURLTB_TextChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 58);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(30, 15);
            label7.TabIndex = 130;
            label7.Text = "Icon";
            // 
            // ExpansionNewsFeedLinkSettingLabelTB
            // 
            ExpansionNewsFeedLinkSettingLabelTB.BackColor = Color.FromArgb(60, 63, 65);
            ExpansionNewsFeedLinkSettingLabelTB.ForeColor = SystemColors.Control;
            ExpansionNewsFeedLinkSettingLabelTB.Location = new Point(55, 26);
            ExpansionNewsFeedLinkSettingLabelTB.Margin = new Padding(4, 3, 4, 3);
            ExpansionNewsFeedLinkSettingLabelTB.Name = "ExpansionNewsFeedLinkSettingLabelTB";
            ExpansionNewsFeedLinkSettingLabelTB.Size = new Size(324, 23);
            ExpansionNewsFeedLinkSettingLabelTB.TabIndex = 126;
            ExpansionNewsFeedLinkSettingLabelTB.TextChanged += ExpansionNewsFeedLinkSettingLabelTB_TextChanged;
            // 
            // ExpansionNewsFeedLinkSettingIconTB
            // 
            ExpansionNewsFeedLinkSettingIconTB.BackColor = Color.FromArgb(60, 63, 65);
            ExpansionNewsFeedLinkSettingIconTB.ForeColor = SystemColors.Control;
            ExpansionNewsFeedLinkSettingIconTB.Location = new Point(55, 55);
            ExpansionNewsFeedLinkSettingIconTB.Margin = new Padding(4, 3, 4, 3);
            ExpansionNewsFeedLinkSettingIconTB.Name = "ExpansionNewsFeedLinkSettingIconTB";
            ExpansionNewsFeedLinkSettingIconTB.Size = new Size(324, 23);
            ExpansionNewsFeedLinkSettingIconTB.TabIndex = 128;
            ExpansionNewsFeedLinkSettingIconTB.TextChanged += ExpansionNewsFeedLinkSettingIconTB_TextChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(12, 29);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(35, 15);
            label8.TabIndex = 127;
            label8.Text = "Label";
            // 
            // ExpansionSocialMediaSettingsLinkControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox29);
            ForeColor = SystemColors.Control;
            Name = "ExpansionSocialMediaSettingsLinkControl";
            Size = new Size(393, 123);
            groupBox29.ResumeLayout(false);
            groupBox29.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox29;
        private TextBox ExpansionNewsFeedLinkSettingLabelTB;
        private Label label8;
        private Label label9;
        private TextBox ExpansionNewsFeedLinkSettingIconTB;
        private TextBox ExpansionNewsFeedLinkSettingURLTB;
        private Label label7;
    }
}
