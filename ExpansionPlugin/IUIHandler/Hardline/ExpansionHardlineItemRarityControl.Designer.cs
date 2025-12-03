namespace ExpansionPlugin
{
    partial class ExpansionHardlineItemRarityControl
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
            groupBox75 = new GroupBox();
            darkButton70 = new Button();
            darkButton71 = new Button();
            darkLabel237 = new Label();
            ItemRarityLB = new ListBox();
            ItemRarityCB = new ComboBox();
            ItemRequirementNUD = new NumericUpDown();
            darkLabel1 = new Label();
            groupBox75.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ItemRequirementNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox75
            // 
            groupBox75.Controls.Add(darkButton70);
            groupBox75.Controls.Add(darkButton71);
            groupBox75.Controls.Add(darkLabel237);
            groupBox75.Controls.Add(ItemRarityLB);
            groupBox75.Controls.Add(ItemRarityCB);
            groupBox75.Controls.Add(ItemRequirementNUD);
            groupBox75.Controls.Add(darkLabel1);
            groupBox75.ForeColor = SystemColors.Control;
            groupBox75.Location = new Point(0, 0);
            groupBox75.Margin = new Padding(4, 3, 4, 3);
            groupBox75.Name = "groupBox75";
            groupBox75.Padding = new Padding(4, 3, 4, 3);
            groupBox75.Size = new Size(296, 615);
            groupBox75.TabIndex = 23;
            groupBox75.TabStop = false;
            groupBox75.Text = "Requirments and Items";
            // 
            // darkButton70
            // 
            darkButton70.FlatStyle = FlatStyle.Flat;
            darkButton70.Location = new Point(147, 577);
            darkButton70.Margin = new Padding(4, 3, 4, 3);
            darkButton70.Name = "darkButton70";
            darkButton70.Size = new Size(138, 27);
            darkButton70.TabIndex = 7;
            darkButton70.Text = "Remove";
            darkButton70.Click += darkButton70_Click;
            // 
            // darkButton71
            // 
            darkButton71.FlatStyle = FlatStyle.Flat;
            darkButton71.Location = new Point(9, 577);
            darkButton71.Margin = new Padding(4, 3, 4, 3);
            darkButton71.Name = "darkButton71";
            darkButton71.Size = new Size(130, 27);
            darkButton71.TabIndex = 5;
            darkButton71.Text = "Add New";
            darkButton71.Click += darkButton71_Click;
            // 
            // darkLabel237
            // 
            darkLabel237.AutoSize = true;
            darkLabel237.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel237.Location = new Point(19, 58);
            darkLabel237.Margin = new Padding(4, 0, 4, 0);
            darkLabel237.Name = "darkLabel237";
            darkLabel237.Size = new Size(75, 15);
            darkLabel237.TabIndex = 2;
            darkLabel237.Tag = "";
            darkLabel237.Text = "Requirement";
            darkLabel237.Visible = false;
            // 
            // ItemRarityLB
            // 
            ItemRarityLB.BackColor = Color.FromArgb(60, 63, 65);
            ItemRarityLB.DrawMode = DrawMode.OwnerDrawFixed;
            ItemRarityLB.ForeColor = SystemColors.Control;
            ItemRarityLB.FormattingEnabled = true;
            ItemRarityLB.Location = new Point(9, 85);
            ItemRarityLB.Margin = new Padding(4, 3, 4, 3);
            ItemRarityLB.Name = "ItemRarityLB";
            ItemRarityLB.Size = new Size(275, 484);
            ItemRarityLB.TabIndex = 4;
            ItemRarityLB.DrawItem += listBox_DrawItem;
            // 
            // ItemRarityCB
            // 
            ItemRarityCB.FormattingEnabled = true;
            ItemRarityCB.Location = new Point(85, 24);
            ItemRarityCB.Margin = new Padding(4, 3, 4, 3);
            ItemRarityCB.Name = "ItemRarityCB";
            ItemRarityCB.Size = new Size(199, 23);
            ItemRarityCB.TabIndex = 1;
            ItemRarityCB.SelectedIndexChanged += ItemRarityCB_SelectedIndexChanged;
            // 
            // ItemRequirementNUD
            // 
            ItemRequirementNUD.BackColor = Color.FromArgb(60, 63, 65);
            ItemRequirementNUD.ForeColor = SystemColors.Control;
            ItemRequirementNUD.Location = new Point(167, 55);
            ItemRequirementNUD.Margin = new Padding(4, 3, 4, 3);
            ItemRequirementNUD.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            ItemRequirementNUD.Minimum = new decimal(new int[] { 999999999, 0, 0, int.MinValue });
            ItemRequirementNUD.Name = "ItemRequirementNUD";
            ItemRequirementNUD.Size = new Size(118, 23);
            ItemRequirementNUD.TabIndex = 3;
            ItemRequirementNUD.Tag = "Weight";
            ItemRequirementNUD.TextAlign = HorizontalAlignment.Center;
            ItemRequirementNUD.Visible = false;
            // 
            // darkLabel1
            // 
            darkLabel1.AutoSize = true;
            darkLabel1.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel1.Location = new Point(19, 28);
            darkLabel1.Margin = new Padding(4, 0, 4, 0);
            darkLabel1.Name = "darkLabel1";
            darkLabel1.Size = new Size(34, 15);
            darkLabel1.TabIndex = 0;
            darkLabel1.Tag = "";
            darkLabel1.Text = "Level";
            // 
            // ExpansionHardlineItemRarityControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox75);
            ForeColor = SystemColors.Control;
            Name = "ExpansionHardlineItemRarityControl";
            Size = new Size(296, 615);
            groupBox75.ResumeLayout(false);
            groupBox75.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ItemRequirementNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox75;
        private Button darkButton70;
        private Button darkButton71;
        private Label darkLabel237;
        private ListBox ItemRarityLB;
        private ComboBox ItemRarityCB;
        private NumericUpDown ItemRequirementNUD;
        private Label darkLabel1;
    }
}
