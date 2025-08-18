using Day2eEditor;

namespace EconomyPlugin
{
    public partial class EconomyForm : Form
    {
        private IUIHandler _currentHandler;
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
        private void AddFileToTree<TFile>(TreeNode parentNode, string relativePath, TFile file, Func<TFile, TreeNode> createFileNode)
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

            // Gameplay config
            string _relativePath = Path.GetRelativePath(_economyManager.basePath, _economyManager.CFGGameplayConfig.FilePath);
            AddFileToTree(rootNode, _relativePath, _economyManager.CFGGameplayConfig.Data, CreateGameplayConfigNodes);

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


            //rootNode.Nodes.Add(CreateeconomyConfigNodes());
            //rootNode.Nodes.Add(CreateGlobalsConfigNodes());
            //rootNode.Nodes.Add(CreateGameplayConfigNodes());
            //rootNode.Nodes.Add(CreateTypesConfigNodes());
            //rootNode.Nodes.Add(CreateSpawnableTypesConfigNodes());
            //rootNode.Nodes.Add(CreaterandomPresetsConfigNodes());
            //rootNode.Nodes.Add(CreateeventConfigNodes());
            EconomyTV.Nodes.Add(rootNode);
        }
        //creating economy Nodes
        private TreeNode CreateeconomyConfigNodes()
        {
            TreeNode Typesroot = new TreeNode("Economy")
            {
                Tag = _economyManager.economyConfig
            };
            foreach (economyFile ef in _economyManager.economyConfig.AllData)
            {
                Typesroot.Nodes.Add(CreateEconomyfileNodes(ef));
            }
            return Typesroot;
        }
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
        private TreeNode CreateGlobalsConfigNodes()
        {
            TreeNode Typesroot = new TreeNode("Globals")
            {
                Tag = _economyManager.globalsConfig
            };
            foreach (globalsFile gf in _economyManager.globalsConfig.AllData)
            {
                Typesroot.Nodes.Add(CreateGlobalsfileNodes(gf));
            }
            return Typesroot;
        }
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
        private TreeNode CreateGameplayConfigNodes()
        {
            return CreateGameplayConfigNodes(_economyManager.CFGGameplayConfig.Data);
        }
        private TreeNode CreateGameplayConfigNodes(cfggameplay ganmeplay)
        {
            TreeNode GameplayRootNode = new TreeNode("GamePlay")
            {
                Tag = _economyManager.CFGGameplayConfig
            };
            GameplayRootNode.Nodes.Add(new TreeNode($"Version:{_economyManager.CFGGameplayConfig.Data.version.ToString()}")
            {
                Tag = _economyManager.CFGGameplayConfig.Data.version
            });
            GameplayRootNode.Nodes.Add(new TreeNode($"GeneralData")
            {
                Tag = _economyManager.CFGGameplayConfig.Data.GeneralData
            });
            TreeNode PlayerDataNodes = new TreeNode($"PlayerData")
            {
                Tag = _economyManager.CFGGameplayConfig.Data.PlayerData
            };
            TreeNode spawnGearNodes = new TreeNode("spawnGearPresets")
            {
                Tag = _economyManager.CFGGameplayConfig.Data.PlayerData.spawnGearPresetFiles
            };
            foreach (string spawnfile in _economyManager.CFGGameplayConfig.Data.PlayerData.spawnGearPresetFiles)
            {
                SpawnGearPresetFiles spawnGearPresetFiles = _economyManager.CFGGameplayConfig.GetSpawnGearPreset(spawnfile);
                spawnGearNodes.Nodes.Add(CreateSpawnGearFilesNodes(spawnGearPresetFiles));
            }
            PlayerDataNodes.Nodes.Add(spawnGearNodes);
            GameplayRootNode.Nodes.Add(PlayerDataNodes);
            GameplayRootNode.Nodes.Add(new TreeNode($"WorldsData")
            {
                Tag = _economyManager.CFGGameplayConfig.Data.WorldsData
            });
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
                Tag = "SpawnGearPresetFilesParent"
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
        //Creating Types Nodes
        private TreeNode CreateTypesConfigNodes()
        {
            TreeNode Typesroot = new TreeNode("Types")
            {
                Tag = _economyManager.TypesConfig
            };
            foreach (TypesFile tf in _economyManager.TypesConfig.AllData)
            {
                Typesroot.Nodes.Add(CreateTypesfileNodes(tf));
            }
            return Typesroot;
        }
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
        private TreeNode CreateSpawnableTypesConfigNodes()
        {
            TreeNode STypesroot = new TreeNode("Spawnable Types")
            {
                Tag = _economyManager.cfgspawnabletypesConfig
            };
            foreach (cfgspawnabletypesFile stf in _economyManager.cfgspawnabletypesConfig.AllData)
            {
                STypesroot.Nodes.Add(CreateSpawnableTypesfileNodes(stf));
            }
            return STypesroot;
        }
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
        private TreeNode CreaterandomPresetsConfigNodes()
        {
            TreeNode RandomPresetsroot = new TreeNode("Random Presets")
            {
                Tag = _economyManager.cfgrandompresetsConfig
            };
            foreach (cfgrandompresetsFile stf in _economyManager.cfgrandompresetsConfig.AllData)
            {
                RandomPresetsroot.Nodes.Add(CreateRandomPresetsFileNodes(stf));
            }
            return RandomPresetsroot;
        }
        private TreeNode CreateRandomPresetsFileNodes(cfgrandompresetsFile rpc)
        {
            TreeNode ConfigRoot = new TreeNode(rpc.FileName)
            {
                Tag = rpc
            };
            TreeNode Attatchnode = new TreeNode("Attachments")
            {
                Tag = "Attachments"
            };
            TreeNode CargoNode = new TreeNode("Cargo")
            {
                Tag = "Cargo"
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
                return "Name = " + RPA.name + " Chance = " + RPA.chance;
            }
            else if (Preset is randompresetsCargo)
            {
                randompresetsCargo RPC = Preset as randompresetsCargo;
                return "Name = " + RPC.name + " Chance = " + RPC.chance;
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
            return "name = " + item.name + " chance = " + item.chance;
        }
        //Creating Event and event spawn Nodes
        private TreeNode CreateeventConfigNodes()
        {
            TreeNode rootNode = new TreeNode("Events")
            {
                Tag = _economyManager.eventsConfig
            };
            foreach (EventsFile ef in _economyManager.eventsConfig.AllData)
            {
                rootNode.Nodes.Add(CreateEventNodes(ef));
            }
            return rootNode;
        }
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
        #endregion loading treeview

        void ShowHandler<T>(T handler, object primaryData, List<TreeNode> selectedNodes)
        where T : IUIHandler
        {
            if (_currentHandler != null && _currentHandler.GetType() == typeof(T))
            {
                _currentHandler.LoadFromData(primaryData, selectedNodes);
                return;
            }

            // Dispose old control if needed
            if (_currentHandler != null)
            {
                var oldControl = _currentHandler.GetControl();
                splitContainer1.Panel2.Controls.Remove(oldControl);
                oldControl.Dispose();
            }

            _currentHandler = handler;
            handler.LoadFromData(primaryData, selectedNodes);

            var ctrl = handler.GetControl();
            splitContainer1.Panel2.Controls.Add(ctrl);
            ctrl.BringToFront();
            ctrl.Visible = true;
        }
        private void HideAllPanels()
        {
            foreach (Control ctrl in splitContainer1.Panel2.Controls.OfType<Control>().ToList())
            {
                if (ctrl != _mapControl)
                {
                    splitContainer1.Panel2.Controls.Remove(ctrl);
                }
            }
        }
        private void EconomyTV_AfterSelect(object sender, TreeViewEventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                _mapControl.Visible = false;
                //HideAllPanels();
                //EconomyTV.SelectedNode = e.Node;
                currentTreeNode = e.Node;

                var selectedNodes = EconomyTV.SelectedNodes.Cast<TreeNode>().ToList();
                if (e.Node.Tag is EconomySection economydata)
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
                else if (e.Node.Tag is Generaldata)
                {

                }
                else if (e.Node.Tag is Playerdata)
                {

                }
                else if (e.Node.Tag is Worldsdata)
                {

                }
                else if (e.Node.Tag is Basebuildingdata)
                {

                }
                else if (e.Node.Tag is Uidata)
                {

                }
                else if (e.Node.Tag is CFGGameplayMapData)
                {

                }
                else if (e.Node.Tag is VehicleData)
                {

                }
                else if (e.Node.Tag is TypesFile typefile)
                {
                    ShowHandler(new TypesCollectionControl(), typefile, selectedNodes);
                }
                else if (e.Node.Tag is Category cat)
                {
                    if(e.Node.Parent == null) { return;}
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
                else if (e.Node.Tag is SpawnableType)
                {

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
                if(e.Node.Tag.ToString() == "RootNode")
                {
                    addNewTypesToolStripMenuItem.Visible = true;
                    removeSelectedToolStripMenuItem.Visible = false;
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
                else if (e.Node.Tag is TypeEntry typeentry)
                {
                    
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

                            _economyManager.economyConfig.AllData.Remove(file);

                            // Delete file on disk
                            string absFolderPath = Path.Combine(_economyManager.basePath, folderPathRel.Replace("/", "\\"));
                            string absFilePath = Path.Combine(absFolderPath, fileName);
                            if (File.Exists(absFilePath))
                            {
                                File.Delete(absFilePath);
                            }

                            // Delete empty directories if needed
                            if (deleteDirectory)
                            {
                                DeleteEmptyFoldersUpToBase(absFolderPath, _economyManager.basePath);
                            }

                            // Remove file node and clean up empty parent nodes
                            RemoveTreeNodeAndEmptyParents(fileNode);
                        }
                        else
                        {
                            // Just remove the variable node
                            currentTreeNode.Remove();
                        }

                        MessageBox.Show(
                             $"Removed section \"{section}\" from {file.FileName}.",
                            "Section Removed",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
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

                            _economyManager.globalsConfig.AllData.Remove(file);

                            // Delete file on disk
                            string absFolderPath = Path.Combine(_economyManager.basePath, folderPathRel.Replace("/", "\\"));
                            string absFilePath = Path.Combine(absFolderPath, fileName);
                            if (File.Exists(absFilePath))
                            {
                                File.Delete(absFilePath);
                            }

                            // Delete empty directories if needed
                            if (deleteDirectory)
                            {
                                DeleteEmptyFoldersUpToBase(absFolderPath, _economyManager.basePath);
                            }

                            // Remove file node and clean up empty parent nodes
                            RemoveTreeNodeAndEmptyParents(fileNode);
                        }
                        else
                        {
                            // Just remove the variable node
                            currentTreeNode.Remove();
                        }

                        MessageBox.Show(
                            $"Removed variable \"{variablevar.name}\" from {file.FileName}.",
                            "Variable Removed",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
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
        private void DeleteEmptyFoldersUpToBase(string startDir, string stopDir)
        {
            DirectoryInfo current = new DirectoryInfo(startDir);
            DirectoryInfo stop = new DirectoryInfo(stopDir);

            while (current.Exists && !IsSameDirectory(current, stop))
            {
                if (!Directory.EnumerateFileSystemEntries(current.FullName).Any())
                {
                    current.Delete();
                    current = current.Parent;
                }
                else
                {
                    break;
                }
            }
        }

        private bool IsSameDirectory(DirectoryInfo a, DirectoryInfo b)
        {
            return string.Equals(
                Path.GetFullPath(a.FullName).TrimEnd(Path.DirectorySeparatorChar),
                Path.GetFullPath(b.FullName).TrimEnd(Path.DirectorySeparatorChar),
                StringComparison.OrdinalIgnoreCase
            );
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
                _economyManager.globalsConfig.AllData.Add(newFile);
            }

            if (GetVariableByName(newFile.Data, sectionName) == null)
            {
                newFile.Data.var.Add(CloneVariable(section));
                newFile.isDirty = true;
            }

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

        private void addNewTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTypes frm = new AddTypes();
            frm.StartPosition = FormStartPosition.CenterParent;
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.Cancel)
            {
                frm.Close();
            }
        }

        private void removeSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TypesFile typefile = currentTreeNode.Parent.Parent.Tag as TypesFile;
            if (typefile.IsModded == false)
            {
                var result = MessageBox.Show(
                            $"Type entry(s) is in the vanilla types file, are you sure you want to delete it?",
                            "Vanilla Types File",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question
                        );
                if (result == DialogResult.No) { return; };
            }
            var selectedNodes = EconomyTV.SelectedNodes.Cast<TreeNode>().ToList();
            foreach (var node in selectedNodes)
            {
                TypeEntry typeeentry = node.Tag as TypeEntry;
                typefile.Data.TypeList.Remove(typeeentry);
                var parent = node.Parent;
                EconomyTV.Nodes.Remove(node);
                if(parent.Nodes.Count == 0)
                {
                    EconomyTV.Nodes.Remove(parent);
                }
            }
            typefile.isDirty = true;
        }

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
