namespace ExpansionPlugin.Forms
{
    partial class TraderPreviewForm
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
            _root = new TableLayoutPanel();
            _topPanel = new Panel();
            layout = new TableLayoutPanel();
            traderIconBox = new PictureBox();
            _titleLabel = new Label();
            _summaryLabel = new Label();
            searchPanel = new FlowLayoutPanel();
            searchLabel = new Label();
            _searchBox = new TextBox();
            splitContainer1 = new SplitContainer();
            _leftScrollPanel = new Panel();
            _detailsPanel = new Panel();
            _detailsTitle = new Label();
            _root.SuspendLayout();
            _topPanel.SuspendLayout();
            layout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)traderIconBox).BeginInit();
            searchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            _detailsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // _root
            // 
            _root.BackColor = Color.Transparent;
            _root.ColumnCount = 1;
            _root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _root.Controls.Add(_topPanel, 0, 0);
            _root.Controls.Add(splitContainer1, 0, 1);
            _root.Dock = DockStyle.Fill;
            _root.Location = new Point(0, 0);
            _root.Name = "_root";
            _root.RowCount = 2;
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            _root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _root.Size = new Size(1132, 616);
            _root.TabIndex = 0;
            // 
            // _topPanel
            // 
            _topPanel.Controls.Add(layout);
            _topPanel.Dock = DockStyle.Fill;
            _topPanel.Location = new Point(3, 3);
            _topPanel.Name = "_topPanel";
            _topPanel.Padding = new Padding(10);
            _topPanel.Size = new Size(1126, 64);
            _topPanel.TabIndex = 0;
            // 
            // layout
            // 
            layout.ColumnCount = 3;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 340F));
            layout.Controls.Add(traderIconBox, 0, 0);
            layout.Controls.Add(_titleLabel, 1, 0);
            layout.Controls.Add(_summaryLabel, 1, 1);
            layout.Controls.Add(searchPanel, 2, 0);
            layout.Dock = DockStyle.Fill;
            layout.Location = new Point(10, 10);
            layout.Name = "layout";
            layout.RowCount = 2;
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layout.Size = new Size(1106, 44);
            layout.TabIndex = 0;
            // 
            // traderIconBox
            // 
            traderIconBox.BackgroundImageLayout = ImageLayout.Zoom;
            traderIconBox.Dock = DockStyle.Fill;
            traderIconBox.Location = new Point(3, 3);
            traderIconBox.Name = "traderIconBox";
            layout.SetRowSpan(traderIconBox, 2);
            traderIconBox.Size = new Size(44, 38);
            traderIconBox.TabIndex = 0;
            traderIconBox.TabStop = false;
            // 
            // _titleLabel
            // 
            _titleLabel.AutoSize = true;
            _titleLabel.BackColor = Color.Transparent;
            _titleLabel.Dock = DockStyle.Fill;
            _titleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _titleLabel.ForeColor = SystemColors.Control;
            _titleLabel.Location = new Point(53, 0);
            _titleLabel.Name = "_titleLabel";
            _titleLabel.Size = new Size(710, 22);
            _titleLabel.TabIndex = 1;
            _titleLabel.Text = "_titleLabel";
            // 
            // _summaryLabel
            // 
            _summaryLabel.AutoSize = true;
            _summaryLabel.Dock = DockStyle.Fill;
            _summaryLabel.ForeColor = SystemColors.Control;
            _summaryLabel.Location = new Point(53, 22);
            _summaryLabel.Name = "_summaryLabel";
            _summaryLabel.Size = new Size(710, 22);
            _summaryLabel.TabIndex = 2;
            _summaryLabel.Text = "_summaryLabel";
            // 
            // searchPanel
            // 
            searchPanel.Controls.Add(searchLabel);
            searchPanel.Controls.Add(_searchBox);
            searchPanel.Dock = DockStyle.Fill;
            searchPanel.Location = new Point(769, 3);
            searchPanel.Name = "searchPanel";
            searchPanel.Padding = new Padding(0, 8, 0, 0);
            layout.SetRowSpan(searchPanel, 2);
            searchPanel.Size = new Size(334, 38);
            searchPanel.TabIndex = 3;
            searchPanel.WrapContents = false;
            // 
            // searchLabel
            // 
            searchLabel.AutoSize = true;
            searchLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            searchLabel.ForeColor = SystemColors.Control;
            searchLabel.Location = new Point(3, 8);
            searchLabel.Name = "searchLabel";
            searchLabel.Size = new Size(45, 15);
            searchLabel.TabIndex = 0;
            searchLabel.Text = "Search";
            // 
            // _searchBox
            // 
            _searchBox.BorderStyle = BorderStyle.FixedSingle;
            _searchBox.Location = new Point(51, 14);
            _searchBox.Margin = new Padding(0, 6, 8, 0);
            _searchBox.Name = "_searchBox";
            _searchBox.Size = new Size(250, 23);
            _searchBox.TabIndex = 1;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 73);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(_leftScrollPanel);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(_detailsPanel);
            splitContainer1.Size = new Size(1126, 540);
            splitContainer1.SplitterDistance = 644;
            splitContainer1.TabIndex = 1;
            // 
            // _leftScrollPanel
            // 
            _leftScrollPanel.AutoScroll = true;
            _leftScrollPanel.BackColor = Color.Transparent;
            _leftScrollPanel.Dock = DockStyle.Fill;
            _leftScrollPanel.Location = new Point(0, 0);
            _leftScrollPanel.Name = "_leftScrollPanel";
            _leftScrollPanel.Padding = new Padding(8);
            _leftScrollPanel.Size = new Size(644, 540);
            _leftScrollPanel.TabIndex = 0;
            // 
            // _detailsPanel
            // 
            _detailsPanel.Controls.Add(_detailsTitle);
            _detailsPanel.Dock = DockStyle.Fill;
            _detailsPanel.Location = new Point(0, 0);
            _detailsPanel.Name = "_detailsPanel";
            _detailsPanel.Size = new Size(478, 540);
            _detailsPanel.TabIndex = 0;
            // 
            // _detailsTitle
            // 
            _detailsTitle.AutoSize = true;
            _detailsTitle.Dock = DockStyle.Top;
            _detailsTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            _detailsTitle.ForeColor = SystemColors.Control;
            _detailsTitle.Location = new Point(0, 0);
            _detailsTitle.Name = "_detailsTitle";
            _detailsTitle.Padding = new Padding(8);
            _detailsTitle.Size = new Size(118, 37);
            _detailsTitle.TabIndex = 0;
            _detailsTitle.Text = "Item Details";
            // 
            // TraderPreviewForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.market_background;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1132, 616);
            Controls.Add(_root);
            Name = "TraderPreviewForm";
            Text = "TraderPreviewForm";
            _root.ResumeLayout(false);
            _topPanel.ResumeLayout(false);
            layout.ResumeLayout(false);
            layout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)traderIconBox).EndInit();
            searchPanel.ResumeLayout(false);
            searchPanel.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            _detailsPanel.ResumeLayout(false);
            _detailsPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel _root;
        private Panel _topPanel;
        private TableLayoutPanel layout;
        private PictureBox traderIconBox;
        private Label _titleLabel;
        private Label _summaryLabel;
        private FlowLayoutPanel searchPanel;
        private Label searchLabel;
        private TextBox _searchBox;
        private SplitContainer splitContainer1;
        private Panel _leftScrollPanel;
        private Panel _detailsPanel;
        private Label _detailsTitle;
    }
}