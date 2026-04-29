namespace ExpansionPlugin
{
    partial class ExpansionQuestObjectiveTargetConfigControl
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
            ObjectivesTargetGB = new GroupBox();
            label2 = new Label();
            label1 = new Label();
            darkLabel189 = new Label();
            ObjectivesTargetMinDistanceNUD = new NumericUpDown();
            checkBox7 = new CheckBox();
            ObjectivesTargetCountSelfKillCB = new CheckBox();
            darkLabel123 = new Label();
            ObjectivesTargetAmountNUD = new NumericUpDown();
            darkLabel122 = new Label();
            ObjectivesTargetMaxDistanceNUD = new NumericUpDown();
            ObjectivesTargetGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ObjectivesTargetMinDistanceNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ObjectivesTargetAmountNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ObjectivesTargetMaxDistanceNUD).BeginInit();
            SuspendLayout();
            // 
            // ObjectivesTargetGB
            // 
            ObjectivesTargetGB.Controls.Add(label2);
            ObjectivesTargetGB.Controls.Add(label1);
            ObjectivesTargetGB.Controls.Add(darkLabel189);
            ObjectivesTargetGB.Controls.Add(ObjectivesTargetMinDistanceNUD);
            ObjectivesTargetGB.Controls.Add(checkBox7);
            ObjectivesTargetGB.Controls.Add(ObjectivesTargetCountSelfKillCB);
            ObjectivesTargetGB.Controls.Add(darkLabel123);
            ObjectivesTargetGB.Controls.Add(ObjectivesTargetAmountNUD);
            ObjectivesTargetGB.Controls.Add(darkLabel122);
            ObjectivesTargetGB.Controls.Add(ObjectivesTargetMaxDistanceNUD);
            ObjectivesTargetGB.ForeColor = SystemColors.Control;
            ObjectivesTargetGB.Location = new Point(0, 0);
            ObjectivesTargetGB.Margin = new Padding(4, 3, 4, 3);
            ObjectivesTargetGB.Name = "ObjectivesTargetGB";
            ObjectivesTargetGB.Padding = new Padding(4, 3, 4, 3);
            ObjectivesTargetGB.Size = new Size(269, 174);
            ObjectivesTargetGB.TabIndex = 292;
            ObjectivesTargetGB.TabStop = false;
            ObjectivesTargetGB.Text = "Target";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.FromArgb(220, 220, 220);
            label2.Location = new Point(15, 115);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(81, 15);
            label2.TabIndex = 365;
            label2.Text = "Count Self Kill";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(220, 220, 220);
            label1.Location = new Point(15, 145);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(94, 15);
            label1.TabIndex = 364;
            label1.Text = "Count AI Players";
            // 
            // darkLabel189
            // 
            darkLabel189.AutoSize = true;
            darkLabel189.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel189.Location = new Point(15, 85);
            darkLabel189.Margin = new Padding(4, 0, 4, 0);
            darkLabel189.Name = "darkLabel189";
            darkLabel189.Size = new Size(76, 15);
            darkLabel189.TabIndex = 363;
            darkLabel189.Text = "Min Distance";
            // 
            // ObjectivesTargetMinDistanceNUD
            // 
            ObjectivesTargetMinDistanceNUD.BackColor = Color.FromArgb(60, 63, 65);
            ObjectivesTargetMinDistanceNUD.DecimalPlaces = 2;
            ObjectivesTargetMinDistanceNUD.ForeColor = SystemColors.Control;
            ObjectivesTargetMinDistanceNUD.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            ObjectivesTargetMinDistanceNUD.Location = new Point(130, 83);
            ObjectivesTargetMinDistanceNUD.Margin = new Padding(4, 3, 4, 3);
            ObjectivesTargetMinDistanceNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            ObjectivesTargetMinDistanceNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            ObjectivesTargetMinDistanceNUD.Name = "ObjectivesTargetMinDistanceNUD";
            ObjectivesTargetMinDistanceNUD.Size = new Size(124, 23);
            ObjectivesTargetMinDistanceNUD.TabIndex = 362;
            ObjectivesTargetMinDistanceNUD.TextAlign = HorizontalAlignment.Center;
            ObjectivesTargetMinDistanceNUD.ValueChanged += ObjectivesTargetMinDistanceNUD_ValueChanged;
            // 
            // checkBox7
            // 
            checkBox7.AutoSize = true;
            checkBox7.ForeColor = SystemColors.Control;
            checkBox7.Location = new Point(130, 146);
            checkBox7.Margin = new Padding(4, 3, 4, 3);
            checkBox7.Name = "checkBox7";
            checkBox7.Size = new Size(15, 14);
            checkBox7.TabIndex = 356;
            checkBox7.UseVisualStyleBackColor = true;
            checkBox7.CheckedChanged += checkBox7_CheckedChanged;
            // 
            // ObjectivesTargetCountSelfKillCB
            // 
            ObjectivesTargetCountSelfKillCB.AutoSize = true;
            ObjectivesTargetCountSelfKillCB.ForeColor = SystemColors.Control;
            ObjectivesTargetCountSelfKillCB.Location = new Point(130, 116);
            ObjectivesTargetCountSelfKillCB.Margin = new Padding(4, 3, 4, 3);
            ObjectivesTargetCountSelfKillCB.Name = "ObjectivesTargetCountSelfKillCB";
            ObjectivesTargetCountSelfKillCB.Size = new Size(15, 14);
            ObjectivesTargetCountSelfKillCB.TabIndex = 355;
            ObjectivesTargetCountSelfKillCB.UseVisualStyleBackColor = true;
            ObjectivesTargetCountSelfKillCB.CheckedChanged += ObjectivesTargetCountSelfKillCB_CheckedChanged;
            // 
            // darkLabel123
            // 
            darkLabel123.AutoSize = true;
            darkLabel123.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel123.Location = new Point(15, 25);
            darkLabel123.Margin = new Padding(4, 0, 4, 0);
            darkLabel123.Name = "darkLabel123";
            darkLabel123.Size = new Size(51, 15);
            darkLabel123.TabIndex = 342;
            darkLabel123.Text = "Amount";
            // 
            // ObjectivesTargetAmountNUD
            // 
            ObjectivesTargetAmountNUD.BackColor = Color.FromArgb(60, 63, 65);
            ObjectivesTargetAmountNUD.ForeColor = SystemColors.Control;
            ObjectivesTargetAmountNUD.Location = new Point(130, 23);
            ObjectivesTargetAmountNUD.Margin = new Padding(4, 3, 4, 3);
            ObjectivesTargetAmountNUD.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            ObjectivesTargetAmountNUD.Name = "ObjectivesTargetAmountNUD";
            ObjectivesTargetAmountNUD.Size = new Size(124, 23);
            ObjectivesTargetAmountNUD.TabIndex = 341;
            ObjectivesTargetAmountNUD.TextAlign = HorizontalAlignment.Center;
            ObjectivesTargetAmountNUD.ValueChanged += ObjectivesTargetAmountNUD_ValueChanged;
            // 
            // darkLabel122
            // 
            darkLabel122.AutoSize = true;
            darkLabel122.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel122.Location = new Point(15, 55);
            darkLabel122.Margin = new Padding(4, 0, 4, 0);
            darkLabel122.Name = "darkLabel122";
            darkLabel122.Size = new Size(77, 15);
            darkLabel122.TabIndex = 340;
            darkLabel122.Text = "Max Distance";
            // 
            // ObjectivesTargetMaxDistanceNUD
            // 
            ObjectivesTargetMaxDistanceNUD.BackColor = Color.FromArgb(60, 63, 65);
            ObjectivesTargetMaxDistanceNUD.DecimalPlaces = 2;
            ObjectivesTargetMaxDistanceNUD.ForeColor = SystemColors.Control;
            ObjectivesTargetMaxDistanceNUD.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            ObjectivesTargetMaxDistanceNUD.Location = new Point(130, 53);
            ObjectivesTargetMaxDistanceNUD.Margin = new Padding(4, 3, 4, 3);
            ObjectivesTargetMaxDistanceNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            ObjectivesTargetMaxDistanceNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            ObjectivesTargetMaxDistanceNUD.Name = "ObjectivesTargetMaxDistanceNUD";
            ObjectivesTargetMaxDistanceNUD.Size = new Size(124, 23);
            ObjectivesTargetMaxDistanceNUD.TabIndex = 339;
            ObjectivesTargetMaxDistanceNUD.TextAlign = HorizontalAlignment.Center;
            ObjectivesTargetMaxDistanceNUD.ValueChanged += ObjectivesTargetMaxDistanceNUD_ValueChanged;
            // 
            // ExpansionQuestObjectiveTargetConfigControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(ObjectivesTargetGB);
            ForeColor = SystemColors.Control;
            Name = "ExpansionQuestObjectiveTargetConfigControl";
            Size = new Size(269, 174);
            ObjectivesTargetGB.ResumeLayout(false);
            ObjectivesTargetGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ObjectivesTargetMinDistanceNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)ObjectivesTargetAmountNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)ObjectivesTargetMaxDistanceNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox ObjectivesTargetGB;
        private Label darkLabel189;
        private NumericUpDown ObjectivesTargetMinDistanceNUD;
        private CheckBox checkBox7;
        private CheckBox ObjectivesTargetCountSelfKillCB;
        private Label darkLabel123;
        private NumericUpDown ObjectivesTargetAmountNUD;
        private Label darkLabel122;
        private NumericUpDown ObjectivesTargetMaxDistanceNUD;
        private Label label2;
        private Label label1;
    }
}
