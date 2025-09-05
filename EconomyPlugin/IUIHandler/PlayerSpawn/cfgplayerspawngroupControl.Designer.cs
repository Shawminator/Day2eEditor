namespace EconomyPlugin
{
    partial class cfgplayerspawngroupControl
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
            label43 = new Label();
            generatorposbubblescounterNUD = new NumericUpDown();
            generatorposbubblesusecounterCB = new CheckBox();
            label42 = new Label();
            generatorposbubbleslifetiemNUD = new NumericUpDown();
            generatorposbubblesUseLifetimeCB = new CheckBox();
            label41 = new Label();
            generatorposbubblesGroupnameTB = new TextBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)generatorposbubblescounterNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)generatorposbubbleslifetiemNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label43);
            groupBox1.Controls.Add(generatorposbubblescounterNUD);
            groupBox1.Controls.Add(generatorposbubblesusecounterCB);
            groupBox1.Controls.Add(label42);
            groupBox1.Controls.Add(generatorposbubbleslifetiemNUD);
            groupBox1.Controls.Add(generatorposbubblesUseLifetimeCB);
            groupBox1.Controls.Add(label41);
            groupBox1.Controls.Add(generatorposbubblesGroupnameTB);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(355, 125);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Group";
            // 
            // label43
            // 
            label43.AutoSize = true;
            label43.ForeColor = SystemColors.Control;
            label43.Location = new Point(175, 86);
            label43.Margin = new Padding(4, 0, 4, 0);
            label43.Name = "label43";
            label43.Size = new Size(50, 15);
            label43.TabIndex = 115;
            label43.Text = "Counter";
            label43.Visible = false;
            // 
            // generatorposbubblescounterNUD
            // 
            generatorposbubblescounterNUD.BackColor = Color.FromArgb(60, 63, 65);
            generatorposbubblescounterNUD.ForeColor = SystemColors.Control;
            generatorposbubblescounterNUD.Location = new Point(233, 85);
            generatorposbubblescounterNUD.Margin = new Padding(4, 3, 4, 3);
            generatorposbubblescounterNUD.Maximum = new decimal(new int[] { 30000, 0, 0, 0 });
            generatorposbubblescounterNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            generatorposbubblescounterNUD.Name = "generatorposbubblescounterNUD";
            generatorposbubblescounterNUD.Size = new Size(108, 23);
            generatorposbubblescounterNUD.TabIndex = 114;
            generatorposbubblescounterNUD.TextAlign = HorizontalAlignment.Center;
            generatorposbubblescounterNUD.Visible = false;
            generatorposbubblescounterNUD.ValueChanged += generatorposbubblescounterNUD_ValueChanged;
            // 
            // generatorposbubblesusecounterCB
            // 
            generatorposbubblesusecounterCB.AutoSize = true;
            generatorposbubblesusecounterCB.CheckAlign = ContentAlignment.MiddleRight;
            generatorposbubblesusecounterCB.ForeColor = SystemColors.Control;
            generatorposbubblesusecounterCB.Location = new Point(15, 81);
            generatorposbubblesusecounterCB.Margin = new Padding(4, 3, 4, 3);
            generatorposbubblesusecounterCB.Name = "generatorposbubblesusecounterCB";
            generatorposbubblesusecounterCB.Size = new Size(97, 19);
            generatorposbubblesusecounterCB.TabIndex = 113;
            generatorposbubblesusecounterCB.Text = "Use Counter  ";
            generatorposbubblesusecounterCB.UseVisualStyleBackColor = true;
            generatorposbubblesusecounterCB.CheckedChanged += generatorposbubblesusecounterCB_CheckedChanged;
            // 
            // label42
            // 
            label42.AutoSize = true;
            label42.ForeColor = SystemColors.Control;
            label42.Location = new Point(175, 54);
            label42.Margin = new Padding(4, 0, 4, 0);
            label42.Name = "label42";
            label42.Size = new Size(50, 15);
            label42.TabIndex = 112;
            label42.Text = "Lifetime";
            label42.Visible = false;
            // 
            // generatorposbubbleslifetiemNUD
            // 
            generatorposbubbleslifetiemNUD.BackColor = Color.FromArgb(60, 63, 65);
            generatorposbubbleslifetiemNUD.ForeColor = SystemColors.Control;
            generatorposbubbleslifetiemNUD.Location = new Point(233, 52);
            generatorposbubbleslifetiemNUD.Margin = new Padding(4, 3, 4, 3);
            generatorposbubbleslifetiemNUD.Maximum = new decimal(new int[] { 30000, 0, 0, 0 });
            generatorposbubbleslifetiemNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            generatorposbubbleslifetiemNUD.Name = "generatorposbubbleslifetiemNUD";
            generatorposbubbleslifetiemNUD.Size = new Size(108, 23);
            generatorposbubbleslifetiemNUD.TabIndex = 111;
            generatorposbubbleslifetiemNUD.TextAlign = HorizontalAlignment.Center;
            generatorposbubbleslifetiemNUD.Visible = false;
            generatorposbubbleslifetiemNUD.ValueChanged += generatorposbubbleslifetiemNUD_ValueChanged;
            // 
            // generatorposbubblesUseLifetimeCB
            // 
            generatorposbubblesUseLifetimeCB.AutoSize = true;
            generatorposbubblesUseLifetimeCB.CheckAlign = ContentAlignment.MiddleRight;
            generatorposbubblesUseLifetimeCB.ForeColor = SystemColors.Control;
            generatorposbubblesUseLifetimeCB.Location = new Point(16, 52);
            generatorposbubblesUseLifetimeCB.Margin = new Padding(4, 3, 4, 3);
            generatorposbubblesUseLifetimeCB.Name = "generatorposbubblesUseLifetimeCB";
            generatorposbubblesUseLifetimeCB.Size = new Size(97, 19);
            generatorposbubblesUseLifetimeCB.TabIndex = 110;
            generatorposbubblesUseLifetimeCB.Text = "Use Lifetime  ";
            generatorposbubblesUseLifetimeCB.UseVisualStyleBackColor = true;
            generatorposbubblesUseLifetimeCB.CheckedChanged += generatorposbubblesUseLifetimeCB_CheckedChanged;
            // 
            // label41
            // 
            label41.AutoSize = true;
            label41.ForeColor = SystemColors.Control;
            label41.Location = new Point(18, 26);
            label41.Margin = new Padding(4, 0, 4, 0);
            label41.Name = "label41";
            label41.Size = new Size(75, 15);
            label41.TabIndex = 109;
            label41.Text = "Group Name";
            // 
            // generatorposbubblesGroupnameTB
            // 
            generatorposbubblesGroupnameTB.BackColor = Color.FromArgb(60, 63, 65);
            generatorposbubblesGroupnameTB.ForeColor = SystemColors.Control;
            generatorposbubblesGroupnameTB.Location = new Point(103, 22);
            generatorposbubblesGroupnameTB.Margin = new Padding(4, 3, 4, 3);
            generatorposbubblesGroupnameTB.Name = "generatorposbubblesGroupnameTB";
            generatorposbubblesGroupnameTB.Size = new Size(237, 23);
            generatorposbubblesGroupnameTB.TabIndex = 108;
            generatorposbubblesGroupnameTB.TextChanged += generatorposbubblesGroupnameTB_TextChanged;
            // 
            // cfgplayerspawngroupControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "cfgplayerspawngroupControl";
            Size = new Size(365, 131);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)generatorposbubblescounterNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)generatorposbubbleslifetiemNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label43;
        private NumericUpDown generatorposbubblescounterNUD;
        private CheckBox generatorposbubblesusecounterCB;
        private Label label42;
        private NumericUpDown generatorposbubbleslifetiemNUD;
        private CheckBox generatorposbubblesUseLifetimeCB;
        private Label label41;
        private TextBox generatorposbubblesGroupnameTB;
    }
}
