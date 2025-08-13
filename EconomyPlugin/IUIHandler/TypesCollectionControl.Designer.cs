namespace EconomyPlugin
{
    partial class TypesCollectionControl
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
            TypesCollectionGB = new GroupBox();
            ChangeMinCB = new CheckBox();
            button5 = new Button();
            CollectionCustomNUD = new NumericUpDown();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            button4 = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            TypesCollectionGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)CollectionCustomNUD).BeginInit();
            SuspendLayout();
            // 
            // TypesCollectionGB
            // 
            TypesCollectionGB.Controls.Add(ChangeMinCB);
            TypesCollectionGB.Controls.Add(button5);
            TypesCollectionGB.Controls.Add(CollectionCustomNUD);
            TypesCollectionGB.Controls.Add(button3);
            TypesCollectionGB.Controls.Add(button2);
            TypesCollectionGB.Controls.Add(button1);
            TypesCollectionGB.Controls.Add(button4);
            TypesCollectionGB.Controls.Add(textBox1);
            TypesCollectionGB.Controls.Add(label1);
            TypesCollectionGB.Dock = DockStyle.Fill;
            TypesCollectionGB.ForeColor = SystemColors.Control;
            TypesCollectionGB.Location = new Point(0, 0);
            TypesCollectionGB.Name = "TypesCollectionGB";
            TypesCollectionGB.Size = new Size(652, 513);
            TypesCollectionGB.TabIndex = 0;
            TypesCollectionGB.TabStop = false;
            TypesCollectionGB.Text = "Types Collection";
            // 
            // ChangeMinCB
            // 
            ChangeMinCB.AutoSize = true;
            ChangeMinCB.Location = new Point(51, 130);
            ChangeMinCB.Name = "ChangeMinCB";
            ChangeMinCB.RightToLeft = RightToLeft.Yes;
            ChangeMinCB.Size = new Size(123, 19);
            ChangeMinCB.TabIndex = 31;
            ChangeMinCB.Text = "Change Minimum";
            ChangeMinCB.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.BackColor = Color.FromArgb(60, 63, 65);
            button5.Location = new Point(15, 155);
            button5.Name = "button5";
            button5.Size = new Size(159, 23);
            button5.TabIndex = 30;
            button5.Text = "Set to Custom Value";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // CollectionCustomNUD
            // 
            CollectionCustomNUD.BackColor = Color.FromArgb(60, 63, 65);
            CollectionCustomNUD.ForeColor = SystemColors.Control;
            CollectionCustomNUD.Location = new Point(15, 101);
            CollectionCustomNUD.Maximum = new decimal(new int[] { 50000, 0, 0, 0 });
            CollectionCustomNUD.Name = "CollectionCustomNUD";
            CollectionCustomNUD.Size = new Size(159, 23);
            CollectionCustomNUD.TabIndex = 29;
            CollectionCustomNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(60, 63, 65);
            button3.Location = new Point(15, 203);
            button3.Name = "button3";
            button3.Size = new Size(159, 23);
            button3.TabIndex = 28;
            button3.Text = "Sync Min to Nom";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(60, 63, 65);
            button2.Location = new Point(15, 232);
            button2.Name = "button2";
            button2.Size = new Size(159, 23);
            button2.TabIndex = 27;
            button2.Text = "Sync Nom to Min";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(60, 63, 65);
            button1.Location = new Point(15, 284);
            button1.Name = "button1";
            button1.Size = new Size(159, 23);
            button1.TabIndex = 26;
            button1.Text = "Update Types file";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button4
            // 
            button4.BackColor = Color.FromArgb(60, 63, 65);
            button4.Location = new Point(15, 56);
            button4.Name = "button4";
            button4.Size = new Size(159, 23);
            button4.TabIndex = 25;
            button4.Text = "Zero all Entries";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(60, 63, 65);
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.ForeColor = SystemColors.ButtonFace;
            textBox1.Location = new Point(101, 34);
            textBox1.Margin = new Padding(4, 3, 4, 3);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(175, 16);
            textBox1.TabIndex = 3;
            textBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 34);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(70, 15);
            label1.TabIndex = 2;
            label1.Text = "Catgegory:-";
            // 
            // TypesCollectionControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(TypesCollectionGB);
            ForeColor = SystemColors.Control;
            Name = "TypesCollectionControl";
            Size = new Size(652, 513);
            TypesCollectionGB.ResumeLayout(false);
            TypesCollectionGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)CollectionCustomNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox TypesCollectionGB;
        private TextBox textBox1;
        private Label label1;
        private Button button4;
        private Button button1;
        private Button button3;
        private Button button2;
        private Button button5;
        private NumericUpDown CollectionCustomNUD;
        private CheckBox ChangeMinCB;
    }
}
