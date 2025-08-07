using Day2eEditor;

namespace EconomyPlugin
{
    partial class EconomyForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            _mapControl = new MapViewerControl();
            EconomyTV = new TreeView();
            splitContainer1 = new SplitContainer();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // _mapControl
            // 
            _mapControl.Dock = DockStyle.Fill;
            _mapControl.Location = new Point(0, 0);
            _mapControl.Name = "_mapControl";
            _mapControl.Size = new Size(756, 618);
            _mapControl.TabIndex = 1;
            _mapControl.Text = "mapViewerControl1";
            // 
            // EconomyTV
            // 
            EconomyTV.BackColor = Color.FromArgb(60, 63, 65);
            EconomyTV.Dock = DockStyle.Fill;
            EconomyTV.ForeColor = SystemColors.ControlDark;
            EconomyTV.Location = new Point(0, 0);
            EconomyTV.Name = "EconomyTV";
            EconomyTV.Size = new Size(380, 618);
            EconomyTV.TabIndex = 2;
            EconomyTV.AfterSelect += EconomyTV_AfterSelect;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(12, 12);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(EconomyTV);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(_mapControl);
            splitContainer1.Size = new Size(1146, 618);
            splitContainer1.SplitterDistance = 380;
            splitContainer1.SplitterWidth = 10;
            splitContainer1.TabIndex = 3;
            // 
            // EconomyForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(1170, 642);
            Controls.Add(splitContainer1);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            Name = "EconomyForm";
            Text = "Form1";
            FormClosed += EconomyForm_FormClosed;
            Load += EconomyForm_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private MapViewerControl _mapControl;
        private TreeView EconomyTV;
        private SplitContainer splitContainer1;
    }
}
