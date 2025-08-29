namespace EconomyPlugin
{
    partial class RandompresetsCargoControl
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
            RandomPresetAttchemntNameTB = new TextBox();
            RandomPresetAttachmentChanceNUD = new NumericUpDown();
            label23 = new Label();
            economyGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)RandomPresetAttachmentChanceNUD).BeginInit();
            SuspendLayout();
            // 
            // economyGB
            // 
            economyGB.Controls.Add(RandomPresetAttchemntNameTB);
            economyGB.Controls.Add(RandomPresetAttachmentChanceNUD);
            economyGB.Controls.Add(label23);
            economyGB.ForeColor = SystemColors.Control;
            economyGB.Location = new Point(0, 0);
            economyGB.Name = "economyGB";
            economyGB.Size = new Size(246, 84);
            economyGB.TabIndex = 0;
            economyGB.TabStop = false;
            economyGB.Text = "Item";
            // 
            // RandomPresetAttchemntNameTB
            // 
            RandomPresetAttchemntNameTB.BackColor = Color.FromArgb(60, 63, 65);
            RandomPresetAttchemntNameTB.ForeColor = SystemColors.Control;
            RandomPresetAttchemntNameTB.Location = new Point(15, 22);
            RandomPresetAttchemntNameTB.Margin = new Padding(4, 3, 4, 3);
            RandomPresetAttchemntNameTB.Name = "RandomPresetAttchemntNameTB";
            RandomPresetAttchemntNameTB.Size = new Size(218, 23);
            RandomPresetAttchemntNameTB.TabIndex = 61;
            RandomPresetAttchemntNameTB.TextChanged += RandomPresetItemNameTB_TextChanged;
            // 
            // RandomPresetAttachmentChanceNUD
            // 
            RandomPresetAttachmentChanceNUD.BackColor = Color.FromArgb(60, 63, 65);
            RandomPresetAttachmentChanceNUD.DecimalPlaces = 2;
            RandomPresetAttachmentChanceNUD.ForeColor = SystemColors.Control;
            RandomPresetAttachmentChanceNUD.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            RandomPresetAttachmentChanceNUD.Location = new Point(117, 51);
            RandomPresetAttachmentChanceNUD.Margin = new Padding(4, 3, 4, 3);
            RandomPresetAttachmentChanceNUD.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            RandomPresetAttachmentChanceNUD.Name = "RandomPresetAttachmentChanceNUD";
            RandomPresetAttachmentChanceNUD.Size = new Size(116, 23);
            RandomPresetAttachmentChanceNUD.TabIndex = 63;
            RandomPresetAttachmentChanceNUD.TextAlign = HorizontalAlignment.Center;
            RandomPresetAttachmentChanceNUD.ValueChanged += RandomPresetItemChanceNUD_ValueChanged;
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
            // RandompresetsAttchments
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(economyGB);
            ForeColor = SystemColors.Control;
            Name = "RandompresetsAttchments";
            Size = new Size(252, 90);
            economyGB.ResumeLayout(false);
            economyGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)RandomPresetAttachmentChanceNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox economyGB;
        private TextBox RandomPresetAttchemntNameTB;
        private NumericUpDown RandomPresetAttachmentChanceNUD;
        private Label label23;
    }
}
