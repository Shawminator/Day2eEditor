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
            CreateProjectbutton = new Button();
            MissionFoldertoUsebutton = new Button();
            MissionFoldertoUselabel = new Label();
            ProjectMissionFolderTB = new TextBox();
            selectProfilefolderNamebutton = new Button();
            ProfileFolderNamelabel = new Label();
            ProjectProfileTB = new TextBox();
            SelectProjectFolderbutton = new Button();
            SelectsetupTypeLabel = new Label();
            ProjectTypeComboBox = new ComboBox();
            SelectProjectFolderlabel = new Label();
            ProjectFolderTB = new TextBox();
            ProjectNameLabel = new Label();
            ProjectNameTB = new TextBox();
            listBoxProjects = new ListBox();
            ProjectsCM = new ContextMenuStrip(components);
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            groupBox2 = new GroupBox();
            button2 = new Button();
            label7 = new Label();
            EditCreateBackupsCB = new CheckBox();
            label6 = new Label();
            EditMapSizeNUD = new NumericUpDown();
            label5 = new Label();
            EditMapPathTB = new TextBox();
            label4 = new Label();
            EditMissionPathTB = new TextBox();
            label3 = new Label();
            EditProfilePathTB = new TextBox();
            label2 = new Label();
            EditProjectRootTB = new TextBox();
            label1 = new Label();
            EditProjectNameTB = new TextBox();
            button1 = new Button();
            button3 = new Button();
            MapAddonsLB = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            PluginCM.SuspendLayout();
            groupBox1.SuspendLayout();
            ProjectsCM.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)EditMapSizeNUD).BeginInit();
            SuspendLayout();
            // 
            // PluginLB
            // 
            PluginLB.BackColor = Color.FromArgb(60, 63, 65);
            PluginLB.Columns.AddRange(new ColumnHeader[] { PluginHeader, InstalledHeader });
            PluginLB.ForeColor = SystemColors.Control;
            PluginLB.FullRowSelect = true;
            PluginLB.Location = new Point(12, 41);
            PluginLB.MultiSelect = false;
            PluginLB.Name = "PluginLB";
            PluginLB.Size = new Size(307, 402);
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
            groupBox1.BackColor = Color.FromArgb(60, 63, 65);
            groupBox1.Controls.Add(CreateProjectbutton);
            groupBox1.Controls.Add(MissionFoldertoUsebutton);
            groupBox1.Controls.Add(MissionFoldertoUselabel);
            groupBox1.Controls.Add(ProjectMissionFolderTB);
            groupBox1.Controls.Add(selectProfilefolderNamebutton);
            groupBox1.Controls.Add(ProfileFolderNamelabel);
            groupBox1.Controls.Add(ProjectProfileTB);
            groupBox1.Controls.Add(SelectProjectFolderbutton);
            groupBox1.Controls.Add(SelectsetupTypeLabel);
            groupBox1.Controls.Add(ProjectTypeComboBox);
            groupBox1.Controls.Add(SelectProjectFolderlabel);
            groupBox1.Controls.Add(ProjectFolderTB);
            groupBox1.Controls.Add(ProjectNameLabel);
            groupBox1.Controls.Add(ProjectNameTB);
            groupBox1.Controls.Add(listBoxProjects);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(325, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(792, 199);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Projects";
            // 
            // CreateProjectbutton
            // 
            CreateProjectbutton.BackColor = Color.FromArgb(60, 63, 65);
            CreateProjectbutton.FlatStyle = FlatStyle.Flat;
            CreateProjectbutton.Location = new Point(615, 167);
            CreateProjectbutton.Name = "CreateProjectbutton";
            CreateProjectbutton.Size = new Size(171, 23);
            CreateProjectbutton.TabIndex = 15;
            CreateProjectbutton.Text = "Create Project";
            CreateProjectbutton.UseVisualStyleBackColor = false;
            CreateProjectbutton.Click += CreateProjectbutton_Click;
            // 
            // MissionFoldertoUsebutton
            // 
            MissionFoldertoUsebutton.BackColor = SystemColors.Control;
            MissionFoldertoUsebutton.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MissionFoldertoUsebutton.ForeColor = SystemColors.ActiveCaptionText;
            MissionFoldertoUsebutton.Location = new Point(764, 138);
            MissionFoldertoUsebutton.Name = "MissionFoldertoUsebutton";
            MissionFoldertoUsebutton.Size = new Size(23, 23);
            MissionFoldertoUsebutton.TabIndex = 14;
            MissionFoldertoUsebutton.Text = "+";
            MissionFoldertoUsebutton.UseVisualStyleBackColor = false;
            MissionFoldertoUsebutton.Click += MissionFoldertoUsebutton_Click;
            // 
            // MissionFoldertoUselabel
            // 
            MissionFoldertoUselabel.AutoSize = true;
            MissionFoldertoUselabel.Location = new Point(202, 141);
            MissionFoldertoUselabel.Name = "MissionFoldertoUselabel";
            MissionFoldertoUselabel.Size = new Size(93, 15);
            MissionFoldertoUselabel.TabIndex = 13;
            MissionFoldertoUselabel.Text = "MPMission Path";
            // 
            // ProjectMissionFolderTB
            // 
            ProjectMissionFolderTB.BackColor = Color.FromArgb(60, 63, 65);
            ProjectMissionFolderTB.ForeColor = SystemColors.Control;
            ProjectMissionFolderTB.Location = new Point(334, 138);
            ProjectMissionFolderTB.Name = "ProjectMissionFolderTB";
            ProjectMissionFolderTB.Size = new Size(452, 23);
            ProjectMissionFolderTB.TabIndex = 12;
            // 
            // selectProfilefolderNamebutton
            // 
            selectProfilefolderNamebutton.BackColor = SystemColors.Control;
            selectProfilefolderNamebutton.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            selectProfilefolderNamebutton.ForeColor = SystemColors.ActiveCaptionText;
            selectProfilefolderNamebutton.Location = new Point(764, 109);
            selectProfilefolderNamebutton.Name = "selectProfilefolderNamebutton";
            selectProfilefolderNamebutton.Size = new Size(23, 23);
            selectProfilefolderNamebutton.TabIndex = 11;
            selectProfilefolderNamebutton.Text = "+";
            selectProfilefolderNamebutton.UseVisualStyleBackColor = false;
            selectProfilefolderNamebutton.Click += selectProfilefolderNamebutton_Click;
            // 
            // ProfileFolderNamelabel
            // 
            ProfileFolderNamelabel.AutoSize = true;
            ProfileFolderNamelabel.Location = new Point(202, 112);
            ProfileFolderNamelabel.Name = "ProfileFolderNamelabel";
            ProfileFolderNamelabel.Size = new Size(68, 15);
            ProfileFolderNamelabel.TabIndex = 10;
            ProfileFolderNamelabel.Text = "Profile Path";
            // 
            // ProjectProfileTB
            // 
            ProjectProfileTB.BackColor = Color.FromArgb(60, 63, 65);
            ProjectProfileTB.ForeColor = SystemColors.Control;
            ProjectProfileTB.Location = new Point(334, 109);
            ProjectProfileTB.Name = "ProjectProfileTB";
            ProjectProfileTB.Size = new Size(452, 23);
            ProjectProfileTB.TabIndex = 9;
            // 
            // SelectProjectFolderbutton
            // 
            SelectProjectFolderbutton.BackColor = SystemColors.Control;
            SelectProjectFolderbutton.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            SelectProjectFolderbutton.ForeColor = SystemColors.ActiveCaptionText;
            SelectProjectFolderbutton.Location = new Point(764, 80);
            SelectProjectFolderbutton.Name = "SelectProjectFolderbutton";
            SelectProjectFolderbutton.Size = new Size(23, 23);
            SelectProjectFolderbutton.TabIndex = 8;
            SelectProjectFolderbutton.Text = "+";
            SelectProjectFolderbutton.UseVisualStyleBackColor = false;
            SelectProjectFolderbutton.Click += SelectProjectFolderbutton_Click;
            // 
            // SelectsetupTypeLabel
            // 
            SelectsetupTypeLabel.AutoSize = true;
            SelectsetupTypeLabel.Location = new Point(202, 25);
            SelectsetupTypeLabel.Name = "SelectsetupTypeLabel";
            SelectsetupTypeLabel.Size = new Size(71, 15);
            SelectsetupTypeLabel.TabIndex = 6;
            SelectsetupTypeLabel.Text = "Project Type";
            // 
            // ProjectTypeComboBox
            // 
            ProjectTypeComboBox.BackColor = Color.FromArgb(60, 63, 65);
            ProjectTypeComboBox.ForeColor = SystemColors.Control;
            ProjectTypeComboBox.FormattingEnabled = true;
            ProjectTypeComboBox.Items.AddRange(new object[] { "Create Blank", "Create Project to Existing Project Files" });
            ProjectTypeComboBox.Location = new Point(334, 22);
            ProjectTypeComboBox.Name = "ProjectTypeComboBox";
            ProjectTypeComboBox.Size = new Size(452, 23);
            ProjectTypeComboBox.TabIndex = 5;
            ProjectTypeComboBox.SelectedIndexChanged += ProjectTypeComboBox_SelectedIndexChanged;
            // 
            // SelectProjectFolderlabel
            // 
            SelectProjectFolderlabel.AutoSize = true;
            SelectProjectFolderlabel.Location = new Point(202, 83);
            SelectProjectFolderlabel.Name = "SelectProjectFolderlabel";
            SelectProjectFolderlabel.Size = new Size(72, 15);
            SelectProjectFolderlabel.TabIndex = 4;
            SelectProjectFolderlabel.Text = "Project Root";
            // 
            // ProjectFolderTB
            // 
            ProjectFolderTB.BackColor = Color.FromArgb(60, 63, 65);
            ProjectFolderTB.ForeColor = SystemColors.Control;
            ProjectFolderTB.Location = new Point(334, 80);
            ProjectFolderTB.Name = "ProjectFolderTB";
            ProjectFolderTB.Size = new Size(424, 23);
            ProjectFolderTB.TabIndex = 3;
            // 
            // ProjectNameLabel
            // 
            ProjectNameLabel.AutoSize = true;
            ProjectNameLabel.Location = new Point(202, 54);
            ProjectNameLabel.Name = "ProjectNameLabel";
            ProjectNameLabel.Size = new Size(79, 15);
            ProjectNameLabel.TabIndex = 2;
            ProjectNameLabel.Text = "Project Name";
            // 
            // ProjectNameTB
            // 
            ProjectNameTB.BackColor = Color.FromArgb(60, 63, 65);
            ProjectNameTB.ForeColor = SystemColors.Control;
            ProjectNameTB.Location = new Point(334, 51);
            ProjectNameTB.Name = "ProjectNameTB";
            ProjectNameTB.Size = new Size(452, 23);
            ProjectNameTB.TabIndex = 1;
            // 
            // listBoxProjects
            // 
            listBoxProjects.BackColor = Color.FromArgb(60, 63, 65);
            listBoxProjects.ForeColor = SystemColors.Control;
            listBoxProjects.FormattingEnabled = true;
            listBoxProjects.Location = new Point(6, 22);
            listBoxProjects.Name = "listBoxProjects";
            listBoxProjects.Size = new Size(180, 169);
            listBoxProjects.TabIndex = 0;
            listBoxProjects.SelectedIndexChanged += listBoxProjects_SelectedIndexChanged;
            listBoxProjects.MouseDown += listBoxProjects_MouseDown;
            // 
            // ProjectsCM
            // 
            ProjectsCM.BackColor = Color.FromArgb(60, 63, 65);
            ProjectsCM.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem2 });
            ProjectsCM.Name = "PluginCM";
            ProjectsCM.ShowImageMargin = false;
            ProjectsCM.ShowItemToolTips = false;
            ProjectsCM.Size = new Size(116, 48);
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.ForeColor = SystemColors.Control;
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(115, 22);
            toolStripMenuItem1.Text = "Set as Active";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.ForeColor = SystemColors.Control;
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(115, 22);
            toolStripMenuItem2.Text = "Remove";
            toolStripMenuItem2.Click += toolStripMenuItem2_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(button2);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(EditCreateBackupsCB);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(EditMapSizeNUD);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(EditMapPathTB);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(EditMissionPathTB);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(EditProfilePathTB);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(EditProjectRootTB);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(EditProjectNameTB);
            groupBox2.ForeColor = SystemColors.Control;
            groupBox2.Location = new Point(325, 217);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(792, 226);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Highlighted Project";
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(60, 63, 65);
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(615, 196);
            button2.Name = "button2";
            button2.Size = new Size(171, 23);
            button2.TabIndex = 23;
            button2.Text = "Save Changes";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(15, 195);
            label7.Name = "label7";
            label7.Size = new Size(88, 15);
            label7.TabIndex = 21;
            label7.Text = "Create Backups";
            // 
            // EditCreateBackupsCB
            // 
            EditCreateBackupsCB.AutoSize = true;
            EditCreateBackupsCB.Location = new Point(147, 196);
            EditCreateBackupsCB.Name = "EditCreateBackupsCB";
            EditCreateBackupsCB.Size = new Size(15, 14);
            EditCreateBackupsCB.TabIndex = 20;
            EditCreateBackupsCB.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(15, 169);
            label6.Name = "label6";
            label6.Size = new Size(54, 15);
            label6.TabIndex = 19;
            label6.Text = "Map Size";
            // 
            // EditMapSizeNUD
            // 
            EditMapSizeNUD.BackColor = Color.FromArgb(60, 63, 65);
            EditMapSizeNUD.ForeColor = SystemColors.Control;
            EditMapSizeNUD.Location = new Point(147, 167);
            EditMapSizeNUD.Maximum = new decimal(new int[] { 50000, 0, 0, 0 });
            EditMapSizeNUD.Name = "EditMapSizeNUD";
            EditMapSizeNUD.Size = new Size(148, 23);
            EditMapSizeNUD.TabIndex = 18;
            EditMapSizeNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(15, 141);
            label5.Name = "label5";
            label5.Size = new Size(102, 15);
            label5.TabIndex = 17;
            label5.Text = "Relative Map Path";
            // 
            // EditMapPathTB
            // 
            EditMapPathTB.BackColor = Color.FromArgb(60, 63, 65);
            EditMapPathTB.ForeColor = SystemColors.Control;
            EditMapPathTB.Location = new Point(147, 138);
            EditMapPathTB.Name = "EditMapPathTB";
            EditMapPathTB.Size = new Size(452, 23);
            EditMapPathTB.TabIndex = 16;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 112);
            label4.Name = "label4";
            label4.Size = new Size(93, 15);
            label4.TabIndex = 15;
            label4.Text = "MPMission Path";
            // 
            // EditMissionPathTB
            // 
            EditMissionPathTB.BackColor = Color.FromArgb(60, 63, 65);
            EditMissionPathTB.ForeColor = SystemColors.Control;
            EditMissionPathTB.Location = new Point(147, 109);
            EditMissionPathTB.Name = "EditMissionPathTB";
            EditMissionPathTB.Size = new Size(452, 23);
            EditMissionPathTB.TabIndex = 14;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 83);
            label3.Name = "label3";
            label3.Size = new Size(68, 15);
            label3.TabIndex = 12;
            label3.Text = "Profile Path";
            // 
            // EditProfilePathTB
            // 
            EditProfilePathTB.BackColor = Color.FromArgb(60, 63, 65);
            EditProfilePathTB.ForeColor = SystemColors.Control;
            EditProfilePathTB.Location = new Point(147, 80);
            EditProfilePathTB.Name = "EditProfilePathTB";
            EditProfilePathTB.Size = new Size(452, 23);
            EditProfilePathTB.TabIndex = 11;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 54);
            label2.Name = "label2";
            label2.Size = new Size(59, 15);
            label2.TabIndex = 6;
            label2.Text = "Root Path";
            // 
            // EditProjectRootTB
            // 
            EditProjectRootTB.BackColor = Color.FromArgb(60, 63, 65);
            EditProjectRootTB.ForeColor = SystemColors.Control;
            EditProjectRootTB.Location = new Point(147, 51);
            EditProjectRootTB.Name = "EditProjectRootTB";
            EditProjectRootTB.Size = new Size(424, 23);
            EditProjectRootTB.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 25);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 4;
            label1.Text = "Name";
            // 
            // EditProjectNameTB
            // 
            EditProjectNameTB.BackColor = Color.FromArgb(60, 63, 65);
            EditProjectNameTB.ForeColor = SystemColors.Control;
            EditProjectNameTB.Location = new Point(147, 22);
            EditProjectNameTB.Name = "EditProjectNameTB";
            EditProjectNameTB.Size = new Size(452, 23);
            EditProjectNameTB.TabIndex = 3;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(60, 63, 65);
            button1.Enabled = false;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(12, 12);
            button1.Name = "button1";
            button1.Size = new Size(148, 23);
            button1.TabIndex = 24;
            button1.Text = "Plugins";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(60, 63, 65);
            button3.FlatStyle = FlatStyle.Flat;
            button3.Location = new Point(171, 12);
            button3.Name = "button3";
            button3.Size = new Size(148, 23);
            button3.TabIndex = 25;
            button3.Text = "MapAddons";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // MapAddonsLB
            // 
            MapAddonsLB.BackColor = Color.FromArgb(60, 63, 65);
            MapAddonsLB.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            MapAddonsLB.ForeColor = SystemColors.Control;
            MapAddonsLB.FullRowSelect = true;
            MapAddonsLB.Location = new Point(12, 41);
            MapAddonsLB.MultiSelect = false;
            MapAddonsLB.Name = "MapAddonsLB";
            MapAddonsLB.Size = new Size(307, 402);
            MapAddonsLB.TabIndex = 26;
            MapAddonsLB.UseCompatibleStateImageBehavior = false;
            MapAddonsLB.View = View.Details;
            MapAddonsLB.MouseDown += PluginLB_MouseDown;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "MapAddons";
            columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Installed";
            columnHeader2.Width = 100;
            // 
            // ProjectForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(1148, 455);
            Controls.Add(button3);
            Controls.Add(button1);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(PluginLB);
            Controls.Add(MapAddonsLB);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            Name = "ProjectForm";
            Text = "Form1";
            FormClosed += ProjectForm_FormClosed;
            Load += ProjectForm_Load;
            PluginCM.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ProjectsCM.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)EditMapSizeNUD).EndInit();
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
        private TextBox ProjectNameTB;
        private ListBox listBoxProjects;
        private Label SelectProjectFolderlabel;
        private TextBox ProjectFolderTB;
        private Label ProjectNameLabel;
        private Label SelectsetupTypeLabel;
        private ComboBox ProjectTypeComboBox;
        private Button SelectProjectFolderbutton;
        private Button MissionFoldertoUsebutton;
        private Label MissionFoldertoUselabel;
        private TextBox ProjectMissionFolderTB;
        private Button selectProfilefolderNamebutton;
        private Label ProfileFolderNamelabel;
        private TextBox ProjectProfileTB;
        private Button CreateProjectbutton;
        private ContextMenuStrip ProjectsCM;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private GroupBox groupBox2;
        private Label label7;
        private CheckBox EditCreateBackupsCB;
        private Label label6;
        private NumericUpDown EditMapSizeNUD;
        private Label label5;
        private TextBox EditMapPathTB;
        private Label label4;
        private TextBox EditMissionPathTB;
        private Label label3;
        private TextBox EditProfilePathTB;
        private Label label2;
        private TextBox EditProjectRootTB;
        private Label label1;
        private TextBox EditProjectNameTB;
        private Button button2;
        private Button button1;
        private Button button3;
        private ListView MapAddonsLB;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
    }
}
