namespace ExpansionPlugin
{
    partial class AISettingsConfigControl
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
            MemeLevelNUD = new NumericUpDown();
            label1 = new Label();
            EnableZombieVehicleAttackHandlerCB = new CheckBox();
            EnableZombieVehicleAttackPhysicsCB = new CheckBox();
            LogAIHitByCB = new CheckBox();
            LogAIKilledCB = new CheckBox();
            DamageReceivedMultiplierNUD = new NumericUpDown();
            darkLabel73 = new Label();
            NoiseInvestigationDistanceLimitNUD = new NumericUpDown();
            darkLabel72 = new Label();
            CanRecruitFriendlyCB = new CheckBox();
            SniperProneDistanceThresholdNUD = new NumericUpDown();
            darkLabel66 = new Label();
            CanRecruitGuardsCB = new CheckBox();
            DamageMultiplierNUD = new NumericUpDown();
            darkLabel9 = new Label();
            ThreatDistanceLimitNUD = new NumericUpDown();
            darkLabel5 = new Label();
            darkLabel16 = new Label();
            MannersCB = new CheckBox();
            VaultingCB = new CheckBox();
            AccuracyMaxNUD = new NumericUpDown();
            AccuracyMinNUD = new NumericUpDown();
            darkLabel2 = new Label();
            FormationScaleNUD = new NumericUpDown();
            darkLabel17 = new Label();
            MaxRecruitableAINUD = new NumericUpDown();
            label2 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MemeLevelNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DamageReceivedMultiplierNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NoiseInvestigationDistanceLimitNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SniperProneDistanceThresholdNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DamageMultiplierNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ThreatDistanceLimitNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AccuracyMaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AccuracyMinNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FormationScaleNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MaxRecruitableAINUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(MaxRecruitableAINUD);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(MemeLevelNUD);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(EnableZombieVehicleAttackHandlerCB);
            groupBox1.Controls.Add(EnableZombieVehicleAttackPhysicsCB);
            groupBox1.Controls.Add(LogAIHitByCB);
            groupBox1.Controls.Add(LogAIKilledCB);
            groupBox1.Controls.Add(DamageReceivedMultiplierNUD);
            groupBox1.Controls.Add(darkLabel73);
            groupBox1.Controls.Add(NoiseInvestigationDistanceLimitNUD);
            groupBox1.Controls.Add(darkLabel72);
            groupBox1.Controls.Add(CanRecruitFriendlyCB);
            groupBox1.Controls.Add(SniperProneDistanceThresholdNUD);
            groupBox1.Controls.Add(darkLabel66);
            groupBox1.Controls.Add(CanRecruitGuardsCB);
            groupBox1.Controls.Add(DamageMultiplierNUD);
            groupBox1.Controls.Add(darkLabel9);
            groupBox1.Controls.Add(ThreatDistanceLimitNUD);
            groupBox1.Controls.Add(darkLabel5);
            groupBox1.Controls.Add(darkLabel16);
            groupBox1.Controls.Add(MannersCB);
            groupBox1.Controls.Add(VaultingCB);
            groupBox1.Controls.Add(AccuracyMaxNUD);
            groupBox1.Controls.Add(AccuracyMinNUD);
            groupBox1.Controls.Add(darkLabel2);
            groupBox1.Controls.Add(FormationScaleNUD);
            groupBox1.Controls.Add(darkLabel17);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(755, 235);
            groupBox1.TabIndex = 131;
            groupBox1.TabStop = false;
            groupBox1.Text = "AI Settings";
            // 
            // MemeLevelNUD
            // 
            MemeLevelNUD.BackColor = Color.FromArgb(60, 63, 65);
            MemeLevelNUD.ForeColor = SystemColors.Control;
            MemeLevelNUD.Location = new Point(224, 141);
            MemeLevelNUD.Margin = new Padding(4, 3, 4, 3);
            MemeLevelNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            MemeLevelNUD.Name = "MemeLevelNUD";
            MemeLevelNUD.Size = new Size(124, 23);
            MemeLevelNUD.TabIndex = 206;
            MemeLevelNUD.TextAlign = HorizontalAlignment.Center;
            MemeLevelNUD.ValueChanged += MemeLevelNUD_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(220, 220, 220);
            label1.Location = new Point(24, 143);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(71, 15);
            label1.TabIndex = 207;
            label1.Text = "Meme Level";
            // 
            // EnableZombieVehicleAttackHandlerCB
            // 
            EnableZombieVehicleAttackHandlerCB.AutoSize = true;
            EnableZombieVehicleAttackHandlerCB.CheckAlign = ContentAlignment.MiddleRight;
            EnableZombieVehicleAttackHandlerCB.ForeColor = SystemColors.Control;
            EnableZombieVehicleAttackHandlerCB.Location = new Point(17, 201);
            EnableZombieVehicleAttackHandlerCB.Margin = new Padding(4, 3, 4, 3);
            EnableZombieVehicleAttackHandlerCB.Name = "EnableZombieVehicleAttackHandlerCB";
            EnableZombieVehicleAttackHandlerCB.Size = new Size(227, 19);
            EnableZombieVehicleAttackHandlerCB.TabIndex = 205;
            EnableZombieVehicleAttackHandlerCB.Text = "Enable Zombie Vehicle Attack Handler";
            EnableZombieVehicleAttackHandlerCB.UseVisualStyleBackColor = true;
            EnableZombieVehicleAttackHandlerCB.CheckedChanged += EnableZombieVehicleAttackHandlerCB_CheckedChanged;
            // 
            // EnableZombieVehicleAttackPhysicsCB
            // 
            EnableZombieVehicleAttackPhysicsCB.AutoSize = true;
            EnableZombieVehicleAttackPhysicsCB.CheckAlign = ContentAlignment.MiddleRight;
            EnableZombieVehicleAttackPhysicsCB.ForeColor = SystemColors.Control;
            EnableZombieVehicleAttackPhysicsCB.Location = new Point(286, 201);
            EnableZombieVehicleAttackPhysicsCB.Margin = new Padding(4, 3, 4, 3);
            EnableZombieVehicleAttackPhysicsCB.Name = "EnableZombieVehicleAttackPhysicsCB";
            EnableZombieVehicleAttackPhysicsCB.Size = new Size(186, 19);
            EnableZombieVehicleAttackPhysicsCB.TabIndex = 204;
            EnableZombieVehicleAttackPhysicsCB.Text = "Zombie Vehicle Attack Physics";
            EnableZombieVehicleAttackPhysicsCB.UseVisualStyleBackColor = true;
            EnableZombieVehicleAttackPhysicsCB.CheckedChanged += EnableZombieVehicleAttackPhysicsCB_CheckedChanged;
            // 
            // LogAIHitByCB
            // 
            LogAIHitByCB.AutoSize = true;
            LogAIHitByCB.CheckAlign = ContentAlignment.MiddleRight;
            LogAIHitByCB.ForeColor = SystemColors.Control;
            LogAIHitByCB.Location = new Point(501, 175);
            LogAIHitByCB.Margin = new Padding(4, 3, 4, 3);
            LogAIHitByCB.Name = "LogAIHitByCB";
            LogAIHitByCB.Size = new Size(95, 19);
            LogAIHitByCB.TabIndex = 203;
            LogAIHitByCB.Text = "Log AI Hit By";
            LogAIHitByCB.UseVisualStyleBackColor = true;
            LogAIHitByCB.CheckedChanged += LogAIHitByCB_CheckedChanged;
            // 
            // LogAIKilledCB
            // 
            LogAIKilledCB.AutoSize = true;
            LogAIKilledCB.CheckAlign = ContentAlignment.MiddleRight;
            LogAIKilledCB.ForeColor = SystemColors.Control;
            LogAIKilledCB.Location = new Point(618, 175);
            LogAIKilledCB.Margin = new Padding(4, 3, 4, 3);
            LogAIKilledCB.Name = "LogAIKilledCB";
            LogAIKilledCB.Size = new Size(92, 19);
            LogAIKilledCB.TabIndex = 202;
            LogAIKilledCB.Text = "Log AI Killed";
            LogAIKilledCB.UseVisualStyleBackColor = true;
            LogAIKilledCB.CheckedChanged += LogAIKilledCB_CheckedChanged;
            // 
            // DamageReceivedMultiplierNUD
            // 
            DamageReceivedMultiplierNUD.BackColor = Color.FromArgb(60, 63, 65);
            DamageReceivedMultiplierNUD.DecimalPlaces = 2;
            DamageReceivedMultiplierNUD.ForeColor = SystemColors.Control;
            DamageReceivedMultiplierNUD.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            DamageReceivedMultiplierNUD.Location = new Point(567, 84);
            DamageReceivedMultiplierNUD.Margin = new Padding(4, 3, 4, 3);
            DamageReceivedMultiplierNUD.Maximum = new decimal(new int[] { 200000, 0, 0, 0 });
            DamageReceivedMultiplierNUD.Name = "DamageReceivedMultiplierNUD";
            DamageReceivedMultiplierNUD.Size = new Size(124, 23);
            DamageReceivedMultiplierNUD.TabIndex = 200;
            DamageReceivedMultiplierNUD.TextAlign = HorizontalAlignment.Center;
            DamageReceivedMultiplierNUD.ValueChanged += DamageReceivedMultiplierNUD_ValueChanged;
            // 
            // darkLabel73
            // 
            darkLabel73.AutoSize = true;
            darkLabel73.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel73.Location = new Point(367, 86);
            darkLabel73.Margin = new Padding(4, 0, 4, 0);
            darkLabel73.Name = "darkLabel73";
            darkLabel73.Size = new Size(155, 15);
            darkLabel73.TabIndex = 201;
            darkLabel73.Text = "Damage Received Multiplier";
            // 
            // NoiseInvestigationDistanceLimitNUD
            // 
            NoiseInvestigationDistanceLimitNUD.BackColor = Color.FromArgb(60, 63, 65);
            NoiseInvestigationDistanceLimitNUD.DecimalPlaces = 2;
            NoiseInvestigationDistanceLimitNUD.ForeColor = SystemColors.Control;
            NoiseInvestigationDistanceLimitNUD.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            NoiseInvestigationDistanceLimitNUD.Location = new Point(567, 52);
            NoiseInvestigationDistanceLimitNUD.Margin = new Padding(4, 3, 4, 3);
            NoiseInvestigationDistanceLimitNUD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            NoiseInvestigationDistanceLimitNUD.Minimum = new decimal(new int[] { 2, 0, 0, int.MinValue });
            NoiseInvestigationDistanceLimitNUD.Name = "NoiseInvestigationDistanceLimitNUD";
            NoiseInvestigationDistanceLimitNUD.Size = new Size(124, 23);
            NoiseInvestigationDistanceLimitNUD.TabIndex = 199;
            NoiseInvestigationDistanceLimitNUD.TextAlign = HorizontalAlignment.Center;
            NoiseInvestigationDistanceLimitNUD.ValueChanged += NoiseInvestigationDistanceLimitNUD_ValueChanged;
            // 
            // darkLabel72
            // 
            darkLabel72.AutoSize = true;
            darkLabel72.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel72.Location = new Point(367, 54);
            darkLabel72.Margin = new Padding(4, 0, 4, 0);
            darkLabel72.Name = "darkLabel72";
            darkLabel72.Size = new Size(186, 15);
            darkLabel72.TabIndex = 198;
            darkLabel72.Text = "Noise Investigation Distance Limit";
            // 
            // CanRecruitFriendlyCB
            // 
            CanRecruitFriendlyCB.AutoSize = true;
            CanRecruitFriendlyCB.CheckAlign = ContentAlignment.MiddleRight;
            CanRecruitFriendlyCB.ForeColor = SystemColors.Control;
            CanRecruitFriendlyCB.Location = new Point(193, 175);
            CanRecruitFriendlyCB.Margin = new Padding(4, 3, 4, 3);
            CanRecruitFriendlyCB.Name = "CanRecruitFriendlyCB";
            CanRecruitFriendlyCB.Size = new Size(132, 19);
            CanRecruitFriendlyCB.TabIndex = 142;
            CanRecruitFriendlyCB.Text = "Can Recruit Friendly";
            CanRecruitFriendlyCB.UseVisualStyleBackColor = true;
            CanRecruitFriendlyCB.CheckedChanged += CanRecruitFriendlyCB_CheckedChanged;
            // 
            // SniperProneDistanceThresholdNUD
            // 
            SniperProneDistanceThresholdNUD.BackColor = Color.FromArgb(60, 63, 65);
            SniperProneDistanceThresholdNUD.DecimalPlaces = 2;
            SniperProneDistanceThresholdNUD.ForeColor = SystemColors.Control;
            SniperProneDistanceThresholdNUD.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            SniperProneDistanceThresholdNUD.Location = new Point(224, 112);
            SniperProneDistanceThresholdNUD.Margin = new Padding(4, 3, 4, 3);
            SniperProneDistanceThresholdNUD.Maximum = new decimal(new int[] { 200000, 0, 0, 0 });
            SniperProneDistanceThresholdNUD.Name = "SniperProneDistanceThresholdNUD";
            SniperProneDistanceThresholdNUD.Size = new Size(124, 23);
            SniperProneDistanceThresholdNUD.TabIndex = 140;
            SniperProneDistanceThresholdNUD.TextAlign = HorizontalAlignment.Center;
            SniperProneDistanceThresholdNUD.ValueChanged += SniperProneDistanceThresholdNUD_ValueChanged;
            // 
            // darkLabel66
            // 
            darkLabel66.AutoSize = true;
            darkLabel66.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel66.Location = new Point(21, 114);
            darkLabel66.Margin = new Padding(4, 0, 4, 0);
            darkLabel66.Name = "darkLabel66";
            darkLabel66.Size = new Size(178, 15);
            darkLabel66.TabIndex = 141;
            darkLabel66.Text = "Sniper Prone Distance Threshold";
            // 
            // CanRecruitGuardsCB
            // 
            CanRecruitGuardsCB.AutoSize = true;
            CanRecruitGuardsCB.CheckAlign = ContentAlignment.MiddleRight;
            CanRecruitGuardsCB.ForeColor = SystemColors.Control;
            CanRecruitGuardsCB.Location = new Point(348, 175);
            CanRecruitGuardsCB.Margin = new Padding(4, 3, 4, 3);
            CanRecruitGuardsCB.Name = "CanRecruitGuardsCB";
            CanRecruitGuardsCB.Size = new Size(127, 19);
            CanRecruitGuardsCB.TabIndex = 139;
            CanRecruitGuardsCB.Text = "Can Recruit Guards";
            CanRecruitGuardsCB.UseVisualStyleBackColor = true;
            CanRecruitGuardsCB.CheckedChanged += CanRecruitGuardsCB_CheckedChanged;
            // 
            // DamageMultiplierNUD
            // 
            DamageMultiplierNUD.BackColor = Color.FromArgb(60, 63, 65);
            DamageMultiplierNUD.DecimalPlaces = 2;
            DamageMultiplierNUD.ForeColor = SystemColors.Control;
            DamageMultiplierNUD.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            DamageMultiplierNUD.Location = new Point(224, 82);
            DamageMultiplierNUD.Margin = new Padding(4, 3, 4, 3);
            DamageMultiplierNUD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            DamageMultiplierNUD.Name = "DamageMultiplierNUD";
            DamageMultiplierNUD.Size = new Size(124, 23);
            DamageMultiplierNUD.TabIndex = 137;
            DamageMultiplierNUD.TextAlign = HorizontalAlignment.Center;
            DamageMultiplierNUD.ValueChanged += DamageMultiplierNUD_ValueChanged;
            // 
            // darkLabel9
            // 
            darkLabel9.AutoSize = true;
            darkLabel9.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel9.Location = new Point(21, 84);
            darkLabel9.Margin = new Padding(4, 0, 4, 0);
            darkLabel9.Name = "darkLabel9";
            darkLabel9.Size = new Size(105, 15);
            darkLabel9.TabIndex = 138;
            darkLabel9.Text = "Damage Multiplier";
            // 
            // ThreatDistanceLimitNUD
            // 
            ThreatDistanceLimitNUD.BackColor = Color.FromArgb(60, 63, 65);
            ThreatDistanceLimitNUD.DecimalPlaces = 2;
            ThreatDistanceLimitNUD.ForeColor = SystemColors.Control;
            ThreatDistanceLimitNUD.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            ThreatDistanceLimitNUD.Location = new Point(224, 52);
            ThreatDistanceLimitNUD.Margin = new Padding(4, 3, 4, 3);
            ThreatDistanceLimitNUD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            ThreatDistanceLimitNUD.Name = "ThreatDistanceLimitNUD";
            ThreatDistanceLimitNUD.Size = new Size(124, 23);
            ThreatDistanceLimitNUD.TabIndex = 135;
            ThreatDistanceLimitNUD.TextAlign = HorizontalAlignment.Center;
            ThreatDistanceLimitNUD.ValueChanged += ThreatDistanceLimitNUD_ValueChanged;
            // 
            // darkLabel5
            // 
            darkLabel5.AutoSize = true;
            darkLabel5.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel5.Location = new Point(20, 54);
            darkLabel5.Margin = new Padding(4, 0, 4, 0);
            darkLabel5.Name = "darkLabel5";
            darkLabel5.Size = new Size(119, 15);
            darkLabel5.TabIndex = 136;
            darkLabel5.Text = "Threat Distance Limit";
            // 
            // darkLabel16
            // 
            darkLabel16.AutoSize = true;
            darkLabel16.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel16.Location = new Point(20, 24);
            darkLabel16.Margin = new Padding(4, 0, 4, 0);
            darkLabel16.Name = "darkLabel16";
            darkLabel16.Size = new Size(80, 15);
            darkLabel16.TabIndex = 122;
            darkLabel16.Text = "Accuracy Min";
            // 
            // MannersCB
            // 
            MannersCB.AutoSize = true;
            MannersCB.CheckAlign = ContentAlignment.MiddleRight;
            MannersCB.ForeColor = SystemColors.Control;
            MannersCB.Location = new Point(104, 175);
            MannersCB.Margin = new Padding(4, 3, 4, 3);
            MannersCB.Name = "MannersCB";
            MannersCB.Size = new Size(72, 19);
            MannersCB.TabIndex = 128;
            MannersCB.Text = "Manners";
            MannersCB.UseVisualStyleBackColor = true;
            MannersCB.CheckedChanged += MannersCB_CheckedChanged;
            // 
            // VaultingCB
            // 
            VaultingCB.AutoSize = true;
            VaultingCB.CheckAlign = ContentAlignment.MiddleRight;
            VaultingCB.ForeColor = SystemColors.Control;
            VaultingCB.Location = new Point(20, 175);
            VaultingCB.Margin = new Padding(4, 3, 4, 3);
            VaultingCB.Name = "VaultingCB";
            VaultingCB.Size = new Size(69, 19);
            VaultingCB.TabIndex = 120;
            VaultingCB.Text = "Vaulting";
            VaultingCB.UseVisualStyleBackColor = true;
            VaultingCB.CheckedChanged += VaultingCB_CheckedChanged;
            // 
            // AccuracyMaxNUD
            // 
            AccuracyMaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            AccuracyMaxNUD.DecimalPlaces = 2;
            AccuracyMaxNUD.ForeColor = SystemColors.Control;
            AccuracyMaxNUD.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            AccuracyMaxNUD.Location = new Point(567, 22);
            AccuracyMaxNUD.Margin = new Padding(4, 3, 4, 3);
            AccuracyMaxNUD.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            AccuracyMaxNUD.Name = "AccuracyMaxNUD";
            AccuracyMaxNUD.Size = new Size(124, 23);
            AccuracyMaxNUD.TabIndex = 126;
            AccuracyMaxNUD.TextAlign = HorizontalAlignment.Center;
            AccuracyMaxNUD.ValueChanged += AccuracyMaxNUD_ValueChanged;
            // 
            // AccuracyMinNUD
            // 
            AccuracyMinNUD.BackColor = Color.FromArgb(60, 63, 65);
            AccuracyMinNUD.DecimalPlaces = 2;
            AccuracyMinNUD.ForeColor = SystemColors.Control;
            AccuracyMinNUD.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            AccuracyMinNUD.Location = new Point(224, 22);
            AccuracyMinNUD.Margin = new Padding(4, 3, 4, 3);
            AccuracyMinNUD.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            AccuracyMinNUD.Name = "AccuracyMinNUD";
            AccuracyMinNUD.Size = new Size(124, 23);
            AccuracyMinNUD.TabIndex = 121;
            AccuracyMinNUD.TextAlign = HorizontalAlignment.Center;
            AccuracyMinNUD.ValueChanged += AccuracyMinNUD_ValueChanged;
            // 
            // darkLabel2
            // 
            darkLabel2.AutoSize = true;
            darkLabel2.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel2.Location = new Point(367, 24);
            darkLabel2.Margin = new Padding(4, 0, 4, 0);
            darkLabel2.Name = "darkLabel2";
            darkLabel2.Size = new Size(81, 15);
            darkLabel2.TabIndex = 127;
            darkLabel2.Text = "Accuracy Max";
            // 
            // FormationScaleNUD
            // 
            FormationScaleNUD.BackColor = Color.FromArgb(60, 63, 65);
            FormationScaleNUD.DecimalPlaces = 2;
            FormationScaleNUD.ForeColor = SystemColors.Control;
            FormationScaleNUD.Location = new Point(567, 112);
            FormationScaleNUD.Margin = new Padding(4, 3, 4, 3);
            FormationScaleNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            FormationScaleNUD.Name = "FormationScaleNUD";
            FormationScaleNUD.Size = new Size(124, 23);
            FormationScaleNUD.TabIndex = 123;
            FormationScaleNUD.TextAlign = HorizontalAlignment.Center;
            FormationScaleNUD.ValueChanged += FormationScaleNUD_ValueChanged;
            // 
            // darkLabel17
            // 
            darkLabel17.AutoSize = true;
            darkLabel17.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel17.Location = new Point(367, 114);
            darkLabel17.Margin = new Padding(4, 0, 4, 0);
            darkLabel17.Name = "darkLabel17";
            darkLabel17.Size = new Size(92, 15);
            darkLabel17.TabIndex = 124;
            darkLabel17.Text = "Formation Scale";
            // 
            // MaxRecruitableAINUD
            // 
            MaxRecruitableAINUD.BackColor = Color.FromArgb(60, 63, 65);
            MaxRecruitableAINUD.DecimalPlaces = 2;
            MaxRecruitableAINUD.ForeColor = SystemColors.Control;
            MaxRecruitableAINUD.Location = new Point(567, 141);
            MaxRecruitableAINUD.Margin = new Padding(4, 3, 4, 3);
            MaxRecruitableAINUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            MaxRecruitableAINUD.Name = "MaxRecruitableAINUD";
            MaxRecruitableAINUD.Size = new Size(124, 23);
            MaxRecruitableAINUD.TabIndex = 208;
            MaxRecruitableAINUD.TextAlign = HorizontalAlignment.Center;
            MaxRecruitableAINUD.ValueChanged += MaxRecruitableAINUD_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.FromArgb(220, 220, 220);
            label2.Location = new Point(367, 143);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(105, 15);
            label2.TabIndex = 209;
            label2.Text = "Max Recruitable AI";
            // 
            // AISettingsConfigControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "AISettingsConfigControl";
            Size = new Size(755, 235);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)MemeLevelNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)DamageReceivedMultiplierNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)NoiseInvestigationDistanceLimitNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)SniperProneDistanceThresholdNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)DamageMultiplierNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)ThreatDistanceLimitNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)AccuracyMaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)AccuracyMinNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)FormationScaleNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)MaxRecruitableAINUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private CheckBox EnableZombieVehicleAttackHandlerCB;
        private CheckBox EnableZombieVehicleAttackPhysicsCB;
        private CheckBox LogAIHitByCB;
        private CheckBox LogAIKilledCB;
        private NumericUpDown DamageReceivedMultiplierNUD;
        private Label darkLabel73;
        private NumericUpDown NoiseInvestigationDistanceLimitNUD;
        private Label darkLabel72;
        private CheckBox CanRecruitFriendlyCB;
        private NumericUpDown SniperProneDistanceThresholdNUD;
        private Label darkLabel66;
        private CheckBox CanRecruitGuardsCB;
        private NumericUpDown DamageMultiplierNUD;
        private Label darkLabel9;
        private NumericUpDown ThreatDistanceLimitNUD;
        private Label darkLabel5;
        private Label darkLabel16;
        private CheckBox MannersCB;
        private CheckBox VaultingCB;
        private NumericUpDown AccuracyMaxNUD;
        private NumericUpDown AccuracyMinNUD;
        private Label darkLabel2;
        private NumericUpDown FormationScaleNUD;
        private Label darkLabel17;
        private NumericUpDown MemeLevelNUD;
        private Label label1;
        private NumericUpDown MaxRecruitableAINUD;
        private Label label2;
    }
}
