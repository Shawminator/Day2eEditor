namespace ExpansionPlugin
{
    partial class AIPatrolLoadbalancingcategorieControl
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
            nameLBCGB = new GroupBox();
            NameLBCTB = new TextBox();
            darkLabel199 = new Label();
            nameLBCGB.SuspendLayout();
            SuspendLayout();
            // 
            // nameLBCGB
            // 
            nameLBCGB.Controls.Add(NameLBCTB);
            nameLBCGB.Controls.Add(darkLabel199);
            nameLBCGB.ForeColor = SystemColors.Control;
            nameLBCGB.Location = new Point(0, 0);
            nameLBCGB.Margin = new Padding(4, 3, 4, 3);
            nameLBCGB.Name = "nameLBCGB";
            nameLBCGB.Padding = new Padding(4, 3, 4, 3);
            nameLBCGB.Size = new Size(470, 76);
            nameLBCGB.TabIndex = 218;
            nameLBCGB.TabStop = false;
            nameLBCGB.Text = "Load Balancing Category";
            // 
            // NameLBCTB
            // 
            NameLBCTB.BackColor = Color.FromArgb(60, 63, 65);
            NameLBCTB.ForeColor = SystemColors.Control;
            NameLBCTB.Location = new Point(72, 32);
            NameLBCTB.Margin = new Padding(4, 3, 4, 3);
            NameLBCTB.Name = "NameLBCTB";
            NameLBCTB.Size = new Size(382, 23);
            NameLBCTB.TabIndex = 204;
            NameLBCTB.TextChanged += NameLBCTB_TextChanged;
            // 
            // darkLabel199
            // 
            darkLabel199.AutoSize = true;
            darkLabel199.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel199.Location = new Point(15, 36);
            darkLabel199.Margin = new Padding(4, 0, 4, 0);
            darkLabel199.Name = "darkLabel199";
            darkLabel199.Size = new Size(39, 15);
            darkLabel199.TabIndex = 203;
            darkLabel199.Text = "Name";
            // 
            // AIPatrolLoadbalancingcategorieControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(nameLBCGB);
            ForeColor = SystemColors.Control;
            Name = "AIPatrolLoadbalancingcategorieControl";
            Size = new Size(470, 76);
            nameLBCGB.ResumeLayout(false);
            nameLBCGB.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox nameLBCGB;
        private TextBox NameLBCTB;
        private Label darkLabel199;
    }
}
