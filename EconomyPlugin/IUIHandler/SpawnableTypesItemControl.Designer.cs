namespace EconomyPlugin
{
    partial class SpawnableTypesItemControl
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
            groupBox34 = new GroupBox();
            flowLayoutPanel5 = new FlowLayoutPanel();
            ItemNameTB = new TextBox();
            CargoChangeItemButton = new Button();
            ItemChanceGB = new Panel();
            UseItemchanceCB = new CheckBox();
            ItemChanceNUD = new NumericUpDown();
            checkBox49 = new CheckBox();
            itemQuantGB = new Panel();
            numericUpDown4 = new NumericUpDown();
            label203 = new Label();
            numericUpDown3 = new NumericUpDown();
            label202 = new Label();
            isItemEquipCB = new CheckBox();
            ItemPresetGB = new Panel();
            ItemEquipGB = new GroupBox();
            ItemPresetCB = new ComboBox();
            darkButton26 = new Button();
            groupBox34.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            ItemChanceGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ItemChanceNUD).BeginInit();
            itemQuantGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            ItemPresetGB.SuspendLayout();
            ItemEquipGB.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox34
            // 
            groupBox34.Controls.Add(flowLayoutPanel5);
            groupBox34.ForeColor = SystemColors.ButtonHighlight;
            groupBox34.Location = new Point(0, 0);
            groupBox34.Margin = new Padding(4, 3, 4, 3);
            groupBox34.Name = "groupBox34";
            groupBox34.Padding = new Padding(4, 3, 4, 3);
            groupBox34.Size = new Size(282, 332);
            groupBox34.TabIndex = 76;
            groupBox34.TabStop = false;
            groupBox34.Text = "Item";
            // 
            // flowLayoutPanel5
            // 
            flowLayoutPanel5.Controls.Add(ItemNameTB);
            flowLayoutPanel5.Controls.Add(CargoChangeItemButton);
            flowLayoutPanel5.Controls.Add(ItemChanceGB);
            flowLayoutPanel5.Controls.Add(checkBox49);
            flowLayoutPanel5.Controls.Add(itemQuantGB);
            flowLayoutPanel5.Controls.Add(isItemEquipCB);
            flowLayoutPanel5.Controls.Add(ItemPresetGB);
            flowLayoutPanel5.Dock = DockStyle.Fill;
            flowLayoutPanel5.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel5.Location = new Point(4, 19);
            flowLayoutPanel5.Margin = new Padding(4, 3, 4, 3);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            flowLayoutPanel5.Size = new Size(274, 310);
            flowLayoutPanel5.TabIndex = 84;
            // 
            // ItemNameTB
            // 
            ItemNameTB.BackColor = Color.FromArgb(60, 63, 65);
            ItemNameTB.ForeColor = SystemColors.Control;
            ItemNameTB.Location = new Point(4, 3);
            ItemNameTB.Margin = new Padding(4, 3, 4, 3);
            ItemNameTB.Name = "ItemNameTB";
            ItemNameTB.ReadOnly = true;
            ItemNameTB.Size = new Size(254, 23);
            ItemNameTB.TabIndex = 81;
            // 
            // CargoChangeItemButton
            // 
            CargoChangeItemButton.FlatStyle = FlatStyle.Flat;
            CargoChangeItemButton.Location = new Point(4, 32);
            CargoChangeItemButton.Margin = new Padding(4, 3, 4, 3);
            CargoChangeItemButton.Name = "CargoChangeItemButton";
            CargoChangeItemButton.Size = new Size(254, 31);
            CargoChangeItemButton.TabIndex = 5;
            CargoChangeItemButton.Text = "Change Item";
            CargoChangeItemButton.Click += CargoChangeItemButton_Click;
            // 
            // ItemChanceGB
            // 
            ItemChanceGB.Controls.Add(UseItemchanceCB);
            ItemChanceGB.Controls.Add(ItemChanceNUD);
            ItemChanceGB.Location = new Point(4, 69);
            ItemChanceGB.Margin = new Padding(4, 3, 4, 3);
            ItemChanceGB.Name = "ItemChanceGB";
            ItemChanceGB.Size = new Size(255, 23);
            ItemChanceGB.TabIndex = 82;
            // 
            // UseItemchanceCB
            // 
            UseItemchanceCB.AutoSize = true;
            UseItemchanceCB.ForeColor = SystemColors.Control;
            UseItemchanceCB.Location = new Point(1, 2);
            UseItemchanceCB.Margin = new Padding(4, 3, 4, 3);
            UseItemchanceCB.Name = "UseItemchanceCB";
            UseItemchanceCB.Size = new Size(86, 19);
            UseItemchanceCB.TabIndex = 74;
            UseItemchanceCB.Text = "Use chance";
            UseItemchanceCB.UseVisualStyleBackColor = true;
            UseItemchanceCB.CheckedChanged += UseItemchanceCB_CheckedChanged;
            // 
            // ItemChanceNUD
            // 
            ItemChanceNUD.BackColor = Color.FromArgb(60, 63, 65);
            ItemChanceNUD.DecimalPlaces = 2;
            ItemChanceNUD.ForeColor = SystemColors.Control;
            ItemChanceNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            ItemChanceNUD.Location = new Point(144, 0);
            ItemChanceNUD.Margin = new Padding(4, 3, 4, 3);
            ItemChanceNUD.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            ItemChanceNUD.Name = "ItemChanceNUD";
            ItemChanceNUD.Size = new Size(111, 23);
            ItemChanceNUD.TabIndex = 64;
            ItemChanceNUD.TextAlign = HorizontalAlignment.Center;
            ItemChanceNUD.ValueChanged += ItemChanceNUD_ValueChanged;
            // 
            // checkBox49
            // 
            checkBox49.AutoSize = true;
            checkBox49.ForeColor = SystemColors.Control;
            checkBox49.Location = new Point(4, 98);
            checkBox49.Margin = new Padding(4, 3, 4, 3);
            checkBox49.Name = "checkBox49";
            checkBox49.Size = new Size(130, 19);
            checkBox49.TabIndex = 79;
            checkBox49.Text = "Use quant Min/Max";
            checkBox49.UseVisualStyleBackColor = true;
            checkBox49.CheckedChanged += checkBox49_CheckedChanged;
            // 
            // itemQuantGB
            // 
            itemQuantGB.Controls.Add(numericUpDown4);
            itemQuantGB.Controls.Add(label203);
            itemQuantGB.Controls.Add(numericUpDown3);
            itemQuantGB.Controls.Add(label202);
            itemQuantGB.Location = new Point(4, 123);
            itemQuantGB.Margin = new Padding(4, 3, 4, 3);
            itemQuantGB.Name = "itemQuantGB";
            itemQuantGB.Size = new Size(255, 23);
            itemQuantGB.TabIndex = 80;
            // 
            // numericUpDown4
            // 
            numericUpDown4.BackColor = Color.FromArgb(60, 63, 65);
            numericUpDown4.ForeColor = SystemColors.Control;
            numericUpDown4.Location = new Point(28, 0);
            numericUpDown4.Margin = new Padding(4, 3, 4, 3);
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new Size(96, 23);
            numericUpDown4.TabIndex = 76;
            numericUpDown4.TextAlign = HorizontalAlignment.Center;
            numericUpDown4.ValueChanged += numericUpDown4_ValueChanged;
            // 
            // label203
            // 
            label203.AutoSize = true;
            label203.ForeColor = SystemColors.Control;
            label203.Location = new Point(130, 2);
            label203.Margin = new Padding(4, 0, 4, 0);
            label203.Name = "label203";
            label203.Size = new Size(29, 15);
            label203.TabIndex = 77;
            label203.Text = "Max";
            // 
            // numericUpDown3
            // 
            numericUpDown3.BackColor = Color.FromArgb(60, 63, 65);
            numericUpDown3.ForeColor = SystemColors.Control;
            numericUpDown3.Location = new Point(161, 0);
            numericUpDown3.Margin = new Padding(4, 3, 4, 3);
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(94, 23);
            numericUpDown3.TabIndex = 78;
            numericUpDown3.TextAlign = HorizontalAlignment.Center;
            numericUpDown3.ValueChanged += numericUpDown3_ValueChanged;
            // 
            // label202
            // 
            label202.AutoSize = true;
            label202.ForeColor = SystemColors.Control;
            label202.Location = new Point(0, 2);
            label202.Margin = new Padding(4, 0, 4, 0);
            label202.Name = "label202";
            label202.Size = new Size(28, 15);
            label202.TabIndex = 75;
            label202.Text = "Min";
            // 
            // isItemEquipCB
            // 
            isItemEquipCB.AutoSize = true;
            isItemEquipCB.ForeColor = SystemColors.Control;
            isItemEquipCB.Location = new Point(4, 152);
            isItemEquipCB.Margin = new Padding(4, 3, 4, 3);
            isItemEquipCB.Name = "isItemEquipCB";
            isItemEquipCB.Size = new Size(117, 19);
            isItemEquipCB.TabIndex = 62;
            isItemEquipCB.Text = "Is Random Preset";
            isItemEquipCB.UseVisualStyleBackColor = true;
            isItemEquipCB.CheckedChanged += isItemEquipCB_CheckedChanged;
            // 
            // ItemPresetGB
            // 
            ItemPresetGB.Controls.Add(ItemEquipGB);
            ItemPresetGB.Location = new Point(4, 177);
            ItemPresetGB.Margin = new Padding(4, 3, 4, 3);
            ItemPresetGB.Name = "ItemPresetGB";
            ItemPresetGB.Size = new Size(257, 119);
            ItemPresetGB.TabIndex = 83;
            // 
            // ItemEquipGB
            // 
            ItemEquipGB.Controls.Add(ItemPresetCB);
            ItemEquipGB.Controls.Add(darkButton26);
            ItemEquipGB.ForeColor = SystemColors.ButtonHighlight;
            ItemEquipGB.Location = new Point(2, 3);
            ItemEquipGB.Margin = new Padding(4, 3, 4, 3);
            ItemEquipGB.Name = "ItemEquipGB";
            ItemEquipGB.Padding = new Padding(4, 3, 4, 3);
            ItemEquipGB.Size = new Size(254, 107);
            ItemEquipGB.TabIndex = 8;
            ItemEquipGB.TabStop = false;
            ItemEquipGB.Text = "Change Item Equip Preset";
            // 
            // ItemPresetCB
            // 
            ItemPresetCB.BackColor = Color.FromArgb(60, 63, 65);
            ItemPresetCB.ForeColor = SystemColors.Control;
            ItemPresetCB.FormattingEnabled = true;
            ItemPresetCB.Location = new Point(12, 29);
            ItemPresetCB.Margin = new Padding(4, 3, 4, 3);
            ItemPresetCB.Name = "ItemPresetCB";
            ItemPresetCB.Size = new Size(235, 23);
            ItemPresetCB.TabIndex = 1;
            // 
            // darkButton26
            // 
            darkButton26.FlatStyle = FlatStyle.Flat;
            darkButton26.Location = new Point(9, 60);
            darkButton26.Margin = new Padding(4, 3, 4, 3);
            darkButton26.Name = "darkButton26";
            darkButton26.Size = new Size(238, 31);
            darkButton26.TabIndex = 5;
            darkButton26.Text = "Change Preset";
            darkButton26.Click += darkButton26_Click;
            // 
            // SpawnableTypesItemControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox34);
            ForeColor = SystemColors.Control;
            Name = "SpawnableTypesItemControl";
            Size = new Size(289, 342);
            groupBox34.ResumeLayout(false);
            flowLayoutPanel5.ResumeLayout(false);
            flowLayoutPanel5.PerformLayout();
            ItemChanceGB.ResumeLayout(false);
            ItemChanceGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ItemChanceNUD).EndInit();
            itemQuantGB.ResumeLayout(false);
            itemQuantGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ItemPresetGB.ResumeLayout(false);
            ItemEquipGB.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox34;
        private FlowLayoutPanel flowLayoutPanel5;
        private TextBox ItemNameTB;
        private Button CargoChangeItemButton;
        private Panel ItemChanceGB;
        private CheckBox UseItemchanceCB;
        private NumericUpDown ItemChanceNUD;
        private CheckBox checkBox49;
        private Panel itemQuantGB;
        private NumericUpDown numericUpDown4;
        private Label label203;
        private NumericUpDown numericUpDown3;
        private Label label202;
        private CheckBox isItemEquipCB;
        private Panel ItemPresetGB;
        private GroupBox ItemEquipGB;
        private ComboBox ItemPresetCB;
        private Button darkButton26;
    }
}
