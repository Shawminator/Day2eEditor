namespace CaparePlugin
{
    partial class CapareLootBoxGeneralControl
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
            LootBoxLifetimeL = new Label();
            LootBoxLifetimeNUD = new NumericUpDown();
            StaticLootBoxEnabledL = new Label();
            StaticLootBoxEnabledCB = new CheckBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LootBoxLifetimeNUD).BeginInit();
            SuspendLayout();
            //
            // groupBox1
            //
            groupBox1.Controls.Add(LootBoxLifetimeL);
            groupBox1.Controls.Add(LootBoxLifetimeNUD);
            groupBox1.Controls.Add(StaticLootBoxEnabledL);
            groupBox1.Controls.Add(StaticLootBoxEnabledCB);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(435, 86);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "General";
            //
            // LootBoxLifetimeL
            //
            LootBoxLifetimeL.AutoSize = true;
            LootBoxLifetimeL.ForeColor = Color.FromArgb(220, 220, 220);
            LootBoxLifetimeL.Location = new Point(11, 25);
            LootBoxLifetimeL.Margin = new Padding(4, 0, 4, 0);
            LootBoxLifetimeL.Name = "LootBoxLifetimeL";
            LootBoxLifetimeL.Size = new Size(189, 15);
            LootBoxLifetimeL.TabIndex = 0;
            LootBoxLifetimeL.Text = "Loot Box Lifetime (Seconds)";
            //
            // LootBoxLifetimeNUD
            //
            LootBoxLifetimeNUD.BackColor = Color.FromArgb(60, 63, 65);
            LootBoxLifetimeNUD.ForeColor = SystemColors.Control;
            LootBoxLifetimeNUD.Location = new Point(190, 22);
            LootBoxLifetimeNUD.Margin = new Padding(4, 3, 4, 3);
            LootBoxLifetimeNUD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            LootBoxLifetimeNUD.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            LootBoxLifetimeNUD.Name = "LootBoxLifetimeNUD";
            LootBoxLifetimeNUD.Size = new Size(230, 23);
            LootBoxLifetimeNUD.TabIndex = 1;
            LootBoxLifetimeNUD.TextAlign = HorizontalAlignment.Center;
            LootBoxLifetimeNUD.ValueChanged += LootBoxLifetimeNUD_ValueChanged;
            //
            // StaticLootBoxEnabledL
            //
            StaticLootBoxEnabledL.AutoSize = true;
            StaticLootBoxEnabledL.ForeColor = Color.FromArgb(220, 220, 220);
            StaticLootBoxEnabledL.Location = new Point(11, 54);
            StaticLootBoxEnabledL.Margin = new Padding(4, 0, 4, 0);
            StaticLootBoxEnabledL.Name = "StaticLootBoxEnabledL";
            StaticLootBoxEnabledL.Size = new Size(161, 15);
            StaticLootBoxEnabledL.TabIndex = 2;
            StaticLootBoxEnabledL.Text = "Static Loot Box Enabled";
            //
            // StaticLootBoxEnabledCB
            //
            StaticLootBoxEnabledCB.AutoSize = true;
            StaticLootBoxEnabledCB.Location = new Point(190, 54);
            StaticLootBoxEnabledCB.Margin = new Padding(4, 3, 4, 3);
            StaticLootBoxEnabledCB.Name = "StaticLootBoxEnabledCB";
            StaticLootBoxEnabledCB.Size = new Size(15, 14);
            StaticLootBoxEnabledCB.TabIndex = 3;
            StaticLootBoxEnabledCB.UseVisualStyleBackColor = true;
            StaticLootBoxEnabledCB.CheckedChanged += StaticLootBoxEnabledCB_CheckedChanged;
            //
            // CapareLootBoxGeneralControl
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "CapareLootBoxGeneralControl";
            Size = new Size(435, 92);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)LootBoxLifetimeNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label LootBoxLifetimeL;
        private NumericUpDown LootBoxLifetimeNUD;
        private Label StaticLootBoxEnabledL;
        private CheckBox StaticLootBoxEnabledCB;
    }
}
