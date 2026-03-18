namespace ExpansionPlugin
{
    partial class ExpansionPersonalStorageLevelControl
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
            QuestIDNUD = new NumericUpDown();
            label1 = new Label();
            ReputationRequirementNUD = new NumericUpDown();
            AllowAttachmentCargoCB = new CheckBox();
            label3 = new Label();
            label2 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)QuestIDNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ReputationRequirementNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(QuestIDNUD);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(ReputationRequirementNUD);
            groupBox1.Controls.Add(AllowAttachmentCargoCB);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(374, 124);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Storage Level Info";
            // 
            // QuestIDNUD
            // 
            QuestIDNUD.BackColor = Color.FromArgb(60, 63, 65);
            QuestIDNUD.ForeColor = SystemColors.Control;
            QuestIDNUD.Location = new Point(205, 53);
            QuestIDNUD.Margin = new Padding(4, 3, 4, 3);
            QuestIDNUD.Maximum = new decimal(new int[] { 1874919423, 2328306, 0, 0 });
            QuestIDNUD.Name = "QuestIDNUD";
            QuestIDNUD.Size = new Size(151, 23);
            QuestIDNUD.TabIndex = 59;
            QuestIDNUD.TextAlign = HorizontalAlignment.Center;
            QuestIDNUD.ValueChanged += QuestIDNUD_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(14, 85);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(138, 15);
            label1.TabIndex = 58;
            label1.Text = "Allow Attachment Cargo";
            // 
            // ReputationRequirementNUD
            // 
            ReputationRequirementNUD.BackColor = Color.FromArgb(60, 63, 65);
            ReputationRequirementNUD.ForeColor = SystemColors.Control;
            ReputationRequirementNUD.Location = new Point(205, 23);
            ReputationRequirementNUD.Margin = new Padding(4, 3, 4, 3);
            ReputationRequirementNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            ReputationRequirementNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            ReputationRequirementNUD.Name = "ReputationRequirementNUD";
            ReputationRequirementNUD.Size = new Size(151, 23);
            ReputationRequirementNUD.TabIndex = 57;
            ReputationRequirementNUD.TextAlign = HorizontalAlignment.Center;
            ReputationRequirementNUD.ValueChanged += ReputationRequirementNUD_ValueChanged;
            // 
            // AllowAttachmentCargoCB
            // 
            AllowAttachmentCargoCB.AutoSize = true;
            AllowAttachmentCargoCB.ForeColor = SystemColors.Control;
            AllowAttachmentCargoCB.Location = new Point(205, 86);
            AllowAttachmentCargoCB.Margin = new Padding(4, 3, 4, 3);
            AllowAttachmentCargoCB.Name = "AllowAttachmentCargoCB";
            AllowAttachmentCargoCB.Size = new Size(15, 14);
            AllowAttachmentCargoCB.TabIndex = 56;
            AllowAttachmentCargoCB.TextAlign = ContentAlignment.MiddleRight;
            AllowAttachmentCargoCB.UseVisualStyleBackColor = true;
            AllowAttachmentCargoCB.CheckedChanged += AllowAttachmentCargoCB_CheckedChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.Control;
            label3.Location = new Point(14, 55);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(52, 15);
            label3.TabIndex = 55;
            label3.Text = "Quest ID";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.Control;
            label2.Location = new Point(14, 25);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(136, 15);
            label2.TabIndex = 54;
            label2.Text = "Reputation Requirement";
            // 
            // ExpansionPersonalStorageLevelControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionPersonalStorageLevelControl";
            Size = new Size(374, 124);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)QuestIDNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)ReputationRequirementNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private NumericUpDown QuestIDNUD;
        private Label label1;
        private NumericUpDown ReputationRequirementNUD;
        private CheckBox AllowAttachmentCargoCB;
        private Label label3;
        private Label label2;
    }
}
