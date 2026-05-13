namespace ExpansionPlugin
{
    partial class NpcNodeControl
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
            label = new Label();
            SuspendLayout();
            // 
            // label
            // 
            label.Dock = DockStyle.Fill;
            label.Location = new Point(6, 6);
            label.Name = "label";
            label.Size = new Size(128, 48);
            label.TabIndex = 0;
            label.Text = "label1";
            label.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // NpcNodeControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(70, 70, 70);
            Controls.Add(label);
            ForeColor = SystemColors.Control;
            Name = "NpcNodeControl";
            Padding = new Padding(6);
            Size = new Size(140, 60);
            Paint += NpcNodeControl_Paint;
            ResumeLayout(false);
        }

        #endregion

        private Label label;
    }
}
