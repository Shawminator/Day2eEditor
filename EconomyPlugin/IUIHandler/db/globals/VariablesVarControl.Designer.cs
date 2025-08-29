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
            variablesvarvalueNUD = new NumericUpDown();
            label63 = new Label();
            globalsGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)variablesvarvalueNUD).BeginInit();
            SuspendLayout();
            // 
            // globalsGB
            // 
            globalsGB.Controls.Add(variablesvarvalueNUD);
            globalsGB.Controls.Add(label63);
            globalsGB.ForeColor = SystemColors.Control;
            globalsGB.Location = new Point(0, 0);
            globalsGB.Name = "globalsGB";
            globalsGB.Size = new Size(295, 59);
            globalsGB.TabIndex = 131;
            globalsGB.TabStop = false;
            // 
            // variablesvarvalueNUD
            // 
            variablesvarvalueNUD.BackColor = Color.FromArgb(60, 63, 65);
            variablesvarvalueNUD.ForeColor = SystemColors.Control;
            variablesvarvalueNUD.Location = new Point(71, 22);
            variablesvarvalueNUD.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            variablesvarvalueNUD.Minimum = new decimal(new int[] { 100000000, 0, 0, int.MinValue });
            variablesvarvalueNUD.Name = "variablesvarvalueNUD";
            variablesvarvalueNUD.Size = new Size(218, 23);
            variablesvarvalueNUD.TabIndex = 72;
            variablesvarvalueNUD.TextAlign = HorizontalAlignment.Center;
            variablesvarvalueNUD.ValueChanged += variablesvarvalueNUD_ValueChanged;
            // 
            // label63
            // 
            label63.AutoSize = true;
            label63.ForeColor = SystemColors.Control;
            label63.Location = new Point(12, 24);
            label63.Name = "label63";
            label63.Size = new Size(35, 15);
            label63.TabIndex = 71;
            label63.Text = "Value";
            // 
            // VariablesVarControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(globalsGB);
            ForeColor = SystemColors.Control;
            Name = "VariablesVarControl";
            Size = new Size(302, 66);
            globalsGB.ResumeLayout(false);
            globalsGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)variablesvarvalueNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox globalsGB;
        private NumericUpDown variablesvarvalueNUD;
        private Label label63;
    }
}
