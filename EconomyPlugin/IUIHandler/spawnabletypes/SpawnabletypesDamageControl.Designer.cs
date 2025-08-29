namespace EconomyPlugin
{
    partial class SpawnabletypesDamageControl
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
            DamageGB = new GroupBox();
            DamageMaxNUD = new NumericUpDown();
            label24 = new Label();
            label25 = new Label();
            DamageMinNUD = new NumericUpDown();
            DamageGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DamageMaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DamageMinNUD).BeginInit();
            SuspendLayout();
            // 
            // DamageGB
            // 
            DamageGB.Controls.Add(DamageMaxNUD);
            DamageGB.Controls.Add(label24);
            DamageGB.Controls.Add(label25);
            DamageGB.Controls.Add(DamageMinNUD);
            DamageGB.ForeColor = SystemColors.ButtonHighlight;
            DamageGB.Location = new Point(4, 3);
            DamageGB.Margin = new Padding(4, 3, 4, 3);
            DamageGB.Name = "DamageGB";
            DamageGB.Padding = new Padding(4, 3, 4, 3);
            DamageGB.Size = new Size(282, 65);
            DamageGB.TabIndex = 66;
            DamageGB.TabStop = false;
            DamageGB.Text = "Damage";
            // 
            // DamageMaxNUD
            // 
            DamageMaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            DamageMaxNUD.DecimalPlaces = 2;
            DamageMaxNUD.ForeColor = SystemColors.Control;
            DamageMaxNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            DamageMaxNUD.Location = new Point(186, 25);
            DamageMaxNUD.Margin = new Padding(4, 3, 4, 3);
            DamageMaxNUD.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            DamageMaxNUD.Name = "DamageMaxNUD";
            DamageMaxNUD.Size = new Size(79, 23);
            DamageMaxNUD.TabIndex = 8;
            DamageMaxNUD.TextAlign = HorizontalAlignment.Center;
            DamageMaxNUD.ValueChanged += DamageMaxNUD_ValueChanged;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.ForeColor = SystemColors.Control;
            label24.Location = new Point(25, 28);
            label24.Margin = new Padding(4, 0, 4, 0);
            label24.Name = "label24";
            label24.Size = new Size(28, 15);
            label24.TabIndex = 0;
            label24.Text = "Min";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.ForeColor = SystemColors.Control;
            label25.Location = new Point(151, 28);
            label25.Margin = new Padding(4, 0, 4, 0);
            label25.Name = "label25";
            label25.Size = new Size(29, 15);
            label25.TabIndex = 7;
            label25.Text = "Max";
            // 
            // DamageMinNUD
            // 
            DamageMinNUD.BackColor = Color.FromArgb(60, 63, 65);
            DamageMinNUD.DecimalPlaces = 2;
            DamageMinNUD.ForeColor = SystemColors.Control;
            DamageMinNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            DamageMinNUD.Location = new Point(59, 25);
            DamageMinNUD.Margin = new Padding(4, 3, 4, 3);
            DamageMinNUD.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            DamageMinNUD.Name = "DamageMinNUD";
            DamageMinNUD.Size = new Size(79, 23);
            DamageMinNUD.TabIndex = 6;
            DamageMinNUD.TextAlign = HorizontalAlignment.Center;
            DamageMinNUD.ValueChanged += DamageMinNUD_ValueChanged;
            // 
            // SpawnabletypesDamageControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(DamageGB);
            ForeColor = SystemColors.Control;
            Name = "SpawnabletypesDamageControl";
            Size = new Size(294, 77);
            DamageGB.ResumeLayout(false);
            DamageGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DamageMaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)DamageMinNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox DamageGB;
        private NumericUpDown DamageMaxNUD;
        private Label label24;
        private Label label25;
        private NumericUpDown DamageMinNUD;
    }
}
