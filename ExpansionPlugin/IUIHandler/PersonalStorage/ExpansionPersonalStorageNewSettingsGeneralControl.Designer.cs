namespace ExpansionPlugin
{
    partial class ExpansionPersonalStorageNewSettingsGeneralControl
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
            label1 = new Label();
            UseCategoryMenuCB = new CheckBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(UseCategoryMenuCB);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(227, 55);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Genral";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(14, 25);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(111, 15);
            label1.TabIndex = 49;
            label1.Text = "Use Category Menu";
            // 
            // UseCategoryMenuCB
            // 
            UseCategoryMenuCB.AutoSize = true;
            UseCategoryMenuCB.ForeColor = SystemColors.Control;
            UseCategoryMenuCB.Location = new Point(198, 26);
            UseCategoryMenuCB.Margin = new Padding(4, 3, 4, 3);
            UseCategoryMenuCB.Name = "UseCategoryMenuCB";
            UseCategoryMenuCB.Size = new Size(15, 14);
            UseCategoryMenuCB.TabIndex = 48;
            UseCategoryMenuCB.TextAlign = ContentAlignment.MiddleRight;
            UseCategoryMenuCB.UseVisualStyleBackColor = true;
            UseCategoryMenuCB.CheckedChanged += UseCategoryMenuCB_CheckedChanged;
            // 
            // ExpansionPersonalStorageNewSettingsGeneralControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionPersonalStorageNewSettingsGeneralControl";
            Size = new Size(227, 55);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label1;
        private CheckBox UseCategoryMenuCB;
    }
}
