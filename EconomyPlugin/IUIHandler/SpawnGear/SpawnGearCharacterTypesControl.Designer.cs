namespace EconomyPlugin
{
    partial class SpawnGearCharacterTypesControl
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
            characterTypesGB = new GroupBox();
            darkButton75 = new Button();
            characterTypesCB = new ComboBox();
            darkButton71 = new Button();
            darkButton72 = new Button();
            characterTypesLB = new ListBox();
            characterTypesGB.SuspendLayout();
            SuspendLayout();
            // 
            // characterTypesGB
            // 
            characterTypesGB.Controls.Add(darkButton75);
            characterTypesGB.Controls.Add(characterTypesCB);
            characterTypesGB.Controls.Add(darkButton71);
            characterTypesGB.Controls.Add(darkButton72);
            characterTypesGB.Controls.Add(characterTypesLB);
            characterTypesGB.ForeColor = SystemColors.Control;
            characterTypesGB.Location = new Point(0, 0);
            characterTypesGB.Margin = new Padding(4, 3, 4, 3);
            characterTypesGB.Name = "characterTypesGB";
            characterTypesGB.Padding = new Padding(4, 3, 4, 3);
            characterTypesGB.Size = new Size(324, 410);
            characterTypesGB.TabIndex = 233;
            characterTypesGB.TabStop = false;
            characterTypesGB.Text = "Character Types";
            // 
            // darkButton75
            // 
            darkButton75.FlatStyle = FlatStyle.Flat;
            darkButton75.Location = new Point(7, 374);
            darkButton75.Margin = new Padding(4, 3, 4, 3);
            darkButton75.Name = "darkButton75";
            darkButton75.Size = new Size(147, 27);
            darkButton75.TabIndex = 138;
            darkButton75.Text = "Add All";
            darkButton75.Click += darkButton75_Click;
            // 
            // characterTypesCB
            // 
            characterTypesCB.BackColor = Color.FromArgb(60, 63, 65);
            characterTypesCB.ForeColor = SystemColors.Control;
            characterTypesCB.FormattingEnabled = true;
            characterTypesCB.Location = new Point(7, 343);
            characterTypesCB.Margin = new Padding(4, 3, 4, 3);
            characterTypesCB.Name = "characterTypesCB";
            characterTypesCB.Size = new Size(279, 23);
            characterTypesCB.TabIndex = 137;
            // 
            // darkButton71
            // 
            darkButton71.FlatStyle = FlatStyle.Flat;
            darkButton71.Font = new Font("Segoe UI", 9F);
            darkButton71.Location = new Point(290, 341);
            darkButton71.Margin = new Padding(0);
            darkButton71.Name = "darkButton71";
            darkButton71.Size = new Size(25, 25);
            darkButton71.TabIndex = 136;
            darkButton71.Text = "+";
            darkButton71.Click += darkButton71_Click;
            // 
            // darkButton72
            // 
            darkButton72.FlatStyle = FlatStyle.Flat;
            darkButton72.Location = new Point(170, 374);
            darkButton72.Margin = new Padding(4, 3, 4, 3);
            darkButton72.Name = "darkButton72";
            darkButton72.Size = new Size(147, 27);
            darkButton72.TabIndex = 135;
            darkButton72.Text = "Remove Selected";
            darkButton72.Click += darkButton72_Click;
            // 
            // characterTypesLB
            // 
            characterTypesLB.BackColor = Color.FromArgb(60, 63, 65);
            characterTypesLB.DrawMode = DrawMode.OwnerDrawFixed;
            characterTypesLB.ForeColor = SystemColors.Control;
            characterTypesLB.FormattingEnabled = true;
            characterTypesLB.Location = new Point(7, 17);
            characterTypesLB.Margin = new Padding(4, 3, 4, 3);
            characterTypesLB.Name = "characterTypesLB";
            characterTypesLB.SelectionMode = SelectionMode.MultiExtended;
            characterTypesLB.Size = new Size(310, 308);
            characterTypesLB.TabIndex = 132;
            characterTypesLB.DrawItem += listBox_DrawItem;
            // 
            // SpawnGearCharacterTypesControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(characterTypesGB);
            ForeColor = SystemColors.Control;
            Name = "SpawnGearCharacterTypesControl";
            Size = new Size(335, 422);
            characterTypesGB.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox characterTypesGB;
        private Button darkButton75;
        private ComboBox characterTypesCB;
        private Button darkButton71;
        private Button darkButton72;
        private ListBox characterTypesLB;
    }
}
