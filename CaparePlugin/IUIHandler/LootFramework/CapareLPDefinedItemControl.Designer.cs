namespace CaparePlugin
{
    partial class CapareLPDefinedItemControl
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
            DefineNameL = new Label();
            DefineNameTB = new TextBox();
            ItemL = new Label();
            ItemTB = new TextBox();
            SpawnExactL = new Label();
            SpawnExactCB = new CheckBox();
            groupBox1.SuspendLayout();

            SuspendLayout();
            //
            // groupBox1
            //
            groupBox1.Controls.Add(DefineNameL);
            groupBox1.Controls.Add(DefineNameTB);
            groupBox1.Controls.Add(ItemL);
            groupBox1.Controls.Add(ItemTB);
            groupBox1.Controls.Add(SpawnExactL);
            groupBox1.Controls.Add(SpawnExactCB);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(435, 115);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Defined Item";
            //
            // DefineNameL
            //
            DefineNameL.AutoSize = true;
            DefineNameL.ForeColor = Color.FromArgb(220, 220, 220);
            DefineNameL.Location = new Point(11, 25);
            DefineNameL.Margin = new Padding(4, 0, 4, 0);
            DefineNameL.Name = "DefineNameL";
            DefineNameL.Size = new Size(77, 15);
            DefineNameL.TabIndex = 0;
            DefineNameL.Text = "Define Name";
            //
            // DefineNameTB
            //
            DefineNameTB.BackColor = Color.FromArgb(60, 63, 65);
            DefineNameTB.ForeColor = SystemColors.Control;
            DefineNameTB.Location = new Point(190, 22);
            DefineNameTB.Margin = new Padding(4, 3, 4, 3);
            DefineNameTB.Name = "DefineNameTB";
            DefineNameTB.Size = new Size(230, 23);
            DefineNameTB.TabIndex = 1;
            DefineNameTB.TextChanged += DefineNameTB_TextChanged;
            //
            // ItemL
            //
            ItemL.AutoSize = true;
            ItemL.ForeColor = Color.FromArgb(220, 220, 220);
            ItemL.Location = new Point(11, 54);
            ItemL.Margin = new Padding(4, 0, 4, 0);
            ItemL.Name = "ItemL";
            ItemL.Size = new Size(30, 15);
            ItemL.TabIndex = 2;
            ItemL.Text = "Item";
            //
            // ItemTB
            //
            ItemTB.BackColor = Color.FromArgb(60, 63, 65);
            ItemTB.ForeColor = SystemColors.Control;
            ItemTB.Location = new Point(190, 51);
            ItemTB.Margin = new Padding(4, 3, 4, 3);
            ItemTB.Name = "ItemTB";
            ItemTB.Size = new Size(230, 23);
            ItemTB.TabIndex = 3;
            ItemTB.TextChanged += ItemTB_TextChanged;
            //
            // SpawnExactL
            //
            SpawnExactL.AutoSize = true;
            SpawnExactL.ForeColor = Color.FromArgb(220, 220, 220);
            SpawnExactL.Location = new Point(11, 83);
            SpawnExactL.Margin = new Padding(4, 0, 4, 0);
            SpawnExactL.Name = "SpawnExactL";
            SpawnExactL.Size = new Size(77, 15);
            SpawnExactL.TabIndex = 4;
            SpawnExactL.Text = "Spawn Exact";
            //
            // SpawnExactCB
            //
            SpawnExactCB.AutoSize = true;
            SpawnExactCB.Location = new Point(190, 83);
            SpawnExactCB.Margin = new Padding(4, 3, 4, 3);
            SpawnExactCB.Name = "SpawnExactCB";
            SpawnExactCB.Size = new Size(15, 14);
            SpawnExactCB.TabIndex = 5;
            SpawnExactCB.UseVisualStyleBackColor = true;
            SpawnExactCB.CheckedChanged += SpawnExactCB_CheckedChanged;
            //
            // CapareLPDefinedItemControl
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "CapareLPDefinedItemControl";
            Size = new Size(435, 121);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();

            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label DefineNameL;
        private TextBox DefineNameTB;
        private Label ItemL;
        private TextBox ItemTB;
        private Label SpawnExactL;
        private CheckBox SpawnExactCB;
    }
}
