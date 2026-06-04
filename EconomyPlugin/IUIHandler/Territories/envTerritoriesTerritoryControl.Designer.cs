namespace EconomyPlugin
{
    partial class envTerritoriesTerritoryControl
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
            label3 = new Label();
            TypeTB = new TextBox();
            label2 = new Label();
            nameTB = new TextBox();
            label1 = new Label();
            BehaviorCB = new ComboBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(BehaviorCB);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(TypeTB);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(nameTB);
            groupBox1.Controls.Add(label1);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(571, 131);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Territory Info";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(19, 92);
            label3.Name = "label3";
            label3.Size = new Size(53, 15);
            label3.TabIndex = 4;
            label3.Text = "Behavior";
            // 
            // TypeTB
            // 
            TypeTB.BackColor = Color.FromArgb(60, 63, 65);
            TypeTB.ForeColor = SystemColors.Control;
            TypeTB.Location = new Point(96, 60);
            TypeTB.Name = "TypeTB";
            TypeTB.Size = new Size(453, 23);
            TypeTB.TabIndex = 3;
            TypeTB.TextChanged += TypeTB_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(19, 63);
            label2.Name = "label2";
            label2.Size = new Size(32, 15);
            label2.TabIndex = 2;
            label2.Text = "Type";
            // 
            // nameTB
            // 
            nameTB.BackColor = Color.FromArgb(60, 63, 65);
            nameTB.ForeColor = SystemColors.Control;
            nameTB.Location = new Point(96, 31);
            nameTB.Name = "nameTB";
            nameTB.Size = new Size(453, 23);
            nameTB.TabIndex = 1;
            nameTB.TextChanged += nameTB_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(19, 34);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 0;
            label1.Text = "Name";
            // 
            // BehaviorCB
            // 
            BehaviorCB.BackColor = Color.FromArgb(60, 63, 65);
            BehaviorCB.ForeColor = SystemColors.Control;
            BehaviorCB.FormattingEnabled = true;
            BehaviorCB.Items.AddRange(new object[] { "DZDeerGroupBeh", "DZdomesticGroupBeh", "DZSheepGroupBeh", "DZWolfGroupBeh", "BlissBearGroupBeh", "DZAmbientLifeGroupBeh", "DZdomesticGroupBeh" });
            BehaviorCB.Location = new Point(96, 89);
            BehaviorCB.Margin = new Padding(4, 3, 4, 3);
            BehaviorCB.Name = "BehaviorCB";
            BehaviorCB.Size = new Size(453, 23);
            BehaviorCB.TabIndex = 178;
            BehaviorCB.SelectedIndexChanged += BehaviorCB_SelectedIndexChanged;
            // 
            // envTerritoriesTerritoryControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "envTerritoriesTerritoryControl";
            Size = new Size(580, 145);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label3;
        private TextBox TypeTB;
        private Label label2;
        private TextBox nameTB;
        private Label label1;
        private ComboBox BehaviorCB;
    }
}
