namespace ExpansionPlugin
{
    partial class AIPAtrolGeneralControl
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
            groupBox3 = new GroupBox();
            darkLabel200 = new Label();
            AIGeneralFormationScaleNUD = new NumericUpDown();
            AIGeneralDamageReceivedMultiplierNUD = new NumericUpDown();
            darkLabel74 = new Label();
            AIGeneralNoiseInvestigationDistanceLimitNUD = new NumericUpDown();
            AIGeneralDanageMultiplierNUD = new NumericUpDown();
            darkLabel62 = new Label();
            darkLabel71 = new Label();
            darkLabel61 = new Label();
            AIGeneralThreatDistanceLimitNUD = new NumericUpDown();
            darkLabel59 = new Label();
            AIGeneralDespawnRadiusNUD = new NumericUpDown();
            darkLabel51 = new Label();
            AIGeneralAccuracyMaxNUD = new NumericUpDown();
            AIGeneralAccuracyMinNUD = new NumericUpDown();
            darkLabel52 = new Label();
            darkLabel37 = new Label();
            AIGeneralDespawnTimeNUD = new NumericUpDown();
            darkLabel15 = new Label();
            AIGeneralMaxDistRadiusNUD = new NumericUpDown();
            darkLabel14 = new Label();
            AIGeneralMinDistRadiusNUD = new NumericUpDown();
            darkLabel3 = new Label();
            AIGeneralEnabledCB = new CheckBox();
            AIGeneralRespawnTimeNUD = new NumericUpDown();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)AIGeneralFormationScaleNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AIGeneralDamageReceivedMultiplierNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AIGeneralNoiseInvestigationDistanceLimitNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AIGeneralDanageMultiplierNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AIGeneralThreatDistanceLimitNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AIGeneralDespawnRadiusNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AIGeneralAccuracyMaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AIGeneralAccuracyMinNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AIGeneralDespawnTimeNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AIGeneralMaxDistRadiusNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AIGeneralMinDistRadiusNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AIGeneralRespawnTimeNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(darkLabel200);
            groupBox3.Controls.Add(AIGeneralFormationScaleNUD);
            groupBox3.Controls.Add(AIGeneralDamageReceivedMultiplierNUD);
            groupBox3.Controls.Add(darkLabel74);
            groupBox3.Controls.Add(AIGeneralNoiseInvestigationDistanceLimitNUD);
            groupBox3.Controls.Add(AIGeneralDanageMultiplierNUD);
            groupBox3.Controls.Add(darkLabel62);
            groupBox3.Controls.Add(darkLabel71);
            groupBox3.Controls.Add(darkLabel61);
            groupBox3.Controls.Add(AIGeneralThreatDistanceLimitNUD);
            groupBox3.Controls.Add(darkLabel59);
            groupBox3.Controls.Add(AIGeneralDespawnRadiusNUD);
            groupBox3.Controls.Add(darkLabel51);
            groupBox3.Controls.Add(AIGeneralAccuracyMaxNUD);
            groupBox3.Controls.Add(AIGeneralAccuracyMinNUD);
            groupBox3.Controls.Add(darkLabel52);
            groupBox3.Controls.Add(darkLabel37);
            groupBox3.Controls.Add(AIGeneralDespawnTimeNUD);
            groupBox3.Controls.Add(darkLabel15);
            groupBox3.Controls.Add(AIGeneralMaxDistRadiusNUD);
            groupBox3.Controls.Add(darkLabel14);
            groupBox3.Controls.Add(AIGeneralMinDistRadiusNUD);
            groupBox3.Controls.Add(darkLabel3);
            groupBox3.Controls.Add(AIGeneralEnabledCB);
            groupBox3.Controls.Add(AIGeneralRespawnTimeNUD);
            groupBox3.ForeColor = SystemColors.Control;
            groupBox3.Location = new Point(0, 0);
            groupBox3.Margin = new Padding(4, 3, 4, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(4, 3, 4, 3);
            groupBox3.Size = new Size(422, 429);
            groupBox3.TabIndex = 137;
            groupBox3.TabStop = false;
            groupBox3.Text = "AI Patrol General Settings";
            // 
            // darkLabel200
            // 
            darkLabel200.AutoSize = true;
            darkLabel200.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel200.Location = new Point(22, 62);
            darkLabel200.Margin = new Padding(4, 0, 4, 0);
            darkLabel200.Name = "darkLabel200";
            darkLabel200.Size = new Size(92, 15);
            darkLabel200.TabIndex = 205;
            darkLabel200.Text = "Formation Scale";
            // 
            // AIGeneralFormationScaleNUD
            // 
            AIGeneralFormationScaleNUD.BackColor = Color.FromArgb(60, 63, 65);
            AIGeneralFormationScaleNUD.DecimalPlaces = 2;
            AIGeneralFormationScaleNUD.ForeColor = SystemColors.Control;
            AIGeneralFormationScaleNUD.Location = new Point(238, 60);
            AIGeneralFormationScaleNUD.Margin = new Padding(4, 3, 4, 3);
            AIGeneralFormationScaleNUD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            AIGeneralFormationScaleNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            AIGeneralFormationScaleNUD.Name = "AIGeneralFormationScaleNUD";
            AIGeneralFormationScaleNUD.Size = new Size(169, 23);
            AIGeneralFormationScaleNUD.TabIndex = 204;
            AIGeneralFormationScaleNUD.TextAlign = HorizontalAlignment.Center;
            AIGeneralFormationScaleNUD.ValueChanged += AIGeneralFormationScaleNUD_ValueChanged;
            // 
            // AIGeneralDamageReceivedMultiplierNUD
            // 
            AIGeneralDamageReceivedMultiplierNUD.BackColor = Color.FromArgb(60, 63, 65);
            AIGeneralDamageReceivedMultiplierNUD.DecimalPlaces = 2;
            AIGeneralDamageReceivedMultiplierNUD.ForeColor = SystemColors.Control;
            AIGeneralDamageReceivedMultiplierNUD.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            AIGeneralDamageReceivedMultiplierNUD.Location = new Point(238, 390);
            AIGeneralDamageReceivedMultiplierNUD.Margin = new Padding(4, 3, 4, 3);
            AIGeneralDamageReceivedMultiplierNUD.Maximum = new decimal(new int[] { 200000, 0, 0, 0 });
            AIGeneralDamageReceivedMultiplierNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            AIGeneralDamageReceivedMultiplierNUD.Name = "AIGeneralDamageReceivedMultiplierNUD";
            AIGeneralDamageReceivedMultiplierNUD.Size = new Size(169, 23);
            AIGeneralDamageReceivedMultiplierNUD.TabIndex = 202;
            AIGeneralDamageReceivedMultiplierNUD.TextAlign = HorizontalAlignment.Center;
            AIGeneralDamageReceivedMultiplierNUD.ValueChanged += AIGeneralDamageReceivedMultiplierNUD_ValueChanged;
            // 
            // darkLabel74
            // 
            darkLabel74.AutoSize = true;
            darkLabel74.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel74.Location = new Point(22, 394);
            darkLabel74.Margin = new Padding(4, 0, 4, 0);
            darkLabel74.Name = "darkLabel74";
            darkLabel74.Size = new Size(155, 15);
            darkLabel74.TabIndex = 203;
            darkLabel74.Text = "Damage Received Multiplier";
            // 
            // AIGeneralNoiseInvestigationDistanceLimitNUD
            // 
            AIGeneralNoiseInvestigationDistanceLimitNUD.BackColor = Color.FromArgb(60, 63, 65);
            AIGeneralNoiseInvestigationDistanceLimitNUD.DecimalPlaces = 2;
            AIGeneralNoiseInvestigationDistanceLimitNUD.ForeColor = SystemColors.Control;
            AIGeneralNoiseInvestigationDistanceLimitNUD.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            AIGeneralNoiseInvestigationDistanceLimitNUD.Location = new Point(238, 331);
            AIGeneralNoiseInvestigationDistanceLimitNUD.Margin = new Padding(4, 3, 4, 3);
            AIGeneralNoiseInvestigationDistanceLimitNUD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            AIGeneralNoiseInvestigationDistanceLimitNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            AIGeneralNoiseInvestigationDistanceLimitNUD.Name = "AIGeneralNoiseInvestigationDistanceLimitNUD";
            AIGeneralNoiseInvestigationDistanceLimitNUD.Size = new Size(169, 23);
            AIGeneralNoiseInvestigationDistanceLimitNUD.TabIndex = 146;
            AIGeneralNoiseInvestigationDistanceLimitNUD.TextAlign = HorizontalAlignment.Center;
            AIGeneralNoiseInvestigationDistanceLimitNUD.ValueChanged += NoiseInvestigationDistanceLimitNUD_ValueChanged;
            // 
            // AIGeneralDanageMultiplierNUD
            // 
            AIGeneralDanageMultiplierNUD.BackColor = Color.FromArgb(60, 63, 65);
            AIGeneralDanageMultiplierNUD.DecimalPlaces = 2;
            AIGeneralDanageMultiplierNUD.ForeColor = SystemColors.Control;
            AIGeneralDanageMultiplierNUD.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            AIGeneralDanageMultiplierNUD.Location = new Point(238, 361);
            AIGeneralDanageMultiplierNUD.Margin = new Padding(4, 3, 4, 3);
            AIGeneralDanageMultiplierNUD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            AIGeneralDanageMultiplierNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            AIGeneralDanageMultiplierNUD.Name = "AIGeneralDanageMultiplierNUD";
            AIGeneralDanageMultiplierNUD.Size = new Size(169, 23);
            AIGeneralDanageMultiplierNUD.TabIndex = 143;
            AIGeneralDanageMultiplierNUD.TextAlign = HorizontalAlignment.Center;
            AIGeneralDanageMultiplierNUD.ValueChanged += AIGeneralDanageMultiplierNUD_ValueChanged;
            // 
            // darkLabel62
            // 
            darkLabel62.AutoSize = true;
            darkLabel62.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel62.Location = new Point(22, 364);
            darkLabel62.Margin = new Padding(4, 0, 4, 0);
            darkLabel62.Name = "darkLabel62";
            darkLabel62.Size = new Size(105, 15);
            darkLabel62.TabIndex = 144;
            darkLabel62.Text = "Damage Multiplier";
            // 
            // darkLabel71
            // 
            darkLabel71.AutoSize = true;
            darkLabel71.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel71.Location = new Point(22, 334);
            darkLabel71.Margin = new Padding(4, 0, 4, 0);
            darkLabel71.Name = "darkLabel71";
            darkLabel71.Size = new Size(186, 15);
            darkLabel71.TabIndex = 145;
            darkLabel71.Text = "Noise Investigation Distance Limit";
            // 
            // darkLabel61
            // 
            darkLabel61.AutoSize = true;
            darkLabel61.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel61.Location = new Point(22, 302);
            darkLabel61.Margin = new Padding(4, 0, 4, 0);
            darkLabel61.Name = "darkLabel61";
            darkLabel61.Size = new Size(118, 15);
            darkLabel61.TabIndex = 142;
            darkLabel61.Text = "Threat Distance Limit";
            // 
            // AIGeneralThreatDistanceLimitNUD
            // 
            AIGeneralThreatDistanceLimitNUD.BackColor = Color.FromArgb(60, 63, 65);
            AIGeneralThreatDistanceLimitNUD.DecimalPlaces = 2;
            AIGeneralThreatDistanceLimitNUD.ForeColor = SystemColors.Control;
            AIGeneralThreatDistanceLimitNUD.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            AIGeneralThreatDistanceLimitNUD.Location = new Point(238, 300);
            AIGeneralThreatDistanceLimitNUD.Margin = new Padding(4, 3, 4, 3);
            AIGeneralThreatDistanceLimitNUD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            AIGeneralThreatDistanceLimitNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            AIGeneralThreatDistanceLimitNUD.Name = "AIGeneralThreatDistanceLimitNUD";
            AIGeneralThreatDistanceLimitNUD.Size = new Size(169, 23);
            AIGeneralThreatDistanceLimitNUD.TabIndex = 141;
            AIGeneralThreatDistanceLimitNUD.TextAlign = HorizontalAlignment.Center;
            AIGeneralThreatDistanceLimitNUD.ValueChanged += AIGeneralThreatDistanceLimitNUD_ValueChanged;
            // 
            // darkLabel59
            // 
            darkLabel59.AutoSize = true;
            darkLabel59.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel59.Location = new Point(22, 122);
            darkLabel59.Margin = new Padding(4, 0, 4, 0);
            darkLabel59.Name = "darkLabel59";
            darkLabel59.Size = new Size(93, 15);
            darkLabel59.TabIndex = 140;
            darkLabel59.Text = "Despawn Radius";
            // 
            // AIGeneralDespawnRadiusNUD
            // 
            AIGeneralDespawnRadiusNUD.BackColor = Color.FromArgb(60, 63, 65);
            AIGeneralDespawnRadiusNUD.DecimalPlaces = 2;
            AIGeneralDespawnRadiusNUD.ForeColor = SystemColors.Control;
            AIGeneralDespawnRadiusNUD.Location = new Point(238, 120);
            AIGeneralDespawnRadiusNUD.Margin = new Padding(4, 3, 4, 3);
            AIGeneralDespawnRadiusNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            AIGeneralDespawnRadiusNUD.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            AIGeneralDespawnRadiusNUD.Name = "AIGeneralDespawnRadiusNUD";
            AIGeneralDespawnRadiusNUD.Size = new Size(169, 23);
            AIGeneralDespawnRadiusNUD.TabIndex = 139;
            AIGeneralDespawnRadiusNUD.TextAlign = HorizontalAlignment.Center;
            AIGeneralDespawnRadiusNUD.ValueChanged += AIGeneralDespawnRadiusNUD_ValueChanged;
            // 
            // darkLabel51
            // 
            darkLabel51.AutoSize = true;
            darkLabel51.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel51.Location = new Point(22, 242);
            darkLabel51.Margin = new Padding(4, 0, 4, 0);
            darkLabel51.Name = "darkLabel51";
            darkLabel51.Size = new Size(80, 15);
            darkLabel51.TabIndex = 136;
            darkLabel51.Text = "Accuracy Min";
            // 
            // AIGeneralAccuracyMaxNUD
            // 
            AIGeneralAccuracyMaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            AIGeneralAccuracyMaxNUD.DecimalPlaces = 2;
            AIGeneralAccuracyMaxNUD.ForeColor = SystemColors.Control;
            AIGeneralAccuracyMaxNUD.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            AIGeneralAccuracyMaxNUD.Location = new Point(238, 270);
            AIGeneralAccuracyMaxNUD.Margin = new Padding(4, 3, 4, 3);
            AIGeneralAccuracyMaxNUD.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            AIGeneralAccuracyMaxNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            AIGeneralAccuracyMaxNUD.Name = "AIGeneralAccuracyMaxNUD";
            AIGeneralAccuracyMaxNUD.Size = new Size(169, 23);
            AIGeneralAccuracyMaxNUD.TabIndex = 137;
            AIGeneralAccuracyMaxNUD.TextAlign = HorizontalAlignment.Center;
            AIGeneralAccuracyMaxNUD.ValueChanged += AIGenralAccuracyMaxNUD_ValueChanged;
            // 
            // AIGeneralAccuracyMinNUD
            // 
            AIGeneralAccuracyMinNUD.BackColor = Color.FromArgb(60, 63, 65);
            AIGeneralAccuracyMinNUD.DecimalPlaces = 2;
            AIGeneralAccuracyMinNUD.ForeColor = SystemColors.Control;
            AIGeneralAccuracyMinNUD.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            AIGeneralAccuracyMinNUD.Location = new Point(238, 240);
            AIGeneralAccuracyMinNUD.Margin = new Padding(4, 3, 4, 3);
            AIGeneralAccuracyMinNUD.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            AIGeneralAccuracyMinNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            AIGeneralAccuracyMinNUD.Name = "AIGeneralAccuracyMinNUD";
            AIGeneralAccuracyMinNUD.Size = new Size(169, 23);
            AIGeneralAccuracyMinNUD.TabIndex = 135;
            AIGeneralAccuracyMinNUD.TextAlign = HorizontalAlignment.Center;
            AIGeneralAccuracyMinNUD.ValueChanged += AIGeneralAccuracyMinNUD_ValueChanged;
            // 
            // darkLabel52
            // 
            darkLabel52.AutoSize = true;
            darkLabel52.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel52.Location = new Point(22, 272);
            darkLabel52.Margin = new Padding(4, 0, 4, 0);
            darkLabel52.Name = "darkLabel52";
            darkLabel52.Size = new Size(82, 15);
            darkLabel52.TabIndex = 138;
            darkLabel52.Text = "Accuracy Max";
            // 
            // darkLabel37
            // 
            darkLabel37.AutoSize = true;
            darkLabel37.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel37.Location = new Point(22, 92);
            darkLabel37.Margin = new Padding(4, 0, 4, 0);
            darkLabel37.Name = "darkLabel37";
            darkLabel37.Size = new Size(84, 15);
            darkLabel37.TabIndex = 134;
            darkLabel37.Text = "Despawn Time";
            // 
            // AIGeneralDespawnTimeNUD
            // 
            AIGeneralDespawnTimeNUD.BackColor = Color.FromArgb(60, 63, 65);
            AIGeneralDespawnTimeNUD.DecimalPlaces = 2;
            AIGeneralDespawnTimeNUD.ForeColor = SystemColors.Control;
            AIGeneralDespawnTimeNUD.Location = new Point(238, 90);
            AIGeneralDespawnTimeNUD.Margin = new Padding(4, 3, 4, 3);
            AIGeneralDespawnTimeNUD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            AIGeneralDespawnTimeNUD.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            AIGeneralDespawnTimeNUD.Name = "AIGeneralDespawnTimeNUD";
            AIGeneralDespawnTimeNUD.Size = new Size(169, 23);
            AIGeneralDespawnTimeNUD.TabIndex = 133;
            AIGeneralDespawnTimeNUD.TextAlign = HorizontalAlignment.Center;
            AIGeneralDespawnTimeNUD.ValueChanged += AIGeneralDespawnTimeNUD_ValueChanged;
            // 
            // darkLabel15
            // 
            darkLabel15.AutoSize = true;
            darkLabel15.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel15.Location = new Point(22, 212);
            darkLabel15.Margin = new Padding(4, 0, 4, 0);
            darkLabel15.Name = "darkLabel15";
            darkLabel15.Size = new Size(91, 15);
            darkLabel15.TabIndex = 132;
            darkLabel15.Text = "Max Dist Radius";
            // 
            // AIGeneralMaxDistRadiusNUD
            // 
            AIGeneralMaxDistRadiusNUD.BackColor = Color.FromArgb(60, 63, 65);
            AIGeneralMaxDistRadiusNUD.DecimalPlaces = 2;
            AIGeneralMaxDistRadiusNUD.ForeColor = SystemColors.Control;
            AIGeneralMaxDistRadiusNUD.Location = new Point(238, 210);
            AIGeneralMaxDistRadiusNUD.Margin = new Padding(4, 3, 4, 3);
            AIGeneralMaxDistRadiusNUD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            AIGeneralMaxDistRadiusNUD.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            AIGeneralMaxDistRadiusNUD.Name = "AIGeneralMaxDistRadiusNUD";
            AIGeneralMaxDistRadiusNUD.Size = new Size(169, 23);
            AIGeneralMaxDistRadiusNUD.TabIndex = 131;
            AIGeneralMaxDistRadiusNUD.TextAlign = HorizontalAlignment.Center;
            AIGeneralMaxDistRadiusNUD.ValueChanged += AIGeneralMaxDistRadiusNUD_ValueChanged;
            // 
            // darkLabel14
            // 
            darkLabel14.AutoSize = true;
            darkLabel14.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel14.Location = new Point(22, 182);
            darkLabel14.Margin = new Padding(4, 0, 4, 0);
            darkLabel14.Name = "darkLabel14";
            darkLabel14.Size = new Size(89, 15);
            darkLabel14.TabIndex = 130;
            darkLabel14.Text = "Min Dist Radius";
            // 
            // AIGeneralMinDistRadiusNUD
            // 
            AIGeneralMinDistRadiusNUD.BackColor = Color.FromArgb(60, 63, 65);
            AIGeneralMinDistRadiusNUD.DecimalPlaces = 2;
            AIGeneralMinDistRadiusNUD.ForeColor = SystemColors.Control;
            AIGeneralMinDistRadiusNUD.Location = new Point(238, 180);
            AIGeneralMinDistRadiusNUD.Margin = new Padding(4, 3, 4, 3);
            AIGeneralMinDistRadiusNUD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            AIGeneralMinDistRadiusNUD.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            AIGeneralMinDistRadiusNUD.Name = "AIGeneralMinDistRadiusNUD";
            AIGeneralMinDistRadiusNUD.Size = new Size(169, 23);
            AIGeneralMinDistRadiusNUD.TabIndex = 129;
            AIGeneralMinDistRadiusNUD.TextAlign = HorizontalAlignment.Center;
            AIGeneralMinDistRadiusNUD.ValueChanged += AIGeneralMinDistRadiusNUD_ValueChanged;
            // 
            // darkLabel3
            // 
            darkLabel3.AutoSize = true;
            darkLabel3.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel3.Location = new Point(22, 152);
            darkLabel3.Margin = new Padding(4, 0, 4, 0);
            darkLabel3.Name = "darkLabel3";
            darkLabel3.Size = new Size(83, 15);
            darkLabel3.TabIndex = 122;
            darkLabel3.Text = "Respawn Time";
            // 
            // AIGeneralEnabledCB
            // 
            AIGeneralEnabledCB.AutoSize = true;
            AIGeneralEnabledCB.CheckAlign = ContentAlignment.MiddleRight;
            AIGeneralEnabledCB.ForeColor = SystemColors.Control;
            AIGeneralEnabledCB.Location = new Point(22, 35);
            AIGeneralEnabledCB.Margin = new Padding(4, 3, 4, 3);
            AIGeneralEnabledCB.Name = "AIGeneralEnabledCB";
            AIGeneralEnabledCB.RightToLeft = RightToLeft.Yes;
            AIGeneralEnabledCB.Size = new Size(68, 19);
            AIGeneralEnabledCB.TabIndex = 128;
            AIGeneralEnabledCB.Text = "Enabled";
            AIGeneralEnabledCB.TextAlign = ContentAlignment.TopCenter;
            AIGeneralEnabledCB.UseVisualStyleBackColor = true;
            AIGeneralEnabledCB.CheckedChanged += AIGeneralEnabledCB_CheckedChanged;
            // 
            // AIGeneralRespawnTimeNUD
            // 
            AIGeneralRespawnTimeNUD.BackColor = Color.FromArgb(60, 63, 65);
            AIGeneralRespawnTimeNUD.DecimalPlaces = 2;
            AIGeneralRespawnTimeNUD.ForeColor = SystemColors.Control;
            AIGeneralRespawnTimeNUD.Location = new Point(238, 150);
            AIGeneralRespawnTimeNUD.Margin = new Padding(4, 3, 4, 3);
            AIGeneralRespawnTimeNUD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            AIGeneralRespawnTimeNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            AIGeneralRespawnTimeNUD.Name = "AIGeneralRespawnTimeNUD";
            AIGeneralRespawnTimeNUD.Size = new Size(169, 23);
            AIGeneralRespawnTimeNUD.TabIndex = 121;
            AIGeneralRespawnTimeNUD.TextAlign = HorizontalAlignment.Center;
            AIGeneralRespawnTimeNUD.ValueChanged += AIGeneralRespawnTimeNUD_ValueChanged;
            // 
            // AIPAtrolGeneralControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox3);
            ForeColor = SystemColors.Control;
            Name = "AIPAtrolGeneralControl";
            Size = new Size(422, 429);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)AIGeneralFormationScaleNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)AIGeneralDamageReceivedMultiplierNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)AIGeneralNoiseInvestigationDistanceLimitNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)AIGeneralDanageMultiplierNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)AIGeneralThreatDistanceLimitNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)AIGeneralDespawnRadiusNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)AIGeneralAccuracyMaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)AIGeneralAccuracyMinNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)AIGeneralDespawnTimeNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)AIGeneralMaxDistRadiusNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)AIGeneralMinDistRadiusNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)AIGeneralRespawnTimeNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox3;
        private Label darkLabel200;
        private NumericUpDown AIGeneralFormationScaleNUD;
        private NumericUpDown AIGeneralDamageReceivedMultiplierNUD;
        private Label darkLabel74;
        private NumericUpDown AIGeneralNoiseInvestigationDistanceLimitNUD;
        private NumericUpDown AIGeneralDanageMultiplierNUD;
        private Label darkLabel62;
        private Label darkLabel71;
        private Label darkLabel61;
        private NumericUpDown AIGeneralThreatDistanceLimitNUD;
        private Label darkLabel59;
        private NumericUpDown AIGeneralDespawnRadiusNUD;
        private Label darkLabel51;
        private NumericUpDown AIGeneralAccuracyMaxNUD;
        private NumericUpDown AIGeneralAccuracyMinNUD;
        private Label darkLabel52;
        private Label darkLabel37;
        private NumericUpDown AIGeneralDespawnTimeNUD;
        private Label darkLabel15;
        private NumericUpDown AIGeneralMaxDistRadiusNUD;
        private Label darkLabel14;
        private NumericUpDown AIGeneralMinDistRadiusNUD;
        private Label darkLabel3;
        private CheckBox AIGeneralEnabledCB;
        private NumericUpDown AIGeneralRespawnTimeNUD;
    }
}
