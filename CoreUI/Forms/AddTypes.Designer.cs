namespace Day2eEditor
{
    partial class AddTypes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddTypes));
            TitlePanel = new Panel();
            button1 = new Button();
            MinimiseButton = new Button();
            CloseButton = new Button();
            TitleLabel = new Label();
            label1 = new Label();
            _grid = new DataGridView();
            NameGridColumn = new DataGridViewTextBoxColumn();
            nominal = new DataGridViewTextBoxColumn();
            lifetime = new DataGridViewTextBoxColumn();
            restock = new DataGridViewTextBoxColumn();
            min = new DataGridViewTextBoxColumn();
            quantmin = new DataGridViewTextBoxColumn();
            quantmax = new DataGridViewTextBoxColumn();
            cost = new DataGridViewTextBoxColumn();
            Flags = new DataGridViewTextBoxColumn();
            Category = new DataGridViewTextBoxColumn();
            Tags = new DataGridViewTextBoxColumn();
            Usages = new DataGridViewTextBoxColumn();
            Values = new DataGridViewTextBoxColumn();
            ResizePanel = new Panel();
            SelectProjectFolderbutton = new Button();
            label2 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label3 = new Label();
            button4 = new Button();
            button2 = new Button();
            button3 = new Button();
            button5 = new Button();
            TitlePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
            SuspendLayout();
            // 
            // TitlePanel
            // 
            TitlePanel.BackColor = Color.Black;
            TitlePanel.Controls.Add(button1);
            TitlePanel.Controls.Add(MinimiseButton);
            TitlePanel.Controls.Add(CloseButton);
            TitlePanel.Controls.Add(TitleLabel);
            TitlePanel.Controls.Add(label1);
            TitlePanel.Dock = DockStyle.Top;
            TitlePanel.Location = new Point(0, 0);
            TitlePanel.Name = "TitlePanel";
            TitlePanel.Size = new Size(1130, 28);
            TitlePanel.TabIndex = 6;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.BackColor = Color.Black;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Segoe UI", 12F);
            button1.ForeColor = Color.DarkRed;
            button1.Location = new Point(1489, -2);
            button1.Name = "button1";
            button1.Size = new Size(41, 28);
            button1.TabIndex = 9;
            button1.Text = "X";
            button1.UseVisualStyleBackColor = false;
            // 
            // MinimiseButton
            // 
            MinimiseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            MinimiseButton.BackColor = Color.Black;
            MinimiseButton.FlatStyle = FlatStyle.Popup;
            MinimiseButton.Font = new Font("Segoe UI", 12F);
            MinimiseButton.ForeColor = Color.DarkRed;
            MinimiseButton.Location = new Point(2395, -3);
            MinimiseButton.Name = "MinimiseButton";
            MinimiseButton.Size = new Size(41, 28);
            MinimiseButton.TabIndex = 7;
            MinimiseButton.Text = "_";
            MinimiseButton.UseVisualStyleBackColor = false;
            // 
            // CloseButton
            // 
            CloseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CloseButton.BackColor = Color.Black;
            CloseButton.FlatStyle = FlatStyle.Popup;
            CloseButton.Font = new Font("Segoe UI", 12F);
            CloseButton.ForeColor = Color.DarkRed;
            CloseButton.Location = new Point(1086, 0);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(41, 28);
            CloseButton.TabIndex = 6;
            CloseButton.Text = "X";
            CloseButton.UseVisualStyleBackColor = false;
            // 
            // TitleLabel
            // 
            TitleLabel.AutoSize = true;
            TitleLabel.ForeColor = Color.FromArgb(75, 110, 175);
            TitleLabel.Location = new Point(5, 6);
            TitleLabel.Name = "TitleLabel";
            TitleLabel.Size = new Size(159, 15);
            TitleLabel.TabIndex = 0;
            TitleLabel.Text = "Day2eEditor by Shawminator";
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.ForeColor = Color.FromArgb(75, 110, 175);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(1130, 28);
            label1.TabIndex = 8;
            label1.Text = "Add Types";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _grid
            // 
            _grid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            _grid.BackgroundColor = Color.FromArgb(60, 63, 65);
            _grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _grid.Columns.AddRange(new DataGridViewColumn[] { NameGridColumn, nominal, lifetime, restock, min, quantmin, quantmax, cost, Flags, Category, Tags, Usages, Values });
            _grid.GridColor = SystemColors.ActiveCaptionText;
            _grid.Location = new Point(12, 123);
            _grid.Name = "_grid";
            _grid.Size = new Size(1094, 416);
            _grid.TabIndex = 7;
            _grid.CellDoubleClick += _grid_CellDoubleClick;
            _grid.CellFormatting += _grid_CellFormatting;
            _grid.KeyDown += _grid_KeyDown;
            // 
            // NameGridColumn
            // 
            NameGridColumn.HeaderText = "Classname";
            NameGridColumn.Name = "NameGridColumn";
            NameGridColumn.Width = 89;
            // 
            // nominal
            // 
            nominal.HeaderText = "nominal";
            nominal.Name = "nominal";
            nominal.Width = 76;
            // 
            // lifetime
            // 
            lifetime.HeaderText = "lifetime";
            lifetime.Name = "lifetime";
            lifetime.Width = 72;
            // 
            // restock
            // 
            restock.HeaderText = "restock";
            restock.Name = "restock";
            restock.Width = 70;
            // 
            // min
            // 
            min.HeaderText = "min";
            min.Name = "min";
            min.Width = 53;
            // 
            // quantmin
            // 
            quantmin.HeaderText = "quantmin";
            quantmin.Name = "quantmin";
            quantmin.Width = 84;
            // 
            // quantmax
            // 
            quantmax.HeaderText = "quantmax";
            quantmax.Name = "quantmax";
            quantmax.Width = 85;
            // 
            // cost
            // 
            cost.HeaderText = "cost";
            cost.Name = "cost";
            cost.Width = 54;
            // 
            // Flags
            // 
            Flags.HeaderText = "Flags";
            Flags.Name = "Flags";
            Flags.Resizable = DataGridViewTriState.True;
            Flags.SortMode = DataGridViewColumnSortMode.NotSortable;
            Flags.Width = 40;
            // 
            // Category
            // 
            Category.HeaderText = "Category";
            Category.Name = "Category";
            Category.Resizable = DataGridViewTriState.True;
            Category.SortMode = DataGridViewColumnSortMode.NotSortable;
            Category.Width = 61;
            // 
            // Tags
            // 
            Tags.HeaderText = "Tags";
            Tags.Name = "Tags";
            Tags.Width = 56;
            // 
            // Usages
            // 
            Usages.HeaderText = "Usages";
            Usages.Name = "Usages";
            Usages.Width = 69;
            // 
            // Values
            // 
            Values.HeaderText = "Values";
            Values.Name = "Values";
            Values.Width = 65;
            // 
            // ResizePanel
            // 
            ResizePanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ResizePanel.BackgroundImage = (Image)resources.GetObject("ResizePanel.BackgroundImage");
            ResizePanel.BackgroundImageLayout = ImageLayout.Stretch;
            ResizePanel.Location = new Point(1102, 536);
            ResizePanel.Name = "ResizePanel";
            ResizePanel.Size = new Size(25, 25);
            ResizePanel.TabIndex = 26;
            // 
            // SelectProjectFolderbutton
            // 
            SelectProjectFolderbutton.BackColor = SystemColors.Control;
            SelectProjectFolderbutton.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            SelectProjectFolderbutton.ForeColor = SystemColors.ActiveCaptionText;
            SelectProjectFolderbutton.Location = new Point(483, 94);
            SelectProjectFolderbutton.Name = "SelectProjectFolderbutton";
            SelectProjectFolderbutton.Size = new Size(23, 23);
            SelectProjectFolderbutton.TabIndex = 27;
            SelectProjectFolderbutton.Text = "+";
            SelectProjectFolderbutton.UseVisualStyleBackColor = false;
            SelectProjectFolderbutton.Click += SelectProjectFolderbutton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 69);
            label2.Name = "label2";
            label2.Size = new Size(82, 15);
            label2.TabIndex = 28;
            label2.Text = "New Filename";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(100, 66);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(406, 23);
            textBox1.TabIndex = 29;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(100, 95);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(377, 23);
            textBox2.TabIndex = 31;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 98);
            label3.Name = "label3";
            label3.Size = new Size(48, 15);
            label3.TabIndex = 30;
            label3.Text = "CE Path";
            // 
            // button4
            // 
            button4.BackColor = Color.FromArgb(60, 63, 65);
            button4.Location = new Point(12, 34);
            button4.Name = "button4";
            button4.Size = new Size(117, 23);
            button4.TabIndex = 32;
            button4.Text = "Import from XML";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(60, 63, 65);
            button2.Location = new Point(135, 34);
            button2.Name = "button2";
            button2.Size = new Size(179, 23);
            button2.TabIndex = 33;
            button2.Text = "Import from Classname List";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(60, 63, 65);
            button3.Location = new Point(320, 34);
            button3.Name = "button3";
            button3.Size = new Size(179, 23);
            button3.TabIndex = 34;
            button3.Text = "Import List from ClipBoard";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button5
            // 
            button5.BackColor = Color.FromArgb(60, 63, 65);
            button5.DialogResult = DialogResult.OK;
            button5.Location = new Point(927, 34);
            button5.Name = "button5";
            button5.Size = new Size(179, 79);
            button5.TabIndex = 35;
            button5.Text = "Import to Economy";
            button5.UseVisualStyleBackColor = false;
            // 
            // AddTypes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(1130, 562);
            Controls.Add(button5);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button4);
            Controls.Add(textBox2);
            Controls.Add(label3);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(SelectProjectFolderbutton);
            Controls.Add(ResizePanel);
            Controls.Add(_grid);
            Controls.Add(TitlePanel);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            Name = "AddTypes";
            Text = "AddTypes";
            Load += AddTypes_Load;
            TitlePanel.ResumeLayout(false);
            TitlePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel TitlePanel;
        private Button button1;
        private Button MinimiseButton;
        private Button CloseButton;
        private Label TitleLabel;
        private Label label1;
        private DataGridView _grid;
        private Panel ResizePanel;
        private DataGridViewTextBoxColumn NameGridColumn;
        private DataGridViewTextBoxColumn nominal;
        private DataGridViewTextBoxColumn lifetime;
        private DataGridViewTextBoxColumn restock;
        private DataGridViewTextBoxColumn min;
        private DataGridViewTextBoxColumn quantmin;
        private DataGridViewTextBoxColumn quantmax;
        private DataGridViewTextBoxColumn cost;
        private DataGridViewTextBoxColumn Flags;
        private DataGridViewTextBoxColumn Category;
        private DataGridViewTextBoxColumn Tags;
        private DataGridViewTextBoxColumn Usages;
        private DataGridViewTextBoxColumn Values;
        private Button SelectProjectFolderbutton;
        private Label label2;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label3;
        private Button button4;
        private Button button2;
        private Button button3;
        private Button button5;
    }
}