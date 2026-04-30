namespace ExpansionPlugin
{
    partial class ExpansionQuestItemConfigControl
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
            darkButton56 = new Button();
            QuestQuestItemsAmountNUD = new NumericUpDown();
            label1 = new Label();
            darkLabel32 = new Label();
            QuestItemClassnameTB = new TextBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)QuestQuestItemsAmountNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(darkButton56);
            groupBox1.Controls.Add(QuestQuestItemsAmountNUD);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(darkLabel32);
            groupBox1.Controls.Add(QuestItemClassnameTB);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(397, 88);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Items Info";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // darkButton56
            // 
            darkButton56.FlatStyle = FlatStyle.Flat;
            darkButton56.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            darkButton56.Location = new Point(366, 22);
            darkButton56.Name = "darkButton56";
            darkButton56.Size = new Size(23, 23);
            darkButton56.TabIndex = 331;
            darkButton56.Text = "+";
            darkButton56.Click += darkButton56_Click;
            // 
            // QuestQuestItemsAmountNUD
            // 
            QuestQuestItemsAmountNUD.BackColor = Color.FromArgb(60, 63, 65);
            QuestQuestItemsAmountNUD.ForeColor = SystemColors.Control;
            QuestQuestItemsAmountNUD.Location = new Point(103, 53);
            QuestQuestItemsAmountNUD.Margin = new Padding(4, 3, 4, 3);
            QuestQuestItemsAmountNUD.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            QuestQuestItemsAmountNUD.Name = "QuestQuestItemsAmountNUD";
            QuestQuestItemsAmountNUD.Size = new Size(140, 23);
            QuestQuestItemsAmountNUD.TabIndex = 111;
            QuestQuestItemsAmountNUD.TextAlign = HorizontalAlignment.Center;
            QuestQuestItemsAmountNUD.ValueChanged += QuestQuestItemsAmountNUD_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(220, 220, 220);
            label1.Location = new Point(15, 55);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 110;
            label1.Text = "Amount";
            // 
            // darkLabel32
            // 
            darkLabel32.AutoSize = true;
            darkLabel32.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel32.Location = new Point(15, 25);
            darkLabel32.Margin = new Padding(4, 0, 4, 0);
            darkLabel32.Name = "darkLabel32";
            darkLabel32.Size = new Size(64, 15);
            darkLabel32.TabIndex = 109;
            darkLabel32.Text = "Classname";
            // 
            // QuestItemClassnameTB
            // 
            QuestItemClassnameTB.BackColor = Color.FromArgb(60, 63, 65);
            QuestItemClassnameTB.ForeColor = SystemColors.Control;
            QuestItemClassnameTB.Location = new Point(103, 22);
            QuestItemClassnameTB.Margin = new Padding(4, 3, 4, 3);
            QuestItemClassnameTB.Name = "QuestItemClassnameTB";
            QuestItemClassnameTB.Size = new Size(256, 23);
            QuestItemClassnameTB.TabIndex = 108;
            QuestItemClassnameTB.TextChanged += QuestItemClassnameTB_TextChanged;
            // 
            // ExpansionQuestItemConfigControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "ExpansionQuestItemConfigControl";
            Size = new Size(397, 88);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)QuestQuestItemsAmountNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label1;
        private Label darkLabel32;
        private TextBox QuestItemClassnameTB;
        private NumericUpDown QuestQuestItemsAmountNUD;
        private Button darkButton56;
    }
}
