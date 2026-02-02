namespace ExpansionPlugin
{
    partial class ExpansionRaidSettingsRaidScheduleControl
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
            groupBox84 = new GroupBox();
            darkLabel255 = new Label();
            DurationMinutesNUD = new NumericUpDown();
            darkLabel257 = new Label();
            StartMinuteNUD = new NumericUpDown();
            darkLabel258 = new Label();
            StartHourNUD = new NumericUpDown();
            darkLabel259 = new Label();
            WeekdayCB = new ComboBox();
            groupBox84.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DurationMinutesNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)StartMinuteNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)StartHourNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox84
            // 
            groupBox84.Controls.Add(WeekdayCB);
            groupBox84.Controls.Add(darkLabel255);
            groupBox84.Controls.Add(DurationMinutesNUD);
            groupBox84.Controls.Add(darkLabel257);
            groupBox84.Controls.Add(StartMinuteNUD);
            groupBox84.Controls.Add(darkLabel258);
            groupBox84.Controls.Add(StartHourNUD);
            groupBox84.Controls.Add(darkLabel259);
            groupBox84.ForeColor = SystemColors.Control;
            groupBox84.Location = new Point(0, 0);
            groupBox84.Margin = new Padding(4, 3, 4, 3);
            groupBox84.Name = "groupBox84";
            groupBox84.Padding = new Padding(4, 3, 4, 3);
            groupBox84.Size = new Size(415, 149);
            groupBox84.TabIndex = 17;
            groupBox84.TabStop = false;
            groupBox84.Text = "schedule";
            // 
            // darkLabel255
            // 
            darkLabel255.AutoSize = true;
            darkLabel255.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel255.Location = new Point(15, 115);
            darkLabel255.Margin = new Padding(4, 0, 4, 0);
            darkLabel255.Name = "darkLabel255";
            darkLabel255.Size = new Size(112, 15);
            darkLabel255.TabIndex = 9;
            darkLabel255.Text = "Duration in Minutes";
            // 
            // DurationMinutesNUD
            // 
            DurationMinutesNUD.BackColor = Color.FromArgb(60, 63, 65);
            DurationMinutesNUD.ForeColor = SystemColors.Control;
            DurationMinutesNUD.Location = new Point(133, 112);
            DurationMinutesNUD.Margin = new Padding(4, 3, 4, 3);
            DurationMinutesNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            DurationMinutesNUD.Name = "DurationMinutesNUD";
            DurationMinutesNUD.Size = new Size(141, 23);
            DurationMinutesNUD.TabIndex = 10;
            DurationMinutesNUD.TextAlign = HorizontalAlignment.Center;
            DurationMinutesNUD.ValueChanged += DurationMinutesNUD_ValueChanged;
            // 
            // darkLabel257
            // 
            darkLabel257.AutoSize = true;
            darkLabel257.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel257.Location = new Point(15, 85);
            darkLabel257.Margin = new Padding(4, 0, 4, 0);
            darkLabel257.Name = "darkLabel257";
            darkLabel257.Size = new Size(72, 15);
            darkLabel257.TabIndex = 7;
            darkLabel257.Text = "Start Minute";
            // 
            // StartMinuteNUD
            // 
            StartMinuteNUD.BackColor = Color.FromArgb(60, 63, 65);
            StartMinuteNUD.ForeColor = SystemColors.Control;
            StartMinuteNUD.Location = new Point(133, 82);
            StartMinuteNUD.Margin = new Padding(4, 3, 4, 3);
            StartMinuteNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            StartMinuteNUD.Name = "StartMinuteNUD";
            StartMinuteNUD.Size = new Size(141, 23);
            StartMinuteNUD.TabIndex = 8;
            StartMinuteNUD.TextAlign = HorizontalAlignment.Center;
            StartMinuteNUD.ValueChanged += StartMinuteNUD_ValueChanged;
            // 
            // darkLabel258
            // 
            darkLabel258.AutoSize = true;
            darkLabel258.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel258.Location = new Point(15, 55);
            darkLabel258.Margin = new Padding(4, 0, 4, 0);
            darkLabel258.Name = "darkLabel258";
            darkLabel258.Size = new Size(61, 15);
            darkLabel258.TabIndex = 5;
            darkLabel258.Text = "Start Hour";
            // 
            // StartHourNUD
            // 
            StartHourNUD.BackColor = Color.FromArgb(60, 63, 65);
            StartHourNUD.ForeColor = SystemColors.Control;
            StartHourNUD.Location = new Point(133, 52);
            StartHourNUD.Margin = new Padding(4, 3, 4, 3);
            StartHourNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            StartHourNUD.Name = "StartHourNUD";
            StartHourNUD.Size = new Size(141, 23);
            StartHourNUD.TabIndex = 6;
            StartHourNUD.TextAlign = HorizontalAlignment.Center;
            StartHourNUD.ValueChanged += StartHourNUD_ValueChanged;
            // 
            // darkLabel259
            // 
            darkLabel259.AutoSize = true;
            darkLabel259.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel259.Location = new Point(14, 25);
            darkLabel259.Margin = new Padding(4, 0, 4, 0);
            darkLabel259.Name = "darkLabel259";
            darkLabel259.Size = new Size(55, 15);
            darkLabel259.TabIndex = 3;
            darkLabel259.Text = "Weekday";
            // 
            // WeekdayCB
            // 
            WeekdayCB.BackColor = Color.FromArgb(60, 63, 65);
            WeekdayCB.ForeColor = SystemColors.Control;
            WeekdayCB.FormattingEnabled = true;
            WeekdayCB.Location = new Point(133, 25);
            WeekdayCB.Name = "WeekdayCB";
            WeekdayCB.Size = new Size(275, 23);
            WeekdayCB.TabIndex = 11;
            WeekdayCB.SelectedIndexChanged += WeekdayCB_SelectedIndexChanged;
            // 
            // ExpansionRaidSettingsRaidScheduleControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox84);
            ForeColor = SystemColors.Control;
            Name = "ExpansionRaidSettingsRaidScheduleControl";
            Size = new Size(415, 149);
            groupBox84.ResumeLayout(false);
            groupBox84.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DurationMinutesNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)StartMinuteNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)StartHourNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox84;
        private Label darkLabel255;
        private NumericUpDown DurationMinutesNUD;
        private Label darkLabel257;
        private NumericUpDown StartMinuteNUD;
        private Label darkLabel258;
        private NumericUpDown StartHourNUD;
        private Label darkLabel259;
        private ComboBox WeekdayCB;
    }
}
