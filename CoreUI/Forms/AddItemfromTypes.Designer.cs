namespace Day2eEditor
{
    partial class AddItemfromTypes
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            CloseButton = new Button();
            TitleLabel = new Label();
            darkButton1 = new Button();
            darkButton2 = new Button();
            listBox1 = new ListBox();
            darkButton3 = new Button();
            darkButton4 = new Button();
            darkButton5 = new Button();
            darkTextBox1 = new TextBox();
            darkButton6 = new Button();
            darkButton7 = new Button();
            treeViewMS1 = new MultiSelectTreeView();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            darkButton8 = new Button();
            richTextBox1 = new RichTextBox();
            panel1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Black;
            panel1.Controls.Add(CloseButton);
            panel1.Controls.Add(TitleLabel);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(783, 32);
            panel1.TabIndex = 8;
            // 
            // CloseButton
            // 
            CloseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CloseButton.BackColor = Color.Black;
            CloseButton.FlatStyle = FlatStyle.Popup;
            CloseButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CloseButton.ForeColor = Color.DarkRed;
            CloseButton.Location = new Point(732, -1);
            CloseButton.Margin = new Padding(4, 3, 4, 3);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(48, 32);
            CloseButton.TabIndex = 7;
            CloseButton.Text = "X";
            CloseButton.UseVisualStyleBackColor = false;
            // 
            // TitleLabel
            // 
            TitleLabel.AutoSize = true;
            TitleLabel.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TitleLabel.ForeColor = Color.FromArgb(75, 110, 175);
            TitleLabel.Location = new Point(6, 7);
            TitleLabel.Margin = new Padding(4, 0, 4, 0);
            TitleLabel.Name = "TitleLabel";
            TitleLabel.Size = new Size(124, 15);
            TitleLabel.TabIndex = 6;
            TitleLabel.Text = "Add Items from Types";
            // 
            // darkButton1
            // 
            darkButton1.DialogResult = DialogResult.OK;
            darkButton1.FlatStyle = FlatStyle.Flat;
            darkButton1.Location = new Point(644, 508);
            darkButton1.Margin = new Padding(4, 3, 4, 3);
            darkButton1.Name = "darkButton1";
            darkButton1.Size = new Size(124, 27);
            darkButton1.TabIndex = 10;
            darkButton1.Text = "OK";
            // 
            // darkButton2
            // 
            darkButton2.DialogResult = DialogResult.Cancel;
            darkButton2.FlatStyle = FlatStyle.Flat;
            darkButton2.Location = new Point(513, 508);
            darkButton2.Margin = new Padding(4, 3, 4, 3);
            darkButton2.Name = "darkButton2";
            darkButton2.Size = new Size(124, 27);
            darkButton2.TabIndex = 11;
            darkButton2.Text = "Cancel";
            // 
            // listBox1
            // 
            listBox1.BackColor = Color.FromArgb(60, 63, 65);
            listBox1.DrawMode = DrawMode.OwnerDrawFixed;
            listBox1.ForeColor = SystemColors.Control;
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(513, 69);
            listBox1.Margin = new Padding(4, 3, 4, 3);
            listBox1.Name = "listBox1";
            listBox1.SelectionMode = SelectionMode.MultiExtended;
            listBox1.Size = new Size(254, 420);
            listBox1.TabIndex = 12;
            listBox1.DrawItem += listBox_DrawItem;
            // 
            // darkButton3
            // 
            darkButton3.FlatStyle = FlatStyle.Flat;
            darkButton3.Location = new Point(516, 39);
            darkButton3.Margin = new Padding(4, 3, 4, 3);
            darkButton3.Name = "darkButton3";
            darkButton3.Size = new Size(124, 27);
            darkButton3.TabIndex = 14;
            darkButton3.Text = "Remove from List";
            darkButton3.Click += RemoveItemsButton_Click;
            // 
            // darkButton4
            // 
            darkButton4.FlatStyle = FlatStyle.Flat;
            darkButton4.Location = new Point(646, 39);
            darkButton4.Margin = new Padding(4, 3, 4, 3);
            darkButton4.Name = "darkButton4";
            darkButton4.Size = new Size(124, 27);
            darkButton4.TabIndex = 13;
            darkButton4.Text = "Add To List";
            darkButton4.Click += darkButton4_Click;
            // 
            // darkButton5
            // 
            darkButton5.FlatStyle = FlatStyle.Flat;
            darkButton5.Location = new Point(372, 7);
            darkButton5.Margin = new Padding(4, 3, 4, 3);
            darkButton5.Name = "darkButton5";
            darkButton5.Size = new Size(124, 27);
            darkButton5.TabIndex = 15;
            darkButton5.Tag = "HideUsed";
            darkButton5.Text = "Hide Used Types";
            darkButton5.Click += darkButton5_Click;
            // 
            // darkTextBox1
            // 
            darkTextBox1.BackColor = Color.FromArgb(69, 73, 74);
            darkTextBox1.BorderStyle = BorderStyle.FixedSingle;
            darkTextBox1.ForeColor = Color.FromArgb(220, 220, 220);
            darkTextBox1.Location = new Point(7, 7);
            darkTextBox1.Margin = new Padding(4, 3, 4, 3);
            darkTextBox1.Name = "darkTextBox1";
            darkTextBox1.Size = new Size(225, 23);
            darkTextBox1.TabIndex = 16;
            // 
            // darkButton6
            // 
            darkButton6.FlatStyle = FlatStyle.Flat;
            darkButton6.Location = new Point(239, 7);
            darkButton6.Margin = new Padding(4, 3, 4, 3);
            darkButton6.Name = "darkButton6";
            darkButton6.Size = new Size(48, 27);
            darkButton6.TabIndex = 17;
            darkButton6.Text = "Find";
            darkButton6.Click += darkButton6_Click;
            // 
            // darkButton7
            // 
            darkButton7.FlatStyle = FlatStyle.Flat;
            darkButton7.Location = new Point(290, 7);
            darkButton7.Margin = new Padding(4, 3, 4, 3);
            darkButton7.Name = "darkButton7";
            darkButton7.Size = new Size(75, 27);
            darkButton7.TabIndex = 18;
            darkButton7.Text = "Find Next";
            darkButton7.Visible = false;
            darkButton7.Click += darkButton7_Click;
            // 
            // treeViewMS1
            // 
            treeViewMS1.BackColor = Color.FromArgb(60, 63, 65);
            treeViewMS1.ForeColor = SystemColors.Control;
            treeViewMS1.HideSelection = false;
            treeViewMS1.LineColor = Color.FromArgb(240, 240, 240);
            treeViewMS1.Location = new Point(7, 37);
            treeViewMS1.Margin = new Padding(4, 3, 4, 3);
            treeViewMS1.Name = "treeViewMS1";
            treeViewMS1.Size = new Size(488, 427);
            treeViewMS1.TabIndex = 19;
            treeViewMS1.NodeMouseClick += treeView1_NodeMouseClick;
            treeViewMS1.NodeMouseDoubleClick += treeView1_NodeMouseDoubleClick;
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(0, 39);
            tabControl1.Margin = new Padding(4, 3, 4, 3);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(509, 500);
            tabControl1.TabIndex = 20;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.FromArgb(60, 63, 65);
            tabPage1.Controls.Add(darkTextBox1);
            tabPage1.Controls.Add(treeViewMS1);
            tabPage1.Controls.Add(darkButton5);
            tabPage1.Controls.Add(darkButton7);
            tabPage1.Controls.Add(darkButton6);
            tabPage1.ForeColor = SystemColors.Control;
            tabPage1.Location = new Point(4, 24);
            tabPage1.Margin = new Padding(4, 3, 4, 3);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(4, 3, 4, 3);
            tabPage1.Size = new Size(501, 472);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Add from Types";
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.FromArgb(60, 63, 65);
            tabPage2.Controls.Add(darkButton8);
            tabPage2.Controls.Add(richTextBox1);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Margin = new Padding(4, 3, 4, 3);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(4, 3, 4, 3);
            tabPage2.Size = new Size(501, 472);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Add from String";
            // 
            // darkButton8
            // 
            darkButton8.FlatStyle = FlatStyle.Flat;
            darkButton8.Location = new Point(372, 7);
            darkButton8.Margin = new Padding(4, 3, 4, 3);
            darkButton8.Name = "darkButton8";
            darkButton8.Size = new Size(124, 27);
            darkButton8.TabIndex = 16;
            darkButton8.Tag = "HideUsed";
            darkButton8.Text = "Clear Lines";
            darkButton8.Click += darkButton8_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = Color.FromArgb(60, 63, 65);
            richTextBox1.ForeColor = SystemColors.Control;
            richTextBox1.Location = new Point(7, 37);
            richTextBox1.Margin = new Padding(4, 3, 4, 3);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(488, 427);
            richTextBox1.TabIndex = 13;
            richTextBox1.Text = "";
            // 
            // AddItemfromTypes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(783, 539);
            Controls.Add(tabControl1);
            Controls.Add(darkButton3);
            Controls.Add(darkButton4);
            Controls.Add(listBox1);
            Controls.Add(darkButton2);
            Controls.Add(darkButton1);
            Controls.Add(panel1);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "AddItemfromTypes";
            StartPosition = FormStartPosition.CenterParent;
            Text = "AddItemfromTypes";
            Load += AddItemfromTypes_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Button darkButton1;
        private System.Windows.Forms.Button darkButton2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button darkButton3;
        private System.Windows.Forms.Button darkButton4;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button darkButton5;
        private System.Windows.Forms.TextBox darkTextBox1;
        private System.Windows.Forms.Button darkButton6;
        private System.Windows.Forms.Button darkButton7;
        private MultiSelectTreeView treeViewMS1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button darkButton8;
    }
}