using Day2eEditor;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using System.Xml.Linq;

namespace EconomyPlugin
{
    public partial class EconomyForm : Form
    {
        private IUIHandler? _currentHandler;
        public MapViewerControl MapControl => _mapControl;
        private EconomyManager _economyManager;
        private readonly ProjectManager _projectManager;
        private IPluginForm _plugin;
        private TreeNode? currentTreeNode;

        #region Ditionarys
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
        public EconomyForm(IPluginForm plugin)
        {
            InitializeComponent();
            _plugin = plugin;
            _economyManager = AppServices.GetRequired<EconomyManager>();
            _projectManager = AppServices.GetRequired<ProjectManager>();
            initializeShowControlHandlers();
            InitializeContextMenuHandlers();
        }
        private void initializeShowControlHandlers()
        {
            // ----------------------
            // Type handlers
            // ----------------------
            _typeHandlers = new Dictionary<Type, Action<TreeNode, List<TreeNode>>>
            {
                // Core files
                [typeof(EconomyFile)] = (node, selected) => ShowHandler<IUIHandler>(null, null, null, selected),
                [typeof(EconomySection)] = (node, selected) =>
                {
                    EconomyFile ef = node.Parent.Tag as EconomyFile;
                    if (ef.IsModded)
                        ShowHandler(new economyControl(), typeof(EconomyFile), node.Tag as EconomySection, selected);
                    else
                        ShowHandler<IUIHandler>(null, null, null, selected);
                },
                [typeof(GlobalsFile)] = (node, selected) => ShowHandler<IUIHandler>(null, null, null, selected),
                [typeof(variablesVar)] = (node, selected) =>
                {
                    GlobalsFile ef = node.Parent.Tag as GlobalsFile;
                    if (ef.IsModded)
                        ShowHandler(new VariablesVarControl(), typeof(GlobalsFile), node.Tag as variablesVar, selected);
                    else
                        ShowHandler<IUIHandler>(null, null, null, selected);
                },
                [typeof(TypesFile)] = (node, selected) =>
                    ShowHandler(new TypesCollectionControl(), typeof(TypesFile), node.Tag as TypesFile, selected),
                [typeof(Category)] = (node, selected) =>
                {
                    if (node.Parent != null)
                        ShowHandler(new TypesCollectionControl(), typeof(TypesFile), node.Parent.Tag as TypesFile, selected);
                },
                [typeof(TypeEntry)] = (node, selected) =>
                {
                    var typentry = node.Tag as TypeEntry;
                    var matches = _economyManager.TypesConfig.MutableItems
                        .SelectMany(tf => tf.Data.TypeList.Select(te => (File: tf, Entry: te)))
                        .Where(x => x.Entry.Name == typentry.Name)
                        .ToList();

                    var latest = matches.LastOrDefault();
                    if (latest != default && !ReferenceEquals(latest.Entry, typentry))
                    {
                        if (MessageBox.Show(
                                $"This type is overridden by a later definition in:\n{latest.File.FileName}\n\nJump to it?",
                                "Type Override Found", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            var foundNode = FindNodeByTag(EconomyTV.Nodes, latest.Entry);
                            if (foundNode != null) EconomyTV.SelectedNode = foundNode;
                        }
                        else
                            ShowHandler(new TypesControl(), typeof(TypesFile), typentry, selected);
                    }
                    else
                        ShowHandler(new TypesControl(), typeof(TypesFile), typentry, selected);
                },

                // Events
                [typeof(eventsEvent)] = (node, selected) =>
                {
                    var ev = node.Tag as eventsEvent;
                    var matches = _economyManager.eventsConfig.MutableItems
                        .SelectMany(tf => tf.Data.@event.Select(te => (File: tf, Event: te)))
                        .Where(x => x.Event.name == ev.name)
                        .ToList();

                    var latest = matches.LastOrDefault();
                    if (latest != default && !ReferenceEquals(latest.Event, ev))
                    {
                        if (MessageBox.Show(
                                $"This Event is overridden by a later definition in:\n{latest.File.FileName}\n\nJump to it?",
                                "Event Override Found", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            var foundNode = FindNodeByTag(EconomyTV.Nodes, latest.Event);
                            if (foundNode != null) EconomyTV.SelectedNode = foundNode;
                        }
                        else
                            ShowHandler(new EventsControl(), typeof(EventsFile), ev, selected);
                    }
                    else
                        ShowHandler(new EventsControl(), typeof(EventsFile), ev, selected);
                },
                [typeof(eventposdefEventZone)] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(new eventspawnZoneControl(), typeof(cfgeventspawnsConfig), node.Tag as eventposdefEventZone, selected);
                },
                [typeof(eventgroupdefGroup)] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(new eventspawngroupnameControl(), typeof(cfgeventgroupsConfig), node.Tag as eventgroupdefGroup, selected);
                },
                [typeof(eventgroupdefGroupChild)] = (node, selected) =>
                {
                    ShowHandler<IUIHandler>(new eventspawngroupchildinfoControl(), typeof(cfgeventgroupsConfig), node.Tag as eventgroupdefGroupChild, selected);
                },
                // Map-related
                [typeof(eventposdefEventPos)] = (node, selected) =>
                {
                    var data = node.Tag as eventposdefEventPos;
                    var control = new eventgroupsspawnpositionControl();
                    control.PositionChanged += (updatedPos) =>
                    {
                        _mapControl.ClearDrawables(); ;
                        DrawEventSpawns(node.Parent.Parent.Tag as eventposdefEvent);
                    };

                    ShowHandler<IUIHandler>(control, typeof(cfgeventspawnsConfig), data, selected);
                    SetupEventPosMap(data, node);
                    _mapControl.EnsureVisible(new PointF((float)data.x, (float)data.z));
                },
                [typeof(cfgeffectareaSafePosition)] = (node, selected) =>
                {
                    cfgeffectareaSafePosition cfgeffectareaSafePosition = node.Tag as cfgeffectareaSafePosition;
                    ShowHandler<IUIHandler>(null, null, null, selected);
                    SetupSafePosMap(cfgeffectareaSafePosition, node);
                    _mapControl.EnsureVisible(new PointF((float)cfgeffectareaSafePosition.X, (float)cfgeffectareaSafePosition.Z));
                },
                [typeof(PRABoxes)] = (node, selected) =>
                {
                    PRABoxes PRABoxes = node.Tag as PRABoxes;
                    ShowHandler<IUIHandler>(null, null, null, selected);
                    SetupPRABoxMap(PRABoxes, node);
                    _mapControl.EnsureVisible(new PointF((float)PRABoxes.Position.X, (float)PRABoxes.Position.Z));
                },
                [typeof(PRASafePosition)] = (node, selected) =>
                {
                    PRASafePosition PRASafePosition = node.Tag as PRASafePosition;
                    ShowHandler<IUIHandler>(null, null, null, selected);
                    SetupPRASafePosMap(PRASafePosition, node);
                    _mapControl.EnsureVisible(new PointF((float)PRASafePosition.Position.X, (float)PRASafePosition.Position.Z));
                },
                [typeof(Areas)] = (node, selected) =>
                {
                    Areas area = node.Tag as Areas;
                    ShowHandler(new cfgeffectAreaMainControl(), typeof(CfgeffectareaConfig), area, selected);
                    SetupEffectAreaMap(node.Tag as Areas, node);
                    _mapControl.EnsureVisible(new PointF((float)area.Data.Pos[0], (float)area.Data.Pos[2]));
                },
                [typeof(playerspawnpointsGroupPos)] = (node, selected) =>
                {
                    playerspawnpointsGroupPos pos = node.Tag as playerspawnpointsGroupPos;
                    ShowHandler<IUIHandler>(null, null, null, selected);
                    SetupPlayerSpawnPosMap(pos, node);
                    _mapControl.EnsureVisible(new PointF((float)pos.x, (float)pos.z));
                },
                [typeof(mapgroupposConfig)] = (node, selected) =>
                {
                    mapgroupposConfig pos = node.Tag as mapgroupposConfig;
                    ShowHandler<IUIHandler>(null, null, null, selected);
                    SetupMapGroupMap(pos, node);
                },
                [typeof(mapGroup)] = (node, selected) =>
                {
                    mapGroup pos = node.Tag as mapGroup;
                    ShowHandler<IUIHandler>(null, null, null, selected);
                    SetupMapGroupPosMap(pos, node);
                    _mapControl.EnsureVisible(new PointF(Convert.ToSingle(pos.pos.Split(' ')[0]), Convert.ToSingle(pos.pos.Split(' ')[2])));
                },
                // territories 
                [typeof(territorytypeTerritoryZone)] = (node, selected) =>
                {
                    var control = new TerritoryZonesControl();
                    control.PositionChanged += (updatedPos) =>
                    {
                        _mapControl.ClearDrawables(); ;
                        DrawTerritoriesPositions(node.Parent.Tag as territorytypeTerritory);
                    };
                    territorytypeTerritoryZone zone = node.Tag as territorytypeTerritoryZone;
                    ShowHandler<IUIHandler>(control, typeof(territorytype), zone, selected);
                    SetupTerritoriesMap(zone, node);
                    _mapControl.EnsureVisible(new PointF((float)zone.x, (float)zone.z));
                },
                [typeof(territorytypeTerritory)] = (node, selected) =>
                    ShowHandler(new territorytypeTerritoryColourControl(), typeof(territorytype), node.Tag as territorytypeTerritory, selected),

                //economy
                [typeof(envTerritoriesTerritory)] = (node, selected) =>
                    ShowHandler(new envTerritoriesTerritoryControl(), typeof(CfgenvironmentConfig), node.Tag as envTerritoriesTerritory, selected),

                //Economycore preview
                [typeof(economyCoreConfig)] = (node, selected) =>
                    ShowHandler(new cfgeconomycorePreviewControl(), typeof(economyCoreConfig), node.Tag as economyCoreConfig, selected),

                // SpawnGear / Discrete / Complex
                [typeof(Discreteitemset)] = (node, selected) =>
                    ShowHandler(new SpawnGearItemControl(), typeof(SpawnGearPresetFile), node.Tag as Discreteitemset, selected),
                [typeof(Discreteunsorteditemset)] = (node, selected) =>
                    ShowHandler(new SpawnGearNameControl(), typeof(SpawnGearPresetFile), node.Tag as Discreteunsorteditemset, selected),
                [typeof(Complexchildrentype)] = (node, selected) =>
                    ShowHandler(new SpawnGearItemControl(), typeof(SpawnGearPresetFile), node.Tag as Complexchildrentype, selected),
                [typeof(Attachmentslotitemset)] = (node, selected) =>
                    ShowHandler(new AttachmentslotitemsetControl(), typeof(SpawnGearPresetFile), node.Tag as Attachmentslotitemset, selected),

                // SpawnableTypes
                [typeof(SpawnableType)] = (node, selected) =>
                    ShowHandler(new SpawnabletypesControl(), typeof(CfgSpawnableTypesFile), node.Tag as SpawnableType, selected),
                [typeof(spawnableTypeItem)] = (node, selected) =>
                    ShowHandler(new SpawnableTypesItemControl(), typeof(CfgSpawnableTypesFile), node.Tag as spawnableTypeItem, selected),
                [typeof(spawnableTypeAttachment)] = (node, selected) =>
                    ShowHandler(new SpawnableTypesAttachmentsControl(), typeof(CfgSpawnableTypesFile), node.Tag as spawnableTypeAttachment, selected),
                [typeof(spawnableTypeCargo)] = (node, selected) =>
                    ShowHandler(new SpawnableTypesCargoControl(), typeof(CfgSpawnableTypesFile), node.Tag as spawnableTypeCargo, selected),
                [typeof(spawnableTypeDamage)] = (node, selected) =>
                    ShowHandler(new SpawnabletypesDamageControl(), typeof(CfgSpawnableTypesFile), node.Tag as spawnableTypeDamage, selected),
                [typeof(spawnableTypeTag)] = (node, selected) =>
                    ShowHandler(new SpawnabletypesTagsControl(), typeof(CfgSpawnableTypesFile), node.Tag as spawnableTypeTag, selected),

                // CFG Gameplay
                [typeof(Generaldata)] = (node, selected) =>
                    ShowHandler(new cfggameplayGeneralDataControl(), typeof(CFGGameplayConfig), node.Tag as Generaldata, selected),
                [typeof(Playerdata)] = (node, selected) =>
                    ShowHandler(new cfggameplayPlayerDataControl(), typeof(CFGGameplayConfig), node.Tag as Playerdata, selected),
                [typeof(Worldsdata)] = (node, selected) =>
                    ShowHandler(new cfggameplayWordlsDataControl(), typeof(CFGGameplayConfig), node.Tag as Worldsdata, selected),
                [typeof(Basebuildingdata)] = (node, selected) =>
                    ShowHandler(new cfggameplayBaseBuildingDataControl(), typeof(CFGGameplayConfig), node.Tag as Basebuildingdata, selected),
                [typeof(Uidata)] = (node, selected) =>
                    ShowHandler(new cfggameplayUIDataControl(), typeof(CFGGameplayConfig), node.Tag as Uidata, selected),
                [typeof(CFGGameplayMapData)] = (node, selected) =>
                    ShowHandler(new cfggameplayMapDataControl(), typeof(CFGGameplayConfig), node.Tag as CFGGameplayMapData, selected),
                [typeof(VehicleData)] = (node, selected) =>
                    ShowHandler(new cfggameplayVehicleDataControl(), typeof(CFGGameplayConfig), node.Tag as VehicleData, selected),

                // Random Presets
                [typeof(randompresetsAttachments)] = (node, selected) =>
                    ShowHandler(new RandompresetsAttchmentsControl(), typeof(CfgrandompresetsFile), node.Tag as randompresetsAttachments, selected),
                [typeof(randompresetsCargo)] = (node, selected) =>
                    ShowHandler(new RandompresetsCargoControl(), typeof(CfgrandompresetsFile), node.Tag as randompresetsCargo, selected),
                [typeof(randompresetsItem)] = (node, selected) =>
                    ShowHandler(new RandomPresetItemControl(), typeof(CfgrandompresetsFile), node.Tag as randompresetsItem, selected),

                // Vector / Areas
                [typeof(Vec3)] = (node, selected) =>
                {
                    var parent = node.FindParentOfType<PlayerRestrictedFile>();
                    if (parent != null)
                        ShowHandler(new Vector3Control(), typeof(PlayerRestrictedFile), node.Tag as Vec3, selected);
                },
                [typeof(Data)] = (node, selected) =>
                    ShowHandler<IUIHandler>(new cfgeffectAreaDataControl(), typeof(CfgeffectareaConfig), node.Tag as Data, selected),
                [typeof(PlayerData)] = (node, selected) =>
                    ShowHandler<IUIHandler>(new cfgeffectAreaPlayerDataControl(), typeof(CfgeffectareaConfig), node.Tag as PlayerData, selected),

                // Player spawns
                [typeof(playerspawnpointsSpawn_params)] = (node, selected) =>
                    ShowHandler<IUIHandler>(new cfgplayerspawnSpawnParamControl(), typeof(cfgplayerspawnpointsConfig), node.Tag as playerspawnpointsSpawn_params, selected),
                [typeof(playerspawnpointsGenerator_params)] = (node, selected) =>
                    ShowHandler<IUIHandler>(new cfgplayerspawnGeneratorParamsControl(), typeof(cfgplayerspawnpointsConfig), node.Tag as playerspawnpointsGenerator_params, selected),
                [typeof(playerspawnpointsGroup_params)] = (node, selected) =>
                    ShowHandler<IUIHandler>(new cfgplayerspawnGroupParamsControl(), typeof(cfgplayerspawnpointsConfig), node.Tag as playerspawnpointsGroup_params, selected),
                [typeof(playerspawnpointsGroup)] = (node, selected) =>
                    ShowHandler<IUIHandler>(new cfgplayerspawngroupControl(), typeof(cfgplayerspawnpointsConfig), node.Tag as playerspawnpointsGroup, selected),

                //weather xml
                [typeof(weatherOvercast)] = (node, selected) =>
                    ShowHandler<IUIHandler>(new cfgweatherOvercastControl(), typeof(cfgweatherConfig), node.Tag as weatherOvercast, selected),
                [typeof(weatherFog)] = (node, selected) =>
                   ShowHandler<IUIHandler>(new cfgweatherFogControl(), typeof(cfgweatherConfig), node.Tag as weatherFog, selected),
                [typeof(weatherRain)] = (node, selected) =>
                   ShowHandler<IUIHandler>(new cfgweatherRainControl(), typeof(cfgweatherConfig), node.Tag as weatherRain, selected),
                [typeof(weatherWindMagnitude)] = (node, selected) =>
                   ShowHandler<IUIHandler>(new cfgweatherWindMagnitudeControl(), typeof(cfgweatherConfig), node.Tag as weatherWindMagnitude, selected),
                [typeof(weatherWindDirection)] = (node, selected) =>
                    ShowHandler<IUIHandler>(new cfgweatherWindDirectionControl(), typeof(cfgweatherConfig), node.Tag as weatherWindDirection, selected),
                [typeof(weatherSnowfall)] = (node, selected) =>
                   ShowHandler<IUIHandler>(new cfgweatherSnowfallControl(), typeof(cfgweatherConfig), node.Tag as weatherSnowfall, selected),
                [typeof(weatherStorm)] = (node, selected) =>
                   ShowHandler<IUIHandler>(new cfgweatherStormControl(), typeof(cfgweatherConfig), node.Tag as weatherStorm, selected),

                //cfgundergroundtriggers
                [typeof(Trigger)] = (node, selected) =>
                   ShowHandler<IUIHandler>(new cfgundergroundtriggersTriggerControl(), typeof(cfgundergroundtriggersConfig), node.Tag as Trigger, selected),
                [typeof(Breadcrumb)] = (node, selected) =>
                   ShowHandler<IUIHandler>(new cfgundergroundtriggersBreadCrumbControl(), typeof(cfgundergroundtriggersConfig), node.Tag as Breadcrumb, selected),


                [typeof(prototypeGroup)] = (node, selected) =>
                   ShowHandler<IUIHandler>(new prototypeGroupControl(), typeof(mapgroupprotoConfig), node.Tag as prototypeGroup, selected),
                [typeof(prototypeGroupContainer)] = (node, selected) =>
                   ShowHandler<IUIHandler>(new prototypeGroupContainerControl(), typeof(mapgroupprotoConfig), node.Tag as prototypeGroupContainer, selected)



            };
            // ----------------------
            // String handlers
            // ----------------------
            _stringHandlers = new Dictionary<string, Action<TreeNode, List<TreeNode>>>
            {
                ["weatherEnable"] = (node, selected) =>
                {
                    cfgweatherConfig cfg = node.FindParentOfType<cfgweatherConfig>();
                    ShowHandler<IUIHandler>(new cfgweatherEnableControl(), typeof(cfgweatherConfig), cfg.Data, selected);
                },
                ["wetaherReset"] = (node, selected) =>
                {
                    cfgweatherConfig cfg = node.FindParentOfType<cfgweatherConfig>();
                    ShowHandler<IUIHandler>(new cfgweatherresetControl(), typeof(cfgweatherConfig), cfg.Data, selected);
                },
                ["DefsCategories"] = (node, selected) =>
                {
                    var cfg = node.FindParentOfType<cfglimitsdefinitionConfig>();
                    ShowHandler(new cfglimitsdefinitionCategoryControl(), typeof(cfglimitsdefinitionConfig), cfg, selected);
                },
                ["DefsTags"] = (node, selected) =>
                {
                    var cfg = node.FindParentOfType<cfglimitsdefinitionConfig>();
                    ShowHandler(new cfglimitsdefinitionTagsControl(), typeof(cfglimitsdefinitionConfig), cfg, selected);
                },
                ["DefsUsageFlags"] = (node, selected) =>
                {
                    var cfg = node.FindParentOfType<cfglimitsdefinitionConfig>();
                    ShowHandler(new cfglimitsdefinitionUagesControl(), typeof(cfglimitsdefinitionConfig), cfg, selected);
                },
                ["DefsValueFlags"] = (node, selected) =>
                {
                    var cfg = node.FindParentOfType<cfglimitsdefinitionConfig>();
                    ShowHandler(new cfglimitsdefinitionValueControl(), typeof(cfglimitsdefinitionConfig), cfg, selected);
                },
                ["DefsUserUsageFlags"] = (node, selected) =>
                {
                    var cfg = node.FindParentOfType<cfglimitsdefinitionuserConfig>();
                    ShowHandler(new cfglimitsdefinitionuserUsageControl(), typeof(cfglimitsdefinitionuserConfig), cfg, selected);
                },
                ["DefsUserValueFlags"] = (node, selected) =>
                {
                    var cfg = node.FindParentOfType<cfglimitsdefinitionuserConfig>();
                    ShowHandler(new cfglimitsdefinitionuserValueControl(), typeof(cfglimitsdefinitionuserConfig), cfg, selected);
                },
                ["SpawnGearAttachmentSlotItemSetsParent"] = (node, selected) =>
                    ShowHandler<IUIHandler>(null, null, null, selected),

                ["SpawnGearName"] = (node, selected) =>
                {
                    var preset = node.FindParentOfType<SpawnGearPresetFile>();
                    ShowHandler(new SpawnGearNameControl(), typeof(SpawnGearPresetFile), preset.Data, selected);
                },
                ["SpawnGearSpawnWeight"] = (node, selected) =>
                {
                    var preset = node.FindParentOfType<SpawnGearPresetFile>();
                    ShowHandler(new SpawnGearSpawnWeightControl(), typeof(SpawnGearPresetFile), preset.Data, selected);
                },
                ["SpawnGearCharacterTypes"] = (node, selected) =>
                {
                    var preset = node.FindParentOfType<SpawnGearPresetFile>();
                    ShowHandler(new SpawnGearCharacterTypesControl(), typeof(SpawnGearPresetFile), preset, selected);
                },

                ["DiscreteitemsetSpawnWeight"] = (node, selected) =>
                {
                    var ds = node.FindParentOfType<Discreteitemset>();
                    ShowHandler(new SpawnGearSpawnWeightControl(), typeof(SpawnGearPresetFile), ds, selected);
                },
                ["DiscreteitemsetQuickBarSlot"] = (node, selected) =>
                {
                    var ds = node.FindParentOfType<Discreteitemset>();
                    ShowHandler(new SpawnGearQuickBarSlotControl(), typeof(SpawnGearPresetFile), ds, selected);
                },
                ["DiscreteitemsetComplexChildrenTypes"] = (node, selected) =>
                    ShowHandler<IUIHandler>(null, null, null, selected),

                ["DiscreteitemsetSimpleChildrenTypes"] = (node, selected) =>
                {
                    var ds = node.FindParentOfType<Discreteitemset>();
                    ShowHandler(new SpawnGearSimpleChildrenControl(), typeof(SpawnGearPresetFile), ds, selected);
                },
                ["DiscreteitemsetSimpleChildrenUseDefaultAttributes"] = (node, selected) =>
                {
                    var ds = node.FindParentOfType<Discreteitemset>();
                    ShowHandler(new SpawnGearsimpleChildrenUseDefaultAttributesControl(), typeof(SpawnGearPresetFile), ds, selected);
                },

                ["DiscreteunsorteditemsetSpawnWeight"] = (node, selected) =>
                {
                    var dsu = node.FindParentOfType<Discreteunsorteditemset>();
                    ShowHandler(new SpawnGearSpawnWeightControl(), typeof(SpawnGearPresetFile), dsu, selected);
                },
                ["DiscreteunsorteditemsetAttributes"] = (node, selected) =>
                {
                    var dsu = node.FindParentOfType<Discreteunsorteditemset>();
                    ShowHandler(new SpawnGearAttributesControl(), typeof(SpawnGearPresetFile), dsu.attributes, selected);
                },
                ["DiscreteunsorteditemsetSimpleChildrenTypes"] = (node, selected) =>
                {
                    var dsu = node.FindParentOfType<Discreteunsorteditemset>();
                    ShowHandler(new SpawnGearSimpleChildrenControl(), typeof(SpawnGearPresetFile), dsu, selected);
                },
                ["DiscreteunsorteditemsetSimpleChildrenUseDefaultAttributes"] = (node, selected) =>
                {
                    var dsu = node.FindParentOfType<Discreteunsorteditemset>();
                    ShowHandler(new SpawnGearsimpleChildrenUseDefaultAttributesControl(), typeof(SpawnGearPresetFile), dsu, selected);
                },
                ["DiscreteunsorteditemsetComplexChildrenTypes"] = (node, selected) =>
                    ShowHandler<IUIHandler>(null, null, null, selected),

                ["ComplexchildrentypeAttributes"] = (node, selected) =>
                {
                    var cc = node.FindParentOfType<Complexchildrentype>();
                    ShowHandler(new SpawnGearAttributesControl(), typeof(SpawnGearPresetFile), cc.attributes, selected);
                },
                ["ComplexchildrentypeQuickBarSlot"] = (node, selected) =>
                {
                    var cc = node.FindParentOfType<Complexchildrentype>();
                    ShowHandler(new SpawnGearQuickBarSlotControl(), typeof(SpawnGearPresetFile), cc, selected);
                },
                ["ComplexchildrentypeSimpleChildrenUseDefaultAttributes"] = (node, selected) =>
                {
                    var cc = node.FindParentOfType<Complexchildrentype>();
                    ShowHandler(new SpawnGearsimpleChildrenUseDefaultAttributesControl(), typeof(SpawnGearPresetFile), cc, selected);
                },
                ["ComplexchildrentypeSimpleChildrenTypes"] = (node, selected) =>
                {
                    var cc = node.FindParentOfType<Complexchildrentype>();
                    ShowHandler(new SpawnGearSimpleChildrenControl(), typeof(SpawnGearPresetFile), cc, selected);
                },
                ["INITC"] = (node, selected) =>
                {
                    OpenWithExternalEditor frm = new OpenWithExternalEditor();
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.filePath = Path.Combine(_economyManager.basePath, "init.c");
                    DialogResult dr = frm.ShowDialog();

                    //ShowOpenWithDialog(Path.Combine(_economyManager.basePath, "init.c"));
                }

            };
        }
        public static void ShowOpenWithDialog(string filePath)
        {
            Process.Start("rundll32.exe", $"shell32.dll,OpenAs_RunDLL {filePath}");
        }

