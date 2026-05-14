namespace ExpansionPlugin
{
    partial class ExpansionQuestSettingsIndicatorsControl
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
            groupBox65 = new GroupBox();
            label2 = new Label();
            label1 = new Label();
            UseQuestNPCIndicatorsCB = new CheckBox();
            CreateQuestNPCMarkersCB = new CheckBox();
            groupBox65.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox65
            // 
            groupBox65.Controls.Add(label2);
            groupBox65.Controls.Add(label1);
            groupBox65.Controls.Add(UseQuestNPCIndicatorsCB);
            groupBox65.Controls.Add(CreateQuestNPCMarkersCB);
            groupBox65.ForeColor = SystemColors.Control;
            groupBox65.Location = new Point(0, 0);
            groupBox65.Margin = new Padding(4, 3, 4, 3);
            groupBox65.Name = "groupBox65";
            groupBox65.Padding = new Padding(4, 3, 4, 3);
            groupBox65.Size = new Size(213, 86);
            groupBox65.TabIndex = 18;
            groupBox65.TabStop = false;
            groupBox65.Text = "UI & Markers";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.FromArgb(220, 220, 220);
            label2.Location = new Point(16, 25);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(147, 15);
            label2.TabIndex = 13;
            label2.Text = "Create Quest NPC Markers";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(220, 220, 220);
            label1.Location = new Point(16, 55);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(142, 15);
            label1.TabIndex = 12;
            label1.Text = "Use Quest NPC Indicators";
            // 
            // UseQuestNPCIndicatorsCB
            // 
            UseQuestNPCIndicatorsCB.AutoSize = true;
            UseQuestNPCIndicatorsCB.ForeColor = SystemColors.Control;
            UseQuestNPCIndicatorsCB.Location = new Point(184, 56);
            UseQuestNPCIndicatorsCB.Margin = new Padding(4, 3, 4, 3);
            UseQuestNPCIndicatorsCB.Name = "UseQuestNPCIndicatorsCB";
            UseQuestNPCIndicatorsCB.Size = new Size(15, 14);
            UseQuestNPCIndicatorsCB.TabIndex = 1;
            UseQuestNPCIndicatorsCB.TextAlign = ContentAlignment.MiddleRight;
            UseQuestNPCIndicatorsCB.UseVisualStyleBackColor = true;
            UseQuestNPCIndicatorsCB.CheckedChanged += UseQuestNPCIndicatorsCB_CheckedChanged;
            // 
            // CreateQuestNPCMarkersCB
            // 
            CreateQuestNPCMarkersCB.AutoSize = true;
            CreateQuestNPCMarkersCB.ForeColor = SystemColors.Control;
            CreateQuestNPCMarkersCB.Location = new Point(184, 26);
            CreateQuestNPCMarkersCB.Margin = new Padding(4, 3, 4, 3);
            CreateQuestNPCMarkersCB.Name = "CreateQuestNPCMarkersCB";
            CreateQuestNPCMarkersCB.Size = new Size(15, 14);
            CreateQuestNPCMarkersCB.TabIndex = 0;
            CreateQuestNPCMarkersCB.TextAlign = ContentAlignment.MiddleRight;
            CreateQuestNPCMarkersCB.UseVisualStyleBackColor = true;
            CreateQuestNPCMarkersCB.CheckedChanged += CreateQuestNPCMarkersCB_CheckedChanged;
            // 
            // ExpansionQuestSettingsIndicatorsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox65);
            ForeColor = SystemColors.Control;
            Name = "ExpansionQuestSettingsIndicatorsControl";
            Size = new Size(213, 86);
            groupBox65.ResumeLayout(false);
            groupBox65.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox65;
        private Label label2;
        private Label label1;
        private CheckBox UseQuestNPCIndicatorsCB;
        private Label darkLabel170;
        private CheckBox CreateQuestNPCMarkersCB;
    }
}
