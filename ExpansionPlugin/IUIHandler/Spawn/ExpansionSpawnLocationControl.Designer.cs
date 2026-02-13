namespace ExpansionPlugin
{
    partial class ExpansionSpawnLocationControl
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
            label2 = new Label();
            NameTB = new TextBox();
            UseCooldownCB = new CheckBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(NameTB);
            groupBox1.Controls.Add(UseCooldownCB);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(405, 83);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Spawn Location";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 25);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 0;
            label1.Text = "Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 55);
            label2.Name = "label2";
            label2.Size = new Size(84, 15);
            label2.TabIndex = 1;
            label2.Text = "Use Cooldown";
            // 
            // NameTB
            // 
            NameTB.BackColor = Color.FromArgb(60, 63, 65);
            NameTB.ForeColor = SystemColors.Control;
            NameTB.Location = new Point(118, 22);
            NameTB.Margin = new Padding(4, 3, 4, 3);
            NameTB.Name = "NameTB";
            NameTB.Size = new Size(280, 23);
            NameTB.TabIndex = 21;
            NameTB.TextChanged += NameTB_TextChanged;
            // 
            // UseCooldownCB
            // 
            UseCooldownCB.AutoSize = true;
            UseCooldownCB.Location = new Point(118, 56);
            UseCooldownCB.Margin = new Padding(4, 3, 4, 3);
            UseCooldownCB.Name = "UseCooldownCB";
            UseCooldownCB.Size = new Size(15, 14);
            UseCooldownCB.TabIndex = 20;
            UseCooldownCB.Tag = "CreateDeathMarker";
            UseCooldownCB.UseVisualStyleBackColor = true;
            UseCooldownCB.CheckedChanged += UseCooldownCB_CheckedChanged;
            // 
            // ExpansionSpawnLocationControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionSpawnLocationControl";
            Size = new Size(405, 83);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private TextBox NameTB;
        private CheckBox UseCooldownCB;
    }
}
