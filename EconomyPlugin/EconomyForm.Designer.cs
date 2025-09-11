using Day2eEditor;

namespace EconomyPlugin
{
    partial class EconomyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EconomyForm));
            _mapControl = new MapViewerControl();
            splitContainer1 = new SplitContainer();
            EconomyTV = new MultiSelectTreeView();
            EditPropertyCMS = new ContextMenuStrip(components);
            editPropertyToolStripMenuItem = new ToolStripMenuItem();
            setToDefaultToolStripMenuItem = new ToolStripMenuItem();
            panel1 = new Panel();
            TerritorieszonesCB = new CheckBox();
            SaveButton = new Button();
            EventSpawnContextMenu = new ContextMenuStrip(components);
            addNewEventSpawnToolStripMenuItem = new ToolStripMenuItem();
            deleteSelectedEventSpawnToolStripMenuItem = new ToolStripMenuItem();
            importPositionFromdzeToolStripMenuItem = new ToolStripMenuItem();
            exportPositionTodzeToolStripMenuItem = new ToolStripMenuItem();
            addNewPosirtionToolStripMenuItem = new ToolStripMenuItem();
            removeSelectedPositionToolStripMenuItem = new ToolStripMenuItem();
            removeAllPositionToolStripMenuItem = new ToolStripMenuItem();
            exportGroupSpawnTodzeToolStripMenuItem = new ToolStripMenuItem();
            importPositionAndCreateEventgroupFormdzeToolStripMenuItem = new ToolStripMenuItem();
            TypesCM = new ContextMenuStrip(components);
            addNewTypesToolStripMenuItem = new ToolStripMenuItem();
            removeSelectedToolStripMenuItem = new ToolStripMenuItem();
            EventsCM = new ContextMenuStrip(components);
            AddNewEventsToolstripMenuItem = new ToolStripMenuItem();
            RemoveEventsToolStripMenuItem = new ToolStripMenuItem();
            RandomPresetsCM = new ContextMenuStrip(components);
            addNewRandomPresetFileToolStripMenuItem = new ToolStripMenuItem();
            addNewAttchementToolStripMenuItem = new ToolStripMenuItem();
            addNewCargoToolStripMenuItem = new ToolStripMenuItem();
            addNewItemToolStripMenuItem = new ToolStripMenuItem();
            removeSelectedRandomPresetToolStripmenuItem = new ToolStripMenuItem();
            SpawnableTypesCM = new ContextMenuStrip(components);
            addNewSpawnableTypesFileToolStripMenuItem = new ToolStripMenuItem();
            addNewSpawnableTypeToolStripMenuItem = new ToolStripMenuItem();
            addNewHoarderToolStripMenuItem = new ToolStripMenuItem();
            addNewTagToolStripMenuItem = new ToolStripMenuItem();
            addNewDamageToolStripMenuItem = new ToolStripMenuItem();
            addNewItemToolStripMenuItem1 = new ToolStripMenuItem();
            addNewCargoToolStripMenuItem1 = new ToolStripMenuItem();
            addNewAttachmentToolStripMenuItem = new ToolStripMenuItem();
            removeSelectedToolStripMenuItem1 = new ToolStripMenuItem();
            SpawnGearPresetCM = new ContextMenuStrip(components);
            addNewSpawnGEarPresetFileToolStripMenuItem = new ToolStripMenuItem();
            addNewAttachmentSlotItemSetToolStripMenuItem = new ToolStripMenuItem();
            addNewDisctreetItemSetToolStripMenuItem = new ToolStripMenuItem();
            addNewComplexChildSetToolStripMenuItem = new ToolStripMenuItem();
            addNewDiscreetUnsortedItemSetToolStripMenuItem = new ToolStripMenuItem();
            SpawnGearremoveSelectedToolStripMenuItem2 = new ToolStripMenuItem();
            PlayerRestrictedAreaCM = new ContextMenuStrip(components);
            addNewPRAFileToolStripMenuItem = new ToolStripMenuItem();
            addNewPRABoxToolStripMenuItem = new ToolStripMenuItem();
            addNewPRASafePositionToolStripMenuItem = new ToolStripMenuItem();
            removePRASelectedToolStripMenuItem = new ToolStripMenuItem();
            ObjectSpawnerArrCM = new ContextMenuStrip(components);
            addNewObjectSpawnerArrFileToolStripMenuItem = new ToolStripMenuItem();
            removeSelectedObjectSpawnerArrToolStripMenuItem = new ToolStripMenuItem();
            cfgEffectsAreaCM = new ContextMenuStrip(components);
            addNewEffectAreaToolStripMenuItem = new ToolStripMenuItem();
            usePlayerDataToolStripMenuItem = new ToolStripMenuItem();
            removePlayerDataToolStripMenuItem = new ToolStripMenuItem();
            removeEffectAreaToolStripMenuItem = new ToolStripMenuItem();
            addNewSafePositionToolStripMenuItem = new ToolStripMenuItem();
            removeSafePositionToolStripMenuItem = new ToolStripMenuItem();
            PlayerSpawnsCM = new ContextMenuStrip(components);
            addNewSpawnPositionToolStripMenuItem = new ToolStripMenuItem();
            removeSpawnPositionToolStripMenuItem = new ToolStripMenuItem();
            addNewSpawnGroupToolStripMenuItem = new ToolStripMenuItem();
            removeSpawnGroupToolStripMenuItem = new ToolStripMenuItem();
            IgnoreListCM = new ContextMenuStrip(components);
            removeClassnameToolStripMenuItem = new ToolStripMenuItem();
            addClassnameToolStripMenuItem = new ToolStripMenuItem();
            MapGroupPosCM = new ContextMenuStrip(components);
            removeSelectedPositionsToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            EditPropertyCMS.SuspendLayout();
            panel1.SuspendLayout();
            EventSpawnContextMenu.SuspendLayout();
            TypesCM.SuspendLayout();
            EventsCM.SuspendLayout();
            RandomPresetsCM.SuspendLayout();
            SpawnableTypesCM.SuspendLayout();
            SpawnGearPresetCM.SuspendLayout();
            PlayerRestrictedAreaCM.SuspendLayout();
            ObjectSpawnerArrCM.SuspendLayout();
            cfgEffectsAreaCM.SuspendLayout();
            PlayerSpawnsCM.SuspendLayout();
            IgnoreListCM.SuspendLayout();
            MapGroupPosCM.SuspendLayout();
            SuspendLayout();
            // 
            // _mapControl
            // 
            _mapControl.Dock = DockStyle.Fill;
            _mapControl.Location = new Point(0, 0);
            _mapControl.Name = "_mapControl";
            _mapControl.Size = new Size(756, 593);
            _mapControl.TabIndex = 1;
            _mapControl.Text = "mapViewerControl1";
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(12, 37);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(EconomyTV);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(_mapControl);
            splitContainer1.Size = new Size(1146, 593);
            splitContainer1.SplitterDistance = 380;
            splitContainer1.SplitterWidth = 10;
            splitContainer1.TabIndex = 3;
            splitContainer1.SplitterMoved += splitContainer1_SplitterMoved;
            // 
            // EconomyTV
            // 
            EconomyTV.BackColor = Color.FromArgb(60, 63, 65);
            EconomyTV.Dock = DockStyle.Fill;
            EconomyTV.ForeColor = SystemColors.Control;
            EconomyTV.LineColor = Color.FromArgb(240, 240, 240);
            EconomyTV.Location = new Point(0, 0);
            EconomyTV.Name = "EconomyTV";
            EconomyTV.Size = new Size(380, 593);
            EconomyTV.TabIndex = 0;
            EconomyTV.AfterSelect += EconomyTV_AfterSelect;
            EconomyTV.NodeMouseClick += EconomyTV_NodeMouseClick;
            // 
            // EditPropertyCMS
            // 
            EditPropertyCMS.BackColor = Color.FromArgb(60, 63, 65);
            EditPropertyCMS.Items.AddRange(new ToolStripItem[] { editPropertyToolStripMenuItem, setToDefaultToolStripMenuItem });
            EditPropertyCMS.Name = "contextMenuStrip1";
            EditPropertyCMS.ShowImageMargin = false;
            EditPropertyCMS.Size = new Size(122, 48);
            // 
            // editPropertyToolStripMenuItem
            // 
            editPropertyToolStripMenuItem.ForeColor = SystemColors.Control;
            editPropertyToolStripMenuItem.Name = "editPropertyToolStripMenuItem";
            editPropertyToolStripMenuItem.Size = new Size(121, 22);
            editPropertyToolStripMenuItem.Text = "Edit Property";
            editPropertyToolStripMenuItem.Click += editPropertyToolStripMenuItem_Click;
            // 
            // setToDefaultToolStripMenuItem
            // 
            setToDefaultToolStripMenuItem.ForeColor = SystemColors.Control;
            setToDefaultToolStripMenuItem.Name = "setToDefaultToolStripMenuItem";
            setToDefaultToolStripMenuItem.Size = new Size(121, 22);
            setToDefaultToolStripMenuItem.Text = "Set To Default";
            setToDefaultToolStripMenuItem.Click += setToDefaultToolStripMenuItem_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(60, 63, 65);
            panel1.Controls.Add(TerritorieszonesCB);
            panel1.Controls.Add(SaveButton);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1170, 31);
            panel1.TabIndex = 4;
            // 
            // TerritorieszonesCB
            // 
            TerritorieszonesCB.AutoSize = true;
            TerritorieszonesCB.Location = new Point(402, 9);
            TerritorieszonesCB.Name = "TerritorieszonesCB";
            TerritorieszonesCB.Size = new Size(153, 19);
            TerritorieszonesCB.TabIndex = 2;
            TerritorieszonesCB.Text = "Show All Territory Zones";
            TerritorieszonesCB.UseVisualStyleBackColor = true;
            TerritorieszonesCB.Visible = false;
            TerritorieszonesCB.CheckedChanged += TerritorieszonesCB_CheckedChanged;
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
            // EventSpawnContextMenu
            // 
            EventSpawnContextMenu.Items.AddRange(new ToolStripItem[] { addNewEventSpawnToolStripMenuItem, deleteSelectedEventSpawnToolStripMenuItem, importPositionFromdzeToolStripMenuItem, exportPositionTodzeToolStripMenuItem, addNewPosirtionToolStripMenuItem, removeSelectedPositionToolStripMenuItem, removeAllPositionToolStripMenuItem, exportGroupSpawnTodzeToolStripMenuItem, importPositionAndCreateEventgroupFormdzeToolStripMenuItem });
            EventSpawnContextMenu.Name = "EventSpawnContextMenu";
            EventSpawnContextMenu.ShowImageMargin = false;
            EventSpawnContextMenu.Size = new Size(254, 202);
            // 
            // addNewEventSpawnToolStripMenuItem
            // 
            addNewEventSpawnToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            addNewEventSpawnToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            addNewEventSpawnToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewEventSpawnToolStripMenuItem.Name = "addNewEventSpawnToolStripMenuItem";
            addNewEventSpawnToolStripMenuItem.Size = new Size(253, 22);
            addNewEventSpawnToolStripMenuItem.Text = "Add new event spawn";
            // 
            // deleteSelectedEventSpawnToolStripMenuItem
            // 
            deleteSelectedEventSpawnToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            deleteSelectedEventSpawnToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            deleteSelectedEventSpawnToolStripMenuItem.ForeColor = SystemColors.Control;
            deleteSelectedEventSpawnToolStripMenuItem.Name = "deleteSelectedEventSpawnToolStripMenuItem";
            deleteSelectedEventSpawnToolStripMenuItem.Size = new Size(253, 22);
            deleteSelectedEventSpawnToolStripMenuItem.Text = "Delete selected event spawn";
            // 
            // importPositionFromdzeToolStripMenuItem
            // 
            importPositionFromdzeToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            importPositionFromdzeToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            importPositionFromdzeToolStripMenuItem.ForeColor = SystemColors.Control;
            importPositionFromdzeToolStripMenuItem.Name = "importPositionFromdzeToolStripMenuItem";
            importPositionFromdzeToolStripMenuItem.Size = new Size(253, 22);
            importPositionFromdzeToolStripMenuItem.Text = "Import position";
            importPositionFromdzeToolStripMenuItem.Click += importPositionFromdzeToolStripMenuItem_Click;
            // 
            // exportPositionTodzeToolStripMenuItem
            // 
            exportPositionTodzeToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            exportPositionTodzeToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            exportPositionTodzeToolStripMenuItem.ForeColor = SystemColors.Control;
            exportPositionTodzeToolStripMenuItem.Name = "exportPositionTodzeToolStripMenuItem";
            exportPositionTodzeToolStripMenuItem.Size = new Size(253, 22);
            exportPositionTodzeToolStripMenuItem.Text = "Export Position";
            // 
            // addNewPosirtionToolStripMenuItem
            // 
            addNewPosirtionToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            addNewPosirtionToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            addNewPosirtionToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewPosirtionToolStripMenuItem.Name = "addNewPosirtionToolStripMenuItem";
            addNewPosirtionToolStripMenuItem.Size = new Size(253, 22);
            addNewPosirtionToolStripMenuItem.Text = "Add new position";
            // 
            // removeSelectedPositionToolStripMenuItem
            // 
            removeSelectedPositionToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            removeSelectedPositionToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            removeSelectedPositionToolStripMenuItem.ForeColor = SystemColors.Control;
            removeSelectedPositionToolStripMenuItem.Name = "removeSelectedPositionToolStripMenuItem";
            removeSelectedPositionToolStripMenuItem.Size = new Size(253, 22);
            removeSelectedPositionToolStripMenuItem.Text = "Remove selected position";
            // 
            // removeAllPositionToolStripMenuItem
            // 
            removeAllPositionToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            removeAllPositionToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            removeAllPositionToolStripMenuItem.ForeColor = SystemColors.Control;
            removeAllPositionToolStripMenuItem.Name = "removeAllPositionToolStripMenuItem";
            removeAllPositionToolStripMenuItem.Size = new Size(253, 22);
            removeAllPositionToolStripMenuItem.Text = "Remove all position";
            // 
            // exportGroupSpawnTodzeToolStripMenuItem
            // 
            exportGroupSpawnTodzeToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            exportGroupSpawnTodzeToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            exportGroupSpawnTodzeToolStripMenuItem.ForeColor = SystemColors.Control;
            exportGroupSpawnTodzeToolStripMenuItem.Name = "exportGroupSpawnTodzeToolStripMenuItem";
            exportGroupSpawnTodzeToolStripMenuItem.Size = new Size(253, 22);
            exportGroupSpawnTodzeToolStripMenuItem.Text = "Export Group Spawn";
            // 
            // importPositionAndCreateEventgroupFormdzeToolStripMenuItem
            // 
            importPositionAndCreateEventgroupFormdzeToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            importPositionAndCreateEventgroupFormdzeToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            importPositionAndCreateEventgroupFormdzeToolStripMenuItem.ForeColor = SystemColors.Control;
            importPositionAndCreateEventgroupFormdzeToolStripMenuItem.Name = "importPositionAndCreateEventgroupFormdzeToolStripMenuItem";
            importPositionAndCreateEventgroupFormdzeToolStripMenuItem.Size = new Size(253, 22);
            importPositionAndCreateEventgroupFormdzeToolStripMenuItem.Text = "Import Position and create Eventgroup";
            // 
            // TypesCM
            // 
            TypesCM.BackColor = Color.FromArgb(60, 63, 65);
            TypesCM.Items.AddRange(new ToolStripItem[] { addNewTypesToolStripMenuItem, removeSelectedToolStripMenuItem });
            TypesCM.Name = "TypesCM";
            TypesCM.ShowImageMargin = false;
            TypesCM.Size = new Size(140, 48);
            // 
            // addNewTypesToolStripMenuItem
            // 
            addNewTypesToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewTypesToolStripMenuItem.Name = "addNewTypesToolStripMenuItem";
            addNewTypesToolStripMenuItem.Size = new Size(139, 22);
            addNewTypesToolStripMenuItem.Text = "Add New Types";
            addNewTypesToolStripMenuItem.Click += addNewTypesToolStripMenuItem_Click;
            // 
            // removeSelectedToolStripMenuItem
            // 
            removeSelectedToolStripMenuItem.ForeColor = SystemColors.Control;
            removeSelectedToolStripMenuItem.Name = "removeSelectedToolStripMenuItem";
            removeSelectedToolStripMenuItem.Size = new Size(139, 22);
            removeSelectedToolStripMenuItem.Text = "Remove Selected";
            removeSelectedToolStripMenuItem.Click += removeSelectedToolStripMenuItem_Click;
            // 
            // EventsCM
            // 
            EventsCM.BackColor = Color.FromArgb(60, 63, 65);
            EventsCM.Items.AddRange(new ToolStripItem[] { AddNewEventsToolstripMenuItem, RemoveEventsToolStripMenuItem });
            EventsCM.Name = "TypesCM";
            EventsCM.ShowImageMargin = false;
            EventsCM.Size = new Size(140, 48);
            // 
            // AddNewEventsToolstripMenuItem
            // 
            AddNewEventsToolstripMenuItem.ForeColor = SystemColors.Control;
            AddNewEventsToolstripMenuItem.Name = "AddNewEventsToolstripMenuItem";
            AddNewEventsToolstripMenuItem.Size = new Size(139, 22);
            AddNewEventsToolstripMenuItem.Text = "Add New Events";
            AddNewEventsToolstripMenuItem.Click += AddNewEventsToolstripMenuItem_Click;
            // 
            // RemoveEventsToolStripMenuItem
            // 
            RemoveEventsToolStripMenuItem.ForeColor = SystemColors.Control;
            RemoveEventsToolStripMenuItem.Name = "RemoveEventsToolStripMenuItem";
            RemoveEventsToolStripMenuItem.Size = new Size(139, 22);
            RemoveEventsToolStripMenuItem.Text = "Remove Selected";
            RemoveEventsToolStripMenuItem.Click += RemoveEventsToolStripMenuItem_Click;
            // 
            // RandomPresetsCM
            // 
            RandomPresetsCM.BackColor = Color.FromArgb(60, 63, 65);
            RandomPresetsCM.Items.AddRange(new ToolStripItem[] { addNewRandomPresetFileToolStripMenuItem, addNewAttchementToolStripMenuItem, addNewCargoToolStripMenuItem, addNewItemToolStripMenuItem, removeSelectedRandomPresetToolStripmenuItem });
            RandomPresetsCM.Name = "TypesCM";
            RandomPresetsCM.ShowImageMargin = false;
            RandomPresetsCM.Size = new Size(203, 114);
            // 
            // addNewRandomPresetFileToolStripMenuItem
            // 
            addNewRandomPresetFileToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewRandomPresetFileToolStripMenuItem.Name = "addNewRandomPresetFileToolStripMenuItem";
            addNewRandomPresetFileToolStripMenuItem.Size = new Size(202, 22);
            addNewRandomPresetFileToolStripMenuItem.Text = "Add New Random Preset File";
            addNewRandomPresetFileToolStripMenuItem.Click += addNewRandomPresetFileToolStripMenuItem_Click;
            // 
            // addNewAttchementToolStripMenuItem
            // 
            addNewAttchementToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewAttchementToolStripMenuItem.Name = "addNewAttchementToolStripMenuItem";
            addNewAttchementToolStripMenuItem.Size = new Size(202, 22);
            addNewAttchementToolStripMenuItem.Text = "Add New Attachement";
            addNewAttchementToolStripMenuItem.Click += addNewAttchementToolStripMenuItem_Click;
            // 
            // addNewCargoToolStripMenuItem
            // 
            addNewCargoToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewCargoToolStripMenuItem.Name = "addNewCargoToolStripMenuItem";
            addNewCargoToolStripMenuItem.Size = new Size(202, 22);
            addNewCargoToolStripMenuItem.Text = "Add New Cargo";
            addNewCargoToolStripMenuItem.Click += addNewCargoToolStripMenuItem_Click;
            // 
            // addNewItemToolStripMenuItem
            // 
            addNewItemToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewItemToolStripMenuItem.Name = "addNewItemToolStripMenuItem";
            addNewItemToolStripMenuItem.Size = new Size(202, 22);
            addNewItemToolStripMenuItem.Text = "Add New Item";
            addNewItemToolStripMenuItem.Click += addNewItemToolStripMenuItem_Click;
            // 
            // removeSelectedRandomPresetToolStripmenuItem
            // 
            removeSelectedRandomPresetToolStripmenuItem.ForeColor = SystemColors.Control;
            removeSelectedRandomPresetToolStripmenuItem.Name = "removeSelectedRandomPresetToolStripmenuItem";
            removeSelectedRandomPresetToolStripmenuItem.Size = new Size(202, 22);
            removeSelectedRandomPresetToolStripmenuItem.Text = "Remove Selected";
            removeSelectedRandomPresetToolStripmenuItem.Click += removeSelectedToolStripMenuItem1_Click;
            // 
            // SpawnableTypesCM
            // 
            SpawnableTypesCM.BackColor = Color.FromArgb(60, 63, 65);
            SpawnableTypesCM.Items.AddRange(new ToolStripItem[] { addNewSpawnableTypesFileToolStripMenuItem, addNewSpawnableTypeToolStripMenuItem, addNewHoarderToolStripMenuItem, addNewTagToolStripMenuItem, addNewDamageToolStripMenuItem, addNewItemToolStripMenuItem1, addNewCargoToolStripMenuItem1, addNewAttachmentToolStripMenuItem, removeSelectedToolStripMenuItem1 });
            SpawnableTypesCM.Name = "TypesCM";
            SpawnableTypesCM.ShowImageMargin = false;
            SpawnableTypesCM.Size = new Size(212, 202);
            // 
            // addNewSpawnableTypesFileToolStripMenuItem
            // 
            addNewSpawnableTypesFileToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewSpawnableTypesFileToolStripMenuItem.Name = "addNewSpawnableTypesFileToolStripMenuItem";
            addNewSpawnableTypesFileToolStripMenuItem.Size = new Size(211, 22);
            addNewSpawnableTypesFileToolStripMenuItem.Text = "Add New Spawnable Types File";
            addNewSpawnableTypesFileToolStripMenuItem.Click += addNewSpawnableTypesFileToolStripMenuItem_Click;
            // 
            // addNewSpawnableTypeToolStripMenuItem
            // 
            addNewSpawnableTypeToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewSpawnableTypeToolStripMenuItem.Name = "addNewSpawnableTypeToolStripMenuItem";
            addNewSpawnableTypeToolStripMenuItem.Size = new Size(211, 22);
            addNewSpawnableTypeToolStripMenuItem.Text = "Add New Spawnable Type";
            addNewSpawnableTypeToolStripMenuItem.Click += addNewSpawnableTypeToolStripMenuItem_Click;
            // 
            // addNewHoarderToolStripMenuItem
            // 
            addNewHoarderToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewHoarderToolStripMenuItem.Name = "addNewHoarderToolStripMenuItem";
            addNewHoarderToolStripMenuItem.Size = new Size(211, 22);
            addNewHoarderToolStripMenuItem.Text = "Add New Hoarder";
            addNewHoarderToolStripMenuItem.Click += addNewHoarderToolStripMenuItem_Click;
            // 
            // addNewTagToolStripMenuItem
            // 
            addNewTagToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewTagToolStripMenuItem.Name = "addNewTagToolStripMenuItem";
            addNewTagToolStripMenuItem.Size = new Size(211, 22);
            addNewTagToolStripMenuItem.Text = "Add New Tag";
            addNewTagToolStripMenuItem.Click += addNewTagToolStripMenuItem_Click;
            // 
            // addNewDamageToolStripMenuItem
            // 
            addNewDamageToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewDamageToolStripMenuItem.Name = "addNewDamageToolStripMenuItem";
            addNewDamageToolStripMenuItem.Size = new Size(211, 22);
            addNewDamageToolStripMenuItem.Text = "Add New Damage";
            addNewDamageToolStripMenuItem.Click += addNewDamageToolStripMenuItem_Click;
            // 
            // addNewItemToolStripMenuItem1
            // 
            addNewItemToolStripMenuItem1.ForeColor = SystemColors.Control;
            addNewItemToolStripMenuItem1.Name = "addNewItemToolStripMenuItem1";
            addNewItemToolStripMenuItem1.Size = new Size(211, 22);
            addNewItemToolStripMenuItem1.Text = "Add New Item";
            addNewItemToolStripMenuItem1.Click += addNewItemToolStripMenuItem1_Click;
            // 
            // addNewCargoToolStripMenuItem1
            // 
            addNewCargoToolStripMenuItem1.ForeColor = SystemColors.Control;
            addNewCargoToolStripMenuItem1.Name = "addNewCargoToolStripMenuItem1";
            addNewCargoToolStripMenuItem1.Size = new Size(211, 22);
            addNewCargoToolStripMenuItem1.Text = "Add New Cargo";
            addNewCargoToolStripMenuItem1.Click += addNewCargoToolStripMenuItem1_Click;
            // 
            // addNewAttachmentToolStripMenuItem
            // 
            addNewAttachmentToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewAttachmentToolStripMenuItem.Name = "addNewAttachmentToolStripMenuItem";
            addNewAttachmentToolStripMenuItem.Size = new Size(211, 22);
            addNewAttachmentToolStripMenuItem.Text = "Add New Attachment";
            addNewAttachmentToolStripMenuItem.Click += addNewAttachmentToolStripMenuItem_Click;
            // 
            // removeSelectedToolStripMenuItem1
            // 
            removeSelectedToolStripMenuItem1.ForeColor = SystemColors.Control;
            removeSelectedToolStripMenuItem1.Name = "removeSelectedToolStripMenuItem1";
            removeSelectedToolStripMenuItem1.Size = new Size(211, 22);
            removeSelectedToolStripMenuItem1.Text = "Remove Selected";
            removeSelectedToolStripMenuItem1.Click += removeSelectedToolStripMenuItem1_Click_1;
            // 
            // SpawnGearPresetCM
            // 
            SpawnGearPresetCM.BackColor = Color.FromArgb(60, 63, 65);
            SpawnGearPresetCM.Items.AddRange(new ToolStripItem[] { addNewSpawnGEarPresetFileToolStripMenuItem, addNewAttachmentSlotItemSetToolStripMenuItem, addNewDisctreetItemSetToolStripMenuItem, addNewComplexChildSetToolStripMenuItem, addNewDiscreetUnsortedItemSetToolStripMenuItem, SpawnGearremoveSelectedToolStripMenuItem2 });
            SpawnGearPresetCM.Name = "TypesCM";
            SpawnGearPresetCM.ShowImageMargin = false;
            SpawnGearPresetCM.Size = new Size(241, 136);
            // 
            // addNewSpawnGEarPresetFileToolStripMenuItem
            // 
            addNewSpawnGEarPresetFileToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewSpawnGEarPresetFileToolStripMenuItem.Name = "addNewSpawnGEarPresetFileToolStripMenuItem";
            addNewSpawnGEarPresetFileToolStripMenuItem.Size = new Size(240, 22);
            addNewSpawnGEarPresetFileToolStripMenuItem.Text = "Add New Spawn Gear Preset File";
            addNewSpawnGEarPresetFileToolStripMenuItem.Click += addNewSpawnGEarPresetFileToolStripMenuItem_Click;
            // 
            // addNewAttachmentSlotItemSetToolStripMenuItem
            // 
            addNewAttachmentSlotItemSetToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewAttachmentSlotItemSetToolStripMenuItem.Name = "addNewAttachmentSlotItemSetToolStripMenuItem";
            addNewAttachmentSlotItemSetToolStripMenuItem.Size = new Size(240, 22);
            addNewAttachmentSlotItemSetToolStripMenuItem.Text = "Add New Attachment Slot Item Set";
            addNewAttachmentSlotItemSetToolStripMenuItem.Click += addNewAttachmentSlotItemSetToolStripMenuItem_Click;
            // 
            // addNewDisctreetItemSetToolStripMenuItem
            // 
            addNewDisctreetItemSetToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewDisctreetItemSetToolStripMenuItem.Name = "addNewDisctreetItemSetToolStripMenuItem";
            addNewDisctreetItemSetToolStripMenuItem.Size = new Size(240, 22);
            addNewDisctreetItemSetToolStripMenuItem.Text = "Add New Disctreet Item Set";
            addNewDisctreetItemSetToolStripMenuItem.Click += addNewDisctreetItemSetToolStripMenuItem_Click;
            // 
            // addNewComplexChildSetToolStripMenuItem
            // 
            addNewComplexChildSetToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewComplexChildSetToolStripMenuItem.Name = "addNewComplexChildSetToolStripMenuItem";
            addNewComplexChildSetToolStripMenuItem.Size = new Size(240, 22);
            addNewComplexChildSetToolStripMenuItem.Text = "Add New Complex Child Set";
            addNewComplexChildSetToolStripMenuItem.Click += addNewComplexChildSetToolStripMenuItem_Click;
            // 
            // addNewDiscreetUnsortedItemSetToolStripMenuItem
            // 
            addNewDiscreetUnsortedItemSetToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewDiscreetUnsortedItemSetToolStripMenuItem.Name = "addNewDiscreetUnsortedItemSetToolStripMenuItem";
            addNewDiscreetUnsortedItemSetToolStripMenuItem.Size = new Size(240, 22);
            addNewDiscreetUnsortedItemSetToolStripMenuItem.Text = "Add New Discreet Unsorted Item Set";
            addNewDiscreetUnsortedItemSetToolStripMenuItem.Click += addNewDiscreetUnsortedItemSetToolStripMenuItem_Click;
            // 
            // SpawnGearremoveSelectedToolStripMenuItem2
            // 
            SpawnGearremoveSelectedToolStripMenuItem2.ForeColor = SystemColors.Control;
            SpawnGearremoveSelectedToolStripMenuItem2.Name = "SpawnGearremoveSelectedToolStripMenuItem2";
            SpawnGearremoveSelectedToolStripMenuItem2.Size = new Size(240, 22);
            SpawnGearremoveSelectedToolStripMenuItem2.Text = "Remove Selected";
            SpawnGearremoveSelectedToolStripMenuItem2.Click += SpawnGearremoveSelectedToolStripMenuItem2_Click;
            // 
            // PlayerRestrictedAreaCM
            // 
            PlayerRestrictedAreaCM.BackColor = Color.FromArgb(60, 63, 65);
            PlayerRestrictedAreaCM.Items.AddRange(new ToolStripItem[] { addNewPRAFileToolStripMenuItem, addNewPRABoxToolStripMenuItem, addNewPRASafePositionToolStripMenuItem, removePRASelectedToolStripMenuItem });
            PlayerRestrictedAreaCM.Name = "TypesCM";
            PlayerRestrictedAreaCM.ShowImageMargin = false;
            PlayerRestrictedAreaCM.Size = new Size(195, 92);
            // 
            // addNewPRAFileToolStripMenuItem
            // 
            addNewPRAFileToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewPRAFileToolStripMenuItem.Name = "addNewPRAFileToolStripMenuItem";
            addNewPRAFileToolStripMenuItem.Size = new Size(194, 22);
            addNewPRAFileToolStripMenuItem.Text = "Add New PRA File";
            addNewPRAFileToolStripMenuItem.Click += addNewPRAFileToolStripMenuItem_Click;
            // 
            // addNewPRABoxToolStripMenuItem
            // 
            addNewPRABoxToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewPRABoxToolStripMenuItem.Name = "addNewPRABoxToolStripMenuItem";
            addNewPRABoxToolStripMenuItem.Size = new Size(194, 22);
            addNewPRABoxToolStripMenuItem.Text = "Add New PRA Box";
            addNewPRABoxToolStripMenuItem.Click += addNewPRABoxToolStripMenuItem_Click;
            // 
            // addNewPRASafePositionToolStripMenuItem
            // 
            addNewPRASafePositionToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewPRASafePositionToolStripMenuItem.Name = "addNewPRASafePositionToolStripMenuItem";
            addNewPRASafePositionToolStripMenuItem.Size = new Size(194, 22);
            addNewPRASafePositionToolStripMenuItem.Text = "Add New PRA Safe Position";
            addNewPRASafePositionToolStripMenuItem.Click += addNewPRASafePositionToolStripMenuItem_Click;
            // 
            // removePRASelectedToolStripMenuItem
            // 
            removePRASelectedToolStripMenuItem.ForeColor = SystemColors.Control;
            removePRASelectedToolStripMenuItem.Name = "removePRASelectedToolStripMenuItem";
            removePRASelectedToolStripMenuItem.Size = new Size(194, 22);
            removePRASelectedToolStripMenuItem.Text = "Remove Selected";
            removePRASelectedToolStripMenuItem.Click += removePRASelectedToolStripMenuItem_Click;
            // 
            // ObjectSpawnerArrCM
            // 
            ObjectSpawnerArrCM.BackColor = Color.FromArgb(60, 63, 65);
            ObjectSpawnerArrCM.Items.AddRange(new ToolStripItem[] { addNewObjectSpawnerArrFileToolStripMenuItem, removeSelectedObjectSpawnerArrToolStripMenuItem });
            ObjectSpawnerArrCM.Name = "TypesCM";
            ObjectSpawnerArrCM.ShowImageMargin = false;
            ObjectSpawnerArrCM.Size = new Size(222, 48);
            // 
            // addNewObjectSpawnerArrFileToolStripMenuItem
            // 
            addNewObjectSpawnerArrFileToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewObjectSpawnerArrFileToolStripMenuItem.Name = "addNewObjectSpawnerArrFileToolStripMenuItem";
            addNewObjectSpawnerArrFileToolStripMenuItem.Size = new Size(221, 22);
            addNewObjectSpawnerArrFileToolStripMenuItem.Text = "Add New ObjectSpawner Arr File";
            addNewObjectSpawnerArrFileToolStripMenuItem.Click += addNewObjectSpawnerArrFileToolStripMenuItem_Click;
            // 
            // removeSelectedObjectSpawnerArrToolStripMenuItem
            // 
            removeSelectedObjectSpawnerArrToolStripMenuItem.ForeColor = SystemColors.Control;
            removeSelectedObjectSpawnerArrToolStripMenuItem.Name = "removeSelectedObjectSpawnerArrToolStripMenuItem";
            removeSelectedObjectSpawnerArrToolStripMenuItem.Size = new Size(221, 22);
            removeSelectedObjectSpawnerArrToolStripMenuItem.Text = "Remove Selected";
            removeSelectedObjectSpawnerArrToolStripMenuItem.Click += removeSelectedObjectSpawnerArrToolStripMenuItem_Click;
            // 
            // cfgEffectsAreaCM
            // 
            cfgEffectsAreaCM.BackColor = Color.FromArgb(60, 63, 65);
            cfgEffectsAreaCM.Items.AddRange(new ToolStripItem[] { addNewEffectAreaToolStripMenuItem, usePlayerDataToolStripMenuItem, removePlayerDataToolStripMenuItem, removeEffectAreaToolStripMenuItem, addNewSafePositionToolStripMenuItem, removeSafePositionToolStripMenuItem });
            cfgEffectsAreaCM.Name = "TypesCM";
            cfgEffectsAreaCM.ShowImageMargin = false;
            cfgEffectsAreaCM.Size = new Size(170, 136);
            // 
            // addNewEffectAreaToolStripMenuItem
            // 
            addNewEffectAreaToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewEffectAreaToolStripMenuItem.Name = "addNewEffectAreaToolStripMenuItem";
            addNewEffectAreaToolStripMenuItem.Size = new Size(169, 22);
            addNewEffectAreaToolStripMenuItem.Text = "Add New Effect Area";
            addNewEffectAreaToolStripMenuItem.Click += addNewEffectAreaToolStripMenuItem_Click;
            // 
            // usePlayerDataToolStripMenuItem
            // 
            usePlayerDataToolStripMenuItem.ForeColor = SystemColors.Control;
            usePlayerDataToolStripMenuItem.Name = "usePlayerDataToolStripMenuItem";
            usePlayerDataToolStripMenuItem.Size = new Size(169, 22);
            usePlayerDataToolStripMenuItem.Text = "Use Player Data";
            usePlayerDataToolStripMenuItem.Click += usePlayerDataToolStripMenuItem_Click;
            // 
            // removePlayerDataToolStripMenuItem
            // 
            removePlayerDataToolStripMenuItem.ForeColor = SystemColors.Control;
            removePlayerDataToolStripMenuItem.Name = "removePlayerDataToolStripMenuItem";
            removePlayerDataToolStripMenuItem.Size = new Size(169, 22);
            removePlayerDataToolStripMenuItem.Text = "Remove Player Data";
            removePlayerDataToolStripMenuItem.Click += removePlayerDataToolStripMenuItem_Click;
            // 
            // removeEffectAreaToolStripMenuItem
            // 
            removeEffectAreaToolStripMenuItem.ForeColor = SystemColors.Control;
            removeEffectAreaToolStripMenuItem.Name = "removeEffectAreaToolStripMenuItem";
            removeEffectAreaToolStripMenuItem.Size = new Size(169, 22);
            removeEffectAreaToolStripMenuItem.Text = "Remove Effect Area";
            removeEffectAreaToolStripMenuItem.Click += removeEffectAreaToolStripMenuItem_Click;
            // 
            // addNewSafePositionToolStripMenuItem
            // 
            addNewSafePositionToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewSafePositionToolStripMenuItem.Name = "addNewSafePositionToolStripMenuItem";
            addNewSafePositionToolStripMenuItem.Size = new Size(169, 22);
            addNewSafePositionToolStripMenuItem.Text = "Add New Safe Position";
            addNewSafePositionToolStripMenuItem.Click += addNewSafePositionToolStripMenuItem_Click;
            // 
            // removeSafePositionToolStripMenuItem
            // 
            removeSafePositionToolStripMenuItem.ForeColor = SystemColors.Control;
            removeSafePositionToolStripMenuItem.Name = "removeSafePositionToolStripMenuItem";
            removeSafePositionToolStripMenuItem.Size = new Size(169, 22);
            removeSafePositionToolStripMenuItem.Text = "Remove Safe Position";
            removeSafePositionToolStripMenuItem.Click += removeSafePositionToolStripMenuItem_Click;
            // 
            // PlayerSpawnsCM
            // 
            PlayerSpawnsCM.BackColor = Color.FromArgb(60, 63, 65);
            PlayerSpawnsCM.Items.AddRange(new ToolStripItem[] { addNewSpawnPositionToolStripMenuItem, removeSpawnPositionToolStripMenuItem, addNewSpawnGroupToolStripMenuItem, removeSpawnGroupToolStripMenuItem });
            PlayerSpawnsCM.Name = "TypesCM";
            PlayerSpawnsCM.ShowImageMargin = false;
            PlayerSpawnsCM.Size = new Size(183, 92);
            // 
            // addNewSpawnPositionToolStripMenuItem
            // 
            addNewSpawnPositionToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewSpawnPositionToolStripMenuItem.Name = "addNewSpawnPositionToolStripMenuItem";
            addNewSpawnPositionToolStripMenuItem.Size = new Size(182, 22);
            addNewSpawnPositionToolStripMenuItem.Text = "Add New Spawn Position";
            addNewSpawnPositionToolStripMenuItem.Click += addNewSpawnPositionToolStripMenuItem_Click;
            // 
            // removeSpawnPositionToolStripMenuItem
            // 
            removeSpawnPositionToolStripMenuItem.ForeColor = SystemColors.Control;
            removeSpawnPositionToolStripMenuItem.Name = "removeSpawnPositionToolStripMenuItem";
            removeSpawnPositionToolStripMenuItem.Size = new Size(182, 22);
            removeSpawnPositionToolStripMenuItem.Text = "Remove Spawn Position";
            removeSpawnPositionToolStripMenuItem.Click += removeSpawnPositionToolStripMenuItem_Click;
            // 
            // addNewSpawnGroupToolStripMenuItem
            // 
            addNewSpawnGroupToolStripMenuItem.ForeColor = SystemColors.Control;
            addNewSpawnGroupToolStripMenuItem.Name = "addNewSpawnGroupToolStripMenuItem";
            addNewSpawnGroupToolStripMenuItem.Size = new Size(182, 22);
            addNewSpawnGroupToolStripMenuItem.Text = "Add New Spawn Group";
            addNewSpawnGroupToolStripMenuItem.Click += addNewSpawnGroupToolStripMenuItem_Click;
            // 
            // removeSpawnGroupToolStripMenuItem
            // 
            removeSpawnGroupToolStripMenuItem.ForeColor = SystemColors.Control;
            removeSpawnGroupToolStripMenuItem.Name = "removeSpawnGroupToolStripMenuItem";
            removeSpawnGroupToolStripMenuItem.Size = new Size(182, 22);
            removeSpawnGroupToolStripMenuItem.Text = "Remove Spawn Group";
            removeSpawnGroupToolStripMenuItem.Click += removeSpawnGroupToolStripMenuItem_Click;
            // 
            // IgnoreListCM
            // 
            IgnoreListCM.BackColor = Color.FromArgb(60, 63, 65);
            IgnoreListCM.Items.AddRange(new ToolStripItem[] { removeClassnameToolStripMenuItem, addClassnameToolStripMenuItem });
            IgnoreListCM.Name = "TypesCM";
            IgnoreListCM.ShowImageMargin = false;
            IgnoreListCM.Size = new Size(153, 48);
            // 
            // removeClassnameToolStripMenuItem
            // 
            removeClassnameToolStripMenuItem.ForeColor = SystemColors.Control;
            removeClassnameToolStripMenuItem.Name = "removeClassnameToolStripMenuItem";
            removeClassnameToolStripMenuItem.Size = new Size(152, 22);
            removeClassnameToolStripMenuItem.Text = "Remove Classname";
            removeClassnameToolStripMenuItem.Click += removeClassnameToolStripMenuItem_Click;
            // 
            // addClassnameToolStripMenuItem
            // 
            addClassnameToolStripMenuItem.ForeColor = SystemColors.Control;
            addClassnameToolStripMenuItem.Name = "addClassnameToolStripMenuItem";
            addClassnameToolStripMenuItem.Size = new Size(152, 22);
            addClassnameToolStripMenuItem.Text = "Add Classname";
            addClassnameToolStripMenuItem.Click += addClassnameToolStripMenuItem_Click;
            // 
            // MapGroupPosCM
            // 
            MapGroupPosCM.BackColor = Color.FromArgb(60, 63, 65);
            MapGroupPosCM.Items.AddRange(new ToolStripItem[] { removeSelectedPositionsToolStripMenuItem });
            MapGroupPosCM.Name = "TypesCM";
            MapGroupPosCM.ShowImageMargin = false;
            MapGroupPosCM.Size = new Size(190, 26);
            // 
            // removeSelectedPositionsToolStripMenuItem
            // 
            removeSelectedPositionsToolStripMenuItem.ForeColor = SystemColors.Control;
            removeSelectedPositionsToolStripMenuItem.Name = "removeSelectedPositionsToolStripMenuItem";
            removeSelectedPositionsToolStripMenuItem.Size = new Size(189, 22);
            removeSelectedPositionsToolStripMenuItem.Text = "Remove selected Positions";
            removeSelectedPositionsToolStripMenuItem.Click += removeSelectedPositionsToolStripMenuItem_Click;
            // 
            // EconomyForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(60, 63, 65);
            ClientSize = new Size(1170, 642);
            Controls.Add(panel1);
            Controls.Add(splitContainer1);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            Name = "EconomyForm";
            Text = "Form1";
            FormClosing += EconomyForm_FormClosing;
            FormClosed += EconomyForm_FormClosed;
            Load += EconomyForm_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            EditPropertyCMS.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            EventSpawnContextMenu.ResumeLayout(false);
            TypesCM.ResumeLayout(false);
            EventsCM.ResumeLayout(false);
            RandomPresetsCM.ResumeLayout(false);
            SpawnableTypesCM.ResumeLayout(false);
            SpawnGearPresetCM.ResumeLayout(false);
            PlayerRestrictedAreaCM.ResumeLayout(false);
            ObjectSpawnerArrCM.ResumeLayout(false);
            cfgEffectsAreaCM.ResumeLayout(false);
            PlayerSpawnsCM.ResumeLayout(false);
            IgnoreListCM.ResumeLayout(false);
            MapGroupPosCM.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private MapViewerControl _mapControl;
        private SplitContainer splitContainer1;
        private ContextMenuStrip EditPropertyCMS;
        private ToolStripMenuItem editPropertyToolStripMenuItem;
        private Panel panel1;
        private Button SaveButton;
        private MultiSelectTreeView EconomyTV;
        private ToolStripMenuItem setToDefaultToolStripMenuItem;
        private ContextMenuStrip EventSpawnContextMenu;
        private System.Windows.Forms.ToolStripMenuItem addNewEventSpawnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedEventSpawnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importPositionFromdzeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportPositionTodzeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewPosirtionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeSelectedPositionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeAllPositionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportGroupSpawnTodzeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importPositionAndCreateEventgroupFormdzeToolStripMenuItem;
        private ContextMenuStrip TypesCM;
        private ToolStripMenuItem addNewTypesToolStripMenuItem;
        private ToolStripMenuItem removeSelectedToolStripMenuItem;
        private ContextMenuStrip EventsCM;
        private ToolStripMenuItem AddNewEventsToolstripMenuItem;
        private ToolStripMenuItem RemoveEventsToolStripMenuItem;
        private ContextMenuStrip RandomPresetsCM;
        private ToolStripMenuItem addNewRandomPresetFileToolStripMenuItem;
        private ToolStripMenuItem addNewAttchementToolStripMenuItem;
        private ToolStripMenuItem addNewCargoToolStripMenuItem;
        private ToolStripMenuItem addNewItemToolStripMenuItem;
        private ToolStripMenuItem removeSelectedRandomPresetToolStripmenuItem;
        private ContextMenuStrip SpawnableTypesCM;
        private ToolStripMenuItem addNewSpawnableTypesFileToolStripMenuItem;
        private ToolStripMenuItem addNewSpawnableTypeToolStripMenuItem;
        private ToolStripMenuItem addNewHoarderToolStripMenuItem;
        private ToolStripMenuItem addNewTagToolStripMenuItem;
        private ToolStripMenuItem addNewDamageToolStripMenuItem;
        private ToolStripMenuItem addNewItemToolStripMenuItem1;
        private ToolStripMenuItem addNewCargoToolStripMenuItem1;
        private ToolStripMenuItem addNewAttachmentToolStripMenuItem;
        private ToolStripMenuItem removeSelectedToolStripMenuItem1;
        private ContextMenuStrip SpawnGearPresetCM;
        private ToolStripMenuItem addNewSpawnGEarPresetFileToolStripMenuItem;
        private ToolStripMenuItem addNewAttachmentSlotItemSetToolStripMenuItem;
        private ToolStripMenuItem addNewDisctreetItemSetToolStripMenuItem;
        private ToolStripMenuItem addNewComplexChildSetToolStripMenuItem;
        private ToolStripMenuItem addNewDiscreetUnsortedItemSetToolStripMenuItem;
        private ToolStripMenuItem SpawnGearremoveSelectedToolStripMenuItem2;
        private ContextMenuStrip PlayerRestrictedAreaCM;
        private ToolStripMenuItem addNewPRAFileToolStripMenuItem;
        private ToolStripMenuItem addNewPRABoxToolStripMenuItem;
        private ToolStripMenuItem addNewPRASafePositionToolStripMenuItem;
        private ToolStripMenuItem removePRASelectedToolStripMenuItem;
        private ContextMenuStrip ObjectSpawnerArrCM;
        private ToolStripMenuItem addNewObjectSpawnerArrFileToolStripMenuItem;
        private ToolStripMenuItem removeSelectedObjectSpawnerArrToolStripMenuItem2;
        private ToolStripMenuItem removeSelectedObjectSpawnerArrToolStripMenuItem;
        private ContextMenuStrip cfgEffectsAreaCM;
        private ToolStripMenuItem addNewEffectAreaToolStripMenuItem;
        private ToolStripMenuItem usePlayerDataToolStripMenuItem;
        private ToolStripMenuItem removePlayerDataToolStripMenuItem;
        private ToolStripMenuItem removeEffectAreaToolStripMenuItem;
        private ToolStripMenuItem addNewSafePositionToolStripMenuItem;
        private ToolStripMenuItem removeSafePositionToolStripMenuItem;
        private ContextMenuStrip PlayerSpawnsCM;
        private ToolStripMenuItem addNewSpawnPositionToolStripMenuItem;
        private ToolStripMenuItem removeSpawnPositionToolStripMenuItem;
        private ToolStripMenuItem addNewSpawnGroupToolStripMenuItem;
        private ToolStripMenuItem removeSpawnGroupToolStripMenuItem;
        private ContextMenuStrip IgnoreListCM;
        private ToolStripMenuItem removeClassnameToolStripMenuItem;
        private ToolStripMenuItem addClassnameToolStripMenuItem;
        private ContextMenuStrip MapGroupPosCM;
        private ToolStripMenuItem removeSelectedPositionsToolStripMenuItem;
        private CheckBox TerritorieszonesCB;
    }
}
