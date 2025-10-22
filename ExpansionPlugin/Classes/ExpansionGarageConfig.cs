using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionGarageConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path);
        public string FilePath => _path;
        public ExpansionGarageSettings Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }

        public const int CurrentVersion = 6;

        public ExpansionGarageConfig(string path)
        {
            _path = path;
        }

        public void Load()
        {
            Data = AppServices.GetRequired<FileService>().LoadOrCreateJson<ExpansionGarageSettings>(
                _path,
                createNew: () => new ExpansionGarageSettings(CurrentVersion),
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
                configName: "ExpansionGarage"
            );
            var missingFields = Data.FixMissingOrInvalidFields();
            if (missingFields.Any())
            {
                HasErrors = true;
                Errors.AddRange(missingFields);
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
    }
    public class ExpansionGarageSettings
    {
        public int m_Version { get; set; }
        public int? Enabled { get; set; }
        public int? AllowStoringDEVehicles { get; set; }
        public int? GarageMode { get; set; }
        public int? GarageStoreMode { get; set; }
        public int? GarageRetrieveMode { get; set; }
        public int? MaxStorableVehicles { get; set; }
        public decimal? VehicleSearchRadius { get; set; }
        public decimal? MaxDistanceFromStoredPosition { get; set; }
        public int? CanStoreWithCargo { get; set; }
        public int? UseVirtualStorageForCargo { get; set; }
        public int? NeedKeyToStore { get; set; }
        public BindingList<string> EntityWhitelist { get; set; }
        public int? EnableGroupFeatures { get; set; }
        public int? GroupStoreMode { get; set; }
        public int? EnableMarketFeatures { get; set; }
        public decimal? StorePricePercent { get; set; }
        public int? StaticStorePrice { get; set; }
        public int? MaxStorableTier1 { get; set; }
        public int? MaxStorableTier2 { get; set; }
        public int? MaxStorableTier3 { get; set; }
        public decimal? MaxRangeTier1 { get; set; }
        public decimal? MaxRangeTier2 { get; set; }
        public decimal? MaxRangeTier3 { get; set; }
        public int? ParkingMeterEnableFlavor { get; set; }

        public ExpansionGarageSettings()
        {
        }
        public ExpansionGarageSettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
            Enabled = 1;
            AllowStoringDEVehicles = 0;

            GarageMode = (int)ExpansionGarageMode.Personal;

            GarageStoreMode = 0;
            GarageRetrieveMode = 0;
            MaxStorableVehicles = 2;

            VehicleSearchRadius = (decimal)20.0;
            MaxDistanceFromStoredPosition = (decimal)150.0;
            CanStoreWithCargo = 1;
            NeedKeyToStore = 1;

            EntityWhitelist = new BindingList<string>() { "ExpansionParkingMeter" };

            EnableGroupFeatures = 0;
            GroupStoreMode = 2;

            EnableMarketFeatures = 0;
            StorePricePercent = (decimal)5.0;
            StaticStorePrice = 0;

            MaxStorableTier1 = 2;
            MaxStorableTier2 = 4;
            MaxStorableTier3 = 6;
            MaxRangeTier1 = (decimal)20.0;
            MaxRangeTier2 = (decimal)30.0;
            MaxRangeTier3 = (decimal)40.0;
            ParkingMeterEnableFlavor = 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is not ExpansionGarageSettings other)
                return false;

            return m_Version == other.m_Version &&
                   Enabled == other.Enabled &&
                   AllowStoringDEVehicles == other.AllowStoringDEVehicles &&
                   GarageMode == other.GarageMode &&
                   GarageStoreMode == other.GarageStoreMode &&
                   GarageRetrieveMode == other.GarageRetrieveMode &&
                   MaxStorableVehicles == other.MaxStorableVehicles &&
                   VehicleSearchRadius == other.VehicleSearchRadius &&
                   MaxDistanceFromStoredPosition == other.MaxDistanceFromStoredPosition &&
                   CanStoreWithCargo == other.CanStoreWithCargo &&
                   UseVirtualStorageForCargo == other.UseVirtualStorageForCargo &&
                   NeedKeyToStore == other.NeedKeyToStore &&
                   EnableGroupFeatures == other.EnableGroupFeatures &&
                   GroupStoreMode == other.GroupStoreMode &&
                   EnableMarketFeatures == other.EnableMarketFeatures &&
                   StorePricePercent == other.StorePricePercent &&
                   StaticStorePrice == other.StaticStorePrice &&
                   MaxStorableTier1 == other.MaxStorableTier1 &&
                   MaxStorableTier2 == other.MaxStorableTier2 &&
                   MaxStorableTier3 == other.MaxStorableTier3 &&
                   MaxRangeTier1 == other.MaxRangeTier1 &&
                   MaxRangeTier2 == other.MaxRangeTier2 &&
                   MaxRangeTier3 == other.MaxRangeTier3 &&
                   ParkingMeterEnableFlavor == other.ParkingMeterEnableFlavor &&
                   EntityWhitelist.SequenceEqual(other.EntityWhitelist ?? new BindingList<string>());
        }

        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (m_Version < ExpansionGarageConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionGarageConfig.CurrentVersion}");
                m_Version = ExpansionGarageConfig.CurrentVersion;
            }

            if (Enabled == null || (Enabled != 0 && Enabled != 1))
            {
                Enabled = 1;
                fixes.Add("Corrected Enabled to 1");
            }

            if (AllowStoringDEVehicles == null || (AllowStoringDEVehicles != 0 && AllowStoringDEVehicles != 1))
            {
                AllowStoringDEVehicles = 0;
                fixes.Add("Corrected AllowStoringDEVehicles to 0");
            }

            if (GarageMode == null || (GarageMode < 0 || GarageMode > 1))
            {
                GarageMode = (int)ExpansionGarageMode.Territory;
                fixes.Add("Set default GarageMode to Territory");
            }

            if (GarageStoreMode == null || (GarageStoreMode < 0 || GarageStoreMode > 2))
            {
                GarageStoreMode = (int)ExpansionGarageStoreMode.Personal;
                fixes.Add("Set default GarageStoreMode to Personal");
            }

            if (GarageRetrieveMode == null || (GarageRetrieveMode < 0 || GarageRetrieveMode > 2))
            {
                GarageRetrieveMode = (int)ExpansionGarageRetrieveMode.Personal;
                fixes.Add("Set default GarageRetrieveMode to Personal");
            }

            if (MaxStorableVehicles == null)
            {
                MaxStorableVehicles = 2;
                fixes.Add("Set default MaxStorableVehicles to 2");
            }

            if (VehicleSearchRadius == null)
            {
                VehicleSearchRadius = 20.0m;
                fixes.Add("Set default VehicleSearchRadius to 20.0");
            }

            if (MaxDistanceFromStoredPosition == null)
            {
                MaxDistanceFromStoredPosition = 150.0m;
                fixes.Add("Set default MaxDistanceFromStoredPosition to 150.0");
            }

            if (CanStoreWithCargo == null || (CanStoreWithCargo != 0 && CanStoreWithCargo != 1))
            {
                CanStoreWithCargo = 1;
                fixes.Add("Corrected CanStoreWithCargo to 1");
            }

            if (UseVirtualStorageForCargo == null || (UseVirtualStorageForCargo != 0 && UseVirtualStorageForCargo != 1))
            {
                UseVirtualStorageForCargo = 0;
                fixes.Add("Corrected UseVirtualStorageForCargo to 0");
            }

            if (NeedKeyToStore == null || (NeedKeyToStore != 0 && NeedKeyToStore != 1))
            {
                NeedKeyToStore = 1;
                fixes.Add("Corrected NeedKeyToStore to 1");
            }

            if (EntityWhitelist == null)
            {
                EntityWhitelist = new BindingList<string> { "ExpansionParkingMeter" };
                fixes.Add("Initialized EntityWhitelist with default value");
            }

            if (EnableGroupFeatures == null || (EnableGroupFeatures != 0 && EnableGroupFeatures != 1))
            {
                EnableGroupFeatures = 0;
                fixes.Add("Corrected EnableGroupFeatures to 0");
            }

            if (GroupStoreMode == null || (GarageStoreMode < 0 || GarageStoreMode > 2))
            {
                GroupStoreMode = (int)ExpansionGarageGroupStoreMode.StoreAndRetrieve;
                fixes.Add("Set default GroupStoreMode to StoreAndRetrieve");
            }

            if (EnableMarketFeatures == null || (EnableMarketFeatures != 0 && EnableMarketFeatures != 1))
            {
                EnableMarketFeatures = 0;
                fixes.Add("Corrected EnableMarketFeatures to 0");
            }

            if (StorePricePercent == null)
            {
                StorePricePercent = 5.0m;
                fixes.Add("Set default StorePricePercent to 5.0");
            }

            if (StaticStorePrice == null)
            {
                StaticStorePrice = 0;
                fixes.Add("Set default StaticStorePrice to 0");
            }

            if (MaxStorableTier1 == null)
            {
                MaxStorableTier1 = 2;
                fixes.Add("Set default MaxStorableTier1 to 2");
            }

            if (MaxStorableTier2 == null)
            {
                MaxStorableTier2 = 4;
                fixes.Add("Set default MaxStorableTier2 to 4");
            }

            if (MaxStorableTier3 == null)
            {
                MaxStorableTier3 = 6;
                fixes.Add("Set default MaxStorableTier3 to 6");
            }

            if (MaxRangeTier1 == null)
            {
                MaxRangeTier1 = 20.0m;
                fixes.Add("Set default MaxRangeTier1 to 20.0");
            }

            if (MaxRangeTier2 == null)
            {
                MaxRangeTier2 = 30.0m;
                fixes.Add("Set default MaxRangeTier2 to 30.0");
            }

            if (MaxRangeTier3 == null)
            {
                MaxRangeTier3 = 40.0m;
                fixes.Add("Set default MaxRangeTier3 to 40.0");
            }

            if (ParkingMeterEnableFlavor == null || (ParkingMeterEnableFlavor != 0 && ParkingMeterEnableFlavor != 1))
            {
                ParkingMeterEnableFlavor = 1;
                fixes.Add("Corrected ParkingMeterEnableFlavor to 1");
            }

            return fixes;
        }
    }
    public enum ExpansionGarageMode
    {
        Territory = 0,
        Personal
    }
    public enum ExpansionGarageStoreMode
    {
        Personal = 0,
        TerritoryShared,
        TerritoryTyrannical
    }
    public enum ExpansionGarageRetrieveMode
    {
        Personal = 0,
        TerritoryShared,
        TerritoryTyrannical
    }
    public enum ExpansionGarageGroupStoreMode
    {
        [Description("Group members can only store vehicles of other group members")]
        StoreOnly = 0,
        [Description("Group members can only retrieve vehicles of other group members")]
        RetrieveOnly,
        [Description("Group members can store and retrieve vehicles of other group members.")]
        StoreAndRetrieve
    }
}