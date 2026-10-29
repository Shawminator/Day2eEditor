namespace CaparePlugin
{
    partial class CapareNameControl
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
            NameL = new Label();
            NameTB = new TextBox();
            groupBox1.SuspendLayout();

            SuspendLayout();
            //
            // groupBox1
            //
            groupBox1.Controls.Add(NameL);
            groupBox1.Controls.Add(NameTB);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(435, 57);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Name";
            //
            // NameL
            //
            NameL.AutoSize = true;
            NameL.ForeColor = Color.FromArgb(220, 220, 220);
            NameL.Location = new Point(11, 25);
            NameL.Margin = new Padding(4, 0, 4, 0);
            NameL.Name = "NameL";
            NameL.Size = new Size(35, 15);
            NameL.TabIndex = 0;
            NameL.Text = "Name:";
            //
            // NameTB
            //
            NameTB.BackColor = Color.FromArgb(60, 63, 65);
            NameTB.ForeColor = SystemColors.Control;
            NameTB.Location = new Point(190, 22);
            NameTB.Margin = new Padding(4, 3, 4, 3);
            NameTB.Name = "NameTB";
            NameTB.Size = new Size(230, 23);
            NameTB.TabIndex = 1;
            NameTB.TextChanged += NameTB_TextChanged;
            //
            // CapareNameControl
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "CapareNameControl";
            Size = new Size(435, 63);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();

            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label NameL;
        private TextBox NameTB;
    }
}
