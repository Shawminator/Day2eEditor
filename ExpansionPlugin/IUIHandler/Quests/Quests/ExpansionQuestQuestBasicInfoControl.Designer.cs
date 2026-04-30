namespace ExpansionPlugin
{
    partial class ExpansionQuestQuestBasicInfoControl
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
            QuestObjectivesBaseInfoGB = new GroupBox();
            QuestFilenameTB = new TextBox();
            darkLabel44 = new Label();
            QuestConfigVersionNUD = new NumericUpDown();
            QuestsIDNUD = new NumericUpDown();
            darkLabel45 = new Label();
            QuestTypeCB = new ComboBox();
            darkLabel46 = new Label();
            QuestActiveCB = new CheckBox();
            darkLabel47 = new Label();
            label2 = new Label();
            QuestObjectivesBaseInfoGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)QuestConfigVersionNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)QuestsIDNUD).BeginInit();
            SuspendLayout();
            // 
            // QuestObjectivesBaseInfoGB
            // 
            QuestObjectivesBaseInfoGB.Controls.Add(QuestFilenameTB);
            QuestObjectivesBaseInfoGB.Controls.Add(darkLabel44);
            QuestObjectivesBaseInfoGB.Controls.Add(QuestConfigVersionNUD);
            QuestObjectivesBaseInfoGB.Controls.Add(QuestsIDNUD);
            QuestObjectivesBaseInfoGB.Controls.Add(darkLabel45);
            QuestObjectivesBaseInfoGB.Controls.Add(QuestTypeCB);
            QuestObjectivesBaseInfoGB.Controls.Add(darkLabel46);
            QuestObjectivesBaseInfoGB.Controls.Add(QuestActiveCB);
            QuestObjectivesBaseInfoGB.Controls.Add(darkLabel47);
            QuestObjectivesBaseInfoGB.Controls.Add(label2);
            QuestObjectivesBaseInfoGB.ForeColor = SystemColors.Control;
            QuestObjectivesBaseInfoGB.Location = new Point(0, 0);
            QuestObjectivesBaseInfoGB.Margin = new Padding(4, 3, 4, 3);
            QuestObjectivesBaseInfoGB.Name = "QuestObjectivesBaseInfoGB";
            QuestObjectivesBaseInfoGB.Padding = new Padding(4, 3, 4, 3);
            QuestObjectivesBaseInfoGB.Size = new Size(418, 172);
            QuestObjectivesBaseInfoGB.TabIndex = 218;
            QuestObjectivesBaseInfoGB.TabStop = false;
            QuestObjectivesBaseInfoGB.Text = "Base Info";
            // 
            // QuestFilenameTB
            // 
            QuestFilenameTB.BackColor = Color.FromArgb(60, 63, 65);
            QuestFilenameTB.ForeColor = SystemColors.Control;
            QuestFilenameTB.Location = new Point(114, 22);
            QuestFilenameTB.Margin = new Padding(4, 3, 4, 3);
            QuestFilenameTB.Name = "QuestFilenameTB";
            QuestFilenameTB.Size = new Size(289, 23);
            QuestFilenameTB.TabIndex = 110;
            QuestFilenameTB.TextChanged += QuestFilenameTB_TextChanged;
            // 
            // darkLabel44
            // 
            darkLabel44.AutoSize = true;
            darkLabel44.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel44.Location = new Point(15, 25);
            darkLabel44.Margin = new Padding(4, 0, 4, 0);
            darkLabel44.Name = "darkLabel44";
            darkLabel44.Size = new Size(60, 15);
            darkLabel44.TabIndex = 111;
            darkLabel44.Text = "File Name";
            // 
            // QuestConfigVersionNUD
            // 
            QuestConfigVersionNUD.BackColor = Color.FromArgb(60, 63, 65);
            QuestConfigVersionNUD.ForeColor = SystemColors.Control;
            QuestConfigVersionNUD.Location = new Point(114, 53);
            QuestConfigVersionNUD.Margin = new Padding(4, 3, 4, 3);
            QuestConfigVersionNUD.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            QuestConfigVersionNUD.Name = "QuestConfigVersionNUD";
            QuestConfigVersionNUD.ReadOnly = true;
            QuestConfigVersionNUD.Size = new Size(140, 23);
            QuestConfigVersionNUD.TabIndex = 106;
            QuestConfigVersionNUD.TextAlign = HorizontalAlignment.Center;
            QuestConfigVersionNUD.ValueChanged += QuestConfigVersionNUD_ValueChanged;
            // 
            // QuestsIDNUD
            // 
            QuestsIDNUD.BackColor = Color.FromArgb(60, 63, 65);
            QuestsIDNUD.ForeColor = SystemColors.Control;
            QuestsIDNUD.Location = new Point(114, 83);
            QuestsIDNUD.Margin = new Padding(4, 3, 4, 3);
            QuestsIDNUD.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            QuestsIDNUD.Name = "QuestsIDNUD";
            QuestsIDNUD.Size = new Size(140, 23);
            QuestsIDNUD.TabIndex = 107;
            QuestsIDNUD.TextAlign = HorizontalAlignment.Center;
            QuestsIDNUD.ValueChanged += QuestsIDNUD_ValueChanged;
            // 
            // darkLabel45
            // 
            darkLabel45.AutoSize = true;
            darkLabel45.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel45.Location = new Point(15, 55);
            darkLabel45.Margin = new Padding(4, 0, 4, 0);
            darkLabel45.Name = "darkLabel45";
            darkLabel45.Size = new Size(84, 15);
            darkLabel45.TabIndex = 109;
            darkLabel45.Text = "Config Version";
            // 
            // QuestTypeCB
            // 
            QuestTypeCB.BackColor = Color.FromArgb(60, 63, 65);
            QuestTypeCB.ForeColor = SystemColors.Control;
            QuestTypeCB.FormattingEnabled = true;
            QuestTypeCB.Location = new Point(114, 112);
            QuestTypeCB.Margin = new Padding(4, 3, 4, 3);
            QuestTypeCB.Name = "QuestTypeCB";
            QuestTypeCB.Size = new Size(289, 23);
            QuestTypeCB.TabIndex = 128;
            QuestTypeCB.SelectedIndexChanged += QuestTypeCB_SelectedIndexChanged;
            // 
            // darkLabel46
            // 
            darkLabel46.AutoSize = true;
            darkLabel46.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel46.Location = new Point(15, 85);
            darkLabel46.Margin = new Padding(4, 0, 4, 0);
            darkLabel46.Name = "darkLabel46";
            darkLabel46.Size = new Size(18, 15);
            darkLabel46.TabIndex = 108;
            darkLabel46.Text = "ID";
            // 
            // QuestActiveCB
            // 
            QuestActiveCB.AutoSize = true;
            QuestActiveCB.ForeColor = SystemColors.Control;
            QuestActiveCB.Location = new Point(114, 146);
            QuestActiveCB.Margin = new Padding(4, 3, 4, 3);
            QuestActiveCB.Name = "QuestActiveCB";
            QuestActiveCB.Size = new Size(15, 14);
            QuestActiveCB.TabIndex = 164;
            QuestActiveCB.UseVisualStyleBackColor = true;
            QuestActiveCB.CheckedChanged += QuestObjectivesActiveCB_CheckedChanged;
            // 
            // darkLabel47
            // 
            darkLabel47.AutoSize = true;
            darkLabel47.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel47.Location = new Point(15, 115);
            darkLabel47.Margin = new Padding(4, 0, 4, 0);
            darkLabel47.Name = "darkLabel47";
            darkLabel47.Size = new Size(66, 15);
            darkLabel47.TabIndex = 129;
            darkLabel47.Text = "Quest Type";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.Control;
            label2.Location = new Point(15, 145);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(40, 15);
            label2.TabIndex = 115;
            label2.Text = "Active";
            // 
            // ExpansionQuestQuestBasicInfoControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(QuestObjectivesBaseInfoGB);
            ForeColor = SystemColors.Control;
            Name = "ExpansionQuestQuestBasicInfoControl";
            Size = new Size(418, 172);
            QuestObjectivesBaseInfoGB.ResumeLayout(false);
            QuestObjectivesBaseInfoGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)QuestConfigVersionNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)QuestsIDNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox QuestObjectivesBaseInfoGB;
        private Label label2;
        private CheckBox QuestActiveCB;
        private Label darkLabel44;
        private TextBox QuestFilenameTB;
        private NumericUpDown QuestConfigVersionNUD;
        private Label darkLabel45;
        private Label darkLabel46;
        private ComboBox QuestTypeCB;
        private Label darkLabel47;
        private NumericUpDown QuestsIDNUD;
    }
}
