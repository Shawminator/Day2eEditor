namespace ExpansionPlugin
{
    partial class ExpansionBookLinksControl
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
            groupBox30 = new GroupBox();
            LinkIconColour = new PictureBox();
            comboBox5 = new ComboBox();
            darkLabel86 = new Label();
            darkLabel85 = new Label();
            darkLabel84 = new Label();
            textBox13 = new TextBox();
            darkLabel83 = new Label();
            textBox12 = new TextBox();
            groupBox30.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LinkIconColour).BeginInit();
            SuspendLayout();
            // 
            // groupBox30
            // 
            groupBox30.Controls.Add(LinkIconColour);
            groupBox30.Controls.Add(comboBox5);
            groupBox30.Controls.Add(darkLabel86);
            groupBox30.Controls.Add(darkLabel85);
            groupBox30.Controls.Add(darkLabel84);
            groupBox30.Controls.Add(textBox13);
            groupBox30.Controls.Add(darkLabel83);
            groupBox30.Controls.Add(textBox12);
            groupBox30.ForeColor = SystemColors.Control;
            groupBox30.Location = new Point(0, 0);
            groupBox30.Margin = new Padding(4, 3, 4, 3);
            groupBox30.Name = "groupBox30";
            groupBox30.Padding = new Padding(4, 3, 4, 3);
            groupBox30.Size = new Size(567, 142);
            groupBox30.TabIndex = 6;
            groupBox30.TabStop = false;
            groupBox30.Text = "Book Links";
            // 
            // LinkIconColour
            // 
            LinkIconColour.Location = new Point(117, 115);
            LinkIconColour.Margin = new Padding(4, 3, 4, 3);
            LinkIconColour.Name = "LinkIconColour";
            LinkIconColour.Size = new Size(275, 15);
            LinkIconColour.TabIndex = 120;
            LinkIconColour.TabStop = false;
            LinkIconColour.Click += LinkIconColour_Click;
            // 
            // comboBox5
            // 
            comboBox5.BackColor = Color.FromArgb(60, 63, 65);
            comboBox5.ForeColor = SystemColors.Control;
            comboBox5.FormattingEnabled = true;
            comboBox5.Items.AddRange(new object[] { "Homepage", "Feedback", "Discord", "Patreon", "Steam", "Reddit", "GitHub", "YouTube", "Twitter" });
            comboBox5.Location = new Point(117, 82);
            comboBox5.Margin = new Padding(4, 3, 4, 3);
            comboBox5.Name = "comboBox5";
            comboBox5.Size = new Size(427, 23);
            comboBox5.TabIndex = 5;
            comboBox5.SelectedIndexChanged += comboBox5_SelectedIndexChanged;
            // 
            // darkLabel86
            // 
            darkLabel86.AutoSize = true;
            darkLabel86.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel86.Location = new Point(18, 115);
            darkLabel86.Margin = new Padding(4, 0, 4, 0);
            darkLabel86.Name = "darkLabel86";
            darkLabel86.Size = new Size(62, 15);
            darkLabel86.TabIndex = 116;
            darkLabel86.Text = "Icon Color";
            // 
            // darkLabel85
            // 
            darkLabel85.AutoSize = true;
            darkLabel85.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel85.Location = new Point(18, 85);
            darkLabel85.Margin = new Padding(4, 0, 4, 0);
            darkLabel85.Name = "darkLabel85";
            darkLabel85.Size = new Size(30, 15);
            darkLabel85.TabIndex = 114;
            darkLabel85.Text = "Icon";
            // 
            // darkLabel84
            // 
            darkLabel84.AutoSize = true;
            darkLabel84.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel84.Location = new Point(18, 55);
            darkLabel84.Margin = new Padding(4, 0, 4, 0);
            darkLabel84.Name = "darkLabel84";
            darkLabel84.Size = new Size(28, 15);
            darkLabel84.TabIndex = 112;
            darkLabel84.Text = "URL";
            // 
            // textBox13
            // 
            textBox13.BackColor = Color.FromArgb(60, 63, 65);
            textBox13.ForeColor = SystemColors.Control;
            textBox13.Location = new Point(117, 52);
            textBox13.Margin = new Padding(4, 3, 4, 3);
            textBox13.Name = "textBox13";
            textBox13.Size = new Size(427, 23);
            textBox13.TabIndex = 4;
            textBox13.TextChanged += textBox13_TextChanged;
            // 
            // darkLabel83
            // 
            darkLabel83.AutoSize = true;
            darkLabel83.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel83.Location = new Point(18, 25);
            darkLabel83.Margin = new Padding(4, 0, 4, 0);
            darkLabel83.Name = "darkLabel83";
            darkLabel83.Size = new Size(64, 15);
            darkLabel83.TabIndex = 110;
            darkLabel83.Text = "Link Name";
            // 
            // textBox12
            // 
            textBox12.BackColor = Color.FromArgb(60, 63, 65);
            textBox12.ForeColor = SystemColors.Control;
            textBox12.Location = new Point(117, 22);
            textBox12.Margin = new Padding(4, 3, 4, 3);
            textBox12.Name = "textBox12";
            textBox12.Size = new Size(427, 23);
            textBox12.TabIndex = 3;
            textBox12.TextChanged += textBox12_TextChanged;
            // 
            // BookLinksControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox30);
            ForeColor = SystemColors.Control;
            Name = "BookLinksControl";
            Size = new Size(567, 142);
            groupBox30.ResumeLayout(false);
            groupBox30.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)LinkIconColour).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox30;
        private ComboBox comboBox5;
        private Label darkLabel86;
        private Label darkLabel85;
        private Label darkLabel84;
        private TextBox textBox13;
        private Label darkLabel83;
        private TextBox textBox12;
        private PictureBox LinkIconColour;
    }
}
