using Day2eEditor;
using System.ComponentModel;
using System.Diagnostics;

namespace CaparePlugin
{
    public partial class CapareForm : Form
    {
        private IUIHandler? _currentHandler;
        public MapViewerControl MapControl => _mapControl;
        private IPluginForm _plugin;
        private TreeNode? currentTreeNode;
        private CapareManager _capareManager { get; set; }
        public MapData MapData { get; private set; }

        #region Dictionarys
        private Dictionary<Type, Action<TreeNode, List<TreeNode>>> _typeHandlers;
        private Dictionary<string, Action<TreeNode, List<TreeNode>>> _stringHandlers;
        private Dictionary<Type, Func<TreeNode, List<(string Text, Action Click)>>> _typeContextMenus;
        private Dictionary<string, Func<TreeNode, List<(string Text, Action Click)>>> _stringContextMenus;
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

        public CapareForm(IPluginForm plugin)
        {
            InitializeComponent();
            _plugin = plugin;
            _capareManager = new CapareManager();
            AppServices.Register(_capareManager);
            _capareManager.SetCapareStuff();
        }

        #region Show Control Handlers
        private void initializeShowControlHandlers()
        {
            // ----------------------
            // Type handlers
            // ----------------------
            _typeHandlers = new Dictionary<Type, Action<TreeNode, List<TreeNode>>>
            {
                //Vec3
                [typeof(Vec3)] = (node, selected) =>
                {
                    Vec3 v3 = (Vec3)node.Tag;
                    ShowHandler(new CapareVector3Control(), typeof(CapareDynamicMusicConfig), v3, selected);
                    if (node.Parent?.Tag is BindingList<Vec3>)
                    {
                        // Dynamic music rectangle corner
                        SetupMap(() => BuildDynamicMusicScene(node.FindParentOfType<CapareDynamicMusicLocationStatic>(), node.Parent.Tag as BindingList<Vec3>, v3));
                        _mapControl.EnsureVisible(new PointF(v3.X, v3.Z));
                    }
                },
                //Dynamic Music
                [typeof(CapareDynamicMusicSettings)] = (node, selected) =>
                {
                    ShowHandler(new CapareDynamicMusicGeneralControl(), typeof(CapareDynamicMusicConfig), node.Tag, selected);
                },
                [typeof(CapareDynamicMusicMenu)] = (node, selected) =>
                {
                    ShowHandler(new CapareDynamicMusicMenuTrackControl(), typeof(CapareDynamicMusicConfig), node.Tag, selected);
                },
                [typeof(CapareDynamicMusicTimeOfDay)] = (node, selected) =>
                {
                    ShowHandler(new CapareDynamicMusicTimeTrackControl(), typeof(CapareDynamicMusicConfig), node.Tag, selected);
                },
                [typeof(CapareDynamicMusicLocationStatic)] = (node, selected) =>
                {
                    CapareDynamicMusicLocationStatic location = (CapareDynamicMusicLocationStatic)node.Tag;
                    ShowHandler(new CapareDynamicMusicLocationControl(), typeof(CapareDynamicMusicConfig), location, selected);
                    SetupMap(() => BuildDynamicMusicScene(location, null, null));
                    EnsureRectanglesVisible(location.locationBoundaries);
                },
                [typeof(BindingList<Vec3>)] = (node, selected) =>
                {
                    BindingList<Vec3> rectangle = (BindingList<Vec3>)node.Tag;
                    ShowHandler<IUIHandler>(null, null, null, selected);
                    SetupMap(() => BuildDynamicMusicScene(node.FindParentOfType<CapareDynamicMusicLocationStatic>(), rectangle, null));
                    EnsureRectanglesVisible(new[] { rectangle });
                },
                //KeyCards
                [typeof(KeycardsSettings)] = (node, selected) =>
                {
                    ShowHandler(new CapareKeyCardGeneralControl(), typeof(CapareKeyCardConfig), node.Tag, selected);
                },
                [typeof(KeyCardAreas)] = (node, selected) =>
                {
                    ShowHandler(new CapareKeyCardAreaControl(), typeof(CapareKeyCardConfig), node.Tag, selected);
                    SetupMap(() => BuildKeyCardScene(null, null));
                },
                [typeof(KeyCardLocations)] = (node, selected) =>
                {
                    KeyCardLocations location = (KeyCardLocations)node.Tag;
                    ShowHandler(new CapareKeyCardLocationControl(), typeof(CapareKeyCardConfig), location, selected);
                    SetupMap(() => BuildKeyCardScene(location, location));
                    _mapControl.EnsureVisible(new PointF(location.building_x, location.building_z));
                },
                [typeof(KeyCardLootLocation)] = (node, selected) =>
                {
                    KeyCardLootLocation loot = (KeyCardLootLocation)node.Tag;
                    ShowHandler(new CapareKeyCardLootLocationControl(), typeof(CapareKeyCardConfig), loot, selected);
                    SetupMap(() => BuildKeyCardScene(node.FindParentOfType<KeyCardLocations>(), loot));
                    _mapControl.EnsureVisible(new PointF(loot.Position.X, loot.Position.Z));
                },
                [typeof(InfectedPosition)] = (node, selected) =>
                {
                    InfectedPosition infected = (InfectedPosition)node.Tag;
                    ShowHandler(new CapareVector3Control(), typeof(CapareKeyCardConfig), infected.Position, selected);
                    SetupMap(() => BuildKeyCardScene(node.FindParentOfType<KeyCardLocations>(), infected));
                    _mapControl.EnsureVisible(new PointF(infected.Position.X, infected.Position.Z));
                },
                [typeof(CapareBuildingScanTarget)] = (node, selected) =>
                {
                    ShowHandler(new CapareKeyCardScanTargetControl(), typeof(CapareKeyCardConfig), node.Tag, selected);
                },
                //LootBox
                [typeof(CapareLootBoxSettingsData)] = (node, selected) =>
                {
                    ShowHandler(new CapareLootBoxGeneralControl(), typeof(CapareLootBoxConfig), node.Tag, selected);
                },
                [typeof(CapareLootBoxSettings)] = (node, selected) =>
                {
                    ShowHandler(new CapareLootBoxSettingsControl(), typeof(CapareLootBoxConfig), node.Tag, selected);
                },
                [typeof(CapareLootBoxStaticBox)] = (node, selected) =>
                {
                    CapareLootBoxStaticBox staticBox = (CapareLootBoxStaticBox)node.Tag;
                    ShowHandler(new CapareLootBoxStaticBoxControl(), typeof(CapareLootBoxConfig), staticBox, selected);
                    SetupMap(() => BuildLootBoxScene(staticBox, null));
                },
                [typeof(CapareStaticBoxPosRot)] = (node, selected) =>
                {
                    CapareStaticBoxPosRot pos = (CapareStaticBoxPosRot)node.Tag;
                    ShowHandler(new CapareStaticBoxPosRotControl(), typeof(CapareLootBoxConfig), pos, selected);
                    SetupMap(() => BuildLootBoxScene(node.FindParentOfType<CapareLootBoxStaticBox>(), pos));
                    _mapControl.EnsureVisible(new PointF(pos.Position.X, pos.Position.Z));
                },
                //Loot Framework
                [typeof(CapareLPLootData)] = (node, selected) =>
                {
                    ShowHandler(new CapareLPGeneralControl(), typeof(CapareLPFrameworkConfig), node.Tag, selected);
                },
                [typeof(CapareLPRewardTable)] = (node, selected) =>
                {
                    CapareLPRewardTable table = (CapareLPRewardTable)node.Tag;
                    ShowHandler(new CapareNameControl(), typeof(CapareLPFrameworkConfig), new CapareNamedItem(() => table.RewardName, v => table.RewardName = v), selected);
                },
                [typeof(CapareLPLootTable)] = (node, selected) =>
                {
                    CapareLPLootTable table = (CapareLPLootTable)node.Tag;
                    ShowHandler(new CapareNameControl(), typeof(CapareLPFrameworkConfig), new CapareNamedItem(() => table.TableName, v => table.TableName = v), selected);
                },
                [typeof(CapareLPLootItemSet)] = (node, selected) =>
                {
                    CapareLPLootItemSet set = (CapareLPLootItemSet)node.Tag;
                    ShowHandler(new CapareNameControl(), typeof(CapareLPFrameworkConfig), new CapareNamedItem(() => set.SetName, v => set.SetName = v), selected);
                },
                [typeof(CapareLPdefinedItem)] = (node, selected) =>
                {
                    ShowHandler(new CapareLPDefinedItemControl(), typeof(CapareLPFrameworkConfig), node.Tag, selected);
                },
                //WorkBench
                [typeof(CapareWorkBenchData)] = (node, selected) =>
                {
                    ShowHandler(new CapareWorkBenchGeneralControl(), typeof(CapareWorkBenchConfig), node.Tag, selected);
                },
                [typeof(CapareWorkBenchRecipe)] = (node, selected) =>
                {
                    ShowHandler(new CapareWorkBenchRecipeControl(), typeof(CapareWorkBenchConfig), node.Tag, selected);
                },
                [typeof(CapareRecipeIngredient)] = (node, selected) =>
                {
                    ShowHandler(new CapareWorkBenchIngredientControl(), typeof(CapareWorkBenchConfig), node.Tag, selected);
                },
                //Salvage
                [typeof(CapareSalvageTimingSettings)] = (node, selected) =>
                {
                    ShowHandler(new CapareSalvageTimingControl(), typeof(CapareSalvageConfig), node.Tag, selected);
                },
                [typeof(CapareSalvageOverride)] = (node, selected) =>
                {
                    ShowHandler(new CapareSalvageOverrideControl(), typeof(CapareSalvageConfig), node.Tag, selected);
                },
                [typeof(CapareSalvageYieldRate)] = (node, selected) =>
                {
                    ShowHandler(new CapareSalvageYieldRateControl(), typeof(CapareSalvageConfig), node.Tag, selected);
                },
            };
            // ----------------------
            // String handlers
            // ----------------------
            _stringHandlers = new Dictionary<string, Action<TreeNode, List<TreeNode>>>
            {
                ["CapareStringItem"] = (node, selected) =>
                {
                    if (node.Parent?.Tag is not CapareStringList stringList)
                        return;
                    int index = node.Index;
                    ShowHandler(new CapareNameControl(), typeof(CapareStringList), new CapareNamedItem(() => stringList.List[index], v => stringList.List[index] = v), selected);
                },
            };
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
                handler.GetControl().Dispose();
                if (!TryLoadHandler(_currentHandler, parent, primaryData, selectedNodes))
                    ShowHandler<IUIHandler>(null, parent, primaryData, selectedNodes);
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
            if (!TryLoadHandler(handler, parent, primaryData, selectedNodes))
            {
                handler.GetControl().Dispose();
                (handler as IDisposable)?.Dispose();
                _currentHandler = null;
                return;
            }
            handler.DataChanged += RedrawMap;

            var ctrl = handler.GetControl();
            ctrl.Location = new Point(2, 2);
            splitContainer1.Panel2.Controls.Add(ctrl);
            ctrl.BringToFront();
            ctrl.Visible = true;
        }
        private bool TryLoadHandler(IUIHandler handler, Type parent, object primaryData, List<TreeNode> selectedNodes)
        {
            try
            {
                handler.LoadFromData(parent, primaryData, selectedNodes);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Capare Manager] Failed to show {handler.GetType().Name}: {ex.Message}");
                return false;
            }
        }
        #endregion Show Control Handlers

