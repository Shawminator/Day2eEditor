namespace ExpansionPlugin
{
    partial class ExpansionQuestQuestRequirementsControl
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
            RequiredFactionCB = new ComboBox();
            ReputationRequirementNUD = new NumericUpDown();
            ReputationRewardNUD = new NumericUpDown();
            darkLabel155 = new Label();
            darkLabel154 = new Label();
            darkLabel38 = new Label();
            darkLabel39 = new Label();
            FactionRewardCB = new ComboBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ReputationRequirementNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ReputationRewardNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(FactionRewardCB);
            groupBox1.Controls.Add(RequiredFactionCB);
            groupBox1.Controls.Add(ReputationRequirementNUD);
            groupBox1.Controls.Add(ReputationRewardNUD);
            groupBox1.Controls.Add(darkLabel155);
            groupBox1.Controls.Add(darkLabel154);
            groupBox1.Controls.Add(darkLabel38);
            groupBox1.Controls.Add(darkLabel39);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(514, 146);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Reputation / Faction";
            // 
            // RequiredFactionCB
            // 
            RequiredFactionCB.BackColor = Color.FromArgb(60, 63, 65);
            RequiredFactionCB.ForeColor = SystemColors.Control;
            RequiredFactionCB.FormattingEnabled = true;
            RequiredFactionCB.Location = new Point(216, 82);
            RequiredFactionCB.Margin = new Padding(4, 3, 4, 3);
            RequiredFactionCB.Name = "RequiredFactionCB";
            RequiredFactionCB.Size = new Size(289, 23);
            RequiredFactionCB.TabIndex = 184;
            RequiredFactionCB.SelectedIndexChanged += RequiredFactionCB_SelectedIndexChanged;
            // 
            // ReputationRequirementNUD
            // 
            ReputationRequirementNUD.BackColor = Color.FromArgb(60, 63, 65);
            ReputationRequirementNUD.ForeColor = SystemColors.Control;
            ReputationRequirementNUD.Location = new Point(216, 53);
            ReputationRequirementNUD.Margin = new Padding(4, 3, 4, 3);
            ReputationRequirementNUD.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            ReputationRequirementNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            ReputationRequirementNUD.Name = "ReputationRequirementNUD";
            ReputationRequirementNUD.Size = new Size(140, 23);
            ReputationRequirementNUD.TabIndex = 183;
            ReputationRequirementNUD.TextAlign = HorizontalAlignment.Center;
            ReputationRequirementNUD.Value = new decimal(new int[] { 1, 0, 0, int.MinValue });
            ReputationRequirementNUD.ValueChanged += ReputationRequirementNUD_ValueChanged;
            // 
            // ReputationRewardNUD
            // 
            ReputationRewardNUD.BackColor = Color.FromArgb(60, 63, 65);
            ReputationRewardNUD.ForeColor = SystemColors.Control;
            ReputationRewardNUD.Location = new Point(216, 23);
            ReputationRewardNUD.Margin = new Padding(4, 3, 4, 3);
            ReputationRewardNUD.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            ReputationRewardNUD.Name = "ReputationRewardNUD";
            ReputationRewardNUD.Size = new Size(140, 23);
            ReputationRewardNUD.TabIndex = 182;
            ReputationRewardNUD.TextAlign = HorizontalAlignment.Center;
            ReputationRewardNUD.ValueChanged += ReputationRewardNUD_ValueChanged;
            // 
            // darkLabel155
            // 
            darkLabel155.AutoSize = true;
            darkLabel155.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel155.Location = new Point(15, 115);
            darkLabel155.Margin = new Padding(4, 0, 4, 0);
            darkLabel155.Name = "darkLabel155";
            darkLabel155.Size = new Size(88, 15);
            darkLabel155.TabIndex = 178;
            darkLabel155.Text = "Faction Reward";
            // 
            // darkLabel154
            // 
            darkLabel154.AutoSize = true;
            darkLabel154.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel154.Location = new Point(15, 85);
            darkLabel154.Margin = new Padding(4, 0, 4, 0);
            darkLabel154.Name = "darkLabel154";
            darkLabel154.Size = new Size(96, 15);
            darkLabel154.TabIndex = 177;
            darkLabel154.Text = "Required Faction";
            // 
            // darkLabel38
            // 
            darkLabel38.AutoSize = true;
            darkLabel38.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel38.Location = new Point(15, 25);
            darkLabel38.Margin = new Padding(4, 0, 4, 0);
            darkLabel38.Name = "darkLabel38";
            darkLabel38.Size = new Size(107, 15);
            darkLabel38.TabIndex = 174;
            darkLabel38.Text = "Reputation Reward";
            // 
            // darkLabel39
            // 
            darkLabel39.AutoSize = true;
            darkLabel39.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel39.Location = new Point(15, 55);
            darkLabel39.Margin = new Padding(4, 0, 4, 0);
            darkLabel39.Name = "darkLabel39";
            darkLabel39.Size = new Size(136, 15);
            darkLabel39.TabIndex = 175;
            darkLabel39.Text = "Reputation Requirement";
            // 
            // FactionRewardCB
            // 
            FactionRewardCB.BackColor = Color.FromArgb(60, 63, 65);
            FactionRewardCB.ForeColor = SystemColors.Control;
            FactionRewardCB.FormattingEnabled = true;
            FactionRewardCB.Location = new Point(216, 112);
            FactionRewardCB.Margin = new Padding(4, 3, 4, 3);
            FactionRewardCB.Name = "FactionRewardCB";
            FactionRewardCB.Size = new Size(289, 23);
            FactionRewardCB.TabIndex = 185;
            FactionRewardCB.SelectedIndexChanged += FactionRewardCB_SelectedIndexChanged;
            // 
            // ExpansionQuestQuestRequirementsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionQuestQuestRequirementsControl";
            Size = new Size(519, 157);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ReputationRequirementNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)ReputationRewardNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label darkLabel155;
        private Label darkLabel154;
        private Label darkLabel38;
        private Label darkLabel39;
        private ComboBox RequiredFactionCB;
        private NumericUpDown ReputationRequirementNUD;
        private NumericUpDown ReputationRewardNUD;
        private ComboBox FactionRewardCB;
    }
}
