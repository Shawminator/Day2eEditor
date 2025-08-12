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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EconomyForm));
            _mapControl = new MapViewerControl();
            splitContainer1 = new SplitContainer();
            EconomyTV = new MultiSelectTreeView();
            EditPropertyCMS = new ContextMenuStrip(components);
            editPropertyToolStripMenuItem = new ToolStripMenuItem();
            panel1 = new Panel();
            SaveButton = new Button();
            contextMenuStrip1 = new ContextMenuStrip(components);
            toolStripMenuItem1 = new ToolStripMenuItem();
            contextMenuStrip2 = new ContextMenuStrip(components);
            toolStripMenuItem2 = new ToolStripMenuItem();
            setToDefaultToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            EditPropertyCMS.SuspendLayout();
            panel1.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            contextMenuStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // _mapControl
            // 
            _mapControl.Dock = DockStyle.Fill;
            _mapControl.Location = new Point(0, 0);
            _mapControl.Name = "_mapControl";
            _mapControl.Size = new Size(756, 593);
            _mapControl.TabIndex = 1;
            _mapControl.Text = "mapViewerControl1";
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(12, 37);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(EconomyTV);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(_mapControl);
            splitContainer1.Size = new Size(1146, 593);
            splitContainer1.SplitterDistance = 380;
            splitContainer1.SplitterWidth = 10;
            splitContainer1.TabIndex = 3;
            // 
            // EconomyTV
            // 
            EconomyTV.BackColor = Color.FromArgb(60, 63, 65);
            EconomyTV.Dock = DockStyle.Fill;
            EconomyTV.ForeColor = SystemColors.Control;
            EconomyTV.LineColor = Color.FromArgb(240, 240, 240);
            EconomyTV.Location = new Point(0, 0);
            EconomyTV.Name = "EconomyTV";
            EconomyTV.Size = new Size(380, 593);
            EconomyTV.TabIndex = 0;
            EconomyTV.AfterSelect += EconomyTV_AfterSelect;
            EconomyTV.NodeMouseClick += EconomyTV_NodeMouseClick;
            // 
            // EditPropertyCMS
            // 
            EditPropertyCMS.BackColor = Color.FromArgb(60, 63, 65);
            EditPropertyCMS.Items.AddRange(new ToolStripItem[] { editPropertyToolStripMenuItem, setToDefaultToolStripMenuItem });
            EditPropertyCMS.Name = "contextMenuStrip1";
            EditPropertyCMS.ShowImageMargin = false;
            EditPropertyCMS.Size = new Size(156, 70);
            // 
            // editPropertyToolStripMenuItem
            // 
            editPropertyToolStripMenuItem.ForeColor = SystemColors.Control;
            editPropertyToolStripMenuItem.Name = "editPropertyToolStripMenuItem";
            editPropertyToolStripMenuItem.Size = new Size(155, 22);
            editPropertyToolStripMenuItem.Text = "Edit Property";
            editPropertyToolStripMenuItem.Click += editPropertyToolStripMenuItem_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(60, 63, 65);
            panel1.Controls.Add(SaveButton);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1170, 31);
            panel1.TabIndex = 4;
            // 
            // SaveButton
            // 
            SaveButton.BackgroundImage = (Image)resources.GetObject("SaveButton.BackgroundImage");
            SaveButton.BackgroundImageLayout = ImageLayout.Stretch;
            SaveButton.FlatStyle = FlatStyle.Flat;
            SaveButton.Location = new Point(12, 3);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(40, 25);
            SaveButton.TabIndex = 0;
            SaveButton.TextImageRelation = TextImageRelation.ImageAboveText;
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.BackColor = Color.FromArgb(60, 63, 65);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1 });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.ShowImageMargin = false;
            contextMenuStrip1.Size = new Size(118, 26);
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.ForeColor = SystemColors.Control;
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(117, 22);
            toolStripMenuItem1.Text = "Edit Property";
            // 
            // contextMenuStrip2
            // 
            contextMenuStrip2.BackColor = Color.FromArgb(60, 63, 65);
            contextMenuStrip2.Items.AddRange(new ToolStripItem[] { toolStripMenuItem2 });
            contextMenuStrip2.Name = "contextMenuStrip1";
            contextMenuStrip2.ShowImageMargin = false;
            contextMenuStrip2.Size = new Size(118, 26);
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.ForeColor = SystemColors.Control;
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(117, 22);
            toolStripMenuItem2.Text = "Edit Property";
            // 
            // setToDefaultToolStripMenuItem
            // 
            setToDefaultToolStripMenuItem.ForeColor = SystemColors.Control;
            setToDefaultToolStripMenuItem.Name = "setToDefaultToolStripMenuItem";
            setToDefaultToolStripMenuItem.Size = new Size(155, 22);
            setToDefaultToolStripMenuItem.Text = "Set To Default";
            setToDefaultToolStripMenuItem.Click += setToDefaultToolStripMenuItem_Click;
            // 
            // EconomyForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(1170, 642);
            Controls.Add(panel1);
            Controls.Add(splitContainer1);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            Name = "EconomyForm";
            Text = "Form1";
            FormClosing += EconomyForm_FormClosing;
            FormClosed += EconomyForm_FormClosed;
            Load += EconomyForm_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            EditPropertyCMS.ResumeLayout(false);
            panel1.ResumeLayout(false);
            contextMenuStrip1.ResumeLayout(false);
            contextMenuStrip2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private MapViewerControl _mapControl;
        private SplitContainer splitContainer1;
        private ContextMenuStrip EditPropertyCMS;
        private ToolStripMenuItem editPropertyToolStripMenuItem;
        private Panel panel1;
        private Button SaveButton;
        private MultiSelectTreeView EconomyTV;
        private ToolStripMenuItem setToDefaultToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem toolStripMenuItem2;
    }
}
