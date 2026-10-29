namespace CaparePlugin
{
    partial class CapareKeyCardLocationControl
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
            building_xL = new Label();
            building_xNUD = new NumericUpDown();
            building_yL = new Label();
            building_yNUD = new NumericUpDown();
            building_zL = new Label();
            building_zNUD = new NumericUpDown();
            BuildingClassNameL = new Label();
            BuildingClassNameTB = new TextBox();
            door_indexL = new Label();
            door_indexNUD = new NumericUpDown();
            KeycardClassNameL = new Label();
            KeycardClassNameComboBox = new ComboBox();
            SingleUseL = new Label();
            SingleUseCB = new CheckBox();
            AutomaticCloseTimerL = new Label();
            AutomaticCloseTimerNUD = new NumericUpDown();
            CloseWarningL = new Label();
            CloseWarningCB = new CheckBox();
            groupBox2 = new GroupBox();
            UseAlarmL = new Label();
            UseAlarmCB = new CheckBox();
            AlarmSoundL = new Label();
            AlarmSoundTB = new TextBox();
            AlarmMapwideL = new Label();
            AlarmMapwideCB = new CheckBox();
            groupBox3 = new GroupBox();
            SpawnLootOnInitL = new Label();
            SpawnLootOnInitCB = new CheckBox();
            NumberOfLootPointsL = new Label();
            NumberOfLootPointsNUD = new NumericUpDown();
            SpawnSpecificLootL = new Label();
            SpawnSpecificLootCB = new CheckBox();
            groupBox4 = new GroupBox();
            SpawnInfectedL = new Label();
            SpawnInfectedCB = new CheckBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)building_xNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)building_yNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)building_zNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)door_indexNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AutomaticCloseTimerNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumberOfLootPointsNUD).BeginInit();
            SuspendLayout();
            //
            // groupBox1
            //
            groupBox1.Controls.Add(building_xL);
            groupBox1.Controls.Add(building_xNUD);
            groupBox1.Controls.Add(building_yL);
            groupBox1.Controls.Add(building_yNUD);
            groupBox1.Controls.Add(building_zL);
            groupBox1.Controls.Add(building_zNUD);
            groupBox1.Controls.Add(BuildingClassNameL);
            groupBox1.Controls.Add(BuildingClassNameTB);
            groupBox1.Controls.Add(door_indexL);
            groupBox1.Controls.Add(door_indexNUD);
            groupBox1.Controls.Add(KeycardClassNameL);
            groupBox1.Controls.Add(KeycardClassNameComboBox);
            groupBox1.Controls.Add(SingleUseL);
            groupBox1.Controls.Add(SingleUseCB);
            groupBox1.Controls.Add(AutomaticCloseTimerL);
            groupBox1.Controls.Add(AutomaticCloseTimerNUD);
            groupBox1.Controls.Add(CloseWarningL);
            groupBox1.Controls.Add(CloseWarningCB);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(435, 289);
            groupBox1.TabIndex = 18;
            groupBox1.TabStop = false;
            groupBox1.Text = "Door";
            //
            // groupBox2
            //
            groupBox2.Controls.Add(UseAlarmL);
            groupBox2.Controls.Add(UseAlarmCB);
            groupBox2.Controls.Add(AlarmSoundL);
            groupBox2.Controls.Add(AlarmSoundTB);
            groupBox2.Controls.Add(AlarmMapwideL);
            groupBox2.Controls.Add(AlarmMapwideCB);
            groupBox2.ForeColor = SystemColors.Control;
            groupBox2.Location = new Point(0, 295);
            groupBox2.Margin = new Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 3, 4, 3);
            groupBox2.Size = new Size(435, 115);
            groupBox2.TabIndex = 25;
            groupBox2.TabStop = false;
            groupBox2.Text = "Alarm";
            //
            // groupBox3
            //
            groupBox3.Controls.Add(SpawnLootOnInitL);
            groupBox3.Controls.Add(SpawnLootOnInitCB);
            groupBox3.Controls.Add(NumberOfLootPointsL);
            groupBox3.Controls.Add(NumberOfLootPointsNUD);
            groupBox3.Controls.Add(SpawnSpecificLootL);
            groupBox3.Controls.Add(SpawnSpecificLootCB);
            groupBox3.ForeColor = SystemColors.Control;
            groupBox3.Location = new Point(0, 416);
            groupBox3.Margin = new Padding(4, 3, 4, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(4, 3, 4, 3);
            groupBox3.Size = new Size(435, 115);
            groupBox3.TabIndex = 32;
            groupBox3.TabStop = false;
            groupBox3.Text = "Loot";
            //
            // groupBox4
            //
            groupBox4.Controls.Add(SpawnInfectedL);
            groupBox4.Controls.Add(SpawnInfectedCB);
            groupBox4.ForeColor = SystemColors.Control;
            groupBox4.Location = new Point(0, 537);
            groupBox4.Margin = new Padding(4, 3, 4, 3);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(4, 3, 4, 3);
            groupBox4.Size = new Size(435, 57);
            groupBox4.TabIndex = 35;
            groupBox4.TabStop = false;
            groupBox4.Text = "Infected";
            //
            // building_xL
            //
            building_xL.AutoSize = true;
            building_xL.ForeColor = Color.FromArgb(220, 220, 220);
            building_xL.Location = new Point(11, 25);
            building_xL.Margin = new Padding(4, 0, 4, 0);
            building_xL.Name = "building_xL";
            building_xL.Size = new Size(70, 15);
            building_xL.TabIndex = 0;
            building_xL.Text = "Building X";
            //
            // building_xNUD
            //
            building_xNUD.BackColor = Color.FromArgb(60, 63, 65);
            building_xNUD.DecimalPlaces = 6;
            building_xNUD.ForeColor = SystemColors.Control;
            building_xNUD.Location = new Point(190, 22);
            building_xNUD.Margin = new Padding(4, 3, 4, 3);
            building_xNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            building_xNUD.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            building_xNUD.Name = "building_xNUD";
            building_xNUD.Size = new Size(230, 23);
            building_xNUD.TabIndex = 1;
            building_xNUD.TextAlign = HorizontalAlignment.Center;
            building_xNUD.ValueChanged += building_xNUD_ValueChanged;
            //
            // building_yL
            //
            building_yL.AutoSize = true;
            building_yL.ForeColor = Color.FromArgb(220, 220, 220);
            building_yL.Location = new Point(11, 54);
            building_yL.Margin = new Padding(4, 0, 4, 0);
            building_yL.Name = "building_yL";
            building_yL.Size = new Size(70, 15);
            building_yL.TabIndex = 2;
            building_yL.Text = "Building Y";
            //
            // building_yNUD
            //
            building_yNUD.BackColor = Color.FromArgb(60, 63, 65);
            building_yNUD.DecimalPlaces = 6;
            building_yNUD.ForeColor = SystemColors.Control;
            building_yNUD.Location = new Point(190, 51);
            building_yNUD.Margin = new Padding(4, 3, 4, 3);
            building_yNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            building_yNUD.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            building_yNUD.Name = "building_yNUD";
            building_yNUD.Size = new Size(230, 23);
            building_yNUD.TabIndex = 3;
            building_yNUD.TextAlign = HorizontalAlignment.Center;
            building_yNUD.ValueChanged += building_yNUD_ValueChanged;
            //
            // building_zL
            //
            building_zL.AutoSize = true;
            building_zL.ForeColor = Color.FromArgb(220, 220, 220);
            building_zL.Location = new Point(11, 83);
            building_zL.Margin = new Padding(4, 0, 4, 0);
            building_zL.Name = "building_zL";
            building_zL.Size = new Size(70, 15);
            building_zL.TabIndex = 4;
            building_zL.Text = "Building Z";
            //
            // building_zNUD
            //
            building_zNUD.BackColor = Color.FromArgb(60, 63, 65);
            building_zNUD.DecimalPlaces = 6;
            building_zNUD.ForeColor = SystemColors.Control;
            building_zNUD.Location = new Point(190, 80);
            building_zNUD.Margin = new Padding(4, 3, 4, 3);
            building_zNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            building_zNUD.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            building_zNUD.Name = "building_zNUD";
            building_zNUD.Size = new Size(230, 23);
            building_zNUD.TabIndex = 5;
            building_zNUD.TextAlign = HorizontalAlignment.Center;
            building_zNUD.ValueChanged += building_zNUD_ValueChanged;
            //
            // BuildingClassNameL
            //
            BuildingClassNameL.AutoSize = true;
            BuildingClassNameL.ForeColor = Color.FromArgb(220, 220, 220);
            BuildingClassNameL.Location = new Point(11, 112);
            BuildingClassNameL.Margin = new Padding(4, 0, 4, 0);
            BuildingClassNameL.Name = "BuildingClassNameL";
            BuildingClassNameL.Size = new Size(133, 15);
            BuildingClassNameL.TabIndex = 6;
            BuildingClassNameL.Text = "Building Class Name";
            //
            // BuildingClassNameTB
            //
            BuildingClassNameTB.BackColor = Color.FromArgb(60, 63, 65);
            BuildingClassNameTB.ForeColor = SystemColors.Control;
            BuildingClassNameTB.Location = new Point(190, 109);
            BuildingClassNameTB.Margin = new Padding(4, 3, 4, 3);
            BuildingClassNameTB.Name = "BuildingClassNameTB";
            BuildingClassNameTB.Size = new Size(230, 23);
            BuildingClassNameTB.TabIndex = 7;
            BuildingClassNameTB.TextChanged += BuildingClassNameTB_TextChanged;
            //
            // door_indexL
            //
            door_indexL.AutoSize = true;
            door_indexL.ForeColor = Color.FromArgb(220, 220, 220);
            door_indexL.Location = new Point(11, 141);
            door_indexL.Margin = new Padding(4, 0, 4, 0);
            door_indexL.Name = "door_indexL";
            door_indexL.Size = new Size(70, 15);
            door_indexL.TabIndex = 8;
            door_indexL.Text = "Door Index";
            //
            // door_indexNUD
            //
            door_indexNUD.BackColor = Color.FromArgb(60, 63, 65);
            door_indexNUD.ForeColor = SystemColors.Control;
            door_indexNUD.Location = new Point(190, 138);
            door_indexNUD.Margin = new Padding(4, 3, 4, 3);
            door_indexNUD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            door_indexNUD.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            door_indexNUD.Name = "door_indexNUD";
            door_indexNUD.Size = new Size(230, 23);
            door_indexNUD.TabIndex = 9;
            door_indexNUD.TextAlign = HorizontalAlignment.Center;
            door_indexNUD.ValueChanged += door_indexNUD_ValueChanged;
            //
            // KeycardClassNameL
            //
            KeycardClassNameL.AutoSize = true;
            KeycardClassNameL.ForeColor = Color.FromArgb(220, 220, 220);
            KeycardClassNameL.Location = new Point(11, 170);
            KeycardClassNameL.Margin = new Padding(4, 0, 4, 0);
            KeycardClassNameL.Name = "KeycardClassNameL";
            KeycardClassNameL.Size = new Size(126, 15);
            KeycardClassNameL.TabIndex = 10;
            KeycardClassNameL.Text = "Keycard Class Name";
            //
            // KeycardClassNameComboBox
            //
            KeycardClassNameComboBox.BackColor = Color.FromArgb(60, 63, 65);
            KeycardClassNameComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            KeycardClassNameComboBox.FlatStyle = FlatStyle.Flat;
            KeycardClassNameComboBox.ForeColor = SystemColors.Control;
            KeycardClassNameComboBox.FormattingEnabled = true;
            KeycardClassNameComboBox.Location = new Point(190, 167);
            KeycardClassNameComboBox.Margin = new Padding(4, 3, 4, 3);
            KeycardClassNameComboBox.Name = "KeycardClassNameComboBox";
            KeycardClassNameComboBox.Size = new Size(230, 23);
            KeycardClassNameComboBox.TabIndex = 11;
            KeycardClassNameComboBox.TextChanged += KeycardClassNameComboBox_TextChanged;
            //
            // SingleUseL
            //
            SingleUseL.AutoSize = true;
            SingleUseL.ForeColor = Color.FromArgb(220, 220, 220);
            SingleUseL.Location = new Point(11, 199);
            SingleUseL.Margin = new Padding(4, 0, 4, 0);
            SingleUseL.Name = "SingleUseL";
            SingleUseL.Size = new Size(70, 15);
            SingleUseL.TabIndex = 12;
            SingleUseL.Text = "Single Use";
            //
            // SingleUseCB
            //
            SingleUseCB.AutoSize = true;
            SingleUseCB.Location = new Point(190, 199);
            SingleUseCB.Margin = new Padding(4, 3, 4, 3);
            SingleUseCB.Name = "SingleUseCB";
            SingleUseCB.Size = new Size(15, 14);
            SingleUseCB.TabIndex = 13;
            SingleUseCB.UseVisualStyleBackColor = true;
            SingleUseCB.CheckedChanged += SingleUseCB_CheckedChanged;
            //
            // AutomaticCloseTimerL
            //
            AutomaticCloseTimerL.AutoSize = true;
            AutomaticCloseTimerL.ForeColor = Color.FromArgb(220, 220, 220);
            AutomaticCloseTimerL.Location = new Point(11, 228);
            AutomaticCloseTimerL.Margin = new Padding(4, 0, 4, 0);
            AutomaticCloseTimerL.Name = "AutomaticCloseTimerL";
            AutomaticCloseTimerL.Size = new Size(217, 15);
            AutomaticCloseTimerL.TabIndex = 14;
            AutomaticCloseTimerL.Text = "Automatic Close Timer (Seconds)";
            //
            // AutomaticCloseTimerNUD
            //
            AutomaticCloseTimerNUD.BackColor = Color.FromArgb(60, 63, 65);
            AutomaticCloseTimerNUD.ForeColor = SystemColors.Control;
            AutomaticCloseTimerNUD.Location = new Point(190, 225);
            AutomaticCloseTimerNUD.Margin = new Padding(4, 3, 4, 3);
            AutomaticCloseTimerNUD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            AutomaticCloseTimerNUD.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            AutomaticCloseTimerNUD.Name = "AutomaticCloseTimerNUD";
            AutomaticCloseTimerNUD.Size = new Size(230, 23);
            AutomaticCloseTimerNUD.TabIndex = 15;
            AutomaticCloseTimerNUD.TextAlign = HorizontalAlignment.Center;
            AutomaticCloseTimerNUD.ValueChanged += AutomaticCloseTimerNUD_ValueChanged;
            //
            // CloseWarningL
            //
            CloseWarningL.AutoSize = true;
            CloseWarningL.ForeColor = Color.FromArgb(220, 220, 220);
            CloseWarningL.Location = new Point(11, 257);
            CloseWarningL.Margin = new Padding(4, 0, 4, 0);
            CloseWarningL.Name = "CloseWarningL";
            CloseWarningL.Size = new Size(91, 15);
            CloseWarningL.TabIndex = 16;
            CloseWarningL.Text = "Close Warning";
            //
            // CloseWarningCB
            //
            CloseWarningCB.AutoSize = true;
            CloseWarningCB.Location = new Point(190, 257);
            CloseWarningCB.Margin = new Padding(4, 3, 4, 3);
            CloseWarningCB.Name = "CloseWarningCB";
            CloseWarningCB.Size = new Size(15, 14);
            CloseWarningCB.TabIndex = 17;
            CloseWarningCB.UseVisualStyleBackColor = true;
            CloseWarningCB.CheckedChanged += CloseWarningCB_CheckedChanged;
            //
            // UseAlarmL
            //
            UseAlarmL.AutoSize = true;
            UseAlarmL.ForeColor = Color.FromArgb(220, 220, 220);
            UseAlarmL.Location = new Point(11, 25);
            UseAlarmL.Margin = new Padding(4, 0, 4, 0);
            UseAlarmL.Name = "UseAlarmL";
            UseAlarmL.Size = new Size(63, 15);
            UseAlarmL.TabIndex = 19;
            UseAlarmL.Text = "Use Alarm";
            //
            // UseAlarmCB
            //
            UseAlarmCB.AutoSize = true;
            UseAlarmCB.Location = new Point(190, 25);
            UseAlarmCB.Margin = new Padding(4, 3, 4, 3);
            UseAlarmCB.Name = "UseAlarmCB";
            UseAlarmCB.Size = new Size(15, 14);
            UseAlarmCB.TabIndex = 20;
            UseAlarmCB.UseVisualStyleBackColor = true;
            UseAlarmCB.CheckedChanged += UseAlarmCB_CheckedChanged;
            //
            // AlarmSoundL
            //
            AlarmSoundL.AutoSize = true;
            AlarmSoundL.ForeColor = Color.FromArgb(220, 220, 220);
            AlarmSoundL.Location = new Point(11, 54);
            AlarmSoundL.Margin = new Padding(4, 0, 4, 0);
            AlarmSoundL.Name = "AlarmSoundL";
            AlarmSoundL.Size = new Size(77, 15);
            AlarmSoundL.TabIndex = 21;
            AlarmSoundL.Text = "Alarm Sound";
            //
            // AlarmSoundTB
            //
            AlarmSoundTB.BackColor = Color.FromArgb(60, 63, 65);
            AlarmSoundTB.ForeColor = SystemColors.Control;
            AlarmSoundTB.Location = new Point(190, 51);
            AlarmSoundTB.Margin = new Padding(4, 3, 4, 3);
            AlarmSoundTB.Name = "AlarmSoundTB";
            AlarmSoundTB.Size = new Size(230, 23);
            AlarmSoundTB.TabIndex = 22;
            AlarmSoundTB.TextChanged += AlarmSoundTB_TextChanged;
            //
            // AlarmMapwideL
            //
            AlarmMapwideL.AutoSize = true;
            AlarmMapwideL.ForeColor = Color.FromArgb(220, 220, 220);
            AlarmMapwideL.Location = new Point(11, 83);
            AlarmMapwideL.Margin = new Padding(4, 0, 4, 0);
            AlarmMapwideL.Name = "AlarmMapwideL";
            AlarmMapwideL.Size = new Size(91, 15);
            AlarmMapwideL.TabIndex = 23;
            AlarmMapwideL.Text = "Alarm Mapwide";
            //
            // AlarmMapwideCB
            //
            AlarmMapwideCB.AutoSize = true;
            AlarmMapwideCB.Location = new Point(190, 83);
            AlarmMapwideCB.Margin = new Padding(4, 3, 4, 3);
            AlarmMapwideCB.Name = "AlarmMapwideCB";
            AlarmMapwideCB.Size = new Size(15, 14);
            AlarmMapwideCB.TabIndex = 24;
            AlarmMapwideCB.UseVisualStyleBackColor = true;
            AlarmMapwideCB.CheckedChanged += AlarmMapwideCB_CheckedChanged;
            //
            // SpawnLootOnInitL
            //
            SpawnLootOnInitL.AutoSize = true;
            SpawnLootOnInitL.ForeColor = Color.FromArgb(220, 220, 220);
            SpawnLootOnInitL.Location = new Point(11, 25);
            SpawnLootOnInitL.Margin = new Padding(4, 0, 4, 0);
            SpawnLootOnInitL.Name = "SpawnLootOnInitL";
            SpawnLootOnInitL.Size = new Size(126, 15);
            SpawnLootOnInitL.TabIndex = 26;
            SpawnLootOnInitL.Text = "Spawn Loot On Init";
            //
            // SpawnLootOnInitCB
            //
            SpawnLootOnInitCB.AutoSize = true;
            SpawnLootOnInitCB.Location = new Point(190, 25);
            SpawnLootOnInitCB.Margin = new Padding(4, 3, 4, 3);
            SpawnLootOnInitCB.Name = "SpawnLootOnInitCB";
            SpawnLootOnInitCB.Size = new Size(15, 14);
            SpawnLootOnInitCB.TabIndex = 27;
            SpawnLootOnInitCB.UseVisualStyleBackColor = true;
            SpawnLootOnInitCB.CheckedChanged += SpawnLootOnInitCB_CheckedChanged;
            //
            // NumberOfLootPointsL
            //
            NumberOfLootPointsL.AutoSize = true;
            NumberOfLootPointsL.ForeColor = Color.FromArgb(220, 220, 220);
            NumberOfLootPointsL.Location = new Point(11, 54);
            NumberOfLootPointsL.Margin = new Padding(4, 0, 4, 0);
            NumberOfLootPointsL.Name = "NumberOfLootPointsL";
            NumberOfLootPointsL.Size = new Size(147, 15);
            NumberOfLootPointsL.TabIndex = 28;
            NumberOfLootPointsL.Text = "Number Of Loot Points";
            //
            // NumberOfLootPointsNUD
            //
            NumberOfLootPointsNUD.BackColor = Color.FromArgb(60, 63, 65);
            NumberOfLootPointsNUD.ForeColor = SystemColors.Control;
            NumberOfLootPointsNUD.Location = new Point(190, 51);
            NumberOfLootPointsNUD.Margin = new Padding(4, 3, 4, 3);
            NumberOfLootPointsNUD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            NumberOfLootPointsNUD.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            NumberOfLootPointsNUD.Name = "NumberOfLootPointsNUD";
            NumberOfLootPointsNUD.Size = new Size(230, 23);
            NumberOfLootPointsNUD.TabIndex = 29;
            NumberOfLootPointsNUD.TextAlign = HorizontalAlignment.Center;
            NumberOfLootPointsNUD.ValueChanged += NumberOfLootPointsNUD_ValueChanged;
            //
            // SpawnSpecificLootL
            //
            SpawnSpecificLootL.AutoSize = true;
            SpawnSpecificLootL.ForeColor = Color.FromArgb(220, 220, 220);
            SpawnSpecificLootL.Location = new Point(11, 83);
            SpawnSpecificLootL.Margin = new Padding(4, 0, 4, 0);
            SpawnSpecificLootL.Name = "SpawnSpecificLootL";
            SpawnSpecificLootL.Size = new Size(133, 15);
            SpawnSpecificLootL.TabIndex = 30;
            SpawnSpecificLootL.Text = "Spawn Specific Loot";
            //
            // SpawnSpecificLootCB
            //
            SpawnSpecificLootCB.AutoSize = true;
            SpawnSpecificLootCB.Location = new Point(190, 83);
            SpawnSpecificLootCB.Margin = new Padding(4, 3, 4, 3);
            SpawnSpecificLootCB.Name = "SpawnSpecificLootCB";
            SpawnSpecificLootCB.Size = new Size(15, 14);
            SpawnSpecificLootCB.TabIndex = 31;
            SpawnSpecificLootCB.UseVisualStyleBackColor = true;
            SpawnSpecificLootCB.CheckedChanged += SpawnSpecificLootCB_CheckedChanged;
            //
            // SpawnInfectedL
            //
            SpawnInfectedL.AutoSize = true;
            SpawnInfectedL.ForeColor = Color.FromArgb(220, 220, 220);
            SpawnInfectedL.Location = new Point(11, 25);
            SpawnInfectedL.Margin = new Padding(4, 0, 4, 0);
            SpawnInfectedL.Name = "SpawnInfectedL";
            SpawnInfectedL.Size = new Size(98, 15);
            SpawnInfectedL.TabIndex = 33;
            SpawnInfectedL.Text = "Spawn Infected";
            //
            // SpawnInfectedCB
            //
            SpawnInfectedCB.AutoSize = true;
            SpawnInfectedCB.Location = new Point(190, 25);
            SpawnInfectedCB.Margin = new Padding(4, 3, 4, 3);
            SpawnInfectedCB.Name = "SpawnInfectedCB";
            SpawnInfectedCB.Size = new Size(15, 14);
            SpawnInfectedCB.TabIndex = 34;
            SpawnInfectedCB.UseVisualStyleBackColor = true;
            SpawnInfectedCB.CheckedChanged += SpawnInfectedCB_CheckedChanged;
            //
            // CapareKeyCardLocationControl
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
            Controls.Add(groupBox3);
            Controls.Add(groupBox4);
            ForeColor = SystemColors.Control;
            Name = "CapareKeyCardLocationControl";
            Size = new Size(435, 600);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)building_xNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)building_yNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)building_zNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)door_indexNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)AutomaticCloseTimerNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumberOfLootPointsNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label building_xL;
        private NumericUpDown building_xNUD;
        private Label building_yL;
        private NumericUpDown building_yNUD;
        private Label building_zL;
        private NumericUpDown building_zNUD;
        private Label BuildingClassNameL;
        private TextBox BuildingClassNameTB;
        private Label door_indexL;
        private NumericUpDown door_indexNUD;
        private Label KeycardClassNameL;
        private ComboBox KeycardClassNameComboBox;
        private Label SingleUseL;
        private CheckBox SingleUseCB;
        private Label AutomaticCloseTimerL;
        private NumericUpDown AutomaticCloseTimerNUD;
        private Label CloseWarningL;
        private CheckBox CloseWarningCB;
        private GroupBox groupBox2;
        private Label UseAlarmL;
        private CheckBox UseAlarmCB;
        private Label AlarmSoundL;
        private TextBox AlarmSoundTB;
        private Label AlarmMapwideL;
        private CheckBox AlarmMapwideCB;
        private GroupBox groupBox3;
        private Label SpawnLootOnInitL;
        private CheckBox SpawnLootOnInitCB;
        private Label NumberOfLootPointsL;
        private NumericUpDown NumberOfLootPointsNUD;
        private Label SpawnSpecificLootL;
        private CheckBox SpawnSpecificLootCB;
        private GroupBox groupBox4;
        private Label SpawnInfectedL;
        private CheckBox SpawnInfectedCB;
    }
}
