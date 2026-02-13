namespace ExpansionPlugin
{
    partial class ExpansionStartingClothingGeneralControl
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
            groupBox45 = new GroupBox();
            SetRandomHealthCB = new CheckBox();
            EnableCustomClothingCB = new CheckBox();
            label1 = new Label();
            label2 = new Label();
            groupBox45.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox45
            // 
            groupBox45.Controls.Add(label2);
            groupBox45.Controls.Add(label1);
            groupBox45.Controls.Add(SetRandomHealthCB);
            groupBox45.Controls.Add(EnableCustomClothingCB);
            groupBox45.ForeColor = SystemColors.Control;
            groupBox45.Location = new Point(0, 0);
            groupBox45.Margin = new Padding(4, 3, 4, 3);
            groupBox45.Name = "groupBox45";
            groupBox45.Padding = new Padding(4, 3, 4, 3);
            groupBox45.Size = new Size(214, 87);
            groupBox45.TabIndex = 2;
            groupBox45.TabStop = false;
            groupBox45.Text = "General Settings";
            // 
            // SetRandomHealthCB
            // 
            SetRandomHealthCB.AutoSize = true;
            SetRandomHealthCB.Location = new Point(183, 56);
            SetRandomHealthCB.Margin = new Padding(4, 3, 4, 3);
            SetRandomHealthCB.Name = "SetRandomHealthCB";
            SetRandomHealthCB.Size = new Size(15, 14);
            SetRandomHealthCB.TabIndex = 1;
            SetRandomHealthCB.Tag = "SetRandomHealth";
            SetRandomHealthCB.UseVisualStyleBackColor = true;
            SetRandomHealthCB.CheckedChanged += SetRandomHealthCB_CheckedChanged;
            // 
            // EnableCustomClothingCB
            // 
            EnableCustomClothingCB.AutoSize = true;
            EnableCustomClothingCB.Location = new Point(183, 26);
            EnableCustomClothingCB.Margin = new Padding(4, 3, 4, 3);
            EnableCustomClothingCB.Name = "EnableCustomClothingCB";
            EnableCustomClothingCB.Size = new Size(15, 14);
            EnableCustomClothingCB.TabIndex = 0;
            EnableCustomClothingCB.Tag = "EnableCustomClothing";
            EnableCustomClothingCB.UseVisualStyleBackColor = true;
            EnableCustomClothingCB.CheckedChanged += EnableCustomClothingCB_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 25);
            label1.Name = "label1";
            label1.Size = new Size(136, 15);
            label1.TabIndex = 2;
            label1.Text = "Enable Custom Clothing";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 55);
            label2.Name = "label2";
            label2.Size = new Size(109, 15);
            label2.TabIndex = 3;
            label2.Text = "Set Random Health";
            // 
            // ExpansionStartingClothingGeneralControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox45);
            ForeColor = SystemColors.Control;
            Name = "ExpansionStartingClothingGeneralControl";
            Size = new Size(214, 87);
            groupBox45.ResumeLayout(false);
            groupBox45.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox45;
        private Label label2;
        private Label label1;
        private CheckBox SetRandomHealthCB;
        private CheckBox EnableCustomClothingCB;
    }
}
