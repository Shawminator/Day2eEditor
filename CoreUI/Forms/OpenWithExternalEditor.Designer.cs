namespace Day2eEditor
{
    partial class OpenWithExternalEditor
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
            button2 = new Button();
            button1 = new Button();
            MinimiseButton = new Button();
            CloseButton = new Button();
            label1 = new Label();
            comboBoxEditors = new ComboBox();
            button3 = new Button();
            TitlePanel.SuspendLayout();
            SuspendLayout();
            // 
            // TitlePanel
            // 
            TitlePanel.BackColor = Color.Black;
            TitlePanel.Controls.Add(button2);
            TitlePanel.Controls.Add(button1);
            TitlePanel.Controls.Add(MinimiseButton);
            TitlePanel.Controls.Add(CloseButton);
            TitlePanel.Controls.Add(label1);
            TitlePanel.Dock = DockStyle.Top;
            TitlePanel.Location = new Point(0, 0);
            TitlePanel.Name = "TitlePanel";
            TitlePanel.Size = new Size(440, 28);
            TitlePanel.TabIndex = 7;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button2.BackColor = Color.Black;
            button2.FlatStyle = FlatStyle.Popup;
            button2.Font = new Font("Segoe UI", 12F);
            button2.ForeColor = Color.DarkRed;
            button2.Location = new Point(399, 0);
            button2.Name = "button2";
            button2.Size = new Size(41, 28);
            button2.TabIndex = 8;
            button2.Text = "X";
            button2.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.BackColor = Color.Black;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Segoe UI", 12F);
            button1.ForeColor = Color.DarkRed;
            button1.Location = new Point(1729, -2);
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
            MinimiseButton.Location = new Point(2635, -3);
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
            CloseButton.Location = new Point(1326, 0);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(41, 28);
            CloseButton.TabIndex = 6;
            CloseButton.Text = "X";
            CloseButton.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.ForeColor = Color.FromArgb(75, 110, 175);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(437, 28);
            label1.TabIndex = 8;
            label1.Text = "OpenWith";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // comboBoxEditors
            // 
            comboBoxEditors.BackColor = Color.FromArgb(60, 63, 65);
            comboBoxEditors.ForeColor = SystemColors.Control;
            comboBoxEditors.FormattingEnabled = true;
            comboBoxEditors.Location = new Point(12, 34);
            comboBoxEditors.Name = "comboBoxEditors";
            comboBoxEditors.Size = new Size(413, 23);
            comboBoxEditors.TabIndex = 8;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(60, 63, 65);
            button3.DialogResult = DialogResult.OK;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Location = new Point(12, 63);
            button3.Name = "button3";
            button3.Size = new Size(413, 23);
            button3.TabIndex = 35;
            button3.Text = "Open With";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // OpenWithExternalEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(440, 105);
            Controls.Add(button3);
            Controls.Add(comboBoxEditors);
            Controls.Add(TitlePanel);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            Name = "OpenWithExternalEditor";
            Text = "OpenWithExternalEditor";
            TitlePanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel TitlePanel;
        private Button button1;
        private Button MinimiseButton;
        private Button CloseButton;
        private Label label1;
        private Button button2;
        private ComboBox comboBoxEditors;
        private Button button3;
    }
}