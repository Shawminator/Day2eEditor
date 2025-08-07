namespace EconomyPlugin
{
    partial class VariablesVarControl
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
            globalsGB = new GroupBox();
            variablesvartypeCB = new ComboBox();
            label60 = new Label();
            variablesvarnameTB = new TextBox();
            variablesvarvalueNUD = new NumericUpDown();
            label63 = new Label();
            label64 = new Label();
            globalsGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)variablesvarvalueNUD).BeginInit();
            SuspendLayout();
            // 
            // globalsGB
            // 
            globalsGB.Controls.Add(variablesvartypeCB);
            globalsGB.Controls.Add(label60);
            globalsGB.Controls.Add(variablesvarnameTB);
            globalsGB.Controls.Add(variablesvarvalueNUD);
            globalsGB.Controls.Add(label63);
            globalsGB.Controls.Add(label64);
            globalsGB.ForeColor = SystemColors.Control;
            globalsGB.Location = new Point(0, 0);
            globalsGB.Name = "globalsGB";
            globalsGB.Size = new Size(295, 105);
            globalsGB.TabIndex = 131;
            globalsGB.TabStop = false;
            globalsGB.Text = "Info";
            // 
            // variablesvartypeCB
            // 
            variablesvartypeCB.BackColor = Color.FromArgb(60, 63, 65);
            variablesvartypeCB.ForeColor = SystemColors.Control;
            variablesvartypeCB.FormattingEnabled = true;
            variablesvartypeCB.Items.AddRange(new object[] { "Int", "Float" });
            variablesvartypeCB.Location = new Point(71, 44);
            variablesvartypeCB.Name = "variablesvartypeCB";
            variablesvartypeCB.Size = new Size(218, 23);
            variablesvartypeCB.TabIndex = 82;
            // 
            // label60
            // 
            label60.AutoSize = true;
            label60.ForeColor = SystemColors.Control;
            label60.Location = new Point(12, 22);
            label60.Name = "label60";
            label60.Size = new Size(39, 15);
            label60.TabIndex = 81;
            label60.Text = "Name";
            // 
            // variablesvarnameTB
            // 
            variablesvarnameTB.BackColor = Color.FromArgb(60, 63, 65);
            variablesvarnameTB.ForeColor = SystemColors.Control;
            variablesvarnameTB.Location = new Point(71, 19);
            variablesvarnameTB.Name = "variablesvarnameTB";
            variablesvarnameTB.Size = new Size(218, 23);
            variablesvarnameTB.TabIndex = 80;
            // 
            // variablesvarvalueNUD
            // 
            variablesvarvalueNUD.BackColor = Color.FromArgb(60, 63, 65);
            variablesvarvalueNUD.ForeColor = SystemColors.Control;
            variablesvarvalueNUD.Location = new Point(71, 71);
            variablesvarvalueNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            variablesvarvalueNUD.Minimum = new decimal(new int[] { 100000000, 0, 0, int.MinValue });
            variablesvarvalueNUD.Name = "variablesvarvalueNUD";
            variablesvarvalueNUD.Size = new Size(218, 23);
            variablesvarvalueNUD.TabIndex = 72;
            variablesvarvalueNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // label63
            // 
            label63.AutoSize = true;
            label63.ForeColor = SystemColors.Control;
            label63.Location = new Point(12, 73);
            label63.Name = "label63";
            label63.Size = new Size(35, 15);
            label63.TabIndex = 71;
            label63.Text = "Value";
            // 
            // label64
            // 
            label64.AutoSize = true;
            label64.ForeColor = SystemColors.Control;
            label64.Location = new Point(12, 47);
            label64.Name = "label64";
            label64.Size = new Size(32, 15);
            label64.TabIndex = 69;
            label64.Text = "Type";
            // 
            // VariablesVarControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(globalsGB);
            ForeColor = SystemColors.Control;
            Name = "VariablesVarControl";
            Size = new Size(302, 111);
            globalsGB.ResumeLayout(false);
            globalsGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)variablesvarvalueNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox globalsGB;
        private ComboBox variablesvartypeCB;
        private Label label60;
        private TextBox variablesvarnameTB;
        private NumericUpDown variablesvarvalueNUD;
        private Label label63;
        private Label label64;
    }
}
