namespace ExpansionPlugin
{
    partial class ExpansionSpawnGearLoadoutsControl
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
            groupBox95 = new GroupBox();
            button2 = new Button();
            SpawnLoadoutsLB = new ListBox();
            groupBox95.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox95
            // 
            groupBox95.Controls.Add(button2);
            groupBox95.Controls.Add(SpawnLoadoutsLB);
            groupBox95.ForeColor = SystemColors.Control;
            groupBox95.Location = new Point(0, 0);
            groupBox95.Margin = new Padding(4, 3, 4, 3);
            groupBox95.Name = "groupBox95";
            groupBox95.Padding = new Padding(4, 3, 4, 3);
            groupBox95.Size = new Size(330, 439);
            groupBox95.TabIndex = 5;
            groupBox95.TabStop = false;
            groupBox95.Text = "Loadouts";
            // 
            // button2
            // 
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(8, 401);
            button2.Margin = new Padding(4, 3, 4, 3);
            button2.Name = "button2";
            button2.Size = new Size(314, 27);
            button2.TabIndex = 2;
            button2.Text = "Add Loadout";
            button2.Click += button2_Click;
            // 
            // SpawnLoadoutsLB
            // 
            SpawnLoadoutsLB.BackColor = Color.FromArgb(60, 63, 65);
            SpawnLoadoutsLB.DrawMode = DrawMode.OwnerDrawFixed;
            SpawnLoadoutsLB.ForeColor = SystemColors.Control;
            SpawnLoadoutsLB.FormattingEnabled = true;
            SpawnLoadoutsLB.Location = new Point(7, 22);
            SpawnLoadoutsLB.Margin = new Padding(4, 3, 4, 3);
            SpawnLoadoutsLB.Name = "SpawnLoadoutsLB";
            SpawnLoadoutsLB.Size = new Size(316, 372);
            SpawnLoadoutsLB.TabIndex = 0;
            SpawnLoadoutsLB.DrawItem += listBox_DrawItem;
            // 
            // ExpansionSpawnGearLoadoutsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox95);
            ForeColor = SystemColors.Control;
            Name = "ExpansionSpawnGearLoadoutsControl";
            Size = new Size(330, 439);
            groupBox95.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox95;
        private Button button2;
        private ListBox SpawnLoadoutsLB;
    }
}
