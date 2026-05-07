namespace ExpansionPlugin
{
    partial class ExpansionMarketTraderZoneControl
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
            trackBar2 = new TrackBar();
            trackBar1 = new TrackBar();
            Label4 = new Label();
            SellPricePercentNUD = new NumericUpDown();
            Label3 = new Label();
            BuyPricePercentNUD = new NumericUpDown();
            Label1 = new Label();
            FilenameTB = new TextBox();
            Label2 = new Label();
            m_DisplayNameTB = new TextBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SellPricePercentNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BuyPricePercentNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(trackBar2);
            groupBox1.Controls.Add(trackBar1);
            groupBox1.Controls.Add(Label4);
            groupBox1.Controls.Add(SellPricePercentNUD);
            groupBox1.Controls.Add(Label3);
            groupBox1.Controls.Add(BuyPricePercentNUD);
            groupBox1.Controls.Add(Label1);
            groupBox1.Controls.Add(FilenameTB);
            groupBox1.Controls.Add(Label2);
            groupBox1.Controls.Add(m_DisplayNameTB);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(635, 170);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Zone Info";
            // 
            // trackBar2
            // 
            trackBar2.LargeChange = 10;
            trackBar2.Location = new Point(197, 115);
            trackBar2.Maximum = 200;
            trackBar2.Minimum = -200;
            trackBar2.Name = "trackBar2";
            trackBar2.Size = new Size(350, 45);
            trackBar2.TabIndex = 192;
            trackBar2.TickStyle = TickStyle.None;
            trackBar2.Value = 10;
            trackBar2.Scroll += trackBar2_Scroll;
            // 
            // trackBar1
            // 
            trackBar1.LargeChange = 10;
            trackBar1.Location = new Point(197, 83);
            trackBar1.Maximum = 200;
            trackBar1.Minimum = -200;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(350, 45);
            trackBar1.TabIndex = 191;
            trackBar1.TickStyle = TickStyle.None;
            trackBar1.Value = 10;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // Label4
            // 
            Label4.AutoSize = true;
            Label4.ForeColor = Color.FromArgb(220, 220, 220);
            Label4.Location = new Point(15, 115);
            Label4.Margin = new Padding(4, 0, 4, 0);
            Label4.Name = "Label4";
            Label4.Size = new Size(97, 15);
            Label4.TabIndex = 190;
            Label4.Text = "Sell Price Percent";
            // 
            // SellPricePercentNUD
            // 
            SellPricePercentNUD.BackColor = Color.FromArgb(60, 63, 65);
            SellPricePercentNUD.DecimalPlaces = 2;
            SellPricePercentNUD.ForeColor = SystemColors.Control;
            SellPricePercentNUD.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            SellPricePercentNUD.Location = new Point(554, 113);
            SellPricePercentNUD.Margin = new Padding(4, 3, 4, 3);
            SellPricePercentNUD.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            SellPricePercentNUD.Minimum = new decimal(new int[] { 200, 0, 0, int.MinValue });
            SellPricePercentNUD.Name = "SellPricePercentNUD";
            SellPricePercentNUD.Size = new Size(73, 23);
            SellPricePercentNUD.TabIndex = 179;
            SellPricePercentNUD.TextAlign = HorizontalAlignment.Center;
            SellPricePercentNUD.ValueChanged += SellPricePercentNUD_ValueChanged;
            // 
            // Label3
            // 
            Label3.AutoSize = true;
            Label3.ForeColor = Color.FromArgb(220, 220, 220);
            Label3.Location = new Point(15, 85);
            Label3.Margin = new Padding(4, 0, 4, 0);
            Label3.Name = "Label3";
            Label3.Size = new Size(99, 15);
            Label3.TabIndex = 189;
            Label3.Text = "Buy Price Percent";
            // 
            // BuyPricePercentNUD
            // 
            BuyPricePercentNUD.BackColor = Color.FromArgb(60, 63, 65);
            BuyPricePercentNUD.DecimalPlaces = 2;
            BuyPricePercentNUD.ForeColor = SystemColors.Control;
            BuyPricePercentNUD.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            BuyPricePercentNUD.Location = new Point(554, 83);
            BuyPricePercentNUD.Margin = new Padding(4, 3, 4, 3);
            BuyPricePercentNUD.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            BuyPricePercentNUD.Minimum = new decimal(new int[] { 200, 0, 0, int.MinValue });
            BuyPricePercentNUD.Name = "BuyPricePercentNUD";
            BuyPricePercentNUD.Size = new Size(73, 23);
            BuyPricePercentNUD.TabIndex = 178;
            BuyPricePercentNUD.TextAlign = HorizontalAlignment.Center;
            BuyPricePercentNUD.ValueChanged += BuyPricePercentNUD_ValueChanged;
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.ForeColor = Color.FromArgb(220, 220, 220);
            Label1.Location = new Point(15, 25);
            Label1.Margin = new Padding(4, 0, 4, 0);
            Label1.Name = "Label1";
            Label1.Size = new Size(57, 15);
            Label1.TabIndex = 187;
            Label1.Text = "FileName";
            // 
            // FilenameTB
            // 
            FilenameTB.BackColor = Color.FromArgb(60, 63, 65);
            FilenameTB.ForeColor = SystemColors.Control;
            FilenameTB.Location = new Point(197, 22);
            FilenameTB.Margin = new Padding(4, 3, 4, 3);
            FilenameTB.Name = "FilenameTB";
            FilenameTB.Size = new Size(430, 23);
            FilenameTB.TabIndex = 176;
            FilenameTB.TextChanged += FilenameTB_TextChanged;
            // 
            // Label2
            // 
            Label2.AutoSize = true;
            Label2.ForeColor = Color.FromArgb(220, 220, 220);
            Label2.Location = new Point(15, 55);
            Label2.Margin = new Padding(4, 0, 4, 0);
            Label2.Name = "Label2";
            Label2.Size = new Size(80, 15);
            Label2.TabIndex = 186;
            Label2.Text = "Display Name";
            // 
            // m_DisplayNameTB
            // 
            m_DisplayNameTB.BackColor = Color.FromArgb(60, 63, 65);
            m_DisplayNameTB.ForeColor = SystemColors.Control;
            m_DisplayNameTB.Location = new Point(197, 52);
            m_DisplayNameTB.Margin = new Padding(4, 3, 4, 3);
            m_DisplayNameTB.Name = "m_DisplayNameTB";
            m_DisplayNameTB.Size = new Size(430, 23);
            m_DisplayNameTB.TabIndex = 177;
            m_DisplayNameTB.TextChanged += m_DisplayNameTB_TextChanged;
            // 
            // ExpansionMarketTraderZoneControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionMarketTraderZoneControl";
            Size = new Size(639, 173);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar2).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ((System.ComponentModel.ISupportInitialize)SellPricePercentNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)BuyPricePercentNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label darkLabel70;
        private Label Label1;
        private TextBox FilenameTB;
        private Label Label2;
        private TextBox m_DisplayNameTB;
        private Label Label3;
        private NumericUpDown BuyPricePercentNUD;
        private Label Label4;
        private NumericUpDown SellPricePercentNUD;
        private TrackBar trackBar1;
        private TrackBar trackBar2;
    }
}
