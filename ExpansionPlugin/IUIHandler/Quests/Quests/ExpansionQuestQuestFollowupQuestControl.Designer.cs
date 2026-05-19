namespace ExpansionPlugin
{
    partial class ExpansionQuestQuestFollowupQuestControl
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
            QuestsCB = new ComboBox();
            darkLabel38 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(QuestsCB);
            groupBox1.Controls.Add(darkLabel38);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(625, 56);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Faction Reps";
            // 
            // QuestsCB
            // 
            QuestsCB.BackColor = Color.FromArgb(60, 63, 65);
            QuestsCB.ForeColor = SystemColors.Control;
            QuestsCB.FormattingEnabled = true;
            QuestsCB.Location = new Point(95, 22);
            QuestsCB.Margin = new Padding(4, 3, 4, 3);
            QuestsCB.Name = "QuestsCB";
            QuestsCB.Size = new Size(523, 23);
            QuestsCB.TabIndex = 184;
            QuestsCB.SelectedIndexChanged += QuestsCB_SelectedIndexChanged;
            // 
            // darkLabel38
            // 
            darkLabel38.AutoSize = true;
            darkLabel38.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel38.Location = new Point(15, 25);
            darkLabel38.Margin = new Padding(4, 0, 4, 0);
            darkLabel38.Name = "darkLabel38";
            darkLabel38.Size = new Size(43, 15);
            darkLabel38.TabIndex = 174;
            darkLabel38.Text = "Quests";
            // 
            // ExpansionQuestQuestFollowupQuestControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionQuestQuestFollowupQuestControl";
            Size = new Size(625, 56);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private ComboBox QuestsCB;
        private Label darkLabel38;
    }
}
