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
            button1 = new Button();
            button4 = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            TypesCollectionGB.SuspendLayout();
            SuspendLayout();
            // 
            // TypesCollectionGB
            // 
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
            // button1
            // 
            button1.BackColor = Color.FromArgb(60, 63, 65);
            button1.Location = new Point(15, 151);
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
            button4.Location = new Point(15, 71);
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
            ResumeLayout(false);
        }

        #endregion

        private GroupBox TypesCollectionGB;
        private TextBox textBox1;
        private Label label1;
        private Button button4;
        private Button button1;
    }
}
