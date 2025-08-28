using Day2eEditor;
using System.ComponentModel;
using System.Text.Json;

namespace EconomyPlugin
{
    public partial class EconomyForm : Form
    {
        private IUIHandler? _currentHandler;
        private EventGuard _eventGuard = new EventGuard();
        public MapViewerControl MapControl => _mapControl;
        private EconomyManager _economyManager;
        private readonly ProjectManager _projectManager;
        private IPluginForm _plugin;
        private TreeNode? currentTreeNode;

        public EconomyForm(IPluginForm plugin)
        {
            InitializeComponent();
            _plugin = plugin;
            _economyManager = AppServices.GetRequired<EconomyManager>();
            _projectManager = AppServices.GetRequired<ProjectManager>();
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
        private void LoadTreeview()
        {
            EconomyTV.Nodes.Clear();

            TreeNode rootNode = new TreeNode(Path.GetFileName(_economyManager.basePath))
            {
                Tag = "RootNode",
            };

            // Add all economy files directly under root
            foreach (var ef in _economyManager.economyConfig.AllData)
            {
                string relativePath = Path.GetRelativePath(_economyManager.basePath, ef.FilePath);
                AddFileToTree(rootNode, relativePath, ef, CreateEconomyfileNodes);
            }

            // Add all globals files directly under root
            foreach (var gf in _economyManager.globalsConfig.AllData)
            {
                string relativePath = Path.GetRelativePath(_economyManager.basePath, gf.FilePath);
                AddFileToTree(rootNode, relativePath, gf, CreateGlobalsfileNodes);
            }

            // Types config
            foreach (var tf in _economyManager.TypesConfig.AllData)
            {
                string relativePath = Path.GetRelativePath(_economyManager.basePath, tf.FilePath);
                AddFileToTree(rootNode, relativePath, tf, CreateTypesfileNodes);
            }

            // SpawnableTypes config
            foreach (var sf in _economyManager.cfgspawnabletypesConfig.AllData)
            {
                string relativePath = Path.GetRelativePath(_economyManager.basePath, sf.FilePath);
                AddFileToTree(rootNode, relativePath, sf, CreateSpawnableTypesfileNodes);
            }

            // RandomPresets config
            foreach (var rf in _economyManager.cfgrandompresetsConfig.AllData)
            {
                string relativePath = Path.GetRelativePath(_economyManager.basePath, rf.FilePath);
                AddFileToTree(rootNode, relativePath, rf, CreateRandomPresetsFileNodes);
            }

            // Events config
            foreach (var ef in _economyManager.eventsConfig.AllData)
            {
                string relativePath = Path.GetRelativePath(_economyManager.basePath, ef.FilePath);
                AddFileToTree(rootNode, relativePath, ef, CreateEventNodes);
            }
            // Gameplay config
            string _relativePath = Path.GetRelativePath(_economyManager.basePath, _economyManager.CFGGameplayConfig.FilePath);
            AddFileToTree(rootNode, _relativePath, _economyManager.CFGGameplayConfig.Data, CreateGameplayConfigNodes);
            // limitdefinitions
            _relativePath = Path.GetRelativePath(_economyManager.basePath, _economyManager.cfglimitsdefinitionConfig.FilePath);
            AddFileToTree(rootNode, _relativePath, _economyManager.cfglimitsdefinitionConfig, CreatelimitsDefininitionsConfigNodes);

            // limitdefinitionsuser
            _relativePath = Path.GetRelativePath(_economyManager.basePath, _economyManager.cfglimitsdefinitionuserConfig.FilePath);
            AddFileToTree(rootNode, _relativePath, _economyManager.cfglimitsdefinitionuserConfig, CreatelimitsDefininitionsUserConfigNodes);

            // cfgeffectareaConfig
            _relativePath = Path.GetRelativePath(_economyManager.basePath, _economyManager.cfgeffectareaConfig.FilePath);
            AddFileToTree(rootNode, _relativePath, _economyManager.cfgeffectareaConfig, CreatecfgeffectareaConfigConfigNodes);

            // cfgundergroundtriggersConfig
            _relativePath = Path.GetRelativePath(_economyManager.basePath, _economyManager.cfgundergroundtriggersConfig.FilePath);
            AddFileToTree(rootNode, _relativePath, _economyManager.cfgundergroundtriggersConfig, CreatecfgundergroundtriggersConfigUserConfigNodes);

            EconomyTV.Nodes.Add(rootNode);
        }
        //creating economy Nodes
        private TreeNode CreateEconomyfileNodes(economyFile ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };
            CreateEconomyNodes(ef, EconomyRootNode);

            return EconomyRootNode;
        }
        private static void CreateEconomyNodes(economyFile ef, TreeNode EconomyRootNode)
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
        private TreeNode CreateGlobalsfileNodes(globalsFile gf)
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
        //creating CFGGameplaynodes
        private TreeNode CreateGameplayConfigNodes(cfggameplay ganmeplay)
        {
            TreeNode GameplayRootNode = new TreeNode("GamePlay")
            {
                Tag = _economyManager.CFGGameplayConfig
            };
            GameplayRootNode.Nodes.Add(new TreeNode($"Version:{_economyManager.CFGGameplayConfig.Data.version.ToString()}")
            {
                Tag = "cfggameplayConfigVersion"
            });
            GameplayRootNode.Nodes.Add(new TreeNode($"GeneralData")
            {
                Tag = _economyManager.CFGGameplayConfig.Data.GeneralData
            });
            TreeNode PlayerDataNodes = new TreeNode($"PlayerData")
            {
                Tag = _economyManager.CFGGameplayConfig.Data.PlayerData
            };
            TreeNode spawnGearNodes = new TreeNode("SpawnGear Presets Files")
            {
                Tag = "SpawnGear Presets Files"
            };
            foreach (string spawnfile in _economyManager.CFGGameplayConfig.Data.PlayerData.spawnGearPresetFiles)
            {
                SpawnGearPresetFiles spawnGearPresetFiles = _economyManager.CFGGameplayConfig.GetSpawnGearPreset(spawnfile);
                spawnGearNodes.Nodes.Add(CreateSpawnGearFilesNodes(spawnGearPresetFiles));
            }
            PlayerDataNodes.Nodes.Add(spawnGearNodes);
            GameplayRootNode.Nodes.Add(PlayerDataNodes);
            TreeNode WorldsDataaNodes = new TreeNode($"WorldsData")
            {
                Tag = _economyManager.CFGGameplayConfig.Data.WorldsData
            };
            TreeNode playerRestrictedAreaFilesNodes = new TreeNode($"Player Restricted Area Files")
            {
                Tag = "playerRestrictedAreaFiles"
            };
            foreach (string restrictedfile in _economyManager.CFGGameplayConfig.Data.WorldsData.playerRestrictedAreaFiles)
            {
                PlayerRestrictedFiles PlayerRestrictedFiles = _economyManager.CFGGameplayConfig.getRestrictedFiles(restrictedfile);
                playerRestrictedAreaFilesNodes.Nodes.Add(CreateRestrictedfilesNodes(PlayerRestrictedFiles));
            }
            TreeNode objectspawnerarrfilenodes = new TreeNode($"Object Spawner Arr Files")
            {
                Tag = "ObjectPawnerArrFiles"
            };
            foreach (string objectspawnerarrfile in _economyManager.CFGGameplayConfig.Data.WorldsData.objectSpawnersArr)
            {
                ObjectSpawnerArr ObjectSpawnerArr = _economyManager.CFGGameplayConfig.getobjectspawnerFiles(objectspawnerarrfile);
                objectspawnerarrfilenodes.Nodes.Add(new TreeNode($"{ObjectSpawnerArr.Filename}") {Tag = ObjectSpawnerArr });
            }
            WorldsDataaNodes.Nodes.Add(playerRestrictedAreaFilesNodes);
            WorldsDataaNodes.Nodes.Add(objectspawnerarrfilenodes);
            GameplayRootNode.Nodes.Add(WorldsDataaNodes);
            GameplayRootNode.Nodes.Add(new TreeNode($"BaseBuildingData")
            {
                Tag = _economyManager.CFGGameplayConfig.Data.BaseBuildingData
            });
            GameplayRootNode.Nodes.Add(new TreeNode($"UIData")
            {
                Tag = _economyManager.CFGGameplayConfig.Data.UIData
            });
            GameplayRootNode.Nodes.Add(new TreeNode($"MapData")
            {
                Tag = _economyManager.CFGGameplayConfig.Data.MapData
            });
            GameplayRootNode.Nodes.Add(new TreeNode($"VehicleData")
            {
                Tag = _economyManager.CFGGameplayConfig.Data.VehicleData
            });

            return GameplayRootNode;
        }
        private TreeNode CreateSpawnGearFilesNodes(SpawnGearPresetFiles spawnGearPresetFiles)
        {
            TreeNode rootNode = new TreeNode(spawnGearPresetFiles.Filename)
            {
                Tag = spawnGearPresetFiles
            };
            rootNode.Nodes.Add(new TreeNode("Name")
            {
                Tag = "name"
            });
            rootNode.Nodes.Add(new TreeNode("Spawn Weight")
            {
                Tag = "spawnWeight"
            });
            rootNode.Nodes.Add(new TreeNode("Character Types")
            {
                Tag = "characterTypes"
            });
            TreeNode AttachmentslotitemsetNode = new TreeNode("Attachment slot item set")
            {
                Tag = "attachmentSlotItemSetsParent"
            };
            foreach (Attachmentslotitemset Slot in spawnGearPresetFiles.attachmentSlotItemSets)
            {
                AttachmentslotitemsetNode.Nodes.Add(AttachmentslotitemsetNodeTN(Slot));
            }
            rootNode.Nodes.Add(AttachmentslotitemsetNode);
            TreeNode discreteUnsortedItemSets = new TreeNode("Discrete Unsorted Item Sets")
            {
                Tag = "discreteUnsortedItemSetsParent"
            };
            foreach (Discreteunsorteditemset DUIS in spawnGearPresetFiles.discreteUnsortedItemSets)
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
                Tag = "spawnWeight"
            });
            DUIS.Nodes.Add(new TreeNode("Attributes")
            {
                Tag = "attributes"
            });
            TreeNode ComplexChildrenTypes = new TreeNode("Complex Children Types")
            {
                Tag = "complexChildrenTypes"
            };
            foreach (Complexchildrentype CCT in dUIS.complexChildrenTypes)
            {
                ComplexChildrenTypes.Nodes.Add(ComplexChildrenTypesNodeTN(CCT));
            }
            DUIS.Nodes.Add(ComplexChildrenTypes);
            DUIS.Nodes.Add(new TreeNode("Simple Children Use Default Attributes")
            {
                Tag = "simpleChildrenUseDefaultAttributes"
            });
            DUIS.Nodes.Add(new TreeNode("Simple Children Types")
            {
                Tag = "simpleChildrenTypes"
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
                Tag = "spawnWeight"
            });
            DISNode.Nodes.Add(new TreeNode("Attributes")
            {
                Tag = "attributes"
            });
            DISNode.Nodes.Add(new TreeNode("Quick Bar Slot")
            {
                Tag = "quickBarSlot"
            });
            TreeNode ComplexChildrenTypes = new TreeNode("Complex Children Types")
            {
                Tag = "complexChildrenTypes"
            };
            foreach (Complexchildrentype CCT in DIS.complexChildrenTypes)
            {
                ComplexChildrenTypes.Nodes.Add(ComplexChildrenTypesNodeTN(CCT));
            }
            DISNode.Nodes.Add(ComplexChildrenTypes);
            DISNode.Nodes.Add(new TreeNode("Simple Children Use Default Attributes")
            {
                Tag = "simpleChildrenUseDefaultAttributes"
            });
            DISNode.Nodes.Add(new TreeNode("Simple Children Types")
            {
                Tag = "simpleChildrenTypes"
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
                Tag = "attributes"
            });
            CCTNode.Nodes.Add(new TreeNode("Quick Bar Slot")
            {
                Tag = "quickBarSlot"
            });
            CCTNode.Nodes.Add(new TreeNode("Simple Children Use Default Attributes")
            {
                Tag = "simpleChildrenUseDefaultAttributes"
            });
            CCTNode.Nodes.Add(new TreeNode("Simple Children Types")
            {
                Tag = "simpleChildrenTypes"
            });
            return CCTNode;
        }
        private TreeNode CreateRestrictedfilesNodes(PlayerRestrictedFiles playerRestrictedFiles)
        {
            TreeNode areaNode = new TreeNode(playerRestrictedFiles.Filename)
            {
                Tag = playerRestrictedFiles
            };
            TreeNode praBoxesNode = new TreeNode("PRABoxes")
            {
                Tag = playerRestrictedFiles.PRABoxes
            };
            for (int i = 0; i < playerRestrictedFiles.PRABoxes.Count; i++)
            {
                var box = playerRestrictedFiles.PRABoxes[i];
                TreeNode boxNode = new TreeNode($"Box {i + 1}")
                {
                    Tag = box
                };

                boxNode.Nodes.Add("HalfExtents: [" + string.Join(", ", box[0]) + "]");
                boxNode.Nodes.Add("Orientation: [" + string.Join(", ", box[1]) + "]");
                boxNode.Nodes.Add("Position: [" + string.Join(", ", box[2]) + "]");

                praBoxesNode.Nodes.Add(boxNode);
            }
            TreeNode safePositionsNode = new TreeNode("SafePositions3D");
            safePositionsNode.Tag = playerRestrictedFiles.SafePositions3D;

            for (int i = 0; i < playerRestrictedFiles.SafePositions3D.Count; i++)
            {
                var pos = playerRestrictedFiles.SafePositions3D[i];
                TreeNode posNode = new TreeNode($"Position {i + 1}: [{string.Join(", ", pos)}]")
                {
                    Tag = pos
                };
                safePositionsNode.Nodes.Add(posNode);
            }

            areaNode.Nodes.Add(praBoxesNode);
            areaNode.Nodes.Add(safePositionsNode);
            return areaNode;
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
            return TypesrootNode;
        }
        //Creating spawnableTypes Nodes
        private TreeNode CreateSpawnableTypesfileNodes(cfgspawnabletypesFile stf)
        {
            TreeNode ConfigRoot = new TreeNode(Path.GetFileNameWithoutExtension(stf.FileName))
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
        private TreeNode CreateRandomPresetsFileNodes(cfgrandompresetsFile rpc)
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
                TreeNode eventposnodes = new TreeNode("pos");
                eventposnodes.Name = "POS";
                eventposnodes.Tag = "PosParent";
                foreach (eventposdefEventPos pos in eventspawns.pos)
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
                    eventposnodes.Nodes.Add(posnodes);
                }
                eventspawnroot.Nodes.Add(eventposnodes);
            }
            return eventspawnroot;
        }
        //Creating Definntions
        private TreeNode CreatelimitsDefininitionsConfigNodes(cfglimitsdefinitionConfig cfglimitsdefinitionConfig)
        {
            TreeNode eventRoot = new TreeNode(cfglimitsdefinitionConfig.FileName)
            {
                Tag = cfglimitsdefinitionConfig
            };
            return eventRoot;
        }
        private TreeNode CreatelimitsDefininitionsUserConfigNodes(cfglimitsdefinitionuserConfig cfglimitsdefinitionuserConfig)
        {
            TreeNode eventRoot = new TreeNode(cfglimitsdefinitionuserConfig.FileName)
            {
                Tag = cfglimitsdefinitionuserConfig
            };
            return eventRoot;
        }
        private TreeNode CreatecfgundergroundtriggersConfigUserConfigNodes(cfgundergroundtriggersConfig config)
        {
            TreeNode eventRoot = new TreeNode(config.FileName)
            {
                Tag = config
            };
            return eventRoot;
        }
        private TreeNode CreatecfgeffectareaConfigConfigNodes(cfgeffectareaConfig config)
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
                    var areaNode = new TreeNode(area.AreaName ?? "Unnamed Area")
                    {
                        Tag = area
                    };

                    // Only stop at Data + PlayerData
                    areaNode.Nodes.Add(new TreeNode("Data") { Tag = area.Data });
                    areaNode.Nodes.Add(new TreeNode("PlayerData") { Tag = area.PlayerData });

                    areasNode.Nodes.Add(areaNode);
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
        #endregion loading treeview

        /// <summary>
        /// Treeview stuff
        /// </summary>
        private void HandleTreeViewSelection<TFile, TSection>(TFile file, string sectionName, Func<TFile, TSection> getSection, TreeView treeView)
                    where TFile : class
                    where TSection : class
        {
            TreeNode parentNode = EconomyTV.Nodes[0];
            TreeNode fileNode = FindOrCreateFileNodeFor(file, treeView, parentNode);
            TreeNode sectionNode = FindOrCreateSectionNode(fileNode, sectionName, getSection(file));

            treeView.SelectedNode = sectionNode;
            sectionNode.EnsureVisible();
        }
        private TreeNode FindOrCreateFileNodeFor<T>(T data, TreeView treeView, TreeNode parentNode = null)
        {
            // First, try to find an existing node for this file anywhere in the tree
            TreeNode foundNode = (parentNode != null)
                ? FindNodeRecursive(parentNode.Nodes, data)
                : FindNodeRecursive(treeView.Nodes, data);

            if (foundNode != null)
                return foundNode;

            // If not found, create it using existing AddFileToTree logic
            if (data is economyFile ecfile)
            {
                string relativePath = Path.GetRelativePath(_economyManager.basePath, ecfile.FilePath);

                // We pass in either the provided parent node or the root
                TreeNode rootNode = parentNode ?? treeView.Nodes[0];

                // This uses your *existing* implementation so nothing breaks
                AddFileToTree(rootNode, relativePath, ecfile, CreateEconomyfileNodes);

                // Return the node that was just added
                return FindNodeRecursive(rootNode.Nodes, ecfile);
            }
            else if (data is globalsFile globalsFile)
            {
                string relativePath = Path.GetRelativePath(_economyManager.basePath, globalsFile.FilePath);

                // We pass in either the provided parent node or the root
                TreeNode rootNode = parentNode ?? treeView.Nodes[0];

                // Reuse AddFileToTree to respect folder structure
                AddFileToTree(rootNode, relativePath, globalsFile, CreateGlobalsfileNodes);

                return FindNodeRecursive(rootNode.Nodes, globalsFile);
            }

            return null;
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
        void ShowHandler<T>(T handler, object primaryData, List<TreeNode> selectedNodes)
        where T : IUIHandler
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
            if (_currentHandler != null && _currentHandler.GetType() == typeof(T))
            {
                _currentHandler.LoadFromData(primaryData, selectedNodes);
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
            handler.LoadFromData(primaryData, selectedNodes);

            var ctrl = handler.GetControl();
            splitContainer1.Panel2.Controls.Add(ctrl);
            ctrl.BringToFront();
            ctrl.Visible = true;
        }
        private void EconomyTV_AfterSelect(object sender, TreeViewEventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                _selectedEventPos = null;
                _mapControl.Visible = false;
                _mapControl.MapDoubleClicked -= MapControl_EventSpawnDoubleclicked;
                _mapControl.MapsingleClicked -= MapControl_EventSpawnSingleclicked;
                _mapControl.MapDoubleClicked -= MapControl_EffectSafePositionsSingleclicked;
                _mapControl.MapsingleClicked -= MapControl_EffectSafePositionsDoubleclicked;
                currentTreeNode = e.Node;

                var selectedNodes = EconomyTV.SelectedNodes.Cast<TreeNode>().ToList();
                if (e.Node.Tag is string _string)
                {
                    switch (_string)
                    {
                        default:
                            ShowHandler<IUIHandler>(null, null, null);
                            break;
                    }
                }
                else if (e.Node.Tag is economyFile)
                {
                    ShowHandler<IUIHandler>(null, null, null);
                }
                else if (e.Node.Tag is globalsFile)
                {
                    ShowHandler<IUIHandler>(null, null, null);
                }
                else if (e.Node.Tag is EconomySection economydata)
                {
                    economyFile ef = e.Node.Parent.Tag as economyFile;
                    if (ef.IsModded)
                        ShowHandler(new economyControl(), economydata, selectedNodes);
                }
                else if (e.Node.Tag is variablesVar varData)
                {
                    globalsFile gf = e.Node.Parent.Tag as globalsFile;
                    if (gf.IsModded)
                        ShowHandler(new VariablesVarControl(), varData, selectedNodes);
                }
                else if (e.Node.Tag is TypesFile typefile)
                {
                    ShowHandler(new TypesCollectionControl(), typefile, selectedNodes);
                }
                else if (e.Node.Tag is Category cat)
                {
                    if (e.Node.Parent == null) { return; }
                    ShowHandler(new TypesCollectionControl(), e.Node.Parent.Tag as TypesFile, selectedNodes);
                }
                else if (e.Node.Tag is TypeEntry typentry)
                {
                    // Build a flattened list of (File, Entry) tuples
                    var matchingEntries = _economyManager.TypesConfig.AllData
                        .SelectMany(tf => tf.Data.TypeList.Select(te => (File: tf, Entry: te)))
                        .Where(x => x.Entry.Name == typentry.Name)
                        .ToList();

                    // Get the latest match (file + entry)
                    var latestMatch = matchingEntries.LastOrDefault();

                    if (latestMatch != default && !ReferenceEquals(latestMatch.Entry, typentry))
                    {
                        var result = MessageBox.Show(
                            $"This type is overridden by a later definition in:\n{latestMatch.File.FileName}\n\nJump to it?",
                            "Type Override Found",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question
                        );
                        if (result == DialogResult.Yes)
                        {
                            Console.WriteLine($"Jumping to latest override in file: {latestMatch.File.FileName}");
                            var foundNode = FindNodeByTag(EconomyTV.Nodes, latestMatch.Entry);
                            if (foundNode != null)
                            {
                                EconomyTV.SelectedNode = foundNode; // triggers AfterSelect again
                            }
                        }
                        else
                        {
                            // User chose No — show current entry
                            ShowHandler(new TypesControl(), typentry, selectedNodes);
                        }
                    }
                    else
                    {
                        // Already latest — show control
                        ShowHandler(new TypesControl(), typentry, selectedNodes);
                    }
                }
                else if (e.Node.Tag is EventsFile)
                {
                    ShowHandler<IUIHandler>(null, null, null);
                }
                else if (e.Node.Tag is events)
                {
                    ShowHandler<IUIHandler>(null, null, null);
                }
                else if (e.Node.Tag is eventsEvent _event)
                {
                    // Build a flattened list of (File, Entry) tuples
                    var matchingEntries = _economyManager.eventsConfig.AllData
                        .SelectMany(tf => tf.Data.@event.Select(te => (File: tf, Event: te)))
                        .Where(x => x.Event.name == _event.name)
                        .ToList();

                    // Get the latest match (file + entry)
                    var latestMatch = matchingEntries.LastOrDefault();


                    if (latestMatch != default && !ReferenceEquals(latestMatch.Event, _event))
                    {
                        var result = MessageBox.Show(
                            $"This Event is overridden by a later definition in:\n{latestMatch.File.FileName}\n\nJump to it?",
                            "Type Override Found",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question
                        );
                        if (result == DialogResult.Yes)
                        {
                            Console.WriteLine($"Jumping to latest override in file: {latestMatch.File.FileName}");
                            var foundNode = FindNodeByTag(EconomyTV.Nodes, latestMatch.Event);
                            if (foundNode != null)
                            {
                                EconomyTV.SelectedNode = foundNode; // triggers AfterSelect again
                            }
                        }
                        else
                        {
                            // User chose No — show current entry
                            ShowHandler(new EventsControl(), _event, selectedNodes);
                        }
                    }
                    else
                    {
                        // Already latest — show control
                        ShowHandler(new EventsControl(), _event, selectedNodes);
                    }
                }
                else if (e.Node.Tag is eventposdefEvent)
                {
                    ShowHandler<IUIHandler>(null, null, null);
                }
                else if (e.Node.Tag is eventposdefEventPos eventpos)
                {
                    ShowHandler<IUIHandler>(null, null, null);
                    _mapControl.Visible = true;
                    _mapControl.ClearDrawables();
                    _selectedEventPos = eventpos;

                    _mapControl.MapDoubleClicked += MapControl_EventSpawnDoubleclicked;
                    _mapControl.MapsingleClicked += MapControl_EventSpawnSingleclicked;

                    eventposdefEvent defevent = e.Node.Parent.Parent.Tag as eventposdefEvent;
                    DrawEventSpawns(defevent);
                }
                else if (e.Node.Tag is CFGGameplayConfig)
                {
                    ShowHandler<IUIHandler>(null, null, null);
                }
                else if (e.Node.Tag.ToString() == "cfggameplayConfigVersion")
                {
                    ShowHandler<IUIHandler>(null, null, null);
                }
                else if (e.Node.Tag is Generaldata Generaldata)
                {
                    ShowHandler(new cfggameplayGeneralDataControl(), Generaldata, selectedNodes);
                }
                else if (e.Node.Tag is Playerdata Playerdata)
                {
                    ShowHandler(new cfggameplayPlayerDataControl(), Playerdata, selectedNodes);
                }
                else if (e.Node.Tag is Worldsdata Worldsdata)
                {
                    ShowHandler(new cfggameplayWordlsDataControl(), Worldsdata, selectedNodes);
                }
                else if (e.Node.Tag is Basebuildingdata Basebuildingdata)
                {
                    ShowHandler(new cfggameplayBaseBuildingDataControl(), Basebuildingdata, selectedNodes);
                }
                else if (e.Node.Tag is Uidata Uidata)
                {
                    ShowHandler(new cfggameplayUIDataControl(), Uidata, selectedNodes);
                }
                else if (e.Node.Tag is CFGGameplayMapData)
                {
                    ShowHandler<IUIHandler>(null, null, null);
                }
                else if (e.Node.Tag is VehicleData)
                {
                    ShowHandler<IUIHandler>(null, null, null);
                }
                else if (e.Node.Tag is cfgspawnabletypesFile)
                {
                    ShowHandler<IUIHandler>(null, null, null);
                }
                else if (e.Node.Tag is randompresetsAttachments PresetAttachment)
                {
                    ShowHandler(new RandompresetsAttchmentsControl(), PresetAttachment, selectedNodes);
                }
                else if (e.Node.Tag is randompresetsCargo PresetCargo)
                {
                    ShowHandler(new RandompresetsCargoControl(), PresetCargo, selectedNodes);
                }
                else if (e.Node.Tag is randompresetsItem item)
                {
                    ShowHandler(new RandomPresetItemControl(), item, selectedNodes);
                }
                else if (e.Node.Tag is SpawnableType spawnabletype)
                {
                    ShowHandler(new SpawnabletypesControl(), spawnabletype, selectedNodes);
                }
                else if (e.Node.Tag is SpawnableTypes spawnabletypes)
                {
                    ShowHandler<IUIHandler>(null, null, null);
                }
                else if (e.Node.Tag is spawnableTypesHoarder spawnableTypesHoarder)
                {
                    ShowHandler<IUIHandler>(null, null, null);
                }
                else if (e.Node.Tag is spawnableTypeCargo spawnableTypeCargo)
                {
                    ShowHandler(new SpawnableTypesCargoControl(), spawnableTypeCargo, selectedNodes);
                }
                else if (e.Node.Tag is spawnableTypeAttachment spawnableTypeAttachment)
                {
                    ShowHandler(new SpawnableTypesAttachmentsControl(), spawnableTypeAttachment, selectedNodes);
                }
                else if (e.Node.Tag is spawnableTypeItem spawnableTypeItem)
                {
                    ShowHandler(new SpawnableTypesItemControl(), spawnableTypeItem, selectedNodes);
                }
                else if (e.Node.Tag is spawnableTypeTag spawnableTypeTag)
                {
                    ShowHandler(new SpawnabletypesTagsControl(), spawnableTypeTag, selectedNodes);
                }
                else if (e.Node.Tag is spawnableTypeDamage spawnableTypeDamage)
                {
                    ShowHandler(new SpawnabletypesDamageControl(), spawnableTypeDamage, selectedNodes);
                }
                else if (e.Node.Tag is cfgeffectareaSafePosition cfgeffectareaSafePosition)
                {
                    ShowHandler<IUIHandler>(null, null, null);
                    _mapControl.Visible = true;
                    _mapControl.ClearDrawables();
                    _selectedEventPos = null;
                    _selectedSafePosition = cfgeffectareaSafePosition;

                    _mapControl.MapsingleClicked += MapControl_EffectSafePositionsSingleclicked;
                    _mapControl.MapDoubleClicked += MapControl_EffectSafePositionsDoubleclicked;

                    cfgeffectareaConfig cfgeffectareaConfig = e.Node.FindParentOfType<cfgeffectareaConfig>();
                    DrawEffectSafePositions(cfgeffectareaConfig);
                }
            }));
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
            if (e.Button == MouseButtons.Right)
            {
                if (e.Node.Tag.ToString() == "RootNode")
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
                }
                else if (e.Node.Tag is EconomySection economydata)
                {
                    economyFile ef = e.Node.Parent.Tag as economyFile;
                    if (!ef.IsModded)
                    {
                        editPropertyToolStripMenuItem.Visible = true;
                        setToDefaultToolStripMenuItem.Visible = false;
                        EditPropertyCMS.Show(Cursor.Position);
                    }
                    else if (ef.IsModded)
                    {
                        editPropertyToolStripMenuItem.Visible = false;
                        setToDefaultToolStripMenuItem.Visible = true;
                        EditPropertyCMS.Show(Cursor.Position);
                    }
                }
                else if (e.Node.Tag is variablesVar varData)
                {
                    globalsFile gf = e.Node.Parent.Tag as globalsFile;
                    if (!gf.IsModded)
                    {
                        editPropertyToolStripMenuItem.Visible = true;
                        setToDefaultToolStripMenuItem.Visible = false;
                        EditPropertyCMS.Show(Cursor.Position);
                    }
                    else if (gf.IsModded)
                    {
                        editPropertyToolStripMenuItem.Visible = false;
                        setToDefaultToolStripMenuItem.Visible = true;
                        EditPropertyCMS.Show(Cursor.Position);
                    }
                }
                else if (e.Node.Tag is TypesFile TypeFile)
                {
                    TypesCM.Items.Clear();
                    TypesCM.Items.Add(addNewTypesToolStripMenuItem);
                    TypesCM.Items.Add(removeSelectedToolStripMenuItem);
                    TypesCM.Show(Cursor.Position);
                }
                else if (e.Node.Tag is TypeEntry typeentry)
                {
                    TypesCM.Items.Clear();
                    TypesCM.Items.Add(removeSelectedToolStripMenuItem);
                    TypesCM.Show(Cursor.Position);
                }
                else if (e.Node.Tag is EventsFile eventfile)
                {
                    EventsCM.Items.Clear();
                    EventsCM.Items.Add(AddNewEventsToolstripMenuItem);
                    EventsCM.Items.Add(RemoveEventsToolStripMenuItem);
                    EventsCM.Show(Cursor.Position);
                }
                else if (e.Node.Tag is eventsEvent)
                {
                    EventsCM.Items.Clear();
                    EventsCM.Items.Add(RemoveEventsToolStripMenuItem);
                    EventsCM.Show(Cursor.Position);
                }
                else if (e.Node.Tag is eventposdefEvent eventpos)
                {
                    foreach (ToolStripMenuItem TSMI in EventSpawnContextMenu.Items)
                    {
                        TSMI.Visible = false;
                    }
                    addNewPosirtionToolStripMenuItem.Visible = true;
                    importPositionFromdzeToolStripMenuItem.Visible = true;
                    importPositionAndCreateEventgroupFormdzeToolStripMenuItem.Visible = true;
                    deleteSelectedEventSpawnToolStripMenuItem.Visible = true;
                    if (eventpos.pos != null && eventpos.pos.Count > 0)
                    {
                        removeAllPositionToolStripMenuItem.Visible = true;
                        exportPositionTodzeToolStripMenuItem.Visible = true;
                    }
                    EventSpawnContextMenu.Show(Cursor.Position);
                }
                else if (e.Node.Tag.ToString() == "RandomPresetsAttachments")
                {
                    RandomPresetsCM.Items.Clear();
                    RandomPresetsCM.Items.Add(addNewAttchementToolStripMenuItem);
                    RandomPresetsCM.Show(Cursor.Position);
                }
                else if (e.Node.Tag.ToString() == "RandomPresetsCargo")
                {
                    RandomPresetsCM.Items.Clear();
                    RandomPresetsCM.Items.Add(addNewCargoToolStripMenuItem);
                    RandomPresetsCM.Show(Cursor.Position);
                }
                else if (e.Node.Tag is cfgrandompresetsFile)
                {
                    RandomPresetsCM.Items.Clear();
                    RandomPresetsCM.Items.Add(removeSelectedRandomPresetToolStripmenuItem);
                    RandomPresetsCM.Show(Cursor.Position);

                }
                else if (e.Node.Tag is randompresetsAttachments)
                {
                    RandomPresetsCM.Items.Clear();
                    RandomPresetsCM.Items.Add(addNewItemToolStripMenuItem);
                    RandomPresetsCM.Items.Add(removeSelectedRandomPresetToolStripmenuItem);
                    RandomPresetsCM.Show(Cursor.Position);
                }
                else if (e.Node.Tag is randompresetsCargo)
                {
                    RandomPresetsCM.Items.Clear();
                    RandomPresetsCM.Items.Add(addNewItemToolStripMenuItem);
                    RandomPresetsCM.Items.Add(removeSelectedRandomPresetToolStripmenuItem);
                    RandomPresetsCM.Show(Cursor.Position);
                }
                else if (e.Node.Tag is randompresetsItem)
                {
                    RandomPresetsCM.Items.Clear();
                    RandomPresetsCM.Items.Add(removeSelectedRandomPresetToolStripmenuItem);
                    RandomPresetsCM.Show(Cursor.Position);
                }
                else if (e.Node.Tag is cfgspawnabletypesFile typefile)
                {
                    SpawnableTypesCM.Items.Clear();
                    SpawnableTypesCM.Items.Add(addNewSpawnableTypeToolStripMenuItem);
                    SpawnableTypesCM.Items.Add(removeSelectedToolStripMenuItem1);
                    SpawnableTypesCM.Show(Cursor.Position);
                }
                else if (e.Node.Tag is SpawnableType SpawnableType)
                {
                    SpawnableTypesCM.Items.Clear();
                    if (!SpawnableType.Items.OfType<spawnableTypesHoarder>().Any())
                        SpawnableTypesCM.Items.Add(addNewHoarderToolStripMenuItem);
                    if (!SpawnableType.Items.OfType<spawnableTypeDamage>().Any())
                        SpawnableTypesCM.Items.Add(addNewDamageToolStripMenuItem);
                    SpawnableTypesCM.Items.Add(addNewTagToolStripMenuItem);
                    SpawnableTypesCM.Items.Add(addNewCargoToolStripMenuItem1);
                    SpawnableTypesCM.Items.Add(addNewAttachmentToolStripMenuItem);
                    SpawnableTypesCM.Items.Add(removeSelectedToolStripMenuItem1);
                    SpawnableTypesCM.Show(Cursor.Position);
                }
                else if (e.Node.Tag is spawnableTypesHoarder)
                {
                    SpawnableTypesCM.Items.Clear();
                    SpawnableTypesCM.Items.Add(removeSelectedToolStripMenuItem1);
                    SpawnableTypesCM.Show(Cursor.Position);
                }
                else if (e.Node.Tag is spawnableTypeTag)
                {
                    SpawnableTypesCM.Items.Clear();
                    SpawnableTypesCM.Items.Add(removeSelectedToolStripMenuItem1);
                    SpawnableTypesCM.Show(Cursor.Position);
                }
                else if (e.Node.Tag is spawnableTypeDamage)
                {
                    SpawnableTypesCM.Items.Clear();
                    SpawnableTypesCM.Items.Add(removeSelectedToolStripMenuItem1);
                    SpawnableTypesCM.Show(Cursor.Position);
                }
                else if (e.Node.Tag is spawnableTypeCargo spawnableTypeCargo)
                {
                    SpawnableTypesCM.Items.Clear();
                    if (spawnableTypeCargo.damage == null)
                        SpawnableTypesCM.Items.Add(addNewDamageToolStripMenuItem);
                    SpawnableTypesCM.Items.Add(addNewItemToolStripMenuItem1);
                    SpawnableTypesCM.Items.Add(removeSelectedToolStripMenuItem1);
                    SpawnableTypesCM.Show(Cursor.Position);
                }
                else if (e.Node.Tag is spawnableTypeAttachment spawnableTypeAttachment)
                {
                    SpawnableTypesCM.Items.Clear();
                    if(spawnableTypeAttachment.damage == null)
                        SpawnableTypesCM.Items.Add(addNewDamageToolStripMenuItem);
                    SpawnableTypesCM.Items.Add(addNewItemToolStripMenuItem1);
                    SpawnableTypesCM.Items.Add(removeSelectedToolStripMenuItem1);
                    SpawnableTypesCM.Show(Cursor.Position);
                }
                else if (e.Node.Tag is spawnableTypeItem spawnableTypeItem)
                {
                    SpawnableTypesCM.Items.Clear();
                    if (spawnableTypeItem.damage == null)
                        SpawnableTypesCM.Items.Add(addNewDamageToolStripMenuItem);
                    SpawnableTypesCM.Items.Add(addNewCargoToolStripMenuItem1);
                    SpawnableTypesCM.Items.Add(addNewAttachmentToolStripMenuItem);
                    SpawnableTypesCM.Items.Add(removeSelectedToolStripMenuItem1);
                    SpawnableTypesCM.Show(Cursor.Position);
                }
            }
        }
        private void editPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTreeNode?.Tag is EconomySection section)
            {
                economyFile file = currentTreeNode.Parent?.Tag as economyFile;
                if (file != null && !file.IsModded)
                {
                    HandleVanillaeconomyEditRedirect(file, section, currentTreeNode.Text.Split(' ')[0]);
                    return;
                }
            }
            else if (currentTreeNode?.Tag is variablesVar variablevar)
            {
                globalsFile file = currentTreeNode.Parent?.Tag as globalsFile;
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
                economyFile file = currentTreeNode.Parent?.Tag as economyFile;
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
                        file.isDirty = true;

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
                            file.toDelete = true;
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
                globalsFile file = currentTreeNode.Parent?.Tag as globalsFile;
                if (file != null && file.IsModded)
                {
                    var removed = file.Data?.var?.Remove(variablevar) ?? false;

                    if (removed)
                    {
                        file.isDirty = true;

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
                            file.toDelete = true;
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
        private void DrawEffectSafePositions(cfgeffectareaConfig cfgeffectareaConfig)
        {
            foreach (cfgeffectareaSafePosition pos in cfgeffectareaConfig.Data._positions)
            {
                if (_selectedSafePosition == pos)
                {
                    var marker = new MarkerDrawable(new PointF((float)pos.X, (float)pos.Z), _mapControl.MapSize)
                    {
                        Color = Color.LimeGreen,
                        Radius = 8
                    };
                    _mapControl.RegisterDrawable(marker);
                }
                else
                {
                    var marker = new MarkerDrawable(new PointF((float)pos.X, (float)pos.Z), _mapControl.MapSize)
                    {
                        Color = Color.Red,
                        Radius = 8
                    };
                    _mapControl.RegisterDrawable(marker);
                }
            }
        }
        private void DrawEventSpawns(eventposdefEvent defevent)
        {
            foreach (eventposdefEventPos pos in defevent.pos)
            {
                if (_selectedEventPos == pos)
                {
                    var marker = new MarkerDrawable(new PointF((float)pos.x, (float)pos.z), _mapControl.MapSize)
                    {
                        Color = Color.LimeGreen,
                        Radius = 8
                    };
                    _mapControl.RegisterDrawable(marker);
                }
                else
                {
                    var marker = new MarkerDrawable(new PointF((float)pos.x, (float)pos.z), _mapControl.MapSize)
                    {
                        Color = Color.Red,
                        Radius = 8
                    };
                    _mapControl.RegisterDrawable(marker);
                }
            }
        }

        /// <summary>
        /// MapViewer clicks
        /// </summary>
        private eventposdefEventPos _selectedEventPos;
        private cfgeffectareaSafePosition _selectedSafePosition;
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
            _economyManager.cfgeventspawnsConfig.isDirty = true;

            _mapControl.ClearDrawables();

            eventposdefEvent defevent = currentTreeNode.Parent.Parent.Tag as eventposdefEvent;

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
            _economyManager.cfgeffectareaConfig.isDirty = true;

            _mapControl.ClearDrawables();

            cfgeffectareaConfig cfgeffectareaConfig = currentTreeNode.FindParentOfType<cfgeffectareaConfig>();
            DrawEffectSafePositions(cfgeffectareaConfig);
            currentTreeNode.Text = $"Position {currentTreeNode.Index+1} ({_selectedSafePosition.X}, {_selectedSafePosition.Z})";
        }

        /// <summary>
        /// Handle editing Economy values
        /// </summary>
        /// <param name="vanillaFile"></param>
        /// <param name="section"></param>
        /// <param name="sectionName"></param>
        private void HandleVanillaeconomyEditRedirect(economyFile vanillaFile, EconomySection section, string sectionName)
        {
            string newmodPath = "CustomMods\\Customdb";
            string newPath = EnsureModFolderAndGetPath(newmodPath, "Custom_" + vanillaFile.FileName);

            var existingFile = _economyManager.economyConfig.AllData
                .FirstOrDefault(f => f.FilePath.Equals(newPath, StringComparison.OrdinalIgnoreCase));

            economyFile newFile = existingFile ?? new economyFile(newPath)
            {
                IsModded = true,
                FileType = "economy",
                ModFolder = newmodPath,
                isDirty = true,
                Data = new economy()
            };

            if (existingFile == null)
            {
                _economyManager.eonomyCoreConfig.AddCe(newFile.ModFolder, newFile.FileName, "economy");
                _economyManager.eonomyCoreConfig.Save();
                _economyManager.economyConfig.AllData.Add(newFile);
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

                newFile.isDirty = true;
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
        private void HandleVanillaglobalsEditRedirect(globalsFile vanillaFile, variablesVar section, string sectionName)
        {
            string newmodPath = "CustomMods\\Customdb";
            string newPath = EnsureModFolderAndGetPath(newmodPath, "Custom_" + vanillaFile.FileName);

            // 2. Create new economyFile and add only selected section
            var existingFile = _economyManager.globalsConfig.AllData
                .FirstOrDefault(f => f.FilePath.Equals(newPath, StringComparison.OrdinalIgnoreCase));

            globalsFile newFile = existingFile ?? new globalsFile(newPath)
            {
                IsModded = true,
                FileType = "globals",
                ModFolder = newmodPath,
                isDirty = true,
                Data = new variables { var = new System.ComponentModel.BindingList<variablesVar>() }
            };

            if (existingFile == null)
            {
                _economyManager.eonomyCoreConfig.AddCe(newFile.ModFolder, newFile.FileName, "globals");
                _economyManager.eonomyCoreConfig.Save();
                _economyManager.globalsConfig.AllData.Add(newFile);
            }

            if (GetVariableByName(newFile.Data, sectionName) == null)
            {
                newFile.Data.var.Add(CloneVariable(section));
                newFile.isDirty = true;
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
                    }
                    ;
                    typefile.isDirty = true;

                    string relativePath = Path.GetRelativePath(_economyManager.basePath, typefile.FilePath);
                    EconomyTV.Nodes.Remove(currentTreeNode);
                    AddFileToTree(EconomyTV.Nodes[0], relativePath, typefile, CreateTypesfileNodes);
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
                    string typesfile = frm.typesname + ".xml";
                    string newPath = EnsureModFolderAndGetPath(newmodPath, typesfile);

                    BindingList<TypeEntry> types = frm._entries;
                    TypesFile newtypesfile = new TypesFile(newPath)
                    {
                        FileType = "types",
                        IsModded = true,
                        ModFolder = newmodPath
                    };
                    newtypesfile.CreateNew();
                    newtypesfile.Data.TypeList = types;
                    newtypesfile.isDirty = true;
                    _economyManager.eonomyCoreConfig.AddCe(newtypesfile.ModFolder, newtypesfile.FileName, "types");
                    _economyManager.TypesConfig.AllData.Add(newtypesfile);
                    string relativePath = Path.GetRelativePath(_economyManager.basePath, newtypesfile.FilePath);
                    AddFileToTree(EconomyTV.Nodes[0], relativePath, newtypesfile, CreateTypesfileNodes);
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
                typefile.isDirty = true;
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
                _typefile.isDirty = true;
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
                        ObjectSpawnerArr newobjectspawner = JsonSerializer.Deserialize<ObjectSpawnerArr>(File.ReadAllText(filePath));
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
                _economyManager.cfgeventspawnsConfig.isDirty = true;
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
                    eventfile.Data.AddnewEvent(neweventEvent);
                    eventfile.isDirty = true;
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
                    string typesfile = frm.typesname + ".xml";
                    string newPath = EnsureModFolderAndGetPath(newmodPath, typesfile);

                    EventsFile newEventsfile = new EventsFile(newPath)
                    {
                        FileType = "events",
                        IsModded = true,
                        ModFolder = newmodPath
                    };
                    newEventsfile.CreateNew();
                    newEventsfile.Data.@event = frm._entries;
                    newEventsfile.isDirty = true;
                    _economyManager.eonomyCoreConfig.AddCe(newEventsfile.ModFolder, newEventsfile.FileName, "events");
                    _economyManager.eventsConfig.AllData.Add(newEventsfile);
                    string relativePath = Path.GetRelativePath(_economyManager.basePath, newEventsfile.FilePath);
                    AddFileToTree(EconomyTV.Nodes[0], relativePath, newEventsfile, CreateEventNodes);
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
                        int count = 0;
                        foreach (EventsFile evfile in _economyManager.eventsConfig.AllData)
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
                            _economyManager.cfgeventspawnsConfig.isDirty = true;
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
                eventfile.isDirty = true;
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
                    foreach (EventsFile evfile in _economyManager.eventsConfig.AllData)
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
                            _economyManager.cfgeventspawnsConfig.isDirty = true;
                        }
                    }
                }
                RemoveTreeNodeAndEmptyParents(currentTreeNode);
                _eventfile.isDirty = true;
            }
        }

        /// <summary>
        /// Random Preset Right click methods
        /// </summary>
        private void addNewRandomPresetFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string newmodPath = "CustomMods\\Customdb";
            string newPath = EnsureModFolderAndGetPath(newmodPath, "Custom_cfgrandompresets.xml");
            if (File.Exists(newPath))
            {
                var result = MessageBox.Show(
                                $"Custom cfgrandomPreset allready exists.\nDo you wish to add another one?",
                                "File Exists",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question
                            );
                if (result == DialogResult.No) { return; }
                AddEventFile frm = new AddEventFile();
                frm.SetTitle = "Add new Random Preset File";
                frm.Button4visable = false;
                frm.StartPosition = FormStartPosition.CenterParent;
                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    newmodPath = frm.moddir.Replace("/", "\\");
                    string typesfile = frm.typesname + ".xml";
                    newPath = EnsureModFolderAndGetPath(newmodPath, typesfile);

                    cfgrandompresetsFile newpresetfile = new cfgrandompresetsFile(newPath)
                    {
                        FileType = "randompresets",
                        IsModded = true,
                        ModFolder = newmodPath
                    };
                    newpresetfile.CreateNew();
                    newpresetfile.isDirty = true;
                    _economyManager.eonomyCoreConfig.AddCe(newpresetfile.ModFolder, newpresetfile.FileName, "randompresets");
                    _economyManager.cfgrandompresetsConfig.AllData.Add(newpresetfile);
                    string relativePath = Path.GetRelativePath(_economyManager.basePath, newpresetfile.FilePath);
                    AddFileToTree(EconomyTV.Nodes[0], relativePath, newpresetfile, CreateRandomPresetsFileNodes);
                    savefiles();
                }
            }
            else
            {
                cfgrandompresetsFile newpresetfile = new cfgrandompresetsFile(newPath)
                {
                    FileType = "randompresets",
                    IsModded = true,
                    ModFolder = newmodPath
                };
                newpresetfile.CreateNew();
                newpresetfile.isDirty = true;
                _economyManager.eonomyCoreConfig.AddCe(newpresetfile.ModFolder, newpresetfile.FileName, "randompresets");
                _economyManager.cfgrandompresetsConfig.AllData.Add(newpresetfile);
                string relativePath = Path.GetRelativePath(_economyManager.basePath, newpresetfile.FilePath);
                AddFileToTree(EconomyTV.Nodes[0], relativePath, newpresetfile, CreateRandomPresetsFileNodes, true);
                savefiles();
            }
        }
        private void addNewAttchementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cfgrandompresetsFile rpf = currentTreeNode.FindParentOfType<cfgrandompresetsFile>();
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
            rpf.isDirty = true;
        }
        private void addNewCargoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cfgrandompresetsFile rpf = currentTreeNode.FindParentOfType<cfgrandompresetsFile>();
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
                rpf.isDirty = true;
            }
        }
        private void addNewItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cfgrandompresetsFile rpf = currentTreeNode.FindParentOfType<cfgrandompresetsFile>();
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

                rpf.isDirty = true;
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }
        private void removeSelectedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (currentTreeNode.Tag is cfgrandompresetsFile randompresetsfile)
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
                RemoveTreeNodeAndEmptyParents(currentTreeNode);
                randompresetsfile.isDirty = true;
                randompresetsfile.ToDelete = true;
            }
            else if (currentTreeNode.Tag is randompresetsAttachments randompresetsAttachments)
            {
                cfgrandompresetsFile _randompresetsfile = currentTreeNode.FindParentOfType<cfgrandompresetsFile>();
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
                RemoveTreeNodeAndEmptyParents(currentTreeNode);
                _randompresetsfile.isDirty = true;
            }
            else if (currentTreeNode.Tag is randompresetsCargo randompresetsCargo)
            {
                cfgrandompresetsFile _randompresetsfile = currentTreeNode.FindParentOfType<cfgrandompresetsFile>();
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
                RemoveTreeNodeAndEmptyParents(currentTreeNode);
                _randompresetsfile.isDirty = true;
            }
            else if (currentTreeNode.Tag is randompresetsItem randompresetsItem)
            {
                cfgrandompresetsFile _randompresetsfile = currentTreeNode.FindParentOfType<cfgrandompresetsFile>();
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
                _randompresetsfile.isDirty = true;
            }

        }

        /// <summary>
        /// Spawnable types right click methods
        /// </summary>
        private void addNewSpawnableTypesFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string newmodPath = "CustomMods\\Customdb";
            string newPath = EnsureModFolderAndGetPath(newmodPath, "Custom_cfgspawnabletypes.xml");
            if (File.Exists(newPath))
            {
                var result = MessageBox.Show(
                                $"Custom cfgspawnabletypes allready exists.\nDo you wish to add another one?",
                                "File Exists",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question
                            );
                if (result == DialogResult.No) { return; }
                AddEventFile frm = new AddEventFile();
                frm.SetTitle = "Add new Spawnable Types";
                frm.Button4visable = false;
                frm.StartPosition = FormStartPosition.CenterParent;
                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    newmodPath = frm.moddir.Replace("/", "\\");
                    string typesfile = frm.typesname + ".xml";
                    newPath = EnsureModFolderAndGetPath(newmodPath, typesfile);

                    cfgspawnabletypesFile newpresetfile = new cfgspawnabletypesFile(newPath)
                    {
                        FileType = "spawnabletypes",
                        IsModded = true,
                        ModFolder = newmodPath
                    };
                    newpresetfile.CreateNew();
                    newpresetfile.isDirty = true;
                    _economyManager.eonomyCoreConfig.AddCe(newpresetfile.ModFolder, newpresetfile.FileName, "spawnabletypes");
                    _economyManager.cfgspawnabletypesConfig.AllData.Add(newpresetfile);
                    string relativePath = Path.GetRelativePath(_economyManager.basePath, newpresetfile.FilePath);
                    AddFileToTree(EconomyTV.Nodes[0], relativePath, newpresetfile, CreateSpawnableTypesfileNodes);
                    savefiles();
                }
            }
            else
            {
                cfgspawnabletypesFile newpresetfile = new cfgspawnabletypesFile(newPath)
                {
                    FileType = "spawnabletypes",
                    IsModded = true,
                    ModFolder = newmodPath
                };
                newpresetfile.CreateNew();
                newpresetfile.isDirty = true;
                _economyManager.eonomyCoreConfig.AddCe(newpresetfile.ModFolder, newpresetfile.FileName, "spawnabletypes");
                _economyManager.cfgspawnabletypesConfig.AllData.Add(newpresetfile);
                string relativePath = Path.GetRelativePath(_economyManager.basePath, newpresetfile.FilePath);
                AddFileToTree(EconomyTV.Nodes[0], relativePath, newpresetfile, CreateSpawnableTypesfileNodes, true);
                savefiles();
            }
        }
        private void addNewSpawnableTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cfgspawnabletypesFile spf = currentTreeNode.FindParentOfType<cfgspawnabletypesFile>();
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
                spf.isDirty = true;
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }
        private void addNewHoarderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cfgspawnabletypesFile spf = currentTreeNode.FindParentOfType<cfgspawnabletypesFile>();
            if (currentTreeNode.Tag is SpawnableType type)
            {
                spawnableTypesHoarder newhoarder = new spawnableTypesHoarder();
                type.Items.Add(newhoarder);
                currentTreeNode.Nodes.Add(new TreeNode("hoarder")
                {
                    Tag = newhoarder
                });
                spf.isDirty = true;
            }
        }
        private void addNewTagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cfgspawnabletypesFile spf = currentTreeNode.FindParentOfType<cfgspawnabletypesFile>();
            if (currentTreeNode.Tag is SpawnableType type)
            {
                spawnableTypeTag newtag = new spawnableTypeTag();
                type.Items.Add(newtag);
                TreeNode newTagNode = new TreeNode(getTagString(newtag))
                {
                    Tag = newtag
                };
                currentTreeNode.Nodes.Add(newTagNode);
                spf.isDirty = true;
            }
        }
        private void addNewDamageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cfgspawnabletypesFile spf = currentTreeNode.FindParentOfType<cfgspawnabletypesFile>();
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
            else if (currentTreeNode.Tag is cfgspawnabletypesFile)
            {
                spf.Data.damage = newdamage;
            }
            TreeNode newdamageNode = CreateDamageNode(newdamage);
            currentTreeNode.Nodes.Insert(0, newdamageNode);
            spf.isDirty = true;
        }
        private void addNewItemToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cfgspawnabletypesFile spf = currentTreeNode.FindParentOfType<cfgspawnabletypesFile>();
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

                spf.isDirty = true;
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }
        private void addNewCargoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cfgspawnabletypesFile spf = currentTreeNode.FindParentOfType<cfgspawnabletypesFile>();
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
            spf.isDirty = true;
        }
        private void addNewAttachmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cfgspawnabletypesFile spf = currentTreeNode.FindParentOfType<cfgspawnabletypesFile>();
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
            spf.isDirty = true;
        }
        private void removeSelectedToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            if (currentTreeNode.Tag is cfgspawnabletypesFile spawnabletypesfile)
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
                spawnabletypesfile.isDirty = true;
                spawnabletypesfile.ToDelete = true;
            }
            else if (currentTreeNode.Tag is SpawnableType type)
            {
                cfgspawnabletypesFile _spawnabletypesfile = currentTreeNode.FindParentOfType<cfgspawnabletypesFile>();
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
                _spawnabletypesfile.isDirty = true;
            }
            else if (currentTreeNode.Tag is spawnableTypesHoarder Hoarder)
            {
                cfgspawnabletypesFile _spawnabletypesfile = currentTreeNode.FindParentOfType<cfgspawnabletypesFile>();
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
                _spawnabletypesfile.isDirty = true;
                
            }
            else if (currentTreeNode.Tag is spawnableTypeTag Tag)
            {
                cfgspawnabletypesFile _spawnabletypesfile = currentTreeNode.FindParentOfType<cfgspawnabletypesFile>();
                if (currentTreeNode.Parent.Tag is SpawnableType _type)
                {
                    _type.Items.Remove(Tag);
                    RemoveTreeNodeAndEmptyParents(currentTreeNode);
                    _spawnabletypesfile.isDirty = true;
                }
            }
            else if (currentTreeNode.Tag is spawnableTypeDamage damage)
            {
                cfgspawnabletypesFile _spawnabletypesfile = currentTreeNode.FindParentOfType<cfgspawnabletypesFile>();
                if (currentTreeNode.Parent.Tag is cfgspawnabletypesFile typefile)
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
                _spawnabletypesfile.isDirty = true;
            }
            else if (currentTreeNode.Tag is spawnableTypeCargo cargo)
            {
                cfgspawnabletypesFile _spawnabletypesfile = currentTreeNode.FindParentOfType<cfgspawnabletypesFile>();
                if (currentTreeNode.Parent.Tag is SpawnableType _type)
                {
                    _type.Items.Remove(cargo);
                }
                else if (currentTreeNode.Parent.Tag is spawnableTypeItem _Item)
                {
                    _Item.cargo.Remove(cargo);
                }
                RemoveTreeNodeAndEmptyParents(currentTreeNode);
                _spawnabletypesfile.isDirty = true;
            }
            else if (currentTreeNode.Tag is spawnableTypeAttachment attachment)
            {
                cfgspawnabletypesFile _spawnabletypesfile = currentTreeNode.FindParentOfType<cfgspawnabletypesFile>();
                if (currentTreeNode.Parent.Tag is SpawnableType _type)
                {
                    _type.Items.Remove(attachment);
                }
                else if (currentTreeNode.Parent.Tag is spawnableTypeItem _Item)
                {
                    _Item.attachments.Remove(attachment);
                }
                RemoveTreeNodeAndEmptyParents(currentTreeNode);
                _spawnabletypesfile.isDirty = true;
            }
            else if (currentTreeNode.Tag is spawnableTypeItem Item)
            {
                cfgspawnabletypesFile _spawnabletypesfile = currentTreeNode.FindParentOfType<cfgspawnabletypesFile>();
                if (currentTreeNode.Parent.Tag is spawnableTypeCargo _cargo)
                {
                    _cargo.item.Remove(Item);
                }
                else if (currentTreeNode.Parent.Tag is spawnableTypeAttachment _attachment)
                {
                    _attachment.item.Remove(Item);
                }
                RemoveTreeNodeAndEmptyParents(currentTreeNode);
                _spawnabletypesfile.isDirty = true;
            }
        }
    }

    [PluginInfo("Economy Manager", "EconomyPlugin")]
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
