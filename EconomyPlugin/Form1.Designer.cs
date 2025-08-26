namespace Day2eEditor
{
    partial class Form1
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

        #region Windows Form Designer generated code

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
            CargoGB = new GroupBox();
            CargoPresetTB = new TextBox();
            UseCargoChanceCB = new CheckBox();
            CarcgoChanceNUD = new NumericUpDown();
            IsCargoPresetCB = new CheckBox();
            CargoPresetGB = new GroupBox();
            CargoPresetComboBox = new ComboBox();
            darkButton36 = new Button();
            AttachmentGB = new GroupBox();
            UseAttachmentchanceCB = new CheckBox();
            AttachemntTB = new TextBox();
            AttachmentchanceNUD = new NumericUpDown();
            AttachmentPresetGB = new GroupBox();
            AttachmentPresetComboBox = new ComboBox();
            darkButton37 = new Button();
            isAttchmentIsPresetCB = new CheckBox();
            groupBox34.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            ItemChanceGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ItemChanceNUD).BeginInit();
            itemQuantGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            ItemPresetGB.SuspendLayout();
            ItemEquipGB.SuspendLayout();
            CargoGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)CarcgoChanceNUD).BeginInit();
            CargoPresetGB.SuspendLayout();
            AttachmentGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)AttachmentchanceNUD).BeginInit();
            AttachmentPresetGB.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox34
            // 
            groupBox34.Controls.Add(flowLayoutPanel5);
            groupBox34.ForeColor = SystemColors.ButtonHighlight;
            groupBox34.Location = new Point(638, 14);
            groupBox34.Margin = new Padding(4, 3, 4, 3);
            groupBox34.Name = "groupBox34";
            groupBox34.Padding = new Padding(4, 3, 4, 3);
            groupBox34.Size = new Size(282, 332);
            groupBox34.TabIndex = 75;
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
            // 
            // CargoGB
            // 
            CargoGB.Controls.Add(CargoPresetTB);
            CargoGB.Controls.Add(UseCargoChanceCB);
            CargoGB.Controls.Add(CarcgoChanceNUD);
            CargoGB.Controls.Add(IsCargoPresetCB);
            CargoGB.Controls.Add(CargoPresetGB);
            CargoGB.ForeColor = SystemColors.ButtonHighlight;
            CargoGB.Location = new Point(331, 14);
            CargoGB.Margin = new Padding(4, 3, 4, 3);
            CargoGB.Name = "CargoGB";
            CargoGB.Padding = new Padding(4, 3, 4, 3);
            CargoGB.Size = new Size(282, 240);
            CargoGB.TabIndex = 65;
            CargoGB.TabStop = false;
            CargoGB.Text = "Cargo";
            // 
            // CargoPresetTB
            // 
            CargoPresetTB.BackColor = Color.FromArgb(60, 63, 65);
            CargoPresetTB.ForeColor = SystemColors.Control;
            CargoPresetTB.Location = new Point(10, 87);
            CargoPresetTB.Margin = new Padding(4, 3, 4, 3);
            CargoPresetTB.Name = "CargoPresetTB";
            CargoPresetTB.ReadOnly = true;
            CargoPresetTB.Size = new Size(254, 23);
            CargoPresetTB.TabIndex = 70;
            // 
            // UseCargoChanceCB
            // 
            UseCargoChanceCB.AutoSize = true;
            UseCargoChanceCB.ForeColor = SystemColors.Control;
            UseCargoChanceCB.Location = new Point(12, 28);
            UseCargoChanceCB.Margin = new Padding(4, 3, 4, 3);
            UseCargoChanceCB.Name = "UseCargoChanceCB";
            UseCargoChanceCB.Size = new Size(86, 19);
            UseCargoChanceCB.TabIndex = 73;
            UseCargoChanceCB.Text = "Use chance";
            UseCargoChanceCB.UseVisualStyleBackColor = true;
            // 
            // CarcgoChanceNUD
            // 
            CarcgoChanceNUD.BackColor = Color.FromArgb(60, 63, 65);
            CarcgoChanceNUD.DecimalPlaces = 2;
            CarcgoChanceNUD.ForeColor = SystemColors.Control;
            CarcgoChanceNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            CarcgoChanceNUD.Location = new Point(154, 25);
            CarcgoChanceNUD.Margin = new Padding(4, 3, 4, 3);
            CarcgoChanceNUD.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            CarcgoChanceNUD.Name = "CarcgoChanceNUD";
            CarcgoChanceNUD.Size = new Size(112, 23);
            CarcgoChanceNUD.TabIndex = 64;
            CarcgoChanceNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // IsCargoPresetCB
            // 
            IsCargoPresetCB.AutoSize = true;
            IsCargoPresetCB.ForeColor = SystemColors.Control;
            IsCargoPresetCB.Location = new Point(12, 62);
            IsCargoPresetCB.Margin = new Padding(4, 3, 4, 3);
            IsCargoPresetCB.Name = "IsCargoPresetCB";
            IsCargoPresetCB.Size = new Size(117, 19);
            IsCargoPresetCB.TabIndex = 62;
            IsCargoPresetCB.Text = "Is Random Preset";
            IsCargoPresetCB.UseVisualStyleBackColor = true;
            // 
            // CargoPresetGB
            // 
            CargoPresetGB.Controls.Add(CargoPresetComboBox);
            CargoPresetGB.Controls.Add(darkButton36);
            CargoPresetGB.ForeColor = SystemColors.ButtonHighlight;
            CargoPresetGB.Location = new Point(12, 118);
            CargoPresetGB.Margin = new Padding(4, 3, 4, 3);
            CargoPresetGB.Name = "CargoPresetGB";
            CargoPresetGB.Padding = new Padding(4, 3, 4, 3);
            CargoPresetGB.Size = new Size(254, 115);
            CargoPresetGB.TabIndex = 8;
            CargoPresetGB.TabStop = false;
            CargoPresetGB.Text = "Change Cargo Preset";
            // 
            // CargoPresetComboBox
            // 
            CargoPresetComboBox.BackColor = Color.FromArgb(60, 63, 65);
            CargoPresetComboBox.ForeColor = SystemColors.Control;
            CargoPresetComboBox.FormattingEnabled = true;
            CargoPresetComboBox.Location = new Point(12, 29);
            CargoPresetComboBox.Margin = new Padding(4, 3, 4, 3);
            CargoPresetComboBox.Name = "CargoPresetComboBox";
            CargoPresetComboBox.Size = new Size(235, 23);
            CargoPresetComboBox.TabIndex = 1;
            // 
            // darkButton36
            // 
            darkButton36.FlatStyle = FlatStyle.Flat;
            darkButton36.Location = new Point(9, 60);
            darkButton36.Margin = new Padding(4, 3, 4, 3);
            darkButton36.Name = "darkButton36";
            darkButton36.Size = new Size(238, 31);
            darkButton36.TabIndex = 5;
            darkButton36.Text = "Change Preset";
            // 
            // AttachmentGB
            // 
            AttachmentGB.Controls.Add(UseAttachmentchanceCB);
            AttachmentGB.Controls.Add(AttachemntTB);
            AttachmentGB.Controls.Add(AttachmentchanceNUD);
            AttachmentGB.Controls.Add(AttachmentPresetGB);
            AttachmentGB.Controls.Add(isAttchmentIsPresetCB);
            AttachmentGB.ForeColor = SystemColors.ButtonHighlight;
            AttachmentGB.Location = new Point(21, 14);
            AttachmentGB.Margin = new Padding(4, 3, 4, 3);
            AttachmentGB.Name = "AttachmentGB";
            AttachmentGB.Padding = new Padding(4, 3, 4, 3);
            AttachmentGB.Size = new Size(282, 240);
            AttachmentGB.TabIndex = 66;
            AttachmentGB.TabStop = false;
            AttachmentGB.Text = "Attachments";
            // 
            // UseAttachmentchanceCB
            // 
            UseAttachmentchanceCB.AutoSize = true;
            UseAttachmentchanceCB.ForeColor = SystemColors.Control;
            UseAttachmentchanceCB.Location = new Point(12, 28);
            UseAttachmentchanceCB.Margin = new Padding(4, 3, 4, 3);
            UseAttachmentchanceCB.Name = "UseAttachmentchanceCB";
            UseAttachmentchanceCB.Size = new Size(86, 19);
            UseAttachmentchanceCB.TabIndex = 74;
            UseAttachmentchanceCB.Text = "Use chance";
            UseAttachmentchanceCB.UseVisualStyleBackColor = true;
            // 
            // AttachemntTB
            // 
            AttachemntTB.BackColor = Color.FromArgb(60, 63, 65);
            AttachemntTB.ForeColor = SystemColors.Control;
            AttachemntTB.Location = new Point(10, 87);
            AttachemntTB.Margin = new Padding(4, 3, 4, 3);
            AttachemntTB.Name = "AttachemntTB";
            AttachemntTB.ReadOnly = true;
            AttachemntTB.Size = new Size(254, 23);
            AttachemntTB.TabIndex = 71;
            // 
            // AttachmentchanceNUD
            // 
            AttachmentchanceNUD.BackColor = Color.FromArgb(60, 63, 65);
            AttachmentchanceNUD.DecimalPlaces = 2;
            AttachmentchanceNUD.ForeColor = SystemColors.Control;
            AttachmentchanceNUD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            AttachmentchanceNUD.Location = new Point(154, 25);
            AttachmentchanceNUD.Margin = new Padding(4, 3, 4, 3);
            AttachmentchanceNUD.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            AttachmentchanceNUD.Name = "AttachmentchanceNUD";
            AttachmentchanceNUD.Size = new Size(111, 23);
            AttachmentchanceNUD.TabIndex = 64;
            AttachmentchanceNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // AttachmentPresetGB
            // 
            AttachmentPresetGB.Controls.Add(AttachmentPresetComboBox);
            AttachmentPresetGB.Controls.Add(darkButton37);
            AttachmentPresetGB.ForeColor = SystemColors.ButtonHighlight;
            AttachmentPresetGB.Location = new Point(12, 118);
            AttachmentPresetGB.Margin = new Padding(4, 3, 4, 3);
            AttachmentPresetGB.Name = "AttachmentPresetGB";
            AttachmentPresetGB.Padding = new Padding(4, 3, 4, 3);
            AttachmentPresetGB.Size = new Size(254, 115);
            AttachmentPresetGB.TabIndex = 8;
            AttachmentPresetGB.TabStop = false;
            AttachmentPresetGB.Text = "Change Attachment Preset";
            // 
            // AttachmentPresetComboBox
            // 
            AttachmentPresetComboBox.BackColor = Color.FromArgb(60, 63, 65);
            AttachmentPresetComboBox.ForeColor = SystemColors.Control;
            AttachmentPresetComboBox.FormattingEnabled = true;
            AttachmentPresetComboBox.Location = new Point(12, 29);
            AttachmentPresetComboBox.Margin = new Padding(4, 3, 4, 3);
            AttachmentPresetComboBox.Name = "AttachmentPresetComboBox";
            AttachmentPresetComboBox.Size = new Size(235, 23);
            AttachmentPresetComboBox.TabIndex = 1;
            // 
            // darkButton37
            // 
            darkButton37.FlatStyle = FlatStyle.Flat;
            darkButton37.Location = new Point(9, 60);
            darkButton37.Margin = new Padding(4, 3, 4, 3);
            darkButton37.Name = "darkButton37";
            darkButton37.Size = new Size(238, 31);
            darkButton37.TabIndex = 5;
            darkButton37.Text = "Change Preset";
            // 
            // isAttchmentIsPresetCB
            // 
            isAttchmentIsPresetCB.AutoSize = true;
            isAttchmentIsPresetCB.ForeColor = SystemColors.Control;
            isAttchmentIsPresetCB.Location = new Point(12, 62);
            isAttchmentIsPresetCB.Margin = new Padding(4, 3, 4, 3);
            isAttchmentIsPresetCB.Name = "isAttchmentIsPresetCB";
            isAttchmentIsPresetCB.Size = new Size(117, 19);
            isAttchmentIsPresetCB.TabIndex = 62;
            isAttchmentIsPresetCB.Text = "Is Random Preset";
            isAttchmentIsPresetCB.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(933, 698);
            Controls.Add(CargoGB);
            Controls.Add(AttachmentGB);
            Controls.Add(groupBox34);
            ForeColor = SystemColors.Control;
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Form1";
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
            CargoGB.ResumeLayout(false);
            CargoGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)CarcgoChanceNUD).EndInit();
            CargoPresetGB.ResumeLayout(false);
            AttachmentGB.ResumeLayout(false);
            AttachmentGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)AttachmentchanceNUD).EndInit();
            AttachmentPresetGB.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox34;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.TextBox ItemNameTB;
        private System.Windows.Forms.Button CargoChangeItemButton;
        private System.Windows.Forms.Panel ItemChanceGB;
        private System.Windows.Forms.CheckBox UseItemchanceCB;
        private System.Windows.Forms.NumericUpDown ItemChanceNUD;
        private System.Windows.Forms.CheckBox checkBox49;
        private System.Windows.Forms.Panel itemQuantGB;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.Label label203;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Label label202;
        private System.Windows.Forms.CheckBox isItemEquipCB;
        private System.Windows.Forms.Panel ItemPresetGB;
        private System.Windows.Forms.GroupBox ItemEquipGB;
        private System.Windows.Forms.ComboBox ItemPresetCB;
        private System.Windows.Forms.Button darkButton26;
        private System.Windows.Forms.GroupBox CargoGB;
        private System.Windows.Forms.TextBox CargoPresetTB;
        private System.Windows.Forms.CheckBox UseCargoChanceCB;
        private System.Windows.Forms.NumericUpDown CarcgoChanceNUD;
        private System.Windows.Forms.CheckBox IsCargoPresetCB;
        private System.Windows.Forms.GroupBox CargoPresetGB;
        private System.Windows.Forms.ComboBox CargoPresetComboBox;
        private System.Windows.Forms.Button darkButton36;
        private System.Windows.Forms.GroupBox AttachmentGB;
        private System.Windows.Forms.CheckBox UseAttachmentchanceCB;
        private System.Windows.Forms.TextBox AttachemntTB;
        private System.Windows.Forms.NumericUpDown AttachmentchanceNUD;
        private System.Windows.Forms.GroupBox AttachmentPresetGB;
        private System.Windows.Forms.ComboBox AttachmentPresetComboBox;
        private System.Windows.Forms.Button darkButton37;
        private System.Windows.Forms.CheckBox isAttchmentIsPresetCB;
    }
}