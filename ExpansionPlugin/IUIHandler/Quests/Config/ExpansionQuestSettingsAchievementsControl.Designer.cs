namespace ExpansionPlugin
{
    partial class ExpansionQuestSettingsAchievementsControl
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
            groupBox1 = new GroupBox();
            darkLabel2 = new Label();
            AchievementCompletedTitleTB = new TextBox();
            darkLabel1 = new Label();
            AchievementCompletedTextTB = new TextBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(darkLabel2);
            groupBox1.Controls.Add(AchievementCompletedTitleTB);
            groupBox1.Controls.Add(darkLabel1);
            groupBox1.Controls.Add(AchievementCompletedTextTB);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(483, 130);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Achievements";
            // 
            // darkLabel2
            // 
            darkLabel2.AutoSize = true;
            darkLabel2.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel2.Location = new Point(15, 25);
            darkLabel2.Margin = new Padding(4, 0, 4, 0);
            darkLabel2.Name = "darkLabel2";
            darkLabel2.Size = new Size(165, 15);
            darkLabel2.TabIndex = 184;
            darkLabel2.Text = "Achievement Completed Title";
            // 
            // AchievementCompletedTitleTB
            // 
            AchievementCompletedTitleTB.BackColor = Color.FromArgb(60, 63, 65);
            AchievementCompletedTitleTB.ForeColor = SystemColors.Control;
            AchievementCompletedTitleTB.Location = new Point(183, 22);
            AchievementCompletedTitleTB.Margin = new Padding(4, 3, 4, 3);
            AchievementCompletedTitleTB.Name = "AchievementCompletedTitleTB";
            AchievementCompletedTitleTB.Size = new Size(285, 23);
            AchievementCompletedTitleTB.TabIndex = 183;
            AchievementCompletedTitleTB.TextChanged += AchievementCompletedTitleTB_TextChanged;
            // 
            // darkLabel1
            // 
            darkLabel1.AutoSize = true;
            darkLabel1.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel1.Location = new Point(15, 55);
            darkLabel1.Margin = new Padding(4, 0, 4, 0);
            darkLabel1.Name = "darkLabel1";
            darkLabel1.Size = new Size(163, 15);
            darkLabel1.TabIndex = 185;
            darkLabel1.Text = "Achievement Completed Text";
            // 
            // AchievementCompletedTextTB
            // 
            AchievementCompletedTextTB.BackColor = Color.FromArgb(60, 63, 65);
            AchievementCompletedTextTB.ForeColor = SystemColors.Control;
            AchievementCompletedTextTB.Location = new Point(183, 52);
            AchievementCompletedTextTB.Margin = new Padding(4, 3, 4, 3);
            AchievementCompletedTextTB.Multiline = true;
            AchievementCompletedTextTB.Name = "AchievementCompletedTextTB";
            AchievementCompletedTextTB.ScrollBars = ScrollBars.Vertical;
            AchievementCompletedTextTB.Size = new Size(285, 65);
            AchievementCompletedTextTB.TabIndex = 186;
            AchievementCompletedTextTB.TextChanged += AchievementCompletedTextTB_TextChanged;
            // 
            // ExpansionQuestSettingsAchievementsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionQuestSettingsAchievementsControl";
            Size = new Size(483, 130);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label darkLabel2;
        private TextBox AchievementCompletedTitleTB;
        private Label darkLabel1;
        private TextBox AchievementCompletedTextTB;
    }
}
