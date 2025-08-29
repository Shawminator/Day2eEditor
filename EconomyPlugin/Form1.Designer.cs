namespace DayZeEditor
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
            attachmentSlotItemSetsGB = new GroupBox();
            ItemAttachmentSlotNameCB = new ComboBox();
            darkLabel29 = new Label();
            SpawnGearAttributesGB = new GroupBox();
            SpawnGearQuanityMaxNUD = new NumericUpDown();
            darkLabel30 = new Label();
            SpawnGearQuanityMinNUD = new NumericUpDown();
            darkLabel31 = new Label();
            SpawnGearhealthMaxNUD = new NumericUpDown();
            darkLabel45 = new Label();
            SpawnGearhealthMinNUD = new NumericUpDown();
            darkLabel44 = new Label();
            simpleChildrenUseDefaultAttributesGB = new GroupBox();
            simpleChildrenUseDefaultAttributesCB = new CheckBox();
            simpleChildrenTypesGB = new GroupBox();
            darkButton73 = new Button();
            darkButton74 = new Button();
            simpleChildrenTypesLB = new ListBox();
            quickBarSlotGB = new GroupBox();
            quickBarSlotNUD = new NumericUpDown();
            itemTypeGB = new GroupBox();
            darkButton70 = new Button();
            SpawnGearItemTypeTB = new TextBox();
            attachmentSlotItemSetsGB.SuspendLayout();
            SpawnGearAttributesGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SpawnGearQuanityMaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SpawnGearQuanityMinNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SpawnGearhealthMaxNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SpawnGearhealthMinNUD).BeginInit();
            simpleChildrenUseDefaultAttributesGB.SuspendLayout();
            simpleChildrenTypesGB.SuspendLayout();
            quickBarSlotGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)quickBarSlotNUD).BeginInit();
            itemTypeGB.SuspendLayout();
            SuspendLayout();
            // 
            // attachmentSlotItemSetsGB
            // 
            attachmentSlotItemSetsGB.Controls.Add(ItemAttachmentSlotNameCB);
            attachmentSlotItemSetsGB.Controls.Add(darkLabel29);
            attachmentSlotItemSetsGB.ForeColor = SystemColors.Control;
            attachmentSlotItemSetsGB.Location = new Point(345, 14);
            attachmentSlotItemSetsGB.Margin = new Padding(4, 3, 4, 3);
            attachmentSlotItemSetsGB.Name = "attachmentSlotItemSetsGB";
            attachmentSlotItemSetsGB.Padding = new Padding(4, 3, 4, 3);
            attachmentSlotItemSetsGB.Size = new Size(324, 81);
            attachmentSlotItemSetsGB.TabIndex = 225;
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
            // 
            // darkLabel29
            // 
            darkLabel29.AutoSize = true;
            darkLabel29.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel29.Location = new Point(19, 23);
            darkLabel29.Margin = new Padding(4, 0, 4, 0);
            darkLabel29.Name = "darkLabel29";
            darkLabel29.Size = new Size(59, 15);
            darkLabel29.TabIndex = 176;
            darkLabel29.Text = "SlotName";
            // 
            // SpawnGearAttributesGB
            // 
            SpawnGearAttributesGB.Controls.Add(SpawnGearQuanityMaxNUD);
            SpawnGearAttributesGB.Controls.Add(darkLabel30);
            SpawnGearAttributesGB.Controls.Add(SpawnGearQuanityMinNUD);
            SpawnGearAttributesGB.Controls.Add(darkLabel31);
            SpawnGearAttributesGB.Controls.Add(SpawnGearhealthMaxNUD);
            SpawnGearAttributesGB.Controls.Add(darkLabel45);
            SpawnGearAttributesGB.Controls.Add(SpawnGearhealthMinNUD);
            SpawnGearAttributesGB.Controls.Add(darkLabel44);
            SpawnGearAttributesGB.ForeColor = SystemColors.Control;
            SpawnGearAttributesGB.Location = new Point(14, 14);
            SpawnGearAttributesGB.Margin = new Padding(4, 3, 4, 3);
            SpawnGearAttributesGB.Name = "SpawnGearAttributesGB";
            SpawnGearAttributesGB.Padding = new Padding(4, 3, 4, 3);
            SpawnGearAttributesGB.Size = new Size(324, 148);
            SpawnGearAttributesGB.TabIndex = 229;
            SpawnGearAttributesGB.TabStop = false;
            SpawnGearAttributesGB.Text = "Attributes";
            // 
            // SpawnGearQuanityMaxNUD
            // 
            SpawnGearQuanityMaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            SpawnGearQuanityMaxNUD.DecimalPlaces = 1;
            SpawnGearQuanityMaxNUD.ForeColor = SystemColors.Control;
            SpawnGearQuanityMaxNUD.Location = new Point(181, 108);
            SpawnGearQuanityMaxNUD.Margin = new Padding(4, 3, 4, 3);
            SpawnGearQuanityMaxNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            SpawnGearQuanityMaxNUD.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            SpawnGearQuanityMaxNUD.Name = "SpawnGearQuanityMaxNUD";
            SpawnGearQuanityMaxNUD.Size = new Size(122, 23);
            SpawnGearQuanityMaxNUD.TabIndex = 195;
            SpawnGearQuanityMaxNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // darkLabel30
            // 
            darkLabel30.AutoSize = true;
            darkLabel30.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel30.Location = new Point(15, 78);
            darkLabel30.Margin = new Padding(4, 0, 4, 0);
            darkLabel30.Name = "darkLabel30";
            darkLabel30.Size = new Size(77, 15);
            darkLabel30.TabIndex = 196;
            darkLabel30.Text = "Quantity Min";
            // 
            // SpawnGearQuanityMinNUD
            // 
            SpawnGearQuanityMinNUD.BackColor = Color.FromArgb(60, 63, 65);
            SpawnGearQuanityMinNUD.DecimalPlaces = 1;
            SpawnGearQuanityMinNUD.ForeColor = SystemColors.Control;
            SpawnGearQuanityMinNUD.Location = new Point(182, 78);
            SpawnGearQuanityMinNUD.Margin = new Padding(4, 3, 4, 3);
            SpawnGearQuanityMinNUD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            SpawnGearQuanityMinNUD.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            SpawnGearQuanityMinNUD.Name = "SpawnGearQuanityMinNUD";
            SpawnGearQuanityMinNUD.Size = new Size(122, 23);
            SpawnGearQuanityMinNUD.TabIndex = 197;
            SpawnGearQuanityMinNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // darkLabel31
            // 
            darkLabel31.AutoSize = true;
            darkLabel31.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel31.Location = new Point(15, 111);
            darkLabel31.Margin = new Padding(4, 0, 4, 0);
            darkLabel31.Name = "darkLabel31";
            darkLabel31.Size = new Size(79, 15);
            darkLabel31.TabIndex = 198;
            darkLabel31.Text = "Quantity Max";
            // 
            // SpawnGearhealthMaxNUD
            // 
            SpawnGearhealthMaxNUD.BackColor = Color.FromArgb(60, 63, 65);
            SpawnGearhealthMaxNUD.DecimalPlaces = 1;
            SpawnGearhealthMaxNUD.ForeColor = SystemColors.Control;
            SpawnGearhealthMaxNUD.Location = new Point(182, 48);
            SpawnGearhealthMaxNUD.Margin = new Padding(4, 3, 4, 3);
            SpawnGearhealthMaxNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            SpawnGearhealthMaxNUD.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            SpawnGearhealthMaxNUD.Name = "SpawnGearhealthMaxNUD";
            SpawnGearhealthMaxNUD.Size = new Size(122, 23);
            SpawnGearhealthMaxNUD.TabIndex = 191;
            SpawnGearhealthMaxNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // darkLabel45
            // 
            darkLabel45.AutoSize = true;
            darkLabel45.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel45.Location = new Point(16, 21);
            darkLabel45.Margin = new Padding(4, 0, 4, 0);
            darkLabel45.Name = "darkLabel45";
            darkLabel45.Size = new Size(66, 15);
            darkLabel45.TabIndex = 192;
            darkLabel45.Text = "Health Min";
            // 
            // SpawnGearhealthMinNUD
            // 
            SpawnGearhealthMinNUD.BackColor = Color.FromArgb(60, 63, 65);
            SpawnGearhealthMinNUD.DecimalPlaces = 1;
            SpawnGearhealthMinNUD.ForeColor = SystemColors.Control;
            SpawnGearhealthMinNUD.Location = new Point(183, 18);
            SpawnGearhealthMinNUD.Margin = new Padding(4, 3, 4, 3);
            SpawnGearhealthMinNUD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            SpawnGearhealthMinNUD.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
            SpawnGearhealthMinNUD.Name = "SpawnGearhealthMinNUD";
            SpawnGearhealthMinNUD.Size = new Size(122, 23);
            SpawnGearhealthMinNUD.TabIndex = 193;
            SpawnGearhealthMinNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // darkLabel44
            // 
            darkLabel44.AutoSize = true;
            darkLabel44.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel44.Location = new Point(15, 51);
            darkLabel44.Margin = new Padding(4, 0, 4, 0);
            darkLabel44.Name = "darkLabel44";
            darkLabel44.Size = new Size(68, 15);
            darkLabel44.TabIndex = 194;
            darkLabel44.Text = "Health Max";
            // 
            // simpleChildrenUseDefaultAttributesGB
            // 
            simpleChildrenUseDefaultAttributesGB.Controls.Add(simpleChildrenUseDefaultAttributesCB);
            simpleChildrenUseDefaultAttributesGB.ForeColor = SystemColors.Control;
            simpleChildrenUseDefaultAttributesGB.Location = new Point(14, 168);
            simpleChildrenUseDefaultAttributesGB.Margin = new Padding(4, 3, 4, 3);
            simpleChildrenUseDefaultAttributesGB.Name = "simpleChildrenUseDefaultAttributesGB";
            simpleChildrenUseDefaultAttributesGB.Padding = new Padding(4, 3, 4, 3);
            simpleChildrenUseDefaultAttributesGB.Size = new Size(324, 60);
            simpleChildrenUseDefaultAttributesGB.TabIndex = 231;
            simpleChildrenUseDefaultAttributesGB.TabStop = false;
            simpleChildrenUseDefaultAttributesGB.Text = "Simple Children Use Default Attributes";
            // 
            // simpleChildrenUseDefaultAttributesCB
            // 
            simpleChildrenUseDefaultAttributesCB.AutoSize = true;
            simpleChildrenUseDefaultAttributesCB.Location = new Point(16, 27);
            simpleChildrenUseDefaultAttributesCB.Margin = new Padding(4, 3, 4, 3);
            simpleChildrenUseDefaultAttributesCB.Name = "simpleChildrenUseDefaultAttributesCB";
            simpleChildrenUseDefaultAttributesCB.Size = new Size(15, 14);
            simpleChildrenUseDefaultAttributesCB.TabIndex = 0;
            simpleChildrenUseDefaultAttributesCB.UseVisualStyleBackColor = true;
            // 
            // simpleChildrenTypesGB
            // 
            simpleChildrenTypesGB.Controls.Add(darkButton73);
            simpleChildrenTypesGB.Controls.Add(darkButton74);
            simpleChildrenTypesGB.Controls.Add(simpleChildrenTypesLB);
            simpleChildrenTypesGB.ForeColor = SystemColors.Control;
            simpleChildrenTypesGB.Location = new Point(13, 234);
            simpleChildrenTypesGB.Margin = new Padding(4, 3, 4, 3);
            simpleChildrenTypesGB.Name = "simpleChildrenTypesGB";
            simpleChildrenTypesGB.Padding = new Padding(4, 3, 4, 3);
            simpleChildrenTypesGB.Size = new Size(324, 410);
            simpleChildrenTypesGB.TabIndex = 233;
            simpleChildrenTypesGB.TabStop = false;
            simpleChildrenTypesGB.Text = "Simple Children Types";
            // 
            // darkButton73
            // 
            darkButton73.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            darkButton73.FlatStyle = FlatStyle.Flat;
            darkButton73.Location = new Point(7, 376);
            darkButton73.Margin = new Padding(4, 3, 4, 3);
            darkButton73.Name = "darkButton73";
            darkButton73.Size = new Size(128, 27);
            darkButton73.TabIndex = 136;
            darkButton73.Text = "Add";
            // 
            // darkButton74
            // 
            darkButton74.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            darkButton74.FlatStyle = FlatStyle.Flat;
            darkButton74.Location = new Point(196, 376);
            darkButton74.Margin = new Padding(4, 3, 4, 3);
            darkButton74.Name = "darkButton74";
            darkButton74.Size = new Size(121, 27);
            darkButton74.TabIndex = 135;
            darkButton74.Text = "Remove";
            // 
            // simpleChildrenTypesLB
            // 
            simpleChildrenTypesLB.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            simpleChildrenTypesLB.BackColor = Color.FromArgb(60, 63, 65);
            simpleChildrenTypesLB.DrawMode = DrawMode.OwnerDrawFixed;
            simpleChildrenTypesLB.ForeColor = SystemColors.Control;
            simpleChildrenTypesLB.FormattingEnabled = true;
            simpleChildrenTypesLB.Location = new Point(7, 17);
            simpleChildrenTypesLB.Margin = new Padding(4, 3, 4, 3);
            simpleChildrenTypesLB.Name = "simpleChildrenTypesLB";
            simpleChildrenTypesLB.Size = new Size(310, 340);
            simpleChildrenTypesLB.TabIndex = 132;
            // 
            // quickBarSlotGB
            // 
            quickBarSlotGB.Controls.Add(quickBarSlotNUD);
            quickBarSlotGB.ForeColor = SystemColors.Control;
            quickBarSlotGB.Location = new Point(345, 102);
            quickBarSlotGB.Margin = new Padding(4, 3, 4, 3);
            quickBarSlotGB.Name = "quickBarSlotGB";
            quickBarSlotGB.Padding = new Padding(4, 3, 4, 3);
            quickBarSlotGB.Size = new Size(324, 60);
            quickBarSlotGB.TabIndex = 230;
            quickBarSlotGB.TabStop = false;
            quickBarSlotGB.Text = "Quick Bar Slot";
            // 
            // quickBarSlotNUD
            // 
            quickBarSlotNUD.BackColor = Color.FromArgb(60, 63, 65);
            quickBarSlotNUD.ForeColor = SystemColors.Control;
            quickBarSlotNUD.Location = new Point(7, 22);
            quickBarSlotNUD.Margin = new Padding(4, 3, 4, 3);
            quickBarSlotNUD.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            quickBarSlotNUD.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            quickBarSlotNUD.Name = "quickBarSlotNUD";
            quickBarSlotNUD.Size = new Size(122, 23);
            quickBarSlotNUD.TabIndex = 186;
            quickBarSlotNUD.TextAlign = HorizontalAlignment.Center;
            // 
            // itemTypeGB
            // 
            itemTypeGB.Controls.Add(darkButton70);
            itemTypeGB.Controls.Add(SpawnGearItemTypeTB);
            itemTypeGB.ForeColor = SystemColors.Control;
            itemTypeGB.Location = new Point(345, 168);
            itemTypeGB.Margin = new Padding(4, 3, 4, 3);
            itemTypeGB.Name = "itemTypeGB";
            itemTypeGB.Padding = new Padding(4, 3, 4, 3);
            itemTypeGB.Size = new Size(324, 59);
            itemTypeGB.TabIndex = 226;
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(933, 847);
            Controls.Add(attachmentSlotItemSetsGB);
            Controls.Add(SpawnGearAttributesGB);
            Controls.Add(simpleChildrenUseDefaultAttributesGB);
            Controls.Add(simpleChildrenTypesGB);
            Controls.Add(quickBarSlotGB);
            Controls.Add(itemTypeGB);
            ForeColor = SystemColors.Control;
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Form1";
            attachmentSlotItemSetsGB.ResumeLayout(false);
            attachmentSlotItemSetsGB.PerformLayout();
            SpawnGearAttributesGB.ResumeLayout(false);
            SpawnGearAttributesGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SpawnGearQuanityMaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)SpawnGearQuanityMinNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)SpawnGearhealthMaxNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)SpawnGearhealthMinNUD).EndInit();
            simpleChildrenUseDefaultAttributesGB.ResumeLayout(false);
            simpleChildrenUseDefaultAttributesGB.PerformLayout();
            simpleChildrenTypesGB.ResumeLayout(false);
            quickBarSlotGB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)quickBarSlotNUD).EndInit();
            itemTypeGB.ResumeLayout(false);
            itemTypeGB.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox attachmentSlotItemSetsGB;
        private System.Windows.Forms.ComboBox ItemAttachmentSlotNameCB;
        private System.Windows.Forms.Label darkLabel29;
        private System.Windows.Forms.GroupBox SpawnGearAttributesGB;
        private System.Windows.Forms.NumericUpDown SpawnGearQuanityMaxNUD;
        private System.Windows.Forms.Label darkLabel30;
        private System.Windows.Forms.NumericUpDown SpawnGearQuanityMinNUD;
        private System.Windows.Forms.Label darkLabel31;
        private System.Windows.Forms.NumericUpDown SpawnGearhealthMaxNUD;
        private System.Windows.Forms.Label darkLabel45;
        private System.Windows.Forms.NumericUpDown SpawnGearhealthMinNUD;
        private System.Windows.Forms.Label darkLabel44;
        private System.Windows.Forms.GroupBox simpleChildrenUseDefaultAttributesGB;
        private System.Windows.Forms.CheckBox simpleChildrenUseDefaultAttributesCB;
        private System.Windows.Forms.GroupBox simpleChildrenTypesGB;
        private System.Windows.Forms.Button darkButton73;
        private System.Windows.Forms.Button darkButton74;
        private System.Windows.Forms.ListBox simpleChildrenTypesLB;
        private System.Windows.Forms.GroupBox quickBarSlotGB;
        private System.Windows.Forms.NumericUpDown quickBarSlotNUD;
        private System.Windows.Forms.GroupBox itemTypeGB;
        private System.Windows.Forms.Button darkButton70;
        private System.Windows.Forms.TextBox SpawnGearItemTypeTB;
    }
}