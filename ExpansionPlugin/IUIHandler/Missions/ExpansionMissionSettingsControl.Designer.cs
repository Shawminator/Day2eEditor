namespace ExpansionPlugin
{
    partial class ExpansionMissionSettingsControl
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
            groupBox39 = new GroupBox();
            darkLabel115 = new Label();
            MinPlayersToStartMissionsNUD = new NumericUpDown();
            darkLabel113 = new Label();
            MaxMissionsNUD = new NumericUpDown();
            darkLabel114 = new Label();
            MinMissionsNUD = new NumericUpDown();
            darkLabel112 = new Label();
            TimeBetweenMissionsNUD = new NumericUpDown();
            darkLabel111 = new Label();
            InitialMissionStartDelayNUD = new NumericUpDown();
            MissionsEnabledCB = new CheckBox();
            label1 = new Label();
            groupBox39.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MinPlayersToStartMissionsNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MaxMissionsNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MinMissionsNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TimeBetweenMissionsNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)InitialMissionStartDelayNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox39
            // 
            groupBox39.Controls.Add(label1);
            groupBox39.Controls.Add(darkLabel115);
            groupBox39.Controls.Add(MinPlayersToStartMissionsNUD);
            groupBox39.Controls.Add(darkLabel113);
            groupBox39.Controls.Add(MaxMissionsNUD);
            groupBox39.Controls.Add(darkLabel114);
            groupBox39.Controls.Add(MinMissionsNUD);
            groupBox39.Controls.Add(darkLabel112);
            groupBox39.Controls.Add(TimeBetweenMissionsNUD);
            groupBox39.Controls.Add(darkLabel111);
            groupBox39.Controls.Add(InitialMissionStartDelayNUD);
            groupBox39.Controls.Add(MissionsEnabledCB);
            groupBox39.ForeColor = SystemColors.Control;
            groupBox39.Location = new Point(0, 0);
            groupBox39.Margin = new Padding(4, 3, 4, 3);
            groupBox39.Name = "groupBox39";
            groupBox39.Padding = new Padding(4, 3, 4, 3);
            groupBox39.Size = new Size(423, 211);
            groupBox39.TabIndex = 2;
            groupBox39.TabStop = false;
            groupBox39.Text = "Settings";
            // 
            // darkLabel115
            // 
            darkLabel115.AutoSize = true;
            darkLabel115.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel115.Location = new Point(14, 175);
            darkLabel115.Margin = new Padding(4, 0, 4, 0);
            darkLabel115.Name = "darkLabel115";
            darkLabel115.Size = new Size(159, 15);
            darkLabel115.TabIndex = 9;
            darkLabel115.Tag = "";
            darkLabel115.Text = "Min Players To Start Missions";
            // 
            // MinPlayersToStartMissionsNUD
            // 
            MinPlayersToStartMissionsNUD.BackColor = Color.FromArgb(60, 63, 65);
            MinPlayersToStartMissionsNUD.ForeColor = SystemColors.Control;
            MinPlayersToStartMissionsNUD.Location = new Point(257, 173);
            MinPlayersToStartMissionsNUD.Margin = new Padding(4, 3, 4, 3);
            MinPlayersToStartMissionsNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            MinPlayersToStartMissionsNUD.Name = "MinPlayersToStartMissionsNUD";
            MinPlayersToStartMissionsNUD.Size = new Size(150, 23);
            MinPlayersToStartMissionsNUD.TabIndex = 10;
            MinPlayersToStartMissionsNUD.Tag = "MinPlayersToStartMissions";
            MinPlayersToStartMissionsNUD.TextAlign = HorizontalAlignment.Center;
            MinPlayersToStartMissionsNUD.ValueChanged += MinPlayersToStartMissionsNUD_ValueChanged;
            // 
            // darkLabel113
            // 
            darkLabel113.AutoSize = true;
            darkLabel113.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel113.Location = new Point(14, 145);
            darkLabel113.Margin = new Padding(4, 0, 4, 0);
            darkLabel113.Name = "darkLabel113";
            darkLabel113.Size = new Size(79, 15);
            darkLabel113.TabIndex = 7;
            darkLabel113.Tag = "";
            darkLabel113.Text = "Max Missions";
            // 
            // MaxMissionsNUD
            // 
            MaxMissionsNUD.BackColor = Color.FromArgb(60, 63, 65);
            MaxMissionsNUD.ForeColor = SystemColors.Control;
            MaxMissionsNUD.Location = new Point(257, 143);
            MaxMissionsNUD.Margin = new Padding(4, 3, 4, 3);
            MaxMissionsNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            MaxMissionsNUD.Name = "MaxMissionsNUD";
            MaxMissionsNUD.Size = new Size(150, 23);
            MaxMissionsNUD.TabIndex = 8;
            MaxMissionsNUD.Tag = "MaxMissions";
            MaxMissionsNUD.TextAlign = HorizontalAlignment.Center;
            MaxMissionsNUD.ValueChanged += MaxMissionsNUD_ValueChanged;
            // 
            // darkLabel114
            // 
            darkLabel114.AutoSize = true;
            darkLabel114.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel114.Location = new Point(14, 115);
            darkLabel114.Margin = new Padding(4, 0, 4, 0);
            darkLabel114.Name = "darkLabel114";
            darkLabel114.Size = new Size(77, 15);
            darkLabel114.TabIndex = 5;
            darkLabel114.Tag = "";
            darkLabel114.Text = "Min Missions";
            // 
            // MinMissionsNUD
            // 
            MinMissionsNUD.BackColor = Color.FromArgb(60, 63, 65);
            MinMissionsNUD.ForeColor = SystemColors.Control;
            MinMissionsNUD.Location = new Point(257, 113);
            MinMissionsNUD.Margin = new Padding(4, 3, 4, 3);
            MinMissionsNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            MinMissionsNUD.Name = "MinMissionsNUD";
            MinMissionsNUD.Size = new Size(150, 23);
            MinMissionsNUD.TabIndex = 6;
            MinMissionsNUD.Tag = "MinMissions";
            MinMissionsNUD.TextAlign = HorizontalAlignment.Center;
            MinMissionsNUD.ValueChanged += MinMissionsNUD_ValueChanged;
            // 
            // darkLabel112
            // 
            darkLabel112.AutoSize = true;
            darkLabel112.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel112.Location = new Point(14, 85);
            darkLabel112.Margin = new Padding(4, 0, 4, 0);
            darkLabel112.Name = "darkLabel112";
            darkLabel112.Size = new Size(173, 15);
            darkLabel112.TabIndex = 3;
            darkLabel112.Tag = "";
            darkLabel112.Text = "Time Between Missions ( mins )";
            // 
            // TimeBetweenMissionsNUD
            // 
            TimeBetweenMissionsNUD.BackColor = Color.FromArgb(60, 63, 65);
            TimeBetweenMissionsNUD.ForeColor = SystemColors.Control;
            TimeBetweenMissionsNUD.Location = new Point(257, 83);
            TimeBetweenMissionsNUD.Margin = new Padding(4, 3, 4, 3);
            TimeBetweenMissionsNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            TimeBetweenMissionsNUD.Name = "TimeBetweenMissionsNUD";
            TimeBetweenMissionsNUD.Size = new Size(150, 23);
            TimeBetweenMissionsNUD.TabIndex = 4;
            TimeBetweenMissionsNUD.Tag = "TimeBetweenMissions";
            TimeBetweenMissionsNUD.TextAlign = HorizontalAlignment.Center;
            TimeBetweenMissionsNUD.ValueChanged += TimeBetweenMissionsNUD_ValueChanged;
            // 
            // darkLabel111
            // 
            darkLabel111.AutoSize = true;
            darkLabel111.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel111.Location = new Point(14, 55);
            darkLabel111.Margin = new Padding(4, 0, 4, 0);
            darkLabel111.Name = "darkLabel111";
            darkLabel111.Size = new Size(182, 15);
            darkLabel111.TabIndex = 1;
            darkLabel111.Tag = "";
            darkLabel111.Text = "Initial Mission Start Delay ( mins )";
            // 
            // InitialMissionStartDelayNUD
            // 
            InitialMissionStartDelayNUD.BackColor = Color.FromArgb(60, 63, 65);
            InitialMissionStartDelayNUD.ForeColor = SystemColors.Control;
            InitialMissionStartDelayNUD.Location = new Point(257, 53);
            InitialMissionStartDelayNUD.Margin = new Padding(4, 3, 4, 3);
            InitialMissionStartDelayNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            InitialMissionStartDelayNUD.Name = "InitialMissionStartDelayNUD";
            InitialMissionStartDelayNUD.Size = new Size(150, 23);
            InitialMissionStartDelayNUD.TabIndex = 2;
            InitialMissionStartDelayNUD.Tag = "InitialMissionStartDelay";
            InitialMissionStartDelayNUD.TextAlign = HorizontalAlignment.Center;
            InitialMissionStartDelayNUD.ValueChanged += InitialMissionStartDelayNUD_ValueChanged;
            // 
            // MissionsEnabledCB
            // 
            MissionsEnabledCB.AutoSize = true;
            MissionsEnabledCB.ForeColor = SystemColors.Control;
            MissionsEnabledCB.Location = new Point(257, 24);
            MissionsEnabledCB.Margin = new Padding(4, 3, 4, 3);
            MissionsEnabledCB.Name = "MissionsEnabledCB";
            MissionsEnabledCB.Size = new Size(15, 14);
            MissionsEnabledCB.TabIndex = 0;
            MissionsEnabledCB.Tag = "Enabled";
            MissionsEnabledCB.TextAlign = ContentAlignment.MiddleRight;
            MissionsEnabledCB.UseVisualStyleBackColor = true;
            MissionsEnabledCB.CheckedChanged += MissionsEnabledCB_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(220, 220, 220);
            label1.Location = new Point(14, 25);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(98, 15);
            label1.TabIndex = 11;
            label1.Tag = "";
            label1.Text = "Missions Enabled";
            // 
            // ExpansionMissionSettingsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox39);
            ForeColor = SystemColors.Control;
            Name = "ExpansionMissionSettingsControl";
            Size = new Size(423, 211);
            groupBox39.ResumeLayout(false);
            groupBox39.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)MinPlayersToStartMissionsNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)MaxMissionsNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)MinMissionsNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)TimeBetweenMissionsNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)InitialMissionStartDelayNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox39;
        private Label label1;
        private Label darkLabel115;
        private NumericUpDown MinPlayersToStartMissionsNUD;
        private Label darkLabel113;
        private NumericUpDown MaxMissionsNUD;
        private Label darkLabel114;
        private NumericUpDown MinMissionsNUD;
        private Label darkLabel112;
        private NumericUpDown TimeBetweenMissionsNUD;
        private Label darkLabel111;
        private NumericUpDown InitialMissionStartDelayNUD;
        private CheckBox MissionsEnabledCB;
    }
}
