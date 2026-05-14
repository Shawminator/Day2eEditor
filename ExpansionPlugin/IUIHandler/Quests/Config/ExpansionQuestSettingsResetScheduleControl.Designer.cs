namespace ExpansionPlugin
{
    partial class ExpansionQuestSettingsResetScheduleControl
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
            WeeklyResetDayCB = new ComboBox();
            WeeklyResetMinuteNUD = new NumericUpDown();
            label2 = new Label();
            label1 = new Label();
            darkLabel168 = new Label();
            darkLabel170 = new Label();
            UseUTCTimeCB = new CheckBox();
            WeeklyResetHourNUD = new NumericUpDown();
            DailyResetHourNUD = new NumericUpDown();
            DailyResetMinuteNUD = new NumericUpDown();
            label3 = new Label();
            label4 = new Label();
            groupBox65.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WeeklyResetMinuteNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)WeeklyResetHourNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DailyResetHourNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DailyResetMinuteNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox65
            // 
            groupBox65.Controls.Add(DailyResetHourNUD);
            groupBox65.Controls.Add(DailyResetMinuteNUD);
            groupBox65.Controls.Add(label3);
            groupBox65.Controls.Add(label4);
            groupBox65.Controls.Add(WeeklyResetHourNUD);
            groupBox65.Controls.Add(WeeklyResetDayCB);
            groupBox65.Controls.Add(WeeklyResetMinuteNUD);
            groupBox65.Controls.Add(label2);
            groupBox65.Controls.Add(label1);
            groupBox65.Controls.Add(darkLabel168);
            groupBox65.Controls.Add(darkLabel170);
            groupBox65.Controls.Add(UseUTCTimeCB);
            groupBox65.ForeColor = SystemColors.Control;
            groupBox65.Location = new Point(0, 0);
            groupBox65.Margin = new Padding(4, 3, 4, 3);
            groupBox65.Name = "groupBox65";
            groupBox65.Padding = new Padding(4, 3, 4, 3);
            groupBox65.Size = new Size(643, 228);
            groupBox65.TabIndex = 18;
            groupBox65.TabStop = false;
            groupBox65.Text = "General";
            // 
            // WeeklyResetDayCB
            // 
            WeeklyResetDayCB.FormattingEnabled = true;
            WeeklyResetDayCB.Items.AddRange(new object[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" });
            WeeklyResetDayCB.Location = new Point(183, 22);
            WeeklyResetDayCB.Margin = new Padding(4, 3, 4, 3);
            WeeklyResetDayCB.Name = "WeeklyResetDayCB";
            WeeklyResetDayCB.Size = new Size(240, 23);
            WeeklyResetDayCB.TabIndex = 15;
            WeeklyResetDayCB.SelectedIndexChanged += WeeklyResetDayCB_SelectedIndexChanged;
            // 
            // WeeklyResetMinuteNUD
            // 
            WeeklyResetMinuteNUD.BackColor = Color.FromArgb(60, 63, 65);
            WeeklyResetMinuteNUD.ForeColor = SystemColors.Control;
            WeeklyResetMinuteNUD.Location = new Point(183, 83);
            WeeklyResetMinuteNUD.Margin = new Padding(4, 3, 4, 3);
            WeeklyResetMinuteNUD.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            WeeklyResetMinuteNUD.Name = "WeeklyResetMinuteNUD";
            WeeklyResetMinuteNUD.Size = new Size(121, 23);
            WeeklyResetMinuteNUD.TabIndex = 14;
            WeeklyResetMinuteNUD.TextAlign = HorizontalAlignment.Center;
            WeeklyResetMinuteNUD.ValueChanged += WeeklyResetMinuteNUD_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.FromArgb(220, 220, 220);
            label2.Location = new Point(15, 175);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(81, 15);
            label2.TabIndex = 13;
            label2.Text = "Use UTC Time";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(220, 220, 220);
            label1.Location = new Point(15, 55);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(106, 15);
            label1.TabIndex = 12;
            label1.Text = "Weekly Reset Hour";
            // 
            // darkLabel168
            // 
            darkLabel168.AutoSize = true;
            darkLabel168.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel168.Location = new Point(15, 25);
            darkLabel168.Margin = new Padding(4, 0, 4, 0);
            darkLabel168.Name = "darkLabel168";
            darkLabel168.Size = new Size(99, 15);
            darkLabel168.TabIndex = 8;
            darkLabel168.Text = "Weekly Reset Day";
            // 
            // darkLabel170
            // 
            darkLabel170.AutoSize = true;
            darkLabel170.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel170.Location = new Point(15, 85);
            darkLabel170.Margin = new Padding(4, 0, 4, 0);
            darkLabel170.Name = "darkLabel170";
            darkLabel170.Size = new Size(117, 15);
            darkLabel170.TabIndex = 6;
            darkLabel170.Text = "Weekly Reset Minute";
            // 
            // UseUTCTimeCB
            // 
            UseUTCTimeCB.AutoSize = true;
            UseUTCTimeCB.ForeColor = SystemColors.Control;
            UseUTCTimeCB.Location = new Point(183, 176);
            UseUTCTimeCB.Margin = new Padding(4, 3, 4, 3);
            UseUTCTimeCB.Name = "UseUTCTimeCB";
            UseUTCTimeCB.Size = new Size(15, 14);
            UseUTCTimeCB.TabIndex = 0;
            UseUTCTimeCB.TextAlign = ContentAlignment.MiddleRight;
            UseUTCTimeCB.UseVisualStyleBackColor = true;
            UseUTCTimeCB.CheckedChanged += UseUTCTimeCB_CheckedChanged;
            // 
            // WeeklyResetHourNUD
            // 
            WeeklyResetHourNUD.BackColor = Color.FromArgb(60, 63, 65);
            WeeklyResetHourNUD.ForeColor = SystemColors.Control;
            WeeklyResetHourNUD.Location = new Point(183, 53);
            WeeklyResetHourNUD.Margin = new Padding(4, 3, 4, 3);
            WeeklyResetHourNUD.Maximum = new decimal(new int[] { 23, 0, 0, 0 });
            WeeklyResetHourNUD.Name = "WeeklyResetHourNUD";
            WeeklyResetHourNUD.Size = new Size(121, 23);
            WeeklyResetHourNUD.TabIndex = 16;
            WeeklyResetHourNUD.TextAlign = HorizontalAlignment.Center;
            WeeklyResetHourNUD.ValueChanged += WeeklyResetHourNUD_ValueChanged;
            // 
            // DailyResetHourNUD
            // 
            DailyResetHourNUD.BackColor = Color.FromArgb(60, 63, 65);
            DailyResetHourNUD.ForeColor = SystemColors.Control;
            DailyResetHourNUD.Location = new Point(183, 113);
            DailyResetHourNUD.Margin = new Padding(4, 3, 4, 3);
            DailyResetHourNUD.Maximum = new decimal(new int[] { 23, 0, 0, 0 });
            DailyResetHourNUD.Name = "DailyResetHourNUD";
            DailyResetHourNUD.Size = new Size(121, 23);
            DailyResetHourNUD.TabIndex = 20;
            DailyResetHourNUD.TextAlign = HorizontalAlignment.Center;
            DailyResetHourNUD.ValueChanged += DailyResetHourNUD_ValueChanged;
            // 
            // DailyResetMinuteNUD
            // 
            DailyResetMinuteNUD.BackColor = Color.FromArgb(60, 63, 65);
            DailyResetMinuteNUD.ForeColor = SystemColors.Control;
            DailyResetMinuteNUD.Location = new Point(183, 143);
            DailyResetMinuteNUD.Margin = new Padding(4, 3, 4, 3);
            DailyResetMinuteNUD.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            DailyResetMinuteNUD.Name = "DailyResetMinuteNUD";
            DailyResetMinuteNUD.Size = new Size(121, 23);
            DailyResetMinuteNUD.TabIndex = 19;
            DailyResetMinuteNUD.TextAlign = HorizontalAlignment.Center;
            DailyResetMinuteNUD.ValueChanged += DailyResetMinuteNUD_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.FromArgb(220, 220, 220);
            label3.Location = new Point(15, 115);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(94, 15);
            label3.TabIndex = 18;
            label3.Text = "Daily Reset Hour";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.FromArgb(220, 220, 220);
            label4.Location = new Point(15, 145);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(105, 15);
            label4.TabIndex = 17;
            label4.Text = "Daily Reset Minute";
            // 
            // ExpansionQuestSettingsResetScheduleControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox65);
            ForeColor = SystemColors.Control;
            Name = "ExpansionQuestSettingsResetScheduleControl";
            Size = new Size(647, 287);
            groupBox65.ResumeLayout(false);
            groupBox65.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)WeeklyResetMinuteNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)WeeklyResetHourNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)DailyResetHourNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)DailyResetMinuteNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox65;
        private ComboBox WeeklyResetDayCB;
        private NumericUpDown WeeklyResetMinuteNUD;
        private Label label2;
        private Label label1;
        private CheckBox EnableQuestLogTabCB;
        private Label darkLabel168;
        private Label darkLabel170;
        private CheckBox UseUTCTimeCB;
        private NumericUpDown WeeklyResetHourNUD;
        private NumericUpDown DailyResetHourNUD;
        private NumericUpDown DailyResetMinuteNUD;
        private Label label3;
        private Label label4;
    }
}
