namespace ExpansionPlugin
{
    partial class ExpansionQuestQuestFactionRepsControl
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
            FactionCB = new ComboBox();
            ReputationNUD = new NumericUpDown();
            darkLabel38 = new Label();
            darkLabel39 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ReputationNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(FactionCB);
            groupBox1.Controls.Add(ReputationNUD);
            groupBox1.Controls.Add(darkLabel38);
            groupBox1.Controls.Add(darkLabel39);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(514, 85);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Faction Reps";
            // 
            // FactionCB
            // 
            FactionCB.BackColor = Color.FromArgb(60, 63, 65);
            FactionCB.ForeColor = SystemColors.Control;
            FactionCB.FormattingEnabled = true;
            FactionCB.Location = new Point(216, 22);
            FactionCB.Margin = new Padding(4, 3, 4, 3);
            FactionCB.Name = "FactionCB";
            FactionCB.Size = new Size(289, 23);
            FactionCB.TabIndex = 184;
            FactionCB.SelectedIndexChanged += FactionCB_SelectedIndexChanged;
            // 
            // ReputationNUD
            // 
            ReputationNUD.BackColor = Color.FromArgb(60, 63, 65);
            ReputationNUD.ForeColor = SystemColors.Control;
            ReputationNUD.Location = new Point(216, 53);
            ReputationNUD.Margin = new Padding(4, 3, 4, 3);
            ReputationNUD.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            ReputationNUD.Name = "ReputationNUD";
            ReputationNUD.Size = new Size(140, 23);
            ReputationNUD.TabIndex = 183;
            ReputationNUD.TextAlign = HorizontalAlignment.Center;
            ReputationNUD.ValueChanged += ReputationNUD_ValueChanged;
            // 
            // darkLabel38
            // 
            darkLabel38.AutoSize = true;
            darkLabel38.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel38.Location = new Point(15, 25);
            darkLabel38.Margin = new Padding(4, 0, 4, 0);
            darkLabel38.Name = "darkLabel38";
            darkLabel38.Size = new Size(46, 15);
            darkLabel38.TabIndex = 174;
            darkLabel38.Text = "Faction";
            // 
            // darkLabel39
            // 
            darkLabel39.AutoSize = true;
            darkLabel39.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel39.Location = new Point(15, 55);
            darkLabel39.Margin = new Padding(4, 0, 4, 0);
            darkLabel39.Name = "darkLabel39";
            darkLabel39.Size = new Size(65, 15);
            darkLabel39.TabIndex = 175;
            darkLabel39.Text = "Reputation";
            // 
            // ExpansionQuestQuestFactionRepsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionQuestQuestFactionRepsControl";
            Size = new Size(514, 85);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ReputationNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private ComboBox FactionCB;
        private NumericUpDown ReputationNUD;
        private Label darkLabel38;
        private Label darkLabel39;
    }
}
