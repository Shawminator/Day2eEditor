namespace EconomyPlugin
{
    partial class cfggameplayVehicleDataControl
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
            groupBox95 = new GroupBox();
            boatDecayMultiplierNUD = new NumericUpDown();
            label198 = new Label();
            groupBox95.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)boatDecayMultiplierNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox95
            // 
            groupBox95.Controls.Add(boatDecayMultiplierNUD);
            groupBox95.Controls.Add(label198);
            groupBox95.ForeColor = SystemColors.Control;
            groupBox95.Location = new Point(0, 0);
            groupBox95.Margin = new Padding(4, 3, 4, 3);
            groupBox95.Name = "groupBox95";
            groupBox95.Padding = new Padding(4, 3, 4, 3);
            groupBox95.Size = new Size(414, 46);
            groupBox95.TabIndex = 88;
            groupBox95.TabStop = false;
            groupBox95.Text = "Vehicle Data";
            // 
            // boatDecayMultiplierNUD
            // 
            boatDecayMultiplierNUD.BackColor = Color.FromArgb(60, 63, 65);
            boatDecayMultiplierNUD.DecimalPlaces = 2;
            boatDecayMultiplierNUD.ForeColor = SystemColors.Control;
            boatDecayMultiplierNUD.Location = new Point(275, 15);
            boatDecayMultiplierNUD.Margin = new Padding(4, 3, 4, 3);
            boatDecayMultiplierNUD.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            boatDecayMultiplierNUD.Name = "boatDecayMultiplierNUD";
            boatDecayMultiplierNUD.Size = new Size(113, 23);
            boatDecayMultiplierNUD.TabIndex = 70;
            boatDecayMultiplierNUD.TextAlign = HorizontalAlignment.Center;
            boatDecayMultiplierNUD.ValueChanged += boatDecayMultiplierNUD_ValueChanged;
            // 
            // label198
            // 
            label198.AutoSize = true;
            label198.ForeColor = SystemColors.Control;
            label198.Location = new Point(8, 20);
            label198.Margin = new Padding(4, 0, 4, 0);
            label198.Name = "label198";
            label198.Size = new Size(120, 15);
            label198.TabIndex = 69;
            label198.Text = "boat Decay Multiplier";
            // 
            // cfggameplayVehicleDataControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox95);
            ForeColor = SystemColors.Control;
            Name = "cfggameplayVehicleDataControl";
            Size = new Size(423, 58);
            groupBox95.ResumeLayout(false);
            groupBox95.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)boatDecayMultiplierNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox95;
        private NumericUpDown boatDecayMultiplierNUD;
        private Label label198;
    }
}
