using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpansionPlugin
{
    public class ExpansionAILocationConfig : IConfigLoader
    {
        private readonly string _path;
        public string FileName => Path.GetFileName(_path);
        public string FilePath => _path;
        public ExpansionAILocationSettings Data { get; private set; }
        public bool HasErrors { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool isDirty { get; set; }

        public const int CurrentVersion = 2;

        public ExpansionAILocationConfig(string path)
        {
            _path = path;
        }

        public void Load()
        {
            Data = AppServices.GetRequired<FileService>().LoadOrCreateJson<ExpansionAILocationSettings>(
                _path,
                createNew: () => new ExpansionAILocationSettings(CurrentVersion),
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
                configName: "AILocation"
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
            GetVec3Points();
        }
        public IEnumerable<string> Save()
        {
            if (isDirty)
            {
                SetAILocationsPoints();
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
        public void GetVec3Points()
        {
            foreach (ExpansionAIRoamingLocation location in Data.RoamingLocations)
            {
                location._Position = new Vec3(location.Position);
            }
            foreach (ExpansionAINoGoArea area in Data.NoGoAreas)
            {
                area._Position = new Vec3(area.Position);
            }
        }
        public void SetAILocationsPoints()
        {
            foreach (ExpansionAIRoamingLocation location in Data.RoamingLocations)
            {
                location.Position = location._Position.getfloatarray();
            }
            foreach (ExpansionAINoGoArea area in Data.NoGoAreas)
            {
                area.Position = area._Position.getfloatarray();
            }
        }
    }
    public class ExpansionAILocationSettings
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
			    "Land_Ship_Medium2"  //! AI can get stuck
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
        public override bool Equals(object? obj)
        {
            if (obj is not ExpansionAILocationSettings other)
                return false;

            return m_Version == other.m_Version &&
                   RoamingLocations.SequenceEqual(other.RoamingLocations) &&
                   ExcludedRoamingBuildings.SequenceEqual(other.ExcludedRoamingBuildings) &&
                   NoGoAreas.SequenceEqual(other.NoGoAreas);
        }
        public List<string> FixMissingOrInvalidFields()
        {
            var fixes = new List<string>();

            if (m_Version < ExpansionAILocationConfig.CurrentVersion)
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
    }
    public class ExpansionAIRoamingLocation
    {
        public string? Name { get; set; }
        public float[]? Position { get; set; }
        public decimal? Radius { get; set; }
        public string? Type { get; set; }
        public int? Enabled { get; set; }

        [JsonIgnore]
        public Vec3 _Position { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not ExpansionAIRoamingLocation other)
                return false;

            return Name == other.Name &&
                   _Position.Equals(other._Position) &&
                   Radius == other.Radius &&
                   Type == other.Type &&
                   Enabled == other.Enabled;
        }

    }
    public class ExpansionAINoGoArea
    {
        public string? Name { get; set; }
        public float[]? Position { get; set; }
        public float? Radius { get; set; }
        public float? Height { get; set; }

        [JsonIgnore]
        public Vec3 _Position { get; set; }

        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(object? obj)
        {
            if (obj is not ExpansionAINoGoArea other)
                return false;

            return Name == other.Name &&
                   _Position.Equals(other._Position) &&
                   Radius == other.Radius &&
                   Height == other.Height;
        }


    }
}
