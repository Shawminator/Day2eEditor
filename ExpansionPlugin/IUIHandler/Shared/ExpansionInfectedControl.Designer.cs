namespace ExpansionPlugin
{
    partial class ExpansionInfectedControl
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
            textBox1 = new TextBox();
            button2 = new Button();
            label3 = new Label();
            button1 = new Button();
            ExpansionLootitemSetAllChanceButton = new Button();
            listBox2 = new ListBox();
            listBox1 = new ListBox();
            label2 = new Label();
            label1 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(ExpansionLootitemSetAllChanceButton);
            groupBox1.Controls.Add(listBox2);
            groupBox1.Controls.Add(listBox1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(643, 629);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Infected";
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(60, 63, 65);
            textBox1.ForeColor = SystemColors.Control;
            textBox1.Location = new Point(7, 560);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(627, 23);
            textBox1.TabIndex = 229;
            // 
            // button2
            // 
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(9, 589);
            button2.Margin = new Padding(4, 3, 4, 3);
            button2.Name = "button2";
            button2.Size = new Size(625, 27);
            button2.TabIndex = 228;
            button2.Text = "Add String";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 542);
            label3.Name = "label3";
            label3.Size = new Size(187, 15);
            label3.TabIndex = 227;
            label3.Text = "Add String (Use this for adding AI)";
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(323, 512);
            button1.Margin = new Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new Size(311, 27);
            button1.TabIndex = 226;
            button1.Text = "Add Selected";
            // 
            // ExpansionLootitemSetAllChanceButton
            // 
            ExpansionLootitemSetAllChanceButton.FlatStyle = FlatStyle.Flat;
            ExpansionLootitemSetAllChanceButton.Location = new Point(7, 512);
            ExpansionLootitemSetAllChanceButton.Margin = new Padding(4, 3, 4, 3);
            ExpansionLootitemSetAllChanceButton.Name = "ExpansionLootitemSetAllChanceButton";
            ExpansionLootitemSetAllChanceButton.Size = new Size(308, 27);
            ExpansionLootitemSetAllChanceButton.TabIndex = 225;
            ExpansionLootitemSetAllChanceButton.Text = "Remove Selected";
            // 
            // listBox2
            // 
            listBox2.BackColor = Color.FromArgb(60, 63, 65);
            listBox2.DrawMode = DrawMode.OwnerDrawFixed;
            listBox2.ForeColor = SystemColors.Control;
            listBox2.FormattingEnabled = true;
            listBox2.Location = new Point(323, 37);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(311, 468);
            listBox2.TabIndex = 3;
            listBox2.DrawItem += listBox_DrawItem;
            // 
            // listBox1
            // 
            listBox1.BackColor = Color.FromArgb(60, 63, 65);
            listBox1.DrawMode = DrawMode.OwnerDrawFixed;
            listBox1.ForeColor = SystemColors.Control;
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(6, 37);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(311, 468);
            listBox1.TabIndex = 2;
            listBox1.DrawItem += listBox_DrawItem;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(323, 19);
            label2.Name = "label2";
            label2.Size = new Size(93, 15);
            label2.TabIndex = 1;
            label2.Text = "To Choose From";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 19);
            label1.Name = "label1";
            label1.Size = new Size(53, 15);
            label1.TabIndex = 0;
            label1.Text = "Included";
            // 
            // ExpansionInfectedControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionInfectedControl";
            Size = new Size(643, 629);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private ListBox listBox2;
        private ListBox listBox1;
        private Label label2;
        private Label label1;
        private TextBox textBox1;
        private Button button2;
        private Label label3;
        private Button button1;
        private Button ExpansionLootitemSetAllChanceButton;
    }
}
