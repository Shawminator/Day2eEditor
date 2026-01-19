using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpansionPlugin
{
    public class ExpansionVehiclesConfig: IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;
        public ExpansionTerritorySettings Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }
        public const int CurrentVersion = 6;

        public ExpansionVehiclesConfig(string path)
        {
            _path = path;
        }
        public void Load()
        {
            Data = null;
            Data = AppServices.GetRequired<FileService>().LoadOrCreateJson<ExpansionTerritorySettings>(
                _path,
                createNew: () => new ExpansionTerritorySettings(CurrentVersion),
                onAfterLoad: cfg => { },
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
                configName: "ExpansionTerritory",
                useVecConvertor: true
            );
            var missingFields = Data.FixMissingOrInvalidFields();
            if (missingFields.Any())
            {
                Console.WriteLine("Validation issues in " + FileName + ":");
                foreach (var issue in missingFields)
                {
                    Console.WriteLine("- " + issue);
                }
                isDirty = true;
            }
        }
        public IEnumerable<string> Save()
        {
            if (isDirty)
            {
                AppServices.GetRequired<FileService>().SaveJson(_path, Data, true);
                isDirty = false;
                return new[] { Path.GetFileName(_path) };
            }

            return Array.Empty<string>();
        }
        public bool needToSave()
        {
            return isDirty;
        }
    }
    public class ExpansionVehicleSettings
    {
        public int m_Version { get; set; }
        public ExpansionVehicleNetworkMode VehicleSync { get; set; } //! 0 = SERVER | 1 = CLIENT
        public ExpansionVehicleKeyStartMode VehicleRequireKeyToStart { get; set; } //! 0 = Disabled | 1 = Require key to start the engine (will check hands, cargo, inventory) | 2 = check only in the hand
        public int? VehicleRequireAllDoors { get; set; }                           //! If enabled, you will need all the doors to lock/unlock the car
        public int? VehicleLockedAllowInventoryAccess { get; set; }               //! If enabled, will be able to see the vehicle inventory regardless of the car have all his doors
        public int? VehicleLockedAllowInventoryAccessWithoutDoors { get; set; }   //! If enabled, will be able to see the vehicle inventory only if at least one car door is missing
        public int? MasterKeyPairingMode { get; set; }                              //! -1 = infinite | 0 = disabled | 1 = limited (will use MasterKeyUses) | 2 = renewable with a electronicrepairkit and KeyGrinder (use MasterKeyUses) | 3 = renewable only with a KeyGrinder (use MasterKeyUses)
        public int? MasterKeyUses { get; set; }                                     //! Amount of times the masterkey can pair unpaired keys
        public int? CanPickLock { get; set; }
        public BindingList<string> PickLockTools { get; set; }
	    public decimal? PickLockChancePercent { get; set; }
        public int? PickLockTimeSeconds { get; set; }
        public decimal? PickLockToolDamagePercent { get; set; }
        public int? EnableWindAerodynamics { get; set; }        //! If enabled, wind simulation will be enabled
        public int? EnableTailRotorDamage { get; set; }        //! If enabled, the rotor will be damageable
        public int? Towing { get; set; }                        //! If enabled, allow vehicle to tow other vehicles
        public int? EnableHelicopterExplosions { get; set; }    //! If enabled, allow Helicopters to explode
        public int? DisableVehicleDamage { get; set; }          //! If disabled, vehicles (cars, trucks) won't take any damages
        public decimal? VehicleCrewDamageMultiplier { get; set; }  //! Damage multiplier for the crew. How fast they will blackout or die.
        public decimal? VehicleSpeedDamageMultiplier { get; set; } //! Damage multiplier for the speed of the car. Above 0 is weaker and below 0 is stronger.
        public decimal? VehicleRoadKillDamageMultiplier { get; set; }
        public int? CollisionDamageIfEngineOff { get; set; }    //! Should the vehicle be able to receive damage if the engine is off?
        public decimal? CollisionDamageMinSpeedKmh { get; set; }	//! Minimum speed in km/h for vehicle to take collision damage
        public int? CanChangeLock { get; set; }
        public BindingList<string> ChangeLockTools { get; set; }
        public int? ChangeLockTimeSeconds{ get; set; }
        public decimal? ChangeLockToolDamagePercent { get; set; }
        public ExpansionPPOGORIVMode PlacePlayerOnGroundOnReconnectInVehicle { get; set; }
        public int? RevvingOverMaxRPMRuinsEngineInstantly { get; set; }
        public int? VehicleDropsRuinedDoors { get; set; }
        public int? ExplodingVehicleDropsAttachments { get; set; }
        public decimal? DesyncInvulnerabilityTimeoutSeconds { get; set; }  //! Timeout for temporary vehicle godmode during desync. Set to 0 to disable.
        public decimal? DamagedEngineStartupChancePercent { get; set; }
        public decimal? FuelConsumptionPercent { get; set; }
        public int? EnableVehicleCovers { get; set; }
        public int? AllowCoveringDEVehicles { get; set; } //! Allow covering of vehicles spawned via dynamic events (events.xml)
        public int? CanCoverWithCargo { get; set; }
        public int? UseVirtualStorageForCoverCargo { get; set; }
        public decimal? VehicleAutoCoverTimeSeconds { get; set; }
        public int? VehicleAutoCoverRequireCamonet { get; set; }
        public int? EnableAutoCoveringDEVehicles { get; set; }
        public string? CFToolsHeliCoverIconName { get; set; }
        public string? CFToolsBoatCoverIconName { get; set; }
        public string? CFToolsCarCoverIconName { get; set; }
        public int? ShowVehicleOwners { get; set; }
        public BindingList<ExpansionVehiclesConfig> VehiclesConfig { get; set; }

        public ExpansionVehicleSettings() { }
        public ExpansionVehicleSettings(int CurrentVersion)
        {
             
        }
    }
    public class ExpansionVehiclesLockConfig
    {
        public string? ClassName { get; set; }
        public decimal? LockComplexity { get; set; }

        public ExpansionVehiclesLockConfig(string classname = "", decimal lockcomplexity = (decimal)1.0)
        {
            ClassName = classname;
            LockComplexity = lockcomplexity;
        }
    }
    public enum ExpansionVehicleNetworkMode
    {
        SERVER,
        CLIENT
    };
    public enum ExpansionVehicleKeyStartMode
    {
        DISABLED,
        REQUIREDINVENTORY,
        REQUIREDHAND,
        COUNT
    };
    public enum ExpansionPPOGORIVMode
    {
        Disabled,
        Always,
        OnlyOnServerRestart
    }
}
