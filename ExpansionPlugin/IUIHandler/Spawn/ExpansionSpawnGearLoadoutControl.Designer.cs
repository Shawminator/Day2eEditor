namespace ExpansionPlugin
{
    partial class ExpansionSpawnGearLoadoutControl
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
            groupBox66 = new GroupBox();
            darkLabel172 = new Label();
            MaleChanceNUD = new NumericUpDown();
            darkButton64 = new Button();
            groupBox66.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MaleChanceNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox66
            // 
            groupBox66.Controls.Add(darkLabel172);
            groupBox66.Controls.Add(MaleChanceNUD);
            groupBox66.Controls.Add(darkButton64);
            groupBox66.ForeColor = SystemColors.Control;
            groupBox66.Location = new Point(0, 0);
            groupBox66.Margin = new Padding(4, 3, 4, 3);
            groupBox66.Name = "groupBox66";
            groupBox66.Padding = new Padding(4, 3, 4, 3);
            groupBox66.Size = new Size(293, 66);
            groupBox66.TabIndex = 6;
            groupBox66.TabStop = false;
            groupBox66.Text = "Spawn Loadouts";
            groupBox66.Visible = false;
            // 
            // darkLabel172
            // 
            darkLabel172.AutoSize = true;
            darkLabel172.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel172.Location = new Point(14, 25);
            darkLabel172.Margin = new Padding(4, 0, 4, 0);
            darkLabel172.Name = "darkLabel172";
            darkLabel172.Size = new Size(47, 15);
            darkLabel172.TabIndex = 5;
            darkLabel172.Text = "Chance";
            // 
            // MaleChanceNUD
            // 
            MaleChanceNUD.BackColor = Color.FromArgb(60, 63, 65);
            MaleChanceNUD.DecimalPlaces = 2;
            MaleChanceNUD.ForeColor = SystemColors.Control;
            MaleChanceNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            MaleChanceNUD.Location = new Point(93, 23);
            MaleChanceNUD.Margin = new Padding(4, 3, 4, 3);
            MaleChanceNUD.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            MaleChanceNUD.Name = "MaleChanceNUD";
            MaleChanceNUD.Size = new Size(184, 23);
            MaleChanceNUD.TabIndex = 6;
            MaleChanceNUD.Tag = "SpawnSelectionScreenMenuID";
            MaleChanceNUD.TextAlign = HorizontalAlignment.Center;
            MaleChanceNUD.ValueChanged += MaleChanceNUD_ValueChanged;
            // 
            // darkButton64
            // 
            darkButton64.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            darkButton64.Location = new Point(182, 198);
            darkButton64.Margin = new Padding(4, 3, 4, 3);
            darkButton64.Name = "darkButton64";
            darkButton64.Size = new Size(142, 27);
            darkButton64.TabIndex = 2;
            darkButton64.Text = "Remove";
            // 
            // ExpansionSpawnGearLoadoutControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox66);
            ForeColor = SystemColors.Control;
            Name = "ExpansionSpawnGearLoadoutControl";
            Size = new Size(293, 66);
            groupBox66.ResumeLayout(false);
            groupBox66.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)MaleChanceNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox66;
        private Label darkLabel172;
        private NumericUpDown MaleChanceNUD;
        private Button darkButton64;
    }
}
