namespace EconomyPlugin
{
    partial class SpawnGearItemControl
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
            itemTypeGB = new GroupBox();
            darkButton70 = new Button();
            SpawnGearItemTypeTB = new TextBox();
            itemTypeGB.SuspendLayout();
            SuspendLayout();
            // 
            // itemTypeGB
            // 
            itemTypeGB.Controls.Add(darkButton70);
            itemTypeGB.Controls.Add(SpawnGearItemTypeTB);
            itemTypeGB.ForeColor = SystemColors.Control;
            itemTypeGB.Location = new Point(0, 0);
            itemTypeGB.Margin = new Padding(4, 3, 4, 3);
            itemTypeGB.Name = "itemTypeGB";
            itemTypeGB.Padding = new Padding(4, 3, 4, 3);
            itemTypeGB.Size = new Size(324, 59);
            itemTypeGB.TabIndex = 227;
            itemTypeGB.TabStop = false;
            itemTypeGB.Text = "item Type";
            // 
            // darkButton70
            // 
            darkButton70.FlatStyle = FlatStyle.Flat;
            darkButton70.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            darkButton70.Location = new Point(290, 21);
            darkButton70.Margin = new Padding(4, 3, 4, 3);
            darkButton70.Name = "darkButton70";
            darkButton70.Size = new Size(25, 25);
            darkButton70.TabIndex = 179;
            darkButton70.Text = "+";
            darkButton70.Click += darkButton70_Click;
            // 
            // SpawnGearItemTypeTB
            // 
            SpawnGearItemTypeTB.BackColor = Color.FromArgb(60, 63, 65);
            SpawnGearItemTypeTB.ForeColor = SystemColors.Control;
            SpawnGearItemTypeTB.Location = new Point(7, 22);
            SpawnGearItemTypeTB.Margin = new Padding(4, 3, 4, 3);
            SpawnGearItemTypeTB.Name = "SpawnGearItemTypeTB";
            SpawnGearItemTypeTB.ReadOnly = true;
            SpawnGearItemTypeTB.Size = new Size(279, 23);
            SpawnGearItemTypeTB.TabIndex = 180;
            // 
            // SpawnGearItemControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(itemTypeGB);
            ForeColor = SystemColors.Control;
            Name = "SpawnGearItemControl";
            Size = new Size(332, 68);
            itemTypeGB.ResumeLayout(false);
            itemTypeGB.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox itemTypeGB;
        private Button darkButton70;
        private TextBox SpawnGearItemTypeTB;
    }
}
