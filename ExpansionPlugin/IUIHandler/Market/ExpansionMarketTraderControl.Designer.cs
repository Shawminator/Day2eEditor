namespace ExpansionPlugin
{
    partial class ExpansionMarketTraderControl
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
            label1 = new Label();
            UseCategoryOrderCB = new CheckBox();
            darkLabel80 = new Label();
            RequiredCompletedQuestIDNUD = new NumericUpDown();
            RequiredFactionLB = new ComboBox();
            darkLabel141 = new Label();
            darkLabel79 = new Label();
            MaxRequiredHumanityNUD = new NumericUpDown();
            darkLabel78 = new Label();
            MinRequiredHumanityNUD = new NumericUpDown();
            darkLabel70 = new Label();
            darkLabel57 = new Label();
            textBox2 = new TextBox();
            darkLabel50 = new Label();
            textBox5 = new TextBox();
            TraderIconCB = new ComboBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)RequiredCompletedQuestIDNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MaxRequiredHumanityNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MinRequiredHumanityNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(TraderIconCB);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(UseCategoryOrderCB);
            groupBox1.Controls.Add(darkLabel80);
            groupBox1.Controls.Add(RequiredCompletedQuestIDNUD);
            groupBox1.Controls.Add(RequiredFactionLB);
            groupBox1.Controls.Add(darkLabel141);
            groupBox1.Controls.Add(darkLabel79);
            groupBox1.Controls.Add(MaxRequiredHumanityNUD);
            groupBox1.Controls.Add(darkLabel78);
            groupBox1.Controls.Add(MinRequiredHumanityNUD);
            groupBox1.Controls.Add(darkLabel70);
            groupBox1.Controls.Add(darkLabel57);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(darkLabel50);
            groupBox1.Controls.Add(textBox5);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(464, 261);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Market Category Info";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(220, 220, 220);
            label1.Location = new Point(14, 235);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(110, 15);
            label1.TabIndex = 194;
            label1.Text = "Use Category Order";
            // 
            // UseCategoryOrderCB
            // 
            UseCategoryOrderCB.AutoSize = true;
            UseCategoryOrderCB.ForeColor = SystemColors.Control;
            UseCategoryOrderCB.Location = new Point(197, 236);
            UseCategoryOrderCB.Margin = new Padding(4, 3, 4, 3);
            UseCategoryOrderCB.Name = "UseCategoryOrderCB";
            UseCategoryOrderCB.RightToLeft = RightToLeft.Yes;
            UseCategoryOrderCB.Size = new Size(15, 14);
            UseCategoryOrderCB.TabIndex = 193;
            UseCategoryOrderCB.UseVisualStyleBackColor = true;
            UseCategoryOrderCB.CheckedChanged += UseCategoryOrderCB_CheckedChanged;
            // 
            // darkLabel80
            // 
            darkLabel80.AutoSize = true;
            darkLabel80.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel80.Location = new Point(14, 175);
            darkLabel80.Margin = new Padding(4, 0, 4, 0);
            darkLabel80.Name = "darkLabel80";
            darkLabel80.Size = new Size(161, 15);
            darkLabel80.TabIndex = 192;
            darkLabel80.Text = "Required Completed QuestID";
            // 
            // RequiredCompletedQuestIDNUD
            // 
            RequiredCompletedQuestIDNUD.BackColor = Color.FromArgb(60, 63, 65);
            RequiredCompletedQuestIDNUD.ForeColor = SystemColors.Control;
            RequiredCompletedQuestIDNUD.Location = new Point(197, 173);
            RequiredCompletedQuestIDNUD.Margin = new Padding(4, 3, 4, 3);
            RequiredCompletedQuestIDNUD.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            RequiredCompletedQuestIDNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            RequiredCompletedQuestIDNUD.Name = "RequiredCompletedQuestIDNUD";
            RequiredCompletedQuestIDNUD.Size = new Size(256, 23);
            RequiredCompletedQuestIDNUD.TabIndex = 181;
            RequiredCompletedQuestIDNUD.TextAlign = HorizontalAlignment.Center;
            RequiredCompletedQuestIDNUD.ValueChanged += RequiredCompletedQuestIDNUD_ValueChanged;
            // 
            // RequiredFactionLB
            // 
            RequiredFactionLB.BackColor = Color.FromArgb(60, 63, 65);
            RequiredFactionLB.ForeColor = SystemColors.Control;
            RequiredFactionLB.FormattingEnabled = true;
            RequiredFactionLB.Location = new Point(197, 142);
            RequiredFactionLB.Margin = new Padding(4, 3, 4, 3);
            RequiredFactionLB.Name = "RequiredFactionLB";
            RequiredFactionLB.Size = new Size(256, 23);
            RequiredFactionLB.TabIndex = 180;
            RequiredFactionLB.SelectedIndexChanged += RequiredFactionLB_SelectedIndexChanged;
            // 
            // darkLabel141
            // 
            darkLabel141.AutoSize = true;
            darkLabel141.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel141.Location = new Point(14, 145);
            darkLabel141.Margin = new Padding(4, 0, 4, 0);
            darkLabel141.Name = "darkLabel141";
            darkLabel141.Size = new Size(96, 15);
            darkLabel141.TabIndex = 191;
            darkLabel141.Text = "Required Faction";
            // 
            // darkLabel79
            // 
            darkLabel79.AutoSize = true;
            darkLabel79.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel79.Location = new Point(14, 115);
            darkLabel79.Margin = new Padding(4, 0, 4, 0);
            darkLabel79.Name = "darkLabel79";
            darkLabel79.Size = new Size(102, 15);
            darkLabel79.TabIndex = 190;
            darkLabel79.Text = "Max Required Rep";
            // 
            // MaxRequiredHumanityNUD
            // 
            MaxRequiredHumanityNUD.BackColor = Color.FromArgb(60, 63, 65);
            MaxRequiredHumanityNUD.ForeColor = SystemColors.Control;
            MaxRequiredHumanityNUD.Location = new Point(197, 113);
            MaxRequiredHumanityNUD.Margin = new Padding(4, 3, 4, 3);
            MaxRequiredHumanityNUD.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            MaxRequiredHumanityNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            MaxRequiredHumanityNUD.Name = "MaxRequiredHumanityNUD";
            MaxRequiredHumanityNUD.Size = new Size(256, 23);
            MaxRequiredHumanityNUD.TabIndex = 179;
            MaxRequiredHumanityNUD.TextAlign = HorizontalAlignment.Center;
            MaxRequiredHumanityNUD.ValueChanged += MaxRequiredHumanityNUD_ValueChanged;
            // 
            // darkLabel78
            // 
            darkLabel78.AutoSize = true;
            darkLabel78.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel78.Location = new Point(14, 85);
            darkLabel78.Margin = new Padding(4, 0, 4, 0);
            darkLabel78.Name = "darkLabel78";
            darkLabel78.Size = new Size(101, 15);
            darkLabel78.TabIndex = 189;
            darkLabel78.Text = "Min Required Rep";
            // 
            // MinRequiredHumanityNUD
            // 
            MinRequiredHumanityNUD.BackColor = Color.FromArgb(60, 63, 65);
            MinRequiredHumanityNUD.ForeColor = SystemColors.Control;
            MinRequiredHumanityNUD.Location = new Point(197, 83);
            MinRequiredHumanityNUD.Margin = new Padding(4, 3, 4, 3);
            MinRequiredHumanityNUD.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            MinRequiredHumanityNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            MinRequiredHumanityNUD.Name = "MinRequiredHumanityNUD";
            MinRequiredHumanityNUD.Size = new Size(256, 23);
            MinRequiredHumanityNUD.TabIndex = 178;
            MinRequiredHumanityNUD.TextAlign = HorizontalAlignment.Center;
            MinRequiredHumanityNUD.ValueChanged += MinRequiredHumanityNUD_ValueChanged;
            // 
            // darkLabel70
            // 
            darkLabel70.AutoSize = true;
            darkLabel70.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel70.Location = new Point(14, 205);
            darkLabel70.Margin = new Padding(4, 0, 4, 0);
            darkLabel70.Name = "darkLabel70";
            darkLabel70.Size = new Size(66, 15);
            darkLabel70.TabIndex = 188;
            darkLabel70.Text = "Trader Icon";
            // 
            // darkLabel57
            // 
            darkLabel57.AutoSize = true;
            darkLabel57.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel57.Location = new Point(14, 25);
            darkLabel57.Margin = new Padding(4, 0, 4, 0);
            darkLabel57.Name = "darkLabel57";
            darkLabel57.Size = new Size(57, 15);
            darkLabel57.TabIndex = 187;
            darkLabel57.Text = "FileName";
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(60, 63, 65);
            textBox2.ForeColor = SystemColors.Control;
            textBox2.Location = new Point(197, 22);
            textBox2.Margin = new Padding(4, 3, 4, 3);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(256, 23);
            textBox2.TabIndex = 176;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // darkLabel50
            // 
            darkLabel50.AutoSize = true;
            darkLabel50.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel50.Location = new Point(14, 55);
            darkLabel50.Margin = new Padding(4, 0, 4, 0);
            darkLabel50.Name = "darkLabel50";
            darkLabel50.Size = new Size(80, 15);
            darkLabel50.TabIndex = 186;
            darkLabel50.Text = "Display Name";
            // 
            // textBox5
            // 
            textBox5.BackColor = Color.FromArgb(60, 63, 65);
            textBox5.ForeColor = SystemColors.Control;
            textBox5.Location = new Point(197, 52);
            textBox5.Margin = new Padding(4, 3, 4, 3);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(256, 23);
            textBox5.TabIndex = 177;
            textBox5.TextChanged += textBox5_TextChanged;
            // 
            // TraderIconCB
            // 
            TraderIconCB.BackColor = Color.FromArgb(60, 63, 65);
            TraderIconCB.ForeColor = SystemColors.Control;
            TraderIconCB.FormattingEnabled = true;
            TraderIconCB.Location = new Point(197, 202);
            TraderIconCB.Margin = new Padding(4, 3, 4, 3);
            TraderIconCB.Name = "TraderIconCB";
            TraderIconCB.Size = new Size(256, 23);
            TraderIconCB.TabIndex = 6;
            TraderIconCB.SelectedIndexChanged += TraderIconCB_SelectedIndexChanged;
            // 
            // ExpansionMarketTraderControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionMarketTraderControl";
            Size = new Size(464, 261);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)RequiredCompletedQuestIDNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)MaxRequiredHumanityNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)MinRequiredHumanityNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private CheckBox UseCategoryOrderCB;
        private Label darkLabel80;
        private NumericUpDown RequiredCompletedQuestIDNUD;
        private ComboBox RequiredFactionLB;
        private Label darkLabel141;
        private Label darkLabel79;
        private NumericUpDown MaxRequiredHumanityNUD;
        private Label darkLabel78;
        private NumericUpDown MinRequiredHumanityNUD;
        private Label darkLabel70;
        private Label darkLabel57;
        private TextBox textBox2;
        private Label darkLabel50;
        private TextBox textBox5;
        private Label label1;
        private ComboBox TraderIconCB;
    }
}
