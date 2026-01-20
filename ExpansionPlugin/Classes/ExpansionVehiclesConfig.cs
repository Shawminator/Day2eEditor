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
        public ExpansionVehicleSettings Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }
        public const int CurrentVersion = 21;

        public ExpansionVehiclesConfig(string path)
        {
            _path = path;
        }
        public void Load()
        {
            Data = null;
            Data = AppServices.GetRequired<FileService>().LoadOrCreateJson<ExpansionVehicleSettings>(
                _path,
                createNew: () => new ExpansionVehicleSettings(CurrentVersion),
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
                configName: "ExpansionVehicle",
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
        public ExpansionVehicleNetworkMode? VehicleSync { get; set; } //! 0 = SERVER | 1 = CLIENT
        public ExpansionVehicleKeyStartMode? VehicleRequireKeyToStart { get; set; } //! 0 = Disabled | 1 = Require key to start the engine (will check hands, cargo, inventory) | 2 = check only in the hand
        public int? VehicleRequireAllDoors { get; set; }                           //! If enabled, you will need all the doors to lock/unlock the car
        public int? VehicleLockedAllowInventoryAccess { get; set; }               //! If enabled, will be able to see the vehicle inventory regardless of the car have all his doors
        public int? VehicleLockedAllowInventoryAccessWithoutDoors { get; set; }   //! If enabled, will be able to see the vehicle inventory only if at least one car door is missing
        public ExpansionMasterKeyPairingMode? MasterKeyPairingMode { get; set; }                              //! -1 = infinite | 0 = disabled | 1 = limited (will use MasterKeyUses) | 2 = renewable with a electronicrepairkit and KeyGrinder (use MasterKeyUses) | 3 = renewable only with a KeyGrinder (use MasterKeyUses)
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
        public ExpansionPPOGORIVMode? PlacePlayerOnGroundOnReconnectInVehicle { get; set; }
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
        public BindingList<ExpansionVehiclesLockConfig> VehiclesConfig { get; set; }

        public ExpansionVehicleSettings() { }
        public ExpansionVehicleSettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;

            VehicleSync = ExpansionVehicleNetworkMode.SERVER;

            VehicleRequireKeyToStart = ExpansionVehicleKeyStartMode.REQUIREDINVENTORY;
            VehicleRequireAllDoors = 1;
            VehicleLockedAllowInventoryAccess = 0;
            VehicleLockedAllowInventoryAccessWithoutDoors = 1;

            MasterKeyPairingMode = ExpansionMasterKeyPairingMode.Renewable_With_Grinder;
            MasterKeyUses = 2;

            CanPickLock = 0;
            PickLockChancePercent = 40;
            PickLockTimeSeconds = 120;
            PickLockToolDamagePercent = 10;
            PickLockTools = new BindingList<string>() { "Lockpick" };

            CanChangeLock = 0;
            ChangeLockTimeSeconds = 120;
            ChangeLockToolDamagePercent = 10;
            ChangeLockTools = new BindingList<string>() { "Screwdriver" };

            EnableWindAerodynamics = 0; // Not ready, need tweaking
            EnableTailRotorDamage = 1;

            EnableHelicopterExplosions = 1;

            Towing = 1;

            DisableVehicleDamage = 0;
            VehicleCrewDamageMultiplier = 1.0m;
            VehicleSpeedDamageMultiplier = 1.0m;
            VehicleRoadKillDamageMultiplier = 1.0m;

            CollisionDamageIfEngineOff = 0;
            CollisionDamageMinSpeedKmh = 30;

            PlacePlayerOnGroundOnReconnectInVehicle = ExpansionPPOGORIVMode.OnlyOnServerRestart;
            RevvingOverMaxRPMRuinsEngineInstantly = 0;
            VehicleDropsRuinedDoors = 1;
            ExplodingVehicleDropsAttachments = 1;

            //ForcePilotSyncIntervalSeconds = 1.0;
            DesyncInvulnerabilityTimeoutSeconds = 3.0m;

            DamagedEngineStartupChancePercent = 100.0m;

            FuelConsumptionPercent = 100.0m;

            EnableVehicleCovers = 1;
            CanCoverWithCargo = 1;
            AllowCoveringDEVehicles = 0;
            VehicleAutoCoverTimeSeconds = 0;  //! Lower than or equal to zero = disabled
            VehicleAutoCoverRequireCamonet = 0;  //! Require camonet attachment on vehicle

            //! CFTools icons. Any FontAwesome icon name should work.
            CFToolsHeliCoverIconName = "helicopter";
            CFToolsBoatCoverIconName = "ship";
            CFToolsCarCoverIconName = "car";

            ShowVehicleOwners = 0;
            DefaultVehicleConfig();
        }

        private void DefaultVehicleConfig()
        {
            VehiclesConfig = new BindingList<ExpansionVehiclesLockConfig>()
            {
                new ExpansionVehiclesLockConfig("ExpansionUAZCargoRoofless", 1.0m),
                new ExpansionVehiclesLockConfig("ExpansionUAZ", 1.0m),
                new ExpansionVehiclesLockConfig("ExpansionBus", 1.5m),
                new ExpansionVehiclesLockConfig("ExpansionVodnik", 2.0m),
                new ExpansionVehiclesLockConfig("ExpansionUtilityBoat", 1.25m),
                new ExpansionVehiclesLockConfig("ExpansionZodiacBoat", 0.5m),
                new ExpansionVehiclesLockConfig("ExpansionLHD", 100.0m),
                new ExpansionVehiclesLockConfig("ExpansionMerlin", 4.0m),
                new ExpansionVehiclesLockConfig("ExpansionMh6", 3.0m),
                new ExpansionVehiclesLockConfig("ExpansionUh1h", 2.5m)
            };
        }

        internal IEnumerable<object> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (m_Version != ExpansionVehiclesConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionVehiclesConfig.CurrentVersion}");
                m_Version = ExpansionVehiclesConfig.CurrentVersion;
            }
            if (!Enum.IsDefined(typeof(ExpansionVehicleNetworkMode), VehicleSync) ||
                    (VehicleSync != ExpansionVehicleNetworkMode.SERVER && VehicleSync != ExpansionVehicleNetworkMode.CLIENT))
            {
                fixes.Add($"Corrected VehicleSync from '{VehicleSync}' to '{ExpansionVehicleNetworkMode.SERVER}'");
                VehicleSync = ExpansionVehicleNetworkMode.SERVER;
            }
            if (!Enum.IsDefined(typeof(ExpansionVehicleKeyStartMode), VehicleRequireKeyToStart))
            {
                fixes.Add($"Corrected VehicleRequireKeyToStart from '{VehicleRequireKeyToStart}' to '{ExpansionVehicleKeyStartMode.REQUIREDINVENTORY}'");
                VehicleRequireKeyToStart = ExpansionVehicleKeyStartMode.REQUIREDINVENTORY;
            }
            if (VehicleRequireAllDoors is null or < 0 or > 1)
            {
                VehicleRequireAllDoors = 1;
                fixes.Add($"Corrected VehicleRequireAllDoors to 1.");
            }
            if (VehicleLockedAllowInventoryAccess is null or < 0 or > 1)
            {
                VehicleLockedAllowInventoryAccess = 0;
                fixes.Add($"Corrected VehicleLockedAllowInventoryAccess to 0.");
            }
            if (VehicleLockedAllowInventoryAccessWithoutDoors is null or < 0 or > 1)
            {
                VehicleLockedAllowInventoryAccessWithoutDoors = 1;
                fixes.Add($"Corrected VehicleLockedAllowInventoryAccessWithoutDoors to 1.");
            }
            if (!Enum.IsDefined(typeof(ExpansionMasterKeyPairingMode), MasterKeyPairingMode))
            {
                fixes.Add($"Corrected PlacePlayerOnGroundOnReconnectInVehicle from '{PlacePlayerOnGroundOnReconnectInVehicle}' to '{ExpansionMasterKeyPairingMode.Renewable_With_Grinder}'");
                MasterKeyPairingMode = ExpansionMasterKeyPairingMode.Renewable_With_Grinder;
            }
            if(MasterKeyUses is null)
            {
                MasterKeyUses = 2;
                fixes.Add($"Corrected MasterKeyUses to 2.");
            }
            if (CanPickLock is null or < 0 or > 1)
            {
                CanPickLock = 0;
                fixes.Add($"Corrected CanPickLock to 0.");
            }
            if (PickLockTools == null)
            {
                PickLockTools = new BindingList<string>() { "Lockpick" };
                fixes.Add($"Initialized default Lockip Tools");
            }
            if (PickLockChancePercent is null)
            {
                PickLockChancePercent = 40.0m;
                fixes.Add($"Corrected PickLockChancePercent to 40.");
            }
            if (PickLockTimeSeconds is null)
            {
                PickLockTimeSeconds = 120;
                fixes.Add($"Corrected PickLockTimeSeconds to 120");
            }
            if (PickLockToolDamagePercent is null)
            {
                PickLockToolDamagePercent = 10.0m;
                fixes.Add($"Corrected PickLockToolDamagePercent to 10.");
            }
            if (EnableWindAerodynamics is null or < 0 or > 1)
            {
                EnableWindAerodynamics = 0;
                fixes.Add($"Corrected EnableWindAerodynamics to 0.");
            }
            if (EnableTailRotorDamage is null or < 0 or > 1)
            {
                EnableTailRotorDamage = 1;
                fixes.Add($"Corrected EnableTailRotorDamage to 1.");
            }
            if (Towing is null or < 0 or > 1)
            {
                Towing = 1;
                fixes.Add($"Corrected Towing to 1.");
            }
            if (EnableHelicopterExplosions is null or < 0 or > 1)
            {
                EnableHelicopterExplosions = 1;
                fixes.Add($"Corrected EnableHelicopterExplosions to 1.");
            }
            if (DisableVehicleDamage is null or < 0 or > 1)
            {
                DisableVehicleDamage = 0;
                fixes.Add($"Corrected DisableVehicleDamage to 0.");
            }
            if (VehicleCrewDamageMultiplier is null)
            {
                VehicleCrewDamageMultiplier = 1.0m;
                fixes.Add($"Corrected VehicleCrewDamageMultiplier to 1.0.");
            }
            if (VehicleSpeedDamageMultiplier is null)
            {
                VehicleSpeedDamageMultiplier = 1.0m;
                fixes.Add($"Corrected VehicleSpeedDamageMultiplier to 1.0.");
            }
            if (VehicleRoadKillDamageMultiplier is null)
            {
                VehicleRoadKillDamageMultiplier = 1.0m;
                fixes.Add($"Corrected VehicleRoadKillDamageMultiplier to 1.0.");
            }
            if (CollisionDamageIfEngineOff is null or < 0 or > 1)
            {
                CollisionDamageIfEngineOff = 0;
                fixes.Add($"Corrected CollisionDamageIfEngineOff to 0.");
            }
            if (CollisionDamageMinSpeedKmh is null)
            {
                CollisionDamageMinSpeedKmh = 30;
                fixes.Add($"Corrected CollisionDamageMinSpeedKmh to 30.");
            }
            if(CanChangeLock is null or < 0 or > 1)
            {
                CanChangeLock = 0;
                fixes.Add($"Corrected CanChangeLock to 0.");
            }
            if (ChangeLockTools == null)
            {
                ChangeLockTools = new BindingList<string>() { "Screwdriver" };
                fixes.Add($"Initialized default changelock tools");
            }
            if (ChangeLockTimeSeconds is null)
            {
                ChangeLockTimeSeconds = 120;
                fixes.Add($"Corrected ChangeLockTimeSeconds to 120.");
            }
            if (ChangeLockToolDamagePercent is null)
            {
                ChangeLockToolDamagePercent = 10;
                fixes.Add($"Corrected ChangeLockToolDamagePercent to 10.");
            }
            if (!Enum.IsDefined(typeof(ExpansionPPOGORIVMode), PlacePlayerOnGroundOnReconnectInVehicle))
            {
                fixes.Add($"Corrected PlacePlayerOnGroundOnReconnectInVehicle from '{PlacePlayerOnGroundOnReconnectInVehicle}' to '{ExpansionPPOGORIVMode.OnlyOnServerRestart}'");
                PlacePlayerOnGroundOnReconnectInVehicle = ExpansionPPOGORIVMode.OnlyOnServerRestart;
            }
            if (RevvingOverMaxRPMRuinsEngineInstantly is null or < 0 or > 1)
            {
                RevvingOverMaxRPMRuinsEngineInstantly = 0;
                fixes.Add($"Corrected RevvingOverMaxRPMRuinsEngineInstantly to 0.");
            }
            if (VehicleDropsRuinedDoors is null or < 0 or > 1)
            {
                VehicleDropsRuinedDoors = 1;
                fixes.Add($"Corrected VehicleDropsRuinedDoors to 1.");
            }
            if (ExplodingVehicleDropsAttachments is null or < 0 or > 1)
            {
                ExplodingVehicleDropsAttachments = 1;
                fixes.Add($"Corrected ExplodingVehicleDropsAttachments to 1.");
            }
            if (DesyncInvulnerabilityTimeoutSeconds == null)
            {
                DesyncInvulnerabilityTimeoutSeconds = 3.0m;
                fixes.Add($"Corrected DesyncInvulnerabilityTimeoutSeconds to 3.");
            }
            if (DamagedEngineStartupChancePercent == null)
            {
                DamagedEngineStartupChancePercent = 100;
                fixes.Add($"Corrected DamagedEngineStartupChancePercent to 100.");
            }
            if (FuelConsumptionPercent == null)
            {
                FuelConsumptionPercent = 100;
                fixes.Add($"Corrected FuelConsumptionPercent to 100.");
            }
            if (EnableVehicleCovers is null or < 0 or > 1)
            {
                EnableVehicleCovers = 1;
                fixes.Add($"Corrected EnableVehicleCovers to 1.");
            }
            if (AllowCoveringDEVehicles is null or < 0 or > 1)
            {
                AllowCoveringDEVehicles = 0;
                fixes.Add($"Corrected AllowCoveringDEVehicles to 0.");
            }
            if (CanCoverWithCargo is null or < 0 or > 1)
            {
                CanCoverWithCargo = 1;
                fixes.Add($"Corrected CanCoverWithCargo to 1.");
            }
            if (UseVirtualStorageForCoverCargo is null or < 0 or > 1)
            {
                UseVirtualStorageForCoverCargo = 0;
                fixes.Add($"Corrected UseVirtualStorageForCoverCargo to 0.");
            }
            if (VehicleAutoCoverTimeSeconds is null or < 0)
            {
                VehicleAutoCoverTimeSeconds = 0;
                fixes.Add($"Corrected VehicleAutoCoverTimeSeconds to 0.");
            }
            if (VehicleAutoCoverRequireCamonet is null or < 0 or > 1)
            {
                VehicleAutoCoverRequireCamonet = 0;
                fixes.Add($"Corrected VehicleAutoCoverRequireCamonet to 0.");
            }
            if (EnableAutoCoveringDEVehicles is null or < 0 or > 1)
            {
                EnableAutoCoveringDEVehicles = 0;
                fixes.Add($"Corrected EnableAutoCoveringDEVehicles to 0.");
            }
            if (CFToolsHeliCoverIconName == null)
            {
                CFToolsHeliCoverIconName = "helicopter";
                fixes.Add($"Corrected CFToolsHeliCoverIconName to helicopter.");
            }
            if (CFToolsBoatCoverIconName == null)
            {
                CFToolsBoatCoverIconName = "ship";
                fixes.Add($"Corrected CFToolsBoatCoverIconName to ship.");
            }
            if (CFToolsCarCoverIconName == null)
            {
                CFToolsCarCoverIconName = "car";
                fixes.Add($"Corrected CFToolsCarCoverIconName to car.");
            }
            if(ShowVehicleOwners is null or < 0 or > 1)
            {
                ShowVehicleOwners = 0;
                fixes.Add($"Corrected ShowVehicleOwners to 0.");
            }
            if (VehiclesConfig == null)
            {
                DefaultVehicleConfig();
                fixes.Add($"Initialized default VehiclesConfigs");
            }
            return fixes;
        }
        public override bool Equals(object obj)
        {
            if (obj is not ExpansionVehicleSettings other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (m_Version != other.m_Version ||
                VehicleSync != other.VehicleSync ||
                VehicleRequireKeyToStart != other.VehicleRequireKeyToStart ||
                VehicleRequireAllDoors != other.VehicleRequireAllDoors ||
                VehicleLockedAllowInventoryAccess != other.VehicleLockedAllowInventoryAccess ||
                VehicleLockedAllowInventoryAccessWithoutDoors != other.VehicleLockedAllowInventoryAccessWithoutDoors ||
                MasterKeyPairingMode != other.MasterKeyPairingMode ||
                MasterKeyUses != other.MasterKeyUses ||
                CanPickLock != other.CanPickLock ||
                PickLockChancePercent != other.PickLockChancePercent ||
                PickLockTimeSeconds != other.PickLockTimeSeconds ||
                PickLockToolDamagePercent != other.PickLockToolDamagePercent ||
                EnableWindAerodynamics != other.EnableWindAerodynamics ||
                EnableTailRotorDamage != other.EnableTailRotorDamage ||
                Towing != other.Towing ||
                EnableHelicopterExplosions != other.EnableHelicopterExplosions ||
                DisableVehicleDamage != other.DisableVehicleDamage ||
                VehicleCrewDamageMultiplier != other.VehicleCrewDamageMultiplier ||
                VehicleSpeedDamageMultiplier != other.VehicleSpeedDamageMultiplier ||
                VehicleRoadKillDamageMultiplier != other.VehicleRoadKillDamageMultiplier ||
                CollisionDamageIfEngineOff != other.CollisionDamageIfEngineOff ||
                CollisionDamageMinSpeedKmh != other.CollisionDamageMinSpeedKmh ||
                CanChangeLock != other.CanChangeLock ||
                ChangeLockTimeSeconds != other.ChangeLockTimeSeconds ||
                ChangeLockToolDamagePercent != other.ChangeLockToolDamagePercent ||
                PlacePlayerOnGroundOnReconnectInVehicle != other.PlacePlayerOnGroundOnReconnectInVehicle ||
                RevvingOverMaxRPMRuinsEngineInstantly != other.RevvingOverMaxRPMRuinsEngineInstantly ||
                VehicleDropsRuinedDoors != other.VehicleDropsRuinedDoors ||
                ExplodingVehicleDropsAttachments != other.ExplodingVehicleDropsAttachments ||
                DesyncInvulnerabilityTimeoutSeconds != other.DesyncInvulnerabilityTimeoutSeconds ||
                DamagedEngineStartupChancePercent != other.DamagedEngineStartupChancePercent ||
                FuelConsumptionPercent != other.FuelConsumptionPercent ||
                EnableVehicleCovers != other.EnableVehicleCovers ||
                AllowCoveringDEVehicles != other.AllowCoveringDEVehicles ||
                CanCoverWithCargo != other.CanCoverWithCargo ||
                UseVirtualStorageForCoverCargo != other.UseVirtualStorageForCoverCargo ||
                VehicleAutoCoverTimeSeconds != other.VehicleAutoCoverTimeSeconds ||
                VehicleAutoCoverRequireCamonet != other.VehicleAutoCoverRequireCamonet ||
                EnableAutoCoveringDEVehicles != other.EnableAutoCoveringDEVehicles ||
                ShowVehicleOwners != other.ShowVehicleOwners ||
                CFToolsHeliCoverIconName != other.CFToolsHeliCoverIconName ||
                CFToolsBoatCoverIconName != other.CFToolsBoatCoverIconName ||
                CFToolsCarCoverIconName != other.CFToolsCarCoverIconName)
                return false;

            if (!PickLockTools.SequenceEqual(other.PickLockTools))
                return false;
            
            if (!ChangeLockTools.SequenceEqual(other.ChangeLockTools))
                return false;

            if (!ListEquals(VehiclesConfig, other.VehiclesConfig))
                return false;

            return true;

        }
        private static bool ListEquals<T>(IList<T> a, IList<T> b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (a is null || b is null)
                return false;

            if (a.Count != b.Count)
                return false;

            for (int i = 0; i < a.Count; i++)
            {
                if (!Equals(a[i], b[i]))
                    return false;
            }

            return true;
        }

        public ExpansionVehicleSettings Clone()
        {
            return new ExpansionVehicleSettings
            {
                m_Version = this.m_Version,
                VehicleSync = this.VehicleSync,
                VehicleRequireKeyToStart = this.VehicleRequireKeyToStart,

                VehicleRequireAllDoors = this.VehicleRequireAllDoors,
                VehicleLockedAllowInventoryAccess = this.VehicleLockedAllowInventoryAccess,
                VehicleLockedAllowInventoryAccessWithoutDoors = this.VehicleLockedAllowInventoryAccessWithoutDoors,
                MasterKeyPairingMode = this.MasterKeyPairingMode,
                MasterKeyUses = this.MasterKeyUses,
                CanPickLock = this.CanPickLock,

                PickLockTools = new BindingList<string>(this.PickLockTools?.ToList() ?? new List<string>()),
                PickLockChancePercent = this.PickLockChancePercent,
                PickLockTimeSeconds = this.PickLockTimeSeconds,
                PickLockToolDamagePercent = this.PickLockToolDamagePercent,

                EnableWindAerodynamics = this.EnableWindAerodynamics,
                EnableTailRotorDamage = this.EnableTailRotorDamage,
                Towing = this.Towing,
                EnableHelicopterExplosions = this.EnableHelicopterExplosions,
                DisableVehicleDamage = this.DisableVehicleDamage,

                VehicleCrewDamageMultiplier = this.VehicleCrewDamageMultiplier,
                VehicleSpeedDamageMultiplier = this.VehicleSpeedDamageMultiplier,
                VehicleRoadKillDamageMultiplier = this.VehicleRoadKillDamageMultiplier,

                CollisionDamageIfEngineOff = this.CollisionDamageIfEngineOff,
                CollisionDamageMinSpeedKmh = this.CollisionDamageMinSpeedKmh,

                CanChangeLock = this.CanChangeLock,
                ChangeLockTools = new BindingList<string>(this.ChangeLockTools?.ToList() ?? new List<string>()),
                ChangeLockTimeSeconds = this.ChangeLockTimeSeconds,
                ChangeLockToolDamagePercent = this.ChangeLockToolDamagePercent,

                PlacePlayerOnGroundOnReconnectInVehicle = this.PlacePlayerOnGroundOnReconnectInVehicle,

                RevvingOverMaxRPMRuinsEngineInstantly = this.RevvingOverMaxRPMRuinsEngineInstantly,
                VehicleDropsRuinedDoors = this.VehicleDropsRuinedDoors,
                ExplodingVehicleDropsAttachments = this.ExplodingVehicleDropsAttachments,

                DesyncInvulnerabilityTimeoutSeconds = this.DesyncInvulnerabilityTimeoutSeconds,
                DamagedEngineStartupChancePercent = this.DamagedEngineStartupChancePercent,
                FuelConsumptionPercent = this.FuelConsumptionPercent,

                EnableVehicleCovers = this.EnableVehicleCovers,
                AllowCoveringDEVehicles = this.AllowCoveringDEVehicles,
                CanCoverWithCargo = this.CanCoverWithCargo,
                UseVirtualStorageForCoverCargo = this.UseVirtualStorageForCoverCargo,
                VehicleAutoCoverTimeSeconds = this.VehicleAutoCoverTimeSeconds,
                VehicleAutoCoverRequireCamonet = this.VehicleAutoCoverRequireCamonet,
                EnableAutoCoveringDEVehicles = this.EnableAutoCoveringDEVehicles,

                CFToolsHeliCoverIconName = this.CFToolsHeliCoverIconName,
                CFToolsBoatCoverIconName = this.CFToolsBoatCoverIconName,
                CFToolsCarCoverIconName = this.CFToolsCarCoverIconName,

                ShowVehicleOwners = this.ShowVehicleOwners,

                VehiclesConfig = new BindingList<ExpansionVehiclesLockConfig>(
                    this.VehiclesConfig?.Select(v => v?.Clone()).ToList()
                    ?? new List<ExpansionVehiclesLockConfig>()
                ),
            };
        }

 
    }
    public class ExpansionVehiclesLockConfig
    {
        public string? ClassName { get; set; }
        public decimal? LockComplexity { get; set; }

        public ExpansionVehiclesLockConfig() { }
        public ExpansionVehiclesLockConfig(string classname = "", decimal lockcomplexity = (decimal)1.0)
        {
            ClassName = classname;
            LockComplexity = lockcomplexity;
        }
        public override bool Equals(object obj)
        {
            if (obj is not ExpansionVehiclesLockConfig other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (ClassName != other.ClassName ||
                LockComplexity != other.LockComplexity)
                return false;

            return true;

        }
        public ExpansionVehiclesLockConfig Clone()
        {
            return new ExpansionVehiclesLockConfig
            {
                ClassName = ClassName,
                LockComplexity = this.LockComplexity
            };
        }
    }
    public enum ExpansionVehicleNetworkMode
    {
        [Description("Vehicles will be simulated by the Server and only the server")]
        SERVER,
        [Description("Vehicles will be simulated by the client (player).")]
        CLIENT
    };
    public enum ExpansionVehicleKeyStartMode
    {
        [Description("Even if your car is paired to a key, you don't need to have the key in the vehicle inventory or on yourself")]
        DISABLED,
        [Description("You will need a car key paired to the vehicle in your inventory or in the inventory of the vehicle to start the engine")]
        REQUIREDINVENTORY,
        [Description("You need to have the key in your hands to start the engine")]
        REQUIREDHAND,
        [Description("No Idea")]
        COUNT
    };
    public enum ExpansionPPOGORIVMode
    {
        [Description("disabled")]
        Disabled,
        [Description("Always")]
        Always,
        [Description(" Only on server restarts")]
        OnlyOnServerRestart
    }
    public enum ExpansionMasterKeyPairingMode
    {
        [Description("infinite master pairing uses")]
        Infinite = -1,
        [Description("disabled. You can pair any keys to your already paired car")]
        Disabled = 0,
        [Description("limited uses before becoming a normal car key (MasterKeyUses)")]
        Limited_Uses = 1,
        [Description("enewable with a electronicalrepairkit or a keygrinder (will also use MasterKeyUses)")]
        Renewable = 2,
        [Description("renewable with a keygrinder (will also use MasterKeyUses). Currently only configured for MuchCarKeys, a futur update will also add a Expansion Grinder.")]
        Renewable_With_Grinder = 2,
    };
}
