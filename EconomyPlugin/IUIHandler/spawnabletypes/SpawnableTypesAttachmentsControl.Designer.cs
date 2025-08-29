namespace EconomyPlugin
{
    partial class SpawnableTypesAttachmentsControl
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
            AttachmentGB = new GroupBox();
            UseAttachmentchanceCB = new CheckBox();
            AttachemntTB = new TextBox();
            AttachmentchanceNUD = new NumericUpDown();
            AttachmentPresetGB = new GroupBox();
            AttachmentPresetComboBox = new ComboBox();
            darkButton37 = new Button();
            isAttchmentIsPresetCB = new CheckBox();
            AttachmentGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)AttachmentchanceNUD).BeginInit();
            AttachmentPresetGB.SuspendLayout();
            SuspendLayout();
            // 
            // AttachmentGB
            // 
            AttachmentGB.Controls.Add(UseAttachmentchanceCB);
            AttachmentGB.Controls.Add(AttachemntTB);
            AttachmentGB.Controls.Add(AttachmentchanceNUD);
            AttachmentGB.Controls.Add(AttachmentPresetGB);
            AttachmentGB.Controls.Add(isAttchmentIsPresetCB);
            AttachmentGB.ForeColor = SystemColors.ButtonHighlight;
            AttachmentGB.Location = new Point(0, 0);
            AttachmentGB.Margin = new Padding(4, 3, 4, 3);
            AttachmentGB.Name = "AttachmentGB";
            AttachmentGB.Padding = new Padding(4, 3, 4, 3);
            AttachmentGB.Size = new Size(282, 240);
            AttachmentGB.TabIndex = 67;
            AttachmentGB.TabStop = false;
            AttachmentGB.Text = "Attachments";
            // 
            // UseAttachmentchanceCB
            // 
            UseAttachmentchanceCB.AutoSize = true;
            UseAttachmentchanceCB.ForeColor = SystemColors.Control;
            UseAttachmentchanceCB.Location = new Point(13, 28);
            UseAttachmentchanceCB.Margin = new Padding(4, 3, 4, 3);
            UseAttachmentchanceCB.Name = "UseAttachmentchanceCB";
            UseAttachmentchanceCB.Size = new Size(86, 19);
            UseAttachmentchanceCB.TabIndex = 74;
            UseAttachmentchanceCB.Text = "Use chance";
            UseAttachmentchanceCB.UseVisualStyleBackColor = true;
            UseAttachmentchanceCB.CheckedChanged += UseAttachmentchanceCB_CheckedChanged;
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
            AttachmentchanceNUD.ValueChanged += AttachmentchanceNUD_ValueChanged;
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
            darkButton37.Click += darkButton37_Click;
            // 
            // isAttchmentIsPresetCB
            // 
            isAttchmentIsPresetCB.AutoSize = true;
            isAttchmentIsPresetCB.ForeColor = SystemColors.Control;
            isAttchmentIsPresetCB.Location = new Point(13, 62);
            isAttchmentIsPresetCB.Margin = new Padding(4, 3, 4, 3);
            isAttchmentIsPresetCB.Name = "isAttchmentIsPresetCB";
            isAttchmentIsPresetCB.Size = new Size(117, 19);
            isAttchmentIsPresetCB.TabIndex = 62;
            isAttchmentIsPresetCB.Text = "Is Random Preset";
            isAttchmentIsPresetCB.UseVisualStyleBackColor = true;
            isAttchmentIsPresetCB.CheckedChanged += isAttchmentIsPresetCB_CheckedChanged;
            // 
            // SpawnableTypesAttachmentsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(AttachmentGB);
            ForeColor = SystemColors.Control;
            Name = "SpawnableTypesAttachmentsControl";
            Size = new Size(294, 254);
            AttachmentGB.ResumeLayout(false);
            AttachmentGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)AttachmentchanceNUD).EndInit();
            AttachmentPresetGB.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox AttachmentGB;
        private CheckBox UseAttachmentchanceCB;
        private TextBox AttachemntTB;
        private NumericUpDown AttachmentchanceNUD;
        private GroupBox AttachmentPresetGB;
        private ComboBox AttachmentPresetComboBox;
        private Button darkButton37;
        private CheckBox isAttchmentIsPresetCB;
    }
}
