
namespace ExpansionPlugin
{
    partial class SelectCategoryFolderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TreeView treeViewFolders;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;


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
            treeViewFolders = new TreeView();
            buttonOK = new Button();
            buttonCancel = new Button();
            panel1 = new Panel();
            CloseButton = new Button();
            TitleLabel = new Label();
            button1 = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // treeViewFolders
            // 
            treeViewFolders.BackColor = Color.FromArgb(60, 63, 65);
            treeViewFolders.ForeColor = SystemColors.Control;
            treeViewFolders.Location = new Point(10, 37);
            treeViewFolders.Margin = new Padding(3, 2, 3, 2);
            treeViewFolders.Name = "treeViewFolders";
            treeViewFolders.Size = new Size(316, 273);
            treeViewFolders.TabIndex = 0;
            // 
            // buttonOK
            // 
            buttonOK.FlatStyle = FlatStyle.Flat;
            buttonOK.ForeColor = SystemColors.Control;
            buttonOK.Location = new Point(12, 315);
            buttonOK.Margin = new Padding(3, 2, 3, 2);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(142, 27);
            buttonOK.TabIndex = 1;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.FlatStyle = FlatStyle.Flat;
            buttonCancel.ForeColor = SystemColors.Control;
            buttonCancel.Location = new Point(177, 315);
            buttonCancel.Margin = new Padding(3, 2, 3, 2);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(149, 27);
            buttonCancel.TabIndex = 2;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Black;
            panel1.Controls.Add(button1);
            panel1.Controls.Add(CloseButton);
            panel1.Controls.Add(TitleLabel);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(336, 32);
            panel1.TabIndex = 9;
            // 
            // CloseButton
            // 
            CloseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CloseButton.BackColor = Color.Black;
            CloseButton.FlatStyle = FlatStyle.Popup;
            CloseButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CloseButton.ForeColor = Color.DarkRed;
            CloseButton.Location = new Point(583, -1);
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
            TitleLabel.Size = new Size(86, 15);
            TitleLabel.TabIndex = 6;
            TitleLabel.Text = "Selecte Folder";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.BackColor = Color.Black;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.DarkRed;
            button1.Location = new Point(288, 0);
            button1.Margin = new Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new Size(48, 32);
            button1.TabIndex = 10;
            button1.Text = "X";
            button1.UseVisualStyleBackColor = false;
            // 
            // SelectCategoryFolderForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(336, 346);
            Controls.Add(panel1);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOK);
            Controls.Add(treeViewFolders);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            MinimizeBox = false;
            Name = "SelectCategoryFolderForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Select Category Folder";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);


        }

        #endregion

        private Panel panel1;
        private Button CloseButton;
        private Label TitleLabel;
        private Button button1;
    }
}