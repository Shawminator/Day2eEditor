using Day2eEditor;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        private void EconomyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_plugin is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        private void EconomyForm_Load(object sender, EventArgs e)
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string imagePath = Path.Combine(appDirectory, _projectManager.CurrentProject.MapPath);
            Image mapImage = Image.FromFile(imagePath);
            _mapControl.LoadMap(mapImage, _projectManager.CurrentProject.MapSize);
            LoadTreeview();
        }
        private void LoadTreeview()
        {
            EconomyTV.Nodes.Clear();
            EconomyTV.Nodes.Add(CreateeconomyConfigNodes());
            EconomyTV.Nodes.Add(CreateGlobalsConfigNodes());
            EconomyTV.Nodes.Add(CreateGameplayConfigNodes());
            EconomyTV.Nodes.Add(CreateTypesConfigNodes());
            EconomyTV.Nodes.Add(CreateSpawnableTypesConfigNodes());
            EconomyTV.Nodes.Add(CreaterandomPresetsConfigNodes());
            EconomyTV.Nodes.Add(CreateeventConfigNodes());
        }
        //creating economy Nodes
        private TreeNode CreateeconomyConfigNodes()
        {
            TreeNode Typesroot = new TreeNode("Economy")
            {
                Tag = "EconomyRoot"
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
            EconomyRootNode.Nodes.Add(new TreeNode($"Dynamic")
            {
                Tag = ef.Data.dynamic
            });
            EconomyRootNode.Nodes.Add(new TreeNode($"Animals")
            {
                Tag = ef.Data.animals
            });
            EconomyRootNode.Nodes.Add(new TreeNode($"Zombies")
            {
                Tag = ef.Data.zombies
            });
            EconomyRootNode.Nodes.Add(new TreeNode($"Vehicles")
            {
                Tag = ef.Data.vehicles
            });
            EconomyRootNode.Nodes.Add(new TreeNode($"Randoms")
            {
                Tag = ef.Data.randoms
            });
            EconomyRootNode.Nodes.Add(new TreeNode($"Custom")
            {
                Tag = ef.Data.custom
            });
            EconomyRootNode.Nodes.Add(new TreeNode($"Building")
            {
                Tag = ef.Data.building
            });
            EconomyRootNode.Nodes.Add(new TreeNode($"Player")
            {
                Tag = ef.Data.player
            });

            return EconomyRootNode;
        }
        //creating globals Nodes
        private TreeNode CreateGlobalsConfigNodes()
        {
            TreeNode Typesroot = new TreeNode("Globals")
            {
                Tag = "GlobalsRoot"
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
            TreeNode GameplayRootNode = new TreeNode("GamePlay")
            {
                Tag = "CFGGameplayRoot"
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
                Tag = "TypeRoot"
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
                string cat = "other";
                if (type.Category != null)
                    cat = type.Category.Name;
                TreeNode typenode = new TreeNode(type.Name)
                {
                    Tag = type
                };
                if (!TypesrootNode.Nodes.ContainsKey(cat))
                {
                    TreeNode newcatnode = new TreeNode(cat)
                    {
                        Name = cat,
                        Tag = cat
                    };
                    TypesrootNode.Nodes.Add(newcatnode);
                }
                TypesrootNode.Nodes[cat].Nodes.Add(typenode);
            }
            return TypesrootNode;
        }
        //Creating spawnableTypes Nodes
        private TreeNode CreateSpawnableTypesConfigNodes()
        {
            TreeNode STypesroot = new TreeNode("Spawnable Types")
            {
                Tag = "SpawnableTypesRoot"
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
                Tag = "RandomPresetsRoot"
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
                Tag = "EventsRoot"
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

        private void ShowHandler(IUIHandler handler, object data, TreeNode node)
        {
            _currentHandler = handler;
            handler.LoadFromData(data, node);

            var ctrl = handler.GetControl();
            splitContainer1.Panel2.Controls.Clear(); // wherever you're hosting dynamic UI
            splitContainer1.Panel2.Controls.Add(ctrl);
            ctrl.Visible = true;
        }

        private void HideAllPanels()
        {
            splitContainer1.Panel2.Controls.Clear();
        }
        private void EconomyTV_AfterSelect(object sender, TreeViewEventArgs e)
        {
            HideAllPanels();
            EconomyTV.SelectedNode = e.Node;
            currentTreeNode = e.Node;
            if (e.Node.Tag is EconomySection)
            {

            }
            else if (e.Node.Tag is variablesVar varData)
            {
                ShowHandler(new VariablesVarControl(), varData, e.Node);
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
            else if (e.Node.Tag is TypesFile)
            {

            }
            else if (e.Node.Tag is TypeEntry)
            {

            }
            else if (e.Node.Tag is SpawnableType)
            {

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
