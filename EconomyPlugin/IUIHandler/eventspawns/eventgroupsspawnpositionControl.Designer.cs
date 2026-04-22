namespace EconomyPlugin
{
    partial class eventgroupsspawnpositionControl
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
            checkBox51 = new CheckBox();
            checkBox50 = new CheckBox();
            EventSpawnPosXNUD = new NumericUpDown();
            label116 = new Label();
            EventSpawnPosYNUD = new NumericUpDown();
            EventSpawnPosZNUD = new NumericUpDown();
            label118 = new Label();
            EventSpawnPosANUD = new NumericUpDown();
            EventspawnPositionGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)EventSpawnPosXNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EventSpawnPosYNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EventSpawnPosZNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EventSpawnPosANUD).BeginInit();
            SuspendLayout();
            // 
            // EventspawnPositionGB
            // 
            EventspawnPositionGB.Controls.Add(checkBox51);
            EventspawnPositionGB.Controls.Add(checkBox50);
            EventspawnPositionGB.Controls.Add(EventSpawnPosXNUD);
            EventspawnPositionGB.Controls.Add(label116);
            EventspawnPositionGB.Controls.Add(EventSpawnPosYNUD);
            EventspawnPositionGB.Controls.Add(EventSpawnPosZNUD);
            EventspawnPositionGB.Controls.Add(label118);
            EventspawnPositionGB.Controls.Add(EventSpawnPosANUD);
            EventspawnPositionGB.ForeColor = SystemColors.Control;
            EventspawnPositionGB.Location = new Point(0, 0);
            EventspawnPositionGB.Margin = new Padding(4, 3, 4, 3);
            EventspawnPositionGB.Name = "EventspawnPositionGB";
            EventspawnPositionGB.Padding = new Padding(4, 3, 4, 3);
            EventspawnPositionGB.Size = new Size(312, 115);
            EventspawnPositionGB.TabIndex = 5;
            EventspawnPositionGB.TabStop = false;
            EventspawnPositionGB.Text = "Position";
            // 
            // checkBox51
            // 
            checkBox51.AutoSize = true;
            checkBox51.Location = new Point(87, 83);
            checkBox51.Margin = new Padding(4, 3, 4, 3);
            checkBox51.Name = "checkBox51";
            checkBox51.RightToLeft = RightToLeft.Yes;
            checkBox51.Size = new Size(56, 19);
            checkBox51.TabIndex = 6;
            checkBox51.Text = "Use A";
            checkBox51.UseVisualStyleBackColor = true;
            checkBox51.CheckedChanged += checkBox51_CheckedChanged;
            // 
            // checkBox50
            // 
            checkBox50.AutoSize = true;
            checkBox50.Location = new Point(87, 53);
            checkBox50.Margin = new Padding(4, 3, 4, 3);
            checkBox50.Name = "checkBox50";
            checkBox50.RightToLeft = RightToLeft.Yes;
            checkBox50.Size = new Size(55, 19);
            checkBox50.TabIndex = 4;
            checkBox50.Text = "Use Y";
            checkBox50.UseVisualStyleBackColor = true;
            checkBox50.CheckedChanged += checkBox50_CheckedChanged;
            // 
            // EventSpawnPosXNUD
            // 
            EventSpawnPosXNUD.BackColor = Color.FromArgb(60, 63, 65);
            EventSpawnPosXNUD.DecimalPlaces = 4;
            EventSpawnPosXNUD.ForeColor = SystemColors.Control;
            EventSpawnPosXNUD.Location = new Point(35, 22);
            EventSpawnPosXNUD.Margin = new Padding(4, 3, 4, 3);
            EventSpawnPosXNUD.Maximum = new decimal(new int[] { 30000, 0, 0, 0 });
            EventSpawnPosXNUD.Minimum = new decimal(new int[] { 30000, 0, 0, int.MinValue });
            EventSpawnPosXNUD.Name = "EventSpawnPosXNUD";
            EventSpawnPosXNUD.Size = new Size(115, 23);
            EventSpawnPosXNUD.TabIndex = 1;
            EventSpawnPosXNUD.TextAlign = HorizontalAlignment.Center;
            EventSpawnPosXNUD.ValueChanged += EventSpawnPosXNUD_ValueChanged;
            // 
            // label116
            // 
            label116.AutoSize = true;
            label116.ForeColor = SystemColors.Control;
            label116.Location = new Point(13, 24);
            label116.Margin = new Padding(4, 0, 4, 0);
            label116.Name = "label116";
            label116.Size = new Size(25, 15);
            label116.TabIndex = 0;
            label116.Text = "X - ";
            // 
            // EventSpawnPosYNUD
            // 
            EventSpawnPosYNUD.BackColor = Color.FromArgb(60, 63, 65);
            EventSpawnPosYNUD.DecimalPlaces = 4;
            EventSpawnPosYNUD.ForeColor = SystemColors.Control;
            EventSpawnPosYNUD.Location = new Point(182, 52);
            EventSpawnPosYNUD.Margin = new Padding(4, 3, 4, 3);
            EventSpawnPosYNUD.Maximum = new decimal(new int[] { 30000, 0, 0, 0 });
            EventSpawnPosYNUD.Minimum = new decimal(new int[] { 30000, 0, 0, int.MinValue });
            EventSpawnPosYNUD.Name = "EventSpawnPosYNUD";
            EventSpawnPosYNUD.Size = new Size(115, 23);
            EventSpawnPosYNUD.TabIndex = 5;
            EventSpawnPosYNUD.TextAlign = HorizontalAlignment.Center;
            EventSpawnPosYNUD.ValueChanged += EventSpawnPosYNUD_ValueChanged;
            // 
            // EventSpawnPosZNUD
            // 
            EventSpawnPosZNUD.BackColor = Color.FromArgb(60, 63, 65);
            EventSpawnPosZNUD.DecimalPlaces = 4;
            EventSpawnPosZNUD.ForeColor = SystemColors.Control;
            EventSpawnPosZNUD.Location = new Point(182, 22);
            EventSpawnPosZNUD.Margin = new Padding(4, 3, 4, 3);
            EventSpawnPosZNUD.Maximum = new decimal(new int[] { 30000, 0, 0, 0 });
            EventSpawnPosZNUD.Minimum = new decimal(new int[] { 30000, 0, 0, int.MinValue });
            EventSpawnPosZNUD.Name = "EventSpawnPosZNUD";
            EventSpawnPosZNUD.Size = new Size(115, 23);
            EventSpawnPosZNUD.TabIndex = 3;
            EventSpawnPosZNUD.TextAlign = HorizontalAlignment.Center;
            EventSpawnPosZNUD.ValueChanged += EventSpawnPosZNUD_ValueChanged;
            // 
            // label118
            // 
            label118.AutoSize = true;
            label118.ForeColor = SystemColors.Control;
            label118.Location = new Point(160, 24);
            label118.Margin = new Padding(4, 0, 4, 0);
            label118.Name = "label118";
            label118.Size = new Size(25, 15);
            label118.TabIndex = 2;
            label118.Text = "Z - ";
            // 
            // EventSpawnPosANUD
            // 
            EventSpawnPosANUD.BackColor = Color.FromArgb(60, 63, 65);
            EventSpawnPosANUD.DecimalPlaces = 4;
            EventSpawnPosANUD.ForeColor = SystemColors.Control;
            EventSpawnPosANUD.Location = new Point(182, 82);
            EventSpawnPosANUD.Margin = new Padding(4, 3, 4, 3);
            EventSpawnPosANUD.Maximum = new decimal(new int[] { 30000, 0, 0, 0 });
            EventSpawnPosANUD.Minimum = new decimal(new int[] { 30000, 0, 0, int.MinValue });
            EventSpawnPosANUD.Name = "EventSpawnPosANUD";
            EventSpawnPosANUD.Size = new Size(115, 23);
            EventSpawnPosANUD.TabIndex = 7;
            EventSpawnPosANUD.TextAlign = HorizontalAlignment.Center;
            EventSpawnPosANUD.ValueChanged += EventSpawnPosANUD_ValueChanged;
            // 
            // eventgroupsspawnpositionControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(EventspawnPositionGB);
            ForeColor = SystemColors.Control;
            Name = "eventgroupsspawnpositionControl";
            Size = new Size(312, 115);
            EventspawnPositionGB.ResumeLayout(false);
            EventspawnPositionGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)EventSpawnPosXNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)EventSpawnPosYNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)EventSpawnPosZNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)EventSpawnPosANUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox EventspawnPositionGB;
        private CheckBox checkBox51;
        private CheckBox checkBox50;
        private NumericUpDown EventSpawnPosXNUD;
        private Label label116;
        private NumericUpDown EventSpawnPosYNUD;
        private NumericUpDown EventSpawnPosZNUD;
        private Label label118;
        private NumericUpDown EventSpawnPosANUD;
    }
}
