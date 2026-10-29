namespace CaparePlugin
{
    partial class CapareSalvageYieldRateControl
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
            BasketL = new Label();
            BasketComboBox = new ComboBox();
            RateL = new Label();
            RateNUD = new NumericUpDown();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)RateNUD).BeginInit();
            SuspendLayout();
            //
            // groupBox1
            //
            groupBox1.Controls.Add(BasketL);
            groupBox1.Controls.Add(BasketComboBox);
            groupBox1.Controls.Add(RateL);
            groupBox1.Controls.Add(RateNUD);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(435, 86);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Yield Rate";
            //
            // BasketL
            //
            BasketL.AutoSize = true;
            BasketL.ForeColor = Color.FromArgb(220, 220, 220);
            BasketL.Location = new Point(11, 25);
            BasketL.Margin = new Padding(4, 0, 4, 0);
            BasketL.Name = "BasketL";
            BasketL.Size = new Size(42, 15);
            BasketL.TabIndex = 0;
            BasketL.Text = "Basket";
            //
            // BasketComboBox
            //
            BasketComboBox.BackColor = Color.FromArgb(60, 63, 65);
            BasketComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            BasketComboBox.FlatStyle = FlatStyle.Flat;
            BasketComboBox.ForeColor = SystemColors.Control;
            BasketComboBox.FormattingEnabled = true;
            BasketComboBox.Location = new Point(190, 22);
            BasketComboBox.Margin = new Padding(4, 3, 4, 3);
            BasketComboBox.Name = "BasketComboBox";
            BasketComboBox.Size = new Size(230, 23);
            BasketComboBox.TabIndex = 1;
            BasketComboBox.TextChanged += BasketComboBox_TextChanged;
            //
            // RateL
            //
            RateL.AutoSize = true;
            RateL.ForeColor = Color.FromArgb(220, 220, 220);
            RateL.Location = new Point(11, 54);
            RateL.Margin = new Padding(4, 0, 4, 0);
            RateL.Name = "RateL";
            RateL.Size = new Size(98, 15);
            RateL.TabIndex = 2;
            RateL.Text = "Scrap Per Gram";
            //
            // RateNUD
            //
            RateNUD.BackColor = Color.FromArgb(60, 63, 65);
            RateNUD.DecimalPlaces = 3;
            RateNUD.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            RateNUD.ForeColor = SystemColors.Control;
            RateNUD.Location = new Point(190, 51);
            RateNUD.Margin = new Padding(4, 3, 4, 3);
            RateNUD.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            RateNUD.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            RateNUD.Name = "RateNUD";
            RateNUD.Size = new Size(230, 23);
            RateNUD.TabIndex = 3;
            RateNUD.TextAlign = HorizontalAlignment.Center;
            RateNUD.ValueChanged += RateNUD_ValueChanged;
            //
            // CapareSalvageYieldRateControl
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "CapareSalvageYieldRateControl";
            Size = new Size(435, 92);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)RateNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label BasketL;
        private ComboBox BasketComboBox;
        private Label RateL;
        private NumericUpDown RateNUD;
    }
}
