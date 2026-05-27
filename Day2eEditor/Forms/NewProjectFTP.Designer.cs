
namespace Day2eEditor
{
    partial class NewProjectFTP
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewProjectFTP));
            panel1 = new Panel();
            CloseButton = new Button();
            TitleLabel = new Label();
            listView1 = new ListView();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            columnHeader1 = new ColumnHeader();
            imageList1 = new ImageList(components);
            darkLabel2 = new Label();
            darkTextBox2 = new TextBox();
            darkButton2 = new Button();
            darkButton3 = new Button();
            panel1.SuspendLayout();
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
            panel1.Size = new Size(933, 32);
            panel1.TabIndex = 99;
            // 
            // CloseButton
            // 
            CloseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CloseButton.BackColor = Color.Black;
            CloseButton.FlatStyle = FlatStyle.Popup;
            CloseButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CloseButton.ForeColor = Color.DarkRed;
            CloseButton.Location = new Point(882, -1);
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
            TitleLabel.Size = new Size(126, 15);
            TitleLabel.TabIndex = 6;
            TitleLabel.Text = "New Project from FTP";
            // 
            // listView1
            // 
            listView1.BackColor = Color.FromArgb(60, 63, 65);
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader5, columnHeader6, columnHeader1 });
            listView1.ForeColor = SystemColors.Control;
            listView1.FullRowSelect = true;
            listView1.Location = new Point(0, 28);
            listView1.Margin = new Padding(4, 3, 4, 3);
            listView1.Name = "listView1";
            listView1.Size = new Size(450, 489);
            listView1.SmallImageList = imageList1;
            listView1.TabIndex = 100;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.MouseDoubleClick += listView1_MouseDoubleClick;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Name";
            columnHeader5.Width = 89;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Size";
            columnHeader6.Width = 98;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Last Modified";
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "folder.png");
            imageList1.Images.SetKeyName(1, "file.png");
            // 
            // darkLabel2
            // 
            darkLabel2.AutoSize = true;
            darkLabel2.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel2.Location = new Point(458, 70);
            darkLabel2.Margin = new Padding(4, 0, 4, 0);
            darkLabel2.Name = "darkLabel2";
            darkLabel2.Size = new Size(68, 15);
            darkLabel2.TabIndex = 103;
            darkLabel2.Text = "Root Folder";
            // 
            // darkTextBox2
            // 
            darkTextBox2.BackColor = Color.FromArgb(69, 73, 74);
            darkTextBox2.BorderStyle = BorderStyle.FixedSingle;
            darkTextBox2.ForeColor = Color.FromArgb(220, 220, 220);
            darkTextBox2.Location = new Point(554, 66);
            darkTextBox2.Margin = new Padding(4, 3, 4, 3);
            darkTextBox2.Name = "darkTextBox2";
            darkTextBox2.ReadOnly = true;
            darkTextBox2.Size = new Size(336, 23);
            darkTextBox2.TabIndex = 105;
            darkTextBox2.TextChanged += darkTextBox2_TextChanged;
            // 
            // darkButton2
            // 
            darkButton2.FlatStyle = FlatStyle.Flat;
            darkButton2.Location = new Point(897, 66);
            darkButton2.Margin = new Padding(4, 3, 4, 3);
            darkButton2.Name = "darkButton2";
            darkButton2.Size = new Size(26, 23);
            darkButton2.TabIndex = 107;
            darkButton2.Text = "+";
            darkButton2.Click += darkButton2_Click;
            // 
            // darkButton3
            // 
            darkButton3.DialogResult = DialogResult.OK;
            darkButton3.FlatStyle = FlatStyle.Flat;
            darkButton3.Location = new Point(796, 479);
            darkButton3.Margin = new Padding(4, 3, 4, 3);
            darkButton3.Name = "darkButton3";
            darkButton3.Size = new Size(132, 27);
            darkButton3.TabIndex = 108;
            darkButton3.Text = "Continue";
            // 
            // NewProjectFTP
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(933, 519);
            Controls.Add(darkButton3);
            Controls.Add(darkButton2);
            Controls.Add(darkTextBox2);
            Controls.Add(darkLabel2);
            Controls.Add(listView1);
            Controls.Add(panel1);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "NewProjectFTP";
            StartPosition = FormStartPosition.CenterParent;
            Text = "NewProjectFTP";
            Load += NewProjectFTP_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label darkLabel2;
        private System.Windows.Forms.TextBox darkTextBox2;
        private System.Windows.Forms.Button darkButton2;
        private System.Windows.Forms.Button darkButton3;
        private System.Windows.Forms.ImageList imageList1;
    }
}