
namespace CaparePlugin
{
    partial class CapareDefinedItemAttachmentPreview
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
            OKButton = new Button();
            CancelBtn = new Button();
            treeView1 = new TreeView();
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
            TitleLabel.Size = new Size(140, 15);
            TitleLabel.TabIndex = 6;
            TitleLabel.Text = "Universal Attachments";
            //
            // OKButton
            //
            OKButton.FlatStyle = FlatStyle.Flat;
            OKButton.Location = new Point(246, 471);
            OKButton.Margin = new Padding(4, 3, 4, 3);
            OKButton.Name = "OKButton";
            OKButton.Size = new Size(238, 27);
            OKButton.TabIndex = 10;
            OKButton.Text = "OK";
            OKButton.Click += OKButton_Click;
            //
            // CancelBtn
            //
            CancelBtn.DialogResult = DialogResult.Cancel;
            CancelBtn.FlatStyle = FlatStyle.Flat;
            CancelBtn.Location = new Point(14, 471);
            CancelBtn.Margin = new Padding(4, 3, 4, 3);
            CancelBtn.Name = "CancelBtn";
            CancelBtn.Size = new Size(225, 27);
            CancelBtn.TabIndex = 11;
            CancelBtn.Text = "Cancel";
            //
            // treeView1
            //
            treeView1.BackColor = Color.FromArgb(60, 63, 65);
            treeView1.CheckBoxes = true;
            treeView1.ForeColor = SystemColors.Control;
            treeView1.LineColor = Color.FromArgb(240, 240, 240);
            treeView1.Location = new Point(14, 38);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(470, 427);
            treeView1.TabIndex = 14;
            treeView1.AfterCheck += treeView1_AfterCheck;
            //
            // CapareDefinedItemAttachmentPreview
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(498, 511);
            Controls.Add(treeView1);
            Controls.Add(CancelBtn);
            Controls.Add(OKButton);
            Controls.Add(panel1);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "CapareDefinedItemAttachmentPreview";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Universal Attachments";
            Load += CapareDefinedItemAttachmentPreview_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button CloseButton;
        private TreeView treeView1;
    }
}
