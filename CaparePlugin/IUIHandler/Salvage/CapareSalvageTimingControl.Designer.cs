namespace CaparePlugin
{
    partial class CapareSalvageTimingControl
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
            BaseSecondsL = new Label();
            BaseSecondsNUD = new NumericUpDown();
            SecondsPerScrapL = new Label();
            SecondsPerScrapNUD = new NumericUpDown();
            SecondsPerKgL = new Label();
            SecondsPerKgNUD = new NumericUpDown();
            MinSecondsL = new Label();
            MinSecondsNUD = new NumericUpDown();
            MaxSecondsL = new Label();
            MaxSecondsNUD = new NumericUpDown();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)BaseSecondsNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SecondsPerScrapNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SecondsPerKgNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MinSecondsNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MaxSecondsNUD).BeginInit();
            SuspendLayout();
            //
            // groupBox1
            //
            groupBox1.Controls.Add(BaseSecondsL);
            groupBox1.Controls.Add(BaseSecondsNUD);
            groupBox1.Controls.Add(SecondsPerScrapL);
            groupBox1.Controls.Add(SecondsPerScrapNUD);
            groupBox1.Controls.Add(SecondsPerKgL);
            groupBox1.Controls.Add(SecondsPerKgNUD);
            groupBox1.Controls.Add(MinSecondsL);
            groupBox1.Controls.Add(MinSecondsNUD);
            groupBox1.Controls.Add(MaxSecondsL);
            groupBox1.Controls.Add(MaxSecondsNUD);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(435, 173);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "Salvage Timing";
            //
            // BaseSecondsL
            //
            BaseSecondsL.AutoSize = true;
            BaseSecondsL.ForeColor = Color.FromArgb(220, 220, 220);
            BaseSecondsL.Location = new Point(11, 25);
            BaseSecondsL.Margin = new Padding(4, 0, 4, 0);
            BaseSecondsL.Name = "BaseSecondsL";
            BaseSecondsL.Size = new Size(84, 15);
            BaseSecondsL.TabIndex = 0;
            BaseSecondsL.Text = "Base Seconds";
            //
            // BaseSecondsNUD
            //
            BaseSecondsNUD.BackColor = Color.FromArgb(60, 63, 65);
            BaseSecondsNUD.DecimalPlaces = 2;
            BaseSecondsNUD.ForeColor = SystemColors.Control;
            BaseSecondsNUD.Location = new Point(190, 22);
            BaseSecondsNUD.Margin = new Padding(4, 3, 4, 3);
            BaseSecondsNUD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            BaseSecondsNUD.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            BaseSecondsNUD.Name = "BaseSecondsNUD";
            BaseSecondsNUD.Size = new Size(230, 23);
            BaseSecondsNUD.TabIndex = 1;
            BaseSecondsNUD.TextAlign = HorizontalAlignment.Center;
            BaseSecondsNUD.ValueChanged += BaseSecondsNUD_ValueChanged;
            //
            // SecondsPerScrapL
            //
            SecondsPerScrapL.AutoSize = true;
            SecondsPerScrapL.ForeColor = Color.FromArgb(220, 220, 220);
            SecondsPerScrapL.Location = new Point(11, 54);
            SecondsPerScrapL.Margin = new Padding(4, 0, 4, 0);
            SecondsPerScrapL.Name = "SecondsPerScrapL";
            SecondsPerScrapL.Size = new Size(119, 15);
            SecondsPerScrapL.TabIndex = 2;
            SecondsPerScrapL.Text = "Seconds Per Scrap";
            //
            // SecondsPerScrapNUD
            //
            SecondsPerScrapNUD.BackColor = Color.FromArgb(60, 63, 65);
            SecondsPerScrapNUD.DecimalPlaces = 3;
            SecondsPerScrapNUD.ForeColor = SystemColors.Control;
            SecondsPerScrapNUD.Location = new Point(190, 51);
            SecondsPerScrapNUD.Margin = new Padding(4, 3, 4, 3);
            SecondsPerScrapNUD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            SecondsPerScrapNUD.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            SecondsPerScrapNUD.Name = "SecondsPerScrapNUD";
            SecondsPerScrapNUD.Size = new Size(230, 23);
            SecondsPerScrapNUD.TabIndex = 3;
            SecondsPerScrapNUD.TextAlign = HorizontalAlignment.Center;
            SecondsPerScrapNUD.ValueChanged += SecondsPerScrapNUD_ValueChanged;
            //
            // SecondsPerKgL
            //
            SecondsPerKgL.AutoSize = true;
            SecondsPerKgL.ForeColor = Color.FromArgb(220, 220, 220);
            SecondsPerKgL.Location = new Point(11, 83);
            SecondsPerKgL.Margin = new Padding(4, 0, 4, 0);
            SecondsPerKgL.Name = "SecondsPerKgL";
            SecondsPerKgL.Size = new Size(98, 15);
            SecondsPerKgL.TabIndex = 4;
            SecondsPerKgL.Text = "Seconds Per Kg";
            //
            // SecondsPerKgNUD
            //
            SecondsPerKgNUD.BackColor = Color.FromArgb(60, 63, 65);
            SecondsPerKgNUD.DecimalPlaces = 2;
            SecondsPerKgNUD.ForeColor = SystemColors.Control;
            SecondsPerKgNUD.Location = new Point(190, 80);
            SecondsPerKgNUD.Margin = new Padding(4, 3, 4, 3);
            SecondsPerKgNUD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            SecondsPerKgNUD.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            SecondsPerKgNUD.Name = "SecondsPerKgNUD";
            SecondsPerKgNUD.Size = new Size(230, 23);
            SecondsPerKgNUD.TabIndex = 5;
            SecondsPerKgNUD.TextAlign = HorizontalAlignment.Center;
            SecondsPerKgNUD.ValueChanged += SecondsPerKgNUD_ValueChanged;
            //
            // MinSecondsL
            //
            MinSecondsL.AutoSize = true;
            MinSecondsL.ForeColor = Color.FromArgb(220, 220, 220);
            MinSecondsL.Location = new Point(11, 112);
            MinSecondsL.Margin = new Padding(4, 0, 4, 0);
            MinSecondsL.Name = "MinSecondsL";
            MinSecondsL.Size = new Size(77, 15);
            MinSecondsL.TabIndex = 6;
            MinSecondsL.Text = "Min Seconds";
            //
            // MinSecondsNUD
            //
            MinSecondsNUD.BackColor = Color.FromArgb(60, 63, 65);
            MinSecondsNUD.DecimalPlaces = 2;
            MinSecondsNUD.ForeColor = SystemColors.Control;
            MinSecondsNUD.Location = new Point(190, 109);
            MinSecondsNUD.Margin = new Padding(4, 3, 4, 3);
            MinSecondsNUD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            MinSecondsNUD.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            MinSecondsNUD.Name = "MinSecondsNUD";
            MinSecondsNUD.Size = new Size(230, 23);
            MinSecondsNUD.TabIndex = 7;
            MinSecondsNUD.TextAlign = HorizontalAlignment.Center;
            MinSecondsNUD.ValueChanged += MinSecondsNUD_ValueChanged;
            //
            // MaxSecondsL
            //
            MaxSecondsL.AutoSize = true;
            MaxSecondsL.ForeColor = Color.FromArgb(220, 220, 220);
            MaxSecondsL.Location = new Point(11, 141);
            MaxSecondsL.Margin = new Padding(4, 0, 4, 0);
            MaxSecondsL.Name = "MaxSecondsL";
            MaxSecondsL.Size = new Size(77, 15);
            MaxSecondsL.TabIndex = 8;
            MaxSecondsL.Text = "Max Seconds";
            //
            // MaxSecondsNUD
            //
            MaxSecondsNUD.BackColor = Color.FromArgb(60, 63, 65);
            MaxSecondsNUD.DecimalPlaces = 2;
            MaxSecondsNUD.ForeColor = SystemColors.Control;
            MaxSecondsNUD.Location = new Point(190, 138);
            MaxSecondsNUD.Margin = new Padding(4, 3, 4, 3);
            MaxSecondsNUD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            MaxSecondsNUD.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            MaxSecondsNUD.Name = "MaxSecondsNUD";
            MaxSecondsNUD.Size = new Size(230, 23);
            MaxSecondsNUD.TabIndex = 9;
            MaxSecondsNUD.TextAlign = HorizontalAlignment.Center;
            MaxSecondsNUD.ValueChanged += MaxSecondsNUD_ValueChanged;
            //
            // CapareSalvageTimingControl
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "CapareSalvageTimingControl";
            Size = new Size(435, 179);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)BaseSecondsNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)SecondsPerScrapNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)SecondsPerKgNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)MinSecondsNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)MaxSecondsNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label BaseSecondsL;
        private NumericUpDown BaseSecondsNUD;
        private Label SecondsPerScrapL;
        private NumericUpDown SecondsPerScrapNUD;
        private Label SecondsPerKgL;
        private NumericUpDown SecondsPerKgNUD;
        private Label MinSecondsL;
        private NumericUpDown MinSecondsNUD;
        private Label MaxSecondsL;
        private NumericUpDown MaxSecondsNUD;
    }
}
