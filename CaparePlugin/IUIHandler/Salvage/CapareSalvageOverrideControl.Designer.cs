namespace CaparePlugin
{
    partial class CapareSalvageOverrideControl
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
            ClassNameL = new Label();
            ClassNameTB = new TextBox();
            BasketL = new Label();
            BasketComboBox = new ComboBox();
            groupBox1.SuspendLayout();

            SuspendLayout();
            //
            // groupBox1
            //
            groupBox1.Controls.Add(ClassNameL);
            groupBox1.Controls.Add(ClassNameTB);
            groupBox1.Controls.Add(BasketL);
            groupBox1.Controls.Add(BasketComboBox);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(435, 86);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Salvage Override";
            //
            // ClassNameL
            //
            ClassNameL.AutoSize = true;
            ClassNameL.ForeColor = Color.FromArgb(220, 220, 220);
            ClassNameL.Location = new Point(11, 25);
            ClassNameL.Margin = new Padding(4, 0, 4, 0);
            ClassNameL.Name = "ClassNameL";
            ClassNameL.Size = new Size(70, 15);
            ClassNameL.TabIndex = 0;
            ClassNameL.Text = "Class Name";
            //
            // ClassNameTB
            //
            ClassNameTB.BackColor = Color.FromArgb(60, 63, 65);
            ClassNameTB.ForeColor = SystemColors.Control;
            ClassNameTB.Location = new Point(190, 22);
            ClassNameTB.Margin = new Padding(4, 3, 4, 3);
            ClassNameTB.Name = "ClassNameTB";
            ClassNameTB.Size = new Size(230, 23);
            ClassNameTB.TabIndex = 1;
            ClassNameTB.TextChanged += ClassNameTB_TextChanged;
            //
            // BasketL
            //
            BasketL.AutoSize = true;
            BasketL.ForeColor = Color.FromArgb(220, 220, 220);
            BasketL.Location = new Point(11, 54);
            BasketL.Margin = new Padding(4, 0, 4, 0);
            BasketL.Name = "BasketL";
            BasketL.Size = new Size(42, 15);
            BasketL.TabIndex = 2;
            BasketL.Text = "Basket";
            //
            // BasketComboBox
            //
            BasketComboBox.BackColor = Color.FromArgb(60, 63, 65);
            BasketComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            BasketComboBox.FlatStyle = FlatStyle.Flat;
            BasketComboBox.ForeColor = SystemColors.Control;
            BasketComboBox.FormattingEnabled = true;
            BasketComboBox.Location = new Point(190, 51);
            BasketComboBox.Margin = new Padding(4, 3, 4, 3);
            BasketComboBox.Name = "BasketComboBox";
            BasketComboBox.Size = new Size(230, 23);
            BasketComboBox.TabIndex = 3;
            BasketComboBox.TextChanged += BasketComboBox_TextChanged;
            //
            // CapareSalvageOverrideControl
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "CapareSalvageOverrideControl";
            Size = new Size(435, 92);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();

            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label ClassNameL;
        private TextBox ClassNameTB;
        private Label BasketL;
        private ComboBox BasketComboBox;
    }
}
