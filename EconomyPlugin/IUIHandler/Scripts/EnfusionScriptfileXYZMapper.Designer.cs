namespace EconomyPlugin
{
    partial class EnfusionScriptfileXYZMapper
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
            EventspawnPositionGB = new GroupBox();
            label2 = new Label();
            label1 = new Label();
            MAPXNUD = new NumericUpDown();
            label116 = new Label();
            RESOLUTIONNUD = new NumericUpDown();
            MAPZNUD = new NumericUpDown();
            EventspawnPositionGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MAPXNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RESOLUTIONNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MAPZNUD).BeginInit();
            SuspendLayout();
            // 
            // EventspawnPositionGB
            // 
            EventspawnPositionGB.Controls.Add(label2);
            EventspawnPositionGB.Controls.Add(label1);
            EventspawnPositionGB.Controls.Add(MAPXNUD);
            EventspawnPositionGB.Controls.Add(label116);
            EventspawnPositionGB.Controls.Add(RESOLUTIONNUD);
            EventspawnPositionGB.Controls.Add(MAPZNUD);
            EventspawnPositionGB.ForeColor = SystemColors.Control;
            EventspawnPositionGB.Location = new Point(0, 0);
            EventspawnPositionGB.Margin = new Padding(4, 3, 4, 3);
            EventspawnPositionGB.Name = "EventspawnPositionGB";
            EventspawnPositionGB.Padding = new Padding(4, 3, 4, 3);
            EventspawnPositionGB.Size = new Size(326, 120);
            EventspawnPositionGB.TabIndex = 6;
            EventspawnPositionGB.TabStop = false;
            EventspawnPositionGB.Text = "Position";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.Control;
            label2.Location = new Point(14, 85);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(63, 15);
            label2.TabIndex = 9;
            label2.Text = "Resolution";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(14, 55);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(41, 15);
            label1.TabIndex = 8;
            label1.Text = "Map Z";
            // 
            // MAPXNUD
            // 
            MAPXNUD.BackColor = Color.FromArgb(60, 63, 65);
            MAPXNUD.ForeColor = SystemColors.Control;
            MAPXNUD.Location = new Point(98, 23);
            MAPXNUD.Margin = new Padding(4, 3, 4, 3);
            MAPXNUD.Maximum = new decimal(new int[] { 30000, 0, 0, 0 });
            MAPXNUD.Minimum = new decimal(new int[] { 30000, 0, 0, int.MinValue });
            MAPXNUD.Name = "MAPXNUD";
            MAPXNUD.Size = new Size(217, 23);
            MAPXNUD.TabIndex = 1;
            MAPXNUD.TextAlign = HorizontalAlignment.Center;
            MAPXNUD.ValueChanged += MAPXNUD_ValueChanged;
            // 
            // label116
            // 
            label116.AutoSize = true;
            label116.ForeColor = SystemColors.Control;
            label116.Location = new Point(14, 25);
            label116.Margin = new Padding(4, 0, 4, 0);
            label116.Name = "label116";
            label116.Size = new Size(41, 15);
            label116.TabIndex = 0;
            label116.Text = "Map X";
            // 
            // RESOLUTIONNUD
            // 
            RESOLUTIONNUD.BackColor = Color.FromArgb(60, 63, 65);
            RESOLUTIONNUD.DecimalPlaces = 1;
            RESOLUTIONNUD.ForeColor = SystemColors.Control;
            RESOLUTIONNUD.Location = new Point(98, 83);
            RESOLUTIONNUD.Margin = new Padding(4, 3, 4, 3);
            RESOLUTIONNUD.Maximum = new decimal(new int[] { 30000, 0, 0, 0 });
            RESOLUTIONNUD.Minimum = new decimal(new int[] { 30000, 0, 0, int.MinValue });
            RESOLUTIONNUD.Name = "RESOLUTIONNUD";
            RESOLUTIONNUD.Size = new Size(217, 23);
            RESOLUTIONNUD.TabIndex = 5;
            RESOLUTIONNUD.TextAlign = HorizontalAlignment.Center;
            RESOLUTIONNUD.ValueChanged += RESOLUTIONNUD_ValueChanged;
            // 
            // MAPZNUD
            // 
            MAPZNUD.BackColor = Color.FromArgb(60, 63, 65);
            MAPZNUD.ForeColor = SystemColors.Control;
            MAPZNUD.Location = new Point(98, 53);
            MAPZNUD.Margin = new Padding(4, 3, 4, 3);
            MAPZNUD.Maximum = new decimal(new int[] { 30000, 0, 0, 0 });
            MAPZNUD.Minimum = new decimal(new int[] { 30000, 0, 0, int.MinValue });
            MAPZNUD.Name = "MAPZNUD";
            MAPZNUD.Size = new Size(217, 23);
            MAPZNUD.TabIndex = 3;
            MAPZNUD.TextAlign = HorizontalAlignment.Center;
            MAPZNUD.ValueChanged += MAPZNUD_ValueChanged;
            // 
            // EnfusionScriptfileXYZMapper
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(EventspawnPositionGB);
            ForeColor = SystemColors.Control;
            Name = "EnfusionScriptfileXYZMapper";
            Size = new Size(326, 120);
            EventspawnPositionGB.ResumeLayout(false);
            EventspawnPositionGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)MAPXNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)RESOLUTIONNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)MAPZNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox EventspawnPositionGB;
        private Label label2;
        private Label label1;
        private NumericUpDown MAPXNUD;
        private Label label116;
        private NumericUpDown RESOLUTIONNUD;
        private NumericUpDown MAPZNUD;
    }
}
