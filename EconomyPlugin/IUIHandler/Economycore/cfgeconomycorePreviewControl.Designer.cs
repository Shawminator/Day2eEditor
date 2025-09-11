namespace EconomyPlugin
{
    partial class cfgeconomycorePreviewControl
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
            xmlPreview = new RichTextBox();
            SuspendLayout();
            // 
            // xmlPreview
            // 
            xmlPreview.Dock = DockStyle.Fill;
            xmlPreview.Location = new Point(0, 0);
            xmlPreview.Name = "xmlPreview";
            xmlPreview.ReadOnly = true;
            xmlPreview.Size = new Size(647, 287);
            xmlPreview.TabIndex = 0;
            xmlPreview.Text = "";
            xmlPreview.TextChanged += xmlPreview_TextChanged;
            // 
            // cfgeconomycorePreviewControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(xmlPreview);
            ForeColor = SystemColors.Control;
            Name = "cfgeconomycorePreviewControl";
            Size = new Size(647, 287);
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox xmlPreview;
    }
}
