namespace EconomyPlugin
{
    partial class RandomPresetItemControl
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
            economyGB = new GroupBox();
            darkButton60 = new Button();
            RandomPresetItemNameTB = new TextBox();
            RandomPresetItemChanceNUD = new NumericUpDown();
            label23 = new Label();
            economyGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)RandomPresetItemChanceNUD).BeginInit();
            SuspendLayout();
            // 
            // economyGB
            // 
            economyGB.Controls.Add(darkButton60);
            economyGB.Controls.Add(RandomPresetItemNameTB);
            economyGB.Controls.Add(RandomPresetItemChanceNUD);
            economyGB.Controls.Add(label23);
            economyGB.ForeColor = SystemColors.Control;
            economyGB.Location = new Point(0, 0);
            economyGB.Name = "economyGB";
            economyGB.Size = new Size(246, 115);
            economyGB.TabIndex = 0;
            economyGB.TabStop = false;
            economyGB.Text = "Item";
            // 
            // darkButton60
            // 
            darkButton60.FlatStyle = FlatStyle.Flat;
            darkButton60.Location = new Point(15, 80);
            darkButton60.Margin = new Padding(4, 3, 4, 3);
            darkButton60.Name = "darkButton60";
            darkButton60.Size = new Size(218, 25);
            darkButton60.TabIndex = 81;
            darkButton60.Text = "Change Item";
            darkButton60.Click += darkButton60_Click;
            // 
            // RandomPresetItemNameTB
            // 
            RandomPresetItemNameTB.BackColor = Color.FromArgb(60, 63, 65);
            RandomPresetItemNameTB.ForeColor = SystemColors.Control;
            RandomPresetItemNameTB.Location = new Point(15, 22);
            RandomPresetItemNameTB.Margin = new Padding(4, 3, 4, 3);
            RandomPresetItemNameTB.Name = "RandomPresetItemNameTB";
            RandomPresetItemNameTB.Size = new Size(218, 23);
            RandomPresetItemNameTB.TabIndex = 61;
            RandomPresetItemNameTB.TextChanged += RandomPresetItemNameTB_TextChanged;
            // 
            // RandomPresetItemChanceNUD
            // 
            RandomPresetItemChanceNUD.BackColor = Color.FromArgb(60, 63, 65);
            RandomPresetItemChanceNUD.DecimalPlaces = 2;
            RandomPresetItemChanceNUD.ForeColor = SystemColors.Control;
            RandomPresetItemChanceNUD.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            RandomPresetItemChanceNUD.Location = new Point(117, 51);
            RandomPresetItemChanceNUD.Margin = new Padding(4, 3, 4, 3);
            RandomPresetItemChanceNUD.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            RandomPresetItemChanceNUD.Name = "RandomPresetItemChanceNUD";
            RandomPresetItemChanceNUD.Size = new Size(116, 23);
            RandomPresetItemChanceNUD.TabIndex = 63;
            RandomPresetItemChanceNUD.TextAlign = HorizontalAlignment.Center;
            RandomPresetItemChanceNUD.ValueChanged += RandomPresetItemChanceNUD_ValueChanged;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.ForeColor = SystemColors.Control;
            label23.Location = new Point(15, 53);
            label23.Margin = new Padding(4, 0, 4, 0);
            label23.Name = "label23";
            label23.Size = new Size(82, 15);
            label23.TabIndex = 62;
            label23.Text = "Preset Chance";
            // 
            // RandomPresetItemControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(economyGB);
            ForeColor = SystemColors.Control;
            Name = "RandomPresetItemControl";
            Size = new Size(252, 125);
            economyGB.ResumeLayout(false);
            economyGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)RandomPresetItemChanceNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox economyGB;
        private TextBox RandomPresetItemNameTB;
        private NumericUpDown RandomPresetItemChanceNUD;
        private Label label23;
        private Button darkButton60;
    }
}
