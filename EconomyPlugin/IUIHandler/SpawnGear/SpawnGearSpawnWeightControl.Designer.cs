namespace EconomyPlugin
{
    partial class SpawnGearSpawnWeightControl
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
            spawnWeightGB = new GroupBox();
            spawnWeightNUD = new NumericUpDown();
            spawnWeightGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)spawnWeightNUD).BeginInit();
            SuspendLayout();
            // 
            // spawnWeightGB
            // 
            spawnWeightGB.Controls.Add(spawnWeightNUD);
            spawnWeightGB.ForeColor = SystemColors.Control;
            spawnWeightGB.Location = new Point(0, 0);
            spawnWeightGB.Margin = new Padding(4, 3, 4, 3);
            spawnWeightGB.Name = "spawnWeightGB";
            spawnWeightGB.Padding = new Padding(4, 3, 4, 3);
            spawnWeightGB.Size = new Size(140, 60);
            spawnWeightGB.TabIndex = 228;
            spawnWeightGB.TabStop = false;
            spawnWeightGB.Text = "Spawn Weight";
            // 
            // spawnWeightNUD
            // 
            spawnWeightNUD.BackColor = Color.FromArgb(60, 63, 65);
            spawnWeightNUD.ForeColor = SystemColors.Control;
            spawnWeightNUD.Location = new Point(7, 22);
            spawnWeightNUD.Margin = new Padding(4, 3, 4, 3);
            spawnWeightNUD.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            spawnWeightNUD.Name = "spawnWeightNUD";
            spawnWeightNUD.Size = new Size(122, 23);
            spawnWeightNUD.TabIndex = 186;
            spawnWeightNUD.TextAlign = HorizontalAlignment.Center;
            spawnWeightNUD.ValueChanged += spawnWeightNUD_ValueChanged;
            // 
            // SpawnGearSpawnWeightControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(spawnWeightGB);
            ForeColor = SystemColors.Control;
            Name = "SpawnGearSpawnWeightControl";
            Size = new Size(150, 66);
            spawnWeightGB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)spawnWeightNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox spawnWeightGB;
        private NumericUpDown spawnWeightNUD;
    }
}
