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
            groupBox1 = new GroupBox();
            button1 = new Button();
            PluginCM.SuspendLayout();
            groupBox1.SuspendLayout();
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
            // groupBox1
            // 
            groupBox1.Controls.Add(button1);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(325, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(792, 384);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "New Project";
            // 
            // button1
            // 
            button1.Location = new Point(150, 78);
            button1.Name = "button1";
            button1.Size = new Size(272, 23);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // ProjectForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(1148, 408);
            Controls.Add(groupBox1);
            Controls.Add(PluginLB);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            Name = "ProjectForm";
            Text = "Form1";
            FormClosed += ProjectForm_FormClosed;
            Load += ProjectForm_Load;
            PluginCM.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListView PluginLB;
        private ColumnHeader PluginHeader;
        private ColumnHeader InstalledHeader;
        private ContextMenuStrip PluginCM;
        private ToolStripMenuItem downloadAndInstallToolStripMenuItem;
        private ToolStripMenuItem removeToolStripMenuItem;
        private GroupBox groupBox1;
        private Button button1;
    }
}
