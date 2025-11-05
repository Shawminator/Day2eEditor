namespace ExpansionPlugin
{
    partial class ExpansionBookDescriptionCategoryControl
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
            groupBox13 = new GroupBox();
            darkButton19 = new Button();
            darkButton20 = new Button();
            listBox12 = new ListBox();
            textBox8 = new TextBox();
            darkLabel37 = new Label();
            darkLabel38 = new Label();
            textBox10 = new TextBox();
            groupBox13.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox13
            // 
            groupBox13.Controls.Add(darkButton19);
            groupBox13.Controls.Add(darkButton20);
            groupBox13.Controls.Add(listBox12);
            groupBox13.Controls.Add(textBox8);
            groupBox13.Controls.Add(darkLabel37);
            groupBox13.Controls.Add(darkLabel38);
            groupBox13.Controls.Add(textBox10);
            groupBox13.ForeColor = SystemColors.Control;
            groupBox13.Location = new Point(0, 0);
            groupBox13.Margin = new Padding(4, 3, 4, 3);
            groupBox13.Name = "groupBox13";
            groupBox13.Padding = new Padding(4, 3, 4, 3);
            groupBox13.Size = new Size(567, 449);
            groupBox13.TabIndex = 4;
            groupBox13.TabStop = false;
            groupBox13.Text = "Book Descriptions";
            // 
            // darkButton19
            // 
            darkButton19.FlatStyle = FlatStyle.Flat;
            darkButton19.Location = new Point(96, 187);
            darkButton19.Margin = new Padding(4, 3, 4, 3);
            darkButton19.Name = "darkButton19";
            darkButton19.Size = new Size(84, 27);
            darkButton19.TabIndex = 6;
            darkButton19.Text = "Remove";
            darkButton19.Click += darkButton19_Click;
            // 
            // darkButton20
            // 
            darkButton20.FlatStyle = FlatStyle.Flat;
            darkButton20.Location = new Point(8, 187);
            darkButton20.Margin = new Padding(4, 3, 4, 3);
            darkButton20.Name = "darkButton20";
            darkButton20.Size = new Size(80, 27);
            darkButton20.TabIndex = 5;
            darkButton20.Text = "Add New";
            darkButton20.Click += darkButton20_Click;
            // 
            // listBox12
            // 
            listBox12.BackColor = Color.FromArgb(60, 63, 65);
            listBox12.DrawMode = DrawMode.OwnerDrawFixed;
            listBox12.ForeColor = SystemColors.Control;
            listBox12.FormattingEnabled = true;
            listBox12.Location = new Point(8, 65);
            listBox12.Margin = new Padding(4, 3, 4, 3);
            listBox12.Name = "listBox12";
            listBox12.Size = new Size(551, 116);
            listBox12.TabIndex = 4;
            listBox12.DrawItem += listBox_DrawItem;
            listBox12.SelectedIndexChanged += listBox12_SelectedIndexChanged;
            // 
            // textBox8
            // 
            textBox8.BackColor = Color.FromArgb(60, 63, 65);
            textBox8.ForeColor = SystemColors.Control;
            textBox8.Location = new Point(8, 251);
            textBox8.Margin = new Padding(4, 3, 4, 3);
            textBox8.Multiline = true;
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(550, 187);
            textBox8.TabIndex = 7;
            textBox8.TextChanged += textBox8_TextChanged;
            // 
            // darkLabel37
            // 
            darkLabel37.AutoSize = true;
            darkLabel37.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel37.Location = new Point(8, 233);
            darkLabel37.Margin = new Padding(4, 0, 4, 0);
            darkLabel37.Name = "darkLabel37";
            darkLabel37.Size = new Size(91, 15);
            darkLabel37.TabIndex = 97;
            darkLabel37.Text = "Description Text";
            // 
            // darkLabel38
            // 
            darkLabel38.AutoSize = true;
            darkLabel38.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel38.Location = new Point(8, 17);
            darkLabel38.Margin = new Padding(4, 0, 4, 0);
            darkLabel38.Name = "darkLabel38";
            darkLabel38.Size = new Size(90, 15);
            darkLabel38.TabIndex = 95;
            darkLabel38.Text = "Category Name";
            // 
            // textBox10
            // 
            textBox10.BackColor = Color.FromArgb(60, 63, 65);
            textBox10.ForeColor = SystemColors.Control;
            textBox10.Location = new Point(8, 35);
            textBox10.Margin = new Padding(4, 3, 4, 3);
            textBox10.Name = "textBox10";
            textBox10.Size = new Size(551, 23);
            textBox10.TabIndex = 3;
            textBox10.TextChanged += textBox10_TextChanged;
            // 
            // ExpansionBookDescriptionCategoryControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox13);
            ForeColor = SystemColors.Control;
            Name = "ExpansionBookDescriptionCategoryControl";
            Size = new Size(567, 449);
            groupBox13.ResumeLayout(false);
            groupBox13.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox13;
        private Button darkButton19;
        private Button darkButton20;
        private ListBox listBox12;
        private TextBox textBox8;
        private Label darkLabel37;
        private Label darkLabel38;
        private TextBox textBox10;
    }
}
