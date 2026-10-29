namespace CaparePlugin
{
    partial class CapareKeyCardLootLocationControl
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
            PositionL = new Label();
            PositionXNUD = new NumericUpDown();
            PositionYNUD = new NumericUpDown();
            PositionZNUD = new NumericUpDown();
            OrientationL = new Label();
            OrientationXNUD = new NumericUpDown();
            OrientationYNUD = new NumericUpDown();
            OrientationZNUD = new NumericUpDown();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PositionXNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PositionYNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PositionZNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)OrientationXNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)OrientationYNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)OrientationZNUD).BeginInit();
            SuspendLayout();
            //
            // groupBox1
            //
            groupBox1.Controls.Add(PositionL);
            groupBox1.Controls.Add(PositionXNUD);
            groupBox1.Controls.Add(PositionYNUD);
            groupBox1.Controls.Add(PositionZNUD);
            groupBox1.Controls.Add(OrientationL);
            groupBox1.Controls.Add(OrientationXNUD);
            groupBox1.Controls.Add(OrientationYNUD);
            groupBox1.Controls.Add(OrientationZNUD);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(435, 86);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "Loot Spawn Point";
            //
            // PositionL
            //
            PositionL.AutoSize = true;
            PositionL.ForeColor = Color.FromArgb(220, 220, 220);
            PositionL.Location = new Point(11, 25);
            PositionL.Margin = new Padding(4, 0, 4, 0);
            PositionL.Name = "PositionL";
            PositionL.Size = new Size(56, 15);
            PositionL.TabIndex = 0;
            PositionL.Text = "Position";
            //
            // PositionXNUD
            //
            PositionXNUD.BackColor = Color.FromArgb(60, 63, 65);
            PositionXNUD.DecimalPlaces = 3;
            PositionXNUD.ForeColor = SystemColors.Control;
            PositionXNUD.Location = new Point(190, 22);
            PositionXNUD.Margin = new Padding(4, 3, 4, 3);
            PositionXNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            PositionXNUD.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            PositionXNUD.Name = "PositionXNUD";
            PositionXNUD.Size = new Size(74, 23);
            PositionXNUD.TabIndex = 1;
            PositionXNUD.TextAlign = HorizontalAlignment.Center;
            PositionXNUD.ValueChanged += PositionXNUD_ValueChanged;
            //
            // PositionYNUD
            //
            PositionYNUD.BackColor = Color.FromArgb(60, 63, 65);
            PositionYNUD.DecimalPlaces = 3;
            PositionYNUD.ForeColor = SystemColors.Control;
            PositionYNUD.Location = new Point(268, 22);
            PositionYNUD.Margin = new Padding(4, 3, 4, 3);
            PositionYNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            PositionYNUD.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            PositionYNUD.Name = "PositionYNUD";
            PositionYNUD.Size = new Size(74, 23);
            PositionYNUD.TabIndex = 2;
            PositionYNUD.TextAlign = HorizontalAlignment.Center;
            PositionYNUD.ValueChanged += PositionYNUD_ValueChanged;
            //
            // PositionZNUD
            //
            PositionZNUD.BackColor = Color.FromArgb(60, 63, 65);
            PositionZNUD.DecimalPlaces = 3;
            PositionZNUD.ForeColor = SystemColors.Control;
            PositionZNUD.Location = new Point(346, 22);
            PositionZNUD.Margin = new Padding(4, 3, 4, 3);
            PositionZNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            PositionZNUD.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            PositionZNUD.Name = "PositionZNUD";
            PositionZNUD.Size = new Size(74, 23);
            PositionZNUD.TabIndex = 3;
            PositionZNUD.TextAlign = HorizontalAlignment.Center;
            PositionZNUD.ValueChanged += PositionZNUD_ValueChanged;
            //
            // OrientationL
            //
            OrientationL.AutoSize = true;
            OrientationL.ForeColor = Color.FromArgb(220, 220, 220);
            OrientationL.Location = new Point(11, 54);
            OrientationL.Margin = new Padding(4, 0, 4, 0);
            OrientationL.Name = "OrientationL";
            OrientationL.Size = new Size(77, 15);
            OrientationL.TabIndex = 5;
            OrientationL.Text = "Orientation";
            //
            // OrientationXNUD
            //
            OrientationXNUD.BackColor = Color.FromArgb(60, 63, 65);
            OrientationXNUD.DecimalPlaces = 3;
            OrientationXNUD.ForeColor = SystemColors.Control;
            OrientationXNUD.Location = new Point(190, 51);
            OrientationXNUD.Margin = new Padding(4, 3, 4, 3);
            OrientationXNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            OrientationXNUD.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            OrientationXNUD.Name = "OrientationXNUD";
            OrientationXNUD.Size = new Size(74, 23);
            OrientationXNUD.TabIndex = 6;
            OrientationXNUD.TextAlign = HorizontalAlignment.Center;
            OrientationXNUD.ValueChanged += OrientationXNUD_ValueChanged;
            //
            // OrientationYNUD
            //
            OrientationYNUD.BackColor = Color.FromArgb(60, 63, 65);
            OrientationYNUD.DecimalPlaces = 3;
            OrientationYNUD.ForeColor = SystemColors.Control;
            OrientationYNUD.Location = new Point(268, 51);
            OrientationYNUD.Margin = new Padding(4, 3, 4, 3);
            OrientationYNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            OrientationYNUD.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            OrientationYNUD.Name = "OrientationYNUD";
            OrientationYNUD.Size = new Size(74, 23);
            OrientationYNUD.TabIndex = 7;
            OrientationYNUD.TextAlign = HorizontalAlignment.Center;
            OrientationYNUD.ValueChanged += OrientationYNUD_ValueChanged;
            //
            // OrientationZNUD
            //
            OrientationZNUD.BackColor = Color.FromArgb(60, 63, 65);
            OrientationZNUD.DecimalPlaces = 3;
            OrientationZNUD.ForeColor = SystemColors.Control;
            OrientationZNUD.Location = new Point(346, 51);
            OrientationZNUD.Margin = new Padding(4, 3, 4, 3);
            OrientationZNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            OrientationZNUD.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            OrientationZNUD.Name = "OrientationZNUD";
            OrientationZNUD.Size = new Size(74, 23);
            OrientationZNUD.TabIndex = 8;
            OrientationZNUD.TextAlign = HorizontalAlignment.Center;
            OrientationZNUD.ValueChanged += OrientationZNUD_ValueChanged;
            //
            // CapareKeyCardLootLocationControl
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "CapareKeyCardLootLocationControl";
            Size = new Size(435, 92);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PositionXNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)PositionYNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)PositionZNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)OrientationXNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)OrientationYNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)OrientationZNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label PositionL;
        private NumericUpDown PositionXNUD;
        private NumericUpDown PositionYNUD;
        private NumericUpDown PositionZNUD;
        private Label OrientationL;
        private NumericUpDown OrientationXNUD;
        private NumericUpDown OrientationYNUD;
        private NumericUpDown OrientationZNUD;
    }
}
