namespace ExpansionPlugin
{
    partial class ExpansionHordeWaypointControl
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
            CenterXNUD = new NumericUpDown();
            CenterYNUD = new NumericUpDown();
            CenterZNUD = new NumericUpDown();
            RadiusNUD = new NumericUpDown();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)CenterXNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CenterYNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CenterZNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RadiusNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(RadiusNUD);
            groupBox1.Controls.Add(CenterZNUD);
            groupBox1.Controls.Add(CenterYNUD);
            groupBox1.Controls.Add(CenterXNUD);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(311, 146);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Waypoint";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 25);
            label1.Name = "label1";
            label1.Size = new Size(119, 15);
            label1.TabIndex = 0;
            label1.Text = "Center Position X";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 55);
            label2.Name = "label2";
            label2.Size = new Size(119, 15);
            label2.TabIndex = 1;
            label2.Text = "Center Position Y";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 85);
            label3.Name = "label3";
            label3.Size = new Size(119, 15);
            label3.TabIndex = 2;
            label3.Text = "Center Position Z";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 115);
            label4.Name = "label4";
            label4.Size = new Size(42, 15);
            label4.TabIndex = 3;
            label4.Text = "Radius";
            // 
            // CenterXNUD
            // 
            CenterXNUD.BackColor = Color.FromArgb(60, 63, 65);
            CenterXNUD.DecimalPlaces = 2;
            CenterXNUD.ForeColor = SystemColors.Control;
            CenterXNUD.Location = new Point(151, 22);
            CenterXNUD.Margin = new Padding(4, 3, 4, 3);
            CenterXNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            CenterXNUD.Name = "CenterXNUD";
            CenterXNUD.Size = new Size(148, 23);
            CenterXNUD.TabIndex = 4;
            CenterXNUD.TextAlign = HorizontalAlignment.Center;
            CenterXNUD.ValueChanged += CenterNUD_ValueChanged;
            // 
            // CenterYNUD
            // 
            CenterYNUD.BackColor = Color.FromArgb(60, 63, 65);
            CenterYNUD.DecimalPlaces = 2;
            CenterYNUD.ForeColor = SystemColors.Control;
            CenterYNUD.Location = new Point(151, 52);
            CenterYNUD.Margin = new Padding(4, 3, 4, 3);
            CenterYNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            CenterYNUD.Minimum = new decimal(new int[] { 10000, 0, 0, int.MinValue });
            CenterYNUD.Name = "CenterYNUD";
            CenterYNUD.Size = new Size(148, 23);
            CenterYNUD.TabIndex = 5;
            CenterYNUD.TextAlign = HorizontalAlignment.Center;
            CenterYNUD.ValueChanged += CenterNUD_ValueChanged;
            // 
            // CenterZNUD
            // 
            CenterZNUD.BackColor = Color.FromArgb(60, 63, 65);
            CenterZNUD.DecimalPlaces = 2;
            CenterZNUD.ForeColor = SystemColors.Control;
            CenterZNUD.Location = new Point(151, 82);
            CenterZNUD.Margin = new Padding(4, 3, 4, 3);
            CenterZNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            CenterZNUD.Name = "CenterZNUD";
            CenterZNUD.Size = new Size(148, 23);
            CenterZNUD.TabIndex = 6;
            CenterZNUD.TextAlign = HorizontalAlignment.Center;
            CenterZNUD.ValueChanged += CenterNUD_ValueChanged;
            // 
            // RadiusNUD
            // 
            RadiusNUD.BackColor = Color.FromArgb(60, 63, 65);
            RadiusNUD.DecimalPlaces = 1;
            RadiusNUD.ForeColor = SystemColors.Control;
            RadiusNUD.Location = new Point(151, 112);
            RadiusNUD.Margin = new Padding(4, 3, 4, 3);
            RadiusNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            RadiusNUD.Name = "RadiusNUD";
            RadiusNUD.Size = new Size(148, 23);
            RadiusNUD.TabIndex = 7;
            RadiusNUD.TextAlign = HorizontalAlignment.Center;
            RadiusNUD.ValueChanged += RadiusNUD_ValueChanged;
            // 
            // ExpansionHordeWaypointControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionHordeWaypointControl";
            Size = new Size(311, 146);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)CenterXNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)CenterYNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)CenterZNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)RadiusNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private NumericUpDown CenterXNUD;
        private NumericUpDown CenterYNUD;
        private NumericUpDown CenterZNUD;
        private NumericUpDown RadiusNUD;
    }
}
