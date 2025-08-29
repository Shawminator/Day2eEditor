namespace EconomyPlugin
{
    partial class cfggameplayMapDataControl
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
            groupBox59 = new GroupBox();
            displayNavInfoCB = new CheckBox();
            displayPlayerPositionCB = new CheckBox();
            ignoreNavItemsOwnershipCB = new CheckBox();
            ignoreMapOwnershipCB = new CheckBox();
            groupBox59.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox59
            // 
            groupBox59.Controls.Add(displayNavInfoCB);
            groupBox59.Controls.Add(displayPlayerPositionCB);
            groupBox59.Controls.Add(ignoreNavItemsOwnershipCB);
            groupBox59.Controls.Add(ignoreMapOwnershipCB);
            groupBox59.ForeColor = SystemColors.Control;
            groupBox59.Location = new Point(0, 0);
            groupBox59.Margin = new Padding(4, 3, 4, 3);
            groupBox59.Name = "groupBox59";
            groupBox59.Padding = new Padding(4, 3, 4, 3);
            groupBox59.Size = new Size(414, 75);
            groupBox59.TabIndex = 87;
            groupBox59.TabStop = false;
            groupBox59.Text = "Map Data";
            // 
            // displayNavInfoCB
            // 
            displayNavInfoCB.AutoSize = true;
            displayNavInfoCB.Location = new Point(224, 44);
            displayNavInfoCB.Margin = new Padding(4, 3, 4, 3);
            displayNavInfoCB.Name = "displayNavInfoCB";
            displayNavInfoCB.Size = new Size(111, 19);
            displayNavInfoCB.TabIndex = 3;
            displayNavInfoCB.Text = "display Nav Info";
            displayNavInfoCB.UseVisualStyleBackColor = true;
            displayNavInfoCB.CheckedChanged += displayNavInfoCB_CheckedChanged;
            // 
            // displayPlayerPositionCB
            // 
            displayPlayerPositionCB.AutoSize = true;
            displayPlayerPositionCB.Location = new Point(224, 17);
            displayPlayerPositionCB.Margin = new Padding(4, 3, 4, 3);
            displayPlayerPositionCB.Name = "displayPlayerPositionCB";
            displayPlayerPositionCB.Size = new Size(144, 19);
            displayPlayerPositionCB.TabIndex = 2;
            displayPlayerPositionCB.Text = "display Player Position";
            displayPlayerPositionCB.UseVisualStyleBackColor = true;
            displayPlayerPositionCB.CheckedChanged += displayPlayerPositionCB_CheckedChanged;
            // 
            // ignoreNavItemsOwnershipCB
            // 
            ignoreNavItemsOwnershipCB.AutoSize = true;
            ignoreNavItemsOwnershipCB.Location = new Point(14, 44);
            ignoreNavItemsOwnershipCB.Margin = new Padding(4, 3, 4, 3);
            ignoreNavItemsOwnershipCB.Name = "ignoreNavItemsOwnershipCB";
            ignoreNavItemsOwnershipCB.Size = new Size(176, 19);
            ignoreNavItemsOwnershipCB.TabIndex = 1;
            ignoreNavItemsOwnershipCB.Text = "ignore Nav Items Ownership";
            ignoreNavItemsOwnershipCB.UseVisualStyleBackColor = true;
            ignoreNavItemsOwnershipCB.CheckedChanged += ignoreNavItemsOwnershipCB_CheckedChanged;
            // 
            // ignoreMapOwnershipCB
            // 
            ignoreMapOwnershipCB.AutoSize = true;
            ignoreMapOwnershipCB.Location = new Point(14, 17);
            ignoreMapOwnershipCB.Margin = new Padding(4, 3, 4, 3);
            ignoreMapOwnershipCB.Name = "ignoreMapOwnershipCB";
            ignoreMapOwnershipCB.Size = new Size(147, 19);
            ignoreMapOwnershipCB.TabIndex = 0;
            ignoreMapOwnershipCB.Text = "ignore Map Ownership";
            ignoreMapOwnershipCB.UseVisualStyleBackColor = true;
            ignoreMapOwnershipCB.CheckedChanged += ignoreMapOwnershipCB_CheckedChanged;
            // 
            // cfggameplayMapDataControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox59);
            ForeColor = SystemColors.Control;
            Name = "cfggameplayMapDataControl";
            Size = new Size(426, 92);
            groupBox59.ResumeLayout(false);
            groupBox59.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox59;
        private CheckBox displayNavInfoCB;
        private CheckBox displayPlayerPositionCB;
        private CheckBox ignoreNavItemsOwnershipCB;
        private CheckBox ignoreMapOwnershipCB;
    }
}
