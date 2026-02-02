using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpansionPlugin
{
    public class ExpansionBookConfig : ExpansionBaseIConfigLoader<ExpansionBookSettings>
    {
        public const int CurrentVersion = 5;

        public ExpansionBookConfig(string path) : base(path)
        {
        }
        protected override ExpansionBookSettings CreateDefaultData()
        {
            return new ExpansionBookSettings(CurrentVersion);
        }

        protected override IEnumerable<string> ValidateData()
        {
            return Data.FixMissingOrInvalidFields();
        }
    }
    public class ExpansionBookSettings : IEquatable<ExpansionBookSettings>, IDeepCloneable<ExpansionBookSettings>
    {
        public int m_Version { get; set; }
        public int EnableStatusTab { get; set; }
        public int EnablePartyTab { get; set; }
        public int EnableServerInfoTab { get; set; }
        public int EnableServerRulesTab { get; set; }
        public int EnableTerritoryTab { get; set; }
        public int EnableBookMenu { get; set; }
        public int CreateBookmarks { get; set; }
        public int ShowHaBStats { get; set; }
        public int ShowPlayerFaction { get; set; }
        public BindingList<ExpansionBookRuleCategory> RuleCategories { get; set; }
        public int DisplayServerSettingsInServerInfoTab { get; set; }
        public BindingList<ExpansionBookSettingCategory> SettingCategories { get; set; }
        public BindingList<ExpansionBookLink> Links { get; set; }
        public BindingList<ExpansionBookDescriptionCategory> Descriptions { get; set; }
        public BindingList<ExpansionBookCraftingCategory> CraftingCategories { get; set; }
        public int EnableCraftingRecipesTab { get; set; }

        public ExpansionBookSettings()
        { }
        public ExpansionBookSettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
            EnableBookMenu = 1;
            CreateBookmarks = 0;
            EnableStatusTab = 1;
            ShowHaBStats = 1;
            EnablePartyTab = 1;
            EnableServerInfoTab = 1;
            EnableServerRulesTab = 1;
            EnableTerritoryTab = 1;
            DefaultRules();
            DisplayServerSettingsInServerInfoTab = 1;
            DefaultSettings();
            DefaultLinks();
            DefaultDescriptions();
            DefaultCraftingCategories();
            EnableCraftingRecipesTab = 0;
        }
        public void RenameRules()
        {
            for (int i = 0; i < RuleCategories.Count; i++)
            {
                RuleCategories[i].renamerules(i + 1);
            }
        }
        void DefaultRules()
        {
            RuleCategories = new BindingList<ExpansionBookRuleCategory>()
            {
                new ExpansionBookRuleCategory()
                {
                    CategoryName = "General",
                    Rules = new BindingList<ExpansionBookRule>()
                    {
                        new ExpansionBookRule()
                        {
                            RuleParagraph = "1.1.", RuleText = "Insults, discrimination, extremist and racist statements or texts are taboo."
                        },
                        new ExpansionBookRule()
                        {
                            RuleParagraph = "1.2.", RuleText = "We reserve the right to exclude people from the server who share extremist or racist ideas or who clearly disturb the server harmony."
                        },
                        new ExpansionBookRule()
                        {
                            RuleParagraph = "1.3.", RuleText = "Decisions of the team members, both the supporter and the admin are to be accepted without discussion."
                        },
                        new ExpansionBookRule()
                        {
                            RuleParagraph = "1.4.", RuleText = "Provocations and toxic behavior will not be tolerated and punished! Be friendly to fellow players and your team, both in chat and in voice!"
                        },new ExpansionBookRule()
                        {
                            RuleParagraph = "1.5.", RuleText = "The use of external programs, scripts and cheats is not tolerated and is punished with a permanent exclusion."
                        }
                    }
                }
            };
        }
        void DefaultSettings()
        {
            SettingCategories = new BindingList<ExpansionBookSettingCategory>()
            {
                new ExpansionBookSettingCategory(){
                    CategoryName = "Base-Building Settings",
                    Settings = new BindingList<ExpansionBookSetting>()
                    {
                        new ExpansionBookSetting()
                        {
                            SettingTitle = "Expansion.Settings.BaseBuilding.CanCraftVanillaBasebuilding",
                            SettingText = "",
                            SettingValue = ""
                        },
                        new ExpansionBookSetting()
                        {
                            SettingTitle = "Expansion.Settings.BaseBuilding.CanCraftExpansionBasebuilding",
                            SettingText = "",
                            SettingValue = ""
                        }
                    }
                },
                new ExpansionBookSettingCategory(){
                    CategoryName = "Raid Settings",
                    Settings = new BindingList<ExpansionBookSetting>()
                    {
                        new ExpansionBookSetting()
                        {
                            SettingTitle = "Expansion.Settings.Raid.CanRaidSafes",
                            SettingText = "",
                            SettingValue = ""
                        },
                        new ExpansionBookSetting()
                        {
                            SettingTitle = "Expansion.Settings.Raid.SafeExplosionDamageMultiplier",
                            SettingText = "",
                            SettingValue = ""
                        },
                        new ExpansionBookSetting()
                        {
                            SettingTitle = "Expansion.Settings.Raid.SafeProjectileDamageMultiplier",
                            SettingText = "",
                            SettingValue = ""
                        },
                        new ExpansionBookSetting()
                        {
                            SettingTitle = "Expansion.Settings.Raid.ExplosionTime",
                            SettingText = "",
                            SettingValue = ""
                        },
                        new ExpansionBookSetting()
                        {
                            SettingTitle = "Expansion.Settings.Raid.ExplosionDamageMultiplier",
                            SettingText = "",
                            SettingValue = ""
                        },
                        new ExpansionBookSetting()
                        {
                            SettingTitle = "Expansion.Settings.Raid.ProjectileDamageMultiplier",
                            SettingText = "",
                            SettingValue = ""
                        }
                    }
                },
                new ExpansionBookSettingCategory(){
                    CategoryName = "Territory Settings",
                    Settings = new BindingList<ExpansionBookSetting>()
                    {
                        new ExpansionBookSetting()
                        {
                            SettingTitle = "Expansion.Settings.Territory.TerritorySize",
                            SettingText = "",
                            SettingValue = ""
                        },
                        new ExpansionBookSetting()
                        {
                            SettingTitle = "Expansion.Settings.Territory.UseWholeMapForInviteList",
                            SettingText = "",
                            SettingValue = ""
                        }
                    }
                },
                new ExpansionBookSettingCategory(){
                    CategoryName = "Map Settings",
                    Settings = new BindingList<ExpansionBookSetting>()
                    {
                        new ExpansionBookSetting()
                        {
                            SettingTitle = "Expansion.Settings.Map.NeedGPSItemForKeyBinding",
                            SettingText = "",
                            SettingValue = ""
                        },
                        new ExpansionBookSetting()
                        {
                            SettingTitle = "Expansion.Settings.Map.NeedGPSItemForKeyBinding",
                            SettingText = "",
                            SettingValue = ""
                        }
                    }
                },
                new ExpansionBookSettingCategory(){
                    CategoryName = "Party Settings",
                    Settings = new BindingList<ExpansionBookSetting>()
                    {
                        new ExpansionBookSetting()
                        {
                            SettingTitle = "Expansion.Settings.Party.MaxMembersInParty",
                            SettingText = "",
                            SettingValue = ""
                        },
                        new ExpansionBookSetting()
                        {
                            SettingTitle = "Expansion.Settings.Party.UseWholeMapForInviteList",
                            SettingText = "",
                            SettingValue = ""
                        }
                    }
                }
            };
        }
        void DefaultLinks()
        {
            Links = new BindingList<ExpansionBookLink>()
            {
                new ExpansionBookLink()
                {
                    Name = "Homepage",
                    URL = "https://www.google.com/",
                    IconName = "Homepage",
                    IconColor = -14473430
                },
                new ExpansionBookLink()
                {
                    Name = "Feedback",
                    URL = "https://www.google.com/",
                    IconName = "Forums",
                    IconColor = -14473430
                },
                new ExpansionBookLink()
                {
                    Name = "Discord",
                    URL = "https://www.google.com/",
                    IconName = "Discord",
                    IconColor = -9270822
                },
                new ExpansionBookLink()
                {
                    Name = "Patreon",
                    URL = "https://www.patreon.com/dayzexpansion",
                    IconName = "Patreon",
                    IconColor = -432044
                },
                new ExpansionBookLink()
                {
                    Name = "Steam",
                    URL = "https://steamcommunity.com/sharedfiles/filedetails/?id=2116151222",
                    IconName = "Steam",
                    IconColor = -14006434
                },
                new ExpansionBookLink()
                {
                    Name = "Reddit",
                    URL = "https://www.reddit.com/r/ExpansionProject/",
                    IconName = "Reddit",
                    IconColor = -12386303
                },
                new ExpansionBookLink()
                {
                    Name = "GitHub",
                    URL = "https://github.com/salutesh/DayZ-Expansion-Scripts/wiki",
                    IconName = "GitHub",
                    IconColor = -16777216
                },
                new ExpansionBookLink()
                {
                    Name = "YouTube",
                    URL = "https://www.youtube.com/channel/UCZNgSvIEWfru963tQZOAVJg",
                    IconName = "YouTube",
                    IconColor = -65536
                },
                new ExpansionBookLink()
                {
                    Name = "Twitter",
                    URL = "https://twitter.com/DayZExpansion",
                    IconName = "Twitter",
                    IconColor = -14835214
                }
            };
        }
        void DefaultDescriptions()
        {
            Descriptions = new BindingList<ExpansionBookDescriptionCategory>()
            {
                 new ExpansionBookDescriptionCategory()
                 {
                     CategoryName = "General Info",
                     Descriptions = new BindingList<ExpansionBookDescription>()
                     {
                         new ExpansionBookDescription()
                         {
                             DescriptionText = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet."
                         },
                         new ExpansionBookDescription()
                         {
                             DescriptionText = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet."
                         },
                         new ExpansionBookDescription()
                         {
                             DescriptionText = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet."
                         }
                     }
                 },
                 new ExpansionBookDescriptionCategory()
                 {
                     CategoryName = "Mod Info",
                     Descriptions = new BindingList<ExpansionBookDescription>()
                     {
                         new ExpansionBookDescription()
                         {
                             DescriptionText = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet."
                         },
                         new ExpansionBookDescription()
                         {
                             DescriptionText = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet."
                         },
                         new ExpansionBookDescription()
                         {
                             DescriptionText = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet."
                         }
                     }
                 }
            };
        }
        void DefaultCraftingCategories()
        {
            CraftingCategories = new BindingList<ExpansionBookCraftingCategory>()
            {
                new ExpansionBookCraftingCategory()
                {
                    CategoryName = "Accessories",
                    Results = new BindingList<string>() {
                        "Armband_",
                        "Armband_White",
                        "EyePatch_Improvised"
                    }
                },
                new ExpansionBookCraftingCategory()
                {
                    CategoryName = "Backpacks",
                    Results = new BindingList<string>() {
                        "CourierBag",
                        "FurCourierBag",
                        "FurImprovisedBag",
                        "ImprovisedBag",
                        "LeatherSack_Brown"
                    }
                },
                new ExpansionBookCraftingCategory()
                {
                    CategoryName = "Base-Building",
                    Results = new BindingList<string>() {
                    "ExpansionBarbedWireKit",
                    "FenceKit",
                    "ExpansionFloorKit",
                    "ExpansionHelipadKit",
                    "ExpansionHescoKit",
                    "ExpansionRampKit",
                    "ShelterKit",
                    "ExpansionStairKit",
                    "TerritoryFlagKit",
                    "ExpansionWallKit",
                    "WatchtowerKit"
                    }
                },
                new ExpansionBookCraftingCategory()
                {
                    CategoryName = "Camouflage",
                    Results = new BindingList<string>() {
                    "Camonet",
                    "GhillieAtt_Tan",
                    "GhillieAtt_Mossy",
                    "GhillieAtt_Woodland",
                    "GhillieBushrag_Tan",
                    "GhillieBushrag_Mossy",
                    "GhillieBushrag_Woodland",
                    "GhillieHood_Tan",
                    "GhillieHood_Mossy",
                    "GhillieHood_Woodland",
                    "GhillieSuit_Tan",
                    "GhillieSuit_Mossy",
                    "GhillieSuit_Woodland",
                    "GhillieTop_Tan",
                    "GhillieTop_Mossy",
                    "GhillieTop_Woodland"
                    }
                },
                new ExpansionBookCraftingCategory()
                {
                    CategoryName = "Cooking",
                    Results = new BindingList<string>() {
                    "Fireplace",
                    "Firewood",
                    "HandDrillKit"
                    }
                },
                new ExpansionBookCraftingCategory()
                {
                    CategoryName = "Fishing",
                    Results = new BindingList<string>() {
                    "Bait",
                    "BoneBait",
                    "BoneHook",
                    "ImprovisedFishingRod"
                    }
                },
                 new ExpansionBookCraftingCategory()
                 {
                     CategoryName = "Food",
                     Results = new BindingList<string>() {
                        "CarpFilletMeat",
                        "MackerelFilletMeat",
                        "ExpansionMilkBottle",
                        "Potato",
                        "SlicedPumpkin"
                     }
                 },
                new ExpansionBookCraftingCategory(){
                    CategoryName = "Horticulture",
                    Results = new BindingList<string>() {
                        "PepperSeeds",
                        "PumpkinSeeds",
                        "TomatoSeeds",
                        "ZucchiniSeeds"
                    }
                },
                new ExpansionBookCraftingCategory()
                {
                    CategoryName = "Lights",
                    Results = new BindingList<string>() {
                        "LongTorch",
                        "Torch"
                    }
                },
                new ExpansionBookCraftingCategory()
                {
                    CategoryName = "Medical Supplies",
                    Results = new BindingList<string>() {
                        "BloodBagIV",
                        "SalineBagIV",
                        "Splint"
                    }
                },
                new ExpansionBookCraftingCategory()
                {
                    CategoryName = "Melee Weapons",
                    Results = new BindingList<string>() {
                        "NailedBaseballBat",
                        "StoneKnife"
                    }
                },
                new ExpansionBookCraftingCategory()
                {
                    CategoryName = "Storage",
                    Results = new BindingList<string>() {
                        "WoodenCrate"
                    }
                },
                new ExpansionBookCraftingCategory()
                {
                CategoryName = "Supplies",
                    Results = new BindingList<string>() {
                        "BoarPelt",
                        "BurlapSack",
                        "BurlapStrip",
                        "LongWoodenStick",
                        "ExpansionLumber1",
                        "ExpansionLumber1_5",
                        "ExpansionLumber3",
                        "Nails",
                        "Netting",
                        "Rag",
                        "Rope",
                        "SharpWoodenStick",
                        "SmallStone",
                        "TannedLeather",
                        "WoodenPlank",
                        "WoodenStick"
                    }
                },
                new ExpansionBookCraftingCategory()
                {
                     CategoryName = "Weapon Modifications",
                     Results = new BindingList<string>() {
                        "SawedoffIzh18Shotgun"
                     }
                },
                new ExpansionBookCraftingCategory()
                {
                     CategoryName = "Weapon Attachments",
                     Results = new BindingList<string>() {
                            "ImprovisedSuppressor"
                     }
                }
            };
        }
        public bool Equals(ExpansionBookSettings other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return m_Version == other.m_Version &&
                       EnableStatusTab == other.EnableStatusTab &&
                       EnablePartyTab == other.EnablePartyTab &&
                       EnableServerInfoTab == other.EnableServerInfoTab &&
                       EnableServerRulesTab == other.EnableServerRulesTab &&
                       EnableTerritoryTab == other.EnableTerritoryTab &&
                       EnableBookMenu == other.EnableBookMenu &&
                       CreateBookmarks == other.CreateBookmarks &&
                       ShowHaBStats == other.ShowHaBStats &&
                       ShowPlayerFaction == other.ShowPlayerFaction &&
                       DisplayServerSettingsInServerInfoTab == other.DisplayServerSettingsInServerInfoTab &&
                       EnableCraftingRecipesTab == other.EnableCraftingRecipesTab &&
                       RuleCategories.SequenceEqual(other.RuleCategories) &&
                       SettingCategories.SequenceEqual(other.SettingCategories) &&
                       Links.SequenceEqual(other.Links) &&
                       Descriptions.SequenceEqual(other.Descriptions) &&
                       CraftingCategories.SequenceEqual(other.CraftingCategories);
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionBookSettings);
        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();
            if (m_Version != ExpansionBookConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionBookConfig.CurrentVersion}");
                m_Version = ExpansionBookConfig.CurrentVersion;
            }
            if (RuleCategories == null)
            {
                DefaultRules();
                fixes.Add("Initialized and created default RuleCategories");
            }

            if (SettingCategories == null)
            {
                DefaultSettings();
                fixes.Add("Initialized and created default SettingCategories");
            }

            if (Links == null)
            {
                DefaultLinks();
                fixes.Add("Initialized and created default Links");
            }

            if (Descriptions == null)
            {
                DefaultDescriptions();
                fixes.Add("Initialized and created default Descriptions");
            }

            if (CraftingCategories == null)
            {
                DefaultCraftingCategories();
                fixes.Add("Initialized and created default CraftingCategories");
            }

            if (EnableStatusTab != 0 && EnableStatusTab != 1)
            {
                EnableStatusTab = 1;
                fixes.Add("Corrected EnableStatusTab");
            }

            if (EnablePartyTab != 0 && EnablePartyTab != 1)
            {
                EnablePartyTab = 1;
                fixes.Add("Corrected EnablePartyTab");
            }

            if (EnableServerInfoTab != 0 && EnableServerInfoTab != 1)
            {
                EnableServerInfoTab = 1;
                fixes.Add("Corrected EnableServerInfoTab");
            }

            if (EnableServerRulesTab != 0 && EnableServerRulesTab != 1)
            {
                EnableServerRulesTab = 1;
                fixes.Add("Corrected EnableServerRulesTab");
            }

            if (EnableTerritoryTab != 0 && EnableTerritoryTab != 1)
            {
                EnableTerritoryTab = 1;
                fixes.Add("Corrected EnableTerritoryTab");
            }

            if (EnableBookMenu != 0 && EnableBookMenu != 1)
            {
                EnableBookMenu = 1;
                fixes.Add("Corrected EnableBookMenu");
            }

            if (CreateBookmarks != 0 && CreateBookmarks != 1)
            {
                CreateBookmarks = 1;
                fixes.Add("Corrected CreateBookmarks");
            }

            if (ShowHaBStats != 0 && ShowHaBStats != 1)
            {
                ShowHaBStats = 1;
                fixes.Add("Corrected ShowHaBStats");
            }

            if (ShowPlayerFaction != 0 && ShowPlayerFaction != 1)
            {
                ShowPlayerFaction = 1;
                fixes.Add("Corrected ShowPlayerFaction");
            }

            if (DisplayServerSettingsInServerInfoTab != 0 && DisplayServerSettingsInServerInfoTab != 1)
            {
                DisplayServerSettingsInServerInfoTab = 1;
                fixes.Add("Corrected DisplayServerSettingsInServerInfoTab");
            }

            if (EnableCraftingRecipesTab != 0 && EnableCraftingRecipesTab != 1)
            {
                EnableCraftingRecipesTab = 1;
                fixes.Add("Corrected EnableCraftingRecipesTab");
            }
            
            return fixes;
        }
        public ExpansionBookSettings Clone()
        {
            return new ExpansionBookSettings()
            {
                m_Version = this.m_Version,
                EnableStatusTab = this.EnableStatusTab,
                EnablePartyTab = this.EnablePartyTab,
                EnableServerInfoTab = this.EnableServerInfoTab,
                EnableServerRulesTab = this.EnableServerRulesTab,
                EnableTerritoryTab = this.EnableTerritoryTab,
                EnableBookMenu = this.EnableBookMenu,
                CreateBookmarks = this.CreateBookmarks,
                ShowHaBStats = this.ShowHaBStats,
                ShowPlayerFaction = this.ShowPlayerFaction,
                DisplayServerSettingsInServerInfoTab = this.DisplayServerSettingsInServerInfoTab,
                EnableCraftingRecipesTab = this.EnableCraftingRecipesTab,

                RuleCategories = new BindingList<ExpansionBookRuleCategory>(
                    this.RuleCategories?.Select(rc => rc.Clone()).ToList() ?? new List<ExpansionBookRuleCategory>()),

                SettingCategories = new BindingList<ExpansionBookSettingCategory>(
                    this.SettingCategories?.Select(sc => sc.CLone()).ToList() ?? new List<ExpansionBookSettingCategory>()),

                Links = new BindingList<ExpansionBookLink>(
                    this.Links?.Select(l => l.Clone()).ToList() ?? new List<ExpansionBookLink>()),

                Descriptions = new BindingList<ExpansionBookDescriptionCategory>(
                    this.Descriptions?.Select(dc => dc.Clone()).ToList() ?? new List<ExpansionBookDescriptionCategory>()),

                CraftingCategories = new BindingList<ExpansionBookCraftingCategory>(
                    this.CraftingCategories?.Select(cc => cc.Clone()).ToList() ?? new List<ExpansionBookCraftingCategory>())
            };
        }
    }
    public class ExpansionBookRuleCategory
    {
        public string CategoryName { get; set; }
        public BindingList<ExpansionBookRule> Rules { get; set; }

        public ExpansionBookRuleCategory()
        {
            Rules = new BindingList<ExpansionBookRule>();
        }
        public override string ToString()
        {
            return CategoryName;
        }
        public void renamerules(int i)
        {
            for (int j = 0; j < Rules.Count; j++)
            {
                Rules[j].RuleParagraph = i.ToString() + "." + (j + 1).ToString();
            }
        }
        public override bool Equals(object obj)
        {
            if (obj is ExpansionBookRuleCategory other)
            {
                return CategoryName == other.CategoryName &&
                       Rules.SequenceEqual(other.Rules);
            }
            return false;
        }
        public ExpansionBookRuleCategory Clone()
        {
            return new ExpansionBookRuleCategory()
            {
                CategoryName = this.CategoryName,
                Rules = new BindingList<ExpansionBookRule>(
                    this.Rules?.Select(r => r.CLone()).ToList() ?? new List<ExpansionBookRule>())
            };
        }
    }
    public class ExpansionBookRule
    {
        public string RuleParagraph { get; set; }
        public string RuleText { get; set; }

        public ExpansionBookRule()
        {
            RuleParagraph = "";
            RuleText = "";
        }
        public override string ToString()
        {
            return RuleParagraph;
        }
        public override bool Equals(object obj)
        {
            if (obj is ExpansionBookRule other)
            {
                return RuleParagraph == other.RuleParagraph &&
                       RuleText == other.RuleText;
            }
            return false;
        }
        public ExpansionBookRule CLone()
        {
            return new ExpansionBookRule()
            {
                RuleParagraph = this.RuleParagraph,
                RuleText = this.RuleText
            };
        }

    }
    public class ExpansionBookSettingCategory
    {
        public string CategoryName { get; set; }
        public BindingList<ExpansionBookSetting> Settings { get; set; }

        public ExpansionBookSettingCategory()
        {
            Settings = new BindingList<ExpansionBookSetting>();
        }
        public override string ToString()
        {
            return CategoryName;
        }
        public override bool Equals(object obj)
        {
            if (obj is ExpansionBookSettingCategory other)
            {
                return CategoryName == other.CategoryName &&
                       Settings.SequenceEqual(other.Settings);
            }
            return false;
        }
        public ExpansionBookSettingCategory CLone()
        {
            return new ExpansionBookSettingCategory()
            {
                CategoryName = this.CategoryName,
                Settings = new BindingList<ExpansionBookSetting>(
                    this.Settings?.Select(r => new ExpansionBookSetting
                    {
                        SettingTitle = r.SettingTitle,
                        SettingText = r.SettingText,
                        SettingValue = r.SettingValue
                    }).ToList() ?? new List<ExpansionBookSetting>())
            };
        }

    }
    public class ExpansionBookSetting
    {
        public string SettingTitle { get; set; }
        public string SettingText { get; set; }
        public string SettingValue { get; set; }

        public ExpansionBookSetting()
        {

        }
        public override bool Equals(object obj)
        {
            if (obj is ExpansionBookSetting other)
            {
                return SettingTitle == other.SettingTitle &&
                       SettingText == other.SettingText &&
                       SettingValue == other.SettingValue;
            }
            return false;
        }
        public ExpansionBookSetting Clone()
        {
            return new ExpansionBookSetting()
            {
                SettingTitle = this.SettingTitle,
                SettingText = this.SettingText,
                SettingValue= this.SettingValue
            };
        }

    }
    public class ExpansionBookLink
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public string IconName { get; set; }
        public int IconColor { get; set; }

        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(object obj)
        {
            if (obj is ExpansionBookLink other)
            {
                return Name == other.Name &&
                       URL == other.URL &&
                       IconName == other.IconName &&
                       IconColor == other.IconColor;
            }
            return false;
        }
        public ExpansionBookLink Clone()
        {
            return new ExpansionBookLink()
            {
                Name = this.Name,
                URL = this.URL,
                IconName = this.IconName,
                IconColor = this.IconColor
            };
        }
    }
    public class ExpansionBookDescriptionCategory
    {
        public string CategoryName { get; set; }
        public BindingList<ExpansionBookDescription> Descriptions { get; set; }

        public ExpansionBookDescriptionCategory()
        {
            Descriptions = new BindingList<ExpansionBookDescription>();
        }
        public override string ToString()
        {
            return CategoryName;
        }
        public override bool Equals(object obj)
        {
            if (obj is ExpansionBookDescriptionCategory other)
            {
                return CategoryName == other.CategoryName &&
                       Descriptions.SequenceEqual(other.Descriptions);
            }
            return false;
        }
        public ExpansionBookDescriptionCategory Clone()
        {
            return new ExpansionBookDescriptionCategory()
            {
                CategoryName = this.CategoryName,
                Descriptions = new BindingList<ExpansionBookDescription>(
                    this.Descriptions.Select(d => d.Clone()).ToList()
                )
            };
        }

    }
    public class ExpansionBookDescription
    {
        public string DescriptionText { get; set; }

        [JsonIgnore]
        public string DTName { get; set; }

        public override string ToString()
        {
            return DTName;
        }
        public override bool Equals(object obj)
        {
            if (obj is ExpansionBookDescription other)
            {
                return DescriptionText == other.DescriptionText &&
                       DTName == other.DTName;
            }
            return false;
        }
        public ExpansionBookDescription Clone()
        {
            return new ExpansionBookDescription()
            {
                DescriptionText = this.DescriptionText,
                DTName= this.DTName
            };
        }

    }
    public class ExpansionBookCraftingCategory
    {
        public string CategoryName { get; set; }
        public BindingList<string> Results { get; set; }

        public ExpansionBookCraftingCategory()
        {
            Results = new BindingList<string>();
        }

        public override string ToString()
        {
            return CategoryName;
        }

        public override bool Equals(object obj)
        {
            if (obj is ExpansionBookCraftingCategory other)
            {
                return CategoryName == other.CategoryName &&
                       Results.SequenceEqual(other.Results);
            }
            return false;
        }
        public ExpansionBookCraftingCategory Clone()
        {
            return new ExpansionBookCraftingCategory()
            {
                CategoryName = this.CategoryName,
                Results = new BindingList<string>(this.Results.ToList())
            };
        }
    }
}