        private void InitializeContextMenuHandlers()
        {
            // ----------------------
            // Type handlers
            // ----------------------
            _typeContextMenus = new Dictionary<Type, Action<TreeNode>>
            {
                // Economy / Globals
                [typeof(EconomySection)] = node =>
                {
                    var ef = node.Parent.Tag as EconomyFile;
                    editPropertyToolStripMenuItem.Visible = !ef.IsModded;
                    setToDefaultToolStripMenuItem.Visible = ef.IsModded;
                    EditPropertyCMS.Show(Cursor.Position);
                },
                [typeof(variablesVar)] = node =>
                {
                    var gf = node.Parent.Tag as GlobalsFile;
                    editPropertyToolStripMenuItem.Visible = !gf.IsModded;
                    setToDefaultToolStripMenuItem.Visible = gf.IsModded;
                    EditPropertyCMS.Show(Cursor.Position);
                },

                // Types
                [typeof(TypesFile)] = node =>
                {
                    TypesCM.Items.Clear();
                    TypesCM.Items.Add(addNewTypesToolStripMenuItem);
                    TypesCM.Items.Add(removeSelectedToolStripMenuItem);
                    TypesCM.Items.Add(new ToolStripSeparator());
                    TypesCM.Items.Add(updateTypesFromXMLToolStripMenuItem);

                    TypesCM.Show(Cursor.Position);
                },
                [typeof(TypeEntry)] = node =>
                {
                    TypesCM.Items.Clear();
                    TypesCM.Items.Add(removeSelectedToolStripMenuItem);
                    TypesCM.Show(Cursor.Position);
                },

                // Events
                [typeof(EventsFile)] = node =>
                {
                    EventsCM.Items.Clear();
                    EventsCM.Items.Add(AddNewEventsToolstripMenuItem);
                    EventsCM.Items.Add(RemoveEventsToolStripMenuItem);
                    EventsCM.Show(Cursor.Position);
                },
                [typeof(eventsEvent)] = node =>
                {
                    EventsCM.Items.Clear();
                    EventsCM.Items.Add(RemoveEventsToolStripMenuItem);
                    EventsCM.Show(Cursor.Position);
                },
                [typeof(eventposdefEvent)] = node =>
                {
                    foreach (ToolStripMenuItem TSMI in EventSpawnContextMenu.Items)
                        TSMI.Visible = false;

                    var evt = node.Tag as eventposdefEvent;
                    addNewPosirtionToolStripMenuItem.Visible = true;
                    importPositionFromdzeToolStripMenuItem.Visible = true;
                    importPositionAndCreateEventgroupFormdzeToolStripMenuItem.Visible = true;
                    deleteSelectedEventSpawnToolStripMenuItem.Visible = true;

                    if (evt.pos != null && evt.pos.Count > 0)
                    {
                        removeAllPositionToolStripMenuItem.Visible = true;
                        exportPositionTodzeToolStripMenuItem.Visible = true;
                    }

                    EventSpawnContextMenu.Show(Cursor.Position);
                },

                // Random Presets
                [typeof(CfgrandompresetsFile)] = node =>
                {
                    RandomPresetsCM.Items.Clear();
                    RandomPresetsCM.Items.Add(removeSelectedRandomPresetToolStripmenuItem);
                    RandomPresetsCM.Show(Cursor.Position);
                },
                [typeof(randompresetsAttachments)] = node =>
                {
                    RandomPresetsCM.Items.Clear();
                    RandomPresetsCM.Items.Add(addNewItemToolStripMenuItem);
                    RandomPresetsCM.Items.Add(removeSelectedRandomPresetToolStripmenuItem);
                    RandomPresetsCM.Show(Cursor.Position);
                },
                [typeof(randompresetsCargo)] = node =>
                {
                    RandomPresetsCM.Items.Clear();
                    RandomPresetsCM.Items.Add(addNewItemToolStripMenuItem);
                    RandomPresetsCM.Items.Add(removeSelectedRandomPresetToolStripmenuItem);
                    RandomPresetsCM.Show(Cursor.Position);
                },
                [typeof(randompresetsItem)] = node =>
                {
                    RandomPresetsCM.Items.Clear();
                    RandomPresetsCM.Items.Add(removeSelectedRandomPresetToolStripmenuItem);
                    RandomPresetsCM.Show(Cursor.Position);
                },

                // Spawnable Types
                [typeof(CfgSpawnableTypesFile)] = node =>
                {
                    SpawnableTypesCM.Items.Clear();
                    SpawnableTypesCM.Items.Add(addNewSpawnableTypeToolStripMenuItem);
                    if ((node.Tag as CfgSpawnableTypesFile).IsModded)
                        SpawnableTypesCM.Items.Add(removeSelectedToolStripMenuItem1);
                    SpawnableTypesCM.Show(Cursor.Position);
                },
                [typeof(SpawnableType)] = node =>
                {
                    var st = node.Tag as SpawnableType;
                    SpawnableTypesCM.Items.Clear();
                    if (!st.Items.OfType<spawnableTypesHoarder>().Any())
                        SpawnableTypesCM.Items.Add(addNewHoarderToolStripMenuItem);
                    if (!st.Items.OfType<spawnableTypeDamage>().Any())
                        SpawnableTypesCM.Items.Add(addNewDamageToolStripMenuItem);
                    SpawnableTypesCM.Items.Add(addNewTagToolStripMenuItem);
                    SpawnableTypesCM.Items.Add(addNewCargoToolStripMenuItem1);
                    SpawnableTypesCM.Items.Add(addNewAttachmentToolStripMenuItem);
                    SpawnableTypesCM.Items.Add(removeSelectedToolStripMenuItem1);
                    SpawnableTypesCM.Show(Cursor.Position);
                },
                [typeof(spawnableTypesHoarder)] = node =>
                {
                    SpawnableTypesCM.Items.Clear();
                    SpawnableTypesCM.Items.Add(removeSelectedToolStripMenuItem1);
                    SpawnableTypesCM.Show(Cursor.Position);
                },
                [typeof(spawnableTypeDamage)] = node =>
                {
                    SpawnableTypesCM.Items.Clear();
                    SpawnableTypesCM.Items.Add(removeSelectedToolStripMenuItem1);
                    SpawnableTypesCM.Show(Cursor.Position);
                },
                [typeof(spawnableTypeCargo)] = node =>
                {
                    var cargo = node.Tag as spawnableTypeCargo;
                    SpawnableTypesCM.Items.Clear();
                    if (cargo.damage == null)
                        SpawnableTypesCM.Items.Add(addNewDamageToolStripMenuItem);
                    SpawnableTypesCM.Items.Add(addNewItemToolStripMenuItem1);
                    SpawnableTypesCM.Items.Add(removeSelectedToolStripMenuItem1);
                    SpawnableTypesCM.Show(Cursor.Position);
                },
                [typeof(spawnableTypeAttachment)] = node =>
                {
                    var attach = node.Tag as spawnableTypeAttachment;
                    SpawnableTypesCM.Items.Clear();
                    if (attach.damage == null)
                        SpawnableTypesCM.Items.Add(addNewDamageToolStripMenuItem);
                    SpawnableTypesCM.Items.Add(addNewItemToolStripMenuItem1);
                    SpawnableTypesCM.Items.Add(removeSelectedToolStripMenuItem1);
                    SpawnableTypesCM.Show(Cursor.Position);
                },
                [typeof(spawnableTypeItem)] = node =>
                {
                    var item = node.Tag as spawnableTypeItem;
                    SpawnableTypesCM.Items.Clear();
                    if (item.damage == null)
                        SpawnableTypesCM.Items.Add(addNewDamageToolStripMenuItem);
                    SpawnableTypesCM.Items.Add(addNewCargoToolStripMenuItem1);
                    SpawnableTypesCM.Items.Add(addNewAttachmentToolStripMenuItem);
                    SpawnableTypesCM.Items.Add(removeSelectedToolStripMenuItem1);
                    SpawnableTypesCM.Show(Cursor.Position);
                },

                // Spawn Gear
                [typeof(SpawnGearPresetFile)] = node =>
                {
                    SpawnGearPresetCM.Items.Clear();
                    SpawnGearPresetCM.Items.Add(SpawnGearremoveSelectedToolStripMenuItem2);
                    SpawnGearPresetCM.Show(Cursor.Position);
                },
                [typeof(Attachmentslotitemset)] = node =>
                {
                    SpawnGearPresetCM.Items.Clear();
                    SpawnGearPresetCM.Items.Add(addNewDisctreetItemSetToolStripMenuItem);
                    SpawnGearPresetCM.Items.Add(SpawnGearremoveSelectedToolStripMenuItem2);
                    SpawnGearPresetCM.Show(Cursor.Position);
                },
                [typeof(Discreteitemset)] = node =>
                {
                    SpawnGearPresetCM.Items.Clear();
                    SpawnGearPresetCM.Items.Add(SpawnGearremoveSelectedToolStripMenuItem2);
                    SpawnGearPresetCM.Show(Cursor.Position);
                },
                [typeof(Discreteunsorteditemset)] = node =>
                {
                    SpawnGearPresetCM.Items.Clear();
                    SpawnGearPresetCM.Items.Add(SpawnGearremoveSelectedToolStripMenuItem2);
                    SpawnGearPresetCM.Show(Cursor.Position);
                },
                [typeof(Complexchildrentype)] = node =>
                {
                    SpawnGearPresetCM.Items.Clear();
                    SpawnGearPresetCM.Items.Add(SpawnGearremoveSelectedToolStripMenuItem2);
                    SpawnGearPresetCM.Show(Cursor.Position);
                },

                // Player Restricted Areas
                [typeof(PRABoxes)] = node =>
                {
                    PlayerRestrictedAreaCM.Items.Clear();
                    PlayerRestrictedAreaCM.Items.AddRange(removePRASelectedToolStripMenuItem);
                    PlayerRestrictedAreaCM.Show(Cursor.Position);
                },
                [typeof(PRASafePosition)] = node =>
                {
                    PlayerRestrictedAreaCM.Items.Clear();
                    PlayerRestrictedAreaCM.Items.AddRange(removePRASelectedToolStripMenuItem);
                    PlayerRestrictedAreaCM.Show(Cursor.Position);
                },
                [typeof(PlayerRestrictedFile)] = node =>
                {
                    PlayerRestrictedAreaCM.Items.Clear();
                    PlayerRestrictedAreaCM.Items.AddRange(removePRASelectedToolStripMenuItem);
                    PlayerRestrictedAreaCM.Show(Cursor.Position);
                },

                // Effects Area
                [typeof(Areas)] = node =>
                {
                    var area = node.Tag as Areas;
                    cfgEffectsAreaCM.Items.Clear();
                    if (area.PlayerData == null)
                        cfgEffectsAreaCM.Items.Add(usePlayerDataToolStripMenuItem);
                    cfgEffectsAreaCM.Items.Add(removeEffectAreaToolStripMenuItem);
                    cfgEffectsAreaCM.Show(Cursor.Position);
                },
                [typeof(PlayerData)] = node =>
                {
                    cfgEffectsAreaCM.Items.Clear();
                    cfgEffectsAreaCM.Items.Add(removePlayerDataToolStripMenuItem);
                    cfgEffectsAreaCM.Show(Cursor.Position);
                },
                [typeof(cfgeffectareaSafePosition)] = node =>
                {
                    cfgEffectsAreaCM.Items.Clear();
                    cfgEffectsAreaCM.Items.Add(removeSafePositionToolStripMenuItem);
                    cfgEffectsAreaCM.Show(Cursor.Position);
                },

                // Player spawns
                [typeof(playerspawnpointsGroup)] = node =>
                {
                    PlayerSpawnsCM.Items.Clear();
                    PlayerSpawnsCM.Items.Add(addNewSpawnPositionToolStripMenuItem);
                    PlayerSpawnsCM.Items.Add(removeSpawnGroupToolStripMenuItem);
                    PlayerSpawnsCM.Show(Cursor.Position);
                },
                [typeof(playerspawnpointsGroupPos)] = node =>
                {
                    PlayerSpawnsCM.Items.Clear();
                    PlayerSpawnsCM.Items.Add(removeSpawnPositionToolStripMenuItem);
                    PlayerSpawnsCM.Show(Cursor.Position);
                },

                // IgnoreList
                [typeof(cfgignorelistConfig)] = node =>
                {
                    IgnoreListCM.Items.Clear();
                    IgnoreListCM.Items.Add(addClassnameToolStripMenuItem);
                    IgnoreListCM.Show(Cursor.Position);
                },
                [typeof(ignoreType)] = node =>
                {
                    IgnoreListCM.Items.Clear();
                    IgnoreListCM.Items.Add(removeClassnameToolStripMenuItem);
                    IgnoreListCM.Show(Cursor.Position);
                },

                //MapgroupPos
                [typeof(mapGroup)] = node =>
                {
                    MapGroupPosCM.Items.Clear();
                    MapGroupPosCM.Items.Add(removeSelectedPositionsToolStripMenuItem);
                    MapGroupPosCM.Show(Cursor.Position);
                }
            };

            // ----------------------
            // String handlers
            // ----------------------
            _stringContextMenus = new Dictionary<string, Action<TreeNode>>
            {
                ["RootNode"] = node =>
                {
                    TypesCM.Items.Clear();
                    TypesCM.Items.Add(addNewTypesToolStripMenuItem);
                    TypesCM.Items.Add(new ToolStripSeparator());
                    TypesCM.Items.Add(AddNewEventsToolstripMenuItem);
                    TypesCM.Items.Add(new ToolStripSeparator());
                    TypesCM.Items.Add(addNewRandomPresetFileToolStripMenuItem);
                    TypesCM.Items.Add(new ToolStripSeparator());
                    TypesCM.Items.Add(addNewSpawnableTypesFileToolStripMenuItem);
                    TypesCM.Show(Cursor.Position);
                },
                ["RandomPresetsAttachments"] = node =>
                {
                    RandomPresetsCM.Items.Clear();
                    RandomPresetsCM.Items.Add(addNewAttchementToolStripMenuItem);
                    RandomPresetsCM.Show(Cursor.Position);
                },
                ["RandomPresetsCargo"] = node =>
                {
                    RandomPresetsCM.Items.Clear();
                    RandomPresetsCM.Items.Add(addNewCargoToolStripMenuItem);
                    RandomPresetsCM.Show(Cursor.Position);
                },
                ["SpawnGearAttachmentSlotItemSetsParent"] = node =>
                {
                    SpawnGearPresetCM.Items.Clear();
                    SpawnGearPresetCM.Items.Add(addNewAttachmentSlotItemSetToolStripMenuItem);
                    SpawnGearPresetCM.Show(Cursor.Position);
                },
                ["SpawnGearDiscreteUnsortedItemSetsParent"] = node =>
                {
                    SpawnGearPresetCM.Items.Clear();
                    SpawnGearPresetCM.Items.Add(addNewDiscreetUnsortedItemSetToolStripMenuItem);
                    SpawnGearPresetCM.Show(Cursor.Position);
                },
                ["SpawnGear Presets Files"] = node =>
                {
                    SpawnGearPresetCM.Items.Clear();
                    SpawnGearPresetCM.Items.Add(addNewSpawnGEarPresetFileToolStripMenuItem);
                    SpawnGearPresetCM.Show(Cursor.Position);
                },
                ["PRABoxes"] = node =>
                {
                    PlayerRestrictedAreaCM.Items.Clear();
                    PlayerRestrictedAreaCM.Items.AddRange(addNewPRABoxToolStripMenuItem);
                    PlayerRestrictedAreaCM.Show(Cursor.Position);
                },
                ["PRASafePositions3D"] = node =>
                {
                    PlayerRestrictedAreaCM.Items.Clear();
                    PlayerRestrictedAreaCM.Items.AddRange(addNewPRASafePositionToolStripMenuItem);
                    PlayerRestrictedAreaCM.Show(Cursor.Position);
                },
                ["playerRestrictedAreaFiles"] = node =>
                {
                    PlayerRestrictedAreaCM.Items.Clear();
                    PlayerRestrictedAreaCM.Items.AddRange(addNewPRAFileToolStripMenuItem);
                    PlayerRestrictedAreaCM.Show(Cursor.Position);
                },
                ["ObjectPawnerArrFiles"] = node =>
                {
                    ObjectSpawnerArrCM.Items.Clear();
                    ObjectSpawnerArrCM.Items.Add(addNewObjectSpawnerArrFileToolStripMenuItem);
                    ObjectSpawnerArrCM.Show(Cursor.Position);
                },
                ["cfgeffectareaareas"] = node =>
                {
                    cfgEffectsAreaCM.Items.Clear();
                    cfgEffectsAreaCM.Items.Add(addNewEffectAreaToolStripMenuItem);
                    cfgEffectsAreaCM.Show(Cursor.Position);
                },
                ["cfgeffectareaSafePositions"] = node =>
                {
                    cfgEffectsAreaCM.Items.Clear();
                    cfgEffectsAreaCM.Items.Add(addNewSafePositionToolStripMenuItem);
                    cfgEffectsAreaCM.Show(Cursor.Position);
                },
                ["PlayerSpawnGenPosBubbles"] = node =>
                {
                    if (NodeHasChildOfType<playerspawnpointsGroup>(node))
                    {
                        PlayerSpawnsCM.Items.Clear();
                        PlayerSpawnsCM.Items.Add(addNewSpawnGroupToolStripMenuItem);
                        PlayerSpawnsCM.Show(Cursor.Position);
                    }
                    else if (NodeHasChildOfType<playerspawnpointsGroupPos>(node))
                    {
                        PlayerSpawnsCM.Items.Clear();
                        PlayerSpawnsCM.Items.Add(addNewSpawnPositionToolStripMenuItem);
                        PlayerSpawnsCM.Show(Cursor.Position);
                    }
                    else
                    {
                        PlayerSpawnsCM.Items.Clear();
                        PlayerSpawnsCM.Items.Add(addNewSpawnGroupToolStripMenuItem);
                        PlayerSpawnsCM.Items.Add(addNewSpawnPositionToolStripMenuItem);
                        PlayerSpawnsCM.Show(Cursor.Position);
                    }
                }
            };
        }
        public bool NodeHasChildOfType<T>(TreeNode node)
        {
            if (node == null) return false;

            foreach (TreeNode child in node.Nodes)
            {
                if (child.Tag is T)
                    return true;
            }

            return false;
        }
        private void EconomyForm_Load(object sender, EventArgs e)
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string imagePath = Path.Combine(appDirectory, "MapAddons", _projectManager.CurrentProject.MapPath);
            if (!File.Exists(imagePath))
            {
                MessageBox.Show($"Map File does not exist for {_projectManager.CurrentProject.ProjectName}\nPlease download it from the Projects Manager");
                this.BeginInvoke(new Action(Close)); // defer until safe
                return;
            }
            Image mapImage = Image.FromFile(imagePath);
            _mapControl.LoadMap(mapImage, _projectManager.CurrentProject.MapSize);

            LoadTreeview();
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            savefiles();
        }
        public void savefiles(bool updated = false)
        {
            var savedFiles = _economyManager.Save();
            Console.WriteLine("Saved files:");
            foreach (var file in savedFiles)
            {
                Console.WriteLine(file);
            }
            if (savedFiles.Count() > 0)
            {
                ShowSavedFilesMessage(savedFiles);
            }
            RebuildWarning();
            

        }
        private void RebuildWarning()
        {
            if (EconomyTV.Nodes.Count == 0)
                return;

            TreeNode rootNode = EconomyTV.Nodes[0];

            TreeNode existingWarningsNode = rootNode.Nodes
                .Cast<TreeNode>()
                .FirstOrDefault(n => n.Tag?.ToString() == "WarningsCategory");

            if (existingWarningsNode != null)
                rootNode.Nodes.Remove(existingWarningsNode);
            _economyManager.ClearWarnings();
            _economyManager.RebuildWarnings();

            TreeNode warningsNode = BuildWarningsNode();
            if (warningsNode != null)
            {
                rootNode.Nodes.Add(warningsNode);
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
        private void EconomyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_economyManager.needToSave())
            {
                DialogResult dialogResult = MessageBox.Show("You have Unsaved Changes, do you wish to save", "Unsaved Changes found", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    savefiles();
                }
            }
        }
        private void EconomyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _mapControl.ClearMap();

