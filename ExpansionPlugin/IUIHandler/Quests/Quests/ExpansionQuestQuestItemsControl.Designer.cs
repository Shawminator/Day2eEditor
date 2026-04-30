namespace ExpansionPlugin
{
    partial class ExpansionQuestQuestItemsControl
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
            darkLabel148 = new Label();
            QuestDeleteQuestItemsCB = new CheckBox();
            darkLabel150 = new Label();
            QuestPlayerNeedQuestItemsCB = new CheckBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(QuestPlayerNeedQuestItemsCB);
            groupBox1.Controls.Add(QuestDeleteQuestItemsCB);
            groupBox1.Controls.Add(darkLabel148);
            groupBox1.Controls.Add(darkLabel150);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(216, 85);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Items";
            // 
            // darkLabel148
            // 
            darkLabel148.AutoSize = true;
            darkLabel148.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel148.Location = new Point(15, 25);
            darkLabel148.Margin = new Padding(4, 0, 4, 0);
            darkLabel148.Name = "darkLabel148";
            darkLabel148.Size = new Size(136, 15);
            darkLabel148.TabIndex = 174;
            darkLabel148.Text = "Player Need Quest Items";
            // 
            // QuestDeleteQuestItemsCB
            // 
            QuestDeleteQuestItemsCB.AutoSize = true;
            QuestDeleteQuestItemsCB.ForeColor = SystemColors.Control;
            QuestDeleteQuestItemsCB.Location = new Point(186, 56);
            QuestDeleteQuestItemsCB.Margin = new Padding(4, 3, 4, 3);
            QuestDeleteQuestItemsCB.Name = "QuestDeleteQuestItemsCB";
            QuestDeleteQuestItemsCB.Size = new Size(15, 14);
            QuestDeleteQuestItemsCB.TabIndex = 172;
            QuestDeleteQuestItemsCB.UseVisualStyleBackColor = true;
            QuestDeleteQuestItemsCB.CheckedChanged += QuestDeleteQuestItemsCB_CheckedChanged;
            // 
            // darkLabel150
            // 
            darkLabel150.AutoSize = true;
            darkLabel150.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel150.Location = new Point(15, 55);
            darkLabel150.Margin = new Padding(4, 0, 4, 0);
            darkLabel150.Name = "darkLabel150";
            darkLabel150.Size = new Size(106, 15);
            darkLabel150.TabIndex = 175;
            darkLabel150.Text = "Delete Quest Items";
            // 
            // QuestPlayerNeedQuestItemsCB
            // 
            QuestPlayerNeedQuestItemsCB.AutoSize = true;
            QuestPlayerNeedQuestItemsCB.ForeColor = SystemColors.Control;
            QuestPlayerNeedQuestItemsCB.Location = new Point(186, 26);
            QuestPlayerNeedQuestItemsCB.Margin = new Padding(4, 3, 4, 3);
            QuestPlayerNeedQuestItemsCB.Name = "QuestPlayerNeedQuestItemsCB";
            QuestPlayerNeedQuestItemsCB.Size = new Size(15, 14);
            QuestPlayerNeedQuestItemsCB.TabIndex = 173;
            QuestPlayerNeedQuestItemsCB.UseVisualStyleBackColor = true;
            QuestPlayerNeedQuestItemsCB.CheckedChanged += QuestPlayerNeedQuestItemsCB_CheckedChanged;
            // 
            // ExpansionQuestQuestItemsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionQuestQuestItemsControl";
            Size = new Size(216, 85);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private CheckBox QuestPlayerNeedQuestItemsCB;
        private CheckBox QuestDeleteQuestItemsCB;
        private Label darkLabel148;
        private Label darkLabel150;
    }
}
