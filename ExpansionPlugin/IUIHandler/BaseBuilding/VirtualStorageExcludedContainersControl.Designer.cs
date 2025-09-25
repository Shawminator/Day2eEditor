namespace ExpansionPlugin
{
    partial class VirtualStorageExcludedContainersControl
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
            groupBox79 = new GroupBox();
            EnableVirtualStorageCB = new CheckBox();
            groupBox79.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox79
            // 
            groupBox79.Controls.Add(EnableVirtualStorageCB);
            groupBox79.ForeColor = SystemColors.Control;
            groupBox79.Location = new Point(0, 0);
            groupBox79.Margin = new Padding(4, 3, 4, 3);
            groupBox79.Name = "groupBox79";
            groupBox79.Padding = new Padding(4, 3, 4, 3);
            groupBox79.Size = new Size(248, 57);
            groupBox79.TabIndex = 8;
            groupBox79.TabStop = false;
            groupBox79.Text = "Storage";
            // 
            // EnableVirtualStorageCB
            // 
            EnableVirtualStorageCB.AutoSize = true;
            EnableVirtualStorageCB.ForeColor = SystemColors.Control;
            EnableVirtualStorageCB.Location = new Point(19, 22);
            EnableVirtualStorageCB.Margin = new Padding(4, 3, 4, 3);
            EnableVirtualStorageCB.Name = "EnableVirtualStorageCB";
            EnableVirtualStorageCB.Size = new Size(141, 19);
            EnableVirtualStorageCB.TabIndex = 0;
            EnableVirtualStorageCB.Text = "Enable Virtual Storage";
            EnableVirtualStorageCB.TextAlign = ContentAlignment.MiddleRight;
            EnableVirtualStorageCB.UseVisualStyleBackColor = true;
            // 
            // VirtualStorageExcludedContainersControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox79);
            ForeColor = SystemColors.Control;
            Name = "VirtualStorageExcludedContainersControl";
            Size = new Size(248, 57);
            groupBox79.ResumeLayout(false);
            groupBox79.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox79;
        private CheckBox EnableVirtualStorageCB;
    }
}
