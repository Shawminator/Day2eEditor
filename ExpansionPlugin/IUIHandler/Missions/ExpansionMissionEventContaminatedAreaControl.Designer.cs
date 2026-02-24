namespace ExpansionPlugin
{
    partial class ExpansionMissionEventContaminatedAreaControl
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
            label1 = new Label();
            label2 = new Label();
            StartDecayLifetimeNUD = new NumericUpDown();
            FinishDecayLifetimeNUD = new NumericUpDown();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)StartDecayLifetimeNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FinishDecayLifetimeNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(FinishDecayLifetimeNUD);
            groupBox1.Controls.Add(StartDecayLifetimeNUD);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(311, 87);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "General";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 25);
            label1.Name = "label1";
            label1.Size = new Size(112, 15);
            label1.TabIndex = 0;
            label1.Text = "Start Decay Lifetime";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 55);
            label2.Name = "label2";
            label2.Size = new Size(119, 15);
            label2.TabIndex = 1;
            label2.Text = "Finish Decay Lifetime";
            // 
            // StartDecayLifetimeNUD
            // 
            StartDecayLifetimeNUD.BackColor = Color.FromArgb(60, 63, 65);
            StartDecayLifetimeNUD.DecimalPlaces = 1;
            StartDecayLifetimeNUD.ForeColor = SystemColors.Control;
            StartDecayLifetimeNUD.Location = new Point(151, 22);
            StartDecayLifetimeNUD.Margin = new Padding(4, 3, 4, 3);
            StartDecayLifetimeNUD.Maximum = new decimal(new int[] { 1410065407, 2, 0, 0 });
            StartDecayLifetimeNUD.Name = "StartDecayLifetimeNUD";
            StartDecayLifetimeNUD.Size = new Size(148, 23);
            StartDecayLifetimeNUD.TabIndex = 13;
            StartDecayLifetimeNUD.TextAlign = HorizontalAlignment.Center;
            StartDecayLifetimeNUD.ValueChanged += StartDecayLifetimeNUD_ValueChanged;
            // 
            // FinishDecayLifetimeNUD
            // 
            FinishDecayLifetimeNUD.BackColor = Color.FromArgb(60, 63, 65);
            FinishDecayLifetimeNUD.DecimalPlaces = 1;
            FinishDecayLifetimeNUD.ForeColor = SystemColors.Control;
            FinishDecayLifetimeNUD.Location = new Point(151, 51);
            FinishDecayLifetimeNUD.Margin = new Padding(4, 3, 4, 3);
            FinishDecayLifetimeNUD.Maximum = new decimal(new int[] { 1410065407, 2, 0, 0 });
            FinishDecayLifetimeNUD.Name = "FinishDecayLifetimeNUD";
            FinishDecayLifetimeNUD.Size = new Size(148, 23);
            FinishDecayLifetimeNUD.TabIndex = 14;
            FinishDecayLifetimeNUD.TextAlign = HorizontalAlignment.Center;
            FinishDecayLifetimeNUD.ValueChanged += FinishDecayLifetimeNUD_ValueChanged;
            // 
            // ExpansionMissionEventContaminatedAreaControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionMissionEventContaminatedAreaControl";
            Size = new Size(311, 87);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)StartDecayLifetimeNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)FinishDecayLifetimeNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label2;
        private Label label1;
        private NumericUpDown FinishDecayLifetimeNUD;
        private NumericUpDown StartDecayLifetimeNUD;
    }
}
