namespace ExpansionPlugin
{
    partial class ExpansionBookRuleCategoryControl
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
            darkLabel33 = new Label();
            textBox5 = new TextBox();
            groupBox12.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox12
            // 
            groupBox12.Controls.Add(darkLabel33);
            groupBox12.Controls.Add(textBox5);
            groupBox12.ForeColor = SystemColors.Control;
            groupBox12.Location = new Point(0, 0);
            groupBox12.Margin = new Padding(4, 3, 4, 3);
            groupBox12.Name = "groupBox12";
            groupBox12.Padding = new Padding(4, 3, 4, 3);
            groupBox12.Size = new Size(567, 68);
            groupBox12.TabIndex = 5;
            groupBox12.TabStop = false;
            groupBox12.Text = "Book Rules";
            // 
            // darkLabel33
            // 
            darkLabel33.AutoSize = true;
            darkLabel33.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel33.Location = new Point(16, 34);
            darkLabel33.Margin = new Padding(4, 0, 4, 0);
            darkLabel33.Name = "darkLabel33";
            darkLabel33.Size = new Size(90, 15);
            darkLabel33.TabIndex = 95;
            darkLabel33.Text = "Category Name";
            // 
            // textBox5
            // 
            textBox5.BackColor = Color.FromArgb(60, 63, 65);
            textBox5.ForeColor = SystemColors.Control;
            textBox5.Location = new Point(114, 31);
            textBox5.Margin = new Padding(4, 3, 4, 3);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(445, 23);
            textBox5.TabIndex = 3;
            textBox5.TextChanged += textBox5_TextChanged;
            // 
            // ExpansionBookRuleCategoryControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox12);
            ForeColor = SystemColors.Control;
            Name = "ExpansionBookRuleCategoryControl";
            Size = new Size(567, 68);
            groupBox12.ResumeLayout(false);
            groupBox12.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox12;
        private Label darkLabel33;
        private TextBox textBox5;
    }
}
