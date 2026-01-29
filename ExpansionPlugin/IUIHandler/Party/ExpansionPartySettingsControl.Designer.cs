namespace ExpansionPlugin
{
    partial class ExpansionPartySettingsControl
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
            groupBox9 = new GroupBox();
            DisplayPartyTagCB = new CheckBox();
            darkLabel262 = new Label();
            InviteCooldownNUD = new NumericUpDown();
            ForcePartyToHaveTagsCB = new CheckBox();
            ShowHUDMemberDistanceCB = new CheckBox();
            ShowPartyMemberMapMarkersCB = new CheckBox();
            ShowHUDMemberStanceCB = new CheckBox();
            ShowHUDMemberStatesCB = new CheckBox();
            ShowHUDMemberBloodCB = new CheckBox();
            darkLabel29 = new Label();
            MaxMembersInPartyNUD = new NumericUpDown();
            ShowPartyMemberHUDCB = new CheckBox();
            CanCreatePartyMarkersCB = new CheckBox();
            ShowNameOnQuickMarkersCB = new CheckBox();
            ShowDistanceUnderPartyMembersMarkersCB = new CheckBox();
            ShowNameOnPartyMembersMarkersCB = new CheckBox();
            ShowDistanceUnderQuickMarkersCB = new CheckBox();
            EnableQuickMarkerCB = new CheckBox();
            EnablePartiesCB = new CheckBox();
            ShowPartyMember3DMarkersCB = new CheckBox();
            UseWholeMapForInviteListCB = new CheckBox();
            groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)InviteCooldownNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MaxMembersInPartyNUD).BeginInit();
            SuspendLayout();
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(DisplayPartyTagCB);
            groupBox9.Controls.Add(darkLabel262);
            groupBox9.Controls.Add(InviteCooldownNUD);
            groupBox9.Controls.Add(ForcePartyToHaveTagsCB);
            groupBox9.Controls.Add(ShowHUDMemberDistanceCB);
            groupBox9.Controls.Add(ShowPartyMemberMapMarkersCB);
            groupBox9.Controls.Add(ShowHUDMemberStanceCB);
            groupBox9.Controls.Add(ShowHUDMemberStatesCB);
            groupBox9.Controls.Add(ShowHUDMemberBloodCB);
            groupBox9.Controls.Add(darkLabel29);
            groupBox9.Controls.Add(MaxMembersInPartyNUD);
            groupBox9.Controls.Add(ShowPartyMemberHUDCB);
            groupBox9.Controls.Add(CanCreatePartyMarkersCB);
            groupBox9.Controls.Add(ShowNameOnQuickMarkersCB);
            groupBox9.Controls.Add(ShowDistanceUnderPartyMembersMarkersCB);
            groupBox9.Controls.Add(ShowNameOnPartyMembersMarkersCB);
            groupBox9.Controls.Add(ShowDistanceUnderQuickMarkersCB);
            groupBox9.Controls.Add(EnableQuickMarkerCB);
            groupBox9.Controls.Add(EnablePartiesCB);
            groupBox9.Controls.Add(ShowPartyMember3DMarkersCB);
            groupBox9.Controls.Add(UseWholeMapForInviteListCB);
            groupBox9.ForeColor = SystemColors.Control;
            groupBox9.Location = new Point(0, 0);
            groupBox9.Margin = new Padding(4, 3, 4, 3);
            groupBox9.Name = "groupBox9";
            groupBox9.Padding = new Padding(4, 3, 4, 3);
            groupBox9.Size = new Size(540, 288);
            groupBox9.TabIndex = 15;
            groupBox9.TabStop = false;
            groupBox9.Text = "Party Settings";
            // 
            // DisplayPartyTagCB
            // 
            DisplayPartyTagCB.AutoSize = true;
            DisplayPartyTagCB.ForeColor = SystemColors.Control;
            DisplayPartyTagCB.Location = new Point(296, 264);
            DisplayPartyTagCB.Margin = new Padding(4, 3, 4, 3);
            DisplayPartyTagCB.Name = "DisplayPartyTagCB";
            DisplayPartyTagCB.Size = new Size(115, 19);
            DisplayPartyTagCB.TabIndex = 18;
            DisplayPartyTagCB.Text = "Display Party Tag";
            DisplayPartyTagCB.TextAlign = ContentAlignment.MiddleRight;
            DisplayPartyTagCB.UseVisualStyleBackColor = true;
            DisplayPartyTagCB.CheckedChanged += PartySettingsCB_CheckedChanged;
            // 
            // darkLabel262
            // 
            darkLabel262.AutoSize = true;
            darkLabel262.ForeColor = SystemColors.Control;
            darkLabel262.Location = new Point(296, 214);
            darkLabel262.Margin = new Padding(4, 0, 4, 0);
            darkLabel262.Name = "darkLabel262";
            darkLabel262.Size = new Size(94, 15);
            darkLabel262.TabIndex = 37;
            darkLabel262.Text = "Invite Cooldown";
            // 
            // InviteCooldownNUD
            // 
            InviteCooldownNUD.BackColor = Color.FromArgb(60, 63, 65);
            InviteCooldownNUD.ForeColor = SystemColors.Control;
            InviteCooldownNUD.Location = new Point(296, 232);
            InviteCooldownNUD.Margin = new Padding(4, 3, 4, 3);
            InviteCooldownNUD.Maximum = new decimal(new int[] { 4000000, 0, 0, 0 });
            InviteCooldownNUD.Name = "InviteCooldownNUD";
            InviteCooldownNUD.Size = new Size(126, 23);
            InviteCooldownNUD.TabIndex = 17;
            InviteCooldownNUD.TextAlign = HorizontalAlignment.Center;
            InviteCooldownNUD.ValueChanged += PartySettingsNUD_ValueChanged;
            // 
            // ForcePartyToHaveTagsCB
            // 
            ForcePartyToHaveTagsCB.AutoSize = true;
            ForcePartyToHaveTagsCB.ForeColor = SystemColors.Control;
            ForcePartyToHaveTagsCB.Location = new Point(295, 185);
            ForcePartyToHaveTagsCB.Margin = new Padding(4, 3, 4, 3);
            ForcePartyToHaveTagsCB.Name = "ForcePartyToHaveTagsCB";
            ForcePartyToHaveTagsCB.Size = new Size(156, 19);
            ForcePartyToHaveTagsCB.TabIndex = 16;
            ForcePartyToHaveTagsCB.Text = "Force Party To Have Tags";
            ForcePartyToHaveTagsCB.TextAlign = ContentAlignment.MiddleRight;
            ForcePartyToHaveTagsCB.UseVisualStyleBackColor = true;
            ForcePartyToHaveTagsCB.CheckedChanged += PartySettingsCB_CheckedChanged;
            // 
            // ShowHUDMemberDistanceCB
            // 
            ShowHUDMemberDistanceCB.AutoSize = true;
            ShowHUDMemberDistanceCB.ForeColor = SystemColors.Control;
            ShowHUDMemberDistanceCB.Location = new Point(295, 160);
            ShowHUDMemberDistanceCB.Margin = new Padding(4, 3, 4, 3);
            ShowHUDMemberDistanceCB.Name = "ShowHUDMemberDistanceCB";
            ShowHUDMemberDistanceCB.Size = new Size(179, 19);
            ShowHUDMemberDistanceCB.TabIndex = 15;
            ShowHUDMemberDistanceCB.Text = "Show HUD Member Distance";
            ShowHUDMemberDistanceCB.TextAlign = ContentAlignment.MiddleRight;
            ShowHUDMemberDistanceCB.UseVisualStyleBackColor = true;
            ShowHUDMemberDistanceCB.CheckedChanged += PartySettingsCB_CheckedChanged;
            // 
            // ShowPartyMemberMapMarkersCB
            // 
            ShowPartyMemberMapMarkersCB.AutoSize = true;
            ShowPartyMemberMapMarkersCB.ForeColor = SystemColors.Control;
            ShowPartyMemberMapMarkersCB.Location = new Point(295, 132);
            ShowPartyMemberMapMarkersCB.Margin = new Padding(4, 3, 4, 3);
            ShowPartyMemberMapMarkersCB.Name = "ShowPartyMemberMapMarkersCB";
            ShowPartyMemberMapMarkersCB.Size = new Size(205, 19);
            ShowPartyMemberMapMarkersCB.TabIndex = 14;
            ShowPartyMemberMapMarkersCB.Text = "Show Party Member Map Markers";
            ShowPartyMemberMapMarkersCB.TextAlign = ContentAlignment.MiddleRight;
            ShowPartyMemberMapMarkersCB.UseVisualStyleBackColor = true;
            ShowPartyMemberMapMarkersCB.CheckedChanged += PartySettingsCB_CheckedChanged;
            // 
            // ShowHUDMemberStanceCB
            // 
            ShowHUDMemberStanceCB.AutoSize = true;
            ShowHUDMemberStanceCB.ForeColor = SystemColors.Control;
            ShowHUDMemberStanceCB.Location = new Point(296, 105);
            ShowHUDMemberStanceCB.Margin = new Padding(4, 3, 4, 3);
            ShowHUDMemberStanceCB.Name = "ShowHUDMemberStanceCB";
            ShowHUDMemberStanceCB.Size = new Size(169, 19);
            ShowHUDMemberStanceCB.TabIndex = 13;
            ShowHUDMemberStanceCB.Text = "Show HUD Member Stance";
            ShowHUDMemberStanceCB.TextAlign = ContentAlignment.MiddleRight;
            ShowHUDMemberStanceCB.UseVisualStyleBackColor = true;
            ShowHUDMemberStanceCB.CheckedChanged += PartySettingsCB_CheckedChanged;
            // 
            // ShowHUDMemberStatesCB
            // 
            ShowHUDMemberStatesCB.AutoSize = true;
            ShowHUDMemberStatesCB.ForeColor = SystemColors.Control;
            ShowHUDMemberStatesCB.Location = new Point(296, 78);
            ShowHUDMemberStatesCB.Margin = new Padding(4, 3, 4, 3);
            ShowHUDMemberStatesCB.Name = "ShowHUDMemberStatesCB";
            ShowHUDMemberStatesCB.Size = new Size(165, 19);
            ShowHUDMemberStatesCB.TabIndex = 12;
            ShowHUDMemberStatesCB.Text = "Show HUD Member States";
            ShowHUDMemberStatesCB.TextAlign = ContentAlignment.MiddleRight;
            ShowHUDMemberStatesCB.UseVisualStyleBackColor = true;
            ShowHUDMemberStatesCB.CheckedChanged += PartySettingsCB_CheckedChanged;
            // 
            // ShowHUDMemberBloodCB
            // 
            ShowHUDMemberBloodCB.AutoSize = true;
            ShowHUDMemberBloodCB.ForeColor = SystemColors.Control;
            ShowHUDMemberBloodCB.Location = new Point(296, 48);
            ShowHUDMemberBloodCB.Margin = new Padding(4, 3, 4, 3);
            ShowHUDMemberBloodCB.Name = "ShowHUDMemberBloodCB";
            ShowHUDMemberBloodCB.Size = new Size(165, 19);
            ShowHUDMemberBloodCB.TabIndex = 11;
            ShowHUDMemberBloodCB.Text = "Show HUD Member Blood";
            ShowHUDMemberBloodCB.TextAlign = ContentAlignment.MiddleRight;
            ShowHUDMemberBloodCB.UseVisualStyleBackColor = true;
            ShowHUDMemberBloodCB.CheckedChanged += PartySettingsCB_CheckedChanged;
            // 
            // darkLabel29
            // 
            darkLabel29.AutoSize = true;
            darkLabel29.ForeColor = SystemColors.Control;
            darkLabel29.Location = new Point(93, 50);
            darkLabel29.Margin = new Padding(4, 0, 4, 0);
            darkLabel29.Name = "darkLabel29";
            darkLabel29.Size = new Size(126, 15);
            darkLabel29.TabIndex = 29;
            darkLabel29.Text = "Max Members In Party";
            // 
            // MaxMembersInPartyNUD
            // 
            MaxMembersInPartyNUD.BackColor = Color.FromArgb(60, 63, 65);
            MaxMembersInPartyNUD.ForeColor = SystemColors.Control;
            MaxMembersInPartyNUD.Location = new Point(9, 45);
            MaxMembersInPartyNUD.Margin = new Padding(4, 3, 4, 3);
            MaxMembersInPartyNUD.Name = "MaxMembersInPartyNUD";
            MaxMembersInPartyNUD.Size = new Size(76, 23);
            MaxMembersInPartyNUD.TabIndex = 1;
            MaxMembersInPartyNUD.TextAlign = HorizontalAlignment.Center;
            MaxMembersInPartyNUD.ValueChanged += PartySettingsNUD_ValueChanged;
            // 
            // ShowPartyMemberHUDCB
            // 
            ShowPartyMemberHUDCB.AutoSize = true;
            ShowPartyMemberHUDCB.ForeColor = SystemColors.Control;
            ShowPartyMemberHUDCB.Location = new Point(296, 22);
            ShowPartyMemberHUDCB.Margin = new Padding(4, 3, 4, 3);
            ShowPartyMemberHUDCB.Name = "ShowPartyMemberHUDCB";
            ShowPartyMemberHUDCB.Size = new Size(161, 19);
            ShowPartyMemberHUDCB.TabIndex = 10;
            ShowPartyMemberHUDCB.Text = "Show Party Member HUD";
            ShowPartyMemberHUDCB.TextAlign = ContentAlignment.MiddleRight;
            ShowPartyMemberHUDCB.UseVisualStyleBackColor = true;
            ShowPartyMemberHUDCB.CheckedChanged += PartySettingsCB_CheckedChanged;
            // 
            // CanCreatePartyMarkersCB
            // 
            CanCreatePartyMarkersCB.AutoSize = true;
            CanCreatePartyMarkersCB.ForeColor = SystemColors.Control;
            CanCreatePartyMarkersCB.Location = new Point(10, 264);
            CanCreatePartyMarkersCB.Margin = new Padding(4, 3, 4, 3);
            CanCreatePartyMarkersCB.Name = "CanCreatePartyMarkersCB";
            CanCreatePartyMarkersCB.Size = new Size(159, 19);
            CanCreatePartyMarkersCB.TabIndex = 9;
            CanCreatePartyMarkersCB.Text = "Can Create Party Markers";
            CanCreatePartyMarkersCB.TextAlign = ContentAlignment.MiddleRight;
            CanCreatePartyMarkersCB.UseVisualStyleBackColor = true;
            CanCreatePartyMarkersCB.CheckedChanged += PartySettingsCB_CheckedChanged;
            // 
            // ShowNameOnQuickMarkersCB
            // 
            ShowNameOnQuickMarkersCB.AutoSize = true;
            ShowNameOnQuickMarkersCB.ForeColor = SystemColors.Control;
            ShowNameOnQuickMarkersCB.Location = new Point(10, 239);
            ShowNameOnQuickMarkersCB.Margin = new Padding(4, 3, 4, 3);
            ShowNameOnQuickMarkersCB.Name = "ShowNameOnQuickMarkersCB";
            ShowNameOnQuickMarkersCB.Size = new Size(188, 19);
            ShowNameOnQuickMarkersCB.TabIndex = 8;
            ShowNameOnQuickMarkersCB.Text = "Show Name On Quick Markers";
            ShowNameOnQuickMarkersCB.TextAlign = ContentAlignment.MiddleRight;
            ShowNameOnQuickMarkersCB.UseVisualStyleBackColor = true;
            ShowNameOnQuickMarkersCB.CheckedChanged += PartySettingsCB_CheckedChanged;
            // 
            // ShowDistanceUnderPartyMembersMarkersCB
            // 
            ShowDistanceUnderPartyMembersMarkersCB.AutoSize = true;
            ShowDistanceUnderPartyMembersMarkersCB.ForeColor = SystemColors.Control;
            ShowDistanceUnderPartyMembersMarkersCB.Location = new Point(10, 132);
            ShowDistanceUnderPartyMembersMarkersCB.Margin = new Padding(4, 3, 4, 3);
            ShowDistanceUnderPartyMembersMarkersCB.Name = "ShowDistanceUnderPartyMembersMarkersCB";
            ShowDistanceUnderPartyMembersMarkersCB.Size = new Size(266, 19);
            ShowDistanceUnderPartyMembersMarkersCB.TabIndex = 4;
            ShowDistanceUnderPartyMembersMarkersCB.Text = "Show Distance Under Party Members Markers";
            ShowDistanceUnderPartyMembersMarkersCB.TextAlign = ContentAlignment.MiddleRight;
            ShowDistanceUnderPartyMembersMarkersCB.UseVisualStyleBackColor = true;
            ShowDistanceUnderPartyMembersMarkersCB.CheckedChanged += PartySettingsCB_CheckedChanged;
            // 
            // ShowNameOnPartyMembersMarkersCB
            // 
            ShowNameOnPartyMembersMarkersCB.AutoSize = true;
            ShowNameOnPartyMembersMarkersCB.ForeColor = SystemColors.Control;
            ShowNameOnPartyMembersMarkersCB.Location = new Point(10, 159);
            ShowNameOnPartyMembersMarkersCB.Margin = new Padding(4, 3, 4, 3);
            ShowNameOnPartyMembersMarkersCB.Name = "ShowNameOnPartyMembersMarkersCB";
            ShowNameOnPartyMembersMarkersCB.Size = new Size(237, 19);
            ShowNameOnPartyMembersMarkersCB.TabIndex = 5;
            ShowNameOnPartyMembersMarkersCB.Text = "Show Name On Party Members Markers";
            ShowNameOnPartyMembersMarkersCB.TextAlign = ContentAlignment.MiddleRight;
            ShowNameOnPartyMembersMarkersCB.UseVisualStyleBackColor = true;
            ShowNameOnPartyMembersMarkersCB.CheckedChanged += PartySettingsCB_CheckedChanged;
            // 
            // ShowDistanceUnderQuickMarkersCB
            // 
            ShowDistanceUnderQuickMarkersCB.AutoSize = true;
            ShowDistanceUnderQuickMarkersCB.ForeColor = SystemColors.Control;
            ShowDistanceUnderQuickMarkersCB.Location = new Point(10, 212);
            ShowDistanceUnderQuickMarkersCB.Margin = new Padding(4, 3, 4, 3);
            ShowDistanceUnderQuickMarkersCB.Name = "ShowDistanceUnderQuickMarkersCB";
            ShowDistanceUnderQuickMarkersCB.Size = new Size(217, 19);
            ShowDistanceUnderQuickMarkersCB.TabIndex = 7;
            ShowDistanceUnderQuickMarkersCB.Text = "Show Distance Under Quick Markers";
            ShowDistanceUnderQuickMarkersCB.TextAlign = ContentAlignment.MiddleRight;
            ShowDistanceUnderQuickMarkersCB.UseVisualStyleBackColor = true;
            ShowDistanceUnderQuickMarkersCB.CheckedChanged += PartySettingsCB_CheckedChanged;
            // 
            // EnableQuickMarkerCB
            // 
            EnableQuickMarkerCB.AutoSize = true;
            EnableQuickMarkerCB.ForeColor = SystemColors.Control;
            EnableQuickMarkerCB.Location = new Point(11, 186);
            EnableQuickMarkerCB.Margin = new Padding(4, 3, 4, 3);
            EnableQuickMarkerCB.Name = "EnableQuickMarkerCB";
            EnableQuickMarkerCB.Size = new Size(135, 19);
            EnableQuickMarkerCB.TabIndex = 6;
            EnableQuickMarkerCB.Text = "Enable Quick Marker";
            EnableQuickMarkerCB.TextAlign = ContentAlignment.MiddleRight;
            EnableQuickMarkerCB.UseVisualStyleBackColor = true;
            EnableQuickMarkerCB.CheckedChanged += PartySettingsCB_CheckedChanged;
            // 
            // EnablePartiesCB
            // 
            EnablePartiesCB.AutoSize = true;
            EnablePartiesCB.ForeColor = SystemColors.Control;
            EnablePartiesCB.Location = new Point(10, 22);
            EnablePartiesCB.Margin = new Padding(4, 3, 4, 3);
            EnablePartiesCB.Name = "EnablePartiesCB";
            EnablePartiesCB.Size = new Size(96, 19);
            EnablePartiesCB.TabIndex = 0;
            EnablePartiesCB.Text = "EnableParties";
            EnablePartiesCB.TextAlign = ContentAlignment.MiddleRight;
            EnablePartiesCB.UseVisualStyleBackColor = true;
            EnablePartiesCB.CheckedChanged += PartySettingsCB_CheckedChanged;
            // 
            // ShowPartyMember3DMarkersCB
            // 
            ShowPartyMember3DMarkersCB.AutoSize = true;
            ShowPartyMember3DMarkersCB.ForeColor = SystemColors.Control;
            ShowPartyMember3DMarkersCB.Location = new Point(10, 105);
            ShowPartyMember3DMarkersCB.Margin = new Padding(4, 3, 4, 3);
            ShowPartyMember3DMarkersCB.Name = "ShowPartyMember3DMarkersCB";
            ShowPartyMember3DMarkersCB.Size = new Size(195, 19);
            ShowPartyMember3DMarkersCB.TabIndex = 3;
            ShowPartyMember3DMarkersCB.Text = "Show Party Member 3D Markers";
            ShowPartyMember3DMarkersCB.TextAlign = ContentAlignment.MiddleRight;
            ShowPartyMember3DMarkersCB.UseVisualStyleBackColor = true;
            ShowPartyMember3DMarkersCB.CheckedChanged += PartySettingsCB_CheckedChanged;
            // 
            // UseWholeMapForInviteListCB
            // 
            UseWholeMapForInviteListCB.AutoSize = true;
            UseWholeMapForInviteListCB.ForeColor = SystemColors.Control;
            UseWholeMapForInviteListCB.Location = new Point(10, 78);
            UseWholeMapForInviteListCB.Margin = new Padding(4, 3, 4, 3);
            UseWholeMapForInviteListCB.Name = "UseWholeMapForInviteListCB";
            UseWholeMapForInviteListCB.Size = new Size(182, 19);
            UseWholeMapForInviteListCB.TabIndex = 2;
            UseWholeMapForInviteListCB.Text = "Use Whole Map For Invite List";
            UseWholeMapForInviteListCB.TextAlign = ContentAlignment.MiddleRight;
            UseWholeMapForInviteListCB.UseVisualStyleBackColor = true;
            UseWholeMapForInviteListCB.CheckedChanged += PartySettingsCB_CheckedChanged;
            // 
            // ExpansionPartySettingsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox9);
            ForeColor = SystemColors.Control;
            Name = "ExpansionPartySettingsControl";
            Size = new Size(540, 288);
            groupBox9.ResumeLayout(false);
            groupBox9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)InviteCooldownNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)MaxMembersInPartyNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox9;
        private CheckBox DisplayPartyTagCB;
        private Label darkLabel262;
        private NumericUpDown InviteCooldownNUD;
        private CheckBox ForcePartyToHaveTagsCB;
        private CheckBox ShowHUDMemberDistanceCB;
        private CheckBox ShowPartyMemberMapMarkersCB;
        private CheckBox ShowHUDMemberStanceCB;
        private CheckBox ShowHUDMemberStatesCB;
        private CheckBox ShowHUDMemberBloodCB;
        private Label darkLabel29;
        private NumericUpDown MaxMembersInPartyNUD;
        private CheckBox ShowPartyMemberHUDCB;
        private CheckBox CanCreatePartyMarkersCB;
        private CheckBox ShowNameOnQuickMarkersCB;
        private CheckBox ShowDistanceUnderPartyMembersMarkersCB;
        private CheckBox ShowNameOnPartyMembersMarkersCB;
        private CheckBox ShowDistanceUnderQuickMarkersCB;
        private CheckBox EnableQuickMarkerCB;
        private CheckBox EnablePartiesCB;
        private CheckBox ShowPartyMember3DMarkersCB;
        private CheckBox UseWholeMapForInviteListCB;
    }
}
