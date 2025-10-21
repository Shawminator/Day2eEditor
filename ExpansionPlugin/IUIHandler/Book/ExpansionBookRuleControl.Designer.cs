namespace EconomyPlugin
{
    partial class ExpansionBookRuleControl
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
            groupBox12 = new GroupBox();
            darkLabel35 = new Label();
            textBox7 = new TextBox();
            darkLabel34 = new Label();
            textBox6 = new TextBox();
            groupBox12.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox12
            // 
            groupBox12.Controls.Add(darkLabel35);
            groupBox12.Controls.Add(textBox7);
            groupBox12.Controls.Add(darkLabel34);
            groupBox12.Controls.Add(textBox6);
            groupBox12.ForeColor = SystemColors.Control;
            groupBox12.Location = new Point(0, 0);
            groupBox12.Margin = new Padding(4, 3, 4, 3);
            groupBox12.Name = "groupBox12";
            groupBox12.Padding = new Padding(4, 3, 4, 3);
            groupBox12.Size = new Size(567, 159);
            groupBox12.TabIndex = 5;
            groupBox12.TabStop = false;
            groupBox12.Text = "Book Rules";
            // 
            // darkLabel35
            // 
            darkLabel35.AutoSize = true;
            darkLabel35.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel35.Location = new Point(15, 56);
            darkLabel35.Margin = new Padding(4, 0, 4, 0);
            darkLabel35.Name = "darkLabel35";
            darkLabel35.Size = new Size(54, 15);
            darkLabel35.TabIndex = 99;
            darkLabel35.Text = "Rule Text";
            // 
            // textBox7
            // 
            textBox7.BackColor = Color.FromArgb(60, 63, 65);
            textBox7.ForeColor = SystemColors.Control;
            textBox7.Location = new Point(114, 61);
            textBox7.Margin = new Padding(4, 3, 4, 3);
            textBox7.Multiline = true;
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(445, 87);
            textBox7.TabIndex = 8;
            textBox7.TextChanged += textBox7_TextChanged;
            // 
            // darkLabel34
            // 
            darkLabel34.AutoSize = true;
            darkLabel34.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel34.Location = new Point(15, 34);
            darkLabel34.Margin = new Padding(4, 0, 4, 0);
            darkLabel34.Name = "darkLabel34";
            darkLabel34.Size = new Size(87, 15);
            darkLabel34.TabIndex = 97;
            darkLabel34.Text = "Rule Paragraph";
            // 
            // textBox6
            // 
            textBox6.BackColor = Color.FromArgb(60, 63, 65);
            textBox6.ForeColor = SystemColors.Control;
            textBox6.Location = new Point(114, 31);
            textBox6.Margin = new Padding(4, 3, 4, 3);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(445, 23);
            textBox6.TabIndex = 7;
            textBox6.TextChanged += textBox6_TextChanged;
            // 
            // ExpansionBookRuleControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox12);
            ForeColor = SystemColors.Control;
            Name = "ExpansionBookRuleControl";
            Size = new Size(567, 159);
            groupBox12.ResumeLayout(false);
            groupBox12.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox12;
        private Label darkLabel35;
        private TextBox textBox7;
        private Label darkLabel34;
        private TextBox textBox6;
    }
}
