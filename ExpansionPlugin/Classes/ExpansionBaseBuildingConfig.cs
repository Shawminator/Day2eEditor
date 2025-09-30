using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionBaseBuildingConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path); 
        public string FilePath => _path;
        public ExpansionBaseBuildingSettings Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }

        const int CurrentVersion = 5;

        public ExpansionBaseBuildingConfig(string path)
        {
            _path = path;
        }

        public void Load()
        {
            Data = AppServices.GetRequired<FileService>().LoadOrCreateJson<ExpansionBaseBuildingSettings>(
                _path,
                createNew: () => new ExpansionBaseBuildingSettings(CurrentVersion),
                onAfterLoad: cfg => { /* optional: do something after load */ },
                onError: ex =>
                {
                    HasErrors = true;
                    Console.WriteLine(
                        "Error in " + Path.GetFileName(_path) + "\n" +
                        ex.Message + "\n" +
                        ex.InnerException?.Message + "\n"
                    );
                    Errors.Add("Error in " + Path.GetFileName(_path) + "\n" +
                        ex.Message + "\n" +
                        ex.InnerException?.Message);
                },
                configName: "ExpansionBaseBuilding"
            );
        }
        public IEnumerable<string> Save()
        {
            if (isDirty)
            {
                AppServices.GetRequired<FileService>().SaveJson(_path, Data);
                isDirty = false;
                return new[] { Path.GetFileName(_path) };
            }

            return Array.Empty<string>();
        }

        public bool needToSave()
        {
            return isDirty;
        }
        public bool checkver()
        {
            if (Data.m_Version != CurrentVersion)
            {
                Data.m_Version = CurrentVersion;
                isDirty = true;
                return true;
            }
            return false;
        }
    }
    public enum ExpansionCodelockAttachMode
    {
        ExpansionOnly = 0,
        ExpansionAndFence,
        ExpansionAndFenceAndTents,
        ExpansionAndTents
    }
    public enum ExpansionFlagMenuMode
    {
        Disabled = 0,
        Enabled,
        NoFlagChoice
    };
    public enum ExpansionDismantleFlagMode
    {
        TerritoryMembersWithHands = -1,
        AnyoneWithHands = 0,
        AnyoneWithTools = 1
    };
    public class ExpansionBaseBuildingSettings
    {
        public int m_Version { get; set; }  // Current Version is 3
        public int CanBuildAnywhere { get; set; }
        public int AllowBuildingWithoutATerritory { get; set; }
        public BindingList<string> DeployableOutsideATerritory { get; set; }
        public BindingList<string> DeployableInsideAEnemyTerritory { get; set; }
        public int CanCraftVanillaBasebuilding { get; set; }
        public int CanCraftExpansionBasebuilding { get; set; }
        public int DestroyFlagOnDismantle { get; set; }
        public int DismantleOutsideTerritory { get; set; }
        public int DismantleInsideTerritory { get; set; }
        public int DismantleAnywhere { get; set; }
        public int CodelockActionsAnywhere { get; set; }
        public int CodeLockLength { get; set; }
        public int DoDamageWhenEnterWrongCodeLock { get; set; }
        public decimal DamageWhenEnterWrongCodeLock { get; set; }
        public int RememberCode { get; set; }
        public int CanCraftTerritoryFlagKit { get; set; }
        public int SimpleTerritory { get; set; }
        public int AutomaticFlagOnCreation { get; set; }
        public int GetTerritoryFlagKitAfterBuild { get; set; }
        public string BuildZoneRequiredCustomMessage { get; set; }
        public BindingList<ExpansionBuildNoBuildZone> Zones { get; set; }
        public int ZonesAreNoBuildZones { get; set; }
        public int CodelockAttachMode { get; set; }
        public int DismantleFlagMode { get; set; }
        public int FlagMenuMode { get; set; }
        public int PreventItemAccessThroughObstructingItems { get; set; }
        public int EnableVirtualStorage { get; set; }
        public BindingList<string> VirtualStorageExcludedContainers { get; set; }

        public ExpansionBaseBuildingSettings()
        {

        }
        public ExpansionBaseBuildingSettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
            CanBuildAnywhere = 1;
            AllowBuildingWithoutATerritory = 1;
            DeployableOutsideATerritory = new BindingList<string>(){
                "ExpansionSatchel",
                "Fireplace",
                "TerritoryFlagKit",
                "MediumTent",
                "LargeTent",
                "CarTent",
                "PartyTent",
                "ExpansionCamoTentKit",
                "ExpansionCamoBoxKit",
                "ShelterKit",
                "LandMineTrap",
                "BearTrap",
                "FishNetTrap",
                "RabbitSnareTrap",
                "SmallFishTrap",
                "TripwireTrap",
                "ExpansionSafeLarge",
                "ExpansionSafeMedium",
                "ExpansionSafeSmall",
                "SeaChest",
                "WoodenCrate",
                "GardenPlot" };
            DeployableInsideAEnemyTerritory = new BindingList<string>() {
                "ExpansionSatchel",
                "LandMineTrap",
                "BearTrap",
                "FishNetTrap",
                "RabbitSnareTrap",
                "SmallFishTrap",
                "TripwireTrap"};

            ZonesAreNoBuildZones = 1;

            CanCraftVanillaBasebuilding = 0;
            CanCraftExpansionBasebuilding = 1;
            DestroyFlagOnDismantle = 1;
            DismantleFlagMode = (int)ExpansionDismantleFlagMode.AnyoneWithTools;
            DismantleOutsideTerritory = 0;
            DismantleInsideTerritory = 0;
            DismantleAnywhere = 0;

            CodelockAttachMode = (int)ExpansionCodelockAttachMode.ExpansionAndFence;  //! Will also allow BBP if installed
            CodelockActionsAnywhere = 0;
            CodeLockLength = 4;
            DoDamageWhenEnterWrongCodeLock = 1;
            DamageWhenEnterWrongCodeLock = (decimal)10.0;
            RememberCode = 1;

            CanCraftTerritoryFlagKit = 1;
            SimpleTerritory = 1;
            AutomaticFlagOnCreation = 1;
            FlagMenuMode = (int)ExpansionFlagMenuMode.NoFlagChoice;
            GetTerritoryFlagKitAfterBuild = 0;

            PreventItemAccessThroughObstructingItems = 1;
            EnableVirtualStorage = 0;
            BuildZoneRequiredCustomMessage = "";
            VirtualStorageExcludedContainers = new BindingList<string>() { "ExpansionAirdropContainerBase" };
            Zones = new BindingList<ExpansionBuildNoBuildZone>
            {
                new ExpansionBuildNoBuildZone
                {
                    Name = "Krasnostav Trader Camp",
                    Center = new float[] { 11882.0f, 143.0f, 12466.0f },
                    Radius = 1000.0f,
                    Items = new BindingList<string>
                    {
                        "Fireplace",
                        "LandMineTrap",
                        "BearTrap",
                        "FishNetTrap",
                        "RabbitSnareTrap",
                        "SmallFishTrap",
                        "TripwireTrap",
                        "ExplosivesBase"
                    },
                    IsWhitelist = 1,
                    CustomMessage = ""
                },
                new ExpansionBuildNoBuildZone
                {
                    Name = "Green Mountain Trader Camp",
                    Center = new float[] { 3728.27f, 403.0f, 6003.60f },
                    Radius = 1000.0f,
                    Items = new BindingList<string>
                    {
                        "Fireplace",
                        "LandMineTrap",
                        "BearTrap",
                        "FishNetTrap",
                        "RabbitSnareTrap",
                        "SmallFishTrap",
                        "TripwireTrap",
                        "ExplosivesBase"
                    },
                    IsWhitelist = 1,
                    CustomMessage = ""
                },
                new ExpansionBuildNoBuildZone
                {
                    Name = "Kamenka Trader Camp",
                    Center = new float[] { 1143.14f, 6.9f, 2423.27f },
                    Radius = 1000.0f,
                    Items = new BindingList<string>
                    {
                        "Fireplace",
                        "LandMineTrap",
                        "BearTrap",
                        "FishNetTrap",
                        "RabbitSnareTrap",
                        "SmallFishTrap",
                        "TripwireTrap",
                        "ExplosivesBase"
                    },
                    IsWhitelist = 1,
                    CustomMessage = ""
                }
            };

        }


        public override bool Equals(object obj)
        {
            if (obj is not ExpansionBaseBuildingSettings other)
                return false;

            return m_Version == other.m_Version &&
                   CanBuildAnywhere == other.CanBuildAnywhere &&
                   AllowBuildingWithoutATerritory == other.AllowBuildingWithoutATerritory &&
                   CanCraftVanillaBasebuilding == other.CanCraftVanillaBasebuilding &&
                   CanCraftExpansionBasebuilding == other.CanCraftExpansionBasebuilding &&
                   DestroyFlagOnDismantle == other.DestroyFlagOnDismantle &&
                   DismantleOutsideTerritory == other.DismantleOutsideTerritory &&
                   DismantleInsideTerritory == other.DismantleInsideTerritory &&
                   DismantleAnywhere == other.DismantleAnywhere &&
                   CodelockActionsAnywhere == other.CodelockActionsAnywhere &&
                   CodeLockLength == other.CodeLockLength &&
                   DoDamageWhenEnterWrongCodeLock == other.DoDamageWhenEnterWrongCodeLock &&
                   DamageWhenEnterWrongCodeLock == other.DamageWhenEnterWrongCodeLock &&
                   RememberCode == other.RememberCode &&
                   CanCraftTerritoryFlagKit == other.CanCraftTerritoryFlagKit &&
                   SimpleTerritory == other.SimpleTerritory &&
                   AutomaticFlagOnCreation == other.AutomaticFlagOnCreation &&
                   GetTerritoryFlagKitAfterBuild == other.GetTerritoryFlagKitAfterBuild &&
                   BuildZoneRequiredCustomMessage == other.BuildZoneRequiredCustomMessage &&
                   ZonesAreNoBuildZones == other.ZonesAreNoBuildZones &&
                   CodelockAttachMode == other.CodelockAttachMode &&
                   DismantleFlagMode == other.DismantleFlagMode &&
                   FlagMenuMode == other.FlagMenuMode &&
                   PreventItemAccessThroughObstructingItems == other.PreventItemAccessThroughObstructingItems &&
                   EnableVirtualStorage == other.EnableVirtualStorage &&
                   DeployableOutsideATerritory.SequenceEqual(other.DeployableOutsideATerritory) &&
                   DeployableInsideAEnemyTerritory.SequenceEqual(other.DeployableInsideAEnemyTerritory) &&
                   VirtualStorageExcludedContainers.SequenceEqual(other.VirtualStorageExcludedContainers) &&
                   Zones.SequenceEqual(other.Zones);
        }

    }
    public class ExpansionBuildNoBuildZone
    {
        public string Name { get; set; }
        public float[] Center { get; set; }
        public float Radius { get; set; }
        public BindingList<string> Items { get; set; }
        public int IsWhitelist { get; set; }
        public string CustomMessage { get; set; }

        public ExpansionBuildNoBuildZone()
        {
            Items = new BindingList<string>();
        }

        public override string ToString()
        {
            return Name;
        }


        public override bool Equals(object obj)
        {
            if (obj is not ExpansionBuildNoBuildZone other)
                return false;

            return Name == other.Name &&
                   Radius == other.Radius &&
                   IsWhitelist == other.IsWhitelist &&
                   CustomMessage == other.CustomMessage &&
                   Center.SequenceEqual(other.Center) &&
                   Items.SequenceEqual(other.Items);
        }

    }
}
