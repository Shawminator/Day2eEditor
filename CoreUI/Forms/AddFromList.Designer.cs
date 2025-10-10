
namespace Day2eEditor
{
    partial class AddFromList
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
            panel1.Size = new Size(498, 32);
            panel1.TabIndex = 8;
            // 
            // CloseButton
            // 
            CloseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CloseButton.BackColor = Color.Black;
            CloseButton.FlatStyle = FlatStyle.Popup;
            CloseButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CloseButton.ForeColor = Color.DarkRed;
            CloseButton.Location = new Point(447, -1);
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
            TitleLabel.Text = "Add Items from String";
            // 
            // darkButton1
            // 
            darkButton1.DialogResult = DialogResult.OK;
            darkButton1.FlatStyle = FlatStyle.Flat;
            darkButton1.Location = new Point(246, 471);
            darkButton1.Margin = new Padding(4, 3, 4, 3);
            darkButton1.Name = "darkButton1";
            darkButton1.Size = new Size(238, 27);
            darkButton1.TabIndex = 10;
            darkButton1.Text = "OK";
            // 
            // darkButton2
            // 
            darkButton2.DialogResult = DialogResult.Cancel;
            darkButton2.FlatStyle = FlatStyle.Flat;
            darkButton2.Location = new Point(14, 471);
            darkButton2.Margin = new Padding(4, 3, 4, 3);
            darkButton2.Name = "darkButton2";
            darkButton2.Size = new Size(225, 27);
            darkButton2.TabIndex = 11;
            darkButton2.Text = "Cancel";
            // 
            // listBox1
            // 
            listBox1.BackColor = Color.FromArgb(60, 63, 65);
            listBox1.DrawMode = DrawMode.OwnerDrawFixed;
            listBox1.ForeColor = SystemColors.Control;
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(13, 45);
            listBox1.Margin = new Padding(4, 3, 4, 3);
            listBox1.Name = "listBox1";
            listBox1.SelectionMode = SelectionMode.MultiExtended;
            listBox1.Size = new Size(471, 420);
            listBox1.TabIndex = 13;
            listBox1.DrawItem += listBox_DrawItem;
            // 
            // AddFromList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(498, 511);
            Controls.Add(listBox1);
            Controls.Add(darkButton2);
            Controls.Add(darkButton1);
            Controls.Add(panel1);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "AddFromList";
            StartPosition = FormStartPosition.CenterParent;
            Text = "AddItemfromTypes";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Button darkButton1;
        private System.Windows.Forms.Button darkButton2;
        private System.Windows.Forms.Button CloseButton;
        private ListBox listBox1;
    }
}