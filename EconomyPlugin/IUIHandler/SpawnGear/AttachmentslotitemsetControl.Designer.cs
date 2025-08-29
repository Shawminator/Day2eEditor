namespace EconomyPlugin
{
    partial class AttachmentslotitemsetControl
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
            attachmentSlotItemSetsGB = new GroupBox();
            ItemAttachmentSlotNameCB = new ComboBox();
            darkLabel29 = new Label();
            attachmentSlotItemSetsGB.SuspendLayout();
            SuspendLayout();
            // 
            // attachmentSlotItemSetsGB
            // 
            attachmentSlotItemSetsGB.Controls.Add(ItemAttachmentSlotNameCB);
            attachmentSlotItemSetsGB.Controls.Add(darkLabel29);
            attachmentSlotItemSetsGB.ForeColor = SystemColors.Control;
            attachmentSlotItemSetsGB.Location = new Point(0, 0);
            attachmentSlotItemSetsGB.Margin = new Padding(4, 3, 4, 3);
            attachmentSlotItemSetsGB.Name = "attachmentSlotItemSetsGB";
            attachmentSlotItemSetsGB.Padding = new Padding(4, 3, 4, 3);
            attachmentSlotItemSetsGB.Size = new Size(324, 81);
            attachmentSlotItemSetsGB.TabIndex = 226;
            attachmentSlotItemSetsGB.TabStop = false;
            attachmentSlotItemSetsGB.Text = "Attachment Slot Item Sets";
            // 
            // ItemAttachmentSlotNameCB
            // 
            ItemAttachmentSlotNameCB.BackColor = Color.FromArgb(60, 63, 65);
            ItemAttachmentSlotNameCB.ForeColor = SystemColors.Control;
            ItemAttachmentSlotNameCB.FormattingEnabled = true;
            ItemAttachmentSlotNameCB.Location = new Point(7, 42);
            ItemAttachmentSlotNameCB.Margin = new Padding(4, 3, 4, 3);
            ItemAttachmentSlotNameCB.Name = "ItemAttachmentSlotNameCB";
            ItemAttachmentSlotNameCB.Size = new Size(310, 23);
            ItemAttachmentSlotNameCB.TabIndex = 177;
            ItemAttachmentSlotNameCB.SelectedIndexChanged += ItemAttachmentSlotNameCB_SelectedIndexChanged;
            // 
            // darkLabel29
            // 
            darkLabel29.AutoSize = true;
            darkLabel29.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel29.Location = new Point(20, 23);
            darkLabel29.Margin = new Padding(4, 0, 4, 0);
            darkLabel29.Name = "darkLabel29";
            darkLabel29.Size = new Size(59, 15);
            darkLabel29.TabIndex = 176;
            darkLabel29.Text = "SlotName";
            // 
            // AttachmentslotitemsetControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(attachmentSlotItemSetsGB);
            ForeColor = SystemColors.Control;
            Name = "AttachmentslotitemsetControl";
            Size = new Size(336, 97);
            attachmentSlotItemSetsGB.ResumeLayout(false);
            attachmentSlotItemSetsGB.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox attachmentSlotItemSetsGB;
        private ComboBox ItemAttachmentSlotNameCB;
        private Label darkLabel29;
    }
}
