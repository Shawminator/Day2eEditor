namespace Day2eEditor
{
    partial class AddEventFile
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
            TitlePanel = new Panel();
            button1 = new Button();
            MinimiseButton = new Button();
            CloseButton = new Button();
            TitleLabel = new Label();
            label1 = new Label();
            SelectProjectFolderbutton = new Button();
            label2 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label3 = new Label();
            button5 = new Button();
            button4 = new Button();
            TitlePanel.SuspendLayout();
            SuspendLayout();
            // 
            // TitlePanel
            // 
            TitlePanel.BackColor = Color.Black;
            TitlePanel.Controls.Add(button1);
            TitlePanel.Controls.Add(MinimiseButton);
            TitlePanel.Controls.Add(CloseButton);
            TitlePanel.Controls.Add(TitleLabel);
            TitlePanel.Controls.Add(label1);
            TitlePanel.Dock = DockStyle.Top;
            TitlePanel.Location = new Point(0, 0);
            TitlePanel.Name = "TitlePanel";
            TitlePanel.Size = new Size(516, 28);
            TitlePanel.TabIndex = 6;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.BackColor = Color.Black;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Segoe UI", 12F);
            button1.ForeColor = Color.DarkRed;
            button1.Location = new Point(875, -2);
            button1.Name = "button1";
            button1.Size = new Size(41, 28);
            button1.TabIndex = 9;
            button1.Text = "X";
            button1.UseVisualStyleBackColor = false;
            // 
            // MinimiseButton
            // 
            MinimiseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            MinimiseButton.BackColor = Color.Black;
            MinimiseButton.FlatStyle = FlatStyle.Popup;
            MinimiseButton.Font = new Font("Segoe UI", 12F);
            MinimiseButton.ForeColor = Color.DarkRed;
            MinimiseButton.Location = new Point(1781, -3);
            MinimiseButton.Name = "MinimiseButton";
            MinimiseButton.Size = new Size(41, 28);
            MinimiseButton.TabIndex = 7;
            MinimiseButton.Text = "_";
            MinimiseButton.UseVisualStyleBackColor = false;
            // 
            // CloseButton
            // 
            CloseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CloseButton.BackColor = Color.Black;
            CloseButton.FlatStyle = FlatStyle.Popup;
            CloseButton.Font = new Font("Segoe UI", 12F);
            CloseButton.ForeColor = Color.DarkRed;
            CloseButton.Location = new Point(472, 0);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(41, 28);
            CloseButton.TabIndex = 6;
            CloseButton.Text = "X";
            CloseButton.UseVisualStyleBackColor = false;
            // 
            // TitleLabel
            // 
            TitleLabel.AutoSize = true;
            TitleLabel.ForeColor = Color.FromArgb(75, 110, 175);
            TitleLabel.Location = new Point(5, 6);
            TitleLabel.Name = "TitleLabel";
            TitleLabel.Size = new Size(159, 15);
            TitleLabel.TabIndex = 0;
            TitleLabel.Text = "Day2eEditor by Shawminator";
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.ForeColor = Color.FromArgb(75, 110, 175);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(516, 28);
            label1.TabIndex = 8;
            label1.Text = "Add New Event File";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // SelectProjectFolderbutton
            // 
            SelectProjectFolderbutton.BackColor = SystemColors.Control;
            SelectProjectFolderbutton.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            SelectProjectFolderbutton.ForeColor = SystemColors.ActiveCaptionText;
            SelectProjectFolderbutton.Location = new Point(481, 94);
            SelectProjectFolderbutton.Name = "SelectProjectFolderbutton";
            SelectProjectFolderbutton.Size = new Size(23, 23);
            SelectProjectFolderbutton.TabIndex = 27;
            SelectProjectFolderbutton.Text = "+";
            SelectProjectFolderbutton.UseVisualStyleBackColor = false;
            SelectProjectFolderbutton.Click += SelectProjectFolderbutton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 69);
            label2.Name = "label2";
            label2.Size = new Size(82, 15);
            label2.TabIndex = 28;
            label2.Text = "New Filename";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(98, 66);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(406, 23);
            textBox1.TabIndex = 29;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(98, 95);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(377, 23);
            textBox2.TabIndex = 31;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 98);
            label3.Name = "label3";
            label3.Size = new Size(48, 15);
            label3.TabIndex = 30;
            label3.Text = "CE Path";
            // 
            // button5
            // 
            button5.BackColor = Color.FromArgb(60, 63, 65);
            button5.DialogResult = DialogResult.OK;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Location = new Point(13, 124);
            button5.Name = "button5";
            button5.Size = new Size(491, 27);
            button5.TabIndex = 35;
            button5.Text = "Import to Economy";
            button5.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            button4.BackColor = Color.FromArgb(60, 63, 65);
            button4.FlatStyle = FlatStyle.Flat;
            button4.Location = new Point(10, 37);
            button4.Name = "button4";
            button4.Size = new Size(117, 23);
            button4.TabIndex = 36;
            button4.Text = "Import from XML";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // AddEventFile
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(516, 164);
            Controls.Add(button4);
            Controls.Add(button5);
            Controls.Add(textBox2);
            Controls.Add(label3);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(SelectProjectFolderbutton);
            Controls.Add(TitlePanel);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            Name = "AddEventFile";
            Text = "AddTypes";
            Load += AddEventFile_Load;
            TitlePanel.ResumeLayout(false);
            TitlePanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel TitlePanel;
        private Button button1;
        private Button MinimiseButton;
        private Button CloseButton;
        private Label TitleLabel;
        private Label label1;
        private Button SelectProjectFolderbutton;
        private Label label2;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label3;
        private Button button5;
        private Button button4;
    }
}