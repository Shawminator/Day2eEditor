namespace EconomyPlugin
{
    partial class SpawnGearsimpleChildrenUseDefaultAttributesControl
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
            simpleChildrenUseDefaultAttributesGB = new GroupBox();
            simpleChildrenUseDefaultAttributesCB = new CheckBox();
            simpleChildrenUseDefaultAttributesGB.SuspendLayout();
            SuspendLayout();
            // 
            // simpleChildrenUseDefaultAttributesGB
            // 
            simpleChildrenUseDefaultAttributesGB.Controls.Add(simpleChildrenUseDefaultAttributesCB);
            simpleChildrenUseDefaultAttributesGB.ForeColor = SystemColors.Control;
            simpleChildrenUseDefaultAttributesGB.Location = new Point(4, 3);
            simpleChildrenUseDefaultAttributesGB.Margin = new Padding(4, 3, 4, 3);
            simpleChildrenUseDefaultAttributesGB.Name = "simpleChildrenUseDefaultAttributesGB";
            simpleChildrenUseDefaultAttributesGB.Padding = new Padding(4, 3, 4, 3);
            simpleChildrenUseDefaultAttributesGB.Size = new Size(228, 60);
            simpleChildrenUseDefaultAttributesGB.TabIndex = 232;
            simpleChildrenUseDefaultAttributesGB.TabStop = false;
            simpleChildrenUseDefaultAttributesGB.Text = "Simple Children Use Default Attributes";
            // 
            // simpleChildrenUseDefaultAttributesCB
            // 
            simpleChildrenUseDefaultAttributesCB.AutoSize = true;
            simpleChildrenUseDefaultAttributesCB.Location = new Point(17, 27);
            simpleChildrenUseDefaultAttributesCB.Margin = new Padding(4, 3, 4, 3);
            simpleChildrenUseDefaultAttributesCB.Name = "simpleChildrenUseDefaultAttributesCB";
            simpleChildrenUseDefaultAttributesCB.Size = new Size(15, 14);
            simpleChildrenUseDefaultAttributesCB.TabIndex = 0;
            simpleChildrenUseDefaultAttributesCB.UseVisualStyleBackColor = true;
            simpleChildrenUseDefaultAttributesCB.CheckedChanged += simpleChildrenUseDefaultAttributesCB_CheckedChanged;
            // 
            // SpawnGearsimpleChildrenUseDefaultAttributesControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(simpleChildrenUseDefaultAttributesGB);
            ForeColor = SystemColors.Control;
            Name = "SpawnGearsimpleChildrenUseDefaultAttributesControl";
            Size = new Size(243, 76);
            simpleChildrenUseDefaultAttributesGB.ResumeLayout(false);
            simpleChildrenUseDefaultAttributesGB.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox simpleChildrenUseDefaultAttributesGB;
        private CheckBox simpleChildrenUseDefaultAttributesCB;
    }
}
