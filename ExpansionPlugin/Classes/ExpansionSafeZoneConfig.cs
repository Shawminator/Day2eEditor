using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpansionPlugin
{
    public class ExpansionSafeZoneConfig : ExpansionBaseIConfigLoader<ExpansionSafeZoneSettings>
    {
        public const int CurrentVersion = 11;

        public ExpansionSafeZoneConfig(string path) : base(path)
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
        protected override ExpansionSafeZoneSettings CreateDefaultData()
        {
            return new ExpansionSafeZoneSettings(CurrentVersion);
        }
        protected override IEnumerable<string> ValidateData()
        {
            return Data.FixMissingOrInvalidFields();
        }
        protected override void OnAfterLoad(ExpansionSafeZoneSettings data)
        {
            Data.SetCircleNames();
            Data.SetPolygonNames();
            Data.SetCylinderNames();
        }
    }
    public class ExpansionSafeZoneSettings : IEquatable<ExpansionSafeZoneSettings>, IDeepCloneable<ExpansionSafeZoneSettings>
    {
        public int m_Version { get; set; }
        public int? Enabled { get; set; }                                                                   // Enable Safezone when set to 1
        public int? FrameRateCheckSafeZoneInMs { get; set; }                                             // How often in ms the server need to check if the player is inside a Safezone
        public BindingList<ExpansionSafeZoneCircle> CircleZones { get; set; }
        public BindingList<ExpansionSafeZonePolygon> PolygonZones { get; set; }
        public BindingList<ExpansionSafeZoneCylinder> CylinderZones { get; set; }
        public int? ActorsPerTick { get; set; }
        public int? DisablePlayerCollision { get; set; }
        public int? DisableVehicleDamageInSafeZone { get; set; }
        public int? EnableForceSZCleanup { get; set; }
        public decimal? ItemLifetimeInSafeZone { get; set; }
        public int? EnableForceSZCleanupVehicles { get; set; }
        public decimal? VehicleLifetimeInSafeZone { get; set; }
        public BindingList<string> ForceSZCleanup_ExcludedItems { get; set; }

        public ExpansionSafeZoneSettings() { }
        public ExpansionSafeZoneSettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
            Enabled = 1;
            DisablePlayerCollision = 0;
            DisableVehicleDamageInSafeZone = 1;
            FrameRateCheckSafeZoneInMs = 0;
            ActorsPerTick = 5;
            EnableForceSZCleanup = 1;
            ItemLifetimeInSafeZone = 15 * 60;  //! 15 Minutes
            EnableForceSZCleanupVehicles = 1;
            VehicleLifetimeInSafeZone = 60 * 60;  //! 60 Minutes
            CircleZones = new BindingList<ExpansionSafeZoneCircle>();
            PolygonZones = new BindingList<ExpansionSafeZonePolygon>();
            CylinderZones = new BindingList<ExpansionSafeZoneCylinder>();
            ForceSZCleanup_ExcludedItems = new BindingList<string>() { "CarCoverBase", "ExpansionVehicleCover" };
            DefaultChernarusSafeZones();
        }
        void DefaultChernarusSafeZones()
        {
            //! Krasnostav Trader Camp
            PolygonZones.Add(new ExpansionSafeZonePolygon()
            {
                Positions = new BindingList<Vec3>()
                {
                    new Vec3( 12288.9f, 142.4f, 12804.4f ),
                    new Vec3( 12068.4f, 139.8f, 12923.4f ),
                    new Vec3( 11680.6f, 141.1f, 12650.6f ),
                    new Vec3( 11805.3f, 146.3f, 12258.9f ),
                    new Vec3( 12327.7f, 140.0f, 12453.8f )
                }
            });
            //! Green Mountain Trader Camp
            CircleZones.Add(new ExpansionSafeZoneCircle()
            {
                Center = new Vec3(3728.27f, 403f, 6003.6f),
                Radius = 500
            });
            //! Kamenka Trader Camp
            CircleZones.Add(new ExpansionSafeZoneCircle()
            {
                Center = new Vec3(1143.14f, 6.9f, 2423.27f),
                Radius = 700
            });
        }
        public void SetCircleNames()
        {
            int i = 0;
            foreach (ExpansionSafeZoneCircle CZ in CircleZones)
            {
                CZ.CircleSafeZoneName = "Circle Zone " + i.ToString();
                i++;
            }
        }
        public void SetPolygonNames()
        {
            int i = 0;
            foreach (ExpansionSafeZonePolygon PZ in PolygonZones)
            {
                PZ.polygonSafeZoneName = "Polygon Zone " + i.ToString();
                i++;
            }
        }
        public void SetCylinderNames()
        {
            int i = 0;
            foreach (ExpansionSafeZoneCylinder PZ in CylinderZones)
            {
                PZ.CylinderSafeZoneName = "Cylinder Zone " + i.ToString();
                i++;
            }
        }
        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (m_Version != ExpansionSafeZoneConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionSafeZoneConfig.CurrentVersion}");
                m_Version = ExpansionSafeZoneConfig.CurrentVersion;
            }
            if (Enabled is null || Enabled < 0 || Enabled > 1)
            {
                Enabled = 1;
                fixes.Add("Corrected Enabled");
            }
            if (DisablePlayerCollision is null || DisablePlayerCollision < 0 || DisablePlayerCollision > 1)
            {
                DisablePlayerCollision = 0;
                fixes.Add("Corrected DisablePlayerCollision");
            }
            if (DisableVehicleDamageInSafeZone is null || DisableVehicleDamageInSafeZone < 0 || DisableVehicleDamageInSafeZone > 1)
            {
                DisableVehicleDamageInSafeZone = 1;
                fixes.Add("Corrected DisableVehicleDamageInSafeZone");
            }
            if (FrameRateCheckSafeZoneInMs is null)
            {
                FrameRateCheckSafeZoneInMs = 0;
                fixes.Add("Corrected FrameRateCheckSafeZoneInMs");
            }
            if (ActorsPerTick is null)
            {
                ActorsPerTick = 5;
                fixes.Add("Corrected ActorsPerTick");
            }
            if (EnableForceSZCleanup is null || EnableForceSZCleanup < 0 || EnableForceSZCleanup > 1)
            {
                EnableForceSZCleanup = 1;
                fixes.Add("Corrected EnableForceSZCleanup");
            }
            if (ItemLifetimeInSafeZone is null)
            {
                ItemLifetimeInSafeZone = 15 * 60;
                fixes.Add("Corrected ItemLifetimeInSafeZone");
            }
            if (EnableForceSZCleanupVehicles is null || EnableForceSZCleanupVehicles < 0 || EnableForceSZCleanupVehicles > 1)
            {
                EnableForceSZCleanupVehicles = 1;
                fixes.Add("Corrected EnableForceSZCleanupVehicles");
            }
            if (VehicleLifetimeInSafeZone is null)
            {
                VehicleLifetimeInSafeZone = 60 * 60;
                fixes.Add("Corrected VehicleLifetimeInSafeZone");
            }
            if(CircleZones == null)
            {
                CircleZones = new BindingList<ExpansionSafeZoneCircle>();
                fixes.Add("Initialised CircleZones");
            }
            if (PolygonZones == null)
            {
                PolygonZones = new BindingList<ExpansionSafeZonePolygon>();
                fixes.Add("Initialised PolygonZones");
            }
            if (CylinderZones == null)
            {
                CylinderZones = new BindingList<ExpansionSafeZoneCylinder>();
                fixes.Add("Initialised CylinderZones");
            }
            if (ForceSZCleanup_ExcludedItems == null)
            {
                ForceSZCleanup_ExcludedItems = new BindingList<string>() { "CarCoverBase", "ExpansionVehicleCover" };
                fixes.Add("Corrected ForceSZCleanup_ExcludedItems");
            }
            return fixes;
        }
        public bool Equals(ExpansionSafeZoneSettings other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (m_Version != other.m_Version ||
                Enabled != other.Enabled ||
                FrameRateCheckSafeZoneInMs != other.FrameRateCheckSafeZoneInMs ||
                ActorsPerTick != other.ActorsPerTick ||
                DisablePlayerCollision != other.DisablePlayerCollision ||
                DisableVehicleDamageInSafeZone != other.DisableVehicleDamageInSafeZone ||
                EnableForceSZCleanup != other.EnableForceSZCleanup ||
                ItemLifetimeInSafeZone != other.ItemLifetimeInSafeZone ||
                EnableForceSZCleanupVehicles != other.EnableForceSZCleanupVehicles ||
                VehicleLifetimeInSafeZone != other.VehicleLifetimeInSafeZone)
                return false;

            if (!ListEquals(CircleZones, other.CircleZones))
                return false;

            if (!ListEquals(PolygonZones, other.PolygonZones))
                return false;

            if (!ListEquals(CylinderZones, other.CylinderZones))
                return false;

            if (!ListEquals(ForceSZCleanup_ExcludedItems, other.ForceSZCleanup_ExcludedItems))
                return false;

            return true;
        }
        public override bool Equals(object? obj) => Equals(obj as ExpansionSafeZoneSettings);
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
        public ExpansionSafeZoneSettings Clone()
        {
            return new ExpansionSafeZoneSettings()
            {
                m_Version = this.m_Version,
                Enabled = this.Enabled,
                FrameRateCheckSafeZoneInMs = this.FrameRateCheckSafeZoneInMs,
                CircleZones = new BindingList<ExpansionSafeZoneCircle>(this.CircleZones.Select(x => x.Clone()).ToList()),
                PolygonZones = new BindingList<ExpansionSafeZonePolygon>(this.PolygonZones.Select(x => x.Clone()).ToList()),
                CylinderZones = new BindingList<ExpansionSafeZoneCylinder>(this.CylinderZones.Select(x => x.Clone()).ToList()),
                ActorsPerTick = this.ActorsPerTick,
                DisablePlayerCollision = this.DisablePlayerCollision,
                DisableVehicleDamageInSafeZone = this.DisableVehicleDamageInSafeZone,
                EnableForceSZCleanup = this.EnableForceSZCleanup,
                ItemLifetimeInSafeZone = this.ItemLifetimeInSafeZone,
                EnableForceSZCleanupVehicles = this.EnableForceSZCleanupVehicles,
                VehicleLifetimeInSafeZone = this.VehicleLifetimeInSafeZone,
                ForceSZCleanup_ExcludedItems = new BindingList<string>(this.ForceSZCleanup_ExcludedItems.ToList()),
            };
        }
    }
    public class ExpansionSafeZoneCircle
    {
        public Vec3 Center { get; set; }
        public decimal? Radius { get; set; }

        [JsonIgnore]
        public string CircleSafeZoneName { get; set; }

        public override string ToString()
        {
            return CircleSafeZoneName;
        }
        public override bool Equals(object obj)
        {
            if (obj is not ExpansionSafeZoneCircle other)
                return false;

            return Center.Equals(other.Center) &&
                Radius == other.Radius &&
                CircleSafeZoneName == other.CircleSafeZoneName;
        }
        public ExpansionSafeZoneCircle Clone()
        {
            return new ExpansionSafeZoneCircle()
            {
                Center = this.Center.Clone(),
                Radius = this.Radius,
                CircleSafeZoneName = this.CircleSafeZoneName
            };
        }
    }
    public class ExpansionSafeZonePolygon
    {
        public BindingList<Vec3> Positions { get; set; }

        [JsonIgnore]
        public string polygonSafeZoneName { get; set; }

        public override string ToString()
        {
            return polygonSafeZoneName;
        }
        public override bool Equals(object obj)
        {
            if (obj is not ExpansionSafeZonePolygon other)
                return false;

            if (polygonSafeZoneName != other.polygonSafeZoneName)
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
        public ExpansionSafeZonePolygon Clone()
        {
            return new ExpansionSafeZonePolygon()
            {
                Positions = new BindingList<Vec3>(this.Positions.Select(x => x.Clone()).ToList()),
                polygonSafeZoneName = this.polygonSafeZoneName
            };
        }
    }
    public class ExpansionSafeZoneCylinder
    {
        public Vec3? Center { get; set; }
        public decimal? Radius { get; set; }
        public decimal? Height { get; set; }

        [JsonIgnore]
        public string CylinderSafeZoneName { get; set; }

        public override string ToString()
        {
            return CylinderSafeZoneName;
        }
        public override bool Equals(object obj)
        {
            if (obj is not ExpansionSafeZoneCylinder other)
                return false;

            return Center.Equals(other.Center) &&
                Radius == other.Radius &&
                Height == other.Height &&
                CylinderSafeZoneName == other.CylinderSafeZoneName;
        }
        public ExpansionSafeZoneCylinder Clone()
        {
            return new ExpansionSafeZoneCylinder()
            {
                Center = this.Center.Clone(),
                Radius = this.Radius,
                Height = this.Height,
                CylinderSafeZoneName = this.CylinderSafeZoneName
            };
        }
    }
}
