namespace CaparePlugin
{
    partial class CapareKeyCardScanTargetControl
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
            BuildingClassNameL = new Label();
            BuildingClassNameTB = new TextBox();
            DoorIndexL = new Label();
            DoorIndexNUD = new NumericUpDown();
            AreaNameL = new Label();
            AreaNameTB = new TextBox();
            KeycardClassNameL = new Label();
            KeycardClassNameComboBox = new ComboBox();
            AlarmSoundL = new Label();
            AlarmSoundTB = new TextBox();
            AlarmMapwideL = new Label();
            AlarmMapwideCB = new CheckBox();
            SingleUseL = new Label();
            SingleUseCB = new CheckBox();
            AutomaticCloseTimerL = new Label();
            AutomaticCloseTimerNUD = new NumericUpDown();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DoorIndexNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AutomaticCloseTimerNUD).BeginInit();
            SuspendLayout();
            //
            // groupBox1
            //
            groupBox1.Controls.Add(BuildingClassNameL);
            groupBox1.Controls.Add(BuildingClassNameTB);
            groupBox1.Controls.Add(DoorIndexL);
            groupBox1.Controls.Add(DoorIndexNUD);
            groupBox1.Controls.Add(AreaNameL);
            groupBox1.Controls.Add(AreaNameTB);
            groupBox1.Controls.Add(KeycardClassNameL);
            groupBox1.Controls.Add(KeycardClassNameComboBox);
            groupBox1.Controls.Add(AlarmSoundL);
            groupBox1.Controls.Add(AlarmSoundTB);
            groupBox1.Controls.Add(AlarmMapwideL);
            groupBox1.Controls.Add(AlarmMapwideCB);
            groupBox1.Controls.Add(SingleUseL);
            groupBox1.Controls.Add(SingleUseCB);
            groupBox1.Controls.Add(AutomaticCloseTimerL);
            groupBox1.Controls.Add(AutomaticCloseTimerNUD);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(435, 260);
            groupBox1.TabIndex = 16;
            groupBox1.TabStop = false;
            groupBox1.Text = "Scan Target";
            //
            // BuildingClassNameL
            //
            BuildingClassNameL.AutoSize = true;
            BuildingClassNameL.ForeColor = Color.FromArgb(220, 220, 220);
            BuildingClassNameL.Location = new Point(11, 25);
            BuildingClassNameL.Margin = new Padding(4, 0, 4, 0);
            BuildingClassNameL.Name = "BuildingClassNameL";
            BuildingClassNameL.Size = new Size(133, 15);
            BuildingClassNameL.TabIndex = 0;
            BuildingClassNameL.Text = "Building Class Name";
            //
            // BuildingClassNameTB
            //
            BuildingClassNameTB.BackColor = Color.FromArgb(60, 63, 65);
            BuildingClassNameTB.ForeColor = SystemColors.Control;
            BuildingClassNameTB.Location = new Point(190, 22);
            BuildingClassNameTB.Margin = new Padding(4, 3, 4, 3);
            BuildingClassNameTB.Name = "BuildingClassNameTB";
            BuildingClassNameTB.Size = new Size(230, 23);
            BuildingClassNameTB.TabIndex = 1;
            BuildingClassNameTB.TextChanged += BuildingClassNameTB_TextChanged;
            //
            // DoorIndexL
            //
            DoorIndexL.AutoSize = true;
            DoorIndexL.ForeColor = Color.FromArgb(220, 220, 220);
            DoorIndexL.Location = new Point(11, 54);
            DoorIndexL.Margin = new Padding(4, 0, 4, 0);
            DoorIndexL.Name = "DoorIndexL";
            DoorIndexL.Size = new Size(70, 15);
            DoorIndexL.TabIndex = 2;
            DoorIndexL.Text = "Door Index";
            //
            // DoorIndexNUD
            //
            DoorIndexNUD.BackColor = Color.FromArgb(60, 63, 65);
            DoorIndexNUD.ForeColor = SystemColors.Control;
            DoorIndexNUD.Location = new Point(190, 51);
            DoorIndexNUD.Margin = new Padding(4, 3, 4, 3);
            DoorIndexNUD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            DoorIndexNUD.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            DoorIndexNUD.Name = "DoorIndexNUD";
            DoorIndexNUD.Size = new Size(230, 23);
            DoorIndexNUD.TabIndex = 3;
            DoorIndexNUD.TextAlign = HorizontalAlignment.Center;
            DoorIndexNUD.ValueChanged += DoorIndexNUD_ValueChanged;
            //
            // AreaNameL
            //
            AreaNameL.AutoSize = true;
            AreaNameL.ForeColor = Color.FromArgb(220, 220, 220);
            AreaNameL.Location = new Point(11, 83);
            AreaNameL.Margin = new Padding(4, 0, 4, 0);
            AreaNameL.Name = "AreaNameL";
            AreaNameL.Size = new Size(63, 15);
            AreaNameL.TabIndex = 4;
            AreaNameL.Text = "Area Name";
            //
            // AreaNameTB
            //
            AreaNameTB.BackColor = Color.FromArgb(60, 63, 65);
            AreaNameTB.ForeColor = SystemColors.Control;
            AreaNameTB.Location = new Point(190, 80);
            AreaNameTB.Margin = new Padding(4, 3, 4, 3);
            AreaNameTB.Name = "AreaNameTB";
            AreaNameTB.Size = new Size(230, 23);
            AreaNameTB.TabIndex = 5;
            AreaNameTB.TextChanged += AreaNameTB_TextChanged;
            //
            // KeycardClassNameL
            //
            KeycardClassNameL.AutoSize = true;
            KeycardClassNameL.ForeColor = Color.FromArgb(220, 220, 220);
            KeycardClassNameL.Location = new Point(11, 112);
            KeycardClassNameL.Margin = new Padding(4, 0, 4, 0);
            KeycardClassNameL.Name = "KeycardClassNameL";
            KeycardClassNameL.Size = new Size(126, 15);
            KeycardClassNameL.TabIndex = 6;
            KeycardClassNameL.Text = "Keycard Class Name";
            //
            // KeycardClassNameComboBox
            //
            KeycardClassNameComboBox.BackColor = Color.FromArgb(60, 63, 65);
            KeycardClassNameComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            KeycardClassNameComboBox.FlatStyle = FlatStyle.Flat;
            KeycardClassNameComboBox.ForeColor = SystemColors.Control;
            KeycardClassNameComboBox.FormattingEnabled = true;
            KeycardClassNameComboBox.Location = new Point(190, 109);
            KeycardClassNameComboBox.Margin = new Padding(4, 3, 4, 3);
            KeycardClassNameComboBox.Name = "KeycardClassNameComboBox";
            KeycardClassNameComboBox.Size = new Size(230, 23);
            KeycardClassNameComboBox.TabIndex = 7;
            KeycardClassNameComboBox.TextChanged += KeycardClassNameComboBox_TextChanged;
            //
            // AlarmSoundL
            //
            AlarmSoundL.AutoSize = true;
            AlarmSoundL.ForeColor = Color.FromArgb(220, 220, 220);
            AlarmSoundL.Location = new Point(11, 141);
            AlarmSoundL.Margin = new Padding(4, 0, 4, 0);
            AlarmSoundL.Name = "AlarmSoundL";
            AlarmSoundL.Size = new Size(77, 15);
            AlarmSoundL.TabIndex = 8;
            AlarmSoundL.Text = "Alarm Sound";
            //
            // AlarmSoundTB
            //
            AlarmSoundTB.BackColor = Color.FromArgb(60, 63, 65);
            AlarmSoundTB.ForeColor = SystemColors.Control;
            AlarmSoundTB.Location = new Point(190, 138);
            AlarmSoundTB.Margin = new Padding(4, 3, 4, 3);
            AlarmSoundTB.Name = "AlarmSoundTB";
            AlarmSoundTB.Size = new Size(230, 23);
            AlarmSoundTB.TabIndex = 9;
            AlarmSoundTB.TextChanged += AlarmSoundTB_TextChanged;
            //
            // AlarmMapwideL
            //
            AlarmMapwideL.AutoSize = true;
            AlarmMapwideL.ForeColor = Color.FromArgb(220, 220, 220);
            AlarmMapwideL.Location = new Point(11, 170);
            AlarmMapwideL.Margin = new Padding(4, 0, 4, 0);
            AlarmMapwideL.Name = "AlarmMapwideL";
            AlarmMapwideL.Size = new Size(91, 15);
            AlarmMapwideL.TabIndex = 10;
            AlarmMapwideL.Text = "Alarm Mapwide";
            //
            // AlarmMapwideCB
            //
            AlarmMapwideCB.AutoSize = true;
            AlarmMapwideCB.Location = new Point(190, 170);
            AlarmMapwideCB.Margin = new Padding(4, 3, 4, 3);
            AlarmMapwideCB.Name = "AlarmMapwideCB";
            AlarmMapwideCB.Size = new Size(15, 14);
            AlarmMapwideCB.TabIndex = 11;
            AlarmMapwideCB.UseVisualStyleBackColor = true;
            AlarmMapwideCB.CheckedChanged += AlarmMapwideCB_CheckedChanged;
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
            // CapareKeyCardScanTargetControl
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "CapareKeyCardScanTargetControl";
            Size = new Size(435, 266);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DoorIndexNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)AutomaticCloseTimerNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label BuildingClassNameL;
        private TextBox BuildingClassNameTB;
        private Label DoorIndexL;
        private NumericUpDown DoorIndexNUD;
        private Label AreaNameL;
        private TextBox AreaNameTB;
        private Label KeycardClassNameL;
        private ComboBox KeycardClassNameComboBox;
        private Label AlarmSoundL;
        private TextBox AlarmSoundTB;
        private Label AlarmMapwideL;
        private CheckBox AlarmMapwideCB;
        private Label SingleUseL;
        private CheckBox SingleUseCB;
        private Label AutomaticCloseTimerL;
        private NumericUpDown AutomaticCloseTimerNUD;
    }
}
