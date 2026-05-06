namespace ExpansionPlugin
{
    partial class ExpansionMarketCategoryControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            IconCB = new ComboBox();
            textBox11 = new TextBox();
            textBox9 = new TextBox();
            darkLabel2 = new Label();
            darkLabel4 = new Label();
            darkLabel13 = new Label();
            IsExchangeCB = new CheckBox();
            InitStockPercentNUD = new NumericUpDown();
            darkLabel76 = new Label();
            ColorPB = new PictureBox();
            IconLabel = new Label();
            darkLabel75 = new Label();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)InitStockPercentNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ColorPB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(pictureBox1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(IconCB);
            groupBox1.Controls.Add(textBox11);
            groupBox1.Controls.Add(textBox9);
            groupBox1.Controls.Add(darkLabel2);
            groupBox1.Controls.Add(darkLabel4);
            groupBox1.Controls.Add(darkLabel13);
            groupBox1.Controls.Add(IsExchangeCB);
            groupBox1.Controls.Add(InitStockPercentNUD);
            groupBox1.Controls.Add(darkLabel76);
            groupBox1.Controls.Add(ColorPB);
            groupBox1.Controls.Add(IconLabel);
            groupBox1.Controls.Add(darkLabel75);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(567, 345);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Market Category Info";
            // 
            // IconCB
            // 
            IconCB.BackColor = Color.FromArgb(60, 63, 65);
            IconCB.ForeColor = SystemColors.Control;
            IconCB.FormattingEnabled = true;
            IconCB.Location = new Point(132, 82);
            IconCB.Margin = new Padding(4, 3, 4, 3);
            IconCB.Name = "IconCB";
            IconCB.Size = new Size(427, 23);
            IconCB.TabIndex = 6;
            IconCB.SelectedIndexChanged += IconCB_SelectedIndexChanged;
            // 
            // textBox11
            // 
            textBox11.BackColor = Color.FromArgb(60, 63, 65);
            textBox11.ForeColor = SystemColors.Control;
            textBox11.Location = new Point(132, 22);
            textBox11.Margin = new Padding(4, 3, 4, 3);
            textBox11.Name = "textBox11";
            textBox11.Size = new Size(428, 23);
            textBox11.TabIndex = 107;
            textBox11.TextChanged += textBox11_TextChanged;
            // 
            // textBox9
            // 
            textBox9.BackColor = Color.FromArgb(60, 63, 65);
            textBox9.ForeColor = SystemColors.Control;
            textBox9.Location = new Point(132, 52);
            textBox9.Margin = new Padding(4, 3, 4, 3);
            textBox9.Name = "textBox9";
            textBox9.Size = new Size(428, 23);
            textBox9.TabIndex = 108;
            textBox9.TextChanged += textBox9_TextChanged;
            // 
            // darkLabel2
            // 
            darkLabel2.AutoSize = true;
            darkLabel2.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel2.Location = new Point(15, 55);
            darkLabel2.Margin = new Padding(4, 0, 4, 0);
            darkLabel2.Name = "darkLabel2";
            darkLabel2.Size = new Size(80, 15);
            darkLabel2.TabIndex = 112;
            darkLabel2.Text = "Display Name";
            // 
            // darkLabel4
            // 
            darkLabel4.AutoSize = true;
            darkLabel4.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel4.Location = new Point(15, 25);
            darkLabel4.Margin = new Padding(4, 0, 4, 0);
            darkLabel4.Name = "darkLabel4";
            darkLabel4.Size = new Size(60, 15);
            darkLabel4.TabIndex = 114;
            darkLabel4.Text = "File Name";
            // 
            // darkLabel13
            // 
            darkLabel13.AutoSize = true;
            darkLabel13.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel13.Location = new Point(15, 285);
            darkLabel13.Margin = new Padding(4, 0, 4, 0);
            darkLabel13.Name = "darkLabel13";
            darkLabel13.Size = new Size(68, 15);
            darkLabel13.TabIndex = 119;
            darkLabel13.Text = "Is Exchange";
            // 
            // IsExchangeCB
            // 
            IsExchangeCB.AutoSize = true;
            IsExchangeCB.ForeColor = SystemColors.Control;
            IsExchangeCB.Location = new Point(131, 285);
            IsExchangeCB.Margin = new Padding(4, 3, 4, 3);
            IsExchangeCB.Name = "IsExchangeCB";
            IsExchangeCB.Size = new Size(15, 14);
            IsExchangeCB.TabIndex = 110;
            IsExchangeCB.UseVisualStyleBackColor = true;
            IsExchangeCB.CheckedChanged += IsExchangeCB_CheckedChanged;
            // 
            // InitStockPercentNUD
            // 
            InitStockPercentNUD.BackColor = Color.FromArgb(60, 63, 65);
            InitStockPercentNUD.DecimalPlaces = 1;
            InitStockPercentNUD.ForeColor = SystemColors.Control;
            InitStockPercentNUD.Location = new Point(132, 313);
            InitStockPercentNUD.Margin = new Padding(4, 3, 4, 3);
            InitStockPercentNUD.Name = "InitStockPercentNUD";
            InitStockPercentNUD.Size = new Size(132, 23);
            InitStockPercentNUD.TabIndex = 111;
            InitStockPercentNUD.TextAlign = HorizontalAlignment.Center;
            InitStockPercentNUD.ValueChanged += InitStockPercentNUD_ValueChanged;
            // 
            // darkLabel76
            // 
            darkLabel76.AutoSize = true;
            darkLabel76.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel76.Location = new Point(15, 315);
            darkLabel76.Margin = new Padding(4, 0, 4, 0);
            darkLabel76.Name = "darkLabel76";
            darkLabel76.Size = new Size(98, 15);
            darkLabel76.TabIndex = 118;
            darkLabel76.Text = "init stock Percent";
            // 
            // ColorPB
            // 
            ColorPB.Location = new Point(132, 115);
            ColorPB.Margin = new Padding(4, 3, 4, 3);
            ColorPB.Name = "ColorPB";
            ColorPB.Size = new Size(257, 18);
            ColorPB.TabIndex = 117;
            ColorPB.TabStop = false;
            ColorPB.Click += CategorycolourPB_Click;
            // 
            // IconLabel
            // 
            IconLabel.AutoSize = true;
            IconLabel.ForeColor = Color.FromArgb(220, 220, 220);
            IconLabel.Location = new Point(15, 85);
            IconLabel.Margin = new Padding(4, 0, 4, 0);
            IconLabel.Name = "IconLabel";
            IconLabel.Size = new Size(30, 15);
            IconLabel.TabIndex = 115;
            IconLabel.Text = "Icon";
            // 
            // darkLabel75
            // 
            darkLabel75.AutoSize = true;
            darkLabel75.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel75.Location = new Point(15, 115);
            darkLabel75.Margin = new Padding(4, 0, 4, 0);
            darkLabel75.Name = "darkLabel75";
            darkLabel75.Size = new Size(43, 15);
            darkLabel75.TabIndex = 116;
            darkLabel75.Text = "Colour";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(60, 63, 65);
            pictureBox1.Location = new Point(132, 145);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(128, 128);
            pictureBox1.TabIndex = 198;
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.FromArgb(220, 220, 220);
            label2.Location = new Point(15, 145);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(74, 15);
            label2.TabIndex = 197;
            label2.Text = "Icon Preview";
            // 
            // ExpansionMarketCategoryControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionMarketCategoryControl";
            Size = new Size(567, 345);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)InitStockPercentNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)ColorPB).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox textBox11;
        private TextBox textBox9;
        private Label darkLabel2;
        private Label darkLabel4;
        private Label darkLabel13;
        private CheckBox IsExchangeCB;
        private NumericUpDown InitStockPercentNUD;
        private Label darkLabel76;
        private TextBox IconTB;
        private PictureBox ColorPB;
        private Label IconLabel;
        private Label darkLabel75;
        private ComboBox IconCB;
        private PictureBox pictureBox1;
        private Label label2;
    }
}
