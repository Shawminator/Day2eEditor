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
        public void DefaultExcludedRoamingBuildings()
        {
            ExcludedRoamingBuildings = new BindingList<string>()
            {
                "Land_CementWorks_Hall2_Grey",
                "Land_Factory_Small",
                "Land_House_1W09",
                "Land_House_2W03",
                "Land_HouseBlock_1F4",
                "Land_Boathouse",
                "Land_Mine_Building",
                "Land_Shed_W2",
                "Land_Tenement_Big",
                "Land_Misc_Toilet_Mobile",
                "Land_Ship_Medium2"
            };
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
                DefaultExcludedRoamingBuildings();
                fixes.Add("Initilised Defualt ExcludedRoamingBuildings");
            }
            if(NoGoAreas == null)
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
