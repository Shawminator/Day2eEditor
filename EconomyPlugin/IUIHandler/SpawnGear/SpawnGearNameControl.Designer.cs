namespace EconomyPlugin
{
    partial class SpawnGearNameControl
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
            SpawnGearNameGB = new GroupBox();
            SpawnGearNameTB = new TextBox();
            SpawnGearNameGB.SuspendLayout();
            SuspendLayout();
            // 
            // SpawnGearNameGB
            // 
            SpawnGearNameGB.Controls.Add(SpawnGearNameTB);
            SpawnGearNameGB.ForeColor = SystemColors.Control;
            SpawnGearNameGB.Location = new Point(0, 0);
            SpawnGearNameGB.Margin = new Padding(4, 3, 4, 3);
            SpawnGearNameGB.Name = "SpawnGearNameGB";
            SpawnGearNameGB.Padding = new Padding(4, 3, 4, 3);
            SpawnGearNameGB.Size = new Size(324, 60);
            SpawnGearNameGB.TabIndex = 229;
            SpawnGearNameGB.TabStop = false;
            SpawnGearNameGB.Text = "Name";
            // 
            // SpawnGearNameTB
            // 
            SpawnGearNameTB.BackColor = Color.FromArgb(69, 73, 74);
            SpawnGearNameTB.BorderStyle = BorderStyle.FixedSingle;
            SpawnGearNameTB.ForeColor = SystemColors.Control;
            SpawnGearNameTB.Location = new Point(7, 22);
            SpawnGearNameTB.Margin = new Padding(4, 3, 4, 3);
            SpawnGearNameTB.Name = "SpawnGearNameTB";
            SpawnGearNameTB.Size = new Size(310, 23);
            SpawnGearNameTB.TabIndex = 13;
            SpawnGearNameTB.TextChanged += this.SpawnGearNameTB_TextChanged;
            // 
            // SpawnGearNameControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(SpawnGearNameGB);
            ForeColor = SystemColors.Control;
            Name = "SpawnGearNameControl";
            Size = new Size(335, 66);
            SpawnGearNameGB.ResumeLayout(false);
            SpawnGearNameGB.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox SpawnGearNameGB;
        private TextBox SpawnGearNameTB;
    }
}
