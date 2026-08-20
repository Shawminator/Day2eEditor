namespace ExpansionPlugin
{
    partial class ExpansionHardlinePlayerDataControl
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
            HardlineFactionIDNUD = new NumericUpDown();
            label3 = new Label();
            hardLinePersonalStorageLevelNUD = new NumericUpDown();
            label2 = new Label();
            HardlineReputationNUD = new NumericUpDown();
            label1 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)HardlineFactionIDNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)hardLinePersonalStorageLevelNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)HardlineReputationNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(HardlineFactionIDNUD);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(hardLinePersonalStorageLevelNUD);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(HardlineReputationNUD);
            groupBox1.Controls.Add(label1);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(281, 116);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Player Data";
            // 
            // HardlineFactionIDNUD
            // 
            HardlineFactionIDNUD.BackColor = Color.FromArgb(60, 63, 65);
            HardlineFactionIDNUD.ForeColor = SystemColors.Control;
            HardlineFactionIDNUD.Location = new Point(152, 53);
            HardlineFactionIDNUD.Margin = new Padding(4, 3, 4, 3);
            HardlineFactionIDNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            HardlineFactionIDNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            HardlineFactionIDNUD.Name = "HardlineFactionIDNUD";
            HardlineFactionIDNUD.Size = new Size(118, 23);
            HardlineFactionIDNUD.TabIndex = 23;
            HardlineFactionIDNUD.Tag = "Weight";
            HardlineFactionIDNUD.TextAlign = HorizontalAlignment.Center;
            HardlineFactionIDNUD.ValueChanged += HardlineFactionIDNUD_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 55);
            label3.Name = "label3";
            label3.Size = new Size(60, 15);
            label3.TabIndex = 22;
            label3.Text = "Faction ID";
            // 
            // hardLinePersonalStorageLevelNUD
            // 
            hardLinePersonalStorageLevelNUD.BackColor = Color.FromArgb(60, 63, 65);
            hardLinePersonalStorageLevelNUD.ForeColor = SystemColors.Control;
            hardLinePersonalStorageLevelNUD.Location = new Point(152, 83);
            hardLinePersonalStorageLevelNUD.Margin = new Padding(4, 3, 4, 3);
            hardLinePersonalStorageLevelNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            hardLinePersonalStorageLevelNUD.Name = "hardLinePersonalStorageLevelNUD";
            hardLinePersonalStorageLevelNUD.Size = new Size(118, 23);
            hardLinePersonalStorageLevelNUD.TabIndex = 21;
            hardLinePersonalStorageLevelNUD.Tag = "Weight";
            hardLinePersonalStorageLevelNUD.TextAlign = HorizontalAlignment.Center;
            hardLinePersonalStorageLevelNUD.ValueChanged += hardLinePersonalStorageLevelNUD_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 85);
            label2.Name = "label2";
            label2.Size = new Size(125, 15);
            label2.TabIndex = 20;
            label2.Text = "Personal Storage Level";
            // 
            // HardlineReputationNUD
            // 
            HardlineReputationNUD.BackColor = Color.FromArgb(60, 63, 65);
            HardlineReputationNUD.ForeColor = SystemColors.Control;
            HardlineReputationNUD.Location = new Point(152, 23);
            HardlineReputationNUD.Margin = new Padding(4, 3, 4, 3);
            HardlineReputationNUD.Maximum = new decimal(new int[] { 1569325055, 23283064, 0, 0 });
            HardlineReputationNUD.Name = "HardlineReputationNUD";
            HardlineReputationNUD.Size = new Size(118, 23);
            HardlineReputationNUD.TabIndex = 19;
            HardlineReputationNUD.Tag = "Weight";
            HardlineReputationNUD.TextAlign = HorizontalAlignment.Center;
            HardlineReputationNUD.ValueChanged += HardlineReputationNUD_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 25);
            label1.Name = "label1";
            label1.Size = new Size(65, 15);
            label1.TabIndex = 0;
            label1.Text = "Reputation";
            // 
            // ExpansionHardlinePlayerDataControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionHardlinePlayerDataControl";
            Size = new Size(281, 116);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)HardlineFactionIDNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)hardLinePersonalStorageLevelNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)HardlineReputationNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label1;
        private NumericUpDown HardlineFactionIDNUD;
        private Label label3;
        private NumericUpDown hardLinePersonalStorageLevelNUD;
        private Label label2;
        private NumericUpDown HardlineReputationNUD;
    }
}
