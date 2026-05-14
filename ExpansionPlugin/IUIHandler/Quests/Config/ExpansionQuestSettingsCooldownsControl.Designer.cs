namespace ExpansionPlugin
{
    partial class ExpansionQuestSettingsCooldownsControl
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
            QuestCooldownTitleTB = new TextBox();
            darkLabel1 = new Label();
            QuestCooldownTextTB = new TextBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(darkLabel2);
            groupBox1.Controls.Add(QuestCooldownTitleTB);
            groupBox1.Controls.Add(darkLabel1);
            groupBox1.Controls.Add(QuestCooldownTextTB);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(483, 130);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Cooldowns";
            // 
            // darkLabel2
            // 
            darkLabel2.AutoSize = true;
            darkLabel2.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel2.Location = new Point(15, 25);
            darkLabel2.Margin = new Padding(4, 0, 4, 0);
            darkLabel2.Name = "darkLabel2";
            darkLabel2.Size = new Size(122, 15);
            darkLabel2.TabIndex = 184;
            darkLabel2.Text = "Quest Cooldown Title";
            // 
            // QuestCooldownTitleTB
            // 
            QuestCooldownTitleTB.BackColor = Color.FromArgb(60, 63, 65);
            QuestCooldownTitleTB.ForeColor = SystemColors.Control;
            QuestCooldownTitleTB.Location = new Point(183, 22);
            QuestCooldownTitleTB.Margin = new Padding(4, 3, 4, 3);
            QuestCooldownTitleTB.Name = "QuestCooldownTitleTB";
            QuestCooldownTitleTB.Size = new Size(285, 23);
            QuestCooldownTitleTB.TabIndex = 183;
            QuestCooldownTitleTB.TextChanged += QuestCooldownTitleTB_TextChanged;
            // 
            // darkLabel1
            // 
            darkLabel1.AutoSize = true;
            darkLabel1.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel1.Location = new Point(15, 55);
            darkLabel1.Margin = new Padding(4, 0, 4, 0);
            darkLabel1.Name = "darkLabel1";
            darkLabel1.Size = new Size(120, 15);
            darkLabel1.TabIndex = 185;
            darkLabel1.Text = "Quest Cooldown Text";
            // 
            // QuestCooldownTextTB
            // 
            QuestCooldownTextTB.BackColor = Color.FromArgb(60, 63, 65);
            QuestCooldownTextTB.ForeColor = SystemColors.Control;
            QuestCooldownTextTB.Location = new Point(183, 52);
            QuestCooldownTextTB.Margin = new Padding(4, 3, 4, 3);
            QuestCooldownTextTB.Multiline = true;
            QuestCooldownTextTB.Name = "QuestCooldownTextTB";
            QuestCooldownTextTB.ScrollBars = ScrollBars.Vertical;
            QuestCooldownTextTB.Size = new Size(285, 65);
            QuestCooldownTextTB.TabIndex = 186;
            QuestCooldownTextTB.TextChanged += QuestCooldownTextTB_TextChanged;
            // 
            // ExpansionQuestSettingsCooldownsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionQuestSettingsCooldownsControl";
            Size = new Size(483, 130);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label darkLabel2;
        private TextBox QuestCooldownTitleTB;
        private Label darkLabel1;
        private TextBox QuestCooldownTextTB;
    }
}
