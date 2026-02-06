namespace ExpansionPlugin
{
    partial class ExpansionSocialMediaSettingsTextControl
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
            ExpansionNewsFeedTextSettingTitleTB = new TextBox();
            label4 = new Label();
            ExpansionNewsFeedTextSettingTextTB = new TextBox();
            label6 = new Label();
            groupBox29.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox29
            // 
            groupBox29.Controls.Add(ExpansionNewsFeedTextSettingTitleTB);
            groupBox29.Controls.Add(label4);
            groupBox29.Controls.Add(ExpansionNewsFeedTextSettingTextTB);
            groupBox29.Controls.Add(label6);
            groupBox29.ForeColor = SystemColors.Control;
            groupBox29.Location = new Point(0, 0);
            groupBox29.Margin = new Padding(4, 3, 4, 3);
            groupBox29.Name = "groupBox29";
            groupBox29.Padding = new Padding(4, 3, 4, 3);
            groupBox29.Size = new Size(386, 89);
            groupBox29.TabIndex = 18;
            groupBox29.TabStop = false;
            groupBox29.Text = "Social Media Settings";
            // 
            // ExpansionNewsFeedTextSettingTitleTB
            // 
            ExpansionNewsFeedTextSettingTitleTB.BackColor = Color.FromArgb(60, 63, 65);
            ExpansionNewsFeedTextSettingTitleTB.ForeColor = SystemColors.Control;
            ExpansionNewsFeedTextSettingTitleTB.Location = new Point(50, 26);
            ExpansionNewsFeedTextSettingTitleTB.Margin = new Padding(4, 3, 4, 3);
            ExpansionNewsFeedTextSettingTitleTB.Name = "ExpansionNewsFeedTextSettingTitleTB";
            ExpansionNewsFeedTextSettingTitleTB.Size = new Size(324, 23);
            ExpansionNewsFeedTextSettingTitleTB.TabIndex = 3;
            ExpansionNewsFeedTextSettingTitleTB.TextChanged += ExpansionNewsFeedTextSettingTitleTB_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 29);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(30, 15);
            label4.TabIndex = 102;
            label4.Text = "Title";
            // 
            // ExpansionNewsFeedTextSettingTextTB
            // 
            ExpansionNewsFeedTextSettingTextTB.BackColor = Color.FromArgb(60, 63, 65);
            ExpansionNewsFeedTextSettingTextTB.ForeColor = SystemColors.Control;
            ExpansionNewsFeedTextSettingTextTB.Location = new Point(50, 55);
            ExpansionNewsFeedTextSettingTextTB.Margin = new Padding(4, 3, 4, 3);
            ExpansionNewsFeedTextSettingTextTB.Name = "ExpansionNewsFeedTextSettingTextTB";
            ExpansionNewsFeedTextSettingTextTB.Size = new Size(324, 23);
            ExpansionNewsFeedTextSettingTextTB.TabIndex = 4;
            ExpansionNewsFeedTextSettingTextTB.TextChanged += ExpansionNewsFeedTextSettingTextTB_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 58);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(28, 15);
            label6.TabIndex = 125;
            label6.Text = "Text";
            // 
            // ExpansionSocialMediaSettingsTextControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox29);
            ForeColor = SystemColors.Control;
            Name = "ExpansionSocialMediaSettingsTextControl";
            Size = new Size(386, 89);
            groupBox29.ResumeLayout(false);
            groupBox29.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox29;
        private TextBox ExpansionNewsFeedTextSettingTitleTB;
        private Label label4;
        private TextBox ExpansionNewsFeedTextSettingTextTB;
        private Label label6;
    }
}
