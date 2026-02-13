using Day2eEditor;
using System.ComponentModel;

namespace ExpansionPlugin
{
    public class ExpansionSpawnConfig : ExpansionBaseIConfigLoader<ExpansionSpawnSettings>
    {
        public const int CurrentVersion = 7;

        public ExpansionSpawnConfig(string path) : base(path)
        {
        }
        public override void Load()
        {
            try
            {
                Data = AppServices.GetRequired<FileService>()
                    .LoadOrCreateJson(
                        _path,
                        createNew: () => CreateDefaultData(),
                        onError: ex =>
                        {
                            HandleLoadError(ex);
                        },
                        configName: FileName,
                        useVecConvertor: true
                    );
                var issues = ValidateData();
                if (issues?.Any() == true)
                {
                    Console.WriteLine("Validation issues in " + FileName + ":");
                    foreach (var msg in issues)
                        Console.WriteLine("- " + msg);

                    isDirty = true;
                }
                OnAfterLoad(Data);
                ClonedData = CloneData(Data);
            }
            catch (Exception ex)
            {
                HandleLoadError(ex);
            }

        }
        public override IEnumerable<string> Save()
        {
            if (Data is null)
                return Array.Empty<string>();

            if (!AreEqual(Data, ClonedData) || isDirty == true)
            {
                isDirty = false;
                AppServices.GetRequired<FileService>().SaveJson(_path, Data, false, true);
                ClonedData = CloneData(Data);
                return new[] { Path.GetFileName(_path) };
            }

            return Array.Empty<string>();
        }
        protected override ExpansionSpawnSettings CreateDefaultData()
        {
            return new ExpansionSpawnSettings(CurrentVersion);
        }
        protected override IEnumerable<string> ValidateData()
        {
            return Data.FixMissingOrInvalidFields();
        }
    }
    public class ExpansionSpawnSettings : IEquatable<ExpansionSpawnSettings>, IDeepCloneable<ExpansionSpawnSettings>
    {
        public int m_Version { get; set; }
        public BindingList<ExpansionSpawnLocation> SpawnLocations { get; set; }
        public ExpansionStartingClothing? StartingClothing { get; set; }
        public int? EnableSpawnSelection { get; set; }
        public int? SpawnOnTerritory { get; set; }
        public ExpansionStartingGear? StartingGear { get; set; }
        public int? UseLoadouts { get; set; }
        public BindingList<ExpansionSpawnGearLoadouts> MaleLoadouts { get; set; }
        public BindingList<ExpansionSpawnGearLoadouts> FemaleLoadouts { get; set; }
        public decimal? SpawnHealthValue { get; set; }
        public decimal? SpawnEnergyValue { get; set; }
        public decimal? SpawnWaterValue { get; set; }
        public int? EnableRespawnCooldowns { get; set; }
        public int? RespawnCooldown { get; set; }
        public int? TerritoryRespawnCooldown { get; set; }
        public int? PunishMultispawn { get; set; }
        public int? PunishCooldown { get; set; }
        public int? PunishTimeframe { get; set; }
        public int? CreateDeathMarker { get; set; }
        public string? BackgroundImagePath { get; set; }

