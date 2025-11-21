namespace ExpansionPlugin
{
    partial class ExpansionHardlineReputationControl
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
            groupBox74 = new GroupBox();
            darkButton110 = new Button();
            darkButton111 = new Button();
            EntityReputationNUD = new NumericUpDown();
            darkLabel229 = new Label();
            EntityReputationLB = new ListBox();
            ReputationLossOnDeathNUD = new NumericUpDown();
            darkLabel227 = new Label();
            ReputationMaxReputationNUD = new NumericUpDown();
            darkLabel221 = new Label();
            groupBox74.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)EntityReputationNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ReputationLossOnDeathNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ReputationMaxReputationNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox74
            // 
            groupBox74.Controls.Add(darkButton110);
            groupBox74.Controls.Add(darkButton111);
            groupBox74.Controls.Add(EntityReputationNUD);
            groupBox74.Controls.Add(darkLabel229);
            groupBox74.Controls.Add(EntityReputationLB);
            groupBox74.Controls.Add(ReputationLossOnDeathNUD);
            groupBox74.Controls.Add(darkLabel227);
            groupBox74.Controls.Add(ReputationMaxReputationNUD);
            groupBox74.Controls.Add(darkLabel221);
            groupBox74.ForeColor = SystemColors.Control;
            groupBox74.Location = new Point(0, 0);
            groupBox74.Margin = new Padding(4, 3, 4, 3);
            groupBox74.Name = "groupBox74";
            groupBox74.Padding = new Padding(4, 3, 4, 3);
            groupBox74.Size = new Size(294, 517);
            groupBox74.TabIndex = 22;
            groupBox74.TabStop = false;
            groupBox74.Text = "Reputation";
            // 
            // darkButton110
            // 
            darkButton110.FlatStyle = FlatStyle.Flat;
            darkButton110.Location = new Point(144, 484);
            darkButton110.Margin = new Padding(4, 3, 4, 3);
            darkButton110.Name = "darkButton110";
            darkButton110.Size = new Size(141, 27);
            darkButton110.TabIndex = 9;
            darkButton110.Text = "Remove";
            darkButton110.Click += darkButton110_Click;
            // 
            // darkButton111
            // 
            darkButton111.FlatStyle = FlatStyle.Flat;
            darkButton111.Location = new Point(10, 484);
            darkButton111.Margin = new Padding(4, 3, 4, 3);
            darkButton111.Name = "darkButton111";
            darkButton111.Size = new Size(126, 27);
            darkButton111.TabIndex = 7;
            darkButton111.Text = "Add New";
            darkButton111.Click += darkButton111_Click;
            // 
            // EntityReputationNUD
            // 
            EntityReputationNUD.BackColor = Color.FromArgb(60, 63, 65);
            EntityReputationNUD.ForeColor = SystemColors.Control;
            EntityReputationNUD.Location = new Point(169, 454);
            EntityReputationNUD.Margin = new Padding(4, 3, 4, 3);
            EntityReputationNUD.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            EntityReputationNUD.Minimum = new decimal(new int[] { 999999999, 0, 0, int.MinValue });
            EntityReputationNUD.Name = "EntityReputationNUD";
            EntityReputationNUD.Size = new Size(118, 23);
            EntityReputationNUD.TabIndex = 6;
            EntityReputationNUD.Tag = "Weight";
            EntityReputationNUD.TextAlign = HorizontalAlignment.Center;
            EntityReputationNUD.ValueChanged += EntityReputationNUD_ValueChanged;
            // 
            // darkLabel229
            // 
            darkLabel229.AutoSize = true;
            darkLabel229.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel229.Location = new Point(17, 457);
            darkLabel229.Margin = new Padding(4, 0, 4, 0);
            darkLabel229.Name = "darkLabel229";
            darkLabel229.Size = new Size(98, 15);
            darkLabel229.TabIndex = 5;
            darkLabel229.Tag = "";
            darkLabel229.Text = "Entity Reputation";
            // 
            // EntityReputationLB
            // 
            EntityReputationLB.BackColor = Color.FromArgb(60, 63, 65);
            EntityReputationLB.DrawMode = DrawMode.OwnerDrawFixed;
            EntityReputationLB.ForeColor = SystemColors.Control;
            EntityReputationLB.FormattingEnabled = true;
            EntityReputationLB.Location = new Point(8, 88);
            EntityReputationLB.Margin = new Padding(4, 3, 4, 3);
            EntityReputationLB.Name = "EntityReputationLB";
            EntityReputationLB.Size = new Size(279, 356);
            EntityReputationLB.TabIndex = 4;
            EntityReputationLB.DrawItem += listBox_DrawItem;
            EntityReputationLB.SelectedIndexChanged += EntityReputationLB_SelectedIndexChanged;
            // 
            // ReputationLossOnDeathNUD
            // 
            ReputationLossOnDeathNUD.BackColor = Color.FromArgb(60, 63, 65);
            ReputationLossOnDeathNUD.ForeColor = SystemColors.Control;
            ReputationLossOnDeathNUD.Location = new Point(167, 23);
            ReputationLossOnDeathNUD.Margin = new Padding(4, 3, 4, 3);
            ReputationLossOnDeathNUD.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            ReputationLossOnDeathNUD.Minimum = new decimal(new int[] { 999999999, 0, 0, int.MinValue });
            ReputationLossOnDeathNUD.Name = "ReputationLossOnDeathNUD";
            ReputationLossOnDeathNUD.Size = new Size(118, 23);
            ReputationLossOnDeathNUD.TabIndex = 1;
            ReputationLossOnDeathNUD.Tag = "Weight";
            ReputationLossOnDeathNUD.TextAlign = HorizontalAlignment.Center;
            ReputationLossOnDeathNUD.ValueChanged += ReputationLossOnDeathNUD_ValueChanged;
            // 
            // darkLabel227
            // 
            darkLabel227.AutoSize = true;
            darkLabel227.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel227.Location = new Point(15, 25);
            darkLabel227.Margin = new Padding(4, 0, 4, 0);
            darkLabel227.Name = "darkLabel227";
            darkLabel227.Size = new Size(144, 15);
            darkLabel227.TabIndex = 0;
            darkLabel227.Tag = "";
            darkLabel227.Text = "Reputation Loss On Death";
            // 
            // ReputationMaxReputationNUD
            // 
            ReputationMaxReputationNUD.BackColor = Color.FromArgb(60, 63, 65);
            ReputationMaxReputationNUD.ForeColor = SystemColors.Control;
            ReputationMaxReputationNUD.Location = new Point(167, 53);
            ReputationMaxReputationNUD.Margin = new Padding(4, 3, 4, 3);
            ReputationMaxReputationNUD.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            ReputationMaxReputationNUD.Minimum = new decimal(new int[] { 999999999, 0, 0, int.MinValue });
            ReputationMaxReputationNUD.Name = "ReputationMaxReputationNUD";
            ReputationMaxReputationNUD.Size = new Size(118, 23);
            ReputationMaxReputationNUD.TabIndex = 3;
            ReputationMaxReputationNUD.Tag = "Weight";
            ReputationMaxReputationNUD.TextAlign = HorizontalAlignment.Center;
            ReputationMaxReputationNUD.ValueChanged += ReputationMaxReputationNUD_ValueChanged;
            // 
            // darkLabel221
            // 
            darkLabel221.AutoSize = true;
            darkLabel221.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel221.Location = new Point(15, 55);
            darkLabel221.Margin = new Padding(4, 0, 4, 0);
            darkLabel221.Name = "darkLabel221";
            darkLabel221.Size = new Size(91, 15);
            darkLabel221.TabIndex = 2;
            darkLabel221.Tag = "";
            darkLabel221.Text = "Max Reputation";
            // 
            // ExpansionHardlineReputationControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox74);
            ForeColor = SystemColors.Control;
            Name = "ExpansionHardlineReputationControl";
            Size = new Size(294, 517);
            groupBox74.ResumeLayout(false);
            groupBox74.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)EntityReputationNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)ReputationLossOnDeathNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)ReputationMaxReputationNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox74;
        private Button darkButton110;
        private Button darkButton111;
        private NumericUpDown EntityReputationNUD;
        private Label darkLabel229;
        private ListBox EntityReputationLB;
        private NumericUpDown ReputationLossOnDeathNUD;
        private Label darkLabel227;
        private NumericUpDown ReputationMaxReputationNUD;
        private Label darkLabel221;
    }
}
