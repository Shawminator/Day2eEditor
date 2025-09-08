namespace EconomyPlugin
{
    partial class cfglimitsdefinitionTagsControl
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
            groupBox79 = new GroupBox();
            darkButton83 = new Button();
            textBox3 = new TextBox();
            darkButton76 = new Button();
            darkButton27 = new Button();
            listBox9 = new ListBox();
            groupBox79.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox79
            // 
            groupBox79.Controls.Add(darkButton83);
            groupBox79.Controls.Add(textBox3);
            groupBox79.Controls.Add(darkButton76);
            groupBox79.Controls.Add(darkButton27);
            groupBox79.Controls.Add(listBox9);
            groupBox79.ForeColor = SystemColors.Control;
            groupBox79.Location = new Point(0, 0);
            groupBox79.Margin = new Padding(4, 3, 4, 3);
            groupBox79.Name = "groupBox79";
            groupBox79.Padding = new Padding(4, 3, 4, 3);
            groupBox79.Size = new Size(329, 475);
            groupBox79.TabIndex = 106;
            groupBox79.TabStop = false;
            groupBox79.Text = "Tags Definitions";
            // 
            // darkButton83
            // 
            darkButton83.FlatStyle = FlatStyle.Flat;
            darkButton83.Location = new Point(219, 442);
            darkButton83.Margin = new Padding(4, 3, 4, 3);
            darkButton83.Name = "darkButton83";
            darkButton83.Size = new Size(100, 27);
            darkButton83.TabIndex = 142;
            darkButton83.Text = "Update";
            darkButton83.Visible = false;
            darkButton83.Click += darkButton83_Click;
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.FromArgb(60, 63, 65);
            textBox3.ForeColor = SystemColors.Control;
            textBox3.Location = new Point(8, 413);
            textBox3.Margin = new Padding(4, 3, 4, 3);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(312, 23);
            textBox3.TabIndex = 139;
            textBox3.TextChanged += textBox3_TextChanged;
            // 
            // darkButton76
            // 
            darkButton76.FlatStyle = FlatStyle.Flat;
            darkButton76.Location = new Point(113, 442);
            darkButton76.Margin = new Padding(4, 3, 4, 3);
            darkButton76.Name = "darkButton76";
            darkButton76.Size = new Size(99, 27);
            darkButton76.TabIndex = 138;
            darkButton76.Text = "Remove";
            darkButton76.Click += darkButton76_Click;
            // 
            // darkButton27
            // 
            darkButton27.FlatStyle = FlatStyle.Flat;
            darkButton27.Location = new Point(7, 442);
            darkButton27.Margin = new Padding(4, 3, 4, 3);
            darkButton27.Name = "darkButton27";
            darkButton27.Size = new Size(99, 27);
            darkButton27.TabIndex = 137;
            darkButton27.Text = "Add";
            darkButton27.Click += darkButton27_Click;
            // 
            // listBox9
            // 
            listBox9.BackColor = Color.FromArgb(60, 63, 65);
            listBox9.DrawMode = DrawMode.OwnerDrawFixed;
            listBox9.ForeColor = SystemColors.Control;
            listBox9.FormattingEnabled = true;
            listBox9.Location = new Point(7, 22);
            listBox9.Margin = new Padding(4, 3, 4, 3);
            listBox9.Name = "listBox9";
            listBox9.Size = new Size(312, 388);
            listBox9.TabIndex = 98;
            listBox9.DrawItem += listBox_DrawItem;
            listBox9.SelectedIndexChanged += listBox9_SelectedIndexChanged;
            // 
            // cfglimitsdefinitionTagsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox79);
            ForeColor = SystemColors.Control;
            Name = "cfglimitsdefinitionTagsControl";
            Size = new Size(339, 482);
            groupBox79.ResumeLayout(false);
            groupBox79.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox79;
        private Button darkButton83;
        private TextBox textBox3;
        private Button darkButton76;
        private Button darkButton27;
        private ListBox listBox9;
    }
}
