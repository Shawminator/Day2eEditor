namespace DayZFileManagerPlugin
{
    partial class DayZFileManagerForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pendingListView = new ListView();
            panel1 = new Panel();
            CreateProjectbutton = new Button();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pendingListView
            // 
            pendingListView.BackColor = Color.FromArgb(60, 63, 65);
            pendingListView.Dock = DockStyle.Fill;
            pendingListView.ForeColor = SystemColors.Control;
            pendingListView.Location = new Point(0, 31);
            pendingListView.Name = "pendingListView";
            pendingListView.Size = new Size(1038, 602);
            pendingListView.TabIndex = 2;
            pendingListView.UseCompatibleStateImageBehavior = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(60, 63, 65);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(CreateProjectbutton);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1038, 31);
            panel1.TabIndex = 6;
            // 
            // CreateProjectbutton
            // 
            CreateProjectbutton.BackColor = Color.FromArgb(60, 63, 65);
            CreateProjectbutton.FlatStyle = FlatStyle.Flat;
            CreateProjectbutton.Location = new Point(3, 5);
            CreateProjectbutton.Name = "CreateProjectbutton";
            CreateProjectbutton.Size = new Size(73, 23);
            CreateProjectbutton.TabIndex = 16;
            CreateProjectbutton.Text = "Upload All";
            CreateProjectbutton.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(60, 63, 65);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(82, 5);
            button1.Name = "button1";
            button1.Size = new Size(112, 23);
            button1.TabIndex = 17;
            button1.Text = "Upload Checked";
            button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(60, 63, 65);
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(200, 5);
            button2.Name = "button2";
            button2.Size = new Size(73, 23);
            button2.TabIndex = 18;
            button2.Text = "Refresh";
            button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(60, 63, 65);
            button3.FlatStyle = FlatStyle.Flat;
            button3.Location = new Point(279, 5);
            button3.Name = "button3";
            button3.Size = new Size(122, 23);
            button3.TabIndex = 19;
            button3.Text = "Test Connection";
            button3.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            button4.BackColor = Color.FromArgb(60, 63, 65);
            button4.FlatStyle = FlatStyle.Flat;
            button4.Location = new Point(407, 5);
            button4.Name = "button4";
            button4.Size = new Size(122, 23);
            button4.TabIndex = 20;
            button4.Text = "Download All Files";
            button4.UseVisualStyleBackColor = false;
            // 
            // DayZFileManagerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(1038, 633);
            Controls.Add(pendingListView);
            Controls.Add(panel1);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            Name = "DayZFileManagerForm";
            Text = "Form1";
            Load += DayZFileManagerForm_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListView pendingListView;
        private Panel panel1;
        private Button button2;
        private Button button1;
        private Button CreateProjectbutton;
        private Button button4;
        private Button button3;
    }
}
