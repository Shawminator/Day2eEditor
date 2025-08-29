namespace EconomyPlugin
{
    partial class economyControl
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
            economyGB = new GroupBox();
            economysaveCB = new CheckBox();
            economyrespawnCB = new CheckBox();
            economyloadCB = new CheckBox();
            economyinitCB = new CheckBox();
            economyGB.SuspendLayout();
            SuspendLayout();
            // 
            // economyGB
            // 
            economyGB.Controls.Add(economysaveCB);
            economyGB.Controls.Add(economyrespawnCB);
            economyGB.Controls.Add(economyloadCB);
            economyGB.Controls.Add(economyinitCB);
            economyGB.ForeColor = SystemColors.Control;
            economyGB.Location = new Point(0, 0);
            economyGB.Name = "economyGB";
            economyGB.Size = new Size(125, 128);
            economyGB.TabIndex = 0;
            economyGB.TabStop = false;
            economyGB.Text = "groupBox1";
            // 
            // economysaveCB
            // 
            economysaveCB.AutoSize = true;
            economysaveCB.Location = new Point(38, 97);
            economysaveCB.Name = "economysaveCB";
            economysaveCB.RightToLeft = RightToLeft.Yes;
            economysaveCB.Size = new Size(49, 19);
            economysaveCB.TabIndex = 3;
            economysaveCB.Text = "save";
            economysaveCB.UseVisualStyleBackColor = true;
            economysaveCB.CheckedChanged += economysaveCB_CheckedChanged;
            // 
            // economyrespawnCB
            // 
            economyrespawnCB.AutoSize = true;
            economyrespawnCB.Location = new Point(17, 72);
            economyrespawnCB.Name = "economyrespawnCB";
            economyrespawnCB.RightToLeft = RightToLeft.Yes;
            economyrespawnCB.Size = new Size(70, 19);
            economyrespawnCB.TabIndex = 2;
            economyrespawnCB.Text = "respawn";
            economyrespawnCB.UseVisualStyleBackColor = true;
            economyrespawnCB.CheckedChanged += economyrespawnCB_CheckedChanged;
            // 
            // economyloadCB
            // 
            economyloadCB.AutoSize = true;
            economyloadCB.Location = new Point(38, 47);
            economyloadCB.Name = "economyloadCB";
            economyloadCB.RightToLeft = RightToLeft.Yes;
            economyloadCB.Size = new Size(49, 19);
            economyloadCB.TabIndex = 1;
            economyloadCB.Text = "load";
            economyloadCB.UseVisualStyleBackColor = true;
            economyloadCB.CheckedChanged += economyloadCB_CheckedChanged;
            // 
            // economyinitCB
            // 
            economyinitCB.AutoSize = true;
            economyinitCB.Location = new Point(44, 22);
            economyinitCB.Name = "economyinitCB";
            economyinitCB.RightToLeft = RightToLeft.Yes;
            economyinitCB.Size = new Size(43, 19);
            economyinitCB.TabIndex = 0;
            economyinitCB.Text = "init";
            economyinitCB.UseVisualStyleBackColor = true;
            economyinitCB.CheckedChanged += economyinitCB_CheckedChanged;
            // 
            // economyControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(economyGB);
            ForeColor = SystemColors.Control;
            Name = "economyControl";
            Size = new Size(129, 131);
            economyGB.ResumeLayout(false);
            economyGB.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox economyGB;
        private CheckBox economysaveCB;
        private CheckBox economyrespawnCB;
        private CheckBox economyloadCB;
        private CheckBox economyinitCB;
    }
}
