namespace ExpansionPlugin
{
    partial class ExpansionCoreControl
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
            groupBox7 = new GroupBox();
            EnableInventoryCargoTidyCB = new CheckBox();
            ForceExactCEItemLifetimeCB = new CheckBox();
            darkLabel215 = new Label();
            ServerUpdateRateLimitNUD = new NumericUpDown();
            groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ServerUpdateRateLimitNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(EnableInventoryCargoTidyCB);
            groupBox7.Controls.Add(ForceExactCEItemLifetimeCB);
            groupBox7.Controls.Add(darkLabel215);
            groupBox7.Controls.Add(ServerUpdateRateLimitNUD);
            groupBox7.ForeColor = SystemColors.Control;
            groupBox7.Location = new Point(0, 0);
            groupBox7.Margin = new Padding(4, 3, 4, 3);
            groupBox7.Name = "groupBox7";
            groupBox7.Padding = new Padding(4, 3, 4, 3);
            groupBox7.Size = new Size(351, 114);
            groupBox7.TabIndex = 7;
            groupBox7.TabStop = false;
            groupBox7.Text = "Core";
            // 
            // EnableInventoryCargoTidyCB
            // 
            EnableInventoryCargoTidyCB.AutoSize = true;
            EnableInventoryCargoTidyCB.ForeColor = SystemColors.Control;
            EnableInventoryCargoTidyCB.Location = new Point(8, 78);
            EnableInventoryCargoTidyCB.Margin = new Padding(4, 3, 4, 3);
            EnableInventoryCargoTidyCB.Name = "EnableInventoryCargoTidyCB";
            EnableInventoryCargoTidyCB.Size = new Size(174, 19);
            EnableInventoryCargoTidyCB.TabIndex = 41;
            EnableInventoryCargoTidyCB.Text = "Enable Inventory Cargo Tidy";
            EnableInventoryCargoTidyCB.TextAlign = ContentAlignment.MiddleRight;
            EnableInventoryCargoTidyCB.UseVisualStyleBackColor = true;
            EnableInventoryCargoTidyCB.CheckedChanged += EnableInventoryCargoTidyCB_CheckedChanged;
            // 
            // ForceExactCEItemLifetimeCB
            // 
            ForceExactCEItemLifetimeCB.AutoSize = true;
            ForceExactCEItemLifetimeCB.ForeColor = SystemColors.Control;
            ForceExactCEItemLifetimeCB.Location = new Point(8, 52);
            ForceExactCEItemLifetimeCB.Margin = new Padding(4, 3, 4, 3);
            ForceExactCEItemLifetimeCB.Name = "ForceExactCEItemLifetimeCB";
            ForceExactCEItemLifetimeCB.Size = new Size(176, 19);
            ForceExactCEItemLifetimeCB.TabIndex = 40;
            ForceExactCEItemLifetimeCB.Text = "Force Exact CE Item Lifetime";
            ForceExactCEItemLifetimeCB.TextAlign = ContentAlignment.MiddleRight;
            ForceExactCEItemLifetimeCB.UseVisualStyleBackColor = true;
            ForceExactCEItemLifetimeCB.CheckedChanged += ForceExactCEItemLifetimeCB_CheckedChanged;
            // 
            // darkLabel215
            // 
            darkLabel215.AutoSize = true;
            darkLabel215.ForeColor = SystemColors.Control;
            darkLabel215.Location = new Point(92, 27);
            darkLabel215.Margin = new Padding(4, 0, 4, 0);
            darkLabel215.Name = "darkLabel215";
            darkLabel215.Size = new Size(136, 15);
            darkLabel215.TabIndex = 39;
            darkLabel215.Text = "Server Update Rate Limit";
            // 
            // ServerUpdateRateLimitNUD
            // 
            ServerUpdateRateLimitNUD.BackColor = Color.FromArgb(60, 63, 65);
            ServerUpdateRateLimitNUD.ForeColor = SystemColors.Control;
            ServerUpdateRateLimitNUD.Location = new Point(8, 22);
            ServerUpdateRateLimitNUD.Margin = new Padding(4, 3, 4, 3);
            ServerUpdateRateLimitNUD.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            ServerUpdateRateLimitNUD.Name = "ServerUpdateRateLimitNUD";
            ServerUpdateRateLimitNUD.Size = new Size(76, 23);
            ServerUpdateRateLimitNUD.TabIndex = 1;
            ServerUpdateRateLimitNUD.TextAlign = HorizontalAlignment.Center;
            ServerUpdateRateLimitNUD.ValueChanged += ServerUpdateRateLimitNUD_ValueChanged;
            // 
            // ExpansionCoreControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox7);
            ForeColor = SystemColors.Control;
            Name = "ExpansionCoreControl";
            Size = new Size(351, 114);
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ServerUpdateRateLimitNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox7;
        private CheckBox EnableInventoryCargoTidyCB;
        private CheckBox ForceExactCEItemLifetimeCB;
        private Label darkLabel215;
        private NumericUpDown ServerUpdateRateLimitNUD;
    }
}
