namespace ExpansionPlugin
{
    partial class ExpansionStartingGearItemControl
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
            PrimaryWeaponGB = new GroupBox();
            button1 = new Button();
            darkLabel152 = new Label();
            GearItemQuantityNUD = new NumericUpDown();
            darkLabel153 = new Label();
            GearItemNameTB = new TextBox();
            PrimaryWeaponGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GearItemQuantityNUD).BeginInit();
            SuspendLayout();
            // 
            // PrimaryWeaponGB
            // 
            PrimaryWeaponGB.Controls.Add(button1);
            PrimaryWeaponGB.Controls.Add(darkLabel152);
            PrimaryWeaponGB.Controls.Add(GearItemQuantityNUD);
            PrimaryWeaponGB.Controls.Add(darkLabel153);
            PrimaryWeaponGB.Controls.Add(GearItemNameTB);
            PrimaryWeaponGB.ForeColor = SystemColors.Control;
            PrimaryWeaponGB.Location = new Point(0, 0);
            PrimaryWeaponGB.Margin = new Padding(4, 3, 4, 3);
            PrimaryWeaponGB.Name = "PrimaryWeaponGB";
            PrimaryWeaponGB.Padding = new Padding(4, 3, 4, 3);
            PrimaryWeaponGB.Size = new Size(473, 86);
            PrimaryWeaponGB.TabIndex = 8;
            PrimaryWeaponGB.TabStop = false;
            PrimaryWeaponGB.Text = "Gear Item";
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(376, 21);
            button1.Margin = new Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new Size(82, 27);
            button1.TabIndex = 5;
            button1.Text = "Change";
            button1.Click += button1_Click;
            // 
            // darkLabel152
            // 
            darkLabel152.AutoSize = true;
            darkLabel152.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel152.Location = new Point(14, 55);
            darkLabel152.Margin = new Padding(4, 0, 4, 0);
            darkLabel152.Name = "darkLabel152";
            darkLabel152.Size = new Size(53, 15);
            darkLabel152.TabIndex = 3;
            darkLabel152.Text = "Quantity";
            // 
            // GearItemQuantityNUD
            // 
            GearItemQuantityNUD.BackColor = Color.FromArgb(60, 63, 65);
            GearItemQuantityNUD.ForeColor = SystemColors.Control;
            GearItemQuantityNUD.Location = new Point(79, 51);
            GearItemQuantityNUD.Margin = new Padding(4, 3, 4, 3);
            GearItemQuantityNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            GearItemQuantityNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            GearItemQuantityNUD.Name = "GearItemQuantityNUD";
            GearItemQuantityNUD.Size = new Size(127, 23);
            GearItemQuantityNUD.TabIndex = 4;
            GearItemQuantityNUD.Tag = "SpawnSelectionScreenMenuID";
            GearItemQuantityNUD.TextAlign = HorizontalAlignment.Center;
            GearItemQuantityNUD.ValueChanged += GearItemQuantityNUD_ValueChanged;
            // 
            // darkLabel153
            // 
            darkLabel153.AutoSize = true;
            darkLabel153.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel153.Location = new Point(14, 25);
            darkLabel153.Margin = new Padding(4, 0, 4, 0);
            darkLabel153.Name = "darkLabel153";
            darkLabel153.Size = new Size(39, 15);
            darkLabel153.TabIndex = 0;
            darkLabel153.Text = "Name";
            // 
            // GearItemNameTB
            // 
            GearItemNameTB.BackColor = Color.FromArgb(60, 63, 65);
            GearItemNameTB.ForeColor = SystemColors.Control;
            GearItemNameTB.Location = new Point(79, 22);
            GearItemNameTB.Margin = new Padding(4, 3, 4, 3);
            GearItemNameTB.Name = "GearItemNameTB";
            GearItemNameTB.ReadOnly = true;
            GearItemNameTB.Size = new Size(289, 23);
            GearItemNameTB.TabIndex = 1;
            // 
            // ExpansionStartingGearItemControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(PrimaryWeaponGB);
            ForeColor = SystemColors.Control;
            Name = "ExpansionStartingGearItemControl";
            Size = new Size(473, 86);
            PrimaryWeaponGB.ResumeLayout(false);
            PrimaryWeaponGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)GearItemQuantityNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox PrimaryWeaponGB;
        private Button button1;
        private Label darkLabel152;
        private NumericUpDown GearItemQuantityNUD;
        private Label darkLabel153;
        private TextBox GearItemNameTB;
    }
}
