namespace CaparePlugin
{
    partial class CapareLPGeneralControl
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
            NumberOfExtraMagsForDefinedWeaponsL = new Label();
            NumberOfExtraMagsForDefinedWeaponsNUD = new NumericUpDown();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumberOfExtraMagsForDefinedWeaponsNUD).BeginInit();
            SuspendLayout();
            //
            // groupBox1
            //
            groupBox1.Controls.Add(NumberOfExtraMagsForDefinedWeaponsL);
            groupBox1.Controls.Add(NumberOfExtraMagsForDefinedWeaponsNUD);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(435, 57);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "General";
            //
            // NumberOfExtraMagsForDefinedWeaponsL
            //
            NumberOfExtraMagsForDefinedWeaponsL.AutoSize = true;
            NumberOfExtraMagsForDefinedWeaponsL.ForeColor = Color.FromArgb(220, 220, 220);
            NumberOfExtraMagsForDefinedWeaponsL.Location = new Point(11, 25);
            NumberOfExtraMagsForDefinedWeaponsL.Margin = new Padding(4, 0, 4, 0);
            NumberOfExtraMagsForDefinedWeaponsL.Name = "NumberOfExtraMagsForDefinedWeaponsL";
            NumberOfExtraMagsForDefinedWeaponsL.Size = new Size(210, 15);
            NumberOfExtraMagsForDefinedWeaponsL.TabIndex = 0;
            NumberOfExtraMagsForDefinedWeaponsL.Text = "Extra Mags For Defined Weapons";
            //
            // NumberOfExtraMagsForDefinedWeaponsNUD
            //
            NumberOfExtraMagsForDefinedWeaponsNUD.BackColor = Color.FromArgb(60, 63, 65);
            NumberOfExtraMagsForDefinedWeaponsNUD.ForeColor = SystemColors.Control;
            NumberOfExtraMagsForDefinedWeaponsNUD.Location = new Point(190, 22);
            NumberOfExtraMagsForDefinedWeaponsNUD.Margin = new Padding(4, 3, 4, 3);
            NumberOfExtraMagsForDefinedWeaponsNUD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            NumberOfExtraMagsForDefinedWeaponsNUD.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            NumberOfExtraMagsForDefinedWeaponsNUD.Name = "NumberOfExtraMagsForDefinedWeaponsNUD";
            NumberOfExtraMagsForDefinedWeaponsNUD.Size = new Size(230, 23);
            NumberOfExtraMagsForDefinedWeaponsNUD.TabIndex = 1;
            NumberOfExtraMagsForDefinedWeaponsNUD.TextAlign = HorizontalAlignment.Center;
            NumberOfExtraMagsForDefinedWeaponsNUD.ValueChanged += NumberOfExtraMagsForDefinedWeaponsNUD_ValueChanged;
            //
            // CapareLPGeneralControl
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "CapareLPGeneralControl";
            Size = new Size(435, 63);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NumberOfExtraMagsForDefinedWeaponsNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label NumberOfExtraMagsForDefinedWeaponsL;
        private NumericUpDown NumberOfExtraMagsForDefinedWeaponsNUD;
    }
}
