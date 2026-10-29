namespace ExpansionPlugin
{
    partial class ExpansionMissionEventHordeControl
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
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            MinInfectedAmountNUD = new NumericUpDown();
            MaxInfectedAmountNUD = new NumericUpDown();
            PositionXNUD = new NumericUpDown();
            PositionYNUD = new NumericUpDown();
            PositionZNUD = new NumericUpDown();
            MinimumSpawnRadiusNUD = new NumericUpDown();
            MaximumSpawnRadiusNUD = new NumericUpDown();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MinInfectedAmountNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MaxInfectedAmountNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PositionXNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PositionYNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PositionZNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MinimumSpawnRadiusNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MaximumSpawnRadiusNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(MaximumSpawnRadiusNUD);
            groupBox1.Controls.Add(MinimumSpawnRadiusNUD);
            groupBox1.Controls.Add(PositionZNUD);
            groupBox1.Controls.Add(PositionYNUD);
            groupBox1.Controls.Add(PositionXNUD);
            groupBox1.Controls.Add(MaxInfectedAmountNUD);
            groupBox1.Controls.Add(MinInfectedAmountNUD);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(311, 236);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Horde";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 25);
            label1.Name = "label1";
            label1.Size = new Size(133, 15);
            label1.TabIndex = 0;
            label1.Text = "Min Infected Amount";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 55);
            label2.Name = "label2";
            label2.Size = new Size(133, 15);
            label2.TabIndex = 1;
            label2.Text = "Max Infected Amount";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 85);
            label3.Name = "label3";
            label3.Size = new Size(70, 15);
            label3.TabIndex = 2;
            label3.Text = "Position X";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 115);
            label4.Name = "label4";
            label4.Size = new Size(70, 15);
            label4.TabIndex = 3;
            label4.Text = "Position Y";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(14, 145);
            label5.Name = "label5";
            label5.Size = new Size(70, 15);
            label5.TabIndex = 4;
            label5.Text = "Position Z";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(14, 175);
            label6.Name = "label6";
            label6.Size = new Size(140, 15);
            label6.TabIndex = 5;
            label6.Text = "Minimum Spawn Radius";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(14, 205);
            label7.Name = "label7";
            label7.Size = new Size(140, 15);
            label7.TabIndex = 6;
            label7.Text = "Maximum Spawn Radius";
            // 
            // MinInfectedAmountNUD
            // 
            MinInfectedAmountNUD.BackColor = Color.FromArgb(60, 63, 65);
            MinInfectedAmountNUD.ForeColor = SystemColors.Control;
            MinInfectedAmountNUD.Location = new Point(151, 22);
            MinInfectedAmountNUD.Margin = new Padding(4, 3, 4, 3);
            MinInfectedAmountNUD.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            MinInfectedAmountNUD.Name = "MinInfectedAmountNUD";
            MinInfectedAmountNUD.Size = new Size(148, 23);
            MinInfectedAmountNUD.TabIndex = 7;
            MinInfectedAmountNUD.TextAlign = HorizontalAlignment.Center;
            MinInfectedAmountNUD.ValueChanged += MinInfectedAmountNUD_ValueChanged;
            // 
            // MaxInfectedAmountNUD
            // 
            MaxInfectedAmountNUD.BackColor = Color.FromArgb(60, 63, 65);
            MaxInfectedAmountNUD.ForeColor = SystemColors.Control;
            MaxInfectedAmountNUD.Location = new Point(151, 52);
            MaxInfectedAmountNUD.Margin = new Padding(4, 3, 4, 3);
            MaxInfectedAmountNUD.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            MaxInfectedAmountNUD.Name = "MaxInfectedAmountNUD";
            MaxInfectedAmountNUD.Size = new Size(148, 23);
            MaxInfectedAmountNUD.TabIndex = 8;
            MaxInfectedAmountNUD.TextAlign = HorizontalAlignment.Center;
            MaxInfectedAmountNUD.ValueChanged += MaxInfectedAmountNUD_ValueChanged;
            // 
            // PositionXNUD
            // 
            PositionXNUD.BackColor = Color.FromArgb(60, 63, 65);
            PositionXNUD.DecimalPlaces = 2;
            PositionXNUD.ForeColor = SystemColors.Control;
            PositionXNUD.Location = new Point(151, 82);
            PositionXNUD.Margin = new Padding(4, 3, 4, 3);
            PositionXNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            PositionXNUD.Name = "PositionXNUD";
            PositionXNUD.Size = new Size(148, 23);
            PositionXNUD.TabIndex = 9;
            PositionXNUD.TextAlign = HorizontalAlignment.Center;
            PositionXNUD.ValueChanged += PositionNUD_ValueChanged;
            // 
            // PositionYNUD
            // 
            PositionYNUD.BackColor = Color.FromArgb(60, 63, 65);
            PositionYNUD.DecimalPlaces = 2;
            PositionYNUD.ForeColor = SystemColors.Control;
            PositionYNUD.Location = new Point(151, 112);
            PositionYNUD.Margin = new Padding(4, 3, 4, 3);
            PositionYNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            PositionYNUD.Minimum = new decimal(new int[] { 10000, 0, 0, int.MinValue });
            PositionYNUD.Name = "PositionYNUD";
            PositionYNUD.Size = new Size(148, 23);
            PositionYNUD.TabIndex = 10;
            PositionYNUD.TextAlign = HorizontalAlignment.Center;
            PositionYNUD.ValueChanged += PositionNUD_ValueChanged;
            // 
            // PositionZNUD
            // 
            PositionZNUD.BackColor = Color.FromArgb(60, 63, 65);
            PositionZNUD.DecimalPlaces = 2;
            PositionZNUD.ForeColor = SystemColors.Control;
            PositionZNUD.Location = new Point(151, 142);
            PositionZNUD.Margin = new Padding(4, 3, 4, 3);
            PositionZNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            PositionZNUD.Name = "PositionZNUD";
            PositionZNUD.Size = new Size(148, 23);
            PositionZNUD.TabIndex = 11;
            PositionZNUD.TextAlign = HorizontalAlignment.Center;
            PositionZNUD.ValueChanged += PositionNUD_ValueChanged;
            // 
            // MinimumSpawnRadiusNUD
            // 
            MinimumSpawnRadiusNUD.BackColor = Color.FromArgb(60, 63, 65);
            MinimumSpawnRadiusNUD.DecimalPlaces = 1;
            MinimumSpawnRadiusNUD.ForeColor = SystemColors.Control;
            MinimumSpawnRadiusNUD.Location = new Point(151, 172);
            MinimumSpawnRadiusNUD.Margin = new Padding(4, 3, 4, 3);
            MinimumSpawnRadiusNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            MinimumSpawnRadiusNUD.Name = "MinimumSpawnRadiusNUD";
            MinimumSpawnRadiusNUD.Size = new Size(148, 23);
            MinimumSpawnRadiusNUD.TabIndex = 12;
            MinimumSpawnRadiusNUD.TextAlign = HorizontalAlignment.Center;
            MinimumSpawnRadiusNUD.ValueChanged += MinimumSpawnRadiusNUD_ValueChanged;
            // 
            // MaximumSpawnRadiusNUD
            // 
            MaximumSpawnRadiusNUD.BackColor = Color.FromArgb(60, 63, 65);
            MaximumSpawnRadiusNUD.DecimalPlaces = 1;
            MaximumSpawnRadiusNUD.ForeColor = SystemColors.Control;
            MaximumSpawnRadiusNUD.Location = new Point(151, 202);
            MaximumSpawnRadiusNUD.Margin = new Padding(4, 3, 4, 3);
            MaximumSpawnRadiusNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            MaximumSpawnRadiusNUD.Name = "MaximumSpawnRadiusNUD";
            MaximumSpawnRadiusNUD.Size = new Size(148, 23);
            MaximumSpawnRadiusNUD.TabIndex = 13;
            MaximumSpawnRadiusNUD.TextAlign = HorizontalAlignment.Center;
            MaximumSpawnRadiusNUD.ValueChanged += MaximumSpawnRadiusNUD_ValueChanged;
            // 
            // ExpansionMissionEventHordeControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionMissionEventHordeControl";
            Size = new Size(311, 236);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)MinInfectedAmountNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)MaxInfectedAmountNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)PositionXNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)PositionYNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)PositionZNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)MinimumSpawnRadiusNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)MaximumSpawnRadiusNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private NumericUpDown MinInfectedAmountNUD;
        private NumericUpDown MaxInfectedAmountNUD;
        private NumericUpDown PositionXNUD;
        private NumericUpDown PositionYNUD;
        private NumericUpDown PositionZNUD;
        private NumericUpDown MinimumSpawnRadiusNUD;
        private NumericUpDown MaximumSpawnRadiusNUD;
    }
}
