namespace EconomyPlugin
{
    partial class ExpansionLootControl
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
            ExpansionLootTV = new Day2eEditor.MultiSelectTreeView();
            expansionLootVarientGB = new GroupBox();
            ExpansionLootitemSetAllRandomChanceButton = new Button();
            ExpansionLootitemSetAllChanceButton = new Button();
            darkLabel1 = new Label();
            darkLabel2 = new Label();
            trackBar2 = new TrackBar();
            expansionLootItemGB = new GroupBox();
            numericUpDown31 = new NumericUpDown();
            darkLabel23 = new Label();
            darkLabel22 = new Label();
            numericUpDown33 = new NumericUpDown();
            trackBar1 = new TrackBar();
            darkLabel220 = new Label();
            darkLabel12 = new Label();
            numericUpDown12 = new NumericUpDown();
            darkLabel251 = new Label();
            groupBox1.SuspendLayout();
            expansionLootVarientGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar2).BeginInit();
            expansionLootItemGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown31).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown33).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown12).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ExpansionLootTV);
            groupBox1.Controls.Add(expansionLootItemGB);
            groupBox1.Controls.Add(expansionLootVarientGB);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(654, 683);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Expansion Loot";
            // 
            // ExpansionLootTV
            // 
            ExpansionLootTV.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            ExpansionLootTV.BackColor = Color.FromArgb(60, 63, 65);
            ExpansionLootTV.ForeColor = SystemColors.Control;
            ExpansionLootTV.LineColor = Color.FromArgb(240, 240, 240);
            ExpansionLootTV.Location = new Point(8, 22);
            ExpansionLootTV.Margin = new Padding(4, 3, 4, 3);
            ExpansionLootTV.Name = "ExpansionLootTV";
            ExpansionLootTV.Size = new Size(308, 655);
            ExpansionLootTV.TabIndex = 214;
            ExpansionLootTV.AfterSelect += ExpansionLootTV_AfterSelect;
            ExpansionLootTV.NodeMouseClick += ExpansionLootTV_NodeMouseClick;
            // 
            // expansionLootVarientGB
            // 
            expansionLootVarientGB.Controls.Add(ExpansionLootitemSetAllRandomChanceButton);
            expansionLootVarientGB.Controls.Add(ExpansionLootitemSetAllChanceButton);
            expansionLootVarientGB.Controls.Add(darkLabel1);
            expansionLootVarientGB.Controls.Add(darkLabel2);
            expansionLootVarientGB.Controls.Add(trackBar2);
            expansionLootVarientGB.ForeColor = SystemColors.Control;
            expansionLootVarientGB.Location = new Point(323, 22);
            expansionLootVarientGB.Margin = new Padding(4, 3, 4, 3);
            expansionLootVarientGB.Name = "expansionLootVarientGB";
            expansionLootVarientGB.Padding = new Padding(4, 3, 4, 3);
            expansionLootVarientGB.Size = new Size(322, 178);
            expansionLootVarientGB.TabIndex = 225;
            expansionLootVarientGB.TabStop = false;
            expansionLootVarientGB.Text = "Expansion Loot Varient";
            expansionLootVarientGB.Visible = false;
            // 
            // ExpansionLootitemSetAllRandomChanceButton
            // 
            ExpansionLootitemSetAllRandomChanceButton.FlatStyle = FlatStyle.Flat;
            ExpansionLootitemSetAllRandomChanceButton.Location = new Point(9, 110);
            ExpansionLootitemSetAllRandomChanceButton.Margin = new Padding(4, 3, 4, 3);
            ExpansionLootitemSetAllRandomChanceButton.Name = "ExpansionLootitemSetAllRandomChanceButton";
            ExpansionLootitemSetAllRandomChanceButton.Size = new Size(304, 27);
            ExpansionLootitemSetAllRandomChanceButton.TabIndex = 225;
            ExpansionLootitemSetAllRandomChanceButton.Text = "Set All Random Chance";
            ExpansionLootitemSetAllRandomChanceButton.Click += ExpansionLootitemSetAllRandomChanceButton_Click;
            // 
            // ExpansionLootitemSetAllChanceButton
            // 
            ExpansionLootitemSetAllChanceButton.FlatStyle = FlatStyle.Flat;
            ExpansionLootitemSetAllChanceButton.Location = new Point(9, 76);
            ExpansionLootitemSetAllChanceButton.Margin = new Padding(4, 3, 4, 3);
            ExpansionLootitemSetAllChanceButton.Name = "ExpansionLootitemSetAllChanceButton";
            ExpansionLootitemSetAllChanceButton.Size = new Size(304, 27);
            ExpansionLootitemSetAllChanceButton.TabIndex = 224;
            ExpansionLootitemSetAllChanceButton.Text = "Set All Selected Chance";
            ExpansionLootitemSetAllChanceButton.Click += ExpansionLootitemSetAllChanceButton_Click;
            // 
            // darkLabel1
            // 
            darkLabel1.AutoSize = true;
            darkLabel1.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel1.Location = new Point(202, 50);
            darkLabel1.Margin = new Padding(4, 0, 4, 0);
            darkLabel1.Name = "darkLabel1";
            darkLabel1.Size = new Size(17, 15);
            darkLabel1.TabIndex = 223;
            darkLabel1.Text = "%";
            // 
            // darkLabel2
            // 
            darkLabel2.AutoSize = true;
            darkLabel2.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel2.Location = new Point(6, 25);
            darkLabel2.Margin = new Padding(4, 0, 4, 0);
            darkLabel2.Name = "darkLabel2";
            darkLabel2.Size = new Size(53, 15);
            darkLabel2.TabIndex = 215;
            darkLabel2.Text = "Chance :";
            // 
            // trackBar2
            // 
            trackBar2.LargeChange = 1;
            trackBar2.Location = new Point(96, 15);
            trackBar2.Margin = new Padding(4, 3, 4, 3);
            trackBar2.Maximum = 100;
            trackBar2.Name = "trackBar2";
            trackBar2.Size = new Size(223, 45);
            trackBar2.TabIndex = 216;
            trackBar2.Scroll += trackBar2_Scroll;
            trackBar2.ValueChanged += trackBar2_ValueChanged;
            trackBar2.MouseUp += trackBar2_MouseUp;
            // 
            // expansionLootItemGB
            // 
            expansionLootItemGB.Controls.Add(numericUpDown31);
            expansionLootItemGB.Controls.Add(darkLabel23);
            expansionLootItemGB.Controls.Add(darkLabel22);
            expansionLootItemGB.Controls.Add(numericUpDown33);
            expansionLootItemGB.Controls.Add(trackBar1);
            expansionLootItemGB.Controls.Add(darkLabel220);
            expansionLootItemGB.Controls.Add(darkLabel12);
            expansionLootItemGB.Controls.Add(numericUpDown12);
            expansionLootItemGB.Controls.Add(darkLabel251);
            expansionLootItemGB.ForeColor = SystemColors.Control;
            expansionLootItemGB.Location = new Point(323, 22);
            expansionLootItemGB.Margin = new Padding(4, 3, 4, 3);
            expansionLootItemGB.Name = "expansionLootItemGB";
            expansionLootItemGB.Padding = new Padding(4, 3, 4, 3);
            expansionLootItemGB.Size = new Size(322, 178);
            expansionLootItemGB.TabIndex = 224;
            expansionLootItemGB.TabStop = false;
            expansionLootItemGB.Text = "Expansion Loot Item";
            expansionLootItemGB.Visible = false;
            // 
            // numericUpDown31
            // 
            numericUpDown31.BackColor = Color.FromArgb(60, 63, 65);
            numericUpDown31.DecimalPlaces = 2;
            numericUpDown31.ForeColor = SystemColors.Control;
            numericUpDown31.Location = new Point(133, 72);
            numericUpDown31.Margin = new Padding(4, 3, 4, 3);
            numericUpDown31.Minimum = new decimal(new int[] { 2, 0, 0, int.MinValue });
            numericUpDown31.Name = "numericUpDown31";
            numericUpDown31.Size = new Size(173, 23);
            numericUpDown31.TabIndex = 220;
            numericUpDown31.TextAlign = HorizontalAlignment.Center;
            numericUpDown31.ValueChanged += numericUpDown31_ValueChanged;
            // 
            // darkLabel23
            // 
            darkLabel23.AutoSize = true;
            darkLabel23.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel23.Location = new Point(202, 50);
            darkLabel23.Margin = new Padding(4, 0, 4, 0);
            darkLabel23.Name = "darkLabel23";
            darkLabel23.Size = new Size(17, 15);
            darkLabel23.TabIndex = 223;
            darkLabel23.Text = "%";
            // 
            // darkLabel22
            // 
            darkLabel22.AutoSize = true;
            darkLabel22.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel22.Location = new Point(6, 25);
            darkLabel22.Margin = new Padding(4, 0, 4, 0);
            darkLabel22.Name = "darkLabel22";
            darkLabel22.Size = new Size(53, 15);
            darkLabel22.TabIndex = 215;
            darkLabel22.Text = "Chance :";
            // 
            // numericUpDown33
            // 
            numericUpDown33.BackColor = Color.FromArgb(60, 63, 65);
            numericUpDown33.ForeColor = SystemColors.Control;
            numericUpDown33.Location = new Point(133, 129);
            numericUpDown33.Margin = new Padding(4, 3, 4, 3);
            numericUpDown33.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            numericUpDown33.Name = "numericUpDown33";
            numericUpDown33.Size = new Size(172, 23);
            numericUpDown33.TabIndex = 222;
            numericUpDown33.TextAlign = HorizontalAlignment.Center;
            numericUpDown33.ValueChanged += numericUpDown33_ValueChanged;
            // 
            // trackBar1
            // 
            trackBar1.LargeChange = 1;
            trackBar1.Location = new Point(96, 15);
            trackBar1.Margin = new Padding(4, 3, 4, 3);
            trackBar1.Maximum = 100;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(223, 45);
            trackBar1.TabIndex = 216;
            trackBar1.Scroll += trackBar1_Scroll;
            trackBar1.ValueChanged += trackBar1_ValueChanged;
            trackBar1.MouseUp += trackBar1_MouseUp;
            // 
            // darkLabel220
            // 
            darkLabel220.AutoSize = true;
            darkLabel220.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel220.Location = new Point(6, 132);
            darkLabel220.Margin = new Padding(4, 0, 4, 0);
            darkLabel220.Name = "darkLabel220";
            darkLabel220.Size = new Size(64, 15);
            darkLabel220.TabIndex = 221;
            darkLabel220.Text = "Min Count";
            // 
            // darkLabel12
            // 
            darkLabel12.AutoSize = true;
            darkLabel12.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel12.Location = new Point(6, 102);
            darkLabel12.Margin = new Padding(4, 0, 4, 0);
            darkLabel12.Name = "darkLabel12";
            darkLabel12.Size = new Size(66, 15);
            darkLabel12.TabIndex = 217;
            darkLabel12.Text = "Max Count";
            // 
            // numericUpDown12
            // 
            numericUpDown12.BackColor = Color.FromArgb(60, 63, 65);
            numericUpDown12.ForeColor = SystemColors.Control;
            numericUpDown12.Location = new Point(133, 99);
            numericUpDown12.Margin = new Padding(4, 3, 4, 3);
            numericUpDown12.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            numericUpDown12.Name = "numericUpDown12";
            numericUpDown12.Size = new Size(173, 23);
            numericUpDown12.TabIndex = 218;
            numericUpDown12.TextAlign = HorizontalAlignment.Center;
            numericUpDown12.ValueChanged += numericUpDown12_ValueChanged;
            // 
            // darkLabel251
            // 
            darkLabel251.AutoSize = true;
            darkLabel251.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel251.Location = new Point(6, 74);
            darkLabel251.Margin = new Padding(4, 0, 4, 0);
            darkLabel251.Name = "darkLabel251";
            darkLabel251.Size = new Size(96, 15);
            darkLabel251.TabIndex = 219;
            darkLabel251.Text = "Quantity Percent";
            // 
            // ExpansionLootControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionLootControl";
            Size = new Size(654, 683);
            groupBox1.ResumeLayout(false);
            expansionLootVarientGB.ResumeLayout(false);
            expansionLootVarientGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar2).EndInit();
            expansionLootItemGB.ResumeLayout(false);
            expansionLootItemGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown31).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown33).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown12).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Day2eEditor.MultiSelectTreeView ExpansionLootTV;
        private GroupBox expansionLootItemGB;
        private NumericUpDown numericUpDown31;
        private Label darkLabel23;
        private Label darkLabel22;
        private NumericUpDown numericUpDown33;
        private TrackBar trackBar1;
        private Label darkLabel220;
        private Label darkLabel12;
        private NumericUpDown numericUpDown12;
        private Label darkLabel251;
        private GroupBox expansionLootVarientGB;
        private Button ExpansionLootitemSetAllRandomChanceButton;
        private Button ExpansionLootitemSetAllChanceButton;
        private Label darkLabel1;
        private Label darkLabel2;
        private TrackBar trackBar2;
    }
}
