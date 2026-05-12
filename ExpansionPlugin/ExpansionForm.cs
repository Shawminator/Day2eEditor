using Day2eEditor;
using EconomyPlugin;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Windows.Forms;
using System.Windows.Forms.Design.Behavior;
using System.Xml;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

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
            AppServices.Register(_expansionManager);
            _expansionManager.SetExpansionStuff();
            _expansionManager.SetExternalFiles();


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
                    if (node.Parent.Tag.ToString() == "AIPatrolWayPoints")
                    {
                        Vec3 v3 = node.Tag as Vec3;
                        var control = new Vector3Control();
                        control.PositionChanged += (updatedPos) =>
                        {
                            _mapControl.ClearDrawables();
                            var tag = node.Parent?.Parent?.Parent?.Parent?.Tag;
                            if (tag is ExpansionAIPatrolConfig patrolConfig)
                            {
                                DrawbaseAIPatrols(patrolConfig);
                            }
                            else if (tag is ExpansionQuestObjectiveAICampConfig campConfig)
                            {
                                DrawbaseObjectiveAICamp(campConfig);
                            }
                            else if (tag is ExpansionQuestObjectiveAIPatrolConfig questPatrolConfig)
                            {
                                DrawbaseObjectiveAIPatrols(questPatrolConfig);
                            }
                        };
                        ShowHandler(control, typeof(ExpansionAIPatrolConfig), v3, selected);

                        ExpansionAIPatrol ExpansionAIPatrol = node.Parent.Parent.Tag as ExpansionAIPatrol;
                        SetupAIPatrols(ExpansionAIPatrol, node);
                        _mapControl.EnsureVisible(new PointF(v3.X, v3.Z));
                    }
                    else if (node.Parent.Tag.ToString() == "ObjectivesTreasureHuntPositions")
                    {
                        Vec3 v3 = node.Tag as Vec3;
                        var control = new Vector3Control();
                        control.PositionChanged += (updatedPos) =>
                        {
                            _mapControl.ClearDrawables();
                            var tag = node.Parent?.Parent?.Tag;
                            if (tag is ExpansionQuestObjectiveTreasureHuntConfig tresure)
                            {
                                DrawbaseTreasureHunt(tresure);
                            }
                        };
                        ShowHandler(control, typeof(ExpansionQuestObjectiveTreasureHuntConfig), v3, selected);
                        ExpansionQuestObjectiveTreasureHuntConfig tresure = node.Parent.Parent.Tag as ExpansionQuestObjectiveTreasureHuntConfig;
                        SetupTreasureHunt(tresure, node);
                        _mapControl.EnsureVisible(new PointF(v3.X, v3.Z));
                    }
                    else if (node.Parent.Tag.ToString() == "QuestObjectiveAIEscortPosition")
                    {
                        Vec3 v3 = node.Tag as Vec3;
                        var control = new Vector3Control();
                        control.PositionChanged += (updatedPos) =>
                        {
                            _mapControl.ClearDrawables();
                            var tag = node.Parent?.Parent?.Tag;
                            if (tag is ExpansionQuestObjectiveAIEscortConfig ExpansionQuestObjectiveAIEscortConfig)
                            {
                                DrawbaseAIEscort(ExpansionQuestObjectiveAIEscortConfig);
                            }
                        };
                        ShowHandler(control, typeof(ExpansionQuestObjectiveAIEscortConfig), v3, selected);
                        ExpansionQuestObjectiveAIEscortConfig ExpansionQuestObjectiveAIEscortConfig = node.Parent.Parent.Tag as ExpansionQuestObjectiveAIEscortConfig;
                        SetupAIEscort(ExpansionQuestObjectiveAIEscortConfig, node);
                        _mapControl.EnsureVisible(new PointF(v3.X, v3.Z));
                    }
                    else if (node.Parent.Tag.ToString() == "QuestObjectiveTargetPosition")
                    {
                        Vec3 v3 = node.Tag as Vec3;
                        var control = new Vector3Control();
                        control.PositionChanged += (updatedPos) =>
                        {
                            _mapControl.ClearDrawables();
                            var tag = node.Parent?.Parent?.Tag;
                            if (tag is ExpansionQuestObjectiveTargetConfig target)
                            {
                                DrawbaseTarget(target);
                            }
                        };
                        ShowHandler(control, typeof(ExpansionQuestObjectiveTargetConfig), v3, selected);
                        ExpansionQuestObjectiveTargetConfig target = node.Parent.Parent.Tag as ExpansionQuestObjectiveTargetConfig;
                        SetupTarget(target, node);
                        _mapControl.EnsureVisible(new PointF(v3.X, v3.Z));
                    }
                    else if (node.Parent.Tag.ToString() == "QuestObjectiveTravelPosition")
                    {
                        Vec3 v3 = node.Tag as Vec3;
                        var control = new Vector3Control();
                        control.PositionChanged += (updatedPos) =>
                        {
                            _mapControl.ClearDrawables();
                            var tag = node.Parent?.Parent?.Tag;
                            if (tag is ExpansionQuestObjectiveTravelConfig travel)
                            {
                                DrawbaseTravel(travel);
                            }
                        };
                        ShowHandler(control, typeof(ExpansionQuestObjectiveTravelConfig), v3, selected);
                        ExpansionQuestObjectiveTravelConfig travel = node.Parent.Parent.Tag as ExpansionQuestObjectiveTravelConfig;
                        SetupTravel(travel, node);
                        _mapControl.EnsureVisible(new PointF(v3.X, v3.Z));
                    }
                    else if (node.Parent.Tag is ExpansionSafeZonePolygon)
                    {
                        Vec3 v3 = node.Tag as Vec3;
                        var control = new Vector3Control();
                        control.PositionChanged += (updatedPos) =>
                        {
                            _mapControl.ClearDrawables();
                            DrawbaseSafeZoneData(node.FindParentOfType<ExpansionSafeZoneConfig>());
                        };
                        ShowHandler(control, typeof(ExpansionSafeZoneConfig), v3, selected);

                        ExpansionSafeZonePolygon ExpansionSafeZonePolygon = node.Parent.Tag as ExpansionSafeZonePolygon;
                        SetupSafeZonePolygonMarkers(ExpansionSafeZonePolygon, node);
                        _mapControl.EnsureVisible(new PointF(v3.X, v3.Z));
                    }
                    else if (node.Parent.Tag is ExpansionSpawnLocation)
                    {
                        Vec3 v3 = node.Tag as Vec3;
                        var control = new Vector3Control();
                        control.PositionChanged += (updatedPos) =>
                        {
                            _mapControl.ClearDrawables();
                            DrawbaseSpawnLocationData(node.FindParentOfType<ExpansionSpawnConfig>());
                        };
                        ShowHandler(control, typeof(ExpansionSpawnConfig), v3, selected);

                        ExpansionSpawnLocation ExpansionSpawnLocation = node.Parent.Tag as ExpansionSpawnLocation;
                        SetupSpawnLocationMarkers(ExpansionSpawnLocation, node);
                        _mapControl.EnsureVisible(new PointF(v3.X, v3.Z));
                    }
                    else if (node.Parent.Nodes[0].Tag is ExpansionP2PMarketTraderConfig)
                    {
                        Vec3 v3 = node.Tag as Vec3;
                        var control = new Vector3Control();
                        control.PositionChanged += (updatedPos) =>
                        {
                            _mapControl.ClearDrawables();
                            DrawP2PTraderVehicleSpawnData(node.FindParentOfType<ExpansionP2pMarketTradersConfig>());
                        };
                        ShowHandler(control, typeof(ExpansionP2pMarketTradersConfig), v3, selected);
                        SetupP2PTraderVehicleSpawnMarkers(v3, node);
                        _mapControl.EnsureVisible(new PointF(v3.X, v3.Z));
                    }
                    else if (node.Parent.Tag is ExpansionTraderMaps ExpansionTraderMaps)
                    {
                        ShowHandler(new Vector3Control(), typeof(ExpansionMarketTraderMapsConfig), node.Tag as Vec3, selected);
                    }
                    else if (node.Parent.Tag.ToString() == "expansionMarketTraderMapWaypoints")
                    {
                        Vec3 v3 = node.Tag as Vec3;
                        var control = new Vector3Control();
                        control.PositionChanged += (updatedPos) =>
                        {
                            _mapControl.ClearDrawables();
                            var tag = node.Parent?.Parent?.Parent?.Parent?.Tag;
                            if (tag is ExpansionMarketTraderMapsConfig ExpansionMarketTraderMapsConfig)
                            {
                                DrawTraderNPCPositions(ExpansionMarketTraderMapsConfig);
                            }
                        };
                        ShowHandler(control, typeof(ExpansionMarketTraderMapsConfig), v3, selected);
                        ExpansionTraderMaps tm = node.Parent.Parent.Tag as ExpansionTraderMaps;
                        SetupTraderNPCPOsitions(tm, node);
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
                    _mapControl.EnsureVisible(new PointF((float)ExpansionAIRoamingLocation.Position.X, (float)ExpansionAIRoamingLocation.Position.Z));
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
                    _mapControl.EnsureVisible(new PointF(ExpansionAINoGoArea.Position.X, ExpansionAINoGoArea.Position.Z));
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
                [typeof(ExpansionChatSettings)] = (node, selected) =>
                {
                    ExpansionChatSettings ExpansionChatSettings = node.Tag as ExpansionChatSettings;
                    ShowHandler(new ExpansionChatSettingsControl(), typeof(ExpansionChatConfig), ExpansionChatSettings, selected);
                },
                [typeof(ExpansionChatColors)] = (node, selected) =>
                {
                    ExpansionChatColors ExpansionChatColors = node.Tag as ExpansionChatColors;
                    ShowHandler(new ExpansionChatColorsControl(), typeof(ExpansionChatConfig), ExpansionChatColors, selected);
                },
                //Core
                [typeof(ExpansionCoreSettings)] = (node, selected) =>
                {
                    ExpansionCoreSettings ExpansionCoreSettings = node.Tag as ExpansionCoreSettings;
                    ShowHandler(new ExpansionCoreControl(), typeof(ExpansionCoreConfig), ExpansionCoreSettings, selected);
                },
                //Damage
                [typeof(ExpansionDamageSystemSettings)] = (node, selected) =>
                {
                    ExpansionDamageSystemSettings ExpansionDamageSystemSettings = node.Tag as ExpansionDamageSystemSettings;
                    ShowHandler(new ExpansionDamageControl(), typeof(ExpansionDamageSystemConfig), ExpansionDamageSystemSettings, selected);
                },
                [typeof(ExplosiveProjectiles)] = (node, selected) =>
                {
                    ExplosiveProjectiles ExplosiveProjectiles = node.Tag as ExplosiveProjectiles;
                    ShowHandler(new ExpansionDamageProjectilesControl(), typeof(ExpansionDamageSystemConfig), ExplosiveProjectiles, selected);
                },
                //Garage
                [typeof(ExpansionGarageSettings)] = (node, selected) =>
                {
                    ExpansionGarageSettings ExpansionGarageSettings = node.Tag as ExpansionGarageSettings;
                    ShowHandler(new ExpansionGarageSettingsControl(), typeof(ExpansionGarageConfig), ExpansionGarageSettings, selected);
                },
                //General
                [typeof(ExpansionMapping)] = (node, selected) =>
                {
                    ExpansionMapping ExpansionMapping = node.Tag as ExpansionMapping;
                    ShowHandler(new ExpansionGeneralMappingControl(), typeof(ExpansionGeneralConfig), ExpansionMapping, selected);
                },
                //Hardline
                [typeof(ExpansionHardlineSettings)] = (node, selected) =>
                {
                    ExpansionHardlineSettings ExpansionHardlineSettings = node.Tag as ExpansionHardlineSettings;
                    ShowHandler(new ExpansionHardlineGneralControl(), typeof(ExpansionHardlineConfig), ExpansionHardlineSettings, selected);
                },
                //Logs
                [typeof(ExpansionLogsSettings)] = (node, selected) =>
                {
                    ExpansionLogsSettings ExpansionLogsSettings = node.Tag as ExpansionLogsSettings;
                    ShowHandler(new ExpansionHardlineLogsControl(), typeof(ExpansionLogsConfig), ExpansionLogsSettings, selected);
                },
                //map
                [typeof(ExpansionServerMarkerData)] = (node, selected) =>
                {
                    ExpansionServerMarkerData ExpansionServerMarkerData = node.Tag as ExpansionServerMarkerData;
                    var control = new ExpansionMapServerMarkerInfoControl();
                    control.MarkerChanged += (updatedMarker) =>
                    {
                        _mapControl.ClearDrawables(); ;
                        DrawbaseServerMarkerData(node.FindParentOfType<ExpansionMapConfig>());
                    };
                    ShowHandler(control, typeof(ExpansionMapConfig), ExpansionServerMarkerData, selected);

                    SetupMapServerMarkers(ExpansionServerMarkerData, node);
                    _mapControl.EnsureVisible(new PointF(ExpansionServerMarkerData.m_Position[0], ExpansionServerMarkerData.m_Position[2]));
                },
                //Market
                [typeof(ExpansionMarketSettings)] = (node, selected) =>
                {
                    ExpansionMarketSettings MarketSettings = node.Tag as ExpansionMarketSettings;
                    ShowHandler(new ExpansionMarketSettingsGeneralControl(), typeof(ExpansionMarketSettingsConfig), MarketSettings, selected);
                },
                [typeof(MarketMenuColours)] = (node, selected) =>
                {
                    ExpansionMarketSettingsConfig ExpansionMarketSettingsConfig = node.Parent.Tag as ExpansionMarketSettingsConfig;
                    ShowHandler(new ExpansionMarketSettingsHUDControl(), typeof(ExpansionMarketSettingsConfig), ExpansionMarketSettingsConfig.Data, selected);
                },
                [typeof(ExpansionMarketSpawnPosition)] = (node, selected) =>
                {
                    ExpansionMarketSpawnPosition ExpansionMarketSpawnPosition = node.Tag as ExpansionMarketSpawnPosition;
                    var control = new ExpasnionMarksetSettingsVehicleSpawnInfoControl();
                    control.PositionChanged += (updatedPos) =>
                    {
                        _mapControl.ClearDrawables();
                        DrawbaseVehicleSpawnPositions(node.FindParentOfType<ExpansionMarketSettingsConfig>());
                    };
                    control.OrientationChanged += (updatedOrientation) =>
                    {
                        _mapControl.ClearDrawables();
                        DrawbaseVehicleSpawnPositions(node.FindParentOfType<ExpansionMarketSettingsConfig>());
                    };
                    ShowHandler(control, typeof(ExpansionMarketSettingsConfig), ExpansionMarketSpawnPosition, selected);
                    SetupVehicleSpawnLocation(ExpansionMarketSpawnPosition, node);
                    _mapControl.EnsureVisible(new PointF(ExpansionMarketSpawnPosition.Position[0], ExpansionMarketSpawnPosition.Position[2]));
                },
                //MarketCategory
                [typeof(ExpansionMarketCategory)] = (node, selected) =>
                {
                    ExpansionMarketCategory ExpansionMarketCategory = node.Tag as ExpansionMarketCategory;
                    ShowHandler(new ExpansionMarketCategoryControl(), typeof(ExpansionMarketCategoryConfig), ExpansionMarketCategory, selected);
                },
                [typeof(ExpansionMarketItem)] = (node, selected) =>
                {
                    ExpansionMarketItem ExpansionMarketItem = node.Tag as ExpansionMarketItem;
                    ShowHandler(new ExpansionMarketItemControl(), typeof(ExpansionMarketCategoryConfig), ExpansionMarketItem, selected);
                },
                //MarketTrader
                [typeof(ExpansionMarketTrader)] = (node, selected) =>
                {
                    ExpansionMarketTrader ExpansionMarketTrader = node.Tag as ExpansionMarketTrader;
                    ShowHandler(new ExpansionMarketTraderControl(), typeof(ExpansionMarketTraderConfig), ExpansionMarketTrader, selected);
                },
                [typeof(ExpansionMarketTraderCategory)] = (node, selected) =>
                {
                    ExpansionMarketTraderCategory ExpansionMarketTraderCategory = node.Tag as ExpansionMarketTraderCategory;
                    ShowHandler(new ExpansionMarketTraderCategoryControl(), typeof(ExpansionMarketTraderConfig), ExpansionMarketTraderCategory, selected);
                },
                [typeof(ExpansionMarketTraderItem)] = (node, selected) =>
                {
                    ExpansionMarketTraderItem ExpansionMarketTraderItem = node.Tag as ExpansionMarketTraderItem;
                    ShowHandler(new ExpansionMarketTraderItemControl(), typeof(ExpansionMarketTraderConfig), ExpansionMarketTraderItem, selected);
                },
                //MarketZone
                [typeof(ExpansionMarketTraderZone)] = (node, selected) =>
                {
                    ExpansionMarketTraderZone ExpansionMarketTraderZone = node.Tag as ExpansionMarketTraderZone;
                    ShowHandler(new ExpansionMarketTraderZoneControl(), typeof(ExpansionMarketTraderZoneConfig), ExpansionMarketTraderZone, selected);
                },
                //Market Trader Maps
                [typeof(ExpansionMarketTraderNpcs)] = (node, selected) =>
                {
                    ExpansionMarketTraderNpcs ExpansionMarketTraderNpcs = node.Tag as ExpansionMarketTraderNpcs;
                    ShowHandler(new ExpansionMarketTraderNpcsControl(), typeof(ExpansionMarketTraderMapsConfig), ExpansionMarketTraderNpcs, selected);
                },
                //Missions
                [typeof(ExpansionMissionSettings)] = (node, selected) =>
                {
                    ExpansionMissionSettings ExpansionMissionSettings = node.Tag as ExpansionMissionSettings;
                    ShowHandler(new ExpansionMissionSettingsControl(), typeof(ExpansionMissionSettingsConfig), ExpansionMissionSettings, selected);
                },
                [typeof(ExpansionMissionEventBase)] = (node, selected) =>
                {
                    ExpansionMissionEventBase ExpansionMissionEventBase = node.Tag as ExpansionMissionEventBase;
                    ShowHandler(new ExpansionMissionEventBaseControl(), typeof(ExpansionMissionSettingsConfig), ExpansionMissionEventBase, selected);
                },
                [typeof(ExpansionMissionEventAirdrop)] = (node, selected) =>
                {
                    ExpansionMissionEventAirdrop ExpansionMissionEventAirdrop = node.Tag as ExpansionMissionEventAirdrop;
                    ShowHandler(new ExpansionMissionEventBaseControl(), typeof(ExpansionMissionSettingsConfig), ExpansionMissionEventAirdrop, selected);
                },
                [typeof(ExpansionAirdropLocation)] = (node, selected) =>
                {
                    ExpansionAirdropLocation ExpansionAirdropLocation = node.Tag as ExpansionAirdropLocation;
                    var control = new ExpansionAirdropLocationControl();
                    control.PositionChanged += (updatedPos) =>
                    {
                        _mapControl.ClearDrawables();
                        DrawbaseMissionMarkerData(node.FindParentOfType<ExpansionMissionsConfig>());
                    };
                    control.RadiusChanged += (UpdateRadius) =>
                    {
                        _mapControl.ClearDrawables();
                        DrawbaseMissionMarkerData(node.FindParentOfType<ExpansionMissionsConfig>());
                    };
                    ShowHandler(control, typeof(ExpansionMissionSettingsConfig), ExpansionAirdropLocation, selected);
                    SetupMissionMarkers(ExpansionAirdropLocation, node);
                    _mapControl.EnsureVisible(new PointF((float)ExpansionAirdropLocation.x, (float)ExpansionAirdropLocation.z));
                },

                [typeof(ExpansionMissionEventContaminatedArea)] = (node, selected) =>
                {
                    ExpansionMissionEventContaminatedArea ExpansionMissionEventContaminatedArea = node.Tag as ExpansionMissionEventContaminatedArea;
                    ShowHandler(new ExpansionMissionEventBaseControl(), typeof(ExpansionMissionSettingsConfig), ExpansionMissionEventContaminatedArea, selected);
                },
                [typeof(Data)] = (node, selected) =>
                {
                    ExpansionMissionEventContaminatedArea area = node.Parent.Tag as ExpansionMissionEventContaminatedArea;
                    ExpansionMissionEffectAreaControl control = new ExpansionMissionEffectAreaControl();
                    control.PositionChanged += (updatedPos) =>
                    {
                        _mapControl.ClearDrawables();
                        DrawbaseMissionMarkerData(node.FindParentOfType<ExpansionMissionsConfig>());
                    };
                    control.RadiusChanged += (UpdateRadius) =>
                    {
                        _mapControl.ClearDrawables();
                        DrawbaseMissionMarkerData(node.FindParentOfType<ExpansionMissionsConfig>());
                    };
                    ShowHandler(control, typeof(CfgeffectareaConfig), area, selected);
                    SetupEffectAreaMap(area, node);
                    _mapControl.EnsureVisible(new PointF((float)area.Data.Pos[0], (float)area.Data.Pos[2]));
                },
                [typeof(PlayerData)] = (node, selected) =>
                {
                    PlayerData PlayerData = node.Tag as PlayerData;
                    ShowHandler<IUIHandler>(new ExpansionMissionEventContaminatedAreaPlayerDataControl(), typeof(ExpansionMissionEventBase), PlayerData, selected);
                },
                //Monitoring
                [typeof(MonitoringSettings)] = (node, selected) =>
                {
                    MonitoringSettings MonitoringSettings = node.Tag as MonitoringSettings;
                    ShowHandler(new ExpansionMonitoringSettingsControl(), typeof(ExpansionMonitoringConfig), MonitoringSettings, selected);
                },
                //NameTag
                [typeof(NameTagsSettings)] = (node, selected) =>
                {
                    NameTagsSettings NameTagsSettings = node.Tag as NameTagsSettings;
                    ShowHandler(new ExpansionNameTagSettingsControl(), typeof(ExpansionNameTagsConfig), NameTagsSettings, selected);
                },
                //Notification
                [typeof(ExpansionNotificationSettings)] = (node, selected) =>
                {
                    ExpansionNotificationSettings ExpansionNotificationSettings = node.Tag as ExpansionNotificationSettings;
                    ShowHandler(new ExpansionNotificationSettingsControl(), typeof(ExpansionNotificationConfig), ExpansionNotificationSettings, selected);
                },
                //NotificationScheduler
                [typeof(ExpansionNotificationSchedulerSettings)] = (node, selected) =>
                {
                    ExpansionNotificationSchedulerSettings ExpansionNotificationSchedulerSettings = node.Tag as ExpansionNotificationSchedulerSettings;
                    ShowHandler(new ExpansionNotificationSchedulerSettingsControl(), typeof(ExpansionNotificationSchedulerConfig), ExpansionNotificationSchedulerSettings, selected);
                },
                [typeof(ExpansionNotificationSchedule)] = (node, selected) =>
                {
                    ExpansionNotificationSchedule ExpansionNotificationSchedule = node.Tag as ExpansionNotificationSchedule;
                    ShowHandler(new ExpansionNotificationScheduleControl(), typeof(ExpansionNotificationSchedulerConfig), ExpansionNotificationSchedule, selected);
                },
                //p2pMarket
                [typeof(ExpasnionP2PMarketSettings)] = (node, selected) =>
                {
                    ExpasnionP2PMarketSettings ExpasnionP2PMarketSettings = node.Tag as ExpasnionP2PMarketSettings;
                    ShowHandler(new ExpansionP2PMarketSettingsGeneralControl(), typeof(ExpansionP2PMarketConfig), ExpasnionP2PMarketSettings, selected);
                },
                [typeof(ExpansionP2PMarketMenuCategory)] = (node, selected) =>
                {
                    ExpansionP2PMarketMenuCategoryBase ExpansionP2PMarketMenuCategoryBase = node.Tag as ExpansionP2PMarketMenuCategoryBase;
                    ShowHandler(new ExpansionP2PMarketSettingsCatControl(), typeof(ExpansionP2PMarketConfig), ExpansionP2PMarketMenuCategoryBase, selected);
                },
                [typeof(ExpansionP2PMarketMenuSubCategory)] = (node, selected) =>
                {
                    ExpansionP2PMarketMenuCategoryBase ExpansionP2PMarketMenuCategoryBase = node.Tag as ExpansionP2PMarketMenuCategoryBase;
                    ShowHandler(new ExpansionP2PMarketSettingsCatControl(), typeof(ExpansionP2PMarketConfig), ExpansionP2PMarketMenuCategoryBase, selected);
                },
                //P2PMarket
                [typeof(ExpansionP2PMarketTraderConfig)] = (node, selected) =>
                {
                    ExpansionP2PMarketTraderConfig ExpansionP2PMarketTraderConfig = node.Tag as ExpansionP2PMarketTraderConfig;
                    ShowHandler(new ExpansionP2PMarketTraderConfigGeneralControl(), typeof(ExpansionP2pMarketTradersConfig), ExpansionP2PMarketTraderConfig, selected);
                },
                //Party
                [typeof(ExpansionPartySettings)] = (node, selected) =>
                {
                    ExpansionPartySettings ExpansionPartySettings = node.Tag as ExpansionPartySettings;
                    ShowHandler(new ExpansionPartySettingsControl(), typeof(ExpansionPartyConfig), ExpansionPartySettings, selected);
                },
                //personalStorage
                [typeof(ExpansionPersonalStorageNewSettings)] = (node, selected) =>
                {
                    ExpansionPersonalStorageNewSettings ExpansionPersonalStorageNewSettings = node.Tag as ExpansionPersonalStorageNewSettings;
                    ShowHandler(new ExpansionPersonalStorageNewSettingsGeneralControl(), typeof(ExpansionPersonalStorageNewSettingsConfig), ExpansionPersonalStorageNewSettings, selected);
                },
                [typeof(ExpansionPersonalStorageSettings)] = (node, selected) =>
                {
                    ExpansionPersonalStorageSettings ExpansionPersonalStorageSettings = node.Tag as ExpansionPersonalStorageSettings;
                    ShowHandler(new ExpansionPersonalStorageSettingsGeneralControl(), typeof(ExpansionPersonalStorageSettingsConfig), ExpansionPersonalStorageSettings, selected);

                },
                [typeof(ExpansionPersonalStorageMenuCategory)] = (node, selected) =>
                {
                    ExpansionPersonalStorageMenuCategoryBase ExpansionPersonalStorageMenuCategoryBase = node.Tag as ExpansionPersonalStorageMenuCategoryBase;
                    ShowHandler(new ExpansionPersonalStorageSettingsCatControl(), typeof(ExpansionPersonalStorageSettingsConfig), ExpansionPersonalStorageMenuCategoryBase, selected);
                },
                [typeof(ExpansionPersonalStorageMenuSubCategory)] = (node, selected) =>
                {
                    ExpansionPersonalStorageMenuSubCategory ExpansionPersonalStorageMenuSubCategory = node.Tag as ExpansionPersonalStorageMenuSubCategory;
                    ShowHandler(new ExpansionPersonalStorageSettingsCatControl(), typeof(ExpansionPersonalStorageSettingsConfig), ExpansionPersonalStorageMenuSubCategory, selected);
                },
                [typeof(ExpansionPersonalStorageLevel)] = (node, selected) =>
                {
                    ExpansionPersonalStorageLevel ExpansionPersonalStorageLevel = node.Tag as ExpansionPersonalStorageLevel;
                    ShowHandler(new ExpansionPersonalStorageLevelControl(), typeof(ExpansionPersonalStorageNewSettingsConfig), ExpansionPersonalStorageLevel, selected);
                },
                //PlayerList
                [typeof(ExpansionPlayerListSettings)] = (node, selected) =>
                {
                    ExpansionPlayerListSettings ExpansionPlayerListSettings = node.Tag as ExpansionPlayerListSettings;
                    ShowHandler(new ExpansionPlayerListSettingsControl(), typeof(ExpansionPlayerListConfig), ExpansionPlayerListSettings, selected);
                },
                //Raid
                [typeof(ExpansionRaidSettings)] = (node, selected) =>
                {
                    ExpansionRaidSettings ExpansionRaidSettings = node.Tag as ExpansionRaidSettings;
                    ShowHandler(new ExpansionRaidSettingsControl(), typeof(ExpansionRaidConfig), ExpansionRaidSettings, selected);
                },
                [typeof(ExpansionRaidSchedule)] = (node, selected) =>
                {
                    ExpansionRaidSchedule ExpansionRaidSchedule = node.Tag as ExpansionRaidSchedule;
                    ShowHandler(new ExpansionRaidSettingsRaidScheduleControl(), typeof(ExpansionRaidConfig), ExpansionRaidSchedule, selected);
                },
                //SafeZone
                [typeof(ExpansionSafeZoneSettings)] = (node, selected) =>
                {
                    ExpansionSafeZoneSettings ExpansionSafeZoneSettings = node.Tag as ExpansionSafeZoneSettings;
                    ShowHandler(new ExpansionSafeZoneSettingsControl(), typeof(ExpansionSafeZoneConfig), ExpansionSafeZoneSettings, selected);
                },
                [typeof(ExpansionSafeZoneCircle)] = (node, selected) =>
                {
                    ExpansionSafeZoneCircle ExpansionSafeZoneCircle = node.Tag as ExpansionSafeZoneCircle;
                    var control = new ExpansionSafeZoneCircleControl();
                    control.PositionChanged += (updatedPos) =>
                    {
                        _mapControl.ClearDrawables();
                        DrawbaseSafeZoneData(node.FindParentOfType<ExpansionSafeZoneConfig>());
                    };
                    control.RadiusChanged += (UpdateRadius) =>
                    {
                        _mapControl.ClearDrawables();
                        DrawbaseSafeZoneData(node.FindParentOfType<ExpansionSafeZoneConfig>());
                    };
                    ShowHandler(control, typeof(ExpansionSafeZoneConfig), ExpansionSafeZoneCircle, selected);

                    SetupSafeZoneCircleMarkers(ExpansionSafeZoneCircle, node);
                    _mapControl.EnsureVisible(new PointF(ExpansionSafeZoneCircle.Center.X, ExpansionSafeZoneCircle.Center.Z));
                },
                [typeof(ExpansionSafeZonePolygon)] = (node, selected) =>
                {
                    ExpansionSafeZonePolygon ExpansionSafeZonePolygon = node.Tag as ExpansionSafeZonePolygon;
                    ShowHandler<IUIHandler>(null, typeof(ExpansionSafeZoneConfig), ExpansionSafeZonePolygon, selected);

                    SetupSafeZonePolygonMarkers(ExpansionSafeZonePolygon, node);
                    _mapControl.EnsureVisible(PolygonPanTarget.GetPanTargetXZ(ExpansionSafeZonePolygon.Positions));
                },
                [typeof(ExpansionSafeZoneCylinder)] = (node, selected) =>
                {
                    ExpansionSafeZoneCylinder ExpansionSafeZoneCylinder = node.Tag as ExpansionSafeZoneCylinder;
                    var control = new ExpansionSafeZoneCylinderControl();
                    control.PositionChanged += (updatedPos) =>
                    {
                        _mapControl.ClearDrawables(); ;
                        DrawbaseSafeZoneData(node.FindParentOfType<ExpansionSafeZoneConfig>());
                    };
                    control.RadiusChanged += (UpdateRadius) =>
                    {
                        _mapControl.ClearDrawables(); ;
                        DrawbaseSafeZoneData(node.FindParentOfType<ExpansionSafeZoneConfig>());
                    };
                    ShowHandler(control, typeof(ExpansionSafeZoneConfig), ExpansionSafeZoneCylinder, selected);

                    SetupSafeZoneCylinderMarkers(ExpansionSafeZoneCylinder, node);
                    _mapControl.EnsureVisible(new PointF(ExpansionSafeZoneCylinder.Center.X, ExpansionSafeZoneCylinder.Center.Z));
                },
                //SocialMedia
                [typeof(ExpansionNewsFeedTextSetting)] = (node, selected) =>
                {
                    ExpansionNewsFeedTextSetting ExpansionNewsFeedTextSetting = node.Tag as ExpansionNewsFeedTextSetting;
                    ShowHandler(new ExpansionSocialMediaSettingsTextControl(), typeof(ExpansionSocialMediaConfig), ExpansionNewsFeedTextSetting, selected);
                },
                [typeof(ExpansionNewsFeedLinkSetting)] = (node, selected) =>
                {
                    ExpansionNewsFeedLinkSetting ExpansionNewsFeedLinkSetting = node.Tag as ExpansionNewsFeedLinkSetting;
                    ShowHandler(new ExpansionSocialMediaSettingsLinkControl(), typeof(ExpansionSocialMediaConfig), ExpansionNewsFeedLinkSetting, selected);
                },
                //Spawn
                [typeof(ExpansionSpawnSettings)] = (node, selected) =>
                {
                    ExpansionSpawnSettings ExpansionSpawnSettings = node.Tag as ExpansionSpawnSettings;
                    ShowHandler(new ExpansionSpawnSettingsGeneralControl(), typeof(ExpansionSpawnConfig), ExpansionSpawnSettings, selected);
                },
                [typeof(ExpansionSpawnLocation)] = (node, selected) =>
                {
                    ExpansionSpawnLocation ExpansionSpawnLocation = node.Tag as ExpansionSpawnLocation;
                    ShowHandler(new ExpansionSpawnLocationControl(), typeof(ExpansionSpawnConfig), ExpansionSpawnLocation, selected);
                    SetupSpawnLocationMarkers(ExpansionSpawnLocation, node);
                    if (ExpansionSpawnLocation.Positions.Count > 0)
                    {
                        _mapControl.EnsureVisible(new PointF(ExpansionSpawnLocation.Positions[0].X, ExpansionSpawnLocation.Positions[0].Z));
                    }
                },
                [typeof(ExpansionStartingClothing)] = (node, selected) =>
                {
                    ExpansionStartingClothing ExpansionStartingClothing = node.Tag as ExpansionStartingClothing;
                    ShowHandler(new ExpansionStartingClothingGeneralControl(), typeof(ExpansionSpawnConfig), ExpansionStartingClothing, selected);
                },
                [typeof(ExpansionStartingGear)] = (node, selected) =>
                {
                    ExpansionStartingGear ExpansionStartingGear = node.Tag as ExpansionStartingGear;
                    ShowHandler(new ExpansionStartingGearGeneralControl(), typeof(ExpansionSpawnConfig), ExpansionStartingGear, selected);
                },
                [typeof(ExpansionStartingGearItem)] = (node, selected) =>
                {
                    ExpansionStartingGearItem ExpansionStartingGearItem = node.Tag as ExpansionStartingGearItem;
                    ShowHandler(new ExpansionStartingGearItemControl(), typeof(ExpansionSpawnConfig), ExpansionStartingGearItem, selected);
                },
                [typeof(ExpansionSpawnGearLoadouts)] = (node, selected) =>
                {
                    ExpansionSpawnGearLoadouts ExpansionSpawnGearLoadouts = node.Tag as ExpansionSpawnGearLoadouts;
                    ShowHandler(new ExpansionSpawnGearLoadoutControl(), typeof(ExpansionSpawnConfig), ExpansionSpawnGearLoadouts, selected);
                },
                //Territory
                [typeof(ExpansionTerritorySettings)] = (node, selected) =>
                {
                    ExpansionTerritorySettings ExpansionTerritorySettings = node.Tag as ExpansionTerritorySettings;
                    ShowHandler(new ExpansionTerritorySettingsControl(), typeof(ExpansionTerritoryConfig), ExpansionTerritorySettings, selected);
                },
                //Vehicles
                [typeof(ExpansionVehiclesLockConfig)] = (node, selected) =>
                {
                    ExpansionVehiclesLockConfig ExpansionVehiclesLockConfig = node.Tag as ExpansionVehiclesLockConfig;
                    ShowHandler(new ExpansionVehiclesLockConfigControl(), typeof(ExpansionVehiclesConfig), ExpansionVehiclesLockConfig, selected);
                },
                //Quests
                //objective node type
                [typeof(ObjectiveNodeTag)] = (node, selected) =>
                {

                    if (node.Tag is ObjectiveNodeTag tag)
                    {
                        switch (tag.Kind)
                        {
                            case ObjectiveNodeKind.BaseConfig:
                                ShowHandler(new ExpansionQuestObjectiveConfigControl(), typeof(ExpansionQuestObjectiveConfigConfig), tag.Object, selected);
                                return;

                            case ObjectiveNodeKind.SpecificConfig:
                                DispatchSpecificEditor(tag.Object, selected);
                                return;
                        }
                    }

                },
                [typeof(ExpansionQuestObjectiveTreasureHuntConfig)] = (node, selected) =>
                {
                    if (node.Tag is not ObjectiveNodeTag)
                    {
                        ShowHandler<IUIHandler>(null, null, null, selected);
                        return;
                    }
                    var tag = (ObjectiveNodeTag)node.Tag;
                    var cfg = (ExpansionQuestObjectiveTreasureHuntConfig)tag.Object;
                    ShowHandler(new ExpansionQuestObjectiveTreasureHuntConfigControl(), typeof(ExpansionQuestObjectiveConfigConfig), cfg, selected);
                },
                [typeof(ExpansionQuestObjectiveActionConfig)] = (node, selected) =>
                {
                    if (node.Tag is not ObjectiveNodeTag)
                    {
                        ShowHandler<IUIHandler>(null, null, null, selected);
                        return;
                    }
                    var tag = (ObjectiveNodeTag)node.Tag;
                    var cfg = (ExpansionQuestObjectiveActionConfig)tag.Object;
                    ShowHandler(new ExpansionQuestObjectiveActionConfigControl(), typeof(ExpansionQuestObjectiveConfigConfig), cfg, selected);
                },
                [typeof(ExpansionQuestObjectiveAICampConfig)] = (node, selected) =>
                {
                    if (node.Tag is not ObjectiveNodeTag)
                    {
                        ShowHandler<IUIHandler>(null, null, null, selected);
                        return;
                    }
                    var tag = (ObjectiveNodeTag)node.Tag;
                    var cfg = (ExpansionQuestObjectiveAICampConfig)tag.Object;
                    ShowHandler(new ExpansionQuestObjectiveAICampConfigControl(), typeof(ExpansionQuestObjectiveConfigConfig), cfg, selected);
                },
                [typeof(ExpansionQuestObjectiveAIPatrolConfig)] = (node, selected) =>
                {
                    if (node.Tag is not ObjectiveNodeTag)
                    {
                        ShowHandler<IUIHandler>(null, null, null, selected);
                        return;
                    }
                    var tag = (ObjectiveNodeTag)node.Tag;
                    var cfg = (ExpansionQuestObjectiveAIPatrolConfig)tag.Object;
                    ShowHandler(new ExpansionQuestObjectiveAIPatrolConfigControl(), typeof(ExpansionQuestObjectiveConfigConfig), cfg, selected);
                },
                [typeof(ExpansionQuestObjectiveAIEscortConfig)] = (node, selected) =>
                {
                    if (node.Tag is not ObjectiveNodeTag)
                    {
                        ShowHandler<IUIHandler>(null, null, null, selected);
                        return;
                    }
                    var tag = (ObjectiveNodeTag)node.Tag;
                    var cfg = (ExpansionQuestObjectiveAIEscortConfig)tag.Object;
                    ShowHandler(new ExpansionQuestObjectiveAIEscortConfigControl(), typeof(ExpansionQuestObjectiveAIEscortConfig), cfg, selected);
                },
                [typeof(ExpansionQuestObjectiveCollectionConfig)] = (node, selected) =>
                {
                    if (node.Tag is not ObjectiveNodeTag)
                    {
                        ShowHandler<IUIHandler>(null, null, null, selected);
                        return;
                    }
                    var tag = (ObjectiveNodeTag)node.Tag;
                    var cfg = (ExpansionQuestObjectiveCollectionConfig)tag.Object;
                    ShowHandler(new ExpansionQuestObjectiveCollectionConfigControl(), typeof(ExpansionQuestObjectiveCollectionConfig), cfg, selected);
                },
                [typeof(ExpansionQuestObjectiveDelivery)] = (node, selected) =>
                {
                    if (node.Parent.Parent.Tag is ExpansionQuestObjectiveCollectionConfig)
                    {
                        ExpansionQuestObjectiveDelivery ExpansionQuestObjectiveDelivery = node.Tag as ExpansionQuestObjectiveDelivery;
                        ShowHandler(new ExpansionQuestObjectiveDeliveryControl(), typeof(ExpansionQuestObjectiveCollectionConfig), ExpansionQuestObjectiveDelivery, selected);
                    }
                    else if (node.Parent.Parent.Tag is ExpansionQuestObjectiveDeliveryConfig)
                    {
                        ExpansionQuestObjectiveDelivery ExpansionQuestObjectiveDelivery = node.Tag as ExpansionQuestObjectiveDelivery;
                        ShowHandler(new ExpansionQuestObjectiveDeliveryControl(), typeof(ExpansionQuestObjectiveDeliveryConfig), ExpansionQuestObjectiveDelivery, selected);
                    }
                },
                [typeof(ExpansionQuestObjectiveCraftingConfig)] = (node, selected) =>
                {
                    if (node.Tag is not ObjectiveNodeTag)
                    {
                        ShowHandler<IUIHandler>(null, null, null, selected);
                        return;
                    }
                    var tag = (ObjectiveNodeTag)node.Tag;
                    var cfg = (ExpansionQuestObjectiveCraftingConfig)tag.Object;
                    ShowHandler(new ExpansionQuestObjectiveCraftingConfigControl(), typeof(ExpansionQuestObjectiveCraftingConfig), cfg, selected);
                },
                [typeof(ExpansionQuestObjectiveDeliveryConfig)] = (node, selected) =>
                {
                    if (node.Tag is not ObjectiveNodeTag)
                    {
                        ShowHandler<IUIHandler>(null, null, null, selected);
                        return;
                    }
                    var tag = (ObjectiveNodeTag)node.Tag;
                    var cfg = (ExpansionQuestObjectiveDeliveryConfig)tag.Object;
                    ShowHandler(new ExpansionQuestObjectiveDeliveryConfigControl(), typeof(ExpansionQuestObjectiveDeliveryConfig), cfg, selected);
                },
                [typeof(ExpansionQuestObjectiveTargetConfig)] = (node, selected) =>
                {
                    if (node.Tag is not ObjectiveNodeTag)
                    {
                        ShowHandler<IUIHandler>(null, null, null, selected);
                        return;
                    }
                    var tag = (ObjectiveNodeTag)node.Tag;
                    var cfg = (ExpansionQuestObjectiveTargetConfig)tag.Object;
                    ShowHandler(new ExpansionQuestObjectiveTargetConfigControl(), typeof(ExpansionQuestObjectiveTargetConfig), cfg, selected);
                },
                [typeof(ExpansionQuestObjectiveTravelConfig)] = (node, selected) =>
                {
                    if (node.Tag is not ObjectiveNodeTag)
                    {
                        ShowHandler<IUIHandler>(null, null, null, selected);
                        return;
                    }
                    var tag = (ObjectiveNodeTag)node.Tag;
                    var cfg = (ExpansionQuestObjectiveTravelConfig)tag.Object;
                    ShowHandler(new ExpansionQuestObjectiveTravelConfigControl(), typeof(ExpansionQuestObjectiveTravelConfig), cfg, selected);
                },
                [typeof(Objectives)] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(null, null, null, selected);
                    var objectiveRef = node.Tag as Objectives;
                    if (MessageBox.Show(
                        $"Jump to Objective {objectiveRef.ID}?",
                        "Navigate",
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        if (objectiveRef == null)
                            return;

                        var objectiveFiles = _expansionManager.ExpansionQuestObjectiveConfigConfig.MutableItems;

                        var objectiveConfig = objectiveFiles.FirstOrDefault(x =>
                            x.ID == objectiveRef.ID &&
                            x.ObjectiveType == objectiveRef.ObjectiveType);

                        if (objectiveConfig == null)
                        {
                            MessageBox.Show($"Objective {objectiveRef.ID} not found.");
                            return;
                        }
                        var targetNode = Helpers.FindNodeByTag(ExpansionTV.Nodes, objectiveConfig);

                        if (targetNode != null)
                        {
                            ExpansionTV.SelectedNode = targetNode;
                            targetNode.EnsureVisible();
                            ExpansionTV.SelectedNode.ExpandAll();
                        }
                    }
                },
                [typeof(ExpansionQuestQuest)] = (node, selected) =>
                {
                    ExpansionQuestQuest quest = node.Tag as ExpansionQuestQuest;
                    ExpansionQuestQuest parentquest = node.Parent.Tag as ExpansionQuestQuest;

                    if (quest.Equals(parentquest))
                    {
                        switch (node.Text)
                        {
                            case "Basic Info":
                                ShowHandler(new ExpansionQuestQuestBasicInfoControl(), typeof(ExpansionQuestQuest), quest, selected);
                                break;
                            case "Advanced":
                                ShowHandler(new ExpansionQuestQuestAdvancedControl(), typeof(ExpansionQuestQuest), quest, selected);
                                break;
                            case "Text / Dialogue":
                                ShowHandler(new ExpansionQuestQuestTextDialogueControl(), typeof(ExpansionQuestQuest), quest, selected);
                                break;
                            case "Flow":
                                ShowHandler(new ExpansionQuestQuestFlowControl(), typeof(ExpansionQuestQuest), quest, selected);
                                break;
                            case "Quest Items":
                                ShowHandler(new ExpansionQuestQuestItemsControl(), typeof(ExpansionQuestQuest), quest, selected);
                                break;
                            case "Rewards":
                                ShowHandler(new ExpansionQuestQuestRewardsControl(), typeof(ExpansionQuestQuest), quest, selected);
                                break;
                            default:
                                ShowHandler<IUIHandler>(null, null, null, selected);
                                break;
                        }
                    }
                    else
                    {
                        ShowHandler<IUIHandler>(null, null, null, selected);
                    }
                },
                [typeof(QuestReferenceNode)] = (node, selected) =>
                {
                    if (node.Tag is QuestReferenceNode questRef)
                    {
                        ShowHandler<IUIHandler>(null, null, null, selected);
                        if (MessageBox.Show(
                        $"Jump to Quest {questRef.QuestID}?",
                        "Navigate",
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            var quest = _expansionManager.ExpansionQuestQuestConfig.GetQuestbyID(questRef.QuestID);
                            var targetNode = Helpers.FindNodeByTag(ExpansionTV.Nodes, quest);

                            if (targetNode != null)
                            {
                                ExpansionTV.SelectedNode = targetNode;
                                targetNode.EnsureVisible();
                                targetNode.Expand();
                            }
                        }
                    }
                },
                [typeof(ExpansionQuestItemConfig)] = (node, selected) =>
                {
                    ExpansionQuestItemConfig ExpansionQuestItemConfig = node.Tag as ExpansionQuestItemConfig;
                    ShowHandler(new ExpansionQuestItemConfigControl(), typeof(ExpansionQuestItemConfig), ExpansionQuestItemConfig, selected);
                },
                [typeof(ExpansionQuestRewardConfig)] = (node, selected) =>
                {
                    ExpansionQuestRewardConfig ExpansionQuestRewardConfig = node.Tag as ExpansionQuestRewardConfig;
                    ShowHandler(new ExpansionQuestRewardConfigControl(), typeof(ExpansionQuestItemConfig), ExpansionQuestRewardConfig, selected);
                },
            };
            // ----------------------
            // String handlers
            // ----------------------
            _stringHandlers = new Dictionary<string, Action<TreeNode, List<TreeNode>>>
            {
                //ExpansionLoot shared
                ["ExpansionLootList"] = (node, selected) =>
                {
                    if (node.Parent.Tag is ExpansionMissionEventAirdrop ExpansionMissionEventAirdrop)
                    {
                        ShowHandler<IUIHandler>(new ExpansionLootControl(), typeof(ExpansionMissionEventAirdrop), ExpansionMissionEventAirdrop.Loot, selected);
                    }
                    else if (node.Parent.Tag is ExpansionLootContainer ExpansionLootContainer)
                    {
                        ShowHandler<IUIHandler>(new ExpansionLootControl(), typeof(ExpansionAirdropConfig), ExpansionLootContainer.Loot, selected);
                    }
                    else if (node.Parent.Tag is ExpansionQuestObjectiveTreasureHuntConfig ExpansionQuestObjectiveTreasureHuntConfig)
                    {
                        ShowHandler<IUIHandler>(new ExpansionLootControl(), typeof(ExpansionQuestObjectiveTreasureHuntConfig), ExpansionQuestObjectiveTreasureHuntConfig.Loot, selected);
                    }
                },
                //AI
                ["AIPatrolGeneral"] = (node, selected) =>
                {
                    if (node.Parent.Parent.Parent.Tag is ExpansionAIPatrolConfig)
                    {
                        ExpansionAIPatrol ExpansionAIPatrol = node.Parent.Tag as ExpansionAIPatrol;
                        ShowHandler<IUIHandler>(new AIPatrolControl(), typeof(ExpansionAIPatrolConfig), ExpansionAIPatrol, selected);
                    }
                    else if (node.Parent.Parent.Parent.Tag is ExpansionQuestObjectiveAICampConfig)
                    {
                        ExpansionAIPatrol ExpansionAIPatrol = node.Parent.Tag as ExpansionAIPatrol;
                        ShowHandler<IUIHandler>(new AIPatrolControl(), typeof(ExpansionQuestObjectiveAICampConfig), ExpansionAIPatrol, selected);
                    }
                    else if (node.Parent.Parent.Parent.Tag is ExpansionQuestObjectiveAIPatrolConfig)
                    {
                        ExpansionAIPatrol ExpansionAIPatrol = node.Parent.Tag as ExpansionAIPatrol;
                        ShowHandler<IUIHandler>(new AIPatrolControl(), typeof(ExpansionQuestObjectiveAIPatrolConfig), ExpansionAIPatrol, selected);
                    }
                },
                //Airdrops
                ["AirdropContainersInfected"] = (node, selected) =>
                {
                    if (node.Parent.Tag is ExpansionMissionEventAirdrop)
                    {
                        ExpansionMissionEventAirdrop cfg = node.FindParentOfType<ExpansionMissionEventAirdrop>();
                        ShowHandler<IUIHandler>(new ExpansionInfectedControl(), typeof(ExpansionMissionEventAirdrop), cfg.Infected, selected);
                    }
                    else
                    {
                        ExpansionLootContainer cfg = node.FindParentOfType<ExpansionLootContainer>();
                        ShowHandler<IUIHandler>(new ExpansionInfectedControl(), typeof(ExpansionAirdropConfig), cfg.Infected, selected);
                    }
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
                //General
                ["GenralGernal"] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(new ExpansionGeneralGeneralControl(), typeof(ExpansionGeneralConfig), _expansionManager.ExpansionGeneralConfig.Data, selected);
                },
                ["GenralScreen"] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(new ExpansionGeneralScreenControl(), typeof(ExpansionGeneralConfig), _expansionManager.ExpansionGeneralConfig.Data, selected);
                },
                ["GenralGraveCross"] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(new ExpansionGeneralGraveCrossControl(), typeof(ExpansionGeneralConfig), _expansionManager.ExpansionGeneralConfig.Data, selected);
                },
                ["GenralLights"] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(new ExpansionGeneralLightsControl(), typeof(ExpansionGeneralConfig), _expansionManager.ExpansionGeneralConfig.Data, selected);
                },
                ["GenralHuds"] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(new ExpansionGeneralHudColoursControl(), typeof(ExpansionGeneralConfig), _expansionManager.ExpansionGeneralConfig.Data, selected);
                },
                //Hardline
                ["Reputation"] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(new ExpansionHardlineReputationControl(), typeof(ExpansionHardlineConfig), _expansionManager.ExpansionHardlineConfig.Data, selected);
                },
                ["RequirementsandItemRarity"] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(new ExpansionHardlineItemRarityControl(), typeof(ExpansionHardlineConfig), _expansionManager.ExpansionHardlineConfig.Data, selected);
                },
                //Map
                ["MapSettings"] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(new ExpansionMapMapControl(), typeof(ExpansionMapConfig), _expansionManager.ExpansionMapConfig.Data, selected);
                },
                ["PersonalMarkerSettings"] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(new ExpansionMapPersonalMarkersControl(), typeof(ExpansionMapConfig), _expansionManager.ExpansionMapConfig.Data, selected);
                },
                ["CompassSettings"] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(new ExpansionMapCompassControl(), typeof(ExpansionMapConfig), _expansionManager.ExpansionMapConfig.Data, selected);
                },
                ["ServerMarkersSettings"] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(new ExpansionMapServerMarkerControl(), typeof(ExpansionMapConfig), _expansionManager.ExpansionMapConfig.Data, selected);
                },
                //MArket Zone
                ["TraderZoneArea"] = (node, selected) =>
                {
                    ExpansionMarketTraderZone ExpansionMarketTraderZone = node.Parent.Tag as ExpansionMarketTraderZone;
                    var control = new ExpansionMarkettraderZonePositionsControl();
                    control.PositionChanged += (updatedPos) =>
                    {
                        _mapControl.ClearDrawables(); ;
                        DrawbaseTraderZonePositions(node.FindParentOfType<ExpansionMarketTraderZoneConfig>());
                    };
                    control.RadiusChanged += (updatedOrientation) =>
                    {
                        _mapControl.ClearDrawables(); ;
                        DrawbaseTraderZonePositions(node.FindParentOfType<ExpansionMarketTraderZoneConfig>());
                    };
                    ShowHandler(control, typeof(ExpansionMarketTraderZoneConfig), ExpansionMarketTraderZone, selected);
                    SetupTraderZonePositions(ExpansionMarketTraderZone, node);
                    _mapControl.EnsureVisible(new PointF(ExpansionMarketTraderZone.Position.X, ExpansionMarketTraderZone.Position.Z));
                },
                ["TarderZoneStock"] = (node, selected) =>
                {
                    ExpansionMarketTraderZone ExpansionMarketTraderZone = node.Parent.Tag as ExpansionMarketTraderZone;
                    ShowHandler<IUIHandler>(new ExpansionMarketTraderZoneStockControl(), typeof(ExpansionMarketTraderZoneConfig), ExpansionMarketTraderZone, selected);
                },
                //Missions
                ["MissionAirdrop"] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(new ExpansionMIssionAirdropSettingsControl(), typeof(ExpansionMissionsConfig), node.Parent.Tag as ExpansionMissionEventAirdrop, selected);
                },
                ["MissionContaminatedAreaData"] = (node, selected) =>
                {
                    ExpansionMissionEventContaminatedArea ExpansionMissionEventContaminatedArea = node.Parent.Tag as ExpansionMissionEventContaminatedArea;
                    ShowHandler<IUIHandler>(new ExpansionMissionEventContaminatedAreaDataControl(), typeof(ExpansionMissionEventBase), ExpansionMissionEventContaminatedArea.Data, selected);
                },
                ["MissionContaminatedAreaGeneral"] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(new ExpansionMissionEventContaminatedAreaControl(), typeof(ExpansionMissionsConfig), node.Parent.Tag as ExpansionMissionEventContaminatedArea, selected);
                },
                //P2P Market
                ["P2PMarketTraderPOSandOri"] = (node, selected) =>
                {
                    ExpansionP2PMarketTraderConfig ExpansionP2PMarketTraderConfig = node.Parent.Nodes[0].Tag as ExpansionP2PMarketTraderConfig;
                    var control = new ExpasnionP2PMarksetTraderSpawnInfoControl();
                    control.PositionChanged += (updatedPos) =>
                    {
                        _mapControl.ClearDrawables(); ;
                        DrawbaseP2PTraderSpawnPositions(node.FindParentOfType<ExpansionP2pMarketTradersConfig>());
                    };
                    control.OrientationChanged += (updatedOrientation) =>
                    {
                        _mapControl.ClearDrawables(); ;
                        DrawbaseP2PTraderSpawnPositions(node.FindParentOfType<ExpansionP2pMarketTradersConfig>());
                    };
                    ShowHandler(control, typeof(ExpansionP2pMarketTradersConfig), ExpansionP2PMarketTraderConfig, selected);
                    SetupP2PTraderSpawnPositions(ExpansionP2PMarketTraderConfig, node);
                    _mapControl.EnsureVisible(new PointF(ExpansionP2PMarketTraderConfig.m_Position.X, ExpansionP2PMarketTraderConfig.m_Position.Z));
                },
                //Personal Storage
                ["ExpansionPersonalStorageConfigPOSandOri"] = (node, selected) =>
                {
                    ExpansionPersonalStorageConfig ExpansionPersonalStorageConfig = node.Parent.Tag as ExpansionPersonalStorageConfig;
                    var control = new ExpasnionPersonalStorageContainerSpawnInfoControl();
                    control.PositionChanged += (updatedPos) =>
                    {
                        _mapControl.ClearDrawables(); ;
                        DrawbaseExpansionPersonalStoragePositions(node.FindParentOfType<ExpansionPersonalStorageContainersConfig>());
                    };
                    control.OrientationChanged += (updatedOrientation) =>
                    {
                        _mapControl.ClearDrawables(); ;
                        DrawbaseExpansionPersonalStoragePositions(node.FindParentOfType<ExpansionPersonalStorageContainersConfig>());
                    };
                    ShowHandler(control, typeof(ExpansionPersonalStorageContainersConfig), ExpansionPersonalStorageConfig, selected);
                    SetupExpansionPersonalStorageSpawnPositions(ExpansionPersonalStorageConfig, node);
                    _mapControl.EnsureVisible(new PointF(ExpansionPersonalStorageConfig.Position.X, ExpansionPersonalStorageConfig.Position.Z));
                },
                ["ExpansionPersonalStorageConfigGeneral"] = (node, selected) =>
                {
                    ExpansionPersonalStorageConfig ExpansionPersonalStorageConfig = node.Parent.Tag as ExpansionPersonalStorageConfig;
                    ShowHandler<IUIHandler>(new ExpasnionPersonalStorageContainerGeneralControl(), typeof(ExpansionPersonalStorageContainersConfig), ExpansionPersonalStorageConfig, selected);
                },
                //Raid
                ["RaidExplosives"] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(new ExpansionRaidSettingsExplosionsControl(), typeof(ExpansionRaidConfig), _expansionManager.ExpansionRaidConfig.Data, selected);
                },
                ["RaidBarbedWire"] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(new ExpansionRaidSettingsBarbedWireControl(), typeof(ExpansionRaidConfig), _expansionManager.ExpansionRaidConfig.Data, selected);

                },
                ["RaidSafes"] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(new ExpansionRaidSettingsSafesControl(), typeof(ExpansionRaidConfig), _expansionManager.ExpansionRaidConfig.Data, selected);
                },
                ["RaidContainers"] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(new expansionRaidContainersControl(), typeof(ExpansionRaidConfig), _expansionManager.ExpansionRaidConfig.Data, selected);
                },
                ["RaidLocks"] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(new ExpansionRaidSettingsLockControl(), typeof(ExpansionRaidConfig), _expansionManager.ExpansionRaidConfig.Data, selected);
                },
                //Spawn
                ["MaleLoadouts"] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(new ExpansionSpawnGearLoadoutsControl(), typeof(ExpansionSpawnConfig), _expansionManager.ExpansionSpawnConfig.Data, selected);
                },
                ["FemaleLoadouts"] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(new ExpansionSpawnGearLoadoutsControl(), typeof(ExpansionSpawnConfig), _expansionManager.ExpansionSpawnConfig.Data, selected);
                },
                //Vehicles
                ["VehicleSettingsGeneral"] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(new ExpansionVehicleSettingsGeneralControl(), typeof(ExpansionVehiclesConfig), _expansionManager.ExpansionVehiclesConfig.Data, selected);
                },
                ["VehicleSettingsCovers"] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(new ExpansionVehicleSettingsCoversControl(), typeof(ExpansionVehiclesConfig), _expansionManager.ExpansionVehiclesConfig.Data, selected);
                },
                ["VehicleSettingsKeys"] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(new ExpansionVehicleSettingsKeysControl(), typeof(ExpansionVehiclesConfig), _expansionManager.ExpansionVehiclesConfig.Data, selected);
                },
                ["VehicleSettingsLocks"] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(new ExpansionVehicleSettingsLocksControl(), typeof(ExpansionVehiclesConfig), _expansionManager.ExpansionVehiclesConfig.Data, selected);
                },
                ["VehicleSettingsCFCloud"] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(new ExpansionVehicleSettingsCFCloudControl(), typeof(ExpansionVehiclesConfig), _expansionManager.ExpansionVehiclesConfig.Data, selected);
                }
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
                    else if (node.Parent.Tag is ExpansionSafeZonePolygon)
                    {
                        ExpansionSettingsCM.Items.Clear();
                        ExpansionSettingsCM.Items.Add(removeSafeZonePolygonPointToolStripMenuItem);
                        ExpansionSettingsCM.Items.Add(new ToolStripSeparator());
                        ExpansionSettingsCM.Items.Add(moveSafeZonePolygonPointUpToolStripMenuItem);
                        ExpansionSettingsCM.Items.Add(moveSafeZonePolygonPointDownToolStripMenuItem);
                        ExpansionSettingsCM.Show(Cursor.Position);
                    }
                    else if (node.Parent.Tag is ExpansionSpawnLocation)
                    {
                        ExpansionSettingsCM.Items.Clear();
                        ExpansionSettingsCM.Items.Add(removeSpawnPointToolStripMenuItem);
                        ExpansionSettingsCM.Items.Add(new ToolStripSeparator());
                        ExpansionSettingsCM.Items.Add(moveSpawnPointUpToolStripMenuItem);
                        ExpansionSettingsCM.Items.Add(moveSpawnPointDownToolStripMenuItem);
                        ExpansionSettingsCM.Show(Cursor.Position);
                    }
                    else if (node.Parent.Tag.ToString() == "expansionMarketTraderMapWaypoints")
                    {
                        ExpansionSettingsCM.Items.Clear();
                        ExpansionSettingsCM.Items.Add(removeTraderNPCWaypointToolStripMenuItem);
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
                },
                //Damage
                [typeof(ExplosiveProjectiles)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeExplosiveProjectileToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //Map
                [typeof(ExpansionServerMarkerData)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeServerMarkerToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //MarketSettings
                [typeof(ExpansionMarketSpawnPosition)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeVehicleSpawnToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(ExpansionNotificationSchedule)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeNotificationToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //Market Categories
                [typeof(ExpansionMarketCategoryConfig)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewFolderToolStripMenuItem);
                    ExpansionSettingsCM.Items.Add(new ToolStripSeparator());
                    ExpansionSettingsCM.Items.Add(addNewMarketCategoryFileToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(ExpansionMarketCategory)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeMarketCategoryFileToolStripMenuItem);
                    ExpansionSettingsCM.Items.Add(new ToolStripSeparator());
                    ExpansionSettingsCM.Items.Add(moveCategoryToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(ExpansionMarketItem)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeMarketItemToolStripMenuItem);
                    ExpansionSettingsCM.Items.Add(new ToolStripSeparator());
                    ExpansionSettingsCM.Items.Add(moveMarketItemToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //Market Traders
                [typeof(ExpansionMarketTraderConfig)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewTraderFileToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(ExpansionMarketTrader)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(previewTraderToolStripMenuItem);
                    ExpansionSettingsCM.Items.Add(new ToolStripSeparator());
                    ExpansionSettingsCM.Items.Add(removeTraderFileToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(ExpansionMarketTraderCategory)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeCategoryFromTraderToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(ExpansionMarketTraderItem)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeItemFromTraderToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //Market Zones
                [typeof(ExpansionMarketTraderZoneConfig)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewTraderZoneToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(ExpansionMarketTraderZone)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeTraderZoneToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //Tradermaps
                [typeof(ExpansionMarketTraderMapsConfig)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewTraderMapFIleToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(ExpansionTraderMaps)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(checkNPCIsInAZoneToolStripMenuItem);
                    ExpansionSettingsCM.Items.Add(new ToolStripSeparator());
                    ExpansionSettingsCM.Items.Add(removeTraderNPCToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(ExpansionMarketTraderNpcs)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeTraderMapFileToolStripMenuItem);
                    ExpansionSettingsCM.Items.Add(new ToolStripSeparator());
                    ExpansionSettingsCM.Items.Add(addNewTraderNPCToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(TraderNPCItem)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeTraderNPCItemToolStripMenuItem);
                    ExpansionSettingsCM.Items.Add(new ToolStripSeparator());
                    ExpansionSettingsCM.Items.Add(addNPCTraderNPCAttachmentToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //Missions
                [typeof(ExpansionMissionEventAirdrop)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeMissionToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(ExpansionMissionEventContaminatedArea)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeMissionToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(ExpansionMissionEventHeliCrash)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeMissionToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(ExpansionMissionsConfig)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewAirdropMissionToolStripMenuItem);
                    ExpansionSettingsCM.Items.Add(new ToolStripSeparator());
                    ExpansionSettingsCM.Items.Add(addNewContaminatedMissionToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //P2P Market
                [typeof(ExpansionP2PMarketMenuCategory)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(RemoveP2PMarketCategoryToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(ExpansionP2PMarketMenuSubCategory)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(RemoveP2PMarketSubCategoryToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(ExpansionPersonalStorageMenuSubCategory)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(RemoveP2PMarketSubCategoryToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(ExpansionP2pMarketTradersConfig)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewP2PTraderToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(ExpansionP2PMarketTraderConfig)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeP2PTraderToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //Personal Storage
                [typeof(ExpansionPersonalStorageContainersConfig)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewPersonalStorageConfigToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(ExpansionPersonalStorageConfig)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removePersonalStorageConfigToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //Raid
                [typeof(ExpansionRaidSchedule)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(RemoveRaidScheduleToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //safezone
                [typeof(ExpansionSafeZoneCircle)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(RemovesafeZoneCircleZoneToolStripmenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(ExpansionSafeZonePolygon)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(RemoveSafeZonePolygonZoneToolStripMenuItem);
                    ExpansionSettingsCM.Items.Add(new ToolStripSeparator());
                    ExpansionSettingsCM.Items.Add(AddNewSafeZonePolygonPointtoolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(ExpansionSafeZoneCylinder)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(RemoveSafeZOneCylinderZoneToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //Spawn
                [typeof(ExpansionSpawnGearLoadouts)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeLoadoutfromSpawnLoadoutToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(ExpansionSpawnLocation)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeSpawnLocationToolStripMenuItem);
                    ExpansionSettingsCM.Items.Add(new ToolStripSeparator());
                    ExpansionSettingsCM.Items.Add(addNewSpawnPointToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                [typeof(ExpansionStartingGearItem)] = node =>
                {
                    if (node.Parent.Tag.ToString() == "PrimaryWeapon" ||
                            node.Parent.Tag.ToString() == "SecondaryWeapon")
                    {
                        ExpansionSettingsCM.Items.Clear();
                        ExpansionSettingsCM.Items.Add(removeStartingWeaponToolStripMenuItem);
                        ExpansionSettingsCM.Show(Cursor.Position);
                    }
                    else
                    {
                        ExpansionSettingsCM.Items.Clear();
                        ExpansionSettingsCM.Items.Add(removeStartingGearItemToolStripMenuItem);
                        ExpansionSettingsCM.Show(Cursor.Position);
                    }
                },
                //Vehicle
                [typeof(ExpansionVehiclesLockConfig)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeVehicleConfigToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //Quests
                [typeof(ExpansionQuestQuest)] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(questFlowPreviewToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
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
                },
                //chat
                ["BlacklistedWords"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewBlacklistedWordToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["BlacklistedWord"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeBlacklistedWordToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //Damage
                ["ExplosionTargets"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewExplosionTargetToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["ExplosionTarget"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeExplosionTargetToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["ExplosiveProjectiles"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewExplosiveProjectileToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //Garage
                ["EntityWhitelist"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewEntityWhitelistToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["EntityWhitelistItem"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeEntityWhitelistToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //Map
                ["ServerMarkersSettings"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewServerMarkerToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //Market Settings
                ["LargeVehicles"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewLargeVehicleToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["LargeVehicle"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeLargeVehicleToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["Currencies"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewCurrencyToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["Currency"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeCurrencyToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["VehicleKeys"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewVehicleKeyToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["VehicleKey"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeVehicleKeyToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["LandSpawnPositions"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewLandSpawnToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["AirSpawnPositions"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewAirSpawnToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["WatertSpawnPositions"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewWaterSpawnToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["TrainSpawnPositions"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewTrainSpawnToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["Notifications"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewNotificationToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //Market Categories
                ["MarketCategoryRelativePath"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewFolderToolStripMenuItem);
                    if (node.Nodes.Count == 0)
                        ExpansionSettingsCM.Items.Add(deleteFolderToolStripMenuItem);
                    ExpansionSettingsCM.Items.Add(new ToolStripSeparator());
                    ExpansionSettingsCM.Items.Add(addNewMarketCategoryFileToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["MarketItems"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewMarketItemToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["MarketItemSpawnAttachments"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addItemAttachmentToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["MarketItemSpawnAttachment"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeItemAttachmentToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["MarketItemVarients"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addItemVariantToolStripMenuItem);
                    ExpansionSettingsCM.Items.Add(new ToolStripSeparator());
                    ExpansionSettingsCM.Items.Add(addItemVariantAutoSearchToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["MarketItemVarient"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeItemVariantToolStripMenuItem);
                    ExpansionSettingsCM.Items.Add(new ToolStripSeparator());
                    ExpansionSettingsCM.Items.Add(createItemFromItemVariantToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //Market Traders
                ["TraderCategories"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addCategoryToTraderToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["TraderItems"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addItemToTraderToolStripMenuItem);
                    ExpansionSettingsCM.Items.Add(new ToolStripSeparator());
                    ExpansionSettingsCM.Items.Add(checkForMissingTraderItemsToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //market Zones
                ["TarderZoneStock"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(clearStockToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //Market Maps
                ["expansionMarketTraderMapWaypoints"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addTraderNPCWaypointToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["expansionMarketTraderMapItems"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addTraderNPCItemToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["expansionMarketTraderMapAttachment"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeTraderNPCAttachmentToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["expansionMarketTraderMapProperties"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addTraderNPCPropertyToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["expansionMarketTraderMapPropertiesName"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeTraderNPCPropertyToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["expansionMarketTraderMapPropertiesLoadout"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeTraderNPCPropertyToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["expansionMarketTraderMapPropertiesFaction"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeTraderNPCPropertyToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //P2P Market
                ["P2PExludedClassnames"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(AddNewP2PMarketExcludedToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["PersonalStorageExcludedClassNames"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(AddNewP2PMarketExcludedToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["PersonalStorageNewExludedItems"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(AddNewP2PMarketExcludedToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["P2PExcludedClassname"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeP2PMarketExcludedToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["PersonalStorageExcludedClassName"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeP2PMarketExcludedToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["PersonalStorageNewExludedItem"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeP2PMarketExcludedToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["P2PMenuCategories"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(AddNewP2PMarketCategoryToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["P2PSubCategories"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(AddNewP2PMarketSubCategoryToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["MenuCatsIncluded"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(AddNewP2PMarketIncludedToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["MenuCatIncluded"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(RemoveP2PMarketIncludedToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["MenuCatsExluded"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(AddNewP2PMarketExcludedToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["MenuCatExluded"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeP2PMarketExcludedToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //personal Storage
                ["StorageLevelExludedSlots"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewExcludedStorageSlotToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["StorageLevelExludedSlot"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeExcludedStorageSlotToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //Raid
                ["RaidSchedule"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewRaidScheduleToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["RaidExplosiveWhiteList"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(AddNewExplosiveWhitelistItemToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["RaidExplosiveWhiteListItem"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(RemoveExplosiveWhitelistItemToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["RaidBarbedWireRaidTools"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(AddNewBarbedWireRaidToolToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["RaidBarbedWireRaidTool"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(RemoveBarbedWireRaidToolToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["RaidSafeRaidTools"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(AddNewSafeRaidToolToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["RaidSafeRaidTool"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(RemoveSafeRaidToolToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["RaidContainerRaidTools"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(AddNewLockOnContainerRaidToolToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["RaidContainerRaidTool"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(RemoveLockOnContainerRaidToolToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["RaidLockRaidTools"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(AddNewLockRaidToolToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["RaidLockRaidTool"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(RemoveLockRaidToolToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //SafeZone
                ["CircleZones"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(AddNewSafeZoneCircleZoneToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["PolygonZones"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(AddNewsafeZonePolygonZoneToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["CylinderZones"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(AddNewSafeZoneCylinderZoneToolStripmenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["ForceSZCleanup_ExcludedItems"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(AddSafeZoneForceCleanUpItemsToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["ForceSZCleanup_ExcludedItem"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(RemoveSafeZoneForcecleanUpItemToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                //Spawn
                ["SpawnLocations"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewSpawnLocationToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["StartingClothing"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addStartingClothingItemToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["StartingClothingItem"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeStartingClothingItemToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["StartingGear"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addStartingGearItemToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["ExpansionStartingGearItemAttachments"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addStartingGearAttachmentToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["ExpansionStartingGearItemAttachment"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeStartingGearAttachmentToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["PrimaryWeapon"] = node =>
                {
                    if (_expansionManager.ExpansionSpawnConfig.Data.StartingGear.PrimaryWeapon == null)
                    {
                        ExpansionSettingsCM.Items.Clear();
                        ExpansionSettingsCM.Items.Add(addStartingWeaponToolStripMenuItem);
                        ExpansionSettingsCM.Show(Cursor.Position);
                    }
                },
                ["SecondaryWeapon"] = node =>
                {
                    if (_expansionManager.ExpansionSpawnConfig.Data.StartingGear.SecondaryWeapon == null)
                    {
                        ExpansionSettingsCM.Items.Clear();
                        ExpansionSettingsCM.Items.Add(addStartingWeaponToolStripMenuItem);
                        ExpansionSettingsCM.Show(Cursor.Position);
                    }
                },
                //Vehicle
                ["VehiclePickLockTools"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewToolToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["VehiclePickLockTool"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeToolToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["VehicleChangeLockTools"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewToolToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["VehicleChangeLockTool"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(removeToolToolStripMenuItem);
                    ExpansionSettingsCM.Show(Cursor.Position);
                },
                ["VehicleSettingsVehicleConfigs"] = node =>
                {
                    ExpansionSettingsCM.Items.Clear();
                    ExpansionSettingsCM.Items.Add(addNewVehicleConfigToolStripMenuItem);
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
        private void OpenFolderButton_Click(object sender, EventArgs e)
        {
            IConfigLoader IConfigLoaderFile = currentTreeNode.FindParentOfType<IConfigLoader>();
            if (IConfigLoaderFile != null)
            {
                string folderPath = IConfigLoaderFile.FilePath;
                if (!Directory.Exists(folderPath))
                    folderPath = Path.GetDirectoryName(folderPath);
                Process.Start(new ProcessStartInfo
                {
                    FileName = folderPath,
                    UseShellExecute = true
                });

            }
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

        private void AddFileToTree<TFile>(
            TreeNode parentNode,
            List<string> relativePath,
            TFile file,
            Func<TFile, TreeNode> createFileNode,
            bool expand = false)
        {
            if (parentNode == null)
                throw new ArgumentNullException(nameof(parentNode));
            if (file == null)
                throw new ArgumentNullException(nameof(relativePath));
            if (createFileNode == null)
                throw new ArgumentNullException(nameof(createFileNode));

            TreeNode currentNode = parentNode;

            if (relativePath != null && relativePath.Count > 0)
            {
                for (int i = 0; i < relativePath.Count; i++)
                {
                    string part = relativePath[i];
                    TreeNode folderNode = currentNode.Nodes
                        .Cast<TreeNode>()
                        .FirstOrDefault(n => n.Text.Equals(part, StringComparison.OrdinalIgnoreCase));

                    if (folderNode == null)
                    {
                        folderNode = new TreeNode(part)
                        {
                            Tag = string.Join(Path.DirectorySeparatorChar.ToString(), relativePath.Take(i + 1))
                        };
                        Helpers.InsertNodeAlphabetically(currentNode.Nodes, folderNode);
                    }

                    currentNode = folderNode;

                    if (expand)
                        currentNode.Expand();
                }
            }
            TreeNode fileNode = createFileNode(file);
            currentNode.Nodes.Add(fileNode);

            if (expand)
            {
                fileNode.Expand();
                ExpansionTV.SelectedNode = fileNode;
            }
        }

        private void BuildTreeview()
        {
            ExpansionTV.Nodes.Clear();
            TreeNode rootNode = new TreeNode("Expansion Mod")
            {
                Tag = "RootNode",
            };


            TreeNode AIrootNode = new TreeNode("AI")
            {
                Tag = "AIrootNode"
            };
            AddFileToTree(AIrootNode, null, _expansionManager.ExpansionAIConfig, CreateExpansionAIConfigNodes);
            AddFileToTree(AIrootNode, null, _expansionManager.ExpansionLoadoutConfig, CreateExpansionLoadoutConfigNodes);
            AddFileToTree(AIrootNode, null, _expansionManager.ExpansionLootDropConfig, CrateLootDropConfigNodes);
            AddFileToTree(AIrootNode, null, _expansionManager.ExpansionAIPatrolConfig, CreateExpansionAIPatrolConfigNodes);
            AddFileToTree(AIrootNode, null, _expansionManager.ExpansionAILocationConfig, CreateExpansionAILocationConfigNodes);
            rootNode.Nodes.Add(AIrootNode);

            TreeNode MarketrootNode = new TreeNode("Market")
            {
                Tag = "MarketrootNode"
            };

            AddFileToTree(MarketrootNode, null, _expansionManager.ExpansionMarketSettingsConfig, CreateExpansionMarketSettingsConfig);
            AddFileToTree(MarketrootNode, null, _expansionManager.ExpansionMarketCategoryConfig, CreateExpansionMarketCategoryConfig);
            AddFileToTree(MarketrootNode, null, _expansionManager.ExpansionMarketTraderConfig, CreateExpansionMarketTraderConfig);
            AddFileToTree(MarketrootNode, null, _expansionManager.ExpansionMarketTraderZoneConfig, CreateExpansionMarketTraderZoneConfig);
            AddFileToTree(MarketrootNode, null, _expansionManager.ExpansionMarketTraderMapsConfig, CreateExpansionMarketTraderMapsConfig);

            rootNode.Nodes.Add(MarketrootNode);

            TreeNode MissionrootNode = new TreeNode("Missions")
            {
                Tag = "MissionsrootNode"
            };
            AddFileToTree(MissionrootNode, null, _expansionManager.ExpansionMissionConfig, CreateExpansionMissionConfig);
            AddFileToTree(MissionrootNode, null, _expansionManager.ExpansionAirdropConfig, CreateExpansionAirdropConfigNodes);
            AddFileToTree(MissionrootNode, null, _expansionManager.ExpansionMissionsConfig, CreateExpansionMissionsConfigNodes);

            rootNode.Nodes.Add(MissionrootNode);

            TreeNode p2pmarketrootNode = new TreeNode("P2P Market")
            {
                Tag = "p2pmarketNode"
            };
            AddFileToTree(p2pmarketrootNode, null, _expansionManager.ExpansionP2PMarketConfig, CreateExpansionP2PMarketConfig);
            AddFileToTree(p2pmarketrootNode, null, _expansionManager.ExpansionP2pMarketTradersConfig, CreateExpansionP2PMarketTradersConfig);
            rootNode.Nodes.Add(p2pmarketrootNode);

            TreeNode personalstoragerootNode = new TreeNode("Personal Storage")
            {
                Tag = "personalstoragerootNode"
            };
            AddFileToTree(personalstoragerootNode, null, _expansionManager.ExpansionPersonalStorageNewConfig, CreateExpansionPersonalStorageNewConfig);
            AddFileToTree(personalstoragerootNode, null, _expansionManager.ExpansionPersonalStorageConfig, CreateExpansionPersonalStorageConfig);
            AddFileToTree(personalstoragerootNode, null, _expansionManager.ExpansionPersonalStorageContainersConfig, CreateExpansionPersonalStorageConfigs);
            rootNode.Nodes.Add(personalstoragerootNode);


            TreeNode SettingsNode = new TreeNode("Settings")
            {
                Tag = "SettingsNode",
            };

            AddFileToTree(SettingsNode, null, _expansionManager.ExpansionBaseBuildingConfig, CreateExpansionBaseBuildingConfigNodes);
            AddFileToTree(SettingsNode, null, _expansionManager.ExpansionBookConfig, CreateExpansionBookConfigConfigNodes);
            AddFileToTree(SettingsNode, null, _expansionManager.ExpansionChatConfig, CreateExpansionChatConfigConfigNodes);
            AddFileToTree(SettingsNode, null, _expansionManager.ExpansionCoreConfig, CreateExpansionCoreConfigConfigNodes);
            AddFileToTree(SettingsNode, null, _expansionManager.ExpansionDamageSystemConfig, CreateExpansionDamageSystemConfigConfigNodes);
            AddFileToTree(SettingsNode, null, _expansionManager.ExpansionGarageConfig, CreateExpansionGarageConfigConfigNodes);
            AddFileToTree(SettingsNode, null, _expansionManager.ExpansionGeneralConfig, CreateExpansionGeneralConfigConfigNodes);
            AddFileToTree(SettingsNode, null, _expansionManager.ExpansionHardlineConfig, CreateExpansionHardlineConfigNodes);
            AddFileToTree(SettingsNode, null, _expansionManager.ExpansionLogsConfig, CreateExpansionLogsConfigConfigNodes);
            AddFileToTree(SettingsNode, null, _expansionManager.ExpansionMapConfig, CreateExpansionMapConfigNodes);
            AddFileToTree(SettingsNode, null, _expansionManager.ExpansionMonitoringConfig, CreateExpansionMonitoringConfig);
            AddFileToTree(SettingsNode, null, _expansionManager.ExpansionNameTagsConfig, CreateExpansionNameTagsConfig);
            AddFileToTree(SettingsNode, null, _expansionManager.ExpansionNotificationSchedulerConfig, CreateExpansionNotificationSchedulerConfigConfig);
            AddFileToTree(SettingsNode, null, _expansionManager.ExpansionNotificationConfig, CreateExpansionNotificationConfigConfig);
            AddFileToTree(SettingsNode, null, _expansionManager.ExpansionPartyConfig, CreateExpansionPartyConfigConfig);
            AddFileToTree(SettingsNode, null, _expansionManager.ExpansionPlayerListConfig, CreateExpansionPlayerListConfig);
            AddFileToTree(SettingsNode, null, _expansionManager.ExpansionRaidConfig, CreateExpansionRaidConfig);
            AddFileToTree(SettingsNode, null, _expansionManager.ExpansionSafeZoneConfig, CreateExpansionSafeZoneConfig);
            AddFileToTree(SettingsNode, null, _expansionManager.ExpansionSocialMediaConfig, CreateExpansionSocialMediaConfig);
            AddFileToTree(SettingsNode, null, _expansionManager.ExpansionSpawnConfig, CreateExpansionSpawnConfig);
            AddFileToTree(SettingsNode, null, _expansionManager.ExpansionTerritoryConfig, CreateExpansionTerritoryConfig);
            AddFileToTree(SettingsNode, null, _expansionManager.ExpansionVehiclesConfig, CreateExpansionVehiclesConfig);

            rootNode.Nodes.Add(SettingsNode);

            TreeNode QuestsrootNode = new TreeNode("Quests")
            {
                Tag = "QuestsrootNode"
            };
            AddFileToTree(QuestsrootNode, null, _expansionManager.ExpansionQuestConfig, CreateExpansionQuestConfig);
            AddFileToTree(QuestsrootNode, null, _expansionManager.ExpansionQuestPersistentServerDataConfig, CreateExpansionQuestPersistentServerDataConfig);
            AddFileToTree(QuestsrootNode, null, _expansionManager.ExpansionQuestNPCDataConfig, CreateExpansionQuestNPCDataConfig);
            AddFileToTree(QuestsrootNode, null, _expansionManager.ExpansionQuestQuestConfig, CreateExpansionQuestQuestConfig);
            AddFileToTree(QuestsrootNode, null, _expansionManager.ExpansionQuestObjectiveConfigConfig, CreateExpansionQuestObjectiveConfigConfig);
            rootNode.Nodes.Add(QuestsrootNode);

            ExpansionTV.Nodes.Add(rootNode);
        }

        //AI
        private TreeNode CreateExpansionLoadoutConfigNodes(ExpansionLoadoutConfig ef)
        {
            TreeNode AILoadoutConfigRootNode = new TreeNode("Loadouts")
            {
                Tag = ef
            };
            foreach (AILoadouts AILoadouts in ef.Items)
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
            foreach (AILootDrops AILootdrops in ef.Items)
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
            foreach (Vec3 v3 in pat.Waypoints)
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

        //Settings files
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
                    Tag = "ExpansionLootList"
                };
                alcnodes.Nodes.Add(alclnodes);

                acnodes.Nodes.Add(alcnodes);

            }
            EconomyRootNode.Nodes.Add(acnodes);
        }
        //AI
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
            TreeNode Blacklistedwordsnode = new TreeNode("Blacklisted Words")
            {
                Tag = "BlacklistedWords"
            };
            foreach (string s in ef.Data.BlacklistedWords)
            {
                Blacklistedwordsnode.Nodes.Add(new TreeNode(s)
                {
                    Tag = "BlacklistedWord"
                });
            }
            EconomyRootNode.Nodes.Add(Blacklistedwordsnode);
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
                    Tag = "ExplosionTarget"
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
                    Tag = ep
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
            TreeNode MappingNode = new TreeNode("Mapping")
            {
                Tag = ef.Data.Mapping
            };
            CreateMappingNodes(ef.Data.Mapping, MappingNode);
            EconomyRootNode.Nodes.Add(MappingNode);
        }
        private static void CreateMappingNodes(ExpansionMapping mapping, TreeNode mappingNode)
        {
            if (mapping.UseCustomMappingModule == 1)
            {
                TreeNode Newcustomnode = new TreeNode("Custom Mappings")
                {
                    Tag = "CustomMappings"
                };
                foreach (string s in mapping.Mapping)
                {
                    Newcustomnode.Nodes.Add(new TreeNode(s)
                    {
                        Tag = "CustomMapping"
                    });
                }
                mappingNode.Nodes.Add(Newcustomnode);
            }
            if (mapping.BuildingInteriors == 1)
            {
                TreeNode Newinteriornode = new TreeNode("Interiors")
                {
                    Tag = "Interiors"
                };
                foreach (string s in mapping.Interiors)
                {
                    Newinteriornode.Nodes.Add(new TreeNode(s)
                    {
                        Tag = "Interior"
                    });
                }
                mappingNode.Nodes.Add(Newinteriornode);
            }
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
        //Maps
        private TreeNode CreateExpansionMapConfigNodes(ExpansionMapConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateMapNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private static void CreateMapNodes(ExpansionMapConfig ef, TreeNode EconomyRootNode)
        {
            EconomyRootNode.Nodes.Add(new TreeNode("Map")
            {
                Tag = "MapSettings"
            });
            TreeNode ServerMarkerNode = new TreeNode("Server Markers")
            {
                Tag = "ServerMarkersSettings"
            };
            foreach (ExpansionServerMarkerData smd in ef.Data.ServerMarkers)
            {
                ServerMarkerNode.Nodes.Add(new TreeNode(smd.m_UID)
                {
                    Tag = smd
                });
            }
            EconomyRootNode.Nodes.Add(ServerMarkerNode);
            EconomyRootNode.Nodes.Add(new TreeNode("Personal Markers")
            {
                Tag = "PersonalMarkerSettings"
            });
            EconomyRootNode.Nodes.Add(new TreeNode("Compass")
            {
                Tag = "CompassSettings"
            });
        }
        //Market
        private TreeNode CreateExpansionMarketSettingsConfig(ExpansionMarketSettingsConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateMarketSettingsNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private static void CreateMarketSettingsNodes(ExpansionMarketSettingsConfig ef, TreeNode EconomyRootNode)
        {
            EconomyRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = ef.Data
            });
            EconomyRootNode.Nodes.Add(new TreeNode("Market Menu Colors")
            {
                Tag = ef.Data.MarketMenuColors
            });
            TreeNode LargeVehiclenodes = new TreeNode("Large Vehicle")
            {
                Tag = "LargeVehicles"
            };
            foreach (string position in ef.Data.LargeVehicles)
            {
                LargeVehiclenodes.Nodes.Add(new TreeNode(position)
                {
                    Tag = "LargeVehicle"
                });
            }
            EconomyRootNode.Nodes.Add(LargeVehiclenodes);
            TreeNode Currenciesnodes = new TreeNode("Currencies")
            {
                Tag = "Currencies"
            };
            foreach (string position in ef.Data.Currencies)
            {
                Currenciesnodes.Nodes.Add(new TreeNode(position)
                {
                    Tag = "Currency"
                });
            }
            EconomyRootNode.Nodes.Add(Currenciesnodes);
            TreeNode VehicleKeysnodes = new TreeNode("Vehicle Keys")
            {
                Tag = "VehicleKeys"
            };
            foreach (string position in ef.Data.VehicleKeys)
            {
                VehicleKeysnodes.Nodes.Add(new TreeNode(position)
                {
                    Tag = "VehicleKey"
                });
            }
            EconomyRootNode.Nodes.Add(VehicleKeysnodes);
            TreeNode spawnpointnodes = new TreeNode("Vehicle Market Spawn Positions")
            {
                Tag = "VehicleMarketSpawnPositions"
            };
            TreeNode Landspawnpointnodes = new TreeNode("Land Spawn Positions")
            {
                Tag = "LandSpawnPositions"
            };
            foreach (ExpansionMarketSpawnPosition position in ef.Data.LandSpawnPositions)
            {
                Landspawnpointnodes.Nodes.Add(new TreeNode(position.ToString())
                {
                    Tag = position
                });
            }
            spawnpointnodes.Nodes.Add(Landspawnpointnodes);

            TreeNode Airspawnpointnodes = new TreeNode("Air Spawn Positions")
            {
                Tag = "AirSpawnPositions"
            };
            foreach (ExpansionMarketSpawnPosition position in ef.Data.AirSpawnPositions)
            {
                Airspawnpointnodes.Nodes.Add(new TreeNode(position.ToString())
                {
                    Tag = position
                });
            }
            spawnpointnodes.Nodes.Add(Airspawnpointnodes);

            TreeNode Waterspawnpointnodes = new TreeNode("Water Spawn Positions")
            {
                Tag = "WatertSpawnPositions"
            };
            foreach (ExpansionMarketSpawnPosition position in ef.Data.WaterSpawnPositions)
            {
                Waterspawnpointnodes.Nodes.Add(new TreeNode(position.ToString())
                {
                    Tag = position
                });
            }
            spawnpointnodes.Nodes.Add(Waterspawnpointnodes);

            TreeNode Trainspawnpointnodes = new TreeNode("Train Spawn Positions")
            {
                Tag = "TrainSpawnPositions"
            };
            foreach (ExpansionMarketSpawnPosition position in ef.Data.TrainSpawnPositions)
            {
                Trainspawnpointnodes.Nodes.Add(new TreeNode(position.ToString())
                {
                    Tag = position
                });
            }
            spawnpointnodes.Nodes.Add(Trainspawnpointnodes);
            EconomyRootNode.Nodes.Add(spawnpointnodes);
        }
        private TreeNode CreateExpansionMarketCategoryConfig(ExpansionMarketCategoryConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode("Market Categories")
            {
                Tag = ef
            };
            foreach (ExpansionMarketCategory file in ef.Items)
            {
                CreateExpansionMarketCategoryNodes(file, EconomyRootNode);
            }
            return EconomyRootNode;
        }
        private static void CreateExpansionMarketCategoryNodes(ExpansionMarketCategory expansionMarketCategory, TreeNode economyRootNode)
        {
            TreeNode categoryNode = new TreeNode(expansionMarketCategory.FileName)
            {
                Tag = expansionMarketCategory
            };

            TreeNode itemsNode = new TreeNode("Items")
            {
                Tag = "MarketItems"
            };

            foreach (ExpansionMarketItem item in expansionMarketCategory.Items)
            {
                CreateMarketItemNode(itemsNode, item);
            }

            categoryNode.Nodes.Add(itemsNode);

            if (expansionMarketCategory.FolderParts.Count == 0)
            {
                Helpers.InsertNodeAlphabetically(economyRootNode.Nodes, categoryNode);
                return;
            }

            TreeNode currentNode = economyRootNode;

            for (int i = 0; i < expansionMarketCategory.FolderParts.Count; i++)
            {
                string part = expansionMarketCategory.FolderParts[i];

                TreeNode folderNode = currentNode.Nodes
                    .Cast<TreeNode>()
                    .FirstOrDefault(n => n.Text.Equals(part, StringComparison.OrdinalIgnoreCase) &&
                                         n.Tag is string tag &&
                                         tag.StartsWith("MarketCategoryRelativePath:", StringComparison.Ordinal));

                if (folderNode == null)
                {
                    folderNode = new TreeNode(part)
                    {
                        Tag = "MarketCategoryRelativePath:" + string.Join(
                            Path.AltDirectorySeparatorChar.ToString(),
                            expansionMarketCategory.FolderParts.Take(i + 1))
                    };

                    Helpers.InsertFolderNodeAtTop(currentNode.Nodes, folderNode);
                }

                currentNode = folderNode;
            }

            Helpers.InsertNodeAlphabetically(currentNode.Nodes, categoryNode);
        }
        private static TreeNode CreateMarketItemNode(TreeNode itemsNode, ExpansionMarketItem item)
        {
            TreeNode itemNode = new TreeNode(item.ClassName)
            {
                Tag = item
            };

            TreeNode attachmentNode = new TreeNode("Spawn Attachments")
            {
                Tag = "MarketItemSpawnAttachments"
            };

            foreach (string att in item.SpawnAttachments)
            {
                attachmentNode.Nodes.Add(new TreeNode(att)
                {
                    Tag = "MarketItemSpawnAttachment"
                });
            }
            itemNode.Nodes.Add(attachmentNode);

            TreeNode variantNode = new TreeNode("Varients")
            {
                Tag = "MarketItemVarients"
            };

            foreach (string varient in item.Variants)
            {
                variantNode.Nodes.Add(new TreeNode(varient)
                {
                    Tag = "MarketItemVarient"
                });
            }
            itemNode.Nodes.Add(variantNode);

            Helpers.InsertNodeAlphabetically(itemsNode.Nodes, itemNode);
            return itemNode;
        }
        private TreeNode CreateExpansionMarketTraderConfig(ExpansionMarketTraderConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode("Market Traders")
            {
                Tag = ef
            };
            foreach (ExpansionMarketTrader file in ef.Items)
            {
                CreateExpansionMarketTraderNodes(file, EconomyRootNode);
            }
            return EconomyRootNode;
        }
        private static void CreateExpansionMarketTraderNodes(ExpansionMarketTrader ExpansionMarketTrader, TreeNode economyRootNode)
        {
            TreeNode traderNode = new TreeNode(ExpansionMarketTrader.FileName)
            {
                Tag = ExpansionMarketTrader
            };
            CreateTraderNodes(ExpansionMarketTrader, traderNode);

            Helpers.InsertNodeAlphabetically(economyRootNode.Nodes, traderNode);
        }
        private static void CreateTraderNodes(ExpansionMarketTrader ExpansionMarketTrader, TreeNode traderNode)
        {
            TreeNode Currenciesnodes = new TreeNode("Currencies")
            {
                Tag = "Currencies"
            };
            foreach (string position in ExpansionMarketTrader.Currencies)
            {
                Currenciesnodes.Nodes.Add(new TreeNode(position)
                {
                    Tag = "Currency"
                });
            }
            traderNode.Nodes.Add(Currenciesnodes);

            TreeNode CategoryNodes = new TreeNode("Categories")
            {
                Tag = "TraderCategories"
            };
            foreach (ExpansionMarketTraderCategory Category in ExpansionMarketTrader.m_Categories)
            {
                CategoryNodes.Nodes.Add(new TreeNode(Category.ToString())
                {
                    Tag = Category
                });
            }
            traderNode.Nodes.Add(CategoryNodes);

            TreeNode itemsNode = new TreeNode("Items")
            {
                Tag = "TraderItems"
            };
            foreach (ExpansionMarketTraderItem TItem in ExpansionMarketTrader.m_Items)
            {
                itemsNode.Nodes.Add(new TreeNode(TItem.MarketItem.ClassName)
                {
                    Tag = TItem
                });
            }
            traderNode.Nodes.Add(itemsNode);
        }
        private TreeNode CreateExpansionMarketTraderZoneConfig(ExpansionMarketTraderZoneConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode("Market Zones")
            {
                Tag = ef
            };
            foreach (ExpansionMarketTraderZone file in ef.Items)
            {
                CreateExpansionMarketTraderZoneNodes(file, EconomyRootNode);
            }
            return EconomyRootNode;
        }
        private static void CreateExpansionMarketTraderZoneNodes(ExpansionMarketTraderZone ExpansionMarketTraderZone, TreeNode economyRootNode)
        {
            TreeNode zoneNode = new TreeNode(ExpansionMarketTraderZone.FileName)
            {
                Tag = ExpansionMarketTraderZone
            };
            createtraderzonenodes(zoneNode);
            Helpers.InsertNodeAlphabetically(economyRootNode.Nodes, zoneNode);
        }
        private static void createtraderzonenodes(TreeNode zoneNode)
        {
            TreeNode PositionNode = new TreeNode("Zone Area")
            {
                Tag = "TraderZoneArea"
            };
            zoneNode.Nodes.Add(PositionNode);
            TreeNode ZoneStockNode = new TreeNode("Zone Stock")
            {
                Tag = "TarderZoneStock"
            };
            zoneNode.Nodes.Add(ZoneStockNode);
        }
        private TreeNode CreateExpansionMarketTraderMapsConfig(ExpansionMarketTraderMapsConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode("Market NPCS")
            {
                Tag = ef
            };
            foreach (ExpansionMarketTraderNpcs file in ef.Items)
            {
                CreateExpansionMarketTraderMaps(file, EconomyRootNode);
            }
            return EconomyRootNode;
        }
        private static void CreateExpansionMarketTraderMaps(ExpansionMarketTraderNpcs ExpansionMarketTraderNpcs, TreeNode economyRootNode)
        {
            TreeNode NpcNode = new TreeNode(ExpansionMarketTraderNpcs.FileName)
            {
                Tag = ExpansionMarketTraderNpcs
            };
            CreateNPCNodes(NpcNode, ExpansionMarketTraderNpcs);
            Helpers.InsertNodeAlphabetically(economyRootNode.Nodes, NpcNode);
        }
        private static void CreateNPCNodes(TreeNode node, ExpansionMarketTraderNpcs traderFile)
        {
            foreach (ExpansionTraderMaps map in traderFile.Tradersmaps)
            {
                Helpers.InsertNodeAlphabetically(node.Nodes, CreateNPCSingleNodes(map));
            }
        }
        private static TreeNode CreateNPCSingleNodes(ExpansionTraderMaps map)
        {
            string typeLabel = map.IsAI ? "[Roaming AI]" : "[Static]";
            TreeNode classNameNode = new TreeNode($"{map.NpcClassName} ({map.TraderName}) {typeLabel}")
            {
                Tag = map
            };

            // --- Rotation ---
            TreeNode rotationNode = new TreeNode("Rotation")
            {
                Tag = map.Rotation
            };
            classNameNode.Nodes.Add(rotationNode);

            // --- Positions / Waypoints ---
            string posLabel = map.Positions.Count == 1 ? "Position" : "Waypoints";
            TreeNode positionsNode = new TreeNode(posLabel)
            {
                Tag = "expansionMarketTraderMapWaypoints"
            };
            foreach (Vec3 v3 in map.Positions)
            {
                positionsNode.Nodes.Add(new TreeNode(v3.ToString()) { Tag = v3 });
            }
            classNameNode.Nodes.Add(positionsNode);

            // --- Items & Attachments ---
            TreeNode itemsNode = new TreeNode("Items") { Tag = "expansionMarketTraderMapItems" };
            foreach (TraderNPCItem item in map.Items)
            {
                TreeNode itemNode = new TreeNode(item.ClassName) { Tag = item };
                foreach (var att in item.Attachments)
                {
                    itemNode.Nodes.Add(new TreeNode(att) { Tag = "expansionMarketTraderMapAttachment" });
                }
                itemsNode.Nodes.Add(itemNode);
            }
            classNameNode.Nodes.Add(itemsNode);

            // --- Special Properties ---
            TreeNode propertiesNode = new TreeNode("Properties") { Tag = "expansionMarketTraderMapProperties" };
            if (!string.IsNullOrEmpty(map.Special.Name))
                propertiesNode.Nodes.Add(new TreeNode("Name: " + map.Special.Name) { Tag = "expansionMarketTraderMapPropertiesName:" + map.Special.Name });
            if (!string.IsNullOrEmpty(map.Special.Loadout))
                propertiesNode.Nodes.Add(new TreeNode("Loadout: " + map.Special.Loadout) { Tag = "expansionMarketTraderMapPropertiesLoadout:" + map.Special.Loadout });
            if (!string.IsNullOrEmpty(map.Special.Faction))
                propertiesNode.Nodes.Add(new TreeNode("Faction: " + map.Special.Faction) { Tag = "expansionMarketTraderMapPropertiesFaction:" + map.Special.Faction });

            classNameNode.Nodes.Add(propertiesNode);
            return classNameNode;
        }

        //Mission
        private TreeNode CreateExpansionMissionConfig(ExpansionMissionSettingsConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateExpansionMissionConfigNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private static void CreateExpansionMissionConfigNodes(ExpansionMissionSettingsConfig ef, TreeNode EconomyRootNode)
        {
            EconomyRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = ef.Data
            });
        }
        //Missions
        private TreeNode CreateExpansionMissionsConfigNodes(ExpansionMissionsConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode("Missions")
            {
                Tag = ef
            };
            foreach (ExpansionMissionEventBase file in ef.Items)
            {
                CreateExpansionMissionsNodes(file, EconomyRootNode);
            }
            return EconomyRootNode;
        }
        private static void CreateExpansionMissionsNodes(ExpansionMissionEventBase MB, TreeNode economyRootNode)
        {
            ExpansionMissionEventBase MissionBase = MB;
            TreeNode missionNode = new TreeNode(MB.FileName)
            {
                Tag = MissionBase
            };
            if (MB is ExpansionMissionEventAirdrop ExpansionMissionEventAirdrop)
            {
                missionNode.Nodes.Add(new TreeNode(ExpansionMissionEventAirdrop.MissionName)
                {
                    Tag = "MissionAirdrop"
                });
                missionNode.Nodes.Add(new TreeNode($"Drop Location - {ExpansionMissionEventAirdrop.DropLocation.Name}")
                {
                    Tag = ExpansionMissionEventAirdrop.DropLocation
                });
                TreeNode alcinodes = new TreeNode("Infected")
                {
                    Tag = "AirdropContainersInfected"
                };
                missionNode.Nodes.Add(alcinodes);

                TreeNode alclnodes = new TreeNode("Loot")
                {
                    Tag = "ExpansionLootList"
                };
                missionNode.Nodes.Add(alclnodes);
            }
            else if (MB is ExpansionMissionEventContaminatedArea ExpansionMissionEventContaminatedArea)
            {
                missionNode.Nodes.Add(new TreeNode(ExpansionMissionEventContaminatedArea.MissionName)
                {
                    Tag = ExpansionMissionEventContaminatedArea.Data
                });
                missionNode.Nodes.Add(new TreeNode("General")
                {
                    Tag = "MissionContaminatedAreaGeneral"
                });
                if (ExpansionMissionEventContaminatedArea.Data != null)
                    missionNode.Nodes.Add(new TreeNode("Data")
                    {
                        Tag = "MissionContaminatedAreaData"
                    });
                if (ExpansionMissionEventContaminatedArea.PlayerData != null)
                    missionNode.Nodes.Add(new TreeNode("PlayerData") { Tag = ExpansionMissionEventContaminatedArea.PlayerData });
            }
            else if (MB is ExpansionMissionEventHeliCrash ExpansionMissionEventHeliCrash)
            {
                missionNode.Nodes.Add(new TreeNode(ExpansionMissionEventHeliCrash.MissionName)
                {
                    Tag = "MissionHelicrash"
                });
                missionNode.Nodes.Add(new TreeNode($"Crash Location - {ExpansionMissionEventHeliCrash.CrashLocation.Name}")
                {
                    Tag = ExpansionMissionEventHeliCrash.CrashLocation
                });
                if (ExpansionMissionEventHeliCrash.UseLootFramework == 1 ? true : false)
                {
                    TreeNode rewardnodes = new TreeNode("Rewards")
                    {
                        Tag = "HelicrashRewards"
                    };
                    missionNode.Nodes.Add(rewardnodes);
                }
                TreeNode alcinodes = new TreeNode("Infected")
                {
                    Tag = "HelicrashInfected"
                };
                missionNode.Nodes.Add(alcinodes);

                TreeNode alclnodes = new TreeNode("Loot")
                {
                    Tag = "HelicrashLoot"
                };
                missionNode.Nodes.Add(alclnodes);
            }
            economyRootNode.Nodes.Add(missionNode);
        }

        //Monitoring
        private TreeNode CreateExpansionMonitoringConfig(ExpansionMonitoringConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateExpansionMonitoringConfigNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private static void CreateExpansionMonitoringConfigNodes(ExpansionMonitoringConfig ef, TreeNode EconomyRootNode)
        {
            EconomyRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = ef.Data
            });
        }
        //NameTags
        private TreeNode CreateExpansionNameTagsConfig(ExpansionNameTagsConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateExpansionNameTagsConfiggNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private static void CreateExpansionNameTagsConfiggNodes(ExpansionNameTagsConfig ef, TreeNode EconomyRootNode)
        {
            EconomyRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = ef.Data
            });
        }
        //NotificationScheduler
        private TreeNode CreateExpansionNotificationSchedulerConfigConfig(ExpansionNotificationSchedulerConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateExpansionNotificationSchedulerConfigNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private static void CreateExpansionNotificationSchedulerConfigNodes(ExpansionNotificationSchedulerConfig ef, TreeNode EconomyRootNode)
        {
            EconomyRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = ef.Data
            });
            TreeNode Notificationsnodes = new TreeNode("Notifications")
            {
                Tag = "Notifications"
            };
            foreach (ExpansionNotificationSchedule ns in ef.Data.Notifications)
            {
                Notificationsnodes.Nodes.Add(new TreeNode(ns.Title)
                {
                    Tag = ns
                });

            }
            EconomyRootNode.Nodes.Add(Notificationsnodes);
        }
        //Notification
        private TreeNode CreateExpansionNotificationConfigConfig(ExpansionNotificationConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateExpansionNotificationConfigNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private static void CreateExpansionNotificationConfigNodes(ExpansionNotificationConfig ef, TreeNode EconomyRootNode)
        {
            EconomyRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = ef.Data
            });
        }
        //Party
        private TreeNode CreateExpansionPartyConfigConfig(ExpansionPartyConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateExpansionPartyConfigNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private static void CreateExpansionPartyConfigNodes(ExpansionPartyConfig ef, TreeNode EconomyRootNode)
        {
            EconomyRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = ef.Data
            });
        }
        // P2P Market
        private TreeNode CreateExpansionP2PMarketConfig(ExpansionP2PMarketConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateExpansionP2PMarketConfigNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private static void CreateExpansionP2PMarketConfigNodes(ExpansionP2PMarketConfig ef, TreeNode EconomyRootNode)
        {
            EconomyRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = ef.Data
            });
            TreeNode ExludedclassnamesNode = new TreeNode("Excluded Classnames")
            {
                Tag = "P2PExludedClassnames"
            };
            foreach (string s in ef.Data.ExcludedClassNames)
            {
                ExludedclassnamesNode.Nodes.Add(new TreeNode(s)
                {
                    Tag = "P2PExcludedClassname"
                });
            }
            EconomyRootNode.Nodes.Add(ExludedclassnamesNode);
            TreeNode MenuCategoriesNode = new TreeNode("Menu Categories")
            {
                Tag = "P2PMenuCategories"
            };
            foreach (ExpansionP2PMarketMenuCategory cat in ef.Data.MenuCategories)
            {
                MenuCategoriesNode.Nodes.Add(Createmenucatnodes(cat));
            }
            EconomyRootNode.Nodes.Add(MenuCategoriesNode);
        }
        private static TreeNode Createmenucatnodes(ExpansionP2PMarketMenuCategory cat)
        {
            TreeNode Menucatnoderoot = new TreeNode(cat.DisplayName)
            {
                Tag = cat
            };

            TreeNode IncludedclassnamesNode = new TreeNode("Included")
            {
                Tag = "MenuCatsIncluded"
            };
            foreach (string s in cat.Included)
            {
                IncludedclassnamesNode.Nodes.Add(new TreeNode(s)
                {
                    Tag = "MenuCatIncluded"
                });
            }
            Menucatnoderoot.Nodes.Add(IncludedclassnamesNode);
            TreeNode ExludedclassnamesNode = new TreeNode("Excluded")
            {
                Tag = "MenuCatsExluded"
            };
            foreach (string s in cat.Excluded)
            {
                ExludedclassnamesNode.Nodes.Add(new TreeNode(s)
                {
                    Tag = "MenuCatExluded"
                });
            }
            Menucatnoderoot.Nodes.Add(ExludedclassnamesNode);
            TreeNode SubCatNodes = new TreeNode("Sub Categories")
            {
                Tag = "P2PSubCategories"
            };
            foreach (ExpansionP2PMarketMenuSubCategory subcat in cat.SubCategories)
            {
                SubCatNodes.Nodes.Add(CreateSubCats(subcat));
            }
            Menucatnoderoot.Nodes.Add(SubCatNodes);
            return Menucatnoderoot;
        }
        private static TreeNode CreateSubCats(ExpansionP2PMarketMenuSubCategory subcat)
        {
            TreeNode Menucatnoderoot = new TreeNode(subcat.DisplayName)
            {
                Tag = subcat
            };
            TreeNode IncludedclassnamesNode = new TreeNode("Included")
            {
                Tag = "MenuCatsIncluded"
            };
            foreach (string s in subcat.Included)
            {
                IncludedclassnamesNode.Nodes.Add(new TreeNode(s)
                {
                    Tag = "MenuCatIncluded"
                });
            }
            Menucatnoderoot.Nodes.Add(IncludedclassnamesNode);
            TreeNode ExludedclassnamesNode = new TreeNode("Excluded")
            {
                Tag = "MenuCatsExluded"
            };
            foreach (string s in subcat.Excluded)
            {
                ExludedclassnamesNode.Nodes.Add(new TreeNode(s)
                {
                    Tag = "MenuCatExluded"
                });
            }
            Menucatnoderoot.Nodes.Add(ExludedclassnamesNode);
            return Menucatnoderoot;
        }
        private TreeNode CreateExpansionP2PMarketTradersConfig(ExpansionP2pMarketTradersConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode("P2P Traders")
            {
                Tag = ef
            };
            foreach (ExpansionP2PMarketTraderConfig file in ef.Items)
            {
                CreateExpansionP2PMarketTraderNodes(file, EconomyRootNode);
            }
            return EconomyRootNode;
        }
        private static void CreateExpansionP2PMarketTraderNodes(ExpansionP2PMarketTraderConfig ef, TreeNode EconomyRootNode)
        {
            TreeNode P2PTraderRootNode = new TreeNode(ef.FileName)
            {
                Tag = "P2PTraderFile"
            };
            P2PTraderRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = ef
            });
            P2PTraderRootNode.Nodes.Add(new TreeNode("Position and Orientation")
            {
                Tag = "P2PMarketTraderPOSandOri"
            });
            P2PTraderRootNode.Nodes.Add(new TreeNode("Vehicle Spawn")
            {
                Tag = ef.m_VehicleSpawnPosition
            });
            P2PTraderRootNode.Nodes.Add(new TreeNode("Water Spawn")
            {
                Tag = ef.m_WatercraftSpawnPosition
            });
            P2PTraderRootNode.Nodes.Add(new TreeNode("Air Spawn")
            {
                Tag = ef.m_AircraftSpawnPosition
            });
            TreeNode WaypointNodes = new TreeNode("Roaming Waypoints")
            {
                Tag = "P2PMarketTraderRoamingWaypoints"
            };
            foreach (Vec3 v3 in ef.m_Waypoints)
            {
                WaypointNodes.Nodes.Add(new TreeNode(v3.ToString())
                {
                    Tag = v3
                });
            }
            P2PTraderRootNode.Nodes.Add(WaypointNodes);
            TreeNode CurrenciesNodes = new TreeNode("Currencies")
            {
                Tag = "P2PMarketTraderCurrencies"
            };
            foreach (string cur in ef.m_Currencies)
            {
                CurrenciesNodes.Nodes.Add(new TreeNode(cur)
                {
                    Tag = "P2PMarketTraderCurrency"
                });
            }
            P2PTraderRootNode.Nodes.Add(CurrenciesNodes);
            EconomyRootNode.Nodes.Add(P2PTraderRootNode);
        }
        //Personal Storage New
        private TreeNode CreateExpansionPersonalStorageNewConfig(ExpansionPersonalStorageNewSettingsConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateExpansionPersonalStorageNewConfigNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private void CreateExpansionPersonalStorageNewConfigNodes(ExpansionPersonalStorageNewSettingsConfig ef, TreeNode EconomyRootNode)
        {
            EconomyRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = ef.Data
            });
            TreeNode ExludedclassnamesNode = new TreeNode("Excluded Items")
            {
                Tag = "PersonalStorageNewExludedItems"
            };
            foreach (string s in ef.Data.ExcludedItems)
            {
                ExludedclassnamesNode.Nodes.Add(new TreeNode(s)
                {
                    Tag = "PersonalStorageNewExludedItem"
                });
            }
            EconomyRootNode.Nodes.Add(ExludedclassnamesNode);
            TreeNode StorageLevelsNode = new TreeNode("Storage Levels")
            {
                Tag = "PersonalStorageNewStorageLevels"
            };
            foreach (KeyValuePair<int, ExpansionPersonalStorageLevel> lvs in ef.Data.StorageLevels)
            {
                TreeNode SLNode = new TreeNode("Storage Level - " + lvs.Key.ToString())
                {
                    Tag = lvs.Value
                };
                TreeNode ExcludedNode = new TreeNode("Excluded Slots")
                {
                    Tag = "StorageLevelExludedSlots"
                };
                foreach (string s in lvs.Value.ExcludedSlots)
                {
                    ExcludedNode.Nodes.Add(new TreeNode(s)
                    {
                        Tag = "StorageLevelExludedSlot"
                    });
                }
                SLNode.Nodes.Add(ExcludedNode);
                StorageLevelsNode.Nodes.Add(SLNode);
            }
            EconomyRootNode.Nodes.Add(StorageLevelsNode);
        }
        //Personal Storage
        private TreeNode CreateExpansionPersonalStorageConfig(ExpansionPersonalStorageSettingsConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateExpansionPersonalStorageConfigNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private void CreateExpansionPersonalStorageConfigNodes(ExpansionPersonalStorageSettingsConfig ef, TreeNode EconomyRootNode)
        {
            EconomyRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = ef.Data
            });
            TreeNode ExludedclassnamesNode = new TreeNode("Excluded ClassNames")
            {
                Tag = "PersonalStorageExcludedClassNames"
            };
            foreach (string s in ef.Data.ExcludedClassNames)
            {
                ExludedclassnamesNode.Nodes.Add(new TreeNode(s)
                {
                    Tag = "PersonalStorageExcludedClassName"
                });
            }
            EconomyRootNode.Nodes.Add(ExludedclassnamesNode);
            TreeNode MenuCategoriesNode = new TreeNode("Menu Categories")
            {
                Tag = "PersonalStorageMenuCategories"
            };
            foreach (ExpansionPersonalStorageMenuCategory cat in ef.Data.MenuCategories)
            {
                MenuCategoriesNode.Nodes.Add(Createmenucatnodes(cat));
            }
            EconomyRootNode.Nodes.Add(MenuCategoriesNode);
        }
        private static TreeNode Createmenucatnodes(ExpansionPersonalStorageMenuCategory cat)
        {
            TreeNode Menucatnoderoot = new TreeNode(cat.DisplayName)
            {
                Tag = cat
            };

            TreeNode IncludedclassnamesNode = new TreeNode("Included")
            {
                Tag = "MenuCatsIncluded"
            };
            foreach (string s in cat.Included)
            {
                IncludedclassnamesNode.Nodes.Add(new TreeNode(s)
                {
                    Tag = "MenuCatIncluded"
                });
            }
            Menucatnoderoot.Nodes.Add(IncludedclassnamesNode);
            TreeNode ExludedclassnamesNode = new TreeNode("Excluded")
            {
                Tag = "MenuCatsExluded"
            };
            foreach (string s in cat.Excluded)
            {
                ExludedclassnamesNode.Nodes.Add(new TreeNode(s)
                {
                    Tag = "MenuCatExluded"
                });
            }
            Menucatnoderoot.Nodes.Add(ExludedclassnamesNode);
            TreeNode SubCatNodes = new TreeNode("Sub Categories")
            {
                Tag = "P2PSubCategories"
            };
            foreach (ExpansionPersonalStorageMenuSubCategory subcat in cat.SubCategories)
            {
                SubCatNodes.Nodes.Add(CreateSubCats(subcat));
            }
            Menucatnoderoot.Nodes.Add(SubCatNodes);
            return Menucatnoderoot;
        }
        private static TreeNode CreateSubCats(ExpansionPersonalStorageMenuSubCategory subcat)
        {
            TreeNode Menucatnoderoot = new TreeNode(subcat.DisplayName)
            {
                Tag = subcat
            };
            TreeNode IncludedclassnamesNode = new TreeNode("Included")
            {
                Tag = "MenuCatsIncluded"
            };
            foreach (string s in subcat.Included)
            {
                IncludedclassnamesNode.Nodes.Add(new TreeNode(s)
                {
                    Tag = "MenuCatIncluded"
                });
            }
            Menucatnoderoot.Nodes.Add(IncludedclassnamesNode);
            TreeNode ExludedclassnamesNode = new TreeNode("Excluded")
            {
                Tag = "MenuCatsExluded"
            };
            foreach (string s in subcat.Excluded)
            {
                ExludedclassnamesNode.Nodes.Add(new TreeNode(s)
                {
                    Tag = "MenuCatExluded"
                });
            }
            Menucatnoderoot.Nodes.Add(ExludedclassnamesNode);
            return Menucatnoderoot;
        }
        private TreeNode CreateExpansionPersonalStorageConfigs(ExpansionPersonalStorageContainersConfig ef)
        {
            TreeNode StorageConfigsNode = new TreeNode("Storage Configs")
            {
                Tag = ef
            };

            foreach (ExpansionPersonalStorageConfig file in ef.Items)
            {
                CreateExpansionPersonalStorageConfigsNodes(file, StorageConfigsNode);
            }
            return StorageConfigsNode;
        }
        private static void CreateExpansionPersonalStorageConfigsNodes(ExpansionPersonalStorageConfig ef, TreeNode EconomyRootNode)
        {
            TreeNode P2PTraderRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            P2PTraderRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = "ExpansionPersonalStorageConfigGeneral"
            });
            P2PTraderRootNode.Nodes.Add(new TreeNode("Position and Orientation")
            {
                Tag = "ExpansionPersonalStorageConfigPOSandOri"
            });


            EconomyRootNode.Nodes.Add(P2PTraderRootNode);
        }
        //Player List
        private TreeNode CreateExpansionPlayerListConfig(ExpansionPlayerListConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateExpansionPlayerListConfiggNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private static void CreateExpansionPlayerListConfiggNodes(ExpansionPlayerListConfig ef, TreeNode EconomyRootNode)
        {
            EconomyRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = ef.Data
            });
        }
        //Quest
        private TreeNode CreateExpansionQuestConfig(ExpansionQuestConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateExpansionQuestConfigNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private void CreateExpansionQuestConfigNodes(ExpansionQuestConfig ef, TreeNode EconomyRootNode)
        {
            EconomyRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = ef.Data
            });
        }
        private TreeNode CreateExpansionQuestPersistentServerDataConfig(ExpansionQuestPersistentServerDataConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreatexpansionQuestPersistentServerDataNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private void CreatexpansionQuestPersistentServerDataNodes(ExpansionQuestPersistentServerDataConfig ef, TreeNode economyRootNode)
        {
            TreeNode QMInode = new TreeNode("Quest Market Items")
            {
                Tag = "QuestPersitantMarketItems"
            };
            foreach (ExpansionQuestItemForMarket item in ef.Data.m_QuestMarketItems)
            {
                QMInode.Nodes.Add(new TreeNode(item.ClassName)
                {
                    Tag = item
                });
            }
            economyRootNode.Nodes.Add(QMInode);
        }
        private TreeNode CreateExpansionQuestNPCDataConfig(ExpansionQuestNPCDataConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode("Quest NPCS")
            {
                Tag = ef
            };
            CreateQuestNPCNodes(EconomyRootNode, ef);
            return EconomyRootNode;
        }
        private static void CreateQuestNPCNodes(TreeNode node, ExpansionQuestNPCDataConfig NPCdata)
        {
            foreach (ExpansionQuestNPCData map in NPCdata.MutableItems)
            {
                TreeNode classNameNode = new TreeNode($"{map.NPCName} ({map.ClassName}) {map.GetNPCType()}")
                {
                    Tag = map
                };

                // --- Rotation ---
                TreeNode rotationNode = new TreeNode("Orientation: " + map.Orientation.ToString())
                {
                    Tag = map.Orientation
                };
                classNameNode.Nodes.Add(rotationNode);

                // --- Positions / Waypoints ---
                TreeNode positionsNode = new TreeNode(map.GetISAI() ? "Waypoints" : "Position")
                {
                    Tag = "expansionQuestNPCMovement"
                };

                if (map.GetISAI())
                {
                    foreach (Vec3 v3 in map.Waypoints)
                    {
                        positionsNode.Nodes.Add(new TreeNode(v3.ToString()) { Tag = v3 });
                    }
                }
                else if (map.Position != null)
                {
                    positionsNode.Nodes.Add(new TreeNode(map.Position.ToString()) { Tag = map.Position });
                }

                classNameNode.Nodes.Add(positionsNode);


                Helpers.InsertNodeAlphabetically(node.Nodes, classNameNode);
            }
        }
        private TreeNode CreateExpansionQuestQuestConfig(ExpansionQuestQuestConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode("Quests")
            {
                Tag = ef
            };
            CreateQuestQuestNodes(EconomyRootNode, ef);
            return EconomyRootNode;
        }
        private void CreateQuestQuestNodes(TreeNode economyRootNode, ExpansionQuestQuestConfig ef)
        {
            var sortedQuests = ef.MutableItems
               .OrderBy(q => q.ID ?? int.MaxValue)
               .ToList();

            foreach (ExpansionQuestQuest quest in sortedQuests)
            {
                TreeNode questNode = new TreeNode($"Quest {quest.ID} : {quest.Title}")
                {
                    Tag = quest
                };

                questNode.Nodes.Add(new TreeNode("Basic Info") { Tag = quest });
                questNode.Nodes.Add(new TreeNode("Advanced") { Tag = quest });
                questNode.Nodes.Add(new TreeNode("Text / Dialogue") { Tag = quest });

                TreeNode flowNode = new TreeNode("Flow") { Tag = quest };

                TreeNode preQuestNode = new TreeNode($"Pre Quests") { Tag = quest };
                if (quest.PreQuestIDs != null && quest.PreQuestIDs.Count > 0)
                {
                    foreach (int preQuestId in quest.PreQuestIDs)
                    {
                        ExpansionQuestQuest linkedQuest = ef.MutableItems.FirstOrDefault(x => x.ID == preQuestId);

                        preQuestNode.Nodes.Add(new TreeNode(GetQuestReferenceText(preQuestId, linkedQuest))
                        {
                            Tag = new QuestReferenceNode { QuestID = (int)linkedQuest.ID }
                        });
                    }
                }

                TreeNode followUpNode = new TreeNode("Follow Up Quest") { Tag = quest };
                if (quest.FollowUpQuest.HasValue && quest.FollowUpQuest.Value != -1)
                {
                    ExpansionQuestQuest linkedQuest = ef.MutableItems.FirstOrDefault(x => x.ID == quest.FollowUpQuest.Value);

                    followUpNode.Nodes.Add(new TreeNode(GetQuestReferenceText(quest.FollowUpQuest.Value, linkedQuest))
                    {
                        Tag = new QuestReferenceNode { QuestID = (int)linkedQuest.ID }
                    });
                }

                flowNode.Nodes.Add(preQuestNode);
                flowNode.Nodes.Add(followUpNode);
                questNode.Nodes.Add(flowNode);

                TreeNode npcNode = new TreeNode("NPCs") { Tag = quest };

                TreeNode giverNode = new TreeNode($"Quest Givers") { Tag = quest };
                if (quest.QuestGiverIDs != null)
                {
                    foreach (int id in quest.QuestGiverIDs)
                        giverNode.Nodes.Add(new TreeNode($"{GetNPCReferenceText(id)}") { Tag = id });
                }

                TreeNode turnInNode = new TreeNode($"Quest Turn Ins") { Tag = quest };
                if (quest.QuestTurnInIDs != null)
                {
                    foreach (int id in quest.QuestTurnInIDs)
                        turnInNode.Nodes.Add(new TreeNode($"{GetNPCReferenceText(id)}") { Tag = id });
                }

                npcNode.Nodes.Add(giverNode);
                npcNode.Nodes.Add(turnInNode);
                questNode.Nodes.Add(npcNode);

                TreeNode objectivesNode = new TreeNode($"Objectives") { Tag = quest };
                if (quest.Objectives != null && quest.Objectives.Count > 0)
                {
                    foreach (Objectives objective in quest.Objectives)
                    {
                        objectivesNode.Nodes.Add(new TreeNode(GetObjectiveReferenceText(objective))
                        {
                            Tag = objective
                        });
                    }
                }

                questNode.Nodes.Add(objectivesNode);

                TreeNode questItemsNode = new TreeNode($"Quest Items") { Tag = quest };
                if (quest.QuestItems != null)
                {
                    foreach (ExpansionQuestItemConfig item in quest.QuestItems)
                    {
                        questItemsNode.Nodes.Add(new TreeNode(item.ClassName)
                        {
                            Tag = item
                        });
                    }
                }
                questNode.Nodes.Add(questItemsNode);

                TreeNode rewardsNode = new TreeNode($"Rewards") { Tag = quest };
                if (quest.Rewards != null)
                {
                    foreach (ExpansionQuestRewardConfig reward in quest.Rewards)
                    {
                        TreeNode rewardnode = new TreeNode(reward.ClassName)
                        {
                            Tag = reward
                        };
                        TreeNode Attachmentnode = new TreeNode("Attachments")
                        {
                            Tag = "QuestRewardAttachments"
                        };
                        foreach (string att in reward.Attachments)
                        {
                            Attachmentnode.Nodes.Add(new TreeNode(att)
                            {
                                Tag = "QuestRewardAttachment"
                            });
                        }
                        rewardnode.Nodes.Add(Attachmentnode);
                        rewardsNode.Nodes.Add(rewardnode);
                    }
                }
                questNode.Nodes.Add(rewardsNode);

                questNode.Nodes.Add(new TreeNode("Reputation / Faction") { Tag = quest });

                economyRootNode.Nodes.Add(questNode);

            }
        }
        private string GetQuestReferenceText(int questId, ExpansionQuestQuest linkedQuest)
        {
            if (linkedQuest == null)
                return $"🔗 Quest {questId}: Missing";

            string title = string.IsNullOrWhiteSpace(linkedQuest.Title) ? "Untitled Quest" : linkedQuest.Title;

            return $"🔗 Quest {questId}: {title}";
        }
        private string GetNPCReferenceText(int id)
        {
            var NPCfiles = _expansionManager.ExpansionQuestNPCDataConfig.MutableItems;
            ExpansionQuestNPCData npc = NPCfiles.FirstOrDefault(x => x.ID == id);

            return $"🔗 {npc.NPCName} ({npc.ClassName}) {npc.GetNPCType()}";
        }
        private string GetObjectiveReferenceText(Objectives objective)
        {
            if (objective == null)
                return "Objective: Missing";

            var objectiveFiles = _expansionManager.ExpansionQuestObjectiveConfigConfig.MutableItems;
            ExpansionQuestObjectiveConfig objectiveBase = objectiveFiles.FirstOrDefault(x => x.ID == objective.ID && x.ObjectiveType == objective.ObjectiveType);

            return $"🔗 {objectiveBase.ObjectiveType} : {objectiveBase.ObjectiveText}";
        }

        private TreeNode CreateExpansionQuestObjectiveConfigConfig(ExpansionQuestObjectiveConfigConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode("Quest Objectives")
            {
                Tag = ef
            };
            CreateQuestObjectiveNodes(EconomyRootNode, ef);
            return EconomyRootNode;
        }
        private void CreateQuestObjectiveNodes(TreeNode economyRootNode, ExpansionQuestObjectiveConfigConfig ef)
        {
            var categoryNodes = new Dictionary<Type, TreeNode>
            {
                { typeof(ExpansionQuestObjectiveActionConfig), new TreeNode("Action") { Tag = "Action" } },
                { typeof(ExpansionQuestObjectiveAICampConfig), new TreeNode("AICamp") { Tag = "AICamp" } },
                { typeof(ExpansionQuestObjectiveAIPatrolConfig), new TreeNode("AIPatrol") { Tag = "AIPatrol" } },
                { typeof(ExpansionQuestObjectiveAIEscortConfig), new TreeNode("AIVIP") { Tag = "AIVIP" } },
                { typeof(ExpansionQuestObjectiveCollectionConfig), new TreeNode("Collection") { Tag = "Collection" } },
                { typeof(ExpansionQuestObjectiveCraftingConfig), new TreeNode("Crafting") { Tag = "Crafting" } },
                { typeof(ExpansionQuestObjectiveDeliveryConfig), new TreeNode("Delivery") { Tag = "Delivery" } },
                { typeof(ExpansionQuestObjectiveTargetConfig), new TreeNode("Target") { Tag = "Target" } },
                { typeof(ExpansionQuestObjectiveTravelConfig), new TreeNode("Travel") { Tag = "Travel" } },
                { typeof(ExpansionQuestObjectiveTreasureHuntConfig), new TreeNode("TreasureHunt") { Tag = "TreasureHunt" } }
            };

            foreach (var objective in ef.MutableItems)
            {
                var objectiveType = objective.GetType();

                if (!categoryNodes.TryGetValue(objectiveType, out var categoryNode))
                    continue;

                var node = new TreeNode(objective.FileName) { Tag = objective };
                objective.BuildTree(node);

                categoryNode.Nodes.Add(node);

            }

            foreach (var node in categoryNodes.Values)
            {
                economyRootNode.Nodes.Add(node);
            }

        }


        //Raid
        private TreeNode CreateExpansionRaidConfig(ExpansionRaidConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateExpansionRaidConfigNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private void CreateExpansionRaidConfigNodes(ExpansionRaidConfig ef, TreeNode EconomyRootNode)
        {
            EconomyRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = ef.Data
            });
            TreeNode Explosivesroot = new TreeNode("Explosives")
            {
                Tag = "RaidExplosives"
            };
            TreeNode ExplosivesWhitelistBode = new TreeNode("Whitelist")
            {
                Tag = "RaidExplosiveWhiteList"
            };
            foreach (string edwl in ef.Data.ExplosiveDamageWhitelist)
            {
                ExplosivesWhitelistBode.Nodes.Add(new TreeNode(edwl)
                {
                    Tag = "RaidExplosiveWhiteListItem"
                });
            }
            Explosivesroot.Nodes.Add(ExplosivesWhitelistBode);
            EconomyRootNode.Nodes.Add(Explosivesroot);

            TreeNode BarbedWireRoot = new TreeNode("Barbed Wire")
            {
                Tag = "RaidBarbedWire"
            };
            TreeNode BarbedWireToolsNode = new TreeNode("Raid Tools")
            {
                Tag = "RaidBarbedWireRaidTools"
            };
            foreach (string edwl in ef.Data.BarbedWireRaidTools)
            {
                BarbedWireToolsNode.Nodes.Add(new TreeNode(edwl)
                {
                    Tag = "RaidBarbedWireRaidTool"
                });
            }
            BarbedWireRoot.Nodes.Add(BarbedWireToolsNode);
            EconomyRootNode.Nodes.Add(BarbedWireRoot);

            TreeNode SafesRoot = new TreeNode("Safes")
            {
                Tag = "RaidSafes"
            };
            TreeNode RaidSafeRaidToolsNode = new TreeNode("Raid Tools")
            {
                Tag = "RaidSafeRaidTools"
            };
            foreach (string edwl in ef.Data.SafeRaidTools)
            {
                RaidSafeRaidToolsNode.Nodes.Add(new TreeNode(edwl)
                {
                    Tag = "RaidSafeRaidTool"
                });
            }
            SafesRoot.Nodes.Add(RaidSafeRaidToolsNode);
            EconomyRootNode.Nodes.Add(SafesRoot);

            TreeNode ContainersRoot = new TreeNode("Containers")
            {
                Tag = "RaidContainers"
            };
            TreeNode RaidContainerRaidToolsNode = new TreeNode("Raid Tools")
            {
                Tag = "RaidContainerRaidTools"
            };
            foreach (string edwl in ef.Data.LockOnContainerRaidTools)
            {
                RaidContainerRaidToolsNode.Nodes.Add(new TreeNode(edwl)
                {
                    Tag = "RaidContainerRaidTool"
                });
            }
            ContainersRoot.Nodes.Add(RaidContainerRaidToolsNode);
            EconomyRootNode.Nodes.Add(ContainersRoot);

            TreeNode LocksRoot = new TreeNode("Locks")
            {
                Tag = "RaidLocks"
            };
            TreeNode RaidLockRaidToolsNode = new TreeNode("Raid Tools")
            {
                Tag = "RaidLockRaidTools"
            };
            foreach (string edwl in ef.Data.LockRaidTools)
            {
                RaidLockRaidToolsNode.Nodes.Add(new TreeNode(edwl)
                {
                    Tag = "RaidLockRaidTool"
                });
            }
            LocksRoot.Nodes.Add(RaidLockRaidToolsNode);
            EconomyRootNode.Nodes.Add(LocksRoot);

            TreeNode RadScheduleRoot = new TreeNode("Raid Schedule")
            {
                Tag = "RaidSchedule"
            };
            foreach (ExpansionRaidSchedule rs in ef.Data.Schedule)
            {
                RadScheduleRoot.Nodes.Add(new TreeNode(rs.ToString())
                {
                    Tag = rs
                });
            }
            EconomyRootNode.Nodes.Add(RadScheduleRoot);
        }
        //SafeZone
        private TreeNode CreateExpansionSafeZoneConfig(ExpansionSafeZoneConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateExpansionSafeZoneConfiNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private void CreateExpansionSafeZoneConfiNodes(ExpansionSafeZoneConfig ef, TreeNode EconomyRootNode)
        {
            EconomyRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = ef.Data
            });

            TreeNode CircleZonesRoot = new TreeNode("Circle Zones")
            {
                Tag = "CircleZones"
            };
            foreach (ExpansionSafeZoneCircle rs in ef.Data.CircleZones)
            {
                CircleZonesRoot.Nodes.Add(new TreeNode(rs.ToString())
                {
                    Tag = rs
                });
            }
            EconomyRootNode.Nodes.Add(CircleZonesRoot);

            TreeNode PolygonZonesRoot = new TreeNode("Polygon Zones")
            {
                Tag = "PolygonZones"
            };
            foreach (ExpansionSafeZonePolygon rs in ef.Data.PolygonZones)
            {
                TreeNode SFP = new TreeNode(rs.ToString())
                {
                    Tag = rs
                };
                foreach (Vec3 v3 in rs.Positions)
                {
                    SFP.Nodes.Add(new TreeNode(v3.GetString())
                    {
                        Tag = v3
                    });
                }
                PolygonZonesRoot.Nodes.Add(SFP);
            }
            EconomyRootNode.Nodes.Add(PolygonZonesRoot);

            TreeNode CylinderZonesRoot = new TreeNode("Cylinder Zones")
            {
                Tag = "CylinderZones"
            };
            foreach (ExpansionSafeZoneCylinder rs in ef.Data.CylinderZones)
            {
                CylinderZonesRoot.Nodes.Add(new TreeNode(rs.ToString())
                {
                    Tag = rs
                });
            }
            EconomyRootNode.Nodes.Add(CylinderZonesRoot);

            TreeNode ForceSZCleanup_ExcludedItemsRoot = new TreeNode("Force SZ Cleanup ExcludedItems")
            {
                Tag = "ForceSZCleanup_ExcludedItems"
            };
            foreach (string rs in ef.Data.ForceSZCleanup_ExcludedItems)
            {
                ForceSZCleanup_ExcludedItemsRoot.Nodes.Add(new TreeNode(rs.ToString())
                {
                    Tag = "ForceSZCleanup_ExcludedItem"
                });
            }
            EconomyRootNode.Nodes.Add(ForceSZCleanup_ExcludedItemsRoot);
        }
        //Social Media
        private TreeNode CreateExpansionSocialMediaConfig(ExpansionSocialMediaConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateExpansionSocialMediaConfigNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private void CreateExpansionSocialMediaConfigNodes(ExpansionSocialMediaConfig ef, TreeNode EconomyRootNode)
        {
            TreeNode NewsFeedTextsRoot = new TreeNode("News Feed Texts")
            {
                Tag = "NewsFeedTexts"
            };
            foreach (ExpansionNewsFeedTextSetting rs in ef.Data.NewsFeedTexts)
            {
                NewsFeedTextsRoot.Nodes.Add(new TreeNode(rs.ToString())
                {
                    Tag = rs
                });
            }
            EconomyRootNode.Nodes.Add(NewsFeedTextsRoot);

            TreeNode NewsFeedLinksRoot = new TreeNode("News Feed Links")
            {
                Tag = "NewsFeedLinks"
            };
            foreach (ExpansionNewsFeedLinkSetting rs in ef.Data.NewsFeedLinks)
            {
                NewsFeedLinksRoot.Nodes.Add(new TreeNode(rs.ToString())
                {
                    Tag = rs
                });
            }
            EconomyRootNode.Nodes.Add(NewsFeedLinksRoot);
        }
        //Spawn
        private TreeNode CreateExpansionSpawnConfig(ExpansionSpawnConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateExpansionSpawnConfigNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private void CreateExpansionSpawnConfigNodes(ExpansionSpawnConfig ef, TreeNode EconomyRootNode)
        {
            EconomyRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = ef.Data
            });

            TreeNode SpawnLocationsRoot = new TreeNode("Spawn Locations")
            {
                Tag = "SpawnLocations"
            };
            foreach (ExpansionSpawnLocation rs in ef.Data.SpawnLocations)
            {
                TreeNode SLNode = new TreeNode(rs.ToString())
                {
                    Tag = rs
                };
                foreach (Vec3 v3 in rs.Positions)
                {
                    SLNode.Nodes.Add(new TreeNode(v3.GetString())
                    {
                        Tag = v3
                    });
                }
                SpawnLocationsRoot.Nodes.Add(SLNode);
            }
            EconomyRootNode.Nodes.Add(SpawnLocationsRoot);

            EconomyRootNode.Nodes.Add(BuildStartingClothingTree(ef.Data.StartingClothing));
            EconomyRootNode.Nodes.Add(BuildStartingGearTree(ef.Data.StartingGear));

            TreeNode MaleLoadoutsRoot = new TreeNode("Male Loadouts")
            {
                Tag = "MaleLoadouts"
            };
            foreach (ExpansionSpawnGearLoadouts rs in ef.Data.MaleLoadouts)
            {
                MaleLoadoutsRoot.Nodes.Add(new TreeNode(rs.ToString())
                {
                    Tag = rs
                });
            }
            EconomyRootNode.Nodes.Add(MaleLoadoutsRoot);

            TreeNode FemaleLoadoutsRoot = new TreeNode("Female Loadouts")
            {
                Tag = "FemaleLoadouts"
            };
            foreach (ExpansionSpawnGearLoadouts rs in ef.Data.FemaleLoadouts)
            {
                FemaleLoadoutsRoot.Nodes.Add(new TreeNode(rs.ToString())
                {
                    Tag = rs
                });
            }
            EconomyRootNode.Nodes.Add(FemaleLoadoutsRoot);
        }
        private TreeNode BuildStartingClothingTree(ExpansionStartingClothing sc)
        {
            TreeNode root = new TreeNode("Starting Clothing")
            {
                Tag = sc
            };

            if (sc.EnableCustomClothing == 1)
            {
                // Loop through all list properties in the class
                var listProperties = typeof(ExpansionStartingClothing)
                    .GetProperties()
                    .Where(p => p.PropertyType == typeof(BindingList<string>));

                foreach (var prop in listProperties)
                {
                    BindingList<string> list = (BindingList<string>)prop.GetValue(sc);

                    // Create category node
                    TreeNode categoryNode = new TreeNode(prop.Name)
                    {
                        Tag = "StartingClothing"
                    };

                    if (list != null)
                    {
                        foreach (var item in list)
                        {
                            categoryNode.Nodes.Add(new TreeNode(item)
                            {
                                Tag = "StartingClothingItem"
                            });
                        }
                    }

                    root.Nodes.Add(categoryNode);
                }
            }

            return root;
        }
        private TreeNode BuildStartingGearTree(ExpansionStartingGear gear)
        {
            TreeNode root = new TreeNode("Starting Gear")
            {
                Tag = gear
            };

            if (gear.EnableStartingGear == 1)
            {
                var listProps = typeof(ExpansionStartingGear)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.PropertyType == typeof(BindingList<ExpansionStartingGearItem>));

                foreach (var prop in listProps)
                {
                    var list = (BindingList<ExpansionStartingGearItem>)prop.GetValue(gear);
                    var categoryNode = new TreeNode(prop.Name)
                    {
                        Tag = "StartingGear"
                    };
                    if (list == null || list.Count == 0)
                    {
                        root.Nodes.Add(categoryNode);
                        continue;
                    }
                    foreach (ExpansionStartingGearItem item in list)
                    {
                        TreeNode Itemnode = new TreeNode(item.ClassName)
                        {
                            Tag = item
                        };
                        TreeNode itemAttchmentsNode = new TreeNode("Attachments")
                        {
                            Tag = "ExpansionStartingGearItemAttachments"
                        };
                        foreach (string itemclassanme in item.Attachments)
                        {
                            itemAttchmentsNode.Nodes.Add(new TreeNode(itemclassanme)
                            {
                                Tag = "ExpansionStartingGearItemAttachment"
                            });
                        }
                        Itemnode.Nodes.Add(itemAttchmentsNode);
                        categoryNode.Nodes.Add(Itemnode);
                    }
                    root.Nodes.Add(categoryNode);
                }
                AddSingleItemNodeIfNotEmpty(root, gear.PrimaryWeapon, nameof(gear.PrimaryWeapon));
                AddSingleItemNodeIfNotEmpty(root, gear.SecondaryWeapon, nameof(gear.SecondaryWeapon));
            }
            return root;
        }
        private void AddSingleItemNodeIfNotEmpty(TreeNode root, ExpansionStartingGearItem item, string name)
        {
            TreeNode categoryNode = new TreeNode(name) { Tag = name };
            if (item != null && item.ClassName != null && item.Quantity != null && item.Attachments != null)
            {
                TreeNode Itemnode = new TreeNode((item.ClassName))
                {
                    Tag = item
                };
                TreeNode itemAttchmentsNode = new TreeNode("Attachments")
                {
                    Tag = "ExpansionStartingGearItemAttachments"
                };
                foreach (string itemclassanme in item.Attachments)
                {
                    itemAttchmentsNode.Nodes.Add(new TreeNode(itemclassanme)
                    {
                        Tag = "ExpansionStartingGearItemAttachment"
                    });
                }
                Itemnode.Nodes.Add(itemAttchmentsNode);
                categoryNode.Nodes.Add(Itemnode);
            }
            root.Nodes.Add(categoryNode);
        }
        //Territory
        private TreeNode CreateExpansionTerritoryConfig(ExpansionTerritoryConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateExpansionTerritoryConfigNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private void CreateExpansionTerritoryConfigNodes(ExpansionTerritoryConfig ef, TreeNode EconomyRootNode)
        {
            EconomyRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = ef.Data
            });
        }
        //Vehicle
        private TreeNode CreateExpansionVehiclesConfig(ExpansionVehiclesConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateExpansionVehiclesConfigNodes(ef, EconomyRootNode);
            return EconomyRootNode;
        }
        private void CreateExpansionVehiclesConfigNodes(ExpansionVehiclesConfig ef, TreeNode EconomyRootNode)
        {
            EconomyRootNode.Nodes.Add(new TreeNode("General")
            {
                Tag = "VehicleSettingsGeneral"
            });
            EconomyRootNode.Nodes.Add(new TreeNode("Covers")
            {
                Tag = "VehicleSettingsCovers"
            });
            EconomyRootNode.Nodes.Add(new TreeNode("Keys")
            {
                Tag = "VehicleSettingsKeys"
            });
            TreeNode VechileLockNodes = new TreeNode("Locks")
            {
                Tag = "VehicleSettingsLocks"
            };
            if (ef.Data.CanPickLock == 1 ? true : false)
            {
                TreeNode PickLockNodes = new TreeNode("Pick Lock Tools")
                {
                    Tag = "VehiclePickLockTools"
                };
                foreach (string tool in ef.Data.PickLockTools)
                {
                    PickLockNodes.Nodes.Add(new TreeNode(tool)
                    {
                        Tag = "VehiclePickLockTool"
                    });
                }
                VechileLockNodes.Nodes.Add(PickLockNodes);
            }
            if (ef.Data.CanChangeLock == 1 ? true : false)
            {
                TreeNode ChangeLockNodes = new TreeNode("Change Lock Tools")
                {
                    Tag = "VehicleChangeLockTools"
                };
                foreach (string tool in ef.Data.ChangeLockTools)
                {
                    ChangeLockNodes.Nodes.Add(new TreeNode(tool)
                    {
                        Tag = "VehicleChangeLockTool"
                    });
                }
                VechileLockNodes.Nodes.Add(ChangeLockNodes);
            }
            EconomyRootNode.Nodes.Add(VechileLockNodes);
            EconomyRootNode.Nodes.Add(new TreeNode("CF CLoud")
            {
                Tag = "VehicleSettingsCFCloud"
            });
            TreeNode VehicleConfigNodes = new TreeNode("Vehicle Configs")
            {
                Tag = "VehicleSettingsVehicleConfigs"
            };
            foreach (ExpansionVehiclesLockConfig lockConfig in ef.Data.VehiclesConfig)
            {
                VehicleConfigNodes.Nodes.Add(new TreeNode(lockConfig.ClassName)
                {
                    Tag = lockConfig
                });
            }
            EconomyRootNode.Nodes.Add(VehicleConfigNodes);
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
            _mapControl.MapsingleClicked -= MapControl_ServerMarkerDataSingleclicked;
            _mapControl.MapDoubleClicked -= MapControl_ServerMarkerDataDoubleclicked;
            _mapControl.MapsingleClicked -= MapControl_VehicleSpawnPositionSingleclicked;
            _mapControl.MapDoubleClicked -= MapControl_VehicleSpawnPositionDoubleclicked;
            _mapControl.MapsingleClicked -= MapControl_ServerSafeZoneSingleclicked;
            _mapControl.MapDoubleClicked -= MapControl_ServerSafeZoneDoubleclicked;
            _mapControl.MapsingleClicked -= MapControl_SpawnLocationPositionSingleclicked;
            _mapControl.MapDoubleClicked -= MapControl_SpawnLocationPositionDoubleclicked;
            _mapControl.MapsingleClicked -= MapControl_MissionLocationPositionSingleclicked;
            _mapControl.MapDoubleClicked -= MapControl_MissionLocationPositionDoubleclicked;
            _mapControl.MapsingleClicked -= MapControl_P2PTraderVehicleSpawnSingleclicked;
            _mapControl.MapDoubleClicked -= MapControl_P2PTraderVehicleSpawnDoubleclicked;
            _mapControl.MapsingleClicked -= MapControl_P2PTraderSpawnPositionsSingleclicked;
            _mapControl.MapDoubleClicked -= MapControl_P2PTraderSpawnPositionsDoubleclicked;
            _mapControl.MapsingleClicked -= MapControl_ExpansionPersonalStorageSpawnPositionsSingleclicked;
            _mapControl.MapDoubleClicked -= MapControl_ExpansionPersonalStorageSpawnPositionsDoubleclicked;
            _mapControl.MapsingleClicked -= MapControl_ExpansionTraderZonesPositionSingleclicked;
            _mapControl.MapDoubleClicked -= MapControl_ExpansionTraderZonePositionsDoubleclicked;
            _mapControl.MapDoubleClicked -= MapControl_ExpansionTraderMapsDoubleclicked;
            _mapControl.MapsingleClicked -= MapControl_ExpansionTraderMapsSingleclicked;
            _mapControl.MapDoubleClicked -= MapControl_AIEscortDoubleclicked;
            _mapControl.MapsingleClicked -= MapControl_AIEscortSingleclicked;
            _mapControl.MapDoubleClicked -= MapControl_TargetDoubleclicked;
            _mapControl.MapsingleClicked -= MapControl_TargetSingleclicked;
            _mapControl.MapDoubleClicked -= MapControl_TravelDoubleclicked;
            _mapControl.MapsingleClicked -= MapControl_TravelSingleclicked;
            _mapControl.MapDoubleClicked -= MapControl_TreasureHuntDoubleclicked;
            _mapControl.MapsingleClicked -= MapControl_TreasureHuntSingleclicked;

            // Reset "selected" state objects
            _selectedNoBuildZonePos = null;
            _selectedAIRoamingLocations = null;
            _selectedAIPatrol = null;
            _selectedAINOGoArea = null;
            _selectedServerMarkerData = null;
            _selectedVehicleSpanPosition = null;
            _selectedSafeZoneCircle = null;
            _selectedSafeZonePolygon = null;
            _selectedSafeZoneCylinder = null;
            _selectedSpawnLocation = null;
            _selectedAirdropLocation = null;
            _selectedExpansionMissionEventContaminatedArea = null;
            _selectedData = null;
            _selectedP2PTradervehiclespawn = null;
            _selectedPersonalStorageContainer = null;
            _selectedtraderZone = null;
            _selectedExpansionTraderMaps = null;
            _selectedTarget = null;
            _selectedTravel = null;
            _selectedTreasureHunt = null;
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

        void DispatchSpecificEditor(
            ExpansionQuestObjectiveConfig obj,
            List<TreeNode> selected)
        {
            var runtimeType = obj.GetType();

            if (_typeHandlers.TryGetValue(runtimeType, out var handler))
            {
                handler(currentTreeNode, selected);
                return;
            }
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
            else if (e.Node.Tag is string s)
            {
                if (s.Contains(":"))
                {
                    s = s.Split(':')[0]; // or Split(new[]{':'},2)
                }
                if (_stringContextMenus.TryGetValue(s, out var stringHandler))
                {
                    stringHandler.Invoke(e.Node);
                }
            }
        }

        #region mapstuff
        /// <summary>
        /// MapViewer Draw Mothods
        /// </summary>
        private ExpansionBuildNoBuildZone _selectedNoBuildZonePos;
        private ExpansionAIRoamingLocation _selectedAIRoamingLocations;
        private ExpansionAIPatrol _selectedAIPatrol;
        private ExpansionQuestObjectiveAIEscortConfig _selectedAIEscort;
        private ExpansionAINoGoArea _selectedAINOGoArea;
        private ExpansionServerMarkerData _selectedServerMarkerData;
        private ExpansionMarketSpawnPosition _selectedVehicleSpanPosition;
        private ExpansionSafeZoneCircle _selectedSafeZoneCircle;
        private ExpansionSafeZonePolygon _selectedSafeZonePolygon;
        private ExpansionSafeZoneCylinder _selectedSafeZoneCylinder;
        private ExpansionSpawnLocation _selectedSpawnLocation;
        private ExpansionAirdropLocation _selectedAirdropLocation;
        private ExpansionMissionEventContaminatedArea _selectedExpansionMissionEventContaminatedArea;
        private Data _selectedData;
        private Vec3 _selectedP2PTradervehiclespawn;
        private ExpansionP2PMarketTraderConfig _selectedP2PMarketTrader;
        private ExpansionPersonalStorageConfig _selectedPersonalStorageContainer;
        private ExpansionMarketTraderZone _selectedtraderZone;
        private ExpansionTraderMaps _selectedExpansionTraderMaps;
        private ExpansionQuestObjectiveTargetConfig _selectedTarget;
        private ExpansionQuestObjectiveTravelConfig _selectedTravel;
        private ExpansionQuestObjectiveTreasureHuntConfig _selectedTreasureHunt;
        private Vec3 _SelectedVec3;

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

                var tag = node.Parent?.Parent?.Parent?.Parent?.Tag;
                if (tag is ExpansionAIPatrolConfig patrolConfig)
                {
                    DrawbaseAIPatrols(patrolConfig);
                }
                else if (tag is ExpansionQuestObjectiveAICampConfig campConfig)
                {
                    DrawbaseObjectiveAICamp(campConfig);
                }
                else if (tag is ExpansionQuestObjectiveAIPatrolConfig questPatrolConfig)
                {
                    DrawbaseObjectiveAIPatrols(questPatrolConfig);
                }
            });
        }
        private void SetupAIEscort(ExpansionQuestObjectiveAIEscortConfig escort, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedAIEscort = escort;
                _mapControl.MapDoubleClicked += MapControl_AIEscortDoubleclicked;
                _mapControl.MapsingleClicked += MapControl_AIEscortSingleclicked;

                var tag = node.Parent?.Parent?.Tag;
                if (tag is ExpansionQuestObjectiveAIEscortConfig cfg)
                {
                    DrawbaseAIEscort(cfg);
                }
            });
        }
        private void SetupTarget(ExpansionQuestObjectiveTargetConfig target, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedTarget = target;
                _mapControl.MapDoubleClicked += MapControl_TargetDoubleclicked;
                _mapControl.MapsingleClicked += MapControl_TargetSingleclicked;

                DrawbaseTarget(target);
            });
        }
        private void SetupTravel(ExpansionQuestObjectiveTravelConfig travel, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedTravel = travel;
                _mapControl.MapDoubleClicked += MapControl_TravelDoubleclicked;
                _mapControl.MapsingleClicked += MapControl_TravelSingleclicked;

                DrawbaseTravel(travel);
            });
        }
        private void SetupTreasureHunt(ExpansionQuestObjectiveTreasureHuntConfig tresure, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedTreasureHunt = tresure;
                _SelectedVec3 = node.Tag as Vec3;
                _mapControl.MapDoubleClicked += MapControl_TreasureHuntDoubleclicked;
                _mapControl.MapsingleClicked += MapControl_TreasureHuntSingleclicked;

                DrawbaseTreasureHunt(tresure);
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
        private void SetupMapServerMarkers(ExpansionServerMarkerData smd, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedServerMarkerData = smd;
                _mapControl.MapsingleClicked += MapControl_ServerMarkerDataSingleclicked;
                _mapControl.MapDoubleClicked += MapControl_ServerMarkerDataDoubleclicked;

                var ExpansionMapConfig = node.Parent?.Parent?.Tag as ExpansionMapConfig;
                if (ExpansionMapConfig != null)
                    DrawbaseServerMarkerData(ExpansionMapConfig);
            });
        }
        private void SetupVehicleSpawnLocation(ExpansionMarketSpawnPosition expansionMarketSpawnPosition, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedVehicleSpanPosition = expansionMarketSpawnPosition;
                _mapControl.MapsingleClicked += MapControl_VehicleSpawnPositionSingleclicked;
                _mapControl.MapDoubleClicked += MapControl_VehicleSpawnPositionDoubleclicked;

                var ExpansionMarketSettingsConfig = node.Parent?.Parent?.Parent?.Tag as ExpansionMarketSettingsConfig;
                if (ExpansionMarketSettingsConfig != null)
                    DrawbaseVehicleSpawnPositions(ExpansionMarketSettingsConfig);
            });
        }
        private void SetupSafeZoneCircleMarkers(ExpansionSafeZoneCircle expansionSafeZoneCircle, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedSafeZoneCircle = expansionSafeZoneCircle;
                _mapControl.MapsingleClicked += MapControl_ServerSafeZoneSingleclicked;
                _mapControl.MapDoubleClicked += MapControl_ServerSafeZoneDoubleclicked;

                var ExpansionSafeZoneConfig = node.FindParentOfType<ExpansionSafeZoneConfig>();
                if (ExpansionSafeZoneConfig != null)
                    DrawbaseSafeZoneData(ExpansionSafeZoneConfig);
            });
        }
        private void SetupSafeZonePolygonMarkers(ExpansionSafeZonePolygon ExpansionSafeZonePolygon, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedSafeZonePolygon = ExpansionSafeZonePolygon;
                _mapControl.MapsingleClicked += MapControl_ServerSafeZoneSingleclicked;
                _mapControl.MapDoubleClicked += MapControl_ServerSafeZoneDoubleclicked;

                var ExpansionSafeZoneConfig = node.FindParentOfType<ExpansionSafeZoneConfig>();
                if (ExpansionSafeZoneConfig != null)
                    DrawbaseSafeZoneData(ExpansionSafeZoneConfig);
            });
        }
        private void SetupSafeZoneCylinderMarkers(ExpansionSafeZoneCylinder ExpansionSafeZoneCylinder, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedSafeZoneCylinder = ExpansionSafeZoneCylinder;
                _mapControl.MapsingleClicked += MapControl_ServerSafeZoneSingleclicked;
                _mapControl.MapDoubleClicked += MapControl_ServerSafeZoneDoubleclicked;

                var ExpansionSafeZoneConfig = node.FindParentOfType<ExpansionSafeZoneConfig>();
                if (ExpansionSafeZoneConfig != null)
                    DrawbaseSafeZoneData(ExpansionSafeZoneConfig);
            });
        }
        private void SetupSpawnLocationMarkers(ExpansionSpawnLocation ExpansionSpawnLocation, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedSpawnLocation = ExpansionSpawnLocation;
                _mapControl.MapsingleClicked += MapControl_SpawnLocationPositionSingleclicked;
                _mapControl.MapDoubleClicked += MapControl_SpawnLocationPositionDoubleclicked;

                var ExpansionSpawnConfig = node.FindParentOfType<ExpansionSpawnConfig>();
                if (ExpansionSpawnConfig != null)
                    DrawbaseSpawnLocationData(ExpansionSpawnConfig);
            });
        }
        private void SetupMissionMarkers(ExpansionAirdropLocation ExpansionAirdropLocation, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedAirdropLocation = ExpansionAirdropLocation;
                _mapControl.MapsingleClicked += MapControl_MissionLocationPositionSingleclicked;
                _mapControl.MapDoubleClicked += MapControl_MissionLocationPositionDoubleclicked;

                var ExpansionMissionsConfig = node.FindParentOfType<ExpansionMissionsConfig>();
                if (ExpansionMissionsConfig != null)
                    DrawbaseMissionMarkerData(ExpansionMissionsConfig);
            });
        }
        private void SetupEffectAreaMap(ExpansionMissionEventContaminatedArea ExpansionMissionEventContaminatedArea, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedExpansionMissionEventContaminatedArea = ExpansionMissionEventContaminatedArea;
                _mapControl.MapsingleClicked += MapControl_MissionLocationPositionSingleclicked;
                _mapControl.MapDoubleClicked += MapControl_MissionLocationPositionDoubleclicked;

                var ExpansionMissionsConfig = node.FindParentOfType<ExpansionMissionsConfig>();
                if (ExpansionMissionsConfig != null)
                    DrawbaseMissionMarkerData(ExpansionMissionsConfig);
            });
        }
        private void SetupP2PTraderVehicleSpawnMarkers(Vec3 v3, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedP2PTradervehiclespawn = v3;
                _mapControl.MapsingleClicked += MapControl_P2PTraderVehicleSpawnSingleclicked;
                _mapControl.MapDoubleClicked += MapControl_P2PTraderVehicleSpawnDoubleclicked;

                var ExpansionP2pMarketTradersConfig = node.FindParentOfType<ExpansionP2pMarketTradersConfig>();
                if (ExpansionP2pMarketTradersConfig != null)
                    DrawP2PTraderVehicleSpawnData(ExpansionP2pMarketTradersConfig);
            });
        }
        private void SetupP2PTraderSpawnPositions(ExpansionP2PMarketTraderConfig expansionP2PMarketTraderConfig, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedP2PMarketTrader = expansionP2PMarketTraderConfig;
                _mapControl.MapsingleClicked += MapControl_P2PTraderSpawnPositionsSingleclicked;
                _mapControl.MapDoubleClicked += MapControl_P2PTraderSpawnPositionsDoubleclicked;

                var ExpansionP2pMarketTradersConfig = node.FindParentOfType<ExpansionP2pMarketTradersConfig>();
                if (ExpansionP2pMarketTradersConfig != null)
                    DrawbaseP2PTraderSpawnPositions(ExpansionP2pMarketTradersConfig);
            });
        }
        private void SetupExpansionPersonalStorageSpawnPositions(ExpansionPersonalStorageConfig expansionPersonalStorageConfig, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedPersonalStorageContainer = expansionPersonalStorageConfig;
                _mapControl.MapsingleClicked += MapControl_ExpansionPersonalStorageSpawnPositionsSingleclicked;
                _mapControl.MapDoubleClicked += MapControl_ExpansionPersonalStorageSpawnPositionsDoubleclicked;

                var ExpansionPersonalStorageContainersConfig = node.FindParentOfType<ExpansionPersonalStorageContainersConfig>();
                if (ExpansionPersonalStorageContainersConfig != null)
                    DrawbaseExpansionPersonalStoragePositions(ExpansionPersonalStorageContainersConfig);
            });
        }
        private void SetupTraderZonePositions(ExpansionMarketTraderZone ExpansionMarketTraderZone, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedtraderZone = ExpansionMarketTraderZone;
                _mapControl.MapsingleClicked += MapControl_ExpansionTraderZonesPositionSingleclicked;
                _mapControl.MapDoubleClicked += MapControl_ExpansionTraderZonePositionsDoubleclicked;

                var ExpansionMarketTraderZoneConfig = node.FindParentOfType<ExpansionMarketTraderZoneConfig>();
                if (ExpansionMarketTraderZoneConfig != null)
                    DrawbaseTraderZonePositions(ExpansionMarketTraderZoneConfig);
            });
        }
        private void SetupTraderNPCPOsitions(ExpansionTraderMaps ExpansionTraderMaps, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedExpansionTraderMaps = ExpansionTraderMaps;
                _mapControl.MapsingleClicked += MapControl_ExpansionTraderMapsSingleclicked;
                _mapControl.MapDoubleClicked += MapControl_ExpansionTraderMapsDoubleclicked;

                var ExpansionMarketTraderMapsConfig = node.FindParentOfType<ExpansionMarketTraderMapsConfig>();
                if (ExpansionMarketTraderMapsConfig != null)
                    DrawTraderNPCPositions(ExpansionMarketTraderMapsConfig);
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
                var marker = new MarkerDrawable(new PointF((float)pos.Position.X, (float)pos.Position.Z), _mapControl.MapSize)
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

                for (int i = 0; i < patrol.Waypoints.Count; i++)
                {
                    Vec3 waypoints = patrol.Waypoints[i];

                    // Determine next waypoint index
                    bool isLast = i == patrol.Waypoints.Count - 1;
                    Vec3 nextWaypoint;

                    if ((behaviour == PatrolBehaviour.ALTERNATE || behaviour == PatrolBehaviour.HALT_OR_ALTERNATE || behaviour == PatrolBehaviour.ONCE) && isLast)
                    {
                        // Don't connect last to first for ALTERNATE
                        nextWaypoint = waypoints;
                    }
                    else
                    {
                        nextWaypoint = patrol.Waypoints[(i + 1) % patrol.Waypoints.Count];
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
        private void DrawbaseObjectiveAICamp(ExpansionQuestObjectiveAICampConfig campConfig)
        {
            _mapControl.RegisterDrawable(new AiPAtrolLegendDrawable(_mapControl.Size));
            foreach (ExpansionAIPatrol patrol in campConfig.AISpawns)
            {
                PatrolBehaviour behaviour = PatrolBehaviour.HALT;
                if (!string.IsNullOrEmpty(patrol.Behaviour))
                {
                    Enum.TryParse(patrol.Behaviour.Replace("-", "_"), true, out behaviour);
                }

                for (int i = 0; i < patrol.Waypoints.Count; i++)
                {
                    Vec3 waypoints = patrol.Waypoints[i];

                    // Determine next waypoint index
                    bool isLast = i == patrol.Waypoints.Count - 1;
                    Vec3 nextWaypoint;

                    if ((behaviour == PatrolBehaviour.ALTERNATE || behaviour == PatrolBehaviour.HALT_OR_ALTERNATE || behaviour == PatrolBehaviour.ONCE) && isLast)
                    {
                        // Don't connect last to first for ALTERNATE
                        nextWaypoint = waypoints;
                    }
                    else
                    {
                        nextWaypoint = patrol.Waypoints[(i + 1) % patrol.Waypoints.Count];
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
        private void DrawbaseObjectiveAIPatrols(ExpansionQuestObjectiveAIPatrolConfig questPatrolConfig)
        {
            _mapControl.RegisterDrawable(new AiPAtrolLegendDrawable(_mapControl.Size));
            PatrolBehaviour behaviour = PatrolBehaviour.HALT;
            if (!string.IsNullOrEmpty(questPatrolConfig.AISpawn.Behaviour))
            {
                Enum.TryParse(questPatrolConfig.AISpawn.Behaviour.Replace("-", "_"), true, out behaviour);
            }

            for (int i = 0; i < questPatrolConfig.AISpawn.Waypoints.Count; i++)
            {
                Vec3 waypoints = questPatrolConfig.AISpawn.Waypoints[i];

                // Determine next waypoint index
                bool isLast = i == questPatrolConfig.AISpawn.Waypoints.Count - 1;
                Vec3 nextWaypoint;

                if ((behaviour == PatrolBehaviour.ALTERNATE || behaviour == PatrolBehaviour.HALT_OR_ALTERNATE || behaviour == PatrolBehaviour.ONCE) && isLast)
                {
                    // Don't connect last to first for ALTERNATE
                    nextWaypoint = waypoints;
                }
                else
                {
                    nextWaypoint = questPatrolConfig.AISpawn.Waypoints[(i + 1) % questPatrolConfig.AISpawn.Waypoints.Count];
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

                if (_selectedAIPatrol == questPatrolConfig.AISpawn)
                    marker.Color = Color.LimeGreen;

                Vec3 v3 = currentTreeNode.Tag as Vec3;
                if (v3 == waypoints)
                    marker.Color = Color.Yellow;

                marker.text = (i == 0 ? questPatrolConfig.AISpawn.Name + "\n" : "") + (i + 1).ToString();

                _mapControl.RegisterDrawable(marker);
            }
        }
        private void DrawbaseAIEscort(ExpansionQuestObjectiveAIEscortConfig expansionQuestObjectiveAIEscortConfig)
        {
            var marker = new TextMarkerDrawable(new PointF((float)expansionQuestObjectiveAIEscortConfig.Position.X, (float)expansionQuestObjectiveAIEscortConfig.Position.Z), _mapControl.MapSize)
            {
                Color = Color.Red,
                Text = $"VIP Name - {expansionQuestObjectiveAIEscortConfig.NPCName}",
                TextPlacement = MarkerLabelPlacement.Top,
                TextBackground = true,
                TextBackgroundColor = Color.Blue
            };
            _mapControl.RegisterDrawable(marker);
        }
        private void DrawbaseTarget(ExpansionQuestObjectiveTargetConfig target)
        {
            var marker = new MarkerDrawable(new PointF((float)target.Position.X, (float)target.Position.Z), _mapControl.MapSize)
            {
                Color = Color.LimeGreen,
                Scaleradius = true,
                Radius = (float)target.MaxDistance
            };
            _mapControl.RegisterDrawable(marker);
        }
        private void DrawbaseTravel(ExpansionQuestObjectiveTravelConfig travel)
        {
            var marker = new MarkerDrawable(new PointF((float)travel.Position.X, (float)travel.Position.Z), _mapControl.MapSize)
            {
                Color = Color.LimeGreen,
                Scaleradius = true,
                Radius = (float)travel.MaxDistance
            };
            _mapControl.RegisterDrawable(marker);
        }
        private void DrawbaseTreasureHunt(ExpansionQuestObjectiveTreasureHuntConfig tresure)
        {
            MarkerDrawable? selectedMarker = null;
            foreach (Vec3 pos in tresure.Positions)
            {
                var marker = new MarkerDrawable(new PointF((float)pos.X, (float)pos.Z), _mapControl.MapSize)
                {
                    Color = Color.Red,
                    Radius = 10f
                };
                if (_SelectedVec3 == pos)
                {
                    marker.Color = Color.LimeGreen;
                    selectedMarker = marker;
                }
                else
                {
                    _mapControl.RegisterDrawable(marker);
                }
            }
            if (selectedMarker != null)
            {
                _mapControl.RegisterDrawable(selectedMarker);
            }
        }
        private void DrawbaseAILocationNoGoAreas(ExpansionAILocationConfig ExpansionAILocationConfig)
        {
            foreach (ExpansionAINoGoArea ExpansionAINoGoArea in ExpansionAILocationConfig.Data.NoGoAreas)
            {
                var marker = new MarkerDrawable(new PointF(ExpansionAINoGoArea.Position.X, ExpansionAINoGoArea.Position.Z), _mapControl.MapSize)
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
        private void DrawbaseServerMarkerData(ExpansionMapConfig ExpansionMapConfig)
        {
            foreach (ExpansionServerMarkerData ExpansionServerMarkerData in ExpansionMapConfig.Data.ServerMarkers)
            {
                var marker = new MarkerIconDrawable(new PointF(ExpansionServerMarkerData.m_Position[0], ExpansionServerMarkerData.m_Position[2]), _mapControl.MapSize)
                {
                    Image = GetIcon(ExpansionServerMarkerData)
                };
                if (_selectedServerMarkerData == ExpansionServerMarkerData)
                {
                    marker.IsSelected = true;
                }
                _mapControl.RegisterDrawable(marker);
            }
        }
        private Image GetIcon(ExpansionServerMarkerData ExpansionServerMarkerData)
        {
            var resourceName = $"ExpansionPlugin.Icons.{ExpansionServerMarkerData.m_IconName}.png".Replace("/", "");
            var stream = ResourceHelper.OpenEmbeddedStream(resourceName);
            if (stream != null)
            {
                Bitmap image = new Bitmap(Image.FromStream(stream));
                Image image2 = ResourceHelper.MultiplyColorToBitmap(image, Color.FromArgb((int)ExpansionServerMarkerData.m_Color), 200, true);
                Image image3 = resizeImage(image2, new Size(35, 35));
                return image3;
            }
            return null;
        }
        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }
        private void DrawbaseVehicleSpawnPositions(ExpansionMarketSettingsConfig expansionMarketSettingsConfig)
        {
            VehicleSpawnDrawable? selectedMarker = null;
            void AddPositions(IEnumerable<ExpansionMarketSpawnPosition> positions)
            {
                foreach (var pos in positions)
                {
                    var marker = new VehicleSpawnDrawable(
                        new PointF((float)pos.Position[0], (float)pos.Position[2]),
                        _mapControl.MapSize)
                    {
                        Color = Color.Red,
                        Orientation = pos.Orientation
                    };
                    if (_selectedVehicleSpanPosition == pos)
                    {
                        marker.Color = Color.LimeGreen;
                        // Defer registering; draw this one last
                        selectedMarker = marker;
                    }
                    else
                    {
                        _mapControl.RegisterDrawable(marker);
                    }
                }
            }
            AddPositions(expansionMarketSettingsConfig.Data.LandSpawnPositions);
            AddPositions(expansionMarketSettingsConfig.Data.AirSpawnPositions);
            AddPositions(expansionMarketSettingsConfig.Data.WaterSpawnPositions);
            AddPositions(expansionMarketSettingsConfig.Data.TrainSpawnPositions);
            if (selectedMarker != null)
            {
                _mapControl.RegisterDrawable(selectedMarker);
            }
        }
        private void DrawbaseSafeZoneData(ExpansionSafeZoneConfig ExpansionSafeZoneConfig)
        {
            foreach (ExpansionSafeZoneCircle pos in ExpansionSafeZoneConfig.Data.CircleZones)
            {
                var marker = new MarkerDrawable(new PointF((float)pos.Center.X, (float)pos.Center.Z), _mapControl.MapSize)
                {
                    Color = Color.Red,
                    Radius = (float)pos.Radius,
                    Scaleradius = true,
                    Fill = true,
                    FillAlpha = 80,
                    Shade = true
                };
                if (_selectedSafeZoneCircle == pos)
                {
                    marker.Color = Color.LimeGreen;
                }
                _mapControl.RegisterDrawable(marker);
            }
            foreach (ExpansionSafeZonePolygon pos in ExpansionSafeZoneConfig.Data.PolygonZones)
            {
                var myPointsList = new List<PointF>();
                foreach (Vec3 _vec3 in pos.Positions)
                {
                    myPointsList.Add(new PointF(_vec3.X, _vec3.Z));
                }
                var marker = new PolygonDrawable(myPointsList, _mapControl.MapSize)
                {
                    Color = Color.Red,
                    StrokeWidth = 2f,
                    Fill = true,
                    FillAlpha = 80,
                    Shade = true,
                    DrawVertices = true,
                    VertexRadius = 5f,
                    Selectedvec3 = currentTreeNode.Tag as Vec3

                };
                if (_selectedSafeZonePolygon == pos)
                {
                    marker.Color = Color.LimeGreen;
                }

                _mapControl.RegisterDrawable(marker);
            }

            foreach (ExpansionSafeZoneCylinder pos in ExpansionSafeZoneConfig.Data.CylinderZones)
            {
                var marker = new MarkerDrawable(new PointF((float)pos.Center.X, (float)pos.Center.Z), _mapControl.MapSize)
                {
                    Color = Color.Red,
                    Radius = (float)pos.Radius,
                    Scaleradius = true,
                    Fill = true,
                    FillAlpha = 80,
                    Shade = true
                };
                if (_selectedSafeZoneCylinder == pos)
                {
                    marker.Color = Color.LimeGreen;
                }
                _mapControl.RegisterDrawable(marker);
            }
        }
        private void DrawbaseSpawnLocationData(ExpansionSpawnConfig ExpansionSpawnConfig)
        {
            foreach (ExpansionSpawnLocation ExpansionSpawnLocation in ExpansionSpawnConfig.Data.SpawnLocations)
            {
                if (_selectedSpawnLocation == ExpansionSpawnLocation)
                {
                    foreach (Vec3 vec3 in ExpansionSpawnLocation.Positions)
                    {
                        if (currentTreeNode.Tag == vec3)
                        {
                            var marker = new MarkerDrawable(new PointF(vec3.X, vec3.Z), _mapControl.MapSize)
                            {
                                Color = Color.Yellow,
                                Radius = 8,
                                Scaleradius = false
                            };
                            _mapControl.RegisterDrawable(marker);
                        }
                        else
                        {
                            var marker = new MarkerDrawable(new PointF(vec3.X, vec3.Z), _mapControl.MapSize)
                            {
                                Color = Color.LimeGreen,
                                Radius = 8,
                                Scaleradius = false
                            };
                            _mapControl.RegisterDrawable(marker);
                        }
                    }
                }
                else
                {
                    foreach (Vec3 vec3 in ExpansionSpawnLocation.Positions)
                    {
                        var marker = new MarkerDrawable(new PointF(vec3.X, vec3.Z), _mapControl.MapSize)
                        {
                            Color = Color.Red,
                            Radius = 8,
                            Scaleradius = false
                        };
                        _mapControl.RegisterDrawable(marker);
                    }
                }
                var outline = new OutlineDrawable(ExpansionSpawnLocation.Positions, _mapControl.MapSize)
                {

                };
                _mapControl.RegisterDrawable(outline);
            }
        }
        private void DrawbaseMissionMarkerData(ExpansionMissionsConfig ExpansionMissionsConfig)
        {
            foreach (ExpansionMissionEventBase mb in ExpansionMissionsConfig.Items)
            {
                if (mb is ExpansionMissionEventAirdrop airdrop)
                {
                    var marker = new TextMarkerDrawable(new PointF((float)airdrop.DropLocation.x, (float)airdrop.DropLocation.z), _mapControl.MapSize)
                    {
                        Color = Color.Red,
                        Radius = (float)airdrop.DropLocation.Radius,
                        Scaleradius = true,
                        Shade = true,
                        Text = $"Airdrop - {airdrop.DropLocation.Name}",
                        TextPlacement = MarkerLabelPlacement.Top,
                        TextBackground = true,
                        TextBackgroundColor = Color.Blue
                    };
                    if (_selectedAirdropLocation == airdrop.DropLocation)
                    {
                        marker.Color = Color.LimeGreen;
                    }
                    _mapControl.RegisterDrawable(marker);
                }
                else if (mb is ExpansionMissionEventContaminatedArea contamiatedarea)
                {
                    var marker = new TextMarkerDrawable(new PointF((float)contamiatedarea.Data.Pos[0], (float)contamiatedarea.Data.Pos[2]), _mapControl.MapSize)
                    {
                        Color = Color.Red,
                        Radius = (float)contamiatedarea.Data.Radius,
                        Scaleradius = true,
                        Shade = true,
                        Text = $"Contaminated Area - {contamiatedarea.MissionName}",
                        TextPlacement = MarkerLabelPlacement.Top,
                        TextBackground = true,
                        TextBackgroundColor = Color.Aqua
                    };
                    if (_selectedExpansionMissionEventContaminatedArea == contamiatedarea)
                    {
                        marker.Color = Color.LimeGreen;
                    }
                    _mapControl.RegisterDrawable(marker);
                }
                else if (mb is ExpansionMissionEventHeliCrash ExpansionMissionEventHeliCrash)
                {
                    var marker = new TextMarkerDrawable(new PointF((float)ExpansionMissionEventHeliCrash.CrashLocation.x, (float)ExpansionMissionEventHeliCrash.CrashLocation.z), _mapControl.MapSize)
                    {
                        Color = Color.Red,
                        Radius = (float)ExpansionMissionEventHeliCrash.CrashLocation.Radius,
                        Scaleradius = true,
                        Shade = true,
                        Text = $"Crash Location - {ExpansionMissionEventHeliCrash.CrashLocation.Name}",
                        TextPlacement = MarkerLabelPlacement.Top,
                        TextBackground = true,
                        TextBackgroundColor = Color.BlueViolet
                    };
                    if (_selectedAirdropLocation == ExpansionMissionEventHeliCrash.CrashLocation)
                    {
                        marker.Color = Color.LimeGreen;
                    }
                    _mapControl.RegisterDrawable(marker);
                }
            }
        }
        private void DrawP2PTraderVehicleSpawnData(ExpansionP2pMarketTradersConfig ExpansionP2pMarketTradersConfig)
        {
            TextMarkerDrawable? selected_marker = null;
            foreach (ExpansionP2PMarketTraderConfig mb in ExpansionP2pMarketTradersConfig.Items)
            {
                var marker = new TextMarkerDrawable(new PointF((float)mb.m_VehicleSpawnPosition.X, (float)mb.m_VehicleSpawnPosition.Z), _mapControl.MapSize)
                {
                    Color = Color.Red,
                    Radius = 10f,
                    Scaleradius = false,
                    Shade = true,
                    Text = $"{mb.FileName}\nVehicle Spawn",
                    TextPlacement = MarkerLabelPlacement.Top,
                    TextBackground = true,
                    TextBackgroundColor = Color.BlueViolet
                };
                if (_selectedP2PTradervehiclespawn == mb.m_VehicleSpawnPosition)
                {
                    marker.Color = Color.LimeGreen;
                    selected_marker = marker;
                }
                else
                {
                    _mapControl.RegisterDrawable(marker);
                }
                marker = new TextMarkerDrawable(new PointF((float)mb.m_AircraftSpawnPosition.X, (float)mb.m_AircraftSpawnPosition.Z), _mapControl.MapSize)
                {
                    Color = Color.Red,
                    Radius = 10f,
                    Scaleradius = false,
                    Shade = true,
                    Text = $"{mb.FileName}\nAir Spawn",
                    TextPlacement = MarkerLabelPlacement.Top,
                    TextBackground = true,
                    TextBackgroundColor = Color.BlueViolet
                };
                if (_selectedP2PTradervehiclespawn == mb.m_AircraftSpawnPosition)
                {
                    marker.Color = Color.LimeGreen;
                    selected_marker = marker;
                }
                else
                {
                    _mapControl.RegisterDrawable(marker);
                }
                marker = new TextMarkerDrawable(new PointF((float)mb.m_WatercraftSpawnPosition.X, (float)mb.m_WatercraftSpawnPosition.Z), _mapControl.MapSize)
                {
                    Color = Color.Red,
                    Radius = 10f,
                    Scaleradius = false,
                    Shade = true,
                    Text = $"{mb.FileName}\nWater Spawn",
                    TextPlacement = MarkerLabelPlacement.Top,
                    TextBackground = true,
                    TextBackgroundColor = Color.BlueViolet
                };
                if (_selectedP2PTradervehiclespawn == mb.m_WatercraftSpawnPosition)
                {
                    marker.Color = Color.LimeGreen;
                    selected_marker = marker;
                }
                else
                {
                    _mapControl.RegisterDrawable(marker);
                }
            }
            if (selected_marker != null)
            {
                _mapControl.RegisterDrawable(selected_marker);
            }
        }
        private void DrawbaseP2PTraderSpawnPositions(ExpansionP2pMarketTradersConfig ExpansionP2pMarketTradersConfig)
        {
            TraderSpawnDrawable? selectedMarker = null;
            foreach (ExpansionP2PMarketTraderConfig ExpansionP2PMarketTraderConfig in ExpansionP2pMarketTradersConfig.Items)
            {
                var marker = new TraderSpawnDrawable(
                        new PointF((float)ExpansionP2PMarketTraderConfig.m_Position.X, (float)ExpansionP2PMarketTraderConfig.m_Position.Z),
                        _mapControl.MapSize)
                {
                    Color = Color.Red,
                    Orientation = ExpansionP2PMarketTraderConfig.m_Orientation.getfloatarray(),
                    Text = $"{ExpansionP2PMarketTraderConfig.FileName}",
                    TextPlacement = MarkerLabelPlacement.Top,
                    TextBackground = true,
                    TextBackgroundColor = Color.BlueViolet
                };

                if (_selectedP2PMarketTrader == ExpansionP2PMarketTraderConfig)
                {
                    marker.Color = Color.LimeGreen;
                    // Defer registering; draw this one last
                    selectedMarker = marker;
                }
                else
                {
                    _mapControl.RegisterDrawable(marker);
                }
            }
            if (selectedMarker != null)
            {
                _mapControl.RegisterDrawable(selectedMarker);
            }
        }
        private void DrawbaseExpansionPersonalStoragePositions(ExpansionPersonalStorageContainersConfig ExpansionPersonalStorageContainersConfig)
        {
            TraderSpawnDrawable? selectedMarker = null;
            foreach (ExpansionPersonalStorageConfig ExpansionPersonalStorageConfig in ExpansionPersonalStorageContainersConfig.Items)
            {
                var marker = new TraderSpawnDrawable(
                        new PointF((float)ExpansionPersonalStorageConfig.Position.X, (float)ExpansionPersonalStorageConfig.Position.Z),
                        _mapControl.MapSize)
                {
                    Color = Color.Red,
                    Orientation = ExpansionPersonalStorageConfig.Orientation.getfloatarray(),
                    Text = $"{ExpansionPersonalStorageConfig.FileName}",
                    TextPlacement = MarkerLabelPlacement.Top,
                    TextBackground = true,
                    TextBackgroundColor = Color.BlueViolet
                };
                if (_selectedPersonalStorageContainer == ExpansionPersonalStorageConfig)
                {
                    marker.Color = Color.LimeGreen;
                    // Defer registering; draw this one last
                    selectedMarker = marker;
                }
                else
                {
                    _mapControl.RegisterDrawable(marker);
                }
            }
            if (selectedMarker != null)
            {
                _mapControl.RegisterDrawable(selectedMarker);
            }
        }
        private void DrawbaseTraderZonePositions(ExpansionMarketTraderZoneConfig ExpansionMarketTraderZoneConfig)
        {
            TextMarkerDrawable? selectedMarker = null;
            foreach (ExpansionMarketTraderZone ExpansionMarketTraderZone in ExpansionMarketTraderZoneConfig.Items)
            {
                var marker = new TextMarkerDrawable(new PointF((float)ExpansionMarketTraderZone.Position.X, (float)ExpansionMarketTraderZone.Position.Z), _mapControl.MapSize)
                {
                    Color = Color.Red,
                    Radius = (float)ExpansionMarketTraderZone.Radius,
                    Scaleradius = true,
                    Shade = true,
                    Text = $"TraderZone - {ExpansionMarketTraderZone.m_DisplayName}",
                    TextPlacement = MarkerLabelPlacement.Top,
                    TextBackground = true,
                    TextBackgroundColor = Color.Blue
                };
                if (_selectedtraderZone == ExpansionMarketTraderZone)
                {
                    marker.Color = Color.LimeGreen;
                    selectedMarker = marker;
                }
                else
                {
                    _mapControl.RegisterDrawable(marker);
                }
            }
            if (selectedMarker != null)
            {
                _mapControl.RegisterDrawable(selectedMarker);
            }
        }
        private void DrawTraderNPCPositions(ExpansionMarketTraderMapsConfig ExpansionMarketTraderMapsConfig)
        {
            foreach (ExpansionMarketTraderZone ExpansionMarketTraderZone in _expansionManager.ExpansionMarketTraderZoneConfig.MutableItems)
            {
                var marker = new TextMarkerDrawable(new PointF((float)ExpansionMarketTraderZone.Position.X, (float)ExpansionMarketTraderZone.Position.Z), _mapControl.MapSize)
                {
                    Color = Color.Red,
                    Radius = (float)ExpansionMarketTraderZone.Radius,
                    Scaleradius = true,
                    Shade = false,
                    Text = $"TraderZone - {ExpansionMarketTraderZone.m_DisplayName}",
                    TextPlacement = MarkerLabelPlacement.Top,
                    TextBackground = true,
                    TextBackgroundColor = Color.Blue,
                    DrawCenterDot = false
                };
                _mapControl.RegisterDrawable(marker);
            }
            TraderSpawnDrawable? selected_marker = null;
            TextMarkerDrawable? selectedtext_marker = null;
            foreach (ExpansionMarketTraderNpcs tmnpc in ExpansionMarketTraderMapsConfig.MutableItems)
            {
                foreach (ExpansionTraderMaps tm in tmnpc.Tradersmaps)
                {
                    PatrolBehaviour behaviour = PatrolBehaviour.LOOP;
                    if (tm.Positions.Count == 1)
                    {
                        Vec3 pos = tm.Positions[0];
                        var marker = new TraderSpawnDrawable(new PointF((float)pos.X, (float)pos.Z), _mapControl.MapSize)
                        {
                            Color = Color.Red,
                            Orientation = tm.Rotation.getfloatarray(),
                            Text = $"{tm.TraderName}",
                            TextPlacement = MarkerLabelPlacement.Top,
                            TextBackground = true,
                            TextBackgroundColor = Color.BlueViolet
                        };
                        Vec3 v3 = currentTreeNode.Tag as Vec3;

                        if (_selectedExpansionTraderMaps == tm)
                        {
                            if (v3 == pos)
                            {
                                marker.Color = Color.LimeGreen;
                                selected_marker = marker;
                            }
                            else
                            {
                                marker.Color = Color.Yellow;
                                _mapControl.RegisterDrawable(marker);
                            }
                        }
                        else
                        {
                            _mapControl.RegisterDrawable(marker);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < tm.Positions.Count; i++)
                        {
                            Vec3 waypoints = tm.Positions[i];

                            // Determine next waypoint index
                            bool isLast = i == tm.Positions.Count - 1;
                            Vec3 nextWaypoint;

                            if ((behaviour == PatrolBehaviour.ALTERNATE || behaviour == PatrolBehaviour.HALT_OR_ALTERNATE || behaviour == PatrolBehaviour.ONCE) && isLast)
                            {
                                // Don't connect last to first for ALTERNATE
                                nextWaypoint = waypoints;
                            }
                            else
                            {
                                nextWaypoint = tm.Positions[(i + 1) % tm.Positions.Count];
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

                            if (_selectedExpansionTraderMaps == tm)
                                marker.Color = Color.LimeGreen;

                            Vec3 v3 = currentTreeNode.Tag as Vec3;
                            if (v3 == waypoints)
                                marker.Color = Color.Yellow;

                            marker.text = (i == 0 ? tm.TraderName + "\n" : "") + (i + 1).ToString();

                            _mapControl.RegisterDrawable(marker);
                        }
                    }
                }
            }

            if (selected_marker != null)
            {
                _mapControl.RegisterDrawable(selected_marker);
            }
            if (selectedtext_marker != null)
            {
                _mapControl.RegisterDrawable(selectedtext_marker);
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
                    PointF posScreen = _mapControl.MapToScreen(new PointF((float)pos.Position.X, (float)pos.Position.Z));

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

                var tag = currentTreeNode.Parent?.Parent?.Parent?.Parent?.Tag;
                if (tag is ExpansionAIPatrolConfig patrolConfig)
                {
                    ShowHandler(new Vector3Control(), typeof(ExpansionAIPatrolConfig), v3, new List<TreeNode>() { currentTreeNode });
                    DrawbaseAIPatrols(patrolConfig);
                }
                else if (tag is ExpansionQuestObjectiveAICampConfig campConfig)
                {
                    ShowHandler(new Vector3Control(), typeof(ExpansionQuestObjectiveAICampConfig), v3, new List<TreeNode>() { currentTreeNode });
                    DrawbaseObjectiveAICamp(campConfig);
                }
                else if (tag is ExpansionQuestObjectiveAIPatrolConfig questPatrolConfig)
                {
                    ShowHandler(new Vector3Control(), typeof(ExpansionQuestObjectiveAIPatrolConfig), v3, new List<TreeNode>() { currentTreeNode });
                    DrawbaseObjectiveAIPatrols(questPatrolConfig);
                }
                currentTreeNode.Text = v3.GetString();
            }
        }
        private void MapControl_AIEscortSingleclicked(object? sender, MapClickEventArgs e)
        {
            //same as travel/target
        }
        private void MapControl_AIEscortDoubleclicked(object? sender, MapClickEventArgs e)
        {
            if (currentTreeNode.Tag is Vec3 vec3)
            {
                vec3.X = (float)e.MapCoordinates.X;
                vec3.Z = (float)e.MapCoordinates.Y;
                if (MapData.FileExists)
                {
                    vec3.Y = (MapData.gethieght(vec3.X, vec3.Z));
                }
                _mapControl.ClearDrawables();
                ExpansionQuestObjectiveAIEscortConfig escort = currentTreeNode.FindParentOfType<ExpansionQuestObjectiveAIEscortConfig>();
                ShowHandler(new Vector3Control(), typeof(ExpansionQuestObjectiveAIEscortConfig), vec3, new List<TreeNode>() { currentTreeNode });
                DrawbaseAIEscort(escort);
                currentTreeNode.Text = vec3.GetString();
            }
        }
        private void MapControl_TravelSingleclicked(object? sender, MapClickEventArgs e)
        {
            //same as target
        }
        private void MapControl_TravelDoubleclicked(object? sender, MapClickEventArgs e)
        {
            if (currentTreeNode.Tag is Vec3 vec3)
            {
                vec3.X = (float)e.MapCoordinates.X;
                vec3.Z = (float)e.MapCoordinates.Y;
                if (MapData.FileExists)
                {
                    vec3.Y = (MapData.gethieght(vec3.X, vec3.Z));
                }
                _mapControl.ClearDrawables();
                ExpansionQuestObjectiveTravelConfig travel = currentTreeNode.FindParentOfType<ExpansionQuestObjectiveTravelConfig>();
                ShowHandler(new Vector3Control(), typeof(ExpansionQuestObjectiveTravelConfig), vec3, new List<TreeNode>() { currentTreeNode });
                DrawbaseTravel(travel);
                currentTreeNode.Text = vec3.GetString();
            }
        }
        private void MapControl_TargetSingleclicked(object? sender, MapClickEventArgs e)
        {
            //will update if i decide to show all targets from all objectives on the same map
        }
        private void MapControl_TargetDoubleclicked(object? sender, MapClickEventArgs e)
        {
            if (currentTreeNode.Tag is Vec3 vec3)
            {
                vec3.X = (float)e.MapCoordinates.X;
                vec3.Z = (float)e.MapCoordinates.Y;
                if (MapData.FileExists)
                {
                    vec3.Y = (MapData.gethieght(vec3.X, vec3.Z));
                }
                _mapControl.ClearDrawables();
                ExpansionQuestObjectiveTargetConfig target = currentTreeNode.FindParentOfType<ExpansionQuestObjectiveTargetConfig>();
                ShowHandler(new Vector3Control(), typeof(ExpansionQuestObjectiveTargetConfig), vec3, new List<TreeNode>() { currentTreeNode });
                DrawbaseTarget(target);
                currentTreeNode.Text = vec3.GetString();
            }
        }
        private void MapControl_TreasureHuntSingleclicked(object? sender, MapClickEventArgs e)
        {
            if (currentTreeNode?.Parent == null)
                return;
            TreeNode parentNode = currentTreeNode.Parent;
            Vec3 closestPos = null;
            double closestDistance = double.MaxValue;
            PointF clickScreen = _mapControl.MapToScreen(e.MapCoordinates);

            // Loop through all child nodes of the parent
            foreach (TreeNode child in parentNode.Nodes)
            {
                if (child.Tag is Vec3 pos)
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
        private void MapControl_TreasureHuntDoubleclicked(object? sender, MapClickEventArgs e)
        {
            if (currentTreeNode.Tag is Vec3 vec3)
            {
                vec3.X = (float)e.MapCoordinates.X;
                vec3.Z = (float)e.MapCoordinates.Y;
                if (MapData.FileExists)
                {
                    vec3.Y = (MapData.gethieght(vec3.X, vec3.Z));
                }
                _mapControl.ClearDrawables();
                ExpansionQuestObjectiveTreasureHuntConfig treasure = currentTreeNode.FindParentOfType<ExpansionQuestObjectiveTreasureHuntConfig>();
                ShowHandler(new Vector3Control(), typeof(ExpansionQuestObjectiveTreasureHuntConfig), vec3, new List<TreeNode>() { currentTreeNode });
                DrawbaseTreasureHunt(treasure);
                currentTreeNode.Text = vec3.GetString();
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
                    PointF posScreen = _mapControl.MapToScreen(new PointF(pos.Position.X, pos.Position.Z));

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
                ExpansionAINoGoArea.Position.X = (float)e.MapCoordinates.X;
                ExpansionAINoGoArea.Position.Z = (float)e.MapCoordinates.Y;
                if (MapData.FileExists)
                {
                    ExpansionAINoGoArea.Position.Y = (MapData.gethieght(ExpansionAINoGoArea.Position.X, ExpansionAINoGoArea.Position.Z));
                }
                _mapControl.ClearDrawables();
                ExpansionAILocationConfig ExpansionAILocationConfig = currentTreeNode.FindParentOfType<ExpansionAILocationConfig>();
                ShowHandler(new ExpansionAINoGoAreaControl(), typeof(ExpansionAILocationConfig), ExpansionAINoGoArea, new List<TreeNode>() { currentTreeNode });
                DrawbaseAILocationNoGoAreas(ExpansionAILocationConfig);
            }
        }
        private void MapControl_ServerMarkerDataSingleclicked(object sender, MapClickEventArgs e)
        {
            if (currentTreeNode?.Parent == null)
                return;

            TreeNode parentNode = currentTreeNode.Parent;

            ExpansionServerMarkerData closestPos = null;
            double closestDistance = double.MaxValue;

            PointF clickScreen = _mapControl.MapToScreen(e.MapCoordinates);

            // Loop through all child nodes of the parent
            foreach (TreeNode child in parentNode.Nodes)
            {
                if (child.Tag is ExpansionServerMarkerData pos)
                {
                    // Node position in screen space
                    PointF posScreen = _mapControl.MapToScreen(new PointF(pos.m_Position[0], pos.m_Position[2]));

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
            if (closestPos != null && closestDistance <= 30) // 10 units tolerance
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
        private void MapControl_ServerMarkerDataDoubleclicked(object sender, MapClickEventArgs e)
        {
            if (currentTreeNode.Tag is ExpansionServerMarkerData ExpansionServerMarkerData)
            {
                ExpansionServerMarkerData.m_Position[0] = (float)e.MapCoordinates.X;
                ExpansionServerMarkerData.m_Position[2] = (float)e.MapCoordinates.Y;
                if (MapData.FileExists)
                {
                    ExpansionServerMarkerData.m_Position[1] = (MapData.gethieght(ExpansionServerMarkerData.m_Position[0], ExpansionServerMarkerData.m_Position[2]));
                }
                _mapControl.ClearDrawables();
                ExpansionMapConfig ExpansionMapConfig = currentTreeNode.FindParentOfType<ExpansionMapConfig>();
                ShowHandler(new ExpansionMapServerMarkerInfoControl(), typeof(ExpansionMapConfig), ExpansionServerMarkerData, new List<TreeNode>() { currentTreeNode });
                DrawbaseServerMarkerData(ExpansionMapConfig);
            }
        }
        private void MapControl_VehicleSpawnPositionSingleclicked(object sender, MapClickEventArgs e)
        {
            if (currentTreeNode?.Parent == null)
                return;

            TreeNode parentNode = currentTreeNode.Parent.Parent;

            ExpansionMarketSpawnPosition closestPos = null;
            double closestDistance = double.MaxValue;

            PointF clickScreen = _mapControl.MapToScreen(e.MapCoordinates);

            // Loop through all child nodes of the parent
            foreach (TreeNode child2 in parentNode.Nodes)
            {
                foreach (TreeNode child in child2.Nodes)
                {
                    if (child.Tag is ExpansionMarketSpawnPosition pos)
                    {
                        // Node position in screen space
                        PointF posScreen = _mapControl.MapToScreen(new PointF(pos.Position[0], pos.Position[2]));

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
            if (closestPos != null && closestDistance <= 30) // 10 units tolerance
            {
                // Select that tree node in the TreeView
                foreach (TreeNode child2 in parentNode.Nodes)
                {
                    foreach (TreeNode child in child2.Nodes)
                    {
                        if (child.Tag == closestPos)
                        {
                            ExpansionTV.SelectedNode = child;
                            break;
                        }
                    }
                }

                //MessageBox.Show($"Selected closest node at X:{closestPos.x:0.##}, Z:{closestPos.z:0.##}");
            }
        }
        private void MapControl_VehicleSpawnPositionDoubleclicked(object sender, MapClickEventArgs e)
        {
            if (currentTreeNode.Tag is ExpansionMarketSpawnPosition ExpansionMarketSpawnPosition)
            {
                ExpansionMarketSpawnPosition.Position[0] = (float)e.MapCoordinates.X;
                ExpansionMarketSpawnPosition.Position[2] = (float)e.MapCoordinates.Y;
                if (currentTreeNode.Parent.Tag.ToString() == "LandSpawnPositions" ||
                    currentTreeNode.Parent.Tag.ToString() == "AirSpawnPositions" ||
                    currentTreeNode.Parent.Tag.ToString() == "TrainSpawnPositions")
                {
                    if (MapData.FileExists)
                    {
                        ExpansionMarketSpawnPosition.Position[1] = (MapData.gethieght(ExpansionMarketSpawnPosition.Position[0], ExpansionMarketSpawnPosition.Position[2]));
                    }
                }
                _mapControl.ClearDrawables();

                ShowHandler(new ExpasnionMarksetSettingsVehicleSpawnInfoControl(), typeof(ExpansionMarketSettingsConfig), ExpansionMarketSpawnPosition, new List<TreeNode>() { currentTreeNode });
                DrawbaseVehicleSpawnPositions(_expansionManager.ExpansionMarketSettingsConfig);
                currentTreeNode.Text = ExpansionMarketSpawnPosition.ToString();
            }
        }
        private void MapControl_ServerSafeZoneSingleclicked(object sender, MapClickEventArgs e)
        {
            if (currentTreeNode?.Parent == null)
                return;

            TreeNode parentNode = currentTreeNode.Parent.Parent;
            if (currentTreeNode.Tag is Vec3)
            {
                parentNode = currentTreeNode.Parent.Parent.Parent;
            }

            object closestPos = null;
            double closestDistance = double.MaxValue;

            PointF clickScreen = _mapControl.MapToScreen(e.MapCoordinates);



            // Loop through all child nodes of the parent
            foreach (TreeNode child2 in parentNode.Nodes)
            {
                foreach (TreeNode child in child2.Nodes)
                {
                    if (child.Tag is ExpansionSafeZoneCircle pos)
                    {
                        // Node position in screen space
                        PointF posScreen = _mapControl.MapToScreen(new PointF(pos.Center.X, pos.Center.Z));

                        double dx = clickScreen.X - posScreen.X;
                        double dy = clickScreen.Y - posScreen.Y;
                        double distance = Math.Sqrt(dx * dx + dy * dy);

                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestPos = pos;
                        }
                    }
                    else if (child.Tag is ExpansionSafeZonePolygon pos1)
                    {
                        // Node position in screen space
                        PointF posScreen = _mapControl.MapToScreen(PolygonPanTarget.GetPanTargetXZ(pos1.Positions));

                        double dx = clickScreen.X - posScreen.X;
                        double dy = clickScreen.Y - posScreen.Y;
                        double distance = Math.Sqrt(dx * dx + dy * dy);

                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestPos = pos1;
                        }
                        foreach (TreeNode chi in child.Nodes)
                        {
                            if (chi.Tag is Vec3 pos12)
                            {
                                PointF posScreen1 = _mapControl.MapToScreen(new PointF(pos12.X, pos12.Z));

                                double dx1 = clickScreen.X - posScreen1.X;
                                double dy1 = clickScreen.Y - posScreen1.Y;
                                double distance1 = Math.Sqrt(dx1 * dx1 + dy1 * dy1);

                                if (distance1 < closestDistance)
                                {
                                    closestDistance = distance1;
                                    closestPos = pos12;
                                }
                            }
                        }
                    }
                    else if (child.Tag is ExpansionSafeZoneCylinder pos2)
                    {
                        // Node position in screen space
                        PointF posScreen = _mapControl.MapToScreen(new PointF(pos2.Center.X, pos2.Center.Z));

                        double dx = clickScreen.X - posScreen.X;
                        double dy = clickScreen.Y - posScreen.Y;
                        double distance = Math.Sqrt(dx * dx + dy * dy);

                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestPos = pos2;
                        }
                    }

                }
            }

            // Optional: choose only if within some "click radius"
            if (closestPos != null && closestDistance <= 30) // 10 units tolerance
            {
                // Select that tree node in the TreeView
                foreach (TreeNode child2 in parentNode.Nodes)
                {
                    foreach (TreeNode child in child2.Nodes)
                    {
                        if (child.Tag == closestPos)
                        {
                            ExpansionTV.SelectedNode = child;
                            break;
                        }
                        foreach (TreeNode chi in child.Nodes)
                        {
                            if (chi.Tag == closestPos)
                            {
                                ExpansionTV.SelectedNode = chi;
                                break;
                            }
                        }
                    }
                }

                //MessageBox.Show($"Selected closest node at X:{closestPos.x:0.##}, Z:{closestPos.z:0.##}");
            }
        }
        private void MapControl_ServerSafeZoneDoubleclicked(object sender, MapClickEventArgs e)
        {
            if (currentTreeNode.Tag is ExpansionSafeZoneCircle ExpansionSafeZoneCircle)
            {
                ExpansionSafeZoneCircle.Center.X = (float)e.MapCoordinates.X;
                ExpansionSafeZoneCircle.Center.Z = (float)e.MapCoordinates.Y;
                if (MapData.FileExists)
                {
                    ExpansionSafeZoneCircle.Center.Y = (MapData.gethieght(ExpansionSafeZoneCircle.Center.X, ExpansionSafeZoneCircle.Center.Z));
                }
                _mapControl.ClearDrawables();
                ShowHandler(new ExpansionSafeZoneCircleControl(), typeof(ExpansionSafeZoneConfig), ExpansionSafeZoneCircle, new List<TreeNode>() { currentTreeNode });
                DrawbaseSafeZoneData(_expansionManager.ExpansionSafeZoneConfig);
            }
            else if (currentTreeNode.Tag is ExpansionSafeZoneCylinder ExpansionSafeZoneCylinder)
            {
                ExpansionSafeZoneCylinder.Center.X = (float)e.MapCoordinates.X;
                ExpansionSafeZoneCylinder.Center.Z = (float)e.MapCoordinates.Y;
                if (MapData.FileExists)
                {
                    ExpansionSafeZoneCylinder.Center.Y = (MapData.gethieght(ExpansionSafeZoneCylinder.Center.X, ExpansionSafeZoneCylinder.Center.Z));
                }
                _mapControl.ClearDrawables();
                ShowHandler(new ExpansionSafeZoneCylinderControl(), typeof(ExpansionSafeZoneConfig), ExpansionSafeZoneCylinder, new List<TreeNode>() { currentTreeNode });
                DrawbaseSafeZoneData(_expansionManager.ExpansionSafeZoneConfig);
            }
            else if (currentTreeNode.Tag is Vec3 vec3)
            {
                vec3.X = (float)e.MapCoordinates.X;
                vec3.Z = (float)e.MapCoordinates.Y;
                if (MapData.FileExists)
                {
                    vec3.Y = (MapData.gethieght(vec3.X, vec3.Z));
                }
                _mapControl.ClearDrawables();
                ShowHandler(new Vector3Control(), typeof(ExpansionSafeZoneConfig), vec3, new List<TreeNode>() { currentTreeNode });
                DrawbaseSafeZoneData(_expansionManager.ExpansionSafeZoneConfig);
            }
        }
        private void MapControl_SpawnLocationPositionSingleclicked(object sender, MapClickEventArgs e)
        {
            if (currentTreeNode?.Parent == null)
                return;

            TreeNode parentNode = currentTreeNode.Parent.Parent;
            if (currentTreeNode.Tag is Vec3)
            {
                parentNode = currentTreeNode.Parent.Parent.Parent;
            }

            object closestPos = null;
            double closestDistance = double.MaxValue;

            PointF clickScreen = _mapControl.MapToScreen(e.MapCoordinates);


            // Loop through all child nodes of the parent
            foreach (TreeNode child2 in parentNode.Nodes)
            {
                foreach (TreeNode child in child2.Nodes)
                {
                    if (child.Tag is ExpansionSpawnLocation ExpansionSpawnLocation)
                    {
                        // Node position in screen space
                        PointF posScreen = _mapControl.MapToScreen(PolygonPanTarget.GetPanTargetXZ(ExpansionSpawnLocation.Positions));

                        double dx = clickScreen.X - posScreen.X;
                        double dy = clickScreen.Y - posScreen.Y;
                        double distance = Math.Sqrt(dx * dx + dy * dy);

                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestPos = ExpansionSpawnLocation;
                        }
                        foreach (TreeNode chi in child.Nodes)
                        {
                            if (chi.Tag is Vec3 pos12)
                            {
                                PointF posScreen1 = _mapControl.MapToScreen(new PointF(pos12.X, pos12.Z));

                                double dx1 = clickScreen.X - posScreen1.X;
                                double dy1 = clickScreen.Y - posScreen1.Y;
                                double distance1 = Math.Sqrt(dx1 * dx1 + dy1 * dy1);

                                if (distance1 <= closestDistance)
                                {
                                    closestDistance = distance1;
                                    closestPos = pos12;
                                }
                            }
                        }
                    }
                }
            }
            if (closestPos != null && closestDistance <= 30)
            {
                foreach (TreeNode child2 in parentNode.Nodes)
                {
                    foreach (TreeNode child in child2.Nodes)
                    {
                        if (child.Tag == closestPos)
                        {
                            ExpansionTV.SelectedNode = child;
                            break;
                        }
                        foreach (TreeNode chi in child.Nodes)
                        {
                            if (chi.Tag == closestPos)
                            {
                                ExpansionTV.SelectedNode = chi;
                                break;
                            }
                        }
                    }
                }

                //MessageBox.Show($"Selected closest node at X:{closestPos.x:0.##}, Z:{closestPos.z:0.##}");
            }
        }
        private void MapControl_SpawnLocationPositionDoubleclicked(object sender, MapClickEventArgs e)
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

                ExpansionSpawnConfig ExpansionSpawnConfig = currentTreeNode.FindParentOfType<ExpansionSpawnConfig>();
                ShowHandler(new Vector3Control(), typeof(ExpansionSpawnConfig), v3, new List<TreeNode>() { currentTreeNode });
                DrawbaseSpawnLocationData(ExpansionSpawnConfig);
                currentTreeNode.Text = v3.GetString();
            }
        }
        private void MapControl_MissionLocationPositionSingleclicked(object sender, MapClickEventArgs e)
        {
            if (currentTreeNode?.Parent.Parent == null)
                return;
            TreeNode parentNode = currentTreeNode.Parent.Parent;
            object closestPos = null;
            double closestDistance = double.MaxValue;

            PointF clickScreen = _mapControl.MapToScreen(e.MapCoordinates);

            // Loop through all child nodes of the parent
            foreach (TreeNode child2 in parentNode.Nodes)
            {
                foreach (TreeNode child in child2.Nodes)
                {
                    if (child.Tag is Data Data)
                    {
                        // Node position in screen space
                        PointF posScreen = _mapControl.MapToScreen(new PointF((float)Data.Pos[0], (float)Data.Pos[2]));

                        double dx = clickScreen.X - posScreen.X;
                        double dy = clickScreen.Y - posScreen.Y;
                        double distance = Math.Sqrt(dx * dx + dy * dy);

                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestPos = Data;
                        }
                    }
                    if (child.Tag is ExpansionAirdropLocation ExpansionAirdropLocation)
                    {
                        // Node position in screen space
                        PointF posScreen = _mapControl.MapToScreen(new PointF((float)ExpansionAirdropLocation.x, (float)ExpansionAirdropLocation.z));

                        double dx = clickScreen.X - posScreen.X;
                        double dy = clickScreen.Y - posScreen.Y;
                        double distance = Math.Sqrt(dx * dx + dy * dy);

                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestPos = ExpansionAirdropLocation;
                        }
                    }
                }
            }
            if (closestPos != null && closestDistance <= 30)
            {
                foreach (TreeNode child2 in parentNode.Nodes)
                {
                    foreach (TreeNode child in child2.Nodes)
                    {
                        if (child.Tag == closestPos)
                        {
                            ExpansionTV.SelectedNode = child;
                            break;
                        }
                    }
                }

                //MessageBox.Show($"Selected closest node at X:{closestPos.x:0.##}, Z:{closestPos.z:0.##}");
            }

        }
        private void MapControl_MissionLocationPositionDoubleclicked(object sender, MapClickEventArgs e)
        {
            if (currentTreeNode.Tag is ExpansionAirdropLocation ExpansionAirdropLocation)
            {
                ExpansionAirdropLocation.x = (decimal)e.MapCoordinates.X;
                ExpansionAirdropLocation.z = (decimal)e.MapCoordinates.Y;
                _mapControl.ClearDrawables();
                ShowHandler(new ExpansionAirdropLocationControl(), typeof(ExpansionMissionsConfig), ExpansionAirdropLocation, new List<TreeNode>() { currentTreeNode });
                DrawbaseMissionMarkerData(_expansionManager.ExpansionMissionsConfig);
            }
            else if (currentTreeNode.Tag is Data Data)
            {
                ExpansionMissionEventContaminatedArea area = currentTreeNode.Parent.Tag as ExpansionMissionEventContaminatedArea;
                Data.Pos[0] = (decimal)e.MapCoordinates.X;
                Data.Pos[2] = (decimal)e.MapCoordinates.Y;
                if (MapData.FileExists)
                {
                    Data.Pos[1] = (decimal)(MapData.gethieght((float)Data.Pos[0], (float)Data.Pos[2]));
                }
                _mapControl.ClearDrawables();
                ShowHandler(new ExpansionMissionEffectAreaControl(), typeof(CfgeffectareaConfig), area, new List<TreeNode>() { currentTreeNode });
                DrawbaseMissionMarkerData(_expansionManager.ExpansionMissionsConfig);
            }
        }
        private void MapControl_P2PTraderVehicleSpawnSingleclicked(object sender, MapClickEventArgs e)
        {
            if (currentTreeNode?.Parent.Parent == null)
                return;
            TreeNode parentNode = currentTreeNode.Parent.Parent;
            object closestPos = null;
            double closestDistance = double.MaxValue;

            PointF clickScreen = _mapControl.MapToScreen(e.MapCoordinates);

            foreach (TreeNode child in parentNode.Nodes)
            {
                foreach (TreeNode child2 in child.Nodes)
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
                    foreach (TreeNode child2 in child.Nodes)
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
        private void MapControl_P2PTraderVehicleSpawnDoubleclicked(object sender, MapClickEventArgs e)
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

                ExpansionP2pMarketTradersConfig ExpansionP2pMarketTradersConfig = currentTreeNode.FindParentOfType<ExpansionP2pMarketTradersConfig>();
                ShowHandler(new Vector3Control(), typeof(ExpansionP2pMarketTradersConfig), v3, new List<TreeNode>() { currentTreeNode });
                DrawP2PTraderVehicleSpawnData(ExpansionP2pMarketTradersConfig);
            }

        }
        private void MapControl_P2PTraderSpawnPositionsSingleclicked(object? sender, MapClickEventArgs e)
        {
            if (currentTreeNode?.Parent.Parent == null)
                return;
            TreeNode parentNode = currentTreeNode.Parent.Parent;
            object closestPos = null;
            double closestDistance = double.MaxValue;

            PointF clickScreen = _mapControl.MapToScreen(e.MapCoordinates);

            foreach (TreeNode child in parentNode.Nodes)
            {
                ExpansionP2PMarketTraderConfig ExpansionP2PMarketTraderConfig = child.Tag as ExpansionP2PMarketTraderConfig;
                Vec3 pos = ExpansionP2PMarketTraderConfig.m_Position;

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

            // Optional: choose only if within some "click radius"
            if (closestPos != null && closestDistance <= 25) // 10 units tolerance
            {
                // Select that tree node in the TreeView
                foreach (TreeNode child in parentNode.Nodes)
                {
                    ExpansionP2PMarketTraderConfig ExpansionP2PMarketTraderConfig = child.Tag as ExpansionP2PMarketTraderConfig;
                    Vec3 pos = ExpansionP2PMarketTraderConfig.m_Position;

                    if (pos == closestPos)
                    {
                        ExpansionTV.SelectedNode = child.Nodes[0];
                        break;
                    }
                }

                //MessageBox.Show($"Selected closest node at X:{closestPos.x:0.##}, Z:{closestPos.z:0.##}");
            }
        }
        private void MapControl_P2PTraderSpawnPositionsDoubleclicked(object? sender, MapClickEventArgs e)
        {
            if (currentTreeNode.Tag.ToString() == "P2PMarketTraderPOSandOri")
            {
                Vec3 v3 = (currentTreeNode.Parent.Tag as ExpansionP2PMarketTraderConfig).m_Position;

                v3.X = (float)e.MapCoordinates.X;
                v3.Z = (float)e.MapCoordinates.Y;
                if (MapData.FileExists)
                {
                    v3.Y = (MapData.gethieght(v3.X, v3.Z));
                }
                _mapControl.ClearDrawables();

                ExpansionP2PMarketTraderConfig ExpansionP2PMarketTraderConfig = currentTreeNode.FindParentOfType<ExpansionP2PMarketTraderConfig>();
                ShowHandler(new ExpasnionP2PMarksetTraderSpawnInfoControl(), typeof(ExpansionP2pMarketTradersConfig), ExpansionP2PMarketTraderConfig, new List<TreeNode>() { currentTreeNode });
                DrawbaseP2PTraderSpawnPositions(currentTreeNode.FindParentOfType<ExpansionP2pMarketTradersConfig>());
            }
        }
        private void MapControl_ExpansionPersonalStorageSpawnPositionsDoubleclicked(object sender, MapClickEventArgs e)
        {
            if (currentTreeNode.Tag.ToString() == "ExpansionPersonalStorageConfigPOSandOri")
            {
                Vec3 v3 = (currentTreeNode.Parent.Tag as ExpansionPersonalStorageConfig).Position;

                v3.X = (float)e.MapCoordinates.X;
                v3.Z = (float)e.MapCoordinates.Y;
                if (MapData.FileExists)
                {
                    v3.Y = (MapData.gethieght(v3.X, v3.Z));
                }
                _mapControl.ClearDrawables();

                ExpansionPersonalStorageConfig ExpansionPersonalStorageConfig = currentTreeNode.FindParentOfType<ExpansionPersonalStorageConfig>();
                ShowHandler(new ExpasnionPersonalStorageContainerSpawnInfoControl(), typeof(ExpansionPersonalStorageContainersConfig), ExpansionPersonalStorageConfig, new List<TreeNode>() { currentTreeNode });
                DrawbaseExpansionPersonalStoragePositions(currentTreeNode.FindParentOfType<ExpansionPersonalStorageContainersConfig>());
            }
        }
        private void MapControl_ExpansionPersonalStorageSpawnPositionsSingleclicked(object sender, MapClickEventArgs e)
        {
            if (currentTreeNode?.Parent.Parent == null)
                return;
            TreeNode parentNode = currentTreeNode.Parent.Parent;
            object closestPos = null;
            double closestDistance = double.MaxValue;

            PointF clickScreen = _mapControl.MapToScreen(e.MapCoordinates);

            foreach (TreeNode child in parentNode.Nodes)
            {
                ExpansionPersonalStorageConfig ExpansionPersonalStorageConfig = child.Tag as ExpansionPersonalStorageConfig;
                Vec3 pos = ExpansionPersonalStorageConfig.Position;

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

            // Optional: choose only if within some "click radius"
            if (closestPos != null && closestDistance <= 25) // 10 units tolerance
            {
                // Select that tree node in the TreeView
                foreach (TreeNode child in parentNode.Nodes)
                {
                    ExpansionPersonalStorageConfig ExpansionPersonalStorageConfig = child.Tag as ExpansionPersonalStorageConfig;
                    Vec3 pos = ExpansionPersonalStorageConfig.Position;

                    if (pos == closestPos)
                    {
                        ExpansionTV.SelectedNode = child.Nodes[1];
                        break;
                    }
                }

                //MessageBox.Show($"Selected closest node at X:{closestPos.x:0.##}, Z:{closestPos.z:0.##}");
            }
        }
        private void MapControl_ExpansionTraderZonesPositionSingleclicked(object sender, MapClickEventArgs e)
        {
            if (currentTreeNode?.Parent.Parent == null)
                return;
            TreeNode parentNode = currentTreeNode.Parent.Parent;
            object closestPos = null;
            double closestDistance = double.MaxValue;

            PointF clickScreen = _mapControl.MapToScreen(e.MapCoordinates);

            foreach (TreeNode child in parentNode.Nodes)
            {
                ExpansionMarketTraderZone ExpansionMarketTraderZone = child.Tag as ExpansionMarketTraderZone;
                Vec3 pos = ExpansionMarketTraderZone.Position;

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

            // Optional: choose only if within some "click radius"
            if (closestPos != null && closestDistance <= 25) // 10 units tolerance
            {
                // Select that tree node in the TreeView
                foreach (TreeNode child in parentNode.Nodes)
                {
                    ExpansionMarketTraderZone ExpansionMarketTraderZone = child.Tag as ExpansionMarketTraderZone;
                    Vec3 pos = ExpansionMarketTraderZone.Position;

                    if (pos == closestPos)
                    {
                        ExpansionTV.SelectedNode = child.Nodes[0];
                        break;
                    }
                }

                //MessageBox.Show($"Selected closest node at X:{closestPos.x:0.##}, Z:{closestPos.z:0.##}");
            }
        }
        private void MapControl_ExpansionTraderZonePositionsDoubleclicked(object sender, MapClickEventArgs e)
        {
            if (currentTreeNode.Tag.ToString() == "TraderZoneArea")
            {
                Vec3 v3 = (currentTreeNode.Parent.Tag as ExpansionMarketTraderZone).Position;

                v3.X = (float)e.MapCoordinates.X;
                v3.Z = (float)e.MapCoordinates.Y;
                if (MapData.FileExists)
                {
                    v3.Y = (MapData.gethieght(v3.X, v3.Z));
                }
                _mapControl.ClearDrawables();

                ExpansionMarketTraderZone ExpansionMarketTraderZone = currentTreeNode.FindParentOfType<ExpansionMarketTraderZone>();
                ShowHandler(new ExpansionMarkettraderZonePositionsControl(), typeof(ExpansionMarketTraderZoneConfig), ExpansionMarketTraderZone, new List<TreeNode>() { currentTreeNode });
                DrawbaseTraderZonePositions(currentTreeNode.FindParentOfType<ExpansionMarketTraderZoneConfig>());
            }
        }
        private void MapControl_ExpansionTraderMapsSingleclicked(object? sender, MapClickEventArgs e)
        {
            if (currentTreeNode?.Parent.Parent == null)
                return;
            TreeNode parentNode = currentTreeNode.Parent.Parent.Parent.Parent;
            object closestPos = null;
            double closestDistance = double.MaxValue;

            PointF clickScreen = _mapControl.MapToScreen(e.MapCoordinates);

            foreach (TreeNode child in parentNode.Nodes)
            {
                foreach (TreeNode child2 in child.Nodes)
                {
                    foreach (TreeNode child3 in child2.Nodes[1].Nodes)
                    {
                        Vec3 pos = child3.Tag as Vec3;

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
                    foreach (TreeNode child2 in child.Nodes)
                    {
                        foreach (TreeNode child3 in child2.Nodes[1].Nodes)
                        {
                            Vec3 pos = child3.Tag as Vec3;

                            if (pos == closestPos)
                            {
                                ExpansionTV.SelectedNode = child3;
                                break;
                            }
                        }
                    }
                }

                //MessageBox.Show($"Selected closest node at X:{closestPos.x:0.##}, Z:{closestPos.z:0.##}");
            }
        }
        private void MapControl_ExpansionTraderMapsDoubleclicked(object? sender, MapClickEventArgs e)
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

                ExpansionMarketTraderMapsConfig ExpansionMarketTraderMapsConfig = currentTreeNode.FindParentOfType<ExpansionMarketTraderMapsConfig>();
                ShowHandler(new Vector3Control(), typeof(ExpansionMarketTraderMapsConfig), v3, new List<TreeNode>() { currentTreeNode });
                DrawTraderNPCPositions(ExpansionMarketTraderMapsConfig);
                currentTreeNode.Text = v3.GetString();
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
            }
            else if (currentTreeNode.FindParentOfType<AILoadouts>() != null)
            {
                AILoadouts AILoadouts = currentTreeNode.FindParentOfType<AILoadouts>();
                AILoadouts.InventoryAttachments.Add(newIA);
                currentTreeNode.Nodes.Add(newnode);
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
            }
            else if (currentTreeNode.FindParentOfType<AILootDrops>() != null)
            {
                // If this inventory attachment is inside an AILoadouts that belongs to a loot drops file,
                // find that specific AILoadouts parent and update it and the loot drops file's dirty flag.
                TreeNode parent = currentTreeNode.Parent;
                if (parent != null && parent.Tag is AILoadouts parentLoadout)
                {
                    parentLoadout.InventoryAttachments.Remove(currentTreeNode.Tag as Inventoryattachment);
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
            }
            else if (currentTreeNode.FindParentOfType<ExpansionLoadoutConfig>() != null)
            {
                AILoadouts AILoadouts = currentTreeNode.FindLastParentOfType<AILoadouts>();
                // If adding under a loadouts file
                if (currentTreeNode.Tag is Inventoryattachment Inventoryattachment)
                    Inventoryattachment.Items.Add(newItem);
            }
            else if (currentTreeNode.FindParentOfType<AILootDrops>() != null)
            {
                AILootDrops AILootDrops = currentTreeNode.FindLastParentOfType<AILootDrops>();
                if (currentTreeNode.Tag is Inventoryattachment Inventoryattachment)
                    Inventoryattachment.Items.Add(newItem);
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
                    _expansionManager.ExpansionLootDropConfig.RemoveFile(AILootDrops);
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
                        ExpansionLoadoutConfig.RemoveFile(AILoadouts);
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
                    Quantity = new Quantity()
                };
                newAILoadouts.SetPath(newPath);
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
                    LootdropList = new BindingList<AILoadouts>()
                };
                newAILootDrops.SetPath(newPath);
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
                Tag = "ExpansionLootList"
            };
            alcnodes.Nodes.Add(alclnodes);
            currentTreeNode.Nodes.Add(alcnodes);
        }
        private void removeAirdropContainerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionAirdropConfig.Data.Containers.Remove(currentTreeNode.Tag as ExpansionLootContainer);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);

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

                    }
                }
            }
        }
        private void removeAIAdminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionAIConfig.Data.Admins.Remove(currentTreeNode.Text);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);

        }
        private void removeAIPreventClimbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionAIConfig.Data.PreventClimb.Remove(currentTreeNode.Text);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);

        }
        private void removeAIPlayerFactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionAIConfig.Data.PlayerFactions.Remove(currentTreeNode.Text);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);

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
                NumberOfAIMax = 3,
                Behaviour = "ALTERNATE",
                LootingBehaviour = "DEFAULT",
                Speed = "WALK",
                UnderThreatSpeed = "SPRINT",
                DefaultStance = "STANDING",
                DefaultLookAngle = (decimal)0.0,
                CanBeLooted = 1,
                LootDropOnDeath = "",
                UnlimitedReload = 1,
                SniperProneDistanceThreshold = (decimal)0.0,
                AccuracyMin = -1,
                AccuracyMax = -1,
                ThreatDistanceLimit = -1,
                NoiseInvestigationDistanceLimit = -1,
                MaxFlankingDistance = -1,
                EnableFlankingOutsideCombat = -1,
                DamageMultiplier = -1,
                DamageReceivedMultiplier = -1,
                HeadshotResistance = (decimal)0.0,
                ShoryukenChance = -1.0M,
                ShoryukenDamageMultiplier = -1.0M,
                CanBeTriggeredByAI = 0,
                CanSpawnInContaminatedArea = 0,
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
                Waypoints = new BindingList<Vec3>()
            };
            ExpansionAIPatrolConfig ExpansionAIPatrolConfig = currentTreeNode.FindParentOfType<ExpansionAIPatrolConfig>();
            ExpansionAIPatrolConfig.Data.Patrols.Add(newpatrol);
            TreeNode PatrolRoot = new TreeNode(newpatrol.Name)
            {
                Tag = newpatrol
            };
            CreatePatrolNodes(newpatrol, PatrolRoot);
            currentTreeNode.Nodes.Add(PatrolRoot);

        }
        private void removePatrolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionAIPatrolConfig ExpansionAIPatrolConfig = currentTreeNode.FindParentOfType<ExpansionAIPatrolConfig>();
            ExpansionAIPatrolConfig.Data.Patrols.Remove(currentTreeNode.Tag as ExpansionAIPatrol);
            currentTreeNode.Remove();

        }
        private void addWaypointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionAIPatrolConfig ExpansionAIPatrolConfig = currentTreeNode.FindParentOfType<ExpansionAIPatrolConfig>();
            ExpansionAIPatrol ExpansionAIPatrol = currentTreeNode.FindParentOfType<ExpansionAIPatrol>();
            if (ExpansionAIPatrol.Waypoints.Count == null)
                ExpansionAIPatrol.Waypoints = new BindingList<Vec3>();

            Vec3 newvec3 = null;
            if (ExpansionAIPatrol.Waypoints.Count == 0)
            {
                newvec3 = new Vec3((float)AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize / 2, 0f, (float)AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize / 2);
                if (MapData.FileExists)
                {
                    newvec3.Y = (MapData.gethieght(newvec3.X, newvec3.Z));
                }
            }
            else
            {
                Vec3 vec3 = ExpansionAIPatrol.Waypoints.Last();
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
            ExpansionAIPatrol.Waypoints.Add(newvec3);
            ExpansionTV.SelectedNode = newvec3node;
        }
        private void removeWaypointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionAIPatrolConfig ExpansionAIPatrolConfig = currentTreeNode.FindParentOfType<ExpansionAIPatrolConfig>();
            ExpansionAIPatrol ExpansionAIPatrol = currentTreeNode.FindParentOfType<ExpansionAIPatrol>();
            ExpansionAIPatrol.Waypoints.Remove(currentTreeNode.Tag as Vec3);

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
                    ExpansionAIPatrol.Waypoints.Clear();
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
                            ExpansionAIPatrol.Waypoints.Add(new Vec3(XYZ));
                        }
                        break;
                    case 2:
                        ObjectSpawnerArrFile newobjectspawner = JsonSerializer.Deserialize<ObjectSpawnerArrFile>(File.ReadAllText(filePath));
                        foreach (SpawnObjects so in newobjectspawner.Data.Objects)
                        {
                            ExpansionAIPatrol.Waypoints.Add(new Vec3(so.pos));
                        }
                        break;
                    case 3:
                        DZE importfile = DZEHelpers.LoadFile(filePath);
                        foreach (Editorobject eo in importfile.EditorObjects)
                        {
                            ExpansionAIPatrol.Waypoints.Add(new Vec3(eo.Position));
                        }
                        break;
                }
                foreach (Vec3 v3 in ExpansionAIPatrol.Waypoints)
                {
                    currentTreeNode.Nodes.Add(new TreeNode(v3.GetString())
                    {
                        Tag = v3
                    });
                }

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
                        foreach (Vec3 array in ExpansionAIPatrol.Waypoints)
                        {
                            SB.AppendLine("eAI_SurvivorM_Lewis|" + array.GetString() + "|0.0 0.0 0.0");
                        }
                        File.WriteAllText(save.FileName, SB.ToString());
                        break;
                    case 2:
                        ObjectSpawnerArrData newobjectspawner = new ObjectSpawnerArrData();
                        newobjectspawner.Objects = new BindingList<SpawnObjects>();
                        foreach (Vec3 array in ExpansionAIPatrol.Waypoints)
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
                ExpansionAIPatrol.Waypoints.RemoveAt(index);
                ExpansionAIPatrol.Waypoints.Insert(index - 1, waypoint);
                siblings.Insert(index - 1, currentTreeNode);
                ExpansionTV.SelectedNode = currentTreeNode; // Optional: reselect the node
            }

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
                ExpansionAIPatrol.Waypoints.RemoveAt(index);
                ExpansionAIPatrol.Waypoints.Insert(index + 1, waypoint);
                siblings.Insert(index + 1, currentTreeNode);
                ExpansionTV.SelectedNode = currentTreeNode; // Optional: reselect the node
            }

        }
        private void addUnitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> classNames = new List<string>
            {
                "eAI_SurvivorM_Mirek",
                "eAI_SurvivorM_Denis",
                "eAI_SurvivorM_Boris",
                "eAI_SurvivorM_Cyril",
                "eAI_SurvivorM_Elias",
                "eAI_SurvivorM_Francis",
                "eAI_SurvivorM_Guo",
                "eAI_SurvivorM_Hassan",
                "eAI_SurvivorM_Indar",
                "eAI_SurvivorM_Jose",
                "eAI_SurvivorM_Kaito",
                "eAI_SurvivorM_Lewis",
                "eAI_SurvivorM_Manua",
                "eAI_SurvivorM_Niki",
                "eAI_SurvivorM_Oliver",
                "eAI_SurvivorM_Peter",
                "eAI_SurvivorM_Quinn",
                "eAI_SurvivorM_Rolf",
                "eAI_SurvivorM_Seth",
                "eAI_SurvivorM_Taiki",
                "eAI_SurvivorF_Linda",
                "eAI_SurvivorF_Maria",
                "eAI_SurvivorF_Frida",
                "eAI_SurvivorF_Gabi",
                "eAI_SurvivorF_Helga",
                "eAI_SurvivorF_Irena",
                "eAI_SurvivorF_Judy",
                "eAI_SurvivorF_Keiko",
                "eAI_SurvivorF_Eva",
                "eAI_SurvivorF_Naomi",
                "eAI_SurvivorF_Baty"
            };



            ExpansionAIPatrolConfig ExpansionAIPatrolConfig = currentTreeNode.FindParentOfType<ExpansionAIPatrolConfig>();
            ExpansionAIPatrol ExpansionAIPatrol = currentTreeNode.FindParentOfType<ExpansionAIPatrol>();
            AddFromList form = new AddFromList();
            form.TitleLable = "Add AI Unit, Delete accordingly";
            form.List = classNames;
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.GetSelected;
                foreach (string l in addedtypes)
                {
                    if (!ExpansionAIPatrol.Units.Contains(l))
                    {
                        ExpansionAIPatrol.Units.Add(l);
                        currentTreeNode.Nodes.Add(new TreeNode(l)
                        {
                            Tag = "AIPatrolsUnit"
                        });
                    }
                }

                currentTreeNode.Expand();
            }
        }
        private void removeUnitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionAIPatrolConfig ExpansionAIPatrolConfig = currentTreeNode.FindParentOfType<ExpansionAIPatrolConfig>();
            ExpansionAIPatrol ExpansionAIPatrol = currentTreeNode.FindParentOfType<ExpansionAIPatrol>();
            ExpansionAIPatrol.Units.Remove(currentTreeNode.Text);
            currentTreeNode.Remove();

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

        }
        private void removeCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionAIPatrolConfig ExpansionAIPatrolConfig = currentTreeNode.FindParentOfType<ExpansionAIPatrolConfig>();
            Loadbalancingcategorie Loadbalancingcategorie = currentTreeNode.Tag as Loadbalancingcategorie;
            ExpansionAIPatrolConfig.Data._LoadBalancingCategories.Remove(Loadbalancingcategorie);
            currentTreeNode.Remove();

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

        }
        private void removeCountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (currentTreeNode.Parent.Tag as Loadbalancingcategorie).Categorieslist.Remove(currentTreeNode.Tag as Loadbalancingcategories);

            currentTreeNode.Remove();
        }
        private void addAiNoGoAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionAILocationConfig ExpansionAIPatrolConfig = currentTreeNode.FindParentOfType<ExpansionAILocationConfig>();
            ExpansionAINoGoArea newnogo = new ExpansionAINoGoArea()
            {
                Name = "New NoGO Area",
                Position = new Vec3((float)AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize / 2, 0f, (float)AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize / 2),
                Radius = 300,
                Height = MapData.gethieght(AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize / 2, AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize / 2) + 200
            };
            if (MapData.FileExists)
            {
                newnogo.Position.Y = (MapData.gethieght(newnogo.Position.X, newnogo.Position.Z));
            }
            ExpansionAIPatrolConfig.Data.NoGoAreas.Add(newnogo);
            TreeNode newtreenode = new TreeNode($"{newnogo.Name}")
            {
                Tag = newnogo
            };
            currentTreeNode.Nodes.Add(newtreenode);
            ExpansionTV.SelectedNode = newtreenode;

        }
        private void removeAINoGoAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionAILocationConfig ExpansionAIPatrolConfig = currentTreeNode.FindParentOfType<ExpansionAILocationConfig>();
            ExpansionAIPatrolConfig.Data.NoGoAreas.Remove(currentTreeNode.Tag as ExpansionAINoGoArea);
            currentTreeNode.Remove();

        }
        private void addAIExcludedBuildingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionAILocationConfig ExpansionAIPatrolConfig = currentTreeNode.FindParentOfType<ExpansionAILocationConfig>();
            AddItemfromString form = new AddItemfromString();
            form.TitleLable = "Add Excluded Building";
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

            }
        }
        private void removeAIExlusdedBuildingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionAILocationConfig ExpansionAIPatrolConfig = currentTreeNode.FindParentOfType<ExpansionAILocationConfig>();
            ExpansionAIPatrolConfig.Data.ExcludedRoamingBuildings.Remove(currentTreeNode.Text);
            currentTreeNode.Remove();

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

                    }
                }
            }
        }
        private void removeDeployableOutsideTerritoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionBaseBuildingConfig.Data.DeployableOutsideATerritory.Remove(currentTreeNode.Text);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);

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

                    }
                }
            }
        }
        private void removeDeployableInsideEnemyTerritoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionBaseBuildingConfig.Data.DeployableInsideAEnemyTerritory.Remove(currentTreeNode.Text);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);

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

                    }
                }
            }
        }
        private void removeVirtualStorageExcludedContainerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionBaseBuildingConfig.Data.VirtualStorageExcludedContainers.Remove(currentTreeNode.Text);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);

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

            ExpansionTV.SelectedNode = currentTreeNode.LastNode;
        }
        private void RemoveNoBuildZoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionBaseBuildingConfig.Data.Zones.Remove(currentTreeNode.Tag as ExpansionBuildNoBuildZone);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);

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

                    }
                }
            }
        }
        private void removeBuildZoneItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionBuildNoBuildZone currentbuildzone = currentTreeNode.Parent.Parent.Tag as ExpansionBuildNoBuildZone;
            currentbuildzone.Items.Remove(currentTreeNode.Text.Substring(6));
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);

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

            currentTreeNode.Nodes.Add(new TreeNode($"Category Name: {newdescrioptcat.CategoryName}")
            {
                Tag = newdescrioptcat
            });
        }
        private void removeDescriptionCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionBookConfig.Data.Descriptions.Remove(currentTreeNode.Tag as ExpansionBookDescriptionCategory);

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

            currentTreeNode.Nodes.Add(new TreeNode($"Category Name: {newExpansionBookRuleCategory.CategoryName}")
            {
                Tag = newExpansionBookRuleCategory
            });
        }
        private void removeRuleCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionBookConfig.Data.RuleCategories.Remove(currentTreeNode.Tag as ExpansionBookRuleCategory);

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

            currentTreeNode.Nodes.Add(new TreeNode($"Rule Paragraph: {newExpansionBookRule.RuleParagraph}")
            {
                Tag = newExpansionBookRule
            });
        }
        private void removeRuleParagrapghToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionBookRuleCategory ExpansionBookRuleCategory = currentTreeNode.Parent.Tag as ExpansionBookRuleCategory;
            ExpansionBookRuleCategory.Rules.Remove(currentTreeNode.Tag as ExpansionBookRule);

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

            currentTreeNode.Nodes.Add(new TreeNode($"Links: {newExpansionBookLink.Name}")
            {
                Tag = newExpansionBookLink
            });
        }
        private void removeLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionBookConfig.Data.Links.Remove(currentTreeNode.Tag as ExpansionBookLink);

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

            currentTreeNode.Nodes.Add(new TreeNode($"Category: {newExpansionBookCraftingCategory.CategoryName}")
            {
                Tag = newExpansionBookCraftingCategory
            });
        }
        private void removeCraftingCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionBookConfig.Data.CraftingCategories.Remove(currentTreeNode.Tag as ExpansionBookCraftingCategory);

            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
        }
        //Chat
        private void addNewBlacklistedWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemfromString form = new AddItemfromString();
            form.TitleLable = "Add Blacklisted words";
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.addedtypes.ToList();
                foreach (string l in addedtypes)
                {
                    if (!_expansionManager.ExpansionChatConfig.Data.BlacklistedWords.Contains(l))
                    {
                        _expansionManager.ExpansionChatConfig.Data.BlacklistedWords.Add(l);
                        currentTreeNode.Nodes.Add(new TreeNode(l)
                        {
                            Tag = "BlacklistedWord"
                        });
                    }
                }

            }
        }
        private void removeBlacklistedWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionChatConfig.Data.BlacklistedWords.Remove(currentTreeNode.Text);
            currentTreeNode.Remove();

        }
        //Damage
        private void addNewExplosionTargetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemfromString form = new AddItemfromString();
            form.TitleLable = "Add Explosion Targets";
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.addedtypes.ToList();
                foreach (string l in addedtypes)
                {
                    if (!_expansionManager.ExpansionDamageSystemConfig.Data.ExplosionTargets.Contains(l))
                    {
                        _expansionManager.ExpansionDamageSystemConfig.Data.ExplosionTargets.Add(l);
                        currentTreeNode.Nodes.Add(new TreeNode(l)
                        {
                            Tag = "ExplosionTarget"
                        });
                    }
                }

            }
        }
        private void removeExplosionTargetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionDamageSystemConfig.Data.ExplosionTargets.Remove(currentTreeNode.Text);
            currentTreeNode.Remove();

        }
        private void addNewExplosiveProjectileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExplosiveProjectiles newEP = new ExplosiveProjectiles()
            {
                ammo = "New Ammo",
                explosion = "New Explosion"
            };
            if (!_expansionManager.ExpansionDamageSystemConfig.Data._ExplosiveProjectiles.Any(x => x.explosion == newEP.explosion))
            {
                _expansionManager.ExpansionDamageSystemConfig.Data._ExplosiveProjectiles.Add(newEP);

                currentTreeNode.Nodes.Add(new TreeNode($"{newEP.explosion}:{newEP.ammo}")
                {
                    Tag = newEP
                });
            }
            else
            {
                MessageBox.Show("There is already a enrty for that Explosive Type");
            }


        }
        private void removeExplosiveProjectileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionDamageSystemConfig.Data._ExplosiveProjectiles.Remove(currentTreeNode.Tag as ExplosiveProjectiles);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);

        }
        //Garage
        private void addNewEntityWhitelistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes { };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    if (!_expansionManager.ExpansionGarageConfig.Data.EntityWhitelist.Contains(l))
                    {
                        _expansionManager.ExpansionGarageConfig.Data.EntityWhitelist.Add(l);
                        currentTreeNode.Nodes.Add(new TreeNode(l)
                        {
                            Tag = "EntityWhitelistItem"
                        });
                    }
                }

            }

        }
        private void removeEntityWhitelistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionGarageConfig.Data.EntityWhitelist.Remove(currentTreeNode.Text);
            currentTreeNode.Remove();

        }
        //Map
        private void addNewServerMarkerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float pos = AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize / 2;
            ExpansionServerMarkerData newSM = new ExpansionServerMarkerData()
            {
                m_UID = "ServerMarker_New_Marker",
                m_Visibility = 6,
                m_Is3D = 1,
                m_Locked = 0,
                m_Persist = 1,
                m_Text = "New Server Marker",
                m_IconName = "Trader",
                m_Color = -13710223,
                m_Position = new float[]
                    {
                        pos,
                        0,
                        pos
                    }
            };
            if (MapData.FileExists)
            {
                newSM.m_Position[1] = (MapData.gethieght(pos, pos));
            }
            _expansionManager.ExpansionMapConfig.Data.ServerMarkers.Add(newSM);
            currentTreeNode.Nodes.Add(new TreeNode(newSM.m_UID)
            {
                Tag = newSM
            });
        }
        private void removeServerMarkerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionMapConfig.Data.ServerMarkers.Remove(currentTreeNode.Tag as ExpansionServerMarkerData);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
        }
        //Market Settings
        private void addNewLargeVehicleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes { };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    if (!_expansionManager.ExpansionMarketSettingsConfig.Data.LargeVehicles.Contains(l))
                    {
                        _expansionManager.ExpansionMarketSettingsConfig.Data.LargeVehicles.Add(l);
                        currentTreeNode.Nodes.Add(new TreeNode(l)
                        {
                            Tag = "LargeVehicle"
                        });
                    }
                }

            }
        }
        private void removeLargeVehicleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionMarketSettingsConfig.Data.LargeVehicles.Remove(currentTreeNode.Text);
            currentTreeNode.Remove();

        }
        private void addNewCurrencyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<ExpansionMarketCategory> cats = _expansionManager.ExpansionMarketCategoryConfig.GetexchangeCats();

            if (cats == null || cats.Count == 0)
                return;

            List<string> items = new List<string>();

            foreach (ExpansionMarketCategory cat in cats)
            {
                foreach (ExpansionMarketItem item in cat.Items)
                {
                    foreach (string vitem in item.Variants)
                    {
                        if (!items.Contains(vitem))
                            items.Add(vitem);
                    }

                    if (!items.Contains(item.ClassName))
                        items.Add(item.ClassName);
                }
            }

            items.Sort();

            AddFromList newform = new AddFromList();
            newform.List = items;

            DialogResult result = newform.ShowDialog();

            if (result != DialogResult.OK)
                return;

            List<string> returnlist = newform.GetSelected;

            BindingList<string> currencies = new BindingList<string>();

            if (currentTreeNode.Parent.Tag is ExpansionMarketTrader trader)
            {
                currencies = trader.Currencies;
            }
            else if (currentTreeNode.Parent.Tag is ExpansionMarketSettingsConfig settings)
            {
                currencies = settings.Data.Currencies;
            }

            if (currencies == null)
                return;

            foreach (var l in returnlist.Where(x => !currencies.Contains(x)))
            {
                currencies.Add(l);
                currentTreeNode.Nodes.Add(new TreeNode(l)
                {
                    Tag = "Currency"
                });
            }
        }
        private void removeCurrencyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTreeNode.Parent.Parent.Tag is ExpansionMarketTrader ExpansionMarketTrader)
            {
                ExpansionMarketTrader.Currencies.Remove(currentTreeNode.Text);
            }
            else if (currentTreeNode.Parent.Parent.Tag is ExpansionMarketSettingsConfig ExpansionMarketSettingsConfig)
            {
                ExpansionMarketSettingsConfig.Data.Currencies.Remove(currentTreeNode.Text);
            }
            currentTreeNode.Remove();
        }
        private void addNewVehicleKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes { };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    if (!_expansionManager.ExpansionMarketSettingsConfig.Data.VehicleKeys.Contains(l))
                    {
                        _expansionManager.ExpansionMarketSettingsConfig.Data.VehicleKeys.Add(l);
                        currentTreeNode.Nodes.Add(new TreeNode(l)
                        {
                            Tag = "VehicleKey"
                        });
                    }
                }

            }
        }
        private void removeVehicleKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionMarketSettingsConfig.Data.VehicleKeys.Remove(currentTreeNode.Text);
            currentTreeNode.Remove();
        }
        private void addNewLandSpawnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float pos = AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize / 2;
            ExpansionMarketSpawnPosition newspawn = new ExpansionMarketSpawnPosition()
            {
                Position = new float[]
                {
                    pos,
                    0,
                    pos
                },
                Orientation = new float[]
                {
                    0,
                    0,
                    0
                }
            };
            if (MapData.FileExists)
            {
                newspawn.Position[1] = (MapData.gethieght(pos, pos));
            }
            _expansionManager.ExpansionMarketSettingsConfig.Data.LandSpawnPositions.Add(newspawn);
            currentTreeNode.Nodes.Add(new TreeNode(newspawn.ToString())
            {
                Tag = newspawn
            });
            ExpansionTV.SelectedNode = currentTreeNode.LastNode;
        }
        private void addNewAirSpawnToolStripMenuItem_Click(object sender, EventArgs e)
        {

            float pos = AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize / 2;
            ExpansionMarketSpawnPosition newspawn = new ExpansionMarketSpawnPosition()
            {
                Position = new float[]
                {
                    pos,
                    0,
                    pos
                },
                Orientation = new float[]
                {
                    0,
                    0,
                    0
                }
            };
            if (MapData.FileExists)
            {
                newspawn.Position[1] = (MapData.gethieght(pos, pos));
            }
            _expansionManager.ExpansionMarketSettingsConfig.Data.AirSpawnPositions.Add(newspawn);
            currentTreeNode.Nodes.Add(new TreeNode(newspawn.ToString())
            {
                Tag = newspawn
            });

            ExpansionTV.SelectedNode = currentTreeNode.LastNode;
        }
        private void addNewWaterSpawnToolStripMenuItem_Click(object sender, EventArgs e)
        {

            float pos = AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize / 2;
            ExpansionMarketSpawnPosition newspawn = new ExpansionMarketSpawnPosition()
            {
                Position = new float[]
                {
                    pos,
                    0,
                    pos
                },
                Orientation = new float[]
                {
                    0,
                    0,
                    0
                }
            };
            _expansionManager.ExpansionMarketSettingsConfig.Data.WaterSpawnPositions.Add(newspawn);
            currentTreeNode.Nodes.Add(new TreeNode(newspawn.ToString())
            {
                Tag = newspawn
            });

            ExpansionTV.SelectedNode = currentTreeNode.LastNode;
        }
        private void addNewTrainSpawnToolStripMenuItem_Click(object sender, EventArgs e)
        {

            float pos = AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize / 2;
            ExpansionMarketSpawnPosition newspawn = new ExpansionMarketSpawnPosition()
            {
                Position = new float[]
                {
                    pos,
                    0,
                    pos
                },
                Orientation = new float[]
                {
                    0,
                    0,
                    0
                }
            };
            if (MapData.FileExists)
            {
                newspawn.Position[1] = (MapData.gethieght(pos, pos));
            }
            _expansionManager.ExpansionMarketSettingsConfig.Data.TrainSpawnPositions.Add(newspawn);
            currentTreeNode.Nodes.Add(new TreeNode(newspawn.ToString())
            {
                Tag = newspawn
            });
            ExpansionTV.SelectedNode = currentTreeNode.LastNode;
        }
        private void removeVehicleSpawnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string spawntype = currentTreeNode.Parent.Tag.ToString();
            switch (spawntype)
            {
                case "LandSpawnPositions":
                    _expansionManager.ExpansionMarketSettingsConfig.Data.LandSpawnPositions.Remove(currentTreeNode.Tag as ExpansionMarketSpawnPosition);
                    break;
                case "AirSpawnPositions":
                    _expansionManager.ExpansionMarketSettingsConfig.Data.AirSpawnPositions.Remove(currentTreeNode.Tag as ExpansionMarketSpawnPosition);
                    break;
                case "WatertSpawnPositions":
                    _expansionManager.ExpansionMarketSettingsConfig.Data.WaterSpawnPositions.Remove(currentTreeNode.Tag as ExpansionMarketSpawnPosition);
                    break;
                case "TrainSpawnPositions":
                    _expansionManager.ExpansionMarketSettingsConfig.Data.TrainSpawnPositions.Remove(currentTreeNode.Tag as ExpansionMarketSpawnPosition);
                    break;
            }
            currentTreeNode.Remove();
        }
        //Market Categories
        private void addNewMarketCategoryFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEventFile frm = new AddEventFile();
            frm.SetTitle = "Add New Market Category File";
            frm.SetLaable2 = "File Name";
            frm.Button4visable = false;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Button5text = "Add File";
            frm.HideCEStuff();
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string File = frm.typesname;
                string relativePath = GetMarketCategoryPathNode(currentTreeNode, "MarketCategoryRelativePath:");
                List<string> folderParts = string.IsNullOrWhiteSpace(relativePath)
                    ? new List<string>()
                    : relativePath.Split('/').ToList();
                ExpansionMarketCategory newExpansionMarketCategory = _expansionManager.ExpansionMarketCategoryConfig.AddNewMarketCategory(File, folderParts);
                TreeNode categoryNode = new TreeNode(newExpansionMarketCategory.FileName)
                {
                    Tag = newExpansionMarketCategory
                };

                categoryNode.Nodes.Add(new TreeNode("Items")
                {
                    Tag = "MarketItems"
                });
                Helpers.InsertFileNodeAfterFolders(currentTreeNode.Nodes, categoryNode);
                ExpansionTV.SelectedNode = categoryNode;
                categoryNode.Expand();
            }
        }
        private void removeMarketCategoryFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionMarketCategoryConfig.RemoveFile(currentTreeNode.Tag as ExpansionMarketCategory);
            currentTreeNode.Remove();
        }
        private void addNewFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEventFile frm = new AddEventFile();
            frm.SetTitle = "Add New Market Category Folder";
            frm.SetLaable2 = "Folder Name";
            frm.Button4visable = false;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Button5text = "Add Folder";
            frm.HideCEStuff();

            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string folder = frm.typesname;
                if (!Helpers.TryValidateFolderName(folder, out string validationError))
                {
                    MessageBox.Show(validationError, "Invalid Folder Name",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string relativePath = GetMarketCategoryPathNode(currentTreeNode, "MarketCategoryRelativePath:");

                string nodeTag = "MarketCategoryRelativePath:" +
                    string.Join("/", new[] { relativePath, folder }
                        .Where(x => !string.IsNullOrWhiteSpace(x)));

                TreeNode newFolderNode = new TreeNode(folder)
                {
                    Tag = nodeTag
                };

                Helpers.InsertFolderNodeAtTop(currentTreeNode.Nodes, newFolderNode);
                ExpansionTV.SelectedNode = newFolderNode;

                string basePath = _expansionManager.ExpansionMarketCategoryConfig.FilePath;
                string diskRelativePath = nodeTag
                    .Replace("MarketCategoryRelativePath:", "")
                    .Replace('/', Path.DirectorySeparatorChar);

                string fullPath = Path.Combine(basePath, diskRelativePath);
                Directory.CreateDirectory(fullPath);
            }
        }
        private string GetMarketCategoryPathNode(TreeNode node, string prefix)
        {
            if (node?.Tag is string tag && tag.StartsWith(prefix, StringComparison.Ordinal))
                return tag.Substring(prefix.Length);

            return string.Empty;
        }
        private void deleteFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (currentTreeNode == null)
                return;

            if (currentTreeNode.Tag is not string tag || !tag.StartsWith("MarketCategoryRelativePath:"))
            {
                MessageBox.Show("Cannot delete: this node does not represent a category folder.");
                return;
            }

            if (MessageBox.Show($"Delete folder '{currentTreeNode.Text}'?",
                                "Confirm Delete",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            string relativePath = tag.Replace("MarketCategoryRelativePath:", "").Replace('/', Path.DirectorySeparatorChar);

            string basePath = _expansionManager.ExpansionMarketCategoryConfig.FilePath;
            string fullPath = Path.Combine(basePath, relativePath);

            try
            {
                if (Directory.Exists(fullPath))
                {
                    Directory.Delete(fullPath, recursive: true);
                }
                TreeNode parent = currentTreeNode.Parent;
                currentTreeNode.Remove();
                ExpansionTV.SelectedNode = parent;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting folder:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void moveCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode parent = currentTreeNode.FindParentNodeOfType<ExpansionMarketCategoryConfig>();
            if (parent == null)
                return;

            using (var picker = new SelectCategoryFolderForm(parent, SelectCategoryFolderForm.SelectionMode.Folder))
            {
                if (picker.ShowDialog() != DialogResult.OK)
                    return;

                string newTag = picker.SelectedCategoryTag;
                if (currentTreeNode?.Tag is not ExpansionMarketCategory category)
                    return;

                TreeNode destinationNode = TreeNodeExtensions.FindNodeByTag(parent, newTag);
                if (newTag == "MarketCategoryRelativePath:")
                    destinationNode = parent;
                if (destinationNode == null)
                    return;

                MoveCategoryNode(currentTreeNode, destinationNode, category);
            }

        }
        private void MoveCategoryNode(TreeNode categoryNode, TreeNode destinationFolderNode, ExpansionMarketCategory category)
        {
            if (categoryNode == null || destinationFolderNode == null || category == null)
                return;

            // Prevent moving into itself or one of its own children
            TreeNode check = destinationFolderNode;
            while (check != null)
            {
                if (check == categoryNode)
                {
                    MessageBox.Show("Cannot move a category into itself.");
                    return;
                }

                check = check.Parent;
            }

            string oldFileName = Path.GetFileName(category._path);

            string relativePath = GetRelativeFolderPathFromTag(destinationFolderNode.Tag as string);


            string rootPath = _expansionManager.ExpansionMarketCategoryConfig.FilePath;
            category.SetFolderParts(string.IsNullOrWhiteSpace(relativePath)
                ? new List<string>()
                : relativePath
                    .Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList());

            category.SetPath(Path.Combine(rootPath, Path.Combine(category.FolderParts.ToArray()), oldFileName));
            TreeNode oldParent = categoryNode.Parent;
            categoryNode.Remove();
            Helpers.InsertNodeAlphabetically(destinationFolderNode.Nodes, categoryNode);

            destinationFolderNode.Expand();

            // Optional cleanup of old empty folders in the tree
            //RemoveEmptyFolderNodes(oldParent);
        }
        private string GetRelativeFolderPathFromTag(string tag)
        {
            const string prefix = "MarketCategoryRelativePath:";

            if (string.IsNullOrWhiteSpace(tag))
                return string.Empty;

            if (!tag.StartsWith(prefix, StringComparison.Ordinal))
                return string.Empty;

            return tag.Substring(prefix.Length);
        }
        private void addNewMarketItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTreeNode.Parent?.Tag is not ExpansionMarketCategory category)
                return;

            Dictionary<string, bool> UsedTypes = new Dictionary<string, bool>();
            foreach (ExpansionMarketCategory mc in _expansionManager.ExpansionMarketCategoryConfig.Items)
            {
                if (mc.Items == null)
                    continue;

                foreach (ExpansionMarketItem item in mc.Items)
                {
                    if (!string.IsNullOrWhiteSpace(item.ClassName) && !UsedTypes.ContainsKey(item.ClassName))
                        UsedTypes.Add(item.ClassName, true);

                    if (item.Variants != null && item.Variants.Count > 0)
                    {
                        foreach (string v in item.Variants)
                        {
                            if (!string.IsNullOrWhiteSpace(v) && !UsedTypes.ContainsKey(v))
                                UsedTypes.Add(v, true);
                        }
                    }
                }
            }

            AddItemfromTypes form = new AddItemfromTypes()
            {
                LowerCase = true,
                UseMultipleOfSameItem = false,
                UsedTypes = UsedTypes
            };

            DialogResult result = form.ShowDialog();
            if (result != DialogResult.OK)
                return;

            List<string> addedtypes = form.AddedTypes.ToList();

            foreach (string l in addedtypes)
            {
                ExpansionMarketItem newItem = new ExpansionMarketItem
                {
                    ClassName = l,
                    MinPriceThreshold = 0,
                    MaxPriceThreshold = 0,
                    SellPricePercent = -1,
                    MinStockThreshold = 0,
                    MaxStockThreshold = 0,
                    QuantityPercent = -1,
                    SpawnAttachments = new BindingList<string>(),
                    Variants = new BindingList<string>()
                };

                var matches = _expansionManager.ExpansionMarketCategoryConfig.FindAllDuplicates(newItem.ClassName);
                if (matches.Count > 0)
                {
                    var locations = string.Join(Environment.NewLine,
                        matches.Select(x =>
                        {
                            string path = x.category.FolderParts != null && x.category.FolderParts.Any()
                                ? Path.Combine(x.category.FolderParts.ToArray())
                                : "";
                            return $"- {Path.Combine(path, x.category.FileName)}";
                        }));

                    MessageBox.Show(
                        $"An item with classname '{newItem.ClassName}' already exists in:{Environment.NewLine}{locations}");
                    return;
                }

                var variantRefs = _expansionManager.ExpansionMarketCategoryConfig.FindVariantReferences(newItem.ClassName);

                var refsInOtherCategories = variantRefs
                    .Where(x => !ReferenceEquals(x.category, category))
                    .ToList();

                if (refsInOtherCategories.Count > 0)
                {
                    var locations = string.Join(Environment.NewLine,
                        refsInOtherCategories.Select(x =>
                        {
                            string path = x.category.FolderParts != null && x.category.FolderParts.Any()
                                ? Path.Combine(x.category.FolderParts.ToArray())
                                : "";
                            return $"- {Path.Combine(path, x.category.FileName)} (variant of '{x.ownerItem.ClassName}')";
                        }));

                    MessageBox.Show(
                        $"Cannot add '{newItem.ClassName}' as a full market item because it already exists as a variant in other category files:{Environment.NewLine}{locations}");
                    return;
                }

                var refsInSameCategory = variantRefs
                    .Where(x => ReferenceEquals(x.category, category))
                    .ToList();

                if (refsInSameCategory.Count > 0)
                {
                    string owners = string.Join(Environment.NewLine,
                        refsInSameCategory.Select(x => $"- Variant under '{x.ownerItem.ClassName}'"));

                    DialogResult confirm = MessageBox.Show(
                        $"'{newItem.ClassName}' already exists as a variant in this category:{Environment.NewLine}" +
                        owners + Environment.NewLine + Environment.NewLine +
                        "Do you want to also add it as a full market entry?",
                        "Variant already exists",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (confirm != DialogResult.Yes)
                        continue;
                }

                if (!_expansionManager.ExpansionMarketCategoryConfig.AddMarketItem(category, newItem, out string error))
                {
                    MessageBox.Show(error);
                    return;
                }

                CreateMarketItemNode(currentTreeNode, newItem);
            }
        }
        private void removeMarketItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTreeNode?.Tag is not ExpansionMarketItem item)
                return;

            TreeNode configNode = currentTreeNode.FindParentNodeOfType<ExpansionMarketCategoryConfig>();
            if (configNode?.Tag is not ExpansionMarketCategoryConfig config)
                return;

            ExpansionMarketCategory category = currentTreeNode.Parent.Parent.Tag as ExpansionMarketCategory;
            if (category == null)
                return;

            if (!config.RemoveMarketItem(category, item, out string error))
            {
                MessageBox.Show(error);
                return;
            }
            currentTreeNode.Remove();
        }
        private void moveMarketItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode parent = currentTreeNode.FindParentNodeOfType<ExpansionMarketCategoryConfig>();
            if (parent == null)
                return;
            using (var form = new SelectCategoryFolderForm(parent, SelectCategoryFolderForm.SelectionMode.ExpansionMarketCategoryFile))
            {
                if (form.ShowDialog() != DialogResult.OK)
                    return;

                if (currentTreeNode.Tag is not ExpansionMarketItem item)
                    return;

                if (form.SelectedExpansionMarketCategory is not ExpansionMarketCategory destinationCategory)
                    return;

                ExpansionMarketCategory sourceCategory = currentTreeNode.Parent.Parent.Tag as ExpansionMarketCategory;
                if (sourceCategory == null)
                    return;

                if (ReferenceEquals(sourceCategory, destinationCategory))
                {
                    MessageBox.Show("The item is already in that category.");
                    return;
                }

                if (!_expansionManager.ExpansionMarketCategoryConfig.RemoveMarketItem(sourceCategory, item, out string removeError))
                {
                    MessageBox.Show(removeError);
                    return;
                }

                if (!_expansionManager.ExpansionMarketCategoryConfig.AddMarketItem(destinationCategory, item, out string addError))
                {
                    _expansionManager.ExpansionMarketCategoryConfig.AddMarketItem(sourceCategory, item, out _);
                    MessageBox.Show(addError);
                    return;
                }
                TreeNode currentitemnode = currentTreeNode;
                currentTreeNode.Remove();
                TreeNode NewCatNode = TreeNodeExtensions.FindNodeByTag(parent.Nodes, destinationCategory);
                Helpers.InsertNodeAlphabetically(NewCatNode.Nodes[0].Nodes, currentitemnode);
                ExpansionTV.SelectedNode = currentitemnode;
            }
        }
        private void addItemAttachmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionMarketItem ExpansionMarketItem = currentTreeNode.Parent.Tag as ExpansionMarketItem;
            AddItemfromTypes form = new AddItemfromTypes()
            {
                LowerCase = true,
                UseMultipleOfSameItem = true
            };

            DialogResult result = form.ShowDialog();
            if (result != DialogResult.OK)
                return;

            List<string> addedtypes = form.AddedTypes.ToList();
            ExpansionMarketItem.SpawnAttachments ??= new BindingList<string>();
            foreach (var l in addedtypes)
            {
                ExpansionMarketItem.SpawnAttachments.Add(l);
                currentTreeNode.Nodes.Add(new TreeNode(l)
                {
                    Tag = "MarketItemSpawnAttachment"
                });
            }
        }
        private void removeItemAttachmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionMarketItem ExpansionMarketItem = currentTreeNode.FindParentOfType<ExpansionMarketItem>();
            ExpansionMarketItem.SpawnAttachments.Remove(currentTreeNode.Text);
            currentTreeNode.Remove();
        }
        private void addItemVariantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionMarketItem ExpansionMarketItem = currentTreeNode.Parent.Tag as ExpansionMarketItem;
            AddItemfromTypes form = new AddItemfromTypes()
            {
                LowerCase = true,
                UseMultipleOfSameItem = false
            };

            DialogResult result = form.ShowDialog();
            if (result != DialogResult.OK)
                return;

            List<string> addedtypes = form.AddedTypes.ToList();
            ExpansionMarketItem.Variants ??= new BindingList<string>();
            foreach (var l in addedtypes.Where(x => !ExpansionMarketItem.Variants.Contains(x)))
            {
                ExpansionMarketItem.Variants.Add(l);
                currentTreeNode.Nodes.Add(new TreeNode(l)
                {
                    Tag = "MarketItemVarient"
                });
            }
        }
        private void removeItemVariantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionMarketItem ExpansionMarketItem = currentTreeNode.FindParentOfType<ExpansionMarketItem>();
            ExpansionMarketItem.Variants.Remove(currentTreeNode.Text);
            currentTreeNode.Remove();
        }
        private void addItemVariantAutoSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionMarketItem ExpansionMarketItem = currentTreeNode.Parent.Tag as ExpansionMarketItem;

            AddEventFile frm = new AddEventFile();
            frm.SetTitle = "Variant Auto Search";
            frm.SetLaable2 = "search Term";
            frm.Button4visable = false;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Button5text = "Search";
            frm.HideCEStuff();
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string searchTerm = frm.typesname;

                List<TypeEntry> vtypesvariaNTS = AppServices.GetRequired<EconomyManager>().TypesConfig.SearchTypes(searchTerm);
                List<string> founditems = new List<string>();
                foreach (TypeEntry te in vtypesvariaNTS)
                {
                    if (te.Name.ToLower() == ExpansionMarketItem.ClassName)
                        continue;
                    founditems.Add(te.Name.ToLower());
                }

                AddFromList newform = new AddFromList();
                newform.List = founditems;
                DialogResult result = newform.ShowDialog();
                if (result == DialogResult.OK)
                {
                    List<string> returnlist = newform.GetSelected;
                    foreach (var l in returnlist.Where(x => !ExpansionMarketItem.Variants.Contains(x)))
                    {
                        ExpansionMarketItem.Variants.Add(l);
                        currentTreeNode.Nodes.Add(new TreeNode(l)
                        {
                            Tag = "MarketItemVarient"
                        });
                    }
                }
            }
        }
        private void createItemFromItemVariantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTreeNode?.Tag.ToString() != "MarketItemVarient")
                return;

            string variantClassName = currentTreeNode.Text;

            if (currentTreeNode.Parent?.Parent?.Parent?.Parent?.Tag is not ExpansionMarketCategory category)
                return;

            TreeNode categoryNode = currentTreeNode.Parent.Parent.Parent;

            TreeNode configNode = currentTreeNode.FindParentNodeOfType<ExpansionMarketCategoryConfig>();
            if (configNode?.Tag is not ExpansionMarketCategoryConfig config)
                return;

            var matches = config.FindAllDuplicates(variantClassName);
            if (matches.Count > 0)
            {
                var locations = string.Join(Environment.NewLine,
                    matches.Select(x =>
                    {
                        string path = x.category.FolderParts != null && x.category.FolderParts.Any()
                            ? Path.Combine(x.category.FolderParts.ToArray())
                            : "";
                        return $"- {Path.Combine(path, x.category.FileName)}";
                    }));

                MessageBox.Show(
                    $"An item with classname '{variantClassName}' already exists in:{Environment.NewLine}{locations}");
                return;
            }

            ExpansionMarketItem newItem = new ExpansionMarketItem
            {
                ClassName = variantClassName,
                MinPriceThreshold = 0,
                MaxPriceThreshold = 0,
                SellPricePercent = -1,
                MinStockThreshold = 0,
                MaxStockThreshold = 0,
                QuantityPercent = -1,
                SpawnAttachments = new BindingList<string>(),
                Variants = new BindingList<string>()
            };

            if (!config.AddMarketItem(category, newItem, out string error))
            {
                MessageBox.Show(error);
                return;
            }

            TreeNode ItemNode = CreateMarketItemNode(categoryNode, newItem);
            ExpansionTV.SelectedNode = ItemNode;
        }
        //Market Traders
        private void addNewTraderFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEventFile frm = new AddEventFile();
            frm.SetTitle = "Add New Market Trader File";
            frm.SetLaable2 = "File Name";
            frm.Button4visable = false;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Button5text = "Add File";
            frm.HideCEStuff();
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string File = frm.typesname;
                ExpansionMarketTrader newExpansionMarketTrader = _expansionManager.ExpansionMarketTraderConfig.AddNewMarketTrader(File);
                TreeNode traderNode = new TreeNode(newExpansionMarketTrader.FileName)
                {
                    Tag = newExpansionMarketTrader
                };

                CreateTraderNodes(newExpansionMarketTrader, traderNode);
                Helpers.InsertFileNodeAfterFolders(currentTreeNode.Nodes, traderNode);
                ExpansionTV.SelectedNode = traderNode;
                traderNode.Expand();
            }
        }
        private void removeTraderFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionMarketTraderConfig.RemoveFile(currentTreeNode.Tag as ExpansionMarketTrader);
            currentTreeNode.Remove();
        }
        private void addCategoryToTraderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode traderConfigNode = currentTreeNode.FindParentNodeOfType<ExpansionMarketTraderConfig>();
            if (traderConfigNode?.Parent == null)
                return;

            TreeNode categoryRootNode = TreeNodeExtensions.FindNodeByTag(
                traderConfigNode.Parent.Nodes,
                AppServices.GetRequired<ExpansionManager>().ExpansionMarketCategoryConfig);

            if (categoryRootNode == null)
                return;

            using (var form = new SelectCategoryFolderForm(
                categoryRootNode,
                SelectCategoryFolderForm.SelectionMode.ExpansionMarketCategoryFile))
            {
                if (form.ShowDialog() != DialogResult.OK || form.SelectedExpansionMarketCategory == null)
                    return;

                ExpansionMarketTrader trader = currentTreeNode.Parent?.Tag as ExpansionMarketTrader;
                if (trader == null)
                    return;

                string traderPath = form.SelectedExpansionMarketCategory.GetTraderPath();

                bool alreadyExists = trader.m_Categories.Any(c => string.Equals(c.CategoryPath, traderPath, StringComparison.OrdinalIgnoreCase));
                if (alreadyExists)
                {
                    string message = $"The category \"{traderPath}\" is already added to this trader.";

                    MessageBox.Show(
                        message,
                        "Duplicate Category",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    Console.WriteLine($"[INFO] Duplicate category prevented: {traderPath}");
                    return;
                }
                ExpansionMarketTraderCategory newCategory =
                    new ExpansionMarketTraderCategory(
                        traderPath,
                        form.SelectedExpansionMarketCategory,
                        ExpansionMarketTraderBuySell.CanBuyAndSell);

                trader.m_Categories.Add(newCategory);
                TreeNode newCategoryNode = new TreeNode(newCategory.ToString())
                {
                    Tag = newCategory
                };

                Helpers.InsertNodeAlphabetically(currentTreeNode.Nodes, newCategoryNode);
                ExpansionTV.SelectedNode = newCategoryNode;
            }
        }
        private void removeCategoryFromTraderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionMarketTrader trader = currentTreeNode.Parent?.Parent?.Tag as ExpansionMarketTrader;
            trader.m_Categories.Remove(currentTreeNode.Tag as ExpansionMarketTraderCategory);
            currentTreeNode.Remove();
        }
        private void addItemToTraderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode traderConfigNode = currentTreeNode.FindParentNodeOfType<ExpansionMarketTraderConfig>();
            if (traderConfigNode?.Parent == null)
                return;

            TreeNode categoryRootNode = TreeNodeExtensions.FindNodeByTag(
                traderConfigNode.Parent.Nodes,
                AppServices.GetRequired<ExpansionManager>().ExpansionMarketCategoryConfig);

            if (categoryRootNode == null)
                return;

            using (var form = new SelectCategoryFolderForm(
                categoryRootNode,
                SelectCategoryFolderForm.SelectionMode.ExpansionMarketItem))
            {
                if (form.ShowDialog() != DialogResult.OK || form.SelectedExpansionMarketItems == null)
                    return;

                ExpansionMarketTrader trader = currentTreeNode.Parent?.Tag as ExpansionMarketTrader;
                if (trader == null)
                    return;

                List<ExpansionMarketItem> items = form.SelectedExpansionMarketItems;
                TreeNode NewItemnode = null;
                foreach (ExpansionMarketItem item in items)
                {
                    ExpansionMarketTraderItem newitem = new ExpansionMarketTraderItem(item, ExpansionMarketTraderBuySell.CanBuyAndSell);
                    bool alreadyExists = trader.m_Items.Any(c => string.Equals(c.MarketItem.ClassName, item.ClassName, StringComparison.OrdinalIgnoreCase));
                    if (alreadyExists)
                    {
                        string message = $"The category \"{item.ClassName}\" is already added to this trader.";

                        MessageBox.Show(
                            message,
                            "Duplicate Item",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        Console.WriteLine($"[INFO] Duplicate category prevented: {item.ClassName}");
                        continue;
                    }

                    trader.m_Items.Add(newitem);
                    NewItemnode = new TreeNode(newitem.MarketItem.ClassName)
                    {
                        Tag = newitem
                    };
                    Helpers.InsertNodeAlphabetically(currentTreeNode.Nodes, NewItemnode);
                }
                ExpansionTV.SelectedNode = NewItemnode;
            }
        }
        private void removeItemFromTraderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionMarketTrader trader = currentTreeNode.Parent?.Parent?.Tag as ExpansionMarketTrader;
            trader.m_Items.Remove(currentTreeNode.Tag as ExpansionMarketTraderItem);
            currentTreeNode.Remove();
        }
        private void checkForMissingTraderItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionMarketTrader trader = currentTreeNode.Parent.Tag as ExpansionMarketTrader;

            if (trader == null)
                return;

            var missingItems = trader.GetMissedMarketItems();

            if (missingItems == null || missingItems.Count == 0)
            {
                MessageBox.Show("No missing attachment items found.");
                return;
            }

            if (trader.m_Items == null)
                trader.m_Items = new BindingList<ExpansionMarketTraderItem>();

            int addedCount = 0;
            TreeNode NewItemnode = null;
            foreach (var marketItem in missingItems)
            {
                if (marketItem == null || string.IsNullOrWhiteSpace(marketItem.ClassName))
                    continue;

                bool exists = trader.m_Items.Any(x =>
                    x?.MarketItem != null &&
                    string.Equals(x.MarketItem.ClassName, marketItem.ClassName, StringComparison.OrdinalIgnoreCase));

                if (!exists)
                {
                    ExpansionMarketTraderItem newitem = new ExpansionMarketTraderItem
                    {
                        MarketItem = marketItem,
                        BuySell = ExpansionMarketTraderBuySell.CanBuyAndSellAsAttachmentOnly // default (change if needed)
                    };
                    trader.m_Items.Add(newitem);
                    NewItemnode = new TreeNode(newitem.MarketItem.ClassName)
                    {
                        Tag = newitem
                    };
                    Helpers.InsertNodeAlphabetically(currentTreeNode.Nodes, NewItemnode);
                    addedCount++;
                }
            }
            ExpansionTV.SelectedNode = NewItemnode;

            MessageBox.Show($"Added {addedCount} missing trader items.");
        }
        private void previewTraderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTreeNode?.Tag is not ExpansionMarketTrader trader)
                return;

            var preview = new TraderPreviewForm(trader, _expansionManager.ExpansionMarketSettingsConfig.Data.MarketMenuColors);
            preview.Show(this);
        }
        //Market Zones
        private void addNewTraderZoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEventFile frm = new AddEventFile();
            frm.SetTitle = "Add New Market Trader Zone";
            frm.SetLaable2 = "File Name";
            frm.Button4visable = false;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Button5text = "Add File";
            frm.HideCEStuff();
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string File = frm.typesname;
                ExpansionMarketTraderZone newExpansionMarketTraderZone = _expansionManager.ExpansionMarketTraderZoneConfig.AddNewTraderZone(File);
                if (MapData.FileExists)
                {
                    newExpansionMarketTraderZone.Position.Y = (MapData.gethieght(newExpansionMarketTraderZone.Position.X, newExpansionMarketTraderZone.Position.Z));
                }
                TreeNode zoneNode = new TreeNode(newExpansionMarketTraderZone.FileName)
                {
                    Tag = newExpansionMarketTraderZone
                };
                createtraderzonenodes(zoneNode);
                Helpers.InsertFileNodeAfterFolders(currentTreeNode.Nodes, zoneNode);
                ExpansionTV.SelectedNode = zoneNode;

            }
        }
        private void removeTraderZoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionMarketTraderZoneConfig.RemoveFile(currentTreeNode.Tag as ExpansionMarketTraderZone);
            currentTreeNode.Remove();
        }
        private void clearStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTreeNode.Parent.Tag is ExpansionMarketTraderZone ExpansionMarketTraderZone)
            {
                ExpansionMarketTraderZone.stockList = new BindingList<ExpansionMarketTraderStockItem>();
            }
            TreeNode stocknode = currentTreeNode;
            TreeNode parent = currentTreeNode.Parent;
            ExpansionTV.SelectedNode = parent;
            ExpansionTV.SelectedNode = stocknode;
        }
        //Market Trader Maps
        private void addNewTraderMapFIleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEventFile frm = new AddEventFile();
            frm.SetTitle = "Add New Market Map File";
            frm.SetLaable2 = "File Name";
            frm.Button4visable = false;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Button5text = "Add File";
            frm.HideCEStuff();
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string File = frm.typesname;
                ExpansionMarketTraderNpcs newExpansionMarketTraderNpcs = _expansionManager.ExpansionMarketTraderMapsConfig.AddNewMarketMap(File);
                TreeNode traderNode = new TreeNode(newExpansionMarketTraderNpcs.FileName)
                {
                    Tag = newExpansionMarketTraderNpcs
                };

                CreateNPCNodes(traderNode, newExpansionMarketTraderNpcs);
                Helpers.InsertFileNodeAfterFolders(currentTreeNode.Nodes, traderNode);
                ExpansionTV.SelectedNode = traderNode;
                traderNode.Expand();
            }
        }
        private void removeTraderMapFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionMarketTraderMapsConfig.RemoveFile(currentTreeNode.Tag as ExpansionMarketTraderNpcs);
            currentTreeNode.Remove();
        }
        private void addNewTraderNPCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float pos = AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize / 2;
            ExpansionTraderMaps newExpansionTraderMaps = new ExpansionTraderMaps()
            {
                NpcClassName = "ExpansionTraderMirek",
                TraderName = "",
                Rotation = new Vec3(0m, 0m, 0m),
                Positions = new BindingList<Vec3>()
                {
                    new Vec3(pos,0f,pos)
                },
                Items = new List<TraderNPCItem>()
            };
            if (MapData.FileExists)
            {
                newExpansionTraderMaps.Positions[0].Y = (MapData.gethieght(pos, pos));
            }
            ExpansionMarketTraderNpcs ExpansionMarketTraderNpcs = currentTreeNode.Tag as ExpansionMarketTraderNpcs;
            ExpansionMarketTraderNpcs.Tradersmaps.Add(newExpansionTraderMaps);
            Helpers.InsertNodeAlphabetically(currentTreeNode.Nodes, CreateNPCSingleNodes(newExpansionTraderMaps));
        }
        private void removeTraderNPCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionTraderMaps ExpansionTraderMaps = currentTreeNode.Tag as ExpansionTraderMaps;
            ExpansionMarketTraderNpcs ExpansionMarketTraderNpcs = currentTreeNode.Parent.Tag as ExpansionMarketTraderNpcs;
            ExpansionMarketTraderNpcs.Tradersmaps.Remove(ExpansionTraderMaps);
            currentTreeNode.Remove();
        }
        private void addTraderNPCWaypointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionTraderMaps ExpansionTraderMaps = currentTreeNode.Parent.Tag as ExpansionTraderMaps;
            Vec3 lastpos = ExpansionTraderMaps.Positions.Last();
            Vec3 newpos = new Vec3()
            {
                X = lastpos.X + 25,
                Y = lastpos.Y,
                Z = lastpos.Z + 10
            };
            if (MapData.FileExists)
            {
                newpos.Y = (MapData.gethieght(newpos.X, newpos.Z));
            }
            ExpansionTraderMaps.Positions.Add(newpos);


            string posLabel = ExpansionTraderMaps.Positions.Count == 1 ? "Position" : "Waypoints";
            currentTreeNode.Text = posLabel;
            currentTreeNode.Nodes.Add(new TreeNode(newpos.ToString()) { Tag = newpos });
        }
        private void removeTraderNPCWaypointToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void addTraderNPCItemToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void removeTraderNPCItemToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void addNPCTraderNPCAttachmentToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void removeTraderNPCAttachmentToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void addTraderNPCPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void rempveTraderNPCPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void checkNPCIsInAZoneToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ExpansionTraderMaps tm = currentTreeNode.Tag as ExpansionTraderMaps;
            if (tm == null)
                return;

            List<ExpansionMarketTraderZone> TMzone =
                AppServices.GetRequired<ExpansionManager>()
                           .ExpansionMarketTraderZoneConfig
                           .GetZonefromTraderMap(tm);

            if (TMzone == null || TMzone.Count == 0)
            {
                MessageBox.Show(
                    "This trader is not in any zone.",
                    "Zone Check",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            // Build a readable list of zones
            string zoneList = string.Join(
                Environment.NewLine,
                TMzone.Select(z => z.m_DisplayName) // adjust property if needed
            );

            MessageBox.Show(
                $"This trader is in the following zone(s):{Environment.NewLine}{Environment.NewLine}{zoneList}",
                "Zone Check",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

        }
        //MIssions
        private void addNewAirdropMissionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEventFile frm = new AddEventFile();
            frm.SetTitle = "Add new Airdrop File";
            frm.Button4visable = false;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Button5text = "Import to Expansion";
            frm.HideCEStuff();
            frm.moddir = Path.Combine(AppServices.GetRequired<ProjectManager>().CurrentProject.ProjectRoot, "mpmissions", AppServices.GetRequired<ProjectManager>().CurrentProject.MpMissionPath, "expansion", "missions");
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string newmodPath = frm.moddir.Replace("/", "\\");
                string AirdropFilename = "Airdrop_Random_" + frm.typesname + ".json";
                string newPath = Path.Combine(newmodPath, AirdropFilename);
                ExpansionMissionEventAirdrop newmission = new ExpansionMissionEventAirdrop()
                {
                    Enabled = 1,
                    Weight = 1.0m,
                    MissionMaxTime = 1200,
                    MissionName = "Random_" + frm.typesname,
                    Difficulty = 0,
                    Objective = 0,
                    Reward = "",
                    ShowNotification = 1,
                    Height = 450m,
                    DropZoneHeight = 450m,
                    Speed = 25.0m,
                    DropZoneSpeed = 25m,
                    Container = "Random",
                    FallSpeed = (decimal)4.5,
                    DropLocation = new ExpansionAirdropLocation()
                    {
                        Name = frm.typesname,
                        Radius = 100,
                        x = AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize / 2,
                        z = AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize / 2
                    },
                    Infected = new BindingList<string>(),
                    ItemCount = -1,
                    InfectedCount = -1,
                    AirdropPlaneClassName = "",
                    Loot = new BindingList<ExpansionLoot>()
                };
                newmission.SetPath(newPath);
                _expansionManager.ExpansionMissionsConfig.AddNewMissionFile(newmission);
                TreeNode missionNode = new TreeNode(newmission.FileName)
                {
                    Tag = newmission
                };
                if (newmission is ExpansionMissionEventAirdrop ExpansionMissionEventAirdrop)
                {
                    missionNode.Nodes.Add(new TreeNode(ExpansionMissionEventAirdrop.MissionName)
                    {
                        Tag = "MissionAirdrop"
                    });
                    missionNode.Nodes.Add(new TreeNode($"Drop Location - {ExpansionMissionEventAirdrop.DropLocation.Name}")
                    {
                        Tag = ExpansionMissionEventAirdrop.DropLocation
                    });
                    TreeNode alcinodes = new TreeNode("Infected")
                    {
                        Tag = "AirdropContainersInfected"
                    };
                    missionNode.Nodes.Add(alcinodes);

                    TreeNode alclnodes = new TreeNode("Loot")
                    {
                        Tag = "ExpansionLootList"
                    };
                    missionNode.Nodes.Add(alclnodes);
                }
                Helpers.InsertNodeAlphabetically(currentTreeNode.Nodes, missionNode);
                ExpansionTV.SelectedNode = missionNode;
            }
        }
        private void addNewContaminatedMissionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEventFile frm = new AddEventFile();
            frm.SetTitle = "Add new Contaminated File";
            frm.Button4visable = false;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Button5text = "Import to Expansion";
            frm.HideCEStuff();
            frm.moddir = Path.Combine(AppServices.GetRequired<ProjectManager>().CurrentProject.ProjectRoot, "mpmissions", AppServices.GetRequired<ProjectManager>().CurrentProject.MpMissionPath, "expansion", "missions");
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string newmodPath = frm.moddir.Replace("/", "\\");
                string AirdropFilename = "ContaminatedArea_Settlement_" + frm.typesname + ".json";
                string newPath = Path.Combine(newmodPath, AirdropFilename);
                ExpansionMissionEventContaminatedArea newmission = new ExpansionMissionEventContaminatedArea()
                {
                    m_Version = 0,
                    Enabled = 1,
                    Weight = 0,
                    MissionMaxTime = 0,
                    MissionName = "Settlement_" + frm.typesname,
                    Difficulty = 0,
                    Objective = 0,
                    Reward = "",
                    Data = new Data()
                    {
                        Pos = new decimal[] { AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize / 2, 0, AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize / 2, },
                        Radius = 100,
                        PosHeight = 25,
                        NegHeight = 20,
                        InnerRingCount = 2,
                        InnerPartDist = 50,
                        OuterRingToggle = true,
                        OuterPartDist = 40,
                        OuterOffset = 0,
                        VerticalLayers = 0,
                        VerticalOffset = 0,
                        ParticleName = "graphics/particles/contaminated_area_gas_bigass"
                    },
                    PlayerData = new PlayerData()
                    {
                        AroundPartName = "graphics/particles/contaminated_area_gas_around",
                        TinyPartName = "graphics/particles/contaminated_area_gas_around_tiny",
                        PPERequesterType = "PPERequester_ContaminatedAreaTint"
                    },
                    StartDecayLifetime = 600,
                    FinishDecayLifetime = 300
                };
                newmission.SetPath(newPath);
                _expansionManager.ExpansionMissionsConfig.AddNewMissionFile(newmission);
                TreeNode missionNode = new TreeNode(newmission.FileName)
                {
                    Tag = newmission
                };
                if (newmission is ExpansionMissionEventContaminatedArea ExpansionMissionEventContaminatedArea)
                {
                    missionNode.Nodes.Add(new TreeNode(ExpansionMissionEventContaminatedArea.MissionName)
                    {
                        Tag = ExpansionMissionEventContaminatedArea.Data
                    });
                    missionNode.Nodes.Add(new TreeNode("General")
                    {
                        Tag = "MissionContaminatedAreaGeneral"
                    });
                    if (ExpansionMissionEventContaminatedArea.Data != null)
                        missionNode.Nodes.Add(new TreeNode("Data")
                        {
                            Tag = "MissionContaminatedAreaData"
                        });
                    if (ExpansionMissionEventContaminatedArea.PlayerData != null)
                        missionNode.Nodes.Add(new TreeNode("PlayerData") { Tag = ExpansionMissionEventContaminatedArea.PlayerData });
                }
                Helpers.InsertNodeAlphabetically(currentTreeNode.Nodes, missionNode);
                ExpansionTV.SelectedNode = missionNode;
            }
        }
        private void removeMissionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTreeNode.Tag is ExpansionMissionEventAirdrop ExpansionMissionEventAirdrop)
            {
                _expansionManager.ExpansionMissionsConfig.RemoveFile(ExpansionMissionEventAirdrop);
            }
            else if (currentTreeNode.Tag is ExpansionMissionEventContaminatedArea ExpansionMissionEventContaminatedArea)
            {
                _expansionManager.ExpansionMissionsConfig.RemoveFile(ExpansionMissionEventContaminatedArea);
            }
            else if (currentTreeNode.Tag is ExpansionMissionEventHeliCrash ExpansionMissionEventHeliCrash)
            {
                _expansionManager.ExpansionMissionsConfig.RemoveFile(ExpansionMissionEventHeliCrash);
            }
            currentTreeNode.Remove();
        }
        //Notifications
        private void addNewNotificationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionNotificationSchedule newsched = new ExpansionNotificationSchedule()
            {
                Title = "New Notification",
                Hour = 00,
                Minute = 00,
                Second = 00,
                Text = "",
                Icon = "Info",
                Color = "27272DFF"
            };
            _expansionManager.ExpansionNotificationSchedulerConfig.Data.Notifications.Add(newsched);
            currentTreeNode.Nodes.Add(new TreeNode(newsched.Title)
            {
                Tag = newsched
            });
        }
        private void removeNotificationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionNotificationSchedulerConfig.Data.Notifications.Remove(currentTreeNode.Tag as ExpansionNotificationSchedule);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
        }
        //P2PMarket
        private void AddNewP2PMarketCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionP2PMarketMenuCategory cat = new ExpansionP2PMarketMenuCategory()
            {
                DisplayName = "New Menu Category",
                IconPath = "",
                Included = new BindingList<string>(),
                Excluded = new BindingList<string>(),
                SubCategories = new BindingList<ExpansionP2PMarketMenuSubCategory>()
            };
            _expansionManager.ExpansionP2PMarketConfig.Data.MenuCategories.Add(cat);
            TreeNode Menucatnoderoot = new TreeNode(cat.DisplayName)
            {
                Tag = cat
            };

            TreeNode IncludedclassnamesNode = new TreeNode("Included")
            {
                Tag = "MenuCatsIncluded"
            };
            foreach (string s in cat.Included)
            {
                IncludedclassnamesNode.Nodes.Add(new TreeNode(s)
                {
                    Tag = "MenuCatIncluded"
                });
            }
            Menucatnoderoot.Nodes.Add(IncludedclassnamesNode);
            TreeNode ExludedclassnamesNode = new TreeNode("Excluded")
            {
                Tag = "MenuCatsExluded"
            };
            foreach (string s in cat.Excluded)
            {
                ExludedclassnamesNode.Nodes.Add(new TreeNode(s)
                {
                    Tag = "MenuCatExluded"
                });
            }
            Menucatnoderoot.Nodes.Add(ExludedclassnamesNode);
            TreeNode SubCatNodes = new TreeNode("Sub Categories")
            {
                Tag = "P2PSubCategories"
            };
            foreach (ExpansionP2PMarketMenuSubCategory subcat in cat.SubCategories)
            {
                SubCatNodes.Nodes.Add(CreateSubCats(subcat));
            }
            Menucatnoderoot.Nodes.Add(SubCatNodes);
            Helpers.InsertNodeAlphabetically(currentTreeNode.Nodes, Menucatnoderoot);
            ExpansionTV.SelectedNode = Menucatnoderoot;
        }
        private void RemoveP2PMarketCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionP2PMarketConfig.Data.MenuCategories.Remove(currentTreeNode.Tag as ExpansionP2PMarketMenuCategory);
            currentTreeNode.Remove();
        }
        private void AddNewP2PMarketSubCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTreeNode.Parent.Tag is ExpansionP2PMarketMenuCategory ExpansionP2PMarketMenuCategory)
            {
                ExpansionP2PMarketMenuSubCategory newsub = new ExpansionP2PMarketMenuSubCategory()
                {
                    DisplayName = "New Sub Category",
                    IconPath = "",
                    Included = new BindingList<string>(),
                    Excluded = new BindingList<string>()
                };
                ExpansionP2PMarketMenuCategory.SubCategories.Add(newsub);
                TreeNode Menucatnoderoot = new TreeNode(newsub.DisplayName)
                {
                    Tag = newsub
                };
                TreeNode IncludedclassnamesNode = new TreeNode("Included")
                {
                    Tag = "MenuCatsIncluded"
                };
                foreach (string s in newsub.Included)
                {
                    IncludedclassnamesNode.Nodes.Add(new TreeNode(s)
                    {
                        Tag = "MenuCatIncluded"
                    });
                }
                Menucatnoderoot.Nodes.Add(IncludedclassnamesNode);
                TreeNode ExludedclassnamesNode = new TreeNode("Excluded")
                {
                    Tag = "MenuCatsExluded"
                };
                foreach (string s in newsub.Excluded)
                {
                    ExludedclassnamesNode.Nodes.Add(new TreeNode(s)
                    {
                        Tag = "MenuCatExluded"
                    });
                }
                Menucatnoderoot.Nodes.Add(ExludedclassnamesNode);
                Helpers.InsertNodeAlphabetically(currentTreeNode.Nodes, Menucatnoderoot);
                ExpansionTV.SelectedNode = Menucatnoderoot;
            }
            else if (currentTreeNode.Parent.Tag is ExpansionPersonalStorageMenuCategory ExpansionPersonalStorageMenuCategory)
            {
                ExpansionPersonalStorageMenuSubCategory newsub = new ExpansionPersonalStorageMenuSubCategory()
                {
                    DisplayName = "New Sub Category",
                    IconPath = "",
                    Included = new BindingList<string>(),
                    Excluded = new BindingList<string>()
                };
                ExpansionPersonalStorageMenuCategory.SubCategories.Add(newsub);
                TreeNode Menucatnoderoot = new TreeNode(newsub.DisplayName)
                {
                    Tag = newsub
                };
                TreeNode IncludedclassnamesNode = new TreeNode("Included")
                {
                    Tag = "MenuCatsIncluded"
                };
                foreach (string s in newsub.Included)
                {
                    IncludedclassnamesNode.Nodes.Add(new TreeNode(s)
                    {
                        Tag = "MenuCatIncluded"
                    });
                }
                Menucatnoderoot.Nodes.Add(IncludedclassnamesNode);
                TreeNode ExludedclassnamesNode = new TreeNode("Excluded")
                {
                    Tag = "MenuCatsExluded"
                };
                foreach (string s in newsub.Excluded)
                {
                    ExludedclassnamesNode.Nodes.Add(new TreeNode(s)
                    {
                        Tag = "MenuCatExluded"
                    });
                }
                Menucatnoderoot.Nodes.Add(ExludedclassnamesNode);
                Helpers.InsertNodeAlphabetically(currentTreeNode.Nodes, Menucatnoderoot);
                ExpansionTV.SelectedNode = Menucatnoderoot;
            }
        }
        private void RemoveP2PMarketSubCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTreeNode.Parent.Parent.Tag is ExpansionP2PMarketMenuCategory ExpansionP2PMarketMenuCategory)
            {
                ExpansionP2PMarketMenuCategory.SubCategories.Remove(currentTreeNode.Tag as ExpansionP2PMarketMenuSubCategory);
                currentTreeNode.Remove();
            }
            else if (currentTreeNode.Parent.Parent.Tag is ExpansionPersonalStorageMenuCategory ExpansionPersonalStorageMenuCategory)
            {
                ExpansionPersonalStorageMenuCategory.SubCategories.Remove(currentTreeNode.Tag as ExpansionPersonalStorageMenuSubCategory);
                currentTreeNode.Remove();
            }
        }
        private void AddNewP2PMarketIncludedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTreeNode.Parent.Tag is ExpansionP2PMarketMenuCategoryBase ExpansionP2PMarketMenuCategoryBase)
            {
                AddItemfromTypes form = new AddItemfromTypes { };
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    List<string> addedtypes = form.AddedTypes.ToList();
                    foreach (string newincluded in addedtypes)
                    {
                        ExpansionP2PMarketMenuCategoryBase.Included.Add(newincluded);
                        currentTreeNode.Nodes.Add(new TreeNode(newincluded)
                        {
                            Tag = "MenuCatIncluded"
                        });
                    }
                    currentTreeNode.ExpandAll();
                }
            }
            else if (currentTreeNode.Parent.Tag is ExpansionPersonalStorageMenuCategory ExpansionPersonalStorageMenuCategory)
            {
                AddItemfromTypes form = new AddItemfromTypes { };
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    List<string> addedtypes = form.AddedTypes.ToList();
                    foreach (string newincluded in addedtypes)
                    {
                        ExpansionPersonalStorageMenuCategory.Included.Add(newincluded);
                        currentTreeNode.Nodes.Add(new TreeNode(newincluded)
                        {
                            Tag = "MenuCatIncluded"
                        });
                    }
                    currentTreeNode.ExpandAll();
                }
            }
            else if (currentTreeNode.Parent.Tag is ExpansionPersonalStorageMenuSubCategory ExpansionPersonalStorageMenuSubCategory)
            {
                AddItemfromTypes form = new AddItemfromTypes { };
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    List<string> addedtypes = form.AddedTypes.ToList();
                    foreach (string newincluded in addedtypes)
                    {
                        ExpansionPersonalStorageMenuSubCategory.Included.Add(newincluded);
                        currentTreeNode.Nodes.Add(new TreeNode(newincluded)
                        {
                            Tag = "MenuCatIncluded"
                        });
                    }
                    currentTreeNode.ExpandAll();
                }
            }
        }
        private void RemoveP2PMarketIncludedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTreeNode.Parent.Parent.Tag is ExpansionP2PMarketMenuCategoryBase ExpansionP2PMarketMenuCategoryBase)
            {
                ExpansionP2PMarketMenuCategoryBase.Included.Remove(currentTreeNode.Text);
                currentTreeNode.Remove();
            }
            else if (currentTreeNode.Parent.Parent.Tag is ExpansionPersonalStorageMenuCategory ExpansionPersonalStorageMenuCategory)
            {
                ExpansionPersonalStorageMenuCategory.Included.Remove(currentTreeNode.Text);
                currentTreeNode.Remove();
            }
            else if (currentTreeNode.Parent.Parent.Tag is ExpansionPersonalStorageMenuSubCategory ExpansionPersonalStorageMenuSubCategory)
            {
                ExpansionPersonalStorageMenuSubCategory.Included.Remove(currentTreeNode.Text);
                currentTreeNode.Remove();
            }
        }
        private void AddNewP2PMarketExcludedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTreeNode.Parent.Tag is ExpansionP2PMarketConfig ExpansionP2PMarketConfig)
            {
                AddItemfromTypes form = new AddItemfromTypes { };
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    List<string> addedtypes = form.AddedTypes.ToList();
                    foreach (string newincluded in addedtypes)
                    {
                        ExpansionP2PMarketConfig.Data.ExcludedClassNames.Add(newincluded);
                        currentTreeNode.Nodes.Add(new TreeNode(newincluded)
                        {
                            Tag = "P2PExcludedClassname"
                        });
                    }
                    currentTreeNode.ExpandAll();
                }
            }
            else if (currentTreeNode.Parent.Tag is ExpansionP2PMarketMenuCategoryBase ExpansionP2PMarketMenuCategoryBase)
            {
                AddItemfromTypes form = new AddItemfromTypes { };
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    List<string> addedtypes = form.AddedTypes.ToList();
                    foreach (string newincluded in addedtypes)
                    {
                        ExpansionP2PMarketMenuCategoryBase.Excluded.Add(newincluded);
                        currentTreeNode.Nodes.Add(new TreeNode(newincluded)
                        {
                            Tag = "MenuCatExluded"
                        });
                    }
                    currentTreeNode.ExpandAll();
                }
            }
            else if (currentTreeNode.Parent.Tag is ExpansionPersonalStorageMenuCategory ExpansionPersonalStorageMenuCategory)
            {
                AddItemfromTypes form = new AddItemfromTypes { };
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    List<string> addedtypes = form.AddedTypes.ToList();
                    foreach (string newincluded in addedtypes)
                    {
                        ExpansionPersonalStorageMenuCategory.Excluded.Add(newincluded);
                        currentTreeNode.Nodes.Add(new TreeNode(newincluded)
                        {
                            Tag = "MenuCatExluded"
                        });
                    }
                    currentTreeNode.ExpandAll();
                }
            }
            else if (currentTreeNode.Parent.Tag is ExpansionPersonalStorageMenuSubCategory ExpansionPersonalStorageMenuSubCategory)
            {
                AddItemfromTypes form = new AddItemfromTypes { };
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    List<string> addedtypes = form.AddedTypes.ToList();
                    foreach (string newincluded in addedtypes)
                    {
                        ExpansionPersonalStorageMenuSubCategory.Excluded.Add(newincluded);
                        currentTreeNode.Nodes.Add(new TreeNode(newincluded)
                        {
                            Tag = "MenuCatExluded"
                        });
                    }
                    currentTreeNode.ExpandAll();
                }
            }
            else if (currentTreeNode.Parent.Tag is ExpansionPersonalStorageNewSettingsConfig ExpansionPersonalStorageNewConfig)
            {
                AddItemfromTypes form = new AddItemfromTypes { };
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    List<string> addedtypes = form.AddedTypes.ToList();
                    foreach (string newincluded in addedtypes)
                    {
                        ExpansionPersonalStorageNewConfig.Data.ExcludedItems.Add(newincluded);
                        currentTreeNode.Nodes.Add(new TreeNode(newincluded)
                        {
                            Tag = "PersonalStorageNewExludedItem"
                        });
                    }
                    currentTreeNode.ExpandAll();
                }
            }
            else if (currentTreeNode.Parent.Tag is ExpansionPersonalStorageSettingsConfig ExpansionPersonalStorageConfig)
            {
                AddItemfromTypes form = new AddItemfromTypes { };
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    List<string> addedtypes = form.AddedTypes.ToList();
                    foreach (string newincluded in addedtypes)
                    {
                        ExpansionPersonalStorageConfig.Data.ExcludedClassNames.Add(newincluded);
                        currentTreeNode.Nodes.Add(new TreeNode(newincluded)
                        {
                            Tag = "PersonalStorageExcludedClassName"
                        });
                    }
                    currentTreeNode.ExpandAll();
                }
            }
        }
        private void removeP2PMarketExcludedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTreeNode.Parent.Parent.Tag is ExpansionP2PMarketConfig ExpansionP2PMarketConfig)
            {
                ExpansionP2PMarketConfig.Data.ExcludedClassNames.Remove(currentTreeNode.Text);
                currentTreeNode.Remove();
            }
            else if (currentTreeNode.Parent.Parent.Tag is ExpansionP2PMarketMenuCategoryBase ExpansionP2PMarketMenuCategoryBase)
            {
                ExpansionP2PMarketMenuCategoryBase.Excluded.Remove(currentTreeNode.Text);
                currentTreeNode.Remove();
            }
            else if (currentTreeNode.Parent.Parent.Tag is ExpansionPersonalStorageMenuCategory ExpansionPersonalStorageMenuCategory)
            {
                ExpansionPersonalStorageMenuCategory.Excluded.Remove(currentTreeNode.Text);
                currentTreeNode.Remove();
            }
            else if (currentTreeNode.Parent.Parent.Tag is ExpansionPersonalStorageMenuSubCategory ExpansionPersonalStorageMenuSubCategory)
            {
                ExpansionPersonalStorageMenuSubCategory.Excluded.Remove(currentTreeNode.Text);
                currentTreeNode.Remove();
            }
            else if (currentTreeNode.Parent.Parent.Tag is ExpansionPersonalStorageNewSettingsConfig ExpansionPersonalStorageNewConfig)
            {
                ExpansionPersonalStorageNewConfig.Data.ExcludedItems.Remove(currentTreeNode.Text);
                currentTreeNode.Remove();
            }
            else if (currentTreeNode.Parent.Parent.Tag is ExpansionPersonalStorageSettingsConfig ExpansionPersonalStorageConfig)
            {
                ExpansionPersonalStorageConfig.Data.ExcludedClassNames.Remove(currentTreeNode.Text);
                currentTreeNode.Remove();
            }
        }
        private void addNewP2PTraderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Newid = _expansionManager.ExpansionP2pMarketTradersConfig.GetNextID();
            ExpansionP2PMarketTraderConfig P2PTrader = _expansionManager.ExpansionP2pMarketTradersConfig.AddNewP2PTraderFile(Newid);
            CreateExpansionP2PMarketTraderNodes(P2PTrader, currentTreeNode);
        }
        private void removeP2PTraderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTreeNode.Tag is ExpansionP2PMarketTraderConfig ExpansionP2PMarketTraderConfig)
            {
                _expansionManager.ExpansionP2pMarketTradersConfig.RemoveFile(ExpansionP2PMarketTraderConfig);
                currentTreeNode.Remove();
            }
        }
        private void addNewExcludedStorageSlotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddFromList newform = new AddFromList();
            newform.List = File.ReadAllLines("Data/ExpansionSlotnames.txt").ToList();
            DialogResult result = newform.ShowDialog();
            if (result == DialogResult.OK)
            {
                ExpansionPersonalStorageLevel ExpansionPersonalStorageLevel = currentTreeNode.Parent.Tag as ExpansionPersonalStorageLevel;
                List<string> returnlist = newform.GetSelected;
                foreach (string faction in returnlist)
                {
                    if (!ExpansionPersonalStorageLevel.ExcludedSlots.Contains(faction))
                    {
                        ExpansionPersonalStorageLevel.ExcludedSlots.Add(faction);
                        currentTreeNode.Nodes.Add(new TreeNode(faction)
                        {
                            Tag = "StorageLevelExludedSlot"
                        });
                    }
                    currentTreeNode.Expand();
                }
            }
        }
        private void removeExcludedStorageSlotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionPersonalStorageLevel ExpansionPersonalStorageLevel = currentTreeNode.Parent.Parent.Tag as ExpansionPersonalStorageLevel;
            ExpansionPersonalStorageLevel.ExcludedSlots.Remove(currentTreeNode.Text);
            currentTreeNode.Remove();
        }
        //Personal Storage
        private void addNewPersonalStorageConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Newid = _expansionManager.ExpansionPersonalStorageContainersConfig.GetNextID();
            ExpansionPersonalStorageConfig ExpansionPersonalStorageConfig = _expansionManager.ExpansionPersonalStorageContainersConfig.AddNewPersonalStorageFile(Newid);
            CreateExpansionPersonalStorageConfigsNodes(ExpansionPersonalStorageConfig, currentTreeNode);
        }
        private void removePersonalStorageConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTreeNode.Tag is ExpansionPersonalStorageConfig ExpansionPersonalStorageConfig)
            {
                _expansionManager.ExpansionPersonalStorageContainersConfig.RemoveFile(ExpansionPersonalStorageConfig);
                currentTreeNode.Remove();
            }
        }

        //Raid Settings
        private void AddNewExplosiveWhitelistItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes { };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    if (!_expansionManager.ExpansionRaidConfig.Data.ExplosiveDamageWhitelist.Contains(l))
                    {
                        _expansionManager.ExpansionRaidConfig.Data.ExplosiveDamageWhitelist.Add(l);
                        currentTreeNode.Nodes.Add(new TreeNode(l)
                        {
                            Tag = "RaidExplosiveWhiteListItem"
                        });
                    }
                }
            }
        }
        private void RemoveExplosiveWhitelistItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionRaidConfig.Data.ExplosiveDamageWhitelist.Remove(currentTreeNode.Text);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
        }
        private void AddNewSafeRaidToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes { };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    if (!_expansionManager.ExpansionRaidConfig.Data.SafeRaidTools.Contains(l))
                    {
                        _expansionManager.ExpansionRaidConfig.Data.SafeRaidTools.Add(l);
                        currentTreeNode.Nodes.Add(new TreeNode(l)
                        {
                            Tag = "RaidSafeRaidTool"
                        });
                    }
                }
            }
        }
        private void RemoveSafeRaidToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionRaidConfig.Data.SafeRaidTools.Remove(currentTreeNode.Text);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
        }
        private void AddNewBarbedWireRaidToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes { };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    if (!_expansionManager.ExpansionRaidConfig.Data.BarbedWireRaidTools.Contains(l))
                    {
                        _expansionManager.ExpansionRaidConfig.Data.BarbedWireRaidTools.Add(l);
                        currentTreeNode.Nodes.Add(new TreeNode(l)
                        {
                            Tag = "RaidBarbedWireRaidTool"
                        });
                    }
                }
            }
        }
        private void RemoveBarbedWireRaidToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionRaidConfig.Data.BarbedWireRaidTools.Remove(currentTreeNode.Text);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
        }
        private void AddNewLockOnContainerRaidToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes { };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    if (!_expansionManager.ExpansionRaidConfig.Data.LockOnContainerRaidTools.Contains(l))
                    {
                        _expansionManager.ExpansionRaidConfig.Data.LockOnContainerRaidTools.Add(l);
                        currentTreeNode.Nodes.Add(new TreeNode(l)
                        {
                            Tag = "RaidContainerRaidTool"
                        });
                    }
                }
            }
        }
        private void RemoveLockOnContainerRaidToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionRaidConfig.Data.LockOnContainerRaidTools.Remove(currentTreeNode.Text);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
        }
        private void AddNewLockRaidToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes { };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    if (!_expansionManager.ExpansionRaidConfig.Data.LockRaidTools.Contains(l))
                    {
                        _expansionManager.ExpansionRaidConfig.Data.LockRaidTools.Add(l);
                        currentTreeNode.Nodes.Add(new TreeNode(l)
                        {
                            Tag = "RaidLockRaidTool"
                        });
                    }
                }
            }
        }
        private void RemoveLockRaidToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionRaidConfig.Data.LockRaidTools.Remove(currentTreeNode.Text);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
        }
        private void AddNewRaidScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionRaidSchedule newsched = new ExpansionRaidSchedule()
            {
                Weekday = "SUNDAY",
                StartHour = 0,
                StartMinute = 0,
                DurationMinutes = 0
            };
            _expansionManager.ExpansionRaidConfig.Data.Schedule.Add(newsched);
            currentTreeNode.Nodes.Add(new TreeNode(newsched.ToString())
            {
                Tag = newsched
            });
            ExpansionTV.SelectedNode = currentTreeNode.LastNode;
        }
        private void RemoveRaidScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionRaidConfig.Data.Schedule.Remove(currentTreeNode.Tag as ExpansionRaidSchedule);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
        }
        //SafeZone
        private void AddNewSafeZoneCircleZoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float pos = AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize / 2;
            ExpansionSafeZoneCircle newzone = new ExpansionSafeZoneCircle()
            {
                CircleSafeZoneName = "Circle Zone " + _expansionManager.ExpansionSafeZoneConfig.Data.CircleZones.Count.ToString(),
                Center = new Vec3(pos, 0, pos),
                Radius = 500
            };
            if (MapData.FileExists)
            {
                newzone.Center.Y = (MapData.gethieght(pos, pos));
            }
            _expansionManager.ExpansionSafeZoneConfig.Data.CircleZones.Add(newzone);
            currentTreeNode.Nodes.Add(new TreeNode(newzone.ToString())
            {
                Tag = newzone
            });
            ExpansionTV.SelectedNode = currentTreeNode.LastNode;
        }
        private void RemovesafeZoneCircleZoneToolStripmenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionSafeZoneConfig.Data.CircleZones.Remove(currentTreeNode.Tag as ExpansionSafeZoneCircle);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
        }
        private void AddNewsafeZonePolygonZoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float pos = AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize / 2;
            float halfSize = 50f;
            ExpansionSafeZonePolygon newzone = new ExpansionSafeZonePolygon()
            {
                polygonSafeZoneName = "Polygon Zone " + _expansionManager.ExpansionSafeZoneConfig.Data.PolygonZones.Count.ToString(),
                Positions = new BindingList<Vec3>()
                {
                    new Vec3(pos - halfSize, 0, pos - halfSize),
                    new Vec3(pos + halfSize, 0, pos - halfSize),
                    new Vec3(pos + halfSize, 0, pos + halfSize),
                    new Vec3(pos - halfSize, 0, pos + halfSize),
                }
            };
            if (MapData.FileExists)
            {
                foreach (Vec3 v3 in newzone.Positions)
                {
                    v3.Y = (MapData.gethieght(v3.X, v3.Z));
                }
            }
            _expansionManager.ExpansionSafeZoneConfig.Data.PolygonZones.Add(newzone);
            TreeNode SFP = new TreeNode(newzone.ToString())
            {
                Tag = newzone
            };
            foreach (Vec3 v3 in newzone.Positions)
            {
                SFP.Nodes.Add(new TreeNode(v3.GetString())
                {
                    Tag = v3
                });
            }
            currentTreeNode.Nodes.Add(SFP);
            ExpansionTV.SelectedNode = currentTreeNode.LastNode;
        }
        private void RemoveSafeZonePolygonZoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionSafeZoneConfig.Data.PolygonZones.Remove(currentTreeNode.Tag as ExpansionSafeZonePolygon);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
        }
        private void AddNewSafeZonePolygonPointtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionSafeZonePolygon ExpansionSafeZonePolygon = currentTreeNode.FindParentOfType<ExpansionSafeZonePolygon>();
            if (ExpansionSafeZonePolygon.Positions.Count == null)
                ExpansionSafeZonePolygon.Positions = new BindingList<Vec3>();

            Vec3 newvec3 = null;
            if (ExpansionSafeZonePolygon.Positions.Count == 0)
            {
                newvec3 = new Vec3((float)AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize / 2, 0f, (float)AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize / 2);
                if (MapData.FileExists)
                {
                    newvec3.Y = (MapData.gethieght(newvec3.X, newvec3.Z));
                }
            }
            else
            {
                Vec3 vec3 = ExpansionSafeZonePolygon.Positions.Last();
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
            ExpansionSafeZonePolygon.Positions.Add(newvec3);
            ExpansionTV.SelectedNode = newvec3node;
        }
        private void removeSafeZonePolygonPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionSafeZonePolygon ExpansionSafeZonePolygon = currentTreeNode.FindParentOfType<ExpansionSafeZonePolygon>();
            ExpansionSafeZonePolygon.Positions.Remove(currentTreeNode.Tag as Vec3);
            currentTreeNode.Remove();
        }
        private void moveSafeZonePolygonPointUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionSafeZonePolygon ExpansionSafeZonePolygon = currentTreeNode.FindParentOfType<ExpansionSafeZonePolygon>();
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
                ExpansionSafeZonePolygon.Positions.RemoveAt(index);
                ExpansionSafeZonePolygon.Positions.Insert(index - 1, waypoint);
                siblings.Insert(index - 1, currentTreeNode);
                ExpansionTV.SelectedNode = currentTreeNode;
            }
        }
        private void moveSafeZonePolygonPointDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionSafeZonePolygon ExpansionSafeZonePolygon = currentTreeNode.FindParentOfType<ExpansionSafeZonePolygon>();
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
                ExpansionSafeZonePolygon.Positions.RemoveAt(index);
                ExpansionSafeZonePolygon.Positions.Insert(index + 1, waypoint);
                siblings.Insert(index + 1, currentTreeNode);
                ExpansionTV.SelectedNode = currentTreeNode;
            }
        }
        private void AddNewSafeZoneCylinderZoneToolStripmenuItem_Click(object sender, EventArgs e)
        {
            float pos = AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize / 2;
            ExpansionSafeZoneCylinder newzone = new ExpansionSafeZoneCylinder()
            {
                CylinderSafeZoneName = "Cylinder Zone " + _expansionManager.ExpansionSafeZoneConfig.Data.CylinderZones.Count.ToString(),
                Center = new Vec3(pos, 0, pos),
                Radius = 500,
                Height = 500
            };
            if (MapData.FileExists)
            {
                newzone.Center.Y = (MapData.gethieght(pos, pos));
            }
            _expansionManager.ExpansionSafeZoneConfig.Data.CylinderZones.Add(newzone);
            currentTreeNode.Nodes.Add(new TreeNode(newzone.ToString())
            {
                Tag = newzone
            });
            ExpansionTV.SelectedNode = currentTreeNode.LastNode;
        }
        private void RemoveSafeZOneCylinderZoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionSafeZoneConfig.Data.CylinderZones.Remove(currentTreeNode.Tag as ExpansionSafeZoneCylinder);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
        }
        private void AddSafeZoneForceCleanUpItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes { };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    if (!_expansionManager.ExpansionSafeZoneConfig.Data.ForceSZCleanup_ExcludedItems.Contains(l))
                    {
                        _expansionManager.ExpansionSafeZoneConfig.Data.ForceSZCleanup_ExcludedItems.Add(l);
                        currentTreeNode.Nodes.Add(new TreeNode(l)
                        {
                            Tag = "ForceSZCleanup_ExcludedItem"
                        });
                    }
                }
            }
        }
        private void RemoveSafeZoneForcecleanUpItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionSafeZoneConfig.Data.ForceSZCleanup_ExcludedItems.Remove(currentTreeNode.Text);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
        }
        //Spawn
        private void removeLoadoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTreeNode.Parent.Tag.ToString() == "MaleLoadouts")
            {
                _expansionManager.ExpansionSpawnConfig.Data.MaleLoadouts.Remove(currentTreeNode.Tag as ExpansionSpawnGearLoadouts);
            }
            else if (currentTreeNode.Parent.Tag.ToString() == "FemaleLoadouts")
            {
                _expansionManager.ExpansionSpawnConfig.Data.FemaleLoadouts.Remove(currentTreeNode.Tag as ExpansionSpawnGearLoadouts);
            }
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
        }
        private void addNewSpawnLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionSpawnLocation newspawnlocation = new ExpansionSpawnLocation()
            {
                Name = "New Spawn Location",
                Positions = new BindingList<Vec3>(),
                UseCooldown = 1
            };
            _expansionManager.ExpansionSpawnConfig.Data.SpawnLocations.Add(newspawnlocation);
            TreeNode SLNode = new TreeNode(newspawnlocation.ToString())
            {
                Tag = newspawnlocation
            };
            foreach (Vec3 v3 in newspawnlocation.Positions)
            {
                SLNode.Nodes.Add(new TreeNode(v3.GetString())
                {
                    Tag = v3
                });
            }
            currentTreeNode.Nodes.Add(SLNode);
            ExpansionTV.SelectedNode = SLNode;
            SLNode.EnsureVisible();
        }
        private void removeSpawnLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionSpawnConfig.Data.SpawnLocations.Remove(currentTreeNode.Tag as ExpansionSpawnLocation);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
        }
        private void addNewSpawnPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionSpawnLocation ExpansionSpawnLocation = currentTreeNode.Tag as ExpansionSpawnLocation;
            float pos = AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize / 2;
            Vec3 v3 = new Vec3(pos, 0, pos);
            if (ExpansionSpawnLocation.Positions.Count > 0)
            {
                Vec3 vec3 = ExpansionSpawnLocation.Positions.Last();
                v3 = new Vec3(vec3.X + 25, 0f, vec3.Z);
            }
            if (MapData.FileExists)
            {
                v3.Y = (MapData.gethieght(pos, pos));
            }

            ExpansionSpawnLocation.Positions.Add(v3);
            currentTreeNode.Nodes.Add(new TreeNode(v3.ToString())
            {
                Tag = v3
            });
            ExpansionTV.SelectedNode = currentTreeNode.LastNode;
        }
        private void removeSpawnPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionSpawnLocation ExpansionSpawnLocation = currentTreeNode.FindParentOfType<ExpansionSpawnLocation>();
            ExpansionSpawnLocation.Positions.Remove(currentTreeNode.Tag as Vec3);
            currentTreeNode.Remove();
        }
        private void moveSpawnPointUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionSpawnLocation ExpansionSpawnLocation = currentTreeNode.FindParentOfType<ExpansionSpawnLocation>();
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
                ExpansionSpawnLocation.Positions.RemoveAt(index);
                ExpansionSpawnLocation.Positions.Insert(index - 1, waypoint);
                siblings.Insert(index - 1, currentTreeNode);
                ExpansionTV.SelectedNode = currentTreeNode;
            }
        }
        private void moveSpawnPointDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionSpawnLocation ExpansionSpawnLocation = currentTreeNode.FindParentOfType<ExpansionSpawnLocation>();
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
                ExpansionSpawnLocation.Positions.RemoveAt(index);
                ExpansionSpawnLocation.Positions.Insert(index + 1, waypoint);
                siblings.Insert(index + 1, currentTreeNode);
                ExpansionTV.SelectedNode = currentTreeNode;
            }
        }
        private void addStartingClothingItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> Addeditems = new List<string>();
            AddItemfromTypes form = new AddItemfromTypes { };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    if (!Addeditems.Contains(l))
                    {
                        Addeditems.Add(l);
                    }
                }
            }
            string clothingType = currentTreeNode.Text;
            ExpansionStartingClothing ExpansionStartingClothing = _expansionManager.ExpansionSpawnConfig.Data.StartingClothing;
            var prop = typeof(ExpansionStartingClothing)
                .GetProperty(clothingType, BindingFlags.Public | BindingFlags.Instance);

            var list = (BindingList<string>)prop.GetValue(ExpansionStartingClothing);
            if (list == null)
            {
                list = new BindingList<string>();
                prop.SetValue(ExpansionStartingClothing, list);
            }

            foreach (var item in Addeditems.Distinct())
            {
                if (!list.Contains(item))
                {
                    list.Add(item);
                    currentTreeNode.Nodes.Add(new TreeNode(item) { Tag = "StartingClothingItem" });
                }
            }
            ExpansionTV.SelectedNode = currentTreeNode.LastNode;
        }
        private void removeStartingClothingItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string clothingType = currentTreeNode.Parent.Text;
            ExpansionStartingClothing ExpansionStartingClothing = _expansionManager.ExpansionSpawnConfig.Data.StartingClothing;
            var prop = typeof(ExpansionStartingClothing)
                .GetProperty(clothingType, BindingFlags.Public | BindingFlags.Instance);

            var list = (BindingList<string>)prop.GetValue(ExpansionStartingClothing);
            if (list.Contains(currentTreeNode.Text))
            {
                list.Remove(currentTreeNode.Text);
                currentTreeNode.Remove();
            }
        }
        private void AddStartingGearItem(ExpansionStartingGearItem startingGearItem)
        {
            switch (currentTreeNode.Text)
            {
                case "UpperGear":
                    if (!_expansionManager.ExpansionSpawnConfig.Data.StartingGear.UpperGear.Any(x => x == startingGearItem))
                        _expansionManager.ExpansionSpawnConfig.Data.StartingGear.UpperGear.Add(startingGearItem);
                    break;
                case "PantsGear":
                    if (!_expansionManager.ExpansionSpawnConfig.Data.StartingGear.PantsGear.Any(x => x == startingGearItem))
                        _expansionManager.ExpansionSpawnConfig.Data.StartingGear.PantsGear.Add(startingGearItem);
                    break;
                case "BackpackGear":
                    if (!_expansionManager.ExpansionSpawnConfig.Data.StartingGear.BackpackGear.Any(x => x == startingGearItem))
                        _expansionManager.ExpansionSpawnConfig.Data.StartingGear.BackpackGear.Add(startingGearItem);
                    break;
                case "VestGear":
                    if (!_expansionManager.ExpansionSpawnConfig.Data.StartingGear.VestGear.Any(x => x == startingGearItem))
                        _expansionManager.ExpansionSpawnConfig.Data.StartingGear.VestGear.Add(startingGearItem);
                    break;
                case "PrimaryWeapon":
                    _expansionManager.ExpansionSpawnConfig.Data.StartingGear.PrimaryWeapon = startingGearItem;
                    break;
                case "SecondaryWeapon":
                    _expansionManager.ExpansionSpawnConfig.Data.StartingGear.SecondaryWeapon = startingGearItem;
                    break;
            }
        }
        private void addStartingGearItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes { };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    ExpansionStartingGearItem newExpansionStartingGearItem = new ExpansionStartingGearItem()
                    {
                        ClassName = l,
                        Attachments = new BindingList<string>(),
                        Quantity = -1
                    };
                    AddStartingGearItem(newExpansionStartingGearItem);
                    TreeNode Itemnode = new TreeNode(newExpansionStartingGearItem.ClassName)
                    {
                        Tag = newExpansionStartingGearItem
                    };
                    TreeNode itemAttchmentsNode = new TreeNode("Attachments")
                    {
                        Tag = "ExpansionStartingGearItemAttachments"
                    };
                    foreach (string itemclassanme in newExpansionStartingGearItem.Attachments)
                    {
                        itemAttchmentsNode.Nodes.Add(new TreeNode(itemclassanme)
                        {
                            Tag = "ExpansionStartingGearItemAttachment"
                        });
                    }
                    Itemnode.Nodes.Add(itemAttchmentsNode);
                    currentTreeNode.Nodes.Add(Itemnode);
                }
            }
            ExpansionTV.SelectedNode = currentTreeNode.LastNode;
        }
        private void removeStartingGearItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (currentTreeNode.Parent.Text)
            {
                case "UpperGear":
                    _expansionManager.ExpansionSpawnConfig.Data.StartingGear.UpperGear.Remove(currentTreeNode.Tag as ExpansionStartingGearItem);
                    break;
                case "PantsGear":
                    _expansionManager.ExpansionSpawnConfig.Data.StartingGear.PantsGear.Remove(currentTreeNode.Tag as ExpansionStartingGearItem);
                    break;
                case "BackpackGear":
                    _expansionManager.ExpansionSpawnConfig.Data.StartingGear.BackpackGear.Remove(currentTreeNode.Tag as ExpansionStartingGearItem);
                    break;
                case "VestGear":
                    _expansionManager.ExpansionSpawnConfig.Data.StartingGear.VestGear.Remove(currentTreeNode.Tag as ExpansionStartingGearItem);
                    break;
            }
            currentTreeNode.Remove();
        }
        private void addStartingGearAttachmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionStartingGearItem ExpansionStartingGearItem = currentTreeNode.Parent.Tag as ExpansionStartingGearItem;
            AddItemfromTypes form = new AddItemfromTypes { };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    if (!ExpansionStartingGearItem.Attachments.Contains(l))
                    {
                        ExpansionStartingGearItem.Attachments.Add(l);
                        currentTreeNode.Nodes.Add(new TreeNode(l)
                        {
                            Tag = "ExpansionStartingGearItemAttachment"
                        });
                    }
                }
            }
        }
        private void removeStartingGearAttachmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpansionStartingGearItem ExpansionStartingGearItem = currentTreeNode.Parent.Parent.Tag as ExpansionStartingGearItem;
            ExpansionStartingGearItem.Attachments.Remove(currentTreeNode.Text);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
        }
        private void addStartingWeaponToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes
            {
                UseOnlySingleItem = true
            };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    ExpansionStartingGearItem newExpansionStartingGearItem = new ExpansionStartingGearItem()
                    {
                        ClassName = l,
                        Attachments = new BindingList<string>(),
                        Quantity = -1
                    };
                    AddStartingGearItem(newExpansionStartingGearItem);
                    TreeNode Itemnode = new TreeNode(newExpansionStartingGearItem.ClassName)
                    {
                        Tag = newExpansionStartingGearItem
                    };
                    TreeNode itemAttchmentsNode = new TreeNode("Attachments")
                    {
                        Tag = "ExpansionStartingGearItemAttachments"
                    };
                    foreach (string itemclassanme in newExpansionStartingGearItem.Attachments)
                    {
                        itemAttchmentsNode.Nodes.Add(new TreeNode(itemclassanme)
                        {
                            Tag = "ExpansionStartingGearItemAttachment"
                        });
                    }
                    Itemnode.Nodes.Add(itemAttchmentsNode);
                    currentTreeNode.Nodes.Add(Itemnode);
                }
            }
            ExpansionTV.SelectedNode = currentTreeNode.LastNode;

        }
        private void removeStartingWeaponToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (currentTreeNode.Parent.Text)
            {
                case "PrimaryWeapon":
                    _expansionManager.ExpansionSpawnConfig.Data.StartingGear.PrimaryWeapon = null;
                    break;
                case "SecondaryWeapon":
                    _expansionManager.ExpansionSpawnConfig.Data.StartingGear.SecondaryWeapon = null;
                    break;
            }
            currentTreeNode.Remove();
        }
        //Vehicle
        private void addNewToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes { };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    AddNewVehicleLockTool(l);
                }
            }
        }
        private void AddNewVehicleLockTool(string Classname)
        {
            switch (currentTreeNode.Tag.ToString())
            {
                case "VehiclePickLockTools":
                    if (!_expansionManager.ExpansionVehiclesConfig.Data.PickLockTools.Contains(Classname))
                    {
                        _expansionManager.ExpansionVehiclesConfig.Data.PickLockTools.Add(Classname);
                        currentTreeNode.Nodes.Add(new TreeNode(Classname)
                        {
                            Tag = "VehiclePickLockTool"
                        });
                    }
                    break;
                case "VehicleChangeLockTools":
                    if (!_expansionManager.ExpansionVehiclesConfig.Data.ChangeLockTools.Contains(Classname))
                    {
                        _expansionManager.ExpansionVehiclesConfig.Data.ChangeLockTools.Add(Classname);
                        currentTreeNode.Nodes.Add(new TreeNode(Classname)
                        {
                            Tag = "VehicleChangeLockTool"
                        });
                    }
                    break;
            }
        }
        private void removeToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (currentTreeNode.Parent.Tag.ToString())
            {
                case "VehiclePickLockTools":
                    _expansionManager.ExpansionVehiclesConfig.Data.PickLockTools.Remove(currentTreeNode.Text);
                    break;
                case "VehicleChangeLockTools":
                    _expansionManager.ExpansionVehiclesConfig.Data.ChangeLockTools.Remove(currentTreeNode.Text);
                    break;
            }
            currentTreeNode.Remove();
        }
        private void addNewVehicleConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes { };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    if (!_expansionManager.ExpansionVehiclesConfig.Data.VehiclesConfig.Any(x => x.ClassName == l))
                    {
                        ExpansionVehiclesLockConfig newvc = new ExpansionVehiclesLockConfig()
                        {
                            ClassName = l,
                            LockComplexity = 10
                        };
                        _expansionManager.ExpansionVehiclesConfig.Data.VehiclesConfig.Add(newvc);
                        currentTreeNode.Nodes.Add(new TreeNode(newvc.ClassName)
                        {
                            Tag = newvc
                        });
                    }
                }
                ExpansionTV.SelectedNode = currentTreeNode.LastNode;
            }
        }
        private void removeVehicleConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _expansionManager.ExpansionVehiclesConfig.Data.VehiclesConfig.Remove(currentTreeNode.Tag as ExpansionVehiclesLockConfig);
            currentTreeNode.Remove();
        }
        //Quests
        private void questFlowPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {

            IReadOnlyDictionary<int, ExpansionQuestQuest> questsByIdDictionary =
                _expansionManager.ExpansionQuestQuestConfig.MutableItems
                    .Where(q => q.ID.HasValue)
                    .ToDictionary(q => q.ID!.Value);

            var form = new QuestFlowPreviewForm(
                currentTreeNode.Tag as ExpansionQuestQuest,
                questsByIdDictionary);

            form.Show(this);

        }
        #endregion right click methods

        #region Search Treeview
        private List<TreeNode> _searchResults = new();
        private int _currentIndex = -1;


        private void button2_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchText)) return;

            _searchResults.Clear();
            _currentIndex = -1;

            FindAllNodes(ExpansionTV.Nodes, searchText);

            if (_searchResults.Count > 0)
            {
                _currentIndex = 0;
                SelectNode(_searchResults[_currentIndex]);
            }
            else
            {
                MessageBox.Show("No matches found.");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (_searchResults.Count == 0) return;


            _currentIndex++;
            if (_currentIndex >= _searchResults.Count)
            {
                MessageBox.Show("No more matches.");
                _currentIndex = _searchResults.Count - 1;
                return;
            }
            ExpansionTV.CollapseAll();
            SelectNode(_searchResults[_currentIndex]);
        }
        private void FindAllNodes(TreeNodeCollection nodes, string searchText)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Text.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                    _searchResults.Add(node);

                FindAllNodes(node.Nodes, searchText);
            }
        }
        private void SelectNode(TreeNode node)
        {
            ExpansionTV.SelectedNode = node;
            node.EnsureVisible();
        }

        #endregion search treeview








    }

    [PluginInfo("Expansion Manager", "ExpansionPlugin", "ExpansionPlugin.Expansion.png")]
    public class PluginExspansion : IPluginForm, IDisposable
    {
        private bool disposed = false;

        public string pluginIdentifier => "ExpansionPlugin";
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