        public ExpansionSpawnSettings() { }
        public ExpansionSpawnSettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
        }

        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (m_Version != ExpansionSpawnConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionSpawnConfig.CurrentVersion}");
                m_Version = ExpansionSpawnConfig.CurrentVersion;
            }
            if (SpawnLocations == null)
            {
                fixes.Add("SpawnLocations was missing — created empty list.");
                SpawnLocations = new BindingList<ExpansionSpawnLocation>();
            }
            if (StartingClothing == null)
            {
                fixes.Add("StartingClothing was null — using default.");
                StartingClothing = new ExpansionStartingClothing();
                StartingClothing.DefaultStartingClothing();
            }
            if (UseLoadouts is null or < 0 or > 1)
            {
                fixes.Add("UseLoadouts was null — set to 0.");
                UseLoadouts = 0;
            }
            if (MaleLoadouts == null)
            {
                fixes.Add("MaleLoadouts was missing — created empty list.");
                MaleLoadouts = new BindingList<ExpansionSpawnGearLoadouts>();
            }
            if (FemaleLoadouts == null)
            {
                fixes.Add("FemaleLoadouts was missing — created empty list.");
                FemaleLoadouts = new BindingList<ExpansionSpawnGearLoadouts>();
            }
            if (EnableSpawnSelection is null or < 0 or > 1)
            {
                fixes.Add("EnableSpawnSelection was null — set to 1.");
                EnableSpawnSelection = 1;
            }
            if (SpawnOnTerritory == null)
            {
                fixes.Add("SpawnOnTerritory was null — set to 0.");
                SpawnOnTerritory = 0;
            }

            if (EnableRespawnCooldowns is null or < 0 or > 1)
            {
                fixes.Add("EnableRespawnCooldowns was null — set to 0.");
                EnableRespawnCooldowns = 0;
            }
            if (RespawnCooldown == null)
            {
                fixes.Add("RespawnCooldown was null — set to 0.");
                RespawnCooldown = 0;
            }
            if (TerritoryRespawnCooldown == null)
            {
                fixes.Add("TerritoryRespawnCooldown was null — set to 0.");
                TerritoryRespawnCooldown = 0;
            }
            if (PunishMultispawn is null or < 0 or > 1)
            {
                fixes.Add("PunishMultispawn was null — set to 1.");
                PunishMultispawn = 1;
            }
            if (PunishCooldown == null)
            {
                fixes.Add("PunishCooldown was null — set to 0.");
                PunishCooldown = 0;
            }
            if (PunishTimeframe == null)
            {
                fixes.Add("PunishTimeframe was null — set to 0.");
                PunishTimeframe = 0;
            }
            if (CreateDeathMarker is null or < 0 or > 1)
            {
                fixes.Add("CreateDeathMarker was null — set to 1.");
                CreateDeathMarker = 1;
            }
            if (SpawnHealthValue == null)
            {
                fixes.Add("SpawnHealthValue was null — set to 100.");
                SpawnHealthValue = 100m;
            }
            if (SpawnEnergyValue == null)
            {
                fixes.Add("SpawnEnergyValue was null — set to 100.");
                SpawnEnergyValue = 100m;
            }
            if (SpawnWaterValue == null)
            {
                fixes.Add("SpawnWaterValue was null — set to 100.");
                SpawnWaterValue = 100m;
            }
            if (string.IsNullOrWhiteSpace(BackgroundImagePath))
            {
                fixes.Add("BackgroundImagePath was missing/empty — set to \"DayZExpansion/SpawnSelection/GUI/textures/wood_background.edds\".");
                BackgroundImagePath = "DayZExpansion/SpawnSelection/GUI/textures/wood_background.edds";
            }
            
            if (StartingGear == null)
            {
                StartingGear = new ExpansionStartingGear();
                StartingGear.DefaultStartingGear();
                fixes.Add("StartingGear was null — using default.");
                
            }
            return fixes;
        }
        
        public bool Equals(ExpansionSpawnSettings other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (m_Version != other.m_Version ||
                    EnableSpawnSelection != other.EnableSpawnSelection ||
                    SpawnOnTerritory != other.SpawnOnTerritory ||
                    UseLoadouts != other.UseLoadouts ||
                    EnableRespawnCooldowns != other.EnableRespawnCooldowns ||
                    RespawnCooldown != other.RespawnCooldown ||
                    TerritoryRespawnCooldown != other.TerritoryRespawnCooldown ||
                    PunishMultispawn != other.PunishMultispawn ||
                    PunishCooldown != other.PunishCooldown ||
                    PunishTimeframe != other.PunishTimeframe ||
                    CreateDeathMarker != other.CreateDeathMarker ||
                    SpawnHealthValue != other.SpawnHealthValue ||
                    SpawnEnergyValue != other.SpawnEnergyValue ||
                    SpawnWaterValue != other.SpawnWaterValue ||
                    BackgroundImagePath != other.BackgroundImagePath)
                return false;

            if (!Equals(StartingClothing, other.StartingClothing))
                return false;

            if (!Equals(StartingGear, other.StartingGear))
                return false;

            if (!ListEquals(SpawnLocations, other.SpawnLocations))
                return false;

            if (!ListEquals(MaleLoadouts, other.MaleLoadouts))
                return false;

            if (!ListEquals(FemaleLoadouts, other.FemaleLoadouts))
                return false;


            return true;
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionSpawnSettings);
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
        public ExpansionSpawnSettings Clone()
        {
            return new ExpansionSpawnSettings
            {
                m_Version = m_Version,
                SpawnLocations = new BindingList<ExpansionSpawnLocation>(SpawnLocations?.Select(x => x.Clone()).ToList() ?? new List<ExpansionSpawnLocation>()),
                StartingClothing = StartingClothing.Clone(),
                EnableSpawnSelection = EnableSpawnSelection,
                SpawnOnTerritory = SpawnOnTerritory,
                StartingGear = StartingGear.Clone(),
                UseLoadouts = UseLoadouts,
                MaleLoadouts = new BindingList<ExpansionSpawnGearLoadouts>(MaleLoadouts?.Select(x => x.Clone()).ToList() ?? new List<ExpansionSpawnGearLoadouts>()),
                FemaleLoadouts = new BindingList<ExpansionSpawnGearLoadouts>(FemaleLoadouts?.Select(x => x.Clone()).ToList() ?? new List<ExpansionSpawnGearLoadouts>()),
                SpawnHealthValue = SpawnHealthValue,
                SpawnEnergyValue = SpawnEnergyValue,
                SpawnWaterValue = SpawnWaterValue,
                EnableRespawnCooldowns = EnableRespawnCooldowns,
                RespawnCooldown = RespawnCooldown,
                TerritoryRespawnCooldown = TerritoryRespawnCooldown,
                PunishMultispawn = PunishMultispawn,
                PunishCooldown = PunishCooldown,
                PunishTimeframe = PunishTimeframe,
                CreateDeathMarker = CreateDeathMarker,
                BackgroundImagePath = BackgroundImagePath
            };
        }

    }
    public class ExpansionSpawnLocation
    {
        public string? Name { get; set; }
        public BindingList<Vec3> Positions { get; set; }
        public int? UseCooldown { get; set; }

        public ExpansionSpawnLocation()
        {
        }
        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(object obj)
        {
            if (obj is not ExpansionSpawnLocation other)
                return false;

            if( Name != other.Name ||
                UseCooldown != other.UseCooldown)
                return false;

            if (Positions == null && other.Positions == null)
                return true;

            if (Positions == null || other.Positions == null)
                return false;

            if (Positions.Count != other.Positions.Count)
                return false;

            for (int i = 0; i < Positions.Count; i++)
            {
                if (!Positions[i].Equals(other.Positions[i]))
                    return false;
            }

            return true;
        }
        public ExpansionSpawnLocation Clone()
        {
            return new ExpansionSpawnLocation()
            {
                Name = this.Name,
                UseCooldown = this.UseCooldown,
                Positions = new BindingList<Vec3>(this.Positions.Select(x => x.Clone()).ToList()),
            };
        }
    }
    public class ExpansionStartingClothing
    {
        public int? EnableCustomClothing { get; set; }
        public int? SetRandomHealth { get; set; }
        public BindingList<string> Headgear { get; set; }
        public BindingList<string> Glasses { get; set; }
        public BindingList<string> Masks { get; set; }
        public BindingList<string> Tops { get; set; }
        public BindingList<string> Vests { get; set; }
        public BindingList<string> Gloves { get; set; }
        public BindingList<string> Pants { get; set; }
        public BindingList<string> Belts { get; set; }
        public BindingList<string> Shoes { get; set; }
        public BindingList<string> Armbands { get; set; }
        public BindingList<string> Backpacks { get; set; }

        public ExpansionStartingClothing() { }
        public void DefaultStartingClothing()
        {
            Headgear = new BindingList<string>();
            Glasses = new BindingList<string>();
            Masks = new BindingList<string>();
            Tops = new BindingList<string>();
            Vests = new BindingList<string>();
            Gloves = new BindingList<string>();
            Pants = new BindingList<string>();
            Belts = new BindingList<string>();
            Shoes = new BindingList<string>();
            Armbands = new BindingList<string>();
            Backpacks = new BindingList<string>();

            Tops.Add("TShirt_Green");
            Tops.Add("TShirt_Blue");
            Tops.Add("TShirt_Black");
            Tops.Add("TShirt_Beige");
            Tops.Add("TShirt_Red");
            Tops.Add("TShirt_OrangeWhiteStripes");
            Tops.Add("TShirt_White");
            Tops.Add("TShirt_Red");
            Tops.Add("TShirt_Grey");
            Tops.Add("TShirt_RedBlackStripes");

            Pants.Add("CanvasPants_Beige");
            Pants.Add("CanvasPants_Blue");
            Pants.Add("CanvasPants_Grey");
            Pants.Add("CanvasPants_Red");
            Pants.Add("CanvasPants_Violet");
            Pants.Add("CanvasPantsMidi_Beige");
            Pants.Add("CanvasPantsMidi_Blue");
            Pants.Add("CanvasPantsMidi_Grey");
            Pants.Add("CanvasPantsMidi_Red");
            Pants.Add("CanvasPantsMidi_Violet");

            Shoes.Add("AthleticShoes_Blue");
            Shoes.Add("AthleticShoes_Grey");
            Shoes.Add("AthleticShoes_Brown");
            Shoes.Add("AthleticShoes_Green");
            Shoes.Add("AthleticShoes_Black");

            Backpacks.Add("TaloonBag_Blue");
            Backpacks.Add("TaloonBag_Green");
            Backpacks.Add("TaloonBag_Orange");
            Backpacks.Add("TaloonBag_Violet");
        }
        public override bool Equals(object obj)
        {
            if (obj is not ExpansionStartingClothing other)
                return false;

            return EnableCustomClothing == other.EnableCustomClothing &&
                SetRandomHealth == other.SetRandomHealth &&
                Headgear.SequenceEqual(other.Headgear) &&
                Glasses.SequenceEqual(other.Glasses) &&
                Masks.SequenceEqual(other.Masks) &&
                Tops.SequenceEqual(other.Tops) &&
                Vests.SequenceEqual(other.Vests) &&
                Gloves.SequenceEqual(other.Gloves) &&
                Pants.SequenceEqual(other.Pants) &&
                Belts.SequenceEqual(other.Belts) &&
                Shoes.SequenceEqual(other.Shoes) &&
                Armbands.SequenceEqual(other.Armbands) &&
                Backpacks.SequenceEqual(other.Backpacks);
        }
        public ExpansionStartingClothing Clone()
        {
            return new ExpansionStartingClothing()
            {
                EnableCustomClothing = this.EnableCustomClothing,
                SetRandomHealth = this.SetRandomHealth,
                Headgear = new BindingList<string>(this.Headgear.ToList()),
                Glasses = new BindingList<string>(this.Glasses.ToList()),
                Masks = new BindingList<string>(this.Masks.ToList()),
                Tops = new BindingList<string>(this.Tops.ToList()),
                Vests = new BindingList<string>(this.Vests.ToList()),
                Gloves = new BindingList<string>(this.Gloves.ToList()),
                Pants = new BindingList<string>(this.Pants.ToList()),
                Belts = new BindingList<string>(this.Belts.ToList()),
                Shoes = new BindingList<string>(this.Shoes.ToList()),
                Armbands = new BindingList<string>(this.Armbands.ToList()),
                Backpacks = new BindingList<string>(this.Backpacks.ToList())
            };
        }
    }
    public class ExpansionStartingGear
    {
        public int? EnableStartingGear { get; set; }
        public int? ApplyEnergySources { get; set; }
        public int? SetRandomHealth { get; set; }
        public BindingList<ExpansionStartingGearItem> UpperGear { get; set; }
        public BindingList<ExpansionStartingGearItem> PantsGear { get; set; }
        public BindingList<ExpansionStartingGearItem> BackpackGear { get; set; }
        public BindingList<ExpansionStartingGearItem> VestGear { get; set; }
        public ExpansionStartingGearItem? PrimaryWeapon { get; set; }
        public ExpansionStartingGearItem? SecondaryWeapon { get; set; }

        public ExpansionStartingGear() { }
        public override bool Equals(object obj)
        {
            if (obj is not ExpansionStartingGear other)
                return false;

            if (EnableStartingGear != other.EnableStartingGear &&
                ApplyEnergySources != other.ApplyEnergySources &&
                SetRandomHealth != other.SetRandomHealth &&
                PrimaryWeapon != other.PrimaryWeapon &&
                SecondaryWeapon != other.SecondaryWeapon)
                return false;
            
            if (UpperGear == null && other.UpperGear == null)
                return true;

            if (UpperGear == null || other.UpperGear == null)
                return false;

            if (UpperGear.Count != other.UpperGear.Count)
                return false;

            for (int i = 0; i < UpperGear.Count; i++)
            {
                if (!UpperGear[i].Equals(other.UpperGear[i]))
                    return false;
            }

            if (PantsGear == null && other.PantsGear == null)
                return true;

            if (PantsGear == null || other.PantsGear == null)
                return false;

            if (PantsGear.Count != other.PantsGear.Count)
                return false;

            for (int i = 0; i < PantsGear.Count; i++)
            {
                if (!PantsGear[i].Equals(other.PantsGear[i]))
                    return false;
            }

            if (BackpackGear == null && other.BackpackGear == null)
                return true;

            if (BackpackGear == null || other.BackpackGear == null)
                return false;

            if (BackpackGear.Count != other.BackpackGear.Count)
                return false;

            for (int i = 0; i < BackpackGear.Count; i++)
            {
                if (!BackpackGear[i].Equals(other.BackpackGear[i]))
                    return false;
            }

            if (VestGear == null && other.VestGear == null)
                return true;

            if (VestGear == null || other.VestGear == null)
                return false;

            if (VestGear.Count != other.VestGear.Count)
                return false;

            for (int i = 0; i < VestGear.Count; i++)
            {
                if (!VestGear[i].Equals(other.VestGear[i]))
                    return false;
            }

            return true;
        }
        public ExpansionStartingGear Clone()
        {
            return new ExpansionStartingGear()
            {
                EnableStartingGear = this.EnableStartingGear,
                ApplyEnergySources = this.ApplyEnergySources,
                SetRandomHealth = this.SetRandomHealth,
                UpperGear = new BindingList<ExpansionStartingGearItem>(this.UpperGear.Select(x => x.Clone()).ToList()),
                PantsGear = new BindingList<ExpansionStartingGearItem>(this.PantsGear.Select(x => x.Clone()).ToList()),
                BackpackGear = new BindingList<ExpansionStartingGearItem>(this.BackpackGear.Select(x => x.Clone()).ToList()),
                VestGear = new BindingList<ExpansionStartingGearItem>(this.VestGear.Select(x => x.Clone()).ToList()),
                PrimaryWeapon = this.PrimaryWeapon?.Clone(),
                SecondaryWeapon = this.SecondaryWeapon?.Clone()
            };
        }
        public void DefaultStartingGear()
        {
            UpperGear = new BindingList<ExpansionStartingGearItem>();
            PantsGear = new BindingList<ExpansionStartingGearItem>();
            BackpackGear = new BindingList<ExpansionStartingGearItem>();
            VestGear = new BindingList<ExpansionStartingGearItem>();
            PrimaryWeapon = null;
            SecondaryWeapon = null;
            UpperGear.Add(new ExpansionStartingGearItem("Rag", 4));
            UpperGear.Add(new ExpansionStartingGearItem("Apple"));
        }
    }
    public class ExpansionStartingGearItem
    {
        public string? ClassName { get; set; }
        public int? Quantity { get; set; }
        public BindingList<string> Attachments { get; set; }

        public ExpansionStartingGearItem() { }
        public ExpansionStartingGearItem(string className, int quantity = -1, List<string> attachments = null)
        {
            ClassName = className;
            Quantity = quantity;

            if (attachments == null)
            {
                Attachments = new BindingList<string>();
            }
            else
            {
                Attachments = new BindingList<string>(attachments); ;
            }
        }
        public override bool Equals(object obj)
        {
            if (obj is not ExpansionStartingGearItem other)
                return false;

            if (ClassName != other.ClassName ||
                Quantity != other.Quantity)
                return false;
            if (!Attachments.SequenceEqual(other.Attachments))
                return false;

            return true;
        }
        public ExpansionStartingGearItem Clone()
        {
            return new ExpansionStartingGearItem()
            {
                ClassName = this.ClassName,
                Quantity = this.Quantity,
                Attachments = this.Attachments != null
                    ? new BindingList<string>(this.Attachments.ToList())
                    : null
            };
        }
    }
    public class ExpansionSpawnGearLoadouts
    {
        public string? Loadout { get; set; }
        public decimal? Chance { get; set; }

        public ExpansionSpawnGearLoadouts() { }
        public ExpansionSpawnGearLoadouts(string loadout, decimal chance)
        {
            Loadout = loadout;
            Chance = chance;
        }
        public override string ToString()
        {
            return Loadout;
        }
        public override bool Equals(object obj)
        {
            if (obj is not ExpansionSpawnGearLoadouts other)
                return false;

            return Loadout == other.Loadout &&
                Chance == other.Chance;
        }
        public ExpansionSpawnGearLoadouts Clone()
        {
            return new ExpansionSpawnGearLoadouts()
            {
                Loadout = this.Loadout,
                Chance = this.Chance
            };
        }
    }
}
