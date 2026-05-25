
namespace ExpansionPlugin
{
    partial class AddObjectives
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
            treeViewMS1 = new Day2eEditor.MultiSelectTreeView();
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
            panel1.Size = new Size(462, 32);
            panel1.TabIndex = 9;
            // 
            // CloseButton
            // 
            CloseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CloseButton.BackColor = Color.Black;
            CloseButton.FlatStyle = FlatStyle.Popup;
            CloseButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CloseButton.ForeColor = Color.DarkRed;
            CloseButton.Location = new Point(411, -1);
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
            TitleLabel.Size = new Size(81, 15);
            TitleLabel.TabIndex = 6;
            TitleLabel.Text = "Add Objective";
            // 
            // darkButton1
            // 
            darkButton1.DialogResult = DialogResult.OK;
            darkButton1.FlatStyle = FlatStyle.Flat;
            darkButton1.Location = new Point(9, 489);
            darkButton1.Margin = new Padding(4, 3, 4, 3);
            darkButton1.Name = "darkButton1";
            darkButton1.Size = new Size(439, 27);
            darkButton1.TabIndex = 96;
            darkButton1.Text = "Select";
            darkButton1.Click += darkButton1_Click;
            // 
            // treeViewMS1
            // 
            treeViewMS1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            treeViewMS1.BackColor = Color.FromArgb(60, 63, 65);
            treeViewMS1.CollapseSelectedNodeOnly = true;
            treeViewMS1.ForeColor = SystemColors.Control;
            treeViewMS1.HideSelection = false;
            treeViewMS1.LineColor = Color.FromArgb(240, 240, 240);
            treeViewMS1.Location = new Point(11, 38);
            treeViewMS1.Margin = new Padding(4, 3, 4, 3);
            treeViewMS1.Name = "treeViewMS1";
            treeViewMS1.Size = new Size(438, 445);
            treeViewMS1.TabIndex = 207;
            // 
            // AddObjectives
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(462, 519);
            Controls.Add(treeViewMS1);
            Controls.Add(darkButton1);
            Controls.Add(panel1);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "AddObjectives";
            StartPosition = FormStartPosition.CenterParent;
            Text = "AddfromPredefinedWeapons";
            Load += AddObjectives_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Button darkButton1;
        private Day2eEditor.MultiSelectTreeView treeViewMS1;
    }
}