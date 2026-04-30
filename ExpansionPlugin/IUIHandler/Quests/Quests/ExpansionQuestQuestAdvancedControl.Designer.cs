namespace ExpansionPlugin
{
    partial class ExpansionQuestQuestAdvancedControl
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
            QuestSuppressQuestLogOnCompetionCB = new CheckBox();
            darkLabel172 = new Label();
            QuestColourPB = new PictureBox();
            darkLabel40 = new Label();
            QuestIsAchievementCB = new CheckBox();
            darkLabel23 = new Label();
            QuestObjectSetFileNameTB = new TextBox();
            darkLabel32 = new Label();
            QuestIsGroupQuestCB = new CheckBox();
            darkLabel29 = new Label();
            QuestCancelQuestOnPlayerDeathCB = new CheckBox();
            darkLabel27 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)QuestColourPB).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(QuestSuppressQuestLogOnCompetionCB);
            groupBox1.Controls.Add(darkLabel172);
            groupBox1.Controls.Add(QuestColourPB);
            groupBox1.Controls.Add(darkLabel40);
            groupBox1.Controls.Add(QuestIsAchievementCB);
            groupBox1.Controls.Add(darkLabel23);
            groupBox1.Controls.Add(QuestObjectSetFileNameTB);
            groupBox1.Controls.Add(darkLabel32);
            groupBox1.Controls.Add(QuestIsGroupQuestCB);
            groupBox1.Controls.Add(darkLabel29);
            groupBox1.Controls.Add(QuestCancelQuestOnPlayerDeathCB);
            groupBox1.Controls.Add(darkLabel27);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(516, 207);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Advanced";
            // 
            // QuestSuppressQuestLogOnCompetionCB
            // 
            QuestSuppressQuestLogOnCompetionCB.AutoSize = true;
            QuestSuppressQuestLogOnCompetionCB.ForeColor = SystemColors.Control;
            QuestSuppressQuestLogOnCompetionCB.Location = new Point(248, 175);
            QuestSuppressQuestLogOnCompetionCB.Margin = new Padding(4, 3, 4, 3);
            QuestSuppressQuestLogOnCompetionCB.Name = "QuestSuppressQuestLogOnCompetionCB";
            QuestSuppressQuestLogOnCompetionCB.Size = new Size(15, 14);
            QuestSuppressQuestLogOnCompetionCB.TabIndex = 172;
            QuestSuppressQuestLogOnCompetionCB.UseVisualStyleBackColor = true;
            QuestSuppressQuestLogOnCompetionCB.CheckedChanged += QuestSuppressQuestLogOnCompetionCB_CheckedChanged;
            // 
            // darkLabel172
            // 
            darkLabel172.AutoSize = true;
            darkLabel172.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel172.Location = new Point(15, 175);
            darkLabel172.Margin = new Padding(4, 0, 4, 0);
            darkLabel172.Name = "darkLabel172";
            darkLabel172.Size = new Size(193, 15);
            darkLabel172.TabIndex = 173;
            darkLabel172.Text = "Suppress Quest Log On Competion";
            // 
            // QuestColourPB
            // 
            QuestColourPB.BorderStyle = BorderStyle.FixedSingle;
            QuestColourPB.ErrorImage = null;
            QuestColourPB.InitialImage = null;
            QuestColourPB.Location = new Point(248, 139);
            QuestColourPB.Margin = new Padding(4, 3, 4, 3);
            QuestColourPB.Name = "QuestColourPB";
            QuestColourPB.Size = new Size(140, 21);
            QuestColourPB.TabIndex = 172;
            QuestColourPB.TabStop = false;
            QuestColourPB.Click += QuestColour_Click;
            // 
            // darkLabel40
            // 
            darkLabel40.AutoSize = true;
            darkLabel40.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel40.Location = new Point(15, 145);
            darkLabel40.Margin = new Padding(4, 0, 4, 0);
            darkLabel40.Name = "darkLabel40";
            darkLabel40.Size = new Size(70, 15);
            darkLabel40.TabIndex = 171;
            darkLabel40.Text = "Quest Color";
            // 
            // QuestIsAchievementCB
            // 
            QuestIsAchievementCB.AutoSize = true;
            QuestIsAchievementCB.ForeColor = SystemColors.Control;
            QuestIsAchievementCB.Location = new Point(248, 116);
            QuestIsAchievementCB.Margin = new Padding(4, 3, 4, 3);
            QuestIsAchievementCB.Name = "QuestIsAchievementCB";
            QuestIsAchievementCB.Size = new Size(15, 14);
            QuestIsAchievementCB.TabIndex = 170;
            QuestIsAchievementCB.UseVisualStyleBackColor = true;
            QuestIsAchievementCB.CheckedChanged += QuestIsAchievementCB_CheckedChanged;
            // 
            // darkLabel23
            // 
            darkLabel23.AutoSize = true;
            darkLabel23.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel23.Location = new Point(15, 115);
            darkLabel23.Margin = new Padding(4, 0, 4, 0);
            darkLabel23.Name = "darkLabel23";
            darkLabel23.Size = new Size(88, 15);
            darkLabel23.TabIndex = 171;
            darkLabel23.Text = "Is Achievement";
            // 
            // QuestObjectSetFileNameTB
            // 
            QuestObjectSetFileNameTB.BackColor = Color.FromArgb(60, 63, 65);
            QuestObjectSetFileNameTB.ForeColor = SystemColors.Control;
            QuestObjectSetFileNameTB.Location = new Point(248, 82);
            QuestObjectSetFileNameTB.Margin = new Padding(4, 3, 4, 3);
            QuestObjectSetFileNameTB.Name = "QuestObjectSetFileNameTB";
            QuestObjectSetFileNameTB.Size = new Size(256, 23);
            QuestObjectSetFileNameTB.TabIndex = 106;
            QuestObjectSetFileNameTB.TextChanged += QuestObjectSetFileNameTB_TextChanged;
            // 
            // darkLabel32
            // 
            darkLabel32.AutoSize = true;
            darkLabel32.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel32.Location = new Point(15, 85);
            darkLabel32.Margin = new Padding(4, 0, 4, 0);
            darkLabel32.Name = "darkLabel32";
            darkLabel32.Size = new Size(114, 15);
            darkLabel32.TabIndex = 107;
            darkLabel32.Text = "Object Set FileName";
            // 
            // QuestIsGroupQuestCB
            // 
            QuestIsGroupQuestCB.AutoSize = true;
            QuestIsGroupQuestCB.ForeColor = SystemColors.Control;
            QuestIsGroupQuestCB.Location = new Point(248, 56);
            QuestIsGroupQuestCB.Margin = new Padding(4, 3, 4, 3);
            QuestIsGroupQuestCB.Name = "QuestIsGroupQuestCB";
            QuestIsGroupQuestCB.Size = new Size(15, 14);
            QuestIsGroupQuestCB.TabIndex = 170;
            QuestIsGroupQuestCB.UseVisualStyleBackColor = true;
            QuestIsGroupQuestCB.CheckedChanged += QuestIsGroupQuestCB_CheckedChanged;
            // 
            // darkLabel29
            // 
            darkLabel29.AutoSize = true;
            darkLabel29.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel29.Location = new Point(15, 55);
            darkLabel29.Margin = new Padding(4, 0, 4, 0);
            darkLabel29.Name = "darkLabel29";
            darkLabel29.Size = new Size(85, 15);
            darkLabel29.TabIndex = 171;
            darkLabel29.Text = "Is Group Quest";
            // 
            // QuestCancelQuestOnPlayerDeathCB
            // 
            QuestCancelQuestOnPlayerDeathCB.AutoSize = true;
            QuestCancelQuestOnPlayerDeathCB.ForeColor = SystemColors.Control;
            QuestCancelQuestOnPlayerDeathCB.Location = new Point(248, 26);
            QuestCancelQuestOnPlayerDeathCB.Margin = new Padding(4, 3, 4, 3);
            QuestCancelQuestOnPlayerDeathCB.Name = "QuestCancelQuestOnPlayerDeathCB";
            QuestCancelQuestOnPlayerDeathCB.Size = new Size(15, 14);
            QuestCancelQuestOnPlayerDeathCB.TabIndex = 170;
            QuestCancelQuestOnPlayerDeathCB.UseVisualStyleBackColor = true;
            QuestCancelQuestOnPlayerDeathCB.CheckedChanged += QuestCancelQuestOnPlayerDeathCB_CheckedChanged;
            // 
            // darkLabel27
            // 
            darkLabel27.AutoSize = true;
            darkLabel27.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel27.Location = new Point(15, 25);
            darkLabel27.Margin = new Padding(4, 0, 4, 0);
            darkLabel27.Name = "darkLabel27";
            darkLabel27.Size = new Size(165, 15);
            darkLabel27.TabIndex = 171;
            darkLabel27.Text = "Cancel Quest On Player Death";
            // 
            // ExpansionQuestQuestAdvancedControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionQuestQuestAdvancedControl";
            Size = new Size(516, 207);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)QuestColourPB).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private CheckBox QuestCancelQuestOnPlayerDeathCB;
        private Label darkLabel27;
        private TextBox QuestObjectSetFileNameTB;
        private Label darkLabel32;
        private CheckBox QuestIsGroupQuestCB;
        private Label darkLabel29;
        private CheckBox QuestIsAchievementCB;
        private Label darkLabel23;
        private PictureBox QuestColourPB;
        private Label darkLabel40;
        private CheckBox QuestSuppressQuestLogOnCompetionCB;
        private Label darkLabel172;
    }
}
