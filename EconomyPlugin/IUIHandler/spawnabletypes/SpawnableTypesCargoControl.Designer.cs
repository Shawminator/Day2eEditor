namespace EconomyPlugin
{
    partial class SpawnableTypesCargoControl
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
            CargoGB = new GroupBox();
            CargoPresetTB = new TextBox();
            UseCargoChanceCB = new CheckBox();
            CarcgoChanceNUD = new NumericUpDown();
            IsCargoPresetCB = new CheckBox();
            CargoPresetGB = new GroupBox();
            CargoPresetComboBox = new ComboBox();
            darkButton36 = new Button();
            CargoGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)CarcgoChanceNUD).BeginInit();
            CargoPresetGB.SuspendLayout();
            SuspendLayout();
            // 
            // CargoGB
            // 
            CargoGB.Controls.Add(CargoPresetTB);
            CargoGB.Controls.Add(UseCargoChanceCB);
            CargoGB.Controls.Add(CarcgoChanceNUD);
            CargoGB.Controls.Add(IsCargoPresetCB);
            CargoGB.Controls.Add(CargoPresetGB);
            CargoGB.ForeColor = SystemColors.ButtonHighlight;
            CargoGB.Location = new Point(0, 0);
            CargoGB.Margin = new Padding(4, 3, 4, 3);
            CargoGB.Name = "CargoGB";
            CargoGB.Padding = new Padding(4, 3, 4, 3);
            CargoGB.Size = new Size(282, 240);
            CargoGB.TabIndex = 66;
            CargoGB.TabStop = false;
            CargoGB.Text = "Cargo";
            // 
            // CargoPresetTB
            // 
            CargoPresetTB.BackColor = Color.FromArgb(60, 63, 65);
            CargoPresetTB.ForeColor = SystemColors.Control;
            CargoPresetTB.Location = new Point(10, 87);
            CargoPresetTB.Margin = new Padding(4, 3, 4, 3);
            CargoPresetTB.Name = "CargoPresetTB";
            CargoPresetTB.ReadOnly = true;
            CargoPresetTB.Size = new Size(254, 23);
            CargoPresetTB.TabIndex = 70;
            // 
            // UseCargoChanceCB
            // 
            UseCargoChanceCB.AutoSize = true;
            UseCargoChanceCB.ForeColor = SystemColors.Control;
            UseCargoChanceCB.Location = new Point(13, 28);
            UseCargoChanceCB.Margin = new Padding(4, 3, 4, 3);
            UseCargoChanceCB.Name = "UseCargoChanceCB";
            UseCargoChanceCB.Size = new Size(86, 19);
            UseCargoChanceCB.TabIndex = 73;
            UseCargoChanceCB.Text = "Use chance";
            UseCargoChanceCB.UseVisualStyleBackColor = true;
            UseCargoChanceCB.CheckedChanged += UseCargoChanceCB_CheckedChanged;
            // 
            // CarcgoChanceNUD
            // 
            CarcgoChanceNUD.BackColor = Color.FromArgb(60, 63, 65);
            CarcgoChanceNUD.DecimalPlaces = 2;
            CarcgoChanceNUD.ForeColor = SystemColors.Control;
            CarcgoChanceNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            CarcgoChanceNUD.Location = new Point(154, 25);
            CarcgoChanceNUD.Margin = new Padding(4, 3, 4, 3);
            CarcgoChanceNUD.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            CarcgoChanceNUD.Name = "CarcgoChanceNUD";
            CarcgoChanceNUD.Size = new Size(112, 23);
            CarcgoChanceNUD.TabIndex = 64;
            CarcgoChanceNUD.TextAlign = HorizontalAlignment.Center;
            CarcgoChanceNUD.ValueChanged += CarcgoChanceNUD_ValueChanged;
            // 
            // IsCargoPresetCB
            // 
            IsCargoPresetCB.AutoSize = true;
            IsCargoPresetCB.ForeColor = SystemColors.Control;
            IsCargoPresetCB.Location = new Point(13, 62);
            IsCargoPresetCB.Margin = new Padding(4, 3, 4, 3);
            IsCargoPresetCB.Name = "IsCargoPresetCB";
            IsCargoPresetCB.Size = new Size(117, 19);
            IsCargoPresetCB.TabIndex = 62;
            IsCargoPresetCB.Text = "Is Random Preset";
            IsCargoPresetCB.UseVisualStyleBackColor = true;
            IsCargoPresetCB.CheckedChanged += IsCargoPresetCB_CheckedChanged;
            // 
            // CargoPresetGB
            // 
            CargoPresetGB.Controls.Add(CargoPresetComboBox);
            CargoPresetGB.Controls.Add(darkButton36);
            CargoPresetGB.ForeColor = SystemColors.ButtonHighlight;
            CargoPresetGB.Location = new Point(12, 118);
            CargoPresetGB.Margin = new Padding(4, 3, 4, 3);
            CargoPresetGB.Name = "CargoPresetGB";
            CargoPresetGB.Padding = new Padding(4, 3, 4, 3);
            CargoPresetGB.Size = new Size(254, 115);
            CargoPresetGB.TabIndex = 8;
            CargoPresetGB.TabStop = false;
            CargoPresetGB.Text = "Change Cargo Preset";
            // 
            // CargoPresetComboBox
            // 
            CargoPresetComboBox.BackColor = Color.FromArgb(60, 63, 65);
            CargoPresetComboBox.ForeColor = SystemColors.Control;
            CargoPresetComboBox.FormattingEnabled = true;
            CargoPresetComboBox.Location = new Point(12, 29);
            CargoPresetComboBox.Margin = new Padding(4, 3, 4, 3);
            CargoPresetComboBox.Name = "CargoPresetComboBox";
            CargoPresetComboBox.Size = new Size(235, 23);
            CargoPresetComboBox.TabIndex = 1;
            // 
            // darkButton36
            // 
            darkButton36.FlatStyle = FlatStyle.Flat;
            darkButton36.Location = new Point(9, 60);
            darkButton36.Margin = new Padding(4, 3, 4, 3);
            darkButton36.Name = "darkButton36";
            darkButton36.Size = new Size(238, 31);
            darkButton36.TabIndex = 5;
            darkButton36.Text = "Change Preset";
            darkButton36.Click += darkButton36_Click;
            // 
            // SpawnableTypesCargoControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(CargoGB);
            ForeColor = SystemColors.Control;
            Name = "SpawnableTypesCargoControl";
            Size = new Size(293, 248);
            CargoGB.ResumeLayout(false);
            CargoGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)CarcgoChanceNUD).EndInit();
            CargoPresetGB.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox CargoGB;
        private TextBox CargoPresetTB;
        private CheckBox UseCargoChanceCB;
        private NumericUpDown CarcgoChanceNUD;
        private CheckBox IsCargoPresetCB;
        private GroupBox CargoPresetGB;
        private ComboBox CargoPresetComboBox;
        private Button darkButton36;
    }
}
