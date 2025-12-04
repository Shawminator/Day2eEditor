namespace ExpansionPlugin
{
    partial class ExpansionHardlineLogsControl
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
            groupBox8 = new GroupBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            AIGeneralCB = new CheckBox();
            AIObjectPatrolCB = new CheckBox();
            AIPatrolCB = new CheckBox();
            ATMCB = new CheckBox();
            AdminToolsCB = new CheckBox();
            BaseBuildingRaidingCB = new CheckBox();
            ChatCB = new CheckBox();
            CodeLockRaidingCB = new CheckBox();
            EntityStorageCB = new CheckBox();
            ExplosionDamageSystemCB = new CheckBox();
            GarageCB = new CheckBox();
            HardlineCB = new CheckBox();
            KillfeedCB = new CheckBox();
            LogToADMCB = new CheckBox();
            LogToScriptsCB = new CheckBox();
            MarketCB = new CheckBox();
            MissionAirdropCB = new CheckBox();
            PartyCB = new CheckBox();
            QuestsCB = new CheckBox();
            SafezoneCB = new CheckBox();
            SpawnSelectionCB = new CheckBox();
            TerritoryCB = new CheckBox();
            VehicleAttachmentsCB = new CheckBox();
            VehicleCarKeyCB = new CheckBox();
            VehicleCoverCB = new CheckBox();
            VehicleDeletedCB = new CheckBox();
            VehicleDestroyedCB = new CheckBox();
            VehicleEngineCB = new CheckBox();
            VehicleEnterCB = new CheckBox();
            VehicleLeaveCB = new CheckBox();
            VehicleLockPickingCB = new CheckBox();
            VehicleTowingCB = new CheckBox();
            groupBox8.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(flowLayoutPanel1);
            groupBox8.ForeColor = SystemColors.Control;
            groupBox8.Location = new Point(0, 0);
            groupBox8.Margin = new Padding(4, 3, 4, 3);
            groupBox8.Name = "groupBox8";
            groupBox8.Padding = new Padding(4, 3, 4, 3);
            groupBox8.Size = new Size(631, 227);
            groupBox8.TabIndex = 24;
            groupBox8.TabStop = false;
            groupBox8.Text = "Logs";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(AIGeneralCB);
            flowLayoutPanel1.Controls.Add(AIObjectPatrolCB);
            flowLayoutPanel1.Controls.Add(AIPatrolCB);
            flowLayoutPanel1.Controls.Add(ATMCB);
            flowLayoutPanel1.Controls.Add(AdminToolsCB);
            flowLayoutPanel1.Controls.Add(BaseBuildingRaidingCB);
            flowLayoutPanel1.Controls.Add(ChatCB);
            flowLayoutPanel1.Controls.Add(CodeLockRaidingCB);
            flowLayoutPanel1.Controls.Add(EntityStorageCB);
            flowLayoutPanel1.Controls.Add(ExplosionDamageSystemCB);
            flowLayoutPanel1.Controls.Add(GarageCB);
            flowLayoutPanel1.Controls.Add(HardlineCB);
            flowLayoutPanel1.Controls.Add(KillfeedCB);
            flowLayoutPanel1.Controls.Add(LogToADMCB);
            flowLayoutPanel1.Controls.Add(LogToScriptsCB);
            flowLayoutPanel1.Controls.Add(MarketCB);
            flowLayoutPanel1.Controls.Add(MissionAirdropCB);
            flowLayoutPanel1.Controls.Add(PartyCB);
            flowLayoutPanel1.Controls.Add(QuestsCB);
            flowLayoutPanel1.Controls.Add(SafezoneCB);
            flowLayoutPanel1.Controls.Add(SpawnSelectionCB);
            flowLayoutPanel1.Controls.Add(TerritoryCB);
            flowLayoutPanel1.Controls.Add(VehicleAttachmentsCB);
            flowLayoutPanel1.Controls.Add(VehicleCarKeyCB);
            flowLayoutPanel1.Controls.Add(VehicleCoverCB);
            flowLayoutPanel1.Controls.Add(VehicleDeletedCB);
            flowLayoutPanel1.Controls.Add(VehicleDestroyedCB);
            flowLayoutPanel1.Controls.Add(VehicleEngineCB);
            flowLayoutPanel1.Controls.Add(VehicleEnterCB);
            flowLayoutPanel1.Controls.Add(VehicleLeaveCB);
            flowLayoutPanel1.Controls.Add(VehicleLockPickingCB);
            flowLayoutPanel1.Controls.Add(VehicleTowingCB);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(4, 19);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(623, 205);
            flowLayoutPanel1.TabIndex = 32;
            // 
            // AIGeneralCB
            // 
            AIGeneralCB.AutoSize = true;
            AIGeneralCB.Location = new Point(4, 3);
            AIGeneralCB.Margin = new Padding(4, 3, 4, 3);
            AIGeneralCB.Name = "AIGeneralCB";
            AIGeneralCB.Size = new Size(80, 19);
            AIGeneralCB.TabIndex = 1;
            AIGeneralCB.Text = "AI General";
            AIGeneralCB.UseVisualStyleBackColor = true;
            AIGeneralCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // AIObjectPatrolCB
            // 
            AIObjectPatrolCB.AutoSize = true;
            AIObjectPatrolCB.Location = new Point(4, 28);
            AIObjectPatrolCB.Margin = new Padding(4, 3, 4, 3);
            AIObjectPatrolCB.Name = "AIObjectPatrolCB";
            AIObjectPatrolCB.Size = new Size(109, 19);
            AIObjectPatrolCB.TabIndex = 2;
            AIObjectPatrolCB.Text = "AI Object Patrol";
            AIObjectPatrolCB.UseVisualStyleBackColor = true;
            AIObjectPatrolCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // AIPatrolCB
            // 
            AIPatrolCB.AutoSize = true;
            AIPatrolCB.Location = new Point(4, 53);
            AIPatrolCB.Margin = new Padding(4, 3, 4, 3);
            AIPatrolCB.Name = "AIPatrolCB";
            AIPatrolCB.Size = new Size(71, 19);
            AIPatrolCB.TabIndex = 3;
            AIPatrolCB.Text = "AI Patrol";
            AIPatrolCB.UseVisualStyleBackColor = true;
            AIPatrolCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // ATMCB
            // 
            ATMCB.AutoSize = true;
            ATMCB.ForeColor = SystemColors.Control;
            ATMCB.Location = new Point(4, 78);
            ATMCB.Margin = new Padding(4, 3, 4, 3);
            ATMCB.Name = "ATMCB";
            ATMCB.Size = new Size(50, 19);
            ATMCB.TabIndex = 4;
            ATMCB.Text = "ATM";
            ATMCB.TextAlign = ContentAlignment.MiddleRight;
            ATMCB.UseVisualStyleBackColor = true;
            ATMCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // AdminToolsCB
            // 
            AdminToolsCB.AutoSize = true;
            AdminToolsCB.ForeColor = SystemColors.Control;
            AdminToolsCB.Location = new Point(4, 103);
            AdminToolsCB.Margin = new Padding(4, 3, 4, 3);
            AdminToolsCB.Name = "AdminToolsCB";
            AdminToolsCB.Size = new Size(89, 19);
            AdminToolsCB.TabIndex = 0;
            AdminToolsCB.Text = "AdminTools";
            AdminToolsCB.TextAlign = ContentAlignment.MiddleRight;
            AdminToolsCB.UseVisualStyleBackColor = true;
            AdminToolsCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // BaseBuildingRaidingCB
            // 
            BaseBuildingRaidingCB.AutoSize = true;
            BaseBuildingRaidingCB.ForeColor = SystemColors.Control;
            BaseBuildingRaidingCB.Location = new Point(4, 128);
            BaseBuildingRaidingCB.Margin = new Padding(4, 3, 4, 3);
            BaseBuildingRaidingCB.Name = "BaseBuildingRaidingCB";
            BaseBuildingRaidingCB.Size = new Size(140, 19);
            BaseBuildingRaidingCB.TabIndex = 5;
            BaseBuildingRaidingCB.Text = "Base Building Raiding";
            BaseBuildingRaidingCB.TextAlign = ContentAlignment.MiddleRight;
            BaseBuildingRaidingCB.UseVisualStyleBackColor = true;
            BaseBuildingRaidingCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // ChatCB
            // 
            ChatCB.AutoSize = true;
            ChatCB.ForeColor = SystemColors.Control;
            ChatCB.Location = new Point(4, 153);
            ChatCB.Margin = new Padding(4, 3, 4, 3);
            ChatCB.Name = "ChatCB";
            ChatCB.Size = new Size(51, 19);
            ChatCB.TabIndex = 6;
            ChatCB.Text = "Chat";
            ChatCB.TextAlign = ContentAlignment.MiddleRight;
            ChatCB.UseVisualStyleBackColor = true;
            ChatCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // CodeLockRaidingCB
            // 
            CodeLockRaidingCB.AutoSize = true;
            CodeLockRaidingCB.ForeColor = SystemColors.Control;
            CodeLockRaidingCB.Location = new Point(4, 178);
            CodeLockRaidingCB.Margin = new Padding(4, 3, 4, 3);
            CodeLockRaidingCB.Name = "CodeLockRaidingCB";
            CodeLockRaidingCB.Size = new Size(122, 19);
            CodeLockRaidingCB.TabIndex = 7;
            CodeLockRaidingCB.Text = "CodeLock Raiding";
            CodeLockRaidingCB.TextAlign = ContentAlignment.MiddleRight;
            CodeLockRaidingCB.UseVisualStyleBackColor = true;
            CodeLockRaidingCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // EntityStorageCB
            // 
            EntityStorageCB.AutoSize = true;
            EntityStorageCB.ForeColor = SystemColors.Control;
            EntityStorageCB.Location = new Point(152, 3);
            EntityStorageCB.Margin = new Padding(4, 3, 4, 3);
            EntityStorageCB.Name = "EntityStorageCB";
            EntityStorageCB.Size = new Size(99, 19);
            EntityStorageCB.TabIndex = 8;
            EntityStorageCB.Text = "Entity Storage";
            EntityStorageCB.TextAlign = ContentAlignment.MiddleRight;
            EntityStorageCB.UseVisualStyleBackColor = true;
            EntityStorageCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // ExplosionDamageSystemCB
            // 
            ExplosionDamageSystemCB.AutoSize = true;
            ExplosionDamageSystemCB.ForeColor = SystemColors.Control;
            ExplosionDamageSystemCB.Location = new Point(152, 28);
            ExplosionDamageSystemCB.Margin = new Padding(4, 3, 4, 3);
            ExplosionDamageSystemCB.Name = "ExplosionDamageSystemCB";
            ExplosionDamageSystemCB.Size = new Size(165, 19);
            ExplosionDamageSystemCB.TabIndex = 9;
            ExplosionDamageSystemCB.Text = "Explosion Damage System";
            ExplosionDamageSystemCB.TextAlign = ContentAlignment.MiddleRight;
            ExplosionDamageSystemCB.UseVisualStyleBackColor = true;
            ExplosionDamageSystemCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // GarageCB
            // 
            GarageCB.AutoSize = true;
            GarageCB.Location = new Point(152, 53);
            GarageCB.Margin = new Padding(4, 3, 4, 3);
            GarageCB.Name = "GarageCB";
            GarageCB.Size = new Size(63, 19);
            GarageCB.TabIndex = 10;
            GarageCB.Text = "Garage";
            GarageCB.UseVisualStyleBackColor = true;
            GarageCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // HardlineCB
            // 
            HardlineCB.AutoSize = true;
            HardlineCB.Location = new Point(152, 78);
            HardlineCB.Margin = new Padding(4, 3, 4, 3);
            HardlineCB.Name = "HardlineCB";
            HardlineCB.Size = new Size(71, 19);
            HardlineCB.TabIndex = 11;
            HardlineCB.Text = "Hardline";
            HardlineCB.UseVisualStyleBackColor = true;
            HardlineCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // KillfeedCB
            // 
            KillfeedCB.AutoSize = true;
            KillfeedCB.ForeColor = SystemColors.Control;
            KillfeedCB.Location = new Point(152, 103);
            KillfeedCB.Margin = new Padding(4, 3, 4, 3);
            KillfeedCB.Name = "KillfeedCB";
            KillfeedCB.Size = new Size(65, 19);
            KillfeedCB.TabIndex = 12;
            KillfeedCB.Text = "Killfeed";
            KillfeedCB.TextAlign = ContentAlignment.MiddleRight;
            KillfeedCB.UseVisualStyleBackColor = true;
            KillfeedCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // LogToADMCB
            // 
            LogToADMCB.AutoSize = true;
            LogToADMCB.Location = new Point(152, 128);
            LogToADMCB.Margin = new Padding(4, 3, 4, 3);
            LogToADMCB.Name = "LogToADMCB";
            LogToADMCB.Size = new Size(91, 19);
            LogToADMCB.TabIndex = 13;
            LogToADMCB.Text = "Log To ADM";
            LogToADMCB.UseVisualStyleBackColor = true;
            LogToADMCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // LogToScriptsCB
            // 
            LogToScriptsCB.AutoSize = true;
            LogToScriptsCB.Location = new Point(152, 153);
            LogToScriptsCB.Margin = new Padding(4, 3, 4, 3);
            LogToScriptsCB.Name = "LogToScriptsCB";
            LogToScriptsCB.Size = new Size(99, 19);
            LogToScriptsCB.TabIndex = 14;
            LogToScriptsCB.Text = "Log To Scripts";
            LogToScriptsCB.UseVisualStyleBackColor = true;
            LogToScriptsCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // MarketCB
            // 
            MarketCB.AutoSize = true;
            MarketCB.ForeColor = SystemColors.Control;
            MarketCB.Location = new Point(152, 178);
            MarketCB.Margin = new Padding(4, 3, 4, 3);
            MarketCB.Name = "MarketCB";
            MarketCB.Size = new Size(63, 19);
            MarketCB.TabIndex = 15;
            MarketCB.Text = "Market";
            MarketCB.TextAlign = ContentAlignment.MiddleRight;
            MarketCB.UseVisualStyleBackColor = true;
            MarketCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // MissionAirdropCB
            // 
            MissionAirdropCB.AutoSize = true;
            MissionAirdropCB.ForeColor = SystemColors.Control;
            MissionAirdropCB.Location = new Point(325, 3);
            MissionAirdropCB.Margin = new Padding(4, 3, 4, 3);
            MissionAirdropCB.Name = "MissionAirdropCB";
            MissionAirdropCB.Size = new Size(110, 19);
            MissionAirdropCB.TabIndex = 16;
            MissionAirdropCB.Text = "Mission Airdrop";
            MissionAirdropCB.TextAlign = ContentAlignment.MiddleRight;
            MissionAirdropCB.UseVisualStyleBackColor = true;
            MissionAirdropCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // PartyCB
            // 
            PartyCB.AutoSize = true;
            PartyCB.ForeColor = SystemColors.Control;
            PartyCB.Location = new Point(325, 28);
            PartyCB.Margin = new Padding(4, 3, 4, 3);
            PartyCB.Name = "PartyCB";
            PartyCB.Size = new Size(53, 19);
            PartyCB.TabIndex = 17;
            PartyCB.Text = "Party";
            PartyCB.TextAlign = ContentAlignment.MiddleRight;
            PartyCB.UseVisualStyleBackColor = true;
            PartyCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // QuestsCB
            // 
            QuestsCB.AutoSize = true;
            QuestsCB.ForeColor = SystemColors.Control;
            QuestsCB.Location = new Point(325, 53);
            QuestsCB.Margin = new Padding(4, 3, 4, 3);
            QuestsCB.Name = "QuestsCB";
            QuestsCB.Size = new Size(62, 19);
            QuestsCB.TabIndex = 18;
            QuestsCB.Text = "Quests";
            QuestsCB.TextAlign = ContentAlignment.MiddleRight;
            QuestsCB.UseVisualStyleBackColor = true;
            QuestsCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // SafezoneCB
            // 
            SafezoneCB.AutoSize = true;
            SafezoneCB.ForeColor = SystemColors.Control;
            SafezoneCB.Location = new Point(325, 78);
            SafezoneCB.Margin = new Padding(4, 3, 4, 3);
            SafezoneCB.Name = "SafezoneCB";
            SafezoneCB.Size = new Size(73, 19);
            SafezoneCB.TabIndex = 19;
            SafezoneCB.Text = "Safezone";
            SafezoneCB.TextAlign = ContentAlignment.MiddleRight;
            SafezoneCB.UseVisualStyleBackColor = true;
            SafezoneCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // SpawnSelectionCB
            // 
            SpawnSelectionCB.AutoSize = true;
            SpawnSelectionCB.ForeColor = SystemColors.Control;
            SpawnSelectionCB.Location = new Point(325, 103);
            SpawnSelectionCB.Margin = new Padding(4, 3, 4, 3);
            SpawnSelectionCB.Name = "SpawnSelectionCB";
            SpawnSelectionCB.Size = new Size(112, 19);
            SpawnSelectionCB.TabIndex = 20;
            SpawnSelectionCB.Text = "Spawn Selection";
            SpawnSelectionCB.TextAlign = ContentAlignment.MiddleRight;
            SpawnSelectionCB.UseVisualStyleBackColor = true;
            SpawnSelectionCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // TerritoryCB
            // 
            TerritoryCB.AutoSize = true;
            TerritoryCB.ForeColor = SystemColors.Control;
            TerritoryCB.Location = new Point(325, 128);
            TerritoryCB.Margin = new Padding(4, 3, 4, 3);
            TerritoryCB.Name = "TerritoryCB";
            TerritoryCB.Size = new Size(69, 19);
            TerritoryCB.TabIndex = 21;
            TerritoryCB.Text = "Territory";
            TerritoryCB.TextAlign = ContentAlignment.MiddleRight;
            TerritoryCB.UseVisualStyleBackColor = true;
            TerritoryCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // VehicleAttachmentsCB
            // 
            VehicleAttachmentsCB.AutoSize = true;
            VehicleAttachmentsCB.ForeColor = SystemColors.Control;
            VehicleAttachmentsCB.Location = new Point(325, 153);
            VehicleAttachmentsCB.Margin = new Padding(4, 3, 4, 3);
            VehicleAttachmentsCB.Name = "VehicleAttachmentsCB";
            VehicleAttachmentsCB.Size = new Size(134, 19);
            VehicleAttachmentsCB.TabIndex = 22;
            VehicleAttachmentsCB.Text = "Vehicle Attachments";
            VehicleAttachmentsCB.TextAlign = ContentAlignment.MiddleRight;
            VehicleAttachmentsCB.UseVisualStyleBackColor = true;
            VehicleAttachmentsCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // VehicleCarKeyCB
            // 
            VehicleCarKeyCB.AutoSize = true;
            VehicleCarKeyCB.ForeColor = SystemColors.Control;
            VehicleCarKeyCB.Location = new Point(325, 178);
            VehicleCarKeyCB.Margin = new Padding(4, 3, 4, 3);
            VehicleCarKeyCB.Name = "VehicleCarKeyCB";
            VehicleCarKeyCB.Size = new Size(106, 19);
            VehicleCarKeyCB.TabIndex = 23;
            VehicleCarKeyCB.Text = "Vehicle Car Key";
            VehicleCarKeyCB.TextAlign = ContentAlignment.MiddleRight;
            VehicleCarKeyCB.UseVisualStyleBackColor = true;
            VehicleCarKeyCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // VehicleCoverCB
            // 
            VehicleCoverCB.AutoSize = true;
            VehicleCoverCB.ForeColor = SystemColors.Control;
            VehicleCoverCB.Location = new Point(467, 3);
            VehicleCoverCB.Margin = new Padding(4, 3, 4, 3);
            VehicleCoverCB.Name = "VehicleCoverCB";
            VehicleCoverCB.Size = new Size(97, 19);
            VehicleCoverCB.TabIndex = 24;
            VehicleCoverCB.Text = "Vehicle Cover";
            VehicleCoverCB.TextAlign = ContentAlignment.MiddleRight;
            VehicleCoverCB.UseVisualStyleBackColor = true;
            VehicleCoverCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // VehicleDeletedCB
            // 
            VehicleDeletedCB.AutoSize = true;
            VehicleDeletedCB.ForeColor = SystemColors.Control;
            VehicleDeletedCB.Location = new Point(467, 28);
            VehicleDeletedCB.Margin = new Padding(4, 3, 4, 3);
            VehicleDeletedCB.Name = "VehicleDeletedCB";
            VehicleDeletedCB.Size = new Size(106, 19);
            VehicleDeletedCB.TabIndex = 25;
            VehicleDeletedCB.Text = "Vehicle Deleted";
            VehicleDeletedCB.TextAlign = ContentAlignment.MiddleRight;
            VehicleDeletedCB.UseVisualStyleBackColor = true;
            VehicleDeletedCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // VehicleDestroyedCB
            // 
            VehicleDestroyedCB.AutoSize = true;
            VehicleDestroyedCB.ForeColor = SystemColors.Control;
            VehicleDestroyedCB.Location = new Point(467, 53);
            VehicleDestroyedCB.Margin = new Padding(4, 3, 4, 3);
            VehicleDestroyedCB.Name = "VehicleDestroyedCB";
            VehicleDestroyedCB.Size = new Size(119, 19);
            VehicleDestroyedCB.TabIndex = 29;
            VehicleDestroyedCB.Text = "Vehicle Destroyed";
            VehicleDestroyedCB.TextAlign = ContentAlignment.MiddleRight;
            VehicleDestroyedCB.UseVisualStyleBackColor = true;
            VehicleDestroyedCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // VehicleEngineCB
            // 
            VehicleEngineCB.AutoSize = true;
            VehicleEngineCB.ForeColor = SystemColors.Control;
            VehicleEngineCB.Location = new Point(467, 78);
            VehicleEngineCB.Margin = new Padding(4, 3, 4, 3);
            VehicleEngineCB.Name = "VehicleEngineCB";
            VehicleEngineCB.Size = new Size(102, 19);
            VehicleEngineCB.TabIndex = 27;
            VehicleEngineCB.Text = "Vehicle Engine";
            VehicleEngineCB.TextAlign = ContentAlignment.MiddleRight;
            VehicleEngineCB.UseVisualStyleBackColor = true;
            VehicleEngineCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // VehicleEnterCB
            // 
            VehicleEnterCB.AutoSize = true;
            VehicleEnterCB.ForeColor = SystemColors.Control;
            VehicleEnterCB.Location = new Point(467, 103);
            VehicleEnterCB.Margin = new Padding(4, 3, 4, 3);
            VehicleEnterCB.Name = "VehicleEnterCB";
            VehicleEnterCB.Size = new Size(93, 19);
            VehicleEnterCB.TabIndex = 28;
            VehicleEnterCB.Text = "Vehicle Enter";
            VehicleEnterCB.TextAlign = ContentAlignment.MiddleRight;
            VehicleEnterCB.UseVisualStyleBackColor = true;
            VehicleEnterCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // VehicleLeaveCB
            // 
            VehicleLeaveCB.AutoSize = true;
            VehicleLeaveCB.ForeColor = SystemColors.Control;
            VehicleLeaveCB.Location = new Point(467, 128);
            VehicleLeaveCB.Margin = new Padding(4, 3, 4, 3);
            VehicleLeaveCB.Name = "VehicleLeaveCB";
            VehicleLeaveCB.Size = new Size(96, 19);
            VehicleLeaveCB.TabIndex = 26;
            VehicleLeaveCB.Text = "Vehicle Leave";
            VehicleLeaveCB.TextAlign = ContentAlignment.MiddleRight;
            VehicleLeaveCB.UseVisualStyleBackColor = true;
            VehicleLeaveCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // VehicleLockPickingCB
            // 
            VehicleLockPickingCB.AutoSize = true;
            VehicleLockPickingCB.ForeColor = SystemColors.Control;
            VehicleLockPickingCB.Location = new Point(467, 153);
            VehicleLockPickingCB.Margin = new Padding(4, 3, 4, 3);
            VehicleLockPickingCB.Name = "VehicleLockPickingCB";
            VehicleLockPickingCB.Size = new Size(133, 19);
            VehicleLockPickingCB.TabIndex = 30;
            VehicleLockPickingCB.Text = "Vehicle Lock Picking";
            VehicleLockPickingCB.TextAlign = ContentAlignment.MiddleRight;
            VehicleLockPickingCB.UseVisualStyleBackColor = true;
            VehicleLockPickingCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // VehicleTowingCB
            // 
            VehicleTowingCB.AutoSize = true;
            VehicleTowingCB.ForeColor = SystemColors.Control;
            VehicleTowingCB.Location = new Point(467, 178);
            VehicleTowingCB.Margin = new Padding(4, 3, 4, 3);
            VehicleTowingCB.Name = "VehicleTowingCB";
            VehicleTowingCB.Size = new Size(104, 19);
            VehicleTowingCB.TabIndex = 31;
            VehicleTowingCB.Text = "Vehicle Towing";
            VehicleTowingCB.TextAlign = ContentAlignment.MiddleRight;
            VehicleTowingCB.UseVisualStyleBackColor = true;
            VehicleTowingCB.CheckedChanged += LogSettingsCB_CheckedChanged;
            // 
            // ExpansionHardlineLogsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            Controls.Add(groupBox8);
            ForeColor = SystemColors.Control;
            Name = "ExpansionHardlineLogsControl";
            Size = new Size(631, 227);
            groupBox8.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox8;
        private CheckBox QuestsCB;
        private CheckBox VehicleCoverCB;
        private CheckBox GarageCB;
        private CheckBox EntityStorageCB;
        private CheckBox VehicleLeaveCB;
        private CheckBox VehicleEnterCB;
        private CheckBox VehicleEngineCB;
        private CheckBox VehicleDeletedCB;
        private CheckBox VehicleAttachmentsCB;
        private CheckBox VehicleDestroyedCB;
        private CheckBox HardlineCB;
        private CheckBox ExplosionDamageSystemCB;
        private CheckBox AIPatrolCB;
        private CheckBox AIGeneralCB;
        private CheckBox VehicleTowingCB;
        private CheckBox VehicleLockPickingCB;
        private CheckBox AIObjectPatrolCB;
        private CheckBox ATMCB;
        private CheckBox MarketCB;
        private CheckBox LogToADMCB;
        private CheckBox MissionAirdropCB;
        private CheckBox SpawnSelectionCB;
        private CheckBox LogToScriptsCB;
        private CheckBox AdminToolsCB;
        private CheckBox ChatCB;
        private CheckBox PartyCB;
        private CheckBox BaseBuildingRaidingCB;
        private CheckBox CodeLockRaidingCB;
        private CheckBox KillfeedCB;
        private CheckBox TerritoryCB;
        private CheckBox SafezoneCB;
        private CheckBox VehicleCarKeyCB;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}
