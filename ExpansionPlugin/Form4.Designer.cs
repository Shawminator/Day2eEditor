namespace DayZeEditor
{
    partial class Form4
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox95 = new GroupBox();
            SpawnLoadoutsLB = new ListBox();
            groupBox66 = new GroupBox();
            darkLabel172 = new Label();
            MaleChanceNUD = new NumericUpDown();
            darkLabel173 = new Label();
            MaleNameTB = new TextBox();
            darkButton64 = new Button();
            MaleLoadoutLB = new ListBox();
            PrimaryWeaponGB = new GroupBox();
            button1 = new Button();
            darkLabel152 = new Label();
            PrimaryWeaponQuantityNUD = new NumericUpDown();
            darkLabel153 = new Label();
            PrimaryWeaponNameTB = new TextBox();
            button2 = new Button();
            groupBox95.SuspendLayout();
            groupBox66.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MaleChanceNUD).BeginInit();
            PrimaryWeaponGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PrimaryWeaponQuantityNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox95
            // 
            groupBox95.Controls.Add(button2);
            groupBox95.Controls.Add(SpawnLoadoutsLB);
            groupBox95.ForeColor = SystemColors.Control;
            groupBox95.Location = new Point(426, 26);
            groupBox95.Margin = new Padding(4, 3, 4, 3);
            groupBox95.Name = "groupBox95";
            groupBox95.Padding = new Padding(4, 3, 4, 3);
            groupBox95.Size = new Size(330, 439);
            groupBox95.TabIndex = 4;
            groupBox95.TabStop = false;
            groupBox95.Text = "Loadouts";
            groupBox95.Visible = false;
            // 
            // SpawnLoadoutsLB
            // 
            SpawnLoadoutsLB.BackColor = Color.FromArgb(60, 63, 65);
            SpawnLoadoutsLB.DrawMode = DrawMode.OwnerDrawFixed;
            SpawnLoadoutsLB.ForeColor = SystemColors.Control;
            SpawnLoadoutsLB.FormattingEnabled = true;
            SpawnLoadoutsLB.Location = new Point(7, 22);
            SpawnLoadoutsLB.Margin = new Padding(4, 3, 4, 3);
            SpawnLoadoutsLB.Name = "SpawnLoadoutsLB";
            SpawnLoadoutsLB.Size = new Size(316, 372);
            SpawnLoadoutsLB.TabIndex = 0;
            // 
            // groupBox66
            // 
            groupBox66.Controls.Add(darkLabel172);
            groupBox66.Controls.Add(MaleChanceNUD);
            groupBox66.Controls.Add(darkLabel173);
            groupBox66.Controls.Add(MaleNameTB);
            groupBox66.Controls.Add(darkButton64);
            groupBox66.Controls.Add(MaleLoadoutLB);
            groupBox66.ForeColor = SystemColors.Control;
            groupBox66.Location = new Point(778, 26);
            groupBox66.Margin = new Padding(4, 3, 4, 3);
            groupBox66.Name = "groupBox66";
            groupBox66.Padding = new Padding(4, 3, 4, 3);
            groupBox66.Size = new Size(330, 363);
            groupBox66.TabIndex = 5;
            groupBox66.TabStop = false;
            groupBox66.Text = "Male Loadouts";
            groupBox66.Visible = false;
            // 
            // darkLabel172
            // 
            darkLabel172.AutoSize = true;
            darkLabel172.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel172.Location = new Point(7, 309);
            darkLabel172.Margin = new Padding(4, 0, 4, 0);
            darkLabel172.Name = "darkLabel172";
            darkLabel172.Size = new Size(47, 15);
            darkLabel172.TabIndex = 5;
            darkLabel172.Text = "Chance";
            // 
            // MaleChanceNUD
            // 
            MaleChanceNUD.BackColor = Color.FromArgb(60, 63, 65);
            MaleChanceNUD.DecimalPlaces = 2;
            MaleChanceNUD.ForeColor = SystemColors.Control;
            MaleChanceNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            MaleChanceNUD.Location = new Point(7, 328);
            MaleChanceNUD.Margin = new Padding(4, 3, 4, 3);
            MaleChanceNUD.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            MaleChanceNUD.Name = "MaleChanceNUD";
            MaleChanceNUD.Size = new Size(127, 23);
            MaleChanceNUD.TabIndex = 6;
            MaleChanceNUD.Tag = "SpawnSelectionScreenMenuID";
            MaleChanceNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // darkLabel173
            // 
            darkLabel173.AutoSize = true;
            darkLabel173.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel173.Location = new Point(7, 263);
            darkLabel173.Margin = new Padding(4, 0, 4, 0);
            darkLabel173.Name = "darkLabel173";
            darkLabel173.Size = new Size(39, 15);
            darkLabel173.TabIndex = 3;
            darkLabel173.Text = "Name";
            // 
            // MaleNameTB
            // 
            MaleNameTB.BackColor = Color.FromArgb(60, 63, 65);
            MaleNameTB.ForeColor = SystemColors.Control;
            MaleNameTB.Location = new Point(7, 282);
            MaleNameTB.Margin = new Padding(4, 3, 4, 3);
            MaleNameTB.Name = "MaleNameTB";
            MaleNameTB.ReadOnly = true;
            MaleNameTB.Size = new Size(241, 23);
            MaleNameTB.TabIndex = 4;
            // 
            // darkButton64
            // 
            darkButton64.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            darkButton64.Location = new Point(181, 232);
            darkButton64.Margin = new Padding(4, 3, 4, 3);
            darkButton64.Name = "darkButton64";
            darkButton64.Size = new Size(142, 27);
            darkButton64.TabIndex = 2;
            darkButton64.Text = "Remove";
            // 
            // MaleLoadoutLB
            // 
            MaleLoadoutLB.BackColor = Color.FromArgb(60, 63, 65);
            MaleLoadoutLB.DrawMode = DrawMode.OwnerDrawFixed;
            MaleLoadoutLB.ForeColor = SystemColors.Control;
            MaleLoadoutLB.FormattingEnabled = true;
            MaleLoadoutLB.Location = new Point(7, 22);
            MaleLoadoutLB.Margin = new Padding(4, 3, 4, 3);
            MaleLoadoutLB.Name = "MaleLoadoutLB";
            MaleLoadoutLB.Size = new Size(316, 196);
            MaleLoadoutLB.TabIndex = 0;
            // 
            // PrimaryWeaponGB
            // 
            PrimaryWeaponGB.Controls.Add(button1);
            PrimaryWeaponGB.Controls.Add(darkLabel152);
            PrimaryWeaponGB.Controls.Add(PrimaryWeaponQuantityNUD);
            PrimaryWeaponGB.Controls.Add(darkLabel153);
            PrimaryWeaponGB.Controls.Add(PrimaryWeaponNameTB);
            PrimaryWeaponGB.ForeColor = SystemColors.Control;
            PrimaryWeaponGB.Location = new Point(23, 26);
            PrimaryWeaponGB.Margin = new Padding(4, 3, 4, 3);
            PrimaryWeaponGB.Name = "PrimaryWeaponGB";
            PrimaryWeaponGB.Padding = new Padding(4, 3, 4, 3);
            PrimaryWeaponGB.Size = new Size(395, 112);
            PrimaryWeaponGB.TabIndex = 7;
            PrimaryWeaponGB.TabStop = false;
            PrimaryWeaponGB.Text = "Gear Item";
            PrimaryWeaponGB.Visible = false;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(305, 30);
            button1.Margin = new Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new Size(82, 27);
            button1.TabIndex = 5;
            button1.Text = "Change";
            // 
            // darkLabel152
            // 
            darkLabel152.AutoSize = true;
            darkLabel152.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel152.Location = new Point(7, 60);
            darkLabel152.Margin = new Padding(4, 0, 4, 0);
            darkLabel152.Name = "darkLabel152";
            darkLabel152.Size = new Size(53, 15);
            darkLabel152.TabIndex = 3;
            darkLabel152.Text = "Quantity";
            // 
            // PrimaryWeaponQuantityNUD
            // 
            PrimaryWeaponQuantityNUD.BackColor = Color.FromArgb(60, 63, 65);
            PrimaryWeaponQuantityNUD.ForeColor = SystemColors.Control;
            PrimaryWeaponQuantityNUD.Location = new Point(7, 78);
            PrimaryWeaponQuantityNUD.Margin = new Padding(4, 3, 4, 3);
            PrimaryWeaponQuantityNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            PrimaryWeaponQuantityNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            PrimaryWeaponQuantityNUD.Name = "PrimaryWeaponQuantityNUD";
            PrimaryWeaponQuantityNUD.Size = new Size(127, 23);
            PrimaryWeaponQuantityNUD.TabIndex = 4;
            PrimaryWeaponQuantityNUD.Tag = "SpawnSelectionScreenMenuID";
            PrimaryWeaponQuantityNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // darkLabel153
            // 
            darkLabel153.AutoSize = true;
            darkLabel153.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel153.Location = new Point(7, 14);
            darkLabel153.Margin = new Padding(4, 0, 4, 0);
            darkLabel153.Name = "darkLabel153";
            darkLabel153.Size = new Size(39, 15);
            darkLabel153.TabIndex = 0;
            darkLabel153.Text = "Name";
            // 
            // PrimaryWeaponNameTB
            // 
            PrimaryWeaponNameTB.BackColor = Color.FromArgb(60, 63, 65);
            PrimaryWeaponNameTB.ForeColor = SystemColors.Control;
            PrimaryWeaponNameTB.Location = new Point(8, 32);
            PrimaryWeaponNameTB.Margin = new Padding(4, 3, 4, 3);
            PrimaryWeaponNameTB.Name = "PrimaryWeaponNameTB";
            PrimaryWeaponNameTB.ReadOnly = true;
            PrimaryWeaponNameTB.Size = new Size(289, 23);
            PrimaryWeaponNameTB.TabIndex = 1;
            // 
            // button2
            // 
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(8, 401);
            button2.Margin = new Padding(4, 3, 4, 3);
            button2.Name = "button2";
            button2.Size = new Size(314, 27);
            button2.TabIndex = 2;
            button2.Text = "Add Loadout";
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(1390, 853);
            Controls.Add(PrimaryWeaponGB);
            Controls.Add(groupBox66);
            Controls.Add(groupBox95);
            ForeColor = SystemColors.Control;
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form4";
            Text = "Form4";
            groupBox95.ResumeLayout(false);
            groupBox66.ResumeLayout(false);
            groupBox66.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)MaleChanceNUD).EndInit();
            PrimaryWeaponGB.ResumeLayout(false);
            PrimaryWeaponGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PrimaryWeaponQuantityNUD).EndInit();
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox95;
        private System.Windows.Forms.ListBox SpawnLoadoutsLB;
        private System.Windows.Forms.GroupBox groupBox66;
        private System.Windows.Forms.Label darkLabel172;
        private System.Windows.Forms.NumericUpDown MaleChanceNUD;
        private System.Windows.Forms.Label darkLabel173;
        private System.Windows.Forms.TextBox MaleNameTB;
        private System.Windows.Forms.Button darkButton63;
        private System.Windows.Forms.Button darkButton64;
        private System.Windows.Forms.ListBox MaleLoadoutLB;
        private System.Windows.Forms.GroupBox PrimaryWeaponGB;
        private System.Windows.Forms.Label darkLabel152;
        private System.Windows.Forms.NumericUpDown PrimaryWeaponQuantityNUD;
        private System.Windows.Forms.Label darkLabel153;
        private System.Windows.Forms.TextBox PrimaryWeaponNameTB;
        private Button button1;
        private Button button2;
    }
}