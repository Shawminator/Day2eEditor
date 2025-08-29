namespace EconomyPlugin
{
    partial class SpawnabletypesTagsControl
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
            SpawnabletypestagGB = new GroupBox();
            textBox2 = new TextBox();
            groupBox24 = new GroupBox();
            comboBox5 = new ComboBox();
            darkButton29 = new Button();
            SpawnabletypestagGB.SuspendLayout();
            groupBox24.SuspendLayout();
            SuspendLayout();
            // 
            // SpawnabletypestagGB
            // 
            SpawnabletypestagGB.Controls.Add(textBox2);
            SpawnabletypestagGB.Controls.Add(groupBox24);
            SpawnabletypestagGB.ForeColor = SystemColors.ButtonHighlight;
            SpawnabletypestagGB.Location = new Point(0, 0);
            SpawnabletypestagGB.Margin = new Padding(4, 3, 4, 3);
            SpawnabletypestagGB.Name = "SpawnabletypestagGB";
            SpawnabletypestagGB.Padding = new Padding(4, 3, 4, 3);
            SpawnabletypestagGB.Size = new Size(282, 178);
            SpawnabletypestagGB.TabIndex = 65;
            SpawnabletypestagGB.TabStop = false;
            SpawnabletypestagGB.Text = "Tags";
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(60, 63, 65);
            textBox2.ForeColor = SystemColors.Control;
            textBox2.Location = new Point(10, 22);
            textBox2.Margin = new Padding(4, 3, 4, 3);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(254, 23);
            textBox2.TabIndex = 8;
            // 
            // groupBox24
            // 
            groupBox24.Controls.Add(comboBox5);
            groupBox24.Controls.Add(darkButton29);
            groupBox24.ForeColor = SystemColors.ButtonHighlight;
            groupBox24.Location = new Point(10, 52);
            groupBox24.Margin = new Padding(4, 3, 4, 3);
            groupBox24.Name = "groupBox24";
            groupBox24.Padding = new Padding(4, 3, 4, 3);
            groupBox24.Size = new Size(254, 115);
            groupBox24.TabIndex = 7;
            groupBox24.TabStop = false;
            groupBox24.Text = "Change Tag";
            // 
            // comboBox5
            // 
            comboBox5.BackColor = Color.FromArgb(60, 63, 65);
            comboBox5.ForeColor = SystemColors.Control;
            comboBox5.FormattingEnabled = true;
            comboBox5.Location = new Point(12, 29);
            comboBox5.Margin = new Padding(4, 3, 4, 3);
            comboBox5.Name = "comboBox5";
            comboBox5.Size = new Size(235, 23);
            comboBox5.TabIndex = 1;
            // 
            // darkButton29
            // 
            darkButton29.FlatStyle = FlatStyle.Flat;
            darkButton29.Location = new Point(9, 60);
            darkButton29.Margin = new Padding(4, 3, 4, 3);
            darkButton29.Name = "darkButton29";
            darkButton29.Size = new Size(238, 31);
            darkButton29.TabIndex = 5;
            darkButton29.Text = "Change Tag";
            darkButton29.Click += darkButton29_Click;
            // 
            // SpawnabletypesTagsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(SpawnabletypestagGB);
            ForeColor = SystemColors.Control;
            Name = "SpawnabletypesTagsControl";
            Size = new Size(291, 189);
            SpawnabletypestagGB.ResumeLayout(false);
            SpawnabletypestagGB.PerformLayout();
            groupBox24.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox SpawnabletypestagGB;
        private TextBox textBox2;
        private GroupBox groupBox24;
        private ComboBox comboBox5;
        private Button darkButton29;
    }
}
