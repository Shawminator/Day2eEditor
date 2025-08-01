namespace ProjectsPlugin
{
    partial class ProjectForm
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
            PluginLB = new ListView();
            PluginHeader = new ColumnHeader();
            InstalledHeader = new ColumnHeader();
            PluginCM = new ContextMenuStrip(components);
            downloadAndInstallToolStripMenuItem = new ToolStripMenuItem();
            removeToolStripMenuItem = new ToolStripMenuItem();
            PluginCM.SuspendLayout();
            SuspendLayout();
            // 
            // PluginLB
            // 
            PluginLB.BackColor = Color.FromArgb(60, 63, 65);
            PluginLB.Columns.AddRange(new ColumnHeader[] { PluginHeader, InstalledHeader });
            PluginLB.ForeColor = SystemColors.Control;
            PluginLB.FullRowSelect = true;
            PluginLB.Location = new Point(12, 12);
            PluginLB.MultiSelect = false;
            PluginLB.Name = "PluginLB";
            PluginLB.Size = new Size(307, 385);
            PluginLB.TabIndex = 1;
            PluginLB.UseCompatibleStateImageBehavior = false;
            PluginLB.View = View.Details;
            PluginLB.MouseDown += PluginLB_MouseDown;
            // 
            // PluginHeader
            // 
            PluginHeader.Text = "Plugins";
            PluginHeader.Width = 200;
            // 
            // InstalledHeader
            // 
            InstalledHeader.Text = "Installed";
            InstalledHeader.Width = 100;
            // 
            // PluginCM
            // 
            PluginCM.BackColor = Color.FromArgb(60, 63, 65);
            PluginCM.Items.AddRange(new ToolStripItem[] { downloadAndInstallToolStripMenuItem, removeToolStripMenuItem });
            PluginCM.Name = "PluginCM";
            PluginCM.ShowImageMargin = false;
            PluginCM.ShowItemToolTips = false;
            PluginCM.Size = new Size(161, 48);
            // 
            // downloadAndInstallToolStripMenuItem
            // 
            downloadAndInstallToolStripMenuItem.ForeColor = SystemColors.Control;
            downloadAndInstallToolStripMenuItem.Name = "downloadAndInstallToolStripMenuItem";
            downloadAndInstallToolStripMenuItem.Size = new Size(160, 22);
            downloadAndInstallToolStripMenuItem.Text = "Download and Install";
            downloadAndInstallToolStripMenuItem.Click += downloadAndInstallToolStripMenuItem_Click;
            // 
            // removeToolStripMenuItem
            // 
            removeToolStripMenuItem.ForeColor = SystemColors.Control;
            removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            removeToolStripMenuItem.Size = new Size(160, 22);
            removeToolStripMenuItem.Text = "Remove";
            removeToolStripMenuItem.Click += removeToolStripMenuItem_Click;
            // 
            // ProjectForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(800, 450);
            Controls.Add(PluginLB);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            Name = "ProjectForm";
            Text = "Form1";
            FormClosed += ProjectForm_FormClosed;
            Load += ProjectForm_Load;
            PluginCM.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListView PluginLB;
        private ColumnHeader PluginHeader;
        private ColumnHeader InstalledHeader;
        private ContextMenuStrip PluginCM;
        private ToolStripMenuItem downloadAndInstallToolStripMenuItem;
        private ToolStripMenuItem removeToolStripMenuItem;
    }
}
