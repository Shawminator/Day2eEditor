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
    public class ExpansionAILocationConfig : ExpansionBaseIConfigLoader<ExpansionAILocationSettings>
    {
        public const int CurrentVersion = 3;

        public ExpansionAILocationConfig(string path) : base(path)
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
                AppServices.GetRequired<FileService>().SaveJson(_path, Data,false, true);
                ClonedData = CloneData(Data);
                return new[] { Path.GetFileName(_path) };
            }

            return Array.Empty<string>();
        }
        protected override ExpansionAILocationSettings CreateDefaultData()
        {
            return new ExpansionAILocationSettings(CurrentVersion);
        }
        protected override IEnumerable<string> ValidateData()
        {
            return Data.FixMissingOrInvalidFields();
        }
    }
    public class ExpansionAILocationSettings : IEquatable<ExpansionAILocationSettings>, IDeepCloneable<ExpansionAILocationSettings>
    {
        public int m_Version { get; set; }
        public BindingList<ExpansionAIRoamingLocation> RoamingLocations { get; set; }
        public BindingList<string> ExcludedRoamingBuildings { get; set; }
        public BindingList<ExpansionAINoGoArea> NoGoAreas { get; set; }

        public ExpansionAILocationSettings()
        {
        }
        public ExpansionAILocationSettings(int CurrentVersion)
        {
            m_Version = CurrentVersion;
            RoamingLocations = new BindingList<ExpansionAIRoamingLocation>();
            DefaultExcludedRoamingBuildings();
            NoGoAreas = new BindingList<ExpansionAINoGoArea>();
        }
        public List<string> DefaultExcludedRoamingBuildings()
        {
            List<string> result = new List<string>();
            List<string> defaults = new List<string>()
            {
                "Land_Boat_",  //! Sakhal, pathfinding won't find a path off the boat
			    "Land_CementWorks_Hall2_Grey",  //! Unsuitable path endpoint
			    "Land_Factory_Small",  //! Unsuitable path endpoint
			    "Land_House_1W09",  //! Path endpoint between opened door and wall leading to AI running in circles trying to reach point
			    "Land_House_2W03",  //! Unsuitable path endpoint
			    "Land_HouseBlock_1F4",  //! Path endpoint behind unopenable lattice door
			    "Land_Boathouse",  //! When AI falls into water it will likely get stuck under pier due to path always leading under it
			    "Land_Mine_Building",  //! Stairs
			    "Land_Shed_W2",  //! Pathfinding tends to find a way in, but not out.
			    "Land_Tenement_Big",  //! AI can get stuck on upper floors when climbing the broken stairs
			    "Land_Misc_Toilet_Mobile",  //! Too many door interactions
			    "Land_Ship_Medium2",  //! AI can get stuck
                "Land_Train_Wagon_Box"//! Inside may not be Reachable
            };
            foreach(string value in defaults)
            {
                if(!ExcludedRoamingBuildings.Contains(value))
                {
                    ExcludedRoamingBuildings.Add(value);
                    result.Add(value);
                }
            }
            return result;
        }
        public bool Equals(ExpansionAILocationSettings other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return m_Version == other.m_Version &&
                   RoamingLocations.SequenceEqual(other.RoamingLocations) &&
                   ExcludedRoamingBuildings.SequenceEqual(other.ExcludedRoamingBuildings) &&
                   NoGoAreas.SequenceEqual(other.NoGoAreas);
        }
        public override bool Equals(object obj) => Equals(obj as ExpansionAILocationSettings);
        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (m_Version != ExpansionAILocationConfig.CurrentVersion)
            {
                fixes.Add($"Updated version from {m_Version} to {ExpansionAILocationConfig.CurrentVersion}");
                m_Version = ExpansionAILocationConfig.CurrentVersion;
            }
            if(RoamingLocations == null)
            {
                RoamingLocations = new BindingList<ExpansionAIRoamingLocation>();
                fixes.Add("Initilised RoamingLocations");
            }
            if(ExcludedRoamingBuildings == null)
            {
                ExcludedRoamingBuildings = new BindingList<string>();
                fixes.Add("Initilised ExcludedRoamingBuildings");
            }
            var result = DefaultExcludedRoamingBuildings();
            if (result.Count() > 0)
            {
                string message = "Updated ExcludedRoamingBuildings";
                foreach (string r in result)
                {
                    message += $"\n{r} Added to List";
                }
                fixes.Add(message);
            }
            if (NoGoAreas == null)
            {
                NoGoAreas = new BindingList<ExpansionAINoGoArea>();
                fixes.Add("Initilised RoamingLocations");
            }
            return fixes;
        }
        public ExpansionAILocationSettings Clone()
        {
            return new ExpansionAILocationSettings
            {
                m_Version = this.m_Version,
                RoamingLocations = this.RoamingLocations != null
                    ? new BindingList<ExpansionAIRoamingLocation>(this.RoamingLocations.Select(cat => cat.Clone()).ToList())
                    : null,
                ExcludedRoamingBuildings = this.ExcludedRoamingBuildings != null
                    ? new BindingList<string>(this.ExcludedRoamingBuildings.ToList())
                    : null,
                NoGoAreas = this.NoGoAreas != null
                    ? new BindingList<ExpansionAINoGoArea>(this.NoGoAreas.Select(cat => cat.Clone()).ToList())
                    : null
            };
        }
    }
    public class ExpansionAIRoamingLocation
    {
        public string? Name { get; set; }
        public Vec3? Position { get; set; }
        public decimal? Radius { get; set; }
        public string? Type { get; set; }
        public int? Enabled { get; set; }


        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(object? obj)
        {
            if (obj is not ExpansionAIRoamingLocation other)
                return false;

            return Name == other.Name &&
                   Position.Equals(other.Position) &&
                   Radius == other.Radius &&
                   Type == other.Type &&
                   Enabled == other.Enabled;
        }
        public ExpansionAIRoamingLocation Clone()
        {
            return new ExpansionAIRoamingLocation()
            {
                Name = this.Name,
                Radius = this.Radius,
                Type = this.Type,
                Enabled = this.Enabled,
                Position = this.Position.Clone()
            };
        }

    }
    public class ExpansionAINoGoArea
    {
        public string? Name { get; set; }
        public Vec3? Position { get; set; }
        public float? Radius { get; set; }
        public float? Height { get; set; }


        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(object? obj)
        {
            if (obj is not ExpansionAINoGoArea other)
                return false;

            return Name == other.Name &&
                   Position.Equals(other.Position) &&
                   Radius == other.Radius &&
                   Height == other.Height;
        }
        public ExpansionAINoGoArea Clone()
        {
            return new ExpansionAINoGoArea()
            {
                Name = this.Name,
                Position = this.Position.Clone(),
                Radius = this.Radius,
                Height = this.Height,
            };
        }
    }
}
