using System.ComponentModel;
using System.IO;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace Day2eEditor
{
    public class CFGGameplayConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path); // e.g., "types.xml"
        public string FilePath => _path;
        public cfggameplay Data { get; private set; } = new cfggameplay();
        public SpawnGearPresetsConfig SpawnGearPresetconfigs { get; private set; }
        public PlayerRestrictedFilesConfig PlayerRestrictedFilesConfig { get; private set; }
        public ObjectSpawnerArrConfig ObjectSpawnerArrConfig { get; private set; }

        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }

        private string BaseDirectory => Path.GetDirectoryName(_path) ?? "";

        public CFGGameplayConfig(string path)
        {
            _path = path ?? throw new ArgumentNullException(nameof(path));
        }

        public void Load()
        {
            Data = AppServices.GetRequired<FileService>().LoadOrCreateJson<cfggameplay>(
                _path,
                createNew: () => new cfggameplay(),
                onAfterLoad: _ => { },
                checkVersionAndUpdate: cfg => cfg.checkver(),
                onError: ex => LogError("CFGGameplay", ex),
                configName: "CFGGameplay",
                useBoolConvertor: false
            );

            LoadSpawnGearFiles();
            LoadRestrictedFiles();
            LoadObjectSpawenArrFiles();
        }
        public IEnumerable<string> Save()
        {
            List<string> filesnames = new List<string>();
            if (isDirty)
            {
                UpdateSpawnGearFileList();
                UpdateRestrictedFileList();
                UpdateObjectSpawenArrList();

                AppServices.GetRequired<FileService>().SaveJson(_path, Data);

                
                isDirty = false;
                filesnames.Add(Path.GetFileName(_path));
            }
            filesnames.AddRange(SpawnGearPresetconfigs.Save());
            filesnames.AddRange(PlayerRestrictedFilesConfig.Save());
            filesnames.AddRange(ObjectSpawnerArrConfig.Save());

            return filesnames.ToArray();
        }

        public bool needToSave()
        {
            return isDirty;
        }


        #region Spawn Gear Files
        private void LoadSpawnGearFiles()
        {
            Console.WriteLine("\t## Starting SpawnGearPresets ##");

            SpawnGearPresetconfigs = new SpawnGearPresetsConfig();
            SpawnGearPresetconfigs.Load(BaseDirectory, Data.PlayerData.spawnGearPresetFiles);

            Console.WriteLine("\t## End SpawnGearPresets ##");
        }
        private void UpdateSpawnGearFileList()
        {
            Data.PlayerData.spawnGearPresetFiles = new BindingList<string>();
            foreach (var preset in SpawnGearPresetconfigs.AllData)
            {
                if (preset.ToDelete)
                    continue;
                string _path = Path.Combine(preset.ModFolder, preset.FileName).Replace("\\", "/").Replace("//", "/");
                Data.PlayerData.spawnGearPresetFiles.Add(_path);
            }
        }
        public SpawnGearPresetFiles GetSpawnGearPreset(string spawnfile)
        {
            string filename = Path.GetFileName(spawnfile);
            string modpath = Path.GetDirectoryName(spawnfile);
            return SpawnGearPresetconfigs.AllData.FirstOrDefault(x => x.FileName == filename && x.ModFolder == modpath);
        }
        public bool AddNewSpawnGear(SpawnGearPresetFiles newfile)
        {
            string relativepath = Path.Combine(newfile.ModFolder, newfile.FileName).Replace("\\", "/");
            if (Data.PlayerData.spawnGearPresetFiles.Contains(relativepath))
                return false;
            Data.PlayerData.spawnGearPresetFiles.Add(relativepath);
            SpawnGearPresetconfigs.AllData.Add(newfile);
            isDirty = true;
            return true;
        }
        public void RemoveSpawnGearPreset(SpawnGearPresetFiles spawnGearPresetFiles)
        {
            string relativepath = Path.Combine(spawnGearPresetFiles.ModFolder, spawnGearPresetFiles.FileName).Replace("\\", "/");
            Data.PlayerData.spawnGearPresetFiles.Remove(relativepath);
            isDirty = true;
        }
        #endregion

        #region Restricted Area Files
        private void LoadRestrictedFiles()
        {
            Console.WriteLine("\t## Starting Restricted Area Files ##");

            PlayerRestrictedFilesConfig = new PlayerRestrictedFilesConfig();
            PlayerRestrictedFilesConfig.Load(BaseDirectory, Data.WorldsData.playerRestrictedAreaFiles);

            Console.WriteLine("\t## End Restricted Area Files ##");
        }
        private void UpdateRestrictedFileList()
        {
            Data.WorldsData.playerRestrictedAreaFiles = new BindingList<string>();
            foreach (var restricted in PlayerRestrictedFilesConfig.AllData)
            {
                string _path = Path.Combine(restricted.ModFolder, restricted.FileName).Replace("\\", "/").Replace("//", "/");
                Data.WorldsData.playerRestrictedAreaFiles.Add(_path);
            }
        }
        public PlayerRestrictedFiles getRestrictedFiles(string restrictedfile)
        {
            string filename = Path.GetFileName(restrictedfile);
            string modpath = Path.GetDirectoryName(restrictedfile);
            return PlayerRestrictedFilesConfig.AllData.FirstOrDefault(x => x.FileName == filename && x.ModFolder == modpath);
        }
        #endregion

        #region objects spwner arr files
        private void LoadObjectSpawenArrFiles()
        {
            Console.WriteLine("\t## Starting Object Spawner Arr Files ##");

            ObjectSpawnerArrConfig = new ObjectSpawnerArrConfig();
            ObjectSpawnerArrConfig.Load(BaseDirectory, Data.WorldsData.objectSpawnersArr);
            
            Console.WriteLine("\t## End Object Spawner Arr  Files ##");
        }
        private void UpdateObjectSpawenArrList()
        {
            Data.WorldsData.objectSpawnersArr = new BindingList<string>();
            foreach (var restricted in ObjectSpawnerArrConfig.AllData)
            {
                string _path = Path.Combine(restricted.ModFolder, restricted.FileName).Replace("\\", "/").Replace("//", "/");
                Data.WorldsData.objectSpawnersArr.Add(_path);
            }
        }
        public ObjectSpawnerArr getobjectspawnerFiles(string objectspawnerarrfile)
        {
            string filename = Path.GetFileName(objectspawnerarrfile);
            string modpath = Path.GetDirectoryName(objectspawnerarrfile);
            return ObjectSpawnerArrConfig.AllData.FirstOrDefault(x => x.FileName == filename && x.ModFolder == modpath);
        }
        #endregion


        private void LogError(string context, Exception ex)
        {
            HasErrors = true;
            string message = $"Error in {context}\n{ex.Message}";
            if (ex.InnerException != null)
                message += $"\n{ex.InnerException.Message}";

            Console.WriteLine(message);
            Errors.Add( message);
        }

    }
    
    
    public class cfggameplay
    {
        public int version { get; set; }
        public Generaldata GeneralData { get; set; }
        public Playerdata PlayerData { get; set; }
        public Worldsdata WorldsData { get; set; }
        public Basebuildingdata BaseBuildingData { get; set; }
        public Uidata UIData { get; set; }
        public CFGGameplayMapData MapData { get; set; }
        public VehicleData VehicleData { get; set; }

        [JsonIgnore]
        const int currentversion = 123;


        public cfggameplay()
        {
            version = currentversion;
            GeneralData = new Generaldata();
            PlayerData = new Playerdata();
            WorldsData = new Worldsdata();
            BaseBuildingData = new Basebuildingdata();
            UIData = new Uidata();
            MapData = new CFGGameplayMapData();
            VehicleData = new VehicleData();
        }

        internal bool checkver()
        {
            if (version != currentversion)
            {
                version = currentversion;
                return true;
            }
            return false;
        }

    }

    public class Generaldata
    {
        public bool disableBaseDamage { get; set; }
        public bool disableContainerDamage { get; set; }
        public bool disableRespawnDialog { get; set; }
        public bool disableRespawnInUnconsciousness { get; set; }

        public Generaldata()
        {
            disableBaseDamage = false;
            disableContainerDamage = false;
            disableRespawnDialog = false;
            disableRespawnInUnconsciousness = false;
        }
        public override bool Equals(object obj)
        {
            if (obj is not Generaldata other)
                return false;

            return disableBaseDamage == other.disableBaseDamage &&
                   disableContainerDamage == other.disableContainerDamage &&
                   disableRespawnDialog == other.disableRespawnDialog &&
                   disableRespawnInUnconsciousness == other.disableRespawnInUnconsciousness;
        }
    }

    public class Playerdata
    {
        public bool disablePersonalLight { get; set; }
        public BindingList<string> spawnGearPresetFiles { get; set; }
        public Staminadata StaminaData { get; set; }
        public Shockhandlingdata ShockHandlingData { get; set; }
        public MovementData MovementData { get; set; }
        public DrowningData DrowningData { get; set; }
        public WeaponObstructionData WeaponObstructionData { get; set; }

        public Playerdata()
        {
            disablePersonalLight = true;
            spawnGearPresetFiles = new BindingList<string>();
            StaminaData = new Staminadata();
            ShockHandlingData = new Shockhandlingdata();
            MovementData = new MovementData();
            DrowningData = new DrowningData();
            WeaponObstructionData = new WeaponObstructionData();
        }


        public override bool Equals(object obj)
        {
            if (obj is not Playerdata other)
                return false;

            return disablePersonalLight == other.disablePersonalLight &&
                   Equals(StaminaData, other.StaminaData) &&
                   Equals(ShockHandlingData, other.ShockHandlingData) &&
                   Equals(MovementData, other.MovementData) &&
                   Equals(DrowningData, other.DrowningData) &&
                   Equals(WeaponObstructionData, other.WeaponObstructionData);
            // spawnGearPresetFiles is intentionally ignored
        }


    }
    public class Staminadata
    {
        public decimal sprintStaminaModifierErc { get; set; }
        public decimal sprintStaminaModifierCro { get; set; }
        public decimal staminaWeightLimitThreshold { get; set; }
        public decimal staminaMax { get; set; }
        public decimal staminaKgToStaminaPercentPenalty { get; set; }
        public decimal staminaMinCap { get; set; }
        public decimal sprintSwimmingStaminaModifier { get; set; }
        public decimal sprintLadderStaminaModifier { get; set; }
        public decimal meleeStaminaModifier { get; set; }
        public decimal obstacleTraversalStaminaModifier { get; set; }
        public decimal holdBreathStaminaModifier { get; set; }

        public Staminadata()
        {
            sprintStaminaModifierErc = (decimal)1.0;
            sprintStaminaModifierCro = (decimal)1.0;
            staminaWeightLimitThreshold = (decimal)6000.0;
            staminaMax = (decimal)100.0;
            staminaKgToStaminaPercentPenalty = (decimal)1.75;
            staminaMinCap = (decimal)5.0;
            sprintSwimmingStaminaModifier = 1;
            sprintLadderStaminaModifier = 1;
            meleeStaminaModifier = 1;
            obstacleTraversalStaminaModifier = 1;
            holdBreathStaminaModifier = 1;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Staminadata other)
                return false;

            return sprintStaminaModifierErc == other.sprintStaminaModifierErc &&
                   sprintStaminaModifierCro == other.sprintStaminaModifierCro &&
                   staminaWeightLimitThreshold == other.staminaWeightLimitThreshold &&
                   staminaMax == other.staminaMax &&
                   staminaKgToStaminaPercentPenalty == other.staminaKgToStaminaPercentPenalty &&
                   staminaMinCap == other.staminaMinCap &&
                   sprintSwimmingStaminaModifier == other.sprintSwimmingStaminaModifier &&
                   sprintLadderStaminaModifier == other.sprintLadderStaminaModifier &&
                   meleeStaminaModifier == other.meleeStaminaModifier &&
                   obstacleTraversalStaminaModifier == other.obstacleTraversalStaminaModifier &&
                   holdBreathStaminaModifier == other.holdBreathStaminaModifier;
        }

    }
    public class Shockhandlingdata
    {
        public decimal shockRefillSpeedConscious { get; set; }
        public decimal shockRefillSpeedUnconscious { get; set; }
        public bool allowRefillSpeedModifier { get; set; }

        public Shockhandlingdata()
        {
            shockRefillSpeedConscious = (decimal)5.0;
            shockRefillSpeedUnconscious = (decimal)1.0;
            allowRefillSpeedModifier = true;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Shockhandlingdata other)
                return false;

            return shockRefillSpeedConscious == other.shockRefillSpeedConscious &&
                   shockRefillSpeedUnconscious == other.shockRefillSpeedUnconscious &&
                   allowRefillSpeedModifier == other.allowRefillSpeedModifier;
        }

    }
    public class MovementData
    {
        public decimal timeToStrafeJog { get; set; }
        public decimal rotationSpeedJog { get; set; }
        public decimal timeToSprint { get; set; }
        public decimal timeToStrafeSprint { get; set; }
        public decimal rotationSpeedSprint { get; set; }
        public bool allowStaminaAffectInertia { get; set; }

        public MovementData()
        {
            timeToStrafeJog = (decimal)0.1;
            rotationSpeedJog = (decimal)0.3;
            timeToSprint = (decimal)0.45;
            timeToStrafeSprint = (decimal)0.3;
            rotationSpeedSprint = (decimal)0.15;
            allowStaminaAffectInertia = true;
        }

        public override bool Equals(object obj)
        {
            if (obj is not MovementData other)
                return false;

            return timeToStrafeJog == other.timeToStrafeJog &&
                   rotationSpeedJog == other.rotationSpeedJog &&
                   timeToSprint == other.timeToSprint &&
                   timeToStrafeSprint == other.timeToStrafeSprint &&
                   rotationSpeedSprint == other.rotationSpeedSprint &&
                   allowStaminaAffectInertia == other.allowStaminaAffectInertia;
        }

    }
    public class DrowningData
    {
        public decimal staminaDepletionSpeed { get; set; }
        public decimal healthDepletionSpeed { get; set; }
        public decimal shockDepletionSpeed { get; set; }

        public DrowningData()
        {
            staminaDepletionSpeed = (decimal)10.0;
            healthDepletionSpeed = (decimal)10.0;
            shockDepletionSpeed = (decimal)10.0;
        }

        public override bool Equals(object obj)
        {
            if (obj is not DrowningData other)
                return false;

            return staminaDepletionSpeed == other.staminaDepletionSpeed &&
                   healthDepletionSpeed == other.healthDepletionSpeed &&
                   shockDepletionSpeed == other.shockDepletionSpeed;
        }

    }

    public class WeaponObstructionData
    {
        public int staticMode { get; set; }
        public int dynamicMode { get; set; }

        public WeaponObstructionData()
        {
            staticMode = 1;
            dynamicMode = 1;
        }

        public override bool Equals(object obj)
        {
            if (obj is not WeaponObstructionData other)
                return false;

            return staticMode == other.staticMode &&
                   dynamicMode == other.dynamicMode;
        }


    }
    public class Worldsdata
    {
        public int lightingConfig { get; set; }
        public BindingList<string> objectSpawnersArr { get; set; }
        public BindingList<decimal> environmentMinTemps { get; set; }
        public BindingList<decimal> environmentMaxTemps { get; set; }
        public BindingList<decimal> wetnessWeightModifiers { get; set; }
        public BindingList<string> playerRestrictedAreaFiles { get; set; }

        public Worldsdata()
        {
            lightingConfig = 1;
            objectSpawnersArr = new BindingList<string>();
            decimal[] mintemp = new decimal[] { -3, -2, 0, 4, 9, 14, 18, 17, 12, 7, 4, 0 };
            decimal[] maxtemp = new decimal[] { 3, 5, 7, 14, 19, 24, 26, 25, 21, 16, 10, 5 };
            decimal[] wetness = new decimal[] { (decimal)1.0, (decimal)1.0, (decimal)1.33, (decimal)1.66, (decimal)2.0 };
            environmentMaxTemps = new BindingList<decimal>(maxtemp.ToArray());
            environmentMinTemps = new BindingList<decimal>(mintemp.ToArray());
            wetnessWeightModifiers = new BindingList<decimal>(wetness.ToArray());
            playerRestrictedAreaFiles = new BindingList<string>();
        }

        public override bool Equals(object obj)
        {
            if (obj is not Worldsdata other)
                return false;

            return lightingConfig == other.lightingConfig &&
                   environmentMinTemps.SequenceEqual(other.environmentMinTemps) &&
                   environmentMaxTemps.SequenceEqual(other.environmentMaxTemps) &&
                   wetnessWeightModifiers.SequenceEqual(other.wetnessWeightModifiers);
        }

    }

    public class Basebuildingdata
    {
        public Hologramdata HologramData { get; set; }
        public Constructiondata ConstructionData { get; set; }

        public Basebuildingdata()
        {
            HologramData = new Hologramdata();
            ConstructionData = new Constructiondata();
        }

        public override bool Equals(object obj)
        {
            if (obj is not Basebuildingdata other)
                return false;

            return
                HologramData.disableIsCollidingBBoxCheck == other.HologramData.disableIsCollidingBBoxCheck &&
                HologramData.disableIsCollidingPlayerCheck == other.HologramData.disableIsCollidingPlayerCheck &&
                HologramData.disableIsClippingRoofCheck == other.HologramData.disableIsClippingRoofCheck &&
                HologramData.disableIsBaseViableCheck == other.HologramData.disableIsBaseViableCheck &&
                HologramData.disableIsCollidingGPlotCheck == other.HologramData.disableIsCollidingGPlotCheck &&
                HologramData.disableIsCollidingAngleCheck == other.HologramData.disableIsCollidingAngleCheck &&
                HologramData.disableIsPlacementPermittedCheck == other.HologramData.disableIsPlacementPermittedCheck &&
                HologramData.disableHeightPlacementCheck == other.HologramData.disableHeightPlacementCheck &&
                HologramData.disableIsUnderwaterCheck == other.HologramData.disableIsUnderwaterCheck &&
                HologramData.disableIsInTerrainCheck == other.HologramData.disableIsInTerrainCheck &&
                HologramData.disableColdAreaBuildingCheck == other.HologramData.disableColdAreaBuildingCheck &&
                HologramData.disallowedTypesInUnderground.SequenceEqual(other.HologramData.disallowedTypesInUnderground) &&
                ConstructionData.disablePerformRoofCheck == other.ConstructionData.disablePerformRoofCheck &&
                ConstructionData.disableIsCollidingCheck == other.ConstructionData.disableIsCollidingCheck &&
                ConstructionData.disableDistanceCheck == other.ConstructionData.disableDistanceCheck;
        }

    }
    public class Hologramdata
    {
        public bool disableIsCollidingBBoxCheck { get; set; }
        public bool disableIsCollidingPlayerCheck { get; set; }
        public bool disableIsClippingRoofCheck { get; set; }
        public bool disableIsBaseViableCheck { get; set; }
        public bool disableIsCollidingGPlotCheck { get; set; }
        public bool disableIsCollidingAngleCheck { get; set; }
        public bool disableIsPlacementPermittedCheck { get; set; }
        public bool disableHeightPlacementCheck { get; set; }
        public bool disableIsUnderwaterCheck { get; set; }
        public bool disableIsInTerrainCheck { get; set; }
        public bool disableColdAreaBuildingCheck { get; set; }
        public BindingList<string> disallowedTypesInUnderground { get; set; }

        public Hologramdata()
        {
            disableIsCollidingBBoxCheck = false;
            disableIsCollidingPlayerCheck = false;
            disableIsClippingRoofCheck = false;
            disableIsBaseViableCheck = false;
            disableIsCollidingGPlotCheck = false;
            disableIsCollidingAngleCheck = false;
            disableIsPlacementPermittedCheck = false;
            disableHeightPlacementCheck = false;
            disableIsUnderwaterCheck = false;
            disableIsInTerrainCheck = false;
            disableColdAreaBuildingCheck = false;
            disallowedTypesInUnderground = new BindingList<string>(new string[] { "FenceKit", "TerritoryFlagKit", "WatchtowerKit" });
        }
    }
    public class Constructiondata
    {
        public bool disablePerformRoofCheck { get; set; }
        public bool disableIsCollidingCheck { get; set; }
        public bool disableDistanceCheck { get; set; }

        public Constructiondata()
        {
            disablePerformRoofCheck = false;
            disableIsCollidingCheck = false;
            disableDistanceCheck = false;
        }
    }

    public class Uidata
    {
        public bool use3DMap { get; set; }
        public Hitindicationdata HitIndicationData { get; set; }

        public Uidata()
        {
            use3DMap = false;
            HitIndicationData = new Hitindicationdata();
        }

        public override bool Equals(object obj)
        {
            if (obj is not Uidata other)
                return false;

            return use3DMap == other.use3DMap &&
                   (HitIndicationData?.Equals(other.HitIndicationData) ?? other.HitIndicationData == null);
        }

    }
    public class Hitindicationdata
    {
        public bool hitDirectionOverrideEnabled { get; set; }
        public int hitDirectionBehaviour { get; set; }
        public int hitDirectionStyle { get; set; }
        public string hitDirectionIndicatorColorStr { get; set; }
        public decimal hitDirectionMaxDuration { get; set; }
        public decimal hitDirectionBreakPointRelative { get; set; }
        public decimal hitDirectionScatter { get; set; }
        public bool hitIndicationPostProcessEnabled { get; set; }

        public Hitindicationdata()
        {
            hitDirectionOverrideEnabled = false;
            hitDirectionBehaviour = 1;
            hitDirectionStyle = 0;
            hitDirectionIndicatorColorStr = "0xffbb0a1e";
            hitDirectionMaxDuration = (decimal)2.0;
            hitDirectionBreakPointRelative = (decimal)0.2;
            hitDirectionScatter = (decimal)10.0;
            hitIndicationPostProcessEnabled = true;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Hitindicationdata other)
                return false;

            return hitDirectionOverrideEnabled == other.hitDirectionOverrideEnabled &&
                   hitDirectionBehaviour == other.hitDirectionBehaviour &&
                   hitDirectionStyle == other.hitDirectionStyle &&
                   hitDirectionIndicatorColorStr == other.hitDirectionIndicatorColorStr &&
                   hitDirectionMaxDuration == other.hitDirectionMaxDuration &&
                   hitDirectionBreakPointRelative == other.hitDirectionBreakPointRelative &&
                   hitDirectionScatter == other.hitDirectionScatter &&
                   hitIndicationPostProcessEnabled == other.hitIndicationPostProcessEnabled;
        }

    }

    public class CFGGameplayMapData
    {
        public bool ignoreMapOwnership { get; set; }
        public bool ignoreNavItemsOwnership { get; set; }
        public bool displayPlayerPosition { get; set; }
        public bool displayNavInfo { get; set; }

        public CFGGameplayMapData()
        {
            ignoreMapOwnership = false;
            ignoreNavItemsOwnership = false;
            displayPlayerPosition = false;
            displayNavInfo = true;
        }
        public override bool Equals(object obj)
        {
            if (obj is not CFGGameplayMapData other)
                return false;

            return ignoreMapOwnership == other.ignoreMapOwnership &&
                   ignoreNavItemsOwnership == other.ignoreNavItemsOwnership &&
                   displayPlayerPosition == other.displayPlayerPosition &&
                   displayNavInfo == other.displayNavInfo;
        }
    }

    public class VehicleData
    {
        public decimal boatDecayMultiplier { get; set; }

        public VehicleData()
        {
            boatDecayMultiplier = 1;
        }
        public override bool Equals(object obj)
        {
            if (obj is not VehicleData other)
                return false;

            return boatDecayMultiplier == other.boatDecayMultiplier;
        }
    }
}
