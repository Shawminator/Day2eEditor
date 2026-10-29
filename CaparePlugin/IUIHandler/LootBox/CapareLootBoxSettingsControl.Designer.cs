namespace CaparePlugin
{
    partial class CapareLootBoxSettingsControl
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
            LootBoxNameL = new Label();
            LootBoxNameTB = new TextBox();
            groupBox1.SuspendLayout();

            SuspendLayout();
            //
            // groupBox1
            //
            groupBox1.Controls.Add(LootBoxNameL);
            groupBox1.Controls.Add(LootBoxNameTB);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(435, 57);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Loot Box";
            //
            // LootBoxNameL
            //
            LootBoxNameL.AutoSize = true;
            LootBoxNameL.ForeColor = Color.FromArgb(220, 220, 220);
            LootBoxNameL.Location = new Point(11, 25);
            LootBoxNameL.Margin = new Padding(4, 0, 4, 0);
            LootBoxNameL.Name = "LootBoxNameL";
            LootBoxNameL.Size = new Size(91, 15);
            LootBoxNameL.TabIndex = 0;
            LootBoxNameL.Text = "Loot Box Name";
            //
            // LootBoxNameTB
            //
            LootBoxNameTB.BackColor = Color.FromArgb(60, 63, 65);
            LootBoxNameTB.ForeColor = SystemColors.Control;
            LootBoxNameTB.Location = new Point(190, 22);
            LootBoxNameTB.Margin = new Padding(4, 3, 4, 3);
            LootBoxNameTB.Name = "LootBoxNameTB";
            LootBoxNameTB.Size = new Size(230, 23);
            LootBoxNameTB.TabIndex = 1;
            LootBoxNameTB.TextChanged += LootBoxNameTB_TextChanged;
            //
            // CapareLootBoxSettingsControl
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "CapareLootBoxSettingsControl";
            Size = new Size(435, 63);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();

            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label LootBoxNameL;
        private TextBox LootBoxNameTB;
    }
}
