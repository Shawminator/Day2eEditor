namespace ExpansionPlugin
{
    partial class AIPAtrolLoadbalancingcategoriesControl
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
            LBCGB = new GroupBox();
            MaxPatrolsLBCNUD = new NumericUpDown();
            darkLabel194 = new Label();
            MaxPlayersLBCNUD = new NumericUpDown();
            darkLabel195 = new Label();
            MinPlayersLBCNUD = new NumericUpDown();
            darkLabel196 = new Label();
            LBCGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MaxPatrolsLBCNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MaxPlayersLBCNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MinPlayersLBCNUD).BeginInit();
            SuspendLayout();
            // 
            // LBCGB
            // 
            LBCGB.Controls.Add(MaxPatrolsLBCNUD);
            LBCGB.Controls.Add(darkLabel194);
            LBCGB.Controls.Add(MaxPlayersLBCNUD);
            LBCGB.Controls.Add(darkLabel195);
            LBCGB.Controls.Add(MinPlayersLBCNUD);
            LBCGB.Controls.Add(darkLabel196);
            LBCGB.ForeColor = SystemColors.Control;
            LBCGB.Location = new Point(0, 0);
            LBCGB.Margin = new Padding(4, 3, 4, 3);
            LBCGB.Name = "LBCGB";
            LBCGB.Padding = new Padding(4, 3, 4, 3);
            LBCGB.Size = new Size(288, 142);
            LBCGB.TabIndex = 217;
            LBCGB.TabStop = false;
            LBCGB.Text = "Load Balancing";
            // 
            // MaxPatrolsLBCNUD
            // 
            MaxPatrolsLBCNUD.BackColor = Color.FromArgb(60, 63, 65);
            MaxPatrolsLBCNUD.ForeColor = SystemColors.Control;
            MaxPatrolsLBCNUD.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            MaxPatrolsLBCNUD.Location = new Point(120, 93);
            MaxPatrolsLBCNUD.Margin = new Padding(4, 3, 4, 3);
            MaxPatrolsLBCNUD.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            MaxPatrolsLBCNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            MaxPatrolsLBCNUD.Name = "MaxPatrolsLBCNUD";
            MaxPatrolsLBCNUD.Size = new Size(139, 23);
            MaxPatrolsLBCNUD.TabIndex = 206;
            MaxPatrolsLBCNUD.TextAlign = HorizontalAlignment.Center;
            MaxPatrolsLBCNUD.ValueChanged += MaxPatrolsLBCNUD_ValueChanged;
            // 
            // darkLabel194
            // 
            darkLabel194.AutoSize = true;
            darkLabel194.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel194.Location = new Point(16, 96);
            darkLabel194.Margin = new Padding(4, 0, 4, 0);
            darkLabel194.Name = "darkLabel194";
            darkLabel194.Size = new Size(69, 15);
            darkLabel194.TabIndex = 207;
            darkLabel194.Text = "Max Patrols";
            // 
            // MaxPlayersLBCNUD
            // 
            MaxPlayersLBCNUD.BackColor = Color.FromArgb(60, 63, 65);
            MaxPlayersLBCNUD.ForeColor = SystemColors.Control;
            MaxPlayersLBCNUD.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            MaxPlayersLBCNUD.Location = new Point(120, 63);
            MaxPlayersLBCNUD.Margin = new Padding(4, 3, 4, 3);
            MaxPlayersLBCNUD.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            MaxPlayersLBCNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            MaxPlayersLBCNUD.Name = "MaxPlayersLBCNUD";
            MaxPlayersLBCNUD.Size = new Size(139, 23);
            MaxPlayersLBCNUD.TabIndex = 204;
            MaxPlayersLBCNUD.TextAlign = HorizontalAlignment.Center;
            MaxPlayersLBCNUD.ValueChanged += MaxPlayersLBCNUD_ValueChanged;
            // 
            // darkLabel195
            // 
            darkLabel195.AutoSize = true;
            darkLabel195.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel195.Location = new Point(16, 66);
            darkLabel195.Margin = new Padding(4, 0, 4, 0);
            darkLabel195.Name = "darkLabel195";
            darkLabel195.Size = new Size(70, 15);
            darkLabel195.TabIndex = 205;
            darkLabel195.Text = "Max Players";
            // 
            // MinPlayersLBCNUD
            // 
            MinPlayersLBCNUD.BackColor = Color.FromArgb(60, 63, 65);
            MinPlayersLBCNUD.ForeColor = SystemColors.Control;
            MinPlayersLBCNUD.Location = new Point(120, 33);
            MinPlayersLBCNUD.Margin = new Padding(4, 3, 4, 3);
            MinPlayersLBCNUD.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            MinPlayersLBCNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            MinPlayersLBCNUD.Name = "MinPlayersLBCNUD";
            MinPlayersLBCNUD.Size = new Size(139, 23);
            MinPlayersLBCNUD.TabIndex = 202;
            MinPlayersLBCNUD.TextAlign = HorizontalAlignment.Center;
            MinPlayersLBCNUD.ValueChanged += MinPlayersLBCNUD_ValueChanged;
            // 
            // darkLabel196
            // 
            darkLabel196.AutoSize = true;
            darkLabel196.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel196.Location = new Point(15, 36);
            darkLabel196.Margin = new Padding(4, 0, 4, 0);
            darkLabel196.Name = "darkLabel196";
            darkLabel196.Size = new Size(68, 15);
            darkLabel196.TabIndex = 203;
            darkLabel196.Text = "Min Players";
            // 
            // AIPAtrolLoadbalancingcategoriesControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(LBCGB);
            ForeColor = SystemColors.Control;
            Name = "AIPAtrolLoadbalancingcategoriesControl";
            Size = new Size(288, 142);
            LBCGB.ResumeLayout(false);
            LBCGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)MaxPatrolsLBCNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)MaxPlayersLBCNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)MinPlayersLBCNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox LBCGB;
        private NumericUpDown MaxPatrolsLBCNUD;
        private Label darkLabel194;
        private NumericUpDown MaxPlayersLBCNUD;
        private Label darkLabel195;
        private NumericUpDown MinPlayersLBCNUD;
        private Label darkLabel196;
    }
}
