namespace ExpansionPlugin
{
    partial class ExpansionQuestObjectiveCollectionConfigControl
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
            checkBox5 = new CheckBox();
            checkBox3 = new CheckBox();
            ObjectivesCollectionShowDistanceCB = new CheckBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(checkBox5);
            groupBox1.Controls.Add(checkBox3);
            groupBox1.Controls.Add(ObjectivesCollectionShowDistanceCB);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(277, 121);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Collection";
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.ForeColor = SystemColors.Control;
            checkBox5.Location = new Point(240, 86);
            checkBox5.Margin = new Padding(4, 3, 4, 3);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new Size(15, 14);
            checkBox5.TabIndex = 359;
            checkBox5.UseVisualStyleBackColor = true;
            checkBox5.CheckedChanged += checkBox5_CheckedChanged;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.ForeColor = SystemColors.Control;
            checkBox3.Location = new Point(240, 56);
            checkBox3.Margin = new Padding(4, 3, 4, 3);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(15, 14);
            checkBox3.TabIndex = 358;
            checkBox3.UseVisualStyleBackColor = true;
            checkBox3.CheckedChanged += checkBox3_CheckedChanged;
            // 
            // ObjectivesCollectionShowDistanceCB
            // 
            ObjectivesCollectionShowDistanceCB.AutoSize = true;
            ObjectivesCollectionShowDistanceCB.ForeColor = SystemColors.Control;
            ObjectivesCollectionShowDistanceCB.Location = new Point(240, 26);
            ObjectivesCollectionShowDistanceCB.Margin = new Padding(4, 3, 4, 3);
            ObjectivesCollectionShowDistanceCB.Name = "ObjectivesCollectionShowDistanceCB";
            ObjectivesCollectionShowDistanceCB.Size = new Size(15, 14);
            ObjectivesCollectionShowDistanceCB.TabIndex = 357;
            ObjectivesCollectionShowDistanceCB.UseVisualStyleBackColor = true;
            ObjectivesCollectionShowDistanceCB.CheckedChanged += ObjectivesCollectionShowDistanceCB_CheckedChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.FromArgb(220, 220, 220);
            label3.Location = new Point(15, 85);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(116, 15);
            label3.TabIndex = 356;
            label3.Text = "Need Any Collection";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.FromArgb(220, 220, 220);
            label2.Location = new Point(15, 55);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(191, 15);
            label2.TabIndex = 354;
            label2.Text = "Add Items To Near by Market Zone";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(220, 220, 220);
            label1.Location = new Point(15, 25);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(84, 15);
            label1.TabIndex = 352;
            label1.Text = "Show Distance";
            // 
            // ExpansionQuestObjectiveCollectionConfigControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionQuestObjectiveCollectionConfigControl";
            Size = new Size(277, 121);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label3;
        private Label label2;
        private Label label1;
        private CheckBox checkBox5;
        private CheckBox checkBox3;
        private CheckBox ObjectivesCollectionShowDistanceCB;
    }
}
