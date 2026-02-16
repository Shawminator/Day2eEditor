namespace ExpansionPlugin
{
    partial class ExpansionVehiclesLockConfigControl
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
            groupBox43 = new GroupBox();
            LockComplexityNUD = new NumericUpDown();
            darkLabel171 = new Label();
            darkLabel128 = new Label();
            ClassNameTB = new TextBox();
            button1 = new Button();
            groupBox43.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LockComplexityNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox43
            // 
            groupBox43.Controls.Add(button1);
            groupBox43.Controls.Add(LockComplexityNUD);
            groupBox43.Controls.Add(darkLabel171);
            groupBox43.Controls.Add(darkLabel128);
            groupBox43.Controls.Add(ClassNameTB);
            groupBox43.ForeColor = SystemColors.Control;
            groupBox43.Location = new Point(0, 0);
            groupBox43.Margin = new Padding(4, 3, 4, 3);
            groupBox43.Name = "groupBox43";
            groupBox43.Padding = new Padding(4, 3, 4, 3);
            groupBox43.Size = new Size(473, 86);
            groupBox43.TabIndex = 13;
            groupBox43.TabStop = false;
            groupBox43.Text = "Vehicle Configs";
            // 
            // LockComplexityNUD
            // 
            LockComplexityNUD.BackColor = Color.FromArgb(60, 63, 65);
            LockComplexityNUD.DecimalPlaces = 1;
            LockComplexityNUD.ForeColor = SystemColors.Control;
            LockComplexityNUD.Location = new Point(126, 53);
            LockComplexityNUD.Margin = new Padding(4, 3, 4, 3);
            LockComplexityNUD.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            LockComplexityNUD.Name = "LockComplexityNUD";
            LockComplexityNUD.Size = new Size(112, 23);
            LockComplexityNUD.TabIndex = 6;
            LockComplexityNUD.Tag = "PickLockChancePercent";
            LockComplexityNUD.TextAlign = HorizontalAlignment.Center;
            LockComplexityNUD.ValueChanged += LockComplexityNUD_ValueChanged;
            // 
            // darkLabel171
            // 
            darkLabel171.AutoSize = true;
            darkLabel171.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel171.Location = new Point(14, 55);
            darkLabel171.Margin = new Padding(4, 0, 4, 0);
            darkLabel171.Name = "darkLabel171";
            darkLabel171.Size = new Size(96, 15);
            darkLabel171.TabIndex = 5;
            darkLabel171.Text = "Lock Complexity";
            // 
            // darkLabel128
            // 
            darkLabel128.AutoSize = true;
            darkLabel128.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel128.Location = new Point(14, 25);
            darkLabel128.Margin = new Padding(4, 0, 4, 0);
            darkLabel128.Name = "darkLabel128";
            darkLabel128.Size = new Size(66, 15);
            darkLabel128.TabIndex = 3;
            darkLabel128.Text = "ClassName";
            // 
            // ClassNameTB
            // 
            ClassNameTB.BackColor = Color.FromArgb(60, 63, 65);
            ClassNameTB.ForeColor = SystemColors.Control;
            ClassNameTB.Location = new Point(126, 22);
            ClassNameTB.Margin = new Padding(4, 3, 4, 3);
            ClassNameTB.Name = "ClassNameTB";
            ClassNameTB.Size = new Size(249, 23);
            ClassNameTB.TabIndex = 4;
            ClassNameTB.Tag = "Container";
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(383, 19);
            button1.Margin = new Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new Size(82, 27);
            button1.TabIndex = 7;
            button1.Text = "Change";
            button1.Click += button1_Click;
            // 
            // ExpansionVehiclesLockConfigControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox43);
            ForeColor = SystemColors.Control;
            Name = "ExpansionVehiclesLockConfigControl";
            Size = new Size(473, 86);
            groupBox43.ResumeLayout(false);
            groupBox43.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)LockComplexityNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox43;
        private NumericUpDown LockComplexityNUD;
        private Label darkLabel171;
        private Label darkLabel128;
        private TextBox ClassNameTB;
        private Button button1;
    }
}
