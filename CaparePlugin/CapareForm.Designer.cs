namespace CaparePlugin
{
    partial class CapareForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CapareForm));
            panel1 = new Panel();
            NextButton = new Button();
            FindButton = new Button();
            txtSearch = new TextBox();
            OpenFolderButton = new Button();
            SaveButton = new Button();
            splitContainer1 = new SplitContainer();
            CapareTV = new Day2eEditor.MultiSelectTreeView();
            _mapControl = new Day2eEditor.MapViewerControl();
            CapareCM = new ContextMenuStrip(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            //
            // panel1
            //
            panel1.BackColor = Color.FromArgb(60, 63, 65);
            panel1.Controls.Add(NextButton);
            panel1.Controls.Add(FindButton);
            panel1.Controls.Add(txtSearch);
            panel1.Controls.Add(OpenFolderButton);
            panel1.Controls.Add(SaveButton);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1192, 31);
            panel1.TabIndex = 6;
            //
            // NextButton
            //
            NextButton.FlatStyle = FlatStyle.Flat;
            NextButton.Location = new Point(332, 4);
            NextButton.Name = "NextButton";
            NextButton.Size = new Size(56, 23);
            NextButton.TabIndex = 9;
            NextButton.Text = "Next";
            NextButton.UseVisualStyleBackColor = true;
            NextButton.Click += NextButton_Click;
            //
            // FindButton
            //
            FindButton.FlatStyle = FlatStyle.Flat;
            FindButton.Location = new Point(284, 4);
            FindButton.Name = "FindButton";
            FindButton.Size = new Size(42, 23);
            FindButton.TabIndex = 8;
            FindButton.Text = "Find";
            FindButton.UseVisualStyleBackColor = true;
            FindButton.Click += FindButton_Click;
            //
            // txtSearch
            //
            txtSearch.BackColor = Color.FromArgb(60, 63, 65);
            txtSearch.ForeColor = SystemColors.Control;
            txtSearch.Location = new Point(105, 4);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(173, 23);
            txtSearch.TabIndex = 7;
            //
            // OpenFolderButton
            //
            OpenFolderButton.BackgroundImage = (Image)resources.GetObject("OpenFolderButton.BackgroundImage");
            OpenFolderButton.BackgroundImageLayout = ImageLayout.Stretch;
            OpenFolderButton.FlatStyle = FlatStyle.Flat;
            OpenFolderButton.Location = new Point(58, 3);
            OpenFolderButton.Name = "OpenFolderButton";
            OpenFolderButton.Size = new Size(40, 25);
            OpenFolderButton.TabIndex = 1;
            OpenFolderButton.TextImageRelation = TextImageRelation.ImageAboveText;
            OpenFolderButton.UseVisualStyleBackColor = true;
            OpenFolderButton.Click += OpenFolderButton_Click;
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
            // splitContainer1
            //
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 31);
            splitContainer1.Name = "splitContainer1";
            //
            // splitContainer1.Panel1
            //
            splitContainer1.Panel1.Controls.Add(CapareTV);
            //
            // splitContainer1.Panel2
            //
            splitContainer1.Panel2.Controls.Add(_mapControl);
            splitContainer1.Size = new Size(1192, 657);
            splitContainer1.SplitterDistance = 394;
            splitContainer1.SplitterWidth = 10;
            splitContainer1.TabIndex = 5;
            //
            // CapareTV
            //
            CapareTV.BackColor = Color.FromArgb(60, 63, 65);
            CapareTV.CollapseSelectedNodeOnly = true;
            CapareTV.Dock = DockStyle.Fill;
            CapareTV.ForeColor = SystemColors.Control;
            CapareTV.HideSelection = false;
            CapareTV.LineColor = Color.FromArgb(240, 240, 240);
            CapareTV.Location = new Point(0, 0);
            CapareTV.Name = "CapareTV";
            CapareTV.Size = new Size(394, 657);
            CapareTV.TabIndex = 0;
            CapareTV.AfterSelect += CapareTV_AfterSelect;
            CapareTV.NodeMouseClick += CapareTV_NodeMouseClick;
            //
            // _mapControl
            //
            _mapControl.Dock = DockStyle.Fill;
            _mapControl.Location = new Point(0, 0);
            _mapControl.Name = "_mapControl";
            _mapControl.Size = new Size(788, 657);
            _mapControl.TabIndex = 1;
            _mapControl.Text = "mapViewerControl1";
            _mapControl.Visible = false;
            //
            // CapareCM
            //
            CapareCM.Name = "CapareCM";
            CapareCM.Size = new Size(61, 4);
            //
            // CapareForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(1192, 688);
            Controls.Add(splitContainer1);
            Controls.Add(panel1);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            Name = "CapareForm";
            Text = "Capare Manager";
            FormClosing += CapareForm_FormClosing;
            FormClosed += CapareForm_FormClosed;
            Load += CapareForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button NextButton;
        private Button FindButton;
        private TextBox txtSearch;
        private Button OpenFolderButton;
        private Button SaveButton;
        private SplitContainer splitContainer1;
        private Day2eEditor.MultiSelectTreeView CapareTV;
        private Day2eEditor.MapViewerControl _mapControl;
        private ContextMenuStrip CapareCM;
    }
}
