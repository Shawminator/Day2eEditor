namespace ExpansionPlugin
{
    partial class ExpansionTraderMapsControl
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
            TraderNameCB = new ComboBox();
            label1 = new Label();
            NpcClassNameCB = new ComboBox();
            darkLabel141 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(TraderNameCB);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(NpcClassNameCB);
            groupBox1.Controls.Add(darkLabel141);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(561, 84);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Expansion Market Trader Item";
            // 
            // TraderNameCB
            // 
            TraderNameCB.BackColor = Color.FromArgb(60, 63, 65);
            TraderNameCB.ForeColor = SystemColors.Control;
            TraderNameCB.FormattingEnabled = true;
            TraderNameCB.Location = new Point(164, 51);
            TraderNameCB.Margin = new Padding(4, 3, 4, 3);
            TraderNameCB.Name = "TraderNameCB";
            TraderNameCB.Size = new Size(390, 23);
            TraderNameCB.TabIndex = 196;
            TraderNameCB.SelectedIndexChanged += TraderNameCB_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(220, 220, 220);
            label1.Location = new Point(14, 54);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(75, 15);
            label1.TabIndex = 197;
            label1.Text = "Trader Name";
            // 
            // NpcClassNameCB
            // 
            NpcClassNameCB.BackColor = Color.FromArgb(60, 63, 65);
            NpcClassNameCB.ForeColor = SystemColors.Control;
            NpcClassNameCB.FormattingEnabled = true;
            NpcClassNameCB.Location = new Point(164, 22);
            NpcClassNameCB.Margin = new Padding(4, 3, 4, 3);
            NpcClassNameCB.Name = "NpcClassNameCB";
            NpcClassNameCB.Size = new Size(390, 23);
            NpcClassNameCB.TabIndex = 193;
            NpcClassNameCB.SelectedIndexChanged += NpcClassNameCB_SelectedIndexChanged;
            // 
            // darkLabel141
            // 
            darkLabel141.AutoSize = true;
            darkLabel141.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel141.Location = new Point(14, 25);
            darkLabel141.Margin = new Padding(4, 0, 4, 0);
            darkLabel141.Name = "darkLabel141";
            darkLabel141.Size = new Size(91, 15);
            darkLabel141.TabIndex = 195;
            darkLabel141.Text = "Npc ClassName";
            // 
            // ExpansionTraderMapsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionTraderMapsControl";
            Size = new Size(572, 88);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private ComboBox NpcClassNameCB;
        private Label darkLabel141;
        private ComboBox TraderNameCB;
        private Label label1;
    }
}