        #region Context Menus
        private void InitializeContextMenuHandlers()
        {
            _typeContextMenus = new Dictionary<Type, Func<TreeNode, List<(string Text, Action Click)>>>
            {
                //Shared
                [typeof(CapareStringList)] = node =>
                {
                    var items = StringListMenu(node);
                    // Attachment slots of a defined item are string lists that can be removed themselves
                    if (node.Parent?.Tag is string parentTag && parentTag == "LPAttachmentSlots")
                    {
                        items.Add(MenuItem("Remove Attachment Slot", () =>
                        {
                            CapareLPdefinedItem item = node.FindParentOfType<CapareLPdefinedItem>();
                            CapareStringList slotList = (CapareStringList)node.Tag;
                            item.attachments.Remove(item.attachments.First(x => x.attachments == slotList.List));
                            TreeNode parent = node.Parent;
                            node.Remove();
                            RenumberAttachmentSlots(parent);
                        }));
                    }
                    return items;
                },
                //Dynamic Music
                [typeof(CapareDynamicMusicMenu)] = node => RemoveMenu("Remove Menu Track", _capareManager.CapareDynamicMusicConfig.Data.m_TracksMenu, node),
                [typeof(CapareDynamicMusicTimeOfDay)] = node => RemoveMenu("Remove Time Of Day Track", _capareManager.CapareDynamicMusicConfig.Data.m_TracksTime, node),
                [typeof(CapareDynamicMusicLocationStatic)] = node =>
                {
                    CapareDynamicMusicLocationStatic location = (CapareDynamicMusicLocationStatic)node.Tag;
                    var items = new List<(string, Action)>
                    {
                        MenuItem("Add New Rectangle", () =>
                        {
                            BindingList<Vec3> rectangle = CreateRectangleAtMapCentre();
                            location.locationBoundaries.Add(rectangle);
                            TreeNode rectNode = CreateRectangleNode(rectangle, location.locationBoundaries.Count);
                            node.Nodes.Add(rectNode);
                            CapareTV.SelectedNode = rectNode;
                        })
                    };
                    items.AddRange(RemoveMenu("Remove Location Track", _capareManager.CapareDynamicMusicConfig.Data.m_TracksLocationStatic, node));
                    return items;
                },
                [typeof(BindingList<Vec3>)] = node => new List<(string, Action)>
                {
                    MenuItem("Remove Rectangle", () =>
                    {
                        CapareDynamicMusicLocationStatic location = node.FindParentOfType<CapareDynamicMusicLocationStatic>();
                        location.locationBoundaries.Remove((BindingList<Vec3>)node.Tag);
                        TreeNode parent = node.Parent;
                        node.Remove();
                        for (int i = 0; i < parent.Nodes.Count; i++)
                            parent.Nodes[i].Text = $"Rectangle {i + 1}";
                    })
                },
                //KeyCards
                [typeof(KeyCardAreas)] = node => RemoveMenu("Remove Area", _capareManager.CapareKeyCardConfig.Data.KeyCard_Areas, node),
                [typeof(KeyCardLocations)] = node => RemoveMenu("Remove Location", node.FindParentOfType<KeyCardAreas>().KeyCard_Locations, node),
                [typeof(KeyCardLootLocation)] = node => RemoveMenu("Remove Loot Spawn Point", node.FindParentOfType<KeyCardLocations>().LootSpawnPoints, node),
                [typeof(InfectedPosition)] = node => RemoveMenu("Remove Infected Position", node.FindParentOfType<KeyCardLocations>().InfectedPositions, node),
                [typeof(CapareBuildingScanTarget)] = node => RemoveMenu("Remove Scan Target", _capareManager.CapareKeyCardConfig.Data.ScanTargets, node),
                //LootBox
                [typeof(CapareLootBoxSettings)] = node => RemoveMenu("Remove Loot Box", _capareManager.CapareLootBoxConfig.Data.CapareLootBoxConfigs, node),
                [typeof(CapareLootBoxStaticBox)] = node => RemoveMenu("Remove Static Box Set", _capareManager.CapareLootBoxConfig.Data.CapareLootBoxStaticBoxes, node),
                [typeof(CapareStaticBoxPosRot)] = node => RemoveMenu("Remove Position", node.FindParentOfType<CapareLootBoxStaticBox>().StaticBoxPositions, node),
                //Loot Framework
                [typeof(CapareLPRewardTable)] = node => RemoveMenu("Remove Reward Table", _capareManager.CapareLPFrameworkConfig.Data.CapareLPRewardTables, node),
                [typeof(CapareLPLootTable)] = node => RemoveMenu("Remove Loot Table", _capareManager.CapareLPFrameworkConfig.Data.CapareLPLootTables, node),
                [typeof(CapareLPLootItemSet)] = node => RemoveMenu("Remove Item Set", _capareManager.CapareLPFrameworkConfig.Data.CapareLPItemSets, node),
                [typeof(CapareLPdefinedItem)] = node =>
                {
                    var items = new List<(string, Action)>
                    {
                        MenuItem("Fill From Universal Attachments", () => FillDefinedItemFromUniversalAttachments(node))
                    };
                    items.AddRange(RemoveMenu("Remove Defined Item", _capareManager.CapareLPFrameworkConfig.Data.CapareLPdefinedItems, node));
                    return items;
                },
                //WorkBench
                [typeof(CapareWorkBenchRecipe)] = node =>
                {
                    var items = new List<(string, Action)>
                    {
                        MenuItem("Duplicate Recipe", () =>
                        {
                            var copy = CapareJson.Clone((CapareWorkBenchRecipe)node.Tag);
                            copy.Recipe_Name += " Copy";
                            var recipes = _capareManager.CapareWorkBenchConfig.Data.WorkbenchRecipes;
                            recipes.Insert(recipes.IndexOf((CapareWorkBenchRecipe)node.Tag) + 1, copy);
                            _capareManager.CapareWorkBenchConfig.Data.AssignRecipeIds();
                            TreeNode copyNode = CreateRecipeNode(copy);
                            node.Parent.Nodes.Insert(node.Index + 1, copyNode);
                            CapareTV.SelectedNode = copyNode;
                        })
                    };
                    items.AddRange(RemoveMenu("Remove Recipe", _capareManager.CapareWorkBenchConfig.Data.WorkbenchRecipes, node, _capareManager.CapareWorkBenchConfig.Data.AssignRecipeIds));
                    return items;
                },
                [typeof(CapareRecipeIngredient)] = node => RemoveMenu("Remove Ingredient", node.FindParentOfType<CapareWorkBenchRecipe>().Recipe_Ingredients, node),
                //Salvage
                [typeof(CapareSalvageOverride)] = node => RemoveMenu("Remove Salvage Override", _capareManager.CapareSalvageConfig.Data.SalvageOverrides, node),
                [typeof(CapareSalvageYieldRate)] = node => RemoveMenu("Remove Yield Rate", _capareManager.CapareSalvageConfig.Data.SalvageYieldRates, node),
            };

            _stringContextMenus = new Dictionary<string, Func<TreeNode, List<(string Text, Action Click)>>>
            {
                //Shared
                ["CapareStringItem"] = node => new List<(string, Action)>
                {
                    MenuItem("Remove", () =>
                    {
                        CapareStringList stringList = (CapareStringList)node.Parent.Tag;
                        stringList.List.RemoveAt(node.Index);
                        node.Remove();
                    })
                },
                //Dynamic Music
                ["DMMenuTracks"] = node => new List<(string, Action)>
                {
                    MenuItem("Add New Menu Tracks", () =>
                    {
                        foreach (string soundSet in GetStringsFromText("Add Menu Track Sound Sets"))
                            AddNode(node, _capareManager.CapareDynamicMusicConfig.Data.m_TracksMenu, new CapareDynamicMusicMenu(soundSet), x => new TreeNode(x.ToString()) { Tag = x });
                    })
                },
                ["DMTimeTracks"] = node => new List<(string, Action)>
                {
                    MenuItem("Add New Time Of Day Tracks", () =>
                    {
                        foreach (string soundSet in GetStringsFromText("Add Time Of Day Track Sound Sets"))
                            AddNode(node, _capareManager.CapareDynamicMusicConfig.Data.m_TracksTime, new CapareDynamicMusicTimeOfDay(soundSet, (int)CapareDynamicMusicTimeOfDayType.ANY), x => new TreeNode(x.ToString()) { Tag = x });
                    })
                },
                ["DMLocationTracks"] = node => new List<(string, Action)>
                {
                    MenuItem("Add New Location Track", () =>
                    {
                        foreach (string soundSet in GetStringsFromText("Add Location Track Sound Sets"))
                        {
                            var location = new CapareDynamicMusicLocationStatic(soundSet, new BindingList<BindingList<Vec3>>() { CreateRectangleAtMapCentre() }, (int)CapareDynamicMusicTimeOfDayType.ANY);
                            AddNode(node, _capareManager.CapareDynamicMusicConfig.Data.m_TracksLocationStatic, location, CreateDynamicMusicLocationNode);
                        }
                    })
                },
                //KeyCards
                ["KCAreas"] = node => new List<(string, Action)>
                {
                    MenuItem("Add New Area", () =>
                    {
                        foreach (string name in GetStringsFromText("Add Area Names"))
                            AddNode(node, _capareManager.CapareKeyCardConfig.Data.KeyCard_Areas, KeyCardAreas.Create(name, 1), CreateKeyCardAreaNode);
                    })
                },
                ["KCLocations"] = node => new List<(string, Action)>
                {
                    MenuItem("Add New Location", () =>
                    {
                        PointF centre = GetMapCentre();
                        var location = new KeyCardLocations(centre.X, GetHeight(centre), centre.Y, 0, 300, "", "", true, "", false, false, true);
                        AddNode(node, node.FindParentOfType<KeyCardAreas>().KeyCard_Locations, location, CreateKeyCardLocationNode);
                    })
                },
                ["KCLootSpawnPoints"] = node => new List<(string, Action)>
                {
                    MenuItem("Add New Loot Spawn Point", () =>
                    {
                        KeyCardLocations location = node.FindParentOfType<KeyCardLocations>();
                        var loot = new KeyCardLootLocation(new Vec3(location.building_x, location.building_y, location.building_z), new Vec3(0f, 0f, 0f));
                        AddNode(node, location.LootSpawnPoints, loot, x => new TreeNode(x.ToString()) { Tag = x });
                    })
                },
                ["KCInfectedPositions"] = node => new List<(string, Action)>
                {
                    MenuItem("Add New Infected Position", () =>
                    {
                        KeyCardLocations location = node.FindParentOfType<KeyCardLocations>();
                        var infected = new InfectedPosition(new Vec3(location.building_x, location.building_y, location.building_z));
                        AddNode(node, location.InfectedPositions, infected, x => new TreeNode(x.ToString()) { Tag = x });
                    })
                },
                ["KCScanTargets"] = node => new List<(string, Action)>
                {
                    MenuItem("Add New Scan Targets", () =>
                    {
                        foreach (string building in GetStringsFromText("Add Building Class Names"))
                            AddNode(node, _capareManager.CapareKeyCardConfig.Data.ScanTargets, new CapareBuildingScanTarget(building, 0, "", "", "", false, true, 0), x => new TreeNode(x.ToString()) { Tag = x });
                    })
                },
                //LootBox
                ["LBLootBoxes"] = node => new List<(string, Action)>
                {
                    MenuItem("Add New Loot Boxes", () =>
                    {
                        foreach (string name in GetStringsFromTypes())
                            AddNode(node, _capareManager.CapareLootBoxConfig.Data.CapareLootBoxConfigs, new CapareLootBoxSettings(name), CreateLootBoxNode);
                    }),
                    MenuItem("Add New Loot Boxes From Text", () =>
                    {
                        foreach (string name in GetStringsFromText("Add Loot Box Class Names"))
                            AddNode(node, _capareManager.CapareLootBoxConfig.Data.CapareLootBoxConfigs, new CapareLootBoxSettings(name), CreateLootBoxNode);
                    })
                },
                ["LBStaticBoxes"] = node => new List<(string, Action)>
                {
                    MenuItem("Add New Static Box Set", () =>
                    {
                        foreach (string name in GetStringsFromText("Add Static Box Set Names"))
                        {
                            var staticBox = new CapareLootBoxStaticBox()
                            {
                                LootBoxSetName = name,
                                PossibleLootBoxNames = new BindingList<string>(),
                                NumberOfPositions = 1,
                                StaticBoxPositions = new BindingList<CapareStaticBoxPosRot>(),
                                UseCustomLootList = false,
                                CustomLootList = new BindingList<string>()
                            };
                            AddNode(node, _capareManager.CapareLootBoxConfig.Data.CapareLootBoxStaticBoxes, staticBox, CreateStaticBoxNode);
                        }
                    })
                },
                ["LBStaticBoxPositions"] = node => new List<(string, Action)>
                {
                    MenuItem("Add New Position", () =>
                    {
                        PointF centre = GetMapCentre();
                        var pos = new CapareStaticBoxPosRot(new Vec3(centre.X, GetHeight(centre), centre.Y), new Vec3(0f, 0f, 0f));
                        AddNode(node, node.FindParentOfType<CapareLootBoxStaticBox>().StaticBoxPositions, pos, x => new TreeNode(x.ToString()) { Tag = x });
                    })
                },
                //Loot Framework
                ["LPRewardTables"] = node => new List<(string, Action)>
                {
                    MenuItem("Add New Reward Tables", () =>
                    {
                        foreach (string name in GetStringsFromText("Add Reward Table Names"))
                            AddNode(node, _capareManager.CapareLPFrameworkConfig.Data.CapareLPRewardTables, new CapareLPRewardTable(name), CreateRewardTableNode);
                    })
                },
                ["LPLootTables"] = node => new List<(string, Action)>
                {
                    MenuItem("Add New Loot Tables", () =>
                    {
                        foreach (string name in GetStringsFromText("Add Loot Table Names"))
                            AddNode(node, _capareManager.CapareLPFrameworkConfig.Data.CapareLPLootTables, new CapareLPLootTable(name), CreateLootTableNode);
                    })
                },
                ["LPItemSets"] = node => new List<(string, Action)>
                {
                    MenuItem("Add New Item Sets", () =>
                    {
                        foreach (string name in GetStringsFromText("Add Item Set Names"))
                            AddNode(node, _capareManager.CapareLPFrameworkConfig.Data.CapareLPItemSets, new CapareLPLootItemSet(name), CreateItemSetNode);
                    })
                },
                ["LPDefinedItems"] = node => new List<(string, Action)>
                {
                    MenuItem("Add New Defined Items", () =>
                    {
                        foreach (string item in GetStringsFromTypes())
                            AddNode(node, _capareManager.CapareLPFrameworkConfig.Data.CapareLPdefinedItems, new CapareLPdefinedItem("Defined_" + item, item, false), CreateDefinedItemNode);
                    })
                },
                ["LPAttachmentSlots"] = node => new List<(string, Action)>
                {
                    MenuItem("Add New Attachment Slot", () =>
                    {
                        CapareLPdefinedItem item = node.FindParentOfType<CapareLPdefinedItem>();
                        var slot = new CapareLPdefinewItemAttachments() { attachments = new BindingList<string>() };
                        item.attachments.Add(slot);
                        TreeNode slotNode = CreateStringListNode($"Slot {item.attachments.Count}", slot.attachments, CapareStringListSource.Types);
                        node.Nodes.Add(slotNode);
                        CapareTV.SelectedNode = slotNode;
                    })
                },
                //WorkBench
                ["WBRecipes"] = node => new List<(string, Action)>
                {
                    MenuItem("Add New Recipe", () =>
                    {
                        foreach (string name in GetStringsFromText("Add Recipe Names"))
                        {
                            AddNode(node, _capareManager.CapareWorkBenchConfig.Data.WorkbenchRecipes, CapareWorkBenchRecipe.CreateNew(name), CreateRecipeNode);
                            _capareManager.CapareWorkBenchConfig.Data.AssignRecipeIds();
                        }
                    })
                },
                ["WBIngredients"] = node => new List<(string, Action)>
                {
                    MenuItem("Add New Ingredients", () =>
                    {
                        foreach (string name in GetStringsFromTypes())
                            AddNode(node, node.FindParentOfType<CapareWorkBenchRecipe>().Recipe_Ingredients, CapareRecipeIngredient.CreateNew(name), x => new TreeNode(x.ToString()) { Tag = x });
                    }),
                    MenuItem("Add New Ingredients From Text", () =>
                    {
                        foreach (string name in GetStringsFromText("Add Ingredient Class Names"))
                            AddNode(node, node.FindParentOfType<CapareWorkBenchRecipe>().Recipe_Ingredients, CapareRecipeIngredient.CreateNew(name), x => new TreeNode(x.ToString()) { Tag = x });
                    })
                },
                //Salvage
                ["SVOverrides"] = node => new List<(string, Action)>
                {
                    MenuItem("Add New Salvage Overrides", () =>
                    {
                        var overrides = _capareManager.CapareSalvageConfig.Data.SalvageOverrides;
                        foreach (string name in GetStringsFromTypes().Where(x => !overrides.Any(o => o.ClassName == x)))
                            AddNode(node, overrides, new CapareSalvageOverride(name, "Scrap_General_Basket"), x => new TreeNode(x.ToString()) { Tag = x });
                    })
                },
                ["SVYieldRates"] = node => new List<(string, Action)>
                {
                    MenuItem("Add New Yield Rates", () =>
                    {
                        var rates = _capareManager.CapareSalvageConfig.Data.SalvageYieldRates;
                        var unused = CapareSalvageData.Baskets.Where(x => !rates.Any(r => r.Basket == x)).ToList();
                        foreach (string basket in GetStringsFromList("Add Yield Rates", unused))
                            AddNode(node, rates, new CapareSalvageYieldRate(basket, 0.02f), x => new TreeNode(x.ToString()) { Tag = x });
                    })
                },
            };
        }
        private List<(string Text, Action Click)> StringListMenu(TreeNode node)
        {
            CapareStringList stringList = (CapareStringList)node.Tag;
            var items = new List<(string, Action)>();
            switch (stringList.Source)
            {
                case CapareStringListSource.Types:
                    items.Add(MenuItem("Add From Types", () => AddStrings(node, GetStringsFromTypes())));
                    break;
                case CapareStringListSource.RewardTables:
                    items.Add(MenuItem("Add Reward Tables", () => AddStrings(node, GetStringsFromList("Add Reward Tables",
                        _capareManager.CapareLPFrameworkConfig.Data.CapareLPRewardTables.Select(x => x.RewardName).ToList()))));
                    break;
                case CapareStringListSource.RewardEntries:
                    items.Add(MenuItem("Add Loot Framework Entries", () => AddStrings(node, GetStringsFromList("Add Loot Tables, Item Sets or Defined Items",
                        _capareManager.CapareLPFrameworkConfig.Data.GetRewardEntryNames()))));
                    items.Add(MenuItem("Add From Types", () => AddStrings(node, GetStringsFromTypes())));
                    break;
                case CapareStringListSource.KeycardClassNames:
                    items.Add(MenuItem("Add Keycard Class Names", () => AddStrings(node, GetStringsFromList("Add Keycard Class Names",
                        _capareManager.CapareKeyCardConfig.Data.KeycardClassNames.ToList()))));
                    break;
                case CapareStringListSource.LootBoxNames:
                    items.Add(MenuItem("Add Loot Box Names", () => AddStrings(node, GetStringsFromList("Add Loot Box Names",
                        _capareManager.CapareLootBoxConfig.Data.CapareLootBoxConfigs.Select(x => x.LootBoxName).ToList()))));
                    items.Add(MenuItem("Add From Types", () => AddStrings(node, GetStringsFromTypes())));
                    break;
            }
            items.Add(MenuItem("Add From Text", () => AddStrings(node, GetStringsFromText($"Add {stringList.Title}"))));
            return items;
        }
        private static (string Text, Action Click) MenuItem(string text, Action click) => (text, click);
        private void AddStrings(TreeNode node, IEnumerable<string> values)
        {
            CapareStringList stringList = (CapareStringList)node.Tag;
            foreach (string value in values)
            {
                // Reward lists can name the same table more than once to weight it, so only free lists skip duplicates
                if (stringList.Source != CapareStringListSource.RewardEntries && stringList.List.Contains(value))
                    continue;
                stringList.List.Add(value);
                node.Nodes.Add(new TreeNode(value) { Tag = "CapareStringItem" });
            }
            node.Expand();
        }
        private List<(string Text, Action Click)> RemoveMenu<T>(string text, BindingList<T> list, TreeNode node, Action? afterRemove = null)
        {
            return new List<(string, Action)>
            {
                MenuItem(text, () =>
                {
                    list.Remove((T)node.Tag);
                    node.Remove();
                    afterRemove?.Invoke();
                })
            };
        }
        private void AddNode<T>(TreeNode parent, BindingList<T> list, T item, Func<T, TreeNode> createNode)
        {
            list.Add(item);
            TreeNode newNode = createNode(item);
            parent.Nodes.Add(newNode);
            CapareTV.SelectedNode = newNode;
        }
        private List<string> GetStringsFromText(string title)
        {
            AddItemfromString form = new AddItemfromString();
            form.TitleLable = title;
            if (form.ShowDialog() != DialogResult.OK)
                return new List<string>();
            return form.addedtypes.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).ToList();
        }
        private List<string> GetStringsFromTypes()
        {
            AddItemfromTypes form = new AddItemfromTypes { };
            if (form.ShowDialog() != DialogResult.OK)
                return new List<string>();
            return form.AddedTypes.ToList();
        }
        private List<string> GetStringsFromList(string title, List<string> list)
        {
            AddFromList form = new AddFromList();
            form.TitleLable = title;
            form.List = list;
            if (form.ShowDialog() != DialogResult.OK)
                return new List<string>();
            return form.GetSelected;
        }
        #endregion Context Menus

        #region Form Events
        private void CapareForm_Load(object sender, EventArgs e)
        {
            bool flowControl = InitializeMap();
            if (!flowControl)
            {
                return;
            }

            if (_capareManager.HasErrors)
            {
                BeginInvoke(new Action(() =>
                {
                    MessageBox.Show("Errors were detected.\nPlease check the console for further info\nIf all errors are corrected please reload Capare Manager", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }));
                return;
            }
            initializeShowControlHandlers();
            InitializeContextMenuHandlers();
            BuildTreeview();
        }
        private void CapareForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            AppServices.Unregister<CapareManager>();
            _mapControl.ClearMap();
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            savefiles();
        }
        public bool savefiles(bool updated = false)
        {
            IEnumerable<string> savedFiles;
            try
            {
                savedFiles = _capareManager.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Saving failed:\n\n{ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            Console.WriteLine("Saved files:");
            foreach (var file in savedFiles)
            {
                Console.WriteLine(file);
            }
            if (savedFiles.Count() > 0)
            {
                ShowSavedFilesMessage(savedFiles);
            }
            return true;
        }
        private void ShowSavedFilesMessage(IEnumerable<string> files)
        {
            var fileListText = string.Join(Environment.NewLine, files);

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
            IConfigLoader IConfigLoaderFile = currentTreeNode?.FindParentOfType<IConfigLoader>();
            string folderPath = IConfigLoaderFile != null ? Path.GetDirectoryName(IConfigLoaderFile.FilePath) : _capareManager.profilePath;
            if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
                return;
            Process.Start(new ProcessStartInfo
            {
                FileName = folderPath,
                UseShellExecute = true
            });
        }
        private void CapareForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_capareManager.needToSave())
            {
                DialogResult dialogResult = MessageBox.Show("You have Unsaved Changes, do you wish to save", "Unsaved Changes found", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    // Keep the form open if the save failed, so the changes aren't lost
                    if (!savefiles())
                        e.Cancel = true;
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
            _mapControl.MapsingleClicked += MapControl_Singleclicked;
            _mapControl.MapDoubleClicked += MapControl_Doubleclicked;
            return true;
        }
        #endregion Form Events

        #region Treeview
        private void BuildTreeview()
        {
            CapareTV.Nodes.Clear();
            TreeNode rootNode = new TreeNode("Capare")
            {
                Tag = "RootNode",
            };

            TreeNode dynamicMusicNode = new TreeNode("Dynamic Music") { Tag = "DynamicMusicRootNode" };
            dynamicMusicNode.Nodes.Add(CreateDynamicMusicConfigNodes(_capareManager.CapareDynamicMusicConfig));
            rootNode.Nodes.Add(dynamicMusicNode);

            TreeNode keyCardNode = new TreeNode("KeyCard System") { Tag = "KeyCardRootNode" };
            keyCardNode.Nodes.Add(CreateKeyCardConfigNodes(_capareManager.CapareKeyCardConfig));
            rootNode.Nodes.Add(keyCardNode);

            TreeNode lootBoxNode = new TreeNode("Loot Box") { Tag = "LootBoxRootNode" };
            lootBoxNode.Nodes.Add(CreateLootBoxConfigNodes(_capareManager.CapareLootBoxConfig));
            rootNode.Nodes.Add(lootBoxNode);

            TreeNode lootFrameworkNode = new TreeNode("Loot Framework") { Tag = "LootFrameworkRootNode" };
            lootFrameworkNode.Nodes.Add(CreateLPFrameworkConfigNodes(_capareManager.CapareLPFrameworkConfig));
            rootNode.Nodes.Add(lootFrameworkNode);

            TreeNode workBenchNode = new TreeNode("WorkBench") { Tag = "WorkBenchRootNode" };
            workBenchNode.Nodes.Add(CreateWorkBenchConfigNodes(_capareManager.CapareWorkBenchConfig));
            workBenchNode.Nodes.Add(CreateSalvageConfigNodes(_capareManager.CapareSalvageConfig));
            rootNode.Nodes.Add(workBenchNode);

            CapareTV.Nodes.Add(rootNode);
            rootNode.Expand();
        }
        private TreeNode CreateStringListNode(string title, BindingList<string> list, CapareStringListSource source)
        {
            TreeNode listNode = new TreeNode(title)
            {
                Tag = new CapareStringList(title, list, source)
            };
            foreach (string s in list)
            {
                listNode.Nodes.Add(new TreeNode(s) { Tag = "CapareStringItem" });
            }
            return listNode;
        }
        private static TreeNode CreateListNode<T>(string title, string tag, IEnumerable<T> items, Func<T, TreeNode> createNode)
        {
            TreeNode listNode = new TreeNode(title) { Tag = tag };
            foreach (T item in items)
            {
                listNode.Nodes.Add(createNode(item));
            }
            return listNode;
        }
        //Dynamic Music
        private TreeNode CreateDynamicMusicConfigNodes(CapareDynamicMusicConfig ef)
        {
            TreeNode fileNode = new TreeNode(ef.FileName) { Tag = ef };
            fileNode.Nodes.Add(new TreeNode("General") { Tag = ef.Data });
            fileNode.Nodes.Add(CreateListNode("Menu Tracks", "DMMenuTracks", ef.Data.m_TracksMenu, x => new TreeNode(x.ToString()) { Tag = x }));
            fileNode.Nodes.Add(CreateListNode("Time Of Day Tracks", "DMTimeTracks", ef.Data.m_TracksTime, x => new TreeNode(x.ToString()) { Tag = x }));
            fileNode.Nodes.Add(CreateListNode("Static Location Tracks", "DMLocationTracks", ef.Data.m_TracksLocationStatic, CreateDynamicMusicLocationNode));
            return fileNode;
        }
        private TreeNode CreateDynamicMusicLocationNode(CapareDynamicMusicLocationStatic location)
        {
            TreeNode locationNode = new TreeNode(location.ToString()) { Tag = location };
            for (int i = 0; i < location.locationBoundaries.Count; i++)
            {
                locationNode.Nodes.Add(CreateRectangleNode(location.locationBoundaries[i], i + 1));
            }
            return locationNode;
        }
        private TreeNode CreateRectangleNode(BindingList<Vec3> rectangle, int number)
        {
            TreeNode rectNode = new TreeNode($"Rectangle {number}") { Tag = rectangle };
            foreach (Vec3 corner in rectangle)
            {
                rectNode.Nodes.Add(new TreeNode(corner.GetString()) { Tag = corner });
            }
            return rectNode;
        }
        //KeyCards
        private TreeNode CreateKeyCardConfigNodes(CapareKeyCardConfig ef)
        {
            TreeNode fileNode = new TreeNode(ef.FileName) { Tag = ef };
            fileNode.Nodes.Add(new TreeNode("General") { Tag = ef.Data });
            fileNode.Nodes.Add(CreateStringListNode("Admin Steam IDs", ef.Data.AdminSteamIDs, CapareStringListSource.Text));
            fileNode.Nodes.Add(CreateStringListNode("Keycard Class Names", ef.Data.KeycardClassNames, CapareStringListSource.Types));
            fileNode.Nodes.Add(CreateListNode("Areas", "KCAreas", ef.Data.KeyCard_Areas, CreateKeyCardAreaNode));
            fileNode.Nodes.Add(CreateListNode("Scan Targets", "KCScanTargets", ef.Data.ScanTargets, x => new TreeNode(x.ToString()) { Tag = x }));
            return fileNode;
        }
        private TreeNode CreateKeyCardAreaNode(KeyCardAreas area)
        {
            TreeNode areaNode = new TreeNode(area.ToString()) { Tag = area };
            areaNode.Nodes.Add(CreateStringListNode("Reward Tables", area.RewardTableNames, CapareStringListSource.RewardTables));
            areaNode.Nodes.Add(CreateListNode("Locations", "KCLocations", area.KeyCard_Locations, CreateKeyCardLocationNode));
            return areaNode;
        }
        private TreeNode CreateKeyCardLocationNode(KeyCardLocations location)
        {
            TreeNode locationNode = new TreeNode(location.ToString()) { Tag = location };
            locationNode.Nodes.Add(CreateStringListNode("Reward Tables", location.RewardTableNames, CapareStringListSource.RewardTables));
            locationNode.Nodes.Add(CreateListNode("Loot Spawn Points", "KCLootSpawnPoints", location.LootSpawnPoints, x => new TreeNode(x.ToString()) { Tag = x }));
            locationNode.Nodes.Add(CreateListNode("Infected Positions", "KCInfectedPositions", location.InfectedPositions, x => new TreeNode(x.ToString()) { Tag = x }));
            locationNode.Nodes.Add(CreateStringListNode("Infected Classnames", location.InfectedClassnames, CapareStringListSource.Types));
            return locationNode;
        }
        //LootBox
        private TreeNode CreateLootBoxConfigNodes(CapareLootBoxConfig ef)
        {
            TreeNode fileNode = new TreeNode(ef.FileName) { Tag = ef };
            fileNode.Nodes.Add(new TreeNode("General") { Tag = ef.Data });
            fileNode.Nodes.Add(CreateListNode("Loot Boxes", "LBLootBoxes", ef.Data.CapareLootBoxConfigs, CreateLootBoxNode));
            fileNode.Nodes.Add(CreateListNode("Static Box Sets", "LBStaticBoxes", ef.Data.CapareLootBoxStaticBoxes, CreateStaticBoxNode));
            return fileNode;
        }
        private TreeNode CreateLootBoxNode(CapareLootBoxSettings box)
        {
            TreeNode boxNode = new TreeNode(box.ToString()) { Tag = box };
            boxNode.Nodes.Add(CreateStringListNode("Possible Loot List", box.PossibleLootList, CapareStringListSource.RewardTables));
            return boxNode;
        }
        private TreeNode CreateStaticBoxNode(CapareLootBoxStaticBox staticBox)
        {
            TreeNode boxNode = new TreeNode(staticBox.ToString()) { Tag = staticBox };
            boxNode.Nodes.Add(CreateStringListNode("Possible Loot Box Names", staticBox.PossibleLootBoxNames, CapareStringListSource.LootBoxNames));
            boxNode.Nodes.Add(CreateListNode("Static Box Positions", "LBStaticBoxPositions", staticBox.StaticBoxPositions, x => new TreeNode(x.ToString()) { Tag = x }));
            boxNode.Nodes.Add(CreateStringListNode("Custom Loot List", staticBox.CustomLootList, CapareStringListSource.RewardTables));
            return boxNode;
        }
        //Loot Framework
        private TreeNode CreateLPFrameworkConfigNodes(CapareLPFrameworkConfig ef)
        {
            TreeNode fileNode = new TreeNode(ef.FileName) { Tag = ef };
            fileNode.Nodes.Add(new TreeNode("General") { Tag = ef.Data });
            fileNode.Nodes.Add(CreateListNode("Reward Tables", "LPRewardTables", ef.Data.CapareLPRewardTables, CreateRewardTableNode));
            fileNode.Nodes.Add(CreateListNode("Loot Tables", "LPLootTables", ef.Data.CapareLPLootTables, CreateLootTableNode));
            fileNode.Nodes.Add(CreateListNode("Item Sets", "LPItemSets", ef.Data.CapareLPItemSets, CreateItemSetNode));
            fileNode.Nodes.Add(CreateListNode("Defined Items", "LPDefinedItems", ef.Data.CapareLPdefinedItems, CreateDefinedItemNode));
            return fileNode;
        }
        private TreeNode CreateRewardTableNode(CapareLPRewardTable table)
        {
            TreeNode tableNode = new TreeNode(table.ToString()) { Tag = table };
            tableNode.Nodes.Add(CreateStringListNode("Rewards", table.Rewards, CapareStringListSource.RewardEntries));
            return tableNode;
        }
        private TreeNode CreateLootTableNode(CapareLPLootTable table)
        {
            TreeNode tableNode = new TreeNode(table.ToString()) { Tag = table };
            tableNode.Nodes.Add(CreateStringListNode("Loot Items", table.LootItems, CapareStringListSource.Types));
            return tableNode;
        }
        private TreeNode CreateItemSetNode(CapareLPLootItemSet set)
        {
            TreeNode setNode = new TreeNode(set.ToString()) { Tag = set };
            setNode.Nodes.Add(CreateStringListNode("Set Items", set.SetItems, CapareStringListSource.RewardEntries));
            return setNode;
        }
        private TreeNode CreateDefinedItemNode(CapareLPdefinedItem item)
        {
            TreeNode itemNode = new TreeNode(item.ToString()) { Tag = item };
            itemNode.Nodes.Add(CreateStringListNode("Magazines", item.magazines, CapareStringListSource.Types));
            TreeNode slotsNode = new TreeNode("Attachment Slots") { Tag = "LPAttachmentSlots" };
            for (int i = 0; i < item.attachments.Count; i++)
            {
                slotsNode.Nodes.Add(CreateStringListNode($"Slot {i + 1}", item.attachments[i].attachments, CapareStringListSource.Types));
            }
            itemNode.Nodes.Add(slotsNode);
            return itemNode;
        }
        private void FillDefinedItemFromUniversalAttachments(TreeNode node)
        {
            CapareLPdefinedItem item = (CapareLPdefinedItem)node.Tag;
            DumpItem? dump = AppServices.GetRequired<EconomyManager>().GetAttachmentItem(item.Item);
            if (dump == null)
            {
                MessageBox.Show($"{item.Item} was not found in the Universal Attachment database.\nRun the dump script from the Economy plugin to create or refresh it.");
                return;
            }

            CapareDefinedItemAttachmentPreview form = new CapareDefinedItemAttachmentPreview { DumpItem = dump };
            if (form.ShowDialog() != DialogResult.OK)
                return;

            // Replaces the existing magazines and slots rather than merging into them
            item.magazines = new BindingList<string>(form.SelectedMagazines);
            item.attachments = new BindingList<CapareLPdefinewItemAttachments>(
                form.SelectedSlots.Select(x => new CapareLPdefinewItemAttachments { attachments = new BindingList<string>(x) }).ToList());

            // Rebuild the child nodes so the tree points at the new lists
            node.Nodes.Clear();
            foreach (TreeNode child in CreateDefinedItemNode(item).Nodes.Cast<TreeNode>().ToList())
            {
                child.Remove();
                node.Nodes.Add(child);
            }
            node.Expand();
        }
        private static void RenumberAttachmentSlots(TreeNode slotsNode)
        {
            for (int i = 0; i < slotsNode.Nodes.Count; i++)
            {
                slotsNode.Nodes[i].Text = $"Slot {i + 1}";
            }
        }
        //WorkBench
        private TreeNode CreateWorkBenchConfigNodes(CapareWorkBenchConfig ef)
        {
            TreeNode fileNode = new TreeNode(ef.FileName) { Tag = ef };
            fileNode.Nodes.Add(new TreeNode("General") { Tag = ef.Data });
            fileNode.Nodes.Add(CreateListNode("Recipes", "WBRecipes", ef.Data.WorkbenchRecipes, CreateRecipeNode));
            return fileNode;
        }
        private TreeNode CreateRecipeNode(CapareWorkBenchRecipe recipe)
        {
            TreeNode recipeNode = new TreeNode(recipe.ToString()) { Tag = recipe };
            recipeNode.Nodes.Add(CreateListNode("Ingredients", "WBIngredients", recipe.Recipe_Ingredients, x => new TreeNode(x.ToString()) { Tag = x }));
            recipeNode.Nodes.Add(CreateStringListNode("Attachments Needed", recipe.AttachmentsNeeded, CapareStringListSource.Types));
            return recipeNode;
        }
        private TreeNode CreateSalvageConfigNodes(CapareSalvageConfig ef)
        {
            TreeNode fileNode = new TreeNode(ef.FileName) { Tag = ef };
            fileNode.Nodes.Add(new TreeNode("Salvage Timing") { Tag = ef.Data.SalvageTiming });
            fileNode.Nodes.Add(CreateListNode("Salvage Overrides", "SVOverrides", ef.Data.SalvageOverrides, x => new TreeNode(x.ToString()) { Tag = x }));
            fileNode.Nodes.Add(CreateListNode("Salvage Yield Rates", "SVYieldRates", ef.Data.SalvageYieldRates, x => new TreeNode(x.ToString()) { Tag = x }));
            fileNode.Nodes.Add(CreateStringListNode("Salvage Exclusions", ef.Data.SalvageExclusions, CapareStringListSource.Types));
            return fileNode;
        }
        private void CapareTV_AfterSelect(object sender, TreeViewEventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                ResetMapControl();

                currentTreeNode = e.Node;
                var selectedNodes = CapareTV.SelectedNodes.Cast<TreeNode>().ToList();

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
        private void CapareTV_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            CapareTV.SelectedNode = e.Node;
            currentTreeNode = e.Node;

            if (e.Button != MouseButtons.Right) return;

            List<(string Text, Action Click)>? items = null;
            if (e.Node.Tag != null && _typeContextMenus.TryGetValue(e.Node.Tag.GetType(), out var typeHandler))
            {
                items = typeHandler.Invoke(e.Node);
            }
            else if (e.Node.Tag is string s && _stringContextMenus.TryGetValue(s, out var stringHandler))
            {
                items = stringHandler.Invoke(e.Node);
            }
            if (items == null || items.Count == 0)
                return;

            CapareCM.Items.Clear();
            foreach (var (text, click) in items)
            {
                var menuItem = new ToolStripMenuItem(text);
                menuItem.Click += (_, _) => click();
                CapareCM.Items.Add(menuItem);
            }
            CapareCM.Show(Cursor.Position);
        }
        #endregion Treeview

        #region mapstuff
        /// <summary>
        /// A position drawn on the map, clicking it selects the tree node tagged with Owner
        /// </summary>
        private class MapPoint
        {
            public object Owner;
            public PointF Position;
            public Color Color;
        }
        /// <summary>
        /// A rectangle drawn on the map from 2 opposite corners
        /// </summary>
        private class MapRectangle
        {
            public BindingList<Vec3> Corners;
            public Color Color;
        }
        /// <summary>
        /// Everything drawn for the current selection.
        /// MoveSelected is called with the map position when the map is double clicked.
        /// </summary>
        private class MapScene
        {
            public List<MapPoint> Points = new();
            public List<MapRectangle> Rectangles = new();
            public object? Selected;
            public Action<PointF>? MoveSelected;
        }
        private Func<MapScene>? _mapSceneBuilder;
        private MapScene? _mapScene;

        private void ResetMapControl()
        {
            _mapControl.Visible = false;
            _mapControl.ClearDrawables();
            _mapSceneBuilder = null;
            _mapScene = null;
        }
        private void SetupMap(Func<MapScene> sceneBuilder)
        {
            _mapControl.Visible = true;
            if (!_mapControl.HasInitialPainted)
            {
                _mapControl.Invalidate();
                _mapControl.Update();
            }
            _mapSceneBuilder = sceneBuilder;
            RedrawMap();
        }
        private void RedrawMap()
        {
            if (_mapSceneBuilder == null)
                return;
            _mapScene = _mapSceneBuilder();
            _mapControl.ClearDrawables();
            foreach (MapRectangle rect in _mapScene.Rectangles)
            {
                if (rect.Corners.Count < 2)
                    continue;
                float minX = Math.Min(rect.Corners[0].X, rect.Corners[1].X);
                float maxX = Math.Max(rect.Corners[0].X, rect.Corners[1].X);
                float minZ = Math.Min(rect.Corners[0].Z, rect.Corners[1].Z);
                float maxZ = Math.Max(rect.Corners[0].Z, rect.Corners[1].Z);
                if (maxX - minX <= 0 || maxZ - minZ <= 0)
                    continue;
                var polygon = new PolygonDrawable(new[]
                {
                    new PointF(minX, minZ),
                    new PointF(maxX, minZ),
                    new PointF(maxX, maxZ),
                    new PointF(minX, maxZ)
                }, _mapControl.MapSize)
                {
                    Color = rect.Color,
                    Fill = true,
                    FillAlpha = 40
                };
                _mapControl.RegisterDrawable(polygon);
            }
            foreach (MapPoint point in _mapScene.Points)
            {
                bool selected = ReferenceEquals(point.Owner, _mapScene.Selected);
                var marker = new MarkerDrawable(point.Position, _mapControl.MapSize)
                {
                    Color = selected ? Color.LimeGreen : point.Color,
                    Radius = selected ? 8f : 5f
                };
                _mapControl.RegisterDrawable(marker);
            }
            _mapControl.Invalidate();
        }
        private void EnsureRectanglesVisible(IEnumerable<BindingList<Vec3>> rectangles)
        {
            var corners = rectangles.SelectMany(x => x).ToList();
            if (corners.Count == 0)
                return;
            _mapControl.EnsureVisible(new PointF(corners.Average(x => x.X), corners.Average(x => x.Z)));
        }
        private PointF GetMapCentre()
        {
            return new PointF(_mapControl.MapSize.Width / 2f, _mapControl.MapSize.Height / 2f);
        }
        private float GetHeight(PointF mapPos)
        {
            return MapData.FileExists ? MapData.gethieght(mapPos.X, mapPos.Y) : 0;
        }
        private BindingList<Vec3> CreateRectangleAtMapCentre()
        {
            PointF centre = GetMapCentre();
            return new BindingList<Vec3>()
            {
                new Vec3(centre.X - 250, 0, centre.Y - 250),
                new Vec3(centre.X + 250, 0, centre.Y + 250)
            };
        }
        //Scenes
        private MapScene BuildDynamicMusicScene(CapareDynamicMusicLocationStatic? focus, BindingList<Vec3>? focusRectangle, Vec3? selectedCorner)
        {
            var scene = new MapScene() { Selected = selectedCorner };
            foreach (CapareDynamicMusicLocationStatic location in _capareManager.CapareDynamicMusicConfig.Data.m_TracksLocationStatic)
            {
                foreach (BindingList<Vec3> rectangle in location.locationBoundaries)
                {
                    Color color = rectangle == focusRectangle ? Color.Yellow : location == focus ? Color.Red : Color.Gray;
                    scene.Rectangles.Add(new MapRectangle() { Corners = rectangle, Color = color });
                    if (location != focus)
                        continue;
                    foreach (Vec3 corner in rectangle)
                        scene.Points.Add(new MapPoint() { Owner = corner, Position = new PointF(corner.X, corner.Z), Color = Color.White });
                }
            }
            if (selectedCorner != null)
            {
                scene.MoveSelected = p =>
                {
                    selectedCorner.X = p.X;
                    selectedCorner.Z = p.Y;
                    currentTreeNode.Text = selectedCorner.GetString();
                };
            }
            return scene;
        }
        private MapScene BuildKeyCardScene(KeyCardLocations? focus, object? selected)
        {
            var scene = new MapScene() { Selected = selected };
            foreach (KeyCardAreas area in _capareManager.CapareKeyCardConfig.Data.KeyCard_Areas)
            {
                foreach (KeyCardLocations location in area.KeyCard_Locations)
                {
                    scene.Points.Add(new MapPoint() { Owner = location, Position = new PointF(location.building_x, location.building_z), Color = Color.Red });
                }
            }
            if (focus != null)
            {
                foreach (KeyCardLootLocation loot in focus.LootSpawnPoints)
                    scene.Points.Add(new MapPoint() { Owner = loot, Position = new PointF(loot.Position.X, loot.Position.Z), Color = Color.Cyan });
                foreach (InfectedPosition infected in focus.InfectedPositions)
                    scene.Points.Add(new MapPoint() { Owner = infected, Position = new PointF(infected.Position.X, infected.Position.Z), Color = Color.Orange });
            }
            switch (selected)
            {
                case KeyCardLocations location:
                    scene.MoveSelected = p =>
                    {
                        location.building_x = p.X;
                        location.building_z = p.Y;
                        if (MapData.FileExists)
                            location.building_y = GetHeight(p);
                    };
                    break;
                case KeyCardLootLocation loot:
                    scene.MoveSelected = p => MovePosition(loot.Position, p);
                    break;
                case InfectedPosition infected:
                    scene.MoveSelected = p => MovePosition(infected.Position, p);
                    break;
            }
            return scene;
        }
        private MapScene BuildLootBoxScene(CapareLootBoxStaticBox? focus, CapareStaticBoxPosRot? selected)
        {
            var scene = new MapScene() { Selected = selected };
            foreach (CapareLootBoxStaticBox staticBox in _capareManager.CapareLootBoxConfig.Data.CapareLootBoxStaticBoxes)
            {
                foreach (CapareStaticBoxPosRot pos in staticBox.StaticBoxPositions)
                {
                    scene.Points.Add(new MapPoint() { Owner = pos, Position = new PointF(pos.Position.X, pos.Position.Z), Color = staticBox == focus ? Color.Red : Color.Gray });
                }
            }
            if (selected != null)
                scene.MoveSelected = p => MovePosition(selected.Position, p);
            return scene;
        }
        private void MovePosition(Vec3 position, PointF mapPos)
        {
            position.X = mapPos.X;
            position.Z = mapPos.Y;
            if (MapData.FileExists)
                position.Y = GetHeight(mapPos);
        }
        private void MapControl_Singleclicked(object sender, MapClickEventArgs e)
        {
            if (_mapScene == null || _mapScene.Points.Count == 0)
                return;

            PointF clickScreen = _mapControl.MapToScreen(e.MapCoordinates);
            MapPoint? closest = null;
            double closestDistance = double.MaxValue;
            foreach (MapPoint point in _mapScene.Points)
            {
                PointF posScreen = _mapControl.MapToScreen(point.Position);
                double dx = clickScreen.X - posScreen.X;
                double dy = clickScreen.Y - posScreen.Y;
                double distance = Math.Sqrt(dx * dx + dy * dy);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = point;
                }
            }
            if (closest == null || closestDistance > 30 || ReferenceEquals(closest.Owner, _mapScene.Selected))
                return;

            TreeNode? fileNode = currentTreeNode;
            while (fileNode != null && fileNode.Tag is not IConfigLoader)
                fileNode = fileNode.Parent;
            TreeNode? found = fileNode == null ? null : FindNodeByTag(fileNode, closest.Owner);
            if (found != null)
            {
                CapareTV.SelectedNode = found;
                found.EnsureVisible();
            }
        }
        private void MapControl_Doubleclicked(object sender, MapClickEventArgs e)
        {
            if (_mapScene?.MoveSelected == null || currentTreeNode == null)
                return;

            _mapScene.MoveSelected(e.MapCoordinates);
            // Reload the selected node so the control shows the new position
            if (currentTreeNode.Tag != null && _typeHandlers.TryGetValue(currentTreeNode.Tag.GetType(), out var typeHandler))
            {
                typeHandler(currentTreeNode, new List<TreeNode>() { currentTreeNode });
            }
            if (currentTreeNode.Tag is not Vec3)
                currentTreeNode.Text = currentTreeNode.Tag.ToString();
        }
        private static TreeNode? FindNodeByTag(TreeNode parent, object tag)
        {
            foreach (TreeNode child in parent.Nodes)
            {
                if (ReferenceEquals(child.Tag, tag))
                    return child;
                TreeNode? found = FindNodeByTag(child, tag);
                if (found != null)
                    return found;
            }
            return null;
        }
        #endregion mapstuff

        #region search treeview
        private List<TreeNode> _searchResults = new List<TreeNode>();
        private int _searchIndex = 0;
        private void FindButton_Click(object sender, EventArgs e)
        {
            _searchResults.Clear();
            _searchIndex = 0;
            string searchText = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
                return;
            FindAllNodes(CapareTV.Nodes, searchText);
            if (_searchResults.Count > 0)
                SelectNode(_searchResults[0]);
            else
                MessageBox.Show("No matching nodes found.");
        }
        private void NextButton_Click(object sender, EventArgs e)
        {
            if (_searchResults.Count == 0)
                return;
            _searchIndex = (_searchIndex + 1) % _searchResults.Count;
            SelectNode(_searchResults[_searchIndex]);
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
            CapareTV.SelectedNode = node;
            node.EnsureVisible();
        }
        #endregion search treeview
    }

    [PluginInfo("Capare Manager", "CaparePlugin")]
    public class PluginCapare : IPluginForm, IDisposable
    {
        private bool disposed = false;

        public string pluginIdentifier => "CaparePlugin";
        public string pluginName => "Capare Manager";

        public Form GetForm()
        {
            return new CapareForm(this);
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
