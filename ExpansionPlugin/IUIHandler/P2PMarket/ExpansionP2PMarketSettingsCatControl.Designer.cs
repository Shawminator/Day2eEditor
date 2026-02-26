namespace ExpansionPlugin
{
    partial class ExpansionP2PMarketSettingsCatControl
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
            groupBox2 = new GroupBox();
            IconPathTB = new TextBox();
            label2 = new Label();
            label1 = new Label();
            DisplayNameTB = new TextBox();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(IconPathTB);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(DisplayNameTB);
            groupBox2.ForeColor = SystemColors.Control;
            groupBox2.Location = new Point(0, 0);
            groupBox2.Margin = new Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 3, 4, 3);
            groupBox2.Size = new Size(519, 90);
            groupBox2.TabIndex = 328;
            groupBox2.TabStop = false;
            // 
            // IconPathTB
            // 
            IconPathTB.BackColor = Color.FromArgb(60, 63, 65);
            IconPathTB.ForeColor = SystemColors.Control;
            IconPathTB.Location = new Point(143, 52);
            IconPathTB.Margin = new Padding(4, 3, 4, 3);
            IconPathTB.Name = "IconPathTB";
            IconPathTB.Size = new Size(363, 23);
            IconPathTB.TabIndex = 307;
            IconPathTB.TextChanged += IconPathTB_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 55);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 306;
            label2.Text = "Icon Path";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 25);
            label1.Name = "label1";
            label1.Size = new Size(80, 15);
            label1.TabIndex = 305;
            label1.Text = "Display Name";
            // 
            // DisplayNameTB
            // 
            DisplayNameTB.BackColor = Color.FromArgb(60, 63, 65);
            DisplayNameTB.ForeColor = SystemColors.Control;
            DisplayNameTB.Location = new Point(143, 22);
            DisplayNameTB.Margin = new Padding(4, 3, 4, 3);
            DisplayNameTB.Name = "DisplayNameTB";
            DisplayNameTB.Size = new Size(363, 23);
            DisplayNameTB.TabIndex = 304;
            DisplayNameTB.TextChanged += DisplayNameTB_TextChanged;
            // 
            // ExpansionP2PMarketSettingsCatControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox2);
            ForeColor = SystemColors.Control;
            Name = "ExpansionP2PMarketSettingsCatControl";
            Size = new Size(519, 90);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox2;
        private TextBox IconPathTB;
        private Label label2;
        private Label label1;
        private TextBox DisplayNameTB;
    }
}
