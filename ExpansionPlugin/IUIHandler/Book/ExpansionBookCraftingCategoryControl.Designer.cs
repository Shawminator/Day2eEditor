namespace EconomyPlugin
{
    partial class ExpansionBookCraftingCategoryControl
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
            groupBox31 = new GroupBox();
            darkLabel88 = new Label();
            darkButton35 = new Button();
            darkButton36 = new Button();
            listBox20 = new ListBox();
            darkLabel87 = new Label();
            textBox14 = new TextBox();
            groupBox31.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox31
            // 
            groupBox31.Controls.Add(darkLabel88);
            groupBox31.Controls.Add(darkButton35);
            groupBox31.Controls.Add(darkButton36);
            groupBox31.Controls.Add(listBox20);
            groupBox31.Controls.Add(darkLabel87);
            groupBox31.Controls.Add(textBox14);
            groupBox31.ForeColor = SystemColors.Control;
            groupBox31.Location = new Point(0, 0);
            groupBox31.Margin = new Padding(4, 3, 4, 3);
            groupBox31.Name = "groupBox31";
            groupBox31.Padding = new Padding(4, 3, 4, 3);
            groupBox31.Size = new Size(266, 500);
            groupBox31.TabIndex = 7;
            groupBox31.TabStop = false;
            groupBox31.Text = "Book Crafting Categories";
            // 
            // darkLabel88
            // 
            darkLabel88.AutoSize = true;
            darkLabel88.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel88.Location = new Point(8, 68);
            darkLabel88.Margin = new Padding(4, 0, 4, 0);
            darkLabel88.Name = "darkLabel88";
            darkLabel88.Size = new Size(90, 15);
            darkLabel88.TabIndex = 114;
            darkLabel88.Text = "Category Name";
            // 
            // darkButton35
            // 
            darkButton35.FlatStyle = FlatStyle.Flat;
            darkButton35.Location = new Point(173, 465);
            darkButton35.Margin = new Padding(4, 3, 4, 3);
            darkButton35.Name = "darkButton35";
            darkButton35.Size = new Size(84, 27);
            darkButton35.TabIndex = 7;
            darkButton35.Text = "Remove";
            darkButton35.Click += darkButton35_Click;
            // 
            // darkButton36
            // 
            darkButton36.FlatStyle = FlatStyle.Flat;
            darkButton36.Location = new Point(8, 465);
            darkButton36.Margin = new Padding(4, 3, 4, 3);
            darkButton36.Name = "darkButton36";
            darkButton36.Size = new Size(80, 27);
            darkButton36.TabIndex = 6;
            darkButton36.Text = "Add New";
            darkButton36.Click += darkButton36_Click;
            // 
            // listBox20
            // 
            listBox20.BackColor = Color.FromArgb(60, 63, 65);
            listBox20.DrawMode = DrawMode.OwnerDrawFixed;
            listBox20.ForeColor = SystemColors.Control;
            listBox20.FormattingEnabled = true;
            listBox20.Location = new Point(7, 87);
            listBox20.Margin = new Padding(4, 3, 4, 3);
            listBox20.Name = "listBox20";
            listBox20.Size = new Size(251, 372);
            listBox20.TabIndex = 5;
            listBox20.DrawItem += listBox_DrawItem;
            // 
            // darkLabel87
            // 
            darkLabel87.AutoSize = true;
            darkLabel87.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel87.Location = new Point(5, 21);
            darkLabel87.Margin = new Padding(4, 0, 4, 0);
            darkLabel87.Name = "darkLabel87";
            darkLabel87.Size = new Size(90, 15);
            darkLabel87.TabIndex = 4;
            darkLabel87.Text = "Category Name";
            // 
            // textBox14
            // 
            textBox14.BackColor = Color.FromArgb(60, 63, 65);
            textBox14.ForeColor = SystemColors.Control;
            textBox14.Location = new Point(7, 39);
            textBox14.Margin = new Padding(4, 3, 4, 3);
            textBox14.Name = "textBox14";
            textBox14.Size = new Size(251, 23);
            textBox14.TabIndex = 4;
            textBox14.TextChanged += textBox14_TextChanged;
            // 
            // ExpansionBookCraftingCategoryControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox31);
            ForeColor = SystemColors.Control;
            Name = "ExpansionBookCraftingCategoryControl";
            Size = new Size(266, 500);
            groupBox31.ResumeLayout(false);
            groupBox31.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox31;
        private Label darkLabel88;
        private Button darkButton35;
        private Button darkButton36;
        private ListBox listBox20;
        private Label darkLabel87;
        private TextBox textBox14;
    }
}