            if (_plugin is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        #region Loading treeview
        private void AddFileToTree<TFile>(TreeNode parentNode, string relativePath, TFile file, Func<TFile, TreeNode> createFileNode, bool expand = false)
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
                        EconomyTV.SelectedNode = fileNode;
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
                                _economyManager.basePath,
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

        //creating economy Nodes
        private TreeNode CreateEconomyfileNodes(EconomyFile ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateEconomyNodes(ef, EconomyRootNode);

            return EconomyRootNode;
        }
        private static void CreateEconomyNodes(EconomyFile ef, TreeNode EconomyRootNode)
        {
            if (ef.Data.dynamic != null)
            {
                EconomyRootNode.Nodes.Add(new TreeNode($"Dynamic init:{ef.Data.dynamic.init} load:{ef.Data.dynamic.load} respawn:{ef.Data.dynamic.respawn} save:{ef.Data.dynamic.save}")
                {
                    Tag = ef.Data.dynamic
                });
            }
            if (ef.Data.animals != null)
            {
                EconomyRootNode.Nodes.Add(new TreeNode($"Animals init:{ef.Data.animals.init} load:{ef.Data.animals.load} respawn:{ef.Data.animals.respawn} save:{ef.Data.animals.save}")
                {
                    Tag = ef.Data.animals
                });
            }
            if (ef.Data.zombies != null)
            {
                EconomyRootNode.Nodes.Add(new TreeNode($"Zombies init:{ef.Data.zombies.init} load:{ef.Data.zombies.load} respawn:{ef.Data.zombies.respawn} save:{ef.Data.zombies.save}")
                {
                    Tag = ef.Data.zombies
                });
            }
            if (ef.Data.vehicles != null)
            {
                EconomyRootNode.Nodes.Add(new TreeNode($"Vehicles init:{ef.Data.vehicles.init} load:{ef.Data.vehicles.load} respawn:{ef.Data.vehicles.respawn} save:{ef.Data.vehicles.save}")
                {
                    Tag = ef.Data.vehicles
                });
            }
            if (ef.Data.randoms != null)
            {
                EconomyRootNode.Nodes.Add(new TreeNode($"Randoms init:{ef.Data.randoms.init} load:{ef.Data.randoms.load} respawn:{ef.Data.randoms.respawn} save:{ef.Data.randoms.save}")
                {
                    Tag = ef.Data.randoms
                });
            }
            if (ef.Data.custom != null)
            {
                EconomyRootNode.Nodes.Add(new TreeNode($"Custom init:{ef.Data.custom.init} load:{ef.Data.custom.load} respawn:{ef.Data.custom.respawn} save:{ef.Data.custom.save}")
                {
                    Tag = ef.Data.custom
                });
            }
            if (ef.Data.building != null)
            {
                EconomyRootNode.Nodes.Add(new TreeNode($"Building init:{ef.Data.building.init} load:{ef.Data.building.load} respawn:{ef.Data.building.respawn} save:{ef.Data.building.save}")
                {
                    Tag = ef.Data.building
                });
            }
            if (ef.Data.player != null)
            {
                EconomyRootNode.Nodes.Add(new TreeNode($"Player init:{ef.Data.player.init} load:{ef.Data.player.load} respawn:{ef.Data.player.respawn} save:{ef.Data.player.save}")
                {
                    Tag = ef.Data.player
                });
            }
        }
        //creating globals Nodes
        private TreeNode CreateGlobalsfileNodes(GlobalsFile gf)
        {
            TreeNode GlobalsRootNode = new TreeNode(gf.FileName)
            {
                Tag = gf
            };
            foreach (variablesVar vv in gf.Data.var)
            {
                GlobalsRootNode.Nodes.Add(new TreeNode($"{vv.name} = {vv.value}")
                {
                    Tag = vv
                });

            }

            return GlobalsRootNode;
        }
        //create enviroment config nodes
        private TreeNode CreateEnviromentConfigNodes(CfgenvironmentConfig gf)
        {
            TreeNode rootNode = new TreeNode(gf.FileName)
            {
                Tag = gf
            };

            if (gf.Data?.territories?.territory != null && gf.Data.territories.territory.Count > 0)
            {
                TreeNode territoriesNode = new TreeNode("Territories")
                {
                    Tag = gf.Data.territories
                };

                foreach (envTerritoriesTerritory ett in gf.Data.territories.territory)
                {
                    TreeNode territoryRefNode = new TreeNode(ett.name)
                    {
                        Tag = ett
                    };

                    envTerritoriesFile usableFile = gf.Data.territories.GetUsableFile(ett.file.usable);
                    if (usableFile != null)
                    {
                        TreeNode usableFileNode = new TreeNode($"Usable File: {Path.GetFileName(usableFile.path)}")
                        {
                            Tag = usableFile
                        };

                        territorytype linkedTerritoryFile = _economyManager.territoriesConfig.MutableItems
                            .FirstOrDefault(t =>
                                string.Equals(
                                    NormalizePath(t.FilePath),
                                    NormalizePath(Path.Combine(_economyManager.basePath, usableFile.path)),
                                    StringComparison.OrdinalIgnoreCase) ||
                                string.Equals(
                                    NormalizePath(t.FileName),
                                    NormalizePath(Path.GetFileName(usableFile.path)),
                                    StringComparison.OrdinalIgnoreCase)
                            );

                        if (linkedTerritoryFile != null)
                        {
                            usableFileNode.Nodes.Add(CreateTerritoryNodes(linkedTerritoryFile));
                        }

                        territoryRefNode.Nodes.Add(usableFileNode);
                    }

                    territoriesNode.Nodes.Add(territoryRefNode);
                }

                rootNode.Nodes.Add(territoriesNode);
            }

            return rootNode;
        }
        // create territory nodes 
        private TreeNode CreateTerritoryNodes(territorytype tf)
        {
            TreeNode GlobalsRootNode = new TreeNode(tf.FileName)
            {
                Tag = tf
            };
            int name = 1;
            foreach (territorytypeTerritory territorytypeTerritory in tf.territory)
            {
                TreeNode typeterritorynode = new TreeNode("Territory" + name.ToString())
                {
                    Tag = territorytypeTerritory
                };
                name++;
                foreach (territorytypeTerritoryZone zone in territorytypeTerritory.zone)
                {
                    TreeNode tn = new TreeNode(zone.ToString())
                    {
                        Tag = zone
                    };
                    typeterritorynode.Nodes.Add(tn);
                }
                GlobalsRootNode.Nodes.Add(typeterritorynode);
            }
            return GlobalsRootNode;
        }
        //creating CFGGameplaynodes
        private TreeNode CreateGameplayConfigNodes(CFGGameplayConfig ganmeplay)
        {
            TreeNode GameplayRootNode = new TreeNode(ganmeplay.FileName)
            {
                Tag = ganmeplay
            };
            GameplayRootNode.Nodes.Add(new TreeNode($"Version:{ganmeplay.Data.version.ToString()}")
            {
                Tag = "cfggameplayConfigVersion"
            });
            GameplayRootNode.Nodes.Add(new TreeNode($"GeneralData")
            {
                Tag = ganmeplay.Data.GeneralData
            });
            TreeNode PlayerDataNodes = new TreeNode($"PlayerData")
            {
                Tag = ganmeplay.Data.PlayerData
            };
            TreeNode spawnGearNodes = new TreeNode("SpawnGear Presets Files")
            {
                Tag = "SpawnGear Presets Files"
            };
            foreach (string spawnfile in ganmeplay.Data.PlayerData.spawnGearPresetFiles)
            {
                SpawnGearPresetFile spawnGearPresetFiles = ganmeplay.GetSpawnGearPreset(spawnfile);
                spawnGearNodes.Nodes.Add(CreateSpawnGearFilesNodes(spawnGearPresetFiles));
            }
            PlayerDataNodes.Nodes.Add(spawnGearNodes);
            GameplayRootNode.Nodes.Add(PlayerDataNodes);
            TreeNode WorldsDataaNodes = new TreeNode($"WorldsData")
            {
                Tag = ganmeplay.Data.WorldsData
            };
            TreeNode playerRestrictedAreaFilesNodes = new TreeNode($"Player Restricted Area Files")
            {
                Tag = "playerRestrictedAreaFiles"
            };
            foreach (string restrictedfile in ganmeplay.Data.WorldsData.playerRestrictedAreaFiles)
            {
                PlayerRestrictedFile PlayerRestrictedFiles = ganmeplay.GetRestrictedFiles(restrictedfile);
                playerRestrictedAreaFilesNodes.Nodes.Add(CreateRestrictedfilesNodes(PlayerRestrictedFiles));
            }
            TreeNode objectspawnerarrfilenodes = new TreeNode($"Object Spawner Arr Files")
            {
                Tag = "ObjectPawnerArrFiles"
            };
            foreach (string objectspawnerarrfile in ganmeplay.Data.WorldsData.objectSpawnersArr)
            {
                ObjectSpawnerArrFile ObjectSpawnerArr = ganmeplay.GetObjectSpawnerFiles(objectspawnerarrfile);
                objectspawnerarrfilenodes.Nodes.Add(new TreeNode(Path.Combine(ObjectSpawnerArr.ModFolder, ObjectSpawnerArr.FileName)) { Tag = ObjectSpawnerArr });
            }
            WorldsDataaNodes.Nodes.Add(playerRestrictedAreaFilesNodes);
            WorldsDataaNodes.Nodes.Add(objectspawnerarrfilenodes);
            GameplayRootNode.Nodes.Add(WorldsDataaNodes);
            GameplayRootNode.Nodes.Add(new TreeNode($"BaseBuildingData")
            {
                Tag = ganmeplay.Data.BaseBuildingData
            });
            GameplayRootNode.Nodes.Add(new TreeNode($"UIData")
            {
                Tag = ganmeplay.Data.UIData
            });
            GameplayRootNode.Nodes.Add(new TreeNode($"MapData")
            {
                Tag = ganmeplay.Data.MapData
            });
            GameplayRootNode.Nodes.Add(new TreeNode($"VehicleData")
            {
                Tag = ganmeplay.Data.VehicleData
            });

            return GameplayRootNode;
        }
        private TreeNode CreateSpawnGearFilesNodes(SpawnGearPresetFile spawnGearPresetFiles)
        {
            TreeNode rootNode = new TreeNode(Path.Combine(spawnGearPresetFiles.ModFolder, spawnGearPresetFiles.FileName))
            {
                Tag = spawnGearPresetFiles
            };
            rootNode.Nodes.Add(new TreeNode($"Name: {spawnGearPresetFiles.Data.name}")
            {
                Tag = "SpawnGearName"
            });
            rootNode.Nodes.Add(new TreeNode($"Spawn Weight: {spawnGearPresetFiles.Data.spawnWeight}")
            {
                Tag = "SpawnGearSpawnWeight"
            });
            rootNode.Nodes.Add(new TreeNode("Character Types")
            {
                Tag = "SpawnGearCharacterTypes"
            });
            TreeNode AttachmentslotitemsetNode = new TreeNode("Attachment slot item set")
            {
                Tag = "SpawnGearAttachmentSlotItemSetsParent"
            };
            foreach (Attachmentslotitemset Slot in spawnGearPresetFiles.Data.attachmentSlotItemSets)
            {
                AttachmentslotitemsetNode.Nodes.Add(AttachmentslotitemsetNodeTN(Slot));
            }
            rootNode.Nodes.Add(AttachmentslotitemsetNode);
            TreeNode discreteUnsortedItemSets = new TreeNode("Discrete Unsorted Item Sets")
            {
                Tag = "SpawnGearDiscreteUnsortedItemSetsParent"
            };
            foreach (Discreteunsorteditemset DUIS in spawnGearPresetFiles.Data.discreteUnsortedItemSets)
            {
                discreteUnsortedItemSets.Nodes.Add(DiscreteunsorteditemsetTN(DUIS));
            }
            rootNode.Nodes.Add(discreteUnsortedItemSets);
            return rootNode;
        }
        private TreeNode DiscreteunsorteditemsetTN(Discreteunsorteditemset dUIS)
        {
            TreeNode DUIS = new TreeNode(dUIS.name)
            {
                Tag = dUIS
            };
            DUIS.Nodes.Add(new TreeNode("Spawn Weight")
            {
                Tag = "DiscreteunsorteditemsetSpawnWeight"
            });
            DUIS.Nodes.Add(new TreeNode("Attributes")
            {
                Tag = "DiscreteunsorteditemsetAttributes"
            });
            TreeNode ComplexChildrenTypes = new TreeNode("Complex Children Types")
            {
                Tag = "DiscreteunsorteditemsetComplexChildrenTypes"
            };
            foreach (Complexchildrentype CCT in dUIS.complexChildrenTypes)
            {
                ComplexChildrenTypes.Nodes.Add(ComplexChildrenTypesNodeTN(CCT));
            }
            DUIS.Nodes.Add(ComplexChildrenTypes);
            DUIS.Nodes.Add(new TreeNode("Simple Children Use Default Attributes")
            {
                Tag = "DiscreteunsorteditemsetSimpleChildrenUseDefaultAttributes"
            });
            DUIS.Nodes.Add(new TreeNode("Simple Children Types")
            {
                Tag = "DiscreteunsorteditemsetSimpleChildrenTypes"
            });

            return DUIS;
        }
        private TreeNode AttachmentslotitemsetNodeTN(Attachmentslotitemset slot)
        {
            string slotname = slot.slotName;
            TreeNode ASISnode = new TreeNode(slotname)
            {
                Tag = slot
            };
            foreach (Discreteitemset DIS in slot.discreteItemSets)
            {
                ASISnode.Nodes.Add(DiscreetItemSetsTN(DIS));
            }
            return ASISnode;
        }
        private TreeNode DiscreetItemSetsTN(Discreteitemset DIS)
        {
            TreeNode DISNode = new TreeNode(DIS.itemType)
            {
                Tag = DIS
            };
            DISNode.Nodes.Add(new TreeNode("Spawn Weight")
            {
                Tag = "DiscreteitemsetSpawnWeight"
            });
            DISNode.Nodes.Add(new TreeNode("Attributes")
            {
                Tag = "DiscreteitemsetAttributes"
            });
            DISNode.Nodes.Add(new TreeNode("Quick Bar Slot")
            {
                Tag = "DiscreteitemsetQuickBarSlot"
            });
            TreeNode ComplexChildrenTypes = new TreeNode("Complex Children Types")
            {
                Tag = "DiscreteitemsetComplexChildrenTypes"
            };
            foreach (Complexchildrentype CCT in DIS.complexChildrenTypes)
            {
                ComplexChildrenTypes.Nodes.Add(ComplexChildrenTypesNodeTN(CCT));
            }
            DISNode.Nodes.Add(ComplexChildrenTypes);
            DISNode.Nodes.Add(new TreeNode("Simple Children Use Default Attributes")
            {
                Tag = "DiscreteitemsetSimpleChildrenUseDefaultAttributes"
            });
            DISNode.Nodes.Add(new TreeNode("Simple Children Types")
            {
                Tag = "DiscreteitemsetSimpleChildrenTypes"
            });

            return DISNode;
        }
        private TreeNode ComplexChildrenTypesNodeTN(Complexchildrentype cCT)
        {
            string slotname = cCT.itemType;
            TreeNode CCTNode = new TreeNode(slotname)
            {
                Tag = cCT
            };
            CCTNode.Nodes.Add(new TreeNode("Attributes")
            {
                Tag = "ComplexchildrentypeAttributes"
            });
            CCTNode.Nodes.Add(new TreeNode("Quick Bar Slot")
            {
                Tag = "ComplexchildrentypeQuickBarSlot"
            });
            CCTNode.Nodes.Add(new TreeNode("Simple Children Use Default Attributes")
            {
                Tag = "ComplexchildrentypeSimpleChildrenUseDefaultAttributes"
            });
            CCTNode.Nodes.Add(new TreeNode("Simple Children Types")
            {
                Tag = "ComplexchildrentypeSimpleChildrenTypes"
            });
            return CCTNode;
        }
        private TreeNode CreateRestrictedfilesNodes(PlayerRestrictedFile playerRestrictedFiles)
        {
            TreeNode areaNode = new TreeNode(Path.Combine(playerRestrictedFiles.ModFolder, playerRestrictedFiles.FileName))
            {
                Tag = playerRestrictedFiles
            };
            TreeNode praBoxesNode = new TreeNode("PRABoxes")
            {
                Tag = "PRABoxes"
            };
            for (int i = 0; i < playerRestrictedFiles.BoxesView.Count; i++)
            {
                var box = playerRestrictedFiles.BoxesView[i];
                praBoxesNode.Nodes.Add(CreatePRABoxesNodes(i, box));
            }
            TreeNode safePositionsNode = new TreeNode("SafePositions3D")
            {
                Tag = "PRASafePositions3D"
            };

            for (int i = 0; i < playerRestrictedFiles.SafePositionsView.Count; i++)
            {
                var PRASafePosition = playerRestrictedFiles.SafePositionsView[i];
                safePositionsNode.Nodes.Add(CreatePRASafePositionNodes(PRASafePosition, i));
            }

            areaNode.Nodes.Add(praBoxesNode);
            areaNode.Nodes.Add(safePositionsNode);
            return areaNode;
        }
        private static TreeNode CreatePRASafePositionNodes(PRASafePosition PRASafePosition, int i)
        {

            TreeNode posNode = new TreeNode($"Position {i + 1}: {PRASafePosition.Position.GetString()}")
            {
                Tag = PRASafePosition
            };
            return posNode;
        }
        private static TreeNode CreatePRABoxesNodes(int i, PRABoxes box)
        {
            TreeNode boxNode = new TreeNode($"Box {i + 1}")
            {
                Tag = box
            };

            boxNode.Nodes.Add(new TreeNode("HalfExtents: [" + string.Join(", ", box.HalfExtents) + "]")
            {
                Tag = box.HalfExtents
            });
            boxNode.Nodes.Add(new TreeNode("Orientation: [" + string.Join(", ", box.Orientation) + "]")
            {
                Tag = box.Orientation
            });
            boxNode.Nodes.Add(new TreeNode("Position: [" + string.Join(", ", box.Position) + "]")
            {
                Tag = box.Position
            });
            return boxNode;
        }
        //Creating Types Nodes
        private TreeNode CreateTypesfileNodes(TypesFile tf)
        {
            TreeNode TypesrootNode = new TreeNode(tf.FileName)
            {
                Tag = tf
            };
            foreach (TypeEntry type in tf.Data.TypeList)
            {
                CreateTyoesNodes(TypesrootNode, type);
            }
            return TypesrootNode;
        }

        private static void CreateTyoesNodes(TreeNode TypesrootNode, TypeEntry type)
        {
            Category cat = type.Category;
            if (type.Category == null)
            {
                cat = new Category()
                {
                    Name = "other"
                };
            }
            TreeNode typenode = new TreeNode(type.Name)
            {
                Tag = type
            };
            if (!TypesrootNode.Nodes.ContainsKey(cat.Name))
            {
                TreeNode newcatnode = new TreeNode(cat.Name)
                {
                    Name = cat.Name,
                    Tag = cat
                };
                TypesrootNode.Nodes.Add(newcatnode);
            }
            TypesrootNode.Nodes[cat.Name].Nodes.Add(typenode);
        }

        //Creating spawnableTypes Nodes
        private TreeNode CreateSpawnableTypesfileNodes(CfgSpawnableTypesFile stf)
        {
            TreeNode ConfigRoot = new TreeNode(stf.FileName)
            {
                Tag = stf
            };
            if (stf.Data.damage != null)
            {
                ConfigRoot.Nodes.Add(CreateDamageNode(stf.Data.damage));
            }
            foreach (SpawnableType SP in stf.Data.type)
            {
                TreeNode IN = new TreeNode(SP.name)
                {
                    Tag = SP
                };
                foreach (var item in SP.Items)
                {
                    IN.Nodes.Add(CrteateSpawnableTypeNodes(item));
                }
                ConfigRoot.Nodes.Add(IN);
            }
            return ConfigRoot;
        }
        private TreeNode CrteateSpawnableTypeNodes(object item)
        {
            if (item is spawnableTypesHoarder)
            {
                return new TreeNode("hoarder")
                {
                    Tag = item as spawnableTypesHoarder
                };
            }
            else if (item is spawnableTypeTag)
            {
                return new TreeNode(getTagString(item as spawnableTypeTag))
                {
                    Tag = item as spawnableTypeTag
                };
            }
            else if (item is spawnableTypeDamage)
            {
                spawnableTypeDamage damage = item as spawnableTypeDamage;
                return CreateDamageNode(damage);
            }
            else if (item is spawnableTypeCargo)
            {
                spawnableTypeCargo cargo = item as spawnableTypeCargo;
                return createCargoNopdes(cargo);
            }
            else if (item is spawnableTypeAttachment)
            {
                spawnableTypeAttachment attachment = item as spawnableTypeAttachment;
                return createattachmentnodes(attachment);
            }
            return null;
        }
        private string getTagString(spawnableTypeTag spawnableTypeTag)
        {
            return $"tag : {spawnableTypeTag.name}";
        }
        private TreeNode createattachmentnodes(spawnableTypeAttachment attachment)
        {
            TreeNode attachmentnode = new TreeNode(getAttachmentString(attachment))
            {
                Tag = attachment
            };
            if (attachment.damage != null)
            {
                attachmentnode.Nodes.Add(CreateDamageNode(attachment.damage));
            }
            if (attachment.item.Count > 0)
            {
                foreach (spawnableTypeItem STI in attachment.item)
                {
                    attachmentnode.Nodes.Add(CreateItemNode(STI));
                }
            }
            return attachmentnode;
        }
        private string getAttachmentString(spawnableTypeAttachment attachment)
        {
            string attachmentstring = "Attachments :";
            if (attachment.preset != null)
            {
                attachmentstring += " Preset = " + attachment.preset;
            }
            if (attachment.chanceSpecified)
            {
                attachmentstring += " Chance = " + attachment.chance;
            }

            return attachmentstring;
        }
        private TreeNode createCargoNopdes(spawnableTypeCargo cargo)
        {

            TreeNode cargonode = new TreeNode(Getcargostring(cargo))
            {
                Tag = cargo
            };
            if (cargo.damage != null)
            {
                cargonode.Nodes.Add(CreateDamageNode(cargo.damage));
            }
            if (cargo.item.Count > 0)
            {
                foreach (spawnableTypeItem STI in cargo.item)
                {
                    cargonode.Nodes.Add(CreateItemNode(STI));
                }
            }
            return cargonode;
        }
        private string Getcargostring(spawnableTypeCargo cargo)
        {
            string cargostring = "Cargo :";
            if (cargo.preset != null)
            {
                cargostring += " Preset = " + cargo.preset;
            }
            if (cargo.chanceSpecified)
            {
                cargostring += " Chance = " + cargo.chance;
            }
            return cargostring;
        }
        private TreeNode CreateDamageNode(spawnableTypeDamage damage)
        {
            TreeNode damagenode = new TreeNode(GetDamageString(damage))
            {
                Tag = damage
            };
            return damagenode;
        }
        private string GetDamageString(spawnableTypeDamage damage)
        {
            return $"damage : quantmin={damage.min} quamtmax={damage.max}";
        }
        private TreeNode CreateItemNode(spawnableTypeItem sTI)
        {
            TreeNode treeNode = new TreeNode(GetItemString(sTI))
            {
                Tag = sTI
            };
            if (sTI.damage != null)
            {
                treeNode.Nodes.Add(CreateDamageNode(sTI.damage));
            }
            if (sTI.attachments.Count > 0)
            {
                foreach (spawnableTypeAttachment attachment in sTI.attachments)
                {
                    treeNode.Nodes.Add(createattachmentnodes(attachment));
                }
            }
            if (sTI.cargo.Count > 0)
            {
                foreach (spawnableTypeCargo cargo in sTI.cargo)
                {
                    treeNode.Nodes.Add(createCargoNopdes(cargo));
                }
            }

            return treeNode;
        }
        private string GetItemString(spawnableTypeItem sTI)
        {
            string itemstring = $"Item = {sTI.name}";
            if (sTI.equipSpecified)
            {
                itemstring += " equip = " + sTI.equip;
            }
            if (sTI.chanceSpecified)
            {
                itemstring += " Chance = " + sTI.chance;
            }
            if (sTI.quantminSpecified && sTI.quantmaxSpecified)
            {
                itemstring += " quantMin = " + sTI.quantmin + " quantMax = " + sTI.quantmax;
            }

            return itemstring;
        }
        //Creating Random presets Nodes
        private TreeNode CreateRandomPresetsFileNodes(CfgrandompresetsFile rpc)
        {
            TreeNode ConfigRoot = new TreeNode(rpc.FileName)
            {
                Tag = rpc
            };
            TreeNode Attatchnode = new TreeNode("Attachments")
            {
                Tag = "RandomPresetsAttachments"
            };
            TreeNode CargoNode = new TreeNode("Cargo")
            {
                Tag = "RandomPresetsCargo"
            };
            foreach (var RP in rpc.Data.Items)
            {
                if (RP is randompresetsAttachments)
                {
                    randompresetsAttachments RPA = RP as randompresetsAttachments;
                    TreeNode IN = new TreeNode(GetpresetString(RP))
                    {
                        Tag = RPA
                    };
                    foreach (randompresetsItem RPI in RPA.item)
                    {
                        IN.Nodes.Add(CreateRPItem(RPI));
                    }
                    Attatchnode.Nodes.Add(IN);
                }
                else if (RP is randompresetsCargo)
                {
                    randompresetsCargo RPC = RP as randompresetsCargo;
                    TreeNode IN = new TreeNode(GetpresetString(RP))
                    {
                        Tag = RPC
                    };
                    foreach (randompresetsItem RPI in RPC.item)
                    {
                        IN.Nodes.Add(CreateRPItem(RPI));
                    }
                    CargoNode.Nodes.Add(IN);
                }

            }
            ConfigRoot.Nodes.Add(Attatchnode);
            ConfigRoot.Nodes.Add(CargoNode);
            return ConfigRoot;
        }
        private string GetpresetString(object Preset)
        {
            if (Preset is randompresetsAttachments)
            {
                randompresetsAttachments RPA = Preset as randompresetsAttachments;
                return $"Name = {RPA.name}, Chance = {RPA.chance}";
            }
            else if (Preset is randompresetsCargo)
            {
                randompresetsCargo RPC = Preset as randompresetsCargo;
                return $"Name = {RPC.name}, Chance = {RPC.chance}";
            }
            return null;
        }
        private TreeNode CreateRPItem(randompresetsItem item)
        {
            TreeNode treeNode = new TreeNode(GetRPItemString(item))
            {
                Tag = item
            };
            return treeNode;
        }
        private string GetRPItemString(randompresetsItem item)
        {
            return $"Name = {item.name}, Chance = {item.chance}";
        }
        //Creating Event and event spawn Nodes
        private TreeNode CreateEventNodes(EventsFile ef)
        {
            TreeNode eventRoot = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            foreach (eventsEvent _event in ef.Data.@event)
            {
                TreeNode ev = new TreeNode(_event.name)
                {
                    Tag = _event
                };
                eventposdefEvent points = _economyManager.cfgeventspawnsConfig.Findevent(_event.name);
                if (points != null)
                {
                    ev.Nodes.Add(CreateeventSpawnsNodes(points));
                }
                eventRoot.Nodes.Add(ev);
            }
            return eventRoot;
        }
        private TreeNode CreateeventSpawnsNodes(eventposdefEvent eventspawns)
        {
            TreeNode eventspawnroot = new TreeNode($"Event Spawn: {eventspawns.name}")
            {
                Tag = eventspawns
            };
            if (eventspawns.zone != null)
            {
                eventposdefEventZone zone = eventspawns.zone;
                TreeNode zonenode = new TreeNode("zone");
                zonenode.Name = "ZONE";
                zonenode.Tag = zone;
                eventspawnroot.Nodes.Add(zonenode);
            }
            if (eventspawns.pos != null && eventspawns.pos.Count > 0)
            {
                eventspawnroot.Nodes.Add(CreateEventposnodes(eventspawns));
            }
            return eventspawnroot;
        }

        private TreeNode CreateEventposnodes(eventposdefEvent? eventspawns)
        {

                TreeNode eventposnodes = new TreeNode("pos");
                eventposnodes.Name = "POS";
                eventposnodes.Tag = "PosParent";
                foreach (eventposdefEventPos pos in eventspawns.pos)
                {
                    eventposnodes.Nodes.Add(CreateEventPositionNode(pos));
                }
                return eventposnodes;
        }

        private TreeNode CreateEventPositionNode(eventposdefEventPos pos)
        {
            TreeNode posnodes = new TreeNode(pos.ToString());
            posnodes.Tag = pos;
            if (pos.group != null)
            {
                eventgroupdefGroup evg = _economyManager.cfgeventgroupsConfig.getassociatedgroup(pos.group);
                if (evg != null)
                {
                    TreeNode neweventgroup = new TreeNode($"Group Name : {evg.name}");
                    neweventgroup.Tag = evg;
                    if (evg.child != null && evg.child.Count > 0)
                    {
                        foreach (eventgroupdefGroupChild child in evg.child)
                        {
                            TreeNode childnode = new TreeNode(child.ToString());
                            childnode.Tag = child;
                            neweventgroup.Nodes.Add(childnode);
                        }
                    }
                    posnodes.Nodes.Add(neweventgroup);
                }
            }

            return posnodes;
        }

        //CreatecfgeconomycoreConfigConfigNodes
        private TreeNode CreatecfgeconomycoreConfigConfigNodes(economyCoreConfig config)
        {
            TreeNode PlayerSpawnPointrootNode = new TreeNode(config.FileName)
            {
                Tag = config
            };
            return PlayerSpawnPointrootNode;
        }
        //creating playerspawnpoint Nodes
        private TreeNode CreatecfgPlayerSpawnPointNodes(cfgplayerspawnpointsConfig config)
        {
            TreeNode PlayerSpawnPointrootNode = new TreeNode(config.FileName)
            {
                Tag = config
            };
            if (config.Data.fresh != null)
            {
                PlayerSpawnPointrootNode.Nodes.Add(createPlayerspawnNodes(config.Data.fresh, "Fresh"));
            }
            if (config.Data.hop != null)
            {
                PlayerSpawnPointrootNode.Nodes.Add(createPlayerspawnNodes(config.Data.hop, "Hop"));
            }
            ;
            if (config.Data.travel != null)
            {
                PlayerSpawnPointrootNode.Nodes.Add(createPlayerspawnNodes(config.Data.travel, "Travel"));
            }
            ;
            return PlayerSpawnPointrootNode;
        }
        private TreeNode createPlayerspawnNodes(playerspawnpointssection Spawntype, string TypeofSpawn)
        {
            TreeNode Spawntypenode = new TreeNode(TypeofSpawn)
            {
                Tag = Spawntype
            };
            Spawntypenode.Nodes.Add(new TreeNode("Spawn Params")
            {
                Tag = Spawntype.spawn_params
            });
            Spawntypenode.Nodes.Add(new TreeNode("Generator Params")
            {
                Tag = Spawntype.generator_params
            });
            Spawntypenode.Nodes.Add(new TreeNode("Group Params")
            {
                Tag = Spawntype.group_params
            });
            TreeNode Bubbles = new TreeNode("Generator Posbubbles")
            {
                Tag = "PlayerSpawnGenPosBubbles"
            };
            if (Spawntype.generator_posbubbles != null && Spawntype.generator_posbubbles.Count > 0)
            {
                // Handle groups
                var groups = new BindingList<playerspawnpointsGroup>(
                    Spawntype.generator_posbubbles.OfType<playerspawnpointsGroup>().ToList()
                );
                if (groups.Count > 0)
                    CreateGeneratorPosBubblesGroups(Bubbles, groups);

                // Handle positions
                var positions = new BindingList<playerspawnpointsGroupPos>(
                    Spawntype.generator_posbubbles.OfType<playerspawnpointsGroupPos>().ToList()
                );
                if (positions.Count > 0)
                    CreateGeneratorPosBubblesPoints(Bubbles, positions);
            }

            Spawntypenode.Nodes.Add(Bubbles);
            return Spawntypenode;
        }
        private void CreateGeneratorPosBubblesPoints(TreeNode bubbles, BindingList<playerspawnpointsGroupPos> generator_posbubbles)
        {
            foreach (playerspawnpointsGroupPos point in generator_posbubbles)
            {
                bubbles.Nodes.Add(new TreeNode(point.ToString())
                {
                    Tag = point
                });
            }
        }
        private void CreateGeneratorPosBubblesGroups(TreeNode bubbles, BindingList<playerspawnpointsGroup> generator_posbubbles)
        {
            foreach (playerspawnpointsGroup group in generator_posbubbles)
            {
                TreeNode groupnode = new TreeNode($"Group Name: {group.name}")
                {
                    Tag = group
                };
                foreach (playerspawnpointsGroupPos point in group.pos)
                {
                    groupnode.Nodes.Add(new TreeNode(point.ToString())
                    {
                        Tag = point
                    });
                }
                bubbles.Nodes.Add(groupnode);
            }
        }
        //Creating Definntions
        private TreeNode CreatelimitsDefininitionsConfigNodes(cfglimitsdefinitionConfig cfglimitsdefinitionConfig)
        {
            TreeNode eventRoot = new TreeNode(cfglimitsdefinitionConfig.FileName)
            {
                Tag = cfglimitsdefinitionConfig
            };
            TreeNode Cat = new TreeNode("Categories")
            {
                Tag = "DefsCategories"
            };
            TreeNode Tag = new TreeNode("Tags")
            {
                Tag = "DefsTags"
            };
            TreeNode UsageFlag = new TreeNode("Usage Flags")
            {
                Tag = "DefsUsageFlags"
            };
            TreeNode ValueFlag = new TreeNode("Value Flags")
            {
                Tag = "DefsValueFlags"
            };
            eventRoot.Nodes.Add(Cat);
            eventRoot.Nodes.Add(Tag);
            eventRoot.Nodes.Add(UsageFlag);
            eventRoot.Nodes.Add(ValueFlag);
            return eventRoot;
        }
        private TreeNode CreatelimitsDefininitionsUserConfigNodes(cfglimitsdefinitionuserConfig cfglimitsdefinitionuserConfig)
        {
            TreeNode eventRoot = new TreeNode(cfglimitsdefinitionuserConfig.FileName)
            {
                Tag = cfglimitsdefinitionuserConfig
            };
            TreeNode UsageFlag = new TreeNode("Usage Flags")
            {
                Tag = "DefsUserUsageFlags"
            };
            TreeNode ValueFlag = new TreeNode("Value Flags")
            {
                Tag = "DefsUserValueFlags"
            };
            eventRoot.Nodes.Add(UsageFlag);
            eventRoot.Nodes.Add(ValueFlag);
            return eventRoot;
        }
        //creating ubergroundnodes
        private TreeNode CreatecfgundergroundtriggersConfigUserConfigNodes(cfgundergroundtriggersConfig config)
        {
            TreeNode rootNode = new TreeNode(config.FileName)
            {
                Tag = config
            };
            foreach (Trigger t in _economyManager.cfgundergroundtriggersConfig.Data.Triggers)
            {
                string triggeryype = t.gettriggertype();
                TreeNode triggernode = new TreeNode($"{triggeryype}")
                {
                    Tag = t
                };
                for (int i = 0; i < t.Breadcrumbs.Count; i++)
                {
                    TreeNode bredcrumb = new TreeNode($"BreadCrumb:{i}")
                    {
                        Tag = t.Breadcrumbs[i]
                    };
                    triggernode.Nodes.Add(bredcrumb);
                }
                rootNode.Nodes.Add(triggernode);
            }
            return rootNode;
        }
        //creating trigger effect nodes
        private TreeNode CreatecfgeffectareaConfigConfigNodes(CfgeffectareaConfig config)
        {
            var root = new TreeNode(config.FileName) { Tag = config };

            if (config.Data == null)
                return root;

            // --- Areas ---
            var areasNode = new TreeNode("Areas")
            {
                Tag = "cfgeffectareaareas"
            };
            if (config.Data.Areas != null)
            {
                foreach (var area in config.Data.Areas)
                {
                    areasNode.Nodes.Add(createeffectareanodes(area));
                }
            }
            root.Nodes.Add(areasNode);

            // --- SafePositions ---
            var safePosNode = new TreeNode("SafePositions")
            {
                Tag = "cfgeffectareaSafePositions"
            };
            if (config.Data._positions != null)
            {
                int i = 1;
                foreach (var pos in config.Data._positions)
                {
                    safePosNode.Nodes.Add(new TreeNode($"Position {i} ({pos.X}, {pos.Z})")
                    {
                        Tag = pos
                    });
                    i++;
                }
            }
            root.Nodes.Add(safePosNode);

            return root;
        }
        private static TreeNode createeffectareanodes(Areas area)
        {
            var areaNode = new TreeNode(area.AreaName ?? "Unnamed Area")
            {
                Tag = area
            };

            // Only stop at Data + PlayerData
            if (area.Data != null)
                areaNode.Nodes.Add(new TreeNode("Data") { Tag = area.Data });
            if (area.PlayerData != null)
                areaNode.Nodes.Add(new TreeNode("PlayerData") { Tag = area.PlayerData });
            return areaNode;
        }
        //creating weather Nodes
        private TreeNode CreatecfgweatherNodes(cfgweatherConfig config)
        {
            TreeNode weatherrootNode = new TreeNode(config.FileName)
            {
                Tag = config
            };

            if (config.Data != null) // assuming cfgweatherConfig has a Weather property
            {
                weatherrootNode.Nodes.Add(new TreeNode($"Enable: {(config.Data.enable != 0)}") { Tag = "weatherEnable" });
                weatherrootNode.Nodes.Add(new TreeNode($"Reset: {(config.Data.reset != 0)}") { Tag = "wetaherReset" });
                weatherrootNode.Nodes.Add(new TreeNode("Overcast") { Tag = config.Data.overcast });
                weatherrootNode.Nodes.Add(new TreeNode("Fog") { Tag = config.Data.fog });
                weatherrootNode.Nodes.Add(new TreeNode("Rain") { Tag = config.Data.rain });
                weatherrootNode.Nodes.Add(new TreeNode("WindMagnitude") { Tag = config.Data.windMagnitude });
                weatherrootNode.Nodes.Add(new TreeNode("WindDirection") { Tag = config.Data.windDirection });
                weatherrootNode.Nodes.Add(new TreeNode("Snowfall") { Tag = config.Data.snowfall });
                weatherrootNode.Nodes.Add(new TreeNode("Storm") { Tag = config.Data.storm });
            }

            return weatherrootNode;
        }
        //creating weather Nodes
        private TreeNode CreatecfgignorelistNodes(cfgignorelistConfig config)
        {
            TreeNode ignorelistrootNode = new TreeNode(config.FileName)
            {
                Tag = config
            };
            foreach (ignoreType ignoreType in config.Data.type)
            {
                TreeNode typenode = new TreeNode(ignoreType.name)
                {
                    Tag = ignoreType
                };
                ignorelistrootNode.Nodes.Add(typenode);
            }

            return ignorelistrootNode;
        }
        //CreatemapgeroupposNodes
        private TreeNode CreatemapgeroupposNodes(mapgroupposConfig config)
        {
            TreeNode ignorelistrootNode = new TreeNode(config.FileName)
            {
                Tag = config
            };
            foreach (mapGroup MGPM in config.Data.group)
            {
                if (!ignorelistrootNode.Nodes.ContainsKey(MGPM.name))
                {
                    ignorelistrootNode.Nodes.Add(new TreeNode(MGPM.name)
                    {
                        Name = MGPM.name,
                        Tag = "MapGroup:" + MGPM.name
                    });
                }
                ignorelistrootNode.Nodes[MGPM.name].Nodes.Add(new TreeNode(MGPM.name) { Tag = MGPM });
            }

            return ignorelistrootNode;
        }
        //CreatemapgeroupprotoNodes
        private TreeNode CreatemapgeroupprotoNodes(mapgroupprotoConfig config)
        {
            TreeNode mapgroupprotoNodes = new TreeNode(config.FileName)
            {
                Tag = config
            };
            TreeNode Defaultnodes = new TreeNode("Defaults")
            {
                Tag = "mapgroupprotoDefaults"
            };
            foreach (prototypeDefault prototypeDefault in config.Data.defaults)
            {
                Defaultnodes.Nodes.Add(new TreeNode(prototypeDefault.ToString())
                {
                    Tag = prototypeDefault
                });
            }
            mapgroupprotoNodes.Nodes.Add(Defaultnodes);
            TreeNode Groupnodes = new TreeNode("Groups")
            {
                Tag = "mapgroupprotoGroups"
            };
            foreach (prototypeGroup prototypeGroup in config.Data.group)
            {
                TreeNode groupnode = new TreeNode(prototypeGroup.ToString())
                {
                    Tag = prototypeGroup
                };
                foreach (prototypeGroupContainer container in prototypeGroup.container)
                {
                    TreeNode containernode = new TreeNode(container.ToString())
                    {
                        Tag = container
                    };
                    groupnode.Nodes.Add(containernode);
                }
                Groupnodes.Nodes.Add(groupnode);
            }
            mapgroupprotoNodes.Nodes.Add(Groupnodes);
            return mapgroupprotoNodes;
        }
        #endregion loading treeview

        #region loading newtreeview
        private void LoadTreeview()
        {
            EconomyTV.BeginUpdate();
            try
            {
                EconomyTV.Nodes.Clear();

                TreeNode rootNode = new TreeNode(Path.GetFileName(_economyManager.basePath))
                {
                    Tag = "RootNode"
                };

                rootNode.Nodes.Add(BuildCoreNode());
                rootNode.Nodes.Add(BuildEnvironmentNode());
                rootNode.Nodes.Add(BuildEconomyDatabaseNode());
                rootNode.Nodes.Add(BuildMapPlacementNode());
                rootNode.Nodes.Add(BuildAreaEffectsNode());
                rootNode.Nodes.Add(BuildGameplayNode());
                TreeNode warningsNode = BuildWarningsNode();
                if (warningsNode != null)
                    rootNode.Nodes.Add(warningsNode);

                EconomyTV.Nodes.Add(rootNode);
                rootNode.Expand();
            }
            finally
            {
                EconomyTV.EndUpdate();
            }
        }
        private static TreeNode CreateCategoryNode(string text, object tag)
        {
            return new TreeNode(text)
            {
                Tag = tag
            };
        }
        private TreeNode BuildCoreNode()
        {
            TreeNode coreNode = CreateCategoryNode("Core", "CoreCategory");

            if (_economyManager.eonomyCoreConfig != null)
                coreNode.Nodes.Add(CreatecfgeconomycoreConfigConfigNodes(_economyManager.eonomyCoreConfig));

            if (_economyManager.cfglimitsdefinitionConfig != null)
                coreNode.Nodes.Add(CreatelimitsDefininitionsConfigNodes(_economyManager.cfglimitsdefinitionConfig));

            if (_economyManager.cfglimitsdefinitionuserConfig != null)
                coreNode.Nodes.Add(CreatelimitsDefininitionsUserConfigNodes(_economyManager.cfglimitsdefinitionuserConfig));

            return coreNode;
        }
        private TreeNode BuildEnvironmentNode()
        {
            TreeNode environmentNode = new TreeNode("Environment")
            {
                Tag = "EnvironmentCategory"
            };

            if (_economyManager.cfgenvironmentConfig != null)
                environmentNode.Nodes.Add(CreateEnviromentConfigNodes(_economyManager.cfgenvironmentConfig));

            if (_economyManager.cfgweatherConfig != null)
                environmentNode.Nodes.Add(CreatecfgweatherNodes(_economyManager.cfgweatherConfig));

            return environmentNode;
        }
        private TreeNode BuildEconomyDatabaseNode()
        {
            TreeNode dbNode = CreateCategoryNode("Economy Database", "EconomyDatabaseCategory");

            TreeNode economyNode = CreateCategoryNode("Economy", "EconomyCategory");
            foreach (var ef in _economyManager.economyConfig.MutableItems)
                economyNode.Nodes.Add(CreateEconomyfileNodes(ef));
            dbNode.Nodes.Add(economyNode);

            TreeNode globalsNode = CreateCategoryNode("Globals", "GlobalsCategory");
            foreach (var gf in _economyManager.globalsConfig.MutableItems)
                globalsNode.Nodes.Add(CreateGlobalsfileNodes(gf));
            dbNode.Nodes.Add(globalsNode);

            TreeNode typesNode = CreateCategoryNode("Types", "TypesCategory");
            foreach (var tf in _economyManager.TypesConfig.MutableItems)
                typesNode.Nodes.Add(CreateTypesfileNodes(tf));
            dbNode.Nodes.Add(typesNode);

            TreeNode spawnableTypesNode = CreateCategoryNode("Spawnable Types", "SpawnableTypesCategory");
            foreach (var sf in _economyManager.cfgspawnabletypesConfig.MutableItems)
                spawnableTypesNode.Nodes.Add(CreateSpawnableTypesfileNodes(sf));
            dbNode.Nodes.Add(spawnableTypesNode);

            TreeNode randomPresetsNode = CreateCategoryNode("Random Presets", "RandomPresetsCategory");
            foreach (var rf in _economyManager.cfgrandompresetsConfig.MutableItems)
                randomPresetsNode.Nodes.Add(CreateRandomPresetsFileNodes(rf));
            dbNode.Nodes.Add(randomPresetsNode);

            TreeNode eventsNode = CreateCategoryNode("Events", "EventsCategory");
            foreach (var ef in _economyManager.eventsConfig.MutableItems)
                eventsNode.Nodes.Add(CreateEventNodes(ef));
            dbNode.Nodes.Add(eventsNode);

            return dbNode;
        }
        private TreeNode BuildMapPlacementNode()
        {
            TreeNode mapPlacementNode = CreateCategoryNode("Map Placement", "MapPlacementCategory");

            if (_economyManager.mapgroupposConfig != null)
                mapPlacementNode.Nodes.Add(CreatemapgeroupposNodes(_economyManager.mapgroupposConfig));

            if (_economyManager.mapgroupprotoConfig != null)
                mapPlacementNode.Nodes.Add(CreatemapgeroupprotoNodes(_economyManager.mapgroupprotoConfig));

            //if (_economyManager.mapgroup != null)
            //    mapPlacementNode.Nodes.Add(CreatemapgroupclusterNodes(_economyManager.mapgroupclusterConfig));

            //if (_economyManager.mapclusterprotoConfig != null)
            //    mapPlacementNode.Nodes.Add(CreatemapclusterprotoNodes(_economyManager.mapclusterprotoConfig));

            return mapPlacementNode;
        }
        private TreeNode BuildAreaEffectsNode()
        {
            TreeNode areaEffectsNode = CreateCategoryNode("Area Effects", "AreaEffectsCategory");

            if (_economyManager.cfgeffectareaConfig != null)
                areaEffectsNode.Nodes.Add(CreatecfgeffectareaConfigConfigNodes(_economyManager.cfgeffectareaConfig));

            if (_economyManager.cfgundergroundtriggersConfig != null)
                areaEffectsNode.Nodes.Add(CreatecfgundergroundtriggersConfigUserConfigNodes(_economyManager.cfgundergroundtriggersConfig));

            return areaEffectsNode;
        }
        private TreeNode BuildGameplayNode()
        {
            TreeNode gameplayNode = CreateCategoryNode("Player / Gameplay", "GameplayCategory");

            if (_economyManager.CFGGameplayConfig != null)
                gameplayNode.Nodes.Add(CreateGameplayConfigNodes(_economyManager.CFGGameplayConfig));

            if (_economyManager.cfgplayerspawnpointsConfig != null)
                gameplayNode.Nodes.Add(CreatecfgPlayerSpawnPointNodes(_economyManager.cfgplayerspawnpointsConfig));

            if (_economyManager.cfgignorelistConfig != null)
                gameplayNode.Nodes.Add(CreatecfgignorelistNodes(_economyManager.cfgignorelistConfig));

            TreeNode initNode = new TreeNode("init.c")
            {
                Tag = "INITC"
            };
            gameplayNode.Nodes.Add(initNode);

            return gameplayNode;
        }
        private TreeNode BuildWarningsNode()
        {
            if (_economyManager.WarningList.Count == 0)
                return null;

            TreeNode warningsNode = new TreeNode($"Warnings ({_economyManager.WarningList.Count})")
            {
                Tag = "WarningsCategory"
            };

            var grouped = _economyManager.WarningList
                .GroupBy(w => w.Group)
                .OrderBy(g => g.Key);

            foreach (var group in grouped)
            {
                TreeNode groupNode = new TreeNode($"{group.Key} ({group.Count()})")
                {
                    Tag = $"WarningsGroup:{group.Key}"
                };

                foreach (var warning in group.OrderBy(w => w.Title))
                {
                    TreeNode warningNode = new TreeNode(warning.Title)
                    {
                        Tag = warning
                    };

                    groupNode.Nodes.Add(warningNode);
                }

                warningsNode.Nodes.Add(groupNode);
            }

            return warningsNode;
        }
        private static string NormalizePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return string.Empty;

            return path
                .Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar)
                .Trim();
        }
        private void AddFileToDBTree(object file, bool selectAndExpand = true)
        {
            if (EconomyTV.Nodes.Count == 0)
                return;

            TreeNode rootNode = EconomyTV.Nodes[0];

            TreeNode dbNode = GetOrCreateChildNode(rootNode, "Economy Database", "EconomyDatabaseCategory");

            TreeNode targetCategoryNode = null;
            TreeNode newFileNode = null;

            switch (file)
            {
                case TypesFile tf:
                    targetCategoryNode = GetOrCreateChildNode(dbNode, "Types", "TypesCategory");
                    newFileNode = CreateTypesfileNodes(tf);
                    break;

                case EventsFile ef:
                    targetCategoryNode = GetOrCreateChildNode(dbNode, "Events", "EventsCategory");
                    newFileNode = CreateEventNodes(ef);
                    break;

                case CfgSpawnableTypesFile sf:
                    targetCategoryNode = GetOrCreateChildNode(dbNode, "Spawnable Types", "SpawnableTypesCategory");
                    newFileNode = CreateSpawnableTypesfileNodes(sf);
                    break;

                case CfgrandompresetsFile rf:
                    targetCategoryNode = GetOrCreateChildNode(dbNode, "Random Presets", "RandomPresetsCategory");
                    newFileNode = CreateRandomPresetsFileNodes(rf);
                    break;

                case GlobalsFile gf:
                    targetCategoryNode = GetOrCreateChildNode(dbNode, "Globals", "GlobalsCategory");
                    newFileNode = CreateGlobalsfileNodes(gf);
                    break;

                case EconomyFile ef2:
                    targetCategoryNode = GetOrCreateChildNode(dbNode, "Economy", "EconomyCategory");
                    newFileNode = CreateEconomyfileNodes(ef2);
                    break;

                default:
                    return; // unknown type, ignore or log
            }

            if (newFileNode == null)
                return;

            targetCategoryNode.Nodes.Add(newFileNode);

            if (selectAndExpand)
            {
                rootNode.Expand();
                dbNode.Expand();
                targetCategoryNode.Expand();
                newFileNode.Expand();
                EconomyTV.SelectedNode = newFileNode;
            }
        }
        private TreeNode GetOrCreateChildNode(TreeNode parent, string text, object tag)
        {
            TreeNode existingNode = parent.Nodes
                .Cast<TreeNode>()
                .FirstOrDefault(n => n.Text.Equals(text, StringComparison.OrdinalIgnoreCase));

            if (existingNode != null)
                return existingNode;

            TreeNode newNode = new TreeNode(text)
            {
                Tag = tag
            };

            parent.Nodes.Add(newNode);
            return newNode;
        }
        private void InsertNodeSorted(TreeNode parent, TreeNode child)
        {
            int insertIndex = 0;

            for (int i = 0; i < parent.Nodes.Count; i++)
            {
                TreeNode existing = parent.Nodes[i];

                if (string.Compare(child.Text, existing.Text, StringComparison.OrdinalIgnoreCase) < 0)
                {
                    insertIndex = i;
                    break;
                }

                insertIndex = i + 1;
            }

            parent.Nodes.Insert(insertIndex, child);
        }
        #endregion
        /// <summary>
        /// Treeview stuff
        /// </summary>
        private void HandleTreeViewSelection<TFile, TSection>(
            TFile file,
            string sectionName,
            Func<TFile, TSection> getSection,
            TreeView treeView)
            where TFile : class
            where TSection : class
        {
            if (treeView.Nodes.Count == 0)
                return;

            TreeNode rootNode = treeView.Nodes[0];

            TreeNode fileNode = FindNodeRecursive(rootNode.Nodes, file);

            if (fileNode == null)
            {
                AddFileToDBTree(file, false);
                fileNode = FindNodeRecursive(rootNode.Nodes, file);
            }

            if (fileNode == null)
                return;

            TreeNode sectionNode = FindOrCreateSectionNode(fileNode, sectionName, getSection(file));

            treeView.SelectedNode = sectionNode;
            sectionNode.EnsureVisible();
        }
        private TreeNode FindNodeRecursive<T>(TreeNodeCollection nodes, T data)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag is T t && ReferenceEquals(t, data))
                    return node;

                TreeNode childResult = FindNodeRecursive(node.Nodes, data);
                if (childResult != null)
                    return childResult;
            }
            return null;
        }
        private TreeNode FindOrCreateSectionNode<T>(TreeNode parent, string nodeName, T sectionData)
        {
            foreach (TreeNode node in parent.Nodes)
            {
                // For EconomySection: check if node text starts with the section name and tag is same type
                if (sectionData is EconomySection)
                {
                    if (node.Text.StartsWith(nodeName) && node.Tag is EconomySection)
                        return node;
                }
                // For variablesVar: match exact name and type
                else if (sectionData is variablesVar variablevar)
                {
                    if (node.Tag is variablesVar existingVar && existingVar.name == variablevar.name)
                        return node;
                }
                // Fallback: match exact text and type
                else
                {
                    if (node.Text == nodeName && node.Tag is T)
                        return node;
                }
            }

            // If no existing node found, create new node
            TreeNode newSectionNode = null;
            if (sectionData is EconomySection economysection)
            {
                newSectionNode = new TreeNode($"{nodeName} init:{economysection.init} load:{economysection.load} respawn:{economysection.respawn} save:{economysection.save}")
                {
                    Tag = sectionData
                };
            }
            else if (sectionData is variablesVar variablevar)
            {
                newSectionNode = new TreeNode($"{variablevar.name} = {variablevar.value}")
                {
                    Tag = sectionData
                };
            }
            else
            {
                newSectionNode = new TreeNode(nodeName)
                {
                    Tag = sectionData
                };
            }

            parent.Nodes.Add(newSectionNode);
            return newSectionNode;
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
            _mapControl.MapDoubleClicked -= MapControl_EventSpawnDoubleclicked;
            _mapControl.MapsingleClicked -= MapControl_EventSpawnSingleclicked;

            _mapControl.MapDoubleClicked -= MapControl_EffectSafePositionsDoubleclicked;
            _mapControl.MapsingleClicked -= MapControl_EffectSafePositionsSingleclicked;

            _mapControl.MapDoubleClicked -= MapControl_EffectPRABoxDoubleclicked;
            _mapControl.MapsingleClicked -= MapControl_EffectPRABoxSingleclicked;

            _mapControl.MapDoubleClicked -= MapControl_EffectPRASafePositionsDoubleclicked;
            _mapControl.MapsingleClicked -= MapControl_EffectPRASafePositionsSingleclicked;

            _mapControl.MapDoubleClicked -= MapControl_EffectAreaDoubleclicked;
            _mapControl.MapsingleClicked -= MapControl_EffectAreaSingleclicked;

            _mapControl.MapDoubleClicked -= MapControl_PlayerSpawnDoubleclicked;
            _mapControl.MapsingleClicked -= MapControl_PlayerSpawnSingleclicked;

            _mapControl.MapDoubleClicked -= MapControl_MapGroupPosDoubleclicked;
            _mapControl.MapsingleClicked -= MapControl_MapGroupPosSingleclicked;

            _mapControl.MapDoubleClicked -= MapControl_TerritoriesDoubleclicked;
            _mapControl.MapDoubleClicked -= MapControl_TerritoriesSingleclicked;

            // Reset "selected" state objects
            _selectedEventPos = null;
            _selectedSafePosition = null;
            _selectedRPABox = null;
            _selectedRPASafePosition = null;
            _selectedeffectarea = null;
            _selectedSpawnpointPosition = null;
            _selectedMapGroupPosPosition = null;
            _selectedterritory = null;

            TerritorieszonesCB.Visible = false;
        }
        private void EconomyTV_AfterSelect(object sender, TreeViewEventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                ResetMapControl();

                currentTreeNode = e.Node;
                var selectedNodes = EconomyTV.SelectedNodes.Cast<TreeNode>().ToList();

                if (e.Node.Tag is EconomyWarning warning)
                {
                    HandleWarningNodeSelection(warning);
                    return;
                }

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
                    // Fallback: handle "MapGroup:*"
                    if (key.StartsWith("MapGroup:"))
                    {
                        mapgroupposConfig pos = e.Node.Parent.Tag as mapgroupposConfig;
                        ShowHandler<IUIHandler>(null, null, null, null);
                        SetupMapGroupMap(pos, e.Node);
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
        private void HandleWarningNodeSelection(EconomyWarning warning)
        {
            if (warning == null)
                return;

            var sb = new StringBuilder();

            sb.AppendLine($"[{warning.Code}]");
            sb.AppendLine();
            sb.AppendLine(warning.Title);
            sb.AppendLine(new string('-', 40));
            sb.AppendLine(warning.Message);
            sb.AppendLine();

            if (!string.IsNullOrWhiteSpace(warning.SourceFile))
                sb.AppendLine($"File: {warning.SourceFile}");

            if (!string.IsNullOrWhiteSpace(warning.RelatedFile))
                sb.AppendLine($"Related: {warning.RelatedFile}");

            sb.AppendLine();
            sb.AppendLine("Do you want to navigate to this item?");

            var result = MessageBox.Show(
                sb.ToString(),
                "Warning Details",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                NavigateToWarning(warning);
            }
        }
        private void NavigateToWarning(EconomyWarning warning)
        {
            if (warning?.SourceObject == null)
                return;

            switch (warning.SourceObject)
            {
                case TypesFile tf:
                    AddFileToDBTree(tf, true);
                    SelectNodeForObject(tf);
                    break;

                case EventsFile ef:
                    AddFileToDBTree(ef, true);
                    SelectNodeForObject(ef);
                    break;

                case GlobalsFile gf:
                    AddFileToDBTree(gf, true);
                    SelectNodeForObject(gf);
                    break;

                case EconomyFile eco:
                    AddFileToDBTree(eco, true);
                    SelectNodeForObject(eco);
                    break;

                case TypeEntry typeEntry:
                    SelectNodeForObject(typeEntry);
                    break;

                case eventsEvent evt:
                    SelectNodeForObject(evt);
                    break;

                case eventgroupdefGroup group:
                    SelectNodeForObject(group);
                    break;

                default:
                    // fallback: try selecting source object anyway
                    SelectNodeForObject(warning.SourceObject);
                    break;
            }
        }
        private void SelectNodeForObject(object data)
        {
            if (EconomyTV.Nodes.Count == 0)
                return;

            TreeNode node = FindNodeRecursive(EconomyTV.Nodes, data);
            if (node != null)
            {
                EconomyTV.SelectedNode = node;
                node.EnsureVisible();
            }
        }
        private TreeNode FindNodeByTag(TreeNodeCollection nodes, object tagToFind)
        {
            foreach (TreeNode node in nodes)
            {
                if (ReferenceEquals(node.Tag, tagToFind))
                    return node;

                var found = FindNodeByTag(node.Nodes, tagToFind);
                if (found != null)
                    return found;
            }
            return null;
        }
        private void EconomyTV_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            EconomyTV.SelectedNode = e.Node;
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
        private void editPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTreeNode?.Tag is EconomySection section)
            {
                EconomyFile file = currentTreeNode.Parent?.Tag as EconomyFile;
                if (file != null && !file.IsModded)
                {
                    HandleVanillaeconomyEditRedirect(file, section, currentTreeNode.Text.Split(' ')[0]);
                    return;
                }
            }
            else if (currentTreeNode?.Tag is variablesVar variablevar)
            {
                GlobalsFile file = currentTreeNode.Parent?.Tag as GlobalsFile;
                if (file != null && !file.IsModded)
                {
                    HandleVanillaglobalsEditRedirect(file, variablevar, currentTreeNode.Text.Split(' ')[0]);
                    return;
                }
            }
        }
        private void setToDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTreeNode?.Tag is EconomySection section)
            {
                EconomyFile file = currentTreeNode.Parent?.Tag as EconomyFile;
                if (file != null && file.IsModded)
                {
                    bool removed = false;

                    // Remove the section based on which property it matches
                    if (file.Data.dynamic == section) { file.Data.dynamic = null; removed = true; }
                    if (file.Data.animals == section) { file.Data.animals = null; removed = true; }
                    if (file.Data.zombies == section) { file.Data.zombies = null; removed = true; }
                    if (file.Data.vehicles == section) { file.Data.vehicles = null; removed = true; }
                    if (file.Data.randoms == section) { file.Data.randoms = null; removed = true; }
                    if (file.Data.custom == section) { file.Data.custom = null; removed = true; }
                    if (file.Data.building == section) { file.Data.building = null; removed = true; }
                    if (file.Data.player == section) { file.Data.player = null; removed = true; }

                    if (removed)
                    {
                        file.IsDirty = true;

                        TreeNode fileNode = currentTreeNode.Parent; // file node in tree

                        // Check if there are no sections left in the file
                        bool noSectionsLeft =
                            file.Data.dynamic == null &&
                            file.Data.animals == null &&
                            file.Data.zombies == null &&
                            file.Data.vehicles == null &&
                            file.Data.randoms == null &&
                            file.Data.custom == null &&
                            file.Data.building == null &&
                            file.Data.player == null;

                        if (noSectionsLeft)
                        {
                            bool deleteDirectory;
                            string folderPathRel;
                            string fileName;

                            _economyManager.eonomyCoreConfig.RemoveCe(
                                file.FileName,
                                out folderPathRel,
                                out fileName,
                                out deleteDirectory
                            );

                            RemoveTreeNodeAndEmptyParents(currentTreeNode);
                            file.ToDelete = true;
                            AppServices.GetRequired<EconomyManager>().eonomyCoreConfig.Save();
                        }
                        else
                        {
                            // Just remove the variable node
                            currentTreeNode.Remove();
                        }

                        AppServices.GetRequired<EconomyManager>().economyConfig.Save();
                    }
                }
            }
            else if (currentTreeNode?.Tag is variablesVar variablevar)
            {
                GlobalsFile file = currentTreeNode.Parent?.Tag as GlobalsFile;
                if (file != null && file.IsModded)
                {
                    var removed = file.Data?.var?.Remove(variablevar) ?? false;

                    if (removed)
                    {
                        file.IsDirty = true;

                        TreeNode fileNode = currentTreeNode.Parent; // file node in tree

                        if (!file.Data.var.Any())
                        {
                            bool deleteDirectory;
                            string folderPathRel;
                            string fileName;

                            _economyManager.eonomyCoreConfig.RemoveCe(
                                file.FileName,
                                out folderPathRel,
                                out fileName,
                                out deleteDirectory
                            );

                            // Remove file node and clean up empty parent nodes
                            RemoveTreeNodeAndEmptyParents(currentTreeNode);
                            file.ToDelete = true;
                            AppServices.GetRequired<EconomyManager>().eonomyCoreConfig.Save();
                        }
                        else
                        {
                            // Just remove the variable node
                            currentTreeNode.Remove();
                        }

                        AppServices.GetRequired<EconomyManager>().globalsConfig.Save();
                    }
                }
            }
        }
        private void RemoveTreeNodeAndEmptyParents(TreeNode node)
        {
            TreeNode parent = node.Parent;
            node.Remove();

            while (parent != null && parent.Nodes.Count == 0)
            {
                TreeNode grandparent = parent.Parent;
                parent.Remove();
                parent = grandparent;
            }
        }

        /// <summary>
        /// MapViewer Draw Mothods
        /// </summary>
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
        private void SetupEventPosMap(eventposdefEventPos pos, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedEventPos = pos;
                _mapControl.MapDoubleClicked += MapControl_EventSpawnDoubleclicked;
                _mapControl.MapsingleClicked += MapControl_EventSpawnSingleclicked;

                var defEvent = node.Parent?.Parent?.Tag as eventposdefEvent;
                if (defEvent != null)
                    DrawEventSpawns(defEvent);
            });
        }
        private void SetupSafePosMap(cfgeffectareaSafePosition safe, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedSafePosition = safe;
                _mapControl.MapsingleClicked += MapControl_EffectSafePositionsSingleclicked;
                _mapControl.MapDoubleClicked += MapControl_EffectSafePositionsDoubleclicked;

                var cfg = node.FindParentOfType<CfgeffectareaConfig>();
                if (cfg != null)
                    DrawEffectSafePositions(cfg);
            });
        }
        private void SetupPRABoxMap(PRABoxes box, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedRPABox = box;
                _mapControl.MapsingleClicked += MapControl_EffectPRABoxSingleclicked;
                _mapControl.MapDoubleClicked += MapControl_EffectPRABoxDoubleclicked;

                var files = node.FindParentOfType<PlayerRestrictedFile>();
                if (files != null)
                    DrawPRABoxesPOsitions(files);
            });
        }
        private void SetupPRASafePosMap(PRASafePosition safe, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedRPASafePosition = safe;
                _mapControl.MapsingleClicked += MapControl_EffectPRASafePositionsSingleclicked;
                _mapControl.MapDoubleClicked += MapControl_EffectPRASafePositionsDoubleclicked;

                var files = node.FindParentOfType<PlayerRestrictedFile>();
                if (files != null)
                    DrawEffectPRASafePositions(files);
            });
        }
        private void SetupEffectAreaMap(Areas area, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedeffectarea = area;
                _mapControl.MapsingleClicked += MapControl_EffectAreaSingleclicked;
                _mapControl.MapDoubleClicked += MapControl_EffectAreaDoubleclicked;

                var cfg = node.FindParentOfType<CfgeffectareaConfig>();
                if (cfg != null)
                    DrawEffectEffectArea(cfg);
            });
        }
        private void SetupPlayerSpawnPosMap(playerspawnpointsGroupPos pos, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedSpawnpointPosition = pos;
                _mapControl.MapsingleClicked += MapControl_PlayerSpawnSingleclicked;
                _mapControl.MapDoubleClicked += MapControl_PlayerSpawnDoubleclicked;

                var section = node.FindParentOfType<playerspawnpointssection>();
                if (section != null)
                    DrawPlayerSpawnPointPositions(section);
            });
        }
        private void SetupMapGroupMap(mapgroupposConfig pos, TreeNode node)
        {
            SetupMap(() =>
            {
                _mapControl.MapsingleClicked += MapControl_MapGroupPosSingleclicked;

                DrawMapGroupPosPositions(pos);
            });
        }
        private void SetupMapGroupPosMap(mapGroup pos, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedMapGroupPosPosition = pos;
                _mapControl.MapsingleClicked += MapControl_MapGroupPosSingleclicked;
                _mapControl.MapDoubleClicked += MapControl_MapGroupPosDoubleclicked;

                var config = node.Parent.Parent.Tag as mapgroupposConfig;
                if (config != null)
                    DrawMapGroupPosPositions(config);
            });
        }
        private void SetupTerritoriesMap(territorytypeTerritoryZone zone, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedterritory = zone;
                _mapControl.MapDoubleClicked += MapControl_TerritoriesDoubleclicked;
                _mapControl.MapsingleClicked += MapControl_TerritoriesSingleclicked;

                TerritorieszonesCB.Visible = true;

                var config = node.Parent.Tag as territorytypeTerritory;
                if (config != null)
                    DrawTerritoriesPositions(config);
            });
        }
        //Draw Methods
        private void DrawPlayerSpawnPointPositions(playerspawnpointssection playerspawnpointssection)
        {
            foreach (playerspawnpointsGroupPos pos in playerspawnpointssection.Positions)
            {
                if (_selectedSpawnpointPosition == pos)
                {
                    var marker = new MarkerDrawable(new PointF((float)pos.x, (float)pos.z), _mapControl.MapSize)
                    {
                        Color = Color.LimeGreen,
                        Radius = 8,
                        Scaleradius = false
                    };
                    _mapControl.RegisterDrawable(marker);
                }
                else
                {
                    var marker = new MarkerDrawable(new PointF((float)pos.x, (float)pos.z), _mapControl.MapSize)
                    {
                        Color = Color.Red,
                        Radius = 8,
                        Scaleradius = false
                    };
                    _mapControl.RegisterDrawable(marker);
                }
            }
            foreach (playerspawnpointsGroup group in playerspawnpointssection.Groups)
            {
                foreach (playerspawnpointsGroupPos pos in group.pos)
                {
                    if (_selectedSpawnpointPosition == pos)
                    {
                        var marker = new MarkerDrawable(new PointF((float)pos.x, (float)pos.z), _mapControl.MapSize)
                        {
                            Color = Color.LimeGreen,
                            Radius = 8,
                            Scaleradius = false
                        };
                        _mapControl.RegisterDrawable(marker);
                    }
                    else
                    {
                        var marker = new MarkerDrawable(new PointF((float)pos.x, (float)pos.z), _mapControl.MapSize)
                        {
                            Color = Color.Red,
                            Radius = 8,
                            Scaleradius = false
                        };
                        _mapControl.RegisterDrawable(marker);
                    }
                }
            }
        }
        private void DrawPRABoxesPOsitions(PlayerRestrictedFile PlayerRestrictedFiles)
        {
            foreach (PRABoxes pos in PlayerRestrictedFiles.BoxesView)
            {
                if (_selectedRPABox == pos)
                {
                    var marker = new PRABoxDrawable(pos.HalfExtents, pos.Orientation, pos.Position, _mapControl.MapSize)
                    {
                        Color = Color.LimeGreen
                    };
                    _mapControl.RegisterDrawable(marker);
                }
                else
                {
                    var marker = new PRABoxDrawable(pos.HalfExtents, pos.Orientation, pos.Position, _mapControl.MapSize)
                    {
                        Color = Color.Red
                    };
                    _mapControl.RegisterDrawable(marker);
                }

            }
        }
        private void DrawEffectPRASafePositions(PlayerRestrictedFile PlayerRestrictedFiles)
        {
            foreach (PRASafePosition safeposition in PlayerRestrictedFiles.SafePositionsView)
            {
                if (_selectedRPASafePosition == safeposition)
                {
                    var marker = new MarkerDrawable(new PointF(safeposition.Position.X, safeposition.Position.Z), _mapControl.MapSize)
                    {
                        Color = Color.LimeGreen,
                        Radius = 8,
                        Scaleradius = false
                    };
                    _mapControl.RegisterDrawable(marker);
                }
                else
                {
                    var marker = new MarkerDrawable(new PointF(safeposition.Position.X, safeposition.Position.Z), _mapControl.MapSize)
                    {
                        Color = Color.Red,
                        Radius = 8,
                        Scaleradius = false
                    };
                    _mapControl.RegisterDrawable(marker);
                }
            }
        }
        private void DrawEffectSafePositions(CfgeffectareaConfig cfgeffectareaConfig)
        {
            foreach (cfgeffectareaSafePosition pos in cfgeffectareaConfig.Data._positions)
            {
                if (_selectedSafePosition == pos)
                {
                    var marker = new MarkerDrawable(new PointF((float)pos.X, (float)pos.Z), _mapControl.MapSize)
                    {
                        Color = Color.LimeGreen,
                        Radius = 8,
                        Scaleradius = false
                    };
                    _mapControl.RegisterDrawable(marker);
                }
                else
                {
                    var marker = new MarkerDrawable(new PointF((float)pos.X, (float)pos.Z), _mapControl.MapSize)
                    {
                        Color = Color.Red,
                        Radius = 8,
                        Scaleradius = false
                    };
                    _mapControl.RegisterDrawable(marker);
                }
            }
        }
        private void DrawEventSpawns(eventposdefEvent defevent)
        {
            foreach (eventposdefEventPos pos in defevent.pos)
            {
                var marker = new MarkerDrawable(new PointF((float)pos.x, (float)pos.z), _mapControl.MapSize)
                {
                    Color = Color.Red,
                    Radius = 8,
                    Scaleradius = false
                };
                if (_selectedEventPos == pos)
                {
                    marker.Color = Color.LimeGreen;
                }
                _mapControl.RegisterDrawable(marker);
            }
        }
        private void DrawEffectEffectArea(CfgeffectareaConfig cfgeffectareaConfig)
        {
            foreach (Areas area in cfgeffectareaConfig.Data.Areas)
            {
                if (_selectedeffectarea == area)
                {
                    var marker = new MarkerDrawable(new PointF((float)area.Data.Pos[0], (float)area.Data.Pos[2]), _mapControl.MapSize)
                    {
                        Color = Color.LimeGreen,
                        Radius = (float)area.Data.Radius,
                        Scaleradius = true
                    };
                    _mapControl.RegisterDrawable(marker);
                }
                else
                {
                    var marker = new MarkerDrawable(new PointF((float)area.Data.Pos[0], (float)area.Data.Pos[2]), _mapControl.MapSize)
                    {
                        Color = Color.Red,
                        Radius = (float)area.Data.Radius,
                        Scaleradius = true
                    };
                    _mapControl.RegisterDrawable(marker);
                }
            }
        }
        private void DrawMapGroupPosPositions(mapgroupposConfig mapgroupposConfig)
        {
            List<mapGroup> nodestodraw = new List<mapGroup>();
            foreach (TreeNode tn in EconomyTV.SelectedNodes)
            {
                nodestodraw.Add(tn.Tag as mapGroup);
            }
            foreach (mapGroup MGPMG in mapgroupposConfig.Data.group)
            {
                var marker = new MarkerDrawable(new PointF(Convert.ToSingle(MGPMG.pos.Split(' ')[0]), Convert.ToSingle(MGPMG.pos.Split(' ')[2])), _mapControl.MapSize)
                {
                    Color = Color.Red,
                    Radius = 8,
                    Scaleradius = false
                };
                if (nodestodraw.Contains(MGPMG))
                {
                    marker.Color = Color.LimeGreen;
                }
                _mapControl.RegisterDrawable(marker);
            }
        }
        private void DrawTerritoriesPositions(territorytypeTerritory territorytypeTerritory)
        {
            _mapControl.ClearDrawables();
            if (TerritorieszonesCB.Checked)
            {
                territorytype territorytype = currentTreeNode.FindParentOfType<territorytype>();
                foreach (territorytypeTerritory ttt in territorytype.territory)
                {
                    foreach (territorytypeTerritoryZone zone in ttt.zone)
                    {
                        string col = string.Format("{0:X}", ttt.color);
                        var territory = new TerritoryDrawable(new PointF((float)zone.x, (float)zone.z), _mapControl.MapSize)
                        {
                            Color = ColorTranslator.FromHtml("#" + col.Substring(2)),
                            Radius = (float)zone.r,
                            Scaleradius = true
                        };
                        if (_selectedterritory == zone)
                        {
                            territory.IsSelected = true;
                        }
                        _mapControl.RegisterDrawable(territory);
                    }
                }
            }
            else
            {
                foreach (territorytypeTerritoryZone zone in territorytypeTerritory.zone)
                {
                    string col = string.Format("{0:X}", territorytypeTerritory.color);
                    var territory = new TerritoryDrawable(new PointF((float)zone.x, (float)zone.z), _mapControl.MapSize)
                    {
                        Color = ColorTranslator.FromHtml("#" + col.Substring(2)),
                        Radius = (float)zone.r,
                        Scaleradius = true
                    };
                    if (_selectedterritory == zone)
                    {
                        territory.IsSelected = true;
                    }
                    _mapControl.RegisterDrawable(territory);
                }
            }
        }
        private void TerritorieszonesCB_CheckedChanged(object sender, EventArgs e)
        {
            territorytypeTerritory ttt = currentTreeNode.FindParentOfType<territorytypeTerritory>();
            DrawTerritoriesPositions(ttt);
        }

        // MapViewer clicks
        private eventposdefEventPos _selectedEventPos;
        private cfgeffectareaSafePosition _selectedSafePosition;
        private PRASafePosition _selectedRPASafePosition;
        private playerspawnpointsGroupPos _selectedSpawnpointPosition;
        private Areas _selectedeffectarea;
        private PRABoxes _selectedRPABox;
        private mapGroup _selectedMapGroupPosPosition;
        private territorytypeTerritoryZone _selectedterritory;

        private void MapControl_EventSpawnSingleclicked(object sender, MapClickEventArgs e)
        {
            if (currentTreeNode?.Parent == null)
                return;

            TreeNode parentNode = currentTreeNode.Parent;

            eventposdefEventPos closestPos = null;
            double closestDistance = double.MaxValue;

            PointF clickScreen = _mapControl.MapToScreen(e.MapCoordinates);

            // Loop through all child nodes of the parent
            foreach (TreeNode child in parentNode.Nodes)
            {
                if (child.Tag is eventposdefEventPos pos)
                {
                    // Node position in screen space
                    PointF posScreen = _mapControl.MapToScreen(new PointF((float)pos.x, (float)pos.z));

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
            if (closestPos != null && closestDistance < 10.0) // 10 units tolerance
            {
                // Select that tree node in the TreeView
                foreach (TreeNode child in parentNode.Nodes)
                {
                    if (child.Tag == closestPos)
                    {
                        EconomyTV.SelectedNode = child;
                        break;
                    }
                }

                //MessageBox.Show($"Selected closest node at X:{closestPos.x:0.##}, Z:{closestPos.z:0.##}");
            }
        }
        private void MapControl_EventSpawnDoubleclicked(object sender, MapClickEventArgs e)
        {
            if (_selectedEventPos == null) return;

            _selectedEventPos.x = (decimal)e.MapCoordinates.X;
            _selectedEventPos.z = (decimal)e.MapCoordinates.Y;

            _mapControl.ClearDrawables();

            eventposdefEvent defevent = currentTreeNode.Parent.Parent.Tag as eventposdefEvent;
            ShowHandler<IUIHandler>(new eventgroupsspawnpositionControl(), typeof(cfgeventspawnsConfig), _selectedEventPos, new List<TreeNode>() { currentTreeNode });
            DrawEventSpawns(defevent);
            currentTreeNode.Text = _selectedEventPos.ToString();
        }
        private void MapControl_EffectSafePositionsSingleclicked(object sender, MapClickEventArgs e)
        {
            if (currentTreeNode?.Parent == null)
                return;

            TreeNode parentNode = currentTreeNode.Parent;

            cfgeffectareaSafePosition closestPos = null;
            double closestDistance = double.MaxValue;

            PointF clickScreen = _mapControl.MapToScreen(e.MapCoordinates);

            // Loop through all child nodes of the parent
            foreach (TreeNode child in parentNode.Nodes)
            {
                if (child.Tag is cfgeffectareaSafePosition pos)
                {
                    // Node position in screen space
                    PointF posScreen = _mapControl.MapToScreen(new PointF((float)pos.X, (float)pos.Z));

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
            if (closestPos != null && closestDistance < 10.0) // 10 units tolerance
            {
                // Select that tree node in the TreeView
                foreach (TreeNode child in parentNode.Nodes)
                {
                    if (child.Tag == closestPos)
                    {
                        EconomyTV.SelectedNode = child;
                        break;
                    }
                }

                //MessageBox.Show($"Selected closest node at X:{closestPos.x:0.##}, Z:{closestPos.z:0.##}");
            }
        }
        private void MapControl_EffectSafePositionsDoubleclicked(object sender, MapClickEventArgs e)
        {
            if (_selectedSafePosition == null) return;

            _selectedSafePosition.X = (decimal)e.MapCoordinates.X;
            _selectedSafePosition.Z = (decimal)e.MapCoordinates.Y;
            

            _mapControl.ClearDrawables();

            CfgeffectareaConfig cfgeffectareaConfig = currentTreeNode.FindParentOfType<CfgeffectareaConfig>();
            DrawEffectSafePositions(cfgeffectareaConfig);
            currentTreeNode.Text = $"Position {currentTreeNode.Index + 1} ({_selectedSafePosition.X}, {_selectedSafePosition.Z})";
        }
        private void MapControl_EffectAreaSingleclicked(object sender, MapClickEventArgs e)
        {
            if (currentTreeNode?.Parent == null)
                return;

            TreeNode parentNode = currentTreeNode.Parent;

            Areas closestPos = null;
            double closestDistance = double.MaxValue;

            PointF clickScreen = _mapControl.MapToScreen(e.MapCoordinates);

            // Loop through all child nodes of the parent
            foreach (TreeNode child in parentNode.Nodes)
            {
                if (child.Tag is Areas area)
                {
                    // Node position in screen space
                    PointF posScreen = _mapControl.MapToScreen(new PointF((float)area.Data.Pos[0], (float)area.Data.Pos[2]));

                    double dx = clickScreen.X - posScreen.X;
                    double dy = clickScreen.Y - posScreen.Y;
                    double distance = Math.Sqrt(dx * dx + dy * dy);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestPos = area;
                    }
                }
            }

            // Optional: choose only if within some "click radius"
            if (closestPos != null && closestDistance < 10.0) // 10 units tolerance
            {
                // Select that tree node in the TreeView
                foreach (TreeNode child in parentNode.Nodes)
                {
                    if (child.Tag == closestPos)
                    {
                        EconomyTV.SelectedNode = child;
                        break;
                    }
                }

                //MessageBox.Show($"Selected closest node at X:{closestPos.x:0.##}, Z:{closestPos.z:0.##}");
            }
        }
        private void MapControl_EffectAreaDoubleclicked(object sender, MapClickEventArgs e)
        {
            if (_selectedeffectarea == null) return;

            _selectedeffectarea.Data.Pos[0] = (decimal)e.MapCoordinates.X;
            _selectedeffectarea.Data.Pos[2] = (decimal)e.MapCoordinates.Y;
            

            _mapControl.ClearDrawables();

            CfgeffectareaConfig cfgeffectareaConfig = currentTreeNode.FindParentOfType<CfgeffectareaConfig>();
            DrawEffectEffectArea(cfgeffectareaConfig);
        }
        private void MapControl_EffectPRASafePositionsSingleclicked(object sender, MapClickEventArgs e)
        {
            if (currentTreeNode?.Parent == null)
                return;

            TreeNode parentNode = currentTreeNode.Parent;

            PRASafePosition closestPos = null;
            double closestDistance = double.MaxValue;

            PointF clickScreen = _mapControl.MapToScreen(e.MapCoordinates);

            // Loop through all child nodes of the parent
            foreach (TreeNode child in parentNode.Nodes)
            {
                if (child.Tag is PRASafePosition safeposition)
                {
                    // Node position in screen space
                    PointF posScreen = _mapControl.MapToScreen(new PointF(safeposition.Position.X, safeposition.Position.Z));

                    double dx = clickScreen.X - posScreen.X;
                    double dy = clickScreen.Y - posScreen.Y;
                    double distance = Math.Sqrt(dx * dx + dy * dy);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestPos = safeposition;
                    }
                }
            }

            // Optional: choose only if within some "click radius"
            if (closestPos != null && closestDistance < 10.0) // 10 units tolerance
            {
                // Select that tree node in the TreeView
                foreach (TreeNode child in parentNode.Nodes)
                {
                    if (child.Tag == closestPos)
                    {
                        EconomyTV.SelectedNode = child;
                        break;
                    }
                }

                //MessageBox.Show($"Selected closest node at X:{closestPos.x:0.##}, Z:{closestPos.z:0.##}");
            }
        }
        private void MapControl_EffectPRASafePositionsDoubleclicked(object sender, MapClickEventArgs e)
        {
            if (_selectedRPASafePosition == null) return;

            _selectedRPASafePosition.Position.X = e.MapCoordinates.X;
            _selectedRPASafePosition.Position.Z = e.MapCoordinates.Y;

            _mapControl.ClearDrawables();

            PlayerRestrictedFile PlayerRestrictedFiles = currentTreeNode.FindParentOfType<PlayerRestrictedFile>();
            DrawEffectPRASafePositions(PlayerRestrictedFiles);
            currentTreeNode.Text = $"Position {currentTreeNode.Index + 1}: [{string.Join(", ", _selectedRPASafePosition)}]";
        }
        private void MapControl_EffectPRABoxSingleclicked(object sender, MapClickEventArgs e)
        {
            if (currentTreeNode?.Parent == null)
                return;

            TreeNode parentNode = currentTreeNode.Parent;

            PRABoxes closestPos = null;
            double closestDistance = double.MaxValue;

            PointF clickScreen = _mapControl.MapToScreen(e.MapCoordinates);

            // Loop through all child nodes of the parent
            foreach (TreeNode child in parentNode.Nodes)
            {
                if (child.Tag is PRABoxes box)
                {
                    // Node position in screen space
                    PointF posScreen = _mapControl.MapToScreen(new PointF(box.Position.X, box.Position.Z));

                    double dx = clickScreen.X - posScreen.X;
                    double dy = clickScreen.Y - posScreen.Y;
                    double distance = Math.Sqrt(dx * dx + dy * dy);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestPos = box;
                    }
                }
            }

            // Optional: choose only if within some "click radius"
            if (closestPos != null && closestDistance < 10.0) // 10 units tolerance
            {
                // Select that tree node in the TreeView
                foreach (TreeNode child in parentNode.Nodes)
                {
                    if (child.Tag == closestPos)
                    {
                        EconomyTV.SelectedNode = child;
                        break;
                    }
                }

                //MessageBox.Show($"Selected closest node at X:{closestPos.x:0.##}, Z:{closestPos.z:0.##}");
            }
        }
        private void MapControl_EffectPRABoxDoubleclicked(object sender, MapClickEventArgs e)
        {
            if (_selectedRPABox == null) return;

            _selectedRPABox.Position.X = e.MapCoordinates.X;
            _selectedRPABox.Position.Z = e.MapCoordinates.Y;

            _mapControl.ClearDrawables();

            PlayerRestrictedFile PlayerRestrictedFiles = currentTreeNode.FindParentOfType<PlayerRestrictedFile>();
            DrawPRABoxesPOsitions(PlayerRestrictedFiles);
        }
        private void MapControl_PlayerSpawnSingleclicked(object? sender, MapClickEventArgs e)
        {
            if (currentTreeNode?.Parent == null)
                return;


            playerspawnpointsGroupPos closestPos = null;
            double closestDistance = double.MaxValue;

            PointF clickScreen = _mapControl.MapToScreen(e.MapCoordinates);

            if (currentTreeNode.Parent.Tag is playerspawnpointsGroup)
            {
                TreeNode parentNode = currentTreeNode.Parent.Parent;
                foreach (TreeNode childp in parentNode.Nodes)
                {
                    foreach (TreeNode child in childp.Nodes)
                    {
                        if (child.Tag is playerspawnpointsGroupPos pos)
                        {
                            // Node position in screen space
                            PointF posScreen = _mapControl.MapToScreen(new PointF((float)pos.x, (float)pos.z));

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
                if (closestPos != null && closestDistance < 10.0) // 10 units tolerance
                {
                    // Select that tree node in the TreeView
                    foreach (TreeNode childp in parentNode.Nodes)
                    {
                        foreach (TreeNode child in childp.Nodes)
                        {
                            if (child.Tag == closestPos)
                            {
                                EconomyTV.SelectedNode = child;
                                break;
                            }
                        }
                    }

                    //MessageBox.Show($"Selected closest node at X:{closestPos.x:0.##}, Z:{closestPos.z:0.##}");
                }
            }
            else
            {
                TreeNode parentNode = currentTreeNode.Parent;
                // Loop through all child nodes of the parent
                foreach (TreeNode child in parentNode.Nodes)
                {
                    if (child.Tag is playerspawnpointsGroupPos pos)
                    {
                        // Node position in screen space
                        PointF posScreen = _mapControl.MapToScreen(new PointF((float)pos.x, (float)pos.z));

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
                if (closestPos != null && closestDistance < 10.0) // 10 units tolerance
                {
                    // Select that tree node in the TreeView
                    foreach (TreeNode child in parentNode.Nodes)
                    {
                        if (child.Tag == closestPos)
                        {
                            EconomyTV.SelectedNode = child;
                            break;
                        }
                    }

                    //MessageBox.Show($"Selected closest node at X:{closestPos.x:0.##}, Z:{closestPos.z:0.##}");
                }
            }
        }
        private void MapControl_PlayerSpawnDoubleclicked(object? sender, MapClickEventArgs e)
        {
            if (_selectedSpawnpointPosition == null) return;

            _selectedSpawnpointPosition.x = (decimal)e.MapCoordinates.X;
            _selectedSpawnpointPosition.z = (decimal)e.MapCoordinates.Y;

            _mapControl.ClearDrawables();

            cfgplayerspawnpointsConfig cfgplayerspawnpointsConfig = currentTreeNode.FindParentOfType<cfgplayerspawnpointsConfig>();
            playerspawnpointssection playerspawnpointssection = currentTreeNode.FindParentOfType<playerspawnpointssection>();
            DrawPlayerSpawnPointPositions(playerspawnpointssection);
            currentTreeNode.Text = _selectedSpawnpointPosition.ToString();
        }
        private void MapControl_MapGroupPosSingleclicked(object? sender, MapClickEventArgs e)
        {
            if (currentTreeNode?.Parent == null)
                return;


            mapGroup closestPos = null;
            double closestDistance = double.MaxValue;

            PointF clickScreen = _mapControl.MapToScreen(e.MapCoordinates);

            if (currentTreeNode.Tag is mapgroupposConfig)
            {
                foreach (TreeNode childp in currentTreeNode.Nodes)
                {
                    foreach (TreeNode child in childp.Nodes)
                    {
                        if (child.Tag is mapGroup pos)
                        {
                            // Node position in screen space
                            PointF posScreen = _mapControl.MapToScreen(new PointF(Convert.ToSingle(pos.pos.Split(' ')[0]), Convert.ToSingle(pos.pos.Split(' ')[2])));

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
                if (closestPos != null && closestDistance < 10.0) // 10 units tolerance
                {
                    // Select that tree node in the TreeView
                    foreach (TreeNode childp in currentTreeNode.Nodes)
                    {
                        foreach (TreeNode child in childp.Nodes)
                        {
                            if (child.Tag == closestPos)
                            {
                                EconomyTV.SelectedNode = child;
                                break;
                            }
                        }
                    }

                    //MessageBox.Show($"Selected closest node at X:{closestPos.x:0.##}, Z:{closestPos.z:0.##}");
                }
            }
            else if (currentTreeNode.Parent.Tag is mapgroupposConfig)
            {
                foreach (TreeNode childp in currentTreeNode.Parent.Nodes)
                {
                    foreach (TreeNode child in childp.Nodes)
                    {
                        if (child.Tag is mapGroup pos)
                        {
                            // Node position in screen space
                            PointF posScreen = _mapControl.MapToScreen(new PointF(Convert.ToSingle(pos.pos.Split(' ')[0]), Convert.ToSingle(pos.pos.Split(' ')[2])));

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
                if (closestPos != null && closestDistance < 10.0) // 10 units tolerance
                {
                    // Select that tree node in the TreeView
                    foreach (TreeNode childp in currentTreeNode.Parent.Nodes)
                    {
                        foreach (TreeNode child in childp.Nodes)
                        {
                            if (child.Tag == closestPos)
                            {
                                EconomyTV.SelectedNode = child;
                                break;
                            }
                        }
                    }

                    //MessageBox.Show($"Selected closest node at X:{closestPos.x:0.##}, Z:{closestPos.z:0.##}");
                }
            }
            else
            {
                TreeNode parentNode = currentTreeNode.Parent.Parent;
                // Loop through all child nodes of the parent
                foreach (TreeNode childp in parentNode.Nodes)
                {
                    foreach (TreeNode child in childp.Nodes)
                    {
                        if (child.Tag is mapGroup pos)
                        {
                            // Node position in screen space
                            PointF posScreen = _mapControl.MapToScreen(new PointF(Convert.ToSingle(pos.pos.Split(' ')[0]), Convert.ToSingle(pos.pos.Split(' ')[2])));

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
                if (closestPos != null && closestDistance < 10.0) // 10 units tolerance
                {
                    // Select that tree node in the TreeView
                    foreach (TreeNode childp in parentNode.Nodes)
                    {
                        foreach (TreeNode child in childp.Nodes)
                        {
                            if (child.Tag == closestPos)
                            {
                                EconomyTV.SelectedNode = child;
                                break;
                            }
                        }
                    }

                    //MessageBox.Show($"Selected closest node at X:{closestPos.x:0.##}, Z:{closestPos.z:0.##}");
                }
            }
        }
        private void MapControl_MapGroupPosDoubleclicked(object? sender, MapClickEventArgs e)
        {
            if (_selectedSpawnpointPosition == null) return;

            _selectedSpawnpointPosition.x = (decimal)e.MapCoordinates.X;
            _selectedSpawnpointPosition.z = (decimal)e.MapCoordinates.Y;

            _mapControl.ClearDrawables();

            cfgplayerspawnpointsConfig cfgplayerspawnpointsConfig = currentTreeNode.FindParentOfType<cfgplayerspawnpointsConfig>();
            playerspawnpointssection playerspawnpointssection = currentTreeNode.FindParentOfType<playerspawnpointssection>();
            DrawPlayerSpawnPointPositions(playerspawnpointssection);
            currentTreeNode.Text = _selectedSpawnpointPosition.ToString();
        }
        private void MapControl_TerritoriesSingleclicked(object? sender, MapClickEventArgs e)
        {
            if (currentTreeNode?.Parent == null)
                return;


            territorytypeTerritoryZone closestPos = null;
            double closestDistance = double.MaxValue;

            PointF clickScreen = _mapControl.MapToScreen(e.MapCoordinates);

            if (currentTreeNode.Tag is territorytypeTerritoryZone)
            {
                foreach (TreeNode childp in currentTreeNode.Parent.Parent.Nodes)
                {
                    foreach (TreeNode child in childp.Nodes)
                    {
                        if (child.Tag is territorytypeTerritoryZone pos)
                        {
                            // Node position in screen space
                            PointF posScreen = _mapControl.MapToScreen(new PointF((float)pos.x, (float)pos.z));

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
                if (closestPos != null && closestDistance < (double)closestPos.r) // 10 units tolerance
                {
                    foreach (TreeNode childp in currentTreeNode.Parent.Parent.Nodes)
                    {
                        foreach (TreeNode child in childp.Nodes)
                        {
                            if (child.Tag == closestPos)
                            {
                                EconomyTV.SelectedNode = child;
                                break;
                            }
                        }
                    }

                    //MessageBox.Show($"Selected closest node at X:{closestPos.x:0.##}, Z:{closestPos.z:0.##}");
                }
            }
        }
        private void MapControl_TerritoriesDoubleclicked(object? sender, MapClickEventArgs e)
        {
            if (_selectedterritory == null) return;

            _selectedterritory.x = (decimal)e.MapCoordinates.X;
            _selectedterritory.z = (decimal)e.MapCoordinates.Y;

            _mapControl.ClearDrawables();

            territorytype territorytype = currentTreeNode.FindParentOfType<territorytype>();
            territorytype.IsDirty = true;
            territorytypeTerritory territorytypeTerritory = currentTreeNode.FindParentOfType<territorytypeTerritory>();
            DrawTerritoriesPositions(territorytypeTerritory);
            currentTreeNode.Text = _selectedterritory.ToString();
        }

        /// <summary>
        /// Handle editing Economy values
        /// </summary>
        /// <param name="vanillaFile"></param>
        /// <param name="section"></param>
        /// <param name="sectionName"></param>
        private void HandleVanillaeconomyEditRedirect(EconomyFile vanillaFile, EconomySection section, string sectionName)
        {
            string newmodPath = "db";
            string newPath = EnsureModFolderAndGetPath(newmodPath, "Custom_" + vanillaFile.FileName);

            var existingFile = _economyManager.economyConfig.MutableItems
                .FirstOrDefault(f => f.FilePath.Equals(newPath, StringComparison.OrdinalIgnoreCase));

            EconomyFile newFile = existingFile ?? new EconomyFile(newPath)
            {
                IsModded = true,
                FileType = "economy",
                ModFolder = newmodPath,
                IsDirty = true,
                Data = new economy()
            };

            if (existingFile == null)
            {
                _economyManager.eonomyCoreConfig.AddCe(newFile.ModFolder, newFile.FileName, "economy");
                _economyManager.eonomyCoreConfig.Save();
                _economyManager.economyConfig.MutableItems.Add(newFile);
            }

            if (GetSectionByName(newFile.Data, sectionName) == null)
            {
                var cloned = CloneSection(section);
                switch (sectionName)
                {
                    case "Dynamic": newFile.Data.dynamic = cloned; break;
                    case "Animals": newFile.Data.animals = cloned; break;
                    case "Zombies": newFile.Data.zombies = cloned; break;
                    case "Vehicles": newFile.Data.vehicles = cloned; break;
                    case "Randoms": newFile.Data.randoms = cloned; break;
                    case "Custom": newFile.Data.custom = cloned; break;
                    case "Building": newFile.Data.building = cloned; break;
                    case "Player": newFile.Data.player = cloned; break;
                }

                newFile.IsDirty = true;
            }
            _economyManager.economyConfig.Save();
            HandleTreeViewSelection(newFile, sectionName, f => GetSectionByName(f.Data, sectionName), EconomyTV);
        }
        private EconomySection CloneSection(EconomySection original)
        {
            return new EconomySection
            {
                init = original.init,
                load = original.load,
                respawn = original.respawn,
                save = original.save
            };
        }
        private EconomySection GetSectionByName(economy data, string name)
        {
            return name switch
            {
                "Dynamic" => data.dynamic,
                "Animals" => data.animals,
                "Zombies" => data.zombies,
                "Vehicles" => data.vehicles,
                "Randoms" => data.randoms,
                "Custom" => data.custom,
                "Building" => data.building,
                "Player" => data.player,
                _ => null
            };
        }
        /// <summary>
        /// Handle editing globals Variables
        /// </summary>
        /// <param name="vanillaFile"></param>
        /// <param name="section"></param>
        /// <param name="sectionName"></param>
        private void HandleVanillaglobalsEditRedirect(GlobalsFile vanillaFile, variablesVar section, string sectionName)
        {
            string newmodPath = "db";
            string newPath = EnsureModFolderAndGetPath(newmodPath, "Custom_" + vanillaFile.FileName);

            // 2. Create new economyFile and add only selected section
            var existingFile = _economyManager.globalsConfig.MutableItems
                .FirstOrDefault(f => f.FilePath.Equals(newPath, StringComparison.OrdinalIgnoreCase));

            GlobalsFile newFile = existingFile ?? new GlobalsFile(newPath)
            {
                IsModded = true,
                FileType = "globals",
                ModFolder = newmodPath,
                IsDirty = true,
                Data = new variables { var = new System.ComponentModel.BindingList<variablesVar>() }
            };

            if (existingFile == null)
            {
                _economyManager.eonomyCoreConfig.AddCe(newFile.ModFolder, newFile.FileName, "globals");
                _economyManager.eonomyCoreConfig.Save();
                _economyManager.globalsConfig.MutableItems.Add(newFile);
            }

            if (GetVariableByName(newFile.Data, sectionName) == null)
            {
                newFile.Data.var.Add(CloneVariable(section));
                newFile.IsDirty = true;
            }

            _economyManager.globalsConfig.Save();
            HandleTreeViewSelection(newFile, sectionName, f => GetVariableByName(f.Data, sectionName), EconomyTV);
        }
        private variablesVar CloneVariable(variablesVar original)
        {
            return new variablesVar
            {
                name = original.name,
                type = original.type,
                value = original.value
            };
        }
        private variablesVar GetVariableByName(variables data, string name)
        {
            if (data?.var == null)
                return null;

            return data.var.FirstOrDefault(v => v.name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
        /// <summary>
        /// Economy and Globals Helpers
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string EnsureModFolderAndGetPath(string modPath, string fileName)
        {
            string modFolder = Path.Combine(_projectManager.CurrentProject.ProjectRoot, "mpmissions", _projectManager.CurrentProject.MpMissionPath, modPath);
            Directory.CreateDirectory(modFolder);
            return Path.Combine(modFolder, fileName);
        }

        /// <summary>
        /// Types Right click methods
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addNewTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTreeNode.Tag is TypesFile typefile)
            {
                if (!typefile.IsModded)
                {
                    var result = MessageBox.Show(
                                $"This is the Vanilla types file, I suggest you add new types to a custom type file......\n\nIf you dont have any custom types yet you can create one by right clicking on {Path.GetFileName(_economyManager.basePath)} and selecting add new types.",
                                "Vanilla Types File",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Question
                            );
                    if (result == DialogResult.OK) { return; }
                }
                AddTypes frm = new AddTypes();
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.typesname = typefile.FileName;
                frm.moddir = typefile.ModFolder;
                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    foreach (TypeEntry te in frm._entries)
                    {
                        if (typefile.Data.TypeList.Any(x => x.Name == te.Name))
                            continue;
                        typefile.Data.TypeList.Add(te);
                        CreateTyoesNodes(currentTreeNode, te);
                    }
                    typefile.IsDirty = true;
                    savefiles();
                }
            }
            else
            {
                AddTypes frm = new AddTypes();
                frm.StartPosition = FormStartPosition.CenterParent;
                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    string newmodPath = frm.moddir.Replace("/", "\\");
                    string typesfile = frm.typesname + "_ce_types.xml";
                    string newPath = EnsureModFolderAndGetPath(newmodPath, typesfile);

                    BindingList<TypeEntry> types = frm._entries;
                    TypesFile newtypesfile = new TypesFile(newPath)
                    {
                        FileType = "types",
                        IsModded = true,
                        ModFolder = newmodPath,
                        Data = new Types()
                    };
                    newtypesfile.Data.TypeList = types;
                    _economyManager.eonomyCoreConfig.AddCe(newtypesfile.ModFolder, newtypesfile.FileName, "types");
                    _economyManager.TypesConfig.MutableItems.Add(newtypesfile);
                    AddFileToDBTree(newtypesfile);
                    savefiles();
                }
            }
        }
        private void removeSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTreeNode.Tag is TypesFile typefile)
            {
                if (typefile.IsModded == false)
                {
                    var result = MessageBox.Show(
                                $"Type TypeFile is in the vanilla types file, You cant delete it......",
                                "Vanilla Types File",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Question
                            );
                    if (result == DialogResult.OK) { return; }
                }
                else if (typefile.IsModded == true)
                {
                    var result = MessageBox.Show(
                               $"Are you sure you want to delete this full Types file?",
                               "Modded Types File",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question
                           );
                    if (result == DialogResult.No) { return; }
                }
                bool deleteDirectory;
                string folderPathRel;
                string fileName;

                _economyManager.eonomyCoreConfig.RemoveCe(
                    typefile.FileName,
                    out folderPathRel,
                    out fileName,
                    out deleteDirectory
                );
                RemoveTreeNodeAndEmptyParents(currentTreeNode);
                typefile.IsDirty = true;
                typefile.ToDelete = true;
            }
            else if (currentTreeNode.Tag is TypeEntry typeentry)
            {
                TypesFile _typefile = currentTreeNode.Parent.Parent.Tag as TypesFile;
                if (_typefile.IsModded == false)
                {
                    var result = MessageBox.Show(
                                $"Type entry(s) is in the vanilla types file, are you sure you want to delete it?",
                                "Vanilla Types File",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question
                            );
                    if (result == DialogResult.No) { return; }
                }
                var selectedNodes = EconomyTV.SelectedNodes.Cast<TreeNode>().ToList();
                foreach (var node in selectedNodes)
                {
                    TypeEntry typeeentry = node.Tag as TypeEntry;
                    _typefile.Data.TypeList.Remove(typeeentry);
                    var parent = node.Parent;
                    RemoveTreeNodeAndEmptyParents(node);
                }
                _typefile.IsDirty = true;
            }
        }
        private void updateTypesFromXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTreeNode.Tag is TypesFile typefile)
            {
                AddTypes frm = new AddTypes();
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.typesname = typefile.FileName;
                frm.moddir = typefile.ModFolder;
                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    List<string> added = new List<string>();

                    foreach (TypeEntry te in frm._entries)
                    {
                        if (typefile.Data.TypeList.Any(x => x.Name == te.Name))
                            continue;
                        typefile.Data.TypeList.Add(te);
                        typefile.IsDirty = true;
                        added.Add(te.Name);
                        Console.WriteLine($"\t{te.Name} added to file....");
                    }


                    string relativePath = Path.GetRelativePath(_economyManager.basePath, typefile.FilePath);
                    EconomyTV.Nodes.Remove(currentTreeNode);
                    AddFileToTree(EconomyTV.Nodes[0], relativePath, typefile, CreateTypesfileNodes);
                    savefiles();

                    if (added.Count > 0)
                    {
                        MessageBox.Show(
                            "Added entries:\n\n" + string.Join("\n", added),
                            "Import Complete",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    else
                    {
                        MessageBox.Show(
                            "No new entries were added (all items already existed).",
                            "Import Complete",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                }
            }
        }

        /// <summary>
        /// Event Right Click Methods
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importPositionFromdzeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eventposdefEvent eventposdefEvent = currentTreeNode.Tag as eventposdefEvent;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Import Positions";
            openFileDialog.Filter = "Expansion Map|*.map|Object Spawner|*.json|DayZ Editor|*.dze";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                TreeNode eventposnodes = null;
                if (eventposdefEvent.pos == null || eventposdefEvent.pos.Count == 0)
                {
                    eventposdefEvent.pos = new BindingList<eventposdefEventPos>();
                    if (!EconomyTV.SelectedNode.Nodes.ContainsKey("POS"))
                    {
                        eventposnodes = new TreeNode("pos");
                        eventposnodes.Name = "POS";
                        eventposnodes.Tag = "PosParent";
                    }
                    else
                    {
                        eventposnodes = EconomyTV.SelectedNode.Nodes.Find("POS", false)[0];
                    }
                }
                else
                {
                    eventposnodes = EconomyTV.SelectedNode.Nodes.Find("POS", false)[0];
                    DialogResult dialogResult = MessageBox.Show("Clear Exisitng Positions?", "Clear position", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        eventposdefEvent.pos = new BindingList<eventposdefEventPos>();
                        eventposnodes.Nodes.Clear();

                    }
                    eventposnodes.Remove();
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
                            string[] YPR = linesplit[2].Split(' ');
                            eventposdefEventPos newpos = new eventposdefEventPos()
                            {
                                x = Convert.ToDecimal(XYZ[0]),
                                ySpecified = true,
                                y = Convert.ToDecimal(XYZ[1]),
                                z = Convert.ToDecimal(XYZ[2]),
                                aSpecified = true,
                                a = Convert.ToDecimal(YPR[0]),
                                group = null

                            };
                            if (newpos.a < 0)
                            {
                                while (newpos.a < 0)
                                {
                                    newpos.a += 360;
                                }
                            }
                            else if (newpos.a >= 360)
                            {
                                while (newpos.a >= 360)
                                {
                                    newpos.a -= 360;
                                }
                            }
                            eventposdefEvent.pos.Add(newpos);
                        }
                        break;
                    case 2:
                        ObjectSpawnerArrData newobjectspawner = JsonSerializer.Deserialize<ObjectSpawnerArrData>(File.ReadAllText(filePath));
                        foreach (SpawnObjects so in newobjectspawner.Objects)
                        {
                            eventposdefEventPos newpos = new eventposdefEventPos()
                            {
                                x = Convert.ToDecimal(so.pos[0]),
                                ySpecified = true,
                                y = Convert.ToDecimal(so.pos[1]),
                                z = Convert.ToDecimal(so.pos[2]),
                                aSpecified = true,
                                a = Convert.ToDecimal(so.ypr[0]),
                                group = null

                            };
                            if (newpos.a < 0)
                            {
                                while (newpos.a < 0)
                                {
                                    newpos.a += 360;
                                }
                            }
                            else if (newpos.a >= 360)
                            {
                                while (newpos.a >= 360)
                                {
                                    newpos.a -= 360;
                                }
                            }
                            eventposdefEvent.pos.Add(newpos);
                        }
                        break;
                    case 3:
                        DZE importfile = DZEHelpers.LoadFile(filePath);
                        foreach (Editorobject eo in importfile.EditorObjects)
                        {
                            eventposdefEventPos newpos = new eventposdefEventPos()
                            {
                                x = Convert.ToDecimal(eo.Position[0]),
                                ySpecified = true,
                                y = Convert.ToDecimal(eo.Position[1]),
                                z = Convert.ToDecimal(eo.Position[2]),
                                aSpecified = true,
                                a = Convert.ToDecimal(eo.Orientation[0]),
                                group = null

                            };
                            if (newpos.a < 0)
                            {
                                while (newpos.a < 0)
                                {
                                    newpos.a += 360;
                                }
                            }
                            else if (newpos.a >= 360)
                            {
                                while (newpos.a >= 360)
                                {
                                    newpos.a -= 360;
                                }
                            }
                            eventposdefEvent.pos.Add(newpos);
                        }
                        break;
                }
                foreach (eventposdefEventPos pos in eventposdefEvent.pos)
                {
                    TreeNode posnodes = new TreeNode(pos.ToString());
                    posnodes.Tag = pos;
                    eventposnodes.Nodes.Add(posnodes);
                }
                EconomyTV.SelectedNode.Nodes.Add(eventposnodes);
            }
        }
        private void AddNewEventsToolstripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTreeNode.Tag is EventsFile eventfile)
            {
                if (!eventfile.IsModded)
                {
                    var result = MessageBox.Show(
                                $"This is the Vanilla Events file, I suggest you add new Events to a custom Event file......\n\nIf you dont have any custom Events files yet you can create one by right clicking on {Path.GetFileName(_economyManager.basePath)} and selecting add new events.",
                                "Vanilla Types File",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Question
                            );
                    if (result == DialogResult.OK) { return; }
                }
                else
                {
                    eventsEvent neweventEvent = new eventsEvent()
                    {
                        name = "NewEvent",
                        nominal = 0,
                        min = 0,
                        max = 0,
                        lifetime = 0,
                        restock = 0,
                        saferadius = 0,
                        distanceradius = 0,
                        cleanupradius = 0,
                        position = position.@fixed,
                        limit = limit.child,
                        active = 0,
                        flags = new eventsEventFlags(),
                        children = new BindingList<eventsEventChild>()
                    };
                    eventfile.Data.AddNewEvent(neweventEvent);
                    currentTreeNode.Nodes.Add(new TreeNode(neweventEvent.name)
                    {
                        Tag = neweventEvent
                    });
                }
            }
            else
            {
                AddEventFile frm = new AddEventFile();
                frm.StartPosition = FormStartPosition.CenterParent;
                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    string newmodPath = frm.moddir.Replace("/", "\\");
                    string typesfile = frm.typesname + "_ce_events.xml";
                    string newPath = EnsureModFolderAndGetPath(newmodPath, typesfile);

                    EventsFile newEventsfile = new EventsFile(newPath)
                    {
                        FileType = "events",
                        IsModded = true,
                        ModFolder = newmodPath,
                        Data = new events()
                    };
                    newEventsfile.Data.@event = frm._entries;
                    _economyManager.eonomyCoreConfig.AddCe(newEventsfile.ModFolder, newEventsfile.FileName, "events");
                    _economyManager.eventsConfig.MutableItems.Add(newEventsfile);
                    AddFileToDBTree(newEventsfile);
                    savefiles();
                }
            }
        }
        private void RemoveEventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTreeNode.Tag is EventsFile eventfile)
            {
                if (eventfile.IsModded == false)
                {
                    var result = MessageBox.Show(
                                $"this is in the vanilla Events file, You cant delete it......",
                                "Vanilla Event File",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Question
                            );
                    if (result == DialogResult.OK) { return; }
                }
                else if (eventfile.IsModded == true)
                {
                    var result = MessageBox.Show(
                               $"Are you sure you want to delete this full Events file?",
                               "Modded event File",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question
                           );
                    if (result == DialogResult.No) { return; }
                }
                //delete any eventspawns that go along with these events inside this file,
                //we will check to make sure that no other events are using the eventspawn file prior to removing it.
                var spawnresult = MessageBox.Show(
                               $"Do you want me to remove any associated event spawn entries if they are not associated with any other events?",
                               "remove events spawns",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question
                           );
                if (spawnresult == DialogResult.Yes)
                {
                    foreach (eventsEvent evev in eventfile.Data.@event)
                    {
                        eventposdefEvent evevpoints = _economyManager.cfgeventspawnsConfig.Findevent(evev.name);
                        if (evevpoints == null)
                            continue;
                        int count = 0;
                        foreach (EventsFile evfile in _economyManager.eventsConfig.MutableItems)
                        {
                            foreach (eventsEvent evev1 in evfile.Data.@event)
                            {
                                eventposdefEvent evevpoints1 = _economyManager.cfgeventspawnsConfig.Findevent(evev1.name);
                                if (evevpoints1 != null && evevpoints1.name == evevpoints.name)
                                    count++;
                            }
                        }
                        if (count == 1)
                        {
                            _economyManager.cfgeventspawnsConfig.Data.@event.Remove(evevpoints);
                        }
                    }
                }
                bool deleteDirectory;
                string folderPathRel;
                string fileName;

                _economyManager.eonomyCoreConfig.RemoveCe(
                    eventfile.FileName,
                    out folderPathRel,
                    out fileName,
                    out deleteDirectory
                );
                RemoveTreeNodeAndEmptyParents(currentTreeNode);
                eventfile.IsDirty = true;
                eventfile.ToDelete = true;
            }
            else if (currentTreeNode.Tag is eventsEvent _event)
            {
                EventsFile _eventfile = currentTreeNode.Parent.Tag as EventsFile;
                if (_eventfile.IsModded == false)
                {
                    var result = MessageBox.Show(
                                $"Event entry(s) is in the vanilla types file, are you sure you want to delete it?\n perhaps just disabling would be better....",
                                "Vanilla Events File",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question
                            );
                    if (result == DialogResult.No) { return; }
                }
                var selectedNodes = EconomyTV.SelectedNodes.Cast<TreeNode>().ToList();
                _eventfile.Data.@event.Remove(_event);
                var parent = currentTreeNode.Parent;
                eventposdefEvent points = _economyManager.cfgeventspawnsConfig.Findevent(_event.name);
                if (points != null)
                {
                    int count = 0;
                    foreach (EventsFile evfile in _economyManager.eventsConfig.MutableItems)
                    {
                        foreach (eventsEvent evev in evfile.Data.@event)
                        {
                            eventposdefEvent evevpoints = _economyManager.cfgeventspawnsConfig.Findevent(evev.name);
                            if (evevpoints != null && evevpoints.name == points.name)
                                count++;
                        }
                    }
                    if (count == 0)
                    {
                        var result = MessageBox.Show(
                                    $"i have found an associated event spawn, do yo uwant me to remove that as well?",
                                    "Event Spawn Found.",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question
                                );
                        if (result == DialogResult.Yes)
                        {
                            _economyManager.cfgeventspawnsConfig.Data.@event.Remove(points);
                        }
                    }
                }
                RemoveTreeNodeAndEmptyParents(currentTreeNode);
                _eventfile.IsDirty = true;
            }
        }
        private void addNewEventSpawnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void deleteSelectedEventSpawnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void exportPositionTodzeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void addNewPosirtionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eventposdefEventPos newpos = new eventposdefEventPos()
            {
                x = _projectManager.CurrentProject.MapSize / 2,
                z = _projectManager.CurrentProject.MapSize / 2,
            };
            eventposdefEvent eventposdefEvent = currentTreeNode.Tag as eventposdefEvent;
            if (eventposdefEvent.pos == null)
                eventposdefEvent.pos = new BindingList<eventposdefEventPos>();
            eventposdefEvent.pos.Add(newpos);
            TreeNode existing = currentTreeNode.Nodes["POS"];
            if (existing == null)
            {
                TreeNode eventposnodes = new TreeNode("pos")
                {
                    Name = "POS",
                    Tag = "PosParent"
                };
                existing = eventposnodes;
                currentTreeNode.Nodes.Add(eventposnodes);
            }
            existing.Nodes.Add(CreateEventPositionNode(newpos));
        }
        private void removeSelectedPositionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void removeAllPositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eventposdefEvent eventposdefEvent = currentTreeNode.Tag as eventposdefEvent;
            eventposdefEvent.pos = null;
            TreeNode existing = currentTreeNode.Nodes["POS"];
            existing.Remove();
        }
        private void exportGroupSpawnTodzeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void importPositionAndCreateEventgroupFormdzeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Random Preset Right click methods
        /// </summary>
        private void addNewRandomPresetFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEventFile frm = new AddEventFile();
            frm.SetTitle = "Add new Random Preset File";
            frm.Button4visable = false;
            frm.StartPosition = FormStartPosition.CenterParent;
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string newmodPath = frm.moddir.Replace("/", "\\");
                string typesfile = frm.typesname + "_ce_cfgrandompresets.xml";
                string newPath = EnsureModFolderAndGetPath(newmodPath, typesfile);

                CfgrandompresetsFile newpresetfile = new CfgrandompresetsFile(newPath)
                {
                    FileType = "randompresets",
                    IsModded = true,
                    ModFolder = newmodPath,
                    Data = new randompresets()
                };
                _economyManager.eonomyCoreConfig.AddCe(newpresetfile.ModFolder, newpresetfile.FileName, "randompresets");
                _economyManager.cfgrandompresetsConfig.MutableItems.Add(newpresetfile);
                AddFileToDBTree(newpresetfile);
                savefiles();
            }
        }
        private void addNewAttchementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CfgrandompresetsFile rpf = currentTreeNode.FindParentOfType<CfgrandompresetsFile>();
            if (!rpf.IsModded)
            {
                var ismoddedresult = MessageBox.Show(
                                $"This is the Vanilla Random Preset file, I suggest you add new Attchemnts to a custom Random Preset file......\n\nIf you dont have any custom Random Presets yet you can create one by right clicking on {Path.GetFileName(_economyManager.basePath)} and selecting add new Random Preset.",
                                "Vanilla Random Presets File",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Question
                            );
                if (ismoddedresult == DialogResult.OK) { return; }
            }
            randompresetsAttachments newattachment = new randompresetsAttachments()
            {
                name = "New Attachment, Change Me!!!!",
                chance = (decimal)1.0,
                item = new BindingList<randompresetsItem>()
            };
            TreeNode IN = new TreeNode(GetpresetString(newattachment))
            {
                Tag = newattachment
            };

            rpf.Data.Items.Add(newattachment);
            currentTreeNode.Nodes.Add(IN);
            EconomyTV.SelectedNode = currentTreeNode.LastNode;
        }
        private void addNewCargoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CfgrandompresetsFile rpf = currentTreeNode.FindParentOfType<CfgrandompresetsFile>();
            if (rpf != null)
            {
                if (!rpf.IsModded)
                {
                    var ismoddedresult = MessageBox.Show(
                                    $"This is the Vanilla Random Preset file, I suggest you add new Cargo items to a custom Random Preset file......\n\nIf you dont have any custom Random Presets yet you can create one by right clicking on {Path.GetFileName(_economyManager.basePath)} and selecting add new Random Preset.",
                                    "Vanilla Random Presets File",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Question
                                );
                    if (ismoddedresult == DialogResult.OK) { return; }
                }
                randompresetsCargo newcargo = new randompresetsCargo()
                {
                    name = "New Cargo Change Me!!!!",
                    chance = (decimal)1.0,
                    item = new BindingList<randompresetsItem>()
                };
                TreeNode IN = new TreeNode(GetpresetString(newcargo))
                {
                    Tag = newcargo
                };

                rpf.Data.Items.Add(newcargo);
                currentTreeNode.Nodes.Add(IN);
                EconomyTV.SelectedNode = currentTreeNode.LastNode;
                rpf.IsDirty = true;
            }
        }
        private void addNewItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CfgrandompresetsFile rpf = currentTreeNode.FindParentOfType<CfgrandompresetsFile>();
            AddItemfromTypes form = new AddItemfromTypes();
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    randompresetsItem newitem = new randompresetsItem()
                    {
                        name = l,
                        chance = (decimal)1.0
                    };
                    if (currentTreeNode.Tag is randompresetsAttachments attachments)
                    {
                        attachments.item.Add(newitem);
                    }
                    else if (currentTreeNode.Tag is randompresetsCargo cargo)
                    {
                        cargo.item.Add(newitem);
                    }
                    currentTreeNode.Nodes.Add(CreateRPItem(newitem));
                }
                EconomyTV.SelectedNode = currentTreeNode.LastNode;

                rpf.IsDirty = true;
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }
        private void removeSelectedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (currentTreeNode.Tag is CfgrandompresetsFile randompresetsfile)
            {
                if (randompresetsfile.IsModded == false)
                {
                    var result = MessageBox.Show(
                                $"This Random Preset file is in the vanilla file, You cant delete it......",
                                "Vanilla Random Preset File",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Question
                            );
                    if (result == DialogResult.OK) { return; }
                }
                else if (randompresetsfile.IsModded == true)
                {
                    var result = MessageBox.Show(
                               $"Are you sure you want to delete this full Random Preset file?",
                               "Modded Random Preset File",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question
                           );
                    if (result == DialogResult.No) { return; }
                }
                bool deleteDirectory;
                string folderPathRel;
                string fileName;

                _economyManager.eonomyCoreConfig.RemoveCe(
                    randompresetsfile.FileName,
                    out folderPathRel,
                    out fileName,
                    out deleteDirectory
                );
                currentTreeNode.Remove();
                randompresetsfile.IsDirty = true;
                randompresetsfile.ToDelete = true;
            }
            else if (currentTreeNode.Tag is randompresetsAttachments randompresetsAttachments)
            {
                CfgrandompresetsFile _randompresetsfile = currentTreeNode.FindParentOfType<CfgrandompresetsFile>();
                if (_randompresetsfile.IsModded == false)
                {
                    var result = MessageBox.Show(
                                $"Preset Attchment is in the vanilla Random Preset file, are you sure you want to delete it?",
                                "Vanilla Random Preset File",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question
                            );
                    if (result == DialogResult.No) { return; }
                }
                _randompresetsfile.Data.Items.Remove(randompresetsAttachments);
                var parent = currentTreeNode.Parent;
                currentTreeNode.Remove();
                _randompresetsfile.IsDirty = true;
            }
            else if (currentTreeNode.Tag is randompresetsCargo randompresetsCargo)
            {
                CfgrandompresetsFile _randompresetsfile = currentTreeNode.FindParentOfType<CfgrandompresetsFile>();
                if (_randompresetsfile.IsModded == false)
                {
                    var result = MessageBox.Show(
                                $"Preset Cargo is in the vanilla Random Preset file, are you sure you want to delete it?",
                                "Vanilla Random Preset File",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question
                            );
                    if (result == DialogResult.No) { return; }
                }
                _randompresetsfile.Data.Items.Remove(randompresetsCargo);
                var parent = currentTreeNode.Parent;
                currentTreeNode.Remove();
                _randompresetsfile.IsDirty = true;
            }
            else if (currentTreeNode.Tag is randompresetsItem randompresetsItem)
            {
                CfgrandompresetsFile _randompresetsfile = currentTreeNode.FindParentOfType<CfgrandompresetsFile>();
                if (_randompresetsfile.IsModded == false)
                {
                    var result = MessageBox.Show(
                                $"Preset Item is in the vanilla Random Preset file, are you sure you want to delete it?",
                                "Vanilla Random Preset File",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question
                            );
                    if (result == DialogResult.No) { return; }
                }
                if (currentTreeNode.Parent.Tag is randompresetsAttachments currentattchment)
                {
                    currentattchment.item.Remove(randompresetsItem);
                }
                else if (currentTreeNode.Parent.Tag is randompresetsCargo currentcargo)
                {
                    currentcargo.item.Remove(randompresetsItem);
                }
                currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
                _randompresetsfile.IsDirty = true;
            }

        }

        /// <summary>
        /// Spawnable types right click methods
        /// </summary>
        private void addNewSpawnableTypesFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEventFile frm = new AddEventFile();
            frm.SetTitle = "Add new Spawnable Types";
            frm.Button4visable = false;
            frm.StartPosition = FormStartPosition.CenterParent;
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string newmodPath = frm.moddir.Replace("/", "\\");
                string typesfile = frm.typesname + "_ce_cfgspawnabletypes.xml";
                string newPath = EnsureModFolderAndGetPath(newmodPath, typesfile);

                CfgSpawnableTypesFile newpresetfile = new CfgSpawnableTypesFile(newPath)
                {
                    FileType = "spawnabletypes",
                    IsModded = true,
                    ModFolder = newmodPath,
                    Data = new SpawnableTypes()
                };
                _economyManager.eonomyCoreConfig.AddCe(newpresetfile.ModFolder, newpresetfile.FileName, "spawnabletypes");
                _economyManager.cfgspawnabletypesConfig.MutableItems.Add(newpresetfile);
                AddFileToDBTree(newpresetfile);
                savefiles();
            }
        }
        private void addNewSpawnableTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CfgSpawnableTypesFile spf = currentTreeNode.FindParentOfType<CfgSpawnableTypesFile>();
            if (!spf.IsModded)
            {
                var ismoddedresult = MessageBox.Show(
                                $"This is the Vanilla Spawnable Types file, I suggest you add new Spawnable Types to a custom SpawnableTypes file......\n\nIf you dont have any custom Random Presets yet you can create one by right clicking on {Path.GetFileName(_economyManager.basePath)} and selecting add new Spawnable Types.",
                                "Vanilla Spawnable Types File",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Question
                            );
                if (ismoddedresult == DialogResult.OK) { return; }
            }
            AddItemfromTypes form = new AddItemfromTypes();
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    SpawnableType newtype = new SpawnableType()
                    {
                        name = l,
                        Items = new BindingList<object>()
                    };
                    spf.Data.type.Add(newtype);
                    TreeNode IN = new TreeNode(newtype.name)
                    {
                        Tag = newtype
                    };
                    currentTreeNode.Nodes.Add(IN);
                }
                EconomyTV.SelectedNode = currentTreeNode.LastNode;
                spf.IsDirty = true;
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }
        private void addNewHoarderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CfgSpawnableTypesFile spf = currentTreeNode.FindParentOfType<CfgSpawnableTypesFile>();
            if (currentTreeNode.Tag is SpawnableType type)
            {
                spawnableTypesHoarder newhoarder = new spawnableTypesHoarder();
                type.Items.Add(newhoarder);
                currentTreeNode.Nodes.Add(new TreeNode("hoarder")
                {
                    Tag = newhoarder
                });
                spf.IsDirty = true;
            }
        }
        private void addNewTagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CfgSpawnableTypesFile spf = currentTreeNode.FindParentOfType<CfgSpawnableTypesFile>();
            if (currentTreeNode.Tag is SpawnableType type)
            {
                spawnableTypeTag newtag = new spawnableTypeTag();
                type.Items.Add(newtag);
                TreeNode newTagNode = new TreeNode(getTagString(newtag))
                {
                    Tag = newtag
                };
                currentTreeNode.Nodes.Add(newTagNode);
                spf.IsDirty = true;
            }
        }
        private void addNewDamageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CfgSpawnableTypesFile spf = currentTreeNode.FindParentOfType<CfgSpawnableTypesFile>();
            spawnableTypeDamage newdamage = new spawnableTypeDamage();
            if (currentTreeNode.Tag is SpawnableType type)
            {
                type.Items.Insert(0, newdamage);
            }
            else if (currentTreeNode.Tag is spawnableTypeCargo cargo)
            {
                cargo.damage = newdamage;
            }
            else if (currentTreeNode.Tag is spawnableTypeAttachment attachment)
            {
                attachment.damage = newdamage;
            }
            else if (currentTreeNode.Tag is spawnableTypeItem item)
            {
                item.damage = newdamage;
            }
            else if (currentTreeNode.Tag is CfgSpawnableTypesFile)
            {
                spf.Data.damage = newdamage;
            }
            TreeNode newdamageNode = CreateDamageNode(newdamage);
            currentTreeNode.Nodes.Insert(0, newdamageNode);
            spf.IsDirty = true;
        }
        private void addNewItemToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CfgSpawnableTypesFile spf = currentTreeNode.FindParentOfType<CfgSpawnableTypesFile>();
            AddItemfromTypes form = new AddItemfromTypes();
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    spawnableTypeItem newitem = new spawnableTypeItem()
                    {
                        name = l,
                        chance = (decimal)1.0,
                        cargo = new BindingList<spawnableTypeCargo>(),
                        attachments = new BindingList<spawnableTypeAttachment>()
                    };
                    if (currentTreeNode.Tag is spawnableTypeAttachment attachments)
                    {
                        attachments.item.Add(newitem);
                    }
                    else if (currentTreeNode.Tag is spawnableTypeCargo cargo)
                    {
                        cargo.item.Add(newitem);
                    }
                    currentTreeNode.Nodes.Add(CreateItemNode(newitem));
                }
                EconomyTV.SelectedNode = currentTreeNode.LastNode;

                spf.IsDirty = true;
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }
        private void addNewCargoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CfgSpawnableTypesFile spf = currentTreeNode.FindParentOfType<CfgSpawnableTypesFile>();
            spawnableTypeCargo newcargo = new spawnableTypeCargo()
            {
                item = new BindingList<spawnableTypeItem>()
            };
            if (currentTreeNode.Tag is SpawnableType type)
            {
                type.Items.Add(newcargo);
            }
            else if (currentTreeNode.Tag is spawnableTypeItem item)
            {
                item.cargo.Add(newcargo);
            }
            currentTreeNode.Nodes.Add(createCargoNopdes(newcargo));
            spf.IsDirty = true;
        }
        private void addNewAttachmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CfgSpawnableTypesFile spf = currentTreeNode.FindParentOfType<CfgSpawnableTypesFile>();
            spawnableTypeAttachment newattchemnts = new spawnableTypeAttachment()
            {
                item = new BindingList<spawnableTypeItem>()
            };
            if (currentTreeNode.Tag is SpawnableType type)
            {
                type.Items.Add(newattchemnts);
            }
            else if (currentTreeNode.Tag is spawnableTypeItem item)
            {
                item.attachments.Add(newattchemnts);
            }
            currentTreeNode.Nodes.Add(createattachmentnodes(newattchemnts));
            spf.IsDirty = true;
        }
        private void removeSelectedToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            if (currentTreeNode.Tag is CfgSpawnableTypesFile spawnabletypesfile)
            {
                if (spawnabletypesfile.IsModded == false)
                {
                    var result = MessageBox.Show(
                                $"This Spawnable Types file is in the vanilla file, You cant delete it......",
                                "Vanilla Spawnable Types File",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Question
                            );
                    if (result == DialogResult.OK) { return; }
                }
                else if (spawnabletypesfile.IsModded == true)
                {
                    var result = MessageBox.Show(
                               $"Are you sure you want to delete this full Spawnable Types file?",
                               "Modded Spawnable Types File",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question
                           );
                    if (result == DialogResult.No) { return; }
                }

                bool deleteDirectory;
                string folderPathRel;
                string fileName;

                _economyManager.eonomyCoreConfig.RemoveCe(
                    spawnabletypesfile.FileName,
                    out folderPathRel,
                    out fileName,
                    out deleteDirectory
                );
                RemoveTreeNodeAndEmptyParents(currentTreeNode);
                spawnabletypesfile.IsDirty = true;
                spawnabletypesfile.ToDelete = true;
            }
            else if (currentTreeNode.Tag is SpawnableType type)
            {
                CfgSpawnableTypesFile _spawnabletypesfile = currentTreeNode.FindParentOfType<CfgSpawnableTypesFile>();
                if (_spawnabletypesfile.IsModded == false)
                {
                    var result = MessageBox.Show(
                                $"Spawnable Type is in the vanilla Spawnable Types file, are you sure you want to delete it?",
                                "Vanilla Spawnable Types File",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question
                            );
                    if (result == DialogResult.No) { return; }
                }
                _spawnabletypesfile.Data.type.Remove(type);
                RemoveTreeNodeAndEmptyParents(currentTreeNode);
                _spawnabletypesfile.IsDirty = true;
            }
            else if (currentTreeNode.Tag is spawnableTypesHoarder Hoarder)
            {
                CfgSpawnableTypesFile _spawnabletypesfile = currentTreeNode.FindParentOfType<CfgSpawnableTypesFile>();
                if (_spawnabletypesfile.IsModded == false)
                {
                    var result = MessageBox.Show(
                                $"Spawnable Type is in the vanilla Spawnable Types file, are you sure you want to delete it?",
                                "Vanilla Spawnable Types File",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question
                            );
                    if (result == DialogResult.No) { return; }
                }
                SpawnableType _type = currentTreeNode.Parent.Tag as SpawnableType;
                RemoveTreeNodeAndEmptyParents(currentTreeNode);
                _spawnabletypesfile.IsDirty = true;

            }
            else if (currentTreeNode.Tag is spawnableTypeTag Tag)
            {
                CfgSpawnableTypesFile _spawnabletypesfile = currentTreeNode.FindParentOfType<CfgSpawnableTypesFile>();
                if (currentTreeNode.Parent.Tag is SpawnableType _type)
                {
                    _type.Items.Remove(Tag);
                    RemoveTreeNodeAndEmptyParents(currentTreeNode);
                    _spawnabletypesfile.IsDirty = true;
                }
            }
            else if (currentTreeNode.Tag is spawnableTypeDamage damage)
            {
                CfgSpawnableTypesFile _spawnabletypesfile = currentTreeNode.FindParentOfType<CfgSpawnableTypesFile>();
                if (currentTreeNode.Parent.Tag is CfgSpawnableTypesFile typefile)
                {
                    typefile.Data.damage = null;
                }
                else if (currentTreeNode.Parent.Tag is SpawnableType _type)
                {
                    _type.Items.Remove(damage);
                }
                else if (currentTreeNode.Parent.Tag is spawnableTypeItem _item)
                {
                    _item.damage = null;
                }
                else if (currentTreeNode.Parent.Tag is spawnableTypeCargo _cargo)
                {
                    _cargo.damage = null;
                }
                else if (currentTreeNode.Parent.Tag is spawnableTypeAttachment _attachment)
                {
                    _attachment.damage = null;
                }
                RemoveTreeNodeAndEmptyParents(currentTreeNode);
                _spawnabletypesfile.IsDirty = true;
            }
            else if (currentTreeNode.Tag is spawnableTypeCargo cargo)
            {
                CfgSpawnableTypesFile _spawnabletypesfile = currentTreeNode.FindParentOfType<CfgSpawnableTypesFile>();
                if (currentTreeNode.Parent.Tag is SpawnableType _type)
                {
                    _type.Items.Remove(cargo);
                }
                else if (currentTreeNode.Parent.Tag is spawnableTypeItem _Item)
                {
                    _Item.cargo.Remove(cargo);
                }
                RemoveTreeNodeAndEmptyParents(currentTreeNode);
                _spawnabletypesfile.IsDirty = true;
            }
            else if (currentTreeNode.Tag is spawnableTypeAttachment attachment)
            {
                CfgSpawnableTypesFile _spawnabletypesfile = currentTreeNode.FindParentOfType<CfgSpawnableTypesFile>();
                if (currentTreeNode.Parent.Tag is SpawnableType _type)
                {
                    _type.Items.Remove(attachment);
                }
                else if (currentTreeNode.Parent.Tag is spawnableTypeItem _Item)
                {
                    _Item.attachments.Remove(attachment);
                }
                RemoveTreeNodeAndEmptyParents(currentTreeNode);
                _spawnabletypesfile.IsDirty = true;
            }
            else if (currentTreeNode.Tag is spawnableTypeItem Item)
            {
                CfgSpawnableTypesFile _spawnabletypesfile = currentTreeNode.FindParentOfType<CfgSpawnableTypesFile>();
                if (currentTreeNode.Parent.Tag is spawnableTypeCargo _cargo)
                {
                    _cargo.item.Remove(Item);
                }
                else if (currentTreeNode.Parent.Tag is spawnableTypeAttachment _attachment)
                {
                    _attachment.item.Remove(Item);
                }
                RemoveTreeNodeAndEmptyParents(currentTreeNode);
                _spawnabletypesfile.IsDirty = true;
            }
        }

        /// <summary>
        /// Spawn Gear Right Click Methods
        /// </summary>
        private void addNewSpawnGEarPresetFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEventFile frm = new AddEventFile();
            frm.SetTitle = "Add new Spawn Gear Preset";
            frm.Button4visable = false;
            frm.StartPosition = FormStartPosition.CenterParent;
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string newmodPath = frm.moddir.Replace("/", "\\");
                string typesfile = frm.typesname + ".json";
                string newPath = EnsureModFolderAndGetPath(newmodPath, typesfile);
                SpawnGearPresetFile newfile = new SpawnGearPresetFile(newPath)
                {
                    ModFolder = newmodPath
                };
                bool added = _economyManager.CFGGameplayConfig.AddNewSpawnGear(newfile);
                if (added)
                {
                    currentTreeNode.Nodes.Add(CreateSpawnGearFilesNodes(newfile));
                }
                else
                {
                    MessageBox.Show($"File with the same modfolder and filename allready exist.\nPlease choose differently next time.");
                }
            }
        }
        private void addNewAttachmentSlotItemSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpawnGearPresetFile currentspawnGearPresetFiles = currentTreeNode.FindParentOfType<SpawnGearPresetFile>();
            Attachmentslotitemset newASIS = new Attachmentslotitemset()
            {
                slotName = "CHANGE ME",
                discreteItemSets = new BindingList<Discreteitemset>()
            };
            currentspawnGearPresetFiles.Data.attachmentSlotItemSets.Add(newASIS);
            currentTreeNode.Nodes.Add(AttachmentslotitemsetNodeTN(newASIS));
            currentspawnGearPresetFiles.IsDirty = true;
            EconomyTV.SelectedNode = currentTreeNode.LastNode;
        }
        private void addNewDisctreetItemSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpawnGearPresetFile currentspawnGearPresetFiles = currentTreeNode.FindParentOfType<SpawnGearPresetFile>();
            Attachmentslotitemset Attachmentslotitemset = currentTreeNode.FindParentOfType<Attachmentslotitemset>();
            Discreteitemset newDIS = new Discreteitemset()
            {
                itemType = "CHANGE ME",
                spawnWeight = 1,
                attributes = new Attributes()
                {
                    healthMin = 1,
                    healthMax = 1,
                    quantityMin = 1,
                    quantityMax = 1
                },
                quickBarSlot = -1,
                complexChildrenTypes = new BindingList<Complexchildrentype>(),
                simpleChildrenUseDefaultAttributes = false,
                simpleChildrenTypes = new BindingList<string>()
            };
            Attachmentslotitemset.discreteItemSets.Add(newDIS);
            currentTreeNode.Nodes.Add(DiscreetItemSetsTN(newDIS));
            currentspawnGearPresetFiles.IsDirty = true;
            EconomyTV.SelectedNode = currentTreeNode.LastNode;
        }
        private void addNewComplexChildSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpawnGearPresetFile currentspawnGearPresetFiles = currentTreeNode.FindParentOfType<SpawnGearPresetFile>();
            Complexchildrentype newCCIS = new Complexchildrentype()
            {
                itemType = "CHANGE ME",
                attributes = new Attributes()
                {
                    healthMin = 1,
                    healthMax = 1,
                    quantityMin = 1,
                    quantityMax = 1
                },
                quickBarSlot = -1,
                simpleChildrenUseDefaultAttributes = false,
                simpleChildrenTypes = new BindingList<string>()
            };
            if (currentTreeNode.Parent.Tag is Discreteunsorteditemset Discreteunsorteditemset)
            {
                Discreteunsorteditemset.complexChildrenTypes.Add(newCCIS);
                currentTreeNode.Nodes.Add(ComplexChildrenTypesNodeTN(newCCIS));
                currentspawnGearPresetFiles.IsDirty = true;

            }
            else if (currentTreeNode.Parent.Tag is Discreteitemset Discreteitemset)
            {
                Discreteitemset.complexChildrenTypes.Add(newCCIS);
                currentTreeNode.Nodes.Add(ComplexChildrenTypesNodeTN(newCCIS));
                currentspawnGearPresetFiles.IsDirty = true;
            }
            EconomyTV.SelectedNode = currentTreeNode.LastNode;
        }
        private void addNewDiscreetUnsortedItemSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpawnGearPresetFile currentspawnGearPresetFiles = currentTreeNode.FindParentOfType<SpawnGearPresetFile>();
            Discreteunsorteditemset newDUIS = new Discreteunsorteditemset()
            {
                name = "New Cargo - Change me",
                spawnWeight = 1,
                attributes = new Attributes()
                {
                    healthMin = 1,
                    healthMax = 1,
                    quantityMin = 1,
                    quantityMax = 1
                },
                complexChildrenTypes = new BindingList<Complexchildrentype>(),
                simpleChildrenUseDefaultAttributes = false,
                simpleChildrenTypes = new BindingList<string>()
            };
            currentspawnGearPresetFiles.Data.discreteUnsortedItemSets.Add(newDUIS);
            currentTreeNode.Nodes.Add(DiscreteunsorteditemsetTN(newDUIS));
            currentspawnGearPresetFiles.IsDirty = true;
            EconomyTV.SelectedNode = currentTreeNode.LastNode;
        }
        private void SpawnGearremoveSelectedToolStripMenuItem2_Click(object sender, EventArgs e)
        {

            if (currentTreeNode.Tag is SpawnGearPresetFile SpawnGearPresetFiles)
            {
                _economyManager.CFGGameplayConfig.RemoveSpawnGearPreset(SpawnGearPresetFiles);
                currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
                SpawnGearPresetFiles.IsDirty = true;
                SpawnGearPresetFiles.ToDelete = true;
            }
            else if (currentTreeNode.Tag is Complexchildrentype complexchildrentype)
            {
                SpawnGearPresetFile currentspawnGearPresetFiles = currentTreeNode.FindParentOfType<SpawnGearPresetFile>();
                if (currentTreeNode.Parent.Parent.Tag is Discreteunsorteditemset CurrentDiscreteunsorteditemset)
                {
                    CurrentDiscreteunsorteditemset.complexChildrenTypes.Remove(complexchildrentype);
                    currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
                    currentspawnGearPresetFiles.IsDirty = true;
                }
                else if (currentTreeNode.Parent.Parent.Tag is Discreteitemset CurrentDiscreteitemset)
                {
                    CurrentDiscreteitemset.complexChildrenTypes.Remove(complexchildrentype);
                    currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
                    currentspawnGearPresetFiles.IsDirty = true;
                }
            }
            else if (currentTreeNode.Tag is Discreteitemset Discreteitemset)
            {
                SpawnGearPresetFile currentspawnGearPresetFiles = currentTreeNode.FindParentOfType<SpawnGearPresetFile>();
                Attachmentslotitemset Attachmentslotitemset = currentTreeNode.FindParentOfType<Attachmentslotitemset>();
                Attachmentslotitemset.discreteItemSets.Remove(Discreteitemset);
                currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
                currentspawnGearPresetFiles.IsDirty = true;
            }
            else if (currentTreeNode.Tag is Discreteunsorteditemset Discreteunsorteditemset)
            {
                SpawnGearPresetFile currentspawnGearPresetFiles = currentTreeNode.FindParentOfType<SpawnGearPresetFile>();
                currentspawnGearPresetFiles.Data.discreteUnsortedItemSets.Remove(Discreteunsorteditemset);
                currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
                currentspawnGearPresetFiles.IsDirty = true;
            }
            else if (currentTreeNode.Tag is Attachmentslotitemset Attachmentslotitemset)
            {
                SpawnGearPresetFile currentspawnGearPresetFiles = currentTreeNode.FindParentOfType<SpawnGearPresetFile>();
                currentspawnGearPresetFiles.Data.attachmentSlotItemSets.Remove(Attachmentslotitemset);
                currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
                currentspawnGearPresetFiles.IsDirty = true;
            }
        }

        /// <summary>
        /// Player Restricted Areas Right Click Methods
        /// </summary>
        private void addNewPRAFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEventFile frm = new AddEventFile();
            frm.SetTitle = "Add new Player Restricted Area File";
            frm.Button4visable = false;
            frm.StartPosition = FormStartPosition.CenterParent;
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string newmodPath = frm.moddir.Replace("/", "\\");
                string typesfile = frm.typesname + ".json";
                string newPath = EnsureModFolderAndGetPath(newmodPath, typesfile);
                PlayerRestrictedFile newfile = new PlayerRestrictedFile(newPath)
                {
                    ModFolder = newmodPath,
                    BoxesView = new BindingList<PRABoxes>(),
                    SafePositionsView = new BindingList<PRASafePosition>(),
                    Data = new PlayerRestrictedData()
                    {
                        areaName = frm.typesname,
                    }
                };
                bool added = _economyManager.CFGGameplayConfig.AddNewPlayerRestrictedAreaFile(newfile);
                if (added)
                {
                    currentTreeNode.Nodes.Add(CreateRestrictedfilesNodes(newfile));
                }
                else
                {
                    MessageBox.Show($"File with the same modfolder and filename allready exist.\nPlease choose differently next time.");
                }
            }
        }
        private void addNewPRABoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayerRestrictedFile currentPlayerRestrictedFiles = currentTreeNode.FindParentOfType<PlayerRestrictedFile>();
            PRABoxes newbox = new PRABoxes()
            {
                HalfExtents = new Vec3(20f, 20f, 20f),
                Orientation = new Vec3(0m, 0m, 0m),
                Position = new Vec3((float)_projectManager.CurrentProject.MapSize / 2, 0, (float)_projectManager.CurrentProject.MapSize / 2)
            };
            currentTreeNode.Nodes.Add(CreatePRABoxesNodes(currentPlayerRestrictedFiles.BoxesView.Count(), newbox));
            currentPlayerRestrictedFiles.BoxesView.Add(newbox);
            currentPlayerRestrictedFiles.IsDirty = true;
        }
        private void addNewPRASafePositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayerRestrictedFile currentPlayerRestrictedFiles = currentTreeNode.FindParentOfType<PlayerRestrictedFile>();
            PRASafePosition newsafeposition = new PRASafePosition()
            {
                Position = new Vec3((float)_projectManager.CurrentProject.MapSize / 2, 0, (float)_projectManager.CurrentProject.MapSize / 2)
            };
            currentTreeNode.Nodes.Add(CreatePRASafePositionNodes(newsafeposition, currentPlayerRestrictedFiles.SafePositionsView.Count()));
            currentPlayerRestrictedFiles.SafePositionsView.Add(newsafeposition);
            currentPlayerRestrictedFiles.IsDirty = true;
        }
        private void removePRASelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayerRestrictedFile currentPlayerRestrictedFiles = currentTreeNode.FindParentOfType<PlayerRestrictedFile>();
            if (currentTreeNode.Tag is PRABoxes prabox)
            {
                currentPlayerRestrictedFiles.BoxesView.Remove(prabox);
                currentPlayerRestrictedFiles.IsDirty = true;
                currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
            }
            else if (currentTreeNode.Tag is PRASafePosition PRASafePosition)
            {
                currentPlayerRestrictedFiles.SafePositionsView.Remove(PRASafePosition);
                currentPlayerRestrictedFiles.IsDirty = true;
                currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
            }
            else if (currentTreeNode.Tag is PlayerRestrictedFile PlayerRestrictedFiles)
            {
                _economyManager.CFGGameplayConfig.RemovePlayerRestrictedAreaFile(PlayerRestrictedFiles);
                RemoveTreeNodeAndEmptyParents(currentTreeNode);
                PlayerRestrictedFiles.ToDelete = true;
            }
        }

        /// <summary>
        /// objectspawner arr right click methods
        /// </summary>
        private void addNewObjectSpawnerArrFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Import DZE, JSON, Map file to Object spawner";
            openFileDialog.Filter = "Expansion Map|*.map|Object Spawner|*.json|DayZ Editor|*.dze";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                AddEventFile frm = new AddEventFile();
                frm.SetTitle = "Add New Object Spawner File";
                frm.Button4visable = false;
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.typesname = Path.GetFileNameWithoutExtension(filePath);
                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    string newmodPath = frm.moddir.Replace("/", "\\");
                    string typesfile = frm.typesname + ".json";
                    string newPath = EnsureModFolderAndGetPath(newmodPath, typesfile);
                    ObjectSpawnerArrFile newfile = new ObjectSpawnerArrFile(newPath)
                    {
                        ModFolder = newmodPath,
                        Data = new ObjectSpawnerArrData()
                        {
                            Objects = new BindingList<SpawnObjects>(),
                        }
                    };

                    switch (openFileDialog.FilterIndex)
                    {
                        case 1://.Map File
                            string[] fileContent = File.ReadAllLines(filePath);
                            for (int i = 0; i < fileContent.Length; i++)
                            {
                                if (fileContent[i] == "") continue;
                                string[] linesplit = fileContent[i].Split('|');
                                string[] XYZ = linesplit[1].Split(' ');
                                string[] YPR = linesplit[2].Split(' ');
                                SpawnObjects newso = new SpawnObjects()
                                {
                                    name = linesplit[0],
                                    pos = new float[] { Convert.ToSingle(XYZ[0]), Convert.ToSingle(XYZ[1]), Convert.ToSingle(XYZ[2]) },
                                    ypr = new float[] { Convert.ToSingle(YPR[0]), Convert.ToSingle(YPR[1]), Convert.ToSingle(YPR[2]) },
                                    scale = 1,
                                    enableCEPersistency = false
                                };
                                newfile.Data.Objects.Add(newso);
                            }
                            break;
                        case 2://.Json
                            ObjectSpawnerArrData newobjectspawner = JsonSerializer.Deserialize<ObjectSpawnerArrData>(File.ReadAllText(filePath));
                            newfile.Data.Objects = new BindingList<SpawnObjects>(newobjectspawner.Objects.Select(obj => new SpawnObjects(obj)).ToList());

                            break;
                        case 3://.DZE
                            DZE importfile = DZEHelpers.LoadFile(filePath);
                            ObjectSpawnerArrData newobjectspawnerarr = importfile.convertToObjectSpawner();
                            newfile.Data.Objects = new BindingList<SpawnObjects>(newobjectspawnerarr.Objects.Select(obj => new SpawnObjects(obj)).ToList());
                            break;
                    }
                    bool added = _economyManager.CFGGameplayConfig.AddNewObjectSpawnerArrFile(newfile);
                    if (added)
                    {
                        currentTreeNode.Nodes.Add(new TreeNode(Path.Combine(newfile.ModFolder, newfile.FileName)) { Tag = newfile });
                    }
                    else
                    {
                        MessageBox.Show($"File with the same modfolder and filename allready exist.\nPlease choose differently next time.");
                    }
                }
            }
        }
        private void removeSelectedObjectSpawnerArrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectSpawnerArrFile ObjectSpawnerArr = currentTreeNode.FindParentOfType<ObjectSpawnerArrFile>();
            _economyManager.CFGGameplayConfig.RemoveObjectSpawnerArrFile(ObjectSpawnerArr);
            RemoveTreeNodeAndEmptyParents(currentTreeNode);
            ObjectSpawnerArr.IsDirty = true;
            ObjectSpawnerArr.ToDelete = true;
        }

        /// <summary>
        /// cfgeffectarea right click mehtods
        /// </summary>
        private void addNewEffectAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data newdata = new Data()
            {
                Pos = new decimal[] { _projectManager.CurrentProject.MapSize / 2, 0, _projectManager.CurrentProject.MapSize / 2 },
                Radius = 0,
                PosHeight = 0,
                NegHeight = 0,
            };
            Areas newArea = new Areas()
            {
                AreaName = "New-Trigger-Area",
                Type = "",
                TriggerType = "",
                Data = newdata
            };
            _economyManager.cfgeffectareaConfig.Data.Areas.Add(newArea);
            
            currentTreeNode.Nodes.Add(createeffectareanodes(newArea));
        }
        private void usePlayerDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Areas areas = currentTreeNode.Tag as Areas;
            PlayerData newPlayerData = new PlayerData()
            {
                AroundPartName = "",
                TinyPartName = "",
                PPERequesterType = ""
            };
            areas.PlayerData = newPlayerData;
            currentTreeNode.Nodes.Add(new TreeNode("PlayerData")
            {
                Tag = newPlayerData
            });
            
        }
        private void removePlayerDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Areas areas = currentTreeNode.Parent.Tag as Areas;
            areas.PlayerData = null;
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
            
        }
        private void removeEffectAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Areas area = currentTreeNode.Tag as Areas;
            _economyManager.cfgeffectareaConfig.Data.Areas.Remove(area);
            
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
        }
        private void addNewSafePositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            decimal middle = _projectManager.CurrentProject.MapSize / 2;
            cfgeffectareaSafePosition newpos = new cfgeffectareaSafePosition()
            {
                Name = $"{middle.ToString()},{middle.ToString()}",
                X = middle,
                Z = middle
            };
            _economyManager.cfgeffectareaConfig.Data._positions.Add(newpos);
            
            currentTreeNode.Nodes.Add(new TreeNode($"Position {currentTreeNode.Nodes.Count + 1} ({newpos.X}, {newpos.Z})")
            {
                Tag = newpos
            });

        }
        private void removeSafePositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cfgeffectareaSafePosition sp = currentTreeNode.Tag as cfgeffectareaSafePosition;
            _economyManager.cfgeffectareaConfig.Data._positions.Remove(sp);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
            
        }


        /// <summary>
        /// PlayerSpawn Right Click Methods
        /// </summary>
        private void addNewSpawnPositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            playerspawnpointssection playerspawnpointssection = currentTreeNode.FindParentOfType<playerspawnpointssection>();
            playerspawnpointsGroupPos newpos = new playerspawnpointsGroupPos()
            {
                x = _projectManager.CurrentProject.MapSize / 2,
                z = _projectManager.CurrentProject.MapSize / 2
            };
            if (currentTreeNode.Tag is playerspawnpointsGroup playerspawnpointsGroup)
            {
                playerspawnpointsGroup.pos.Add(newpos);
            }
            else
            {
                if (playerspawnpointssection.generator_posbubbles.Count == 0)
                {
                    playerspawnpointssection.group_params.enablegroups = false;
                    playerspawnpointssection.group_params.lifetime = 0;
                    playerspawnpointssection.group_params.counter = 0;
                }
                playerspawnpointssection.generator_posbubbles.Add(newpos);

            }
            currentTreeNode.Nodes.Add(new TreeNode(newpos.ToString())
            {
                Tag = newpos
            });
        }
        private void removeSpawnPositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            playerspawnpointsGroupPos pos = currentTreeNode.Tag as playerspawnpointsGroupPos;
            if (currentTreeNode.Parent.Tag is playerspawnpointsGroup playerspawnpointsGroup)
            {
                playerspawnpointsGroup.pos.Remove(pos);
            }
            else
            {
                playerspawnpointssection playerspawnpointssection = currentTreeNode.FindParentOfType<playerspawnpointssection>();
                playerspawnpointssection.generator_posbubbles.Remove(pos);
            }
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
        }
        private void addNewSpawnGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            playerspawnpointssection playerspawnpointssection = currentTreeNode.FindParentOfType<playerspawnpointssection>();
            playerspawnpointsGroup newgroup = new playerspawnpointsGroup()
            {
                name = "Change Me",
                lifetimeSpecified = false,
                counterSpecified = false,
                pos = new BindingList<playerspawnpointsGroupPos>()
            };
            currentTreeNode.Nodes.Add(new TreeNode($"Group Name: {newgroup.name}")
            {
                Tag = newgroup
            });
            if (playerspawnpointssection.generator_posbubbles.Count == 0)
            {
                playerspawnpointssection.group_params.enablegroups = true;
                playerspawnpointssection.group_params.lifetime = 240;
                playerspawnpointssection.group_params.counter = -1;
            }
            playerspawnpointssection.generator_posbubbles.Add(newgroup);
        }
        private void removeSpawnGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            playerspawnpointssection playerspawnpointssection = currentTreeNode.FindParentOfType<playerspawnpointssection>();
            playerspawnpointsGroup group = currentTreeNode.Tag as playerspawnpointsGroup;
            playerspawnpointssection.generator_posbubbles.Remove(group);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
        }

        /// <summary>
        /// ignorelist right click methods
        /// </summary>
        private void removeClassnameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ignoreType ignotrtype = currentTreeNode.Tag as ignoreType;
            _economyManager.cfgignorelistConfig.Data.type.Remove(ignotrtype);
            currentTreeNode.Parent.Nodes.Remove(currentTreeNode);
        }
        private void addClassnameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes
            {
            };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    ignoreType newignoretype = new ignoreType()
                    {
                        name = l
                    };
                    if (!_economyManager.cfgignorelistConfig.Data.type.Any(x => x.name == newignoretype.name))
                    {
                        _economyManager.cfgignorelistConfig.Data.type.Add(newignoretype);
                        TreeNode typenode = new TreeNode(newignoretype.name)
                        {
                            Tag = newignoretype
                        };
                        currentTreeNode.Nodes.Add(typenode);
                        currentTreeNode.ExpandAll();
                    }
                    else
                    {
                        MessageBox.Show(newignoretype.name + " Allready exists.....");
                    }
                }
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }


        /// <summary>
        /// MapGroupPos Right Click Methods
        /// </summary>
        private void removeSelectedPositionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<TreeNode> nodestoremove = new List<TreeNode>();
            foreach (TreeNode tn in EconomyTV.SelectedNodes)
            {
                nodestoremove.Add(tn);
            }

            EconomyTV.SelectedNode = currentTreeNode.Parent.Parent;

            foreach (TreeNode tnode in nodestoremove)
            {
                mapGroup mapGroup = tnode.Tag as mapGroup;
                if (mapGroup != null)
                {
                    _economyManager.mapgroupposConfig.Data.group.Remove(mapGroup);
                    TreeNode Parent = tnode.Parent;
                    Parent.Nodes.Remove(tnode);
                    if (Parent.Nodes.Count == 0)
                        Parent.Parent.Nodes.Remove(Parent);
                }
            }
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            TerritorieszonesCB.Location = new Point(14 + EconomyTV.Width + 10, 9);
        }

        private void button1_Click(object sender, EventArgs e)
        {
                Process.Start(new ProcessStartInfo
                {
                    FileName = _economyManager.basePath,
                    UseShellExecute = true
                });
        }

        #region search treeview
        private List<TreeNode> _searchResults = new();
        private int _currentIndex = -1;

        private void button2_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchText)) return;

            _searchResults.Clear();
            _currentIndex = -1;

            FindAllNodes(EconomyTV.Nodes, searchText);

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
            EconomyTV.CollapseAll();
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
            EconomyTV.SelectedNode = node;
            node.EnsureVisible();
        }
        #endregion search treeview



    }

    [PluginInfo("Economy Manager", "EconomyPlugin", "EconomyPlugin.DayzEconomy.png")]
    public class PluginEconomy : IPluginForm, IDisposable
    {
        private bool disposed = false;
        public string pluginIdentifier => "EconomyPlugin";
        public string pluginName => "Economy Manager";
        public Form GetForm()
        {
            var form = new EconomyForm(this);

            if (form is EconomyForm economyForm)
            {
                var map = economyForm.MapControl;

                // Example: Register a sample spawn point
                //var spawnPoint = new EconomySpawnPoint(new PointF(5000, 5000));
                //map.RegisterDrawable(spawnPoint);
            }

            return form;
        }
        public override string ToString() => pluginName;
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
