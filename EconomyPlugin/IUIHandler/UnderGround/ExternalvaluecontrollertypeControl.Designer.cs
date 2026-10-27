namespace EconomyPlugin
{
    partial class ExternalvaluecontrollertypeControl
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
            CFGUBreadCrumbGB = new GroupBox();
            CommentTB = new TextBox();
            label3 = new Label();
            CFGUBreadCrumbGB.SuspendLayout();
            SuspendLayout();
            // 
            // CFGUBreadCrumbGB
            // 
            CFGUBreadCrumbGB.Controls.Add(CommentTB);
            CFGUBreadCrumbGB.Controls.Add(label3);
            CFGUBreadCrumbGB.ForeColor = SystemColors.Control;
            CFGUBreadCrumbGB.Location = new Point(0, 0);
            CFGUBreadCrumbGB.Margin = new Padding(4, 3, 4, 3);
            CFGUBreadCrumbGB.Name = "CFGUBreadCrumbGB";
            CFGUBreadCrumbGB.Padding = new Padding(4, 3, 4, 3);
            CFGUBreadCrumbGB.Size = new Size(499, 55);
            CFGUBreadCrumbGB.TabIndex = 225;
            CFGUBreadCrumbGB.TabStop = false;
            CFGUBreadCrumbGB.Text = "External Value Controller Type";
            // 
            // CommentTB
            // 
            CommentTB.BackColor = Color.FromArgb(60, 63, 65);
            CommentTB.ForeColor = SystemColors.Control;
            CommentTB.Location = new Point(107, 22);
            CommentTB.Name = "CommentTB";
            CommentTB.Size = new Size(380, 23);
            CommentTB.TabIndex = 223;
            CommentTB.TextChanged += CommentTB_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.FromArgb(220, 220, 220);
            label3.Location = new Point(15, 25);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(32, 15);
            label3.TabIndex = 215;
            label3.Text = "Type";
            // 
            // ExternalvaluecontrollertypeControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(CFGUBreadCrumbGB);
            ForeColor = SystemColors.Control;
            Name = "ExternalvaluecontrollertypeControl";
            Size = new Size(499, 55);
            CFGUBreadCrumbGB.ResumeLayout(false);
            CFGUBreadCrumbGB.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox CFGUBreadCrumbGB;
        private TextBox CommentTB;
        private Label label3;
    }
}
