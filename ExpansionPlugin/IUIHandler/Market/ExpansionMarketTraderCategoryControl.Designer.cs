namespace ExpansionPlugin
{
    partial class ExpansionMarketTraderCategoryControl
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExpansionMarketTraderCategoryControl));
            groupBox1 = new GroupBox();
            TradercheckPanel = new Panel();
            label1 = new Label();
            ExpansionMarketTraderBuySellCB = new ComboBox();
            darkLabel141 = new Label();
            darkLabel57 = new Label();
            textBox2 = new TextBox();
            imageList1 = new ImageList(components);
            ttpShow = new ToolTip(components);
            darkButton26 = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(darkButton26);
            groupBox1.Controls.Add(TradercheckPanel);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(ExpansionMarketTraderBuySellCB);
            groupBox1.Controls.Add(darkLabel141);
            groupBox1.Controls.Add(darkLabel57);
            groupBox1.Controls.Add(textBox2);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(561, 115);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Expansion Market Trader Category";
            // 
            // TradercheckPanel
            // 
            TradercheckPanel.BackgroundImageLayout = ImageLayout.Stretch;
            TradercheckPanel.Location = new Point(164, 20);
            TradercheckPanel.Margin = new Padding(4, 3, 4, 3);
            TradercheckPanel.Name = "TradercheckPanel";
            TradercheckPanel.Size = new Size(25, 25);
            TradercheckPanel.TabIndex = 197;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(220, 220, 220);
            label1.Location = new Point(14, 25);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(134, 15);
            label1.TabIndex = 196;
            label1.Text = "Is Valid Market Category";
            // 
            // ExpansionMarketTraderBuySellCB
            // 
            ExpansionMarketTraderBuySellCB.BackColor = Color.FromArgb(60, 63, 65);
            ExpansionMarketTraderBuySellCB.ForeColor = SystemColors.Control;
            ExpansionMarketTraderBuySellCB.FormattingEnabled = true;
            ExpansionMarketTraderBuySellCB.Location = new Point(164, 82);
            ExpansionMarketTraderBuySellCB.Margin = new Padding(4, 3, 4, 3);
            ExpansionMarketTraderBuySellCB.Name = "ExpansionMarketTraderBuySellCB";
            ExpansionMarketTraderBuySellCB.Size = new Size(390, 23);
            ExpansionMarketTraderBuySellCB.TabIndex = 193;
            ExpansionMarketTraderBuySellCB.SelectedIndexChanged += ExpansionMarketTraderBuySellCB_SelectedIndexChanged;
            // 
            // darkLabel141
            // 
            darkLabel141.AutoSize = true;
            darkLabel141.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel141.Location = new Point(14, 85);
            darkLabel141.Margin = new Padding(4, 0, 4, 0);
            darkLabel141.Name = "darkLabel141";
            darkLabel141.Size = new Size(84, 15);
            darkLabel141.TabIndex = 195;
            darkLabel141.Text = "Trader Buy Sell";
            // 
            // darkLabel57
            // 
            darkLabel57.AutoSize = true;
            darkLabel57.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel57.Location = new Point(14, 55);
            darkLabel57.Margin = new Padding(4, 0, 4, 0);
            darkLabel57.Name = "darkLabel57";
            darkLabel57.Size = new Size(104, 15);
            darkLabel57.TabIndex = 194;
            darkLabel57.Text = "Full Category Path";
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(60, 63, 65);
            textBox2.ForeColor = SystemColors.Control;
            textBox2.Location = new Point(164, 52);
            textBox2.Margin = new Padding(4, 3, 4, 3);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(390, 23);
            textBox2.TabIndex = 192;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "tick1.png");
            imageList1.Images.SetKeyName(1, "cross.png");
            // 
            // darkButton26
            // 
            darkButton26.FlatStyle = FlatStyle.Flat;
            darkButton26.Location = new Point(435, 21);
            darkButton26.Margin = new Padding(4, 3, 4, 3);
            darkButton26.Name = "darkButton26";
            darkButton26.Size = new Size(119, 23);
            darkButton26.TabIndex = 198;
            darkButton26.Text = "Switch Category";
            darkButton26.Click += darkButton26_Click;
            // 
            // ExpansionMarketTraderCategoryControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionMarketTraderCategoryControl";
            Size = new Size(561, 115);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private ComboBox ExpansionMarketTraderBuySellCB;
        private Label darkLabel141;
        private Label darkLabel57;
        private TextBox textBox2;
        private Label label1;
        private Panel TradercheckPanel;
        private ImageList imageList1;
        private ToolTip ttpShow;
        private Button darkButton26;
    }
}
