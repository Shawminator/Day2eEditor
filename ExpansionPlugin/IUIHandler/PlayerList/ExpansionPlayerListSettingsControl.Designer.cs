namespace ExpansionPlugin
{
    partial class ExpansionPlayerListSettingsControl
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
            groupBox10 = new GroupBox();
            EnablePlayerListCB = new CheckBox();
            EnableTooltipCB = new CheckBox();
            groupBox10.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox10
            // 
            groupBox10.Controls.Add(EnablePlayerListCB);
            groupBox10.Controls.Add(EnableTooltipCB);
            groupBox10.ForeColor = SystemColors.Control;
            groupBox10.Location = new Point(0, 0);
            groupBox10.Margin = new Padding(4, 3, 4, 3);
            groupBox10.Name = "groupBox10";
            groupBox10.Padding = new Padding(4, 3, 4, 3);
            groupBox10.Size = new Size(172, 77);
            groupBox10.TabIndex = 10;
            groupBox10.TabStop = false;
            groupBox10.Text = "Player List";
            // 
            // EnablePlayerListCB
            // 
            EnablePlayerListCB.AutoSize = true;
            EnablePlayerListCB.ForeColor = SystemColors.Control;
            EnablePlayerListCB.Location = new Point(28, 22);
            EnablePlayerListCB.Margin = new Padding(4, 3, 4, 3);
            EnablePlayerListCB.Name = "EnablePlayerListCB";
            EnablePlayerListCB.Size = new Size(117, 19);
            EnablePlayerListCB.TabIndex = 0;
            EnablePlayerListCB.Text = "Enable Player List";
            EnablePlayerListCB.TextAlign = ContentAlignment.MiddleRight;
            EnablePlayerListCB.UseVisualStyleBackColor = true;
            EnablePlayerListCB.CheckedChanged += EnablePlayerListCB_CheckedChanged;
            // 
            // EnableTooltipCB
            // 
            EnableTooltipCB.AutoSize = true;
            EnableTooltipCB.ForeColor = SystemColors.Control;
            EnableTooltipCB.Location = new Point(28, 49);
            EnableTooltipCB.Margin = new Padding(4, 3, 4, 3);
            EnableTooltipCB.Name = "EnableTooltipCB";
            EnableTooltipCB.Size = new Size(100, 19);
            EnableTooltipCB.TabIndex = 1;
            EnableTooltipCB.Text = "Enable Tooltip";
            EnableTooltipCB.TextAlign = ContentAlignment.MiddleRight;
            EnableTooltipCB.UseVisualStyleBackColor = true;
            EnableTooltipCB.CheckedChanged += EnableTooltipCB_CheckedChanged;
            // 
            // ExpansionPlayerListSettingsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox10);
            ForeColor = SystemColors.Control;
            Name = "ExpansionPlayerListSettingsControl";
            Size = new Size(172, 77);
            groupBox10.ResumeLayout(false);
            groupBox10.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox10;
        private CheckBox EnablePlayerListCB;
        private CheckBox EnableTooltipCB;
    }
}
