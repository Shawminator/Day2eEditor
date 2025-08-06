﻿using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using static FileService;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Day2eEditor
{
    public class CFGGameplayConfig: IConfigLoader
    {
        private readonly string _path;

        public cfggameplay Data { get; private set; } = new cfggameplay();
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool IsDirty { get; set; }

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
        }

        public void Save()
        {
            UpdateSpawnGearFileList();
            UpdateRestrictedFileList();

            AppServices.GetRequired<FileService>().SaveJson(_path, Data);

            SaveSpawnGearPresetFiles();
            SavePlayerRestrictedAreaFiles();

            IsDirty = false;
        }

        #region Spawn Gear Files

        private void LoadSpawnGearFiles()
        {
            Data.SpawnGearPresetFiles = new BindingList<SpawnGearPresetFiles>();
            Console.WriteLine("## Starting SpawnGearPresets ##");

            foreach (string filename in Data.PlayerData.spawnGearPresetFiles)
            {
                var fullPath = Path.Combine(BaseDirectory, filename);
                var preset = AppServices.GetRequired<FileService>().LoadOrCreateJson<SpawnGearPresetFiles>(
                    fullPath,
                    createNew: () => new SpawnGearPresetFiles(),
                    onAfterLoad: _ => { },
                    checkVersionAndUpdate: _ => false,
                    onError: ex => LogError(filename, ex),
                    configName: "SpawnGearPreset",
                    useBoolConvertor: false
                );

                preset.Filename = filename;
                Data.SpawnGearPresetFiles.Add(preset);
            }

            Console.WriteLine("## End SpawnGearPresets ##");
        }

        private void SaveSpawnGearPresetFiles()
        {
            foreach (var preset in Data.SpawnGearPresetFiles)
            {
                if (!preset.isDirty) continue;

                var fullPath = Path.Combine(BaseDirectory, preset.Filename);
                AppServices.GetRequired<FileService>().SaveJson(fullPath, preset);
                preset.isDirty = false;
            }
        }

        private void UpdateSpawnGearFileList()
        {
            Data.PlayerData.spawnGearPresetFiles = new BindingList<string>();
            foreach (var preset in Data.SpawnGearPresetFiles)
            {
                if (!string.IsNullOrWhiteSpace(preset.Filename))
                    Data.PlayerData.spawnGearPresetFiles.Add(preset.Filename);
            }
        }

        #endregion

        #region Restricted Area Files

        private void LoadRestrictedFiles()
        {
            Data.RestrictedAreaFiles = new BindingList<PlayerRestrictedFiles>();
            Console.WriteLine("## Starting Restricted Area Files ##");

            foreach (string filename in Data.WorldsData.playerRestrictedAreaFiles)
            {
                var fullPath = Path.Combine(BaseDirectory, filename);
                var restricted = AppServices.GetRequired<FileService>().LoadOrCreateJson<PlayerRestrictedFiles>(
                    fullPath,
                    createNew: () => new PlayerRestrictedFiles(),
                    onAfterLoad: _ => { },
                    checkVersionAndUpdate: _ => false,
                    onError: ex => LogError(filename, ex),
                    configName: "RestrictedArea",
                    useBoolConvertor: false
                );

                restricted.Filename = filename;
                Data.RestrictedAreaFiles.Add(restricted);
            }

            Console.WriteLine("## End Restricted Area Files ##");
        }

        private void SavePlayerRestrictedAreaFiles()
        {
            foreach (var restricted in Data.RestrictedAreaFiles)
            {
                if (!restricted.isDirty) continue;

                var fullPath = Path.Combine(BaseDirectory, restricted.Filename);
                AppServices.GetRequired<FileService>().SaveJson(fullPath, restricted);
                restricted.isDirty = false;
            }
        }

        private void UpdateRestrictedFileList()
        {
            Data.WorldsData.playerRestrictedAreaFiles = new BindingList<string>();
            foreach (var restricted in Data.RestrictedAreaFiles)
            {
                if (!string.IsNullOrWhiteSpace(restricted.Filename))
                    Data.WorldsData.playerRestrictedAreaFiles.Add(restricted.Filename);
            }
        }

        #endregion

        public void AddNewObjectSpawner(string spawner)
        {
            if (!string.IsNullOrWhiteSpace(spawner))
            {
                Data.WorldsData.objectSpawnersArr.Add(spawner);
                IsDirty = true;
                Save();
            }
        }

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
        [JsonIgnore]
        public BindingList<SpawnGearPresetFiles> SpawnGearPresetFiles { get; set; }
        [JsonIgnore]
        public BindingList<PlayerRestrictedFiles> RestrictedAreaFiles { get; set; }

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
            SpawnGearPresetFiles = new BindingList<SpawnGearPresetFiles>();
            RestrictedAreaFiles = new BindingList<PlayerRestrictedFiles>();
        }

        public void Addnewspawngearfile(string filename)
        {
            SpawnGearPresetFiles newSGPF = new SpawnGearPresetFiles()
            {
                isDirty = true,
                Filename = filename,
                name = Path.GetFileNameWithoutExtension(filename),
                spawnWeight = 1,
                characterTypes = new BindingList<string>(),
                attachmentSlotItemSets = new BindingList<Attachmentslotitemset>(),
                discreteUnsortedItemSets = new BindingList<Discreteunsorteditemset>(),
            };
            SpawnGearPresetFiles.Add(newSGPF);
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
    }

    public class VehicleData
    {
        public decimal boatDecayMultiplier { get; set; }

        public VehicleData()
        {
            boatDecayMultiplier = 1;
        }
    }

    public class SpawnGearPresetFiles
    {
        public int spawnWeight { get; set; }
        public string name { get; set; }
        public BindingList<string> characterTypes { get; set; }
        public BindingList<Attachmentslotitemset> attachmentSlotItemSets { get; set; }
        public BindingList<Discreteunsorteditemset> discreteUnsortedItemSets { get; set; }

        [JsonIgnore]
        public string Filename { get; set; }
        [JsonIgnore]
        public bool isDirty { get; set; }

        public SpawnGearPresetFiles()
        {
            spawnWeight = 1;
            name = "";
            characterTypes = new BindingList<string>();
            attachmentSlotItemSets = new BindingList<Attachmentslotitemset>();
            discreteUnsortedItemSets = new BindingList<Discreteunsorteditemset>();
        }
        public void SaveFile()
        {

        }
        public override string ToString()
        {
            return name;
        }
    }

    public class Attachmentslotitemset
    {
        public string slotName { get; set; }
        public BindingList<Discreteitemset> discreteItemSets { get; set; }
    }

    public class Discreteitemset
    {
        public string itemType { get; set; }
        public int spawnWeight { get; set; }
        public Attributes attributes { get; set; }
        public int quickBarSlot { get; set; }
        public BindingList<Complexchildrentype> complexChildrenTypes { get; set; }
        public bool simpleChildrenUseDefaultAttributes { get; set; }
        public BindingList<string> simpleChildrenTypes { get; set; }

        public Discreteitemset()
        {
            attributes = new Attributes();
            complexChildrenTypes = new BindingList<Complexchildrentype>();
            simpleChildrenTypes = new BindingList<string>();
        }
    }

    public class Attributes
    {
        public decimal healthMin { get; set; }
        public decimal healthMax { get; set; }
        public decimal quantityMin { get; set; }
        public decimal quantityMax { get; set; }
    }

    public class Complexchildrentype
    {
        public string itemType { get; set; }
        public Attributes attributes { get; set; }
        public int quickBarSlot { get; set; }
        public bool simpleChildrenUseDefaultAttributes { get; set; }
        public BindingList<string> simpleChildrenTypes { get; set; }

        public Complexchildrentype()
        {
            attributes = new Attributes();
            simpleChildrenTypes = new BindingList<string>();
        }
    }

    public class Discreteunsorteditemset
    {
        public string name { get; set; }
        public int spawnWeight { get; set; }
        public Attributes attributes { get; set; }
        public BindingList<Complexchildrentype> complexChildrenTypes { get; set; }
        public bool simpleChildrenUseDefaultAttributes { get; set; }
        public BindingList<string> simpleChildrenTypes { get; set; }

        public Discreteunsorteditemset()
        {
            attributes = new Attributes();
            complexChildrenTypes = new BindingList<Complexchildrentype>();
            simpleChildrenTypes = new BindingList<string>();
        }
    }

    public class ObjectSpawnerArr
    {
        [JsonIgnore]
        public string Filename { get; set; }
        public BindingList<SpawnObjects> Objects { get; set; }
    }

    public class SpawnObjects
    {
        public string name { get; set; }
        public float[] pos { get; set; }
        public float[] ypr { get; set; }
        public float scale { get; set; }
        public bool enableCEPersistency { get; set; }
    }

    public class PlayerRestrictedFiles
    {
        public string areaName { get; set; }
        public BindingList<float[][]> PRABoxes { get; set; }
        public BindingList<float[]> safePositions3D { get; set; }

        [JsonIgnore]
        public bool isDirty;
        [JsonIgnore]
        public string Filename { get; internal set; }
    }
}
