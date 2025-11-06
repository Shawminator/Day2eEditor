namespace ExpansionPlugin
{
    partial class ExpansionForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExpansionForm));
            panel1 = new Panel();
            SaveButton = new Button();
            splitContainer1 = new SplitContainer();
            ExpansionTV = new Day2eEditor.MultiSelectTreeView();
            _mapControl = new Day2eEditor.MapViewerControl();
            ExpansionSettingsCM = new ContextMenuStrip(components);
            addNewAirdropContainerToolStripMenuItem = new ToolStripMenuItem();
            removeAirdropContainerToolStripMenuItem = new ToolStripMenuItem();
            addAIAdminToolStripMenuItem = new ToolStripMenuItem();
            addAIPreventClimbToolStripMenuItem = new ToolStripMenuItem();
            addAIPlayerFactionToolStripMenuItem = new ToolStripMenuItem();
            removeAIAdminToolStripMenuItem = new ToolStripMenuItem();
            removeAIPreventClimbToolStripMenuItem = new ToolStripMenuItem();
            removeAIPlayerFactionToolStripMenuItem = new ToolStripMenuItem();
            addNewDeployableOutsideTerritoryToolStripMenuItem = new ToolStripMenuItem();
            removeDeployableOutsideTerritoryToolStripMenuItem = new ToolStripMenuItem();
            addNewDeployableInsideEnemyTerritoryToolStripMenuItem = new ToolStripMenuItem();
            removeDeployableInsideEnemyTerritoryToolStripMenuItem = new ToolStripMenuItem();
            addNewVirtualStorageExcludedContainerToolStripMenuItem = new ToolStripMenuItem();
            removeVirtualStorageExcludedContainerToolStripMenuItem = new ToolStripMenuItem();
            addNewNoBuildZoneToolStripMenuItem = new ToolStripMenuItem();
            RemoveNoBuildZoneToolStripMenuItem = new ToolStripMenuItem();
            addBuildZoneItemToolStripMenuItem = new ToolStripMenuItem();
            removeBuildZoneItemToolStripMenuItem = new ToolStripMenuItem();
            addNewDescriptionCategoryToolStripMenuItem = new ToolStripMenuItem();
            removeDescriptionCategoryToolStripMenuItem = new ToolStripMenuItem();
            addNewLinkToolStripMenuItem = new ToolStripMenuItem();
            removeLinkToolStripMenuItem = new ToolStripMenuItem();
            addNewCraftingCategoryToolStripMenuItem = new ToolStripMenuItem();
            removeCraftingCategoryToolStripMenuItem = new ToolStripMenuItem();
            addNewRuleCategoryToolStripMenuItem = new ToolStripMenuItem();
            removeRuleCategoryToolStripMenuItem = new ToolStripMenuItem();
            addNewRuleParagraphToolStripMenuItem = new ToolStripMenuItem();
            removeRuleParagrapghToolStripMenuItem = new ToolStripMenuItem();
            AddNewAttachmentItemToolStripMenuItem = new ToolStripMenuItem();
            RemoveAttachemtItemToolStripMenuItem = new ToolStripMenuItem();
            addNewItemToolStripMenuItem = new ToolStripMenuItem();
            removItemToolStripMenuItem = new ToolStripMenuItem();
            AddNewCargoItemToolStripMenuItem = new ToolStripMenuItem();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ExpansionSettingsCM.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(60, 63, 65);
            panel1.Controls.Add(SaveButton);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1192, 31);
            panel1.TabIndex = 6;
            // 
            // SaveButton
            // 
            SaveButton.BackgroundImage = (Image)resources.GetObject("SaveButton.BackgroundImage");
            SaveButton.BackgroundImageLayout = ImageLayout.Stretch;
            SaveButton.FlatStyle = FlatStyle.Flat;
            SaveButton.Location = new Point(12, 3);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(40, 25);
            SaveButton.TabIndex = 0;
            SaveButton.TextImageRelation = TextImageRelation.ImageAboveText;
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 31);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(ExpansionTV);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(_mapControl);
            splitContainer1.Size = new Size(1192, 657);
            splitContainer1.SplitterDistance = 394;
            splitContainer1.SplitterWidth = 10;
            splitContainer1.TabIndex = 5;
            // 
            // ExpansionTV
            // 
            ExpansionTV.BackColor = Color.FromArgb(60, 63, 65);
            ExpansionTV.Dock = DockStyle.Fill;
            ExpansionTV.ForeColor = SystemColors.Control;
            ExpansionTV.LineColor = Color.FromArgb(240, 240, 240);
            ExpansionTV.Location = new Point(0, 0);
            ExpansionTV.Name = "ExpansionTV";
            ExpansionTV.Size = new Size(394, 657);
            ExpansionTV.TabIndex = 0;
            ExpansionTV.AfterSelect += ExpansionTV_AfterSelect;
            ExpansionTV.NodeMouseClick += ExpansionTV_NodeMouseClick;
            // 
            // _mapControl
            // 
            _mapControl.Dock = DockStyle.Fill;
            _mapControl.Location = new Point(0, 0);
            _mapControl.Name = "_mapControl";
            _mapControl.Size = new Size(788, 657);
            _mapControl.TabIndex = 1;
            _mapControl.Text = "mapViewerControl1";
            // 
            // ExpansionSettingsCM
            // 
            ExpansionSettingsCM.BackColor = Color.FromArgb(60, 63, 65);
            ExpansionSettingsCM.Items.AddRange(new ToolStripItem[] { addNewAirdropContainerToolStripMenuItem, removeAirdropContainerToolStripMenuItem, addAIAdminToolStripMenuItem, addAIPreventClimbToolStripMenuItem, addAIPlayerFactionToolStripMenuItem, removeAIAdminToolStripMenuItem, removeAIPreventClimbToolStripMenuItem, removeAIPlayerFactionToolStripMenuItem, addNewDeployableOutsideTerritoryToolStripMenuItem, removeDeployableOutsideTerritoryToolStripMenuItem, addNewDeployableInsideEnemyTerritoryToolStripMenuItem, removeDeployableInsideEnemyTerritoryToolStripMenuItem, addNewVirtualStorageExcludedContainerToolStripMenuItem, removeVirtualStorageExcludedContainerToolStripMenuItem, addNewNoBuildZoneToolStripMenuItem, RemoveNoBuildZoneToolStripMenuItem, addBuildZoneItemToolStripMenuItem, removeBuildZoneItemToolStripMenuItem, addNewDescriptionCategoryToolStripMenuItem, removeDescriptionCategoryToolStripMenuItem, addNewLinkToolStripMenuItem, removeLinkToolStripMenuItem, addNewCraftingCategoryToolStripMenuItem, removeCraftingCategoryToolStripMenuItem, addNewRuleCategoryToolStripMenuItem, removeRuleCategoryToolStripMenuItem, addNewRuleParagraphToolStripMenuItem, removeRuleParagrapghToolStripMenuItem, AddNewAttachmentItemToolStripMenuItem, RemoveAttachemtItemToolStripMenuItem, addNewItemToolStripMenuItem, removItemToolStripMenuItem, AddNewCargoItemToolStripMenuItem });
            ExpansionSettingsCM.Name = "TypesCM";
            ExpansionSettingsCM.ShowImageMargin = false;
            ExpansionSettingsCM.Size = new Size(283, 730);
            // 
            // addNewAirdropContainerToolStripMenuItem
            // 
            addNewAirdropContainerToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewAirdropContainerToolStripMenuItem.Name = "addNewAirdropContainerToolStripMenuItem";
            addNewAirdropContainerToolStripMenuItem.Size = new Size(282, 22);
            addNewAirdropContainerToolStripMenuItem.Text = "Add New Airdrop Container";
            addNewAirdropContainerToolStripMenuItem.Click += addNewAirdropContainerToolStripMenuItem_Click;
            // 
            // removeAirdropContainerToolStripMenuItem
            // 
            removeAirdropContainerToolStripMenuItem.ForeColor = SystemColors.Control;
            removeAirdropContainerToolStripMenuItem.Name = "removeAirdropContainerToolStripMenuItem";
            removeAirdropContainerToolStripMenuItem.Size = new Size(282, 22);
            removeAirdropContainerToolStripMenuItem.Text = "Remove Airdrop Container";
            removeAirdropContainerToolStripMenuItem.Click += removeAirdropContainerToolStripMenuItem_Click;
            // 
            // addAIAdminToolStripMenuItem
            // 
            addAIAdminToolStripMenuItem.ForeColor = SystemColors.Control;
            addAIAdminToolStripMenuItem.Name = "addAIAdminToolStripMenuItem";
            addAIAdminToolStripMenuItem.Size = new Size(282, 22);
            addAIAdminToolStripMenuItem.Text = "Add AI Admin";
            addAIAdminToolStripMenuItem.Click += addAIAdminToolStripMenuItem_Click;
            // 
            // addAIPreventClimbToolStripMenuItem
            // 
            addAIPreventClimbToolStripMenuItem.ForeColor = SystemColors.Control;
            addAIPreventClimbToolStripMenuItem.Name = "addAIPreventClimbToolStripMenuItem";
            addAIPreventClimbToolStripMenuItem.Size = new Size(282, 22);
            addAIPreventClimbToolStripMenuItem.Text = "Add AI Prevent Climb";
            addAIPreventClimbToolStripMenuItem.Click += addAIPreventClimbToolStripMenuItem_Click;
            // 
            // addAIPlayerFactionToolStripMenuItem
            // 
            addAIPlayerFactionToolStripMenuItem.ForeColor = SystemColors.Control;
            addAIPlayerFactionToolStripMenuItem.Name = "addAIPlayerFactionToolStripMenuItem";
            addAIPlayerFactionToolStripMenuItem.Size = new Size(282, 22);
            addAIPlayerFactionToolStripMenuItem.Text = "Add AI Player Faction";
            addAIPlayerFactionToolStripMenuItem.Click += addAIPlayerFactionToolStripMenuItem_Click;
            // 
            // removeAIAdminToolStripMenuItem
            // 
            removeAIAdminToolStripMenuItem.ForeColor = SystemColors.Control;
            removeAIAdminToolStripMenuItem.Name = "removeAIAdminToolStripMenuItem";
            removeAIAdminToolStripMenuItem.Size = new Size(282, 22);
            removeAIAdminToolStripMenuItem.Text = "Remove AI Admin";
            removeAIAdminToolStripMenuItem.Click += removeAIAdminToolStripMenuItem_Click;
            // 
            // removeAIPreventClimbToolStripMenuItem
            // 
            removeAIPreventClimbToolStripMenuItem.ForeColor = SystemColors.Control;
            removeAIPreventClimbToolStripMenuItem.Name = "removeAIPreventClimbToolStripMenuItem";
            removeAIPreventClimbToolStripMenuItem.Size = new Size(282, 22);
            removeAIPreventClimbToolStripMenuItem.Text = "Remove AI Prevent Climb";
            removeAIPreventClimbToolStripMenuItem.Click += removeAIPreventClimbToolStripMenuItem_Click;
            // 
            // removeAIPlayerFactionToolStripMenuItem
            // 
            removeAIPlayerFactionToolStripMenuItem.ForeColor = SystemColors.Control;
            removeAIPlayerFactionToolStripMenuItem.Name = "removeAIPlayerFactionToolStripMenuItem";
            removeAIPlayerFactionToolStripMenuItem.Size = new Size(282, 22);
            removeAIPlayerFactionToolStripMenuItem.Text = "Remove AI Player Faction";
            removeAIPlayerFactionToolStripMenuItem.Click += removeAIPlayerFactionToolStripMenuItem_Click;
            // 
            // addNewDeployableOutsideTerritoryToolStripMenuItem
            // 
            addNewDeployableOutsideTerritoryToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewDeployableOutsideTerritoryToolStripMenuItem.Name = "addNewDeployableOutsideTerritoryToolStripMenuItem";
            addNewDeployableOutsideTerritoryToolStripMenuItem.Size = new Size(282, 22);
            addNewDeployableOutsideTerritoryToolStripMenuItem.Text = "Add New Deployable Outside Territory";
            addNewDeployableOutsideTerritoryToolStripMenuItem.Click += addNewDeployableOutsideTerritoryToolStripMenuItem_Click;
            // 
            // removeDeployableOutsideTerritoryToolStripMenuItem
            // 
            removeDeployableOutsideTerritoryToolStripMenuItem.ForeColor = SystemColors.Control;
            removeDeployableOutsideTerritoryToolStripMenuItem.Name = "removeDeployableOutsideTerritoryToolStripMenuItem";
            removeDeployableOutsideTerritoryToolStripMenuItem.Size = new Size(282, 22);
            removeDeployableOutsideTerritoryToolStripMenuItem.Text = "Remove Deployable Outside Territory";
            removeDeployableOutsideTerritoryToolStripMenuItem.Click += removeDeployableOutsideTerritoryToolStripMenuItem_Click;
            // 
            // addNewDeployableInsideEnemyTerritoryToolStripMenuItem
            // 
            addNewDeployableInsideEnemyTerritoryToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewDeployableInsideEnemyTerritoryToolStripMenuItem.Name = "addNewDeployableInsideEnemyTerritoryToolStripMenuItem";
            addNewDeployableInsideEnemyTerritoryToolStripMenuItem.Size = new Size(282, 22);
            addNewDeployableInsideEnemyTerritoryToolStripMenuItem.Text = "Add New Deployable Inside Enemy Territory";
            addNewDeployableInsideEnemyTerritoryToolStripMenuItem.Click += addNewDeployableInsideEnemyTerritoryToolStripMenuItem_Click;
            // 
            // removeDeployableInsideEnemyTerritoryToolStripMenuItem
            // 
            removeDeployableInsideEnemyTerritoryToolStripMenuItem.ForeColor = SystemColors.Control;
            removeDeployableInsideEnemyTerritoryToolStripMenuItem.Name = "removeDeployableInsideEnemyTerritoryToolStripMenuItem";
            removeDeployableInsideEnemyTerritoryToolStripMenuItem.Size = new Size(282, 22);
            removeDeployableInsideEnemyTerritoryToolStripMenuItem.Text = "Remove Deployable Inside Enemy Territory";
            removeDeployableInsideEnemyTerritoryToolStripMenuItem.Click += removeDeployableInsideEnemyTerritoryToolStripMenuItem_Click;
            // 
            // addNewVirtualStorageExcludedContainerToolStripMenuItem
            // 
            addNewVirtualStorageExcludedContainerToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewVirtualStorageExcludedContainerToolStripMenuItem.Name = "addNewVirtualStorageExcludedContainerToolStripMenuItem";
            addNewVirtualStorageExcludedContainerToolStripMenuItem.Size = new Size(282, 22);
            addNewVirtualStorageExcludedContainerToolStripMenuItem.Text = "Add new Virtual Storage Excluded Container";
            addNewVirtualStorageExcludedContainerToolStripMenuItem.Click += addNewVirtualStorageExcludedContainerToolStripMenuItem_Click;
            // 
            // removeVirtualStorageExcludedContainerToolStripMenuItem
            // 
            removeVirtualStorageExcludedContainerToolStripMenuItem.ForeColor = SystemColors.Control;
            removeVirtualStorageExcludedContainerToolStripMenuItem.Name = "removeVirtualStorageExcludedContainerToolStripMenuItem";
            removeVirtualStorageExcludedContainerToolStripMenuItem.Size = new Size(282, 22);
            removeVirtualStorageExcludedContainerToolStripMenuItem.Text = "Remove Virtual Storage Excluded Container";
            removeVirtualStorageExcludedContainerToolStripMenuItem.Click += removeVirtualStorageExcludedContainerToolStripMenuItem_Click;
            // 
            // addNewNoBuildZoneToolStripMenuItem
            // 
            addNewNoBuildZoneToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewNoBuildZoneToolStripMenuItem.Name = "addNewNoBuildZoneToolStripMenuItem";
            addNewNoBuildZoneToolStripMenuItem.Size = new Size(282, 22);
            addNewNoBuildZoneToolStripMenuItem.Text = "Add New Build Zone";
            addNewNoBuildZoneToolStripMenuItem.Click += addNewNoBuildZoneToolStripMenuItem_Click;
            // 
            // RemoveNoBuildZoneToolStripMenuItem
            // 
            RemoveNoBuildZoneToolStripMenuItem.ForeColor = SystemColors.Control;
            RemoveNoBuildZoneToolStripMenuItem.Name = "RemoveNoBuildZoneToolStripMenuItem";
            RemoveNoBuildZoneToolStripMenuItem.Size = new Size(282, 22);
            RemoveNoBuildZoneToolStripMenuItem.Text = "Remove Build Zone";
            RemoveNoBuildZoneToolStripMenuItem.Click += RemoveNoBuildZoneToolStripMenuItem_Click;
            // 
            // addBuildZoneItemToolStripMenuItem
            // 
            addBuildZoneItemToolStripMenuItem.ForeColor = SystemColors.Control;
            addBuildZoneItemToolStripMenuItem.Name = "addBuildZoneItemToolStripMenuItem";
            addBuildZoneItemToolStripMenuItem.Size = new Size(282, 22);
            addBuildZoneItemToolStripMenuItem.Text = "Add Build Zone Item";
            addBuildZoneItemToolStripMenuItem.Click += addBuildZoneItemToolStripMenuItem_Click;
            // 
            // removeBuildZoneItemToolStripMenuItem
            // 
            removeBuildZoneItemToolStripMenuItem.ForeColor = SystemColors.Control;
            removeBuildZoneItemToolStripMenuItem.Name = "removeBuildZoneItemToolStripMenuItem";
            removeBuildZoneItemToolStripMenuItem.Size = new Size(282, 22);
            removeBuildZoneItemToolStripMenuItem.Text = "Remove Build Zone Item";
            removeBuildZoneItemToolStripMenuItem.Click += removeBuildZoneItemToolStripMenuItem_Click;
            // 
            // addNewDescriptionCategoryToolStripMenuItem
            // 
            addNewDescriptionCategoryToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewDescriptionCategoryToolStripMenuItem.Name = "addNewDescriptionCategoryToolStripMenuItem";
            addNewDescriptionCategoryToolStripMenuItem.Size = new Size(282, 22);
            addNewDescriptionCategoryToolStripMenuItem.Text = "Add New Description Category";
            addNewDescriptionCategoryToolStripMenuItem.Click += addNewDescriptionCategoryToolStripMenuItem_Click;
            // 
            // removeDescriptionCategoryToolStripMenuItem
            // 
            removeDescriptionCategoryToolStripMenuItem.ForeColor = SystemColors.Control;
            removeDescriptionCategoryToolStripMenuItem.Name = "removeDescriptionCategoryToolStripMenuItem";
            removeDescriptionCategoryToolStripMenuItem.Size = new Size(282, 22);
            removeDescriptionCategoryToolStripMenuItem.Text = "Remove DescriptionCategory";
            removeDescriptionCategoryToolStripMenuItem.Click += removeDescriptionCategoryToolStripMenuItem_Click;
            // 
            // addNewLinkToolStripMenuItem
            // 
            addNewLinkToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewLinkToolStripMenuItem.Name = "addNewLinkToolStripMenuItem";
            addNewLinkToolStripMenuItem.Size = new Size(282, 22);
            addNewLinkToolStripMenuItem.Text = "Add New Link";
            addNewLinkToolStripMenuItem.Click += addNewLinkToolStripMenuItem_Click;
            // 
            // removeLinkToolStripMenuItem
            // 
            removeLinkToolStripMenuItem.ForeColor = SystemColors.Control;
            removeLinkToolStripMenuItem.Name = "removeLinkToolStripMenuItem";
            removeLinkToolStripMenuItem.Size = new Size(282, 22);
            removeLinkToolStripMenuItem.Text = "Remove Link";
            removeLinkToolStripMenuItem.Click += removeLinkToolStripMenuItem_Click;
            // 
            // addNewCraftingCategoryToolStripMenuItem
            // 
            addNewCraftingCategoryToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewCraftingCategoryToolStripMenuItem.Name = "addNewCraftingCategoryToolStripMenuItem";
            addNewCraftingCategoryToolStripMenuItem.Size = new Size(282, 22);
            addNewCraftingCategoryToolStripMenuItem.Text = "Add New Crafting Category";
            addNewCraftingCategoryToolStripMenuItem.Click += addNewCraftingCategoryToolStripMenuItem_Click;
            // 
            // removeCraftingCategoryToolStripMenuItem
            // 
            removeCraftingCategoryToolStripMenuItem.ForeColor = SystemColors.Control;
            removeCraftingCategoryToolStripMenuItem.Name = "removeCraftingCategoryToolStripMenuItem";
            removeCraftingCategoryToolStripMenuItem.Size = new Size(282, 22);
            removeCraftingCategoryToolStripMenuItem.Text = "Remove Crafting Category";
            removeCraftingCategoryToolStripMenuItem.Click += removeCraftingCategoryToolStripMenuItem_Click;
            // 
            // addNewRuleCategoryToolStripMenuItem
            // 
            addNewRuleCategoryToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewRuleCategoryToolStripMenuItem.Name = "addNewRuleCategoryToolStripMenuItem";
            addNewRuleCategoryToolStripMenuItem.Size = new Size(282, 22);
            addNewRuleCategoryToolStripMenuItem.Text = "Add New Rule Category";
            addNewRuleCategoryToolStripMenuItem.Click += addNewRuleCategoryToolStripMenuItem_Click;
            // 
            // removeRuleCategoryToolStripMenuItem
            // 
            removeRuleCategoryToolStripMenuItem.ForeColor = SystemColors.Control;
            removeRuleCategoryToolStripMenuItem.Name = "removeRuleCategoryToolStripMenuItem";
            removeRuleCategoryToolStripMenuItem.Size = new Size(282, 22);
            removeRuleCategoryToolStripMenuItem.Text = "Remove Rule Category";
            removeRuleCategoryToolStripMenuItem.Click += removeRuleCategoryToolStripMenuItem_Click;
            // 
            // addNewRuleParagraphToolStripMenuItem
            // 
            addNewRuleParagraphToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewRuleParagraphToolStripMenuItem.Name = "addNewRuleParagraphToolStripMenuItem";
            addNewRuleParagraphToolStripMenuItem.Size = new Size(282, 22);
            addNewRuleParagraphToolStripMenuItem.Text = "Add New Rule Paragraph";
            addNewRuleParagraphToolStripMenuItem.Click += addNewRuleParagraphToolStripMenuItem_Click;
            // 
            // removeRuleParagrapghToolStripMenuItem
            // 
            removeRuleParagrapghToolStripMenuItem.ForeColor = SystemColors.Control;
            removeRuleParagrapghToolStripMenuItem.Name = "removeRuleParagrapghToolStripMenuItem";
            removeRuleParagrapghToolStripMenuItem.Size = new Size(282, 22);
            removeRuleParagrapghToolStripMenuItem.Text = "Remove Rule Paragrapgh";
            removeRuleParagrapghToolStripMenuItem.Click += removeRuleParagrapghToolStripMenuItem_Click;
            // 
            // AddNewAttachmentItemToolStripMenuItem
            // 
            AddNewAttachmentItemToolStripMenuItem.ForeColor = SystemColors.Control;
            AddNewAttachmentItemToolStripMenuItem.Name = "AddNewAttachmentItemToolStripMenuItem";
            AddNewAttachmentItemToolStripMenuItem.Size = new Size(282, 22);
            AddNewAttachmentItemToolStripMenuItem.Text = "Add New AttachmentItem";
            AddNewAttachmentItemToolStripMenuItem.Click += AddNewAttachmentItemToolStripMenuItem_Click;
            // 
            // RemoveAttachemtItemToolStripMenuItem
            // 
            RemoveAttachemtItemToolStripMenuItem.ForeColor = SystemColors.Control;
            RemoveAttachemtItemToolStripMenuItem.Name = "RemoveAttachemtItemToolStripMenuItem";
            RemoveAttachemtItemToolStripMenuItem.Size = new Size(282, 22);
            RemoveAttachemtItemToolStripMenuItem.Text = "Remove Attachemt Item";
            RemoveAttachemtItemToolStripMenuItem.Click += RemoveAttachemtItemToolStripMenuItem_Click;
            // 
            // addNewItemToolStripMenuItem
            // 
            addNewItemToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewItemToolStripMenuItem.Name = "addNewItemToolStripMenuItem";
            addNewItemToolStripMenuItem.Size = new Size(282, 22);
            addNewItemToolStripMenuItem.Text = "Add New Item";
            addNewItemToolStripMenuItem.Click += addNewItemToolStripMenuItem_Click;
            // 
            // removItemToolStripMenuItem
            // 
            removItemToolStripMenuItem.ForeColor = SystemColors.Control;
            removItemToolStripMenuItem.Name = "removItemToolStripMenuItem";
            removItemToolStripMenuItem.Size = new Size(282, 22);
            removItemToolStripMenuItem.Text = "RemovItem";
            removItemToolStripMenuItem.Click += removeItemToolStripMenuItem_Click;
            // 
            // AddNewCargoItemToolStripMenuItem
            // 
            AddNewCargoItemToolStripMenuItem.ForeColor = SystemColors.Control;
            AddNewCargoItemToolStripMenuItem.Name = "AddNewCargoItemToolStripMenuItem";
            AddNewCargoItemToolStripMenuItem.Size = new Size(282, 22);
            AddNewCargoItemToolStripMenuItem.Text = "Add New Cargo Item";
            AddNewCargoItemToolStripMenuItem.Click += this.AddNewCargoItemToolStripMenuItem_Click;
            // 
            // ExpansionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(1192, 688);
            Controls.Add(splitContainer1);
            Controls.Add(panel1);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            Name = "ExpansionForm";
            Text = "Form1";
            FormClosing += ExpansionForm_FormClosing;
            FormClosed += ExpansionForm_FormClosed;
            Load += ExpansionForm_Load;
            panel1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ExpansionSettingsCM.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private CheckBox TerritorieszonesCB;
        private Button SaveButton;
        private SplitContainer splitContainer1;
        private Day2eEditor.MultiSelectTreeView ExpansionTV;
        private Day2eEditor.MapViewerControl _mapControl;
        private ContextMenuStrip ExpansionSettingsCM;
        private ToolStripMenuItem addNewAirdropContainerToolStripMenuItem;
        private ToolStripMenuItem removeAirdropContainerToolStripMenuItem;
        private ToolStripMenuItem addAIAdminToolStripMenuItem;
        private ToolStripMenuItem addAIPreventClimbToolStripMenuItem;
        private ToolStripMenuItem addAIPlayerFactionToolStripMenuItem;
        private ToolStripMenuItem removeAIAdminToolStripMenuItem;
        private ToolStripMenuItem removeAIPreventClimbToolStripMenuItem;
        private ToolStripMenuItem removeAIPlayerFactionToolStripMenuItem;
        private ToolStripMenuItem addNewDeployableOutsideTerritoryToolStripMenuItem;
        private ToolStripMenuItem removeDeployableOutsideTerritoryToolStripMenuItem;
        private ToolStripMenuItem addNewDeployableInsideEnemyTerritoryToolStripMenuItem;
        private ToolStripMenuItem removeDeployableInsideEnemyTerritoryToolStripMenuItem;
        private ToolStripMenuItem addNewNoBuildZoneToolStripMenuItem;
        private ToolStripMenuItem RemoveNoBuildZoneToolStripMenuItem;
        private ToolStripMenuItem addNewVirtualStorageExcludedContainerToolStripMenuItem;
        private ToolStripMenuItem removeVirtualStorageExcludedContainerToolStripMenuItem;
        private ToolStripMenuItem addBuildZoneItemToolStripMenuItem;
        private ToolStripMenuItem removeBuildZoneItemToolStripMenuItem;
        private ToolStripMenuItem addNewDescriptionCategoryToolStripMenuItem;
        private ToolStripMenuItem removeDescriptionCategoryToolStripMenuItem;
        private ToolStripMenuItem addNewLinkToolStripMenuItem;
        private ToolStripMenuItem removeLinkToolStripMenuItem;
        private ToolStripMenuItem addNewCraftingCategoryToolStripMenuItem;
        private ToolStripMenuItem removeCraftingCategoryToolStripMenuItem;
        private ToolStripMenuItem addNewRuleCategoryToolStripMenuItem;
        private ToolStripMenuItem removeRuleCategoryToolStripMenuItem;
        private ToolStripMenuItem addNewRuleParagraphToolStripMenuItem;
        private ToolStripMenuItem removeRuleParagrapghToolStripMenuItem;
        private ToolStripMenuItem AddNewAttachmentItemToolStripMenuItem;
        private ToolStripMenuItem RemoveAttachemtItemToolStripMenuItem;
        private ToolStripMenuItem addNewItemToolStripMenuItem;
        private ToolStripMenuItem removItemToolStripMenuItem;
        private ToolStripMenuItem AddNewCargoItemToolStripMenuItem;
    }
}
