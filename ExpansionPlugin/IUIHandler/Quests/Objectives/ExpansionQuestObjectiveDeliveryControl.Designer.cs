namespace ExpansionPlugin
{
    partial class ExpansionQuestObjectiveDeliveryControl
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
            groupBox9 = new GroupBox();
            ObjectivesDeliveryClassnameTB = new TextBox();
            darkButton56 = new Button();
            darkLabel165 = new Label();
            darkLabel117 = new Label();
            ObjectivesDeliveryMinQuantityPerentNUD = new NumericUpDown();
            ObjectivesDeliveryAmountNUD = new NumericUpDown();
            darkLabel162 = new Label();
            darkLabel116 = new Label();
            ObjectivesDeliveryQuantityPercentNUD = new NumericUpDown();
            groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ObjectivesDeliveryMinQuantityPerentNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ObjectivesDeliveryAmountNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ObjectivesDeliveryQuantityPercentNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(ObjectivesDeliveryClassnameTB);
            groupBox9.Controls.Add(darkButton56);
            groupBox9.Controls.Add(darkLabel165);
            groupBox9.Controls.Add(darkLabel117);
            groupBox9.Controls.Add(ObjectivesDeliveryMinQuantityPerentNUD);
            groupBox9.Controls.Add(ObjectivesDeliveryAmountNUD);
            groupBox9.Controls.Add(darkLabel162);
            groupBox9.Controls.Add(darkLabel116);
            groupBox9.Controls.Add(ObjectivesDeliveryQuantityPercentNUD);
            groupBox9.ForeColor = SystemColors.Control;
            groupBox9.Location = new Point(0, 0);
            groupBox9.Margin = new Padding(4, 3, 4, 3);
            groupBox9.Name = "groupBox9";
            groupBox9.Padding = new Padding(4, 3, 4, 3);
            groupBox9.Size = new Size(534, 149);
            groupBox9.TabIndex = 345;
            groupBox9.TabStop = false;
            groupBox9.Text = "Items";
            // 
            // ObjectivesDeliveryClassnameTB
            // 
            ObjectivesDeliveryClassnameTB.BackColor = Color.FromArgb(60, 63, 65);
            ObjectivesDeliveryClassnameTB.ForeColor = SystemColors.Control;
            ObjectivesDeliveryClassnameTB.Location = new Point(156, 52);
            ObjectivesDeliveryClassnameTB.Margin = new Padding(4, 3, 4, 3);
            ObjectivesDeliveryClassnameTB.Name = "ObjectivesDeliveryClassnameTB";
            ObjectivesDeliveryClassnameTB.ReadOnly = true;
            ObjectivesDeliveryClassnameTB.Size = new Size(333, 23);
            ObjectivesDeliveryClassnameTB.TabIndex = 327;
            ObjectivesDeliveryClassnameTB.TextChanged += ObjectivesDeliveryClassnameTB_TextChanged;
            // 
            // darkButton56
            // 
            darkButton56.FlatStyle = FlatStyle.Flat;
            darkButton56.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            darkButton56.Location = new Point(497, 52);
            darkButton56.Name = "darkButton56";
            darkButton56.Size = new Size(23, 23);
            darkButton56.TabIndex = 330;
            darkButton56.Text = "+";
            darkButton56.Click += darkButton56_Click;
            // 
            // darkLabel165
            // 
            darkLabel165.AutoSize = true;
            darkLabel165.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel165.Location = new Point(15, 115);
            darkLabel165.Margin = new Padding(4, 0, 4, 0);
            darkLabel165.Name = "darkLabel165";
            darkLabel165.Size = new Size(120, 15);
            darkLabel165.TabIndex = 343;
            darkLabel165.Text = "Min Quantity Percent";
            // 
            // darkLabel117
            // 
            darkLabel117.AutoSize = true;
            darkLabel117.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel117.Location = new Point(15, 55);
            darkLabel117.Margin = new Padding(4, 0, 4, 0);
            darkLabel117.Name = "darkLabel117";
            darkLabel117.Size = new Size(64, 15);
            darkLabel117.TabIndex = 326;
            darkLabel117.Text = "Classname";
            // 
            // ObjectivesDeliveryMinQuantityPerentNUD
            // 
            ObjectivesDeliveryMinQuantityPerentNUD.BackColor = Color.FromArgb(60, 63, 65);
            ObjectivesDeliveryMinQuantityPerentNUD.ForeColor = SystemColors.Control;
            ObjectivesDeliveryMinQuantityPerentNUD.Location = new Point(156, 113);
            ObjectivesDeliveryMinQuantityPerentNUD.Margin = new Padding(4, 3, 4, 3);
            ObjectivesDeliveryMinQuantityPerentNUD.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            ObjectivesDeliveryMinQuantityPerentNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            ObjectivesDeliveryMinQuantityPerentNUD.Name = "ObjectivesDeliveryMinQuantityPerentNUD";
            ObjectivesDeliveryMinQuantityPerentNUD.Size = new Size(124, 23);
            ObjectivesDeliveryMinQuantityPerentNUD.TabIndex = 342;
            ObjectivesDeliveryMinQuantityPerentNUD.TextAlign = HorizontalAlignment.Center;
            ObjectivesDeliveryMinQuantityPerentNUD.ValueChanged += ObjectivesDeliveryMinQuantityPerentNUD_ValueChanged;
            // 
            // ObjectivesDeliveryAmountNUD
            // 
            ObjectivesDeliveryAmountNUD.BackColor = Color.FromArgb(60, 63, 65);
            ObjectivesDeliveryAmountNUD.ForeColor = SystemColors.Control;
            ObjectivesDeliveryAmountNUD.Location = new Point(156, 23);
            ObjectivesDeliveryAmountNUD.Margin = new Padding(4, 3, 4, 3);
            ObjectivesDeliveryAmountNUD.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            ObjectivesDeliveryAmountNUD.Name = "ObjectivesDeliveryAmountNUD";
            ObjectivesDeliveryAmountNUD.Size = new Size(124, 23);
            ObjectivesDeliveryAmountNUD.TabIndex = 328;
            ObjectivesDeliveryAmountNUD.TextAlign = HorizontalAlignment.Center;
            ObjectivesDeliveryAmountNUD.ValueChanged += ObjectivesDeliveryAmountNUD_ValueChanged;
            // 
            // darkLabel162
            // 
            darkLabel162.AutoSize = true;
            darkLabel162.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel162.Location = new Point(15, 85);
            darkLabel162.Margin = new Padding(4, 0, 4, 0);
            darkLabel162.Name = "darkLabel162";
            darkLabel162.Size = new Size(96, 15);
            darkLabel162.TabIndex = 341;
            darkLabel162.Text = "Quantity Percent";
            // 
            // darkLabel116
            // 
            darkLabel116.AutoSize = true;
            darkLabel116.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel116.Location = new Point(15, 25);
            darkLabel116.Margin = new Padding(4, 0, 4, 0);
            darkLabel116.Name = "darkLabel116";
            darkLabel116.Size = new Size(51, 15);
            darkLabel116.TabIndex = 329;
            darkLabel116.Text = "Amount";
            // 
            // ObjectivesDeliveryQuantityPercentNUD
            // 
            ObjectivesDeliveryQuantityPercentNUD.BackColor = Color.FromArgb(60, 63, 65);
            ObjectivesDeliveryQuantityPercentNUD.ForeColor = SystemColors.Control;
            ObjectivesDeliveryQuantityPercentNUD.Location = new Point(156, 83);
            ObjectivesDeliveryQuantityPercentNUD.Margin = new Padding(4, 3, 4, 3);
            ObjectivesDeliveryQuantityPercentNUD.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            ObjectivesDeliveryQuantityPercentNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            ObjectivesDeliveryQuantityPercentNUD.Name = "ObjectivesDeliveryQuantityPercentNUD";
            ObjectivesDeliveryQuantityPercentNUD.Size = new Size(124, 23);
            ObjectivesDeliveryQuantityPercentNUD.TabIndex = 340;
            ObjectivesDeliveryQuantityPercentNUD.TextAlign = HorizontalAlignment.Center;
            ObjectivesDeliveryQuantityPercentNUD.ValueChanged += ObjectivesDeliveryQuantityPercentNUD_ValueChanged;
            // 
            // ExpansionQuestObjectiveDeliveryControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox9);
            ForeColor = SystemColors.Control;
            Name = "ExpansionQuestObjectiveDeliveryControl";
            Size = new Size(534, 149);
            groupBox9.ResumeLayout(false);
            groupBox9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ObjectivesDeliveryMinQuantityPerentNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)ObjectivesDeliveryAmountNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)ObjectivesDeliveryQuantityPercentNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox9;
        private TextBox ObjectivesDeliveryClassnameTB;
        private Button darkButton56;
        private Label darkLabel165;
        private Label darkLabel117;
        private NumericUpDown ObjectivesDeliveryMinQuantityPerentNUD;
        private NumericUpDown ObjectivesDeliveryAmountNUD;
        private Label darkLabel162;
        private Label darkLabel116;
        private NumericUpDown ObjectivesDeliveryQuantityPercentNUD;
    }
}
