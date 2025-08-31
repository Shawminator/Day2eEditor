namespace EconomyPlugin
{
    partial class SpawnGearQuickBarSlotControl
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
            quickBarSlotGB = new GroupBox();
            quickBarSlotNUD = new NumericUpDown();
            quickBarSlotGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)quickBarSlotNUD).BeginInit();
            SuspendLayout();
            // 
            // quickBarSlotGB
            // 
            quickBarSlotGB.Controls.Add(quickBarSlotNUD);
            quickBarSlotGB.ForeColor = SystemColors.Control;
            quickBarSlotGB.Location = new Point(0, 0);
            quickBarSlotGB.Margin = new Padding(4, 3, 4, 3);
            quickBarSlotGB.Name = "quickBarSlotGB";
            quickBarSlotGB.Padding = new Padding(4, 3, 4, 3);
            quickBarSlotGB.Size = new Size(140, 60);
            quickBarSlotGB.TabIndex = 231;
            quickBarSlotGB.TabStop = false;
            quickBarSlotGB.Text = "Quick Bar Slot";
            // 
            // quickBarSlotNUD
            // 
            quickBarSlotNUD.BackColor = Color.FromArgb(60, 63, 65);
            quickBarSlotNUD.ForeColor = SystemColors.Control;
            quickBarSlotNUD.Location = new Point(7, 22);
            quickBarSlotNUD.Margin = new Padding(4, 3, 4, 3);
            quickBarSlotNUD.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            quickBarSlotNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            quickBarSlotNUD.Name = "quickBarSlotNUD";
            quickBarSlotNUD.Size = new Size(122, 23);
            quickBarSlotNUD.TabIndex = 186;
            quickBarSlotNUD.TextAlign = HorizontalAlignment.Center;
            quickBarSlotNUD.ValueChanged += quickBarSlotNUD_ValueChanged;
            // 
            // SpawnGearQuickBarSlotControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(quickBarSlotGB);
            ForeColor = SystemColors.Control;
            Name = "SpawnGearQuickBarSlotControl";
            Size = new Size(148, 68);
            quickBarSlotGB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)quickBarSlotNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox quickBarSlotGB;
        private NumericUpDown quickBarSlotNUD;
    }
}
