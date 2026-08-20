namespace ExpansionPlugin
{
    partial class ExpansionHardlineFactionRepsControl
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
            HardlineReputationNUD = new NumericUpDown();
            label1 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)HardlineFactionIDNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)HardlineReputationNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(HardlineFactionIDNUD);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(HardlineReputationNUD);
            groupBox1.Controls.Add(label1);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(281, 92);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Player Data";
            // 
            // HardlineFactionIDNUD
            // 
            HardlineFactionIDNUD.BackColor = Color.FromArgb(60, 63, 65);
            HardlineFactionIDNUD.ForeColor = SystemColors.Control;
            HardlineFactionIDNUD.Location = new Point(139, 23);
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
            label3.Location = new Point(14, 25);
            label3.Name = "label3";
            label3.Size = new Size(60, 15);
            label3.TabIndex = 22;
            label3.Text = "Faction ID";
            // 
            // HardlineReputationNUD
            // 
            HardlineReputationNUD.BackColor = Color.FromArgb(60, 63, 65);
            HardlineReputationNUD.ForeColor = SystemColors.Control;
            HardlineReputationNUD.Location = new Point(139, 53);
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
            label1.Location = new Point(14, 55);
            label1.Name = "label1";
            label1.Size = new Size(65, 15);
            label1.TabIndex = 0;
            label1.Text = "Reputation";
            // 
            // ExpansionHardlineFactionRepsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionHardlineFactionRepsControl";
            Size = new Size(281, 92);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)HardlineFactionIDNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)HardlineReputationNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private NumericUpDown HardlineFactionIDNUD;
        private Label label3;
        private NumericUpDown HardlineReputationNUD;
        private Label label1;
    }
}
