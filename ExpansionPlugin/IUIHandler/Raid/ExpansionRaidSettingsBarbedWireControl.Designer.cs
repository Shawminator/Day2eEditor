namespace ExpansionPlugin
{
    partial class ExpansionRaidSettingsBarbedWireControl
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
            groupBox24 = new GroupBox();
            darkLabel68 = new Label();
            BarbedWireRaidToolDamagePercentNUD = new NumericUpDown();
            darkLabel69 = new Label();
            BarbedWireRaidToolCyclesNUD = new NumericUpDown();
            darkLabel73 = new Label();
            BarbedWireRaidToolTimeSecondsNUD = new NumericUpDown();
            CanRaidBarbedWireCB = new CheckBox();
            label1 = new Label();
            groupBox24.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)BarbedWireRaidToolDamagePercentNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BarbedWireRaidToolCyclesNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BarbedWireRaidToolTimeSecondsNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox24
            // 
            groupBox24.Controls.Add(label1);
            groupBox24.Controls.Add(darkLabel68);
            groupBox24.Controls.Add(BarbedWireRaidToolDamagePercentNUD);
            groupBox24.Controls.Add(darkLabel69);
            groupBox24.Controls.Add(BarbedWireRaidToolCyclesNUD);
            groupBox24.Controls.Add(darkLabel73);
            groupBox24.Controls.Add(BarbedWireRaidToolTimeSecondsNUD);
            groupBox24.Controls.Add(CanRaidBarbedWireCB);
            groupBox24.ForeColor = SystemColors.Control;
            groupBox24.Location = new Point(0, 0);
            groupBox24.Margin = new Padding(4, 3, 4, 3);
            groupBox24.Name = "groupBox24";
            groupBox24.Padding = new Padding(4, 3, 4, 3);
            groupBox24.Size = new Size(335, 150);
            groupBox24.TabIndex = 13;
            groupBox24.TabStop = false;
            groupBox24.Text = "Barbed Wire";
            // 
            // darkLabel68
            // 
            darkLabel68.AutoSize = true;
            darkLabel68.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel68.Location = new Point(8, 109);
            darkLabel68.Margin = new Padding(4, 0, 4, 0);
            darkLabel68.Name = "darkLabel68";
            darkLabel68.Size = new Size(206, 15);
            darkLabel68.TabIndex = 9;
            darkLabel68.Text = "BarberWire Raid Tool Damage Percent";
            // 
            // BarbedWireRaidToolDamagePercentNUD
            // 
            BarbedWireRaidToolDamagePercentNUD.BackColor = Color.FromArgb(60, 63, 65);
            BarbedWireRaidToolDamagePercentNUD.DecimalPlaces = 1;
            BarbedWireRaidToolDamagePercentNUD.ForeColor = SystemColors.Control;
            BarbedWireRaidToolDamagePercentNUD.Location = new Point(231, 107);
            BarbedWireRaidToolDamagePercentNUD.Margin = new Padding(4, 3, 4, 3);
            BarbedWireRaidToolDamagePercentNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            BarbedWireRaidToolDamagePercentNUD.Name = "BarbedWireRaidToolDamagePercentNUD";
            BarbedWireRaidToolDamagePercentNUD.Size = new Size(83, 23);
            BarbedWireRaidToolDamagePercentNUD.TabIndex = 10;
            BarbedWireRaidToolDamagePercentNUD.TextAlign = HorizontalAlignment.Center;
            BarbedWireRaidToolDamagePercentNUD.ValueChanged += BarbedWireRaidToolDamagePercentNUD_ValueChanged;
            // 
            // darkLabel69
            // 
            darkLabel69.AutoSize = true;
            darkLabel69.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel69.Location = new Point(8, 79);
            darkLabel69.Margin = new Padding(4, 0, 4, 0);
            darkLabel69.Name = "darkLabel69";
            darkLabel69.Size = new Size(153, 15);
            darkLabel69.TabIndex = 7;
            darkLabel69.Text = "BarberWire Raid Tool Cycles";
            // 
            // BarbedWireRaidToolCyclesNUD
            // 
            BarbedWireRaidToolCyclesNUD.BackColor = Color.FromArgb(60, 63, 65);
            BarbedWireRaidToolCyclesNUD.ForeColor = SystemColors.Control;
            BarbedWireRaidToolCyclesNUD.Location = new Point(231, 77);
            BarbedWireRaidToolCyclesNUD.Margin = new Padding(4, 3, 4, 3);
            BarbedWireRaidToolCyclesNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            BarbedWireRaidToolCyclesNUD.Name = "BarbedWireRaidToolCyclesNUD";
            BarbedWireRaidToolCyclesNUD.Size = new Size(83, 23);
            BarbedWireRaidToolCyclesNUD.TabIndex = 8;
            BarbedWireRaidToolCyclesNUD.TextAlign = HorizontalAlignment.Center;
            BarbedWireRaidToolCyclesNUD.ValueChanged += BarbedWireRaidToolCyclesNUD_ValueChanged;
            // 
            // darkLabel73
            // 
            darkLabel73.AutoSize = true;
            darkLabel73.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel73.Location = new Point(8, 49);
            darkLabel73.Margin = new Padding(4, 0, 4, 0);
            darkLabel73.Name = "darkLabel73";
            darkLabel73.Size = new Size(198, 15);
            darkLabel73.TabIndex = 5;
            darkLabel73.Text = "Barbed Wire Raid Tool Time Seconds";
            // 
            // BarbedWireRaidToolTimeSecondsNUD
            // 
            BarbedWireRaidToolTimeSecondsNUD.BackColor = Color.FromArgb(60, 63, 65);
            BarbedWireRaidToolTimeSecondsNUD.ForeColor = SystemColors.Control;
            BarbedWireRaidToolTimeSecondsNUD.Location = new Point(231, 47);
            BarbedWireRaidToolTimeSecondsNUD.Margin = new Padding(4, 3, 4, 3);
            BarbedWireRaidToolTimeSecondsNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            BarbedWireRaidToolTimeSecondsNUD.Name = "BarbedWireRaidToolTimeSecondsNUD";
            BarbedWireRaidToolTimeSecondsNUD.Size = new Size(83, 23);
            BarbedWireRaidToolTimeSecondsNUD.TabIndex = 6;
            BarbedWireRaidToolTimeSecondsNUD.TextAlign = HorizontalAlignment.Center;
            BarbedWireRaidToolTimeSecondsNUD.ValueChanged += BarbedWireRaidToolTimeSecondsNUD_ValueChanged;
            // 
            // CanRaidBarbedWireCB
            // 
            CanRaidBarbedWireCB.AutoSize = true;
            CanRaidBarbedWireCB.ForeColor = SystemColors.Control;
            CanRaidBarbedWireCB.Location = new Point(231, 22);
            CanRaidBarbedWireCB.Margin = new Padding(4, 3, 4, 3);
            CanRaidBarbedWireCB.Name = "CanRaidBarbedWireCB";
            CanRaidBarbedWireCB.Size = new Size(15, 14);
            CanRaidBarbedWireCB.TabIndex = 0;
            CanRaidBarbedWireCB.TextAlign = ContentAlignment.MiddleRight;
            CanRaidBarbedWireCB.UseVisualStyleBackColor = true;
            CanRaidBarbedWireCB.CheckedChanged += CanRaidBarbedWireCB_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(220, 220, 220);
            label1.Location = new Point(8, 22);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(121, 15);
            label1.TabIndex = 14;
            label1.Text = "Can Raid Barbed Wire";
            // 
            // ExpansionRaidSettingsBarbedWireControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox24);
            ForeColor = SystemColors.Control;
            Name = "ExpansionRaidSettingsBarbedWireControl";
            Size = new Size(335, 150);
            groupBox24.ResumeLayout(false);
            groupBox24.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)BarbedWireRaidToolDamagePercentNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)BarbedWireRaidToolCyclesNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)BarbedWireRaidToolTimeSecondsNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox24;
        private Label darkLabel68;
        private NumericUpDown BarbedWireRaidToolDamagePercentNUD;
        private Label darkLabel69;
        private NumericUpDown BarbedWireRaidToolCyclesNUD;
        private Label darkLabel73;
        private NumericUpDown BarbedWireRaidToolTimeSecondsNUD;
        private CheckBox CanRaidBarbedWireCB;
        private Label label1;
    }
}
