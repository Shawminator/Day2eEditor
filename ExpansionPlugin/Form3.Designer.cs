namespace DayZeEditor
{
    partial class Form3
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
            groupBox75 = new GroupBox();
            darkButton2 = new Button();
            darkButton72 = new Button();
            darkButton70 = new Button();
            darkButton71 = new Button();
            darkLabel237 = new Label();
            ItemRarityLB = new ListBox();
            ItemRarityCB = new ComboBox();
            ItemRequirementNUD = new NumericUpDown();
            darkLabel1 = new Label();
            groupBox8 = new GroupBox();
            QuestsCB = new CheckBox();
            VehicleCoverCB = new CheckBox();
            GarageCB = new CheckBox();
            EntityStorageCB = new CheckBox();
            VehicleLeaveCB = new CheckBox();
            VehicleEnterCB = new CheckBox();
            VehicleEngineCB = new CheckBox();
            VehicleDeletedCB = new CheckBox();
            VehicleAttachmentsCB = new CheckBox();
            VehicleDestroyedCB = new CheckBox();
            HardlineCB = new CheckBox();
            ExplosionDamageSystemCB = new CheckBox();
            AIPatrolCB = new CheckBox();
            AIGeneralCB = new CheckBox();
            VehicleTowingCB = new CheckBox();
            VehicleLockPickingCB = new CheckBox();
            AIObjectPatrolCB = new CheckBox();
            ATMCB = new CheckBox();
            MarketCB = new CheckBox();
            LogToADMCB = new CheckBox();
            MissionAirdropCB = new CheckBox();
            SpawnSelectionCB = new CheckBox();
            LogToScriptsCB = new CheckBox();
            AdminToolsCB = new CheckBox();
            ChatCB = new CheckBox();
            PartyCB = new CheckBox();
            BaseBuildingRaidingCB = new CheckBox();
            CodeLockRaidingCB = new CheckBox();
            KillfeedCB = new CheckBox();
            TerritoryCB = new CheckBox();
            SafezoneCB = new CheckBox();
            VehicleCarKeyCB = new CheckBox();
            groupBox75.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ItemRequirementNUD).BeginInit();
            groupBox8.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox75
            // 
            groupBox75.Controls.Add(darkButton2);
            groupBox75.Controls.Add(darkButton72);
            groupBox75.Controls.Add(darkButton70);
            groupBox75.Controls.Add(darkButton71);
            groupBox75.Controls.Add(darkLabel237);
            groupBox75.Controls.Add(ItemRarityLB);
            groupBox75.Controls.Add(ItemRarityCB);
            groupBox75.Controls.Add(ItemRequirementNUD);
            groupBox75.Controls.Add(darkLabel1);
            groupBox75.ForeColor = SystemColors.Control;
            groupBox75.Location = new Point(13, 12);
            groupBox75.Margin = new Padding(4, 3, 4, 3);
            groupBox75.Name = "groupBox75";
            groupBox75.Padding = new Padding(4, 3, 4, 3);
            groupBox75.Size = new Size(296, 615);
            groupBox75.TabIndex = 22;
            groupBox75.TabStop = false;
            groupBox75.Text = "Requirments and Items";
            // 
            // darkButton2
            // 
            darkButton2.FlatStyle = FlatStyle.Flat;
            darkButton2.Location = new Point(9, 582);
            darkButton2.Margin = new Padding(4, 3, 4, 3);
            darkButton2.Name = "darkButton2";
            darkButton2.Size = new Size(275, 27);
            darkButton2.TabIndex = 8;
            darkButton2.Text = "Assign All From CE Rarity";
            // 
            // darkButton72
            // 
            darkButton72.FlatStyle = FlatStyle.Flat;
            darkButton72.Location = new Point(97, 548);
            darkButton72.Margin = new Padding(4, 3, 4, 3);
            darkButton72.Name = "darkButton72";
            darkButton72.Size = new Size(97, 27);
            darkButton72.TabIndex = 6;
            darkButton72.Text = "From String";
            // 
            // darkButton70
            // 
            darkButton70.FlatStyle = FlatStyle.Flat;
            darkButton70.Location = new Point(201, 548);
            darkButton70.Margin = new Padding(4, 3, 4, 3);
            darkButton70.Name = "darkButton70";
            darkButton70.Size = new Size(84, 27);
            darkButton70.TabIndex = 7;
            darkButton70.Text = "Remove";
            // 
            // darkButton71
            // 
            darkButton71.FlatStyle = FlatStyle.Flat;
            darkButton71.Location = new Point(9, 548);
            darkButton71.Margin = new Padding(4, 3, 4, 3);
            darkButton71.Name = "darkButton71";
            darkButton71.Size = new Size(80, 27);
            darkButton71.TabIndex = 5;
            darkButton71.Text = "Add New";
            // 
            // darkLabel237
            // 
            darkLabel237.AutoSize = true;
            darkLabel237.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel237.Location = new Point(18, 58);
            darkLabel237.Margin = new Padding(4, 0, 4, 0);
            darkLabel237.Name = "darkLabel237";
            darkLabel237.Size = new Size(75, 15);
            darkLabel237.TabIndex = 2;
            darkLabel237.Tag = "";
            darkLabel237.Text = "Requirement";
            // 
            // ItemRarityLB
            // 
            ItemRarityLB.BackColor = Color.FromArgb(60, 63, 65);
            ItemRarityLB.DrawMode = DrawMode.OwnerDrawFixed;
            ItemRarityLB.ForeColor = SystemColors.Control;
            ItemRarityLB.FormattingEnabled = true;
            ItemRarityLB.Location = new Point(9, 85);
            ItemRarityLB.Margin = new Padding(4, 3, 4, 3);
            ItemRarityLB.Name = "ItemRarityLB";
            ItemRarityLB.Size = new Size(275, 452);
            ItemRarityLB.TabIndex = 4;
            // 
            // ItemRarityCB
            // 
            ItemRarityCB.FormattingEnabled = true;
            ItemRarityCB.Items.AddRange(new object[] { "Poor", "Common", "Uncommon", "Rare", "Epic", "Legendary", "Mythic", "Exotic", "Quest", "Collectable", "Ingredient" });
            ItemRarityCB.Location = new Point(85, 24);
            ItemRarityCB.Margin = new Padding(4, 3, 4, 3);
            ItemRarityCB.Name = "ItemRarityCB";
            ItemRarityCB.Size = new Size(199, 23);
            ItemRarityCB.TabIndex = 1;
            // 
            // ItemRequirementNUD
            // 
            ItemRequirementNUD.BackColor = Color.FromArgb(60, 63, 65);
            ItemRequirementNUD.ForeColor = SystemColors.Control;
            ItemRequirementNUD.Location = new Point(167, 55);
            ItemRequirementNUD.Margin = new Padding(4, 3, 4, 3);
            ItemRequirementNUD.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            ItemRequirementNUD.Minimum = new decimal(new int[] { 999999999, 0, 0, int.MinValue });
            ItemRequirementNUD.Name = "ItemRequirementNUD";
            ItemRequirementNUD.Size = new Size(118, 23);
            ItemRequirementNUD.TabIndex = 3;
            ItemRequirementNUD.Tag = "Weight";
            ItemRequirementNUD.TextAlign = HorizontalAlignment.Center;
            ItemRequirementNUD.ValueChanged += ItemRequirementNUD_ValueChanged;
            // 
            // darkLabel1
            // 
            darkLabel1.AutoSize = true;
            darkLabel1.ForeColor = Color.FromArgb(220, 220, 220);
            darkLabel1.Location = new Point(18, 28);
            darkLabel1.Margin = new Padding(4, 0, 4, 0);
            darkLabel1.Name = "darkLabel1";
            darkLabel1.Size = new Size(34, 15);
            darkLabel1.TabIndex = 0;
            darkLabel1.Tag = "";
            darkLabel1.Text = "Level";
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(QuestsCB);
            groupBox8.Controls.Add(VehicleCoverCB);
            groupBox8.Controls.Add(GarageCB);
            groupBox8.Controls.Add(EntityStorageCB);
            groupBox8.Controls.Add(VehicleLeaveCB);
            groupBox8.Controls.Add(VehicleEnterCB);
            groupBox8.Controls.Add(VehicleEngineCB);
            groupBox8.Controls.Add(VehicleDeletedCB);
            groupBox8.Controls.Add(VehicleAttachmentsCB);
            groupBox8.Controls.Add(VehicleDestroyedCB);
            groupBox8.Controls.Add(HardlineCB);
            groupBox8.Controls.Add(ExplosionDamageSystemCB);
            groupBox8.Controls.Add(AIPatrolCB);
            groupBox8.Controls.Add(AIGeneralCB);
            groupBox8.Controls.Add(VehicleTowingCB);
            groupBox8.Controls.Add(VehicleLockPickingCB);
            groupBox8.Controls.Add(AIObjectPatrolCB);
            groupBox8.Controls.Add(ATMCB);
            groupBox8.Controls.Add(MarketCB);
            groupBox8.Controls.Add(LogToADMCB);
            groupBox8.Controls.Add(MissionAirdropCB);
            groupBox8.Controls.Add(SpawnSelectionCB);
            groupBox8.Controls.Add(LogToScriptsCB);
            groupBox8.Controls.Add(AdminToolsCB);
            groupBox8.Controls.Add(ChatCB);
            groupBox8.Controls.Add(PartyCB);
            groupBox8.Controls.Add(BaseBuildingRaidingCB);
            groupBox8.Controls.Add(CodeLockRaidingCB);
            groupBox8.Controls.Add(KillfeedCB);
            groupBox8.Controls.Add(TerritoryCB);
            groupBox8.Controls.Add(SafezoneCB);
            groupBox8.Controls.Add(VehicleCarKeyCB);
            groupBox8.ForeColor = SystemColors.Control;
            groupBox8.Location = new Point(330, 30);
            groupBox8.Margin = new Padding(4, 3, 4, 3);
            groupBox8.Name = "groupBox8";
            groupBox8.Padding = new Padding(4, 3, 4, 3);
            groupBox8.Size = new Size(351, 270);
            groupBox8.TabIndex = 23;
            groupBox8.TabStop = false;
            groupBox8.Text = "Logs";
            // 
            // QuestsCB
            // 
            QuestsCB.AutoSize = true;
            QuestsCB.ForeColor = SystemColors.Control;
            QuestsCB.Location = new Point(264, 129);
            QuestsCB.Margin = new Padding(4, 3, 4, 3);
            QuestsCB.Name = "QuestsCB";
            QuestsCB.Size = new Size(62, 19);
            QuestsCB.TabIndex = 18;
            QuestsCB.Text = "Quests";
            QuestsCB.TextAlign = ContentAlignment.MiddleRight;
            QuestsCB.UseVisualStyleBackColor = true;
            // 
            // VehicleCoverCB
            // 
            VehicleCoverCB.AutoSize = true;
            VehicleCoverCB.ForeColor = SystemColors.Control;
            VehicleCoverCB.Location = new Point(8, 197);
            VehicleCoverCB.Margin = new Padding(4, 3, 4, 3);
            VehicleCoverCB.Name = "VehicleCoverCB";
            VehicleCoverCB.Size = new Size(97, 19);
            VehicleCoverCB.TabIndex = 24;
            VehicleCoverCB.Text = "Vehicle Cover";
            VehicleCoverCB.TextAlign = ContentAlignment.MiddleRight;
            VehicleCoverCB.UseVisualStyleBackColor = true;
            // 
            // GarageCB
            // 
            GarageCB.AutoSize = true;
            GarageCB.Location = new Point(183, 85);
            GarageCB.Margin = new Padding(4, 3, 4, 3);
            GarageCB.Name = "GarageCB";
            GarageCB.Size = new Size(63, 19);
            GarageCB.TabIndex = 10;
            GarageCB.Text = "Garage";
            GarageCB.UseVisualStyleBackColor = true;
            // 
            // EntityStorageCB
            // 
            EntityStorageCB.AutoSize = true;
            EntityStorageCB.ForeColor = SystemColors.Control;
            EntityStorageCB.Location = new Point(203, 62);
            EntityStorageCB.Margin = new Padding(4, 3, 4, 3);
            EntityStorageCB.Name = "EntityStorageCB";
            EntityStorageCB.Size = new Size(99, 19);
            EntityStorageCB.TabIndex = 8;
            EntityStorageCB.Text = "Entity Storage";
            EntityStorageCB.TextAlign = ContentAlignment.MiddleRight;
            EntityStorageCB.UseVisualStyleBackColor = true;
            // 
            // VehicleLeaveCB
            // 
            VehicleLeaveCB.AutoSize = true;
            VehicleLeaveCB.ForeColor = SystemColors.Control;
            VehicleLeaveCB.Location = new Point(237, 197);
            VehicleLeaveCB.Margin = new Padding(4, 3, 4, 3);
            VehicleLeaveCB.Name = "VehicleLeaveCB";
            VehicleLeaveCB.Size = new Size(96, 19);
            VehicleLeaveCB.TabIndex = 26;
            VehicleLeaveCB.Text = "Vehicle Leave";
            VehicleLeaveCB.TextAlign = ContentAlignment.MiddleRight;
            VehicleLeaveCB.UseVisualStyleBackColor = true;
            // 
            // VehicleEnterCB
            // 
            VehicleEnterCB.AutoSize = true;
            VehicleEnterCB.ForeColor = SystemColors.Control;
            VehicleEnterCB.Location = new Point(119, 220);
            VehicleEnterCB.Margin = new Padding(4, 3, 4, 3);
            VehicleEnterCB.Name = "VehicleEnterCB";
            VehicleEnterCB.Size = new Size(93, 19);
            VehicleEnterCB.TabIndex = 28;
            VehicleEnterCB.Text = "Vehicle Enter";
            VehicleEnterCB.TextAlign = ContentAlignment.MiddleRight;
            VehicleEnterCB.UseVisualStyleBackColor = true;
            // 
            // VehicleEngineCB
            // 
            VehicleEngineCB.AutoSize = true;
            VehicleEngineCB.ForeColor = SystemColors.Control;
            VehicleEngineCB.Location = new Point(8, 220);
            VehicleEngineCB.Margin = new Padding(4, 3, 4, 3);
            VehicleEngineCB.Name = "VehicleEngineCB";
            VehicleEngineCB.Size = new Size(102, 19);
            VehicleEngineCB.TabIndex = 27;
            VehicleEngineCB.Text = "Vehicle Engine";
            VehicleEngineCB.TextAlign = ContentAlignment.MiddleRight;
            VehicleEngineCB.UseVisualStyleBackColor = true;
            // 
            // VehicleDeletedCB
            // 
            VehicleDeletedCB.AutoSize = true;
            VehicleDeletedCB.ForeColor = SystemColors.Control;
            VehicleDeletedCB.Location = new Point(119, 197);
            VehicleDeletedCB.Margin = new Padding(4, 3, 4, 3);
            VehicleDeletedCB.Name = "VehicleDeletedCB";
            VehicleDeletedCB.Size = new Size(106, 19);
            VehicleDeletedCB.TabIndex = 25;
            VehicleDeletedCB.Text = "Vehicle Deleted";
            VehicleDeletedCB.TextAlign = ContentAlignment.MiddleRight;
            VehicleDeletedCB.UseVisualStyleBackColor = true;
            // 
            // VehicleAttachmentsCB
            // 
            VehicleAttachmentsCB.AutoSize = true;
            VehicleAttachmentsCB.ForeColor = SystemColors.Control;
            VehicleAttachmentsCB.Location = new Point(8, 174);
            VehicleAttachmentsCB.Margin = new Padding(4, 3, 4, 3);
            VehicleAttachmentsCB.Name = "VehicleAttachmentsCB";
            VehicleAttachmentsCB.Size = new Size(134, 19);
            VehicleAttachmentsCB.TabIndex = 22;
            VehicleAttachmentsCB.Text = "Vehicle Attachments";
            VehicleAttachmentsCB.TextAlign = ContentAlignment.MiddleRight;
            VehicleAttachmentsCB.UseVisualStyleBackColor = true;
            // 
            // VehicleDestroyedCB
            // 
            VehicleDestroyedCB.AutoSize = true;
            VehicleDestroyedCB.ForeColor = SystemColors.Control;
            VehicleDestroyedCB.Location = new Point(219, 219);
            VehicleDestroyedCB.Margin = new Padding(4, 3, 4, 3);
            VehicleDestroyedCB.Name = "VehicleDestroyedCB";
            VehicleDestroyedCB.Size = new Size(119, 19);
            VehicleDestroyedCB.TabIndex = 29;
            VehicleDestroyedCB.Text = "Vehicle Destroyed";
            VehicleDestroyedCB.TextAlign = ContentAlignment.MiddleRight;
            VehicleDestroyedCB.UseVisualStyleBackColor = true;
            // 
            // HardlineCB
            // 
            HardlineCB.AutoSize = true;
            HardlineCB.Location = new Point(261, 85);
            HardlineCB.Margin = new Padding(4, 3, 4, 3);
            HardlineCB.Name = "HardlineCB";
            HardlineCB.Size = new Size(71, 19);
            HardlineCB.TabIndex = 11;
            HardlineCB.Text = "Hardline";
            HardlineCB.UseVisualStyleBackColor = true;
            // 
            // ExplosionDamageSystemCB
            // 
            ExplosionDamageSystemCB.AutoSize = true;
            ExplosionDamageSystemCB.ForeColor = SystemColors.Control;
            ExplosionDamageSystemCB.Location = new Point(8, 85);
            ExplosionDamageSystemCB.Margin = new Padding(4, 3, 4, 3);
            ExplosionDamageSystemCB.Name = "ExplosionDamageSystemCB";
            ExplosionDamageSystemCB.Size = new Size(165, 19);
            ExplosionDamageSystemCB.TabIndex = 9;
            ExplosionDamageSystemCB.Text = "Explosion Damage System";
            ExplosionDamageSystemCB.TextAlign = ContentAlignment.MiddleRight;
            ExplosionDamageSystemCB.UseVisualStyleBackColor = true;
            // 
            // AIPatrolCB
            // 
            AIPatrolCB.AutoSize = true;
            AIPatrolCB.Location = new Point(8, 39);
            AIPatrolCB.Margin = new Padding(4, 3, 4, 3);
            AIPatrolCB.Name = "AIPatrolCB";
            AIPatrolCB.Size = new Size(71, 19);
            AIPatrolCB.TabIndex = 3;
            AIPatrolCB.Text = "AI Patrol";
            AIPatrolCB.UseVisualStyleBackColor = true;
            // 
            // AIGeneralCB
            // 
            AIGeneralCB.AutoSize = true;
            AIGeneralCB.Location = new Point(104, 16);
            AIGeneralCB.Margin = new Padding(4, 3, 4, 3);
            AIGeneralCB.Name = "AIGeneralCB";
            AIGeneralCB.Size = new Size(80, 19);
            AIGeneralCB.TabIndex = 1;
            AIGeneralCB.Text = "AI General";
            AIGeneralCB.UseVisualStyleBackColor = true;
            // 
            // VehicleTowingCB
            // 
            VehicleTowingCB.AutoSize = true;
            VehicleTowingCB.ForeColor = SystemColors.Control;
            VehicleTowingCB.Location = new Point(162, 246);
            VehicleTowingCB.Margin = new Padding(4, 3, 4, 3);
            VehicleTowingCB.Name = "VehicleTowingCB";
            VehicleTowingCB.Size = new Size(104, 19);
            VehicleTowingCB.TabIndex = 31;
            VehicleTowingCB.Text = "Vehicle Towing";
            VehicleTowingCB.TextAlign = ContentAlignment.MiddleRight;
            VehicleTowingCB.UseVisualStyleBackColor = true;
            // 
            // VehicleLockPickingCB
            // 
            VehicleLockPickingCB.AutoSize = true;
            VehicleLockPickingCB.ForeColor = SystemColors.Control;
            VehicleLockPickingCB.Location = new Point(8, 246);
            VehicleLockPickingCB.Margin = new Padding(4, 3, 4, 3);
            VehicleLockPickingCB.Name = "VehicleLockPickingCB";
            VehicleLockPickingCB.Size = new Size(133, 19);
            VehicleLockPickingCB.TabIndex = 30;
            VehicleLockPickingCB.Text = "Vehicle Lock Picking";
            VehicleLockPickingCB.TextAlign = ContentAlignment.MiddleRight;
            VehicleLockPickingCB.UseVisualStyleBackColor = true;
            // 
            // AIObjectPatrolCB
            // 
            AIObjectPatrolCB.AutoSize = true;
            AIObjectPatrolCB.Location = new Point(200, 16);
            AIObjectPatrolCB.Margin = new Padding(4, 3, 4, 3);
            AIObjectPatrolCB.Name = "AIObjectPatrolCB";
            AIObjectPatrolCB.Size = new Size(109, 19);
            AIObjectPatrolCB.TabIndex = 2;
            AIObjectPatrolCB.Text = "AI Object Patrol";
            AIObjectPatrolCB.UseVisualStyleBackColor = true;
            // 
            // ATMCB
            // 
            ATMCB.AutoSize = true;
            ATMCB.ForeColor = SystemColors.Control;
            ATMCB.Location = new Point(91, 39);
            ATMCB.Margin = new Padding(4, 3, 4, 3);
            ATMCB.Name = "ATMCB";
            ATMCB.Size = new Size(50, 19);
            ATMCB.TabIndex = 4;
            ATMCB.Text = "ATM";
            ATMCB.TextAlign = ContentAlignment.MiddleRight;
            ATMCB.UseVisualStyleBackColor = true;
            // 
            // MarketCB
            // 
            MarketCB.AutoSize = true;
            MarketCB.ForeColor = SystemColors.Control;
            MarketCB.Location = new Point(8, 129);
            MarketCB.Margin = new Padding(4, 3, 4, 3);
            MarketCB.Name = "MarketCB";
            MarketCB.Size = new Size(63, 19);
            MarketCB.TabIndex = 15;
            MarketCB.Text = "Market";
            MarketCB.TextAlign = ContentAlignment.MiddleRight;
            MarketCB.UseVisualStyleBackColor = true;
            // 
            // LogToADMCB
            // 
            LogToADMCB.AutoSize = true;
            LogToADMCB.Location = new Point(83, 106);
            LogToADMCB.Margin = new Padding(4, 3, 4, 3);
            LogToADMCB.Name = "LogToADMCB";
            LogToADMCB.Size = new Size(91, 19);
            LogToADMCB.TabIndex = 13;
            LogToADMCB.Text = "Log To ADM";
            LogToADMCB.UseVisualStyleBackColor = true;
            // 
            // MissionAirdropCB
            // 
            MissionAirdropCB.AutoSize = true;
            MissionAirdropCB.ForeColor = SystemColors.Control;
            MissionAirdropCB.Location = new Point(79, 129);
            MissionAirdropCB.Margin = new Padding(4, 3, 4, 3);
            MissionAirdropCB.Name = "MissionAirdropCB";
            MissionAirdropCB.Size = new Size(110, 19);
            MissionAirdropCB.TabIndex = 16;
            MissionAirdropCB.Text = "Mission Airdrop";
            MissionAirdropCB.TextAlign = ContentAlignment.MiddleRight;
            MissionAirdropCB.UseVisualStyleBackColor = true;
            // 
            // SpawnSelectionCB
            // 
            SpawnSelectionCB.AutoSize = true;
            SpawnSelectionCB.ForeColor = SystemColors.Control;
            SpawnSelectionCB.Location = new Point(94, 151);
            SpawnSelectionCB.Margin = new Padding(4, 3, 4, 3);
            SpawnSelectionCB.Name = "SpawnSelectionCB";
            SpawnSelectionCB.Size = new Size(112, 19);
            SpawnSelectionCB.TabIndex = 20;
            SpawnSelectionCB.Text = "Spawn Selection";
            SpawnSelectionCB.TextAlign = ContentAlignment.MiddleRight;
            SpawnSelectionCB.UseVisualStyleBackColor = true;
            // 
            // LogToScriptsCB
            // 
            LogToScriptsCB.AutoSize = true;
            LogToScriptsCB.Location = new Point(183, 106);
            LogToScriptsCB.Margin = new Padding(4, 3, 4, 3);
            LogToScriptsCB.Name = "LogToScriptsCB";
            LogToScriptsCB.Size = new Size(99, 19);
            LogToScriptsCB.TabIndex = 14;
            LogToScriptsCB.Text = "Log To Scripts";
            LogToScriptsCB.UseVisualStyleBackColor = true;
            // 
            // AdminToolsCB
            // 
            AdminToolsCB.AutoSize = true;
            AdminToolsCB.ForeColor = SystemColors.Control;
            AdminToolsCB.Location = new Point(8, 16);
            AdminToolsCB.Margin = new Padding(4, 3, 4, 3);
            AdminToolsCB.Name = "AdminToolsCB";
            AdminToolsCB.Size = new Size(89, 19);
            AdminToolsCB.TabIndex = 0;
            AdminToolsCB.Text = "AdminTools";
            AdminToolsCB.TextAlign = ContentAlignment.MiddleRight;
            AdminToolsCB.UseVisualStyleBackColor = true;
            // 
            // ChatCB
            // 
            ChatCB.AutoSize = true;
            ChatCB.ForeColor = SystemColors.Control;
            ChatCB.Location = new Point(8, 62);
            ChatCB.Margin = new Padding(4, 3, 4, 3);
            ChatCB.Name = "ChatCB";
            ChatCB.Size = new Size(51, 19);
            ChatCB.TabIndex = 6;
            ChatCB.Text = "Chat";
            ChatCB.TextAlign = ContentAlignment.MiddleRight;
            ChatCB.UseVisualStyleBackColor = true;
            // 
            // PartyCB
            // 
            PartyCB.AutoSize = true;
            PartyCB.ForeColor = SystemColors.Control;
            PartyCB.Location = new Point(198, 129);
            PartyCB.Margin = new Padding(4, 3, 4, 3);
            PartyCB.Name = "PartyCB";
            PartyCB.Size = new Size(53, 19);
            PartyCB.TabIndex = 17;
            PartyCB.Text = "Party";
            PartyCB.TextAlign = ContentAlignment.MiddleRight;
            PartyCB.UseVisualStyleBackColor = true;
            // 
            // BaseBuildingRaidingCB
            // 
            BaseBuildingRaidingCB.AutoSize = true;
            BaseBuildingRaidingCB.ForeColor = SystemColors.Control;
            BaseBuildingRaidingCB.Location = new Point(162, 39);
            BaseBuildingRaidingCB.Margin = new Padding(4, 3, 4, 3);
            BaseBuildingRaidingCB.Name = "BaseBuildingRaidingCB";
            BaseBuildingRaidingCB.Size = new Size(140, 19);
            BaseBuildingRaidingCB.TabIndex = 5;
            BaseBuildingRaidingCB.Text = "Base Building Raiding";
            BaseBuildingRaidingCB.TextAlign = ContentAlignment.MiddleRight;
            BaseBuildingRaidingCB.UseVisualStyleBackColor = true;
            // 
            // CodeLockRaidingCB
            // 
            CodeLockRaidingCB.AutoSize = true;
            CodeLockRaidingCB.ForeColor = SystemColors.Control;
            CodeLockRaidingCB.Location = new Point(71, 62);
            CodeLockRaidingCB.Margin = new Padding(4, 3, 4, 3);
            CodeLockRaidingCB.Name = "CodeLockRaidingCB";
            CodeLockRaidingCB.Size = new Size(122, 19);
            CodeLockRaidingCB.TabIndex = 7;
            CodeLockRaidingCB.Text = "CodeLock Raiding";
            CodeLockRaidingCB.TextAlign = ContentAlignment.MiddleRight;
            CodeLockRaidingCB.UseVisualStyleBackColor = true;
            // 
            // KillfeedCB
            // 
            KillfeedCB.AutoSize = true;
            KillfeedCB.ForeColor = SystemColors.Control;
            KillfeedCB.Location = new Point(8, 106);
            KillfeedCB.Margin = new Padding(4, 3, 4, 3);
            KillfeedCB.Name = "KillfeedCB";
            KillfeedCB.Size = new Size(65, 19);
            KillfeedCB.TabIndex = 12;
            KillfeedCB.Text = "Killfeed";
            KillfeedCB.TextAlign = ContentAlignment.MiddleRight;
            KillfeedCB.UseVisualStyleBackColor = true;
            // 
            // TerritoryCB
            // 
            TerritoryCB.AutoSize = true;
            TerritoryCB.ForeColor = SystemColors.Control;
            TerritoryCB.Location = new Point(225, 151);
            TerritoryCB.Margin = new Padding(4, 3, 4, 3);
            TerritoryCB.Name = "TerritoryCB";
            TerritoryCB.Size = new Size(69, 19);
            TerritoryCB.TabIndex = 21;
            TerritoryCB.Text = "Territory";
            TerritoryCB.TextAlign = ContentAlignment.MiddleRight;
            TerritoryCB.UseVisualStyleBackColor = true;
            // 
            // SafezoneCB
            // 
            SafezoneCB.AutoSize = true;
            SafezoneCB.ForeColor = SystemColors.Control;
            SafezoneCB.Location = new Point(8, 151);
            SafezoneCB.Margin = new Padding(4, 3, 4, 3);
            SafezoneCB.Name = "SafezoneCB";
            SafezoneCB.Size = new Size(73, 19);
            SafezoneCB.TabIndex = 19;
            SafezoneCB.Text = "Safezone";
            SafezoneCB.TextAlign = ContentAlignment.MiddleRight;
            SafezoneCB.UseVisualStyleBackColor = true;
            // 
            // VehicleCarKeyCB
            // 
            VehicleCarKeyCB.AutoSize = true;
            VehicleCarKeyCB.ForeColor = SystemColors.Control;
            VehicleCarKeyCB.Location = new Point(159, 174);
            VehicleCarKeyCB.Margin = new Padding(4, 3, 4, 3);
            VehicleCarKeyCB.Name = "VehicleCarKeyCB";
            VehicleCarKeyCB.Size = new Size(106, 19);
            VehicleCarKeyCB.TabIndex = 23;
            VehicleCarKeyCB.Text = "Vehicle Car Key";
            VehicleCarKeyCB.TextAlign = ContentAlignment.MiddleRight;
            VehicleCarKeyCB.UseVisualStyleBackColor = true;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(1919, 975);
            Controls.Add(groupBox8);
            Controls.Add(groupBox75);
            ForeColor = SystemColors.Control;
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form3";
            Text = "Form3";
            groupBox75.ResumeLayout(false);
            groupBox75.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ItemRequirementNUD).EndInit();
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox75;
        private System.Windows.Forms.Button darkButton2;
        private System.Windows.Forms.Button darkButton72;
        private System.Windows.Forms.Button darkButton70;
        private System.Windows.Forms.Button darkButton71;
        private System.Windows.Forms.Label darkLabel237;
        private System.Windows.Forms.ListBox ItemRarityLB;
        private System.Windows.Forms.ComboBox ItemRarityCB;
        private System.Windows.Forms.NumericUpDown ItemRequirementNUD;
        private System.Windows.Forms.Label darkLabel1;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.CheckBox QuestsCB;
        private System.Windows.Forms.CheckBox VehicleCoverCB;
        private System.Windows.Forms.CheckBox GarageCB;
        private System.Windows.Forms.CheckBox EntityStorageCB;
        private System.Windows.Forms.CheckBox VehicleLeaveCB;
        private System.Windows.Forms.CheckBox VehicleEnterCB;
        private System.Windows.Forms.CheckBox VehicleEngineCB;
        private System.Windows.Forms.CheckBox VehicleDeletedCB;
        private System.Windows.Forms.CheckBox VehicleAttachmentsCB;
        private System.Windows.Forms.CheckBox VehicleDestroyedCB;
        private System.Windows.Forms.CheckBox HardlineCB;
        private System.Windows.Forms.CheckBox ExplosionDamageSystemCB;
        private System.Windows.Forms.CheckBox AIPatrolCB;
        private System.Windows.Forms.CheckBox AIGeneralCB;
        private System.Windows.Forms.CheckBox VehicleTowingCB;
        private System.Windows.Forms.CheckBox VehicleLockPickingCB;
        private System.Windows.Forms.CheckBox AIObjectPatrolCB;
        private System.Windows.Forms.CheckBox ATMCB;
        private System.Windows.Forms.CheckBox MarketCB;
        private System.Windows.Forms.CheckBox LogToADMCB;
        private System.Windows.Forms.CheckBox MissionAirdropCB;
        private System.Windows.Forms.CheckBox SpawnSelectionCB;
        private System.Windows.Forms.CheckBox LogToScriptsCB;
        private System.Windows.Forms.CheckBox AdminToolsCB;
        private System.Windows.Forms.CheckBox ChatCB;
        private System.Windows.Forms.CheckBox PartyCB;
        private System.Windows.Forms.CheckBox BaseBuildingRaidingCB;
        private System.Windows.Forms.CheckBox CodeLockRaidingCB;
        private System.Windows.Forms.CheckBox KillfeedCB;
        private System.Windows.Forms.CheckBox TerritoryCB;
        private System.Windows.Forms.CheckBox SafezoneCB;
        private System.Windows.Forms.CheckBox VehicleCarKeyCB;
    }
}