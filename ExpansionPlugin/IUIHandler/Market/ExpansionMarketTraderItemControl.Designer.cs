namespace ExpansionPlugin
{
    partial class ExpansionMarketTraderItemControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExpansionMarketTraderItemControl));
            groupBox1 = new GroupBox();
            ExpansionMarketTraderBuySellCB = new ComboBox();
            darkLabel141 = new Label();
            imageList1 = new ImageList(components);
            ttpShow = new ToolTip(components);
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ExpansionMarketTraderBuySellCB);
            groupBox1.Controls.Add(darkLabel141);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(561, 58);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Expansion Market Trader Item";
            // 
            // ExpansionMarketTraderBuySellCB
            // 
            ExpansionMarketTraderBuySellCB.BackColor = Color.FromArgb(60, 63, 65);
            ExpansionMarketTraderBuySellCB.ForeColor = SystemColors.Control;
            ExpansionMarketTraderBuySellCB.FormattingEnabled = true;
            ExpansionMarketTraderBuySellCB.Location = new Point(164, 22);
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
            darkLabel141.Location = new Point(14, 25);
            darkLabel141.Margin = new Padding(4, 0, 4, 0);
            darkLabel141.Name = "darkLabel141";
            darkLabel141.Size = new Size(84, 15);
            darkLabel141.TabIndex = 195;
            darkLabel141.Text = "Trader Buy Sell";
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "tick1.png");
            imageList1.Images.SetKeyName(1, "cross.png");
            // 
            // ExpansionMarketTraderItemControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionMarketTraderItemControl";
            Size = new Size(561, 58);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private ComboBox ExpansionMarketTraderBuySellCB;
        private Label darkLabel141;
        private ImageList imageList1;
        private ToolTip ttpShow;
    }
}
