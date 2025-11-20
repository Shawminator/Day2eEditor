namespace ExpansionPlugin
{
    partial class ExpansionGeneralLightsControl
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
            groupBox35 = new GroupBox();
            label1 = new Label();
            LampSelectionModeCB = new ComboBox();
            darkLabel301 = new Label();
            numericUpDown34 = new NumericUpDown();
            darkLabel90 = new Label();
            EnableGeneratorsCB = new CheckBox();
            EnableLighthousesCB = new CheckBox();
            EnableLampsComboBox = new ComboBox();
            groupBox35.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown34).BeginInit();
            SuspendLayout();
            // 
            // groupBox35
            // 
            groupBox35.Controls.Add(label1);
            groupBox35.Controls.Add(LampSelectionModeCB);
            groupBox35.Controls.Add(darkLabel301);
            groupBox35.Controls.Add(numericUpDown34);
            groupBox35.Controls.Add(darkLabel90);
            groupBox35.Controls.Add(EnableGeneratorsCB);
            groupBox35.Controls.Add(EnableLighthousesCB);
            groupBox35.Controls.Add(EnableLampsComboBox);
            groupBox35.ForeColor = SystemColors.Control;
            groupBox35.Location = new Point(0, 0);
            groupBox35.Margin = new Padding(4, 3, 4, 3);
            groupBox35.Name = "groupBox35";
            groupBox35.Padding = new Padding(4, 3, 4, 3);
            groupBox35.Size = new Size(382, 186);
            groupBox35.TabIndex = 16;
            groupBox35.TabStop = false;
            groupBox35.Text = "Lights";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(220, 220, 220);
            label1.Location = new Point(23, 146);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(122, 15);
            label1.TabIndex = 8;
            label1.Text = "Lamp Selection Mode";
            // 
            // LampSelectionModeCB
            // 
            LampSelectionModeCB.BackColor = Color.FromArgb(60, 63, 65);
            LampSelectionModeCB.ForeColor = SystemColors.Control;
            LampSelectionModeCB.FormattingEnabled = true;
            LampSelectionModeCB.Location = new Point(177, 146);
            LampSelectionModeCB.Margin = new Padding(4, 3, 4, 3);
            LampSelectionModeCB.Name = "LampSelectionModeCB";
            LampSelectionModeCB.Size = new Size(188, 23);
            LampSelectionModeCB.TabIndex = 7;
            LampSelectionModeCB.SelectedIndexChanged += LampSelectionModeCB_SelectedIndexChanged;
            // 
            // darkLabel301
            // 
            darkLabel301.AutoSize = true;
            darkLabel301.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel301.Location = new Point(22, 116);
            darkLabel301.Margin = new Padding(4, 0, 4, 0);
            darkLabel301.Name = "darkLabel301";
            darkLabel301.Size = new Size(132, 15);
            darkLabel301.TabIndex = 6;
            darkLabel301.Text = "Lamp Amount One In X";
            // 
            // numericUpDown34
            // 
            numericUpDown34.BackColor = Color.FromArgb(60, 63, 65);
            numericUpDown34.ForeColor = SystemColors.Control;
            numericUpDown34.Location = new Point(177, 114);
            numericUpDown34.Margin = new Padding(4, 3, 4, 3);
            numericUpDown34.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            numericUpDown34.Name = "numericUpDown34";
            numericUpDown34.Size = new Size(122, 23);
            numericUpDown34.TabIndex = 5;
            numericUpDown34.TextAlign = HorizontalAlignment.Center;
            numericUpDown34.ValueChanged += numericUpDown34_ValueChanged;
            // 
            // darkLabel90
            // 
            darkLabel90.AutoSize = true;
            darkLabel90.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel90.Location = new Point(23, 29);
            darkLabel90.Margin = new Padding(4, 0, 4, 0);
            darkLabel90.Name = "darkLabel90";
            darkLabel90.Size = new Size(80, 15);
            darkLabel90.TabIndex = 0;
            darkLabel90.Text = "Enable Lamps";
            // 
            // EnableGeneratorsCB
            // 
            EnableGeneratorsCB.AutoSize = true;
            EnableGeneratorsCB.Enabled = false;
            EnableGeneratorsCB.Location = new Point(23, 55);
            EnableGeneratorsCB.Margin = new Padding(4, 3, 4, 3);
            EnableGeneratorsCB.Name = "EnableGeneratorsCB";
            EnableGeneratorsCB.Size = new Size(121, 19);
            EnableGeneratorsCB.TabIndex = 2;
            EnableGeneratorsCB.Text = "Enable Generators";
            EnableGeneratorsCB.UseVisualStyleBackColor = true;
            EnableGeneratorsCB.CheckedChanged += EnableGeneratorsCB_CheckedChanged;
            // 
            // EnableLighthousesCB
            // 
            EnableLighthousesCB.AutoSize = true;
            EnableLighthousesCB.Location = new Point(22, 90);
            EnableLighthousesCB.Margin = new Padding(4, 3, 4, 3);
            EnableLighthousesCB.Name = "EnableLighthousesCB";
            EnableLighthousesCB.Size = new Size(128, 19);
            EnableLighthousesCB.TabIndex = 3;
            EnableLighthousesCB.Text = "Enable Lighthouses";
            EnableLighthousesCB.UseVisualStyleBackColor = true;
            EnableLighthousesCB.CheckedChanged += EnableLighthousesCB_CheckedChanged;
            // 
            // EnableLampsComboBox
            // 
            EnableLampsComboBox.BackColor = Color.FromArgb(60, 63, 65);
            EnableLampsComboBox.ForeColor = SystemColors.Control;
            EnableLampsComboBox.FormattingEnabled = true;
            EnableLampsComboBox.Location = new Point(177, 26);
            EnableLampsComboBox.Margin = new Padding(4, 3, 4, 3);
            EnableLampsComboBox.Name = "EnableLampsComboBox";
            EnableLampsComboBox.Size = new Size(188, 23);
            EnableLampsComboBox.TabIndex = 1;
            EnableLampsComboBox.SelectedIndexChanged += EnableLampsComboBox_SelectedIndexChanged;
            // 
            // ExpansionGeneralLightsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox35);
            ForeColor = SystemColors.Control;
            Name = "ExpansionGeneralLightsControl";
            Size = new Size(382, 186);
            groupBox35.ResumeLayout(false);
            groupBox35.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown34).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox35;
        private ComboBox LampSelectionModeCB;
        private Label darkLabel301;
        private NumericUpDown numericUpDown34;
        private Label darkLabel90;
        private CheckBox EnableGeneratorsCB;
        private CheckBox EnableLighthousesCB;
        private ComboBox EnableLampsComboBox;
        private Label label1;
    }
}
