namespace ExpansionPlugin
{
    partial class ExpansionQuestSettingsGeneralControl
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
            EnableQuestsCB = new CheckBox();
            darkLabel170 = new Label();
            darkLabel168 = new Label();
            EnableQuestLogTabCB = new CheckBox();
            label1 = new Label();
            label2 = new Label();
            groupBox65 = new GroupBox();
            GroupQuestModeCB = new ComboBox();
            MaxActiveQuestsNUD = new NumericUpDown();
            groupBox65.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MaxActiveQuestsNUD).BeginInit();
            SuspendLayout();
            // 
            // EnableQuestsCB
            // 
            EnableQuestsCB.AutoSize = true;
            EnableQuestsCB.ForeColor = SystemColors.Control;
            EnableQuestsCB.Location = new Point(183, 26);
            EnableQuestsCB.Margin = new Padding(4, 3, 4, 3);
            EnableQuestsCB.Name = "EnableQuestsCB";
            EnableQuestsCB.Size = new Size(15, 14);
            EnableQuestsCB.TabIndex = 0;
            EnableQuestsCB.TextAlign = ContentAlignment.MiddleRight;
            EnableQuestsCB.UseVisualStyleBackColor = true;
            EnableQuestsCB.CheckedChanged += EnableQuestsCB_CheckedChanged;
            // 
            // darkLabel170
            // 
            darkLabel170.AutoSize = true;
            darkLabel170.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel170.Location = new Point(15, 85);
            darkLabel170.Margin = new Padding(4, 0, 4, 0);
            darkLabel170.Name = "darkLabel170";
            darkLabel170.Size = new Size(104, 15);
            darkLabel170.TabIndex = 6;
            darkLabel170.Text = "Max Active Quests";
            // 
            // darkLabel168
            // 
            darkLabel168.AutoSize = true;
            darkLabel168.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel168.Location = new Point(15, 115);
            darkLabel168.Margin = new Padding(4, 0, 4, 0);
            darkLabel168.Name = "darkLabel168";
            darkLabel168.Size = new Size(108, 15);
            darkLabel168.TabIndex = 8;
            darkLabel168.Text = "Group Quest Mode";
            // 
            // EnableQuestLogTabCB
            // 
            EnableQuestLogTabCB.AutoSize = true;
            EnableQuestLogTabCB.ForeColor = SystemColors.Control;
            EnableQuestLogTabCB.Location = new Point(183, 56);
            EnableQuestLogTabCB.Margin = new Padding(4, 3, 4, 3);
            EnableQuestLogTabCB.Name = "EnableQuestLogTabCB";
            EnableQuestLogTabCB.Size = new Size(15, 14);
            EnableQuestLogTabCB.TabIndex = 1;
            EnableQuestLogTabCB.TextAlign = ContentAlignment.MiddleRight;
            EnableQuestLogTabCB.UseVisualStyleBackColor = true;
            EnableQuestLogTabCB.CheckedChanged += EnableQuestLogTabCB_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(220, 220, 220);
            label1.Location = new Point(15, 55);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(121, 15);
            label1.TabIndex = 12;
            label1.Text = "Enable Quest Log Tab";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.FromArgb(220, 220, 220);
            label2.Location = new Point(15, 25);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(81, 15);
            label2.TabIndex = 13;
            label2.Text = "Enable Quests";
            // 
            // groupBox65
            // 
            groupBox65.Controls.Add(GroupQuestModeCB);
            groupBox65.Controls.Add(MaxActiveQuestsNUD);
            groupBox65.Controls.Add(label2);
            groupBox65.Controls.Add(label1);
            groupBox65.Controls.Add(EnableQuestLogTabCB);
            groupBox65.Controls.Add(darkLabel168);
            groupBox65.Controls.Add(darkLabel170);
            groupBox65.Controls.Add(EnableQuestsCB);
            groupBox65.ForeColor = SystemColors.Control;
            groupBox65.Location = new Point(0, 0);
            groupBox65.Margin = new Padding(4, 3, 4, 3);
            groupBox65.Name = "groupBox65";
            groupBox65.Padding = new Padding(4, 3, 4, 3);
            groupBox65.Size = new Size(643, 142);
            groupBox65.TabIndex = 17;
            groupBox65.TabStop = false;
            groupBox65.Text = "General";
            // 
            // GroupQuestModeCB
            // 
            GroupQuestModeCB.FormattingEnabled = true;
            GroupQuestModeCB.Items.AddRange(new object[] { "Only group owners can accept and turn-in quests. ", "Only group owner can turn-in quest but all group members can accept them. ", "All group members can accept and turn-in quests." });
            GroupQuestModeCB.Location = new Point(183, 112);
            GroupQuestModeCB.Margin = new Padding(4, 3, 4, 3);
            GroupQuestModeCB.Name = "GroupQuestModeCB";
            GroupQuestModeCB.Size = new Size(452, 23);
            GroupQuestModeCB.TabIndex = 15;
            GroupQuestModeCB.SelectedIndexChanged += GroupQuestModeCB_SelectedIndexChanged;
            // 
            // MaxActiveQuestsNUD
            // 
            MaxActiveQuestsNUD.BackColor = Color.FromArgb(60, 63, 65);
            MaxActiveQuestsNUD.ForeColor = SystemColors.Control;
            MaxActiveQuestsNUD.Location = new Point(183, 83);
            MaxActiveQuestsNUD.Margin = new Padding(4, 3, 4, 3);
            MaxActiveQuestsNUD.Maximum = new decimal(new int[] { 1410065407, 2, 0, 0 });
            MaxActiveQuestsNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            MaxActiveQuestsNUD.Name = "MaxActiveQuestsNUD";
            MaxActiveQuestsNUD.Size = new Size(121, 23);
            MaxActiveQuestsNUD.TabIndex = 14;
            MaxActiveQuestsNUD.TextAlign = HorizontalAlignment.Center;
            MaxActiveQuestsNUD.ValueChanged += MaxActiveQuestsNUD_ValueChanged;
            // 
            // ExpansionQuestSettingsGeneralControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox65);
            ForeColor = SystemColors.Control;
            Name = "ExpansionQuestSettingsGeneralControl";
            Size = new Size(643, 142);
            groupBox65.ResumeLayout(false);
            groupBox65.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)MaxActiveQuestsNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private CheckBox EnableQuestsCB;
        private Label darkLabel170;
        private Label darkLabel168;
        private CheckBox EnableQuestLogTabCB;
        private Label label1;
        private Label label2;
        private GroupBox groupBox65;
        private NumericUpDown MaxActiveQuestsNUD;
        private ComboBox GroupQuestModeCB;
    }
}
