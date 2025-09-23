namespace ExpansionPlugin
{
    partial class ExpansionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExpansionForm));
            panel1 = new Panel();
            SaveButton = new Button();
            splitContainer1 = new SplitContainer();
            ExpansionTV = new Day2eEditor.MultiSelectTreeView();
            _mapControl = new Day2eEditor.MapViewerControl();
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
            panel1.Controls.Add(SaveButton);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1192, 31);
            panel1.TabIndex = 6;
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
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 31);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(ExpansionTV);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(_mapControl);
            splitContainer1.Size = new Size(1192, 657);
            splitContainer1.SplitterDistance = 394;
            splitContainer1.SplitterWidth = 10;
            splitContainer1.TabIndex = 5;
            // 
            // ExpansionTV
            // 
            ExpansionTV.BackColor = Color.FromArgb(60, 63, 65);
            ExpansionTV.Dock = DockStyle.Fill;
            ExpansionTV.ForeColor = SystemColors.Control;
            ExpansionTV.LineColor = Color.FromArgb(240, 240, 240);
            ExpansionTV.Location = new Point(0, 0);
            ExpansionTV.Name = "ExpansionTV";
            ExpansionTV.Size = new Size(394, 657);
            ExpansionTV.TabIndex = 0;
            ExpansionTV.AfterSelect += ExpansionTV_AfterSelect;
            ExpansionTV.NodeMouseClick += ExpansionTV_NodeMouseClick;
            // 
            // _mapControl
            // 
            _mapControl.Dock = DockStyle.Fill;
            _mapControl.Location = new Point(0, 0);
            _mapControl.Name = "_mapControl";
            _mapControl.Size = new Size(788, 657);
            _mapControl.TabIndex = 1;
            _mapControl.Text = "mapViewerControl1";
            // 
            // ExpansionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(1192, 688);
            Controls.Add(splitContainer1);
            Controls.Add(panel1);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            Name = "ExpansionForm";
            Text = "Form1";
            FormClosed += ExpansionForm_FormClosed;
            Load += ExpansionForm_Load;
            panel1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private CheckBox TerritorieszonesCB;
        private Button SaveButton;
        private SplitContainer splitContainer1;
        private Day2eEditor.MultiSelectTreeView ExpansionTV;
        private Day2eEditor.MapViewerControl _mapControl;
    }
}
