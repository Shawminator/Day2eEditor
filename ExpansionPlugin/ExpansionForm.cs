using Day2eEditor;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Windows.Forms;
using System.Windows.Forms.Design.Behavior;
using System.Xml.Linq;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ExpansionPlugin
{
    public partial class ExpansionForm : Form
    {
        private IUIHandler? _currentHandler;
        public MapViewerControl MapControl => _mapControl;
        private IPluginForm _plugin;
        private TreeNode? currentTreeNode;
        private ExpansionManager _expansionManager { get; set; }
        public MapData MapData { get; private set; }

        #region Dictionarys
        private Dictionary<Type, Action<TreeNode, List<TreeNode>>> _typeHandlers;
        private Dictionary<string, Action<TreeNode, List<TreeNode>>> _stringHandlers;
        private Dictionary<Type, Action<TreeNode>> _typeContextMenus;
        private Dictionary<string, Action<TreeNode>> _stringContextMenus;
        #endregion Dictionarys

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        public ExpansionForm(IPluginForm plugin)
        {
            InitializeComponent();
            _plugin = plugin;
            _expansionManager = new ExpansionManager();
            _expansionManager.SetExpansionStuff();
            _expansionManager.SetExternalFiles();
            AppServices.Register(_expansionManager);

        }
        private void initializeShowControlHandlers()
        {
            // ----------------------
            // Type handlers
            // ----------------------
            _typeHandlers = new Dictionary<Type, Action<TreeNode, List<TreeNode>>>
            {
                //vec3
                [typeof(Vec3)] = (node, selected) =>
                {
                    Vec3 v3 = node.Tag as Vec3;
                    var control = new Vector3Control();
                    control.PositionChanged += (updatedPos) =>
                    {
                        _mapControl.ClearDrawables();
                        DrawbaseAIPatrols(node.Parent.Parent.Parent.Parent.Tag as ExpansionAIPatrolConfig);
                    };
                    ShowHandler(control, typeof(ExpansionAIPatrolConfig), v3, selected);
                    if (node.Parent.Tag.ToString() == "AIPatrolWayPoints")
                    {
                        ExpansionAIPatrol ExpansionAIPatrol = node.Parent.Parent.Nodes[0].Tag as ExpansionAIPatrol;
                        SetupAIPatrols(ExpansionAIPatrol, node);
                        _mapControl.EnsureVisible(new PointF(v3.X, v3.Z));
                    }
                },
                //Loadouts
                [typeof(AILoadouts)] = (node, selected) =>
                {
                    AILoadouts AILoadouts = node.Tag as AILoadouts;
                    ShowHandler(new ExpansionAILoadoutsControl(), typeof(AILoadouts), AILoadouts, selected);
                },
                [typeof(Inventoryattachment)] = (node, selected) =>
                {
                    Inventoryattachment Inventoryattachment = node.Tag as Inventoryattachment;
                    ShowHandler(new ExpansionInventoryattachmentControl(), typeof(AILoadouts), Inventoryattachment, selected);
                },
                //Airdrops
                [typeof(ExpansionAirdropSettings)] = (node, selected) =>
                {
                    ExpansionAirdropSettings ExpansionAirdropSettings = node.Tag as ExpansionAirdropSettings;
                    ShowHandler(new ExpansionAirdropSettingsControl(), typeof(ExpansionAirdropConfig), ExpansionAirdropSettings, selected);
                },
                [typeof(ExpansionLootContainer)] = (node, selected) =>
                {
                    ExpansionLootContainer ExpansionLootContainer = node.Tag as ExpansionLootContainer;
                    ShowHandler(new ExpansionLootContainerControl(), typeof(ExpansionAirdropConfig), ExpansionLootContainer, selected);
                },
                //AI
                [typeof(ExpansionAIRoamingLocation)] = (node, selected) =>
                {
                    ExpansionAIRoamingLocation ExpansionAIRoamingLocation = node.Tag as ExpansionAIRoamingLocation;
                    ShowHandler(new AIRoamingLocationControl(), typeof(ExpansionAILocationConfig), ExpansionAIRoamingLocation, selected);
                    SetupAILocationRoamingLocations(ExpansionAIRoamingLocation, node);
                    _mapControl.EnsureVisible(new PointF((float)ExpansionAIRoamingLocation.Position[0], (float)ExpansionAIRoamingLocation.Position[2]));
                },
                [typeof(ExpansionAIPatrolSettings)] = (node, selected) =>
                {
                    ExpansionAIPatrolSettings ExpansionAIPatrolSettings = node.Tag as ExpansionAIPatrolSettings;
                    ShowHandler(new AIPAtrolGeneralControl(), typeof(ExpansionAIPatrolConfig), ExpansionAIPatrolSettings, selected);
                },
                [typeof(Loadbalancingcategorie)] = (node, selected) =>
                {
                    Loadbalancingcategorie Loadbalancingcategorie = node.Tag as Loadbalancingcategorie;
                    ShowHandler(new AIPatrolLoadbalancingcategorieControl(), typeof(ExpansionAIPatrolConfig), Loadbalancingcategorie, selected);
                },
                [typeof(Loadbalancingcategories)] = (node, selected) =>
                {
                    Loadbalancingcategories Loadbalancingcategories = node.Tag as Loadbalancingcategories;
                    ShowHandler(new AIPAtrolLoadbalancingcategoriesControl(), typeof(ExpansionAIPatrolConfig), Loadbalancingcategories, selected);
                },
                [typeof(ExpansionAINoGoArea)] = (node, selected) =>
                {
                    ExpansionAINoGoArea ExpansionAINoGoArea = node.Tag as ExpansionAINoGoArea;
                    var control = new ExpansionAINoGoAreaControl();
                    control.PositionChanged += (updatedPos) =>
                    {
                        _mapControl.ClearDrawables(); ;
                        DrawbaseAILocationNoGoAreas(node.FindParentOfType<ExpansionAILocationConfig>());
                    };
                    control.RadiusChanged += (updatedRadius) =>
                    {
                        _mapControl.ClearDrawables(); ;
                        DrawbaseAILocationNoGoAreas(node.FindParentOfType<ExpansionAILocationConfig>());
                    };



                    ShowHandler(control, typeof(ExpansionAILocationConfig), ExpansionAINoGoArea, selected);
                    SetupAILocationNoGoAreas(ExpansionAINoGoArea, node);
                    _mapControl.EnsureVisible(new PointF(ExpansionAINoGoArea._Position.X, ExpansionAINoGoArea._Position.Z));
                },
                //BaseBuilding
                [typeof(ExpansionBuildNoBuildZone)] = (node, selected) =>
                {
                    ExpansionBuildNoBuildZone ExpansionBuildNoBuildZone = node.Tag as ExpansionBuildNoBuildZone;
                    var control = new ExpansionBuildNoBuildZoneControl();
                    control.PositionChanged += (updatedPos) =>
                    {
                        _mapControl.ClearDrawables(); ;
                        DrawbasebuildingNoBuildZones(node.FindParentOfType<ExpansionBaseBuildingConfig>());
                    };
                    control.RadiusChanged += (updatedRadius) =>
                    {
                        _mapControl.ClearDrawables(); ;
                        DrawbasebuildingNoBuildZones(node.FindParentOfType<ExpansionBaseBuildingConfig>());
                    };
                    ShowHandler(control, typeof(ExpansionBaseBuildingConfig), ExpansionBuildNoBuildZone, selected);
                    SetupBaseBuildingNoBuildZone(ExpansionBuildNoBuildZone, node);
                    _mapControl.EnsureVisible(new PointF((float)ExpansionBuildNoBuildZone.Center[0], (float)ExpansionBuildNoBuildZone.Center[2]));
                },
                [typeof(ExpansionAISettings)] = (node, selected) =>
                {
                    ExpansionAISettings ExpansionAISettings = node.Tag as ExpansionAISettings;
                    ShowHandler(new AISettingsConfigControl(), typeof(ExpansionAIConfig), ExpansionAISettings, selected);
                },
                [typeof(AILightEntries)] = (node, selected) =>
                {
                    AILightEntries AILightEntries = node.Tag as AILightEntries;
                    ShowHandler(new AILightControl(), typeof(ExpansionAIConfig), AILightEntries, selected);
                },
                //Book 
                [typeof(ExpansionBookCraftingCategory)] = (node, selected) =>
                {
                    ExpansionBookCraftingCategory ExpansionBookCraftingCategory = node.Tag as ExpansionBookCraftingCategory;
                    ShowHandler(new ExpansionBookCraftingCategoryControl(), typeof(ExpansionBookConfig), ExpansionBookCraftingCategory, selected);
                },
                [typeof(ExpansionBookRuleCategory)] = (node, selected) =>
                {
                    ExpansionBookRuleCategory ExpansionBookRuleCategory = node.Tag as ExpansionBookRuleCategory;
                    ShowHandler(new ExpansionBookRuleCategoryControl(), typeof(ExpansionBookConfig), ExpansionBookRuleCategory, selected);
                },
                [typeof(ExpansionBookRule)] = (node, selected) =>
                {
                    ExpansionBookRule ExpansionBookRule = node.Tag as ExpansionBookRule;
                    ShowHandler(new ExpansionBookRuleControl(), typeof(ExpansionBookConfig), ExpansionBookRule, selected);
                },
                [typeof(ExpansionBookDescriptionCategory)] = (node, selected) =>
                {
                    ExpansionBookDescriptionCategory ExpansionBookDescriptionCategory = node.Tag as ExpansionBookDescriptionCategory;
                    ShowHandler(new ExpansionBookDescriptionCategoryControl(), typeof(ExpansionBookConfig), ExpansionBookDescriptionCategory, selected);
                },
                [typeof(ExpansionBookLink)] = (node, selected) =>
                {
                    ExpansionBookLink ExpansionBookLink = node.Tag as ExpansionBookLink;
                    ShowHandler(new ExpansionBookLinksControl(), typeof(ExpansionBookConfig), ExpansionBookLink, selected);
                },
                // Chat
                [typeof(ExpansionChatSettings)] = (node,selected) =>
                {
                    ExpansionChatSettings ExpansionChatSettings = node.Tag as ExpansionChatSettings;
                    ShowHandler(new ExpansionChatSettingsControl(), typeof(ExpansionChatConfig), ExpansionChatSettings, selected);
                },
            };
            // ----------------------
            // String handlers
            // ----------------------
            _stringHandlers = new Dictionary<string, Action<TreeNode, List<TreeNode>>>
            {
                //AI
                ["AIPatrolGeneral"] = (node, selected) =>
                {
                    ExpansionAIPatrol ExpansionAIPatrol = node.Parent.Tag as ExpansionAIPatrol;
                    ShowHandler<IUIHandler>(new AIPatrolControl(), typeof(ExpansionAIPatrolConfig), ExpansionAIPatrol, selected);
                },
                //Airdrops
                ["AirdropContainersLoot"] = (node, selected) =>
                {
                    ExpansionLootContainer cfg = node.FindParentOfType<ExpansionLootContainer>();
                    ShowHandler<IUIHandler>(new ExpansionLootControl(), typeof(ExpansionAirdropConfig), cfg.Loot, selected);
                },
                ["AirdropContainersInfected"] = (node, selected) =>
                {
                    ExpansionLootContainer cfg = node.FindParentOfType<ExpansionLootContainer>();
                    ShowHandler<IUIHandler>(new ExpansionInfectedControl(), typeof(ExpansionAirdropConfig), cfg.Infected, selected);
                },
                //Basebuilding
                ["BaseBuildingNoBuldZones"] = (node, selected) =>
                {
                    ExpansionBaseBuildingConfig cfg = node.FindParentOfType<ExpansionBaseBuildingConfig>();
                    ShowHandler<IUIHandler>(new ExpansionBuildNoBuildZonesControl(), typeof(ExpansionBaseBuildingConfig), cfg.Data, selected);
                },
                ["BaseBuildingTerritory"] = (node, selected) =>
                {
                    ExpansionBaseBuildingConfig cfg = node.FindParentOfType<ExpansionBaseBuildingConfig>();
                    ShowHandler<IUIHandler>(new ExpansionBaseBuildingTerritoryControl(), typeof(ExpansionBaseBuildingConfig), cfg.Data, selected);
                },
                ["BaseBuildingCodelocks"] = (node, selected) =>
                {
                    ExpansionBaseBuildingConfig cfg = node.FindParentOfType<ExpansionBaseBuildingConfig>();
                    ShowHandler<IUIHandler>(new BaseBuildingCodelocksControl(), typeof(ExpansionBaseBuildingConfig), cfg.Data, selected);
                },
                ["BaseBuildingCraftDismantle"] = (node, selected) =>
                {
                    ExpansionBaseBuildingConfig cfg = node.FindParentOfType<ExpansionBaseBuildingConfig>();
                    ShowHandler<IUIHandler>(new BaseBuildingCraftDismantleControl(), typeof(ExpansionBaseBuildingConfig), cfg.Data, selected);
                },
                ["BaseBuildingVirtualStorageExcludedContainers"] = (node, selected) =>
                {
                    ExpansionBaseBuildingConfig cfg = node.FindParentOfType<ExpansionBaseBuildingConfig>();
                    ShowHandler<IUIHandler>(new VirtualStorageExcludedContainersControl(), typeof(ExpansionBaseBuildingConfig), cfg.Data, selected);
                },
                //book  
                ["BookGeneral"] = (node, selected) =>
                {
                    ExpansionBookConfig cfg = node.FindParentOfType<ExpansionBookConfig>();
                    ShowHandler<IUIHandler>(new ExpansionBookGeneralControl(), typeof(ExpansionBookConfig), cfg.Data, selected);
                },

            };
        }
        private void InitializeContextMenuHandlers()
        {
            // ----------------------
            // Type handlers
            // ----------------------
            _typeContextMenus = new Dictionary<Type, Action<TreeNode>>
            {
                [typeof(Vec3)] = node =>
                {
                    Vec3 v3 = node.Tag as Vec3;
                    if (node.Parent.Tag.ToString() == "AIPatrolWayPoints")
                    {
                        ExpansionSettingsCM.Items.Clear();
                        ExpansionSettingsCM.Items.Add(removeWaypointToolStripMenuItem);
                        ExpansionSettingsCM.Items.Add(new ToolStripSeparator());
                        ExpansionSettingsCM.Items.Add(moveWaypointUpToolStripMenuItem);
                        ExpansionSettingsCM.Items.Add(moveWaypointDownToolStripMenuItem);
                        ExpansionSettingsCM.Show(Cursor.Position);
                    }
                },
                //AI
                [typeof(ExpansionAIPatrol)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removePatrolToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                // Loadouts
                [typeof(Inventoryattachment)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(RemoveAttachemtItemToolStripMenuItem);
                    ExpansionSettingsCM.Items.Add(new ToolStripSeparator());
                    ExpansionSettingsCM.Items.Add(addNewItemToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(AILoadouts)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(RemoveItemToolStripMenuItem);
                    ExpansionSettingsCM.Items.Add(new ToolStripSeparator());
                    ExpansionSettingsCM.Items.Add(AddNewAttachmentItemToolStripMenuItem);
                    ExpansionSettingsCM.Items.Add(AddNewCargoItemToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(ExpansionLoadoutConfig)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewLoadoutFileToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(Loadbalancingcategorie)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeCategoryToolStripMenuItem);
                    ExpansionSettingsCM.Items.Add(new ToolStripSeparator());
                    ExpansionSettingsCM.Items.Add(addNewLoadBalancingCountsToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(Loadbalancingcategories)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeCountsToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(ExpansionAINoGoArea)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeAINoGoAreaToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                // LootDrop
                [typeof(AILootDrops)] = noew =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(RemoveItemToolStripMenuItem);
                    ExpansionSettingsCM.Items.Add(new ToolStripSeparator());
                    ExpansionSettingsCM.Items.Add(addNewItemToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(ExpansionLootDropConfig)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewLootDropFileToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                // Airdrops
                [typeof(ExpansionLootContainer)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeAirdropContainerToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //basebuilding 
                [typeof(ExpansionBuildNoBuildZone)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(RemoveNoBuildZoneToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //book
                [typeof(ExpansionBookDescriptionCategory)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeDescriptionCategoryToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(ExpansionBookRuleCategory)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewRuleParagraphToolStripMenuItem);
                    ExpansionSettingsCM.Items.Add(new ToolStripSeparator());
                    ExpansionSettingsCM.Items.Add(removeRuleCategoryToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(ExpansionBookRule)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeRuleParagrapghToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(ExpansionBookLink)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeLinkToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(ExpansionBookCraftingCategory)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeCraftingCategoryToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                }

            };
            // ----------------------
            // String handlers
            // ----------------------
            _stringContextMenus = new Dictionary<string, Action<TreeNode>>
            {
                // Loadouts
                ["InventoryAttachments"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(AddNewAttachmentItemToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["InventoryCargo"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(AddNewCargoItemToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["Sets"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewSetToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                // Airdrops
                ["AirdropContainers"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewAirdropContainerToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //AI
                ["AISettingsAdmins"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addAIAdminToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["AISettingsPreventClimb"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addAIPreventClimbToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["AISettingsPlayerFactions"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addAIPlayerFactionToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["AISettingsAdminString"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeAIAdminToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["AISettingsPreventClimbString"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeAIPreventClimbToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["AISettingsPlayerFactionsString"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeAIPlayerFactionToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["AIPatrols"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewPatrolToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["AIPatrolWayPoints"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addWaypointToolStripMenuItem);
                    ExpansionSettingsCM.Items.Add(new ToolStripSeparator());
                    ExpansionSettingsCM.Items.Add(importWaypointsToolStripMenuItem);
                    ExpansionSettingsCM.Items.Add(exportWaypointsToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["AIPatrolUnits"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addUnitToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["AIPatrolsUnit"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeUnitToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["AILoadBlanacing"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewLoadBalancingCategoryToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["NoGoAreas"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addAiNoGoAreaToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["ExcludedRoamingBuildings"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addAIExcludedBuildingsToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["ExcludedRoamingBuilding"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeAIExludedBuildingsToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //BaseBuilding 
                ["BaseBuildingDeployableOutsideATerritory"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewDeployableOutsideTerritoryToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["BaseBuildingDeployableOutsideATerritoryItem"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeDeployableOutsideTerritoryToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["BaseBuildingDeployableInsideAEnemyTerritory"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewDeployableInsideEnemyTerritoryToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["BaseBuildingVirtualStorageExcludedContainers"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewVirtualStorageExcludedContainerToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["BaseBuildingVirtualStorageExcludedContainersItem"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeVirtualStorageExcludedContainerToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["BaseBuildingDeployableInsideAEnemyTerritoryItem"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeDeployableInsideEnemyTerritoryToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["BaseBuildingNoBuldZones"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewNoBuildZoneToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["BaseBuildingNoBuldZoneItems"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addBuildZoneItemToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["BaseBuildingNoBuldZoneItem"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeBuildZoneItemToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //Book
                ["BookDescriptions"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewDescriptionCategoryToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["BookRulesCategories"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewRuleCategoryToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["BookLinks"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewLinkToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["BookCraftingCategories"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewCraftingCategoryToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                }
            };
        }

        private void ExpansionForm_Load(object sender, EventArgs e)
        {
            bool flowControl = InitializeMap();
            if (!flowControl)
            {
                return;
            }

            if (_expansionManager.HasErrors)
            {

                BeginInvoke(new Action(() =>
                {

                    MessageBox.Show("Errors were detected.\nPlease check the console for further info\nIf all errors are corrected please reload Expansion Manager", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }));
                return;
            }
            initializeShowControlHandlers();
            InitializeContextMenuHandlers();
            BuildTreeview();

        }
        private void ExpansionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            AppServices.Unregister<ExpansionManager>();
            _mapControl.ClearMap();
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            savefiles();
        }
        public void savefiles(bool updated = false)
        {
            var savedFiles = _expansionManager.Save();
            if (_currentHandler != null)
                _currentHandler.ApplyChanges();
            Console.WriteLine("Saved files:");
            foreach (var file in savedFiles)
            {
                Console.WriteLine(file);
            }
            if (savedFiles.Count() > 0)
            {
                ShowSavedFilesMessage(savedFiles);
            }
        }
        private void ShowSavedFilesMessage(IEnumerable<string> files)
        {
            // Build a nice multiline string
            var fileListText = string.Join(Environment.NewLine, files);

            // Limit length so the box doesn't get too tall
            if (files.Count() > 15)
            {
                fileListText = string.Join(Environment.NewLine, files.Take(15)) +
                               Environment.NewLine + $"...and {files.Count() - 15} more";
            }

            MessageBox.Show(
                $"The following files were saved successfully:\n\n{fileListText}",
                "Save Complete",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void ExpansionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_expansionManager.needToSave())
            {
                DialogResult dialogResult = MessageBox.Show("You have Unsaved Changes, do you wish to save", "Unsaved Changes found", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    savefiles();
                }
            }
        }
        private bool InitializeMap()
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string imagePath = Path.Combine(appDirectory, "MapAddons", AppServices.GetRequired<ProjectManager>().CurrentProject.MapPath);
            if (!File.Exists(imagePath))
            {
                MessageBox.Show($"Map File does not exist for {AppServices.GetRequired<ProjectManager>().CurrentProject.ProjectName}\nPlease download it from the Projects Manager");
                this.BeginInvoke(new Action(Close)); // defer until safe
                return false;
            }
            Image mapImage = Image.FromFile(imagePath);
            _mapControl.LoadMap(mapImage, AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize);
            MapData = new MapData(Path.Combine(appDirectory, "MapAddons", AppServices.GetRequired<ProjectManager>().CurrentProject.MapPath + ".xyz"), AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize);
            return true;
        }
        private void AddFileToTree<TFile>(TreeNode parentNode, string relativePath, string rootPath, TFile file, Func<TFile, TreeNode> createFileNode, bool expand = false)
        {
            string[] parts = relativePath.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            TreeNode currentNode = parentNode;

            for (int i = 0; i < parts.Length; i++)
            {
                string part = parts[i];

                if (i == parts.Length - 1)
                {
                    TreeNode fileNode = createFileNode(file);
                    currentNode.Nodes.Add(fileNode);
                    if (expand)
                    {
                        // Expand all parent folders
                        fileNode.Expand();
                        ExpansionTV.SelectedNode = fileNode;
                    }
                }
                else
                {
                    TreeNode folderNode = currentNode.Nodes
                        .Cast<TreeNode>()
                        .FirstOrDefault(n => n.Text.Equals(part, StringComparison.OrdinalIgnoreCase));

                    if (folderNode == null)
                    {
                        folderNode = new TreeNode(part)
                        {
                            Tag = Path.Combine(
                                rootPath,
                                string.Join(Path.DirectorySeparatorChar.ToString(), parts.Take(i + 1))
                            )
                        };
                        currentNode.Nodes.Add(folderNode);
                    }

                    currentNode = folderNode;

                    if (expand)
                    {
                        // Expand all parent folders
                        currentNode.Expand();
                    }
                }
            }
        }
        private void BuildTreeview()
        {
            ExpansionTV.Nodes.Clear();
            TreeNode rootNode = new TreeNode("Expansion Mod")
            {
                Tag = "RootNode",
            };
            TreeNode SettingsNode = new TreeNode("Settings")
            {
                Tag = "SettingsNode",
            };
            AddFileToTree(SettingsNode, "", "", _expansionManager.ExpansionAirdropConfig, CreateExpansionAirdropConfigNodes);
            AddFileToTree(SettingsNode, "", "", _expansionManager.ExpansionAIConfig, CreateExpansionAIConfigNodes);
            AddFileToTree(SettingsNode, "", "", _expansionManager.ExpansionBaseBuildingConfig, CreateExpansionBaseBuildingConfigNodes);
            AddFileToTree(SettingsNode, "", "", _expansionManager.ExpansionBookConfig, CreateExpansionBookConfigConfigNodes);
            AddFileToTree(SettingsNode, "", "", _expansionManager.ExpansionChatConfig, CreateExpansionChatConfigConfigNodes);
            AddFileToTree(SettingsNode, "", "", _expansionManager.ExpansionCoreConfig, CreateExpansionCoreConfigConfigNodes);
            AddFileToTree(SettingsNode, "", "", _expansionManager.ExpansionDamageSystemConfig, CreateExpansionDamageSystemConfigConfigNodes);
            AddFileToTree(SettingsNode, "", "", _expansionManager.ExpansionGarageConfig, CreateExpansionGarageConfigConfigNodes);
            AddFileToTree(SettingsNode, "", "", _expansionManager.ExpansionGeneralConfig, CreateExpansionGeneralConfigConfigNodes);
            AddFileToTree(SettingsNode, "", "", _expansionManager.ExpansionHardlineConfig, CreateExpansionHardlineConfigNodes);
            AddFileToTree(SettingsNode, "", "", _expansionManager.ExpansionLogsConfig, CreateExpansionLogsConfigConfigNodes);

            TreeNode AIrootNode = new TreeNode("AI")
            {
                Tag = "AIrootNode"
            };

            AddFileToTree(AIrootNode, "", "", _expansionManager.ExpansionLoadoutConfig, CreateExpansionLoadoutConfigNodes);
            AddFileToTree(AIrootNode, "", "", _expansionManager.ExpansionLootDropConfig, CrateLootDropConfigNodes);
            AddFileToTree(AIrootNode, "", "", _expansionManager.ExpansionAIPatrolConfig, CreateExpansionAIPatrolConfigNodes);
            AddFileToTree(AIrootNode, "", "", _expansionManager.ExpansionAILocationConfig, CreateExpansionAILocationConfigNodes);
            rootNode.Nodes.Add(AIrootNode);

            rootNode.Nodes.Add(SettingsNode);
            ExpansionTV.Nodes.Add(rootNode);
        }


        //airdrop
        private TreeNode CreateExpansionAirdropConfigNodes(ExpansionAirdropConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateairdropNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private void CreateairdropNodes(ExpansionAirdropConfig ef, TreeNode EconomyRootNode)
        {
            EconomyRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = ef.Data
            });
            TreeNode acnodes = new TreeNode("Containers")
            {
                Tag = "AirdropContainers"
            };
            foreach (ExpansionLootContainer alc in ef.Data.Containers)
            {
                TreeNode alcnodes = new TreeNode(alc.Container)
                {
                    Tag = alc
                };
                TreeNode alcinodes = new TreeNode("Infected")
                {
                    Tag = "AirdropContainersInfected"
                };
                alcnodes.Nodes.Add(alcinodes);

                TreeNode alclnodes = new TreeNode("Loot")
                {
                    Tag = "AirdropContainersLoot"
                };
                alcnodes.Nodes.Add(alclnodes);

                acnodes.Nodes.Add(alcnodes);

            }
            EconomyRootNode.Nodes.Add(acnodes);
        }
        //AI
        private TreeNode CreateExpansionLoadoutConfigNodes(ExpansionLoadoutConfig ef)
        {
            TreeNode AILoadoutConfigRootNode = new TreeNode("Loadouts")
            {
                Tag = ef
            };
            foreach (AILoadouts AILoadouts in ef.AllData)
            {
                AILoadoutConfigRootNode.Nodes.Add(SetupLoadoutTreeView(AILoadouts));
            }

            return AILoadoutConfigRootNode;
        }
        private TreeNode SetupLoadoutTreeView(AILoadouts load)
        {
            string display = !string.IsNullOrWhiteSpace(load.FileName) ? load.FileName : Path.GetFileNameWithoutExtension(load.FileName ?? string.Empty);
            TreeNode root = new TreeNode(display) { Tag = load };

            TreeNode invAttachments = new TreeNode("inventoryAttachments") { Tag = "InventoryAttachments" };
            foreach (Inventoryattachment ia in load.InventoryAttachments ?? new BindingList<Inventoryattachment>())
                invAttachments.Nodes.Add(BuildInventoryAttachmentNode(ia));
            root.Nodes.Add(invAttachments);

            TreeNode invCargo = new TreeNode("InventoryCargo") { Tag = "InventoryCargo" };
            foreach (AILoadouts cargo in load.InventoryCargo ?? new BindingList<AILoadouts>())
                invCargo.Nodes.Add(BuildAILoadoutsNode(cargo));
            root.Nodes.Add(invCargo);

            TreeNode sets = new TreeNode("Sets") { Tag = "Sets" };
            foreach (AILoadouts set in load.Sets ?? new BindingList<AILoadouts>())
                sets.Nodes.Add(BuildAILoadoutsNode(set));
            root.Nodes.Add(sets);

            return root;
        }
        private TreeNode BuildInventoryAttachmentNode(Inventoryattachment ia)
        {
            string slotname = string.IsNullOrWhiteSpace(ia.SlotName) ? "Default Slot" : ia.SlotName;
            TreeNode tn = new TreeNode(slotname) { Tag = ia };
            foreach (AILoadouts child in ia.Items ?? new BindingList<AILoadouts>())
                tn.Nodes.Add(BuildAILoadoutsNode(child));
            return tn;
        }
        private TreeNode BuildAILoadoutsNode(AILoadouts a)
        {
            string label = string.IsNullOrWhiteSpace(a.ClassName) ? "Set" : a.ClassName;
            TreeNode tn = new TreeNode(label) { Tag = a };

            foreach (Inventoryattachment ia in a.InventoryAttachments ?? new BindingList<Inventoryattachment>())
                tn.Nodes.Add(BuildInventoryAttachmentNode(ia));

            TreeNode cargoNode = BuildCargoNode(a.InventoryCargo);
            if (cargoNode != null) tn.Nodes.Add(cargoNode);

            foreach (AILoadouts set in a.Sets ?? new BindingList<AILoadouts>())
                tn.Nodes.Add(BuildAILoadoutsNode(set));

            return tn;
        }
        private TreeNode BuildCargoNode(BindingList<AILoadouts> cargoList)
        {
            if (cargoList == null || cargoList.Count == 0) return null;
            TreeNode tn = new TreeNode("InventoryCargo") { Tag = "InventoryCargo" };
            foreach (AILoadouts c in cargoList)
                tn.Nodes.Add(BuildAILoadoutsNode(c));
            return tn;
        }
        private TreeNode CrateLootDropConfigNodes(ExpansionLootDropConfig ef)
        {
            TreeNode AILootdropConfigRootNode = new TreeNode("LootDrops")
            {
                Tag = ef
            };
            foreach (AILootDrops AILootdrops in ef.AllData)
            {
                TreeNode fileNode = new TreeNode(AILootdrops.FileName) { Tag = AILootdrops };
                foreach (AILoadouts entry in AILootdrops.LootdropList)
                {
                    fileNode.Nodes.Add(BuildAILoadoutsNode(entry));
                }
                AILootdropConfigRootNode.Nodes.Add(fileNode);
            }

            return AILootdropConfigRootNode;
        }
        private TreeNode CreateExpansionAIConfigNodes(ExpansionAIConfig ef)
        {
            TreeNode AIRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            AIRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = ef.Data
            });
            TreeNode AISettingsAdminsNodes = new TreeNode("Admins")
            {
                Tag = "AISettingsAdmins"
            };
            foreach (string s in ef.Data.Admins)
            {
                AISettingsAdminsNodes.Nodes.Add(new TreeNode(s)
                {
                    Tag = "AISettingsAdminString"
                });
            }
            AIRootNode.Nodes.Add(AISettingsAdminsNodes);
            TreeNode AISettingsPreventClimbNodes = new TreeNode("PreventClimb")
            {
                Tag = "AISettingsPreventClimb"
            };
            foreach (string s in ef.Data.PreventClimb)
            {
                AISettingsPreventClimbNodes.Nodes.Add(new TreeNode(s)
                {
                    Tag = "AISettingsPreventClimbString"
                });
            }
            AIRootNode.Nodes.Add(AISettingsPreventClimbNodes);
            TreeNode AISettingsPlayerFactionsNodes = new TreeNode("Player Factions")
            {
                Tag = "AISettingsPlayerFactions"
            };
            foreach (string s in ef.Data.PlayerFactions)
            {
                AISettingsPlayerFactionsNodes.Nodes.Add(new TreeNode(s)
                {
                    Tag = "AISettingsPlayerFactionsString"
                });
            }
            AIRootNode.Nodes.Add(AISettingsPlayerFactionsNodes);
            TreeNode LightingConfigMinNightVisibilityMetersNodes = new TreeNode("Lighting Config Min Night Visibility Meters")
            {
                Tag = "LightingConfigMinNightVisibilityMeters"
            };
            foreach (AILightEntries AILightEntries in ef.Data.AILightEntries)
            {
                LightingConfigMinNightVisibilityMetersNodes.Nodes.Add(new TreeNode($"Lighting Config {AILightEntries.Key} : Visibility {AILightEntries.Value}")
                {
                    Tag = AILightEntries
                });
            }
            AIRootNode.Nodes.Add(LightingConfigMinNightVisibilityMetersNodes);
            return AIRootNode;
        }
        private TreeNode CreateExpansionAIPatrolConfigNodes(ExpansionAIPatrolConfig ef)
        {
            TreeNode AIPatrolRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            AIPatrolRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = ef.Data
            });
            TreeNode AIAPatrolsNode = new TreeNode("Patrols")
            {
                Tag = "AIPatrols"
            };
            foreach (ExpansionAIPatrol pat in ef.Data.Patrols)
            {
                TreeNode PatrolRoot = new TreeNode(pat.Name)
                {
                    Tag = pat
                };
                CreatePatrolNodes(pat, PatrolRoot);
                AIAPatrolsNode.Nodes.Add(PatrolRoot);
            }
            AIPatrolRootNode.Nodes.Add(AIAPatrolsNode);
            TreeNode LoadBalancingNode = new TreeNode("Load Balancing")
            {
                Tag = "AILoadBlanacing"
            };
            foreach (Loadbalancingcategorie lbc in ef.Data._LoadBalancingCategories)
            {
                TreeNode LoadbalancingcategorieRoot = new TreeNode($"Category Name : - {lbc.name}")
                {
                    Tag = lbc
                };
                int i = 0;
                foreach (Loadbalancingcategories lbcat in lbc.Categorieslist)
                {
                    LoadbalancingcategorieRoot.Nodes.Add(new TreeNode($"Load Balancing : {i.ToString()}")
                    {
                        Tag = lbcat
                    });
                    i++;
                }
                LoadBalancingNode.Nodes.Add(LoadbalancingcategorieRoot);
            }
            AIPatrolRootNode.Nodes.Add(LoadBalancingNode);
            return AIPatrolRootNode;
        }
        private void CreatePatrolNodes(ExpansionAIPatrol pat, TreeNode Root)
        {
            Root.Nodes.Add(new TreeNode("General")
            {
                Tag = "AIPatrolGeneral"
            });
            TreeNode WaypointsNode = new TreeNode("WayPoints")
            {
                Tag = "AIPatrolWayPoints"
            };
            foreach (Vec3 v3 in pat._waypoints)
            {
                WaypointsNode.Nodes.Add(new TreeNode(v3.GetString())
                {
                    Tag = v3
                });
            }
            Root.Nodes.Add(WaypointsNode);
            TreeNode UnitsNode = new TreeNode("Units")
            {
                Tag = "AIPatrolUnits"
            };
            foreach (string s in pat.Units)
            {
                UnitsNode.Nodes.Add(new TreeNode(s)
                {
                    Tag = "AIPatrolsUnit"
                });
            }
            Root.Nodes.Add(UnitsNode);
        }
        private TreeNode CreateExpansionAILocationConfigNodes(ExpansionAILocationConfig ef)
        {
            TreeNode AILocationlRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            TreeNode RoamingLocationsNodes = new TreeNode("RoamingLocations")
            {
                Tag = "RoamingLocations"
            };
            foreach (ExpansionAIRoamingLocation ExpansionAIRoamingLocation in ef.Data.RoamingLocations)
            {
                RoamingLocationsNodes.Nodes.Add(new TreeNode(ExpansionAIRoamingLocation.Name)
                {
                    Tag = ExpansionAIRoamingLocation
                });
            }
            AILocationlRootNode.Nodes.Add(RoamingLocationsNodes);
            TreeNode AExcludedRoamingBuildingsNode = new TreeNode("Excluded Roaming Buildings")
            {
                Tag = "ExcludedRoamingBuildings"
            };
            foreach (string s in ef.Data.ExcludedRoamingBuildings)
            {
                AExcludedRoamingBuildingsNode.Nodes.Add(new TreeNode(s)
                {
                    Tag = "ExcludedRoamingBuilding"
                });
            }
            AILocationlRootNode.Nodes.Add(AExcludedRoamingBuildingsNode);
            TreeNode NoGoAreasNode = new TreeNode("No Go Areas")
            {
                Tag = "NoGoAreas"
            };
            foreach (ExpansionAINoGoArea lbc in ef.Data.NoGoAreas)
            {
                NoGoAreasNode.Nodes.Add(new TreeNode($"{lbc.Name}")
                {
                    Tag = lbc
                });
            }
            AILocationlRootNode.Nodes.Add(NoGoAreasNode);
            return AILocationlRootNode;
        }

        //Basebuilding
        private TreeNode CreateExpansionBaseBuildingConfigNodes(ExpansionBaseBuildingConfig ef)
        {
            TreeNode BaseBuildingRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateBasebuildingNodes(ef, BaseBuildingRootNode);
            return BaseBuildingRootNode;
        }
        private void CreateBasebuildingNodes(ExpansionBaseBuildingConfig ef, TreeNode EconomyRootNode)
        {
            TreeNode TerritoryNodes = new TreeNode("Territory")
            {
                Tag = "BaseBuildingTerritory"
            };
            TreeNode DeployableOutsideATerritoryNodes = new TreeNode("Deployable Outside A Territory")
            {
                Tag = "BaseBuildingDeployableOutsideATerritory"
            };
            foreach (string s in ef.Data.DeployableOutsideATerritory)
            {
                DeployableOutsideATerritoryNodes.Nodes.Add(new TreeNode(s)
                {
                    Tag = "BaseBuildingDeployableOutsideATerritoryItem"
                });
            }
            TerritoryNodes.Nodes.Add(DeployableOutsideATerritoryNodes);
            TreeNode DeployableInsideAEnemyTerritoryNodes = new TreeNode("Deployable Inside An Enemy Territory")
            {
                Tag = "BaseBuildingDeployableInsideAEnemyTerritory"
            };
            foreach (string s in ef.Data.DeployableInsideAEnemyTerritory)
            {
                DeployableInsideAEnemyTerritoryNodes.Nodes.Add(new TreeNode(s)
                {
                    Tag = "BaseBuildingDeployableInsideAEnemyTerritoryItem"
                });
            }
            TerritoryNodes.Nodes.Add(DeployableInsideAEnemyTerritoryNodes);
            EconomyRootNode.Nodes.Add(TerritoryNodes);
            EconomyRootNode.Nodes.Add(new TreeNode("Codelocks")
            {
                Tag = "BaseBuildingCodelocks"
            });
            EconomyRootNode.Nodes.Add(new TreeNode("Craft/Dismantle")
            {
                Tag = "BaseBuildingCraftDismantle"
            });
            TreeNode VirtualStorageExcludedContainersnodes = new TreeNode("Virtual Storage Excluded Containers")
            {
                Tag = "BaseBuildingVirtualStorageExcludedContainers"
            };
            foreach (string s in ef.Data.VirtualStorageExcludedContainers)
            {
                VirtualStorageExcludedContainersnodes.Nodes.Add(new TreeNode(s)
                {
                    Tag = "BaseBuildingVirtualStorageExcludedContainersItem"
                });
            }
            EconomyRootNode.Nodes.Add(VirtualStorageExcludedContainersnodes);
            EconomyRootNode.Nodes.Add(CreateBuildZoneNodes(ef));
        }
        private static TreeNode CreateBuildZoneNodes(ExpansionBaseBuildingConfig ef)
        {
            TreeNode nbznodes = new TreeNode("No Build Zones")
            {
                Tag = "BaseBuildingNoBuldZones"
            };
            foreach (ExpansionBuildNoBuildZone nbz in ef.Data.Zones)
            {
                TreeNode nbznode = new TreeNode(nbz.Name)
                {
                    Tag = nbz
                };
                TreeNode nbzinodes = new TreeNode("Items")
                {
                    Tag = "BaseBuildingNoBuldZoneItems"
                };
                foreach (string s in nbz.Items)
                {
                    nbzinodes.Nodes.Add(new TreeNode($"Item: {s}")
                    {
                        Tag = "BaseBuildingNoBuldZoneItem"
                    });
                }
                nbznode.Nodes.Add(nbzinodes);
                nbznodes.Nodes.Add(nbznode);
            }

            return nbznodes;
        }
        //Book
        private TreeNode CreateExpansionBookConfigConfigNodes(ExpansionBookConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateBookNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private static void CreateBookNodes(ExpansionBookConfig ef, TreeNode EconomyRootNode)
        {
            EconomyRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = "BookGeneral"
            });
            TreeNode dnodes = new TreeNode("Descriptions")
            {
                Tag = "BookDescriptions"
            };
            foreach (ExpansionBookDescriptionCategory bdc in ef.Data.Descriptions)
            {
                TreeNode bdcnodes = new TreeNode($"Category Name: {bdc.CategoryName}")
                {
                    Tag = bdc
                };
                dnodes.Nodes.Add(bdcnodes);
            }
            EconomyRootNode.Nodes.Add(dnodes);
            TreeNode rnodes = new TreeNode("Rules Categories")
            {
                Tag = "BookRulesCategories"
            };
            foreach (ExpansionBookRuleCategory rc in ef.Data.RuleCategories)
            {
                TreeNode rcnodes = new TreeNode($"Category Name: {rc.CategoryName}")
                {
                    Tag = rc
                };
                foreach (ExpansionBookRule br in rc.Rules)
                {
                    rcnodes.Nodes.Add(new TreeNode($"Rule Paragraph: {br.RuleParagraph}")
                    {
                        Tag = br
                    });
                }
                rnodes.Nodes.Add(rcnodes);
            }
            EconomyRootNode.Nodes.Add(rnodes);
            TreeNode lnodes = new TreeNode("Links")
            {
                Tag = "BookLinks"
            };
            foreach (ExpansionBookLink bl in ef.Data.Links)
            {
                lnodes.Nodes.Add(new TreeNode($"Links: {bl.Name}")
                {
                    Tag = bl
                });
            }
            EconomyRootNode.Nodes.Add(lnodes);
            TreeNode cnodes = new TreeNode("Crafting Categories")
            {
                Tag = "BookCraftingCategories"
            };
            foreach (ExpansionBookCraftingCategory cc in ef.Data.CraftingCategories)
            {
                cnodes.Nodes.Add(new TreeNode($"Category: {cc.CategoryName}")
                {
                    Tag = cc
                });
            }
            EconomyRootNode.Nodes.Add(cnodes);
        }
        //Chat
        private TreeNode CreateExpansionChatConfigConfigNodes(ExpansionChatConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateChatNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private static void CreateChatNodes(ExpansionChatConfig ef, TreeNode EconomyRootNode)
        {
            EconomyRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = ef.Data
            });
            EconomyRootNode.Nodes.Add(new TreeNode("Colours")
            {
                Tag = ef.Data.ChatColors
            });
        }
        //Core
        private TreeNode CreateExpansionCoreConfigConfigNodes(ExpansionCoreConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateCoreNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private static void CreateCoreNodes(ExpansionCoreConfig ef, TreeNode EconomyRootNode)
        {
            EconomyRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = ef.Data
            });
        }
        //DamageSystem
        private TreeNode CreateExpansionDamageSystemConfigConfigNodes(ExpansionDamageSystemConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateDamageSystemNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private static void CreateDamageSystemNodes(ExpansionDamageSystemConfig ef, TreeNode EconomyRootNode)
        {
            EconomyRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = ef.Data
            });
            TreeNode ExplosionTargetsNodes = new TreeNode("ExplosionTargets")
            {
                Tag = "ExplosionTargets"
            };
            foreach (string s in ef.Data.ExplosionTargets)
            {
                ExplosionTargetsNodes.Nodes.Add(new TreeNode(s)
                {
                    Tag = "ExplosivewTarget"
                });
            }
            EconomyRootNode.Nodes.Add(ExplosionTargetsNodes);
            TreeNode ExplosiveProjectilesNodes = new TreeNode("ExplosiveProjectiles")
            {
                Tag = "ExplosiveProjectiles"
            };
            foreach (ExplosiveProjectiles ep in ef.Data._ExplosiveProjectiles)
            {
                ExplosiveProjectilesNodes.Nodes.Add(new TreeNode($"{ep.explosion}:{ep.ammo}")
                {
                    Tag = "ExplosivewTarget"
                });
            }
            EconomyRootNode.Nodes.Add(ExplosiveProjectilesNodes);
        }
        //Garage
        private TreeNode CreateExpansionGarageConfigConfigNodes(ExpansionGarageConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateGarageNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private static void CreateGarageNodes(ExpansionGarageConfig ef, TreeNode EconomyRootNode)
        {
            EconomyRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = ef.Data
            });
            TreeNode WhitelistNode = new TreeNode("EntityWhitelist")
            {
                Tag = "EntityWhitelist"
            };
            foreach (string s in ef.Data.EntityWhitelist)
            {
                WhitelistNode.Nodes.Add(new TreeNode(s)
                {
                    Tag = "EntityWhitelistItem"
                });
            }
            EconomyRootNode.Nodes.Add(WhitelistNode);
        }
        //General
        private TreeNode CreateExpansionGeneralConfigConfigNodes(ExpansionGeneralConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateGeneralNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private static void CreateGeneralNodes(ExpansionGeneralConfig ef, TreeNode EconomyRootNode)
        {
            EconomyRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = "GenralGernal"
            });
            EconomyRootNode.Nodes.Add(new TreeNode("Screen")
            {
                Tag = "GenralScreen"
            });
            EconomyRootNode.Nodes.Add(new TreeNode("Grave Cross")
            {
                Tag = "GenralGraveCross"
            });
            EconomyRootNode.Nodes.Add(new TreeNode("Lights")
            {
                Tag = "GenralLights"
            });
            EconomyRootNode.Nodes.Add(new TreeNode("HUD")
            {
                Tag = "GenralHuds"
            });
            EconomyRootNode.Nodes.Add(new TreeNode("Mapping")
            {
                Tag = ef.Data.Mapping
            });
        }
        //Hardline
        private TreeNode CreateExpansionHardlineConfigNodes(ExpansionHardlineConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateHardlineNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private static void CreateHardlineNodes(ExpansionHardlineConfig ef, TreeNode EconomyRootNode)
        {
            EconomyRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = ef.Data
            });
            EconomyRootNode.Nodes.Add(new TreeNode("Reputation")
            {
                Tag = "Reputation"
            });
            EconomyRootNode.Nodes.Add(new TreeNode("Requirements and Item Rarity")
            {
                Tag = "RequirementsandItemRarity"
            });
        }
        //Logs
        private TreeNode CreateExpansionLogsConfigConfigNodes(ExpansionLogsConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateLogsNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private static void CreateLogsNodes(ExpansionLogsConfig ef, TreeNode EconomyRootNode)
        {
            EconomyRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = ef.Data
            });
        }


        void ShowHandler<THandler>(THandler handler, Type parent, object primaryData, List<TreeNode> selectedNodes)
        where THandler : IUIHandler
        {
            if (handler == null)
            {
                if (_currentHandler != null)
                {
                    var oldControl = _currentHandler.GetControl();
                    splitContainer1.Panel2.Controls.Remove(oldControl);
                    oldControl.Dispose();
                    (_currentHandler as IDisposable)?.Dispose();
                    _currentHandler = null;
                }
                return;
            }

            // If same type → just reload
            Type handlerType = handler.GetType();
            if (_currentHandler != null && _currentHandler.GetType() == handlerType)
            {
                _currentHandler.LoadFromData(parent, primaryData, selectedNodes);
                return;
            }

            // Dispose old handler if different
            if (_currentHandler != null)
            {
                var oldControl = _currentHandler.GetControl();
                splitContainer1.Panel2.Controls.Remove(oldControl);
                oldControl.Dispose();
                (_currentHandler as IDisposable)?.Dispose();
            }

            // Set new handler
            _currentHandler = handler;
            handler.LoadFromData(parent, primaryData, selectedNodes);

            var ctrl = handler.GetControl();
            ctrl.Location = new Point(2, 2);
            splitContainer1.Panel2.Controls.Add(ctrl);
            ctrl.BringToFront();
            ctrl.Visible = true;

            if (parent == typeof(economyCoreConfig))
            {
                ctrl.Dock = DockStyle.Fill;
            }
        }
        private void ResetMapControl()
        {
            // Hide and clear map until a handler explicitly sets it up
            _mapControl.Visible = false;
            _mapControl.ClearDrawables();


            // Remove all event subscriptions to avoid duplicates
            _mapControl.MapsingleClicked -= MapControl_BuildZoneSingleclicked;
            _mapControl.MapDoubleClicked -= MapControl_BuildZoneDoubleclicked;
            _mapControl.MapsingleClicked -= MapControl_AIRomaingLocationSingleclicked;
            _mapControl.MapsingleClicked -= MapControl_AIPatrolSingleclicked;
            _mapControl.MapDoubleClicked -= MapControl_AIPatrolDoubleclicked;
            _mapControl.MapsingleClicked -= MapControl_AILocationNoGoAreasSingleclicked;
            _mapControl.MapDoubleClicked -= MapControl_AILocationNoGoAreasDoubleclicked;

            // Reset "selected" state objects
            _selectedNoBuildZonePos = null;
            _selectedAIRoamingLocations = null;
            _selectedAIPatrol = null;
            _selectedAINOGoArea = null;
        }
        private void ExpansionTV_AfterSelect(object sender, TreeViewEventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                ResetMapControl();

                currentTreeNode = e.Node;
                var selectedNodes = ExpansionTV.SelectedNodes.Cast<TreeNode>().ToList();

                if (e.Node.Tag == null)
                {
                    ShowHandler<IUIHandler>(null, null, null, null);
                    return;
                }
                if (e.Node.Tag is string key)
                {
                    if (_stringHandlers.TryGetValue(key, out var stringHandler))
                    {
                        stringHandler(e.Node, selectedNodes);
                        return;
                    }
                }


                var nodeType = e.Node.Tag.GetType();
                if (_typeHandlers.TryGetValue(nodeType, out var typeHandler))
                {
                    typeHandler(e.Node, selectedNodes);
                    return;
                }

                ShowHandler<IUIHandler>(null, null, null, selectedNodes);
            }));
        }
        private void ExpansionTV_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ExpansionTV.SelectedNode = e.Node;
            currentTreeNode = e.Node;

            if (e.Button != MouseButtons.Right) return;

            // Try type-based
            if (e.Node.Tag != null && _typeContextMenus.TryGetValue(e.Node.Tag.GetType(), out var typeHandler))
            {
                typeHandler.Invoke(e.Node);
            }
            // Try string-based
            else if (e.Node.Tag is string s && _stringContextMenus.TryGetValue(s, out var stringHandler))
            {
                stringHandler.Invoke(e.Node);
            }
        }

        #region mapstuff
        /// <summary>
        /// MapViewer Draw Mothods
        /// </summary>
        private ExpansionBuildNoBuildZone _selectedNoBuildZonePos;
        private ExpansionAIRoamingLocation _selectedAIRoamingLocations;
        private ExpansionAIPatrol _selectedAIPatrol;
        private ExpansionAINoGoArea _selectedAINOGoArea;

        // Generic map reset + show
        private void SetupMap(Action config)
        {
            _mapControl.Visible = true;
            if (!_mapControl.HasInitialPainted)
            {
                _mapControl.Invalidate();
                _mapControl.Update();
            }
            config?.Invoke();
        }
        // Specific helpers for different map cases
        private void SetupBaseBuildingNoBuildZone(ExpansionBuildNoBuildZone pos, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedNoBuildZonePos = pos;
                _mapControl.MapDoubleClicked += MapControl_BuildZoneDoubleclicked;
                _mapControl.MapsingleClicked += MapControl_BuildZoneSingleclicked;

                var ExpansionBaseBuildingConfig = node.Parent?.Parent?.Tag as ExpansionBaseBuildingConfig;
                if (ExpansionBaseBuildingConfig != null)
                    DrawbasebuildingNoBuildZones(ExpansionBaseBuildingConfig);
            });
        }
        private void SetupAILocationRoamingLocations(ExpansionAIRoamingLocation pos, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedAIRoamingLocations = pos;
                _mapControl.MapsingleClicked += MapControl_AIRomaingLocationSingleclicked;

                var ExpansionAILocationConfig = node.Parent?.Parent?.Tag as ExpansionAILocationConfig;
                if (ExpansionAILocationConfig != null)
                    DrawbaseAIRoamingLocation(ExpansionAILocationConfig);
            });
        }
        private void SetupAIPatrols(ExpansionAIPatrol patrol, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedAIPatrol = patrol;
                _mapControl.MapDoubleClicked += MapControl_AIPatrolDoubleclicked;
                _mapControl.MapsingleClicked += MapControl_AIPatrolSingleclicked;

                var ExpansionAIPatrolConfig = node.Parent?.Parent?.Parent?.Parent?.Tag as ExpansionAIPatrolConfig;
                if (ExpansionAIPatrolConfig != null)
                    DrawbaseAIPatrols(ExpansionAIPatrolConfig);
            });
        }
        private void SetupAILocationNoGoAreas(ExpansionAINoGoArea pos, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedAINOGoArea = pos;
                _mapControl.MapsingleClicked += MapControl_AILocationNoGoAreasSingleclicked;
                _mapControl.MapDoubleClicked += MapControl_AILocationNoGoAreasDoubleclicked;

                var ExpansionAILocationConfig = node.Parent?.Parent?.Tag as ExpansionAILocationConfig;
                if (ExpansionAILocationConfig != null)
                    DrawbaseAILocationNoGoAreas(ExpansionAILocationConfig);
            });
        }

        //Draw Methods
        private void DrawbasebuildingNoBuildZones(ExpansionBaseBuildingConfig ExpansionBaseBuildingConfig)
        {
            foreach (ExpansionBuildNoBuildZone pos in ExpansionBaseBuildingConfig.Data.Zones)
            {
                var marker = new MarkerDrawable(new PointF((float)pos.Center[0], (float)pos.Center[2]), _mapControl.MapSize)
                {
                    Color = Color.Red,
                    Radius = pos.Radius,
                    Scaleradius = true,
                    Shade = true
                };
                if (_selectedNoBuildZonePos == pos)
                {
                    marker.Color = Color.LimeGreen;
                }
                _mapControl.RegisterDrawable(marker);
            }
        }
        private void DrawbaseAIRoamingLocation(ExpansionAILocationConfig ExpansionAILocationConfig)
        {
            foreach (ExpansionAIRoamingLocation pos in ExpansionAILocationConfig.Data.RoamingLocations)
            {
                var marker = new MarkerDrawable(new PointF((float)pos.Position[0], (float)pos.Position[2]), _mapControl.MapSize)
                {
                    Color = Color.Red,
                    Radius = (float)pos.Radius,
                    Scaleradius = true,
                    Shade = true
                };
                if (_selectedAIRoamingLocations == pos)
                {
                    marker.Color = Color.LimeGreen;
                }
                _mapControl.RegisterDrawable(marker);
            }
        }
        private void DrawbaseAIPatrols(ExpansionAIPatrolConfig ExpansionAIPatrolConfig)
        {
            _mapControl.RegisterDrawable(new AiPAtrolLegendDrawable(_mapControl.Size));
            foreach (ExpansionAIPatrol patrol in ExpansionAIPatrolConfig.Data.Patrols)
            {
                PatrolBehaviour behaviour = PatrolBehaviour.HALT;
                if (!string.IsNullOrEmpty(patrol.Behaviour))
                {
                    Enum.TryParse(patrol.Behaviour.Replace("-", "_"), true, out behaviour);
                }

                for (int i = 0; i < patrol._waypoints.Count; i++)
                {
                    Vec3 waypoints = patrol._waypoints[i];

                    // Determine next waypoint index
                    bool isLast = i == patrol._waypoints.Count - 1;
                    Vec3 nextWaypoint;

                    if ((behaviour == PatrolBehaviour.ALTERNATE || behaviour == PatrolBehaviour.HALT_OR_ALTERNATE || behaviour == PatrolBehaviour.ONCE) && isLast)
                    {
                        // Don't connect last to first for ALTERNATE
                        nextWaypoint = waypoints;
                    }
                    else
                    {
                        nextWaypoint = patrol._waypoints[(i + 1) % patrol._waypoints.Count];
                    }

                    var marker = new AIPatrolDrawable(
                        new PointF(waypoints.X, waypoints.Z),
                        new PointF(nextWaypoint.X, nextWaypoint.Z),
                        _mapControl.MapSize,
                        behaviour)
                    {
                        Color = Color.Red,
                        WriteString = true
                    };

                    if (_selectedAIPatrol == patrol)
                        marker.Color = Color.LimeGreen;

                    Vec3 v3 = currentTreeNode.Tag as Vec3;
                    if (v3 == waypoints)
                        marker.Color = Color.Yellow;

                    marker.text = (i == 0 ? patrol.Name + "\n" : "") + (i + 1).ToString();

                    _mapControl.RegisterDrawable(marker);
                }
            }
        }
        private void DrawbaseAILocationNoGoAreas(ExpansionAILocationConfig ExpansionAILocationConfig)
        {
            foreach (ExpansionAINoGoArea ExpansionAINoGoArea in ExpansionAILocationConfig.Data.NoGoAreas)
            {
                var marker = new MarkerDrawable(new PointF(ExpansionAINoGoArea._Position.X, ExpansionAINoGoArea._Position.Z), _mapControl.MapSize)
                {
                    Color = Color.Red,
                    Radius = (float)ExpansionAINoGoArea.Radius,
                    Scaleradius = true,
                    Shade = true
                };
                if (_selectedAINOGoArea == ExpansionAINoGoArea)
                {
                    marker.Color = Color.LimeGreen;
                }
                _mapControl.RegisterDrawable(marker);
            }
        }

        //map click methods
        private void MapControl_BuildZoneSingleclicked(object sender, MapClickEventArgs e)
        {
            if (currentTreeNode?.Parent == null)
                return;

            TreeNode parentNode = currentTreeNode.Parent;

            ExpansionBuildNoBuildZone closestPos = null;
            double closestDistance = double.MaxValue;

            PointF clickScreen = _mapControl.MapToScreen(e.MapCoordinates);

            // Loop through all child nodes of the parent
            foreach (TreeNode child in parentNode.Nodes)
            {
                if (child.Tag is ExpansionBuildNoBuildZone pos)
                {
                    // Node position in screen space
                    PointF posScreen = _mapControl.MapToScreen(new PointF((float)pos.Center[0], (float)pos.Center[2]));

                    double dx = clickScreen.X - posScreen.X;
                    double dy = clickScreen.Y - posScreen.Y;
                    double distance = Math.Sqrt(dx * dx + dy * dy);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestPos = pos;
                    }
                }
            }

            // Optional: choose only if within some "click radius"
            if (closestPos != null && closestDistance <= 25) // 10 units tolerance
            {
                // Select that tree node in the TreeView
                foreach (TreeNode child in parentNode.Nodes)
                {
                    if (child.Tag == closestPos)
                    {
                        ExpansionTV.SelectedNode = child;
                        break;
                    }
                }

                //MessageBox.Show($"Selected closest node at X:{closestPos.x:0.##}, Z:{closestPos.z:0.##}");
            }
        }
        private void MapControl_BuildZoneDoubleclicked(object sender, MapClickEventArgs e)
        {
            if (_selectedNoBuildZonePos == null) return;

            _selectedNoBuildZonePos.Center[0] = (float)e.MapCoordinates.X;
            _selectedNoBuildZonePos.Center[2] = (float)e.MapCoordinates.Y;
            if (MapData.FileExists)
            {
                _selectedNoBuildZonePos.Center[1] = (MapData.gethieght(_selectedNoBuildZonePos.Center[0], _selectedNoBuildZonePos.Center[2]));
            }
            _expansionManager.ExpansionBaseBuildingConfig.isDirty = true;

            _mapControl.ClearDrawables();

            ExpansionBaseBuildingConfig ExpansionBaseBuildingConfig = currentTreeNode.Parent.Parent.Tag as ExpansionBaseBuildingConfig;
            ShowHandler<IUIHandler>(new ExpansionBuildNoBuildZoneControl(), typeof(ExpansionBaseBuildingConfig), _selectedNoBuildZonePos, new List<TreeNode>() { currentTreeNode });
            DrawbasebuildingNoBuildZones(ExpansionBaseBuildingConfig);

        }
        private void MapControl_AIRomaingLocationSingleclicked(object sender, MapClickEventArgs e)
        {
            if (currentTreeNode?.Parent == null)
                return;

            TreeNode parentNode = currentTreeNode.Parent;

            ExpansionAIRoamingLocation closestPos = null;
            double closestDistance = double.MaxValue;

            PointF clickScreen = _mapControl.MapToScreen(e.MapCoordinates);

            // Loop through all child nodes of the parent
            foreach (TreeNode child in parentNode.Nodes)
            {
                if (child.Tag is ExpansionAIRoamingLocation pos)
                {
                    // Node position in screen space
                    PointF posScreen = _mapControl.MapToScreen(new PointF((float)pos.Position[0], (float)pos.Position[2]));

                    double dx = clickScreen.X - posScreen.X;
                    double dy = clickScreen.Y - posScreen.Y;
                    double distance = Math.Sqrt(dx * dx + dy * dy);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestPos = pos;
                    }
                }
            }

            // Optional: choose only if within some "click radius"
            if (closestPos != null && closestDistance <= 25) // 10 units tolerance
            {
                // Select that tree node in the TreeView
                foreach (TreeNode child in parentNode.Nodes)
                {
                    if (child.Tag == closestPos)
                    {
                        ExpansionTV.SelectedNode = child;
                        break;
                    }
                }

                //MessageBox.Show($"Selected closest node at X:{closestPos.x:0.##}, Z:{closestPos.z:0.##}");
            }
        }
        private void MapControl_AIPatrolSingleclicked(object sender, MapClickEventArgs e)
        {
            if (currentTreeNode?.Parent == null)
                return;

            TreeNode parentNode = currentTreeNode.Parent.Parent.Parent;

            Vec3 closestPos = null;
            double closestDistance = double.MaxValue;

            PointF clickScreen = _mapControl.MapToScreen(e.MapCoordinates);

            // Loop through all child nodes of the parent
            foreach (TreeNode child in parentNode.Nodes)
            {
                foreach (TreeNode child2 in child.Nodes[1].Nodes)
                {
                    if (child2.Tag is Vec3 pos)
                    {
                        // Node position in screen space
                        PointF posScreen = _mapControl.MapToScreen(new PointF(pos.X, pos.Z));

                        double dx = clickScreen.X - posScreen.X;
                        double dy = clickScreen.Y - posScreen.Y;
                        double distance = Math.Sqrt(dx * dx + dy * dy);

                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestPos = pos;
                        }
                    }
                }
            }

            // Optional: choose only if within some "click radius"
            if (closestPos != null && closestDistance <= 25) // 10 units tolerance
            {
                // Select that tree node in the TreeView
                foreach (TreeNode child in parentNode.Nodes)
                {
                    foreach (TreeNode child2 in child.Nodes[1].Nodes)
                    {
                        if (child2.Tag == closestPos)
                        {
                            ExpansionTV.SelectedNode = child2;
                            break;
                        }
                    }
                }

                //MessageBox.Show($"Selected closest node at X:{closestPos.x:0.##}, Z:{closestPos.z:0.##}");
            }
        }
        private void MapControl_AIPatrolDoubleclicked(object sender, MapClickEventArgs e)
        {
            if (currentTreeNode.Tag is Vec3 v3)
            {
                v3.X = (float)e.MapCoordinates.X;
                v3.Z = (float)e.MapCoordinates.Y;
                if (MapData.FileExists)
                {
                    v3.Y = (MapData.gethieght(v3.X, v3.Z));
                }
                _mapControl.ClearDrawables();

                ExpansionAIPatrolConfig ExpansionAIPatrolConfig = currentTreeNode.FindParentOfType<ExpansionAIPatrolConfig>();
                ExpansionAIPatrolConfig.isDirty = true;
                ShowHandler(new Vector3Control(), typeof(ExpansionAIPatrolConfig), v3, new List<TreeNode>() { currentTreeNode });
                DrawbaseAIPatrols(ExpansionAIPatrolConfig);
                currentTreeNode.Text = v3.GetString();
            }
        }
        private void MapControl_AILocationNoGoAreasSingleclicked(object sender, MapClickEventArgs e)
        {
            if (currentTreeNode?.Parent == null)
                return;

            TreeNode parentNode = currentTreeNode.Parent;

            ExpansionAINoGoArea closestPos = null;
            double closestDistance = double.MaxValue;

            PointF clickScreen = _mapControl.MapToScreen(e.MapCoordinates);

            // Loop through all child nodes of the parent
            foreach (TreeNode child in parentNode.Nodes)
            {
                if (child.Tag is ExpansionAINoGoArea pos)
                {
                    // Node position in screen space
                    PointF posScreen = _mapControl.MapToScreen(new PointF(pos._Position.X, pos._Position.Z));

                    double dx = clickScreen.X - posScreen.X;
                    double dy = clickScreen.Y - posScreen.Y;
                    double distance = Math.Sqrt(dx * dx + dy * dy);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestPos = pos;
                    }
                }
            }

            // Optional: choose only if within some "click radius"
            if (closestPos != null && closestDistance <= 25) // 10 units tolerance
            {
                // Select that tree node in the TreeView
                foreach (TreeNode child in parentNode.Nodes)
                {
                    if (child.Tag == closestPos)
                    {
                        ExpansionTV.SelectedNode = child;
                        break;
                    }
                }

                //MessageBox.Show($"Selected closest node at X:{closestPos.x:0.##}, Z:{closestPos.z:0.##}");
            }
        }
        private void MapControl_AILocationNoGoAreasDoubleclicked(object sender, MapClickEventArgs e)
        {
            if (currentTreeNode.Tag is ExpansionAINoGoArea ExpansionAINoGoArea)
            {
                ExpansionAINoGoArea._Position.X = (float)e.MapCoordinates.X;
                ExpansionAINoGoArea._Position.Z = (float)e.MapCoordinates.Y;
                if (MapData.FileExists)
                {
                    ExpansionAINoGoArea._Position.Y = (MapData.gethieght(ExpansionAINoGoArea._Position.X, ExpansionAINoGoArea._Position.Z));
                }
                _expansionManager.ExpansionAILocationConfig.isDirty = true;

                _mapControl.ClearDrawables();

                ExpansionAILocationConfig ExpansionAILocationConfig = currentTreeNode.FindParentOfType<ExpansionAILocationConfig>();
                ExpansionAILocationConfig.isDirty = true;
                ShowHandler(new ExpansionAINoGoAreaControl(), typeof(ExpansionAILocationConfig), ExpansionAINoGoArea, new List<TreeNode>() { currentTreeNode });
                DrawbaseAILocationNoGoAreas(ExpansionAILocationConfig);
            }
        }
        #endregion mapstuff

        #region right click methods
        /// <summary>
        /// Treeview right click methods
        /// </summary>
        //Loadouts
        private void AddNewAttachmentItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Inventoryattachment newIA = new Inventoryattachment()
            {
                SlotName = "Back",
                Items = new BindingList<AILoadouts>()
            };

            TreeNode newnode = new TreeNode(newIA.SlotName) { Tag = newIA };

            // If selected node is the "inventoryAttachments" category under a file
            if (currentTreeNode != null && currentTreeNode.Tag is string tag && tag == "InventoryAttachments" && currentTreeNode.FindParentOfType<ExpansionLoadoutConfig>() != null)
            {
                AILoadouts AILoadouts = currentTreeNode.FindLastParentOfType<AILoadouts>();
                AILoadouts.InventoryAttachments.Add(newIA);
                currentTreeNode.Nodes.Add(newnode);
                AILoadouts.isDirty = true;
            }
            else if (currentTreeNode.FindParentOfType<AILoadouts>() != null)
            {
                AILoadouts AILoadouts = currentTreeNode.FindParentOfType<AILoadouts>();
                AILoadouts.InventoryAttachments.Add(newIA);
                currentTreeNode.Nodes.Add(newnode);


                // If parent is inside a loot drops file, mark that file dirty
                if (currentTreeNode.FindParentOfType<AILootDrops>() != null)
                    currentTreeNode.FindParentOfType<AILootDrops>().isDirty = true;
                else if (currentTreeNode.FindLastParentOfType<AILoadouts>() != null)
                    currentTreeNode.FindLastParentOfType<AILoadouts>().isDirty = true;
            }
        }
        private void RemoveAttachemtItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If the inventory attachment belongs to an AILoadouts file
            if (currentTreeNode.FindParentOfType<ExpansionLoadoutConfig>() != null)
            {
                AILoadouts AILoadouts = currentTreeNode.FindLastParentOfType<AILoadouts>();
                AILoadouts.InventoryAttachments.Remove(currentTreeNode.Tag as Inventoryattachment);
                if (currentTreeNode != null)
                    currentTreeNode.Remove();
                AILoadouts.isDirty = true;
            }
            else if (currentTreeNode.FindParentOfType<AILootDrops>() != null)
            {
                // If this inventory attachment is inside an AILoadouts that belongs to a loot drops file,
                // find that specific AILoadouts parent and update it and the loot drops file's dirty flag.
                TreeNode parent = currentTreeNode.Parent;
                if (parent != null && parent.Tag is AILoadouts parentLoadout)
                {
                    parentLoadout.InventoryAttachments.Remove(currentTreeNode.Tag as Inventoryattachment);
                    currentTreeNode.FindParentOfType<AILootDrops>().isDirty = true;
                    currentTreeNode.Remove();
                }
            }
        }
        private void AddNewCargoItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AILoadouts newItem = new AILoadouts()
            {
                ClassName = "New Item, Replace me.",
                Chance = (decimal)1.0,
                Quantity = new Quantity(),
                Health = new BindingList<Health>(),
                InventoryAttachments = new BindingList<Inventoryattachment>(),
                InventoryCargo = new BindingList<AILoadouts>(),
                ConstructionPartsBuilt = new BindingList<object>(),
                Sets = new BindingList<AILoadouts>()
            };
            TreeNode newNode = new TreeNode(newItem.ClassName) { Tag = newItem };



            if (currentTreeNode.Parent.Tag is AILoadouts AILoadouts)
            {
                AILoadouts.InventoryCargo.Add(newItem);
                currentTreeNode.Nodes.Add(newNode);
                AILootDrops owningLootFile = currentTreeNode.FindParentOfType<AILootDrops>();
                AILoadouts AILoadouts3 = currentTreeNode.FindLastParentOfType<AILoadouts>();
                if (owningLootFile != null)
                {
                    owningLootFile.isDirty = true;
                }
                else if (AILoadouts3 != null)
                {
                    AILoadouts3.isDirty = true;
                }
            }
            else if (currentTreeNode.Tag is AILoadouts selectedAILoadouts)
            {
                // find/create Cargo child
                TreeNode cargoNode = currentTreeNode.Nodes.Cast<TreeNode>().FirstOrDefault(n => string.Equals(n.Tag as string, "InventoryCargo"));
                if (cargoNode == null)
                {
                    cargoNode = new TreeNode("InventoryCargo") { Tag = "InventoryCargo" };
                    currentTreeNode.Nodes.Add(cargoNode);
                }
                cargoNode.Nodes.Add(newNode);
                selectedAILoadouts.InventoryCargo.Add(newItem);

                // If this AILoadouts is inside a loot drops file, mark the loot drops file dirty
                AILootDrops owningLootFile = currentTreeNode.FindParentOfType<AILootDrops>();
                AILoadouts AILoadouts3 = currentTreeNode.FindLastParentOfType<AILoadouts>();
                if (owningLootFile != null)
                {
                    owningLootFile.isDirty = true;
                }
                else if (AILoadouts3 != null)
                {
                    AILoadouts3.isDirty = true;
                }
            }
            ExpansionTV.SelectedNode = newNode;
        }
        private void AddNewSetItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AILoadouts newSet = new AILoadouts()
            {
                ClassName = "",
                Chance = (decimal)1.0,
                Quantity = new Quantity(),
                Health = new BindingList<Health>(),
                InventoryAttachments = new BindingList<Inventoryattachment>(),
                InventoryCargo = new BindingList<AILoadouts>(),
                ConstructionPartsBuilt = new BindingList<object>(),
                Sets = new BindingList<AILoadouts>()
            };
            AILootDrops owningLootFile = currentTreeNode.FindParentOfType<AILootDrops>(); // capture before removal
            AILoadouts AILoadouts = currentTreeNode.FindLastParentOfType<AILoadouts>();
            if (currentTreeNode.FindParentOfType<ExpansionLoadoutConfig>() != null)
            {
                AILoadouts.Sets.Add(newSet);
                TreeNode newNode = new TreeNode("New Set") { Tag = newSet };
                currentTreeNode.Nodes.Add(newNode);
                AILoadouts.isDirty = true;
            }
        }
        private void addNewItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AILoadouts newItem = new AILoadouts()
            {
                ClassName = "New Item, Replace me.",
                Chance = (decimal)1.0,
                Quantity = new Quantity(),
                Health = new BindingList<Health>(),
                InventoryAttachments = new BindingList<Inventoryattachment>(),
                InventoryCargo = new BindingList<AILoadouts>(),
                ConstructionPartsBuilt = new BindingList<object>(),
                Sets = new BindingList<AILoadouts>()
            };
            newItem.Health.Add(new Health() { Zone = "", Min = (decimal)0.7, Max = (decimal)1.0 });

            TreeNode newNode = new TreeNode(newItem.ClassName) { Tag = newItem };
            currentTreeNode.Nodes.Add(newNode);

            // If adding under AILootDrops file node
            if (currentTreeNode != null && currentTreeNode.Tag is AILootDrops drops)
            {
                drops.LootdropList.Add(newItem);
                drops.isDirty = true;
            }
            else if (currentTreeNode.FindParentOfType<ExpansionLoadoutConfig>() != null)
            {
                AILoadouts AILoadouts = currentTreeNode.FindLastParentOfType<AILoadouts>();
                // If adding under a loadouts file
                if (currentTreeNode.Tag is Inventoryattachment Inventoryattachment)
                    Inventoryattachment.Items.Add(newItem);
                AILoadouts.isDirty = true;
            }
            else if (currentTreeNode.FindParentOfType<AILootDrops>() != null)
            {
                AILootDrops AILootDrops = currentTreeNode.FindLastParentOfType<AILootDrops>();
                if (currentTreeNode.Tag is Inventoryattachment Inventoryattachment)
                    Inventoryattachment.Items.Add(newItem);

                AILootDrops.isDirty = true;
            }
        }
        private void removeItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode parent = currentTreeNode.Parent;

            // --- 1) Inventoryattachment parent ---
            if (parent.Tag is Inventoryattachment ia)
            {
                AILoadouts item = currentTreeNode.Tag as AILoadouts;
                AILootDrops owningLootFile = currentTreeNode.FindParentOfType<AILootDrops>(); // capture before removal
                AILoadouts AILoadouts = currentTreeNode.FindLastParentOfType<AILoadouts>();
                ia.Items.Remove(item);
                currentTreeNode.Remove();

                if (owningLootFile != null)
                    owningLootFile.isDirty = true;
                else if (AILoadouts != null)
                    AILoadouts.isDirty = true;

                return;
            }

            // --- 2) AILootDrops file parent (top-level loadout under LootDrops file) ---
            if (parent.Tag is AILootDrops parentLootFile)
            {
                AILoadouts toRemove = currentTreeNode.Tag as AILoadouts;
                if (toRemove != null)
                {
                    parentLootFile.LootdropList.Remove(toRemove);
                    currentTreeNode.Remove();
                    parentLootFile.isDirty = true;
                }
                return;
            }
            // --- 3) is Lootdrop file ---
            if (currentTreeNode.Tag is AILootDrops AILootDrops)
            {
                if (MessageBox.Show("This Will Remove The All reference to this Lootdrop, Are you sure you want to do this?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    // remove node from tree
                    if (currentTreeNode != null && currentTreeNode.Parent != null)
                        currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
                    AILootDrops.isDirty = true;
                    AILootDrops.ToDelete = true;
                }
            }

            // --- 4) All other cases where selected.Tag is AILoadouts ---
            if (currentTreeNode.Tag is AILoadouts selectedLoadout)
            {
                AILootDrops owningLootFile = currentTreeNode.FindParentOfType<AILootDrops>(); // capture before removal
                AILoadouts AILoadouts = currentTreeNode.FindLastParentOfType<AILoadouts>();
                // (a) InventoryCargo category
                if (string.Equals(parent.Text, "InventoryCargo", StringComparison.OrdinalIgnoreCase))
                {
                    AILoadouts fileRoot = parent.Parent?.Tag as AILoadouts;
                    if (fileRoot != null)
                    {
                        fileRoot.InventoryCargo.Remove(selectedLoadout);


                        if (owningLootFile != null)
                            owningLootFile.isDirty = true;
                        else if (AILoadouts != null)
                            AILoadouts.isDirty = true;

                        TreeNode parentonventory = currentTreeNode.Parent;
                        currentTreeNode.Remove();
                        if (fileRoot.InventoryCargo.Count == 0)
                        {
                            parentonventory.Remove();
                        }
                    }
                }
                // (b) Sets category
                else if (string.Equals(parent.Text, "Sets", StringComparison.OrdinalIgnoreCase))
                {
                    AILoadouts fileRoot = parent.Parent?.Tag as AILoadouts;
                    if (fileRoot != null)
                    {
                        fileRoot.Sets.Remove(selectedLoadout);

                        if (owningLootFile != null)
                            owningLootFile.isDirty = true;
                        else if (AILoadouts != null)
                            AILoadouts.isDirty = true;

                        currentTreeNode.Remove();
                    }
                }
                // (c) Removing Loadouts file
                else if (parent.Tag is ExpansionLoadoutConfig ExpansionLoadoutConfig)
                {
                    if (MessageBox.Show("This Will Remove The All reference to this Loadout, Are you sure you want to do this?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        // remove node from tree
                        if (currentTreeNode != null && currentTreeNode.Parent != null)
                            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
                        AILoadouts.isDirty = true;
                        AILoadouts.ToDelete = true;
                    }
                }
            }
        }
        private void addNewLoadoutFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEventFile frm = new AddEventFile();
            frm.SetTitle = "Add new Loadout File";
            frm.Button4visable = false;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Button5text = "Import to Expansion";
            frm.HideCEStuff();
            frm.moddir = Path.Combine(AppServices.GetRequired<ProjectManager>().CurrentProject.ProjectRoot, AppServices.GetRequired<ProjectManager>().CurrentProject.ProfileName, "ExpansionMod", "Loadouts");
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string newmodPath = frm.moddir.Replace("/", "\\");
                string typesfile = frm.typesname + ".json";
                string newPath = Path.Combine(newmodPath, typesfile);
                AILoadouts newAILoadouts = new AILoadouts()
                {
                    // default constructor already initializes lists
                    ClassName = "",
                    Chance = (decimal)1.0,
                    Quantity = new Quantity(),
                    isDirty = true
                };
                newAILoadouts.setpath(newPath);
                newAILoadouts.isDirty = true;
                bool added = _expansionManager.ExpansionLoadoutConfig.AddNewLoadoutFile(newAILoadouts);
                if (added)
                {
                    TreeNode newNode = SetupLoadoutTreeView(newAILoadouts);
                    string newNodeText = newNode.Text.ToLowerInvariant();

                    int insertIndex = 0;
                    foreach (TreeNode node in currentTreeNode.Nodes)
                    {
                        if (string.Compare(newNodeText, node.Text.ToLowerInvariant()) < 0)
                        {
                            break;
                        }
                        insertIndex++;
                    }

                    currentTreeNode.Nodes.Insert(insertIndex, newNode);
                    ExpansionTV.SelectedNode = newNode;
                    newNode.Expand();
                }
                else
                {
                    MessageBox.Show($"File with the same filename allready exist.\nYou chose... poorly.");
                }
            }
        }
        private void addNewLootDropFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEventFile frm = new AddEventFile();
            frm.SetTitle = "Add new Lootdrop File";
            frm.Button4visable = false;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Button5text = "Import to Expansion";
            frm.HideCEStuff();
            frm.moddir = Path.Combine(AppServices.GetRequired<ProjectManager>().CurrentProject.ProjectRoot, AppServices.GetRequired<ProjectManager>().CurrentProject.ProfileName, "ExpansionMod", "AI", "LootDrops");
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string newmodPath = frm.moddir.Replace("/", "\\");
                string typesfile = frm.typesname + ".json";
                string newPath = Path.Combine(newmodPath, typesfile);
                AILootDrops newAILootDrops = new AILootDrops()
                {
                    LootdropList = new BindingList<AILoadouts>(),
                    isDirty = true
                };
                newAILootDrops.setpath(newPath);
                newAILootDrops.isDirty = true;
                bool added = _expansionManager.ExpansionLootDropConfig.AddNewLootDropFile(newAILootDrops);
                if (added)
                {
                    TreeNode newNode = new TreeNode(newAILootDrops.FileName) { Tag = newAILootDrops };
                    foreach (AILoadouts entry in newAILootDrops.LootdropList)
                    {
                        newNode.Nodes.Add(BuildAILoadoutsNode(entry));
                    }
                    string newNodeText = newNode.Text.ToLowerInvariant();

                    int insertIndex = 0;
                    foreach (TreeNode node in currentTreeNode.Nodes)
                    {
                        if (string.Compare(newNodeText, node.Text.ToLowerInvariant()) < 0)
                        {
                            break;
                        }
                        insertIndex++;
                    }

                    currentTreeNode.Nodes.Insert(insertIndex, newNode);
                    ExpansionTV.SelectedNode = newNode;
                    newNode.Expand();
                }
                else
                {
                    MessageBox.Show($"File with the same filename allready exist.\nYou chose... poorly.");
                }
            }
        }
        // Airdrops 
        private void addNewAirdropContainerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionLootContainer NewContainer = new ExpansionLootContainer();
            _expansionManager.ExpansionAirdropConfig.Data.Containers.Add(NewContainer);
            _expansionManager.ExpansionAirdropConfig.isDirty = true;
            TreeNode alcnodes = new TreeNode(NewContainer.Container)
            {
                Tag = NewContainer
            };
            TreeNode alcinodes = new TreeNode("Infected")
            {
                Tag = "AirdropContainersInfected"
            };
            alcnodes.Nodes.Add(alcinodes);

            TreeNode alclnodes = new TreeNode("Loot")
            {
                Tag = "AirdropContainersLoot"
            };
            alcnodes.Nodes.Add(alclnodes);
            currentTreeNode.Nodes.Add(alcnodes);
        }
        private void removeAirdropContainerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionAirdropConfig.Data.Containers.Remove(currentTreeNode.Tag as ExpansionLootContainer);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
            _expansionManager.ExpansionAirdropConfig.isDirty = true;
        }
        //AI Settings
        private void addAIAdminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemfromString form = new AddItemfromString();
            form.TitleLable = "Add Admin Steam ID";
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.addedtypes.ToList();
                foreach (string l in addedtypes)
                {
                    _expansionManager.ExpansionAIConfig.Data.Admins.Add(l.ToLower());
                    currentTreeNode.Nodes.Add(new TreeNode(l.ToLower())
                    {
                        Tag = "AISettingsAdminString"
                    });
                }
                _expansionManager.ExpansionAIConfig.isDirty = true;
            }
        }
        private void addAIPreventClimbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemfromString form = new AddItemfromString();
            form.TitleLable = "Add Prevent Climb String";
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.addedtypes.ToList();
                foreach (string l in addedtypes)
                {
                    _expansionManager.ExpansionAIConfig.Data.PreventClimb.Add(l);
                    currentTreeNode.Nodes.Add(new TreeNode(l)
                    {
                        Tag = "AISettingsPreventClimbString"
                    });
                }
                _expansionManager.ExpansionAIConfig.isDirty = true;
            }
        }
        private void addAIPlayerFactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddFromList newform = new AddFromList();
            newform.List = File.ReadAllLines("Data/ExpansionFactions.txt").ToList();
            DialogResult result = newform.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> returnlist = newform.GetSelected;
                foreach (string faction in returnlist)
                {
                    if (!_expansionManager.ExpansionAIConfig.Data.PlayerFactions.Contains(faction))
                    {
                        _expansionManager.ExpansionAIConfig.Data.PlayerFactions.Add(faction);
                        currentTreeNode.Nodes.Add(new TreeNode(faction)
                        {
                            Tag = "AISettingsPlayerFactionsString"
                        });
                        _expansionManager.ExpansionAIConfig.isDirty = true;
                    }
                }
            }
        }
        private void removeAIAdminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionAIConfig.Data.Admins.Remove(currentTreeNode.Text);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
            _expansionManager.ExpansionAIConfig.isDirty = true;
        }
        private void removeAIPreventClimbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionAIConfig.Data.PreventClimb.Remove(currentTreeNode.Text);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
            _expansionManager.ExpansionAIConfig.isDirty = true;
        }
        private void removeAIPlayerFactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionAIConfig.Data.PlayerFactions.Remove(currentTreeNode.Text);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
            _expansionManager.ExpansionAIConfig.isDirty = true;
        }
        //AI Patrol Settings
        private void addNewPatrolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionAIPatrol newpatrol = new ExpansionAIPatrol()
            {
                Name = "NewPatrol",
                Persist = 0,
                Faction = "West",
                Formation = "",
                FormationScale = (decimal)-1.0,
                FormationLooseness = (decimal)0.0,
                Loadout = "",
                Units = new BindingList<string>(),
                NumberOfAI = -3,
                Behaviour = "ALTERNATE",
                LootingBehaviour = "DEFAULT",
                Speed = "WALK",
                UnderThreatSpeed = "SPRINT",
                CanBeLooted = 1,
                LootDropOnDeath = "",
                UnlimitedReload = 1,
                SniperProneDistanceThreshold = (decimal)0.0,
                AccuracyMin = -1,
                AccuracyMax = -1,
                ThreatDistanceLimit = -1,
                NoiseInvestigationDistanceLimit = -1,
                DamageMultiplier = -1,
                DamageReceivedMultiplier = -1,
                CanBeTriggeredByAI = 0,
                MinDistRadius = -1,
                MaxDistRadius = -1,
                DespawnRadius = -1,
                MinSpreadRadius = 1,
                MaxSpreadRadius = 100,
                Chance = 1,
                DespawnTime = -1,
                RespawnTime = -2,
                LoadBalancingCategory = "",
                ObjectClassName = "",
                WaypointInterpolation = "",
                UseRandomWaypointAsStartPoint = 1,
                Waypoints = new BindingList<float[]>(),
                _waypoints = new BindingList<Vec3>()
            };
            ExpansionAIPatrolConfig ExpansionAIPatrolConfig = currentTreeNode.FindParentOfType<ExpansionAIPatrolConfig>();
            ExpansionAIPatrolConfig.Data.Patrols.Add(newpatrol);
            TreeNode PatrolRoot = new TreeNode(newpatrol.Name)
            {
                Tag = newpatrol
            };
            CreatePatrolNodes(newpatrol, PatrolRoot);
            currentTreeNode.Nodes.Add(PatrolRoot);
            ExpansionAIPatrolConfig.isDirty = true;
        }
        private void removePatrolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionAIPatrolConfig ExpansionAIPatrolConfig = currentTreeNode.FindParentOfType<ExpansionAIPatrolConfig>();
            ExpansionAIPatrolConfig.Data.Patrols.Remove(currentTreeNode.Tag as ExpansionAIPatrol);
            currentTreeNode.Remove();
            ExpansionAIPatrolConfig.isDirty = true;
        }
        private void addWaypointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionAIPatrolConfig ExpansionAIPatrolConfig = currentTreeNode.FindParentOfType<ExpansionAIPatrolConfig>();
            ExpansionAIPatrol ExpansionAIPatrol = currentTreeNode.FindParentOfType<ExpansionAIPatrol>();
            if (ExpansionAIPatrol._waypoints.Count == null)
                ExpansionAIPatrol._waypoints = new BindingList<Vec3>();

            Vec3 newvec3 = null;
            if (ExpansionAIPatrol._waypoints.Count == 0)
            {
                newvec3 = new Vec3((float)AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize / 2, 0f, (float)AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize / 2);
                if (MapData.FileExists)
                {
                    newvec3.Y = (MapData.gethieght(newvec3.X, newvec3.Z));
                }
            }
            else
            {
                Vec3 vec3 = ExpansionAIPatrol._waypoints.Last();
                newvec3 = new Vec3(vec3.X + 25, 0f, vec3.Z);
                if (MapData.FileExists)
                {
                    newvec3.Y = (MapData.gethieght(newvec3.X, newvec3.Z));
                }
            }
            TreeNode newvec3node = new TreeNode(newvec3.GetString())
            {
                Tag = newvec3
            };
            currentTreeNode.Nodes.Add(newvec3node);
            ExpansionAIPatrol._waypoints.Add(newvec3);
            ExpansionAIPatrolConfig.isDirty = true;
            ExpansionTV.SelectedNode = newvec3node;
        }
        private void removeWaypointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionAIPatrolConfig ExpansionAIPatrolConfig = currentTreeNode.FindParentOfType<ExpansionAIPatrolConfig>();
            ExpansionAIPatrol ExpansionAIPatrol = currentTreeNode.FindParentOfType<ExpansionAIPatrol>();
            ExpansionAIPatrol._waypoints.Remove(currentTreeNode.Tag as Vec3);
            ExpansionAIPatrolConfig.isDirty = true;
            currentTreeNode.Remove();

        }
        private void importWaypointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Import AI Patrol";
            openFileDialog.Filter = "Expansion Map|*.map|Object Spawner|*.json|DayZ Editor|*.dze";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExpansionAIPatrolConfig ExpansionAIPatrolConfig = currentTreeNode.FindParentOfType<ExpansionAIPatrolConfig>();
                ExpansionAIPatrol ExpansionAIPatrol = currentTreeNode.FindParentOfType<ExpansionAIPatrol>();
                string filePath = openFileDialog.FileName;
                DialogResult dialogResult = MessageBox.Show("Clear Exisitng Position?", "Clear position", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    ExpansionAIPatrol._waypoints.Clear();
                    currentTreeNode.Nodes.Clear();
                }
                switch (openFileDialog.FilterIndex)
                {
                    case 1:
                        string[] fileContent = File.ReadAllLines(filePath);
                        for (int i = 0; i < fileContent.Length; i++)
                        {
                            if (fileContent[i] == "") continue;
                            string[] linesplit = fileContent[i].Split('|');
                            string[] XYZ = linesplit[1].Split(' ');
                            ExpansionAIPatrol._waypoints.Add(new Vec3(XYZ));
                        }
                        break;
                    case 2:
                        ObjectSpawnerArr newobjectspawner = JsonSerializer.Deserialize<ObjectSpawnerArr>(File.ReadAllText(filePath));
                        foreach (SpawnObjects so in newobjectspawner.Objects)
                        {
                            ExpansionAIPatrol._waypoints.Add(new Vec3(so.pos));
                        }
                        break;
                    case 3:
                        DZE importfile = DZEHelpers.LoadFile(filePath);
                        foreach (Editorobject eo in importfile.EditorObjects)
                        {
                            ExpansionAIPatrol._waypoints.Add(new Vec3(eo.Position));
                        }
                        break;
                }
                foreach (Vec3 v3 in ExpansionAIPatrol._waypoints)
                {
                    currentTreeNode.Nodes.Add(new TreeNode(v3.GetString())
                    {
                        Tag = v3
                    });
                }
                ExpansionAIPatrolConfig.isDirty = true;
            }
        }
        private void exportWaypointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionAIPatrol ExpansionAIPatrol = currentTreeNode.FindParentOfType<ExpansionAIPatrol>();
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Export AI Patrol";
            save.Filter = "Expansion Map |*.map|Object Spawner|*.json";
            save.FileName = ExpansionAIPatrol.Name;
            if (save.ShowDialog() == DialogResult.OK)
            {
                switch (save.FilterIndex)
                {
                    case 1:
                        StringBuilder SB = new StringBuilder();
                        foreach (Vec3 array in ExpansionAIPatrol._waypoints)
                        {
                            SB.AppendLine("eAI_SurvivorM_Lewis|" + array.GetString() + "|0.0 0.0 0.0");
                        }
                        File.WriteAllText(save.FileName, SB.ToString());
                        break;
                    case 2:
                        ObjectSpawnerArr newobjectspawner = new ObjectSpawnerArr();
                        newobjectspawner.Objects = new BindingList<SpawnObjects>();
                        foreach (Vec3 array in ExpansionAIPatrol._waypoints)
                        {
                            SpawnObjects newobject = new SpawnObjects();
                            newobject.name = "eAI_SurvivorM_Lewis";
                            newobject.pos = array.getfloatarray();
                            newobject.ypr = new float[] { 0, 0, 0 };
                            newobject.scale = 1;
                            newobject.enableCEPersistency = false;
                            newobjectspawner.Objects.Add(newobject);
                        }
                        var options = new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
                        string jsonString = JsonSerializer.Serialize(newobjectspawner, options);
                        File.WriteAllText(save.FileName, jsonString);
                        break;
                }
            }
        }
        private void moveWaypointUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionAIPatrolConfig ExpansionAIPatrolConfig = currentTreeNode.FindParentOfType<ExpansionAIPatrolConfig>();
            ExpansionAIPatrol ExpansionAIPatrol = currentTreeNode.FindParentOfType<ExpansionAIPatrol>();
            Vec3 waypoint = currentTreeNode.Tag as Vec3;
            TreeNodeCollection siblings;
            if (currentTreeNode.Parent != null)
            {
                siblings = currentTreeNode.Parent.Nodes;
            }
            else
            {
                siblings = ExpansionTV.Nodes;
            }

            int index = siblings.IndexOf(currentTreeNode);
            if (index > 0)
            {
                siblings.RemoveAt(index);
                ExpansionAIPatrol._waypoints.RemoveAt(index);
                ExpansionAIPatrol._waypoints.Insert(index - 1, waypoint);
                siblings.Insert(index - 1, currentTreeNode);
                ExpansionTV.SelectedNode = currentTreeNode; // Optional: reselect the node
            }
            ExpansionAIPatrolConfig.isDirty = true;
        }
        private void moveWaypointDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionAIPatrolConfig ExpansionAIPatrolConfig = currentTreeNode.FindParentOfType<ExpansionAIPatrolConfig>();
            ExpansionAIPatrol ExpansionAIPatrol = currentTreeNode.FindParentOfType<ExpansionAIPatrol>();
            Vec3 waypoint = currentTreeNode.Tag as Vec3;
            TreeNodeCollection siblings;
            if (currentTreeNode.Parent != null)
            {
                siblings = currentTreeNode.Parent.Nodes;
            }
            else
            {
                siblings = ExpansionTV.Nodes;
            }

            int index = siblings.IndexOf(currentTreeNode);
            if (index < siblings.Count - 1)
            {
                siblings.RemoveAt(index);
                ExpansionAIPatrol._waypoints.RemoveAt(index);
                ExpansionAIPatrol._waypoints.Insert(index + 1, waypoint);
                siblings.Insert(index + 1, currentTreeNode);
                ExpansionTV.SelectedNode = currentTreeNode; // Optional: reselect the node
            }
            ExpansionAIPatrolConfig.isDirty = true;
        }
        private void addUnitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void removeUnitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionAIPatrolConfig ExpansionAIPatrolConfig = currentTreeNode.FindParentOfType<ExpansionAIPatrolConfig>();
            ExpansionAIPatrol ExpansionAIPatrol = currentTreeNode.FindParentOfType<ExpansionAIPatrol>();
            ExpansionAIPatrol.Units.Remove(currentTreeNode.Text);
            currentTreeNode.Remove();
            ExpansionAIPatrolConfig.isDirty = true;
        }
        private void addNewLoadBalancingCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionAIPatrolConfig ExpansionAIPatrolConfig = currentTreeNode.FindParentOfType<ExpansionAIPatrolConfig>();
            Loadbalancingcategorie newcat = new Loadbalancingcategorie()
            {
                name = "New Category - Change Me",
                Categorieslist = new BindingList<Loadbalancingcategories>()
            };
            ExpansionAIPatrolConfig.Data._LoadBalancingCategories.Add(newcat);
            TreeNode newtreenode = new TreeNode($"Category Name : - {newcat.name}")
            {
                Tag = newcat
            };
            currentTreeNode.Nodes.Add(newtreenode);
            ExpansionTV.SelectedNode = newtreenode;
            ExpansionAIPatrolConfig.isDirty = true;
        }
        private void removeCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionAIPatrolConfig ExpansionAIPatrolConfig = currentTreeNode.FindParentOfType<ExpansionAIPatrolConfig>();
            Loadbalancingcategorie Loadbalancingcategorie = currentTreeNode.Tag as Loadbalancingcategorie;
            ExpansionAIPatrolConfig.Data._LoadBalancingCategories.Remove(Loadbalancingcategorie);
            currentTreeNode.Remove();
            ExpansionAIPatrolConfig.isDirty = true;
        }
        private void addNewLoadBalancingCountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionAIPatrolConfig ExpansionAIPatrolConfig = currentTreeNode.FindParentOfType<ExpansionAIPatrolConfig>();
            Loadbalancingcategorie Loadbalancingcategorie = currentTreeNode.Tag as Loadbalancingcategorie;
            Loadbalancingcategorie.Categorieslist.Add(new Loadbalancingcategories()
            {
                MinPlayers = 0,
                MaxPlayers = 255,
                MaxPatrols = -1
            });
            TreeNode Newtreenode = new TreeNode($"Load Balancing : {(Loadbalancingcategorie.Categorieslist.Count - 1).ToString()}")
            {
                Tag = Loadbalancingcategorie.Categorieslist.Last()
            };
            currentTreeNode.Nodes.Add(Newtreenode);
            ExpansionTV.SelectedNode = Newtreenode;
            ExpansionAIPatrolConfig.isDirty = true;
        }
        private void removeCountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (currentTreeNode.Parent.Tag as Loadbalancingcategorie).Categorieslist.Remove(currentTreeNode.Tag as Loadbalancingcategories);
            currentTreeNode.FindParentOfType<ExpansionAIPatrolConfig>().isDirty = true;
            currentTreeNode.Remove();
        }
        private void addAiNoGoAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionAILocationConfig ExpansionAIPatrolConfig = currentTreeNode.FindParentOfType<ExpansionAILocationConfig>();
            ExpansionAINoGoArea newnogo = new ExpansionAINoGoArea()
            {
                Name = "New NoGO Area",
                _Position = new Vec3((float)AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize / 2, 0f, (float)AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize / 2),
                Radius = 300,
                Height = MapData.gethieght(AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize / 2, AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize / 2) + 200
            };
            if (MapData.FileExists)
            {
                newnogo._Position.Y = (MapData.gethieght(newnogo._Position.X, newnogo._Position.Z));
            }
            ExpansionAIPatrolConfig.Data.NoGoAreas.Add(newnogo);
            TreeNode newtreenode = new TreeNode($"{newnogo.Name}")
            {
                Tag = newnogo
            };
            currentTreeNode.Nodes.Add(newtreenode);
            ExpansionTV.SelectedNode = newtreenode;
            ExpansionAIPatrolConfig.isDirty = true;
        }
        private void removeAINoGoAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionAILocationConfig ExpansionAIPatrolConfig = currentTreeNode.FindParentOfType<ExpansionAILocationConfig>();
            ExpansionAIPatrolConfig.Data.NoGoAreas.Remove(currentTreeNode.Tag as ExpansionAINoGoArea);
            currentTreeNode.Remove();
            ExpansionAIPatrolConfig.isDirty = true;
        }
        private void addAIExcludedBuildingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionAILocationConfig ExpansionAIPatrolConfig = currentTreeNode.FindParentOfType<ExpansionAILocationConfig>();
            AddItemfromString form = new AddItemfromString();
            form.TitleLable = "Add Admin Steam ID";
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.addedtypes.ToList();
                foreach (string l in addedtypes)
                {
                    if (!ExpansionAIPatrolConfig.Data.ExcludedRoamingBuildings.Contains(l))
                    {
                        ExpansionAIPatrolConfig.Data.ExcludedRoamingBuildings.Add(l);
                        currentTreeNode.Nodes.Add(new TreeNode(l)
                        {
                            Tag = "ExcludedRoamingBuilding"
                        });
                    }
                }
                ExpansionAIPatrolConfig.isDirty = true;
            }
        }
        private void removeAIExlusdedBuildingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionAILocationConfig ExpansionAIPatrolConfig = currentTreeNode.FindParentOfType<ExpansionAILocationConfig>();
            ExpansionAIPatrolConfig.Data.ExcludedRoamingBuildings.Remove(currentTreeNode.Text);
            currentTreeNode.Remove();
            ExpansionAIPatrolConfig.isDirty = true;
        }
        //BaseBuilding Settings
        private void addNewDeployableOutsideTerritoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes { };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    if (!_expansionManager.ExpansionBaseBuildingConfig.Data.DeployableOutsideATerritory.Contains(l))
                    {
                        _expansionManager.ExpansionBaseBuildingConfig.Data.DeployableOutsideATerritory.Add(l);
                        currentTreeNode.Nodes.Add(new TreeNode(l)
                        {
                            Tag = "BaseBuildingDeployableOutsideATerritoryItem"
                        });
                        _expansionManager.ExpansionBaseBuildingConfig.isDirty = true;
                    }
                }
            }
        }
        private void removeDeployableOutsideTerritoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionBaseBuildingConfig.Data.DeployableOutsideATerritory.Remove(currentTreeNode.Text);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
            _expansionManager.ExpansionBaseBuildingConfig.isDirty = true;
        }
        private void addNewDeployableInsideEnemyTerritoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes { };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    if (!_expansionManager.ExpansionBaseBuildingConfig.Data.DeployableInsideAEnemyTerritory.Contains(l))
                    {
                        _expansionManager.ExpansionBaseBuildingConfig.Data.DeployableInsideAEnemyTerritory.Add(l);
                        currentTreeNode.Nodes.Add(new TreeNode(l)
                        {
                            Tag = "BaseBuildingDeployableInsideAEnemyTerritoryItem"
                        });
                        _expansionManager.ExpansionBaseBuildingConfig.isDirty = true;
                    }
                }
            }
        }
        private void removeDeployableInsideEnemyTerritoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionBaseBuildingConfig.Data.DeployableInsideAEnemyTerritory.Remove(currentTreeNode.Text);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
            _expansionManager.ExpansionBaseBuildingConfig.isDirty = true;
        }
        private void addNewVirtualStorageExcludedContainerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes { };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    if (!_expansionManager.ExpansionBaseBuildingConfig.Data.VirtualStorageExcludedContainers.Contains(l))
                    {
                        _expansionManager.ExpansionBaseBuildingConfig.Data.VirtualStorageExcludedContainers.Add(l);
                        currentTreeNode.Nodes.Add(new TreeNode(l)
                        {
                            Tag = "BaseBuildingVirtualStorageExcludedContainersItem"
                        });
                        _expansionManager.ExpansionBaseBuildingConfig.isDirty = true;
                    }
                }
            }
        }
        private void removeVirtualStorageExcludedContainerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionBaseBuildingConfig.Data.VirtualStorageExcludedContainers.Remove(currentTreeNode.Text);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
            _expansionManager.ExpansionBaseBuildingConfig.isDirty = true;
        }
        private void addNewNoBuildZoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int MapSize = AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize;
            ExpansionBuildNoBuildZone nbz = new ExpansionBuildNoBuildZone()
            {
                Name = "new No Build Zone",
                CustomMessage = "",
                Radius = 100,
                Center = new float[] { MapSize / 2, 0, MapSize / 2 },
                IsWhitelist = 0,
                Items = new BindingList<string>()
            };
            _expansionManager.ExpansionBaseBuildingConfig.Data.Zones.Add(nbz);
            TreeNode nbznode = new TreeNode(nbz.Name)
            {
                Tag = nbz
            };
            TreeNode nbzinodes = new TreeNode("Items")
            {
                Tag = "BaseBuildingNoBuldZoneItems"
            };
            foreach (string s in nbz.Items)
            {
                nbzinodes.Nodes.Add(new TreeNode($"Item: {s}")
                {
                    Tag = "BaseBuildingNoBuldZoneItem"
                });
            }
            nbznode.Nodes.Add(nbzinodes);
            currentTreeNode.Nodes.Add(nbznode);
            _expansionManager.ExpansionBaseBuildingConfig.isDirty = true;
            ExpansionTV.SelectedNode = currentTreeNode.LastNode;
        }
        private void RemoveNoBuildZoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionBaseBuildingConfig.Data.Zones.Remove(currentTreeNode.Tag as ExpansionBuildNoBuildZone);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
            _expansionManager.ExpansionBaseBuildingConfig.isDirty = true;
        }
        private void addBuildZoneItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes { };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                ExpansionBuildNoBuildZone currentbuildzone = currentTreeNode.Parent.Tag as ExpansionBuildNoBuildZone;
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    if (!currentbuildzone.Items.Contains(l))
                    {
                        currentbuildzone.Items.Add(l);
                        currentTreeNode.Nodes.Add(new TreeNode($"Item: {l}")
                        {
                            Tag = "BaseBuildingNoBuldZoneItem"
                        });
                        _expansionManager.ExpansionBaseBuildingConfig.isDirty = true;
                    }
                }
            }
        }
        private void removeBuildZoneItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionBuildNoBuildZone currentbuildzone = currentTreeNode.Parent.Parent.Tag as ExpansionBuildNoBuildZone;
            currentbuildzone.Items.Remove(currentTreeNode.Text.Substring(6));
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
            _expansionManager.ExpansionBaseBuildingConfig.isDirty = true;
        }
        //Book settings
        private void addNewDescriptionCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionBookDescriptionCategory newdescrioptcat = new ExpansionBookDescriptionCategory()
            {
                CategoryName = "New Category",
                Descriptions = new BindingList<ExpansionBookDescription>()
            };
            _expansionManager.ExpansionBookConfig.Data.Descriptions.Add(newdescrioptcat);
            _expansionManager.ExpansionBookConfig.isDirty = true;
            currentTreeNode.Nodes.Add(new TreeNode($"Category Name: {newdescrioptcat.CategoryName}")
            {
                Tag = newdescrioptcat
            });
        }
        private void removeDescriptionCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionBookConfig.Data.Descriptions.Remove(currentTreeNode.Tag as ExpansionBookDescriptionCategory);
            _expansionManager.ExpansionBookConfig.isDirty = true;
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
        }
        private void addNewRuleCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionBookRuleCategory newExpansionBookRuleCategory = new ExpansionBookRuleCategory()
            {
                CategoryName = "New Rule",
                Rules = new BindingList<ExpansionBookRule>()
            };
            _expansionManager.ExpansionBookConfig.Data.RuleCategories.Add(newExpansionBookRuleCategory);
            _expansionManager.ExpansionBookConfig.isDirty = true;
            currentTreeNode.Nodes.Add(new TreeNode($"Category Name: {newExpansionBookRuleCategory.CategoryName}")
            {
                Tag = newExpansionBookRuleCategory
            });
        }
        private void removeRuleCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionBookConfig.Data.RuleCategories.Remove(currentTreeNode.Tag as ExpansionBookRuleCategory);
            _expansionManager.ExpansionBookConfig.isDirty = true;
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
        }
        private void addNewRuleParagraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionBookRuleCategory ExpansionBookRuleCategory = currentTreeNode.Tag as ExpansionBookRuleCategory;
            ExpansionBookRule newExpansionBookRule = new ExpansionBookRule()
            {
                RuleParagraph = "new paragraph",
                RuleText = "NewText"
            };
            ExpansionBookRuleCategory.Rules.Add(newExpansionBookRule);
            _expansionManager.ExpansionBookConfig.isDirty = true;
            currentTreeNode.Nodes.Add(new TreeNode($"Rule Paragraph: {newExpansionBookRule.RuleParagraph}")
            {
                Tag = newExpansionBookRule
            });
        }
        private void removeRuleParagrapghToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionBookRuleCategory ExpansionBookRuleCategory = currentTreeNode.Parent.Tag as ExpansionBookRuleCategory;
            ExpansionBookRuleCategory.Rules.Remove(currentTreeNode.Tag as ExpansionBookRule);
            _expansionManager.ExpansionBookConfig.isDirty = true;
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
        }
        private void addNewLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionBookLink newExpansionBookLink = new ExpansionBookLink()
            {
                Name = "New link",
                URL = "Some Url",
                IconColor = -1,
                IconName = "Homepage"
            };
            _expansionManager.ExpansionBookConfig.Data.Links.Add(newExpansionBookLink);
            _expansionManager.ExpansionBookConfig.isDirty = true;
            currentTreeNode.Nodes.Add(new TreeNode($"Links: {newExpansionBookLink.Name}")
            {
                Tag = newExpansionBookLink
            });
        }
        private void removeLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionBookConfig.Data.Links.Remove(currentTreeNode.Tag as ExpansionBookLink);
            _expansionManager.ExpansionBookConfig.isDirty = true;
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
        }
        private void addNewCraftingCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionBookCraftingCategory newExpansionBookCraftingCategory = new ExpansionBookCraftingCategory()
            {
                CategoryName = "New Crafting Category",
                Results = new BindingList<string>()
            };
            _expansionManager.ExpansionBookConfig.Data.CraftingCategories.Add(newExpansionBookCraftingCategory);
            _expansionManager.ExpansionBookConfig.isDirty = true;
            currentTreeNode.Nodes.Add(new TreeNode($"Category: {newExpansionBookCraftingCategory.CategoryName}")
            {
                Tag = newExpansionBookCraftingCategory
            });
        }
        private void removeCraftingCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionBookConfig.Data.CraftingCategories.Remove(currentTreeNode.Tag as ExpansionBookCraftingCategory);
            _expansionManager.ExpansionBookConfig.isDirty = true;
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
        }
        #endregion right click methods



    }

    [PluginInfo("Exspansion Manager", "ExspansionPlugin", "ExpansionPlugin.Expansion.png")]
    public class PluginExspansion : IPluginForm, IDisposable
    {
        private bool disposed = false;

        public string pluginIdentifier => "ExspansionPlugin";
        public string pluginName => "Exspansion Manager";

        public Form GetForm()
        {
            return new ExpansionForm(this);
        }
        public override string ToString()
        {
            return pluginName;
        }

        public void Dispose()
        {
            if (!disposed)
            {
                // Dispose any resources (e.g., file handles, etc.)
                disposed = true;
            }
        }
    }
}
