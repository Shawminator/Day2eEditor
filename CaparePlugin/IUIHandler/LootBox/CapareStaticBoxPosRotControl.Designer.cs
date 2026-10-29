namespace CaparePlugin
{
    partial class CapareStaticBoxPosRotControl
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
            RotationL = new Label();
            RotationXNUD = new NumericUpDown();
            RotationYNUD = new NumericUpDown();
            RotationZNUD = new NumericUpDown();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PositionXNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PositionYNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PositionZNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RotationXNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RotationYNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RotationZNUD).BeginInit();
            SuspendLayout();
            //
            // groupBox1
            //
            groupBox1.Controls.Add(PositionL);
            groupBox1.Controls.Add(PositionXNUD);
            groupBox1.Controls.Add(PositionYNUD);
            groupBox1.Controls.Add(PositionZNUD);
            groupBox1.Controls.Add(RotationL);
            groupBox1.Controls.Add(RotationXNUD);
            groupBox1.Controls.Add(RotationYNUD);
            groupBox1.Controls.Add(RotationZNUD);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(435, 86);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "Static Box Position";
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
            // RotationL
            //
            RotationL.AutoSize = true;
            RotationL.ForeColor = Color.FromArgb(220, 220, 220);
            RotationL.Location = new Point(11, 54);
            RotationL.Margin = new Padding(4, 0, 4, 0);
            RotationL.Name = "RotationL";
            RotationL.Size = new Size(56, 15);
            RotationL.TabIndex = 5;
            RotationL.Text = "Rotation";
            //
            // RotationXNUD
            //
            RotationXNUD.BackColor = Color.FromArgb(60, 63, 65);
            RotationXNUD.DecimalPlaces = 3;
            RotationXNUD.ForeColor = SystemColors.Control;
            RotationXNUD.Location = new Point(190, 51);
            RotationXNUD.Margin = new Padding(4, 3, 4, 3);
            RotationXNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            RotationXNUD.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            RotationXNUD.Name = "RotationXNUD";
            RotationXNUD.Size = new Size(74, 23);
            RotationXNUD.TabIndex = 6;
            RotationXNUD.TextAlign = HorizontalAlignment.Center;
            RotationXNUD.ValueChanged += RotationXNUD_ValueChanged;
            //
            // RotationYNUD
            //
            RotationYNUD.BackColor = Color.FromArgb(60, 63, 65);
            RotationYNUD.DecimalPlaces = 3;
            RotationYNUD.ForeColor = SystemColors.Control;
            RotationYNUD.Location = new Point(268, 51);
            RotationYNUD.Margin = new Padding(4, 3, 4, 3);
            RotationYNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            RotationYNUD.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            RotationYNUD.Name = "RotationYNUD";
            RotationYNUD.Size = new Size(74, 23);
            RotationYNUD.TabIndex = 7;
            RotationYNUD.TextAlign = HorizontalAlignment.Center;
            RotationYNUD.ValueChanged += RotationYNUD_ValueChanged;
            //
            // RotationZNUD
            //
            RotationZNUD.BackColor = Color.FromArgb(60, 63, 65);
            RotationZNUD.DecimalPlaces = 3;
            RotationZNUD.ForeColor = SystemColors.Control;
            RotationZNUD.Location = new Point(346, 51);
            RotationZNUD.Margin = new Padding(4, 3, 4, 3);
            RotationZNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            RotationZNUD.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            RotationZNUD.Name = "RotationZNUD";
            RotationZNUD.Size = new Size(74, 23);
            RotationZNUD.TabIndex = 8;
            RotationZNUD.TextAlign = HorizontalAlignment.Center;
            RotationZNUD.ValueChanged += RotationZNUD_ValueChanged;
            //
            // CapareStaticBoxPosRotControl
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "CapareStaticBoxPosRotControl";
            Size = new Size(435, 92);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PositionXNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)PositionYNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)PositionZNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)RotationXNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)RotationYNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)RotationZNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label PositionL;
        private NumericUpDown PositionXNUD;
        private NumericUpDown PositionYNUD;
        private NumericUpDown PositionZNUD;
        private Label RotationL;
        private NumericUpDown RotationXNUD;
        private NumericUpDown RotationYNUD;
        private NumericUpDown RotationZNUD;
    }
}
