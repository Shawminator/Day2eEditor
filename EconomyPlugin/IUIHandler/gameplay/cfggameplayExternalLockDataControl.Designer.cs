namespace EconomyPlugin
{
    partial class cfggameplayExternalLockDataControl
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
            groupBox61 = new GroupBox();
            protectionResetTimeNUD = new NumericUpDown();
            label1 = new Label();
            protectionTimeNUD = new NumericUpDown();
            label139 = new Label();
            protectionCountStageTwoNUD = new NumericUpDown();
            label132 = new Label();
            protectionCountStageOneNUD = new NumericUpDown();
            label138 = new Label();
            groupBox61.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)protectionResetTimeNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)protectionTimeNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)protectionCountStageTwoNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)protectionCountStageOneNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox61
            // 
            groupBox61.Controls.Add(protectionResetTimeNUD);
            groupBox61.Controls.Add(label1);
            groupBox61.Controls.Add(protectionTimeNUD);
            groupBox61.Controls.Add(label139);
            groupBox61.Controls.Add(protectionCountStageTwoNUD);
            groupBox61.Controls.Add(label132);
            groupBox61.Controls.Add(protectionCountStageOneNUD);
            groupBox61.Controls.Add(label138);
            groupBox61.ForeColor = SystemColors.Control;
            groupBox61.Location = new Point(0, 0);
            groupBox61.Margin = new Padding(4, 3, 4, 3);
            groupBox61.Name = "groupBox61";
            groupBox61.Padding = new Padding(4, 3, 4, 3);
            groupBox61.Size = new Size(344, 149);
            groupBox61.TabIndex = 73;
            groupBox61.TabStop = false;
            groupBox61.Text = "External Lock Data";
            // 
            // protectionResetTimeNUD
            // 
            protectionResetTimeNUD.BackColor = Color.FromArgb(60, 63, 65);
            protectionResetTimeNUD.DecimalPlaces = 2;
            protectionResetTimeNUD.ForeColor = SystemColors.Control;
            protectionResetTimeNUD.Location = new Point(250, 111);
            protectionResetTimeNUD.Margin = new Padding(4, 3, 4, 3);
            protectionResetTimeNUD.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            protectionResetTimeNUD.Name = "protectionResetTimeNUD";
            protectionResetTimeNUD.Size = new Size(79, 23);
            protectionResetTimeNUD.TabIndex = 74;
            protectionResetTimeNUD.TextAlign = HorizontalAlignment.Center;
            protectionResetTimeNUD.ValueChanged += protectionResetTimeNUD_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(8, 116);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(123, 15);
            label1.TabIndex = 73;
            label1.Text = "Protection Reset Time";
            // 
            // protectionTimeNUD
            // 
            protectionTimeNUD.BackColor = Color.FromArgb(60, 63, 65);
            protectionTimeNUD.DecimalPlaces = 2;
            protectionTimeNUD.ForeColor = SystemColors.Control;
            protectionTimeNUD.Location = new Point(250, 82);
            protectionTimeNUD.Margin = new Padding(4, 3, 4, 3);
            protectionTimeNUD.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            protectionTimeNUD.Name = "protectionTimeNUD";
            protectionTimeNUD.Size = new Size(79, 23);
            protectionTimeNUD.TabIndex = 72;
            protectionTimeNUD.TextAlign = HorizontalAlignment.Center;
            protectionTimeNUD.ValueChanged += protectionTimeNUD_ValueChanged;
            // 
            // label139
            // 
            label139.AutoSize = true;
            label139.ForeColor = SystemColors.Control;
            label139.Location = new Point(8, 87);
            label139.Margin = new Padding(4, 0, 4, 0);
            label139.Name = "label139";
            label139.Size = new Size(92, 15);
            label139.TabIndex = 71;
            label139.Text = "Protection Time";
            // 
            // protectionCountStageTwoNUD
            // 
            protectionCountStageTwoNUD.BackColor = Color.FromArgb(60, 63, 65);
            protectionCountStageTwoNUD.ForeColor = SystemColors.Control;
            protectionCountStageTwoNUD.Location = new Point(250, 52);
            protectionCountStageTwoNUD.Margin = new Padding(4, 3, 4, 3);
            protectionCountStageTwoNUD.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            protectionCountStageTwoNUD.Name = "protectionCountStageTwoNUD";
            protectionCountStageTwoNUD.Size = new Size(79, 23);
            protectionCountStageTwoNUD.TabIndex = 70;
            protectionCountStageTwoNUD.TextAlign = HorizontalAlignment.Center;
            protectionCountStageTwoNUD.ValueChanged += protectionCountStageTwoNUD_ValueChanged;
            // 
            // label132
            // 
            label132.AutoSize = true;
            label132.ForeColor = SystemColors.Control;
            label132.Location = new Point(8, 57);
            label132.Margin = new Padding(4, 0, 4, 0);
            label132.Name = "label132";
            label132.Size = new Size(155, 15);
            label132.TabIndex = 69;
            label132.Text = "Protection Count Stage Two";
            // 
            // protectionCountStageOneNUD
            // 
            protectionCountStageOneNUD.BackColor = Color.FromArgb(60, 63, 65);
            protectionCountStageOneNUD.ForeColor = SystemColors.Control;
            protectionCountStageOneNUD.Location = new Point(250, 22);
            protectionCountStageOneNUD.Margin = new Padding(4, 3, 4, 3);
            protectionCountStageOneNUD.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            protectionCountStageOneNUD.Name = "protectionCountStageOneNUD";
            protectionCountStageOneNUD.Size = new Size(79, 23);
            protectionCountStageOneNUD.TabIndex = 68;
            protectionCountStageOneNUD.TextAlign = HorizontalAlignment.Center;
            protectionCountStageOneNUD.ValueChanged += protectionCountStageOneNUD_ValueChanged;
            // 
            // label138
            // 
            label138.AutoSize = true;
            label138.ForeColor = SystemColors.Control;
            label138.Location = new Point(8, 27);
            label138.Margin = new Padding(4, 0, 4, 0);
            label138.Name = "label138";
            label138.Size = new Size(155, 15);
            label138.TabIndex = 67;
            label138.Text = "Protection Count Stage One";
            // 
            // cfggameplayExternalLockDataControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox61);
            ForeColor = SystemColors.Control;
            Name = "cfggameplayExternalLockDataControl";
            Size = new Size(344, 149);
            groupBox61.ResumeLayout(false);
            groupBox61.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)protectionResetTimeNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)protectionTimeNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)protectionCountStageTwoNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)protectionCountStageOneNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox61;
        private NumericUpDown protectionTimeNUD;
        private Label label139;
        private NumericUpDown protectionCountStageTwoNUD;
        private Label label132;
        private NumericUpDown protectionCountStageOneNUD;
        private Label label138;
        private NumericUpDown protectionResetTimeNUD;
        private Label label1;
    }
}
