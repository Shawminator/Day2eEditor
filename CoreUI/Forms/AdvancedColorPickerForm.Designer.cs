namespace CoreUI.Forms
{
    partial class AdvancedColorPickerForm
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
            MinimiseButton = new Button();
            CloseButton = new Button();
            TitleLabel = new Label();
            label1 = new Label();
            btnPickColor = new Button();
            trackBarAlpha = new TrackBar();
            lblAlphaValue = new Label();
            panelPreview = new Panel();
            lblColorCode = new Label();
            btnOK = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            lblColorCodeints = new Label();
            TitlePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarAlpha).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // TitlePanel
            // 
            TitlePanel.BackColor = Color.Black;
            TitlePanel.Controls.Add(MinimiseButton);
            TitlePanel.Controls.Add(CloseButton);
            TitlePanel.Controls.Add(TitleLabel);
            TitlePanel.Controls.Add(label1);
            TitlePanel.Dock = DockStyle.Top;
            TitlePanel.Location = new Point(0, 0);
            TitlePanel.Name = "TitlePanel";
            TitlePanel.Size = new Size(328, 28);
            TitlePanel.TabIndex = 7;
            // 
            // MinimiseButton
            // 
            MinimiseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            MinimiseButton.BackColor = Color.Black;
            MinimiseButton.FlatStyle = FlatStyle.Popup;
            MinimiseButton.Font = new Font("Segoe UI", 12F);
            MinimiseButton.ForeColor = Color.DarkRed;
            MinimiseButton.Location = new Point(1909, -3);
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
            CloseButton.Location = new Point(287, -1);
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
            label1.Size = new Size(328, 28);
            label1.TabIndex = 8;
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnPickColor
            // 
            btnPickColor.Location = new Point(18, 15);
            btnPickColor.Name = "btnPickColor";
            btnPickColor.Size = new Size(283, 23);
            btnPickColor.TabIndex = 8;
            btnPickColor.Text = "Pick Base Color";
            btnPickColor.Click += btnPickColor_Click;
            // 
            // trackBarAlpha
            // 
            trackBarAlpha.Location = new Point(18, 53);
            trackBarAlpha.Maximum = 255;
            trackBarAlpha.Name = "trackBarAlpha";
            trackBarAlpha.Size = new Size(200, 45);
            trackBarAlpha.TabIndex = 9;
            trackBarAlpha.Value = 255;
            trackBarAlpha.Scroll += trackBarAlpha_Scroll;
            // 
            // lblAlphaValue
            // 
            lblAlphaValue.AutoSize = true;
            lblAlphaValue.Location = new Point(228, 53);
            lblAlphaValue.Name = "lblAlphaValue";
            lblAlphaValue.Size = new Size(62, 15);
            lblAlphaValue.TabIndex = 10;
            lblAlphaValue.Text = "Alpha: 255";
            // 
            // panelPreview
            // 
            panelPreview.BackColor = SystemColors.Control;
            panelPreview.BorderStyle = BorderStyle.FixedSingle;
            panelPreview.Location = new Point(18, 101);
            panelPreview.Name = "panelPreview";
            panelPreview.Size = new Size(100, 100);
            panelPreview.TabIndex = 11;
            // 
            // lblColorCode
            // 
            lblColorCode.AutoSize = true;
            lblColorCode.Location = new Point(124, 101);
            lblColorCode.Name = "lblColorCode";
            lblColorCode.Size = new Size(103, 15);
            lblColorCode.TabIndex = 12;
            lblColorCode.Text = "ARGB: 0xFFFF0000";
            // 
            // btnOK
            // 
            btnOK.Location = new Point(18, 213);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(283, 23);
            btnOK.TabIndex = 13;
            btnOK.Text = "OK";
            btnOK.Click += btnOK_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Control;
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 28);
            panel1.Name = "panel1";
            panel1.Size = new Size(328, 257);
            panel1.TabIndex = 14;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.BackColor = Color.FromArgb(60, 63, 65);
            panel2.Controls.Add(lblColorCodeints);
            panel2.Controls.Add(btnPickColor);
            panel2.Controls.Add(btnOK);
            panel2.Controls.Add(panelPreview);
            panel2.Controls.Add(lblAlphaValue);
            panel2.Controls.Add(lblColorCode);
            panel2.Controls.Add(trackBarAlpha);
            panel2.Location = new Point(3, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(322, 254);
            panel2.TabIndex = 15;
            // 
            // lblColorCodeints
            // 
            lblColorCodeints.AutoSize = true;
            lblColorCodeints.Location = new Point(124, 127);
            lblColorCodeints.Name = "lblColorCodeints";
            lblColorCodeints.Size = new Size(103, 15);
            lblColorCodeints.TabIndex = 14;
            lblColorCodeints.Text = "ARGB: 0xFFFF0000";
            // 
            // AdvancedColorPickerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(328, 285);
            Controls.Add(panel1);
            Controls.Add(TitlePanel);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            Name = "AdvancedColorPickerForm";
            Text = "AdvancedColorPickerForm";
            TitlePanel.ResumeLayout(false);
            TitlePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarAlpha).EndInit();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel TitlePanel;
        private Button MinimiseButton;
        private Button CloseButton;
        private Label TitleLabel;
        private Label label1;
        private Button btnPickColor;
        private TrackBar trackBarAlpha;
        private Label lblAlphaValue;
        private Panel panelPreview;
        private Label lblColorCode;
        private Button btnOK;
        private Panel panel1;
        private Panel panel2;
        private Label lblColorCodeints;
    }
}