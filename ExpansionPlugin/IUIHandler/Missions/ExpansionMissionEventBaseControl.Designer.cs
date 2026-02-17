namespace ExpansionPlugin
{
    partial class ExpansionMissionEventBaseControl
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
            ContaminatedAreaGB = new GroupBox();
            RewardTB = new TextBox();
            ObjectiveNUD = new NumericUpDown();
            DifficultyNUD = new NumericUpDown();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            darkLabel181 = new Label();
            darkLabel182 = new Label();
            darkLabel178 = new Label();
            MissionNameTB = new TextBox();
            EnabledCB = new CheckBox();
            WeightNUD = new NumericUpDown();
            MissionMaxTimeNUD = new NumericUpDown();
            ContaminatedAreaGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ObjectiveNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DifficultyNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)WeightNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MissionMaxTimeNUD).BeginInit();
            SuspendLayout();
            // 
            // ContaminatedAreaGB
            // 
            ContaminatedAreaGB.Controls.Add(RewardTB);
            ContaminatedAreaGB.Controls.Add(ObjectiveNUD);
            ContaminatedAreaGB.Controls.Add(DifficultyNUD);
            ContaminatedAreaGB.Controls.Add(label4);
            ContaminatedAreaGB.Controls.Add(label3);
            ContaminatedAreaGB.Controls.Add(label2);
            ContaminatedAreaGB.Controls.Add(label1);
            ContaminatedAreaGB.Controls.Add(darkLabel181);
            ContaminatedAreaGB.Controls.Add(darkLabel182);
            ContaminatedAreaGB.Controls.Add(darkLabel178);
            ContaminatedAreaGB.Controls.Add(MissionNameTB);
            ContaminatedAreaGB.Controls.Add(EnabledCB);
            ContaminatedAreaGB.Controls.Add(WeightNUD);
            ContaminatedAreaGB.Controls.Add(MissionMaxTimeNUD);
            ContaminatedAreaGB.ForeColor = SystemColors.Control;
            ContaminatedAreaGB.Location = new Point(0, 0);
            ContaminatedAreaGB.Margin = new Padding(4, 3, 4, 3);
            ContaminatedAreaGB.Name = "ContaminatedAreaGB";
            ContaminatedAreaGB.Padding = new Padding(4, 3, 4, 3);
            ContaminatedAreaGB.Size = new Size(612, 240);
            ContaminatedAreaGB.TabIndex = 4;
            ContaminatedAreaGB.TabStop = false;
            ContaminatedAreaGB.Text = "Mission General";
            // 
            // RewardTB
            // 
            RewardTB.BackColor = Color.FromArgb(60, 63, 65);
            RewardTB.Enabled = false;
            RewardTB.ForeColor = SystemColors.Control;
            RewardTB.Location = new Point(184, 202);
            RewardTB.Margin = new Padding(4, 3, 4, 3);
            RewardTB.Name = "RewardTB";
            RewardTB.Size = new Size(257, 23);
            RewardTB.TabIndex = 15;
            // 
            // ObjectiveNUD
            // 
            ObjectiveNUD.BackColor = Color.FromArgb(60, 63, 65);
            ObjectiveNUD.Enabled = false;
            ObjectiveNUD.ForeColor = SystemColors.Control;
            ObjectiveNUD.Location = new Point(184, 172);
            ObjectiveNUD.Margin = new Padding(4, 3, 4, 3);
            ObjectiveNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            ObjectiveNUD.Name = "ObjectiveNUD";
            ObjectiveNUD.Size = new Size(151, 23);
            ObjectiveNUD.TabIndex = 14;
            ObjectiveNUD.Tag = "MissionMaxTime";
            ObjectiveNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // DifficultyNUD
            // 
            DifficultyNUD.BackColor = Color.FromArgb(60, 63, 65);
            DifficultyNUD.Enabled = false;
            DifficultyNUD.ForeColor = SystemColors.Control;
            DifficultyNUD.Location = new Point(184, 143);
            DifficultyNUD.Margin = new Padding(4, 3, 4, 3);
            DifficultyNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            DifficultyNUD.Name = "DifficultyNUD";
            DifficultyNUD.Size = new Size(151, 23);
            DifficultyNUD.TabIndex = 13;
            DifficultyNUD.Tag = "MissionMaxTime";
            DifficultyNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.FromArgb(220, 220, 220);
            label4.Location = new Point(15, 145);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(55, 15);
            label4.TabIndex = 12;
            label4.Text = "Difficulty";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.FromArgb(220, 220, 220);
            label3.Location = new Point(15, 175);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 11;
            label3.Text = "Objective";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.FromArgb(220, 220, 220);
            label2.Location = new Point(15, 205);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(46, 15);
            label2.TabIndex = 10;
            label2.Text = "Reward";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(220, 220, 220);
            label1.Location = new Point(15, 115);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(83, 15);
            label1.TabIndex = 9;
            label1.Text = "Mission Name";
            // 
            // darkLabel181
            // 
            darkLabel181.AutoSize = true;
            darkLabel181.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel181.Location = new Point(15, 55);
            darkLabel181.Margin = new Padding(4, 0, 4, 0);
            darkLabel181.Name = "darkLabel181";
            darkLabel181.Size = new Size(45, 15);
            darkLabel181.TabIndex = 7;
            darkLabel181.Tag = "";
            darkLabel181.Text = "Weight";
            // 
            // darkLabel182
            // 
            darkLabel182.AutoSize = true;
            darkLabel182.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel182.Location = new Point(15, 85);
            darkLabel182.Margin = new Padding(4, 0, 4, 0);
            darkLabel182.Name = "darkLabel182";
            darkLabel182.Size = new Size(149, 15);
            darkLabel182.TabIndex = 5;
            darkLabel182.Text = "Mission Max Time (  Mins )";
            // 
            // darkLabel178
            // 
            darkLabel178.AutoSize = true;
            darkLabel178.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel178.Location = new Point(15, 25);
            darkLabel178.Margin = new Padding(4, 0, 4, 0);
            darkLabel178.Name = "darkLabel178";
            darkLabel178.Size = new Size(60, 15);
            darkLabel178.TabIndex = 2;
            darkLabel178.Text = "Is Enabled";
            // 
            // MissionNameTB
            // 
            MissionNameTB.BackColor = Color.FromArgb(60, 63, 65);
            MissionNameTB.ForeColor = SystemColors.Control;
            MissionNameTB.Location = new Point(184, 112);
            MissionNameTB.Margin = new Padding(4, 3, 4, 3);
            MissionNameTB.Name = "MissionNameTB";
            MissionNameTB.Size = new Size(416, 23);
            MissionNameTB.TabIndex = 3;
            // 
            // EnabledCB
            // 
            EnabledCB.AutoSize = true;
            EnabledCB.ForeColor = SystemColors.Control;
            EnabledCB.Location = new Point(185, 24);
            EnabledCB.Margin = new Padding(4, 3, 4, 3);
            EnabledCB.Name = "EnabledCB";
            EnabledCB.Size = new Size(15, 14);
            EnabledCB.TabIndex = 4;
            EnabledCB.Tag = "Enabled";
            EnabledCB.TextAlign = ContentAlignment.MiddleRight;
            EnabledCB.UseVisualStyleBackColor = true;
            // 
            // WeightNUD
            // 
            WeightNUD.BackColor = Color.FromArgb(60, 63, 65);
            WeightNUD.DecimalPlaces = 1;
            WeightNUD.ForeColor = SystemColors.Control;
            WeightNUD.Location = new Point(184, 47);
            WeightNUD.Margin = new Padding(4, 3, 4, 3);
            WeightNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            WeightNUD.Name = "WeightNUD";
            WeightNUD.Size = new Size(118, 23);
            WeightNUD.TabIndex = 7;
            WeightNUD.Tag = "Weight";
            WeightNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // MissionMaxTimeNUD
            // 
            MissionMaxTimeNUD.BackColor = Color.FromArgb(60, 63, 65);
            MissionMaxTimeNUD.ForeColor = SystemColors.Control;
            MissionMaxTimeNUD.Location = new Point(184, 83);
            MissionMaxTimeNUD.Margin = new Padding(4, 3, 4, 3);
            MissionMaxTimeNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            MissionMaxTimeNUD.Name = "MissionMaxTimeNUD";
            MissionMaxTimeNUD.Size = new Size(118, 23);
            MissionMaxTimeNUD.TabIndex = 9;
            MissionMaxTimeNUD.Tag = "MissionMaxTime";
            MissionMaxTimeNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // ExpansionMissionEventBaseControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(ContaminatedAreaGB);
            ForeColor = SystemColors.Control;
            Name = "ExpansionMissionEventBaseControl";
            Size = new Size(612, 240);
            ContaminatedAreaGB.ResumeLayout(false);
            ContaminatedAreaGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ObjectiveNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)DifficultyNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)WeightNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)MissionMaxTimeNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox ContaminatedAreaGB;
        private TextBox RewardTB;
        private NumericUpDown ObjectiveNUD;
        private NumericUpDown DifficultyNUD;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label darkLabel181;
        private Label darkLabel182;
        private Label darkLabel178;
        private TextBox MissionNameTB;
        private CheckBox EnabledCB;
        private NumericUpDown WeightNUD;
        private NumericUpDown MissionMaxTimeNUD;
    }
}
