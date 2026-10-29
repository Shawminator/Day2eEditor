namespace CaparePlugin
{
    partial class CapareKeyCardAreaControl
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
            AreaNameL = new Label();
            AreaNameTB = new TextBox();
            MaxInitialLootSpawnsL = new Label();
            MaxInitialLootSpawnsNUD = new NumericUpDown();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MaxInitialLootSpawnsNUD).BeginInit();
            SuspendLayout();
            //
            // groupBox1
            //
            groupBox1.Controls.Add(AreaNameL);
            groupBox1.Controls.Add(AreaNameTB);
            groupBox1.Controls.Add(MaxInitialLootSpawnsL);
            groupBox1.Controls.Add(MaxInitialLootSpawnsNUD);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(435, 86);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Area";
            //
            // AreaNameL
            //
            AreaNameL.AutoSize = true;
            AreaNameL.ForeColor = Color.FromArgb(220, 220, 220);
            AreaNameL.Location = new Point(11, 25);
            AreaNameL.Margin = new Padding(4, 0, 4, 0);
            AreaNameL.Name = "AreaNameL";
            AreaNameL.Size = new Size(63, 15);
            AreaNameL.TabIndex = 0;
            AreaNameL.Text = "Area Name";
            //
            // AreaNameTB
            //
            AreaNameTB.BackColor = Color.FromArgb(60, 63, 65);
            AreaNameTB.ForeColor = SystemColors.Control;
            AreaNameTB.Location = new Point(190, 22);
            AreaNameTB.Margin = new Padding(4, 3, 4, 3);
            AreaNameTB.Name = "AreaNameTB";
            AreaNameTB.Size = new Size(230, 23);
            AreaNameTB.TabIndex = 1;
            AreaNameTB.TextChanged += AreaNameTB_TextChanged;
            //
            // MaxInitialLootSpawnsL
            //
            MaxInitialLootSpawnsL.AutoSize = true;
            MaxInitialLootSpawnsL.ForeColor = Color.FromArgb(220, 220, 220);
            MaxInitialLootSpawnsL.Location = new Point(11, 54);
            MaxInitialLootSpawnsL.Margin = new Padding(4, 0, 4, 0);
            MaxInitialLootSpawnsL.Name = "MaxInitialLootSpawnsL";
            MaxInitialLootSpawnsL.Size = new Size(161, 15);
            MaxInitialLootSpawnsL.TabIndex = 2;
            MaxInitialLootSpawnsL.Text = "Max Initial Loot Spawns";
            //
            // MaxInitialLootSpawnsNUD
            //
            MaxInitialLootSpawnsNUD.BackColor = Color.FromArgb(60, 63, 65);
            MaxInitialLootSpawnsNUD.ForeColor = SystemColors.Control;
            MaxInitialLootSpawnsNUD.Location = new Point(190, 51);
            MaxInitialLootSpawnsNUD.Margin = new Padding(4, 3, 4, 3);
            MaxInitialLootSpawnsNUD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            MaxInitialLootSpawnsNUD.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            MaxInitialLootSpawnsNUD.Name = "MaxInitialLootSpawnsNUD";
            MaxInitialLootSpawnsNUD.Size = new Size(230, 23);
            MaxInitialLootSpawnsNUD.TabIndex = 3;
            MaxInitialLootSpawnsNUD.TextAlign = HorizontalAlignment.Center;
            MaxInitialLootSpawnsNUD.ValueChanged += MaxInitialLootSpawnsNUD_ValueChanged;
            //
            // CapareKeyCardAreaControl
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "CapareKeyCardAreaControl";
            Size = new Size(435, 92);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)MaxInitialLootSpawnsNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label AreaNameL;
        private TextBox AreaNameTB;
        private Label MaxInitialLootSpawnsL;
        private NumericUpDown MaxInitialLootSpawnsNUD;
    }
}
