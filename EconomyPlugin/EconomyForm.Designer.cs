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
            EditPropertyCMS.Size = new Size(123, 48);
            // 
            // editPropertyToolStripMenuItem
            // 
            editPropertyToolStripMenuItem.ForeColor = SystemColors.Control;
            editPropertyToolStripMenuItem.Name = "editPropertyToolStripMenuItem";
            editPropertyToolStripMenuItem.Size = new Size(122, 22);
            editPropertyToolStripMenuItem.Text = "Edit Property";
            editPropertyToolStripMenuItem.Click += editPropertyToolStripMenuItem_Click;
            // 
            // setToDefaultToolStripMenuItem
            // 
            setToDefaultToolStripMenuItem.ForeColor = SystemColors.Control;
            setToDefaultToolStripMenuItem.Name = "setToDefaultToolStripMenuItem";
            setToDefaultToolStripMenuItem.Size = new Size(122, 22);
            setToDefaultToolStripMenuItem.Text = "Set To Default";
            setToDefaultToolStripMenuItem.Click += setToDefaultToolStripMenuItem_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(60, 63, 65);
            panel1.Controls.Add(SaveButton);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1170, 31);
            panel1.TabIndex = 4;
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
            RandomPresetsCM.Size = new Size(203, 136);
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
            EventSpawnContextMenu.ResumeLayout(false);
            TypesCM.ResumeLayout(false);
            EventsCM.ResumeLayout(false);
            RandomPresetsCM.ResumeLayout(false);
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
    }
}
