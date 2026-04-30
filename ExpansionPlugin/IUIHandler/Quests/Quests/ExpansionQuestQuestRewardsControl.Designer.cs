namespace ExpansionPlugin
{
    partial class ExpansionQuestQuestRewardsControl
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
            QuestRandomRewardAmountNUD = new NumericUpDown();
            darkLabel155 = new Label();
            QuestRandomRewardCB = new CheckBox();
            darkLabel154 = new Label();
            QuestNeedToSelectRewardCB = new CheckBox();
            darkLabel38 = new Label();
            QuestRewardsForGroupOwnerOnlyCB = new CheckBox();
            darkLabel39 = new Label();
            QuestRewardBehavorCB = new ComboBox();
            darkLabel56 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)QuestRandomRewardAmountNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(QuestRewardBehavorCB);
            groupBox1.Controls.Add(darkLabel56);
            groupBox1.Controls.Add(QuestRandomRewardAmountNUD);
            groupBox1.Controls.Add(darkLabel155);
            groupBox1.Controls.Add(QuestRandomRewardCB);
            groupBox1.Controls.Add(darkLabel154);
            groupBox1.Controls.Add(QuestNeedToSelectRewardCB);
            groupBox1.Controls.Add(darkLabel38);
            groupBox1.Controls.Add(QuestRewardsForGroupOwnerOnlyCB);
            groupBox1.Controls.Add(darkLabel39);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(516, 175);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Rewards";
            // 
            // QuestRandomRewardAmountNUD
            // 
            QuestRandomRewardAmountNUD.BackColor = Color.FromArgb(60, 63, 65);
            QuestRandomRewardAmountNUD.ForeColor = SystemColors.Control;
            QuestRandomRewardAmountNUD.Location = new Point(216, 113);
            QuestRandomRewardAmountNUD.Margin = new Padding(4, 3, 4, 3);
            QuestRandomRewardAmountNUD.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            QuestRandomRewardAmountNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            QuestRandomRewardAmountNUD.Name = "QuestRandomRewardAmountNUD";
            QuestRandomRewardAmountNUD.Size = new Size(140, 23);
            QuestRandomRewardAmountNUD.TabIndex = 179;
            QuestRandomRewardAmountNUD.TextAlign = HorizontalAlignment.Center;
            QuestRandomRewardAmountNUD.Value = new decimal(new int[] { 1, 0, 0, int.MinValue });
            QuestRandomRewardAmountNUD.ValueChanged += QuestRandomRewardAmountNUD_ValueChanged;
            // 
            // darkLabel155
            // 
            darkLabel155.AutoSize = true;
            darkLabel155.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel155.Location = new Point(15, 115);
            darkLabel155.Margin = new Padding(4, 0, 4, 0);
            darkLabel155.Name = "darkLabel155";
            darkLabel155.Size = new Size(141, 15);
            darkLabel155.TabIndex = 178;
            darkLabel155.Text = "Random Reward Amount";
            // 
            // QuestRandomRewardCB
            // 
            QuestRandomRewardCB.AutoSize = true;
            QuestRandomRewardCB.ForeColor = SystemColors.Control;
            QuestRandomRewardCB.Location = new Point(216, 86);
            QuestRandomRewardCB.Margin = new Padding(4, 3, 4, 3);
            QuestRandomRewardCB.Name = "QuestRandomRewardCB";
            QuestRandomRewardCB.Size = new Size(15, 14);
            QuestRandomRewardCB.TabIndex = 176;
            QuestRandomRewardCB.UseVisualStyleBackColor = true;
            QuestRandomRewardCB.CheckedChanged += QuestRandomRewardCB_CheckedChanged;
            // 
            // darkLabel154
            // 
            darkLabel154.AutoSize = true;
            darkLabel154.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel154.Location = new Point(15, 85);
            darkLabel154.Margin = new Padding(4, 0, 4, 0);
            darkLabel154.Name = "darkLabel154";
            darkLabel154.Size = new Size(94, 15);
            darkLabel154.TabIndex = 177;
            darkLabel154.Text = "Random Reward";
            // 
            // QuestNeedToSelectRewardCB
            // 
            QuestNeedToSelectRewardCB.AutoSize = true;
            QuestNeedToSelectRewardCB.ForeColor = SystemColors.Control;
            QuestNeedToSelectRewardCB.Location = new Point(216, 26);
            QuestNeedToSelectRewardCB.Margin = new Padding(4, 3, 4, 3);
            QuestNeedToSelectRewardCB.Name = "QuestNeedToSelectRewardCB";
            QuestNeedToSelectRewardCB.Size = new Size(15, 14);
            QuestNeedToSelectRewardCB.TabIndex = 172;
            QuestNeedToSelectRewardCB.UseVisualStyleBackColor = true;
            QuestNeedToSelectRewardCB.CheckedChanged += QuestNeedToSelectRewardCB_CheckedChanged;
            // 
            // darkLabel38
            // 
            darkLabel38.AutoSize = true;
            darkLabel38.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel38.Location = new Point(15, 25);
            darkLabel38.Margin = new Padding(4, 0, 4, 0);
            darkLabel38.Name = "darkLabel38";
            darkLabel38.Size = new Size(127, 15);
            darkLabel38.TabIndex = 174;
            darkLabel38.Text = "Need To Select Reward";
            // 
            // QuestRewardsForGroupOwnerOnlyCB
            // 
            QuestRewardsForGroupOwnerOnlyCB.AutoSize = true;
            QuestRewardsForGroupOwnerOnlyCB.ForeColor = SystemColors.Control;
            QuestRewardsForGroupOwnerOnlyCB.Location = new Point(216, 56);
            QuestRewardsForGroupOwnerOnlyCB.Margin = new Padding(4, 3, 4, 3);
            QuestRewardsForGroupOwnerOnlyCB.Name = "QuestRewardsForGroupOwnerOnlyCB";
            QuestRewardsForGroupOwnerOnlyCB.Size = new Size(15, 14);
            QuestRewardsForGroupOwnerOnlyCB.TabIndex = 173;
            QuestRewardsForGroupOwnerOnlyCB.UseVisualStyleBackColor = true;
            QuestRewardsForGroupOwnerOnlyCB.CheckedChanged += QuestRewardsForGroupOwnerOnlyCB_CheckedChanged;
            // 
            // darkLabel39
            // 
            darkLabel39.AutoSize = true;
            darkLabel39.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel39.Location = new Point(15, 55);
            darkLabel39.Margin = new Padding(4, 0, 4, 0);
            darkLabel39.Name = "darkLabel39";
            darkLabel39.Size = new Size(173, 15);
            darkLabel39.TabIndex = 175;
            darkLabel39.Text = "Rewards For Group Owner Only";
            // 
            // QuestRewardBehavorCB
            // 
            QuestRewardBehavorCB.BackColor = Color.FromArgb(60, 63, 65);
            QuestRewardBehavorCB.ForeColor = SystemColors.Control;
            QuestRewardBehavorCB.FormattingEnabled = true;
            QuestRewardBehavorCB.Location = new Point(216, 142);
            QuestRewardBehavorCB.Margin = new Padding(4, 3, 4, 3);
            QuestRewardBehavorCB.Name = "QuestRewardBehavorCB";
            QuestRewardBehavorCB.Size = new Size(289, 23);
            QuestRewardBehavorCB.TabIndex = 180;
            QuestRewardBehavorCB.SelectedIndexChanged += QuestRewardBehavorCB_SelectedIndexChanged;
            // 
            // darkLabel56
            // 
            darkLabel56.AutoSize = true;
            darkLabel56.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel56.Location = new Point(15, 145);
            darkLabel56.Margin = new Padding(4, 0, 4, 0);
            darkLabel56.Name = "darkLabel56";
            darkLabel56.Size = new Size(95, 15);
            darkLabel56.TabIndex = 181;
            darkLabel56.Text = "Reward Behavior";
            // 
            // ExpansionQuestQuestRewardsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionQuestQuestRewardsControl";
            Size = new Size(516, 175);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)QuestRandomRewardAmountNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private CheckBox QuestNeedToSelectRewardCB;
        private Label darkLabel38;
        private CheckBox QuestRewardsForGroupOwnerOnlyCB;
        private Label darkLabel39;
        private CheckBox QuestRandomRewardCB;
        private Label darkLabel154;
        private NumericUpDown QuestRandomRewardAmountNUD;
        private Label darkLabel155;
        private ComboBox QuestRewardBehavorCB;
        private Label darkLabel56;
    }
}
