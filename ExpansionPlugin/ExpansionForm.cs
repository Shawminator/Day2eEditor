using Day2eEditor;

namespace ExpansionPlugin
{
    public partial class ExpansionForm : Form
    {
        private IUIHandler? _currentHandler;
        public MapViewerControl MapControl => _mapControl;
        private IPluginForm _plugin;
        private TreeNode? currentTreeNode;
        private ExpansionManager _expansionManager { get; set; }

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

        public ExpansionForm(IPluginForm plugin)
        {
            InitializeComponent();
            _plugin = plugin;
            _expansionManager = new ExpansionManager();
            _expansionManager.SetExpansionStuff();
            AppServices.Register(_expansionManager);
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
                [typeof(ExpansionBuildNoBuildZone)] = (node, selected) =>
                {
                    ExpansionBuildNoBuildZone ExpansionBuildNoBuildZone = node.Tag as ExpansionBuildNoBuildZone;
                    ShowHandler(new ExpansionBuildNoBuildZoneControl(), typeof(ExpansionBaseBuildingConfig), node.Tag as ExpansionBuildNoBuildZone, selected);
                    SetupBaseBuildingNoBuildZone(ExpansionBuildNoBuildZone, node);
                    _mapControl.EnsureVisible(new PointF((float)ExpansionBuildNoBuildZone.Center[0], (float)ExpansionBuildNoBuildZone.Center[2]));
                },
                [typeof(ExpansionAIConfig)] = (node, selected) =>
                {
                    ExpansionAIConfig ExpansionAIConfig = node.Tag as ExpansionAIConfig;
                    ShowHandler(new AISettingsConfigControl(), typeof(ExpansionAIConfig), ExpansionAIConfig.Data, selected);
                }
            };
            // ----------------------
            // String handlers
            // ----------------------
            _stringHandlers = new Dictionary<string, Action<TreeNode, List<TreeNode>>>
            {
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
                ["BaseBuildingStorage"] = (node, selected) =>
                {
                    ExpansionBaseBuildingConfig cfg = node.FindParentOfType<ExpansionBaseBuildingConfig>();
                    ShowHandler<IUIHandler>(new VirtualStorageExcludedContainersControl(), typeof(ExpansionBaseBuildingConfig), cfg.Data, selected);
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
            };
            // ----------------------
            // String handlers
            // ----------------------
            _stringContextMenus = new Dictionary<string, Action<TreeNode>>
            {
            };
        }

        private void ExpansionForm_Load(object sender, EventArgs e)
        {
            bool flowControl = InitializeMap();
            if (!flowControl)
            {
                return;
            }
            BuildTreeview();
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
                Tag = "AirdropGeneral"
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
        private TreeNode CreateExpansionAIConfigNodes(ExpansionAIConfig ef)
        {
            TreeNode AIRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
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
            TreeNode VirtualStorageExcludedContainersnodes = new TreeNode("Storage")
            {
                Tag = "BaseBuildingStorage"
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
            TreeNode dnodes = new TreeNode("descriptions")
            {
                Tag = "BookDescriptions"
            };
            foreach (ExpansionBookDescriptionCategory bdc in ef.Data.Descriptions)
            {
                TreeNode bdcnodes = new TreeNode($"Category Name: {bdc.CategoryName}")
                {
                    Tag = bdc
                };
                int i = 1;
                foreach (ExpansionBookDescription db in bdc.Descriptions)
                {
                    bdcnodes.Nodes.Add(new TreeNode($"Description Text {i}")
                    {
                        Tag = bdc
                    });
                    i++;
                }
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


            // Reset "selected" state objects
            _selectedNoBuildZonePos = null;
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

        /// <summary>
        /// MapViewer Draw Mothods
        /// </summary>
        private ExpansionBuildNoBuildZone _selectedNoBuildZonePos;
        // Generic map reset + show
        private void SetupMap(Action config)
        {
            _mapControl.Visible = true;
            _mapControl.ClearDrawables();
            config?.Invoke();
        }
        // Specific helpers for different map cases
        private void SetupBaseBuildingNoBuildZone(ExpansionBuildNoBuildZone pos, TreeNode node)
        {
            SetupMap(() =>
            {
                _selectedNoBuildZonePos = pos;
                //_mapControl.MapDoubleClicked += MapControl_EventSpawnDoubleclicked;
                //_mapControl.MapsingleClicked += MapControl_EventSpawnSingleclicked;

                var ExpansionBaseBuildingConfig = node.Parent?.Parent?.Tag as ExpansionBaseBuildingConfig;
                if (ExpansionBaseBuildingConfig != null)
                    DrawbasebuildingNoBuildZones(ExpansionBaseBuildingConfig);
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
                    Radius = 8,
                    Scaleradius = false
                };
                if (_selectedNoBuildZonePos == pos)
                {
                    marker.Color = Color.LimeGreen;
                }
                _mapControl.RegisterDrawable(marker);
            }
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
