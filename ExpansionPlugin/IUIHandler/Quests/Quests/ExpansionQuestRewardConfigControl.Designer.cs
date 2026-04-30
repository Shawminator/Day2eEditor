namespace ExpansionPlugin
{
    partial class ExpansionQuestRewardConfigControl
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
            darkLabel59 = new Label();
            QuestRewardChanceNUD = new NumericUpDown();
            darkLabel153 = new Label();
            QuestRewardsQuestIDNUD = new NumericUpDown();
            darkLabel152 = new Label();
            QuestRewardsDamagePercentNUD = new NumericUpDown();
            darkLabel137 = new Label();
            QuestRewardsHealthPercentNUD = new NumericUpDown();
            darkLabel68 = new Label();
            QuestRewardsAmountNUD = new NumericUpDown();
            darkButton56 = new Button();
            darkLabel32 = new Label();
            QuestItemClassnameTB = new TextBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)QuestRewardChanceNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)QuestRewardsQuestIDNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)QuestRewardsDamagePercentNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)QuestRewardsHealthPercentNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)QuestRewardsAmountNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(QuestRewardChanceNUD);
            groupBox1.Controls.Add(QuestRewardsQuestIDNUD);
            groupBox1.Controls.Add(QuestRewardsDamagePercentNUD);
            groupBox1.Controls.Add(QuestRewardsHealthPercentNUD);
            groupBox1.Controls.Add(QuestRewardsAmountNUD);
            groupBox1.Controls.Add(darkLabel59);
            groupBox1.Controls.Add(darkLabel153);
            groupBox1.Controls.Add(darkLabel152);
            groupBox1.Controls.Add(darkLabel137);
            groupBox1.Controls.Add(darkLabel68);
            groupBox1.Controls.Add(darkButton56);
            groupBox1.Controls.Add(darkLabel32);
            groupBox1.Controls.Add(QuestItemClassnameTB);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(424, 208);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Reward Items";
            // 
            // darkLabel59
            // 
            darkLabel59.AutoSize = true;
            darkLabel59.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel59.Location = new Point(15, 175);
            darkLabel59.Margin = new Padding(4, 0, 4, 0);
            darkLabel59.Name = "darkLabel59";
            darkLabel59.Size = new Size(47, 15);
            darkLabel59.TabIndex = 112;
            darkLabel59.Text = "Chance";
            // 
            // QuestRewardChanceNUD
            // 
            QuestRewardChanceNUD.BackColor = Color.FromArgb(60, 63, 65);
            QuestRewardChanceNUD.DecimalPlaces = 2;
            QuestRewardChanceNUD.ForeColor = SystemColors.Control;
            QuestRewardChanceNUD.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            QuestRewardChanceNUD.Location = new Point(129, 173);
            QuestRewardChanceNUD.Margin = new Padding(4, 3, 4, 3);
            QuestRewardChanceNUD.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            QuestRewardChanceNUD.Name = "QuestRewardChanceNUD";
            QuestRewardChanceNUD.Size = new Size(140, 23);
            QuestRewardChanceNUD.TabIndex = 108;
            QuestRewardChanceNUD.TextAlign = HorizontalAlignment.Center;
            QuestRewardChanceNUD.ValueChanged += QuestRewardChanceNUD_ValueChanged;
            // 
            // darkLabel153
            // 
            darkLabel153.AutoSize = true;
            darkLabel153.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel153.Location = new Point(15, 145);
            darkLabel153.Margin = new Padding(4, 0, 4, 0);
            darkLabel153.Name = "darkLabel153";
            darkLabel153.Size = new Size(52, 15);
            darkLabel153.TabIndex = 112;
            darkLabel153.Text = "Quest ID";
            // 
            // QuestRewardsQuestIDNUD
            // 
            QuestRewardsQuestIDNUD.BackColor = Color.FromArgb(60, 63, 65);
            QuestRewardsQuestIDNUD.ForeColor = SystemColors.Control;
            QuestRewardsQuestIDNUD.Location = new Point(129, 143);
            QuestRewardsQuestIDNUD.Margin = new Padding(4, 3, 4, 3);
            QuestRewardsQuestIDNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            QuestRewardsQuestIDNUD.Name = "QuestRewardsQuestIDNUD";
            QuestRewardsQuestIDNUD.Size = new Size(140, 23);
            QuestRewardsQuestIDNUD.TabIndex = 108;
            QuestRewardsQuestIDNUD.TextAlign = HorizontalAlignment.Center;
            QuestRewardsQuestIDNUD.ValueChanged += QuestRewardsQuestIDNUD_ValueChanged;
            // 
            // darkLabel152
            // 
            darkLabel152.AutoSize = true;
            darkLabel152.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel152.Location = new Point(15, 115);
            darkLabel152.Margin = new Padding(4, 0, 4, 0);
            darkLabel152.Name = "darkLabel152";
            darkLabel152.Size = new Size(97, 15);
            darkLabel152.TabIndex = 112;
            darkLabel152.Text = "Damage  Percent";
            // 
            // QuestRewardsDamagePercentNUD
            // 
            QuestRewardsDamagePercentNUD.BackColor = Color.FromArgb(60, 63, 65);
            QuestRewardsDamagePercentNUD.ForeColor = SystemColors.Control;
            QuestRewardsDamagePercentNUD.Location = new Point(129, 113);
            QuestRewardsDamagePercentNUD.Margin = new Padding(4, 3, 4, 3);
            QuestRewardsDamagePercentNUD.Name = "QuestRewardsDamagePercentNUD";
            QuestRewardsDamagePercentNUD.Size = new Size(140, 23);
            QuestRewardsDamagePercentNUD.TabIndex = 108;
            QuestRewardsDamagePercentNUD.TextAlign = HorizontalAlignment.Center;
            QuestRewardsDamagePercentNUD.ValueChanged += QuestRewardsDamagePercentNUD_ValueChanged;
            // 
            // darkLabel137
            // 
            darkLabel137.AutoSize = true;
            darkLabel137.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel137.Location = new Point(15, 85);
            darkLabel137.Margin = new Padding(4, 0, 4, 0);
            darkLabel137.Name = "darkLabel137";
            darkLabel137.Size = new Size(85, 15);
            darkLabel137.TabIndex = 112;
            darkLabel137.Text = "Health Percent";
            // 
            // QuestRewardsHealthPercentNUD
            // 
            QuestRewardsHealthPercentNUD.BackColor = Color.FromArgb(60, 63, 65);
            QuestRewardsHealthPercentNUD.ForeColor = SystemColors.Control;
            QuestRewardsHealthPercentNUD.Location = new Point(129, 83);
            QuestRewardsHealthPercentNUD.Margin = new Padding(4, 3, 4, 3);
            QuestRewardsHealthPercentNUD.Name = "QuestRewardsHealthPercentNUD";
            QuestRewardsHealthPercentNUD.Size = new Size(140, 23);
            QuestRewardsHealthPercentNUD.TabIndex = 108;
            QuestRewardsHealthPercentNUD.TextAlign = HorizontalAlignment.Center;
            QuestRewardsHealthPercentNUD.ValueChanged += QuestRewardsHealthPercentNUD_ValueChanged;
            // 
            // darkLabel68
            // 
            darkLabel68.AutoSize = true;
            darkLabel68.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel68.Location = new Point(15, 55);
            darkLabel68.Margin = new Padding(4, 0, 4, 0);
            darkLabel68.Name = "darkLabel68";
            darkLabel68.Size = new Size(78, 15);
            darkLabel68.TabIndex = 112;
            darkLabel68.Text = "Item Amount";
            // 
            // QuestRewardsAmountNUD
            // 
            QuestRewardsAmountNUD.BackColor = Color.FromArgb(60, 63, 65);
            QuestRewardsAmountNUD.ForeColor = SystemColors.Control;
            QuestRewardsAmountNUD.Location = new Point(129, 53);
            QuestRewardsAmountNUD.Margin = new Padding(4, 3, 4, 3);
            QuestRewardsAmountNUD.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            QuestRewardsAmountNUD.Name = "QuestRewardsAmountNUD";
            QuestRewardsAmountNUD.Size = new Size(140, 23);
            QuestRewardsAmountNUD.TabIndex = 108;
            QuestRewardsAmountNUD.TextAlign = HorizontalAlignment.Center;
            QuestRewardsAmountNUD.ValueChanged += QuestRewardsAmountNUD_ValueChanged;
            // 
            // darkButton56
            // 
            darkButton56.FlatStyle = FlatStyle.Flat;
            darkButton56.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            darkButton56.Location = new Point(392, 18);
            darkButton56.Name = "darkButton56";
            darkButton56.Size = new Size(23, 23);
            darkButton56.TabIndex = 334;
            darkButton56.Text = "+";
            darkButton56.Click += darkButton56_Click;
            // 
            // darkLabel32
            // 
            darkLabel32.AutoSize = true;
            darkLabel32.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel32.Location = new Point(15, 25);
            darkLabel32.Margin = new Padding(4, 0, 4, 0);
            darkLabel32.Name = "darkLabel32";
            darkLabel32.Size = new Size(64, 15);
            darkLabel32.TabIndex = 333;
            darkLabel32.Text = "Classname";
            // 
            // QuestItemClassnameTB
            // 
            QuestItemClassnameTB.BackColor = Color.FromArgb(60, 63, 65);
            QuestItemClassnameTB.ForeColor = SystemColors.Control;
            QuestItemClassnameTB.Location = new Point(129, 18);
            QuestItemClassnameTB.Margin = new Padding(4, 3, 4, 3);
            QuestItemClassnameTB.Name = "QuestItemClassnameTB";
            QuestItemClassnameTB.Size = new Size(256, 23);
            QuestItemClassnameTB.TabIndex = 332;
            QuestItemClassnameTB.TextChanged += QuestItemClassnameTB_TextChanged;
            // 
            // ExpansionQuestRewardConfigControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionQuestRewardConfigControl";
            Size = new Size(424, 208);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)QuestRewardChanceNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)QuestRewardsQuestIDNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)QuestRewardsDamagePercentNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)QuestRewardsHealthPercentNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)QuestRewardsAmountNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label darkLabel59;
        private NumericUpDown QuestRewardChanceNUD;
        private Label darkLabel153;
        private NumericUpDown QuestRewardsQuestIDNUD;
        private Label darkLabel152;
        private NumericUpDown QuestRewardsDamagePercentNUD;
        private Label darkLabel137;
        private NumericUpDown QuestRewardsHealthPercentNUD;
        private Label darkLabel68;
        private NumericUpDown QuestRewardsAmountNUD;
        private Button darkButton56;
        private Label darkLabel32;
        private TextBox QuestItemClassnameTB;
    }
}
