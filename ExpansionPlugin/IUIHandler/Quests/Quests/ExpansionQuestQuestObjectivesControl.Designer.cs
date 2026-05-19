namespace ExpansionPlugin
{
    partial class ExpansionQuestQuestObjectivesControl
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
            ObjIdCB = new ComboBox();
            label1 = new Label();
            ObjTypeCB = new ComboBox();
            darkLabel38 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ObjIdCB);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(ObjTypeCB);
            groupBox1.Controls.Add(darkLabel38);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(625, 87);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Objectives";
            // 
            // ObjIdCB
            // 
            ObjIdCB.BackColor = Color.FromArgb(60, 63, 65);
            ObjIdCB.ForeColor = SystemColors.Control;
            ObjIdCB.FormattingEnabled = true;
            ObjIdCB.Location = new Point(95, 52);
            ObjIdCB.Margin = new Padding(4, 3, 4, 3);
            ObjIdCB.Name = "ObjIdCB";
            ObjIdCB.Size = new Size(523, 23);
            ObjIdCB.TabIndex = 186;
            ObjIdCB.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(220, 220, 220);
            label1.Location = new Point(15, 55);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(32, 15);
            label1.TabIndex = 185;
            label1.Text = "Type";
            // 
            // ObjTypeCB
            // 
            ObjTypeCB.BackColor = Color.FromArgb(60, 63, 65);
            ObjTypeCB.ForeColor = SystemColors.Control;
            ObjTypeCB.FormattingEnabled = true;
            ObjTypeCB.Location = new Point(95, 22);
            ObjTypeCB.Margin = new Padding(4, 3, 4, 3);
            ObjTypeCB.Name = "ObjTypeCB";
            ObjTypeCB.Size = new Size(523, 23);
            ObjTypeCB.TabIndex = 184;
            ObjTypeCB.SelectedIndexChanged += QuestsCB_SelectedIndexChanged;
            // 
            // darkLabel38
            // 
            darkLabel38.AutoSize = true;
            darkLabel38.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel38.Location = new Point(15, 25);
            darkLabel38.Margin = new Padding(4, 0, 4, 0);
            darkLabel38.Name = "darkLabel38";
            darkLabel38.Size = new Size(32, 15);
            darkLabel38.TabIndex = 174;
            darkLabel38.Text = "Type";
            // 
            // ExpansionQuestQuestObjectivesControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionQuestQuestObjectivesControl";
            Size = new Size(647, 559);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private ComboBox ObjIdCB;
        private Label label1;
        private ComboBox ObjTypeCB;
        private Label darkLabel38;
    }
}
