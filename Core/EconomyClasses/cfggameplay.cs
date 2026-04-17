using System.ComponentModel;
using System.IO;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace Day2eEditor
{
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

                SpawnGearPresetconfigs = new SpawnGearPresetsConfig(BaseDirectory);
                SpawnGearPresetconfigs.Load(BaseDirectory, Data.PlayerData.spawnGearPresetFiles);

                PlayerRestrictedFilesConfig = new PlayerRestrictedFilesConfig(BaseDirectory);
                PlayerRestrictedFilesConfig.Load(BaseDirectory, Data.WorldsData.playerRestrictedAreaFiles);

                ObjectSpawnerArrConfig = new ObjectSpawnerArrConfig(BaseDirectory);
                ObjectSpawnerArrConfig.Load(BaseDirectory, Data.WorldsData.objectSpawnersArr);

                var issues = ValidateData();
                if (issues?.Any() == true)
                {
                    Console.WriteLine("Validation issues in " + FileName + ":");
                    foreach (var msg in issues)
                        Console.WriteLine("- " + msg);

                    MarkDirty();
                }

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
                fileNames.Add(Path.GetFileName(_path));
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
        public Generaldata GeneralData { get; set; }
        public Playerdata PlayerData { get; set; }
        public Worldsdata WorldsData { get; set; }
        public Basebuildingdata BaseBuildingData { get; set; }
        public Uidata UIData { get; set; }
        public CFGGameplayMapData MapData { get; set; }
        public VehicleData VehicleData { get; set; }

        [JsonIgnore]
        private const int currentversion = 123;

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

        public cfggameplay Clone()
        {
            return new cfggameplay
            {
                version = version,
                GeneralData = GeneralData?.Clone() ?? new Generaldata(),
                PlayerData = PlayerData?.Clone() ?? new Playerdata(),
                WorldsData = WorldsData?.Clone() ?? new Worldsdata(),
                BaseBuildingData = BaseBuildingData?.Clone() ?? new Basebuildingdata(),
                UIData = UIData?.Clone() ?? new Uidata(),
                MapData = MapData?.Clone() ?? new CFGGameplayMapData(),
                VehicleData = VehicleData?.Clone() ?? new VehicleData()
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
                   Equals(VehicleData, other.VehicleData);
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



            return fixes;
        }
    }

    public class Generaldata : IDeepCloneable<Generaldata>, IEquatable<Generaldata>
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

        public Generaldata Clone()
        {
            return new Generaldata
            {
                disableBaseDamage = disableBaseDamage,
                disableContainerDamage = disableContainerDamage,
                disableRespawnDialog = disableRespawnDialog,
                disableRespawnInUnconsciousness = disableRespawnInUnconsciousness
            };
        }

        public bool Equals(Generaldata? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return disableBaseDamage == other.disableBaseDamage &&
                   disableContainerDamage == other.disableContainerDamage &&
                   disableRespawnDialog == other.disableRespawnDialog &&
                   disableRespawnInUnconsciousness == other.disableRespawnInUnconsciousness;
        }

        public override bool Equals(object? obj) => Equals(obj as Generaldata);
    }

    public class Playerdata : IDeepCloneable<Playerdata>, IEquatable<Playerdata>
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

        public Playerdata Clone()
        {
            return new Playerdata
            {
                disablePersonalLight = disablePersonalLight,
                spawnGearPresetFiles = new BindingList<string>(spawnGearPresetFiles?.ToList() ?? new List<string>()),
                StaminaData = StaminaData?.Clone() ?? new Staminadata(),
                ShockHandlingData = ShockHandlingData?.Clone() ?? new Shockhandlingdata(),
                MovementData = MovementData?.Clone() ?? new MovementData(),
                DrowningData = DrowningData?.Clone() ?? new DrowningData(),
                WeaponObstructionData = WeaponObstructionData?.Clone() ?? new WeaponObstructionData()
            };
        }

        public bool Equals(Playerdata? other)
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

        public override bool Equals(object? obj) => Equals(obj as Playerdata);
    }

    public class Staminadata : IDeepCloneable<Staminadata>, IEquatable<Staminadata>
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

        public Staminadata Clone()
        {
            return new Staminadata
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

        public bool Equals(Staminadata? other)
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

        public override bool Equals(object? obj) => Equals(obj as Staminadata);
    }

    public class Shockhandlingdata : IDeepCloneable<Shockhandlingdata>, IEquatable<Shockhandlingdata>
    {
        public decimal shockRefillSpeedConscious { get; set; }
        public decimal shockRefillSpeedUnconscious { get; set; }
        public bool allowRefillSpeedModifier { get; set; }

        public Shockhandlingdata()
        {
            shockRefillSpeedConscious = 5.0m;
            shockRefillSpeedUnconscious = 1.0m;
            allowRefillSpeedModifier = true;
        }

        public Shockhandlingdata Clone()
        {
            return new Shockhandlingdata
            {
                shockRefillSpeedConscious = shockRefillSpeedConscious,
                shockRefillSpeedUnconscious = shockRefillSpeedUnconscious,
                allowRefillSpeedModifier = allowRefillSpeedModifier
            };
        }

        public bool Equals(Shockhandlingdata? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return shockRefillSpeedConscious == other.shockRefillSpeedConscious &&
                   shockRefillSpeedUnconscious == other.shockRefillSpeedUnconscious &&
                   allowRefillSpeedModifier == other.allowRefillSpeedModifier;
        }

        public override bool Equals(object? obj) => Equals(obj as Shockhandlingdata);
    }

    public class MovementData : IDeepCloneable<MovementData>, IEquatable<MovementData>
    {
        public decimal timeToStrafeJog { get; set; }
        public decimal rotationSpeedJog { get; set; }
        public decimal timeToSprint { get; set; }
        public decimal timeToStrafeSprint { get; set; }
        public decimal rotationSpeedSprint { get; set; }
        public bool allowStaminaAffectInertia { get; set; }

        public MovementData()
        {
            timeToStrafeJog = 0.1m;
            rotationSpeedJog = 0.3m;
            timeToSprint = 0.45m;
            timeToStrafeSprint = 0.3m;
            rotationSpeedSprint = 0.15m;
            allowStaminaAffectInertia = true;
        }

        public MovementData Clone()
        {
            return new MovementData
            {
                timeToStrafeJog = timeToStrafeJog,
                rotationSpeedJog = rotationSpeedJog,
                timeToSprint = timeToSprint,
                timeToStrafeSprint = timeToStrafeSprint,
                rotationSpeedSprint = rotationSpeedSprint,
                allowStaminaAffectInertia = allowStaminaAffectInertia
            };
        }

        public bool Equals(MovementData? other)
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

        public override bool Equals(object? obj) => Equals(obj as MovementData);
    }

    public class DrowningData : IDeepCloneable<DrowningData>, IEquatable<DrowningData>
    {
        public decimal staminaDepletionSpeed { get; set; }
        public decimal healthDepletionSpeed { get; set; }
        public decimal shockDepletionSpeed { get; set; }

        public DrowningData()
        {
            staminaDepletionSpeed = 10.0m;
            healthDepletionSpeed = 10.0m;
            shockDepletionSpeed = 10.0m;
        }

        public DrowningData Clone()
        {
            return new DrowningData
            {
                staminaDepletionSpeed = staminaDepletionSpeed,
                healthDepletionSpeed = healthDepletionSpeed,
                shockDepletionSpeed = shockDepletionSpeed
            };
        }

        public bool Equals(DrowningData? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return staminaDepletionSpeed == other.staminaDepletionSpeed &&
                   healthDepletionSpeed == other.healthDepletionSpeed &&
                   shockDepletionSpeed == other.shockDepletionSpeed;
        }

        public override bool Equals(object? obj) => Equals(obj as DrowningData);
    }

    public class WeaponObstructionData : IDeepCloneable<WeaponObstructionData>, IEquatable<WeaponObstructionData>
    {
        public int staticMode { get; set; }
        public int dynamicMode { get; set; }

        public WeaponObstructionData()
        {
            staticMode = 1;
            dynamicMode = 1;
        }

        public WeaponObstructionData Clone()
        {
            return new WeaponObstructionData
            {
                staticMode = staticMode,
                dynamicMode = dynamicMode
            };
        }

        public bool Equals(WeaponObstructionData? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return staticMode == other.staticMode &&
                   dynamicMode == other.dynamicMode;
        }

        public override bool Equals(object? obj) => Equals(obj as WeaponObstructionData);
    }

    public class Worldsdata : IDeepCloneable<Worldsdata>, IEquatable<Worldsdata>
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
            decimal[] mintemp = { -3, -2, 0, 4, 9, 14, 18, 17, 12, 7, 4, 0 };
            decimal[] maxtemp = { 3, 5, 7, 14, 19, 24, 26, 25, 21, 16, 10, 5 };
            decimal[] wetness = { 1.0m, 1.0m, 1.33m, 1.66m, 2.0m };
            environmentMaxTemps = new BindingList<decimal>(maxtemp.ToList());
            environmentMinTemps = new BindingList<decimal>(mintemp.ToList());
            wetnessWeightModifiers = new BindingList<decimal>(wetness.ToList());
            playerRestrictedAreaFiles = new BindingList<string>();
        }

        public Worldsdata Clone()
        {
            return new Worldsdata
            {
                lightingConfig = lightingConfig,
                objectSpawnersArr = new BindingList<string>(objectSpawnersArr?.ToList() ?? new List<string>()),
                environmentMinTemps = new BindingList<decimal>(environmentMinTemps?.ToList() ?? new List<decimal>()),
                environmentMaxTemps = new BindingList<decimal>(environmentMaxTemps?.ToList() ?? new List<decimal>()),
                wetnessWeightModifiers = new BindingList<decimal>(wetnessWeightModifiers?.ToList() ?? new List<decimal>()),
                playerRestrictedAreaFiles = new BindingList<string>(playerRestrictedAreaFiles?.ToList() ?? new List<string>())
            };
        }

        public bool Equals(Worldsdata? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return lightingConfig == other.lightingConfig &&
                   objectSpawnersArr.SequenceEqual(other.objectSpawnersArr) &&
                   environmentMinTemps.SequenceEqual(other.environmentMinTemps) &&
                   environmentMaxTemps.SequenceEqual(other.environmentMaxTemps) &&
                   wetnessWeightModifiers.SequenceEqual(other.wetnessWeightModifiers) &&
                   playerRestrictedAreaFiles.SequenceEqual(other.playerRestrictedAreaFiles);
        }

        public override bool Equals(object? obj) => Equals(obj as Worldsdata);
    }

    public class Basebuildingdata : IDeepCloneable<Basebuildingdata>, IEquatable<Basebuildingdata>
    {
        public Hologramdata HologramData { get; set; }
        public Constructiondata ConstructionData { get; set; }

        public Basebuildingdata()
        {
            HologramData = new Hologramdata();
            ConstructionData = new Constructiondata();
        }

        public Basebuildingdata Clone()
        {
            return new Basebuildingdata
            {
                HologramData = HologramData?.Clone() ?? new Hologramdata(),
                ConstructionData = ConstructionData?.Clone() ?? new Constructiondata()
            };
        }

        public bool Equals(Basebuildingdata? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(HologramData, other.HologramData) &&
                   Equals(ConstructionData, other.ConstructionData);
        }

        public override bool Equals(object? obj) => Equals(obj as Basebuildingdata);
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
                disableDistanceCheck = disableDistanceCheck
            };
        }

        public bool Equals(Constructiondata? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return disablePerformRoofCheck == other.disablePerformRoofCheck &&
                   disableIsCollidingCheck == other.disableIsCollidingCheck &&
                   disableDistanceCheck == other.disableDistanceCheck;
        }

        public override bool Equals(object? obj) => Equals(obj as Constructiondata);
    }

    public class Uidata : IDeepCloneable<Uidata>, IEquatable<Uidata>
    {
        public bool use3DMap { get; set; }
        public Hitindicationdata HitIndicationData { get; set; }

        public Uidata()
        {
            use3DMap = false;
            HitIndicationData = new Hitindicationdata();
        }

        public Uidata Clone()
        {
            return new Uidata
            {
                use3DMap = use3DMap,
                HitIndicationData = HitIndicationData?.Clone() ?? new Hitindicationdata()
            };
        }

        public bool Equals(Uidata? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return use3DMap == other.use3DMap &&
                   Equals(HitIndicationData, other.HitIndicationData);
        }

        public override bool Equals(object? obj) => Equals(obj as Uidata);
    }

    public class Hitindicationdata : IDeepCloneable<Hitindicationdata>, IEquatable<Hitindicationdata>
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
            hitDirectionMaxDuration = 2.0m;
            hitDirectionBreakPointRelative = 0.2m;
            hitDirectionScatter = 10.0m;
            hitIndicationPostProcessEnabled = true;
        }

        public Hitindicationdata Clone()
        {
            return new Hitindicationdata
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

        public bool Equals(Hitindicationdata? other)
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

        public override bool Equals(object? obj) => Equals(obj as Hitindicationdata);
    }

    public class CFGGameplayMapData : IDeepCloneable<CFGGameplayMapData>, IEquatable<CFGGameplayMapData>
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

        public CFGGameplayMapData Clone()
        {
            return new CFGGameplayMapData
            {
                ignoreMapOwnership = ignoreMapOwnership,
                ignoreNavItemsOwnership = ignoreNavItemsOwnership,
                displayPlayerPosition = displayPlayerPosition,
                displayNavInfo = displayNavInfo
            };
        }

        public bool Equals(CFGGameplayMapData? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return ignoreMapOwnership == other.ignoreMapOwnership &&
                   ignoreNavItemsOwnership == other.ignoreNavItemsOwnership &&
                   displayPlayerPosition == other.displayPlayerPosition &&
                   displayNavInfo == other.displayNavInfo;
        }

        public override bool Equals(object? obj) => Equals(obj as CFGGameplayMapData);
    }

    public class VehicleData : IDeepCloneable<VehicleData>, IEquatable<VehicleData>
    {
        public decimal boatDecayMultiplier { get; set; }

        public VehicleData()
        {
            boatDecayMultiplier = 1;
        }

        public VehicleData Clone()
        {
            return new VehicleData
            {
                boatDecayMultiplier = boatDecayMultiplier
            };
        }

        public bool Equals(VehicleData? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return boatDecayMultiplier == other.boatDecayMultiplier;
        }

        public override bool Equals(object? obj) => Equals(obj as VehicleData);
    }
}
