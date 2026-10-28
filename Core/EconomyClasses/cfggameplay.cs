using System.ComponentModel;
using System.IO;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace Day2eEditor
{
    public enum EWeaponObstructionMode
    {
        [Description("Obstruction disallowed. Weapon doesn't obstruct, but still lifts.")]
        DISABLED = 0,

        [Description("Obstruction allowed. Weapon first obstructs and then lifts.")]
        ENABLED = 1,

        [Description("Obstruction always. Weapon obstructs and never lifts.")]
        ALWAYS = 2
    }
    public class CFGGameplayConfig : SingleFileConfigLoaderBase<cfggameplay>
    {
        public SpawnGearPresetsConfig SpawnGearPresetconfigs { get; private set; }
        public PlayerRestrictedFilesConfig PlayerRestrictedFilesConfig { get; private set; }
        public ObjectSpawnerArrConfig ObjectSpawnerArrConfig { get; private set; }

        private string BaseDirectory => Path.GetDirectoryName(_path) ?? "";

        public CFGGameplayConfig(string path) : base(path)
        {
            SpawnGearPresetconfigs = new SpawnGearPresetsConfig(BaseDirectory);
            PlayerRestrictedFilesConfig = new PlayerRestrictedFilesConfig(BaseDirectory);
            ObjectSpawnerArrConfig = new ObjectSpawnerArrConfig(BaseDirectory);
        }

        public override void Load()
        {
            HasErrors = false;
            _errors.Clear();

            try
            {
                Data = AppServices.GetRequired<FileService>().LoadOrCreateJson<cfggameplay>(
                    _path,
                    createNew: () => new cfggameplay(),
                    onError: ex => HandleLoadError(ex),
                    configName: "CFGGameplay",
                    useBoolConvertor: false
                );

                // Validate first so missing sections exist before the sub-configs read them
                var issues = ValidateData();
                if (issues?.Any() == true)
                {
                    Console.WriteLine("Validation issues in " + FileName + ":");
                    foreach (var msg in issues)
                        Console.WriteLine("- " + msg);

                    MarkDirty();
                }

                SpawnGearPresetconfigs = new SpawnGearPresetsConfig(BaseDirectory);
                SpawnGearPresetconfigs.Load(BaseDirectory, Data.PlayerData.spawnGearPresetFiles);

                PlayerRestrictedFilesConfig = new PlayerRestrictedFilesConfig(BaseDirectory);
                PlayerRestrictedFilesConfig.Load(BaseDirectory, Data.WorldsData.playerRestrictedAreaFiles);

                ObjectSpawnerArrConfig = new ObjectSpawnerArrConfig(BaseDirectory);
                ObjectSpawnerArrConfig.Load(BaseDirectory, Data.WorldsData.objectSpawnersArr);

                OnAfterLoad(Data);
                ClonedData = CloneData(Data);
            }
            catch (Exception ex)
            {
                HandleLoadError(ex);
            }
        }

        protected override cfggameplay CreateDefaultData()
        {
            return new cfggameplay();
        }
        protected override IEnumerable<string> ValidateData()
        {
            return Data.FixMissingOrInvalidFields();
        }
        public override IEnumerable<string> Save()
        {
            var fileNames = new List<string>();

            if (Data is null)
                return Array.Empty<string>();

            if (!AreEqual(Data, ClonedData) || IsDirty)
            {
                UpdateSpawnGearFileList();
                UpdateRestrictedFileList();
                UpdateObjectSpawnerArrList();

                AppServices.GetRequired<FileService>().SaveJson(_path, Data);
                ClonedData = CloneData(Data);
                ClearDirty();
                fileNames.Add(_path);
            }

            fileNames.AddRange(SpawnGearPresetconfigs.Save());
            fileNames.AddRange(PlayerRestrictedFilesConfig.Save());
            fileNames.AddRange(ObjectSpawnerArrConfig.Save());

            return fileNames;
        }

        public override bool NeedToSave()
        {
            if (base.NeedToSave())
                return true;

            return SpawnGearPresetconfigs.NeedToSave()
                || PlayerRestrictedFilesConfig.NeedToSave()
                || ObjectSpawnerArrConfig.NeedToSave();
        }

        private void UpdateSpawnGearFileList()
        {
            Data.PlayerData.spawnGearPresetFiles = new BindingList<string>();

            foreach (var preset in SpawnGearPresetconfigs.Items)
            {
                if (preset.ToDelete)
                    continue;

                var relativePath = Path.Combine(preset.ModFolder, preset.FileName)
                    .Replace("\\", "/")
                    .Replace("//", "/");

                Data.PlayerData.spawnGearPresetFiles.Add(relativePath);
            }
        }

        private void UpdateRestrictedFileList()
        {
            Data.WorldsData.playerRestrictedAreaFiles = new BindingList<string>();

            foreach (var restricted in PlayerRestrictedFilesConfig.Items)
            {
                if (restricted.ToDelete)
                    continue;

                var relativePath = Path.Combine(restricted.ModFolder, restricted.FileName)
                    .Replace("\\", "/")
                    .Replace("//", "/");

                Data.WorldsData.playerRestrictedAreaFiles.Add(relativePath);
            }
        }

        private void UpdateObjectSpawnerArrList()
        {
            Data.WorldsData.objectSpawnersArr = new BindingList<string>();

            foreach (var obj in ObjectSpawnerArrConfig.Items)
            {
                if (obj.ToDelete)
                    continue;

                var relativePath = Path.Combine(obj.ModFolder, obj.FileName)
                    .Replace("\\", "/")
                    .Replace("//", "/");

                Data.WorldsData.objectSpawnersArr.Add(relativePath);
            }
        }

        public SpawnGearPresetFile? GetSpawnGearPreset(string spawnFile)
        {
            var filename = Path.GetFileName(spawnFile);
            var modpath = Path.GetDirectoryName(spawnFile) ?? string.Empty;

            return SpawnGearPresetconfigs.Items.FirstOrDefault(x =>
                x.FileName == filename && x.ModFolder == modpath);
        }

        public bool AddNewSpawnGear(SpawnGearPresetFile newFile)
        {
            var relativePath = Path.Combine(newFile.ModFolder, newFile.FileName).Replace("\\", "/");

            if (Data.PlayerData.spawnGearPresetFiles.Contains(relativePath))
                return false;

            Data.PlayerData.spawnGearPresetFiles.Add(relativePath);
            SpawnGearPresetconfigs.MutableItems.Add(newFile);
            MarkDirty();
            return true;
        }

        public void RemoveSpawnGearPreset(SpawnGearPresetFile spawnGearPreset)
        {
            var relativePath = Path.Combine(spawnGearPreset.ModFolder, spawnGearPreset.FileName).Replace("\\", "/");
            Data.PlayerData.spawnGearPresetFiles.Remove(relativePath);
            spawnGearPreset.ToDelete = true;
            MarkDirty();
        }

        public PlayerRestrictedFile? GetRestrictedFiles(string restrictedFile)
        {
            var filename = Path.GetFileName(restrictedFile);
            var modpath = Path.GetDirectoryName(restrictedFile) ?? string.Empty;

            return PlayerRestrictedFilesConfig.Items.FirstOrDefault(x =>
                x.FileName == filename && x.ModFolder == modpath);
        }

        public bool AddNewPlayerRestrictedAreaFile(PlayerRestrictedFile newFile)
        {
            var relativePath = Path.Combine(newFile.ModFolder, newFile.FileName).Replace("\\", "/");

            if (Data.WorldsData.playerRestrictedAreaFiles.Contains(relativePath))
                return false;

            Data.WorldsData.playerRestrictedAreaFiles.Add(relativePath);
            PlayerRestrictedFilesConfig.MutableItems.Add(newFile);
            MarkDirty();
            return true;
        }

        public void RemovePlayerRestrictedAreaFile(PlayerRestrictedFile restrictedFile)
        {
            var relativePath = Path.Combine(restrictedFile.ModFolder, restrictedFile.FileName).Replace("\\", "/");
            Data.WorldsData.playerRestrictedAreaFiles.Remove(relativePath);
            restrictedFile.ToDelete = true;
            MarkDirty();
        }

        public ObjectSpawnerArrFile? GetObjectSpawnerFiles(string objectSpawnerArrFile)
        {
            var filename = Path.GetFileName(objectSpawnerArrFile);
            var modpath = Path.GetDirectoryName(objectSpawnerArrFile) ?? string.Empty;

            return ObjectSpawnerArrConfig.Items.FirstOrDefault(x =>
                x.FileName == filename && x.ModFolder == modpath);
        }

        public bool AddNewObjectSpawnerArrFile(ObjectSpawnerArrFile newFile)
        {
            var relativePath = Path.Combine(newFile.ModFolder, newFile.FileName).Replace("\\", "/");

            if (Data.WorldsData.objectSpawnersArr.Contains(relativePath))
                return false;

            Data.WorldsData.objectSpawnersArr.Add(relativePath);
            ObjectSpawnerArrConfig.MutableItems.Add(newFile);
            MarkDirty();
            return true;
        }

        public void RemoveObjectSpawnerArrFile(ObjectSpawnerArrFile objectSpawnerArr)
        {
            var relativePath = Path.Combine(objectSpawnerArr.ModFolder, objectSpawnerArr.FileName).Replace("\\", "/");
            Data.WorldsData.objectSpawnersArr.Remove(relativePath);
            objectSpawnerArr.ToDelete = true;
            MarkDirty();
        }
    }


    public class cfggameplay : IDeepCloneable<cfggameplay>, IEquatable<cfggameplay>
    {
        public int version { get; set; }
        public ITEM_GeneralData GeneralData { get; set; }
        public ITEM_PlayerData PlayerData { get; set; }
        public ITEM_WorldData WorldsData { get; set; }
        public ITEM_BaseBuildingData BaseBuildingData { get; set; }
        public ITEM_UIData UIData { get; set; }
        public ITEM_MapData MapData { get; set; }
        public ITEM_VehicleData VehicleData { get; set; }
        public ITEM_ExternalLockData ExternalLockData { get; set; }
        public ITEM_SandstormData SandstormData { get; set; }

        [JsonIgnore]
        private const int currentversion = 130;

        public cfggameplay()
        {
            version = currentversion;
            GeneralData = new ITEM_GeneralData();
            PlayerData = new ITEM_PlayerData();
            WorldsData = new ITEM_WorldData();
            BaseBuildingData = new ITEM_BaseBuildingData();
            UIData = new ITEM_UIData();
            MapData = new ITEM_MapData();
            VehicleData = new ITEM_VehicleData();
            ExternalLockData = new ITEM_ExternalLockData();
            SandstormData = new ITEM_SandstormData();
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

        public cfggameplay Clone()
        {
            return new cfggameplay
            {
                version = version,
                GeneralData = GeneralData?.Clone() ?? new ITEM_GeneralData(),
                PlayerData = PlayerData?.Clone() ?? new ITEM_PlayerData(),
                WorldsData = WorldsData?.Clone() ?? new ITEM_WorldData(),
                BaseBuildingData = BaseBuildingData?.Clone() ?? new ITEM_BaseBuildingData(),
                UIData = UIData?.Clone() ?? new ITEM_UIData(),
                MapData = MapData?.Clone() ?? new ITEM_MapData(),
                VehicleData = VehicleData?.Clone() ?? new ITEM_VehicleData(),
                ExternalLockData = ExternalLockData?.Clone() ?? new ITEM_ExternalLockData(),
                SandstormData = SandstormData?.Clone() ?? new ITEM_SandstormData()
            };
        }

        public bool Equals(cfggameplay? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return version == other.version &&
                   Equals(GeneralData, other.GeneralData) &&
                   Equals(PlayerData, other.PlayerData) &&
                   Equals(WorldsData, other.WorldsData) &&
                   Equals(BaseBuildingData, other.BaseBuildingData) &&
                   Equals(UIData, other.UIData) &&
                   Equals(MapData, other.MapData) &&
                   Equals(VehicleData, other.VehicleData) &&
                   Equals(ExternalLockData, other.ExternalLockData) &&
                   Equals(SandstormData, other.SandstormData);
        }

        public override bool Equals(object? obj) => Equals(obj as cfggameplay);

        internal IEnumerable<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (version != currentversion)
            {
                fixes.Add($"Updated version from {version} to {currentversion}");
                version = currentversion;
            }

            // Top level sections
            if (GeneralData == null) { GeneralData = new ITEM_GeneralData(); fixes.Add("GeneralData was missing and has been set to defaults."); }
            if (PlayerData == null) { PlayerData = new ITEM_PlayerData(); fixes.Add("PlayerData was missing and has been set to defaults."); }
            if (WorldsData == null) { WorldsData = new ITEM_WorldData(); fixes.Add("WorldsData was missing and has been set to defaults."); }
            if (BaseBuildingData == null) { BaseBuildingData = new ITEM_BaseBuildingData(); fixes.Add("BaseBuildingData was missing and has been set to defaults."); }
            if (UIData == null) { UIData = new ITEM_UIData(); fixes.Add("UIData was missing and has been set to defaults."); }
            if (MapData == null) { MapData = new ITEM_MapData(); fixes.Add("MapData was missing and has been set to defaults."); }
            if (VehicleData == null) { VehicleData = new ITEM_VehicleData(); fixes.Add("VehicleData was missing and has been set to defaults."); }
            if (ExternalLockData == null) { ExternalLockData = new ITEM_ExternalLockData(); fixes.Add("ExternalLockData was missing and has been set to defaults."); }
            if (SandstormData == null) { SandstormData = new ITEM_SandstormData(); fixes.Add("SandstormData was missing and has been set to defaults."); }

            // PlayerData
            if (PlayerData.spawnGearPresetFiles == null) { PlayerData.spawnGearPresetFiles = new BindingList<string>(); fixes.Add("PlayerData.spawnGearPresetFiles was missing and has been set to an empty list."); }
            if (PlayerData.StaminaData == null) { PlayerData.StaminaData = new ITEM_StaminaData(); fixes.Add("PlayerData.StaminaData was missing and has been set to defaults."); }
            if (PlayerData.ShockHandlingData == null) { PlayerData.ShockHandlingData = new ITEM_ShockHandlingData(); fixes.Add("PlayerData.ShockHandlingData was missing and has been set to defaults."); }
            if (PlayerData.MovementData == null) { PlayerData.MovementData = new ITEM_MovementData(); fixes.Add("PlayerData.MovementData was missing and has been set to defaults."); }
            if (PlayerData.DrowningData == null) { PlayerData.DrowningData = new ITEM_DrowningData(); fixes.Add("PlayerData.DrowningData was missing and has been set to defaults."); }
            if (PlayerData.WeaponObstructionData == null) { PlayerData.WeaponObstructionData = new ITEM_WeaponObstructionData(); fixes.Add("PlayerData.WeaponObstructionData was missing and has been set to defaults."); }

            if (!Enum.IsDefined(PlayerData.WeaponObstructionData.staticMode))
            {
                fixes.Add($"PlayerData.WeaponObstructionData.staticMode had invalid value {(int)PlayerData.WeaponObstructionData.staticMode} and was reset to ENABLED.");
                PlayerData.WeaponObstructionData.staticMode = EWeaponObstructionMode.ENABLED;
            }
            if (!Enum.IsDefined(PlayerData.WeaponObstructionData.dynamicMode))
            {
                fixes.Add($"PlayerData.WeaponObstructionData.dynamicMode had invalid value {(int)PlayerData.WeaponObstructionData.dynamicMode} and was reset to ENABLED.");
                PlayerData.WeaponObstructionData.dynamicMode = EWeaponObstructionMode.ENABLED;
            }

            // WorldsData
            var worldDefaults = new ITEM_WorldData();
            if (WorldsData.lightingConfig is not (0 or 1))
            {
                fixes.Add($"WorldsData.lightingConfig had invalid value {WorldsData.lightingConfig} and was reset to {worldDefaults.lightingConfig}.");
                WorldsData.lightingConfig = worldDefaults.lightingConfig;
            }
            if (WorldsData.objectSpawnersArr == null) { WorldsData.objectSpawnersArr = new BindingList<string>(); fixes.Add("WorldsData.objectSpawnersArr was missing and has been set to an empty list."); }
            if (WorldsData.playerRestrictedAreaFiles == null) { WorldsData.playerRestrictedAreaFiles = new BindingList<string>(); fixes.Add("WorldsData.playerRestrictedAreaFiles was missing and has been set to an empty list."); }
            if (WorldsData.environmentMinTemps?.Count != 12)
            {
                fixes.Add("WorldsData.environmentMinTemps was missing or did not have 12 monthly values and was reset to defaults.");
                WorldsData.environmentMinTemps = worldDefaults.environmentMinTemps;
            }
            if (WorldsData.environmentMaxTemps?.Count != 12)
            {
                fixes.Add("WorldsData.environmentMaxTemps was missing or did not have 12 monthly values and was reset to defaults.");
                WorldsData.environmentMaxTemps = worldDefaults.environmentMaxTemps;
            }
            if (WorldsData.wetnessWeightModifiers?.Count != 5)
            {
                fixes.Add("WorldsData.wetnessWeightModifiers was missing or did not have 5 values and was reset to defaults.");
                WorldsData.wetnessWeightModifiers = worldDefaults.wetnessWeightModifiers;
            }

            // BaseBuildingData
            if (BaseBuildingData.HologramData == null) { BaseBuildingData.HologramData = new Hologramdata(); fixes.Add("BaseBuildingData.HologramData was missing and has been set to defaults."); }
            if (BaseBuildingData.ConstructionData == null) { BaseBuildingData.ConstructionData = new Constructiondata(); fixes.Add("BaseBuildingData.ConstructionData was missing and has been set to defaults."); }
            if (BaseBuildingData.HologramData.disallowedTypesInUnderground == null)
            {
                BaseBuildingData.HologramData.disallowedTypesInUnderground = new Hologramdata().disallowedTypesInUnderground;
                fixes.Add("BaseBuildingData.HologramData.disallowedTypesInUnderground was missing and has been set to defaults.");
            }

            // UIData
            if (UIData.HitIndicationData == null) { UIData.HitIndicationData = new ITEM_HitIndicationData(); fixes.Add("UIData.HitIndicationData was missing and has been set to defaults."); }
            var hitDefaults = new ITEM_HitIndicationData();
            var hit = UIData.HitIndicationData;
            if (hit.hitDirectionBehaviour is < 0 or > 2)
            {
                fixes.Add($"UIData.HitIndicationData.hitDirectionBehaviour had invalid value {hit.hitDirectionBehaviour} and was reset to {hitDefaults.hitDirectionBehaviour}.");
                hit.hitDirectionBehaviour = hitDefaults.hitDirectionBehaviour;
            }
            if (hit.hitDirectionStyle is < 0 or > 2)
            {
                fixes.Add($"UIData.HitIndicationData.hitDirectionStyle had invalid value {hit.hitDirectionStyle} and was reset to {hitDefaults.hitDirectionStyle}.");
                hit.hitDirectionStyle = hitDefaults.hitDirectionStyle;
            }
            if (string.IsNullOrWhiteSpace(hit.hitDirectionIndicatorColorStr))
            {
                fixes.Add($"UIData.HitIndicationData.hitDirectionIndicatorColorStr was missing and was reset to {hitDefaults.hitDirectionIndicatorColorStr}.");
                hit.hitDirectionIndicatorColorStr = hitDefaults.hitDirectionIndicatorColorStr;
            }

            return fixes;
        }
    }

    public class ITEM_GeneralData : IDeepCloneable<ITEM_GeneralData>, IEquatable<ITEM_GeneralData>
    {
        public bool disableBaseDamage { get; set; }
        public bool disableBaseDecay { get; set; }
        public bool disableContainerDamage { get; set; }
        public bool disableRespawnDialog { get; set; }
        public bool disableRespawnInUnconsciousness { get; set; }

        public ITEM_GeneralData()
        {
            disableBaseDamage = false;
            disableBaseDecay = false;
            disableContainerDamage = false;
            disableRespawnDialog = false;
            disableRespawnInUnconsciousness = false;
        }

        public ITEM_GeneralData Clone()
        {
            return new ITEM_GeneralData
            {
                disableBaseDamage = disableBaseDamage,
                disableBaseDecay = disableBaseDecay,
                disableContainerDamage = disableContainerDamage,
                disableRespawnDialog = disableRespawnDialog,
                disableRespawnInUnconsciousness = disableRespawnInUnconsciousness
            };
        }

        public bool Equals(ITEM_GeneralData? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return disableBaseDamage == other.disableBaseDamage &&
                    disableBaseDecay == other.disableBaseDecay &&
                   disableContainerDamage == other.disableContainerDamage &&
                   disableRespawnDialog == other.disableRespawnDialog &&
                   disableRespawnInUnconsciousness == other.disableRespawnInUnconsciousness;
        }

        public override bool Equals(object? obj) => Equals(obj as ITEM_GeneralData);
    }

    public class ITEM_PlayerData : IDeepCloneable<ITEM_PlayerData>, IEquatable<ITEM_PlayerData>
    {
        public bool disablePersonalLight { get; set; }
        public BindingList<string> spawnGearPresetFiles { get; set; }
        public ITEM_StaminaData StaminaData { get; set; }
        public ITEM_ShockHandlingData ShockHandlingData { get; set; }
        public ITEM_MovementData MovementData { get; set; }
        public ITEM_DrowningData DrowningData { get; set; }
        public ITEM_WeaponObstructionData WeaponObstructionData { get; set; }

        public ITEM_PlayerData()
        {
            disablePersonalLight = true;
            spawnGearPresetFiles = new BindingList<string>();
            StaminaData = new ITEM_StaminaData();
            ShockHandlingData = new ITEM_ShockHandlingData();
            MovementData = new ITEM_MovementData();
            DrowningData = new ITEM_DrowningData();
            WeaponObstructionData = new ITEM_WeaponObstructionData();
        }

        public ITEM_PlayerData Clone()
        {
            return new ITEM_PlayerData
            {
                disablePersonalLight = disablePersonalLight,
                spawnGearPresetFiles = new BindingList<string>(spawnGearPresetFiles?.ToList() ?? new List<string>()),
                StaminaData = StaminaData?.Clone() ?? new ITEM_StaminaData(),
                ShockHandlingData = ShockHandlingData?.Clone() ?? new ITEM_ShockHandlingData(),
                MovementData = MovementData?.Clone() ?? new ITEM_MovementData(),
                DrowningData = DrowningData?.Clone() ?? new ITEM_DrowningData(),
                WeaponObstructionData = WeaponObstructionData?.Clone() ?? new ITEM_WeaponObstructionData()
            };
        }

        public bool Equals(ITEM_PlayerData? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return disablePersonalLight == other.disablePersonalLight &&
                   spawnGearPresetFiles.SequenceEqual(other.spawnGearPresetFiles) &&
                   Equals(StaminaData, other.StaminaData) &&
                   Equals(ShockHandlingData, other.ShockHandlingData) &&
                   Equals(MovementData, other.MovementData) &&
                   Equals(DrowningData, other.DrowningData) &&
                   Equals(WeaponObstructionData, other.WeaponObstructionData);
        }

        public override bool Equals(object? obj) => Equals(obj as ITEM_PlayerData);
    }

    public class ITEM_StaminaData : IDeepCloneable<ITEM_StaminaData>, IEquatable<ITEM_StaminaData>
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

        public ITEM_StaminaData()
        {
            sprintStaminaModifierErc = 1.0m;
            sprintStaminaModifierCro = 1.0m;
            staminaWeightLimitThreshold = 6000.0m;
            staminaMax = 100.0m;
            staminaKgToStaminaPercentPenalty = 1.75m;
            staminaMinCap = 5.0m;
            sprintSwimmingStaminaModifier = 1;
            sprintLadderStaminaModifier = 1;
            meleeStaminaModifier = 1;
            obstacleTraversalStaminaModifier = 1;
            holdBreathStaminaModifier = 1;
        }

        public ITEM_StaminaData Clone()
        {
            return new ITEM_StaminaData
            {
                sprintStaminaModifierErc = sprintStaminaModifierErc,
                sprintStaminaModifierCro = sprintStaminaModifierCro,
                staminaWeightLimitThreshold = staminaWeightLimitThreshold,
                staminaMax = staminaMax,
                staminaKgToStaminaPercentPenalty = staminaKgToStaminaPercentPenalty,
                staminaMinCap = staminaMinCap,
                sprintSwimmingStaminaModifier = sprintSwimmingStaminaModifier,
                sprintLadderStaminaModifier = sprintLadderStaminaModifier,
                meleeStaminaModifier = meleeStaminaModifier,
                obstacleTraversalStaminaModifier = obstacleTraversalStaminaModifier,
                holdBreathStaminaModifier = holdBreathStaminaModifier
            };
        }

        public bool Equals(ITEM_StaminaData? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

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

        public override bool Equals(object? obj) => Equals(obj as ITEM_StaminaData);
    }

    public class ITEM_ShockHandlingData : IDeepCloneable<ITEM_ShockHandlingData>, IEquatable<ITEM_ShockHandlingData>
    {
        public decimal shockRefillSpeedConscious { get; set; }
        public decimal shockRefillSpeedUnconscious { get; set; }
        public bool allowRefillSpeedModifier { get; set; }

        public ITEM_ShockHandlingData()
        {
            shockRefillSpeedConscious = 5.0m;
            shockRefillSpeedUnconscious = 1.0m;
            allowRefillSpeedModifier = true;
        }

        public ITEM_ShockHandlingData Clone()
        {
            return new ITEM_ShockHandlingData
            {
                shockRefillSpeedConscious = shockRefillSpeedConscious,
                shockRefillSpeedUnconscious = shockRefillSpeedUnconscious,
                allowRefillSpeedModifier = allowRefillSpeedModifier
            };
        }

        public bool Equals(ITEM_ShockHandlingData? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return shockRefillSpeedConscious == other.shockRefillSpeedConscious &&
                   shockRefillSpeedUnconscious == other.shockRefillSpeedUnconscious &&
                   allowRefillSpeedModifier == other.allowRefillSpeedModifier;
        }

        public override bool Equals(object? obj) => Equals(obj as ITEM_ShockHandlingData);
    }

    public class ITEM_MovementData : IDeepCloneable<ITEM_MovementData>, IEquatable<ITEM_MovementData>
    {
        public decimal timeToStrafeJog { get; set; }
        public decimal rotationSpeedJog { get; set; }
        public decimal timeToSprint { get; set; }
        public decimal timeToStrafeSprint { get; set; }
        public decimal rotationSpeedSprint { get; set; }
        public bool allowStaminaAffectInertia { get; set; }

        public ITEM_MovementData()
        {
            timeToStrafeJog = 0.1m;
            rotationSpeedJog = 0.3m;
            timeToSprint = 0.45m;
            timeToStrafeSprint = 0.3m;
            rotationSpeedSprint = 0.15m;
            allowStaminaAffectInertia = true;
        }

        public ITEM_MovementData Clone()
        {
            return new ITEM_MovementData
            {
                timeToStrafeJog = timeToStrafeJog,
                rotationSpeedJog = rotationSpeedJog,
                timeToSprint = timeToSprint,
                timeToStrafeSprint = timeToStrafeSprint,
                rotationSpeedSprint = rotationSpeedSprint,
                allowStaminaAffectInertia = allowStaminaAffectInertia
            };
        }

        public bool Equals(ITEM_MovementData? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return timeToStrafeJog == other.timeToStrafeJog &&
                   rotationSpeedJog == other.rotationSpeedJog &&
                   timeToSprint == other.timeToSprint &&
                   timeToStrafeSprint == other.timeToStrafeSprint &&
                   rotationSpeedSprint == other.rotationSpeedSprint &&
                   allowStaminaAffectInertia == other.allowStaminaAffectInertia;
        }

        public override bool Equals(object? obj) => Equals(obj as ITEM_MovementData);
    }

    public class ITEM_DrowningData : IDeepCloneable<ITEM_DrowningData>, IEquatable<ITEM_DrowningData>
    {
        public decimal staminaDepletionSpeed { get; set; }
        public decimal healthDepletionSpeed { get; set; }
        public decimal shockDepletionSpeed { get; set; }

        public ITEM_DrowningData()
        {
            staminaDepletionSpeed = 10.0m;
            healthDepletionSpeed = 10.0m;
            shockDepletionSpeed = 10.0m;
        }

        public ITEM_DrowningData Clone()
        {
            return new ITEM_DrowningData
            {
                staminaDepletionSpeed = staminaDepletionSpeed,
                healthDepletionSpeed = healthDepletionSpeed,
                shockDepletionSpeed = shockDepletionSpeed
            };
        }

        public bool Equals(ITEM_DrowningData? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return staminaDepletionSpeed == other.staminaDepletionSpeed &&
                   healthDepletionSpeed == other.healthDepletionSpeed &&
                   shockDepletionSpeed == other.shockDepletionSpeed;
        }

        public override bool Equals(object? obj) => Equals(obj as ITEM_DrowningData);
    }

    public class ITEM_WeaponObstructionData : IDeepCloneable<ITEM_WeaponObstructionData>, IEquatable<ITEM_WeaponObstructionData>
    {
        public EWeaponObstructionMode staticMode { get; set; }
        public EWeaponObstructionMode dynamicMode { get; set; }

        public ITEM_WeaponObstructionData()
        {
            staticMode = EWeaponObstructionMode.ENABLED;
            dynamicMode = EWeaponObstructionMode.ENABLED;
        }

        public ITEM_WeaponObstructionData Clone()
        {
            return new ITEM_WeaponObstructionData
            {
                staticMode = staticMode,
                dynamicMode = dynamicMode
            };
        }

        public bool Equals(ITEM_WeaponObstructionData? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return staticMode == other.staticMode &&
                   dynamicMode == other.dynamicMode;
        }

        public override bool Equals(object? obj) => Equals(obj as ITEM_WeaponObstructionData);
    }

    public class ITEM_WorldData : IDeepCloneable<ITEM_WorldData>, IEquatable<ITEM_WorldData>
    {
        public int lightingConfig { get; set; }
        public bool bunkerBroadcastEnabled { get; set; }
        public BindingList<string> objectSpawnersArr { get; set; }
        public BindingList<decimal> environmentMinTemps { get; set; }
        public BindingList<decimal> environmentMaxTemps { get; set; }
        public BindingList<decimal> wetnessWeightModifiers { get; set; }
        public BindingList<string> playerRestrictedAreaFiles { get; set; }

        public ITEM_WorldData()
        {
            lightingConfig = 1;
            objectSpawnersArr = new BindingList<string>();
            decimal[] mintemp = { -3, -2, 0, 4, 9, 14, 18, 17, 12, 7, 4, 0 };
            decimal[] maxtemp = { 3, 5, 7, 14, 19, 24, 26, 25, 21, 16, 10, 5 };
            decimal[] wetness = { 1.0m, 1.0m, 1.33m, 1.66m, 2.0m };
            environmentMaxTemps = new BindingList<decimal>(maxtemp.ToList());
            environmentMinTemps = new BindingList<decimal>(mintemp.ToList());
            wetnessWeightModifiers = new BindingList<decimal>(wetness.ToList());
            playerRestrictedAreaFiles = new BindingList<string>();
        }

        public ITEM_WorldData Clone()
        {
            return new ITEM_WorldData
            {
                lightingConfig = lightingConfig,
                bunkerBroadcastEnabled = bunkerBroadcastEnabled,
                objectSpawnersArr = new BindingList<string>(objectSpawnersArr?.ToList() ?? new List<string>()),
                environmentMinTemps = new BindingList<decimal>(environmentMinTemps?.ToList() ?? new List<decimal>()),
                environmentMaxTemps = new BindingList<decimal>(environmentMaxTemps?.ToList() ?? new List<decimal>()),
                wetnessWeightModifiers = new BindingList<decimal>(wetnessWeightModifiers?.ToList() ?? new List<decimal>()),
                playerRestrictedAreaFiles = new BindingList<string>(playerRestrictedAreaFiles?.ToList() ?? new List<string>())
            };
        }

        public bool Equals(ITEM_WorldData? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return lightingConfig == other.lightingConfig &&
                   bunkerBroadcastEnabled == other.bunkerBroadcastEnabled &&
                   objectSpawnersArr.SequenceEqual(other.objectSpawnersArr) &&
                   environmentMinTemps.SequenceEqual(other.environmentMinTemps) &&
                   environmentMaxTemps.SequenceEqual(other.environmentMaxTemps) &&
                   wetnessWeightModifiers.SequenceEqual(other.wetnessWeightModifiers) &&
                   playerRestrictedAreaFiles.SequenceEqual(other.playerRestrictedAreaFiles);
        }

        public override bool Equals(object? obj) => Equals(obj as ITEM_WorldData);
    }

    public class ITEM_BaseBuildingData : IDeepCloneable<ITEM_BaseBuildingData>, IEquatable<ITEM_BaseBuildingData>
    {
        public Hologramdata HologramData { get; set; }
        public Constructiondata ConstructionData { get; set; }

        public ITEM_BaseBuildingData()
        {
            HologramData = new Hologramdata();
            ConstructionData = new Constructiondata();
        }

        public ITEM_BaseBuildingData Clone()
        {
            return new ITEM_BaseBuildingData
            {
                HologramData = HologramData?.Clone() ?? new Hologramdata(),
                ConstructionData = ConstructionData?.Clone() ?? new Constructiondata()
            };
        }

        public bool Equals(ITEM_BaseBuildingData? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(HologramData, other.HologramData) &&
                   Equals(ConstructionData, other.ConstructionData);
        }

        public override bool Equals(object? obj) => Equals(obj as ITEM_BaseBuildingData);
    }

    public class Hologramdata : IDeepCloneable<Hologramdata>, IEquatable<Hologramdata>
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
            disallowedTypesInUnderground = new BindingList<string>(new[] { "FenceKit", "TerritoryFlagKit", "WatchtowerKit" });
        }

        public Hologramdata Clone()
        {
            return new Hologramdata
            {
                disableIsCollidingBBoxCheck = disableIsCollidingBBoxCheck,
                disableIsCollidingPlayerCheck = disableIsCollidingPlayerCheck,
                disableIsClippingRoofCheck = disableIsClippingRoofCheck,
                disableIsBaseViableCheck = disableIsBaseViableCheck,
                disableIsCollidingGPlotCheck = disableIsCollidingGPlotCheck,
                disableIsCollidingAngleCheck = disableIsCollidingAngleCheck,
                disableIsPlacementPermittedCheck = disableIsPlacementPermittedCheck,
                disableHeightPlacementCheck = disableHeightPlacementCheck,
                disableIsUnderwaterCheck = disableIsUnderwaterCheck,
                disableIsInTerrainCheck = disableIsInTerrainCheck,
                disableColdAreaBuildingCheck = disableColdAreaBuildingCheck,
                disallowedTypesInUnderground = new BindingList<string>(
                    disallowedTypesInUnderground?.ToList() ?? new List<string>())
            };
        }

        public bool Equals(Hologramdata? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return disableIsCollidingBBoxCheck == other.disableIsCollidingBBoxCheck &&
                   disableIsCollidingPlayerCheck == other.disableIsCollidingPlayerCheck &&
                   disableIsClippingRoofCheck == other.disableIsClippingRoofCheck &&
                   disableIsBaseViableCheck == other.disableIsBaseViableCheck &&
                   disableIsCollidingGPlotCheck == other.disableIsCollidingGPlotCheck &&
                   disableIsCollidingAngleCheck == other.disableIsCollidingAngleCheck &&
                   disableIsPlacementPermittedCheck == other.disableIsPlacementPermittedCheck &&
                   disableHeightPlacementCheck == other.disableHeightPlacementCheck &&
                   disableIsUnderwaterCheck == other.disableIsUnderwaterCheck &&
                   disableIsInTerrainCheck == other.disableIsInTerrainCheck &&
                   disableColdAreaBuildingCheck == other.disableColdAreaBuildingCheck &&
                   disallowedTypesInUnderground.SequenceEqual(other.disallowedTypesInUnderground);
        }

        public override bool Equals(object? obj) => Equals(obj as Hologramdata);
    }

    public class Constructiondata : IDeepCloneable<Constructiondata>, IEquatable<Constructiondata>
    {
        public bool disablePerformRoofCheck { get; set; }
        public bool disableIsCollidingCheck { get; set; }
        public bool disableDistanceCheck { get; set; }
        public bool disableItemFalling {  get; set; }
        public bool disableDropAttachmentsOnFall { get; set; }
        public bool disableRefundMaterialsOnFall { get; set; }

        public Constructiondata()
        {
            disablePerformRoofCheck = false;
            disableIsCollidingCheck = false;
            disableDistanceCheck = false;
        }

        public Constructiondata Clone()
        {
            return new Constructiondata
            {
                disablePerformRoofCheck = disablePerformRoofCheck,
                disableIsCollidingCheck = disableIsCollidingCheck,
                disableDistanceCheck = disableDistanceCheck,
                disableItemFalling = disableItemFalling,
                disableDropAttachmentsOnFall = disableDropAttachmentsOnFall,
                disableRefundMaterialsOnFall = disableRefundMaterialsOnFall
            };
        }

        public bool Equals(Constructiondata? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return disablePerformRoofCheck == other.disablePerformRoofCheck &&
                   disableIsCollidingCheck == other.disableIsCollidingCheck &&
                   disableDistanceCheck == other.disableDistanceCheck &&
                   disableItemFalling == other.disableItemFalling &&
                   disableDropAttachmentsOnFall == other.disableDropAttachmentsOnFall &&
                   disableRefundMaterialsOnFall == other.disableRefundMaterialsOnFall;

        }

        public override bool Equals(object? obj) => Equals(obj as Constructiondata);
    }

    public class ITEM_UIData : IDeepCloneable<ITEM_UIData>, IEquatable<ITEM_UIData>
    {
        public bool use3DMap { get; set; }
        public ITEM_HitIndicationData HitIndicationData { get; set; }

        public ITEM_UIData()
        {
            use3DMap = false;
            HitIndicationData = new ITEM_HitIndicationData();
        }

        public ITEM_UIData Clone()
        {
            return new ITEM_UIData
            {
                use3DMap = use3DMap,
                HitIndicationData = HitIndicationData?.Clone() ?? new ITEM_HitIndicationData()
            };
        }

        public bool Equals(ITEM_UIData? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return use3DMap == other.use3DMap &&
                   Equals(HitIndicationData, other.HitIndicationData);
        }

        public override bool Equals(object? obj) => Equals(obj as ITEM_UIData);
    }

    public class ITEM_HitIndicationData : IDeepCloneable<ITEM_HitIndicationData>, IEquatable<ITEM_HitIndicationData>
    {
        public bool hitDirectionOverrideEnabled { get; set; }
        public int hitDirectionBehaviour { get; set; }
        public int hitDirectionStyle { get; set; }
        public string hitDirectionIndicatorColorStr { get; set; }
        public decimal hitDirectionMaxDuration { get; set; }
        public decimal hitDirectionBreakPointRelative { get; set; }
        public decimal hitDirectionScatter { get; set; }
        public bool hitIndicationPostProcessEnabled { get; set; }

        public ITEM_HitIndicationData()
        {
            hitDirectionOverrideEnabled = false;
            hitDirectionBehaviour = 1;
            hitDirectionStyle = 0;
            hitDirectionIndicatorColorStr = "0xffbb0a1e";
            hitDirectionMaxDuration = 2.0m;
            hitDirectionBreakPointRelative = 0.2m;
            hitDirectionScatter = 10.0m;
            hitIndicationPostProcessEnabled = true;
        }

        public ITEM_HitIndicationData Clone()
        {
            return new ITEM_HitIndicationData
            {
                hitDirectionOverrideEnabled = hitDirectionOverrideEnabled,
                hitDirectionBehaviour = hitDirectionBehaviour,
                hitDirectionStyle = hitDirectionStyle,
                hitDirectionIndicatorColorStr = hitDirectionIndicatorColorStr,
                hitDirectionMaxDuration = hitDirectionMaxDuration,
                hitDirectionBreakPointRelative = hitDirectionBreakPointRelative,
                hitDirectionScatter = hitDirectionScatter,
                hitIndicationPostProcessEnabled = hitIndicationPostProcessEnabled
            };
        }

        public bool Equals(ITEM_HitIndicationData? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return hitDirectionOverrideEnabled == other.hitDirectionOverrideEnabled &&
                   hitDirectionBehaviour == other.hitDirectionBehaviour &&
                   hitDirectionStyle == other.hitDirectionStyle &&
                   hitDirectionIndicatorColorStr == other.hitDirectionIndicatorColorStr &&
                   hitDirectionMaxDuration == other.hitDirectionMaxDuration &&
                   hitDirectionBreakPointRelative == other.hitDirectionBreakPointRelative &&
                   hitDirectionScatter == other.hitDirectionScatter &&
                   hitIndicationPostProcessEnabled == other.hitIndicationPostProcessEnabled;
        }

        public override bool Equals(object? obj) => Equals(obj as ITEM_HitIndicationData);
    }

    public class ITEM_MapData : IDeepCloneable<ITEM_MapData>, IEquatable<ITEM_MapData>
    {
        public bool ignoreMapOwnership { get; set; }
        public bool ignoreNavItemsOwnership { get; set; }
        public bool displayPlayerPosition { get; set; }
        public bool displayNavInfo { get; set; }

        public ITEM_MapData()
        {
            ignoreMapOwnership = false;
            ignoreNavItemsOwnership = false;
            displayPlayerPosition = false;
            displayNavInfo = true;
        }

        public ITEM_MapData Clone()
        {
            return new ITEM_MapData
            {
                ignoreMapOwnership = ignoreMapOwnership,
                ignoreNavItemsOwnership = ignoreNavItemsOwnership,
                displayPlayerPosition = displayPlayerPosition,
                displayNavInfo = displayNavInfo
            };
        }

        public bool Equals(ITEM_MapData? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return ignoreMapOwnership == other.ignoreMapOwnership &&
                   ignoreNavItemsOwnership == other.ignoreNavItemsOwnership &&
                   displayPlayerPosition == other.displayPlayerPosition &&
                   displayNavInfo == other.displayNavInfo;
        }

        public override bool Equals(object? obj) => Equals(obj as ITEM_MapData);
    }

    public class ITEM_VehicleData : IDeepCloneable<ITEM_VehicleData>, IEquatable<ITEM_VehicleData>
    {
        public decimal boatDecayMultiplier { get; set; }
        public bool canDetachAttachedCarWheels { get; set; }
        public bool canDamageAttachedCarWheels { get; set; }
        public bool canDetachAttachedBikeWheels { get; set; }
        public bool canDamageAttachedBikeWheels { get; set; }

        public ITEM_VehicleData()
        {
            boatDecayMultiplier = 1;
            canDetachAttachedCarWheels = true;
            canDamageAttachedCarWheels = true;
            canDetachAttachedBikeWheels = true;
            canDamageAttachedBikeWheels = true;
        }

        public ITEM_VehicleData Clone()
        {
            return new ITEM_VehicleData
            {
                boatDecayMultiplier = boatDecayMultiplier,
                canDetachAttachedCarWheels = canDetachAttachedCarWheels,
                canDamageAttachedCarWheels = canDamageAttachedCarWheels,
                canDetachAttachedBikeWheels = canDetachAttachedBikeWheels,
                canDamageAttachedBikeWheels = canDamageAttachedBikeWheels
            };
        }

        public bool Equals(ITEM_VehicleData? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (boatDecayMultiplier != other.boatDecayMultiplier ||
                canDetachAttachedCarWheels != other.canDetachAttachedCarWheels ||
                canDamageAttachedCarWheels != other.canDamageAttachedCarWheels ||
                canDetachAttachedBikeWheels != other.canDetachAttachedBikeWheels ||
                canDamageAttachedBikeWheels != other.canDamageAttachedBikeWheels)
                return false;

            return true;
        }

        public override bool Equals(object? obj) => Equals(obj as ITEM_VehicleData);
    }

    public class ITEM_ExternalLockData : IDeepCloneable<ITEM_ExternalLockData>, IEquatable<ITEM_ExternalLockData>
    {
        public int protectionCountStageOne { get; set; }
        public int protectionCountStageTwo { get; set; }
        public decimal protectionTime { get; set; }
        public decimal protectionResetTime { get; set; }

        public ITEM_ExternalLockData()
        {
            protectionCountStageOne = 3;
            protectionCountStageTwo = 1;
            protectionTime = 30.0m;
            protectionResetTime = 3600.0m;
        }

        public ITEM_ExternalLockData Clone()
        {
            return new ITEM_ExternalLockData
            {
                protectionCountStageOne = protectionCountStageOne,
                protectionCountStageTwo = protectionCountStageTwo,
                protectionTime = protectionTime,
                protectionResetTime = protectionResetTime
            };
        }
        public bool Equals(ITEM_ExternalLockData? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (protectionCountStageOne != other.protectionCountStageOne ||
                protectionCountStageTwo != other.protectionCountStageTwo ||
                protectionTime != other.protectionTime ||
                protectionResetTime != other.protectionResetTime)
                return false;

            return true;
        }
        public override bool Equals(object? obj) => Equals(obj as ITEM_ExternalLockData);
    }
    public class ITEM_SandstormData : IDeepCloneable<ITEM_SandstormData>, IEquatable<ITEM_SandstormData>
    {
        public decimal sandstormFrequency { get; set; }

        public ITEM_SandstormData()
        {
            sandstormFrequency = 1.0m;
        }
        public ITEM_SandstormData Clone()
        {
            return new ITEM_SandstormData
            {
                sandstormFrequency = sandstormFrequency
            };
        }
        public bool Equals(ITEM_SandstormData? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (sandstormFrequency != other.sandstormFrequency)
                return false;

            return true;
        }
        public override bool Equals(object? obj) => Equals(obj as ITEM_SandstormData);
    }
}
