namespace ExpansionPlugin
{
    partial class ExpansionTraderMapsSpecialNameControl
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
            darkLabel141 = new Label();
            FilenameTB = new TextBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(FilenameTB);
            groupBox1.Controls.Add(darkLabel141);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(556, 55);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Special";
            // 
            // darkLabel141
            // 
            darkLabel141.AutoSize = true;
            darkLabel141.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel141.Location = new Point(14, 25);
            darkLabel141.Margin = new Padding(4, 0, 4, 0);
            darkLabel141.Name = "darkLabel141";
            darkLabel141.Size = new Size(39, 15);
            darkLabel141.TabIndex = 195;
            darkLabel141.Text = "Name";
            // 
            // FilenameTB
            // 
            FilenameTB.BackColor = Color.FromArgb(60, 63, 65);
            FilenameTB.ForeColor = SystemColors.Control;
            FilenameTB.Location = new Point(113, 22);
            FilenameTB.Margin = new Padding(4, 3, 4, 3);
            FilenameTB.Name = "FilenameTB";
            FilenameTB.Size = new Size(430, 23);
            FilenameTB.TabIndex = 177;
            FilenameTB.TextChanged += FilenameTB_TextChanged;
            // 
            // ExpansionTraderMapsSpecialNameControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionTraderMapsSpecialNameControl";
            Size = new Size(566, 61);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label darkLabel141;
        private TextBox FilenameTB;
    }
}
