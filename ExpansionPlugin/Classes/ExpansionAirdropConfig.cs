using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpansionPlugin
{
    public class ExpansionAirdropConfig : ExpansionBaseIConfigLoader<ExpansionAirdropSettings>
    {
        public const int CurrentVersion = 8;
        public ExpansionAirdropConfig(string path) : base(path)
        {
        }
        protected override ExpansionAirdropSettings CreateDefaultData()
        {
            return new ExpansionAirdropSettings(CurrentVersion);
        }

        protected override IEnumerable<string> ValidateData()
        {
            return Data.FixMissingOrInvalidFields();
        }
    }
    public class ExpansionAirdropSettings : IEquatable<ExpansionAirdropSettings>, IDeepCloneable<ExpansionAirdropSettings>
    {
        public int m_Version { get; set; }
        public int? ServerMarkerOnDropLocation { get; set; }
        public int? Server3DMarkerOnDropLocation { get; set; }
        public int? ShowAirdropTypeOnMarker { get; set; }
        public int? HideCargoWhileParachuteIsDeployed { get; set; }
        public int? HeightIsRelativeToGroundLevel { get; set; }
        public decimal? Height { get; set; }
        public decimal? DropZoneHeight { get; set; }
        public decimal? FollowTerrainFraction { get; set; }
        public decimal? Speed { get; set; }
        public decimal? DropZoneSpeed { get; set; }
        public decimal? Radius { get; set; }
        public decimal? InfectedSpawnRadius { get; set; }
        public int? InfectedSpawnInterval { get; set; }
        public int? ItemCount { get; set; }
        public string? AirdropPlaneClassName { get; set; }
        public decimal? DropZoneProximityDistance { get; set; }
        public int? ExplodeAirVehiclesOnCollision { get; set; }
        public BindingList<ExpansionLootContainer> Containers { get; set; }

        public ExpansionAirdropSettings() 
        { 
        }
        public ExpansionAirdropSettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
            ServerMarkerOnDropLocation = 1;
            Server3DMarkerOnDropLocation = 1;
            ShowAirdropTypeOnMarker = 1;
            HideCargoWhileParachuteIsDeployed = 1;
            HeightIsRelativeToGroundLevel = 1;
            Height = 450;
            FollowTerrainFraction = (decimal)0.5;
            Speed = 35;
            Radius = 1;
            InfectedSpawnRadius = 50;
            InfectedSpawnInterval = 250;
            ItemCount = 50;
            AirdropPlaneClassName = "";
            DropZoneProximityDistance = (decimal)1500.0;
            ExplodeAirVehiclesOnCollision = 0;
            Containers = new BindingList<ExpansionLootContainer>();
            DefaultRegular();
            DefaultMedical();
            DefaultBaseBuilding();
            DefaultMilitary();
        }
        public bool Equals(ExpansionAirdropSettings other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return m_Version == other.m_Version &&
                   ServerMarkerOnDropLocation == other.ServerMarkerOnDropLocation &&
                   Server3DMarkerOnDropLocation == other.Server3DMarkerOnDropLocation &&
                   ShowAirdropTypeOnMarker == other.ShowAirdropTypeOnMarker &&
                   HideCargoWhileParachuteIsDeployed == other.HideCargoWhileParachuteIsDeployed &&
                   HeightIsRelativeToGroundLevel == other.HeightIsRelativeToGroundLevel &&
                   Height == other.Height &&
                   DropZoneHeight == other.DropZoneHeight &&
                   FollowTerrainFraction == other.FollowTerrainFraction &&
                   Speed == other.Speed &&
                   DropZoneSpeed == other.DropZoneSpeed &&
                   Radius == other.Radius &&
                   InfectedSpawnRadius == other.InfectedSpawnRadius &&
                   InfectedSpawnInterval == other.InfectedSpawnInterval &&
                   ItemCount == other.ItemCount &&
                   AirdropPlaneClassName == other.AirdropPlaneClassName &&
                   DropZoneProximityDistance == other.DropZoneProximityDistance &&
                   ExplodeAirVehiclesOnCollision == other.ExplodeAirVehiclesOnCollision &&
                   Containers.SequenceEqual(other.Containers);
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionCoreSettings);
        void DefaultRegular()
        {
            BindingList<ExpansionLoot> Loot = ExpansionLootDefaults.Airdrop_Regular();
            BindingList<string> Infected = new BindingList<string>() {
            "ZmbM_HermitSkinny_Beige",
            "ZmbM_HermitSkinny_Black",
            "ZmbM_HermitSkinny_Green",
            "ZmbM_HermitSkinny_Red",
            "ZmbM_FarmerFat_Beige",
            "ZmbM_FarmerFat_Blue",
            "ZmbM_FarmerFat_Brown",
            "ZmbM_FarmerFat_Green",
            "ZmbF_CitizenANormal_Beige",
            "ZmbF_CitizenANormal_Blue",
            "ZmbF_CitizenANormal_Brown",
            "ZmbM_CitizenBFat_Blue",
            "ZmbM_CitizenBFat_Red",
            "ZmbM_CitizenBFat_Green",
            "ZmbF_CitizenBSkinny",
            "ZmbM_FishermanOld_Blue",
            "ZmbM_FishermanOld_Green",
            "ZmbM_FishermanOld_Grey",
            "ZmbM_FishermanOld_Red",
            "ZmbM_JournalistSkinny",
            "ZmbF_JournalistNormal_Blue",
            "ZmbF_JournalistNormal_Green",
            "ZmbF_JournalistNormal_Red",
            "ZmbF_JournalistNormal_White",
            "ZmbM_HikerSkinny_Blue",
            "ZmbM_HikerSkinny_Green",
            "ZmbM_HikerSkinny_Yellow",
            "ZmbF_HikerSkinny_Blue",
            "ZmbF_HikerSkinny_Grey",
            "ZmbF_HikerSkinny_Green",
            "ZmbF_HikerSkinny_Red",
            "ZmbF_SurvivorNormal_Blue",
            "ZmbF_SurvivorNormal_Orange",
            "ZmbF_SurvivorNormal_Red",
            "ZmbF_SurvivorNormal_White",
            "ZmbM_CommercialPilotOld_Blue",
            "ZmbM_CommercialPilotOld_Olive",
            "ZmbM_CommercialPilotOld_Brown",
            "ZmbM_CommercialPilotOld_Grey",
            "ZmbM_JoggerSkinny_Blue",
            "ZmbM_JoggerSkinny_Green",
            "ZmbM_JoggerSkinny_Red",
            "ZmbF_JoggerSkinny_Blue",
            "ZmbF_JoggerSkinny_Brown",
            "ZmbF_JoggerSkinny_Green",
            "ZmbF_JoggerSkinny_Red",
            "ZmbM_MotobikerFat_Beige",
            "ZmbM_MotobikerFat_Black",
            "ZmbM_MotobikerFat_Blue",
            "ZmbM_VillagerOld_Blue",
            "ZmbM_VillagerOld_Green",
            "ZmbM_VillagerOld_White",
            "ZmbM_SkaterYoung_Blue",
            "ZmbM_SkaterYoung_Brown",
            "ZmbM_SkaterYoung_Green",
            "ZmbM_SkaterYoung_Grey",
            "ZmbF_SkaterYoung_Brown",
            "ZmbF_SkaterYoung_Striped",
            "ZmbF_SkaterYoung_Violet",
            "ZmbM_OffshoreWorker_Green",
            "ZmbM_OffshoreWorker_Orange",
            "ZmbM_OffshoreWorker_Red",
            "ZmbM_OffshoreWorker_Yellow",
            "ZmbM_Jacket_beige",
            "ZmbM_Jacket_black",
            "ZmbM_Jacket_blue",
            "ZmbM_Jacket_bluechecks",
            "ZmbM_Jacket_brown",
            "ZmbM_Jacket_greenchecks",
            "ZmbM_Jacket_grey",
            "ZmbM_Jacket_khaki",
            "ZmbM_Jacket_magenta",
            "ZmbM_Jacket_stripes",
            "ZmbF_ShortSkirt_beige",
            "ZmbF_ShortSkirt_black",
            "ZmbF_ShortSkirt_brown",
            "ZmbF_ShortSkirt_green",
            "ZmbF_ShortSkirt_grey",
            "ZmbF_ShortSkirt_checks",
            "ZmbF_ShortSkirt_red",
            "ZmbF_ShortSkirt_stripes",
            "ZmbF_ShortSkirt_white",
            "ZmbF_ShortSkirt_yellow",
            "ZmbF_VillagerOld_Blue",
            "ZmbF_VillagerOld_Green",
            "ZmbF_VillagerOld_Red",
            "ZmbF_VillagerOld_White",
            "ZmbF_MilkMaidOld_Beige",
            "ZmbF_MilkMaidOld_Black",
            "ZmbF_MilkMaidOld_Green",
            "ZmbF_MilkMaidOld_Grey",
        };

            int itemCount = 30;
            int infectedCount = 25;

            if (Containers != null)
                Containers.Add(new ExpansionLootContainer("ExpansionAirdropContainer", 0, 5, Loot, Infected, itemCount, infectedCount));
        }
        void DefaultMedical()
        {
            BindingList<ExpansionLoot> Loot = ExpansionLootDefaults.Airdrop_Medical();
            BindingList<string> Infected = new BindingList<string>(){
            "ZmbM_HermitSkinny_Beige",
            "ZmbM_HermitSkinny_Black",
            "ZmbM_HermitSkinny_Green",
            "ZmbM_HermitSkinny_Red",
            "ZmbM_FarmerFat_Beige",
            "ZmbM_FarmerFat_Blue",
            "ZmbM_FarmerFat_Brown",
            "ZmbM_FarmerFat_Green",
            "ZmbF_CitizenANormal_Beige",
            "ZmbF_CitizenANormal_Brown",
            "ZmbF_CitizenANormal_Blue",
            "ZmbM_CitizenBFat_Blue",
            "ZmbM_CitizenBFat_Red",
            "ZmbM_CitizenBFat_Green",
            "ZmbF_CitizenBSkinny",
            "ZmbM_FishermanOld_Blue",
            "ZmbM_FishermanOld_Green",
            "ZmbM_FishermanOld_Grey",
            "ZmbM_FishermanOld_Red",
            "ZmbM_JournalistSkinny",
            "ZmbF_JournalistNormal_Blue",
            "ZmbF_JournalistNormal_Green",
            "ZmbF_JournalistNormal_Red",
            "ZmbF_JournalistNormal_White",
            "ZmbM_HikerSkinny_Blue",
            "ZmbM_HikerSkinny_Green",
            "ZmbM_HikerSkinny_Yellow",
            "ZmbF_HikerSkinny_Blue",
            "ZmbF_HikerSkinny_Grey",
            "ZmbF_HikerSkinny_Green",
            "ZmbF_HikerSkinny_Red",
            "ZmbF_SurvivorNormal_Blue",
            "ZmbF_SurvivorNormal_Orange",
            "ZmbF_SurvivorNormal_Red",
            "ZmbF_SurvivorNormal_White",
            "ZmbM_CommercialPilotOld_Blue",
            "ZmbM_CommercialPilotOld_Olive",
            "ZmbM_CommercialPilotOld_Brown",
            "ZmbM_CommercialPilotOld_Grey",
            "ZmbM_JoggerSkinny_Blue",
            "ZmbM_JoggerSkinny_Green",
            "ZmbM_JoggerSkinny_Red",
            "ZmbF_JoggerSkinny_Blue",
            "ZmbF_JoggerSkinny_Brown",
            "ZmbF_JoggerSkinny_Green",
            "ZmbF_JoggerSkinny_Red",
            "ZmbM_MotobikerFat_Beige",
            "ZmbM_MotobikerFat_Black",
            "ZmbM_MotobikerFat_Blue",
            "ZmbM_VillagerOld_Blue",
            "ZmbM_VillagerOld_Green",
            "ZmbM_VillagerOld_White",
            "ZmbM_SkaterYoung_Blue",
            "ZmbM_SkaterYoung_Brown",
            "ZmbM_SkaterYoung_Green",
            "ZmbM_SkaterYoung_Grey",
            "ZmbF_SkaterYoung_Brown",
            "ZmbF_SkaterYoung_Striped",
            "ZmbF_SkaterYoung_Violet",
            "ZmbM_OffshoreWorker_Green",
            "ZmbM_OffshoreWorker_Orange",
            "ZmbM_OffshoreWorker_Red",
            "ZmbM_OffshoreWorker_Yellow",
            "ZmbM_Jacket_beige",
            "ZmbM_Jacket_black",
            "ZmbM_Jacket_blue",
            "ZmbM_Jacket_bluechecks",
            "ZmbM_Jacket_brown",
            "ZmbM_Jacket_greenchecks",
            "ZmbM_Jacket_grey",
            "ZmbM_Jacket_khaki",
            "ZmbM_Jacket_magenta",
            "ZmbM_Jacket_stripes",
            "ZmbF_ShortSkirt_beige",
            "ZmbF_ShortSkirt_black",
            "ZmbF_ShortSkirt_brown",
            "ZmbF_ShortSkirt_green",
            "ZmbF_ShortSkirt_grey",
            "ZmbF_ShortSkirt_checks",
            "ZmbF_ShortSkirt_red",
            "ZmbF_ShortSkirt_stripes",
            "ZmbF_ShortSkirt_white",
            "ZmbF_ShortSkirt_yellow",
            "ZmbF_VillagerOld_Blue",
            "ZmbF_VillagerOld_Green",
            "ZmbF_VillagerOld_Red",
            "ZmbF_VillagerOld_White",
            "ZmbF_MilkMaidOld_Beige",
            "ZmbF_MilkMaidOld_Black",
            "ZmbF_MilkMaidOld_Green",
            "ZmbF_MilkMaidOld_Grey",
            "ZmbF_DoctorSkinny",
            "ZmbF_NurseFat",
            "ZmbM_DoctorFat",
            "ZmbF_PatientOld",
            "ZmbM_PatientSkinny",
        };

            int itemCount = 25;
            int infectedCount = 15;

            if (Containers != null)
                Containers.Add(new ExpansionLootContainer("ExpansionAirdropContainer_Medical", 0, 10, Loot, Infected, itemCount, infectedCount));
        }
        void DefaultBaseBuilding()
        {
            BindingList<ExpansionLoot> Loot = ExpansionLootDefaults.Airdrop_BaseBuilding();
            BindingList<string> Infected = new BindingList<string>(){
            "ZmbM_HermitSkinny_Beige",
            "ZmbM_HermitSkinny_Black",
            "ZmbM_HermitSkinny_Green",
            "ZmbM_HermitSkinny_Red",
            "ZmbM_FarmerFat_Beige",
            "ZmbM_FarmerFat_Blue",
            "ZmbM_FarmerFat_Brown",
            "ZmbM_FarmerFat_Green",
            "ZmbF_CitizenANormal_Beige",
            "ZmbF_CitizenANormal_Brown",
            "ZmbF_CitizenANormal_Blue",
            "ZmbM_CitizenBFat_Blue",
            "ZmbM_CitizenBFat_Red",
            "ZmbM_CitizenBFat_Green",
            "ZmbF_CitizenBSkinny",
            "ZmbM_FishermanOld_Blue",
            "ZmbM_FishermanOld_Green",
            "ZmbM_FishermanOld_Grey",
            "ZmbM_FishermanOld_Red",
            "ZmbM_JournalistSkinny",
            "ZmbF_JournalistNormal_Blue",
            "ZmbF_JournalistNormal_Green",
            "ZmbF_JournalistNormal_Red",
            "ZmbF_JournalistNormal_White",
            "ZmbM_HikerSkinny_Blue",
            "ZmbM_HikerSkinny_Green",
            "ZmbM_HikerSkinny_Yellow",
            "ZmbF_HikerSkinny_Blue",
            "ZmbF_HikerSkinny_Grey",
            "ZmbF_HikerSkinny_Green",
            "ZmbF_HikerSkinny_Red",
            "ZmbF_SurvivorNormal_Blue",
            "ZmbF_SurvivorNormal_Orange",
            "ZmbF_SurvivorNormal_Red",
            "ZmbF_SurvivorNormal_White",
            "ZmbM_CommercialPilotOld_Blue",
            "ZmbM_CommercialPilotOld_Olive",
            "ZmbM_CommercialPilotOld_Brown",
            "ZmbM_CommercialPilotOld_Grey",
            "ZmbM_JoggerSkinny_Blue",
            "ZmbM_JoggerSkinny_Green",
            "ZmbM_JoggerSkinny_Red",
            "ZmbF_JoggerSkinny_Blue",
            "ZmbF_JoggerSkinny_Brown",
            "ZmbF_JoggerSkinny_Green",
            "ZmbF_JoggerSkinny_Red",
            "ZmbM_MotobikerFat_Beige",
            "ZmbM_MotobikerFat_Black",
            "ZmbM_MotobikerFat_Blue",
            "ZmbM_VillagerOld_Blue",
            "ZmbM_VillagerOld_Green",
            "ZmbM_VillagerOld_White",
            "ZmbM_SkaterYoung_Blue",
            "ZmbM_SkaterYoung_Brown",
            "ZmbM_SkaterYoung_Green",
            "ZmbM_SkaterYoung_Grey",
            "ZmbF_SkaterYoung_Brown",
            "ZmbF_SkaterYoung_Striped",
            "ZmbF_SkaterYoung_Violet",
            "ZmbM_OffshoreWorker_Green",
            "ZmbM_OffshoreWorker_Orange",
            "ZmbM_OffshoreWorker_Red",
            "ZmbM_OffshoreWorker_Yellow",
            "ZmbM_Jacket_beige",
            "ZmbM_Jacket_black",
            "ZmbM_Jacket_blue",
            "ZmbM_Jacket_bluechecks",
            "ZmbM_Jacket_brown",
            "ZmbM_Jacket_greenchecks",
            "ZmbM_Jacket_grey",
            "ZmbM_Jacket_khaki",
            "ZmbM_Jacket_magenta",
            "ZmbM_Jacket_stripes",
            "ZmbF_ShortSkirt_beige",
            "ZmbF_ShortSkirt_black",
            "ZmbF_ShortSkirt_brown",
            "ZmbF_ShortSkirt_green",
            "ZmbF_ShortSkirt_grey",
            "ZmbF_ShortSkirt_checks",
            "ZmbF_ShortSkirt_red",
            "ZmbF_ShortSkirt_stripes",
            "ZmbF_ShortSkirt_white",
            "ZmbF_ShortSkirt_yellow",
            "ZmbF_VillagerOld_Blue",
            "ZmbF_VillagerOld_Green",
            "ZmbF_VillagerOld_Red",
            "ZmbF_VillagerOld_White",
            "ZmbF_MilkMaidOld_Beige",
            "ZmbF_MilkMaidOld_Black",
            "ZmbF_MilkMaidOld_Green",
            "ZmbF_MilkMaidOld_Grey",
            "ZmbF_BlueCollarFat_Blue",
            "ZmbF_BlueCollarFat_Green",
            "ZmbF_BlueCollarFat_Red",
            "ZmbF_BlueCollarFat_White",
            "ZmbF_MechanicNormal_Beige",
            "ZmbF_MechanicNormal_Green",
            "ZmbF_MechanicNormal_Grey",
            "ZmbF_MechanicNormal_Orange",
            "ZmbM_MechanicSkinny_Blue",
            "ZmbM_MechanicSkinny_Grey",
            "ZmbM_MechanicSkinny_Green",
            "ZmbM_MechanicSkinny_Red",
            "ZmbM_ConstrWorkerNormal_Beige",
            "ZmbM_ConstrWorkerNormal_Black",
            "ZmbM_ConstrWorkerNormal_Green",
            "ZmbM_ConstrWorkerNormal_Grey",
            "ZmbM_HeavyIndustryWorker",
        };

            int itemCount = 50;
            int infectedCount = 10;

            if (Containers != null)
                Containers.Add(new ExpansionLootContainer("ExpansionAirdropContainer_Basebuilding", 0, 15, Loot, Infected, itemCount, infectedCount));
        }
        void DefaultMilitary()
        {
            BindingList<ExpansionLoot> Loot = ExpansionLootDefaults.Airdrop_Military();
            BindingList<string> Infected = new BindingList<string>(){
            "ZmbM_usSoldier_normal_Woodland",
            "ZmbM_SoldierNormal",
            "ZmbM_usSoldier_normal_Desert",
            "ZmbM_PatrolNormal_PautRev",
            "ZmbM_PatrolNormal_Autumn",
            "ZmbM_PatrolNormal_Flat",
            "ZmbM_PatrolNormal_Summer",
        };

            int itemCount = 50;
            int infectedCount = 50;

            if (Containers != null)
            {
                Containers.Add(new ExpansionLootContainer("ExpansionAirdropContainer_Military", 0, 20, Loot, Infected, itemCount, infectedCount));
            }
        }
        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (m_Version != ExpansionAirdropConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionAirdropConfig.CurrentVersion}");
                m_Version = ExpansionAirdropConfig.CurrentVersion;
            }

            if (ServerMarkerOnDropLocation != 0 && ServerMarkerOnDropLocation != 1)
            {
                ServerMarkerOnDropLocation = 1;
                fixes.Add("Corrected ServerMarkerOnDropLocation");
            }

            if (Server3DMarkerOnDropLocation != 0 && Server3DMarkerOnDropLocation != 1)
            {
                Server3DMarkerOnDropLocation = 1;
                fixes.Add("Corrected Server3DMarkerOnDropLocation");
            }

            if (ShowAirdropTypeOnMarker != 0 && ShowAirdropTypeOnMarker != 1)
            {
                ShowAirdropTypeOnMarker = 1;
                fixes.Add("Corrected ShowAirdropTypeOnMarker");
            }

            if (HideCargoWhileParachuteIsDeployed != 0 && HideCargoWhileParachuteIsDeployed != 1)
            {
                HideCargoWhileParachuteIsDeployed = 1;
                fixes.Add("Corrected HideCargoWhileParachuteIsDeployed");
            }

            if (HeightIsRelativeToGroundLevel != 0 && HeightIsRelativeToGroundLevel != 1)
            {
                HeightIsRelativeToGroundLevel = 1;
                fixes.Add("Corrected HeightIsRelativeToGroundLevel");
            }

            if (Height == null)
            {
                Height = 250.0m;
                fixes.Add("Set default Height");
            }

            if (DropZoneHeight == null)
            {
                DropZoneHeight = 250.0m;
                fixes.Add("Set default DropZoneHeight");
            }

            if (FollowTerrainFraction == null)
            {
                FollowTerrainFraction = 0.5m;
                fixes.Add("Set default FollowTerrainFraction");
            }

            if (Speed == null)
            {
                Speed = 50.0m;
                fixes.Add("Set default Speed");
            }

            if (DropZoneSpeed == null)
            {
                DropZoneSpeed = 25.0m;
                fixes.Add("Set default DropZoneSpeed");
            }

            if (Radius == null)
            {
                Radius = 100.0m;
                fixes.Add("Set default Radius");
            }

            if (InfectedSpawnRadius == null)
            {
                InfectedSpawnRadius = 50.0m;
                fixes.Add("Set default InfectedSpawnRadius");
            }

            if (InfectedSpawnInterval == null)
            {
                InfectedSpawnInterval = 30;
                fixes.Add("Set default InfectedSpawnInterval");
            }

            if (ItemCount == null)
            {
                ItemCount = 10;
                fixes.Add("Set default ItemCount");
            }

            if (AirdropPlaneClassName == null)
            {
                AirdropPlaneClassName = "";
                fixes.Add("Set default AirdropPlaneClassName");
            }

            if (DropZoneProximityDistance == null)
            {
                DropZoneProximityDistance = 100.0m;
                fixes.Add("Set default DropZoneProximityDistance");
            }

            if (ExplodeAirVehiclesOnCollision == null || (ExplodeAirVehiclesOnCollision != -1  && ExplodeAirVehiclesOnCollision != 0 && ExplodeAirVehiclesOnCollision != 1))
            {
                ExplodeAirVehiclesOnCollision = -1;
                fixes.Add("Corrected ExplodeAirVehiclesOnCollision");
            }

            if(Containers == null)
            {
                Containers = new BindingList<ExpansionLootContainer>();
                DefaultRegular();
                DefaultMedical();
                DefaultBaseBuilding();
                DefaultMilitary();
                fixes.Add("Set default Containers");
            }


            return fixes;
        }
        public ExpansionAirdropSettings Clone()
        {
            return new ExpansionAirdropSettings()
            {
                m_Version = this.m_Version,
                ServerMarkerOnDropLocation = this.ServerMarkerOnDropLocation,
                Server3DMarkerOnDropLocation = this.Server3DMarkerOnDropLocation,
                ShowAirdropTypeOnMarker = this.ShowAirdropTypeOnMarker,
                HideCargoWhileParachuteIsDeployed = this.HideCargoWhileParachuteIsDeployed,
                HeightIsRelativeToGroundLevel = this.HeightIsRelativeToGroundLevel,
                Height = this.Height,
                DropZoneHeight = this.DropZoneHeight,
                FollowTerrainFraction = this.FollowTerrainFraction,
                Speed = this.Speed,
                DropZoneSpeed = this.DropZoneSpeed,
                Radius = this.Radius,
                InfectedSpawnRadius = this.InfectedSpawnRadius,
                InfectedSpawnInterval = this.InfectedSpawnInterval,
                ItemCount = this.ItemCount,
                AirdropPlaneClassName = this.AirdropPlaneClassName,
                DropZoneProximityDistance = this.DropZoneProximityDistance,
                ExplodeAirVehiclesOnCollision = this.ExplodeAirVehiclesOnCollision,
                Containers = new BindingList<ExpansionLootContainer>(this.Containers.Select(Container => Container.Clone()).ToList())
            };
        }
    }
    public class ExpansionLootContainer
    {
        public string Container { get; set; }
        public decimal FallSpeed { get; set; }
        public int Usage { get; set; }
        public decimal Weight { get; set; }
        public BindingList<string> Infected { get; set; }
        public int ItemCount { get; set; }
        public int InfectedCount { get; set; }
        public int SpawnInfectedForPlayerCalledDrops { get; set; }
        public int ExplodeAirVehiclesOnCollision { get; set; }
        public BindingList<ExpansionLoot> Loot { get; set; }

        public ExpansionLootContainer(string container, int usage, decimal weight, BindingList<ExpansionLoot> loot, BindingList<string> infected, int itemCount, int infectedCount, bool spawnInfectedForPlayerCalledDrops = false, decimal fallSpeed = (decimal)4.5)
        {
            Container = container;
            Usage = usage;
            Weight = weight;
            Loot = loot;
            Infected = infected;
            ItemCount = itemCount;
            InfectedCount = infectedCount;
            SpawnInfectedForPlayerCalledDrops = spawnInfectedForPlayerCalledDrops == true ? 1 : 0;
            ExplodeAirVehiclesOnCollision = -1;
            FallSpeed = fallSpeed;
        }
        public ExpansionLootContainer()
        {
            Container = "ExpansionAirdropContainer";
            FallSpeed = (decimal)4.5;
            Usage = 0;
            Weight = 0;
            Loot = new BindingList<ExpansionLoot>();
            Infected = new BindingList<string>();
            ItemCount = 0;
            InfectedCount = 0;
            SpawnInfectedForPlayerCalledDrops = 0;
            ExplodeAirVehiclesOnCollision = -1;
        }

        public override string ToString()
        {
            return Container;
        }
        public override bool Equals(object obj)
        {
            if (obj is not ExpansionLootContainer other)
                return false;

            return Container == other.Container &&
                   FallSpeed == other.FallSpeed &&
                   Usage == other.Usage &&
                   Weight == other.Weight &&
                   ItemCount == other.ItemCount &&
                   InfectedCount == other.InfectedCount &&
                   SpawnInfectedForPlayerCalledDrops == other.SpawnInfectedForPlayerCalledDrops &&
                   ExplodeAirVehiclesOnCollision == other.ExplodeAirVehiclesOnCollision &&
                   Infected.SequenceEqual(other.Infected) &&
                   Loot.SequenceEqual(other.Loot);
        }
        public ExpansionLootContainer Clone()
        {
            return new ExpansionLootContainer
            {
                Container = this.Container,
                FallSpeed = this.FallSpeed,
                Usage = this.Usage,
                Weight = this.Weight,
                ItemCount = this.ItemCount,
                InfectedCount = this.InfectedCount,
                SpawnInfectedForPlayerCalledDrops = this.SpawnInfectedForPlayerCalledDrops,
                ExplodeAirVehiclesOnCollision = this.ExplodeAirVehiclesOnCollision,
                Infected = new BindingList<string>(this.Infected.ToList()),
                Loot = new BindingList<ExpansionLoot>(
                    this.Loot.Select(l => new ExpansionLoot
                    {
                        Name = l.Name,
                        Chance = l.Chance,
                        QuantityPercent = l.QuantityPercent,
                        Max = l.Max,
                        Min = l.Min,
                        Attachments = new BindingList<ExpansionLootVariant>(l.Attachments.Select(a => a.Clone()).ToList()),
                        Variants = new BindingList<ExpansionLootVariant>(l.Variants.Select(v => v.Clone()).ToList())
                    }).ToList()
                )
            };
        }
    }
    public class ExpansionLoot
    {
        public string Name { get; set; }
        public BindingList<ExpansionLootVariant> Attachments { get; set; }
        public decimal Chance { get; set; }
        public decimal QuantityPercent { get; set; }
        public int Max { get; set; }
        public int Min { get; set; }
        public BindingList<ExpansionLootVariant> Variants { get; set; }

        public ExpansionLoot(string name, BindingList<ExpansionLootVariant> attachments = null, decimal chance = 1, decimal quantityPercent = -1, BindingList<ExpansionLootVariant> variants = null, int max = -1, int min = 0)
        {
            Name = name;
            if (attachments != null)
                Attachments = attachments;
            else
                Attachments = new BindingList<ExpansionLootVariant>();
            Chance = chance;
            if (variants == null)
                Variants = new BindingList<ExpansionLootVariant>();
            else
                Variants = variants;

            QuantityPercent = quantityPercent;
            Max = max;
            Min = min;
        }
        public ExpansionLoot()
        {
            Chance = (decimal)0.25;
            QuantityPercent = -1;
            Max = -1;
            Min = 0;
            Attachments = new BindingList<ExpansionLootVariant>();
            Variants = new BindingList<ExpansionLootVariant>();
        }
        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(object obj)
        {
            if (obj is not ExpansionLoot other)
                return false;

            return Name == other.Name &&
                   Chance == other.Chance &&
                   QuantityPercent == other.QuantityPercent &&
                   Max == other.Max &&
                   Min == other.Min &&
                   ListsAreEqual(Attachments, other.Attachments) &&
                   ListsAreEqual(Variants, other.Variants);
        }
        private bool ListsAreEqual(BindingList<ExpansionLootVariant> list1, BindingList<ExpansionLootVariant> list2)
        {
            if (list1 == null || list2 == null)
                return list1 == list2;

            if (list1.Count != list2.Count)
                return false;

            for (int i = 0; i < list1.Count; i++)
            {
                if (!list1[i].Equals(list2[i]))
                    return false;
            }

            return true;
        }
        public ExpansionLoot Clone()
        {
            return new ExpansionLoot()
            {
                Name = this.Name,
                Chance = this.Chance,
                QuantityPercent = this.QuantityPercent,
                Max = this.Max,
                Min = this.Min,
                Attachments = new BindingList<ExpansionLootVariant>(this.Attachments.Select(a => a.Clone()).ToList()),
                Variants = new BindingList<ExpansionLootVariant>(this.Variants.Select(v => v.Clone()).ToList())
            };
        }
    }
    public class ExpansionLootVariant
    {
        public string Name { get; set; }
        public BindingList<ExpansionLootVariant> Attachments { get; set; }
        public decimal Chance { get; set; }

        public ExpansionLootVariant(string _name, BindingList<ExpansionLootVariant> _Attachments = null, decimal _Chance = 1)
        {
            Name = _name;
            if (_Attachments != null)
                Attachments = _Attachments;
            else
                Attachments = new BindingList<ExpansionLootVariant>();
            Chance = _Chance;
        }
        public ExpansionLootVariant()
        {
            Chance = (decimal)0.2;
            Attachments = new BindingList<ExpansionLootVariant>();
        }

        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(object obj)
        {
            if (obj is not ExpansionLootVariant other)
                return false;

            return Name == other.Name &&
                   Chance == other.Chance &&
                   ListsAreEqual(Attachments, other.Attachments);
        }
        private bool ListsAreEqual(BindingList<ExpansionLootVariant> list1, BindingList<ExpansionLootVariant> list2)
        {
            if (list1 == null || list2 == null)
                return list1 == list2;

            if (list1.Count != list2.Count)
                return false;

            for (int i = 0; i < list1.Count; i++)
            {
                if (!list1[i].Equals(list2[i]))
                    return false;
            }

            return true;
        }
        public ExpansionLootVariant Clone()
        {
            return new ExpansionLootVariant()
            {
                Name = this.Name,
                Chance = this.Chance,
                Attachments = new BindingList<ExpansionLootVariant>(this.Attachments.Select(a => a.Clone()).ToList())
            };
        }
    }
    public enum ContainerTypes
    {
        ExpansionAirdropContainer,
        ExpansionAirdropContainer_Medical,
        ExpansionAirdropContainer_Military,
        ExpansionAirdropContainer_Basebuilding,
        ExpansionAirdropContainer_Grey,
        ExpansionAirdropContainer_Blue,
        ExpansionAirdropContainer_Olive,
        ExpansionAirdropContainer_Military_GreenCamo,
        ExpansionAirdropContainer_Military_MarineCamo,
        ExpansionAirdropContainer_Military_OliveCamo,
        ExpansionAirdropContainer_Military_OliveCamo2,
        ExpansionAirdropContainer_Military_WinterCamo
    };
}
