namespace ExpansionPlugin
{
    partial class ExpansionGeneralGraveCrossControl
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
            groupBox33 = new GroupBox();
            EnableAIGravecrossCB = new CheckBox();
            darkLabel228 = new Label();
            GravecrossSpawnTimeDelayNUD = new NumericUpDown();
            darkLabel89 = new Label();
            EnableGravecrossCB = new CheckBox();
            GravecrossDeleteBodyCB = new CheckBox();
            GravecrossTimeThresholdNUD = new NumericUpDown();
            groupBox33.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GravecrossSpawnTimeDelayNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GravecrossTimeThresholdNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox33
            // 
            groupBox33.Controls.Add(EnableAIGravecrossCB);
            groupBox33.Controls.Add(darkLabel228);
            groupBox33.Controls.Add(GravecrossSpawnTimeDelayNUD);
            groupBox33.Controls.Add(darkLabel89);
            groupBox33.Controls.Add(EnableGravecrossCB);
            groupBox33.Controls.Add(GravecrossDeleteBodyCB);
            groupBox33.Controls.Add(GravecrossTimeThresholdNUD);
            groupBox33.ForeColor = SystemColors.Control;
            groupBox33.Location = new Point(0, 0);
            groupBox33.Margin = new Padding(4, 3, 4, 3);
            groupBox33.Name = "groupBox33";
            groupBox33.Padding = new Padding(4, 3, 4, 3);
            groupBox33.Size = new Size(278, 202);
            groupBox33.TabIndex = 15;
            groupBox33.TabStop = false;
            groupBox33.Text = "Grave Cross";
            // 
            // EnableAIGravecrossCB
            // 
            EnableAIGravecrossCB.AutoSize = true;
            EnableAIGravecrossCB.Location = new Point(21, 48);
            EnableAIGravecrossCB.Margin = new Padding(4, 3, 4, 3);
            EnableAIGravecrossCB.Name = "EnableAIGravecrossCB";
            EnableAIGravecrossCB.Size = new Size(135, 19);
            EnableAIGravecrossCB.TabIndex = 1;
            EnableAIGravecrossCB.Text = "Enable AI Gravecross";
            EnableAIGravecrossCB.UseVisualStyleBackColor = true;
            EnableAIGravecrossCB.CheckedChanged += EnableAIGravecrossCB_CheckedChanged;
            // 
            // darkLabel228
            // 
            darkLabel228.AutoSize = true;
            darkLabel228.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel228.Location = new Point(16, 147);
            darkLabel228.Margin = new Padding(4, 0, 4, 0);
            darkLabel228.Name = "darkLabel228";
            darkLabel228.Size = new Size(163, 15);
            darkLabel228.TabIndex = 5;
            darkLabel228.Text = "Gravecross Spawn Time Delay";
            // 
            // GravecrossSpawnTimeDelayNUD
            // 
            GravecrossSpawnTimeDelayNUD.BackColor = Color.FromArgb(60, 63, 65);
            GravecrossSpawnTimeDelayNUD.DecimalPlaces = 1;
            GravecrossSpawnTimeDelayNUD.ForeColor = SystemColors.Control;
            GravecrossSpawnTimeDelayNUD.Location = new Point(19, 165);
            GravecrossSpawnTimeDelayNUD.Margin = new Padding(4, 3, 4, 3);
            GravecrossSpawnTimeDelayNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            GravecrossSpawnTimeDelayNUD.Name = "GravecrossSpawnTimeDelayNUD";
            GravecrossSpawnTimeDelayNUD.Size = new Size(130, 23);
            GravecrossSpawnTimeDelayNUD.TabIndex = 6;
            GravecrossSpawnTimeDelayNUD.TextAlign = HorizontalAlignment.Center;
            GravecrossSpawnTimeDelayNUD.ValueChanged += GravecrossSpawnTimeDelayNUD_ValueChanged;
            // 
            // darkLabel89
            // 
            darkLabel89.AutoSize = true;
            darkLabel89.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel89.Location = new Point(16, 98);
            darkLabel89.Margin = new Padding(4, 0, 4, 0);
            darkLabel89.Name = "darkLabel89";
            darkLabel89.Size = new Size(148, 15);
            darkLabel89.TabIndex = 3;
            darkLabel89.Text = "Gravecross Time Threshold";
            // 
            // EnableGravecrossCB
            // 
            EnableGravecrossCB.AutoSize = true;
            EnableGravecrossCB.Location = new Point(21, 22);
            EnableGravecrossCB.Margin = new Padding(4, 3, 4, 3);
            EnableGravecrossCB.Name = "EnableGravecrossCB";
            EnableGravecrossCB.Size = new Size(121, 19);
            EnableGravecrossCB.TabIndex = 0;
            EnableGravecrossCB.Text = "Enable Gravecross";
            EnableGravecrossCB.UseVisualStyleBackColor = true;
            EnableGravecrossCB.CheckedChanged += EnableGravecrossCB_CheckedChanged;
            // 
            // GravecrossDeleteBodyCB
            // 
            GravecrossDeleteBodyCB.AutoSize = true;
            GravecrossDeleteBodyCB.Location = new Point(20, 75);
            GravecrossDeleteBodyCB.Margin = new Padding(4, 3, 4, 3);
            GravecrossDeleteBodyCB.Name = "GravecrossDeleteBodyCB";
            GravecrossDeleteBodyCB.Size = new Size(149, 19);
            GravecrossDeleteBodyCB.TabIndex = 2;
            GravecrossDeleteBodyCB.Text = "Gravecross Delete Body";
            GravecrossDeleteBodyCB.UseVisualStyleBackColor = true;
            GravecrossDeleteBodyCB.CheckedChanged += GravecrossDeleteBodyCB_CheckedChanged;
            // 
            // GravecrossTimeThresholdNUD
            // 
            GravecrossTimeThresholdNUD.BackColor = Color.FromArgb(60, 63, 65);
            GravecrossTimeThresholdNUD.DecimalPlaces = 1;
            GravecrossTimeThresholdNUD.ForeColor = SystemColors.Control;
            GravecrossTimeThresholdNUD.Location = new Point(19, 117);
            GravecrossTimeThresholdNUD.Margin = new Padding(4, 3, 4, 3);
            GravecrossTimeThresholdNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            GravecrossTimeThresholdNUD.Name = "GravecrossTimeThresholdNUD";
            GravecrossTimeThresholdNUD.Size = new Size(130, 23);
            GravecrossTimeThresholdNUD.TabIndex = 4;
            GravecrossTimeThresholdNUD.TextAlign = HorizontalAlignment.Center;
            GravecrossTimeThresholdNUD.ValueChanged += GravecrossTimeThresholdNUD_ValueChanged;
            // 
            // ExpansionGeneralGraveCrossControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox33);
            ForeColor = SystemColors.Control;
            Name = "ExpansionGeneralGraveCrossControl";
            Size = new Size(278, 202);
            groupBox33.ResumeLayout(false);
            groupBox33.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)GravecrossSpawnTimeDelayNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)GravecrossTimeThresholdNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox33;
        private CheckBox EnableAIGravecrossCB;
        private Label darkLabel228;
        private NumericUpDown GravecrossSpawnTimeDelayNUD;
        private Label darkLabel89;
        private CheckBox EnableGravecrossCB;
        private CheckBox GravecrossDeleteBodyCB;
        private NumericUpDown GravecrossTimeThresholdNUD;
    }
}
