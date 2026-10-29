namespace CaparePlugin
{
    partial class CapareLootBoxStaticBoxControl
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
            LootBoxSetNameL = new Label();
            LootBoxSetNameTB = new TextBox();
            NumberOfPositionsL = new Label();
            NumberOfPositionsNUD = new NumericUpDown();
            UseCustomLootListL = new Label();
            UseCustomLootListCB = new CheckBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumberOfPositionsNUD).BeginInit();
            SuspendLayout();
            //
            // groupBox1
            //
            groupBox1.Controls.Add(LootBoxSetNameL);
            groupBox1.Controls.Add(LootBoxSetNameTB);
            groupBox1.Controls.Add(NumberOfPositionsL);
            groupBox1.Controls.Add(NumberOfPositionsNUD);
            groupBox1.Controls.Add(UseCustomLootListL);
            groupBox1.Controls.Add(UseCustomLootListCB);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(435, 115);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Static Box Set";
            //
            // LootBoxSetNameL
            //
            LootBoxSetNameL.AutoSize = true;
            LootBoxSetNameL.ForeColor = Color.FromArgb(220, 220, 220);
            LootBoxSetNameL.Location = new Point(11, 25);
            LootBoxSetNameL.Margin = new Padding(4, 0, 4, 0);
            LootBoxSetNameL.Name = "LootBoxSetNameL";
            LootBoxSetNameL.Size = new Size(119, 15);
            LootBoxSetNameL.TabIndex = 0;
            LootBoxSetNameL.Text = "Loot Box Set Name";
            //
            // LootBoxSetNameTB
            //
            LootBoxSetNameTB.BackColor = Color.FromArgb(60, 63, 65);
            LootBoxSetNameTB.ForeColor = SystemColors.Control;
            LootBoxSetNameTB.Location = new Point(190, 22);
            LootBoxSetNameTB.Margin = new Padding(4, 3, 4, 3);
            LootBoxSetNameTB.Name = "LootBoxSetNameTB";
            LootBoxSetNameTB.Size = new Size(230, 23);
            LootBoxSetNameTB.TabIndex = 1;
            LootBoxSetNameTB.TextChanged += LootBoxSetNameTB_TextChanged;
            //
            // NumberOfPositionsL
            //
            NumberOfPositionsL.AutoSize = true;
            NumberOfPositionsL.ForeColor = Color.FromArgb(220, 220, 220);
            NumberOfPositionsL.Location = new Point(11, 54);
            NumberOfPositionsL.Margin = new Padding(4, 0, 4, 0);
            NumberOfPositionsL.Name = "NumberOfPositionsL";
            NumberOfPositionsL.Size = new Size(133, 15);
            NumberOfPositionsL.TabIndex = 2;
            NumberOfPositionsL.Text = "Number Of Positions";
            //
            // NumberOfPositionsNUD
            //
            NumberOfPositionsNUD.BackColor = Color.FromArgb(60, 63, 65);
            NumberOfPositionsNUD.ForeColor = SystemColors.Control;
            NumberOfPositionsNUD.Location = new Point(190, 51);
            NumberOfPositionsNUD.Margin = new Padding(4, 3, 4, 3);
            NumberOfPositionsNUD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            NumberOfPositionsNUD.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            NumberOfPositionsNUD.Name = "NumberOfPositionsNUD";
            NumberOfPositionsNUD.Size = new Size(230, 23);
            NumberOfPositionsNUD.TabIndex = 3;
            NumberOfPositionsNUD.TextAlign = HorizontalAlignment.Center;
            NumberOfPositionsNUD.ValueChanged += NumberOfPositionsNUD_ValueChanged;
            //
            // UseCustomLootListL
            //
            UseCustomLootListL.AutoSize = true;
            UseCustomLootListL.ForeColor = Color.FromArgb(220, 220, 220);
            UseCustomLootListL.Location = new Point(11, 83);
            UseCustomLootListL.Margin = new Padding(4, 0, 4, 0);
            UseCustomLootListL.Name = "UseCustomLootListL";
            UseCustomLootListL.Size = new Size(140, 15);
            UseCustomLootListL.TabIndex = 4;
            UseCustomLootListL.Text = "Use Custom Loot List";
            //
            // UseCustomLootListCB
            //
            UseCustomLootListCB.AutoSize = true;
            UseCustomLootListCB.Location = new Point(190, 83);
            UseCustomLootListCB.Margin = new Padding(4, 3, 4, 3);
            UseCustomLootListCB.Name = "UseCustomLootListCB";
            UseCustomLootListCB.Size = new Size(15, 14);
            UseCustomLootListCB.TabIndex = 5;
            UseCustomLootListCB.UseVisualStyleBackColor = true;
            UseCustomLootListCB.CheckedChanged += UseCustomLootListCB_CheckedChanged;
            //
            // CapareLootBoxStaticBoxControl
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox1);
            ForeColor = SystemColors.Control;
            Name = "CapareLootBoxStaticBoxControl";
            Size = new Size(435, 121);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NumberOfPositionsNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label LootBoxSetNameL;
        private TextBox LootBoxSetNameTB;
        private Label NumberOfPositionsL;
        private NumericUpDown NumberOfPositionsNUD;
        private Label UseCustomLootListL;
        private CheckBox UseCustomLootListCB;
    }
}
