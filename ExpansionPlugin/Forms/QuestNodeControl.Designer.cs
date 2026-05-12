namespace ExpansionPlugin.Forms
{
    partial class QuestNodeControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            flags = new Label();
            title = new Label();
            SuspendLayout();
            // 
            // flags
            // 
            flags.Dock = DockStyle.Top;
            flags.ForeColor = SystemColors.Control;
            flags.Location = new Point(8, 8);
            flags.Name = "flags";
            flags.Size = new Size(284, 18);
            flags.TabIndex = 0;
            flags.Text = "flags";
            // 
            // title
            // 
            title.Dock = DockStyle.Top;
            title.ForeColor = SystemColors.Control;
            title.Location = new Point(8, 26);
            title.Name = "title";
            title.Size = new Size(284, 106);
            title.TabIndex = 1;
            title.Text = "title";
            // 
            // QuestNodeControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(48, 48, 48);
            Controls.Add(title);
            Controls.Add(flags);
            Name = "QuestNodeControl";
            Padding = new Padding(8);
            Size = new Size(300, 140);
            Load += QuestNodeControl_Load;
            ResumeLayout(false);
        }

        #endregion

        private Label flags;
        private Label title;
    }
}
