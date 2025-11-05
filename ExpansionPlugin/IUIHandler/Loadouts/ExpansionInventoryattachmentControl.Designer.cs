namespace ExpansionPlugin
{
    partial class ExpansionInventoryattachmentControl
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
            InventoryattchemntGB = new GroupBox();
            ItemAttachmentSlotNameCB = new ComboBox();
            darkLabel9 = new Label();
            InventoryattchemntGB.SuspendLayout();
            SuspendLayout();
            // 
            // InventoryattchemntGB
            // 
            InventoryattchemntGB.Controls.Add(ItemAttachmentSlotNameCB);
            InventoryattchemntGB.Controls.Add(darkLabel9);
            InventoryattchemntGB.ForeColor = SystemColors.Control;
            InventoryattchemntGB.Location = new Point(0, 0);
            InventoryattchemntGB.Margin = new Padding(4, 3, 4, 3);
            InventoryattchemntGB.Name = "InventoryattchemntGB";
            InventoryattchemntGB.Padding = new Padding(4, 3, 4, 3);
            InventoryattchemntGB.Size = new Size(324, 81);
            InventoryattchemntGB.TabIndex = 217;
            InventoryattchemntGB.TabStop = false;
            InventoryattchemntGB.Text = "Inventory Attchments";
            // 
            // ItemAttachmentSlotNameCB
            // 
            ItemAttachmentSlotNameCB.BackColor = Color.FromArgb(60, 63, 65);
            ItemAttachmentSlotNameCB.ForeColor = SystemColors.Control;
            ItemAttachmentSlotNameCB.FormattingEnabled = true;
            ItemAttachmentSlotNameCB.Items.AddRange(new object[] { "Default Slot", "Back", "Body", "Dogtag", "Eyes", "Feet", "Gloves", "Hands", "Headgear", "Hips", "Legs", "Mask", "Melee", "Shoulder", "Vest" });
            ItemAttachmentSlotNameCB.Location = new Point(22, 42);
            ItemAttachmentSlotNameCB.Margin = new Padding(4, 3, 4, 3);
            ItemAttachmentSlotNameCB.Name = "ItemAttachmentSlotNameCB";
            ItemAttachmentSlotNameCB.Size = new Size(284, 23);
            ItemAttachmentSlotNameCB.TabIndex = 177;
            ItemAttachmentSlotNameCB.SelectedIndexChanged += ItemAttachmentSlotNameCB_SelectedIndexChanged;
            // 
            // darkLabel9
            // 
            darkLabel9.AutoSize = true;
            darkLabel9.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel9.Location = new Point(20, 23);
            darkLabel9.Margin = new Padding(4, 0, 4, 0);
            darkLabel9.Name = "darkLabel9";
            darkLabel9.Size = new Size(59, 15);
            darkLabel9.TabIndex = 176;
            darkLabel9.Text = "SlotName";
            // 
            // ExpansionInventoryattachmentControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(InventoryattchemntGB);
            ForeColor = SystemColors.Control;
            Name = "ExpansionInventoryattachmentControl";
            Size = new Size(324, 81);
            InventoryattchemntGB.ResumeLayout(false);
            InventoryattchemntGB.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox InventoryattchemntGB;
        private ComboBox ItemAttachmentSlotNameCB;
        private Label darkLabel9;
    }
}
