namespace EconomyPlugin
{
    partial class BaseBuildingCodelocksControl
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
            groupBox3 = new GroupBox();
            darkLabel56 = new Label();
            CodelockAttachModeCB = new ComboBox();
            label2 = new Label();
            DamageWhenEnterWrongCodeLockNUD = new NumericUpDown();
            label1 = new Label();
            CodeLockLengthNUD = new NumericUpDown();
            CodelockActionsAnywhereCB = new CheckBox();
            DoDamageWhenEnterWrongCodeLockCB = new CheckBox();
            RememberCodeCB = new CheckBox();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DamageWhenEnterWrongCodeLockNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CodeLockLengthNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(darkLabel56);
            groupBox3.Controls.Add(CodelockAttachModeCB);
            groupBox3.Controls.Add(label2);
            groupBox3.Controls.Add(DamageWhenEnterWrongCodeLockNUD);
            groupBox3.Controls.Add(label1);
            groupBox3.Controls.Add(CodeLockLengthNUD);
            groupBox3.Controls.Add(CodelockActionsAnywhereCB);
            groupBox3.Controls.Add(DoDamageWhenEnterWrongCodeLockCB);
            groupBox3.Controls.Add(RememberCodeCB);
            groupBox3.ForeColor = SystemColors.Control;
            groupBox3.Location = new Point(0, 0);
            groupBox3.Margin = new Padding(4, 3, 4, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(4, 3, 4, 3);
            groupBox3.Size = new Size(329, 218);
            groupBox3.TabIndex = 6;
            groupBox3.TabStop = false;
            groupBox3.Text = "CodeLocks";
            // 
            // darkLabel56
            // 
            darkLabel56.AutoSize = true;
            darkLabel56.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel56.Location = new Point(16, 23);
            darkLabel56.Margin = new Padding(4, 0, 4, 0);
            darkLabel56.Name = "darkLabel56";
            darkLabel56.Size = new Size(123, 15);
            darkLabel56.TabIndex = 18;
            darkLabel56.Text = "Codelock Attch Mode";
            // 
            // CodelockAttachModeCB
            // 
            CodelockAttachModeCB.BackColor = Color.FromArgb(60, 63, 65);
            CodelockAttachModeCB.ForeColor = SystemColors.Control;
            CodelockAttachModeCB.FormattingEnabled = true;
            CodelockAttachModeCB.Location = new Point(18, 42);
            CodelockAttachModeCB.Margin = new Padding(4, 3, 4, 3);
            CodelockAttachModeCB.Name = "CodelockAttachModeCB";
            CodelockAttachModeCB.Size = new Size(304, 23);
            CodelockAttachModeCB.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(125, 164);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(185, 15);
            label2.TabIndex = 16;
            label2.Text = "Damage When Enter Wrong Code";
            // 
            // DamageWhenEnterWrongCodeLockNUD
            // 
            DamageWhenEnterWrongCodeLockNUD.BackColor = Color.FromArgb(60, 63, 65);
            DamageWhenEnterWrongCodeLockNUD.ForeColor = SystemColors.Control;
            DamageWhenEnterWrongCodeLockNUD.Location = new Point(18, 162);
            DamageWhenEnterWrongCodeLockNUD.Margin = new Padding(4, 3, 4, 3);
            DamageWhenEnterWrongCodeLockNUD.Name = "DamageWhenEnterWrongCodeLockNUD";
            DamageWhenEnterWrongCodeLockNUD.Size = new Size(86, 23);
            DamageWhenEnterWrongCodeLockNUD.TabIndex = 4;
            DamageWhenEnterWrongCodeLockNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(125, 107);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(100, 15);
            label1.TabIndex = 14;
            label1.Text = "CodeLock Length";
            // 
            // CodeLockLengthNUD
            // 
            CodeLockLengthNUD.BackColor = Color.FromArgb(60, 63, 65);
            CodeLockLengthNUD.ForeColor = SystemColors.Control;
            CodeLockLengthNUD.Location = new Point(18, 105);
            CodeLockLengthNUD.Margin = new Padding(4, 3, 4, 3);
            CodeLockLengthNUD.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            CodeLockLengthNUD.Name = "CodeLockLengthNUD";
            CodeLockLengthNUD.Size = new Size(86, 23);
            CodeLockLengthNUD.TabIndex = 2;
            CodeLockLengthNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // CodelockActionsAnywhereCB
            // 
            CodelockActionsAnywhereCB.AutoSize = true;
            CodelockActionsAnywhereCB.ForeColor = SystemColors.Control;
            CodelockActionsAnywhereCB.Location = new Point(19, 82);
            CodelockActionsAnywhereCB.Margin = new Padding(4, 3, 4, 3);
            CodelockActionsAnywhereCB.Name = "CodelockActionsAnywhereCB";
            CodelockActionsAnywhereCB.Size = new Size(175, 19);
            CodelockActionsAnywhereCB.TabIndex = 1;
            CodelockActionsAnywhereCB.Text = "Codelock Actions Anywhere";
            CodelockActionsAnywhereCB.TextAlign = ContentAlignment.MiddleRight;
            CodelockActionsAnywhereCB.UseVisualStyleBackColor = true;
            // 
            // DoDamageWhenEnterWrongCodeLockCB
            // 
            DoDamageWhenEnterWrongCodeLockCB.AutoSize = true;
            DoDamageWhenEnterWrongCodeLockCB.ForeColor = SystemColors.Control;
            DoDamageWhenEnterWrongCodeLockCB.Location = new Point(19, 135);
            DoDamageWhenEnterWrongCodeLockCB.Margin = new Padding(4, 3, 4, 3);
            DoDamageWhenEnterWrongCodeLockCB.Name = "DoDamageWhenEnterWrongCodeLockCB";
            DoDamageWhenEnterWrongCodeLockCB.Size = new Size(222, 19);
            DoDamageWhenEnterWrongCodeLockCB.TabIndex = 3;
            DoDamageWhenEnterWrongCodeLockCB.Text = "Do Damage When Enter Wrong Code";
            DoDamageWhenEnterWrongCodeLockCB.TextAlign = ContentAlignment.MiddleRight;
            DoDamageWhenEnterWrongCodeLockCB.UseVisualStyleBackColor = true;
            // 
            // RememberCodeCB
            // 
            RememberCodeCB.AutoSize = true;
            RememberCodeCB.ForeColor = SystemColors.Control;
            RememberCodeCB.Location = new Point(19, 192);
            RememberCodeCB.Margin = new Padding(4, 3, 4, 3);
            RememberCodeCB.Name = "RememberCodeCB";
            RememberCodeCB.Size = new Size(115, 19);
            RememberCodeCB.TabIndex = 5;
            RememberCodeCB.Text = "Remember Code";
            RememberCodeCB.TextAlign = ContentAlignment.MiddleRight;
            RememberCodeCB.UseVisualStyleBackColor = true;
            // 
            // BaseBuildingCodelocksControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox3);
            ForeColor = SystemColors.Control;
            Name = "BaseBuildingCodelocksControl";
            Size = new Size(329, 218);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DamageWhenEnterWrongCodeLockNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)CodeLockLengthNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox3;
        private Label darkLabel56;
        private ComboBox CodelockAttachModeCB;
        private Label label2;
        private NumericUpDown DamageWhenEnterWrongCodeLockNUD;
        private Label label1;
        private NumericUpDown CodeLockLengthNUD;
        private CheckBox CodelockActionsAnywhereCB;
        private CheckBox DoDamageWhenEnterWrongCodeLockCB;
        private CheckBox RememberCodeCB;
    }
}
