namespace EconomyPlugin
{
    partial class cfggameplaySandstorm
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
            sandstormFrequencyNUD = new NumericUpDown();
            label138 = new Label();
            groupBox61.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)sandstormFrequencyNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox61
            // 
            groupBox61.Controls.Add(sandstormFrequencyNUD);
            groupBox61.Controls.Add(label138);
            groupBox61.ForeColor = SystemColors.Control;
            groupBox61.Location = new Point(0, 0);
            groupBox61.Margin = new Padding(4, 3, 4, 3);
            groupBox61.Name = "groupBox61";
            groupBox61.Padding = new Padding(4, 3, 4, 3);
            groupBox61.Size = new Size(344, 64);
            groupBox61.TabIndex = 74;
            groupBox61.TabStop = false;
            groupBox61.Text = "Sandstom Data";
            // 
            // sandstormFrequencyNUD
            // 
            sandstormFrequencyNUD.BackColor = Color.FromArgb(60, 63, 65);
            sandstormFrequencyNUD.DecimalPlaces = 2;
            sandstormFrequencyNUD.ForeColor = SystemColors.Control;
            sandstormFrequencyNUD.Location = new Point(250, 22);
            sandstormFrequencyNUD.Margin = new Padding(4, 3, 4, 3);
            sandstormFrequencyNUD.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            sandstormFrequencyNUD.Name = "sandstormFrequencyNUD";
            sandstormFrequencyNUD.Size = new Size(79, 23);
            sandstormFrequencyNUD.TabIndex = 68;
            sandstormFrequencyNUD.TextAlign = HorizontalAlignment.Center;
            sandstormFrequencyNUD.ValueChanged += sandstormFrequencyNUD_ValueChanged;
            // 
            // label138
            // 
            label138.AutoSize = true;
            label138.ForeColor = SystemColors.Control;
            label138.Location = new Point(9, 27);
            label138.Margin = new Padding(4, 0, 4, 0);
            label138.Name = "label138";
            label138.Size = new Size(122, 15);
            label138.TabIndex = 67;
            label138.Text = "Sandstorm Frequency";
            // 
            // cfggameplaySandstorm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox61);
            ForeColor = SystemColors.Control;
            Name = "cfggameplaySandstorm";
            Size = new Size(344, 64);
            groupBox61.ResumeLayout(false);
            groupBox61.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)sandstormFrequencyNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox61;
        private NumericUpDown protectionResetTimeNUD;
        private Label label1;
        private NumericUpDown protectionTimeNUD;
        private Label label139;
        private NumericUpDown protectionCountStageTwoNUD;
        private Label label132;
        private NumericUpDown sandstormFrequencyNUD;
        private Label label138;
    }
}
