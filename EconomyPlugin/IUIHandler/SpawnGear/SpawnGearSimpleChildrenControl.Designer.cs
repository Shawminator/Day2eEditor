namespace EconomyPlugin
{
    partial class SpawnGearSimpleChildrenControl
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
            simpleChildrenTypesGB = new GroupBox();
            darkButton73 = new Button();
            darkButton74 = new Button();
            simpleChildrenTypesLB = new ListBox();
            simpleChildrenTypesGB.SuspendLayout();
            SuspendLayout();
            // 
            // simpleChildrenTypesGB
            // 
            simpleChildrenTypesGB.Controls.Add(darkButton73);
            simpleChildrenTypesGB.Controls.Add(darkButton74);
            simpleChildrenTypesGB.Controls.Add(simpleChildrenTypesLB);
            simpleChildrenTypesGB.ForeColor = SystemColors.Control;
            simpleChildrenTypesGB.Location = new Point(0, 0);
            simpleChildrenTypesGB.Margin = new Padding(4, 3, 4, 3);
            simpleChildrenTypesGB.Name = "simpleChildrenTypesGB";
            simpleChildrenTypesGB.Padding = new Padding(4, 3, 4, 3);
            simpleChildrenTypesGB.Size = new Size(324, 410);
            simpleChildrenTypesGB.TabIndex = 234;
            simpleChildrenTypesGB.TabStop = false;
            simpleChildrenTypesGB.Text = "Simple Children Types";
            // 
            // darkButton73
            // 
            darkButton73.FlatStyle = FlatStyle.Flat;
            darkButton73.Location = new Point(7, 376);
            darkButton73.Margin = new Padding(4, 3, 4, 3);
            darkButton73.Name = "darkButton73";
            darkButton73.Size = new Size(128, 27);
            darkButton73.TabIndex = 136;
            darkButton73.Text = "Add";
            darkButton73.Click += darkButton73_Click;
            // 
            // darkButton74
            // 
            darkButton74.FlatStyle = FlatStyle.Flat;
            darkButton74.Location = new Point(196, 376);
            darkButton74.Margin = new Padding(4, 3, 4, 3);
            darkButton74.Name = "darkButton74";
            darkButton74.Size = new Size(121, 27);
            darkButton74.TabIndex = 135;
            darkButton74.Text = "Remove";
            darkButton74.Click += darkButton74_Click;
            // 
            // simpleChildrenTypesLB
            // 
            simpleChildrenTypesLB.BackColor = Color.FromArgb(60, 63, 65);
            simpleChildrenTypesLB.DrawMode = DrawMode.OwnerDrawFixed;
            simpleChildrenTypesLB.ForeColor = SystemColors.Control;
            simpleChildrenTypesLB.FormattingEnabled = true;
            simpleChildrenTypesLB.Location = new Point(7, 17);
            simpleChildrenTypesLB.Margin = new Padding(4, 3, 4, 3);
            simpleChildrenTypesLB.Name = "simpleChildrenTypesLB";
            simpleChildrenTypesLB.Size = new Size(310, 340);
            simpleChildrenTypesLB.TabIndex = 132;
            simpleChildrenTypesLB.DrawItem += listBox_DrawItem;
            // 
            // SpawnGearSimpleChildrenControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(simpleChildrenTypesGB);
            ForeColor = SystemColors.Control;
            Name = "SpawnGearSimpleChildrenControl";
            Size = new Size(335, 422);
            simpleChildrenTypesGB.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox simpleChildrenTypesGB;
        private Button darkButton73;
        private Button darkButton74;
        private ListBox simpleChildrenTypesLB;
    }
}
