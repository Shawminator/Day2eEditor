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
            TitlePanel = new Panel();
            button2 = new Button();
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
            FlagsColumn = new DataGridViewTextBoxColumn();
            CategoryGridColumn = new DataGridViewTextBoxColumn();
            Tags = new DataGridViewTextBoxColumn();
            Usage = new DataGridViewTextBoxColumn();
            Tiers = new DataGridViewTextBoxColumn();
            button4 = new Button();
            TitlePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
            SuspendLayout();
            // 
            // TitlePanel
            // 
            TitlePanel.BackColor = Color.Black;
            TitlePanel.Controls.Add(button2);
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
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button2.BackColor = Color.Black;
            button2.DialogResult = DialogResult.Cancel;
            button2.FlatStyle = FlatStyle.Popup;
            button2.Font = new Font("Segoe UI", 12F);
            button2.ForeColor = Color.DarkRed;
            button2.Location = new Point(1086, 0);
            button2.Name = "button2";
            button2.Size = new Size(41, 28);
            button2.TabIndex = 10;
            button2.Text = "X";
            button2.UseVisualStyleBackColor = false;
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
            CloseButton.Location = new Point(2435, 0);
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
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(75, 110, 175);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(61, 15);
            label1.TabIndex = 8;
            label1.Text = "Add Types";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _grid
            // 
            _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            _grid.BackgroundColor = Color.FromArgb(60, 63, 65);
            _grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _grid.Columns.AddRange(new DataGridViewColumn[] { NameGridColumn, nominal, lifetime, restock, min, quantmin, quantmax, cost, FlagsColumn, CategoryGridColumn, Tags, Usage, Tiers });
            _grid.GridColor = SystemColors.ActiveCaptionText;
            _grid.Location = new Point(12, 63);
            _grid.Name = "_grid";
            _grid.Size = new Size(1106, 487);
            _grid.TabIndex = 7;
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
            quantmax.Width = 86;
            // 
            // cost
            // 
            cost.HeaderText = "cost";
            cost.Name = "cost";
            cost.Width = 54;
            // 
            // FlagsColumn
            // 
            FlagsColumn.HeaderText = "Flags";
            FlagsColumn.Name = "FlagsColumn";
            FlagsColumn.Resizable = DataGridViewTriState.True;
            FlagsColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            FlagsColumn.Width = 40;
            // 
            // CategoryGridColumn
            // 
            CategoryGridColumn.HeaderText = "Category";
            CategoryGridColumn.Name = "CategoryGridColumn";
            CategoryGridColumn.Resizable = DataGridViewTriState.True;
            CategoryGridColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            CategoryGridColumn.Width = 61;
            // 
            // Tags
            // 
            Tags.HeaderText = "TagColumn";
            Tags.Name = "Tags";
            Tags.Width = 93;
            // 
            // Usage
            // 
            Usage.HeaderText = "Usage";
            Usage.Name = "Usage";
            Usage.Width = 64;
            // 
            // Tiers
            // 
            Tiers.HeaderText = "TiersColumn";
            Tiers.Name = "Tiers";
            Tiers.Width = 99;
            // 
            // button4
            // 
            button4.BackColor = Color.FromArgb(60, 63, 65);
            button4.Location = new Point(12, 34);
            button4.Name = "button4";
            button4.Size = new Size(152, 23);
            button4.TabIndex = 25;
            button4.Text = "Remove Selected";
            button4.UseVisualStyleBackColor = false;
            // 
            // AddTypes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(1130, 562);
            Controls.Add(button4);
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
        }

        #endregion

        private Panel TitlePanel;
        private Button button1;
        private Button MinimiseButton;
        private Button CloseButton;
        private Label TitleLabel;
        private Label label1;
        private DataGridView _grid;
        private Button button2;
        private Button button4;
        private DataGridViewTextBoxColumn NameGridColumn;
        private DataGridViewTextBoxColumn nominal;
        private DataGridViewTextBoxColumn lifetime;
        private DataGridViewTextBoxColumn restock;
        private DataGridViewTextBoxColumn min;
        private DataGridViewTextBoxColumn quantmin;
        private DataGridViewTextBoxColumn quantmax;
        private DataGridViewTextBoxColumn cost;
        private DataGridViewTextBoxColumn FlagsColumn;
        private DataGridViewTextBoxColumn CategoryGridColumn;
        private DataGridViewTextBoxColumn Tags;
        private DataGridViewTextBoxColumn Usage;
        private DataGridViewTextBoxColumn Tiers;
    }
}